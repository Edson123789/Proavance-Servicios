using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/DuracionAvanceAcademicoMoodle")]
    [ApiController]
    public class DuracionAvanceAcademicoMoodleOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltrado(string TextoBuscar)
        {
            try
            {
                DuracionAvanceAcademicoMoodleRepositorio _repDuracionAvanceAcademicoMoodle = new DuracionAvanceAcademicoMoodleRepositorio();
                if (!string.IsNullOrEmpty(TextoBuscar))
                {
                    return Ok(_repDuracionAvanceAcademicoMoodle.GetBy(x => x.IdMoodle.ToString().Contains(TextoBuscar), x => new { x.Id, x.IdMoodle, x.IdTipoCapacitacionMoodle, x.Duracion, x.Meses }));
                }
                return Ok(_repDuracionAvanceAcademicoMoodle.GetBy(x => x.Estado == true, x => new { x.Id, x.IdMoodle, x.IdTipoCapacitacionMoodle, x.Duracion, x.Meses }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] DuracionAvanceAcademicoMoodleDTO DuracionAvanceAcademicoMoodle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DuracionAvanceAcademicoMoodleRepositorio _repDuracionAvanceAcademicoMoodle = new DuracionAvanceAcademicoMoodleRepositorio();

                if (_repDuracionAvanceAcademicoMoodle.GetBy(x => x.IdMoodle == DuracionAvanceAcademicoMoodle.IdMoodle && x.IdTipoCapacitacionMoodle == DuracionAvanceAcademicoMoodle.IdTipoCapacitacionMoodle).Count() == 0)
                {
                    DuracionAvanceAcademicoMoodleBO duracionAvanceAcademicoMoodleBO = new DuracionAvanceAcademicoMoodleBO()
                    {
                        IdMoodle = DuracionAvanceAcademicoMoodle.IdMoodle,
                        IdTipoCapacitacionMoodle = DuracionAvanceAcademicoMoodle.IdTipoCapacitacionMoodle,
                        Duracion = DuracionAvanceAcademicoMoodle.Duracion,
                        Meses = DuracionAvanceAcademicoMoodle.Meses,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = DuracionAvanceAcademicoMoodle.NombreUsuario,
                        UsuarioModificacion = DuracionAvanceAcademicoMoodle.NombreUsuario
                    };
                    if (!duracionAvanceAcademicoMoodleBO.HasErrors)
                    {
                        _repDuracionAvanceAcademicoMoodle.Insert(duracionAvanceAcademicoMoodleBO);
                    }
                    else
                    {
                        return BadRequest(duracionAvanceAcademicoMoodleBO.ActualesErrores);
                    }
                    return Ok(new { duracionAvanceAcademicoMoodleBO.Id, duracionAvanceAcademicoMoodleBO.IdMoodle, duracionAvanceAcademicoMoodleBO.IdTipoCapacitacionMoodle, duracionAvanceAcademicoMoodleBO.Duracion, duracionAvanceAcademicoMoodleBO.Meses });
                }
                else {
                    return BadRequest("La Duración seleccionada ya existe.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] DuracionAvanceAcademicoMoodleDTO DuracionAvanceAcademicoMoodle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DuracionAvanceAcademicoMoodleRepositorio _repDuracionAvanceAcademicoMoodle = new DuracionAvanceAcademicoMoodleRepositorio();

                if (_repDuracionAvanceAcademicoMoodle.Exist(DuracionAvanceAcademicoMoodle.Id))
                {
                    var duracionAvanceAcademicoMoodleBO = _repDuracionAvanceAcademicoMoodle.FirstById(DuracionAvanceAcademicoMoodle.Id);
                    duracionAvanceAcademicoMoodleBO.IdMoodle = DuracionAvanceAcademicoMoodle.IdMoodle;
                    duracionAvanceAcademicoMoodleBO.IdTipoCapacitacionMoodle = DuracionAvanceAcademicoMoodle.IdTipoCapacitacionMoodle;
                    duracionAvanceAcademicoMoodleBO.Duracion = DuracionAvanceAcademicoMoodle.Duracion;
                    duracionAvanceAcademicoMoodleBO.Meses = DuracionAvanceAcademicoMoodle.Meses;
                    duracionAvanceAcademicoMoodleBO.Estado = true;
                    duracionAvanceAcademicoMoodleBO.FechaModificacion = DateTime.Now;
                    duracionAvanceAcademicoMoodleBO.UsuarioModificacion = DuracionAvanceAcademicoMoodle.NombreUsuario;
                    if (!duracionAvanceAcademicoMoodleBO.HasErrors)
                    {
                        _repDuracionAvanceAcademicoMoodle.Update(duracionAvanceAcademicoMoodleBO);
                    }
                    else
                    {
                        return BadRequest(duracionAvanceAcademicoMoodleBO.ActualesErrores);
                    }
                    return Ok(new { duracionAvanceAcademicoMoodleBO.Id, duracionAvanceAcademicoMoodleBO.IdMoodle, duracionAvanceAcademicoMoodleBO.IdTipoCapacitacionMoodle, duracionAvanceAcademicoMoodleBO.Duracion, duracionAvanceAcademicoMoodleBO.Meses });
                }
                else
                {
                    return BadRequest("Registro no existente");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
