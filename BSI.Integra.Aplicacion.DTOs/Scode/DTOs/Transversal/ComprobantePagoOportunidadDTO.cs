using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public  class ComprobantePagoOportunidadDTO
    {
        public int IdContacto { get; set; }
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
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
    public class ComprobantePagoAlumnoDTO
    {
        public string MedioPago { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string Programa { get; set; }
        public string ConceptoPago { get; set; }
        public string Moneda { get; set; }
        public float Monto { get; set; }
        public float MontoPagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public string TipoComprobante { get; set; }
        public string NroDocumento { get; set; }
        public string NombreRazonSocial { get; set; }       
    }
    public class filtroReporteComprobanteDTO
    {
        public string IdFormaPago { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string Programa { get; set; }
        public string Comprobante { get; set; }
        public int? IdPeriodo { get; set; }
    }
    public class CombosComprobanteDTO
    {
        public List<FiltroDTO> ListaPeriodo { get; set; }
        public List<FormaPagoDTO> ListaFormaPago { get; set; }
    }
}
