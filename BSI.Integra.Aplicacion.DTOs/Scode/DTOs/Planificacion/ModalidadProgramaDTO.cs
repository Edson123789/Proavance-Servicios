using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ModalidadProgramaDTO
    {
        public string Tipo { get; set; }
        public string Ciudad { get; set; }
        public string TipoCiudad { get; set; }
        public string FechaHoraInicio { get; set; }
        public string NombrePG { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombreESP { get; set; }
        public string Duracion { get; set; }
        public string Pw_duracion { get; set; }
        public DateTime? FechaReal { get; set; }
    }


    public class ModalidadProgramaSincronicoDTO
    {
        public int IdPEspecifico { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string Modalidad { get; set; }
        public string EstadoPEspecifico { get; set; }
        public string Pais { get; set; }
        public string FechaInicioSesion { get; set; }
    }
}
