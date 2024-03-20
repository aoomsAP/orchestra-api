using Project.Entities;

namespace Project.Services
{
    public interface IData
    {
        // Country Data

        IEnumerable<Country> GetCountries();
        Country GetCountry(string code);

        //IEnumerable<Orchestra> GetOrchestrasPerCountry(string code);
        void AddCountry(Country country);
        void DeleteCountry(Country country);
        void UpdateCountry(Country country);

        // Orchestra Data

        IEnumerable<Orchestra> GetOrchestras();
        Orchestra GetOrchestra(int id);

        //IEnumerable<Musician> GetMusiciansPerOrchestra(int id);
        void AddOrchestra(Orchestra orchestra);
        void DeleteOrchestra(Orchestra orchestra);
        void UpdateOrchestra(Orchestra orchestra);

        // Musician Data

        IEnumerable<Musician> GetMusicians();
        Musician GetMusician(int id);

        //IEnumerable<Orchestra> GetOrchestrasPerMusician(int id);
        void AddMusician(Musician musician);
        void DeleteMusician(Musician musician);
        void UpdateMusician(Musician musician);
    }
}
