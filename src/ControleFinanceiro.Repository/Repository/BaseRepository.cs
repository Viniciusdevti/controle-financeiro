using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ControleFinanceiro.Repository.Interfaces;

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
            db = (TDBContext)Activator.CreateInstance(typeof(TDBContext), new object[] { stringConnection });
            table = db.Set<TTable>();
            StringConnection = stringConnection;
        }

        protected void SaveChanges() => db.SaveChanges();
                
        public bool Any(Expression<Func<TTable, bool>> value) => table.Any(value);

        public void Create(TTable entity) { table.Add(entity); SaveChanges(); }

        public void Edit(TTable entity, int id)
        {
            var result = table.Find(id);

            db.Entry(result).CurrentValues.SetValues(entity);
            SaveChanges();


        }

        public TTable Find(params object[] keyValues) => table.Find(keyValues);

        public List<TTable> List(string foreignKey = null)
        {
            if (!string.IsNullOrEmpty(foreignKey)) return table.Include(foreignKey).ToList();
            return table.AsNoTracking().ToList();
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
    }
}

