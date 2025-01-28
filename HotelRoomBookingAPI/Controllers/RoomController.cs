using HotelRoomBookingAPI.Models;
using HotelRoomBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HotelRoomBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(IRoomService roomService) : ControllerBase
    {
        private readonly IRoomService _roomService = roomService;

        /// <summary>
        /// GET: api/room
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            var rooms = await _roomService.GetRoomsAsync();
            return Ok(rooms);
        }

        /// <summary>
        /// GET: api/room/available
        /// </summary>
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Room>>> GetAvailableRooms(
            [BindRequired] DateOnly checkInDate, 
            [BindRequired] DateOnly checkOutDate,
            [BindRequired] int numberOfGuests)
        {
            try 
            {
                var availableRooms = await _roomService.GetAvailableRoomsAsync(checkInDate, checkOutDate, numberOfGuests);
                return Ok(availableRooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
