using HotelRoomBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomBookingAPI.Data
{
    public class HotelRoomBookingContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public HotelRoomBookingContext(DbContextOptions<HotelRoomBookingContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
