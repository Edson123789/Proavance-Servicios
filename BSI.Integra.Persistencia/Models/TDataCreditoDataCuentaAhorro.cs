using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataCuentaAhorro
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public bool? Bloqueada { get; set; }
        public string Entidad { get; set; }
        public string Numero { get; set; }
        public DateTime? FechaApertura { get; set; }
        public string Calificacion { get; set; }
        public string SituacionTitular { get; set; }
        public string Oficina { get; set; }
        public string Ciudad { get; set; }
        public string CodigoDaneCiudad { get; set; }
        public int? TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Sector { get; set; }
        public string CaracteristicaClase { get; set; }
        public string ValorMoneda { get; set; }
        public DateTime? ValorFecha { get; set; }
        public string ValorCalificacion { get; set; }
        public string EstadoCodigo { get; set; }
        public DateTime? EstadoFecha { get; set; }
        public string Llave { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TDataCreditoBusqueda IdDataCreditoBusquedaNavigation { get; set; }
    }
}
