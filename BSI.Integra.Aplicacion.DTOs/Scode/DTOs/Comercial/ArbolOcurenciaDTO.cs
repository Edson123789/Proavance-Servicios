using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class ArbolOcurenciaDTO
    {
        public int IdOcurrenciaActividad { get; set; }
        public int IdOcurrenciaReporte { get; set; }
        public string RequiereLlamada { get; set; }
        public int EstadoOcurrencia { get; set; }
        public string NombreOcurrencia { get; set; }
        public string Color { get; set; }
        public string Roles { get; set; }
        public string Nivel { get; set; }
        public bool TieneOcurrencias { get; set; }
        public string TieneActividades { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdOcurrenciaActividad_Padre { get; set; }
        public int IdActividadCabecera { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdPlantilla_Speech { get; set; }
        public string NombreEstadoOcurrencia { get; set; }
        public bool CrearOportunidad { get; set; }
        public string FaseSiguiente { get; set; }
        public int? IdPlantillaWP { get; set; }
        public int? IdPlantillaCE { get; set; }

    }
    public class OcurenciaActividadCompletoDTO
    {
        public int Id { get; set; }
        public int IdOcurrencia { get; set; }
        public string Nombre { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdActividadCabecera { get; set; }

        public int? IdPlantilla_Speech { get; set; }
        public int? IdActividadCabeceraProgramada { get; set; }
        public string Roles { get; set; }

    }
}
