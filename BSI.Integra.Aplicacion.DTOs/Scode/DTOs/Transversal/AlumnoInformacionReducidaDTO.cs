using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoInformacionReducidaDTO
    {
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int? IdCodigoPais { get; set; }
    }

    public class EnvioSMSOportunidad
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
    }
}
