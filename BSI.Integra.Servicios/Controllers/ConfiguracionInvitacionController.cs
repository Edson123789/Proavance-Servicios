using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ConfiguracionInvitacion")]
    
    public class ConfiguracionInvitacionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PespecificoRepositorio _repPespecifico;
        private readonly TiempoFrecuenciaRepositorio _repTiempoFrecuencia;
        private readonly PlantillaRepositorio _repPlantilla;


        public ConfiguracionInvitacionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repPespecifico = new PespecificoRepositorio(_integraDBContext);
            _repTiempoFrecuencia = new TiempoFrecuenciaRepositorio(_integraDBContext);
            _repPlantilla = new PlantillaRepositorio(_integraDBContext);
        }    

        
        
        [HttpPost]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var comboPEspecifico = _repPespecifico.ObtenerListaProgramasOnline();
                var comboTiempoFrecuencia = _repTiempoFrecuencia.ObtenerListaTiempoFrecuencia();
                //var comboPlantilla = _repPlantilla.

                return Ok(comboPEspecifico);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}
