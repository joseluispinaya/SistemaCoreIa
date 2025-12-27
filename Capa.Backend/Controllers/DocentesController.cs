using Capa.Backend.Helpers;
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
        private readonly IIARecommendationService _iARecommendationService;
        public DocentesController(IDocentesRepository docentesRepository, IIARecommendationService iARecommendationService)
        {
            _docentesRepository = docentesRepository;
            _iARecommendationService = iARecommendationService;
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

        [HttpPost("Consulta")]
        public async Task<IActionResult> ConsultaAsync([FromBody] ConsultaRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var docentesResponse = await _docentesRepository.ConsultaAsync(request.CarreraId);

            if (!docentesResponse.WasSuccess || docentesResponse.Result == null || !docentesResponse.Result.Any())
                return BadRequest("No se encontraron docentes para la carrera seleccionada.");

            var listaDocentes = docentesResponse.Result!;

            var recomendacion = await _iARecommendationService.GenerarRecomendacionAsync(
                request.TituloPropuesto,
                listaDocentes.ToList());

            if (!recomendacion.WasSuccess)
                return BadRequest(recomendacion.Message);

            return Ok(recomendacion.Result);
        }

    }
}
