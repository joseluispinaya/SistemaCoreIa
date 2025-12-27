using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.Repositories.Intefaces
{
    public interface IEstudiantesRepository
    {
        Task<ActionResponse<IEnumerable<Estudiante>>> GetAsync();
        Task<ActionResponse<IEnumerable<EstudianteResponseDTO>>> GetNewAsync();
        //Task<ActionResponse<IEnumerable<EstudianteResponseDTO>>> GetNewAsync();
    }
}
