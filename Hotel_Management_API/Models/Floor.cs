using System;
using System.Collections.Generic;

namespace Hotel_Management_API.Models
{
    public partial class Floor
    {
        public Floor()
        {
            Rooms = new HashSet<Room>();
        }

        public int FloorId { get; set; }
        public string? FloorName { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
