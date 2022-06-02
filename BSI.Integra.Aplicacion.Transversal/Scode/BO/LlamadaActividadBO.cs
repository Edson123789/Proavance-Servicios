using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    
    public class LlamadaActividadBO : BaseBO
    {

        public int Id { get; set; }
        public int? IdActividadDetalle { get; set; }
        public int? IdAsesor { get; set; }
        public int? IdLlamada { get; set; }
        public bool EstadoProgramado { get; set; }
        public string Tag { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public int? IdAgendaTab { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        private LlamadaActividadRepositorio _repLlamadaActividad;

        public LlamadaActividadBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
        public LlamadaActividadBO(int idActividad)
        {
            _repLlamadaActividad = new LlamadaActividadRepositorio();
            var Llamada = _repLlamadaActividad.ObtenerLlamadaActividadPorActividad(idActividad);
            if(Llamada != null)
            {
                this.Id = Llamada.Id;
                this.IdActividadDetalle = Llamada.IdActividadDetalle;
                this.IdAsesor = Llamada.IdAsesor;
                this.IdLlamada = Llamada.IdLlamada;
                this.EstadoProgramado = Llamada.EstadoProgramado;
                this.Tag = Llamada.Tag;
                this.FechaInicioLlamada = Llamada.FechaInicioLlamada;
                this.FechaFinLlamada = Llamada.FechaFinLlamada;
                this.IdAgendaTab = Llamada.IdAgendaTab;
                this.FechaCreacion = Llamada.FechaCreacion;
                this.FechaModificacion = Llamada.FechaModificacion;
                this.UsuarioCreacion = Llamada.UsuarioCreacion;
                this.UsuarioModificacion = Llamada.UsuarioModificacion;
                this.Estado = Llamada.Estado;
                this.RowVersion = Llamada.RowVersion;
            }
        }
    }
}
