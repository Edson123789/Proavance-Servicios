using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MaestroNivelPuestoTrabajoController
    /// Autor: Edgar S.
    /// Fecha: 15/06/2021
    /// <summary>
    /// Gestiona todas la propiedades de la tabla T_NivelPuestoTrabajo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MaestroNivelPuestoTrabajoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PuestoTrabajoNivelRepositorio _repPuestoTrabajoNivel;

        public MaestroNivelPuestoTrabajoController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repPuestoTrabajoNivel = new PuestoTrabajoNivelRepositorio(_integraDBContext);
        }

        /// TipoFuncion: POST
        /// Autor: Edgar Serruto
        /// Fecha: 15/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene niveles registrados
        /// </summary>
        /// <returns> List<PuestoTrabajoNivelDTO> </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerNivelPuestoTrabajoRegistrado()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaNivelPuestoTrabajo = _repPuestoTrabajoNivel.ObtenerPuestoTrabajoNivelRegistrado();
                return Ok(listaNivelPuestoTrabajo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto
        /// Fecha: 15/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Agrega nuevo nivel de puesto de trabajo
        /// </summary>
        /// <param name="PuestoTrabajoNivel"> Información de Nivel de Puesto de Trabajo </param>
        /// <returns> Bool confirmación de inserción </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarNivelPuestoTrabajo([FromBody] PuestoTrabajoNivelDTO PuestoTrabajoNivel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if(PuestoTrabajoNivel.Nombre.Trim().Length > 0 && PuestoTrabajoNivel.Nombre != null)
                {
                    PuestoTrabajoNivelBO nuevoNivelPuestoTrabajo = new PuestoTrabajoNivelBO()
                    {
                        Nombre = PuestoTrabajoNivel.Nombre,
                        Descripcion = PuestoTrabajoNivel.Descripcion,
                        Estado = true,
                        UsuarioCreacion = PuestoTrabajoNivel.Usuario,
                        UsuarioModificacion = PuestoTrabajoNivel.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    var resultado = _repPuestoTrabajoNivel.Insert(nuevoNivelPuestoTrabajo);
                    return Ok(new { Respuesta = resultado, Mensaje = "Se guardo el registro correctamente" });
                }
                else
                {
                    return Ok(new { Respuesta = false, Mensaje = "El Nombre no puede ser vacio o nulo" });
                }                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto
        /// Fecha: 15/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza información de Nivel de Puesto de Trabajo
        /// </summary>
        /// <param name="PuestoTrabajoNivel"> Información de Nivel de Puesto de Trabajo </param>
        /// <returns> Bool confirmación de actualización </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarNivelPuestoTrabajo([FromBody] PuestoTrabajoNivelDTO PuestoTrabajoNivel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (PuestoTrabajoNivel.Nombre.Trim().Length > 0 && PuestoTrabajoNivel.Nombre != null)
                {
                    var actualizarNivelPuestoTrabajo = _repPuestoTrabajoNivel.FirstById(PuestoTrabajoNivel.Id);
                    actualizarNivelPuestoTrabajo.Nombre = PuestoTrabajoNivel.Nombre;
                    actualizarNivelPuestoTrabajo.Descripcion = PuestoTrabajoNivel.Descripcion;
                    actualizarNivelPuestoTrabajo.UsuarioModificacion = PuestoTrabajoNivel.Usuario;
                    actualizarNivelPuestoTrabajo.FechaModificacion = DateTime.Now;
                    var resultado = _repPuestoTrabajoNivel.Update(actualizarNivelPuestoTrabajo);
                    return Ok(new { Respuesta = resultado, Mensaje = "Se actualizó el registro correctamente" });
                }
                else
                {
                    return Ok(new { Respuesta = false, Mensaje = "El Nombre no puede ser vacio o nulo" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto
        /// Fecha: 15/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza información de Nivel de Puesto de Trabajo
        /// </summary>
        /// <param name="EliminarNivelPuestoTrabajo"> Objeto con nombre de usuario de Módulo e Id de registro a eliminar </param>
        /// <returns> Bool confirmación de eliminación </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarNivelPuestoTrabajo([FromBody] EliminarDTO EliminarNivelPuestoTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repPuestoTrabajoNivel.Exist(EliminarNivelPuestoTrabajo.Id))
                {
                    var resultado = _repPuestoTrabajoNivel.Delete(EliminarNivelPuestoTrabajo.Id, EliminarNivelPuestoTrabajo.NombreUsuario);
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest("El nivel de Puesto de Trabajo no existe o ya fue eliminada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
