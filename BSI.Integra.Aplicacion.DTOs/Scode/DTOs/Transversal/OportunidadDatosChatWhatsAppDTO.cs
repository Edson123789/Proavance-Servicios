using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadDatosChatWhatsAppDTO
    {
        public int? IdOportunidad { get; set; }
        public int? IdContacto { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int? IdCodigoPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdEmpresa { get; set; }
        public string Codigo { get; set; }
        public string Asesor { get; set; }
        public bool? EnSeguimiento { get; set; }
        public string NombreCentroCosto { get; set; }
    }
}
