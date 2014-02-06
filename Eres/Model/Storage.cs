using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EresData;
using System.Data.Entity;

namespace Eres.Model
{
    public class Storage
    {
    #region Gets - tables to lists
        public List<Student> getStudents()
        {
            using (var db = new DataContext())
                return db.Students.Include("Group").ToList();
        }

        public List<Group> getGroups()
        {
            using (var db = new DataContext())
                return db.Groups.ToList();
        }

        public List<Semester> getSemesters()
        {
            using (var db = new DataContext())
                return db.Semesters.ToList();
        }

        public List<Subject> getSubjects()
        {
            using (var db = new DataContext())
                return db.Subjects.ToList();
        }

        public List<Realisation> getRealisations()
        {
            using (var db = new DataContext())
                return db.Realisations.Include("Semester").Include("Subject").ToList();
        }

        public List<Registration> getRegistrations()
        {
            using (var db = new DataContext())
                return db.Registrations.Include("Student").Include("Realisation").Include("Realisation.Subject").Include("Realisation.Semester").ToList();
        }

        public List<Grade> getGrades()
        {
            using (var db = new DataContext())
                return db.Grades.Include("Realisation").Include("Realisation.Subject").Include("Realisation.Semester").ToList();
        }

        public List<GradeValue> getGradeValues()
        {
            using (var db = new DataContext())
                return db.GradeValues.Include("Grade").Include("Registration").ToList();
        }

