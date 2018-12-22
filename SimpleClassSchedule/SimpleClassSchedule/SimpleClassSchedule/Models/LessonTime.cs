using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleClassSchedule.Models
{
    public class LessonTime
    {
        public enum Days { 周日, 周一, 周二, 周三, 周四, 周五, 周六 };
        public Days Day { set; get; }//课程在周几
        public int Start { set; get; }//课程开始是第几节课
        public int End { set; get; }//课程结束是第几节课
        public int StartWeek { set; get; }//课程开始是第几周
        public int EndWeek { set; get; }//课程结束是第几周
        public int Gap = 1;//课程间隔，默认每周一次课
        //构造函数，默认1-17周

        public LessonTime(Days day, int start, int end, int startWeek = 1, int endWeek = 17, int gap = 1)
        {
            Day = day;
            Start = start;
            End = end;
            StartWeek = startWeek;
            EndWeek = endWeek;
            if (gap > 0)
                Gap = gap;
        }

        public override string ToString()
        {
            return Day.ToString() + "，" + StartWeek + "-" + EndWeek + "，每" + Gap + "周，" + Start + "-" + End + "节";
        }
        public override bool Equals(object obj)
        {
            LessonTime lessonTime = obj as LessonTime;

            return (Day == lessonTime.Day) && (Start == lessonTime.Start) &&
                (End == lessonTime.End) && (StartWeek == lessonTime.StartWeek)
                && (EndWeek == lessonTime.EndWeek);
        }

        public override int GetHashCode()
        {
            return (int)Day + Start * End + StartWeek * EndWeek;
        }
    }
}
