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
            ConfigureServices(builder.Services, DatabaseConnection(builder));

            // Build the app
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.MapOpenApi();
            }
            
            // Configure Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Booking API V1");
                c.RoutePrefix = string.Empty; // Swagger at root URL
            });

            // Add the endpoints
            app.MapControllers();
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, string dbConnection)
        {
            services.AddDbContext<HotelRoomBookingContext>(options => options.UseSqlServer(dbConnection));
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

       private static string DatabaseConnection(WebApplicationBuilder builder)
       {
            var connection = string.Empty;
            
            // Local test db
            // connection = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = HotelRoomBookingDatabase";

            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
            }
            else
            {
                builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.json");
            }

            connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            return connection;
        }
    }
}