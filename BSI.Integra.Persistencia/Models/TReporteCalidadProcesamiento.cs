using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TReporteCalidadProcesamiento
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPersonal { get; set; }
        public int IdFaseOportunidad { get; set; }
        public decimal PromedioPerfil { get; set; }
        public decimal PromedioHistorialFinanciero { get; set; }
        public decimal PromedioPgeneral { get; set; }
        public decimal PromedioPespecifico { get; set; }
        public decimal PromedioBeneficios { get; set; }
        public decimal PromedioCompetidores { get; set; }
        public decimal PromedioProblemaSeleccionados { get; set; }
        public decimal PromedioProblemaSolucionados { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
