namespace HotelRoomBookingAPI.Services
{
    public interface ISampleDataService
    {
        /// <summary>
        /// Add minimal seed data for testing.
        /// </summary>
        Task<bool> PopulateSampleDataAsync();

        /// <summary>
        /// Remove all testing data.
        /// </summary>
        Task ResetDataAsync();
    }
}
