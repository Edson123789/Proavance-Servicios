using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Transactions;
using System.Globalization;
using System.Net;
using BSI.Integra.Aplicacion.Transversal.Socket;
using BSI.Integra.Aplicacion.Transversal.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AsignacionManualController
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Oportunidades y asignación
    /// </summary>

    [Route("api/AsignacionManual")]//Hace referencia al BO principal "Asignacion manual"
    public class AsignacionManualController : ControllerBase
    {
        private static readonly int OCURRENCIA_ASIGNACION_MANUAL = 35;
        private readonly integraDBContext _integraDBContext;
        public AsignacionManualController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }


        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros de Oportunidades
        /// </summary>
        /// <returns> Obtiene Lista de Registros de Oportunidades y su información : ResultadoAsignacionManualFiltroTotalDTO </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerOportunidades([FromBody] AsignacionAutomaticaManualOportunidadFiltroGrillaDTO obj)
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                OperadorComparacionRepositorio _repOperadorComparacion = new OperadorComparacionRepositorio(_integraDBContext);

                var oportunidadManual = _repOportunidad.ObtenerPorFiltroPaginaManual(obj.filtro, obj.paginador, obj.filter, _repOperadorComparacion.ObtenerListado());

                foreach (var item in oportunidadManual.data)
                {
                    item.Email = EncriptarStringCorreo(item.Email);
                }

                return Ok(oportunidadManual);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// TipoFuncion: GET
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Filtros para interfaz
        /// </summary>
        /// <returns> Obtiene Información para combos : filtroAsignacionManualDTO </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltros()
        {
            TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio(_integraDBContext);
            PgeneralRepositorio _repPgeneral = new PgeneralRepositorio(_integraDBContext);
            AreaCapacitacionRepositorio _repAreaCapacitacion = new AreaCapacitacionRepositorio(_integraDBContext);
            SubAreaCapacitacionRepositorio _repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio(_integraDBContext);
            CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
            CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio(_integraDBContext);
            PaisRepositorio _repPaisRepositorio = new PaisRepositorio(_integraDBContext);
            ProbabilidadRegistroPwRepositorio _repProbabilidadRegistroPw = new ProbabilidadRegistroPwRepositorio(_integraDBContext);
            FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio();
            PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
            OrigenRepositorio _repOrigen = new OrigenRepositorio(_integraDBContext);
            TipoCategoriaOrigenRepositorio _repTipoCategoriaOrigen = new TipoCategoriaOrigenRepositorio(_integraDBContext);
            OperadorComparacionRepositorio _repOperadorComparacion = new OperadorComparacionRepositorio(_integraDBContext);

            try
            {
                filtroAsignacionManualDTO filtroAsignacionManual = new filtroAsignacionManualDTO()
                {
                    filtroPersonal = _repPersonal.CargarPersonalParaFiltro(),
                    filtroCentroCosto = _repCentroCosto.ObtenerCentroCostoParaFiltro(),
                    filtroFaseOportunidad = _repFaseOportunidad.ObtenerFaseOportunidadTodoFiltro(),
                    filtroSubAreaCapacitacion = _repSubAreaCapacitacion.ObtenerTodoFiltro(),
                    filtroAreaCapacitacion = _repAreaCapacitacion.ObtenerTodoFiltro(),
                    filtroPgeneral = _repPgeneral.ObtenerProgramasFiltro(),
                    filtroTipoDato = _repTipoDato.ObtenerFiltro(),
                    filtroCategoriaOrigen = _repCategoriaOrigen.ObtenerCategoriaFiltro(),
                    filtroPais = _repPaisRepositorio.ObtenerTodoFiltro(),
                    filtroProbabilidad = _repProbabilidadRegistroPw.ObtenerTodoFiltro(),
                    filtroOrigen = _repOrigen.ObtenerTodoFiltro(),
                    filtroTipoCategoriaOrigen = _repTipoCategoriaOrigen.ObtenerTodoFiltro(),
                    filtroOperadorComparacion = _repOperadorComparacion.ObtenerListaOperadorComparacion()
                };


                return Ok(filtroAsignacionManual);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de alumnos AutoComplete
        /// </summary>
        /// <returns> Obtiene nombres de alumnos AutoComplete : List<AlumnoFiltroAutocompleteDTO>  </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerAlumnoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                    var resultados = _repAlumno.ObtenerTodoFiltroAutoComplete(Filtros["valor"].ToString());
                    //var resultados= _repAlumno.GetBy(w => w.Estado == true && (w.ApellidoPaterno +" "+ w.ApellidoMaterno + " " + w.Nombre1 + " " + w.Nombre2).Contains(Filtros["valor"].ToString()),y=> new FiltroDTO { Id=y.Id,Nombre=(y.ApellidoPaterno+ " " + y.ApellidoMaterno+ " " + y.Nombre1+ " " + y.Nombre2)});
                    return Ok(resultados);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de personal AutoComplete
        /// </summary>
        /// <returns> Obtiene nombres de personal AutoComplete : List<PersonalAutocompleteDTO>  </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerPersonalAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (Filtros != null)
                {

                    PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                    var personal = _repPersonal.CargarPersonalAutoComplete(Filtros["valor"]);
                    return Ok(personal);
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de centros de costo AutoComplete
        /// </summary>
        /// <returns> Obtiene nombres de centros de costo AutoComplete : List<CentroCostoFiltroAutocompleteDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (Filtros != null)
                {
                    CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
                    var centroCosto = _repCentroCosto.ObtenerTodoFiltroAutoComplete(Filtros["valor"]);
                    return Ok(centroCosto);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Asignación de oportunidades por Asesor
        /// </summary>
        /// <param name="AsignarAsesor"> Lista de Id de oportunidades asignado a asesor </param>
        /// <param name="Usuario"> Usuario de módulo </param>
        /// <returns> Confirmación de asignación : Bool </returns>
        [Route("[Action]/{Usuario}")]
        [HttpPost]
        public ActionResult AsignarAsesor([FromBody] AsignarAsesorManualDTO AsignarAsesor, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                AsignacionOportunidadRepositorio _repAsignacionOportunidad = new AsignacionOportunidadRepositorio(_integraDBContext);
                AsignacionOportunidadLogRepositorio _repAsignacionOportunidadLog = new AsignacionOportunidadLogRepositorio(_integraDBContext);
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                List<OportunidadesAsesorAsignacionAutomaticaDTO> oportunidadesAsesorAsignacionAutomatica = new List<OportunidadesAsesorAsignacionAutomaticaDTO>();
                DateTime? fecha;

                if (!AsignarAsesor.IdAsesor.HasValue) AsignarAsesor.IdAsesor = 0;
                if (!AsignarAsesor.IdCentroCosto.HasValue) AsignarAsesor.IdCentroCosto = 0;
                if (!AsignarAsesor.FechaProgramada.HasValue) fecha = null; else fecha = AsignarAsesor.FechaProgramada;

                var oportunidadesFaltantes = AsignarAsesor.IdOportunidades.ToList();
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (int idOportunidad in AsignarAsesor.IdOportunidades)
                    {
                        //Actualizar Oportunidad con centro costo y/o asesor
                        OportunidadBO opo = new OportunidadBO(idOportunidad, Usuario, _integraDBContext);
                        AsignacionOportunidadLogBO asignacionLog = new AsignacionOportunidadLogBO();
                        if (opo.IdPersonalAsignado == 125)
                        {
                            OportunidadesAsesorAsignacionAutomaticaDTO oportunidadesAsesorAsignacion = new OportunidadesAsesorAsignacionAutomaticaDTO()
                            {
                                Id = opo.Id,
                                IdMigracion = opo.IdMigracion
                            };
                            oportunidadesAsesorAsignacionAutomatica.Add(oportunidadesAsesorAsignacion);
                        }

                        asignacionLog.FechaLog = DateTime.Now;
                        asignacionLog.IdPersonalAnterior = opo.IdPersonalAsignado;
                        asignacionLog.IdCentroCostoAnt = opo.IdCentroCosto;
                        asignacionLog.IdOportunidad = opo.Id;

                        opo.Id = idOportunidad;
                        var validacionCentroCostoV2 = opo.IdCentroCosto;
                        opo.IdCentroCosto = AsignarAsesor.IdCentroCosto.Value == 0 ? opo.IdCentroCosto : AsignarAsesor.IdCentroCosto.Value;
                        opo.IdPersonalAsignado = AsignarAsesor.IdAsesor.Value == 0 ? opo.IdPersonalAsignado : AsignarAsesor.IdAsesor.Value;

                        //VALIDACION DE CAMBIO DE CENTRO DE COSTO
                        if (AsignarAsesor.IdCentroCosto != null && AsignarAsesor.IdCentroCosto != 0)
                        {
                            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                            CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
                            PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
                            AlumnoRepositorio _repAlumnoCambio = new AlumnoRepositorio(_integraDBContext);

                            //Obtener el IdPEspecifico según el centro de costo Anterior
                            if (validacionCentroCostoV2 != null)
                            {
                                var pEspecificoCambio = _repPEspecifico.GetBy(x => x.IdCentroCosto == validacionCentroCostoV2).FirstOrDefault();

                                if (pEspecificoCambio != null)
                                {
                                    //Validamos que la matrícula exista con el Id del Alumno y el Id de PEspecifico
                                    var validarMatricula = _repMatriculaCabecera.GetBy(x => x.IdAlumno == opo.IdAlumno && x.IdPespecifico == pEspecificoCambio.Id).FirstOrDefault();
                                    if (validarMatricula != null)
                                    {
                                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "AsignarAsesor/ValidarMatricula", Parametros = $"IdAlumno={opo.IdAlumno}&IdPEspecifico={pEspecificoCambio.Id}", Mensaje = "Error en validacion de Matricula en Asignacion de asesor", Excepcion = "Error en validacion de Matricula en Asignacion de asesor", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                                        var datosAlumno = _repAlumnoCambio.FirstById(opo.IdAlumno);
                                        return BadRequest("El alumno: " + datosAlumno.Nombre1 + " " + datosAlumno.Nombre2 + " " + datosAlumno.ApellidoPaterno + " " + datosAlumno.ApellidoMaterno + " ya tiene una Matricula Cabecera Registrada, si desea hacer el cambio de Centro de Costo comunicarse con Operaciones");
                                    }

                                    MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(_integraDBContext);
                                    var validacionMontoPagoCronograma = _repMontoPagoCronograma.GetBy(x => x.IdOportunidad == idOportunidad).FirstOrDefault();
                                    if (validacionMontoPagoCronograma != null)
                                    {
                                        var validarMatricula2 = _repMatriculaCabecera.GetBy(x => x.IdCronograma == validacionMontoPagoCronograma.Id).FirstOrDefault();
                                        if (validarMatricula2 != null)
                                        {
                                            var datosAlumno = _repAlumnoCambio.FirstById(opo.IdAlumno);
                                            _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "AsignarAsesor/ValidarCronograma", Parametros = $"IdAlumno={opo.IdAlumno}&IdPEspecifico={pEspecificoCambio.Id}&IdCronograma={validacionMontoPagoCronograma.Id}", Mensaje = "Error en validacion de Matricula en Asignacion de asesor", Excepcion = "Error en validacion de Matricula en Asignacion de asesor", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                                            return BadRequest("El alumno: " + datosAlumno.Nombre1 + " " + datosAlumno.Nombre2 + " " + datosAlumno.ApellidoPaterno + " " + datosAlumno.ApellidoMaterno + " ya tiene una Matricula Cabecera Registrada, si desea hacer el cambio de Centro de Costo comunicarse con Operaciones");
                                        }
                                    }
                                }
                            }

                        }

                        //Registramos la asignacion con los nuevos datos
                        opo.AsignacionOportunidad = _repAsignacionOportunidad.ObtenerPorIdOportunidad(idOportunidad);

                        AsignacionOportunidadBO asig = new AsignacionOportunidadBO();

                        if (opo.AsignacionOportunidad == null)
                        {
                            //Creamos un nuevo registro en asignacionOportunidad
                            asig.FechaAsignacion = DateTime.Now;
                            asig.IdAlumno = opo.IdAlumno;
                            asig.IdClasificacionPersona = opo.IdClasificacionPersona;
                            asig.IdPersonal = opo.IdPersonalAsignado;
                            asig.IdCentroCosto = opo.IdCentroCosto.Value;
                            asig.IdOportunidad = idOportunidad;
                            asig.IdTipoDato = opo.IdTipoDato;
                            asig.IdFaseOportunidad = opo.IdFaseOportunidad;
                            asig.Estado = true;
                            asig.FechaCreacion = DateTime.Now;
                            asig.FechaModificacion = DateTime.Now;
                            asig.UsuarioCreacion = Usuario;
                            asig.UsuarioModificacion = Usuario;

                            opo.AsignacionOportunidad = asig;
                        }
                        opo.AsignacionOportunidad.FechaAsignacion = DateTime.Now;
                        opo.AsignacionOportunidad.IdPersonal = opo.IdPersonalAsignado == 0 ? opo.AsignacionOportunidad.IdPersonal : opo.IdPersonalAsignado;
                        opo.AsignacionOportunidad.IdCentroCosto = opo.IdCentroCosto == 0 ? opo.AsignacionOportunidad.IdCentroCosto : opo.IdCentroCosto.Value;
                        opo.AsignacionOportunidad.IdAlumno = opo.IdAlumno == 0 ? opo.AsignacionOportunidad.IdAlumno : opo.IdAlumno;
                        opo.AsignacionOportunidad.IdClasificacionPersona = opo.IdClasificacionPersona == 0 ? opo.AsignacionOportunidad.IdClasificacionPersona : opo.IdClasificacionPersona;
                        opo.AsignacionOportunidad.IdPersonal = opo.IdPersonalAsignado == 0 ? opo.AsignacionOportunidad.IdPersonal : opo.IdPersonalAsignado;
                        opo.AsignacionOportunidad.FechaModificacion = DateTime.Now;
                        opo.AsignacionOportunidad.UsuarioModificacion = Usuario;

                        asignacionLog.IdTipoDato = opo.AsignacionOportunidad.IdTipoDato;
                        asignacionLog.IdPersonal = opo.AsignacionOportunidad.IdPersonal;
                        asignacionLog.IdFaseOportunidad = opo.AsignacionOportunidad.IdFaseOportunidad;
                        asignacionLog.IdAlumno = opo.AsignacionOportunidad.IdAlumno;
                        asignacionLog.IdClasificacionPersona = opo.AsignacionOportunidad.IdClasificacionPersona;
                        asignacionLog.Estado = true;
                        asignacionLog.FechaCreacion = DateTime.Now;
                        asignacionLog.FechaModificacion = DateTime.Now;
                        asignacionLog.UsuarioCreacion = Usuario;
                        asignacionLog.UsuarioModificacion = Usuario;
                        asignacionLog.IdCentroCosto = opo.AsignacionOportunidad.IdCentroCosto;
                        asignacionLog.IdAsignacionOportunidad = opo.AsignacionOportunidad.Id;
                        opo.AsignacionOportunidad.AsignacionOportunidadLog = asignacionLog;

                        //Finalizar Actividad
                        opo.ActividadAntigua = new ActividadDetalleBO(opo.IdActividadDetalleUltima, _integraDBContext);
                        opo.ActividadAntigua.Comentario = "Asignacion Manual";
                        opo.ActividadAntigua.IdOcurrencia = OCURRENCIA_ASIGNACION_MANUAL;
                        opo.ActividadAntigua.IdOcurrenciaAlterno = OCURRENCIA_ASIGNACION_MANUAL;
                        opo.ActividadAntigua.IdOcurrenciaActividad = null;
                        opo.ActividadAntigua.IdOcurrenciaActividadAlterno = null;
                        opo.ActividadAntigua.IdAlumno = opo.IdAlumno;
                        opo.ActividadAntigua.IdClasificacionPersona = opo.IdClasificacionPersona;
                        opo.ActividadAntigua.IdOportunidad = opo.Id;
                        opo.ActividadAntigua.IdCentralLlamada = 0;
                        opo.ActividadAntigua.IdActividadCabecera = opo.IdActividadCabeceraUltima;


                        OportunidadDTO oportunidad = new OportunidadDTO();

                        oportunidad.Id = opo.Id;
                        oportunidad.IdCentroCosto = opo.IdCentroCosto.Value;
                        oportunidad.IdPersonalAsignado = opo.IdPersonalAsignado;
                        oportunidad.IdTipoDato = opo.IdTipoDato;
                        oportunidad.IdFaseOportunidad = opo.IdFaseOportunidad;
                        oportunidad.IdOrigen = opo.IdOrigen;
                        oportunidad.IdAlumno = opo.IdAlumno;
                        oportunidad.UltimoComentario = opo.UltimoComentario;
                        oportunidad.IdActividadDetalleUltima = opo.IdActividadDetalleUltima;
                        oportunidad.IdActividadCabeceraUltima = opo.IdActividadCabeceraUltima;
                        oportunidad.IdEstadoActividadDetalleUltimoEstado = opo.IdEstadoActividadDetalleUltimoEstado;
                        oportunidad.UltimaFechaProgramada = opo.UltimaFechaProgramada.ToString();
                        oportunidad.IdEstadoOportunidad = opo.IdEstadoOportunidad;
                        oportunidad.IdEstadoOcurrenciaUltimo = opo.IdEstadoOcurrenciaUltimo;
                        oportunidad.IdFaseOportunidadMaxima = opo.IdFaseOportunidadMaxima;
                        oportunidad.IdFaseOportunidadInicial = opo.IdFaseOportunidadInicial;
                        oportunidad.IdCategoriaOrigen = opo.IdCategoriaOrigen;
                        oportunidad.IdConjuntoAnuncio = opo.IdConjuntoAnuncio;
                        oportunidad.IdCampaniaScoring = opo.IdCampaniaScoring;
                        oportunidad.IdFaseOportunidadIp = opo.IdFaseOportunidadIp;
                        oportunidad.IdFaseOportunidadIc = opo.IdFaseOportunidadIc;
                        oportunidad.FechaEnvioFaseOportunidadPf = opo.FechaEnvioFaseOportunidadPf;
                        oportunidad.FechaPagoFaseOportunidadPf = opo.FechaPagoFaseOportunidadPf;
                        oportunidad.FechaPagoFaseOportunidadIc = opo.FechaPagoFaseOportunidadIc;
                        oportunidad.FechaRegistroCampania = opo.FechaRegistroCampania;
                        oportunidad.IdFaseOportunidadPortal = opo.IdFaseOportunidadPortal;
                        oportunidad.IdFaseOportunidadPf = opo.IdFaseOportunidadPf;
                        oportunidad.CodigoPagoIc = opo.CodigoPagoIc;
                        oportunidad.FlagVentaCruzada = opo.IdTiempoCapacitacion;
                        oportunidad.IdTiempoCapacitacion = opo.IdTiempoCapacitacion;
                        oportunidad.IdTiempoCapacitacionValidacion = opo.IdTiempoCapacitacionValidacion;
                        oportunidad.IdSubCategoriaDato = opo.IdSubCategoriaDato;
                        oportunidad.IdInteraccionFormulario = opo.IdInteraccionFormulario;
                        oportunidad.UrlOrigen = opo.UrlOrigen;
                        oportunidad.IdClasificacionPersona = opo.IdClasificacionPersona;
                        oportunidad.IdPersonalAreaTrabajo = opo.IdPersonalAreaTrabajo;
                        if (opo.FechaPaso2 != null)
                        {
                            oportunidad.FechaPaso2 = opo.FechaPaso2.Value;
                        }
                        if (opo.Paso2 != null)
                        {
                            oportunidad.Paso2 = opo.Paso2.Value;
                        }
                        if (opo.CodMailing != null)
                        {
                            oportunidad.CodMailing = opo.CodMailing;
                        }
                        oportunidad.IdPagina = opo.IdPagina;

                        opo.FinalizarActividad(false, oportunidad);

                        if (AsignarAsesor.IdAsesor != 0)
                            opo.OportunidadLogNueva.IdAsesorAnt = AsignarAsesor.IdAsesor;

                        if (fecha != null)
                        {
                            opo.UltimaFechaProgramada = fecha;
                        }

                        opo.ProgramaActividad();

                        _repOportunidad.Update(opo);
                        //Programar Actividad
                    }
                    scope.Complete();
                }

                ActividadDetalleRepositorio _repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);
                OportunidadLogRepositorio _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                OportunidadRepositorio _repOportunidad2 = new OportunidadRepositorio(_integraDBContext);
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                foreach (int idOportunidad in AsignarAsesor.IdOportunidades)
                {
                    OportunidadBO oportunidad = new OportunidadBO();
                    OportunidadLogBO oportunidadLog = new OportunidadLogBO();
                    var actividadDetalle = _repActividadDetalle.GetBy(w => w.IdOportunidad == idOportunidad, w => new { w.Id, w.FechaCreacion }).OrderByDescending(y => y.FechaCreacion).FirstOrDefault();
                    if (actividadDetalle != null)
                    {
                        oportunidad = _repOportunidad2.FirstById(idOportunidad);
                        oportunidad.IdActividadDetalleUltima = actividadDetalle.Id;
                        oportunidadLog.IdActividadDetalle = actividadDetalle.Id;
                        _repOportunidad2.Update(oportunidad);
                    }
                }

                try
                {
                    if (AsignarAsesor.IdAsesor != null)
                    {
                        AgendaSocket.getInstance().NuevaActividadParaEjecutar(AsignarAsesor.IdOportunidades[0], AsignarAsesor.IdAsesor.Value);
                    }
                }
                catch (Exception)
                {
                }

                return Ok(new { data = true, OportunidadesAsesorAsignacionAutomatica = oportunidadesAsesorAsignacionAutomatica });
            }
            catch (Exception ex)
            {
                LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "AsignarAsesor", Parametros = $"{AsignarAsesor.IdAsesor},{AsignarAsesor.IdCentroCosto},{AsignarAsesor.IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda - Edgar S.
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Envia Correo desde el modulo de Asignacion Manual
        /// </summary>
        /// <param name="OportunidadesAsesorAsignacionAutomatica">Oportunidades seleccionadas en el módulo para asignaciones en bloque</param>
        /// <returns>Response 200 si en caso se llevó a cabo todo correctamente</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarCorreoAsignacionManual([FromBody] List<OportunidadesAsesorAsignacionAutomaticaDTO> OportunidadesAsesorAsignacionAutomatica)
        {
            try
            {
                foreach (var item in OportunidadesAsesorAsignacionAutomatica)
                {
                    string uri = $"https://integraV4-servicios.bsginstitute.com/api/MailingEnvioAutomatico/EnvioCorreoOportunidadPlantilla/{item.Id}/{ValorEstatico.IdPlantillaInformacionCursoVentas}/false";

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(uri);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return Ok();
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Envia Sms desde el modulo de Asignacion Manual
        /// </summary>
        /// <param name="OportunidadesAsesorAsignacionAutomatica">Oportunidades seleccionadas en el módulo para asignaciones en bloque</param>
        /// <returns>Response 200 si en caso se llevó a cabo todo correctamente</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarSmsAsignacionManual([FromBody] List<OportunidadesAsesorAsignacionAutomaticaDTO> OportunidadesAsesorAsignacionAutomatica)
        {
            try
            {
                foreach (var item in OportunidadesAsesorAsignacionAutomatica)
                {
                    try
                    {
                        string uriSms = string.Empty;

                        if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 21) // Horario permitido para el envio de Sms
                        {
                            if (DateTime.Now.Hour == 18)
                                uriSms = DateTime.Now.Minute < 30 ? $"https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{item.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}" : "https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                            else if (DateTime.Now.Hour > 18)
                                uriSms = "https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{item.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                            else
                                uriSms = "https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{item.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}";
                        }

                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(uriSms);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return Ok();
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase OD
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool </returns>
        [Route("[Action]/{Usuario}")]
        [HttpPost]
        public ActionResult CerrarOportunidadOD([FromBody] int[] IdOportunidades, string Usuario)
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                OportunidadRemarketingAgendaRepositorio _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

                foreach (int idOportunidad in IdOportunidades)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            OportunidadBO oportunidadBD = new OportunidadBO(idOportunidad, Usuario, _integraDBContext);
                            if (oportunidadBD == null)
                                throw new Exception("No existe oportunidad!");

                            try
                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }

                            OportunidadDTO oportunidad = new OportunidadDTO();

                            oportunidad.Id = oportunidadBD.Id;
                            oportunidad.IdCentroCosto = oportunidadBD.IdCentroCosto.Value;
                            oportunidad.IdPersonalAsignado = oportunidadBD.IdPersonalAsignado;
                            oportunidad.IdTipoDato = oportunidadBD.IdTipoDato;
                            oportunidad.IdFaseOportunidad = oportunidadBD.IdFaseOportunidad;
                            oportunidad.IdOrigen = oportunidadBD.IdOrigen;
                            oportunidad.IdAlumno = oportunidadBD.IdAlumno;
                            oportunidad.UltimoComentario = oportunidadBD.UltimoComentario;
                            oportunidad.IdActividadDetalleUltima = oportunidadBD.IdActividadDetalleUltima;
                            oportunidad.IdActividadCabeceraUltima = oportunidadBD.IdActividadCabeceraUltima;
                            oportunidad.IdEstadoActividadDetalleUltimoEstado = oportunidadBD.IdEstadoActividadDetalleUltimoEstado;
                            oportunidad.UltimaFechaProgramada = oportunidadBD.UltimaFechaProgramada.ToString();
                            oportunidad.IdEstadoOportunidad = oportunidadBD.IdEstadoOportunidad;
                            oportunidad.IdEstadoOcurrenciaUltimo = oportunidadBD.IdEstadoOcurrenciaUltimo;
                            oportunidad.IdFaseOportunidadMaxima = oportunidadBD.IdFaseOportunidadMaxima;
                            oportunidad.IdFaseOportunidadInicial = oportunidadBD.IdFaseOportunidadInicial;
                            oportunidad.IdCategoriaOrigen = oportunidadBD.IdCategoriaOrigen;
                            oportunidad.IdConjuntoAnuncio = oportunidadBD.IdConjuntoAnuncio;
                            oportunidad.IdCampaniaScoring = oportunidadBD.IdCampaniaScoring;
                            oportunidad.IdFaseOportunidadIp = oportunidadBD.IdFaseOportunidadIp;
                            oportunidad.IdFaseOportunidadIc = oportunidadBD.IdFaseOportunidadIc;
                            oportunidad.FechaEnvioFaseOportunidadPf = oportunidadBD.FechaEnvioFaseOportunidadPf;
                            oportunidad.FechaPagoFaseOportunidadPf = oportunidadBD.FechaPagoFaseOportunidadPf;
                            oportunidad.FechaPagoFaseOportunidadIc = oportunidadBD.FechaPagoFaseOportunidadIc;
                            oportunidad.FechaRegistroCampania = oportunidadBD.FechaRegistroCampania;
                            oportunidad.IdFaseOportunidadPortal = oportunidadBD.IdFaseOportunidadPortal;
                            oportunidad.IdFaseOportunidadPf = oportunidadBD.IdFaseOportunidadPf;
                            oportunidad.CodigoPagoIc = oportunidadBD.CodigoPagoIc;
                            oportunidad.FlagVentaCruzada = oportunidadBD.IdTiempoCapacitacion;
                            oportunidad.IdTiempoCapacitacion = oportunidadBD.IdTiempoCapacitacion;
                            oportunidad.IdTiempoCapacitacionValidacion = oportunidadBD.IdTiempoCapacitacionValidacion;
                            oportunidad.IdSubCategoriaDato = oportunidadBD.IdSubCategoriaDato;
                            oportunidad.IdInteraccionFormulario = oportunidadBD.IdInteraccionFormulario;
                            oportunidad.UrlOrigen = oportunidadBD.UrlOrigen;
                            oportunidad.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                            oportunidad.IdPersonalAreaTrabajo = oportunidadBD.IdPersonalAreaTrabajo;
                            if (oportunidadBD.FechaPaso2 != null)
                            {
                                oportunidad.FechaPaso2 = oportunidadBD.FechaPaso2.Value;
                            }
                            if (oportunidadBD.Paso2 != null)
                            {
                                oportunidad.Paso2 = oportunidadBD.Paso2.Value;
                            }
                            if (oportunidadBD.CodMailing != null)
                            {
                                oportunidad.CodMailing = oportunidadBD.CodMailing;
                            }
                            oportunidad.IdPagina = oportunidadBD.IdPagina;

                            //Finalizar Actividad
                            oportunidadBD.ActividadAntigua = new ActividadDetalleBO();
                            oportunidadBD.ActividadAntigua.Id = oportunidadBD.IdActividadDetalleUltima;
                            oportunidadBD.ActividadAntigua.Comentario = "Cerrado OD";
                            oportunidadBD.ActividadAntigua.IdOcurrencia = _repOcurrencia.ObtenerOcurrenciaPorNombre("Cerrado Fase OD"); //"B42B5A91-ADB4-C47A-9557-08D30721ED66";// 3. No le interesa en este momento, pero le interesa para los próximos meses (RN2)
                            oportunidadBD.ActividadAntigua.IdOcurrenciaActividad = null;
                            oportunidadBD.ActividadAntigua.IdAlumno = oportunidadBD.IdAlumno;
                            oportunidadBD.ActividadAntigua.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                            oportunidadBD.ActividadAntigua.IdOportunidad = oportunidadBD.Id;
                            oportunidadBD.ActividadAntigua.IdCentralLlamada = 0;
                            oportunidadBD.ActividadAntigua.IdActividadCabecera = oportunidadBD.IdActividadCabeceraUltima;
                            oportunidadBD.FinalizarActividad(false, oportunidad);

                            _repOportunidad.Update(oportunidadBD);

                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadOD", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return BadRequest(e.Message);
                    }

                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadOD", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase E
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool  </returns>
        [Route("[Action]/{Usuario}")]
        [HttpPost]
        public ActionResult CerrarOportunidadE([FromBody] int[] IdOportunidades, string Usuario)
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                OportunidadRemarketingAgendaRepositorio _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

                foreach (int idOportunidad in IdOportunidades)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            OportunidadBO oportunidadBD = new OportunidadBO(idOportunidad, Usuario, _integraDBContext);
                            if (oportunidadBD == null)
                                throw new Exception("No existe oportunidad!");

                            try
                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }

                            OportunidadDTO oportunidad = new OportunidadDTO();

                            oportunidad.Id = oportunidadBD.Id;
                            oportunidad.IdCentroCosto = oportunidadBD.IdCentroCosto.Value;
                            oportunidad.IdPersonalAsignado = oportunidadBD.IdPersonalAsignado;
                            oportunidad.IdTipoDato = oportunidadBD.IdTipoDato;
                            oportunidad.IdFaseOportunidad = oportunidadBD.IdFaseOportunidad;
                            oportunidad.IdOrigen = oportunidadBD.IdOrigen;
                            oportunidad.IdAlumno = oportunidadBD.IdAlumno;
                            oportunidad.UltimoComentario = oportunidadBD.UltimoComentario;
                            oportunidad.IdActividadDetalleUltima = oportunidadBD.IdActividadDetalleUltima;
                            oportunidad.IdActividadCabeceraUltima = oportunidadBD.IdActividadCabeceraUltima;
                            oportunidad.IdEstadoActividadDetalleUltimoEstado = oportunidadBD.IdEstadoActividadDetalleUltimoEstado;
                            oportunidad.UltimaFechaProgramada = oportunidadBD.UltimaFechaProgramada.ToString();
                            oportunidad.IdEstadoOportunidad = oportunidadBD.IdEstadoOportunidad;
                            oportunidad.IdEstadoOcurrenciaUltimo = oportunidadBD.IdEstadoOcurrenciaUltimo;
                            oportunidad.IdFaseOportunidadMaxima = oportunidadBD.IdFaseOportunidadMaxima;
                            oportunidad.IdFaseOportunidadInicial = oportunidadBD.IdFaseOportunidadInicial;
                            oportunidad.IdCategoriaOrigen = oportunidadBD.IdCategoriaOrigen;
                            oportunidad.IdConjuntoAnuncio = oportunidadBD.IdConjuntoAnuncio;
                            oportunidad.IdCampaniaScoring = oportunidadBD.IdCampaniaScoring;
                            oportunidad.IdFaseOportunidadIp = oportunidadBD.IdFaseOportunidadIp;
                            oportunidad.IdFaseOportunidadIc = oportunidadBD.IdFaseOportunidadIc;
                            oportunidad.FechaEnvioFaseOportunidadPf = oportunidadBD.FechaEnvioFaseOportunidadPf;
                            oportunidad.FechaPagoFaseOportunidadPf = oportunidadBD.FechaPagoFaseOportunidadPf;
                            oportunidad.FechaPagoFaseOportunidadIc = oportunidadBD.FechaPagoFaseOportunidadIc;
                            oportunidad.FechaRegistroCampania = oportunidadBD.FechaRegistroCampania;
                            oportunidad.IdFaseOportunidadPortal = oportunidadBD.IdFaseOportunidadPortal;
                            oportunidad.IdFaseOportunidadPf = oportunidadBD.IdFaseOportunidadPf;
                            oportunidad.CodigoPagoIc = oportunidadBD.CodigoPagoIc;
                            oportunidad.FlagVentaCruzada = oportunidadBD.IdTiempoCapacitacion;
                            oportunidad.IdTiempoCapacitacion = oportunidadBD.IdTiempoCapacitacion;
                            oportunidad.IdTiempoCapacitacionValidacion = oportunidadBD.IdTiempoCapacitacionValidacion;
                            oportunidad.IdSubCategoriaDato = oportunidadBD.IdSubCategoriaDato;
                            oportunidad.IdInteraccionFormulario = oportunidadBD.IdInteraccionFormulario;
                            oportunidad.UrlOrigen = oportunidadBD.UrlOrigen;
                            oportunidad.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                            oportunidad.IdPersonalAreaTrabajo = oportunidadBD.IdPersonalAreaTrabajo;
                            if (oportunidadBD.FechaPaso2 != null)
                            {
                                oportunidad.FechaPaso2 = oportunidadBD.FechaPaso2.Value;
                            }
                            if (oportunidadBD.Paso2 != null)
                            {
                                oportunidad.Paso2 = oportunidadBD.Paso2.Value;
                            }
                            if (oportunidadBD.CodMailing != null)
                            {
                                oportunidad.CodMailing = oportunidadBD.CodMailing;
                            }
                            oportunidad.IdPagina = oportunidadBD.IdPagina;

                            //Finalizar Actividad
                            oportunidadBD.ActividadAntigua = new ActividadDetalleBO();

                            oportunidadBD.ActividadAntigua.Id = oportunidadBD.IdActividadDetalleUltima;
                            oportunidadBD.ActividadAntigua.Comentario = "Cerrado Fase E";
                            oportunidadBD.ActividadAntigua.IdOcurrencia = _repOcurrencia.ObtenerOcurrenciaPorNombre("67. Número no le pertenece (E)");
                            oportunidadBD.ActividadAntigua.IdOcurrenciaActividad = null;
                            oportunidadBD.ActividadAntigua.IdAlumno = oportunidadBD.IdAlumno;
                            oportunidadBD.ActividadAntigua.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                            oportunidadBD.ActividadAntigua.IdOportunidad = oportunidadBD.Id;
                            oportunidadBD.ActividadAntigua.IdCentralLlamada = 0;
                            oportunidadBD.ActividadAntigua.IdActividadCabecera = oportunidadBD.IdActividadCabeceraUltima;
                            oportunidadBD.FinalizarActividad(false, oportunidad);

                            _repOportunidad.Update(oportunidadBD);

                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadE", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return BadRequest(e.Message);
                    }

                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadE", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase OM
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool  </returns>
        [Route("[Action]/{Usuario}")]
        [HttpPost]
        public ActionResult CerrarOportunidadOM([FromBody] int[] IdOportunidades, string Usuario)
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                OportunidadRemarketingAgendaRepositorio _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

                foreach (int idOportunidad in IdOportunidades)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            OportunidadBO oportunidadBD = new OportunidadBO(idOportunidad, Usuario, _integraDBContext);
                            if (oportunidadBD == null)
                                throw new Exception("No existe oportunidad!");

                            try
                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }

                            OportunidadDTO oportunidad = new OportunidadDTO();

                            oportunidad.Id = oportunidadBD.Id;
                            oportunidad.IdCentroCosto = oportunidadBD.IdCentroCosto.Value;
                            oportunidad.IdPersonalAsignado = oportunidadBD.IdPersonalAsignado;
                            oportunidad.IdTipoDato = oportunidadBD.IdTipoDato;
                            oportunidad.IdFaseOportunidad = oportunidadBD.IdFaseOportunidad;
                            oportunidad.IdOrigen = oportunidadBD.IdOrigen;
                            oportunidad.IdAlumno = oportunidadBD.IdAlumno;
                            oportunidad.UltimoComentario = oportunidadBD.UltimoComentario;
                            oportunidad.IdActividadDetalleUltima = oportunidadBD.IdActividadDetalleUltima;
                            oportunidad.IdActividadCabeceraUltima = oportunidadBD.IdActividadCabeceraUltima;
                            oportunidad.IdEstadoActividadDetalleUltimoEstado = oportunidadBD.IdEstadoActividadDetalleUltimoEstado;
                            oportunidad.UltimaFechaProgramada = oportunidadBD.UltimaFechaProgramada.ToString();
                            oportunidad.IdEstadoOportunidad = oportunidadBD.IdEstadoOportunidad;
                            oportunidad.IdEstadoOcurrenciaUltimo = oportunidadBD.IdEstadoOcurrenciaUltimo;
                            oportunidad.IdFaseOportunidadMaxima = oportunidadBD.IdFaseOportunidadMaxima;
                            oportunidad.IdFaseOportunidadInicial = oportunidadBD.IdFaseOportunidadInicial;
                            oportunidad.IdCategoriaOrigen = oportunidadBD.IdCategoriaOrigen;
                            oportunidad.IdConjuntoAnuncio = oportunidadBD.IdConjuntoAnuncio;
                            oportunidad.IdCampaniaScoring = oportunidadBD.IdCampaniaScoring;
                            oportunidad.IdFaseOportunidadIp = oportunidadBD.IdFaseOportunidadIp;
                            oportunidad.IdFaseOportunidadIc = oportunidadBD.IdFaseOportunidadIc;
                            oportunidad.FechaEnvioFaseOportunidadPf = oportunidadBD.FechaEnvioFaseOportunidadPf;
                            oportunidad.FechaPagoFaseOportunidadPf = oportunidadBD.FechaPagoFaseOportunidadPf;
                            oportunidad.FechaPagoFaseOportunidadIc = oportunidadBD.FechaPagoFaseOportunidadIc;
                            oportunidad.FechaRegistroCampania = oportunidadBD.FechaRegistroCampania;
                            oportunidad.IdFaseOportunidadPortal = oportunidadBD.IdFaseOportunidadPortal;
                            oportunidad.IdFaseOportunidadPf = oportunidadBD.IdFaseOportunidadPf;
                            oportunidad.CodigoPagoIc = oportunidadBD.CodigoPagoIc;
                            oportunidad.FlagVentaCruzada = oportunidadBD.IdTiempoCapacitacion;
                            oportunidad.IdTiempoCapacitacion = oportunidadBD.IdTiempoCapacitacion;
                            oportunidad.IdTiempoCapacitacionValidacion = oportunidadBD.IdTiempoCapacitacionValidacion;
                            oportunidad.IdSubCategoriaDato = oportunidadBD.IdSubCategoriaDato;
                            oportunidad.IdInteraccionFormulario = oportunidadBD.IdInteraccionFormulario;
                            oportunidad.UrlOrigen = oportunidadBD.UrlOrigen;
                            oportunidad.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                            oportunidad.IdPersonalAreaTrabajo = oportunidadBD.IdPersonalAreaTrabajo;
                            if (oportunidadBD.FechaPaso2 != null)
                            {
                                oportunidad.FechaPaso2 = oportunidadBD.FechaPaso2.Value;
                            }
                            if (oportunidadBD.Paso2 != null)
                            {
                                oportunidad.Paso2 = oportunidadBD.Paso2.Value;
                            }
                            if (oportunidadBD.CodMailing != null)
                            {
                                oportunidad.CodMailing = oportunidadBD.CodMailing;
                            }
                            oportunidad.IdPagina = oportunidadBD.IdPagina;

                            //Finalizar Actividad
                            oportunidadBD.ActividadAntigua = new ActividadDetalleBO();

                            oportunidadBD.ActividadAntigua.Id = oportunidadBD.IdActividadDetalleUltima;
                            oportunidadBD.ActividadAntigua.Comentario = "Cerrado Fase OM";
                            oportunidadBD.ActividadAntigua.IdOcurrencia = _repOcurrencia.ObtenerOcurrenciaPorNombre("Cerrado Fase OM");
                            oportunidadBD.ActividadAntigua.IdOcurrenciaActividad = null;
                            oportunidadBD.ActividadAntigua.IdAlumno = oportunidadBD.IdAlumno;
                            oportunidadBD.ActividadAntigua.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                            oportunidadBD.ActividadAntigua.IdOportunidad = oportunidadBD.Id;
                            oportunidadBD.ActividadAntigua.IdCentralLlamada = 0;
                            oportunidadBD.ActividadAntigua.IdActividadCabecera = oportunidadBD.IdActividadCabeceraUltima;
                            oportunidadBD.FinalizarActividad(false, oportunidad);

                            _repOportunidad.Update(oportunidadBD);

                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadOM", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return BadRequest(e.Message);
                    }

                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadOM", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase RN5
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool  </returns>
        [Route("[Action]/{Usuario}")]
        [HttpPost]
        public ActionResult CerrarOportunidadRN5([FromBody] int[] IdOportunidades, string Usuario)
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                OportunidadRemarketingAgendaRepositorio _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

                foreach (var idOportunidad in IdOportunidades)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            OportunidadBO oportunidadBD = new OportunidadBO(idOportunidad, Usuario, _integraDBContext);
                            if (oportunidadBD == null)
                                throw new Exception("No existe oportunidad!");

                            try
                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }

                            OportunidadDTO oportunidad = new OportunidadDTO();

                            oportunidad.Id = oportunidadBD.Id;
                            oportunidad.IdCentroCosto = oportunidadBD.IdCentroCosto.Value;
                            oportunidad.IdPersonalAsignado = oportunidadBD.IdPersonalAsignado;
                            oportunidad.IdTipoDato = oportunidadBD.IdTipoDato;
                            oportunidad.IdFaseOportunidad = oportunidadBD.IdFaseOportunidad;
                            oportunidad.IdOrigen = oportunidadBD.IdOrigen;
                            oportunidad.IdAlumno = oportunidadBD.IdAlumno;
                            oportunidad.UltimoComentario = oportunidadBD.UltimoComentario;
                            oportunidad.IdActividadDetalleUltima = oportunidadBD.IdActividadDetalleUltima;
                            oportunidad.IdActividadCabeceraUltima = oportunidadBD.IdActividadCabeceraUltima;
                            oportunidad.IdEstadoActividadDetalleUltimoEstado = oportunidadBD.IdEstadoActividadDetalleUltimoEstado;
                            oportunidad.UltimaFechaProgramada = oportunidadBD.UltimaFechaProgramada.ToString();
                            oportunidad.IdEstadoOportunidad = oportunidadBD.IdEstadoOportunidad;
                            oportunidad.IdEstadoOcurrenciaUltimo = oportunidadBD.IdEstadoOcurrenciaUltimo;
                            oportunidad.IdFaseOportunidadMaxima = oportunidadBD.IdFaseOportunidadMaxima;
                            oportunidad.IdFaseOportunidadInicial = oportunidadBD.IdFaseOportunidadInicial;
                            oportunidad.IdCategoriaOrigen = oportunidadBD.IdCategoriaOrigen;
                            oportunidad.IdConjuntoAnuncio = oportunidadBD.IdConjuntoAnuncio;
                            oportunidad.IdCampaniaScoring = oportunidadBD.IdCampaniaScoring;
                            oportunidad.IdFaseOportunidadIp = oportunidadBD.IdFaseOportunidadIp;
                            oportunidad.IdFaseOportunidadIc = oportunidadBD.IdFaseOportunidadIc;
                            oportunidad.FechaEnvioFaseOportunidadPf = oportunidadBD.FechaEnvioFaseOportunidadPf;
                            oportunidad.FechaPagoFaseOportunidadPf = oportunidadBD.FechaPagoFaseOportunidadPf;
                            oportunidad.FechaPagoFaseOportunidadIc = oportunidadBD.FechaPagoFaseOportunidadIc;
                            oportunidad.FechaRegistroCampania = oportunidadBD.FechaRegistroCampania;
                            oportunidad.IdFaseOportunidadPortal = oportunidadBD.IdFaseOportunidadPortal;
                            oportunidad.IdFaseOportunidadPf = oportunidadBD.IdFaseOportunidadPf;
                            oportunidad.CodigoPagoIc = oportunidadBD.CodigoPagoIc;
                            oportunidad.FlagVentaCruzada = oportunidadBD.IdTiempoCapacitacion;
                            oportunidad.IdTiempoCapacitacion = oportunidadBD.IdTiempoCapacitacion;
                            oportunidad.IdTiempoCapacitacionValidacion = oportunidadBD.IdTiempoCapacitacionValidacion;
                            oportunidad.IdSubCategoriaDato = oportunidadBD.IdSubCategoriaDato;
                            oportunidad.IdInteraccionFormulario = oportunidadBD.IdInteraccionFormulario;
                            oportunidad.UrlOrigen = oportunidadBD.UrlOrigen;
                            oportunidad.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                            oportunidad.IdPersonalAreaTrabajo = oportunidadBD.IdPersonalAreaTrabajo;
                            if (oportunidadBD.FechaPaso2 != null)
                            {
                                oportunidad.FechaPaso2 = oportunidadBD.FechaPaso2.Value;
                            }
                            if (oportunidadBD.Paso2 != null)
                            {
                                oportunidad.Paso2 = oportunidadBD.Paso2.Value;
                            }
                            if (oportunidadBD.CodMailing != null)
                            {
                                oportunidad.CodMailing = oportunidadBD.CodMailing;
                            }
                            oportunidad.IdPagina = oportunidadBD.IdPagina;

                            //Finalizar Actividad
                            oportunidadBD.ActividadAntigua = new ActividadDetalleBO();

                            oportunidadBD.ActividadAntigua.Id = oportunidadBD.IdActividadDetalleUltima;
                            oportunidadBD.ActividadAntigua.Comentario = "Cerrado Fase RN5";
                            oportunidadBD.ActividadAntigua.IdOcurrencia = _repOcurrencia.ObtenerOcurrenciaPorNombre("130. Cliente no responde correo (RN5)");
                            oportunidadBD.ActividadAntigua.IdOcurrenciaActividad = null;
                            oportunidadBD.ActividadAntigua.IdAlumno = oportunidadBD.IdAlumno;
                            oportunidadBD.ActividadAntigua.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                            oportunidadBD.ActividadAntigua.IdOportunidad = oportunidadBD.Id;
                            oportunidadBD.ActividadAntigua.IdCentralLlamada = 0;
                            oportunidadBD.ActividadAntigua.IdActividadCabecera = oportunidadBD.IdActividadCabeceraUltima;
                            oportunidadBD.FinalizarActividad(false, oportunidad);

                            _repOportunidad.Update(oportunidadBD);

                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadRN5", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return BadRequest(e.Message);
                    }

                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadRN5", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase BIC
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool  </returns>
        [Route("[action]/{Usuario}")]
        [HttpPost]
        public ActionResult CerrarOportunidadBic([FromBody] int[] IdOportunidades, string Usuario)
        {
            try
            {
                integraDBContext context = new integraDBContext();
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(context);
                OportunidadRemarketingAgendaRepositorio _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(context);
                ActividadDetalleRepositorio _repActividadDetalle = new ActividadDetalleRepositorio(context);

                foreach (var idOportunidad in IdOportunidades)
                {
                    try
                    {
                        OportunidadBO oportunidad = new OportunidadBO(idOportunidad, Usuario, context);
                        if (oportunidad == null)
                            throw new Exception("No existe oportunidad!");

                        try
                        {
                            _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                        }
                        catch (Exception ex)
                        {
                        }

                        ActividadDetalleBO actividadDetalleDTO = _repActividadDetalle.FirstById(oportunidad.IdActividadDetalleUltima);
                        actividadDetalleDTO.Comentario = "Cerrado Reporte BIC";
                        actividadDetalleDTO.IdOcurrencia = ValorEstatico.IdOcurrenciaCerradoReporteBic;
                        actividadDetalleDTO.IdOcurrenciaActividad = null;
                        actividadDetalleDTO.IdAlumno = oportunidad.IdAlumno;
                        actividadDetalleDTO.IdOportunidad = idOportunidad;
                        actividadDetalleDTO.IdEstadoActividadDetalle = 0;//
                        actividadDetalleDTO.IdActividadCabecera = oportunidad.IdActividadCabeceraUltima;
                        oportunidad.ActividadAntigua = actividadDetalleDTO;


                        OportunidadDTO oportunidadDTO = new OportunidadDTO();
                        oportunidadDTO.IdEstadoOportunidad = oportunidad.IdEstadoOportunidad;
                        oportunidadDTO.IdFaseOportunidad = oportunidad.IdFaseOportunidad;
                        oportunidadDTO.IdFaseOportunidadIc = oportunidad.IdFaseOportunidadIc;
                        oportunidadDTO.IdFaseOportunidadIp = oportunidad.IdFaseOportunidadIp;
                        oportunidadDTO.IdFaseOportunidadPf = oportunidad.IdFaseOportunidadPf;
                        oportunidadDTO.FechaEnvioFaseOportunidadPf = oportunidad.FechaEnvioFaseOportunidadPf;
                        oportunidadDTO.FechaPagoFaseOportunidadIc = oportunidad.FechaPagoFaseOportunidadIc;
                        oportunidadDTO.FechaPagoFaseOportunidadPf = oportunidad.FechaPagoFaseOportunidadPf;
                        //oportunidadDTO.FasesActivas = Oportunidad.fasesac;
                        oportunidadDTO.CodigoPagoIc = oportunidad.CodigoPagoIc;

                        oportunidad.FinalizarActividad(false, oportunidadDTO);

                        using (TransactionScope scope = new TransactionScope())
                        {
                            _repOportunidad.Update(oportunidad);
                            scope.Complete();
                        }

                    }
                    catch (Exception e)
                    {
                        LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadBic", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return BadRequest(e.Message);
                    }

                }

                return Ok(true);
            }
            catch (Exception e)
            {
                LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadBic", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase BRM1
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool  </returns>
        [Route("[Action]/{Usuario}")]
        [HttpPost]
        public ActionResult CerrarOportunidadBRM1([FromBody] int[] IdOportunidades, string Usuario)
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                OportunidadRemarketingAgendaRepositorio _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

                foreach (var idOportunidad in IdOportunidades)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            OportunidadBO oportunidadBD = new OportunidadBO(idOportunidad, Usuario, _integraDBContext);
                            if (oportunidadBD == null)
                                throw new Exception("No existe oportunidad!");

                            try
                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }

                            OportunidadDTO oportunidad = new OportunidadDTO();

                            oportunidad.Id = oportunidadBD.Id;
                            oportunidad.IdCentroCosto = oportunidadBD.IdCentroCosto.Value;
                            oportunidad.IdPersonalAsignado = oportunidadBD.IdPersonalAsignado;
                            oportunidad.IdTipoDato = oportunidadBD.IdTipoDato;
                            oportunidad.IdFaseOportunidad = oportunidadBD.IdFaseOportunidad;
                            oportunidad.IdOrigen = oportunidadBD.IdOrigen;
                            oportunidad.IdAlumno = oportunidadBD.IdAlumno;
                            oportunidad.UltimoComentario = oportunidadBD.UltimoComentario;
                            oportunidad.IdActividadDetalleUltima = oportunidadBD.IdActividadDetalleUltima;
                            oportunidad.IdActividadCabeceraUltima = oportunidadBD.IdActividadCabeceraUltima;
                            oportunidad.IdEstadoActividadDetalleUltimoEstado = oportunidadBD.IdEstadoActividadDetalleUltimoEstado;
                            oportunidad.UltimaFechaProgramada = oportunidadBD.UltimaFechaProgramada.ToString();
                            oportunidad.IdEstadoOportunidad = oportunidadBD.IdEstadoOportunidad;
                            oportunidad.IdEstadoOcurrenciaUltimo = oportunidadBD.IdEstadoOcurrenciaUltimo;
                            oportunidad.IdFaseOportunidadMaxima = oportunidadBD.IdFaseOportunidadMaxima;
                            oportunidad.IdFaseOportunidadInicial = oportunidadBD.IdFaseOportunidadInicial;
                            oportunidad.IdCategoriaOrigen = oportunidadBD.IdCategoriaOrigen;
                            oportunidad.IdConjuntoAnuncio = oportunidadBD.IdConjuntoAnuncio;
                            oportunidad.IdCampaniaScoring = oportunidadBD.IdCampaniaScoring;
                            oportunidad.IdFaseOportunidadIp = oportunidadBD.IdFaseOportunidadIp;
                            oportunidad.IdFaseOportunidadIc = oportunidadBD.IdFaseOportunidadIc;
                            oportunidad.FechaEnvioFaseOportunidadPf = oportunidadBD.FechaEnvioFaseOportunidadPf;
                            oportunidad.FechaPagoFaseOportunidadPf = oportunidadBD.FechaPagoFaseOportunidadPf;
                            oportunidad.FechaPagoFaseOportunidadIc = oportunidadBD.FechaPagoFaseOportunidadIc;
                            oportunidad.FechaRegistroCampania = oportunidadBD.FechaRegistroCampania;
                            oportunidad.IdFaseOportunidadPortal = oportunidadBD.IdFaseOportunidadPortal;
                            oportunidad.IdFaseOportunidadPf = oportunidadBD.IdFaseOportunidadPf;
                            oportunidad.CodigoPagoIc = oportunidadBD.CodigoPagoIc;
                            oportunidad.FlagVentaCruzada = oportunidadBD.IdTiempoCapacitacion;
                            oportunidad.IdTiempoCapacitacion = oportunidadBD.IdTiempoCapacitacion;
                            oportunidad.IdTiempoCapacitacionValidacion = oportunidadBD.IdTiempoCapacitacionValidacion;
                            oportunidad.IdSubCategoriaDato = oportunidadBD.IdSubCategoriaDato;
                            oportunidad.IdInteraccionFormulario = oportunidadBD.IdInteraccionFormulario;
                            oportunidad.UrlOrigen = oportunidadBD.UrlOrigen;
                            oportunidad.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                            oportunidad.IdPersonalAreaTrabajo = oportunidadBD.IdPersonalAreaTrabajo;
                            if (oportunidadBD.FechaPaso2 != null)
                            {
                                oportunidad.FechaPaso2 = oportunidadBD.FechaPaso2.Value;
                            }
                            if (oportunidadBD.Paso2 != null)
                            {
                                oportunidad.Paso2 = oportunidadBD.Paso2.Value;
                            }
                            if (oportunidadBD.CodMailing != null)
                            {
                                oportunidad.CodMailing = oportunidadBD.CodMailing;
                            }
                            oportunidad.IdPagina = oportunidadBD.IdPagina;

                            //Finalizar Actividad
                            oportunidadBD.ActividadAntigua = new ActividadDetalleBO();

                            oportunidadBD.ActividadAntigua.Id = oportunidadBD.IdActividadDetalleUltima;
                            oportunidadBD.ActividadAntigua.Comentario = "Cerrado Fase BRM1";
                            oportunidadBD.ActividadAntigua.IdOcurrencia = 327;
                            oportunidadBD.ActividadAntigua.IdOcurrenciaActividad = null;
                            oportunidadBD.ActividadAntigua.IdAlumno = oportunidadBD.IdAlumno;
                            oportunidadBD.ActividadAntigua.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                            oportunidadBD.ActividadAntigua.IdOportunidad = oportunidadBD.Id;
                            oportunidadBD.ActividadAntigua.IdCentralLlamada = 0;
                            oportunidadBD.ActividadAntigua.IdActividadCabecera = oportunidadBD.IdActividadCabeceraUltima;
                            oportunidadBD.FinalizarActividad(false, oportunidad);

                            _repOportunidad.Update(oportunidadBD);

                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadBRM1", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return BadRequest(e.Message);
                    }

                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadBRM1", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return BadRequest(ex.Message);
            }
        }
        //carlos comentado
        //[Route("[action]/{parametro}")]
        //[HttpPost]
        public string EncriptarString(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int LongNombres = (parametro.Length * 2) / 3;
                if (LongNombres > 0)
                {
                    respuesta = new string('x', LongNombres) + parametro.Remove(0, LongNombres);
                }
            }
            return respuesta;
        }

        /// Autor: _ _ _ _ _ _ _ .
        /// Fecha: 06/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Encripta correos
        /// </summary>
        /// <returns> string </returns>
        public string EncriptarStringCorreo(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int posicion = parametro.IndexOf("@");

                if (posicion > 0)
                {
                    respuesta = new string('x', posicion) + parametro.Remove(0, posicion);
                }
            }
            return respuesta;
        }
        public string EncriptarStringNumero(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int longitud = parametro.Length;
                if (longitud > 4)
                {
                    int posicion = longitud - 4;
                    //respuesta = parametro.Remove(0, posicion) + new string('x', 4);
                    respuesta = parametro.Remove(posicion, 4) + new string('x', 4);
                }
            }
            return respuesta;
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 06/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Oportunidad en NS
        /// </summary>
        /// <returns> Confirmación de inserción : Bool </returns>
        [Route("[Action]/{Usuario}")]
        [HttpPost]
        public ActionResult CerrarOportunidadNS([FromBody] int[] IdOportunidades, string Usuario)
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                OportunidadRemarketingAgendaRepositorio _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

                foreach (int idOportunidad in IdOportunidades)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            OportunidadBO oportunidadBD = new OportunidadBO(idOportunidad, Usuario, _integraDBContext);
                            if (oportunidadBD == null)
                                throw new Exception("No existe oportunidad!");

                            try
                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }

                            OportunidadDTO oportunidad = new OportunidadDTO();

                            oportunidad.Id = oportunidadBD.Id;
                            oportunidad.IdCentroCosto = oportunidadBD.IdCentroCosto.Value;
                            oportunidad.IdPersonalAsignado = oportunidadBD.IdPersonalAsignado;
                            oportunidad.IdTipoDato = oportunidadBD.IdTipoDato;
                            oportunidad.IdFaseOportunidad = oportunidadBD.IdFaseOportunidad;
                            oportunidad.IdOrigen = oportunidadBD.IdOrigen;
                            oportunidad.IdAlumno = oportunidadBD.IdAlumno;
                            oportunidad.UltimoComentario = oportunidadBD.UltimoComentario;
                            oportunidad.IdActividadDetalleUltima = oportunidadBD.IdActividadDetalleUltima;
                            oportunidad.IdActividadCabeceraUltima = oportunidadBD.IdActividadCabeceraUltima;
                            oportunidad.IdEstadoActividadDetalleUltimoEstado = oportunidadBD.IdEstadoActividadDetalleUltimoEstado;
                            oportunidad.UltimaFechaProgramada = oportunidadBD.UltimaFechaProgramada.ToString();
                            oportunidad.IdEstadoOportunidad = oportunidadBD.IdEstadoOportunidad;
                            oportunidad.IdEstadoOcurrenciaUltimo = oportunidadBD.IdEstadoOcurrenciaUltimo;
                            oportunidad.IdFaseOportunidadMaxima = oportunidadBD.IdFaseOportunidadMaxima;
                            oportunidad.IdFaseOportunidadInicial = oportunidadBD.IdFaseOportunidadInicial;
                            oportunidad.IdCategoriaOrigen = oportunidadBD.IdCategoriaOrigen;
                            oportunidad.IdConjuntoAnuncio = oportunidadBD.IdConjuntoAnuncio;
                            oportunidad.IdCampaniaScoring = oportunidadBD.IdCampaniaScoring;
                            oportunidad.IdFaseOportunidadIp = oportunidadBD.IdFaseOportunidadIp;
                            oportunidad.IdFaseOportunidadIc = oportunidadBD.IdFaseOportunidadIc;
                            oportunidad.FechaEnvioFaseOportunidadPf = oportunidadBD.FechaEnvioFaseOportunidadPf;
                            oportunidad.FechaPagoFaseOportunidadPf = oportunidadBD.FechaPagoFaseOportunidadPf;
                            oportunidad.FechaPagoFaseOportunidadIc = oportunidadBD.FechaPagoFaseOportunidadIc;
                            oportunidad.FechaRegistroCampania = oportunidadBD.FechaRegistroCampania;
                            oportunidad.IdFaseOportunidadPortal = oportunidadBD.IdFaseOportunidadPortal;
                            oportunidad.IdFaseOportunidadPf = oportunidadBD.IdFaseOportunidadPf;
                            oportunidad.CodigoPagoIc = oportunidadBD.CodigoPagoIc;
                            oportunidad.FlagVentaCruzada = oportunidadBD.IdTiempoCapacitacion;
                            oportunidad.IdTiempoCapacitacion = oportunidadBD.IdTiempoCapacitacion;
                            oportunidad.IdTiempoCapacitacionValidacion = oportunidadBD.IdTiempoCapacitacionValidacion;
                            oportunidad.IdSubCategoriaDato = oportunidadBD.IdSubCategoriaDato;
                            oportunidad.IdInteraccionFormulario = oportunidadBD.IdInteraccionFormulario;
                            oportunidad.UrlOrigen = oportunidadBD.UrlOrigen;
                            oportunidad.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                            oportunidad.IdPersonalAreaTrabajo = oportunidadBD.IdPersonalAreaTrabajo;
                            if (oportunidadBD.FechaPaso2 != null)
                            {
                                oportunidad.FechaPaso2 = oportunidadBD.FechaPaso2.Value;
                            }
                            if (oportunidadBD.Paso2 != null)
                            {
                                oportunidad.Paso2 = oportunidadBD.Paso2.Value;
                            }
                            if (oportunidadBD.CodMailing != null)
                            {
                                oportunidad.CodMailing = oportunidadBD.CodMailing;
                            }
                            oportunidad.IdPagina = oportunidadBD.IdPagina;

                            //Finalizar Actividad
                            oportunidadBD.ActividadAntigua = new ActividadDetalleBO();
                            oportunidadBD.ActividadAntigua.Id = oportunidadBD.IdActividadDetalleUltima;
                            oportunidadBD.ActividadAntigua.Comentario = "Cerrado NS";

                            oportunidadBD.ActividadAntigua.IdOcurrencia = _repOcurrencia.ObtenerOcurrenciaPorNombre("242. No solicitó información (NS)");
                            oportunidadBD.ActividadAntigua.IdOcurrenciaActividad = null;
                            oportunidadBD.ActividadAntigua.IdAlumno = oportunidadBD.IdAlumno;
                            oportunidadBD.ActividadAntigua.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                            oportunidadBD.ActividadAntigua.IdOportunidad = oportunidadBD.Id;
                            oportunidadBD.ActividadAntigua.IdCentralLlamada = 0;
                            oportunidadBD.ActividadAntigua.IdActividadCabecera = oportunidadBD.IdActividadCabeceraUltima;
                            oportunidadBD.FinalizarActividad(false, oportunidad);

                            _repOportunidad.Update(oportunidadBD);

                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadNS", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return BadRequest(e.Message);
                    }

                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                LogRepositorio _repLog = new LogRepositorio(_integraDBContext);
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadNS", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 27/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Email de Alumno AutoComplete
        /// </summary>
        /// <param name="Filtros"> Filtros de búsqueda </param>
        /// <returns> Si los parámetros de entrada es diferente de null retorna List<FiltroBasicoDTO>, sino retorna List<FiltroDTO> </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult GetEmailAlumnoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
            try
            {
                if (Filtros != null)
                {
                    return Ok(_repAlumno.CargarEmailAlumnoAutoComplete(Filtros["valor"].ToString()));
                }
                else
                {
                    List<FiltroDTO> lista = new List<FiltroDTO>();
                    return Ok(lista);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 27/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Nombre de Alumno AutoComplete
        /// </summary>
        /// <returns> Lista de Id Nombre : List<FiltroDTO> </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult GetNombreAlumnoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
            try
            {
                if (Filtros != null)
                {
                    return Ok(_repAlumno.CargarNombreAlumnoAutoComplete(Filtros["valor"].ToString()));
                }
                else
                {
                    List<FiltroDTO> lista = new List<FiltroDTO>();
                    return Ok(lista);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros de Oportunidades
        /// </summary>
        /// <param name="Filtro"> Filtros de búsqueda </param>
        /// <returns> ResultadoAsignacionManualFiltroTotalDTO </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerOportunidadesV2([FromBody] AsignacionAutomaticaManualOportunidadFiltroGrillaDTO Filtro)
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                OperadorComparacionRepositorio _repOperadorComparacion = new OperadorComparacionRepositorio(_integraDBContext);

                var oportunidadManual = _repOportunidad.ObtenerPorFiltroPaginaManualV2(Filtro.filtro, Filtro.paginador, Filtro.filter, _repOperadorComparacion.ObtenerListado());

                foreach (var item in oportunidadManual.data)
                {
                    item.Email = EncriptarStringCorreo(item.Email);
                }

                return Ok(oportunidadManual);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}