using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class ReclamoDTO
    {
        public int Id { get; set; }
        public int IdMatricula { get; set; }
        public string Descripcion { get; set; }
        public int IdReclamoEstado { get; set; }
        public int IdOrigen { get; set; }
        public int IdTipoReclamoAlumno { get; set; }
        public string Usuario { get; set; }
    }
}
