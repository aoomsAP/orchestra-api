using Microsoft.AspNetCore.Mvc;
using Project.Entities;
using Project.Services;
using Project.ViewModels;

namespace Project.Controllers
{
    [Route("countries")]
    public class CountryController : Controller
    {
        private ICountryData countryData;

        public CountryController(ICountryData countryData)
        {
            this.countryData = countryData;
        }

        // GET ----------------------------------------------

        [HttpGet]
        [Route("")]
        public IActionResult GetCountryAll()
        {
            var countries = new List<CountryGetViewModel>();
            foreach (var country in this.countryData.GetAll())
            {
                countries.Add(new CountryGetViewModel { Code = country.Code, Name = country.Name });
            }
            return Ok(countries);
        }

        [HttpGet]
        [Route("{code}")]
        public IActionResult GetCountryDetail(string code)
        {
            var viewModel = new CountryGetViewModel();

            var country = this.countryData.GetDetail(code);
            if (country == null)
            {
                return NotFound("Country not found.");
            }

            viewModel.Name = country.Name;
            viewModel.Code = country.Code;

            return Ok(viewModel);
        }

        // POST ----------------------------------------------

        [HttpPost]
        [Route("create")]
        public IActionResult CreateCountry([FromBody] CountryCreateViewModel countryCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCountry = new Country
            {
                Code = countryCreateViewModel.Code,
                Name = countryCreateViewModel.Name,
            };

            countryData.Add(newCountry);
            return CreatedAtAction(nameof(GetCountryDetail), new { code = newCountry.Code }, newCountry);
        }

        // UPDATE ----------------------------------------------

        [HttpPut]
        [Route("update/{code}")]
        public IActionResult UpdateCountry(string code, [FromBody] CountryUpdateViewModel countryUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var oldCountry = countryData.GetDetail(code);

            if (oldCountry == null)
            {
                return NotFound(); // 404
            }

            var newCountry = new Country
            {
                Code = oldCountry.Code,
                Name = countryUpdateViewModel.Name,
            };

            countryData.Update(newCountry);

            // a PUT doesn't have to return data because it's merely an update
            // in that case we can use NoContent() to confirm the action was successful but no data needs to be returned:
            // return NoContent(); // 204

            // however we can also return the updated data for clarity
            // in that case we can use CreatedAtAction():
            return CreatedAtAction(nameof(GetCountryDetail), new { code = newCountry.Code }, newCountry); // 201
        }

        // DELETE ----------------------------------------------

        [HttpDelete]
        [Route("delete/{code}")]
        public IActionResult DeleteRouteMethod(string code)
        {
            var newCountry = countryData.GetDetail(code);

            if (newCountry == null)
            {
                return NotFound(); // 404
            }

            countryData.Delete(newCountry);

            return NoContent(); // 204
        }

    }
}
