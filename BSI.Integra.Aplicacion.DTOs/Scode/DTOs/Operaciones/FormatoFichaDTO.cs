using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FormatoFichaDTO
    {
            public string NombrePEspecifico { get; set; }
            public string CodigoAlumno { get; set; }
            public string Alumno { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Nombre1 { get; set; }
            public string Nombre2 { get; set; }
            public string NroDocumento { get; set; }
            public DateTime? FechaNacimiento { get; set; }
            public string LugarNacimiento { get; set; }
            public int? Edad { get; set; }
            public string EstadoCivil { get; set; }
            public string Ciudad { get; set; }
            public string Pais { get; set; }
            public string Direccion { get; set; }
            public string TelefonoDomiciliario { get; set; }
            public string Celular { get; set; }
            public string Email { get; set; }
            public string Nombres { get; set; }
            public List<TotalCuotaDTO> ListadoCuotas { get; set; }
    }
}
