using ControleFinanceiro.Api.Dtos;
using ControleFinanceiro.Api.Dtos.CategoriaDto;
using ControleFinanceiro.Api.Dtos.SubCategoriaDto;
using ControleFinanceiro.Api.Helpers;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace ControleFinanceiro.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubCategoriaService : ControllerBase
    {

        private readonly ISubCategoriaService _service;

        public SubCategoriaService(ISubCategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
       // [ApiKey]
        public ActionResult GetAll()
        {

            var result = _service.GetAll();
            var resultFinal = result.ResultList.Select(x => new  { x.IdSubCategoria, x.Nome, x.IdCategoria  });

            if (result.Successfull)
                return Ok(resultFinal);

            else
                return result.CodeHttp == 400
                    ? BadRequest(result.Message)
                    : StatusCode(StatusCodes.Status500InternalServerError,
                     new ErrorDto { Codigo = result.CodeError, Mensagem = result.Message });
        }

        [HttpGet("{id}")]
        //[ApiKey]
        public ActionResult Get(long id)
        {

            var result = _service.Get(id);

            if (result.Successfull)
            {
                var resultFinal = new SubCategoriaGetDto
                {
                    IdSubCategoria = result.Result.IdSubCategoria,
                    Nome = result.Result.Nome,
                    IdCategoria = result.Result.IdCategoria,
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
        //[ApiKey]
        public ActionResult Post(SubCategoriaCreateDto subCategoriaDto)
        {
            var subCategoria = new SubCategoria { 
                Nome = subCategoriaDto.Nome,
                IdCategoria = subCategoriaDto.IdCategoria };

            var result = _service.Post(subCategoria);
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
        //[ApiKey]
        public ActionResult Put(SubCategoriaUpdateDto subCategoriaDto)
        {

            var subCategoria = new SubCategoria
            {
               IdSubCategoria = subCategoriaDto.IdSubCategoria,
                Nome = subCategoriaDto.Nome,
                IdCategoria = subCategoriaDto.IdCategoria,
            };

            var result = _service.Put(subCategoria);
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
        //[ApiKey]
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
