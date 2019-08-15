using Microsoft.EntityFrameworkCore.Migrations;

namespace INEZ.Migrations
{
    public partial class Add_TestModel_context : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_TestModel_TestId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestModel",
                table: "TestModel");

            migrationBuilder.RenameTable(
                name: "TestModel",
                newName: "TestModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestModels",
                table: "TestModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_TestModels_TestId",
                table: "Items",
                column: "TestId",
                principalTable: "TestModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_TestModels_TestId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestModels",
                table: "TestModels");

            migrationBuilder.RenameTable(
                name: "TestModels",
                newName: "TestModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestModel",
                table: "TestModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_TestModel_TestId",
                table: "Items",
                column: "TestId",
                principalTable: "TestModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
