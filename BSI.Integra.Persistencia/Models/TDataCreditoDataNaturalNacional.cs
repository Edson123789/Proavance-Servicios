using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataNaturalNacional
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string NroDocumento { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NombreCompleto { get; set; }
        public bool? Validada { get; set; }
        public bool? Rut { get; set; }
        public string Genero { get; set; }
        public string IdentificacionEstado { get; set; }
        public DateTime? IdentificacionFechaExpedicion { get; set; }
        public string IdentificacionCiudad { get; set; }
        public string IdentificacionDepartamento { get; set; }
        public string IdentificacionNumero { get; set; }
        public int? EdadMinima { get; set; }
        public int? EdadMaxima { get; set; }
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
