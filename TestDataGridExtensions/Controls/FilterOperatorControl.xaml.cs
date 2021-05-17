using DataGridExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestDataGridExtensions.Controls
{
    /// <summary>
    /// Logique d'interaction pour FilterOperatorControl.xaml
    /// </summary>
    public partial class FilterOperatorControl : Control
    {
        #region Properties

        public double? Value
        {
            get => (double?)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double?), typeof(FilterOperatorControl),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((FilterOperatorControl)sender).Range_Changed()));

        public FilterOperator Operator
        {
            get { return (FilterOperator)this.GetValue(OperatorProperty); }
            set { this.SetValue(OperatorProperty, value); }
        }

        public static readonly DependencyProperty OperatorProperty = DependencyProperty.Register("Operator", typeof(FilterOperator), typeof(FilterOperatorControl),
            new FrameworkPropertyMetadata(FilterOperator.Undefined, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((FilterOperatorControl)sender).Range_Changed()));

        public IContentFilter? Filter
        {
            get => (IContentFilter?)GetValue(FilterProperty);
            set => SetValue(FilterProperty, value);
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IContentFilter), typeof(FilterOperatorControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((FilterOperatorControl)sender).Filter_Changed()));

        #endregion

        #region Constructors

        public FilterOperatorControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void Range_Changed()
        {
            Filter = (Operator != FilterOperator.Undefined && Value.HasValue) ? new ContentFilter(Operator, Value.Value) : null;
        }

        private void Filter_Changed()
        {
            if (!(Filter is ContentFilter filter))
                return;

            Operator = filter.Operator;
            Value = filter.Value;
        }

        #endregion

        #region Class (Filter)

        public class ContentFilter : IContentFilter
        {
            #region Fields

            private readonly FilterOperator _operator;

            private readonly double _value;

            #endregion

            #region Properties

            public FilterOperator Operator => _operator;

            public double Value => _value;

            #endregion

            #region Constructors

            public ContentFilter()
            {
                _operator = FilterOperator.Undefined;
                _value = 0;
            }

            public ContentFilter(FilterOperator @operator, double value)
            {
                _operator = @operator;
                _value = value;
            }

            #endregion

            #region Methods

            public bool IsMatch(object? value)
            {
                if (value == null)
                    return false;

                if (!double.TryParse(value.ToString(), out var number))
                {
                    return false;
                }

                switch (_operator)
                {
                    case FilterOperator.Undefined:
                        return true;
                    case FilterOperator.LessThan:
                        return number < _value;
                    case FilterOperator.LessThanOrEqual:
                        return number <= _value;
                    case FilterOperator.GreaterThan:
                        return number > _value;
                    case FilterOperator.GreaterThanOrEqual:
                        return number >= _value;
                    case FilterOperator.Equals:
                        return number == _value;
                    case FilterOperator.Like:
                        return false;
                    case FilterOperator.Between:
                        return false;
                    default:
                        return false;
                }
            }

            #endregion
        }

        #endregion
    }
}
