using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    ///BO: PersonalAccesoTemporalAulaVirtualRepositorio
    ///Autor: Gian Miranda
    ///Fecha: 29/04/2021
    ///<summary>
    ///Repositorio para consultas de gp.T_PersonalAccesoTemporalAulaVirtual
    ///</summary>
    public class PersonalAccesoTemporalAulaVirtualRepositorio : BaseRepository<TPersonalAccesoTemporalAulaVirtual, PersonalAccesoTemporalAulaVirtualBO>
    {
        #region Metodos Base
        public PersonalAccesoTemporalAulaVirtualRepositorio() : base()
        {
        }
        public PersonalAccesoTemporalAulaVirtualRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalAccesoTemporalAulaVirtualBO> GetBy(Expression<Func<TPersonalAccesoTemporalAulaVirtual, bool>> filter)
        {
            IEnumerable<TPersonalAccesoTemporalAulaVirtual> listado = base.GetBy(filter);
            List<PersonalAccesoTemporalAulaVirtualBO> listadoBO = new List<PersonalAccesoTemporalAulaVirtualBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalAccesoTemporalAulaVirtualBO objetoBO = Mapper.Map<TPersonalAccesoTemporalAulaVirtual, PersonalAccesoTemporalAulaVirtualBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalAccesoTemporalAulaVirtualBO FirstById(int id)
        {
            try
            {
                TPersonalAccesoTemporalAulaVirtual entidad = base.FirstById(id);
                PersonalAccesoTemporalAulaVirtualBO objetoBO = new PersonalAccesoTemporalAulaVirtualBO();
                Mapper.Map<TPersonalAccesoTemporalAulaVirtual, PersonalAccesoTemporalAulaVirtualBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalAccesoTemporalAulaVirtualBO FirstBy(Expression<Func<TPersonalAccesoTemporalAulaVirtual, bool>> filter)
        {
            try
            {
                TPersonalAccesoTemporalAulaVirtual entidad = base.FirstBy(filter);
                PersonalAccesoTemporalAulaVirtualBO objetoBO = Mapper.Map<TPersonalAccesoTemporalAulaVirtual, PersonalAccesoTemporalAulaVirtualBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalAccesoTemporalAulaVirtualBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalAccesoTemporalAulaVirtual entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalAccesoTemporalAulaVirtualBO> listadoBO)
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

        public bool Update(PersonalAccesoTemporalAulaVirtualBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalAccesoTemporalAulaVirtual entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalAccesoTemporalAulaVirtualBO> listadoBO)
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
        private void AsignacionId(TPersonalAccesoTemporalAulaVirtual entidad, PersonalAccesoTemporalAulaVirtualBO objetoBO)
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

        private TPersonalAccesoTemporalAulaVirtual MapeoEntidad(PersonalAccesoTemporalAulaVirtualBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalAccesoTemporalAulaVirtual entidad = new TPersonalAccesoTemporalAulaVirtual();
                entidad = Mapper.Map<PersonalAccesoTemporalAulaVirtualBO, TPersonalAccesoTemporalAulaVirtual>(objetoBO,
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

        /// Autor: Gian Miranda.
        /// Fecha: 22/06/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id usuario del portal web por el correo
        /// </summary>
        /// <param name="email">Cadena con el Id del usuario del portal web</param>
        /// <returns>String</returns>
        public string ObtenerIdUsuarioPortalWebCorreo(string email)
        {
            try
            {
                var resultado = new ValorStringDTO();
                var query = "[conf].[SP_ObtenerIdUsuarioPortalWebPorCorreo]";
                var respuestaQuery = _dapper.QuerySPFirstOrDefault(query, new { Email = email });
                if (!string.IsNullOrEmpty(respuestaQuery) && respuestaQuery != "null" && !respuestaQuery.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<ValorStringDTO>(respuestaQuery);
                }
                else
                {
                    resultado.Valor = string.Empty;
                }
                return resultado.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 22/06/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id usuario del portal web por el correo
        /// </summary>
        /// <param name="username">Cadena con el Id del usuario del portal web</param>
        /// <returns>Objeto de clase DatosBasicosPortalContactoDTO</returns>
        public DatosBasicosPortalContactoDTO ObtenerDatosBasicosPortalWebUsername(string username)
        {
            try
            {
                var resultado = new DatosBasicosPortalContactoDTO();
                var query = "[conf].[SP_ObtenerDatosPortalWebPorUsername]";
                var respuestaQuery = _dapper.QuerySPFirstOrDefault(query, new { Username = username });

                if (!string.IsNullOrEmpty(respuestaQuery) && respuestaQuery != "null" && !respuestaQuery.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<DatosBasicosPortalContactoDTO>(respuestaQuery);
                }
                else
                {
                    resultado = null;
                }

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Actualiza en la tabla de los accesos temporales para el personal en la DB del portal web
		/// </summary>
        /// <param name="idUsuarioPortalWeb">Id del usuario del portal, (PK de la tabla dbo.AspNetUsers del portal web)</param>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <returns>Bool</returns>
        public bool ActualizarIdAlumnoUsuarioPortalWeb(string idUsuarioPortalWeb, int idAlumno)
        {
            try
            {
                var query = "[conf].[SP_ActualizarIdAlumnoUsuarioPortalWeb]";
                _dapper.QuerySPFirstOrDefault(query, new { IdUsuarioPortalWeb = idUsuarioPortalWeb, IdAlumno = idAlumno });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Actualiza en la tabla de los accesos temporales para el personal en la DB del portal web
		/// </summary>
        /// <param name="idPersonal">Id del personal que se le va a otorgar los accesos temporales (PK de la tabla gp.T_Personal)</param>
        /// <param name="idUsuarioPortal">Id del usuario del portal, (PK de la tabla dbo.AspNetUsers del portal web)</param>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <returns>Bool</returns>
        public bool ActualizarAccesosTemporalesPortalWeb(int idPersonal, string idUsuarioPortal, int idAlumno)
        {
            try
            {
                var resultado = new ValorBoolDTO();

                var query = "[gp].[SP_GenerarAccesosTemporalesPersonal]";
                var respuestaQuery = _dapper.QuerySPFirstOrDefault(query, new { IdPersonal = idPersonal, IdUsuarioPortal = idUsuarioPortal, IdAlumno = idAlumno });

                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<ValorBoolDTO>(respuestaQuery);
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Actualiza en la tabla de integra los accesos temporales para el personal en base al DTO enviado
		/// </summary>
		/// <param name="datosAccesoTemporal">Objeto de clase ActualizarAccesoTemporalDTO</param>
        /// <returns>Bool</returns>
        public bool ActualizarAccesosTemporalesIntegra(ActualizarAccesoTemporalDTO datosAccesoTemporal)
        {
            try
            {
                var resultado = new ValorBoolDTO();
                var query = "[gp].[SP_ActualizarAccesoTemporalPersonal]";
                var respuestaQuery = _dapper.QuerySPFirstOrDefault(query, new { IdPersonal = datosAccesoTemporal.IdPersonal, IdPEspecificoPadre = datosAccesoTemporal.IdPEspecificoPadre, IdPEspecificoPadreAnterior = datosAccesoTemporal.IdPEspecificoPadreAnterior, ListaPEspecificoHijo = String.Join(",", datosAccesoTemporal.ListaPEspecificoHijo), FechaInicio = datosAccesoTemporal.FechaInicio, FechaFin = datosAccesoTemporal.FechaFin, FechaInicioAnterior = datosAccesoTemporal.FechaInicioAnterior, FechaFinAnterior = datosAccesoTemporal.FechaFinAnterior, EvaluacionHabilitada = datosAccesoTemporal.EvaluacionHabilitada, Usuario = datosAccesoTemporal.Usuario });

                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<ValorBoolDTO>(respuestaQuery);
                }

                return resultado.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene lista de accesos temporales por IdPersonal
		/// </summary>
		/// <param name="idPersonal">Id del personal del cual se quiere obtener los accesos temporales</param>
		/// <returns>Lista de objetos de clase MaestroPersonalAccesoTemporalDTO</returns>
		public List<MaestroPersonalAccesoTemporalDTO> ObtenerListaAccesoTemporal(int idPersonal)
        {
            try
            {
                var listaAccesoTemporal = new List<MaestroPersonalAccesoTemporalDTO>();
                var query = "[gp].[SP_ObtenerDataAccesoTemporalPersonal]";
                var respuestaQuery = _dapper.QuerySPDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                {
                    listaAccesoTemporal = JsonConvert.DeserializeObject<List<MaestroPersonalAccesoTemporalDTO>>(respuestaQuery);
                }

                return listaAccesoTemporal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Elimina los accesos temporales basandose en el IdPEspecificoPadre
		/// </summary>
        /// <param name="idPEspecificoPadre">Id del PEspecificoPadre a eliminar (PK de la tabla pla.T_PEspecifico)</param>
		/// <param name="idPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <param name="usuario">Usuario que realiza el eliminado</param>
		/// <returns>Bool</returns>
        public bool EliminarAccesoTemporalPorIdPEspecificoPadre(int idPersonal, int idPEspecificoPadre, DateTime fechaInicio, DateTime fechaFin, string usuario)
        {
            try
            {
                var resultado = new ValorBoolDTO();
                var query = "[gp].[SP_EliminarAccesoTemporalPorIdPEspecificoPadre]";
                var respuestaQuery = _dapper.QuerySPFirstOrDefault(query, new { IdPersonal = idPersonal, IdPEspecificoPadre = idPEspecificoPadre, FechaInicio = fechaInicio, FechaFin = fechaFin, Usuario = usuario });

                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<ValorBoolDTO>(respuestaQuery);
                }

                return resultado.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Elimina los accesos temporales basandose en el IdPersonal
		/// </summary>
		/// <param name="idPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <param name="usuario">Usuario que realiza el eliminado</param>
		/// <returns>Bool</returns>
        public bool EliminarAccesoTemporalPorIdPersonal(int idPersonal, string usuario)
        {
            try
            {
                var resultado = new ValorBoolDTO();
                var query = "[gp].[SP_EliminarAccesoTemporalPorIdPersonal]";
                var respuestaQuery = _dapper.QuerySPFirstOrDefault(query, new { IdPersonal = idPersonal, Usuario = usuario });

                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<ValorBoolDTO>(respuestaQuery);
                }

                return resultado.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
