using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class CajaEgresoAprobadoBO : BaseBO
    {
        public int IdCaja { get; set; }
        public string CodigoRec { get; set; }
        public string Anho { get; set; }
        public string Detalle { get; set; }
        public string Observacion { get; set; }
        public string Origen { get; set; }
        public DateTime FechaCreacionRegistro { get; set; }        
        public int? IdMigracion { get; set; }

        public byte[] GenerarPDFReciboEgresoCaja(CajaEgresoGenerarPdfDTO datosGenerarPdf)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 72f, 65f, 70f, 65f);
                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, ms);
                doc.AddTitle("Recibo" + datosGenerarPdf.CodigoEgresoCaja);
                doc.AddCreator("BS grupo");
                doc.Open();
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font ForLine = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont_normal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                PdfPTable TablaTop = new PdfPTable(3);
                TablaTop.WidthPercentage = 100;
                TablaTop.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths = new float[] { 25f, 46.5f, 28.5f };
                TablaTop.SetWidths(widths);
                PdfPCell SeccionEmpresa = new PdfPCell(new Phrase(datosGenerarPdf.RazonSocial, _standardFont));
                SeccionEmpresa.BorderWidth = 1;
                SeccionEmpresa.BackgroundColor = BaseColor.LIGHT_GRAY;
                PdfPCell SeccionTitulo = new PdfPCell(new Phrase("              RECIBO DE EGRESOS EFECTIVO", _standardFont));
                SeccionTitulo.BorderWidth = 0;
                PdfPCell SeccioNumeroRecibo = new PdfPCell(new Phrase(datosGenerarPdf.CodigoEgresoCaja, _standardFont));
                SeccioNumeroRecibo.BorderWidth = 1;
                SeccioNumeroRecibo.BackgroundColor = BaseColor.LIGHT_GRAY;
                TablaTop.AddCell(SeccionEmpresa);
                TablaTop.AddCell(SeccionTitulo);
                TablaTop.AddCell(SeccioNumeroRecibo);
                doc.Add(TablaTop);
                //Segunda 
                Paragraph paDir = new Paragraph("Dirección: " + datosGenerarPdf.Direccion, _standardFont_normal); paDir.SpacingBefore = 10f; doc.Add(paDir);
                doc.Add(new Paragraph("Ruc: " + datosGenerarPdf.Ruc, _standardFont_normal));
                Paragraph paCentral = new Paragraph("Central: " + datosGenerarPdf.Central, _standardFont_normal); paCentral.SpacingAfter = 8f; doc.Add(paCentral);
                //Tercera seccion
                PdfPTable TablaBottom = new PdfPTable(2);
                TablaBottom.DefaultCell.Padding = 10f;
                //TablaBottom.DefaultCell.CellEvent = new CellSpacingEvent(20);
                TablaBottom.WidthPercentage = 80;
                TablaBottom.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths2 = new float[] { 26f, 78f };
                TablaBottom.SetWidths(widths2);
                PdfPCell SeccionCuentaTitulo = new PdfPCell(new Phrase("Cuenta", _standardFont2));
                SeccionCuentaTitulo.PaddingTop = 6f;
                SeccionCuentaTitulo.BorderWidthTop = 1;
                SeccionCuentaTitulo.BorderWidth = 0;
                PdfPCell SeccionCuentaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.NumeroCuenta, _standardFont_normal));
                SeccionCuentaValor.BorderWidth = 0;
                SeccionCuentaValor.PaddingTop = 6f;
                SeccionCuentaValor.BorderWidthTop = 1;
                PdfPCell SeccionFechaTitulo = new PdfPCell(new Phrase("Fecha Pago", _standardFont2));
                SeccionFechaTitulo.BorderWidth = 0;
                PdfPCell SeccionFechaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.FechaGeneracionREC, _standardFont_normal));
                SeccionFechaValor.BorderWidth = 0;
                PdfPCell SeccionNroFurTitulo = new PdfPCell(new Phrase("Nro Furs", _standardFont2));
                SeccionNroFurTitulo.BorderWidth = 0;
                PdfPCell SeccionNroFurValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.CodigoFur, _standardFont_normal));
                SeccionNroFurValor.BorderWidth = 0;
                PdfPCell SeccionSeentregoaTitulo = new PdfPCell(new Phrase("Se entregó a", _standardFont2));
                SeccionSeentregoaTitulo.BorderWidth = 0;
                PdfPCell SeccionSeentregoaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.EntregadoA, _standardFont_normal));
                SeccionSeentregoaValor.BorderWidth = 0;
                //////////////
                PdfPCell SeccionProveedorTitulo = new PdfPCell(new Phrase("Proveedor", _standardFont2));
                SeccionProveedorTitulo.BorderWidth = 0;
                PdfPCell SeccionProveedorValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.NombreProveedor, _standardFont_normal));
                SeccionProveedorValor.BorderWidth = 0;
                PdfPCell SeccionRucProveedorTitulo = new PdfPCell(new Phrase("RUC", _standardFont2));
                SeccionRucProveedorTitulo.BorderWidth = 0;
                PdfPCell SeccionRucProveedorValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.RucProveedor, _standardFont_normal));
                SeccionRucProveedorValor.BorderWidth = 0;
                ///////////////////
                PdfPCell SeccionTipoDocumentosTitulo = new PdfPCell(new Phrase("T. Documento", _standardFont2));
                SeccionTipoDocumentosTitulo.BorderWidth = 0;
                PdfPCell SeccionTipoDocumentosValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.TipoDocumentosSunat, _standardFont_normal));
                SeccionTipoDocumentosValor.BorderWidth = 0;
                PdfPCell SeccionComprobantesrTitulo = new PdfPCell(new Phrase("N° Comprobante", _standardFont2));
                SeccionComprobantesrTitulo.BorderWidth = 0;
                PdfPCell SeccionComprobantesrValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.Comprobantes, _standardFont_normal));
                SeccionComprobantesrValor.BorderWidth = 0;


                                                          
                PdfPCell SeccionImporteTitulo = new PdfPCell(new Phrase("Importe", _standardFont2));
                SeccionImporteTitulo.BorderWidth = 0;
                PdfPCell SeccionImporteValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.MontoTotal + "   " + datosGenerarPdf.Moneda, _standardFont_normal));
                SeccionImporteValor.BorderWidth = 0;
                PdfPCell SeccionDetalleTitulo = new PdfPCell(new Phrase("Concepto", _standardFont2));
                SeccionDetalleTitulo.PaddingBottom = 43f;
                SeccionDetalleTitulo.BorderWidth = 0;
                PdfPCell SeccionDetalleValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.Detalle, _standardFont_normal));
                SeccionDetalleTitulo.PaddingBottom = 43f;
                SeccionDetalleValor.BorderWidth = 0;
                TablaBottom.AddCell(SeccionCuentaTitulo);
                TablaBottom.AddCell(SeccionCuentaValor);
                TablaBottom.AddCell(SeccionFechaTitulo);
                TablaBottom.AddCell(SeccionFechaValor);
                TablaBottom.AddCell(SeccionNroFurTitulo);
                TablaBottom.AddCell(SeccionNroFurValor);
                TablaBottom.AddCell(SeccionSeentregoaTitulo);
                TablaBottom.AddCell(SeccionSeentregoaValor);

                TablaBottom.AddCell(SeccionProveedorTitulo);
                TablaBottom.AddCell(SeccionProveedorValor);
                TablaBottom.AddCell(SeccionRucProveedorTitulo);
                TablaBottom.AddCell(SeccionRucProveedorValor);

                TablaBottom.AddCell(SeccionTipoDocumentosTitulo);
                TablaBottom.AddCell(SeccionTipoDocumentosValor);
                TablaBottom.AddCell(SeccionComprobantesrTitulo);
                TablaBottom.AddCell(SeccionComprobantesrValor);



                TablaBottom.AddCell(SeccionImporteTitulo);
                TablaBottom.AddCell(SeccionImporteValor);
                TablaBottom.AddCell(SeccionDetalleTitulo);
                TablaBottom.AddCell(SeccionDetalleValor);
                doc.Add(TablaBottom);
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new iTextSharp.text.Paragraph("  ________________________                             ________________________", _standardFont2));
                doc.Add(new iTextSharp.text.Paragraph("     ENTREGUE CONFORME                                       RECIBI CONFORME", _standardFont2));
                PdfPTable TablaBottom2 = new PdfPTable(2);
                TablaBottom2.WidthPercentage = 100;
                TablaBottom2.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths3 = new float[] { 48, 60 };
                TablaBottom2.SetWidths(widths3);
                PdfPCell SeccionCuentaTitulo2 = new PdfPCell(new Phrase(datosGenerarPdf.Responsable, _standardFont_normal));
                SeccionCuentaTitulo2.PaddingTop = 10f;
                SeccionCuentaTitulo2.BorderWidth = 0;
                PdfPCell SeccionCuentaValor2 = new PdfPCell(new Phrase("              " + "________________________", _standardFont_normal));
                SeccionCuentaValor2.BorderWidth = 0;
                SeccionCuentaValor2.PaddingTop = 10f;
                PdfPCell SeccionNroFurTitulo2 = new PdfPCell(new Phrase("DNI: ", _standardFont_normal));
                SeccionNroFurTitulo2.BorderWidth = 0;
                PdfPCell SeccionNroFurValor2 = new PdfPCell(new Phrase("              DNI: ", _standardFont_normal));
                SeccionNroFurValor2.BorderWidth = 0;
                TablaBottom2.AddCell(SeccionCuentaTitulo2);
                TablaBottom2.AddCell(SeccionCuentaValor2);
                TablaBottom2.AddCell(SeccionNroFurTitulo2);
                TablaBottom2.AddCell(SeccionNroFurValor2);
                doc.Add(TablaBottom2);
                doc.Close();
                writer.Close();
                doc.Dispose();

                return ms.ToArray();
            }
        }

    }
}
