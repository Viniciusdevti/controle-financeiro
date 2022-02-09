using ControleFinanceiro.Api.Helpers.Error;
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
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }
            
        }

        public ServiceMessage Get(long id)
        {
            ServiceMessage serviceMessage = new();
            try
            {
                var result = myRepository.Find(id);

                if(result == null)
                {
                    serviceMessage.AddReturnBadRequest("Id não encontrado.", EnumErrors.IdNotFound.ToString());
                    return serviceMessage;
                }

                serviceMessage.Result = result;
                serviceMessage.Successfull = true;

                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }
        }

        public ServiceMessage Post(Categoria categoria)
        {
            ServiceMessage serviceMessage = new();
            try
            {

                myRepository.Create(categoria);

                serviceMessage.Successfull = true;
                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }
        }

        public ServiceMessage Put(Categoria categoria)
        {
            ServiceMessage serviceMessage = new();
            try
            {
                var result = myRepository.Find(categoria.IdCategoria);

                if (result == null)
                {
                    serviceMessage.AddReturnBadRequest("Id não encontrado.", EnumErrors.IdNotFound.ToString());
                    return serviceMessage;
                }

                myRepository.Edit(categoria, categoria.IdCategoria);

                serviceMessage.Result = result;

                serviceMessage.Successfull = true;
                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }
        }

        public ServiceMessage Delete()
        {
            throw new NotImplementedException();
        }
    }
}
