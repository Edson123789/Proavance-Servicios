using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Operaciones
{
    [Route("api/Operaciones/Asistencia")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private AsistenciaRepositorio _repoAsistencia;

        private readonly integraDBContext _integraDBContext;

        public AsistenciaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repoAsistencia = new AsistenciaRepositorio(_integraDBContext);
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Registrar([FromForm]List<AsistenciaRegistrarDTO> notas, [FromForm]int idPEspecifico, [FromForm]string usuario)
        {
            var data = Request;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<AsistenciaBO> listado = new List<AsistenciaBO>();
                List<AsistenciaBO> listadoAsistenciaExistente = new List<AsistenciaBO>();
                if (notas != null && notas.Count > 0)
                {
                    listadoAsistenciaExistente = _repoAsistencia.GetBy(w => notas.Select(s => s.Id).Contains(w.Id)).ToList();
                }
                foreach (var nota in notas)
                {
                    if (listadoAsistenciaExistente.Any(w => w.Id == nota.Id))
                    {
                        AsistenciaBO notaBo = listadoAsistenciaExistente.FirstOrDefault(w => w.Id == nota.Id);

                        notaBo.IdPespecificoSesion = nota.IdPEspecificoSesion;
                        notaBo.IdMatriculaCabecera = nota.IdMatriculaCabecera;
                        notaBo.Asistio = nota.Asistio;
                        notaBo.Justifico = nota.Justifico;
                        
                        notaBo.Estado = true;
                        notaBo.FechaModificacion = DateTime.Now;
                        notaBo.UsuarioModificacion = usuario;

                        listado.Add(notaBo);
                    }
                    else
                    {
                        AsistenciaBO notaBo = new AsistenciaBO();
                        notaBo.Id = nota.Id;
                        notaBo.IdPespecificoSesion = nota.IdPEspecificoSesion;
                        notaBo.IdMatriculaCabecera = nota.IdMatriculaCabecera;
                        notaBo.Asistio = nota.Asistio;
                        notaBo.Justifico = nota.Justifico;

                        notaBo.Estado = true;
                        notaBo.FechaCreacion = DateTime.Now;
                        notaBo.FechaModificacion = DateTime.Now;
                        notaBo.UsuarioCreacion = usuario;
                        notaBo.UsuarioModificacion = usuario;

                        listado.Add(notaBo);
                    }
                }

                bool resultado;
                using (TransactionScope scope = new TransactionScope())
                {
                    resultado = _repoAsistencia.Update(listado);
                    scope.Complete();
                }

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + (e.InnerException != null ? ("- " + e.InnerException.Message) : ""));
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Aprobar([FromForm]List<AsistenciaRegistrarDTO> notas, [FromForm]int idPEspecifico, [FromForm] int grupo, [FromForm]string usuario)
        {
            var data = Request;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<AsistenciaBO> listado = new List<AsistenciaBO>();
                List<AsistenciaBO> listadoAsistenciaExistente = new List<AsistenciaBO>();
                if (notas != null && notas.Count > 0)
                {
                    listadoAsistenciaExistente = _repoAsistencia.GetBy(w => notas.Select(s => s.Id).Contains(w.Id)).ToList();
                }
                foreach (var nota in notas)
                {
                    if (listadoAsistenciaExistente.Any(w => w.Id == nota.Id))
                    {
                        AsistenciaBO notaBo = listadoAsistenciaExistente.FirstOrDefault(w => w.Id == nota.Id);

                        notaBo.IdPespecificoSesion = nota.IdPEspecificoSesion;
                        notaBo.IdMatriculaCabecera = nota.IdMatriculaCabecera;
                        notaBo.Asistio = nota.Asistio;
                        notaBo.Justifico = nota.Justifico;

                        notaBo.Estado = true;
                        notaBo.FechaModificacion = DateTime.Now;
                        notaBo.UsuarioModificacion = usuario;

                        listado.Add(notaBo);
                    }
                    else
                    {
                        AsistenciaBO notaBo = new AsistenciaBO();
                        notaBo.Id = nota.Id;
                        notaBo.IdPespecificoSesion = nota.IdPEspecificoSesion;
                        notaBo.IdMatriculaCabecera = nota.IdMatriculaCabecera;
                        notaBo.Asistio = nota.Asistio;
                        notaBo.Justifico = nota.Justifico;

                        notaBo.Estado = true;
                        notaBo.FechaCreacion = DateTime.Now;
                        notaBo.FechaModificacion = DateTime.Now;
                        notaBo.UsuarioCreacion = usuario;
                        notaBo.UsuarioModificacion = usuario;

                        listado.Add(notaBo);
                    }
                }

                //adicion de la aprobacion
                PEspecificoAprobacionCalificacionRepositorio aprobacionRepositorio = new PEspecificoAprobacionCalificacionRepositorio(_integraDBContext);
                PEspecificoAprobacionCalificacionBO aprobacionBO;
                if (aprobacionRepositorio.Exist(w => w.IdPespecifico == idPEspecifico && w.Grupo == grupo))
                {
                    aprobacionBO = aprobacionRepositorio.FirstBy(w => w.IdPespecifico == idPEspecifico && w.Grupo == grupo);
                    aprobacionBO.EsAsistenciaAprobada = true;
                    aprobacionBO.FechaAprobacionAsistencia = DateTime.Now;
                    aprobacionBO.UsuarioModificacion = usuario;
                    aprobacionBO.FechaModificacion = DateTime.Now;
                }
                else
                {
                    aprobacionBO = new PEspecificoAprobacionCalificacionBO()
                    {
                        IdPespecifico = idPEspecifico,
                        Grupo = grupo,
                        EsAsistenciaAprobada = true,
                        FechaAprobacionAsistencia = DateTime.Now,

                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                }

                bool resultado;
                using (TransactionScope scope = new TransactionScope())
                {
                    resultado = _repoAsistencia.Update(listado);
                    var resultadoAprobacion = aprobacionRepositorio.Update(aprobacionBO);
                    scope.Complete();
                }

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + (e.InnerException != null ? ("- " + e.InnerException.Message) : ""));
            }
        }

        [Route("[Action]/{idPespecifico}/{grupo}")]
        public ActionResult ObtenerResumenIngresoAsistencia(int idPEspecifico, int grupo)
        {
            try
            {
                PespecificoSesionRepositorio _repoSesiones = new PespecificoSesionRepositorio();
                var listado = _repoSesiones.ObtenerResumenRegistroAsistencia(idPEspecifico, grupo);
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + (e.InnerException != null ? ("- " + e.InnerException.Message) : ""));
            }
        }
    }
}