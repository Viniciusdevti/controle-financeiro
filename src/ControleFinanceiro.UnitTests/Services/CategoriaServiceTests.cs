using ControleFinanceiro.Api.Helpers.Error;
using ControleFinanceiro.DataContext;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Repository.Repository;
using ControleFinanceiro.Service.Interfaces;
using ControleFinanceiro.Service.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ControleFinanceiro.UnitTests.Services
{
    public class CategoriaServiceTests
    {

        private CategoriaService categoriaService;
        private  BaseRepository<ControleFinanceiroDb, Categoria> _contextCategoria;
        private const string _stringConnection = ":memory:";
        public CategoriaServiceTests()
        {
            _contextCategoria = new BaseRepository<ControleFinanceiroDb, Categoria>(_stringConnection);
            categoriaService = new CategoriaService(_stringConnection);
            categoriaService.Post(new Categoria { Nome = "Categoria 1", });
        }

        [Fact]
        public void Post_SendingValid()
        {
            var result =  categoriaService.Post(new Categoria { Nome = "Categoria 2", });
            Assert.True(result.Successfull);
        }

        [Fact]
        public void Get_SendingValid()
        {
            var result = categoriaService.Get();
            Assert.True(result.Successfull);
        }

        [Fact]
        public void GetAll_SendingValid()
        {
            var result = categoriaService.GetAll();
            Assert.True(result.Successfull);
        }

        [Fact]
        public void Put_SendingValid()
        {
            var result = categoriaService.Put( new Categoria { IdCategoria = 1, Nome = "Categoria 01" });
            Assert.True(result.Successfull);
        }

        [Fact]
        public void Put_SendingValidIdNotFound()
        {
            var result = categoriaService.Put(new Categoria { IdCategoria = 1000, Nome = "Categoria 01" });
            Assert.True(result.CodeError == EnumErrors.IdNaoEncontrado.ToString());
        }
    }
}
