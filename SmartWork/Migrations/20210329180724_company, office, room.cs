using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartWork.Migrations
{
    public partial class companyofficeroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Office",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Office_CompanyId",
                table: "Office",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Office_Company_CompanyId",
                table: "Office",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

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
            migrationBuilder.DropForeignKey(
                name: "FK_Office_Company_CompanyId",
                table: "Office");

            migrationBuilder.DropIndex(
                name: "IX_Office_CompanyId",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Office");
        }
    }
}
