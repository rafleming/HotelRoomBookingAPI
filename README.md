# HotelRoomBookingAPI

The Hotel Room Booking API is a RESTful web service that allows clients to interact with a hotel's booking system. It supports operations to find hotels, check available rooms, create bookings, and retrieve booking details. The API is built using ASP.NET Core and Entity Framework Core.

## Base URL
The base URL for the API is:
```
https://localhost:7171
```

## Business Rules
- Hotels have 3 room types: single, double, deluxe.
- Hotels have 6 rooms.
- A room cannot be double booked for any given night.
- Any booking at the hotel must not require guests to change rooms at any point
during their stay.
- Booking numbers should be unique. There should not be overlapping at any
given time.
- A room cannot be occupied by more people than its capacit

## Endpoints

### Booking
- ```GET: /api/Booking``` Returns all bookings.
- ```GET: /api/Booking/{bookingNumber}``` Returns a single booking with matching booking number.
- ```POST: /api/Booking``` Places a new booking. Note that to ensure that BookingNumbers remain unique, they will be autogenerated and applied to successfully placed bookings. 

### Hotel
- ```GET: /api/Hotel``` Returns all hotels.
- ```GET: /api/Hotel/{hotelName}``` Returns a single hotel with matching hotel name.

### Room
- ```GET: /api/Room``` Returns all rooms for each hotel.
- ```GET: /api/Room/available``` Returns all available rooms for the dates and number of guests, across all hotels.

### SampleData
- ```POST: /api/SampleData/seed``` Inserts sample data for 6 rooms in 2 hotels.
- ```DELETE: /api/SampleData/seed``` Removes all hotel, room and booking data.

# Notes

### Testing
I have not included unit or automated testing at this point, though dependency injection of service interfaces have been used in consideration of tests being added at a later date.

### Next steps
Finally, this solution is representative of the functional requirements of the technical task but is missing a number of key components that would be expected of a production API, for example:

-  Robust configuration and authentication
-  Validation
-  Error handling
-  Unit testing
-  CI/CD pipelines 

