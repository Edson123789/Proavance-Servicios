/// Controlador: GestionPersonas/PostulanteInformacionImportacion
/// Autor: Edgar S.
/// Fecha: 14/01/2021
/// <summary>
/// Controlador del módulo PostulanteInformacionImportacion CRUD y obtención de Combos
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PostulanteInformacionImportacion")]
    [ApiController]
    public class PostulanteInformacionImportacionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly ProcesoSeleccionRepositorio _repProcesoSeleccion;
        private readonly PostulanteRepositorio _repPostulante;
        private readonly PostulanteInformacionImportacionRepositorio _repPostulanteInformacionImportacion;
        private readonly PostulanteInformacionImportacionLogRepositorio _repPostulanteInformacionImportacionLog;

        public PostulanteInformacionImportacionController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repProcesoSeleccion = new ProcesoSeleccionRepositorio(_integraDBContext);
            _repPostulante = new PostulanteRepositorio(_integraDBContext);
            _repPostulanteInformacionImportacion = new PostulanteInformacionImportacionRepositorio(_integraDBContext);
            _repPostulanteInformacionImportacionLog = new PostulanteInformacionImportacionLogRepositorio(_integraDBContext);
        }


        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 14/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtención de Registros de Importaciones de Procesos de Seleccion Activos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerRegistrosActivos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {                
                var lista = _repPostulanteInformacionImportacion.ObtenerRegistrosProcesoSeleccionActivo();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 14/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Hace la Inserción de un registro o actualización si ya existe y su inserción histórica
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarRegistro([FromBody] RegistrarImportacionDTO Registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var res = false;
                    var existe = _repPostulanteInformacionImportacion.GetBy(x => x.IdProcesoSeleccion == Registro.IdProcesoSeleccion).FirstOrDefault();
                    if (existe == null)
                    {
                        PostulanteInformacionImportacionBO informacionImportacion = new PostulanteInformacionImportacionBO()
                        {
                            IdProcesoSeleccion = Registro.IdProcesoSeleccion,
                            CantidadTotal = Registro.CantidadTotal,
                            CantidadPrimerCriterio = Registro.CantidadPrimerCriterio,
                            CantidadSegundoCriterio = Registro.CantidadSegundoCriterio,
                            CantidadFinal = Registro.CantidadFinal,
                            Estado = true,
                            UsuarioCreacion = Registro.Usuario,
                            UsuarioModificacion = Registro.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        res = _repPostulanteInformacionImportacion.Insert(informacionImportacion);
                    }
                    else
                    {
                        existe.CantidadTotal = Registro.CantidadTotal;
                        existe.CantidadPrimerCriterio = Registro.CantidadPrimerCriterio;
                        existe.CantidadSegundoCriterio = Registro.CantidadSegundoCriterio;
                        existe.CantidadFinal = Registro.CantidadFinal;
                        existe.UsuarioCreacion = Registro.Usuario;
                        existe.UsuarioModificacion = Registro.Usuario;
                        existe.FechaModificacion = DateTime.Now;
                        res = _repPostulanteInformacionImportacion.Update(existe);
                    }

                    if (res == true)
                    {
                        PostulanteInformacionImportacionLogBO informacionImportacionLog = new PostulanteInformacionImportacionLogBO()
                        {
                            IdProcesoSeleccion = Registro.IdProcesoSeleccion,
                            CantidadTotal = Registro.CantidadTotal,
                            CantidadPrimerCriterio = Registro.CantidadPrimerCriterio,
                            CantidadSegundoCriterio = Registro.CantidadSegundoCriterio,
                            CantidadFinal = Registro.CantidadFinal,
                            Estado = true,
                            UsuarioCreacion = Registro.Usuario,
                            UsuarioModificacion = Registro.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _repPostulanteInformacionImportacionLog.Insert(informacionImportacionLog);
                    }

                    scope.Complete();
                    return Ok(true);

                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 14/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un registro de Importacion e Inserta su registro Histórico
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarRegistro([FromBody]PostulanteInformacionImportacionDTO Registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var existe = _repPostulanteInformacionImportacion.FirstById(Registro.Id);
                    existe.CantidadTotal = Registro.CantidadTotal;
                    existe.CantidadPrimerCriterio = Registro.CantidadPrimerCriterio;
                    existe.CantidadSegundoCriterio = Registro.CantidadSegundoCriterio;
                    existe.CantidadFinal = Registro.CantidadFinal;
                    existe.UsuarioModificacion = Registro.Usuario;
                    existe.FechaModificacion = DateTime.Now;
                    var res = _repPostulanteInformacionImportacion.Update(existe);

                    if (res == true)
                    {
                        PostulanteInformacionImportacionLogBO informacionImportacionLog = new PostulanteInformacionImportacionLogBO()
                        {
                            IdProcesoSeleccion = Registro.IdProcesoSeleccion,
                            CantidadTotal = Registro.CantidadTotal,
                            CantidadPrimerCriterio = Registro.CantidadPrimerCriterio,
                            CantidadSegundoCriterio = Registro.CantidadSegundoCriterio,
                            CantidadFinal = Registro.CantidadFinal,
                            Estado = true,
                            UsuarioCreacion = Registro.Usuario,
                            UsuarioModificacion = Registro.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _repPostulanteInformacionImportacionLog.Insert(informacionImportacionLog);
                    }
                    scope.Complete();
                    return Ok(true);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 14/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Eliminar lógicamente un registro
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO Registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repPostulanteInformacionImportacion.Exist(Registro.Id))
                {
                    var res = _repPostulanteInformacionImportacion.Delete(Registro.Id, Registro.NombreUsuario);
                    return Ok(res);
                }
                else
                {
                    return BadRequest("La categoria a eliminar no existe o ya fue eliminada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 14/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos para el módulo
        /// </summary>
        /// <returns></returns>
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
                var obj = new
                {
                    ListaProcesoSeleccion = _repProcesoSeleccion.ObtenerProcesoSeleccionParaCombo()
                };
                return Ok(obj);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: GET
        /// Autor: Edgar S.
        /// Fecha: 14/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtención de Registro Por Proceso de Seleccion
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{IdProcesoSeleccion}")]
        public ActionResult ObtenerRegistroPorProcesoSeleccion(int IdProcesoSeleccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaTotal = _repPostulanteInformacionImportacion.ObtenerRegistrosProcesoSeleccionActivo();
                var registro = listaTotal.Where(x => x.IdProcesoSeleccion == IdProcesoSeleccion).FirstOrDefault();
                return Ok(registro);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
