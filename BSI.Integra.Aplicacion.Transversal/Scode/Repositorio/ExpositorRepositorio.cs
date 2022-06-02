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
using BSI.Integra.Persistencia.SCode.Repository;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Planificacion/Expositor
    /// Autor: Ansoli Espinoza - Jose Villena.
    /// Fecha: 28/04/2021
    /// <summary>
    /// Repositorio para consultas de pla.T_Expositor
    /// </summary>
    public class ExpositorRepositorio : BaseRepository<TExpositor, ExpositorBO>
    {
        #region Metodos Base
        public ExpositorRepositorio() : base()
        {
        }
        public ExpositorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ExpositorBO> GetBy(Expression<Func<TExpositor, bool>> filter)
        {
            IEnumerable<TExpositor> listado = base.GetBy(filter);
            List<ExpositorBO> listadoBO = new List<ExpositorBO>();
            foreach (var itemEntidad in listado)
            {
                ExpositorBO objetoBO = Mapper.Map<TExpositor, ExpositorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ExpositorBO FirstById(int id)
        {
            try
            {
                TExpositor entidad = base.FirstById(id);
                ExpositorBO objetoBO = new ExpositorBO();
                Mapper.Map<TExpositor, ExpositorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ExpositorBO FirstBy(Expression<Func<TExpositor, bool>> filter)
        {
            try
            {
                TExpositor entidad = base.FirstBy(filter);
                ExpositorBO objetoBO = Mapper.Map<TExpositor, ExpositorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ExpositorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TExpositor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ExpositorBO> listadoBO)
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

        public bool Update(ExpositorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TExpositor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ExpositorBO> listadoBO)
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
        private void AsignacionId(TExpositor entidad, ExpositorBO objetoBO)
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

        private TExpositor MapeoEntidad(ExpositorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TExpositor entidad = new TExpositor();
                entidad = Mapper.Map<ExpositorBO, TExpositor>(objetoBO,
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

        /// Repositorio: ExpositorRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene los expositores por programa general
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<AgendaExpositorDTO> </returns>
        public List<AgendaExpositorDTO> ObtenerExpositoresPorProgramaGeneral(int idPGeneral)
        {
            List<AgendaExpositorDTO> agendaExpositores = new List<AgendaExpositorDTO>();
            var query = "SELECT Nombres, HojaVida " +
                         "FROM pla.V_Expositores " +
                         "WHERE " +
                             "EstadoExpositor = 1 " +
                             "AND IdProgramaGeneral = @idPGeneral " +
                         "GROUP BY Nombres, HojaVida";
            var expositoresDB = _dapper.QueryDapper(query, new { idPGeneral });
            agendaExpositores = JsonConvert.DeserializeObject<List<AgendaExpositorDTO>>(expositoresDB);
            return agendaExpositores;
        }

        /// Autor: 
        /// Fecha: 24/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de expositores con sus nombre completos
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public List<ExpositorFiltroDTO> ObtenerExpositoresFiltro()
        {
            try
            {
                List<ExpositorFiltroDTO> expositor = new List<ExpositorFiltroDTO>();
                string queryExpositor = string.Empty;
                queryExpositor = "Select Id,Nombre From pla.V_DatosExpositor Where Estado=1 Order by Nombre";
                var resultadoQueryExpositor = _dapper.QueryDapper(queryExpositor, new { });
                if (!string.IsNullOrEmpty(resultadoQueryExpositor) && !resultadoQueryExpositor.Contains("[]"))
                {
                    expositor = JsonConvert.DeserializeObject<List<ExpositorFiltroDTO>>(resultadoQueryExpositor);
                }
                return expositor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<FiltroGenericoDTO> ObtenerFiltroExpositor()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new FiltroGenericoDTO { Value = x.Id, Text = string.Concat(x.PrimerNombre, " ", x.SegundoNombre, " ", x.ApellidoPaterno, " ", x.ApellidoMaterno) }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ExpositorDocumentoDTO> ObtenerListado()
        {
            try
            {
                List<ExpositorDocumentoDTO> expositor = new List<ExpositorDocumentoDTO>();
                string _queryExpositor = string.Empty;
                _queryExpositor = "SELECT Id, Nombres, Apellidos, PrimerNombre, SegundoNombre, ApellidoPaterno, ApellidoMaterno, TelfCelular1, TelfCelular2, TelfCelular3, NombrePais, NombreCiudad, TipoDocumento, NroDocumento, Email1, Domicilio, HojaVidaResumidaPerfil, Email2, Email3 FROM pla.T_ObtenerExpositor WHERE Estado = 1";
                var queryExpositor = _dapper.QueryDapper(_queryExpositor, new { });
                if (!string.IsNullOrEmpty(queryExpositor) && !queryExpositor.Contains("[]"))
                {
                    expositor = JsonConvert.DeserializeObject<List<ExpositorDocumentoDTO>>(queryExpositor);
                }
                return expositor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<ExpositorDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new ExpositorDTO
                {
                    Id = y.Id,
                    IdTipoDocumento = y.IdTipoDocumento,
                    NroDocumento = y.NroDocumento,
                    PrimerNombre = y.PrimerNombre,
                    SegundoNombre = y.SegundoNombre,
                    ApellidoMaterno = y.ApellidoMaterno,
                    ApellidoPaterno = y.ApellidoPaterno,
                    FechaNacimiento = y.FechaNacimiento,
                    IdPaisProcedencia = y.IdPaisProcedencia,
                    IdCiudadProcedencia = y.IdCiudadProcedencia,
                    IdReferidoPor = y.IdReferidoPor,
                    TelfCelular1 = y.TelfCelular1,
                    TelfCelular2 = y.TelfCelular2,
                    TelfCelular3 = y.TelfCelular3,
                    Email1 = y.Email1,
                    Email2 = y.Email2,
                    Email3 = y.Email3,
                    Domicilio = y.Domicilio,
                    IdPaisDomicilio = y.IdPaisDomicilio,
                    IdCiudadDomicilio = y.IdCiudadDomicilio,
                    LugarTrabajo = y.LugarTrabajo,
                    IdPaisLugarTrabajo = y.IdPaisLugarTrabajo,
                    IdCiudadLugarTrabajo = y.IdCiudadLugarTrabajo,
                    AsistenteNombre = y.AsistenteNombre,
                    AsistenteTelefono = y.AsistenteTelefono,
                    AsistenteCelular = y.AsistenteCelular,
                    HojaVidaResumidaPerfil = y.HojaVidaResumidaPerfil,
                    HojaVidaResumidaSpeech = y.HojaVidaResumidaSpeech,
                    FormacionAcademica = y.FormacionAcademica,
                    ExperienciaProfesional = y.ExperienciaProfesional,
                    Publicaciones = y.Publicaciones,
                    PremiosDistinciones = y.PremiosDistinciones,
                    OtraInformacion = y.OtraInformacion,
                    IdPersonalAsignado = y.IdPersonalAsignado,
                    FotoDocente = y.FotoDocente,
                    UrlFotoDocente = y.UrlFotoDocente
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// ELimina de forma fisica de la base de datos de Expositor
        /// </summary>
        /// <returns></returns>
        public bool EliminarFisicaExpositor(string tablaV3, string tablaV4, int idV4, Guid? idv3, int? id_v3)
        {
            try
            {
                bool expositor = new bool();
                
                string queryExpositor = _dapper.QuerySPDapper("conf.SP_EliminarRegistroTablaMaestro", new { NombreTablaV3 = tablaV3, NombreTablaV4 = tablaV4, IdV4 = idV4, IdV3 = idv3, IdV3Int = id_v3 });
                if (!string.IsNullOrEmpty(queryExpositor) && !queryExpositor.Contains("[]"))
                {
                    expositor = JsonConvert.DeserializeObject<bool>(queryExpositor);
                }
                return expositor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        ///Repositorio: ExpositorRepositorio
        ///Autor: Jose Villena.
        ///Fecha: 28/04/2021
        /// <summary>
        /// Obtiene el email1 del Expositor
        /// </summary>
        /// <param name="id"> Id Expositor </param>
        /// <returns>Email 1 del Expositor</returns>
        public string ObtenerEmail(int id)
        {
            try
            {
                return this.GetBy(x => x.Id == id).FirstOrDefault().Email1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene id migracion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Guid? ObtenerIdMigracion(int id)
        {
            try
            {
                return this.GetBy(x => x.Id == id).Select(x => x.IdMigracion).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Verifica que el correo no sea repetido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int? ObtenerExpositorEliminadoEmailRepetido(string email)
        {
            try
            {
                Dictionary<string, int> expositor = new Dictionary<string, int>();
                var _query = "SELECT Id FROM pla.T_Expositor where estado = 0 and Email1=@Email";
                var expositorDB = _dapper.FirstOrDefault(_query, new { Email = email });
                if (!string.IsNullOrEmpty(expositorDB) && !expositorDB.Contains("[]") && !expositorDB.Contains("null"))
                {
                    expositor = JsonConvert.DeserializeObject<Dictionary<string, int>>(expositorDB);
                }
                return expositor.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public ExpositorAgendaDTO ObtenerExpositorPorClasificacionPersona(int idClasificacionPersona)
        {
            try
            {
                ExpositorAgendaDTO expositor = new ExpositorAgendaDTO();
                var _query = "SELECT Id, PrimerNombre, SegundoNombre, ApellidoPaterno, ApellidoMaterno, TelfCelular1, TelfCelular2, Email1, Email2, NroDocumento, Domicilio, IdPaisDomicilio, IdCiudadDomicilio, NombrePaisDomicilio, NombreCiudadDomicilio FROM pla.V_TExpositor_ObtenerDatosParaAgenda WHERE IdClasificacionPersona=@idClasificacionPersona";
                var expositorDB = _dapper.FirstOrDefault(_query, new { idClasificacionPersona });
                if (!string.IsNullOrEmpty(expositorDB))
                {
                    expositor = JsonConvert.DeserializeObject<ExpositorAgendaDTO>(expositorDB);
                }
                return expositor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ProveedorAgendaDTO ObtenerProveedorPorClasificacionPersona(int idClasificacionPersona)
        {
            try
            {
                ProveedorAgendaDTO expositor = new ProveedorAgendaDTO();
                var _query = "SELECT Id, RazonSocial, PrimerNombre, SegundoNombre, ApellidoPaterno, ApellidoMaterno, TelfCelular1, TelfCelular2, Email1, Email2, NroDocumento, Domicilio, IdPaisDomicilio, IdCiudadDomicilio, NombrePaisDomicilio, NombreCiudadDomicilio, Alias FROM fin.V_TProveedor_ObtenerDatosParaAgenda WHERE IdClasificacionPersona=@idClasificacionPersona";
                var expositorDB = _dapper.FirstOrDefault(_query, new { idClasificacionPersona });
                if (!string.IsNullOrEmpty(expositorDB))
                {
                    expositor = JsonConvert.DeserializeObject<ProveedorAgendaDTO>(expositorDB);
                }
                return expositor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene para filtro
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                var lista = new List<FiltroDTO>();
                var query = $@"
                            SELECT Id, 
                                   Nombre
                            FROM pla.V_DatosExpositor
                            WHERE Estado = 1
                            ORDER BY Nombre;
                            ";
                var resultadoDB = _dapper.QueryDapper(query, new { });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<FiltroDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Valida si existe un expositor con el email
        /// </summary>
        /// <param name="Email1"></param>
        /// <returns></returns>
        public bool ExisteContacto(string Email1) {
            try
            {
                return this.Exist(x => x.Email1 == Email1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los id de los expositores
        /// </summary>
        /// <returns></returns>
        public List<int> ObtenerExpositoresParaOportunidades()
        {
            try
            {
                List<IdDTO> agendaExpositores = new List<IdDTO>();
                var _query = "SELECT Id FROM pla.V_TExpositor_ObtenerExpositorSinOportunidad WHERE Estado = 1 AND IdPersonal_Asignado is not null and IdClasificacionPersona IS NULL";
                var expositoresDB = _dapper.QueryDapper(_query, new { });
                
                    agendaExpositores = JsonConvert.DeserializeObject<List<IdDTO>>(expositoresDB);
               
                return agendaExpositores.Select(x => x.Id).ToList();
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

                    string _direccionBlob = @"repositorioweb/img/docentes/";

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
