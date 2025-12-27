using Capa.Shared.DTOs;
using Capa.Shared.Responses;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Capa.Backend.Helpers
{
    public class IARecommendationService : IIARecommendationService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _http;
        public IARecommendationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _http = new HttpClient();
        }
        public async Task<ActionResponse<List<TutorRecomendadoDTO>>> GenerarRecomendacionAsync(string tituloProyecto, List<DocenteModelDTO> docentes)
        {
            var apiKey = _configuration["OpenAIKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return new ActionResponse<List<TutorRecomendadoDTO>>
                {
                    WasSuccess = false,
                    Message = "OpenAI API Key no configurada."
                };
            }

            var prompt = ConstruirPrompt(tituloProyecto, docentes);

            var requestBody = new
            {
                model = "gpt-4.1-mini",
                messages = new[]
                {
                    new { role = "system", content = "Eres un asesor académico universitario especializado en evaluación de perfiles docentes y asignación de tutores para trabajos de grado. Analiza afinidad temática, experiencia profesional y antecedentes de tutoría." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.3
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await _http.PostAsync("https://api.openai.com/v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                return new ActionResponse<List<TutorRecomendadoDTO>>
                {
                    WasSuccess = false,
                    Message = "Error al comunicarse con OpenAI."
                };
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var resultado = ExtraerResultado(responseJson);

            return new ActionResponse<List<TutorRecomendadoDTO>>
            {
                WasSuccess = true,
                Result = resultado
            };
        }

        private List<TutorRecomendadoDTO> ExtraerResultado(string json)
        {
            using var doc = JsonDocument.Parse(json);

            var content = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return JsonSerializer.Deserialize<List<TutorRecomendadoDTO>>(content!)!;
        }

        private string ConstruirPrompt(string titulo, List<DocenteModelDTO> docentes)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Recomienda los tutores más adecuados para el siguiente proyecto:");
            sb.AppendLine($"Proyecto: {titulo}");
            sb.AppendLine("Docentes disponibles:");

            foreach (var d in docentes)
            {
                sb.AppendLine($"Docente: {d.NombreCompleto}");
                sb.AppendLine($"Perfil: {d.ResumenPerfil}");

                if (d.Proyectos.Any())
                {
                    sb.AppendLine("Proyectos tutorizados:");
                    foreach (var p in d.Proyectos)
                        sb.AppendLine($"- {p.Titulo} ({p.Gestion})");
                }

                sb.AppendLine();
            }

            sb.AppendLine("Selecciona únicamente los docentes con afinidad REAL con el proyecto.");
            sb.AppendLine("Descarta completamente a los docentes cuya afinidad sea baja o irrelevante.");
            sb.AppendLine("Devuelve SOLO los mejores candidatos (máximo 3).");

            sb.AppendLine("Asigna PuntajeAfinidad de 0 a 100 según coincidencia temática, experiencia previa y proyectos tutorizados similares.");
            sb.AppendLine("Da mayor peso a los docentes que hayan tutorizado proyectos similares al proyecto propuesto.");

            sb.AppendLine("Responde exclusivamente con un JSON válido. No agregues texto adicional.");
            sb.AppendLine("Formato:");
            sb.AppendLine("[{\"NombreDocente\":\"\",\"PuntajeAfinidad\":0,\"Justificacion\":\"\"}]");

            return sb.ToString();
        }
    }
}
