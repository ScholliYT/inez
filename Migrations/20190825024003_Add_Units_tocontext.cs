using Microsoft.EntityFrameworkCore.Migrations;

namespace INEZ.Migrations
{
    public partial class Add_Units_tocontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Unit_UnitName",
                table: "Quantity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Units_UnitName",
                table: "Quantity",
                column: "UnitName",
                principalTable: "Units",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Units_UnitName",
                table: "Quantity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Unit_UnitName",
                table: "Quantity",
                column: "UnitName",
                principalTable: "Unit",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
