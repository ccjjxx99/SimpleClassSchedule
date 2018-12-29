using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SimpleClassSchedule.Models;

namespace SimpleClassSchedule.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                AimTime = "",
                Description = ""
 
            };
            BindingContext = this;
            _timePicker.Time = DateTime.Now - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);//获取当前时间


        }

        async void Save_Clicked(object sender, EventArgs e)
        { 

            DateTime AimTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Add(_timePicker.Time);//获取目标时间
            Item.AimTime = AimTime.ToString("m") + " " + AimTime.ToString("t");

            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();                
        }
    }
}