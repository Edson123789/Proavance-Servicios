using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PanelDepositoDisponible")]
    public class PanelDepositoDisponibleController : Controller
    {
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerDiaSemana()
        {
            try
            {
                DiaSemanaRepositorio _repDiaSemanaRep = new DiaSemanaRepositorio();
                return Ok(_repDiaSemanaRep.ObtenerDiaSemana());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPanelDepositoDisponible()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PanelIngresoDisponibleRepositorio repPanelIngresoRep = new PanelIngresoDisponibleRepositorio();
                var Records = repPanelIngresoRep.ObtenerPanelDepositoDisponible();
                var Total = Records.Count();
                return Ok(new { Records, Total });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarPanelDepositoDisponible([FromBody] EliminarDTO eliminarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();
                using (TransactionScope scope = new TransactionScope())
                {
                    PanelIngresoDisponibleRepositorio repPanelDepositoRep = new PanelIngresoDisponibleRepositorio(_integraDBContext);
                    if (repPanelDepositoRep.Exist(eliminarDTO.Id))
                    {
                        repPanelDepositoRep.Delete(eliminarDTO.Id, eliminarDTO.NombreUsuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarPanelDepositoDisponible([FromBody] PanelDepositoDisponibleDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PanelIngresoDisponibleRepositorio repPanelDepositoRep = new PanelIngresoDisponibleRepositorio();
                PanelIngresoDisponibleBO panelDeposito = new PanelIngresoDisponibleBO    ();

                using (TransactionScope scope = new TransactionScope())
                {
                    panelDeposito.IdFormaPago = Json.IdFormaPago;
                    panelDeposito.DiasDeposito = Json.DiasDeposito;
                    panelDeposito.DiasDisponible = Json.DiasDisponible;
                    panelDeposito.CuentaFeriados = Json.CuentaFeriados;
                    panelDeposito.CuentaFeriadosEstatales = Json.CuentaFeriadosEstatales;
                    panelDeposito.ConsideraVsd = Json.ConsideraVSD;
                    panelDeposito.ConsideraDiasHabilesLunesViernes = Json.ConsideraDiasHabilesLunesViernes;
                    panelDeposito.ConsideraDiasHabilesLunesSabado = Json.ConsideraDiasHabilesLunesSabado;
                    panelDeposito.ConsideraDiasFijoSemana = Json.ConsideraDiasFijoSemana;
                    panelDeposito.IdDiaSemanaFijo = Json.ConsideraDiasFijoSemana==true? Json.IdDiaSemanaFijo:null;
                    panelDeposito.HoraCorte = Json.HoraCorte;
                    panelDeposito.MinutoCorte = Json.MinutoCorte;
                    panelDeposito.PorcentajeCobro = Json.PorcentajeCobro;
                    panelDeposito.Estado = true;
                    panelDeposito.UsuarioCreacion = Json.UsuarioModificacion;
                    panelDeposito.FechaCreacion = DateTime.Now;
                    panelDeposito.UsuarioModificacion = Json.UsuarioModificacion;
                    panelDeposito.FechaModificacion = DateTime.Now;

                    repPanelDepositoRep.Insert(panelDeposito);
                    scope.Complete();
                }
                Json.Id = panelDeposito.Id;
                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarPanelDepositoDisponible([FromBody] PanelDepositoDisponibleDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PanelIngresoDisponibleRepositorio repPanelDepositoRep = new PanelIngresoDisponibleRepositorio();
                PanelIngresoDisponibleBO panelDeposito = new PanelIngresoDisponibleBO();
                panelDeposito = repPanelDepositoRep.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    panelDeposito.IdFormaPago = Json.IdFormaPago;
                    panelDeposito.DiasDeposito = Json.DiasDeposito;
                    panelDeposito.DiasDisponible = Json.DiasDisponible;
                    panelDeposito.CuentaFeriados = Json.CuentaFeriados;
                    panelDeposito.CuentaFeriadosEstatales = Json.CuentaFeriadosEstatales;
                    panelDeposito.ConsideraVsd = Json.ConsideraVSD;
                    panelDeposito.ConsideraDiasHabilesLunesViernes = Json.ConsideraDiasHabilesLunesViernes;
                    panelDeposito.ConsideraDiasHabilesLunesSabado = Json.ConsideraDiasHabilesLunesSabado;
                    panelDeposito.ConsideraDiasFijoSemana = Json.ConsideraDiasFijoSemana;
                    panelDeposito.IdDiaSemanaFijo = Json.ConsideraDiasFijoSemana == true ? Json.IdDiaSemanaFijo : null;
                    panelDeposito.HoraCorte = Json.HoraCorte;
                    panelDeposito.MinutoCorte = Json.MinutoCorte;
                    panelDeposito.PorcentajeCobro = Json.PorcentajeCobro;
                    panelDeposito.UsuarioModificacion = Json.UsuarioModificacion;
                    panelDeposito.FechaModificacion = DateTime.Now;
                    repPanelDepositoRep.Update(panelDeposito);
                    scope.Complete();
                }
                Json.IdDiaSemanaFijo = panelDeposito.IdDiaSemanaFijo;
                return Ok(Json);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}