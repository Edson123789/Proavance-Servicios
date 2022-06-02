using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AscensoCompuestoDTO
    {
        public int Id { get; set; }
        public string CargoMercado { get; set; }
        public bool? Activo { get; set; }
        public string FechaPublicacion { get; set; }
        public int? SueldoMin { get; set; }
        public int? IdMoneda { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdPortalEmpleo { get; set; }
        public int? IdCargo { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdPais { get; set; }
        public int? IdRegionCiudad { get; set; }
        public int? IdCiudad { get; set; }
        public string UrlOferta { get; set; }

        public List<int> AreaFormaciones { get; set; }
        public List<int> Certificaciones { get; set; }
        public List<int> Membresias { get; set; }
        public List<AscensoProgramaCapacitacionDTO> ProgramasCapacitacion { get; set; }
        public List<AscensoProgramaCapacitacionExperienciaDTO> ProgramasCapacitacionExperiencia { get; set; }
        public List<AscensoExperienciaCargoIndustriaDTO> Experiencias { get; set; }
        
        public string Usuario { get; set; }
    }
}
