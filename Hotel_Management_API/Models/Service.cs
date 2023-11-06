using System;
using System.Collections.Generic;

namespace Hotel_Management_API.Models
{
    public partial class Service
    {
        public int ServiceId { get; set; }
        public string Servicename { get; set; } = null!;
        public double Price { get; set; }
        public int? InvoiceId { get; set; }

        public virtual Invoice? Invoice { get; set; }
    }
}
