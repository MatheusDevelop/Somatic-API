﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UI.Aws.Context;

#nullable disable

namespace UI.Aws.Migrations
{
    [DbContext(typeof(SomaticContext))]
    [Migration("20221128022601_up7")]
    partial class up7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("MachineId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Domain.Entities.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("Domain.Entities.MediaUrl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int?>("MachineId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("MachineId");

                    b.ToTable("MediaUrl");
                });

            modelBuilder.Entity("Domain.Entities.Sequence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("Repetitions")
                        .HasColumnType("int");

                    b.Property<int>("Series")
                        .HasColumnType("int");

                    b.Property<bool>("UntilFail")
                        .HasColumnType("bit");

                    b.Property<int?>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Sequence");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePictureUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserNick")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("LeanerWorkouts", b =>
                {
                    b.Property<int>("LeanersId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutsId")
                        .HasColumnType("int");

                    b.HasKey("LeanersId", "WorkoutsId");

                    b.HasIndex("WorkoutsId");

                    b.ToTable("LeanerWorkouts");
                });

            modelBuilder.Entity("Domain.Entities.Leaner", b =>
                {
                    b.HasBaseType("Domain.Entities.User");

                    b.HasDiscriminator().HasValue("Leaner");
                });

            modelBuilder.Entity("Domain.Entities.Teacher", b =>
                {
                    b.HasBaseType("Domain.Entities.User");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("Domain.Entities.Exercise", b =>
                {
                    b.HasOne("Domain.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Machine", "Machine")
                        .WithMany("Exercises")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Domain.Entities.MediaUrl", b =>
                {
                    b.HasOne("Domain.Entities.Exercise", null)
                        .WithMany("MediaUrls")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Entities.Machine", null)
                        .WithMany("MediaUrls")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Domain.Entities.Sequence", b =>
                {
                    b.HasOne("Domain.Entities.Exercise", "Exercise")
                        .WithMany("Sequences")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Workout", null)
                        .WithMany("Sequences")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("Domain.Entities.Workout", b =>
                {
                    b.HasOne("Domain.Entities.Teacher", "CreatedBy")
                        .WithMany("CreatedWorkouts")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("LeanerWorkouts", b =>
                {
                    b.HasOne("Domain.Entities.Leaner", null)
                        .WithMany()
                        .HasForeignKey("LeanersId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Workout", null)
                        .WithMany()
                        .HasForeignKey("WorkoutsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Exercise", b =>
                {
                    b.Navigation("MediaUrls");

                    b.Navigation("Sequences");
                });

            modelBuilder.Entity("Domain.Entities.Machine", b =>
                {
                    b.Navigation("Exercises");

                    b.Navigation("MediaUrls");
                });

            modelBuilder.Entity("Domain.Entities.Workout", b =>
                {
                    b.Navigation("Sequences");
                });

            modelBuilder.Entity("Domain.Entities.Teacher", b =>
                {
                    b.Navigation("CreatedWorkouts");
                });
#pragma warning restore 612, 618
        }
    }
}
