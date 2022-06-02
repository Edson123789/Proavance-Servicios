using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoCronogramaGrupalDTO
    {
        public int Id { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public int IdPespecifico { get; set; }
        public double Duracion { get; set; }
        public string DuracionTotal { get; set; }
        public string Curso { get; set; }
        public string Tipo { get; set; }
        public int Grupo { get; set; }
        public string ModalidadSesion { get; set; }
    }
}
