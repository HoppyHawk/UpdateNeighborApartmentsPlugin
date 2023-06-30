using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace UpdateNeighborAppartementsPlugin.UI.Converters
{
    public class StateToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Collapsed;

            if (!(value.GetType().IsEnum && parameter.GetType().IsEnum))
                return Visibility.Collapsed;

            var currentState = (AnalyzeDocumentViewModelState)value;
            var expectedState = (AnalyzeDocumentViewModelState)parameter;

            return currentState == expectedState
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
