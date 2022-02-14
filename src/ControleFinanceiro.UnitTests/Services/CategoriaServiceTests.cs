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
    public class CategoriaServiceTests
    {

        private CategoriaService categoriaService;
        private BaseRepository<ControleFinanceiroDb, Categoria> _contextCategoria;
        private const string _stringConnection = ":memory:";
        private ModelStateDictionary _modelState = new();
        private IConfiguration _configuration;
        private ServiceMessage<Categoria> response;

        public CategoriaServiceTests()
        {
            _contextCategoria = new BaseRepository<ControleFinanceiroDb, Categoria>(_stringConnection);
            categoriaService = new CategoriaService(_stringConnection);
            categoriaService.Post(new Categoria { Nome = "Categoria 1", });
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            _configuration = configurationBuilder.AddJsonFile("AppSettings.json").Build();

        }

        [Fact]
        public void ValidationServiceGetAll()
        {
            var result = categoriaService.GetAll();
            Assert.True(result.Successfull);
        }

        [Fact]
        public void ValidationServiceGet()
        {
            var result = categoriaService.Get(1);
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServiceGet()
        {
            var result = categoriaService.Get(100);
            Assert.Equal(result.CodeError, EnumErrors.IdNaoEncontrado.ToString());
        }


        [Fact]
        public void ValidationServicePost()
        {
            var result = categoriaService.Post(new Categoria {  Nome = "Categoria 2" });
            Assert.True(result.Successfull);
        }

        [Fact]
        public void ValidationServicePut()
        {
            var result = categoriaService.Put(new Categoria { IdCategoria = 2, Nome = "Categoria 0" });
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServicePut()
        {
            var result = categoriaService.Put(new Categoria { IdCategoria = 100, Nome = "Categoria 0" });
            Assert.Equal(result.CodeError, EnumErrors.IdNaoEncontrado.ToString());
        }

        [Fact]
        public void ValidationServiceDelete()
        {
            var result = categoriaService.Delete(1);
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServiceDelete()
        {
            var result = categoriaService.Delete(100);
            Assert.Equal(result.CodeError, EnumErrors.IdNaoEncontrado.ToString());
        }
    }


}


