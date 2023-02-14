﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OptimizationFiltering.InteractionDB;

#nullable disable

namespace OptimizationFiltering.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230212112314_update data")]
    partial class updatedata
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("OptimizationFiltering.Models.Method", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsImplemented")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Method");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsImplemented = true,
                            Name = "Метод Бокса"
                        });
                });

            modelBuilder.Entity("OptimizationFiltering.Models.Parameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Parameter");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Количество перегородок"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Величина перепада давлений на первой перегородке"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Величина перепада давлений на второй перегородке"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Погрешность вычисления"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Минимальная температура на первой перегородке"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Максимальная температура на первой перегородке"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Минимальная температура на второй перегородке"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Максимальная температура на второй перегородке"
                        });
                });

            modelBuilder.Entity("OptimizationFiltering.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Пользователь"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Администратор"
                        });
                });

            modelBuilder.Entity("OptimizationFiltering.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Task");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Вариант №20"
                        });
                });

            modelBuilder.Entity("OptimizationFiltering.Models.TaskParameter", b =>
                {
                    b.Property<int>("ParameterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TaskId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("ParameterId", "TaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskParameter");

                    b.HasData(
                        new
                        {
                            ParameterId = 1,
                            TaskId = 1,
                            Value = 2.0
                        },
                        new
                        {
                            ParameterId = 2,
                            TaskId = 1,
                            Value = 11.0
                        },
                        new
                        {
                            ParameterId = 3,
                            TaskId = 1,
                            Value = 7.0
                        },
                        new
                        {
                            ParameterId = 4,
                            TaskId = 1,
                            Value = -5.0
                        },
                        new
                        {
                            ParameterId = 5,
                            TaskId = 1,
                            Value = 0.0
                        },
                        new
                        {
                            ParameterId = 6,
                            TaskId = 1,
                            Value = -1.0
                        },
                        new
                        {
                            ParameterId = 7,
                            TaskId = 1,
                            Value = 5.0
                        },
                        new
                        {
                            ParameterId = 8,
                            TaskId = 1,
                            Value = 1.5
                        });
                });

            modelBuilder.Entity("OptimizationFiltering.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "user",
                            RoleId = 1,
                            Username = "user"
                        },
                        new
                        {
                            Id = 2,
                            Password = "admin",
                            RoleId = 2,
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("OptimizationFiltering.Models.TaskParameter", b =>
                {
                    b.HasOne("OptimizationFiltering.Models.Parameter", "Parameter")
                        .WithMany("Parameters")
                        .HasForeignKey("ParameterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OptimizationFiltering.Models.Task", "Task")
                        .WithMany("Parameters")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parameter");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("OptimizationFiltering.Models.User", b =>
                {
                    b.HasOne("OptimizationFiltering.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("OptimizationFiltering.Models.Parameter", b =>
                {
                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("OptimizationFiltering.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("OptimizationFiltering.Models.Task", b =>
                {
                    b.Navigation("Parameters");
                });
#pragma warning restore 612, 618
        }
    }
}
