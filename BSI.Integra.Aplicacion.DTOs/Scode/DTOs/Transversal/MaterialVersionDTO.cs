using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MaterialVersionDTO2
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public string UrlArchivo { get; set; }
        public int IdMaterialPespecificoSesion { get; set; }
        public int IdMaterialEstado { get; set; }
        public int IdTipoPersona { get; set; }
        public int? IdProveedor { get; set; }
        public DateTime? FechaSubida { get; set; }
        public string ComentarioSubida { get; set; }
    }

    public class MaterialCompuestoVersionDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public List<MaterialVersionDTO> ListaMaterialVersion { get; set; }
    }

    public class MaterialVersionSubirDTO
    {
        public int Id { get; set; }
        public string ComentarioSubida { get; set; }
        public string NombreUsuario { get; set; }
    }
}
