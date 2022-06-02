using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class IdDTO
    {
        public int Id { get; set; }
    }

    public class IdDTOV2
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
    }

    public class IdOportunidadDTO
    {
        public int IdCentroCosto { get; set; }
        public int IdAlumno { get; set; }
        public string CodigoAlumno { get; set; }
    }

    public class IdDTOV3
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
    }

    public class CampaniaDetalleOportunidadDTO
    {
        public string CodMailing { get; set; }
        public int? IdCentroCosto { get; set; }
        public int IdPersonal { get; set; }
    }
}
