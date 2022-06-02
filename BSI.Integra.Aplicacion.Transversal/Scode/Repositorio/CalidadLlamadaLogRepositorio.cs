using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CalidadLlamadaLogRepositorio : BaseRepository<TCalidadLlamadaLog, CalidadLlamadaLogBO>
    {
        #region Metodos Base
        public CalidadLlamadaLogRepositorio() : base()
        {
        }
        public CalidadLlamadaLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<CalidadLlamadaLogBO> GetBy(Expression<Func<TCalidadLlamadaLog, bool>> filter)
        {
            try
            {
                IEnumerable<TCalidadLlamadaLog> listado = base.GetBy(filter);
                List<CalidadLlamadaLogBO> listadoBO = new List<CalidadLlamadaLogBO>();
                foreach (var itemEntidad in listado)
                {
                    CalidadLlamadaLogBO objetoBO = Mapper.Map<TCalidadLlamadaLog, CalidadLlamadaLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                    listadoBO.Add(objetoBO);
                }

                return listadoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CalidadLlamadaLogBO FirstById(int id)
        {
            try
            {
                TCalidadLlamadaLog entidad = base.FirstById(id);
                CalidadLlamadaLogBO objetoBO = new CalidadLlamadaLogBO();
                Mapper.Map<TCalidadLlamadaLog, CalidadLlamadaLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CalidadLlamadaLogBO FirstBy(Expression<Func<TCalidadLlamadaLog, bool>> filter)
        {
            try
            {
                TCalidadLlamadaLog entidad = base.FirstBy(filter);
                CalidadLlamadaLogBO objetoBO = Mapper.Map<TCalidadLlamadaLog, CalidadLlamadaLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CalidadLlamadaLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCalidadLlamadaLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CalidadLlamadaLogBO> listadoBO)
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

        public bool Update(CalidadLlamadaLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCalidadLlamadaLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CalidadLlamadaLogBO> listadoBO)
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
        private void AsignacionId(TCalidadLlamadaLog entidad, CalidadLlamadaLogBO objetoBO)
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

        private TCalidadLlamadaLog MapeoEntidad(CalidadLlamadaLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCalidadLlamadaLog entidad = new TCalidadLlamadaLog();
                entidad = Mapper.Map<CalidadLlamadaLogBO, TCalidadLlamadaLog>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

    }
}
