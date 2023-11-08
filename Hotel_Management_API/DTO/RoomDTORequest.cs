namespace Hotel_Management_API.DTO
{
    public class RoomDTORequest
    {
        public int RoomId { get; set; }
        public string? RoomName { get; set; }
        public int? TypeId { get; set; }
        public int? StatusId { get; set; }
        public int? FloorId { get; set; }
    }
}
