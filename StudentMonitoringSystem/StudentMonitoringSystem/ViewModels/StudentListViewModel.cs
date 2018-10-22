using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using Plugin.Messaging;
using StudentMonitoringSystem.Database;
using StudentMonitoringSystem.Model;
using Xamarin.Forms;

namespace StudentMonitoringSystem.ViewModels
{
    public class StudentListViewModel: BaseViewModel
    {
        protected IUserDialogs Dialogs { get; }
        private ObservableCollection<Student> _listOfStudents;
        private bool _isEmpty;
        private bool _isNotEmpty;
        private StudentClass _studentClass;

        public StudentListViewModel(StudentClass classId)
        {
            _studentClass = classId;
            _listOfStudents = new ObservableCollection<Student>();
            Dialogs = UserDialogs.Instance;
            IsEmpty = true;
            GetAllStudents();
        }
        public ObservableCollection<Student> ListOfStudents
        {
            get
            {
                return _listOfStudents;
            }
            set
            {
                _listOfStudents = value;
            }
        }
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }
        public bool IsNotEmpty
        {
            get { return _isNotEmpty; }
            set
            {
                _isNotEmpty = value;
                OnPropertyChanged();
            }
        }
        private void GetAllStudents()
        {
            var allStudents = DatabaseHelper.GetInstance().GetStudentsForClass(_studentClass.ClassId);

            if (allStudents != null && allStudents.Count > 0)
            {
                IsEmpty = false;
                IsNotEmpty = true;
                foreach (var student in allStudents)
                {
                    ListOfStudents.Add(student);
                }
            }

        }
        public void EditPhoneNumber(string studentId,string phoneNumber)
        {
            try
            {
                Student student = DatabaseHelper.GetInstance().GetStudentById(studentId);
                student.CellNo = phoneNumber;
                DatabaseHelper.GetInstance().UpdateStudent(student);

                for (int i = 0; i < ListOfStudents.Count; i++)
                {
                    if (studentId.Equals(ListOfStudents[i].StudentId))
                    {
                        Student st = ListOfStudents[i];
                        st.CellNo = phoneNumber;
                        ListOfStudents[i] = st;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }
        public void DeleteStudent(string studentId)
        {
            try{
                DatabaseHelper.GetInstance().DeleteStudent(studentId);
                for (int i = 0; i < ListOfStudents.Count; i++) 
                {
                    if(studentId.Equals(ListOfStudents[i].StudentId))
                    {
                        ListOfStudents.RemoveAt(i);
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }
        public void UpdateStudentList(Student newStudent)
        {
            IsEmpty = false;
            IsNotEmpty = true;
            if (!string.IsNullOrEmpty(newStudent.StudentName))
            {
                ListOfStudents.Add(newStudent);
               
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alert!", "Student name is missing.Please try again.", "OK");
            }

        }

        public string GetAllSelectedPhoneNo()
        {
            string PhoneNumbers = "";
            foreach(var student in ListOfStudents)
            {
                if(student.IsSelected)
                {
                    PhoneNumbers = PhoneNumbers + student.CellNo+",";
                }
            }
            if(!String.IsNullOrEmpty(PhoneNumbers))
            {
                PhoneNumbers = PhoneNumbers.Substring(0, PhoneNumbers.Length - 1);
            }
            return PhoneNumbers;
        }

        public Command PhoneCommand
        {
            get
            {
                return new Command(async (student) =>
                {

                    try
                    {
                        Student selectedStudent = student as Student;
                        var phoneDialer = CrossMessaging.Current.PhoneDialer;
                        if (phoneDialer.CanMakePhoneCall)
                            phoneDialer.MakePhoneCall(selectedStudent.CellNo);


                    }
                    catch (Exception ex)
                    {
                       
                        await Application.Current.MainPage.DisplayAlert("", ex.Message, "Cancel");
                    }

                });
            }
        }

        public Command SelectStudentCommand
        {
            get
            {
                return new Command(async (student) =>
                {

                    try
                    {
                        Student selectedStudent = student as Student;
                        int count = ListOfStudents.Count;
                        for (int i = 0; i <count; i++ )
                        {
                            Student st = ListOfStudents[i];   
                            if(st.StudentId.Equals(selectedStudent.StudentId))
                            {
                                if(!st.IsSelected)
                                {
                                    st.IsSelected = true;
                                }
                                else
                                {
                                    st.IsSelected = false;   
                                }

                                ListOfStudents[i] = st;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        
                        await Application.Current.MainPage.DisplayAlert("", ex.Message, "Cancel");
                    }

                });
            }
        }
       
    }
}
