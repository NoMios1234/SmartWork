using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartWork.Migrations
{
    public partial class roomandcompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Company_CompanyId",
                table: "Room");


            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Company_CompanyId",
                table: "Room",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetDefault);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Company_CompanyId",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Room",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Room",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Company_CompanyId",
                table: "Room",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
