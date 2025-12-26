using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.Repositories.Intefaces
{
    public interface ICarrerasRepository
    {
        //Task<ActionResponse<IEnumerable<CarreraResponseDTO>>> GetNewAsync();
        Task<ActionResponse<IEnumerable<CarreraResponseDTO>>> GetNewAsync();
        Task<ActionResponse<IEnumerable<Carrera>>> GetAsync();
        Task<ActionResponse<Carrera>> AddAsync(Carrera carrera);
    }
}
