using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.SCode.BO;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MaestroDiccionarioPalabraOfensivaController
    /// Autor: Edgar S.
    /// Fecha: 03/03/2021
    /// <summary>
    /// Gestión de Palabras ofensivas para chat de Asesores
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MaestroDiccionarioPalabraOfensivaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly DiccionarioPalabraOfensivaRepositorio _repDiccionarioPalabraOfensiva;

        public MaestroDiccionarioPalabraOfensivaController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repDiccionarioPalabraOfensiva = new DiccionarioPalabraOfensivaRepositorio(_integraDBContext);
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 03/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros de Palabras Ofensivas
        /// </summary>
        /// <returns> Lista de ObjetoBO: List<DiccionarioPalabraOfensivaBO>  </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerPalabraOfensivaRegistradas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaDiccionarioPalabraOfensiva = _repDiccionarioPalabraOfensiva.GetAll();
                return Ok(listaDiccionarioPalabraOfensiva);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 03/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros de Id y Palabra Ofensivas
        /// </summary>
        /// <returns> Lista de Objeto: List<Id,String>  </returns>
        [HttpGet]
        [Route("[action]")]
        public ActionResult ObtenerIdPalabraOfensiva()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaIdPalabra = _repDiccionarioPalabraOfensiva.GetBy(x => x.Estado == true).Select(x => new { x.Id, x.PalabraFiltrada }).ToList();
                return Ok(listaIdPalabra);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 03/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Insertar nueva palabra
        /// </summary>
        /// <returns> Confirmación de inserción : Bool </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult Insertar([FromBody] DiccionarioPalabraOfensivaDTO PalabraOfensiva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DiccionarioPalabraOfensivaBO palabraOfensiva = new DiccionarioPalabraOfensivaBO()
                {
                    PalabraFiltrada = PalabraOfensiva.PalabraOfensiva,
                    Estado = true,
                    UsuarioCreacion = PalabraOfensiva.Usuario,
                    UsuarioModificacion = PalabraOfensiva.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                var res = _repDiccionarioPalabraOfensiva.Insert(palabraOfensiva);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 03/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza nueva palabra
        /// </summary>
        /// <returns> Confirmación de actualización : Bool </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] DiccionarioPalabraOfensivaDTO PalabraOfensiva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var palabraOfensiva = _repDiccionarioPalabraOfensiva.FirstById(PalabraOfensiva.Id);
                palabraOfensiva.PalabraFiltrada = PalabraOfensiva.PalabraOfensiva;
                palabraOfensiva.UsuarioModificacion = PalabraOfensiva.Usuario;
                palabraOfensiva.FechaModificacion = DateTime.Now;
                var res = _repDiccionarioPalabraOfensiva.Update(palabraOfensiva);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 03/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina palabra de registro
        /// </summary>
        /// <returns> Confirmación de eliminación : Bool </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO PalabraOfensiva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repDiccionarioPalabraOfensiva.Exist(PalabraOfensiva.Id))
                {
                    var res = _repDiccionarioPalabraOfensiva.Delete(PalabraOfensiva.Id, PalabraOfensiva.NombreUsuario);
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
