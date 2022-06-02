using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SesionesOnlineWebinarDocenteDTO
    {
        public int IdPGeneral { get; set; }
        public string PGeneral { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public string PEspecificoPadre { get; set; }
        public int IdPEspecificoHijo { get; set; }
        public string CursoNombre { get; set; }
        public int IdProveedor { get; set; }
        public DateTime FechaSesion { get; set; }
        public string HoraSesion { get; set; }
        public string Tipo { get; set; }
        public string UrlWebex { get; set; }
    }

    public class DetalleSesionesAlumnosDTO
    {
        public int IdPGeneral { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdSesion { get; set; }
        public int IdCoordinadoraAcademica { get; set; }
        public string NombreCoordinadoraAcademica { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }
        public string CentroCosto { get; set; }
        public string EstadoMatricula { get; set; }
        public string Confirmo { get; set; }

    }


}
