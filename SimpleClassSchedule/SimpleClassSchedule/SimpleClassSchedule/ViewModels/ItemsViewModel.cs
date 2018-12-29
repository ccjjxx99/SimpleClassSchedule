using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SimpleClassSchedule.Models;
using SimpleClassSchedule.Views;
using System.IO;
using System.Xml.Serialization;

namespace SimpleClassSchedule.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "提醒";
            Items = new ObservableCollection<Item>();
            InitialzeItems();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void InitialzeItems()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Item.xml");
            if (File.Exists(fileName))
            {
                using (var reader = new StreamReader(fileName))
                {
                    var serializer = new XmlSerializer(typeof(ObservableCollection<Item>));
                    Items = (ObservableCollection<Item>)serializer.Deserialize(reader);//反序列化stream流
                }
            }
        }
    }
}