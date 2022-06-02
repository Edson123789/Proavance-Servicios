using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Comercial
{
    public class SemaforoFinancieroDTO
    {
        public int Id { get; set; }
        public int IdPais { get; set; }
        public bool Activo { get; set; }
        public string Usuario { get; set; }
        public List<SemaforoFinancieroDetalleDTO> Detalle { get; set; }
    }
    public class SemaforoFinancieroDetalleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Mensaje { get; set; }
        public string Color { get; set; }
    }
    public class SemaforoFinancieroDetalleVariableDTO
    {
        public int? Id { get; set; }
        public int? IdSemaforoFinancieroDetalle { get; set; }
        public int? IdSemaforoFinancieroVariable { get; set; }
        public string Variable { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public int? IdMoneda { get; set; }
        public string Unidad { get; set; }
        public bool? AplicaUnidad { get; set; }
    }
    public class SemaforoFinancieroDetalleV2DTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Mensaje { get; set; }
        public string Color { get; set; }
        public string Usuario { get; set; }
        public int Actualizar { get; set; }
        public List<SemaforoFinancieroDetalleVariableDTO> Variable { get; set; }
    }
}
