using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class PersonalFormularioDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string DistritoDireccion { get; set; }
        public string FijoReferencia { get; set; }
        public string MovilReferencia { get; set; }
        public string EmailReferencia { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public int? IdSistemaPensionario { get; set; }
        public string SistemaPensionario{ get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public string EntidadSistemaPensionario { get; set; }
        public bool Estado { get; set; }
    }
}
