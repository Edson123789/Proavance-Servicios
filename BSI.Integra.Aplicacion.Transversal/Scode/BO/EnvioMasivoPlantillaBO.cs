using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using Mandrill.Models;
using BSI.Integra.Aplicacion.Base.BO;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Scode.Helper;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Operaciones/EnvioMasivoPlantilla
    /// Autor: Wilber Choque - Gian Miranda
    /// Fecha: 08/02/2021
    /// <summary>
    /// BO para la logica del envio masivo de plantillas
    /// </summary>
    public class EnvioMasivoPlantillaBO
    {
        /// Propiedades	                                Significado
        /// -----------	                                ------------
        /// _integraDBContext                           Objeto del tipo integraDBContext(Contexto generado para Entity)
        /// _repTokenUsuario                            Repositorio de la tabla mkt.T_WhatsAppUsuarioCredencial
        /// _repWhatsAppConfiguracionLogEjecucion       Repositorio de la tabla mkt.T_WhatsAppConfiguracionLogEjecucion
        /// _repAlumno                                  Repositorio de la tabla mkt.T_Alumno
        /// _repPersonal                                Repositorio de la tabla gp.T_Personal
        /// _repPlantillaClaveValor                     Repositorio de la tabla mkt.T_PlantillaClaveValor
        /// _repCredenciales                            Repositorio de la tabla mkt.T_WhatsAppConfiguracion
        /// _repPlantilla                               Repositorio de la tabla mkt.T_Plantilla
        /// _repCentroCosto                             Repositorio de la tabla pla.T_CentroCosto
        /// _repPespecifico                             Repositorio de la tabla pla.T_PEspecifico
        /// _repPgeneral                                Repositorio de la tabla pla.T_PGeneral
        /// _repConfiguracionEnvioMailing               Repositorio de la tabla mkt.T_ConfiguracionEnvioMailing
        /// _repConfiguracionEnvioMailingDetalle        Repositorio de la tabla mkt.T_ConfiguracionEnvioMailingDetalle
        /// _repOportunidad                             Repositorio de la tabla com.T_Oportunidad
        /// _repConjuntoListaDetalle                    Repositorio de la tabla mkt.T_ConjuntoListaDetalle
        /// _repConjuntoListaResultado                  Repositorio de la tabla mkt.T_ConjuntoListaResultado
        /// _repOportunidadClasificacionOperaciones     Repositorio de la tabla ope.T_OportunidadClasificacionOperaciones
        /// _repPEspecifico                             Repositorio de la tabla pla.T_PEspecifico
        /// _repPEspecificoSesion                       Repositorio de la tabla pla.T_PEspecificoSesion
        /// _repPlantillaBase                           Repositorio de la tabla pla.T_PlantillaBase
        /// _repCronogramaPagoDetalleFinal              Repositorio de la tabla fin.T_CronogramaPagoDetalleFinal
        /// _repMatriculaCabecera                       Repositorio de la tabla fin.T_MatriculaCabecera
        /// _repMandrilEnvioCorreo                      Repositorio de la tabla mkt.T_MandrilEnvioCorreo

        private readonly integraDBContext _integraDBContext;
        private readonly WhatsAppUsuarioCredencialRepositorio _repTokenUsuario;
        private readonly WhatsAppConfiguracionLogEjecucionRepositorio _repWhatsAppConfiguracionLogEjecucion;
        private readonly AlumnoRepositorio _repAlumno;
        private readonly PersonalRepositorio _repPersonal;
        private readonly PlantillaClaveValorRepositorio _repPlantillaClaveValor;
        private readonly WhatsAppConfiguracionRepositorio _repCredenciales;
        private readonly PlantillaRepositorio _repPlantilla;
        private readonly CentroCostoRepositorio _repCentroCosto;
        private readonly PespecificoRepositorio _repPespecifico;
        private readonly PgeneralRepositorio _repPgeneral;
        private readonly ConfiguracionEnvioMailingRepositorio _repConfiguracionEnvioMailing;
        private readonly ConfiguracionEnvioMailingDetalleRepositorio _repConfiguracionEnvioMailingDetalle;
        private readonly OportunidadRepositorio _repOportunidad;
        private readonly ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle;
        private readonly ConjuntoListaResultadoRepositorio _repConjuntoListaResultado;
        private readonly OportunidadClasificacionOperacionesRepositorio _repOportunidadClasificacionOperaciones;
        private readonly PespecificoRepositorio _repPEspecifico;
        private readonly PespecificoSesionRepositorio _repPEspecificoSesion;
        private readonly PlantillaBaseRepositorio _repPlantillaBase;
        private readonly CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal;
        private readonly MatriculaCabeceraRepositorio _repMatriculaCabecera;
        private readonly MandrilEnvioCorreoRepositorio _repMandrilEnvioCorreo;

        public EnvioMasivoPlantillaBO()
        {
        }

        public EnvioMasivoPlantillaBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _integraDBContext.ChangeTracker.AutoDetectChangesEnabled = false;
            _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(integraDBContext);
            _repConjuntoListaResultado = new ConjuntoListaResultadoRepositorio(integraDBContext);
            _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(integraDBContext);
            _repConfiguracionEnvioMailingDetalle = new ConfiguracionEnvioMailingDetalleRepositorio(integraDBContext);

            _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);
            _repWhatsAppConfiguracionLogEjecucion = new WhatsAppConfiguracionLogEjecucionRepositorio(_integraDBContext);

            _repPlantillaClaveValor = new PlantillaClaveValorRepositorio(_integraDBContext);
            _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
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
            _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDBContext);
            _repOportunidadClasificacionOperaciones = new OportunidadClasificacionOperacionesRepositorio(_integraDBContext);
            _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            _repMandrilEnvioCorreo = new MandrilEnvioCorreoRepositorio(_integraDBContext);

        }

        /// <summary>
        /// En base a una configuracion envio mailing detalle enviamos el correo dinamico
        /// </summary>
        /// <param name="IdConjuntoListaResultado"></param>
        /// <returns></returns>
        public List<MandrilEnvioCorreoBO> EnvioAutomaticoPorConfiguracionEnvioMailingDetalleDinamico(int idConfiguracionEnvioMailingDetalle)
        {
            /*Una sola vez RESULTADO*/
            /*Antes de envio de correo*/
            /*Envio correo - 20 correo*/
            try
            {
                if (!_repConfiguracionEnvioMailingDetalle.Exist(idConfiguracionEnvioMailingDetalle))
                {
                    throw new Exception("No existe la configuracion envio mailing detalle");
                }
                var configuracionEnvioMailingDetalle = _repConfiguracionEnvioMailingDetalle.FirstById(idConfiguracionEnvioMailingDetalle);

                // Envio correo
                var conjuntoListaResultado = _repConjuntoListaResultado.FirstById(configuracionEnvioMailingDetalle.IdConjuntoListaResultado);

                // var oportunidad = _repOportunidad.FirstById(conjuntoListaResultado.IdOportunidad.Value);
                var oportunidad = _repOportunidad.FirstById(configuracionEnvioMailingDetalle.IdOportunidad.Value);
                var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);
                var alumno = _repAlumno.FirstById(oportunidad.IdAlumno);

                List<string> correosPersonalizados = new List<string>
                {
                    alumno.Email1
                };

                var archivosAdjuntos = this.ObtenerArchivosAdjuntos(configuracionEnvioMailingDetalle.CuerpoHtml);
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = personal.Email,
                    //Sender = "w.choque.itusaca@isur.edu.pe",
                    Recipient = string.Join(",", correosPersonalizados.Distinct()),
                    Subject = configuracionEnvioMailingDetalle.Asunto,
                    Message = this.QuitarEtiquetasArchivosAdjuntos(configuracionEnvioMailingDetalle.CuerpoHtml),
                    Cc = "",
                    Bcc = "gmiranda@bsginstitute.com",
                    AttachedFiles = archivosAdjuntos
                };
                var mailServie = new TMK_MailServiceImpl();

                mailServie.SetData(mailDataPersonalizado);
                var listaIdsMailChimp = mailServie.SendMessageTask();

                List<MandrilEnvioCorreoBO> listaMandrilEnvioCorreoBO = new List<MandrilEnvioCorreoBO>();

                foreach (var mensaje in listaIdsMailChimp)
                {
                    var mandrilEnvioCorreoBO = new MandrilEnvioCorreoBO
                    {
                        IdOportunidad = oportunidad.Id,
                        IdPersonal = oportunidad.IdPersonalAsignado,
                        IdAlumno = oportunidad.IdAlumno,
                        IdCentroCosto = oportunidad.IdCentroCosto,
                        IdMandrilTipoAsignacion = 7, //Envio masivo automatico nuevas oportunidades
                        EstadoEnvio = 1,
                        IdMandrilTipoEnvio = 2, //Manual = 1
                        FechaEnvio = DateTime.Now,
                        Asunto = configuracionEnvioMailingDetalle.Asunto,
                        FkMandril = mensaje.MensajeId,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "EnvioAutomatico",
                        UsuarioModificacion = "EnvioAutomatico",
                        EsEnvioMasivo = true
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
                return listaMandrilEnvioCorreoBO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// En base a una configuracion envio mailing detalle enviamos el correo
        /// </summary>
        /// <param name="idConfiguracionEnvioMailingDetalle">Id de Configuracion Envio Mailing Detalle (PK de la tabla mkt.T_ConfiguracionEnvioMailingDetalle)</param>
        /// <returns>Lista de objeto del tipo MandrilEnvioCorreoBO</returns>
        public List<MandrilEnvioCorreoBO> EnvioAutomaticoPorConfiguracionEnvioMailingDetalle(int idConfiguracionEnvioMailingDetalle)
        {
        /* Una sola vez RESULTADO*/
        /* Antes de envio de correo*/
        /* Envio correo - 20 correo*/
            try
            {
                /*if (!_repConfiguracionEnvioMailingDetalle.Exist(idConfiguracionEnvioMailingDetalle))*/
                if (!_repConfiguracionEnvioMailingDetalle.ExisteConfiguracionEnvioMailingDetalle(idConfiguracionEnvioMailingDetalle))
                {
                    throw new Exception("No existe la configuracion envio mailing detalle");
                }
                // Mantener por respaldo
                // var configuracionEnvioMailingDetalle = _repConfiguracionEnvioMailingDetalle.FirstById(idConfiguracionEnvioMailingDetalle);
                var configuracionEnvioMailingDetalle = _repConfiguracionEnvioMailingDetalle.BuscaConfiguracionEnvioMailingDetallePorId(idConfiguracionEnvioMailingDetalle);

                // Envio correo
                //var conjuntoListaResultado = _repConjuntoListaResultado.FirstById(configuracionEnvioMailingDetalle.IdConjuntoListaResultado);
                // Nuevo
                var conjuntoListaResultado = _repConjuntoListaResultado.BuscaConjuntoListaResultado(configuracionEnvioMailingDetalle.IdConjuntoListaResultado);
                
                var oportunidad = _repOportunidad.FirstById(conjuntoListaResultado.IdOportunidad.Value);
                var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);
                var alumno = _repAlumno.FirstById(oportunidad.IdAlumno);

                List<string> correosPersonalizados = new List<string>
                {
                    alumno.Email1
                };
                   
                var archivosAdjuntos = this.ObtenerArchivosAdjuntos(configuracionEnvioMailingDetalle.CuerpoHtml);
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = personal.Email,
                    //Sender = "w.choque.itusaca@isur.edu.pe",
                    Recipient = string.Join(",", correosPersonalizados.Distinct()),
                    Subject = configuracionEnvioMailingDetalle.Asunto,
                    Message = this.QuitarEtiquetasArchivosAdjuntos(configuracionEnvioMailingDetalle.CuerpoHtml),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = archivosAdjuntos
                };
                var mailServie = new TMK_MailServiceImpl();

                mailServie.SetData(mailDataPersonalizado);
                var listaIdsMailChimp = mailServie.SendMessageTask();

                List<MandrilEnvioCorreoBO> listaMandrilEnvioCorreoBO = new List<MandrilEnvioCorreoBO>();

                foreach (var mensaje in listaIdsMailChimp)
                {
                    var mandrilEnvioCorreoBO = new MandrilEnvioCorreoBO
                    {
                        IdOportunidad = oportunidad.Id,
                        IdPersonal = oportunidad.IdPersonalAsignado,
                        IdAlumno = oportunidad.IdAlumno,
                        IdCentroCosto = oportunidad.IdCentroCosto,
                        IdMandrilTipoAsignacion = 6, //Envio masivo operaciones
                        EstadoEnvio = 1,
                        IdMandrilTipoEnvio = 2, //Manual = 2
                        FechaEnvio = DateTime.Now,
                        Asunto = configuracionEnvioMailingDetalle.Asunto,
                        FkMandril = mensaje.MensajeId,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "EnvioAutomatico",
                        UsuarioModificacion = "EnvioAutomatico",
                        EsEnvioMasivo = true
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
                // Mantener por respaldo
                //_repMandrilEnvioCorreo.Insert(listaMandrilEnvioCorreoBO);

                // Nuevo
                var resultadoListaMandrilEnvioCorreoBO = _repMandrilEnvioCorreo.InsertarMandrilEnvioCorreo(listaMandrilEnvioCorreoBO);
                return resultadoListaMandrilEnvioCorreoBO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los archivos adjuntos por plantilla
        /// </summary>
        /// <param name="plantilla">Plantilla a la cual se va a analizar</param>
        /// <returns>Lista de objetos (EmailAttachment)</returns>
        public List<EmailAttachment> ObtenerArchivosAdjuntos(string plantilla) {
            try
            {
                var listaArchivosAdjunto = new List<string>()
                {
                    "{ArchivoAdjunto.ManualIngresoAulaVirtual}",
                    "{ArchivoAdjunto.ManualBSPlay}",
                    "{ArchivoAdjunto.ManualConectarseSesionWebinar}",
                    "{ArchivoAdjunto.ManualConectarseSesionVirtual}"
                };

                var listaArchivosAdjuntos = new List<EmailAttachment>();

                if (listaArchivosAdjunto.Any(plantilla.Contains))
                {
                    if (plantilla.Contains("{ArchivoAdjunto.ManualIngresoAulaVirtual}"))
                    {
                        listaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualIngresoAulaVirtual}"))),
                            Name = "Manual para ingreso al Aula Virtual.pdf",
                            Type = "application/pdf"
                        });
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualIngresoAulaVirtual}", "");
                    }

                    if (plantilla.Contains("{ArchivoAdjunto.ManualBSPlay}"))
                    {
                        listaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualBSPlay}"))),
                            Name = "Manual BS Play.pdf",
                            Type = "application/pdf"
                        });
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualBSPlay}", "");
                    }

                    if (plantilla.Contains("{ArchivoAdjunto.ManualConectarseSesionWebinar}"))
                    {
                        listaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualConectarseSesionWebinar}"))),
                            Name = "Manual para conectarse a la sesión webinar.pdf",
                            Type = "application/pdf"
                        });
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualConectarseSesionWebinar}", "");
                    }

                    if (plantilla.Contains("{ArchivoAdjunto.ManualConectarseSesionVirtual}"))
                    {
                        listaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualConectarseSesionVirtual}"))),
                            Name = "Manual para conectarse a la sesión virtual.pdf",
                            Type = "application/pdf"
                        });
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualConectarseSesionVirtual}", "");
                    }
                }
                return listaArchivosAdjuntos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los archivos adjuntos por plantilla
        /// </summary>
        /// <param name="plantilla"></param>
        /// <returns></returns>
        public string QuitarEtiquetasArchivosAdjuntos(string plantilla)
        {
            try
            {
                var listaArchivosAdjunto = new List<string>()
                {
                    "{ArchivoAdjunto.ManualIngresoAulaVirtual}",
                    "{ArchivoAdjunto.ManualBSPlay}",
                    "{ArchivoAdjunto.ManualConectarseSesionWebinar}",
                    "{ArchivoAdjunto.ManualConectarseSesionVirtual}"
                };

                var listaArchivosAdjuntos = new List<EmailAttachment>();

                if (listaArchivosAdjunto.Any(plantilla.Contains))
                {
                    if (plantilla.Contains("{ArchivoAdjunto.ManualIngresoAulaVirtual}"))
                    {
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualIngresoAulaVirtual}", "");
                    }

                    if (plantilla.Contains("{ArchivoAdjunto.ManualBSPlay}"))
                    {
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualBSPlay}", "");
                    }

                    if (plantilla.Contains("{ArchivoAdjunto.ManualConectarseSesionWebinar}"))
                    {
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualConectarseSesionWebinar}", "");
                    }

                    if (plantilla.Contains("{ArchivoAdjunto.ManualConectarseSesionVirtual}"))
                    {
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualConectarseSesionVirtual}", "");
                    }
                }
                return plantilla;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
