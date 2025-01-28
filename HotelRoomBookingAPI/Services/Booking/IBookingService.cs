using HotelRoomBookingAPI.Models;

namespace HotelRoomBookingAPI.Services
{
    public interface IBookingService
    {
        /// <summary>
        /// Retrieve all <see cref="Booking"/>s.
        /// </summary>
        Task<IEnumerable<Booking>> GetBookingsAsync();

        /// <summary>
        /// Retrieve a <see cref="Booking"/> with a given booking number.
        /// </summary>
        Task<Booking?> GetBookingAsync(string bookingNumber);

        /// <summary>
        /// Attempts to confirm the <see cref="Booking"/>.
        /// </summary>
        /// <remarks>
        /// If confirmed the booking BookingNumber will be automatically set.
        /// </remarks>
        Task<Booking?> ConfirmBookingAsync(Booking booking);
    }
}