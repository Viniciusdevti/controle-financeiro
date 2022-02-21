using Microsoft.AspNetCore.Mvc;
using iCommercial.Api.Controllers;
using Xunit;
using System;
using ControleFinanceiro.Api.Dtos.Balanco;
using ControleFinanceiro.Service.Services;
using ControleFinanceiro.UnitTests.Controllers;
using ControleFinanceiro.Api.Helpers.Error;

namespace ControleFinanceiro.UnitTests.Services
{

    public class BalancoServiceTests
    {

        private BalancoService balancoService;
        private const string _stringConnection = ":memory:";


        public BalancoServiceTests()
        {
            new LancamentoControllerTest();
            balancoService = new(_stringConnection);
        }
        [Fact]
        public void ValidationServiceGet()
        {
            var result = balancoService.Get(
                DateTime.Now.Date,
                DateTime.Now.Date, 1);
            Assert.True(result.Successfull);
        }

        [Fact]
        public void InvalidServiceGet()
        {
            var result = balancoService.Get(
               DateTime.Now.Date.AddDays(1),
               DateTime.Now.Date.AddDays(-1), -1);
            Assert.Equal(result.Mensagens[0].Codigo, EnumErrors.DataInvalida.ToString());
            Assert.Equal(result.Mensagens[1].Codigo, EnumErrors.DataInvalida.ToString());

            var result2 = balancoService.Get(
                DateTime.Now.Date,
                DateTime.Now.Date, 100);
            Assert.Equal(result2.Mensagens[0].Codigo, EnumErrors.BalancoNaoEncontrado.ToString());

        }
    }
}
