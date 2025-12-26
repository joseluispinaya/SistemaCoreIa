using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Shared.DTOs
{
    public class ProyectoSimpleDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Gestion { get; set; } = null!;
    }
}
