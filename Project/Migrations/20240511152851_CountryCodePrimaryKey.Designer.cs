﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Entities;

#nullable disable

namespace Project.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240511152851_CountryCodePrimaryKey")]
    partial class CountryCodePrimaryKey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MusicianOrchestra", b =>
                {
                    b.Property<int>("MusiciansId")
                        .HasColumnType("int");

                    b.Property<int>("OrchestrasId")
                        .HasColumnType("int");

                    b.HasKey("MusiciansId", "OrchestrasId");

                    b.HasIndex("OrchestrasId");

                    b.ToTable("MusicianOrchestra");
                });

            modelBuilder.Entity("Project.Entities.Country", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Code");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Project.Entities.Musician", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Instrument")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Musicians");
                });

            modelBuilder.Entity("Project.Entities.Orchestra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Conductor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CountryCode")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CountryCode");

                    b.ToTable("Orchestras");
                });

            modelBuilder.Entity("MusicianOrchestra", b =>
                {
                    b.HasOne("Project.Entities.Musician", null)
                        .WithMany()
                        .HasForeignKey("MusiciansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.Entities.Orchestra", null)
                        .WithMany()
                        .HasForeignKey("OrchestrasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Project.Entities.Orchestra", b =>
                {
                    b.HasOne("Project.Entities.Country", null)
                        .WithMany("Orchestras")
                        .HasForeignKey("CountryCode");
                });

            modelBuilder.Entity("Project.Entities.Country", b =>
                {
                    b.Navigation("Orchestras");
                });
#pragma warning restore 612, 618
        }
    }
}
