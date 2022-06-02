using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ActividadCabeceraTipoDatoRepositorio : BaseRepository<TActividadCabeceraTipoDato, ActividadCabeceraTipoDatoBO>
    {
        #region Metodos Base
        public ActividadCabeceraTipoDatoRepositorio() : base()
        {
        }
        public ActividadCabeceraTipoDatoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ActividadCabeceraTipoDatoBO> GetBy(Expression<Func<TActividadCabeceraTipoDato, bool>> filter)
        {
            IEnumerable<TActividadCabeceraTipoDato> listado = base.GetBy(filter);
            List<ActividadCabeceraTipoDatoBO> listadoBO = new List<ActividadCabeceraTipoDatoBO>();
            foreach (var itemEntidad in listado)
            {
                ActividadCabeceraTipoDatoBO objetoBO = Mapper.Map<TActividadCabeceraTipoDato, ActividadCabeceraTipoDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ActividadCabeceraTipoDatoBO FirstById(int id)
        {
            try
            {
                TActividadCabeceraTipoDato entidad = base.FirstById(id);
                ActividadCabeceraTipoDatoBO objetoBO = new ActividadCabeceraTipoDatoBO();
                Mapper.Map<TActividadCabeceraTipoDato, ActividadCabeceraTipoDatoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ActividadCabeceraTipoDatoBO FirstBy(Expression<Func<TActividadCabeceraTipoDato, bool>> filter)
        {
            try
            {
                TActividadCabeceraTipoDato entidad = base.FirstBy(filter);
                ActividadCabeceraTipoDatoBO objetoBO = Mapper.Map<TActividadCabeceraTipoDato, ActividadCabeceraTipoDatoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ActividadCabeceraTipoDatoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TActividadCabeceraTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ActividadCabeceraTipoDatoBO> listadoBO)
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

        public bool Update(ActividadCabeceraTipoDatoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TActividadCabeceraTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ActividadCabeceraTipoDatoBO> listadoBO)
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
        private void AsignacionId(TActividadCabeceraTipoDato entidad, ActividadCabeceraTipoDatoBO objetoBO)
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

        private TActividadCabeceraTipoDato MapeoEntidad(ActividadCabeceraTipoDatoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TActividadCabeceraTipoDato entidad = new TActividadCabeceraTipoDato();
                entidad = Mapper.Map<ActividadCabeceraTipoDatoBO, TActividadCabeceraTipoDato>(objetoBO,
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
        /// Autor: Jashin Salazar
        /// Fecha: 19/10/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los tipos de datos por actividad cabecera
        /// </summary>
        /// <param name="IdActividadCabecera">Id de la actividad cabecera/param>
        /// <returns>Tipo de objeto que retorna la función</returns>
        public List<ActividadCabeceraTipoDatoDTO> ObtenerTipoDatoPorActividadCabecera(int IdActividadCabecera)
        {
            try
            {
                List<ActividadCabeceraTipoDatoDTO> actividadCabeceraTipoDato = new List<ActividadCabeceraTipoDatoDTO>();
                var query = string.Empty;
                query = "SELECT Id, IdActividadCabecera, IdTipoDato FROM com.T_ActividadCabeceraTipoDato WHERE Estado=1 AND IdActividadCabecera=" + IdActividadCabecera;
                var Resultado = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(Resultado) && !Resultado.Contains("[]"))
                {
                    actividadCabeceraTipoDato = JsonConvert.DeserializeObject<List<ActividadCabeceraTipoDatoDTO>>(Resultado);
                }
                return actividadCabeceraTipoDato;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
