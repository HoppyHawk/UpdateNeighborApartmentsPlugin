using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;
using UpdateNeighborAppartementsPlugin.Service;
using UpdateNeighborAppartementsPlugin.UI.Commands;

namespace UpdateNeighborAppartementsPlugin.UI
{
    public enum AnalyzeDocumentViewModelState
    {
        Empty,
        Loading,
        Loaded,
        Processing,
        Processed
    }

    public class AnalyzeDocumentViewModel : INotifyPropertyChanged
    {
        private INeighborApartmentsService apartmentsService;

        private List<ApartmentNode> apartments;
        private List<ApartmentNode> apartmentsToProcess;
        private AnalyzeDocumentViewModelState processingState;

        private string searchStatistics;

        public AnalyzeDocumentViewModel(INeighborApartmentsService apartmentsService) {
            this.apartmentsService = apartmentsService;
            StartAnalysisCommand = new ActionCommand(OnStartAnalysisExecute);
            UpdateAppartmentsCommand = new ActionCommand(OnUpdateAppartmentsExecute);
        }

        public List<ApartmentNode> Apartments
        {
            get
            {
                return apartments;
            }
            set
            {
                apartments = value;
                OnPropertyChanged(nameof(Apartments));
            }
        }

        public AnalyzeDocumentViewModelState ProcessingState {
            get { return processingState; }
            set {
                processingState = value;
                OnPropertyChanged(nameof(ProcessingState));
                OnPropertyChanged(nameof(IsProcessing));
                OnPropertyChanged(nameof(ProcessingStatus));
            }
        }

        public bool IsProcessing =>
            ProcessingState == AnalyzeDocumentViewModelState.Loading
            || ProcessingState == AnalyzeDocumentViewModelState.Processing;

        public string ProcessingStatus {
            get {
                if (ProcessingState == AnalyzeDocumentViewModelState.Loading)
                    return "Выполняется анализ документа...";

                if (ProcessingState == AnalyzeDocumentViewModelState.Processing)
                    return "Выполняется обработка помещений...";

                return string.Empty;
            }
        }

        public string SearchStatistics { 
            get { return searchStatistics; }
            set { 
                searchStatistics = value;
                OnPropertyChanged(nameof(SearchStatistics));
            }
        }

        public ICommand StartAnalysisCommand { get; }
        public ICommand UpdateAppartmentsCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void OnStartAnalysisExecute()
        {
            await LoadAsync();
        }
        private async void OnUpdateAppartmentsExecute()
        {
            await UpdateAppartmentsAsync();
        }

        private async Task LoadAsync() {
            ProcessingState = AnalyzeDocumentViewModelState.Loading;
            try
            {   
                var apartmentNodes = await LoadAppartmentNodesAsync();
                apartmentsToProcess = await apartmentsService.FindFirstNeighborApartments();
                foreach (var apartment in apartmentNodes)
                    apartment.IsSelected = apartmentsToProcess.Contains(apartment);

                Apartments = apartmentNodes;
                UpdateSearchStatistics();
            }
            finally {
                ProcessingState = AnalyzeDocumentViewModelState.Loaded;
            }
        }

        private async Task<List<ApartmentNode>> LoadAppartmentNodesAsync()
        {
            return await apartmentsService.FindDistinctNeighborApartments();
        }

        private async Task UpdateAppartmentsAsync() {
            ProcessingState = AnalyzeDocumentViewModelState.Processing;
            try
            {
                foreach (var apartment in apartmentsToProcess) { 
                    await apartmentsService.UpdateApartment(apartment);
                }
                //await apartmentsService.UpdateNeighborApartments(apartmentsToProcess);
            }
            finally {
                ProcessingState = AnalyzeDocumentViewModelState.Processed;
            }
        }

        private void UpdateSearchStatistics() {
            var apartmentCount = apartments.Count;
            var apartmentToProcessCount = apartmentsToProcess.Count;
            var elementCount = apartmentsToProcess.SelectMany(a => a.Elements).Count();
            SearchStatistics = $"Найдено {apartments.Count} квартир одинаковой комнатности, примыкающих друг к другу. " +
                $"Квартиры, выбранные для обработки, отмечены красным. " +
                $"Будет обработано {apartmentsToProcess.Count} квартир ({elementCount} помещений).";
        }

    }
}
