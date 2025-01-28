namespace HotelRoomBookingAPI.Models
{
    /// <summary>
    /// This class represents a Hotel with a number of bookable <see cref="Room"/>s.
    /// </summary>
    public class Hotel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        /// <summary>
        /// All of the <see cref="Room"/>s within the Hotel.
        /// </summary>
        public ICollection<Room> Rooms { get; set; }
    }
}