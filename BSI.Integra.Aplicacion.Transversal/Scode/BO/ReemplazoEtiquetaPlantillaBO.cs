using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Mandrill.Models;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Base.BO;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Scode.Helper;
using System.Globalization;
using BSI.Integra.Aplicacion.Transversal.Helper;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;
using static BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas.PostulanteAccesoTemporalAulaVirtualDTO;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Transversal/ReemplazoEtiquetaPlantilla
    /// Autor: Gian Miranda
    /// Fecha: 26/01/2021
    /// <summary>
    /// BO para el intercambio de etiquetas a informacion
    /// </summary>
    public class ReemplazoEtiquetaPlantillaBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdPlantilla		                    PK de la tabla mkt.T_Plantilla
        /// IdPlantillaBase                     PK de la tabla mkt.T_Plantilla, indica la plantilla base a la que pertenece
        /// IdOportunidad		                PK de la tabla com.T_Oportunidad, indica la oportunidad arraigada al mensaje
        /// Grupo		                        Grupo de materiales del PEspecifico
        /// IdMaterialPEspecificoDetalle		PK de la tabla ope.T_MaterialPEspecificoDetalle, indica el material por PEspecifico
        /// ListaIdMaterialPEspecificoDetalle	Lista de Materiales enlazados al PEspecifico
        /// IdPostulanteProcesoSeleccion        PK de la tabla gp.T_PostulanteProcesoSeleccion, indica la relacion entre un postulante con el proceso
        /// FechaGP                             Fecha de registro en GP
        /// Personal                            BO de Personal
        /// IncrementoZonaHoraria               Incremento de la zona horaria dependiendo del pais objetivo para la informacion
        /// NombrePais                          Nombre del Pais
        /// IdPEspecificoSesion                 PK de pla.T_PEspecificoSesion, indica las sesiones de un PEspecifico
        /// IdMatriculaCabecera                 PK de la tabla fin.T_MatriculaCabecera
        /// IdProveedor                         PK de la tabla fin.T_Proveedor
        /// IdPEspecificoWebinar                Id del webinar que se desea tomar en cuenta para la plantilla

        public int IdPlantilla { get; set; }
        public int IdPlantillaBase { get; set; }
        public int IdOportunidad { get; set; }
        public int Grupo { get; set; }
        public int IdMaterialPEspecificoDetalle { get; set; }
        public List<int> ListaIdMaterialPEspecificoDetalle { get; set; }
        public int IdPostulanteProcesoSeleccion { get; set; }
        public DateTime? FechaGP { get; set; }
        public PersonalBO Personal { get; set; }
        public int IncrementoZonaHoraria { get; set; }
        public string NombrePais { get; set; }
        public int? IdPEspecificoSesion { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int IdProveedor { get; set; }
        public int IdPEspecificoWebinar { get; set; }
        public int IdPEspecifico { get; set; }
        private readonly DateTime FechaActual = DateTime.Now;

        public PlantillaEmailMandrillDTO EmailReemplazado;
        public PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado;
        public PlantillaSmsCalculadoDTO SmsReemplazado;

        private readonly integraDBContext _integraDBContext;
        private readonly AlumnoRepositorio _repAlumno;
        private readonly PersonalRepositorio _repPersonal;
        private readonly PlantillaRepositorio _repPlantilla;
        private readonly CentroCostoRepositorio _repCentroCosto;
        private readonly PespecificoRepositorio _repPespecifico;
        private readonly PgeneralRepositorio _repPgeneral;
        private readonly OportunidadRepositorio _repOportunidad;
        private readonly OportunidadClasificacionOperacionesRepositorio _repOportunidadClasificacionOperaciones;
        private readonly PespecificoRepositorio _repPEspecifico;
        private readonly PespecificoSesionRepositorio _repPEspecificoSesion;
        private readonly PlantillaBaseRepositorio _repPlantillaBase;
        private readonly MatriculaCabeceraRepositorio _repMatriculaCabecera;
        private readonly DocumentoSeccionPwRepositorio _repDocumentoSeccionPw;
        private readonly MaterialPespecificoDetalleRepositorio _repMaterialPEspecificoDetalle;
        private readonly PartnerPwRepositorio _repPartnerPw;
        private readonly SolicitudOperacionesRepositorio _repSolicitudOperaciones;
        private readonly SolicitudCertificadoFisicoRepositorio _repSolicitudCertificadoFisico;
        private readonly ModalidadCursoRepositorio _repModalidadCurso;

        private readonly PostulanteRepositorio _repPostulante;
        private readonly ClasificacionPersonaRepositorio _repClasificacionPersona;
        private readonly ProveedorRepositorio _repProveedorRepositorio;

        public ReemplazoEtiquetaPlantillaBO()
        {
            EmailReemplazado = new PlantillaEmailMandrillDTO();
            WhatsAppReemplazado = new PlantillaWhatsAppCalculadoDTO();
            SmsReemplazado = new PlantillaSmsCalculadoDTO();
        }


        public ReemplazoEtiquetaPlantillaBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _integraDBContext.ChangeTracker.AutoDetectChangesEnabled = false;

            _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
            _repPespecifico = new PespecificoRepositorio(_integraDBContext);
            _repPgeneral = new PgeneralRepositorio(_integraDBContext);
            _repOportunidad = new OportunidadRepositorio(_integraDBContext);

            _repAlumno = new AlumnoRepositorio(_integraDBContext);
            _repPlantilla = new PlantillaRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
            _repPEspecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
            _repPlantillaBase = new PlantillaBaseRepositorio(_integraDBContext);
            _repOportunidadClasificacionOperaciones = new OportunidadClasificacionOperacionesRepositorio(_integraDBContext);
            _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            EmailReemplazado = new PlantillaEmailMandrillDTO();
            WhatsAppReemplazado = new PlantillaWhatsAppCalculadoDTO();
            SmsReemplazado = new PlantillaSmsCalculadoDTO();

            _repMaterialPEspecificoDetalle = new MaterialPespecificoDetalleRepositorio(_integraDBContext);
            _repPartnerPw = new PartnerPwRepositorio(_integraDBContext);
            _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio(_integraDBContext);
            ListaIdMaterialPEspecificoDetalle = new List<int>();

            _repPostulante = new PostulanteRepositorio(_integraDBContext);
            _repClasificacionPersona = new ClasificacionPersonaRepositorio(_integraDBContext);
            _repProveedorRepositorio = new ProveedorRepositorio(_integraDBContext);
            _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);
            _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);
            _repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
        }

        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla
        /// </summary>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public void ReemplazarEtiquetas()
        {
            try
            {
                if (!_repOportunidad.Exist(x => x.Id == this.IdOportunidad))
                {
                    throw new Exception("Oportunidad no existente!");
                }

                if (!_repOportunidadClasificacionOperaciones.Exist(x => x.IdOportunidad == this.IdOportunidad))
                {
                    throw new Exception("Oportunidad no valida!");
                }
                var oportunidadClasificacionOperaciones = _repOportunidadClasificacionOperaciones.FirstBy(x => x.IdOportunidad == this.IdOportunidad);

                if (!_repMatriculaCabecera.Exist(x => x.Id == oportunidadClasificacionOperaciones.IdMatriculaCabecera))
                {
                    throw new Exception("Matricula cabecera no valida!");
                }

                var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

                var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.Id == oportunidadClasificacionOperaciones.IdMatriculaCabecera);
                var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();

                if (!_repAlumno.Exist(matriculaCabecera.IdAlumno))
                {
                    throw new Exception("Codigo de alumno no valido!");
                }

                if (!_repPlantilla.Exist(IdPlantilla))
                {
                    throw new Exception("Codigo de alumno no valido!");
                }

                var plantilla = _repPlantilla.FirstById(this.IdPlantilla);

                this.IdPlantillaBase = plantilla.IdPlantillaBase;

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Codigo de alumno no valido!");
                }

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Codigo de alumno no valido!");
                }

                var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);
                var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);

                var oportunidad = _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
                var DatosCompuestosOportunidad = _repOportunidad.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
                var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

                var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                foreach (var etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                }

                if (plantillaBase.Cuerpo.Contains("{tPersonal.Nombre1}"))
                {
                    var valor = personal.PrimerNombre;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.Nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.Nombre1}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tPersonal.email}"))
                {
                    var valor = personal.Email;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.email}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.email}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{tPersonal.PrimerNombreApellidoPaterno}"))
                {
                    var valor = personal.PrimerNombreApellidoPaterno;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.PrimerNombreApellidoPaterno}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.PrimerNombreApellidoPaterno}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.UrlConfirmacionParticipacionSesionWebinarDentro7Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerUrlConfirmacionParticipacionSesionWebinar(matriculaCabecera.Id, 7);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.UrlConfirmacionParticipacionSesionWebinarDentro7Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.UrlConfirmacionParticipacionSesionWebinarDentro7Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.PresentacionTrabajoHoy}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerPresentacionTrabajoNDias(matriculaCabecera.Id, 0);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.PresentacionTrabajoHoy}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.PresentacionTrabajoHoy}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.PresentacionTrabajoFinalHoy}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerPresentacionTrabajoFinalNDias(matriculaCabecera.Id, 0);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.PresentacionTrabajoFinalHoy}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.PresentacionTrabajoFinalHoy}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.SesionWebinarDentro7Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerSesionesWebinarNDias(matriculaCabecera.Id, 7, false);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.SesionWebinarDentro7Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.SesionWebinarDentro7Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                var listaEtiquetasWebinarSesion7Dias = new List<string>()
                {
                    "{T_PEspecifico.FechaSesionWebinarDentro7Dias}",
                    "{T_PEspecifico.HoraInicioSesionWebinarDentro7Dias}",
                    "{T_PEspecifico.HoraFinSesionWebinarDentro7Dias}"
                };
                // Etiquetas texto plano
                if (listaEtiquetasWebinarSesion7Dias.Any(plantillaBase.Cuerpo.Contains))
                {
                    var valor = _repMatriculaCabecera.ObtenerSesionesWebinarNDias(matriculaCabecera.Id, 7).FirstOrDefault();

                    if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.FechaSesionWebinarDentro7Dias}"))
                    {
                        var valorFormateado = valor.FechaInicio.ToString("dd/MM/yyyy");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.FechaSesionWebinarDentro7Dias}", valorFormateado);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.FechaSesionWebinarDentro7Dias}")).FirstOrDefault().texto = valorFormateado;
                        }
                    }


                    if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.HoraInicioSesionWebinarDentro7Dias}"))
                    {
                        var valorFormateado = valor.FechaInicio.ToString("hh:mm tt");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.HoraInicioSesionWebinarDentro7Dias}", valorFormateado);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.HoraInicioSesionWebinarDentro7Dias}")).FirstOrDefault().texto = valorFormateado;
                        }
                    }


                    if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.HoraFinSesionWebinarDentro7Dias}"))
                    {
                        var valorFormateado = valor.FechaTermino.ToString("hh:mm tt");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.HoraFinSesionWebinarDentro7Dias}", valorFormateado);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.HoraFinSesionWebinarDentro7Dias}")).FirstOrDefault().texto = valorFormateado;
                        }
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.AccesoSesionWebinarDentro1Dia}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerSesionesWebinarConfirmadasNDias(matriculaCabecera.Id, 1, true);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.AccesoSesionWebinarDentro1Dia}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.AccesoSesionWebinarDentro1Dia}")).FirstOrDefault().texto = valor;
                    }
                }

                var listaEtiquetasWebinarSesion1Dia = new List<string>()
                {
                    "{T_PEspecifico.FechaAccesoSesionWebinarDentro1Dia}",
                    "{T_PEspecifico.HoraInicioAccesoSesionWebinarDentro1Dia}",
                    "{T_PEspecifico.HoraFinAccesoSesionWebinarDentro1Dia}",
                    "{T_PEspecifico.LinkAccesoSesionWebinarDentro1Dia}",
                };

                // Etiquetas texto plano
                if (listaEtiquetasWebinarSesion1Dia.Any(plantillaBase.Cuerpo.Contains))
                {
                    var valor = _repMatriculaCabecera.ObtenerSesionesConfirmadasWebinarNDias(matriculaCabecera.Id, 1).FirstOrDefault();

                    if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.FechaAccesoSesionWebinarDentro1Dia}"))
                    {
                        var valorFormateado = valor.FechaInicio.ToString("dd/MM/yyyy");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.FechaAccesoSesionWebinarDentro1Dia}", valorFormateado);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.FechaAccesoSesionWebinarDentro1Dia}")).FirstOrDefault().texto = valorFormateado;
                        }
                    }


                    if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.HoraInicioAccesoSesionWebinarDentro1Dia}"))
                    {
                        var valorFormateado = valor.FechaInicio.ToString("hh:mm tt");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.HoraInicioAccesoSesionWebinarDentro1Dia}", valorFormateado);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.HoraInicioAccesoSesionWebinarDentro1Dia}")).FirstOrDefault().texto = valorFormateado;
                        }
                    }


                    if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.HoraFinAccesoSesionWebinarDentro1Dia}"))
                    {
                        var valorFormateado = valor.FechaTermino.ToString("hh:mm tt");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.HoraFinAccesoSesionWebinarDentro1Dia}", valorFormateado);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.HoraFinAccesoSesionWebinarDentro1Dia}")).FirstOrDefault().texto = valorFormateado;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.LinkAccesoSesionWebinarDentro1Dia}"))
                    {
                        var valorFormateado = valor.LinkWebinar;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.LinkAccesoSesionWebinarDentro1Dia}", valorFormateado);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.LinkAccesoSesionWebinarDentro1Dia}")).FirstOrDefault().texto = valorFormateado;
                        }
                    }
                }

                if (listaEtiquetasWebinarSesion1Dia.Any(plantillaBase.Asunto.Contains))
                {
                    var valor = _repMatriculaCabecera.ObtenerSesionesConfirmadasWebinarNDias(matriculaCabecera.Id, 1).FirstOrDefault();

                    if (plantillaBase.Asunto.Contains("{T_PEspecifico.FechaAccesoSesionWebinarDentro1Dia}"))
                    {
                        var valorFormateado = valor.FechaInicio.ToString("dd/MM/yyyy");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_PEspecifico.FechaAccesoSesionWebinarDentro1Dia}", valorFormateado);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            //listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.FechaAccesoSesionWebinarDentro1Dia}")).FirstOrDefault().texto = valorFormateado;
                        }
                    }


                    if (plantillaBase.Asunto.Contains("{T_PEspecifico.HoraInicioAccesoSesionWebinarDentro1Dia}"))
                    {
                        var valorFormateado = valor.FechaInicio.ToString("hh:mm tt");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_PEspecifico.HoraInicioAccesoSesionWebinarDentro1Dia}", valorFormateado);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            //listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.HoraInicioAccesoSesionWebinarDentro1Dia}")).FirstOrDefault().texto = valorFormateado;
                        }
                    }


                    if (plantillaBase.Asunto.Contains("{T_PEspecifico.HoraFinAccesoSesionWebinarDentro1Dia}"))
                    {
                        var valorFormateado = valor.FechaTermino.ToString("hh:mm tt");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_PEspecifico.HoraFinAccesoSesionWebinarDentro1Dia}", valorFormateado);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            //listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.HoraFinAccesoSesionWebinarDentro1Dia}")).FirstOrDefault().texto = valorFormateado;
                        }
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaAutoEvaluacionCompleto}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCronogramaAutoEvaluacionCompleto(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CronogramaAutoEvaluacionCompleto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CronogramaAutoEvaluacionCompleto}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencidas}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesVencidas}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.AutoEvaluacionesVencidas}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesCompletas}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerAutoEvaluacionesCompletas(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesCompletas}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.AutoEvaluacionesCompletas}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CantidadAutoEvaluacionesPendientes}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCantidadAutoEvaluacionesPendientes(matriculaCabecera.Id).ToString();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CantidadAutoEvaluacionesPendientes}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CantidadAutoEvaluacionesPendientes}")).FirstOrDefault().texto = valor;
                    }
                }

                // Nuevos solicitadas por Pilar
                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace1Dia}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, -1, false, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace1Dia}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace1Dia}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace3Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, -3, false, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace3Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace3Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace7Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, -7, false, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace7Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace7Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencerProximos3Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, 3, true, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesVencerProximos3Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.AutoEvaluacionesVencerProximos3Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoAutoEvaluacionesVencerProximos3Dias}"))
                {
                    var detalleCuota = _repMatriculaCabecera.ObtenerDetalleAutoEvaluacionesVencidas(matriculaCabecera.Id, 3, true);
                    var valor = "";
                    if (detalleCuota.Count() > 0)
                    {
                        valor = detalleCuota.FirstOrDefault().FechaCronograma.ToString("dd/MM/yyyy");
                    }

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.FechaVencimientoAutoEvaluacionesVencerProximos3Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.FechaVencimientoAutoEvaluacionesVencerProximos3Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencerHoy}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, 0, true, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesVencerHoy}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.AutoEvaluacionesVencerHoy}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoAutoEvaluacionesVencerHoy}"))
                {
                    var detalleCuota = _repMatriculaCabecera.ObtenerDetalleAutoEvaluacionesVencidas(matriculaCabecera.Id, 0, true);
                    var valor = "";
                    if (detalleCuota.Count() > 0)
                    {
                        valor = detalleCuota.FirstOrDefault().FechaCronograma.ToString("dd/MM/yyyy");
                    }

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.FechaVencimientoAutoEvaluacionesVencerHoy}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.FechaVencimientoAutoEvaluacionesVencerHoy}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoProximaAutoEvaluacion}"))
                {
                    var detalleCuota = _repMatriculaCabecera.ObtenerDetalleProximaAutoEvaluacion(matriculaCabecera.Id);
                    var valor = detalleCuota.FechaCronograma.ToString("dd/MM/yyyy");

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.FechaVencimientoProximaAutoEvaluacion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.FechaVencimientoProximaAutoEvaluacion}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CantidadAutoEvaluacionesVencidas}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCantidadAutoEvaluacionesVencidas(matriculaCabecera.Id).ToString();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CantidadAutoEvaluacionesVencidas}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CantidadAutoEvaluacionesVencidas}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.PeriodoDuracion}"))
                {
                    var valor = _repPEspecifico.ObtenerPeriodoDuracion(matriculaCabecera.IdPespecifico, matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.PeriodoDuracion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.PeriodoDuracion}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.UrlAccesoSesionOnline}"))
                {
                    var valor = _repPEspecifico.ObtenerUrlAccesoSesionOnline(matriculaCabecera.IdPespecifico);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.UrlAccesoSesionOnline}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.UrlAccesoSesionOnline}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.BeneficiosVersion}"))
                {
                    var valor = _repPgeneral.ObtenerBeneficiosVersion(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.BeneficiosVersion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.BeneficiosVersion}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.ConjuntoSesion}"))
                {
                    var valor = _repPEspecifico.ObtenerConjuntoSesion(matriculaCabecera.IdPespecifico);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.ConjuntoSesion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.ConjuntoSesion}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesion}"))
                {
                    var valor = _repPEspecifico.ObtenerProximoConjuntoSesion(matriculaCabecera.IdPespecifico, 0);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.ProximoConjuntoSesion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.ProximoConjuntoSesion}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro1Dia}"))
                {
                    var valor = _repPEspecifico.ObtenerProximoConjuntoSesion(matriculaCabecera.IdPespecifico, 1);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.ProximoConjuntoSesionDentro1Dia}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.ProximoConjuntoSesionDentro1Dia}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro0DiaWebex}"))
                {
                    var valor = _repPEspecifico.ObtenerProximoConjuntoSesionWebex(IdPEspecifico, 0, IncrementoZonaHoraria, NombrePais, false);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.ProximoConjuntoSesionDentro0DiaWebex}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.ProximoConjuntoSesionDentro0DiaWebex}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro1DiaWebex}"))
                {
                    var valor = _repPEspecifico.ObtenerProximoConjuntoSesionWebex(IdPEspecifico, 1, IncrementoZonaHoraria, NombrePais, false);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.ProximoConjuntoSesionDentro1DiaWebex}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.ProximoConjuntoSesionDentro1DiaWebex}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro2DiaWebex}"))
                {
                    var valor = _repPEspecifico.ObtenerProximoConjuntoSesionWebex(IdPEspecifico, 2, IncrementoZonaHoraria, NombrePais, true);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.ProximoConjuntoSesionDentro2DiaWebex}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.ProximoConjuntoSesionDentro2DiaWebex}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro7DiaWebexSinNombreCurso}"))
                {
                    var valor = _repPEspecifico.ObtenerProximoConjuntoSesionWebex(IdPEspecifico, 7, IncrementoZonaHoraria, NombrePais, false);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.ProximoConjuntoSesionDentro7DiaWebexSinNombreCurso}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.ProximoConjuntoSesionDentro7DiaWebexSinNombreCurso}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro2DiaWebinarSinNombreCurso}"))
                {
                    var valor = _repPEspecifico.ObtenerProximoConjuntoSesionWebex(IdPEspecifico, 2, IncrementoZonaHoraria, NombrePais, false);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.ProximoConjuntoSesionDentro2DiaWebinarSinNombreCurso}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.ProximoConjuntoSesionDentro2DiaWebinarSinNombreCurso}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro3DiaWebex}"))
                {
                    var valor = _repPEspecifico.ObtenerProximoConjuntoSesionWebex(matriculaCabecera.IdPespecifico, 3, IncrementoZonaHoraria, NombrePais, false);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.ProximoConjuntoSesionDentro3DiaWebex}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.ProximoConjuntoSesionDentro3DiaWebex}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro3Dias}"))
                {
                    var valor = _repPEspecifico.ObtenerProximoConjuntoSesion(matriculaCabecera.IdPespecifico, 3);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.ProximoConjuntoSesionDentro3Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.ProximoConjuntoSesionDentro3Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                ///Cronograma pago completo

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaPagoCompleto}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCronogramaPagoCompleto(matriculaCabecera.Id, FormatoHTMLMostrar.Lista);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CronogramaPagoCompleto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CronogramaPagoCompleto}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaPagoCompletoTabla}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCronogramaPagoCompleto(matriculaCabecera.Id, FormatoHTMLMostrar.Tabla);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CronogramaPagoCompletoTabla}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CronogramaPagoCompletoTabla}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.MontoTotal}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerMontoTotal(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.MontoTotal}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.MontoTotal}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_SolicitudOperaciones.ProgramaAccesoTemporalNuevaAula}"))
                {
                    var valor = _repSolicitudOperaciones.FirstBy(x => x.Aprobado == true && x.Realizado == true && x.IdOportunidad == IdOportunidad && x.IdTipoSolicitudOperaciones == 8).ValorNuevo;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_SolicitudOperaciones.ProgramaAccesoTemporalNuevaAula}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_SolicitudOperaciones.ProgramaAccesoTemporalNuevaAula}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_SolicitudOperaciones.FechaFinAccesoTemporalNuevaAula}"))
                {
                    var valor = _repSolicitudOperaciones.FirstBy(x => x.Aprobado == true && x.Realizado == true && x.IdOportunidad == IdOportunidad && x.IdTipoSolicitudOperaciones == 8).ObservacionEncargado;

                    string[] fechas = valor.Split(",");
                    string temp = fechas.Length > 1 ? fechas[0] : string.Empty;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_SolicitudOperaciones.FechaFinAccesoTemporalNuevaAula}", temp);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_SolicitudOperaciones.FechaFinAccesoTemporalNuevaAula}")).FirstOrDefault().texto = temp;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_SolicitudOperaciones.FechaInicioAccesoTemporalNuevaAula}"))
                {
                    var valor = _repSolicitudOperaciones.FirstBy(x => x.Aprobado == true && x.Realizado == true && x.IdOportunidad == IdOportunidad && x.IdTipoSolicitudOperaciones == 8).ObservacionEncargado;
                    string[] fechas = valor.Split(",");
                    string temp = fechas.Length > 1 ? fechas[1] : string.Empty;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_SolicitudOperaciones.FechaInicioAccesoTemporalNuevaAula}", temp);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_SolicitudOperaciones.FechaInicioAccesoTemporalNuevaAula}")).FirstOrDefault().texto = temp;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{T_SolicitudCertificadoFisico.Courier}"))
                {
                    var solicitud = _repSolicitudCertificadoFisico.FirstBy(x=>x.IdMatriculaCabecera==matriculaCabecera.Id);

                    string valor = _repSolicitudCertificadoFisico.ObtenerCourierPorNombre(solicitud.Id);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_SolicitudCertificadoFisico.Courier}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_SolicitudCertificadoFisico.Courier}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{T_SolicitudCertificadoFisico.CodigoSeguimiento}"))
                {
                    var valor = _repSolicitudCertificadoFisico.FirstBy(x => x.IdMatriculaCabecera == matriculaCabecera.Id).CodigoSeguimiento;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_SolicitudCertificadoFisico.CodigoSeguimiento}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_SolicitudCertificadoFisico.CodigoSeguimiento}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Anexo1EstructuraCurricular}"))
                {
                    DocumentosBO documentos = new DocumentosBO(_integraDBContext);
                    List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = documentos.ObtenerListaSeccionDocumentoProgramaGeneral(detalleMatriculaCabecera.IdProgramaGeneral);
                    var seccionEstructura = documentos.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);

                    var estructuraV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "estructura curricular").FirstOrDefault();
                    if (estructuraV2 != null)
                    {
                        var valor = "";
                        valor = "<strong>" + estructuraV2.Seccion + "</strong><br>";
                        valor += estructuraV2.Contenido;
                        valor = valor.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo1EstructuraCurricular}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.Anexo1EstructuraCurricular}")).FirstOrDefault().texto = valor;
                        }
                    }
                    else
                    {
                        var valorFinal = _repDocumentoSeccionPw.ObtenerSecciones(detalleMatriculaCabecera.IdProgramaGeneral);
                        var detalle = valorFinal.Where(x => x.Titulo.Contains("Estructura Curricular")).FirstOrDefault();
                        var valor = "";
                        if (detalle != null)
                        {
                            valor = "<h2>" + detalle.Titulo + "</h2>";
                            valor += detalle.Contenido;
                        }
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo1EstructuraCurricular}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.Anexo1EstructuraCurricular}")).FirstOrDefault().texto = valor;
                        }
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Anexo2Certificacion}"))
                {
                    DocumentosBO documentos = new DocumentosBO(_integraDBContext);
                    List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = documentos.ObtenerListaSeccionDocumentoProgramaGeneral(detalleMatriculaCabecera.IdProgramaGeneral);
                    var seccionEstructura = documentos.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);

                    var certificacionV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "certificacion").FirstOrDefault();
                    if (certificacionV2 != null)
                    {
                        var valor = "";
                        valor = "<strong>" + certificacionV2.Seccion + "</strong><br>";
                        valor += certificacionV2.Contenido;
                        valor = valor.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo2Certificacion}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.Anexo2Certificacion}")).FirstOrDefault().texto = valor;
                        }
                    }
                    else
                    {
                        var valorFinal = _repDocumentoSeccionPw.ObtenerSecciones(detalleMatriculaCabecera.IdProgramaGeneral);
                        var detalle = valorFinal.Where(x => x.Titulo.Contains("Certificación")).FirstOrDefault();
                        var valor = "";
                        if (detalle != null)
                        {
                            valor = "<h2>" + detalle.Titulo + "</h2>";
                            valor += detalle.Contenido;
                        }
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo2Certificacion}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.Anexo2Certificacion}")).FirstOrDefault().texto = valor;
                        }
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaPagoCompletoCuotasVencidas}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCronogramaPagoCompletoCuotasVencidas(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CronogramaPagoCompletoCuotasVencidas}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CronogramaPagoCompletoCuotasVencidas}")).FirstOrDefault().texto = valor;
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CantidadCuotasPendientes}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCantidadCuotasPendientes(matriculaCabecera.Id).ToString();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CantidadCuotasPendientes}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CantidadCuotasPendientes}")).FirstOrDefault().texto = valor;
                    }
                }

                // Solicitados por Pilar
                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasMayorIgual6Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCuotasVencidas(matriculaCabecera.Id, -6, false, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CuotasVencidasMayorIgual6Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CuotasVencidasMayorIgual6Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                // Solicitados por Celina
                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasMayorIgual90Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCuotasVencidas(matriculaCabecera.Id, -90, false, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CuotasVencidasMayorIgual90Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CuotasVencidasMayorIgual90Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CantidadCuotasVencidasMayorIgual6Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCantidadCuotasVencidas(matriculaCabecera.Id, -6, false, plantilla.IdPlantillaBase).ToString();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CantidadCuotasVencidasMayorIgual6Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CantidadCuotasVencidasMayorIgual6Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasHace1Dia}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCuotasVencidas(matriculaCabecera.Id, -1, true, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CuotasVencidasHace1Dia}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CuotasVencidasHace1Dia}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoCuotasVencidasHace1Dia}"))
                {
                    var detalleCuota = _repMatriculaCabecera.ObtenerDetalleCuotasVencidas(matriculaCabecera.Id, -1, true);
                    var valorFormateado = "";
                    if (detalleCuota.Count() > 0)
                    {
                        valorFormateado = detalleCuota.FirstOrDefault().FechaVencimiento.ToString("dd/MM/yyyy");
                    }
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.FechaVencimientoCuotasVencidasHace1Dia}", valorFormateado);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.FechaVencimientoCuotasVencidasHace1Dia}")).FirstOrDefault().texto = valorFormateado;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasHace3Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCuotasVencidas(matriculaCabecera.Id, -3, false, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CuotasVencidasHace3Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CuotasVencidasHace3Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasHace7Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCuotasVencidas(matriculaCabecera.Id, -7, false, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CuotasVencidasHace7Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CuotasVencidasHace7Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencerHoy}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCuotasVencidas(matriculaCabecera.Id, 0, true, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CuotasVencerHoy}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CuotasVencerHoy}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoCuotasVencerHoy}"))
                {
                    var detalleCuota = _repMatriculaCabecera.ObtenerDetalleCuotasVencidas(matriculaCabecera.Id, 0, true);
                    var valor = "";
                    if (detalleCuota.Count() > 0)
                    {
                        valor = detalleCuota.FirstOrDefault().FechaVencimiento.ToString("dd/MM/yyyy");
                    }
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.FechaVencimientoCuotasVencerHoy}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.FechaVencimientoCuotasVencerHoy}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencerProximos3Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCuotasVencidas(matriculaCabecera.Id, 3, true, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CuotasVencerProximos3Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CuotasVencerProximos3Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoCuotasVencerProximos3Dias}"))
                {
                    var detalleCuota = _repMatriculaCabecera.ObtenerDetalleCuotasVencidas(matriculaCabecera.Id, 3, true);
                    var valor = "";
                    if (detalleCuota.Count() > 0)
                    {
                        valor = detalleCuota.FirstOrDefault().FechaVencimiento.ToString("dd/MM/yyyy");
                    }
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.FechaVencimientoCuotasVencerProximos3Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.FechaVencimientoCuotasVencerProximos3Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoProximaCuota}"))
                {
                    var detalleCuota = _repMatriculaCabecera.ObtenerDetalleProximaCuota(matriculaCabecera.Id);
                    var valor = detalleCuota.FechaVencimiento.ToString("dd/MM/yyyy");

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.FechaVencimientoProximaCuota}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.FechaVencimientoProximaCuota}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CantidadCuotasVencidas}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCantidadCuotasVencidas(matriculaCabecera.Id).ToString();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CantidadCuotasVencidas}", valor);

                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CantidadCuotasVencidas}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.5PorcientoDescuentoTotalCuotasPendientes}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerDescuentoCuotasPendientesPorPorcentaje(matriculaCabecera.Id, Convert.ToDecimal(0.05));
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.5PorcientoDescuentoTotalCuotasPendientes}", valor);

                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.5PorcientoDescuentoTotalCuotasPendientes}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.8PorcientoDescuentoTotalCuotasPendientes}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerDescuentoCuotasPendientesPorPorcentaje(matriculaCabecera.Id, Convert.ToDecimal(0.08));
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.8PorcientoDescuentoTotalCuotasPendientes}", valor);

                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.8PorcientoDescuentoTotalCuotasPendientes}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.10PorcientoDescuentoTotalCuotasPendientes}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerDescuentoCuotasPendientesPorPorcentaje(matriculaCabecera.Id, Convert.ToDecimal(0.10));
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.10PorcientoDescuentoTotalCuotasPendientes}", valor);

                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.10PorcientoDescuentoTotalCuotasPendientes}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.FechaLimitePorAbandonar}"))
                {
                    var valor = ((DateTime.Now).AddMonths(1)).ToString("dd/MM/yyyy");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.FechaLimitePorAbandonar}", valor);

                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.FechaLimitePorAbandonar}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                {
                    var valor = matriculaCabecera.CodigoMatricula;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CodigoMatricula}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CodigoMatricula}")).FirstOrDefault().texto = valor;
                    }
                }

                // Reemplazar nombre 1 alumno
                if (plantillaBase.Cuerpo.Contains("{T_Alumno.NombreCompleto}"))
                {
                    var valor = alumno.NombreCompleto;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.NombreCompleto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.NombreCompleto}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.NombreCompleto}")).FirstOrDefault().texto = valor;
                    }
                }
                // Reemplazar Fecha Inicio Capacitacion
                if (plantillaBase.Cuerpo.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                {
                    var valor = _repAlumno.ObtenerFechaInicioCapacitacion(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.FechaInicioCapacitacion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.FechaInicioCapacitacion}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.FechaInicioCapacitacion}")).FirstOrDefault().texto = valor;
                    }
                }

                // Reemplazar Fecha Fin Capacitacion
                if (plantillaBase.Cuerpo.Contains("{T_Alumno.FechaFinCapacitacion}"))
                {
                    var valor = _repAlumno.ObtenerFechaFinCapacitacion(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.FechaFinCapacitacion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.FechaFinCapacitacion}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.FechaFinCapacitacion}")).FirstOrDefault().texto = valor;
                    }
                }
                // Reemplazar Calificacion Promedio
                if (plantillaBase.Cuerpo.Contains("{T_Alumno.CalificacionPromedio}"))
                {
                    var valor = _repAlumno.ObtenerNotaPromedio(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.CalificacionPromedio}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.CalificacionPromedio}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.CalificacionPromedio}")).FirstOrDefault().texto = valor;
                    }
                }

                // Reemplazar Fecha Emision Certificado
                if (plantillaBase.Cuerpo.Contains("{T_Alumno.FechaEmisionCertificado}"))
                {
                    var valor = _repAlumno.ObtenerFechaEmision();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.FechaEmisionCertificado}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.FechaEmisionCertificado}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.FechaEmisionCertificado}")).FirstOrDefault().texto = valor;
                    }
                }

                // Reemplazar Fecha Codigo Certificado
                if (plantillaBase.Cuerpo.Contains("{T_Alumno.CodigoCertificado}"))
                {
                    var valor = _repAlumno.ObtenerCodigoCertificado(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.CodigoCertificado}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.CodigoCertificado}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.CodigoCertificado}")).FirstOrDefault().texto = valor;
                    }
                }

                // Reemplazar duracion en horas de Programa Especifico
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.duracion}"))
                {
                    var valor = _repPEspecifico.ObtenerDuracionProgramaEspecifico(matriculaCabecera.IdPespecifico, matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.duracion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPEspecifico.duracion}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPEspecifico.duracion}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_Alumno.NroDocumento}"))
                {
                    var valor = alumno.Dni;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.NroDocumento}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.NroDocumento}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.direccion}"))
                {
                    var valor = alumno.Direccion.ToUpper();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.direccion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.direccion}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.NombreCiudad}"))
                {
                    var valor = alumno.NombreCiudadOrigen;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.NombreCiudad}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.NombreCiudad}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Version}"))
                {
                    var valor = _repPgeneral.ObtenerVersion(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Version}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.Version}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tpla_pgeneral.pw_duracion}"))
                {
                    var valor = _repPgeneral.ObtenerDuracionMeses(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tpla_pgeneral.pw_duracion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tpla_pgeneral.pw_duracion}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.nombre1}"))
                {
                    var valor = alumno.Nombre1;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{tAlumnos.nombre2}"))
                {
                    var valor = alumno.Nombre2;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.nombre2}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre2}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre2}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{tAlumnos.apematerno}"))
                {
                    var valor = alumno.ApellidoMaterno;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.apematerno}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.apematerno}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.apematerno}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{tAlumnos.apepaterno}"))
                {
                    var valor = alumno.ApellidoPaterno;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.apepaterno}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.apepaterno}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.apepaterno}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{tpla_pgeneral.pw_duracion}"))
                {
                    var valor = DatosCompuestosOportunidad.pw_duracion;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tpla_pgeneral.pw_duracion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tpla_pgeneral.pw_duracion}")).FirstOrDefault().texto = valor;
                    }
                    else
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tpla_pgeneral.pw_duracion}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.NombrePais}"))
                {
                    var valor = alumno.NombrePaisOrigen;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.NombrePais}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.NombrePais}")).FirstOrDefault().texto = valor;
                    }

                }

                //nombre programa general
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    var valor = detalleMatriculaCabecera.NombreProgramaGeneral;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.Nombre}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.Nombre}")).FirstOrDefault().texto = valor;
                    }
                }

                //nombre centro de costo
                if (plantillaBase.Cuerpo.Contains("{T_CentroCosto.Nombre}"))
                {
                    var valor = detalleMatriculaCabecera.NombreCentroCosto;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_CentroCosto.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_CentroCosto.Nombre}")).FirstOrDefault().texto = valor;
                    }
                }

                // Horario atencion personal
                if (plantillaBase.Cuerpo.Contains("{T_Personal.HorarioAtencion}"))
                {
                    var valor = _repPersonal.ObtenerHorarioTrabajo(oportunidad.IdPersonalAsignado);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.HorarioAtencion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Personal.HorarioAtencion}")).FirstOrDefault().texto = valor;
                    }
                }

                //anexo personal
                if (plantillaBase.Cuerpo.Contains("{tPersonal.Anexo}"))
                {
                    var valor = personal.Anexo;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.Anexo}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.Anexo}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{tPersonal.nombres}"))
                {
                    string valor = personal.Nombres;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.nombres}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.nombres}")).FirstOrDefault().texto = valor;
                    }
                }

                // Apellidos del personal
                if (plantillaBase.Cuerpo.Contains("{tPersonal.apellidos}"))
                {
                    string valor = personal.Apellidos;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.apellidos}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.apellidos}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_AlumnoMoodle.CursoActualMoodle}"))
                {
                    var cursoMoodle = _repMatriculaCabecera.ObtenerCursoActualAlumnoMoodle(matriculaCabecera.Id);
                    if (cursoMoodle != null)
                    {
                        var valor = cursoMoodle[0].NombreCurso;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_AlumnoMoodle.CursoActualMoodle}", valor);
                        }
                    }
                    else
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_AlumnoMoodle.CursoActualMoodle}", "");
                    }
                }


                //Telefono personal
                if (plantillaBase.Cuerpo.Contains("{T_Personal.Telefono}"))
                {
                    //var valor = personal.Telefono;
                    var valor = alumno.NroTelefonoCoordinador;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.Telefono}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Personal.Telefono}")).FirstOrDefault().texto = valor;
                    }
                }


                if (plantillaBase.Asunto.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                {
                    var valor = matriculaCabecera.CodigoMatricula;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_MatriculaCabecera.CodigoMatricula}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        //listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CodigoMatricula}")).FirstOrDefault().texto = valor;
                    }
                }

                //reemplazar nombre 1 alumno
                if (plantillaBase.Asunto.Contains("{tAlumnos.nombre1}"))
                {
                    var valor = alumno.Nombre1;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tAlumnos.nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        //listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                }

                //nombre programa general
                if (plantillaBase.Asunto.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tPLA_PGeneral.Nombre}", detalleMatriculaCabecera.NombreProgramaGeneral);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        //listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{tPLA_PGeneral.Nombre}", texto = detalleMatriculaCabecera.NombreProgramaGeneral });
                    }
                }

                //nombre centro de costo
                if (plantillaBase.Asunto.Contains("{T_CentroCosto.Nombre}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_CentroCosto.Nombre}", detalleMatriculaCabecera.NombreCentroCosto);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        //listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_CentroCosto.Nombre}", texto = detalleMatriculaCabecera.NombreCentroCosto });
                    }
                }

                //numero whatsapp por pais alumno
                if (plantillaBase.Asunto.Contains("{T_Alumno.NroWhatsAppCoordinador}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_Alumno.NroWhatsAppCoordinador}", alumno.NroWhatsAppCoordinador);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        //listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_Alumno.NroWhatsAppCoordinador}", texto = alumno.NroWhatsAppCoordinador });
                    }
                }

                //horario atencion personal
                if (plantillaBase.Asunto.Contains("{T_Personal.HorarioAtencion}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_Personal.HorarioAtencion}", _repPersonal.ObtenerHorarioTrabajo(oportunidad.IdPersonalAsignado));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        //listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_Personal.HorarioAtencion}", texto = _repPersonal.ObtenerHorarioTrabajo(oportunidad.IdPersonalAsignado) });
                    }
                }

                //anexo personal
                if (plantillaBase.Asunto.Contains("{tPersonal.Anexo}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tPersonal.Anexo}", _repPersonal.FirstById(oportunidad.IdPersonalAsignado).Anexo);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        //listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{tPersonal.Anexo}", texto = _repPersonal.FirstById(oportunidad.IdPersonalAsignado).Anexo });
                    }
                }

                var listaArchivosAdjunto = new List<string>()
                {
                    "{ArchivoAdjunto.ManualIngresoAulaVirtual}",
                    "{ArchivoAdjunto.ManualBSPlay}",
                    "{ArchivoAdjunto.ManualConectarseSesionWebinar}",
                    "{ArchivoAdjunto.ManualConectarseSesionVirtual}"
                };

                var listaImagenes = new List<Image>();
                //listaImagenes.Add(new Image()
                //{
                //    Content = Convert.ToBase64String(ExtendedWebClient.GetFile(urlFirmaRepositorio)),
                //    Type = "image/png",
                //    Name = "image_name"
                //});

                //logica documentos adjuntos

                if (listaArchivosAdjunto.Any(plantillaBase.Cuerpo.Contains))
                {
                    if (plantillaBase.Cuerpo.Contains("{ArchivoAdjunto.ManualIngresoAulaVirtual}"))
                    {
                        EmailReemplazado.ListaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualIngresoAulaVirtual}"))),
                            Name = "Manual para ingreso al Aula Virtual.pdf",
                            Type = "application/pdf"
                        });
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ArchivoAdjunto.ManualIngresoAulaVirtual}", "");
                    }

                    if (plantillaBase.Cuerpo.Contains("{ArchivoAdjunto.ManualBSPlay}"))
                    {
                        EmailReemplazado.ListaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualBSPlay}"))),
                            Name = "Manual BS Play.pdf",
                            Type = "application/pdf"
                        });
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ArchivoAdjunto.ManualBSPlay}", "");
                    }

                    if (plantillaBase.Cuerpo.Contains("{ArchivoAdjunto.ManualConectarseSesionWebinar}"))
                    {
                        EmailReemplazado.ListaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualConectarseSesionWebinar}"))),
                            Name = "Manual para conectarse a la sesión webinar.pdf",
                            Type = "application/pdf"
                        });
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ArchivoAdjunto.ManualConectarseSesionWebinar}", "");
                    }

                    if (plantillaBase.Cuerpo.Contains("{ArchivoAdjunto.ManualConectarseSesionVirtual}"))
                    {
                        EmailReemplazado.ListaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualConectarseSesionVirtual}"))),
                            Name = "Manual para conectarse a la sesión virtual.pdf",
                            Type = "application/pdf"
                        });
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ArchivoAdjunto.ManualConectarseSesionVirtual}", "");
                    }
                }

                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlDescargarAplicativoAndroid%7D", "{Link.UrlDescargarAplicativoAndroid}");
                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlDescargarAplicativoIOS%7D", "{Link.UrlDescargarAplicativoIOS}");
                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlGuiaAccederSesionWebinarPorVideo%7D", "{Link.UrlGuiaAccederSesionWebinarPorVideo}");
                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlAulaVirtual%7D", "{Link.UrlAulaVirtual}");
                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlManualBSPlay%7D", "{Link.UrlManualBSPlay}");
                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlPortalWeb%7D", "{Link.UrlPortalWeb}");

                //{ link.urlimagenfelizcumpleanios}

                var listaTextoPlano = new List<string>()
                {
                    "{Link.UrlAulaVirtual}",
                    "{Link.UrlDescargarAplicativoAndroid}",
                    "{Link.UrlDescargarAplicationIOS}",
                    "{Link.UrlGuiaAccederSesionWebinarPorVideo}",
                    "{Link.UrlImagenFelizCumpleanios}",
                    "{Link.UrlManualBSPlay}",
                    "{Link.UrlPortalWeb}"
                };
                //Etiquetas texto plano
                if (listaTextoPlano.Any(plantillaBase.Cuerpo.Contains))
                {
                    if (plantillaBase.Cuerpo.Contains("{Link.UrlPortalWeb}"))
                    {
                        var valor = ValorEstaticoUtil.Get("{Link.UrlPortalWeb}");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlPortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{Link.UrlPortalWeb}")).FirstOrDefault().texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{Link.UrlManualBSPlay}"))
                    {
                        var valor = ValorEstaticoUtil.Get("{Link.UrlManualBSPlay}");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlManualBSPlay}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{Link.UrlManualBSPlay}")).FirstOrDefault().texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{Link.UrlAulaVirtual}"))
                    {
                        var valor = ValorEstaticoUtil.Get("{Link.UrlAulaVirtual}");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlAulaVirtual}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{Link.UrlAulaVirtual}")).FirstOrDefault().texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{Link.UrlDescargarAplicativoAndroid}"))
                    {
                        var valor = ValorEstaticoUtil.Get("{Link.UrlDescargarAplicativoAndroid}");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlDescargarAplicativoAndroid}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{Link.UrlDescargarAplicativoAndroid}")).FirstOrDefault().texto = valor;
                        }
                    }
                    if (plantillaBase.Cuerpo.Contains("{Link.UrlDescargarAplicativoIOS}"))
                    {
                        var valor = ValorEstaticoUtil.Get("{Link.UrlDescargarAplicativoIOS}");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlDescargarAplicativoIOS}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{Link.UrlDescargarAplicativoIOS}")).FirstOrDefault().texto = valor;
                        }
                    }
                    if (plantillaBase.Cuerpo.Contains("{Link.UrlGuiaAccederSesionWebinarPorVideo}"))
                    {
                        var valor = ValorEstaticoUtil.Get("{Link.UrlGuiaAccederSesionWebinarPorVideo}");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlGuiaAccederSesionWebinarPorVideo}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{Link.UrlGuiaAccederSesionWebinarPorVideo}")).FirstOrDefault().texto = valor;
                        }
                    }
                    if (plantillaBase.Cuerpo.Contains("{Link.UrlImagenFelizCumpleanios}"))
                    {
                        //var valor = ValorEstaticoUtil.Get("{Link.UrlImagenFelizCumpleanios}");
                        //var valor = ValorEstaticoUtil.Get("{Link.UrlImagenFelizCumpleanios}");
                        var valor = _repAlumno.ObtenerUrlImagenFelizCumpleanios();
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlImagenFelizCumpleanios}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{Link.UrlImagenFelizCumpleanios}")).FirstOrDefault().texto = valor;
                        }
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_Alumno.FormaPago}"))
                {
                    var detallePEspecifico = _repPEspecifico.ObtenerDetalle(matriculaCabecera.Id);
                    var valor = alumno.ObtenerFormaPago(detallePEspecifico.IdModalidadCurso, detallePEspecifico.IdCiudad, matriculaCabecera.CodigoMatricula, detallePEspecifico.MonedaCronograma);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.FormaPago}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.FormaPago}")).FirstOrDefault().texto = valor;
                    }
                }

                var listaAccesoAulaVirtual = new List<string>()
                {
                    "{T_Alumno.UsuarioAulaVirtual}",
                    "{T_Alumno.ClaveAulaVirtual}"
                };
                if (listaAccesoAulaVirtual.Any(plantillaBase.Cuerpo.Contains))
                {
                    var accesoAulaVirtual = _repMatriculaCabecera.ObtenerDetalleAccesoAulaVirtual(matriculaCabecera.Id);
                    // Acceso aula virtual
                    if (plantillaBase.Cuerpo.Contains("{T_Alumno.UsuarioAulaVirtual}"))
                    {
                        var valor = accesoAulaVirtual.UsuarioMoodle;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.UsuarioAulaVirtual}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.UsuarioAulaVirtual}")).FirstOrDefault().texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_Alumno.ClaveAulaVirtual}"))
                    {
                        var valor = accesoAulaVirtual.ClaveMoodle;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.ClaveAulaVirtual}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.ClaveAulaVirtual}")).FirstOrDefault().texto = valor;
                        }
                    }
                }
                var listaAccesoDocentePortalWeb = new List<string>()
                {
                    "{T_Docente.UsuarioPortalWeb}",
                    "{T_Docente.ClavePortalWeb}"
                };
                if (listaAccesoDocentePortalWeb.Any(plantillaBase.Cuerpo.Contains))
                {
                    var accesoPortalWeb = _repMatriculaCabecera.ObtenerDetalleAccesoDocentePortalWeb(IdProveedor);

                    //Acceso PW
                    if (plantillaBase.Cuerpo.Contains("{T_Docente.UsuarioPortalWeb}"))
                    {
                        var valor = accesoPortalWeb.Usuario;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Docente.UsuarioPortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Docente.UsuarioPortalWeb}")).FirstOrDefault().texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_Docente.ClavePortalWeb}"))
                    {
                        var valor = accesoPortalWeb.Clave;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Docente.ClavePortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Docente.ClavePortalWeb}")).FirstOrDefault().texto = valor;
                        }
                    }
                }


                var listaAccesoPortalWeb = new List<string>()
                {
                    "{T_Alumno.UsuarioPortalWeb}",
                    "{T_Alumno.ClavePortalWeb}"
                };

                if (listaAccesoPortalWeb.Any(plantillaBase.Cuerpo.Contains))
                {
                    var accesoPortalWeb = _repMatriculaCabecera.ObtenerDetalleAccesoPortalWeb(matriculaCabecera.Id);

                    //Acceso PW
                    if (plantillaBase.Cuerpo.Contains("{T_Alumno.UsuarioPortalWeb}"))
                    {
                        var valor = accesoPortalWeb.Usuario;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.UsuarioPortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.UsuarioPortalWeb}")).FirstOrDefault().texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_Alumno.ClavePortalWeb}"))
                    {
                        var valor = accesoPortalWeb.Clave;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.ClavePortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.ClavePortalWeb}")).FirstOrDefault().texto = valor;
                        }
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{T_Personal.FirmaCorreo}"))
                {
                    var valor = personal.FirmaHtml;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.FirmaCorreo}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Personal.FirmaCorreo}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_Personal.FirmaCorreoImagen}"))
                {
                    var valor = personal.ObtenerFirmaCorreoImagen(alumno.IdCodigoPais, alumno.IdCiudad);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.FirmaCorreoImagen}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Personal.FirmaCorreoImagen}")).FirstOrDefault().texto = valor;
                    }
                }

                //Presencial
                //reemplaza en la etiqueta de firma tambien
                //numero whatsapp por pais alumno
                if (plantillaBase.Cuerpo.Contains("{T_Alumno.NroWhatsAppCoordinador}"))
                {
                    var valor = alumno.NroWhatsAppCoordinador;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.NroWhatsAppCoordinador}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.NroWhatsAppCoordinador}")).FirstOrDefault().texto = valor;
                    }
                }

                //calcular ultima sesion
                var listaPEspecificoSesion = new List<string>()
                {
                    "{T_PEspecificoSesion.UrlUbicacionCiudad}",
                    "{T_PEspecificoSesion.DireccionDictadoClases}",
                    "{T_PEspecificoSesion.NombreCiudadDictadoClases}",
                    "{T_PEspecificoSesion.NombreDocente}"
                };
                if (listaPEspecificoSesion.Any(plantillaBase.Cuerpo.Contains))
                {
                    var idPEspecificoSesion = _repMatriculaCabecera.ObtenerProximaSesion(matriculaCabecera.IdPespecifico, 0);
                    //se calcula en base a una sesion
                    if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.UrlUbicacionCiudad}"))
                    {
                        var valor = _repPEspecificoSesion.ObtenerUrlUbicacionCiudad(idPEspecificoSesion);
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.UrlUbicacionCiudad}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecificoSesion.UrlUbicacionCiudad}", texto = valor });
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.DireccionDictadoClases}"))
                    {
                        var valor = _repPEspecificoSesion.ObtenerDireccionDictadoClases(idPEspecificoSesion);
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.DireccionDictadoClases}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecificoSesion.DireccionDictadoClases}", texto = valor });
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.NombreCiudadDictadoClases}"))
                    {
                        var valor = _repPEspecificoSesion.ObtenerNombreCiudadDictadoClases(idPEspecificoSesion);
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.NombreCiudadDictadoClases}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecificoSesion.NombreCiudadDictadoClases}", texto = valor });
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.NombreDocente}"))
                    {
                        var valor = _repPEspecificoSesion.ObtenerNombreDocenteDictadoClases(idPEspecificoSesion);
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.NombreDocente}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecificoSesion.NombreDocente}", texto = valor });
                        }
                    }
                }

                // Calcular ultima sesion
                var listaPEspecificoSesionDentro3Dias = new List<string>()
                {
                    "{T_PEspecificoSesion.UrlUbicacionCiudadDentro3Dias}",
                    "{T_PEspecificoSesion.DireccionDictadoClasesDentro3Dias}",
                    "{T_PEspecificoSesion.NombreCiudadDictadoClasesDentro3Dias}",
                    "{T_PEspecificoSesion.NombreDocenteDentro3Dias}"
                };
                if (listaPEspecificoSesion.Any(plantillaBase.Cuerpo.Contains))
                {
                    var idPEspecificoSesion = _repMatriculaCabecera.ObtenerProximaSesion(matriculaCabecera.IdPespecifico, 3);
                    // Se calcula en base a una sesion
                    if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.UrlUbicacionCiudadDentro3Dias}"))
                    {
                        var valor = _repPEspecificoSesion.ObtenerUrlUbicacionCiudad(idPEspecificoSesion);
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.UrlUbicacionCiudadDentro3Dias}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecificoSesion.UrlUbicacionCiudadDentro3Dias}", texto = valor });
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.DireccionDictadoClasesDentro3Dias}"))
                    {
                        var valor = _repPEspecificoSesion.ObtenerDireccionDictadoClases(idPEspecificoSesion);
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.DireccionDictadoClasesDentro3Dias}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecificoSesion.DireccionDictadoClasesDentro3Dias}", texto = valor });
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.NombreCiudadDictadoClasesDentro3Dias}"))
                    {
                        var valor = _repPEspecificoSesion.ObtenerNombreCiudadDictadoClases(idPEspecificoSesion);
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.NombreCiudadDictadoClasesDentro3Dias}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecificoSesion.NombreCiudadDictadoClasesDentro3Dias}", texto = valor });
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.NombreDocenteDentro3Dias}"))
                    {
                        var valor = _repPEspecificoSesion.ObtenerNombreDocenteDictadoClases(idPEspecificoSesion);
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.NombreDocenteDentro3Dias}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecificoSesion.NombreDocenteDentro3Dias}", texto = valor });
                        }
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.CalendarioSemanal}"))
                {
                    //var listaPrueba = new List<PEspecificoSesionDetalleDTO>
                    //{
                    //    new PEspecificoSesionDetalleDTO()
                    //    {
                    //        Id = 123,
                    //        IdPEspecifico = 1234,
                    //        FechaHoraInicio = DateTime.Now,
                    //        FechaHoraFin = DateTime.Now,
                    //        NombrePEspecifico = "PESPECIFICO 1",
                    //        NombreCurso = "Nombre curso 1"
                    //    },
                    //    new PEspecificoSesionDetalleDTO()
                    //    {
                    //        Id = 1234,
                    //        IdPEspecifico = 12345,
                    //        FechaHoraInicio = DateTime.Now,
                    //        FechaHoraFin = DateTime.Now,
                    //        NombrePEspecifico = "PESPECIFICO 2",
                    //        NombreCurso = "Nombre curso 2"
                    //    },
                    //    new PEspecificoSesionDetalleDTO()
                    //    {
                    //        Id = 12345,
                    //        IdPEspecifico = 123456,
                    //        FechaHoraInicio = DateTime.Now,
                    //        FechaHoraFin = DateTime.Now,
                    //        NombrePEspecifico = "PESPECIFICO 3",
                    //        NombreCurso = "Nombre curso 3"
                    //    }
                    //};

                    //EmailReemplazado.ListaArchivosAdjuntos.Add(new EmailAttachment()
                    //{
                    //    Base64 = true,
                    //    Content = Convert.ToBase64String(_repPEspecifico.ObtenerCalendarioSemanal(listaPrueba)),
                    //    Name = "Calendario Semanal - BSG Institute.ics",
                    //    Type = "text/calendar"
                    //});
                    //plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ArchivoAdjunto.CalendarioSemanal}", "");
                }


                // TODO (To Do: Por hacer)
                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.UrlQuejaSugerenciaHoraActual}"))
                {
                    var valor = _repPEspecifico.ObtenerUrlQuejaSugerenciaNDiasNHora(matriculaCabecera.Id, 0, 0);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.UrlQuejaSugerenciaHoraActual}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecifico.UrlQuejaSugerenciaHoraActual}", texto = valor });
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.HorarioSemanaSesionWebex}"))
                {
                    var valor = _repPEspecificoSesion.ObtenerHorarioSemanaSesionWebex(matriculaCabecera.IdPespecifico);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.HorarioSemanaSesionWebex}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecificoSesion.HorarioSemanaSesionWebex}", texto = valor });
                    }
                }

                // TODO (To Do: Por hacer)
                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.NombreCursoEncuestaHoraActual}"))
                {
                    var valor = _repPEspecifico.ObtenerNombreCursoEncuestaNDiasNHora(matriculaCabecera.Id, 0, 0);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.NombreCursoEncuestaHoraActual}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecifico.NombreCursoEncuestaHoraActual}", texto = valor });
                    }
                }


                // TODO (To Do: Por hacer)
                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.UrlEncuestaHoraActual}"))
                {
                    var valor = _repPEspecifico.ObtenerUrlEncuestaNDiasNHora(matriculaCabecera.Id, 0, 0);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.UrlEncuestaHoraActual}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecifico.UrlEncuestaHoraActual}", texto = valor });
                    }
                }

                // TODO (To Do: Por hacer)
                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.FechaEmisionCertificado}"))
                {
                    var valor = _repPespecifico.ObtenerFechaEmisionUltimoCertificado(1, 1);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.FechaEmisionCertificado}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecifico.FechaEmisionCertificado}", texto = valor });
                    }
                }

                // SECCION NUEVOS TEMPLATES

                // ESTRUCTURA CURRICULAR
                if (plantillaBase.Cuerpo.Contains("Template") && plantillaBase.Cuerpo.Contains("Estructura Curricular"))
                {
                    var valor = "";

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {

                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {

                    }
                }

                // CERTIFICACION
                if (plantillaBase.Cuerpo.Contains("Template") && plantillaBase.Cuerpo.Contains("Certificación"))
                {
                    foreach (var item in listaObjetoWhasApp.Where(x => x.codigo.Contains("Template")))
                    {

                    }
                }

                //Templates
                if (plantillaBase.Cuerpo.Contains("Template"))
                {
                    //foreach (var item in listaObjetoWhasApp.Where(x => x.codigo.Contains("Template")))
                    //{
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        foreach (var item in listaObjetoWhasApp.Where(x => x.codigo.Contains("Template")))
                        {
                            var array = item.codigo.Replace("{", "").Replace("}", "").Split(".");

                            var IdPlantilla = array[3];
                            var IdColumna = array[4];

                            var valor = _repPEspecifico.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna), oportunidad.IdCentroCosto ?? default(int));
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace(item.codigo, valor.Valor);
                        }
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        foreach (var item in listaObjetoWhasApp.Where(x => x.codigo.Contains("Template")))
                        {
                            var array = item.codigo.Replace("{", "").Replace("}", "").Split(".");

                            var IdPlantilla = array[3];
                            var IdColumna = array[4];

                            var valor = _repPEspecifico.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna), oportunidad.IdCentroCosto ?? default(int));
                            listaObjetoWhasApp.Where(x => x.codigo.Equals(item.codigo)).FirstOrDefault().texto = valor.Valor;
                        }
                        //listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                    else
                    {
                        foreach (var item in listaObjetoWhasApp.Where(x => x.codigo.Contains("Template")))
                        {
                            var array = item.codigo.Replace("{", "").Replace("}", "").Split(".");

                            var IdPlantilla = array[3];
                            var IdColumna = array[4];

                            var valor = _repPEspecifico.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna), oportunidad.IdCentroCosto ?? default(int));
                            listaObjetoWhasApp.Where(x => x.codigo.Equals(item.codigo)).FirstOrDefault().texto = valor.Valor.Replace("<p>", "<p id='estructura'>");
                        }
                    }
                    //}

                }

                if (IdPEspecifico != 0)
                {//agregamos logica reemplazo

                }

                if (Grupo != 0)
                {//agregamos logica reemplazo

                }
                if (ListaIdMaterialPEspecificoDetalle.Any())
                {
                    if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.MaterialesDescargar}"))
                    {
                        //var valor = _repMatriculaCabecera.ObtenerMateriales(matriculaCabecera.Id);
                        var valor = _repMatriculaCabecera.ObtenerMaterialesPorMaterialPEspecificoDetalle(ListaIdMaterialPEspecificoDetalle);
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.MaterialesDescargar}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.MaterialesDescargar}")).FirstOrDefault().texto = valor;
                        }
                    }
                }
                if (IdMaterialPEspecificoDetalle != 0)
                {
                    if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.MaterialesDescargar}"))
                    {
                        var valor = _repMatriculaCabecera.ObtenerMaterialesPorMaterialPEspecificoDetalle(IdMaterialPEspecificoDetalle);
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.MaterialesDescargar}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.MaterialesDescargar}")).FirstOrDefault().texto = valor;
                        }
                    }
                }
                //DateTime hoy = DateTime.Now;
                //Contenido = Contenido.Replace("## DATEMONTH ##", hoy.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES")));
                //Contenido = Contenido.Replace("##DATEDAYS##", hoy.Day.ToString());
                //Contenido = Contenido.Replace("##DATEYEAR##", hoy.Year.ToString());

                if (plantillaBase.Cuerpo.Contains("{ValorDinamico.DiaFechaActual}"))
                {
                    var valor = FechaActual.Day.ToString();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ValorDinamico.DiaFechaActual}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{ValorDinamico.DiaFechaActual}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{ValorDinamico.NombreMesFechaActual}"))
                {
                    var valor = FechaActual.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ValorDinamico.NombreMesFechaActual}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{ValorDinamico.NombreMesFechaActual}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{ValorDinamico.AnioFechaActual}"))
                {
                    var valor = FechaActual.Year.ToString();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ValorDinamico.AnioFechaActual}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{ValorDinamico.AnioFechaActual}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.LinkConfirmacionWebinarPositiva}"))
                {
                    var link = "https://bsginstitute.com/NotificacionAlumno/Webinar/ConfirmarParticipacion?ses=" + IdPEspecificoSesion + "&mat=" + IdMatriculaCabecera + "&est=True";
                    var valor = "<a href =" + link + "> Confirmar Participación </a>";
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.LinkConfirmacionWebinarPositiva}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecifico.LinkConfirmacionWebinarPositiva}")).FirstOrDefault().texto = valor;
                    }
                }

                if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
                else if (this.IdPlantillaBase == 12 || this.IdPlantillaBase == 13)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para casos de proveedores
        /// </summary>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public void ReemplazarEtiquetasProveedor()
        {
            try
            {
                var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

                var plantilla = _repPlantilla.FirstById(this.IdPlantilla);

                this.IdPlantillaBase = plantilla.IdPlantillaBase;

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Codigo de plantilla no valido!");
                }

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Codigo de plantilla no valido!");
                }

                var materialPEspecificoDetalle = _repMaterialPEspecificoDetalle.FirstById(this.IdMaterialPEspecificoDetalle);

                var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);
                var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();


                foreach (var etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                }

                //reemplazo
                var detalleEnvioMaterialProveedorImpresion = _repMaterialPEspecificoDetalle.ObtenerDetalleMaterialPEspecificoEnviarProveedor(this.IdMaterialPEspecificoDetalle);

                if (plantillaBase.Cuerpo.Contains("{T_Proveedor.NombreContacto}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.NombreProveedor;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Proveedor.NombreContacto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Proveedor.NombreContacto}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.NombreDocente}"))
                {
                    var valor = "docnete temporal";
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.NombreDocente}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecificoSesion.NombreDocente}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MaterialPEspecificoDetalle.UrlMaterialVersionProveedor}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.UrlArchivo;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MaterialPEspecificoDetalle.UrlMaterialVersionProveedor}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MaterialPEspecificoDetalle.UrlMaterialVersionProveedor}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MaterialPEspecificoDetalle.DireccionEntrega}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.DireccionEntrega;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MaterialPEspecificoDetalle.DireccionEntrega}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MaterialPEspecificoDetalle.DireccionEntrega}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MaterialPEspecificoDetalle.FechaAproximadaRecepcion}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.FechaEntrega.ToString("dd/MM/yyyy");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MaterialPEspecificoDetalle.FechaAproximadaRecepcion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MaterialPEspecificoDetalle.FechaAproximadaRecepcion}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_CentroCosto.Nombre}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.NombreCentroCosto;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_CentroCosto.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_CentroCosto.Nombre}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.nombre}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.NombrePEspecifico;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPEspecifico.nombre}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.Grupo}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.Grupo.ToString();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.Grupo}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPEspecifico.Grupo}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MaterialPEspecificoDetalle.DetalleMaterialesSolicitados}"))
                {
                    var valorCantidad = detalleEnvioMaterialProveedorImpresion.Cantidad;
                    var valorTipoMaterial = detalleEnvioMaterialProveedorImpresion.NombreMaterialTipo;

                    var valor = $@"
                               <span>
                                    Tipo: { valorTipoMaterial }
                                    <br>
                                    Cantidad: { valorCantidad }
                                </span>
                            ";
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MaterialPEspecificoDetalle.DetalleMaterialesSolicitados}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MaterialPEspecificoDetalle.DetalleMaterialesSolicitados}")).FirstOrDefault().texto = valor;
                    }
                }


                //asunto
                if (plantillaBase.Asunto.Contains("{T_Proveedor.NombreContacto}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.NombreProveedor;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_Proveedor.NombreContacto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Proveedor.NombreContacto}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Asunto.Contains("{T_MaterialPEspecificoDetalle.UrlMaterialVersionProveedor}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.UrlArchivo;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_MaterialPEspecificoDetalle.UrlMaterialVersionProveedor}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MaterialPEspecificoDetalle.UrlMaterialVersionProveedor}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Asunto.Contains("{T_MaterialPEspecificoDetalle.DireccionEntrega}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.DireccionEntrega;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_MaterialPEspecificoDetalle.DireccionEntrega}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MaterialPEspecificoDetalle.DireccionEntrega}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Asunto.Contains("{T_MaterialPEspecificoDetalle.FechaAproximadaRecepcion}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.FechaEntrega.ToString("dd/MM/yyyy");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_MaterialPEspecificoDetalle.FechaAproximadaRecepcion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MaterialPEspecificoDetalle.FechaAproximadaRecepcion}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Asunto.Contains("{T_CentroCosto.Nombre}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.NombreCentroCosto;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_CentroCosto.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_CentroCosto.Nombre}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Asunto.Contains("{tPEspecifico.nombre}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.NombrePEspecifico;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tPEspecifico.nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPEspecifico.nombre}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Asunto.Contains("{tPEspecifico.Grupo}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.Grupo.ToString();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tPEspecifico.Grupo}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPEspecifico.Grupo}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Asunto.Contains("{T_MaterialPEspecificoDetalle.DetalleMaterialesSolicitados}"))
                {
                    var valorCantidad = detalleEnvioMaterialProveedorImpresion.Cantidad;
                    var valorTipoMaterial = detalleEnvioMaterialProveedorImpresion.NombreMaterialTipo;

                    var valor = $@"
                               <span>
                                    Tipo: { valorTipoMaterial }
                                    <br>
                                    Cantidad: { valorCantidad }
                                </span>
                            ";
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_MaterialPEspecificoDetalle.DetalleMaterialesSolicitados}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MaterialPEspecificoDetalle.DetalleMaterialesSolicitados}")).FirstOrDefault().texto = valor;
                    }
                }


                if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para casos de docentes
        /// </summary>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public void ReemplazarEtiquetasDocente()
        {
            try
            {
                var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

                var plantilla = _repPlantilla.FirstById(this.IdPlantilla);
                this.IdPlantillaBase = plantilla.IdPlantillaBase;

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Codigo de plantilla no valido!");
                }

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Codigo de plantilla no valido!");
                }

                var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);
                var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                foreach (var etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                }

                //obtencion del provedor
                ProveedorBO proveedor;
                if (this.IdOportunidad != 0)
                {
                    var idClasificacionPersona = _repOportunidad.FirstBy(w => w.Id == this.IdOportunidad,
                        s => new { s.IdClasificacionPersona });
                    var idProveedor = _repClasificacionPersona.FirstBy(s =>
                            s.Id == idClasificacionPersona.IdClasificacionPersona &&
                            s.IdTipoPersona == (int)TipoPersona.Proveedor,
                        s => new { IdProveedor = s.IdTablaOriginal });
                    proveedor = _repProveedorRepositorio.FirstById(idProveedor.IdProveedor);
                }
                else
                {
                    proveedor = _repProveedorRepositorio.FirstById(this.IdProveedor);
                }

                //reemplazo
                if (plantillaBase.Cuerpo.Contains("{T_Proveedor.NombreContacto}"))
                {
                    var nombreTemporal = (string.IsNullOrEmpty(proveedor.Nombre1) ? "" : proveedor.Nombre1.Trim()) +
                                         " " + (string.IsNullOrEmpty(proveedor.ApePaterno)
                                             ? ""
                                             : proveedor.ApePaterno.Trim());
                    var valor = string.IsNullOrEmpty(nombreTemporal.Trim()) ? proveedor.RazonSocial : nombreTemporal;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Proveedor.NombreContacto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Proveedor.NombreContacto}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.NombreDocente}"))
                {
                    var nombreTemporal = "";
                    if (proveedor.Alias != null)
                    {
                        nombreTemporal = proveedor.Alias;
                    }
                    else
                    {
                        nombreTemporal = (string.IsNullOrEmpty(proveedor.Nombre1) ? "" : proveedor.Nombre1.Trim()) +
                                         " " + (string.IsNullOrEmpty(proveedor.ApePaterno)
                                             ? ""
                                             : proveedor.ApePaterno.Trim());
                    }

                    var valor = string.IsNullOrEmpty(nombreTemporal.Trim()) ? proveedor.RazonSocial : nombreTemporal;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.NombreDocente}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecificoSesion.NombreDocente}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProyectoAplicacion.ListadoAlumnosProyectoSinCalificarPorProveedor}"))
                {
                    PgeneralProyectoAplicacionEnvioRepositorio _repo = new PgeneralProyectoAplicacionEnvioRepositorio();
                    var listado = _repo.ListadoProyectoAplicacionAulaAnterior_SinCalificarPorProveedor(proveedor.Id);

                    var valor = @"  <table border=""1"" style=""padding: 0; margin: 0;"">
                                        <thead>
                                            <tr>
                                                <th>Código</th>
                                                <th>Alumno</th>
                                                <th>Programa/Curso</th>
                                            </tr>
                                        </thead>
                                        <tbody>";
                    foreach (var item in listado)
                    {
                        valor += $@"         <tr>
                                                <td>{item.CodigoMatricula}</td>
                                                <td>{item.Alumno.ToUpper()}</td>
                                                <td>{item.PEspecifico}</td>
                                            </tr>";
                    }
                    valor += @"         </tbody>
                                    </table>";
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProyectoAplicacion.ListadoAlumnosProyectoSinCalificarPorProveedor}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProyectoAplicacion.ListadoAlumnosProyectoSinCalificarPorProveedor}")).FirstOrDefault().texto = valor;
                    }
                }

                //Acceso PW
                var listaAccesoDocentePortalWeb = new List<string>()
                {
                    "{T_Docente.UsuarioPortalWeb}",
                    "{T_Docente.ClavePortalWeb}"
                };
                if (listaAccesoDocentePortalWeb.Any(plantillaBase.Cuerpo.Contains))
                {
                    var accesoPortalWeb = _repMatriculaCabecera.ObtenerDetalleAccesoDocentePortalWeb(IdProveedor);
                    if (plantillaBase.Cuerpo.Contains("{T_Docente.UsuarioPortalWeb}"))
                    {
                        var valor = accesoPortalWeb.Usuario;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Docente.UsuarioPortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Docente.UsuarioPortalWeb}"))
                                .FirstOrDefault().texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_Docente.ClavePortalWeb}"))
                    {
                        var valor = accesoPortalWeb.Clave;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Docente.ClavePortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Docente.ClavePortalWeb}"))
                                .FirstOrDefault().texto = valor;
                        }
                    }
                }

                //Etiquetas texto plano
                var listaTextoPlano = new List<string>()
                {
                    "{Link.UrlAulaVirtual}",
                    "{Link.UrlDescargarAplicativoAndroid}",
                    "{Link.UrlDescargarAplicationIOS}",
                    "{Link.UrlGuiaAccederSesionWebinarPorVideo}",
                    "{Link.UrlImagenFelizCumpleanios}",
                    "{Link.UrlManualBSPlay}",
                    "{Link.UrlPortalWeb}"
                };
                if (listaTextoPlano.Any(plantillaBase.Cuerpo.Contains))
                {
                    if (plantillaBase.Cuerpo.Contains("{Link.UrlPortalWeb}"))
                    {
                        var valor = ValorEstaticoUtil.Get("{Link.UrlPortalWeb}");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlPortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{Link.UrlPortalWeb}")).FirstOrDefault()
                                .texto = valor;
                        }
                    }
                }

                //asunto
                if (plantillaBase.Asunto.Contains("{T_Proveedor.NombreContacto}"))
                {
                    var nombreTemporal = (string.IsNullOrEmpty(proveedor.Nombre1) ? "" : proveedor.Nombre1.Trim()) +
                                         " " + (string.IsNullOrEmpty(proveedor.ApePaterno)
                                             ? ""
                                             : proveedor.ApePaterno.Trim());
                    var valor = string.IsNullOrEmpty(nombreTemporal.Trim()) ? proveedor.RazonSocial : nombreTemporal;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_Proveedor.NombreContacto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Proveedor.NombreContacto}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Asunto.Contains("{T_PEspecificoSesion.NombreDocente}"))
                {
                    var nombreTemporal = (string.IsNullOrEmpty(proveedor.Nombre1) ? "" : proveedor.Nombre1.Trim()) +
                                         " " + (string.IsNullOrEmpty(proveedor.ApePaterno)
                                             ? ""
                                             : proveedor.ApePaterno.Trim());
                    var valor = string.IsNullOrEmpty(nombreTemporal.Trim()) ? proveedor.RazonSocial : nombreTemporal;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_PEspecificoSesion.NombreDocente}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_PEspecificoSesion.NombreDocente}")).FirstOrDefault().texto = valor;
                    }
                }

                if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para casos de proceso de seleccion
        /// </summary>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public void ReemplazarEtiquetasProcesoSeleccion()
        {
            try
            {
                var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

                var plantilla = _repPlantilla.FirstById(this.IdPlantilla);

                this.IdPlantillaBase = plantilla.IdPlantillaBase;

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Plantilla no valida");
                }

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Plantilla no valida");
                }

                var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);
                var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();


                foreach (var etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                }

                //reemplazo
                //var detalleEnvioMaterialProveedorImpresion = _repMaterialPEspecificoDetalle.ObtenerDetalleMaterialPEspecificoEnviarProveedor(this.IdMaterialPEspecificoDetalle);
                var postulanteProcesoSeleccion = _repPostulante.ObtenerProcesoSeleccionInscrito(this.IdPostulanteProcesoSeleccion);
                var postulante = _repPostulante.FirstById(postulanteProcesoSeleccion.IdPostulante);

                if (plantillaBase.Cuerpo.Contains("{T_Postulante.Nombre1}") || plantillaBase.Cuerpo.Contains("{T_Postulante.Nombre}"))
                {
                    var valor = postulante.Nombre;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Postulante.Nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Postulante.Nombre1}")).FirstOrDefault().texto = valor;
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.NombrePuesto}") || plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.Nombre}"))
                {
                    var valor = postulanteProcesoSeleccion.PuestoTrabajo;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.NombrePuesto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.NombrePuesto}")).FirstOrDefault().texto = valor;
                    }
                }

                var token = _repPostulante.ObtenerPostulanteProcesoSeleccion(postulanteProcesoSeleccion.Id);
                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.LinkExamenPostulante}"))
                {
                    var valor = "https://bsginstitute.com/procesoseleccion/acceso?guid=" + token.GuidAccess;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.LinkExamenPostulante}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.LinkExamenPostulante}")).FirstOrDefault().texto = valor;
                    }
                }



                if (plantillaBase.Cuerpo.Contains("{T_Postulante.Usuario}"))
                {
                    var valor = postulante.NroDocumento;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Postulante.Usuario}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Postulante.Usuario}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_Postulante.Clave}"))
                {
                    var valor = token.Token;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Postulante.Clave}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Postulante.Clave}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.NombreReclutador}"))
                {
                    var valor = string.Concat(Personal.Nombres, " ", Personal.Apellidos);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.NombreReclutador}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.NombreReclutador}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.NumeroReclutador}"))
                {
                    var valor = Personal.MovilReferencia;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.NumeroReclutador}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.NumeroReclutador}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.Hora}"))
                {
                    if (!FechaGP.HasValue)
                    {
                        FechaGP = DateTime.Now;
                    }
                    var valor = FechaGP.Value.ToString("hh:mm tt");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.Hora}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.Hora}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.Fecha}"))
                {
                    if (!FechaGP.HasValue)
                    {
                        FechaGP = DateTime.Now;
                    }
                    var valor = FechaGP.Value.Date.ToString("dd-MM-yyyy");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.Fecha}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.Fecha}")).FirstOrDefault().texto = valor;
                    }
                }
                //asunto
                if (plantillaBase.Asunto.Contains("{T_Postulante.Nombre1}"))
                {
                    var valor = postulante.Nombre;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_Postulante.Nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Postulante.Nombre1}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Asunto.Contains("{T_ProcesoSeleccion.NombrePuesto}"))
                {
                    var valor = postulanteProcesoSeleccion.PuestoTrabajo;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_ProcesoSeleccion.NombrePuesto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.NombrePuesto}")).FirstOrDefault().texto = valor;
                    }
                }

                if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ReemplazarEtiquetasAlumnoSinOportunidad(EtiquetaParametroAlumnoSinOportunidadDTO parametrosEtiquetasOpcionales)
        {
            try
            {
                // Declaracion de parametros e inicializaciones
                var alumno = _repAlumno.FirstBy(x => x.Id == parametrosEtiquetasOpcionales.IdAlumno);

                if (alumno == null)
                    throw new Exception("Alumno no existente");

                PlantillaBO plantilla = _repPlantilla.FirstBy(x => x.Id == this.IdPlantilla);

                if (plantilla == null)
                    throw new Exception("Plantilla no existente");

                if (_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                    IdPlantillaBase = plantilla.IdPlantillaBase;
                else
                    throw new Exception("Plantilla base no existente");

                List<datoPlantillaWhatsApp> listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();
                PlantillaBaseCorreoOperacionesDTO plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);

                List<string> listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();
                foreach (string etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = string.Empty });
                }

                // Inicio logica intercambio
                #region DatosPGeneral
                if (parametrosEtiquetasOpcionales.IdPGeneral.HasValue)
                {
                    var pGeneral = _repPgeneral.FirstBy(x => x.Id == parametrosEtiquetasOpcionales.IdPGeneral);

                    if (pGeneral == null)
                    {
                        throw new Exception("El programa general no existe");
                    }

                    if (plantillaBase.Cuerpo.Contains("{tPgeneral.Nombre}"))
                    {
                        var valor = pGeneral.Nombre;

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPgeneral.Nombre}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPgeneral.Nombre}")).FirstOrDefault().texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                    {
                        var valor = pGeneral.Nombre;

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Nombre}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.Nombre}")).FirstOrDefault().texto = valor;
                        }
                    }
                }
                #endregion

                #region DatosAlumno
                if (plantillaBase.Cuerpo.Contains("{tAlumno.nombre1}"))
                {
                    var valorFormateado = alumno.Nombre1;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumno.nombre1}", valorFormateado);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumno.nombre1}")).FirstOrDefault().texto = valorFormateado;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.nombre1}"))
                {
                    var valorFormateado = alumno.Nombre1;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.nombre1}", valorFormateado);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valorFormateado;
                    }
                }

                var listaAccesoPortalWeb = new List<string>()
                {
                    "{T_Alumno.UsuarioPortalWeb}",
                    "{T_Alumno.ClavePortalWeb}"
                };

                if (listaAccesoPortalWeb.Any(plantillaBase.Cuerpo.Contains))
                {
                    var accesoPortalWeb = _repAlumno.ObtenerCredencialesPortalWebPorIdAlumno(alumno.Id);

                    if (plantillaBase.Cuerpo.Contains("{T_Alumno.UsuarioPortalWeb}"))
                    {
                        var valor = accesoPortalWeb.PortalWebUsuario;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.UsuarioPortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.UsuarioPortalWeb}")).FirstOrDefault().texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_Alumno.ClavePortalWeb}"))
                    {
                        var valor = accesoPortalWeb.PortalWebClave;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.ClavePortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.ClavePortalWeb}")).FirstOrDefault().texto = valor;
                        }
                    }
                }
                #endregion

                // Intercambio final de la plantilla a sus valores respectivos
                if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: ----------
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para casos de oportunidades sin matriculas
        /// </summary>
        /// <param name="personalPorDefecto">Flag para determinar si se usara el personal por defecto que se encuentra en la tabla conf.T_ConfiguracionFija</param>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public void ReemplazarEtiquetasNuevasOportunidades(bool personalPorDefecto = false, int idCentroCosto = 0)
        {
            try
            {
                if (!_repOportunidad.Exist(x => x.Id == this.IdOportunidad))
                    throw new Exception("Oportunidad no existente!");

                OportunidadBO oportunidad = _repOportunidad.FirstById(this.IdOportunidad);

                oportunidad.IdCentroCosto = idCentroCosto > 0 ? idCentroCosto : oportunidad.IdCentroCosto;

                if (!_repAlumno.Exist(oportunidad.IdAlumno))
                    throw new Exception("Alumno no existente");

                AlumnoBO alumno = _repAlumno.FirstById(oportunidad.IdAlumno);

                if (!_repPlantilla.Exist(IdPlantilla))
                    throw new Exception("Plantilla no existente");

                PlantillaBO plantilla = _repPlantilla.FirstById(this.IdPlantilla);

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                    throw new Exception("Plantilla base no existente");

                IdPlantillaBase = plantilla.IdPlantillaBase;

                PlantillaBaseCorreoOperacionesDTO plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);

                int idPersonalFinal = personalPorDefecto ? ValorEstatico.IdPersonalCorreoPorDefecto : oportunidad.IdPersonalAsignado;

                if (!_repPersonal.Exist(idPersonalFinal))
                    throw new Exception("Personal no asignado");

                PersonalBO personal = _repPersonal.FirstById(idPersonalFinal);

                if (!_repCentroCosto.Exist(oportunidad.IdCentroCosto.GetValueOrDefault()))
                    throw new Exception("Centro de costo no existente");

                CentroCostoBO centroCosto = _repCentroCosto.FirstById(oportunidad.IdCentroCosto.GetValueOrDefault());

                PespecificoBO pespecifico = _repPespecifico.FirstBy(x => x.IdCentroCosto == centroCosto.Id);

                if (pespecifico == null)
                    throw new Exception("Programa especifico no existente");

                PgeneralBO pgeneral = _repPgeneral.FirstBy(x => x.Id == pespecifico.IdProgramaGeneral);

                if (pgeneral == null)
                    throw new Exception("Programa general no existente");

                ListadoEtiquetaBO listadoEtiqueta = new ListadoEtiquetaBO(_integraDBContext);
                List<datoPlantillaWhatsApp> listaObjetoWhatsApp = new List<datoPlantillaWhatsApp>();
                PartnerPwBO partnerPw = _repPartnerPw.FirstBy(x => x.Id == pgeneral.IdPartner);

                List<string> listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                foreach (string etiqueta in listaEtiqueta)
                    listaObjetoWhatsApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = string.Empty });

                // Seccion Asunto
                // Nombre del programa general
                if (plantillaBase.Asunto.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    string valor = pgeneral.Nombre;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tPLA_PGeneral.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.Nombre}")).FirstOrDefault().texto = valor;
                    }
                }

                // Primer nombre del alumno
                if (plantillaBase.Cuerpo.Contains("{tAlumnos.nombre1}"))
                {
                    string valor = alumno.Nombre1;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                }

                // Nombres del personal
                if (plantillaBase.Cuerpo.Contains("{tPersonal.nombres}"))
                {
                    string valor = personal.Nombres;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.nombres}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPersonal.nombres}")).FirstOrDefault().texto = valor;
                    }
                }

                // Apellidos del personal
                if (plantillaBase.Cuerpo.Contains("{tPersonal.apellidos}"))
                {
                    string valor = personal.Apellidos;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.apellidos}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPersonal.apellidos}")).FirstOrDefault().texto = valor;
                    }
                }

                //Telefono del personal
                if (plantillaBase.Cuerpo.Contains("{tPersonal.Telefono}"))
                {
                    string valor = listadoEtiqueta.ObtenerTelefonoPersonal(personal.Central, personal.Anexo3Cx);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.Telefono}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPersonal.Telefono}")).FirstOrDefault().texto = valor;
                    }
                }

                //Firma del personal
                if (plantillaBase.Cuerpo.Contains("{tPersonal.UrlFirmaCorreos}"))
                {
                    string firma = listadoEtiqueta.EtiquetaUrlFirmaCorreo(personal.Email);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.UrlFirmaCorreos}", firma);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPersonal.UrlFirmaCorreos}")).FirstOrDefault().texto = firma;
                    }
                }


                /*Revisar versiones un programa*/
                // Monto Pago de versiones
                if (plantillaBase.Cuerpo.Contains("{TPW_MontoPago.Versiones}"))
                {
                    string valor;

                    valor = listadoEtiqueta.EtiquetaMontosPagoV2(oportunidad, pgeneral.Id);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{TPW_MontoPago.Versiones}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{TPW_MontoPago.Versiones}")).FirstOrDefault().texto = valor;
                    }
                }

                // Encabezado de correo del partner
                if (plantillaBase.Cuerpo.Contains("{TPW_Partner.EncabezadoCorreoPartner}"))
                {
                    string valor = "";

                    if (partnerPw != null)
                        valor = partnerPw.EncabezadoCorreoPartner;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{TPW_Partner.EncabezadoCorreoPartner}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{TPW_Partner.EncabezadoCorreoPartner}")).FirstOrDefault().texto = valor;
                    }
                }

                // Lista de programas segun Cursos Ti1
                if (plantillaBase.Cuerpo.Contains("{NoTabla.ListaProgramasCursosTi1}"))
                {
                    string valor = listadoEtiqueta.EtiquetaListaProgramasPorIdEtiqueta(this.IdOportunidad, ValorEstatico.IdListaCursoAreaEtiquetaTi1);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        if (valor == null)
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{NoTabla.ListaProgramasCursosTi1}", string.Empty);
                        else
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{NoTabla.ListaProgramasCursosTi1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{NoTabla.ListaProgramasCursosTi1}")).FirstOrDefault().texto = valor;
                    }
                }

                // Nombre del programa general
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    string valor = pgeneral.Nombre;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.Nombre}")).FirstOrDefault().texto = valor;
                    }
                }

                // Version del programa general
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.urlVersion}"))
                {
                    string valor = listadoEtiqueta.ObtenerUrlVersion(pgeneral.UrlVersion);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.urlVersion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.urlVersion}")).FirstOrDefault().texto = valor;
                    }
                }

                // Programas, cursos relacionados
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.CursosRelacionados}"))
                {
                    string valor = listadoEtiqueta.EtiquetaCursoRelacionado(centroCosto.Id);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.CursosRelacionados}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.CursosRelacionados}")).FirstOrDefault().texto = valor;
                    }
                }

                // Brochure del programa general
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.UrlBrochurePrograma}"))
                {
                    string valor = listadoEtiqueta.ObtenerUrlBrochurePrograma(pgeneral.UrlBrochurePrograma);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        if (valor == null)
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.UrlBrochurePrograma}", "");
                        else
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.UrlBrochurePrograma}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.UrlBrochurePrograma}")).FirstOrDefault().texto = valor;
                    }
                }

                // Expositores
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Expositores}"))
                {
                    string valor = listadoEtiqueta.EtiquetaExpositor(pgeneral.Id);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        if (valor == null)
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Expositores}", "");
                        else
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Expositores}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.Expositores}")).FirstOrDefault().texto = valor;
                    }
                }

                // Duracion y Horarios
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.DuracionAndHorarios}"))
                {
                    var valor = listadoEtiqueta.ObtenerDuracionAndHorario(pgeneral.Id);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        if (valor == null)
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.DuracionAndHorarios}", "");
                        else
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.DuracionAndHorarios}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPEspecifico.DuracionAndHorarios}")).FirstOrDefault().texto = valor;
                    }
                }

                // Ciudad del programa especifico
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.ciudad}"))
                {
                    var valor = pespecifico.Ciudad;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.ciudad}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPEspecifico.ciudad}")).FirstOrDefault().texto = valor;
                    }
                }

                // Fecha inicio del programa especifico
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.FechaInicioPrograma}"))
                {
                    //string valor = ListadoEtiqueta.ObtenerFechaInicioPrograma(pgeneral.Id, centroCosto.Id);
                    string valor = listadoEtiqueta.FechaInicioProgramaV2(pgeneral.Id);

                    valor = string.IsNullOrEmpty(valor) ? "Por definir" : valor;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.FechaInicioPrograma}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPEspecifico.FechaInicioPrograma}")).FirstOrDefault().texto = valor;
                    }
                }

                // Url Documento del programa especifico
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.UrlDocumentoCronograma}"))
                {
                    var valor = listadoEtiqueta.ObtenerUrlDocumentoCronograma(pespecifico.UrlDocumentoCronograma);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail || plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.UrlDocumentoCronograma}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhatsApp.Where(x => x.codigo.Equals("{tPEspecifico.UrlDocumentoCronograma}")).FirstOrDefault().texto = valor;
                    }
                }

                // Templates V2
                if (plantillaBase.Cuerpo.Contains("TemplateV2"))
                {
                    const string etiquetavacio = "<vacio></vacio>";

                    DocumentosBO documentos = new DocumentosBO(_integraDBContext);

                    var listaSecciones = documentos.ObtenerListaSeccionDocumentoProgramaGeneral(pgeneral.Id);
                    var listaSeccionesDocumentoV2 = _repDocumentoSeccionPw.ObtenerDatosComplementariosProgramaGeneralV2(pgeneral.Id);

                    foreach (var item in listaObjetoWhatsApp.Where(x => x.codigo.Contains("TemplateV2")))
                    {
                        string[] array = item.codigo.Replace("{", "").Replace("}", "").Split(".");
                        string valor = string.Empty;

                        string nombreSeccion = array[array.Length - 1];
                        bool conTitulo = nombreSeccion == "Estructura Curricular";
                        string descripcionAdicional = string.Concat("Descripci&#243;n ", nombreSeccion.Split(" ")[0]);

                        var seccion = listadoEtiqueta.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones.Where(x => x.Seccion == nombreSeccion).ToList(), conTitulo);

                        valor = seccion.Aggregate(valor, (current, item01) => current + item01.Contenido);

                        // Unir Descripcion adicional de etiquetas que tienen dicho contenido
                        if (listaSeccionesDocumentoV2.Exists(x => x.Titulo == descripcionAdicional))
                        {
                            string descripcion = listaSeccionesDocumentoV2.First(x => x.Titulo == descripcionAdicional).Contenido;

                            valor += descripcion != etiquetavacio ? descripcion.Replace(etiquetavacio, string.Empty) : string.Empty;
                        }

                        // Sacar etiquetas no agrupadas de V2
                        if (listaSeccionesDocumentoV2.Any())
                            valor += valor.Equals(string.Empty) ? listaSeccionesDocumentoV2.First(x => x.Titulo == nombreSeccion).Contenido : string.Empty;

                        // Obtener etiquetas de V1 si en caso no encuentra
                        if (valor.Equals(string.Empty))
                        {
                            nombreSeccion = nombreSeccion == "Certificacion" ? "Certificación" : nombreSeccion;
                            List<SeccionDocumentoDTO> seccionV1 = _repDocumentoSeccionPw.ObtenerSecciones(pgeneral.Id).Where(x => x.Titulo == nombreSeccion).ToList();

                            valor = seccionV1.Aggregate(valor, (current, item01) => current + item01.Contenido);
                        }

                        // Asignar valores
                        if (plantilla.IdPlantillaBase.Equals(ValorEstatico.IdPlantillaBaseEmail))
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace(item.codigo, valor ?? string.Empty);
                        else if (plantilla.IdPlantillaBase.Equals(ValorEstatico.IdPlantillaBaseWhatsAppFacebook))
                            listaObjetoWhatsApp.FirstOrDefault(x => x.codigo.Equals(item.codigo)).texto = valor;
                        else
                            listaObjetoWhatsApp.FirstOrDefault(x => x.codigo.Equals(item.codigo)).texto = valor.Replace("<p>", "<p id='estructura'>");
                    }
                }

                // Templates
                if (plantillaBase.Cuerpo.Contains("Template"))
                {
                    // Templates V1
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        foreach (var item in listaObjetoWhatsApp.Where(x => x.codigo.Contains("Template") && !x.codigo.Contains("V2")))
                        {
                            var array = item.codigo.Replace("{", "").Replace("}", "").Split(".");

                            var idPlantilla = array[3];
                            var idColumna = array[4];

                            var valor = _repPEspecifico.ObtenerContenidoTemplate(new Guid(idPlantilla), new Guid(idColumna), oportunidad.IdCentroCosto ?? default(int));

                            if (valor == null)
                                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace(item.codigo, "");
                            else
                                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace(item.codigo, valor.Valor);
                        }

                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        foreach (var item in listaObjetoWhatsApp.Where(x => x.codigo.Contains("Template")))
                        {
                            var array = item.codigo.Replace("{", "").Replace("}", "").Split(".");

                            var idPlantilla = array[3];
                            var idColumna = array[4];

                            var valor = _repPEspecifico.ObtenerContenidoTemplate(new Guid(idPlantilla), new Guid(idColumna), oportunidad.IdCentroCosto ?? default(int));
                            listaObjetoWhatsApp.Where(x => x.codigo.Equals(item.codigo)).FirstOrDefault().texto = valor.Valor;
                        }
                        //listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                    else
                    {
                        foreach (var item in listaObjetoWhatsApp.Where(x => x.codigo.Contains("Template")))
                        {
                            var array = item.codigo.Replace("{", "").Replace("}", "").Split(".");

                            var idPlantilla = array[3];
                            var idColumna = array[4];

                            var valor = _repPEspecifico.ObtenerContenidoTemplate(new Guid(idPlantilla), new Guid(idColumna), oportunidad.IdCentroCosto ?? default(int));
                            listaObjetoWhatsApp.Where(x => x.codigo.Equals(item.codigo)).FirstOrDefault().texto = valor.Valor.Replace("<p>", "<p id='estructura'>");
                        }
                    }
                }

                if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhatsApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
                else if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseMensajeTexto)
                {
                    SmsReemplazado.Cuerpo = plantillaBase.Cuerpo;
                }
                else if (this.IdPlantillaBase == 12 || this.IdPlantillaBase == 13)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhatsApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para casos sin oportunidad
        /// </summary>
        /// <param name="idPersonal">Id del Personal que envia el correo</param>
        /// <param name="idCentroCosto">Id del Centro de Costo enlazado al programa general</param>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public void ReemplazarEtiquetasSinOportunidad(int? idPersonal, int? idCentroCosto)
        {
            try
            {
                ListadoEtiquetaBO ListadoEtiqueta = new ListadoEtiquetaBO(_integraDBContext);
                PartnerPwRepositorio _repPartnerPw = new PartnerPwRepositorio(_integraDBContext);

                List<datoPlantillaWhatsApp> listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

                if (!_repPlantilla.Exist(IdPlantilla))
                    throw new Exception("Plantilla no existente");

                PlantillaBO plantilla = _repPlantilla.FirstById(this.IdPlantilla);

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                    throw new Exception("Plantilla base no existente");

                IdPlantillaBase = plantilla.IdPlantillaBase;

                PlantillaBaseCorreoOperacionesDTO plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);

                var pespecifico = _repPespecifico.FirstBy(x => x.IdCentroCosto == idCentroCosto);

                if (pespecifico == null)
                    throw new Exception("Programa especifico no existente");

                var pgeneral = _repPgeneral.FirstBy(x => x.Id == pespecifico.IdProgramaGeneral);
                var partnerPw = _repPartnerPw.FirstBy(x => x.Id == pgeneral.IdPartner);

                List<string> listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                foreach (string etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                }

                /*Lista de etiquetas para la generacion de plantillas genericas*/
                if (idPersonal.HasValue)
                {
                    var personal = _repPersonal.FirstById(idPersonal.Value);

                    /*Nombres del personal*/
                    if (plantillaBase.Cuerpo.Contains("{tPersonal.nombres}"))
                    {
                        var valor = personal.Nombres;

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.nombres}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.nombres}")).FirstOrDefault().texto = valor;
                        }
                    }

                    /*Apellidos del personal*/
                    if (plantillaBase.Cuerpo.Contains("{tPersonal.apellidos}"))
                    {
                        var valor = personal.Apellidos;

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.apellidos}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.apellidos}")).FirstOrDefault().texto = valor;
                        }
                    }

                    /*Telefono del personal*/
                    if (plantillaBase.Cuerpo.Contains("{tPersonal.Telefono}"))
                    {
                        var valor = personal.Telefono;

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.Telefono}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.Telefono}")).FirstOrDefault().texto = valor;
                        }
                    }


                    //Firma del personal
                    if (plantillaBase.Cuerpo.Contains("{tPersonal.UrlFirmaCorreos}"))
                    {
                        var firma = ListadoEtiqueta.EtiquetaUrlFirmaCorreo(personal.Email);

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.UrlFirmaCorreos}", firma);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.UrlFirmaCorreos}")).FirstOrDefault().texto = firma;
                        }
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.nombre1}"))
                {
                    var valor = "";
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                }

                /*Nombre del programa general*/
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    var valor = pgeneral.Nombre;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.Nombre}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.Nombre}")).FirstOrDefault().texto = valor;
                    }
                }

                // Version del programa general
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.urlVersion}"))
                {
                    string valor = ListadoEtiqueta.ObtenerUrlVersion(pgeneral.UrlVersion);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.urlVersion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.urlVersion}")).FirstOrDefault().texto = valor;
                    }
                }

                // Version del programa general
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.UrlBrochurePrograma}"))
                {
                    string valor = ListadoEtiqueta.ObtenerUrlBrochurePrograma(pgeneral.UrlBrochurePrograma);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.UrlBrochurePrograma}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.UrlBrochurePrograma}")).FirstOrDefault().texto = valor;
                    }
                }

                // Ciudad del programa especifico
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.ciudad}"))
                {
                    var valor = pespecifico.Ciudad;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.ciudad}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPEspecifico.ciudad}")).FirstOrDefault().texto = valor;
                    }
                }

                // Fecha inicio del programa especifico
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.FechaInicioPrograma}"))
                {
                    //string valor = ListadoEtiqueta.ObtenerFechaInicioPrograma(pgeneral.Id, IdCentroCosto.Value);
                    string valor = ListadoEtiqueta.FechaInicioProgramaV2(pgeneral.Id);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.FechaInicioPrograma}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPEspecifico.FechaInicioPrograma}")).FirstOrDefault().texto = valor;
                    }
                }

                // Encabezado de correo del partner
                if (plantillaBase.Cuerpo.Contains("{TPW_Partner.EncabezadoCorreoPartner}"))
                {
                    string valor = "";

                    if (partnerPw != null)
                        valor = partnerPw.EncabezadoCorreoPartner;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{TPW_Partner.EncabezadoCorreoPartner}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{TPW_Partner.EncabezadoCorreoPartner}")).FirstOrDefault().texto = valor;
                    }
                }

                // Duracion y Horarios
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.DuracionAndHorarios}"))
                {
                    var valor = ListadoEtiqueta.ObtenerDuracionAndHorario(pgeneral.Id);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        if (valor == null)
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.DuracionAndHorarios}", "");
                        else
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.DuracionAndHorarios}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPEspecifico.DuracionAndHorarios}")).FirstOrDefault().texto = valor;
                    }
                }

                // Lista de programas segun Cursos Ti1
                if (plantillaBase.Cuerpo.Contains("{NoTabla.ListaProgramasCursosTi1}"))
                {
                    string valor = ListadoEtiqueta.EtiquetaListaProgramasPorIdEtiqueta(this.IdOportunidad, ValorEstatico.IdListaCursoAreaEtiquetaTi1);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        if (valor == null)
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{NoTabla.ListaProgramasCursosTi1}", string.Empty);
                        else
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{NoTabla.ListaProgramasCursosTi1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{NoTabla.ListaProgramasCursosTi1}")).FirstOrDefault().texto = valor;
                    }
                }

                // Expositores
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Expositores}"))
                {
                    string valor = ListadoEtiqueta.EtiquetaExpositor(pgeneral.Id);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        if (valor == null)
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Expositores}", "");
                        else
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Expositores}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.Expositores}")).FirstOrDefault().texto = valor;
                    }
                }

                // Url Documento del programa especifico
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.UrlDocumentoCronograma}"))
                {
                    var valor = ListadoEtiqueta.ObtenerUrlDocumentoCronograma(pespecifico.UrlDocumentoCronograma);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPEspecifico.UrlDocumentoCronograma}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPEspecifico.UrlDocumentoCronograma}")).FirstOrDefault().texto = valor;
                    }
                }

                // Programas, cursos relacionados
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.CursosRelacionados}"))
                {
                    string valor = ListadoEtiqueta.EtiquetaCursoRelacionado(idCentroCosto.Value);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.CursosRelacionados}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.CursosRelacionados}")).FirstOrDefault().texto = valor;
                    }
                }

                // Templates V2
                if (plantillaBase.Cuerpo.Contains("TemplateV2"))
                {
                    const string etiquetavacio = "<vacio></vacio>";

                    DocumentosBO documentos = new DocumentosBO(_integraDBContext);

                    var listaSecciones = documentos.ObtenerListaSeccionDocumentoProgramaGeneral(pgeneral.Id);
                    var listaSeccionesDocumentoV2 = _repDocumentoSeccionPw.ObtenerDatosComplementariosProgramaGeneralV2(pgeneral.Id);

                    foreach (var item in listaObjetoWhasApp.Where(x => x.codigo.Contains("TemplateV2")))
                    {
                        string[] array = item.codigo.Replace("{", "").Replace("}", "").Split(".");
                        string valor = string.Empty;

                        string nombreSeccion = array[array.Length - 1];
                        bool conTitulo = nombreSeccion == "Estructura Curricular";
                        string descripcionAdicional = string.Concat("Descripci&#243;n ", nombreSeccion.Split(" ")[0]);

                        var seccion = ListadoEtiqueta.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones.Where(x => x.Seccion == nombreSeccion).ToList(), conTitulo);

                        valor = seccion.Aggregate(valor, (current, item01) => current + item01.Contenido);

                        // Unir Descripcion adicional de etiquetas que tienen dicho contenido
                        if (listaSeccionesDocumentoV2.Exists(x => x.Titulo == descripcionAdicional))
                        {
                            string descripcion = listaSeccionesDocumentoV2.First(x => x.Titulo == descripcionAdicional).Contenido;

                            valor += descripcion != etiquetavacio ? descripcion.Replace(etiquetavacio, string.Empty) : string.Empty;
                        }

                        // Sacar etiquetas no agrupadas de V2
                        if (listaSeccionesDocumentoV2.Any())
                        {
                            nombreSeccion = nombreSeccion == "Certificacion" ? descripcionAdicional : nombreSeccion;
                            valor += valor.Equals(string.Empty) ? listaSeccionesDocumentoV2.First(x => x.Titulo == nombreSeccion).Contenido : string.Empty;
                        }
                        // Obtener etiquetas de V1 si en caso no encuentra
                        if (valor.Equals(string.Empty))
                        {
                            nombreSeccion = nombreSeccion == "Certificacion" ? "Certificación" : nombreSeccion;
                            List<SeccionDocumentoDTO> seccionV1 = _repDocumentoSeccionPw.ObtenerSecciones(pgeneral.Id).Where(x => x.Titulo == nombreSeccion).ToList();

                            valor = seccionV1.Aggregate(valor, (current, item01) => current + item01.Contenido);
                        }

                        // Asignar valores
                        if (plantilla.IdPlantillaBase.Equals(ValorEstatico.IdPlantillaBaseEmail))
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace(item.codigo, valor ?? string.Empty);
                        else if (plantilla.IdPlantillaBase.Equals(ValorEstatico.IdPlantillaBaseWhatsAppFacebook))
                            listaObjetoWhasApp.FirstOrDefault(x => x.codigo.Equals(item.codigo)).texto = valor;
                        else
                            listaObjetoWhasApp.FirstOrDefault(x => x.codigo.Equals(item.codigo)).texto = valor.Replace("<p>", "<p id='estructura'>");
                    }
                }

                // Templates
                if (plantillaBase.Cuerpo.Contains("Template"))
                {
                    // Templates V1
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        foreach (var item in listaObjetoWhasApp.Where(x => x.codigo.Contains("Template") && !x.codigo.Contains("V2")))
                        {
                            var array = item.codigo.Replace("{", "").Replace("}", "").Split(".");

                            var IdPlantilla = array[3];
                            var IdColumna = array[4];

                            var valor = _repPEspecifico.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna), idCentroCosto ?? default(int));

                            if (valor == null)
                                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace(item.codigo, "");
                            else
                                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace(item.codigo, valor.Valor);
                        }

                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        foreach (var item in listaObjetoWhasApp.Where(x => x.codigo.Contains("Template")))
                        {
                            var array = item.codigo.Replace("{", "").Replace("}", "").Split(".");

                            var IdPlantilla = array[3];
                            var IdColumna = array[4];

                            var valor = _repPEspecifico.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna), idCentroCosto ?? default(int));
                            listaObjetoWhasApp.Where(x => x.codigo.Equals(item.codigo)).FirstOrDefault().texto = valor.Valor;
                        }
                        //listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                    else
                    {
                        foreach (var item in listaObjetoWhasApp.Where(x => x.codigo.Contains("Template")))
                        {
                            var array = item.codigo.Replace("{", "").Replace("}", "").Split(".");

                            var IdPlantilla = array[3];
                            var IdColumna = array[4];

                            var valor = _repPEspecifico.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna), idCentroCosto ?? default(int));
                            listaObjetoWhasApp.Where(x => x.codigo.Equals(item.codigo)).FirstOrDefault().texto = valor.Valor.Replace("<p>", "<p id='estructura'>");
                        }
                    }
                }

                if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
                else if (this.IdPlantillaBase == 12 || this.IdPlantillaBase == 13)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Edgar S .
        /// Fecha: 22/06/2021
        /// Version: 1.0
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para casos de accesos temporales de Postulantes
        /// </summary>
        /// <param name="informacionAccesoTemporal"> Información de Acceso Temporal </param>
        /// <param name="idPespecifico">Id de Programa Específico</param>
        /// <param name="fechaInicio">Fecha Inicio de Acceso</param>
        /// <param name="fechaFin">Fecha Fin de Acceso</param>
        /// <param name="personalEmail">Emai lde Personal</param>
        /// <returns> Vacio, asigna a las propiedades locales los resultados </returns>
        public void ReemplazarEtiquetasAccesosTemporalesPostulante(InformacionAccesoPostulanteDTO informacionAccesoTemporal, int idPespecifico, DateTime fechaInicio, DateTime fechaFin, string personalEmail)
        {
            try
            {
                ListadoEtiquetaBO listadoEtiqueta = new ListadoEtiquetaBO(_integraDBContext);
                PartnerPwRepositorio _repPartnerPw = new PartnerPwRepositorio(_integraDBContext);
                List<datoPlantillaWhatsApp> listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();
                if (!_repPlantilla.Exist(IdPlantilla))
                    throw new Exception("Plantilla no existente");

                PlantillaBO plantilla = _repPlantilla.FirstById(IdPlantilla);
                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                    throw new Exception("Plantilla base no existente");
                IdPlantillaBase = plantilla.IdPlantillaBase;

                var alumno = _repAlumno.FirstById(informacionAccesoTemporal.IdAlumno.GetValueOrDefault());
                PlantillaBaseCorreoOperacionesDTO plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);
                List<string> listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();
                /*Lista de etiquetas para la generacion de plantillas genericas*/
                if (informacionAccesoTemporal.IdAlumno.GetValueOrDefault() > 0)
                {
                    if (plantillaBase.Asunto.Contains("{tAlumno.nombre1}"))
                    {
                        var valorFormateado = alumno.Nombre1;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tAlumno.nombre1}", valorFormateado);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumno.nombre1}")).FirstOrDefault().texto = valorFormateado;
                        }
                    }
                    /*Curso Habilitado*/
                    if (plantillaBase.Cuerpo.Contains("{tPespecifico.Nombre}"))
                    {
                        var valor = _repPespecifico.GetBy(x => x.Id == idPespecifico).Select(x => x.Nombre).FirstOrDefault();

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPespecifico.Nombre}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPespecifico.Nombre}")).FirstOrDefault().texto = valor;
                        }
                    }
                    /*Correo Usuario Acceso Portal*/
                    if (plantillaBase.Cuerpo.Contains("{tAspNetUser.Usuario}"))
                    {
                        var valor = informacionAccesoTemporal.Usuario;

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAspNetUser.Usuario}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAspNetUser.Usuario}")).FirstOrDefault().texto = valor;
                        }
                    }
                    /*Correo Clave Acceso Portal*/
                    if (plantillaBase.Cuerpo.Contains("{tAspNetUser.Clave}"))
                    {
                        var valor = informacionAccesoTemporal.Clave;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAspNetUser.Clave}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAspNetUser.Clave}")).FirstOrDefault().texto = valor;
                        }
                    }
                    /*Fecha Inicio de Acceso*/
                    if (plantillaBase.Cuerpo.Contains("{tPostulanteAccesoTemporalAulaVirtual.FechaInicio}"))
                    {
                        var valor = fechaInicio.ToString("MM/dd/yyyy");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPostulanteAccesoTemporalAulaVirtual.FechaInicio}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPostulanteAccesoTemporalAulaVirtual.FechaInicio}")).FirstOrDefault().texto = valor;
                        }
                    }
                    /*Fecha Fin de Acceso*/
                    if (plantillaBase.Cuerpo.Contains("{tPostulanteAccesoTemporalAulaVirtual.FechaFin}"))
                    {
                        var valor = fechaFin.ToString("MM/dd/yyyy");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPostulanteAccesoTemporalAulaVirtual.FechaFin}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPostulanteAccesoTemporalAulaVirtual.FechaFin}")).FirstOrDefault().texto = valor;
                        }
                    }
                    //Firma del personal
                    if (plantillaBase.Cuerpo.Contains("{tPersonal.UrlFirmaCorreos}"))
                    {
                        var firma = listadoEtiqueta.EtiquetaUrlFirmaCorreo(personalEmail);
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.UrlFirmaCorreos}", firma);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.UrlFirmaCorreos}")).FirstOrDefault().texto = firma;
                        }
                    }
                }
                if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
                else if (this.IdPlantillaBase == 12 || this.IdPlantillaBase == 13)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 25/06/2021
        /// Version: 1.0
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para los correos que se envian cuando se solicitan certificados fisico
        /// </summary>
        /// <param name="datosAlumno">Tipo de dato DatosRegistroEnvioFisico: tiene los datos necesarios para el reemplazo de plantilla </param>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>

        public void ReemplazarEtiquetasEnvioCorreoSolicitudEnvioFiscio(DatosRegistroEnvioFisico datosAlumno)
        {
            try
            {
                ListadoEtiquetaBO ListadoEtiqueta = new ListadoEtiquetaBO(_integraDBContext);
                PartnerPwRepositorio _repPartnerPw = new PartnerPwRepositorio(_integraDBContext);

                List<datoPlantillaWhatsApp> listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();
                var alumno = _repAlumno.FirstById(datosAlumno.IdAlumno);

                var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.Id == datosAlumno.IdMatriculaCabecera);
                var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();

                var personal = _repPersonal.FirstById((int)datosAlumno.IdPersonal);

                //if (!_repPlantilla.Exist(IdPlantilla))
                //    throw new Exception("Plantilla no existente");

                PlantillaBO plantilla = _repPlantilla.FirstById(IdPlantilla);

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                    throw new Exception("Plantilla base no existente");

                IdPlantillaBase = plantilla.IdPlantillaBase;

                PlantillaBaseCorreoOperacionesDTO plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);

                /*Lista de etiquetas para la generacion de plantillas genericas*/
                if (plantillaBase.Cuerpo.Contains("{tAlumno.Pais}"))
                {
                    var valor = datosAlumno.Pais;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumno.Pais}", valor);
                    }

                }

                if (plantillaBase.Cuerpo.Contains("{tAlumno.Region}"))
                {
                    var valor = datosAlumno.Region;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumno.Region}", valor);
                    }
                }
                /*Nombre del programa general*/
                if (plantillaBase.Cuerpo.Contains("{tAlumno.Ciudad}"))
                {
                    var valor = datosAlumno.Ciudad;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumno.Ciudad}", valor);
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{tAlumno.Codigo}"))
                {
                    string valor = datosAlumno.CodigoPostal;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumno.Codigo}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumno.Direccion}"))
                {
                    string valor = datosAlumno.Direccion;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumno.Direccion}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumno.Referencia}"))
                {
                    string valor = datosAlumno.Referencia;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumno.Referencia}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumno.Nombres}"))
                {
                    string valor = datosAlumno.Nombre;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumno.Nombres}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumno.Nombres}")).FirstOrDefault().texto = valor;
                    }
                }
                
                if (plantillaBase.Cuerpo.Contains("{T_SolicitudCertificadoFisico.Courier}"))
                {
                    string valor = datosAlumno.Courier;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_SolicitudCertificadoFisico.Courier}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_SolicitudCertificadoFisico.Courier}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{T_SolicitudCertificadoFisico.CodigoSeguimiento}"))
                {
                    string valor = datosAlumno.CodigoSeguimiento;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_SolicitudCertificadoFisico.CodigoSeguimiento}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_SolicitudCertificadoFisico.CodigoSeguimiento}")).FirstOrDefault().texto = valor;
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{tAlumnos.nombre1}"))
                {
                    var valor = alumno.Nombre1;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.apepaterno}"))
                {
                    var valor = alumno.ApellidoPaterno;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.apepaterno}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.apepaterno}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.apepaterno}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumno.Apellido}"))
                {
                    string valor = datosAlumno.Apellido;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumno.Apellido}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumno.Apellido}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumno.DNI}"))
                {
                    string valor = datosAlumno.DNI;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumno.DNI}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumno.DNI}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{tAlumno.Celular}"))
                {
                    string valor = datosAlumno.Telefono;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumno.Celular}", valor);
                    }
                }
                //nombre programa general
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    var valor = detalleMatriculaCabecera.NombreProgramaGeneral;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.Nombre}")).FirstOrDefault().texto = valor;
                    }
                    else if (plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.Nombre}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Asunto.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tPLA_PGeneral.Nombre}", detalleMatriculaCabecera.NombreProgramaGeneral);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        //listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{tPLA_PGeneral.Nombre}", texto = detalleMatriculaCabecera.NombreProgramaGeneral });
                    }
                }
                if (plantillaBase.Asunto.Contains("{tAlumno.Nombres}"))
                {
                    string valor = datosAlumno.Nombre;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tAlumno.Nombres}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumno.DNI}")).FirstOrDefault().texto = valor;
                    }
                }

                //reemplazar nombre 1 alumno
                if (plantillaBase.Asunto.Contains("{tAlumnos.nombre1}"))
                {
                    var valor = alumno.Nombre1;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tAlumnos.nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        //listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{T_Personal.FirmaCorreoImagen}"))
                {
                    var valor = personal.ObtenerFirmaCorreoImagen(alumno.IdCodigoPais, alumno.IdCiudad);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.FirmaCorreoImagen}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Personal.FirmaCorreoImagen}")).FirstOrDefault().texto = valor;
                    }
                }        
                if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
                else if (this.IdPlantillaBase == 12 || this.IdPlantillaBase == 13)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Edgar Serruto
        /// Fecha: 26/06/2021
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para notificacion de actualización de foro de programa general
        /// </summary>
        /// <param name="listaModalidadesLetra">Lista de strings de modalidades</param>
        /// <param name="nombrePgeneral">Nombre de Programa General </param>
        /// <param name="nombreProveedor">Nombre de Proveedor</param>
        /// <param name="urlFirmaPersonal">Url de Perfil de Personal</param>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public void ReemplazarEtiquetasNotificarProveedorAsignarForo(string nombrePgeneral, List<string> listaModalidadesLetra, string nombreProveedor, string urlFirmaPersonal)
        {
            try
            {
                var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

                var plantilla = _repPlantilla.FirstById(this.IdPlantilla);

                this.IdPlantillaBase = plantilla.IdPlantillaBase;

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Codigo de plantilla no valido!");
                }

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Codigo de plantilla no valido!");
                }
                var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);
                var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                foreach (var etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                }
                /* Información Adicionada para Reemplazo */
                var listaHtml = "<ul>";
                foreach (var modalidad in listaModalidadesLetra)
                {
                    listaHtml = listaHtml + "<li>" + modalidad + "</li>";
                }
                listaHtml = listaHtml + "</ul>";

                //reemplazo
                if (plantillaBase.Cuerpo.Contains("{tProveedor.Nombre}"))
                {
                    var valor = nombreProveedor;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tProveedor.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tProveedor.Nombre}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{tPgeneral.Nombre}"))
                {
                    var valor = nombrePgeneral;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPgeneral.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPgeneral.Nombre}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{tListaModalidad.Curso}"))
                {
                    var valor = listaHtml;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tListaModalidad.Curso}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tListaModalidad.Curso}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{tPersonal.UrlFirma}"))
                {
                    var valor = urlFirmaPersonal;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.UrlFirma}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.UrlFirma}")).FirstOrDefault().texto = valor;
                    }
                }
                if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 25/06/2021
        /// Version: 1.0
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para los correos que se envian cuando se solicitan certificados fisico
        /// </summary>
        /// <param name="datosAlumno">Tipo de dato DatosRegistroEnvioFisico: tiene los datos necesarios para el reemplazo de plantilla </param>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>

        public void ReemplazarEtiquetasEnvioCorreoAsignacionTareaDocente(TrabajoDeParesCorreoDetalleDTO datosAlumno)
        {
            try
            {
                ListadoEtiquetaBO ListadoEtiqueta = new ListadoEtiquetaBO(_integraDBContext);
                PartnerPwRepositorio _repPartnerPw = new PartnerPwRepositorio(_integraDBContext);

                List<datoPlantillaWhatsApp> listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

                //if (!_repPlantilla.Exist(IdPlantilla))
                //    throw new Exception("Plantilla no existente");

                PlantillaBO plantilla = _repPlantilla.FirstById(IdPlantilla);

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                    throw new Exception("Plantilla base no existente");

                IdPlantillaBase = plantilla.IdPlantillaBase;

                PlantillaBaseCorreoOperacionesDTO plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);

                /*Lista de etiquetas para la generacion de plantillas genericas*/
                if (plantillaBase.Cuerpo.Contains("{docente.nombre}"))
                {
                    var valor = datosAlumno.NombreProveedor;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{docente.nombre}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{programa.nombre}"))
                {
                    var valor = datosAlumno.NombrePrograma;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{programa.nombre}", valor);
                    }
                }
                /*Nombre del programa general*/
                if (plantillaBase.Cuerpo.Contains("{alumno.nombre}"))
                {
                    var valor = datosAlumno.NombreAlumno;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{alumno.nombre}", valor);
                    }
                }

                //Firma del personal
                if (plantillaBase.Cuerpo.Contains("{tPersonal.UrlFirmaCorreos}"))
                {
                    string firma = ListadoEtiqueta.EtiquetaUrlFirmaCorreo(datosAlumno.Email);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.UrlFirmaCorreos}", firma);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.UrlFirmaCorreos}")).FirstOrDefault().texto = firma;
                    }
                }
                if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
                else if (this.IdPlantillaBase == 12 || this.IdPlantillaBase == 13)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 28/10/2021
        /// Version: 1.0
        /// <summary>
        /// Reemplaza el mensaje segun etiquetas genericas
        /// </summary>
        /// <param name="mensaje">Mensaje que se reemplazara</param>
        /// <returns>Mensaje ya reemplazado</returns>
        public string ReemplazarEtiquetasMensajeGenerico(string mensaje)
        {
            try
            {
                var listadoEtiqueta = new ListadoEtiquetaBO(_integraDBContext);

                if (mensaje.Contains("{RedSocial_Facebook}"))
                    mensaje = mensaje.Replace("{RedSocial_Facebook}", listadoEtiqueta.ObtenerIconoFacebookUrlRedireccionadoSegunPais(null));

                if (mensaje.Contains("{RedSocial_FacebookPeru}"))
                    mensaje = mensaje.Replace("{RedSocial_FacebookPeru}", listadoEtiqueta.ObtenerIconoFacebookUrlRedireccionadoSegunPais(ValorEstatico.IdPaisPeru));

                if (mensaje.Contains("{RedSocial_FacebookColombia}"))
                    mensaje = mensaje.Replace("{RedSocial_FacebookColombia}", listadoEtiqueta.ObtenerIconoFacebookUrlRedireccionadoSegunPais(ValorEstatico.IdPaisColombia));

                if (mensaje.Contains("{RedSocial_FacebookBolivia}"))
                    mensaje = mensaje.Replace("{RedSocial_FacebookBolivia}", listadoEtiqueta.ObtenerIconoFacebookUrlRedireccionadoSegunPais(ValorEstatico.IdPaisBolivia));

                if (mensaje.Contains("{RedSocial_FacebookCarreraProfesional}"))
                    mensaje = mensaje.Replace("{RedSocial_FacebookCarreraProfesional}", listadoEtiqueta.ObtenerIconoFacebookUrlRedireccionadoSegunPais(-1));

                if (mensaje.Contains("{BSGInstitute_PaginaWeb}"))
                    mensaje = mensaje.Replace("{BSGInstitute_PaginaWeb}", listadoEtiqueta.ObtenerIconoUrlEstandar());

                if (mensaje.Contains("{BSGInstitute_Instagram}"))
                    mensaje = mensaje.Replace("{BSGInstitute_Instagram}", listadoEtiqueta.ObtenerIconoInstagramUrlRedireccionado());

                return mensaje;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edgar Serruto
        /// Fecha: 26/06/2021
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para notificacion de actualización de foro de programa general
        /// </summary>
        /// <param name="emailPersonal">Email de Personal</param>
        /// <param name="idPersonal">Id de Personal</param>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public void ReemplazarSpeechChatSoporte(string emailPersonal, int idPersonal)
        {
            try
            {
                var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

                var plantilla = _repPlantilla.FirstById(this.IdPlantilla);

                this.IdPlantillaBase = plantilla.IdPlantillaBase;

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Codigo de plantilla no valido!");
                }

                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Codigo de plantilla no valido!");
                }
                var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);
                var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                foreach (var etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                }
                //Valores Personal
                var personal = _repPersonal.FirstById(idPersonal);
                //reemplazo
                if (plantillaBase.Cuerpo.Contains("{T_Personal.Nombres}"))
                {
                    var valor = personal.Nombres;
                    plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.Nombres}", valor);
                }
                if (plantillaBase.Cuerpo.Contains("{T_Personal.Email}"))
                {
                    var valor = personal.Email;
                    plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.Email}", valor);
                }

                var textoPlano = ConvertirHtmlEnTextoPlano(plantillaBase.Cuerpo);
                EmailReemplazado.CuerpoHTML = textoPlano;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Edgar Serruto
        /// Fecha: 15/07/2021
        /// <summary>
        /// Cambia HTML a texto plano
        /// </summary>
        /// <param name="html">Cadena en formato html</param>
        /// <returns>String cadena en formato plano</returns>
        private static string ConvertirHtmlEnTextoPlano(string html)
        {
            const string espacioBlanco = @"(>|$)(\W|\n|\r)+<";
            const string darFormato = @"<[^>]*(>|$)";
            const string darFormatoLinea = @"<(br|BR)\s{0,1}\/{0,1}>";
            var reemplazoLinea = new Regex(darFormatoLinea, RegexOptions.Multiline);
            var reemplazoFormatoPlano = new Regex(darFormato, RegexOptions.Multiline);
            var reemplazoFormatoBlanco = new Regex(espacioBlanco, RegexOptions.Multiline);
            var texto = html;
            texto = System.Net.WebUtility.HtmlDecode(texto);
            texto = reemplazoFormatoBlanco.Replace(texto, "><");
            texto = reemplazoLinea.Replace(texto, Environment.NewLine);
            texto = reemplazoFormatoPlano.Replace(texto, string.Empty);
            return texto;
        }
    }
}

