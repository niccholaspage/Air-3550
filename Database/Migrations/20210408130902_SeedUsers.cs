// 20210408130902_SeedUsers.cs - Air 3550 Project
//
// This program emulates a flight reservation system for a new airline,
// allowing customers to book and manage trips,as well as allowing employees
// to update the available flights and view stats on them.
//
// Authors:		Nicholas Nassar, Jacob Hammitte, Nikesh Dhital
// Class:		EECS 3550-001 Software Engineering, Spring 2021
// Instructor:	Dr. Thomas
// Date:		April 28, 2021
// Copyright:	Copyright 2021 by Nicholas Nassar, Jacob Hammitte, and Nikesh Dhital. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { 3, "flight_manager", "4iifPzpmqB9f+1Lf8aCc0q6Ro56ySCMNKQcIRnnGus20qIDaeDXKcgA9jDfhB8+Rxfl5VngwN1Trob5CA5v/TQ==", 3 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "LoginId", "PasswordHash", "Role" },
                values: new object[] { 4, "marketing_manager", "NgyuvZ7baGCcCTO63jVlNQ5Z4oTMUDzmG/DuvUL7flvWV6ce0UmCJRaHV+fxCVkgQRzOJ3eeDHeOxSU13q4gQA==", 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);
        }
    }
}
