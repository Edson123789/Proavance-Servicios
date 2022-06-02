using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Gestion de Personal/MaestroPersonal
    /// Autor: Luis Huallpa - Brittsel Calluchi - Edgar Serruto
    /// Fecha: 23/03/2021
    /// <summary>
    /// Gestiona la funcionalidad del modulo de maestro personal
    /// </summary>
    [Route("api/MaestroPersonal")]
    public class MaestroPersonalController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PaisRepositorio _repPais;
        private readonly CiudadRepositorio _repCiudad;
        private readonly EstadoCivilRepositorio _repEstado;
        private readonly SexoRepositorio _repSexo;
        private readonly SistemaPensionarioRepositorio _repSistemaPensionario;
        private readonly EntidadSistemaPensionarioRepositorio _repEntidad;
        private readonly TipoDocumentoPersonalRepositorio _repTipoDocumento;
        private readonly MotivoCeseRepositorio _repMotivoCese;
        private readonly PersonalRepositorio _repPersonal;
        private readonly EntidadSeguroSaludRepositorio _repEntidadSeguroSalud;
        private readonly PersonalFormacionRepositorio _repPersonalFormacion;
        private readonly PersonalComputoRepositorio _repPersonalComputo;
        private readonly PersonalIdiomaRepositorio _repPersonalIdioma;
        private readonly PersonalCertificacionRepositorio _repPersonalCertificacion;
        private readonly PersonalExperienciaRepositorio _repPersonalExperiencia;
        private readonly PersonalInformacionMedicaRepositorio _repPersonalInformacionMedica;
        private readonly PersonalHistorialMedicoRepositorio _repPersonalHistorialMedico;
        private readonly PersonalSistemaPensionarioRepositorio _repPersonalSistemaPensionario;
        private readonly PersonalSeguroSaludRepositorio _repPersonalSeguroSalud;
        private readonly PuestoTrabajoRepositorio _repPuestoTrabajo;
        private readonly SedeTrabajoRepositorio _repSedeTrabajo;
        private readonly PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo;
        private readonly DatoContratoPersonalRepositorio _repDatoContratoPersonal;
        private readonly CentroEstudioRepositorio _repCentroEstudio;
        private readonly MandrilEnvioCorreoRepositorio _repMandrilEnvioCorreo;
        private readonly TipoEstudioRepositorio _repTipoEstudio;
        private readonly AreaFormacionRepositorio _repAreaFormacion;
        private readonly GradoEstudioRepositorio _repGradoEstudio;
        private readonly MontoPagoCronogramaRepositorio _repMontoPagoCronograma;
        //private readonly NivelEstudioRepositorio _repNivelEstudio;
        private readonly IdiomaRepositorio _repIdioma;
        private readonly NivelIdiomaRepositorio _repNivelIdioma;
        private readonly EmpresaRepositorio _repEmpresa;
        private readonly AreaTrabajoRepositorio _repAreaTrabajo;
        private readonly CargoRepositorio _repCargo;
        private readonly PersonalLogRepositorio _repPersonalLog;
        private readonly PespecificoRepositorio _repPEspecifico;
        private readonly ParentescoPersonalRepositorio _repParentescoPersonal;
        private readonly TipoSangreRepositorio _repTipoSangre;
        private readonly DatoFamiliarPersonalRepositorio _repDatoFamiliarPersonal;
        private readonly PersonalCeseRepositorio _repPersonalCese;
        private readonly PersonalAccesoTemporalAulaVirtualRepositorio _repPersonalAccesoTemporalAulaVirtual;
        private readonly NivelCompetenciaTecnicaRepositorio _repNivelCompetenciaTecnica;
        private readonly TipoPagoRemuneracionRepositorio _repTipoPagoRemuneracion;
        private readonly EntidadFinancieraRepositorio _repEntidadFinanciera;
        private readonly ContratoEstadoRepositorio _repContratoEstado;
        private readonly PersonalRemuneracionRepositorio _repPersonalRemuneracion;
        private readonly PersonalDireccionRepositorio _repPersonalDireccion;
        private readonly ModuloSistemaPuestoTrabajoRepositorio _moduloPuesto;
        private readonly ModuloSistemaAccesoRepositorio _moduloAcceso;
        private readonly UsuarioRepositorio _usuarioModulo;
        private readonly PersonalPuestoSedeHistoricoRepositorio _repPersonalPuestoSedeHistorico;
        private readonly MotivoInactividadRepositorio _repMotivoInactividad;
        private readonly PersonalMotivoTiempoInactividadRepositorio _repPersonalMotivoTiempoInactividad;
        private readonly AlumnoRepositorio _repAlumno;
        private PersonalAccesoTemporalAulaVirtualBO PersonalAccesoTemporalAulaVirtual;
        private readonly PuestoTrabajoNivelRepositorio _repPuestoTrabajoNivel;
        private readonly TableroComercialCategoriaAsesorRepositorio _repTableroComercialCategoriaAsesor;
        public MaestroPersonalController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repPais = new PaisRepositorio(_integraDBContext);
            _repCiudad = new CiudadRepositorio(_integraDBContext);
            _repEstado = new EstadoCivilRepositorio(_integraDBContext);
            _repSexo = new SexoRepositorio(_integraDBContext);
            _repSistemaPensionario = new SistemaPensionarioRepositorio(_integraDBContext);
            _repEntidad = new EntidadSistemaPensionarioRepositorio(_integraDBContext);
            _repTipoDocumento = new TipoDocumentoPersonalRepositorio(_integraDBContext);
            _repMotivoCese = new MotivoCeseRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repEntidadSeguroSalud = new EntidadSeguroSaludRepositorio(_integraDBContext);
            _repPersonalFormacion = new PersonalFormacionRepositorio(_integraDBContext);
            _repPersonalComputo = new PersonalComputoRepositorio(_integraDBContext);
            _repPersonalIdioma = new PersonalIdiomaRepositorio(_integraDBContext);
            _repPersonalCertificacion = new PersonalCertificacionRepositorio(_integraDBContext);
            _repPersonalExperiencia = new PersonalExperienciaRepositorio(_integraDBContext);
            _repPersonalInformacionMedica = new PersonalInformacionMedicaRepositorio(_integraDBContext);
            _repPersonalHistorialMedico = new PersonalHistorialMedicoRepositorio(_integraDBContext);
            _repPersonalSistemaPensionario = new PersonalSistemaPensionarioRepositorio(_integraDBContext);
            _repPersonalSeguroSalud = new PersonalSeguroSaludRepositorio(_integraDBContext);
            _repPuestoTrabajo = new PuestoTrabajoRepositorio(_integraDBContext);
            _repSedeTrabajo = new SedeTrabajoRepositorio(_integraDBContext);
            _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
            _repDatoContratoPersonal = new DatoContratoPersonalRepositorio(_integraDBContext);
            _repPersonalLog = new PersonalLogRepositorio(_integraDBContext);
            _repCentroEstudio = new CentroEstudioRepositorio(_integraDBContext);
            _repTipoEstudio = new TipoEstudioRepositorio(_integraDBContext);
            _repAreaTrabajo = new AreaTrabajoRepositorio(_integraDBContext);
            _repGradoEstudio = new GradoEstudioRepositorio(_integraDBContext);
            _repPersonalAccesoTemporalAulaVirtual = new PersonalAccesoTemporalAulaVirtualRepositorio(_integraDBContext);
            _repMandrilEnvioCorreo = new MandrilEnvioCorreoRepositorio(_integraDBContext);
            _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(_integraDBContext);
            _repAlumno = new AlumnoRepositorio(_integraDBContext);
            _repIdioma = new IdiomaRepositorio(_integraDBContext);
            _repNivelIdioma = new NivelIdiomaRepositorio(_integraDBContext);
            _repEmpresa = new EmpresaRepositorio(_integraDBContext);
            _repAreaFormacion = new AreaFormacionRepositorio(_integraDBContext);
            _repCargo = new CargoRepositorio(_integraDBContext);
            _repParentescoPersonal = new ParentescoPersonalRepositorio(_integraDBContext);
            _repTipoSangre = new TipoSangreRepositorio(_integraDBContext);
            _repDatoFamiliarPersonal = new DatoFamiliarPersonalRepositorio(_integraDBContext);
            _repPersonalCese = new PersonalCeseRepositorio(_integraDBContext);
            _repNivelCompetenciaTecnica = new NivelCompetenciaTecnicaRepositorio(_integraDBContext);
            _repTipoPagoRemuneracion = new TipoPagoRemuneracionRepositorio(_integraDBContext);
            _repEntidadFinanciera = new EntidadFinancieraRepositorio(_integraDBContext);
            _repContratoEstado = new ContratoEstadoRepositorio(_integraDBContext);
            _repPersonalRemuneracion = new PersonalRemuneracionRepositorio(_integraDBContext);
            _repPersonalDireccion = new PersonalDireccionRepositorio(_integraDBContext);
            _moduloPuesto = new ModuloSistemaPuestoTrabajoRepositorio(_integraDBContext);
            _moduloAcceso = new ModuloSistemaAccesoRepositorio(_integraDBContext);
            _usuarioModulo = new UsuarioRepositorio(_integraDBContext);
            _repPersonalPuestoSedeHistorico = new PersonalPuestoSedeHistoricoRepositorio(_integraDBContext);
            _repMotivoInactividad = new MotivoInactividadRepositorio(_integraDBContext);
            _repPersonalMotivoTiempoInactividad = new PersonalMotivoTiempoInactividadRepositorio(_integraDBContext);
            _repPuestoTrabajoNivel = new PuestoTrabajoNivelRepositorio(_integraDBContext);
            _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
            PersonalAccesoTemporalAulaVirtual = new PersonalAccesoTemporalAulaVirtualBO(_integraDBContext);
            _repTableroComercialCategoriaAsesor = new TableroComercialCategoriaAsesorRepositorio(_integraDBContext);
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de Empresas
        /// </summary>
        /// <returns>Lista de Empresas función AutoComplete</returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ObtenerEmpresaAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    return Ok(_repEmpresa.ObtenerTodoFiltroAutoComplete(Filtros["valor"].ToString()));
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
        /// TipoFuncion: GET
        /// Autor: Luis Huallpa - Britsel Calluchi - Edgar Serruto
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información para selección de combos en módulo
        /// </summary>
        /// <returns> Objeto Agrupado </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var objetoAgrupado = new
                {
                    ListaCiudad = _repCiudad.ObtenerCiudadesPorPais(),
                    ListaPais = _repPais.ObtenerPaisesCombo(),
                    ListaEstadoCivil = _repEstado.GetFiltroIdNombre(),
                    ListaSexo = _repSexo.GetFiltroIdNombre(),
                    ListaSistemaPensionario = _repSistemaPensionario.GetFiltroIdNombre(),
                    ListaEntidad = _repEntidad.GetFiltroIdNombre(),
                    ListaTipoDocumento = _repTipoDocumento.GetFiltroIdNombre(),
                    ListaMotivoCese = _repMotivoCese.GetFiltroIdNombre(),
                    ListaEntidadSeguroSalud = _repEntidadSeguroSalud.ObtenerListaEntidadSeguroSalud(),
                    ListaCentroEstudio = _repCentroEstudio.ObtenerListaParaFiltro(),
                    ListaTipoEstudio = _repTipoEstudio.ObtenerListaParaFiltro(),
                    ListaAreaFormacion = _repAreaFormacion.ObtenerAreaFormacionFiltro(),
                    ListaEstadoEstudio = _repGradoEstudio.ObtenerListaEstadoEstudioParaFiltro(),
                    ListaNivelEstudio = _repNivelCompetenciaTecnica.ObtenerListaParaFiltro(),
                    ListaIdioma = _repIdioma.ObtenerListaParaFiltro(),
                    ListaNivelIdioma = _repNivelIdioma.ObtenerListaParaFiltro(),
                    ListaEmpresa = _repEmpresa.ObtenerTodoEmpresasFiltro(),
                    ListaAreaTrabajo = _repAreaTrabajo.ObtenerTodoAreaTrabajoFiltro(),
                    ListaCargo = _repCargo.ObtenerCargoFiltro(),
                    ListaParentesco = _repParentescoPersonal.ObtenerListaParaFiltro(),
                    ListaTipoSangre = _repTipoSangre.ObtenerListaParaFiltro(),
                    ListaPuestoTrabajo = _repPuestoTrabajo.GetFiltroIdNombre(),
                    ListaSedeTrabajo = _repSedeTrabajo.GetFiltroIdNombre(),
                    ListaPersonalAreaTrabajo = _repPersonalAreaTrabajo.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.Codigo }).ToList(),
                    ListaPersonal = _repPersonal.ObtenerTodoFiltro(),
                    ListaPersonalAsesorAsociado = _repPersonal.ObtenerAsesorCerrador(),
                    ListaTipoPagoRemuneracion = _repTipoPagoRemuneracion.GetFiltroIdNombre(),
                    ListaEntidadFinanciera = _repEntidadFinanciera.ObtenerEntidadesFinancieras(),
                    ListaContratoEstado = _repContratoEstado.ObtenerListaParaFiltro(),
                    ListaMotivoInactividad = _repMotivoInactividad.ObtenerListaParaFiltro(),
                    ListaPuestoTrabajoNivel = _repPuestoTrabajoNivel.ObtenerListaParaFiltro(),
                    ListaCategoriaAsesor = _repTableroComercialCategoriaAsesor.ObtenerComboBoxCategoriaAsesor()
                };
                return Ok(objetoAgrupado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información Nombres Rol e Email de personal registrado
        /// </summary>
        /// <returns>Lista de Nombres Rol e Email de personal registrado</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPersonalGrid()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repPersonal.ObtenerGrid());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información completa de Personal registrado en el sistema
        /// </summary>
        /// <param name="IdPersonal"> Id de Personal </param>
        /// <returns> Objeto Agrupado </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerInformacionPersonal([FromBody] int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var puestoTrabajoNivel = _repPuestoTrabajoNivel.GetBy(x => x.Estado == true).ToList();
                var personal = _repPersonal.ObtenerInformacionPersonalPuestoSede(IdPersonal);
                if (personal.IdPuestoTrabajoNivel == null)
                {
                    if (personal.TipoPersonal != null && personal.TipoPersonal.Length > 0)
                    {
                        var validacion = puestoTrabajoNivel.Where(x => x.Nombre.Contains(personal.TipoPersonal)).FirstOrDefault();
                        if (validacion != null)
                        {
                            personal.IdPuestoTrabajoNivel = validacion.Id;
                        }
                    }
                }
                var personalCese = _repPersonalCese.ObtenerMotivoFechaUltimo(IdPersonal);
                var personalRemuneracion = _repPersonalRemuneracion.ObtenerPersonalRemuneracion(IdPersonal);
                var personalDireccion = _repPersonal.ObtenerPersonalDireccionDomiciliaria(IdPersonal);
                var listaFormacion = _repPersonalFormacion.ObtenerPersonalFormacion(IdPersonal);
                var listaComputo = _repPersonalComputo.ObtenerPersonalComputo(IdPersonal);
                var listaIdioma = _repPersonalIdioma.ObtenerPersonalIdioma(IdPersonal);
                var listaCertificacion = _repPersonalCertificacion.ObtenerPersonalCertificacion(IdPersonal);
                var listaExperiencia = _repPersonalExperiencia.ObtenerPersonalExperiencia(IdPersonal);
                var listaInformacionMedica = _repPersonalInformacionMedica.ObtenerPersonalInformacionMedica(IdPersonal);
                var listaHistorialMedico = _repPersonalHistorialMedico.ObtenerPersonalHistorialMedico(IdPersonal);
                var listaSistemaPensionario = _repPersonalSistemaPensionario.ObtenerPersonalSistemaPensionario(IdPersonal);
                var listaSeguroSalud = _repPersonalSeguroSalud.ObtenerPersonalSeguroSalud(IdPersonal);
                var listaDatosPersonalFamiliar = _repDatoFamiliarPersonal.ObtenerListaFamiliarPersonal(IdPersonal);
                var listaAccesoTemporal = PersonalAccesoTemporalAulaVirtual.ObtenerListaAccesoTemporal(IdPersonal);
                var datoContratoPersonal = _repDatoContratoPersonal.GetBy(x => x.IdPersonal == IdPersonal && x.EstadoContrato == true).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();
                var listaPuestoTrabajo = _repPersonalLog.GetBy(x => x.EstadoRol == true && x.IdPersonal == IdPersonal, x => new PersonalPuestoTrabajoDTO { Id = x.Id, Rol = x.Rol, FechaInicio = x.FechaInicio, FechaFin = x.FechaFin }).ToList();
                if (listaPuestoTrabajo != null)
                {
                    var ultimo = listaPuestoTrabajo.Count;
                    listaPuestoTrabajo[ultimo - 1].FechaFin = null;
                }
                var listaTipoAsesorHistorico = _repPersonalLog.ObtenerTipoAsesorHistorico(IdPersonal);
                var listaJefeInmediatoHistorico = _repPersonalLog.ObtenerJefeInmediatoHistorico(IdPersonal);
                var listaPeriodoInactivoHistorico = _repPersonalMotivoTiempoInactividad.ObtenerPeriodoInactivoHistorico(IdPersonal);
                var ultimoPeriodoInactivo = listaPeriodoInactivoHistorico.OrderByDescending(x => x.Id).FirstOrDefault();
                if (personal.Activo == true)
                {
                    personalCese = null;
                    ultimoPeriodoInactivo = null;
                }
                return Ok(new
                {
                    DatosPersonal = personal,
                    DatosPersonalCese = personalCese,
                    PersonalRemuneracion = personalRemuneracion,
                    Formacion = listaFormacion,
                    Computo = listaComputo,
                    Idioma = listaIdioma,
                    Certificacion = listaCertificacion,
                    Experiencia = listaExperiencia,
                    DatoFamiliar = listaDatosPersonalFamiliar,
                    InformacionMedica = listaInformacionMedica,
                    HistorialMedico = listaHistorialMedico,
                    SistemaPensionario = listaSistemaPensionario,
                    SeguroSalud = listaSeguroSalud,
                    DatoContratoPersonal = datoContratoPersonal,
                    ListaAccesoTemporal = listaAccesoTemporal,
                    listaPuestoTrabajo = listaPuestoTrabajo,
                    PersonalDireccion = personalDireccion,
                    listaTipoAsesorHistorico,
                    listaJefeInmediatoHistorico,
                    listaPeriodoInactivoHistorico,
                    DatoPersonalDescanso = ultimoPeriodoInactivo
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 30/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los pespecificos para los accesos temporales para el modulo del personal, distinto al modulo de la agenda
        /// </summary>
        /// <returns>Response 200 (Objeto anonimo con los registros para los combos) o 400 con mensaje de error</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoPersonalAccesoTemporalCombo()
        {
            try
            {
                var resultado = _repPEspecifico.ObtenerPEspecificoPersonalNuevoAulaVirtualTipo();

                var programasAsignados = resultado.GroupBy(x => new { x.IdPEspecifico, x.NombrePEspecifico, x.IdCentroCosto, x.EstadoP, x.Modalidad, x.IdPGeneral, x.Ciudad, x.IdCursoMoodle, x.IdCursoMoodlePrueba, x.TipoPEspecifico })
                                                    .Select(s => new { s.Key.IdPEspecifico, s.Key.NombrePEspecifico, s.Key.IdCentroCosto, s.Key.EstadoP, s.Key.Modalidad, s.Key.IdPGeneral, s.Key.Ciudad, s.Key.IdCursoMoodle, s.Key.IdCursoMoodlePrueba, s.Key.TipoPEspecifico })
                                                    .ToList();
                var cursosAsignados = resultado.Select(s => new { IdPEspecificoPadre = s.IdPEspecifico, IdPEspecifico = s.IdPEspecificoHijo, NombrePEspecifico = s.NombrePEspecificoHijo }).ToList();

                return Ok(new { ProgramasAsignados = programasAsignados, CursosAsignados = cursosAsignados });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Luis Huallpa - Edgar Serruto.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Insertar Personal en el Sistema
        /// </summary>
        /// <param name="Compuesto"> Información Compuesta de Personal </param>
        /// <returns> Bool confirmación de inserción </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarPersonal([FromBody] MaestroPersonalCompuestoDTO Compuesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //Validacion de Id Jefe null
                Compuesto.Personal.IdJefe = Compuesto.Personal.IdJefe != null ? Compuesto.Personal.IdJefe : 0;
                //Validacion de Nivel de Puesto de Trabajo
                var auxiliarNivelPuestoTrabajo = "otro";
                if (Compuesto.Personal.IdPuestoTrabajoNivel > 0 && Compuesto.Personal.IdPuestoTrabajoNivel != null)
                {
                    auxiliarNivelPuestoTrabajo = _repPuestoTrabajoNivel.GetBy(x => x.Id == Compuesto.Personal.IdPuestoTrabajoNivel.GetValueOrDefault()).Select(x => x.NivelVisualizacionAgenda).FirstOrDefault();
                }
                else if (Compuesto.Personal.TipoPersonal != null && Compuesto.Personal.TipoPersonal.Length > 0)
                {
                    auxiliarNivelPuestoTrabajo = _repPuestoTrabajoNivel.GetBy(x => x.NivelVisualizacionAgenda.ToUpper().Contains(Compuesto.Personal.TipoPersonal.ToUpper())).Select(x => x.NivelVisualizacionAgenda).FirstOrDefault();
                }
                PersonaBO persona = new PersonaBO(_integraDBContext);
                PersonalBO personal;
                int? IdPersonaClasificacion = null;
                var ListEmailRepetidoValido = _repPersonal.GetBy(x => x.Estado == true && x.Email.Equals(Compuesto.Personal.Email), x => new { x.Email, x.Id }).ToList();
                var IdPersonalEmailRepetido = _repPersonal.ObtenerPersonalEliminadoEmailRepetido(Compuesto.Personal.Email);
                using (TransactionScope scope = new TransactionScope())
                {
                    if (ListEmailRepetidoValido.Count == 0 && (IdPersonalEmailRepetido == null || IdPersonalEmailRepetido == 0))
                    {
                        personal = new PersonalBO()
                        {
                            Apellidos = Compuesto.Personal.Apellidos,
                            Rol = Compuesto.Personal.Area,
                            AreaAbrev = Compuesto.Personal.AreaAbrev,
                            DistritoDireccion = Compuesto.Personal.DistritoDireccion,
                            EmailReferencia = Compuesto.Personal.EmailReferencia,
                            FechaNacimiento = Compuesto.Personal.FechaNacimiento,
                            IdCiudad = Compuesto.Personal.IdCiudadNacimiento,
                            IdRegionDireccion = Compuesto.Personal.IdCiudadReferencia,
                            IdEstadocivil = Compuesto.Personal.IdEstadocivil,
                            IdPaisNacimiento = Compuesto.Personal.IdPaisNacimiento,
                            IdPaisDireccion = Compuesto.Personal.IdPaisReferencia,
                            IdSexo = Compuesto.Personal.IdSexo,
                            IdTipoDocumento = Compuesto.Personal.IdTipoDocumento,
                            NombreDireccion = Compuesto.Personal.NombreDireccion,
                            Nombres = Compuesto.Personal.Nombres,
                            NumeroDocumento = Compuesto.Personal.NumeroDocumento,
                            FijoReferencia = Compuesto.Personal.TelefonoFijo,
                            MovilReferencia = Compuesto.Personal.TelefonoMovil,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Email = Compuesto.Personal.Email,
                            TipoPersonal = auxiliarNivelPuestoTrabajo,
                            IdJefe = Compuesto.Personal.IdJefe,
                            Central = Compuesto.Personal.Central,
                            Anexo3Cx = Compuesto.Personal.Anexo3CX,
                            UrlFirmaCorreos = Compuesto.Personal.UrlFirmaCorreos,
                            Activo = Compuesto.Personal.Activo,
                            IdSistemaPensionario = Compuesto.PersonalSistemaPensionario.IdSistemaPensionario,
                            IdEntidadSistemaPensionario = Compuesto.PersonalSistemaPensionario.IdEntidadSistemaPensionario,
                            NombreCuspp = Compuesto.PersonalSistemaPensionario.CodigoAfiliado,
                            ConEssalud = Compuesto.PersonalSeguroSalud.IdEntidadSeguroSalud.HasValue ? true : false,
                            IdTipoSangre = Compuesto.Personal.IdTipoSangre,
                            EsCerrador = Compuesto.Personal.EsCerrador,
                            IdCerrador = Compuesto.Personal.IdAsesorAsociado,
                            IdPuestoTrabajoNivel = Compuesto.Personal.IdPuestoTrabajoNivel,
                            IdPersonalArchivo = Compuesto.Personal.IdPersonalArchivo,
                            IdPersonalAreaTrabajo = Compuesto.Personal.IdPersonalAreaTrabajo,
                            IdTableroComercialCategoriaAsesor = Compuesto.Personal.IdTableroComercialCategoriaAsesor
                    };
                        _repPersonal.Insert(personal);

                        PersonalPuestoSedeHistoricoBO agregar = new PersonalPuestoSedeHistoricoBO()
                        {
                            IdPersonal = personal.Id,
                            IdPuestoTrabajo = Compuesto.Personal.IdPuestoTrabajo.GetValueOrDefault(),
                            IdSedeTrabajo = Compuesto.Personal.IdSede.GetValueOrDefault(),
                            Actual = true,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario
                        };
                        var resInsertar = _repPersonalPuestoSedeHistorico.Insert(agregar);
                    }
                    else if (ListEmailRepetidoValido.Count == 0 && (IdPersonalEmailRepetido != null || IdPersonalEmailRepetido != 0))
                    {
                        _repPersonal.ActivarPersonal(IdPersonalEmailRepetido.Value);
                        personal = _repPersonal.FirstById(IdPersonalEmailRepetido.Value);
                        personal.Apellidos = Compuesto.Personal.Apellidos;
                        personal.Rol = Compuesto.Personal.Area;
                        personal.AreaAbrev = Compuesto.Personal.AreaAbrev;
                        personal.DistritoDireccion = Compuesto.Personal.DistritoDireccion;
                        personal.EmailReferencia = Compuesto.Personal.EmailReferencia;
                        personal.FechaNacimiento = Compuesto.Personal.FechaNacimiento;
                        personal.IdCiudad = Compuesto.Personal.IdCiudadNacimiento;
                        personal.IdRegionDireccion = Compuesto.Personal.IdCiudadReferencia;
                        personal.IdEstadocivil = Compuesto.Personal.IdEstadocivil;
                        personal.IdPaisNacimiento = Compuesto.Personal.IdPaisNacimiento;
                        personal.IdPaisDireccion = Compuesto.Personal.IdPaisReferencia;
                        personal.IdSexo = Compuesto.Personal.IdSexo;
                        personal.IdTipoDocumento = Compuesto.Personal.IdTipoDocumento;
                        personal.NombreDireccion = Compuesto.Personal.NombreDireccion;
                        personal.Nombres = Compuesto.Personal.Nombres;
                        personal.NumeroDocumento = Compuesto.Personal.NumeroDocumento;
                        personal.FijoReferencia = Compuesto.Personal.TelefonoFijo;
                        personal.MovilReferencia = Compuesto.Personal.TelefonoMovil;
                        personal.UsuarioModificacion = Compuesto.Usuario;
                        personal.FechaModificacion = DateTime.Now;
                        personal.Email = Compuesto.Personal.Email;
                        personal.TipoPersonal = auxiliarNivelPuestoTrabajo;
                        personal.IdJefe = Compuesto.Personal.IdJefe;
                        personal.Central = Compuesto.Personal.Central;
                        personal.Anexo3Cx = Compuesto.Personal.Anexo3CX;
                        personal.UrlFirmaCorreos = Compuesto.Personal.UrlFirmaCorreos;
                        personal.Activo = Compuesto.Personal.Activo;
                        personal.IdSistemaPensionario = Compuesto.PersonalSistemaPensionario.IdSistemaPensionario;
                        personal.IdEntidadSistemaPensionario = Compuesto.PersonalSistemaPensionario.IdEntidadSistemaPensionario;
                        personal.NombreCuspp = Compuesto.PersonalSistemaPensionario.CodigoAfiliado;
                        personal.ConEssalud = Compuesto.PersonalSeguroSalud.IdEntidadSeguroSalud.HasValue ? true : false;
                        personal.IdTipoSangre = Compuesto.Personal.IdTipoSangre;
                        personal.EsCerrador = Compuesto.Personal.EsCerrador;
                        personal.IdCerrador = Compuesto.Personal.IdAsesorAsociado;
                        personal.IdPuestoTrabajoNivel = Compuesto.Personal.IdPuestoTrabajoNivel;
                        personal.IdTableroComercialCategoriaAsesor = Compuesto.Personal.IdTableroComercialCategoriaAsesor;
                        _repPersonal.Update(personal);

                        var personalPuestoTrabajoSede = _repPersonalPuestoSedeHistorico.GetBy(x => x.IdPersonal == personal.Id).FirstOrDefault();
                        if (personalPuestoTrabajoSede == null)
                        {
                            PersonalPuestoSedeHistoricoBO agregar = new PersonalPuestoSedeHistoricoBO()
                            {
                                IdPersonal = personal.Id,
                                IdPuestoTrabajo = Compuesto.Personal.IdPuestoTrabajo.GetValueOrDefault(),
                                IdSedeTrabajo = Compuesto.Personal.IdSede.GetValueOrDefault(),
                                Actual = true,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario
                            };
                            var resInsertar = _repPersonalPuestoSedeHistorico.Insert(agregar);
                        }
                        else
                        {
                            personalPuestoTrabajoSede.FechaModificacion = DateTime.Now;
                            personalPuestoTrabajoSede.UsuarioModificacion = Compuesto.Usuario;
                            personalPuestoTrabajoSede.Actual = false;
                            var res = _repPersonalPuestoSedeHistorico.Update(personalPuestoTrabajoSede);

                            if (res)
                            {
                                PersonalPuestoSedeHistoricoBO agregar = new PersonalPuestoSedeHistoricoBO()
                                {
                                    IdPersonal = personal.Id,
                                    IdPuestoTrabajo = Compuesto.Personal.IdPuestoTrabajo.GetValueOrDefault(),
                                    IdSedeTrabajo = Compuesto.Personal.IdSede.GetValueOrDefault(),
                                    Actual = true,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = Compuesto.Usuario,
                                    UsuarioModificacion = Compuesto.Usuario
                                };
                                var resInsertar = _repPersonalPuestoSedeHistorico.Insert(agregar);
                            }
                        }
                        //Asignacion de módulos
                        var usuario = _usuarioModulo.GetBy(x => x.IdPersonal == Compuesto.Personal.Id).FirstOrDefault();
                        if (usuario != null)
                        {
                            var listaModuloAnterior = _moduloAcceso.GetBy(x => x.IdUsuario == usuario.Id).ToList();
                            var listaModuloNuevo = _moduloPuesto.GetBy(x => x.IdPuestoTrabajo == Compuesto.Personal.IdPuestoTrabajo).ToList();
                            if (listaModuloAnterior.Count > 0)
                            {
                                foreach (var moduloAnterior in listaModuloAnterior)
                                {
                                    _moduloAcceso.Delete(moduloAnterior.Id, Compuesto.Usuario);
                                }
                            }
                            if (listaModuloNuevo.Count > 0)
                            {
                                ModuloSistemaAccesoBO agregarModulo;
                                foreach (var moduloNuevo in listaModuloNuevo)
                                {
                                    agregarModulo = new ModuloSistemaAccesoBO()
                                    {
                                        IdUsuarioRol = usuario.IdUsuarioRol,
                                        IdUsuario = usuario.Id,
                                        IdModuloSistema = moduloNuevo.IdModuloSistema,
                                        Estado = true,
                                        UsuarioCreacion = Compuesto.Usuario,
                                        UsuarioModificacion = Compuesto.Usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now
                                    };
                                    _moduloAcceso.Insert(agregarModulo);
                                }
                            }
                        }
                    }
                    else
                    {
                        personal = new PersonalBO();
                    }
                    IdPersonaClasificacion = persona.InsertarPersona(personal.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Personal, Compuesto.Usuario);

                    if (IdPersonaClasificacion == null)
                    {
                        throw new Exception("Error al insertar el Tipo Persona Clasificacion");
                    }
                    if (personal != null)
                    {
                        PersonalLogBO personalLogBO = new PersonalLogBO();
                        personalLogBO.IdPersonal = personal.Id;
                        personalLogBO.Rol = Compuesto.Personal.Area;
                        personalLogBO.TipoPersonal = personal.TipoPersonal;
                        personalLogBO.IdPuestoTrabajoNivel = personal.IdPuestoTrabajoNivel;
                        personalLogBO.IdJefe = personal.IdJefe;
                        personalLogBO.EstadoCerrador = false;
                        personalLogBO.EstadoRol = true;
                        personalLogBO.EstadoTipoPersonal = true;
                        personalLogBO.EstadoIdJefe = true;
                        personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                        personalLogBO.FechaFin = null;
                        personalLogBO.Estado = true;
                        personalLogBO.UsuarioCreacion = Compuesto.Usuario;
                        personalLogBO.UsuarioModificacion = Compuesto.Usuario;
                        personalLogBO.FechaCreacion = DateTime.Now;
                        personalLogBO.FechaModificacion = DateTime.Now;
                        _repPersonalLog.Insert(personalLogBO);

                        var personalLogActualizar = _repPersonalLog.GetBy(x => x.IdPersonal == personal.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                        PersonalLogBO personalLogTipoAsesorBO = new PersonalLogBO();
                        personalLogTipoAsesorBO.IdPersonal = personalLogActualizar.IdPersonal;
                        personalLogTipoAsesorBO.Rol = personalLogActualizar.Rol;
                        personalLogTipoAsesorBO.TipoPersonal = personalLogActualizar.TipoPersonal;
                        personalLogTipoAsesorBO.IdPuestoTrabajoNivel = personalLogActualizar.IdPuestoTrabajoNivel;
                        personalLogTipoAsesorBO.IdJefe = personalLogActualizar.IdJefe;
                        personalLogTipoAsesorBO.IdCerrador = personal.IdCerrador;
                        personalLogTipoAsesorBO.EsCerrador = personal.EsCerrador;
                        personalLogTipoAsesorBO.EstadoCerrador = true;
                        personalLogTipoAsesorBO.EstadoRol = false;
                        personalLogTipoAsesorBO.EstadoTipoPersonal = false;
                        personalLogTipoAsesorBO.EstadoIdJefe = false;
                        personalLogTipoAsesorBO.FechaInicio = DateTime.Now.Date;
                        personalLogTipoAsesorBO.FechaFin = null;
                        personalLogTipoAsesorBO.Estado = true;
                        personalLogTipoAsesorBO.UsuarioCreacion = personalLogActualizar.UsuarioCreacion;
                        personalLogTipoAsesorBO.UsuarioModificacion = personalLogActualizar.UsuarioModificacion;
                        personalLogTipoAsesorBO.FechaCreacion = personalLogActualizar.FechaCreacion;
                        personalLogTipoAsesorBO.FechaModificacion = personalLogActualizar.FechaModificacion;
                        _repPersonalLog.Insert(personalLogTipoAsesorBO);
                    }
                    if (Compuesto.PersonalDireccion.IdPais.HasValue)
                    {
                        PersonalDireccionBO personalDireccion = new PersonalDireccionBO
                        {
                            IdPersonal = personal.Id,
                            IdPais = Compuesto.PersonalDireccion.IdPais,
                            IdCiudad = Compuesto.PersonalDireccion.IdCiudad,
                            Distrito = Compuesto.PersonalDireccion.Distrito == "" ? null : Compuesto.PersonalDireccion.Distrito,
                            TipoVia = Compuesto.PersonalDireccion.TipoVia == "" ? null : Compuesto.PersonalDireccion.TipoVia,
                            TipoZonaUrbana = Compuesto.PersonalDireccion.TipoZonaUrbana == "" ? null : Compuesto.PersonalDireccion.TipoZonaUrbana,
                            NombreVia = Compuesto.PersonalDireccion.NombreVia == "" ? null : Compuesto.PersonalDireccion.NombreVia,
                            NombreZonaUrbana = Compuesto.PersonalDireccion.NombreZonaUrbana == "" ? null : Compuesto.PersonalDireccion.NombreZonaUrbana,
                            Manzana = Compuesto.PersonalDireccion.Manzana == "" ? null : Compuesto.PersonalDireccion.Manzana,
                            Lote = Compuesto.PersonalDireccion.Lote,
                            Activo = true,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _repPersonalDireccion.Insert(personalDireccion);
                    }
                    if (Compuesto.PersonalSistemaPensionario.IdSistemaPensionario.HasValue)
                    {
                        PersonalSistemaPensionarioBO personalSistemaPensionario = new PersonalSistemaPensionarioBO
                        {
                            Activo = true,
                            CodigoAfiliado = Compuesto.PersonalSistemaPensionario.CodigoAfiliado,
                            IdEntidadSistemaPensionario = Compuesto.PersonalSistemaPensionario.IdEntidadSistemaPensionario,
                            IdSistemaPensionario = Compuesto.PersonalSistemaPensionario.IdSistemaPensionario.Value,
                            IdPersonal = personal.Id,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _repPersonalSistemaPensionario.Insert(personalSistemaPensionario);
                    }
                    if (Compuesto.PersonalSeguroSalud.IdEntidadSeguroSalud.HasValue)
                    {
                        PersonalSeguroSaludBO personalSeguroSalud = new PersonalSeguroSaludBO()
                        {
                            IdEntidadSeguroSalud = Compuesto.PersonalSeguroSalud.IdEntidadSeguroSalud.Value,
                            IdPersonal = personal.Id,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Activo = true,
                        };
                        _repPersonalSeguroSalud.Insert(personalSeguroSalud);
                    }
                    if (Compuesto.PersonalRemuneracion.IdTipoPagoRemuneracion.HasValue)
                    {
                        PersonalRemuneracionBO personalSeguroSalud = new PersonalRemuneracionBO()
                        {
                            IdTipoPagoRemuneracion = Compuesto.PersonalRemuneracion.IdTipoPagoRemuneracion.Value,
                            IdEntidadFinanciera = Compuesto.PersonalRemuneracion.IdEntidadFinanciera,
                            IdPersonal = personal.Id,
                            NumeroCuenta = Compuesto.PersonalRemuneracion.NumeroCuenta,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Activo = true,
                        };
                        _repPersonalRemuneracion.Insert(personalSeguroSalud);
                    }
                    foreach (var item in Compuesto.PersonalFormacion)
                    {
                        PersonalFormacionBO personalFormacion = new PersonalFormacionBO
                        {
                            AlaActualidad = item.AlaActualidad,
                            FechaFin = item.FechaFin,
                            FechaInicio = item.FechaInicio,
                            IdAreaFormacion = item.IdAreaFormacion,
                            IdCentroEstudio = item.IdCentroEstudio,
                            IdEstadoEstudio = item.IdEstadoEstudio,
                            IdTipoEstudio = item.IdTipoEstudio,
                            Logro = item.Logro,
                            IdPersonal = personal.Id,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            IdPersonalArchivo = item.IdPersonalArchivo
                        };
                        _repPersonalFormacion.Insert(personalFormacion);
                    }
                    foreach (var item in Compuesto.PersonalInformatica)
                    {
                        PersonalComputoBO personalComputo = new PersonalComputoBO
                        {
                            IdCentroEstudio = item.IdCentroEstudio,
                            IdNivelCompetenciaTecnica = item.IdNivelCompetenciaTecnica,
                            Programa = item.Programa,
                            IdPersonal = personal.Id,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            IdPersonalArchivo = item.IdPersonalArchivo,
                        };
                        _repPersonalComputo.Insert(personalComputo);
                    }
                    foreach (var item in Compuesto.PersonalIdiomas)
                    {
                        PersonalIdiomaBO personalIdioma = new PersonalIdiomaBO
                        {
                            IdCentroEstudio = item.IdCentroEstudio,
                            IdIdioma = item.IdIdioma,
                            IdNivelIdioma = item.IdNivelIdioma,
                            IdPersonal = personal.Id,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            IdPersonalArchivo = item.IdPersonalArchivo
                        };
                        _repPersonalIdioma.Insert(personalIdioma);
                    }
                    foreach (var item in Compuesto.PersonalCertificacion)
                    {
                        PersonalCertificacionBO personalCertificacion = new PersonalCertificacionBO
                        {
                            Institucion = item.Institucion,
                            Programa = item.Programa,
                            FechaCertificacion = item.FechaCertificacion,
                            IdPersonal = personal.Id,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            IdPersonalArchivo = item.IdPersonalArchivo,
                            IdCentroEstudio = item.IdCentroEstudio
                        };
                        _repPersonalCertificacion.Insert(personalCertificacion);
                    }
                    foreach (var item in Compuesto.PersonalExperiencia)
                    {
                        PersonalExperienciaBO personalExperiencia = new PersonalExperienciaBO
                        {
                            IdPersonal = personal.Id,
                            FechaIngreso = item.FechaIngreso,
                            FechaRetiro = item.FechaRetiro,
                            IdAreaTrabajo = item.IdAreaTrabajo,
                            IdCargo = item.IdCargo,
                            IdEmpresa = item.IdEmpresa,
                            MotivoRetiro = item.MotivoRetiro,
                            NombreJefeInmediato = item.NombreJefeInmediato,
                            TelefonoJefeInmediato = item.TelefonoJefeInmediato,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            IdPersonalArchivo = item.IdPersonalArchivo
                        };
                        _repPersonalExperiencia.Insert(personalExperiencia);
                    }
                    foreach (var item in Compuesto.PersonalFamiliar)
                    {
                        DatoFamiliarPersonalBO personalFamiliar = new DatoFamiliarPersonalBO
                        {
                            Apellidos = item.Apellidos,
                            Nombres = item.Nombres,
                            DerechoHabiente = item.DerechoHabiente,
                            EsContactoInmediato = item.EsContactoInmediato,
                            FechaNacimiento = item.FechaNacimiento,
                            IdParentescoPersonal = item.IdParentescoPersonal,
                            IdSexo = item.IdSexo,
                            IdTipoDocumentoPersonal = item.IdTipoDocumentoPersonal,
                            NumeroDocumento = item.NumeroDocumento,
                            NumeroReferencia1 = item.NumeroReferencia,
                            IdPersonal = personal.Id,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _repDatoFamiliarPersonal.Insert(personalFamiliar);
                    }
                    foreach (var item in Compuesto.PersonalInformacionMedica)
                    {
                        PersonalInformacionMedicaBO personalInformacionMedica = new PersonalInformacionMedicaBO
                        {
                            Alergia = item.Alergia,
                            IdTipoSangre = Compuesto.Personal.IdTipoSangre,
                            Precaucion = item.Precaucion,
                            IdPersonal = personal.Id,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _repPersonalInformacionMedica.Insert(personalInformacionMedica);
                    }
                    foreach (var item in Compuesto.PersonalHistorialMedico)
                    {
                        PersonalHistorialMedicoBO personalHistorialMedico = new PersonalHistorialMedicoBO
                        {
                            Enfermedad = item.Enfermedad,
                            DetalleEnfermedad = item.DetalleEnfermedad,
                            Periodo = item.Periodo,
                            IdPersonal = personal.Id,
                            Estado = true,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _repPersonalHistorialMedico.Insert(personalHistorialMedico);
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Luis Huallpa - Edgar Serruto.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualización de datos de un Personal en el Sistema
        /// </summary>
        /// <param name="Compuesto"> Información Compuesta de Personal </param>
        /// <returns>Bool confirmación de actualización</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarPersonal([FromBody] MaestroPersonalCompuestoDTO Compuesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //Validacion de Id Jefe null
                Compuesto.Personal.IdJefe = Compuesto.Personal.IdJefe != null ? Compuesto.Personal.IdJefe : 0;
                var personal = _repPersonal.FirstById(Compuesto.Personal.Id);
                //Validacion de Nivel de Puesto de Trabajo
                var auxiliarNivelPuestoTrabajo = "otro";
                if (Compuesto.Personal.IdPuestoTrabajoNivel > 0 && Compuesto.Personal.IdPuestoTrabajoNivel != null)
                {
                    auxiliarNivelPuestoTrabajo = _repPuestoTrabajoNivel.GetBy(x => x.Id == Compuesto.Personal.IdPuestoTrabajoNivel.GetValueOrDefault()).Select(x => x.NivelVisualizacionAgenda).FirstOrDefault();
                }
                else if (Compuesto.Personal.TipoPersonal != null && Compuesto.Personal.TipoPersonal.Length > 0)
                {
                    auxiliarNivelPuestoTrabajo = _repPuestoTrabajoNivel.GetBy(x => x.NivelVisualizacionAgenda.ToUpper().Contains(Compuesto.Personal.TipoPersonal.ToUpper())).Select(x => x.NivelVisualizacionAgenda).FirstOrDefault();
                }
                else
                {
                    auxiliarNivelPuestoTrabajo = "otro";
                }
                var PersonalCertificacion = _repPersonalCertificacion.GetBy(x => x.IdPersonal == Compuesto.Personal.Id).ToList();
                var PersonalExperiencia = _repPersonalExperiencia.GetBy(x => x.IdPersonal == Compuesto.Personal.Id).ToList();
                var PersonalFamiliar = _repDatoFamiliarPersonal.GetBy(x => x.IdPersonal == Compuesto.Personal.Id).ToList();
                var PersonalFormacion = _repPersonalFormacion.GetBy(x => x.IdPersonal == Compuesto.Personal.Id).ToList();
                var PersonalHistorialMedico = _repPersonalHistorialMedico.GetBy(x => x.IdPersonal == Compuesto.Personal.Id).ToList();
                var PersonalIdiomas = _repPersonalIdioma.GetBy(x => x.IdPersonal == Compuesto.Personal.Id).ToList();
                var PersonalInformacionMedica = _repPersonalInformacionMedica.GetBy(x => x.IdPersonal == Compuesto.Personal.Id).ToList();
                var PersonalInformatica = _repPersonalComputo.GetBy(x => x.IdPersonal == Compuesto.Personal.Id).ToList();
                var personalPuestoTrabajoSede = _repPersonalPuestoSedeHistorico.GetBy(x => x.IdPersonal == personal.Id && x.Actual == true).FirstOrDefault();
                var RolAnterior = personal.Rol;
                var TipoPersonalAnterior = personal.TipoPersonal == null ? "" : personal.TipoPersonal;
                int? IdJefeAnterior = personal.IdJefe;
                bool? esCerradorAnterior = personal.EsCerrador;
                int? idCerradorAnterior = personal.IdCerrador;
                var estadoCambioRolJefe = false;
                bool? estadoPersonalAnterior = personal.Activo;
                //Registro de Puesto de trabajo y Sede
                if (personal != null)
                {
                    if (personalPuestoTrabajoSede == null)
                    {
                        PersonalPuestoSedeHistoricoBO agregar = new PersonalPuestoSedeHistoricoBO()
                        {
                            IdPersonal = personal.Id,
                            IdPuestoTrabajo = Compuesto.Personal.IdPuestoTrabajo.GetValueOrDefault(),
                            IdSedeTrabajo = Compuesto.Personal.IdSede.GetValueOrDefault(),
                            Actual = true,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = Compuesto.Usuario,
                            UsuarioModificacion = Compuesto.Usuario
                        };
                        var resInsertar = _repPersonalPuestoSedeHistorico.Insert(agregar);                        
                    }
                    else
                    {
                        if (personalPuestoTrabajoSede.IdPuestoTrabajo != Compuesto.Personal.IdPuestoTrabajo || personalPuestoTrabajoSede.IdSedeTrabajo != Compuesto.Personal.IdSede)
                        {
                            personalPuestoTrabajoSede.FechaModificacion = DateTime.Now;
                            personalPuestoTrabajoSede.UsuarioModificacion = Compuesto.Usuario;
                            personalPuestoTrabajoSede.Actual = false;
                            var res = _repPersonalPuestoSedeHistorico.Update(personalPuestoTrabajoSede);

                            if (res)
                            {
                                PersonalPuestoSedeHistoricoBO agregar = new PersonalPuestoSedeHistoricoBO()
                                {
                                    IdPersonal = personal.Id,
                                    IdPuestoTrabajo = Compuesto.Personal.IdPuestoTrabajo.GetValueOrDefault(),
                                    IdSedeTrabajo = Compuesto.Personal.IdSede.GetValueOrDefault(),
                                    Actual = true,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = Compuesto.Usuario,
                                    UsuarioModificacion = Compuesto.Usuario
                                };
                                _repPersonalPuestoSedeHistorico.Insert(agregar);
                            }                            
                        }
                    }
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    personal.Apellidos = Compuesto.Personal.Apellidos;
                    personal.Rol = Compuesto.Personal.Area;
                    personal.AreaAbrev = Compuesto.Personal.AreaAbrev;
                    personal.TipoPersonal = auxiliarNivelPuestoTrabajo;
                    personal.IdPuestoTrabajoNivel = Compuesto.Personal.IdPuestoTrabajoNivel;
                    personal.DistritoDireccion = Compuesto.Personal.DistritoDireccion;
                    personal.EmailReferencia = Compuesto.Personal.EmailReferencia;
                    personal.FechaNacimiento = Compuesto.Personal.FechaNacimiento;
                    personal.IdCiudad = Compuesto.Personal.IdCiudadNacimiento;
                    personal.IdRegionDireccion = Compuesto.Personal.IdCiudadReferencia;
                    personal.IdEstadocivil = Compuesto.Personal.IdEstadocivil;
                    personal.IdPaisNacimiento = Compuesto.Personal.IdPaisNacimiento;
                    personal.IdPaisDireccion = Compuesto.Personal.IdPaisReferencia;
                    personal.IdSexo = Compuesto.Personal.IdSexo;
                    personal.IdTipoDocumento = Compuesto.Personal.IdTipoDocumento;
                    personal.NombreDireccion = Compuesto.Personal.NombreDireccion;
                    personal.Nombres = Compuesto.Personal.Nombres;
                    personal.NumeroDocumento = Compuesto.Personal.NumeroDocumento;
                    personal.FijoReferencia = Compuesto.Personal.TelefonoFijo;
                    personal.MovilReferencia = Compuesto.Personal.TelefonoMovil;
                    personal.UsuarioModificacion = Compuesto.Usuario;
                    personal.FechaModificacion = DateTime.Now;
                    personal.Email = Compuesto.Personal.Email;
                    personal.TipoPersonal = auxiliarNivelPuestoTrabajo;
                    personal.IdJefe = Compuesto.Personal.IdJefe;
                    personal.Central = Compuesto.Personal.Central;
                    personal.Anexo3Cx = Compuesto.Personal.Anexo3CX;
                    personal.UrlFirmaCorreos = Compuesto.Personal.UrlFirmaCorreos;
                    personal.Activo = Compuesto.Personal.Activo;
                    personal.IdSistemaPensionario = Compuesto.PersonalSistemaPensionario.IdSistemaPensionario;
                    personal.IdEntidadSistemaPensionario = Compuesto.PersonalSistemaPensionario.IdEntidadSistemaPensionario;
                    personal.NombreCuspp = Compuesto.PersonalSistemaPensionario.CodigoAfiliado;
                    personal.ConEssalud = Compuesto.PersonalSeguroSalud.IdEntidadSeguroSalud.HasValue ? true : personal.ConEssalud;
                    personal.IdTipoSangre = Compuesto.Personal.IdTipoSangre;
                    personal.EsCerrador = Compuesto.Personal.EsCerrador;
                    personal.IdCerrador = Compuesto.Personal.IdAsesorAsociado;
                    personal.IdPersonalArchivo = Compuesto.Personal.IdPersonalArchivo;
                    personal.IdPersonalAreaTrabajo = Compuesto.Personal.IdPersonalAreaTrabajo;
                    personal.IdTableroComercialCategoriaAsesor = Compuesto.Personal.IdTableroComercialCategoriaAsesor;
                    _repPersonal.Update(personal);

                    //Inicio de insert or update in T_PersonalLog 
                    if (!(RolAnterior.ToUpper().Equals(personal.Rol.ToUpper())) || !(TipoPersonalAnterior.ToUpper().Equals(personal.TipoPersonal.ToUpper())))
                    {
                        var personalLogUpdate = _repPersonalLog.FirstBy(x => x.IdPersonal == personal.Id && (x.EstadoRol == true || x.EstadoTipoPersonal == true) && x.FechaFin == null);
                        var personalCambioJefe = _repPersonalLog.GetBy(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true && x.EstadoRol == false && x.EstadoTipoPersonal == false) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                        estadoCambioRolJefe = personalLogUpdate.EstadoIdJefe == true && personalLogUpdate.EstadoRol == true && personalLogUpdate.EstadoTipoPersonal == true;
                        if (estadoCambioRolJefe && personalCambioJefe == null)
                        {
                            PersonalLogBO personalLog = new PersonalLogBO();
                            personalLog.IdPersonal = personal.Id;
                            personalLog.Rol = personal.Rol;
                            personalLog.TipoPersonal = personal.TipoPersonal;
                            personalLog.IdPuestoTrabajoNivel = personal.IdPuestoTrabajoNivel;
                            personalLog.IdJefe = IdJefeAnterior;
                            personalLog.EstadoRol = false;
                            personalLog.EstadoTipoPersonal = false;
                            personalLog.EstadoIdJefe = true;
                            personalLog.FechaInicio = personalLogUpdate.FechaInicio;
                            personalLog.FechaFin = null;
                            personalLog.Estado = true;
                            personalLog.UsuarioModificacion = Compuesto.Usuario;
                            personalLog.UsuarioCreacion = Compuesto.Usuario;
                            personalLog.FechaCreacion = DateTime.Now;
                            personalLog.FechaModificacion = DateTime.Now;
                            _repPersonalLog.Insert(personalLog);
                        }
                        personalLogUpdate.FechaFin = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 23, 59, 59);
                        personalLogUpdate.UsuarioModificacion = Compuesto.Usuario;
                        personalLogUpdate.FechaModificacion = DateTime.Now;
                        _repPersonalLog.Update(personalLogUpdate);

                        PersonalLogBO personalLogBO = new PersonalLogBO();
                        personalLogBO.IdPersonal = personal.Id;
                        personalLogBO.Rol = personal.Rol;
                        personalLogBO.TipoPersonal = personal.TipoPersonal;
                        personalLogBO.IdPuestoTrabajoNivel = personal.IdPuestoTrabajoNivel;
                        personalLogBO.IdJefe = personal.IdJefe;
                        personalLogBO.EstadoRol = RolAnterior != personal.Rol;
                        personalLogBO.EstadoTipoPersonal = TipoPersonalAnterior != personal.TipoPersonal;
                        personalLogBO.EstadoIdJefe = false;
                        personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0); ;
                        personalLogBO.FechaFin = null;
                        personalLogBO.Estado = true;
                        personalLogBO.UsuarioModificacion = Compuesto.Usuario;
                        personalLogBO.UsuarioCreacion = Compuesto.Usuario;
                        personalLogBO.FechaCreacion = DateTime.Now;
                        personalLogBO.FechaModificacion = DateTime.Now;

                        _repPersonalLog.Insert(personalLogBO);
                    }
                    if (IdJefeAnterior != personal.IdJefe)
                    {
                        if (estadoCambioRolJefe == false)
                        {
                            var personalLogUpdate = _repPersonalLog.GetBy(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                            var personalCambioJefe = _repPersonalLog.GetBy(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true && x.EstadoRol == false && x.EstadoTipoPersonal == false) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                            estadoCambioRolJefe = personalLogUpdate.EstadoIdJefe == true && personalLogUpdate.EstadoRol == true && personalLogUpdate.EstadoTipoPersonal == true;
                            if (estadoCambioRolJefe && personalCambioJefe == null)
                            {
                                PersonalLogBO personalLog = new PersonalLogBO();
                                personalLog.IdPersonal = personal.Id;
                                personalLog.Rol = personal.Rol;
                                personalLog.TipoPersonal = personal.TipoPersonal;
                                personalLog.IdPuestoTrabajoNivel = personal.IdPuestoTrabajoNivel;
                                personalLog.IdJefe = IdJefeAnterior;
                                personalLog.EstadoRol = false;
                                personalLog.EstadoTipoPersonal = false;
                                personalLog.EstadoIdJefe = true;
                                personalLog.FechaInicio = personalLogUpdate.FechaInicio;
                                personalLog.FechaFin = null;
                                personalLog.Estado = true;
                                personalLog.UsuarioModificacion = Compuesto.Usuario;
                                personalLog.UsuarioCreacion = Compuesto.Usuario;
                                personalLog.FechaCreacion = DateTime.Now;
                                personalLog.FechaModificacion = DateTime.Now;
                                _repPersonalLog.Insert(personalLog);
                            }
                        }
                        var personalLogUpdate2 = _repPersonalLog.GetBy(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                        personalLogUpdate2.FechaFin = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 23, 59, 59);
                        personalLogUpdate2.UsuarioModificacion = Compuesto.Usuario;
                        personalLogUpdate2.FechaModificacion = DateTime.Now;
                        _repPersonalLog.Update(personalLogUpdate2);

                        PersonalLogBO personalLogBO = new PersonalLogBO();
                        personalLogBO.IdPersonal = personal.Id;
                        personalLogBO.Rol = personal.Rol;
                        personalLogBO.TipoPersonal = personal.TipoPersonal;
                        personalLogBO.IdPuestoTrabajoNivel = personal.IdPuestoTrabajoNivel;
                        personalLogBO.IdJefe = personal.IdJefe;
                        personalLogBO.EstadoRol = false;
                        personalLogBO.EstadoTipoPersonal = false;
                        personalLogBO.EstadoIdJefe = IdJefeAnterior != personal.IdJefe;
                        personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                        personalLogBO.FechaFin = null;
                        personalLogBO.Estado = true;
                        personalLogBO.UsuarioModificacion = Compuesto.Usuario;
                        personalLogBO.UsuarioCreacion = Compuesto.Usuario;
                        personalLogBO.FechaCreacion = DateTime.Now;
                        personalLogBO.FechaModificacion = DateTime.Now;
                        _repPersonalLog.Insert(personalLogBO);
                    }
                    if (esCerradorAnterior != personal.EsCerrador || idCerradorAnterior != personal.IdCerrador)
                    {
                        var actualizarFechaAnteriorTipoAsesor = _repPersonalLog.GetBy(x => x.IdPersonal == personal.Id && x.EstadoCerrador == true).OrderByDescending(x => x.Id).FirstOrDefault();
                        if (actualizarFechaAnteriorTipoAsesor != null)
                        {
                            actualizarFechaAnteriorTipoAsesor.FechaFin = DateTime.Now.Date;
                            _repPersonalLog.Update(actualizarFechaAnteriorTipoAsesor);
                        }
                        var personalLogActualizar = _repPersonalLog.GetBy(x => x.IdPersonal == personal.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                        if (personalLogActualizar != null)
                        {
                            PersonalLogBO personalLogBO = new PersonalLogBO();
                            personalLogBO.IdPersonal = personalLogActualizar.IdPersonal;
                            personalLogBO.Rol = personalLogActualizar.Rol;
                            personalLogBO.TipoPersonal = personalLogActualizar.TipoPersonal;
                            personalLogBO.IdPuestoTrabajoNivel = personalLogActualizar.IdPuestoTrabajoNivel;
                            personalLogBO.IdJefe = personalLogActualizar.IdJefe;
                            personalLogBO.IdCerrador = personal.IdCerrador;
                            personalLogBO.EsCerrador = personal.EsCerrador;
                            personalLogBO.EstadoCerrador = true;
                            personalLogBO.EstadoRol = false;
                            personalLogBO.EstadoTipoPersonal = false;
                            personalLogBO.EstadoIdJefe = false;
                            personalLogBO.FechaInicio = DateTime.Now.Date;
                            personalLogBO.FechaFin = null;
                            personalLogBO.Estado = personalLogActualizar.Estado;
                            personalLogBO.UsuarioCreacion = personalLogActualizar.UsuarioCreacion;
                            personalLogBO.UsuarioModificacion = personalLogActualizar.UsuarioModificacion;
                            personalLogBO.FechaCreacion = personalLogActualizar.FechaCreacion;
                            personalLogBO.FechaModificacion = personalLogActualizar.FechaModificacion;
                            _repPersonalLog.Insert(personalLogBO);
                        }
                    }
                    //Fin de T_Personal Log 
                    if (Compuesto.PersonalDireccion.EsModificado)
                    {
                        var listaPersonalDireccion = _repPersonalDireccion.GetBy(x => x.IdPersonal == personal.Id && x.Activo == true).ToList();
                        foreach (var item in listaPersonalDireccion)
                        {
                            item.Activo = false;
                            item.UsuarioModificacion = Compuesto.Usuario;
                            item.FechaModificacion = DateTime.Now;
                            _repPersonalDireccion.Update(item);
                        }
                        if (Compuesto.PersonalDireccion.IdPais.HasValue)
                        {
                            PersonalDireccionBO personalDireccion = new PersonalDireccionBO
                            {
                                IdPersonal = personal.Id,
                                IdPais = Compuesto.PersonalDireccion.IdPais,
                                IdCiudad = Compuesto.PersonalDireccion.IdCiudad,
                                Distrito = Compuesto.PersonalDireccion.Distrito == "" ? null : Compuesto.PersonalDireccion.Distrito,
                                TipoVia = Compuesto.PersonalDireccion.TipoVia == "" ? null : Compuesto.PersonalDireccion.TipoVia,
                                TipoZonaUrbana = Compuesto.PersonalDireccion.TipoZonaUrbana == "" ? null : Compuesto.PersonalDireccion.TipoZonaUrbana,
                                NombreVia = Compuesto.PersonalDireccion.NombreVia == "" ? null : Compuesto.PersonalDireccion.NombreVia,
                                NombreZonaUrbana = Compuesto.PersonalDireccion.NombreZonaUrbana == "" ? null : Compuesto.PersonalDireccion.NombreZonaUrbana,
                                Manzana = Compuesto.PersonalDireccion.Manzana == "" ? null : Compuesto.PersonalDireccion.Manzana,
                                Lote = Compuesto.PersonalDireccion.Lote,
                                Activo = true,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repPersonalDireccion.Insert(personalDireccion);
                        }
                    }
                    if (Compuesto.PersonalCese.EsModificado)
                    {
                        var listaPersonalCese = _repPersonalCese.GetBy(x => x.IdPersonal == personal.Id);
                        foreach (var item in listaPersonalCese)
                        {
                            _repPersonalCese.Delete(item.Id, Compuesto.Usuario);
                        }
                        if (Compuesto.PersonalCese.IdMotivoCese.HasValue && Compuesto.PersonalCese.FechaCese.HasValue)
                        {
                            var ultimoContrato = _repDatoContratoPersonal.GetBy(x => x.IdPersonal == personal.Id && x.EstadoContrato == true).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();
                            if (ultimoContrato != null)
                            {
                                ultimoContrato.EstadoContrato = false;
                                ultimoContrato.IdContratoEstado = Compuesto.PersonalCese.IdContratoEstado;
                                ultimoContrato.UsuarioModificacion = Compuesto.Usuario;
                                ultimoContrato.FechaModificacion = DateTime.Now;
                                _repDatoContratoPersonal.Update(ultimoContrato);
                            }
                            PersonalCeseBO personalCese = new PersonalCeseBO
                            {
                                IdMotivoCese = Compuesto.PersonalCese.IdMotivoCese.Value,
                                FechaCese = Compuesto.PersonalCese.FechaCese.Value,
                                IdPersonal = personal.Id,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repPersonalCese.Insert(personalCese);

                            var personalLogActualizarTipoAsesor = _repPersonalLog.GetBy(x => x.IdPersonal == personal.Id && x.EstadoCerrador == true).OrderByDescending(x => x.Id).FirstOrDefault();
                            if (personalLogActualizarTipoAsesor != null && personalLogActualizarTipoAsesor.FechaFin == null)
                            {
                                personalLogActualizarTipoAsesor.FechaFin = DateTime.Now.Date;
                                _repPersonalLog.Update(personalLogActualizarTipoAsesor);
                            }
                        }
                    }
                    if (Compuesto.PersonalDescanso.EsModificado)
                    {
                        var banderaDescansoNuevo = false;
                        var listaPersonalDescanso = _repPersonalMotivoTiempoInactividad.GetBy(x => x.IdPersonal == personal.Id).FirstOrDefault();
                        if (listaPersonalDescanso != null)
                        {
                            if (listaPersonalDescanso.IdMotivoInactividad == Compuesto.PersonalDescanso.IdMotivoInactividad && listaPersonalDescanso.FechaInicio == Compuesto.PersonalDescanso.FechaInicioDescanso.GetValueOrDefault().Date && listaPersonalDescanso.FechaFin == Compuesto.PersonalDescanso.FechaFinDescanso.GetValueOrDefault().Date && Compuesto.PersonalDescanso.FechaInicioDescanso != null && Compuesto.PersonalDescanso.FechaFinDescanso != null)
                            {
                                banderaDescansoNuevo = true;
                            }
                            if (!banderaDescansoNuevo)
                            {
                                _repPersonalMotivoTiempoInactividad.Delete(listaPersonalDescanso.Id, Compuesto.Usuario);
                                PersonalMotivoTiempoInactividadBO personalDescanso = new PersonalMotivoTiempoInactividadBO
                                {
                                    IdPersonal = personal.Id,
                                    IdMotivoInactividad = Compuesto.PersonalDescanso.IdMotivoInactividad.GetValueOrDefault(),
                                    FechaInicio = Compuesto.PersonalDescanso.FechaInicioDescanso,
                                    FechaFin = Compuesto.PersonalDescanso.FechaFinDescanso,
                                    Estado = true,
                                    UsuarioCreacion = Compuesto.Usuario,
                                    UsuarioModificacion = Compuesto.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                _repPersonalMotivoTiempoInactividad.Insert(personalDescanso);
                            }
                        }
                        else
                        {
                            PersonalMotivoTiempoInactividadBO personalDescanso = new PersonalMotivoTiempoInactividadBO
                            {
                                IdPersonal = personal.Id,
                                IdMotivoInactividad = Compuesto.PersonalDescanso.IdMotivoInactividad.GetValueOrDefault(),
                                FechaInicio = Compuesto.PersonalDescanso.FechaInicioDescanso,
                                FechaFin = Compuesto.PersonalDescanso.FechaFinDescanso,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repPersonalMotivoTiempoInactividad.Insert(personalDescanso);
                        }
                    }
                    // En caso de pasar a estado Activo se elimina tiempo de inactividad de personal
                    if (estadoPersonalAnterior != null)
                    {
                        if (personal.Activo != estadoPersonalAnterior && personal.Activo == true)
                        {
                            var listaPersonalDescanso = _repPersonalMotivoTiempoInactividad.GetBy(x => x.IdPersonal == personal.Id).FirstOrDefault();
                            if (listaPersonalDescanso != null)
                            {
                                _repPersonalMotivoTiempoInactividad.Delete(listaPersonalDescanso.Id, Compuesto.Usuario);
                            }
                        }
                    }
                    if (Compuesto.PersonalSistemaPensionario.EsModificado)
                    {
                        var listaSistemaPensionario = _repPersonalSistemaPensionario.GetBy(x => x.IdPersonal == personal.Id && x.Activo == true).ToList();
                        foreach (var item in listaSistemaPensionario)
                        {
                            item.Activo = false;
                            item.UsuarioModificacion = Compuesto.Usuario;
                            item.FechaModificacion = DateTime.Now;
                            _repPersonalSistemaPensionario.Update(item);
                        }
                        if (Compuesto.PersonalSistemaPensionario.IdSistemaPensionario.HasValue)
                        {
                            PersonalSistemaPensionarioBO personalSistemaPensionario = new PersonalSistemaPensionarioBO
                            {
                                CodigoAfiliado = Compuesto.PersonalSistemaPensionario.CodigoAfiliado,
                                IdEntidadSistemaPensionario = Compuesto.PersonalSistemaPensionario.IdEntidadSistemaPensionario,
                                IdSistemaPensionario = Compuesto.PersonalSistemaPensionario.IdSistemaPensionario.Value,
                                IdPersonal = personal.Id,
                                Activo = true,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repPersonalSistemaPensionario.Insert(personalSistemaPensionario);
                        }
                    }
                    if (Compuesto.PersonalSeguroSalud.EsModificado)
                    {
                        var listaSeguroSalud = _repPersonalSeguroSalud.GetBy(x => x.IdPersonal == personal.Id && x.Activo == true).ToList();
                        foreach (var item in listaSeguroSalud)
                        {
                            item.Activo = false;
                            item.UsuarioModificacion = Compuesto.Usuario;
                            item.FechaModificacion = DateTime.Now;
                            _repPersonalSeguroSalud.Update(item);
                        }
                        if (Compuesto.PersonalSeguroSalud.IdEntidadSeguroSalud.HasValue)
                        {
                            PersonalSeguroSaludBO personalSeguroSalud = new PersonalSeguroSaludBO()
                            {
                                IdEntidadSeguroSalud = Compuesto.PersonalSeguroSalud.IdEntidadSeguroSalud.Value,
                                IdPersonal = personal.Id,
                                Activo = true,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repPersonalSeguroSalud.Insert(personalSeguroSalud);
                        }
                    }
                    if (Compuesto.PersonalRemuneracion.EsModificado)
                    {
                        var listaPersonalRemuneracion = _repPersonalRemuneracion.GetBy(x => x.IdPersonal == personal.Id && x.Activo == true).ToList();
                        foreach (var item in listaPersonalRemuneracion)
                        {
                            item.Activo = false;
                            item.UsuarioModificacion = Compuesto.Usuario;
                            item.FechaModificacion = DateTime.Now;
                            _repPersonalRemuneracion.Update(item);
                        }
                        if (Compuesto.PersonalRemuneracion.IdTipoPagoRemuneracion.HasValue)
                        {
                            PersonalRemuneracionBO personalSeguroSalud = new PersonalRemuneracionBO()
                            {
                                IdTipoPagoRemuneracion = Compuesto.PersonalRemuneracion.IdTipoPagoRemuneracion.Value,
                                IdEntidadFinanciera = Compuesto.PersonalRemuneracion.IdEntidadFinanciera,
                                IdPersonal = personal.Id,
                                NumeroCuenta = Compuesto.PersonalRemuneracion.NumeroCuenta,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Activo = true,
                            };
                            _repPersonalRemuneracion.Insert(personalSeguroSalud);
                        }
                    }
                    foreach (var item in PersonalCertificacion)
                    {
                        if (!Compuesto.PersonalCertificacion.Any(x => x.Id == item.Id))
                        {
                            _repPersonalCertificacion.Delete(item.Id, Compuesto.Usuario);
                        }
                    }
                    foreach (var item in Compuesto.PersonalCertificacion)
                    {
                        PersonalCertificacionBO personalCertificacion;
                        if (item.Id > 0)
                        {
                            personalCertificacion = _repPersonalCertificacion.FirstById(item.Id);
                            personalCertificacion.Institucion = "";
                            personalCertificacion.Programa = item.Programa;
                            personalCertificacion.FechaCertificacion = item.FechaCertificacion;
                            personalCertificacion.UsuarioModificacion = Compuesto.Usuario;
                            personalCertificacion.FechaModificacion = DateTime.Now;
                            personalCertificacion.IdPersonalArchivo = item.IdPersonalArchivo;
                            personalCertificacion.IdCentroEstudio = item.IdCentroEstudio;
                            _repPersonalCertificacion.Update(personalCertificacion);
                        }
                        else
                        {
                            personalCertificacion = new PersonalCertificacionBO
                            {
                                Institucion = "",
                                Programa = item.Programa,
                                FechaCertificacion = item.FechaCertificacion,
                                IdPersonal = Compuesto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdPersonalArchivo = item.IdPersonalArchivo,
                                IdCentroEstudio = item.IdCentroEstudio,
                            };
                            _repPersonalCertificacion.Insert(personalCertificacion);
                        }
                    }
                    foreach (var item in PersonalExperiencia)
                    {
                        if (!Compuesto.PersonalExperiencia.Any(x => x.Id == item.Id))
                        {
                            _repPersonalExperiencia.Delete(item.Id, Compuesto.Usuario);
                        }
                    }
                    foreach (var item in Compuesto.PersonalExperiencia)
                    {
                        PersonalExperienciaBO personalExperiencia;
                        if (item.Id > 0)
                        {
                            personalExperiencia = _repPersonalExperiencia.FirstById(item.Id);
                            personalExperiencia.FechaIngreso = item.FechaIngreso;
                            personalExperiencia.FechaRetiro = item.FechaRetiro;
                            personalExperiencia.IdAreaTrabajo = item.IdAreaTrabajo;
                            personalExperiencia.IdCargo = item.IdCargo;
                            personalExperiencia.IdEmpresa = item.IdEmpresa;
                            personalExperiencia.MotivoRetiro = item.MotivoRetiro;
                            personalExperiencia.NombreJefeInmediato = item.NombreJefeInmediato;
                            personalExperiencia.TelefonoJefeInmediato = item.TelefonoJefeInmediato;
                            personalExperiencia.UsuarioModificacion = Compuesto.Usuario;
                            personalExperiencia.FechaModificacion = DateTime.Now;
                            personalExperiencia.IdPersonalArchivo = item.IdPersonalArchivo;
                            _repPersonalExperiencia.Update(personalExperiencia);
                        }
                        else
                        {
                            personalExperiencia = new PersonalExperienciaBO
                            {
                                IdPersonal = Compuesto.Personal.Id,
                                FechaIngreso = item.FechaIngreso,
                                FechaRetiro = item.FechaRetiro,
                                IdAreaTrabajo = item.IdAreaTrabajo,
                                IdCargo = item.IdCargo,
                                IdEmpresa = item.IdEmpresa,
                                MotivoRetiro = item.MotivoRetiro,
                                NombreJefeInmediato = item.NombreJefeInmediato,
                                TelefonoJefeInmediato = item.TelefonoJefeInmediato,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdPersonalArchivo = item.IdPersonalArchivo,
                            };
                            _repPersonalExperiencia.Insert(personalExperiencia);
                        }
                    }
                    foreach (var item in PersonalFamiliar)
                    {
                        if (!Compuesto.PersonalFamiliar.Any(x => x.Id == item.Id))
                        {
                            _repDatoFamiliarPersonal.Delete(item.Id, Compuesto.Usuario);
                        }
                    }
                    foreach (var item in Compuesto.PersonalFamiliar)
                    {
                        DatoFamiliarPersonalBO datoFamiliarPersonal;
                        if (item.Id > 0)
                        {
                            datoFamiliarPersonal = _repDatoFamiliarPersonal.FirstById(item.Id);
                            datoFamiliarPersonal.Apellidos = item.Apellidos;
                            datoFamiliarPersonal.Nombres = item.Nombres;
                            datoFamiliarPersonal.DerechoHabiente = item.DerechoHabiente;
                            datoFamiliarPersonal.EsContactoInmediato = item.EsContactoInmediato;
                            datoFamiliarPersonal.FechaNacimiento = item.FechaNacimiento;
                            datoFamiliarPersonal.IdParentescoPersonal = item.IdParentescoPersonal;
                            datoFamiliarPersonal.IdSexo = item.IdSexo;
                            datoFamiliarPersonal.IdTipoDocumentoPersonal = item.IdTipoDocumentoPersonal;
                            datoFamiliarPersonal.NumeroDocumento = item.NumeroDocumento;
                            datoFamiliarPersonal.NumeroReferencia1 = item.NumeroReferencia;
                            datoFamiliarPersonal.UsuarioModificacion = Compuesto.Usuario;
                            datoFamiliarPersonal.FechaModificacion = DateTime.Now;
                            _repDatoFamiliarPersonal.Update(datoFamiliarPersonal);
                        }
                        else
                        {
                            datoFamiliarPersonal = new DatoFamiliarPersonalBO
                            {
                                Apellidos = item.Apellidos,
                                Nombres = item.Nombres,
                                DerechoHabiente = item.DerechoHabiente,
                                EsContactoInmediato = item.EsContactoInmediato,
                                FechaNacimiento = item.FechaNacimiento,
                                IdParentescoPersonal = item.IdParentescoPersonal,
                                IdSexo = item.IdSexo,
                                IdTipoDocumentoPersonal = item.IdTipoDocumentoPersonal,
                                NumeroDocumento = item.NumeroDocumento,
                                NumeroReferencia1 = item.NumeroReferencia,
                                IdPersonal = Compuesto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repDatoFamiliarPersonal.Insert(datoFamiliarPersonal);
                        }
                    }
                    foreach (var item in PersonalFormacion)
                    {
                        if (!Compuesto.PersonalFormacion.Any(x => x.Id == item.Id))
                        {
                            _repPersonalFormacion.Delete(item.Id, Compuesto.Usuario);
                        }
                    }
                    foreach (var item in Compuesto.PersonalFormacion)
                    {
                        PersonalFormacionBO personalFormacion;
                        if (item.Id > 0)
                        {
                            personalFormacion = _repPersonalFormacion.FirstById(item.Id);
                            personalFormacion.AlaActualidad = item.AlaActualidad;
                            personalFormacion.FechaFin = item.FechaFin;
                            personalFormacion.FechaInicio = item.FechaInicio;
                            personalFormacion.IdAreaFormacion = item.IdAreaFormacion;
                            personalFormacion.IdCentroEstudio = item.IdCentroEstudio;
                            personalFormacion.IdEstadoEstudio = item.IdEstadoEstudio;
                            personalFormacion.IdTipoEstudio = item.IdTipoEstudio;
                            personalFormacion.Logro = item.Logro;
                            personalFormacion.UsuarioModificacion = Compuesto.Usuario;
                            personalFormacion.FechaModificacion = DateTime.Now;
                            personalFormacion.IdPersonalArchivo = item.IdPersonalArchivo;
                            _repPersonalFormacion.Update(personalFormacion);
                        }
                        else
                        {
                            personalFormacion = new PersonalFormacionBO
                            {
                                AlaActualidad = item.AlaActualidad,
                                FechaFin = item.FechaFin,
                                FechaInicio = item.FechaInicio,
                                IdAreaFormacion = item.IdAreaFormacion,
                                IdCentroEstudio = item.IdCentroEstudio,
                                IdEstadoEstudio = item.IdEstadoEstudio,
                                IdTipoEstudio = item.IdTipoEstudio,
                                Logro = item.Logro,
                                IdPersonal = Compuesto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdPersonalArchivo = item.IdPersonalArchivo,
                            };
                            _repPersonalFormacion.Insert(personalFormacion);
                        }
                    }
                    foreach (var item in PersonalHistorialMedico)
                    {
                        if (!Compuesto.PersonalHistorialMedico.Any(x => x.Id == item.Id))
                        {
                            _repPersonalHistorialMedico.Delete(item.Id, Compuesto.Usuario);
                        }
                    }
                    foreach (var item in Compuesto.PersonalHistorialMedico)
                    {
                        PersonalHistorialMedicoBO personalHistorialMedico;
                        if (item.Id > 0)
                        {
                            personalHistorialMedico = _repPersonalHistorialMedico.FirstById(item.Id);
                            personalHistorialMedico.Enfermedad = item.Enfermedad;
                            personalHistorialMedico.DetalleEnfermedad = item.DetalleEnfermedad;
                            personalHistorialMedico.Periodo = item.Periodo;
                            personalHistorialMedico.UsuarioModificacion = Compuesto.Usuario;
                            personalHistorialMedico.FechaModificacion = DateTime.Now;
                            _repPersonalHistorialMedico.Update(personalHistorialMedico);
                        }
                        else
                        {
                            personalHistorialMedico = new PersonalHistorialMedicoBO
                            {
                                Enfermedad = item.Enfermedad,
                                DetalleEnfermedad = item.DetalleEnfermedad,
                                Periodo = item.Periodo,
                                IdPersonal = Compuesto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repPersonalHistorialMedico.Insert(personalHistorialMedico);
                        }
                    }
                    foreach (var item in PersonalIdiomas)
                    {
                        if (!Compuesto.PersonalIdiomas.Any(x => x.Id == item.Id))
                        {
                            _repPersonalIdioma.Delete(item.Id, Compuesto.Usuario);
                        }
                    }
                    foreach (var item in Compuesto.PersonalIdiomas)
                    {
                        PersonalIdiomaBO personalIdioma;
                        if (item.Id > 0)
                        {
                            personalIdioma = _repPersonalIdioma.FirstById(item.Id);
                            personalIdioma.IdCentroEstudio = item.IdCentroEstudio;
                            personalIdioma.IdIdioma = item.IdIdioma;
                            personalIdioma.IdNivelIdioma = item.IdNivelIdioma;
                            personalIdioma.UsuarioModificacion = Compuesto.Usuario;
                            personalIdioma.FechaModificacion = DateTime.Now;
                            personalIdioma.IdPersonalArchivo = item.IdPersonalArchivo;
                            _repPersonalIdioma.Update(personalIdioma);
                        }
                        else
                        {
                            personalIdioma = new PersonalIdiomaBO
                            {
                                IdCentroEstudio = item.IdCentroEstudio,
                                IdIdioma = item.IdIdioma,
                                IdNivelIdioma = item.IdNivelIdioma,
                                IdPersonal = Compuesto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdPersonalArchivo = item.IdPersonalArchivo,
                            };
                            _repPersonalIdioma.Insert(personalIdioma);
                        }
                    }
                    foreach (var item in PersonalInformacionMedica)
                    {
                        if (!Compuesto.PersonalInformacionMedica.Any(x => x.Id == item.Id))
                        {
                            _repPersonalInformacionMedica.Delete(item.Id, Compuesto.Usuario);
                        }
                    }
                    foreach (var item in Compuesto.PersonalInformacionMedica)
                    {
                        PersonalInformacionMedicaBO personalInformacionMedica;
                        if (item.Id > 0)
                        {
                            personalInformacionMedica = _repPersonalInformacionMedica.FirstById(item.Id);
                            personalInformacionMedica.Alergia = item.Alergia;
                            personalInformacionMedica.IdTipoSangre = Compuesto.Personal.IdTipoSangre;
                            personalInformacionMedica.Precaucion = item.Precaucion;
                            personalInformacionMedica.UsuarioModificacion = Compuesto.Usuario;
                            personalInformacionMedica.FechaModificacion = DateTime.Now;
                            _repPersonalInformacionMedica.Update(personalInformacionMedica);
                        }
                        else
                        {
                            personalInformacionMedica = new PersonalInformacionMedicaBO
                            {
                                Alergia = item.Alergia,
                                IdTipoSangre = Compuesto.Personal.IdTipoSangre,
                                Precaucion = item.Precaucion,
                                IdPersonal = Compuesto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repPersonalInformacionMedica.Insert(personalInformacionMedica);
                        }
                    }
                    foreach (var item in PersonalInformatica)
                    {
                        if (!Compuesto.PersonalInformatica.Any(x => x.Id == item.Id))
                        {
                            _repPersonalComputo.Delete(item.Id, Compuesto.Usuario);
                        }
                    }
                    foreach (var item in Compuesto.PersonalInformatica)
                    {
                        PersonalComputoBO personalComputo;
                        if (item.Id > 0)
                        {
                            personalComputo = _repPersonalComputo.FirstById(item.Id);
                            personalComputo.IdCentroEstudio = item.IdCentroEstudio;
                            personalComputo.IdNivelCompetenciaTecnica = item.IdNivelCompetenciaTecnica;
                            personalComputo.Programa = item.Programa;
                            personalComputo.UsuarioModificacion = Compuesto.Usuario;
                            personalComputo.FechaModificacion = DateTime.Now;
                            personalComputo.IdPersonalArchivo = item.IdPersonalArchivo;
                            _repPersonalComputo.Update(personalComputo);
                        }
                        else
                        {
                            personalComputo = new PersonalComputoBO
                            {
                                IdCentroEstudio = item.IdCentroEstudio,
                                IdNivelCompetenciaTecnica = item.IdNivelCompetenciaTecnica,
                                Programa = item.Programa,
                                IdPersonal = Compuesto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = Compuesto.Usuario,
                                UsuarioModificacion = Compuesto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdPersonalArchivo = item.IdPersonalArchivo,
                            };
                            _repPersonalComputo.Insert(personalComputo);
                        }
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gian Miranda
        /// Fecha: 24/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta o actualiza los accesos temporales del personal
        /// </summary>
        /// <param name="AccesoTemporal">Objeto de clase ActualizarAccesoTemporalDTO con los parametros necesarios para el eliminado</param>
        /// <returns>Lista de objetos de clase MaestroPersonalGrupoAccesoTemporalDTO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarAccesoTemporal([FromBody] ActualizarAccesoTemporalDTO AccesoTemporal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (AccesoTemporal.FechaInicio > AccesoTemporal.FechaFin)
                    return BadRequest("La fecha de fin del acceso temporal debe ser mayor o igual a la fecha de inicio");

                string emailPersonalSolicitado = string.Empty;

                var personalSolicitado = _repPersonal.FirstBy(x => x.Id == AccesoTemporal.IdPersonal);
                if (personalSolicitado == null)
                    return BadRequest("El personal al que se desea dar acceso temporal no existe");
                else
                    emailPersonalSolicitado = personalSolicitado.Email;

                var pEspecificoPadre = _repPEspecifico.FirstBy(x => x.Id == AccesoTemporal.IdPEspecificoPadre);
                if (pEspecificoPadre == null)
                    return BadRequest("No existe el programa especifico");

                IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                if (!_repIntegraAspNetUsers.ExistePorNombreUsuario(AccesoTemporal.Usuario))
                    return BadRequest("El usuario no existe");

                List<MaestroPersonalGrupoAccesoTemporalDTO> listaAccesoTemporal = new List<MaestroPersonalGrupoAccesoTemporalDTO>();
                ReemplazoEtiquetaPlantillaBO reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext);
                // Email del usuario que realiza la modificacion
                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(AccesoTemporal.Usuario);

                bool resultadoActualizacion = PersonalAccesoTemporalAulaVirtual.ActualizarAccesosTemporalesIntegra(AccesoTemporal);

                if (!resultadoActualizacion)
                    return BadRequest("Hubo un fallo en la actualizacion de los accesos temporales");
                else
                {
                    var alumnoPersonal = _repAlumno.FirstBy(x => x.Email1 == personalSolicitado.Email);

                    if (alumnoPersonal == null)
                        return BadRequest("No se ha encontrado un alumno con el correo del personal");

                    EtiquetaParametroAlumnoSinOportunidadDTO parametrosEtiquetas = new EtiquetaParametroAlumnoSinOportunidadDTO
                    {
                        IdAlumno = alumnoPersonal.Id,
                        IdPGeneral = pEspecificoPadre.IdProgramaGeneral
                    };

                    reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdPlantilla = ValorEstatico.IdPlantillaAccesoTemporalPersonalMailing
                    };

                    reemplazoEtiquetaPlantilla.ReemplazarEtiquetasAlumnoSinOportunidad(parametrosEtiquetas);

                    PlantillaEmailMandrillDTO emailFinalEnvio = reemplazoEtiquetaPlantilla.EmailReemplazado;

                    List<string> correosPersonalizados = new List<string>
                    {
                        alumnoPersonal.Email1
                    };

                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = "matriculas@bsginstitute.com",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailFinalEnvio.Asunto,
                        Message = emailFinalEnvio.CuerpoHTML,
                        Cc = usuarioResponsable,
                        Bcc = "gmiranda@bsginstitute.com",
                        AttachedFiles = null
                    };

                    TMK_MailServiceImpl mailService = new TMK_MailServiceImpl();
                    mailService.SetData(mailDataPersonalizado);

                    List<TMKMensajeIdDTO> listaIdsMailChimp = mailService.SendMessageTask();
                    List<MandrilEnvioCorreoBO> listaMandrilEnvioCorreoBO = new List<MandrilEnvioCorreoBO>();

                    foreach (var mensaje in listaIdsMailChimp)
                    {
                        var mandrilEnvioCorreoBO = new MandrilEnvioCorreoBO
                        {
                            IdOportunidad = null,
                            IdPersonal = personalSolicitado.Id,
                            IdAlumno = alumnoPersonal.Id,
                            IdCentroCosto = pEspecificoPadre.IdCentroCosto,
                            IdMandrilTipoAsignacion = 1, //Correos enviados automaticos
                            EstadoEnvio = 1,
                            IdMandrilTipoEnvio = 1, // Correo enviado automaticamente
                            FechaEnvio = DateTime.Now,
                            Asunto = emailFinalEnvio.Asunto,
                            FkMandril = mensaje.MensajeId,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "EnvioAutomaticoGP",
                            UsuarioModificacion = "EnvioAutomaticoGP",
                            EsEnvioMasivo = false
                        };

                        if (!mandrilEnvioCorreoBO.HasErrors)
                        {
                            listaMandrilEnvioCorreoBO.Add(mandrilEnvioCorreoBO);
                        }
                        else
                        {
                            continue;
                        }
                    }

                    _repMandrilEnvioCorreo.Insert(listaMandrilEnvioCorreoBO);
                }

                listaAccesoTemporal = PersonalAccesoTemporalAulaVirtual.ObtenerListaAccesoTemporal(AccesoTemporal.IdPersonal);

                return Ok(new { ListaAccesoTemporal = listaAccesoTemporal });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Gian Miranda
        /// Fecha: 24/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina logicamente los accesos temporales de un personal
        /// </summary>
        /// <param name="AccesoTemporal">Objeto de clase EliminarAccesoTemporalDTO con los parametros necesarios para el eliminado</param>
        /// <returns>Lista de objetos de clase MaestroPersonalGrupoAccesoTemporalDTO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EliminarAccesoTemporal([FromBody] EliminarAccesoTemporalDTO AccesoTemporal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<MaestroPersonalGrupoAccesoTemporalDTO> listaAccesoTemporal = new List<MaestroPersonalGrupoAccesoTemporalDTO>();

                if (_repPersonal.Exist(AccesoTemporal.IdPersonal))
                {
                    bool resultado = _repPersonalAccesoTemporalAulaVirtual.EliminarAccesoTemporalPorIdPEspecificoPadre(AccesoTemporal.IdPersonal, AccesoTemporal.IdPEspecificoPadre, AccesoTemporal.FechaInicio, AccesoTemporal.FechaFin, AccesoTemporal.NombreUsuario);

                    var personal = _repPersonal.FirstById(AccesoTemporal.IdPersonal);
                    var idPortalWeb = _repPersonalAccesoTemporalAulaVirtual.ObtenerIdUsuarioPortalWebCorreo(personal.Email);
                    var alumno = _repAlumno.FirstBy(x => x.Email1 == personal.Email);

                    if (alumno == null)
                    {
                        return BadRequest("El alumno no existe");
                    }

                    bool resultadoPortalWeb = _repPersonalAccesoTemporalAulaVirtual.ActualizarAccesosTemporalesPortalWeb(AccesoTemporal.IdPersonal, idPortalWeb, alumno.Id);

                    if (!resultado || !resultadoPortalWeb)
                    {
                        return BadRequest("No se pudo eliminar el registro");
                    }
                }
                else
                {
                    return BadRequest("El elemento no existe o ya fue eliminado");
                }

                listaAccesoTemporal = PersonalAccesoTemporalAulaVirtual.ObtenerListaAccesoTemporal(AccesoTemporal.IdPersonal);

                return Ok(new { ListaAccesoTemporal = listaAccesoTemporal });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina logicamente el registro de un personal
        /// </summary>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EliminarPersonal([FromBody] EliminarDTO Personal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repPersonal.Exist(Personal.Id))
                    {
                        var personalCertificacion = _repPersonalCertificacion.GetBy(x => x.IdPersonal == Personal.Id).ToList();
                        var personalExperiencia = _repPersonalExperiencia.GetBy(x => x.IdPersonal == Personal.Id).ToList();
                        var personalFamiliar = _repDatoFamiliarPersonal.GetBy(x => x.IdPersonal == Personal.Id).ToList();
                        var personalFormacion = _repPersonalFormacion.GetBy(x => x.IdPersonal == Personal.Id).ToList();
                        var personalHistorialMedico = _repPersonalHistorialMedico.GetBy(x => x.IdPersonal == Personal.Id).ToList();
                        var personalIdiomas = _repPersonalIdioma.GetBy(x => x.IdPersonal == Personal.Id).ToList();
                        var personalInformacionMedica = _repPersonalInformacionMedica.GetBy(x => x.IdPersonal == Personal.Id).ToList();
                        var personalInformatica = _repPersonalComputo.GetBy(x => x.IdPersonal == Personal.Id).ToList();
                        var personalSeguroSalud = _repPersonalSeguroSalud.FirstBy(x => x.IdPersonal == Personal.Id && x.Activo == true);
                        var personalSistemaPensionario = _repPersonalSistemaPensionario.FirstBy(x => x.IdPersonal == Personal.Id && x.Activo == true);
                        var personalRemuneracion = _repPersonalRemuneracion.FirstBy(x => x.IdPersonal == Personal.Id && x.Activo == true);

                        if (personalRemuneracion != null)
                        {
                            _repPersonalRemuneracion.Delete(personalRemuneracion.Id, Personal.NombreUsuario);
                        }

                        if (personalSeguroSalud != null)
                        {
                            _repPersonalSeguroSalud.Delete(personalSeguroSalud.Id, Personal.NombreUsuario);
                        }

                        if (personalSistemaPensionario != null)
                        {
                            _repPersonalSistemaPensionario.Delete(personalSistemaPensionario.Id, Personal.NombreUsuario);
                        }

                        foreach (var item in personalCertificacion)
                        {
                            _repPersonalCertificacion.Delete(item.Id, Personal.NombreUsuario);
                        }
                        foreach (var item in personalExperiencia)
                        {
                            _repPersonalExperiencia.Delete(item.Id, Personal.NombreUsuario);
                        }
                        foreach (var item in personalFamiliar)
                        {
                            _repDatoFamiliarPersonal.Delete(item.Id, Personal.NombreUsuario);
                        }
                        foreach (var item in personalFormacion)
                        {
                            _repPersonalFormacion.Delete(item.Id, Personal.NombreUsuario);
                        }
                        foreach (var item in personalHistorialMedico)
                        {
                            _repPersonalHistorialMedico.Delete(item.Id, Personal.NombreUsuario);
                        }
                        foreach (var item in personalIdiomas)
                        {
                            _repPersonalIdioma.Delete(item.Id, Personal.NombreUsuario);
                        }
                        foreach (var item in personalInformacionMedica)
                        {
                            _repPersonalInformacionMedica.Delete(item.Id, Personal.NombreUsuario);
                        }
                        foreach (var item in personalInformatica)
                        {
                            _repPersonalComputo.Delete(item.Id, Personal.NombreUsuario);
                        }

                        try
                        {
                            _repPersonalAccesoTemporalAulaVirtual.EliminarAccesoTemporalPorIdPersonal(Personal.Id, Personal.NombreUsuario);
                        }
                        catch (Exception e)
                        {
                        }

                        _repPersonal.Delete(Personal.Id, Personal.NombreUsuario);
                    }
                    else
                    {
                        return BadRequest("El elemento no existe o ya fue eliminado");
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 16/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Insertar información de archivo para archivos del personal
        /// </summary>
        /// <param name="ArchivoPersonal">DTO con información de Formato y Usuario de Interfaz</param>
        /// <returns>Objeto Agrupado</returns>
        [Route("[action]")]
        [HttpPost]
        [RequestSizeLimit(200000000)]
        public ActionResult AdjuntarArchivoPersonal([FromForm] ArchivoPersonalDTO ArchivoPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalArchivoRepositorio _repPersonalArchivo = new PersonalArchivoRepositorio();
                string nombreArchivo = "";
                string nombreArchivotemp = "";
                string contentType = "";
                var urlArchivoRepositorio = "";
                if (ArchivoPersonal.File != null)
                {
                    UtilBO utilBO = new UtilBO();
                    contentType = ArchivoPersonal.File.ContentType;
                    nombreArchivo = ArchivoPersonal.File.FileName;
                    nombreArchivotemp = ArchivoPersonal.File.FileName;
                    nombreArchivotemp = DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + utilBO.SlugNombreArchivo(nombreArchivotemp);
                    urlArchivoRepositorio = _repPersonalArchivo.SubirDocumentosPersonal(ArchivoPersonal.File.ConvertToByte(), ArchivoPersonal.File.ContentType, nombreArchivotemp);
                }
                else
                {
                    return BadRequest("No se subió ningún archivo.");
                }

                if (string.IsNullOrEmpty(urlArchivoRepositorio))
                {
                    return BadRequest("Ocurrió un problema al subir el archivo.");
                }
                bool esImagen = false;
                if (ArchivoPersonal.File.ContentType.Contains("image"))
                {
                    esImagen = true;
                }
                var agregarArchivo = new PersonalArchivoBO
                {
                    NombreArchivo = nombreArchivo,
                    RutaArchivo = urlArchivoRepositorio,
                    MimeType = ArchivoPersonal.File.ContentType,
                    EsImagen = esImagen,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = ArchivoPersonal.Usuario,
                    UsuarioModificacion = ArchivoPersonal.Usuario
                };
                var resultado = _repPersonalArchivo.Insert(agregarArchivo);
                if(ArchivoPersonal.Id != null)
                {
                    var personal = _repPersonal.FirstById((int) ArchivoPersonal.Id);
                    if (personal != null)
                    {
                        personal.IdPersonalArchivo = agregarArchivo.Id;
                        personal.UsuarioModificacion = ArchivoPersonal.Usuario;
                        personal.FechaModificacion = DateTime.Now;
                        _repPersonal.Update(personal);
                    }
                }
                if (resultado)
                {
                    return Ok(new { Respuesta = true, IdArchivo = agregarArchivo.Id });
                }
                else
                {
                    return Ok(new { Respuesta = false });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Edgar Serruto
        /// Fecha: 17/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Archivo para su visualización
        /// </summary>
        /// <param name="IdPersonalArchivo">Id de Archivo de Personal</param>
        /// <returns> Objeto Agrupado </returns>
        [Route("[action]/{IdPersonalArchivo}")]
        [HttpGet]
        public ActionResult ObtenerArchivoPersonal(int IdPersonalArchivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalArchivoRepositorio _repPersonalArchivo = new PersonalArchivoRepositorio(_integraDBContext);
                string htmlArchivo = string.Empty;
                var informacionArchivo = _repPersonalArchivo.FirstById(IdPersonalArchivo);
                if (informacionArchivo != null)
                {
                    if (informacionArchivo.EsImagen.GetValueOrDefault())
                    {
                        htmlArchivo = "<img src='" + informacionArchivo.RutaArchivo + "'style='max-width:500px'>";
                    }
                    else
                    {
                        htmlArchivo = "<a href='" + informacionArchivo.RutaArchivo + "' target='_blank'>Descargar</a>";
                    }
                    return Ok(new { Respuesta = true, Datos = informacionArchivo, Html = htmlArchivo, Mensaje = "Información cargada correctamente" });
                }
                else
                {
                    return Ok(new { Respuesta = false, Datos = informacionArchivo, Html = htmlArchivo, Mensaje = "Error: No se encontró el archivo solicitado" });
                }                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Edgar Serruto
        /// Fecha: 17/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Archivo y realiza conversión para su Descarga
        /// </summary>
        /// <param name="IdPersonalArchivo">Id de Archivo de Personal</param>
        /// <returns> Objeto Agrupado </returns>
        [Route("[action]/{IdPersonalArchivo}")]
        [HttpGet]
        public ActionResult DescargarArchivoPersonal(int IdPersonalArchivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalArchivoRepositorio _repPersonalArchivo = new PersonalArchivoRepositorio(_integraDBContext);
                var informacionArchivo = _repPersonalArchivo.FirstById(IdPersonalArchivo);
                if (informacionArchivo != null)
                {
                    string base64String = string.Empty;
                    if (informacionArchivo.EsImagen.GetValueOrDefault())
                    {
                        using (WebClient webClient = new WebClient())
                        {
                            Image image;
                            using (Stream stream = webClient.OpenRead(informacionArchivo.RutaArchivo))
                            {
                                image = Image.FromStream(stream);
                            }
                            using (MemoryStream m = new MemoryStream())
                            {
                                image.Save(m, image.RawFormat);
                                byte[] imageBytes = m.ToArray();
                                base64String = Convert.ToBase64String(imageBytes);
                            }
                            return Ok(new { Respuesta = true, EsImagen = true, RutaArchivo = base64String, Datos = informacionArchivo, Mensaje = "Información cargada correctamente" });

                        }
                    }
                    else
                    {
                        using (WebClient client = new WebClient())
                        {
                            var bytes = client.DownloadData(informacionArchivo.RutaArchivo);
                            base64String = Convert.ToBase64String(bytes);
                        }
                        return Ok(new { Respuesta = true, EsImagen = false, RutaArchivo = base64String, Datos = informacionArchivo, Mensaje = "Información cargada correctamente" });
                    }
                }
                else
                {
                    return Ok(new { Respuesta = false, Mensaje = "Error: No se encontró el archivo solicitado" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
