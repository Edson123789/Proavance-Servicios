using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FinalizarActividadDTO
    {
        public OportunidadDTO Oportunidad { get; set; }
        public ActividadDetalleDTO ActividadAntigua { get; set; }
        public ComprobantePagoOportunidadDTO ComprobantePago { get; set; }
        public OportunidadPreriquisitosBeneficiosSolucionesCompuestoDTO DatosCompuesto { get; set; }
        public OportunidadFiltroDTO Filtro { get; set; }
        public string Usuario { get; set; }
    }

    public class FinalizarActividadAlternoDTO
    {
        public OportunidadDTO Oportunidad { get; set; }
        public ActividadDetalleDTO ActividadAntigua { get; set; }
        public ComprobantePagoOportunidadDTO ComprobantePago { get; set; }
        public OportunidadPreriquisitosBeneficiosSolucionesCompuestoAlternoDTO DatosCompuesto { get; set; }
        public OportunidadFiltroDTO Filtro { get; set; }
        public string Usuario { get; set; }
    }
}
