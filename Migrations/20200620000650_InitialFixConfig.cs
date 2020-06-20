using Microsoft.EntityFrameworkCore.Migrations;

namespace NonconformityControl.Migrations
{
    public partial class InitialFixConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<sbyte>(
                name: "Status",
                table: "Nonconformities",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<sbyte>(
                name: "Evaluation",
                table: "Nonconformities",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Nonconformities",
                type: "VARCHAR(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Nonconformities",
                type: "VARCHAR(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Actions",
                type: "VARCHAR(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Nonconformities",
                type: "int",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "TINYINT");

            migrationBuilder.AlterColumn<int>(
                name: "Evaluation",
                table: "Nonconformities",
                type: "int",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "TINYINT");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Nonconformities",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Nonconformities",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(15)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Actions",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1024)",
                oldMaxLength: 1024);
        }
    }
}
