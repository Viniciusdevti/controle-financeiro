using ControleFinanceiro.Api.Dtos;
using ControleFinanceiro.Api.Dtos.CategoriaDto;
using ControleFinanceiro.Api.Helpers;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace iCommercial.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ValidateModel]
    [ApiController]
    //[ApiKey]
    public class BalancoController : ControllerBase
    {

        private readonly IBalancoService _service;

        public BalancoController(IBalancoService service)
        {
            _service = service;
        }

        
        [HttpGet("{id}")]
        public ActionResult Get()
        {

            var result = _service.Get();

            if (result.Successfull)
            {
               
                return Ok();
            }

            else
                return result.CodeHttp == 400
                    ? BadRequest(result.Mensagens)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Mensagem);

        }
        
    }
}


