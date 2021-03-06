using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ControleFinanceiro.Repository.Interfaces;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.DataContext.BaseQuery;

namespace ControleFinanceiro.Repository.Repository
{

    public class BaseRepository<TDBContext, TTable> : IRepository<TTable> where TTable : class where TDBContext : DbContext, new()
    {

        protected DbSet<TTable> table;

        protected readonly TDBContext db;

        protected IDbContextTransaction transaction;
        public string StringConnection;


        public BaseRepository(string stringConnection)
        {
            object parameterDB = stringConnection;
            if (stringConnection == ":memory:")
            {
                parameterDB = new DbContextOptionsBuilder<TDBContext>()
                                .UseInMemoryDatabase(databaseName: "QADB")
                                .Options;
            }

            db = (TDBContext)Activator.CreateInstance(typeof(TDBContext), new object[] { parameterDB });
            table = db.Set<TTable>();
            StringConnection = stringConnection;
        }

        protected void SaveChanges() => db.SaveChanges();
                
        public bool Any(Expression<Func<TTable, bool>> value) => table.Any(value);

        public void Create(TTable entity) { table.Add(entity); SaveChanges(); }

        public void Edit(TTable entity, TTable entityAlter)
        {

            db.Entry(entity).CurrentValues.SetValues(entityAlter);
            SaveChanges();

        }

        public TTable Find(params object[] keyValues) => table.Find(keyValues);

        public TTable Find(Expression<Func<TTable, bool>> expression) => table.Where(expression).FirstOrDefault();

        public List<TTable> List(string foreignKey = null)
        {
            if (!string.IsNullOrEmpty(foreignKey)) return table.Include(foreignKey).ToList();
            return table.AsNoTracking().ToList();
        }
        public List<TTable> List(Expression<Func<TTable, bool>> expression) => table.AsNoTracking().Where(expression).ToList();
        public List<LancamentoQuery> ListLancamento(DateTime dateInit, DateTime dateFinal, long id)
        {
            var entity = db.Set<Lancamento>();
            return id == -1
                ? entity.Include(x => x.SubCategoria).
                Where(x => x.Data.Date >= dateInit.Date
                && x.Data.Date <= dateFinal.Date).Select(x => new LancamentoQuery
                { Valor = x.Valor })
                .ToList()

                : entity.Include(x => x.SubCategoria).
                Where(x => x.SubCategoria.IdCategoria == id
                && x.Data.Date >= dateInit.Date
                && x.Data.Date <= dateFinal.Date).Select(x => new LancamentoQuery
                { Valor = x.Valor, Categoria = x.SubCategoria.Categoria })
                .ToList();





        }

        public void Remove(int[] keyValues)
        {
            var listToRemove = new List<TTable>();
            foreach (var item in keyValues)
            {
                var row = table.Find(item);
                if (row != null) listToRemove.Add(row);
            }
            table.RemoveRange(listToRemove);
            SaveChanges();
        }

        public void Remove(TTable entity)
        {
            table.Remove(entity);
            SaveChanges();
        }

        public void Delete(TTable entity, TTable entityAlter)
        {
            db.Entry(entity).CurrentValues.SetValues(entityAlter);
            SaveChanges();
        }
    }
}

