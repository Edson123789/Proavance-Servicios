using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaterialPEspecificoSesion")]
    public class MaterialPEspecificoSesionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialTipoEntregaRepositorio _repMaterialTipoEntrega;
        private readonly MaterialTipoRepositorio _repMaterialTipo;
        private readonly MaterialPespecificoSesionRepositorio _repMaterialPespecificoSesion;
        private readonly PespecificoSesionRepositorio _repPespecificoSesion;
        private readonly MaterialVersionRepositorio _repMaterialVersion;

        public MaterialPEspecificoSesionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repMaterialTipoEntrega = new MaterialTipoEntregaRepositorio(_integraDBContext);
            _repMaterialTipo = new MaterialTipoRepositorio(_integraDBContext);
            _repMaterialPespecificoSesion = new MaterialPespecificoSesionRepositorio(_integraDBContext);
            _repPespecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
            _repMaterialVersion = new MaterialVersionRepositorio(_integraDBContext);
        }

        [Route("[Action]/{IdMaterialPEspecificoSesion}")]
        [HttpGet]
        public ActionResult ObtenerMaterialVersion(int IdMaterialPEspecificoSesion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecificoSesion.Exist(IdMaterialPEspecificoSesion))
                {
                    return BadRequest("Material no existente");
                }
                //return Ok(_repMaterialVersion.ObtenerPorMaterialPEspecificoSesion(IdMaterialPEspecificoSesion));
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPEspecificoSesion}")]
        [HttpGet]
        public ActionResult ObtenerPorPEspecificoSesion(int IdPEspecificoSesion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repPespecificoSesion.Exist(IdPEspecificoSesion))
                {
                    return BadRequest("Sesion no existente");
                }
                return Ok(_repMaterialPespecificoSesion.ObtenerPorPEspecificoSesion(IdPEspecificoSesion));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
        public ActionResult Insertar([FromBody] MaterialPespecificoSesionDTO MaterialPespecificoSesion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialTipo.Exist(MaterialPespecificoSesion.IdMaterialTipo))
                {
                    return BadRequest("Tipo de material no existente!");
                }
                if (!_repPespecificoSesion.Exist(MaterialPespecificoSesion.IdPEspecificoSesion))
                {
                    return BadRequest("Sesion no existente");
                }
                var materialPEspecificoSesion = new MaterialPespecificoSesionBO()
                {
                    Nombre = MaterialPespecificoSesion.Nombre,
                    Descripcion = MaterialPespecificoSesion.Descripcion,
                    IdFur = null,
                    IdMaterialTipo = MaterialPespecificoSesion.IdMaterialTipo,
                    IdPespecificoSesion = MaterialPespecificoSesion.IdPEspecificoSesion,
                    UsuarioCreacion = MaterialPespecificoSesion.NombreUsuario,
                    UsuarioModificacion = MaterialPespecificoSesion.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };

                //var listaTipoPersona = new List<int>() { 0, 1, 3, 4};//0 Es archivo original
                var listaTipoPersona = new List<int>() { 0 };//0 Es archivo original
                MaterialVersionBO materialVersion;
                foreach (var item in listaTipoPersona)
                {
                    materialVersion = new MaterialVersionBO()
                    {
                        //NombreArchivo = MaterialPespecificoSesion.Nombre,
                        //UrlArchivo = "",
                        ////IdMaterialPespecificoSesion,
                        //IdMaterialEstado = 1,
                        //IdTipoPersona = item,
                        //IdProveedor = null,
                        //ComentarioSubida = "",
                        //FechaSubida = null,
                        UsuarioCreacion = MaterialPespecificoSesion.NombreUsuario,
                        UsuarioModificacion = MaterialPespecificoSesion.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                    materialPEspecificoSesion.ListaMaterialVersion.Add(materialVersion);
                }
                if (!materialPEspecificoSesion.HasErrors)
                {
                    _repMaterialPespecificoSesion.Insert(materialPEspecificoSesion);
                }
                else
                {
                    return BadRequest(materialPEspecificoSesion.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] MaterialPespecificoSesionDTO MaterialPespecificoSesion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecificoSesion.Exist(MaterialPespecificoSesion.Id))
                {
                    return BadRequest("Tipo material no existente!");
                }
                if (!_repMaterialTipo.Exist(MaterialPespecificoSesion.IdMaterialTipo))
                {
                    return BadRequest("Tipo de material no existente!");
                }
                if (!_repPespecificoSesion.Exist(MaterialPespecificoSesion.IdPEspecificoSesion))
                {
                    return BadRequest("Sesion no existente");
                }
                var materialPespecificoSesion = _repMaterialPespecificoSesion.FirstById(MaterialPespecificoSesion.Id);
                materialPespecificoSesion.Nombre = MaterialPespecificoSesion.Nombre;
                materialPespecificoSesion.Descripcion = MaterialPespecificoSesion.Descripcion;
                materialPespecificoSesion.IdMaterialTipo = MaterialPespecificoSesion.IdMaterialTipo;
                //materialPespecificoSesion.IdPespecificoSesion = MaterialPespecificoSesion.IdPEspecificoSesion;
                //materialPespecificoSesion.IdFur = MaterialPespecificoSesion.IdFur;
                materialPespecificoSesion.UsuarioModificacion = MaterialPespecificoSesion.NombreUsuario;
                materialPespecificoSesion.FechaModificacion = DateTime.Now;
                if (!materialPespecificoSesion.HasErrors)
                {
                    _repMaterialPespecificoSesion.Update(materialPespecificoSesion);
                }
                else
                {
                    return BadRequest(materialPespecificoSesion.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO MaterialPespecificoSesion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecificoSesion.Exist(MaterialPespecificoSesion.Id))
                {
                    return BadRequest("Material por sesion no existente");
                }
                _repMaterialPespecificoSesion.Delete(MaterialPespecificoSesion.Id, MaterialPespecificoSesion.NombreUsuario);
                //_repMaterialVersion.Delete(_repMaterialVersion.GetBy(x => x.IdMaterialPespecificoSesion == MaterialPespecificoSesion.Id).Select(x => x.Id), MaterialPespecificoSesion.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}