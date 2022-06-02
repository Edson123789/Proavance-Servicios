using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/LlamadaWebphoneCruceCentral")]
    public class LlamadaWebphoneCruceCentralController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        
        public LlamadaWebphoneCruceCentralController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarLlamadaWebphoneCruceCentral()
        {
            int? RegistroBloqueo = null;
            try
            {
                string listaIdLlamadas;
                LlamadaWebphoneCruceCentralRepositorio _repLlamadaWebphoneCruceCentral = new LlamadaWebphoneCruceCentralRepositorio(_integraDBContext);

                List<IdLlamadaWebphoneCruceCentralDTO> listaIdLlamadaWebphone = new List<IdLlamadaWebphoneCruceCentralDTO>();
                List<CentralCruceCentralDTO> listaLlamadaWebphoneCruceCentral = new List<CentralCruceCentralDTO>();
                LlamadaWebphoneCruceCentralBO primerRegistro = new LlamadaWebphoneCruceCentralBO();

                if (_repLlamadaWebphoneCruceCentral.BloqueoInsertar())
                {
                    return Ok(false);
                }
                
                RegistroBloqueo = _repLlamadaWebphoneCruceCentral.ObtenerUltimoRegistro();
                if (RegistroBloqueo == null)
                {
                    return Ok(false);
                }        

                listaIdLlamadaWebphone = _repLlamadaWebphoneCruceCentral.ObtenerListaIdLlamadaWebphone(RegistroBloqueo.Value);

                if (listaIdLlamadaWebphone.Count() == 0)
                {
                    return Ok(false);
                }

                var LlamadaWebphoneCruceCentral = _repLlamadaWebphoneCruceCentral.FirstById(RegistroBloqueo.Value);
                LlamadaWebphoneCruceCentral.Estado = false;

                _repLlamadaWebphoneCruceCentral.Update(LlamadaWebphoneCruceCentral);

                var regularizar1 = _repLlamadaWebphoneCruceCentral.RegularizarPorNumeroFecha();

                List<int> ListaId = listaIdLlamadaWebphone.Select(w => w.IdLlamadaWebphone).ToList();

                listaIdLlamadas = string.Join(",", ListaId);     

                _repLlamadaWebphoneCruceCentral.InsertarLlamadaWebphoneCruceCentral(listaIdLlamadas);
                
                var LiberarInsercion = _repLlamadaWebphoneCruceCentral.ObtenerLlamadaWebphoneCruceCentral(RegistroBloqueo.Value);
                LiberarInsercion.Estado = true;
                _repLlamadaWebphoneCruceCentral.Update(LiberarInsercion);

                return Ok(true);
            }
            catch (Exception Ex)
            {
                if (RegistroBloqueo != null)
                {
                    LlamadaWebphoneCruceCentralRepositorio _repLlamadaWebphoneCruceCentral2 = new LlamadaWebphoneCruceCentralRepositorio(_integraDBContext);
                    var LiberarInsercion = _repLlamadaWebphoneCruceCentral2.ObtenerLlamadaWebphoneCruceCentral(RegistroBloqueo.Value);
                    LiberarInsercion.Estado = true;
                    _repLlamadaWebphoneCruceCentral2.Update(LiberarInsercion);
                }     
                return BadRequest(Ex.Message);
            }
        }
    }
    public class ValidadorLlamadaWebphoneCruceCentralDTO : AbstractValidator<LlamadaWebphoneCruceCentralBO>
    {
        public static ValidadorLlamadaWebphoneCruceCentralDTO Current = new ValidadorLlamadaWebphoneCruceCentralDTO();
        public ValidadorLlamadaWebphoneCruceCentralDTO()
        {

        }
    }
}
