using Project.Entities;
using System.Collections.Immutable;

namespace Project.Services
{
    public interface ICountryData
    {
        IEnumerable<Country> GetAll();
        Country GetDetail(string code);
        void Add(Country country);
        void Delete(Country country);
        void Update(Country country);
    }

    public class InMemoryCountryData : ICountryData
    {
        static List<Country> countries;

        static InMemoryCountryData()
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

            countries = new List<Country>()
            {
                new Country {Name = "Australia", Code = "AU"},
                new Country {Name = "Belgium", Code = "BE", Orchestras = new List<Orchestra> { antwerpSymphonyOrchestra, brusselsPhilharmonic, symfonieorkestVanDeMunt, brusselsSinfonietta } },
                new Country {Name = "Colombia", Code = "CO"},
                new Country {Name = "Germany", Code = "DE", Orchestras = new List<Orchestra> { muncherPhilharmoniker }},
                new Country {Name = "Hungary", Code = "HU"},
                new Country {Name = "Lebanon", Code = "LB"},
                new Country {Name = "Singapore", Code = "SG"},
                new Country {Name = "South Africa", Code = "ZA"},
                new Country {Name = "Taiwan", Code = "TW"},
                new Country {Name = "Netherlands", Code = "NL", Orchestras = new List<Orchestra> { nederlandsPhilharmonischOrkest, amsterdamSinfonietta }},
                new Country {Name = "United Kingdom", Code = "UK", Orchestras = new List<Orchestra> { londonPhilharmonicOrchestra } }
            };
        }

        public IEnumerable<Country> GetAll()
        {
            return countries;
        }

        public Country GetDetail(string code)
        {
            return countries.FirstOrDefault(x => x.Code == code);
        }

        public void Add(Country country)
        {
            countries.Add(country);
        }

        public void Delete(Country country)
        {
            countries.Remove(country);
        }

        public void Update(Country country)
        {
            var oldCountry = countries.FirstOrDefault(x => x.Code == country.Code);
            oldCountry.Name = country.Name;
            oldCountry.Orchestras = country.Orchestras;
        }
    }
}
