using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaterialEntrega")]
    public class MaterialEntregaController : ControllerBase
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
        private readonly PespecificoSesionRepositorio _repPEspecificoSesion;
        private readonly ExpositorRepositorio _repExpositor;
        private readonly AsistenciaRepositorio _repAsistencia;
        private readonly MaterialEntregaRepositorio _repMaterialEntrega;

        public MaterialEntregaController(integraDBContext integraDBContext)
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
            _repPEspecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
            _repExpositor = new ExpositorRepositorio(_integraDBContext);
            _repAsistencia = new AsistenciaRepositorio(_integraDBContext);
            _repMaterialEntrega = new MaterialEntregaRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFiltros()
        {
            try
            {
                var listaGrupo = _repPEspecifico.ObtenerGrupoSesiones();
                var listaPEspecificoPadre = _repPEspecifico.ObtenerPadres();
                var listaPEspecificoHijo = _repPEspecifico.ObtenerHijos();
                var listaExpositor = _repExpositor.ObtenerTodoFiltro();

                var listaFiltros = new
                {
                    ListaExpositor= listaExpositor,
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

        [Route("[action]/{IdPEspecificoHijo}/{IdGrupo}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoSesionPorProgramaEspecificoGrupo(int IdPEspecificoHijo, int IdGrupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repPEspecifico.ObtenerPorProgramaEspecificoGrupo(IdPEspecificoHijo, IdGrupo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdPEspecificoSesion}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoSesionPorProgramaEspecificoSesion(int IdPEspecificoSesion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repPEspecificoSesion.Exist(IdPEspecificoSesion))
                {
                    return BadRequest("Sesion no valida!");
                }
                return Ok(_repPEspecificoSesion.ObtenerAlumnosAsistencia(IdPEspecificoSesion));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdAsistencia}")]
        [HttpGet]
        public ActionResult ObtenerMaterialEntregar(int IdAsistencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repAsistencia.Exist(IdAsistencia))
                {
                    return BadRequest("Asistencia no valida!");
                }
                return Ok(_repPEspecificoSesion.ObtenerMaterialesPorAsistencia(IdAsistencia));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult MarcarAsistencia([FromBody] PEspecificoSesionCompuestoDTO PEspecificoSesion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repPEspecificoSesion.Exist(PEspecificoSesion.Id))
                {
                    return BadRequest("Sesion no existe!");
                }
               
                //var materialEnvio = _repMaterialEnvio.FirstById(MaterialEnvio.Id);
                //materialEnvio.IdSedeTrabajo = MaterialEnvio.IdSedeTrabajo;
                //materialEnvio.IdPersonalRemitente = MaterialEnvio.IdPersonalRemitente;
                //materialEnvio.IdProveedorEnvio = MaterialEnvio.IdProveedorEnvio;
                //materialEnvio.FechaEnvio = MaterialEnvio.FechaEnvio;
                //materialEnvio.UsuarioModificacion = MaterialEnvio.NombreUsuario;
                //materialEnvio.FechaModificacion = DateTime.Now;

                //var idsDebenContinuar = MaterialEnvio.ListaMaterialEnvioDetalle.Where(x => x.Id != 0).Select(x => x.Id).ToList();
                //var idsExisten = _repMaterialEnvioDetalle.GetBy(x => x.IdMaterialEnvio == materialEnvio.Id).Select(x => x.Id).ToList();
                //var idsEliminar = idsExisten.Where(x => !idsDebenContinuar.Contains(x));
                ////eliminamos los que deben ser eliminados
                //_repMaterialEnvioDetalle.Delete(idsEliminar, MaterialEnvio.NombreUsuario);

                AsistenciaBO asistencia;
                List<AsistenciaBO> listaAsistencia = new List<AsistenciaBO>(); ;
                foreach (var item in PEspecificoSesion.ListaAsistencia)
                {
                    if (item.Id == 0)// no existe
                    {
                        continue;
                    }
                    else
                    {
                        if (!_repAsistencia.Exist(item.Id))
                        {
                            return BadRequest("Detalle no existe");
                        }
                        asistencia = _repAsistencia.FirstById(item.Id);
                        asistencia.Asistio = item.Asistio;
                        asistencia.Justifico = item.Justifico;
                        asistencia.UsuarioModificacion = PEspecificoSesion.NombreUsuario;
                        asistencia.FechaModificacion = DateTime.Now;
                    }

                    listaAsistencia.Add(asistencia);
                }
                _repAsistencia.Update(listaAsistencia);
                return Ok(listaAsistencia);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult EntregarMaterial([FromBody] MaterialEntregaDetalleDTO MaterialEntrega)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                if (!_repAsistencia.Exist(MaterialEntrega.IdAsistencia))
                {
                    return BadRequest("Asistencia no existe");
                }
                var asistencia = _repAsistencia.FirstById(MaterialEntrega.IdAsistencia);

                MaterialEntregaBO materialEntrega;
                if (MaterialEntrega.Id == 0)// no existe
                {
                    materialEntrega = new MaterialEntregaBO()
                    {
                        IdMaterialVersion = MaterialEntrega.IdMaterialVersion,
                        Entregado = MaterialEntrega.Entregado,
                        Comentario = string.IsNullOrEmpty(MaterialEntrega.Comentario) ? " " : MaterialEntrega.Comentario,
                        UsuarioCreacion = MaterialEntrega.NombreUsuario,
                        UsuarioModificacion = MaterialEntrega.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                }
                else
                {
                    if (!_repMaterialEntrega.Exist(MaterialEntrega.Id))
                    {
                        return BadRequest("Material no existe");
                    }
                    materialEntrega = _repMaterialEntrega.FirstById(MaterialEntrega.Id);
                    materialEntrega.IdMaterialVersion = MaterialEntrega.IdMaterialVersion;
                    materialEntrega.Entregado = MaterialEntrega.Entregado;
                    materialEntrega.Comentario = string.IsNullOrEmpty(MaterialEntrega.Comentario) ? " " : MaterialEntrega.Comentario;
                    materialEntrega.UsuarioModificacion = MaterialEntrega.NombreUsuario;
                    materialEntrega.FechaModificacion = DateTime.Now;
                }
                asistencia.ListaMaterialEntrega.Add(materialEntrega);
                _repAsistencia.Update(asistencia);

                return Ok(asistencia);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
