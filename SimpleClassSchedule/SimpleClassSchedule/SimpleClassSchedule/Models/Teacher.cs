using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleClassSchedule.Models
{
    [Serializable]
    public class Teacher
    {
        public string Name { set; get; }//老师姓名
        public string Title { set; get; }//老师头衔

        public override string ToString()
        {
            return Name + "-" + Title;
        }

        public Teacher() { }

        public Teacher(string name)
        {
            Name = name;
        }

        public Teacher(string name, string title)
        {
            Name = name;
            Title = title;
        }
    }
}
