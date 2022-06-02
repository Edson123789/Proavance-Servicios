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
    /// Repositorio: Planificacion/Courier
    /// Autor: Max Mantilla
    /// Fecha: 25/11/2021
    /// <summary>
    /// Repositorio de couriers
    /// </summary>
    public class CourierRepositorio : BaseRepository<TCourier, CourierBO>
    {
        #region Metodos Base
        public CourierRepositorio() : base()
        {
        }
        public CourierRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CourierBO> GetBy(Expression<Func<TCourier, bool>> filter)
        {
            IEnumerable<TCourier> listado = base.GetBy(filter);
            List<CourierBO> listadoBO = new List<CourierBO>();
            foreach (var itemEntidad in listado)
            {
                CourierBO objetoBO = Mapper.Map<TCourier, CourierBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CourierBO FirstById(int id)
        {
            try
            {
                TCourier entidad = base.FirstById(id);
                CourierBO objetoBO = new CourierBO();
                Mapper.Map<TCourier, CourierBO > (entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CourierBO FirstBy(Expression<Func<TCourier, bool>> filter)
        {
            try
            {
                TCourier entidad = base.FirstBy(filter);
                CourierBO objetoBO = Mapper.Map<TCourier, CourierBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CourierBO objetoBO)
        {
            try
            {
                // Mapeo de la entidad
                TCourier entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CourierBO> listadoBO)
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

        public bool Update(CourierBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                // Mapeo de la entidad
                TCourier entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CourierBO> listadoBO)
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
        private void AsignacionId(TCourier entidad, CourierBO objetoBO)
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

        private TCourier MapeoEntidad(CourierBO objetoBO)
        {
            try
            {
                // Crea la entidad padre
                TCourier entidad = new TCourier();
                entidad = Mapper.Map<CourierBO, TCourier>(objetoBO,
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
        /// Obtiene [Id, Url] de los Couriers existentes en una lista 
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerListaCourier()
        {
            try
            {
                List<FiltroDTO> listaCourier = new List<FiltroDTO>();
                var query = string.Empty;
                query = "SELECT Id, Direccion, Telefono, Url as Nombre FROM pla.T_Courier WHERE Estado = 1";
                var datosCourier = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(datosCourier) && !datosCourier.Contains("[]") && datosCourier != "null")
                {
                    listaCourier = JsonConvert.DeserializeObject<List<FiltroDTO>>(datosCourier);
                }
                return listaCourier;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Miguel Mora
        /// Fecha: 26/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la liste de curiers mas la ciudad
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroBasicoDTO> ObtenerListaCourierMasCiudad()
        {
            try
            {
                List<FiltroBasicoDTO> listaCourier = new List<FiltroBasicoDTO>();
                var query = string.Empty;
                query = "SELECT cou.Id, CONCAT(cou.Nombre,' - ',ciu.Nombre) as Nombre FROM pla.T_Courier as cou INNER JOIN conf.T_Ciudad ciu on ciu.Id=cou.IdCiudad  WHERE cou.Estado = 1 and ciu.Estado=1";
                var datosCourier = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(datosCourier) && !datosCourier.Contains("[]") && datosCourier != "null")
                {
                    listaCourier = JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(datosCourier);
                }
                return listaCourier;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // Autor: Miguel Mora
        /// Fecha: 26/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la liste de curiers mas la ciudad por el vcalor redivido
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroBasicoDTO> ObtenerListaCourierMasCiudadAutocomplete(string valor)
        {
            try
            {
                List<FiltroBasicoDTO> listaCourier = new List<FiltroBasicoDTO>();
                var query = string.Empty;
                query = "SELECT cou.Id, CONCAT(cou.Nombre,' - ',ciu.Nombre) as Nombre FROM pla.T_Courier as cou INNER JOIN conf.T_Ciudad ciu on ciu.Id=cou.IdCiudad  WHERE cou.Estado = 1 and ciu.Estado=1 and (cou.Nombre like '%"+
                    valor+"%' or ciu.Nombre like '%"+valor+"%')";
                var datosCourier = _dapper.QueryDapper(query, new {valor});
                if (!string.IsNullOrEmpty(datosCourier) && !datosCourier.Contains("[]") && datosCourier != "null")
                {
                    listaCourier = JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(datosCourier);
                }
                return listaCourier;
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
        /// Obtiene [Id, R] de Courier segun el pais
        /// </summary>
        /// <param name=”idDePais”>Identificador de la tabla T_Pais</param>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerValorCourierPorPais(int idDePais)
        {
            try
            {
                List<FiltroDTO> listaCourier = new List<FiltroDTO>();
                var query = string.Empty;
                query = "SELECT  IdCourier as Id,Direccion, Telefono, UrlCourier as Url FROM [pla].[V_ObtenerCourierAsociadoPais] WHERE IdPais =" + idDePais;
                var datosCourier = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(datosCourier) && !datosCourier.Contains("[]") && datosCourier != "null")
                {
                    listaCourier = JsonConvert.DeserializeObject<List<FiltroDTO>>(datosCourier);
                }
                return listaCourier;
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
        /// <returns>List<CourierDTO></returns>
        public List<CourierDTO> ObtenerCourier()
        {
            try
            {
                List<CourierDTO> listaCourier = new List<CourierDTO>();
                var queryText = string.Empty;
                queryText = "SELECT  C.Id,C.Nombre,C.IdPais,CP.Id AS IdCiudad,C.Direccion,C.Telefono,C.Url,NombrePais AS Pais, CP.Nombre AS Ciudad FROM pla.T_Courier AS C " +
                    "INNER JOIN conf.T_Pais AS P ON P.id=C.IdPais INNER JOIN conf.T_Ciudad AS CP ON C.IdCiudad=CP.id WHERE C.Estado = 1 AND P.Estado=1 AND CP.Estado=1";
                var datosCourier = _dapper.QueryDapper(queryText, null);
                if (!string.IsNullOrEmpty(datosCourier) && !datosCourier.Contains("[]") && datosCourier != "null")
                {
                    listaCourier = JsonConvert.DeserializeObject<List<CourierDTO>>(datosCourier);
                }
                return listaCourier;
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
        /// <returns>List<CourierDTO></returns>
        public CourierIdDTO ObtenerCourierPorId(string Courier)
        {
            try
            {
                CourierIdDTO IdCourier = new CourierIdDTO();
                var queryText = string.Empty;
                queryText = "SELECT IdCourier FROM pla.V_TIdCourier_Nombre WHERE Courier='" + Courier+"'";
                var Id = _dapper.FirstOrDefault(queryText, null);

                if (!string.IsNullOrEmpty(Id) && !Id.Contains("[]") && Id != "null")
                {
                    IdCourier = JsonConvert.DeserializeObject<CourierIdDTO>(Id);
                }
                return IdCourier;
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
        /// Obtiene los couriers por el nombre de courier , pais y ciudad
        /// </summary>
        /// <returns>List<CourierDTO></returns>
        public CourierIdDTO ObtenerCourierPorNombre(string Courier, string Pais, string Ciudad)
        {
            try
            {
                CourierIdDTO IdCourier = new CourierIdDTO();
                var queryText = string.Empty;
                queryText = "SELECT TOP 1 Id FROM [fin].[V_ReporteCourier] WHERE Courier='" + Courier + "' AND Pais='"+ Pais + "' COLLATE Modern_Spanish_CI_AI AND Ciudad='" + Ciudad+ "' COLLATE Modern_Spanish_CI_AI";
                var Id = _dapper.FirstOrDefault(queryText, null);

                if (!string.IsNullOrEmpty(Id) && !Id.Contains("[]") && Id != "null")
                {
                    IdCourier = JsonConvert.DeserializeObject<CourierIdDTO>(Id);
                }
                else {
                    return null;
                }

                return IdCourier;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
