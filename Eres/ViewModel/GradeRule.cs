using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Eres.Model;
using EresData;

namespace Eres.ViewModel
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
                return new ValidationResult(false, "Name can be max " + Max + " letters long.");
            }
            else if (SelReal == null && storage.isGradeName(Real.RealisationID, (String)value)) //pełna lista Grade, nie można dodawać, sprawdzanie tylko edytowania
            {
                 MainWindowViewModel.GradeNameValid = false;
                 return new ValidationResult(false, "Grade name must be unique in realisation.");
            }
            else
            {
                MainWindowViewModel.GradeNameValid = true;
                return new ValidationResult(true, null);
            }
        }
    }
}
