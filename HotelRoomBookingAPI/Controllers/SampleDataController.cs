using HotelRoomBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelRoomBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleDataController(ISampleDataService sampleDataService) : ControllerBase
    {
        private readonly ISampleDataService _sampleDataService = sampleDataService;

        /// <summary>
        /// POST: api/sampledata/seed
        /// </summary>
        [HttpPost("seed")]
        public async Task<IActionResult> SeedData()
        {
            var populated = await _sampleDataService.PopulateSampleDataAsync();
            if (!populated)
            {
                return BadRequest("Existing data must first be cleared using Reset.");
            }

            return Ok("Database seeded with test data.");
        }

        /// <summary>
        /// DELETE: api/sampledata/reset
        /// </summary>
        [HttpDelete("reset")]
        public async Task<IActionResult> ResetData()
        {
            await _sampleDataService.ResetDataAsync();
            return Ok("Database reset.");
        }
    }
}