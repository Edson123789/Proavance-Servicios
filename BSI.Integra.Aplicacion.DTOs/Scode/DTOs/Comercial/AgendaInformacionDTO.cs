using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AgendaInformacionDTO
    {
    }

    public class AgendaInformacionSolicitudAccesoTemporalDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public string Modalidad { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int IdAlumno { get; set; }
        public string Alumno { get; set; }
    }
}
