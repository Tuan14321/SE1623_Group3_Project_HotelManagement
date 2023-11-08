namespace Hotel_Management_API.DTO
{
    public class RoomDTO
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string? TypeName { get; set; } // Thêm trường này
        public string? StatusName { get; set; } // Thêm trường này
        public string?  FloorName { get; set; } // Thêm trường này
    }
}
