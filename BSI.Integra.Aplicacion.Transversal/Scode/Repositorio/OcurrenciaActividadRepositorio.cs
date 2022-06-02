using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class OcurrenciaActividadRepositorio : BaseRepository<TOcurrenciaActividad, OcurrenciaActividadBO>
    {
        #region Metodos Base
        public OcurrenciaActividadRepositorio() : base()
        {
        }
        public OcurrenciaActividadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OcurrenciaActividadBO> GetBy(Expression<Func<TOcurrenciaActividad, bool>> filter)
        {
            IEnumerable<TOcurrenciaActividad> listado = base.GetBy(filter);
            List<OcurrenciaActividadBO> listadoBO = new List<OcurrenciaActividadBO>();
            foreach (var itemEntidad in listado)
            {
                OcurrenciaActividadBO objetoBO = Mapper.Map<TOcurrenciaActividad, OcurrenciaActividadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OcurrenciaActividadBO FirstById(int id)
        {
            try
            {
                TOcurrenciaActividad entidad = base.FirstById(id);
                OcurrenciaActividadBO objetoBO = new OcurrenciaActividadBO();
                Mapper.Map<TOcurrenciaActividad, OcurrenciaActividadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OcurrenciaActividadBO FirstBy(Expression<Func<TOcurrenciaActividad, bool>> filter)
        {
            try
            {
                TOcurrenciaActividad entidad = base.FirstBy(filter);
                OcurrenciaActividadBO objetoBO = Mapper.Map<TOcurrenciaActividad, OcurrenciaActividadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OcurrenciaActividadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOcurrenciaActividad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OcurrenciaActividadBO> listadoBO)
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

        public bool Update(OcurrenciaActividadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOcurrenciaActividad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OcurrenciaActividadBO> listadoBO)
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
        private void AsignacionId(TOcurrenciaActividad entidad, OcurrenciaActividadBO objetoBO)
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

        private TOcurrenciaActividad MapeoEntidad(OcurrenciaActividadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOcurrenciaActividad entidad = new TOcurrenciaActividad();
                entidad = Mapper.Map<OcurrenciaActividadBO, TOcurrenciaActividad>(objetoBO,
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

        /// <summary>
        /// Obtiene una lista de Ocurrencias de acuerdo al IdActividadCabecera y IdOcurrenciaPadre de la actividad 
        /// abierta por el Asesor
        /// </summary>
        /// <param name="ocurrenciaActividadBO"></param>
        public List<ArbolOcurenciaDTO> ObtenerArbolOcurrencia(OcurrenciaActividadBO ocurrenciaActividadBO)
        {
            try
            {
				string _queryArbolOcurrencia = "SELECT IdOcurrenciaActividad, IdOcurrenciaReporte,RequiereLlamada,EstadoOcurrencia,NombreOcurrencia,Color, Roles,Nivel," +
                "TieneOcurrencias,TieneActividades,IdFaseOportunidad,IdOcurrenciaActividad_Padre,FechaCreacion,IdPlantilla_Speech,NombreEstadoOcurrencia,CrearOportunidad,FaseSiguiente,IdPlantillaWP,IdPlantillaCE FROM com.V_HojaGetArbolDeOcurrencias WHERE IdActividadCabecera = @IdActividadCabecera AND IdOcurrenciaActividad_Padre = @IdOcurrenciaPadre AND EstadoOa = 1 AND EstadoOc = 1";
				string queryArbolOcurrencia = _dapper.QueryDapper(_queryArbolOcurrencia, new { @IdActividadCabecera = ocurrenciaActividadBO.IdActividadCabecera, @IdOcurrenciaPadre = ocurrenciaActividadBO.IdOcurrenciaActividadPadre });
                List<ArbolOcurenciaDTO> listaArbolOcurrencia = JsonConvert.DeserializeObject<List<ArbolOcurenciaDTO>>(queryArbolOcurrencia);
                return listaArbolOcurrencia;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }

}
