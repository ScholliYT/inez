using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace INEZ.Migrations
{
    public partial class Add_TestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TestModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TestProp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_TestId",
                table: "Items",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_TestModel_TestId",
                table: "Items",
                column: "TestId",
                principalTable: "TestModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_TestModel_TestId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "TestModel");

            migrationBuilder.DropIndex(
                name: "IX_Items_TestId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Items");
        }
    }
}
