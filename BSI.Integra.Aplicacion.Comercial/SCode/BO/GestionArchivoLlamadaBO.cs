﻿using BSI.Integra.Aplicacion.Base.BO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class GestionArchivoLlamadaBO : BaseBO
    {
        /// <summary>
        /// Sube el archivo en Azure Blob Storage
        /// </summary>
        /// <param name="archivo">Archivo a subir</param>
        /// <param name="mimeType">Tipo de mime del archivo a subir</param>
        /// <param name="nombreArchivo">Nombre del archivo a subir</param>
        /// <param name="rutaCompleta">Ruta del archivo completa</param>
        /// <param name="rutaBlob">Ruta del blob</param>
        /// <returns>Cadena del archivo subido en Azure Blog Storage</returns>
        public string SubirArchivo(byte[] archivo, string mimeType, string nombreArchivo, string rutaCompleta, string rutaBlob)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioaudiollamada;AccountKey=I36vJHayP1gja6+IsxnQAFGXHzgp9hn6zOxZ/hq4C7b2roXMdOdpke64UV8c+Lsdc0ekzAEJU0rB0Rr03Awp0w==;EndpointSuffix=core.windows.net";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(rutaBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = mimeType;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = mimeType;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    _nombreLink = correcto ? rutaCompleta + nombreArchivo : string.Empty;

                    return _nombreLink.Replace(" ", "%20");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
