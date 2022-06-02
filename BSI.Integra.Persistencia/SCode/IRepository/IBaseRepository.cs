using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Persistencia.IRepository
{
    public interface IBaseRepository<TEntity, TEntityBO> where TEntity : BaseEntity where TEntityBO : BaseEntity
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> filter);
        ICollection<TType> GetBy<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select)
            where TType : class;

        bool Exist(int id);
        bool Exist(Expression<Func<TEntity, bool>> filter);

        TEntity FirstById(int id);
        TEntity FirstBy(Expression<Func<TEntity, bool>> filter);
        TType FirstBy<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select)
            where TType : class;

        bool Insert(TEntity entity);
        bool Insert(IEnumerable<TEntity> list);

        bool Delete(int id, string nombreUsuario);
        bool Delete(IEnumerable<int> list, string nombreUsuario);

        bool Update(TEntity entity);
        bool Update(IEnumerable<TEntity> list);

        IEnumerable<TEntity> GetFiltered<KProperty>(IEnumerable<Expression<Func<TEntity, bool>>> filters,
            Expression<Func<TEntity, KProperty>> orderBy, bool ascending);
    }
}
