using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ConfiguracionPlanilla")]
    public class ConfiguracionPlanillaController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public ConfiguracionPlanillaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionPlanilla()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionPlanillaRepositorio _repConfiguracionPlanillaRepositorio = new ConfiguracionPlanillaRepositorio(_integraDBContext);
                return Ok(_repConfiguracionPlanillaRepositorio.ObtenerTodoConfiguracionPlanilla());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionFechas(int Id)
        {
            try
            {
                ConfiguracionPlanillaFechasRepositorio parametroFechas = new ConfiguracionPlanillaFechasRepositorio(_integraDBContext);
                return Ok(parametroFechas.ObtenerFechasConfiguracion(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarConfiguracionPlanilla([FromBody] CompuestoConfiguracionPlanillaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionPlanillaRepositorio _repConfiguracionPlanilla = new ConfiguracionPlanillaRepositorio(_integraDBContext);
                ConfiguracionPlanillaFechasRepositorio _repConfiguracionPlanillaFechas = new ConfiguracionPlanillaFechasRepositorio(_integraDBContext);

                ConfiguracionPlanillaBO NuevaConfiguracionPlanilla = new ConfiguracionPlanillaBO
                {
                    Nombre = ObjetoDTO.ConfiguracionPlanilla.Nombre,
                    IdTipoRemuneracionAdicional = ObjetoDTO.ConfiguracionPlanilla.IdTipoRemuneracionAdicional,
                    Recurrente = ObjetoDTO.ConfiguracionPlanilla.Recurrente,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.ConfiguracionPlanilla.Usuario,
                    UsuarioModificacion = ObjetoDTO.ConfiguracionPlanilla.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                _repConfiguracionPlanilla.Insert(NuevaConfiguracionPlanilla);

                foreach (var itemFechas in ObjetoDTO.ConfiguracionPlanillaFechas)
                {
                    ConfiguracionPlanillaFechasBO NuevaConfiguracionPlanillaFechas = new ConfiguracionPlanillaFechasBO
                    {
                        IdConfiguracionPlanilla = NuevaConfiguracionPlanilla.Id,
                        FechaProceso = itemFechas.FechaProceso,
                        CalculoReal = itemFechas.CalculoReal,
                        FechaProcesoReal = itemFechas.FechaProcesoReal,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.ConfiguracionPlanilla.Usuario,
                        UsuarioModificacion = ObjetoDTO.ConfiguracionPlanilla.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _repConfiguracionPlanillaFechas.Insert(NuevaConfiguracionPlanillaFechas);
                }

                return Ok(NuevaConfiguracionPlanilla);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarConfiguracionPlanilla([FromBody] CompuestoConfiguracionPlanillaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionPlanillaRepositorio _repConfiguracionPlanilla = new ConfiguracionPlanillaRepositorio(_integraDBContext);
                ConfiguracionPlanillaFechasRepositorio _repConfiguracionPlanillaFechas = new ConfiguracionPlanillaFechasRepositorio(_integraDBContext);

                ConfiguracionPlanillaBO ConfiguracionPlanilla = _repConfiguracionPlanilla.GetBy(x => x.Id == ObjetoDTO.ConfiguracionPlanilla.Id).FirstOrDefault();

                ConfiguracionPlanilla.Nombre = ObjetoDTO.ConfiguracionPlanilla.Nombre;
                ConfiguracionPlanilla.IdTipoRemuneracionAdicional = ObjetoDTO.ConfiguracionPlanilla.IdTipoRemuneracionAdicional;
                ConfiguracionPlanilla.Recurrente = ObjetoDTO.ConfiguracionPlanilla.Recurrente;
                ConfiguracionPlanilla.Estado = true;
                ConfiguracionPlanilla.UsuarioModificacion = ObjetoDTO.ConfiguracionPlanilla.Usuario;
                ConfiguracionPlanilla.FechaModificacion = DateTime.Now;

                _repConfiguracionPlanilla.Update(ConfiguracionPlanilla);

                var fechasEliminacion = _repConfiguracionPlanillaFechas.ObtenerIdsTodoConfiguracionPlanillaFechas(ConfiguracionPlanilla.Id);
                _repConfiguracionPlanillaFechas.Delete(fechasEliminacion, ObjetoDTO.ConfiguracionPlanilla.Usuario);

                foreach (var itemFechas in ObjetoDTO.ConfiguracionPlanillaFechas)
                {
                    ConfiguracionPlanillaFechasBO NuevaConfiguracionPlanillaFechas = new ConfiguracionPlanillaFechasBO
                    {
                        IdConfiguracionPlanilla = ConfiguracionPlanilla.Id,
                        FechaProceso = itemFechas.FechaProceso,
                        CalculoReal = itemFechas.CalculoReal,
                        FechaProcesoReal = itemFechas.FechaProcesoReal,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.ConfiguracionPlanilla.Usuario,
                        UsuarioModificacion = ObjetoDTO.ConfiguracionPlanilla.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _repConfiguracionPlanillaFechas.Insert(NuevaConfiguracionPlanillaFechas);
                }

                return Ok(ConfiguracionPlanilla);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{UserName}/{Id}")]
        [HttpPost]
        public ActionResult EliminarConfiguracionPlanilla(int Id, string UserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionPlanillaRepositorio _repConfiguracionPlanilla = new ConfiguracionPlanillaRepositorio(_integraDBContext);
                ConfiguracionPlanillaBO ConfiguracionPlanilla = _repConfiguracionPlanilla.GetBy(x => x.Id == Id).FirstOrDefault();
                _repConfiguracionPlanilla.Delete(Id, UserName);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarFurAutomaticoPlanillas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionPlanillaRepositorio _repConfPlanilla = new ConfiguracionPlanillaRepositorio();
                var configuracionPlanilla = _repConfPlanilla.ObtenerRegistroConfiguracionPlanilla();
                var PersonalActivo = _repConfPlanilla.ObtenerDatosPlanillaPersonalActivo();
                var fechaActual = DateTime.Now.Date;

                FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);

                foreach (var configuracion in configuracionPlanilla)
                {
                    configuracion.FechaProceso = configuracion.FechaProceso.Date;

                    if (configuracion.EsMensual && fechaActual>=configuracion.FechaProceso.Date && fechaActual.Day== configuracion.FechaProceso.Day) {
                        //Se crean los sueldos automaticamente
                        if (configuracion.IdTipoRemuneracion == ValorEstatico.IdTipoRemuneracionSueldo) { 
                            var x = "Insertar en proyectados todos los sueldos que se encuentren en planilla, con sus descuentos en CTS";
                            
                            foreach (var personal in PersonalActivo)
                            {
                                if (personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa || personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima)
                                {
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        repFurRep.InsertarFurAutomaticoPlanilla(personal,fechaActual, _integraDBContext);
                                        scope.Complete();
                                    }

                                }
                            }
                        }
                        

                        //Se crean los Furs para Bono de acuerdo al mes que estemos se considera dias trabajados para el deposito
                        if (configuracion.IdTipoRemuneracion == ValorEstatico.IdTipoRemuneracionBono)
                        {
                            foreach (var personal in PersonalActivo)
                            {
                                if (personal.TieneBono && personal.TieneContratoVigente)
                                {
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        repFurRep.InsertarFurAutomaticoBonos(personal, fechaActual, _integraDBContext);
                                        scope.Complete();
                                    }
                                }
                            }
                        }
                    }

                    if (!configuracion.EsMensual && fechaActual >= configuracion.FechaProceso.Date && fechaActual.Day == configuracion.FechaProceso.Day && fechaActual.Month == configuracion.FechaProceso.Month) {
                        //Se crean los Furs para Gratificaciones de acuerdo al mes que estemos se considera meses concretos
                        if (configuracion.IdTipoRemuneracion == ValorEstatico.IdTipoRemuneracionGratificacion)
                        {
                            foreach (var personal in PersonalActivo)
                            {
                                if (personal.RecibeBeneficios && (personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa || personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima))
                                {
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        repFurRep.InsertarFurAutomaticoGratificacion(personal, fechaActual, _integraDBContext);
                                        scope.Complete();
                                    }
                                }
                            }
                        }

                        //Se crean los Furs para CTS de acuerdo al mes que estemos se considera dias trabajados para el deposito
                        if (configuracion.IdTipoRemuneracion == ValorEstatico.IdTipoRemuneracionCTS)
                        {

                            foreach (var personal in PersonalActivo)
                            {
                                if (personal.RecibeBeneficios && (personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa || personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima))
                                {
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        repFurRep.InsertarFurAutomaticoCTS(personal, fechaActual, _integraDBContext);
                                        scope.Complete();
                                    }
                                }
                            }
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

        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarFurAutomaticoPlanillasVersion2()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionPlanillaRepositorio _repConfPlanilla = new ConfiguracionPlanillaRepositorio();
                var PersonalActivo = _repConfPlanilla.ObtenerDatosPlanillaPersonalActivo();
                var fechaInicio = new DateTime(2021, 04, 27, 0, 0, 0, 0);
                var fechaHoy = DateTime.Now.Date;
                var fechatemp = DateTime.Today;
                var fechaCreacionFur = new DateTime(fechatemp.Year, fechatemp.Month, 1);

                FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                while (fechaHoy >= fechaInicio)
                {
                    if (fechaHoy.Day == fechaInicio.Day && fechaHoy.Month == fechaInicio.Month)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            decimal TotalEsSaludArequipa = 0;
                            decimal TotalEsSaludLima = 0;

                            fechaCreacionFur = fechaCreacionFur.AddMonths(1);
                            //Se crean los sueldos automaticamente
                            foreach (var personal in PersonalActivo)
                            {
                                if (personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa || personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima
                                    || personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoBogota || personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoSantaCruz)
                                {
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        repFurRep.InsertarFurAutomaticoPlanilla(personal, fechaCreacionFur, _integraDBContext);
                                        scope.Complete();
                                    }
                                }

                                if (personal.TieneBono && personal.TieneContratoVigente)
                                {
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        repFurRep.InsertarFurAutomaticoBonos(personal, fechaCreacionFur, _integraDBContext);
                                        scope.Complete();
                                    }
                                }
                                if (personal.RecibeBeneficios) {
                                    //CREAR FUR PARA SEGURO
                                    if (personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa)
                                    {
                                        TotalEsSaludArequipa += personal.RemuneracionFija;
                                    }
                                    if (personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima)
                                    {
                                        TotalEsSaludLima += personal.RemuneracionFija;
                                    }
                                }
                            }
                            
                            using (TransactionScope scope = new TransactionScope())
                            {
                                repFurRep.InsertarFurAutomaticoEsSalud(4/*ValorEstatico.IdCiudadArequipa*/, TotalEsSaludArequipa, fechaCreacionFur, _integraDBContext);
                                repFurRep.InsertarFurAutomaticoEsSalud(14/*ValorEstatico.IdCiudadLima*/, TotalEsSaludLima, fechaCreacionFur, _integraDBContext);
                                scope.Complete();
                            }

                            //Se crean los Furs para Bono de acuerdo al mes que estemos se considera dias trabajados para el deposito

                            if (fechaCreacionFur.Month == 7|| fechaCreacionFur.Month == 12 || fechaCreacionFur.Month == 2 || fechaCreacionFur.Month == 8)
                            {
                                //Se crean los Furs para Gratificaciones de acuerdo al mes que estemos se considera meses concretos
                                foreach (var personal in PersonalActivo)
                                {
                                    if (personal.RecibeBeneficios && (personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa || personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima)
                                        &&  (fechaCreacionFur.Month == 7 || fechaCreacionFur.Month == 12))
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            repFurRep.InsertarFurAutomaticoGratificacion(personal, fechaCreacionFur, _integraDBContext);
                                            scope.Complete();
                                        }
                                    }

                                    if (personal.RecibeBeneficios && (personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoBogota)
                                        && fechaCreacionFur.Month == 2)
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            repFurRep.InsertarFurAutomaticoGratificacion(personal, fechaCreacionFur, _integraDBContext);
                                            scope.Complete();
                                        }
                                    }
                                    if (personal.RecibeBeneficios && (personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoSantaCruz)
                                        && fechaCreacionFur.Month == 12)
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            repFurRep.InsertarFurAutomaticoGratificacion(personal, fechaCreacionFur, _integraDBContext);
                                            scope.Complete();
                                        }
                                    }
                                }
                            }
                            if (fechaCreacionFur.Month == 5 || fechaCreacionFur.Month == 11 || fechaCreacionFur.Month == 6 || fechaCreacionFur.Month == 12)
                            { //Se crean los Furs para CTS de acuerdo al mes que estemos se considera dias trabajados para el deposito
                                foreach (var personal in PersonalActivo)
                                {
                                    if (personal.RecibeBeneficios && (personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa || personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima)
                                        && (fechaCreacionFur.Month == 5 || fechaCreacionFur.Month == 11) )
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            repFurRep.InsertarFurAutomaticoCTS(personal, fechaCreacionFur, _integraDBContext);
                                            scope.Complete();
                                        }
                                    }
                                    if (personal.RecibeBeneficios && (personal.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoBogota )
                                        && (fechaCreacionFur.Month == 6 || fechaCreacionFur.Month == 12))
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            repFurRep.InsertarFurAutomaticoCTS(personal, fechaCreacionFur, _integraDBContext);
                                            scope.Complete();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    fechaInicio=fechaInicio.AddMonths(6);
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
