using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class GmailCorreoRepositorio : BaseRepository<TGmailCorreo, GmailCorreoBO>
    {
        #region Metodos Base
        public GmailCorreoRepositorio() : base()
        {
        }
        public GmailCorreoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<GmailCorreoBO> GetBy(Expression<Func<TGmailCorreo, bool>> filter)
        {
            IEnumerable<TGmailCorreo> listado = base.GetBy(filter);
            List<GmailCorreoBO> listadoBO = new List<GmailCorreoBO>();
            foreach (var itemEntidad in listado)
            {
                GmailCorreoBO objetoBO = Mapper.Map<TGmailCorreo, GmailCorreoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public GmailCorreoBO FirstById(int id)
        {
            try
            {
                TGmailCorreo entidad = base.FirstById(id);
                GmailCorreoBO objetoBO = new GmailCorreoBO();
                Mapper.Map<TGmailCorreo, GmailCorreoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GmailCorreoBO FirstBy(Expression<Func<TGmailCorreo, bool>> filter)
        {
            try
            {
                TGmailCorreo entidad = base.FirstBy(filter);
                GmailCorreoBO objetoBO = Mapper.Map<TGmailCorreo, GmailCorreoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(GmailCorreoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TGmailCorreo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<GmailCorreoBO> listadoBO)
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

        public bool Update(GmailCorreoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TGmailCorreo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<GmailCorreoBO> listadoBO)
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
        private void AsignacionId(TGmailCorreo entidad, GmailCorreoBO objetoBO)
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

        private TGmailCorreo MapeoEntidad(GmailCorreoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TGmailCorreo entidad = new TGmailCorreo();
                entidad = Mapper.Map<GmailCorreoBO, TGmailCorreo>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaGmailCorreoArchivoAdjunto != null && objetoBO.ListaGmailCorreoArchivoAdjunto.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaGmailCorreoArchivoAdjunto)
                    {
                        TGmailCorreoArchivoAdjunto entidadHijo = new TGmailCorreoArchivoAdjunto();
                        entidadHijo = Mapper.Map<GmailCorreoArchivoAdjuntoBO, TGmailCorreoArchivoAdjunto>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TGmailCorreoArchivoAdjunto.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene todos los correos enviados por el asesor segun un skip y take
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public BandejaCorreoDTO ObtenerCorreosEnviados(FiltroBandejaCorreoBO filtroBandejaCorreo, GridFiltersDTO filter = null)
        {
            try
            {
                string filtro_subject = string.Empty, filtro_des = string.Empty, filtro_rem = string.Empty;
                if (filtroBandejaCorreo.FiltroKendo != null)
                {
                    foreach (var item in filtroBandejaCorreo.FiltroKendo.Filters)
                    {
                        switch (item.Field)
                        {
                            case "Asunto":
                                filtro_subject = item.Value;
                                break;
                            case "Destinatarios":
                                filtro_des = item.Value;
                                break;
                            case "Destinatario":
                                filtro_des = item.Value;
                                break;
                            case "Remitente":
                                filtro_rem = item.Value;
                                break;
                        }
                    }
                }
                if (filtroBandejaCorreo.Take == 0)
                {
                    filtroBandejaCorreo.Take = 10000;
                }
                var _correosEnviados = "";
                if (filtroBandejaCorreo.TipoCorreos == "Normal")
                {
                    filtroBandejaCorreo.IdAsesor = 0;
                    _correosEnviados = _dapper.QuerySPDapper("com.SP_ObtenerCorreosEnviadosPorPersonal", new { IdPersonal = filtroBandejaCorreo.IdAsesor, skip = filtroBandejaCorreo.Skip, take = filtroBandejaCorreo.Take, Destinatarios = filtro_des, Asunto = filtro_subject, Remitente = filtro_rem });
                }
                else { 
                    _correosEnviados = _dapper.QuerySPDapper("com.SP_ObtenerCorreosEnviadosPorPersonal", new { IdPersonal = filtroBandejaCorreo.IdAsesor, skip = filtroBandejaCorreo.Skip, take = filtroBandejaCorreo.Take, Destinatarios = filtro_des, Asunto = filtro_subject, Remitente = filtro_rem });
                }

                BandejaCorreoDTO bandejaSalidaDTO = new BandejaCorreoDTO();
                if (_correosEnviados != "null" && !_correosEnviados.Contains("{}") && !_correosEnviados.Contains("[]"))
                {
                    
                    bandejaSalidaDTO.ListaCorreos = JsonConvert.DeserializeObject<List<CorreoDTO>>(_correosEnviados);
                    bandejaSalidaDTO.TotalEnviados = bandejaSalidaDTO.ListaCorreos[0].TotalCorreos ?? 0;
                    return bandejaSalidaDTO;
                }
                bandejaSalidaDTO.TotalEnviados = 0;
                return bandejaSalidaDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }                
        }

        /// <summary>
        /// Obtiene el cuerpo de un correo, filtrado por el Id de correo
        /// </summary>
        /// <param name="idGmailCorreo"></param>
        public CorreoBodyDTO ObtenerCuerpoCorreoEnviado(int idGmailCorreo)
        {
            try
            {
                string _query = "SELECT EmailBody FROM mkt.V_TGmailCorreo_EmailMensaje WHERE Id = @IdGmailCorreo";
                string RegistroBO = _dapper.FirstOrDefault(_query, new { idGmailCorreo });
                if (!string.IsNullOrEmpty(RegistroBO) && !RegistroBO.Contains("{}"))
                {
                    CorreoBodyDTO correoBodyDTO = JsonConvert.DeserializeObject<CorreoBodyDTO>(RegistroBO);
                    return correoBodyDTO;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public string SubirArchivoRepositorio(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {

                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"operaciones/comprobantes/";
                    //string _direccionBlob = @"correos/individuales/";

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
