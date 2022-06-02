using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CertificadoTipoProgramaDTO
    {
        public int Id { get; set; }
        public string NombreProgramaCertificado { get; set; }
        public string Codigo { get; set; }
        public bool AplicaFondoDiploma { get; set; }
        public bool AplicaSeOtorga { get; set; }
        public bool AplicaNota { get; set; }
    }
}
