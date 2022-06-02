using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: SolicitudOperacionesRepositorio
    /// Autor:Jose Villena
    /// Fecha: 03/05/2021
    /// <summary>
    /// Gestión de SolicitudOperaciones
    /// </summary>
    public class SolicitudOperacionesRepositorio : BaseRepository<TSolicitudOperaciones, SolicitudOperacionesBO>
    {
        #region Metodos Base
        public SolicitudOperacionesRepositorio() : base()
        {
        }
        public SolicitudOperacionesRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SolicitudOperacionesBO> GetBy(Expression<Func<TSolicitudOperaciones, bool>> filter)
        {
            IEnumerable<TSolicitudOperaciones> listado = base.GetBy(filter);
            List<SolicitudOperacionesBO> listadoBO = new List<SolicitudOperacionesBO>();
            foreach (var itemEntidad in listado)
            {
                SolicitudOperacionesBO objetoBO = Mapper.Map<TSolicitudOperaciones, SolicitudOperacionesBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SolicitudOperacionesBO FirstById(int id)
        {
            try
            {
                TSolicitudOperaciones entidad = base.FirstById(id);
                SolicitudOperacionesBO objetoBO = new SolicitudOperacionesBO();
                Mapper.Map<TSolicitudOperaciones, SolicitudOperacionesBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SolicitudOperacionesBO FirstBy(Expression<Func<TSolicitudOperaciones, bool>> filter)
        {
            try
            {
                TSolicitudOperaciones entidad = base.FirstBy(filter);
                SolicitudOperacionesBO objetoBO = Mapper.Map<TSolicitudOperaciones, SolicitudOperacionesBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SolicitudOperacionesBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSolicitudOperaciones entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SolicitudOperacionesBO> listadoBO)
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

        public bool Update(SolicitudOperacionesBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSolicitudOperaciones entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SolicitudOperacionesBO> listadoBO)
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
        private void AsignacionId(TSolicitudOperaciones entidad, SolicitudOperacionesBO objetoBO)
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

        private TSolicitudOperaciones MapeoEntidad(SolicitudOperacionesBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSolicitudOperaciones entidad = new TSolicitudOperaciones();
                entidad = Mapper.Map<SolicitudOperacionesBO, TSolicitudOperaciones>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion}

        ///Repositorio: SolicitudOperacionesRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtener solicitudes operaciones
        /// </summary>
        /// <param name="IdOportunidad">Id de oportunidad </param>
        /// <returns> Lista Datos solicitudes operaciones: List<DatosSolicitudOperacionesDTO></returns>        
        public List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperaciones(int IdOportunidad)
        {
            try
            {
                List<DatosSolicitudOperacionesDTO> datosSolicitudOperaciones = new List<DatosSolicitudOperacionesDTO>();
                string _querySolicitud = "Select Id,IdOportunidad,IdTipoSolicitudOperaciones,TipoSolicitudOperaciones,FechaSolicitud,IdPersonalSolicitante," +
                                         "PersonalSolicitante,IdPersonalAprobacion,PersonalAprobacion,ValorAnterior,ValorNuevo,Aprobado,EsCancelado,ComentarioSolicitante" +
                                         ",Observacion,IdUrlBlockStorage,UrlBlockStorage,NombreArchivo,ContentType,Realizado,ObservacionEncargado,FechaAprobacion " +
                                         "from ope.V_ObtenerSolicitudOperaciones where IdOportunidad=@IdOportunidad and  Aprobado = 0 and EsCancelado = 0 and Realizado = 0 order by FechaSolicitud desc";
                string querySolicitud = _dapper.QueryDapper(_querySolicitud, new { IdOportunidad });

                if (!querySolicitud.Contains("[]") && querySolicitud != "")
                {
                    datosSolicitudOperaciones = JsonConvert.DeserializeObject<List<DatosSolicitudOperacionesDTO>>(querySolicitud);
                }

                return datosSolicitudOperaciones;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
		/// Se obtiene el historial de acceso temporal por IdOportunidad
		/// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
		/// <returns>Lista de datos de solicitud de operaciones</returns>
        public List<DatosSolicitudOperacionesDTO> ObtenerHistorialAccesoTemporal(int idOportunidad)
        {
            try
            {
                List<DatosSolicitudOperacionesDTO> listaSolicitudOperaciones = new List<DatosSolicitudOperacionesDTO>();

                var registrosBO = _dapper.QuerySPDapper("ope.SP_ObtenerSolicitudOportunidadAccesoTemporal", new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
                {
                    listaSolicitudOperaciones = JsonConvert.DeserializeObject<List<DatosSolicitudOperacionesDTO>>(registrosBO);
                }
                return listaSolicitudOperaciones;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Registra en el nuevo aula virtual los cursos de prueba segun la solicitud ingresada
        /// </summary>
        /// <param name="idSolicitudOperaciones">Id de la solicitud de operaciones (PK de la tabla ope.T_SolicitudOperaciones)</param>
        public void RegistrarCursoPrueba(int idSolicitudOperaciones)
        {
            try
            {
                _dapper.QuerySPDapper("ope.SP_RegistrarAccesoPruebaSolicitud", new { IdSolicitudOperacion = idSolicitudOperaciones });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperacionesRealizadas(int IdOportunidad)
        {
            try
            {
                List<DatosSolicitudOperacionesDTO> datosSolicitudOperaciones = new List<DatosSolicitudOperacionesDTO>();
                string _querySolicitud = "Select Id,IdOportunidad,IdTipoSolicitudOperaciones,TipoSolicitudOperaciones,FechaSolicitud,IdPersonalSolicitante," +
                                         "PersonalSolicitante,IdPersonalAprobacion,PersonalAprobacion,ValorAnterior,ValorNuevo,Aprobado,EsCancelado,ComentarioSolicitante" +
                                         ",Observacion,IdUrlBlockStorage,UrlBlockStorage,NombreArchivo,ContentType,Realizado,ObservacionEncargado,FechaAprobacion " +
                                         "from ope.V_ObtenerSolicitudOperaciones where IdOportunidad=@IdOportunidad and (EsCancelado = 1 or Aprobado = 1 or Realizado = 1) order by FechaSolicitud desc";
                string querySolicitud = _dapper.QueryDapper(_querySolicitud, new { IdOportunidad });

                if (!querySolicitud.Contains("[]") && querySolicitud != "")
                {
                    datosSolicitudOperaciones = JsonConvert.DeserializeObject<List<DatosSolicitudOperacionesDTO>>(querySolicitud);
                }

                return datosSolicitudOperaciones;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TodoSolicitudOperacionesDTO> ObtenerTodoSolicitudOperaciones()
        {
            try
            {
                List<TodoSolicitudOperacionesDTO> datosSolicitudOperaciones = new List<TodoSolicitudOperacionesDTO>();
                string _querySolicitud = "Select Id,IdOportunidad,IdTipoSolicitudOperaciones,TipoSolicitudOperaciones,FechaSolicitud,IdPersonalSolicitante, " +
                                         "PersonalSolicitante,EmailSolicitante,IdPersonalAprobacion,PersonalAprobacion,EmailAprobador,ValorAnterior,ValorNuevo,Aprobado,EsCancelado,ComentarioSolicitante " +
                                         ",Observacion,IdUrlBlockStorage,UrlBlockStorage,NombreArchivo,ContentType,Realizado,ObservacionEncargado,FechaAprobacion,NombreCompleto,Correo,Dni,Correo "+
                                         ",CentroCosto,Pespecifico,CodigoMatricula,Direccion " +
                                         "from ope.V_ObtenerTodoSolicitudOperaciones  order by FechaSolicitud desc";
                string querySolicitud = _dapper.QueryDapper(_querySolicitud,null);

                if (!querySolicitud.Contains("[]") && querySolicitud != "")
                {
                    datosSolicitudOperaciones = JsonConvert.DeserializeObject<List<TodoSolicitudOperacionesDTO>>(querySolicitud);
                }

                return datosSolicitudOperaciones;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        /// <summary>
        /// Obtiene el IdOportunidad de Operaciones 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ObtenerIdoportunidad(int idMatriculaCabecera)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"
                              SELECT IdOportunidad as Valor FROM ope.T_oportunidadClasificacionOperaciones where IdMatriculaCabecera = @IdMatriculaCabecera
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return _resultado.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el IdMatriculaCabecera por la oportunidad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ObtenerMatriculaPorOportunidad(int idOportunidad)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"
                              SELECT IdMatriculaCabecera as Valor FROM ope.T_oportunidadClasificacionOperaciones where IdOportunidad = @IdOportunidad
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { IdOportunidad = idOportunidad });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return _resultado.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el IdOportunidad de Operaciones 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<LIstaDiasProgramadosDTO> ObtenerDiasPendientesRecorrer(int idOportunidad)
        {
            try
            {
                var _resultado = new List<LIstaDiasProgramadosDTO>();
                
                string _querySolicitud = "SELECT ObservacionEncargado FROM ope.T_SolicitudOperaciones where IdOportunidad = @IdOportunidad";
                string querySolicitud = _dapper.QueryDapper(_querySolicitud, new { IdOportunidad = idOportunidad });

                if (!querySolicitud.Contains("[]") && querySolicitud != "")
                {
                    _resultado = JsonConvert.DeserializeObject<List<LIstaDiasProgramadosDTO>>(querySolicitud);
                }

                return _resultado;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el IdOportunidad de Operaciones 
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="valorNuevo"></param>
        /// <returns></returns>
        public ResultadoFinalDTO ValidarCambioSubEstado(int idOportunidad,string valorNuevo)
        {
            try
            {
                try
                {
                    var _resultado = new ResultadoFinalDTO();
                    var _query = "ope.SP_ValidarCriteriosSubEstadoMatricula";
                    var pEspecificoDB = _dapper.QuerySPFirstOrDefault(_query, new { IdOportunidad = idOportunidad, ValorNuevo = valorNuevo });
                    if (!pEspecificoDB.Contains("[]") && pEspecificoDB != "")
                    {
                        _resultado = JsonConvert.DeserializeObject<ResultadoFinalDTO>(pEspecificoDB);
                    }
                    return _resultado;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el IdOportunidad de Operaciones 
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public ResultadoFinalDTO ActualizarTerminosPortalWeb(int idOportunidad)
        {
            try
            {
                try
                {
                    var _resultado = new ResultadoFinalDTO();
                    var _query = "ope.SP_ActualizarTerminosPortalWeb";
                    var pEspecificoDB = _dapper.QuerySPFirstOrDefault(_query, new { IdOportunidad = idOportunidad});
                    if (!pEspecificoDB.Contains("[]") && pEspecificoDB != "")
                    {
                        _resultado = JsonConvert.DeserializeObject<ResultadoFinalDTO>(pEspecificoDB);
                    }
                    return _resultado;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el IdPersonalAsignado que esta la Oportunidad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ObtenerIdPersonalAsignado(int idOportunidad)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"
                              SELECT IdPersonal_Asignado as Valor FROM com.T_Oportunidad where Id = @Id
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { Id = idOportunidad });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return _resultado.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene solicitudes de cambio con validacion por bloque
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperacionesEnBloque(int IdOportunidad)
        {
            try
            {
                List<DatosSolicitudOperacionesDTO> datosSolicitudOperaciones = new List<DatosSolicitudOperacionesDTO>();
                string _querySolicitud = "Select Id,IdOportunidad,IdTipoSolicitudOperaciones,TipoSolicitudOperaciones,FechaSolicitud,IdPersonalSolicitante," +
                                         "PersonalSolicitante,IdPersonalAprobacion,PersonalAprobacion,ValorAnterior,ValorNuevo,Aprobado,EsCancelado,ComentarioSolicitante" +
                                         ",Observacion,IdUrlBlockStorage,UrlBlockStorage,NombreArchivo,ContentType,Realizado,ObservacionEncargado,FechaAprobacion,RelacionEstadoSubEstado " +
                                         "from ope.V_ObtenerSolicitudOperacionesConRelacionEstado where IdOportunidad=@IdOportunidad and  Aprobado = 0 and EsCancelado = 0 and Realizado = 0 order by FechaSolicitud desc";
                string querySolicitud = _dapper.QueryDapper(_querySolicitud, new { IdOportunidad });

                if (!querySolicitud.Contains("[]") && querySolicitud != "")
                {
                    datosSolicitudOperaciones = JsonConvert.DeserializeObject<List<DatosSolicitudOperacionesDTO>>(querySolicitud);
                }

                return datosSolicitudOperaciones;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        ///Repositorio: SolicitudOperacionesRepositorio
        ///Autor: Jose Villena
        ///Fecha: 08/11/2021
        /// <summary>
        /// Obtener solicitudes operaciones Envio Automatico Operaciones
        /// </summary>        
        /// <returns> Lista Datos Estado - SubEstado Envio Automatico operaciones: List<DatosAlumnosEnvioAutomaticoOperacionesDTO></returns>   
        public List<DatosAlumnosEnvioAutomaticoOperacionesDTO> ObtenerDatosEnvioAutomaticoOperaciones()
        {
            try
            {
                List<DatosAlumnosEnvioAutomaticoOperacionesDTO> obtenerListaEstadoSubEstado = new List<DatosAlumnosEnvioAutomaticoOperacionesDTO>();
                var obtenerListaEstadoSubEstadoDB = _dapper.QuerySPDapper("ope.SP_ObtenerDatosEnvioAutomaticoOperaciones", null);
                if (!string.IsNullOrEmpty(obtenerListaEstadoSubEstadoDB) && !obtenerListaEstadoSubEstadoDB.Contains("[]"))
                {
                    obtenerListaEstadoSubEstado = JsonConvert.DeserializeObject<List<DatosAlumnosEnvioAutomaticoOperacionesDTO>>(obtenerListaEstadoSubEstadoDB);
                }

                return obtenerListaEstadoSubEstado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
