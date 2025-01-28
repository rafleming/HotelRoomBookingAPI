namespace HotelRoomBookingAPI.Models
{
    /// <summary>
    /// This class contains the details of a <see cref="Room"/> booking.
    /// </summary>
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public string CustomerName { get; set; }
        public int NumberOfGuests { get; set; }

        /// <summary>
        /// This is a unique booking reference assigned when the booking is confirmed.
        /// </summary>
        public string BookingNumber { get; set; }
    }
}