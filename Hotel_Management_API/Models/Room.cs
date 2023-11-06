using System;
using System.Collections.Generic;

namespace Hotel_Management_API.Models
{
    public partial class Room
    {
        public Room()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int RoomId { get; set; }
        public string? RoomName { get; set; }
        public int TypeId { get; set; }
        public int? StatusId { get; set; }
        public int? FloorId { get; set; }

        public virtual Floor? Floor { get; set; }
        public virtual Status? Status { get; set; }
        public virtual RoomType Type { get; set; } = null!;
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
