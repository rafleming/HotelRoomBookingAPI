using HotelRoomBookingAPI.Extensions;

namespace HotelRoomBookingAPI.Models
{
    /// <summary>
    /// This class represents a room in a <see cref="Hotel"/>.
    /// </summary>
    public class Room
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public string RoomNumber { get; set; }

        /// <summary>
        /// Is this a Single, Double or Deluxe room.
        /// </summary>
        public RoomType RoomType { get; set; }

        /// <summary>
        /// All the <see cref="Booking"/>'s for the room.
        /// </summary>
        public ICollection<Booking> Bookings { get; set; }

        /// <summary>
        /// Maximum number of occupants for the room. 
        /// </summary>
        public int Capacity => RoomType.Capacity();
    }
}