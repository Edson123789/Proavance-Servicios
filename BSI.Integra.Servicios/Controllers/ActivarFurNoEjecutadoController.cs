using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ActivarFurNoEjecutado")]
    public class ActivarFurNoEjecutadoController : Controller
    {

        private readonly integraDBContext _integraDBContext;
        public ActivarFurNoEjecutadoController(integraDBContext _integraDBContexto)
        {
            _integraDBContext = _integraDBContexto;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFursNoEjecutados()
        {
            try
            {
                FurRepositorio _repFurRep = new FurRepositorio(_integraDBContext);
                var Data = _repFurRep.ObtenerFursNoEjecutados();
                return Ok(Data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActivarFurNoEjecutado([FromBody] ListaFiltroDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    integraDBContext integraDB = new integraDBContext();
                    FurBO fur = new FurBO();
                    foreach (var objIdUsuario in json.ListaFiltro)
                    {
                        fur.ActivarFurNoEjecutado(objIdUsuario, integraDB);
                    }
                    scope.Complete();
                }
                string result = "ACTIVADOS CORRECTAMENTE";
                return Ok(new { result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}