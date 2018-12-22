using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleClassSchedule.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public SettingPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                "导入课程表",
                "导出为图片",
                "创建窗口小部件",
                "关于",
            };
			
			MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            else if (e.Item == Items[0])
            {
                await Navigation.PushAsync(new ImportLessonPage(), true);
            }
            

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
