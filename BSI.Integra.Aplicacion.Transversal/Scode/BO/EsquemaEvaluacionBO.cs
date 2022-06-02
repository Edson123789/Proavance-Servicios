using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Enums;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EsquemaEvaluacionBO : BaseBO
    {
        private readonly EsquemaEvaluacionRepositorio _repoEsquema;
        private readonly ParametroEvaluacionRepositorio _repoParametro;
        private readonly CriterioEvaluacionRepositorio _repoCriterio;
        private readonly EscalaCalificacionRepositorio _repoEscala;
        private readonly EscalaCalificacionDetalleRepositorio _repoEscalaDetalle;
        protected DapperRepository _dapper;

        public string Nombre { get; set; }
        public int IdFormaCalculoEvaluacion { get; set; }
        public List<EsquemaEvaluacionDetalleBO> ListadoDetalle { get; set; }

        public EsquemaEvaluacionBO()
        {
        }

        public EsquemaEvaluacionBO(integraDBContext context)
        {
            _repoEsquema = new EsquemaEvaluacionRepositorio(context);
            _repoParametro = new ParametroEvaluacionRepositorio(context);
            _repoCriterio = new CriterioEvaluacionRepositorio(context);
            _repoEscala = new EscalaCalificacionRepositorio(context);
            _repoEscalaDetalle = new EscalaCalificacionDetalleRepositorio(context);
        }

        public string SubirArchivo(byte[] archivo, string mimeType, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"planificacion/esquemaevaluacion/";
                    //string _direccionBlob = @"correos/individuales/";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = mimeType;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = mimeType;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        _nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreArchivo.Replace(" ", "%20");
                    }
                    else
                    {
                        _nombreLink = "";
                    }
                    return _nombreLink;
                }
                catch (Exception ex)
                {
                    //return "";
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                throw new Exception(e.Message);
            }
        }

        public decimal ObtenerNotaCriterioEvaluacion(List<ParametroEvaluacionNotaBO> listado)
        {
            try
            {
                if (listado == null || listado.Count == 0)
                    throw new Exception("No se enviaron parámetros a evaluar");

                var listadoParametros =
                    _repoParametro.GetBy(w => listado.Select(s => s.IdParametroEvaluacion).Contains(w.Id));
                var criterio = _repoCriterio.FirstById(listadoParametros.FirstOrDefault().IdCriterioEvaluacion);

                if (criterio == null)
                    throw new Exception("No se identificó el criterio a Evaluar");
                if (criterio.IdFormaCalculoEvaluacionParametro == null)
                    throw new Exception("No se configuró la forma de calculo de los parámetros");

                decimal nota = 0;
                foreach (var item in listado)
                {
                    var itemEscala = _repoEscalaDetalle.FirstById(item.IdEscalaCalificacionDetalle.Value);
                    if (itemEscala == null)
                        throw new Exception("No se seleccionó una escala de calificación");
                    nota += itemEscala.Valor;
                }

                //pondera si el tipo de cálculo es por promedio
                if (criterio.IdFormaCalculoEvaluacionParametro == (int) Enums.FormaCalculoEvaluacion.Promedio)
                    nota = nota / ((decimal) (listado.Count * 1.0));

                return nota;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public EsquemaEvaluacion_NotaCursoDTO ObtenerDetalleCalificacionPorCurso(int idMatriculaCabecera, int idPEspecifico, int grupo)
        {
            try
            {
                var listadoEvaluaciones =
                    _repoEsquema.ListadoDetalladoItemEvaluablePorAlumno(idMatriculaCabecera, idPEspecifico, grupo);

                if (listadoEvaluaciones == null || listadoEvaluaciones.Count == 0)
                    throw new Exception("No se tiene configurado el esquema de evaluación");

                if (listadoEvaluaciones.Any(w => w.IdFormaCalificacionCriterio == null))
                    throw new Exception(
                        "No se configuró correctamente la Forma de Calificación de un Criterio de Evaluación");
                if (listadoEvaluaciones.Any(w => w.IdFormaCalculoEvaluacion_Parametro == null))
                    throw new Exception(
                        "No se configuró correctamente la Forma de Calculo de un Parametro de Evaluación");
                if (listadoEvaluaciones.Any(w => w.IdFormaCalculoEvaluacion_Criterio == null))
                    throw new Exception(
                        "No se configuró correctamente la Forma de Calculo de un Criterio de Evaluación");

                EsquemaEvaluacion_NotaCursoDTO notaCurso = new EsquemaEvaluacion_NotaCursoDTO()
                {
                    IdMatriculaCabecera = idMatriculaCabecera,
                    IdPEspecifico = idPEspecifico,
                    Grupo = grupo,
                    DetalleCalificacion = new List<EsquemaEvaluacion_DetalleCalificacionDTO>()
                };

                foreach (var grupoCriterio in listadoEvaluaciones.GroupBy(g =>
                    new {g.IdCriterioEvaluacion, g.CriterioEvaluacion}))
                {
                    EsquemaEvaluacion_DetalleCalificacionDTO detalleCriterio = new EsquemaEvaluacion_DetalleCalificacionDTO()
                    {
                        IdCriterioEvaluacion = grupoCriterio.Key.IdCriterioEvaluacion,
                        CriterioEvaluacion = grupoCriterio.Key.CriterioEvaluacion,
                        Ponderacion = grupoCriterio.FirstOrDefault().Ponderacion_Criterio,
                        Valor = 0
                    };

                    //calculo de la nota por criterio
                    foreach (var grupoCriterioInstanciado in grupoCriterio.GroupBy(g =>
                        g.IdEsquemaEvaluacionPGeneralDetalle))
                    {
                        decimal notaCriterio = 0;

                        foreach (var grupoParametro in grupoCriterioInstanciado.GroupBy(g => g.IdParametroEvaluacion))
                        {
                            decimal notaParametro = 0;
                            foreach (var parametroIndividual in grupoParametro)
                            {
                                if (parametroIndividual.IdFormaCalculoEvaluacion_Parametro ==
                                    (int) Enums.FormaCalculoEvaluacion.Suma)
                                    notaParametro += parametroIndividual.ValorEscala ?? 0;
                                if (parametroIndividual.IdFormaCalculoEvaluacion_Parametro ==
                                    (int) Enums.FormaCalculoEvaluacion.Promedio)
                                    notaParametro += Convert.ToDecimal(
                                        ((double) (parametroIndividual.ValorEscala ?? 0)) *
                                        (parametroIndividual.Ponderacion_Parametro * 1.0) / 100.0
                                    );
                            }

                            //if (grupoParametro.FirstOrDefault().IdFormaCalculoEvaluacion_Parametro ==
                            //    (int) Enums.FormaCalculoEvaluacion.Promedio)
                            //    notaParametro =
                            //        notaParametro / ((decimal) (grupoParametro.Count() * 1.0));
                            notaCriterio += notaParametro;
                        }

                        detalleCriterio.Valor += notaCriterio;
                    }

                    //valida si el criterio es promedio
                    if (grupoCriterio.FirstOrDefault().IdFormaCalculoEvaluacion_Criterio ==
                        (int) Enums.FormaCalculoEvaluacion.Promedio)
                        detalleCriterio.Valor = detalleCriterio.Valor /
                                                ((decimal) (grupoCriterio
                                                    .Select(s => s.IdEsquemaEvaluacionPGeneralDetalle).Distinct()
                                                    .Count() * 1.0));

                    notaCurso.DetalleCalificacion.Add(detalleCriterio);
                }

                //calculo de la nota final
                if (notaCurso.DetalleCalificacion != null && notaCurso.DetalleCalificacion.Count > 0)
                {
                    notaCurso.NotaCurso = 0;
                    foreach (var calificacion in notaCurso.DetalleCalificacion)
                    {
                        notaCurso.NotaCurso += calificacion.Valor;
                    }

                    if (listadoEvaluaciones.FirstOrDefault().IdFormaCalculoEvaluacion_Esquema ==
                        (int) Enums.FormaCalculoEvaluacion.Promedio)
                        notaCurso.NotaCurso = Convert.ToDecimal(
                            ((double) notaCurso.NotaCurso * 1.0) / (notaCurso.DetalleCalificacion.Count * 1.0)
                        );
                }

                return notaCurso;
            }   
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla y Renato Escobar
        /// Fecha: 15/10/2021
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns>int</returns>
        public int CongelarMatricula() 
        {
      
            try
            {

                EsquemaEvaluacionRepositorio _repObjeto = new EsquemaEvaluacionRepositorio();

                var _objetoRegistro = _repObjeto.Congelar();

                return _objetoRegistro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }



        /// Autor: Max Mantilla y Renato Escobar
        /// Fecha: 15/10/2021
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns>int</returns>
        public int InsertarMatricula(List<ValorIdMatriculaDTO> Json)
        {
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                EsquemaEvaluacionRepositorio _repEsquemaRepositorio = new EsquemaEvaluacionRepositorio();
                foreach (var matriculas in Json)
                {
                    EsquemaEvaluacionRepositorio _repObjeto = new EsquemaEvaluacionRepositorio();
                    var matriculacabecera = _repMatriculaCabecera.FirstBy(x => x.Id == matriculas.IdMatriculaCabecera);
                    matriculas.Nuevo = _repObjeto.ExisteNuevaAulaVirtual(matriculacabecera.IdPespecifico);
                    //añadir el eliminar
                    _repEsquemaRepositorio.EliminarMatriculaCabecera(matriculas.IdMatriculaCabecera);
                   
                    if (matriculas.Nuevo == true)
                    {

                        _repEsquemaRepositorio.InsertarMatriculaNueva(matriculas.IdMatriculaCabecera);
                      
                    }
                    else
                    {
                        _repEsquemaRepositorio.InsertarMatriculaAntigua(matriculas.IdMatriculaCabecera);


                    }
                }

                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<CongelamientoPEspecificoMatriculaAlumnoDTO> ObtenerCongelamientoEsquemaPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                List<CongelamientoPEspecificoMatriculaAlumnoDTO> Esquemas = new List<CongelamientoPEspecificoMatriculaAlumnoDTO>();
                EsquemaEvaluacionRepositorio __repoEsquema = new EsquemaEvaluacionRepositorio();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltro = "Select * FROM [ope].[V_CongelamientoEsquemasEvaluacionAlumno] WHERE IdMatriculaCabecera=@IdMatriculaCabecera";
                var Subfiltro = _dapper.QueryDapper(_queryfiltro, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    Esquemas = JsonConvert.DeserializeObject<List<CongelamientoPEspecificoMatriculaAlumnoDTO>>(Subfiltro);
                }
                foreach (var esquema in Esquemas)
                {
                    _queryfiltro = "Select * FROM [ope].[V_CongelamientoPEspecificoEsquemasEvaluacionAlumno] WHERE IdProgramaGeneral=@IdProgramaGeneral AND  (IdMatriculaCabecera = 0 OR IdMatriculaCabecera=@IdMatriculaCabecera)";
                    Subfiltro = _dapper.QueryDapper(_queryfiltro, new { IdProgramaGeneral=esquema.IdProgramaGeneral, IdMatriculaCabecera= idMatriculaCabecera });
                    if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                    {
                        esquema.EsquemasEvaluacion = JsonConvert.DeserializeObject<List<EsquemaEvaluacionCongelado_ListadoDTO>>(Subfiltro);
                    }

                    foreach (var esquemaevaluacion in esquema.EsquemasEvaluacion)
                    {
                        if (esquemaevaluacion.IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno != 0)
                        {
                            _queryfiltro = "Select * FROM [ope].[V_CongelamientoEsquemasEvaluacionDetallesAlumno] WHERE IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno=@IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno";
                            Subfiltro = _dapper.QueryDapper(_queryfiltro, new { IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno = esquemaevaluacion.IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno });
                            if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                            {
                                esquemaevaluacion.EsquemasEvaluacionDetalle = JsonConvert.DeserializeObject<List<EsquemaEvaluacionDetalle_CongeladoDTO>>(Subfiltro);
                            }
                        }
                        else { 
                        
                        }
                    }
                };

                return Esquemas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int Congelar()
        {
            try
            {
                var registroDB = _dapper.QuerySPFirstOrDefault("ope.SP_CongelarEsquemasEvaluacionMasivo", new { });
                var valor = JsonConvert.DeserializeObject<ValorIntDTO>(registroDB);
                return valor.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

               
        public int InsertarMatriculaCabeceraEjemplo(List<ValorIdMatriculaDTO> Json)
        {
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                foreach (var matriculas in Json)
                {

                    var matriculacabecera = _repMatriculaCabecera.FirstBy(x => x.Id == matriculas.IdMatriculaCabecera);
                    matriculas.Nuevo = ExisteNuevaAulaVirtual(matriculacabecera.IdPespecifico);
                    if (matriculas.Nuevo == true)
                    {
                        var registroDB = _dapper.QuerySPFirstOrDefault("ope.SP_CongelarEsquemaEvaluacionMatriculaAlumno", new { idMatriculaCabecera = matriculas.IdMatriculaCabecera });
                    }
                    else
                    {
                        var registroDBAntiguo = _dapper.QuerySPFirstOrDefault("ope.SP_CongelarEsquemaEvaluacionMatriculaAlumnoAntiguo", new { idMatriculaCabecera = matriculas.IdMatriculaCabecera });
                        
                    }
                }
                
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ExisteNuevaAulaVirtual(int idPEspecifico)
        {
            try
            {
                var query = "SELECT Id FROM [pla].[V_TPEspecificoNuevoAulaVirtual_DataBasica] WHERE Id = @idPEspecifico";
                var resultado = _dapper.QueryDapper(query, new { idPEspecifico });

                return !string.IsNullOrEmpty(resultado) && !resultado.Contains("[]");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
