using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleClassSchedule.Models
{
    public class GridLesson
    {
        public List<Lesson> lessons = new List<Lesson>();


        public void read(string s)
        {
            string pattern0 = "lessonName.{800,1400}方便前台展示";
            //将每个课程信息的部分分离出来
            Regex regex = new Regex(pattern0, RegexOptions.Singleline);
            MatchCollection mc = regex.Matches(s);
            foreach (Match match in mc)
            {
                string[] infos = match.ToString().Split('\n');
                string lessonName = infos[0].Split('\"')[1];//获取课程名
                int i = int.Parse(infos[1].Split('\"')[1]);
                LessonTime.Days day = (LessonTime.Days)(i % 7);//获取在周几
                int beginWeek = int.Parse(infos[3].Split('\"')[1]);//第几周开始
                int endWeek = int.Parse(infos[4].Split('\"')[1]);//第几周结束
                int beginTime = int.Parse(infos[6].Split('\"')[1]);//第几节课开始
                int endTime = int.Parse(infos[7].Split('\"')[1]);//第几节课结束
                string classroom = infos[9].Split('\"')[1];//上课的教室
                int weekInterval = int.Parse(infos[10].Split('\"')[1]);//课程间隔的周数
                string teacherName = infos[11].Split('\"')[1];//老师姓名
                string professionName = infos[12].Split('\"')[1];//老师职称
                Teacher teacher = new Teacher(teacherName, professionName);//构造老师对象
                double credit = Double.Parse(infos[14].Split('\"')[1]);//学分
                string areaName = infos[15].Split('\"')[1];//上课区域
                string academicTeach = infos[17].Split('\"')[1];//开课学院
                //构造上课时间
                LessonTime lessonTime = new LessonTime(day, beginTime, endTime, beginWeek, endWeek, weekInterval);
                //构造课程
                Lesson lesson = new Lesson(lessonName, lessonTime, areaName + classroom, credit, teacher, academicTeach);
                lessons.Add(lesson);//添加课程
            }
        }
        
        public void AddLesson(Lesson l)
        {
            lessons.Add(l);
        }

        public void removeLesson(int i)
        {
            try
            {
                lessons.RemoveAt(i);
            }
            catch (Exception e)
            {
                //提醒用户删除失败
            }
        }
    }
}
