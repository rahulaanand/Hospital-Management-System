﻿// <auto-generated />
using System;
using Big_Bang_Assessment.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Big_Bang_Assessment.Migrations
{
    [DbContext(typeof(HospitalContext))]
    [Migration("20230702205800_new")]
    partial class @new
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Big_Bang_Assessment.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"));

                    b.Property<string>("AdPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Big_Bang_Assessment.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<string>("AppointmentDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int?>("Doctor_id")
                        .HasColumnType("int");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId");

                    b.HasIndex("Doctor_id");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Big_Bang_Assessment.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorAge")
                        .HasColumnType("int");

                    b.Property<string>("DoctorEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorExperience")
                        .HasColumnType("int");

                    b.Property<string>("DoctorGender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorPwd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorSpeciality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DoctorId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Big_Bang_Assessment.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientId"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("EmailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Big_Bang_Assessment.Models.Appointment", b =>
                {
                    b.HasOne("Big_Bang_Assessment.Models.Doctor", "Doctors")
                        .WithMany()
                        .HasForeignKey("Doctor_id");

                    b.HasOne("Big_Bang_Assessment.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctors");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Big_Bang_Assessment.Models.Patient", b =>
                {
                    b.HasOne("Big_Bang_Assessment.Models.Doctor", null)
                        .WithMany("Patients")
                        .HasForeignKey("DoctorId");
                });

            modelBuilder.Entity("Big_Bang_Assessment.Models.Doctor", b =>
                {
                    b.Navigation("Patients");
                });
#pragma warning restore 612, 618
        }
    }
}
