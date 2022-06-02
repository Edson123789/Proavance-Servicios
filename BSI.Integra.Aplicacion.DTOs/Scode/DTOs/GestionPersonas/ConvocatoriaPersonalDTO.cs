using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class ConvocatoriaPersonalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreProcesoSeleccion { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Codigo { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string CuerpoConvocatoria { get; set; }
        public string Usuario { get; set; }
        public int? IdSedeTrabajo { get; set; }
        public string SedeTrabajo { get; set; }
        public int? IdArea { get; set; }
        public string Area { get; set; }
        public int? IdPersonal { get; set; }
        public string PersonalEncargado { get; set; }
        public string UrlAviso { get; set; }
        public bool? Activo { get; set; }

    }

    public class ConvocatoriaPersonalCodificadoDTO
    {        
        public string DatosCodificados { get; set; }
    }
}
