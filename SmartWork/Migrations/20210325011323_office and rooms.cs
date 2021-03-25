using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartWork.Migrations
{
    public partial class officeandrooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            

         

            migrationBuilder.CreateTable(
                name: "Subscribe",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribe", x => x.id);
                });

           

           
            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    officeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    officeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isFavourite = table.Column<bool>(type: "bit", nullable: false),
                    subscribeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.id);
                    table.ForeignKey(
                        name: "FK_Office_Subscribe_subscribeId",
                        column: x => x.subscribeId,
                        principalTable: "Subscribe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

          
            migrationBuilder.CreateIndex(
                name: "IX_Office_subscribeId",
                table: "Office",
                column: "subscribeId");
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "Office");

           
            migrationBuilder.DropTable(
                name: "Subscribe");

        }
    }
}
