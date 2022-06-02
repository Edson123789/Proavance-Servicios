using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Planificacion/SesionConfigurarVideo
    /// Autor: Jorge Rivera Tito - Gian Miranda
    /// Fecha: 01/03/2021
    /// <summary>
    /// BO para la logica de la configuracion del video del programa por sesion
    /// </summary>
    public class SesionConfigurarVideoBO : BaseBO
    {
        /// Propiedades	                                Significado
        /// -----------	                                ------------
        /// IdConfigurarVideoPrograma                   Id de la configuracion del video (PK de la tabla pla.T_ConfigurarVideoPrograma)
        /// Minuto                                      Minuto en el que se mostrara lo configurado
        /// IdTipoVista                                 Id del tipo de vista que se usara en la configuracion (PK de la tabla pla.T_ConfigurarVideoPrograma)
        /// NroDiapositiva                              Entero con el numero de diapositiva
        /// IdEvaluacion                                Id de la evaluacion enlazada (PK de la tabla pla.T_ConfigurarVideoPrograma)
        /// ConLogoVideo                                Flag para determinar si tiene logo el video
        /// ConLogoDiapositiva                          Flag para determinar si tiene logo la diapositiva
        
        public int IdConfigurarVideoPrograma { get; set; }
        public int Minuto { get; set; }
        public int IdTipoVista { get; set; }
        public int NroDiapositiva { get; set; }
        public int IdEvaluacion { get; set; }
        public bool ConLogoVideo { get; set; }
        public bool ConLogoDiapositiva { get; set; }

        private integraDBContext _integraDBContext;
        private ConfigurarVideoProgramaRepositorio _repConfigurarVideoPrograma;
        private PgeneralRepositorio _repPGeneral;
        private ConfigurarVideoProgramaBO ConfigurarVideoPrograma;

        public SesionConfigurarVideoBO()
        {
            _repPGeneral = new PgeneralRepositorio();
            _repConfigurarVideoPrograma = new ConfigurarVideoProgramaRepositorio();
            ConfigurarVideoPrograma = new ConfigurarVideoProgramaBO();
        }

        public SesionConfigurarVideoBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repPGeneral = new PgeneralRepositorio(_integraDBContext);
            _repConfigurarVideoPrograma = new ConfigurarVideoProgramaRepositorio(_integraDBContext);
            ConfigurarVideoPrograma = new ConfigurarVideoProgramaBO(_integraDBContext);
        }

        /// <summary>
        /// Obtiene la plantilla para llenar y realizar la importacion de la seccion de Configuracion de Video
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Archivo excel de la plantilla de configuracion de video</returns>
        public byte[] ObtenerPlantillaExcelConfiguracionDeVideo(int idPGeneral)
        {
            try
            {
                ConfigurarVideoPrograma = new ConfigurarVideoProgramaBO(_integraDBContext);

                string pGeneral = _repPGeneral.FirstById(idPGeneral).Nombre;
                #region Campos Generados
                var listaCompletaProgramaSesionSubsesion = ConfigurarVideoPrograma.ObtenerEstructuraCapituloProgramaPorIdPGeneralDescarga(idPGeneral).OrderBy(c => c.OrdenCapitulo).ThenBy(c => c.OrdenFila).ToList();
                
                var listaCompletaProgramaSesionSubsesionv2 = ConfigurarVideoPrograma.ObtenerEstructuraCapituloProgramaPorIdPGeneralDescargaSinDatos(idPGeneral).OrderBy(c => c.OrdenCapitulo).ThenBy(c => c.OrdenFila).ToList();
                
                var configuracionPorIdPGeneral = _repConfigurarVideoPrograma.GetBy(x => x.IdPgeneral == idPGeneral && x.Estado, x => new ConfigurarVideoProgramaBasicoDTO { Id = x.Id, IdPGeneral = x.IdPgeneral, NumeroFila = x.NumeroFila.Value, NroDiapositivas = x.NroDiapositivas, TotalMinutos = x.TotalMinutos }).ToList();

                var camposGenerados = new List<CampoObligatorioDTO>();

                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Programa", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "ID del Programa", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "NroCap", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Capitulo", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Sesion", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Subsesion", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Orden Fila", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Id Configuracion", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Duracion segundos", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Nro diapositivas", FlagObligatorio = true });
                #endregion

                #region Campos Adicionales
                var camposAdicionales = new List<CampoObligatorioDTO>();

                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Segundo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Tipo Vista", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "NroDiapositiva", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Logo video", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Logo diapositiva", FlagObligatorio = false });
                #endregion

                #region Creacion Plantilla
                MemoryStream memoryStreamPlantilla = new MemoryStream();

                using (var package = new ExcelPackage(memoryStreamPlantilla))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("PlantillaConfigurarSecuenciaVideo");
                    var listaNivel = new List<CeldaDTO>();
                    var listaCabecera = new List<CeldaDTO>();

                    worksheet.Cells.Style.Font.Name = "Calibri";
                    worksheet.Cells.Style.Font.Size = 10.5f;
                    worksheet.PrinterSettings.PaperSize = ePaperSize.A4;
                    Color colorCabeceraGenerado = Color.FromArgb(200, 200, 200);
                    Color colorCabeceraAdicional = Color.FromArgb(255, 230, 150);
                    Color colorCabeceraObligatoria = Color.FromArgb(225, 100, 100);

                    // Encabezado
                    int fila = 1, columna = 1;

                    foreach (var campo in camposGenerados)
                    {
                        worksheet.Cells[fila, columna].Value = campo.Campo;
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[fila, columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[fila, columna].Style.Fill.BackgroundColor.SetColor(colorCabeceraGenerado);

                        listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        columna++;
                    }

                    foreach (var campo in camposAdicionales)
                    {
                        worksheet.Cells[fila, columna].Value = campo.Campo;
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[fila, columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[fila, columna].Style.Fill.BackgroundColor.SetColor(campo.FlagObligatorio ? colorCabeceraObligatoria : colorCabeceraAdicional);

                        listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        columna++;
                    }

                    fila++;
                    if (listaCompletaProgramaSesionSubsesion.Count != 0)
                    {
                        foreach (var dato in listaCompletaProgramaSesionSubsesion)
                        {
                            var configuracionIndividual = configuracionPorIdPGeneral.Where(x => x.IdPGeneral == dato.IdPgeneral && x.NumeroFila == dato.OrdenFila).FirstOrDefault();
                            var columnasAdicionalExcel = _repConfigurarVideoPrograma.ObtenerSesionConfigurarVideo(dato.IdConfigurarVideoPrograma);

                            for (int i = 0; i < Convert.ToInt32(configuracionIndividual.NroDiapositivas); i++)
                            {
                                columna = 1;
                                worksheet.Cells[fila, columna].Value = pGeneral;
                                columna++;
                                worksheet.Cells[fila, columna].Value = dato.IdPgeneral;
                                columna++;
                                worksheet.Cells[fila, columna].Value = dato.OrdenCapitulo;
                                columna++;
                                worksheet.Cells[fila, columna].Value = dato.Capitulo ?? string.Empty;
                                columna++;
                                worksheet.Cells[fila, columna].Value = dato.Sesion ?? string.Empty;
                                columna++;
                                worksheet.Cells[fila, columna].Value = dato.SubSesion ?? string.Empty;
                                columna++;
                                worksheet.Cells[fila, columna].Value = dato.OrdenFila;
                                columna++;
                                worksheet.Cells[fila, columna].Value = configuracionIndividual.Id;
                                columna++;
                                worksheet.Cells[fila, columna].Value = configuracionIndividual.TotalMinutos;
                                columna++;
                                worksheet.Cells[fila, columna].Value = configuracionIndividual.NroDiapositivas;
                                columna++;
                                worksheet.Cells[fila, columna].Value = columnasAdicionalExcel[i].Minuto;
                                columna++;
                                worksheet.Cells[fila, columna].Value = columnasAdicionalExcel[i].IdTipoVista;
                                columna++;
                                worksheet.Cells[fila, columna].Value = columnasAdicionalExcel[i].NroDiapositiva;
                                columna++;
                                worksheet.Cells[fila, columna].Value = columnasAdicionalExcel[i].ConLogoVideo == true ? "si" : columnasAdicionalExcel[i].ConLogoVideo == false ? "no" : "";
                                columna++;
                                worksheet.Cells[fila, columna].Value = columnasAdicionalExcel[i].ConLogoDiapositiva == true ? "si" : columnasAdicionalExcel[i].ConLogoDiapositiva == false ? "no" : "";
                                fila++;
                            }
                        }
                    }
                    else
                    {
                        foreach (var temp in listaCompletaProgramaSesionSubsesionv2)
                        {
                            var configuracionIndividual = configuracionPorIdPGeneral.Where(x => x.IdPGeneral == temp.IdPgeneral && x.NumeroFila == temp.OrdenFila).FirstOrDefault();                            

                            for (int i = 0; i < Convert.ToInt32(configuracionIndividual.NroDiapositivas); i++)
                            {
                                columna = 1;
                                worksheet.Cells[fila, columna].Value = pGeneral;
                                columna++;
                                worksheet.Cells[fila, columna].Value = temp.IdPgeneral;
                                columna++;
                                worksheet.Cells[fila, columna].Value = temp.OrdenCapitulo;
                                columna++;
                                worksheet.Cells[fila, columna].Value = temp.Capitulo ?? string.Empty;
                                columna++;
                                worksheet.Cells[fila, columna].Value = temp.Sesion ?? string.Empty;
                                columna++;
                                worksheet.Cells[fila, columna].Value = temp.SubSesion ?? string.Empty;
                                columna++;
                                worksheet.Cells[fila, columna].Value = temp.OrdenFila;
                                columna++;
                                worksheet.Cells[fila, columna].Value = configuracionIndividual.Id;
                                columna++;
                                worksheet.Cells[fila, columna].Value = configuracionIndividual.TotalMinutos;
                                columna++;
                                worksheet.Cells[fila, columna].Value = configuracionIndividual.NroDiapositivas;
                                fila++;
                            }
                        }
                    }
                    

                    package.Save();
                }

                byte[] excel = memoryStreamPlantilla.ToArray();
                memoryStreamPlantilla.Close();
                #endregion

                return excel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    public class RegistroSesionConfigurarVideoBO
    {
        public int Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int Minuto { get; set; }
        public int IdTipoVista { get; set; }
        public int NroDiapositiva { get; set; }
        public int IdEvaluacion { get; set; }
        public bool ConLogoVideo { get; set; }
        public bool ConLogoDiapositiva { get; set; }
    }
}
