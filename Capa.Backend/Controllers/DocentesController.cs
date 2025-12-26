using Capa.Backend.Repositories.Intefaces;
using Capa.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Capa.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocentesController : ControllerBase
    {
        private readonly IDocentesRepository _docentesRepository;
        //private readonly IDocentesRepository _docentesRepository;
        public DocentesController(IDocentesRepository docentesRepository)
        {
            _docentesRepository = docentesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var action = await _docentesRepository.GetNewAsync();
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PostAsync([FromBody] DocenteDTO docenteDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(errors);
            }

            var action = await _docentesRepository.AddAsync(docenteDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

    }
}
