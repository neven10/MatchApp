using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class GuidToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Guid",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "StakeValueChanged",
                table: "Stakes",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "StakeValue",
                table: "Stakes",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Guid",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "StakeValueChanged",
                table: "Stakes",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "StakeValue",
                table: "Stakes",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
