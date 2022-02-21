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

        public CategoriaServiceTests()
        {
            _contextCategoria = new BaseRepository<ControleFinanceiroDb, Categoria>(_stringConnection);
            categoriaService = new CategoriaService(_stringConnection);
            categoriaService.Post(new Categoria { Nome = "Categoria 1", });
            categoriaService.Post(new Categoria { IdCategoria = 2, Nome = "Categoria 2", });
            categoriaService.Post(new Categoria { IdCategoria = 3, Nome = "Categoria 3", });
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
            Assert.Equal(result.Mensagens[0].Codigo, EnumErrors.IdNaoEncontrado.ToString());
        }


        [Fact]
        public void ValidationServicePost()
        {
            var result = categoriaService.Post(new Categoria {  Nome = "Categoria 4" });
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServicePost()
        {
            var result = categoriaService.Post(new Categoria { Nome = "Categoria 3", });
            Assert.True(result.Mensagens[0].Codigo == EnumErrors.CampoUnico.ToString());
        }
        [Fact]
        public void ValidationServicePut()
        {
            var result = categoriaService.Put(new Categoria { IdCategoria = 3, Nome = "Categoria 3" });
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServicePut()
        {
            var result = categoriaService.Put(new Categoria { IdCategoria = 100, Nome = "Categoria 0" });
            Assert.Equal(result.Mensagens[0].Codigo, EnumErrors.IdNaoEncontrado.ToString());
            var result2 = categoriaService.Put(new Categoria { IdCategoria = 2, Nome = "Categoria 3" });
            Assert.Equal(result2.Mensagens[0].Codigo, EnumErrors.CampoUnico.ToString());

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
            Assert.Equal(result.Mensagens[0].Codigo, EnumErrors.IdNaoEncontrado.ToString());
        }
    }


}


