using Project.Entities;

namespace Project.Services
{
    public interface IOrchestraData
    {
        IEnumerable<Orchestra> GetAll();
        Orchestra GetDetail(int id);
        void Add(Orchestra orchestra);
        void Delete(Orchestra orchestra);
        void Update(Orchestra orchestra);
    }

    public class InMemoryOrchestraData : IOrchestraData
    {
        public static List<Orchestra> Orchestras { get; set;}

        static InMemoryOrchestraData()
        {
            // is this the way to add Musicians?

            var sylvia = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 1);
            var maria = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 2);
            var christophe = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 3);
            var mona = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 4);
            var orsolya = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 5);
            var miki = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 6);
            var sander = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 7);
            var elaine = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 8);
            var raphael = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 9);
            var marc = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 10);
            var ioan = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 11);
            var vlad = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 12);
            var aldo = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 13);
            var peter = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 14);
            var louis = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 15);
            var nele = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 16);
            var oliver = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 17);
            var michaela = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 18);
            var alain = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 19);
            var daniel = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 20);
            var bernd = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 21);
            var pieterjan = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 22);
            var cristiano = InMemoryMusicianData.Musicians.FirstOrDefault(x => x.Id == 23);

            Orchestras = new List<Orchestra>()
            {
                new Orchestra {Id = 1, Name = "Antwerp Symphony Orchestra", Conductor = "Elim Chan", Musicians = new List<Musician> {maria, christophe, mona, orsolya, miki, sander, elaine, raphael, marc, ioan, vlad, aldo, peter, louis, nele, oliver, michaela, alain, daniel, bernd, pieterjan, cristiano} },
                new Orchestra {Id = 2, Name = "Nederlands Philharmonisch Orkest", Conductor = "Lorenzo Viotti", Musicians = new List<Musician> {maria, marc} },
                new Orchestra {Id = 3, Name = "Brussels Philharmonic", Conductor = "Kazushi Ono", Musicians = new List<Musician> {mona}},
                new Orchestra {Id = 4, Name = "Symfonieorkest van de Munt", Conductor = "Alain Altinoglu", Musicians = new List<Musician> {sylvia, mona}},
                new Orchestra {Id = 5, Name = "Brussels Sinfonietta", Conductor = "Erik Sluys", Musicians = new List<Musician> {mona}},
                new Orchestra {Id = 6, Name = "Amsterdam Sinfonietta", Conductor = "Candida Thompson", Musicians = new List<Musician> {miki}},
                new Orchestra {Id = 7, Name = "London Philharmonic Orchestra", Conductor = "Edward Gardner", Musicians = new List<Musician> {sander}},
                new Orchestra {Id = 8, Name = "Münchner Philharmoniker", Conductor = "Lahav Shani", Musicians = new List < Musician > { raphael }},
            };
        }

        public IEnumerable<Orchestra> GetAll()
        {
            return Orchestras;
        }

        public Orchestra GetDetail(int id)
        {
            return Orchestras.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Orchestra orchestra)
        {
            orchestra.Id = Orchestras.Max(x => x.Id) + 1;
            Orchestras.Add(orchestra);
        }

        public void Delete(Orchestra orchestra)
        {
            Orchestras.Remove(orchestra);
        }

        public void Update(Orchestra orchestra)
        {
            var updatedOrchestra = GetDetail(orchestra.Id);
            updatedOrchestra.Name = orchestra.Name;
            updatedOrchestra.Conductor = orchestra.Conductor;
            updatedOrchestra.Musicians = orchestra.Musicians;
        }
    }
}
