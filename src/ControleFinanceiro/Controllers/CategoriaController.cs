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
            var resultFinal = result.ResultList.Select(x => new { x.IdCategoria, x.Nome });

            if (result.Successfull)
                return Ok(resultFinal);

            else
                return result.CodeHttp == 400
                    ? BadRequest(result.Message)
                    : StatusCode(StatusCodes.Status500InternalServerError,
                     new ErrorDto { Codigo = result.CodeError, Mensagem = result.Message });

        }

        [HttpGet("{id}")]
        [ApiKey]
        public ActionResult Get(long id)
        {

            var result = _service.Get(id);
           
            if (result.Successfull)
            {
                var resultFinal = new CategoriaGetDto
                {
                    IdCategoria = result.Result.IdCategoria,
                    Nome = result.Result.Nome
                };
                return Ok(resultFinal);
            }

            else
            {
                var error = new ErrorDto { Codigo = result.CodeError, Mensagem = result.Message };
                return result.CodeHttp == 400
                    ? BadRequest(error)
                    : StatusCode(StatusCodes.Status500InternalServerError, error);
            }

        }
        [HttpPost]
        [ApiKey]
        public ActionResult Post(string nome)
        {
            var categoria = new Categoria { Nome = nome };
            var result = _service.Post(categoria);
            if (result.Successfull)
                return StatusCode(StatusCodes.Status201Created);

            else
            {
                var error = new ErrorDto { Codigo = result.CodeError, Mensagem = result.Message };
                return result.CodeHttp == 400
                    ? BadRequest(error)
                    : StatusCode(StatusCodes.Status500InternalServerError, error);
            }

        }

        [HttpPut]
        [ApiKey]
        public ActionResult Put([FromBody] CategoriaUpdateDto categoriaDto)
        {

            

            var categoria = new Categoria
            {
                IdCategoria = categoriaDto.IdCategoria,
                Nome = categoriaDto.Nome,
            };

            var result = _service.Put(categoria);
            if (result.Successfull)
                return Ok();


            else
            {
                var error = new ErrorDto { Codigo = result.CodeError, Mensagem = result.Message };
                return result.CodeHttp == 400
                    ? BadRequest(error)
                    : StatusCode(StatusCodes.Status500InternalServerError, error);
            }

        }

        [HttpDelete]
        [ApiKey]
        public ActionResult Delete(long id)
        {

            var result = _service.Delete(id);
            if (result.Successfull)
                return Ok();

            else
            {
                var error = new ErrorDto { Codigo = result.CodeError, Mensagem = result.Message };
                return result.CodeHttp == 400
                    ? BadRequest(error)
                    : StatusCode(StatusCodes.Status500InternalServerError, error);
            }

        }
    }
}


