using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartWork.Migrations
{
    public partial class companyofficeandroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Company_CompanyId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_CompanyId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Room");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Room",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Room");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Room_CompanyId",
                table: "Room",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Company_CompanyId",
                table: "Room",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
