using System;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsesorCentroCostoDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int AsignacionMaxima { get; set; }
        public int AsignacionMinima { get; set; }
        public int AsignacionMaximaBnc { get; set; }
        public string AsignacionPais { get; set; }
        public bool Habilitado { get; set; }
        public string NombreUsuario { get; set; }
    }
}
