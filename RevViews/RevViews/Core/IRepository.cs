using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RevViews.Core
{
    public interface IRepository<TEnt> where TEnt : class
    {
        TEnt Get(int id);
        IEnumerable<TEnt> GetAll();
        IEnumerable<TEnt> Find(Expression<Func<TEnt, bool>> predicate);

        TEnt SingleOrDefault(Expression<Func<TEnt, bool>> predicate);

        void Add(TEnt entity);
        void AddRange(IEnumerable<TEnt> entities);

        void Remove(TEnt entity);
        void RemoveRange(IEnumerable<TEnt> entities);
    }
}
