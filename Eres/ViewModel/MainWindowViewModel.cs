using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Eres.Model;
using EresData;

namespace Eres.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private Storage storage;
        private ICommand _studentsCommand;
        private ICommand _groupsCommand;
        private ICommand _subjectsCommand;
        private ICommand _semestersCommand;
        private ICommand _realisationsCommand;
        private ICommand _registrationsCommand;
        private ICommand _gradesCommand;
        private ICommand _gVCommand;

        public IList<Student> Students { get; set; }
        public Student _selectedStudent;
        public Group _selectedGroup;
        public String NewName { get; set; }
        public String NewSurname { get; set; }
        public String NewIndexNo { get; set; }
        public static Boolean NameValid { get; set; }
        public static Boolean SurnameValid { get; set; }
        public static Boolean IndexValid { get; set; }

        public IList<Group> Groups { get; set; }
        public Group _selectedGGroup;
        public String NewGName { get; set; }
        public static Boolean GNameValid { get; set; }

        public IList<Subject> Subjects { get; set; }
        public Subject _selectedSubject;
        public String NewConspect { get; set; }
        public String NewUrl { get; set; }
        public String NewSubName { get; set; }
        public static Boolean SubNameValid { get; set; }
        public static Boolean UrlValid { get; set; }
        public static Boolean ConspectValid { get; set; }

        public IList<Semester> Semesters { get; set; }
        public Semester _selectedSemester;
        public static string NewSemName { get; set; }
        public static Boolean SemNameValid { get; set; }

        public IList<Realisation> Realisations { get; set; }
        public Realisation SelectedRealisation { get; set; }
        public Semester _selRealSem;
        public Subject SelRealSub { get; set; }

        public IList<Registration> Registrations { get; set; }
        public Registration SelectedRegistration { get; set; }
        public Realisation _selRegReal;
        public Student SelRegStud { get; set; }

        public IList<Grade> Grades { get; set; }
        public Grade SelectedGrade { get; set; }
        public Realisation _selGReal;
        public String NewMaxValue { get; set; }
        public String NewGradeName { get; set; }
        public static Boolean GradeNameValid { get; set; }
        public static Boolean MaxValueValid { get; set; }

        public IList<GradeValue> GradeValues { get; set; }
        public GradeValue SelGV { get; set; }
        public Realisation _selGVReal;
        public Registration _selGVReg;
        public string NewDate { get; set; }
        public string NewValue { get; set; }
        public static Boolean DateValid { get; set; }
        public static Boolean ValueValid { get; set; }
        public Grade SelGVGrade { get; set; }
        public IList<Grade> GVGrades { get; set; }
        //public GradeValue _selGV { get; set; }
        //public static string MV { get; set; }

        public MainWindowViewModel()
        {
            storage = new Storage();
            Groups = storage.getGroups();
            Students = storage.getStudents();
            Subjects = storage.getSubjects();
            Semesters = storage.getSemesters();
            Realisations = storage.getRealisations();
            Registrations = storage.getRegistrations();
            Grades = storage.getGrades();
            GradeValues = null;
            GVGrades = null;

            IndexValid = NameValid = SurnameValid = true;
            GNameValid = true;
            SubNameValid = UrlValid = ConspectValid = true;
            SemNameValid = true;
            GradeNameValid = MaxValueValid = true;
            DateValid = ValueValid = true;
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value == _selectedIndex)
                    return;
                _selectedIndex = value;
                if (_selectedIndex == 0)
                    UpdateStudentsList();
                else if (_selectedIndex == 1)
                    UpdateGroupsList();
                else if (_selectedIndex == 2)
                    UpdateSubjectsList();
                else if (_selectedIndex == 3)
                    UpdateSemestersList();
                else if (_selectedIndex == 4)
                    UpdateRealisationsList();
                else if (_selectedIndex == 5)
                    UpdateRegistrationsList();
                else if (_selectedIndex == 6)
                    UpdateGradesList();
                else if (_selectedIndex == 7)
                    UpdateGVList();
            }
        }
   #region Updates
        void UpdateStudentsList()
        {
            if (_selectedGroup == null)
                Students = storage.getStudents();
            else
                Students = storage.getGroupStudents(_selectedGroup.GroupID);
            base.OnPropertyChanged("Students");
        }
        
        private void UpdateGroupsList()
        {
            Groups = storage.getGroups();
            OnPropertyChanged("Groups");
        }

        private void UpdateSubjectsList()
        {
            Subjects = storage.getSubjects();
            OnPropertyChanged("Subjects");
        }

        private void UpdateSemestersList()
        {
            Semesters = storage.getSemesters();
            OnPropertyChanged("Semesters");
        }

        private void UpdateRealisationsList()
        {
            if (_selRealSem == null)
                Realisations = storage.getRealisations();
            else
                Realisations = storage.getSemRealisations(_selRealSem.SemesterID);
            base.OnPropertyChanged("Realisations");
        }

        private void UpdateRegistrationsList()
        {
            SelRealSem = null;
            SelectedGroup = null;
            SelGReal = null;
            if (_selRegReal == null)
                Registrations = storage.getRegistrations();
            else
                Registrations = storage.getRealRegistrations(_selRegReal.RealisationID);
            base.OnPropertyChanged("Registrations");
        }

        private void UpdateGradesList()
        {
            SelRealSem = null;
            SelectedGroup = null;
            SelGVReal = null;
            SelGVReg = null;
            if (_selGReal == null)
                Grades = storage.getGrades();
            else
                Grades = storage.getRealGrades(_selGReal.RealisationID);
            base.OnPropertyChanged("Grades");
        }

        private void UpdateGVList()
        {
            SelRealSem = null;
            if (_selGVReg == null)
            {
                GradeValues = null;
            }
            else
            {
                GradeValues = storage.getRegGradeValues(_selGVReg.RegistrationID);
            }
            base.OnPropertyChanged("GradeValues");
            if (_selGVReal == null)
            {
                Registrations = storage.getRegistrations();
                GVGrades = null;
            }
            else
            {
                Registrations = storage.getRealRegistrations(_selGVReal.RealisationID);
                GVGrades = storage.getRealGrades(_selGVReal.RealisationID);
            }
            //base.OnPropertyChanged("Registrations");
            base.OnPropertyChanged("GVGrades");
            DateTime now = DateTime.Now;
            NewDate = now.ToShortDateString();
            OnPropertyChanged("NewDate");
        }
   #endregion
   #region Students
        public Group SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (value == _selectedGroup)
                    return;
                _selectedGroup = value;
                base.OnPropertyChanged("SelectedGroup");
                if (_selectedGroup == null)
                {
                    Students = storage.getStudents();
                }
                else
                    Students = storage.getGroupStudents(_selectedGroup.GroupID);
                base.OnPropertyChanged("Students");
            }
        }
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                if (value == _selectedStudent)
                    return;
                _selectedStudent = value;
            }
        }

       public ICommand StudentsCommand
        {
            get
            {
                if (_studentsCommand == null)
                {
                    _studentsCommand = new RelayCommand(param => this.StudProcess((string)param),
                        param => this.CanExecuteStudentsCommand((string)param));
                }
                return _studentsCommand;
            }
        }

        private bool CanExecuteStudentsCommand(string cmd)
        {           
            if (cmd.Equals("New"))
            {
                if (!NameValid || !SurnameValid || SelectedGroup == null
                    || string.IsNullOrEmpty(NewName) || string.IsNullOrEmpty(NewSurname)
                    || string.IsNullOrEmpty(NewIndexNo) || storage.isStudentId(NewIndexNo))
                {
                    return false;
                }
                else
                    return true;
            }
            else if (cmd.Equals("Save"))
            {
                if (SelectedStudent == null || !NameValid || !SurnameValid || !IndexValid)
                {
                    return false;
                }
                else
                    return true;
            }
            else if (cmd.Equals("Delete"))
            {
                if (SelectedStudent == null)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        public void StudProcess(string cmd)
        {
            if (cmd.Equals("New"))
            {
                try
                {
                    storage.createStudent(NewName, NewSurname, NewIndexNo, _selectedGroup.GroupID);
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to add student.", "DataBase Error");
                    return;
                }
                UpdateStudentsList();
            }
            else if (cmd.Equals("Save"))
            {
                try
                {
                    storage.updateStudent(_selectedStudent.StudentID, NewIndexNo, NewName, NewSurname, SelectedStudent.Group.GroupID, _selectedStudent.TimeStamp);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    MessageBox.Show("Try again to update student.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to update student.", "DataBase Error");
                    return;
                }
                UpdateStudentsList();
            }
            else if (cmd.Equals("Delete"))
            {
                try
                {
                    storage.deleteStudent(SelectedStudent);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    MessageBox.Show("Try again to delete student.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to delete student.", "DataBase Error");
                    return;
                }
                UpdateStudentsList();
            }
            else
                return;
        }
   #endregion
   #region Groups
        public Group SelectedGGroup
        {
            get { return _selectedGGroup; }
            set
            {
                if (value == _selectedGGroup)
                    return;
                _selectedGGroup = value;
            }
        }

        public ICommand GroupsCommand
        {
            get
            {
                if (_groupsCommand == null)
                {
                    _groupsCommand = new RelayCommand(param => this.GroupProcess((string)param),
                        param => this.CanExecuteGroupsCommand((string)param));
                }
                return _groupsCommand;
            }
        }

        private void GroupProcess(string cmd)
        {
            if (cmd.Equals("New"))
            {
                try
                {
                    storage.createGroup(NewGName);
                    UpdateGroupsList();
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to add group.", "DataBase Error");
                    return;
                }
            }
            else if (cmd.Equals("Edit"))
            {
                try
                {
                    storage.updateGroup(_selectedGGroup.GroupID, NewGName, _selectedGGroup.TimeStamp);
                    UpdateGroupsList();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    MessageBox.Show("Try again to update group.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to update group.", "DataBase Error");
                    return;
                }
            }
            else if (cmd.Equals("Delete"))
            {
                try
                {
                    storage.deleteGroup(SelectedGGroup);
                    UpdateGroupsList();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    MessageBox.Show("Try again to delete group.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to delete group.", "DataBase Error");
                    return;
                }
            }
            else
                return;
        }

        private bool CanExecuteGroupsCommand(string cmd)
        {
            if (cmd.Equals("New"))
            {
                if(!GNameValid || string.IsNullOrEmpty(NewGName))
                    return false;
                else
                    return true;
            }
            else if (cmd.Equals("Edit"))
            {
                if(SelectedGGroup == null || !GNameValid || string.IsNullOrEmpty(NewGName))
                    return false;
                else
                    return true;
            }
            else if (cmd.Equals("Delete"))
            {
                if(SelectedGGroup == null)
                    return false;
                else
                    return true;
            }
            else
                return false;
            
        }
    #endregion
   #region Subjects
        public Subject SelectedSubject
        {
            get { return _selectedSubject; }
            set
            {
                if (value == _selectedSubject)
                    return;
                _selectedSubject = value;
            }
        }

        public ICommand SubjectsCommand
        {
            get
            {
                if (_subjectsCommand == null)
                {
                    _subjectsCommand = new RelayCommand(param => this.SubjectProcess((string)param),
                        param => this.CanExecuteSubjectsCommand((string)param));
                }
                return _subjectsCommand;
            }
        }

        private void SubjectProcess(string cmd)
        {
            if (cmd.Equals("New"))
            {
                try
                {
                    storage.createSubject(NewSubName, NewConspect, NewUrl);
                    UpdateSubjectsList();
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to add subject.", "DataBase Error");
                    return;
                }
            }
            else if (cmd.Equals("Edit"))
            {
                try
                {
                    storage.updateSubject(SelectedSubject.SubjectID, NewSubName, NewConspect, NewUrl, SelectedSubject.TimeStamp);
                    UpdateSubjectsList();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    MessageBox.Show("Try again to update subject.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to update subject.", "DataBase Error");
                    return;
                }
            }
            else if (cmd.Equals("Delete"))
            {
                try
                {
                    storage.deleteSubject(SelectedSubject);
                    UpdateSubjectsList();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    MessageBox.Show("Try again to delete group.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to delete group.", "DataBase Error");
                    return;
                }
            }
            else
                return;
        }

        private bool CanExecuteSubjectsCommand(string cmd)
        {
            if (cmd.Equals("New"))
            {
                if (!SubNameValid || !ConspectValid || !UrlValid ||
                    string.IsNullOrEmpty(NewSubName) || string.IsNullOrEmpty(NewConspect))
                    return false;
                else
                    return true;
            }
            else if (cmd.Equals("Edit"))
            {
                if (SelectedSubject == null || !SubNameValid || !ConspectValid || !UrlValid ||
                (string.IsNullOrEmpty(NewSubName) && string.IsNullOrEmpty(NewConspect) && string.IsNullOrEmpty(NewUrl)))
                    return false;
                else
                    return true;
            }
            else if (cmd.Equals("Delete"))
            {
                if (SelectedSubject == null)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
   #endregion
   #region Semesters
        public Semester SelectedSemester
        {
            get { return _selectedSemester; }
            set
            {
                if (value == _selectedSemester)
                    return;
                _selectedSemester = value;
            }
        }

        public ICommand SemestersCommand
        {
            get
            {
                if (_semestersCommand == null)
                {
                    _semestersCommand = new RelayCommand(param => this.SemesterProcess((string)param),
                        param => this.CanExecuteSemestersCommand((string)param));
                }
                return _semestersCommand;
            }
        }

        private void SemesterProcess(string cmd)
        {
            if (cmd.Equals("New"))
            {
                try
                {
                    storage.createSemester(NewSemName);
                    UpdateSemestersList();
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to add semester.", "DataBase Error");
                    return;
                }
            }
            else if (cmd.Equals("Edit"))
            {
                try
                {
                    storage.updateSemester(_selectedSemester.SemesterID, NewSemName, _selectedSemester.TimeStamp);
                    UpdateSemestersList();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    MessageBox.Show("Try again to update semester.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to update semester.", "DataBase Error");
                    return;
                }
            }
            else if (cmd.Equals("Delete"))
            {
                try
                {
                    storage.deleteSemester(SelectedSemester);
                    UpdateSemestersList();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    MessageBox.Show("Try again to delete semester.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to delete semester.", "DataBase Error");
                    return;
                }
            }
            else
                return;
        }

        private bool CanExecuteSemestersCommand(string cmd)
        {
            if (cmd.Equals("New"))
            {
                if (!SemNameValid || string.IsNullOrEmpty(NewSemName))
                    return false;
                else
                    return true;
            }
            else if (cmd.Equals("Edit"))
            {
                if (SelectedSemester == null || !SemNameValid || string.IsNullOrEmpty(NewSemName))
                    return false;
                else
                    return true;
            }
            else if (cmd.Equals("Delete"))
            {
                if (SelectedSemester == null)
                    return false;
                else
                    return true;
            }
            else
                return false;

        }
   #endregion
   #region Realisations
        public Semester SelRealSem
        {
            get { return _selRealSem; }
            set
            {
                if (value == _selRealSem)
                    return;
                _selRealSem= value;
                base.OnPropertyChanged("SelRealSem");
                if (_selRealSem == null)
                {
                    Realisations = storage.getRealisations();
                }
                else
                    Realisations = storage.getSemRealisations(_selRealSem.SemesterID);
                base.OnPropertyChanged("Realisations");
            }
        }

        public ICommand RealisationsCommand
        {
            get
            {
                if (_realisationsCommand == null)
                {
                    _realisationsCommand = new RelayCommand(param => this.RealProcess((string)param),
                        param => this.CanExecuteRealisationsCommand((string)param));
                }
                return _realisationsCommand;
            }
        }

        private bool CanExecuteRealisationsCommand(string cmd)
        {
            if (cmd.Equals("New"))
            {
                if (SelRealSem == null || SelRealSub == null)
                    return false;
                else if (storage.isSubInSem(SelRealSem.SemesterID, SelRealSub.SubjectID))
                    return false;
                else
                    return true;
            }
            else if (cmd.Equals("Edit"))
            {
                if (SelectedRealisation == null)
                {
                    return false;
                }
                else
                    return true;
            }
            else if (cmd.Equals("Delete"))
            {
                if (SelectedRealisation == null)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        public void RealProcess(string cmd)
        {
            if (cmd.Equals("New"))
            {
                try
                {
                    storage.createRealisation(SelRealSem.SemesterID, SelRealSub.SubjectID);
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to add realisation.", "DataBase Error");
                    return;
                }
                UpdateRealisationsList();
            }
            else if (cmd.Equals("Edit"))
            {
                try
                {
                    storage.updateRealisation(SelectedRealisation.RealisationID, SelectedRealisation.Semester.SemesterID, SelectedRealisation.Subject.SubjectID, SelectedRealisation.TimeStamp);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    MessageBox.Show("Try again to update realisation.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to update realisation.", "DataBase Error");
                    return;
                }
                UpdateRealisationsList();
            }
            else if (cmd.Equals("Delete"))
            {
                try
                {
                    storage.deleteRealisation(SelectedRealisation);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    MessageBox.Show("Try again to delete realisation.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to delete realisation.", "DataBase Error");
                    return;
                }
                UpdateRealisationsList();
            }
            else
                return;
        }
   #endregion
   #region Registrations
        public Realisation SelRegReal
        {
            get { return _selRegReal; }
            set
            {
                if (value == _selRegReal)
                    return;
                _selRegReal = value;
                base.OnPropertyChanged("SelRegReal");
                if (_selRegReal == null)
                    Registrations = storage.getRegistrations();
                else
                    Registrations = storage.getRealRegistrations(_selRegReal.RealisationID);
                base.OnPropertyChanged("Registrations");
            }
        }

        public ICommand RegistrationsCommand
        {
            get
            {
                if (_registrationsCommand == null)
                {
                    _registrationsCommand = new RelayCommand(param => this.RegProcess((string)param),
                        param => this.CanExecuteRegistrationsCommand((string)param));
                }
                return _registrationsCommand;
            }
        }

        private bool CanExecuteRegistrationsCommand(string cmd)
        {
            if (cmd.Equals("New"))
            {
                if (SelRegReal == null || SelRegStud == null)
                    return false;
                else if (!storage.canRegister(SelRegReal.RealisationID, SelRegStud.StudentID))
                    return false;
                else
                    return true;
            }
            else if (cmd.Equals("Edit"))
            {
                if (SelectedRegistration == null)
                    return false;
                else
                    return true;
            }
            else if (cmd.Equals("Delete"))
            {
                if (SelectedRegistration == null)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        public void RegProcess(string cmd)
        {
            if (cmd.Equals("New"))
            {
                try
                {
                    storage.createRegistration(SelRegReal.RealisationID, SelRegStud.StudentID);
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to add registration.", "DataBase Error");
                    return;
                }
                UpdateRegistrationsList();
            }
            else if (cmd.Equals("Edit"))
            {
                try
                {
                    storage.updateRegistration(SelectedRegistration.RegistrationID, SelectedRegistration.Realisation.RealisationID, SelectedRegistration.Student.StudentID, SelectedRegistration.TimeStamp);
                }
                catch (DbUpdateConcurrencyException)
                {
                    MessageBox.Show("Try again to update registration.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to update registration.", "DataBase Error");
                    return;
                }
                UpdateRegistrationsList();
            }
            else if (cmd.Equals("Delete"))
            {
                try
                {
                    storage.deleteRegistration(SelectedRegistration);
                }
                catch (DbUpdateConcurrencyException)
                {
                    MessageBox.Show("Try again to delete registration.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to delete registration.", "DataBase Error");
                    return;
                }
                UpdateRegistrationsList();
            }
            else
                return;
        }
   #endregion
   #region Grades
        public Realisation SelGReal
        {
            get { return _selGReal; }
            set
            {
                if (value == _selGReal)
                    return;
                _selGReal = value;
                base.OnPropertyChanged("SelGReal");
                if (_selGReal == null)
                    Grades = storage.getGrades();
                else
                    Grades = storage.getRealGrades(_selGReal.RealisationID);
                base.OnPropertyChanged("Grades");
            }
        }

        public ICommand GradesCommand
        {
            get
            {
                if (_gradesCommand == null)
                {
                    _gradesCommand = new RelayCommand(param => this.GradeProcess((string)param),
                        param => this.CanExecuteGradesCommand((string)param));
                }
                return _gradesCommand;
            }
        }

        private bool CanExecuteGradesCommand(string cmd)
        {
            if (cmd.Equals("New"))
            {
                if (SelGReal==null || !GradeNameValid || !MaxValueValid ||
                        string.IsNullOrEmpty(NewGradeName) || string.IsNullOrEmpty(NewMaxValue))
                    return false;
                else if (storage.isGradeName(SelGReal.RealisationID, NewGradeName))
                    return false;
                else
                    return true;
            }
            else if (cmd.Equals("Edit"))
            {
                if (SelectedGrade == null || !GradeNameValid || !MaxValueValid)
                    return false;
                else
                    return true;
            }
            else if (cmd.Equals("Delete"))
            {
                if (SelectedGrade == null)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        public void GradeProcess(string cmd)
        {
            if (cmd.Equals("New"))
            {
                try
                {
                    storage.createGrade(SelGReal.RealisationID, NewGradeName, NewMaxValue);
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to add grade.", "DataBase Error");
                    return;
                }
                UpdateGradesList();
            }
            else if (cmd.Equals("Edit"))
            {
                try
                {
                    storage.updateGrade(SelectedGrade.GradeID, NewGradeName, NewMaxValue, SelectedGrade.Realisation.RealisationID, SelectedGrade.TimeStamp);
                }
                catch (DbUpdateConcurrencyException)
                {
                    MessageBox.Show("Try again to update grade.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to update grade.", "DataBase Error");
                    return;
                }
                UpdateGradesList();
            }
            else if (cmd.Equals("Delete"))
            {
                try
                {
                    storage.deleteGrade(SelectedGrade);
                }
                catch (DbUpdateConcurrencyException)
                {
                    MessageBox.Show("Try again to delete grade.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to delete grade.", "DataBase Error");
                    return;
                }
                UpdateGradesList();
            }
            else
                return;
        }
   #endregion
   #region GradeValues
        public Realisation SelGVReal
        {
            get { return _selGVReal; }
            set
            {
                if (value == _selGVReal)
                    return;
                _selGVReal = value;
                base.OnPropertyChanged("SelGVReal");
                if (_selGVReal == null)
                {
                    Registrations = storage.getRegistrations();
                    GVGrades = null;
                }
                else
                {
                    Registrations = storage.getRealRegistrations(_selGVReal.RealisationID);
                    GVGrades = storage.getRealGrades(_selGVReal.RealisationID);
                }
                base.OnPropertyChanged("Registrations");
                base.OnPropertyChanged("GVGrades");
            }
        }

        public Registration SelGVReg
        {
            get { return _selGVReg; }
            set
            {
                if (value == _selGVReg)
                    return;
                _selGVReg = value;
                base.OnPropertyChanged("SelGVReg");
                if (_selGVReg == null)
                    GradeValues = null;
                else
                    GradeValues = storage.getRegGradeValues(_selGVReg.RegistrationID);
                base.OnPropertyChanged("GradeValues");
            }
        }

/*        public GradeValue SelGV
        {
            get { return _selGV; }
            set
            {
                if (value == _selGV)
                    return;
                _selGV = value;
                base.OnPropertyChanged("SelGV");
                if (_selGV == null)
                    MV = null;
                else
                    MV = _selGV.Grade.MaxValue;
            }
        }*/

        public ICommand GVCommand
        {
            get
            {
                if (_gVCommand == null)
                {
                    _gVCommand = new RelayCommand(param => this.GVProcess((string)param),
                        param => this.CanExecuteGVCommand((string)param));
                }
                return _gVCommand;
            }
        }

        private bool CanExecuteGVCommand(string cmd)
        {
            if (cmd.Equals("New"))
            {
                if (SelGVReg == null || !ValueValid || !DateValid || SelGVGrade ==null ||
                        string.IsNullOrEmpty(NewDate) || string.IsNullOrEmpty(NewValue))
                    return false;
                else if (storage.isGrade(SelGVGrade.GradeID, SelGVReg.RegistrationID) || !ValueValidation(SelGVGrade.MaxValue))
                    return false;
                else
                    return true;
            }
            else if (cmd.Equals("Edit"))
            {
                if (SelGV==null || !ValueValid || !DateValid)
                    return false;
                else if (!ValueValidation(SelGV.Grade.MaxValue)) /*|| storage.isGrade(SelGV.Grade.GradeID, SelGV.Registration.RegistrationID))*/
                {
                    UpdateGVList();
                    return false;
                }
                else
                    return true;
            }
            else if (cmd.Equals("Delete"))
            {
                if (SelGV == null)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        private bool ValueValidation(string Max)
        {
                string str;
                if (NewValue != null)
                {
                    NewValue = NewValue.ToUpper();
                    str = NewValue;
                }
                else //przy edytowaniu, jeśli nie zmieniane pole NewValue
                {
                    str = SelGV.Value;
                }
                int m;
                if (int.TryParse(Max, out m)) //MaxValue to pojedyncza liczba
                {
                    int n;
                    if (int.TryParse(str, out n))
                    {
                        if (n <= m)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                {
                    str = str.Replace(" ", String.Empty);
                    string temp = Max.Replace(" ", String.Empty);
                    if (temp.IndexOf(str) != -1 && str.IndexOf(",") == -1 && str.IndexOf(";") == -1 && !str.Equals("-"))
                        return true;
                    else
                        return false;
                }
        }

        public void GVProcess(string cmd)
        {
            if (cmd.Equals("New"))
            {
                try
                {
                    storage.createGV(SelGVReg.RegistrationID, SelGVGrade.GradeID, NewValue, NewDate);
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to add grade value.", "DataBase Error");
                    return;
                }
                UpdateGVList();
            }
            else if (cmd.Equals("Edit"))
            {
                try
                {
                    storage.updateGV(SelGV.GradeValueID, NewValue, NewDate, SelGV.Registration.RegistrationID, SelGV.Grade.GradeID, SelGV.TimeStamp);
                }
                catch (DbUpdateConcurrencyException)
                {
                    MessageBox.Show("Try again to update grade value.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to update grade value.", "DataBase Error");
                    return;
                }
                UpdateGVList();
            }
            else if (cmd.Equals("Delete"))
            {
                try
                {
                    storage.deleteGV(SelGV);
                }
                catch (DbUpdateConcurrencyException)
                {
                    MessageBox.Show("Try again to delete grade value.", "Optimistic concurrency conflict");
                    return;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Unable to delete grade value.", "DataBase Error");
                    return;
                }
                UpdateGVList();
            }
            else
                return;
        }
   #endregion
    }
}
