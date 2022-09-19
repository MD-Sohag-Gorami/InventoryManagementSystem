using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Product",
                newName: "SellPrice");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateOn",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProductQnty",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "WareHouseId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WareHouseModelId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_WareHouseModelId",
                table: "Product",
                column: "WareHouseModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_WareHouse_WareHouseModelId",
                table: "Product",
                column: "WareHouseModelId",
                principalTable: "WareHouse",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_WareHouse_WareHouseModelId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_WareHouseModelId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreateDateOn",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductQnty",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "WareHouseId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "WareHouseModelId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "SellPrice",
                table: "Product",
                newName: "Price");
        }
    }
}
