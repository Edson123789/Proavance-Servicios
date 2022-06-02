using System;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Transactions;
using BSI.Integra.Aplicacion.Servicios;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Marketing/ConjuntoLista
    /// Autor: Wilber Choque - Fischer Valdez - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// Configura las actividades automaticas de la interfaz ConjuntoLista
    /// </summary>
    [Route("api/ConjuntoLista")]
    public class ConjuntoListaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly ConjuntoListaRepositorio _repConjuntoLista;

        public ConjuntoListaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repConjuntoLista = new ConjuntoListaRepositorio(_integraDBContext);
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repConjuntoLista.Obtener());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ObtenerDetalle(int IdConjuntoLista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoListaDetalleRepositorio repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
                ConjuntoListaDetalleValorRepositorio repConjuntoListaDetalleValor = new ConjuntoListaDetalleValorRepositorio(_integraDBContext);

                var listaDetalle = repConjuntoListaDetalle.Obtener(IdConjuntoLista);

                foreach (var item in listaDetalle)
                {
                    var conjuntoListaDetalleValor = repConjuntoListaDetalleValor.ObtenerConjuntoListaDetalleValor(item.Id);
                    item.ListaArea = conjuntoListaDetalleValor.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).ToList();
                    item.ListaSubArea = conjuntoListaDetalleValor.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea).ToList();
                    item.ListaProgramaGeneral = conjuntoListaDetalleValor.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral).ToList();
                }

                var conjuntoListaDetalleCompleto = new ConjuntoListaDetalleCompletoDTO()
                {
                    ConjuntoListaDetalle = listaDetalle
                };
                return Ok( conjuntoListaDetalleCompleto.ConjuntoListaDetalle );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltros()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var filtros = new FiltroConjuntoLista();

                CategoriaObjetoFiltroRepositorio repCategoriaObjetoFiltro = new CategoriaObjetoFiltroRepositorio(_integraDBContext);
                AreaCapacitacionRepositorio _repAreaCapacitacion = new AreaCapacitacionRepositorio(_integraDBContext);
                SubAreaCapacitacionRepositorio _repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio(_integraDBContext);
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(_integraDBContext);
                FiltroSegmentoRepositorio _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);

                filtros.ListaCategoriaObjetoFiltro = repCategoriaObjetoFiltro.ObtenerParaConjuntoAnuncio();
                filtros.ListaArea = _repAreaCapacitacion.ObtenerTodoFiltro();
                filtros.ListaSubArea = _repSubAreaCapacitacion.ObtenerTodoFiltro();
                filtros.ListaProgramaGeneral = _repPGeneral.ObtenerPorSubArea();
                filtros.ListaFiltroSegmento = _repFiltroSegmento.ObtenerTodoFiltroTabs();

                return Ok(filtros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO ConjuntoLista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoListaRepositorio _repConjuntoLista = new ConjuntoListaRepositorio(_integraDBContext);
                ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
                ConjuntoListaDetalleValorRepositorio _repConjuntoListaDetalleValor = new ConjuntoListaDetalleValorRepositorio(_integraDBContext);

                if (_repConjuntoLista.Exist(ConjuntoLista.Id))
                {
                    //Eliminamos hijos - detalle
                    var conjuntoListaDetalle = _repConjuntoListaDetalle.GetBy(x => x.IdConjuntoLista == ConjuntoLista.Id).ToList();
                    foreach (var item in conjuntoListaDetalle)
                    {
                        //Eliminamos hijos de hijos -detalle valor
                        var conjuntoListaDetalleValor = _repConjuntoListaDetalleValor.GetBy(x => x.IdConjuntoListaDetalle == item.Id).ToList();
                        foreach (var valor in conjuntoListaDetalleValor)
                        {
                            _repConjuntoListaDetalleValor.Delete(valor.Id, ConjuntoLista.NombreUsuario);
                        }
                        _repConjuntoListaDetalle.Delete(item.Id, ConjuntoLista.NombreUsuario);
                    }
                    _repConjuntoLista.Delete(ConjuntoLista.Id, ConjuntoLista.NombreUsuario);
                    return Ok(true);
                }
                else
                {
                    return BadRequest("Registro no existente");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ConjuntoListaDetalleCompletoDTO ConjuntoListaDetalleCompleto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var conjuntoLista = new ConjuntoListaBO(_integraDBContext);
                return Ok(conjuntoLista.Insertar(ConjuntoListaDetalleCompleto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] ConjuntoListaDetalleCompletoDTO ConjuntoListaDetalleCompleto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoListaRepositorio _repConjuntoLista = new ConjuntoListaRepositorio(_integraDBContext);
                ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
                ConjuntoListaDetalleValorRepositorio _repConjuntoListaDetalleValor = new ConjuntoListaDetalleValorRepositorio(_integraDBContext);
                using (TransactionScope scope = new TransactionScope())
                {

                    if (!_repConjuntoLista.Exist(ConjuntoListaDetalleCompleto.ConjuntoLista.Id))
                    {
                        return BadRequest("No existe el conjunto de lista");
                    }
                    var conjuntoLista =_repConjuntoLista.FirstById(ConjuntoListaDetalleCompleto.ConjuntoLista.Id);
                    conjuntoLista.Nombre = ConjuntoListaDetalleCompleto.ConjuntoLista.Nombre;
                    conjuntoLista.Descripcion = ConjuntoListaDetalleCompleto.ConjuntoLista.Descripcion;
                    conjuntoLista.IdCategoriaObjetoFiltro = ConjuntoListaDetalleCompleto.ConjuntoLista.IdCategoriaObjetoFiltro;
                    conjuntoLista.IdFiltroSegmento = ConjuntoListaDetalleCompleto.ConjuntoLista.IdFiltroSegmento;
                    conjuntoLista.NroListasRepeticionContacto = ConjuntoListaDetalleCompleto.ConjuntoLista.NroListasRepeticionContacto;
                    conjuntoLista.ConsiderarYaEnviados = ConjuntoListaDetalleCompleto.ConjuntoLista.ConsiderarYaEnviados;
                    conjuntoLista.UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario;
                    conjuntoLista.FechaModificacion = DateTime.Now;

                    ///Se cuales pertenecen
                    ///Si no me lo enviaron es porque quieren eliminarlo
                    ///

                    var idsDebenContinuar = ConjuntoListaDetalleCompleto.ConjuntoListaDetalle.Where(x => x.Id != 0).Select(x => x.Id).ToList();
                    var idsExisten = _repConjuntoListaDetalle.GetBy(x => x.IdConjuntoLista == ConjuntoListaDetalleCompleto.ConjuntoLista.Id).Select(x => x.Id).ToList();

                    var idsEliminar = idsExisten.Where(x => !idsDebenContinuar.Contains(x));

                    //eliminamos los que deben ser eliminados
                    _repConjuntoListaDetalle.Delete(idsEliminar, ConjuntoListaDetalleCompleto.NombreUsuario);


                    foreach (var item in ConjuntoListaDetalleCompleto.ConjuntoListaDetalle)
                    {
                        ConjuntoListaDetalleBO conjuntoListaDetalle = null;
                        if (item.Id == 0)// no existe
                        {
                            conjuntoListaDetalle = new ConjuntoListaDetalleBO()
                            {
                                Nombre = item.Nombre,
                                Descripcion = item.Descripcion,
                                Prioridad = item.Prioridad,
                                UsuarioCreacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            };
                        }
                        else
                        {
                            if (!_repConjuntoListaDetalle.Exist(item.Id))
                            {
                                return BadRequest("Detalle no existe");
                            }

                            conjuntoListaDetalle = _repConjuntoListaDetalle.FirstById(item.Id);
                            conjuntoListaDetalle.Nombre = item.Nombre;
                            conjuntoListaDetalle.Descripcion = item.Descripcion;
                            conjuntoListaDetalle.Prioridad = item.Prioridad;
                            conjuntoListaDetalle.UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario;
                            conjuntoListaDetalle.FechaModificacion = DateTime.Now;

                        }

                        

                        ///llenamos hijos
                        var conjuntoListaDetalleValor = new List<ConjuntoListaDetalleValorBO>();

                        //borramos existentes
                        var listaBorrar = _repConjuntoListaDetalleValor.GetBy(x => x.IdConjuntoListaDetalle == item.Id && x.Estado == true).ToList();
                        listaBorrar.RemoveAll(x => item.ListaArea.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea));
                        listaBorrar.RemoveAll(x => item.ListaSubArea.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea));
                        listaBorrar.RemoveAll(x => item.ListaProgramaGeneral.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral));

                        foreach (var borrar in listaBorrar)
                        {
                            _repConjuntoListaDetalleValor.Delete(borrar.Id, ConjuntoListaDetalleCompleto.NombreUsuario);
                        }
                        //eliminados

                        ConjuntoListaDetalleValorBO _new;
                        foreach (var area in item.ListaArea)
                        {
                            if (_repConjuntoListaDetalleValor.Exist(x => x.Valor == area.Valor && x.IdConjuntoListaDetalle == item.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea))
                            {
                                _new = _repConjuntoListaDetalleValor.FirstBy(x => x.Valor == area.Valor && x.IdConjuntoListaDetalle == item.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea);
                                _new.UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario;
                                _new.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                _new = new ConjuntoListaDetalleValorBO
                                {
                                    IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroArea,
                                    Valor = area.Valor,
                                    Estado = true,
                                    UsuarioCreacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                    UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                            }
                            conjuntoListaDetalleValor.Add(_new);
                        }
                        foreach (var subArea in item.ListaSubArea)
                        {
                            if (_repConjuntoListaDetalleValor.Exist(x => x.Valor == subArea.Valor && x.IdConjuntoListaDetalle == item.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea))
                            {
                                _new = _repConjuntoListaDetalleValor.FirstBy(x => x.Valor == subArea.Valor && x.IdConjuntoListaDetalle == item.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea);
                                _new.UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario;
                                _new.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                _new = new ConjuntoListaDetalleValorBO
                                {
                                    IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSubArea,
                                    Valor = subArea.Valor,
                                    Estado = true,
                                    UsuarioCreacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                    UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                            }
                            conjuntoListaDetalleValor.Add(_new);
                        }
                        foreach (var pGeneral in item.ListaProgramaGeneral)
                        {
                            if (_repConjuntoListaDetalleValor.Exist(x => x.Valor == pGeneral.Valor && x.IdConjuntoListaDetalle == item.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral))
                            {
                                _new = _repConjuntoListaDetalleValor.FirstBy(x => x.Valor == pGeneral.Valor && x.IdConjuntoListaDetalle == item.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral);
                                _new.UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario;
                                _new.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                _new = new ConjuntoListaDetalleValorBO
                                {
                                    IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral,
                                    Valor = pGeneral.Valor,
                                    Estado = true,
                                    UsuarioCreacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                    UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                            }
                            conjuntoListaDetalleValor.Add(_new);
                        }
                        //hijos de detalle
                        conjuntoListaDetalle.ListaConjuntoListaDetalleValor.AddRange(conjuntoListaDetalleValor);
                        conjuntoLista.ListaConjuntoListaDetalle.Add(conjuntoListaDetalle);
                    }
                    _repConjuntoLista.Update(conjuntoLista);
                    scope.Complete();
                    return Ok(true);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerResultados(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repConjuntoLista.Exist(Id))
                {
                    return BadRequest("Conjunto lista no existente");
                }
                var conjuntoLista = _repConjuntoLista.FirstById(Id);
                return Ok(new { listadoDatosFiltrados = conjuntoLista.ObtenerResultados() });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Wilber Choque - Gian Miranda
        /// Fecha: 30/01/2021
        /// Versión: 1.5
        /// <summary>
        /// Ejecuta Filtro Segmento sin autenticación, solo mandando el Id del FiltroSegmentoConjuntoLista
        /// </summary>
        /// <param name="Id">Id del Conjunto Lista que se va a ejecutar</param>
        /// <returns>Response 200 con booleano true o caso contrario, response 400 con el mensaje de error</returns>
        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult Ejecutar(int Id)
        {
            var _repConjuntoLista = new ConjuntoListaRepositorio();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var conjuntoLista = new ConjuntoListaBO(_integraDBContext)
                {
                    Id = Id,
                    UsuarioCreacion = "ProcesoAutomatico"
                };
                var listadoDatosFiltrados = conjuntoLista.EjecutarConjuntoLista();

                var _conjuntoLista = _repConjuntoLista.FirstById(Id);
                // Enviar mensaje sistemas
                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Procesar conjunto lista - Correcto ", _conjuntoLista.Nombre),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(listadoDatosFiltrados)),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                mailservice.SetData(mailData);
                mailservice.SendMessageTask();
                // Mensaje al usuario final
                var mensajeFinal = new List<MensajeProcesarDTO>();
                var listadoCorrecto = listadoDatosFiltrados.Where(x => x.EsCorrecto).Select(x => x.Listado).FirstOrDefault();
                var listadoErroneo = listadoDatosFiltrados.Where(x => !x.EsCorrecto).Select(x => x.Listado).FirstOrDefault();

                var correcto = new MensajeProcesarDTO()
                {
                    Nombre = "CORRECTO",
                    ListaDetalle = listadoCorrecto.GroupBy(x => x.Nombre).Select(X => new MensajeProcesarDetalleDTO
                       {
                           NombreCampania = _conjuntoLista.Nombre,
                           NombreLista = X.Key,
                           NroIntentos = X.ToList().Count()
                       }).ToList()
                };
                var error = new MensajeProcesarDTO()
                {
                    Nombre = "ERROR",
                    ListaDetalle =
                        listadoErroneo.GroupBy(x => x.Nombre).Select(X => new MensajeProcesarDetalleDTO
                        {
                            NombreCampania = _conjuntoLista.Nombre,
                            NombreLista = X.Key,
                            NroIntentos = X.ToList().Count()
                        }).ToList()
                };

                mensajeFinal.Add(error);
                mensajeFinal.Add(correcto);

                List<string> correosPersonalizados = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl mailservicePersonalizado = new TMK_MailServiceImpl();
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correosPersonalizados),
                    Subject = string.Concat("Procesar conjunto lista - Correcto ", _conjuntoLista.Nombre),
                    //Message = string.Concat("Message: ", JsonConvert.SerializeObject(mensajeFinal)),
                    Message = conjuntoLista.GenerarPlantillaNotificacionProcesamientoCorrecto(mensajeFinal),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                mailservicePersonalizado.SetData(mailDataPersonalizado);
                mailservicePersonalizado.SendMessageTask();

                return Ok(true);
            }
            catch (Exception e)
            {
                var conjuntoLista = _repConjuntoLista.FirstById(Id);

                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Procesar conjunto lista - Error ", conjuntoLista.Nombre),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                mailservice.SetData(mailData);
                mailservice.SendMessageTask();
                return BadRequest(new { Resultado = "ERROR", e.Message });
                //return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{Id}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult Ejecutar(int Id, string NombreUsuario)
        {
            var _repConjuntoLista = new ConjuntoListaRepositorio();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
              
                var conjuntoLista = new ConjuntoListaBO(_integraDBContext)

                {
                    Id = Id,
                    UsuarioCreacion = NombreUsuario
                };
                var listadoDatosFiltrados = conjuntoLista.EjecutarConjuntoLista();

                var _conjuntoLista = _repConjuntoLista.FirstById(Id);
                // Enviar mensaje sistemas
                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Procesar conjunto lista - Correcto ", _conjuntoLista.Nombre),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(listadoDatosFiltrados)),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                mailservice.SetData(mailData);
                mailservice.SendMessageTask();
                // Mensaje a usuario final
                var mensajeFinal = new List<MensajeProcesarDTO>();
                var listadoCorrecto = listadoDatosFiltrados.Where(x => x.EsCorrecto).Select(x => x.Listado).FirstOrDefault();
                var listadoErroneo = listadoDatosFiltrados.Where(x => !x.EsCorrecto).Select(x => x.Listado).FirstOrDefault();

                var correcto = new MensajeProcesarDTO()
                {
                    Nombre = "CORRECTO",
                    ListaDetalle = listadoCorrecto.GroupBy(x => x.Nombre).Select(X => new MensajeProcesarDetalleDTO
                    {
                        NombreCampania = _conjuntoLista.Nombre,
                        NombreLista = X.Key,
                        NroIntentos = X.ToList().Count()
                    }).ToList()
                };
                var error = new MensajeProcesarDTO()
                {
                    Nombre = "ERROR",
                    ListaDetalle =
                        listadoErroneo.GroupBy(x => x.Nombre).Select(X => new MensajeProcesarDetalleDTO
                        {
                            NombreCampania = _conjuntoLista.Nombre,
                            NombreLista = X.Key,
                            NroIntentos = X.ToList().Count()
                        }).ToList()
                };

                mensajeFinal.Add(error);
                mensajeFinal.Add(correcto);

                List<string> correosPersonalizados = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };

                if (_repIntegraAspNetUsers.ExistePorNombreUsuario(NombreUsuario))
                {
                    try
                    {
                        correosPersonalizados.Add(_repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(NombreUsuario));
                    }
                    catch (Exception)
                    {
                    }
                }
                TMK_MailServiceImpl mailservicePersonalizado = new TMK_MailServiceImpl();
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correosPersonalizados),
                    Subject = string.Concat("Procesar conjunto lista - Correcto ", _conjuntoLista.Nombre),
                    //Message = string.Concat("Message: ", JsonConvert.SerializeObject(mensajeFinal)),
                    Message = string.Concat(conjuntoLista.GenerarPlantillaNotificacionProcesamientoCorrecto(mensajeFinal),
                    $@"

                    >>>>> Servicio de confirmación de conjunto lista <<<<<
                    "
                    ),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                mailservicePersonalizado.SetData(mailDataPersonalizado);
                mailservicePersonalizado.SendMessageTask();

                return Ok(true);
            }
            catch (Exception e)
            {
                var correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };

                var Mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Procesar conjunto lista - Error ", _repConjuntoLista.FirstById(Id).Nombre),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(e),
                     $@"

                    >>>>> Servicio de confirmación de conjunto lista <<<<<
                    "
                    ),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
                return BadRequest(new { Resultado = "ERROR", e.Message });
                //return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}/{IdConjuntoListaDetalle}/{EsPrueba}")]
        [HttpGet]
        public ActionResult Ejecutar(int Id, int IdConjuntoListaDetalle, bool EsPrueba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var conjuntoLista = new ConjuntoListaBO(_integraDBContext)
                {
                    Id = Id
                };
                return Ok(new { listadoDatosFiltrados = conjuntoLista.EjecutarConjuntoLista(IdConjuntoListaDetalle) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("[action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ObtenerIdConjuntoListaDetalle(int IdConjuntoLista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
                var Respuesta = _repConjuntoListaDetalle.ObtenerConjuntoListaDetalleId(IdConjuntoLista);
                return Ok(Respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("[action]/{IdConjuntoListaDetalle}")]
        [HttpGet]
        public ActionResult ObtenerResultadoLista(int IdConjuntoListaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
                ConjuntoListaResultadoRepositorio _repConjuntoListaResultado = new ConjuntoListaResultadoRepositorio(_integraDBContext);
                var Respuesta = _repConjuntoListaResultado.ObtenerConjuntoListaResultado(IdConjuntoListaDetalle);
                return Ok(Respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult Duplicar([FromBody] DuplicarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var conjuntoLista = new ConjuntoListaBO(_integraDBContext)
                {
                    Id = Json.Id
                };
                var conjuntoListaDetalle = conjuntoLista.ObtenerDetalle();
                conjuntoListaDetalle.NombreUsuario = Json.NombreUsuario;
                conjuntoListaDetalle.ConjuntoLista.Nombre = string.Concat(conjuntoListaDetalle.ConjuntoLista.Nombre, " - COPIA");
                conjuntoListaDetalle.ConjuntoLista.Descripcion = string.Concat(conjuntoListaDetalle.ConjuntoLista.Descripcion, " - COPIA");

                var nuevoConjuntoLista = new ConjuntoListaBO(_integraDBContext);
                return Ok(nuevoConjuntoLista.Insertar(conjuntoListaDetalle));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}