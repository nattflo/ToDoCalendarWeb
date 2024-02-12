using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoCalendarWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Routines",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("f62bddcc-6f3c-40d9-9d00-91762842caa5"), "Тестовая рутина", "Рутина" });

            migrationBuilder.InsertData(
                table: "Periods",
                columns: new[] { "Id", "DayOfWeek", "Name", "RoutineId", "TimePeriod" },
                values: new object[] { new Guid("06e6d9cb-8e1e-4640-b4c9-2acd4048e86d"), 1, "Период", new Guid("f62bddcc-6f3c-40d9-9d00-91762842caa5"), "{\"StartTime\":\"-10675199.02:48:05.4775808\",\"EndTime\":\"10675199.02:48:05.4775807\"}" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "IsCompleted", "IsDraft", "IsTracked", "Name", "PeriodId" },
                values: new object[,]
                {
                    { new Guid("250aa0cb-4ab1-48a1-b8c7-82a8fdeb5d5c"), null, false, false, true, "Задача1", new Guid("06e6d9cb-8e1e-4640-b4c9-2acd4048e86d") },
                    { new Guid("750d8544-52af-416c-8d42-69fec5169a78"), null, false, false, true, "Задача3", new Guid("06e6d9cb-8e1e-4640-b4c9-2acd4048e86d") },
                    { new Guid("ee89b75e-0c8b-4603-9ed5-92dc22330796"), null, false, false, true, "Задача2", new Guid("06e6d9cb-8e1e-4640-b4c9-2acd4048e86d") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("250aa0cb-4ab1-48a1-b8c7-82a8fdeb5d5c"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("750d8544-52af-416c-8d42-69fec5169a78"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("ee89b75e-0c8b-4603-9ed5-92dc22330796"));

            migrationBuilder.DeleteData(
                table: "Periods",
                keyColumn: "Id",
                keyValue: new Guid("06e6d9cb-8e1e-4640-b4c9-2acd4048e86d"));

            migrationBuilder.DeleteData(
                table: "Routines",
                keyColumn: "Id",
                keyValue: new Guid("f62bddcc-6f3c-40d9-9d00-91762842caa5"));
        }
    }
}
