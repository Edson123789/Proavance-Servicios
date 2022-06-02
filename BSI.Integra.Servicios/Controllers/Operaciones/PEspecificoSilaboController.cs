using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs.Operaciones;
using BSI.Integra.Aplicacion.DTOs.Reportes;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Operaciones
{
    [Route("api/Operaciones/PEspecificoSilabo")]
    [ApiController]
    public class PEspecificoSilaboController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PEspecificoSilaboRepositorio _repSilabo;

        public PEspecificoSilaboController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repSilabo = new PEspecificoSilaboRepositorio(integraDBContext);
        }

        [Route("[action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult PorPrograma(int idPespecifico)
        {
            try
            {
                PEspecificoSilaboBO bo = new PEspecificoSilaboBO();
                var consultar = _repSilabo.GetBy(w => w.IdPespecifico == idPespecifico).FirstOrDefault();

                if (consultar != null)
                    bo = consultar;
                else
                {
                    PespecificoRepositorio _repoPEspecifico = new PespecificoRepositorio();
                    var pespecifico = _repoPEspecifico.FirstBy(w => w.Id == idPespecifico, s => new { IdProgramaGeneral = s.IdProgramaGeneral });

                    PgeneralDocumentoPwRepositorio _repodocumento = new PgeneralDocumentoPwRepositorio();
                    //var documento = _repodocumento.FirstBy(w => w.IdPgeneral == pespecifico.IdProgramaGeneral, s => new { IdDocumento = s.IdDocumento });
                    var documento = _repodocumento.DocumentoSilabov2(pespecifico.IdProgramaGeneral);

                    DocumentoSeccionPwRepositorio _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio();
                    var listaDocumentoSeccion = _repDocumentoSeccionPw.ObtenerDocumentoSeccionPorId(documento.IdDocumento);
                    foreach (var item in listaDocumentoSeccion)
                    {
                        if (item.IdSeccionPW == 95) //"Objetivos" //20
                            bo.ObjetivoAprendizaje = item.Contenido;
                        if (item.IdSeccionPW == 97) //PautaComplementaria //"Estructura Curricular" //45
                            bo.PautaComplementaria = item.Contenido;
                        if (item.IdSeccionPW == 92) //"Público Objetivo"
                            bo.PublicoObjetivo = item.Contenido;
                        if (item.IdSeccionPW == 96) //Material //"Pre-Requisitos" //44
                            bo.Material = item.Contenido;
                        if (item.IdSeccionPW == 93) //"Bibliografia"
                            bo.Bibliografia = item.Contenido;
                    }

                    bo.IdPespecifico = idPespecifico;
                }
                //List<EvaluacionListadoDTO> listado = _repEvaluacion.ListadoPorPrograma(idPespecifico);
                return Ok(bo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Registrar([FromBody]PEspecificoSilaboRegistrarDTO silabo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PEspecificoSilaboBO silaboBo;
                EvaluacionRepositorio _repoEvaluacion = new EvaluacionRepositorio(_integraDBContext);

                List<EvaluacionBO> listadoEvaluacionNueva = new List<EvaluacionBO>();

                var listadoIdEvaluacionExistente =
                    _repoEvaluacion.GetBy(w => w.IdPespecifico == silabo.IdPespecifico, s => new {s.Id});

                if (_repSilabo.Exist(w => w.Id == silabo.Id))
                {
                    silaboBo = _repSilabo.FirstById(silabo.Id);

                    silaboBo.IdPespecifico = silabo.IdPespecifico;
                    silaboBo.ObjetivoAprendizaje = silabo.ObjetivoAprendizaje;
                    silaboBo.PautaComplementaria = silabo.PautaComplementaria;
                    silaboBo.PublicoObjetivo = silabo.PublicoObjetivo;
                    silaboBo.Material = silabo.Material;
                    silaboBo.Bibliografia = silabo.Bibliografia;

                    silaboBo.Estado = true;
                    silaboBo.FechaModificacion = DateTime.Now;
                    silaboBo.UsuarioModificacion = silabo.Usuario;
                }
                else
                {
                    silaboBo = new PEspecificoSilaboBO();

                    silaboBo.IdPespecifico = silabo.IdPespecifico;
                    silaboBo.ObjetivoAprendizaje = silabo.ObjetivoAprendizaje;
                    silaboBo.PautaComplementaria = silabo.PautaComplementaria;
                    silaboBo.PublicoObjetivo = silabo.PublicoObjetivo;
                    silaboBo.Material = silabo.Material;
                    silaboBo.Bibliografia = silabo.Bibliografia;

                    silaboBo.Estado = true;
                    silaboBo.FechaCreacion = DateTime.Now;
                    silaboBo.FechaModificacion = DateTime.Now;
                    silaboBo.UsuarioCreacion = silabo.Usuario;
                    silaboBo.UsuarioModificacion = silabo.Usuario;
                }

                foreach (var evaluacion in silabo.ListadoCriteriosEvaluacion)
                {
                    EvaluacionBO bo = new EvaluacionBO();
                    bo.IdPespecifico = evaluacion.IdPEspecifico;
                    bo.Nombre = evaluacion.Nombre;
                    bo.Porcentaje = evaluacion.Porcentaje;
                    bo.Aprobado = false;

                    bo.Estado = true;
                    bo.FechaCreacion = DateTime.Now;
                    bo.FechaModificacion = DateTime.Now;
                    bo.UsuarioCreacion = silabo.Usuario;
                    bo.UsuarioModificacion = silabo.Usuario;

                    listadoEvaluacionNueva.Add(bo);
                }

                bool resultado;
                using (TransactionScope scope = new TransactionScope())
                {
                    resultado = _repSilabo.Update(silaboBo);
                    bool resultadoEvaluacionExistente =
                        _repoEvaluacion.Delete(listadoIdEvaluacionExistente.Select(s => s.Id), silabo.Usuario);
                    bool resultadoEvaluacionNueva = _repoEvaluacion.Update(listadoEvaluacionNueva);
                    scope.Complete();
                }

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Aprobar([FromBody]PEspecificoSilaboRegistrarDTO silabo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (silabo.ListadoCriteriosEvaluacion.Sum(s => s.Porcentaje) != 100)
                    return BadRequest("El porcentaje total debe de sumar el 100%");

                PEspecificoSilaboBO silaboBo;
                EvaluacionRepositorio _repoEvaluacion = new EvaluacionRepositorio(_integraDBContext);

                List<EvaluacionBO> listadoEvaluacionNueva = new List<EvaluacionBO>();

                var listadoIdEvaluacionExistente =
                    _repoEvaluacion.GetBy(w => w.IdPespecifico == silabo.IdPespecifico, s => new { s.Id });

                if (_repSilabo.Exist(w => w.Id == silabo.Id))
                {
                    silaboBo = _repSilabo.FirstById(silabo.Id);

                    silaboBo.IdPespecifico = silabo.IdPespecifico;
                    silaboBo.ObjetivoAprendizaje = silabo.ObjetivoAprendizaje;
                    silaboBo.PautaComplementaria = silabo.PautaComplementaria;
                    silaboBo.PublicoObjetivo = silabo.PublicoObjetivo;
                    silaboBo.Material = silabo.Material;
                    silaboBo.Bibliografia = silabo.Bibliografia;
                    silaboBo.Aprobado = true;

                    silaboBo.Estado = true;
                    silaboBo.FechaModificacion = DateTime.Now;
                    silaboBo.UsuarioModificacion = silabo.Usuario;
                }
                else
                {
                    silaboBo = new PEspecificoSilaboBO();

                    silaboBo.IdPespecifico = silabo.IdPespecifico;
                    silaboBo.ObjetivoAprendizaje = silabo.ObjetivoAprendizaje;
                    silaboBo.PautaComplementaria = silabo.PautaComplementaria;
                    silaboBo.PublicoObjetivo = silabo.PublicoObjetivo;
                    silaboBo.Material = silabo.Material;
                    silaboBo.Bibliografia = silabo.Bibliografia;
                    silaboBo.Aprobado = true;

                    silaboBo.Estado = true;
                    silaboBo.FechaCreacion = DateTime.Now;
                    silaboBo.FechaModificacion = DateTime.Now;
                    silaboBo.UsuarioCreacion = silabo.Usuario;
                    silaboBo.UsuarioModificacion = silabo.Usuario;
                }

                foreach (var evaluacion in silabo.ListadoCriteriosEvaluacion)
                {
                    EvaluacionBO bo = new EvaluacionBO();
                    bo.IdPespecifico = evaluacion.IdPEspecifico;
                    bo.Grupo = evaluacion.Grupo;
                    bo.Nombre = evaluacion.Nombre;
                    bo.Porcentaje = evaluacion.Porcentaje;
                    bo.Aprobado = true;

                    bo.Estado = true;
                    bo.FechaCreacion = DateTime.Now;
                    bo.FechaModificacion = DateTime.Now;
                    bo.UsuarioCreacion = silabo.Usuario;
                    bo.UsuarioModificacion = silabo.Usuario;

                    listadoEvaluacionNueva.Add(bo);
                }

                //adicion de la aprobacion
                PespecificoParticipacionDocenteRepositorio participacionRepositorio = new PespecificoParticipacionDocenteRepositorio(_integraDBContext);
                PespecificoParticipacionDocenteBO participacionBO;
                participacionBO = participacionRepositorio.FirstBy(w => w.IdPespecifico == silabo.IdPespecifico);
                if (participacionBO == null)
                {
                    participacionBO = new PespecificoParticipacionDocenteBO()
                    {
                        IdPespecifico = silabo.IdPespecifico,
                        IdExpositor = silabo.IdProveedor,
                        EsSilaboAprobado = true,
                        Estado = true,
                        UsuarioCreacion = silabo.Usuario,
                        UsuarioModificacion = silabo.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                }
                else
                {
                    participacionBO.EsSilaboAprobado = true;
                    participacionBO.UsuarioModificacion = silabo.Usuario;
                    participacionBO.FechaModificacion = DateTime.Now;
                }

                bool resultado;
                using (TransactionScope scope = new TransactionScope())
                {
                    resultado = _repSilabo.Update(silaboBo);
                    bool resultadoEvaluacionExistente =
                        _repoEvaluacion.Delete(listadoIdEvaluacionExistente.Select(s => s.Id), silabo.Usuario);
                    bool resultadoEvaluacionNueva = _repoEvaluacion.Update(listadoEvaluacionNueva);
                    var resultadoAprobacion = participacionRepositorio.Update(participacionBO);
                    scope.Complete();
                }

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idExpositor}/{filtro}")]
        [HttpGet]
        public ActionResult ListadoPendientePorDocente(int idExpositor, string filtro)
        {
            try
            {
                //var listado = _repSilabo.ListadoPendientePorDocente(idDocente, filtro);
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                var listado = pespecificoRepositorio.ObtenerHistorialParticipacionV3PorExpositorPortal(idExpositor);

                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{idProveedor}/{filtro}")]
        [HttpGet]
        public ActionResult ListadoPendientePorProveedor(int idProveedor, string filtro)
        {
            try
            {
                //var listado = _repSilabo.ListadoPendientePorDocente(idDocente, filtro);
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                var listado = pespecificoRepositorio.ObtenerHistorialParticipacionV3PorProveedorPortal(idProveedor);

                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ListadoPendientePorDocenteFiltrado([FromBody]CursoPorDocenteFiltroDTO filtro)
        {
            try
            {
                var listado = _repSilabo.ListadoCcPorDocenteFiltrado(filtro);

                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ListadoPendientePorExpositorFiltrado([FromBody] ParticipacionExpositorFiltroDTO filtro)
        {
            try
            {
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                var listado = pespecificoRepositorio.ObtenerHistorialParticipacionV3Portal_Filtrado(filtro);

                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}