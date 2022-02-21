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
                    serviceMessage.AddReturnBadRequest("Id  não encontrado.", EnumErrors.IdNaoEncontrado.ToString());
                    serviceMessage.CodeHttp = 400;
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

        public ServiceMessage<SubCategoria> Post(SubCategoria subCategoria)
        {
            ServiceMessage<SubCategoria> serviceMessage = new();
            try
            {
                var resultSubCategoria = myRepository.Any(
                  x => x.Nome == subCategoria.Nome && x.Ativo == true);
                ValidationErrosPost(subCategoria.IdCategoria, serviceMessage, resultSubCategoria);
                
                if (serviceMessage.Mensagens.Count > 0)
                {
                    serviceMessage.CodeHttp = 400;
                    return serviceMessage;
                }

                myRepository.Create(subCategoria);
                serviceMessage.Successfull = true;
                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }
        }

        public ServiceMessage<SubCategoria> Put(SubCategoria subCategoria)
        {
            ServiceMessage<SubCategoria> serviceMessage = new();
            try
            {
                var result = myRepository.Find(x => x.IdSubCategoria == subCategoria.IdSubCategoria && x.Ativo == true);
                var resultCategoria = myRepository.Any(
                   x => x.Nome == subCategoria.Nome && x.IdSubCategoria != subCategoria.IdCategoria 
                   &&  x.IdSubCategoria != subCategoria.IdSubCategoria
                   && x.Ativo == true);
                ValidationErrosUpdate(subCategoria.IdCategoria, serviceMessage, result, resultCategoria);

                if (serviceMessage.Mensagens.Count > 0)
                {
                    serviceMessage.CodeHttp = 400;
                    return serviceMessage;
                }

                myRepository.Edit(result, subCategoria);

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
                    serviceMessage.AddReturnBadRequest("Id não encontrado.", EnumErrors.IdNaoEncontrado.ToString());
                    serviceMessage.CodeHttp = 400;
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

        public void ValidationErrosUpdate(long id, ServiceMessage<SubCategoria> serviceMessage, SubCategoria entity, bool resultSubCategoria)
        {

            if (entity == null)
                serviceMessage.AddReturnBadRequest("Id subCategoria não encontrado.", EnumErrors.IdNaoEncontrado.ToString());


            if (!myRepositoryCategoia.Any(x => x.IdCategoria == id && x.Ativo == true))
            {
                serviceMessage.AddReturnBadRequest("Id categoria não encontrado.", EnumErrors.IdNaoEncontrado.ToString());
                return;
            }

            if (resultSubCategoria)
                serviceMessage.AddReturnBadRequest("O campo nome deve ser unico",
                EnumErrors.CampoUnico.ToString());

        }
        public void ValidationErrosPost(long id, ServiceMessage<SubCategoria> serviceMessage, bool resultSubCategoria)
        {
            if (!myRepositoryCategoia.Any(x => x.IdCategoria == id && x.Ativo == true))
            {
                serviceMessage.AddReturnBadRequest("Id categoria não encontrado.", EnumErrors.IdNaoEncontrado.ToString());
                return;
            }

            if (resultSubCategoria)
                serviceMessage.AddReturnBadRequest("O campo nome deve ser unico",
                EnumErrors.CampoUnico.ToString());
        }
    }
}
