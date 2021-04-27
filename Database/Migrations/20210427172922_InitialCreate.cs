using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    AirportId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<decimal>(type: "Decimal(8,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "Decimal(9,6)", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.AirportId);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    PlaneId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    MaxSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxDistance = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.PlaneId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LoginId = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    OriginAirportId = table.Column<int>(type: "INTEGER", nullable: false),
                    DestinationAirportId = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartureTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    PlaneId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCanceled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_DestinationAirportId",
                        column: x => x.DestinationAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_OriginAirportId",
                        column: x => x.OriginAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Planes_PlaneId",
                        column: x => x.PlaneId,
                        principalTable: "Planes",
                        principalColumn: "PlaneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDatas",
                columns: table => new
                {
                    CustomerDataId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    CreditCardNumber = table.Column<string>(type: "TEXT", nullable: false),
                    AccountBalance = table.Column<decimal>(type: "TEXT", nullable: false),
                    RewardPointsBalance = table.Column<int>(type: "INTEGER", nullable: false),
                    RewardPointsUsed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDatas", x => x.CustomerDataId);
                    table.ForeignKey(
                        name: "FK_CustomerDatas_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledFlights",
                columns: table => new
                {
                    ScheduledFlightId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledFlights", x => x.ScheduledFlightId);
                    table.ForeignKey(
                        name: "FK_ScheduledFlights_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstReturnTicketIndex = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_CustomerDatas_CustomerDataId",
                        column: x => x.CustomerDataId,
                        principalTable: "CustomerDatas",
                        principalColumn: "CustomerDataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScheduledFlightId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCanceled = table.Column<bool>(type: "INTEGER", nullable: false),
                    PointsEarned = table.Column<bool>(type: "INTEGER", nullable: false),
                    PaymentMethod = table.Column<int>(type: "INTEGER", nullable: false),
                    BookingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_ScheduledFlights_ScheduledFlightId",
                        column: x => x.ScheduledFlightId,
                        principalTable: "ScheduledFlights",
                        principalColumn: "ScheduledFlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Latitude", "Longitude", "State" },
                values: new object[] { 1, "Cleveland", "CLE", 41.411667m, -81.849722m, "Ohio" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Latitude", "Longitude", "State" },
                values: new object[] { 10, "Seattle", "SEA", 47.448889m, -122.309444m, "Washington" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Latitude", "Longitude", "State" },
                values: new object[] { 8, "Dallas", "DFW", 32.896944m, -97.038056m, "Texas" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Latitude", "Longitude", "State" },
                values: new object[] { 7, "Chicago", "MDW", 41.786111m, -87.7525m, "Illinois" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Latitude", "Longitude", "State" },
                values: new object[] { 6, "Los Angeles", "LAX", 33.9425m, -118.408056m, "California" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Latitude", "Longitude", "State" },
                values: new object[] { 9, "Denver", "DEN", 39.861667m, -104.673056m, "Colorado" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Latitude", "Longitude", "State" },
                values: new object[] { 4, "Atlanta", "ATL", 33.636667m, -84.428056m, "Georgia" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Latitude", "Longitude", "State" },
                values: new object[] { 3, "Detroit", "DTW", 42.2125m, -83.353333m, "Michigan" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Latitude", "Longitude", "State" },
                values: new object[] { 2, "Nashville", "BNA", 36.126667m, -86.681944m, "Tennessee" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Latitude", "Longitude", "State" },
                values: new object[] { 5, "New York City", "LGA", 40.775m, -73.875m, "New York" });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxDistance", "MaxSeats", "Model" },
                values: new object[] { 1, 6570, 230, "Boeing 737 MAX" });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxDistance", "MaxSeats", "Model" },
                values: new object[] { 2, 14815, 416, "Boeing 747" });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxDistance", "MaxSeats", "Model" },
                values: new object[] { 3, 17395, 550, "Boeing 777" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "LoginId", "PasswordHash", "Role" },
                values: new object[] { 3, "flight_manager", "4iifPzpmqB9f+1Lf8aCc0q6Ro56ySCMNKQcIRnnGus20qIDaeDXKcgA9jDfhB8+Rxfl5VngwN1Trob5CA5v/TQ==", 3 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "LoginId", "PasswordHash", "Role" },
                values: new object[] { 1, "accountant", "O+hVd5AIc4hngejP3jaUBHyJduXRk15reb/RyCkSY8wgBK6+AKe48n0LJ2UxVWY60NglI4NkfGRnKraWch8ftQ==", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "LoginId", "PasswordHash", "Role" },
                values: new object[] { 2, "load_engineer", "QewmDvo6oFSpHMbPlEHjZSY391lGyPpsLpJvKJ4JX0bmLhmijgL7D8Jd0Eex8E5uA8+TDUZLVAxAwARetrfiUg==", 2 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "LoginId", "PasswordHash", "Role" },
                values: new object[] { 4, "marketing_manager", "NgyuvZ7baGCcCTO63jVlNQ5Z4oTMUDzmG/DuvUL7flvWV6ce0UmCJRaHV+fxCVkgQRzOJ3eeDHeOxSU13q4gQA==", 4 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 1, new TimeSpan(0, 6, 35, 0, 0), 2, false, 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 196, new TimeSpan(0, 13, 30, 0, 0), 8, false, 196, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 197, new TimeSpan(0, 7, 0, 0, 0), 7, false, 197, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 198, new TimeSpan(0, 10, 30, 0, 0), 7, false, 198, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 199, new TimeSpan(0, 13, 0, 0, 0), 7, false, 199, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 200, new TimeSpan(0, 16, 30, 0, 0), 7, false, 200, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 201, new TimeSpan(0, 12, 55, 0, 0), 9, false, 201, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 202, new TimeSpan(0, 15, 25, 0, 0), 9, false, 202, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 203, new TimeSpan(0, 18, 55, 0, 0), 9, false, 203, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 204, new TimeSpan(0, 21, 25, 0, 0), 9, false, 204, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 205, new TimeSpan(0, 12, 55, 0, 0), 7, false, 205, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 206, new TimeSpan(0, 15, 25, 0, 0), 7, false, 206, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 207, new TimeSpan(0, 18, 55, 0, 0), 7, false, 207, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 208, new TimeSpan(0, 21, 25, 0, 0), 7, false, 208, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 217, new TimeSpan(0, 13, 50, 0, 0), 9, false, 217, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 218, new TimeSpan(0, 16, 20, 0, 0), 9, false, 218, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 219, new TimeSpan(0, 19, 50, 0, 0), 9, false, 219, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 220, new TimeSpan(0, 22, 20, 0, 0), 9, false, 220, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 221, new TimeSpan(0, 10, 50, 0, 0), 8, false, 221, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 222, new TimeSpan(0, 13, 20, 0, 0), 8, false, 222, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 223, new TimeSpan(0, 16, 50, 0, 0), 8, false, 223, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 224, new TimeSpan(0, 19, 20, 0, 0), 8, false, 224, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 225, new TimeSpan(0, 15, 50, 0, 0), 10, false, 225, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 226, new TimeSpan(0, 18, 20, 0, 0), 10, false, 226, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 227, new TimeSpan(0, 21, 50, 0, 0), 10, false, 227, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 228, new TimeSpan(0, 0, 20, 0, 0), 10, false, 228, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 195, new TimeSpan(0, 10, 0, 0, 0), 8, false, 195, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 229, new TimeSpan(0, 12, 50, 0, 0), 9, false, 229, 10, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 194, new TimeSpan(0, 7, 30, 0, 0), 8, false, 194, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 192, new TimeSpan(0, 21, 35, 0, 0), 6, false, 192, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 127, new TimeSpan(0, 20, 45, 0, 0), 4, false, 127, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 128, new TimeSpan(0, 23, 15, 0, 0), 4, false, 128, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 129, new TimeSpan(0, 16, 0, 0, 0), 8, false, 129, 4, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 130, new TimeSpan(0, 19, 30, 0, 0), 8, false, 130, 4, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 131, new TimeSpan(0, 22, 0, 0, 0), 8, false, 131, 4, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 132, new TimeSpan(0, 1, 30, 0, 0), 8, false, 132, 4, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 133, new TimeSpan(0, 13, 0, 0, 0), 4, false, 133, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 134, new TimeSpan(0, 16, 30, 0, 0), 4, false, 134, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 135, new TimeSpan(0, 19, 0, 0, 0), 4, false, 135, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 136, new TimeSpan(0, 22, 30, 0, 0), 4, false, 136, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 145, new TimeSpan(0, 6, 30, 0, 0), 7, false, 145, 5, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 146, new TimeSpan(0, 9, 0, 0, 0), 7, false, 146, 5, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 147, new TimeSpan(0, 12, 30, 0, 0), 7, false, 147, 5, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 148, new TimeSpan(0, 15, 0, 0, 0), 7, false, 148, 5, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 149, new TimeSpan(0, 9, 30, 0, 0), 5, false, 149, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 150, new TimeSpan(0, 12, 0, 0, 0), 5, false, 150, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 151, new TimeSpan(0, 15, 30, 0, 0), 5, false, 151, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 152, new TimeSpan(0, 18, 0, 0, 0), 5, false, 152, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 185, new TimeSpan(0, 9, 5, 0, 0), 9, false, 185, 6, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 186, new TimeSpan(0, 9, 35, 0, 0), 9, false, 186, 6, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 187, new TimeSpan(0, 9, 5, 0, 0), 9, false, 187, 6, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 188, new TimeSpan(0, 9, 35, 0, 0), 9, false, 188, 6, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 189, new TimeSpan(0, 12, 5, 0, 0), 6, false, 189, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 190, new TimeSpan(0, 15, 35, 0, 0), 6, false, 190, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 191, new TimeSpan(0, 18, 5, 0, 0), 6, false, 191, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 193, new TimeSpan(0, 4, 0, 0, 0), 8, false, 193, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 126, new TimeSpan(0, 17, 15, 0, 0), 4, false, 126, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 230, new TimeSpan(0, 15, 20, 0, 0), 9, false, 230, 10, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 232, new TimeSpan(0, 21, 20, 0, 0), 9, false, 232, 10, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 166, new TimeSpan(0, 16, 30, 0, 0), 5, false, 166, 9, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 167, new TimeSpan(0, 19, 0, 0, 0), 5, false, 167, 9, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 168, new TimeSpan(0, 22, 30, 0, 0), 5, false, 168, 9, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 169, new TimeSpan(0, 9, 45, 0, 0), 7, false, 169, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 170, new TimeSpan(0, 12, 15, 0, 0), 7, false, 170, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 171, new TimeSpan(0, 15, 45, 0, 0), 7, false, 171, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 172, new TimeSpan(0, 18, 15, 0, 0), 7, false, 171, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 173, new TimeSpan(0, 12, 45, 0, 0), 6, false, 173, 7, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 174, new TimeSpan(0, 15, 15, 0, 0), 6, false, 174, 7, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 175, new TimeSpan(0, 18, 45, 0, 0), 6, false, 175, 7, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 176, new TimeSpan(0, 21, 15, 0, 0), 6, false, 176, 7, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 177, new TimeSpan(0, 17, 0, 0, 0), 8, false, 177, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 178, new TimeSpan(0, 20, 30, 0, 0), 8, false, 178, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 179, new TimeSpan(0, 23, 0, 0, 0), 8, false, 179, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 180, new TimeSpan(0, 2, 30, 0, 0), 8, false, 180, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 181, new TimeSpan(0, 14, 0, 0, 0), 6, false, 181, 8, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 182, new TimeSpan(0, 17, 30, 0, 0), 6, false, 182, 8, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 183, new TimeSpan(0, 20, 0, 0, 0), 6, false, 183, 8, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 184, new TimeSpan(0, 23, 30, 0, 0), 6, false, 184, 8, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 209, new TimeSpan(0, 12, 50, 0, 0), 10, false, 209, 7, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 210, new TimeSpan(0, 15, 20, 0, 0), 10, false, 210, 7, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 211, new TimeSpan(0, 18, 50, 0, 0), 10, false, 211, 7, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 212, new TimeSpan(0, 21, 20, 0, 0), 10, false, 212, 7, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 213, new TimeSpan(0, 9, 50, 0, 0), 7, false, 213, 10, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 214, new TimeSpan(0, 12, 20, 0, 0), 7, false, 214, 10, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 165, new TimeSpan(0, 13, 0, 0, 0), 5, false, 165, 9, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 231, new TimeSpan(0, 18, 50, 0, 0), 9, false, 231, 10, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 164, new TimeSpan(0, 19, 30, 0, 0), 9, false, 164, 5, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 162, new TimeSpan(0, 13, 30, 0, 0), 9, false, 162, 5, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 65, new TimeSpan(0, 6, 30, 0, 0), 6, false, 65, 2, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 66, new TimeSpan(0, 9, 0, 0, 0), 6, false, 66, 2, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 67, new TimeSpan(0, 12, 30, 0, 0), 6, false, 67, 2, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 68, new TimeSpan(0, 15, 0, 0, 0), 6, false, 68, 2, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 69, new TimeSpan(0, 9, 30, 0, 0), 2, false, 69, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 70, new TimeSpan(0, 12, 0, 0, 0), 2, false, 70, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 71, new TimeSpan(0, 15, 30, 0, 0), 2, false, 71, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 72, new TimeSpan(0, 18, 0, 0, 0), 2, false, 72, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 137, new TimeSpan(0, 6, 30, 0, 0), 9, false, 137, 4, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 138, new TimeSpan(0, 9, 0, 0, 0), 9, false, 138, 4, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 139, new TimeSpan(0, 12, 30, 0, 0), 9, false, 139, 4, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 140, new TimeSpan(0, 15, 0, 0, 0), 9, false, 140, 4, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 141, new TimeSpan(0, 9, 30, 0, 0), 4, false, 141, 9, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 142, new TimeSpan(0, 12, 0, 0, 0), 4, false, 142, 9, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 143, new TimeSpan(0, 15, 30, 0, 0), 4, false, 143, 9, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 144, new TimeSpan(0, 18, 0, 0, 0), 4, false, 144, 9, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 153, new TimeSpan(0, 12, 50, 0, 0), 8, false, 153, 5, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 154, new TimeSpan(0, 15, 20, 0, 0), 8, false, 154, 5, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 155, new TimeSpan(0, 18, 50, 0, 0), 8, false, 155, 5, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 156, new TimeSpan(0, 21, 20, 0, 0), 8, false, 156, 5, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 157, new TimeSpan(0, 9, 50, 0, 0), 5, false, 157, 8, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 158, new TimeSpan(0, 12, 20, 0, 0), 5, false, 158, 8, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 159, new TimeSpan(0, 15, 50, 0, 0), 5, false, 159, 8, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 160, new TimeSpan(0, 18, 20, 0, 0), 5, false, 160, 8, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 161, new TimeSpan(0, 10, 0, 0, 0), 9, false, 161, 5, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 163, new TimeSpan(0, 16, 0, 0, 0), 9, false, 163, 5, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 125, new TimeSpan(0, 14, 45, 0, 0), 4, false, 125, 7, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 124, new TimeSpan(0, 23, 15, 0, 0), 7, false, 124, 4, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 123, new TimeSpan(0, 20, 45, 0, 0), 7, false, 123, 4, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 31, new TimeSpan(0, 20, 5, 0, 0), 1, false, 31, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 32, new TimeSpan(0, 23, 35, 0, 0), 1, false, 32, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 41, new TimeSpan(0, 13, 40, 0, 0), 3, false, 41, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 42, new TimeSpan(0, 16, 10, 0, 0), 3, false, 42, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 43, new TimeSpan(0, 19, 40, 0, 0), 3, false, 43, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 44, new TimeSpan(0, 22, 10, 0, 0), 3, false, 44, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 45, new TimeSpan(0, 16, 40, 0, 0), 2, false, 45, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 46, new TimeSpan(0, 19, 10, 0, 0), 2, false, 46, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 47, new TimeSpan(0, 22, 40, 0, 0), 2, false, 47, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 48, new TimeSpan(0, 1, 10, 0, 0), 2, false, 48, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 49, new TimeSpan(0, 14, 0, 0, 0), 4, false, 49, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 50, new TimeSpan(0, 17, 30, 0, 0), 4, false, 50, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 51, new TimeSpan(0, 20, 0, 0, 0), 4, false, 51, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 52, new TimeSpan(0, 23, 30, 0, 0), 4, false, 52, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 53, new TimeSpan(0, 11, 0, 0, 0), 2, false, 53, 4, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 54, new TimeSpan(0, 14, 30, 0, 0), 2, false, 54, 4, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 55, new TimeSpan(0, 17, 0, 0, 0), 2, false, 55, 4, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 56, new TimeSpan(0, 20, 30, 0, 0), 2, false, 56, 4, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 57, new TimeSpan(0, 9, 45, 0, 0), 5, false, 57, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 58, new TimeSpan(0, 12, 15, 0, 0), 5, false, 58, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 59, new TimeSpan(0, 15, 45, 0, 0), 5, false, 59, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 60, new TimeSpan(0, 18, 15, 0, 0), 5, false, 60, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 61, new TimeSpan(0, 6, 45, 0, 0), 2, false, 61, 5, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 62, new TimeSpan(0, 9, 15, 0, 0), 2, false, 62, 5, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 63, new TimeSpan(0, 12, 45, 0, 0), 2, false, 63, 5, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 30, new TimeSpan(0, 17, 35, 0, 0), 1, false, 30, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 64, new TimeSpan(0, 15, 15, 0, 0), 2, false, 64, 5, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 29, new TimeSpan(0, 14, 5, 0, 0), 1, false, 29, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 27, new TimeSpan(0, 23, 5, 0, 0), 7, false, 27, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 2, new TimeSpan(0, 9, 30, 0, 0), 2, false, 2, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 3, new TimeSpan(0, 12, 35, 0, 0), 2, false, 3, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 4, new TimeSpan(0, 15, 30, 0, 0), 2, false, 4, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 5, new TimeSpan(0, 9, 45, 0, 0), 1, false, 5, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 6, new TimeSpan(0, 12, 30, 0, 0), 1, false, 6, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 7, new TimeSpan(0, 15, 45, 0, 0), 1, false, 7, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 8, new TimeSpan(0, 18, 30, 0, 0), 1, false, 8, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 9, new TimeSpan(0, 8, 15, 0, 0), 3, false, 9, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 10, new TimeSpan(0, 11, 30, 0, 0), 3, false, 10, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 11, new TimeSpan(0, 14, 15, 0, 0), 3, false, 11, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 12, new TimeSpan(0, 17, 15, 0, 0), 3, false, 12, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 13, new TimeSpan(0, 11, 15, 0, 0), 1, false, 13, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 14, new TimeSpan(0, 14, 30, 0, 0), 1, false, 14, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 15, new TimeSpan(0, 17, 15, 0, 0), 1, false, 15, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 16, new TimeSpan(0, 20, 15, 0, 0), 1, false, 16, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 17, new TimeSpan(0, 15, 15, 0, 0), 4, false, 17, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 18, new TimeSpan(0, 18, 30, 0, 0), 4, false, 18, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 19, new TimeSpan(0, 21, 15, 0, 0), 4, false, 19, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 20, new TimeSpan(0, 0, 15, 0, 0), 4, false, 20, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 21, new TimeSpan(0, 12, 15, 0, 0), 1, false, 21, 4, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 22, new TimeSpan(0, 15, 30, 0, 0), 1, false, 22, 4, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 23, new TimeSpan(0, 18, 15, 0, 0), 1, false, 23, 4, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 24, new TimeSpan(0, 21, 15, 0, 0), 1, false, 24, 4, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 25, new TimeSpan(0, 17, 5, 0, 0), 7, false, 25, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 26, new TimeSpan(0, 20, 35, 0, 0), 7, false, 26, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 28, new TimeSpan(0, 2, 35, 0, 0), 7, false, 28, 1, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 73, new TimeSpan(0, 6, 0, 0, 0), 7, false, 73, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 74, new TimeSpan(0, 9, 30, 0, 0), 7, false, 74, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 75, new TimeSpan(0, 12, 0, 0, 0), 7, false, 75, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 90, new TimeSpan(0, 8, 10, 0, 0), 9, false, 90, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 91, new TimeSpan(0, 11, 40, 0, 0), 9, false, 91, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 92, new TimeSpan(0, 14, 10, 0, 0), 9, false, 92, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 93, new TimeSpan(0, 8, 40, 0, 0), 2, false, 93, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 94, new TimeSpan(0, 11, 10, 0, 0), 2, false, 94, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 95, new TimeSpan(0, 14, 40, 0, 0), 2, false, 95, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 96, new TimeSpan(0, 17, 10, 0, 0), 2, false, 96, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 105, new TimeSpan(0, 16, 30, 0, 0), 9, false, 105, 3, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 106, new TimeSpan(0, 19, 0, 0, 0), 9, false, 106, 3, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 107, new TimeSpan(0, 22, 30, 0, 0), 9, false, 107, 3, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 108, new TimeSpan(1, 1, 0, 0, 0), 9, false, 108, 3, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 109, new TimeSpan(0, 13, 30, 0, 0), 3, false, 109, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 110, new TimeSpan(0, 16, 0, 0, 0), 3, false, 110, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 111, new TimeSpan(0, 19, 30, 0, 0), 3, false, 111, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 112, new TimeSpan(0, 22, 0, 0, 0), 3, false, 112, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 113, new TimeSpan(0, 7, 0, 0, 0), 5, false, 113, 4, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 114, new TimeSpan(0, 10, 30, 0, 0), 5, false, 114, 4, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 115, new TimeSpan(0, 13, 0, 0, 0), 5, false, 115, 4, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 116, new TimeSpan(0, 16, 30, 0, 0), 5, false, 116, 4, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 117, new TimeSpan(0, 10, 0, 0, 0), 4, false, 117, 5, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 118, new TimeSpan(0, 13, 30, 0, 0), 4, false, 118, 5, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 119, new TimeSpan(0, 16, 0, 0, 0), 4, false, 119, 5, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 120, new TimeSpan(0, 19, 30, 0, 0), 4, false, 120, 5, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 121, new TimeSpan(0, 14, 45, 0, 0), 7, false, 121, 4, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 122, new TimeSpan(0, 17, 15, 0, 0), 7, false, 122, 4, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 89, new TimeSpan(0, 5, 40, 0, 0), 9, false, 89, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 88, new TimeSpan(0, 19, 15, 0, 0), 2, false, 88, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 87, new TimeSpan(0, 16, 45, 0, 0), 2, false, 87, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 86, new TimeSpan(0, 13, 15, 0, 0), 2, false, 86, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 76, new TimeSpan(0, 15, 30, 0, 0), 7, false, 76, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 77, new TimeSpan(0, 6, 0, 0, 0), 2, false, 77, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 78, new TimeSpan(0, 9, 30, 0, 0), 2, false, 78, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 79, new TimeSpan(0, 12, 0, 0, 0), 2, false, 79, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 80, new TimeSpan(0, 15, 30, 0, 0), 2, false, 80, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 97, new TimeSpan(0, 7, 5, 0, 0), 7, false, 97, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 98, new TimeSpan(0, 10, 45, 0, 0), 7, false, 98, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 99, new TimeSpan(0, 13, 5, 0, 0), 7, false, 99, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 100, new TimeSpan(0, 16, 45, 0, 0), 7, false, 100, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 101, new TimeSpan(0, 10, 5, 0, 0), 3, false, 101, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 102, new TimeSpan(0, 13, 45, 0, 0), 3, false, 102, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 103, new TimeSpan(0, 16, 5, 0, 0), 3, false, 103, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 215, new TimeSpan(0, 15, 50, 0, 0), 7, false, 215, 10, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 104, new TimeSpan(0, 19, 45, 0, 0), 3, false, 104, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 34, new TimeSpan(0, 14, 45, 0, 0), 9, false, 34, 1, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 35, new TimeSpan(0, 17, 15, 0, 0), 9, false, 35, 1, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 36, new TimeSpan(0, 20, 45, 0, 0), 9, false, 36, 1, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 37, new TimeSpan(0, 14, 15, 0, 0), 1, false, 37, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 38, new TimeSpan(0, 17, 45, 0, 0), 1, false, 38, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 39, new TimeSpan(0, 20, 15, 0, 0), 1, false, 39, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 40, new TimeSpan(0, 23, 45, 0, 0), 1, false, 40, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 81, new TimeSpan(0, 7, 45, 0, 0), 8, false, 81, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 82, new TimeSpan(0, 10, 15, 0, 0), 8, false, 82, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 83, new TimeSpan(0, 13, 45, 0, 0), 8, false, 83, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 84, new TimeSpan(0, 16, 45, 0, 0), 8, false, 84, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 85, new TimeSpan(0, 10, 45, 0, 0), 2, false, 85, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 33, new TimeSpan(0, 11, 15, 0, 0), 9, false, 33, 1, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 216, new TimeSpan(0, 18, 20, 0, 0), 7, false, 216, 10, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerDataId",
                table: "Bookings",
                column: "CustomerDataId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDatas_UserId",
                table: "CustomerDatas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestinationAirportId",
                table: "Flights",
                column: "DestinationAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_OriginAirportId",
                table: "Flights",
                column: "OriginAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PlaneId",
                table: "Flights",
                column: "PlaneId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledFlights_FlightId",
                table: "ScheduledFlights",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_BookingId",
                table: "Tickets",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ScheduledFlightId",
                table: "Tickets",
                column: "ScheduledFlightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "ScheduledFlights");

            migrationBuilder.DropTable(
                name: "CustomerDatas");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "Planes");
        }
    }
}
