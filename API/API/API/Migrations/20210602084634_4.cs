using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Questionpools");

            migrationBuilder.DropColumn(
                name: "ShareMethod",
                table: "Questionpools");

            migrationBuilder.DropColumn(
                name: "QuizCode",
                table: "AccountinLessons");

            migrationBuilder.AddColumn<string>(
                name: "ExamQuizCode",
                table: "AccountinLessons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamQuizCode",
                table: "AccountinLessons");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Questionpools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShareMethod",
                table: "Questionpools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuizCode",
                table: "AccountinLessons",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
