using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using Eres.Model;

namespace Eres.ViewModel
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
                    return new ValidationResult(false, "New Name can not be empty.");
                }
                if (Id == 7) //SemName
                {
                    MainWindowViewModel.SemNameValid = false;
                    return new ValidationResult(false, "New Name can not be empty.");
                }
                else
                    return new ValidationResult(true, null);
            }
            else if (((String)value).Length > Max)
            {
                if (Id == 1)
                {
                    MainWindowViewModel.NameValid = false;
                    return new ValidationResult(false, "Name can be max " + Max + " letters long.");
                }
                else if (Id == 2)
                {
                    MainWindowViewModel.SurnameValid = false;
                    return new ValidationResult(false, "Name can be max " + Max + " letters long.");
                }
                else if (Id == 3)
                {
                    MainWindowViewModel.GNameValid = false;
                    return new ValidationResult(false, "Name can be max " + Max + " letters long.");
                }
                else if (Id == 4)
                {
                    MainWindowViewModel.SubNameValid = false;
                    return new ValidationResult(false, "Name can be max " + Max + " letters long.");
                }
                else if (Id == 5)
                {
                    MainWindowViewModel.ConspectValid = false;
                    return new ValidationResult(false, "Conspect can be max " + Max + " letters long.");
                }
                else if (Id == 6)
                {
                    MainWindowViewModel.UrlValid = false;
                    return new ValidationResult(false, "Url can be max " + Max + " letters long.");
                }
                else if (Id == 7)
                {
                    MainWindowViewModel.SemNameValid = false;
                    return new ValidationResult(false, "Name can be max " + Max + " letters long.");
                }
                else if (Id == 8)
                {
                    MainWindowViewModel.GradeNameValid = false;
                    return new ValidationResult(false, "Name can be max " + Max + " letters long.");
                }
                else if (Id == 9)
                {
                    MainWindowViewModel.MaxValueValid = false;
                    return new ValidationResult(false, "Grade max value can consist of max" + Max + " signs.");
                }
                else if (Id == 10)
                {
                    MainWindowViewModel.DateValid = false;
                    return new ValidationResult(false, "Date can consist of max" + Max + " signs.");
                }
                else if (Id == 11)
                {
                    MainWindowViewModel.ValueValid = false;
                    return new ValidationResult(false, "Grade value can cosist of max " + Max + " signs.");
                }
                return new ValidationResult(false, "Can be max " + Max + " letters long.");
            }
            else if (Id == 3) //GroupName
            {
                Storage storage = new Storage();
                if (storage.isGroupName((String)value))
                {
                    MainWindowViewModel.GNameValid = false;
                    return new ValidationResult(false, "Group name must be unique.");
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
                    return new ValidationResult(false, "Subject name must be unique.");
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
                    return new ValidationResult(false, "Semester name must be unique.");
                }
                if(!Regex.IsMatch((String)value, @"^\d\d(Z|L)$"))
                {
                    MainWindowViewModel.SemNameValid = false;
                    return new ValidationResult(false, "Semester name should have pattern \\d\\d(Z|L), like 12Z.");
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
                    return new ValidationResult(false, "Grade MaxValue can be number, list of numbers or N;Z expression.");
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
                    return new ValidationResult(false, "Wrong date format, try yyyy-mm-dd.");
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
