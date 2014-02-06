using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using EresData;

namespace EresWpf.ViewModel
{
    class NameRule : ValidationRule
    {
        private int _max;
        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        // 1-FirstName, 2-LastName, 3-GroupName, 4-SubName, 5-Conspect, 6-Url, 7-SemName, 8-GradeName, 9-MaxValue, 10-GVDate

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (string.IsNullOrEmpty((string)value))
            {
                if (Id == 3) //Groups
                {
                    MainWindowViewModel.GNameValid = false;
                    return new ValidationResult(false, "Nazwa nie moze byc pusta.");
                }
                if (Id == 7) //SemName
                {
                    MainWindowViewModel.SemNameValid = false;
                    return new ValidationResult(false, "Nazwa nie moze byc pusta.");
                }
                else
                    return new ValidationResult(true, null);
            }
            else if (((String)value).Length > Max)
            {
                if (Id == 1)
                {
                    MainWindowViewModel.NameValid = false;
                    return new ValidationResult(false, "Nazwa moze miec maksymalnie " + Max + " znakow.");
                }
                else if (Id == 2)
                {
                    MainWindowViewModel.SurnameValid = false;
                    return new ValidationResult(false, "Nazwa moze miec maksymalnie " + Max + " znakow.");
                }
                else if (Id == 3)
                {
                    MainWindowViewModel.GNameValid = false;
                    return new ValidationResult(false, "Nazwa moze miec maksymalnie " + Max + " znakow.");
                }
                else if (Id == 4)
                {
                    MainWindowViewModel.SubNameValid = false;
                    return new ValidationResult(false, "Nazwa moze miec maksymalnie " + Max + " znakow.");
                }
                else if (Id == 5)
                {
                    MainWindowViewModel.ConspectValid = false;
                    return new ValidationResult(false, "Konspekt moze miec maksylanie " + Max + " znakow.");
                }
                else if (Id == 6)
                {
                    MainWindowViewModel.UrlValid = false;
                    return new ValidationResult(false, "Url moze miec maksymalnie " + Max + " znakow.");
                }
                else if (Id == 7)
                {
                    MainWindowViewModel.SemNameValid = false;
                    return new ValidationResult(false, "Nazwa moze miec maksymalnie " + Max + " znakow.");
                }
                else if (Id == 8)
                {
                    MainWindowViewModel.GradeNameValid = false;
                    return new ValidationResult(false, "Nazwa moze miec maksymalnie " + Max + " znakow.");
                }
                else if (Id == 9)
                {
                    MainWindowViewModel.MaxValueValid = false;
                    return new ValidationResult(false, "Maksymalnie wartosc moze miec maksymalnie " + Max + " wartosci.");
                }
                else if (Id == 10)
                {
                    MainWindowViewModel.DateValid = false;
                    return new ValidationResult(false, "Data sklada sie z " + Max + " cyfr.");
                }
                else if (Id == 11)
                {
                    MainWindowViewModel.ValueValid = false;
                    return new ValidationResult(false, "Maksymalna wartosc moze miec " + Max + " znakow.");
                }
                return new ValidationResult(false, "Maksymalnie " + Max + " znakow.");
            }
            else if (Id == 3) //GroupName
            {
                Storage storage = new Storage();
                if (storage.isGroupName((String)value))
                {
                    MainWindowViewModel.GNameValid = false;
                    return new ValidationResult(false, "Nazwa grupy musi byc unikalna.");
                }
                MainWindowViewModel.GNameValid = true;
                return new ValidationResult(true, null);
            }
            else if (Id == 4) //SubName
            {
                Storage storage = new Storage();
                if (storage.isSubName((String)value))
                {
                    MainWindowViewModel.SubNameValid = false;
                    return new ValidationResult(false, "Nazwa przedmiotu musi byc unikalna");
                }
                MainWindowViewModel.SubNameValid = true;
                return new ValidationResult(true, null);
            }
            else if (Id == 7) //SemName
            {
                Storage storage = new Storage();
                if (storage.isSemName((String)value))
                {
                    MainWindowViewModel.SemNameValid = false;
                    return new ValidationResult(false, "Nazwa semestru musi byc unikalna.");
                }
                if(!Regex.IsMatch((String)value, @"^\d\d(Z|L)$"))
                {
                    MainWindowViewModel.SemNameValid = false;
                    return new ValidationResult(false, "Nieprawidlowy format semestru, poprawny format liczba(Z lub L), np. 12Z.");
                }
                MainWindowViewModel.SemNameValid = true;
                return new ValidationResult(true, null);
            }
            else if (Id == 9) //GradeMaxValue
            {
                string str = (String)value;
                str = str.Replace(" ", String.Empty);
                str = str.ToUpper();
                if (!(Regex.IsMatch(str, @"^(\-?[0-9]+[;,])*\-?[0-9]+$") || Regex.IsMatch(str, @"^N[,;]?Z$") || Regex.IsMatch(str, @"^Z[,;]?N$")))
                {
                    MainWindowViewModel.MaxValueValid = false;
                    return new ValidationResult(false, "Maksymalna wartosc moze byc liczba, lub lista liczb");
                }
                MainWindowViewModel.MaxValueValid = true;
                return new ValidationResult(true, null);
            }
            else if (Id == 10) //Date
            {
                string str = (String)value;
                try
                {
                    DateTime.Parse(str);
                }
                catch (FormatException)
                {
                    MainWindowViewModel.DateValid = false;
                    return new ValidationResult(false, "Zly format, poprawny: yyyy-mm-dd, np 1991-01-31");
                }
                MainWindowViewModel.MaxValueValid = true;
                return new ValidationResult(true, null);
            }
            /*else if (Id == 11) //GradeValue
            {
                string str = ((string)value).ToUpper();
                int m;
                if (int.TryParse(MainWindowViewModel.MV, out m)) //MaxValue to pojedyncza liczba
                {
                    int n;
                    if (int.TryParse(str, out n))
                    {
                        if (n <= m)
                        {
                            MainWindowViewModel.ValueValid = true;
                            return new ValidationResult(true, null);
                        }
                        else
                        {
                            MainWindowViewModel.ValueValid = false;
                            return new ValidationResult(false, "Wrong grade value, check MaxValue.");
                        }
                    }
                    else
                    {
                        MainWindowViewModel.ValueValid = false;
                        return new ValidationResult(false, "Wrong grade value, check MaxValue.");
                    }
                }
                else
                {
                    str = str.Replace(" ", String.Empty);
                    string temp = MainWindowViewModel.MV.Replace(" ", String.Empty);
                    //string temp = MainWindowViewModel.MV.Replace(",", String.Empty);
                    //temp = temp.Replace(";", String.Empty);
                    if (temp.IndexOf(str) != -1 && str.IndexOf(",") == -1 && str.IndexOf(";") == -1 && !str.Equals("-"))
                    {
                        MainWindowViewModel.ValueValid = true;
                        return new ValidationResult(true, null);
                    }
                    else
                    {
                        MainWindowViewModel.ValueValid = false;
                        return new ValidationResult(false, "Wrong grade value, check MaxValue.");
                    }
                }
            }*/
            else
            {
                if (Id == 1)
                    MainWindowViewModel.NameValid = true;
                if (Id == 2)
                    MainWindowViewModel.SurnameValid = true;
                if (Id == 3)
                    MainWindowViewModel.GNameValid = true;
                if (Id == 4)
                    MainWindowViewModel.SubNameValid = true;
                if (Id == 5)
                    MainWindowViewModel.ConspectValid = true;
                if (Id == 6)
                    MainWindowViewModel.UrlValid = true;
                if (Id == 7)
                    MainWindowViewModel.SemNameValid = true;
                if (Id == 8)
                    MainWindowViewModel.GradeNameValid = true;
                if (Id == 9)
                    MainWindowViewModel.MaxValueValid = true;
                if (Id == 10)
                    MainWindowViewModel.DateValid = true;
                if (Id == 11)
                    MainWindowViewModel.ValueValid = true;
                return new ValidationResult(true, null);
            }
        }
    }
}
