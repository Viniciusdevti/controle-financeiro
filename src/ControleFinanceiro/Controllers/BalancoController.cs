using ControleFinanceiro.Api.Dtos;
using ControleFinanceiro.Api.Dtos.Balanco;
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

        
        [HttpGet]
        public ActionResult Get([FromQuery] BalancoGetDto balancoDto)
        {

            var result = _service.Get(balancoDto.DataInicio, balancoDto.DataFim, balancoDto.Id);

            if (result.Successfull)
            {
               
                return Ok(result.Result);
            }

            else
                return result.CodeHttp == 400
                    ? BadRequest(result.Mensagens)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Mensagem);

        }
        
    }
}


