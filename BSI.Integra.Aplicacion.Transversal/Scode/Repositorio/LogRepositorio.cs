using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class LogRepositorio : BaseRepository<TLog, LogBO>
    {
        #region Metodos Base
        public LogRepositorio() : base()
        {
        }
        public LogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<LogBO> GetBy(Expression<Func<TLog, bool>> filter)
        {
            IEnumerable<TLog> listado = base.GetBy(filter);
            List<LogBO> listadoBO = new List<LogBO>();
            foreach (var itemEntidad in listado)
            {
                LogBO objetoBO = Mapper.Map<TLog, LogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public LogBO FirstById(int id)
        {
            try
            {
                TLog entidad = base.FirstById(id);
                LogBO objetoBO = new LogBO();
                Mapper.Map<TLog, LogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public LogBO FirstBy(Expression<Func<TLog, bool>> filter)
        {
            try
            {
                TLog entidad = base.FirstBy(filter);
                LogBO objetoBO = Mapper.Map<TLog, LogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(LogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TLog entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<LogBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
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

        public bool Update(LogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TLog entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<LogBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
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
        private void AsignacionId(TLog entidad, LogBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TLog MapeoEntidad(LogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TLog entidad = new TLog();
                entidad = Mapper.Map<LogBO, TLog>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<LogBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TLog, bool>>> filters, Expression<Func<TLog, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TLog> listado = base.GetFiltered(filters, orderBy, ascending);
            List<LogBO> listadoBO = new List<LogBO>();

            foreach (var itemEntidad in listado)
            {
                LogBO objetoBO = Mapper.Map<TLog, LogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
