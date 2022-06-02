using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteSeguimientoComisiones")]
    public class ReporteSeguimientoComisionesController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public ReporteSeguimientoComisionesController(integraDBContext integraDBContext) {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaSubEstados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repoReportes = new ReportesRepositorio();
                var Lista = _repoReportes.ObtenerListaSubEstadosParaSeguimientoComisiones();
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCoordinadoresVentas() 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repoPersonal = new PersonalRepositorio();
                var Lista = _repoPersonal.ObtenerPersonalCoordinadoresFiltro();
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPersonalVentas() //ObtenerPersonalVentas
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repoPersonal = new PersonalRepositorio();
                var Lista = _repoPersonal.ObtenerPersonalVentas();
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPorcentajesComision()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repoReportes = new ReportesRepositorio();
                var Lista = _repoReportes.ObtenerListaPorcentajesComision();
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarComisionMontoPago([FromBody] FiltroComisionMontoPagoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                ComisionMontoPagoRepositorio _repoComisionMontoPago = new ComisionMontoPagoRepositorio(_integraDBContext);
                MatriculaCabeceraControlCondicionesComisionRepositorio _repoMatriculaCabeceraControlCondicionesComision = new MatriculaCabeceraControlCondicionesComisionRepositorio(_integraDBContext);
                List<ComisionMontoPagoBO> ComisionMontoPago = _repoComisionMontoPago.GetBy(x => x.IdMatriculaCabecera==ObjetoDTO.IdMatriculaCabecera).ToList();
                
                // eliminar registros anteriores
                for(int i =0; i<ComisionMontoPago.Count; ++i)
                {
                    ComisionMontoPago[i].Estado = false;
                    ComisionMontoPago[i].UsuarioModificacion = ObjetoDTO.Usuario;
                    ComisionMontoPago[i].FechaModificacion = DateTime.Now;
                }

                _repoComisionMontoPago.Update(ComisionMontoPago);

                if (ObjetoDTO.EsComisionable) // datos que se marcaron como "NO COMISIONABLES" no tienen porque volver a generar nuevo registro en "ComisionMontoPago"
                {

                    //crear registros nuevos
                    ComisionMontoPago.Clear();
                    for (int i = 0; i < ObjetoDTO.ComisionMontoPagos.Count; ++i)
                    {
                        ComisionMontoPagoBO NuevaComisionMontoPago = new ComisionMontoPagoBO();
                        NuevaComisionMontoPago.IdMatriculaCabecera = ObjetoDTO.ComisionMontoPagos[i].IdMatriculaCabecera;
                        NuevaComisionMontoPago.IdPersonal = ObjetoDTO.ComisionMontoPagos[i].IdPersonal;
                        NuevaComisionMontoPago.MontoComision = ObjetoDTO.ComisionMontoPagos[i].MontoComision;
                        NuevaComisionMontoPago.IdMoneda = ObjetoDTO.ComisionMontoPagos[i].IdMoneda;
                        NuevaComisionMontoPago.IdComercialTipoPersonal = ObjetoDTO.ComisionMontoPagos[i].IdComercialTipoPersonal;
                        NuevaComisionMontoPago.IdComisionEstadoPago = ObjetoDTO.ComisionMontoPagos[i].IdComisionEstadoPago;
                        NuevaComisionMontoPago.Observacion = ObjetoDTO.ComisionMontoPagos[i].Observacion;
                        NuevaComisionMontoPago.Estado = true;
                        NuevaComisionMontoPago.UsuarioCreacion = ObjetoDTO.Usuario;
                        NuevaComisionMontoPago.UsuarioModificacion = ObjetoDTO.Usuario;
                        NuevaComisionMontoPago.FechaCreacion = DateTime.Now;
                        NuevaComisionMontoPago.FechaModificacion = DateTime.Now;

                        ComisionMontoPago.Add(NuevaComisionMontoPago);
                    }

                    _repoComisionMontoPago.Insert(ComisionMontoPago);
                }

                MatriculaCabeceraControlCondicionesComisionBO MatriculaCabeceraControlCondicionesComision = _repoMatriculaCabeceraControlCondicionesComision.GetBy(x => x.IdMatriculaCabecera == ObjetoDTO.IdMatriculaCabecera).FirstOrDefault();
                bool registroNuevo = false;
                if (MatriculaCabeceraControlCondicionesComision == null)
                {
                    MatriculaCabeceraControlCondicionesComision = new MatriculaCabeceraControlCondicionesComisionBO();
                    registroNuevo = true;
                }

                if(registroNuevo)
                {

                    MatriculaCabeceraControlCondicionesComision.IdMatriculaCabecera = ObjetoDTO.IdMatriculaCabecera;
                    MatriculaCabeceraControlCondicionesComision.TieneDocumento = ObjetoDTO.TieneDocumento;
                    MatriculaCabeceraControlCondicionesComision.TieneMatriculaPagada = ObjetoDTO.TieneMatriculaPagada;
                    MatriculaCabeceraControlCondicionesComision.TieneAsistencia = ObjetoDTO.TieneAsistencia;
                    MatriculaCabeceraControlCondicionesComision.EsComisionable = ObjetoDTO.EsComisionable;
                    MatriculaCabeceraControlCondicionesComision.Estado = true;
                    MatriculaCabeceraControlCondicionesComision.FechaCreacion = DateTime.Now;
                    MatriculaCabeceraControlCondicionesComision.FechaModificacion = DateTime.Now;
                    MatriculaCabeceraControlCondicionesComision.UsuarioCreacion = ObjetoDTO.Usuario;
                    MatriculaCabeceraControlCondicionesComision.UsuarioModificacion = ObjetoDTO.Usuario;
                    _repoMatriculaCabeceraControlCondicionesComision.Insert(MatriculaCabeceraControlCondicionesComision);
                } 
                else
                {
                    MatriculaCabeceraControlCondicionesComision.TieneDocumento = ObjetoDTO.TieneDocumento;
                    MatriculaCabeceraControlCondicionesComision.TieneMatriculaPagada = ObjetoDTO.TieneMatriculaPagada;
                    MatriculaCabeceraControlCondicionesComision.TieneAsistencia = ObjetoDTO.TieneAsistencia;
                    MatriculaCabeceraControlCondicionesComision.EsComisionable = ObjetoDTO.EsComisionable;
                    MatriculaCabeceraControlCondicionesComision.FechaModificacion = DateTime.Now;
                    MatriculaCabeceraControlCondicionesComision.UsuarioModificacion = ObjetoDTO.Usuario;
                    _repoMatriculaCabeceraControlCondicionesComision.Update(MatriculaCabeceraControlCondicionesComision);
                }



                return Ok(ComisionMontoPago);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerReporteSeguimientoComisiones([FromBody] FiltroReporteSeguimientoComisionesDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repoReportes = new ReportesRepositorio();

                if (ObjetoDTO.IdEstadoComision != 1)
                {
                    var Lista = _repoReportes.ObtenerDatosReporteSeguimientoComisiones(ObjetoDTO.ListaAsesores, ObjetoDTO.FechaInicio, ObjetoDTO.FechaFin, ObjetoDTO.IdEstadoComision);
                    return Ok(Lista);
                } 
                else
                {
                    var Lista = _repoReportes.ObtenerDatosReporteSeguimientoComisionables(ObjetoDTO.ListaAsesores, ObjetoDTO.FechaInicio, ObjetoDTO.FechaFin, ObjetoDTO.IdSubEstadoComision);
                    return Ok(Lista);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerReporteComisiones([FromBody] FiltroReporteComisionesDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repoReportes = new ReportesRepositorio();
                var Lista = _repoReportes.ObtenerReporteComisiones(ObjetoDTO.IdsAsesores, ObjetoDTO.FechaInicio, ObjetoDTO.FechaFin, ObjetoDTO.IdSubEstado);
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 25/01/2021
        /// <summary>
        /// Actualiza el estao de las comisiones a pagado
        /// </summary>
        /// <returns>boolean</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarReporteComisiones([FromBody] FiltroReporteComisionesDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repoReportes = new ReportesRepositorio();
                var Lista = _repoReportes.ActualizarReporteComisiones( ObjetoDTO.FechaInicio, ObjetoDTO.FechaFin);
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
