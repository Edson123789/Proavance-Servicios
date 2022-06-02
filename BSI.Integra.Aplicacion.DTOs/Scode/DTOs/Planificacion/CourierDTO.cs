using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CourierDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public int IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Url { get; set; }
        public string Usuario { get; set; }
    }

    public class CourierIdDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
    }
}

