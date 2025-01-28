using HotelRoomBookingAPI.Data;
using HotelRoomBookingAPI.Models;

namespace HotelRoomBookingAPI.Services
{
    public class SampleDataService(HotelRoomBookingContext context) : ISampleDataService
    {
        private readonly HotelRoomBookingContext _context = context;

        public async Task<bool> PopulateSampleDataAsync()
        {
            // Prevent populating with sample data unless first reset.
            if (_context.Hotels?.Any() == true)
            {
                return false;
            }

            AddSampleHotels();
            await _context.SaveChangesAsync();

            AddSampleBookings();
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task ResetDataAsync()
        {
            _context.Bookings.RemoveRange(_context.Bookings);
            _context.Rooms.RemoveRange(_context.Rooms);
            _context.Hotels.RemoveRange(_context.Hotels);

            await _context.SaveChangesAsync();
        }

        private void AddSampleHotels()
        {
            _context.Hotels.AddRange([
               new Hotel
                {
                    Name = "Hotel 1",
                    Address = "Test St 1",
                    Rooms =
                    [
                        new() { RoomNumber = "1", RoomType = RoomType.Single },
                        new() { RoomNumber = "2", RoomType = RoomType.Double },
                        new() { RoomNumber = "3", RoomType = RoomType.Deluxe },
                        new() { RoomNumber = "4", RoomType = RoomType.Single },
                        new() { RoomNumber = "5", RoomType = RoomType.Double },
                        new() { RoomNumber = "6", RoomType = RoomType.Deluxe },
                    ]
                },
                new Hotel
                {
                    Name = "Hotel 2",
                    Address = "Test St 2",
                    Rooms =
                    [
                        new() { RoomNumber = "1a", RoomType = RoomType.Double },
                        new() { RoomNumber = "2a", RoomType = RoomType.Double },
                        new() { RoomNumber = "3a", RoomType = RoomType.Deluxe },
                        new() { RoomNumber = "4a", RoomType = RoomType.Deluxe },
                        new() { RoomNumber = "5a", RoomType = RoomType.Single },
                        new() { RoomNumber = "6a", RoomType = RoomType.Single },
                    ]
                }
               ]);
        }

        private void AddSampleBookings()
        {
            // Add a test booking for each hotel
            var bookingNumber = 1;

            foreach (var hotel in _context.Hotels.ToList())
            {
                var room = hotel.Rooms.First();

                _context.Bookings.Add(new Booking
                {
                    RoomId = room.Id,
                    CheckInDate = DateOnly.FromDateTime(DateTime.Now),
                    CheckOutDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                    CustomerName = $"Customer {bookingNumber+1}",
                    NumberOfGuests = room.Capacity,
                    BookingNumber = $"HRB {bookingNumber}",
                });

                bookingNumber++;
            }
        }
    }
}