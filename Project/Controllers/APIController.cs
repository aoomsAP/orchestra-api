using Microsoft.AspNetCore.Mvc;
using Project.Entities;
using Project.Services;
using Project.ViewModels;

namespace Project.Controllers
{
    [Route("api")]
    public class APIController : Controller
    {
        // this API controller class contains endpoints for 3 entities: Country, Orchestra, and Musician
        // the logic for these endpoints is roughly the same for all entities
        // further documentation is provided for the first entity, Country, as to not clutter the entire class

        private IData inMemoryData;

        public APIController(IData inMemoryData)
        {
            this.inMemoryData = inMemoryData;
        }

        // COUNTRIES ----------------------------------------------

        // GET

        // the below methods make use of a GetViewModel
        // its purpose is to exclude the list of relationships from the view
        // this is necessary to avoid an endless loop, given that the many/many relationships endlessly refer to each other

        [HttpGet()]
        [Route("countries")]
        public IActionResult GetCountryAll()
        {
            // for each country in the database, collect copy that adheres to view model (exclude relationships (orchestras) list)
            var countries = new List<CountryGetViewModel>();
            foreach (var country in this.inMemoryData.GetCountries())
            {
                countries.Add(new CountryGetViewModel { Code = country.Code, Name = country.Name });
            }

            // return list of view model countries
            return Ok(countries);
        }

        [HttpGet()]
        [Route("countries/{code}")]
        public IActionResult GetCountryDetail(string code)
        {
            var viewModel = new CountryGetViewModel();

            // check if country exists in database
            var country = this.inMemoryData.GetCountry(code);
            if (country == null)
            {
                return NotFound("Country not found.");
            }

            // update view model with data from database
            viewModel.Name = country.Name;
            viewModel.Code = country.Code;

            // return view model
            return Ok(viewModel);
        }

        [HttpGet()]
        [Route("countries/{code}/orchestras")]
        public IActionResult GetOrchestrasPerCountry(string code)
        {
            // check if country exists
            var country = this.inMemoryData.GetCountry(code);
            if (country == null)
            {
                return NotFound("Country not found.");
            }

            // for each relationship (orchestra), get copy that adheres to view model (exclude relationships (musicians) list)
            var orchestras = new List<OrchestraGetViewModel>();
            foreach (var orchestra in country.Orchestras)
            {
                orchestras.Add(new OrchestraGetViewModel { Id = orchestra.Id, Name = orchestra.Name, Conductor = orchestra.Conductor });
            }

            // return list of view model relationships (orchestras)
            return Ok(orchestras);
        }

        // POST

        [HttpPost()]
        [Route("countries")]
        public IActionResult CreateCountry([FromBody] CountryCreateViewModel countryCreateViewModel)
        {
            // check if modelstate is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // create new country based on view model data + initialize empty list of relationships
            var newCountry = new Country
            {
                Code = countryCreateViewModel.Code,
                Name = countryCreateViewModel.Name,
                Orchestras = new List<Orchestra>(),
            };

            // add country to database & return object that was just created
            this.inMemoryData.AddCountry(newCountry);
            return CreatedAtAction(nameof(GetCountryDetail), new { code = newCountry.Code }, newCountry);
        }

        // UPDATE

        // the below method makes use of a CountryUpdateViewModel
        // it consists of updated data + a list of ids that refer to the relationships (orchestras) for this object
        // a) both regular data & relationships are updated in this method
        // b) the httpput/update takes care of all changes, whether they alter data, add relationships, or remove relationships
        // therefore, no other controller methods are necessary

