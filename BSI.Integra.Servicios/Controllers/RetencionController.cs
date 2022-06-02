using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RetencionController
    /// Autor: Miguel Mora
    /// Fecha: 08/07/2021
    /// <summary>
    /// Mantenimiento de la tabla fin.T_Retenciones
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class RetencionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public RetencionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        /// TipoFuncion: GET
        /// Autor: Miguel Mora
        /// Fecha: 08/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Retenciones
        /// </summary>
        /// <returns>List<RetencionDTO><returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerRetenciones()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RetencionRepositorio _repRetencionRepositorioRepositorio = new RetencionRepositorio(_integraDBContext);
                return Ok(_repRetencionRepositorioRepositorio.ObtenerRetenciones());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Miguel Mora
        /// Fecha: 08/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene valores de retenciones para un combo
        /// </summary>
        /// <returns>List<FiltroDTO><returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult VizualizarRetenciones()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RetencionRepositorio _repRetencionRepositorioRepositorio = new RetencionRepositorio(_integraDBContext);
                return Ok(_repRetencionRepositorioRepositorio.ObtenerListaRetencion());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Miguel Mora
        /// Fecha: 08/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda Retenciones
        /// </summary>
        /// <param name=”ObjetoDTO”>DTO de la tabla retenciones</param>
        /// <returns>RetencionDTO<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarRetenecion([FromBody] RetencionDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PaisRepositorio _repPaisRepositorio = new PaisRepositorio(_integraDBContext);
                PaisNombreDTO pais = _repPaisRepositorio.ObtenerNombrePaisPorId(ObjetoDTO.IdPais);
                ObjetoDTO.Pais = pais.NombrePais;
                RetencionRepositorio _repRetencionRepositorioRepositorio = new RetencionRepositorio(_integraDBContext);
                RetencionBO nuevoRepositorio = new RetencionBO
                {
                    Nombre = ObjetoDTO.Nombre,
                    Descripcion = ObjetoDTO.Descripcion,
                    IdPais = ObjetoDTO.IdPais,
                    Estado = true,
                    Valor = ObjetoDTO.Valor,
                    UsuarioCreacion= ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                _repRetencionRepositorioRepositorio.Insert(nuevoRepositorio);
                ObjetoDTO.Id = nuevoRepositorio.Id;
                return Ok(ObjetoDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Miguel Mora
        /// Fecha: 08/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Retenciones
        /// </summary>
        /// <param name=”ObjetoDTO”>DTO de la tabla retenciones</param>
        /// <returns>RetencionDTO<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarRetencion([FromBody] RetencionDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PaisRepositorio _repPaisRepositorio = new PaisRepositorio(_integraDBContext);
                PaisNombreDTO pais = _repPaisRepositorio.ObtenerNombrePaisPorId(ObjetoDTO.IdPais);
                ObjetoDTO.Pais = pais.NombrePais;

                RetencionRepositorio _repRetencionRepositorio = new RetencionRepositorio(_integraDBContext);
                RetencionBO retencion = _repRetencionRepositorio.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                retencion.Nombre = ObjetoDTO.Nombre;
                retencion.Descripcion = ObjetoDTO.Descripcion;
                retencion.Valor = ObjetoDTO.Valor;
                retencion.IdPais = ObjetoDTO.IdPais;
                retencion.Estado = true;
                retencion.UsuarioModificacion = ObjetoDTO.Usuario;
                retencion.FechaModificacion = DateTime.Now;

                _repRetencionRepositorio.Update(retencion);

                return Ok(ObjetoDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Miguel Mora
        /// Fecha: 08/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina Retenciones
        /// </summary>
        /// <param name=”Eliminar”>DTO general que tiene el id a eliminar de una tabla</param>
        /// <returns>Bool<returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarRetencion([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RetencionRepositorio _repRetencionRepositorio = new RetencionRepositorio(_integraDBContext);
                RetencionBO retencion = _repRetencionRepositorio.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repRetencionRepositorio.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Miguel Mora
        /// Fecha: 08/07/2021
        /// Versión: 1.0
        /// <summary>
        /// OBtiene los paises para el combo 
        /// </summary>
        /// <returns>Objeto {string, List<PaisFiltroComboDTO>}<returns>

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPais()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PaisRepositorio _repPaisRepositorio = new PaisRepositorio(_integraDBContext);
                var listaPais = _repPaisRepositorio.ObtenerPaisesCombo();
                return Ok(new { Result = "OK", Records = listaPais });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}
