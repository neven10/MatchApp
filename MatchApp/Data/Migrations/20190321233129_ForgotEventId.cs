using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ForgotEventId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventID",
                table: "MatchTimes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventID",
                table: "MatchTimes");
        }
    }
}
