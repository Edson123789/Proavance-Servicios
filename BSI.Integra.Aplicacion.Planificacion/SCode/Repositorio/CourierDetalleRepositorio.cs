using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    /// Repositorio: Planificacion/CourierDetalle
    /// Autor: Max Mantilla
    /// Fecha: 25/11/2021
    /// <summary>
    /// Repositorio de detalle de couriers
    /// </summary>
    public class CourierDetalleRepositorio : BaseRepository<TCourierDetalle, CourierDetalleBO>
    {
        #region Metodos Base
        public CourierDetalleRepositorio() : base()
        {
        }
        public CourierDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CourierDetalleBO> GetBy(Expression<Func<TCourierDetalle, bool>> filter)
        {
            IEnumerable<TCourierDetalle> listado = base.GetBy(filter);
            List<CourierDetalleBO> listadoBO = new List<CourierDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                CourierDetalleBO objetoBO = Mapper.Map<TCourierDetalle, CourierDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CourierDetalleBO FirstById(int id)
        {
            try
            {
                TCourierDetalle entidad = base.FirstById(id);
                CourierDetalleBO objetoBO = new CourierDetalleBO();
                Mapper.Map<TCourierDetalle, CourierDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CourierDetalleBO FirstBy(Expression<Func<TCourierDetalle, bool>> filter)
        {
            try
            {
                TCourierDetalle entidad = base.FirstBy(filter);
                CourierDetalleBO objetoBO = Mapper.Map<TCourierDetalle, CourierDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CourierDetalleBO objetoBO)
        {
            try
            {
                // Mapeo de la entidad
                TCourierDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CourierDetalleBO> listadoBO)
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

        public bool Update(CourierDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                // Mapeo de la entidad
                TCourierDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CourierDetalleBO> listadoBO)
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
        private void AsignacionId(TCourierDetalle entidad, CourierDetalleBO objetoBO)
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

        private TCourierDetalle MapeoEntidad(CourierDetalleBO objetoBO)
        {
            try
            {
                // Crea la entidad padre
                TCourierDetalle entidad = new TCourierDetalle();
                entidad = Mapper.Map<CourierDetalleBO, TCourierDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                // Mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene [Id, Direccion, Telefono, TiempEnvio] de los Couriers existentes en una lista 
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerListaCourierDetalle(int IdCourier)
        {
            try
            {
                List<FiltroDTO> listaCourierDetalle = new List<FiltroDTO>();
                var query = string.Empty;
                query = "SELECT Id, Direccion, Telefono, TiempoEnvio as Nombre FROM pla.T_CourierDetalle WHERE Estado = 1 AND IdCourier='"+IdCourier+"'";
                var datosCourierDetalle = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(datosCourierDetalle) && !datosCourierDetalle.Contains("[]") && datosCourierDetalle != "null")
                {
                    listaCourierDetalle = JsonConvert.DeserializeObject<List<FiltroDTO>>(datosCourierDetalle);
                }
                return listaCourierDetalle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene [Id, Direccion, Telefono, TiempEnvio] de Courier segun el pais
        /// </summary>
        /// <param name=”idDePais”>Identificador de la tabla T_Pais</param>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerValorCourierDetallePorPais(int idDePais)
        {
            try
            {
                List<FiltroDTO> listaCourierDetalle = new List<FiltroDTO>();
                var query = string.Empty;
                query = "SELECT  IdCourierDetalle as Id, Direccion, Telefono, TiempoEnvio FROM [pla].[V_ObtenerCourierDetalleAsociadoPais] WHERE IdPais =" + idDePais;
                var datosCourierDetalle = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(datosCourierDetalle) && !datosCourierDetalle.Contains("[]") && datosCourierDetalle != "null")
                {
                    listaCourierDetalle = JsonConvert.DeserializeObject<List<FiltroDTO>>(datosCourierDetalle);
                }
                return listaCourierDetalle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los couriers válidos
        /// </summary>
        /// <returns>List<CourierDetalleDTO></returns>
        public List<CourierDetalleDTO> ObtenerCourierDetalle(int IdCourier)
        {
            try
            {
                List<CourierDetalleDTO> listaCourierDetalle = new List<CourierDetalleDTO>(IdCourier);
                var queryText = string.Empty;
                queryText = "SELECT  CD.Id,CD.IdCourier,C.Nombre,CD.IdPais,CP.Id AS IdCiudad,CD.Direccion,CD.Telefono, CD.TiempoEnvio,NombrePais AS Pais, CP.Nombre AS Ciudad " +
                    "FROM pla.T_CourierDetalle AS CD INNER JOIN pla.T_Courier AS C ON C.Id=CD.IdCourier " +
                    "INNER JOIN conf.T_Pais AS P ON P.id=CD.IdPais " +
                    "INNER JOIN conf.T_Ciudad AS CP ON CD.IdCiudad=CP.id WHERE CD.Estado = 1 AND P.Estado=1 AND CP.Estado=1 AND IdCourier="+IdCourier;
                var datosCourierDetalle = _dapper.QueryDapper(queryText, null);
                if (!string.IsNullOrEmpty(datosCourierDetalle) && !datosCourierDetalle.Contains("[]") && datosCourierDetalle != "null")
                {
                    listaCourierDetalle = JsonConvert.DeserializeObject<List<CourierDetalleDTO>>(datosCourierDetalle);
                }
                return listaCourierDetalle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los couriers válidos
        /// </summary>
        /// <returns>List<CourierDetalleDTO></returns>
        public List<CourierDetalleDTO> ObtenerCourierDetallePais(int IdCourier)
        {
            try
            {
                List<CourierDetalleDTO> listaCourierDetalle = new List<CourierDetalleDTO>(IdCourier);
                var queryText = string.Empty;
                queryText = "SELECT CD.IdPais,NombrePais AS Pais "+
                    "FROM pla.T_CourierDetalle AS CD " +
                    "INNER JOIN conf.T_Pais AS P ON P.id=CD.IdPais " +
                    "WHERE CD.Estado = 1 AND P.Estado=1 AND IdCourier=" + IdCourier +
                    " group by CD.IdPais,NombrePais ";
                var datosCourierDetalle = _dapper.QueryDapper(queryText, null);
                if (!string.IsNullOrEmpty(datosCourierDetalle) && !datosCourierDetalle.Contains("[]") && datosCourierDetalle != "null")
                {
                    listaCourierDetalle = JsonConvert.DeserializeObject<List<CourierDetalleDTO>>(datosCourierDetalle);
                }
                return listaCourierDetalle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los couriers válidos
        /// </summary>
        /// <returns>List<CourierDetalleDTO></returns>
        public List<CourierDetalleDTO> ObtenerCiudadCourierDetallePorPais(int IdCourier,int IdPais)
        {
            try
            {
                List<CourierDetalleDTO> listaCourierDetalle = new List<CourierDetalleDTO>();
                var queryText = string.Empty;
                queryText = "SELECT  CP.Id AS IdCiudad,CP.Nombre AS Ciudad " +
                    "FROM pla.T_CourierDetalle AS CD " +
                    "INNER JOIN conf.T_Pais AS P ON P.id=CD.IdPais " +
                    "INNER JOIN conf.T_Ciudad AS CP ON CP.IdPais=P.id and CP.id=CD.IdCiudad " +
                    "WHERE CD.Estado = 1 AND P.Estado=1 AND CP.Estado=1 AND CD.IdCourier=" + IdCourier+ " AND CD.IdPais="+IdPais;
                var datosCourierDetalle = _dapper.QueryDapper(queryText, null);
                if (!string.IsNullOrEmpty(datosCourierDetalle) && !datosCourierDetalle.Contains("[]") && datosCourierDetalle != "null")
                {
                    listaCourierDetalle = JsonConvert.DeserializeObject<List<CourierDetalleDTO>>(datosCourierDetalle);
                }
                return listaCourierDetalle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Miguel Mora
        /// Fecha: 30/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los detalles de los couriers por el id de courier , pais y ciudad
        /// </summary>
        /// <returns>List<CourierDTO></returns>
        public ReporteCourierDetalleDTO ObtenerCourierDetallePorNombre(int IdCourier, string Pais, string Ciudad)
        {
            try
            {
                ReporteCourierDetalleDTO listaCourierDetalle = new ReporteCourierDetalleDTO();
                var queryText = string.Empty;
                queryText = "SELECT TOP 1 * FROM [fin].[V_ReporteCourierDetalle] WHERE IdCourier='" + IdCourier + "' AND Pais='" + Pais + "' COLLATE Modern_Spanish_CI_AI AND Ciudad='" + Ciudad + "' COLLATE Modern_Spanish_CI_AI";
                var rpta = _dapper.FirstOrDefault(queryText, null);

                if (!string.IsNullOrEmpty(rpta) && !rpta.Contains("[]") && rpta != "null")
                {
                    listaCourierDetalle = JsonConvert.DeserializeObject<ReporteCourierDetalleDTO>(rpta);
                }
                else {
                    return null;
                }
                return listaCourierDetalle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
