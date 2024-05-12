using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Project.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Orchestra> Orchestras { get; set; }
        public DbSet<Musician> Musicians { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Country>().HasData(
                new Country { Code = "AU", Name = "Australia" },
                new Country { Code = "BE", Name = "Belgium" },
                new Country { Code = "CO", Name = "Colombia" },
                new Country { Code = "DE", Name = "Germany" },
                new Country { Code = "HU", Name = "Hungary" },
                new Country { Code = "LB", Name = "Lebanon" },
                new Country { Code = "SG", Name = "Singapore" },
                new Country { Code = "ZA", Name = "South Africa" },
                new Country { Code = "TW", Name = "Taiwan" },
                new Country { Code = "NL", Name = "Netherlands" },
                new Country { Code = "UK", Name = "United Kingdom" }
                );

            _ = modelBuilder.Entity<Orchestra>(o => o.HasOne(c => c.Country)
                                        .WithMany(x => x.Orchestras)
                                        .HasForeignKey("CountryCode"));

            _ = modelBuilder.Entity<Orchestra>().HasData(
                new { Id = 1, Name = "Antwerp Symphony Orchestra", Conductor = "Elim Chan", CountryCode = "BE" },
                new { Id = 2, Name = "Nederlands Philharmonisch Orkest", Conductor = "Lorenzo Viotti", CountryCode = "NL" },
                new { Id = 3, Name = "Brussels Philharmonic", Conductor = "Kazushi Ono", CountryCode = "BE" },
                new { Id = 4, Name = "Symfonieorkest van de Munt", Conductor = "Alain Altinoglu", CountryCode = "BE" },
                new { Id = 5, Name = "Brussels Sinfonietta", Conductor = "Erik Sluys", CountryCode = "BE" },
                new { Id = 6, Name = "Amsterdam Sinfonietta", Conductor = "Candida Thompson", CountryCode = "NL" },
                new { Id = 7, Name = "London Philharmonic Orchestra", Conductor = "Edward Gardner", CountryCode = "UK" },
                new { Id = 8, Name = "Münchner Philharmoniker", Conductor = "Lahav Shani", CountryCode = "DE" }
                );

            _ = modelBuilder.Entity<Musician>().HasData(
                new Musician { Id = 1, Name = "Sylvia Huang", Instrument = Instruments.Violin },
                new Musician { Id = 2, Name = "Maria Kouznetsova", Instrument = Instruments.Violin },
                new Musician { Id = 3, Name = "Christophe Mourguiart", Instrument = Instruments.Violin },
                new Musician { Id = 4, Name = "Mona Verhas", Instrument = Instruments.Violin },
                new Musician { Id = 5, Name = "Orsolya Horváth", Instrument = Instruments.Violin },
                new Musician { Id = 6, Name = "Miki Tsunoda", Instrument = Instruments.Violin },
                new Musician { Id = 7, Name = "Sander Geerts", Instrument = Instruments.Viola },
                new Musician { Id = 8, Name = "Elaine Ng", Instrument = Instruments.Viola },
                new Musician { Id = 9, Name = "Raphael Bell", Instrument = Instruments.Cello },
                new Musician { Id = 10, Name = "Marc Vossen", Instrument = Instruments.Cello },
                new Musician { Id = 11, Name = "Ioan Baranga", Instrument = Instruments.DoubleBass },
                new Musician { Id = 12, Name = "Vlad Rațiu", Instrument = Instruments.DoubleBass },
                new Musician { Id = 13, Name = "Aldo Baerten", Instrument = Instruments.Flute },
                new Musician { Id = 14, Name = "Peter Verhoyen", Instrument = Instruments.Flute },
                new Musician { Id = 15, Name = "Louis Baumann", Instrument = Instruments.Oboe },
                new Musician { Id = 16, Name = "Nele Delafonteyne", Instrument = Instruments.Clarinet },
                new Musician { Id = 17, Name = "Oliver Engels", Instrument = Instruments.Bassoon },
                new Musician { Id = 18, Name = "Michaela Bužková", Instrument = Instruments.Horn },
                new Musician { Id = 19, Name = "Alain De Rudder", Instrument = Instruments.Trumpet },
                new Musician { Id = 20, Name = "Daniel Quiles Cascant", Instrument = Instruments.Trombone },
                new Musician { Id = 21, Name = "Bernd van Echelpoel", Instrument = Instruments.Tuba },
                new Musician { Id = 22, Name = "Pieterjan Vrankx", Instrument = Instruments.Percussion },
                new Musician { Id = 23, Name = "Cristiano Menegazzo", Instrument = Instruments.Percussion }
                );

            _ = modelBuilder
                .Entity<Orchestra>()
                .HasMany(o => o.Musicians)
                .WithMany(i => i.Orchestras)
                .UsingEntity(e => e.HasData(
                    new { OrchestrasId = 1, MusiciansId = 2 },
                    new { OrchestrasId = 1, MusiciansId = 3 },
                    new { OrchestrasId = 1, MusiciansId = 4 },
                    new { OrchestrasId = 1, MusiciansId = 5 },
                    new { OrchestrasId = 1, MusiciansId = 6 },
                    new { OrchestrasId = 1, MusiciansId = 7 },
                    new { OrchestrasId = 1, MusiciansId = 8 },
                    new { OrchestrasId = 1, MusiciansId = 9 },
                    new { OrchestrasId = 1, MusiciansId = 10 },
                    new { OrchestrasId = 1, MusiciansId = 11 },
                    new { OrchestrasId = 1, MusiciansId = 12 },
                    new { OrchestrasId = 1, MusiciansId = 13 },
                    new { OrchestrasId = 1, MusiciansId = 14 },
                    new { OrchestrasId = 1, MusiciansId = 15 },
                    new { OrchestrasId = 1, MusiciansId = 16 },
                    new { OrchestrasId = 1, MusiciansId = 17 },
                    new { OrchestrasId = 1, MusiciansId = 18 },
                    new { OrchestrasId = 1, MusiciansId = 19 },
                    new { OrchestrasId = 1, MusiciansId = 20 },
                    new { OrchestrasId = 1, MusiciansId = 21 },
                    new { OrchestrasId = 1, MusiciansId = 22 },
                    new { OrchestrasId = 1, MusiciansId = 23 },
                    new { OrchestrasId = 2, MusiciansId = 2 },
                    new { OrchestrasId = 2, MusiciansId = 10 },
                    new { OrchestrasId = 3, MusiciansId = 4 },
                    new { OrchestrasId = 4, MusiciansId = 1 },
                    new { OrchestrasId = 4, MusiciansId = 4 },
                    new { OrchestrasId = 5, MusiciansId = 4 },
                    new { OrchestrasId = 6, MusiciansId = 6 },
                    new { OrchestrasId = 7, MusiciansId = 7 },
                    new { OrchestrasId = 8, MusiciansId = 9 }
                    ));

            base.OnModelCreating(modelBuilder);
        }
    }
}
