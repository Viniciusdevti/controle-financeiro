
using ControleFinanceiro.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ControleFinanceiro.Repository.Interfaces
{
    public interface IRepository<T> 
    {
            void Create(T entity);
            T Find(params object[] keyValues);
            void Edit(T entity, long id);
            void Remove(int[] keyValues);
            void Remove(T entity);
            List<T> List(string foreignKey = null);
            bool Any(Expression<Func<T, bool>> value);
        }

    }

