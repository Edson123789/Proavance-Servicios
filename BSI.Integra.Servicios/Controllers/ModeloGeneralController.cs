using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
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

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ModeloGeneral")]
    public class ModeloGeneralController : BaseController<TModeloGeneral, ValidadoModeloGeneralDTO>
    {
        public ModeloGeneralController(IIntegraRepository<TModeloGeneral> repositorio, ILogger<BaseController<TModeloGeneral, ValidadoModeloGeneralDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        { }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModeloGeneralRepositorio _repModeloGeneral = new ModeloGeneralRepositorio();
                return Ok(_repModeloGeneral.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarModeloGeneral([FromBody] ModeloGeneralCompuestoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModeloGeneralRepositorio _repModeloGeneral = new ModeloGeneralRepositorio();
                ModeloGeneralBO modeloGeneralBO = new ModeloGeneralBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    modeloGeneralBO.Nombre = Json.Modelo.Nombre;
                    modeloGeneralBO.PeIntercepto = Json.Modelo.PeIntercepto;
                    modeloGeneralBO.PeEstado = Json.Modelo.PeEstado;
                    modeloGeneralBO.IdPadre = Json.Modelo.IdPadre;
                    modeloGeneralBO.PeVersion = Json.Modelo.PeVersion;

                    modeloGeneralBO.Estado = true;
                    modeloGeneralBO.UsuarioCreacion = Json.Usuario;
                    modeloGeneralBO.UsuarioModificacion = Json.Usuario;
                    modeloGeneralBO.FechaCreacion = DateTime.Now;
                    modeloGeneralBO.FechaModificacion = DateTime.Now;

                    modeloGeneralBO.ModeloGeneralEscala = new List<ModeloGeneralEscalaBO>();
                    modeloGeneralBO.ModeloGeneralCargo = new List<ModeloGeneralCargoBO>();
                    modeloGeneralBO.ModeloGeneralIndustria = new List<ModeloGeneralIndustriaBO>();
                    modeloGeneralBO.ModeloGeneralAFormacion = new List<ModeloGeneralAFormacionBO>();
                    modeloGeneralBO.ModeloGeneralATrabajo = new List<ModeloGeneralATrabajoBO>();
                    modeloGeneralBO.ModeloGeneralCategoriaDato = new List<ModeloGeneralCategoriaDatoBO>();
                    modeloGeneralBO.ModeloGeneralTipoDato = new List<ModeloGeneralTipoDatoBO>();

                    foreach (var item in Json.ListaEscalaProbabilidad)
                    {
                        ModeloGeneralEscalaBO modeloGeneralEscalaBO = new ModeloGeneralEscalaBO();
                        modeloGeneralEscalaBO.Nombre = item.Nombre;
                        modeloGeneralEscalaBO.Orden = item.Orden;
                        modeloGeneralEscalaBO.ValorMaximo = item.ValorMaximo;
                        modeloGeneralEscalaBO.ValorMinimo = item.ValorMinimo;
                        modeloGeneralEscalaBO.IdModeloGeneral = Json.Modelo.Id;
                        modeloGeneralEscalaBO.Estado = true;
                        modeloGeneralEscalaBO.UsuarioCreacion = Json.Usuario;
                        modeloGeneralEscalaBO.UsuarioModificacion = Json.Usuario;
                        modeloGeneralEscalaBO.FechaCreacion = DateTime.Now;
                        modeloGeneralEscalaBO.FechaModificacion = DateTime.Now;

                        modeloGeneralBO.ModeloGeneralEscala.Add(modeloGeneralEscalaBO);
                    }

                    foreach (var item in Json.ListaCargo)
                    {
                        ModeloGeneralCargoBO modeloGeneralCargoBO = new ModeloGeneralCargoBO();
                        modeloGeneralCargoBO.IdCargo = item.IdCargo;
                        modeloGeneralCargoBO.Nombre = item.Nombre;
                        modeloGeneralCargoBO.Valor = item.Valor;
                        modeloGeneralCargoBO.Estado = true;
                        modeloGeneralCargoBO.UsuarioCreacion = Json.Usuario;
                        modeloGeneralCargoBO.UsuarioModificacion = Json.Usuario;
                        modeloGeneralCargoBO.FechaCreacion = DateTime.Now;
                        modeloGeneralCargoBO.FechaModificacion = DateTime.Now;

                        modeloGeneralBO.ModeloGeneralCargo.Add(modeloGeneralCargoBO);
                    }

                    foreach (var item in Json.ListaIndustria)
                    {
                        ModeloGeneralIndustriaBO modeloGeneralIndustriaBO = new ModeloGeneralIndustriaBO();
                        modeloGeneralIndustriaBO.IdIndustria = item.IdIndustria;
                        modeloGeneralIndustriaBO.Nombre = item.Nombre;
                        modeloGeneralIndustriaBO.Valor = item.Valor;
                        modeloGeneralIndustriaBO.Estado = true;
                        modeloGeneralIndustriaBO.UsuarioCreacion = Json.Usuario;
                        modeloGeneralIndustriaBO.UsuarioModificacion = Json.Usuario;
                        modeloGeneralIndustriaBO.FechaCreacion = DateTime.Now;
                        modeloGeneralIndustriaBO.FechaModificacion = DateTime.Now;

                        modeloGeneralBO.ModeloGeneralIndustria.Add(modeloGeneralIndustriaBO);
                    }

                    foreach (var item in Json.ListaAFormacion)
                    {
                        ModeloGeneralAFormacionBO modeloGeneralAFormacionBO = new ModeloGeneralAFormacionBO();
                        modeloGeneralAFormacionBO.IdAreaFormacion = item.IdAreaFormacion;
                        modeloGeneralAFormacionBO.Nombre = item.Nombre;
                        modeloGeneralAFormacionBO.Valor = item.Valor;
                        modeloGeneralAFormacionBO.Estado = true;
                        modeloGeneralAFormacionBO.UsuarioCreacion = Json.Usuario;
                        modeloGeneralAFormacionBO.UsuarioModificacion = Json.Usuario;
                        modeloGeneralAFormacionBO.FechaCreacion = DateTime.Now;
                        modeloGeneralAFormacionBO.FechaModificacion = DateTime.Now; 

                        modeloGeneralBO.ModeloGeneralAFormacion.Add(modeloGeneralAFormacionBO);
                    }

                    foreach (var item in Json.ListaATrabajo)
                    {
                        ModeloGeneralATrabajoBO modeloGeneralATrabajoBO = new ModeloGeneralATrabajoBO();
                        modeloGeneralATrabajoBO.IdAreaTrabajo = item.IdAreaTrabajo;
                        modeloGeneralATrabajoBO.Nombre = item.Nombre;
                        modeloGeneralATrabajoBO.Valor = item.Valor;
                        modeloGeneralATrabajoBO.Estado = true;
                        modeloGeneralATrabajoBO.UsuarioCreacion = Json.Usuario;
                        modeloGeneralATrabajoBO.UsuarioModificacion = Json.Usuario;
                        modeloGeneralATrabajoBO.FechaCreacion = DateTime.Now;
                        modeloGeneralATrabajoBO.FechaModificacion = DateTime.Now; 

                        modeloGeneralBO.ModeloGeneralATrabajo.Add(modeloGeneralATrabajoBO);
                    }

                    foreach (var item in Json.ListaCategoriaDato)
                    {
                        ModeloGeneralCategoriaDatoBO modeloGeneralCategoriaDatoBO = new ModeloGeneralCategoriaDatoBO();
                        modeloGeneralCategoriaDatoBO.IdCategoriaDato = item.IdCategoriaDato;
                        modeloGeneralCategoriaDatoBO.Nombre = item.Nombre;
                        modeloGeneralCategoriaDatoBO.Valor = item.Valor;
                        modeloGeneralCategoriaDatoBO.Estado = true;
                        modeloGeneralCategoriaDatoBO.UsuarioCreacion = Json.Usuario;
                        modeloGeneralCategoriaDatoBO.UsuarioModificacion = Json.Usuario;
                        modeloGeneralCategoriaDatoBO.FechaCreacion = DateTime.Now;
                        modeloGeneralCategoriaDatoBO.FechaModificacion = DateTime.Now;

                        modeloGeneralBO.ModeloGeneralCategoriaDato.Add(modeloGeneralCategoriaDatoBO);
                    }

                    foreach (var item in Json.ListaTipoDato)
                    {
                        ModeloGeneralTipoDatoBO modeloGeneralTipoDatoBO = new ModeloGeneralTipoDatoBO();
                        modeloGeneralTipoDatoBO.IdTipoDato = item;
                        modeloGeneralTipoDatoBO.Estado = true;
                        modeloGeneralTipoDatoBO.UsuarioCreacion = Json.Usuario;
                        modeloGeneralTipoDatoBO.UsuarioModificacion = Json.Usuario;
                        modeloGeneralTipoDatoBO.FechaCreacion = DateTime.Now;
                        modeloGeneralTipoDatoBO.FechaModificacion = DateTime.Now;

                        modeloGeneralBO.ModeloGeneralTipoDato.Add(modeloGeneralTipoDatoBO);
                    }

                    _repModeloGeneral.Insert(modeloGeneralBO);
                    scope.Complete();
                }
                return Ok(modeloGeneralBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarModeloGeneral([FromBody] ModeloGeneralCompuestoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();

                ModeloGeneralRepositorio _repModeloGeneral = new ModeloGeneralRepositorio(contexto);
                ModeloGeneralEscalaRepositorio _repModeloGeneralEscala= new ModeloGeneralEscalaRepositorio(contexto);
                ModeloGeneralCargoRepositorio _repModeloGeneralCargo = new ModeloGeneralCargoRepositorio(contexto);
                ModeloGeneralIndustriaRepositorio _repModeloGeneralIndustria = new ModeloGeneralIndustriaRepositorio(contexto);
                ModeloGeneralAFormacionRepositorio _repModeloGeneralAFormacion = new ModeloGeneralAFormacionRepositorio(contexto);
                ModeloGeneralATrabajoRepositorio _repModeloGeneralATrabajo = new ModeloGeneralATrabajoRepositorio(contexto);
                ModeloGeneralCategoriaDatoRepositorio _repModeloGeneralCategoriaDato = new ModeloGeneralCategoriaDatoRepositorio(contexto);
                ModeloGeneralTipoDatoRepositorio _repModeloGeneralTipoDato = new ModeloGeneralTipoDatoRepositorio(contexto);

                ModeloGeneralBO modeloGeneralBO = new ModeloGeneralBO();


                using (TransactionScope scope = new TransactionScope())
                {
                    _repModeloGeneralEscala.EliminacionLogicoPorIdModeloGeneral(Json.Modelo.Id, Json.Usuario, Json.ListaEscalaProbabilidad);
                    _repModeloGeneralCargo.EliminacionLogicoPorIdModeloGeneral(Json.Modelo.Id, Json.Usuario, Json.ListaCargo);
                    _repModeloGeneralIndustria.EliminacionLogicoPorIdModeloGeneral(Json.Modelo.Id, Json.Usuario, Json.ListaIndustria);
                    _repModeloGeneralAFormacion.EliminacionLogicoPorIdModeloGeneral(Json.Modelo.Id, Json.Usuario, Json.ListaAFormacion);
                    _repModeloGeneralATrabajo.EliminacionLogicoPorIdModeloGeneral(Json.Modelo.Id, Json.Usuario, Json.ListaATrabajo);
                    _repModeloGeneralCategoriaDato.EliminacionLogicoPorIdModeloGeneral(Json.Modelo.Id, Json.Usuario, Json.ListaCategoriaDato);
                    _repModeloGeneralTipoDato.EliminacionLogicoPorIdModeloGeneral(Json.Modelo.Id, Json.Usuario, Json.ListaTipoDato);

                    modeloGeneralBO = _repModeloGeneral.FirstById(Json.Modelo.Id);

                    modeloGeneralBO.Nombre = Json.Modelo.Nombre;
                    modeloGeneralBO.PeIntercepto = Json.Modelo.PeIntercepto;
                    modeloGeneralBO.PeEstado = Json.Modelo.PeEstado;
                    modeloGeneralBO.PeVersion = Json.Modelo.PeVersion;
                    modeloGeneralBO.IdPadre = Json.Modelo.IdPadre;
                    modeloGeneralBO.UsuarioModificacion = Json.Usuario;
                    modeloGeneralBO.FechaModificacion = DateTime.Now;

                    modeloGeneralBO.ModeloGeneralEscala = new List<ModeloGeneralEscalaBO>();
                    modeloGeneralBO.ModeloGeneralCargo = new List<ModeloGeneralCargoBO>();
                    modeloGeneralBO.ModeloGeneralIndustria = new List<ModeloGeneralIndustriaBO>();
                    modeloGeneralBO.ModeloGeneralAFormacion = new List<ModeloGeneralAFormacionBO>();
                    modeloGeneralBO.ModeloGeneralATrabajo = new List<ModeloGeneralATrabajoBO>();
                    modeloGeneralBO.ModeloGeneralCategoriaDato = new List<ModeloGeneralCategoriaDatoBO>();
                    modeloGeneralBO.ModeloGeneralTipoDato = new List<ModeloGeneralTipoDatoBO>();

                    foreach (var item in Json.ListaEscalaProbabilidad)
                    {
                        ModeloGeneralEscalaBO modeloGeneralEscalaBO;
                        if (_repModeloGeneralEscala.Exist(x => x.Id == item.Id && x.IdModeloGeneral == Json.Modelo.Id))
                        {
                            modeloGeneralEscalaBO = _repModeloGeneralEscala.FirstBy(x => x.Id == item.Id && x.IdModeloGeneral == Json.Modelo.Id);
                            modeloGeneralEscalaBO.Nombre = item.Nombre;
                            modeloGeneralEscalaBO.Orden = item.Orden;
                            modeloGeneralEscalaBO.ValorMaximo = item.ValorMaximo;
                            modeloGeneralEscalaBO.ValorMinimo = item.ValorMinimo;
                            modeloGeneralEscalaBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralEscalaBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modeloGeneralEscalaBO = new ModeloGeneralEscalaBO();
                            modeloGeneralEscalaBO.Nombre = item.Nombre;
                            modeloGeneralEscalaBO.Orden = item.Orden;
                            modeloGeneralEscalaBO.ValorMaximo = item.ValorMaximo;
                            modeloGeneralEscalaBO.ValorMinimo = item.ValorMinimo;
                            modeloGeneralEscalaBO.Estado = true;
                            modeloGeneralEscalaBO.UsuarioCreacion = Json.Usuario;
                            modeloGeneralEscalaBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralEscalaBO.FechaCreacion = DateTime.Now;
                            modeloGeneralEscalaBO.FechaModificacion = DateTime.Now;
                        }
                        modeloGeneralBO.ModeloGeneralEscala.Add(modeloGeneralEscalaBO);
                    }

                    foreach (var item in Json.ListaCargo)
                    {
                        ModeloGeneralCargoBO modeloGeneralCargoBO;
                        if (_repModeloGeneralCargo.Exist(x => x.Id == item.Id && x.IdModeloGeneral == Json.Modelo.Id))
                        {
                            modeloGeneralCargoBO = _repModeloGeneralCargo.FirstBy(x => x.Id == item.Id && x.IdModeloGeneral == Json.Modelo.Id);
                            modeloGeneralCargoBO.IdCargo = item.IdCargo;
                            modeloGeneralCargoBO.Nombre = item.Nombre;
                            modeloGeneralCargoBO.Valor = item.Valor;
                            modeloGeneralCargoBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralCargoBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modeloGeneralCargoBO = new ModeloGeneralCargoBO();
                            modeloGeneralCargoBO.IdCargo = item.IdCargo;
                            modeloGeneralCargoBO.Nombre = item.Nombre;
                            modeloGeneralCargoBO.Valor = item.Valor;
                            modeloGeneralCargoBO.Estado = true;
                            modeloGeneralCargoBO.UsuarioCreacion = Json.Usuario;
                            modeloGeneralCargoBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralCargoBO.FechaCreacion = DateTime.Now;
                            modeloGeneralCargoBO.FechaModificacion = DateTime.Now;
                        }
                        modeloGeneralBO.ModeloGeneralCargo.Add(modeloGeneralCargoBO);
                    }

                    foreach (var item in Json.ListaIndustria)
                    {
                        ModeloGeneralIndustriaBO modeloGeneralIndustriaBO;
                        if (_repModeloGeneralIndustria.Exist(x => x.Id == item.Id && x.IdModeloGeneral == Json.Modelo.Id))
                        {
                            modeloGeneralIndustriaBO = _repModeloGeneralIndustria.FirstBy(x => x.Id == item.Id && x.IdModeloGeneral == Json.Modelo.Id);
                            modeloGeneralIndustriaBO.IdIndustria = item.IdIndustria;
                            modeloGeneralIndustriaBO.Nombre = item.Nombre;
                            modeloGeneralIndustriaBO.Valor = item.Valor;
                            modeloGeneralIndustriaBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralIndustriaBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modeloGeneralIndustriaBO = new ModeloGeneralIndustriaBO();
                            modeloGeneralIndustriaBO.IdIndustria = item.IdIndustria;
                            modeloGeneralIndustriaBO.Nombre = item.Nombre;
                            modeloGeneralIndustriaBO.Valor = item.Valor;
                            modeloGeneralIndustriaBO.Estado = true;
                            modeloGeneralIndustriaBO.UsuarioCreacion = Json.Usuario;
                            modeloGeneralIndustriaBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralIndustriaBO.FechaCreacion = DateTime.Now;
                            modeloGeneralIndustriaBO.FechaModificacion = DateTime.Now;
                        }
                        modeloGeneralBO.ModeloGeneralIndustria.Add(modeloGeneralIndustriaBO);
                    }

                    foreach (var item in Json.ListaAFormacion)
                    {
                        ModeloGeneralAFormacionBO modeloGeneralAFormacionBO;
                        if (_repModeloGeneralAFormacion.Exist(x => x.Id == item.Id && x.IdModeloGeneral == Json.Modelo.Id))
                        {
                            modeloGeneralAFormacionBO = _repModeloGeneralAFormacion.FirstBy(x => x.Id == item.Id && x.IdModeloGeneral == Json.Modelo.Id);
                            modeloGeneralAFormacionBO.IdAreaFormacion = item.IdAreaFormacion;
                            modeloGeneralAFormacionBO.Nombre = item.Nombre;
                            modeloGeneralAFormacionBO.Valor = item.Valor;
                            modeloGeneralAFormacionBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralAFormacionBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modeloGeneralAFormacionBO = new ModeloGeneralAFormacionBO();
                            modeloGeneralAFormacionBO.IdAreaFormacion = item.IdAreaFormacion;
                            modeloGeneralAFormacionBO.Nombre = item.Nombre;
                            modeloGeneralAFormacionBO.Valor = item.Valor;
                            modeloGeneralAFormacionBO.Estado = true;
                            modeloGeneralAFormacionBO.UsuarioCreacion = Json.Usuario;
                            modeloGeneralAFormacionBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralAFormacionBO.FechaCreacion = DateTime.Now;
                            modeloGeneralAFormacionBO.FechaModificacion = DateTime.Now;
                        }
                        modeloGeneralBO.ModeloGeneralAFormacion.Add(modeloGeneralAFormacionBO);
                    }

                    foreach (var item in Json.ListaATrabajo)
                    {
                        ModeloGeneralATrabajoBO modeloGeneralATrabajoBO;
                        if (_repModeloGeneralATrabajo.Exist(x => x.Id == item.Id && x.IdModeloGeneral == Json.Modelo.Id))
                        {
                            modeloGeneralATrabajoBO = _repModeloGeneralATrabajo.FirstBy(x => x.Id == item.Id && x.IdModeloGeneral == Json.Modelo.Id);
                            modeloGeneralATrabajoBO.IdAreaTrabajo = item.IdAreaTrabajo;
                            modeloGeneralATrabajoBO.Nombre = item.Nombre;
                            modeloGeneralATrabajoBO.Valor = item.Valor;
                            modeloGeneralATrabajoBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralATrabajoBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modeloGeneralATrabajoBO = new ModeloGeneralATrabajoBO();
                            modeloGeneralATrabajoBO.IdAreaTrabajo = item.IdAreaTrabajo;
                            modeloGeneralATrabajoBO.Nombre = item.Nombre;
                            modeloGeneralATrabajoBO.Valor = item.Valor;
                            modeloGeneralATrabajoBO.Estado = true;
                            modeloGeneralATrabajoBO.UsuarioCreacion = Json.Usuario;
                            modeloGeneralATrabajoBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralATrabajoBO.FechaCreacion = DateTime.Now;
                            modeloGeneralATrabajoBO.FechaModificacion = DateTime.Now;
                        }
                        modeloGeneralBO.ModeloGeneralATrabajo.Add(modeloGeneralATrabajoBO);
                    }

                    foreach (var item in Json.ListaCategoriaDato)
                    {
                        ModeloGeneralCategoriaDatoBO modeloGeneralCategoriaDatoBO;
                        if (_repModeloGeneralCategoriaDato.Exist(x => x.Id == item.Id && x.IdModeloGeneral == Json.Modelo.Id))
                        {
                            modeloGeneralCategoriaDatoBO = _repModeloGeneralCategoriaDato.FirstBy(x => x.Id == item.Id && x.IdModeloGeneral == Json.Modelo.Id);
                            modeloGeneralCategoriaDatoBO.IdCategoriaDato = item.IdCategoriaDato;
                            modeloGeneralCategoriaDatoBO.Nombre = item.Nombre;
                            modeloGeneralCategoriaDatoBO.Valor = item.Valor;
                            modeloGeneralCategoriaDatoBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralCategoriaDatoBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modeloGeneralCategoriaDatoBO = new ModeloGeneralCategoriaDatoBO();
                            modeloGeneralCategoriaDatoBO.IdCategoriaDato = item.IdCategoriaDato;
                            modeloGeneralCategoriaDatoBO.Nombre = item.Nombre;
                            modeloGeneralCategoriaDatoBO.Valor = item.Valor;
                            modeloGeneralCategoriaDatoBO.Estado = true;
                            modeloGeneralCategoriaDatoBO.UsuarioCreacion = Json.Usuario;
                            modeloGeneralCategoriaDatoBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralCategoriaDatoBO.FechaCreacion = DateTime.Now;
                            modeloGeneralCategoriaDatoBO.FechaModificacion = DateTime.Now;
                        }
                        modeloGeneralBO.ModeloGeneralCategoriaDato.Add(modeloGeneralCategoriaDatoBO);
                    }

                    foreach (var item in Json.ListaTipoDato)
                    {
                        ModeloGeneralTipoDatoBO modeloGeneralTipoDatoBO;
                        if (_repModeloGeneralTipoDato.Exist(x => x.Id == item && x.IdModeloGeneral == Json.Modelo.Id))
                        {
                            modeloGeneralTipoDatoBO = _repModeloGeneralTipoDato.FirstBy(x => x.Id == item && x.IdModeloGeneral == Json.Modelo.Id);
                            modeloGeneralTipoDatoBO.IdTipoDato = item;
                            modeloGeneralTipoDatoBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralTipoDatoBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modeloGeneralTipoDatoBO = new ModeloGeneralTipoDatoBO();
                            modeloGeneralTipoDatoBO.IdTipoDato = item;
                            modeloGeneralTipoDatoBO.Estado = true;
                            modeloGeneralTipoDatoBO.UsuarioCreacion = Json.Usuario;
                            modeloGeneralTipoDatoBO.UsuarioModificacion = Json.Usuario;
                            modeloGeneralTipoDatoBO.FechaCreacion = DateTime.Now;
                            modeloGeneralTipoDatoBO.FechaModificacion = DateTime.Now;
                        }
                        modeloGeneralBO.ModeloGeneralTipoDato.Add(modeloGeneralTipoDatoBO);
                    }

                    _repModeloGeneral.Update(modeloGeneralBO);
                    scope.Complete();
                }
                return Ok(modeloGeneralBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerVariablesConfiguracion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CargoRepositorio _repCargo = new CargoRepositorio();
                AreaFormacionRepositorio _repAreaFormacion = new AreaFormacionRepositorio();
                AreaTrabajoRepositorio _repAreaTrabajo = new AreaTrabajoRepositorio();
                IndustriaRepositorio _repIndustria = new IndustriaRepositorio();
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio();
                SubCategoriaDatoRepositorio _repSubCategoriaDato = new SubCategoriaDatoRepositorio();
                TipoInteraccionRepositorio _repTipoInteraccion = new TipoInteraccionRepositorio();

                var rptaCargo = _repCargo.ObtenerCargoFiltro();
                var rptaAreaFormacion = _repAreaFormacion.ObtenerAreaFormacionFiltro();
                var rptaAreaTrabajo = _repAreaTrabajo.ObtenerAreasTrabajo();
                var rptaIndustria = _repIndustria.ObtenerIndustriaFiltro();

                var rptaCategoriaDato = _repCategoriaOrigen.ObtenerTodoGrid();
                var rptaSubCategoriaDato = _repSubCategoriaDato.ObtenerTodoGrid();
                var rptaTipoInteraccion = _repTipoInteraccion.ObtenerTodoGrid();

                return Ok( new {resultado = "OK", rptaCargo, rptaAreaFormacion, rptaAreaTrabajo, rptaIndustria, rptaCategoriaDato, rptaSubCategoriaDato, rptaTipoInteraccion });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdModelo}")]
        [HttpGet]
        public ActionResult ObtenerVariablesGuardadas(int IdModelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModeloGeneralRepositorio _repModeloGeneral = new ModeloGeneralRepositorio();
                ModeloGeneralEscalaRepositorio _repModeloGeneralEscala = new ModeloGeneralEscalaRepositorio();
                ModeloGeneralCargoRepositorio _repModeloGeneralCargo = new ModeloGeneralCargoRepositorio();
                ModeloGeneralIndustriaRepositorio _repModeloGeneralIndustria = new ModeloGeneralIndustriaRepositorio();
                ModeloGeneralAFormacionRepositorio _repModeloGeneralAFormacion = new ModeloGeneralAFormacionRepositorio();
                ModeloGeneralATrabajoRepositorio _repModeloGeneralATrabajo = new ModeloGeneralATrabajoRepositorio();
                ModeloGeneralCategoriaDatoRepositorio _repModeloGeneralCategoriaDato = new ModeloGeneralCategoriaDatoRepositorio();
                ModeloGeneralTipoDatoRepositorio _repModeloGeneralTipoDato = new ModeloGeneralTipoDatoRepositorio();

                ModeloGeneralCompuestoDTO modeloGeneral = new ModeloGeneralCompuestoDTO
                {
                    Modelo = new ModeloGeneralDTO(),
                    ListaEscalaProbabilidad = _repModeloGeneralEscala.ObtenerProgramaGeneralConfiguracionVariableFiltro(IdModelo),
                    ListaIndustria = _repModeloGeneralIndustria.ObtenerProgramaGeneralConfiguracionVariableFiltro(IdModelo),
                    ListaCargo = _repModeloGeneralCargo.ObtenerProgramaGeneralConfiguracionVariableFiltro(IdModelo),
                    ListaAFormacion= _repModeloGeneralAFormacion.ObtenerProgramaGeneralConfiguracionVariableFiltro(IdModelo),
                    ListaATrabajo = _repModeloGeneralATrabajo.ObtenerProgramaGeneralConfiguracionVariableFiltro(IdModelo),
                    ListaCategoriaDato = _repModeloGeneralCategoriaDato.ObtenerProgramaGeneralConfiguracionVariableFiltro(IdModelo),
                    ListaTipoDato = _repModeloGeneralTipoDato.ObtenerProgramaGeneralConfiguracionVariableFiltro(IdModelo).Select(x => x.IdTipoDato).ToList()
                };

                return Ok(modeloGeneral);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoTipoDato()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio();
                return Ok(_repTipoDato.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarModeloGeneral([FromBody] ModeloGeneralDTO ModeloGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                ModeloGeneralRepositorio _repModeloGeneral = new ModeloGeneralRepositorio(contexto);
                ModeloGeneralEscalaRepositorio _repModeloGeneralEscala = new ModeloGeneralEscalaRepositorio(contexto);
                ModeloGeneralCargoRepositorio _repModeloGeneralCargo = new ModeloGeneralCargoRepositorio(contexto);
                ModeloGeneralIndustriaRepositorio _repModeloGeneralIndustria = new ModeloGeneralIndustriaRepositorio(contexto);
                ModeloGeneralAFormacionRepositorio _repModeloGeneralAFormacion = new ModeloGeneralAFormacionRepositorio(contexto);
                ModeloGeneralATrabajoRepositorio _repModeloGeneralATrabajo = new ModeloGeneralATrabajoRepositorio(contexto);
                ModeloGeneralCategoriaDatoRepositorio _repModeloGeneralCategoriaDato = new ModeloGeneralCategoriaDatoRepositorio(contexto);
                ModeloGeneralTipoDatoRepositorio _repModeloGeneralTipoDato = new ModeloGeneralTipoDatoRepositorio(contexto);

                ModeloGeneralBO modeloGeneralBO = new ModeloGeneralBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repModeloGeneral.Exist(ModeloGeneral.Id))
                    {
                        _repModeloGeneral.Delete(ModeloGeneral.Id, ModeloGeneral.Usuario);
                        var hijoModeloGeneralEscala = _repModeloGeneralEscala.GetBy(x => x.IdModeloGeneral == ModeloGeneral.Id);
                        foreach (var hijo in hijoModeloGeneralEscala)
                        {
                            _repModeloGeneralEscala.Delete(hijo.Id, ModeloGeneral.Usuario);
                        }

                        var hijoModeloGeneralCargo = _repModeloGeneralCargo.GetBy(x => x.IdModeloGeneral == ModeloGeneral.Id);
                        foreach (var hijo in hijoModeloGeneralCargo)
                        {
                            _repModeloGeneralCargo.Delete(hijo.Id, ModeloGeneral.Usuario);
                        }

                        var hijoModeloGeneralIndustria = _repModeloGeneralIndustria.GetBy(x => x.IdModeloGeneral == ModeloGeneral.Id);
                        foreach (var hijo in hijoModeloGeneralIndustria)
                        {
                            _repModeloGeneralIndustria.Delete(hijo.Id, ModeloGeneral.Usuario);
                        }

                        var hijoModeloGeneralAFormacion = _repModeloGeneralAFormacion.GetBy(x => x.IdModeloGeneral == ModeloGeneral.Id);
                        foreach (var hijo in hijoModeloGeneralAFormacion)
                        {
                            _repModeloGeneralAFormacion.Delete(hijo.Id, ModeloGeneral.Usuario);
                        }

                        var hijoModeloGeneralATrabajo = _repModeloGeneralATrabajo.GetBy(x => x.IdModeloGeneral == ModeloGeneral.Id);
                        foreach (var hijo in hijoModeloGeneralATrabajo)
                        {
                            _repModeloGeneralATrabajo.Delete(hijo.Id, ModeloGeneral.Usuario);
                        }

                        var hijoModeloGeneralCategoriaDato = _repModeloGeneralCategoriaDato.GetBy(x => x.IdModeloGeneral == ModeloGeneral.Id);
                        foreach (var hijo in hijoModeloGeneralCategoriaDato)
                        {
                            _repModeloGeneralCategoriaDato.Delete(hijo.Id, ModeloGeneral.Usuario);
                        }

                        var hijoModeloGeneralTipoDato = _repModeloGeneralTipoDato.GetBy(x => x.IdModeloGeneral == ModeloGeneral.Id);
                        foreach (var hijo in hijoModeloGeneralTipoDato)
                        {
                            _repModeloGeneralTipoDato.Delete(hijo.Id, ModeloGeneral.Usuario);
                        }
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class ValidadoModeloGeneralDTO : AbstractValidator<TModeloGeneral>
    {
        public static ValidadoModeloGeneralDTO Current = new ValidadoModeloGeneralDTO();
        public ValidadoModeloGeneralDTO()
        {
        }
    }
}
