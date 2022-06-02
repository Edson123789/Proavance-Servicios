using System;
using System.Linq;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FaseOportunidad")]
    public class FaseOportunidadController : Controller 
    {
        private readonly integraDBContext _integraDBContext;
        public FaseOportunidadController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerParaFiltro()
        {
            try
            {
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                return Ok(_repFaseOportunidad.ObtenerFasesOportunidadFiltroCodigo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodasFasesOportunidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                return Ok(_repFaseOportunidad.ObtenerFaseOportunidadTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                FaseOportunidadRepositorio faseOportunidadRepositorio = new FaseOportunidadRepositorio(_integraDBContext);
                return Ok(faseOportunidadRepositorio.ObtenerTodoGrid());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoActividadesCabecera()
        {
            try
            {
                ActividadCabeceraRepositorio actividadCabeceraRepositorio = new ActividadCabeceraRepositorio(_integraDBContext);
                return Ok(actividadCabeceraRepositorio.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] FaseOportunidadDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FaseOportunidadRepositorio faseOportunidadRepositorio = new FaseOportunidadRepositorio(_integraDBContext);
                FaseOportunidadBO faseOportunidadBO = new FaseOportunidadBO
                {
                    Codigo = Json.Codigo,
                    Nombre = Json.Nombre,
                    NroMinutos = Json.NroMinutos,
                    IdActividad = Json.IdActividad,
                    MaxNumDias = Json.MaxNumDias,
                    MinNumDias = Json.MinNumDias,
                    TasaConversionEsperada = Json.TasaConversionEsperada,
                    Meta = Json.Meta,
                    Final = Json.Final,
                    ReporteMeta = Json.ReporteMeta,
                    EnSeguimiento = Json.EnSeguimiento,
                    EsCierre = Json.EsCierre,
                    Estado = true,
                    UsuarioCreacion = Json.Usuario,
                    UsuarioModificacion = Json.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                return Ok(faseOportunidadRepositorio.Insert(faseOportunidadBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerActividadesCabecera()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ActividadCabeceraRepositorio _repoActividadCabecera = new ActividadCabeceraRepositorio(_integraDBContext);
                var actividades = _repoActividadCabecera.ObtenerTodoFiltro();
                return Json(new { Result = "OK", Records = actividades });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult VisualizarFaseOportunidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                var FaseOportunidad = _repFaseOportunidad.ObtenerTodasFaseOportunidad();
                return Json(new { Result = "OK", Records = FaseOportunidad });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarFaseOportunidad([FromBody] FaseOportunidadDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                FaseOportunidadBO NuevaFaseOportunidad = new FaseOportunidadBO
                {
                    Codigo = ObjetoDTO.Codigo,
                    Nombre = ObjetoDTO.Nombre,
                    NroMinutos = ObjetoDTO.NroMinutos,
                    MinNumDias = ObjetoDTO.MinNumDias,
                    MaxNumDias = ObjetoDTO.MaxNumDias,
                    Final = ObjetoDTO.Final,
                    TasaConversionEsperada = ObjetoDTO.TasaConversionEsperada,
                    Meta = ObjetoDTO.Meta,
                    IdActividad = ObjetoDTO.IdActividad,
                    ReporteMeta = ObjetoDTO.ReporteMeta,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };
                _repFaseOportunidad.Insert(NuevaFaseOportunidad);

                return Ok(NuevaFaseOportunidad);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarFaseOportunidad([FromBody] FaseOportunidadDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                FaseOportunidadBO FaseOportunidad = _repFaseOportunidad.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                FaseOportunidad.Codigo = ObjetoDTO.Codigo;
                FaseOportunidad.Nombre = ObjetoDTO.Nombre;
                FaseOportunidad.NroMinutos = ObjetoDTO.NroMinutos;
                FaseOportunidad.MinNumDias = ObjetoDTO.MinNumDias;
                FaseOportunidad.MaxNumDias = ObjetoDTO.MaxNumDias;
                FaseOportunidad.Final = ObjetoDTO.Final;
                FaseOportunidad.TasaConversionEsperada = ObjetoDTO.TasaConversionEsperada;
                FaseOportunidad.Meta = ObjetoDTO.Meta;
                FaseOportunidad.IdActividad = ObjetoDTO.IdActividad;
                FaseOportunidad.ReporteMeta = ObjetoDTO.ReporteMeta;
                FaseOportunidad.UsuarioModificacion = ObjetoDTO.Usuario;
                FaseOportunidad.FechaModificacion = DateTime.Now;
                FaseOportunidad.Estado = true;
                _repFaseOportunidad.Update(FaseOportunidad);

                return Ok(FaseOportunidad);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarFaseOportunidad([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                FaseOportunidadBO FaseOportunidad = _repFaseOportunidad.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repFaseOportunidad.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
