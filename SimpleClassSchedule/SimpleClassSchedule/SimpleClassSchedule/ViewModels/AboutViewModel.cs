using System;
using System.Windows.Input;
using SimpleClassSchedule.Views;
using Xamarin.Forms;

namespace SimpleClassSchedule.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        int week = 0;
        public int Week
        {
            get { return AboutPage.currentWeek; }
            set { SetProperty(ref week, value); }
        }
        

        public AboutViewModel()
        {
            Title = "课程表";
            week = AboutPage.currentWeek;
        } 
    }
}