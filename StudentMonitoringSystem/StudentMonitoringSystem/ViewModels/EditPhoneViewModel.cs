using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

namespace StudentMonitoringSystem.ViewModels
{
    public class EditPhoneViewModel : BaseViewModel
    {
        private string _phoneNumber { get; set; }
        private INavigation _navigation;
        public EditPhoneViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }
        public Command AddNewPhoneCommand
        {
            get
            {
                return new Command(async () =>
                {

                    try
                    {
                        MessagingCenter.Send((App)Application.Current, "EditPhoneEvent", PhoneNumber);
                        await _navigation.PopAllPopupAsync();
                    }
                    catch (Exception) { }

                });
            }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }
    }

}
