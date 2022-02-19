using ControleFinanceiro.Api.Helpers.Error;
using ControleFinanceiro.DataContext;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Repository.Repository;
using ControleFinanceiro.Service.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ControleFinanceiro.Api.Dtos.LancamentoDto;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using iCommercial.Api.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Xunit;
using System;
using ControleFinanceiro.Api.Controllers;

namespace ControleFinanceiro.UnitTests.Controllers
{
    public class LancamentoControllerTest
    {
        private LancamentoService LancamentoService;
        private BaseRepository<ControleFinanceiroDb, Lancamento> _contextLancamento;
        private const string _stringConnection = ":memory:";
        private ModelStateDictionary _modelState = new();
        private IConfiguration _configuration;


        public LancamentoControllerTest()
        {
            _contextLancamento = new BaseRepository<ControleFinanceiroDb, Lancamento>(_stringConnection);
            LancamentoService = new LancamentoService(_stringConnection);
            
            new SubCategoriaControllerTest();
            _contextLancamento.Create(new Lancamento { Valor = 100, Data = DateTime.Now,IdSubCategoria = 1 });
            _contextLancamento.Create(new Lancamento { Valor = 100, Data = DateTime.Now,IdSubCategoria = 1 });
            _contextLancamento.Create(new Lancamento { Valor = 100, Data = DateTime.Now,IdSubCategoria = 1 });
              IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            _configuration = configurationBuilder.AddJsonFile("AppSettings.json").Build();

        }

        [Fact]
        public void ValidationControllerGetAll()
        {
            var controller = new LancamentoController(LancamentoService);
            var result = controller.GetAll();
            var result200 = result as OkObjectResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]

        public void ValidationControllerGet()
        {
            var controller = new LancamentoController(LancamentoService);
            var result = controller.Get(2);
            var result200 = result as OkObjectResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]

        public void InvalidControllerGet()
        {
            var controller = new LancamentoController(LancamentoService);
            var result = controller.Get(0);
            var result400 = result as BadRequestObjectResult;
            Assert.True(result400.StatusCode == 400);
        }
        [Fact]

        public void ValidationControllerPost()
        {
            var controller = new LancamentoController(LancamentoService);
            var result = controller.Post(new LancamentoCreateDto { Valor = 200, IdSubCategoria = 2, Data = DateTime.Now });
            var result200 = result as StatusCodeResult;
            Assert.True(result200.StatusCode == 201);
        }

        [Fact]
        public void InvalidControllerPost()
        {
            var model = new LancamentoCreateDto();

            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            Assert.True(results[0].ErrorMessage.Split('#')[1] == EnumErrors.CampoObrigatorio.ToString());


        }

        [Fact]
        public void ValidationControllerPut()
        {
            var controller = new LancamentoController(LancamentoService);
            var result = controller.Put(new LancamentoUpdateDto { IdLancamento = 3, Valor = 200, IdSubCategoria = 1, Data = DateTime.Now });
            var result200 = result as OkResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]
        public void InvalidControllerPut()
        {
            var model = new LancamentoUpdateDto();
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            Assert.True(results[0].ErrorMessage.Split('#')[1] == EnumErrors.CampoObrigatorio.ToString());
            Assert.True(results[1].ErrorMessage.Split('#')[1] == EnumErrors.CampoObrigatorio.ToString());

        }

        [Fact]
        public void ValidationControllerDelete()
        {
            var controller = new LancamentoController(LancamentoService);
            var result = controller.Delete(2);
            var result200 = result as OkResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]
        public void InvalidControllerDelete()
        {
            var controller = new LancamentoController(LancamentoService);
            var result = controller.Delete(100);
            var result400 = result as BadRequestObjectResult;
            Assert.True(result400.StatusCode == 400);

        }

    }
}
