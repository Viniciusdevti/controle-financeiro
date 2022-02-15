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
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult GetAll()
        {

            var result = _service.GetAll();
            var resultFinal = result.ResultList.Select(x => new { x.IdCategoria, x.Nome });

            if (result.Successfull)
                return Ok(resultFinal);

            else
                return result.CodeHttp == 400
                    ? BadRequest(result.Mensagens)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Mensagem);

        }

        [HttpGet("{id}")]
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
                return result.CodeHttp == 400
                    ? BadRequest(result.Mensagens)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Mensagem);

        }
        [HttpPost]
        public ActionResult Post(CategoriaCreateDto categoriaDto)
        {
            var categoria = new Categoria { Nome = categoriaDto.Nome };
            var result = _service.Post(categoria);
            if (result.Successfull)
                return StatusCode(StatusCodes.Status201Created);

            else
                return result.CodeHttp == 400
                    ? BadRequest(result.Mensagens)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Mensagem);

        }

        [HttpPut]
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
                return result.CodeHttp == 400
                    ? BadRequest(result.Mensagens)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Mensagem);

        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {

            var result = _service.Delete(id);
            if (result.Successfull)
                return Ok();

            else
                return result.CodeHttp == 400
                    ? BadRequest(result.Mensagens)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Mensagem);

        }
    }
}


