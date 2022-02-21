
using ControleFinanceiro.Api.Dtos.LancamentoDto;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;


namespace ControleFinanceiro.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiKey]
    public class LancamentoController : ControllerBase
    {

        private readonly ILancamentoService _service;

        public LancamentoController(ILancamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult GetAll()
        {

            var result = _service.GetAll();

            if (result.Successfull)
            {

                var resultFinal = result.ResultList.Select(x => new
                {
                    x.IdLancamento,
                    x.Valor,
                    x.Data,
                    x.IdSubCategoria,
                    x.Comentario
                });

                return Ok(resultFinal);
            }

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
                var resultFinal = new
                {
                    IdLancamento = result.Result.IdLancamento,
                    Valor = result.Result.Valor,
                    Data = result.Result.Data,
                    IdSubCategoria = result.Result.IdSubCategoria,
                    Comentario = result.Result.Comentario
                };

                return Ok(resultFinal);
            }

            else
            {
                return result.CodeHttp == 400
                    ? BadRequest(result.Mensagens)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Mensagem);
            }

        }
        [HttpPost]
        public ActionResult Post(LancamentoCreateDto lancamentoDto)
        {
            var lancamento = new Lancamento
            {
                Valor = lancamentoDto.Valor,
                Data = lancamentoDto.Data.Date,
                IdSubCategoria = lancamentoDto.IdSubCategoria,
                Comentario = lancamentoDto.Comentario,
            };

            var result = _service.Post(lancamento);
            if (result.Successfull)
                return StatusCode(StatusCodes.Status201Created);

            else
                return result.CodeHttp == 400
                    ? BadRequest(result.Mensagens)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Mensagem);

        }

        [HttpPut]
        public ActionResult Put(LancamentoUpdateDto lancamentoDto)
        {

            var lancamento = new Lancamento 
            {
                IdLancamento = lancamentoDto.IdLancamento,
                Valor = lancamentoDto.Valor,
                Data = lancamentoDto.Data.Date,
                IdSubCategoria = lancamentoDto.IdSubCategoria,
                Comentario = lancamentoDto.Comentario,
            };

            var result = _service.Put(lancamento);
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
