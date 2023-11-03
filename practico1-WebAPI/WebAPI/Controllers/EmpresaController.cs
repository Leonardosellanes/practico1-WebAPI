﻿using BusinessLayer.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IBL_Empresas _bl;

        public EmpresaController(IBL_Empresas bl)
        {
            _bl = bl;
        }

        // GET: api/<EmpresaController>
        [ProducesResponseType(typeof(List<Empresa>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bl.Get());
        }

        // GET api/<EmpresaController>/1
        [ProducesResponseType(typeof(Empresa), 200)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_bl.Get(id));
        }

        // POST api/<EmpresaController>
        [ProducesResponseType(typeof(Empresa), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Empresa e)
        {
            _bl.Insert(e);
            return Ok();
        }

        // PUT api/<EmpresaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Empresa e)
        {
            _bl.Update(e);
        }

        // DELETE api/<EmpresaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bl.Delete(id);
        }

    }
}