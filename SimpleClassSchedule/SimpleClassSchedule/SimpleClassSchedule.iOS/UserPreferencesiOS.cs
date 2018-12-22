using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace SimpleClassSchedule.iOS
{
    public class UserPreferencesiOS : IUserPreferences
    {
        public UserPreferencesiOS()
        {
        }

        public void SetString(string key, string value)
        {
            NSUserDefaults.StandardUserDefaults.SetString(value, key);
        }

        public string GetString(string key)
        {
            return NSUserDefaults.StandardUserDefaults.StringForKey(key);

        }

        public void DeleteString(string key)
        {
            NSUserDefaults.StandardUserDefaults.RemoveObject(key);
        }
    }
}