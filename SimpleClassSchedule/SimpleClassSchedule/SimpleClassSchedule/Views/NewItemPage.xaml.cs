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
                Text = "",
                Description = "",
                Time = DateTime.Now
            };
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (Item.Text != "")
            {
                MessagingCenter.Send(this, "AddItem", Item);
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("保存失败", "标题不能为空", "确定");
            }
        }
    }
}