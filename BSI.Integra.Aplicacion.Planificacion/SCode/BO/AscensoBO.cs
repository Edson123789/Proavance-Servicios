using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class AscensoBO : BaseBO
    {
        public int Id { get; set; }
        public string CargoMercado { get; set; }
        public bool? Activo { get; set; }
        public string FechaPublicacion { get; set; }
        public int? SueldoMin { get; set; }
        public int? IdMoneda { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdPortalEmpleo { get; set; }
        public int? IdCargo { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdPais { get; set; }
        public int? IdRegionCiudad { get; set; }
        public int? IdCiudad { get; set; }
        public string UrlOferta { get; set; }

    }
}
