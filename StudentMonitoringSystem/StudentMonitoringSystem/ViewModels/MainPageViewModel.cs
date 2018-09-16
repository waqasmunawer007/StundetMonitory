using System;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using StudentMonitoringSystem.Database;
using StudentMonitoringSystem.Model;
using Xamarin.Forms;

namespace StudentMonitoringSystem.ViewModels
{
    public class MainPageViewModel: BaseViewModel
    {
        protected IUserDialogs Dialogs { get; }       
        private ObservableCollection<StudentClass> _listOfClasses;
        private bool _isEmpty;
        private bool _isNotEmpty;

        public MainPageViewModel()
        {
            _listOfClasses = new ObservableCollection<StudentClass>();
            Dialogs = UserDialogs.Instance;
            IsEmpty = true;
            GetAllClasses();
        }
        public ObservableCollection<StudentClass> ListOfClasses
        {
            get
            {
                return _listOfClasses;
            }
            set
            {
                _listOfClasses = value;
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
        private void GetAllClasses()
        {

            var allClasses = DatabaseHelper.GetInstance().GetAllClasses();
            if(allClasses != null && allClasses.Count > 0)
            {
                IsEmpty = false;
                IsNotEmpty = true;
                foreach(var studentClass in allClasses)
                {
                    ListOfClasses.Add(studentClass);
                }
            }
        }
        public void UpdateClassList(StudentClass newClass)
        {
            IsEmpty = false;
            IsNotEmpty = true;
            if(!string.IsNullOrEmpty(newClass.ClassName))
            {
                ListOfClasses.Add(newClass);
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alert!", "Class name is missing.Please try again.", "OK");
            }

           
        }
        public Command SendCommand
        {
            get
            {
                return new Command(async(student) =>
                {
                    
                        try
                        {
                            this.Dialogs.ShowLoading("Waiting..", MaskType.Black);

                        }
                        catch (Exception ex)
                        {
                            this.Dialogs.HideLoading();
                            await Application.Current.MainPage.DisplayAlert("", ex.Message, "Cancel");
                        }

                });
            }
        }
        public Command SendAllCommand
        {
            get
            {
                return new Command(async (student) =>
                {

                    try
                    {
                        this.Dialogs.ShowLoading("Waiting..", MaskType.Black);

                    }
                    catch (Exception ex)
                    {
                        this.Dialogs.HideLoading();
                        await Application.Current.MainPage.DisplayAlert("", ex.Message, "Cancel");
                    }
                });
            }
        }
        public Command AddNewClassCommand
        {
            get
            {
                return new Command(() =>
                {

                    try
                    {
                        this.Dialogs.ShowLoading("Waiting..", MaskType.Black);

                    }
                    catch (Exception ex)
                    {
                        this.Dialogs.HideLoading();
                        Application.Current.MainPage.DisplayAlert("", ex.Message, "Cancel");
                    }


                });
            }
        }
    }
}
