﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Entities;

#nullable disable

namespace MinimalAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240511215020_OrchestraMusicianRelationFix")]
    partial class OrchestraMusicianRelationFix
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

                    b.HasData(
                        new
                        {
                            MusiciansId = 2,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 3,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 4,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 5,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 6,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 7,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 8,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 9,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 10,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 11,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 12,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 13,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 14,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 15,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 16,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 17,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 18,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 19,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 20,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 21,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 22,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 23,
                            OrchestrasId = 1
                        },
                        new
                        {
                            MusiciansId = 2,
                            OrchestrasId = 2
                        },
                        new
                        {
                            MusiciansId = 10,
                            OrchestrasId = 2
                        },
                        new
                        {
                            MusiciansId = 4,
                            OrchestrasId = 3
                        },
                        new
                        {
                            MusiciansId = 1,
                            OrchestrasId = 4
                        },
                        new
                        {
                            MusiciansId = 4,
                            OrchestrasId = 4
                        },
                        new
                        {
                            MusiciansId = 4,
                            OrchestrasId = 5
                        },
                        new
                        {
                            MusiciansId = 6,
                            OrchestrasId = 6
                        },
                        new
                        {
                            MusiciansId = 7,
                            OrchestrasId = 7
                        },
                        new
                        {
                            MusiciansId = 9,
                            OrchestrasId = 8
                        });
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

                    b.HasData(
                        new
                        {
                            Code = "AU",
                            Name = "Australia"
                        },
                        new
                        {
                            Code = "BE",
                            Name = "Belgium"
                        },
                        new
                        {
                            Code = "CO",
                            Name = "Colombia"
                        },
                        new
                        {
                            Code = "DE",
                            Name = "Germany"
                        },
                        new
                        {
                            Code = "HU",
                            Name = "Hungary"
                        },
                        new
                        {
                            Code = "LB",
                            Name = "Lebanon"
                        },
                        new
                        {
                            Code = "SG",
                            Name = "Singapore"
                        },
                        new
                        {
                            Code = "ZA",
                            Name = "South Africa"
                        },
                        new
                        {
                            Code = "TW",
                            Name = "Taiwan"
                        },
                        new
                        {
                            Code = "NL",
                            Name = "Netherlands"
                        },
                        new
                        {
                            Code = "UK",
                            Name = "United Kingdom"
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Instrument = 0,
                            Name = "Sylvia Huang"
                        },
                        new
                        {
                            Id = 2,
                            Instrument = 0,
                            Name = "Maria Kouznetsova"
                        },
                        new
                        {
                            Id = 3,
                            Instrument = 0,
                            Name = "Christophe Mourguiart"
                        },
                        new
                        {
                            Id = 4,
                            Instrument = 0,
                            Name = "Mona Verhas"
                        },
                        new
                        {
                            Id = 5,
                            Instrument = 0,
                            Name = "Orsolya Horváth"
                        },
                        new
                        {
                            Id = 6,
                            Instrument = 0,
                            Name = "Miki Tsunoda"
                        },
                        new
                        {
                            Id = 7,
                            Instrument = 1,
                            Name = "Sander Geerts"
                        },
                        new
                        {
                            Id = 8,
                            Instrument = 1,
                            Name = "Elaine Ng"
                        },
                        new
                        {
                            Id = 9,
                            Instrument = 2,
                            Name = "Raphael Bell"
                        },
                        new
                        {
                            Id = 10,
                            Instrument = 2,
                            Name = "Marc Vossen"
                        },
                        new
                        {
                            Id = 11,
                            Instrument = 3,
                            Name = "Ioan Baranga"
                        },
                        new
                        {
                            Id = 12,
                            Instrument = 3,
                            Name = "Vlad Rațiu"
                        },
                        new
                        {
                            Id = 13,
                            Instrument = 4,
                            Name = "Aldo Baerten"
                        },
                        new
                        {
                            Id = 14,
                            Instrument = 4,
                            Name = "Peter Verhoyen"
                        },
                        new
                        {
                            Id = 15,
                            Instrument = 5,
                            Name = "Louis Baumann"
                        },
                        new
                        {
                            Id = 16,
                            Instrument = 6,
                            Name = "Nele Delafonteyne"
                        },
                        new
                        {
                            Id = 17,
                            Instrument = 7,
                            Name = "Oliver Engels"
                        },
                        new
                        {
                            Id = 18,
                            Instrument = 9,
                            Name = "Michaela Bužková"
                        },
                        new
                        {
                            Id = 19,
                            Instrument = 10,
                            Name = "Alain De Rudder"
                        },
                        new
                        {
                            Id = 20,
                            Instrument = 11,
                            Name = "Daniel Quiles Cascant"
                        },
                        new
                        {
                            Id = 21,
                            Instrument = 12,
                            Name = "Bernd van Echelpoel"
                        },
                        new
                        {
                            Id = 22,
                            Instrument = 13,
                            Name = "Pieterjan Vrankx"
                        },
                        new
                        {
                            Id = 23,
                            Instrument = 13,
                            Name = "Cristiano Menegazzo"
                        });
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
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CountryCode");

                    b.ToTable("Orchestras");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Conductor = "Elim Chan",
                            CountryCode = "BE",
                            Name = "Antwerp Symphony Orchestra"
                        },
                        new
                        {
                            Id = 2,
                            Conductor = "Lorenzo Viotti",
                            CountryCode = "NL",
                            Name = "Nederlands Philharmonisch Orkest"
                        },
                        new
                        {
                            Id = 3,
                            Conductor = "Kazushi Ono",
                            CountryCode = "BE",
                            Name = "Brussels Philharmonic"
                        },
                        new
                        {
                            Id = 4,
                            Conductor = "Alain Altinoglu",
                            CountryCode = "BE",
                            Name = "Symfonieorkest van de Munt"
                        },
                        new
                        {
                            Id = 5,
                            Conductor = "Erik Sluys",
                            CountryCode = "BE",
                            Name = "Brussels Sinfonietta"
                        },
                        new
                        {
                            Id = 6,
                            Conductor = "Candida Thompson",
                            CountryCode = "NL",
                            Name = "Amsterdam Sinfonietta"
                        },
                        new
                        {
                            Id = 7,
                            Conductor = "Edward Gardner",
                            CountryCode = "UK",
                            Name = "London Philharmonic Orchestra"
                        },
                        new
                        {
                            Id = 8,
                            Conductor = "Lahav Shani",
                            CountryCode = "DE",
                            Name = "Münchner Philharmoniker"
                        });
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
                    b.HasOne("Project.Entities.Country", "Country")
                        .WithMany("Orchestras")
                        .HasForeignKey("CountryCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Project.Entities.Country", b =>
                {
                    b.Navigation("Orchestras");
                });
#pragma warning restore 612, 618
        }
    }
}
