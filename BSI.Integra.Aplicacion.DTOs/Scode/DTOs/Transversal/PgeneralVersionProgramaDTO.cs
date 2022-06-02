using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralVersionProgramaDTO
    {
        public int? Id { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdVersionPrograma { get; set; }
        public int? Duracion { get; set; }
    }

    public class PgeneralVersionesAsociadasDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class ListaPgeneralVersionProgramaDTO
    {
        public int? Id { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPgeneralVersionPrograma { get; set; }
        public int? IdVersionPrograma { get; set; }
        public string NombreVersion { get; set; }
        public int? Duracion { get; set; }
    }
}
