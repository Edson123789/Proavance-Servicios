using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class SentinelSdtInfGenBO : BaseBO
    {
        public int? IdSentinel { get; set; }
        public string Dni { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Digito { get; set; }
        public string DigitoAnterior { get; set; }
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string TipoContribuyente { get; set; }
        public string CodigoTipoContribuyente { get; set; }
        public string EstadoContribuyente { get; set; }
        public string CodigoEstadoContribuyente { get; set; }
        public string CondicionContribuyente { get; set; }
        public string CodigoCondicionContribuyente { get; set; }
        public string ActividadEconomica { get; set; }
        public string Ciiu { get; set; }
        public string ActividadEconomica2 { get; set; }
        public string Ciiu2 { get; set; }
        public string ActividadEconomica3 { get; set; }
        public string Ciiu3 { get; set; }
        public DateTime? FechaActividad { get; set; }
        public string Direccion { get; set; }
        public string Referencia { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string Ubigeo { get; set; }
        public DateTime? FechaConstitucion { get; set; }
        public string ActvidadComercioExterior { get; set; }
        public string CodigoActividadComerExt { get; set; }
        public string CodigoDependencia { get; set; }
        public string Dependencia { get; set; }
        public string Folio { get; set; }
        public string Asiento { get; set; }
        public string Tomo { get; set; }
        public string PartidaReg { get; set; }
        public string Patron { get; set; }
		public Guid? IdMigracion { get; set; }
	}
}
