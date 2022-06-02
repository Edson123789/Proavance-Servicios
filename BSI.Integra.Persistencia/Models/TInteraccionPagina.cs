using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionPagina
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdInteraccionScore { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPgeneralGenerico { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? IdPgeneralGenericoSiguiente { get; set; }
        public int? IdSubAreaCapcitacionSiguiente { get; set; }
        public int? IdAreaCapacitacionSiguiente { get; set; }
        public int? IdPgeneralGenericoAnterior { get; set; }
        public int? IdSubAreaCapcitacionAnterior { get; set; }
        public int? IdAreaCapacitacionAnterior { get; set; }
        public int? IdCategoriaInteraccion { get; set; }
        public int? IdCategoriaInteraccionSiguiente { get; set; }
        public int? IdCategoriaInteraccionAnterior { get; set; }
        public int? IdSubcategoriaInteraccion { get; set; }
        public int? IdSubCategoriaInteraccionSiguiente { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public Guid? IpIdCookie { get; set; }
        public int? IpScore { get; set; }
        public int? IpValorMedible { get; set; }
        public string IpIp { get; set; }
        public DateTime? IpFechaInicio { get; set; }
        public DateTime? IpFechaFin { get; set; }
        public string UrlActual { get; set; }
        public string UrlAnterior { get; set; }
        public string UrlSiguiente { get; set; }
        public string Correo { get; set; }
        public string Ip { get; set; }
        public int? IdProveedorCampaniaIntegra { get; set; }
        public string IdConjuntoAnuncio { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
