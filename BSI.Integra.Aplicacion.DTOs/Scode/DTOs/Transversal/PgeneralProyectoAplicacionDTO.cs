using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralProyectoAplicacionDTO
    {
        public int Id { get; set; }
        public List<PgeneralProyectoAplicacionModalidadDTO> IdModalidadCurso { get; set; }
        public List<PgeneralProyectoAplicacionProveedorDTO> IdProveedor { get; set; }
    }
    public class PGeneralForoAsignacionProveedorDTO
    {
        public int IdModalidadCurso { get; set; }
        public List<int> IdProveedor { get; set; }
    }
    public class ProveedorEmailDTO
    {
        public List<int> IdModalidadCurso { get; set; }
        public int IdProveedor { get; set; }
    }
    public class PGeneralForoAsignacionProveedorAuxiliarDTO
    {
        public int IdPgeneral { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdProveedor { get; set; }
    }
}
