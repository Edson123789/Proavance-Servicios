using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EliminarDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class EliminarAccesoTemporalDTO
    {
        public int IdPersonal { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class ActualizarAccesoTemporalDTO
    {
        public int IdPersonal { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public int? IdPEspecificoPadreAnterior { get; set; }
        public List<int> ListaPEspecificoHijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime? FechaInicioAnterior { get; set; }
        public DateTime? FechaFinAnterior { get; set; }
        public bool EvaluacionHabilitada { get; set; }
        public string Usuario { get; set; }
    }

    public class AprobarMaterialVersionDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
    }
    public class DesaprobarMaterialVersionDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class NotificarMaterialVersionDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class NotificarListaMaterialVersionDTO
    {
        public List<int> ListaIdMaterialPEspecificoDetalle { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class AsociarActualizarFurMaterialVersionDTO
    {
        public int IdMaterialPEspecificoDetalle { get; set; }
        public int? IdFur { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdProducto { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Monto { get; set; }
        public string DireccionEntrega { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class EliminarConfiguracionVideoDTO
    {
        public string VideoId { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class EliminarConfiguracionProgramaDTO
    {
        public int IdPGeneral { get; set; }
        public string NombreUsuario { get; set; }
    }
}
