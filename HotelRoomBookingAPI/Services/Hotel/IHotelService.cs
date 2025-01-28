using HotelRoomBookingAPI.Models;

namespace HotelRoomBookingAPI.Services
{
    public interface IHotelService
    {
        /// <summary>
        /// Retrieve all <see cref="Hotel"/>s.
        /// </summary>
        Task<IEnumerable<Hotel>> GetHotelsAsync();

        /// <summary>
        /// Retrieve a single <see cref="Hotel"/> by name.
        /// </summary>
        Task<Hotel?> GetHotelAsync(string hotelName);
    }
}
