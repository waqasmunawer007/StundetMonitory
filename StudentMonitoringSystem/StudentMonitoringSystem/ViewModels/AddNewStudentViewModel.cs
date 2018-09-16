using System;
using StudentMonitoringSystem.Database;
using StudentMonitoringSystem.Model;
using Xamarin.Forms;

namespace StudentMonitoringSystem.ViewModels
{
    public class AddNewStudentViewModel:BaseViewModel
    {
        private string _stundentName, _fatherName, _cellNumber,_rollNumber;
        INavigation _navigation;
        private StudentClass _studentClass;
        public AddNewStudentViewModel(INavigation navigation,StudentClass studentClass)
        {
            _navigation = navigation;
            _studentClass = studentClass;
        }
        public string StudentName
        {
            get { return _stundentName; }
            set
            {
                _stundentName = value;
                OnPropertyChanged();
            }
        }
        public string FatherName
        {
            get { return _fatherName; }
            set
            {
                _fatherName = value;
                OnPropertyChanged();
            }
        }
        public string RollNumber
        {
            get { return _rollNumber; }
            set
            {
                _rollNumber = value;
                OnPropertyChanged();
            }
        }
        public string CellNumber
        {
            get { return _cellNumber; }
            set
            {
                _cellNumber = value;
                OnPropertyChanged();
            }
        }

        public Command AddNewStudentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    
                    try
                    {

                        Student newStudent = new Student()
                        {
                            StudentName = this.StudentName,
                            StudentRollNumber = this.RollNumber,
                            FatherName = this.FatherName,
                            CellNo = this.CellNumber,
                            ClassId = _studentClass.ClassId,
                            ClassName = _studentClass.ClassName,
                            StudentId = Guid.NewGuid().ToString()
                        };
                        DatabaseHelper.GetInstance().AddNewStudent(newStudent);
                        MessagingCenter.Send((App)Application.Current, "UpdateStudents", newStudent);
                        await _navigation.PopAsync();
                    }
                    catch (Exception) { }

                });
            }
        }
    }
}
