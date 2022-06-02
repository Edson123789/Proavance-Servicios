using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xceed.Words.NET;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public partial class RaCoordinadorDocumentacionAlumnoBO : BaseBO
    {
        public int Id { get; set; }
        public string CodigoAlumno { get; set; }
        public int IdCriterioDoc { get; set; }
        public bool? EnviadoContabilidad { get; set; }
        public byte Version { get; set; }
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }
        public string ContentType { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        private RaCoordinadorDocumentacionAlumnoRepositorio _repRaCoordinadorDocumentacionAlumnoRepositorio;
        public RaCoordinadorDocumentacionAlumnoBO() {
            _repRaCoordinadorDocumentacionAlumnoRepositorio = new RaCoordinadorDocumentacionAlumnoRepositorio();
        }

        public byte[] ObtenerFormatoConvenio(AlumnoPresencialSinMatriculaVerificadaDTO alumno, string rutaPlantilla)
        {
            try
            {
                FormatoConvenioDTO modelo = new FormatoConvenioDTO();
                byte[] formato = this.GenerarFormatoConvenio(modelo, rutaPlantilla);
                return formato;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public byte[] ObtenerFormatoPagare(AlumnoPresencialSinMatriculaVerificadaDTO alumno, string rutaPlantilla)
        {
            try
            {
                var modelo = _repRaCoordinadorDocumentacionAlumnoRepositorio.ObtenerModeloFormatoPagare(alumno);
                bool aplicaBogota = alumno.NombreCentroCosto.Contains("BOGOTA") ? true : false;
                if (modelo != null)
                {
                    return this.GenerarFormatoPagare(modelo, rutaPlantilla, aplicaBogota);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public byte[] ObtenerFormatoFichaMatricula(AlumnoPresencialSinMatriculaVerificadaDTO alumno, string rutaPlantilla)
        {
            try
            {
                FormatoFichaDTO modelo = new FormatoFichaDTO
                {
                    ListadoCuotas = _repRaCoordinadorDocumentacionAlumnoRepositorio.ListadoCuotasSinMatriculaVerificadaPorCodigoAlumno(alumno.CodigoMatricula)
                };
                return this.GenerarFormatoFicha(modelo, rutaPlantilla);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Genera el reporte de ficha 
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="rutaPlantilla"></param>
        /// <returns></returns>
        public byte[] GenerarFormatoFicha(FormatoFichaDTO modelo, string rutaPlantilla)
        {
            try
            {
                string ruta_template_carta = rutaPlantilla + "ficha-plantilla" + ".docx";

                MemoryStream ms_origen = new MemoryStream(File.ReadAllBytes(ruta_template_carta));
                MemoryStream ms_destino = new MemoryStream();

                using (DocX document = DocX.Load(ms_origen))
                {
                    document.ReplaceText("$nombre_pespecifico", modelo.NombrePEspecifico == null ? "" : modelo.NombrePEspecifico, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$codigo_alumno", modelo.CodigoAlumno == null ? "" : modelo.CodigoAlumno, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$nombres", modelo.Nombres == null ? "" : modelo.Nombres, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$apellido_paterno", modelo.ApellidoPaterno == null ? "" : modelo.ApellidoPaterno, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$apellido_materno", modelo.ApellidoMaterno == null ? "" : modelo.ApellidoMaterno, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$nro_documento", modelo.NroDocumento == null ? "" : modelo.NroDocumento, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$dia_nacimiento", modelo.FechaNacimiento == null ? "" : modelo.FechaNacimiento.Value.Day.ToString(), false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$mes_nacimiento", modelo.FechaNacimiento == null ? "" : modelo.FechaNacimiento.Value.Month.ToString(), false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$anho_nacimiento", modelo.FechaNacimiento == null ? "" : modelo.FechaNacimiento.Value.Year.ToString(), false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$lugar_nacimiento", modelo.LugarNacimiento == null ? "" : modelo.LugarNacimiento, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$edad", modelo.Edad == null ? "" : modelo.Edad.ToString(), false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$estado_civil", modelo.EstadoCivil == null ? "" : modelo.EstadoCivil, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$ciudad", modelo.Ciudad == null ? "" : modelo.Ciudad, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$pais", modelo.Pais == null ? "" : modelo.Pais, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$direccion", modelo.Direccion == null ? "" : modelo.Direccion, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$telefono_domicilio", modelo.TelefonoDomiciliario == null ? "" : modelo.TelefonoDomiciliario, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$celular", modelo.Celular == null ? "" : modelo.Celular, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$email", modelo.Email == null ? "" : modelo.Email, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$nombre_completo", modelo.Alumno == null ? "" : modelo.Alumno, false, RegexOptions.IgnoreCase);


                    int columna = 0, fila = 0;
                    Table nueva_tabla = document.AddTable(2 + (modelo.ListadoCuotas == null ? 0 : modelo.ListadoCuotas.Count), 3);
                    nueva_tabla.SetBorder(TableBorderType.InsideH, new Border(BorderStyle.Tcbs_single, BorderSize.one, 0, Color.Black));

                    nueva_tabla.Rows[0].Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    nueva_tabla.Rows[0].Cells[0].Paragraphs.First().Append("CRONOGRAMA");
                    //nueva_tabla.Rows[0].Cells[0].VerticalAlignment=VerticalAlignment.Center;
                    nueva_tabla.Rows[0].Cells[0].Paragraphs.First().Alignment = Alignment.center;
                    nueva_tabla.Rows[0].MergeCells(0, 2);

                    nueva_tabla.Rows[1].Cells[0].Paragraphs.First().Append("Número de Cuota");
                    nueva_tabla.Rows[1].Cells[1].Paragraphs.First().Append("Monto " + (modelo.ListadoCuotas == null ? "" : modelo.ListadoCuotas.Sum(s => s.Cuota.Value).ToString("0.00")));
                    nueva_tabla.Rows[1].Cells[2].Paragraphs.First().Append("Fecha de vencimiento");

                    fila = 1;
                    if (modelo.ListadoCuotas != null)
                    {
                        foreach (var cuota in modelo.ListadoCuotas.OrderBy(o => o.FechaVencimiento)
                            .ThenBy(o => o.NroCuota).ThenBy(o => o.NroSubCuota))
                        {
                            fila++;
                            nueva_tabla.Rows[fila + 1].Cells[0].Paragraphs.First().Append(cuota.NroCuota.ToString());
                            nueva_tabla.Rows[fila + 1].Cells[1].Paragraphs.First()
                                .Append("Monto " + cuota.Cuota.Value.ToString("0.00"));
                            nueva_tabla.Rows[fila + 1].Cells[2].Paragraphs.First()
                                .Append(cuota.FechaVencimiento.Value.ToShortDateString());
                        }
                    }

                    document.Tables[7].InsertTableBeforeSelf(nueva_tabla);
                    int indice_tabla_anterior = document.Tables[7].Index;
                    document.InsertTable(indice_tabla_anterior, nueva_tabla);

                    //document.Tables[7] = nueva_tabla;
                    //document.Tables[7].Remove();
                    //document.InsertTable(indice_tabla_anterior, nueva_tabla);


                    //Row fila = document.Tables[7].InsertRow();
                    //document.Tables[7].InsertRow();

                    document.SaveAs(ms_destino);
                }
                ms_destino.Position = 0;
                return ms_destino.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Genera el formato de pagare
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="rutaPlantilla"></param>
        /// <param name="aplicaBogota"></param>
        /// <returns></returns>
        public byte[] GenerarFormatoPagare(FormatoPagareDTO modelo, string rutaPlantilla, bool aplicaBogota)
        {
            try
            {
                string rutaTemplateCarta = rutaPlantilla + "pagare-plantilla" + ".docx";
                if (aplicaBogota == true) {
                    rutaTemplateCarta = rutaPlantilla + "pagare-plantilla-bogota" + ".docx";
                }

                MemoryStream ms_origen = new MemoryStream(File.ReadAllBytes(rutaTemplateCarta));
                MemoryStream ms_destino = new MemoryStream();

                using (DocX document = DocX.Load(ms_origen))
                {
                    document.ReplaceText("$razon_social", modelo.RazonSocial == null ? "" : modelo.RazonSocial, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$tipo_identificacion_contibuyente", modelo.TipoIdentificacionContribuyente == null ? "" : modelo.TipoIdentificacionContribuyente, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$numero_identificacion_contribuyente", modelo.NumeroIdentificacionContribuyente == null ? "" : modelo.NumeroIdentificacionContribuyente, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$oficina_direccion", modelo.DireccionOficinas == null ? "" : modelo.DireccionOficinas, false, RegexOptions.IgnoreCase);

                    document.ReplaceText("$nombre_completo", modelo.NombreAlumno == null ? "" : modelo.NombreAlumno, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$nro_documento", modelo.NroDocumento == null ? "" : modelo.NroDocumento, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$direccion", modelo.Direccion == null ? "" : modelo.Direccion, false, RegexOptions.IgnoreCase);
                    document.ReplaceText("$telefono_celular", modelo.Celular == null ? "" : modelo.Celular, false, RegexOptions.IgnoreCase);

                    document.SaveAs(ms_destino);
                }
                ms_destino.Position = 0;
                return ms_destino.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Genera el formato de convenio
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="rutaPlantilla"></param>
        /// <returns></returns>
        public byte[] GenerarFormatoConvenio(FormatoConvenioDTO modelo, string rutaPlantilla)
        {
            try
            {
                string rutaTemplateCarta = rutaPlantilla + "convenio-plantilla" + ".docx";

                MemoryStream ms_origen = new MemoryStream(File.ReadAllBytes(rutaTemplateCarta));
                MemoryStream ms_destino = new MemoryStream();

                using (DocX document = DocX.Load(ms_origen))
                {

                    var tabla = document.Tables[0];

                    Table nuevaTabla = document.AddTable(4, 4);



                    nuevaTabla.Rows[0].Cells[0].Paragraphs.First().Append("A");
                    nuevaTabla.Rows[0].Cells[1].Paragraphs.First().Append("B");
                    nuevaTabla.Rows[0].Cells[2].Paragraphs.First().Append("C");
                    nuevaTabla.Rows[1].Cells[0].Paragraphs.First().Append("D");

                    document.Tables[0] = nuevaTabla;
                    int index = document.Tables[0].Index;

                    document.Tables[0].Remove();
                    document.InsertTable(index, nuevaTabla);

                    //document.InsertTable(nuevaTabla);

                    //document.ReplaceText("$fecha_texto", modelo.Fecha_Texto, false, RegexOptions.IgnoreCase);
                    //document.ReplaceText("$alumno", modelo.Alumno, false, RegexOptions.IgnoreCase);

                    //if (modelo.Direccion == null)
                    //    document.ReplaceText("$direccion", "", false, RegexOptions.IgnoreCase);
                    //else
                    //    document.ReplaceText("$direccion", modelo.Direccion, false, RegexOptions.IgnoreCase);

                    //if (modelo.Referencia == null)
                    //    document.ReplaceText("$referencia", "", false, RegexOptions.IgnoreCase);
                    //else
                    //    document.ReplaceText("$referencia", "\n" + modelo.Referencia, false, RegexOptions.IgnoreCase);

                    //document.ReplaceText("$departamento", modelo.Departamento == null ? "" : modelo.Departamento, false, RegexOptions.IgnoreCase);
                    //document.ReplaceText("$pais", modelo.Pais == null ? "" : modelo.Pais, false, RegexOptions.IgnoreCase);
                    //document.ReplaceText("$fecha_numero", modelo.Fecha_Numero, false, RegexOptions.IgnoreCase);
                    //document.ReplaceText("$simbolo", modelo.Simbolo, false, RegexOptions.IgnoreCase);
                    //document.ReplaceText("$monto", modelo.Monto, false, RegexOptions.IgnoreCase);
                    //document.ReplaceText("$moneda", modelo.Moneda, false, RegexOptions.IgnoreCase);
                    //document.ReplaceText("$centrocosto", modelo.Centrocosto, false, RegexOptions.IgnoreCase);
                    //document.ReplaceText("$fecha_plazo_texto", modelo.Fecha_Plazo_Texto, false, RegexOptions.IgnoreCase);
                    //document.ReplaceText("$remitente", modelo.Remitente, false, RegexOptions.IgnoreCase);
                    //document.ReplaceText("$cargo", modelo.Cargo, false, RegexOptions.IgnoreCase);

                    //document.ReplaceText("$detalle_cuotas", modelo.Detalle_Cuota, false, RegexOptions.IgnoreCase);

                    ////document.ReplaceText("$direccion ", tipo_documento, false, RegexOptions.IgnoreCase);
                    ////document.ReplaceText("$direccion ", tipo_documento, false, RegexOptions.IgnoreCase);

                    document.SaveAs(ms_destino);
                }
                ms_destino.Position = 0;
                return ms_destino.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
