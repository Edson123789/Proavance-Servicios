using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BSI.Integra.Persistencia.SCode.Repository
{
    public class IntegraVistaRepository<TEntity> : IIntegraVistaRepository<TEntity>
        where TEntity : BaseVistaEntity
    {
        public integraDBContext _context;
        public DbSet<TEntity> entities;

        public IntegraVistaRepository(integraDBContext context)
        {
            _context = context;
            entities = _context.Set<TEntity>();
        }

        public bool Existe(int id)
        {
            return entities.Any(w => w.Id == id);
        }

        public bool Existe(Expression<Func<TEntity, bool>> filter)
        {
            return entities.Where(filter).Any();
        }

        public IEnumerable<TEntity> ObtenerTabla()
        {
            return entities.AsQueryable();
        }

        public TEntity ObtenerPorId(int id)
        {
            return entities.FirstOrDefault(w => w.Id == id);
        }

        public IEnumerable<TEntity> Obtener(Expression<Func<TEntity, bool>> filter = null)
        {
            return entities.Where(filter).AsEnumerable<TEntity>();
        }

        public IQueryable<TEntity> Obtener()
        {
            return entities.AsQueryable();
        }

        public bool Insertar(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("La Entidad es nula");

                _context.Add<TEntity>(entity);
                return _context.SaveChanges() >= 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Insertar(IEnumerable<TEntity> list)
        {
            try
            {
                foreach (var entity in list)
                {
                    bool resultado = Insertar(entity);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Eliminar(int id, string nombreUsuario)
        {
            try
            {
                var objeto = entities.FirstOrDefault(w => w.Id == id);
                if (objeto == null)
                    throw new ArgumentNullException("La Entidad es nula y/o ya fue eliminada");
                if (string.IsNullOrEmpty(nombreUsuario) || (nombreUsuario != null && nombreUsuario.Trim() == ""))
                    throw new ArgumentNullException("El nombre de usuario es nulo y/o no se proporcionó");

                _context.Set<TEntity>().Update(objeto);
                return _context.SaveChanges() >= 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Eliminar(IEnumerable<int> list, string nombreUsuario)
        {
            try
            {
                foreach (var id in list)
                {
                    bool resultado = Eliminar(id, nombreUsuario);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Actualizar(TEntity entity)
        {
            try
            {
                //entity.Estado = false;

                //_context.Entry(entity).State = EntityState.Modified;
                //_context.Set<T>().Attach(entity);
                //return _context.SaveChanges() >= 0;

                if (entity == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                _context.Set<TEntity>().Update(entity);
                return _context.SaveChanges() >= 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Actualizar(IEnumerable<TEntity> list)
        {
            try
            {
                foreach (var entity in list)
                {
                    bool resultado = Actualizar(entity);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
