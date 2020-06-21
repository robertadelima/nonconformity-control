﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NonconformityControl.Infra.Repositories;

namespace NonconformityControl.Migrations
{
    [DbContext(typeof(NonconformityContext))]
    [Migration("20200621221008_NonconformityVersion")]
    partial class NonconformityVersion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NonconformityControl.Models.Action", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(1024)")
                        .HasMaxLength(1024);

                    b.Property<int>("NonconformityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NonconformityId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("NonconformityControl.Models.Nonconformity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("VARCHAR(15)");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(1024)")
                        .HasMaxLength(1024);

                    b.Property<sbyte>("Evaluation")
                        .HasColumnType("TINYINT");

                    b.Property<sbyte>("Status")
                        .HasColumnType("TINYINT");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Nonconformities");
                });

            modelBuilder.Entity("NonconformityControl.Models.Action", b =>
                {
                    b.HasOne("NonconformityControl.Models.Nonconformity", "Nonconformity")
                        .WithMany("Actions")
                        .HasForeignKey("NonconformityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
