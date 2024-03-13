using Project.Entities;
using System;
using System.Collections.Immutable;

namespace Project.Services
{
    public interface IMusicianData
    {
        IEnumerable<Musician> GetAll();
        Musician GetDetail(int id);
        void Add(Musician musician);
        void Delete(Musician musician);
        void Update(Musician musician);
    }

    public class InMemoryMusicianData : IMusicianData
    {

        public static List<Musician> Musicians;

        static InMemoryMusicianData()
        {
            // is this the way to add Orchestras?

            var antwerpSymphonyOrchestra = InMemoryOrchestraData.Orchestras.FirstOrDefault(x => x.Id == 1);
            var nederlandsPhilharmonischOrkest = InMemoryOrchestraData.Orchestras.FirstOrDefault(x => x.Id == 2);
            var brusselsPhilharmonic = InMemoryOrchestraData.Orchestras.FirstOrDefault(x => x.Id == 3);
            var symfonieorkestVanDeMunt = InMemoryOrchestraData.Orchestras.FirstOrDefault(x => x.Id == 4);
            var brusselsSinfonietta = InMemoryOrchestraData.Orchestras.FirstOrDefault(x => x.Id == 5);
            var amsterdamSinfonietta = InMemoryOrchestraData.Orchestras.FirstOrDefault(x => x.Id == 6);
            var londonPhilharmonicOrchestra = InMemoryOrchestraData.Orchestras.FirstOrDefault(x => x.Id == 7);
            var muncherPhilharmoniker = InMemoryOrchestraData.Orchestras.FirstOrDefault(x => x.Id == 8);

            Musicians = new List<Musician>()
            {
                new Musician {Id = 1, Name = "Sylvia Huang", Instrument = Instruments.Violin, Orchestras = new List<Orchestra> { symfonieorkestVanDeMunt }},
                new Musician {Id = 2, Name = "Maria Kouznetsova", Instrument = Instruments.Violin, Orchestras = new List<Orchestra> { antwerpSymphonyOrchestra, nederlandsPhilharmonischOrkest }},
                new Musician {Id = 3, Name = "Christophe Mourguiart", Instrument = Instruments.Violin, Orchestras = new List<Orchestra> { antwerpSymphonyOrchestra }},
                new Musician {Id = 4, Name = "Mona Verhas", Instrument = Instruments.Violin, Orchestras = new List<Orchestra> { antwerpSymphonyOrchestra, symfonieorkestVanDeMunt, brusselsPhilharmonic, brusselsSinfonietta }},
                new Musician {Id = 5, Name = "Orsolya Horváth", Instrument = Instruments.Violin, Orchestras = new List<Orchestra> { antwerpSymphonyOrchestra }},
                new Musician {Id = 6, Name = "Miki Tsunoda", Instrument = Instruments.Violin, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra, amsterdamSinfonietta }},
                new Musician {Id = 7, Name = "Sander Geerts", Instrument = Instruments.Viola, Orchestras = new List<Orchestra> { antwerpSymphonyOrchestra, londonPhilharmonicOrchestra }},
                new Musician {Id = 8, Name = "Elaine Ng", Instrument = Instruments.Viola, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 9, Name = "Raphael Bell", Instrument = Instruments.Cello, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra, muncherPhilharmoniker }},
                new Musician {Id = 10, Name = "Marc Vossen", Instrument = Instruments.Cello, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra,nederlandsPhilharmonischOrkest }},
                new Musician {Id = 11, Name = "Ioan Baranga", Instrument = Instruments.DoubleBass, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 12, Name = "Vlad Rațiu", Instrument = Instruments.DoubleBass, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 13, Name = "Aldo Baerten", Instrument = Instruments.Flute, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 14, Name = "Peter Verhoyen", Instrument = Instruments.Flute, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 15, Name = "Louis Baumann", Instrument = Instruments.Oboe, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 16, Name = "Nele Delafonteyne", Instrument = Instruments.Clarinet, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 17, Name = "Oliver Engels", Instrument = Instruments.Bassoon, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 18, Name = "Michaela Bužková", Instrument = Instruments.Horn, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 19, Name = "Alain De Rudder", Instrument = Instruments.Trumpet, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 20, Name = "Daniel Quiles Cascant", Instrument = Instruments.Trombone, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 21, Name = "Bernd van Echelpoe", Instrument = Instruments.Tuba, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 22, Name = "Pieterjan Vrankx", Instrument = Instruments.Percussion, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
                new Musician {Id = 23, Name = "Cristiano Menegazzo", Instrument = Instruments.Percussion, Orchestras = new List < Orchestra > { antwerpSymphonyOrchestra }},
            };
        }

        public IEnumerable<Musician> GetAll()
        {
            return Musicians;
        }

        public Musician GetDetail(int id)
        {
            return Musicians.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Musician musician)
        {
            musician.Id = Musicians.Max(x => x.Id) + 1;
            Musicians.Add(musician);
        }

        public void Delete(Musician musician)
        {
            Musicians.Remove(musician);
        }

        public void Update(Musician musician)
        {
            var oldMusician = Musicians.FirstOrDefault(x => x.Id == musician.Id);
            oldMusician.Name = musician.Name;
            oldMusician.Instrument = musician.Instrument;
            oldMusician.Orchestras = musician.Orchestras;
        }
    }
}
