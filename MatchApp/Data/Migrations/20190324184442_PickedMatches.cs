using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PickedMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PickedMatches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventID = table.Column<string>(nullable: true),
                    Sport = table.Column<string>(nullable: true),
                    HomeTeam = table.Column<string>(nullable: true),
                    AwayTeam = table.Column<string>(nullable: true),
                    Score = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StakeValueOne = table.Column<double>(nullable: false),
                    SelectedStakeValueOne = table.Column<double>(nullable: false),
                    StakeValueX = table.Column<double>(nullable: false),
                    SelectedStakeValueX = table.Column<double>(nullable: false),
                    StakeValueTwo = table.Column<double>(nullable: false),
                    SelectedStakeValueTwo = table.Column<double>(nullable: false),
                    IsBlocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickedMatches", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PickedMatches");
        }
    }
}
