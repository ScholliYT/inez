using Microsoft.EntityFrameworkCore.Migrations;

namespace INEZ.Migrations
{
    public partial class Move_Unit_to_Quantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Units_UnitName",
                table: "Quantity");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Quantity_UnitName",
                table: "Quantity");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Quantity",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "Quantity",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quantity_UnitName",
                table: "Quantity",
                column: "UnitName");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Units_UnitName",
                table: "Quantity",
                column: "UnitName",
                principalTable: "Units",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
