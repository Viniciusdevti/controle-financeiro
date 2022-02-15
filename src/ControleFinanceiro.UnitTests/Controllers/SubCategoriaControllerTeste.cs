using ControleFinanceiro.Api.Helpers.Error;
using ControleFinanceiro.DataContext;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Repository.Repository;
using ControleFinanceiro.Service.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ControleFinanceiro.Api.Dtos.SubCategoriaDto;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using iCommercial.Api.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Xunit;
using ControleFinanceiro.Api.Controllers;

namespace ControleFinanceiro.UnitTests.Controllers
{
    public class SubCategoriaControllerTest
    {
        private SubCategoriaService SubCategoriaService;
        private CategoriaService categoriaService;
        private BaseRepository<ControleFinanceiroDb, SubCategoria> _contextSubCategoria;
        private BaseRepository<ControleFinanceiroDb, Categoria> _contextCategoria;
        private const string _stringConnection = ":memory:";
        private ModelStateDictionary _modelState = new();
        private IConfiguration _configuration;


        public SubCategoriaControllerTest()
        {
            _contextSubCategoria = new BaseRepository<ControleFinanceiroDb, SubCategoria>(_stringConnection);
            _contextCategoria = new BaseRepository<ControleFinanceiroDb, Categoria>(_stringConnection);
            SubCategoriaService = new SubCategoriaService(_stringConnection);
            categoriaService = new CategoriaService(_stringConnection);

            categoriaService.Post(new Categoria { Nome = "Categoria 1" });
            SubCategoriaService.Post(new SubCategoria { Nome = "SubCategoria 1", IdCategoria = 1 });
            SubCategoriaService.Post(new SubCategoria { Nome = "SubCategoria 2", IdCategoria = 1 });
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            _configuration = configurationBuilder.AddJsonFile("AppSettings.json").Build();

        }

        [Fact]
        public void ValidationControllerGetAll()
        {
            var controller = new SubCategoriaController(SubCategoriaService);
            var result = controller.GetAll();
            var result200 = result as OkObjectResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]

        public void ValidationControllerGet()
        {
            var controller = new SubCategoriaController(SubCategoriaService);
            var result = controller.Get(2);
            var result200 = result as OkObjectResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]

        public void InvalidControllerGet()
        {
            var controller = new SubCategoriaController(SubCategoriaService);
            var result = controller.Get(0);
            var result400 = result as BadRequestObjectResult;
            Assert.True(result400.StatusCode == 400);
        }
        [Fact]

        public void ValidationControllerPost()
        {
            var controller = new SubCategoriaController(SubCategoriaService);
            var result = controller.Post(new SubCategoriaCreateDto { Nome = "SubCategoria 001", IdCategoria = 1 });
            var result200 = result as StatusCodeResult;
            Assert.True(result200.StatusCode == 201);
        }

        [Fact]
        public void InvalidControllerPost()
        {
            var model = new SubCategoriaCreateDto();
            model.Nome = new string('N', 301);
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            Assert.True(results[0].ErrorMessage.Split('#')[1] == EnumErrors.MaximoCaracteres.ToString());
            Assert.True(results[1].ErrorMessage.Split('#')[1] == EnumErrors.CampoObrigatorio.ToString());
        }

        [Fact]
        public void ValidationControllerPut()
        {
            var controller = new SubCategoriaController(SubCategoriaService);
            var result = controller.Put(new SubCategoriaUpdateDto
            {
             IdSubCategoria = 2,
             Nome = "SubCategoria 0001",
             IdCategoria = 1,
            });
            var result200 = result as OkResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]
        public void InvalidControllerPut()
        {
            var model = new SubCategoriaUpdateDto();
            model.Nome = new string('N', 301);
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            Assert.True(results[0].ErrorMessage.Split('#')[1] == EnumErrors.CampoObrigatorio.ToString());
            Assert.True(results[1].ErrorMessage.Split('#')[1] == EnumErrors.MaximoCaracteres.ToString());
            Assert.True(results[2].ErrorMessage.Split('#')[1] == EnumErrors.CampoObrigatorio.ToString());

        }

        [Fact]
        public void ValidationControllerDelete()
        {
            var controller = new SubCategoriaController(SubCategoriaService);
            var result = controller.Delete(1);
            var result200 = result as OkResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]
        public void InvalidControllerDelete()
        {
            var controller = new SubCategoriaController(SubCategoriaService);
            var result = controller.Delete(100);
            var result400 = result as BadRequestObjectResult;
            Assert.True(result400.StatusCode == 400);

        }

    }
}
