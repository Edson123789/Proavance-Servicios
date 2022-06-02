using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CiudadDTO
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdPais { get; set; }
        public int LongCelular { get; set; }
        public int LongTelefono { get; set; }
        public string Usuario { get; set; }
    }

    public class CiudadMultipleDTO
    {
        public int LongitudCelular { get; set; }
        public int LongitudTelefono { get; set; }
        public int IdPais { get; set; }
        public List<int> ListaRegiones { get; set; }
        public string Usuario { get; set; }
    }
}
