using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoInformacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        public string EstadoP { get; set; }
        public string Tipo { get; set; }
        public string Categoria { get; set; }
        public string CodigoBanco { get; set; }
        public string Ciudad { get; set; }
        public int IdProgramaGeneral { get; set; }
    }

    public class PEspecificoAlumnoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
    }
}
