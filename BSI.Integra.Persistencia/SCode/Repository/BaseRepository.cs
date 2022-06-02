using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.IRepository;
using BSI.Integra.Persistencia.SCode.Repository;
using Microsoft.EntityFrameworkCore;

namespace BSI.Integra.Persistencia.Repository
{
    public class BaseRepository<TEntity, TEntityBO> : IBaseRepository<TEntity, TEntityBO>
        where TEntity : BaseEntity where TEntityBO : BaseEntity
    {
        private integraDBContext _context;
        private DbSet<TEntity> _entities;
        protected DapperRepository _dapper;

        public BaseRepository()
        {
            _context = new integraDBContext();
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _entities = _context.Set<TEntity>();
            _dapper = new DapperRepository(_context);
        }

        public BaseRepository(integraDBContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _entities = _context.Set<TEntity>();
            _dapper = new DapperRepository(_context);
        }

        public IQueryable<TEntity> GetAll()
        {
            //Mapper.Initialize(cfg => { });
            return _entities.AsNoTracking().Where(w => w.Estado == true).AsQueryable();
        }

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> filter)
        {
            //Mapper.Initialize(cfg => { });
            return _entities.AsNoTracking().Where(w => w.Estado == true).Where(filter).AsQueryable();
        }

        public ICollection<TType> GetBy<TType>(Expression<Func<TEntity, bool>> where,Expression<Func<TEntity, TType>> select) where TType : class
        {
            return _entities.AsNoTracking().Where(w => w.Estado == true).Where(where).Select(select).ToList();
        }

        public bool Exist(int id)
        {
            return _entities.Any(w => w.Id == id && w.Estado == true);
        }

        public bool Exist(Expression<Func<TEntity, bool>> filter)
        {
            return _entities.Where(w => w.Estado == true).Where(filter).Any();
        }

        public TEntity FirstById(int id)
        {
            try
            {
                if (id == null)
                    throw new Exception($"El Id de {typeof(TEntity)} es incorrecto");

                if (!Exist(id))
                    throw new Exception($"La entidad con Id {id} de {typeof(TEntity)} no existe");

                TEntity entidad = _entities.AsNoTracking().FirstOrDefault(w => w.Id == id && w.Estado == true);

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public TEntity FirstBy(Expression<Func<TEntity, bool>> filter)
        {
            return _entities.AsNoTracking().Where(w => w.Estado == true).Where(filter).FirstOrDefault();
        }

        public TType FirstBy<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select)
            where TType : class
        {
            return _entities.AsNoTracking().Where(w => w.Estado == true).Where(where).Select(select).FirstOrDefault();
        }

        public bool Insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("La Entidad es nula");

                //TEntity entityDB = Mapper.Map<TEntity, TEntity>(entity);

                _context.Add<TEntity>(entity);
                bool estado = _context.SaveChanges() >= 0;
                _context.Entry(entity).State = EntityState.Detached;

                return estado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool Insert(IEnumerable<TEntity> list)
        {
            try
            {
                if (list == null)
                    throw new ArgumentNullException("La Entidad es nula");
                //TEntity entityDB = Mapper.Map<TEntity, TEntity>(entity);
                _context.Add(list);
                bool estado = _context.SaveChanges() >= 0;
                _context.Entry(list).State = EntityState.Detached;
                return estado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(int id, string nombreUsuario)
        {
            try
            {
                var objeto = _entities.FirstOrDefault(w => w.Id == id && w.Estado == true);
                if (objeto == null)
                    throw new ArgumentNullException("La Entidad es nula y/o ya fue eliminada");
                if (string.IsNullOrEmpty(nombreUsuario) || (nombreUsuario != null && nombreUsuario.Trim() == ""))
                    throw new ArgumentNullException("El nombre de usuario es nulo y/o no se proporcionó");

                objeto["Estado"] = false;
                objeto["UsuarioModificacion"] = nombreUsuario;
                objeto["FechaModificacion"] = DateTime.Now;

                _context.Set<TEntity>().Update(objeto);
                bool estado = _context.SaveChanges() >= 0;
                _context.Entry(objeto).State = EntityState.Detached;

                return estado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(IEnumerable<int> list, string nombreUsuario)
        {
            try
            {
                foreach (var id in list)
                {
                    bool resultado = Delete(id, nombreUsuario);
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

        public IEnumerable<TAlumno> GetFiltered<KProperty>(IEnumerable<Expression<Func<TAlumno, bool>>> filters, Expression<Func<TAlumno, KProperty>> orderBy, bool ascending)
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //TEntity entityDB = Mapper.Map<TEntity, TEntity>(entity);

                _context.Set<TEntity>().Update(entity);
                bool estado = _context.SaveChanges() >= 0;
                _context.Entry(entity).State = EntityState.Detached;

                return estado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<TEntity> list)
        {
            try
            {
                //foreach (var entity in list)
                //{
                //    bool resultado = Update(entity);
                //    if (resultado == false)
                //        return false;
                //}

                //return true;
                if (list == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }
                //TEntity entityDB = Mapper.Map<TEntity, TEntity>(entity);
                _context.Set<IEnumerable<TEntity>>().Update(list);
                bool estado = _context.SaveChanges() >= 0;
                _context.Entry(list).State = EntityState.Detached;
                return estado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<TEntity> GetFiltered<KProperty>(IEnumerable<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, KProperty>> orderBy, bool ascending)
        {
            var rpta = _entities.Where(e => e.Estado == true);

            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    rpta = rpta.AsNoTracking().Where(filter);
                }
            }

            if (ascending)
            {
                return rpta.OrderBy(orderBy).AsNoTracking();
            }
            else
            {
                return rpta.OrderByDescending(orderBy).AsNoTracking();
            }
        }

        /// Autor: Ansoli Espinoza
        /// Fecha: 26-01-2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el valor máximo de la columna especificada en tipo de dato decimal
        /// </summary>
        /// <returns>Devuele un valor decimal</returns>
        public decimal GetMaxDecimal(Func<TEntity, decimal> columnSelector)
        {
            var GetMaxId = _entities.AsNoTracking().Where(w => w.Estado == true).Max(columnSelector);
            return GetMaxId;
        }

        /// Autor: Ansoli Espinoza
        /// Fecha: 26-01-2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el valor máximo de la columna especificada en tipo de dato entero
        /// </summary>
        /// <returns>Devuele un valor int</returns>
        public int GetMaxInt(Func<TEntity, int> columnSelector)
        {
            var GetMaxId = _entities.AsNoTracking().Where(w => w.Estado == true).Max(columnSelector);
            return GetMaxId;
        }
    }
}
