using System;
using System.Collections.Generic;
using StudentMonitoringSystem.Model;
using StudentMonitoringSystem.ViewModels;
using Xamarin.Forms;

namespace StudentMonitoringSystem
{
    public partial class AddNewStudentPage : ContentPage
    {
        AddNewStudentViewModel _viewModel;
        public AddNewStudentPage(StudentClass studentClass)
        {
            InitializeComponent();
            _viewModel = new AddNewStudentViewModel(Navigation,studentClass);
            BindingContext = _viewModel;
        }

    }
}
