using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MatriculaActualizarDTO
    {
        public string Codigomatricula { get; set; }
        public string Estado { get; set; }
        public Nullable<int> Periodo { get; set; }
        public Nullable<int> Programa { get; set; }
        public Nullable<int> Asesor { get; set; }
        public Nullable<int> Coordinador { get; set; }
        public string Observaciones { get; set; }
        public bool EmpresaPaga { get; set; }
        public string EmpresaNombre { get; set; }
        public string usuario { get; set; }
    }
}
