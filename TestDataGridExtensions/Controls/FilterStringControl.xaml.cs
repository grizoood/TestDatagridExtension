using DataGridExtensions;
using DataGridExtensions.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using TestDataGridExtensions.Wpf;
using TomsToolbox.Essentials;

namespace TestDataGridExtensions.Controls
{
    /// <summary>
    /// Logique d'interaction pour FilterStringControl.xaml
    /// </summary>
    public partial class FilterStringControl
    {
        #region Fields

        private ListBox? _listBox;

        #endregion

        #region Properties

        public bool IsPopupVisible
        {
            get => (bool)GetValue(IsPopupVisibleProperty);
            set => SetValue(IsPopupVisibleProperty, value);
        }

        public static readonly DependencyProperty IsPopupVisibleProperty =
            DependencyProperty.Register("IsPopupVisible", typeof(bool), typeof(FilterStringControl),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((FilterStringControl)sender).IsPopupVisible_Changed()));

        public IList<string> Values
        {
            get => (IList<string>)GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }

        public static readonly DependencyProperty ValuesProperty = DependencyProperty.Register("Values", typeof(IEnumerable<string>), typeof(FilterStringControl),
            new FrameworkPropertyMetadata((sender, e) => ((FilterStringControl)sender).Values_Changed()));

        public IList<string> SourceValues
        {
            get => (IList<string>)GetValue(SourceValuesProperty);
            set => SetValue(SourceValuesProperty, value);
        }

        public static readonly DependencyProperty SourceValuesProperty = DependencyProperty.Register("SourceValues", typeof(IEnumerable<string>), typeof(FilterStringControl),
            new FrameworkPropertyMetadata((sender, e) => ((FilterStringControl)sender).SourceValues_Changed()));

        public IList<string> SelectableValues
        {
            get => (IList<string>)GetValue(SelectableValuesProperty);
            set => SetValue(SelectableValuesProperty, value);
        }

        public static readonly DependencyProperty SelectableValuesProperty = DependencyProperty.Register("SelectableValues", typeof(IEnumerable<string>), typeof(FilterStringControl),
            new FrameworkPropertyMetadata((sender, e) => ((FilterStringControl)sender).SelectableValues_Changed()));

        public object SelectAllContent
        {
            get => GetValue(SelectAllContentProperty);
            set => SetValue(SelectAllContentProperty, value);
        }

        public static readonly DependencyProperty SelectAllContentProperty = DependencyProperty.Register(
            "SelectAllContent", typeof(object), typeof(FilterStringControl), new PropertyMetadata("(Select All)"));

        public ContentFilter? Filter
        {
            get => (ContentFilter)GetValue(FilterProperty);
            set => SetValue(FilterProperty, value);
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(ContentFilter), typeof(FilterStringControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((FilterStringControl)sender).Filter_Changed()));

        #endregion

        #region Constructors

        public FilterStringControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var listBox = _listBox = Template?.FindName("listBox", this) as ListBox;
            if (listBox == null)
                return;

            DataGridFilterColumnControl filterColumnControl = this.FindAncestorOrSelf<DataGridFilterColumnControl>();

            BindingOperations.SetBinding(this, FilterProperty, new Binding { Source = filterColumnControl, Path = new PropertyPath(DataGridFilterColumnControl.FilterProperty) });
            BindingOperations.SetBinding(this, SourceValuesProperty, new Binding { Source = filterColumnControl, Path = new PropertyPath(nameof(DataGridFilterColumnControl.SourceValues)) });

            var filter = Filter;

            if (filter?.Items == null)
            {
                listBox.SelectAll();
            }

            listBox.SelectionChanged += ListBox_SelectionChanged;
            var items = (INotifyCollectionChanged)listBox.Items;

            items.CollectionChanged += ListBox_ItemsCollectionChanged;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;

            var selectedItems = listBox.SelectedItems.Cast<string>().ToArray();

            var areAllItemsSelected = listBox.Items.Count == selectedItems.Length;

            Filter = !areAllItemsSelected ? new ContentFilter(selectedItems) : null;
        }

        private void ListBox_ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var filter = Filter;

            if (filter?.Items == null)
            {
                _listBox?.SelectAll();
            }
        }

        private void IsPopupVisible_Changed()
        { }

        private void Values_Changed()
        { }

        private void SourceValues_Changed()
        { }

        private void SelectableValues_Changed()
        { }

        private void Filter_Changed()
        {
            var listBox = _listBox;
            if (listBox == null)
                return;

            var filter = Filter;
            if (filter?.Items == null)
            {
                listBox.SelectAll();
                return;
            }

            if (listBox.SelectedItems.Count != 0)
                return;

            foreach (var item in filter.Items)
            {
                listBox.SelectedItems.Add(item);
            }



            //var listBox = _listBox;
            //if (listBox == null)
            //    return;

            //var filter = Filter;
            //if (filter?.Items == null)
            //{
            //    listBox.SelectAll();
            //    return;
            //}

            ////if (listBox.SelectedItems.Count != 0)
            ////    return;

            ////foreach (var item in filter.Items)
            ////{
            ////    listBox.SelectedItems.Add(item);
            ////}






            //_listBox.SelectionChanged -= ListBox_SelectionChanged;
            //var items = (INotifyCollectionChanged)_listBox.Items;

            //items.CollectionChanged -= ListBox_ItemsCollectionChanged;

            //listBox.SelectedItems.Clear();
            //foreach (var item in filter.Items)
            //{
            //    listBox.SelectedItems.Add(item);
            //}

            //_listBox.SelectionChanged += ListBox_SelectionChanged;

            //items.CollectionChanged += ListBox_ItemsCollectionChanged;
        }

        protected virtual ContentFilter CreateFilter(IEnumerable<string?>? items)
        {
            return new ContentFilter(items);
        }

        #endregion

        #region Class (Filter)

        public class ContentFilter : IContentFilter
        {
            #region Properties

            public IList<string?>? Items
            {
                get;
            }

            #endregion

            #region Contructors

            public ContentFilter(IEnumerable<string?>? items)
            {
                Items = items?.ToArray();
            }

            #endregion

            #region Methods

            public bool IsMatch(object? value)
            {
                var input = value?.ToString();
                if (string.IsNullOrWhiteSpace(input))
                    return Items?.Contains(string.Empty) ?? true;

                return Items?.ContainsAny(input) ?? true;
            }

            #endregion
        }

        #endregion
    }
}
