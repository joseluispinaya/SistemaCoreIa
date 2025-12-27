namespace Capa.Shared.DTOs
{
    public class TutorRecomendadoDTO
    {
        public string NombreDocente { get; set; } = null!;
        public int PuntajeAfinidad { get; set; }
        public string Justificacion { get; set; } = null!;
    }
}
