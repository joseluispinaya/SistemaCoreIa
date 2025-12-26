using Capa.Backend.Repositories.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace Capa.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudiantesRepository _estudiantesRepository;
        public EstudiantesController(IEstudiantesRepository estudiantesRepository)
        {
            _estudiantesRepository = estudiantesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var action = await _estudiantesRepository.GetAsync();
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}
