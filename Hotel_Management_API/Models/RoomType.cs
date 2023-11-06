using System;
using System.Collections.Generic;

namespace Hotel_Management_API.Models
{
    public partial class RoomType
    {
        public RoomType()
        {
            Rooms = new HashSet<Room>();
        }

        public int TypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? PricePerNight { get; set; }
        public double? PricePerHour { get; set; }
        public int? Capacity { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
