using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.Repositories.Intefaces
{
    public interface IDocentesRepository
    {
        Task<ActionResponse<IEnumerable<Docente>>> GetAsync();
        Task<ActionResponse<IEnumerable<DocenteResponseDTO>>> GetNewAsync();
        Task<ActionResponse<Docente>> AddAsync(DocenteDTO docenteDTO);
    }
}
