using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SimpleClassSchedule.Models;
using SimpleClassSchedule.Views;
using SimpleClassSchedule.ViewModels;
using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace SimpleClassSchedule.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        /// <summary>
        /// 序列化保存提醒事件
        /// </summary>
        private void SaveAll(object sender,EventArgs e)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Item.xml");
            using (var writer = new StreamWriter(fileName))
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<Item>));
                serializer.Serialize(writer, viewModel.Items);
            }
        }
    }
}