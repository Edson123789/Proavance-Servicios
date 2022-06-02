using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MaterialPresencialMininimoDTO
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public int Grupo { get; set; }
        public string TipoCursoMaterial { get; set; }
        public int Cantidad { get; set; }
        public string CursoMaterialEstado { get; set; }
        public DateTime FechaSubida { get; set; }
        public string ComentarioSubidaArchivo { get; set; }
    }
}
