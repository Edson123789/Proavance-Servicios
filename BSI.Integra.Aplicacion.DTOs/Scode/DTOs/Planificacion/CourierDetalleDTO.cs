using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CourierDetalleDTO
    {
        public int Id { get; set; }
        public int IdCourier { get; set; }
        public string Courier { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public int IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int TiempoEnvio { get; set; }
        public string Usuario { get; set; }
    }
    public class ReporteCourierDetalleDTO
    {
        public int Id { get; set; }
        public int IdCourier { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int TiempoEnvio { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
    }
}
