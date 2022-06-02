using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using static BSI.Integra.Servicios.Controllers.ConfiguracionRutinaBncObsoletoController;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ConfiguracionRutinaBncObsoleto")]
    public class ConfiguracionRutinaBncObsoletoController : BaseController<TConfiguracionRutinaBncObsoleto, ValidadorConfiguracionRutinaBncObsoletoDTO>
    {
        public ConfiguracionRutinaBncObsoletoController(IIntegraRepository<TConfiguracionRutinaBncObsoleto> repositorio, ILogger<BaseController<TConfiguracionRutinaBncObsoleto, ValidadorConfiguracionRutinaBncObsoletoDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }


        #region Metodos-Adicionales

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaConfiguracionRutinaBncObsoletoTipoDato(int IdConfiguracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ConfiguracionRutinaBncObsoletoTipoDatoRepositorio _repoConfiguracionRutinaBncObsoletoTipoDato = new ConfiguracionRutinaBncObsoletoTipoDatoRepositorio();
                var Lista = _repoConfiguracionRutinaBncObsoletoTipoDato.ObtenerTipoDatoPorIdConfiguracionBnc(IdConfiguracion);
                return Json(new { Result = "OK", Records = Lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaConfiguracionRutinaBncObsoletoCategoriaOrigen(int IdConfiguracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio _repoConfiguracionRutinaBncObsoletoCategoriaOrigen = new ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio();
                var Lista = _repoConfiguracionRutinaBncObsoletoCategoriaOrigen.ObtenerCategoriaOrigenPorIdConfiguracionBnc(IdConfiguracion);
                return Json(new { Result = "OK", Records = Lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaFaseOportunidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                FaseOportunidadRepositorio _repoFaseOportunidad = new FaseOportunidadRepositorio();
                var Lista = _repoFaseOportunidad.ObtenerTodoFiltro();
                return Json(new { Result = "OK", Records = Lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaOcurrencia()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                OcurrenciaRepositorio _repoOcurrencia = new OcurrenciaRepositorio();
                var Lista = _repoOcurrencia.ObtenerOcurrenciaFiltro();
                return Json(new { Result = "OK", Records = Lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaTipoDato()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                TipoDatoRepositorio _repoTipoDato = new TipoDatoRepositorio();
                var Lista = _repoTipoDato.ObtenerFiltro();
                return Json(new { Result = "OK", Records = Lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPersonal()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PersonalRepositorio _repoPersonal = new PersonalRepositorio();
                var Lista = _repoPersonal.GetTodoPersonalActivoParaFiltro();
                for (int i = 0; i < Lista.Count; ++i)
                    Lista[i].Nombres = Lista[i].Apellidos + ", " + Lista[i].Nombres;

                return Json(new { Result = "OK", Records = Lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPlantilla()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PlantillaRepositorio _repoPlantilla = new PlantillaRepositorio();
                var Lista = _repoPlantilla.ObtenerAllPlantillaEmailAlterado();
                return Json(new { Result = "OK", Records = Lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCategoriaOrigen()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CategoriaOrigenRepositorio _repoCategoriaOrigen = new CategoriaOrigenRepositorio();
                var Lista = _repoCategoriaOrigen.ObtenerCategoriaFiltro();
                return Json(new { Result = "OK", Records = Lista });
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
        public ActionResult VizualizarConfiguracionRutinaBncObsoletos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionRutinaBncObsoletoRepositorio _repoConfiguracionRutinaBncObsoleto = new ConfiguracionRutinaBncObsoletoRepositorio();
                var ConfiguracionRutinaBncObsoletos = _repoConfiguracionRutinaBncObsoleto.ObtenerTodoConfiguracion();
                return Json(new { Result = "OK", Records = ConfiguracionRutinaBncObsoletos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }



        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarConfiguracionRutinaBncObsoleto([FromBody] ConfiguracionRutinaBncObsoletoCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionRutinaBncObsoletoRepositorio _repoConfiguracionRutinaBncObsoleto = new ConfiguracionRutinaBncObsoletoRepositorio();
                ConfiguracionRutinaBncObsoletoTipoDatoRepositorio _repoConfiguracionRutinaBncObsoletoTipoDato = new ConfiguracionRutinaBncObsoletoTipoDatoRepositorio();
                ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio _repoConfiguracionRutinaBncObsoletoCategoriaOrigen = new ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio();

                ConfiguracionRutinaBncObsoletoBO ConfiguracionRutinaBncObsoleto = _repoConfiguracionRutinaBncObsoleto.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                ConfiguracionRutinaBncObsoleto.NumDiasProbabilidadMedia = ObjetoDTO.NumDiasProbabilidadMedia;
                ConfiguracionRutinaBncObsoleto.NumDiasProbabilidadAlta = ObjetoDTO.NumDiasProbabilidadAlta;
                ConfiguracionRutinaBncObsoleto.NumDiasProbabilidadMuyAlta = ObjetoDTO.NumDiasProbabilidadMuyAlta;
                ConfiguracionRutinaBncObsoleto.EjecutarRutinaProbabilidadMedia = ObjetoDTO.EjecutarRutinaProbabilidadMedia;
                ConfiguracionRutinaBncObsoleto.EjecutarRutinaProbabilidadAlta = ObjetoDTO.EjecutarRutinaProbabilidadAlta;
                ConfiguracionRutinaBncObsoleto.EjecutarRutinaProbabilidadMuyAlta = ObjetoDTO.EjecutarRutinaProbabilidadMuyAlta;
                ConfiguracionRutinaBncObsoleto.IdPlantillaCorreo = ObjetoDTO.IdPlantillaCorreo;
                ConfiguracionRutinaBncObsoleto.IdPersonalCorreoNoExistente = ObjetoDTO.IdPersonalCorreoNoExistente;
                ConfiguracionRutinaBncObsoleto.IdOcurrenciaDestino = ObjetoDTO.IdOcurrenciaDestino;
                ConfiguracionRutinaBncObsoleto.EjecutarRutinaEnviarCorreo = ObjetoDTO.EjecutarRutinaEnviarCorreo;
                
                ConfiguracionRutinaBncObsoleto.Estado = true;
                ConfiguracionRutinaBncObsoleto.UsuarioModificacion = ObjetoDTO.Usuario;
                ConfiguracionRutinaBncObsoleto.FechaModificacion = DateTime.Now;

                _repoConfiguracionRutinaBncObsoleto.Update(ConfiguracionRutinaBncObsoleto);

                // reinsersion de TipoDatosAsociados
                var RegistrosTipoDatoEnDB = _repoConfiguracionRutinaBncObsoletoTipoDato.GetBy(x => x.IdConfiguracionRutinaBncObsoleto == ObjetoDTO.Id).ToList();
                for (int i = 0; i < RegistrosTipoDatoEnDB.Count; ++i)
                    _repoConfiguracionRutinaBncObsoletoTipoDato.Delete(RegistrosTipoDatoEnDB[i].Id, ObjetoDTO.Usuario);
                if (ObjetoDTO.TipoDatos != null)
                {
                    var TipoDatos = ObjetoDTO.TipoDatos;
                    for (int j=0; j<TipoDatos.Count; ++j)
                    {
                        ConfiguracionRutinaBncObsoletoTipoDatoBO TipoDato = new ConfiguracionRutinaBncObsoletoTipoDatoBO();
                        TipoDato.IdConfiguracionRutinaBncObsoleto = ObjetoDTO.Id;
                        TipoDato.IdTipoDato = TipoDatos[j];
                        TipoDato.Estado = true;
                        TipoDato.UsuarioCreacion = ObjetoDTO.Usuario;
                        TipoDato.UsuarioModificacion = ObjetoDTO.Usuario;
                        TipoDato.FechaCreacion = DateTime.Now;
                        TipoDato.FechaModificacion = DateTime.Now;

                        _repoConfiguracionRutinaBncObsoletoTipoDato.Insert(TipoDato);
                    }
                }

                // reinsersion de CategoriaOrigenesAsociados
                var RegistrosCategoriaOrigenEnDB = _repoConfiguracionRutinaBncObsoletoCategoriaOrigen.GetBy(x => x.IdConfiguracionRutinaBncObsoleto == ObjetoDTO.Id).ToList();
                for (int i = 0; i < RegistrosCategoriaOrigenEnDB.Count; ++i)
                    _repoConfiguracionRutinaBncObsoletoCategoriaOrigen.Delete(RegistrosCategoriaOrigenEnDB[i].Id, ObjetoDTO.Usuario);

                if (ObjetoDTO.CategoriaOrigenes != null)
                {
                    var CategoriaOrigenes = ObjetoDTO.CategoriaOrigenes;
                    for (int j = 0; j < CategoriaOrigenes.Count; ++j)
                    {
                        ConfiguracionRutinaBncObsoletoCategoriaOrigenBO CategoriaOrigen = new ConfiguracionRutinaBncObsoletoCategoriaOrigenBO();
                        CategoriaOrigen.IdConfiguracionRutinaBncObsoleto = ObjetoDTO.Id;
                        CategoriaOrigen.IdCategoriaOrigen = CategoriaOrigenes[j];
                        CategoriaOrigen.Estado = true;
                        CategoriaOrigen.UsuarioCreacion = ObjetoDTO.Usuario;
                        CategoriaOrigen.UsuarioModificacion = ObjetoDTO.Usuario;
                        CategoriaOrigen.FechaCreacion = DateTime.Now;
                        CategoriaOrigen.FechaModificacion = DateTime.Now;

                        _repoConfiguracionRutinaBncObsoletoCategoriaOrigen.Insert(CategoriaOrigen);
                    }
                }

                return Ok(ConfiguracionRutinaBncObsoleto);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        #endregion



        public class ValidadorConfiguracionRutinaBncObsoletoDTO : AbstractValidator<TConfiguracionRutinaBncObsoleto>
        {
            public static ValidadorConfiguracionRutinaBncObsoletoDTO Current = new ValidadorConfiguracionRutinaBncObsoletoDTO();
            public ValidadorConfiguracionRutinaBncObsoletoDTO()
            {
                RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                                .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 50 maximo");
            }
        }
    }
}
