using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ParametroEvaluacionNotaBO : BaseBO
    {
        public int IdPespecifico { get; set; }
        public int Grupo { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdEsquemaEvaluacionPGeneralDetalle { get; set; }
        public int IdParametroEvaluacion { get; set; }
        public int? IdEscalaCalificacionDetalle { get; set; }
        public string Retroalimentacion { get; set; }
        public string NombreArchivoRetroalimentacion { get; set; }
        public string UrlArchivoSubidoRetroalimentacion { get; set; }

        public string SubirArchivo(byte[] archivo, string mimeType, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"operaciones/retroalimentacion-calificacion/";
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
    }
}
