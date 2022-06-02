using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ConfiguracionEnvioMailing")]
    public class ConfiguracionEnvioMailingController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private ConfiguracionEnvioMailingRepositorio _repConfiguracionEnvioMailingRepositorio;
        public ConfiguracionEnvioMailingController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repConfiguracionEnvioMailingRepositorio = new ConfiguracionEnvioMailingRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ConfiguracionEnvioMailingMasivoDTO ObjetoJson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaConfiguracionEnvioMailing = new List<ConfiguracionEnvioMailingBO>();
                foreach (var item in ObjetoJson.ListaConfiguracionEnvioMailing)
                {
                    if (item.Id != 0)
                    {
                        if (_repConfiguracionEnvioMailingRepositorio.Exist(item.Id))
                        {
                            var configuracionEnvioMailingBO = _repConfiguracionEnvioMailingRepositorio.FirstById(item.Id);
                            configuracionEnvioMailingBO.Activo = false;
                            configuracionEnvioMailingBO.UsuarioModificacion = item.NombreUsuario;
                            configuracionEnvioMailingBO.FechaModificacion = DateTime.Now;
                            _repConfiguracionEnvioMailingRepositorio.Update(configuracionEnvioMailingBO);
                        }
                    }

                    var configuracionEnvioMailing = new ConfiguracionEnvioMailingBO()
                    {
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        IdConjuntoListaDetalle = item.IdConjuntoListaDetalle,
                        IdPlantilla = item.IdPlantilla,
                        Estado = true,
                        Activo = true,
                        UsuarioCreacion = item.NombreUsuario,
                        UsuarioModificacion = item.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    if (!configuracionEnvioMailing.HasErrors)
                    {
                        listaConfiguracionEnvioMailing.Add(configuracionEnvioMailing);
                    }
                }
                _repConfiguracionEnvioMailingRepositorio.Insert(listaConfiguracionEnvioMailing);
                return Ok(listaConfiguracionEnvioMailing);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarDetalle([FromBody] ConfiguracionEnvioMailingMasivoDTO ObjetoJson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaConfiguracionEnvioMailing = new List<ConfiguracionEnvioMailingBO>();
                foreach (var item in ObjetoJson.ListaConfiguracionEnvioMailing)
                {
                    if (item.Id != 0)
                    {
                        if (_repConfiguracionEnvioMailingRepositorio.Exist(item.Id))
                        {
                            var configuracionEnvioMailingBO = _repConfiguracionEnvioMailingRepositorio.FirstById(item.Id);
                            configuracionEnvioMailingBO.Activo = false;
                            configuracionEnvioMailingBO.UsuarioModificacion = item.NombreUsuario;
                            configuracionEnvioMailingBO.FechaModificacion = DateTime.Now;
                            _repConfiguracionEnvioMailingRepositorio.Update(configuracionEnvioMailingBO);
                        }
                    }

                    var configuracionEnvioMailing = new ConfiguracionEnvioMailingBO()
                    {
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        IdConjuntoListaDetalle = item.IdConjuntoListaDetalle,
                        IdPlantilla = item.IdPlantilla,
                        Estado = true,
                        Activo = true,
                        UsuarioCreacion = item.NombreUsuario,
                        UsuarioModificacion = item.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    if (!configuracionEnvioMailing.HasErrors)
                    {
                        listaConfiguracionEnvioMailing.Add(configuracionEnvioMailing);
                    }
                }
                _repConfiguracionEnvioMailingRepositorio.Insert(listaConfiguracionEnvioMailing);
                return Ok(listaConfiguracionEnvioMailing);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracion(int IdConjuntoLista)
        {
            try
            {
                ConfiguracionEnvioMailingRepositorio _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(_integraDBContext);
                return Ok(_repConfiguracionEnvioMailing.ObtenerConfiguracionPorConjuntoLista(IdConjuntoLista));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
