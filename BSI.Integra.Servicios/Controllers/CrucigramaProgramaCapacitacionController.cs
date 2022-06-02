using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using System.Transactions;
using BSI.Integra.Aplicacion.Planificacion.BO;
using Google.Api.Ads.Common.Util;
using CsvHelper;
using System.IO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: Operaciones/CrucigramaProgramaCapacitacion
    /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
    /// Fecha: 20/02/2021
    /// <summary>
    /// Gestiona las acciones con los crucigramas de un programa capacitacion
    /// </summary>
    [Route("api/CrucigramaProgramaCapacitacion")]
    [ApiController]
    public class CrucigramaProgramaCapacitacionController : ControllerBase
    {
		private readonly integraDBContext _integraDBContext;
		private readonly CrucigramaProgramaCapacitacionRepositorio _repCrucigramaProgramaCapacitacion;
		private readonly CrucigramaProgramaCapacitacionDetalleRepositorio _repCrucigramaProgramaCapacitacionDetalle;
        private readonly TipoDocumentoAlumnoRepositorio _repTipoDocumentoAlumno;
        private readonly TipoDocumentoAlumnoModalidadCursoRepositorio _repTipoDocumentoAlumnoModalidadCurso;
        private readonly TipoDocumentoAlumnoPGeneralRepositorio _repTipoDocumentoAlumnoPGeneral;
        private readonly TipoDocumentoAlumnoEstadoMatriculaRepositorio _repTipoDocumentoAlumnoEstadoMatricula;
        private readonly TipoDocumentoAlumnoSubEstadoMatriculaRepositorio _repTipoDocumentoAlumnoSubEstadoMatricula;
        private readonly PgeneralRepositorio _repPGeneral;
		private readonly ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio;
		private readonly TipoMarcadorRepositorio _repTipoMarcador;
		private readonly PespecificoRepositorio _repPEspecifico;
		public CrucigramaProgramaCapacitacionController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
			_repCrucigramaProgramaCapacitacion = new CrucigramaProgramaCapacitacionRepositorio(_integraDBContext);
			_repCrucigramaProgramaCapacitacionDetalle = new CrucigramaProgramaCapacitacionDetalleRepositorio(_integraDBContext);
            _repTipoDocumentoAlumno = new TipoDocumentoAlumnoRepositorio(_integraDBContext);
            _repTipoDocumentoAlumnoModalidadCurso = new TipoDocumentoAlumnoModalidadCursoRepositorio(_integraDBContext);
            _repTipoDocumentoAlumnoPGeneral = new TipoDocumentoAlumnoPGeneralRepositorio(_integraDBContext);
            _repTipoDocumentoAlumnoEstadoMatricula = new TipoDocumentoAlumnoEstadoMatriculaRepositorio(_integraDBContext);
            _repTipoDocumentoAlumnoSubEstadoMatricula = new TipoDocumentoAlumnoSubEstadoMatriculaRepositorio(_integraDBContext);
            _repPGeneral = new PgeneralRepositorio(_integraDBContext);
			_configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio(_integraDBContext);
			_repTipoMarcador = new TipoMarcadorRepositorio(_integraDBContext);
			_repPEspecifico = new PespecificoRepositorio(_integraDBContext);
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Obtiene los combos necesarios para el funcionamiento del modulo
        /// </summary>
        /// <returns>Objeto anonimo { PGeneral, TipoMarcador, ProgramaEspecifico }</returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCombos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var pgeneral = _repPGeneral.ObtenerProgramasFiltro();
				var tipoMarcador = _repTipoMarcador.ObtenerTipoMarcador();
				var pespecifico = _repPEspecifico.ObtenerPEspecificoProgramaGeneral();
				return Ok(new { PGeneral = pgeneral, TipoMarcador = tipoMarcador, ProgramaEspecifico = pespecifico });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Obtiene los crucigramas
        /// </summary>
        /// <returns>Lista de objetos (CrucigramaProgramaCapacitacionDTO) con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCrucigramas()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var crucigramas = _repCrucigramaProgramaCapacitacion.ObtenerCrucigramasRegistrados();
				return Ok(crucigramas);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Obtiene las respuestas de un crucigrama segun un id especifico
        /// </summary>
		/// <param name="IdPreguntaProgramaCapacitacion">Id de la pregunta de programa capacitacion(PK de la tabla pla.T_CrucigramaProgramaCapacitacion)</param>
        /// <returns>Lista de objetos (CrucigramaProgramaCapacitacionDetalleDTO) con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerRespuestasCrucigrama([FromBody]int IdCrucigramaProgramaCapacitacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<CrucigramaProgramaCapacitacionDetalleDTO> res;
				if (IdCrucigramaProgramaCapacitacion > 0)
				{
					res = _repCrucigramaProgramaCapacitacionDetalle.ObtenerRespuestaCrucigramaProgramaCapacitacion(IdCrucigramaProgramaCapacitacion);
					return Ok(res);
				}
				else
				{
					res = new List<CrucigramaProgramaCapacitacionDetalleDTO>();
					return Ok(res);
				}

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]/{IdCrucigramaProgramaCapacitacion}")]
		[HttpPost]
		public ActionResult ObtenerCrucigramaTiempoSesion(int IdCrucigramaProgramaCapacitacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<CrucigramaProgramaCapacitacionDetalleDTO> res;
				CrucigramaProgramaCapacitacionSesion crucigramaSesion = new CrucigramaProgramaCapacitacionSesion();
				List<CrucigramaProgramaCapacitacionSesionDetalle> crucigramaSesionDetalle = new List<CrucigramaProgramaCapacitacionSesionDetalle>();
				if (IdCrucigramaProgramaCapacitacion > 0)
				{
					var _crucigrama = _repCrucigramaProgramaCapacitacion.FirstById(IdCrucigramaProgramaCapacitacion);

					crucigramaSesion.IdPGeneral = _crucigrama.IdPgeneral;
					crucigramaSesion.OrdenFilaCapitulo = _crucigrama.OrdenFilaCapitulo.Value;
					crucigramaSesion.OrdenFilaSesion = _crucigrama.OrdenFilaSesion.Value;
					crucigramaSesion.CodigoCrucigrama = _crucigrama.CodigoCrucigrama;

					res = _repCrucigramaProgramaCapacitacionDetalle.ObtenerRespuestaCrucigramaProgramaCapacitacion(IdCrucigramaProgramaCapacitacion);

                    if (res != null && res.Count > 0)
                    {
                        foreach (var item in res)
                        {
							int[] _posicion = {item.ColumnaInicio,item.FilaInicio };
							CrucigramaProgramaCapacitacionSesionDetalle _registro = new CrucigramaProgramaCapacitacionSesionDetalle() { 
								pos = _posicion,
								sentido=item.Tipo,
								palabra=item.Palabra,
								pista=item.Definicion
							};
							crucigramaSesionDetalle.Add(_registro);
						}

						crucigramaSesion.CrucigramaDetalle = crucigramaSesionDetalle;

					}

					return Ok(crucigramaSesion);
				}
				else
				{
					res = new List<CrucigramaProgramaCapacitacionDetalleDTO>();
					return Ok(crucigramaSesion);
				}

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]/{IdPGeneral}/{IdCapitulo}/{IdSesion}")]
		[HttpPost]
		public ActionResult ObtenerCrucigramaProgramaCapacitacionSesion(int IdPGeneral, int IdCapitulo, int IdSesion)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<CrucigramaProgramaCapacitacionDetalleDTO> res;
				CrucigramaProgramaCapacitacionSesion crucigramaSesion = new CrucigramaProgramaCapacitacionSesion();
				List<CrucigramaProgramaCapacitacionSesionDetalle> crucigramaSesionDetalle = new List<CrucigramaProgramaCapacitacionSesionDetalle>();
				if (IdPGeneral > 0 && IdCapitulo > 0 && IdSesion > 0)
				{
					var _crucigrama = _repCrucigramaProgramaCapacitacion.ObtenerCrucigramaProgramaCapacitacionSesion(IdPGeneral, IdCapitulo, IdSesion);

					crucigramaSesion.Id = _crucigrama.Id;
					crucigramaSesion.IdPGeneral = _crucigrama.IdPgeneral;
					crucigramaSesion.OrdenFilaCapitulo = _crucigrama.OrdenFilaCapitulo.Value;
					crucigramaSesion.OrdenFilaSesion = _crucigrama.OrdenFilaSesion.Value;
					crucigramaSesion.CodigoCrucigrama = _crucigrama.CodigoCrucigrama;

					res = _repCrucigramaProgramaCapacitacionDetalle.ObtenerRespuestaCrucigramaProgramaCapacitacion(_crucigrama.Id);

					if (res != null && res.Count > 0)
					{
						foreach (var item in res)
						{
							int[] _posicion = { item.ColumnaInicio, item.FilaInicio };
							CrucigramaProgramaCapacitacionSesionDetalle _registro = new CrucigramaProgramaCapacitacionSesionDetalle()
							{
								pos = _posicion,
								sentido = item.Tipo,
								palabra = item.Palabra,
								pista = item.Definicion
							};
							crucigramaSesionDetalle.Add(_registro);
						}

						crucigramaSesion.CrucigramaDetalle = crucigramaSesionDetalle;

					}

					return Ok(crucigramaSesion);
				}
				else
				{
					res = new List<CrucigramaProgramaCapacitacionDetalleDTO>();
					return Ok(crucigramaSesion);
				}

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Inserta un crucigrama especifico
        /// </summary>
		/// <param name="Filtro">Objeto de tipo CompuestoCrucigramaProgramaCapacitacionDTO</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarCrucigrama([FromBody]CompuestoCrucigramaProgramaCapacitacionDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					CrucigramaProgramaCapacitacionBO crucigrama = new CrucigramaProgramaCapacitacionBO()
					{

						IdPgeneral = Filtro.Crucigrama.IdPGeneral,
						IdPespecifico = Filtro.Crucigrama.IdPEspecifico,
						OrdenFilaCapitulo = Filtro.Crucigrama.IdCapitulo,
						OrdenFilaSesion = Filtro.Crucigrama.IdSesion,
						IdTipoMarcador = Filtro.Crucigrama.IdTipoMarcador,
						ValorMarcador = Filtro.Crucigrama.ValorMarcador,
						CodigoCrucigrama = Filtro.Crucigrama.CodigoCrucigrama,
						CantidadFila = Filtro.Crucigrama.CantidadFila,
						CantidadColumna = Filtro.Crucigrama.CantidadColumna,
						Estado = true,
						UsuarioCreacion = Filtro.Usuario,
						UsuarioModificacion = Filtro.Usuario,
						FechaCreacion = DateTime.Now,
						FechaModificacion = DateTime.Now
					};
					_repCrucigramaProgramaCapacitacion.Insert(crucigrama);

					foreach (var item in Filtro.CrucigramaDetalle)
					{

						CrucigramaProgramaCapacitacionDetalleBO crucigramaDetalle = new CrucigramaProgramaCapacitacionDetalleBO()
						{
							IdCrucigramaProgramaCapacitacionDetalle = crucigrama.Id,
							ColumnaInicio = item.ColumnaInicio,
							FilaInicio = item.FilaInicio,
							NumeroPalabra = item.NumeroPalabra,
							Palabra = item.Palabra,
							Tipo = item.Tipo,
							Definicion = item.Definicion,
							Estado = true,
							UsuarioCreacion = Filtro.Usuario,
							UsuarioModificacion = Filtro.Usuario,
							FechaCreacion = DateTime.Now,
							FechaModificacion = DateTime.Now,
							
						};
						_repCrucigramaProgramaCapacitacionDetalle.Insert(crucigramaDetalle);
					}
					scope.Complete();
				}

				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Actualiza un crucigrama en especifico
        /// </summary>
		/// <param name="Filtro">Objeto de tipo CompuestoCrucigramaProgramaCapacitacionDTO</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarCrucigrama([FromBody]CompuestoCrucigramaProgramaCapacitacionDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					var crucigrama = _repCrucigramaProgramaCapacitacion.FirstById(Filtro.Crucigrama.Id);

					crucigrama.IdPgeneral = Filtro.Crucigrama.IdPGeneral;
					crucigrama.IdPespecifico = Filtro.Crucigrama.IdPEspecifico;
					crucigrama.OrdenFilaCapitulo = Filtro.Crucigrama.IdCapitulo;
					crucigrama.OrdenFilaSesion = Filtro.Crucigrama.IdSesion;
					crucigrama.IdTipoMarcador = Filtro.Crucigrama.IdTipoMarcador;
					crucigrama.ValorMarcador = Filtro.Crucigrama.ValorMarcador;
					crucigrama.CodigoCrucigrama = Filtro.Crucigrama.CodigoCrucigrama;
					crucigrama.CantidadFila = Filtro.Crucigrama.CantidadFila;
					crucigrama.CantidadColumna = Filtro.Crucigrama.CantidadColumna;
					crucigrama.UsuarioModificacion = Filtro.Usuario;
					crucigrama.FechaModificacion = DateTime.Now;


					_repCrucigramaProgramaCapacitacion.Update(crucigrama);

					var rp = _repCrucigramaProgramaCapacitacionDetalle.GetBy(x => x.IdCrucigramaProgramaCapacitacionDetalle == crucigrama.Id);
					foreach (var item in rp)
					{
						if (!Filtro.CrucigramaDetalle.Any(x => x.Id == item.Id))
						{
							_repCrucigramaProgramaCapacitacionDetalle.Delete(item.Id, Filtro.Usuario);
						}
					}

					foreach (var item in Filtro.CrucigramaDetalle)
					{
						CrucigramaProgramaCapacitacionDetalleBO crucigramaDetalle;
						if (item.Id > 0)
						{
							crucigramaDetalle = _repCrucigramaProgramaCapacitacionDetalle.FirstById(item.Id);
							crucigramaDetalle.IdCrucigramaProgramaCapacitacionDetalle = crucigrama.Id;
							crucigramaDetalle.ColumnaInicio = item.ColumnaInicio;
							crucigramaDetalle.FilaInicio = item.FilaInicio;
							crucigramaDetalle.NumeroPalabra = item.NumeroPalabra;
							crucigramaDetalle.Palabra = item.Palabra;
							crucigramaDetalle.Tipo = item.Tipo;
							crucigramaDetalle.Definicion = item.Definicion;
							crucigramaDetalle.UsuarioModificacion = Filtro.Usuario;
							crucigramaDetalle.FechaModificacion = DateTime.Now;

							_repCrucigramaProgramaCapacitacionDetalle.Update(crucigramaDetalle);
						}
						else
						{
							crucigramaDetalle = new CrucigramaProgramaCapacitacionDetalleBO()
							{
								IdCrucigramaProgramaCapacitacionDetalle = crucigrama.Id,
								ColumnaInicio = item.ColumnaInicio,
								FilaInicio = item.FilaInicio,
								NumeroPalabra = item.NumeroPalabra,
								Palabra = item.Palabra,
								Tipo = item.Tipo,
								Definicion = item.Definicion,
								Estado = true,
								UsuarioCreacion = Filtro.Usuario,
								UsuarioModificacion = Filtro.Usuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now,
							};
							_repCrucigramaProgramaCapacitacionDetalle.Insert(crucigramaDetalle);
						}
					}
					scope.Complete();
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Elimina un crucigrama especifico
        /// </summary>
		/// <param name="Filtro">Objeto de tipo EliminarDTO</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarCrucigrama([FromBody]EliminarDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					var crucigrama = _repCrucigramaProgramaCapacitacion.FirstById(Filtro.Id);
					if (crucigrama != null)
					{
						var crucigramaDetalle = _repCrucigramaProgramaCapacitacionDetalle.GetBy(x => x.IdCrucigramaProgramaCapacitacionDetalle == crucigrama.Id);

						foreach (var item in crucigramaDetalle)
						{
							_repCrucigramaProgramaCapacitacionDetalle.Delete(item.Id, Filtro.NombreUsuario);
						}
						_repCrucigramaProgramaCapacitacion.Delete(crucigrama.Id, Filtro.NombreUsuario);
					}
					scope.Complete();
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Funcion para importar desde un archivo CSV a la base de datos
        /// </summary>
		/// <param name="Files">Objeto de tipo IFormFile con las preguntas</param>
        /// <returns>Booleano con respuesta 200 y la objeto anonimo con las propiedades({ Total, Correctos, Error, Errores }) o 400 con el mensaje de error</returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ImportarExcel([FromForm] IFormFile Files)
		{
			CsvFile file = new CsvFile();
			List<string> listaErrores = new List<string>();
			try
			{
				int indexError = 0;
				int indexTotal = 0;
				List<ImportacionCrucigramaProgramaCapacitacionDTO> listaExcel = new List<ImportacionCrucigramaProgramaCapacitacionDTO>();
				StreamReader sw = new StreamReader(Files.OpenReadStream(), System.Text.Encoding.GetEncoding("iso-8859-1"));
				using (var cvs = new CsvReader(sw))
				{
					cvs.Configuration.Delimiter = ";";
					cvs.Configuration.MissingFieldFound = null;
					cvs.Read();
					cvs.ReadHeader();
					while (cvs.Read())
					{
						try
						{
							using (TransactionScope scope = new TransactionScope())
							{
								ImportacionCrucigramaProgramaCapacitacionDTO crucigramaProgramaCapacitacion = new ImportacionCrucigramaProgramaCapacitacionDTO()
								{
									IdPGeneral = cvs.GetField<int>("IdPGeneral"),
									IdPEspecifico = cvs.GetField<int>("IdPEspecifico"),
									OrdenFilaCapitulo = cvs.GetField<int?>("OrdenFilaCapitulo"),
									Sesion = cvs.GetField<string>("Sesion"),
									SubSeccion = cvs.GetField<string>("Subsesion"),
									IdTipoMarcador = cvs.GetField<int>("IdTipoMarcador"),
									ValorMarcador = cvs.GetField<decimal>("ValorMarcador"),
									CodigoCrucigrama = cvs.GetField<string>("CodigoCrucigrama"),
									CantidadFila = cvs.GetField<int>("CantidadFila"),
									CantidadColumna = cvs.GetField<int>("CantidadColumna"),

									//============RESPUESTAS=============================
									ColumnaInicio = cvs.GetField<int>("ColumnaInicio"),
									FilaInicio = cvs.GetField<int>("FilaInicio"),
									Definicion = cvs.GetField<string>("Definicion"),
									Palabra = cvs.GetField<string>("Palabra"),
									NumeroPalabra = cvs.GetField<int>("NumeroPalabra"),
									Tipo = cvs.GetField<int>("Tipo")
								};
								listaExcel.Add(crucigramaProgramaCapacitacion);
								scope.Complete();
							}
						}
						catch (Exception e)
						{
							//indexError++;
							//listaErrores.Add("Error en: " + cvs.GetField<string>("EnunciadoPregunta") + " - " + e.Message);
						}

					}
				}
				var agrupado = listaExcel.GroupBy(x => new
				{
					x.IdPGeneral,
					x.IdPEspecifico,
					x.OrdenFilaCapitulo,
					x.Sesion,
					x.SubSeccion,
					x.IdTipoMarcador,
					x.ValorMarcador,
					x.CodigoCrucigrama,
					x.CantidadFila,
					x.CantidadColumna
				}).Select(x => new CrucigramaProgramaCapacitacionExcelCompuestoDTO
				{
					CrucigramaProgramaCapacitacion = new CrucigramaProgramaCapacitacionAgrupadoDTO
					{
						IdPGeneral = x.Key.IdPGeneral,
						IdPEspecifico = x.Key.IdPEspecifico,
						OrdenFilaCapitulo = x.Key.OrdenFilaCapitulo,
						Sesion = x.Key.Sesion,
						SubSeccion = x.Key.SubSeccion,
						IdTipoMarcador = x.Key.IdTipoMarcador,
						ValorMarcador = x.Key.ValorMarcador,
						CodigoCrucigrama = x.Key.CodigoCrucigrama,
						CantidadFila = x.Key.CantidadFila,
						CantidadColumna = x.Key.CantidadColumna,
						CrucigramaDetalleProgramaCapacitacion = x.GroupBy(z => new { z.Tipo, z.NumeroPalabra, z.Palabra, z.Definicion, z.ColumnaInicio, z.FilaInicio }).Select(z => new CrucigramaDetalleProgramaCapacitacionAgrupadoDTO
						{
							Tipo = z.Key.Tipo,
							NumeroPalabra = z.Key.NumeroPalabra,
							Palabra = z.Key.Palabra,
							Definicion = z.Key.Definicion,
							ColumnaInicio = z.Key.ColumnaInicio,
							FilaInicio = z.Key.FilaInicio,
						}).ToList()
					}
				}).ToList();

				foreach (var item in agrupado)
				{
					var listaCompuesta = ObtenerCapituloSesionProgramaCapacitacion(item.CrucigramaProgramaCapacitacion.IdPGeneral);
					var listaCapitulos = listaCompuesta.Where(x => x.IdCapituloProgramaCapacitacion == item.CrucigramaProgramaCapacitacion.OrdenFilaCapitulo).FirstOrDefault();
					int? ordenFilaSesion = null;
					if(listaCapitulos != null)
					{
						var sesion = listaCapitulos.ListaSesionesProgramaCapacitacion.Where(x => x.SesionProgramaCapacitacion.ToLower().Equals(item.CrucigramaProgramaCapacitacion.Sesion.ToLower())).FirstOrDefault();

						int idSesionTemporal = 0;

						if (sesion != null)
						{
							idSesionTemporal = sesion.IdSesionProgramaCapacitacion;
						}

						if (!string.IsNullOrEmpty(item.CrucigramaProgramaCapacitacion.SubSeccion))
                        {
							try
							{
								idSesionTemporal = listaCapitulos.ListaSesionesProgramaCapacitacion.Select(x => x.ListaSubSeccionProgramaCapacitacion.FirstOrDefault(y => y.SubSeccionProgramaCapacitacion.ToLower().Equals(item.CrucigramaProgramaCapacitacion.SubSeccion.ToLower())).IdSesionProgramaCapacitacion).FirstOrDefault();
							}
							catch(Exception e)
                            {
								idSesionTemporal = sesion.IdSesionProgramaCapacitacion;
                            }
                        }

						if (idSesionTemporal > 0)
						{
							ordenFilaSesion = idSesionTemporal;
						}
					}
					try
					{
						indexTotal++;
						using (TransactionScope scope = new TransactionScope())
						{
							CrucigramaProgramaCapacitacionBO crucigrama = new CrucigramaProgramaCapacitacionBO()
							{
								IdPgeneral = item.CrucigramaProgramaCapacitacion.IdPGeneral,
								IdPespecifico = item.CrucigramaProgramaCapacitacion.IdPEspecifico,
								OrdenFilaCapitulo = item.CrucigramaProgramaCapacitacion.OrdenFilaCapitulo,
								OrdenFilaSesion = ordenFilaSesion,
								IdTipoMarcador = item.CrucigramaProgramaCapacitacion.IdTipoMarcador,
								ValorMarcador = item.CrucigramaProgramaCapacitacion.ValorMarcador,
								CodigoCrucigrama = item.CrucigramaProgramaCapacitacion.CodigoCrucigrama,
								CantidadFila = item.CrucigramaProgramaCapacitacion.CantidadFila,
								CantidadColumna = item.CrucigramaProgramaCapacitacion.CantidadColumna,
								Estado = true,
								UsuarioCreacion = "ImportarExcel",
								UsuarioModificacion = "ImportarExcel",
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now
							};
							_repCrucigramaProgramaCapacitacion.Insert(crucigrama);

							foreach (var respuesta in item.CrucigramaProgramaCapacitacion.CrucigramaDetalleProgramaCapacitacion)
							{
								CrucigramaProgramaCapacitacionDetalleBO crucigramaDetalle = new CrucigramaProgramaCapacitacionDetalleBO()
								{
									IdCrucigramaProgramaCapacitacionDetalle = crucigrama.Id,
									ColumnaInicio = respuesta.ColumnaInicio,
									FilaInicio = respuesta.FilaInicio,
									NumeroPalabra = respuesta.NumeroPalabra,
									Palabra = respuesta.Palabra,
									Tipo = respuesta.Tipo,
									Definicion = respuesta.Definicion,
									Estado = true,
									UsuarioCreacion = "ImportarExcel",
									UsuarioModificacion = "ImportarExcel",
									FechaCreacion = DateTime.Now,
									FechaModificacion = DateTime.Now,
								};
								_repCrucigramaProgramaCapacitacionDetalle.Insert(crucigramaDetalle);
							}
							scope.Complete();
						}
					}
					catch (Exception e)
					{
						indexError++;
						listaErrores.Add("Error - " + e.Message);
					}
				}
				return Ok(new { Total = indexTotal, Correctos = (indexTotal - indexError), Error = indexError, Errores = listaErrores });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}
        [Route("[action]")]
        [HttpPost]
        public ActionResult SubidaArchivos([FromForm] IFormFile files)
        {
            CsvFile file = new CsvFile();
            List<string> listaErrores = new List<string>();
            try
            {
                int indexError = 0;
                int indexTotal = 0;
                List<SubidaArchivosDTO> ListaExcel = new List<SubidaArchivosDTO>();
                StreamReader sw = new StreamReader(files.OpenReadStream(), System.Text.Encoding.GetEncoding("iso-8859-1"));
                using (var cvs = new CsvReader(sw))
                {
                    cvs.Configuration.Delimiter = ";";
                    cvs.Configuration.MissingFieldFound = null;
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        try
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                SubidaArchivosDTO subidaArchivosTotal = new SubidaArchivosDTO()
                                {
                                    Nombre = cvs.GetField<string>("Nombre"),
                                    IdPlantillaFrontal = cvs.GetField<int>("IdPlantillaFrontal"),
                                    IdPlantillaPosterior = cvs.GetField<int>("IdPlantillaPosterior"),
                                    IdOperadorComparacion = cvs.GetField<int>("IdOperadorComparacion"),
                                    TieneDeuda = cvs.GetField<int>("TieneDeuda"),
                                    IdModalidad = cvs.GetField<string>("IdModalidad"),
                                    ProgramasGenerales = cvs.GetField<string>("ProgramasGenerales"),
                                    Estados = cvs.GetField<string>("Estados"),
                                    SubEstados = cvs.GetField<string>("SubEstados")
                                    
                                };
                                ListaExcel.Add(subidaArchivosTotal);
                                scope.Complete();
                            }
                        }
                        catch (Exception e)
                        {
                            //indexError++;
                            //listaErrores.Add("Error en: " + cvs.GetField<string>("EnunciadoPregunta") + " - " + e.Message);
                        }

                    }
                }

                foreach (var item in ListaExcel)
                {
                    var listaModalidad = item.IdModalidad.Split(";");
                    var listaProgramaGeneral = item.ProgramasGenerales.Split(";");
                    var listaEstados = item.Estados.Split(";");
                    var listaSubEstados = item.SubEstados.Split(";");

                    try
                    {
                        var objeto = _repTipoDocumentoAlumno.FirstBy(w => w.Nombre == item.Nombre);
                        foreach (var programa in listaProgramaGeneral)
                        {
                            //indexTotal++;
                            using (TransactionScope scope = new TransactionScope())
                            {
                                //TipoDocumentoAlumnoBO objeto = new TipoDocumentoAlumnoBO()
                                //{
                                //    Nombre = item.Nombre,
                                //    IdPlantillaFrontal = item.IdPlantillaFrontal,
                                //    IdPlantillaPosterior = item.IdPlantillaPosterior,
                                //    IdOperadorComparacion = item.IdOperadorComparacion,
                                //    TieneDeuda = item.TieneDeuda == 1 ? true : false,
                                //    Estado = true,
                                //    UsuarioCreacion = "ImportarExcel",
                                //    UsuarioModificacion = "ImportarExcel",
                                //    FechaCreacion = DateTime.Now,
                                //    FechaModificacion = DateTime.Now
                                //};
                                //var id=_repTipoDocumentoAlumno.Insert(objeto);

                                //foreach (var modalidad in listaModalidad)
                                //{
                                //    try
                                //    {
                                //        TipoDocumentoAlumnoModalidadCursoBO modalidaDetalle = new TipoDocumentoAlumnoModalidadCursoBO()
                                //        {
                                //            IdTipoDocumentoAlumno = objeto.Id,
                                //            IdModalidad = Convert.ToInt32(modalidad),
                                //            Estado = true,
                                //            UsuarioCreacion = "ImportarExcel",
                                //            UsuarioModificacion = "ImportarExcel",
                                //            FechaCreacion = DateTime.Now,
                                //            FechaModificacion = DateTime.Now,
                                //        };
                                //        _repTipoDocumentoAlumnoModalidadCurso.Insert(modalidaDetalle);
                                //    }
                                //    catch (Exception ex1)
                                //    {

                                //    }
                                //}

                                


                                try
                                {
                                    if (_repTipoDocumentoAlumnoPGeneral.FirstBy(w => w.IdPgeneral == Convert.ToInt32(programa) && w.IdTipoDocumentoAlumno==objeto.Id) == null)
                                    {
                                        TipoDocumentoAlumnoPGeneralBO programaDetalle = new TipoDocumentoAlumnoPGeneralBO()
                                        {
                                            IdTipoDocumentoAlumno = objeto.Id,
                                            IdPgeneral = Convert.ToInt32(programa),
                                            Estado = true,
                                            UsuarioCreacion = "ImportarExcel",
                                            UsuarioModificacion = "ImportarExcel",
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                        };
                                        _repTipoDocumentoAlumnoPGeneral.Insert(programaDetalle);
                                    }


                                }
                                catch (Exception ex2)
                                {

                                }


                                //foreach (var estado in listaEstados)
                                //{
                                //    try
                                //    {
                                //        TipoDocumentoAlumnoEstadoMatriculaBO estadoDetalle = new TipoDocumentoAlumnoEstadoMatriculaBO()
                                //        {
                                //            IdTipoDocumentoAlumno = objeto.Id,
                                //            IdEstadoMatricula = Convert.ToInt32(estado),
                                //            Estado = true,
                                //            UsuarioCreacion = "ImportarExcel",
                                //            UsuarioModificacion = "ImportarExcel",
                                //            FechaCreacion = DateTime.Now,
                                //            FechaModificacion = DateTime.Now,
                                //        };
                                //        _repTipoDocumentoAlumnoEstadoMatricula.Insert(estadoDetalle);
                                //    }
                                //    catch (Exception ex3)
                                //    {

                                //    }
                                //}

                                //foreach (var subestado in listaSubEstados)
                                //{
                                //    try
                                //    {
                                //        TipoDocumentoAlumnoSubEstadoMatriculaBO subestadoDetalle = new TipoDocumentoAlumnoSubEstadoMatriculaBO()
                                //        {
                                //            IdTipoDocumentoAlumno = objeto.Id,
                                //            IdSubEstadoMatricula = Convert.ToInt32(subestado),
                                //            Estado = true,
                                //            UsuarioCreacion = "ImportarExcel",
                                //            UsuarioModificacion = "ImportarExcel",
                                //            FechaCreacion = DateTime.Now,
                                //            FechaModificacion = DateTime.Now,
                                //        };
                                //        _repTipoDocumentoAlumnoSubEstadoMatricula.Insert(subestadoDetalle);
                                //    }
                                //    catch (Exception ex4)
                                //    {

                                //    }
                                //}
                                scope.Complete();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        indexError++;
                        listaErrores.Add("Error - " + e.Message);
                    }

                }
                return Ok(new { Total = indexTotal, Correctos = (indexTotal - indexError), Error = indexError, Errores = listaErrores });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

		/// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Obtiene los capitulos y sesiones para los programas capacitacciones
        /// </summary>
		/// <param name="IdPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Lista de objeto de tipo CapituloSesionProgramaCapacitacionDTO</returns>
        private List<CapituloSesionProgramaCapacitacionDTO> ObtenerCapituloSesionProgramaCapacitacion(int IdPGeneral)
		{
			try
			{
				List<CapituloSesionProgramaCapacitacionDTO> listaRegistro;
				if (IdPGeneral == -1)
				{
					listaRegistro = new List<CapituloSesionProgramaCapacitacionDTO>();
				}
				else
				{
					var respuesta = _configurarVideoProgramaRepositorio.ObtenerPreConfigurarVideoPrograma(IdPGeneral);
					var listadoEstructura = (from x in respuesta
											  group x by x.NumeroFila into newGroup
											  select newGroup).ToList();


					List<EstructuraCapituloProgramaBO> lista = new List<EstructuraCapituloProgramaBO>();

					foreach (var item in listadoEstructura)
					{
						EstructuraCapituloProgramaBO objeto = new EstructuraCapituloProgramaBO();
						objeto.OrdenFila = item.Key;
						foreach (var itemRegistros in item)
						{
							switch (itemRegistros.NombreTitulo)
							{
								case "Capitulo":
									objeto.Nombre = itemRegistros.Nombre;
									objeto.Capitulo = itemRegistros.Contenido;
									objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
									objeto.IdPgeneral = itemRegistros.IdPgeneral;
									break;
								case "Sesion":
									objeto.Sesion = itemRegistros.Contenido;
									objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
									objeto.IdDocumentoSeccionPw = itemRegistros.Id;
									break;
								case "SubSeccion":
									objeto.SubSesion = itemRegistros.Contenido;
									objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
									if (!string.IsNullOrEmpty(objeto.SubSesion))
									{
										objeto.IdDocumentoSeccionPw = itemRegistros.Id;
									}
									break;
								default:
									objeto.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
									objeto.TotalSegundos = itemRegistros.TotalSegundos;
									break;
							}
						}
						lista.Add(objeto);
					}

					var rpta = lista.OrderBy(x => x.OrdenFila).ToList();

					var listas = rpta.GroupBy(x => new { x.IdPgeneral, x.Nombre, x.Capitulo, x.OrdenCapitulo });

					listaRegistro = new List<CapituloSesionProgramaCapacitacionDTO>();

					foreach (var capitulo in listas)
					{
						CapituloSesionProgramaCapacitacionDTO registro = new CapituloSesionProgramaCapacitacionDTO();
						registro.IdPGeneral = capitulo.Key.IdPgeneral;
						registro.CapituloProgramaCapacitacion = capitulo.Key.Capitulo;
						registro.IdCapituloProgramaCapacitacion = capitulo.Key.OrdenCapitulo;

						registro.ListaSesionesProgramaCapacitacion = new List<SesionSubSeccionProgramaCapacitacionDTO>();

						registro.ListaSesionesProgramaCapacitacion = capitulo.GroupBy(x => x.Sesion).Select(x => new SesionSubSeccionProgramaCapacitacionDTO {
							SesionProgramaCapacitacion = x.Key,
							IdSesionProgramaCapacitacion = capitulo.GroupBy(z => new { z.OrdenFila, z.Sesion, z.SubSesion }).Where(z => z.Key.Sesion == x.Key).FirstOrDefault().Key.OrdenFila,
							ListaSubSeccionProgramaCapacitacion = capitulo.GroupBy(y => new { y.OrdenFila, y.Sesion, y.SubSesion }).Where(y => y.Key.Sesion == x.Key).Select(y => new SubSeccionProgramaCapacitacionDTO { 
								IdSesionProgramaCapacitacion = y.Key.OrdenFila,
								SubSeccionProgramaCapacitacion = y.Key.SubSesion
							}).ToList()
						}).ToList();

						listaRegistro.Add(registro);
					}
				}
				return listaRegistro;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
