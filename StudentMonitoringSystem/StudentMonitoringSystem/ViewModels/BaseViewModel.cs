using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentMonitoringSystem.ViewModels
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static object collisionLock = new object();
        public BaseViewModel()
        {
           
        }
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
