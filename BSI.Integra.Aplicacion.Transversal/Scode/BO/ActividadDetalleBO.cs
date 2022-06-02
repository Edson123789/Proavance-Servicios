using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ActividadDetalleBO : BaseBO
    {
        public int Id { get; set; }
        public int? IdActividadCabecera { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public int? DuracionReal { get; set; }
        public int IdOcurrencia { get; set; }
        public int IdEstadoActividadDetalle { get; set; }
        public string Comentario { get; set; }
        public int? IdAlumno { get; set; }
        public string Actor { get; set; }
        public int IdOportunidad { get; set; }
        public int? IdCentralLlamada { get; set; }
        public string RefLlamada { get; set; }
        public int? IdOcurrenciaActividad { get; set; }

        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public DateTime? FechaOcultarWhatsapp { get; set; }
        public int? IdOcurrenciaAlterno { get; set; }
        public int? IdOcurrenciaActividadAlterno { get; set; }
        //BO
        public LlamadaActividadBO LlamadaActividad;


        ActividadDetalleRepositorio _repActividad;
        public ActividadDetalleBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
        public ActividadDetalleBO(int id)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repActividad = new ActividadDetalleRepositorio();
            LlamadaActividad = new LlamadaActividadBO();

            var Actividad = _repActividad.FirstById(id);
            this.Id = Actividad.Id;
            this.IdActividadCabecera = Actividad.IdActividadCabecera;
            this.FechaProgramada = Actividad.FechaProgramada;
            this.FechaReal = Actividad.FechaReal;
            this.DuracionReal = Actividad.DuracionReal;
            this.IdOcurrencia = Actividad.IdOcurrencia;
            this.IdEstadoActividadDetalle = Actividad.IdEstadoActividadDetalle;
            this.Comentario = Actividad.Comentario;
            this.IdAlumno = Actividad.IdAlumno;
            this.Actor = Actividad.Actor;
            this.IdOportunidad = Actividad.IdOportunidad;
            this.IdCentralLlamada = Actividad.IdCentralLlamada;
            this.RefLlamada = Actividad.RefLlamada;
            this.IdOcurrenciaActividad = Actividad.IdOcurrenciaActividad;
            this.FechaCreacion = Actividad.FechaCreacion;
            this.FechaModificacion = Actividad.FechaModificacion;
            this.UsuarioCreacion = Actividad.UsuarioCreacion;
            this.UsuarioModificacion = Actividad.UsuarioModificacion;
			this.IdClasificacionPersona = Actividad.IdClasificacionPersona;
			this.Estado = Actividad.Estado;
            this.RowVersion = Actividad.RowVersion;
            this.IdOcurrenciaAlterno = Actividad.IdOcurrenciaAlterno;
            this.IdOcurrenciaActividadAlterno = Actividad.IdOcurrenciaActividadAlterno;
        }

        public ActividadDetalleBO(int id , integraDBContext contexto)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repActividad = new ActividadDetalleRepositorio(contexto);
            LlamadaActividad = new LlamadaActividadBO();

            var Actividad = _repActividad.FirstById(id);
            this.Id = Actividad.Id;
            this.IdActividadCabecera = Actividad.IdActividadCabecera;
            this.FechaProgramada = Actividad.FechaProgramada;
            this.FechaReal = Actividad.FechaReal;
            this.DuracionReal = Actividad.DuracionReal;
            this.IdOcurrencia = Actividad.IdOcurrencia;
            this.IdEstadoActividadDetalle = Actividad.IdEstadoActividadDetalle;
            this.Comentario = Actividad.Comentario;
            this.IdAlumno = Actividad.IdAlumno;
            this.Actor = Actividad.Actor;
            this.IdOportunidad = Actividad.IdOportunidad;
            this.IdCentralLlamada = Actividad.IdCentralLlamada;
            this.RefLlamada = Actividad.RefLlamada;
            this.IdOcurrenciaActividad = Actividad.IdOcurrenciaActividad;
            this.FechaCreacion = Actividad.FechaCreacion;
            this.FechaModificacion = Actividad.FechaModificacion;
            this.UsuarioCreacion = Actividad.UsuarioCreacion;
            this.UsuarioModificacion = Actividad.UsuarioModificacion;
            this.Estado = Actividad.Estado;
			this.IdClasificacionPersona = Actividad.IdClasificacionPersona;
            this.RowVersion = Actividad.RowVersion;
            this.IdOcurrenciaAlterno = Actividad.IdOcurrenciaAlterno;
            this.IdOcurrenciaActividadAlterno = Actividad.IdOcurrenciaActividadAlterno;
        }
        public void GuardarCompuestos()
        {
           
        }
    }
}
