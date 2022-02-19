using ControleFinanceiro.Api.Helpers.Error;
using ControleFinanceiro.DataContext;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Repository.Repository;
using ControleFinanceiro.Service.Services;
using Xunit;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System;

namespace ControleFinanceiro.UnitTests.Services
{
    public class LancamentoServiceTests
    {

        private LancamentoService lancamentoService;
        private BaseRepository<ControleFinanceiroDb, Lancamento> _contextLancamento;
        private const string _stringConnection = ":memory:";
        private ModelStateDictionary _modelState = new();
        private IConfiguration _configuration;

        public LancamentoServiceTests()
        {
            lancamentoService = new LancamentoService(_stringConnection);
            _contextLancamento = new BaseRepository<ControleFinanceiroDb, Lancamento>(_stringConnection);

            new SubCategoriaServiceTests();
            _contextLancamento.Create(new Lancamento { Valor = 100, Data = DateTime.Now, IdSubCategoria = 1 });
            _contextLancamento.Create(new Lancamento { Valor = 100, Data = DateTime.Now, IdSubCategoria = 1 });
            _contextLancamento.Create(new Lancamento { Valor = 100, Data = DateTime.Now, IdSubCategoria = 1 });
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            _configuration = configurationBuilder.AddJsonFile("AppSettings.json").Build();
        }

        [Fact]
        public void ValidationServiceGetAll()
        {
            var result = lancamentoService.GetAll();
            Assert.True(result.Successfull);
        }

        [Fact]
        public void ValidationServiceGet()
        {
            var result = lancamentoService.Get(2);
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServiceGet()
        {
            var result = lancamentoService.Get(100);
            Assert.Equal(result.Mensagens[0].Codigo, EnumErrors.IdNaoEncontrado.ToString());
        }


        [Fact]
        public void ValidationServicePost()
        {
            var result = lancamentoService.Post(new Lancamento { Valor = 100, Data = DateTime.Now, IdSubCategoria = 1 });
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServicePost()
        {
            var result = lancamentoService.Post(new Lancamento
            {
                Valor = 100,
                Data = DateTime.Now.AddDays(1),
                IdSubCategoria = 1
            });
            Assert.True(result.Mensagens[0].Codigo == EnumErrors.DataInvalida.ToString());

            var result2 = lancamentoService.Post(new Lancamento
            {
                Valor = 0,
                Data = DateTime.Now,
                IdSubCategoria = 1
            });
            Assert.True(result2.Mensagens[0].Codigo == EnumErrors.ValorInvalido.ToString());

            var result3 = lancamentoService.Post(new Lancamento
            {
                Valor = 100,
                Data = DateTime.Now,
                IdSubCategoria = 100

            });
            Assert.True(result3.Mensagens[0].Codigo == EnumErrors.IdNaoEncontrado.ToString());
        }

        [Fact]
        public void ValidationServicePut()
        {
            var result = lancamentoService.Put(new Lancamento
            {
                Valor = 100,
                Data = DateTime.Now,
                IdLancamento = 2,
                IdSubCategoria = 1
            });
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServicePut()
        {
            var result = lancamentoService.Put(new Lancamento
            {
                IdLancamento = 1,
                Valor = 100,
                Data = DateTime.Now.AddDays(1),
                IdSubCategoria = 1
            });
            Assert.True(result.Mensagens[0].Codigo == EnumErrors.DataInvalida.ToString());

            var result2 = lancamentoService.Put(new Lancamento
            {
                IdLancamento = 1,
                Valor = 0,
                Data = DateTime.Now,
                IdSubCategoria = 1
            });
            Assert.True(result2.Mensagens[0].Codigo == EnumErrors.ValorInvalido.ToString());

            var result3 = lancamentoService.Put(new Lancamento
            {
                IdLancamento = 100,
                Valor = 100,
                Data = DateTime.Now,
                IdSubCategoria = 1

            });

            Assert.True(result3.Mensagens[0].Codigo == EnumErrors.IdNaoEncontrado.ToString());

            var result4 = lancamentoService.Put(new Lancamento
            {
                IdLancamento = 1,
                Valor = 100,
                Data = DateTime.Now,
                IdSubCategoria = 100

            });
            Assert.True(result4.Mensagens[0].Codigo == EnumErrors.IdNaoEncontrado.ToString());
        }

        [Fact]
        public void ValidationServiceDelete()
        {
            var result = lancamentoService.Delete(1);
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServiceDelete()
        {
            var result = lancamentoService.Delete(100);
            Assert.Equal(result.Mensagens[0].Codigo, EnumErrors.IdNaoEncontrado.ToString());
        }
    }


}


