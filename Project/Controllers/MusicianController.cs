using Microsoft.AspNetCore.Mvc;
using Project.Entities;
using Project.Services;
using Project.ViewModels;

namespace Project.Controllers
{
    [Route("musicians")]
    public class MusicianController : Controller
    {
        private IMusicianData musicianData;

        public MusicianController(IMusicianData musicianData)
        {
            this.musicianData = musicianData;
        }

        // GET ----------------------------------------------

        [HttpGet]
        [Route("all")]
        public IActionResult GetMusicianAll()
        {
            var musicians = new List<MusicianGetViewModel>();
            foreach (var musician in this.musicianData.GetAll())
            {
                musicians.Add(new MusicianGetViewModel { Id = musician.Id, Name = musician.Name, Instrument = musician.Instrument });
            }
            return Ok(musicians);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetMusicianDetail(int id)
        {
            var viewModel = new MusicianGetViewModel();

            var musician = this.musicianData.GetDetail(id);
            if (musician == null)
            {
                return NotFound("Musician not found.");
            }

            viewModel.Id = musician.Id;
            viewModel.Name = musician.Name;
            viewModel.Instrument = musician.Instrument;

            return Ok(viewModel);
        }

        // POST ----------------------------------------------

        [HttpPost]
        [Route("create")]
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

            };

            musicianData.Add(newMusician);
            return CreatedAtAction(nameof(GetMusicianDetail), new { id = newMusician.Id }, newMusician);
        }

        // UPDATE ----------------------------------------------

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdateMusician(int id, [FromBody] MusicianUpdateViewModel musicianUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var oldMusician = musicianData.GetDetail(id);

            if (oldMusician == null)
            {
                return NotFound(); // 404
            }

            var newMusician = new Musician
            {
                Id = oldMusician.Id,
                Name = musicianUpdateViewModel.Name,
                Instrument = musicianUpdateViewModel.Instrument,
            };

            musicianData.Update(newMusician);

            // a PUT doesn't have to return data because it's merely an update
            // in that case we can use NoContent() to confirm the action was successful but no data needs to be returned:
            // return NoContent(); // 204

            // however we can also return the updated data for clarity
            // in that case we can use CreatedAtAction():
            return CreatedAtAction(nameof(GetMusicianDetail), new { id = newMusician.Id }, newMusician); // 201
        }

        // DELETE ----------------------------------------------

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteMusician(int id)
        {
            var newMusician = musicianData.GetDetail(id);

            if (newMusician == null)
            {
                return NotFound(); // 404
            }

            musicianData.Delete(newMusician);

            return NoContent(); // 204
        }

    }
}
