using Capa.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCarrerasAsync();
            await CheckDocentesAsync();
            await CheckEstudiantesAsync();
            await CheckProyectosAsync();
        }

        private async Task CheckCarrerasAsync()
        {
            if (!_context.Carreras.Any())
            {
                _context.Carreras.Add(new Carrera { Nombre = "Ing. Sistemas" });
                _context.Carreras.Add(new Carrera { Nombre = "Ing. Comercial" });
                _context.Carreras.Add(new Carrera { Nombre = "Ing. Civil" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckDocentesAsync()
        {
            if (!_context.Docentes.Any())
            {
                await AddDocenteAsync(
                    "21222321",
                    "Fernando",
                    "Apaza Luna",
                    "fernando@yopmail.com",
                    @"Ingeniero de Sistemas con amplia experiencia en desarrollo de software empresarial,
                      bases de datos relacionales, arquitectura de sistemas y proyectos de transformación digital.
                      Ha participado como tutor en proyectos relacionados con sistemas académicos, plataformas web,
                      sistemas de control administrativo y aplicaciones con inteligencia artificial.
                      Especialista en .NET, SQL Server, APIs REST y análisis de requerimientos.",
                    "Ing. Sistemas");

                await AddDocenteAsync(
                    "45875212",
                    "Lidia",
                    "Mamani Mara",
                    "lidia@yopmail.com",
                    @"Ingeniera de Sistemas con enfoque en ciencia de datos, machine learning y análisis predictivo.
                      Experiencia en proyectos de minería de datos, visualización de información y
                      aplicaciones académicas con modelos de aprendizaje automático.
                      Conocimientos en Python, Scikit-Learn, análisis estadístico y procesamiento de lenguaje natural.",
                    "Ing. Sistemas");

                await AddDocenteAsync(
                    "88745212",
                    "Marcos",
                    "Pedraza Campos",
                    "pedraza@yopmail.com",
                    @"Ingeniero de Sistemas con especialidad en desarrollo web full stack,
                      aplicaciones móviles y arquitectura de software.
                      Tutor de proyectos relacionados con sistemas de gestión, plataformas educativas,
                      aplicaciones móviles y comercio electrónico.
                      Experiencia en .NET, JavaScript, React, bases de datos SQL y diseño de sistemas distribuidos.",
                    "Ing. Sistemas");

                await AddDocenteAsync(
                    "15874521",
                    "Marcelo",
                    "Aponte Chavez",
                    "aponte@yopmail.com",
                    @"Ingeniero Comercial con experiencia en sistemas de información empresarial,
                      gestión de procesos, análisis financiero y control administrativo.
                      Ha asesorado proyectos de grado relacionados con sistemas de ventas,
                      gestión de inventarios, contabilidad y administración de empresas.
                      Especialista en análisis de costos, planificación estratégica y sistemas ERP.",
                    "Ing. Comercial");

                await AddDocenteAsync(
                    "33445566",
                    "Ana",
                    "Quispe Rojas",
                    "anaq@yopmail.com",
                    @"Ingeniera de Sistemas con especialización en ciberseguridad y seguridad informática.
                      Experiencia en auditoría de sistemas, gestión de riesgos tecnológicos y protección de datos.
                      Ha sido tutora en proyectos de seguridad de la información, análisis de vulnerabilidades
                      y desarrollo de sistemas seguros para instituciones académicas y empresariales.",
                    "Ing. Sistemas");

                await AddDocenteAsync(
                    "55667788",
                    "Carlos",
                    "Rivera Choque",
                    "carlosr@yopmail.com",
                    @"Ingeniero de Sistemas con experiencia en inteligencia artificial aplicada,
                      visión por computadora y procesamiento de imágenes.
                      Ha tutorizado proyectos de reconocimiento facial, detección de objetos
                      y sistemas inteligentes para automatización de procesos.",
                    "Ing. Sistemas");

                await AddDocenteAsync(
                    "77889900",
                    "Verónica",
                    "Salazar Pinto",
                    "veronica@yopmail.com",
                    @"Ingeniera de Sistemas con enfoque en ingeniería de software,
                      calidad de sistemas y metodologías ágiles.
                      Experiencia en dirección de proyectos, pruebas de software,
                      gestión de requerimientos y arquitectura de aplicaciones empresariales.",
                    "Ing. Sistemas");

                await AddDocenteAsync(
                    "99001122",
                    "Jorge",
                    "Medina Flores",
                    "jorge@yopmail.com",
                    @"Ingeniero de Sistemas con especialidad en ciencia de datos,
                      big data y analítica avanzada.
                      Ha desarrollado y tutorizado proyectos de análisis de grandes volúmenes de datos,
                      sistemas de recomendación y modelos predictivos para toma de decisiones.",
                    "Ing. Sistemas");

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckEstudiantesAsync()
        {
            if (!_context.Estudiantes.Any())
            {
                await AddEstudianteAsync("65652544", "Ever", "Muchairo Lazo", "muchairo@yopmail.com", "R298-4", "Ing. Sistemas");
                await AddEstudianteAsync("11255712", "Waldo", "Saenz Pilco", "waldo@yopmail.com", "R299-2", "Ing. Sistemas");
                await AddEstudianteAsync("45888544", "Felipe", "Montes Paz", "felipem@yopmail.com", "R300-1", "Ing. Sistemas");
                await AddEstudianteAsync("20125485", "Pablo", "Quette Lara", "pablol@yopmail.com", "R300-2", "Ing. Sistemas");
                await AddEstudianteAsync("10111213", "Jorge", "Mamanta Duri", "jorged@yopmail.com", "R300-3", "Ing. Sistemas");
                await AddEstudianteAsync("20212223", "Mariela", "Daza Surita", "marielad@yopmail.com", "R300-4", "Ing. Sistemas");
                await AddEstudianteAsync("22745225", "Milton", "Yujra Pally", "milton@yopmail.com", "R300-5", "Ing. Sistemas");
                await AddEstudianteAsync("21874588", "Dario", "Miranda Lino", "dario@yopmail.com", "R450-1", "Ing. Comercial");
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckProyectosAsync()
        {
            if (!_context.ProyectoGrados.Any())
            {
                var estudiantes = await _context.Estudiantes.ToListAsync();
                var docentes = await _context.Docentes.ToListAsync();

                // Fernando - Sistemas académicos / IA
                AddProyecto(
                    estudiantes.First(e => e.Nombres == "Ever"),
                    docentes.First(d => d.Nombres == "Fernando"),
                    "Sistema de gestión académica con inteligencia artificial",
                    @"Desarrollo de un sistema web para automatizar procesos académicos mediante
                    algoritmos de inteligencia artificial, incluyendo análisis de rendimiento,
                    predicción de deserción estudiantil y recomendación automática de tutorías.",
                    "2024");

                // Lidia - Data Science / Machine Learning
                AddProyecto(
                    estudiantes.First(e => e.Nombres == "Waldo"),
                    docentes.First(d => d.Nombres == "Lidia"),
                    "Plataforma de análisis predictivo del rendimiento estudiantil",
                    @"Implementación de una plataforma basada en machine learning que analice el
                    comportamiento académico de los estudiantes y genere modelos predictivos
                    para apoyar la toma de decisiones institucionales.",
                    "2024");

                // Carlos - Visión por Computadora / IA
                AddProyecto(
                    estudiantes.First(e => e.Nombres == "Felipe"),
                    docentes.First(d => d.Nombres == "Carlos"),
                    "Sistema de reconocimiento facial para control de acceso universitario",
                    @"Sistema inteligente que utiliza visión por computadora y redes neuronales
                    para identificar personas y controlar el acceso a instalaciones académicas.",
                    "2024");

                // Jorge - Big Data / Ciencia de Datos
                AddProyecto(
                    estudiantes.First(e => e.Nombres == "Pablo"),
                    docentes.First(d => d.Nombres == "Jorge"),
                    "Sistema de análisis de grandes volúmenes de datos para toma de decisiones",
                    @"Plataforma de big data orientada al procesamiento, análisis y visualización
                    de información institucional para apoyar decisiones estratégicas.",
                    "2024");

                // Ana - Ciberseguridad
                AddProyecto(
                    estudiantes.First(e => e.Nombres == "Jorge"),
                    docentes.First(d => d.Nombres == "Ana"),
                    "Sistema de detección de vulnerabilidades en redes académicas",
                    @"Desarrollo de una herramienta de seguridad informática para análisis de
                    vulnerabilidades, monitoreo de redes y protección de datos institucionales.",
                    "2023");

                // Marcos - Apps / Software Engineering
                AddProyecto(
                    estudiantes.First(e => e.Nombres == "Mariela"),
                    docentes.First(d => d.Nombres == "Marcos"),
                    "Aplicación móvil para gestión de proyectos colaborativos",
                    @"Aplicación multiplataforma orientada a mejorar la productividad de equipos
                    mediante planificación, asignación de tareas y control de avances.",
                    "2023");

                // Verónica - Ingeniería de Software / Calidad
                AddProyecto(
                    estudiantes.First(e => e.Nombres == "Milton"),
                    docentes.First(d => d.Nombres == "Verónica"),
                    "Sistema de control de calidad de software basado en métricas",
                    @"Sistema que permite evaluar la calidad del software mediante métricas,
                    pruebas automatizadas y seguimiento de defectos en proyectos de desarrollo.",
                    "2023");

                // Marcelo - Sistemas empresariales
                AddProyecto(
                    estudiantes.First(e => e.Nombres == "Dario"),
                    docentes.First(d => d.Nombres == "Marcelo"),
                    "Sistema integrado de gestión empresarial para PYMES",
                    @"Desarrollo de un sistema ERP para el control de ventas, inventarios,
                    contabilidad y reportes financieros en pequeñas empresas.",
                    "2023");

                await _context.SaveChangesAsync();
            }
        }

        private async Task AddDocenteAsync(string nroCi, string nombres, string apellidos, string correo, string resumenPerfil, string carrera)
        {
            var carrer = await _context.Carreras.FirstOrDefaultAsync(x => x.Nombre == carrera);

            carrer ??= await _context.Carreras.FirstOrDefaultAsync();
            //carrer ??= await _context.Carreras.FirstOrDefaultAsync();

            Docente docente = new()
            {
                NroCi = nroCi,
                Nombres = nombres,
                Apellidos = apellidos,
                Correo = correo,
                ResumenPerfil = resumenPerfil,
                Carrera = carrer!
            };

            _context.Docentes.Add(docente);
        }

        private async Task AddEstudianteAsync(string nroCi, string nombres, string apellidos, string correo, string codigo, string carrera)
        {
            var carrer = await _context.Carreras.FirstOrDefaultAsync(x => x.Nombre == carrera);
            carrer ??= await _context.Carreras.FirstOrDefaultAsync();

            Estudiante estudiante = new()
            {
                NroCi = nroCi,
                Nombres = nombres,
                Apellidos = apellidos,
                Correo = correo,
                Codigo = codigo,
                Carrera = carrer!
            };

            _context.Estudiantes.Add(estudiante);
        }

        private void AddProyecto(Estudiante estudiante, Docente docente, string titulo, string resumen, string gestion)
        {
            ProyectoGrado proyecto = new()
            {
                Estudiante = estudiante,
                Docente = docente,
                Titulo = titulo,
                Resumen = resumen,
                Gestion = gestion
            };

            _context.ProyectoGrados.Add(proyecto);
        }
    }
}
