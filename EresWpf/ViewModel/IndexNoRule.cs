using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EresWpf.ViewModel
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
                return new ValidationResult(true, null);
            }
            try 
            {
                number = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                MainWindowViewModel.IndexValid = false;
                return new ValidationResult(false, "Index moze skladac sie wylacznie z cyfr");
            }
            if (((String)value).Length < 6)
            {
                MainWindowViewModel.IndexValid = false;
                return new ValidationResult(false, "Index musi miec conajmniej 6 znakow");
            }
            MainWindowViewModel.IndexValid = true;
            return new ValidationResult(true, null);
        }

    }
}
