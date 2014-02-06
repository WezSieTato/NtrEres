using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EresData;
using System.Collections.Generic;

namespace StorageTest
{
    [TestClass]
    public class UnitTest1
    {
        private Storage _storage;
        private Student _student;
        private Group _group;
        private Semester _semestr;

        [TestInitialize]
        public void testInit()
        {
            _storage = new Storage();
            _student = new Student();
            _group = new Group();
            _semestr = new Semester();

            _student.FirstName = "Testowy";
            _student.LastName = "Test";
            _student.IndexNo = "123456";

            _group.Name = "TstwGrp$#^134";

            _semestr.Name = "78Z";

        }

        [TestMethod]
        public void createGroup()
        {
            _storage.createGroup(_group.Name);
            Assert.IsNotNull(_storage.getGroups().Find(p => p.Name == _group.Name));
            Group testGroup = _storage.getGroups().Find(p => p.Name == _group.Name);
            _storage.deleteGroup(testGroup);
        }

        [TestMethod]
        public void updateGroup()
        {
            _storage.createGroup(_group.Name);
            Group testGroup = _storage.getGroups().Find(p => p.Name == _group.Name);
            _storage.updateGroup(testGroup.GroupID, "NowaNazwaTestowa", testGroup.TimeStamp);
            Assert.IsNotNull(_storage.getGroups().Find(p => p.GroupID == testGroup.GroupID && p.Name == "NowaNazwaTestowa"));
            _storage.deleteGroup(testGroup);
        }

        [TestMethod]
        public void deleteGroup()
        {
            _storage.createGroup(_group.Name);
            Group testGroup = _storage.getGroups().Find(p => p.Name == _group.Name);
            _storage.deleteGroup(testGroup);
            Assert.IsNull(_storage.getGroups().Find(p => p.GroupID == testGroup.GroupID));
        }




        [TestMethod]
        public void createStudent()
        {
            _storage.createGroup(_group.Name);
            Group testGroup = _storage.getGroups().Find(p => p.Name == _group.Name);
            _storage.createStudent(_student.FirstName, _student.LastName, _student.IndexNo, testGroup.GroupID);
            Student testStudent = _storage.getStudents().Find(p => p.FirstName == _student.FirstName
                && p.LastName == _student.LastName);
            Assert.IsNotNull(testStudent);


            _storage.deleteStudent(testStudent);
            _storage.deleteGroup(testGroup);
        }

        [TestMethod]
        public void createStudentException()
        {
            _storage.createGroup(_group.Name);
            Group testGroup = _storage.getGroups().Find(p => p.Name == _group.Name);
            _storage.createStudent(_student.FirstName, _student.LastName, _student.IndexNo, testGroup.GroupID);
            Student testStudent = _storage.getStudents().Find(p => p.FirstName == _student.FirstName
                && p.LastName == _student.LastName);

            bool exception = false;

            try
            {
                _storage.createStudent(_student.FirstName, _student.LastName, _student.IndexNo, testGroup.GroupID);
            } catch (EresDataContextException){
                exception = true;

            }
            Assert.IsTrue(exception, "Oczekiwany wyjatek nie zostal rzucony");

            _storage.deleteStudent(testStudent);
            _storage.deleteGroup(testGroup);
        }

        [TestMethod]
        public void getStudentFromGroup()
        {
            _storage.createGroup(_group.Name);
            Group testGroup = _storage.getGroups().Find(p => p.Name == _group.Name);
            _storage.createStudent(_student.FirstName, _student.LastName, _student.IndexNo, testGroup.GroupID);


            List<Student> studentList = _storage.getGroupStudents(testGroup.GroupID);
            Student testStudent = studentList.Find(p => p.FirstName == _student.FirstName
    && p.LastName == _student.LastName);
            Assert.IsNotNull((testStudent), "Lista z grupy nie zawiera studenta");
            Assert.IsTrue(studentList.Count == 1, "Lista powinna miec jeden element");

            _storage.deleteStudent(testStudent);
            _storage.deleteGroup(testGroup);
        }

        [TestMethod]
        public void deleteStudent()
        {
            _storage.createGroup(_group.Name);
            Group testGroup = _storage.getGroups().Find(p => p.Name == _group.Name);
            _storage.createStudent(_student.FirstName, _student.LastName, _student.IndexNo, testGroup.GroupID);
            Student testStudent = _storage.getStudents().Find(p => p.FirstName == _student.FirstName
                && p.LastName == _student.LastName);
            _storage.deleteStudent(testStudent);
            _storage.deleteGroup(testGroup);

            testStudent = _storage.getStudents().Find(p => p.FirstName == _student.FirstName && p.LastName == _student.LastName && p.IndexNo == _student.IndexNo);
            Assert.IsNull(testStudent);
        }

        [TestMethod]
        public void deleteGroupException()
        {
            _storage.createGroup(_group.Name);
            Group testGroup = _storage.getGroups().Find(p => p.Name == _group.Name);
            _storage.createStudent(_student.FirstName, _student.LastName, _student.IndexNo, testGroup.GroupID);
            Student testStudent = _storage.getStudents().Find(p => p.FirstName == _student.FirstName 
                && p.LastName == _student.LastName);
            String exceptionMessage = "";

            try
            {
                _storage.deleteGroup(testGroup);
            }
            catch (EresDataContextException e)
            {
                exceptionMessage = e.Message;
            }

            Assert.IsFalse(exceptionMessage.Length == 0, "Nie bylo wyjatku!");
            Assert.AreEqual("Nie mozna usunac grupy ze studentami", exceptionMessage, "Wyjatek inny niz przewidywany");

            _storage.deleteStudent(testStudent);
            _storage.deleteGroup(testGroup);

            testStudent = _storage.getStudents().Find(p => p.FirstName == _student.FirstName && p.LastName == _student.LastName && p.IndexNo == _student.IndexNo);
        }

        [TestMethod]
        public void createSubject()
        {
            _storage.createSubject("T3ST", "Kospekt testowy", "http://test.pl");
            Subject testSubject = _storage.getSubjects().Find(p => p.Name == "T3ST");
            Assert.IsNotNull(testSubject);
            _storage.deleteSubject(testSubject);
        }

    }
}