        [HttpPut()]
        [Route("countries/{code}")]
        public IActionResult UpdateCountry(string code, [FromBody] CountryUpdateViewModel countryUpdateViewModel)
        {
            // check if model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            // check if country exists
            var oldCountry = this.inMemoryData.GetCountry(code);
            if (oldCountry == null)
            {
                return NotFound("Country not found."); // 404
            }

            // get list of relationships (orchestras) based on list of ids from model
            var newOrchestras = new List<Orchestra>();
            foreach (var id in countryUpdateViewModel.OrchestraIds)
            {
                // check if orchestra exists
                var newOrchestra = this.inMemoryData.GetOrchestra(id);
                if (newOrchestra == null)
                {
                    return NotFound($"Orchestra {id} not found.");
                }

                newOrchestras.Add(newOrchestra);
            }

            // create new country with updated values
            var newCountry = new Country
            {
                Code = oldCountry.Code,
                Name = countryUpdateViewModel.Name,
                Orchestras = newOrchestras,
            };

            // update country in database
            this.inMemoryData.UpdateCountry(newCountry);
            return NoContent(); // 204
        }

        // DELETE

        [HttpDelete()]
        [Route("countries/{code}")]
        public IActionResult DeleteCountry(string code)
        {
            // check if country exists
            var country = this.inMemoryData.GetCountry(code);
            if (country == null)
            {
                return NotFound("Country not found."); // 404
            }

            // delete country in database
            this.inMemoryData.DeleteCountry(country);
            return NoContent(); // 204
        }

        // ORCHESTRAS ----------------------------------------------

        // GET

        [HttpGet()]
        [Route("orchestras")]
        public IActionResult GetOrchestraAll()
        {
            var orchestras = new List<OrchestraGetViewModel>();
            foreach (var orchestra in this.inMemoryData.GetOrchestras())
            {
                orchestras.Add(new OrchestraGetViewModel { Id = orchestra.Id, Name = orchestra.Name, Conductor = orchestra.Conductor });
            }
            return Ok(orchestras);
        }

        [HttpGet()]
        [Route("orchestras/{id}")]
        public IActionResult GetOrchestraDetail(int id)
        {
            var viewModel = new OrchestraGetViewModel();

            var orchestra = this.inMemoryData.GetOrchestra(id);
            if (orchestra == null)
            {
                return NotFound("Orchestra not found.");
            }

            viewModel.Id = orchestra.Id;
            viewModel.Name = orchestra.Name;
            viewModel.Conductor = orchestra.Conductor;

            return Ok(viewModel);
        }

        [HttpGet()]
        [Route("orchestras/{id}/musicians")]
        public IActionResult GetMusiciansPerOrchestra(int id)
        {
            var orchestra = this.inMemoryData.GetOrchestra(id);
            if (orchestra == null)
            {
                return NotFound("Orchestra not found.");
            }

            var musicians = new List<MusicianGetViewModel>();
            foreach (var musician in orchestra.Musicians)
            {
                musicians.Add(new MusicianGetViewModel() { Id = musician.Id, Name = musician.Name, Instrument = musician.Instrument });
            }

            return Ok(musicians);
        }

        // POST

        [HttpPost()]
        [Route("orchestras")]
        public IActionResult CreateOrchestra([FromBody] OrchestraCreateViewModel orchestraCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newOrchestra = new Orchestra
            {
                Name = orchestraCreateViewModel.Name,
                Conductor = orchestraCreateViewModel.Conductor,
                Musicians = new List<Musician>(),
            };

            this.inMemoryData.AddOrchestra(newOrchestra);
            return CreatedAtAction(nameof(GetOrchestraDetail), new { id = newOrchestra.Id }, newOrchestra);
        }

        // UPDATE

        [HttpPut()]
        [Route("orchestras/{id}")]
        public IActionResult UpdateOrchestra(int id, [FromBody] OrchestraUpdateViewModel orchestraUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var oldOrchestra = this.inMemoryData.GetOrchestra(id);
            if (oldOrchestra == null)
            {
                return NotFound("Orchestra not found."); // 404
            }

            var newMusicians = new List<Musician>();
            foreach (var musicianId in orchestraUpdateViewModel.MusicianIds)
            {
                var newMusician = this.inMemoryData.GetMusician(musicianId);
                if (newMusician == null)
                {
                    return NotFound($"Musician {id} not found.");
                }

                newMusicians.Add(newMusician);
            }

            var newOrchestra = new Orchestra
            {
                Id = oldOrchestra.Id,
                Name = orchestraUpdateViewModel.Name,
                Conductor = orchestraUpdateViewModel.Conductor,
                Musicians = newMusicians,
            };

            this.inMemoryData.UpdateOrchestra(newOrchestra);
            return NoContent(); // 204
        }

