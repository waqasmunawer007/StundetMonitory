using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using StudentMonitoringSystem.ViewModels;
using Xamarin.Forms;

namespace StudentMonitoringSystem
{
    public partial class EditPhonePopup : PopupPage
    {
        EditPhoneViewModel _viewModel;
        public EditPhonePopup()
        {
            InitializeComponent();
            _viewModel = new EditPhoneViewModel(Navigation);
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
    }
}
