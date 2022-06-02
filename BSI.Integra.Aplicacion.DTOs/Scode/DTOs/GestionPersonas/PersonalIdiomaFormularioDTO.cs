using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class PersonalIdiomaFormularioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdIdioma { get; set; }
        public string Idioma { get; set; }
        public int? IdNivelIdioma { get; set; }
        public string NivelIdioma { get; set; }
        public int? IdCentroEstudio { get; set; }
        public string CentroEstudio { get; set; }
        public bool Estado { get; set; }

    }
}
