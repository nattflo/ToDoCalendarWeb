﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ToDoCalendarWeb.Controllers;

#nullable disable

namespace ToDoCalendarWeb.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ToDoCalendarWeb.Domain.Diff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangeTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ObjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("ObjectType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PropName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Diffs");
                });

            modelBuilder.Entity("ToDoCalendarWeb.Domain.Period", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RoutineId")
                        .HasColumnType("uuid");

                    b.Property<string>("TimePeriod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoutineId");

                    b.ToTable("Periods");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7db87a7a-0090-417f-b5bb-dc24db87c128"),
                            DayOfWeek = 0,
                            Name = "Работа",
                            RoutineId = new Guid("38a14d28-c09b-4508-887d-648b184d42c9"),
                            TimePeriod = "{\"StartTime\":\"10:00:00\",\"EndTime\":\"18:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("b5ea1d46-59c9-47c4-940a-d8688fb35949"),
                            DayOfWeek = 1,
                            Name = "Работа",
                            RoutineId = new Guid("38a14d28-c09b-4508-887d-648b184d42c9"),
                            TimePeriod = "{\"StartTime\":\"10:00:00\",\"EndTime\":\"18:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("8dfb9aeb-b2cb-4a32-af7c-41293fe0188d"),
                            DayOfWeek = 2,
                            Name = "Работа",
                            RoutineId = new Guid("38a14d28-c09b-4508-887d-648b184d42c9"),
                            TimePeriod = "{\"StartTime\":\"10:00:00\",\"EndTime\":\"18:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("174969c8-7308-44c3-ab99-792c919cdeac"),
                            DayOfWeek = 3,
                            Name = "Работа",
                            RoutineId = new Guid("38a14d28-c09b-4508-887d-648b184d42c9"),
                            TimePeriod = "{\"StartTime\":\"10:00:00\",\"EndTime\":\"18:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("7a998f35-a866-419a-acfd-78c95a560de0"),
                            DayOfWeek = 4,
                            Name = "Работа",
                            RoutineId = new Guid("38a14d28-c09b-4508-887d-648b184d42c9"),
                            TimePeriod = "{\"StartTime\":\"10:00:00\",\"EndTime\":\"18:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("e34eebc2-7e25-45e1-ba0e-d66e5f52f001"),
                            DayOfWeek = 0,
                            Name = "Учеба",
                            RoutineId = new Guid("8baa0d5a-d0e6-4e71-9aa8-4f6d1d4638c1"),
                            TimePeriod = "{\"StartTime\":\"19:00:00\",\"EndTime\":\"22:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("1eac9215-99b8-463e-a3b0-48aef1bb577d"),
                            DayOfWeek = 2,
                            Name = "Учеба",
                            RoutineId = new Guid("8baa0d5a-d0e6-4e71-9aa8-4f6d1d4638c1"),
                            TimePeriod = "{\"StartTime\":\"19:00:00\",\"EndTime\":\"22:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("89972ef9-b666-466f-b7af-b960a9efcf17"),
                            DayOfWeek = 4,
                            Name = "Учеба",
                            RoutineId = new Guid("8baa0d5a-d0e6-4e71-9aa8-4f6d1d4638c1"),
                            TimePeriod = "{\"StartTime\":\"19:00:00\",\"EndTime\":\"22:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("f78fcaef-3a3b-49ef-83ec-68fe79ee92c6"),
                            DayOfWeek = 1,
                            Name = "Спорт",
                            RoutineId = new Guid("e818ff86-7de6-4493-bd49-e9f77654c241"),
                            TimePeriod = "{\"StartTime\":\"19:00:00\",\"EndTime\":\"21:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("c66cb7e1-ff7c-4931-835d-24f2a1af8239"),
                            DayOfWeek = 3,
                            Name = "Спорт",
                            RoutineId = new Guid("e818ff86-7de6-4493-bd49-e9f77654c241"),
                            TimePeriod = "{\"StartTime\":\"19:00:00\",\"EndTime\":\"21:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("0e936348-da23-4532-827b-f6fde9204d97"),
                            DayOfWeek = 5,
                            Name = "Спорт",
                            RoutineId = new Guid("e818ff86-7de6-4493-bd49-e9f77654c241"),
                            TimePeriod = "{\"StartTime\":\"10:00:00\",\"EndTime\":\"12:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("c4ba6251-a6be-4abf-a0ef-f10e4c6fa751"),
                            DayOfWeek = 5,
                            Name = "Домашние дела",
                            RoutineId = new Guid("9d7d9bc5-9308-47b3-8cbb-0290fccc27b1"),
                            TimePeriod = "{\"StartTime\":\"13:00:00\",\"EndTime\":\"18:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("e2b4d5ef-6e35-4a9a-b325-c185829e7ac4"),
                            DayOfWeek = 6,
                            Name = "Домашние дела",
                            RoutineId = new Guid("9d7d9bc5-9308-47b3-8cbb-0290fccc27b1"),
                            TimePeriod = "{\"StartTime\":\"10:00:00\",\"EndTime\":\"18:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("d88313a3-3fac-46d7-a76f-75bb3b9ec4a0"),
                            DayOfWeek = 5,
                            Name = "Отдых",
                            RoutineId = new Guid("1c6fba9e-cb51-436c-98bd-1bd1db1e5da8"),
                            TimePeriod = "{\"StartTime\":\"19:00:00\",\"EndTime\":\"22:00:00\"}"
                        },
                        new
                        {
                            Id = new Guid("fa0ab53b-8798-4b7c-a529-5c64e1097063"),
                            DayOfWeek = 6,
                            Name = "Отдых",
                            RoutineId = new Guid("1c6fba9e-cb51-436c-98bd-1bd1db1e5da8"),
                            TimePeriod = "{\"StartTime\":\"19:00:00\",\"EndTime\":\"22:00:00\"}"
                        });
                });

            modelBuilder.Entity("ToDoCalendarWeb.Domain.Routine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Routines");

                    b.HasData(
                        new
                        {
                            Id = new Guid("38a14d28-c09b-4508-887d-648b184d42c9"),
                            Name = "Работа"
                        },
                        new
                        {
                            Id = new Guid("8baa0d5a-d0e6-4e71-9aa8-4f6d1d4638c1"),
                            Name = "Учеба"
                        },
                        new
                        {
                            Id = new Guid("e818ff86-7de6-4493-bd49-e9f77654c241"),
                            Name = "Спорт"
                        },
                        new
                        {
                            Id = new Guid("9d7d9bc5-9308-47b3-8cbb-0290fccc27b1"),
                            Name = "Домашние дела"
                        },
                        new
                        {
                            Id = new Guid("1c6fba9e-cb51-436c-98bd-1bd1db1e5da8"),
                            Name = "Отдых"
                        });
                });

            modelBuilder.Entity("ToDoCalendarWeb.Domain.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDraft")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsTracked")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PeriodId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PeriodId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2e38daaf-3392-4b6b-b9d8-f7780f655d49"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Разработка веб-приложения",
                            PeriodId = new Guid("7db87a7a-0090-417f-b5bb-dc24db87c128")
                        },
                        new
                        {
                            Id = new Guid("28f63c53-211a-4c39-9cc2-48726ebfda87"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Оптимизация производительности",
                            PeriodId = new Guid("b5ea1d46-59c9-47c4-940a-d8688fb35949")
                        },
                        new
                        {
                            Id = new Guid("c9407998-7a07-4b02-9048-b2b0bffadd3f"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Исправление багов",
                            PeriodId = new Guid("8dfb9aeb-b2cb-4a32-af7c-41293fe0188d")
                        },
                        new
                        {
                            Id = new Guid("85df47b2-1eeb-42e8-a90d-65f952d96d35"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Написание технической документации",
                            PeriodId = new Guid("174969c8-7308-44c3-ab99-792c919cdeac")
                        },
                        new
                        {
                            Id = new Guid("212683ce-e6dc-49d4-a316-a56366ebf400"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Планирование следующего спринта",
                            PeriodId = new Guid("7a998f35-a866-419a-acfd-78c95a560de0")
                        },
                        new
                        {
                            Id = new Guid("2f32d63b-6fdf-4137-926c-b648f956e28e"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Изучение алгоритмов и структур данных",
                            PeriodId = new Guid("e34eebc2-7e25-45e1-ba0e-d66e5f52f001")
                        },
                        new
                        {
                            Id = new Guid("3a67a89d-e2f0-409f-b782-9f2a92569585"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Практика по программированию",
                            PeriodId = new Guid("1eac9215-99b8-463e-a3b0-48aef1bb577d")
                        },
                        new
                        {
                            Id = new Guid("5cac17f4-a823-4f50-ab99-9bf1ba50b062"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Подготовка к экзамену",
                            PeriodId = new Guid("89972ef9-b666-466f-b7af-b960a9efcf17")
                        },
                        new
                        {
                            Id = new Guid("741fcf66-24cb-490e-9c77-0931b170eea2"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Силовая тренировка",
                            PeriodId = new Guid("f78fcaef-3a3b-49ef-83ec-68fe79ee92c6")
                        },
                        new
                        {
                            Id = new Guid("1da2a1d4-574d-4091-850b-1846ee85d119"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Кардио-тренировка",
                            PeriodId = new Guid("c66cb7e1-ff7c-4931-835d-24f2a1af8239")
                        },
                        new
                        {
                            Id = new Guid("c485079a-efb7-4c7b-a6ac-49def869aa69"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Йога",
                            PeriodId = new Guid("0e936348-da23-4532-827b-f6fde9204d97")
                        },
                        new
                        {
                            Id = new Guid("b9546311-3af6-4e39-bc89-fac1caf99d98"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Уборка квартиры",
                            PeriodId = new Guid("c4ba6251-a6be-4abf-a0ef-f10e4c6fa751")
                        },
                        new
                        {
                            Id = new Guid("51b9b18a-2464-4783-b1b4-77127cf2a352"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Стирка белья",
                            PeriodId = new Guid("e2b4d5ef-6e35-4a9a-b325-c185829e7ac4")
                        },
                        new
                        {
                            Id = new Guid("67c43f05-0c0a-4efd-99cb-ca5a5f9e2626"),
                            IsCompleted = false,
                            IsDraft = false,
                            IsTracked = true,
                            Name = "Приготовление обеда",
                            PeriodId = new Guid("e2b4d5ef-6e35-4a9a-b325-c185829e7ac4")
                        });
                });

            modelBuilder.Entity("ToDoCalendarWeb.Domain.Period", b =>
                {
                    b.HasOne("ToDoCalendarWeb.Domain.Routine", null)
                        .WithMany("Periods")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ToDoCalendarWeb.Domain.Task", b =>
                {
                    b.HasOne("ToDoCalendarWeb.Domain.Period", null)
                        .WithMany("Tasks")
                        .HasForeignKey("PeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ToDoCalendarWeb.Domain.Period", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ToDoCalendarWeb.Domain.Routine", b =>
                {
                    b.Navigation("Periods");
                });
#pragma warning restore 612, 618
        }
    }
}
