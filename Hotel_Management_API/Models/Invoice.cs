using System;
using System.Collections.Generic;

namespace Hotel_Management_API.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            Payments = new HashSet<Payment>();
            Services = new HashSet<Service>();
        }

        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public double? TotalPrice { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
