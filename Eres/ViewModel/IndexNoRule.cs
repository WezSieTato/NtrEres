using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Eres.ViewModel
{
    class IndexNoRule : ValidationRule
    {
        private int _max;
        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number = 0;

            if (string.IsNullOrEmpty((string)value))
            {
                //MainWindowViewModel.IndexValid = false;
                //return new ValidationResult(false, "IndexNo can not be empty.");
                return new ValidationResult(true, null);
            }
            try
            {
                number = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                MainWindowViewModel.IndexValid = false;
                return new ValidationResult(false, "IndexNo should have a number format.");
            }
            if (((String)value).Length < 6)
            {
                MainWindowViewModel.IndexValid = false;
                return new ValidationResult(false, "IndexNo should have at least 6 digits.");
            }
            MainWindowViewModel.IndexValid = true;
            return new ValidationResult(true, null);
        }

    }
}
