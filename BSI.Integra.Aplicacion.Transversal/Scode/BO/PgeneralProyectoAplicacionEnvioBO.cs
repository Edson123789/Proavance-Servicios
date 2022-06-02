using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.DTO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralProyectoAplicacionEnvioBO : BaseBO
    {
        public int IdPgeneralProyectoAplicacionEstado { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public DateTime? FechaCalificacion { get; set; }
        public decimal? Nota { get; set; }
        public string NombreArchivoRetroalimentacion { get; set; }
        public string RutaArchivoRetroalimentacion { get; set; }
        public string Comentarios { get; set; }
        public int? IdEscalaCalificacionDetalle { get; set; }
        public bool EsEntregable { get; set; }

        
        private readonly integraDBContext _integraDBContext;
        private PgeneralProyectoAplicacionEnvioRepositorio _repoEnvioProyecto;

        public PgeneralProyectoAplicacionEnvioBO()
        {
        }

        public PgeneralProyectoAplicacionEnvioBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repoEnvioProyecto = new PgeneralProyectoAplicacionEnvioRepositorio(_integraDBContext);
            //_integraDBContext.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        /// <summary>
        /// Envía el correo a los docentes sobre proyectos sin calificar del aula virtual Anterior
        /// </summary>
        /// <param name="listado">Listado de los resumen de correo de proyectos a notificar</param>
        /// <param name="idPlantilla">Id de la plantilla a utilizar</param>
        /// <returns>Lista de GmailCorreoDTO</returns>
        public List<GmailCorreoDTO> EnviarCorreoProyectoPendienteDocentesAulaAnterior(
            List<ProyectoAplicacionEnvioSinCalificarResumenCabeceraCorreoDTO> listado, int idPlantilla)
        {
            var listadoEnviar = listado.Where(w => w.IdPersonalResponsableCoordinacion != null)
                .Select(s => new
                {
                    s.IdProveedor, s.EmailProveedor, s.IdPersonalResponsableCoordinacion, s.EmailResponsableCoordinacion
                }).Distinct();
            List<GmailCorreoDTO> listadoRespuesta = new List<GmailCorreoDTO>();
            var repoPersonal = new PersonalRepositorio(_integraDBContext);

            foreach (var envio in listadoEnviar)
            {
                try
                {
                    var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdProveedor = envio.IdProveedor,
                        IdPlantilla = idPlantilla
                    };
                    reemplazoEtiquetaPlantilla.ReemplazarEtiquetasDocente();

                    var emailCalculado = reemplazoEtiquetaPlantilla.EmailReemplazado;

                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = envio.EmailResponsableCoordinacion,
                        Recipient = envio.EmailProveedor,
                        //Recipient = "adespinoza@bsginstitute.com",

                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Bcc = "modpru@bsginstitute.com"
                    };
                    
                    var mailServie = new TMK_MailServiceImpl();

                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();

                    var personal = repoPersonal.GetBy(w => w.Id == envio.IdPersonalResponsableCoordinacion,
                        s => new {s.Nombres, s.ApellidoPaterno}).FirstOrDefault();

                    listadoRespuesta.Add(new GmailCorreoDTO()
                    {
                        Asunto = mailDataPersonalizado.Subject,
                        Fecha = DateTime.Now,
                        EmailBody = mailDataPersonalizado.Message,
                        Remitente = mailDataPersonalizado.Sender,
                        Destinatarios = mailDataPersonalizado.Recipient,
                        From = personal.Nombres + " " + personal.ApellidoPaterno,
                        Bcc = mailDataPersonalizado.Bcc,
                        IdPersonal = envio.IdPersonalResponsableCoordinacion.Value
                    });
                }
                catch (Exception e)
                {
                    
                }
            }

            return listadoRespuesta;
        }

        /// <summary>
        /// Envía el correo al alumno al calificar el proyecto del aula virtual Anterior
        /// </summary>
        /// <param name="idEnvio">Id del envio - tabla: T_PgeneralProyectoAplicacionEnvio</param>
        /// <param name="idPlantilla">Id de la plantilla a utilizar</param>
        /// <returns></returns>
        public GmailCorreoDTO EnviarCorreoAlumnoProyectoCalificadoAulaAnterior(int idEnvio, int idPlantilla)
        {
            GmailCorreoDTO respuesta = null;
            try
            {
                var envio = _repoEnvioProyecto.FirstById(idEnvio);
                var repoClasificacionOportunidad = new OportunidadClasificacionOperacionesRepositorio(_integraDBContext);
                var repoPersonal = new PersonalRepositorio(_integraDBContext);
                var repoOportunidad = new OportunidadRepositorio(_integraDBContext);

                var clasificacion =
                    repoClasificacionOportunidad.FirstBy(w => w.IdMatriculaCabecera == envio.IdMatriculaCabecera);

                var oportunidad = repoOportunidad
                    .GetBy(w => w.Id == clasificacion.IdOportunidad, s => new {s.IdPersonalAsignado}).FirstOrDefault();
                var emailDestinatarios = repoOportunidad.ObtenerEmailPorOportunidad(clasificacion.IdOportunidad);
                var personal = repoPersonal.GetBy(w => w.Id == oportunidad.IdPersonalAsignado,
                    s => new {s.Id, s.Nombres, s.ApellidoPaterno}).FirstOrDefault();

                var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdOportunidad = clasificacion.IdOportunidad,
                    IdPlantilla = idPlantilla
                };
                _reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();

                var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;

                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = emailDestinatarios.EmailPersonal,
                    Recipient = emailDestinatarios.EmailAlumno,
                    //Recipient = "adespinoza@bsginstitute.com",

                    Subject = emailCalculado.Asunto,
                    Message = emailCalculado.CuerpoHTML,
                    Bcc = "modpru@bsginstitute.com"
                };

                var mailServie = new TMK_MailServiceImpl();

                mailServie.SetData(mailDataPersonalizado);
                mailServie.SendMessageTask();
                
                respuesta = new GmailCorreoDTO()
                {
                    Asunto = mailDataPersonalizado.Subject,
                    Fecha = DateTime.Now,
                    EmailBody = mailDataPersonalizado.Message,
                    Remitente = mailDataPersonalizado.Sender,
                    Destinatarios = mailDataPersonalizado.Recipient,
                    From = personal.Nombres + " " + personal.ApellidoPaterno,
                    Bcc = mailDataPersonalizado.Cc,
                    IdPersonal = personal.Id
                };
            }
            catch (Exception e)
            {
            }

            return respuesta;
        }
    }
}
