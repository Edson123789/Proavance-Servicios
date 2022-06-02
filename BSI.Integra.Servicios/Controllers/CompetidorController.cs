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
    [Route("api/Competidor")]
    public class CompetidorController : BaseController<TCompetidor, ValidadorCompetidorDTO>
    {
        public CompetidorController(IIntegraRepository<TCompetidor> repositorio, ILogger<BaseController<TCompetidor, ValidadorCompetidorDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        

        #region MetodosAdicionales

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerDatosComplementariosCompetidor(int IdCompetidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CompetidorProgramaRelacionadoRepositorio _repoCompetidorProgramaRelacionado = new CompetidorProgramaRelacionadoRepositorio();
                CompetidorCertificacionRepositorio _repoCompetidorCertificacion = new CompetidorCertificacionRepositorio();
                CompetidorMembresiaRepositorio _repoCompetidorMembresia = new CompetidorMembresiaRepositorio();
                CompetidorCapacitacionRepositorio _repoCompetidorCapacitacion = new CompetidorCapacitacionRepositorio();
                CompetidorTipoModalidadRepositorio _repoCompetidorTipoModalidad = new CompetidorTipoModalidadRepositorio();


                var listaPGenerales = _repoCompetidorProgramaRelacionado.ObtenerTodoCompetidorProgramaRelacionadoPorIdCompetidor(IdCompetidor);
                var listaTipoModalidades = _repoCompetidorTipoModalidad.ObtenerTodoCompetidorTipoModalidadPorIdCompetidor(IdCompetidor);
                var listaMembresias = _repoCompetidorMembresia.ObtenerTodoCompetidorMembresiaPorIdCompetidor(IdCompetidor);
                var listaCertificaciones = _repoCompetidorCertificacion.ObtenerTodoCompetidorCertificacionPorIdCompetidor(IdCompetidor);
                var listaCapacitaciones = _repoCompetidorCapacitacion.ObtenerTodoCompetidorCapacitacionPorIdCompetidor(IdCompetidor); 

                return Json(new { Result = "OK", PGenerales = listaPGenerales, TipoModalidades = listaTipoModalidades, Membresias = listaMembresias, Certificaciones = listaCertificaciones, Capacitaciones = listaCapacitaciones });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerVentajasPorCompetidor(int IdCompetidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CompetidorVentajaDesventajaRepositorio _repoCompetidorVD = new CompetidorVentajaDesventajaRepositorio();
                var lista = _repoCompetidorVD.ObtenerCompetidorVentajas(IdCompetidor);
               
                return Json(new { Result = "OK", Records=lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerDesventajasPorCompetidor(int IdCompetidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CompetidorVentajaDesventajaRepositorio _repoCompetidorVD = new CompetidorVentajaDesventajaRepositorio();
                var lista = _repoCompetidorVD.ObtenerCompetidorDesventajas(IdCompetidor);

                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaProgramaCapacitacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ProgramaCapacitacionRepositorio _repoProgramaCapacitacion = new ProgramaCapacitacionRepositorio();
                var listaProgramaCapacitacion = _repoProgramaCapacitacion.ObtenerTodoProgramaCapacitacion();
                
                
                return Json(new { Result = "OK", Records = listaProgramaCapacitacion });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaMoneda()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                MonedaRepositorio _repoMoneda = new MonedaRepositorio();
                var listaMoneda = _repoMoneda.ObtenerFiltroMoneda();
                return Json(new { Result = "OK", Records = listaMoneda });
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

                EmpresaRepositorio _repoEmpresa = new EmpresaRepositorio();
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
        public ActionResult ObtenerListaEmpresaPorIndicioNombre(string NombreParcial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // evita que se devuelva todas las empresas competidoras (  todas encajan con string vacio ("")  )
                if (NombreParcial == null || NombreParcial.Trim().Equals("")) {
                    Object[] ListaVacia = new object[0]; 
                    return Json(new { Result = "OK", Records = ListaVacia});
                }

                EmpresaRepositorio _repoEmpresa = new EmpresaRepositorio();
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
        public ActionResult ObtenerListaPais()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PaisRepositorio _repoPais = new PaisRepositorio();
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

                CiudadRepositorio _repoCiudad = new CiudadRepositorio();
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

                RegionCiudadRepositorio _repoRegionCiudad = new RegionCiudadRepositorio();
                var listaRegionCiudad = _repoRegionCiudad.ObtenerRegionCiudadFiltro();
                return Json(new { Result = "OK", Records = listaRegionCiudad });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaAreaCapacitacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                AreaCapacitacionRepositorio _repoAreaCapacitacion = new AreaCapacitacionRepositorio();
                var listaAreaCapacitacion = _repoAreaCapacitacion.ObtenerTodoFiltro();
                return Json(new { Result = "OK", Records = listaAreaCapacitacion });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaSubAreaCapacitacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                SubAreaCapacitacionRepositorio _repoSubAreaCapacitacion = new SubAreaCapacitacionRepositorio();
                var listaSubAreaCapacitacion = _repoSubAreaCapacitacion.ObtenerTodoFiltro();
                return Json(new { Result = "OK", Records = listaSubAreaCapacitacion });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCategoriaPrograma()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CategoriaProgramaRepositorio _repoCategoriaPrograma = new CategoriaProgramaRepositorio();
                var listaCategoriaPrograma = _repoCategoriaPrograma.ObtenerCategoriasNombrePrograma();
                return Json(new { Result = "OK", Records = listaCategoriaPrograma });
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

                CertificacionRepositorio _repoCertificacion = new CertificacionRepositorio();
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

                CertificacionRepositorio _repoCertificacion = new CertificacionRepositorio();
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
        public ActionResult ObtenerListaTipoModalidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                TipoModalidadRepositorio _repoTipoModalidad = new TipoModalidadRepositorio();
                var listaTipoModalidad = _repoTipoModalidad.ObtenerTodoTipoModalidadesFiltro();
                return Json(new { Result = "OK", Records = listaTipoModalidad });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPgeneral()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PgeneralRepositorio _repoPgeneral = new PgeneralRepositorio();
                var listaPgeneral = _repoPgeneral.ObtenerProgramasConAreaSubAreaCategoriaFiltro();
                return Json(new { Result = "OK", Records = listaPgeneral });
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
        public ActionResult VisualizarCompetidores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CompetidorRepositorio _repoCompetidor = new CompetidorRepositorio();
                var Competidores = _repoCompetidor.ObtenerTodoCompetidores();
                return Json(new { Result = "OK", Records = Competidores });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCompetidor([FromBody] CompetidorCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CompetidorRepositorio _repoCompetidor = new CompetidorRepositorio();
                CompetidorVentajaDesventajaRepositorio _repoCompetidorVD = new CompetidorVentajaDesventajaRepositorio();
                CompetidorProgramaRelacionadoRepositorio _repoCompetidorProgramaRelacionado = new CompetidorProgramaRelacionadoRepositorio();
                CompetidorCertificacionRepositorio _repoCompetidorCertificacion = new CompetidorCertificacionRepositorio();
                CompetidorMembresiaRepositorio _repoCompetidorMembresia = new CompetidorMembresiaRepositorio();
                CompetidorCapacitacionRepositorio _repoCompetidorCapacitacion = new CompetidorCapacitacionRepositorio();
                CompetidorTipoModalidadRepositorio _repoCompetidorTipoModalidad = new CompetidorTipoModalidadRepositorio();


                CompetidorBO NuevoCompetidor = new CompetidorBO();

                NuevoCompetidor.Nombre = ObjetoDTO.Nombre;
                NuevoCompetidor.DuracionCronologica = ObjetoDTO.DuracionCronologica;
                NuevoCompetidor.CostoNeto = ObjetoDTO.CostoNeto;
                NuevoCompetidor.Precio = ObjetoDTO.Precio;
                NuevoCompetidor.IdMoneda = ObjetoDTO.IdMoneda;
                NuevoCompetidor.IdInstitucionCompetidora = ObjetoDTO.IdInstitucionCompetidora;
                NuevoCompetidor.IdPais = ObjetoDTO.IdPais;
                NuevoCompetidor.IdCiudad = ObjetoDTO.IdCiudad;
                NuevoCompetidor.IdRegionCiudad = ObjetoDTO.IdRegionCiudad;
                NuevoCompetidor.IdAeaCapacitacion = ObjetoDTO.IdAeaCapacitacion;
                NuevoCompetidor.IdSubAreaCapacitacion = ObjetoDTO.IdSubAreaCapacitacion;
                NuevoCompetidor.IdCategoria = ObjetoDTO.IdCategoria;

                NuevoCompetidor.Estado = true;
                NuevoCompetidor.UsuarioCreacion = ObjetoDTO.Usuario;
                NuevoCompetidor.UsuarioModificacion = ObjetoDTO.Usuario;
                NuevoCompetidor.FechaCreacion = DateTime.Now;
                NuevoCompetidor.FechaModificacion = DateTime.Now;

                _repoCompetidor.Insert(NuevoCompetidor);

                //insercion de Ventajas o desventajas
                if (ObjetoDTO.Ventajas != null)
                {
                    var Ventajas = ObjetoDTO.Ventajas;
                    for (int i=0; i<Ventajas.Count; ++i)
                    {
                        CompetidorVentajaDesventajaBO VentajaNuevo = new CompetidorVentajaDesventajaBO();
                        VentajaNuevo.IdCompetidor = NuevoCompetidor.Id;
                        VentajaNuevo.Tipo = Ventajas[i].Tipo;
                        VentajaNuevo.Contenido = Ventajas[i].Contenido;
                        VentajaNuevo.Estado = true;
                        VentajaNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        VentajaNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        VentajaNuevo.FechaCreacion = DateTime.Now;
                        VentajaNuevo.FechaModificacion = DateTime.Now;

                        _repoCompetidorVD.Insert(VentajaNuevo);
                    }
                }

                //insercion de ProgramasRelacionados
                if (ObjetoDTO.PGenerales != null)
                {
                    var PGenerales = ObjetoDTO.PGenerales;
                    for (int i = 0; i < PGenerales.Count; ++i)
                    {
                        CompetidorProgramaRelacionadoBO PRelacionadoNuevo = new CompetidorProgramaRelacionadoBO();
                        PRelacionadoNuevo.IdCompetidor = NuevoCompetidor.Id;
                        PRelacionadoNuevo.IdPrograma = PGenerales[i];
                        PRelacionadoNuevo.Estado = true;
                        PRelacionadoNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        PRelacionadoNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        PRelacionadoNuevo.FechaCreacion = DateTime.Now;
                        PRelacionadoNuevo.FechaModificacion = DateTime.Now;

                        _repoCompetidorProgramaRelacionado.Insert(PRelacionadoNuevo);
                    }
                }

                //insercion de Capacitacion
                if (ObjetoDTO.Capacitaciones != null)
                {
                    var Capacitaciones = ObjetoDTO.Capacitaciones;
                    for (int i = 0; i < Capacitaciones.Count; ++i)
                    {
                        CompetidorCapacitacionBO ProgramaCapacitacionNuevo = new CompetidorCapacitacionBO();
                        ProgramaCapacitacionNuevo.IdCompetidor = NuevoCompetidor.Id;
                        ProgramaCapacitacionNuevo.IdProgramaCapacitacion = Capacitaciones[i];
                        ProgramaCapacitacionNuevo.Estado = true;
                        ProgramaCapacitacionNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        ProgramaCapacitacionNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        ProgramaCapacitacionNuevo.FechaCreacion = DateTime.Now;
                        ProgramaCapacitacionNuevo.FechaModificacion = DateTime.Now;

                        _repoCompetidorCapacitacion.Insert(ProgramaCapacitacionNuevo);
                    }
                }

                //insercion de Membresias
                if (ObjetoDTO.Membresias != null)
                {
                    var Membresias = ObjetoDTO.Membresias;
                    for (int i = 0; i < Membresias.Count; ++i)
                    {
                        CompetidorMembresiaBO ProgramaMembresiaNuevo = new CompetidorMembresiaBO();
                        ProgramaMembresiaNuevo.IdCompetidor = NuevoCompetidor.Id;
                        ProgramaMembresiaNuevo.IdCertificacion = Membresias[i];
                        ProgramaMembresiaNuevo.Estado = true;
                        ProgramaMembresiaNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        ProgramaMembresiaNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        ProgramaMembresiaNuevo.FechaCreacion = DateTime.Now;
                        ProgramaMembresiaNuevo.FechaModificacion = DateTime.Now;

                        _repoCompetidorMembresia.Insert(ProgramaMembresiaNuevo);
                    }
                }

                //insercion de Certificacion
                if (ObjetoDTO.Certificaciones != null)
                {
                    var Certificaciones = ObjetoDTO.Certificaciones;
                    for (int i = 0; i < Certificaciones.Count; ++i)
                    {
                        CompetidorCertificacionBO ProgramaCertificacionNuevo = new CompetidorCertificacionBO();
                        ProgramaCertificacionNuevo.IdCompetidor = NuevoCompetidor.Id;
                        ProgramaCertificacionNuevo.IdCertificacion = Certificaciones[i];
                        ProgramaCertificacionNuevo.Estado = true;
                        ProgramaCertificacionNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        ProgramaCertificacionNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        ProgramaCertificacionNuevo.FechaCreacion = DateTime.Now;
                        ProgramaCertificacionNuevo.FechaModificacion = DateTime.Now;

                        _repoCompetidorCertificacion.Insert(ProgramaCertificacionNuevo);
                    }
                }

                //insercion de TipoModalidad
                if (ObjetoDTO.Certificaciones != null)
                {
                    var TipoModalidades = ObjetoDTO.TipoModalidades;
                    for (int i = 0; i < TipoModalidades.Count; ++i)
                    {
                        CompetidorTipoModalidadBO ProgramaTipoModalidadNuevo = new CompetidorTipoModalidadBO();
                        ProgramaTipoModalidadNuevo.IdCompetidor = NuevoCompetidor.Id;
                        ProgramaTipoModalidadNuevo.IdTipoModalidad = TipoModalidades[i];
                        ProgramaTipoModalidadNuevo.Estado = true;
                        ProgramaTipoModalidadNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        ProgramaTipoModalidadNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        ProgramaTipoModalidadNuevo.FechaCreacion = DateTime.Now;
                        ProgramaTipoModalidadNuevo.FechaModificacion = DateTime.Now;

                        _repoCompetidorTipoModalidad.Insert(ProgramaTipoModalidadNuevo);
                    }
                }

                return Ok(NuevoCompetidor);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarCompetidor([FromBody] CompetidorCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CompetidorRepositorio _repoCompetidor = new CompetidorRepositorio();
                CompetidorVentajaDesventajaRepositorio _repoCompetidorVD = new CompetidorVentajaDesventajaRepositorio();
                CompetidorProgramaRelacionadoRepositorio _repoCompetidorProgramaRelacionado = new CompetidorProgramaRelacionadoRepositorio();
                CompetidorCertificacionRepositorio _repoCompetidorCertificacion = new CompetidorCertificacionRepositorio();
                CompetidorMembresiaRepositorio _repoCompetidorMembresia = new CompetidorMembresiaRepositorio();
                CompetidorCapacitacionRepositorio _repoCompetidorCapacitacion = new CompetidorCapacitacionRepositorio();
                CompetidorTipoModalidadRepositorio _repoCompetidorTipoModalidad = new CompetidorTipoModalidadRepositorio();


                CompetidorBO Competidor = _repoCompetidor.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();
                if (Competidor == null) throw new Exception("El registro que se quiere actualizar no existe ¿Id correcto?");

                Competidor.Nombre = ObjetoDTO.Nombre;
                Competidor.DuracionCronologica = ObjetoDTO.DuracionCronologica;
                Competidor.CostoNeto = ObjetoDTO.CostoNeto;
                Competidor.Precio = ObjetoDTO.Precio;
                Competidor.IdMoneda = ObjetoDTO.IdMoneda;
                Competidor.IdInstitucionCompetidora = ObjetoDTO.IdInstitucionCompetidora;
                Competidor.IdPais = ObjetoDTO.IdPais;
                Competidor.IdCiudad = ObjetoDTO.IdCiudad;
                Competidor.IdRegionCiudad = ObjetoDTO.IdRegionCiudad;
                Competidor.IdAeaCapacitacion = ObjetoDTO.IdAeaCapacitacion;
                Competidor.IdSubAreaCapacitacion = ObjetoDTO.IdSubAreaCapacitacion;
                Competidor.IdCategoria = ObjetoDTO.IdCategoria;

                Competidor.Estado = true;
                Competidor.UsuarioModificacion = ObjetoDTO.Usuario;
                Competidor.FechaModificacion = DateTime.Now;


                _repoCompetidor.Update(Competidor);


                //reinsercion de Ventajas o desventajas
                var ventajasEnDB = _repoCompetidorVD.GetBy(x=>x.IdCompetidor==ObjetoDTO.Id).ToList();
                for (int i = 0; i < ventajasEnDB.Count; ++i)
                    _repoCompetidorVD.Delete(ventajasEnDB[i].Id, ObjetoDTO.Usuario);

                if (ObjetoDTO.Ventajas != null)
                {
                    var Ventajas = ObjetoDTO.Ventajas;
                    for (int i = 0; i < Ventajas.Count; ++i)
                    {
                        CompetidorVentajaDesventajaBO VentajaNuevo = new CompetidorVentajaDesventajaBO();
                        VentajaNuevo.IdCompetidor = ObjetoDTO.Id;
                        VentajaNuevo.Tipo = Ventajas[i].Tipo;
                        VentajaNuevo.Contenido = Ventajas[i].Contenido;
                        VentajaNuevo.Estado = true;
                        VentajaNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        VentajaNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        VentajaNuevo.FechaCreacion = DateTime.Now;
                        VentajaNuevo.FechaModificacion = DateTime.Now;

                        _repoCompetidorVD.Insert(VentajaNuevo);
                    }
                }
                //fin reinsercion de Ventajas o desventajas


                var PGeneralesEnDB = _repoCompetidorProgramaRelacionado.GetBy(x => x.IdCompetidor == ObjetoDTO.Id).ToList();
                for (int i = 0; i < PGeneralesEnDB.Count; ++i)
                    _repoCompetidorProgramaRelacionado.Delete(PGeneralesEnDB[i].Id, ObjetoDTO.Usuario);
                //insercion de ProgramasRelacionados
                if (ObjetoDTO.PGenerales != null)
                {
                    var PGenerales = ObjetoDTO.PGenerales;
                    for (int i = 0; i < PGenerales.Count; ++i)
                    {
                        CompetidorProgramaRelacionadoBO PRelacionadoNuevo = new CompetidorProgramaRelacionadoBO();
                        PRelacionadoNuevo.IdCompetidor = Competidor.Id;
                        PRelacionadoNuevo.IdPrograma = PGenerales[i];
                        PRelacionadoNuevo.Estado = true;
                        PRelacionadoNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        PRelacionadoNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        PRelacionadoNuevo.FechaCreacion = DateTime.Now;
                        PRelacionadoNuevo.FechaModificacion = DateTime.Now;

                        _repoCompetidorProgramaRelacionado.Insert(PRelacionadoNuevo);
                    }
                }



                var CapacitacionesEnDB = _repoCompetidorCapacitacion.GetBy(x => x.IdCompetidor == ObjetoDTO.Id).ToList();
                for (int i = 0; i < CapacitacionesEnDB.Count; ++i)
                    _repoCompetidorCapacitacion.Delete(CapacitacionesEnDB[i].Id, ObjetoDTO.Usuario);
                
                //insercion de Capacitacion
                if (ObjetoDTO.Capacitaciones != null)
                {
                    var Capacitaciones = ObjetoDTO.Capacitaciones;
                    for (int i = 0; i < Capacitaciones.Count; ++i)
                    {
                        CompetidorCapacitacionBO ProgramaCapacitacionNuevo = new CompetidorCapacitacionBO();
                        ProgramaCapacitacionNuevo.IdCompetidor = Competidor.Id;
                        ProgramaCapacitacionNuevo.IdProgramaCapacitacion = Capacitaciones[i];
                        ProgramaCapacitacionNuevo.Estado = true;
                        ProgramaCapacitacionNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        ProgramaCapacitacionNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        ProgramaCapacitacionNuevo.FechaCreacion = DateTime.Now;
                        ProgramaCapacitacionNuevo.FechaModificacion = DateTime.Now;

                        _repoCompetidorCapacitacion.Insert(ProgramaCapacitacionNuevo);
                    }
                }


                var MembresiasEnDB = _repoCompetidorMembresia.GetBy(x => x.IdCompetidor == ObjetoDTO.Id).ToList();
                for (int i = 0; i < MembresiasEnDB.Count; ++i)
                    _repoCompetidorMembresia.Delete(MembresiasEnDB[i].Id, ObjetoDTO.Usuario);

                //insercion de Membresias
                if (ObjetoDTO.Membresias != null)
                {
                    var Membresias = ObjetoDTO.Membresias;
                    for (int i = 0; i < Membresias.Count; ++i)
                    {
                        CompetidorMembresiaBO ProgramaMembresiaNuevo = new CompetidorMembresiaBO();
                        ProgramaMembresiaNuevo.IdCompetidor = Competidor.Id;
                        ProgramaMembresiaNuevo.IdCertificacion = Membresias[i];
                        ProgramaMembresiaNuevo.Estado = true;
                        ProgramaMembresiaNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        ProgramaMembresiaNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        ProgramaMembresiaNuevo.FechaCreacion = DateTime.Now;
                        ProgramaMembresiaNuevo.FechaModificacion = DateTime.Now;

                        _repoCompetidorMembresia.Insert(ProgramaMembresiaNuevo);
                    }
                }

                var CertificacionesEnDB = _repoCompetidorCertificacion.GetBy(x => x.IdCompetidor == ObjetoDTO.Id).ToList();
                for (int i = 0; i < CertificacionesEnDB.Count; ++i)
                    _repoCompetidorCertificacion.Delete(CertificacionesEnDB[i].Id, ObjetoDTO.Usuario);

                //insercion de Certificacion
                if (ObjetoDTO.Certificaciones != null)
                {
                    var Certificaciones = ObjetoDTO.Certificaciones;
                    for (int i = 0; i < Certificaciones.Count; ++i)
                    {
                        CompetidorCertificacionBO ProgramaCertificacionNuevo = new CompetidorCertificacionBO();
                        ProgramaCertificacionNuevo.IdCompetidor = Competidor.Id;
                        ProgramaCertificacionNuevo.IdCertificacion = Certificaciones[i];
                        ProgramaCertificacionNuevo.Estado = true;
                        ProgramaCertificacionNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        ProgramaCertificacionNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        ProgramaCertificacionNuevo.FechaCreacion = DateTime.Now;
                        ProgramaCertificacionNuevo.FechaModificacion = DateTime.Now;

                        _repoCompetidorCertificacion.Insert(ProgramaCertificacionNuevo);
                    }
                }


                var TipoModalidadesEnDB = _repoCompetidorTipoModalidad.GetBy(x => x.IdCompetidor == ObjetoDTO.Id).ToList();
                for (int i = 0; i < TipoModalidadesEnDB.Count; ++i)
                    _repoCompetidorTipoModalidad.Delete(TipoModalidadesEnDB[i].Id, ObjetoDTO.Usuario);

                //insercion de TipoModalidad
                if (ObjetoDTO.Certificaciones != null)
                {
                    var TipoModalidades = ObjetoDTO.TipoModalidades;
                    for (int i = 0; i < TipoModalidades.Count; ++i)
                    {
                        CompetidorTipoModalidadBO ProgramaTipoModalidadNuevo = new CompetidorTipoModalidadBO();
                        ProgramaTipoModalidadNuevo.IdCompetidor = Competidor.Id;
                        ProgramaTipoModalidadNuevo.IdTipoModalidad = TipoModalidades[i];
                        ProgramaTipoModalidadNuevo.Estado = true;
                        ProgramaTipoModalidadNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        ProgramaTipoModalidadNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        ProgramaTipoModalidadNuevo.FechaCreacion = DateTime.Now;
                        ProgramaTipoModalidadNuevo.FechaModificacion = DateTime.Now;

                        _repoCompetidorTipoModalidad.Insert(ProgramaTipoModalidadNuevo);
                    }
                }


                return Ok(Competidor);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]/{UserName}/{Id}")]
        [HttpDelete]
        public ActionResult EliminarCompetidor(int Id, string UserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CompetidorRepositorio _repoCompetidor = new CompetidorRepositorio();
                CompetidorVentajaDesventajaRepositorio _repoCompetidorVD = new CompetidorVentajaDesventajaRepositorio();
                CompetidorProgramaRelacionadoRepositorio _repoCompetidorProgramaRelacionado = new CompetidorProgramaRelacionadoRepositorio();
                CompetidorCertificacionRepositorio _repoCompetidorCertificacion = new CompetidorCertificacionRepositorio();
                CompetidorMembresiaRepositorio _repoCompetidorMembresia = new CompetidorMembresiaRepositorio();
                CompetidorCapacitacionRepositorio _repoCompetidorCapacitacion = new CompetidorCapacitacionRepositorio();
                CompetidorTipoModalidadRepositorio _repoCompetidorTipoModalidad = new CompetidorTipoModalidadRepositorio();


                var PGeneralesEnDB = _repoCompetidorProgramaRelacionado.GetBy(x => x.IdCompetidor == Id).ToList();
                for (int i = 0; i < PGeneralesEnDB.Count; ++i)
                    _repoCompetidorProgramaRelacionado.Delete(PGeneralesEnDB[i].Id, UserName);
                
                var CapacitacionesEnDB = _repoCompetidorCapacitacion.GetBy(x => x.IdCompetidor == Id).ToList();
                for (int i = 0; i < CapacitacionesEnDB.Count; ++i)
                    _repoCompetidorCapacitacion.Delete(CapacitacionesEnDB[i].Id, UserName);

                var MembresiasEnDB = _repoCompetidorMembresia.GetBy(x => x.IdCompetidor == Id).ToList();
                for (int i = 0; i < MembresiasEnDB.Count; ++i)
                    _repoCompetidorMembresia.Delete(MembresiasEnDB[i].Id, UserName);

                var CertificacionesEnDB = _repoCompetidorCertificacion.GetBy(x => x.IdCompetidor == Id).ToList();
                for (int i = 0; i < CertificacionesEnDB.Count; ++i)
                    _repoCompetidorCertificacion.Delete(CertificacionesEnDB[i].Id, UserName);

                var TipoModalidadesEnDB = _repoCompetidorTipoModalidad.GetBy(x => x.IdCompetidor == Id).ToList();
                for (int i = 0; i < TipoModalidadesEnDB.Count; ++i)
                    _repoCompetidorTipoModalidad.Delete(TipoModalidadesEnDB[i].Id, UserName);

                var ventajasEnDB = _repoCompetidorVD.GetBy(x => x.IdCompetidor == Id).ToList();
                for (int i = 0; i < ventajasEnDB.Count; ++i)
                    _repoCompetidorVD.Delete(ventajasEnDB[i].Id, UserName);


                _repoCompetidor.Delete(Id, UserName);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
#endregion
    }




    public class ValidadorCompetidorDTO : AbstractValidator<TCompetidor>
    {
        public static ValidadorCompetidorDTO Current = new ValidadorCompetidorDTO();
        public ValidadorCompetidorDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
            
        }
    }
}
