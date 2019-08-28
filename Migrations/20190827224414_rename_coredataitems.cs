using Microsoft.EntityFrameworkCore.Migrations;

namespace INEZ.Migrations
{
    public partial class rename_coredataitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "CoreDataItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoreDataItems",
                table: "CoreDataItems",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoreDataItems",
                table: "CoreDataItems");

            migrationBuilder.RenameTable(
                name: "CoreDataItems",
                newName: "Items");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");
        }
    }
}
