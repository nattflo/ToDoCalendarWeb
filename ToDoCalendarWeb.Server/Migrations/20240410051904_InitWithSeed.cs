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
                    { new Guid("1c6fba9e-cb51-436c-98bd-1bd1db1e5da8"), null, "Отдых" },
                    { new Guid("38a14d28-c09b-4508-887d-648b184d42c9"), null, "Работа" },
                    { new Guid("8baa0d5a-d0e6-4e71-9aa8-4f6d1d4638c1"), null, "Учеба" },
                    { new Guid("9d7d9bc5-9308-47b3-8cbb-0290fccc27b1"), null, "Домашние дела" },
                    { new Guid("e818ff86-7de6-4493-bd49-e9f77654c241"), null, "Спорт" }
                });

            migrationBuilder.InsertData(
                table: "Periods",
                columns: new[] { "Id", "DayOfWeek", "Name", "RoutineId", "TimePeriod" },
                values: new object[,]
                {
                    { new Guid("0e936348-da23-4532-827b-f6fde9204d97"), 5, "Спорт", new Guid("e818ff86-7de6-4493-bd49-e9f77654c241"), "{\"StartTime\":\"10:00:00\",\"EndTime\":\"12:00:00\"}" },
                    { new Guid("174969c8-7308-44c3-ab99-792c919cdeac"), 3, "Работа", new Guid("38a14d28-c09b-4508-887d-648b184d42c9"), "{\"StartTime\":\"10:00:00\",\"EndTime\":\"18:00:00\"}" },
                    { new Guid("1eac9215-99b8-463e-a3b0-48aef1bb577d"), 2, "Учеба", new Guid("8baa0d5a-d0e6-4e71-9aa8-4f6d1d4638c1"), "{\"StartTime\":\"19:00:00\",\"EndTime\":\"22:00:00\"}" },
                    { new Guid("7a998f35-a866-419a-acfd-78c95a560de0"), 4, "Работа", new Guid("38a14d28-c09b-4508-887d-648b184d42c9"), "{\"StartTime\":\"10:00:00\",\"EndTime\":\"18:00:00\"}" },
                    { new Guid("7db87a7a-0090-417f-b5bb-dc24db87c128"), 0, "Работа", new Guid("38a14d28-c09b-4508-887d-648b184d42c9"), "{\"StartTime\":\"10:00:00\",\"EndTime\":\"18:00:00\"}" },
                    { new Guid("89972ef9-b666-466f-b7af-b960a9efcf17"), 4, "Учеба", new Guid("8baa0d5a-d0e6-4e71-9aa8-4f6d1d4638c1"), "{\"StartTime\":\"19:00:00\",\"EndTime\":\"22:00:00\"}" },
                    { new Guid("8dfb9aeb-b2cb-4a32-af7c-41293fe0188d"), 2, "Работа", new Guid("38a14d28-c09b-4508-887d-648b184d42c9"), "{\"StartTime\":\"10:00:00\",\"EndTime\":\"18:00:00\"}" },
                    { new Guid("b5ea1d46-59c9-47c4-940a-d8688fb35949"), 1, "Работа", new Guid("38a14d28-c09b-4508-887d-648b184d42c9"), "{\"StartTime\":\"10:00:00\",\"EndTime\":\"18:00:00\"}" },
                    { new Guid("c4ba6251-a6be-4abf-a0ef-f10e4c6fa751"), 5, "Домашние дела", new Guid("9d7d9bc5-9308-47b3-8cbb-0290fccc27b1"), "{\"StartTime\":\"13:00:00\",\"EndTime\":\"18:00:00\"}" },
                    { new Guid("c66cb7e1-ff7c-4931-835d-24f2a1af8239"), 3, "Спорт", new Guid("e818ff86-7de6-4493-bd49-e9f77654c241"), "{\"StartTime\":\"19:00:00\",\"EndTime\":\"21:00:00\"}" },
                    { new Guid("d88313a3-3fac-46d7-a76f-75bb3b9ec4a0"), 5, "Отдых", new Guid("1c6fba9e-cb51-436c-98bd-1bd1db1e5da8"), "{\"StartTime\":\"19:00:00\",\"EndTime\":\"22:00:00\"}" },
                    { new Guid("e2b4d5ef-6e35-4a9a-b325-c185829e7ac4"), 6, "Домашние дела", new Guid("9d7d9bc5-9308-47b3-8cbb-0290fccc27b1"), "{\"StartTime\":\"10:00:00\",\"EndTime\":\"18:00:00\"}" },
                    { new Guid("e34eebc2-7e25-45e1-ba0e-d66e5f52f001"), 0, "Учеба", new Guid("8baa0d5a-d0e6-4e71-9aa8-4f6d1d4638c1"), "{\"StartTime\":\"19:00:00\",\"EndTime\":\"22:00:00\"}" },
                    { new Guid("f78fcaef-3a3b-49ef-83ec-68fe79ee92c6"), 1, "Спорт", new Guid("e818ff86-7de6-4493-bd49-e9f77654c241"), "{\"StartTime\":\"19:00:00\",\"EndTime\":\"21:00:00\"}" },
                    { new Guid("fa0ab53b-8798-4b7c-a529-5c64e1097063"), 6, "Отдых", new Guid("1c6fba9e-cb51-436c-98bd-1bd1db1e5da8"), "{\"StartTime\":\"19:00:00\",\"EndTime\":\"22:00:00\"}" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "IsCompleted", "IsDraft", "IsTracked", "Name", "PeriodId" },
                values: new object[,]
                {
                    { new Guid("1da2a1d4-574d-4091-850b-1846ee85d119"), null, false, false, true, "Кардио-тренировка", new Guid("c66cb7e1-ff7c-4931-835d-24f2a1af8239") },
                    { new Guid("212683ce-e6dc-49d4-a316-a56366ebf400"), null, false, false, true, "Планирование следующего спринта", new Guid("7a998f35-a866-419a-acfd-78c95a560de0") },
                    { new Guid("28f63c53-211a-4c39-9cc2-48726ebfda87"), null, false, false, true, "Оптимизация производительности", new Guid("b5ea1d46-59c9-47c4-940a-d8688fb35949") },
                    { new Guid("2e38daaf-3392-4b6b-b9d8-f7780f655d49"), null, false, false, true, "Разработка веб-приложения", new Guid("7db87a7a-0090-417f-b5bb-dc24db87c128") },
                    { new Guid("2f32d63b-6fdf-4137-926c-b648f956e28e"), null, false, false, true, "Изучение алгоритмов и структур данных", new Guid("e34eebc2-7e25-45e1-ba0e-d66e5f52f001") },
                    { new Guid("3a67a89d-e2f0-409f-b782-9f2a92569585"), null, false, false, true, "Практика по программированию", new Guid("1eac9215-99b8-463e-a3b0-48aef1bb577d") },
                    { new Guid("51b9b18a-2464-4783-b1b4-77127cf2a352"), null, false, false, true, "Стирка белья", new Guid("e2b4d5ef-6e35-4a9a-b325-c185829e7ac4") },
                    { new Guid("5cac17f4-a823-4f50-ab99-9bf1ba50b062"), null, false, false, true, "Подготовка к экзамену", new Guid("89972ef9-b666-466f-b7af-b960a9efcf17") },
                    { new Guid("67c43f05-0c0a-4efd-99cb-ca5a5f9e2626"), null, false, false, true, "Приготовление обеда", new Guid("e2b4d5ef-6e35-4a9a-b325-c185829e7ac4") },
                    { new Guid("741fcf66-24cb-490e-9c77-0931b170eea2"), null, false, false, true, "Силовая тренировка", new Guid("f78fcaef-3a3b-49ef-83ec-68fe79ee92c6") },
                    { new Guid("85df47b2-1eeb-42e8-a90d-65f952d96d35"), null, false, false, true, "Написание технической документации", new Guid("174969c8-7308-44c3-ab99-792c919cdeac") },
                    { new Guid("b9546311-3af6-4e39-bc89-fac1caf99d98"), null, false, false, true, "Уборка квартиры", new Guid("c4ba6251-a6be-4abf-a0ef-f10e4c6fa751") },
                    { new Guid("c485079a-efb7-4c7b-a6ac-49def869aa69"), null, false, false, true, "Йога", new Guid("0e936348-da23-4532-827b-f6fde9204d97") },
                    { new Guid("c9407998-7a07-4b02-9048-b2b0bffadd3f"), null, false, false, true, "Исправление багов", new Guid("8dfb9aeb-b2cb-4a32-af7c-41293fe0188d") }
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
