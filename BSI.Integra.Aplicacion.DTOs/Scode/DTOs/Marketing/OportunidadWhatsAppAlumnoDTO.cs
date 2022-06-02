using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadWhatsAppAlumnoDTO
    {
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdCargo { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string Usuario { get; set; }
    }

    public class OportunidadWhatsAppAlumnoActualizableDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdCargo { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string Usuario { get; set; }
    }
}
