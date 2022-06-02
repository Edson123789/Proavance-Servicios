using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class PersonalExperienciaFormularioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public string AreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public string MotivoRetiro { get; set; }
        public string NombreJefeInmediato { get; set; }
        public string TelefonoJefeInmediato { get; set; }
        public bool Estado { get; set; }

    }
}
