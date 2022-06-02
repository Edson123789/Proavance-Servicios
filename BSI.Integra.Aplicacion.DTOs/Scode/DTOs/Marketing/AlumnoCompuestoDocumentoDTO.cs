using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoCompuestoDocumentoDTO
    {
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public int? IdCodigoPais { get; set; }
        public string NombrePais { get; set; }
        public int? IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        //Adicionales
        public string Paquete { get; set; }
        public int? IdOportunidad { get; set; }
        public string Correo { get; set; }
    }
}
