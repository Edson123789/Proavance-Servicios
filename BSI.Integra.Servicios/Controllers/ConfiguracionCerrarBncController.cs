using System;
using System.Transactions;
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

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ConfiguracionCerrarBncController
    /// Autor: Jose Villena.
    /// Fecha: 16/02/2021
    /// <summary>
    /// Configuracion Cerrar BNC a RN5
    /// </summary>
    [Route("api/ConfiguracionCerrarBnc")]
    public class ConfiguracionCerrarBncController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ConfiguracionCerrarBncController()
        {
            _integraDBContext = new integraDBContext();
        }


        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtine Tipos de Datos Asociado a la configuracion
        /// </summary>
        /// <returns>Objeto<returns>
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

                ConfiguracionRutinaBncObsoletoTipoDatoRepositorio repoConfiguracionRutinaBncObsoletoTipoDato = new ConfiguracionRutinaBncObsoletoTipoDatoRepositorio();
                var lista = repoConfiguracionRutinaBncObsoletoTipoDato.ObtenerTipoDatoPorIdConfiguracionBnc(IdConfiguracion);
                return Ok(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de Categorías Origenes
        /// </summary>
        /// <returns>Objeto<returns>
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

                ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio repoConfiguracionRutinaBncObsoletoCategoriaOrigen = new ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio();
                var lista = repoConfiguracionRutinaBncObsoletoCategoriaOrigen.ObtenerCategoriaOrigenPorIdConfiguracionBnc(IdConfiguracion);
                return Ok(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de Fase de Oportunidades
        /// </summary>
        /// <returns>Objeto<returns>
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

                FaseOportunidadRepositorio repoFaseOportunidad = new FaseOportunidadRepositorio();
                var lista = repoFaseOportunidad.ObtenerTodoFiltro();
                return Ok(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de Ocurrencias
        /// </summary>
        /// <returns>Objeto<returns>
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

                OcurrenciaRepositorio repoOcurrencia = new OcurrenciaRepositorio();
                var lista = repoOcurrencia.ObtenerOcurrenciaFiltro();
                return Ok(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de tipos de datos
        /// </summary>
        /// <returns>Objeto<returns>
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

                TipoDatoRepositorio repoTipoDato = new TipoDatoRepositorio();
                var lista = repoTipoDato.ObtenerFiltro();
                return Ok(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de Personal Activo
        /// </summary>
        /// <returns>Objeto<returns>
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

                PersonalRepositorio repoPersonal = new PersonalRepositorio();
                var lista = repoPersonal.GetTodoPersonalActivoParaFiltro();
                for (int i = 0; i < lista.Count; ++i)
                    lista[i].Nombres = lista[i].Apellidos + ", " + lista[i].Nombres;

                return Ok(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de Plantillas
        /// </summary>
        /// <returns>Objeto<returns>
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

                PlantillaRepositorio repoPlantilla = new PlantillaRepositorio();
                var lista = repoPlantilla.ObtenerAllPlantillaEmailAlterado();
                return Ok(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de categoria origen
        /// </summary>
        /// <returns><returns>
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

                CategoriaOrigenRepositorio repoCategoriaOrigen = new CategoriaOrigenRepositorio();
                var lista = repoCategoriaOrigen.ObtenerCategoriaFiltro();
                return Ok(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene toda las configuraciones del modulo
        /// </summary>
        /// <returns><returns>
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
                ConfiguracionRutinaBncObsoletoRepositorio repoConfiguracionRutinaBncObsoleto = new ConfiguracionRutinaBncObsoletoRepositorio();
                var configuracionRutinaBncObsoletos = repoConfiguracionRutinaBncObsoleto.ObtenerTodoConfiguracion();
                return Ok(new { Result = "OK", Records = configuracionRutinaBncObsoletos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta una Configuracion en el modulo
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarConfiguracionRutinaBncObsoleto([FromBody] ConfiguracionRutinaBncObsoletoCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionRutinaBncObsoletoRepositorio repoConfiguracionRutinaBncObsoleto = new ConfiguracionRutinaBncObsoletoRepositorio(_integraDBContext);
                ConfiguracionRutinaBncObsoletoBO configuracionRutinaBncObsoleto = new ConfiguracionRutinaBncObsoletoBO();

                ConfiguracionRutinaBncObsoletoTipoDatoRepositorio repoConfiguracionRutinaBncObsoletoTipoDato = new ConfiguracionRutinaBncObsoletoTipoDatoRepositorio(_integraDBContext);
                ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio repoConfiguracionRutinaBncObsoletoCategoriaOrigen = new ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {

                    configuracionRutinaBncObsoleto.Nombre = ObjetoDTO.Nombre;
                    configuracionRutinaBncObsoleto.NumDiasProbabilidadMedia = ObjetoDTO.NumDiasProbabilidadMedia;
                    configuracionRutinaBncObsoleto.NumDiasProbabilidadAlta = ObjetoDTO.NumDiasProbabilidadAlta;
                    configuracionRutinaBncObsoleto.NumDiasProbabilidadMuyAlta = ObjetoDTO.NumDiasProbabilidadMuyAlta;
                    configuracionRutinaBncObsoleto.EjecutarRutinaProbabilidadMedia = ObjetoDTO.EjecutarRutinaProbabilidadMedia;
                    configuracionRutinaBncObsoleto.EjecutarRutinaProbabilidadAlta = ObjetoDTO.EjecutarRutinaProbabilidadAlta;
                    configuracionRutinaBncObsoleto.EjecutarRutinaProbabilidadMuyAlta = ObjetoDTO.EjecutarRutinaProbabilidadMuyAlta;
                    configuracionRutinaBncObsoleto.IdOcurrenciaDestino = ObjetoDTO.IdOcurrenciaDestino;
                    configuracionRutinaBncObsoleto.EjecutarRutinaEnviarCorreo = ObjetoDTO.EjecutarRutinaEnviarCorreo;
                    configuracionRutinaBncObsoleto.IdPlantillaCorreo = ObjetoDTO.IdPlantillaCorreo;
                    configuracionRutinaBncObsoleto.IdPersonalCorreoNoExistente = ObjetoDTO.IdPersonalCorreoNoExistente;

                    configuracionRutinaBncObsoleto.Estado = true;
                    configuracionRutinaBncObsoleto.UsuarioCreacion = ObjetoDTO.Usuario;
                    configuracionRutinaBncObsoleto.FechaCreacion = DateTime.Now;
                    configuracionRutinaBncObsoleto.UsuarioModificacion = ObjetoDTO.Usuario;
                    configuracionRutinaBncObsoleto.FechaModificacion = DateTime.Now;

                    repoConfiguracionRutinaBncObsoleto.Insert(configuracionRutinaBncObsoleto);
                    ObjetoDTO.Id = configuracionRutinaBncObsoleto.Id;
                    foreach (var item in ObjetoDTO.CategoriaOrigenes)
                    {
                        ConfiguracionRutinaBncObsoletoCategoriaOrigenBO configuracionRutinaBncObsoletoCategoriaOrigen = new ConfiguracionRutinaBncObsoletoCategoriaOrigenBO();
                        configuracionRutinaBncObsoletoCategoriaOrigen.IdConfiguracionRutinaBncObsoleto = ObjetoDTO.Id;
                        configuracionRutinaBncObsoletoCategoriaOrigen.IdCategoriaOrigen = item;

                        configuracionRutinaBncObsoletoCategoriaOrigen.Estado = true;
                        configuracionRutinaBncObsoletoCategoriaOrigen.UsuarioCreacion = ObjetoDTO.Usuario;
                        configuracionRutinaBncObsoletoCategoriaOrigen.FechaCreacion = DateTime.Now;
                        configuracionRutinaBncObsoletoCategoriaOrigen.UsuarioModificacion = ObjetoDTO.Usuario;
                        configuracionRutinaBncObsoletoCategoriaOrigen.FechaModificacion = DateTime.Now;
                        repoConfiguracionRutinaBncObsoletoCategoriaOrigen.Insert(configuracionRutinaBncObsoletoCategoriaOrigen);
                    }
                    foreach (var item in ObjetoDTO.TipoDatos)
                    {
                        ConfiguracionRutinaBncObsoletoTipoDatoBO configuracionRutinaBncObsoletoTipoDato = new ConfiguracionRutinaBncObsoletoTipoDatoBO();
                        configuracionRutinaBncObsoletoTipoDato.IdConfiguracionRutinaBncObsoleto = ObjetoDTO.Id;
                        configuracionRutinaBncObsoletoTipoDato.IdTipoDato = item;

                        configuracionRutinaBncObsoletoTipoDato.Estado = true;
                        configuracionRutinaBncObsoletoTipoDato.UsuarioCreacion = ObjetoDTO.Usuario;
                        configuracionRutinaBncObsoletoTipoDato.FechaCreacion = DateTime.Now;
                        configuracionRutinaBncObsoletoTipoDato.UsuarioModificacion = ObjetoDTO.Usuario;
                        configuracionRutinaBncObsoletoTipoDato.FechaModificacion = DateTime.Now;
                        repoConfiguracionRutinaBncObsoletoTipoDato.Insert(configuracionRutinaBncObsoletoTipoDato);
                    }
                    scope.Complete();
                }
                return Ok(ObjetoDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }



        /// TipoFuncion: POST
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una Configuracion en el modulo
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarConfiguracionRutinaBncObsoleto([FromBody] ConfiguracionRutinaBncObsoletoCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionRutinaBncObsoletoRepositorio repoConfiguracionRutinaBncObsoleto = new ConfiguracionRutinaBncObsoletoRepositorio(_integraDBContext);
                ConfiguracionRutinaBncObsoletoTipoDatoRepositorio repoConfiguracionRutinaBncObsoletoTipoDato = new ConfiguracionRutinaBncObsoletoTipoDatoRepositorio(_integraDBContext);
                ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio repoConfiguracionRutinaBncObsoletoCategoriaOrigen = new ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio(_integraDBContext);

                ConfiguracionRutinaBncObsoletoBO configuracionRutinaBncObsoleto = repoConfiguracionRutinaBncObsoleto.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                configuracionRutinaBncObsoleto.Nombre = ObjetoDTO.Nombre;
                configuracionRutinaBncObsoleto.NumDiasProbabilidadMedia = ObjetoDTO.NumDiasProbabilidadMedia;
                configuracionRutinaBncObsoleto.NumDiasProbabilidadAlta = ObjetoDTO.NumDiasProbabilidadAlta;
                configuracionRutinaBncObsoleto.NumDiasProbabilidadMuyAlta = ObjetoDTO.NumDiasProbabilidadMuyAlta;
                configuracionRutinaBncObsoleto.EjecutarRutinaProbabilidadMedia = ObjetoDTO.EjecutarRutinaProbabilidadMedia;
                configuracionRutinaBncObsoleto.EjecutarRutinaProbabilidadAlta = ObjetoDTO.EjecutarRutinaProbabilidadAlta;
                configuracionRutinaBncObsoleto.EjecutarRutinaProbabilidadMuyAlta = ObjetoDTO.EjecutarRutinaProbabilidadMuyAlta;
                configuracionRutinaBncObsoleto.IdPlantillaCorreo = ObjetoDTO.IdPlantillaCorreo;
                configuracionRutinaBncObsoleto.IdPersonalCorreoNoExistente = ObjetoDTO.IdPersonalCorreoNoExistente;
                configuracionRutinaBncObsoleto.IdOcurrenciaDestino = ObjetoDTO.IdOcurrenciaDestino;
                configuracionRutinaBncObsoleto.EjecutarRutinaEnviarCorreo = ObjetoDTO.EjecutarRutinaEnviarCorreo;

                configuracionRutinaBncObsoleto.Estado = true;
                configuracionRutinaBncObsoleto.UsuarioModificacion = ObjetoDTO.Usuario;
                configuracionRutinaBncObsoleto.FechaModificacion = DateTime.Now;

                repoConfiguracionRutinaBncObsoleto.Update(configuracionRutinaBncObsoleto);

                // reinsersion de TipoDatosAsociados
                var registrosTipoDatoEnDB = repoConfiguracionRutinaBncObsoletoTipoDato.GetBy(x => x.IdConfiguracionRutinaBncObsoleto == ObjetoDTO.Id).ToList();
                for (int i = 0; i < registrosTipoDatoEnDB.Count; ++i)
                    repoConfiguracionRutinaBncObsoletoTipoDato.Delete(registrosTipoDatoEnDB[i].Id, ObjetoDTO.Usuario);
                if (ObjetoDTO.TipoDatos != null)
                {
                    var TipoDatos = ObjetoDTO.TipoDatos;
                    for (int j = 0; j < TipoDatos.Count; ++j)
                    {
                        ConfiguracionRutinaBncObsoletoTipoDatoBO tipoDato = new ConfiguracionRutinaBncObsoletoTipoDatoBO();
                        tipoDato.IdConfiguracionRutinaBncObsoleto = ObjetoDTO.Id;
                        tipoDato.IdTipoDato = TipoDatos[j];
                        tipoDato.Estado = true;
                        tipoDato.UsuarioCreacion = ObjetoDTO.Usuario;
                        tipoDato.UsuarioModificacion = ObjetoDTO.Usuario;
                        tipoDato.FechaCreacion = DateTime.Now;
                        tipoDato.FechaModificacion = DateTime.Now;

                        repoConfiguracionRutinaBncObsoletoTipoDato.Insert(tipoDato);
                    }
                }

                // reinsersion de CategoriaOrigenesAsociados
                var registrosCategoriaOrigenEnDB = repoConfiguracionRutinaBncObsoletoCategoriaOrigen.GetBy(x => x.IdConfiguracionRutinaBncObsoleto == ObjetoDTO.Id).ToList();
                for (int i = 0; i < registrosCategoriaOrigenEnDB.Count; ++i)
                    repoConfiguracionRutinaBncObsoletoCategoriaOrigen.Delete(registrosCategoriaOrigenEnDB[i].Id, ObjetoDTO.Usuario);

                if (ObjetoDTO.CategoriaOrigenes != null)
                {
                    var categoriaOrigenes = ObjetoDTO.CategoriaOrigenes;
                    for (int j = 0; j < categoriaOrigenes.Count; ++j)
                    {
                        ConfiguracionRutinaBncObsoletoCategoriaOrigenBO categoriaOrigen = new ConfiguracionRutinaBncObsoletoCategoriaOrigenBO();
                        categoriaOrigen.IdConfiguracionRutinaBncObsoleto = ObjetoDTO.Id;
                        categoriaOrigen.IdCategoriaOrigen = categoriaOrigenes[j];
                        categoriaOrigen.Estado = true;
                        categoriaOrigen.UsuarioCreacion = ObjetoDTO.Usuario;
                        categoriaOrigen.UsuarioModificacion = ObjetoDTO.Usuario;
                        categoriaOrigen.FechaCreacion = DateTime.Now;
                        categoriaOrigen.FechaModificacion = DateTime.Now;

                        repoConfiguracionRutinaBncObsoletoCategoriaOrigen.Insert(categoriaOrigen);
                    }
                }

                return Ok(configuracionRutinaBncObsoleto);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }



        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina una Configuracion en el modulo
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarConfiguracionRutinaBncObsoleto([FromBody] EliminarDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {               
                ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio repoConfiguracionRutinaBncObsoletoCategoriaOrigen = new ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio(_integraDBContext);
                ConfiguracionRutinaBncObsoletoTipoDatoRepositorio repoConfiguracionRutinaBncObsoletoTipoDato = new ConfiguracionRutinaBncObsoletoTipoDatoRepositorio(_integraDBContext);
                ConfiguracionRutinaBncObsoletoRepositorio repoConfiguracionRutinaBncObsoleto = new ConfiguracionRutinaBncObsoletoRepositorio(_integraDBContext);

                
                var listaCategoriaOrigen = repoConfiguracionRutinaBncObsoletoCategoriaOrigen.ObtenerCategoriaOrigenPorIdConfiguracionBnc(ObjetoDTO.Id);
                var listaTipoDato = repoConfiguracionRutinaBncObsoletoTipoDato.ObtenerTipoDatoPorIdConfiguracionBnc(ObjetoDTO.Id);

                using (TransactionScope scope = new TransactionScope())
                {

                    foreach (var item in listaCategoriaOrigen)
                    {
                        repoConfiguracionRutinaBncObsoletoCategoriaOrigen.Delete(item.Id, ObjetoDTO.NombreUsuario);
                    }
                    foreach (var item in listaTipoDato)
                    {
                        repoConfiguracionRutinaBncObsoletoTipoDato.Delete(item.Id, ObjetoDTO.NombreUsuario);
                    }
                    if (repoConfiguracionRutinaBncObsoleto.Exist(ObjetoDTO.Id))
                        repoConfiguracionRutinaBncObsoleto.Delete(ObjetoDTO.Id, ObjetoDTO.NombreUsuario);


                    scope.Complete();

                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




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
