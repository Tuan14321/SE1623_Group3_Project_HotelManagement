namespace Hotel_Management_Client.Models
{
    public class HomeViewModel
    {
        public class FloorViewModel
        {
            public int FloorId { get; set; }
            public string FloorName { get; set; }
            public List<RoomViewModel> Rooms { get; set; }
        }

        public class RoomViewModel
        {
            public int RoomId { get; set; }
            public string RoomName { get; set; }
            public int? StatusId { get; set; }
            // Add more room properties as needed
        }

    }
}