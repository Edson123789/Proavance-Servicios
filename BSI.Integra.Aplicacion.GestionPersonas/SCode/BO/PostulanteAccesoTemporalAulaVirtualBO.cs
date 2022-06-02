using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using static BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas.PostulanteAccesoTemporalAulaVirtualDTO;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.BO
{
    /// BO: GestionPersonas/PostulanteAccesoTemporalAulaVirtual
    /// Autor: Edgar Serruto. 
    /// Fecha: 21/06/2021
    /// <summary>
    /// BO para la logica de T_PostulanteAccesoTemporalAulaVirtual
    /// </summary>
    public class PostulanteAccesoTemporalAulaVirtualBO : BaseBO
    {
        /// Propiedades                 Significado
		/// -----------	                ------------
		/// IdPostulante                Id de Postulante
        /// IdPespecificoPadre          Id de Programa Específico Padre
        /// IdPespecificoHijo           Id de Programa Específico Hijo
        /// FechaInicio                 Fecha de Inicio de Acceso
        /// FechaFin                    Fecha de Fin de Acceso
        /// IdMigracion                 Id de Migración
        public int IdPostulante { get; set; }
        public int IdPespecificoPadre { get; set; }
        public int IdPespecificoHijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPostulanteProcesoSeleccion { get; set; }
        public int? IdExamen { get; set; }

        private readonly integraDBContext _integraDBContext;
        private readonly ExamenRepositorio _repExamen;
        private readonly CentroCostoRepositorio _repCentroCosto;
        private readonly PespecificoRepositorio _repPespecifico;
        private readonly PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadrePespecificoHijo;
        private readonly AlumnoRepositorio _repAlumno;
        private readonly PersonaRepositorio _repPersona;
        private readonly PostulanteRepositorio _repPostulante;
        private readonly ClasificacionPersonaRepositorio _repClasificacionPersona;
        private readonly IntegraAspNetUsersRepositorio _repIntegraAspNetUsers;
        private readonly MontoPagoCronogramaRepositorio _repMontoPagoCronograma;
        private readonly PostulanteAccesoTemporalAulaVirtualRepositorio _repPostulanteAccesoTemporalAulaVirtual;
        private readonly PersonalAccesoTemporalAulaVirtualRepositorio _repPersonalAccesoTemporalAulaVirtual;
        private readonly ProcesoSeleccionRepositorio _repProcesoSeleccion;
        private readonly PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion;
        private readonly PostulanteCursoPortalNotasHistoricoRepositorio _repPostulanteCursoPortalNotasHistorico;

        private AlumnoBO Alumno;
        private ClasificacionPersonaBO ClasificacionPersona;
        private string IdPortalWeb;

        public PostulanteAccesoTemporalAulaVirtualBO()
        {
            _repExamen = new ExamenRepositorio();
            _repCentroCosto = new CentroCostoRepositorio();
            _repPespecifico = new PespecificoRepositorio();
            _repPespecificoPadrePespecificoHijo = new PespecificoPadrePespecificoHijoRepositorio();
            _repAlumno = new AlumnoRepositorio();
            _repPersona = new PersonaRepositorio();
            _repPostulante = new PostulanteRepositorio();
            _repClasificacionPersona = new ClasificacionPersonaRepositorio();
            _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio();
            _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio();
            _repPostulanteAccesoTemporalAulaVirtual = new PostulanteAccesoTemporalAulaVirtualRepositorio();
            _repPersonalAccesoTemporalAulaVirtual = new PersonalAccesoTemporalAulaVirtualRepositorio();
            _repProcesoSeleccion = new ProcesoSeleccionRepositorio();
            _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio();
            _repPostulanteCursoPortalNotasHistorico = new PostulanteCursoPortalNotasHistoricoRepositorio();
            Alumno = new AlumnoBO();
            ClasificacionPersona = new ClasificacionPersonaBO();
            IdPortalWeb = string.Empty;
        }
        public PostulanteAccesoTemporalAulaVirtualBO(integraDBContext integraDBContext)
        {
            _repExamen = new ExamenRepositorio(integraDBContext);
            _repCentroCosto = new CentroCostoRepositorio(integraDBContext);
            _repPespecifico = new PespecificoRepositorio(integraDBContext);
            _repPespecificoPadrePespecificoHijo = new PespecificoPadrePespecificoHijoRepositorio(integraDBContext);
            _repAlumno = new AlumnoRepositorio(integraDBContext);
            _repPersona = new PersonaRepositorio(integraDBContext);
            _repPostulante = new PostulanteRepositorio(integraDBContext);
            _repClasificacionPersona = new ClasificacionPersonaRepositorio(integraDBContext);
            _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(integraDBContext);
            _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(integraDBContext);
            _repPostulanteAccesoTemporalAulaVirtual = new PostulanteAccesoTemporalAulaVirtualRepositorio(integraDBContext);
            _repPersonalAccesoTemporalAulaVirtual = new PersonalAccesoTemporalAulaVirtualRepositorio(integraDBContext);
            _repProcesoSeleccion = new ProcesoSeleccionRepositorio(integraDBContext);
            _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(integraDBContext);
            _repPostulanteCursoPortalNotasHistorico = new PostulanteCursoPortalNotasHistoricoRepositorio(integraDBContext);
            Alumno = new AlumnoBO(integraDBContext);
            ClasificacionPersona = new ClasificacionPersonaBO(integraDBContext);
            IdPortalWeb = string.Empty;
        }

        /// Autor: Edgar Serruto
        /// Fecha: 22/06/2021
        /// Version: 1.0
        /// <summary>
        /// Crea Accesos Temporales de Postulante
        /// </summary>
        /// <param name="informacionPostulanteExamen"> Información de Postulante para Creación de Accesos al Portal </param>
        /// <returns> InformacionAccesoPostulanteDTO </returns>
        public InformacionAccesoPostulanteDTO CrearAccesosTemporalesPostulante(EnviarAccesoPostulanteDTO informacionPostulanteExamen)
        {
            try
            {
                InformacionAccesoPostulanteDTO respuestaCreacionAcceso = new InformacionAccesoPostulanteDTO();
                var postulanteProcesoSeleccion = _repPostulanteProcesoSeleccion.GetBy(x => x.IdPostulante == informacionPostulanteExamen.IdPostulante).OrderByDescending(x => x.Id).FirstOrDefault();
                var examen = _repExamen.FirstById(informacionPostulanteExamen.IdExamen);
                var cantidadDias = examen.CantidadDiasAcceso;
                var idPEspecifico = _repPespecifico.GetBy(x => x.IdCentroCosto == examen.IdCentroCosto).FirstOrDefault();
                var idPGeneral = idPEspecifico.IdProgramaGeneral;
                var idPEspecificoPadre = _repPespecificoPadrePespecificoHijo.GetBy(x => x.PespecificoHijoId == idPEspecifico.Id).Select(x => x.PespecificoPadreId).FirstOrDefault();
                if (idPEspecificoPadre == 0)
                {
                    idPEspecificoPadre = idPEspecifico.Id;
                }
                DateTime fechaInicio = DateTime.Now.Date;
                DateTime fechaFin = fechaInicio.AddDays(cantidadDias.GetValueOrDefault() + 1);

                var postulante = _repPostulante.FirstById(informacionPostulanteExamen.IdPostulante);
                IdPortalWeb = _repPersonalAccesoTemporalAulaVirtual.ObtenerIdUsuarioPortalWebCorreo(postulante.Email);
                Alumno = _repAlumno.FirstBy(x => x.Email1 == postulante.Email);
                //Validación de accesos en portal
                if (string.IsNullOrEmpty(IdPortalWeb))
                {
                    // Logica para crear el usuario con sus registros correspondientes en la tabla de alumno, clasificacionpersona, todo si es que tiene un registro en persona
                    var persona = _repPersona.FirstBy(x => x.Email1 == postulante.Email);
                    if (persona == null)
                    {
                        respuestaCreacionAcceso.ValidacionRespuesta = false;
                        return respuestaCreacionAcceso;
                    }
                    if (Alumno == null)
                    {
                        Alumno = new AlumnoBO();
                        //Nombres
                        var nombres = postulante.Nombre.Split(new char[] { ' ' }).ToList().Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();
                        if (nombres.Count == 1)
                        {
                            Alumno.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                            Alumno.Nombre2 = string.Empty;
                        }
                        else if (nombres.Count == 2)
                        {
                            Alumno.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                            Alumno.Nombre2 = nombres[1].Length >= 100 ? nombres[1].Substring(0, 100) : nombres[1];
                        }
                        else if (nombres.Count > 2)
                        {
                            Alumno.Nombre1 = string.Join(" ", nombres.ToArray()).Length >= 100 ? String.Join(" ", nombres.ToArray()).Substring(0, 100) : String.Join(" ", nombres.ToArray());
                            Alumno.Nombre2 = string.Empty;
                        }
                        //Apellidos
                        postulante.ApellidoPaterno = postulante.ApellidoPaterno ?? string.Empty;
                        postulante.ApellidoMaterno = postulante.ApellidoMaterno ?? string.Empty;
                        var apellidos = (postulante.ApellidoPaterno + " " + postulante.ApellidoMaterno).Split(new char[] { ' ' }).ToList().Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();
                        if (apellidos.Count == 1)
                        {
                            Alumno.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                            Alumno.ApellidoMaterno = string.Empty;
                        }
                        else if (apellidos.Count == 2)
                        {
                            Alumno.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                            Alumno.ApellidoMaterno = apellidos[1].Length >= 100 ? apellidos[1].Substring(0, 100) : apellidos[1];
                        }
                        else if (apellidos.Count > 2)
                        {
                            Alumno.ApellidoPaterno = String.Join(" ", apellidos.ToArray()).Length >= 100 ? String.Join(" ", apellidos.ToArray()).Substring(0, 100) : String.Join(" ", apellidos.ToArray());
                            Alumno.ApellidoMaterno = string.Empty;
                        }
                        else
                        {
                            Alumno.ApellidoPaterno = string.Empty;
                            Alumno.ApellidoMaterno = string.Empty;
                        }
                        Alumno.IdAformacion = 3/*Sin area de formacion*/;
                        Alumno.IdAtrabajo = 3/*Sin area de trabajo*/;
                        Alumno.IdCargo = 11/*Sin cargo*/;
                        Alumno.IdIndustria = 48/*Sin industria*/;
                        Alumno.Celular = "963852741";
                        Alumno.IdCodigoRegionCiudad = null;
                        Alumno.IdCodigoPais = 51/*Peru*/;
                        Alumno.Telefono = string.Empty;
                        Alumno.Email1 = postulante.Email;
                        Alumno.Email2 = postulante.Email;
                        Alumno.Estado = true;
                        Alumno.UsuarioCreacion = "AccesosPostulante";
                        Alumno.UsuarioModificacion = "SYSTEM";
                        Alumno.FechaModificacion = DateTime.Now;
                        Alumno.FechaCreacion = DateTime.Now;
                        Alumno.IdEstadoContactoWhatsApp = 3;
                        bool resultadoAlumno = _repAlumno.Insert(Alumno);
                        if (!resultadoAlumno)
                        {
                            respuestaCreacionAcceso.ValidacionRespuesta = false;
                            return respuestaCreacionAcceso;
                        }
                    }
                    ClasificacionPersona = _repClasificacionPersona.FirstBy(x => x.IdTipoPersona == 1 && x.IdPersona == persona.Id);/*Tipo Alumno*/
                    if (ClasificacionPersona != null)
                    {
                        if (ClasificacionPersona.IdTablaOriginal != Alumno.Id)
                        {
                            ClasificacionPersona.IdTablaOriginal = Alumno.Id;
                            _repClasificacionPersona.Update(ClasificacionPersona);
                        }
                    }
                    else
                    {
                        ClasificacionPersona = new ClasificacionPersonaBO();
                        ClasificacionPersona.IdPersona = persona.Id;
                        ClasificacionPersona.IdTipoPersona = 1;/*Tipo Alumno*/
                        ClasificacionPersona.IdTablaOriginal = Alumno.Id;
                        ClasificacionPersona.Estado = true;
                        ClasificacionPersona.UsuarioCreacion = "AccesosPostulante";
                        ClasificacionPersona.UsuarioModificacion = "AccesosPostulante";
                        ClasificacionPersona.FechaCreacion = DateTime.Now;
                        ClasificacionPersona.FechaModificacion = DateTime.Now;
                        _repClasificacionPersona.Insert(ClasificacionPersona);

                        ClasificacionPersona.IdMigracion = ClasificacionPersona.Id;
                    }
                    /*Logica para crear el contacto*/
                    if (string.IsNullOrEmpty(IdPortalWeb))
                    {
                        Random letraRandom = new Random();
                        int numero = letraRandom.Next(26);
                        char letra = (char)(((int)'A') + numero);
                        string claveIntegra = Alumno.Nombre1.ToLower().Substring(0, 2) + Alumno.Email1.ToUpper().Substring(0, 1) + "AcTmp0" + letra;
                        string claveHash = string.Empty;
                        claveHash = Crypto.HashPassword(claveIntegra);

                        var resultadoAspNetUsers = _repMontoPagoCronograma.CrearUsuarioClavePortalWeb(Alumno.Id, postulante.Email, claveIntegra, claveHash, postulante.Nombre, postulante.ApellidoPaterno + " " + postulante.ApellidoMaterno, Alumno.Telefono, Alumno.Celular, Alumno.IdCodigoRegionCiudad, Alumno.IdCodigoPais, DateTime.Now);
                        respuestaCreacionAcceso.IdAlumno = resultadoAspNetUsers.IdAlumno;
                        respuestaCreacionAcceso.Usuario = resultadoAspNetUsers.UserName;
                        respuestaCreacionAcceso.Clave = resultadoAspNetUsers.Password;
                        IdPortalWeb = _repPersonalAccesoTemporalAulaVirtual.ObtenerIdUsuarioPortalWebCorreo(postulante.Email);
                    }
                }
                //Obtengo la información de accesos al portal de postulante
                var datosAcceso = _repPostulanteAccesoTemporalAulaVirtual.ObtenerAccesosPortalWebCorreo(postulante.Email);
                if (datosAcceso.IdAlumno > 0)
                {
                    respuestaCreacionAcceso.IdAlumno = datosAcceso.IdAlumno;
                    respuestaCreacionAcceso.Usuario = datosAcceso.Email;
                    respuestaCreacionAcceso.Clave = datosAcceso.Clave;
                    respuestaCreacionAcceso.ValidacionRespuesta = true;
                }
                else
                {
                    respuestaCreacionAcceso.ValidacionRespuesta = false;
                    return respuestaCreacionAcceso;
                }
                bool resultado = false;
                PostulanteCursoPortalNotasHistoricoBO nuevoRegistroNotas;
                //Lógica para Accesos Temporales anteriores
                var listaPostulanteAnterior = _repPostulanteAccesoTemporalAulaVirtual.GetBy(x => x.IdPostulante == informacionPostulanteExamen.IdPostulante && x.IdPespecificoHijo == idPEspecifico.Id && x.IdPespecificoPadre == idPEspecificoPadre).ToList();
                foreach (var accesoAnterior in listaPostulanteAnterior)
                {
                    //Función para obtener anteriores notas de postulante desde el portal
                    var notasPortalCursoAnterior = _repPostulanteCursoPortalNotasHistorico.ObtenerNotasAnteriores(datosAcceso.IdAlumno.GetValueOrDefault(), accesoAnterior.IdPespecificoPadre);

                    var pGeneral = 0;
                    var idUsuario = "";
                    foreach (var notaIndividual in notasPortalCursoAnterior)
                    {
                        pGeneral = notaIndividual.IdPgeneral;
                        idUsuario = notaIndividual.IdUsuario;                        
                        nuevoRegistroNotas = new PostulanteCursoPortalNotasHistoricoBO()
                        {
                            IdPostulanteProcesoSeleccion = accesoAnterior.IdPostulanteProcesoSeleccion.GetValueOrDefault(),
                            IdPgeneral = notaIndividual.IdPgeneral,
                            OrdenFilaCapitulo = notaIndividual.OrdenFilaCapitulo,
                            OrdenFilaSesion = notaIndividual.OrdenFilaSesion,
                            GrupoPregunta = notaIndividual.GrupoPregunta,
                            Calificacion = notaIndividual.Calificacion,
                            IdUsuario = notaIndividual.IdUsuario,
                            IdAlumno = notaIndividual.IdAlumno,
                            IdPespecifico = notaIndividual.IdPespecifico,
                            AccesoPrueba = notaIndividual.AccesoPrueba,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = informacionPostulanteExamen.Usuario,
                            UsuarioModificacion = informacionPostulanteExamen.Usuario
                        };
                        _repPostulanteCursoPortalNotasHistorico.Insert(nuevoRegistroNotas);
                    }
                    var visualizacionVideoAnterior = _repPostulanteCursoPortalNotasHistorico.ObtenerVisualizacionVideoAnteriores(idUsuario, pGeneral);
                    if (notasPortalCursoAnterior.Any() && visualizacionVideoAnterior.Any() && pGeneral > 0 && idUsuario.Length > 0)
                    {
                        var respuestaEliminacionFisicaNota = _repPostulanteCursoPortalNotasHistorico.EliminarFisicamenteAnterioresNotas(idUsuario, pGeneral, notasPortalCursoAnterior.Select(x => x.Id).ToList(), visualizacionVideoAnterior.Select(x => x.Id).ToList());
                        if (!respuestaEliminacionFisicaNota)
                        {
                            respuestaCreacionAcceso.ValidacionRespuesta = false;
                            return respuestaCreacionAcceso;
                        }
                    }
                    _repPostulanteAccesoTemporalAulaVirtual.Delete(accesoAnterior.Id, informacionPostulanteExamen.Usuario);
                }
                //Agregar nuevos accesos
                PostulanteAccesoTemporalAulaVirtualBO agregar = new PostulanteAccesoTemporalAulaVirtualBO()
                {
                    IdPostulante = informacionPostulanteExamen.IdPostulante,
                    IdPespecificoPadre = idPEspecificoPadre,
                    IdPespecificoHijo = idPEspecifico.Id,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    Estado = true,
                    UsuarioCreacion = informacionPostulanteExamen.Usuario,
                    UsuarioModificacion = informacionPostulanteExamen.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    IdAlumno = Alumno.Id,
                    IdPostulanteProcesoSeleccion = postulanteProcesoSeleccion.Id,
                    IdExamen = informacionPostulanteExamen.IdExamen,
                };
                resultado = _repPostulanteAccesoTemporalAulaVirtual.Insert(agregar);
                if (!resultado)
                {
                    respuestaCreacionAcceso.ValidacionRespuesta = resultado;
                    return respuestaCreacionAcceso;
                }
                bool resultadoPortalWeb = _repPostulanteAccesoTemporalAulaVirtual.ActualizarAccesosTemporalesPortalWeb(postulante.Id, IdPortalWeb, Alumno.Id, idPEspecifico.Id);
                if (!resultadoPortalWeb)
                {
                    respuestaCreacionAcceso.ValidacionRespuesta = false;
                    return respuestaCreacionAcceso;
                }
                
                return respuestaCreacionAcceso;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Edgar Serruto
        /// Fecha: 22/06/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Reporte de Accesos Temporales
        /// </summary>
        /// <param name="filtro">Filtro de Búsqueda</param>
        /// <returns> List<AccesosRegistradosPostulanteDTO> </returns>
        public List<RespuestaAccesoDTO> GenerarReporteAccesosTemporales(FiltroReporteAccesosTemporalesDTO filtro)
        {
            try
            {
                List<AccesosRegistradosPostulanteDTO> resultado = new List<AccesosRegistradosPostulanteDTO>();
                List<RespuestaAccesoDTO> resultadoAgrupado = new List<RespuestaAccesoDTO>();
                List<RespuestaProcesoSeleccionPostulanteDTO> listaPostulante = new List<RespuestaProcesoSeleccionPostulanteDTO>();
                List<int> listaPostulanteEtapa = new List<int>();
                string condicionPostulante = string.Empty;
                var filtroPostulante = new
                {
                    ListaPostulante = filtro.ListaPostulante == null ? "" : string.Join(",", filtro.ListaPostulante.Select(x => x)),
                    IdProcesoSeleccion = filtro.IdProcesoSeleccion == null ? "" : string.Join(",", filtro.IdProcesoSeleccion),
                    NroDocumento = filtro.NroDocumento == null ? "" : filtro.NroDocumento.Trim(),
                    EstadoAcceso = filtro.EstadoAcceso == null ? "" : string.Join(",", filtro.EstadoAcceso)
                };
                if (filtroPostulante.ListaPostulante.Length > 0)
                {
                    if (condicionPostulante.Length > 0)
                    {
                        condicionPostulante =  condicionPostulante + " AND IdPostulante IN ("+ filtroPostulante.ListaPostulante + ") ";
                    }
                    else
                    {
                        condicionPostulante = condicionPostulante + " IdPostulante IN (" + filtroPostulante.ListaPostulante + ") ";
                    }
                }
                if (filtroPostulante.IdProcesoSeleccion.Length > 0)
                {
                    if (condicionPostulante.Length > 0)
                    {
                        condicionPostulante = condicionPostulante + " AND IdProcesoSeleccion = " + filtroPostulante.IdProcesoSeleccion + " ";
                    }
                    else
                    {
                        condicionPostulante = condicionPostulante + " IdProcesoSeleccion = " + filtroPostulante.IdProcesoSeleccion + " ";
                    }
                }
                if (filtroPostulante.NroDocumento.Length > 0)
                {
                    if (condicionPostulante.Length > 0)
                    {
                        condicionPostulante = condicionPostulante + " AND NroDocumento = '" + filtroPostulante.NroDocumento + "' ";
                    }
                    else
                    {
                        condicionPostulante = condicionPostulante + " NroDocumento = '" + filtroPostulante.NroDocumento + "' ";
                    }
                }
                if (filtroPostulante.EstadoAcceso.Length > 0)
                {
                    if (condicionPostulante.Length > 0)
                    {
                        condicionPostulante = condicionPostulante + " AND TieneAcceso = " + filtroPostulante.EstadoAcceso + " ";
                    }
                    else
                    {
                        condicionPostulante = condicionPostulante + " TieneAcceso = " + filtroPostulante.EstadoAcceso + " ";
                    }
                }
                //Validamos que existan condiciones
                if (condicionPostulante.Length > 0)
                {
                    //Obtenemos información de Postulante y Proceso de Selección
                    listaPostulante = _repPostulanteAccesoTemporalAulaVirtual.ObtenerPostulantesPorCondiciones(condicionPostulante).ToList();
                    if(listaPostulante.Count > 0)
                    {
                        string condicionEtapas = string.Empty;
                        var filtroEtapaEstado = new
                        {
                            PostulanteFiltrado = string.Join(",", listaPostulante.Select(x => x.IdPostulante)),  
                            ProcesoSeleccionFiltrado = string.Join(",", listaPostulante.Select(x => x.IdProcesoSeleccion)),
                            EtapaProceso = filtro.ListaEtapaProceso == null ? "" : string.Join(",", filtro.ListaEtapaProceso.Select(x => x).Distinct()),
                            EstadoEtapa = filtro.ListaEstadoEtapa == null ? "" : string.Join(",", filtro.ListaEstadoEtapa.Select(x => x).Distinct()),
                        };
                        if (filtroEtapaEstado.PostulanteFiltrado.Length > 0)
                        {
                            if (condicionEtapas.Length > 0)
                            {
                                condicionEtapas = condicionEtapas + " AND IdPostulante IN (" + filtroEtapaEstado.PostulanteFiltrado + ") ";
                            }
                            else
                            {
                                condicionEtapas = condicionEtapas + " IdPostulante IN (" + filtroEtapaEstado.PostulanteFiltrado + ") ";
                            }
                        }
                        if (filtroEtapaEstado.ProcesoSeleccionFiltrado.Length > 0)
                        {
                            if (condicionEtapas.Length > 0)
                            {
                                condicionEtapas = condicionEtapas + " AND IdProcesoSeleccion IN (" + filtroEtapaEstado.ProcesoSeleccionFiltrado + ") ";
                            }
                            else
                            {
                                condicionEtapas = condicionEtapas + " IdProcesoSeleccion IN (" + filtroEtapaEstado.ProcesoSeleccionFiltrado + ") ";
                            }
                        }
                        if (filtroEtapaEstado.EtapaProceso.Length > 0)
                        {
                            if (condicionEtapas.Length > 0)
                            {
                                condicionEtapas = condicionEtapas + " AND IdProcesoSeleccionEtapa IN (" + filtroEtapaEstado.EtapaProceso + ") ";
                            }
                            else
                            {
                                condicionEtapas = condicionEtapas + " IdProcesoSeleccionEtapa IN (" + filtroEtapaEstado.EtapaProceso + ") ";
                            }
                        }
                        if (filtroEtapaEstado.EstadoEtapa.Length > 0)
                        {
                            if (condicionEtapas.Length > 0)
                            {
                                condicionEtapas = condicionEtapas + " AND IdEstadoEtapaProcesoSeleccion IN (" + filtroEtapaEstado.EstadoEtapa + ") ";
                            }
                            else
                            {
                                condicionEtapas = condicionEtapas + " IdEstadoEtapaProcesoSeleccion IN (" + filtroEtapaEstado.EstadoEtapa + ") ";
                            }
                        }
                        if (condicionEtapas.Length > 0)
                        {
                            listaPostulanteEtapa = _repPostulanteAccesoTemporalAulaVirtual.ObtenerPostulantesEtapaPorCondiciones(condicionEtapas).Select(x => x.IdPostulante).ToList();
                            listaPostulante = listaPostulante.Where(x => listaPostulanteEtapa.Contains(x.IdPostulante)).ToList();
                            string condicionResultado = string.Empty;
                            var filtroResultado = new
                            {
                                PostulanteFiltradoFinal = string.Join(",", listaPostulante.Select(x => x.IdPostulante).Distinct()),
                                ProcesoSeleccionFiltradoFinal = string.Join(",", listaPostulante.Select(x => x.IdProcesoSeleccion).Distinct()),
                            };
                            if (filtroResultado.PostulanteFiltradoFinal.Length > 0)
                            {
                                condicionResultado = condicionResultado + " AND IdPostulante IN (" + filtroResultado.PostulanteFiltradoFinal + ") ";
                            }                            
                            if (condicionResultado.Length > 0)
                            {
                                resultado = _repPostulanteAccesoTemporalAulaVirtual.ObtenerPostulantesAccesosPorCondiciones(condicionResultado, filtro.FechaInicio, filtro.FechaFin).Distinct().ToList();
                                if (resultado.Count > 0)
                                {
                                    resultadoAgrupado = resultado.GroupBy(x => new{ x.IdPostulante, x.Postulante, x.NroDocumento }).Select(x => new RespuestaAccesoDTO
                                    {
                                        IdPostulante = x.Key.IdPostulante,
                                        Postulante = x.Key.Postulante,
                                        NroDocumento = x.Key.NroDocumento,
                                        Agrupado = x.GroupBy(y => new { y.IdExamen, y.Examen, y.EstadoAcceso, y.FechaInicio, y.FechaFin }).Select(y => new RespuestaAccesoAgrupadoDTO {
                                            IdExamen = y.Key.IdExamen,
                                            Examen = y.Key.Examen,
                                            EstadoAcceso = y.Key.EstadoAcceso.GetValueOrDefault(),
                                            FechaInicio = y.Key.FechaInicio,
                                            FechaFin = y.Key.FechaFin
                                        }).ToList(),
                                    }).ToList();
                                    return resultadoAgrupado;
                                }
                                else
                                {
                                    return resultadoAgrupado;
                                }                                
                            }
                            else
                            {
                                //Si no hay postulantes retornamos vacio
                                return resultadoAgrupado;
                            }
                        }
                        else
                        {
                            //Si no hay postulantes retornamos vacio
                            return resultadoAgrupado;
                        }
                    }
                    else
                    {
                        //Si no hay postulantes retornamos vacio
                        return resultadoAgrupado;
                    }
                }
                else
                {
                    condicionPostulante = " IdPostulante > 0";
                    listaPostulante = _repPostulanteAccesoTemporalAulaVirtual.ObtenerPostulantesPorCondiciones(condicionPostulante).ToList();
                    if (listaPostulante.Count > 0)
                    {
                        string condicionEtapas = string.Empty;
                        var filtroEtapaEstado = new
                        {
                            PostulanteFiltrado = string.Join(",", listaPostulante.Select(x => x.IdPostulante)),
                            ProcesoSeleccionFiltrado = string.Join(",", listaPostulante.Select(x => x.IdProcesoSeleccion)),
                            EtapaProceso = filtro.ListaEtapaProceso == null ? "" : string.Join(",", filtro.ListaEtapaProceso.Select(x => x).Distinct()),
                            EstadoEtapa = filtro.ListaEstadoEtapa == null ? "" : string.Join(",", filtro.ListaEstadoEtapa.Select(x => x).Distinct()),
                        };
                        if (filtroEtapaEstado.PostulanteFiltrado.Length > 0)
                        {
                            if (condicionEtapas.Length > 0)
                            {
                                condicionEtapas = condicionEtapas + " AND IdPostulante IN (" + filtroEtapaEstado.PostulanteFiltrado + ") ";
                            }
                            else
                            {
                                condicionEtapas = condicionEtapas + " IdPostulante IN (" + filtroEtapaEstado.PostulanteFiltrado + ") ";
                            }
                        }
                        if (filtroEtapaEstado.ProcesoSeleccionFiltrado.Length > 0)
                        {
                            if (condicionEtapas.Length > 0)
                            {
                                condicionEtapas = condicionEtapas + " AND IdProcesoSeleccion IN (" + filtroEtapaEstado.ProcesoSeleccionFiltrado + ") ";
                            }
                            else
                            {
                                condicionEtapas = condicionEtapas + " IdProcesoSeleccion IN (" + filtroEtapaEstado.ProcesoSeleccionFiltrado + ") ";
                            }
                        }
                        if (filtroEtapaEstado.EtapaProceso.Length > 0)
                        {
                            if (condicionEtapas.Length > 0)
                            {
                                condicionEtapas = condicionEtapas + " AND IdProcesoSeleccionEtapa IN (" + filtroEtapaEstado.EtapaProceso + ") ";
                            }
                            else
                            {
                                condicionEtapas = condicionEtapas + " IdProcesoSeleccionEtapa IN (" + filtroEtapaEstado.EtapaProceso + ") ";
                            }
                        }
                        if (filtroEtapaEstado.EstadoEtapa.Length > 0)
                        {
                            if (condicionEtapas.Length > 0)
                            {
                                condicionEtapas = condicionEtapas + " AND IdEstadoEtapaProcesoSeleccion IN (" + filtroEtapaEstado.EstadoEtapa + ") ";
                            }
                            else
                            {
                                condicionEtapas = condicionEtapas + " IdEstadoEtapaProcesoSeleccion IN (" + filtroEtapaEstado.EstadoEtapa + ") ";
                            }
                        }
                        if (condicionEtapas.Length > 0)
                        {
                            listaPostulanteEtapa = _repPostulanteAccesoTemporalAulaVirtual.ObtenerPostulantesEtapaPorCondiciones(condicionEtapas).Select(x => x.IdPostulante).ToList();
                            listaPostulante = listaPostulante.Where(x => listaPostulanteEtapa.Contains(x.IdPostulante)).ToList();
                            string condicionResultado = string.Empty;
                            var filtroResultado = new
                            {
                                PostulanteFiltradoFinal = string.Join(",", listaPostulante.Select(x => x.IdPostulante).Distinct()),
                                ProcesoSeleccionFiltradoFinal = string.Join(",", listaPostulante.Select(x => x.IdProcesoSeleccion).Distinct()),
                            };
                            if (filtroResultado.PostulanteFiltradoFinal.Length > 0)
                            {
                                condicionResultado = condicionResultado + " AND IdPostulante IN (" + filtroResultado.PostulanteFiltradoFinal + ") ";
                            }
                            if (condicionResultado.Length > 0)
                            {
                                resultado = _repPostulanteAccesoTemporalAulaVirtual.ObtenerPostulantesAccesosPorCondiciones(condicionResultado, filtro.FechaInicio, filtro.FechaFin).Distinct().ToList();
                                if (resultado.Count > 0)
                                {
                                    resultadoAgrupado = resultado.GroupBy(x => new { x.IdPostulante, x.Postulante, x.NroDocumento }).Select(x => new RespuestaAccesoDTO
                                    {
                                        IdPostulante = x.Key.IdPostulante,
                                        Postulante = x.Key.Postulante,
                                        NroDocumento = x.Key.NroDocumento,
                                        Agrupado = x.GroupBy(y => new { y.IdExamen, y.Examen, y.EstadoAcceso, y.FechaInicio, y.FechaFin }).Select(y => new RespuestaAccesoAgrupadoDTO
                                        {
                                            IdExamen = y.Key.IdExamen,
                                            Examen = y.Key.Examen,
                                            EstadoAcceso = y.Key.EstadoAcceso.GetValueOrDefault(),
                                            FechaInicio = y.Key.FechaInicio,
                                            FechaFin = y.Key.FechaFin
                                        }).ToList(),
                                    }).ToList();
                                    return resultadoAgrupado;
                                }
                                else
                                {
                                    return resultadoAgrupado;
                                }
                            }
                            else
                            {
                                //Si no hay postulantes retornamos vacio
                                return resultadoAgrupado;
                            }
                        }
                        else
                        {
                            //Si no hay postulantes retornamos vacio
                            return resultadoAgrupado;
                        }
                    }
                    else
                    {
                        //Si no hay postulantes retornamos vacio
                        return resultadoAgrupado;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
