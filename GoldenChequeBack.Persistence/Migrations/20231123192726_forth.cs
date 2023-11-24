using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenChequeBack.Persistence.Migrations
{
    public partial class forth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Objects");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WareHouseStock = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("033ff71b-049b-4ce5-9120-ce2d8e8cf483"), true, new DateTime(2023, 11, 23, 22, 57, 25, 746, DateTimeKind.Local).AddTicks(9248), 1, new Guid("13e3a228-200c-41db-b9fe-1cb96edb46e2"), new DateTime(2023, 11, 23, 22, 57, 25, 746, DateTimeKind.Local).AddTicks(9250), 1, "الکترونیکی", true });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("13e3a228-200c-41db-b9fe-1cb96edb46e2"), true, new DateTime(2023, 11, 23, 22, 57, 25, 746, DateTimeKind.Local).AddTicks(9187), 1, null, new DateTime(2023, 11, 23, 22, 57, 25, 746, DateTimeKind.Local).AddTicks(9200), 1, "محصولات", true });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("c72cd428-c8b7-413e-8ffc-ba43205e8aef"), true, new DateTime(2023, 11, 23, 22, 57, 25, 746, DateTimeKind.Local).AddTicks(9271), 1, new Guid("13e3a228-200c-41db-b9fe-1cb96edb46e2"), new DateTime(2023, 11, 23, 22, 57, 25, 746, DateTimeKind.Local).AddTicks(9273), 1, "غذایی", true });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("033ff71b-049b-4ce5-9120-ce2d8e8cf483"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("13e3a228-200c-41db-b9fe-1cb96edb46e2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c72cd428-c8b7-413e-8ffc-ba43205e8aef"));

            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false),
                    BuyPrice = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    WareHouseStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objects_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Objects_UnitId",
                table: "Objects",
                column: "UnitId");
        }
    }
}
