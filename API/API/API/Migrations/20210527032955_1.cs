using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamQuizs",
                columns: table => new
                {
                    ExamQuizId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamQuizName = table.Column<string>(nullable: true),
                    ExamQuestion = table.Column<string>(nullable: true),
                    ExamIsCorrect = table.Column<string>(nullable: true),
                    ExamOption1 = table.Column<string>(nullable: true),
                    ExamOption2 = table.Column<string>(nullable: true),
                    ExamOption3 = table.Column<string>(nullable: true),
                    ExamOption4 = table.Column<string>(nullable: true),
                    ExamOption5 = table.Column<string>(nullable: true),
                    ExamQuestionImageURL = table.Column<string>(nullable: true),
                    ExamOptionImageURL1 = table.Column<string>(nullable: true),
                    ExamOptionImageURL2 = table.Column<string>(nullable: true),
                    ExamOptionImageURL3 = table.Column<string>(nullable: true),
                    ExamOptionImageURL4 = table.Column<string>(nullable: true),
                    ExamOptionImageURL5 = table.Column<string>(nullable: true),
                    ExamThumbnailImage = table.Column<byte[]>(nullable: true),
                    ExamQuestionImage = table.Column<byte[]>(nullable: true),
                    ExamOptionImage1 = table.Column<byte[]>(nullable: true),
                    ExamOptionImage2 = table.Column<byte[]>(nullable: true),
                    ExamOptionImage3 = table.Column<byte[]>(nullable: true),
                    ExamOptionImage4 = table.Column<byte[]>(nullable: true),
                    ExamOptionImage5 = table.Column<byte[]>(nullable: true),
                    ExamTagTopic = table.Column<string>(nullable: true),
                    ExamCode = table.Column<string>(nullable: true),
                    CourseId = table.Column<string>(nullable: true),
                    ExamTime = table.Column<int>(nullable: false),
                    QuizId = table.Column<string>(nullable: true),
                    IsBlocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuizs", x => x.ExamQuizId);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageTitle = table.Column<string>(nullable: true),
                    Message = table.Column<string>(maxLength: 200, nullable: true),
                    Type = table.Column<string>(nullable: true),
                    SendTo = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastLogOnDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    AvatarPath = table.Column<byte[]>(nullable: true),
                    Balance = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(maxLength: 100, nullable: true),
                    Role = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Verification = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    NumberOfVoters = table.Column<double>(nullable: false),
                    NumberOfParticipants = table.Column<double>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    CourseDuration = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ThumbnailImage = table.Column<byte[]>(nullable: true),
                    Hastag = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    LessonNumber = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountinCourses",
                columns: table => new
                {
                    AccountinCourseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false),
                    LessonCompleted = table.Column<int>(nullable: false),
                    InvoiceCode = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    PaymentMethod = table.Column<string>(nullable: true),
                    IsBought = table.Column<bool>(nullable: false),
                    IsLiked = table.Column<bool>(nullable: false),
                    GetPayment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountinCourses", x => x.AccountinCourseID);
                    table.ForeignKey(
                        name: "FK_AccountinCourses_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountinCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questionpools",
                columns: table => new
                {
                    QuestionpoolId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionpoolName = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastEdited = table.Column<DateTime>(nullable: false),
                    Hastag = table.Column<string>(nullable: true),
                    QuestionpoolThumbnailImage = table.Column<byte[]>(nullable: true),
                    QuestionpoolThumbnailImageURL = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    ShareMethod = table.Column<string>(nullable: true),
                    AccountId = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionpools", x => x.QuestionpoolId);
                    table.ForeignKey(
                        name: "FK_Questionpools_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    TopicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicTitle = table.Column<string>(nullable: true),
                    SessionNumber = table.Column<int>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_Topics_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quizs",
                columns: table => new
                {
                    QuizId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(nullable: true),
                    QuestionType = table.Column<string>(nullable: true),
                    QuizImage = table.Column<byte[]>(nullable: true),
                    QuizImageLink = table.Column<string>(nullable: true),
                    Time = table.Column<int>(nullable: false),
                    TopicId = table.Column<string>(nullable: true),
                    QuestionpoolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizs", x => x.QuizId);
                    table.ForeignKey(
                        name: "FK_Quizs_Questionpools_QuestionpoolId",
                        column: x => x.QuestionpoolId,
                        principalTable: "Questionpools",
                        principalColumn: "QuestionpoolId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubTopics",
                columns: table => new
                {
                    SubTopicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubTopicTitle = table.Column<string>(nullable: true),
                    SubTopicNumber = table.Column<int>(nullable: false),
                    TopicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTopics", x => x.SubTopicId);
                    table.ForeignKey(
                        name: "FK_SubTopics_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    ChoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(nullable: true),
                    IsCorrect = table.Column<bool>(nullable: false),
                    AnswerImage = table.Column<byte[]>(nullable: true),
                    AnswerImageLink = table.Column<string>(nullable: true),
                    QuizId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => x.ChoiceId);
                    table.ForeignKey(
                        name: "FK_Choices_Quizs_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizs",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonTitle = table.Column<string>(nullable: true),
                    LessonContent = table.Column<string>(nullable: true),
                    LessonNo = table.Column<int>(nullable: false),
                    SubTopicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_Lessons_SubTopics_SubTopicId",
                        column: x => x.SubTopicId,
                        principalTable: "SubTopics",
                        principalColumn: "SubTopicId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountinLessons",
                columns: table => new
                {
                    AccountinLessonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<string>(nullable: true),
                    QuizCode = table.Column<string>(nullable: true),
                    QuizName = table.Column<string>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false),
                    Result = table.Column<string>(nullable: true),
                    LastTaken = table.Column<DateTime>(nullable: false),
                    AccountId1 = table.Column<int>(nullable: true),
                    LessonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountinLessons", x => x.AccountinLessonID);
                    table.ForeignKey(
                        name: "FK_AccountinLessons_Accounts_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountinLessons_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentContent = table.Column<string>(nullable: true),
                    Rating = table.Column<float>(nullable: false),
                    DatePost = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    IsLiked = table.Column<bool>(nullable: false),
                    LikeCount = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: true),
                    LessonId = table.Column<string>(nullable: true),
                    LessonId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Lessons_LessonId1",
                        column: x => x.LessonId1,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubComments",
                columns: table => new
                {
                    SubCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCommentContent = table.Column<string>(nullable: true),
                    SubDatePost = table.Column<DateTime>(nullable: false),
                    IsLiked = table.Column<bool>(nullable: false),
                    LikeCount = table.Column<int>(nullable: false),
                    ParentCommentId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubComments", x => x.SubCommentId);
                    table.ForeignKey(
                        name: "FK_SubComments_Comments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountinCourses_AccountId",
                table: "AccountinCourses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountinCourses_CourseId",
                table: "AccountinCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountinLessons_AccountId1",
                table: "AccountinLessons",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccountinLessons_LessonId",
                table: "AccountinLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Choices_QuizId",
                table: "Choices",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CourseId",
                table: "Comments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_LessonId1",
                table: "Comments",
                column: "LessonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AccountId",
                table: "Courses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubTopicId",
                table: "Lessons",
                column: "SubTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Questionpools_CourseId",
                table: "Questionpools",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_QuestionpoolId",
                table: "Quizs",
                column: "QuestionpoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SubComments_ParentCommentId",
                table: "SubComments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubComments_UserId",
                table: "SubComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTopics_TopicId",
                table: "SubTopics",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CourseId",
                table: "Topics",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountinCourses");

            migrationBuilder.DropTable(
                name: "AccountinLessons");

            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropTable(
                name: "ExamQuizs");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "SubComments");

            migrationBuilder.DropTable(
                name: "Quizs");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Questionpools");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "SubTopics");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
