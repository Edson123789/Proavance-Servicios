using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/LlamadaWebphone")]
    public class LlamadaWebphoneController : BaseController<LlamadaWebphoneBO, ValidadorLlamadaWebphoneDTO>
    {
        public LlamadaWebphoneController(IIntegraRepository<LlamadaWebphoneBO> repositorio, ILogger<BaseController<LlamadaWebphoneBO, ValidadorLlamadaWebphoneDTO>> logger, IIntegraRepository<TLog> logrepositorio) : base(repositorio, logger, logrepositorio)
        {
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarLlamadaWebphonePanel([FromBody] LlamadaWebphoneDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                LlamadaWebphoneRepositorio _repWebphone = new LlamadaWebphoneRepositorio();
                LlamadaWebphoneBO llamada = new LlamadaWebphoneBO();
                llamada.FechaInicio = DateTime.Now;
                llamada.FechaFin = Json.FechaFin;
                llamada.Anexo = Json.Anexo;
                llamada.TelefonoDestino = Json.TelefonoDestino;
                llamada.IdPersonalAreaTrabajo = Json.IdPersonalAreaTrabajo.Value;
                llamada.IdLlamadasWebphoneTipo = Json.IdLlamadasWebphoneTipo;
                llamada.DuracionTimbrado = Json.DuracionTimbrado;
                llamada.DuracionContesto = Json.DuracionContesto;
                llamada.WebPhoneId = Json.WebPhoneId;
                llamada.IdAlumno = Json.IdAlumno.Value;
                llamada.IdActividadDetalle = Json.IdActividadDetalle.Value;
                llamada.Estado = true;
                llamada.FechaCreacion = DateTime.Now;
                llamada.FechaModificacion = DateTime.Now;
                llamada.UsuarioCreacion =Json.Usuario;
                llamada.UsuarioModificacion = Json.Usuario;
                _repWebphone.Insert(llamada);
                return Ok(llamada.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarLlamadaWebphonePanel([FromBody] LlamadaWebphoneDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                LlamadaWebphoneRepositorio _repWebphone = new LlamadaWebphoneRepositorio(contexto);
                LlamadaWebphoneEstadoRepositorio _repLlamadaEstado = new LlamadaWebphoneEstadoRepositorio(contexto);

                using (TransactionScope scope = new TransactionScope())
                {
                    var estadoLlamada = _repLlamadaEstado.FirstBy(x => x.Nombre == Json.LlamadasWebphoneEstado);
                    if (estadoLlamada == null)
                    {
                        LlamadaWebphoneEstadoBO newLlamada = new LlamadaWebphoneEstadoBO();
                        newLlamada.Nombre = Json.LlamadasWebphoneEstado;
                        newLlamada.Estado = true;
                        newLlamada.FechaCreacion = DateTime.Now;
                        newLlamada.FechaModificacion = DateTime.Now;
                        newLlamada.UsuarioCreacion = Json.Usuario;
                        newLlamada.UsuarioModificacion = Json.Usuario;
                        _repLlamadaEstado.Insert(newLlamada);

                        var llamada = _repWebphone.FirstById(Json.Id);
                        llamada.FechaFin = DateTime.Now;
                        llamada.DuracionTimbrado = Json.DuracionTimbrado;
                        llamada.DuracionContesto = Json.DuracionContesto;
                        llamada.WebPhoneId = Json.WebPhoneId;
                        llamada.IdLlamadasWebphoneEstado = newLlamada.Id;
                        llamada.Anexo = Json.Anexo;
                        llamada.FechaModificacion = DateTime.Now;
                        llamada.UsuarioModificacion = Json.Usuario;
                        llamada.NombreGrabacion = Json.NombreGrabacion.Replace("[","").Replace("]","");
                        _repWebphone.Update(llamada);
                    }
                    else
                    {
                        var llamada = _repWebphone.FirstById(Json.Id);
                        llamada.FechaFin = DateTime.Now;
                        llamada.DuracionTimbrado = Json.DuracionTimbrado;
                        llamada.DuracionContesto = Json.DuracionContesto;
                        llamada.WebPhoneId = Json.WebPhoneId;
                        llamada.IdLlamadasWebphoneEstado = estadoLlamada.Id;
                        llamada.Anexo = Json.Anexo;
                        llamada.FechaModificacion = DateTime.Now;
                        llamada.UsuarioModificacion = Json.Usuario;
                        llamada.NombreGrabacion = Json.NombreGrabacion.Replace("[", "").Replace("]", "");
                        _repWebphone.Update(llamada);
                    }
                   
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SubirAudio")]
        public async Task<IActionResult> Post([FromForm]  List<IFormFile> file)
        {
            try
            {
                long size = file.Sum(f => f.Length);

                // full path to file in temp location
                //var filePath = Path.GetTempFileName();
                //var filePath = "C:\\Users\\sistemas\\Desktop\\llamadas";
                var filePath = "E:\\llamadas";

                foreach (var formFile in file)
                {
                    if (formFile.Length > 0)
                    {
                        var filep = Path.Combine(filePath, formFile.FileName);
                        using (var stream = new FileStream(filep, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }
                return Ok(new { count = file.Count, size, filePath });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult RegularizarLlamadas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                LlamadaWebphoneRepositorio _repWebphone = new LlamadaWebphoneRepositorio(contexto);
                var lista = _repWebphone.ObtenerActividadesSinLlamadas();
                List<RegularizacionLLamadaWebphoneGrupoDTO> actividades = new List<RegularizacionLLamadaWebphoneGrupoDTO>();

                actividades = (from p in lista
                               group p by new
                               {
                                   p.IdPersonal,
                                   p.Anexo3cx

                               } into g
                               select new RegularizacionLLamadaWebphoneGrupoDTO
                               {
                                   IdPersonal = g.Key.IdPersonal,
                                   Anexo3cx = g.Key.Anexo3cx,

                                   Lista = g.Select(o => new RegularizacionLLamadaWebphoneDTO
                                   {
                                       Celular = o.Celular,
                                       FechaReal = o.FechaReal,
                                       IdActividad = o.IdActividad,
                                       IdAlumno = o.IdAlumno
                                   }).OrderByDescending(x => x.FechaReal).ToList(),
                               }).ToList();

                foreach (var item in actividades)
                {
                    int contador = 0;
                    var primeraActividad = item.Lista.FirstOrDefault();
                    foreach (var subItem in item.Lista)
                    {
                        contador = contador + 1;
                        if (contador > 1)
                        {
                            List<LlamadaRegularizacionDTO> llamadas = new List<LlamadaRegularizacionDTO>();
                            if (contador == item.Lista.Count())
                            {
                                var fechaFin = subItem.FechaReal.AddMinutes(-5);
                                llamadas = _repWebphone.ObtenerLlamadasPorFecha(fechaFin,subItem.FechaReal, item.Anexo3cx);
                                if (llamadas.Count() >= 4)
                                {
                                    llamadas = llamadas.Where(x => x.TelefonoDestino.Contains(primeraActividad.Celular)).ToList();
                                }
                                foreach (var Json in llamadas)
                                {
                                    LlamadaWebphoneBO llamada = new LlamadaWebphoneBO();
                                    llamada.FechaInicio = Json.FechaInicioLlamada;
                                    llamada.FechaFin = Json.FechaFinLlamada;
                                    llamada.Anexo = item.Anexo3cx;
                                    llamada.TelefonoDestino = Json.TelefonoDestino;
                                    llamada.IdPersonalAreaTrabajo = 8;
                                    llamada.IdLlamadasWebphoneTipo = 1;
                                    llamada.DuracionTimbrado = Json.TiempoTimbrado;
                                    llamada.DuracionContesto = Json.TiempoContesto;
                                    llamada.WebPhoneId = "";
                                    llamada.IdLlamadasWebphoneEstado = 1;
                                    llamada.IdAlumno = primeraActividad.IdAlumno;
                                    llamada.IdActividadDetalle = primeraActividad.IdActividad;
                                    llamada.Estado = true;
                                    llamada.FechaCreacion = Json.FechaInicioLlamada;
                                    llamada.FechaModificacion = Json.FechaFinLlamada;
                                    llamada.UsuarioCreacion = "Regularizacion";
                                    llamada.UsuarioModificacion = "Regularizacion";
                                    _repWebphone.Insert(llamada);
                                }

                                primeraActividad = subItem;
                            }
                            else
                            {
                                llamadas = _repWebphone.ObtenerLlamadasPorFecha(subItem.FechaReal, primeraActividad.FechaReal, item.Anexo3cx);
                                if (llamadas.Count() >= 4)
                                {
                                    llamadas = llamadas.Where(x => x.TelefonoDestino.Contains(primeraActividad.Celular)).ToList();
                                }
                                foreach (var Json in llamadas)
                                {
                                    LlamadaWebphoneBO llamada = new LlamadaWebphoneBO();
                                    llamada.FechaInicio = Json.FechaInicioLlamada;
                                    llamada.FechaFin = Json.FechaFinLlamada;
                                    llamada.Anexo = item.Anexo3cx;
                                    llamada.TelefonoDestino = Json.TelefonoDestino;
                                    llamada.IdPersonalAreaTrabajo = 8;
                                    llamada.IdLlamadasWebphoneTipo = 1;
                                    llamada.DuracionTimbrado = Json.TiempoTimbrado;
                                    llamada.DuracionContesto = Json.TiempoContesto;
                                    llamada.WebPhoneId = "";
                                    llamada.IdLlamadasWebphoneEstado = 1;
                                    llamada.IdAlumno = primeraActividad.IdAlumno;
                                    llamada.IdActividadDetalle = primeraActividad.IdActividad;
                                    llamada.Estado = true;
                                    llamada.FechaCreacion = Json.FechaInicioLlamada;
                                    llamada.FechaModificacion = Json.FechaFinLlamada;
                                    llamada.UsuarioCreacion = "Regularizacion";
                                    llamada.UsuarioModificacion = "Regularizacion";
                                    _repWebphone.Insert(llamada);
                                }
                                primeraActividad = subItem;
                            }
                        }


                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

    public class ValidadorLlamadaWebphoneDTO : AbstractValidator<LlamadaWebphoneBO>
    {
        public static ValidadorLlamadaWebphoneDTO Current = new ValidadorLlamadaWebphoneDTO();
        public ValidadorLlamadaWebphoneDTO()
        {

        }
    }
}
