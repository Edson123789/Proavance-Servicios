using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaterialPespecificoDetalle")]
    public class MaterialPespecificoDetalleController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialTipoEntregaRepositorio _repMaterialTipoEntrega;
        private readonly MaterialTipoRepositorio _repMaterialTipo;
        private readonly MaterialPespecificoRepositorio _repMaterialPespecifico;
        private readonly MaterialPespecificoDetalleRepositorio _repMaterialPespecificoDetalle;
        private readonly PespecificoRepositorio _repPespecifico;
        private readonly PespecificoSesionRepositorio _repPespecificoSesion;
        private readonly MaterialVersionRepositorio _repMaterialVersion;
        private readonly MaterialAsociacionVersionRepositorio _repMaterialAsociacionVersion;

        public MaterialPespecificoDetalleController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repMaterialTipoEntrega = new MaterialTipoEntregaRepositorio(_integraDBContext);
            _repMaterialTipo = new MaterialTipoRepositorio(_integraDBContext);
            _repMaterialPespecifico = new MaterialPespecificoRepositorio(_integraDBContext);
            _repPespecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
            _repPespecifico = new PespecificoRepositorio(_integraDBContext);
            _repMaterialVersion = new MaterialVersionRepositorio(_integraDBContext);
            _repMaterialAsociacionVersion = new MaterialAsociacionVersionRepositorio(_integraDBContext);
            _repMaterialPespecificoDetalle = new MaterialPespecificoDetalleRepositorio(_integraDBContext);
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
                return Ok(_repMaterialTipo.Obtener());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDetalle(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialTipo.Exist(Id))
                {
                    return BadRequest("Tipo material no existente");
                }
                return Ok(Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] MaterialPespecificoDTO MaterialPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialTipo.Exist(MaterialPespecifico.IdMaterialTipo))
                {
                    return BadRequest("Tipo de material no existente!");
                }

                if (MaterialPespecifico.GrupoEdicion == 0)
                {
                    MaterialPespecifico.GrupoEdicion = _repMaterialPespecifico.ObtenerProximoGrupoEdicion(MaterialPespecifico.IdPEspecifico);
                }

                var materialPEspecifico = new MaterialPespecificoBO()
                {
                    IdMaterialTipo = MaterialPespecifico.IdMaterialTipo,
                    IdPespecifico = MaterialPespecifico.IdPEspecifico,
                    Grupo = MaterialPespecifico.Grupo,
                    GrupoEdicion = MaterialPespecifico.GrupoEdicion,
                    GrupoEdicionOrden = MaterialPespecifico.GrupoEdicionOrden,
                    IdFur = null,
                    UsuarioCreacion = MaterialPespecifico.NombreUsuario,
                    UsuarioModificacion = MaterialPespecifico.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };

                //IdMaterialTipo = MaterialPespecifico.IdMaterialTipo,
                //Insertamos todas las versiones del tipo de material
                var idMaterialEstado_PorEditar = 0;

                var listaMaterialAsociacionVersion = _repMaterialAsociacionVersion.GetBy(x => x.IdMaterialTipo == MaterialPespecifico.IdMaterialTipo);

                MaterialPespecificoDetalleBO materialPespecificoDetalle;
                foreach (var item in listaMaterialAsociacionVersion)
                {
                    materialPespecificoDetalle = new MaterialPespecificoDetalleBO()
                    {
                        IdMaterialEstado = idMaterialEstado_PorEditar,
                        ComentarioSubida = "",
                        UrlArchivo = "",
                        FechaSubida = null,
                        IdFur = null,
                        DireccionEntrega = "",
                        FechaEntrega = null,
                        NombreArchivo = "",
                        IdMaterialVersion = item.IdMaterialVersion,
                        UsuarioCreacion = MaterialPespecifico.NombreUsuario,
                        UsuarioModificacion = MaterialPespecifico.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                    materialPEspecifico.ListaMaterialPespecificoDetalle.Add(materialPespecificoDetalle);
                }

                if (!materialPEspecifico.HasErrors)
                {
                    _repMaterialPespecifico.Insert(materialPEspecifico);
                }
                else
                {
                    return BadRequest(materialPEspecifico.GetErrors());
                }
                var listaGrupoEdicion = _repPespecifico.ObtenerGrupoEdicionDisponible(MaterialPespecifico.IdPEspecifico);
                var filtros = new
                {
                    ListaGrupoEdicion = listaGrupoEdicion
                };
                return Ok(filtros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] MaterialPespecificoDTO MaterialPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecifico.Exist(MaterialPespecifico.Id))
                {
                    return BadRequest("Material por programa especifico no existente!");
                }
                if (!_repMaterialTipo.Exist(MaterialPespecifico.IdMaterialTipo))
                {
                    return BadRequest("Tipo de material no existente!");
                }

                var materialPespecifico = _repMaterialPespecifico.FirstById(MaterialPespecifico.Id);
                materialPespecifico.IdMaterialTipo = MaterialPespecifico.IdMaterialTipo;
                materialPespecifico.Grupo = MaterialPespecifico.Grupo;
                materialPespecifico.GrupoEdicion = MaterialPespecifico.GrupoEdicion;
                materialPespecifico.GrupoEdicionOrden = MaterialPespecifico.GrupoEdicionOrden;
                materialPespecifico.UsuarioModificacion = MaterialPespecifico.NombreUsuario;
                materialPespecifico.FechaModificacion = DateTime.Now;
                if (!materialPespecifico.HasErrors)
                {
                    _repMaterialPespecifico.Update(materialPespecifico);
                }
                else
                {
                    return BadRequest(materialPespecifico.GetErrors());
                }
                var listaGrupoEdicion = _repPespecifico.ObtenerGrupoEdicionDisponible(MaterialPespecifico.IdPEspecifico);
                var filtros = new
                {
                    ListaGrupoEdicion = listaGrupoEdicion
                };
                return Ok(filtros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO MaterialPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecifico.Exist(MaterialPespecifico.Id))
                {
                    return BadRequest("Material por programa especifico no existente");
                }
                _repMaterialPespecifico.Delete(MaterialPespecifico.Id, MaterialPespecifico.NombreUsuario);
                //_repMaterialVersion.Delete(_repMaterialVersion.GetBy(x => x.IdMaterialPespecificoSesion == MaterialPespecificoSesion.Id).Select(x => x.Id), MaterialPespecificoSesion.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult SubirMaterialArchivo(MaterialPEspecificoDetalleDTO MaterialPEspecificoDetalle, [FromForm] IList<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecificoDetalle.Exist(MaterialPEspecificoDetalle.Id))
                {
                    return BadRequest("Version no existente!");
                }

                string NombreArchivo = "";
                string ContentType = "";
                var urlArchivoRepositorio = "";
                var materialPespecificoDetalle = _repMaterialPespecificoDetalle.FirstById(MaterialPEspecificoDetalle.Id);
                var materialPEspecifico = _repMaterialPespecifico.FirstById(materialPespecificoDetalle.IdMaterialPespecifico);

                if (Files != null)
                {
                    foreach (var file in Files)
                    {
                        ContentType = file.ContentType;
                        NombreArchivo = file.FileName;
                        NombreArchivo = string.Concat(materialPespecificoDetalle.Id, "-", materialPEspecifico.IdPespecifico, "-", NombreArchivo);
                        urlArchivoRepositorio = _repMaterialVersion.SubirArchivoRepositorio(file.ConvertToByte(), file.ContentType, NombreArchivo);
                    }
                }

                materialPespecificoDetalle.UrlArchivo = urlArchivoRepositorio;
                materialPespecificoDetalle.IdMaterialEstado = 2;//editado
                materialPespecificoDetalle.NombreArchivo = NombreArchivo;
                materialPespecificoDetalle.FechaSubida = DateTime.Now;
                materialPespecificoDetalle.ComentarioSubida = MaterialPEspecificoDetalle.ComentarioSubida;
                materialPespecificoDetalle.UsuarioSubida = MaterialPEspecificoDetalle.NombreUsuario;
                materialPespecificoDetalle.UsuarioModificacion = MaterialPEspecificoDetalle.NombreUsuario;

                materialPespecificoDetalle.FechaModificacion = DateTime.Now;
                if (!materialPespecificoDetalle.HasErrors)
                {
                    _repMaterialPespecificoDetalle.Update(materialPespecificoDetalle);
                }
                else
                {
                    return BadRequest(materialPespecificoDetalle.GetErrors());
                }
                return Ok(materialPespecificoDetalle);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}