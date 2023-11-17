using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenChequeBack.Persistence.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objects_FactorObjects_FactorObjectsId",
                table: "Objects");

            migrationBuilder.DropForeignKey(
                name: "FK_Shobes_Banks_BankId",
                table: "Shobes");

            migrationBuilder.DropIndex(
                name: "IX_Objects_FactorObjectsId",
                table: "Objects");

            migrationBuilder.DropColumn(
                name: "FactorObjectsId",
                table: "Objects");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "FactorObjects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "FactorObjects");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "FactorObjects");

            migrationBuilder.DropColumn(
                name: "Sum",
                table: "FactorObjects");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Shobes",
                newName: "Code");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Shobes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Shobes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "BankId",
                table: "Shobes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Shobes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Shobes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Shobes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Shobes_Banks_BankId",
                table: "Shobes",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shobes_Banks_BankId",
                table: "Shobes");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Shobes");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Shobes");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Shobes");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Shobes",
                newName: "code");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Shobes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "code",
                table: "Shobes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "BankId",
                table: "Shobes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "FactorObjectsId",
                table: "Objects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "FactorObjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FactorObjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "FactorObjects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Sum",
                table: "FactorObjects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Objects_FactorObjectsId",
                table: "Objects",
                column: "FactorObjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objects_FactorObjects_FactorObjectsId",
                table: "Objects",
                column: "FactorObjectsId",
                principalTable: "FactorObjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shobes_Banks_BankId",
                table: "Shobes",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id");
        }
    }
}
