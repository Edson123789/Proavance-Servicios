using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ProgramaCapacitacion")]
    public class ProgramaCapacitacionController : BaseController<TProgramaCapacitacion, ValidadorProgramaCapacitacionDTO>
    {
        public ProgramaCapacitacionController(IIntegraRepository<TProgramaCapacitacion> repositorio, ILogger<BaseController<TProgramaCapacitacion, ValidadorProgramaCapacitacionDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        #region Metodos_Adicionales
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPGenerales()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralRepositorio _repoPGeneral = new PgeneralRepositorio();
                var lista = _repoPGeneral.ObtenerProgramasFiltroDeSubAreasCapacitacion();
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPGeneralesPorIdProgramaCapacitacion(int IdProgramaCapacitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaCapacitacionRepositorio _repoPCapacitacionRepositorio = new ProgramaCapacitacionRepositorio();
                var lista = _repoPCapacitacionRepositorio.ObtenerListasIdPGeneralPorIdProgramaCapacitacion(IdProgramaCapacitacion);
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaTipoTema()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoTemaProgramaCapacitacionRepositorio _repoTipoTema = new TipoTemaProgramaCapacitacionRepositorio();
                var lista = _repoTipoTema.ObtenerTodoTipoTemaProgramaCapacitacion();
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        #endregion




        #region CRUD
        [Route("[Action]")]
        [HttpGet]
        public ActionResult VisualizarProgramaCapacitacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ProgramaCapacitacionRepositorio _repoProgramaCapacitacion = new ProgramaCapacitacionRepositorio();
                var ProgramaCapacitacion = _repoProgramaCapacitacion.ObtenerTodoProgramaCapacitacionExtendido();
                var ListasPGeneralAreaSubArea = _repoProgramaCapacitacion.ObtenerListasPGeneralAreasSubAreasDeProgramasCapacitacion().ToList();

                for (int i = 0; i<ProgramaCapacitacion.Count; ++i)
                {
                    ProgramaCapacitacion[i].NombreProgramaGeneral = "";
                    ProgramaCapacitacion[i].NombreAreaCapacitacion = "";
                    ProgramaCapacitacion[i].NombreSubAreaCapacitacion = "";

                    var Lista = ListasPGeneralAreaSubArea.FindAll(x => x.Id == ProgramaCapacitacion[i].Id).ToList() ;
                    for (int s=0; s< Lista.Count; ++s)
                    {
                        if (s == 0) {
                            ProgramaCapacitacion[i].NombreProgramaGeneral += Lista[s].NombreProgramaGeneral;
                            ProgramaCapacitacion[i].NombreAreaCapacitacion += Lista[s].NombreAreaCapacitacion;
                            ProgramaCapacitacion[i].NombreSubAreaCapacitacion += Lista[s].NombreSubAreaCapacitacion;
                        }
                        else
                        {
                            ProgramaCapacitacion[i].NombreProgramaGeneral += "|*|" + Lista[s].NombreProgramaGeneral;
                            ProgramaCapacitacion[i].NombreAreaCapacitacion += "|*|" + Lista[s].NombreAreaCapacitacion;
                            ProgramaCapacitacion[i].NombreSubAreaCapacitacion += "|*|" + Lista[s].NombreSubAreaCapacitacion;
                        }
                    }
                }

                return Json(new { Result = "OK", Records = ProgramaCapacitacion });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarProgramaCapacitacion([FromBody] ProgramaCapacitacionCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaCapacitacionRepositorio _repoProgramaCapacitacion = new ProgramaCapacitacionRepositorio();
                ProgramaCapacitacionPorPGeneralRepositorio _repoProgramaCapacitacionPorPGeneral = new ProgramaCapacitacionPorPGeneralRepositorio();




                ProgramaCapacitacionBO ProgramaCapacitacionNuevo = new ProgramaCapacitacionBO();

                ProgramaCapacitacionNuevo.Nombre = ObjetoDTO.Nombre;
                ProgramaCapacitacionNuevo.Descripcion = ObjetoDTO.Descripcion;
                ProgramaCapacitacionNuevo.IdTipoTemaProgramaCapacitacion = ObjetoDTO.IdTipoTemaProgramaCapacitacion;

                ProgramaCapacitacionNuevo.Estado = true;
                ProgramaCapacitacionNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                ProgramaCapacitacionNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                ProgramaCapacitacionNuevo.FechaCreacion = DateTime.Now;
                ProgramaCapacitacionNuevo.FechaModificacion = DateTime.Now;

                _repoProgramaCapacitacion.Insert(ProgramaCapacitacionNuevo);



                //insercion de ProgramasGenerales relacionados
                if (ObjetoDTO.PGenerales != null)
                {
                    var listaPGenerales = ObjetoDTO.PGenerales;
                    for (int i=0; i < listaPGenerales.Count; ++i)
                    {
                        ProgramaCapacitacionPorPGeneralBO ProgramaCapacitacionPorPGeneral = new ProgramaCapacitacionPorPGeneralBO();
                        ProgramaCapacitacionPorPGeneral.IdProgramaCapacitacion = ProgramaCapacitacionNuevo.Id;
                        ProgramaCapacitacionPorPGeneral.IdPGeneral = listaPGenerales[i];

                        ProgramaCapacitacionPorPGeneral.Estado = true;
                        ProgramaCapacitacionPorPGeneral.UsuarioCreacion = ObjetoDTO.Usuario;
                        ProgramaCapacitacionPorPGeneral.UsuarioModificacion = ObjetoDTO.Usuario;
                        ProgramaCapacitacionPorPGeneral.FechaCreacion = DateTime.Now;
                        ProgramaCapacitacionPorPGeneral.FechaModificacion = DateTime.Now;

                        _repoProgramaCapacitacionPorPGeneral.Insert(ProgramaCapacitacionPorPGeneral);
                    }
                }



                var ProgramaCapacitacion = _repoProgramaCapacitacion.ObtenerProgramaCapacitacionExtendidoPorId(ProgramaCapacitacionNuevo.Id);
                if (ProgramaCapacitacion.Count > 1 || ProgramaCapacitacion.Count < 1)
                    throw new Exception("Multiples Registros con el mismo Id o Ninguno Encontrado");

                var ListasPGeneralAreaSubArea = _repoProgramaCapacitacion.ObtenerListasPGeneralAreasSubAreasDeProgramasCapacitacion().ToList();
                ProgramaCapacitacion[0].NombreProgramaGeneral = "";
                ProgramaCapacitacion[0].NombreAreaCapacitacion = "";
                ProgramaCapacitacion[0].NombreSubAreaCapacitacion = "";

                var Lista = ListasPGeneralAreaSubArea.FindAll(x => x.Id == ProgramaCapacitacion[0].Id).ToList();
                for (int s = 0; s < Lista.Count; ++s)
                {
                    if (s == 0)
                    {
                        ProgramaCapacitacion[0].NombreProgramaGeneral += Lista[s].NombreProgramaGeneral;
                        ProgramaCapacitacion[0].NombreAreaCapacitacion += Lista[s].NombreAreaCapacitacion;
                        ProgramaCapacitacion[0].NombreSubAreaCapacitacion += Lista[s].NombreSubAreaCapacitacion;
                    }
                    else
                    {
                        ProgramaCapacitacion[0].NombreProgramaGeneral += "|*|" + Lista[s].NombreProgramaGeneral;
                        ProgramaCapacitacion[0].NombreAreaCapacitacion += "|*|" + Lista[s].NombreAreaCapacitacion;
                        ProgramaCapacitacion[0].NombreSubAreaCapacitacion += "|*|" + Lista[s].NombreSubAreaCapacitacion;
                    }
                }


                return Ok(ProgramaCapacitacion[0]);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarProgramaCapacitacion([FromBody] ProgramaCapacitacionCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaCapacitacionRepositorio _repoProgramaCapacitacion = new ProgramaCapacitacionRepositorio();
                ProgramaCapacitacionPorPGeneralRepositorio _repoProgramaCapacitacionPorPGeneral = new ProgramaCapacitacionPorPGeneralRepositorio();




                ProgramaCapacitacionBO ProgramaCapacitacion = _repoProgramaCapacitacion.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                ProgramaCapacitacion.Nombre = ObjetoDTO.Nombre;
                ProgramaCapacitacion.Descripcion = ObjetoDTO.Descripcion;
                ProgramaCapacitacion.IdTipoTemaProgramaCapacitacion = ObjetoDTO.IdTipoTemaProgramaCapacitacion;

                ProgramaCapacitacion.Estado = true;
                ProgramaCapacitacion.UsuarioModificacion = ObjetoDTO.Usuario;
                ProgramaCapacitacion.FechaModificacion = DateTime.Now;

                _repoProgramaCapacitacion.Update(ProgramaCapacitacion);


                var ListaEnDB = _repoProgramaCapacitacionPorPGeneral.GetBy(x=>x.IdProgramaCapacitacion==ObjetoDTO.Id).ToList();
                for (int i = 0; i < ListaEnDB.Count; ++i)
                    _repoProgramaCapacitacionPorPGeneral.Delete(ListaEnDB[i].Id, ObjetoDTO.Usuario);

                //insercion de ProgramasGenerales relacionados
                if (ObjetoDTO.PGenerales != null)
                {
                    var listaPGenerales = ObjetoDTO.PGenerales;
                    for (int i = 0; i < listaPGenerales.Count; ++i)
                    {
                        ProgramaCapacitacionPorPGeneralBO ProgramaCapacitacionPorPGeneral = new ProgramaCapacitacionPorPGeneralBO();
                        ProgramaCapacitacionPorPGeneral.IdProgramaCapacitacion = ProgramaCapacitacion.Id;
                        ProgramaCapacitacionPorPGeneral.IdPGeneral = listaPGenerales[i];

                        ProgramaCapacitacionPorPGeneral.Estado = true;
                        ProgramaCapacitacionPorPGeneral.UsuarioCreacion = ObjetoDTO.Usuario;
                        ProgramaCapacitacionPorPGeneral.UsuarioModificacion = ObjetoDTO.Usuario;
                        ProgramaCapacitacionPorPGeneral.FechaCreacion = DateTime.Now;
                        ProgramaCapacitacionPorPGeneral.FechaModificacion = DateTime.Now;

                        _repoProgramaCapacitacionPorPGeneral.Insert(ProgramaCapacitacionPorPGeneral);
                    }
                }



                var ProgramaCapacitacionActualizado = _repoProgramaCapacitacion.ObtenerProgramaCapacitacionExtendidoPorId(ProgramaCapacitacion.Id);
                if (ProgramaCapacitacionActualizado.Count > 1 || ProgramaCapacitacionActualizado.Count < 1)
                    throw new Exception("Multiples Registros con el mismo Id o Ninguno Encontrado");

                var ListasPGeneralAreaSubArea = _repoProgramaCapacitacion.ObtenerListasPGeneralAreasSubAreasDeProgramasCapacitacion().ToList();
                ProgramaCapacitacionActualizado[0].NombreProgramaGeneral = "";
                ProgramaCapacitacionActualizado[0].NombreAreaCapacitacion = "";
                ProgramaCapacitacionActualizado[0].NombreSubAreaCapacitacion = "";

                var Lista = ListasPGeneralAreaSubArea.FindAll(x => x.Id == ProgramaCapacitacionActualizado[0].Id).ToList();
                for (int s = 0; s < Lista.Count; ++s)
                {
                    if (s == 0)
                    {
                        ProgramaCapacitacionActualizado[0].NombreProgramaGeneral += Lista[s].NombreProgramaGeneral;
                        ProgramaCapacitacionActualizado[0].NombreAreaCapacitacion += Lista[s].NombreAreaCapacitacion;
                        ProgramaCapacitacionActualizado[0].NombreSubAreaCapacitacion += Lista[s].NombreSubAreaCapacitacion;
                    }
                    else
                    {
                        ProgramaCapacitacionActualizado[0].NombreProgramaGeneral += "|*|" + Lista[s].NombreProgramaGeneral;
                        ProgramaCapacitacionActualizado[0].NombreAreaCapacitacion += "|*|" + Lista[s].NombreAreaCapacitacion;
                        ProgramaCapacitacionActualizado[0].NombreSubAreaCapacitacion += "|*|" + Lista[s].NombreSubAreaCapacitacion;
                    }
                }

                return Ok(ProgramaCapacitacionActualizado[0]);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]/{UserName}/{Id}")]
        [HttpDelete]
        public ActionResult EliminarProgramaCapacitacion(int Id, string UserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaCapacitacionRepositorio _repoProgramaCapacitacion = new ProgramaCapacitacionRepositorio();
                ProgramaCapacitacionPorPGeneralRepositorio _repoProgramaCapacitacionPorPGeneral = new ProgramaCapacitacionPorPGeneralRepositorio();

                var ListaEnDB = _repoProgramaCapacitacionPorPGeneral.GetBy(x => x.IdProgramaCapacitacion == Id).ToList();
                for (int i = 0; i < ListaEnDB.Count; ++i)
                    _repoProgramaCapacitacionPorPGeneral.Delete(ListaEnDB[i].Id, UserName);




                _repoProgramaCapacitacion.Delete(Id, UserName);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion 
    }




    public class ValidadorProgramaCapacitacionDTO : AbstractValidator<TProgramaCapacitacion>
    {
        public static ValidadorProgramaCapacitacionDTO Current = new ValidadorProgramaCapacitacionDTO();
        public ValidadorProgramaCapacitacionDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
            
        }
    }
}
