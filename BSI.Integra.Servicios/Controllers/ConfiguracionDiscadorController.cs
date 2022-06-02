using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ConfiguracionDiscador
    /// Autor: Carlos Crispin
    /// Fecha: 25/01/2021        
    /// <summary>
    /// Controlador de Configuracion de Discador
    /// </summary>
    [Route("api/ConfiguracionDiscador")]
    public class ConfiguracionDiscadorController : Controller
    {
        
        private readonly integraDBContext _integraDBContext;
        public ConfiguracionDiscadorController(integraDBContext _integraDBContexto) {
            _integraDBContext = _integraDBContexto;
        }



        /// TipoFuncion: GET
        /// Autor: Carlos Crispin
        /// Fecha: 04/01/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de las configuraciones del discador
        /// </summary>
        /// <returns> Lista de Configuraciones : List<ConfiguracionDiscadorDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodoConfiguracionDiscador()
        {
            try
            {
                ConfiguracionDiscadorRepositorio _repConfiguracionDiscadorRep = new ConfiguracionDiscadorRepositorio();
                var data = _repConfiguracionDiscadorRep.ObtenerGrid();
                return Ok(new { Data = data});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Carlos Crispin
        /// Fecha: 04/01/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos para módulo
        /// </summary>
        /// <returns>Objeto Agrupado</returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repOperadorComparacion = new OperadorComparacionRepositorio(_integraDBContext);
                var _repEstadoOcurrencia = new EstadoOcurrenciaRepositorio(_integraDBContext);


                var listaOperadorComparacion = _repOperadorComparacion.ObtenerListaOperadorComparacion();
                var listaEstadoLlamada = _repEstadoOcurrencia.ObtenerEstadoOcurrenciasParaFiltro();


                return Ok(new
                {
                    ListaOperadorComparacion = listaOperadorComparacion,
                    ListaEstadoLlamada= listaEstadoLlamada
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarConfiguracionDiscador([FromBody] ConfiguracionDiscadorDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionDiscadorRepositorio repConfiguracionDiscadorRep = new ConfiguracionDiscadorRepositorio();

                var configuracionDiscador = new ConfiguracionDiscadorBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    configuracionDiscador.IdEstadoOcurrencia = Json.IdEstadoOcurrencia;
                    configuracionDiscador.ContestaLlamada = Json.ContestaLlamada;
                    configuracionDiscador.IdOperadorComparacionTimbradoSegundos = Json.IdOperadorComparacionTimbradoSegundos;
                    configuracionDiscador.TiempoTimbrado = Json.TiempoTimbrado;
                    configuracionDiscador.IdOperadorComparacionEfectivoSegundos = Json.IdOperadorComparacionEfectivoSegundos;
                    configuracionDiscador.TiempoEfectivo = Json.TiempoEfectivo;
                    configuracionDiscador.CantidadIntentosContacto = Json.CantidadIntentosContacto;
                    configuracionDiscador.TiempoEsperaLlamadaSegundos = Json.TiempoEsperaLlamadaSegundos;
                    configuracionDiscador.DesvioLlamada = Json.DesvioLlamada;
                    configuracionDiscador.BuzonVoz = Json.BuzonVoz;
                    configuracionDiscador.NoConectaLlamada = Json.NoConectaLlamada;
                    configuracionDiscador.TelefonoApagado = Json.TelefonoApagado;
                    configuracionDiscador.NumeroNoExiste = Json.NumeroNoExiste;
                    configuracionDiscador.NumeroSuspendido = Json.NumeroNoExiste;

                    configuracionDiscador.Estado = true;
                    configuracionDiscador.UsuarioCreacion = Json.Usuario;
                    configuracionDiscador.UsuarioModificacion = Json.Usuario;
                    configuracionDiscador.FechaCreacion = DateTime.Now;
                    configuracionDiscador.FechaModificacion = DateTime.Now;

                    repConfiguracionDiscadorRep.Insert(configuracionDiscador);

                    scope.Complete();
                }

                return Ok(Json);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarConfiguracionDiscador([FromBody] ConfiguracionDiscadorDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionDiscadorRepositorio repConfiguracionDiscadorRep = new ConfiguracionDiscadorRepositorio();

                var configuracionDiscador = repConfiguracionDiscadorRep.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    configuracionDiscador.IdEstadoOcurrencia = Json.IdEstadoOcurrencia;
                    configuracionDiscador.ContestaLlamada = Json.ContestaLlamada;
                    configuracionDiscador.IdOperadorComparacionTimbradoSegundos = Json.IdOperadorComparacionTimbradoSegundos;
                    configuracionDiscador.TiempoTimbrado = Json.TiempoTimbrado;
                    configuracionDiscador.IdOperadorComparacionEfectivoSegundos = Json.IdOperadorComparacionEfectivoSegundos;
                    configuracionDiscador.TiempoEfectivo = Json.TiempoEfectivo;
                    configuracionDiscador.CantidadIntentosContacto = Json.CantidadIntentosContacto;
                    configuracionDiscador.TiempoEsperaLlamadaSegundos = Json.TiempoEsperaLlamadaSegundos;
                    configuracionDiscador.DesvioLlamada = Json.DesvioLlamada;
                    configuracionDiscador.BuzonVoz = Json.BuzonVoz;
                    configuracionDiscador.NoConectaLlamada = Json.NoConectaLlamada;
                    configuracionDiscador.TelefonoApagado = Json.TelefonoApagado;
                    configuracionDiscador.NumeroNoExiste = Json.NumeroNoExiste;
                    configuracionDiscador.NumeroSuspendido = Json.NumeroNoExiste;
                    configuracionDiscador.UsuarioModificacion = Json.Usuario;
                    configuracionDiscador.FechaModificacion = DateTime.Now;

                    repConfiguracionDiscadorRep.Update(configuracionDiscador);

                    scope.Complete();
                }

                return Ok(Json);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarConfiguracionDiscador([FromBody] ConfiguracionDiscadorDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionDiscadorRepositorio repConfiguracionDiscadorRep = new ConfiguracionDiscadorRepositorio();
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    estadoEliminacion = repConfiguracionDiscadorRep.Delete(Json.Id, Json.Usuario);
                    scope.Complete();
                }

                return Ok(estadoEliminacion);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
