using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MaterialEstudioAdicionalDTO
    {
        public int IdPGeneral { get; set; }
        public string Usuario { get; set; }
        public List<ProgramaGeneralMaterialEstudioAdicionalDTO> MaterialRegistro { get; set; }
        public List<ProgramaGeneralMaterialEstudioAdicionalDTO> MaterialEliminado { get; set; }
        public List<int> ProgramaEspecifico { get; set; }
    }

    public class ProgramaGeneralMaterialEstudioAdicionalDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreArchivo { get; set; }
        public string EnlaceArchivo { get; set; }
        public bool EsEnlace { get; set; }
        public bool Estado { get; set; }
    }

    public class EliminarMaterialEstudioAdicionalDTO
    {
        public int IdPGeneral { get; set; }
        public string Usuario { get; set; }
        public bool Estado { get; set; }
    }

    public class MaterialEstudioAdicionalPEspecificoDTO
    {
        //public int IdProgramaGeneralMaterialEstudioAdicional { get; set; }
        public int IdPEspecifico { get; set; }
        //public string Usuario { get; set; }
        //public bool Estado { get; set; }
    }
}
