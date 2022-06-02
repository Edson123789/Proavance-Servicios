using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteOportunidadDetalleDTO
    {
        public List<OportunidadVentaCruzadaDTO> listaOportunidadVentaCruzada { get; set; }
        public AlumnoInformacionDTO datosAlumno { get; set; }
        public ProgramaGeneralPreBenCompuestoDTO ProgramaGeneralPreBen { get; set; }
        public List<OportunidadProblemaClienteDTO> ListaProblemaCliente { get; set; }
        public ReporteSeguimientoOportunidadComplementosDTO OportunidadComplementos { get; set; }
        public SueldosDescripcionDTO probabilidadsueldo { get; set; }

    }
}
