using HotelRoomBookingAPI.Data;
using HotelRoomBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomBookingAPI.Services
{
    public class HotelService(HotelRoomBookingContext context) : IHotelService
    {
        private readonly HotelRoomBookingContext _context = context;

        public async Task<IEnumerable<Hotel>> GetHotelsAsync()
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .ToListAsync();
        }

        public async Task<Hotel?> GetHotelAsync(string hotelName)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Name == hotelName);
        }
    }
}
