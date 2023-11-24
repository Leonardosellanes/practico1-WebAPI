using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase

    {
        private readonly IWebHostEnvironment _env;

        public ArchivosController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("subir-imagen")]
        public IActionResult SubirImagen(IFormFile archivo)
        {

            if (archivo != null && archivo.Length > 0)
            {
                var rutaDirectorio = Path.Combine(_env.ContentRootPath, "Archivos", "Imagenes");
                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
                var rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    archivo.CopyTo(stream);
                }

                // Guarda la ruta del archivo en tu base de datos si es necesario
                // (alternativamente, podrías almacenar solo el nombre del archivo y reconstruir la ruta cuando sea necesario

                // Resto de la lógica según tus necesidades

                return Ok(new { ruta = nombreArchivo });
            }

            return BadRequest("No se proporcionó un archivo válido.");
        }

        [HttpPost("subir-pdf")]
        public IActionResult SubirPdf(IFormFile archivo)
        {
            try
            {
                if (archivo != null && archivo.Length > 0)
                {
                    var rutaDirectorio = Path.Combine(_env.ContentRootPath, "Archivos", "Pdf");
                    var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
                    var rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);

                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        archivo.CopyTo(stream);
                    }

                    // Puedes guardar la ruta del archivo en tu base de datos si es necesario
                    // (alternativamente, podrías almacenar solo el nombre del archivo y reconstruir la ruta cuando sea necesario)

                    // Resto de la lógica según tus necesidades

                    return Ok(new { ruta = nombreArchivo });
                }

                return BadRequest("No se proporcionó un archivo PDF válido.");
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción aquí
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("obtener-pdf")]
        public IActionResult ObtenerPdf(string nombreArchivo)
        {
            var rutaCompleta = Path.Combine(_env.ContentRootPath, "Archivos", "Pdf", nombreArchivo);

            if (System.IO.File.Exists(rutaCompleta))
            {
                var archivoBytes = System.IO.File.ReadAllBytes(rutaCompleta);
                return File(archivoBytes, "application/pdf", nombreArchivo);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
