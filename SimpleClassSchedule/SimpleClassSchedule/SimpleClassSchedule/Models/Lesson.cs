using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleClassSchedule.Models
{
    public class Lesson
    {
        public string LessonName { set; get; }//课程名称
        public string LessonPosition { set; get; }//上课地点
        public LessonTime LessonTime { set; get; }//上课时间
        public double LessonCredits { set; get; }//课程学分
        public Teacher LessonTeacher { set; get; }//授课教师
        public string Institute { set; get; } //开课学院
        //构造函数，不需要的项置为null
        public Lesson(string name = null, LessonTime time = null, string position = null, double credits = -1, Teacher teacher = null, string institute = null)
        {
            if (name != null)
                LessonName = name;
            if (time != null)
                LessonTime = time;
            if (position != null)
                LessonPosition = position;
            if (credits > 0)
                LessonCredits = credits;
            if (teacher != null)
                LessonTeacher = teacher;
            if (institute != null)
                Institute = institute;
        }
        //修改课程信息，不修改的项置为null
        public void ChangeLesson(string name = null, LessonTime time = null, string position = null, double credits = -1, Teacher teacher = null, string institute = null)
        {
            if (name != null)
                LessonName = name;
            if (time != null)
                LessonTime = time;
            if (position != null)
                LessonPosition = position;
            if (credits > 0)
                LessonCredits = credits;
            if (teacher != null)
                LessonTeacher = teacher;
            if (institute != null)
                Institute = institute;
        }
        //修改老师信息
        public void ChangeLessonTeacher(string name, string title = null)
        {
            LessonTeacher.Name = name;
            if (title != null)
                LessonTeacher.Title = title;
        }

        public override string ToString()
        {
            return LessonName + "\n" + LessonPosition;
        }
        public override bool Equals(object obj)
        {
            Lesson lesson = obj as Lesson;
            return LessonTime.Equals(lesson.LessonTime);
        }

        public override int GetHashCode()
        {
            return -999999999;
        }
    }
}
