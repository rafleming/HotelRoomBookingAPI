using HotelRoomBookingAPI.Data;
using HotelRoomBookingAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace HotelRoomBookingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.MapOpenApi();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Booking API V1");
                c.RoutePrefix = string.Empty; // Swagger at root URL
            });

            app.MapControllers();
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HotelRoomBookingContext>(options =>
            {
                options.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = HotelRoomBookingDatabase");
            });

            services.AddControllers();
            services.AddOpenApi();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Booking API", Version = "v1" });
            });

            // Register services
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<ISampleDataService, SampleDataService>();
        }
    }
}