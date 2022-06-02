using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: ActividadDetalleRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Detalle de actividades
    /// </summary>
    public class ActividadDetalleRepositorio : BaseRepository<TActividadDetalle, BO.ActividadDetalleBO>
    {
        #region Metodos Base
        public ActividadDetalleRepositorio() : base()
        {
        }
        public ActividadDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<BO.ActividadDetalleBO> GetBy(Expression<Func<TActividadDetalle, bool>> filter)
        {
            IEnumerable<TActividadDetalle> listado = base.GetBy(filter).ToList();
			List<BO.ActividadDetalleBO> listadoBO = new List<BO.ActividadDetalleBO>();
            foreach (var itemEntidad in listado)
            {
				BO.ActividadDetalleBO objetoBO = Mapper.Map<TActividadDetalle, BO.ActividadDetalleBO>(itemEntidad, (IMappingOperationOptions<TActividadDetalle, BO.ActividadDetalleBO> opt) => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ActividadDetalleBO FirstById(int id)
        {
            try
            {
                TActividadDetalle entidad = base.FirstById(id);
				BO.ActividadDetalleBO objetoBO = new BO.ActividadDetalleBO();
				Mapper.Map<TActividadDetalle, BO.ActividadDetalleBO>(entidad, objetoBO, (IMappingOperationOptions<TActividadDetalle, BO.ActividadDetalleBO> opt) => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ActividadDetalleBO FirstBy(Expression<Func<TActividadDetalle, bool>> filter)
        {
            try
            {
                TActividadDetalle entidad = base.FirstBy(filter);
				BO.ActividadDetalleBO objetoBO = Mapper.Map<TActividadDetalle, BO.ActividadDetalleBO>(entidad, (IMappingOperationOptions<TActividadDetalle, BO.ActividadDetalleBO> opt) => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(BO.ActividadDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TActividadDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<BO.ActividadDetalleBO> listadoBO)
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

        public bool Update(BO.ActividadDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TActividadDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<BO.ActividadDetalleBO> listadoBO)
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
        private void AsignacionId(TActividadDetalle entidad, BO.ActividadDetalleBO objetoBO)
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

        private TActividadDetalle MapeoEntidad(BO.ActividadDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TActividadDetalle entidad = new TActividadDetalle();
				entidad = Mapper.Map<BO.ActividadDetalleBO, TActividadDetalle>(objetoBO,
					(IMappingOperationOptions<BO.ActividadDetalleBO, TActividadDetalle> opt)
						=> opt.ConfigureMap(MemberList.None));//.ForMember(dest => dest.IdMigracion, (IMemberConfigurationExpression<BO.ActividadDetalleBO, TActividadDetalle, Guid?> m) => m.Ignore()));

                //mapea los hijos
                if (objetoBO.LlamadaActividad != null)
                {
                    TLlamadaActividad entidadHijo = new TLlamadaActividad();
					entidadHijo = Mapper.Map<LlamadaActividadBO, TLlamadaActividad>(objetoBO.LlamadaActividad,
						opt => opt.ConfigureMap(MemberList.None));
                    entidad.TLlamadaActividad.Add(entidadHijo);
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
        public void ActualizarDatosEstaticosPantalla2(int idAsesor, int idCategoriaOrigen, int estadoISOM)
        {
            try
            {
                string _querypantalla1 = "com.SP_ObtenerDatosEstaticosPantalla2";
                var querypantalla1 = _dapper.QuerySPDapper(_querypantalla1, new { idAsesor = idAsesor, idCategoriaOrigen = idCategoriaOrigen, estado = estadoISOM });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        /// <summary>
        /// metodos de retorna las actividades ejecutadas filtradas pos idactvidaddetalle
        /// </summary>
        /// <param name="idActividadDetalle"></param>
        /// <returns></returns>
        private List<ActividadEjecutadaTrescxDTO> ObtenerLlamadasTresCXPorActividadDetalleLlamadasTresCXPorActividadDetalle(int idActividadDetalle)
        {
            try
            {
                List<ActividadEjecutadaTrescxDTO> actividadesEjecutadas = new List<ActividadEjecutadaTrescxDTO>();
                var registrosBD = _dapper.QuerySPDapper("com.SP_ObtenerLlamadasTRESCXPorActividadDetalle", new { idActividadDetalle });
                actividadesEjecutadas = JsonConvert.DeserializeObject<List<ActividadEjecutadaTrescxDTO>>(registrosBD);
                return actividadesEjecutadas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// metodo que retorna una lista de actividades compuestas filtradas por un idactividad detalle
        /// solo es usado en ObtenerAgendaRealizadaRegistroTiempoReal
        /// </summary>
        /// <param name="idActividadDetalle"></param>
        /// <returns></returns>
        private List<CompuestoActividadEjecutadaDTO> ObtenerAgendaActividades(int idActividadDetalle)
        {  
            try
            {
                List<CompuestoActividadEjecutadaDTO> compuestosActividadesEjecutadas = new List<CompuestoActividadEjecutadaDTO>();
                var AgendaBD = _dapper.QuerySPDapper("com.SP_AgendaActividadesEjecutadasRealTimeNuevoModelo", new { idActividadDetalle });
                compuestosActividadesEjecutadas = JsonConvert.DeserializeObject<List<CompuestoActividadEjecutadaDTO>>(AgendaBD);
                return compuestosActividadesEjecutadas;
            }
            catch (Exception)
            {
                throw;
            }
           
        }
        /// <summary>
        /// Obtiene la lista de actividades
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteActividadOcurrenciaDTO> ReporteActividadOcurrenciaAgenda(int idOportunidad)
        {
            try
            {
                List<ReporteActividadOcurrenciaDTO> items = new List<ReporteActividadOcurrenciaDTO>();
                var query1 = "SELECT " +
                            "IdOportunidad," +
                            "IdEstadoOcurrencia," +
                            "IdFaseOportunidadAnterior," +
                            "IdFaseActual," +
                            "FechaReal " +
                            "FROM com.V_NumeroActividadesEstadoOcurrencia where IdOportunidad = @IdOportunidad";

                var queryRespuesta1 = _dapper.QueryDapper(query1, new { IdOportunidad = idOportunidad });
                if (!queryRespuesta1.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteActividadOcurrenciaDTO>>(queryRespuesta1);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// metodo que retorna un objeto de actividad ejecutada que genera un reporte filtrado por
        /// idactividaddetalles
        /// </summary>
        /// <param name="idActividadDetalle"></param>
        /// <returns></returns>
        public CompuestoActividadEjecutadaDTO ObtenerAgendaRealizadaRegistroTiempoReal(int idActividadDetalle)
        {
            //testCompuestoActividadesEjecutadasDTO
          
            List<CompuestoActividadEjecutadaDTO> temp = this.ObtenerAgendaActividades(idActividadDetalle);
            //IList<testCompuestoActividadesEjecutadasDTO> temp = _tcrmHojaOportunidadService.getAgendaRealizadaRegistro(filtroDTO);

            var result = (from p in temp
                          group p by new
                          {
                              p.Id,
                              p.CentroCosto,
                              p.Contacto,
                              p.CodigoFase,
                              p.NombreTipoDato,
                              p.Origen,
                              p.FechaProgramada,
                              p.FechaReal,
                              p.Duracion,
                              p.Actividad,
                              p.Ocurrencia,
                              p.Comentario,
                              p.Asesor,
                              p.IdContacto,
                              p.IdOportunidad,
                              p.ProbActual,
                              p.Ca_nombre,//ca_nombre
                              p.IdCategoria,
                              p.FaseInicial,
                              p.FaseMaxima,
                              p.TotalOportunidades,
                              p.UnicoTimbrado,
                              p.UnicoContesto,
                              p.UnicoEstadoLlamada,
                              p.NumeroLlamadas,
                              p.Estado,
                              p.UnicoClasificacion,
                              p.UnicoFechaLlamada,
                              p.NombreGrupo,
                              p.IdFaseOportunidadInicial,
                              p.FechaModificacion

                          } into g
                          select new CompuestoActividadesEjecutadasTempDTO
                          {
                              Id = g.Key.Id,
                              CentroCosto = g.Key.CentroCosto,
                              Contacto = g.Key.Contacto,
                              CodigoFase = g.Key.CodigoFase,
                              NombreTipoDato = g.Key.NombreTipoDato,
                              Origen = g.Key.Origen,
                              FechaProgramada = g.Key.FechaProgramada,
                              FechaReal = g.Key.FechaReal,
                              Duracion = g.Key.Duracion,
                              Actividad = g.Key.Actividad,
                              Ocurrencia = g.Key.Ocurrencia,
                              Comentario = g.Key.Comentario,
                              Asesor = g.Key.Asesor,
                              IdContacto = g.Key.IdContacto,
                              IdOportunidad = g.Key.IdOportunidad,
                              ProbActual = g.Key.ProbActual,
                              Ca_nombre = g.Key.Ca_nombre,
                              IdCategoria = g.Key.IdCategoria,
                              FaseInicial = g.Key.FaseInicial,
                              FaseMaxima = g.Key.FaseMaxima,
                              TotalOportunidades = g.Key.TotalOportunidades,
                              UnicoTimbrado = g.Key.UnicoTimbrado,
                              UnicoContesto = g.Key.UnicoContesto,
                              UnicoEstadoLlamada = g.Key.UnicoEstadoLlamada,
                              NumeroLlamadas = g.Key.NumeroLlamadas,
                              Estado = g.Key.Estado,
                              NombreGrupo = g.Key.NombreGrupo,
                              IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                              FechaModificacion = g.Key.FechaModificacion,

                              lista = g.Select(o => new CompuestoActividadesEjecutadasTemp_DetalleDTO
                              {
                                  Id = o.IdLlamada,
                                  DuracionTimbrado = o.DuracionTimbrado,
                                  DuracionContesto = o.DuracionContesto,
                                  EstadoLlamada = o.EstadoLlamada,
                                  FechaLlamada = o.FechaLlamadaIntegra,
                                  FechaLlamadaFin = o.FechaLlamadaFin,
                                  SubEstadoLlamada = o.SubEstadoLlamadaIntegra,

                              }).OrderByDescending(o => o.FechaLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                              llamadasTresCX = g.Select(o => new CompuestoActividadesEjecutadasTemp_DetalleDTO
                              {
                                  Id = o.IdTresCX,
                                  DuracionContesto = o.TiempoContestoTresCx.ToString(),
                                  DuracionTimbrado = o.TiempoTimbradoTresCx.ToString(),
                                  EstadoLlamada = o.EstadoLlamadaTresCX,
                                  FechaLlamada = o.FechaIncioLlamadaTresCX,
                                  FechaLlamadaFin = o.FechaFinLlamadaTresCX,
                                  SubEstadoLlamada = o.SubEstadoLlamadaTresCX,

                              }).OrderBy(o => o.FechaLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()

                          });

            var item = result.FirstOrDefault();

            CompuestoActividadEjecutadaDTO item_detalle = new CompuestoActividadEjecutadaDTO()
            {

                Id = item.Id,
                CentroCosto = item.CentroCosto,
                Contacto = item.Contacto,
                CodigoFase = item.CodigoFase,
                NombreTipoDato = item.NombreTipoDato,
                Origen = item.Origen,
                FechaProgramada = item.FechaProgramada,
                FechaReal = item.FechaReal,
                Duracion = item.Duracion,
                Actividad = item.Actividad,
                Ocurrencia = item.Ocurrencia,
                Comentario = item.Comentario,
                Asesor = item.Asesor,
                IdContacto = item.IdContacto,
                IdOportunidad = item.IdOportunidad,
                ProbActual = item.ProbActual,
                Ca_nombre = item.Ca_nombre,
                IdCategoria = item.IdCategoria,
                FaseInicial = item.FaseInicial,
                FaseMaxima = item.FaseMaxima,
                TotalOportunidades = item.TotalOportunidades,
                UnicoTimbrado = item.UnicoTimbrado,
                UnicoContesto = item.UnicoContesto,
                UnicoEstadoLlamada = item.UnicoEstadoLlamada,
                Estado = item.Estado,
                NombreGrupo = item.NombreGrupo,

            };

            if (item.lista != null && item.lista.Select(s => s.DuracionTimbrado).FirstOrDefault() != null)
            {

                item_detalle.NumeroLlamadas = item.lista.Count().ToString();
                item.lista = item.lista.OrderBy(x => x.FechaLlamada).ToList();

                item_detalle.DuracionTimbrado = String.Concat(item.lista.Select(o => " <strong >TT:</strong > / " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + "<br />"));
                item_detalle.EstadoLlamada = String.Concat(item.lista.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
                item_detalle.FechaLlamada = String.Concat(item.lista.Select(o => "<strong >I: </strong >" + o.FechaLlamada.Value.ToString("HH:mm:ss") + "<strong> T: </strong >" + o.FechaLlamadaFin.Value.ToString("HH:mm:ss") + "<br />"));

            }
            else
            {
                string date = item.UnicoFechaLlamada == null ? "" : item.UnicoFechaLlamada.Value.ToString("yyyy/MM/dd HH:mm");
                item_detalle.NumeroLlamadas = "1";
                item_detalle.DuracionTimbrado = item.UnicoEstadoLlamada + " <strong >- TT:</strong >" + item.UnicoTimbrado + "  <strong >TC:</strong >" + item.UnicoContesto + " <strong >-</strong > " + date + "<br /><strong id='estadoNuevoT'>Nuevo Estado: </strong ><strong id='estadoNuevoC'>" + item.UnicoClasificacion + "</strong><br />";

            }
            item_detalle.MinutosIntervale = 0;
            item_detalle.MinutosTotalContesto = 0;
            item_detalle.MinutosTotalTimbrado = 0;
            item_detalle.MinutosTotalPerdido = -1;

            //var llamadasTresCX = this.GetLlamadasTresCXPorActividadDetalle(item.Id);

            item_detalle.TiemposTresCX = String.Concat(item.llamadasTresCX.Select(o => " <strong >TT:</strong > / " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + "<br />"));
            item_detalle.EstadosTresCX = String.Concat(item.llamadasTresCX.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
            var listaActividades = ReporteActividadOcurrenciaAgenda(item.IdOportunidad);
            item_detalle.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
            item_detalle.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
            item_detalle.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
            item_detalle.NombreGrabacionTresCX = "-";
            item_detalle.NombreGrabacionIntegra = "-";
            return item_detalle;

        }


        /// <summary>
        /// Obtiene las llamadas de 3CX por IdActividadDetalle que han sido ejecutadas
        /// </summary>
        /// <param name="idActividadDetalle"></param>
        /// <returns>Retorna lista de actividades ejecutadas</returns>
        public List<ActividadesEjecutadasTresCX> GetLlamadasTresCXPorActividadDetalle(int idActividadDetalle)
        {
            List<ActividadesEjecutadasTresCX> actividadesEjecutadas = new List<ActividadesEjecutadasTresCX>();
            try
            {
                var registrosBD = _dapper.QuerySPDapper("com.SP_ObtenerLlamadasTRESCXPorActividadDetalle", new { IdActividadDetalle = idActividadDetalle });
                actividadesEjecutadas = JsonConvert.DeserializeObject<List<ActividadesEjecutadasTresCX>>(registrosBD);
                return actividadesEjecutadas;
            }
            catch (Exception)
            {
                return actividadesEjecutadas;
            }

        }

        public class ActividadesEjecutadasTresCX
        {
            public int DuracionTimbrado { get; set; }
            public int DuracionContesto { get; set; }
            public string EstadoLlamada { get; set; }
            public DateTime FechaLlamada { get; set; }
        }

        /// <summary>
        /// Método que retorna un objeto bo de tipo ActividadDetalle de actividades detalle encontradas por el Id de actividad detalle
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActividadDetalleBO ObtenerActividadDetallePorId(int Id)
		{
			try
			{
				string query = "SELECT Id,IdActividadCabecera,FechaProgramada,FechaReal,DuracionReal,IdOcurrencia,IdEstadoActividadDetalle,Comentario,IdAlumno,Actor,IdOportunidad,IdCentralLlamada,RefLlamada,IdOcurrenciaActividad FROM com.V_ActividadDetalle_ObtenerInformacion WHERE Estado = 1 and Id = @id";
				var _actividadDetalle = _dapper.FirstOrDefault(query, new { id = Id });
				return JsonConvert.DeserializeObject<BO.ActividadDetalleBO>(_actividadDetalle);
			}
			catch(Exception Ex)
			{
				throw new Exception(Ex.Message);
			}
		}

		/// <summary>
		/// Método que retorna una Lista de ActividadDetalle
		/// </summary>
		/// <param name="IdOportunidad"></param>
		/// <returns></returns>
		public List<DTOs.ActividadDetalleDTO> ObtenerActividadDetallePorIdOportunidad(int IdOportunidad)
		{
			try
			{
				string query = "SELECT Id,IdActividadCabecera,FechaProgramada,FechaReal,DuracionReal,IdOcurrencia,IdEstadoActividadDetalle,Comentario,IdAlumno,Actor,IdOportunidad,IdCentralLlamada,RefLlamada,IdOcurrenciaActividad FROM  com.T_ActividadDetalle WHERE Estado = 1 and IdOportunidad = @IdOportunidad";
				var _actividadDetalle = _dapper.QueryDapper(query, new { IdOportunidad});
				return JsonConvert.DeserializeObject<List<DTOs.ActividadDetalleDTO>>(_actividadDetalle);
			}
			catch (Exception Ex)
			{
				throw new Exception(Ex.Message);
			}
		}
	}
}
