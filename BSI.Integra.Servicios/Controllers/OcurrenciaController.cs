using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.Extensions.Logging;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Ocurrencia")]
    public class OcurrenciaController : BaseController<TOcurrencia, ValidadorOcurrenciaDTO>
    {
        public OcurrenciaController(IIntegraRepository<TOcurrencia> repositorio, ILogger<BaseController<TOcurrencia, ValidadorOcurrenciaDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerConfigLlamadaPorIdOcurrencia(int IdOcurrencia)
        {
            try
            {
                ConfiguracionLlamadaOcurrenciaRepositorio repositorio = new ConfiguracionLlamadaOcurrenciaRepositorio();
                var lista = repositorio.ObtenerTodasConfigLlamadaPorIdOcurrencia(IdOcurrencia);
                return Json(new { Result = "OK", Records = lista });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFaseTiempoLlamadas()
        {
            try
            {
                FaseTiempoLlamadaRepositorio repositorio = new FaseTiempoLlamadaRepositorio();
                var lista = repositorio.ObtenerTodasFases();
                return Json(new { Result = "OK", Records = lista });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCondicionOcurrenciasLlamadas()
        {
            try
            {
                CondicionOcurrenciaLlamadaRepositorio repositorio = new CondicionOcurrenciaLlamadaRepositorio();
                var lista = repositorio.ObtenerTodasCondiciones();
                return Json(new { Result = "OK", Records = lista });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerConectorOcurrenciasLlamadas()
        {
            try
            {
                ConectorOcurrenciaLlamadaRepositorio repositorio = new ConectorOcurrenciaLlamadaRepositorio();
                var lista = repositorio.ObtenerTodosConectores();
                return Json(new { Result = "OK", Records = lista });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerActividadesCabeceras()
        {
            try
            {
                ActividadCabeceraRepositorio repositorio = new ActividadCabeceraRepositorio();
                var lista = repositorio.ObtenerTodoFiltro();
                return Json(new { Result = "OK", Records = lista });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPlantillas()
        {
            try
            {
                PlantillaRepositorio repositorio = new PlantillaRepositorio();
                var lista = repositorio.ObtenerListaPlantilla();
                return Json(new { Result = "OK", Records = lista });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaFasesOportunidad()
        {
            try
            {
                FaseOportunidadRepositorio repositorio = new FaseOportunidadRepositorio();
                var lista = repositorio.ObtenerFaseOportunidadTodoFiltro();
                return Json(new { Result = "OK", Records = lista });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaEstadosOcurrencias()
        {
            try
            {
                EstadoOcurrenciaRepositorio repositorio = new EstadoOcurrenciaRepositorio();
                var lista = repositorio.ObtenerEstadoOcurrenciasParaFiltro();
                return Json(new { Result = "OK", Records = lista });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaActividades()
        {
            try
            {
                ActividadCabeceraRepositorio repositorio = new ActividadCabeceraRepositorio();
                var lista = repositorio.ObtenerAllActividadCabecera();
                return Json(new { Result = "OK", Records = lista });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaOcurrencias()
        {
            try
            {
                OcurrenciaRepositorio repositorio = new OcurrenciaRepositorio();
                var lista = repositorio.ObtenerOcurrenciasParaFiltro();
                return Json(new { Result = "OK", Records = lista });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosMontoPago()
        {
            try
            {
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{IdOcurrencia}")]
        [HttpGet]
        public ActionResult ObtenerActividadesPorOcurrencia(int IdOCurrencia)
        {
            try
            {
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio();

                var lista = _repOcurrencia.ObtenerActividadesPorOcurrencia(IdOCurrencia);
                return Json(new { Result = "OK", Records = lista });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCategorias([FromBody]ObtenerCategoriaFiltroDTO Objeto)
        {
            IList<OcurrenciaPorActividadPadreDTO> lista = new List<OcurrenciaPorActividadPadreDTO>();
            OcurrenciaRepositorio repositorio = new OcurrenciaRepositorio();

            try
            {
                if (Objeto.IdActividad == 0)
                {
                    lista = repositorio.ObtenerTodasOcurrenciasActividad(Objeto.Id.Value);

                    //return Ok(lista);
                    return Json(new { Result = "OK", Records = lista });
                }
                else
                {
                    if (Objeto.Id != null)
                    {
                        lista = repositorio.ObtenerTodasOcurrenciasActividadPadre(Objeto.Id.Value, Objeto.IdActividad.Value);
                    }
                    else
                    {
                        lista = repositorio.ObtenerTodasOcurrenciasActividadPadre(0, Objeto.IdActividad.Value);
                    }

                    return Json(new { Result = "OK", Records = lista });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("[action]/{IdOcurrencia}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionLLamadasPorOcurrencia(int IdOcurrencia)
        {
            try
            {
                ConfiguracionLlamadaOcurrenciaRepositorio _repConfiguracionLlamada = new ConfiguracionLlamadaOcurrenciaRepositorio();

                var configuracion = _repConfiguracionLlamada.GetBy(w => w.IdOcurrencia == IdOcurrencia);
                return Ok(configuracion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{IdOcurrencia}/{IdOcurrenciaActividad}")]
        [HttpGet]
        public ActionResult ObtenerOcurrenciaPorId(int IdOcurrencia, int IdOcurrenciaActividad)
        {
            try
            {
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio();
                WhatsAppPlantillaPorOcurrenciaActividadRepositorio _repWhatsAppPlantillaPorOcurrenciaActividad = new WhatsAppPlantillaPorOcurrenciaActividadRepositorio();

                var ListaWhatsAppPlantillaPorOcurrenciaActividad = _repWhatsAppPlantillaPorOcurrenciaActividad.ObtenerAsociacionWhatsAppPlantillaPorIdActividadOcurrencia(IdOcurrenciaActividad);
                var ListaCorreoPlantillaPorOcurrenciaActividad = _repWhatsAppPlantillaPorOcurrenciaActividad.ObtenerAsociacionCorreoPlantillaPorIdActividadOcurrencia(IdOcurrenciaActividad);

                var Ocurrencia =_repOcurrencia.FirstById(IdOcurrencia);
                return Ok( new { Ocurrencia, ListaWhatsAppPlantillaPorOcurrenciaActividad, ListaCorreoPlantillaPorOcurrenciaActividad });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarOcurrencia([FromBody] OcurrenciaCompuestoDTO Objeto)
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio();
                OcurrenciaReporteRepositorio _repOcurrenciaReporte = new OcurrenciaReporteRepositorio();
                OcurrenciaActividadRepositorio _repOcurrenciaActividad = new OcurrenciaActividadRepositorio();

                OcurrenciaBO ocurrencia = new OcurrenciaBO();
                ocurrencia = _repOcurrencia.FirstById(Objeto.ocurrencia.Id);

                ocurrencia.Nombre = Objeto.ocurrencia.Nombre;
                ocurrencia.NombreM = Objeto.ocurrencia.Nombre.ToUpper();
                //ocurrencia.NombreCs  se desconoce su logica de actualizacion o insercion
                ocurrencia.IdFaseOportunidad = Objeto.ocurrencia.IdFaseOportunidad.Value;
                ocurrencia.IdActividadCabecera = Objeto.ocurrencia.IdActividadCabecera;
                ocurrencia.IdPlantillaSpeech = Objeto.ocurrencia.IdPlantilla_Speech;
                ocurrencia.IdEstadoOcurrencia = Objeto.ocurrencia.IdEstadoOcurrencia.Value;
                ocurrencia.Oportunidad = Objeto.ocurrencia.Oportunidad.Value;
                ocurrencia.RequiereLlamada = Objeto.ocurrencia.RequiereLlamada;
                ocurrencia.Roles = Objeto.ocurrencia.Roles;
                ocurrencia.Color = Objeto.ocurrencia.Color;
				ocurrencia.IdPersonalAreaTrabajo = Objeto.ocurrencia.IdPersonalAreaTrabajo;
				ocurrencia.FechaModificacion = DateTime.Now;
                ocurrencia.UsuarioModificacion = Objeto.Usuario;
				
                if (!ocurrencia.HasErrors)
                    _repOcurrencia.Update(ocurrencia);
                else
                    return BadRequest(ocurrencia.GetErrors(null));

                if (!_repOcurrencia.ExcepcionesOcurrencia(ocurrencia.Id))
                {
                    OcurrenciaReporteBO ocurrenciaReporte = new OcurrenciaReporteBO();
                    ocurrenciaReporte = _repOcurrenciaReporte.FirstById(Objeto.ocurrencia.Id);

                    ocurrenciaReporte.Nombre = Objeto.ocurrencia.Nombre;
                    ocurrenciaReporte.IdFaseOportunidad = Objeto.ocurrencia.IdFaseOportunidad.Value;
                    ocurrenciaReporte.IdActividadCabecera = Objeto.ocurrencia.IdActividadCabecera;
                    ocurrenciaReporte.IdPlantillaSpeech = Objeto.ocurrencia.IdPlantilla_Speech;
                    ocurrenciaReporte.IdEstadoOcurrencia = Objeto.ocurrencia.IdEstadoOcurrencia.Value;
                    ocurrenciaReporte.Oportunidad = Objeto.ocurrencia.Oportunidad.Value;
                    ocurrenciaReporte.RequiereLlamada = Objeto.ocurrencia.RequiereLlamada;
                    ocurrenciaReporte.Roles = Objeto.ocurrencia.Roles;
                    ocurrenciaReporte.Color = Objeto.ocurrencia.Color;
                    ocurrenciaReporte.FechaModificacion = ocurrencia.FechaModificacion;
                    ocurrenciaReporte.UsuarioModificacion = Objeto.Usuario;

                    if (!ocurrenciaReporte.HasErrors)
                        _repOcurrenciaReporte.Update(ocurrenciaReporte);
                    else
                        return BadRequest(ocurrenciaReporte.GetErrors(null));
                }
                ConfiguracionLlamadaOcurrenciaRepositorio repositorio = new ConfiguracionLlamadaOcurrenciaRepositorio();
                //var configuracionesDesdeDB = repositorio.ObtenerTodasConfigLlamadaPorIdOcurrencia(ocurrencia.Id);
                var configuracionesDesdeCliente = Objeto.configuraciones;

                if (configuracionesDesdeCliente.Count == 0) // cuando desde el cliente se establece "RequiereLlamada=No"
                {
                    var configuracionesDesdeDB = repositorio.GetBy(x => x.IdOcurrencia == ocurrencia.Id).ToList();
                    for (int i = 0; i < configuracionesDesdeDB.Count; ++i)
                    {
                        repositorio.Delete(configuracionesDesdeDB[i].Id, Objeto.Usuario);
                    }
                }
                else
                {
                    var configuracionesDesdeDB = repositorio.ObtenerTodasConfigLlamadaPorIdOcurrencia(ocurrencia.Id);
                    for (int i = 0; i < configuracionesDesdeCliente.Count; ++i)
                    {
                        if (configuracionesDesdeCliente[i].Id == 0) // cuando son configuraciones nuevas (insercion)
                        {
                            
                            ConfiguracionLlamadaOcurrenciaBO NuevaConfiguracionLlamada = new ConfiguracionLlamadaOcurrenciaBO();
                            NuevaConfiguracionLlamada.IdOcurrencia = configuracionesDesdeCliente[i].IdOcurrencia;
                            NuevaConfiguracionLlamada.IdConectorOcurrenciaLlamada = configuracionesDesdeCliente[i].IdConectorOcurrenciaLlamada;
                            NuevaConfiguracionLlamada.NumeroLlamada = configuracionesDesdeCliente[i].NumeroLlamada;
                            NuevaConfiguracionLlamada.IdCondicionOcurrenciaLlamada = configuracionesDesdeCliente[i].IdCondicionOcurrenciaLlamada;
                            NuevaConfiguracionLlamada.IdFaseTiempoLlamada = configuracionesDesdeCliente[i].IdFaseTiempoLlamada;
                            NuevaConfiguracionLlamada.Duracion = configuracionesDesdeCliente[i].Duracion;
                            NuevaConfiguracionLlamada.FechaCreacion  = DateTime.Now ;
                            NuevaConfiguracionLlamada.FechaModificacion  = DateTime.Now;
                            NuevaConfiguracionLlamada.UsuarioCreacion  = Objeto.Usuario;
                            NuevaConfiguracionLlamada.UsuarioModificacion  = Objeto.Usuario;
                            NuevaConfiguracionLlamada.Estado  = true ;
                            NuevaConfiguracionLlamada.IdMigracion  = null;

                            repositorio.Insert(NuevaConfiguracionLlamada);
                        }
                        else // actualizar configuraciones existentes
                        {
                            var configuracion = repositorio.FirstById(configuracionesDesdeCliente[i].Id);
                            configuracion.UsuarioModificacion = Objeto.Usuario;
                            configuracion.FechaModificacion = DateTime.Now;
                            configuracion.IdConectorOcurrenciaLlamada = configuracionesDesdeCliente[i].IdConectorOcurrenciaLlamada;
                            configuracion.NumeroLlamada = configuracionesDesdeCliente[i].NumeroLlamada;
                            configuracion.IdCondicionOcurrenciaLlamada = configuracionesDesdeCliente[i].IdCondicionOcurrenciaLlamada;
                            configuracion.IdFaseTiempoLlamada = configuracionesDesdeCliente[i].IdFaseTiempoLlamada;
                            configuracion.Duracion = configuracionesDesdeCliente[i].Duracion;
                            repositorio.Update(configuracion);
                        }

                    }
                }

                
                

                return Ok(ocurrencia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarOcurrencia([FromBody] OcurrenciaCompuestoDTO Objeto)
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                OcurrenciaReporteRepositorio _repOcurrenciaReporte = new OcurrenciaReporteRepositorio(contexto);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(contexto);
               
               
                OcurrenciaBO ocurrencia = new OcurrenciaBO();
                var newOcurrencia = Objeto.ocurrencia;
                ocurrencia.Nombre = newOcurrencia.Nombre;
                ocurrencia.NombreM = newOcurrencia.Nombre.ToUpper();
                //ocurrencia.NombreCS = se desconoce su logica de insercion
                ocurrencia.IdFaseOportunidad = newOcurrencia.IdFaseOportunidad.Value;
                ocurrencia.IdActividadCabecera = newOcurrencia.IdActividadCabecera;
                ocurrencia.IdPlantillaSpeech = newOcurrencia.IdPlantilla_Speech;
                ocurrencia.IdEstadoOcurrencia = newOcurrencia.IdEstadoOcurrencia.Value;
                ocurrencia.Oportunidad = newOcurrencia.Oportunidad.Value;
                ocurrencia.RequiereLlamada = newOcurrencia.RequiereLlamada;
                ocurrencia.Roles = newOcurrencia.Roles;
                ocurrencia.Color = newOcurrencia.Color;
                ocurrencia.Estado = true;
                ocurrencia.UsuarioCreacion = Objeto.Usuario;
                ocurrencia.UsuarioModificacion = Objeto.Usuario;
                ocurrencia.FechaCreacion = DateTime.Now;
                ocurrencia.FechaModificacion = DateTime.Now;
				ocurrencia.IdPersonalAreaTrabajo = newOcurrencia.IdPersonalAreaTrabajo;

                if (!ocurrencia.HasErrors)
                    _repOcurrencia.Insert(ocurrencia);
                else
                    return BadRequest(ocurrencia.GetErrors(null));

                OcurrenciaReporteBO ocurrenciaReporte = new OcurrenciaReporteBO();
                ocurrenciaReporte.Nombre = newOcurrencia.Nombre;
                ocurrenciaReporte.IdFaseOportunidad = newOcurrencia.IdFaseOportunidad.Value;
                ocurrenciaReporte.IdActividadCabecera = newOcurrencia.IdActividadCabecera;
                ocurrenciaReporte.IdPlantillaSpeech = newOcurrencia.IdPlantilla_Speech;
                ocurrenciaReporte.IdEstadoOcurrencia = newOcurrencia.IdEstadoOcurrencia.Value;
                ocurrenciaReporte.Oportunidad = newOcurrencia.Oportunidad.Value;
                ocurrenciaReporte.RequiereLlamada = newOcurrencia.RequiereLlamada;
                ocurrenciaReporte.Roles = newOcurrencia.Roles;
                ocurrenciaReporte.Color = newOcurrencia.Color;
                ocurrenciaReporte.Estado = true;
                ocurrenciaReporte.UsuarioCreacion = Objeto.Usuario;
                ocurrenciaReporte.UsuarioModificacion = Objeto.Usuario;
                ocurrenciaReporte.FechaCreacion = DateTime.Now;
                ocurrenciaReporte.FechaModificacion = DateTime.Now;

                if (!ocurrenciaReporte.HasErrors)
                    _repOcurrenciaReporte.Insert(ocurrenciaReporte);
                else
                    return BadRequest(ocurrenciaReporte.GetErrors(null));

                ConfiguracionLlamadaOcurrenciaRepositorio _repoConfLlamada = new ConfiguracionLlamadaOcurrenciaRepositorio();
                var configuracionesDesdeCliente = Objeto.configuraciones;
                for (int i = 0; i < configuracionesDesdeCliente.Count; ++i)
                {
                    if (configuracionesDesdeCliente[i].Id == 0) 
                    {

                        ConfiguracionLlamadaOcurrenciaBO NuevaConfiguracionLlamada = new ConfiguracionLlamadaOcurrenciaBO();
                        NuevaConfiguracionLlamada.IdOcurrencia = ocurrencia.Id;
                        NuevaConfiguracionLlamada.IdConectorOcurrenciaLlamada = configuracionesDesdeCliente[i].IdConectorOcurrenciaLlamada;
                        NuevaConfiguracionLlamada.NumeroLlamada = configuracionesDesdeCliente[i].NumeroLlamada;
                        NuevaConfiguracionLlamada.IdCondicionOcurrenciaLlamada = configuracionesDesdeCliente[i].IdCondicionOcurrenciaLlamada;
                        NuevaConfiguracionLlamada.IdFaseTiempoLlamada = configuracionesDesdeCliente[i].IdFaseTiempoLlamada;
                        NuevaConfiguracionLlamada.Duracion = configuracionesDesdeCliente[i].Duracion;
                        NuevaConfiguracionLlamada.FechaCreacion = DateTime.Now;
                        NuevaConfiguracionLlamada.FechaModificacion = DateTime.Now;
                        NuevaConfiguracionLlamada.UsuarioCreacion = Objeto.Usuario;
                        NuevaConfiguracionLlamada.UsuarioModificacion = Objeto.Usuario;
                        NuevaConfiguracionLlamada.Estado = true;
                        NuevaConfiguracionLlamada.IdMigracion = null;

                        _repoConfLlamada.Insert(NuevaConfiguracionLlamada);
                    }
                 
                }

                return Ok(ocurrencia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarOcurrencia([FromBody] OcurrenciaDTO Objeto)
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(contexto);
                OcurrenciaReporteRepositorio _repOcurrenciaReporte = new OcurrenciaReporteRepositorio(contexto);

                var ocurrencia = _repOcurrencia.GetBy(x => x.Id == Objeto.Id).FirstOrDefault();
                _repOcurrencia.Delete(ocurrencia.Id, Objeto.Usuario);

                var ocurrenciaReporte = _repOcurrenciaReporte.GetBy(x => x.Id == Objeto.Id).FirstOrDefault();
                if(ocurrenciaReporte != null)
					_repOcurrenciaReporte.Delete(ocurrencia.Id, Objeto.Usuario);

                ConfiguracionLlamadaOcurrenciaRepositorio repositorio = new ConfiguracionLlamadaOcurrenciaRepositorio();
                var configuracionesDesdeDB = repositorio.GetBy(x => x.IdOcurrencia == Objeto.Id).ToList();
                for (int i = 0; i < configuracionesDesdeDB.Count; ++i)
                {
                    repositorio.Delete(configuracionesDesdeDB[i].Id, Objeto.Usuario);
                }


                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		[Route("[Action]")]
		[HttpGet]
		public ActionResult ObtenerCombosOcurrencia()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio();
				PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
				ActividadCabeceraRepositorio _repActividadCabecera = new ActividadCabeceraRepositorio();
				var plantillaFase = _repPlantillaClaveValor.ObtenterPlantillaWhatsApp().OrderBy(w => w.Nombre);
                var plantillaCorreo = _repPlantillaClaveValor.ObtenterPlantillaCorreo().OrderBy(w => w.Nombre);
                var personalAreaTrabajo = _repPersonalAreaTrabajo.ObtenerAreaTrabajoPersonalNombre();
				var actividadCabecera = _repActividadCabecera.ObtenerTodoFiltro();
				return Ok(new { Plantilla = plantillaFase,PlantillaCorreo= plantillaCorreo, Area = personalAreaTrabajo, Actividad = actividadCabecera});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 20/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los tipos de ocurrencia
        /// </summary>
        /// <returns>Tipo de objeto que retorna la función</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTipoOcurrencia()
        {
            try
            {
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio();

                var Ocurrencia = _repOcurrencia.ObtenerTipoOcurrencia();
                return Ok(new { Result = "OK", Records = Ocurrencia });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class ValidadorOcurrenciaDTO : AbstractValidator<TOcurrencia>
    {
        public static ValidadorOcurrenciaDTO Current = new ValidadorOcurrenciaDTO();
        public ValidadorOcurrenciaDTO()
        {

            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");

            RuleFor(objeto => objeto.IdFaseOportunidad).NotEmpty().WithMessage("IdFaseOportunidad es Obligatorio");

            RuleFor(objeto => objeto.IdActividadCabecera).NotEmpty().WithMessage("IdActividadCabecera es Obligatorio");

            RuleFor(objeto => objeto.IdPlantillaSpeech).NotEmpty().WithMessage("IdPlantillaSpeech es Obligatorio");

            RuleFor(objeto => objeto.IdEstadoOcurrencia).NotEmpty().WithMessage("IdEstadoOcurrencia es Obligatorio");

            RuleFor(objeto => objeto.RequiereLlamada).NotEmpty().WithMessage("RequiereLlamada es Obligatorio")
                                                        .Length(1, 20).WithMessage("Nombre debe tener 1 caracter minimo y 20 maximo");

            RuleFor(objeto => objeto.Roles).NotEmpty().WithMessage("Roles es Obligatorio")
                                                      .Length(1, 50).WithMessage("Roles debe tener 1 caracter minimo y 50 maximo");

            RuleFor(objeto => objeto.Color).NotEmpty().WithMessage("Color es Obligatorio")
                                            .Length(1, 20).WithMessage("Color debe tener 1 caracter minimo y 20 maximo");


        }
        
    }
}
