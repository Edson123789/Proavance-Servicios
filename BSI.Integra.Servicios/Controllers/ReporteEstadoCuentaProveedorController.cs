using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteEstadoCuentaProveedor")]
    public class ReporteEstadoCuentaProveedorController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public ReporteEstadoCuentaProveedorController(integraDBContext integraDBContext) {
            _integraDBContext = integraDBContext;
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaSedes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SedeRepositorio _repoSede = new SedeRepositorio();
                var Lista = _repoSede.ObtenerListaSedesSegunFur();
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaProveedores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProveedorRepositorio _repoProveedor = new ProveedorRepositorio();
                //var Lista = _repoProveedor.ObtenerProveedoresConEstadoCuentaPagadoPendiente();
                var Lista = _repoProveedor.ObtenerProveedorCombo();
                
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPlanContable(string NombreParcial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlanContableRepositorio _repoPlanContable = new PlanContableRepositorio();
                var Lista = _repoPlanContable.ObtenerPlanContableFiltro(NombreParcial);
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult VizualizarReporteEstadoCuentaProveedor([FromBody] ReporteEstadoCuentaProveedorFiltroDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurPagoRepositorio _repoFurPago = new FurPagoRepositorio(_integraDBContext);
                List<ReporteEstadoCuentaProveedorDTO> listaCrudo = _repoFurPago.GenerarReporteEstadoCuentaProveedor(Filtro.IdsSede, Filtro.IdsPlanContable, Filtro.IdsProveedor);

                bool FiltroPorFur = false;
                bool FiltroPorEstado = false;
                List<ReporteEstadoCuentaProveedorDTO> ListaFiltradaFur =  new List<ReporteEstadoCuentaProveedorDTO>();
                List<ReporteEstadoCuentaProveedorDTO> ListaFiltradaEstado = new List<ReporteEstadoCuentaProveedorDTO>();
                if (Filtro.CodigoFur != null && !Filtro.CodigoFur.Trim().Equals(""))
                {
                    FiltroPorFur = true;
                    ListaFiltradaFur = listaCrudo.Where(x=>x.CodigoFur.ToLower().Contains(Filtro.CodigoFur.ToLower())).ToList();
                    
                }

                if (Filtro.Estado != null && !Filtro.Estado.Trim().Equals(""))
                {
                    FiltroPorEstado = true;
                    if (FiltroPorFur)
                    {
                        ListaFiltradaEstado = ListaFiltradaFur.Where(x => x.Estado.ToLower().Equals(Filtro.Estado.ToLower())).ToList();
                        return Ok(ListaFiltradaFur);
                    } else
                    {
                        ListaFiltradaEstado = listaCrudo.Where(x => x.Estado.ToLower().Equals(Filtro.Estado.ToLower())).ToList();
                        
                    }
                    
                }

                if (FiltroPorFur && FiltroPorEstado) return Ok(ListaFiltradaEstado);
                else if (!FiltroPorFur && FiltroPorEstado) return Ok(ListaFiltradaEstado);
                else if (FiltroPorFur && !FiltroPorEstado) return Ok(ListaFiltradaFur);
                else  return Ok(listaCrudo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       
    }
}
