using Microsoft.EntityFrameworkCore.Migrations;

namespace NonconformityControl.Migrations
{
    public partial class ActionNonconformityRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Nonconformities_NonconformityId",
                table: "Actions");

            migrationBuilder.AlterColumn<int>(
                name: "NonconformityId",
                table: "Actions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Nonconformities_NonconformityId",
                table: "Actions",
                column: "NonconformityId",
                principalTable: "Nonconformities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Nonconformities_NonconformityId",
                table: "Actions");

            migrationBuilder.AlterColumn<int>(
                name: "NonconformityId",
                table: "Actions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Nonconformities_NonconformityId",
                table: "Actions",
                column: "NonconformityId",
                principalTable: "Nonconformities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
