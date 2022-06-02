using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ParametroFinalizarActividadDTO
    {
        public DatosFiltroFinalizarActividadDTO filtro { get; set; }
        public OportunidadDTO datosOportunidad { get; set; }
        public ActividadDetalleDTO ActividadAntigua { get; set; }
        public OportunidadPreriquisitosBeneficiosSolucionesCompuestoDTO DatosCompuesto { get; set; }
        public ComprobantePagoOportunidadDTO ComprobantePago { get; set; }
        public CalidadLlamadaDTO CalidadLlamada { get; set; }
        public string Usuario { get; set; }
        public int IdFaseOportunidad { get; set; }
        public string tipoProgramacion { get; set; }//manual//automatica
    }

    public class ParametroFinalizarActividadAlternoDTO
    {
        public DatosFiltroFinalizarActividadDTO filtro { get; set; }
        public OportunidadDTO datosOportunidad { get; set; }
        public ActividadDetalleDTO ActividadAntigua { get; set; }
        public OportunidadPreriquisitosBeneficiosSolucionesCompuestoAlternoDTO DatosCompuesto { get; set; }
        public ComprobantePagoOportunidadDTO ComprobantePago { get; set; }
        public CalidadLlamadaDTO CalidadLlamada { get; set; }
        public string Usuario { get; set; }
        public int IdFaseOportunidad { get; set; }
        public string tipoProgramacion { get; set; }//manual//automatica
    }
}
