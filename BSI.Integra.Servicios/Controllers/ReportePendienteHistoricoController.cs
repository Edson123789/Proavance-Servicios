using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static BSI.Integra.Servicios.Controllers.ReportePendienteHistoricoController;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReportePendienteHistorico")]
    [ApiController]
    public class ReportePendienteHistoricoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ReportePendienteHistoricoController (integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar(PanelControlMetaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportePendienteHistoricoRepositorio repReportePendienteHistoricoRep = new ReportePendienteHistoricoRepositorio(_integraDBContext);
                var correlativo = repReportePendienteHistoricoRep.ObtenerCorrelativo();
                using (TransactionScope scope = new TransactionScope())
                {
                    ReportePendienteHistoricoRepositorio reportePendienteHistoricoRepositorio = new ReportePendienteHistoricoRepositorio();

                    ReportePendienteHistoricoBO reportePendienteHistoricoBO = new ReportePendienteHistoricoBO();
                    reportePendienteHistoricoBO.TipoReporte = Json.Nombre;
                    reportePendienteHistoricoBO.TipoReporte = Json.Nombre;
                    reportePendienteHistoricoBO.Estado = true;
                    reportePendienteHistoricoBO.UsuarioCreacion = Json.Usuario;
                    reportePendienteHistoricoBO.UsuarioModificacion = Json.Usuario;
                    reportePendienteHistoricoBO.FechaCreacion = DateTime.Now;
                    reportePendienteHistoricoBO.FechaModificacion = DateTime.Now;
                
                    scope.Complete();
                }
                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar(PanelControlMetaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PanelControlMetaRepositorio panelControlMetaRepositorio = new PanelControlMetaRepositorio();

                PanelControlMetaBO panelControlMetaBO = panelControlMetaRepositorio.FirstById(Json.Id);
                panelControlMetaBO.Nombre = Json.Nombre;
                panelControlMetaBO.Meta = Json.Meta;
                panelControlMetaBO.UsuarioModificacion = Json.Usuario;
                panelControlMetaBO.FechaModificacion = DateTime.Now;
                
                return Ok(panelControlMetaRepositorio.Update(panelControlMetaBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar(PanelControlMetaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PanelControlMetaRepositorio panelControlMetaRepositorio = new PanelControlMetaRepositorio();
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (panelControlMetaRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = panelControlMetaRepositorio.Delete(Json.Id, Json.Usuario);
                    }
                    
                    scope.Complete();
                }
                return Ok(estadoEliminacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
