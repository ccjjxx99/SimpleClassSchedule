using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace SimpleClassSchedule.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "课程表";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("http://210.42.121.241/servlet/Login")));
        }

        public ICommand OpenWebCommand { get; }
    }
}