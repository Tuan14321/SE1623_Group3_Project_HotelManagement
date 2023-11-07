namespace Hotel_Management_API.Models
{
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public double? TotalPrice { get; set; }
        public string TypeName { get; set; }
        public string Status { get; set; }
        public string Floor { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }

    }
}
