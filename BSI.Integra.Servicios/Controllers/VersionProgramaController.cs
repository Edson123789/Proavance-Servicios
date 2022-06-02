using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/VersionPrograma")]
    public class VersionProgramaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public VersionProgramaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            VersionProgramaRepositorio _repVersionPrograma = new VersionProgramaRepositorio(_integraDBContext);

            var rpta = _repVersionPrograma.ObtenerTodo();
            return Ok(new { Data = rpta, Total = rpta.Count });
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody]VersionProgramaDTO Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                VersionProgramaRepositorio _repVersionPrograma = new VersionProgramaRepositorio(_integraDBContext);

                VersionProgramaBO versionProgramaBO = new VersionProgramaBO();
                versionProgramaBO.Nombre = Obj.Nombre;
                versionProgramaBO.Estado = true;
                versionProgramaBO.UsuarioCreacion = Obj.Usuario;
                versionProgramaBO.UsuarioModificacion = Obj.Usuario;
                versionProgramaBO.FechaCreacion = DateTime.Now;
                versionProgramaBO.FechaModificacion = DateTime.Now;

                _repVersionPrograma.Insert(versionProgramaBO);

                return Ok(versionProgramaBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody]VersionProgramaDTO Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                VersionProgramaRepositorio _repVersionPrograma = new VersionProgramaRepositorio(_integraDBContext);

                VersionProgramaBO versionProgramaBO = _repVersionPrograma.FirstById(Obj.Id);
                versionProgramaBO.Nombre = Obj.Nombre;
                versionProgramaBO.UsuarioModificacion = Obj.Usuario;
                versionProgramaBO.FechaModificacion = DateTime.Now;

                _repVersionPrograma.Update(versionProgramaBO);

                return Ok(versionProgramaBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [Route("[action]/{Id}/{Usuario}")]
        [HttpGet]
        public ActionResult Eliminar(int Id , string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                VersionProgramaRepositorio _repVersionPrograma = new VersionProgramaRepositorio(_integraDBContext);

                _repVersionPrograma.Delete(Id,Usuario);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
