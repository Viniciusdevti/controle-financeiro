using ControleFinanceiro.Api.Helpers.Error;
using ControleFinanceiro.DataContext;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Repository.Repository;
using ControleFinanceiro.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace ControleFinanceiro.Service.Services
{
    public class LancamentoService : ILancamentoService
    {
        private BaseRepository<ControleFinanceiroDb, Lancamento> myRepository;
        private BaseRepository<ControleFinanceiroDb, SubCategoria> myRepositorySubCategoria;
        public void Dispose() => myRepository = null;

        public LancamentoService(string stringConnection)
        {
            myRepository = new BaseRepository<ControleFinanceiroDb, Lancamento>(stringConnection);
            myRepositorySubCategoria = new BaseRepository<ControleFinanceiroDb, SubCategoria>(stringConnection);
        }
        public ServiceMessage<Lancamento> GetAll()
        {
            ServiceMessage<Lancamento> serviceMessage = new();
            try
            {

                List<Lancamento> result = myRepository.List(x => x.Ativo == true);

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

        public ServiceMessage<Lancamento> Get(long id)
        {
            ServiceMessage<Lancamento> serviceMessage = new();
            try
            {
                var result = myRepository.Find(x => x.IdLancamento == id && x.Ativo == true);

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

        public ServiceMessage<Lancamento> Post(Lancamento Lancamento)
        {
            ServiceMessage<Lancamento> serviceMessage = new();
            try
            {

                ValidationErrosPost(Lancamento.IdSubCategoria, serviceMessage);
                if (serviceMessage.Mensagens.Count > 0)
                {
                    serviceMessage.CodeHttp = 400;
                    return serviceMessage;
                }

                myRepository.Create(Lancamento);
                serviceMessage.Successfull = true;
                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }
        }

        public ServiceMessage<Lancamento> Put(Lancamento Lancamento)
        {
            ServiceMessage<Lancamento> serviceMessage = new();
            try
            {
                var result = myRepository.Find(x => x.IdLancamento == Lancamento.IdLancamento && x.Ativo == true);
               
                ValidationErrosUpdate(Lancamento.IdLancamento, serviceMessage, result);

                if (serviceMessage.Mensagens.Count > 0)
                {
                    serviceMessage.CodeHttp = 400;
                    return serviceMessage;
                }

                myRepository.Edit(result, Lancamento);

                serviceMessage.Successfull = true;
                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }
        }

        public ServiceMessage<Lancamento> Delete(long id)
        {
            ServiceMessage<Lancamento> serviceMessage = new();
            try
            {
                var result = myRepository.Find(x => x.IdLancamento == id && x.Ativo == true);

                if (result == null)
                {
                    serviceMessage.AddReturnBadRequest("Id do lancamento não encontrado.", EnumErrors.IdNaoEncontrado.ToString());
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

        public void ValidationErrosUpdate(long id, ServiceMessage<Lancamento> serviceMessage, Lancamento entity)
        {

            if (entity == null)
                serviceMessage.AddReturnBadRequest("Id de Lancamento não encontrado.", EnumErrors.IdNaoEncontrado.ToString());


            if (!myRepositorySubCategoria.Any(x => x.IdSubCategoria == id && x.Ativo == true))
            {
                serviceMessage.AddReturnBadRequest("Id da  Subcategoria não encontrado.", EnumErrors.IdNaoEncontrado.ToString());
                return;
            }


        }
        public void ValidationErrosPost(long id, ServiceMessage<Lancamento> serviceMessage)
        {
            if (!myRepositorySubCategoria.Any(x => x.IdSubCategoria == id && x.Ativo == true))
            {
                serviceMessage.AddReturnBadRequest("Id de Subcategoria não encontrado.", EnumErrors.IdNaoEncontrado.ToString());
                return;
            }

        }
    }
}
