using SimpleClassSchedule.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using SimpleClassSchedule.ViewModels;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace SimpleClassSchedule.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public Grid g = new Grid();
        List<Lesson> list;//保存所有课程
        List<Lesson> finalList;//当前周数课程
        Button[] buttons;
        public static int currentWeek = 1;


        public AboutPage()
        {
            InitializeComponent();
            try
            {
                currentWeek = int.Parse(App.UserPreferences.GetString("week"));
            }
            catch { }
            weekToolbar.Text = "第" + currentWeek.ToString() + "周";
            Content = g;

            

            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(7, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(3, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Star) });
            //列标签
            for (int i = 0; i < 8; i++)
            {
                Label label = new Label();
                if (i == 0)
                {
                    label.Text = "";
                    label.HorizontalTextAlignment = TextAlignment.Center;
                    g.Children.Add(label);
                    Grid.SetRow(label, 0);
                    Grid.SetColumn(label, i);
                }
                else
                {
                    label.Text = ((LessonTime.Days)(i - 1)).ToString();
                    label.TextColor = Color.Black;
                    label.HorizontalTextAlignment = TextAlignment.Center;
                    g.Children.Add(label);
                    Grid.SetRow(label, 0);
                    Grid.SetColumn(label, i);

                }
            }

            string[] times = {"08:00","08:50","09:50","10:40","11:30","14:05","14:55",
                    "15:45","16:40","17:30","18:30","19:20","21:30"};

            //行标签
            for (int i = 1; i <= 13; i++)
            {
                Label label = new Label
                {
                    Text = times[i - 1],
                    FontSize = 8,
                    TextColor = Color.Black
                };
                g.Children.Add(label);
                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
            }

            //string s = App.UserPreferences.GetString("LessonInfo");//读取本地保存的字符串
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lessons.xml");
            if(File.Exists(fileName))
            {
                //string s = File.ReadAllText(fileName);
                //GridLesson gridLesson = new GridLesson();
                //if (s.Length != 0)
                //{
                //    gridLesson.read(s);
                //    list = gridLesson.lessons;

                //    draw(currentWeek);
                //}
                using (var reader = new StreamReader(fileName))
                {
                    var serializer = new XmlSerializer(typeof(List<Lesson>));
                    list = (List<Lesson>)serializer.Deserialize(reader);//反序列化stream流
                }
                draw(currentWeek);
            }
        }

        /// <summary>
        /// 初始化课程界面
        /// </summary>
        /// <param name="week"></param>
        private void draw(int week)
        {
            finalList = SelectLesson(week);

            buttons = new Button[finalList.Count];
            //按钮
            for (int i = 0; i < finalList.Count; i++)
            {
                buttons[i] = new Button
                {
                    Text = finalList[i].ToString(),
                    ClassId = i.ToString(),
                    TextColor = Color.White,
                    Padding = 0,
                    BorderRadius = 6,
                    FontSize = 10,
                    BackgroundColor = Color.FromRgb(60, 160, 245),
                };
                buttons[i].Clicked += BtnClick;//点击后弹出窗口
                g.Children.Add(buttons[i]);
                Grid.SetRow(buttons[i], finalList[i].LessonTime.Start);
                Grid.SetRowSpan(buttons[i], finalList[i].LessonTime.End - finalList[i].LessonTime.Start + 1);
                Grid.SetColumn(buttons[i], (int)finalList[i].LessonTime.Day + 1);
            }
        }

        /// <summary>
        /// 点击按钮弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void BtnClick(object sender, EventArgs args)
        {
            int i = Convert.ToInt32(((Button)sender).ClassId);
            int startweek = finalList[i].LessonTime.StartWeek;
            int endweek = finalList[i].LessonTime.EndWeek;
            int start = finalList[i].LessonTime.Start;
            int end = finalList[i].LessonTime.End;
            int day = (int)finalList[i].LessonTime.Day;
            string teacher = finalList[i].LessonTeacher.ToString();
            string institute = finalList[i].Institute;
            //信息
            string LessonName = finalList[i].LessonName;           
            string LessonTime = "第"+startweek+"-"+endweek+"周 每一周 周"+day+" "+start+"-"+end + "节";
            string LessonPosition = finalList[i].LessonPosition;
            string Teacher = teacher + "-" + institute;
            string info = LessonTime + "\n\n" + LessonPosition + "\n\n" + Teacher;
            //弹出窗口
            DisplayAlert(LessonName, info, "确定");
        }

        /// <summary>
        /// 根据当前周数选择课程
        /// </summary>
        /// <returns></returns>
        private List<Lesson> SelectLesson(int i)
        {
            return list.Where(l => (l.LessonTime.StartWeek <= i && l.LessonTime.EndWeek >= i)).ToList<Lesson>();
        }     
        
        /// <summary>
        /// 设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingPage(), true);
        }

        /// <summary>
        /// 更改周数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void WeekBtn_Clicked(object sender, EventArgs e)
        {
            
            string[] weeks = new string[20];
            for(int i = 0; i < 20; i++)
            {
                weeks[i] = (i + 1).ToString();
            }
            var action = await DisplayActionSheet("更改当前周数", "cancel", null, weeks);
            string s = action;//获取返回的字符串
            App.UserPreferences.SetString("week", s);

            if (s != "cancel")
            {
                foreach (Button btn in buttons)
                    g.Children.Remove(btn);
                currentWeek = int.Parse(s);//更改当前周数
                ((ToolbarItem)sender).Text = "第" + currentWeek.ToString() + "周";
                draw(currentWeek);//重新画图
            }
        }

    }
}