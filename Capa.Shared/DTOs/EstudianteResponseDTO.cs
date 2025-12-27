using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Shared.DTOs
{
    public class EstudianteResponseDTO
    {
        public int Id { get; set; }
        public string NroCi { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public CarreraSimpleDTO Carrera { get; set; } = null!;
        public ProyectoSimpleDTO? Proyecto { get; set; }
    }
}
