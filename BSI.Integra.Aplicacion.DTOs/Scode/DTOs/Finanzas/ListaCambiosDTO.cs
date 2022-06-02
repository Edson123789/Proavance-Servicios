using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ListaCambiosDTO
    {
        public string id { get; set; }
        public string TipoCambio { get; set; }
        public int Orden { get; set; }
        public int Cuota { get; set; }
        public int SubCuota { get; set; }
    }
    public class ListaCambiosPorPeriodoDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public float MontoCambio { get; set; }
        public string TipoModificacion { get; set; }
        public string Periodo { get; set; }
    }
    public class ListaCambiosCSVPorPeriodoDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public float MontoCambio { get; set; }
        public string TipoModificacion { get; set; }
        public string Periodo { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion{ get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
