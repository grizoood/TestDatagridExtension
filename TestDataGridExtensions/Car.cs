using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataGridExtensions
{
    public class Car : NotifyPropertyChangedBase
    {
        #region Fields

        private string _name;

        private double _length;

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public double Length
        {
            get { return _length; }
            set
            {
                _length = value;
                OnPropertyChanged(nameof(Length));
            }
        }

        #endregion

        #region Constructors
        public Car(string name, double length)
        {
            this.Name = name;
            this.Length = length;
        }

        #endregion
    }
}
