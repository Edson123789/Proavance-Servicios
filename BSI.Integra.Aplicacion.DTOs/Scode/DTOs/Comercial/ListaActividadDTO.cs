using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ListaActividadDTO
    {
        public int Id { get; set; }
        public ActividadCabeceraCompuestoDTO ActividadCabeceraLlamada { get; set; }
        public ActividadCabeceraIndividualDTO ActividadCabeceraIndividual { get; set; }
        public ActividadCabeceraMasivoDTO ActividadCabeceraMasivo { get; set; }
        
        public string ActividadBase { get; set; }
        public string Usuario { get; set; }
    }
}
