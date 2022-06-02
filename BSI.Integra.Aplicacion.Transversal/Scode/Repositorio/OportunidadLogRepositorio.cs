using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: OportunidadLogRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Log de Oportunidades
    /// </summary>
    public class OportunidadLogRepositorio : BaseRepository<TOportunidadLog, OportunidadLogBO>
    {
        #region Metodos Base
        public OportunidadLogRepositorio() : base()
        {
        }
        public OportunidadLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OportunidadLogBO> GetBy(Expression<Func<TOportunidadLog, bool>> filter)
        {
            IEnumerable<TOportunidadLog> listado = base.GetBy(filter).ToList();
            List<OportunidadLogBO> listadoBO = new List<OportunidadLogBO>();
            foreach (var itemEntidad in listado)
            {
                OportunidadLogBO objetoBO = Mapper.Map<TOportunidadLog, OportunidadLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OportunidadLogBO FirstById(int id)
        {
            try
            {
                TOportunidadLog entidad = base.FirstById(id);
                OportunidadLogBO objetoBO = Mapper.Map<TOportunidadLog, OportunidadLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OportunidadLogBO FirstBy(Expression<Func<TOportunidadLog, bool>> filter)
        {
            try
            {
                TOportunidadLog entidad = base.FirstBy(filter);
                OportunidadLogBO objetoBO = Mapper.Map<TOportunidadLog, OportunidadLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OportunidadLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOportunidadLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OportunidadLogBO> listadoBO)
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

        public bool Update(OportunidadLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOportunidadLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OportunidadLogBO> listadoBO)
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
        private void AsignacionId(TOportunidadLog entidad, OportunidadLogBO objetoBO)
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

        private TOportunidadLog MapeoEntidad(OportunidadLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOportunidadLog entidad = new TOportunidadLog();
                entidad = Mapper.Map<OportunidadLogBO, TOportunidadLog>(objetoBO,
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

        /// Repositorio: OportunidadLogRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Retorna el ultimo log de oportunidad encontrado mediante el IdOportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> Objeto BO: OportunidadLogBO </returns>
        public OportunidadLogBO ObtenerUltimoOportunidadLog(int idOportunidad)
        {
            try
            {
                string consultaLogOportunidad = $@"
                                    SELECT Fecha_Log AS FechaLog, 
                                           FechaCambioFase, 
                                           FechaCambioFaseIS, 
                                           IdPersonal_Asignado, 
                                           IdCentroCosto, 
                                           FechaCambioFaseAnt, 
                                           FechaCambioAsesor, 
                                           FechaCambioAsesorAnt, 
                                           CambioFaseAsesor, 
                                           CicloRN2, 
                                           IdClasificacionPersona, 
                                           IdPersonalAreaTrabajo
                                    FROM com.T_OportunidadLog
                                    WHERE IdOportunidad = @idOportunidad
                                          AND Fecha_Log =
                                    (
                                        SELECT MAX(fecha_log)
                                        FROM com.T_OportunidadLog
                                        WHERE IdOportunidad = @idOportunidad
                                    );
                                ";
                var queryLogOportunidad = _dapper.FirstOrDefault(consultaLogOportunidad, new { idOportunidad });
                return JsonConvert.DeserializeObject<OportunidadLogBO>(queryLogOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <summary>
		/// Retorna oportunidad log mediante id de oportunidad
		/// </summary>
		/// <param name="idOportunidad"></param>
		/// <returns></returns>
        public OportunidadLogDTO ObtenerPorIdOportunidad(int idOportunidad) {
			try
			{
				OportunidadLogDTO oportunidadLog = new OportunidadLogDTO();
				var _query = "SELECT Id, IdCentroCosto, IdPersonalAsignado, IdTipoDato, IdOrigen, IdFaseOportunidad, IdFaseOportunidadAnt FROM COM.V_TOportunidadLog_ObtenerPorIdOportunidad WHERE IdOportunidad = @idOportunidad AND Estado = 1 ORDER BY FechaLog DESC";
				var registroDB = _dapper.FirstOrDefault(_query, new { idOportunidad });
				oportunidadLog = JsonConvert.DeserializeObject<OportunidadLogDTO>(registroDB);
				return oportunidadLog;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

        }

		/// <summary>
		/// Retorna lista de logs mediante Id de oportunidad
		/// </summary>
		/// <param name="idOportunidad"></param>
		/// <returns></returns>
		public List<OportunidadLogDTO> ObtenerLogsPorIdOportunidad(int idOportunidad)
		{
			try
			{
				var _query = "SELECT Id, IdCentroCosto, IdPersonalAsignado, IdTipoDato, IdOrigen, IdFaseOportunidad, IdFaseOportunidadAnt FROM COM.V_TOportunidadLog_ObtenerPorIdOportunidad WHERE IdOportunidad = @IdOportunidad AND Estado = 1 ORDER BY FechaLog DESC";
				var registroDB = _dapper.QueryDapper(_query, new { IdOportunidad = idOportunidad });
				return JsonConvert.DeserializeObject<List<OportunidadLogDTO>>(registroDB);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene ultimo log de oportunidad mediante el Id de oportunidad
		/// </summary>
		/// <param name="idOportunidad"></param>
		/// <returns></returns>
		public OportunidadLogBO ObtenerUltimoOportunidadLogPorIdOportunidad(int idOportunidad)
		{
			try
			{
				OportunidadLogBO oportunidadLog = new OportunidadLogBO();

				 
				var _query = "SELECT IdOportunidad,IdCentroCosto,IdPersonalAsignado,IdTipoDato,IdFaseOportunidadAnt,IdFaseOportunidad,IdOrigen,IdContacto,FechaLog,"+
					"IdActividadDetalle,IdOcurrencia,IdOcurrenciaActividad,Comentario,IdCategoriaOrigen,IdConjuntoAnuncio,IdFaseOportunidad_IP,IdFaseOportunidad_IC,"+
					"FechaEnvioFaseOportunidadPf,FechaPagoFaseOportunidadPf,FechaPagoFaseOportunidadIc,FasesActivas,FechaRegistroCampania,IdFaseOportunidad_PF,CodigoPagoIc,"+
					"IdAsesor_Ant,IdCentroCosto_Ant,FechaFinLog,FechaCambioFase,CambioFase,FechaCambioFaseIs,CambioFaseIs,FechaCambioFaseAnt,FechaCambioAsesor,"+
					"FechaCambioAsesorAnt,CambioFaseAsesor,CicloRn2,IdSubCategoriaDato FROM com.V_TOportunidadLog_ObtenerPorIdOportunidad WHERE IdOportunidad =  @idOportunidad AND Estado = 1 ORDER BY FechaLog DESC; ";

				//this.GetBy(x => x.IdOportunidad == idOportunidad, x => new { x.IdOportunidad, x.IdOportunidad, x.IdOportunidad });
				var registroDB = _dapper.FirstOrDefault(_query, new { idOportunidad });

				return JsonConvert.DeserializeObject<OportunidadLogBO>(registroDB);
			}
			catch(Exception Ex)
			{
				throw new Exception(Ex.Message);
			}
		}
        /// Autor: Luis Huallpa - Jose Villena
        /// Fecha: 21/01/2021
        /// Version: 2.0
        /// <summary>
        /// Elimina logicamente la ultima oportunidad log por el idOportunidad para retroceder a la anterior
        /// </summary>
        /// <returns></returns>

        public OportunidadLogRevertirDTO RevertirFaseOportunidad(int idOportunidad, DateTime? FechaProgramada, string usuario)
		{
			try
			{
				var result = _dapper.QuerySPDapper("[com].[SP_RevertirUltimoCambioFaseEliminarOportunidadLog]", new { IdOportunidad=idOportunidad,Usuario=usuario });
				var oportunidadLog = this.GetBy(x => x.IdOportunidad == idOportunidad && x.Estado == true, x => new OportunidadLogRevertirDTO{Id = x.Id, IdOportunidad = x.IdOportunidad, IdCentroCosto = x.IdCentroCosto, IdPersonalAsignado = x.IdPersonalAsignado, IdFaseOportunidad = x.IdFaseOportunidad, IdTipoDato = x.IdTipoDato, IdContacto = x.IdContacto, FechaLog = x.FechaLog, IdClasificacionPersona = x.IdClasificacionPersona}).OrderByDescending(x => x.FechaLog).FirstOrDefault();

				return oportunidadLog;
			}
			catch (Exception e)
				{
				throw new Exception(e.Message);
			}

		}

		public List<ObtenerDetalleOportunidadDTO> ObtenerDetalleOportunidad(int idOportunidad)
		{
			try
			{
				var query = "SELECT FaseInicio, FaseDestino, FechaModificacion, Estado FROM com.ObtenerDetalleOportunidad WHERE Est = 1 AND IdOportunidad = @idOportunidad ORDER BY Fecha DESC";
				var res = _dapper.QueryDapper(query, new { idOportunidad });
				return JsonConvert.DeserializeObject<List<ObtenerDetalleOportunidadDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        public List<DiasSinContactoOportunidadLogDTO> ObtenerFechasSinCotacto(int idOportunidad)
		{
			try
			{
                List<DiasSinContactoOportunidadLogDTO> valor = new List<DiasSinContactoOportunidadLogDTO>();
                var query = "com.SP_TOportunidadLog_SinContacto";
				var res = _dapper.QuerySPDapper(query, new { idOportunidad });
                if (res.Contains("[]"))
                {
                    return valor;
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<DiasSinContactoOportunidadLogDTO>>(res);
                }
				 
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        /// <summary>
        /// Obtiene la cantidad de Log repetidos
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public int CantidadMaximaLogDuplicados()
		{
			try
			{
                
                var query = "com.SP_TOportunidadLog_CantidadDuplicados";
				var res = _dapper.QuerySPFirstOrDefault(query,null);
                if (res.Contains("null") || res == "")
                {
                    return 0;
                }
                else
                {
                    var cantidad = JsonConvert.DeserializeObject<Dictionary<string, int>>(res);
                    return cantidad.Select(w => w.Value).FirstOrDefault();
                }
				 
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        /// <summary>
        /// Elimina Log repetidos mas de 2 veces
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public int EliminarLogDuplicados()
		{
			try
			{
                
                var query = "com.SP_TOportunidadLog_EliminarLogRepetidos";
				var res = _dapper.QuerySPFirstOrDefault(query,null);
                return 1; 
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        
        /// <summary>
        /// Elimina Log repetidos mas de 2 veces
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public int ActualizarLogDuplicados()
		{
			try
			{
                
                var query = "com.SP_TOportunidadLog_ActualizarLogRepetidos";
				var res = _dapper.QuerySPFirstOrDefault(query,null);
                return 1; 
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
