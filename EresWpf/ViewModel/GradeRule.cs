using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using EresData;

namespace EresWpf.ViewModel
{
    class GradeRule : ValidationRule
    {
        private int _max;
        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        private Realisation _selReal;
        public Realisation SelReal
        {
            get { return _selReal; }
            set { _selReal = value; }
        }

        private Realisation _real;
        public Realisation Real
        {
            get { return _real; }
            set { _real = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Storage storage = new Storage();
            if (((String)value).Length > Max)
            {
                MainWindowViewModel.GradeNameValid = false;
                return new ValidationResult(false, "Nazwa moze miec maksymalnie " + Max + " znakow");
            }
            else if (SelReal == null && storage.isGradeName(Real.RealisationID, (String)value)) 
            {
                 MainWindowViewModel.GradeNameValid = false;
                 return new ValidationResult(false, "Nazwa oceny musi byc unikalna w obrebie przedmiotu.");
            }
            else
            {
                MainWindowViewModel.GradeNameValid = true;
                return new ValidationResult(true, null);
            }
        }
    }
}
