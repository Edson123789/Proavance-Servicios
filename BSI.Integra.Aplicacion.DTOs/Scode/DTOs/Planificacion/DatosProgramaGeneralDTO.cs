using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosProgramaGeneralDTO
    {
        public PgeneralDTO ProgramaGeneral { get; set; }
        public DetallesProgramasDTO DetallesProgramaGeneral { get; set; }
        //public List<int> Modalidades { get; set; }
        public string Usuario {get;set;}

    }
    public class CorreoProveedorPersonalDTO
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string ProveedorEmail { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public string PersonalEmail { get; set; }
        public string UrlFirmaPersonal { get; set; }
    }
}
