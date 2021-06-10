using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonCompleted",
                table: "AccountinCourses");

            migrationBuilder.AddColumn<int>(
                name: "QuizCompleted",
                table: "AccountinCourses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizCompleted",
                table: "AccountinCourses");

            migrationBuilder.AddColumn<int>(
                name: "LessonCompleted",
                table: "AccountinCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
