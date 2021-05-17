using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestDataGridExtensions
{
    public class MainViewModel : NotifyPropertyChangedBase
    {
        #region Fields

        private ObservableCollection<Car> _cars = new ObservableCollection<Car>();

        public ListCollectionView _carsCollectionView;

        #endregion

        #region Properties

        public ListCollectionView CarsCollectionView
        {
            get { return _carsCollectionView; }
            set { _carsCollectionView = value; }
        }

        //public Localization Localization
        //{
        //    get { return _localization; }
        //    set
        //    {
        //        _localization = value;

        //        foreach (var originalCategory in OriginalCategories)
        //        {
        //            originalCategory.MyPartsCollectionView.GroupDescriptions.Clear();

        //            switch (_localization)
        //            {
        //                case Localization.None:
        //                    break;
        //                case Localization.Worksets:
        //                    originalCategory.MyPartsCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(MyPart.WorksetFilter)));
        //                    break;
        //                case Localization.Parameter:
        //                    UpdateParameters();
        //                    originalCategory.MyPartsCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(MyPart.ParameterFilter)));
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }

        //        RefreshMyPartsCollectionView();

        //        OnPropertyChanged(nameof(Localization));
        //    }
        //}

        #endregion

        public MainViewModel()
        {
            _cars.Add(new Car("Peugeot", 2.50));
            _cars.Add(new Car("Renault", 2.50));
            _cars.Add(new Car("BMW", 4.75));
            _cars.Add(new Car("Peugeot", 3.20));

            CarsCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(_cars);
        }
    }
}
