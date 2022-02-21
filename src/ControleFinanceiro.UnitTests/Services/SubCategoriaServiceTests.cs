using ControleFinanceiro.Api.Helpers.Error;
using ControleFinanceiro.DataContext;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Repository.Repository;
using ControleFinanceiro.Service.Services;
using Xunit;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using iCommercial.Api.Controllers;

namespace ControleFinanceiro.UnitTests.Services
{
    public class SubCategoriaServiceTests
    {

        private SubCategoriaService SubCategoriaService;
        private CategoriaService categoriaService;
        private BaseRepository<ControleFinanceiroDb, SubCategoria> _contextSubCategoria;
        private BaseRepository<ControleFinanceiroDb, Categoria> _contextCategoria;
        private const string _stringConnection = ":memory:";
        private ModelStateDictionary _modelState = new();
        private IConfiguration _configuration;

        public SubCategoriaServiceTests()
        {
            _contextSubCategoria = new BaseRepository<ControleFinanceiroDb, SubCategoria>(_stringConnection);
            _contextCategoria = new BaseRepository<ControleFinanceiroDb, Categoria>(_stringConnection);
            SubCategoriaService = new SubCategoriaService(_stringConnection);
            categoriaService = new CategoriaService(_stringConnection);

            categoriaService.Post(new Categoria { Nome = "Categoria 1" });
            SubCategoriaService.Post(new SubCategoria { Nome = "SubCategoria 1", IdCategoria = 1 });
            SubCategoriaService.Post(new SubCategoria { Nome = "SubCategoria 2", IdCategoria = 1 });
            SubCategoriaService.Post(new SubCategoria { Nome = "SubCategoria 3", IdCategoria = 1 });
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            _configuration = configurationBuilder.AddJsonFile("AppSettings.json").Build();
        }

        [Fact]
        public void ValidationServiceGetAll()
        {
            var result = SubCategoriaService.GetAll();
            Assert.True(result.Successfull);
        }

        [Fact]
        public void ValidationServiceGet()
        {
            var result = SubCategoriaService.Get(1);
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServiceGet()
        {
            var result = SubCategoriaService.Get(100);
            Assert.Equal(result.Mensagens[0].Codigo, EnumErrors.IdNaoEncontrado.ToString());
        }


        [Fact]
        public void ValidationServicePost()
        {
            var result = SubCategoriaService.Post(new SubCategoria { Nome = "SubCategoria 4", IdCategoria = 1 });
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServicePost()
        {
            var result = SubCategoriaService.Post(new SubCategoria { Nome = "SubCategoria 2", IdCategoria = 1 });
            Assert.True(result.Mensagens[0].Codigo == EnumErrors.CampoUnico.ToString());
        }

        [Fact]
        public void ValidationServicePut()
        {
            var result = SubCategoriaService.Put(new SubCategoria {
                IdSubCategoria = 2, 
                Nome = "SubCategoria 0",
                IdCategoria = 1,
            });
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServicePut()
        {
            var result = SubCategoriaService.Put(new SubCategoria { 
                IdSubCategoria = 100, 
                Nome = "SubCategoria 0",
                IdCategoria = 0,

            });;
            Assert.Equal(result.Mensagens[0].Codigo, EnumErrors.IdNaoEncontrado.ToString());
            Assert.Equal(result.Mensagens[1].Codigo, EnumErrors.IdNaoEncontrado.ToString());
            var result2 = SubCategoriaService.Put(new SubCategoria
            {
                IdSubCategoria = 3,
                Nome = "SubCategoria 2",
                IdCategoria = 1,

            }); ;
            Assert.Equal(result2.Mensagens[0].Codigo, EnumErrors.CampoUnico.ToString());
        }

        [Fact]
        public void ValidationServiceDelete()
        {
            var result = SubCategoriaService.Delete(1);
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServiceDelete()
        {
            var result = SubCategoriaService.Delete(100);
            Assert.Equal(result.Mensagens[0].Codigo, EnumErrors.IdNaoEncontrado.ToString());
        }
    }


}


