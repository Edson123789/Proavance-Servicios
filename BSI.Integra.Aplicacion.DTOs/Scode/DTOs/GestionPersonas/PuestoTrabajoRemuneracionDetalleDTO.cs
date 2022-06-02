using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class PuestoTrabajoRemuneracionDetalleDTO
    {
        public int Id { get; set; }
        public int? IdPuestoTrabajoRemuneracion { get; set; }
        public int? IdRemuneracion { get; set; }
        public int? IdTipoRemuneracion { get; set; }
        public int? IdClaseRemuneracion { get; set; }
        public int? IdPeriodoRemuneracion { get; set; }
        public bool? Tasa { get; set; }
        public decimal? Monto { get; set; }
        public int? IdMoneda { get; set; }
        public decimal? PorcentajeTasa { get; set; }
        public string DescripcionEquipo { get; set; }
        public bool? TieneCondicion { get; set; }
        public int? IdDescripcionMonetaria { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public int? IdMonedaValorVariable { get; set; }
        public decimal? IngresoMensual { get; set; }
    }
}
