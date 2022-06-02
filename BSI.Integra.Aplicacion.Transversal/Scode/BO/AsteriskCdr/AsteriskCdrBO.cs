using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.DTOs.Transversal.AsteriskCdr;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio.AsteriskCdr;

namespace BSI.Integra.Aplicacion.Transversal.BO.AsteriskCdr
{
    /// BO: Transversal/AsteriskCdrBO
    /// Autor: Ansoli Deyvis
    /// Fecha: 26-01-2021
    /// <summary>
    /// BO para la gestion de la data de Asterisk
    /// </summary>
    public class AsteriskCdrBO
    {
        /// Propiedades	Significado
        /// -----------	------------
        /// _repo		Respositorio de mysql de Asterisk
        
        private AsteriskCdrRepositorio _repo;

        public AsteriskCdrBO()
        {
            _repo = new AsteriskCdrRepositorio();
        }
        
        /// Autor: Ansoli Espinoza
        /// Fecha: 26-01-2021
        /// Version: 1.0
        /// <summary>
        /// Importa las llamadas pendientes de asterisk a v4
        /// </summary>
        /// <returns>Devuele la espuesta de la importación</returns>
        public RespuestaImportacionLlamadaDTO ImportarLlamadasPendientes()
        {
            RespuestaImportacionLlamadaDTO respuesta = new RespuestaImportacionLlamadaDTO();
            ControlDescargaLlamadaAsteriskRepositorio repoControlDescarga =
                new ControlDescargaLlamadaAsteriskRepositorio();

            int id = 0;
            try
            {
                LlamadaWebphoneAsteriskRepositorio repoLlamadasv4 = new LlamadaWebphoneAsteriskRepositorio();

                var idDb = repoLlamadasv4.ObtenerUltimoIdImportado();
                if (idDb != null)
                    id = idDb;

                var listadoPendiente = _repo.ListadoLlamadasMayoresA(id);
                if (listadoPendiente != null && listadoPendiente.Count > 0)
                {
                    respuesta.IdLlamadaInicial = listadoPendiente.Min(m => m.RecordingId);
                    respuesta.IdLlamadaFinal = listadoPendiente.Max(m => m.RecordingId);
                    var listadoImportar = listadoPendiente.Select(s => new LlamadaWebphoneAsteriskBO()
                    {
                        FechaInicio = s.FechaInicio,
                        FechaFin =
                            s.FechaInicio == null ? (DateTime?) null : s.FechaInicio.Value.AddSeconds(s.Duration),
                        Anexo = s.Src,
                        TelefonoDestino = s.Dst,
                        IdActividadDetalle = s.IdActividadDetalle,
                        IdLlamadaWebphoneTipo =
                            s.CallType == "incoming"
                                ? 1
                                : (s.CallType == "outbound" ? 2 : 0), //1: Entrante - 2: Saliente
                        CdrId = s.RecordingId,
                        DuracionTimbrado = s.Duration - s.Billsec,
                        DuracionContesto = s.Billsec,
                        NombreGrabacion = s.Recordingfile,

                        Estado = true,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    });
                    bool resultado = repoLlamadasv4.Insert(listadoImportar);
                    repoControlDescarga.Insert(new ControlDescargaLlamadaAsteriskBO()
                    {
                        IdLlamadaInicial = respuesta.IdLlamadaInicial,
                        IdLlamadaFinal = respuesta.IdLlamadaFinal,
                        DescargaCorrecta = true,
                        DescargaEnProceso = false,
                        TieneError = false,
                        Estado = true,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    });
                }

                respuesta.Estado = true;
                respuesta.Mensaje = "Importación Correcta";
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = e.Message + (e.InnerException != null ? (" - " + e.InnerException.Message) : "");
                repoControlDescarga.Insert(new ControlDescargaLlamadaAsteriskBO()
                {
                    IdLlamadaInicial = id > 0 ? id + 1 : respuesta.IdLlamadaInicial,
                    IdLlamadaFinal = respuesta.IdLlamadaFinal,
                    DescargaCorrecta = false,
                    DescargaEnProceso = false,
                    TieneError = true,
                    MensajeError = respuesta.Mensaje,
                    Estado = true,
                    UsuarioCreacion = "SYSTEM",
                    UsuarioModificacion = "SYSTEM",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                });
                return respuesta;
            }
        }
    }
}
