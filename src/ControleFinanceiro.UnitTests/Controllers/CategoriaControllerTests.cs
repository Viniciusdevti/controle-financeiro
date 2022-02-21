using ControleFinanceiro.Api.Helpers.Error;
using ControleFinanceiro.DataContext;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Repository.Repository;
using ControleFinanceiro.Service.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ControleFinanceiro.Api.Dtos.CategoriaDto;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using iCommercial.Api.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Xunit;

namespace ControleFinanceiro.UnitTests.Controllers
{
    public class CategoriaControllerTests
    {
        private CategoriaService categoriaService;
        private BaseRepository<ControleFinanceiroDb, Categoria> _contextCategoria;
        private const string _stringConnection = ":memory:";
        private ModelStateDictionary _modelState = new();
        private IConfiguration _configuration;
       

        public CategoriaControllerTests()
        {
            _contextCategoria = new BaseRepository<ControleFinanceiroDb, Categoria>(_stringConnection);
            categoriaService = new CategoriaService(_stringConnection);
            categoriaService.Post(new Categoria { Nome = "Categoria 1", });
            categoriaService.Post(new Categoria { Nome = "Categoria 2", });
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            _configuration = configurationBuilder.AddJsonFile("AppSettings.json").Build();

        }

        [Fact]
        public void ValidationControllerGetAll()
        {
            var controller = new CategoriaController(categoriaService);
            var result = controller.GetAll();
            var result200 = result as OkObjectResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]

        public void ValidationControllerGet()
        {
            var controller = new CategoriaController(categoriaService);
            var result = controller.Get(2);
            var result200 = result as OkObjectResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]

        public void InvalidControllerGet()
        {
            var controller = new CategoriaController(categoriaService);
            var result = controller.Get(0);
            var result400 = result as BadRequestObjectResult;
            Assert.True(result400.StatusCode == 400);
        }
        [Fact]

        public void ValidationControllerPost()
        {
            var controller = new CategoriaController(categoriaService);
            var result = controller.Post(new CategoriaCreateDto { Nome = "Categoria 0010"});
            var result200 = result as StatusCodeResult;
            Assert.True(result200.StatusCode == 201);
        }

        [Fact]
        public void InvalidControllerPost()
        {
            var model = new CategoriaCreateDto();
            model.Nome = new string('N', 301);
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            Assert.True(results[0].ErrorMessage.Split('#')[1] == EnumErrors.MaximoCaracteres.ToString());
        }

        [Fact]
        public void ValidationControllerPut()
        {
            var controller = new CategoriaController(categoriaService);
            var result = controller.Put(new CategoriaUpdateDto { IdCategoria = 2, Nome = "Categoria 001" });
            var result200 = result as OkResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]
        public void InvalidControllerPut()
        {
            var model = new CategoriaUpdateDto();
            model.Nome = new string('N', 301);
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            Assert.True(results[0].ErrorMessage.Split('#')[1] == EnumErrors.CampoObrigatorio.ToString());
            Assert.True(results[1].ErrorMessage.Split('#')[1] == EnumErrors.MaximoCaracteres.ToString());

        }

        [Fact]
        public void ValidationControllerDelete()
        {
            var controller = new CategoriaController(categoriaService);
            var result = controller.Delete(1);
            var result200 = result as OkResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]
        public void InvalidControllerDelete()
        {
            var controller = new CategoriaController(categoriaService);
            var result = controller.Delete(100);
            var result400 = result as BadRequestObjectResult;
            Assert.True(result400.StatusCode == 400);

        }

    }
}
