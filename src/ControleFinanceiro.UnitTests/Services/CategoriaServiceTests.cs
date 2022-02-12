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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ControleFinanceiro.Api.Dtos.CategoriaDto;

namespace ControleFinanceiro.UnitTests.Services
{
    public class CategoriaServiceTests
    {

        private CategoriaService categoriaService;
        private BaseRepository<ControleFinanceiroDb, Categoria> _contextCategoria;
        private const string _stringConnection = ":memory:";
        private ModelStateDictionary _modelState;
        public CategoriaServiceTests()
        {
            _contextCategoria = new BaseRepository<ControleFinanceiroDb, Categoria>(_stringConnection);
            categoriaService = new CategoriaService(_stringConnection);
            categoriaService.Post(new Categoria { Nome = "Categoria 1", });
        }

        [Fact]
        public void Post_SendingValid()
        {
            var result = categoriaService.Post(new Categoria { });
            Assert.True(result.Successfull);
        }

        [Fact]
        public void Post_SendingRequerId()
        {
            var fiz = new CategoriaUpdateDto
            {
                
               
            };
            var nome = _modelState["FirstName"].Errors[0];
            //Assert.Equal(true);
        }

        [Fact]
        public void Get_SendingValid()
        {
            var result = categoriaService.Get(1);
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
            var result = categoriaService.Put(new Categoria { IdCategoria = 1, Nome = "Categoria 01" });
            Assert.True(result.Successfull);
        }

        [Fact]
        public void Put_SendingValidIdNotFound()
        {
            var result = categoriaService.Put(new Categoria { IdCategoria = 1000, Nome = "Categoria 01" });
            Assert.True(result.CodeError == EnumErrors.IdNaoEncontrado.ToString());
        }

        [Fact]
        public void Put_SendingRequest()
        {
            var result = categoriaService.Put(new Categoria { IdCategoria = 1000, Nome = "Categoria 01" });
            Assert.True(result.CodeError == EnumErrors.IdNaoEncontrado.ToString());
        }
    }
}
