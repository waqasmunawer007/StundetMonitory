using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using StudentMonitoringSystem.ViewModels;

namespace StudentMonitoringSystem
{
    public partial class AddMessagePopup : PopupPage
    {
        AddNewMessageViewModel _viewModel;
        public AddMessagePopup()
        {
            InitializeComponent();
            _viewModel = new AddNewMessageViewModel(Navigation);
            BindingContext = _viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            return base.OnBackButtonPressed();

        }
        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            CloseAllPopup();
            return false;
        }
        private void CancelBtn_Clicked(object sender, EventArgs e)
        {
            CloseAllPopup();
        }
        // close all open popup,s
        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }

        //  Invoked when close button tapped close all popup,s
        //private void OnCloseButton_Tapped(object sender, EventArgs e)
        //{
        //    CloseAllPopup();
        //}
        // close all open popup,s
        //private async void CloseAllPopup()
        //{
        //    await Navigation.PopAllPopupAsync();
        //}
    }
}
