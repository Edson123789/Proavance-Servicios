using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Planificacion
{
    public class ReporteRevisionDocenteDTO
    {
        public List<int> ListaArea { get; set; }
        public List<int> ListaSubArea { get; set; }
        public List<int> ListaProgramaGeneral { get; set; }
        public List<int> ListaDocente { get; set; }
        public int? IdCategoriaRevision { get; set; }
    }
    public class RespuestaReporteRevisionDocenteDTO
    {
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdPGeneral { get; set; }
        public int? IdCategoriaRevision { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdModalidadCurso { get; set; }
        public string Area { get; set; }
        public string SubArea { get; set; }
        public string PGeneral { get; set; }
        public string CategoriaRevision { get; set; }
        public string Nombre { get; set; }
        public string PersonalAsignado { get; set; }
        public string ModalidadCurso { get; set; }
    }
}
