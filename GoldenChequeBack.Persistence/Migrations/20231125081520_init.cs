using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenChequeBack.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Objects");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

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
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                values: new object[] { new Guid("3e6812a8-a937-46f8-8bb5-15e2c22ef6b2"), true, new DateTime(2023, 11, 25, 11, 45, 20, 186, DateTimeKind.Local).AddTicks(8618), 1, new Guid("d6c78b97-691c-4110-8f56-77ebdc82d1d0"), new DateTime(2023, 11, 25, 11, 45, 20, 186, DateTimeKind.Local).AddTicks(8619), 1, "الکترونیکی", true });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("d6c78b97-691c-4110-8f56-77ebdc82d1d0"), true, new DateTime(2023, 11, 25, 11, 45, 20, 186, DateTimeKind.Local).AddTicks(8559), 1, null, new DateTime(2023, 11, 25, 11, 45, 20, 186, DateTimeKind.Local).AddTicks(8577), 1, "محصولات", true });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[] { new Guid("e4c4391f-60a1-488c-ac7d-8c78c4b99f76"), true, new DateTime(2023, 11, 25, 11, 45, 20, 186, DateTimeKind.Local).AddTicks(8629), 1, new Guid("d6c78b97-691c-4110-8f56-77ebdc82d1d0"), new DateTime(2023, 11, 25, 11, 45, 20, 186, DateTimeKind.Local).AddTicks(8630), 1, "غذایی", true });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

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
