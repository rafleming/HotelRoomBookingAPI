using HotelRoomBookingAPI.Models;

namespace HotelRoomBookingAPI.Extensions
{
    public static class RoomTypeExtensions
    {
        public static int Capacity(this RoomType roomType)
        {
            return roomType switch
            {
                RoomType.Single => 1,
                RoomType.Double => 2,
                RoomType.Deluxe => 4,
                _ => throw new Exception($"Unknown room type {roomType}"),
            };
        }
    }
}