using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenChequeBack.Persistence.Migrations
{
    public partial class sec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("114998e2-6830-454a-95d9-8f87fc4c4a6e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ac558e1c-b1e2-4be2-b98c-19f231c7a01c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f0d3a781-2901-4c75-a28b-17b0f45198ca"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("b221a4f1-645f-4d5d-8a2a-36d996973af8"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

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

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[,]
                {
                    { new Guid("114998e2-6830-454a-95d9-8f87fc4c4a6e"), true, new DateTime(2023, 11, 29, 23, 40, 15, 725, DateTimeKind.Local).AddTicks(606), 1, new Guid("f0d3a781-2901-4c75-a28b-17b0f45198ca"), new DateTime(2023, 11, 29, 23, 40, 15, 725, DateTimeKind.Local).AddTicks(608), 1, "الکترونیکی", true },
                    { new Guid("ac558e1c-b1e2-4be2-b98c-19f231c7a01c"), true, new DateTime(2023, 11, 29, 23, 40, 15, 725, DateTimeKind.Local).AddTicks(627), 1, new Guid("f0d3a781-2901-4c75-a28b-17b0f45198ca"), new DateTime(2023, 11, 29, 23, 40, 15, 725, DateTimeKind.Local).AddTicks(629), 1, "غذایی", true },
                    { new Guid("f0d3a781-2901-4c75-a28b-17b0f45198ca"), true, new DateTime(2023, 11, 29, 23, 40, 15, 725, DateTimeKind.Local).AddTicks(577), 1, null, new DateTime(2023, 11, 29, 23, 40, 15, 725, DateTimeKind.Local).AddTicks(579), 1, "محصولات", true }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "Name", "QuantityPerUnit", "RegisterDate", "RegisterUser", "Visable" },
                values: new object[] { new Guid("b221a4f1-645f-4d5d-8a2a-36d996973af8"), true, new DateTime(2023, 11, 29, 23, 40, 15, 725, DateTimeKind.Local).AddTicks(353), 1, "بسته", 10, new DateTime(2023, 11, 29, 23, 40, 15, 725, DateTimeKind.Local).AddTicks(367), 1, true });
        }
    }
}
