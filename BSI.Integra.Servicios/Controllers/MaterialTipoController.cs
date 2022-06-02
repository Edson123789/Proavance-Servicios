using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaterialTipo")]
    public class MaterialTipoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialTipoEntregaRepositorio _repMaterialTipoEntrega;
        private readonly MaterialTipoRepositorio _repMaterialTipo;

        private readonly MaterialAsociacionVersionRepositorio _repMaterialAsociacionVersion;
        private readonly MaterialAsociacionAccionRepositorio _repMaterialAsociacionAccion;
        private readonly MaterialAsociacionCriterioVerificacionRepositorio _repMaterialAsociacionCriterioVerificacion;

        public MaterialTipoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repMaterialTipoEntrega = new MaterialTipoEntregaRepositorio(_integraDBContext);
            _repMaterialTipo = new MaterialTipoRepositorio(_integraDBContext);
            _repMaterialAsociacionVersion = new MaterialAsociacionVersionRepositorio(_integraDBContext);
            _repMaterialAsociacionAccion = new MaterialAsociacionAccionRepositorio(_integraDBContext);
            _repMaterialAsociacionCriterioVerificacion = new MaterialAsociacionCriterioVerificacionRepositorio(_integraDBContext);
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

                var _repMaterialVersion = new MaterialVersionRepositorio(_integraDBContext);
                var _repMaterialAccion = new MaterialAccionRepositorio(_integraDBContext);
                var _repMaterialCriterioVerificacion = new MaterialCriterioVerificacionRepositorio(_integraDBContext);

                var filtros = new {
                    ListaMaterialVersion = _repMaterialVersion.ObtenerTodoFiltro(),
                    ListaMaterialAccion = _repMaterialAccion.ObtenerTodoFiltro(),
                    ListaMaterialCriterioVerificacion = _repMaterialCriterioVerificacion.ObtenerTodoFiltro()
                };
                return Ok(filtros);
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
                var listaMaterialTipo = _repMaterialTipo.Obtener();

                foreach (var item in listaMaterialTipo)
                {
                    item.ListaMaterialAsociacionVersion = _repMaterialAsociacionVersion.GetBy(x => x.IdMaterialTipo == item.Id).ToList();
                    item.ListaMaterialAsociacionAccion = _repMaterialAsociacionAccion.GetBy(x => x.IdMaterialTipo == item.Id).ToList();
                    item.ListaMaterialAsociacionCriterioVerificacion = _repMaterialAsociacionCriterioVerificacion.GetBy(x => x.IdMaterialTipo == item.Id).ToList();
                }
                return Ok(listaMaterialTipo);
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
        public ActionResult Insertar([FromBody] MaterialTipoDTO MaterialTipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //if (!_repMaterialTipoEntrega.Exist(MaterialTipo.IdMaterialTipoEntrega))
                //{
                //    return BadRequest("Material tipo entrega no existente!");
                //}
                var materialTipo = new MaterialTipoBO()
                {
                    Nombre = MaterialTipo.Nombre,
                    Descripcion = MaterialTipo.Descripcion,
                    UsuarioCreacion = MaterialTipo.NombreUsuario,
                    UsuarioModificacion = MaterialTipo.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };

                MaterialAsociacionAccionBO materialAsociacionAccion;
                foreach (var item in MaterialTipo.ListaMaterialAsociacionAccion)
                {
                    materialAsociacionAccion = new MaterialAsociacionAccionBO()
                    {
                        IdMaterialAccion = item.IdMaterialAccion,
                        UsuarioCreacion = MaterialTipo.NombreUsuario,
                        UsuarioModificacion = MaterialTipo.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                    materialTipo.ListaMaterialAsociacionAccion.Add(materialAsociacionAccion);
                }

                MaterialAsociacionCriterioVerificacionBO materialAsociacionCriterioVerificacion;
                foreach (var item in MaterialTipo.ListaMaterialAsociacionCriterioVerificacion)
                {
                    materialAsociacionCriterioVerificacion = new MaterialAsociacionCriterioVerificacionBO()
                    {
                        IdMaterialCriterioVerificacion = item.IdMaterialCriterioVerificacion,
                        UsuarioCreacion = MaterialTipo.NombreUsuario,
                        UsuarioModificacion = MaterialTipo.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                    materialTipo.ListaMaterialAsociacionCriterioVerificacion.Add(materialAsociacionCriterioVerificacion);
                }

                MaterialAsociacionVersionBO materialAsociacionVersion;
                foreach (var item in MaterialTipo.ListaMaterialAsociacionVersion)
                {
                    materialAsociacionVersion = new MaterialAsociacionVersionBO()
                    {
                        IdMaterialVersion = item.IdMaterialVersion,
                        UsuarioCreacion = MaterialTipo.NombreUsuario,
                        UsuarioModificacion = MaterialTipo.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                    materialTipo.ListaMaterialAsociacionVersion.Add(materialAsociacionVersion);
                }

                if (!materialTipo.HasErrors)
                {
                    _repMaterialTipo.Insert(materialTipo);
                }
                else
                {
                    return BadRequest(materialTipo.GetErrors());
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
        public ActionResult Actualizar([FromBody] MaterialTipoDTO MaterialTipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialTipo.Exist(MaterialTipo.Id))
                {
                    return BadRequest("Tipo de material no existente!");
                }
              
                var materialTipo = _repMaterialTipo.FirstById(MaterialTipo.Id);
                materialTipo.Nombre = MaterialTipo.Nombre;
                materialTipo.Descripcion= MaterialTipo.Descripcion;
                materialTipo.UsuarioModificacion = MaterialTipo.NombreUsuario;
                materialTipo.FechaModificacion = DateTime.Now;

                _repMaterialAsociacionVersion.EliminacionLogicoPorMaterialTipo(MaterialTipo.Id, MaterialTipo.NombreUsuario, MaterialTipo.ListaMaterialAsociacionVersion.Select(x => x.IdMaterialVersion).ToList());
                _repMaterialAsociacionAccion.EliminacionLogicoPorMaterialTipo(MaterialTipo.Id, MaterialTipo.NombreUsuario, MaterialTipo.ListaMaterialAsociacionAccion.Select(x => x.IdMaterialAccion).ToList());
                _repMaterialAsociacionCriterioVerificacion.EliminacionLogicoPorMaterialTipo(MaterialTipo.Id, MaterialTipo.NombreUsuario, MaterialTipo.ListaMaterialAsociacionCriterioVerificacion.Select(x => x.IdMaterialCriterioVerificacion).ToList());

                MaterialAsociacionVersionBO materialAsociacionVersion;
                foreach (var item in MaterialTipo.ListaMaterialAsociacionVersion)
                {
                    if (_repMaterialAsociacionVersion.Exist(x => x.IdMaterialVersion == item.IdMaterialVersion && x.IdMaterialTipo == MaterialTipo.Id))
                    {
                        materialAsociacionVersion = _repMaterialAsociacionVersion.FirstBy(x => x.IdMaterialVersion == item.IdMaterialVersion && x.IdMaterialTipo == MaterialTipo.Id);
                        materialAsociacionVersion.IdMaterialVersion = item.IdMaterialVersion;
                        materialAsociacionVersion.UsuarioModificacion = MaterialTipo.NombreUsuario;
                        materialAsociacionVersion.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        materialAsociacionVersion = new MaterialAsociacionVersionBO
                        {
                            IdMaterialVersion = item.IdMaterialVersion,
                            UsuarioCreacion = MaterialTipo.NombreUsuario,
                            UsuarioModificacion = MaterialTipo.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                    }
                    materialTipo.ListaMaterialAsociacionVersion.Add(materialAsociacionVersion);
                }

                MaterialAsociacionAccionBO materialAsociacionAccion;
                foreach (var item in MaterialTipo.ListaMaterialAsociacionAccion)
                {
                    if (_repMaterialAsociacionAccion.Exist(x => x.IdMaterialAccion == item.IdMaterialAccion && x.IdMaterialTipo == MaterialTipo.Id))
                    {
                        materialAsociacionAccion = _repMaterialAsociacionAccion.FirstBy(x => x.IdMaterialAccion == item.IdMaterialAccion && x.IdMaterialTipo == MaterialTipo.Id);
                        materialAsociacionAccion.IdMaterialAccion = item.IdMaterialAccion;
                        materialAsociacionAccion.UsuarioModificacion = MaterialTipo.NombreUsuario;
                        materialAsociacionAccion.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        materialAsociacionAccion = new MaterialAsociacionAccionBO
                        {
                            IdMaterialAccion = item.IdMaterialAccion,
                            UsuarioCreacion = MaterialTipo.NombreUsuario,
                            UsuarioModificacion = MaterialTipo.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                    }
                    materialTipo.ListaMaterialAsociacionAccion.Add(materialAsociacionAccion);
                }

                MaterialAsociacionCriterioVerificacionBO materialAsociacionCriterioVerificacion;
                foreach (var item in MaterialTipo.ListaMaterialAsociacionCriterioVerificacion)
                {
                    if (_repMaterialAsociacionCriterioVerificacion.Exist(x => x.IdMaterialCriterioVerificacion == item.IdMaterialCriterioVerificacion && x.IdMaterialTipo == MaterialTipo.Id))
                    {
                        materialAsociacionCriterioVerificacion = _repMaterialAsociacionCriterioVerificacion.FirstBy(x => x.IdMaterialCriterioVerificacion == item.IdMaterialCriterioVerificacion && x.IdMaterialTipo == MaterialTipo.Id);
                        materialAsociacionCriterioVerificacion.IdMaterialCriterioVerificacion = item.IdMaterialCriterioVerificacion;
                        materialAsociacionCriterioVerificacion.UsuarioModificacion = MaterialTipo.NombreUsuario;
                        materialAsociacionCriterioVerificacion.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        materialAsociacionCriterioVerificacion = new MaterialAsociacionCriterioVerificacionBO
                        {
                            IdMaterialCriterioVerificacion = item.IdMaterialCriterioVerificacion,
                            UsuarioCreacion = MaterialTipo.NombreUsuario,
                            UsuarioModificacion = MaterialTipo.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                    }
                    materialTipo.ListaMaterialAsociacionCriterioVerificacion.Add(materialAsociacionCriterioVerificacion);
                }

                if (!materialTipo.HasErrors)
                {
                    _repMaterialTipo.Update(materialTipo);
                }
                else
                {
                    return BadRequest(materialTipo.GetErrors());
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
        public ActionResult Eliminar([FromBody] EliminarDTO MaterialTipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialTipo.Exist(MaterialTipo.Id))
                {
                    return BadRequest("Tipo material no existente");
                }
                _repMaterialTipo.Delete(MaterialTipo.Id, MaterialTipo.NombreUsuario);
                _repMaterialAsociacionVersion.Delete(_repMaterialAsociacionVersion.GetBy(x => x.IdMaterialTipo == MaterialTipo.Id).Select(x => x.Id), MaterialTipo.NombreUsuario);
                _repMaterialAsociacionAccion.Delete(_repMaterialAsociacionAccion.GetBy(x => x.IdMaterialTipo == MaterialTipo.Id).Select(x => x.Id), MaterialTipo.NombreUsuario);
                _repMaterialAsociacionCriterioVerificacion.Delete(_repMaterialAsociacionCriterioVerificacion.GetBy(x => x.IdMaterialTipo == MaterialTipo.Id).Select(x => x.Id), MaterialTipo.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}