        public IList<GradeValue> getRegGradeValues(int regId)
        {
            using (var db = new DataContext())
                return db.GradeValues.Include("Grade").Include("Registration").Include("Registration.Student").Where(o => o.Registration.RegistrationID == regId).ToList();
        }
    #endregion
    #region Check if unique name
        public Boolean isGroupName(string n)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (db.Groups.Where(orig => orig.Name == n).FirstOrDefault() == null) 
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception e)
            { Console.WriteLine("isGroupName" + e.Message); return false; }
        }
        public Boolean isSemName(string n)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (db.Semesters.Where(orig => orig.Name == n).FirstOrDefault() == null)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception e)
            { Console.WriteLine("isSemName" + e.Message); return false; }
        }
        public Boolean isSubName(string n)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (db.Subjects.Where(orig => orig.Name == n).FirstOrDefault() == null)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception e)
            { Console.WriteLine("isSubName" + e.Message); return false; }
        }

        public bool isGradeName(int realId, string gname)
        {
           try
           {
                using (var db = new DataContext())
                {
                    if (db.Grades.Include("Realisation").Where(o => o.Realisation.RealisationID == realId).Where(o => o.Name == gname).FirstOrDefault() == null)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception e)
            { Console.WriteLine("isGradeName" + e.Message); return false; }
        }
    #endregion
    #region Students
        public void createStudent(string firstName, string lastName, string index, int groupId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var group = db.Groups.Find(groupId);
                    var student = new Student { FirstName = firstName, LastName = lastName, IndexNo = index, Group = group };
                    db.Students.Add(student);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException)
            { throw; }
        }

        public List<Student> getGroupStudents(int groupId)
        {
            using (var db = new DataContext())
            {
                var group = db.Groups.Find(groupId);
                return group.Students.ToList();
            }
        }

        public Boolean isStudentId(string str)
        {
            int id = Int32.Parse((String)str);
            try
            {
                using (var db = new DataContext())
                {
                    if (db.Students.Find(id) == null)
                        return false;
                    else
                        return true;
                }
            }
            catch (DbEntityValidationException)
            { throw; }
        }

        public void updateStudent(int id, string ind, string first, string last, int groupId, byte[] timestamp)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var original = db.Students.Include("Group").Where(orig => orig.StudentID == id).FirstOrDefault(); 
                    if (original != null)
                    {
                        if (!string.IsNullOrEmpty(first))
                            original.FirstName = first;
                        if (!string.IsNullOrEmpty(ind))
                            original.IndexNo = ind;
                        if (!string.IsNullOrEmpty(last))
                            original.LastName = last;
                        if (groupId!=original.Group.GroupID )
                        {
                            var group = db.Groups.Find(groupId);
                            original.Group = group;
                        }
                        original.TimeStamp = timestamp;
                        db.Students.Attach(original);
                        db.Entry(original).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void deleteStudent(Student st)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var original = db.Students.Find(st.StudentID);
                    if (original != null)
                    {
                        db.Students.Remove(original);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (DbEntityValidationException)
            { throw; }
        }
    #endregion
    #region Groups
        internal void createGroup(string NewGName)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var group = new Group { Name = NewGName};
                    db.Groups.Add(group);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException)
            { throw; }
        }

        public void deleteGroup(Group gr)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var original = db.Groups.Find(gr.GroupID);
                    if (original != null)
                    {
                        db.Groups.Remove(original);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("You can not delete group with students.", "Group Deletion Error");
                return;
            }
            catch (DbEntityValidationException)
            { throw; }
        }
        public void updateGroup(int id, string n, byte[] timestamp)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var original = db.Groups.Where(orig => orig.GroupID == id).FirstOrDefault();
                    if (original != null)
                    {
                        if (!original.Name.Equals(n))
                        {
                            original.Name = n;
                            original.TimeStamp = timestamp;
                            db.Groups.Attach(original);
                            db.Entry(original).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    #endregion
    #region Subjects
        public void createSubject(string newName, string newConspect, string newUrl)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var subject = new Subject { Name = newName, Conspect = newConspect, url = newUrl };
                    db.Subjects.Add(subject);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException)
            { throw; }
        }

        public void updateSubject(int id, string NewSubName, string NewConspect, string NewUrl, byte[] timestamp)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var original = db.Subjects.Where(orig => orig.SubjectID == id).FirstOrDefault();
                    if (original != null)
                    {
                        if (!string.IsNullOrEmpty(NewSubName))
                            original.Name = NewSubName;
                        if (!string.IsNullOrEmpty(NewConspect))
                            original.Conspect = NewConspect;
                        if (!string.IsNullOrEmpty(NewUrl))
                            original.url = NewUrl;
                        original.TimeStamp = timestamp;
                        db.Subjects.Attach(original);
                        db.Entry(original).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (DbEntityValidationException e)
            { throw; }
        }

        internal void deleteSubject(Subject sub)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var original = db.Subjects.Find(sub.SubjectID);
                    if (original != null)
                    {
                        db.Subjects.Remove(original);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("You can not delete subject which has realisation.", "Subject Deletion Error");
                return;
            }
            catch (DbEntityValidationException)
            { throw; }
        }
    #endregion
    #region Semesters
        public void createSemester(string NewSemName)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var sem = new Semester { Name = NewSemName };
                    db.Semesters.Add(sem);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException)
            { throw; }
        }

        public void updateSemester(int id, string n, byte[] timestamp)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var original = db.Semesters.Where(orig => orig.SemesterID == id).FirstOrDefault();
                    if (original != null)
                    {
                        if (!original.Name.Equals(n))
                        {
                            original.Name = n;
                            original.TimeStamp = timestamp;
                            db.Semesters.Attach(original);
                            db.Entry(original).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }

        public void deleteSemester(Semester sem)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var original = db.Semesters.Find(sem.SemesterID);
                    if (original != null)
                    {
                        db.Semesters.Remove(original);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("You can not delete semester which has realisation.", "Semester Deletion Error");
                return;
            }
            catch (DbEntityValidationException)
            { throw; }
        }
    #endregion
    #region Realisations
        public List<Realisation> getSemRealisations(int semId)
        {
            using (var db = new DataContext())
            {
                return db.Realisations.Include("Semester").Include("Subject").Where(o => o.Semester.SemesterID == semId).ToList();
            }
        }

        public Boolean isSubInSem(int semId, int subId)
        {
            try
            {
                using (var db = new DataContext())
                {            
                    var real = db.Realisations.Where(o=>o.Subject.SubjectID==subId).Where(o=>o.Semester.SemesterID==semId).FirstOrDefault();
                    if (real == null)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception e)
            { Console.WriteLine("isSubInSem1" + e.Message); return false; }
        }

        public Boolean isSubInSem(Realisation r)
        {
            try
            {
                using (var db = new DataContext())
                {
                    int id = r.RealisationID;
                    int subId = r.Subject.SubjectID;
                    int semId = r.Semester.SemesterID;
                    var orig = db.Realisations.Include("Subject").Include("Semester").Where(o => o.RealisationID == id).FirstOrDefault();
                    if (orig.Semester.SemesterID != semId || orig.Subject.SubjectID != subId) //jeśli coś się zmieniło
                    {
                        var real = db.Realisations.Where(o => o.Subject.SubjectID == subId).Where(o => o.Semester.SemesterID == semId).FirstOrDefault();
                        if (real == null)
                            return false;
                        else
                            return true;                        
                    }
                    else
                        return false;
                }
            }
            catch (Exception e)
            { Console.WriteLine("isSubInSem2" + e.Message); return false; }
        }

        public void createRealisation(int semId, int subId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var semester = db.Semesters.Find(semId);
                    var subject = db.Subjects.Find(subId);
                    var real = new Realisation { Subject = subject, Semester = semester };
                    db.Realisations.Add(real);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException)
            { throw; }
        }

        public void updateRealisation(int id, int semId, int subId, byte[] timestamp)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var orig = db.Realisations.Include("Subject").Include("Semester").Where(o => o.RealisationID == id).FirstOrDefault();
                    if (orig != null)
                    {
                        if (orig.Semester.SemesterID != semId || orig.Subject.SubjectID != subId) //jeśli coś się zmieniło
                        {
                            if (db.Realisations.Where(o => o.Subject.SubjectID == subId).Where(o => o.Semester.SemesterID == semId).FirstOrDefault() != null)
                            {
                                MessageBox.Show("There is already in this semester such subject.","Realisation Update Error");
                                return;
                            }
                            else
                            {
                                var sem = db.Semesters.Find(semId);
                                orig.Semester = sem;
                                var sub = db.Subjects.Find(subId);
                                orig.Subject = sub;
                                orig.TimeStamp = timestamp;
                                db.Realisations.Attach(orig);
                                db.Entry(orig).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }

        public void deleteRealisation(Realisation r)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var orig = db.Realisations.Find(r.RealisationID);
                    if (orig != null)
                    {
                        db.Realisations.Remove(orig);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("You can not delete used realisation.", "Realisation Deletion Error");
                return;
            }
            catch (DbEntityValidationException)
            { throw; }
        }
    #endregion
    #region Registrations
        public IList<Registration> getRealRegistrations(int realId)
        {
            using (var db = new DataContext())
            {
                return db.Registrations.Include("Student").Include("Realisation").Include("Realisation.Subject").Include("Realisation.Semester").Where(o => o.Realisation.RealisationID == realId).ToList();
            }
        }

        public bool canRegister(int realId, int studId)
        {
            using (var db = new DataContext())
            {
                if (db.Registrations.Where(o => o.Realisation.RealisationID == realId).Where(o => o.Student.StudentID == studId).FirstOrDefault() == null)
                    return true;
                else
                    return false;
            }
        }

        public void createRegistration(int realId, int studId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var real = db.Realisations.Find(realId);
                    var stud = db.Students.Find(studId);
                    var reg = new Registration { Realisation = real, Student = stud };
                    db.Registrations.Add(reg);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException)
            { throw; }
        }

        public void updateRegistration(int id, int realId, int studId, byte[] timestamp)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var orig = db.Registrations.Include("Realisation").Include("Student").Where(o => o.RegistrationID == id).FirstOrDefault();
                    if (orig != null)
                    {
                        if (orig.Realisation.RealisationID != realId || orig.Student.StudentID != studId)
                        {
                            if (db.Registrations.Where(o => o.Realisation.RealisationID == realId).Where(o => o.Student.StudentID == studId).FirstOrDefault() != null)
                            {
                                MessageBox.Show("Student is already registered to this realisation.", "Realisation Update Error");
                                return;
                            }
                            else
                            {
                                var real = db.Realisations.Find(realId);
                                orig.Realisation = real;
                                var stud = db.Students.Find(studId);
                                orig.Student = stud;
                                orig.TimeStamp = timestamp;
                                db.Registrations.Attach(orig);
                                db.Entry(orig).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }

        public void deleteRegistration(Registration r)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var orig = db.Registrations.Find(r.RegistrationID);
                    if (orig != null)
                    {
                        db.Registrations.Remove(orig);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("You can not delete used registartion.", "Registration Deletion Error");
                return;
            }
            catch (DbEntityValidationException)
            { throw; }
        }
    #endregion
    #region Grades
        public IList<Grade> getRealGrades(int realId)
        {
            using (var db = new DataContext())
            {
                return db.Grades.Include("Realisation").Include("Realisation.Subject").Include("Realisation.Semester").Where(o => o.Realisation.RealisationID == realId).ToList();
            }
        }

        public void createGrade(int realId, string gradeName, string maxValue)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var real = db.Realisations.Find(realId);
                    var grade = new Grade { Realisation = real, Name = gradeName, MaxValue = maxValue };
                    db.Grades.Add(grade);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException)
            { throw; }
        }

        public void updateGrade(int id, string gradeName, string maxValue, int realId, byte[] timestamp)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var original = db.Grades.Include("Realisation").Where(orig => orig.GradeID == id).FirstOrDefault();
                    if (original != null)
                    {
                        if (realId != original.Realisation.RealisationID)
                        {
                            var real = db.Realisations.Find(realId);
                            original.Realisation = real;
                        }
                        if (!string.IsNullOrEmpty(maxValue))
                        {
                            string Stud = string.Empty;
                            List<GradeValue> gv = db.GradeValues.Include("Grade").Include("Registration").Include("Registration.Student").Where(o => o.Grade.GradeID == id).ToList();
                            if (gv != null)
                            {
                                foreach(GradeValue v in gv)
                                {
                                    if (!Validation(maxValue, v.Value))
                                    {
                                        Stud = string.Concat(Stud, "\n", v.Registration.Student.LastName);
                                        Console.WriteLine(v.Registration.Student.LastName);
                                    }
                                }
                                if (!string.IsNullOrEmpty(Stud))
                                {
                                    MessageBox.Show("You have to change or delete these students marks:" + Stud, "Grade MaxValue Update Error");
                                    return;
                                }
                            }
                            original.MaxValue = maxValue.ToUpper();
                        }
                        if (!string.IsNullOrEmpty(gradeName))
                        {
                            if (db.Grades.Include("Realisation").Where(o => o.Realisation.RealisationID == realId).Where(o => o.Name == gradeName).FirstOrDefault() != null)
                            {
                                MessageBox.Show("Grade name must be unique in realisation.", "Grade Update Error");
                                return;
                            }
                            original.Name = gradeName;
                        }
                        original.TimeStamp = timestamp;
                        db.Grades.Attach(original);
                        db.Entry(original).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Grade Update Error", "Grade Update Error");
                return;
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }

        public void deleteGrade(Grade g)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var orig = db.Grades.Find(g.GradeID);
                    if (orig != null)
                    {
                        db.Grades.Remove(orig);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("You can not delete used grade.", "Grade Deletion Error");
                return;
            }
            catch (DbEntityValidationException)
            { throw; }
        }

        private bool Validation(string Max, string str)
        {
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
    #endregion
    #region GradeValues
        public bool isGrade(int gradeId, int regId)
        {
            using (var db = new DataContext())
            {
                if (db.GradeValues.Include("Grade").Include("Registration").Where(o => o.Registration.RegistrationID == regId).Where(o => o.Grade.GradeID == gradeId).FirstOrDefault() == null)
                {
                    //Console.WriteLine("isGrade = FALSE");
                    return false;
                }
                else
                {
                    //Console.WriteLine("isGrade = TRUE");
                    return true;
                }
            }
        }

        public void deleteGV(GradeValue gv)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var orig = db.GradeValues.Find(gv.GradeValueID);
                    if (orig != null)
                    {
                        db.GradeValues.Remove(orig);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("You can not delete grade value with references.", "Grade Deletion Error");
                return;
            }
            catch (DbEntityValidationException)
            { throw; }
        }

        public void createGV(int regId, int gradeId, string value, string date)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var reg = db.Registrations.Find(regId);
                    var grade = db.Grades.Find(gradeId);
                    var gv = new GradeValue { Registration = reg, Grade = grade, Value = value, Date = date };
                    db.GradeValues.Add(gv);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException)
            { throw; }
        }

        public void updateGV(int id, string value, string date, int regId, int gradeId, byte[] timestamp)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var original = db.GradeValues.Include("Registration").Include("Grade").Where(orig => orig.GradeValueID == id).FirstOrDefault();
                    if (original != null)
                    {
                        if (regId != original.Registration.RegistrationID || gradeId != original.Grade.GradeID)
                        {
                            if (isGrade(gradeId, regId))
                            {
                                MessageBox.Show("Student already have this grade value.", "GradeValue Update Error");
                                return;
                            }
                            var reg = db.Registrations.Find(regId);
                            original.Registration = reg;
                            var g = db.Grades.Find(gradeId);
                            original.Grade = g;
                        }
                        if (!string.IsNullOrEmpty(value))
                        {
                            original.Value = value.ToUpper();
                        }
                        if (!string.IsNullOrEmpty(date))
                        {
                            original.Date = date;
                        }
                        original.TimeStamp = timestamp;
                        db.GradeValues.Attach(original);
                        db.Entry(original).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("GradeValue Update Error", "GradeValue Update Error");
                return;
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }
    #endregion
    }
}
