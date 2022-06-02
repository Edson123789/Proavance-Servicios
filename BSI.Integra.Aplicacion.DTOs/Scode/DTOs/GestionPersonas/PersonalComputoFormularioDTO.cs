using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class PersonalComputoFormularioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string Programa { get; set; }
        public int? IdNivelEstudio { get; set; }
        public string NivelEstudio { get; set; }
        public int? IdCentroEstudio { get; set; }
        public string CentroEstudio { get; set; }
        public bool Estado { get; set; }
    }
}
