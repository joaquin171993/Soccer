using Microsoft.EntityFrameworkCore.Migrations;

namespace Soccer.Web.DataAccess.Migrations
{
    public partial class addCampoNumberMatchInTableMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberMatch",
                table: "Matches",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberMatch",
                table: "Matches");
        }
    }
}
