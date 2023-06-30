using System;
using System.Linq;
using System.Windows;

namespace UpdateNeighborAppartementsPlugin.UI
{
    public partial class AnalyzeDocumentView : Window
    {
        public AnalyzeDocumentView(AnalyzeDocumentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
