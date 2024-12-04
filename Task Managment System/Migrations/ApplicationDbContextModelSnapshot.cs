﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Task_Managment_System.Data;

#nullable disable

namespace Task_Managment_System.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Task_Managment_System.Models.Tasks", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaskId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            TaskId = 1,
                            Description = "Lorem Ipsum",
                            Status = "Completed",
                            Title = "Important Task 1"
                        },
                        new
                        {
                            TaskId = 2,
                            Description = "Lorem Ipsum",
                            Status = "Pending",
                            Title = " Not Vert Important Task"
                        },
                        new
                        {
                            TaskId = 3,
                            Description = "Lorem Ipsum",
                            Status = "Completed",
                            Title = "Important Task 2"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
