using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/ConstanciaAlumno")]
    [ApiController]
    public class ConstanciaAlumnoOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltrado(string TextoBuscar, int? IdRaTipoConstanciaAlumno, int? IdRaEstadoConstanciaAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaTipoConstanciaAlumnoRepositorio _repTipoConstanciaAlumno = new RaTipoConstanciaAlumnoRepositorio();
                RaEstadoConstanciaAlumnoRepositorio _repEstadoConstanciaAlumno = new RaEstadoConstanciaAlumnoRepositorio();
                RaConstanciaAlumnoRepositorio _repConstanciaAlumno = new RaConstanciaAlumnoRepositorio();

                List<RaConstanciaAlumnoBO> listadoConstancia = new List<RaConstanciaAlumnoBO>();
                listadoConstancia = _repConstanciaAlumno.GetBy(x => x.Estado == true).ToList();
                if (!string.IsNullOrEmpty(TextoBuscar))
                    listadoConstancia = listadoConstancia.Where(x => x.CodigoAlumno.ToUpper().Contains(TextoBuscar.Trim().ToUpper()) || x.Alumno.ToUpper().Contains(TextoBuscar.ToUpper()) || (x.Correlativo + "-" + x.Anho).Contains(TextoBuscar)).ToList();
                if (IdRaTipoConstanciaAlumno != null)
                {
                    if (_repTipoConstanciaAlumno.Exist(IdRaTipoConstanciaAlumno.Value))
                    {
                        listadoConstancia = listadoConstancia.Where(w => w.IdRaTipoConstanciaAlumno == IdRaTipoConstanciaAlumno.Value).ToList();
                    }
                }
                if (IdRaEstadoConstanciaAlumno != null)
                {
                    if (_repEstadoConstanciaAlumno.Exist(IdRaEstadoConstanciaAlumno.Value))
                    {
                        listadoConstancia = listadoConstancia.Where(w => w.IdRaEstadoConstanciaAlumno == IdRaEstadoConstanciaAlumno).ToList();
                    }
                }
                return Ok(listadoConstancia);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
