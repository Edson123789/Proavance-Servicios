using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Ascenso")]
    public class AscensoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public AscensoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        #region Metodos_Adicionales

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaExperienciaCargoIndustria(int IdAscenso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                AscensoExperienciaCargoIndustriaRepositorio _repoAscExpCarInd = new AscensoExperienciaCargoIndustriaRepositorio(_integraDBContext);
                var lista = _repoAscExpCarInd.ObtenerTodoAscensoExperienciaCargoIndustriasIndustriaPorIdAscenso(IdAscenso);
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaProgramaCapacitacionTema()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ProgramaCapacitacionRepositorio _repoProgramaCapacitacion = new ProgramaCapacitacionRepositorio(_integraDBContext);
                var ProgramaCapacitaciones = _repoProgramaCapacitacion.ObtenerTodoTemaProgramaCapacitacion();
                return Json(new { Result = "OK", Records = ProgramaCapacitaciones });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaProgramaCapacitacionExperiencia()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ProgramaCapacitacionRepositorio _repoProgramaCapacitacion = new ProgramaCapacitacionRepositorio(_integraDBContext);
                var ProgramaCapacitaciones = _repoProgramaCapacitacion.ObtenerTodoExperienciaProgramaCapacitacion();
                return Json(new { Result = "OK", Records = ProgramaCapacitaciones });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaProgramaCapacitacionTemaPorId(int IdAscenso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AscensoProgramaCapacitacionRepositorio _repoAscProgCapacitacion = new AscensoProgramaCapacitacionRepositorio(_integraDBContext);
                var listaResultados = _repoAscProgCapacitacion.ObtenerProgramasCapacitacionPorAscenso(IdAscenso);
                return Json(new { Result = "OK", Records = listaResultados });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaProgramaCapacitacionExperienciaPorId(int IdAscenso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AscensoProgramaCapacitacionExperienciaRepositorio _repoAscProgCapacitacion = new AscensoProgramaCapacitacionExperienciaRepositorio(_integraDBContext);
                var listaResultados = _repoAscProgCapacitacion.ObtenerProgramasCapacitacionExperienciaPorAscenso(IdAscenso);
                return Json(new { Result = "OK", Records = listaResultados });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCertificacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CertificacionRepositorio _repoCertificacion = new CertificacionRepositorio(_integraDBContext);
                var listaCertificacion = _repoCertificacion.ObtenerTodoCertificacionTipoCertificacion();
                return Json(new { Result = "OK", Records = listaCertificacion });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaMembresia()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificacionRepositorio _repoCertificacion = new CertificacionRepositorio(_integraDBContext);
                var listaCertificacion = _repoCertificacion.ObtenerTodoCertificacionTipoMembresia();
                return Json(new { Result = "OK", Records = listaCertificacion });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCertificacionPorIdAscenso(int IdAscenso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AscensoCertificacionRepositorio _repoAscensoCertificacion = new AscensoCertificacionRepositorio(_integraDBContext);
                var listaCertificacion = _repoAscensoCertificacion.ObtenerTodoAscensoCertificacionPorIdAscenso(IdAscenso);
                return Json(new { Result = "OK", Records = listaCertificacion });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaMembresiaPorIdAscenso(int IdAscenso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                AscensoMembresiaRepositorio _repoAscensoMembresia = new AscensoMembresiaRepositorio(_integraDBContext);
                var listaMembresia = _repoAscensoMembresia.ObtenerTodoAscensoMembresiaPorIdAscenso(IdAscenso);
                return Json(new { Result = "OK", Records = listaMembresia });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerAreasFormacionPorIdAscenso(int IdAscenso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AscensoAreaFormacionRepositorio _repoAscensoAreaFormacion = new AscensoAreaFormacionRepositorio(_integraDBContext);
                var listaAreaFormacion = _repoAscensoAreaFormacion.ObtenerTodoAscensoAreaFormacionPorIdAscenso(IdAscenso);
                return Json(new { Result = "OK", Records = listaAreaFormacion });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCargo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CargoRepositorio _repoCargo = new CargoRepositorio(_integraDBContext);
                var Cargos = _repoCargo.ObtenerCargoFiltro();
                return Json(new { Result = "OK", Records = Cargos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaAreaTrabajo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                AreaTrabajoRepositorio _repoAreaTrabajo = new AreaTrabajoRepositorio(_integraDBContext);
                var AreaTrabajos = _repoAreaTrabajo.ObtenerTodoAreaTrabajoFiltro();
                return Json(new { Result = "OK", Records = AreaTrabajos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaAreaFormacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaFormacionRepositorio _repoAreaFormacion = new AreaFormacionRepositorio(_integraDBContext);
                var AreaFormacions = _repoAreaFormacion.ObtenerAreaFormacionFiltro();
                return Json(new { Result = "OK", Records = AreaFormacions });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaIndustria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                IndustriaRepositorio _repoIndustria = new IndustriaRepositorio(_integraDBContext);
                var Industrias = _repoIndustria.ObtenerIndustriaFiltro();
                return Json(new { Result = "OK", Records = Industrias });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaEmpresa()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                EmpresaRepositorio _repoEmpresa = new EmpresaRepositorio(_integraDBContext);
                var listaEmpresa = _repoEmpresa.ObtenerTodoEmpresasFiltro();
                return Json(new { Result = "OK", Records = listaEmpresa });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerEmpresasPorNombre(string NombreParcial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                // evita que se devuelva todas las empresas competidoras (  todas encajan con string vacio ("")  )
                if (NombreParcial == null || NombreParcial.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return Json(new { Result = "OK", Records = ListaVacia });
                }

                EmpresaRepositorio _repoEmpresa = new EmpresaRepositorio(_integraDBContext);
                var listaEmpresa = _repoEmpresa.CargarEmpresasAutoComplete(NombreParcial);
                return Json(new { Result = "OK", Records = listaEmpresa });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoMonedas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                MonedaRepositorio _repoMoneda = new MonedaRepositorio(_integraDBContext);
                var Monedas = _repoMoneda.ObtenerFiltroMonedaSingular();
                return Json(new { Result = "OK", Records = Monedas });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPortalEmpleo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PortalEmpleoRepositorio _repoPortalEmpleo = new PortalEmpleoRepositorio(_integraDBContext);
                var PortalEmpleos = _repoPortalEmpleo.ObtenerTodoPortalEmpleoFiltro();
                return Json(new { Result = "OK", Records = PortalEmpleos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPais()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PaisRepositorio _repoPais = new PaisRepositorio(_integraDBContext);
                var listaPais = _repoPais.ObtenerPaisesCombo();
                return Json(new { Result = "OK", Records = listaPais });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCiudad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CiudadRepositorio _repoCiudad = new CiudadRepositorio(_integraDBContext);
                var listaCiudad = _repoCiudad.ObtenerCiudadesPorPais();
                return Json(new { Result = "OK", Records = listaCiudad });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaRegionCiudad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                RegionCiudadRepositorio _repoRegionCiudad = new RegionCiudadRepositorio(_integraDBContext);
                var listaRegionCiudad = _repoRegionCiudad.ObtenerRegionCiudadFiltro();
                return Json(new { Result = "OK", Records = listaRegionCiudad });
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
        public ActionResult VisualizarAscensos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                AscensoRepositorio _repoAscenso = new AscensoRepositorio(_integraDBContext);
                var Ascensos = _repoAscenso.ObtenerTodoAscensosIndustriaEmpresa();
                return Json(new { Result = "OK", Records = Ascensos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarAscenso([FromBody] AscensoCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AscensoRepositorio _repoAscenso = new AscensoRepositorio(_integraDBContext);
                AscensoAreaFormacionRepositorio _repoAscensoAreaFormacion = new AscensoAreaFormacionRepositorio(_integraDBContext);
                AscensoProgramaCapacitacionRepositorio _repoAscensoProgramaCapacitacion = new AscensoProgramaCapacitacionRepositorio(_integraDBContext);
                AscensoProgramaCapacitacionExperienciaRepositorio _repoAscensoProgramaCapacitacionExperiencia = new AscensoProgramaCapacitacionExperienciaRepositorio(_integraDBContext);
                AscensoCertificacionRepositorio _repoAscensoCertificacion = new AscensoCertificacionRepositorio(_integraDBContext);
                AscensoMembresiaRepositorio _repoAscensoMembresia = new AscensoMembresiaRepositorio(_integraDBContext);
                AscensoExperienciaCargoIndustriaRepositorio _repoAscensoExperienciaCargoIndustria = new AscensoExperienciaCargoIndustriaRepositorio(_integraDBContext);



                AscensoBO AscensoNuevo = new AscensoBO();

                AscensoNuevo.CargoMercado = ObjetoDTO.CargoMercado;
                AscensoNuevo.Activo = ObjetoDTO.Activo;
                AscensoNuevo.FechaPublicacion = ObjetoDTO.FechaPublicacion;
                AscensoNuevo.SueldoMin = ObjetoDTO.SueldoMin;
                AscensoNuevo.IdMoneda = ObjetoDTO.IdMoneda;
                AscensoNuevo.IdAreaTrabajo = ObjetoDTO.IdAreaTrabajo;
                AscensoNuevo.IdPortalEmpleo = ObjetoDTO.IdPortalEmpleo;
                AscensoNuevo.IdCargo = ObjetoDTO.IdCargo;
                AscensoNuevo.IdEmpresa = ObjetoDTO.IdEmpresa;
                AscensoNuevo.IdPais = ObjetoDTO.IdPais;
                AscensoNuevo.IdRegionCiudad = ObjetoDTO.IdRegionCiudad;
                AscensoNuevo.IdCiudad = ObjetoDTO.IdCiudad;
                AscensoNuevo.UrlOferta = ObjetoDTO.UrlOferta;

                AscensoNuevo.Estado = true;
                AscensoNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                AscensoNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                AscensoNuevo.FechaCreacion = DateTime.Now;
                AscensoNuevo.FechaModificacion = DateTime.Now;

                _repoAscenso.Insert(AscensoNuevo);



                //insercion de AreaFormacion
                if (ObjetoDTO.AreaFormaciones != null)
                {
                    var listaAFormacion = ObjetoDTO.AreaFormaciones;
                    for (int i=0; i < listaAFormacion.Count; ++i)
                    {
                        AscensoAreaFormacionBO AscensoAreaFormacion = new AscensoAreaFormacionBO();
                        AscensoAreaFormacion.IdAscenso = AscensoNuevo.Id;
                        AscensoAreaFormacion.IdAreaFormacion = listaAFormacion[i];

                        AscensoAreaFormacion.Estado = true;
                        AscensoAreaFormacion.UsuarioCreacion = ObjetoDTO.Usuario;
                        AscensoAreaFormacion.UsuarioModificacion = ObjetoDTO.Usuario;
                        AscensoAreaFormacion.FechaCreacion = DateTime.Now;
                        AscensoAreaFormacion.FechaModificacion = DateTime.Now;

                        _repoAscensoAreaFormacion.Insert(AscensoAreaFormacion);
                    }
                }

                //insercion de Certificaciones
                if (ObjetoDTO.Certificaciones != null)
                {
                    var lista = ObjetoDTO.Certificaciones;
                    for (int i = 0; i < lista.Count; ++i)
                    {
                        AscensoCertificacionBO AscensoCertificacion = new AscensoCertificacionBO();
                        AscensoCertificacion.IdAscenso = AscensoNuevo.Id;
                        AscensoCertificacion.IdCertificacion = lista[i];

                        AscensoCertificacion.Estado = true;
                        AscensoCertificacion.UsuarioCreacion = ObjetoDTO.Usuario;
                        AscensoCertificacion.UsuarioModificacion = ObjetoDTO.Usuario;
                        AscensoCertificacion.FechaCreacion = DateTime.Now;
                        AscensoCertificacion.FechaModificacion = DateTime.Now;

                        _repoAscensoCertificacion.Insert(AscensoCertificacion);
                    }
                }

                //insercion de membresias
                if (ObjetoDTO.Membresias != null)
                {
                    var lista = ObjetoDTO.Membresias;
                    for (int i = 0; i < lista.Count; ++i)
                    {
                        AscensoMembresiaBO AscensoMembresia = new AscensoMembresiaBO();
                        AscensoMembresia.IdAscenso = AscensoNuevo.Id;
                        AscensoMembresia.IdCertificacion = lista[i];

                        AscensoMembresia.Estado = true;
                        AscensoMembresia.UsuarioCreacion = ObjetoDTO.Usuario;
                        AscensoMembresia.UsuarioModificacion = ObjetoDTO.Usuario;
                        AscensoMembresia.FechaCreacion = DateTime.Now;
                        AscensoMembresia.FechaModificacion = DateTime.Now;

                        _repoAscensoMembresia.Insert(AscensoMembresia);
                    }
                }


                //insercion de CapacitacionTema
                if (ObjetoDTO.ProgramasCapacitacion != null)
                {
                    var lista = ObjetoDTO.ProgramasCapacitacion;
                    for (int i = 0; i < lista.Count; ++i)
                    {
                        AscensoProgramaCapacitacionBO AscensoProgramaCapacitacion = new AscensoProgramaCapacitacionBO();
                        AscensoProgramaCapacitacion.IdAscenso = AscensoNuevo.Id;
                        AscensoProgramaCapacitacion.IdProgramaCapacitacion = lista[i].IdProgramaCapacitacion;
                        AscensoProgramaCapacitacion.Contenido = lista[i].Contenido;

                        AscensoProgramaCapacitacion.Estado = true;
                        AscensoProgramaCapacitacion.UsuarioCreacion = ObjetoDTO.Usuario;
                        AscensoProgramaCapacitacion.UsuarioModificacion = ObjetoDTO.Usuario;
                        AscensoProgramaCapacitacion.FechaCreacion = DateTime.Now;
                        AscensoProgramaCapacitacion.FechaModificacion = DateTime.Now;

                        _repoAscensoProgramaCapacitacion.Insert(AscensoProgramaCapacitacion);
                    }
                }

                //insercion de CapacitacionExperiencia
                if (ObjetoDTO.ProgramasCapacitacionExperiencia != null)
                {
                    var lista = ObjetoDTO.ProgramasCapacitacionExperiencia;
                    for (int i = 0; i < lista.Count; ++i)
                    {
                        AscensoProgramaCapacitacionExperienciaBO AscensoProgramaCapacitacionExperiencia = new AscensoProgramaCapacitacionExperienciaBO();
                        AscensoProgramaCapacitacionExperiencia.IdAscenso = AscensoNuevo.Id;
                        AscensoProgramaCapacitacionExperiencia.IdProgramaCapacitacion = lista[i].IdProgramaCapacitacion;
                        AscensoProgramaCapacitacionExperiencia.Contenido = lista[i].Contenido;

                        AscensoProgramaCapacitacionExperiencia.Estado = true;
                        AscensoProgramaCapacitacionExperiencia.UsuarioCreacion = ObjetoDTO.Usuario;
                        AscensoProgramaCapacitacionExperiencia.UsuarioModificacion = ObjetoDTO.Usuario;
                        AscensoProgramaCapacitacionExperiencia.FechaCreacion = DateTime.Now;
                        AscensoProgramaCapacitacionExperiencia.FechaModificacion = DateTime.Now;

                        _repoAscensoProgramaCapacitacionExperiencia.Insert(AscensoProgramaCapacitacionExperiencia);
                    }
                }

                //insercion de Experiencia
                if (ObjetoDTO.Experiencias != null)
                {
                    var lista = ObjetoDTO.Experiencias;
                    for (int i = 0; i < lista.Count; ++i)
                    {
                        AscensoExperienciaCargoIndustriaBO AscensoExperienciaCargoIndustria = new AscensoExperienciaCargoIndustriaBO();
                        AscensoExperienciaCargoIndustria.IdAscenso = AscensoNuevo.Id;
                        AscensoExperienciaCargoIndustria.AniosExperiencia = lista[i].AniosExperiencia;
                        AscensoExperienciaCargoIndustria.IdCargo = lista[i].IdCargo;
                        AscensoExperienciaCargoIndustria.IdAreaTrabajo = lista[i].IdAreaTrabajo;
                        AscensoExperienciaCargoIndustria.IdIndustria = lista[i].IdIndustria;
                        AscensoExperienciaCargoIndustria.DescripcionPuestoAnterior = lista[i].DescripcionPuestoAnterior;

                        AscensoExperienciaCargoIndustria.Estado = true;
                        AscensoExperienciaCargoIndustria.UsuarioCreacion = ObjetoDTO.Usuario;
                        AscensoExperienciaCargoIndustria.UsuarioModificacion = ObjetoDTO.Usuario;
                        AscensoExperienciaCargoIndustria.FechaCreacion = DateTime.Now;
                        AscensoExperienciaCargoIndustria.FechaModificacion = DateTime.Now;

                        _repoAscensoExperienciaCargoIndustria.Insert(AscensoExperienciaCargoIndustria);
                    }
                }


                var AscensoCreado = _repoAscenso.ObtenerAscensosIndustriaEmpresaPorId(AscensoNuevo.Id);
                if (AscensoCreado.Count > 1 || AscensoCreado.Count < 1)
                    throw new Exception("Multiples Registros con el mismo Id o Ninguno Encontrado");


                return Ok(AscensoCreado[0]);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarAscenso([FromBody] AscensoCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AscensoRepositorio _repoAscenso = new AscensoRepositorio(_integraDBContext);
                AscensoAreaFormacionRepositorio _repoAscensoAreaFormacion = new AscensoAreaFormacionRepositorio(_integraDBContext);
                AscensoProgramaCapacitacionRepositorio _repoAscensoProgramaCapacitacion = new AscensoProgramaCapacitacionRepositorio(_integraDBContext);
                AscensoProgramaCapacitacionExperienciaRepositorio _repoAscensoProgramaCapacitacionExperiencia = new AscensoProgramaCapacitacionExperienciaRepositorio(_integraDBContext);
                AscensoCertificacionRepositorio _repoAscensoCertificacion = new AscensoCertificacionRepositorio(_integraDBContext);
                AscensoMembresiaRepositorio _repoAscensoMembresia = new AscensoMembresiaRepositorio(_integraDBContext);
                AscensoExperienciaCargoIndustriaRepositorio _repoAscensoExperienciaCargoIndustria = new AscensoExperienciaCargoIndustriaRepositorio(_integraDBContext);


                AscensoBO Ascenso = _repoAscenso.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                if (Ascenso == null) throw new Exception("No se encuentra el registro que se desea actualizar ¿Id correcto?");

                Ascenso.CargoMercado = ObjetoDTO.CargoMercado;
                Ascenso.Activo = ObjetoDTO.Activo;
                Ascenso.FechaPublicacion = ObjetoDTO.FechaPublicacion;
                Ascenso.SueldoMin = ObjetoDTO.SueldoMin;
                Ascenso.IdMoneda = ObjetoDTO.IdMoneda;
                Ascenso.IdAreaTrabajo = ObjetoDTO.IdAreaTrabajo;
                Ascenso.IdPortalEmpleo = ObjetoDTO.IdPortalEmpleo;
                Ascenso.IdCargo = ObjetoDTO.IdCargo;
                Ascenso.IdEmpresa = ObjetoDTO.IdEmpresa;
                Ascenso.IdPais = ObjetoDTO.IdPais;
                Ascenso.IdRegionCiudad = ObjetoDTO.IdRegionCiudad;
                Ascenso.IdCiudad = ObjetoDTO.IdCiudad;
                Ascenso.UrlOferta = ObjetoDTO.UrlOferta;

                Ascenso.Estado = true;
                Ascenso.UsuarioModificacion = ObjetoDTO.Usuario;
                Ascenso.FechaModificacion = DateTime.Now;

                _repoAscenso.Update(Ascenso);


                var RegistrosAreaEnDB = _repoAscensoAreaFormacion.GetBy(x=>x.IdAscenso == ObjetoDTO.Id).ToList();
                for (int i = 0; i < RegistrosAreaEnDB.Count; ++i)
                    _repoAscensoAreaFormacion.Delete(RegistrosAreaEnDB[i].Id, ObjetoDTO.Usuario);

                //insercion de AreaFormacion
                if (ObjetoDTO.AreaFormaciones != null)
                {
                    var listaAFormacion = ObjetoDTO.AreaFormaciones;
                    for (int i = 0; i < listaAFormacion.Count; ++i)
                    {
                        AscensoAreaFormacionBO AscensoAreaFormacion = new AscensoAreaFormacionBO();
                        AscensoAreaFormacion.IdAscenso = Ascenso.Id;
                        AscensoAreaFormacion.IdAreaFormacion = listaAFormacion[i];

                        AscensoAreaFormacion.Estado = true;
                        AscensoAreaFormacion.UsuarioCreacion = ObjetoDTO.Usuario;
                        AscensoAreaFormacion.UsuarioModificacion = ObjetoDTO.Usuario;
                        AscensoAreaFormacion.FechaCreacion = DateTime.Now;
                        AscensoAreaFormacion.FechaModificacion = DateTime.Now;

                        _repoAscensoAreaFormacion.Insert(AscensoAreaFormacion);
                    }
                }



                var RegistrosCertEnDB = _repoAscensoCertificacion.GetBy(x => x.IdAscenso == ObjetoDTO.Id).ToList();
                for (int i = 0; i < RegistrosCertEnDB.Count; ++i)
                    _repoAscensoCertificacion.Delete(RegistrosCertEnDB[i].Id, ObjetoDTO.Usuario);

                //insercion de Certificaciones
                if (ObjetoDTO.Certificaciones != null)
                {
                    var lista = ObjetoDTO.Certificaciones;
                    for (int i = 0; i < lista.Count; ++i)
                    {
                        AscensoCertificacionBO AscensoCertificacion = new AscensoCertificacionBO();
                        AscensoCertificacion.IdAscenso = Ascenso.Id;
                        AscensoCertificacion.IdCertificacion = lista[i];

                        AscensoCertificacion.Estado = true;
                        AscensoCertificacion.UsuarioCreacion = ObjetoDTO.Usuario;
                        AscensoCertificacion.UsuarioModificacion = ObjetoDTO.Usuario;
                        AscensoCertificacion.FechaCreacion = DateTime.Now;
                        AscensoCertificacion.FechaModificacion = DateTime.Now;

                        _repoAscensoCertificacion.Insert(AscensoCertificacion);
                    }
                }



                var RegistrosMemtEnDB = _repoAscensoMembresia.GetBy(x => x.IdAscenso == ObjetoDTO.Id).ToList();
                for (int i = 0; i < RegistrosMemtEnDB.Count; ++i)
                    _repoAscensoMembresia.Delete(RegistrosMemtEnDB[i].Id, ObjetoDTO.Usuario);
                //insercion de membresias
                if (ObjetoDTO.Membresias != null)
                {
                    var lista = ObjetoDTO.Membresias;
                    for (int i = 0; i < lista.Count; ++i)
                    {
                        AscensoMembresiaBO AscensoMembresia = new AscensoMembresiaBO();
                        AscensoMembresia.IdAscenso = Ascenso.Id;
                        AscensoMembresia.IdCertificacion = lista[i];

                        AscensoMembresia.Estado = true;
                        AscensoMembresia.UsuarioCreacion = ObjetoDTO.Usuario;
                        AscensoMembresia.UsuarioModificacion = ObjetoDTO.Usuario;
                        AscensoMembresia.FechaCreacion = DateTime.Now;
                        AscensoMembresia.FechaModificacion = DateTime.Now;

                        _repoAscensoMembresia.Insert(AscensoMembresia);
                    }
                }




                var RegistrosCapaEnDB = _repoAscensoProgramaCapacitacion.GetBy(x => x.IdAscenso == ObjetoDTO.Id).ToList();
                for (int i = 0; i < RegistrosCapaEnDB.Count; ++i)
                    _repoAscensoProgramaCapacitacion.Delete(RegistrosCapaEnDB[i].Id, ObjetoDTO.Usuario);
                //insercion de CapacitacionTema
                if (ObjetoDTO.ProgramasCapacitacion != null)
                {
                    var lista = ObjetoDTO.ProgramasCapacitacion;
                    for (int i = 0; i < lista.Count; ++i)
                    {
                        AscensoProgramaCapacitacionBO AscensoProgramaCapacitacion = new AscensoProgramaCapacitacionBO();
                        AscensoProgramaCapacitacion.IdAscenso = Ascenso.Id;
                        AscensoProgramaCapacitacion.IdProgramaCapacitacion = lista[i].IdProgramaCapacitacion;
                        AscensoProgramaCapacitacion.Contenido = lista[i].Contenido;

                        AscensoProgramaCapacitacion.Estado = true;
                        AscensoProgramaCapacitacion.UsuarioCreacion = ObjetoDTO.Usuario;
                        AscensoProgramaCapacitacion.UsuarioModificacion = ObjetoDTO.Usuario;
                        AscensoProgramaCapacitacion.FechaCreacion = DateTime.Now;
                        AscensoProgramaCapacitacion.FechaModificacion = DateTime.Now;

                        _repoAscensoProgramaCapacitacion.Insert(AscensoProgramaCapacitacion);
                    }
                }



                var RegistrosCExpEnDB = _repoAscensoProgramaCapacitacionExperiencia.GetBy(x => x.IdAscenso == ObjetoDTO.Id).ToList();
                for (int i = 0; i < RegistrosCExpEnDB.Count; ++i)
                    _repoAscensoProgramaCapacitacionExperiencia.Delete(RegistrosCExpEnDB[i].Id, ObjetoDTO.Usuario);
                //insercion de CapacitacionExperiencia
                if (ObjetoDTO.ProgramasCapacitacionExperiencia != null)
                {
                    var lista = ObjetoDTO.ProgramasCapacitacionExperiencia;
                    for (int i = 0; i < lista.Count; ++i)
                    {
                        AscensoProgramaCapacitacionExperienciaBO AscensoProgramaCapacitacionExperiencia = new AscensoProgramaCapacitacionExperienciaBO();
                        AscensoProgramaCapacitacionExperiencia.IdAscenso = Ascenso.Id;
                        AscensoProgramaCapacitacionExperiencia.IdProgramaCapacitacion = lista[i].IdProgramaCapacitacion;
                        AscensoProgramaCapacitacionExperiencia.Contenido = lista[i].Contenido;

                        AscensoProgramaCapacitacionExperiencia.Estado = true;
                        AscensoProgramaCapacitacionExperiencia.UsuarioCreacion = ObjetoDTO.Usuario;
                        AscensoProgramaCapacitacionExperiencia.UsuarioModificacion = ObjetoDTO.Usuario;
                        AscensoProgramaCapacitacionExperiencia.FechaCreacion = DateTime.Now;
                        AscensoProgramaCapacitacionExperiencia.FechaModificacion = DateTime.Now;

                        _repoAscensoProgramaCapacitacionExperiencia.Insert(AscensoProgramaCapacitacionExperiencia);
                    }
                }



                var RegistrosExpEnDB = _repoAscensoExperienciaCargoIndustria.GetBy(x => x.IdAscenso == ObjetoDTO.Id).ToList();
                for (int i = 0; i < RegistrosExpEnDB.Count; ++i)
                    _repoAscensoExperienciaCargoIndustria.Delete(RegistrosExpEnDB[i].Id, ObjetoDTO.Usuario);
                //insercion de Experiencia
                if (ObjetoDTO.Experiencias != null)
                {
                    var lista = ObjetoDTO.Experiencias;
                    for (int i = 0; i < lista.Count; ++i)
                    {
                        AscensoExperienciaCargoIndustriaBO AscensoExperienciaCargoIndustria = new AscensoExperienciaCargoIndustriaBO();
                        AscensoExperienciaCargoIndustria.IdAscenso = Ascenso.Id;
                        AscensoExperienciaCargoIndustria.AniosExperiencia = lista[i].AniosExperiencia;
                        AscensoExperienciaCargoIndustria.IdCargo = lista[i].IdCargo;
                        AscensoExperienciaCargoIndustria.IdAreaTrabajo = lista[i].IdAreaTrabajo;
                        AscensoExperienciaCargoIndustria.IdIndustria = lista[i].IdIndustria;
                        AscensoExperienciaCargoIndustria.DescripcionPuestoAnterior = lista[i].DescripcionPuestoAnterior;

                        AscensoExperienciaCargoIndustria.Estado = true;
                        AscensoExperienciaCargoIndustria.UsuarioCreacion = ObjetoDTO.Usuario;
                        AscensoExperienciaCargoIndustria.UsuarioModificacion = ObjetoDTO.Usuario;
                        AscensoExperienciaCargoIndustria.FechaCreacion = DateTime.Now;
                        AscensoExperienciaCargoIndustria.FechaModificacion = DateTime.Now;

                        _repoAscensoExperienciaCargoIndustria.Insert(AscensoExperienciaCargoIndustria);
                    }
                }


                var AscensoModificado = _repoAscenso.ObtenerAscensosIndustriaEmpresaPorId(Ascenso.Id);
                if (AscensoModificado.Count > 1 || AscensoModificado.Count < 1)
                    throw new Exception("Multiples Registros con el mismo Id o Ninguno Encontrado");


                return Ok(AscensoModificado[0]);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]/{UserName}/{Id}")]
        [HttpDelete]
        public ActionResult EliminarAscenso(int Id, string UserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AscensoRepositorio _repoAscenso = new AscensoRepositorio(_integraDBContext);
                AscensoAreaFormacionRepositorio _repoAscensoAreaFormacion = new AscensoAreaFormacionRepositorio(_integraDBContext);
                AscensoProgramaCapacitacionRepositorio _repoAscensoProgramaCapacitacion = new AscensoProgramaCapacitacionRepositorio(_integraDBContext);
                AscensoProgramaCapacitacionExperienciaRepositorio _repoAscensoProgramaCapacitacionExperiencia = new AscensoProgramaCapacitacionExperienciaRepositorio(_integraDBContext);
                AscensoCertificacionRepositorio _repoAscensoCertificacion = new AscensoCertificacionRepositorio(_integraDBContext);
                AscensoMembresiaRepositorio _repoAscensoMembresia = new AscensoMembresiaRepositorio(_integraDBContext);
                AscensoExperienciaCargoIndustriaRepositorio _repoAscensoExperienciaCargoIndustria = new AscensoExperienciaCargoIndustriaRepositorio(_integraDBContext);



                var RegistrosAreaEnDB = _repoAscensoAreaFormacion.GetBy(x => x.IdAscenso == Id).ToList();
                for (int i = 0; i < RegistrosAreaEnDB.Count; ++i)
                    _repoAscensoAreaFormacion.Delete(RegistrosAreaEnDB[i].Id, UserName);

                var RegistrosCertEnDB = _repoAscensoCertificacion.GetBy(x => x.IdAscenso == Id).ToList();
                for (int i = 0; i < RegistrosCertEnDB.Count; ++i)
                    _repoAscensoCertificacion.Delete(RegistrosCertEnDB[i].Id, UserName);

                var RegistrosMemtEnDB = _repoAscensoMembresia.GetBy(x => x.IdAscenso == Id).ToList();
                for (int i = 0; i < RegistrosMemtEnDB.Count; ++i)
                    _repoAscensoMembresia.Delete(RegistrosMemtEnDB[i].Id, UserName);

                var RegistrosCapaEnDB = _repoAscensoProgramaCapacitacion.GetBy(x => x.IdAscenso == Id).ToList();
                for (int i = 0; i < RegistrosCapaEnDB.Count; ++i)
                    _repoAscensoProgramaCapacitacion.Delete(RegistrosCapaEnDB[i].Id, UserName);
                
                var RegistrosCExpEnDB = _repoAscensoProgramaCapacitacionExperiencia.GetBy(x => x.IdAscenso == Id).ToList();
                for (int i = 0; i < RegistrosCExpEnDB.Count; ++i)
                    _repoAscensoProgramaCapacitacionExperiencia.Delete(RegistrosCExpEnDB[i].Id, UserName);

                var RegistrosExpEnDB = _repoAscensoExperienciaCargoIndustria.GetBy(x => x.IdAscenso == Id).ToList();
                for (int i = 0; i < RegistrosExpEnDB.Count; ++i)
                    _repoAscensoExperienciaCargoIndustria.Delete(RegistrosExpEnDB[i].Id, UserName);
                


                _repoAscenso.Delete(Id, UserName);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion 
    }

}
