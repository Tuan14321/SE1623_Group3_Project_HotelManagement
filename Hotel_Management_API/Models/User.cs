using System;
using System.Collections.Generic;

namespace Hotel_Management_API.Models
{
    public partial class User
    {
        public User()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
