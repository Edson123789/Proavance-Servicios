using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class DatoFamiliarPersonalFormularioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdSexo { get; set; }
        public string Sexo { get; set; }
        public int? IdParentescoPersonal { get; set; }
        public string ParentescoPersonal { get; set; }
        public int? IdTipoDocumentoPersonal { get; set; }
        public string TipoDocumentoPersonal { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroReferencial1 { get; set; }
        public string NumeroReferencial2 { get; set; }
        public bool? DerechoHabiente { get; set; }
        public bool? EsContactoInmediato { get; set; }
        public bool Estado { get; set; }
    }
}
