using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MaterialVersionRepositorio : BaseRepository<TMaterialVersion, MaterialVersionBO>
    {
        #region Metodos Base
        public MaterialVersionRepositorio() : base()
        {
        }
        public MaterialVersionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialVersionBO> GetBy(Expression<Func<TMaterialVersion, bool>> filter)
        {
            IEnumerable<TMaterialVersion> listado = base.GetBy(filter);
            List<MaterialVersionBO> listadoBO = new List<MaterialVersionBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialVersionBO objetoBO = Mapper.Map<TMaterialVersion, MaterialVersionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialVersionBO FirstById(int id)
        {
            try
            {
                TMaterialVersion entidad = base.FirstById(id);
                MaterialVersionBO objetoBO = new MaterialVersionBO();
                Mapper.Map<TMaterialVersion, MaterialVersionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialVersionBO FirstBy(Expression<Func<TMaterialVersion, bool>> filter)
        {
            try
            {
                TMaterialVersion entidad = base.FirstBy(filter);
                MaterialVersionBO objetoBO = Mapper.Map<TMaterialVersion, MaterialVersionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialVersionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialVersion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialVersionBO> listadoBO)
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

        public bool Update(MaterialVersionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialVersion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialVersionBO> listadoBO)
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
        private void AsignacionId(TMaterialVersion entidad, MaterialVersionBO objetoBO)
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

        private TMaterialVersion MapeoEntidad(MaterialVersionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialVersion entidad = new TMaterialVersion();
                entidad = Mapper.Map<MaterialVersionBO, TMaterialVersion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialVersionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialVersion, bool>>> filters, Expression<Func<TMaterialVersion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialVersion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialVersionBO> listadoBO = new List<MaterialVersionBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialVersionBO objetoBO = Mapper.Map<TMaterialVersion, MaterialVersionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene para filtro
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro() {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la lista
        /// </summary>
        /// <returns></returns>
        public List<MaterialVersionBO> Obtener()
        {
            try
            {
                return this.GetBy(x => x.Estado).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Obtiene para filtro por programa especifico y grupo
        /// </summary>
        /// <returns></returns>
        public List<MaterialPEspecificoGrupoDTO> ObtenerTodoFiltroPorProgramaEspecificoGrupo(int idPEspecifico, int idGrupo)
        {
            try
            {
                var listaMateriales = new List<MaterialPEspecificoGrupoDTO>();
                var query = $@"
                        SELECT Id, 
                               Nombre, 
                               IdPEspecifico, 
                               IdGrupo
                        FROM ope.V_ObtenerMaterialPorProgramaEspecifico
                        WHERE EstadoMaterialPEspecificoSesion = 1
                              AND EstadoPEspecifico = 1
                              AND EstadoMaterialVersion = 1
                              AND EstadoPEspecificoSesion = 1
                              AND (EstadoTipoPersona = 1
                                   OR EstadoTipoPersona IS NULL)
                              AND IdPEspecifico = @idPEspecifico
                              AND IdGrupo = @idGrupo
                ";
                var resultadoDB = _dapper.QueryDapper(query, new { idPEspecifico, idGrupo });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    listaMateriales = JsonConvert.DeserializeObject<List<MaterialPEspecificoGrupoDTO>>(resultadoDB);
                }
                return listaMateriales;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene para filtro programa especifico y grupo
        /// </summary>
        /// <returns></returns>
        public List<MaterialPEspecificoGrupoDTO> ObtenerTodoFiltroPorProgramaEspecificoGrupo()
        {
            try
            {
                var listaMateriales = new List<MaterialPEspecificoGrupoDTO>();
                var query = $@"
                        SELECT Id, 
                               Nombre, 
                               IdPEspecifico, 
                               IdGrupo
                        FROM ope.V_ObtenerMaterialPorProgramaEspecifico
                        WHERE EstadoMaterialPEspecificoSesion = 1
                              AND EstadoPEspecifico = 1
                              AND EstadoMaterialVersion = 1
                              AND EstadoPEspecificoSesion = 1
                              AND (EstadoTipoPersona = 1
                                   OR EstadoTipoPersona IS NULL)
                ";
                var resultadoDB = _dapper.QueryDapper(query, new { });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    listaMateriales = JsonConvert.DeserializeObject<List<MaterialPEspecificoGrupoDTO>>(resultadoDB);
                }
                return listaMateriales;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///// <summary>
        ///// Obtiene todos los materiales asociados
        ///// </summary>
        ///// <param name="idMaterialPEspecificoSesion"></param>
        ///// <returns></returns>
        //public List<MaterialVersionBO> ObtenerPorMaterialPEspecificoSesion(int idMaterialPEspecificoSesion) {
        //    try
        //    {
        //        return this.GetBy(x => x.Estado && x.IdMaterialPespecificoSesion == idMaterialPEspecificoSesion).ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

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

                    string _direccionBlob = @"operaciones/materiales/";

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
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public string SubirDocumentosOportunidadRepositorio(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;
                //Elimina los caracteres con tilde
                nombreArchivo = nombreArchivo.Replace("á", "a");
                nombreArchivo = nombreArchivo.Replace("é", "e");
                nombreArchivo = nombreArchivo.Replace("í", "i");
                nombreArchivo = nombreArchivo.Replace("ó", "o");
                nombreArchivo = nombreArchivo.Replace("ú", "u");

                nombreArchivo = nombreArchivo.Replace("Á", "A");
                nombreArchivo = nombreArchivo.Replace("É", "E");
                nombreArchivo = nombreArchivo.Replace("Í", "I");
                nombreArchivo = nombreArchivo.Replace("Ó", "O");
                nombreArchivo = nombreArchivo.Replace("Ú", "U");

                //Elimina las Ñ
                nombreArchivo = nombreArchivo.Replace("ñ", "n");
                nombreArchivo = nombreArchivo.Replace("Ñ", "N");


                //Elimina los caracteres con tilde
                nombreArchivo = nombreArchivo.Replace("á", "a");
                nombreArchivo = nombreArchivo.Replace("é", "e");
                nombreArchivo = nombreArchivo.Replace("í", "i");
                nombreArchivo = nombreArchivo.Replace("ó", "o");
                nombreArchivo = nombreArchivo.Replace("ú", "u");

                nombreArchivo = nombreArchivo.Replace("Á", "A");
                nombreArchivo = nombreArchivo.Replace("É", "E");
                nombreArchivo = nombreArchivo.Replace("Í", "I");
                nombreArchivo = nombreArchivo.Replace("Ó", "O");
                nombreArchivo = nombreArchivo.Replace("Ú", "U");

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"repositorioweb/Ventas/";

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

        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public string SubirDocumentosProyectoAplicacionRepositorio(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"operaciones/proyectoaplicacion/";

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
