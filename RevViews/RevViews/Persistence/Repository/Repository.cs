using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RevViews.Core;

namespace RevViews.Persistence.Repository
{
    public class Repository<TEnt> : IRepository<TEnt> where TEnt : class
    {
        private readonly DbSet<TEnt> _ents;
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
            _ents = Context.Set<TEnt>();
        }


        //Get by ID
        public TEnt Get(int id)
        {
            return _ents.Find(id);
        }

        //Get all
        public IEnumerable<TEnt> GetAll()
        {
            return _ents.ToList();
        }

        //Enable Lamdas by way of the funct delegate expression predicate
        public IEnumerable<TEnt> Find(Expression<Func<TEnt, bool>> predicate)
        {
            return _ents.Where(predicate);
        }

        public TEnt SingleOrDefault(Expression<Func<TEnt, bool>> predicate)
        {
            return _ents.SingleOrDefault(predicate);
        }

        public void Add(TEnt ent)
        {
            _ents.Add(ent);
        }

        public void Update(TEnt ent)
        {
            Context.Entry(ent).State = EntityState.Modified;
        }

        public void AddRange(IEnumerable<TEnt> ents)
        {
            _ents.AddRange(ents);
        }

        public void Remove(TEnt ent)
        {
            _ents.Remove(ent);
        }

        public void RemoveRange(IEnumerable<TEnt> ents)
        {
            _ents.RemoveRange(ents);
        }
    }
}