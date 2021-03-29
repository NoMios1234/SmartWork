using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartWork.Migrations
{
    public partial class dropcasscadecompanyidinroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Company_CompanyId",
                table: "Room");

            migrationBuilder.AddForeignKey(
               name: "FK_Room_Company_CompanyId",
               table: "Room",
               column: "CompanyId",
               principalTable: "Company",
               principalColumn: "Id",
               onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
