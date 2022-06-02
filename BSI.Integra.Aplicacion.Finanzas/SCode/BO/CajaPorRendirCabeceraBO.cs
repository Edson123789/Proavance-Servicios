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
    public class CajaPorRendirCabeceraBO : BaseBO
    {
        public int IdCaja { get; set; }
        public string Codigo { get; set; }
        public int Anho { get; set; }
        public int IdPersonalAprobacion { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public bool EsRendido { get; set; }
        public decimal MontoDevolucion { get; set; }
        public int? IdMigracion { get; set; }



        public byte[] GenerarPDFReciboCajaPorRendir(CajaPorRendirGenerarPdfDTO datosGenerarPdf)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 72f, 65f, 70f, 65f);
                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, ms);
                doc.AddTitle("Recibo" + datosGenerarPdf.CodigoPorRendir);
                doc.AddCreator("BS grupo");
                doc.Open();
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont_peque = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont_pequeBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
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
                PdfPCell SeccionTitulo = new PdfPCell(new Phrase("           EGRESOS POR REGULARIZAR", _standardFont));
                SeccionTitulo.BorderWidth = 0;
                PdfPCell SeccioNumeroRecibo = new PdfPCell(new Phrase(datosGenerarPdf.CodigoPorRendir, _standardFont));
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
                float[] widths2 = new float[] { 22f, 78f };
                TablaBottom.SetWidths(widths2);
                PdfPCell SeccionCuentaTitulo = new PdfPCell(new Phrase("Cuenta", _standardFont2));
                SeccionCuentaTitulo.PaddingTop = 6f;
                SeccionCuentaTitulo.BorderWidthTop = 1;
                SeccionCuentaTitulo.BorderWidth = 0;
                PdfPCell SeccionCuentaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.CuentaCaja, _standardFont_normal));
                SeccionCuentaValor.BorderWidth = 0;
                SeccionCuentaValor.PaddingTop = 6f;
                SeccionCuentaValor.BorderWidthTop = 1;
                PdfPCell SeccionFechaTitulo = new PdfPCell(new Phrase("Fecha Entrega Efectivo", _standardFont2));
                SeccionFechaTitulo.BorderWidth = 0;
                PdfPCell SeccionFechaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.FechaAprobacion, _standardFont_normal));
                SeccionFechaValor.BorderWidth = 0;
                PdfPCell SeccionNroFurTitulo = new PdfPCell(new Phrase("Nro Furs", _standardFont2));
                SeccionNroFurTitulo.BorderWidth = 0;
                PdfPCell SeccionNroFurValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.CodigoFur, _standardFont_normal));
                SeccionNroFurValor.BorderWidth = 0;
                PdfPCell SeccionSeentregoaTitulo = new PdfPCell(new Phrase("Se entregó a", _standardFont2));
                SeccionSeentregoaTitulo.BorderWidth = 0;
                PdfPCell SeccionSeentregoaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.EntregadoA, _standardFont_normal));
                SeccionSeentregoaValor.BorderWidth = 0;
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
                PdfPCell SeccionCuentaTitulo2 = new PdfPCell(new Phrase(datosGenerarPdf.PersonalResponsable, _standardFont_normal));
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
                doc.Add(new Paragraph("\n\n\n"));
                doc.Add(new Paragraph("AUTORIZACIÓN DE DESCUENTO POR PLANILLA DE HABERES", _standardFont2));
                Paragraph condiciones = new Paragraph("Yo _______________________________________________________, " +
                    "por la presente autorizo expresamente que se me descuente, de mi planilla de haberes del mes, la cantidad de "+ datosGenerarPdf.MontoTotal + " "+datosGenerarPdf.Moneda+", en el" +
                    " caso de no entregar la documentación sustentatoria de compra, a los 05 días calendarios de habérseme entregado el importe indicado, el dia de hoy "
                    +DateTime.Now+" , para lo cual he firmado en señal de conformidad el recibo.", _standardFont_normal);
                condiciones.SpacingBefore = 10f;
                condiciones.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(condiciones);

                Paragraph nota = new Paragraph();
                nota.Add(new Chunk("NOTA:", _standardFont_pequeBold));
                nota.Add(new Chunk("  Unicamente se aceptara Comprobantes que hayan sido emitidos dentro del período que se le asigno los gastos. " +
                    "Los comprobantes (Facturas, boletas, etc) deben ser aceptados tributariamente por SUNAT. Por ningún motivo debe ir el término \"POR CONSUMO\", siempre debe ir el detallado de la compra. " +
                    "El dinero entregado por viáticos son gastos específicos de movilidad y Alimentacion.", _standardFont_peque));
                nota.SpacingBefore = 8f;
                nota.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(nota);



                doc.Close();
                writer.Close();
                doc.Dispose();

                return ms.ToArray();
            }
        }

    }
}
