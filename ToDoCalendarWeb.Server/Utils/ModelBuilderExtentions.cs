using Microsoft.EntityFrameworkCore;
using System;
using ToDoCalendarWeb.Domain;
using DayOfWeek = ToDoCalendarWeb.Domain.DayOfWeek;
using Task = ToDoCalendarWeb.Domain.Task;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        // Рутина "Работа"
        var workRoutine = new Routine { Id = Guid.NewGuid(), Name = "Работа" };
        var workPeriodMonday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Работа",
            DayOfWeek = DayOfWeek.Monday,
            RoutineId = workRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0) }
        };
        var workPeriodTuesday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Работа",
            DayOfWeek = DayOfWeek.Tuesday,
            RoutineId = workRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0) }
        };
        var workPeriodWednesday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Работа",
            DayOfWeek = DayOfWeek.Wednesday,
            RoutineId = workRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0) }
        };
        var workPeriodThursday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Работа",
            DayOfWeek = DayOfWeek.Thursday,
            RoutineId = workRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0) }
        };
        var workPeriodFriday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Работа",
            DayOfWeek = DayOfWeek.Friday,
            RoutineId = workRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0) }
        };

        // Рутина "Учеба"
        var studyRoutine = new Routine { Id = Guid.NewGuid(), Name = "Учеба" };
        var studyPeriodMonday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Учеба",
            DayOfWeek = DayOfWeek.Monday,
            RoutineId = studyRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(19, 0, 0), EndTime = new TimeSpan(22, 0, 0) }
        };
        var studyPeriodWednesday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Учеба",
            DayOfWeek = DayOfWeek.Wednesday,
            RoutineId = studyRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(19, 0, 0), EndTime = new TimeSpan(22, 0, 0) }
        };
        var studyPeriodFriday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Учеба",
            DayOfWeek = DayOfWeek.Friday,
            RoutineId = studyRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(19, 0, 0), EndTime = new TimeSpan(22, 0, 0) }
        };

        // Рутина "Спорт"
        var sportRoutine = new Routine { Id = Guid.NewGuid(), Name = "Спорт" };
        var sportPeriodTuesday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Спорт",
            DayOfWeek = DayOfWeek.Tuesday,
            RoutineId = sportRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(19, 0, 0), EndTime = new TimeSpan(21, 0, 0) }
        };
        var sportPeriodThursday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Спорт",
            DayOfWeek = DayOfWeek.Thursday,
            RoutineId = sportRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(19, 0, 0), EndTime = new TimeSpan(21, 0, 0) }
        };
        var sportPeriodSaturday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Спорт",
            DayOfWeek = DayOfWeek.Saturday,
            RoutineId = sportRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 0, 0) }
        };

        // Рутина "Домашние дела"
        var houseworkRoutine = new Routine { Id = Guid.NewGuid(), Name = "Домашние дела" };
        var houseworkPeriodSaturday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Домашние дела",
            DayOfWeek = DayOfWeek.Saturday,
            RoutineId = houseworkRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(18, 0, 0) }
        };
        var houseworkPeriodSunday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Домашние дела",
            DayOfWeek = DayOfWeek.Sunday,
            RoutineId = houseworkRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0) }
        };

        // Рутина "Отдых"
        var relaxRoutine = new Routine { Id = Guid.NewGuid(), Name = "Отдых" };
        var relaxPeriodSaturday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Отдых",
            DayOfWeek = DayOfWeek.Saturday,
            RoutineId = relaxRoutine.Id,
            TimePeriod = new TimePeriod { StartTime = new TimeSpan(19, 0, 0), EndTime = new TimeSpan(22, 0, 0) }
        };
        var relaxPeriodSunday = new Period
        {
            Id = Guid.NewGuid(),
            Name = "Отдых",
            DayOfWeek = DayOfWeek.Sunday,
            RoutineId = relaxRoutine.Id,
            TimePeriod = new TimePeriod
            {
                StartTime = new TimeSpan(19, 0, 0),
                EndTime = new TimeSpan(22, 0, 0)
            }
        };
        // Задачи для периодов работы
        var workTask1 = new Task { Id = Guid.NewGuid(), PeriodId = workPeriodMonday.Id, Name = "Разработка веб-приложения" };
        var workTask2 = new Task { Id = Guid.NewGuid(), PeriodId = workPeriodTuesday.Id, Name = "Оптимизация производительности" };
        var workTask3 = new Task { Id = Guid.NewGuid(), PeriodId = workPeriodWednesday.Id, Name = "Исправление багов" };
        var workTask4 = new Task { Id = Guid.NewGuid(), PeriodId = workPeriodThursday.Id, Name = "Написание технической документации" };
        var workTask5 = new Task { Id = Guid.NewGuid(), PeriodId = workPeriodFriday.Id, Name = "Планирование следующего спринта" };

        // Задачи для периодов учебы
        var studyTask1 = new Task { Id = Guid.NewGuid(), PeriodId = studyPeriodMonday.Id, Name = "Изучение алгоритмов и структур данных" };
        var studyTask2 = new Task { Id = Guid.NewGuid(), PeriodId = studyPeriodWednesday.Id, Name = "Практика по программированию" };
        var studyTask3 = new Task { Id = Guid.NewGuid(), PeriodId = studyPeriodFriday.Id, Name = "Подготовка к экзамену" };

        // Задачи для периодов спорта
        var sportTask1 = new Task { Id = Guid.NewGuid(), PeriodId = sportPeriodTuesday.Id, Name = "Силовая тренировка" };
        var sportTask2 = new Task { Id = Guid.NewGuid(), PeriodId = sportPeriodThursday.Id, Name = "Кардио-тренировка" };
        var sportTask3 = new Task { Id = Guid.NewGuid(), PeriodId = sportPeriodSaturday.Id, Name = "Йога" };

        // Задачи для периодов домашних дел
        var houseworkTask1 = new Task { Id = Guid.NewGuid(), PeriodId = houseworkPeriodSaturday.Id, Name = "Уборка квартиры" };
        var houseworkTask2 = new Task { Id = Guid.NewGuid(), PeriodId = houseworkPeriodSunday.Id, Name = "Стирка белья" };
        var houseworkTask3 = new Task { Id = Guid.NewGuid(), PeriodId = houseworkPeriodSunday.Id, Name = "Приготовление обеда" };

        // Сидим данные в базу данных
        modelBuilder.Entity<Routine>().HasData(
            workRoutine, studyRoutine, sportRoutine, houseworkRoutine, relaxRoutine
        );

        modelBuilder.Entity<Period>().HasData(
            workPeriodMonday, workPeriodTuesday, workPeriodWednesday, workPeriodThursday, workPeriodFriday,
            studyPeriodMonday, studyPeriodWednesday, studyPeriodFriday,
            sportPeriodTuesday, sportPeriodThursday, sportPeriodSaturday,
            houseworkPeriodSaturday, houseworkPeriodSunday,
            relaxPeriodSaturday, relaxPeriodSunday
        );

        modelBuilder.Entity<Task>().HasData(
            workTask1, workTask2, workTask3, workTask4, workTask5,
            studyTask1, studyTask2, studyTask3,
            sportTask1, sportTask2, sportTask3,
            houseworkTask1, houseworkTask2, houseworkTask3
        );
    }
}