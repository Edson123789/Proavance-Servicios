using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MaestroFaseOportunidadController
    /// Autor: Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Maestro de Fases de Oportunidad
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MaestroFaseOportunidadController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly FaseOportunidadRepositorio _repFaseOportunidad;

        public MaestroFaseOportunidadController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 26/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros de Oportunidad Registrados
        /// </summary>
        /// <returns> objetoBO: FaseOportunidadBO </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerFaseOportunidadRegistrados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaFaseOportunidad = _repFaseOportunidad.GetAll();
                return Ok(listaFaseOportunidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 26/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta Registro de Oportunidad
        /// </summary>
        /// <returns> bool: Confirmación de Inserción </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarFaseOportunidad([FromBody] FaseOportunidadDTO FaseOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FaseOportunidadBO faseOportunidad = new FaseOportunidadBO()
                {
                    Nombre = FaseOportunidad.Nombre,
                    Codigo = FaseOportunidad.Codigo,
                    Descripcion = FaseOportunidad.Descripcion,
                    VisibleEnReporte = FaseOportunidad.VisibleEnReporte,
                    EsCierre = FaseOportunidad.EsCierre,
                    Estado = true,
                    UsuarioCreacion = FaseOportunidad.Usuario,
                    UsuarioModificacion = FaseOportunidad.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                var res = _repFaseOportunidad.Insert(faseOportunidad);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 26/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualización Registro de Oportunidad
        /// </summary>
        /// <returns> bool: Confirmación de Actualización </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarFaseOportunidad([FromBody] FaseOportunidadDTO FaseOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var faseOportunidad = _repFaseOportunidad.FirstById(FaseOportunidad.Id);
                faseOportunidad.Nombre = FaseOportunidad.Nombre;
                faseOportunidad.Codigo = FaseOportunidad.Codigo;
                faseOportunidad.Descripcion = FaseOportunidad.Descripcion;
                faseOportunidad.VisibleEnReporte = FaseOportunidad.VisibleEnReporte;
                faseOportunidad.EsCierre = FaseOportunidad.EsCierre;
                faseOportunidad.UsuarioModificacion = FaseOportunidad.Usuario;
                faseOportunidad.FechaModificacion = DateTime.Now;
                var res = _repFaseOportunidad.Update(faseOportunidad);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 26/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina logicamente un registro de Fase Oportunidad
        /// </summary>
        /// <returns> bool: Confirmación de Eliminación </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarFaseOportunidad([FromBody] EliminarDTO FaseOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repFaseOportunidad.Exist(FaseOportunidad.Id))
                {
                    var res = _repFaseOportunidad.Delete(FaseOportunidad.Id, FaseOportunidad.NombreUsuario);
                    return Ok(res);
                }
                else
                {
                    return BadRequest("La categoria a eliminar no existe o ya fue eliminada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
