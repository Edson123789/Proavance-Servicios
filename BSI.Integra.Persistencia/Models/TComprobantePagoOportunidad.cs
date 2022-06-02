using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TComprobantePagoOportunidad
    {
        public int Id { get; set; }
        public int? IdContacto { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Celular { get; set; }
        public string Dni { get; set; }
        public string Correo { get; set; }
        public string NombrePais { get; set; }
        public int IdPais { get; set; }
        public string NombreCiudad { get; set; }
        public string TipoComprobante { get; set; }
        public string NroDocumento { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Direccion { get; set; }
        public int BitComprobante { get; set; }
        public int? IdOcurrencia { get; set; }
        public int IdAsesor { get; set; }
        public int? IdOportunidad { get; set; }
        public string Comentario { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdClasificacionPersona { get; set; }

        public virtual TOportunidad IdOportunidadNavigation { get; set; }
    }
}
