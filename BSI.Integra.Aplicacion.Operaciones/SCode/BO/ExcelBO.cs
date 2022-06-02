using BSI.Integra.Aplicacion.DTOs;
using Microsoft.AspNetCore.Hosting.Internal;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using static BSI.Integra.Aplicacion.Base.BO.Enum;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class ExcelBO
    {
        public byte[] ObtenerFormatoVerificacionUltimosDetalles()
        {
            try
            {
                MemoryStream ms = new MemoryStream();

                using (var package = new ExcelPackage(ms))
                {
                    // add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("RE-EJP-039");

                    worksheet.Cells.Style.Font.Name = "Arial";
                    worksheet.Cells.Style.Font.Size = 10;
                    //worksheet.Cells.AutoFitColumns();
                    //worksheet.PrinterSettings.Orientation = eOrientation.Landscape; //orientacion de la pagina
                    worksheet.PrinterSettings.PaperSize = ePaperSize.A4; //orientacion de la pagina

                    ///valores a considerar
                    int fila = 1, columna = 2;

                    //Color color_fondo = Color.FromArgb(31, 73, 125);  //azul oscuro
                    //Color color_fondo = Color.FromArgb(204, 255, 255);  //verde claro
                    Color color_fondo = Color.FromArgb(217, 217, 217);  //gris oscuro

                    //Color color_encabezado = Color.FromArgb(255, 255, 255); // blanco
                    Color color_encabezado = Color.FromArgb(0, 0, 0); //negro

                    #region Colores para la celda de evaluacion de aprobacion del curso
                    Color color_fondo_aprobado = Color.FromArgb(197, 217, 241); // azul claro
                    Color color_fondo_desaprobado = Color.FromArgb(255, 167, 167); //rojo claro
                    Color color_letra_aprobado = Color.FromArgb(31, 73, 125); // azul oscuro
                    Color color_letra_desaprobado = Color.FromArgb(255, 0, 0); //rojo oscuro
                    #endregion

                    ///////////////cabecera
                    /// añade la cabecera de SGC
                    /// 
                    //Image imagen_cabecera_sgc = Image.FromFile( a.ContentRootPath ApplicationPhysicalPath + "Content/img/EncabezadoSGC/" + "RE-EJP-039 Lista de verificación de últimos detalles.png");
                    //Image imagen_cabecera_sgc = Image.FromFile(new HostingEnvironment().WebRootPath + "Content/img/EncabezadoSGC/" + "RE-EJP-039 Lista de verificación de últimos detalles.png");
                    //ExcelPicture imagen_cabecera = worksheet.Drawings.AddPicture("cabecera", imagen_cabecera_sgc);
                    //imagen_cabecera.SetPosition(0, 0, 1, 0);
                    //imagen_cabecera.SetSize(85);

                    #region Encabezado
                    //primerafila
                    fila = 5;

                    //segunda fila
                    fila++;
                    worksheet.Cells[fila, columna].Value = "Responsable:";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    //tercera fila
                    fila++; fila++;
                    worksheet.Cells[fila, columna].Value = "Fecha:";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[fila, columna + 1].Value = DateTime.Now.ToShortDateString();
                    worksheet.Cells[fila, columna + 1, fila, columna + 2].Merge = true;

                    worksheet.Cells[fila, columna + 4].Value = "Horario:";
                    worksheet.Cells[fila, columna + 4].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna + 4, fila, columna + 5].Merge = true;

                    //cuarta fila
                    fila++; fila++;
                    columna = 2;
                    for (int i = 0; i < 9; i++)
                    {
                        columna++;
                        worksheet.Cells[fila, columna].Value = "AULA";
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    fila++;
                    columna = 2;
                    Border bordes_aula = worksheet.Cells[fila - 1, columna + 1, fila, columna + 9].Style.Border;
                    bordes_aula.Bottom.Style = bordes_aula.Top.Style = bordes_aula.Left.Style = bordes_aula.Right.Style = ExcelBorderStyle.Thin;

                    #endregion

                    columna = 2;

                    #region Fila - Puesta de Equipos
                    // encabezado
                    fila++;
                    worksheet.Cells[fila, columna].Value = "Puesta de Equipos y Presentación";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;

                    //data
                    fila++;
                    worksheet.Cells[fila, columna].Value = "Cañon y control";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Computadora para Profesor";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Parlantes (si requiere)";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Presentación Digital";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Operatividad computadoras laboratorio";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Otros:";

                    // bordes
                    Border bordes_puesta_equipos = worksheet.Cells[fila - 5, columna, fila, columna + 9].Style.Border;
                    bordes_puesta_equipos.Bottom.Style = bordes_puesta_equipos.Top.Style = bordes_puesta_equipos.Left.Style = bordes_puesta_equipos.Right.Style = ExcelBorderStyle.Thin;

                    #endregion

                    #region Fila - Orden y Limpieza del Salón
                    // encabezado
                    fila++; fila++;
                    worksheet.Cells[fila, columna].Value = "Orden y Limpieza del Salón";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;

                    //data
                    fila++;
                    worksheet.Cells[fila, columna].Value = "Piso y Paredes";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Carpetas";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Pizarra";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Vasos y Jarras";


                    fila++;
                    worksheet.Cells[fila, columna].Value = "Otros:";

                    // bordes
                    Border bordes_orden_salon = worksheet.Cells[fila - 4, columna, fila, columna + 9].Style.Border;
                    bordes_orden_salon.Bottom.Style = bordes_orden_salon.Top.Style = bordes_orden_salon.Left.Style = bordes_orden_salon.Right.Style = ExcelBorderStyle.Thin;

                    #endregion

                    #region Fila - Orden y Limpieza de Servicios Higiénicos
                    // encabezado
                    fila++; fila++;
                    worksheet.Cells[fila, columna].Value = "Orden y Limpieza de Servicios Higiénicos";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;

                    //data
                    fila++;
                    worksheet.Cells[fila, columna].Value = "Limpieza del Baño";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Doodorización baño y pasadizos";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Papel Higiénico y Papel Toalla";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Jabón Líquido";

                    // bordes
                    Border bordes_orden_sshh = worksheet.Cells[fila - 3, columna, fila, columna + 9].Style.Border;
                    bordes_orden_sshh.Bottom.Style = bordes_orden_sshh.Top.Style = bordes_orden_sshh.Left.Style = bordes_orden_sshh.Right.Style = ExcelBorderStyle.Thin;

                    #endregion

                    #region Fila - Puesta de Utiles de Escritorio
                    // encabezado
                    fila++; fila++;
                    worksheet.Cells[fila, columna].Value = "Puesta de Equipos y Presentación";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;

                    //data
                    fila++;
                    worksheet.Cells[fila, columna].Value = "Plumones";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Mota";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Puntero";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Carpeta para Profesor";

                    fila++;
                    worksheet.Cells[fila, columna].Value = "Otros:";

                    // bordes
                    Border bordes_puesta_utiles = worksheet.Cells[fila - 4, columna, fila, columna + 9].Style.Border;
                    bordes_puesta_utiles.Bottom.Style = bordes_puesta_utiles.Top.Style = bordes_puesta_utiles.Left.Style = bordes_puesta_utiles.Right.Style = ExcelBorderStyle.Thin;

                    #endregion

                    #region Fila - Observaciones
                    //fila observaciones
                    fila++; fila++;
                    worksheet.Row(fila).Height = 25;
                    worksheet.Cells[fila, columna].Value = "Observaciones:";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;

                    for (int i = 0; i < 3; i++)
                    {
                        fila++;
                        worksheet.Row(fila).Height = 25;
                    }

                    //bordes
                    Border bordes_observaciones = worksheet.Cells[fila - 3, columna, fila, columna + 9].Style.Border;
                    bordes_observaciones.Bottom.Style = ExcelBorderStyle.Thin;

                    Border bordes_observaciones_complemento = worksheet.Cells[fila - 3, columna, fila - 3, columna].Style.Border;
                    bordes_observaciones_complemento.Bottom.Style = ExcelBorderStyle.None;
                    #endregion

                    #region Fila - Revisado por
                    //fila revisado por
                    fila++; fila++;
                    worksheet.Cells[fila, columna].Value = "Revisado por Coordinador(as) Académico(as):";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna, fila, columna + 6].Merge = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[fila, columna + 7].Value = "Fecha";
                    worksheet.Cells[fila, columna + 7].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna + 7, fila, columna + 9].Merge = true;
                    worksheet.Cells[fila, columna + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    fila += 3;

                    //bordes
                    Border bordes_revisado_por = worksheet.Cells[fila - 3, columna, fila, columna + 9].Style.Border;
                    bordes_revisado_por.Bottom.Style = bordes_revisado_por.Top.Style = bordes_revisado_por.Left.Style = bordes_revisado_por.Right.Style = ExcelBorderStyle.Thin;
                    #endregion

                    //dimensiones columnas
                    for (int i = 1; i <= worksheet.Dimension.Columns; i++)
                    {
                        worksheet.Column(i).AutoFit();
                    }
                    worksheet.Column(1).Width = 2;
                    for (int i = 3; i <= worksheet.Dimension.Columns; i++)
                    {
                        worksheet.Column(i).Width = 6;
                    }

                    //formatos fecha
                    //string formato_fecha = "dd/MM/yyyy";
                    //worksheet.Column(9).Style.Numberformat.Format = formato_fecha;

                    ////bordes
                    //Border bordes = worksheet.Cells[4, 1, fila, 7].Style.Border;
                    //bordes.BorderAround(ExcelBorderStyle.Thin);
                    //bordes.Bottom.Style = bordes.Top.Style = bordes.Left.Style = bordes.Right.Style = ExcelBorderStyle.Thin;

                    package.Save();
                }

                byte[] excel = ms.ToArray();

                ms.Close();

                return excel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public byte[] ObtenerEmbudo(EmbudoResumentoDTO datos)
        {
            try
            {


                MemoryStream ms = new MemoryStream();

                using (var package = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Resultado embudo");

                    worksheet.Cells.Style.Font.Name = "Arial";
                    worksheet.Cells.Style.Font.Size = 10;
                    //worksheet.Cells.AutoFitColumns();
                    //worksheet.PrinterSettings.Orientation = eOrientation.Landscape; //orientacion de la pagina
                    worksheet.PrinterSettings.PaperSize = ePaperSize.A4; //orientacion de la pagina

                    ///valores a considerar
                    int fila = 1, columna = 2;

                    //Color color_fondo = Color.FromArgb(31, 73, 125);  //azul oscuro
                    //Color color_fondo = Color.FromArgb(204, 255, 255);  //verde claro
                    Color color_fondo = Color.FromArgb(217, 217, 217);  //gris oscuro

                    //Color color_encabezado = Color.FromArgb(255, 255, 255); // blanco
                    Color color_encabezado = Color.FromArgb(0, 0, 0); //negro

                    #region Colores del excel
                    Color _colorCabecera = Color.FromArgb(191, 191, 191); // gris
                    Color _colorSubSubNivel = Color.FromArgb(197, 217, 241); // azul claro
                    Color _colorNivel = Color.FromArgb(33, 182, 193); // integra 
                    Color _colorSubNivel = Color.FromArgb(169, 208, 142); //rojo claro
                    #endregion

                    ///////////////cabecera
                    /// añade la cabecera de SGC
                    /// 
                    //Image imagen_cabecera_sgc = Image.FromFile( a.ContentRootPath ApplicationPhysicalPath + "Content/img/EncabezadoSGC/" + "RE-EJP-039 Lista de verificación de últimos detalles.png");
                    //Image imagen_cabecera_sgc = Image.FromFile(new HostingEnvironment().WebRootPath + "Content/img/EncabezadoSGC/" + "RE-EJP-039 Lista de verificación de últimos detalles.png");
                    //ExcelPicture imagen_cabecera = worksheet.Drawings.AddPicture("cabecera", imagen_cabecera_sgc);
                    //imagen_cabecera.SetPosition(0, 0, 1, 0);
                    //imagen_cabecera.SetSize(85);


                    var listaNivel = new List<CeldaDTO>();
                    var listaSubNivel = new List<CeldaDTO>();
                    var listaSubSubNivel = new List<CeldaDTO>();
                    var listaCabecera = new List<CeldaDTO>();


                    #region Encabezado
                    //primerafila
                    fila = 5;
                    columna = 3;
                    //segunda fila

                    columna += 5;
                    worksheet.Cells[fila, columna].Value = "Inicial";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Entran";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Salen";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Nuevos";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Final";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Cantidad";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    fila++;
                    columna = 1;


                    columna += 1;
                    foreach (var item in datos.ListaNivel)
                    {
                        columna = 1;
                        worksheet.Cells[fila, columna + 4].Value = item.Id;//nivel 1
                        worksheet.Cells[fila, columna + 4].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna + 4].Merge = true;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna + 4 });

                        columna++;

                        worksheet.Cells[fila, columna + 4].Value = item.Nombre;//nivel 1
                        worksheet.Cells[fila, columna + 4].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna + 4, fila, columna + 5].Merge = true;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna + 4 });

                        worksheet.Cells[fila, columna + 6].Value = item.Inicial;//nivel 1
                        worksheet.Cells[fila, columna + 7].Value = item.Entran;//nivel 1
                        worksheet.Cells[fila, columna + 8].Value = item.Salen;//nivel 1
                        worksheet.Cells[fila, columna + 9].Value = item.Nuevos;//nivel 1
                        worksheet.Cells[fila, columna + 10].Value = item.Final;//nivel 1

                        fila++;

                        foreach (var item2 in item.ListaSubNiveles)
                        {
                            worksheet.Cells[fila, columna + 3].Value = Convert.ToDecimal(string.Concat(item.Id, ".", item2.Id));//nivel 1
                            worksheet.Cells[fila, columna + 3].Style.Font.Bold = true;
                            worksheet.Cells[fila, columna + 3].Merge = true;
                            listaSubNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna + 3 });

                            worksheet.Cells[fila, columna + 4].Value = item2.Nombre;//sub nivel
                            worksheet.Cells[fila, columna + 4].Style.Font.Bold = true;
                            worksheet.Cells[fila, columna + 4, fila, columna + 5].Merge = true;
                            listaSubNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna + 4 });

                            worksheet.Cells[fila, columna + 6].Value = item2.Inicial;//nivel 1
                            worksheet.Cells[fila, columna + 7].Value = item2.Entran;//nivel 1
                            worksheet.Cells[fila, columna + 8].Value = item2.Salen;//nivel 1
                            worksheet.Cells[fila, columna + 9].Value = item2.Nuevos;//nivel 1
                            worksheet.Cells[fila, columna + 10].Value = item2.Final;//nivel 1

                            worksheet.Row(fila).Height = 20;
                            worksheet.Cells[fila, columna].Style.WrapText = true;

                            fila++;

                            foreach (var item3 in item2.ListaSubSubNivel)
                            {
                                worksheet.Cells[fila, columna + 4].Value = item3.Nombre;//sub  subnivel
                                worksheet.Cells[fila, columna + 4].Style.Font.Bold = true;
                                worksheet.Cells[fila, columna + 4, fila, columna + 5].Merge = true;
                                listaSubSubNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna + 4 });

                                worksheet.Cells[fila, columna + 11].Value = item3.Cantidad;//nivel 1

                                fila++;
                            }
                        }
                        fila++;//Agregamos una fila entre niveles
                    }
                    worksheet.Column(columna + 4).Width = 20;
                    worksheet.Column(columna + 5).Width = 20;


                    worksheet.Column(columna + 7).AutoFit();
                    worksheet.Column(columna + 8).AutoFit();

                    //colores
                    foreach (var item in listaCabecera)
                    {
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.BackgroundColor.SetColor(_colorCabecera);
                    }


                    //bold
                    foreach (var item in listaNivel)
                    {
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.BackgroundColor.SetColor(_colorNivel);
                        worksheet.Cells[item.Fila, item.Columna, item.Fila, item.Columna].Style.Font.Bold = true;
                    }
                    foreach (var item in listaSubNivel)
                    {
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.BackgroundColor.SetColor(_colorSubNivel);
                        //worksheet.Cells[item.Fila, item.Columna, item.Fila, item.Columna].Style.Font.Bold = true;
                    }
                    foreach (var item in listaSubSubNivel)
                    {
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.BackgroundColor.SetColor(_colorSubSubNivel);
                        //worksheet.Cells[item.Fila, item.Columna, item.Fila, item.Columna].Style.Font.Bold = true;
                    }

                    //for (int i = 3; i <= worksheet.Dimension.Columns; i++)
                    //{
                    //    worksheet.Column(i).Width = 20;
                    //}
                    #endregion

                    //dimensiones columnas
                    //for (int i = 1; i <= worksheet.Dimension.Columns; i++)
                    //{
                    //    worksheet.Column(i).Width();
                    //}
                    //worksheet.Column(1).Width = 2;


                    //formatos fecha
                    //string formato_fecha = "dd/MM/yyyy";
                    //worksheet.Column(9).Style.Numberformat.Format = formato_fecha;

                    ////bordes
                    //Border bordes = worksheet.Cells[4, 1, fila, 7].Style.Border;
                    //bordes.BorderAround(ExcelBorderStyle.Thin);
                    //bordes.Bottom.Style = bordes.Top.Style = bordes.Left.Style = bordes.Right.Style = ExcelBorderStyle.Thin;

                    package.Save();
                }

                byte[] excel = ms.ToArray();

                ms.Close();

                return excel;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        internal byte[] ReporteDetalladoPorAlumnoAsistenciaOnline(List<ReporteAsistenciaOnlineDTO> listadoReporte)
        {
            try
            {
                MemoryStream ms = new MemoryStream();

                using (var package = new ExcelPackage(ms))
                {
                    // add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Consolidado Asistencia Sesiones");

                    worksheet.Cells.Style.Font.Name = "Calibri";
                    worksheet.Cells.Style.Font.Size = 11;
                    //worksheet.Cells.AutoFitColumns();

                    ///valores a considerar
                    int fila = 1;
                    int columna = 3;
                    //Color color_fondo = Color.FromArgb(31, 73, 125);  //azul oscuro
                    Color color_fondo = Color.FromArgb(155, 187, 89);  //verde 
                    //Color color_fondo = Color.FromArgb(204, 255, 255);  //verde claro
                    Color color_encabezado = Color.FromArgb(255, 255, 255); // blanco
                    //Color color_encabezado = Color.FromArgb(0, 0, 0); //negro

                    ///////////////cabecera                    
                    var centroCostoPrograma = listadoReporte.FirstOrDefault();

                    //primerafila
                    worksheet.Cells[fila, 1].Value = centroCostoPrograma.NombreCentroCosto + " - " + centroCostoPrograma.NombrePrograma;
                    worksheet.Cells[fila, 1, fila, 12].Merge = true;
                    worksheet.Cells[fila, 1].Style.Font.Bold = true;
                    worksheet.Cells[fila, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[fila, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[fila, 1].Style.Fill.BackgroundColor.SetColor(color_fondo);
                    worksheet.Cells[fila, 1].Style.Font.Color.SetColor(color_encabezado);

                    //Datos Alumnos
                    //foreach (DateTime fecha in listado_sesiones_docente_por_periodo.OrderBy(o => o.Fecha).Select(s => s.Fecha).Distinct())
                    string curso = centroCostoPrograma.NombreCurso;

                    fila = 3;

                    Border bordes = worksheet.Cells[fila, columna, fila + 1, columna + 1].Style.Border;
                    bordes.Bottom.Style = bordes.Top.Style = bordes.Left.Style = bordes.Right.Style = ExcelBorderStyle.Thin;


                    //formatos fecha
                    string formato_fecha = "dd/MM/yyyy hh:mm";

                    worksheet.Cells[fila, columna].Value = centroCostoPrograma.NombreCurso;
                    worksheet.Cells[fila, columna].Style.WrapText = true;
                    worksheet.Cells[fila, columna, fila + 1, columna + 1].Merge = true;
                    worksheet.Cells[fila, columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[fila, columna].Style.Fill.BackgroundColor.SetColor(color_fondo);
                    worksheet.Cells[fila, columna].Style.Font.Color.SetColor(color_encabezado);
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;

                    fila++; fila++;

                    bordes = worksheet.Cells[fila, columna, fila + 1, columna + 1].Style.Border;
                    bordes.Bottom.Style = bordes.Top.Style = bordes.Left.Style = bordes.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[fila, columna].Value = "Fecha";
                    worksheet.Cells[fila, columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[fila, columna].Style.Fill.BackgroundColor.SetColor(color_fondo);
                    worksheet.Cells[fila, columna].Style.Font.Color.SetColor(color_encabezado);
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna + 1].Value = "Asistencia";
                    worksheet.Cells[fila, columna + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[fila, columna + 1].Style.Fill.BackgroundColor.SetColor(color_fondo);
                    worksheet.Cells[fila, columna + 1].Style.Font.Color.SetColor(color_encabezado);
                    worksheet.Cells[fila, columna + 1].Style.Font.Bold = true;

                    foreach (var item_reporte in listadoReporte)
                    {
                        fila++;

                        if (curso == item_reporte.NombreCurso)
                        {

                            bordes = worksheet.Cells[fila, columna, fila, columna + 1].Style.Border;
                            bordes.Bottom.Style = bordes.Top.Style = bordes.Left.Style = bordes.Right.Style = ExcelBorderStyle.Thin;

                            worksheet.Cells[fila, columna].Value = item_reporte.FechaSesion;
                            worksheet.Cells[fila, columna + 1].Value = item_reporte.Asistio;
                            worksheet.Cells[fila, columna + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[fila, columna].Style.Numberformat.Format = formato_fecha;
                        }
                        else
                        {
                            columna = columna + 4;
                            fila = 3;
                            bordes = worksheet.Cells[fila, columna, fila + 1, columna + 1].Style.Border;
                            bordes.Bottom.Style = bordes.Top.Style = bordes.Left.Style = bordes.Right.Style = ExcelBorderStyle.Thin;

                            worksheet.Cells[fila, columna].Value = item_reporte.NombreCurso;
                            worksheet.Cells[fila, columna].Style.WrapText = true;
                            worksheet.Cells[fila, columna, fila + 1, columna + 1].Merge = true;
                            worksheet.Cells[fila, columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[fila, columna].Style.Fill.BackgroundColor.SetColor(color_fondo);
                            worksheet.Cells[fila, columna].Style.Font.Color.SetColor(color_encabezado);
                            worksheet.Cells[fila, columna].Style.Font.Bold = true;

                            fila++; fila++;
                            bordes = worksheet.Cells[fila, columna, fila + 1, columna + 1].Style.Border;
                            bordes.Bottom.Style = bordes.Top.Style = bordes.Left.Style = bordes.Right.Style = ExcelBorderStyle.Thin;

                            worksheet.Cells[fila, columna].Value = "Fecha";
                            worksheet.Cells[fila, columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[fila, columna].Style.Fill.BackgroundColor.SetColor(color_fondo);
                            worksheet.Cells[fila, columna].Style.Font.Color.SetColor(color_encabezado);
                            worksheet.Cells[fila, columna].Style.Font.Bold = true;
                            worksheet.Cells[fila, columna + 1].Value = "Asistencia";
                            worksheet.Cells[fila, columna + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[fila, columna + 1].Style.Fill.BackgroundColor.SetColor(color_fondo);
                            worksheet.Cells[fila, columna + 1].Style.Font.Color.SetColor(color_encabezado);
                            worksheet.Cells[fila, columna + 1].Style.Font.Bold = true;

                            fila++;
                            bordes = worksheet.Cells[fila, columna, fila, columna + 1].Style.Border;
                            bordes.Bottom.Style = bordes.Top.Style = bordes.Left.Style = bordes.Right.Style = ExcelBorderStyle.Thin;

                            worksheet.Cells[fila, columna].Value = item_reporte.FechaSesion;
                            worksheet.Cells[fila, columna + 1].Value = item_reporte.Asistio;
                            worksheet.Cells[fila, columna + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[fila, columna].Style.Numberformat.Format = formato_fecha;
                        }

                        curso = item_reporte.NombreCurso;
                    }

                    //dimensiones columnas
                    for (int i = 1; i <= worksheet.Dimension.Columns; i++)
                    {
                        worksheet.Column(i).AutoFit();
                    }

                    //bordes
                    //Border bordes = worksheet.Cells[3, 3, worksheet.Dimension.Rows, worksheet.Dimension.Columns].Style.Border;
                    //bordes.Bottom.Style = bordes.Top.Style = bordes.Left.Style = bordes.Right.Style = ExcelBorderStyle.Thin;

                    package.Save();
                }

                byte[] excel = ms.ToArray();

                ms.Close();
                return excel;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public byte[] ObtenerContactosCampaniaMalingDetalle(List<ContactoCampaniaMailingDTO> datos, TipoArchivo tipoArchivo)
        {
            try
            {


                MemoryStream ms = new MemoryStream();

                using (var package = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Contactos");

                    worksheet.Cells.Style.Font.Name = "Arial";
                    worksheet.Cells.Style.Font.Size = 10;
                    //worksheet.Cells.AutoFitColumns();
                    //worksheet.PrinterSettings.Orientation = eOrientation.Landscape; //orientacion de la pagina
                    worksheet.PrinterSettings.PaperSize = ePaperSize.A4; //orientacion de la pagina
                    ///valores a considerar
                    int fila = 1, columna = 2;
                    Color color_fondo = Color.FromArgb(217, 217, 217);  //gris oscuro

                    #region Colores del excel
                    Color _colorCabecera = Color.FromArgb(191, 191, 191); // gris
                    #endregion
                    ///////////////cabecera

                    var listaNivel = new List<CeldaDTO>();
                    var listaCabecera = new List<CeldaDTO>();

                    #region Encabezado
                    //primerafila
                    fila = 1;
                    columna = 1;
                    //segunda fila
                    if (tipoArchivo == TipoArchivo.Excel)
                    {
                        worksheet.Cells[fila, columna].Value = "Email1";
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        columna++;
                        worksheet.Cells[fila, columna].Value = "Nombre1";
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        columna++;
                        worksheet.Cells[fila, columna].Value = "ApellidoPaterno";
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        columna++;
                        worksheet.Cells[fila, columna].Value = "IdPrioridadMailChimpLista";
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        fila++;
                    }
                    

                    foreach (var item in datos)
                    {
                        columna = 1;
                        worksheet.Cells[fila, columna].Value = item.Email1;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Nombre1;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.ApellidoPaterno;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.IdPrioridadMailChimpListaCorreo;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        fila++;
                    }
                    //worksheet.Column(columna + 4).Width = 20;
                    //worksheet.Column(columna + 5).Width = 20;
                    //worksheet.Column(columna + 7).AutoFit();
                    //worksheet.Column(columna + 8).AutoFit();

                    //colores
                    foreach (var item in listaCabecera)
                    {
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.BackgroundColor.SetColor(_colorCabecera);
                    }

                    //bold
                    //foreach (var item in listaNivel)
                    //{
                    //    //worksheet.Cells[item.Fila, item.Columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    //worksheet.Cells[item.Fila, item.Columna].Style.Fill.BackgroundColor.SetColor(_colorNivel);
                    //    worksheet.Cells[item.Fila, item.Columna, item.Fila, item.Columna].Style.Font.Bold = true;
                    //}

                    #endregion

                    if (tipoArchivo == TipoArchivo.Csv)
                    {
                        ms = package.ConvertToCsv();
                    }
                    package.Save();
                }

                
                byte[] excel = ms.ToArray();
                ms.Close();
                return excel;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public byte[] ReporteAmbientePespecifico(List<ReporteAmbienteDTO> listadoReporte)
        {
            try
            {
                MemoryStream ms = new MemoryStream();

                using (var package = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Reporte Programa Especifico Ambientes");

                    //const double minWidth = 0.00;
                    //const double maxWidth = 50.00;

                    //worksheet.Cells.AutoFitColumns(minWidth, maxWidth);

                    worksheet.Cells.Style.Font.Name = "Arial";
                    worksheet.Cells.Style.Font.Size = 10;
                    //worksheet.Cells.AutoFitColumns();
                    //worksheet.PrinterSettings.Orientation = eOrientation.Landscape; //orientacion de la pagina
                    worksheet.PrinterSettings.PaperSize = ePaperSize.A4; //orientacion de la pagina

                    ///valores a considerar
                    int fila = 1, columna = 2;

                    //Color color_fondo = Color.FromArgb(31, 73, 125);  //azul oscuro
                    //Color color_fondo = Color.FromArgb(204, 255, 255);  //verde claro
                    Color color_fondo = Color.FromArgb(217, 217, 217);  //gris oscuro

                    //Color color_encabezado = Color.FromArgb(255, 255, 255); // blanco
                    Color color_encabezado = Color.FromArgb(0, 0, 0); //negro

                    #region Colores del excel
                    Color _colorCabecera = Color.FromArgb(191, 191, 191); // gris
                    Color _colorSubSubNivel = Color.FromArgb(197, 217, 241); // azul claro
                    Color _colorNivel = Color.FromArgb(33, 182, 193); // integra 
                    Color _colorSubNivel = Color.FromArgb(169, 208, 142); //rojo claro
                    #endregion

                    ///////////////cabecera
                    /// añade la cabecera de SGC
                    /// 
                    //Image imagen_cabecera_sgc = Image.FromFile( a.ContentRootPath ApplicationPhysicalPath + "Content/img/EncabezadoSGC/" + "RE-EJP-039 Lista de verificación de últimos detalles.png");
                    //Image imagen_cabecera_sgc = Image.FromFile(new HostingEnvironment().WebRootPath + "Content/img/EncabezadoSGC/" + "RE-EJP-039 Lista de verificación de últimos detalles.png");
                    //ExcelPicture imagen_cabecera = worksheet.Drawings.AddPicture("cabecera", imagen_cabecera_sgc);
                    //imagen_cabecera.SetPosition(0, 0, 1, 0);
                    //imagen_cabecera.SetSize(85);


                    var listaNivel = new List<CeldaDTO>();
                    var listaSubNivel = new List<CeldaDTO>();
                    var listaSubSubNivel = new List<CeldaDTO>();
                    var listaCabecera = new List<CeldaDTO>();


                    #region Encabezado
                    //primerafila
                    fila =  1;
                    columna =1;
                    //segunda fila
                    //worksheet.Column(1).AutoFit(0,50);
                    //columna += 5;
                    worksheet.Cells[fila, columna].Value = "Modalidad";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells.AutoFitColumns();
                    worksheet.Column(columna).Width= 20;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Centro de Costo";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 50;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Estado Centro de Costo";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Curso";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 100;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Año";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 10;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "SemanaCalendario";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Fecha";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Horario";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Sede";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Aula";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "NroSesión";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Docente";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 25;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Coordinador";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 25;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "NroAmbientesProgramados";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "NroAmbientesDisponibles";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    fila++;
                    columna = 1;


                    columna += 1;
                    foreach (var item in listadoReporte)
                    {
                        columna = 1;
                        worksheet.Cells[fila, columna].Value = item.Modalidad;                        
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.CentroCosto;
                        //worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.EstadoCentroCosto;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Curso;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Anio;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.SemanaCalendario;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Fecha;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Horario;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Sede;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Aula;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.NroSesión;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Docente;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Coordinador;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.NroAmbientesProgramados;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;                        

                        worksheet.Cells[fila, columna].Value = item.NroAmbientesDisponibles;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        fila++;
                    }
                    //worksheet.Column(columna + 4).Width = 20;
                    //worksheet.Column(columna + 5).Width = 20;


                    //worksheet.Column(columna + 7).AutoFit();
                    //worksheet.Column(columna + 8).AutoFit();
                    columna = 1;
                    //colores
                    foreach (var item in listaCabecera)
                    {
                        //worksheet.Column(columna++).AutoFit();
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.BackgroundColor.SetColor(_colorCabecera);
                    }
                    //for (int i = 1; i <= worksheet.Dimension.Columns; i++)
                    //{
                    //    worksheet.Column(i).AutoFit();
                    //}
                    //bold
                    //foreach (var item in listaNivel)
                    //{
                    //    worksheet.Cells[item.Fila, item.Columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    worksheet.Cells[item.Fila, item.Columna].Style.Fill.BackgroundColor.SetColor(_colorNivel);
                    //    worksheet.Cells[item.Fila, item.Columna, item.Fila, item.Columna].Style.Font.Bold = true;
                    //}
                    //foreach (var item in listaSubNivel)
                    //{
                    //    worksheet.Cells[item.Fila, item.Columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    worksheet.Cells[item.Fila, item.Columna].Style.Fill.BackgroundColor.SetColor(_colorSubNivel);
                    //    //worksheet.Cells[item.Fila, item.Columna, item.Fila, item.Columna].Style.Font.Bold = true;
                    //}
                    //foreach (var item in listaSubSubNivel)
                    //{
                    //    worksheet.Cells[item.Fila, item.Columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    worksheet.Cells[item.Fila, item.Columna].Style.Fill.BackgroundColor.SetColor(_colorSubSubNivel);
                    //    //worksheet.Cells[item.Fila, item.Columna, item.Fila, item.Columna].Style.Font.Bold = true;
                    //}

                    #endregion

                    //worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    package.Save();
                }

                byte[] excel = ms.ToArray();

                ms.Close();
                return excel;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
