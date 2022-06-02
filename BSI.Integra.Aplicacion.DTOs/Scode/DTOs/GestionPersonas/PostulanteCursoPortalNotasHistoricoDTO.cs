using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class PostulanteCursoPortalNotasHistoricoDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public int? OrdenFilaSesion { get; set; }
        public string GrupoPregunta { get; set; }
        public decimal? Calificacion { get; set; }
        public string IdUsuario { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPespecifico { get; set; }
        public bool? AccesoPrueba { get; set; }
        public int? IdMigracion { get; set; }
    }
    public class PostulanteVideoVisualizacionDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int? IdPrincipal { get; set; }
        public string IdUsuario { get; set; }
    }
}
