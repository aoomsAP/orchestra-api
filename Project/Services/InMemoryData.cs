using Project.Entities;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Reflection;
using System.Security.Claims;

namespace Project.Services
{
    public class InMemoryData : IData
    {
        private static List<Country> countries;

        private static List<Orchestra> orchestras;

        private static List<Musician> musicians;

        static InMemoryData()
        {
            // country data 

            var australia = new Country { Name = "Australia", Code = "AU", Orchestras = new List<Orchestra>()};
            var belgium = new Country { Name = "Belgium", Code = "BE", Orchestras = new List<Orchestra>()};
            var colombia = new Country { Name = "Colombia", Code = "CO", Orchestras = new List<Orchestra>()};
            var germany = new Country { Name = "Germany", Code = "DE", Orchestras = new List<Orchestra>()};
            var hungary = new Country { Name = "Hungary", Code = "HU", Orchestras = new List<Orchestra>()};
            var lebanon = new Country { Name = "Lebanon", Code = "LB", Orchestras = new List<Orchestra>()};
            var singapore = new Country { Name = "Singapore", Code = "SG", Orchestras = new List<Orchestra>()};
            var southafrica = new Country { Name = "South Africa", Code = "ZA", Orchestras = new List<Orchestra>()};
            var taiwan = new Country { Name = "Taiwan", Code = "TW", Orchestras = new List<Orchestra>()};
            var netherlands = new Country { Name = "Netherlands", Code = "NL", Orchestras = new List<Orchestra>()};
            var uk = new Country { Name = "United Kingdom", Code = "UK", Orchestras = new List<Orchestra>()};

            // orchestra data

            var antwerpSymphonyOrchestra = new Orchestra { Id = 1, Name = "Antwerp Symphony Orchestra", Conductor = "Elim Chan", Musicians = new List<Musician>() };
            var nederlandsPhilharmonischOrkest = new Orchestra { Id = 2, Name = "Nederlands Philharmonisch Orkest", Conductor = "Lorenzo Viotti", Musicians = new List<Musician>() };
            var brusselsPhilharmonic = new Orchestra { Id = 3, Name = "Brussels Philharmonic", Conductor = "Kazushi Ono", Musicians = new List<Musician>() };
            var symfonieorkestVanDeMunt = new Orchestra { Id = 4, Name = "Symfonieorkest van de Munt", Conductor = "Alain Altinoglu", Musicians = new List<Musician>() };
            var brusselsSinfonietta = new Orchestra { Id = 5, Name = "Brussels Sinfonietta", Conductor = "Erik Sluys", Musicians = new List<Musician>() };
            var amsterdamSinfonietta = new Orchestra { Id = 6, Name = "Amsterdam Sinfonietta", Conductor = "Candida Thompson", Musicians = new List<Musician>() };
            var londonPhilharmonicOrchestra = new Orchestra { Id = 7, Name = "London Philharmonic Orchestra", Conductor = "Edward Gardner", Musicians = new List<Musician>() };
            var muncherPhilharmoniker = new Orchestra { Id = 8, Name = "Münchner Philharmoniker", Conductor = "Lahav Shani", Musicians = new List<Musician>() };

            // musician data

            var sylvia = new Musician { Id = 1, Name = "Sylvia Huang", Instrument = Instruments.Violin, Orchestras = new List<Orchestra>() };
            var maria = new Musician { Id = 2, Name = "Maria Kouznetsova", Instrument = Instruments.Violin, Orchestras = new List<Orchestra>() };
            var christophe = new Musician { Id = 3, Name = "Christophe Mourguiart", Instrument = Instruments.Violin, Orchestras = new List<Orchestra>() };
            var mona = new Musician { Id = 4, Name = "Mona Verhas", Instrument = Instruments.Violin, Orchestras = new List<Orchestra>() };
            var orsolya = new Musician { Id = 5, Name = "Orsolya Horváth", Instrument = Instruments.Violin, Orchestras = new List<Orchestra>() };
            var miki = new Musician { Id = 6, Name = "Miki Tsunoda", Instrument = Instruments.Violin, Orchestras = new List<Orchestra>() };
            var sander = new Musician { Id = 7, Name = "Sander Geerts", Instrument = Instruments.Viola, Orchestras = new List<Orchestra>() };
            var elaine = new Musician { Id = 8, Name = "Elaine Ng", Instrument = Instruments.Viola, Orchestras = new List<Orchestra>() };
            var raphael = new Musician { Id = 9, Name = "Raphael Bell", Instrument = Instruments.Cello, Orchestras = new List<Orchestra>() };
            var marc = new Musician { Id = 10, Name = "Marc Vossen", Instrument = Instruments.Cello, Orchestras = new List<Orchestra>() };
            var ioan = new Musician { Id = 11, Name = "Ioan Baranga", Instrument = Instruments.DoubleBass, Orchestras = new List<Orchestra>() };
            var vlad = new Musician { Id = 12, Name = "Vlad Rațiu", Instrument = Instruments.DoubleBass, Orchestras = new List<Orchestra>() };
            var aldo = new Musician { Id = 13, Name = "Aldo Baerten", Instrument = Instruments.Flute, Orchestras = new List<Orchestra>() }; ;
            var peter = new Musician { Id = 14, Name = "Peter Verhoyen", Instrument = Instruments.Flute, Orchestras = new List<Orchestra>() };
            var louis = new Musician { Id = 15, Name = "Louis Baumann", Instrument = Instruments.Oboe, Orchestras = new List<Orchestra>() };
            var nele = new Musician { Id = 16, Name = "Nele Delafonteyne", Instrument = Instruments.Clarinet, Orchestras = new List<Orchestra>() };
            var oliver = new Musician { Id = 17, Name = "Oliver Engels", Instrument = Instruments.Bassoon, Orchestras = new List<Orchestra>() };
            var michaela = new Musician { Id = 18, Name = "Michaela Bužková", Instrument = Instruments.Horn, Orchestras = new List<Orchestra>() };
            var alain = new Musician { Id = 19, Name = "Alain De Rudder", Instrument = Instruments.Trumpet, Orchestras = new List<Orchestra>() };
            var daniel = new Musician { Id = 20, Name = "Daniel Quiles Cascant", Instrument = Instruments.Trombone, Orchestras = new List<Orchestra>() };
            var bernd = new Musician { Id = 21, Name = "Bernd van Echelpoel", Instrument = Instruments.Tuba, Orchestras = new List<Orchestra>() };
            var pieterjan = new Musician { Id = 22, Name = "Pieterjan Vrankx", Instrument = Instruments.Percussion, Orchestras = new List<Orchestra>() };
            var cristiano = new Musician { Id = 23, Name = "Cristiano Menegazzo", Instrument = Instruments.Percussion, Orchestras = new List<Orchestra>() };

            // adding country relationships data

            belgium.Orchestras.Add(antwerpSymphonyOrchestra);
            belgium.Orchestras.Add(brusselsPhilharmonic);
            belgium.Orchestras.Add(symfonieorkestVanDeMunt);
            belgium.Orchestras.Add(brusselsSinfonietta);
            germany.Orchestras.Add(muncherPhilharmoniker);
            netherlands.Orchestras.Add(nederlandsPhilharmonischOrkest);
            netherlands.Orchestras.Add(amsterdamSinfonietta);
            uk.Orchestras.Add(londonPhilharmonicOrchestra);

            // adding orchestra relationships data

            antwerpSymphonyOrchestra.Musicians.Add(maria);
            antwerpSymphonyOrchestra.Musicians.Add(christophe);
            antwerpSymphonyOrchestra.Musicians.Add(mona);
            antwerpSymphonyOrchestra.Musicians.Add(orsolya);
            antwerpSymphonyOrchestra.Musicians.Add(miki);
            antwerpSymphonyOrchestra.Musicians.Add(sander);
            antwerpSymphonyOrchestra.Musicians.Add(elaine);
            antwerpSymphonyOrchestra.Musicians.Add(raphael);
            antwerpSymphonyOrchestra.Musicians.Add(marc);
            antwerpSymphonyOrchestra.Musicians.Add(ioan);
            antwerpSymphonyOrchestra.Musicians.Add(vlad);
            antwerpSymphonyOrchestra.Musicians.Add(aldo);
            antwerpSymphonyOrchestra.Musicians.Add(peter);
            antwerpSymphonyOrchestra.Musicians.Add(louis);
            antwerpSymphonyOrchestra.Musicians.Add(nele);
            antwerpSymphonyOrchestra.Musicians.Add(oliver);
            antwerpSymphonyOrchestra.Musicians.Add(michaela);
            antwerpSymphonyOrchestra.Musicians.Add(alain);
            antwerpSymphonyOrchestra.Musicians.Add(daniel);
            antwerpSymphonyOrchestra.Musicians.Add(bernd);
            antwerpSymphonyOrchestra.Musicians.Add(pieterjan);
            antwerpSymphonyOrchestra.Musicians.Add(cristiano);
            nederlandsPhilharmonischOrkest.Musicians.Add(maria);
            nederlandsPhilharmonischOrkest.Musicians.Add(marc);
            brusselsPhilharmonic.Musicians.Add(mona);
            symfonieorkestVanDeMunt.Musicians.Add(sylvia);
            symfonieorkestVanDeMunt.Musicians.Add(mona);
            brusselsSinfonietta.Musicians.Add(mona);
            amsterdamSinfonietta.Musicians.Add(miki);
            londonPhilharmonicOrchestra.Musicians.Add(sander);
            muncherPhilharmoniker.Musicians.Add(raphael);

            // adding musician relationships data

            sylvia.Orchestras.Add(symfonieorkestVanDeMunt);
            maria.Orchestras.Add(antwerpSymphonyOrchestra);
            maria.Orchestras.Add(nederlandsPhilharmonischOrkest);
            christophe.Orchestras.Add(antwerpSymphonyOrchestra);
            mona.Orchestras.Add(antwerpSymphonyOrchestra);
            mona.Orchestras.Add(symfonieorkestVanDeMunt);
            mona.Orchestras.Add(brusselsPhilharmonic);
            mona.Orchestras.Add(brusselsSinfonietta);
            orsolya.Orchestras.Add(antwerpSymphonyOrchestra);
            miki.Orchestras.Add(antwerpSymphonyOrchestra);
            miki.Orchestras.Add(amsterdamSinfonietta);
            sander.Orchestras.Add(antwerpSymphonyOrchestra);
            sander.Orchestras.Add(londonPhilharmonicOrchestra);
            elaine.Orchestras.Add(antwerpSymphonyOrchestra);
            raphael.Orchestras.Add(antwerpSymphonyOrchestra);
            raphael.Orchestras.Add(muncherPhilharmoniker);
            marc.Orchestras.Add(antwerpSymphonyOrchestra);
            marc.Orchestras.Add(nederlandsPhilharmonischOrkest);
            vlad.Orchestras.Add(antwerpSymphonyOrchestra);
            aldo.Orchestras.Add(antwerpSymphonyOrchestra);
            peter.Orchestras.Add(antwerpSymphonyOrchestra);
            louis.Orchestras.Add(antwerpSymphonyOrchestra);
            nele.Orchestras.Add(antwerpSymphonyOrchestra);
            oliver.Orchestras.Add(antwerpSymphonyOrchestra);
            michaela.Orchestras.Add(antwerpSymphonyOrchestra);
            alain.Orchestras.Add(antwerpSymphonyOrchestra);
            daniel.Orchestras.Add(antwerpSymphonyOrchestra);
            bernd.Orchestras.Add(antwerpSymphonyOrchestra);
            pieterjan.Orchestras.Add(antwerpSymphonyOrchestra);
            cristiano.Orchestras.Add(antwerpSymphonyOrchestra);

            // add objects to static lists

            countries = new List<Country>()
            {
                australia, belgium, colombia, germany, hungary, lebanon, singapore, southafrica, taiwan, netherlands, uk,
            };

            orchestras = new List<Orchestra>()
            {
                antwerpSymphonyOrchestra, nederlandsPhilharmonischOrkest, brusselsPhilharmonic, symfonieorkestVanDeMunt, brusselsSinfonietta, amsterdamSinfonietta, londonPhilharmonicOrchestra, muncherPhilharmoniker,
            };

            musicians = new List<Musician>()
            {
                sylvia, maria, christophe, mona, orsolya, miki, sander, elaine, raphael, marc, ioan, vlad, aldo, peter, louis, nele, oliver, michaela, alain, daniel, bernd, pieterjan, cristiano,
            };
        }

