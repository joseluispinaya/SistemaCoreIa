using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.Repositories.Intefaces
{
    public interface ICarrerasRepository
    {
        //Task<ActionResponse<IEnumerable<Carrera>>> GetAsync();
        Task<ActionResponse<IEnumerable<Carrera>>> GetAsync();
        Task<ActionResponse<Carrera>> AddAsync(Carrera carrera);
    }
}
