﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimpleClassSchedule.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SimpleClassSchedule
{
    public partial class App : Application
    {
        public static IUserPreferences UserPreferences { get; private set; }

        public static void Init(IUserPreferences userPreferencesImpl)
        {
            App.UserPreferences = userPreferencesImpl;
        }

        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