        // country data

        public IEnumerable<Country> GetCountries()
        {
            return countries;
        }

        public Country GetCountry(string code)
        {
            return countries.FirstOrDefault(x => x.Code == code);
        }

        public void AddCountry(Country country)
        {
            countries.Add(country);
        }

        public void DeleteCountry(Country country)
        {
            countries.Remove(country);
        }

        public void UpdateCountry(Country country)
        {
            var oldCountry = countries.FirstOrDefault(x => x.Code == country.Code);
            oldCountry.Name = country.Name;
            oldCountry.Orchestras = country.Orchestras;
        }

        // orchestra data

        public IEnumerable<Orchestra> GetOrchestras()
        {
            return orchestras;
        }

        public Orchestra GetOrchestra(int id)
        {
            return orchestras.FirstOrDefault(x => x.Id == id);
        }

        public void AddOrchestra(Orchestra orchestra)
        {
            orchestra.Id = orchestras.Max(x => x.Id) + 1;
            orchestras.Add(orchestra);
        }

        public void DeleteOrchestra(Orchestra orchestra)
        {
            orchestras.Remove(orchestra);
        }

        public void UpdateOrchestra(Orchestra orchestra)
        {
            var updatedOrchestra = GetOrchestra(orchestra.Id);
            updatedOrchestra.Name = orchestra.Name;
            updatedOrchestra.Conductor = orchestra.Conductor;
            updatedOrchestra.Musicians = orchestra.Musicians;
        }

        // musician data

        public IEnumerable<Musician> GetMusicians()
        {
            return musicians;
        }

        public Musician GetMusician(int id)
        {
            return musicians.FirstOrDefault(x => x.Id == id);
        }

        public void AddMusician(Musician musician)
        {
            musician.Id = musicians.Max(x => x.Id) + 1;
            musicians.Add(musician);
        }

        public void DeleteMusician(Musician musician)
        {
            musicians.Remove(musician);
        }

        public void UpdateMusician(Musician musician)
        {
            var oldMusician = musicians.FirstOrDefault(x => x.Id == musician.Id);
            oldMusician.Name = musician.Name;
            oldMusician.Instrument = musician.Instrument;
            oldMusician.Orchestras = musician.Orchestras;
        }
    }
}
