using ControleFinanceiro.Api.Helpers.Error;
using ControleFinanceiro.DataContext;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Repository.Repository;
using ControleFinanceiro.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace ControleFinanceiro.Service.Services
{
    public class SubCategoriaService : ISubCategoriaService
    {
        private BaseRepository<ControleFinanceiroDb, SubCategoria> myRepository;
        private BaseRepository<ControleFinanceiroDb, Categoria> myRepositoryCategoia;
        public void Dispose() => myRepository = null;

        public SubCategoriaService(string stringConnection)
        {
            myRepository = new BaseRepository<ControleFinanceiroDb, SubCategoria>(stringConnection);
            myRepositoryCategoia = new BaseRepository<ControleFinanceiroDb, Categoria>(stringConnection);
        }
        public ServiceMessage<SubCategoria> GetAll()
        {
            ServiceMessage<SubCategoria> serviceMessage = new();
            try
            {

                List<SubCategoria> result = myRepository.List(x => x.Ativo == true);

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

        public ServiceMessage<SubCategoria> Get(long id)
        {
            ServiceMessage<SubCategoria> serviceMessage = new();
            try
            {
                var result = myRepository.Find(x => x.IdSubCategoria == id && x.Ativo == true);

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

        public ServiceMessage<SubCategoria> Post(SubCategoria SubCategoria)
        {
            ServiceMessage<SubCategoria> serviceMessage = new();
            try
            {
                ValidationErrosPost(SubCategoria.IdCategoria, serviceMessage);

                if (serviceMessage.CodeHttp != 0)
                    return serviceMessage;

                myRepository.Create(SubCategoria);
                serviceMessage.Successfull = true;
                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }
        }

        public ServiceMessage<SubCategoria> Put(SubCategoria SubCategoria)
        {
            ServiceMessage<SubCategoria> serviceMessage = new();
            try
            {
                var result = myRepository.Find(x => x.IdSubCategoria == SubCategoria.IdSubCategoria && x.Ativo == true);

                ValidationErrosUpdate(SubCategoria.IdCategoria, serviceMessage, result);

                if (serviceMessage.CodeHttp != 0)
                    return serviceMessage;

                myRepository.Edit(result, SubCategoria);

                serviceMessage.Successfull = true;
                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }
        }

        public ServiceMessage<SubCategoria> Delete(long id)
        {
            ServiceMessage<SubCategoria> serviceMessage = new();
            try
            {
                var result = myRepository.Find(x => x.IdSubCategoria == id && x.Ativo == true);

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

        public void ValidationErrosUpdate(long id, ServiceMessage<SubCategoria> serviceMessage, SubCategoria entity) 
        {

            if (entity == null)
            {
                serviceMessage.AddReturnBadRequest("Id subCategoria não encontrado.", EnumErrors.IdNotFound.ToString());
                return;
            }

            if (!myRepositoryCategoia.Any(x=> x.IdCategoria == id && x.Ativo == true ))
            {
                serviceMessage.AddReturnBadRequest("Id categoria não encontrado.", EnumErrors.IdNotFound.ToString());
            }
        }
        public void ValidationErrosPost(long id, ServiceMessage<SubCategoria> serviceMessage) 
        {
            if (!myRepositoryCategoia.Any(x=> x.IdCategoria == id && x.Ativo == true ))
            {
                serviceMessage.AddReturnBadRequest("Id categoria não encontrado.", EnumErrors.IdNotFound.ToString());
            }
        }
    }
}
