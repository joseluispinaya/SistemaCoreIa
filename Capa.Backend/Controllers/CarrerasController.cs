using Capa.Backend.Repositories.Intefaces;
using Capa.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Capa.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrerasController : ControllerBase
    {
        private readonly ICarrerasRepository _carrerasRepository;
        //private readonly ICarrerasRepository _carrerasRepository;
        public CarrerasController(ICarrerasRepository carrerasRepository)
        {
            _carrerasRepository = carrerasRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var action = await _carrerasRepository.GetAsync();
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PostAsync([FromBody] Carrera carrera)
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

            var action = await _carrerasRepository.AddAsync(carrera);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

    }
}
