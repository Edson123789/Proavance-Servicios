using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CursosCentroCostoDTO
    {
        public int Id { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombreCursoEspecifico { get; set; }
        public int  Duracion { get; set; }
        public int  Orden { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string NombrePEspecifico { get; set; }
        public DateTime  FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public int Estado { get; set; }

    }
}
