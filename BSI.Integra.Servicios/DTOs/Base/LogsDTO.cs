using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.DTOs
{
    public class LogsDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Ip { get; set; }
        public string Usuario { get; set; }
        public string Maquina { get; set; }
        public string Ruta { get; set; }
        public string Parametros { get; set; }
        public string Mensaje { get; set; }
        public string Excepcion { get; set; }
        public string Tipo { get; set; }
        public int IdPadre { get; set; }
    }
}
