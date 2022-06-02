using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api/Problema")]
    public class ProblemaController : BaseController<TProblema, ValidadorProblemaDTO>
    {
        public ProblemaController(IIntegraRepository<TProblema> repositorio, ILogger<BaseController<TProblema, ValidadorProblemaDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }



        #region Servicios Adicionales
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerIndicadoresPorProblema(int IdProblema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndicadorProblemaRepositorio _repoIndicadorProblema = new IndicadorProblemaRepositorio();
                IndicadorFrecuenciaRepositorio _repoIndicadorFrecuencia = new IndicadorFrecuenciaRepositorio();
                IndicadorProblemaTipoPanelRepositorio _repoIndiProbTipoPanel = new IndicadorProblemaTipoPanelRepositorio();


                List<IndicadorProblemaCompuestoDTO> Indicadores = new List<IndicadorProblemaCompuestoDTO>();
                var IndicadoresProblemas = _repoIndicadorProblema.ObtenerTodoIndicadorProblemaPorIdProblema(IdProblema);
                IndicadorProblemaCompuestoDTO IndicadorCompuesto;
                for (int i = 0; i < IndicadoresProblemas.Count; ++i)
                {
                    IndicadorCompuesto = new IndicadorProblemaCompuestoDTO();

                    IndicadorCompuesto.Id = IndicadoresProblemas[i].Id;
                    IndicadorCompuesto.IdProblema = IndicadoresProblemas[i].IdProblema;
                    IndicadorCompuesto.IdIndicador = IndicadoresProblemas[i].IdIndicador;
                    IndicadorCompuesto.Valor = IndicadoresProblemas[i].Valor;
                    IndicadorCompuesto.IdOperadorComparacion = IndicadoresProblemas[i].IdOperadorComparacion;
                    IndicadorCompuesto.MuestraMinima = IndicadoresProblemas[i].MuestraMinima;

                    IndicadorCompuesto.Frecuencias = _repoIndicadorFrecuencia.ObtenerFrecuenciasPorIdIndicadorProblema(IndicadoresProblemas[i].Id);
                    IndicadorCompuesto.TipoPaneles = _repoIndiProbTipoPanel.ObtenerTodoIndicadorPorProblema(IndicadoresProblemas[i].Id);

                    Indicadores.Add(IndicadorCompuesto);
                }

                return Json(new { Result = "OK", Records = Indicadores, });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerHorasPorIdProblema(int IdProblema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProblemaHorarioRepositorio _repProblemaHorario = new ProblemaHorarioRepositorio();
                var Horarios = _repProblemaHorario.ObtenerTodoHorarioPorIdProblema(IdProblema);
                return Json(new { Result = "OK", Records = Horarios });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodosIndicadores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndicadorRepositorio _repIndicadorProblema = new IndicadorRepositorio();
                var Indicadores = _repIndicadorProblema.ObtenerTodoIndicador();
                return Json(new { Result = "OK", Records = Indicadores });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodosPaneles()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                HojaOportunidadTipoPanelRepositorio _repHojOpoTipoPanel = new HojaOportunidadTipoPanelRepositorio();
                var paneles = _repHojOpoTipoPanel.ObtenerTodoFiltro();
                return Json(new { Result = "OK", Records = paneles });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodosOperadorComparacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OperadorComparacionRepositorio _repOperComparacion = new OperadorComparacionRepositorio();
                var operadores = _repOperComparacion.ObtenerListaOperadorComparacion();
                return Json(new { Result = "OK", Records = operadores });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodasHoras()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                HoraRepositorio _repHora = new HoraRepositorio();
                var horas = _repHora.ObtenerListaHora();
                return Json(new { Result = "OK", Records = horas });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFrecuenciasPorIdIndicadorProblema(int IdIndicador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndicadorFrecuenciaRepositorio _repHora = new IndicadorFrecuenciaRepositorio();
                var horas = _repHora.ObtenerFrecuenciasPorIdIndicadorProblema(IdIndicador);
                return Json(new { Result = "OK", Records = horas });

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
        public ActionResult vizualizarProblemas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProblemaRepositorio _repoProblema = new ProblemaRepositorio();
                var Problemas = _repoProblema.ObtenerTodoProblemas();

                return Json(new { Result = "OK", Records = Problemas });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarProblema([FromBody] ProblemaCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                IndicadorProblemaRepositorio _repoIndicadorProblema = new IndicadorProblemaRepositorio();
                IndicadorFrecuenciaRepositorio _repoIndicadorFrecuencia = new IndicadorFrecuenciaRepositorio();
                ProblemaHorarioRepositorio _repoProblemaHorario = new ProblemaHorarioRepositorio();
                IndicadorProblemaTipoPanelRepositorio _repoIndiProbTipoPanel = new IndicadorProblemaTipoPanelRepositorio();
                ProblemaRepositorio _repProblema = new ProblemaRepositorio();

                // INSERCION PROBLEMA
                ProblemaBO NuevoProblema = new ProblemaBO();
                NuevoProblema.Nombre = ObjetoDTO.Nombre;
                NuevoProblema.Descripcion = ObjetoDTO.Descripcion;

                NuevoProblema.Estado = true;
                NuevoProblema.UsuarioCreacion = ObjetoDTO.Usuario;
                NuevoProblema.UsuarioModificacion = ObjetoDTO.Usuario;
                NuevoProblema.FechaCreacion = DateTime.Now;
                NuevoProblema.FechaModificacion = DateTime.Now;

                _repProblema.Insert(NuevoProblema);
                // FIN INSERCION PROBLEMA

                ObjetoDTO.Id = NuevoProblema.Id; // estableciendo el ID despues de la insercion

                // INSERCION HORAS PROBLEMA
                if (ObjetoDTO.ProblemaHorarios != null)
                {
                    var Horas = ObjetoDTO.ProblemaHorarios;
                    for (int h = 0; h < Horas.Count; ++h)
                    {
                        ProblemaHorarioBO NuevoProblemaHorario = new ProblemaHorarioBO();
                        NuevoProblemaHorario.IdProblema = ObjetoDTO.Id;
                        NuevoProblemaHorario.IdHora = Horas[h].IdHora;

                        NuevoProblemaHorario.Estado = true;
                        NuevoProblemaHorario.UsuarioCreacion = ObjetoDTO.Usuario;
                        NuevoProblemaHorario.UsuarioModificacion = ObjetoDTO.Usuario;
                        NuevoProblemaHorario.FechaCreacion = DateTime.Now;
                        NuevoProblemaHorario.FechaModificacion = DateTime.Now;

                        _repoProblemaHorario.Insert(NuevoProblemaHorario);
                    }
                }
                // FIN INSERCION HORAS PROBLEMA



                //INSERCION INDICADORES PROBLEMA
                if (ObjetoDTO.Indicadores != null)
                {
                    var Indicadores = ObjetoDTO.Indicadores;
                    for (int i = 0; i < Indicadores.Count; ++i)
                    {
                        IndicadorProblemaBO NuevoIndicadorProblema = new IndicadorProblemaBO();
                        NuevoIndicadorProblema.IdProblema = ObjetoDTO.Id;
                        NuevoIndicadorProblema.IdIndicador = Indicadores[i].IdIndicador;
                        NuevoIndicadorProblema.IdOperadorComparacion = Indicadores[i].IdOperadorComparacion;
                        NuevoIndicadorProblema.Valor = Indicadores[i].Valor;
                        NuevoIndicadorProblema.MuestraMinima = Indicadores[i].MuestraMinima;

                        NuevoIndicadorProblema.Estado = true;
                        NuevoIndicadorProblema.UsuarioCreacion = ObjetoDTO.Usuario;
                        NuevoIndicadorProblema.UsuarioModificacion = ObjetoDTO.Usuario;
                        NuevoIndicadorProblema.FechaCreacion = DateTime.Now;
                        NuevoIndicadorProblema.FechaModificacion = DateTime.Now;

                        _repoIndicadorProblema.Insert(NuevoIndicadorProblema);

                        ObjetoDTO.Indicadores[i].IdIndicador = NuevoIndicadorProblema.Id; // estableciendo el Id correcto despues de la insersion

                        // insercion de IndicadorFrecuencias
                        if (Indicadores[i].Frecuencias != null)
                        {
                            var IndicadorFrecuencias = Indicadores[i].Frecuencias;
                            for (int f = 0; f < IndicadorFrecuencias.Count; ++f)
                            {
                                IndicadorFrecuenciaBO NuevoIndicadorFrecuencia = new IndicadorFrecuenciaBO();
                                NuevoIndicadorFrecuencia.IdIndicadorProblema = ObjetoDTO.Indicadores[i].IdIndicador;
                                NuevoIndicadorFrecuencia.IdHora = IndicadorFrecuencias[f].IdHora;

                                NuevoIndicadorFrecuencia.Estado = true;
                                NuevoIndicadorFrecuencia.UsuarioCreacion = ObjetoDTO.Usuario;
                                NuevoIndicadorFrecuencia.UsuarioModificacion = ObjetoDTO.Usuario;
                                NuevoIndicadorFrecuencia.FechaCreacion = DateTime.Now;
                                NuevoIndicadorFrecuencia.FechaModificacion = DateTime.Now;

                                _repoIndicadorFrecuencia.Insert(NuevoIndicadorFrecuencia);
                            }
                        }


                        // insercion de IndicadorTipoPanel
                        if (Indicadores[i].TipoPaneles != null)
                        {
                            var TipoPaneles = Indicadores[i].TipoPaneles;
                            for (int p = 0; p < TipoPaneles.Count; ++p)
                            {
                                IndicadorProblemaTipoPanelBO NuevoIndProTipoPanel = new IndicadorProblemaTipoPanelBO();
                                NuevoIndProTipoPanel.IdIndicadorProblema = ObjetoDTO.Indicadores[i].IdIndicador;
                                NuevoIndProTipoPanel.IdHojaOportunidadTipoPanel = TipoPaneles[p].IdHojaOportunidadTipoPanel;

                                NuevoIndProTipoPanel.Estado = true;
                                NuevoIndProTipoPanel.UsuarioCreacion = ObjetoDTO.Usuario;
                                NuevoIndProTipoPanel.UsuarioModificacion = ObjetoDTO.Usuario;
                                NuevoIndProTipoPanel.FechaCreacion = DateTime.Now;
                                NuevoIndProTipoPanel.FechaModificacion = DateTime.Now;

                                _repoIndiProbTipoPanel.Insert(NuevoIndProTipoPanel);
                            }
                        }

                    }
                }
                //FIN INSERCION INDICADORES PROBLEMA

                return Ok(NuevoProblema);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarProblema([FromBody] ProblemaCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndicadorProblemaRepositorio _repoIndicadorProblema = new IndicadorProblemaRepositorio();
                IndicadorFrecuenciaRepositorio _repoIndicadorFrecuencia = new IndicadorFrecuenciaRepositorio();
                ProblemaHorarioRepositorio _repoProblemaHorario = new ProblemaHorarioRepositorio();
                IndicadorProblemaTipoPanelRepositorio _repoIndiProbTipoPanel = new IndicadorProblemaTipoPanelRepositorio();
                ProblemaRepositorio _repProblema = new ProblemaRepositorio();

                // ACTUALIZACION PROBLEMA
                ProblemaBO Problema = _repProblema.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();
                if (Problema == null)
                    throw new Exception("No se encontro el problema que se desea actualizar ¿Id correcto?");

                Problema.Nombre = ObjetoDTO.Nombre;
                Problema.Descripcion = ObjetoDTO.Descripcion;
                Problema.UsuarioModificacion = ObjetoDTO.Usuario;
                Problema.FechaModificacion = DateTime.Now;
                Problema.Estado = true;

                _repProblema.Update(Problema);
                // FIN ACTUALIZACION PROBLEMA


                // ACTUALIZACION HORAS PROBLEMA
                var HorariosEnDB = _repoProblemaHorario.GetBy(x => x.IdProblema == ObjetoDTO.Id).ToList();

                for (int k = 0; k < HorariosEnDB.Count; ++k)
                    _repoProblemaHorario.Delete(HorariosEnDB[k].Id, ObjetoDTO.Usuario);

                if (ObjetoDTO.ProblemaHorarios != null)
                {
                    var Horas = ObjetoDTO.ProblemaHorarios;
                    for (int h = 0; h < Horas.Count; ++h)
                    {
                        ProblemaHorarioBO NuevoProblemaHorario = new ProblemaHorarioBO();
                        NuevoProblemaHorario.IdProblema = ObjetoDTO.Id;
                        NuevoProblemaHorario.IdHora = Horas[h].IdHora;

                        NuevoProblemaHorario.Estado = true;
                        NuevoProblemaHorario.UsuarioCreacion = ObjetoDTO.Usuario;
                        NuevoProblemaHorario.UsuarioModificacion = ObjetoDTO.Usuario;
                        NuevoProblemaHorario.FechaCreacion = DateTime.Now;
                        NuevoProblemaHorario.FechaModificacion = DateTime.Now;

                        _repoProblemaHorario.Insert(NuevoProblemaHorario);
                    }
                }
                // FIN ACTUALIZACION HORAS PROBLEMA



                //ACTUALIZACION INDICADORES PROBLEMA
                var IndicadoresEnDB = _repoIndicadorProblema.GetBy(x => x.IdProblema == ObjetoDTO.Id).ToList();
                for (int i=0; i<IndicadoresEnDB.Count; ++i)
                {
                    var IndiProbTipoPanel = _repoIndiProbTipoPanel.GetBy(x => x.IdIndicadorProblema == IndicadoresEnDB[i].Id).ToList();
                    var IndiFrec = _repoIndicadorFrecuencia.GetBy(x => x.IdIndicadorProblema == IndicadoresEnDB[i].Id).ToList();

                    for (int k = 0; k < IndiProbTipoPanel.Count; ++k)
                        _repoIndiProbTipoPanel.Delete(IndiProbTipoPanel[k].Id, ObjetoDTO.Usuario);

                    for (int j = 0; j < IndiFrec.Count; ++j)
                        _repoIndicadorFrecuencia.Delete(IndiFrec[j].Id, ObjetoDTO.Usuario);

                    _repoIndicadorProblema.Delete(IndicadoresEnDB[i].Id, ObjetoDTO.Usuario);
                }

                if (ObjetoDTO.Indicadores != null)
                {
                    var Indicadores = ObjetoDTO.Indicadores;
                    for (int i = 0; i < Indicadores.Count; ++i)
                    {
                        IndicadorProblemaBO IndicadorProblema = new IndicadorProblemaBO();
                        IndicadorProblema.IdProblema = ObjetoDTO.Id;
                        IndicadorProblema.IdIndicador = Indicadores[i].IdIndicador;
                        IndicadorProblema.IdOperadorComparacion = Indicadores[i].IdOperadorComparacion;
                        IndicadorProblema.Valor = Indicadores[i].Valor;
                        IndicadorProblema.MuestraMinima = Indicadores[i].MuestraMinima;

                        IndicadorProblema.Estado = true;
                        IndicadorProblema.UsuarioCreacion = ObjetoDTO.Usuario;
                        IndicadorProblema.UsuarioModificacion = ObjetoDTO.Usuario;
                        IndicadorProblema.FechaCreacion = DateTime.Now;
                        IndicadorProblema.FechaModificacion = DateTime.Now;

                        _repoIndicadorProblema.Insert(IndicadorProblema);

                        ObjetoDTO.Indicadores[i].IdIndicador = IndicadorProblema.Id; // estableciendo el Id correcto despues de la insersion

                        // ACTUALIZACION de IndicadorFrecuencias
                        if (Indicadores[i].Frecuencias != null)
                        {
                            var IndicadorFrecuencias = Indicadores[i].Frecuencias;
                            for (int f = 0; f < IndicadorFrecuencias.Count; ++f)
                            {
                                IndicadorFrecuenciaBO IndicadorFrecuencia = new IndicadorFrecuenciaBO();
                                IndicadorFrecuencia.IdIndicadorProblema = ObjetoDTO.Indicadores[i].IdIndicador;
                                IndicadorFrecuencia.IdHora = IndicadorFrecuencias[f].IdHora;

                                IndicadorFrecuencia.Estado = true;
                                IndicadorFrecuencia.UsuarioCreacion = ObjetoDTO.Usuario;
                                IndicadorFrecuencia.UsuarioModificacion = ObjetoDTO.Usuario;
                                IndicadorFrecuencia.FechaCreacion = DateTime.Now;
                                IndicadorFrecuencia.FechaModificacion = DateTime.Now;

                                _repoIndicadorFrecuencia.Insert(IndicadorFrecuencia);
                            }
                        }


                        // ACTUALIZACION de IndicadorTipoPanel
                        if (Indicadores[i].TipoPaneles != null)
                        {
                            var TipoPaneles = Indicadores[i].TipoPaneles;
                            for (int p = 0; p < TipoPaneles.Count; ++p)
                            {
                                IndicadorProblemaTipoPanelBO IndProTipoPanel = new IndicadorProblemaTipoPanelBO();
                                IndProTipoPanel.IdIndicadorProblema = ObjetoDTO.Indicadores[i].IdIndicador;
                                IndProTipoPanel.IdHojaOportunidadTipoPanel = TipoPaneles[p].IdHojaOportunidadTipoPanel;

                                IndProTipoPanel.Estado = true;
                                IndProTipoPanel.UsuarioCreacion = ObjetoDTO.Usuario;
                                IndProTipoPanel.UsuarioModificacion = ObjetoDTO.Usuario;
                                IndProTipoPanel.FechaCreacion = DateTime.Now;
                                IndProTipoPanel.FechaModificacion = DateTime.Now;

                                _repoIndiProbTipoPanel.Insert(IndProTipoPanel);
                            }
                        }

                    }
                }
                //FIN ACTUALIZACION INDICADORES PROBLEMA

                return Ok(Problema);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarProblema([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndicadorProblemaRepositorio _repoIndicadorProblema = new IndicadorProblemaRepositorio();
                IndicadorFrecuenciaRepositorio _repoIndicadorFrecuencia = new IndicadorFrecuenciaRepositorio();
                ProblemaHorarioRepositorio _repoProblemaHorario = new ProblemaHorarioRepositorio();
                IndicadorProblemaTipoPanelRepositorio _repoIndiProbTipoPanel = new IndicadorProblemaTipoPanelRepositorio();
                ProblemaRepositorio _repProblema = new ProblemaRepositorio();

                var Problema = _repProblema.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                if (Problema == null) throw new Exception("No se ha encontrado el problema que se desea eliminar¿Id Correcto?");

                // ELIMINACION de HorariosProblemas
                var ProblemaHorarios = _repoProblemaHorario.GetBy(x => x.IdProblema == Eliminar.Id).ToList();
                for (int h = 0; h < ProblemaHorarios.Count; ++h)
                    _repoProblemaHorario.Delete(ProblemaHorarios[h].Id, Eliminar.NombreUsuario);

                // ELIMINACION de Indicadores y sus Frecuencias y TipoPaneles
                var IndicadoresProblema = _repoIndicadorProblema.GetBy(x => x.IdProblema == Eliminar.Id).ToList();
                for (int i = 0; i < IndicadoresProblema.Count; ++i)
                {
                    var IndicadoresFrecuencia = _repoIndicadorFrecuencia.GetBy(x => x.IdIndicadorProblema == IndicadoresProblema[i].Id).ToList();
                    for (int f = 0; f < IndicadoresFrecuencia.Count; ++f)
                        _repoIndicadorFrecuencia.Delete(IndicadoresFrecuencia[f].Id, Eliminar.NombreUsuario);

                    var IndProTipoPaneles = _repoIndiProbTipoPanel.GetBy(x => x.IdIndicadorProblema == IndicadoresProblema[i].Id).ToList();
                    for (int t = 0; t < IndProTipoPaneles.Count; ++t)
                        _repoIndiProbTipoPanel.Delete(IndProTipoPaneles[t].Id, Eliminar.NombreUsuario);

                    //_repoIndicadorProblema.Delete(IndicadoresProblema[i].Id, UserName);
                }

                for (int i = 0; i < IndicadoresProblema.Count; ++i)
                    _repoIndicadorProblema.Delete(IndicadoresProblema[i].Id, Eliminar.NombreUsuario);

                // ELIMINACION de problema
                _repProblema.Delete(Problema.Id, Eliminar.NombreUsuario);
                
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }




    public class ValidadorProblemaDTO : AbstractValidator<TProblema>
    {
        public static ValidadorProblemaDTO Current = new ValidadorProblemaDTO();
        public ValidadorProblemaDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
            RuleFor(objeto => objeto.Descripcion).MaximumLength(200).WithMessage("Descripcion debe tener 200 caracteres maximo");
        }
    }
}
