using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PersonalAsignadoDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public string Usuario { get; set; }
        public string TipoPersonal { get; set; }
        public string NivelVisualizacionAgenda { get; set; }
    }

    public class PersonalAsignadoReportePendienteDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public string Usuario { get; set; }
        public string TipoPersonal { get; set; }        
    }

}
