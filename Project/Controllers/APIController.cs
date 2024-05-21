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

        private IData data;

        public APIController(IData data)
        {
            this.data = data;
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
            foreach (var country in this.data.GetCountries())
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
            var country = this.data.GetCountry(code);
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
            var country = this.data.GetCountry(code);
            if (country == null)
            {
                return NotFound("Country not found.");
            }

            // for each relationship (orchestra), get copy that adheres to view model (exclude relationships (musicians) list)
            var orchestras = new List<OrchestraGetViewModel>();
            foreach (var orchestra in country.Orchestras)
            {
                orchestras.Add(new OrchestraGetViewModel { Id = orchestra.Id, Name = orchestra.Name, Conductor = orchestra.Conductor ?? null, Country = orchestra.Country?.Name ?? null });
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
            this.data.AddCountry(newCountry);
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
        public IActionResult UpdateCountry(string code, [FromBody] CountryUpdateViewModel viewModel)
        {
            // check if model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            // check if country exists
            var oldCountry = this.data.GetCountry(code);
            if (oldCountry == null)
            {
                return NotFound("Country not found."); // 404
            }

            // create new country with updated values
            var newCountry = new Country
            {
                Code = oldCountry.Code,
                Name = viewModel.Name,
                Orchestras = oldCountry.Orchestras,
            };

            // update country in database
            this.data.UpdateCountry(newCountry);
            return NoContent(); // 204
        }

        [HttpPut()]
        [Route("countries/{code}/orchestras")]
        public IActionResult UpdateCountryOrchestras(string code, [FromBody] CountryOrchestrasUpdateViewModel viewModel)
        {
            // check if model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            // check if country exists
            var oldCountry = this.data.GetCountry(code);
            if (oldCountry == null)
            {
                return NotFound("Country not found."); // 404
            }

            // get list of relationships (orchestras) based on list of ids from model
            // the reason that this happens in the controller instead of the data service,
            // is so we can send a 404 response if an orchestra is not found
            var newOrchestras = new List<Orchestra>();
            foreach (var orchestraId in viewModel.OrchestraIds)
            {
                // check if orchestra exists
                var newOrchestra = this.data.GetOrchestra(orchestraId);
                if (newOrchestra == null)
                {
                    return NotFound($"Orchestra {orchestraId} not found.");
                }

                newOrchestras.Add(newOrchestra);
            }

            // create new country with updated values
            var newCountry = new Country
            {
                Code = oldCountry.Code,
                Name = oldCountry.Name,
                Orchestras = newOrchestras,
            };

            // update country in database
            this.data.UpdateCountryOrchestras(newCountry);
            return NoContent(); // 204
        }

        // DELETE

        [HttpDelete()]
        [Route("countries/{code}")]
        public IActionResult DeleteCountry(string code)
        {
            // check if country exists
            var country = this.data.GetCountry(code);
            if (country == null)
            {
                return NotFound("Country not found."); // 404
            }

            // delete country in database
            this.data.DeleteCountry(country);
            return NoContent(); // 204
        }

        // ORCHESTRAS ----------------------------------------------

        // GET

        [HttpGet()]
        [Route("orchestras")]
        public IActionResult GetOrchestraAll()
        {
            var orchestras = new List<OrchestraGetViewModel>();
            foreach (var orchestra in this.data.GetOrchestras())
            {
                orchestras.Add(new OrchestraGetViewModel { Id = orchestra.Id, Name = orchestra.Name, Conductor = orchestra.Conductor ?? null, Country = orchestra.Country?.Name ?? null });
            }
            return Ok(orchestras);
        }

        [HttpGet()]
        [Route("orchestras/{id}")]
        public IActionResult GetOrchestraDetail(int id)
        {
            var viewModel = new OrchestraGetViewModel();

            var orchestra = this.data.GetOrchestra(id);
            if (orchestra == null)
            {
                return NotFound("Orchestra not found.");
            }

            viewModel.Id = orchestra.Id;
            viewModel.Name = orchestra.Name;
            viewModel.Conductor = orchestra.Conductor ?? null;
            viewModel.Country = orchestra.Country?.Name ?? null;

            return Ok(viewModel);
        }

        [HttpGet()]
        [Route("orchestras/{id}/musicians")]
        public IActionResult GetMusiciansPerOrchestra(int id)
        {
            var orchestra = this.data.GetOrchestra(id);
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
        public IActionResult CreateOrchestra([FromBody] OrchestraCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newOrchestra = new Orchestra
            {
                Name = viewModel.Name,
                Conductor = viewModel.Conductor,
                Country = this.data.GetCountry(viewModel.CountryCode),
                Musicians = new List<Musician>(),
            };

            this.data.AddOrchestra(newOrchestra);
            return CreatedAtAction(nameof(GetOrchestraDetail), new { id = newOrchestra.Id }, new OrchestraGetViewModel { Id = newOrchestra.Id, Name = newOrchestra.Name, Conductor = newOrchestra.Conductor ?? null, Country = newOrchestra.Country?.Name ?? null });
        }

        // UPDATE

        // updates an orchestra
        // expects the orchestra name & the orchestra conductor
        [HttpPut()]
        [Route("orchestras/{id}")]
        public IActionResult UpdateOrchestra(int id, [FromBody] OrchestraUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var oldOrchestra = this.data.GetOrchestra(id);
            if (oldOrchestra == null)
            {
                return NotFound("Orchestra not found."); // 404
            }

            var newOrchestra = new Orchestra
            {
                Id = oldOrchestra.Id,
                Name = viewModel.Name,
                Conductor = viewModel.Conductor,
                Country = this.data.GetCountry(viewModel.CountryCode),
                Musicians = oldOrchestra.Musicians,
            };

            this.data.UpdateOrchestra(newOrchestra);
            return NoContent(); // 204
        }

        // updates the list of musicians for an orchestra
        // expects a list of musician ids
        [HttpPut()]
        [Route("orchestras/{id}/musicians")]
        public IActionResult UpdateOrchestraMusicians(int id, [FromBody] OrchestraMusiciansUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var oldOrchestra = this.data.GetOrchestra(id);
            if (oldOrchestra == null)
            {
                return NotFound("Orchestra not found."); // 404
            }

            // check if musician ids refer to musicians that exist
            var updatedMusicians = new List<Musician>();
            foreach (var musicianId in viewModel.MusicianIds)
            {
                var newMusician = this.data.GetMusician(musicianId);
                if (newMusician == null)
                {
                    return NotFound($"Musician {musicianId} not found.");
                }
                updatedMusicians.Add(newMusician);
            }

            var newOrchestra = new Orchestra
            {
                Id = oldOrchestra.Id,
                Name = oldOrchestra.Name,
                Conductor = oldOrchestra.Conductor,
                Country = oldOrchestra.Country,
                Musicians = updatedMusicians,
            };

            this.data.UpdateOrchestraMusicians(newOrchestra);
            return NoContent(); // 204
        }

        // DELETE

        [HttpDelete()]
        [Route("orchestras/{id}")]
        public IActionResult DeleteOrchestra(int id)
        {
            var orchestra = this.data.GetOrchestra(id);
            if (orchestra == null)
            {
                return NotFound("Orchestra not found."); // 404
            }

            this.data.DeleteOrchestra(orchestra);
            return NoContent(); // 204
        }

        // MUSICIANS ----------------------------------------------

        // GET

        [HttpGet()]
        [Route("musicians")]
        public IActionResult GetMusicianAll()
        {
            var musicians = new List<MusicianGetViewModel>();
            foreach (var musician in this.data.GetMusicians())
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

            var musician = this.data.GetMusician(id);
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
            var musician = this.data.GetMusician(id);
            if (musician == null)
            {
                return NotFound("Musician not found.");
            }

            var orchestras = new List<OrchestraGetViewModel>();
            foreach (var orchestra in musician.Orchestras)
            {
                orchestras.Add(new OrchestraGetViewModel() { Id = orchestra.Id, Name = orchestra.Name, Conductor = orchestra.Conductor ?? null, Country = orchestra.Country?.Name ?? null });
            }

            return Ok(orchestras);
        }

        // POST

        [HttpPost()]
        [Route("musicians")]
        public IActionResult CreateMusician([FromBody] MusicianCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newMusician = new Musician
            {
                Name = viewModel.Name,
                Instrument = viewModel.Instrument,
                Orchestras = new List<Orchestra>(),
            };

            this.data.AddMusician(newMusician);
            return CreatedAtAction(nameof(GetMusicianDetail), new { id = newMusician.Id }, newMusician);
        }

        // UPDATE

        // updates a musician
        // expects the musician name & the musician instrument
        [HttpPut()]
        [Route("musicians/{id}")]
        public IActionResult UpdateMusician(int id, [FromBody] MusicianUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var oldMusician = this.data.GetMusician(id);
            if (oldMusician == null)
            {
                return NotFound("Musician not found."); // 404
            }

            var newMusician = new Musician
            {
                Id = oldMusician.Id,
                Name = viewModel.Name,
                Instrument = viewModel.Instrument,
                Orchestras = oldMusician.Orchestras,
            };

            this.data.UpdateMusician(newMusician);
            return NoContent(); // 204
        }

        // updates the list of orchestras for a musician
        // expects a list of orchestra ids
        [HttpPut()]
        [Route("musicians/{id}/orchestras")]
        public IActionResult UpdateMusicianOrchestras(int id, [FromBody] MusicianOrchestrasUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var oldMusician = this.data.GetMusician(id);
            if (oldMusician == null)
            {
                return NotFound("Musician not found."); // 404
            }

            // check if orchestra ids refer to orchestras that exist
            var newOrchestras = new List<Orchestra>();
            foreach (var orchestraId in viewModel.OrchestraIds)
            {
                var newOrchestra = this.data.GetOrchestra(orchestraId);
                if (newOrchestra == null)
                {
                    return NotFound($"Orchestra {orchestraId} not found.");
                }

                newOrchestras.Add(newOrchestra);
            }

            var newMusician = new Musician
            {
                Id = oldMusician.Id,
                Name = oldMusician.Name,
                Instrument = oldMusician.Instrument,
                Orchestras = newOrchestras,
            };

            this.data.UpdateMusicianOrchestras(newMusician);
            return NoContent(); // 204
        }

        // DELETE

        [HttpDelete()]
        [Route("musicians/{id}")]
        public IActionResult DeleteMusician(int id)
        {
            var musician = this.data.GetMusician(id);
            if (musician == null)
            {
                return NotFound("Musician not found."); // 404
            }

            this.data.DeleteMusician(musician);
            return NoContent(); // 204
        }
    }
}
