using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace INEZ.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Quantity = table.Column<string>(maxLength: 127, nullable: true),
                    GenericName = table.Column<string>(maxLength: 255, nullable: true),
                    Brands = table.Column<string>(maxLength: 127, nullable: true),
                    EAN = table.Column<decimal>(nullable: false),
                    DatasourceUrl = table.Column<string>(maxLength: 255, nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 255, nullable: true),
                    ImageSmallUrl = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Quantity = table.Column<string>(maxLength: 127, nullable: true),
                    OwnerId = table.Column<string>(nullable: true),
                    CreationTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Checked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ShoppingListItems");
        }
    }
}