        // DELETE

        [HttpDelete()]
        [Route("orchestras/{id}")]
        public IActionResult DeleteOrchestra(int id)
        {
            var newOrchestra = this.inMemoryData.GetOrchestra(id);
            if (newOrchestra == null)
            {
                return NotFound("Orchestra not found."); // 404
            }

            this.inMemoryData.DeleteOrchestra(newOrchestra);
            return NoContent(); // 204
        }

        // MUSICIANS ----------------------------------------------

        // GET

        [HttpGet()]
        [Route("musicians")]
        public IActionResult GetMusicianAll()
        {
            var musicians = new List<MusicianGetViewModel>();
            foreach (var musician in this.inMemoryData.GetMusicians())
            {
                musicians.Add(new MusicianGetViewModel { Id = musician.Id, Name = musician.Name, Instrument = musician.Instrument });
            }
            return Ok(musicians);
        }

        [HttpGet()]
        [Route("musicians/{id}")]
        public IActionResult GetMusicianDetail(int id)
        {
            var viewModel = new MusicianGetViewModel();

            var musician = this.inMemoryData.GetMusician(id);
            if (musician == null)
            {
                return NotFound("Musician not found.");
            }

            viewModel.Id = musician.Id;
            viewModel.Name = musician.Name;
            viewModel.Instrument = musician.Instrument;

            return Ok(viewModel);
        }

        [HttpGet()]
        [Route("musicians/{id}/orchestras")]
        public IActionResult GetOrchestrasPerMusician(int id)
        {
            var musician = this.inMemoryData.GetMusician(id);
            if (musician == null)
            {
                return NotFound("Musician not found.");
            }

            var orchestras = new List<OrchestraGetViewModel>();
            foreach (var orchestra in musician.Orchestras)
            {
                orchestras.Add(new OrchestraGetViewModel() { Id = orchestra.Id, Name = orchestra.Name, Conductor = orchestra.Conductor });
            }

            return Ok(orchestras);
        }

        // POST

        [HttpPost()]
        [Route("musicians")]
        public IActionResult CreateMusician([FromBody] MusicianCreateViewModel musicianCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newMusician = new Musician
            {
                Name = musicianCreateViewModel.Name,
                Instrument = musicianCreateViewModel.Instrument,
                Orchestras = new List<Orchestra>(),
            };

            this.inMemoryData.AddMusician(newMusician);
            return CreatedAtAction(nameof(GetMusicianDetail), new { id = newMusician.Id }, newMusician);
        }

        // UPDATE

        [HttpPut()]
        [Route("musicians/{id}")]
        public IActionResult UpdateMusician(int id, [FromBody] MusicianUpdateViewModel musicianUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var oldMusician = this.inMemoryData.GetMusician(id);
            if (oldMusician == null)
            {
                return NotFound("Musician not found."); // 404
            }

            var newOrchestras = new List<Orchestra>();
            foreach (var orchestraId in musicianUpdateViewModel.OrchestraIds)
            {
                var newOrchestra = this.inMemoryData.GetOrchestra(orchestraId);
                if (newOrchestra == null)
                {
                    return NotFound($"Orchestra {id} not found.");
                }

                newOrchestras.Add(newOrchestra);
            }

            var newMusician = new Musician
            {
                Id = oldMusician.Id,
                Name = musicianUpdateViewModel.Name,
                Instrument = musicianUpdateViewModel.Instrument,
                Orchestras = newOrchestras,
            };

            this.inMemoryData.UpdateMusician(newMusician);
            return NoContent(); // 204
        }

        // DELETE

        [HttpDelete()]
        [Route("musicians/{id}")]
        public IActionResult DeleteMusician(int id)
        {
            var newMusician = this.inMemoryData.GetMusician(id);
            if (newMusician == null)
            {
                return NotFound("Musician not found."); // 404
            }

            this.inMemoryData.DeleteMusician(newMusician);
            return NoContent(); // 204
        }
    }
}
