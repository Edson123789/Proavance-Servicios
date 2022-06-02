using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.DTO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/PgeneralProyectoAplicacionAnexo
    /// Autor: Lourdes Priscila Pacsi Gamboa
    /// Fecha: 02/07/2021
    /// <summary>
    /// Repositorio para consultas de fin.PgeneralProyectoAplicacionAnexo
    /// </summary>
    /// 
    public class PgeneralProyectoAplicacionAnexoRepositorio : BaseRepository<TPgeneralProyectoAplicacionAnexo, PgeneralProyectoAplicacionAnexoBO>
    {
        #region Metodos Base
        public PgeneralProyectoAplicacionAnexoRepositorio() : base()
        {
        }
        public PgeneralProyectoAplicacionAnexoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralProyectoAplicacionAnexoBO> GetBy(Expression<Func<TPgeneralProyectoAplicacionAnexo, bool>> filter)
        {
            IEnumerable<TPgeneralProyectoAplicacionAnexo> listado = base.GetBy(filter);
            List<PgeneralProyectoAplicacionAnexoBO> listadoBO = new List<PgeneralProyectoAplicacionAnexoBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralProyectoAplicacionAnexoBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacionAnexo, PgeneralProyectoAplicacionAnexoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralProyectoAplicacionAnexoBO FirstById(int id)
        {
            try
            {
                TPgeneralProyectoAplicacionAnexo entidad = base.FirstById(id);
                PgeneralProyectoAplicacionAnexoBO objetoBO = new PgeneralProyectoAplicacionAnexoBO();
                Mapper.Map<TPgeneralProyectoAplicacionAnexo, PgeneralProyectoAplicacionAnexoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralProyectoAplicacionAnexoBO FirstBy(Expression<Func<TPgeneralProyectoAplicacionAnexo, bool>> filter)
        {
            try
            {
                TPgeneralProyectoAplicacionAnexo entidad = base.FirstBy(filter);
                PgeneralProyectoAplicacionAnexoBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacionAnexo, PgeneralProyectoAplicacionAnexoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralProyectoAplicacionAnexoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralProyectoAplicacionAnexo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralProyectoAplicacionAnexoBO> listadoBO)
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

        public bool Update(PgeneralProyectoAplicacionAnexoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralProyectoAplicacionAnexo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralProyectoAplicacionAnexoBO> listadoBO)
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
        private void AsignacionId(TPgeneralProyectoAplicacionAnexo entidad, PgeneralProyectoAplicacionAnexoBO objetoBO)
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

        private TPgeneralProyectoAplicacionAnexo MapeoEntidad(PgeneralProyectoAplicacionAnexoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralProyectoAplicacionAnexo entidad = new TPgeneralProyectoAplicacionAnexo();
                entidad = Mapper.Map<PgeneralProyectoAplicacionAnexoBO, TPgeneralProyectoAplicacionAnexo>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos


                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        public List<PgeneralProyectoAplicacionAnexoDTO> ObtenerListaPgeneralProyectoAplicacionAnexo(int Id)
        {
            try
            {
                List<PgeneralProyectoAplicacionAnexoDTO> Filtro = new List<PgeneralProyectoAplicacionAnexoDTO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltro = "Select * FROM [ope].[V_ObtenerDatosPgeneralProyectoAplicacionAnexo] where IdPgeneral = @Id";
                var Subfiltro = _dapper.QueryDapper(_queryfiltro, new { Id });
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    Filtro = JsonConvert.DeserializeObject<List<PgeneralProyectoAplicacionAnexoDTO>>(Subfiltro);
                }
                return Filtro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public string GuardarArchivo(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"repositorioweb/aulavirtual/anexosproyectos/";

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
