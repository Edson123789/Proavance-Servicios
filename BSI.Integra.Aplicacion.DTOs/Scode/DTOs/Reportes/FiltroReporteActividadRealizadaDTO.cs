using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroReporteActividadRealizadaDTO
    {
        public List<EstadoOcurrenciaFiltroDTO> EstadoOcurrencia { get; set; }
        public List<PersonalAsignadoDTO> Asesores { get; set; }
        public List<FaseOportunidadFiltroDTO> FaseOportunidad { get; set; }
        public List<FiltroDTO> TipoDato { get; set; }
        public List<ProbabilidadRegistroPwFiltroDTO> Probabilidad { get; set; }
        public List<FiltroDTO> CategoriaOrigen { get; set; }
        public List<PersonalAsignadoDTO> AsistentesActivos { get; set; }
        public List<PersonalAsignadoDTO> AsistentesTotales { get; set; }
        public List<PersonalAsignadoDTO> AsistentesInactivos { get; set; }


    }
}
