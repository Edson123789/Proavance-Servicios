using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaterialEnvio")]
    public class MaterialEnvioController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialEnvioRepositorio _repMaterialEnvio;
        private readonly MaterialEnvioDetalleRepositorio _repMaterialEnvioDetalle;
        private readonly MaterialEstadoRecepcionRepositorio _repMaterialEstadoRecepcion;
        private readonly SedeTrabajoRepositorio _repSedeTrabajo;
        private readonly PersonalRepositorio _repPersonal;
        private readonly ProveedorRepositorio _repProveedor;
        private readonly MaterialVersionRepositorio _repMaterialVersion;
        private readonly PespecificoRepositorio _repPEspecifico;
        public MaterialEnvioController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repMaterialEstadoRecepcion = new MaterialEstadoRecepcionRepositorio(_integraDBContext);
            _repMaterialEnvio = new MaterialEnvioRepositorio(_integraDBContext);
            _repMaterialEnvioDetalle = new MaterialEnvioDetalleRepositorio(_integraDBContext);
            _repSedeTrabajo = new SedeTrabajoRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repProveedor = new ProveedorRepositorio(_integraDBContext);
            _repMaterialVersion = new MaterialVersionRepositorio(_integraDBContext);
            _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                return Ok(_repMaterialEnvio.Obtener());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFiltros()
        {
            try
            {

                var idTipoServicio_envio = 2;

                var listaSedeTrabajo = _repSedeTrabajo.ObtenerTodoFiltro();
                var listaPersonal = _repPersonal.ObtenerTodoFiltro();
                var listaProveedorEnvio = _repProveedor.ObtenerTodoFiltroPorTipoServicio(idTipoServicio_envio);
                var listaMaterialEstadoRecepcion = _repMaterialEstadoRecepcion.ObtenerTodoFiltro();
                //var listaMaterialVersion = _repMaterialVersion.ObtenerTodoFiltro();
                var listaMaterialVersion = _repMaterialVersion.ObtenerTodoFiltroPorProgramaEspecificoGrupo();
                var listaGrupo = _repPEspecifico.ObtenerGrupoSesiones();
                var listaPEspecificoPadre = _repPEspecifico.ObtenerPadres();
                var listaPEspecificoHijo = _repPEspecifico.ObtenerHijos();

                var listaFiltros = new
                {
                    ListaSedeTrabajo = listaSedeTrabajo,
                    ListaPersonal = listaPersonal,
                    ListaProveedorEnvio = listaProveedorEnvio,
                    ListaMaterialEstadoRecepcion = listaMaterialEstadoRecepcion,
                    ListaMaterialVersion = listaMaterialVersion,

                    ListaGrupo = listaGrupo,
                    ListaPEspecificoPadre = listaPEspecificoPadre,
                    ListaPEspecificoHijo = listaPEspecificoHijo
                };
                return Ok(listaFiltros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDetalle(int Id)
        {
            try
            {
                if (!_repMaterialEnvio.Exist(Id))
                {
                    return BadRequest("Material envio no existente!");
                }
                return Ok(_repMaterialEnvioDetalle.ObtenerDetalle(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Route("[action]")]
        //[HttpPost]
        //public ActionResult Insertar([FromBody] MaterialEnvioDetalleCompletoDTO MaterialEnvioDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        if (!_repSedeTrabajo.Exist(MaterialEnvioDTO.MaterialEnvio.IdSedeTrabajo))
        //        {
        //            return BadRequest("Sede trabajo inexistente");
        //        }
        //        if (!_repPersonal.Exist(MaterialEnvioDTO.MaterialEnvio.IdPersonalRemitente))
        //        {
        //            return BadRequest("Personal inexistente");
        //        }
        //        if (!_repProveedor.Exist(MaterialEnvioDTO.MaterialEnvio.IdProveedorEnvio))
        //        {
        //            return BadRequest("Proveedor inexistente");
        //        }

        //        var materialEnvio = new MaterialEnvioBO()
        //        {
        //            IdSedeTrabajo = MaterialEnvioDTO.MaterialEnvio.IdSedeTrabajo,
        //            IdPersonalRemitente = MaterialEnvioDTO.MaterialEnvio.IdPersonalRemitente,
        //            IdProveedorEnvio = MaterialEnvioDTO.MaterialEnvio.IdProveedorEnvio,
        //            FechaEnvio = MaterialEnvioDTO.MaterialEnvio.FechaEnvio,
        //            Estado = true,
        //            UsuarioCreacion = MaterialEnvioDTO.MaterialEnvio.NombreUsuario,
        //            UsuarioModificacion = MaterialEnvioDTO.MaterialEnvio.NombreUsuario,
        //            FechaCreacion = DateTime.Now,
        //            FechaModificacion = DateTime.Now
        //        };
        //        MaterialEnvioDetalleBO materialEnvioDetalle;
        //        foreach (var item in MaterialEnvioDTO.ListaMaterialEnvioDetalle)
        //        {
        //            materialEnvioDetalle = new MaterialEnvioDetalleBO()
        //            {
        //                IdMaterialVersion = item.IdMaterialVersion,
        //                IdMaterialEstadoRecepcion = item.IdMaterialEstadoRecepcion,
        //                IdPersonalReceptor = item.IdPersonalReceptor,
        //                CantidadEnvio = item.CantidadEnvio,
        //                CantidadRecepcion = 0,
        //                ComentarioEnvio = item.ComentarioEnvio,
        //                ComentarioRecepcion = "",
        //                Estado = true,
        //                UsuarioCreacion = MaterialEnvioDTO.NombreUsuario,
        //                UsuarioModificacion = MaterialEnvioDTO.NombreUsuario,
        //                FechaCreacion = DateTime.Now,
        //                FechaModificacion = DateTime.Now
        //            };
        //            materialEnvio.ListaMaterialEnvioDetalle.Add(materialEnvioDetalle);
        //        }
        //        if (!materialEnvio.HasErrors)
        //        {
        //            _repMaterialEnvio.Insert(materialEnvio);
        //        }
        //        else {
        //            return BadRequest(materialEnvio.GetErrors());
        //        }
        //        return Ok(materialEnvio);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] MaterialEnvioDTO MaterialEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repSedeTrabajo.Exist(MaterialEnvio.IdSedeTrabajo))
                {
                    return BadRequest("Sede trabajo inexistente");
                }
                if (!_repPersonal.Exist(MaterialEnvio.IdPersonalRemitente))
                {
                    return BadRequest("Personal inexistente");
                }
                if (!_repProveedor.Exist(MaterialEnvio.IdProveedorEnvio))
                {
                    return BadRequest("Proveedor inexistente");
                }

                var materialEnvio = new MaterialEnvioBO()
                {
                    IdSedeTrabajo = MaterialEnvio.IdSedeTrabajo,
                    IdPersonalRemitente = MaterialEnvio.IdPersonalRemitente,
                    IdProveedorEnvio = MaterialEnvio.IdProveedorEnvio,
                    FechaEnvio = MaterialEnvio.FechaEnvio,
                    Estado = true,
                    UsuarioCreacion = MaterialEnvio.NombreUsuario,
                    UsuarioModificacion = MaterialEnvio.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                MaterialEnvioDetalleBO materialEnvioDetalle;
                foreach (var item in MaterialEnvio.ListaMaterialEnvioDetalle)
                {
                    materialEnvioDetalle = new MaterialEnvioDetalleBO()
                    {
                        IdMaterialVersion = item.IdMaterialVersion,
                        IdMaterialEstadoRecepcion = item.IdMaterialEstadoRecepcion,
                        IdPersonalReceptor = item.IdPersonalReceptor,
                        CantidadEnvio = item.CantidadEnvio,
                        CantidadRecepcion = 0,
                        ComentarioEnvio = item.ComentarioEnvio,
                        ComentarioRecepcion = "",
                        Estado = true,
                        UsuarioCreacion = MaterialEnvio.NombreUsuario,
                        UsuarioModificacion = MaterialEnvio.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    materialEnvio.ListaMaterialEnvioDetalle.Add(materialEnvioDetalle);
                }
                if (!materialEnvio.HasErrors)
                {
                    _repMaterialEnvio.Insert(materialEnvio);
                }
                else
                {
                    return BadRequest(materialEnvio.GetErrors());
                }
                return Ok(materialEnvio);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] MaterialEnvioDTO MaterialEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialEnvio.Exist(MaterialEnvio.Id))
                {
                    return BadRequest("Material envio inexistente");
                }
                if (!_repSedeTrabajo.Exist(MaterialEnvio.IdSedeTrabajo))
                {
                    return BadRequest("Sede trabajo inexistente");
                }
                if (!_repPersonal.Exist(MaterialEnvio.IdPersonalRemitente))
                {
                    return BadRequest("Personal inexistente");
                }
                if (!_repProveedor.Exist(MaterialEnvio.IdProveedorEnvio))
                {
                    return BadRequest("Proveedor inexistente");
                }
                var materialEnvio = _repMaterialEnvio.FirstById(MaterialEnvio.Id);
                materialEnvio.IdSedeTrabajo = MaterialEnvio.IdSedeTrabajo;
                materialEnvio.IdPersonalRemitente = MaterialEnvio.IdPersonalRemitente;
                materialEnvio.IdProveedorEnvio = MaterialEnvio.IdProveedorEnvio;
                materialEnvio.FechaEnvio = MaterialEnvio.FechaEnvio;
                materialEnvio.UsuarioModificacion = MaterialEnvio.NombreUsuario;
                materialEnvio.FechaModificacion = DateTime.Now;

                var idsDebenContinuar = MaterialEnvio.ListaMaterialEnvioDetalle.Where(x => x.Id != 0).Select(x => x.Id).ToList();
                var idsExisten = _repMaterialEnvioDetalle.GetBy(x => x.IdMaterialEnvio == materialEnvio.Id).Select(x => x.Id).ToList();
                var idsEliminar = idsExisten.Where(x => !idsDebenContinuar.Contains(x));
                //eliminamos los que deben ser eliminados
                _repMaterialEnvioDetalle.Delete(idsEliminar, MaterialEnvio.NombreUsuario);


                MaterialEnvioDetalleBO materialEnvioDetalle;
                foreach (var item in MaterialEnvio.ListaMaterialEnvioDetalle)
                {
                    if (item.Id == 0)// no existe
                    {
                        materialEnvioDetalle = new MaterialEnvioDetalleBO()
                        {
                            IdMaterialVersion = item.IdMaterialVersion,
                            IdMaterialEstadoRecepcion = item.IdMaterialEstadoRecepcion,
                            IdPersonalReceptor = item.IdPersonalReceptor,
                            CantidadEnvio = item.CantidadEnvio,
                            CantidadRecepcion = 0,
                            ComentarioEnvio = item.ComentarioEnvio,
                            ComentarioRecepcion = "",
                            Estado = true,
                            UsuarioCreacion = MaterialEnvio.NombreUsuario,
                            UsuarioModificacion = MaterialEnvio.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    else {
                        if (!_repMaterialEnvioDetalle.Exist(item.Id))
                        {
                            return BadRequest("Detalle no existe");
                        }

                        materialEnvioDetalle = _repMaterialEnvioDetalle.FirstById(item.Id);
                        materialEnvioDetalle.IdMaterialVersion = item.IdMaterialVersion;
                        materialEnvioDetalle.IdPersonalReceptor = item.IdPersonalReceptor;
                        materialEnvioDetalle.IdMaterialEstadoRecepcion = item.IdMaterialEstadoRecepcion;
                        materialEnvioDetalle.CantidadEnvio = item.CantidadEnvio;
                        materialEnvioDetalle.ComentarioEnvio = item.ComentarioEnvio;
                        materialEnvioDetalle.UsuarioModificacion = MaterialEnvio.NombreUsuario;
                        materialEnvioDetalle.FechaModificacion = DateTime.Now;
                    }
                    
                    materialEnvio.ListaMaterialEnvioDetalle.Add(materialEnvioDetalle);
                }
                if (!materialEnvio.HasErrors)
                {
                    _repMaterialEnvio.Update(materialEnvio);
                }
                else
                {
                    return BadRequest(materialEnvio.GetErrors());
                }
                return Ok(materialEnvio);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO MaterialEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialEnvio.Exist(MaterialEnvio.Id))
                {
                    return BadRequest("Material envio inexistente");
                }
                _repMaterialEnvio.Delete(MaterialEnvio.Id, MaterialEnvio.NombreUsuario);
                _repMaterialEnvioDetalle.Delete(_repMaterialEnvioDetalle.GetBy(x => x.IdMaterialEnvio == MaterialEnvio.Id).Select(x => x.Id),MaterialEnvio.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarEstadoRecepcion([FromBody] MaterialEnvioDTO MaterialEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialEnvio.Exist(MaterialEnvio.Id))
                {
                    return BadRequest("Material envio inexistente");
                }
                var materialEnvio = _repMaterialEnvio.FirstById(MaterialEnvio.Id);

                MaterialEnvioDetalleBO materialEnvioDetalle;
                foreach (var item in MaterialEnvio.ListaMaterialEnvioDetalle)
                {
                    if (!_repMaterialEnvioDetalle.Exist(item.Id))
                    {
                        return BadRequest("Detalle no existe");
                    }
                    materialEnvioDetalle = _repMaterialEnvioDetalle.FirstById(item.Id);
                    materialEnvioDetalle.IdMaterialEstadoRecepcion = item.IdMaterialEstadoRecepcion;
                    materialEnvioDetalle.CantidadRecepcion = item.CantidadRecepcion;
                    materialEnvioDetalle.ComentarioRecepcion = item.ComentarioRecepcion;
                    materialEnvioDetalle.UsuarioModificacion = MaterialEnvio.NombreUsuario;
                    materialEnvioDetalle.FechaModificacion = DateTime.Now;
                    materialEnvio.ListaMaterialEnvioDetalle.Add(materialEnvioDetalle);
                }
                if (!materialEnvio.HasErrors)
                {
                    _repMaterialEnvio.Update(materialEnvio);
                }
                else
                {
                    return BadRequest(materialEnvio.GetErrors());
                }
                return Ok(materialEnvio);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}