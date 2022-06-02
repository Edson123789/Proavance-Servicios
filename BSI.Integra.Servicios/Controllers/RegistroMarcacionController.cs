using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/RegistroMarcacion")]
    public class RegistroMarcacionController : ControllerBase
    {
		private readonly integraDBContext _integraDBContext;
		private readonly Aplicacion.GestionPersonas.Repositorio.RegistroMarcadorFechaRepositorio _repRegistroMarcadorFecha;
		private readonly IntegraAspNetUsersRepositorio _repIntegraAspNetUsers;
		private readonly PersonalRepositorio _repPersonal;
        private readonly MensajeTiempoInactivoRepositorio _repTiempoInactivo;
        private readonly ConfiguracionMarcadorMensajeRepositorio _repConfiguracionMarcadorMensaje;
        private readonly ConfiguracionTipoMarcacionRepositorio _repConfiguracionTipoMarcacion;


        public RegistroMarcacionController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repRegistroMarcadorFecha = new Aplicacion.GestionPersonas.Repositorio.RegistroMarcadorFechaRepositorio(_integraDBContext);
			_repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
			_repPersonal = new PersonalRepositorio(_integraDBContext);
            _repTiempoInactivo= new MensajeTiempoInactivoRepositorio(_integraDBContext);
            _repConfiguracionMarcadorMensaje = new ConfiguracionMarcadorMensajeRepositorio(_integraDBContext);
            _repConfiguracionTipoMarcacion = new ConfiguracionTipoMarcacionRepositorio(_integraDBContext);

        }

        [HttpGet]
		[Route("[action]/{Usuario}")]
		public ActionResult ObtenerTiempoInactividadPersonal(string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var user = _repIntegraAspNetUsers.ObtenerIdentidadUsusario(Usuario);
				var personal = _repPersonal.FirstById(user.Id);
				var fechaUltimaLLamada = _repRegistroMarcadorFecha.ObtenerFechaUltimaLlamadaPersonal(personal.Anexo3Cx);
				TimeSpan diff = DateTime.Now - fechaUltimaLLamada.FechaFin;
				var registroMarcacionPersonal = _repRegistroMarcadorFecha.ObtenerRegistroMarcacionPersonal(personal.Id, DateTime.Now);
				return Ok(new { TiempoInactivo = diff.Minutes, Marcacion = registroMarcacionPersonal } );
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet]
		[Route("[action]/{Usuario}/{TipoBoton}/{DNI}")]
		public ActionResult InsertarMarcacionPersonal(string Usuario, int TipoBoton,string DNI)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{

				var user = _repIntegraAspNetUsers.ObtenerIdentidadUsusarioDNI(Usuario,DNI);
				var personal = _repPersonal.FirstById(user.Id);
				var fechaActual = DateTime.Now;
				bool rpta = false;
				bool yaMarcado = false;
                bool noCumpleTiempoMinimoAlmuerzo = false;
				var registroMarcacionPersonal = _repRegistroMarcadorFecha.ObtenerRegistroMarcacionPersonalDNI(personal.Id, fechaActual,DNI);
				switch(TipoBoton){
					case 1: //Boton entrada a la empresa
						if(registroMarcacionPersonal == null)
						{
							registroMarcacionPersonal = new RegistroMarcadorFechaBO()
							{
								Pin = personal.NumeroDocumento,
								Fecha = fechaActual.Date,
								IdCiudad = personal.IdCiudad == null ? 0 : personal.IdCiudad.Value,
								IdPersonal = personal.Id,
								M1 = fechaActual.TimeOfDay,
								Estado = true,
								UsuarioCreacion = Usuario,
								UsuarioModificacion = Usuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now
							};
							rpta = _repRegistroMarcadorFecha.Insert(registroMarcacionPersonal);
						}
						else
						{
							if(registroMarcacionPersonal.M1 != null)
							{
								yaMarcado = true;
							}
							else
							{
								registroMarcacionPersonal.M1 = fechaActual.TimeOfDay;
								registroMarcacionPersonal.UsuarioModificacion = Usuario;
								registroMarcacionPersonal.FechaModificacion = DateTime.Now;
								rpta = _repRegistroMarcadorFecha.Update(registroMarcacionPersonal);
							}
						}
						break;
					case 2: //Hora salida almuerzo
						if (registroMarcacionPersonal != null)
						{
							if (registroMarcacionPersonal.M2 == null)
							{
								registroMarcacionPersonal.M2 = fechaActual.TimeOfDay;
								registroMarcacionPersonal.UsuarioModificacion = Usuario;
								registroMarcacionPersonal.FechaModificacion = DateTime.Now;
								rpta = _repRegistroMarcadorFecha.Update(registroMarcacionPersonal);
							}
							else
							{
								yaMarcado = true;
							}
						}
						else
						{
							registroMarcacionPersonal = new RegistroMarcadorFechaBO()
							{
								Pin = personal.NumeroDocumento,
								Fecha = fechaActual.Date,
								IdCiudad = personal.IdCiudad == null ? 0 : personal.IdCiudad.Value,
								IdPersonal = personal.Id,
								M2 = fechaActual.TimeOfDay,
								Estado = true,
								UsuarioCreacion = Usuario,
								UsuarioModificacion = Usuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now
							};
							rpta = _repRegistroMarcadorFecha.Insert(registroMarcacionPersonal);
						}
						break;
					case 3:
						if (registroMarcacionPersonal != null)
						{
							if (registroMarcacionPersonal.M3 == null)
							{
                                var diferencia = new TimeSpan();
                                if (registroMarcacionPersonal.M2 != null)
                                {
                                    diferencia = fechaActual.TimeOfDay - registroMarcacionPersonal.M2.Value;
                                    var horasdiferencia = diferencia.Hours * 60;
                                    var minutosdiferencia = diferencia.Minutes + horasdiferencia;
                                    if (minutosdiferencia < 45)
                                    {
                                        noCumpleTiempoMinimoAlmuerzo = true;
                                        break;
                                    }
                                }
                                


                                registroMarcacionPersonal.M3 = fechaActual.TimeOfDay;
								registroMarcacionPersonal.UsuarioModificacion = Usuario;
								registroMarcacionPersonal.FechaModificacion = DateTime.Now;
								rpta = _repRegistroMarcadorFecha.Update(registroMarcacionPersonal);
							}
							else
							{
								yaMarcado = true;
							}
						}
						else
						{
							registroMarcacionPersonal = new RegistroMarcadorFechaBO()
							{
								Pin = personal.NumeroDocumento,
								Fecha = fechaActual.Date,
								IdCiudad = personal.IdCiudad == null ? 0 : personal.IdCiudad.Value,
								IdPersonal = personal.Id,
								M3 = fechaActual.TimeOfDay,
								Estado = true,
								UsuarioCreacion = Usuario,
								UsuarioModificacion = Usuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now
							};
							rpta = _repRegistroMarcadorFecha.Insert(registroMarcacionPersonal);
						}
						break;
					case 4:
						if(fechaActual.Hour >= 0 && fechaActual.Hour <= 6)
						{
							//Esta de amanecida
							var temp = fechaActual.AddDays(-1);
							var newFecha = new DateTime(temp.Year, temp.Month, temp.Day, 23, 59, 59);
							var registroMarcacionPersonalTemp = _repRegistroMarcadorFecha.ObtenerRegistroMarcacionPersonalDNI(personal.Id, temp,DNI);
							registroMarcacionPersonalTemp.M4 = newFecha.TimeOfDay;
							registroMarcacionPersonalTemp.M5 = fechaActual.Date.TimeOfDay;
							registroMarcacionPersonalTemp.M6 = fechaActual.TimeOfDay;
							registroMarcacionPersonalTemp.UsuarioModificacion = Usuario;
							registroMarcacionPersonalTemp.FechaModificacion = DateTime.Now;
							rpta = _repRegistroMarcadorFecha.Update(registroMarcacionPersonalTemp);

						}
						else
						{
							if (registroMarcacionPersonal != null)
							{
								if (registroMarcacionPersonal.M4 == null)
								{
									registroMarcacionPersonal.M4 = fechaActual.TimeOfDay;
									registroMarcacionPersonal.UsuarioModificacion = Usuario;
									registroMarcacionPersonal.FechaModificacion = DateTime.Now;
									rpta = _repRegistroMarcadorFecha.Update(registroMarcacionPersonal);
								}
								else
								{
									yaMarcado = true;
								}
							}
							else
							{
								registroMarcacionPersonal = new RegistroMarcadorFechaBO()
								{
									Pin = personal.NumeroDocumento,
									Fecha = fechaActual.Date,
									IdCiudad = personal.IdCiudad == null ? 0 : personal.IdCiudad.Value,
									IdPersonal = personal.Id,
									M4 = fechaActual.TimeOfDay,
									Estado = true,
									UsuarioCreacion = Usuario,
									UsuarioModificacion = Usuario,
									FechaCreacion = DateTime.Now,
									FechaModificacion = DateTime.Now
								};
								rpta = _repRegistroMarcadorFecha.Insert(registroMarcacionPersonal);
							}
						}
						break;
				}
				return Ok(new { EsInsertado = rpta, EsMarcado = yaMarcado,NoCumpleTiempoAlmuerzo= noCumpleTiempoMinimoAlmuerzo });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}



		[HttpGet]
		[Route("[action]/{Usuario}")]
		public ActionResult ObtenerAreaPersonal(string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var user = _repIntegraAspNetUsers.ObtenerIdentidadUsusario(Usuario);
				var personal = _repPersonal.FirstById(user.Id);
				return Ok(new { Area = personal.AreaAbrev } );
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


        [HttpGet]
        [Route("[action]")]
        public ActionResult ObtenerTiempoRestringido()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaMensaje = _repTiempoInactivo.GetBy(x => x.Estado == true, x => new { x.Id, x.MinutoInactivo, x.Mensaje }).ToList();
                return Ok(listaMensaje);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

		[HttpGet]
		[Route("[action]")]
		public ActionResult ObtenerArea()
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio();
				var Area = _repPersonalAreaTrabajo.ObtenerTodoFiltro();
				return Ok(Area);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet]
		[Route("[action]")]
		public ActionResult ObtenerSede()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				SedeTrabajoRepositorio _repSedeTrabajo = new SedeTrabajoRepositorio();
				var Sede = _repSedeTrabajo.ObtenerTodoFiltro();
				return Ok(Sede);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet]
		[Route("[action]")]
		public ActionResult ObtenerPersonal()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalRepositorio _repPersonal = new PersonalRepositorio();
				var Personal = _repPersonal.ObtenerTodoFiltro();
				return Ok(Personal);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Route("[action]")]
		public ActionResult ObtenerPersonalPorArea(List<int> idarea)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio();
				PersonalRepositorio _repPersonal = new PersonalRepositorio();

				List<string> abreviatura = new List<string>();
				List<DatoPersonalDTO> listaPersonal = new List<DatoPersonalDTO>();

				foreach (var item in idarea)
                {
					var lista = _repPersonalAreaTrabajo.getAbreviaturaArea(item);
					abreviatura.Add(lista);
                }

				foreach (var item in abreviatura)
				{
					var lista = _repPersonal.getDatosPersonalPorArea(item);
					listaPersonal.AddRange(lista);
				}				
				return Ok(listaPersonal);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Route("[action]")]
		public ActionResult ObtenerIdPersonalPorArea(List<int> idarea)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio();
				PersonalRepositorio _repPersonal = new PersonalRepositorio();

				List<string> abreviatura = new List<string>();
				List<DatoPersonalDTO> listaPersonal = new List<DatoPersonalDTO>();
				List<int> idpersonal = new List<int>();

				foreach (var item in idarea)
				{
					var lista = _repPersonalAreaTrabajo.getAbreviaturaArea(item);
					abreviatura.Add(lista);
				}

				foreach (var item in abreviatura)
				{
					var lista = _repPersonal.getDatosIdPersonalPorArea(item);
					listaPersonal.AddRange(lista);
				}

				foreach (var item in listaPersonal)
                {
					idpersonal.Add(item.Id);
                }
				return Ok(idpersonal);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ReporteMarcacion([FromBody] FiltroReporteMarcacionDTO data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				//data.FechaInicio = Convert.ToDateTime(data.FechaInicio.Day.ToString() +"/" +data.FechaInicio.Month.ToString()+"/" + data.FechaInicio.Year.ToString());
				//data.FechaFin = Convert.ToDateTime(data.FechaFin.Day.ToString() + "/" + data.FechaFin.Month.ToString() + "/" + data.FechaFin.Year.ToString());				
				var marcaciones = _repRegistroMarcadorFecha.ObtenerMarcacionesPersonal(data);
				var _marcaciones = marcaciones.OrderBy(x => x.Fecha).ToList();
				var datosAgrupado = (from p in _marcaciones
									 group p by p.Fecha into grupo
									 select new { g = grupo.Key, l = grupo }).ToList();
				return Ok(datosAgrupado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet]
		[Route("[action]")]
		public ActionResult ObtenerValoresMarcacion()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var Mensaje = _repConfiguracionMarcadorMensaje.GetBy(x => x.Estado == true).ToList();
				var listaBotones = _repConfiguracionTipoMarcacion.GetBy(x => x.Estado == true).ToList();

				return Ok(new { Mensaje, listaBotones });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
        
    
}