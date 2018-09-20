eusing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Messaging;
using StudentMonitoringSystem.Database;
using StudentMonitoringSystem.Model;
using StudentMonitoringSystem.ViewModels;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

namespace StudentMonitoringSystem
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
            DatabaseHelper.GetInstance().CreateDatabase();
            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;

            ToolbarItems.Add(new ToolbarItem("","Add", async() =>
            {
                await Navigation.PushPopupAsync(new AddNewClassPopup(),true);

            }));

            //var smsMessenger = CrossMessaging.Current.SmsMessenger;
            //if (smsMessenger.CanSendSms)
                //smsMessenger.SendSms("+923456866919","Well hello there from Xam.Messaging.Plugin");
        }
        private void OnUserItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                StudentClass selectedClass = e.SelectedItem as StudentClass;
                Navigation.PushAsync(new StudentListPage(selectedClass));
                ((ListView)sender).SelectedItem = null;
            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<App, StudentClass>((App)Application.Current,"UpdateClasses", (s, cl) =>
            {
                _viewModel.UpdateClassList(cl);
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<App, StudentClass>((App)Application.Current, "UpdateClasses");
        }
    }
}
