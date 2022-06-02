using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralMaterialEstudioAdicionalBO : BaseBO
    {
        public int IdPGeneral { get; set; }
        public string NombreArchivo { get; set; }
        public string EnlaceArchivo { get; set; }
        public bool EsEnlace { get; set; }
    }

    public class ListaProgramaGeneralMaterialEstudioAdicionalBO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
    }

    public class RegistroProgramaGeneralMaterialEstudioAdicionalBO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreArchivo { get; set; }
        public string EnlaceArchivo { get; set; }
        public bool EsEnlace { get; set; }
    }

    public class datosMaterialAdicionalBO
    {
        public List<RegistroProgramaGeneralMaterialEstudioAdicionalBO> MaterialAdicional { get; set; }
        public List<int> listaEspecifico { get; set; }
    }
}
