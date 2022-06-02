using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataNaturalNacionalBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public string NroDocumento { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NombreCompleto { get; set; }
        public bool? Validada { get; set; }
        public bool? Rut { get; set; }
        public string Genero { get; set; }
        public string IdentificacionEstado { get; set; }
        public DateTime? IdentificacionFechaExpedicion { get; set; }
        public string IdentificacionCiudad { get; set; }
        public string IdentificacionDepartamento { get; set; }
        public string IdentificacionNumero { get; set; }
        public int? EdadMinima { get; set; }
        public int? EdadMaxima { get; set; }
    }
}
