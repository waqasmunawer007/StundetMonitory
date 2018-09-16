using System;
using StudentMonitoringSystem.Database;
using StudentMonitoringSystem.Model;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

namespace StudentMonitoringSystem.ViewModels
{
    public class AddNewClassViewModel:BaseViewModel
    {
        private string _className { get; set; }
        private INavigation _navigation;
        public AddNewClassViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }
        public Command AddNewClassCommand
        {
            get
            {
                return new Command(async() =>
                {

                    try
                    {
                        
                        StudentClass newClass = new StudentClass()
                        {
                            ClassName = this.ClassName,
                            ClassId = Guid.NewGuid().ToString()
                        };
                        DatabaseHelper.GetInstance().AddNewClass(newClass);
                        MessagingCenter.Send((App)Application.Current, "UpdateClasses", newClass);
                        await _navigation.PopAllPopupAsync();
                    }
                    catch (Exception) { }                     

                });
            }
        }
        public string ClassName
        {
            get { return _className; }
            set
            {
                _className = value;
                OnPropertyChanged();
            }
        }
    }

}
