using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FeedbackConfigurar")]
    public class FeedbackConfigurarController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public FeedbackConfigurarController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFeedbackConfigurar()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeedbackConfigurarRepositorio _repFeedbackConfigurar = new FeedbackConfigurarRepositorio(_integraDBContext);
                return Ok(_repFeedbackConfigurar.ObtenerTodoFeedbackConfigurarFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarFeedbackConfigurar([FromBody] FeedbackConfigurarRegistroDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeedbackConfigurarRepositorio _repFeedbackConfigurar = new FeedbackConfigurarRepositorio(_integraDBContext);
                FeedbackConfigurarDetalleRepositorio _repFeedbackConfigurarDetalle = new FeedbackConfigurarDetalleRepositorio(_integraDBContext);

                FeedbackConfigurarBO NuevaFeedbackConfigurar = new FeedbackConfigurarBO
                {
                    IdFeedbackTipo = ObjetoDTO.FeedbackConfigurar.IdFeedbackTipo,
                    Nombre = ObjetoDTO.FeedbackConfigurar.Nombre,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                _repFeedbackConfigurar.Insert(NuevaFeedbackConfigurar);

                List<FeedbackConfigurarDetalleBO> _listaDetalle = new List<FeedbackConfigurarDetalleBO>();
                foreach (var detalle in ObjetoDTO.FeedbackConfigurarDetalle)
                {
                    FeedbackConfigurarDetalleBO _newConfigurar = new FeedbackConfigurarDetalleBO
                    {
                        IdFeedbackConfigurar = NuevaFeedbackConfigurar.Id,
                        IdSexo = detalle.IdSexo,
                        Puntaje = detalle.Puntaje,
                        NombreVideo = detalle.NombreVideo,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.Usuario,
                        UsuarioModificacion = ObjetoDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _listaDetalle.Add(_newConfigurar);
                }

                if (_listaDetalle.Count > 0)
                {
                    _repFeedbackConfigurarDetalle.Insert(_listaDetalle);
                }

                return Ok(NuevaFeedbackConfigurar);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarFeedbackConfigurar([FromBody] FeedbackConfigurarRegistroDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeedbackConfigurarRepositorio _repFeedbackConfigurar = new FeedbackConfigurarRepositorio(_integraDBContext);
                FeedbackConfigurarDetalleRepositorio _repFeedbackConfigurarDetalle = new FeedbackConfigurarDetalleRepositorio(_integraDBContext);

                FeedbackConfigurarBO FeedbackConfigurar = _repFeedbackConfigurar.GetBy(x => x.Id == ObjetoDTO.FeedbackConfigurar.Id).FirstOrDefault();
                FeedbackConfigurar.IdFeedbackTipo = ObjetoDTO.FeedbackConfigurar.IdFeedbackTipo;
                FeedbackConfigurar.Nombre = ObjetoDTO.FeedbackConfigurar.Nombre;
                FeedbackConfigurar.Estado = true;
                FeedbackConfigurar.UsuarioModificacion = ObjetoDTO.Usuario;
                FeedbackConfigurar.FechaModificacion = DateTime.Now;
                _repFeedbackConfigurar.Update(FeedbackConfigurar);

                foreach (var itemEliminar in ObjetoDTO.FeedbackConfigurarDetalleEliminar)
                {
                    FeedbackConfigurarDetalleBO FeedbackConfigurarDetalle = _repFeedbackConfigurarDetalle.GetBy(x => x.Id == itemEliminar.Id).FirstOrDefault();
                    _repFeedbackConfigurarDetalle.Delete(FeedbackConfigurarDetalle.Id, ObjetoDTO.Usuario);
                }

                foreach (var itemActualizar in ObjetoDTO.FeedbackConfigurarDetalle)
                {
                    if (itemActualizar.Id == 0)
                    {
                        FeedbackConfigurarDetalleBO _newConfigurar = new FeedbackConfigurarDetalleBO
                        {
                            IdFeedbackConfigurar = FeedbackConfigurar.Id,
                            IdSexo = itemActualizar.IdSexo,
                            Puntaje = itemActualizar.Puntaje,
                            NombreVideo = itemActualizar.NombreVideo,
                            Estado = true,
                            UsuarioCreacion = ObjetoDTO.Usuario,
                            UsuarioModificacion = ObjetoDTO.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _repFeedbackConfigurarDetalle.Insert(_newConfigurar);
                    }
                    else
                    {
                        FeedbackConfigurarDetalleBO FeedbackConfigurarDetalle = _repFeedbackConfigurarDetalle.GetBy(x => x.Id == itemActualizar.Id).FirstOrDefault();
                        FeedbackConfigurarDetalle.IdSexo = itemActualizar.IdSexo;
                        FeedbackConfigurarDetalle.Puntaje = itemActualizar.Puntaje;
                        FeedbackConfigurarDetalle.NombreVideo = itemActualizar.NombreVideo;
                        FeedbackConfigurarDetalle.UsuarioModificacion = ObjetoDTO.Usuario;
                        FeedbackConfigurarDetalle.FechaModificacion = DateTime.Now;
                        _repFeedbackConfigurarDetalle.Update(FeedbackConfigurarDetalle);
                    }

                    
                }

                return Ok(FeedbackConfigurar);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarFeedbackConfigurar([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeedbackConfigurarRepositorio _repFeedbackConfigurar = new FeedbackConfigurarRepositorio(_integraDBContext);
                FeedbackConfigurarBO FeedbackConfigurar = _repFeedbackConfigurar.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repFeedbackConfigurar.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Listas

        [Route("[Action]/{IdFeedbackConfigurar}")]
        [HttpGet]
        public ActionResult ObteneListaConfiguracionDetalle(int IdFeedbackConfigurar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeedbackConfigurarDetalleRepositorio _repFeedbackConfigurarDetalle = new FeedbackConfigurarDetalleRepositorio(_integraDBContext);
                var respuesta = _repFeedbackConfigurarDetalle.GetBy(x => x.IdFeedbackConfigurar == IdFeedbackConfigurar).ToList(); ;

                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObteneListaSexo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SexoRepositorio _repSexoRepositorio = new SexoRepositorio(_integraDBContext);
                var respuesta = _repSexoRepositorio.GetFiltroIdNombre();

                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
