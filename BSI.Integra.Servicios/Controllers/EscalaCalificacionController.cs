using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/EscalaCalificacion")]
    [ApiController]
    public class EscalaCalificacionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly EscalaCalificacionRepositorio _repoEscala;
        private readonly EscalaCalificacionDetalleRepositorio _repoEscalaDetalle;

        public EscalaCalificacionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repoEscala = new EscalaCalificacionRepositorio(_integraDBContext);
            _repoEscalaDetalle = new EscalaCalificacionDetalleRepositorio(_integraDBContext);
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
                return Ok(_repoEscala.ObtenerTodo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] EscalaCalificacion_RegistrarDTO escala)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var EscalaCalificacionNuevo = new EscalaCalificacionBO()
                {
                    Nombre = escala.Nombre,
                    
                    Estado = true,
                    UsuarioCreacion = escala.NombreUsuario,
                    UsuarioModificacion = escala.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                //añade la lista de detalles
                if (escala.ListadoDetalle != null && escala.ListadoDetalle.Count > 0)
                {
                    EscalaCalificacionNuevo.ListadoDetalle = new List<EscalaCalificacionDetalleBO>();
                    EscalaCalificacionNuevo.ListadoDetalle.AddRange(escala.ListadoDetalle.Select(s =>
                        new EscalaCalificacionDetalleBO()
                        {
                            Nombre = s.Nombre, Valor = s.Valor, 
                            
                            Estado = true, UsuarioCreacion = escala.NombreUsuario,
                            UsuarioModificacion = escala.NombreUsuario, FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }));
                }
                if (!EscalaCalificacionNuevo.HasErrors)
                {
                    _repoEscala.Insert(EscalaCalificacionNuevo);
                }
                else
                {
                    return BadRequest(EscalaCalificacionNuevo.GetErrors());
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
        public ActionResult Actualizar([FromBody] EscalaCalificacion_RegistrarDTO escala)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repoEscala.Exist(escala.Id))
                {
                    return BadRequest("Escala no existente!");
                }
                var escalaExistente = _repoEscala.FirstById(escala.Id);
                escalaExistente.Nombre = escala.Nombre;
                
                escalaExistente.UsuarioModificacion = escala.NombreUsuario;
                escalaExistente.FechaModificacion = DateTime.Now;

                var listadoDetalleExistente =
                    _repoEscalaDetalle.GetBy(w => w.IdEscalaCalificacion == escala.Id);
                List<int> listadoIdDetalleExistente = new List<int>();
                    
                
                var listadoEliminar = new List<int>();
                //añade la lista de detalles
                if (escala.ListadoDetalle != null && escala.ListadoDetalle.Count > 0)
                {
                    escalaExistente.ListadoDetalle = new List<EscalaCalificacionDetalleBO>();
                    listadoIdDetalleExistente = listadoDetalleExistente.Select(s => s.Id).ToList();

                    var listadoNuevo = new List<EscalaCalificacionDetalleBO>();
                    var listadoActualizar = new List<EscalaCalificacionDetalleBO>();

                    listadoNuevo.AddRange(escala.ListadoDetalle.Where(w => w.Id == null || w.Id == 0).Select(s =>
                        new EscalaCalificacionDetalleBO()
                        {
                            Nombre = s.Nombre,
                            Valor = s.Valor,

                            Estado = true,
                            UsuarioCreacion = escala.NombreUsuario,
                            UsuarioModificacion = escala.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }));
                    foreach (var detalleExistente in listadoDetalleExistente.Where(w => escala.ListadoDetalle.Select(s => s.Id).Contains(w.Id)))
                    {
                        var itemActualizado = escala.ListadoDetalle.FirstOrDefault(f => f.Id == detalleExistente.Id);

                        detalleExistente.Nombre = itemActualizado.Nombre;
                        detalleExistente.Valor = itemActualizado.Valor;

                        detalleExistente.UsuarioModificacion = escala.NombreUsuario;
                        detalleExistente.FechaModificacion = DateTime.Now;

                        listadoActualizar.Add(detalleExistente);
                    }
                    
                    escalaExistente.ListadoDetalle.AddRange(listadoNuevo);
                    escalaExistente.ListadoDetalle.AddRange(listadoActualizar);

                }
                if (listadoIdDetalleExistente != null && listadoIdDetalleExistente.Count > 0)
                {
                    listadoEliminar.AddRange(listadoIdDetalleExistente.Where(w =>
                        !escala.ListadoDetalle.Select(s => s.Id).Contains(w)));
                }
                
                if (!escalaExistente.HasErrors)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        _repoEscala.Update(escalaExistente);
                        _repoEscalaDetalle.Delete(listadoEliminar, escala.NombreUsuario);
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
                if (!_repoEscala.Exist(flujo.Id))
                {
                    return BadRequest("Escala no existente");
                }
                _repoEscala.Delete(flujo.Id, flujo.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idEscalaCalificacion}")]
        [HttpGet]
        public ActionResult ObtenerListadoDetalle(int idEscalaCalificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repoEscalaDetalle.GetBy(w => w.IdEscalaCalificacion == idEscalaCalificacion));
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
                return Ok(_repoEscala.ObtenerCombo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
