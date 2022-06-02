using System;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PersonalAsesorCentroCostoDTO
    {
        public string NombreAsesor { get; set; }
        public int IdAsesor { get; set; }
        public bool Habilitado { get; set; }
        public int IdAsesorCentroCosto { get; set; }
        public int AsignacionMaxima { get; set; }
        public int AsignacionMinima { get; set; }
        public int AsignacionMaximaBnc { get; set; }
        public string AsignacionPais { get; set; }
        public int CantidadAsignados { get; set; }
    }
}
