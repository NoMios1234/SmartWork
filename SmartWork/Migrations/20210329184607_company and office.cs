using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartWork.Migrations
{
    public partial class companyandoffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
              name: "FK_Office_Company_CompanyId",
              table: "Office");

            migrationBuilder.AddForeignKey(
               name: "FK_Office_Company_CompanyId",
               table: "Office",
               column: "CompanyId",
               principalTable: "Company",
               principalColumn: "Id",
               onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
