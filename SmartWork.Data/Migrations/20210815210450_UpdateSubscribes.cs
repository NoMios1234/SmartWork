using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartWork.Data.Migrations
{
    public partial class UpdateSubscribes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VisitStatisticName",
                table: "VisitStatistic",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "VisitStatistic",
                newName: "VisitStatisticName");
        }
    }
}
