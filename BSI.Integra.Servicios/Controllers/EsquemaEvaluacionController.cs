using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: EsquemaEvaluacionController
    /// Autor:--
    /// Fecha: 17/09/2021
    /// <summary>
    /// Gestion de los esuqemas de evaluacion de un curso asi como sus tablas hijas
    /// </summary>
    [Route("api/EsquemaEvaluacion")]
    [ApiController]
    public class EsquemaEvaluacionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly EsquemaEvaluacionRepositorio _repoEsquema;
        private readonly EsquemaEvaluacionDetalleRepositorio _repoEsquemaDetalle;
        private readonly EsquemaEvaluacionBO _repoEjemplo;

        public EsquemaEvaluacionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repoEsquema = new EsquemaEvaluacionRepositorio(_integraDBContext);
            _repoEsquemaDetalle = new EsquemaEvaluacionDetalleRepositorio(_integraDBContext);
        }
        /// TipoFuncion: GET
        /// Autor: 
        /// Fecha: 17/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene una lista con los esquemas de evaluacion
        /// </summary>
        /// <returns>List<EsquemaEvaluacion_ListadoDTO><returns>
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
                return Ok(_repoEsquema.ObtenerTodo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 17/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta el esquema de evaluacion y sus detalles
        /// </summary>
        /// <param name=”escala”>DTO del esquema de evaluacion y sus detalles</param>
        /// <returns>bool<returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] EsquemaEvaluacion_RegistrarDTO escala)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var EsquemaEvaluacionNuevo = new EsquemaEvaluacionBO()
                {
                    Nombre = escala.Nombre,
                    IdFormaCalculoEvaluacion = escala.IdFormaCalculoEvaluacion,

                    Estado = true,
                    UsuarioCreacion = escala.NombreUsuario,
                    UsuarioModificacion = escala.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                //añade la lista de detalles
                if (escala.ListadoDetalle != null && escala.ListadoDetalle.Count > 0)
                {
                    if (escala.ListadoDetalle.GroupBy(g => new {g.IdCriterioEvaluacion})
                        .Select(s => new {Key = s.Key, Contador = s.Count()}).Max(m => m.Contador) > 1)
                        throw new Exception("No se puede registrar más de 1 criterio de evaluación del mismo tipo.");

                    EsquemaEvaluacionNuevo.ListadoDetalle = new List<EsquemaEvaluacionDetalleBO>();
                    EsquemaEvaluacionNuevo.ListadoDetalle.AddRange(escala.ListadoDetalle.Select(s =>
                        new EsquemaEvaluacionDetalleBO()
                        {
                            IdCriterioEvaluacion = s.IdCriterioEvaluacion,
                            Ponderacion = s.Ponderacion,

                            Estado = true,
                            UsuarioCreacion = escala.NombreUsuario,
                            UsuarioModificacion = escala.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }));
                }
                if (!EsquemaEvaluacionNuevo.HasErrors)
                {
                    _repoEsquema.Insert(EsquemaEvaluacionNuevo);
                }
                else
                {
                    return BadRequest(EsquemaEvaluacionNuevo.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 17/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Modifica el esquema de evaluacion y sus detalles
        /// </summary>
        /// <param name=”esquema”>DTO del esquema de evaluacion y sus detalles</param>
        /// <returns>bool<returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] EsquemaEvaluacion_RegistrarDTO esquema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repoEsquema.Exist(esquema.Id))
                {
                    return BadRequest("Esquema no existente!");
                }
                var escalaExistente = _repoEsquema.FirstById(esquema.Id);
                escalaExistente.Nombre = esquema.Nombre;
                escalaExistente.IdFormaCalculoEvaluacion = esquema.IdFormaCalculoEvaluacion;

                escalaExistente.UsuarioModificacion = esquema.NombreUsuario;
                escalaExistente.FechaModificacion = DateTime.Now;

                var listadoDetalleExistente =
                    _repoEsquemaDetalle.GetBy(w => w.IdEsquemaEvaluacion == esquema.Id);
                List<int> listadoIdDetalleExistente = new List<int>();


                var listadoEliminar = new List<int>();
                //añade la lista de detalles
                if (esquema.ListadoDetalle != null && esquema.ListadoDetalle.Count > 0)
                {
                    if (esquema.ListadoDetalle.GroupBy(g => new { g.IdCriterioEvaluacion })
                        .Select(s => new { Key = s.Key, Contador = s.Count() }).Max(m => m.Contador) > 1)
                        throw new Exception("No se puede registrar más de 1 criterio de evaluación del mismo tipo.");

                    escalaExistente.ListadoDetalle = new List<EsquemaEvaluacionDetalleBO>();
                    listadoIdDetalleExistente = listadoDetalleExistente.Select(s => s.Id).ToList();

                    var listadoNuevo = new List<EsquemaEvaluacionDetalleBO>();
                    var listadoActualizar = new List<EsquemaEvaluacionDetalleBO>();

                    listadoNuevo.AddRange(esquema.ListadoDetalle.Where(w => w.Id == null || w.Id == 0).Select(s =>
                        new EsquemaEvaluacionDetalleBO()
                        {
                            IdCriterioEvaluacion = s.IdCriterioEvaluacion,
                            Ponderacion = s.Ponderacion,

                            Estado = true,
                            UsuarioCreacion = esquema.NombreUsuario,
                            UsuarioModificacion = esquema.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }));
                    foreach (var detalleExistente in listadoDetalleExistente.Where(w => esquema.ListadoDetalle.Select(s => s.Id).Contains(w.Id)))
                    {
                        var itemActualizado = esquema.ListadoDetalle.FirstOrDefault(f => f.Id == detalleExistente.Id);

                        detalleExistente.IdCriterioEvaluacion = itemActualizado.IdCriterioEvaluacion;
                        detalleExistente.Ponderacion = itemActualizado.Ponderacion;

                        detalleExistente.UsuarioModificacion = esquema.NombreUsuario;
                        detalleExistente.FechaModificacion = DateTime.Now;

                        listadoActualizar.Add(detalleExistente);
                    }

                    escalaExistente.ListadoDetalle.AddRange(listadoNuevo);
                    escalaExistente.ListadoDetalle.AddRange(listadoActualizar);

                }
                if (listadoIdDetalleExistente != null && listadoIdDetalleExistente.Count > 0)
                {
                    listadoEliminar.AddRange(listadoIdDetalleExistente.Where(w =>
                        !esquema.ListadoDetalle.Select(s => s.Id).Contains(w)));
                }

                if (!escalaExistente.HasErrors)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        _repoEsquema.Update(escalaExistente);
                        _repoEsquemaDetalle.Delete(listadoEliminar, esquema.NombreUsuario);
                        scope.Complete();
                    }
                }
                else
                {
                    return BadRequest(escalaExistente.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 20/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina esquemas
        /// </summary>
        /// <param name=”flujo”>DTO general que tiene el id a eliminar de una tabla</param>
        /// <returns>bool<returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO flujo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repoEsquema.Exist(flujo.Id))
                {
                    return BadRequest("Esquema no existente");
                }
                _repoEsquema.Delete(flujo.Id, flujo.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 20/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina esquemas
        /// </summary>
        /// <param name=”flujo”>DTO general que tiene el id a eliminar de una tabla</param>
        /// <returns>bool<returns>
        [Route("[Action]/{idEsquemaEvaluacion}")]
        [HttpGet]
        public ActionResult ObtenerListadoDetalle(int idEsquemaEvaluacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repoEsquemaDetalle.GetBy(w => w.IdEsquemaEvaluacion == idEsquemaEvaluacion));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repoEsquema.ObtenerCombo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult SubirArchivo([FromForm] IList<IFormFile> archivos)
        {
            try
            {
                EsquemaEvaluacionBO bo = new EsquemaEvaluacionBO();
                string varUrl = "";
                foreach (var archivo in archivos)
                {
                    var nombreArchivo = string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), '-', archivo.FileName);
                    varUrl = bo.SubirArchivo(archivo.ConvertToByte(), archivo.ContentType, nombreArchivo);
                }

                return Ok(varUrl);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idEsquemaEvaluacion}")]
        [HttpGet]
        public ActionResult ObtenerDetalleEsquema(int idEsquemaEvaluacion)
        {
            try
            {
                return Ok(_repoEsquemaDetalle.GetBy(w => w.IdEsquemaEvaluacion == idEsquemaEvaluacion,
                    s => new
                    {
                        s.Id, s.IdEsquemaEvaluacion, s.IdCriterioEvaluacion, s.IdCriterioEvaluacionNavigation.Nombre,
                        s.Ponderacion
                    }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerEsquemaAsociado(int idPGeneral)
        {
            try
            {
                EsquemaEvaluacionPgeneralRepositorio _repoEsquemaAsignado =
                    new EsquemaEvaluacionPgeneralRepositorio(_integraDBContext);
                EsquemaEvaluacionPgeneralModalidadRepositorio _repoModalidad =
                    new EsquemaEvaluacionPgeneralModalidadRepositorio(_integraDBContext);
                EsquemaEvaluacionPgeneralProveedorRepositorio _repoProveedor =
                    new EsquemaEvaluacionPgeneralProveedorRepositorio(_integraDBContext);

                var listado = _repoEsquemaAsignado.GetBy(w => w.IdPgeneral == idPGeneral,
                    s => new
                    {
                        s.Id, s.IdEsquemaEvaluacion, s.IdPgeneral, Esquema = s.IdEsquemaEvaluacionNavigation.Nombre, s.FechaInicio, s.FechaFin,s.EsquemaPredeterminado
                    });

                var listadoFinal = listado.Select(s => new
                {
                    s.Id,
                    s.IdEsquemaEvaluacion,
                    s.IdPgeneral,
                    s.FechaInicio, s.FechaFin,
                    s.EsquemaPredeterminado,
                    Esquema = s.Esquema,
                    ListadoModalidad = _repoModalidad.GetBy(w => w.IdEsquemaEvaluacionPgeneral == s.Id,
                        s2 => new {s2.IdModalidadCurso}).Select(s3 => s3.IdModalidadCurso).ToList(),
                    ModalidadMostrar = string.Join(",", _repoModalidad.GetBy(w => w.IdEsquemaEvaluacionPgeneral == s.Id,
                        s2 => s2.IdModalidadCursoNavigation.Nombre).ToList()),
                    ListadoProveedor = _repoProveedor.GetBy(w => w.IdEsquemaEvaluacionPgeneral == s.Id,
                        s2 => new { s2.IdProveedor }).Select(s3 => s3.IdProveedor).ToList(),
                }).ToList();

                return Ok(listadoFinal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult RegistrarAsignacion([FromBody] EsquemaEvaluacion_RegistrarAsignacionDTO esquema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EsquemaEvaluacionPgeneralRepositorio _repoAsignacion = new EsquemaEvaluacionPgeneralRepositorio();

                EsquemaEvaluacionPgeneralBO bo = new EsquemaEvaluacionPgeneralBO()
                {
                    IdPgeneral = esquema.IdPGeneral,
                    IdEsquemaEvaluacion = esquema.IdEsquemaEvaluacion,
                    FechaInicio = esquema.FechaInicio,
                    FechaFin = esquema.FechaFin,
                    EsquemaPredeterminado = esquema.EsquemaPredeterminado,
                    Estado = true,
                    UsuarioCreacion = esquema.NombreUsuario,
                    UsuarioModificacion = esquema.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                //añade la lista de detalles
                if (esquema.ListadoDetalleAsignacion != null && esquema.ListadoDetalleAsignacion.Count > 0)
                {
                    bo.ListadoDetalle = new List<EsquemaEvaluacionPgeneralDetalleBO>();
                    bo.ListadoDetalle.AddRange(esquema.ListadoDetalleAsignacion.Select(s =>
                        new EsquemaEvaluacionPgeneralDetalleBO()
                        {
                            IdCriterioEvaluacion = s.IdCriterioEvaluacion,
                            IdProveedor = s.IdProveedor,
                            Nombre = s.Nombre,
                            UrlArchivoInstrucciones = s.UrlArchivoInstrucciones,
                            
                            Estado = true,
                            UsuarioCreacion = esquema.NombreUsuario,
                            UsuarioModificacion = esquema.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }));
                }
                //añade la lista de modalidad
                if (esquema.IdModalidad != null && esquema.IdModalidad.Count > 0)
                {
                    bo.ListadoModalidad = new List<EsquemaEvaluacionPgeneralModalidadBO>();
                    bo.ListadoModalidad.AddRange(esquema.IdModalidad.Select(s =>
                        new EsquemaEvaluacionPgeneralModalidadBO()
                        {
                            IdModalidadCurso = s,

                            Estado = true,
                            UsuarioCreacion = esquema.NombreUsuario,
                            UsuarioModificacion = esquema.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }));
                }
                //añade la lista de proveedor
                if (esquema.IdProveedor != null && esquema.IdProveedor.Count > 0)
                {
                    bo.ListadoProveedor = new List<EsquemaEvaluacionPgeneralProveedorBO>();
                    bo.ListadoProveedor.AddRange(esquema.IdProveedor.Select(s =>
                        new EsquemaEvaluacionPgeneralProveedorBO()
                        {
                            IdProveedor = s,

                            Estado = true,
                            UsuarioCreacion = esquema.NombreUsuario,
                            UsuarioModificacion = esquema.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }));
                }
                if (!bo.HasErrors)
                {
                    _repoAsignacion.Insert(bo);
                }
                else
                {
                    return BadRequest(bo.GetErrors());
                }

                if (esquema.EsquemaPredeterminado == true)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        _repoEsquema.UpdateEsquemaEvaluacionPredefinido(esquema.Id);
                        scope.Complete();
                    }
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 20/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Modifica o inserta un nuevo esquemas asi como sus detalles ,proveedores y  modalidades 
        /// </summary>
        /// <param name=”Esquema”>DTO del esquema de evaluacion</param>
        /// <returns>bool<returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarAsignacion([FromBody] EsquemaEvaluacion_RegistrarAsignacionDTO Esquema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EsquemaEvaluacionPgeneralRepositorio _repoAsignacion = new EsquemaEvaluacionPgeneralRepositorio();
                var boExistente = _repoAsignacion.FirstById(Esquema.Id);
                int nuevoIdEsquema = 0;

                bool estatusDetalle = _repoAsignacion.CompareEsquemaEvaluacionPgeneralDetalle(Esquema);
                bool estatusModalidad = _repoAsignacion.CompareEsquemaEvaluacionPgeneralModalidad(Esquema);
                bool estatusproveedor = _repoAsignacion.CompareEsquemaEvaluacionPgeneralProveedor(Esquema);
                if (boExistente.IdEsquemaEvaluacion != Esquema.IdEsquemaEvaluacion)
                {
                    EsquemaEvaluacionPgeneralBO bo = new EsquemaEvaluacionPgeneralBO()
                    {
                        IdPgeneral = Esquema.IdPGeneral,
                        IdEsquemaEvaluacion = Esquema.IdEsquemaEvaluacion,
                        FechaInicio = Esquema.FechaInicio,
                        FechaFin = Esquema.FechaFin,
                        EsquemaPredeterminado = Esquema.EsquemaPredeterminado,
                        Estado = true,
                        UsuarioCreacion = Esquema.NombreUsuario,
                        UsuarioModificacion = Esquema.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    //añade la lista de detalles

                    if (Esquema.ListadoDetalleAsignacion != null && Esquema.ListadoDetalleAsignacion.Count > 0)
                    {
                        bo.ListadoDetalle = new List<EsquemaEvaluacionPgeneralDetalleBO>();
                        bo.ListadoDetalle.AddRange(Esquema.ListadoDetalleAsignacion.Select(s =>
                            new EsquemaEvaluacionPgeneralDetalleBO()
                            {
                                IdCriterioEvaluacion = s.IdCriterioEvaluacion,
                                IdProveedor = s.IdProveedor,
                                Nombre = s.Nombre,
                                UrlArchivoInstrucciones = s.UrlArchivoInstrucciones,

                                Estado = true,
                                UsuarioCreacion = Esquema.NombreUsuario,
                                UsuarioModificacion = Esquema.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            }));
                    }
                    //añade la lista de modalidad
                    if (Esquema.IdModalidad != null && Esquema.IdModalidad.Count > 0)
                    {
                        bo.ListadoModalidad = new List<EsquemaEvaluacionPgeneralModalidadBO>();
                        bo.ListadoModalidad.AddRange(Esquema.IdModalidad.Select(s =>
                            new EsquemaEvaluacionPgeneralModalidadBO()
                            {
                                IdModalidadCurso = s,

                                Estado = true,
                                UsuarioCreacion = Esquema.NombreUsuario,
                                UsuarioModificacion = Esquema.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            }));
                    }
                    //añade la lista de modalidad
                    if (Esquema.IdProveedor != null && Esquema.IdProveedor.Count > 0)
                    {
                        bo.ListadoProveedor = new List<EsquemaEvaluacionPgeneralProveedorBO>();
                        bo.ListadoProveedor.AddRange(Esquema.IdProveedor.Select(s =>
                            new EsquemaEvaluacionPgeneralProveedorBO()
                            {
                                IdProveedor = s,

                                Estado = true,
                                UsuarioCreacion = Esquema.NombreUsuario,
                                UsuarioModificacion = Esquema.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            }));
                    }
                    if (!bo.HasErrors)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {

                            nuevoIdEsquema = _repoAsignacion.Update(bo, estatusDetalle, estatusModalidad, estatusproveedor);
                            _repoAsignacion.Delete(Esquema.Id, Esquema.NombreUsuario);
                            scope.Complete();
                        }

                    }
                    else
                    {
                        return BadRequest(bo.GetErrors());
                    }

                    if (!estatusDetalle)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            _repoAsignacion.UpdateEsquemaEvaluacionPgeneralDetalle(Esquema, true);
                            scope.Complete();
                        }
                    }
                    if (!estatusModalidad)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            _repoAsignacion.UpdateEsquemaEvaluacionPgeneralModelo(Esquema, true);
                            scope.Complete();
                        }
                    }
                    if (!estatusproveedor)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            _repoAsignacion.UpdateEsquemaEvaluacionPgeneralProveedor(Esquema, true);
                            scope.Complete();
                        }
                    }
                  
                }
                else {
                    EsquemaEvaluacionPgeneralBO esquemaEvaluacionUpdate = new EsquemaEvaluacionPgeneralBO();
                    esquemaEvaluacionUpdate= _repoAsignacion.FirstById((int)Esquema.Id);
                    esquemaEvaluacionUpdate.IdEsquemaEvaluacion = Esquema.IdEsquemaEvaluacion;
                    esquemaEvaluacionUpdate.FechaInicio = Esquema.FechaInicio;
                    esquemaEvaluacionUpdate.FechaFin = Esquema.FechaFin;
                    esquemaEvaluacionUpdate.IdPgeneral = Esquema.IdPGeneral;
                    esquemaEvaluacionUpdate.EsquemaPredeterminado= Esquema.EsquemaPredeterminado;
                    esquemaEvaluacionUpdate.UsuarioModificacion = Esquema.NombreUsuario;
                    esquemaEvaluacionUpdate.FechaModificacion = DateTime.Now;
                    if (!esquemaEvaluacionUpdate.HasErrors)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            _repoAsignacion.Update(esquemaEvaluacionUpdate, false, false, false);
                            scope.Complete();
                        }
                        if (estatusDetalle)
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                _repoAsignacion.UpdateEsquemaEvaluacionPgeneralDetalle(Esquema, false);
                                scope.Complete();
                            }
                        }
                        if (estatusModalidad)
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                _repoAsignacion.UpdateEsquemaEvaluacionPgeneralModelo(Esquema, false);
                                scope.Complete();
                            }
                        }
                        if (estatusproveedor)
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                _repoAsignacion.UpdateEsquemaEvaluacionPgeneralProveedor(Esquema, false);
                                scope.Complete();
                            }
                        }

                    }
                }
                if ((boExistente.EsquemaPredeterminado == null || boExistente.EsquemaPredeterminado == false) && Esquema.EsquemaPredeterminado == true) 
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        _repoEsquema.UpdateEsquemaEvaluacionPredefinido(Esquema.Id);
                        scope.Complete();
                    }
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idEsquemaAsignado}")]
        [HttpGet]
        public ActionResult ObtenerDetalleEsquemaAsignado(int idEsquemaAsignado)
        {
            try
            {
                EsquemaEvaluacionPgeneralDetalleRepositorio _repodetalleEsquemaAignado = new EsquemaEvaluacionPgeneralDetalleRepositorio();
                var listado = _repodetalleEsquemaAignado.GetBy(w => w.IdEsquemaEvaluacionPgeneral == idEsquemaAsignado,
                    s => new
                    {
                        s.Id,
                        s.IdCriterioEvaluacion,
                        s.IdProveedor,
                        NombreCriterioEvaluacion = s.IdCriterioEvaluacionNavigation.Nombre,
                        s.Nombre,
                        s.UrlArchivoInstrucciones
                    });

                var lisatdoFinal = listado.GroupBy(g => g.IdCriterioEvaluacion)
                    .Select(s => new {IdCriterioEvaluacion = s.Key, Detalle = s.ToList()});
                return Ok(lisatdoFinal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ListadoProgramasDocenteFiltrado([FromBody] ActividadEvaluacionFiltroDTO filtro)
        {
            try
            {
                var listado = _repoEsquema.ListadoProgramasDocenteFiltrado(filtro);

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ListadoAlumnosCalificarPorPespecifico([FromBody] ListadoAlumnoCalificarFiltroDTO filtro)
        {
            try
            {
                //var listado = _repoEsquema.ListadoAlumnosCalificarPorPespecifico(filtro.IdPespecifico, filtro.Grupo);
                var listado = _repoEsquema.ListadoAlumnosCalificarPorPespecificoAlterno(filtro.IdPespecifico, filtro.Grupo);

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdPespecifico}")]
        [HttpGet]
        public ActionResult ListadoAlumnosCalificarPorPespecificoCongelado(int IdPespecifico)
        {
            try
            {
                //var listado = _repoEsquema.ListadoAlumnosCalificarPorPespecifico(filtro.IdPespecifico, filtro.Grupo);
                var listado = _repoEsquema.ListadoAlumnosCalificarPorPespecificoCongelado(IdPespecifico);

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Route("[action]/{idAlumno}")]
        [HttpPost]
        public ActionResult ListaCursosAplicaProyectoAnterior(int idAlumno)
        {
            try
            {
                var listado = _repoEsquema.ListaCursosAplicaProyectoAnterior(idAlumno);
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Max M. y Renato E.
        /// Fecha: 15/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Congela Matriculas Cabecera
        /// </summary>
        /// <returns>int<returns>

        [Route("[action]")]
        [HttpGet]
        public ActionResult CongelarMatriculaCabecera()
        {
            try
            {
                
                int listado = _repoEjemplo.CongelarMatricula();
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Max M. y Renato E.
        /// Fecha: 15/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Insertar Matriculas Cabecera
        /// </summary>
        /// <param name=”json”>DTO que tiene el id al insertar en la tabla</param>
        /// <returns>int<returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarMatriculaCabecera([FromBody] List<ValorIdMatriculaDTO> json)
        {

            try
            {
                var esquemaEvaluacionBO = new EsquemaEvaluacionBO();
                var listado = esquemaEvaluacionBO.InsertarMatricula(json);
                return Ok(listado);
            }
            catch (Exception e)
                {
                return BadRequest(e.Message);
            }
        }
    
        [Route("[action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerEsquemaEvaluacionPorMatricula(int IdMatriculaCabecera)
        {
            try
            {

                var esquemaEvaluacionBO = new EsquemaEvaluacionBO();

                var listado = _repoEsquema.ObtenerCongelamientoEsquemaPorMatricula(IdMatriculaCabecera);
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerNombreEsquemaEvaluacionPorMatricula(int IdMatriculaCabecera)
        {
            try
            {

                var esquemaEvaluacionBO = new EsquemaEvaluacionBO();

                var listado = _repoEsquema.ObtenerNombreCongelamientoEsquemaPorMatricula(IdMatriculaCabecera);
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarCongelamientoEsquemaEvaluacion(EditarCongelamientoPEspecificoMatriculaAlumnoDTO Json)
        {
            try
            {

                var esquemaEvaluacionBO = new EsquemaEvaluacionBO();

                var listado = _repoEsquema.ActualizarCongelamientoEsquemaPorMatricula(Json);
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
