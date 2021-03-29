using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartWork.Migrations
{
    public partial class officeandcompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Office_OfficeId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "OfficeId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Room",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OfficePhoneNumber",
                table: "Office",
                type: "nvarchar(max)",
                nullable: true);           

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Office_OfficeId",
                table: "Room",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Company_CompanyId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Office_OfficeId",
                table: "Room");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Room_CompanyId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "OfficePhoneNumber",
                table: "Office");

            migrationBuilder.AlterColumn<int>(
                name: "OfficeId",
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
                name: "FK_Room_Office_OfficeId",
                table: "Room",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);        
        }
    }
}
