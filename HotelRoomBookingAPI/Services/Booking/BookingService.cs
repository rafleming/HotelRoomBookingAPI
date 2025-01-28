using HotelRoomBookingAPI.Data;
using HotelRoomBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomBookingAPI.Services
{
    public class BookingService(HotelRoomBookingContext context, IRoomService roomService) : IBookingService
    {
        private readonly HotelRoomBookingContext _context = context;
        private readonly IRoomService _roomService = roomService;
        public static readonly object _lock = new ();

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetBookingAsync(string bookingNumber)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(b => b.BookingNumber == bookingNumber);
        }

        public async Task<Booking?> ConfirmBookingAsync(Booking booking)
        {
            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room == null)
            {
                throw new Exception($"No room found with Id: {booking.RoomId}");
            }

            if (booking.NumberOfGuests <= 0)
            {
                throw new Exception($"At least 1 guest per room is required.");
            }

            if (booking.CheckInDate >= booking.CheckOutDate)
            {
                throw new Exception($"Check out date must be later than check in date.");
            }

            lock (_lock)
            {
                var isAvailable = _roomService.IsRoomAvailable(booking.RoomId, booking.CheckInDate, booking.CheckOutDate, booking.NumberOfGuests);
                if (isAvailable)
                {
                    // Assign a unique booking number to the booking.
                    booking.BookingNumber = GetUniqueBookingNumber();

                    _context.Bookings.Add(booking);
                    _context.SaveChanges();

                    return booking;
                }
                return null;
            }
        }

        private string GetUniqueBookingNumber()
        {
            // Booking reference prefix for HotelRoomBooking.
            const string prefix = "HRB"; 

            // Increment the numeric suffix of the last created booking.
            var lastBooking = _context.Bookings.OrderBy(b => b.Id).LastOrDefault();
            return lastBooking != null ? $"{prefix} {int.Parse(lastBooking.BookingNumber.Split(' ')[1])+1}" : $"{prefix} 1";
        }
    }
}