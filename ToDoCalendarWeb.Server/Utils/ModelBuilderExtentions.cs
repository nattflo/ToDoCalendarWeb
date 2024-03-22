using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;
using Task = ToDoCalendarWeb.Domain.Task;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var cleaningRoutine = new Routine { Id = Guid.NewGuid(), Name = "Уборка" };
        var cleaningPeriod = new Period {
            Id = Guid.NewGuid(),
            Name = "Уборка",
            DayOfWeek = ToDoCalendarWeb.Domain.DayOfWeek.Monday,
            RoutineId = cleaningRoutine.Id,
            TimePeriod = new TimePeriod()
            {
                StartTime = new TimeSpan(10,00,00),
                EndTime = new TimeSpan(12,00,00)
            }
        };
        var dustCleaningTask = new Task
        {
            Id = Guid.NewGuid(),
            PeriodId = cleaningPeriod.Id,
            Name = "Убрать пыль"
        };
        var windowCleaningTask = new Task
        {
            Id = Guid.NewGuid(),
            PeriodId = cleaningPeriod.Id,
            Name = "Помыть окно"
        };

        var cookingRoutine = new Routine { Id = Guid.NewGuid(), Name = "Готовка" };
        var breakfastPeriod = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Завтрак",
            DayOfWeek = ToDoCalendarWeb.Domain.DayOfWeek.Monday,
            RoutineId = cookingRoutine.Id,
            TimePeriod = new TimePeriod()
            {
                StartTime = new TimeSpan(8, 00, 00),
                EndTime = new TimeSpan(10, 00, 00)
            }
        };
        var dinnerPeriod = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Обед",
            DayOfWeek = ToDoCalendarWeb.Domain.DayOfWeek.Monday,
            RoutineId = cookingRoutine.Id,
            TimePeriod = new TimePeriod()
            {
                StartTime = new TimeSpan(12, 00, 00),
                EndTime = new TimeSpan(14, 00, 00)
            }
        };
        var lunchPeriod = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Ужин",
            DayOfWeek = ToDoCalendarWeb.Domain.DayOfWeek.Monday,
            RoutineId = cookingRoutine.Id,
            TimePeriod = new TimePeriod()
            {
                StartTime = new TimeSpan(18, 00, 00),
                EndTime = new TimeSpan(20, 00, 00)
            }
        };

        var workRoutine = new Routine
        {
            Id = Guid.NewGuid(),
            Name = "Работа"
        };
        var workPeriod = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Работа",
            DayOfWeek = ToDoCalendarWeb.Domain.DayOfWeek.Monday,
            RoutineId = workRoutine.Id,
            TimePeriod = new TimePeriod()
            {
                StartTime = new TimeSpan(14, 00, 00),
                EndTime = new TimeSpan(18, 00, 00)
            }
        };
        var workTask1 = new Task
        {
            Id = Guid.NewGuid(),
            PeriodId = workPeriod.Id,
            Name = "Доделать пэт проект"
        };
        var workTask2 = new Task
        {
            Id = Guid.NewGuid(),
            PeriodId = workPeriod.Id,
            Name = "Прочитать главу книги"
        };


        modelBuilder.Entity<Routine>().HasData(
            cleaningRoutine,
            cookingRoutine,
            workRoutine
        );
        modelBuilder.Entity<Period>().HasData(
            cleaningPeriod,
            breakfastPeriod,
            dinnerPeriod,
            lunchPeriod,
            workPeriod
        );
        modelBuilder.Entity<Task>().HasData(
            dustCleaningTask,
            windowCleaningTask,
            workTask1,
            workTask2
        );
    }
}