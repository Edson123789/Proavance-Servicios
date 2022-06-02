using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.EntityFrameworkCore;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Models.Vistas;
using Dapper;

namespace BSI.Integra.Persistencia.SCode.Repository
{
    public class IntegraRepository<TEntity> : IIntegraRepository<TEntity>
        where TEntity : BaseEntity
    {

        protected integraDBContext _context;
        protected DbSet<TEntity> entities;

        public IntegraRepository()
        {
            _context = new integraDBContext();
            entities = _context.Set<TEntity>();
        }

        public IntegraRepository(integraDBContext context)
        {
            _context = context;
            entities = _context.Set<TEntity>();
        }

        public bool Existe(int id)
        {
            return entities.Any(w => w.Id == id && w.Estado == true);
        }

        public bool Existe(Expression<Func<TEntity, bool>> filter)
        {
            return entities.Where(w => w.Estado == true).Where(filter).Any();
        }

        public IEnumerable<TEntity> ObtenerTabla()
        {
            return entities.Where(w => w.Estado == true).AsQueryable();
        }

        public TEntity ObtenerPorId(int id)
        {
            return entities.FirstOrDefault(w => w.Id == id && w.Estado == true);
        }

        public TEntity ObtenerPor(Expression<Func<TEntity, bool>> filter = null)
        {
            return entities.Where(w => w.Estado == true).Where(filter).FirstOrDefault();
        }

        public IEnumerable<TEntity> Obtener(Expression<Func<TEntity, bool>> filter = null)
        {
            return entities.Where(w => w.Estado == true).Where(filter).AsEnumerable<TEntity>();
        }

        public IQueryable<TEntity> Obtener()
        {
            return entities.Where(w => w.Estado == true).AsQueryable();
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
                var objeto = entities.FirstOrDefault(w => w.Id == id && w.Estado == true);
                if (objeto == null)
                    throw new ArgumentNullException("La Entidad es nula y/o ya fue eliminada");
                if (string.IsNullOrEmpty(nombreUsuario) || (nombreUsuario != null && nombreUsuario.Trim() == ""))
                    throw new ArgumentNullException("El nombre de usuario es nulo y/o no se proporcionó");

                objeto["Estado"] = false;
                objeto["UsuarioModificacion"] = nombreUsuario;
                objeto["FechaModificacion"] = DateTime.Now;

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

        //public IList<dynamic> Query(string sql)
        //{
        //    //var connection = new SqlConnection(_context.Database.GetDbConnection());
        //    IList<dynamic> result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql, CommandType.Text).ToList();

        //    //var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
        //    return result;
        //}
    }
}
