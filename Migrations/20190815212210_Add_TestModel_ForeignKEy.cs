using Microsoft.EntityFrameworkCore.Migrations;

namespace INEZ.Migrations
{
    public partial class Add_TestModel_ForeignKEy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_TestModels_TestId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_TestId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestModels",
                table: "TestModels");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "TestModels",
                newName: "TestModel");

            migrationBuilder.AddColumn<int>(
                name: "ItemTest",
                table: "Items",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestModel",
                table: "TestModel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTest",
                table: "Items",
                column: "ItemTest");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_TestModel_ItemTest",
                table: "Items",
                column: "ItemTest",
                principalTable: "TestModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_TestModel_ItemTest",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemTest",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestModel",
                table: "TestModel");

            migrationBuilder.DropColumn(
                name: "ItemTest",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "TestModel",
                newName: "TestModels");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestModels",
                table: "TestModels",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TestId",
                table: "Items",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_TestModels_TestId",
                table: "Items",
                column: "TestId",
                principalTable: "TestModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
