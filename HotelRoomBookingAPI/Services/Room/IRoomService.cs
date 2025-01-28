using HotelRoomBookingAPI.Models;

namespace HotelRoomBookingAPI.Services
{
    public interface IRoomService
    {
        /// <summary>
        /// Retrieve all <see cref="Room"/>s.
        /// </summary>
        Task<IEnumerable<Room>> GetRoomsAsync();

        /// <summary>
        /// Retrieve all <see cref="Room"/>s available for the given dates and that have capacity for the given number of guests.
        /// </summary>
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateOnly checkInDate, DateOnly checkOutDate, int numberOfGuests);

        /// <summary>
        /// Check if a <see cref="Room"/>s available for the given dates and has capacity for the given number of guests.
        /// </summary>
        bool IsRoomAvailable(int roomId, DateOnly checkInDate, DateOnly checkOutDate, int numberOfGuests);
    }
}