using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenChequeBack.Persistence.Migrations
{
    public partial class forth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("6f8694dc-3302-4df2-9303-2a172543cb39"), true, new DateTime(2023, 11, 22, 11, 40, 43, 133, DateTimeKind.Local).AddTicks(3540), 1, new Guid("ed6113b4-0cc2-44fa-ae12-9e00699fffd2"), new DateTime(2023, 11, 22, 11, 40, 43, 133, DateTimeKind.Local).AddTicks(3541), 1, "غذایی", true });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("c85092ac-d6e6-4c43-a9bf-1bb86949d285"), true, new DateTime(2023, 11, 22, 11, 40, 43, 133, DateTimeKind.Local).AddTicks(3526), 1, new Guid("ed6113b4-0cc2-44fa-ae12-9e00699fffd2"), new DateTime(2023, 11, 22, 11, 40, 43, 133, DateTimeKind.Local).AddTicks(3527), 1, "الکترونیکی", true });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("ed6113b4-0cc2-44fa-ae12-9e00699fffd2"), true, new DateTime(2023, 11, 22, 11, 40, 43, 133, DateTimeKind.Local).AddTicks(3483), 1, null, new DateTime(2023, 11, 22, 11, 40, 43, 133, DateTimeKind.Local).AddTicks(3494), 1, "محصولات", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6f8694dc-3302-4df2-9303-2a172543cb39"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c85092ac-d6e6-4c43-a9bf-1bb86949d285"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ed6113b4-0cc2-44fa-ae12-9e00699fffd2"));
        }
    }
}
