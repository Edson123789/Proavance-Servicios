using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoLogDTO
    {
        public int Id { get; set; }
        public string CampoActualizado { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }

    public class ExpositorLogDTO
    {
        public int Id { get; set; }
        public string CampoActualizado { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }

    public class ProveedorLogDTO
    {
        public int Id { get; set; }
        public string CampoActualizado { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
