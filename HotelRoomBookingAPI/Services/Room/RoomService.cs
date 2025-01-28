using HotelRoomBookingAPI.Data;
using HotelRoomBookingAPI.Extensions;
using HotelRoomBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomBookingAPI.Services
{
    public class RoomService(HotelRoomBookingContext context) : IRoomService
    {
        private readonly HotelRoomBookingContext _context = context;

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            return await _context.Rooms
                .Include(r => r.Bookings)
                .ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(
            DateOnly checkInDate,
            DateOnly checkOutDate,
            int numberOfGuests)
        {
            if (numberOfGuests <= 0)
            {
                throw new Exception($"At least 1 guest per room is required.");
            }

            if (checkInDate >= checkOutDate)
            {
                throw new Exception($"Check out date must be later than check in date.");
            }

            // Filter the matching room types based on the number of guests.
            var possibleRoomTypes = RoomTypesWithCapacity(numberOfGuests);

            return await _context.Rooms
                .Include(r => r.Bookings)
                .Where(r => !r.Bookings.Any(b => b.CheckInDate < checkOutDate && b.CheckOutDate > checkInDate) && possibleRoomTypes.Contains(r.RoomType))
                .ToListAsync();
        }

        public bool IsRoomAvailable(int roomId, DateOnly checkInDate, DateOnly checkOutDate, int numberOfGuests)
        {
            var room = _context.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.Id == roomId);

            if (room != null)
            {
                var possibleRoomTypes = RoomTypesWithCapacity(numberOfGuests);

                // Check the room has capacity and does not have any existing bookings for those dates.
                return possibleRoomTypes.Contains(room.RoomType) 
                    && !room.Bookings.Any(b => b.CheckInDate < checkOutDate && b.CheckOutDate > checkInDate);
            }

            return false;
        }

        private static IEnumerable<RoomType> RoomTypesWithCapacity(int numberOfGuests)
        {
            return Enum.GetValues<RoomType>().Where(r => r.Capacity() >= numberOfGuests);
        }
    }
}