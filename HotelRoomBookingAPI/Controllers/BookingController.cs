using HotelRoomBookingAPI.Models;
using HotelRoomBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelRoomBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(IBookingService bookingService) : ControllerBase
    {
        private readonly IBookingService _bookingService = bookingService;

        /// <summary>
        /// GET: api/booking
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            var bookings = await _bookingService.GetBookingsAsync();
            return Ok(bookings);
        }

        /// <summary>
        /// GET: api/booking/{bookingNumber}
        /// </summary>
        [HttpGet("{bookingNumber}")]
        public async Task<ActionResult<Booking>> GetBookingByNumber(string bookingNumber)
        {
            var booking = await _bookingService.GetBookingAsync(bookingNumber);
            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        /// <summary>
        /// POST: api/booking
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(Booking booking)
        {
            try 
            {
                var confirmedBooking = await _bookingService.ConfirmBookingAsync(booking);
                if (confirmedBooking == null)
                {
                    return BadRequest("Failed to create booking.");
                }
                return confirmedBooking;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}