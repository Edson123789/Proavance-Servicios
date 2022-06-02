using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ValorIntDTO
    {
        public int Valor { get; set; }
    }
    public class AlumnoWhatsappDTO
    {
        public int IdAlumno{ get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
    }

    public class ValorOpcionalDecimalDTO
    {
        public decimal? Valor { get; set; }
    }
    public class ValorStringDTO
    {
        public string Valor { get; set; }
    }
    public class ValorBoolDTO
    {
        public bool Valor { get; set; }
    }
    public class ValorDecimalDTO
    {
        public decimal Valor { get; set; }
    }
    public class ValorDateTimeDTO
    {
        public DateTime Valor { get; set; }
    }
    public class ValorIdMatriculaDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public bool? Nuevo { get; set; }
    }

}
