using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Persistencia.SCode.IRepository
{
    public interface IIntegraVistaRepository<TEntity> where TEntity : BaseVistaEntity
    {

        bool Existe(int id);
        bool Existe(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> ObtenerTabla();
        TEntity ObtenerPorId(int id);
        IEnumerable<TEntity> Obtener(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> Obtener();
        bool Insertar(TEntity entity);
        bool Insertar(IEnumerable<TEntity> list);
        bool Eliminar(int id, string nombreUsuario);
        bool Eliminar(IEnumerable<int> list, string nombreUsuario);
        bool Actualizar(TEntity entity);
        bool Actualizar(IEnumerable<TEntity> list);
    }
}
