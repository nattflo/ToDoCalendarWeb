using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoCalendarWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectType = table.Column<string>(type: "text", nullable: false),
                    ChangeTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PropName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    From = table.Column<string>(type: "text", nullable: false),
                    To = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RoutineId = table.Column<Guid>(type: "uuid", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    TimePeriod = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Periods_Routines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDraft = table.Column<bool>(type: "boolean", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsTracked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Routines",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1e642aac-2cc5-4a70-bfa3-903ca4349f14"), null, "Готовка" },
                    { new Guid("1f8c6ba0-b06f-4979-a84a-0c789d5ae168"), null, "Уборка" },
                    { new Guid("b41acb52-dd15-4567-a09d-17db0cc28998"), null, "Работа" }
                });

            migrationBuilder.InsertData(
                table: "Periods",
                columns: new[] { "Id", "DayOfWeek", "Name", "RoutineId", "TimePeriod" },
                values: new object[,]
                {
                    { new Guid("1f351d75-1e0e-4163-9bce-06a9e7e123cc"), 0, "Обед", new Guid("1e642aac-2cc5-4a70-bfa3-903ca4349f14"), "{\"StartTime\":\"12:00:00\",\"EndTime\":\"14:00:00\"}" },
                    { new Guid("236f1c19-ffa9-4d84-975a-8a32e8ee6df7"), 0, "Ужин", new Guid("1e642aac-2cc5-4a70-bfa3-903ca4349f14"), "{\"StartTime\":\"18:00:00\",\"EndTime\":\"20:00:00\"}" },
                    { new Guid("5a58b463-ed8c-429e-905a-1fac51176435"), 0, "Работа", new Guid("b41acb52-dd15-4567-a09d-17db0cc28998"), "{\"StartTime\":\"14:00:00\",\"EndTime\":\"18:00:00\"}" },
                    { new Guid("6ebcaabe-6bb4-4412-94c6-1cd6c9480721"), 0, "Уборка", new Guid("1f8c6ba0-b06f-4979-a84a-0c789d5ae168"), "{\"StartTime\":\"10:00:00\",\"EndTime\":\"12:00:00\"}" },
                    { new Guid("7adee775-c6d8-4d6a-924e-316232303a69"), 0, "Завтрак", new Guid("1e642aac-2cc5-4a70-bfa3-903ca4349f14"), "{\"StartTime\":\"08:00:00\",\"EndTime\":\"10:00:00\"}" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "IsCompleted", "IsDraft", "IsTracked", "Name", "PeriodId" },
                values: new object[,]
                {
                    { new Guid("4ed80e97-bf91-46cd-a689-cb705b859755"), "", false, false, true, "Доделать пэт проект", new Guid("5a58b463-ed8c-429e-905a-1fac51176435") },
                    { new Guid("61c82b03-9a8d-4e53-b053-c7477b218e6a"), "", false, false, true, "Прочитать главу книги", new Guid("5a58b463-ed8c-429e-905a-1fac51176435") },
                    { new Guid("656f7f70-6bcf-42b3-92df-b1e6390578e5"), "", false, false, true, "Убрать пыль", new Guid("6ebcaabe-6bb4-4412-94c6-1cd6c9480721") },
                    { new Guid("cc15dd8f-d486-47bb-97eb-c0ca02972220"), "", false, false, true, "Помыть окно", new Guid("6ebcaabe-6bb4-4412-94c6-1cd6c9480721") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Periods_RoutineId",
                table: "Periods",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PeriodId",
                table: "Tasks",
                column: "PeriodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diffs");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropTable(
                name: "Routines");
        }
    }
}
