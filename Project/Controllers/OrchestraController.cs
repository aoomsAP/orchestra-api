using Microsoft.AspNetCore.Mvc;
using Project.Entities;
using Project.Services;
using Project.ViewModels;

namespace Project.Controllers
{
    [Route("orchestras")]
    public class OrchestraController : Controller
    {
        private IOrchestraData orchestraData;

        public OrchestraController(IOrchestraData orchestraData)
        {
            this.orchestraData = orchestraData;
        }

        // GET ----------------------------------------------

        [HttpGet]
        [Route("all")]
        public IActionResult GetOrchestraAll()
        {
            var orchestras = new List<OrchestraGetViewModel>();
            foreach (var orchestra in this.orchestraData.GetAll())
            {
                orchestras.Add(new OrchestraGetViewModel { Id = orchestra.Id, Name = orchestra.Name, Conductor = orchestra.Conductor });
            }
            return Ok(orchestras);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOrchestraDetail(int id)
        {
            var viewModel = new OrchestraGetViewModel();

            var orchestra = this.orchestraData.GetDetail(id);
            if (orchestra == null)
            {
                return NotFound("Orchestra not found.");
            }

            viewModel.Id = orchestra.Id;
            viewModel.Name = orchestra.Name;
            viewModel.Conductor = orchestra.Conductor;

            return Ok(viewModel);
        }

        // POST ----------------------------------------------

        [HttpPost]
        [Route("create")]
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
            };

            orchestraData.Add(newOrchestra);
            return CreatedAtAction(nameof(GetOrchestraDetail), new { id = newOrchestra.Id }, newOrchestra);
        }

        // UPDATE ----------------------------------------------

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdateOrchestra(int id, [FromBody] OrchestraUpdateViewModel orchestraUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var oldOrchestra = orchestraData.GetDetail(id);

            if (oldOrchestra == null)
            {
                return NotFound(); // 404
            }

            var newOrchestra = new Orchestra
            {
                Id = oldOrchestra.Id,
                Name = orchestraUpdateViewModel.Name,
                Conductor = orchestraUpdateViewModel.Conductor,
            };

            orchestraData.Update(newOrchestra);

            // a PUT doesn't have to return data because it's merely an update
            // in that case we can use NoContent() to confirm the action was successful but no data needs to be returned:
            // return NoContent(); // 204

            // however we can also return the updated data for clarity
            // in that case we can use CreatedAtAction():
            return CreatedAtAction(nameof(GetOrchestraDetail), new { id = newOrchestra.Id }, newOrchestra); // 201
        }

        // DELETE ----------------------------------------------

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteOrchestra(int id)
        {
            var newOrchestra = orchestraData.GetDetail(id);

            if (newOrchestra == null)
            {
                return NotFound(); // 404
            }

            orchestraData.Delete(newOrchestra);

            return NoContent(); // 204
        }
    }
}
