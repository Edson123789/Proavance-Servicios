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
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Planificacion/PEspecificoSesion
    /// Autor: Fischer Valdez - Carlos Crispin - Luis Huallpa - Priscila Pacsi - Gian Miranda
    /// Fecha: 05/02/2021
    /// <summary>
    /// Repositorio para consultas de pla.T_PespecificoSesion
    /// </summary>
    public class PespecificoSesionRepositorio : BaseRepository<TPespecificoSesion, PespecificoSesionBO>
    {
        #region Metodos Base
        public PespecificoSesionRepositorio() : base()
        {
        }
        public PespecificoSesionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PespecificoSesionBO> GetBy(Expression<Func<TPespecificoSesion, bool>> filter)
        {
            IEnumerable<TPespecificoSesion> listado = base.GetBy(filter);
            List<PespecificoSesionBO> listadoBO = new List<PespecificoSesionBO>();
            foreach (var itemEntidad in listado)
            {
                PespecificoSesionBO objetoBO = Mapper.Map<TPespecificoSesion, PespecificoSesionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PespecificoSesionBO FirstById(int id)
        {
            try
            {
                TPespecificoSesion entidad = base.FirstById(id);
                PespecificoSesionBO objetoBO = Mapper.Map<TPespecificoSesion, PespecificoSesionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PespecificoSesionBO FirstBy(Expression<Func<TPespecificoSesion, bool>> filter)
        {
            try
            {
                TPespecificoSesion entidad = base.FirstBy(filter);
                PespecificoSesionBO objetoBO = Mapper.Map<TPespecificoSesion, PespecificoSesionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PespecificoSesionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPespecificoSesion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PespecificoSesionBO> listadoBO)
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

        public bool Update(PespecificoSesionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPespecificoSesion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PespecificoSesionBO> listadoBO)
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
        private void AsignacionId(TPespecificoSesion entidad, PespecificoSesionBO objetoBO)
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

        private TPespecificoSesion MapeoEntidad(PespecificoSesionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPespecificoSesion entidad = new TPespecificoSesion();
                entidad = Mapper.Map<PespecificoSesionBO, TPespecificoSesion>(objetoBO,
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
        /// Genera un dto compuesto para procesar pdf
        /// </summary>
        /// <param name="programaEspecifico"></param>
        /// <returns></returns>
        public List<PespecificoSesionCompuestoDTO> ObtenerCronogramaIndividualPorPEspecifico(DatosProgramaEspecificoDTO programaEspecifico)
        {
            try
            {
                string _queryListaSesion = "Select Id,FechaHoraInicio,Duracion,IdExpositor,IdProveedor,Comentario,IdAmbiente From pla.V_ListaPespecificoSesion Where Estado=1 and IdPEspecifico=@IdPespecifico ";
                var queryListaSesion = _dapper.QueryDapper(_queryListaSesion, new { IdPespecifico = programaEspecifico.Id });
                List<InformacionProgramaEspecificoSesionDTO> listaSesiones = JsonConvert.DeserializeObject<List<InformacionProgramaEspecificoSesionDTO>>(queryListaSesion);

                var rpta = listaSesiones.Select(Sesiones => new PespecificoSesionCompuestoDTO
                {
                    Id = Sesiones.Id,
                    FechaHoraInicio = Sesiones.FechaHoraInicio,
                    Duracion = Sesiones.Duracion,
                    DuracionTotal = string.IsNullOrEmpty(programaEspecifico.Duracion) ? 0 : Convert.ToDecimal(programaEspecifico.Duracion),
                    Curso = programaEspecifico.Nombre,
                    IdExpositor = Sesiones.IdExpositor,
                    IdProveedor = Sesiones.IdProveedor,
                    IdAmbiente = Sesiones.IdAmbiente,
                    IdCiudad = programaEspecifico.IdCiudad,
                    PEspecificoHijoId = programaEspecifico.Id,
                    Tipo = programaEspecifico.Tipo,
                    Comentario = Sesiones.Comentario,
                    EsSesionInicial = Sesiones.Id == programaEspecifico.IdSesion_Inicio,
                    MostrarPDF = true
                });
                return rpta.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene El id de los Pespecificos que estan en las sesiones
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<int> ListaPespecificoSesiones(int idPespecifico)
        {
            try
            {
                string _queryListaPespecifico = "Select Id from pla.V_TPespecificoSesionporIdEspecifico Where Estado=1 and IdPEspecifico=@IdPespecifico";
                var queryListaPespecifico = _dapper.QueryDapper(_queryListaPespecifico, new { IdPespecifico = idPespecifico });
                var listaPespecificoSesion = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(queryListaPespecifico);

                var _listaIds = listaPespecificoSesion.SelectMany(x => x.Values).ToList();

                return _listaIds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
        }
        /// <summary>
        /// Obtiene Sesiones pespecifico hijos
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<PespecificoSesionCompuestoDTO> ListaPespecificoSesioneshijos(int idPespecifico)
        {
            try
            {
                string _querySesionesPespecificoCompuesto = "select Id,FechaHoraInicio,Duracion,DuracionTotal,Curso,IdExpositor,IdProveedor,IdAmbiente,IdCiudad,PEspecificoHijoId,Tipo,Comentario,EsSesionInicial,MostrarPDF,PEspecificoPadreId,Estado " +
														"from pla.V_ListaSesionesProgramaEspecificoHijo Where Estado=1 and SesionEstado=1 and PEspecificoPadreId=@IdPespecifico Order By PEspecificoHijoId ASC, FechaHoraInicio Asc";
                var querySesionesPespecificoCompuesto = _dapper.QueryDapper(_querySesionesPespecificoCompuesto, new { IdPespecifico = idPespecifico });
                return JsonConvert.DeserializeObject<List<PespecificoSesionCompuestoDTO>>(querySesionesPespecificoCompuesto);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
        }
        
        /// <summary>
        /// Obtiene Sesiones por idpespecifico
        /// </summary>
        /// <param name="idSesion"></param>
        /// <returns></returns>
        public RegistroSesionDTO ObtnerSesionPorIdSesion(int idSesion)
        {
            try
            {
                string _querySesiones = "Select Id,FechaHoraInicio,Duracion,IdAmbiente,IdExpositor From pla.V_TPEspecificoSesionPorId Where Estado=1 and Id=@IdSesion";
                var querySesiones = _dapper.FirstOrDefault(_querySesiones, new { IdSesion = idSesion });
                return  JsonConvert.DeserializeObject<RegistroSesionDTO>(querySesiones);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
        }

        public PadrePespecificoHijoCompuestoDTO ObtenerDatosPespecificoHijoPorSesion(int idSesion)
        {
            try
            {
                string _querySesiones = "Select Id,IdSesion,PEspecificoHijoId,PEspecificoPadreId From pla.V_TPEspecificoSesion_ObtenerHijo Where Estado=1 and IdSesion=@IdSesion";
                var querySesiones = _dapper.FirstOrDefault(_querySesiones, new { IdSesion = idSesion });
                return  JsonConvert.DeserializeObject<PadrePespecificoHijoCompuestoDTO>(querySesiones);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
        }

        /// <summary>
        /// Obtiene Sesiones pespecifico por el centro de costo
        /// </summary>
        /// <param name="listaIdCentroCosto"></param>
        /// <returns></returns>
        public List<PespecificoSesionCentroCostoDTO> ObtenerTodoSesionesPorCentroCosto(int [] listaIdCentroCosto)
        {
            try
            {
                List<PespecificoSesionCentroCostoDTO> obtenerSesionCompletaPorIdCentroCosto = new List<PespecificoSesionCentroCostoDTO>();
                var _query = "SELECT id, IdPespecifico, title, \"start\", \"end\", DuracionTotal, Comentario, allDay, editable, NombreExpositor, NombreAmbiente FROM pla.V_ObtenerSesionPorCentroCosto Where IdCentroCosto in @listaIdCentroCosto AND EstadoPEspecificoSesion = 1";
                var obtenerSesionCompletaPorIdCentroCostoDB = _dapper.QueryDapper(_query, new { listaIdCentroCosto });
                obtenerSesionCompletaPorIdCentroCosto = JsonConvert.DeserializeObject<List<PespecificoSesionCentroCostoDTO>>(obtenerSesionCompletaPorIdCentroCostoDB);
                return obtenerSesionCompletaPorIdCentroCosto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<PespecificoSesionCompuestoDatosDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PespecificoSesionCompuestoDatosDTO
                {
                    Id = y.Id,
                    IdPespecifico = y.IdPespecifico,
                    FechaHoraInicio = y.FechaHoraInicio,
                    Duracion = y.Duracion,
                    IdExpositor = y.IdExpositor,
                    Comentario = y.Comentario,
                    SesionAutoGenerada = y.SesionAutoGenerada,
                    IdAmbiente = y.IdAmbiente,
                    Predeterminado = y.Predeterminado,

                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///Obtiene las sesiones por pespecifico, ambientes, expositor
        /// </summary>
        /// <param name="pEspecifico"></param>
        /// <param name="idAmbiente"></param>
        /// <param name="idExpositor"></param>
        /// <returns></returns>
        public List<PespecificoSesionCentroCostoDTO> ObtenerTodoSesionPorAmbienteExpositorPEspecifico(int? pEspecifico, int? idAmbiente, int? idExpositor)
        {
            try
            {
                List<PespecificoSesionCentroCostoDTO> obtenerSesionPorAmbienteExpositorPEspecifico = new List<PespecificoSesionCentroCostoDTO>();
                var _query = string.Empty;
                if (pEspecifico.HasValue )
                {
                    _query = "SELECT id, IdPespecifico, title, \"start\", \"end\", DuracionTotal, Comentario, allDay, editable, NombreExpositor, NombreAmbiente FROM pla.V_ObtenerSesionPorAmbienteExpositorPEspecifico WHERE PEspecificoPadreId = @pEspecifico AND EstadoPEspecificoSesion = 1";
                }
                else
                {
                    _query = "SELECT id, IdPespecifico, title, \"start\", \"end\", DuracionTotal, Comentario, allDay, editable, NombreExpositor, NombreAmbiente FROM pla.V_ObtenerSesionPorAmbienteExpositorPEspecifico Where IdExpositor = @idExpositor AND EstadoPEspecificoSesion = 1";
                }
                var obtenerSesionPorAmbienteExpositorPEspecificoDB = _dapper.QueryDapper(_query, new { pEspecifico, idAmbiente, idExpositor });
                obtenerSesionPorAmbienteExpositorPEspecifico = JsonConvert.DeserializeObject<List<PespecificoSesionCentroCostoDTO>>(obtenerSesionPorAmbienteExpositorPEspecificoDB);
                return obtenerSesionPorAmbienteExpositorPEspecifico;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Se obtiene las sedes por idCiudad, idSede
        /// </summary>
        /// <param name="idCiudad"></param>
        /// <param name="idSede"></param>
        /// <param name="idAmbiente"></param>
        /// <returns></returns>
        public List<PespecificoSesionCentroCostoDTO> ObtenerTodoSesionPorCiudadLocacionAmbiente(int? idCiudad, int? idSede, int? idAmbiente)
        {
            try
            {
                List<PespecificoSesionCentroCostoDTO> obtenerSesionPorCiudadLocacionAmbiente = new List<PespecificoSesionCentroCostoDTO>();
                var _query = "SELECT id, IdPespecifico, title, \"start\", \"end\", DuracionTotal, Comentario, allDay, editable, NombreExpositor, NombreAmbiente FROM pla.V_ObtenerSesionPorCiudadLocacionAmbiente Where IdCiudadRegion = @idCiudad AND IdLocacion = @idSede AND IdAmbiente = @idAmbiente AND EstadoPEspecificoSesion = 1";
                var obtenerSesionPorCiudadLocacionAmbienteDB = _dapper.QueryDapper(_query, new { idCiudad, idSede, idAmbiente });
                obtenerSesionPorCiudadLocacionAmbiente = JsonConvert.DeserializeObject<List<PespecificoSesionCentroCostoDTO>>(obtenerSesionPorCiudadLocacionAmbienteDB);
                return obtenerSesionPorCiudadLocacionAmbiente;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		/// <summary>
		/// Valida los cruces entre fechas y docentes en los cronogramas
		/// </summary>
		/// <param name="objeto"></param>
		/// <returns></returns>
		public List<CruceSesionPEspecificoDTO> ValidarCrucesSesiones(InformacionCronogramaSesionesDTO objeto, double duracion)
		{
			try
			{
				var query = "";
				var res = "";
				if (objeto.IdAmbiente != null && objeto.IdProveedor == null)
				{
					query = "SELECT DISTINCT IdPEspecifico, Curso,NombreCentroCosto, Ambiente, Expositor,Proveedor,IdProveedor, Duracion, FechaHoraInicio, FechaFin, IdAmbiente,IdExpositor FROM pla.V_ObtenerInformacionSesionesPEspecifico WHERE (@Fecha BETWEEN FechaHoraInicio AND FechaFin OR @FechaFin BETWEEN FechaHoraInicio AND FechaFin) AND IdAmbiente = @IdAmbiente AND Estado = 1 AND Id != @Id";
					res = _dapper.QueryDapper(query, new { Fecha = objeto.FechaHoraInicio, FechaFin = objeto.FechaHoraInicio.AddHours(duracion), IdAmbiente = objeto.IdAmbiente, Id = objeto.Id });
				}
				else if (objeto.IdProveedor != null && objeto.IdAmbiente == null)
				{
					query = "SELECT DISTINCT IdPEspecifico, Curso,NombreCentroCosto, Ambiente, Expositor,Proveedor,IdProveedor, Duracion, FechaHoraInicio, FechaFin, IdAmbiente,IdExpositor FROM pla.V_ObtenerInformacionSesionesPEspecifico WHERE (@Fecha BETWEEN FechaHoraInicio AND FechaFin OR @FechaFin BETWEEN FechaHoraInicio AND FechaFin) AND IdProveedor = @IdProveedor AND Estado = 1 AND Id != @Id";
					res = _dapper.QueryDapper(query, new { Fecha = objeto.FechaHoraInicio, FechaFin = objeto.FechaHoraInicio.AddHours(duracion), IdProveedor = objeto.IdProveedor, Id = objeto.Id });
				}
				else
				{
					query = "SELECT DISTINCT IdPEspecifico, Curso,NombreCentroCosto, Ambiente, Expositor,Proveedor,IdProveedor, Duracion, FechaHoraInicio, FechaFin, IdAmbiente,IdExpositor FROM pla.V_ObtenerInformacionSesionesPEspecifico WHERE (@Fecha BETWEEN FechaHoraInicio AND FechaFin OR @FechaFin BETWEEN FechaHoraInicio AND FechaFin) AND (IdAmbiente = @IdAmbiente OR IdProveedor = @IdProveedor) AND Estado = 1 AND Id != @Id";
					res = _dapper.QueryDapper(query, new { Fecha = objeto.FechaHoraInicio, FechaFin = objeto.FechaHoraInicio.AddHours(duracion), IdAmbiente = objeto.IdAmbiente, IdProveedor = objeto.IdProveedor, Id = objeto.Id });
				}

				var registros = JsonConvert.DeserializeObject<List<CruceSesionPEspecificoDTO>>(res);
				return registros;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene Id de las sesiones mediante id programa especifico padre
		/// </summary>
		/// <param name="idPespecificoPadre"></param>
		/// <param name="numeroGrupo"></param>
		/// <returns></returns>
		public List<ProgramaEspecificoIdDTO> ObtenerIdSesiones(int idPespecificoPadre, int numeroGrupo)
		{
			try
			{
				var query = "SELECT Id FROM pla.V_ObtenerIdProgramaEspecificoSesion WHERE Estado = 1 AND EsSesionInicio = 1 AND Grupo = @Grupo AND PEspecificoPadreId = @PEspecificoPadreId";
				var listaId = _dapper.QueryDapper(query,new { Grupo = numeroGrupo, PEspecificoPadreId = idPespecificoPadre});
				return JsonConvert.DeserializeObject<List<ProgramaEspecificoIdDTO>>(listaId);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene Id de las sesiones mediante id programa especifico individuales
		/// </summary>
		/// <param name="idPespecificoPadre"></param>
		/// <param name="numeroGrupo"></param>
		/// <returns></returns>
		public List<ProgramaEspecificoIdDTO> ObtenerIdSesionesIndividuales(int idPespecifico, int numeroGrupo)
		{
			try
			{
				var query = "SELECT Id FROM pla.V_ObtenerIdProgramaEspecificoSesionIndividual WHERE Estado = 1 AND EsSesionInicio = 1 AND Grupo = @Grupo AND IdPEspecifico = @IdPEspecifico";
				var listaId = _dapper.QueryDapper(query, new { Grupo = numeroGrupo, IdPEspecifico = idPespecifico });
				return JsonConvert.DeserializeObject<List<ProgramaEspecificoIdDTO>>(listaId);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <summary>
        /// Obtiene las cuentas Webex
        /// </summary>
        /// <returns></returns>
        public List<TokenWebexDTO> ObtenerCuentasWebex()
        {
            try
            {
                var query = "SELECT Id,Cuenta,Token FROM pla.T_TokenWebex WHERE Estado = 1";
                var listaId = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<TokenWebexDTO>>(listaId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene las sesiones de 1 dia
        /// </summary>
        /// <returns></returns>
        public List<SesionesWebexDTO> ObtenerSesionesCuentaporDia(int idCuenta,DateTime Fecha)
        {
            try
            {
                var query = "select Id,dateadd(MINUTE,1,FechaHoraInicio)HoraInicio,dateadd(MINUTE,-1,(dateadd(hour,Duracion,FechaHoraInicio))) HoraFin from pla.t_pespecificosesion where estado=1 and UrlWebex is not null and CuentaWebex=@IdCuenta and convert(date,FechaHoraInicio)= convert(date,@FechaComparar)";
                var listaId = _dapper.QueryDapper(query, new { IdCuenta = idCuenta, FechaComparar = Fecha });
                return JsonConvert.DeserializeObject<List<SesionesWebexDTO>>(listaId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Cronograma de sesiones por idPespecifico grupo cronograma y grupo sesion
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <param name="grupoSesion"></param>
        /// <param name="grupoCronograma"></param>
        /// <returns></returns>
        public List<CronogramaGrupoSesionDTO> ObtenerCronogramaGrupoSesion(int idPespecifico, int grupoCronograma, int grupoSesion)
		{
			try
			{
				var query = "SELECT IdPespecifico, IdPespecificoSesion, IdExpositor, IdCiudad, FechaHoraInicio, Ciudad, Duracion, GrupoSesion, GrupoCronograma, NombreExpositor FROM[pla].[V_ObtenerSesionesPorPespecificoGrupoSesion] WHERE Estado = 1 AND IdPespecifico = @idPespecifico AND GrupoCronograma = @grupoCronograma AND GrupoSesion = @grupoSesion ORDER BY FechaHoraInicio ASC";
				var res = _dapper.QueryDapper(query, new { idPespecifico, grupoCronograma, grupoSesion });
				return JsonConvert.DeserializeObject<List<CronogramaGrupoSesionDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


        /// <summary>
        /// Obtiene la sede en formato de html
        /// </summary>
        /// <param name="id">Id de la sesion del PEspecifico (PK de la tabla pla.T_PEspecificoSesion)</param>
        /// <returns>Cadena formateada de la URL de ubicaciond e la ciudad</returns>
        public string ObtenerUrlUbicacionCiudad(int id)
        {
            try
            {
                var urlUbicacion = "";
                var resultadoFinal = new ValorIntDTO();
                var query = $@"ope.SP_ObtenerIdCiudadDictadoClasesPorPEspecificoSesion";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdPEspecificoSesion = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }

                var IdCiudad = resultadoFinal.Valor;

                if (IdCiudad == ValorEstatico.IdCiudadArequipa)
                {
                    urlUbicacion = "https://www.google.com/maps/place/BSG+Institute/@-16.391617,-71.549994,17z/data=!4m5!3m4!1s0x0:0xfa338199798a589d!8m2!3d-16.3916171!4d-71.5499941?hl=en";
                }
                else if (IdCiudad == ValorEstatico.IdCiudadLima)
                {
                    urlUbicacion = "https://www.google.com/maps/place/BSG+Institute/@-12.118881,-77.035406,15z/data=!4m5!3m4!1s0x0:0x44216f10931e46b7!8m2!3d-12.1188811!4d-77.0354064?hl=en";
                }
                else if (IdCiudad == ValorEstatico.IdCiudadBogota)
                {
                    urlUbicacion = "https://www.google.com/maps/search/Av+Marcelo+Terceros+B%C3%A1nzer+304,+Santa+Cruz+de+la+Sierra,+Santa+Cruz,+Bolivia/@-17.763943,-63.197579,15z?hl=en";
                }
                else if (IdCiudad == ValorEstatico.IdCiudadSantaCruz)
                {
                    urlUbicacion = "https://www.google.com/maps/search/Cra.+45+%23%23108-27+Bogot%C3%A1,+Cundinamarca+Colombia/@4.696379,-74.056552,17z?hl=en";
                }

                return urlUbicacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la direccion de dictado de clases por sesión
        /// </summary>
        /// <param name="id">Id de la sesion del PEspecifico (PK de la tabla pla.T_PEspecificoSesion)</param>
        /// <returns>Cadena formateada del PEspecificoSesion</returns>
        public string ObtenerDireccionDictadoClases(int id)
        {
            try
            {
                var resultadoFinal = new ValorStringDTO();
                var query = $@"ope.SP_ObtenerDireccionDictadoClasesPorPEspecificoSesion";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdPEspecificoSesion = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la direccion de dictado de clases por sesión
        /// </summary>
        /// <param name="id">Id de la sesion del PEspecifico (PK de la tabla pla.T_PEspecificoSesion)</param>
        /// <returns>Cadena formateada con el nombre de la ciudad donde se lleva a cabo el dictado de la sesion</returns>
        public string ObtenerNombreCiudadDictadoClases(int id)
        {
            try
            {
                var resultadoFinal = new ValorStringDTO();
                var query = $@"ope.SP_ObtenerNombreCiudadDictadoClasesPorPEspecificoSesion";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdPEspecificoSesion = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el nombre del docente que dicta la clase
        /// </summary>
        /// <param name="id">Id de la sesion del PEspecifico (PK de la tabla pla.T_PEspecificoSesion)</param>
        /// <returns>Cadena formateada con el nombre del docente encargado del dictado de clases</returns>
        public string ObtenerNombreDocenteDictadoClases(int id)
        {
            try
            {
                var resultadoFinal = new ValorStringDTO();
                var query = $@"ope.SP_ObtenerNombreDocentePorPEspecificoSesion";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdPEspecificoSesion = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los alumnos disponibles para una sesion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PEspecificoSesionAsistenciaDetalleDTO> ObtenerAlumnosAsistencia(int id)
        {
            try
            {
                var lista = new List<PEspecificoSesionAsistenciaDetalleDTO>();
                var query = $@"
                            SELECT IdAsistencia, 
                                   IdMatriculaCabecera, 
                                   IdPEspecificoSesion, 
                                   CodigoMatricula, 
                                   NombreAlumno, 
                                   Asistio, 
                                   Justifico
                            FROM ope.V_ObtenerAsistenciaMatriculaSesion
                            WHERE EstadoAsistencia = 1
                                  AND EstadoMatriculaCabecera = 1
                                  AND EstadoPEspecificoSesion = 1
                                  AND EstadoAlumno = 1
                                  AND IdPEspecificoSesion = @id;
                ";
                var resultadoDB = _dapper.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PEspecificoSesionAsistenciaDetalleDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtener detalle asistencia
        /// </summary>
        /// <param name="idAsistencia"></param>
        /// <returns></returns>
        public List<EntregaMaterialDetalleDTO> ObtenerMaterialesPorAsistencia(int idAsistencia)
        {
            try
            {
                var lista = new List<EntregaMaterialDetalleDTO>();
                var query = $@"
                            SELECT IdAsistencia, 
                                   NombreMaterial, 
                                   IdMaterialEntrega, 
                                   IdMaterialVersion,
                                   Entregado, 
                                   Comentario
                            FROM ope.V_ObtenerAsistenciaEntregaMaterial
                            WHERE IdAsistencia = @idAsistencia
                                  AND EstadoPEspecificoSesion = 1
                                  AND EstadoMaterialPEspecificoSesion = 1
                                  AND EstadoMaterialVersion = 1
                                  AND EstadoAsistencia = 1
                                  AND (EstadoMaterialEntrega = 1
                                       OR EstadoMaterialEntrega IS NULL);
                ";
                var resultadoDB = _dapper.QueryDapper(query, new { idAsistencia });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<EntregaMaterialDetalleDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		public List<FiltroDTO> ObtenerGruposProgramaEspecificoFiltro()
		{
			try
			{
				var grupos = GetBy(x => x.Estado == true).GroupBy(x => x.Grupo).Select(x => new FiltroDTO
				{
					Id = x.Key,
					Nombre = "Grupo " + x.Key
				}).ToList();

				return grupos;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        public int ObtenerSesionInicial(int IdPespecifico, int Grupo)
		{
            try
            {
                ValorIntDTO valor = new ValorIntDTO();
                var query = "SELECT  IdPespecificoSesion AS Valor From [pla].[V_ObtenerSesionesPorPespecificoGrupoSesion] WHERE Estado = 1 AND IdPespecifico = @IdPespecifico AND GrupoCronograma = @Grupo ORDER BY FechaHoraInicio ASC";
                var res = _dapper.FirstOrDefault(query, new { IdPespecifico, Grupo });

                valor = JsonConvert.DeserializeObject<ValorIntDTO>(res);

                return valor.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ResumenRegistroAsistenciaDTO> ObtenerResumenRegistroAsistencia(int idPEspecifico, int grupo)
        {
            try
            {
                List<ResumenRegistroAsistenciaDTO> listado = new List<ResumenRegistroAsistenciaDTO>();
                var query = "SELECT IdPEspecifico, Grupo, FechaHoraInicio, TotalAsistenciaRegistrada From ope.V_Obtener_ResumenRegistroAsistencia WHERE IdPespecifico = @IdPespecifico AND Grupo = @Grupo ORDER BY FechaHoraInicio ASC";
                var res = _dapper.QueryDapper(query, new {idPEspecifico, grupo});

                listado = JsonConvert.DeserializeObject<List<ResumenRegistroAsistenciaDTO>>(res);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Listar los grupos de PEspecifico
        public List<PEspecificoSesionGruposDTO> ListarGruposPEspecifico(int idPEspecifico)
        {
            try
            {
                List<PEspecificoSesionGruposDTO> listado = new List<PEspecificoSesionGruposDTO>();
                var query = "SELECT IdPespecifico,Grupo,GrupoCursos From [pla].[V_PEspecificoIdSesionGrupo] WHERE IdPespecifico = @IdPespecifico";
                var res = _dapper.QueryDapper(query, new { idPEspecifico});
                listado = JsonConvert.DeserializeObject<List<PEspecificoSesionGruposDTO>>(res);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Lista los horarios de los cursos de Webex
        /// </summary>
        /// <param name="id">Id del pespecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Cadena con la lista de cursos de Webex</returns>
        public string ObtenerHorarioSemanaSesionWebex(int idPespecifico)
        {
            try
            {
                List<PEspecificoProximaSesionWebexDTO> listado = new List<PEspecificoProximaSesionWebexDTO>();
                string htmlFinal = string.Empty;
                var query = "pla.SP_ObtenerSesionProximoPorPespecifico";
                var res = _dapper.QuerySPDapper(query, new { idPespecifico });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<PEspecificoProximaSesionWebexDTO>>(res);
                    listado = listado.OrderBy(w => w.FechaInicio).ToList();
                    htmlFinal += $@"";
                    DateTime? ultimaFecha = null;
                    foreach (var item in listado )
                    {
                        if (ultimaFecha == null)
                        {
                            ultimaFecha = item.FechaInicio;
                        }
                        if ((ultimaFecha.Value.Date - item.FechaInicio.Date).Days <2)
                        {
                            ultimaFecha= item.FechaInicio;
                            htmlFinal += $@"<strong>{item.NombreDia} { item.FechaInicio.ToString("dd/MM/yyyy") }</strong>
                                        <ul>
                                        
                                          <li><strong>Hora de inicio:</strong> { item.FechaInicio.ToString("hh:mm tt") }</li>                                         
                                          <li><strong>Hora de término:</strong> { item.FechaFin.ToString("hh:mm tt") }</li>
                                     </ul>
                                     ";
                        }
                        
                    }
                    htmlFinal += $@"";
                }
                
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ActualizarModalidadSesion(int IdPespecifico, int Grupo, int IdModalidadCurso,string Usuario)
        {
            var query = "pla.SP_ActualizarModalidadPespecificoSesion";
            var res = _dapper.QuerySPDapper(query, new { IdPespecifico,Grupo,IdModalidadCurso,Usuario });
            if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Obtiene los registros de Fecha, hora y estado sesion (Modulo Informacion Webinar)
        /// </summary>
        /// <param name="filtro">Objeto de clase WebinarReporteFiltroDTO</param>
        /// <returns>Lista de objetos de clase WebinarDDetalleSesionDTO</returns>
        public List<WebinarDDetalleSesionDTO> ObtenerInformacionSesionesWebinarGrid(WebinarReporteFiltroDTO filtro)
        {
            try
            {
                var filtros = new
                {
                    ListaPGeneral = filtro.ListaPGeneral == null ? "" : string.Join(",", filtro.ListaPGeneral.Select(x => x)),
                    ListaPEspecifico = filtro.ListaPEspecifico == null ? "" : string.Join(",", filtro.ListaPEspecifico.Select(x => x)),
                    EstadoSesion = filtro.EstadoSesion,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    FechaPorDefecto = filtro.FechaPorDefecto,
                    CodigoMatricula =filtro.CodigoMatricula
                };
                List<WebinarDDetalleSesionDTO> webinarSesiones = new List<WebinarDDetalleSesionDTO>();
                string query = string.Empty;
                query = "pla.SP_ObtenerInformacionSesionesWebinar";
                var WebinarDB = _dapper.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(WebinarDB) && !WebinarDB.Contains("[]"))
                {
                    webinarSesiones = JsonConvert.DeserializeObject<List<WebinarDDetalleSesionDTO>>(WebinarDB);
                }
                return webinarSesiones;

                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los registros de Fecha, hora y estado sesion (Modulo Informacion Webinar)
        /// </summary>
        /// <param name="filtro">Objeto de clase WebinarReporteFiltroDTO</param>
        /// <returns>Lista de objetos de clase DatosListaControlCursosDTO</returns>
        public List<DatosListaControlCursosDTO> GenerarReporteControlCursos(ControlCursosFiltroDTO filtro)
        {
            try
            {
                var filtros = new
                {
                    IdPGeneral = filtro.IdPGeneral,
                    IdProgramaEspecifico = filtro.IdProgramaEspecifico,
                    IdCentroCosto = filtro.IdCentroCosto,
                    IdEstadoPEspecifico = filtro.IdEstadoPEspecifico,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin            
                };
                List<DatosListaControlCursosDTO> controlCursos = new List<DatosListaControlCursosDTO>();
                string query = string.Empty;
                query = "pla.SP_CursosFinalizadosFiltro";
                var controlCursosDB = _dapper.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(controlCursosDB) && !controlCursosDB.Contains("[]"))
                {
                    controlCursos = JsonConvert.DeserializeObject<List<DatosListaControlCursosDTO>>(controlCursosDB);
                }
                return controlCursos;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<SesionesOnlineWebinarDocenteDTO> ObtenerSesionesOnlineWebinarPorProveedor(int idProveedor)
        {
            try
            {
                List<SesionesOnlineWebinarDocenteDTO> listado = new List<SesionesOnlineWebinarDocenteDTO>();
                string _query = "SELECT IdPGeneral, PGeneral, IdPEspecificoPadre,PEspecificoPadre,IdPEspecificoHijo,CursoNombre,IdProveedor,FechaSesion,HoraSesion,Tipo,UrlWebex FROM pla.V_ObtenerSesionesOnlineWebinarDocente WHERE IdProveedor = @idProveedor ORDER BY FechaSesion ASC";
                var query = _dapper.QueryDapper(_query, new { idProveedor });
                listado = JsonConvert.DeserializeObject<List<SesionesOnlineWebinarDocenteDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Se obtiene la lista de sesiones por el idPespecifico
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<PEspecificoSesionRecuperacionDTO> ObtenerSesionesPorPEspecifico(int idPespecifico,int idMatriculaCabecera)
        {
            try
            {
                List<PEspecificoSesionRecuperacionDTO> obtenerSesionPorPEspecifico = new List<PEspecificoSesionRecuperacionDTO>();
                var obtenerSesionPorPEspecificoDB = _dapper.QuerySPDapper("[pla].[SP_ObtenerSesionesPorPEspecifico]", new { idPespecifico = idPespecifico, idMatriculaCabecera= idMatriculaCabecera });
                if (!string.IsNullOrEmpty(obtenerSesionPorPEspecificoDB) && !obtenerSesionPorPEspecificoDB.Contains("[]"))
                {
                    obtenerSesionPorPEspecifico = JsonConvert.DeserializeObject<List<PEspecificoSesionRecuperacionDTO>>(obtenerSesionPorPEspecificoDB);
                }
                return obtenerSesionPorPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EsWebinarPasado(int idPEspecificoSesion)
        {
            try
            {
                return this.Exist(x => x.Id == idPEspecificoSesion && x.FechaHoraInicio < DateTime.Now);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
