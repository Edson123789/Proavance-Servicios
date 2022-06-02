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
    public class OcurrenciaActividadAlternoRepositorio : BaseRepository<TOcurrenciaActividadAlterno, OcurrenciaActividadAlternoBO>
    {
        #region Metodos Base
        public OcurrenciaActividadAlternoRepositorio() : base()
        {
        }
        public OcurrenciaActividadAlternoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OcurrenciaActividadAlternoBO> GetBy(Expression<Func<TOcurrenciaActividadAlterno, bool>> filter)
        {
            IEnumerable<TOcurrenciaActividadAlterno> listado = base.GetBy(filter);
            List<OcurrenciaActividadAlternoBO> listadoBO = new List<OcurrenciaActividadAlternoBO>();
            foreach (var itemEntidad in listado)
            {
                OcurrenciaActividadAlternoBO objetoBO = Mapper.Map<TOcurrenciaActividadAlterno, OcurrenciaActividadAlternoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OcurrenciaActividadAlternoBO FirstById(int id)
        {
            try
            {
                TOcurrenciaActividadAlterno entidad = base.FirstById(id);
                OcurrenciaActividadAlternoBO objetoBO = new OcurrenciaActividadAlternoBO();
                Mapper.Map<TOcurrenciaActividadAlterno, OcurrenciaActividadAlternoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OcurrenciaActividadAlternoBO FirstBy(Expression<Func<TOcurrenciaActividadAlterno, bool>> filter)
        {
            try
            {
                TOcurrenciaActividadAlterno entidad = base.FirstBy(filter);
                OcurrenciaActividadAlternoBO objetoBO = Mapper.Map<TOcurrenciaActividadAlterno, OcurrenciaActividadAlternoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OcurrenciaActividadAlternoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOcurrenciaActividadAlterno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OcurrenciaActividadAlternoBO> listadoBO)
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

        public bool Update(OcurrenciaActividadAlternoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOcurrenciaActividadAlterno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OcurrenciaActividadAlternoBO> listadoBO)
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
        private void AsignacionId(TOcurrenciaActividadAlterno entidad, OcurrenciaActividadAlternoBO objetoBO)
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

        private TOcurrenciaActividadAlterno MapeoEntidad(OcurrenciaActividadAlternoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOcurrenciaActividadAlterno entidad = new TOcurrenciaActividadAlterno();
                entidad = Mapper.Map<OcurrenciaActividadAlternoBO, TOcurrenciaActividadAlterno>(objetoBO,
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
        /// Fecha: 22/10/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de Ocurrencias de acuerdo al IdActividadCabecera y IdOcurrenciaPadre de la actividad 
        /// abierta por el Asesor
        /// </summary>
        /// <param name="OcurrenciaActividadAlternoBO"></param>
        /// <returns> List<ArbolOcurenciaDTO> </returns>
        public List<ArbolOcurenciaDTO> ObtenerArbolOcurrencia(OcurrenciaActividadAlternoBO OcurrenciaActividadAlternoBO)
        {
            try
            {
                string _queryArbolOcurrencia = "SELECT IdOcurrenciaActividad, IdOcurrenciaReporte,RequiereLlamada,EstadoOcurrencia,NombreOcurrencia,Color, Roles,Nivel," +
                "TieneOcurrencias,TieneActividades,IdFaseOportunidad,IdOcurrenciaActividad_Padre,FechaCreacion,IdPlantilla_Speech,NombreEstadoOcurrencia,CrearOportunidad,FaseSiguiente,IdPlantillaWP,IdPlantillaCE FROM com.V_HojaGetArbolDeOcurrenciasAlterno WHERE IdActividadCabecera = @IdActividadCabecera AND IdOcurrenciaActividad_Padre = @IdOcurrenciaPadre AND EstadoOa = 1 AND EstadoOc = 1";
                string queryArbolOcurrencia = _dapper.QueryDapper(_queryArbolOcurrencia, new { @IdActividadCabecera = OcurrenciaActividadAlternoBO.IdActividadCabecera, @IdOcurrenciaPadre = OcurrenciaActividadAlternoBO.IdOcurrenciaActividadPadre });
                List<ArbolOcurenciaDTO> listaArbolOcurrencia = JsonConvert.DeserializeObject<List<ArbolOcurenciaDTO>>(queryArbolOcurrencia);
                return listaArbolOcurrencia;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// Autor: Jashin Salazar
        /// Fecha: 22/10/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de OcurrenciaActividades por el Id de OcurrenciaActividad
        /// </summary>
        /// <param name="IdOcurrenciaActividad"></param>
        /// <returns>OcurenciaActividadCompletoDTO</returns>
        public OcurenciaActividadCompletoDTO ObtenerOcurrenciaActividadPorId(int? IdOcurrenciaActividad)
        {
            try
            {
                string query = "SELECT * FROM com.V_ObtenerOcurrenciasActividadesPorId WHERE Id=@Id";
                string resultado = _dapper.FirstOrDefault(query, new { @Id = IdOcurrenciaActividad});
                OcurenciaActividadCompletoDTO lista = JsonConvert.DeserializeObject<OcurenciaActividadCompletoDTO>(resultado);
                return lista;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }

}
