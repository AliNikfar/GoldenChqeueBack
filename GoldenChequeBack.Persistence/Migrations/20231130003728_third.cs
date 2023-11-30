using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenChequeBack.Persistence.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3ceed5e9-c185-4808-91b9-27542ca1c775"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b950323-624a-4376-8922-2b4c79c24c42"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b8a93015-a2ae-47fa-a09d-3fb911726914"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("44a795fd-d134-416f-b3f5-3bdf7ea69ac6"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[,]
                {
                    { new Guid("13109421-3994-4530-9aea-3094046fb63e"), true, new DateTime(2023, 11, 30, 4, 7, 27, 949, DateTimeKind.Local).AddTicks(5307), 1, new Guid("95a04c7c-781c-4eaf-a713-5f923dd6ed09"), new DateTime(2023, 11, 30, 4, 7, 27, 949, DateTimeKind.Local).AddTicks(5310), 1, "الکترونیکی", true },
                    { new Guid("8f8c943e-6031-4721-8b47-7bab4216bb0e"), true, new DateTime(2023, 11, 30, 4, 7, 27, 949, DateTimeKind.Local).AddTicks(5334), 1, new Guid("95a04c7c-781c-4eaf-a713-5f923dd6ed09"), new DateTime(2023, 11, 30, 4, 7, 27, 949, DateTimeKind.Local).AddTicks(5335), 1, "غذایی", true },
                    { new Guid("95a04c7c-781c-4eaf-a713-5f923dd6ed09"), true, new DateTime(2023, 11, 30, 4, 7, 27, 949, DateTimeKind.Local).AddTicks(5271), 1, null, new DateTime(2023, 11, 30, 4, 7, 27, 949, DateTimeKind.Local).AddTicks(5274), 1, "محصولات", true }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "Name", "QuantityPerUnit", "RegisterDate", "RegisterUser", "Visable" },
                values: new object[] { new Guid("20732e16-ffc9-44be-bb82-6df798ca7296"), true, new DateTime(2023, 11, 30, 4, 7, 27, 949, DateTimeKind.Local).AddTicks(5004), 1, "بسته", 10, new DateTime(2023, 11, 30, 4, 7, 27, 949, DateTimeKind.Local).AddTicks(5020), 1, true });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageId",
                table: "Products",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ImageId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("13109421-3994-4530-9aea-3094046fb63e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8f8c943e-6031-4721-8b47-7bab4216bb0e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("95a04c7c-781c-4eaf-a713-5f923dd6ed09"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("20732e16-ffc9-44be-bb82-6df798ca7296"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[,]
                {
                    { new Guid("3ceed5e9-c185-4808-91b9-27542ca1c775"), true, new DateTime(2023, 11, 30, 0, 32, 56, 880, DateTimeKind.Local).AddTicks(3734), 1, new Guid("6b950323-624a-4376-8922-2b4c79c24c42"), new DateTime(2023, 11, 30, 0, 32, 56, 880, DateTimeKind.Local).AddTicks(3736), 1, "الکترونیکی", true },
                    { new Guid("6b950323-624a-4376-8922-2b4c79c24c42"), true, new DateTime(2023, 11, 30, 0, 32, 56, 880, DateTimeKind.Local).AddTicks(3698), 1, null, new DateTime(2023, 11, 30, 0, 32, 56, 880, DateTimeKind.Local).AddTicks(3701), 1, "محصولات", true },
                    { new Guid("b8a93015-a2ae-47fa-a09d-3fb911726914"), true, new DateTime(2023, 11, 30, 0, 32, 56, 880, DateTimeKind.Local).AddTicks(3762), 1, new Guid("6b950323-624a-4376-8922-2b4c79c24c42"), new DateTime(2023, 11, 30, 0, 32, 56, 880, DateTimeKind.Local).AddTicks(3764), 1, "غذایی", true }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "Name", "QuantityPerUnit", "RegisterDate", "RegisterUser", "Visable" },
                values: new object[] { new Guid("44a795fd-d134-416f-b3f5-3bdf7ea69ac6"), true, new DateTime(2023, 11, 30, 0, 32, 56, 880, DateTimeKind.Local).AddTicks(3441), 1, "بسته", 10, new DateTime(2023, 11, 30, 0, 32, 56, 880, DateTimeKind.Local).AddTicks(3458), 1, true });
        }
    }
}
