using SimpleClassSchedule.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleClassSchedule.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        Grid g = new Grid();

        public AboutPage()
        {
            InitializeComponent();


            string s = App.UserPreferences.GetString("LessonInfo");
            if(s != "")
            {
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
                g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
                g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Star) });
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

                //行标签
                for (int i = 1; i <= 13; i++)
                {
                    Label label = new Label();
                    label.Text = i.ToString();
                    label.TextColor = Color.Black;
                    g.Children.Add(label);
                    Grid.SetRow(label, i);
                    Grid.SetColumn(label, 0);

                }

                GridLesson gridLesson = new GridLesson();
                if (s.Length != 0) ;
                {
                    gridLesson.read(s);
                    List<Lesson> list = gridLesson.lessons;

                    //按钮
                    for (int i = 0; i < list.Count; i++)
                    {
                        Button button = new Button();
                        button.Text = list[i].LessonName;
                        button.TextColor = Color.White;
                        button.Padding = 0;
                        button.BorderRadius = 6;
                        button.FontSize = 11;
                        button.BackgroundColor = Color.FromRgb(60, 160, 245);
                        g.Children.Add(button);
                        Grid.SetRow(button, list[i].LessonTime.Start);
                        Grid.SetRowSpan(button, list[i].LessonTime.End - list[i].LessonTime.Start + 1);
                        Grid.SetColumn(button, (int)list[i].LessonTime.Day + 1);
                    }

                }
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingPage(), true);
        }
    }
}