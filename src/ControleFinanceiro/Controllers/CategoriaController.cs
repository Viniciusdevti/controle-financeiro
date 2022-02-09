using ControleFinanceiro.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace iCommercial.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiKey]
        public ActionResult GetAll()
        {

            var result = _service.GetAll();
            if (result.Successfull)
                return Ok(result.Result);

            else
                return BadRequest(result.Message);
        }
    }
}


