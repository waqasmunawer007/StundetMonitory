using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

namespace StudentMonitoringSystem.ViewModels
{
    public class AddNewMessageViewModel: BaseViewModel
    {
        private string _message { get; set; }
        private INavigation _navigation;
        public AddNewMessageViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }
        public Command AddMessageCommand
        {
            get
            {
                return new Command(async () =>
                {

                    try
                    {
                        MessagingCenter.Send((App)Application.Current, "AddMessage", Message);
                        await _navigation.PopAllPopupAsync();
                    }
                    catch (Exception) { }

                });
            }
        }
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }
    }

}
