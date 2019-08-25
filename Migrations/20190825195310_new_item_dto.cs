using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace INEZ.Migrations
{
    public partial class new_item_dto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Quantity_BaseQuantityId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Quantity");

            migrationBuilder.DropIndex(
                name: "IX_Items_BaseQuantityId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BaseQuantityId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Items",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "Brands",
                table: "Items",
                maxLength: 127,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DatasourceUrl",
                table: "Items",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EAN",
                table: "Items",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "GenericName",
                table: "Items",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSmallUrl",
                table: "Items",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Items",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "Items",
                maxLength: 127,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brands",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DatasourceUrl",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "EAN",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GenericName",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ImageSmallUrl",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Items",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<Guid>(
                name: "BaseQuantityId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Items",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Quantity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quantity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_BaseQuantityId",
                table: "Items",
                column: "BaseQuantityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Quantity_BaseQuantityId",
                table: "Items",
                column: "BaseQuantityId",
                principalTable: "Quantity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
