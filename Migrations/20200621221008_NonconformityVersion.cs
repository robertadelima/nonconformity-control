using Microsoft.EntityFrameworkCore.Migrations;

namespace NonconformityControl.Migrations
{
    public partial class NonconformityVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Nonconformities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Nonconformities");
        }
    }
}
