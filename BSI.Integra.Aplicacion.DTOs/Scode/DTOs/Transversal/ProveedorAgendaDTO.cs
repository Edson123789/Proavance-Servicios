using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProveedorAgendaDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string TelfCelular1 { get; set; }
        public string TelfCelular2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string NroDocumento { get; set; }
        public string Domicilio { get; set; }
        public int? IdPaisDomicilio { get; set; }
        public int? IdCiudadDomicilio { get; set; }
        public string NombrePaisDomicilio { get; set; }
        public string NombreCiudadDomicilio { get; set; }
        public string Usuario { get; set; }
        public string Alias { get; set; }
    }
}
