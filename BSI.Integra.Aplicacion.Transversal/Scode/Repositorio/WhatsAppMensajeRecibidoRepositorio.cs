using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class WhatsAppMensajeRecibidoRepositorio : BaseRepository<TWhatsAppMensajeRecibido, WhatsAppMensajeRecibidoBO>
    {

        #region Metodos Base
        public WhatsAppMensajeRecibidoRepositorio() : base()
        {
        }
        public WhatsAppMensajeRecibidoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppMensajeRecibidoBO> GetBy(Expression<Func<TWhatsAppMensajeRecibido, bool>> filter)
        {
            IEnumerable<TWhatsAppMensajeRecibido> listado = base.GetBy(filter);
            List<WhatsAppMensajeRecibidoBO> listadoBO = new List<WhatsAppMensajeRecibidoBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppMensajeRecibidoBO objetoBO = Mapper.Map<TWhatsAppMensajeRecibido, WhatsAppMensajeRecibidoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppMensajeRecibidoBO FirstById(int id)
        {
            try
            {
                TWhatsAppMensajeRecibido entidad = base.FirstById(id);
                WhatsAppMensajeRecibidoBO objetoBO = new WhatsAppMensajeRecibidoBO();
                Mapper.Map<TWhatsAppMensajeRecibido, WhatsAppMensajeRecibidoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppMensajeRecibidoBO FirstBy(Expression<Func<TWhatsAppMensajeRecibido, bool>> filter)
        {
            try
            {
                TWhatsAppMensajeRecibido entidad = base.FirstBy(filter);
                WhatsAppMensajeRecibidoBO objetoBO = Mapper.Map<TWhatsAppMensajeRecibido, WhatsAppMensajeRecibidoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppMensajeRecibidoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppMensajeRecibido entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<WhatsAppMensajeRecibidoBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(WhatsAppMensajeRecibidoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppMensajeRecibido entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<WhatsAppMensajeRecibidoBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TWhatsAppMensajeRecibido entidad, WhatsAppMensajeRecibidoBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TWhatsAppMensajeRecibido MapeoEntidad(WhatsAppMensajeRecibidoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppMensajeRecibido entidad = new TWhatsAppMensajeRecibido();
                entidad = Mapper.Map<WhatsAppMensajeRecibidoBO, TWhatsAppMensajeRecibido>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        public string guardarArchivos(byte[] archivo, string carpetaArchivo, string tipo, string nombreArchivo,  int IdPais)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"repositorioweb/whatsapp/";

                    switch (IdPais)
                    {
                        case 51: // Peru
                            _direccionBlob += "peru/" + carpetaArchivo;
                            break;
                        case 57: // Colombia
                            _direccionBlob += "colombia/" + carpetaArchivo;
                            break;
                        case 591: // Bolivia
                            _direccionBlob += "bolivia/" + carpetaArchivo;
                            break;
                        case 0: // Internacional
                            _direccionBlob += "internacional/" + carpetaArchivo;
                            break;
                        default:
                            _direccionBlob += "pruebas/" + carpetaArchivo;
                            break;
                    }

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = tipo;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = tipo;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    blockBlob.UploadFromStreamAsync(stream);

                    _nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + "/" + nombreArchivo.Replace(" ","%20");

                    

                }
                catch (Exception ex)
                {
                    //Logger.Error(ex.ToString());
                }
                return _nombreLink;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string guardarArchivos(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"repositorioweb/whatsapp/enviados/";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = tipo;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = tipo;
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
                    return "";
                }
                
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return "";
            }
        }

    }
}
