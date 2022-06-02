using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AreaFormacionController
    /// Autor: Jorge Rivera - Ansoli Espinoza - Wilber Choque - Luis Huallpa - Richard Zenteno - Jose Villena
    /// Fecha: 03/01/2019
    /// <summary>
    /// Area Formacion
    /// </summary>
    [Route("api/AreaFormacion")]
    public class AreaFormacionController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public AreaFormacionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Area Formacion Filtro
        /// </summary>
        /// <returns>List<AreaFormacionFiltroDTO><returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerAreaFormacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaFormacionRepositorio _repAreaFormacion = new AreaFormacionRepositorio(_integraDBContext);
                return Ok(_repAreaFormacion.ObtenerAreaFormacionFiltro());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta Registro de Área de Formacion
        /// </summary>
        /// <param name="ObjetoDTO">Información Compuesta de Área de Formación</param>
        /// <returns>Objeto de Tipo: AreaFormacionBO<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarAreaFormacion([FromBody] AreaFormacionDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaFormacionRepositorio _repAreaFormacion = new AreaFormacionRepositorio(_integraDBContext);
                AreaFormacionBO nuevaAreaFormacion = new AreaFormacionBO
                {
                    Nombre = ObjetoDTO.Nombre,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                _repAreaFormacion.Insert(nuevaAreaFormacion);
                return Ok(nuevaAreaFormacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Registro de Área de Formacion
        /// </summary>
        /// <param name="ObjetoDTO">Información Compuesta de Área de Formación</param>
        /// <returns>AreaFormacionBO<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarAreaFormacion([FromBody] AreaFormacionDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaFormacionRepositorio _repAreaFormacion = new AreaFormacionRepositorio(_integraDBContext);
                AreaFormacionBO areaFormacion = _repAreaFormacion.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();
                areaFormacion.Nombre = ObjetoDTO.Nombre;
                areaFormacion.Estado = true;
                areaFormacion.UsuarioModificacion = ObjetoDTO.Usuario;
                areaFormacion.FechaModificacion = DateTime.Now;
                _repAreaFormacion.Update(areaFormacion);
                return Ok(areaFormacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina Registro de Área de Formacion
        /// </summary>
        /// <param name="Eliminar">Información Id de Registro y Nombre de Usuario de Interfaz</param>
        /// <returns>StatusCode 200, Bool de Confirmación de Eliminación de registro<returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarAreaFormacion([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaFormacionRepositorio _repAreaFormacion = new AreaFormacionRepositorio(_integraDBContext);
                AreaFormacionBO areaFormacion = _repAreaFormacion.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repAreaFormacion.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
