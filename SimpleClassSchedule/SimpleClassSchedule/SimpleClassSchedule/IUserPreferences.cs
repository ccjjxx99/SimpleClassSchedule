using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleClassSchedule
{
    public interface IUserPreferences
    {
        void SetString(string key, string value);
        string GetString(string key);
        void DeleteString(string key);
    }
}
