using System;

namespace SimpleClassSchedule.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string ItemTitle { get; set; }
        public string AimTime { get; set; }
        public string Description { get; set; }


        public Item()
        {

            Id = Guid.NewGuid().ToString();
        }
    }
}