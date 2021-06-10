using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamCode",
                table: "ExamQuizs");

            migrationBuilder.AddColumn<string>(
                name: "ExamQuizCode",
                table: "ExamQuizs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamQuizCode",
                table: "ExamQuizs");

            migrationBuilder.AddColumn<string>(
                name: "ExamCode",
                table: "ExamQuizs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
