using ControleFinanceiro.Service.Services;
using Microsoft.AspNetCore.Mvc;
using iCommercial.Api.Controllers;
using Xunit;
using System;
using ControleFinanceiro.Api.Dtos.Balanco;
using ControleFinanceiro.UnitTests.Services;

namespace ControleFinanceiro.UnitTests.Controllers
{
    public class BalancoControllerTests
    {

         private BalancoService balancoService;
        private const string _stringConnection = ":memory:";


        public BalancoControllerTests()
        {
            
            new LancamentoServiceTests();
            balancoService = new(_stringConnection);

        }

        [Fact]
        public void ValidationControllerGetAll()
        {
            var controller = new BalancoController(balancoService);
            var result = controller.Get(new BalancoGetDto
            {
                DataInicio = DateTime.Now.Date,
                DataFim = DateTime.Now.Date,
                Id = 1

            }) ;
            var result200 = result as OkObjectResult;
            Assert.True(result200.StatusCode == 200);
        }
        [Fact]
        public void InvalidControllerGetAll()
        {
            var controller = new BalancoController(balancoService);
            var result = controller.Get(new BalancoGetDto());
            var result400 = result as BadRequestObjectResult;
            Assert.True(result400.StatusCode == 400);
        }
    }
}
