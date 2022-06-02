using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.SCode.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Operaciones
{
    [Route("api/Operaciones/Evaluacion")]
    public class EvaluacionController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly EvaluacionRepositorio _repEvaluacion;
        
        public EvaluacionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repEvaluacion = new EvaluacionRepositorio(integraDBContext);
        }

        [Route("[action]/{idPespecifico}/{grupo}")]
        [HttpGet]
        public ActionResult ListadoEvaluacionPorPrograma(int idPespecifico, int grupo)
        {
            try
            {
                var b = _repEvaluacion.Exist(w => w.IdPespecifico == idPespecifico && w.Grupo == grupo);
                if (!_repEvaluacion.Exist(w => w.IdPespecifico == idPespecifico && w.Grupo == grupo))
                {
                    PespecificoRepositorio _repoPespecifico = new PespecificoRepositorio();
                    var pespecifico = _repoPespecifico.FirstBy(w => w.Id == idPespecifico,
                        s => new {s.IdProgramaGeneral, s.TipoId});

                    if (pespecifico != null && pespecifico.IdProgramaGeneral != null && pespecifico.TipoId != null)
                    {
                        PGeneralCriterioEvaluacionRepositorio _repoCriterioEvaluacionPgeneral =
                            new PGeneralCriterioEvaluacionRepositorio();
                        var listadoCriteriosPadre =
                            _repoCriterioEvaluacionPgeneral.ListarCriteriosPorPespecificoModalidad(
                                pespecifico.IdProgramaGeneral.Value, pespecifico.TipoId.Value);

                        bool insertarDesdeMaestro = _repEvaluacion.InsertarDesdePadre(listadoCriteriosPadre, idPespecifico, grupo);
                    }
                }
                List<EvaluacionListadoDTO> listado = _repEvaluacion.ListadoPorPrograma(idPespecifico, grupo);
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Registrar([FromBody]EvaluacionRegistrarDTO evaluacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CriterioEvaluacionRepositorio _repoCriterio = new CriterioEvaluacionRepositorio();
                var criterio = _repoCriterio.FirstBy(x => x.Id == evaluacion.IdCriterioEvaluacion, s => new {s.Nombre});

                EvaluacionBO bo = new EvaluacionBO();
                bo.IdPespecifico = evaluacion.IdPEspecifico;
                bo.Grupo = evaluacion.Grupo;
                bo.Nombre = criterio == null ? evaluacion.Nombre : criterio.Nombre;
                bo.IdCriterioEvaluacion = evaluacion.IdCriterioEvaluacion;
                bo.Porcentaje= evaluacion.Porcentaje;
                bo.Aprobado = false;
                
                bo.Estado = true;
                bo.FechaCreacion = DateTime.Now;
                bo.FechaModificacion = DateTime.Now;
                bo.UsuarioCreacion = evaluacion.Usuario;
                bo.UsuarioModificacion = evaluacion.Usuario;

                var resultado = _repEvaluacion.Insert(bo);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody]EvaluacionRegistrarDTO evaluacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CriterioEvaluacionRepositorio _repoCriterio = new CriterioEvaluacionRepositorio();
                var criterio = _repoCriterio.FirstBy(x => x.Id == evaluacion.IdCriterioEvaluacion, s => new { s.Nombre });
                
                EvaluacionBO bo = _repEvaluacion.FirstById(evaluacion.Id.Value);

                bo.Nombre = criterio == null ? evaluacion.Nombre : criterio.Nombre;
                bo.IdCriterioEvaluacion = evaluacion.IdCriterioEvaluacion;
                bo.Porcentaje = evaluacion.Porcentaje;

                bo.Estado = true;
                bo.FechaModificacion = DateTime.Now;
                bo.UsuarioModificacion = evaluacion.Usuario;

                var resultado = _repEvaluacion.Update(bo);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody]EvaluacionRegistrarDTO evaluacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = _repEvaluacion.Delete(evaluacion.Id.Value, evaluacion.Usuario);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idPespecifico}/{grupo}/{usuario}")]
        [HttpGet]
        public ActionResult Aprobar(int idPespecifico, int grupo, string usuario)
        {
            try
            {
                var listado = _repEvaluacion.GetBy(w =>
                    w.IdPespecifico == idPespecifico && w.Grupo == grupo && w.Estado == true);
                if (listado.Sum(s => s.Porcentaje) != 100)
                    return BadRequest("El porcentaje total debe de sumar el 100%");

                foreach (var item in listado)
                {
                    item.Aprobado = true;
                    item.FechaAprobacion = DateTime.Now;
                    item.UsuarioAprobacion = usuario;

                    item.FechaModificacion = DateTime.Now;
                    item.UsuarioAprobacion = usuario;
                }
                var resultado = _repEvaluacion.Update(listado);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}