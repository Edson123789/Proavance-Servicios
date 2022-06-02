using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/CentroCosto")]
    [ApiController]
    public class CentroCostoOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                RaCentroCostoRepositorio _repCentroCosto = new RaCentroCostoRepositorio();
                return Ok(_repCentroCosto.GetBy(x => x.Estado, x => new { x.Id, x.NombreCentroCosto, x.NombrePespecifico, x.IdPespecifico, x.IdCentroCosto, x.IdRaCentroCostoEstado, x.Coordinador, x.ResponsableCoordinacion }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltro()
        {
            try
            {
                CentroCostoCertificadoRepositorio _repCentroCosto = new CentroCostoCertificadoRepositorio();

                return Ok(_repCentroCosto.GetBy(w=>w.Estado,y=> new { y.IdCentroCosto}));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltrado(string TextoBuscar, string UsuarioCoordinador, int IdRaCentroCostoEstado = 3)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCentroCostoRepositorio _repCentroCosto = new RaCentroCostoRepositorio();
                RaCentroCostoEstadoRepositorio _repCentroCostoEstado = new RaCentroCostoEstadoRepositorio();
                if (!_repCentroCostoEstado.Exist(IdRaCentroCostoEstado)) {
                    return BadRequest("Estado de centro de costo incorrecto");
                }
                //filtra segun los parametros enviados
                var listadoCentroCosto = _repCentroCosto.GetBy(x => x.IdRaCentroCostoEstado == IdRaCentroCostoEstado, x => new { x.Id, x.NombreCentroCosto, x.NombrePespecifico, x.IdPespecifico, x.IdCentroCosto, x.IdRaCentroCostoEstado, x.Coordinador, x.ResponsableCoordinacion }).ToList();

                if (!string.IsNullOrEmpty(TextoBuscar)) {
                    listadoCentroCosto = listadoCentroCosto.Where(s => s.NombreCentroCosto.ToUpper().Contains(TextoBuscar.Trim().ToUpper()) ||
                                        s.NombrePespecifico.ToUpper().Contains(TextoBuscar.Trim().ToUpper())).ToList();
                }
                if (!string.IsNullOrEmpty(UsuarioCoordinador)) {
                    listadoCentroCosto = listadoCentroCosto.Where(s => s.Coordinador == UsuarioCoordinador).ToList();
                }

                return Ok(listadoCentroCosto.OrderBy(x => x.NombreCentroCosto).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDetalle(int Id)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaCentroCostoRepositorio _repRaCentroCostoRepositorio = new RaCentroCostoRepositorio();
                RaCursoRepositorio _repCursoRepositorio = new RaCursoRepositorio();
                RaSesionRepositorio _repSesion = new RaSesionRepositorio();
                RaEvaluacionRepositorio _repEvaluacion = new RaEvaluacionRepositorio();
                RaCursoMaterialRepositorio _repCursoMaterial = new RaCursoMaterialRepositorio();
                RaCursoTrabajoAlumnoRepositorio _repCursoTrabajoAlumno = new RaCursoTrabajoAlumnoRepositorio();
                ExpositorRepositorio _repExpositor = new ExpositorRepositorio();
                RaCursoObservacionRepositorio _repCursoObservacion = new RaCursoObservacionRepositorio();
                List<RaListadoMinimoCursoDTO> listaCurso = _repCursoRepositorio.GetBy(x => x.Estado == true && x.IdRaCentroCosto == Id, x => new RaListadoMinimoCursoDTO { Id = x.Id, NombreCurso = x.NombreCurso, Orden = x.Orden, Grupo = x.Grupo, PorcentajeAsistencia = x.PorcentajeAsistencia, IdExpositor = x.IdExpositor, IdRaCentroCosto = x.IdRaCentroCosto, Finalizado = x.Finalizado }).ToList();
                List<RaExpositorDTO> listaDocente = _repExpositor.GetBy(x => x.Estado == true, x => new RaExpositorDTO { Id = x.Id, NombresCompletos = string.Concat(x.PrimerNombre, " ", x.SegundoNombre, " ", x.ApellidoPaterno, " ", x.ApellidoMaterno) }).ToList();

                foreach (var item in listaCurso)
                {
                    item.Docente = listaDocente.Where(x => x.Id == item.IdExpositor).FirstOrDefault();
                    item.ListadoMinimoMaterialPresencial = _repCursoMaterial.ObtenerListadoMinimoPorCurso(item.Id);
                    item.ListadoMinimoCursoTrabajoAlumno = _repCursoTrabajoAlumno.ObtenerListadoMinimoPorCurso(item.Id);
                    item.ListadoMinimoCursoObservacion = _repCursoObservacion.ObtenerListadoMinimoPorCurso(item.Id);
                    item.ListadoMinimoEvaluacion = _repEvaluacion.ObtenerListadoMinimoPorCurso(item.Id);
                    item.ListadoMinimoSesion = _repSesion.ObtenerListadoMinimoPorCurso(item.Id);
                }

                DetalleCentroCostoDTO detalleCentroCosto = new DetalleCentroCostoDTO() {
                    Id = Id,
                    ListadoCursoMinimo = listaCurso,
                    ListadoAlumnos = new List<RaAlumnoListadoMinimoDTO>() {
                        new RaAlumnoListadoMinimoDTO{ Id = 1, Nombre = "test" },
                        new RaAlumnoListadoMinimoDTO{ Id = 1, Nombre = "test" }
                    }
                };
                return Ok(detalleCentroCosto);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody]RaCentroCostoDTO CentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCentroCostoRepositorio _repCentroCosto = new RaCentroCostoRepositorio();
                if (!_repCentroCosto.ExistePorNombre(CentroCosto.NombreCentroCosto))
                {
                    RaCentroCostoBO centroCostoBO = new RaCentroCostoBO()
                    {
                        IdCentroCosto = CentroCosto.IdCentroCosto,
                        NombreCentroCosto = CentroCosto.NombreCentroCosto.ToUpper().Trim(),
                        IdPespecifico = CentroCosto.IdPespecifico,
                        NombrePespecifico = CentroCosto.NombrePespecifico,
                        NroCursosCertificado = 1,// centroCostoBONroCursosCertificado, TODO
                        IdRaCentroCostoEstado = 1,//ValorEstatico.
                        ResponsableCoordinacion = CentroCosto.ResponsableCoordinacion,
                        VisibleCoordinador = true,
                        Estado = true,
                        UsuarioCreacion = CentroCosto.NombreUsuario,
                        UsuarioModificacion = CentroCosto.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    _repCentroCosto.Insert(centroCostoBO);
                    return Ok(centroCostoBO);
                }
                else {
                    return BadRequest("El Centro de Costo ingresado ya existe en la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] RaCentroCostoDTO CentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCentroCostoRepositorio _repCentroCosto = new RaCentroCostoRepositorio();
                if (_repCentroCosto.Exist(CentroCosto.Id))
                {
                    var raCentroCostoBO = _repCentroCosto.FirstById(CentroCosto.Id);
                    raCentroCostoBO.NombrePespecifico = CentroCosto.NombrePespecifico;
                    raCentroCostoBO.IdRaCentroCostoEstado = CentroCosto.IdRaCentroCostoEstado;
                    raCentroCostoBO.IdRaFrecuencia = CentroCosto.IdRaFrecuencia;
                    raCentroCostoBO.IdRaCentroCostoEstado = CentroCosto.IdRaCentroCostoEstado;
                    raCentroCostoBO.NroCursosCertificado = 1;// CentroCosto.NroCursosCertificado;
                    raCentroCostoBO.ResponsableCoordinacion = CentroCosto.ResponsableCoordinacion;
                    raCentroCostoBO.Observacion = CentroCosto.Observacion;
                    raCentroCostoBO.Estado = true;
                    raCentroCostoBO.FechaModificacion = DateTime.Now;
                    raCentroCostoBO.UsuarioModificacion = CentroCosto.NombreUsuario;
                    if (!raCentroCostoBO.HasErrors)
                    {
                        _repCentroCosto.Update(raCentroCostoBO);
                    }
                    else
                    {
                        return BadRequest(raCentroCostoBO.ActualesErrores);
                    }
                    return Ok(new { raCentroCostoBO });
                }
                else
                {
                    return BadRequest("El Centro de Costo no existe en la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult AsignarCoordinador([FromBody] AsignarCoordinadorCentroCostoDTO CentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCentroCostoRepositorio _repCentroCosto = new RaCentroCostoRepositorio();
                CoordinadoraRepositorio _repCoordinadora = new CoordinadoraRepositorio();

                if (_repCentroCosto.Exist(CentroCosto.Id))
                {
                    if (_repCoordinadora.ExistePorNombreUsuario(CentroCosto.UsuarioCoordinador))
                    {
                        var raCentroCostoBO = _repCentroCosto.FirstById(CentroCosto.Id);
                        raCentroCostoBO.Coordinador = CentroCosto.UsuarioCoordinador;
                        raCentroCostoBO.Estado = true;
                        raCentroCostoBO.FechaModificacion = DateTime.Now;
                        raCentroCostoBO.UsuarioModificacion = CentroCosto.NombreUsuario;
                        if (!raCentroCostoBO.HasErrors)
                        {
                            _repCentroCosto.Update(raCentroCostoBO);
                        }
                        else
                        {
                            return BadRequest(raCentroCostoBO.ActualesErrores);
                        }
                        return Ok(new { raCentroCostoBO });
                    }
                    else {
                        return BadRequest("El Coordinador seleccionado no existe en la base de datos.");
                    }
                }
                else
                {
                    return BadRequest("El Centro de Costo no existe en la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]/{Id}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult AutoAsignar(int Id, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCentroCostoRepositorio _repCentroCosto = new RaCentroCostoRepositorio();
                CoordinadoraRepositorio _repCoordinadora = new CoordinadoraRepositorio();

                if (!_repCentroCosto.Exist(Id))
                {
                    return BadRequest("El Centro de Costo seleccionado no esta registrado.");
                }
                else
                {
                    var raCentroCostoBO = _repCentroCosto.FirstById(Id);
                    if (raCentroCostoBO.Coordinador != null)
                    {
                        return BadRequest("El Centro de Costo ya se encuentra asignado.");
                    }
                    else
                    {
                        raCentroCostoBO.Coordinador = NombreUsuario;
                        if (!raCentroCostoBO.HasErrors)
                        {
                            _repCentroCosto.Update(raCentroCostoBO);
                        }
                        else
                        {
                            return BadRequest(raCentroCostoBO.ActualesErrores);
                        }
                        return Ok(new { raCentroCostoBO });
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult VisualizarCronograma(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCentroCostoRepositorio _repCentroCosto = new RaCentroCostoRepositorio();

                if (!_repCentroCosto.Exist(Id))
                {
                    return BadRequest("El Centro de Costo no existe.");
                }
                else
                {
                    var raCentroCostoBO = _repCentroCosto.FirstById(Id);
                    if (raCentroCostoBO.Coordinador != null)
                    {
                        return BadRequest("El Centro de Costo ya se encuentra asignado.");
                    }
                    else
                    {
                        //Obtenemos cronograma
                        return Ok();
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
