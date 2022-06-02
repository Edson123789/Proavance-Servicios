using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/WhatsAppPlantillaPorOcurrenciaActividad")]
    public class WhatsAppPlantillaPorOcurrenciaActividadController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public WhatsAppPlantillaPorOcurrenciaActividadController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarWhatsAppCorreoPlantillaPorOcurrenciaActividad([FromBody] InsertarWhatsAppPlantillaPorOcurrenciaActividadDTO Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //Whatsapp
                List<WhatsAppPlantillaPorOcurrenciaActividadBO> ListaWhatsAppPlantillaPorOcurrenciaActividad = new List<WhatsAppPlantillaPorOcurrenciaActividadBO>();
                WhatsAppPlantillaPorOcurrenciaActividadRepositorio _WhatsAppPlantillaPorOcurrenciaActividad = new WhatsAppPlantillaPorOcurrenciaActividadRepositorio(_integraDBContext);

                if (Obj.WhatsAppPlantillaPorOcurrenciaActividad.Count() != 0 && Obj.WhatsAppPlantillaPorOcurrenciaActividad != null)
                {
                    if (_WhatsAppPlantillaPorOcurrenciaActividad.Exist(w => w.IdOcurrenciaActividad == Obj.WhatsAppPlantillaPorOcurrenciaActividad[0].IdOcurrenciaActividad))
                    {
                        var ListaId = _WhatsAppPlantillaPorOcurrenciaActividad.GetBy(w => w.Estado == true && w.IdOcurrenciaActividad == Obj.WhatsAppPlantillaPorOcurrenciaActividad[0].IdOcurrenciaActividad, w => new { w.Id }).ToList();
                        _WhatsAppPlantillaPorOcurrenciaActividad.Delete(ListaId.Select(x => x.Id), Obj.Usuario);
                    }
                }
                else
                {
                    if (_WhatsAppPlantillaPorOcurrenciaActividad.Exist(w => w.IdOcurrenciaActividad == Obj.IdOcurrenciaActividad))
                    {
                        var ListaId = _WhatsAppPlantillaPorOcurrenciaActividad.GetBy(w => w.Estado == true && w.IdOcurrenciaActividad == Obj.IdOcurrenciaActividad, w => new { w.Id }).ToList();
                        _WhatsAppPlantillaPorOcurrenciaActividad.Delete(ListaId.Select(x => x.Id), Obj.Usuario);
                    }
                }
                foreach (var item in Obj.WhatsAppPlantillaPorOcurrenciaActividad)
                {
                    WhatsAppPlantillaPorOcurrenciaActividadBO WhatsAppPlantillaPorOcurrenciaActividad = new WhatsAppPlantillaPorOcurrenciaActividadBO();

                    WhatsAppPlantillaPorOcurrenciaActividad.IdOcurrenciaActividad = item.IdOcurrenciaActividad;
                    WhatsAppPlantillaPorOcurrenciaActividad.IdPlantilla = item.IdPlantilla;
                    WhatsAppPlantillaPorOcurrenciaActividad.NumeroDiasSinContacto = item.NumeroDiasSinContacto;
                    WhatsAppPlantillaPorOcurrenciaActividad.Estado = true;
                    WhatsAppPlantillaPorOcurrenciaActividad.FechaCreacion = DateTime.Now;
                    WhatsAppPlantillaPorOcurrenciaActividad.FechaModificacion = DateTime.Now;
                    WhatsAppPlantillaPorOcurrenciaActividad.UsuarioCreacion = Obj.Usuario;
                    WhatsAppPlantillaPorOcurrenciaActividad.UsuarioModificacion = Obj.Usuario;
                    ListaWhatsAppPlantillaPorOcurrenciaActividad.Add(WhatsAppPlantillaPorOcurrenciaActividad);
                }               

                _WhatsAppPlantillaPorOcurrenciaActividad.Insert(ListaWhatsAppPlantillaPorOcurrenciaActividad);


                //Correos

                List<CorreoPlantillaPorOcurrenciaActividadBO> ListaCorreoPlantillaPorOcurrenciaActividad = new List<CorreoPlantillaPorOcurrenciaActividadBO>();
                CorreoPlantillaPorOcurrenciaActividadRepositorio _CorreoPlantillaPorOcurrenciaActividad = new CorreoPlantillaPorOcurrenciaActividadRepositorio(_integraDBContext);

                if (Obj.CorreoPlantillaPorOcurrenciaActividad.Count() != 0 && Obj.CorreoPlantillaPorOcurrenciaActividad != null)
                {
                    if (_CorreoPlantillaPorOcurrenciaActividad.Exist(w => w.IdOcurrenciaActividad == Obj.CorreoPlantillaPorOcurrenciaActividad[0].IdOcurrenciaActividad))
                    {
                        var ListaId = _CorreoPlantillaPorOcurrenciaActividad.GetBy(w => w.Estado == true && w.IdOcurrenciaActividad == Obj.CorreoPlantillaPorOcurrenciaActividad[0].IdOcurrenciaActividad, w => new { w.Id }).ToList();
                        _CorreoPlantillaPorOcurrenciaActividad.Delete(ListaId.Select(x => x.Id), Obj.Usuario);
                    }
                }
                else
                {
                    if (_CorreoPlantillaPorOcurrenciaActividad.Exist(w => w.IdOcurrenciaActividad == Obj.IdOcurrenciaActividad))
                    {
                        var ListaId = _CorreoPlantillaPorOcurrenciaActividad.GetBy(w => w.Estado == true && w.IdOcurrenciaActividad == Obj.IdOcurrenciaActividad, w => new { w.Id }).ToList();
                        _CorreoPlantillaPorOcurrenciaActividad.Delete(ListaId.Select(x => x.Id), Obj.Usuario);
                    }
                }
                foreach (var item in Obj.CorreoPlantillaPorOcurrenciaActividad)
                {
                    CorreoPlantillaPorOcurrenciaActividadBO CorreoPlantillaPorOcurrenciaActividad = new CorreoPlantillaPorOcurrenciaActividadBO();

                    CorreoPlantillaPorOcurrenciaActividad.IdOcurrenciaActividad = item.IdOcurrenciaActividad;
                    CorreoPlantillaPorOcurrenciaActividad.IdPlantilla = item.IdPlantilla;
                    CorreoPlantillaPorOcurrenciaActividad.NumeroDiasSinContacto = item.NumeroDiasSinContacto;
                    CorreoPlantillaPorOcurrenciaActividad.Estado = true;
                    CorreoPlantillaPorOcurrenciaActividad.FechaCreacion = DateTime.Now;
                    CorreoPlantillaPorOcurrenciaActividad.FechaModificacion = DateTime.Now;
                    CorreoPlantillaPorOcurrenciaActividad.UsuarioCreacion = Obj.Usuario;
                    CorreoPlantillaPorOcurrenciaActividad.UsuarioModificacion = Obj.Usuario;
                    ListaCorreoPlantillaPorOcurrenciaActividad.Add(CorreoPlantillaPorOcurrenciaActividad);
                }

                _CorreoPlantillaPorOcurrenciaActividad.Insert(ListaCorreoPlantillaPorOcurrenciaActividad);

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [Route("[action]/{IdOcurrenciaActividad}")]
        [HttpGet]
        public ActionResult ObtenerPlantillaPorActividadOcurrencia(int IdOcurrenciaActividad)
        {
            WhatsAppPlantillaPorOcurrenciaActividadRepositorio _repWhatsAppPlantillaPorOcurrenciaActividad = new WhatsAppPlantillaPorOcurrenciaActividadRepositorio(_integraDBContext);

            var ListaWhatsAppPlantillaPorOcurrenciaActividad = _repWhatsAppPlantillaPorOcurrenciaActividad.ObtenerAsociacionWhatsAppPlantillaPorIdActividadOcurrencia(IdOcurrenciaActividad);

            return Ok(ListaWhatsAppPlantillaPorOcurrenciaActividad);
        }
    }
}
