using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegistroMarcadorFechaDTO
    {
        public int Id { get; set; }
        public int IdCiudad { get; set; }
        public int IdPersonal { get; set; }
        public string Pin { get; set; }
        public string Fecha { get; set; }
        public string M1 { get; set; }
        public string M2 { get; set; }
        public string M3 { get; set; }
        public string M4 { get; set; }
        public string M5 { get; set; }
        public string M6 { get; set; }
        public string Usuario { get; set; }

    }

    public class ListaRegistroMarcadorFechaDTO
    {
        public List<RegistroMarcadorFechaDTO> ListaRegistroMarcadorFecha { get; set; }
    }
}
