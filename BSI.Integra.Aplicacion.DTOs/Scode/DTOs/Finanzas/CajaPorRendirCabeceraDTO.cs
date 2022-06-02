using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CajaPorRendirCabeceraDTO
    {
        public int Id { get; set; }
        public int IdCaja { get; set; }
        public string Codigo { get; set; }
        public int IdPersonalAprobacion { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public bool EsRendido { get; set; }
        public decimal MontoDevolucion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
