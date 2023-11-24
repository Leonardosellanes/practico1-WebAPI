using System;
using System.Collections.Generic;
using BusinessLayer.IBLs;
using DataAccessLayer.EFModels;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OCController : ControllerBase
    {
        private readonly IBL_OC _blOC;

        public OCController(IBL_OC blOC)
        {
            _blOC = blOC ?? throw new ArgumentNullException(nameof(blOC));
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var orden = _blOC.ObtenerOCPorId(id);

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

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var ordenes = _blOC.ObtenerTodasLasOcs();
                return Ok(ordenes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las órdenes: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] OC orden)
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

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] OC orden)
        {
            try
            {
                var existingOrden = _blOC.ObtenerOCPorId(id);

                if (existingOrden == null)
                {
                    return NotFound($"Orden con ID {id} no encontrada.");
                }

                _blOC.ActualizarOC(orden);
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

