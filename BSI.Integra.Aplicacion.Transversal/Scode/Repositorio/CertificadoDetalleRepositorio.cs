using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
    public class CertificadoDetalleRepositorio : BaseRepository<TCertificadoDetalle, CertificadoDetalleBO>
    {
        #region Metodos Base
        public CertificadoDetalleRepositorio() : base()
        {
        }
        public CertificadoDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoDetalleBO> GetBy(Expression<Func<TCertificadoDetalle, bool>> filter)
        {
            IEnumerable<TCertificadoDetalle> listado = base.GetBy(filter);
            List<CertificadoDetalleBO> listadoBO = new List<CertificadoDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoDetalleBO objetoBO = Mapper.Map<TCertificadoDetalle, CertificadoDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoDetalleBO FirstById(int id)
        {
            try
            {
                TCertificadoDetalle entidad = base.FirstById(id);
                CertificadoDetalleBO objetoBO = new CertificadoDetalleBO();
                Mapper.Map<TCertificadoDetalle, CertificadoDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoDetalleBO FirstBy(Expression<Func<TCertificadoDetalle, bool>> filter)
        {
            try
            {
                TCertificadoDetalle entidad = base.FirstBy(filter);
                CertificadoDetalleBO objetoBO = Mapper.Map<TCertificadoDetalle, CertificadoDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificadoDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoDetalleBO> listadoBO)
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

        public bool Update(CertificadoDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoDetalleBO> listadoBO)
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
        private void AsignacionId(TCertificadoDetalle entidad, CertificadoDetalleBO objetoBO)
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

        private TCertificadoDetalle MapeoEntidad(CertificadoDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoDetalle entidad = new TCertificadoDetalle();
                entidad = Mapper.Map<CertificadoDetalleBO, TCertificadoDetalle>(objetoBO,
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

        public object ObtenerTodo(Paginador paginador, GridFilters filtroGrilla)
        {
            try
            {
                string Condicion = "";
                string NombreCentroCosto = "";
                string NombreAlumno = "";
                string CodigoMatricula = "";

                if (filtroGrilla != null)
                {

                    foreach (var item in filtroGrilla.Filters)
                    {
                        if (item.Field == "NombreCentroCosto" && item.Value.Contains(""))
                        {
                            Condicion = Condicion + " and NombreCentroCosto like @NombreCentroCosto ";
                            NombreCentroCosto = item.Value;
                        }
                        if (item.Field == "CodigoMatricula" && item.Value.Contains(""))
                        {
                            Condicion = Condicion + " and CodigoMatricula like @CodigoMatricula ";
                            CodigoMatricula = item.Value;
                        }
                        if (item.Field == "NombreAlumno" && item.Value.Contains(""))
                        {
                            Condicion = Condicion + " and NombreAlumno like @NombreAlumno ";
                            NombreAlumno = item.Value;
                        }
                    }
                }

                List<CertificadoDetalleDTO> _certificadoDetalle = new List<CertificadoDetalleDTO>();
                var _query = string.Empty;
                if (Condicion != "")
                {
                    if (paginador != null && paginador.take != 0)
                    {
                        _query = "SELECT Id,IdCertificadoBrochure,IdCertificadoSolicitud,NumeroSolicitud,IdCertificadoTipo,NombreCertificadoTipo,EsDiploma,IdMatriculaCabecera, " +
                        " CodigoMatricula,NombreAlumno,NombreCentroCosto,CodigoCertificado,FechaInicio,FechaTermino,Nota,FechaEnvio, " +
                        " FechaRecepcion,EscalaCalificacion,FechaEmision,TamanioFuenteNombreAlumno,TamanioFuenteNombrePrograma, " +
                        " NombreArchivoFrontal,NombreArchivoReverso,NombreArchivoFrontalImpresion,NombreArchivoReversoImpresion,RutaArchivo,NombreArchivoPartner, " +
                        " IdUrlBlockStorage,ContentType,DireccionEntrega,FechaUltimoEnvioAlumno,IdCertificadoTipoPrograma,EsAsistenciaPartner,NombreEstadoMatricula,UsuarioCoordinadorAcademico, " +
                        " AplicaPartner,IdPespecifico FROM ope.V_CertificadoDetalle WHERE Id Is not null " + Condicion + " order by Id desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                        var CertificadoSolicitud = _dapper.QueryDapper(_query, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take });
                        if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                        {
                            _certificadoDetalle = JsonConvert.DeserializeObject<List<CertificadoDetalleDTO>>(CertificadoSolicitud);
                            var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) FROM ope.V_CertificadoDetalle WHERE Estado=1 and FechaEmision Is not null " + Condicion, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take }));

                            return new { data = _certificadoDetalle, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                        }
                    }
                    else
                    {
                        _query = "SELECT Id,IdCertificadoBrochure,IdCertificadoSolicitud,NumeroSolicitud,IdCertificadoTipo,NombreCertificadoTipo,EsDiploma,IdMatriculaCabecera, " +
                        " CodigoMatricula,NombreAlumno,NombreCentroCosto,CodigoCertificado,FechaInicio,FechaTermino,Nota,FechaEnvio, " +
                        " FechaRecepcion,EscalaCalificacion,FechaEmision,TamanioFuenteNombreAlumno,TamanioFuenteNombrePrograma, " +
                        " NombreArchivoFrontal,NombreArchivoReverso,NombreArchivoFrontalImpresion,NombreArchivoReversoImpresion,RutaArchivo,NombreArchivoPartner, " +
                        " IdUrlBlockStorage,ContentType,DireccionEntrega,FechaUltimoEnvioAlumno,IdCertificadoTipoPrograma,EsAsistenciaPartner,NombreEstadoMatricula,UsuarioCoordinadorAcademico, " +
                        " AplicaPartner,IdPespecifico FROM ope.V_CertificadoDetalle WHERE Id Is not null Order by Id desc " + Condicion;
                        var CertificadoSolicitud = _dapper.QueryDapper(_query, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take });
                        if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                        {
                            _certificadoDetalle = JsonConvert.DeserializeObject<List<CertificadoDetalleDTO>>(CertificadoSolicitud);
                            var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) FROM ope.V_CertificadoDetalle WHERE Estado=1 and FechaEmision Is not null " + Condicion, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take }));

                            return new { data = _certificadoDetalle, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                        }
                    }
                }
                if (paginador != null && paginador.take != 0)
                {
                    _query = "SELECT Id,IdCertificadoBrochure,IdCertificadoSolicitud,NumeroSolicitud,IdCertificadoTipo,NombreCertificadoTipo,EsDiploma,IdMatriculaCabecera, " +
                    " CodigoMatricula,NombreAlumno,NombreCentroCosto,CodigoCertificado,FechaInicio,FechaTermino,Nota,FechaEnvio, " +
                    " FechaRecepcion,EscalaCalificacion,FechaEmision,TamanioFuenteNombreAlumno,TamanioFuenteNombrePrograma, " +
                    " NombreArchivoFrontal,NombreArchivoReverso,NombreArchivoFrontalImpresion,NombreArchivoReversoImpresion,RutaArchivo,NombreArchivoPartner, " +
                    " IdUrlBlockStorage,ContentType,DireccionEntrega,FechaUltimoEnvioAlumno,IdCertificadoTipoPrograma,EsAsistenciaPartner,NombreEstadoMatricula,UsuarioCoordinadorAcademico, " +
                    " AplicaPartner,IdPespecifico FROM ope.V_CertificadoDetalle WHERE FechaEmision Is Null " + Condicion + " order by Id desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                    var CertificadoSolicitud = _dapper.QueryDapper(_query, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take });
                    if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                    {
                        _certificadoDetalle = JsonConvert.DeserializeObject<List<CertificadoDetalleDTO>>(CertificadoSolicitud);
                        var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) FROM ope.V_CertificadoDetalle WHERE Estado=1 and FechaEmision Is not null " + Condicion, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take }));

                        return new { data = _certificadoDetalle, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                    }
                }
                else
                {
                    _query = "SELECT Id,IdCertificadoBrochure,IdCertificadoSolicitud,NumeroSolicitud,IdCertificadoTipo,NombreCertificadoTipo,EsDiploma,IdMatriculaCabecera, " +
                    " CodigoMatricula,NombreAlumno,NombreCentroCosto,CodigoCertificado,FechaInicio,FechaTermino,Nota,FechaEnvio, " +
                    " FechaRecepcion,EscalaCalificacion,FechaEmision,TamanioFuenteNombreAlumno,TamanioFuenteNombrePrograma, " +
                    " NombreArchivoFrontal,NombreArchivoReverso,NombreArchivoFrontalImpresion,NombreArchivoReversoImpresion,RutaArchivo,NombreArchivoPartner, " +
                    " IdUrlBlockStorage,ContentType,DireccionEntrega,FechaUltimoEnvioAlumno,IdCertificadoTipoPrograma,EsAsistenciaPartner,NombreEstadoMatricula,UsuarioCoordinadorAcademico, " +
                    " AplicaPartner,IdPespecifico FROM ope.V_CertificadoDetalle WHERE FechaEmision Is Null Order by Id desc " + Condicion ;
                    var CertificadoSolicitud = _dapper.QueryDapper(_query, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take });
                    if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                    {
                        _certificadoDetalle = JsonConvert.DeserializeObject<List<CertificadoDetalleDTO>>(CertificadoSolicitud);
                        var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) FROM ope.V_CertificadoDetalle WHERE Estado=1 and FechaEmision Is not null " + Condicion, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take }));

                        return new { data = _certificadoDetalle, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                    }
                }



                return new { data = _certificadoDetalle, Total = 0 };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public CertificadoCaraFrontalDTO ObtenerDatosAlumnoCertificado(int IdCertificadoDetalle)
        {
            try
            {
                CertificadoCaraFrontalDTO _certificadoDetalle = new CertificadoCaraFrontalDTO();
                var _query = string.Empty;
                _query = "Select IdCertificadoDetalle,IdCertificadoTipo,NombreCertificadoTipo,CodigoMatricula,CodigoCertificado,ApellidoPaterno,ApellidoMaterno," +
                    " Nombre1,Nombre2,IdCentroCosto,NombreCentroCosto,IdCertificadoTipoPrograma,IdPespecifico,FechaEmision,TamanioFuenteNombreAlumno," +
                    " TamanioFuenteNombrePrograma,Nota,EscalaCalificacion,EsAsistenciaPartner,FechaInicio,FechaTermino,CodigoCertificado" +
                    " FROM ope.V_ObtenerCertificadoDetalleDocumento WHERE IdCertificadoDetalle = @IdCertificadoDetalle";
                var CertificadoSolicitud = _dapper.FirstOrDefault(_query, new { IdCertificadoDetalle });
                if (!string.IsNullOrEmpty(CertificadoSolicitud) && CertificadoSolicitud != "null")
                {
                    _certificadoDetalle = JsonConvert.DeserializeObject<CertificadoCaraFrontalDTO>(CertificadoSolicitud);
                }

                return _certificadoDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string guardarArchivoCertificado(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"operaciones/comprobantes/";

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

        public string guardarArchivoCertificadoFisico(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"operaciones/CertificadoSinFondo/";

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

        public DatosAlumnoEnvioCertificadoDTO ObtenerDatosAlumnoEnvio(int IdCertificadoDetalle)
        {
            try
            {
                DatosAlumnoEnvioCertificadoDTO _certificadoDetalle = new DatosAlumnoEnvioCertificadoDTO();
                var _query = string.Empty;
                _query = "Select CodigoMatricula,ApellidoPaterno,ApellidoMaterno," +
                    " Nombre1,Nombre2,Email1,Email2,Genero,Email,Nombres,Apellidos,IdCentroCosto,IdCiudad,IdCodigoPais,NombreCentroCosto,UrlDocumento,UrlFoto " +
                    " FROM ope.V_ObtenerDatosParaEnvioCertificado WHERE IdCertificadoDetalle = @IdCertificadoDetalle";
                var CertificadoSolicitud = _dapper.FirstOrDefault(_query, new { IdCertificadoDetalle });
                if (!string.IsNullOrEmpty(CertificadoSolicitud) && CertificadoSolicitud != "null")
                {
                    _certificadoDetalle = JsonConvert.DeserializeObject<DatosAlumnoEnvioCertificadoDTO>(CertificadoSolicitud);
                }

                return _certificadoDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
