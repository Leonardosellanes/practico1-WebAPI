using BusinessLayer.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared;
using System;
using DataAccessLayer.EFModels;
using BusinessLayer.BLs;
using DataAccessLayer.IDALs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IBL_ApplicationUsers _bl;
        private readonly IDAL_ApplicationUsers _dal;

        public PersonasController(IBL_ApplicationUsers bl, IDAL_ApplicationUsers dal)
        {
            _bl = bl;
            _dal = dal;
        }

        // GET: api/<PersonasController>
        [ProducesResponseType(typeof(List<Usuario>), 200)]
        [HttpGet]
        public IActionResult Get(int empresaId)
        {
            try
            {
                return Ok(_bl.Get(empresaId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }
        }

        // GET: api/Personas/5
        [ProducesResponseType(typeof(Usuario), 200)]
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                ApplicationUser user = _dal.GetById(id);

                if (user != null)
                {
                    // Mapea el modelo de entidad a un modelo de vista si es necesario
                    Usuario usuario = MapToViewModel(user);
                    return Ok(usuario);
                }
                else
                {
                    return NotFound("Usuario no encontrado.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el usuario: " + ex.Message);
            }
        }

        private Usuario MapToViewModel(ApplicationUser user)
        {
            Usuario usuario = new Usuario
            {
                id = user.Id,
                email = user.Email,
                name = user.Name,
                lName = user.LName,
                address = user.Address,
            };
            return usuario;
        }


        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ApplicationUserUpdateModel updatedUser)
        {
            try
            {
                ApplicationUser existingUser = _dal.GetById(id);

                if (existingUser == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                // Actualizar solo las propiedades que se proporcionan en la solicitud
                if (updatedUser.Email != null)
                    existingUser.Email = updatedUser.Email;

                if (updatedUser.Password != null)
                    existingUser.Password = updatedUser.Password;

                if (updatedUser.Name != null)
                    existingUser.Name = updatedUser.Name;

                if (updatedUser.LName != null)
                    existingUser.LName = updatedUser.LName;

                if (updatedUser.IsAdmin != null)
                    existingUser.IsAdmin = updatedUser.IsAdmin.Value;

                if (updatedUser.Address != null)
                    existingUser.Address = updatedUser.Address;

                if (updatedUser.EmpresaId != null)
                    existingUser.EmpresaId = updatedUser.EmpresaId;

                _dal.Update(existingUser);

                return Ok("Usuario actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el usuario: " + ex.Message);
            }
        }


        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                // Intenta eliminar el usuario por su identificador
                bool deleted = _bl.Delete(id);

                if (deleted)
                {
                    return Ok("Usuario eliminado correctamente.");
                }
                else
                {
                    return NotFound("Usuario no encontrado.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el usuario: " + ex.Message);
            }
        }
    }

}
