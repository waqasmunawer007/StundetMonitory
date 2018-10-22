using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using StudentMonitoringSystem.Model;
using Xamarin.Forms;

namespace StudentMonitoringSystem.Database
{
    public class DatabaseHelper
    {
        public SQLiteConnection database { get; }
        private static DatabaseHelper databaseHelper;
        private static object _collisionLock = new object();

        public DatabaseHelper()
        {
            database = DependencyService.Get<IDatabaseConnection>().DBConnection();
        }
        public static DatabaseHelper GetInstance()
        {
            if (databaseHelper == null)
            {
                databaseHelper = new DatabaseHelper();
            }
            return databaseHelper;
        }

        public void CreateDatabase()
        {
            try
            {
                database.CreateTable<Student>();
                database.CreateTable<StudentClass>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Class CRUD operations

        public int AddNewClass(StudentClass studentClass)
        {
            int result = 0;
            try
            {
                lock (_collisionLock)
                {
                    result = database.Insert(studentClass);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                return 0; //not inserted
            }
        }
        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns>The all students.</returns>
        public List<StudentClass> GetAllClasses()
        {
            try
            {
                lock (_collisionLock)
                {
                    return database.Table<StudentClass>().ToList();
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                return null;
            }
        }
        public int DeleteClass(string classID)
        {
            try
            {
                var countOfDeletedRecord = 0;
                lock (_collisionLock)
                {
                    countOfDeletedRecord = database.Table<Student>().Delete(x => x.ClassId.Equals(classID));
                }
                return countOfDeletedRecord;
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                return 0;
            }
        }


        #endregion

        #region Student CRUD operations

        public Student GetStudentById(string studentId)
        {
            try
            {
                lock (_collisionLock)
                {
                    if (studentId != null)

                    {
                        Student student = database.Table<Student>().First(x => x.StudentId == studentId);
                        return student;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                return null;
            }
        }

        /// <summary>
        /// Adds the new student.
        /// </summary>
        /// <returns>The new student.</returns>
        /// <param name="student">Student.</param>
        public int AddNewStudent(Student student)
        {
            int result = 0;
            try
            {
                lock (_collisionLock)
                {
                    result = database.Insert(student);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                return 0; //not inserted
            }
        }
      
        /// <summary>
        /// Gets the students for class.
        /// </summary>
        /// <returns>The students for class.</returns>
        /// <param name="classId">Class identifier.</param>
        public List<Student> GetStudentsForClass(string classId)
        {
            try
            {
                lock (_collisionLock)
                {
                    var s = database.Table<Student>().Where(x => x.ClassId.Equals(classId)).ToList();
                    return s;
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                return null;
            }
        }
        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns>The all students.</returns>
        public List<Student> GetAllStudents()
        {
            try
            {
                lock (_collisionLock)
                {
                    return database.Table<Student>().ToList();
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                return null;
            }
        }
        public int DeleteStudent(string studentId)
        {
            try
            {
                var countOfDeletedRecord = 0;
                lock (_collisionLock)
                {
                    countOfDeletedRecord = database.Table<Student>().Delete(x => x.StudentId.Equals(studentId));
                }
                return countOfDeletedRecord;
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                return 0;
            }
        }
        public int UpdateStudent(Student student)
        {
            try
            {
                int result = 0;
                lock (_collisionLock)
                {
                    result = database.Update(student);
                }
                return result;
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                return 0;
            }
        }

        #endregion
    }
}
