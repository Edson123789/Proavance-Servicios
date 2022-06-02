using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Modalidad { get; set; }
        public string Codigo { get; set; }
        public bool? ConsiderarEnvioAutomatico { get; set; }
        public string TipoPersonal { get; set; }
    }
    public class ValorFiltroDTO
    {
        public string Valor { get; set; }
    }
    public class FiltroBasicoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class FiltroConfiguracionCoordinadoraEstadoMatriculaDTO
    {
        public int IdEstadoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
    }
    public class MatriculaInHouseDTO
    {
        public string CodigoMatricula { get; set; }
        public string Cuota { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaMatricula { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
