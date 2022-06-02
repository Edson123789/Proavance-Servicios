using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MarcarAsistenciaDTO
    {
        public int Id { get; set; }
        public bool Asistio { get; set; }
        public bool Justifico { get; set; }
    }

    public class PEspecificoSesionCompuestoDTO
    {
        public int  Id { get; set; } //IdPEspecificoSesion
        public string NombreUsuario { get; set; }
        public List<MarcarAsistenciaDTO> ListaAsistencia { get; set; }
    }

    public class MaterialEntregaDetalleDTO
    {
        public int Id { get; set; }
        public int IdMaterialVersion { get; set; }
        public int IdAsistencia { get; set; }
        public bool Entregado { get; set; }
        public string Comentario { get; set; }
        public string NombreUsuario { get; set; }
    }

}
