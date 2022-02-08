using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.DataContext;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Repository.Repository;
using ControleFinanceiro.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace ControleFinanceiro.Service.Services
{
    public class CategoriaService : ICategoriaService
    {
        private BaseRepository<ControleFinanceiroDb, Categoria> myRepository;

        public void Dispose() => myRepository = null;

        public CategoriaService(string stringConnection)
        {
            myRepository = new BaseRepository<ControleFinanceiroDb, Categoria>(stringConnection);
        }
        public ServiceMessage GetAll()
        {
            ServiceMessage serviceMessage = new();
            try
            {

            ICollection<Categoria> result = myRepository.List();

            serviceMessage.Successfull = true;
            serviceMessage.Result = result;

            return serviceMessage;
            } 
            catch (Exception ex)
            {
                serviceMessage.Message = ex.Message;
                return serviceMessage;
            }
            
        }
    }
}
