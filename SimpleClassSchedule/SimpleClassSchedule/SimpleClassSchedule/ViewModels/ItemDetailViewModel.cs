using System;

using SimpleClassSchedule.Models;

namespace SimpleClassSchedule.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = "日常详情";
            Item = item;
        }
    }
}
