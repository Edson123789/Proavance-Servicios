using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InsertarCertificadoDTO
    {
        public int IdCertificadoDetalle { get; set; }
        public int? IdCertificadoBrochure { get; set; }
        public string CodigoMatricula { get; set; }
        public byte[] PdfFrontal { get; set; }
        public byte[] PdfReverso { get; set; }
        public byte[] PdfFrontalImpresion { get; set; }
        public byte[] PdfReversoImpresion { get; set; }
        public string Usuario { get; set; }
    }
}
