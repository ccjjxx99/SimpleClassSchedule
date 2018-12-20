using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleClassSchedule.Models
{
    class Teacher
    {
        public string Name { set; get; }//老师姓名
        public string Title { set; get; }//老师头衔

        public override string ToString()
        {
            return "任课老师：" + Name + " 教师职称：" + Title;
        }
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
