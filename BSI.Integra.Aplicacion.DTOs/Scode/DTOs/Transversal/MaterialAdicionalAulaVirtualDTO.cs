using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MaterialAdicionalAulaVirtualDTO
    {
        public int Id { get; set; }
        public string NombreConfiguracion { get; set; }
        public int IdPgeneral { get; set; }
        public bool? EsOnline { get; set; }
        public string Usuario { get; set; }
    }

    public class MaterialAdicionalAulaVirtualPGeneralDTO
    {
        public int Id { get; set; }
        public string NombreConfiguracion { get; set; }
        public string Nombre { get; set; }
        public int IdPgeneral { get; set; }
    }

    public class MaterialAdicionalAulaVirtualPespecificoDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int? IdMaterialAdicionalAulaVirtual { get; set; }
        public string Usuario { get; set; }
    }

    public class MaterialAdicionalAulaVirtualRegistroDTO
    {
        public int Id { get; set; }
        public int IdMaterialAdicionalAulaVirtual { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public bool EsEnlace { get; set; }
        public string Usuario { get; set; }
    }

    public class InsertarMaterialAdicionalAulaVirtualRegistroDTO
    {
        public int Id { get; set; }
        public string NombreConfiguracion { get; set; }
        public int IdPgeneral { get; set; }
        public bool? EsOnline { get; set; }
        public List<int> ProgramaEspecifico { get; set; }
        public List<MaterialAdicionalAulaVirtualRegistroDTO> MaterialAdicional { get; set; }
        public string Usuario { get; set; }
    }

    public class DatosMaterialAdicionalAulaVirtualDTO
    {
        public MaterialAdicionalAulaVirtualDTO MaterialAdicional { get; set; }
        public List<int> ProgramaEspecifico { get; set; }
        public List<MaterialAdicionalAulaVirtualRegistroDTO> MaterialAdicionalRegistro { get; set; }
        public string Usuario { get; set; }
    }
}
