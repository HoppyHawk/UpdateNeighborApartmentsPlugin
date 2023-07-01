using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revit.Async;
using System;
using System.Linq;
using UpdateNeighborAppartementsPlugin.Analyzers;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel;
using UpdateNeighborAppartementsPlugin.Model;
using UpdateNeighborAppartementsPlugin.Service;
using UpdateNeighborAppartementsPlugin.UI;

namespace UpdateNeighborAppartementsPlugin
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ShowAnalyzeDocumentViewCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (commandData.Application.ActiveUIDocument == null
                || commandData.Application.ActiveUIDocument.Document == null)
                return Result.Cancelled;

            RevitTask.Initialize(commandData.Application);

            Document activeDocument = commandData.Application.ActiveUIDocument.Document;

            IElementsLoader elementsLoader = new DocumentElementsLoader(activeDocument);
            IElementsUpdater elementsUpdater = new RoomsColorUpdater(activeDocument);
            IDocumentTreeBuilder documentTreeBuilder = new DefaultDocumentTreeBuilder();

            INodeCombinationsFilter combinationsFilter = new CompositeNodeCombinationsFilter();
            IDocumentTreeAnalyzer neighborAppartementsAnalyzer = new NeighborApartmentsAnalyzer(combinationsFilter);

            var apartmentService = new NeighborApartmentsService(elementsLoader, 
                documentTreeBuilder, neighborAppartementsAnalyzer, elementsUpdater);

            var viewModel = new AnalyzeDocumentViewModel(apartmentService);
            var analyzeDocumentView = new AnalyzeDocumentView(viewModel);
            analyzeDocumentView.Show();

            return Result.Succeeded;
        }
    }
}
