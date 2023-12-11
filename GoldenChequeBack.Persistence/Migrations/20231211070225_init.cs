using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenChequeBack.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseInfoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StringValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntValue = table.Column<int>(type: "int", nullable: true),
                    LongValue = table.Column<long>(type: "bigint", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseInfoes", x => x.Id);
                });

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
                name: "CustomerRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityPerUnit = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shobes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shobes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shobes_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OstanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_States_OstanId",
                        column: x => x.OstanId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WareHouseStock = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Mob1 = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Mob2 = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Mob3 = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaxBuyPrice = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerRates_CustomerRateId",
                        column: x => x.CustomerRateId,
                        principalTable: "CustomerRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorSumPrice = table.Column<long>(type: "bigint", nullable: false),
                    FactorSodDarsad = table.Column<int>(type: "int", nullable: false),
                    FactorKharidDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FactorSumObjectsPrice = table.Column<long>(type: "bigint", nullable: false),
                    Kind = table.Column<int>(type: "int", nullable: false),
                    FactorBeforePrice = table.Column<long>(type: "bigint", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factors_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cheques",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kind = table.Column<int>(type: "int", nullable: false),
                    ShomareHesab = table.Column<int>(type: "int", nullable: false),
                    ShomareChek = table.Column<int>(type: "int", nullable: false),
                    SahabChequeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShobeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChequeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChequeStatus = table.Column<int>(type: "int", nullable: false),
                    PassDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChequePrice = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cheques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cheques_Customers_SahabChequeId",
                        column: x => x.SahabChequeId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cheques_Factors_FactorId",
                        column: x => x.FactorId,
                        principalTable: "Factors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cheques_Shobes_ShobeId",
                        column: x => x.ShobeId,
                        principalTable: "Shobes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorObjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorObjects_Factors_FactorId",
                        column: x => x.FactorId,
                        principalTable: "Factors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ghests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FactorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<int>(type: "int", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeUser = table.Column<int>(type: "int", nullable: false),
                    Visable = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ghests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ghests_Factors_FactorId",
                        column: x => x.FactorId,
                        principalTable: "Factors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "ParentId", "RegisterDate", "RegisterUser", "Title", "Visable" },
                values: new object[,]
                {
                    { new Guid("48ecb5fd-d805-451e-a92d-31a5b96ae942"), true, new DateTime(2023, 12, 11, 10, 32, 24, 984, DateTimeKind.Local).AddTicks(4112), 1, new Guid("b2c4adc5-4ddf-4458-b4de-f471acf3fa89"), new DateTime(2023, 12, 11, 10, 32, 24, 984, DateTimeKind.Local).AddTicks(4112), 1, "غذایی", true },
                    { new Guid("b2c4adc5-4ddf-4458-b4de-f471acf3fa89"), true, new DateTime(2023, 12, 11, 10, 32, 24, 984, DateTimeKind.Local).AddTicks(4089), 1, null, new DateTime(2023, 12, 11, 10, 32, 24, 984, DateTimeKind.Local).AddTicks(4090), 1, "محصولات", true },
                    { new Guid("d5fe20a4-5718-4592-b51b-85c5ae58e8e5"), true, new DateTime(2023, 12, 11, 10, 32, 24, 984, DateTimeKind.Local).AddTicks(4102), 1, new Guid("b2c4adc5-4ddf-4458-b4de-f471acf3fa89"), new DateTime(2023, 12, 11, 10, 32, 24, 984, DateTimeKind.Local).AddTicks(4103), 1, "الکترونیکی", true }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Author", "LastChangeDate", "LastChangeUser", "Name", "QuantityPerUnit", "RegisterDate", "RegisterUser", "Visable" },
                values: new object[] { new Guid("ef81d444-9062-496c-a641-c26d21ac7833"), true, new DateTime(2023, 12, 11, 10, 32, 24, 984, DateTimeKind.Local).AddTicks(3913), 1, "بسته", 10, new DateTime(2023, 12, 11, 10, 32, 24, 984, DateTimeKind.Local).AddTicks(3923), 1, true });

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_FactorId",
                table: "Cheques",
                column: "FactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_SahabChequeId",
                table: "Cheques",
                column: "SahabChequeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_ShobeId",
                table: "Cheques",
                column: "ShobeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_OstanId",
                table: "Cities",
                column: "OstanId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CityId",
                table: "Customers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerRateId",
                table: "Customers",
                column: "CustomerRateId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorObjects_FactorId",
                table: "FactorObjects",
                column: "FactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Factors_CustomerId",
                table: "Factors",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ghests_FactorId",
                table: "Ghests",
                column: "FactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageId",
                table: "Products",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Shobes_BankId",
                table: "Shobes",
                column: "BankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseInfoes");

            migrationBuilder.DropTable(
                name: "Cheques");

            migrationBuilder.DropTable(
                name: "FactorObjects");

            migrationBuilder.DropTable(
                name: "Ghests");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Shobes");

            migrationBuilder.DropTable(
                name: "Factors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "CustomerRates");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
