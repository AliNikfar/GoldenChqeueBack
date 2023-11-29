using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenChequeBack.Persistence.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0dd3c5eb-53f6-4af5-b025-76c3706f3d9f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("259503b4-3e39-4fee-856a-a76b91fc7021"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8c948cf2-cf09-4368-bcd8-0733e195175e"));

            migrationBuilder.DropColumn(
                name: "ImageExtention",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("1f73bf1d-8354-41ef-811a-6b2954ae4331"), true, new DateTime(2023, 11, 29, 18, 16, 16, 983, DateTimeKind.Local).AddTicks(8998), 1, null, new DateTime(2023, 11, 29, 18, 16, 16, 983, DateTimeKind.Local).AddTicks(9027), 1, "محصولات", true });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("6d3bc212-8345-44d9-a9c4-05477c2f3bf5"), true, new DateTime(2023, 11, 29, 18, 16, 16, 983, DateTimeKind.Local).AddTicks(9057), 1, new Guid("1f73bf1d-8354-41ef-811a-6b2954ae4331"), new DateTime(2023, 11, 29, 18, 16, 16, 983, DateTimeKind.Local).AddTicks(9059), 1, "الکترونیکی", true });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("80bbac10-6bfe-437c-8f28-e124b4327a1e"), true, new DateTime(2023, 11, 29, 18, 16, 16, 983, DateTimeKind.Local).AddTicks(9070), 1, new Guid("1f73bf1d-8354-41ef-811a-6b2954ae4331"), new DateTime(2023, 11, 29, 18, 16, 16, 983, DateTimeKind.Local).AddTicks(9071), 1, "غذایی", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1f73bf1d-8354-41ef-811a-6b2954ae4331"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6d3bc212-8345-44d9-a9c4-05477c2f3bf5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("80bbac10-6bfe-437c-8f28-e124b4327a1e"));

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ImageExtention",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("0dd3c5eb-53f6-4af5-b025-76c3706f3d9f"), true, new DateTime(2023, 11, 28, 16, 54, 7, 280, DateTimeKind.Local).AddTicks(7665), 1, new Guid("259503b4-3e39-4fee-856a-a76b91fc7021"), new DateTime(2023, 11, 28, 16, 54, 7, 280, DateTimeKind.Local).AddTicks(7666), 1, "غذایی", true });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("259503b4-3e39-4fee-856a-a76b91fc7021"), true, new DateTime(2023, 11, 28, 16, 54, 7, 280, DateTimeKind.Local).AddTicks(7613), 1, null, new DateTime(2023, 11, 28, 16, 54, 7, 280, DateTimeKind.Local).AddTicks(7622), 1, "محصولات", true });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("8c948cf2-cf09-4368-bcd8-0733e195175e"), true, new DateTime(2023, 11, 28, 16, 54, 7, 280, DateTimeKind.Local).AddTicks(7654), 1, new Guid("259503b4-3e39-4fee-856a-a76b91fc7021"), new DateTime(2023, 11, 28, 16, 54, 7, 280, DateTimeKind.Local).AddTicks(7655), 1, "الکترونیکی", true });
        }
    }
}
