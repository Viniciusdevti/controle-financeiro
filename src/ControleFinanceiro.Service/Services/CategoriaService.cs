using ControleFinanceiro.Api.Helpers.Error;
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
        public ServiceMessage<Categoria> GetAll()
        {
            ServiceMessage<Categoria> serviceMessage = new();
            try
            {

                List<Categoria> result = myRepository.List(x => x.Ativo == true);

                serviceMessage.Successfull = true;
                serviceMessage.ResultList = result;

                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }

        }

        public ServiceMessage<Categoria> Get(long id)
        {
            ServiceMessage<Categoria> serviceMessage = new();
            try
            {
                var result = myRepository.Find(x => x.IdCategoria == id && x.Ativo == true);

                if (result == null)
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

        public ServiceMessage<Categoria> Post(Categoria categoria)
        {
            ServiceMessage<Categoria> serviceMessage = new();
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

        public ServiceMessage<Categoria> Put(Categoria categoria)
        {
            ServiceMessage<Categoria> serviceMessage = new();
            try
            {
                var result = myRepository.Find(x => x.IdCategoria == categoria.IdCategoria && x.Ativo == true);

                if (result == null)
                {
                    serviceMessage.AddReturnBadRequest("Id não encontrado.", EnumErrors.IdNotFound.ToString());
                    return serviceMessage;
                }

                myRepository.Edit(result, categoria);

                serviceMessage.Successfull = true;
                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }
        }

        public ServiceMessage<Categoria> Delete(long id)
        {
            ServiceMessage<Categoria> serviceMessage = new();
            try
            {
                var result = myRepository.Find(x => x.IdCategoria == id && x.Ativo == true);

                if (result == null)
                {
                    serviceMessage.AddReturnBadRequest("Id não encontrado.", EnumErrors.IdNotFound.ToString());
                    return serviceMessage;
                }

                var entityAlter = result;
                entityAlter.Ativo = false;

                myRepository.Delete(result, entityAlter);


                serviceMessage.Successfull = true;
                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }
        }
    }
}

