using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SimpleClassSchedule.Models;
using SimpleClassSchedule.ViewModels;

namespace SimpleClassSchedule.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                ItemTitle = "",
                Description = ""
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}