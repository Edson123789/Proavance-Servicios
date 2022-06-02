using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EstadoMatriculaDTO
    {
        public int Id { get; set; }
        public string EstadoMatricula { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
    public class EstadoMatriculaListDTO
    {
        public List<TCRM_EstadoMatriculaInsertarDTO> Estado { get; set; }

    }
    public class TCRM_EstadoMatriculaInsertarDTO
    {
        public int Id { get; set; }
        public string EstadoMatricula { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public List<int> IdSubEstados { get; set; }
    }
}
