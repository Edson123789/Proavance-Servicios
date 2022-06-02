using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class PersonalHistorialMedicoFormularioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string DetalleEnfermedad { get; set; }
        public string Periodo { get; set; }
        public bool Estado { get; set; }

    }
}
