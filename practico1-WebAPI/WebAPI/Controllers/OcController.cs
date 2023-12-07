using BusinessLayer.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OCController : ControllerBase
    {
        private readonly IBL_OC _blOC;

        public OCController(IBL_OC blOc)
        {
            _blOC = blOc ?? throw new ArgumentNullException(nameof(blOc));
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                Orden orden = _blOC.ObtenerOCPorId(id);

                if (orden == null)
                {
                    return NotFound($"Orden con ID {id} no encontrada.");
                }

                return Ok(orden);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la orden: {ex.Message}");
            }
        }

        [HttpGet("carrito/{clienteId}")]
        public IActionResult GetCarrito(string clienteId)
        {
            try
            {
                Orden orden = _blOC.obtenerCarrito(clienteId);

                if (orden == null)
                {
                    return NotFound($"Carrito del cliente {clienteId} no encontrada.");
                }

                return Ok(orden);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la orden: {ex.Message}");
            }
        }

        [HttpGet("user/{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var ordenes = _blOC.ObtenerOCPorUserId(id);
                return Ok(ordenes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las órdenes: {ex.Message}");
            }
        } 
        [HttpGet("Empresa/{id}")]
        public IActionResult GetbyEmpresaId(int id)
        {
            try
            {
                var ordenes = _blOC.ObtenerOCPorEmpresaId(id);
                return Ok(ordenes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las órdenes: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Orden orden)
        {
            try
            {
                _blOC.CrearOC(orden);
                return Ok("Orden creada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la orden: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Orden orden)
        {
            try
            {
                _blOC.ActualizarOC(orden);

                if (orden.EstadoOrden == "Entregado")
                {

                    var emailSender = HttpContext.RequestServices.GetRequiredService<IEmailSender>();
                    await emailSender.SendOrderConfirmationAsync("pedritodiestro@gmail.com", orden);
                }

                return Ok("Orden actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la orden: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var existingOrden = _blOC.ObtenerOCPorId(id);

                if (existingOrden == null)
                {
                    return NotFound($"Orden con ID {id} no encontrada.");
                }

                _blOC.EliminarOC(id);
                return Ok("Orden eliminada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la orden: {ex.Message}");
            }
        }
    }
}

