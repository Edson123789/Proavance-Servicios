using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
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
    [Route("api/Causa")]
    public class CausaController : BaseController<TCausa, ValidadorCausaDTO>
    {
        public CausaController(IIntegraRepository<TCausa> repositorio, ILogger<BaseController<TCausa, ValidadorCausaDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }



        #region Servicios Adicionales
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoUsuarios()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaTipoUsuarioRepositorio _repoTipoUusario = new AgendaTipoUsuarioRepositorio();
                var TipoUusario = _repoTipoUusario.ObtenerTipoUsuarioFiltro();

                return Json(new { Result = "OK", Records = TipoUusario });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerSolucionesPorCausa(int IdCausa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SolucionRepositorio _repoSolucion = new SolucionRepositorio();
                var Soluciones = _repoSolucion.ObtenerSolucionesPorIdCausa(IdCausa);

                return Json(new { Result = "OK", Records = Soluciones });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerSolucionesPorCausaPorTipoUsuario(int IdCausa, int IdTipoUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SolucionRepositorio _repoSolucion = new SolucionRepositorio();
                var Soluciones = _repoSolucion.ObtenerSolucionesPorIdCausa(IdCausa, IdTipoUsuario);

                return Json(new { Result = "OK", Records = Soluciones });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        #endregion



        #region Servicios CRUD
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCausasPorProblema(int IdProblema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CausaRepositorio _repoCausa = new CausaRepositorio();
                var Causas = _repoCausa.ObtenerCausasPorIdProblema(IdProblema);

                return Json(new { Result = "OK", Records = Causas });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCausa([FromBody] CausaCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CausaRepositorio _repoCausa = new CausaRepositorio();
                SolucionRepositorio _repoSolucion = new SolucionRepositorio();

                CausaBO CausaNuevo = new CausaBO();
                CausaNuevo.IdProblema = ObjetoDTO.IdProblema;
                CausaNuevo.Nombre = ObjetoDTO.Nombre;
                CausaNuevo.Descripcion = ObjetoDTO.Descripcion;
                CausaNuevo.Estado = true;
                CausaNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                CausaNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                CausaNuevo.FechaCreacion = DateTime.Now;
                CausaNuevo.FechaModificacion = DateTime.Now;

                _repoCausa.Insert(CausaNuevo);

                if (ObjetoDTO.Soluciones != null)
                {
                    var Soluciones = ObjetoDTO.Soluciones;
                    for (int s = 0; s < Soluciones.Count; ++s)
                    {
                        SolucionBO Solucion = new SolucionBO();
                        Solucion.IdCausa = CausaNuevo.Id ;
                        Solucion.IdAgendaTipoUsuario = Soluciones[s].IdAgendaTipoUsuario ;
                        Solucion.Nombre = Soluciones[s].Nombre;
                        Solucion.Descripcion = Soluciones[s].Descripcion;
                        Solucion.Estado = true;
                        Solucion.UsuarioCreacion = ObjetoDTO.Usuario;
                        Solucion.UsuarioModificacion = ObjetoDTO.Usuario;
                        Solucion.FechaCreacion = DateTime.Now;
                        Solucion.FechaModificacion = DateTime.Now;

                        _repoSolucion.Insert(Solucion);
                    }

                }

                    
                return Ok(CausaNuevo);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarCausa([FromBody] CausaCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CausaRepositorio _repoCausa = new CausaRepositorio();
                SolucionRepositorio _repoSolucion = new SolucionRepositorio();

                CausaBO Causa = _repoCausa.GetBy(x=>x.Id==ObjetoDTO.Id).FirstOrDefault();
                Causa.IdProblema = ObjetoDTO.IdProblema;
                Causa.Nombre = ObjetoDTO.Nombre;
                Causa.Descripcion = ObjetoDTO.Descripcion;
                Causa.Estado = true;
                Causa.UsuarioModificacion = ObjetoDTO.Usuario;
                Causa.FechaModificacion = DateTime.Now;

                _repoCausa.Update(Causa);

                if (ObjetoDTO.Soluciones != null)
                {
                    var SolucionesEnDB = _repoSolucion.ObtenerSolucionesPorIdCausa(ObjetoDTO.Id).ToList();
                    for (int s = 0; s < SolucionesEnDB.Count; ++s)
                        _repoSolucion.Delete(SolucionesEnDB[s].Id, ObjetoDTO.Usuario);

                    // reinsercion
                    var Soluciones = ObjetoDTO.Soluciones;
                    for (int s = 0; s < Soluciones.Count; ++s)
                    {
                        SolucionBO Solucion = new SolucionBO();
                        Solucion.IdCausa = Causa.Id;
                        Solucion.IdAgendaTipoUsuario = Soluciones[s].IdAgendaTipoUsuario;
                        Solucion.Nombre = Soluciones[s].Nombre;
                        Solucion.Descripcion = Soluciones[s].Descripcion;
                        Solucion.Estado = true;
                        Solucion.UsuarioCreacion = ObjetoDTO.Usuario;
                        Solucion.UsuarioModificacion = ObjetoDTO.Usuario;
                        Solucion.FechaCreacion = DateTime.Now;
                        Solucion.FechaModificacion = DateTime.Now;

                        _repoSolucion.Insert(Solucion);
                    }

                }
                else
                {
                    var SolucionesEnDB = _repoSolucion.ObtenerSolucionesPorIdCausa(ObjetoDTO.Id).ToList();
                    for (int s = 0; s < SolucionesEnDB.Count; ++s)
                        _repoSolucion.Delete(SolucionesEnDB[s].Id, ObjetoDTO.Usuario);
                }

                return Ok(Causa);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarCausa([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CausaRepositorio _repoCausa = new CausaRepositorio();
                SolucionRepositorio _repoSolucion = new SolucionRepositorio();

                var SolucionesEnDB = _repoSolucion.ObtenerSolucionesPorIdCausa(Eliminar.Id).ToList();
                for (int s = 0; s < SolucionesEnDB.Count; ++s)
                    _repoSolucion.Delete(SolucionesEnDB[s].Id, Eliminar.NombreUsuario);

                var Causa = _repoCausa.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                if (Causa == null) throw new Exception("No se ha encontrado la Causa que se desea eliminar¿Id Correcto?");
                _repoCausa.Delete(Causa.Id, Eliminar.NombreUsuario);

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }




    public class ValidadorCausaDTO : AbstractValidator<TCausa>
    {
        public static ValidadorCausaDTO Current = new ValidadorCausaDTO();
        public ValidadorCausaDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
            RuleFor(objeto => objeto.Descripcion).MaximumLength(200).WithMessage("Descripcion debe tener 200 caracteres maximo");
        }
    }
}
