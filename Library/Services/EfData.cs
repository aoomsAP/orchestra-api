using Library.Contexts;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using System.Diagnostics.Metrics;

namespace Project.Services
{
    public class EfData : IData
    {
        private DataContext context;

        public EfData(DataContext context)
        {
            this.context = context;
        }

        // country data

        public struct NameAndCode
        {
            public NameAndCode(string name, string code)
            {
                Name = name;
                Code = code;
            }

            public string Name;
            public string Code;
        }

        public IEnumerable<Country> GetCountries()
        {
            return this.context.Countries;
        }

        public Country GetCountry(string code)
        {
            // eager loading
            return this.context.Countries
                .Include(c => c.Orchestras)
                .FirstOrDefault(x => x.Code == code);
        }

        public void AddCountry(Country country)
        {
            this.context.Countries.Add(country);
            this.context.SaveChanges();
        }

        public void DeleteCountry(Country country)
        {
            var toDelete = GetCountry(country.Code);
            this.context.Countries.Remove(toDelete);
            this.context.SaveChanges();
        }

        public void UpdateCountry(Country country)
        {
            var toUpdate = GetCountry(country.Code);
            toUpdate.Name = country.Name;
            this.context.SaveChanges();
        }

        public void UpdateCountryOrchestras(Country country)
        {
            var toUpdate = GetCountry(country.Code);
            toUpdate.Orchestras = country.Orchestras;
            this.context.SaveChanges();
        }

        // orchestra data

        public IEnumerable<Orchestra> GetOrchestras()
        {
            return this.context.Orchestras
                .Include(o => o.Country);
        }

        public Orchestra GetOrchestra(int id)
        {
            // eager loading
            return this.context.Orchestras
                .Include(o => o.Country)
                .Include(o => o.Musicians)
                .FirstOrDefault(x => x.Id == id);
        }

        public void AddOrchestra(Orchestra orchestra)
        {
            this.context.Orchestras.Add(orchestra);
            this.context.SaveChanges();
        }

        public void DeleteOrchestra(Orchestra orchestra)
        {
            var toDelete = GetOrchestra(orchestra.Id);
            this.context.Orchestras.Remove(toDelete);
            this.context.SaveChanges();
        }

        public void UpdateOrchestra(Orchestra orchestra)
        {
            var toUpdate = GetOrchestra(orchestra.Id);
            toUpdate.Name = orchestra.Name;
            toUpdate.Conductor = orchestra.Conductor;
            this.context.SaveChanges();
        }

        // below method updates the listed musicians for an orchestra
        // a) it updates the orchestra relationship for each removed/added musician
        // b) it updates the musicians relationship for the orchestra
        public void UpdateOrchestraMusicians(Orchestra orchestra)
        {
            var toUpdateOrchestra = GetOrchestra(orchestra.Id);

            var oldMusicians = toUpdateOrchestra.Musicians;
            var newMusicians = orchestra.Musicians;

            // REMOVE this orchestra relationship for each musician that was removed
            foreach (var musician in oldMusicians)
            {
                // if the musician from the old list doesn't appear on the new list, that means it's been removed
                if (!newMusicians.Any(m => m.Id == musician.Id))
                {
                    musician.Orchestras.Remove(toUpdateOrchestra);
                }
            }

            // ADD this orchestra relationship for each musician that was added
            foreach (var musician in newMusicians)
            {
                // if the musician from the new list doesn't appear on the old list, that means it's been newly added
                if (!oldMusicians.Any(m => m.Id == musician.Id))
                {
                    musician.Orchestras.Add(toUpdateOrchestra);
                }
            }

            // update orchestra-musicians list on the orchestra
            toUpdateOrchestra.Musicians = newMusicians;

            this.context.SaveChanges();
        }

        // musician data

        public IEnumerable<Musician> GetMusicians()
        {
            return this.context.Musicians;
        }

        public Musician GetMusician(int id)
        {
            // eager loading
            return this.context.Musicians
                .Include(m => m.Orchestras)
                .ThenInclude(o => o.Country)
                .FirstOrDefault(x => x.Id == id);
        }

        public void AddMusician(Musician musician)
        {
            this.context.Musicians.Add(musician);
            this.context.SaveChanges();
        }

        public void DeleteMusician(Musician musician)
        {
            var toDelete = GetMusician(musician.Id);
            this.context.Musicians.Remove(toDelete);
            this.context.SaveChanges();
        }

        public void UpdateMusician(Musician musician)
        {
            var toUpdate = GetMusician(musician.Id);
            toUpdate.Name = musician.Name;
            toUpdate.Instrument = musician.Instrument;
            this.context.SaveChanges();
        }

        // below method updates the listed orchestras for a musician
        // a) it updates the musician relationship for each removed/added orchestra
        // b) it updates the orchestras relationship for the musician
        public void UpdateMusicianOrchestras(Musician musician)
        {
            var toUpdateMusician = GetMusician(musician.Id);

            var oldOrchestras = toUpdateMusician.Orchestras;
            var newOrchestras = musician.Orchestras;

            // REMOVE this musician relationship for each orchestra that was removed
            foreach (var orchestra in oldOrchestras)
            {
                // if the orchestra from the old list doesn't appear on the new list, that means it's been removed
                if (!newOrchestras.Any(o => o.Id == orchestra.Id))
                {
                    orchestra.Musicians.Remove(toUpdateMusician);
                }
            }

            // ADD this musician relationship for each orchestra that was added
            foreach (var orchestra in newOrchestras)
            {
                // if the orchestra from the new list doesn't appear on the old list, that means it's been newly added
                if (!oldOrchestras.Any(o => o.Id == orchestra.Id))
                {
                    orchestra.Musicians.Add(toUpdateMusician);
                }
            }

            // update orchestra-musicians list on the orchestra
            toUpdateMusician.Orchestras = newOrchestras;

            this.context.SaveChanges();
        }

    }
}
