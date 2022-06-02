using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml.html.table;
using iTextSharp.text.html.simpleparser;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class DatoContratoPersonalBO:BaseBO
    {
        public int IdPersonal { get; set; }
        public int IdTipoContrato { get; set; }
        public bool EstadoContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal RemuneracionFija { get; set; }
        public int? IdTipoPagoRemuneracion { get; set; }
        public int? IdEntidadFinancieraPago { get; set; }
        public string NumeroCuentaPago { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdSedeTrabajo { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int IdCargo { get; set; }
        public int? IdTipoPerfil { get; set; }
        public int? IdPersonalJefe { get; set; }
        public int? IdEntidadFinancieraCts { get; set; }
        public string NumeroCuentaCts { get; set; }
        public bool? EsPeridoPrueba { get; set; }
        public DateTime? FechaFinPeriodoPrueba { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdContratoEstado { get; set; }
        public string UrlDocumentoContrato { get; set; }

        public byte[] GenerarPDFDatoContratoPersonal(string datosControPersonal)
        {
            StringReader sr = new StringReader(datosControPersonal.ToString());

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            pdfDoc.SetMargins(50f, 50f, 50f, 50f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                htmlparser.Parse(sr);
                pdfDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                return bytes;
            }
            
        }
    }
}
