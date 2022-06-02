using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: LlamadaWebphoneReinicioAsesor
    /// Autor: Jose Villena
    /// Fecha: 03/005/2020
    /// <summary>
    /// Gestiona todas la propiedades de la tabla t_LlamadaWebphoneReinicioAsesor
    /// </summary>
    [Route("api/LlamadaWebphoneReinicioAsesor")]
    public class LlamadaWebphoneReinicioAsesorController : Controller
    {
        public LlamadaWebphoneReinicioAsesorController()
        {
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.  
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        ///Obtiene Estado Reinicio Webphone
        /// </summary>
        /// <param name="IdPersonal">Id del Personal</param>
        /// <returns>Retorna valor estado reinicio: Bool</returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerEstadoReinicioWebphone(string IdPersonal)
        {
            try
            {
                int idpersonal = Convert.ToInt32(IdPersonal);
                LlamadaWebphoneReinicioAsesorRepositorio rep = new LlamadaWebphoneReinicioAsesorRepositorio();
                var valor = rep.ValidarReinicioWebphone(idpersonal);
                return Ok(new { estado = valor });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPanelReinicioWebphone()
        {
            try
            {
                LlamadaWebphoneReinicioAsesorRepositorio rep = new LlamadaWebphoneReinicioAsesorRepositorio();
                var personalVentas = rep.ObtnerPanelReinicioWebphone();
                return Ok(personalVentas);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosReinicioWebphone()
        {
            try
            {
                PersonalRepositorio rep = new PersonalRepositorio();
                var personalVentas = rep.ObtenerPersonalVentas();
                return Ok(personalVentas);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarLlamadaWebphoneReinicioAsesor([FromBody] LlamadaWebphoneReinicioAsesorDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                LlamadaWebphoneReinicioAsesorRepositorio repWebphone = new LlamadaWebphoneReinicioAsesorRepositorio();
                LlamadaWebphoneReinicioAsesorBO reinicioWebphone;
                if (repWebphone.Exist(x => x.IdPersonal == Json.IdPersonal))
                {
                    reinicioWebphone = repWebphone.FirstBy(x => x.IdPersonal == Json.IdPersonal);
                    reinicioWebphone.AplicaReinicio = Json.AplicaReinicio;
                    reinicioWebphone.UsuarioModificacion = Json.Usuario;
                    reinicioWebphone.FechaModificacion = DateTime.Now;
                    repWebphone.Update(reinicioWebphone);
                }
                else
                {
                    return BadRequest("No Existe Entidad");
                }

                return Ok(reinicioWebphone);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarLlamadaWebphoneReinicioAsesor([FromBody] LlamadaWebphoneReinicioAsesorDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                LlamadaWebphoneReinicioAsesorRepositorio repWebphone = new LlamadaWebphoneReinicioAsesorRepositorio();
                LlamadaWebphoneReinicioAsesorBO reinicioWebphone;
                if (repWebphone.Exist(x => x.IdPersonal == Json.IdPersonal))
                {
                    return BadRequest("Ya Existe el Personal");
                }
                else
                {
                    reinicioWebphone = new LlamadaWebphoneReinicioAsesorBO();
                    reinicioWebphone.IdPersonal = Json.IdPersonal;
                    reinicioWebphone.AplicaReinicio = Json.AplicaReinicio;
                    reinicioWebphone.UsuarioCreacion = Json.Usuario;
                    reinicioWebphone.UsuarioModificacion = Json.Usuario;
                    reinicioWebphone.FechaCreacion = DateTime.Now;
                    reinicioWebphone.FechaModificacion = DateTime.Now;
                    reinicioWebphone.Estado = true;
                    repWebphone.Insert(reinicioWebphone);
                }
              
               
                return Ok(reinicioWebphone);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
       
    }
  
    
}

