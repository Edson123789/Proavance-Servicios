using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosAlumnoEnvioCertificadoDTO
    {
        public string CodigoMatricula { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Genero { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdCodigoPais { get; set; }
        public int IdCiudad { get; set; }
        public string UrlFoto { get; set; }
        public string NombreCentroCosto { get; set; }
        public string UrlDocumento { get; set; }
        public string Trato { get; set; }
    }
}
