﻿using System;
using System.Collections.Generic;
using Plugin.Messaging;
using StudentMonitoringSystem.Model;
using StudentMonitoringSystem.ViewModels;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using StudentMonitoringSystem.Database;

namespace StudentMonitoringSystem
{
    public partial class StudentListPage : ContentPage
    {
        StudentListViewModel _viewModel;
        private string _studenId;
        public StudentListPage(StudentClass studentClass)
        {
            InitializeComponent();
            _viewModel = new StudentListViewModel(studentClass);
            BindingContext = _viewModel;

            ToolbarItems.Add(new ToolbarItem("", "Add", async() =>
            {
                await Navigation.PushAsync(new AddNewStudentPage(studentClass));
            }));
            ToolbarItems.Add(new ToolbarItem("", "SendToAll", async() =>
            {
                await Navigation.PushPopupAsync(new AddMessagePopup());
            }));

           
        }
        private void EditStudentContextual_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            _studenId = item.CommandParameter.ToString();
            Navigation.PushPopupAsync(new EditPhonePopup());

        }
        private void DeleteStudentContextual_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var id = item.CommandParameter.ToString();
            _viewModel.DeleteStudent(id);
          
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Unsubscribe<App, StudentClass>((App)Application.Current, "UpdateStudents");
            MessagingCenter.Unsubscribe<App, string>((App)Application.Current, "EditPhoneEvent");
            MessagingCenter.Subscribe<App, string>((App)Application.Current, "EditPhoneEvent", (s, phoneNumber) =>
            {
                _viewModel.EditPhoneNumber(_studenId, phoneNumber);
            });

            MessagingCenter.Subscribe<App, Student>((App)Application.Current, "UpdateStudents", (s, st) =>
            {
                _viewModel.UpdateStudentList(st);
            });

            MessagingCenter.Subscribe<App, string>((App)Application.Current, "AddMessage", (s, message) =>
            {
                if(!String.IsNullOrEmpty(message))
                {
                   // _viewModel.UpdateStudentList(st);
                    string phones = _viewModel.GetAllSelectedPhoneNo();
                    if (!String.IsNullOrEmpty(phones))
                    { 
                        var smsMessenger = CrossMessaging.Current.SmsMessenger;
                        if (smsMessenger.CanSendSms)
                            smsMessenger.SendSms(phones, message);
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Alert!", "Please must select one or more contact number before sending message. ", "OK");   
                    }
                   
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert!", "Message can not be empty.Please must enter message body. ", "OK");
                }
           });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<App, string>((App)Application.Current, "AddMessage");
           
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                //StudentClass selectedClass = e.SelectedItem as StudentClass;
                //Navigation.PushAsync(new StudentListPage(selectedClass.ClassId));
                ((ListView)sender).SelectedItem = null;
            }

        }
    }
}
