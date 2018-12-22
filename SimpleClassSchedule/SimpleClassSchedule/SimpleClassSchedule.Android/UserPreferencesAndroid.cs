using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SimpleClassSchedule.Droid
{
    public class UserPreferencesAndroid : IUserPreferences
    {
        public UserPreferencesAndroid()
        {
        }


        public void SetString(string key, string value)
        {
            var prefs = Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
            var prefsEditor = prefs.Edit();

            prefsEditor.PutString(key, value);
            prefsEditor.Commit();
        }

        public string GetString(string key)
        {
            var prefs = Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
            return prefs.GetString(key, "");

        }

        public void DeleteString(string key)
        {
            var prefs = Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
            prefs.Edit().Remove(key).Commit();
        }
    }
}