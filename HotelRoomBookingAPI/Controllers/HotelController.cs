using HotelRoomBookingAPI.Models;
using HotelRoomBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelRoomBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController(IHotelService hotelService) : ControllerBase
    {
        private readonly IHotelService _hotelService = hotelService;

        /// <summary>
        /// GET: api/hotel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            var hotels = await _hotelService.GetHotelsAsync();
            return Ok(hotels);
        }

        /// <summary>
        /// GET: api/hotel/{hotelName}
        /// </summary>
        [HttpGet("{hotelName}")]
        public async Task<ActionResult<Hotel>> GetHotelByName(string hotelName)
        {
            var hotel = await _hotelService.GetHotelAsync(hotelName);
            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }
    }
}