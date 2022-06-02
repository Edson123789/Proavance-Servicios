using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Comercial;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: GestionRemuneracionPuestoTrabajo
    /// Autor: ---, Jashin Salazar
    /// Fecha: 07/12/2021
    /// <summary>
    /// Contiene los endpoints para la interfaz (O) Compensacion por puesto de trabajo
    /// </summary>
    [Route("api/SemaforoFinanciero")]
    [ApiController]
    public class SemaforoFinancieroController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        private readonly PaisRepositorio _repPais;
        public SemaforoFinancieroController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repPais = new PaisRepositorio(_integraDBContext);
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los puestos de trabajo registrados en el sistema.
        /// </summary>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerSemaforosFinancierosRegistrados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                SemaforoFinancieroRepositorio _repSemaforo = new SemaforoFinancieroRepositorio(_integraDBContext);
                return Ok(_repSemaforo.GetAll().ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene detalle de los puestos de trabajo
        /// </summary>
		/// <param name="IdSemaforoFinanciero">DTO enviado desde la interfaz<</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerSemaforoFinancieroDetalle([FromBody] int IdSemaforoFinanciero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                SemaforoFinancieroDetalleRepositorio _repSemFinDetalle = new SemaforoFinancieroDetalleRepositorio(_integraDBContext);
                return Ok(_repSemFinDetalle.GetBy(x=>x.IdSemaforoFinanciero== IdSemaforoFinanciero).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta nuevo semaforo
        /// </summary>
		/// <param name="Semaforo">DTO enviado desde la interfaz<</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarSemaforoFinanciero([FromBody] SemaforoFinancieroDTO Semaforo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SemaforoFinancieroBO semaforoNuevo = new SemaforoFinancieroBO();
                SemaforoFinancieroDetalleBO semaforoDetalleNuevo = new SemaforoFinancieroDetalleBO();
                SemaforoFinancieroRepositorio _repSemaforo = new SemaforoFinancieroRepositorio(_integraDBContext);
                SemaforoFinancieroDetalleRepositorio _repSemaforoDetalle = new SemaforoFinancieroDetalleRepositorio(_integraDBContext);

                semaforoNuevo.IdPais = Semaforo.IdPais;
                semaforoNuevo.Activo = Semaforo.Activo;
                semaforoNuevo.Estado = true;
                semaforoNuevo.UsuarioCreacion = Semaforo.Usuario;
                semaforoNuevo.UsuarioModificacion = Semaforo.Usuario;
                semaforoNuevo.FechaCreacion = DateTime.Now;
                semaforoNuevo.FechaModificacion = DateTime.Now;
                _repSemaforo.Insert(semaforoNuevo);

                foreach(var item in Semaforo.Detalle)
                {
                    semaforoDetalleNuevo.IdSemaforoFinanciero = semaforoNuevo.Id;
                    semaforoDetalleNuevo.Nombre = item.Nombre;
                    semaforoDetalleNuevo.Mensaje = item.Mensaje;
                    semaforoDetalleNuevo.Color = item.Color;
                    semaforoDetalleNuevo.Estado = true;
                    semaforoDetalleNuevo.UsuarioCreacion = Semaforo.Usuario;
                    semaforoDetalleNuevo.UsuarioModificacion = Semaforo.Usuario;
                    semaforoDetalleNuevo.FechaCreacion = DateTime.Now;
                    semaforoDetalleNuevo.FechaModificacion = DateTime.Now;
                    _repSemaforoDetalle.Insert(semaforoDetalleNuevo);
                    semaforoDetalleNuevo.Id = 0;
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza semaforo financiero
        /// </summary>
		/// <param name="Semaforo">DTO enviado desde la interfaz<</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarSemaforoFinanciero([FromBody] SemaforoFinancieroDTO Semaforo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SemaforoFinancieroBO semaforoNuevo = new SemaforoFinancieroBO();
                
                SemaforoFinancieroRepositorio _repSemaforo = new SemaforoFinancieroRepositorio(_integraDBContext);
                SemaforoFinancieroDetalleRepositorio _repSemaforoDetalle = new SemaforoFinancieroDetalleRepositorio(_integraDBContext);

                semaforoNuevo= _repSemaforo.FirstById(Semaforo.Id);
                semaforoNuevo.IdPais = Semaforo.IdPais;
                semaforoNuevo.Activo = Semaforo.Activo;
                semaforoNuevo.UsuarioModificacion = Semaforo.Usuario;
                semaforoNuevo.FechaModificacion = DateTime.Now;
                _repSemaforo.Update(semaforoNuevo);
                if (Semaforo.Detalle != null)
                {
                    //Borramos los que no estan
                    var detalleBD = _repSemaforoDetalle.GetBy(x=>x.IdSemaforoFinanciero==semaforoNuevo.Id);
                    foreach (var item in detalleBD)
                    {
                        if (!Semaforo.Detalle.Any(x => x.Id == item.Id))
                        {
                            _repSemaforoDetalle.Delete(item.Id, semaforoNuevo.UsuarioModificacion);
                        }
                    }

                    foreach (var item in Semaforo.Detalle)
                    {
                        SemaforoFinancieroDetalleBO semaforoDetalleNuevo = new SemaforoFinancieroDetalleBO();
                        if (item.Id>0)
                        {   
                            
                            semaforoDetalleNuevo= _repSemaforoDetalle.FirstById(item.Id);
                            semaforoDetalleNuevo.IdSemaforoFinanciero = semaforoNuevo.Id;
                            semaforoDetalleNuevo.Nombre = item.Nombre;
                            semaforoDetalleNuevo.Mensaje = item.Mensaje;
                            semaforoDetalleNuevo.Color = item.Color;
                            semaforoDetalleNuevo.UsuarioModificacion = Semaforo.Usuario;
                            semaforoDetalleNuevo.FechaModificacion = DateTime.Now;
                            _repSemaforoDetalle.Update(semaforoDetalleNuevo);
                        }
                        else
                        {
                            semaforoDetalleNuevo.IdSemaforoFinanciero = semaforoNuevo.Id;
                            semaforoDetalleNuevo.Nombre = item.Nombre;
                            semaforoDetalleNuevo.Mensaje = item.Mensaje;
                            semaforoDetalleNuevo.Color = item.Color;
                            semaforoDetalleNuevo.Estado = true;
                            semaforoDetalleNuevo.UsuarioCreacion = Semaforo.Usuario;
                            semaforoDetalleNuevo.UsuarioModificacion = Semaforo.Usuario;
                            semaforoDetalleNuevo.FechaCreacion = DateTime.Now;
                            semaforoDetalleNuevo.FechaModificacion = DateTime.Now;
                            _repSemaforoDetalle.Insert(semaforoDetalleNuevo);
                        }
                    }
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina Semaforo.
        /// </summary>
        /// <param name="Semaforo">DTO enviado desde la interfaz</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarSemaforoFinanciero([FromBody] EliminarDTO Semaforo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SemaforoFinancieroRepositorio _repSemaforo = new SemaforoFinancieroRepositorio(_integraDBContext);
                SemaforoFinancieroDetalleRepositorio _repSemaforoDetalle = new SemaforoFinancieroDetalleRepositorio(_integraDBContext);
                var listaIds = _repSemaforoDetalle.GetBy(x => x.IdSemaforoFinanciero == Semaforo.Id).Select(x => x.Id).ToList();

                _repSemaforo.Delete(Semaforo.Id, Semaforo.NombreUsuario);
                _repSemaforoDetalle.Delete(listaIds, Semaforo.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos para la interfaz compensacion por puesto
        /// </summary>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SemaforoFinancieroVariableRepositorio _repSemFinVariable = new SemaforoFinancieroVariableRepositorio(_integraDBContext);
                MonedaRepositorio _repMoneda = new MonedaRepositorio(_integraDBContext);
                OperadorComparacionRepositorio _repComparacion = new OperadorComparacionRepositorio(_integraDBContext);
                var obj = new
                {
                    ListaPais = _repPais.ObtenerListaPais(),
                    ListaVariables = _repSemFinVariable.GetAll().Select(s => new FiltroDTO { Id = s.Id, Nombre = s.Nombre }).ToList(),
                    ListaMonedas=_repMoneda.ObtenerMonedaTodo(),
                    ListaComparadores=_repComparacion.ObtenerListaOperadorComparacion()
                };
                return Ok(obj);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene detalle de los puestos de trabajo
        /// </summary>
		/// <param name="IdSemaforoFinancieroDetalle">DTO enviado desde la interfaz<</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerSemaforoFinancieroDetalleVariable([FromBody] int IdSemaforoFinancieroDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                SemaforoFinancieroDetalleRepositorio _repSemFinDetalle = new SemaforoFinancieroDetalleRepositorio(_integraDBContext);
                var resultado=_repSemFinDetalle.ObtenerVariables(IdSemaforoFinancieroDetalle);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza semaforo financiero
        /// </summary>
		/// <param name="Semaforo">DTO enviado desde la interfaz<</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarSemaforoFinancieroVariable([FromBody] SemaforoFinancieroDetalleV2DTO Semaforo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                SemaforoFinancieroDetalleRepositorio _repSemaforoDetalle = new SemaforoFinancieroDetalleRepositorio(_integraDBContext);
                SemaforoFinancieroDetalleBO semaforoDetalleNuevo = new SemaforoFinancieroDetalleBO();
                SemaforoFinancieroDetalleVariableRepositorio _repSemaforoVariable = new SemaforoFinancieroDetalleVariableRepositorio(_integraDBContext);
                
                semaforoDetalleNuevo = _repSemaforoDetalle.FirstById(Semaforo.Id);
                semaforoDetalleNuevo.Color = Semaforo.Color;
                semaforoDetalleNuevo.Mensaje = Semaforo.Mensaje;
                semaforoDetalleNuevo.Nombre = Semaforo.Nombre;
                semaforoDetalleNuevo.UsuarioModificacion = Semaforo.Usuario;
                semaforoDetalleNuevo.FechaModificacion = DateTime.Now;
                _repSemaforoDetalle.Update(semaforoDetalleNuevo);
                if (Semaforo.Actualizar==0)//insertar
                {
                    foreach(var item in Semaforo.Variable)
                    {
                        SemaforoFinancieroDetalleVariableBO semaforoVariable = new SemaforoFinancieroDetalleVariableBO();
                        semaforoVariable.IdSemaforoFinancieroDetalle = semaforoDetalleNuevo.Id;
                        semaforoVariable.IdSemaforoFinancieroVariable = (int)item.IdSemaforoFinancieroVariable;
                        semaforoVariable.ValorMinimo = item.ValorMinimo;
                        semaforoVariable.ValorMaximo = item.ValorMaximo;
                        semaforoVariable.IdMoneda = item.IdMoneda;
                        semaforoVariable.Estado = true;
                        semaforoVariable.UsuarioCreacion = Semaforo.Usuario;
                        semaforoVariable.UsuarioModificacion = Semaforo.Usuario;
                        semaforoVariable.FechaCreacion = DateTime.Now;
                        semaforoVariable.FechaModificacion = DateTime.Now;
                        _repSemaforoVariable.Insert(semaforoVariable);
                    }
                }
                else//Actualizar
                {
                    foreach (var item in Semaforo.Variable)
                    {
                        SemaforoFinancieroDetalleVariableBO semaforoVariable = new SemaforoFinancieroDetalleVariableBO();
                        semaforoVariable = _repSemaforoVariable.FirstById((int)item.Id);
                        semaforoVariable.IdSemaforoFinancieroDetalle = semaforoDetalleNuevo.Id;
                        semaforoVariable.IdSemaforoFinancieroVariable = (int)item.IdSemaforoFinancieroVariable;
                        semaforoVariable.ValorMinimo = item.ValorMinimo;
                        semaforoVariable.ValorMaximo = item.ValorMaximo;
                        semaforoVariable.IdMoneda = item.IdMoneda;
                        semaforoVariable.UsuarioModificacion = Semaforo.Usuario;
                        semaforoVariable.FechaModificacion = DateTime.Now;
                        _repSemaforoVariable.Update(semaforoVariable);
                    }
                }
                
                return Ok(_repSemaforoDetalle.GetBy(x => x.IdSemaforoFinanciero == semaforoDetalleNuevo.IdSemaforoFinanciero).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
