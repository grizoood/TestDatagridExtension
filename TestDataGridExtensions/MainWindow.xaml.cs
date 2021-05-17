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
using TestDataGridExtensions.Exceptions;

namespace TestDataGridExtensions
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private bool _isClosed;

        #endregion

        #region Properties

        public MainViewModel ViewModel =>
           (MainViewModel)DataContext ?? throw new NullMemberException(nameof(DataContext));

        public bool IsClosed
        {
            get { return _isClosed; }
            private set { _isClosed = value; }
        }

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        #endregion

        #region Methods

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

        #endregion
    }
}
