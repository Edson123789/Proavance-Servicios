using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.BO;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.Socket;
using System.Net;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{

	[Route("api/ChatController")]
    public class ChatController : Controller
    {
        // GET: api/<controller>
       
		[Route("[Action]")]
		[HttpPost]
		public ActionResult RegistrarOportunidadActualizarAlumno([FromBody] RegistroOportunidadAlumnoDTO Formulario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				integraDBContext context = new integraDBContext();
				ActividadDetalleRepositorio _repActividadDetalle = new ActividadDetalleRepositorio(context);
				OportunidadLogRepositorio _repOportunidadLog = new OportunidadLogRepositorio(context);
				OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(context);
				AlumnoRepositorio _repAlumno = new AlumnoRepositorio(context);

				var res = "";
				var _alumno = Formulario.Alumno;
				var _oportunidad = Formulario.Oportunidad;

				var Alumno = _repAlumno.FirstById(_alumno.Id);
				#region Alumno Atributos
				Alumno.Nombre1 = _alumno.Nombre1;
				Alumno.Nombre2 = _alumno.Nombre2;
				Alumno.ApellidoPaterno = _alumno.ApellidoPaterno;
				Alumno.ApellidoMaterno = _alumno.ApellidoMaterno;
				Alumno.Dni = _alumno.DNI;
				Alumno.Direccion = _alumno.Direccion;
				Alumno.Telefono = _alumno.Telefono;
				Alumno.Celular = _alumno.Celular;
				Alumno.Email1 = _alumno.Email1;
				Alumno.Email2 = _alumno.Email2;
				Alumno.IdCargo = _alumno.IdCargo;
				Alumno.IdAformacion = _alumno.IdAFormacion;
				Alumno.IdAtrabajo = _alumno.IdATrabajo;
				Alumno.IdIndustria = _alumno.IdIndustria;
				Alumno.IdReferido = _alumno.IdReferido;
				Alumno.IdCodigoPais = _alumno.IdCodigoPais;
				Alumno.IdCiudad = _alumno.IdCodigoCiudad;
				Alumno.HoraContacto = _alumno.HoraContacto;
				Alumno.HoraPeru = _alumno.HoraPeru;
				Alumno.Telefono2 = _alumno.Telefono2;
				Alumno.Celular2 = _alumno.Celular2;
				Alumno.IdEmpresa = _alumno.IdEmpresa;
				Alumno.UsuarioModificacion = Formulario.Usuario;
				Alumno.FechaModificacion = DateTime.Now;
				#endregion
				Alumno.ValidarEstadoContactoWhatsAppTemporal(context);
				_repAlumno.Update(Alumno);

				try
				{
					string URI = "http://localhost:4348/marketing/InsertarActualizarAlumno_a_v3?IdAlumno=" + Alumno.Id;
					using (WebClient wc = new WebClient())
					{
						wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
						wc.DownloadString(URI);
					}
				}
				catch (Exception e)
				{
				}
				var _oportunidadLog = _repOportunidadLog.ObtenerLogsPorIdOportunidad(_oportunidad.Id);
				var _actividadDetalle = _repActividadDetalle.ObtenerActividadDetallePorIdOportunidad(_oportunidad.Id);

				if (_oportunidadLog.Count > 1) throw new Exception("Se actualizo contacto pero la oportunidad seleccionada no se puede editar debido a que ya ha sido ejecutada mas de una vez");
				if (_oportunidadLog.Count == 0) throw new Exception("Se actualizo contacto pero la oportunidad seleccionada no se puede editar debido a que no tiene Logs");
				if (_actividadDetalle.Count == 0) throw new Exception("Se actualizo contacto pero la oportunidad seleccionada no se puede editar debido a que no tiene Actividades Detalle");

				using (TransactionScope scope = new TransactionScope())
				{
					var Oportunidad = _repOportunidad.FirstById(_oportunidad.Id);
					#region Oportunidad Atributos
					Oportunidad.IdAlumno = _oportunidad.IdAlumno;
					Oportunidad.IdCentroCosto = _oportunidad.IdCentroCosto;
					Oportunidad.IdFaseOportunidad = _oportunidad.IdFaseOportunidad;
					Oportunidad.IdOrigen = _oportunidad.IdOrigen;
					Oportunidad.IdPersonalAsignado = _oportunidad.IdPersonal_Asignado;
					Oportunidad.IdTipoDato = _oportunidad.IdTipoDato;
					Oportunidad.UltimoComentario = _oportunidad.UltimoComentario;
					Oportunidad.IdInteraccionFormulario = _oportunidad.fk_id_tipointeraccion;
					Oportunidad.UsuarioModificacion = Formulario.Usuario;
					Oportunidad.FechaModificacion = DateTime.Now;
					#endregion Atributos
					_repOportunidad.Update(Oportunidad);

					var ActividadDetalle = _repActividadDetalle.FirstById(Oportunidad.IdActividadDetalleUltima);
					#region ActividadDetalle Atributos
					ActividadDetalle.FechaProgramada = Oportunidad.UltimaFechaProgramada;
					ActividadDetalle.IdEstadoActividadDetalle = Oportunidad.IdActividadDetalleUltima;
					ActividadDetalle.UsuarioModificacion = Formulario.Usuario;
					ActividadDetalle.FechaModificacion = DateTime.Now;
					#endregion
					_repActividadDetalle.Update(ActividadDetalle);

					//var OportunidadLog = _repOportunidadLog.ObtenerUltimoOportunidadLogPorIdOportunidad(_oportunidad.Id);
					var OportunidadLog = _repOportunidadLog.GetBy(x => x.IdOportunidad == _oportunidad.Id).OrderByDescending(x => x.FechaLog).FirstOrDefault();
					#region OportunidadLog Atributos
					OportunidadLog.IdPersonalAsignado = _oportunidad.IdPersonal_Asignado;
					OportunidadLog.IdFaseOportunidad = _oportunidad.IdFaseOportunidad;
					OportunidadLog.IdFaseOportunidadAnt = _oportunidad.IdFaseOportunidad;
					OportunidadLog.IdCentroCosto = _oportunidad.IdCentroCosto;
					Oportunidad.IdOrigen = _oportunidad.IdOrigen;
					OportunidadLog.IdTipoDato = _oportunidad.IdTipoDato;
					OportunidadLog.FechaModificacion = DateTime.Now;
					OportunidadLog.UsuarioModificacion = Formulario.Usuario;
					#endregion
					_repOportunidadLog.Update(OportunidadLog);

                    scope.Complete();
					res = "OK";
				}

				try
				{
					string URI = "http://localhost:4348/marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + _oportunidad.Id;
					using (WebClient wc = new WebClient())
					{
						wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
						wc.DownloadString(URI);
					}
				}
				catch (Exception e)
				{
				}


				return Ok(new { rpta = res, Alumno, Oportunidad = _oportunidad.Id});
			}
			catch(Exception Ex)
			{
				return BadRequest(Ex.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ActualizarAsignacionAutomaticaEnviarValidar([FromBody] RegistroOportunidadAlumnoDTO Formulario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				AsignacionAutomaticaRepositorio _repAsignacionAutomatica = new AsignacionAutomaticaRepositorio();
				OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();
				var _alumnoFormulario = Formulario.Alumno;
				var _oportunidadFormulario = Formulario.Oportunidad;
				if (_repAsignacionAutomatica.Exist(_oportunidadFormulario.Id))//asignacion auto?
				{
					var AsignacionAutomatica = _repAsignacionAutomatica.FirstById(_oportunidadFormulario.Id);
					#region AsignacionAutomatica Atributos
					AsignacionAutomatica.IdPais = _alumnoFormulario.IdCodigoPais;
					AsignacionAutomatica.IdCiudad = _alumnoFormulario.IdCodigoCiudad;
					AsignacionAutomatica.Email = _alumnoFormulario.Email1;
					AsignacionAutomatica.Celular = _alumnoFormulario.Celular;
					AsignacionAutomatica.IdCentroCosto = _oportunidadFormulario.IdCentroCosto;
					AsignacionAutomatica.IdOrigen = _oportunidadFormulario.IdOrigen;
					AsignacionAutomatica.FechaModificacion = DateTime.Now;
					AsignacionAutomatica.UsuarioModificacion = Formulario.Usuario;
					#endregion
					_repAsignacionAutomatica.Update(AsignacionAutomatica);

					AsignacionAutomaticaErrorRepositorio _repAsignacionAutomaticaError = new AsignacionAutomaticaErrorRepositorio();
					var listaErroresTemp = _repAsignacionAutomaticaError.GetBy(x => x.IdAsignacionAutomatica == _oportunidadFormulario.Id, x => new { x.Id }).ToList();
					_repAsignacionAutomaticaError.Delete(listaErroresTemp.Select(x => x.Id), Formulario.Usuario);
					var id = AsignacionAutomatica.Id;

					var idAA = _repAsignacionAutomatica.GetBy(x => x.IdOportunidad == _oportunidadFormulario.Id, x => new { x.Id }).FirstOrDefault().Id;
					var idFaseOportunidadPW = _repOportunidad.GetBy(x=> x.Id == _oportunidadFormulario.Id, x => new { x.IdFaseOportunidadPortal }).FirstOrDefault().IdFaseOportunidadPortal;
					AsignacionAutomaticaTempRepositorio _repAsignacionAutomaticaTemp = new AsignacionAutomaticaTempRepositorio();
					AgendaSocket.getInstance().ValidarOportunidad(_repAsignacionAutomaticaTemp.GetBy(x => x.IdFaseOportunidadPortal == idFaseOportunidadPW, x => new { x.Id }).FirstOrDefault().Id);
				}

				return Ok();
			}
			catch (Exception Ex)
			{
				return BadRequest(Ex.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCentroCostosNombre([FromBody]Dictionary<string, string> Filtros)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
				var CentroCosto = _repCentroCosto.ObtenerCentroCostoNombrePorId(int.Parse(Filtros["id"]));
				return Ok(CentroCosto);

			}
			catch(Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[Route("[Action]")]
		[HttpGet]
		public ActionResult ObtenerDatosFiltroContactoOportunidad()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var nombre = "Chat";
				PaisRepositorio _repPais = new PaisRepositorio();
				CiudadRepositorio _repCiudad = new CiudadRepositorio();
				TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio();
				FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio();
				CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio();
				CargoRepositorio _repCargo = new CargoRepositorio();
				AreaFormacionRepositorio _repAreaFormacion = new AreaFormacionRepositorio();
				AreaTrabajoRepositorio _repAreaTrabajo = new AreaTrabajoRepositorio();
				IndustriaRepositorio _repIndustria = new IndustriaRepositorio();
				OrigenRepositorio _repOrigen = new OrigenRepositorio();


				ContactoOportunidadDTO ContactoOportunidad = new ContactoOportunidadDTO
				{
					Paises = _repPais.ObtenerTodoFiltro(),
					Ciudades = _repCiudad.ObtenerCiudadesPorPais(),
					TipoDatoChats = _repTipoDato.CargarTipoDatoChat(),
					FaseOportunidades = _repFaseOportunidad.ObtenerFaseOportunidadTodoFiltro(),
					CategoriaOrigenes = _repCategoriaOrigen.ObtenerCategoriaOrigenPorNombre(nombre),
					Cargos = _repCargo.ObtenerCargoFiltro(),
					AreasFormacion = _repAreaFormacion.ObtenerAreaFormacionFiltro(),
					AreasTrabajo = _repAreaTrabajo.ObtenerTodoAreaTrabajoFiltro(),
					Industrias = _repIndustria.ObtenerIndustriaFiltro(),
					Origenes = _repOrigen.ObtenerOrigenChat(nombre)
				};

				return Ok(ContactoOportunidad);

			}
			catch (Exception Ex)
			{
				return BadRequest(Ex.Message);
			}
		}
	}
}