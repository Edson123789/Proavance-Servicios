using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ExpositorDocumentoDTO
    {
        public int  Id { get;set; }
        public string  Nombres { get;set; }
        public string  Apellidos { get;set; }
        public string  PrimerNombre { get;set; }
        public string  SegundoNombre { get;set; }
        public string  ApellidoPaterno { get;set; }
        public string  ApellidoMaterno { get;set; }
        public string  TelfCelular1 { get;set; }
        public string  TelfCelular2 { get;set; }
        public string  TelfCelular3 { get;set; }
        public string  NombrePais { get;set; }
        public string  NombreCiudad { get;set; }
        public string  TipoDocumento { get;set; }
        public string  NroDocumento { get;set; }
        public string  Email1 { get;set; }
        public string  Domicilio { get;set; }
        public string  HojaVidaResumidaPerfil { get;set; }
        public string  Email2 { get;set; }
        public string  Email3 { get; set; }
    }
}
