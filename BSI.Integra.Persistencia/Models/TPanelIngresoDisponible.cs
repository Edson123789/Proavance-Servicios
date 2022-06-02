using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPanelIngresoDisponible
    {
        public int Id { get; set; }
        public int? IdFormaPago { get; set; }
        public int DiasDeposito { get; set; }
        public int DiasDisponible { get; set; }
        public bool CuentaFeriados { get; set; }
        public bool ConsideraVsd { get; set; }
        public bool ConsideraDiasHabilesLunesSabado { get; set; }
        public bool ConsideraDiasHabilesLunesViernes { get; set; }
        public bool ConsideraDiasFijoSemana { get; set; }
        public int HoraCorte { get; set; }
        public int MinutoCorte { get; set; }
        public decimal PorcentajeCobro { get; set; }
        public bool CuentaFeriadosEstatales { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdDiaSemanaFijo { get; set; }
    }
}
