using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 

   public class PGeneralFiltroDTO
    {

        public int Id { get; set; }
        public string Nombre { get; set; }

    }

    public class PGeneralSubAreaFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
    }

    public class ProgramaGeneralSubAreaFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSubArea { get; set; }
    }

    public class PEspecificoProgramaGeneralFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPGeneral{ get; set; }
    }

    public class PEspecificoProgramaGeneralFiltroV2DTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdEstadoPEspecifico { get; set; }
        public int? IdModalidad { get; set; }
        public int IdCodigoBSCiudad { get; set; }
    }

    public class CentroCostoProgramaEspecificoFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPEspecifico { get; set; }
    }
    public class PartnerExtraFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

    }
}


