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
    /// Repositorio: Finanzas/MatriculaCabecera
    /// Autor: Fischer Valdez - Carlos Crispin - Ansoli Deyvis - Wilber Choque - Gian Miranda - Edgar S.
    /// Fecha: 05/02/2021
    /// <summary>
    /// Repositorio para consultas de fin.T_MatriculaCabecera
    /// </summary>
    public class AlumnoRepositorio : BaseRepository<TAlumno, AlumnoBO>
    {
        #region Metodos Base
        public AlumnoRepositorio() : base()
        {
        }
        public AlumnoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AlumnoBO> GetBy(Expression<Func<TAlumno, bool>> filter)
        {
            IEnumerable<TAlumno> listado = base.GetBy(filter);
            List<AlumnoBO> listadoBO = new List<AlumnoBO>();
            foreach (var itemEntidad in listado)
            {
                AlumnoBO objetoBO = Mapper.Map<TAlumno, AlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IEnumerable<AlumnoBO> GetBy(Expression<Func<TAlumno, bool>> filter, int skip, int take)
        {
            IEnumerable<TAlumno> listado = base.GetBy(filter).Skip(skip).Take(take);
            List<AlumnoBO> listadoBO = new List<AlumnoBO>();
            foreach (var itemEntidad in listado)
            {
                AlumnoBO objetoBO = Mapper.Map<TAlumno, AlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public AlumnoBO FirstById(int id)
        {
            try
            {
                TAlumno entidad = base.FirstById(id);
                AlumnoBO objetoBO = new AlumnoBO();
                Mapper.Map<TAlumno, AlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AlumnoBO FirstBy(Expression<Func<TAlumno, bool>> filter)
        {
            try
            {
                TAlumno entidad = base.FirstBy(filter);
                AlumnoBO objetoBO = Mapper.Map<TAlumno, AlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AlumnoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AlumnoBO> listadoBO)
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

        public bool Update(AlumnoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AlumnoBO> listadoBO)
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
        private void AsignacionId(TAlumno entidad, AlumnoBO objetoBO)
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

        private TAlumno MapeoEntidad(AlumnoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAlumno entidad = new TAlumno();
                entidad = Mapper.Map<AlumnoBO, TAlumno>(objetoBO,
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
        /// <summary>
        /// Obtener el celular y codigo pais por Id Alumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns></returns>
        public CelularCodigoPaisAlumnoDTO ObtenerCelularCodigoPais(int idAlumno)
        {
            try
            {
                string _query = "SELECT Id, IdCodigoPais, Celular FROM mkt.V_TAlumno_DatosMensajeTexto WHERE Estado=1 AND Id=@IdAlumno";
                var alumnoDB = _dapper.FirstOrDefault(_query, new { IdAlumno = idAlumno });
                if (!alumnoDB.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<CelularCodigoPaisAlumnoDTO>(alumnoDB);
                }
                else
                {
                    throw new Exception("No Existe Alumno con Identificador " + idAlumno);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// Autor: ----------, Jashin Salazar
        /// Fecha: 28/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene documentos para alumno por ID   
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ObjetoDTO: AlumnoCompuestoDocumentoDTO</returns>
        public AlumnoCompuestoDocumentoDTO ObtenerDatosAlumnoDocumentoPorId(int id)
        {
            try
            {
                string _queryAlumno = "Select Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Direccion,DNI,Celular,Telefono,IdCiudad,NombreCiudad,IdCodigoPais,NombrePais,Correo" +
                                      " From mkt.V_TAlumno_DatosAlumnoParaDocumento Where Id=@Id and Estado=1";
                var _alumno = _dapper.FirstOrDefault(_queryAlumno, new { Id = id });
                return JsonConvert.DeserializeObject<AlumnoCompuestoDocumentoDTO>(_alumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: ----------, Jashin Salazar
        /// Fecha: 22/10/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene cupon por IdAlumno  
        /// </summary>
        /// <param name="IdAlumno">Id de alumno</param>
        /// <returns>ObjetoDTO: AlumnoCompuestoDocumentoDTO</returns>
        public AlumnoCuponDTO ObtenerCuponPorIdAlumno(int IdAlumno)
        {
            try
            {
                string queryAlumno = "SELECT Id,IdAlumno,CodigoCupon FROM mkt.T_AlumnoCuponRegistro WHERE IdAlumno=@IdAlumno";
                var cupon = _dapper.FirstOrDefault(queryAlumno, new { IdAlumno = IdAlumno });
                return JsonConvert.DeserializeObject<AlumnoCuponDTO>(cupon);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: ----------
        /// Fecha: 04/03/2021
        /// Version: 1.0
        /// <summary>
        /// Validar Email 1 - Alumno    
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public AlumnoEmailDTO ValidarEmail1Alumno(string email)
        {
            try
            {
                string queryAlumnovalidar = "Select Id,Email1,Email2 From mkt.V_TAlumno_ValidarEmail where Email1=@Email1 and Estado=1";
                var rptaQueryAlumnovalidar = _dapper.FirstOrDefault(queryAlumnovalidar, new { Email1 = email });
                return JsonConvert.DeserializeObject<AlumnoEmailDTO>(rptaQueryAlumnovalidar);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// lisbeth
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AlumnoComprobanteDTO ObtenerDatosAlumnoPorId(int id)
        {
            try
            {
                string _queryAlumno = "Select Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Direccion,DNI,Celular,Telefono,IdCiudad,NombreCiudad,IdCodigoPais,NombrePais,Email1" +
                                      " From mkt.T_Alumno Where Id=@Id and Estado=1";
                var _alumno = _dapper.FirstOrDefault(_queryAlumno, new { Id = id });
                return JsonConvert.DeserializeObject<AlumnoComprobanteDTO>(_alumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Retorna un alumno con todos sus datos, podra ser buscado por Email1 o Email2
        /// </summary>
        /// <param name="email1">Email 1 del alumno</param>
        /// <param name="email2">Email 2 del alumno</param>
        /// <returns>Objeto de clase AlumnoBO</returns>
        public AlumnoBO ObtenerPormail(string email1, string email2)
        {
            AlumnoBO alumno = new AlumnoBO();
            var query = string.Empty;
            string email = string.Empty;

            if (email1 != null && email2 == null)
            {
                query = "SELECT * FROM mkt.T_Alumno WHERE Estado = 1 AND Email1 = @Email";
                email = email1;
            }
            else if (email2 != null && email1 == null)
            {
                query = "SELECT * FROM mkt.T_Alumno WHERE Estado = 1 AND Email2 = @Email";
                email = email2;
            }

            var alumnoDB = _dapper.FirstOrDefault(query, new { Email = email });
            if (!alumnoDB.Contains("[]"))
            {
                alumno = JsonConvert.DeserializeObject<AlumnoBO>(alumnoDB);
            }
            return alumno;
        }
        /// <summary>
        /// Retorna un alumno con todos sus datos, podra ser buscado por Celular 1 o Celular 2
        /// </summary>
        /// <param name="celular1">Celular 1 del alumno</param>
        /// <param name="celular2">Celular 2 del alumno</param>
        /// <returns>Objeto de clase AlumnoBO</returns>
        public AlumnoBO ObtenerPorCelular(string celular1, string celular2)
        {
            AlumnoBO alumno = new AlumnoBO();
            var query = string.Empty;
            string celular = string.Empty;

            if (celular1 != null && celular2 == null)
            {
                query = "SELECT * FROM mkt.T_Alumno WHERE Estado = 1 AND Celular = @Celular";
                celular = celular1;
            }
            else if (celular2 != null && celular1 == null)
            {
                query = "SELECT * FROM mkt.T_Alumno WHERE Estado = 1 AND Celular2 = @Celular";
                celular = celular2;
            }
            var alumnoDB = _dapper.FirstOrDefault(query, new { Celular = celular });
            if (!alumnoDB.Contains("[]"))
            {
                alumno = JsonConvert.DeserializeObject<AlumnoBO>(alumnoDB);
            }
            return alumno;
        }

        /// <summary>
        /// Retorna un los datos si el alumno se le hizo un envio masivo de SMS
        /// </summary>
        /// <param name="idAlumno">Id del alumno</param>
        /// <returns>Objeto de clase AlumnoBO</returns>
        public AlumnoEnvioMasivoSMSDTO ObtenerEnvioMasivoSMS(int idAlumno)
        {
            AlumnoEnvioMasivoSMSDTO alumno = new AlumnoEnvioMasivoSMSDTO();
            var query = string.Empty;
            string celular = string.Empty;

            query = "SELECT IdAlumno,AreaVentas from mkt.T_AlumnoCuponRegistro where IdPersonal = 4363 and areaVentas = '(A) Gestión Ambiental' and estado = 1 AND IdAlumno = @IdAlumno";

            var alumnoDB = _dapper.FirstOrDefault(query, new { IdAlumno = idAlumno });
            if (!alumnoDB.Contains("[]"))
            {
                alumno = JsonConvert.DeserializeObject<AlumnoEnvioMasivoSMSDTO>(alumnoDB);
            }
            return alumno;
        }


        /// Autor: ----------
        /// Fecha: 04/03/2021
        /// Version: 1.0
        /// <summary>
        /// Validar Email 2 - Alumno    
        /// </summary>
        /// <param name="email">Email que se validara en el proceso</param>
        /// <returns>Objeto de clase AlumnoEmailDTO</returns>
        public AlumnoEmailDTO ValidarEmail2Alumno(string email)
        {
            try
            {
                string queryAlumnovalidar = "Select Id, Email1, Email2 FROM mkt.V_TAlumno_ValidarEmail where Email2 = @Email2 and Estado=1";
                var rptaQueryAlumnovalidar = _dapper.FirstOrDefault(queryAlumnovalidar, new { Email2 = email });

                return JsonConvert.DeserializeObject<AlumnoEmailDTO>(rptaQueryAlumnovalidar);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la Probabilidad del Sueldo del Alumno
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idPais"></param>
        /// <returns></returns>
        public SueldoAlumnoDTO ObtenerProbabilidadSueldo(int idOportunidad, int idPais)
        {
            try
            {
                string _querySueldo = "com.SP_TAlumnoGetMontoPago";
                var querySueldo = _dapper.QuerySPFirstOrDefault(_querySueldo, new { IdOportunidad = idOportunidad, IdPais = idPais });
                return JsonConvert.DeserializeObject<SueldoAlumnoDTO>(querySueldo);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public AlumnoInformacionDTO ObtenerDatosAlumno(int idClasificacionPersona)
        {
            try
            {
                string _queryDatosAlumno = "Select Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,DNI,Direccion,FechaNacimiento,Telefono,Celular,Email1" +
                                            ",Email2,Genero,Parentesco,NombreFamiliar,TelefonoFamiliar,Empresa,IdCargo,Cargo,IdAFormacion,AFormacion,IdATrabajo,ATrabajo,IdIndustria,Industria,IdReferido," +
                                            "Referido,IdCodigoPais,NombrePais,IdCiudad,NombreCiudad,HoraContacto,HoraPeru,Telefono2,Celular2,IdEmpresa,IdEstadoContactoWhatsApp,IdEstadoContactoWhatsApp_Secundario AS IdEstadoContactoWhatsAppSecundario," +
                                            "IdOportunidad_Inicial,IdTipoDocumento,NroDocumento,DescripcionCargo,Asociado, RutaBandera From com.V_InformacionAlumno where IdClasificacionPersona=@IdClasificacionPersona and Estado=1";
                var queryDatosAlumno = _dapper.FirstOrDefault(_queryDatosAlumno, new { IdClasificacionPersona = idClasificacionPersona });
                return JsonConvert.DeserializeObject<AlumnoInformacionDTO>(queryDatosAlumno);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        ///Repositorio: AlumnoRepositorio
        ///Autor: Edgar S.
        ///Fecha: 08/02/2021
        /// <summary>
        /// Obtener información de Id, Alumnos por nombre AutoComplete
        /// </summary>
        /// <param name="valor"> valor de búsqueda </param>
        /// <returns> Lista de Alumnos por nombre Registrados </returns>
        /// <returns> Objeto DTO: List<AlumnoFiltroAutocompleteDTO> </returns>	
        public List<AlumnoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoComplete(string valor)
        {
            try
            {
                List<AlumnoFiltroAutocompleteDTO> alumnosAutocompleteFiltro = new List<AlumnoFiltroAutocompleteDTO>();
                string queryAlumnoFiltro = string.Empty;
                queryAlumnoFiltro = "SELECT Id,NombreCompleto FROM mkt.V_TAlumno_NombreCompleto WHERE NombreCompleto LIKE CONCAT('%',@valor,'%') AND Estado = 1 ORDER By NombreCompleto ASC";
                var alumnoDB = _dapper.QueryDapper(queryAlumnoFiltro, new { valor });
                if (!string.IsNullOrEmpty(alumnoDB) && !alumnoDB.Contains("[]"))
                {
                    alumnosAutocompleteFiltro = JsonConvert.DeserializeObject<List<AlumnoFiltroAutocompleteDTO>>(alumnoDB);
                }
                return alumnosAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id,NombreCompleto de los alumnos por  un parametro
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<AlumnoFiltroAutocompleteDTO> ObtenerTodoDocenteFiltroAutoComplete(string valor)
        {
            try
            {
                List<AlumnoFiltroAutocompleteDTO> alumnosAutocompleteFiltro = new List<AlumnoFiltroAutocompleteDTO>();
                string _queryAlumnoFiltro = string.Empty;
                _queryAlumnoFiltro = "SELECT Id,NombreCompleto FROM pla.V_TExpositor_NombreCompleto WHERE NombreCompleto LIKE CONCAT('%',@valor,'%') AND Estado = 1 ORDER By NombreCompleto ASC";
                var alumnoDB = _dapper.QueryDapper(_queryAlumnoFiltro, new { valor });
                if (!string.IsNullOrEmpty(alumnoDB) && !alumnoDB.Contains("[]"))
                {
                    alumnosAutocompleteFiltro = JsonConvert.DeserializeObject<List<AlumnoFiltroAutocompleteDTO>>(alumnoDB);
                }
                return alumnosAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id,NombreCompleto de los alumnos por  un parametro
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<AlumnoFiltroAutocompleteDTO> ObtenerTodoProveedorFiltroAutoComplete(string valor)
        {
            try
            {
                List<AlumnoFiltroAutocompleteDTO> alumnosAutocompleteFiltro = new List<AlumnoFiltroAutocompleteDTO>();
                string _queryAlumnoFiltro = string.Empty;
                _queryAlumnoFiltro = "SELECT Id, Nombre NombreCompleto FROM fin.V_Obtener_ProveedorParaHonorario WHERE Nombre LIKE CONCAT('%',@valor,'%')  ORDER By Nombre ASC";
                var alumnoDB = _dapper.QueryDapper(_queryAlumnoFiltro, new { valor });
                if (!string.IsNullOrEmpty(alumnoDB) && !alumnoDB.Contains("[]"))
                {
                    alumnosAutocompleteFiltro = JsonConvert.DeserializeObject<List<AlumnoFiltroAutocompleteDTO>>(alumnoDB);
                }
                return alumnosAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del alumno mediante su email
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<AlumnoFiltroAutocompleteDTO> alumnnosTodoFiltroAutoCompletePorEmail(string valor)
        {
            try
            {
                List<AlumnoFiltroAutocompleteDTO> alumnosEmail = new List<AlumnoFiltroAutocompleteDTO>();
                string _queryAlumno = string.Empty;
                _queryAlumno = "SELECT Id, NombreCompleto FROM mkt.V_TAlumno_NombreCompletoEmail WHERE " +
                    "(ltrim(rtrim(Email1)) like '%'+ltrim(rtrim(@val))+'%' or ltrim(rtrim(Email2)) like '%'+ltrim(rtrim(@val))+'%') and Estado='1'";
                var Alumno = _dapper.QueryDapper(_queryAlumno, new { val = valor });
                if (!string.IsNullOrEmpty(Alumno) && !Alumno.Contains("[]"))
                {
                    alumnosEmail = JsonConvert.DeserializeObject<List<AlumnoFiltroAutocompleteDTO>>(Alumno);
                }
                return alumnosEmail;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id y NombreCompleto del Alumno mediante el IdReferido
        /// </summary>
        /// <param name="IdR"></param>
        /// <returns></returns>
        public List<AlumnoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoCompleteReferido(int IdR)
        {
            try
            {
                List<AlumnoFiltroAutocompleteDTO> alumnosAutocompleteFiltro = new List<AlumnoFiltroAutocompleteDTO>();
                string _queryAlumnoFiltro = string.Empty;
                _queryAlumnoFiltro = "SELECT Id,NombreCompleto FROM mkt.V_TAlumno_NombreCompleto WHERE Id = @IdR AND Estado = 1 ORDER By NombreCompleto ASC";
                var AlumnoDB = _dapper.QueryDapper(_queryAlumnoFiltro, new { IdR = IdR });
                if (!string.IsNullOrEmpty(AlumnoDB) && !AlumnoDB.Contains("[]"))
                {
                    alumnosAutocompleteFiltro = JsonConvert.DeserializeObject<List<AlumnoFiltroAutocompleteDTO>>(AlumnoDB);
                }
                return alumnosAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// <summary>
        /// Obtiene la lista de usuarios con un mismo Email1 o Email2
        /// </summary>
        /// <param name="Email1"></param>
        /// <param name="Email2"></param>
        /// <returns></returns>
        public List<AlumnoEmailDTO> ObtenerAlumnoPorEmail(string Email1, string Email2)
        {
            try
            {
                string _queryAlumno = "com.SP_existeContacto";
                var queryAlumno = _dapper.QuerySPDapper(_queryAlumno, new { Email1 = Email1, Email2 = Email2 });
                List<AlumnoEmailDTO> listaAlumno = JsonConvert.DeserializeObject<List<AlumnoEmailDTO>>(queryAlumno);
                return listaAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el correo del Alumno a  traves del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AlumnoEmailDTO ObtenerEmailAlumno(int id)
        {
            try
            {
                string _queryalumnoBD = "Select Id, Email1 From mkt.V_TAlumno_Email Where Id=@IdAlumno";
                var queryAlumno = _dapper.FirstOrDefault(_queryalumnoBD, new { IdAlumno = id });
                AlumnoEmailDTO alumno = JsonConvert.DeserializeObject<AlumnoEmailDTO>(queryAlumno);
                return alumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public AlumnoInformacionReducidaDTO Obtener_InformacionReducida(int idAlumno)
        {
            try
            {
                string _queryAlumno = @"
                    SELECT 
                        Id, Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno, DNI, Direccion, Celular, Email1, Email2, IdCodigoPais
                    FROM mkt.T_Alumno
                    WHERE Id = @Id;
                    ";
                var _alumno = _dapper.FirstOrDefault(_queryAlumno, new { Id = idAlumno });
                return JsonConvert.DeserializeObject<AlumnoInformacionReducidaDTO>(_alumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene si ya se ha enviado en el mismo dia un mensaje
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad  (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="fecha">Fecha de envio</param>
        /// <returns>Objeto de clase EnvioSMSOportunidad</returns>
        public EnvioSMSOportunidad Obtener_EnvioSMSPorDiaOportunidad(int idOportunidad, DateTime fecha)
        {
            try
            {
                string queryAlumno = @"
                    SELECT Id, IdOportunidad
                    FROM mkt.T_EnvioSMSOportunidad WITH (NOLOCK)
                    WHERE IdOportunidad = @IdOportunidad AND CONVERT(DATE, Fecha) = CONVERT(DATE, @FechaEnvio);";

                var envio = _dapper.FirstOrDefault(queryAlumno, new { IdOportunidad = idOportunidad, FechaEnvio = fecha });

                return JsonConvert.DeserializeObject<EnvioSMSOportunidad>(envio);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta el envio de SMS por oportunidad y dia
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public RespuestaSMSOportunidadDTO InsertaSMSOportunidad(int idOportunidad, DateTime fecha)
        {
            try
            {
                string _querySMS = "mkt.SP_insertarSMSOportunidad";
                var querySMS = _dapper.QuerySPFirstOrDefault(_querySMS, new { IdOportunidad = idOportunidad, FechaEnvio = fecha });
                RespuestaSMSOportunidadDTO estadoInsertado = JsonConvert.DeserializeObject<RespuestaSMSOportunidadDTO>(querySMS);
                return estadoInsertado;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta el envio de SMS por oportunidad y dia en la tabla mkt.SP_InsertarMensajeEnviado
        /// </summary>
        /// <param name="celular">Celular al que se envia el mensaje</param>
        /// <param name="idPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="mensaje">Mensaje a enviar</param>
        /// <param name="parteMensaje">Parte del mensaje seccionado</param>
        /// <param name="idPais">Id del pais (PK de la tabla gp.T_Pais)</param>
        /// <returns>Booleano</returns>
        public bool InsertaSMSOportunidadUsuario(string celular, int idPersonal, int idAlumno, string mensaje, int parteMensaje, int idPais, string usuario)
        {
            try
            {
                string spQuery = "mkt.SP_InsertarMensajeEnviado";
                var querySMS = _dapper.QuerySPFirstOrDefault(spQuery, new { Celular = celular, IdPersonal = idPersonal, IdAlumno = idAlumno, Mensaje = mensaje, ParteMensaje = parteMensaje, IdPais = idPais, Usuario = usuario });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta el envio de SMS por oportunidad y dia que fue exitoso
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public RespuestaSMSOportunidadDTO InsertaSMSMasivoExitoso(int idAlumno, int idCodigoPais, string rpta)
        {
            try
            {
                string _querySMS = "mkt.SP_insertarSMSMasivo";
                var querySMS = _dapper.QuerySPFirstOrDefault(_querySMS, new { IdAlumno = idAlumno, IdCodigoPais = idCodigoPais, Exitoso = 1, Error = rpta });
                RespuestaSMSOportunidadDTO estadoInsertado = JsonConvert.DeserializeObject<RespuestaSMSOportunidadDTO>(querySMS);
                return estadoInsertado;

            }
            catch (Exception e)
            {
                RespuestaSMSOportunidadDTO estadoInsertadoerror = new RespuestaSMSOportunidadDTO();
                return estadoInsertadoerror;
            }
        }
        /// <summary>
        /// Inserta el envio de SMS por oportunidad y dia que fallo
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public RespuestaSMSOportunidadDTO InsertaSMSMasivoErroneo(int idAlumno, int idCodigoPais, string error)
        {
            try
            {
                string _querySMS = "mkt.SP_insertarSMSMasivo";
                var querySMS = _dapper.QuerySPFirstOrDefault(_querySMS, new { IdAlumno = idAlumno, IdCodigoPais = idCodigoPais, Exitoso = 0, Error = error });
                RespuestaSMSOportunidadDTO estadoInsertado = JsonConvert.DeserializeObject<RespuestaSMSOportunidadDTO>(querySMS);
                return estadoInsertado;

            }
            catch (Exception e)
            {
                RespuestaSMSOportunidadDTO estadoInsertadoerror = new RespuestaSMSOportunidadDTO();
                return estadoInsertadoerror;
            }
        }
        /// <summary>
        /// Obtiene el IdAlumno , y Codigo de descuento
        /// </summary>
        /// <returns></returns>
        public List<AlumnosEnvioMasivoSMSDTO> ObtenerListaEnvioSMS()
        {
            try
            {
                List<AlumnosEnvioMasivoSMSDTO> alumnosAutocompleteFiltro = new List<AlumnosEnvioMasivoSMSDTO>();
                string _queryAlumnoFiltro = string.Empty;
                _queryAlumnoFiltro = "SELECT IdAlumno,CodigoCupon FROM mkt.T_AlumnoCuponRegistro WHERE  Estado = 1 AND IdPersonal=4363 and areaVentas='(A) Gestión Ambiental' ";
                var AlumnoDB = _dapper.QueryDapper(_queryAlumnoFiltro, null);
                if (!string.IsNullOrEmpty(AlumnoDB) && !AlumnoDB.Contains("[]"))
                {
                    alumnosAutocompleteFiltro = JsonConvert.DeserializeObject<List<AlumnosEnvioMasivoSMSDTO>>(AlumnoDB);
                }
                return alumnosAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del alumno para messenger chat mediante IdAlumno
        /// </summary>
        /// <param name="IdAlumno"></param>
        /// <returns></returns>
        public AlumnoInformacionMessengerDTO ObtenerAlumnoInformacionMessengerChatPorId(int IdAlumno)
        {
            try
            {
                string _queryAlumno = string.Empty;
                _queryAlumno = "Select Id, Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno," +
                    "Direccion, Telefono, Celular, Email1, Email2, IdReferido, IdCodigoPais, IdCiudad, HoraContacto," +
                    "HoraPeru, IdCargo, IdAFormacion, IdATrabajo, IdIndustria, IdEmpresa, Asociado From com.V_DatosAlumno_MessengerChat Where Estado = 1 and Id=@Id";
                var Alumno = _dapper.FirstOrDefault(_queryAlumno, new { Id = IdAlumno });
                return JsonConvert.DeserializeObject<AlumnoInformacionMessengerDTO>(Alumno);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la ciudad y el pais de un alumno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AlumnoCiudadPaisDTO ObtenerCiudadPais(int id)
        {
            try
            {
                AlumnoCiudadPaisDTO alumno = new AlumnoCiudadPaisDTO();
                var _query = "SELECT IdCiudad, IdPais FROM com.V_TAlumno_ObtenerCiudadPais WHERE Id = @id AND Estado = 1";
                var alumnoDB = _dapper.FirstOrDefault(_query, new { id });
                alumno = JsonConvert.DeserializeObject<AlumnoCiudadPaisDTO>(alumnoDB);
                return alumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene una lista de alumnos por email
        /// </summary>
        /// <param name="email1">Correo principal del alumno a buscar</param>
        /// <param name="email2">Correo secundario del alumno a buscar</param>
        /// <returns>Lista de objetos de clase AlumnoMailDTO</returns>
        public List<AlumnoMailDTO> ObtenerAlumnosPorEmail(string email1, string email2)
        {
            try
            {
                List<AlumnoMailDTO> alumnoMails = new List<AlumnoMailDTO>();
                email1 = String.IsNullOrEmpty(email1) ? "-|!x!|-" : email1;
                email2 = String.IsNullOrEmpty(email2) ? "-|!x!|-" : email2;
                var _query = "SELECT Id, Email1 ,Email2 FROM mkt.V_TAlumno_ObtenerPorEmail WHERE ( Rtrim(Ltrim(@email1)) = Rtrim(Ltrim(Email1)) OR Rtrim(Ltrim(@email1)) = Rtrim(Ltrim(Email2)) OR Isnull(Rtrim(Ltrim(@email2)), '-|!x!|-') = Rtrim(Ltrim(Email1)) OR Isnull(Rtrim(Ltrim(@email2)), '-|!x!|-') = Rtrim(Ltrim(Email2)) ) AND Estado = 1";
                var alumnoDB = _dapper.QueryDapper(_query, new { email1, email2 });
                alumnoMails = JsonConvert.DeserializeObject<List<AlumnoMailDTO>>(alumnoDB);
                return alumnoMails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Valida si existe el alumno con email1 o email2
        /// </summary>
        /// <param name="email1">Correo principal del alumno a buscar</param>
        /// <param name="email2">Correo secundario del alumno a buscar</param>
        /// <param name="id">Id del contacto</param>
        /// <returns>Booleano</returns>
        public bool ExisteContacto(string email1, string email2, int id = 0)
        {
            try
            {
                bool existe = true;
                var alumnos = this.ObtenerAlumnosPorEmail(email1, email2).ToList();
                if (alumnos.Count() == 0)
                {
                    existe = false;
                }
                else if (alumnos.Count() == 1)
                {
                    // Si es el registro que se esta editando, retorna false por que no existe duplicados, si podria admitirlo en cualquiera: email 1 o email2
                    existe = !(alumnos.FirstOrDefault().Id == id);
                }
                else
                {
                    //Verificar el caso, que hay varias filas, pero en el row que se esta editando se quiere pasar el email2 y duplicarlo en email1
                    bool CumpleCondiciones = false;
                    var alumnoDB = JsonConvert.DeserializeObject<AlumnoMailDTO>(JsonConvert.SerializeObject(this.GetBy(w => w.Id == id, x => new { x.Id, x.Email1, x.Email2 }).FirstOrDefault()));
                    foreach (var alumno in alumnos)
                    {
                        if (id == alumno.Id && string.IsNullOrEmpty(alumnoDB.Email1) && alumno.Email2.Equals(email1))
                        {
                            CumpleCondiciones = true;
                        }
                    }

                    if (CumpleCondiciones)
                    {
                        existe = false;
                    }

                }
                return existe;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<AlumnoReferidosDTO> ObtenerReferidos(int idAlumno)
        {
            try
            {
                return GetBy(x => x.IdReferido == idAlumno).Select(y => new AlumnoReferidosDTO
                {
                    Id = y.Id,
                    Nombre1 = y.Nombre1,
                    Nombre2 = y.Nombre2,
                    ApellidoPaterno = y.ApellidoPaterno,
                    ApellidoMaterno = y.ApellidoMaterno,
                    Telefono = y.Telefono,
                    Celular = y.Celular,
                    Email1 = y.Email1,
                    Email2 = y.Email2,
                    HoraPeru = y.HoraPeru,
                }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un alumno para poder actualizar el nombre del visitante en el Chat del portal Web
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AlumnoPortalWebDTO ObtenerAlumnoChatPortalWebPorId(int id)
        {
            try
            {
                AlumnoPortalWebDTO alumno = new AlumnoPortalWebDTO();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre1, ApellidoPaterno, ApellidoMaterno, Estado FROM com.V_TAlumno_ObtenerParaChaPWSignalR WHERE Estado = 1 AND Id = @id";
                var alumnoDB = _dapper.FirstOrDefault(_query, new { id });
                alumno = JsonConvert.DeserializeObject<AlumnoPortalWebDTO>(alumnoDB);
                return alumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// Valida si puede mostrarse o no en la agenda
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public ResultadoFinalVisualizarOportunidadDTO ValidarVisualizarAgenda(int idOportunidad, int idPersonal)
        {
            try
            {
                var _resultado = new ResultadoFinalVisualizarOportunidadDTO();
                var query = $@"mkt.SP_ValidarVisualizarOportunidadAgenda";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { idOportunidad, idPersonal });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ResultadoFinalVisualizarOportunidadDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Valida si puede mostrarse o no en la agenda
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public ResultadoFinalDTO InsertarSolicitudVisualizarDatosOportunidad(int idOportunidad, int idPersonal)
        {
            try
            {
                var _resultado = new ResultadoFinalDTO();
                var query = $@"com.SP_InsertarVisualizacionOportunidad";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { idOportunidad, idPersonal });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ResultadoFinalDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Calcula el numero de oportunidades de todos los contactos y los actualiza en el contacto
        /// </summary>
        public void CalcularNumeroOportunidadPorAlumno()
        {
            try
            {
                _dapper.QuerySPDapper("mkt.SP_CalcularNroOportunidadPorAlumno", null);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: AlumnoRepositorio
        ///Autor: Jose V.
        ///Fecha: 28/04/2021
        /// <summary>
        /// Obtiene el email1 del Alumno
        /// </summary>
        /// <param name="id"> Id alumno </param>
        /// <returns> Email1 del alumo </returns>
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
        /// ELimina de forma fisica de la base de datos de Alumno
        /// </summary>
        /// <returns></returns>
        public bool EliminarFisicaAlumno(string tablaV3, string tablaV4, int idV4, string idv3, int? id_v3)
        {
            try
            {
                bool expositor = new bool();

                string queryExpositor = _dapper.QuerySPDapper("conf.SP_EliminarRegistroTablaMaestro", new { tablaV3, tablaV4, idV4, idv3, id_v3 });
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

        /// <summary>
        /// Obtiene los alumnos suscritos por email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<AlumnoBO> ObtenerSuscritosPorEmail(string email)
        {
            try
            {
                return this.GetBy(x => x.DeSuscrito == false && x.Email1 == email).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Desuscribir(int id, string nombreUsuario)
        {
            try
            {
                var alumno = this.FirstById(id);
                var listaAlumnosADesuscribir = this.ObtenerSuscritosPorEmail(alumno.Email1);
                foreach (var item in listaAlumnosADesuscribir)
                {
                    item.DeSuscrito = true;
                    item.UsuarioModificacion = nombreUsuario;
                    item.FechaModificacion = DateTime.Now;
                }
                return this.Update(listaAlumnosADesuscribir);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        ///Repositorio: AlumnoRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene el usuario y contraseña inicial del alumno en IdMoodle
        /// </summary>
        /// <param name="IdAlumno"> Id del Alumno </param>
        /// <returns> Acceso moodle alumno : AccesosMoodleDTO </returns>
        public AccesosMoodleDTO ObtenerAccesosInicialesMoodle(int IdAlumno)
        {
            try
            {
                var query = "SELECT IdAlumno, IdMoodle, UsuarioMoodle, PasswordMoodle FROM [ope].[V_TAccesosMoodle_ObtenerAccesosMoodle] WHERE IdAlumno = @IdAlumno";
                var res = _dapper.FirstOrDefault(query, new { IdAlumno });
                return JsonConvert.DeserializeObject<AccesosMoodleDTO>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene el pais de origen del alumno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerPaisOrigen(int id)
        {
            try
            {
                var _resultado = new ValorStringDTO();
                var query = $@"mkt.SP_ObtenerNombrePaisOrigenAlumno";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdAlumno = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene el pais de origen del alumno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerCiudadOrigen(int id)
        {
            try
            {
                var _resultado = new ValorStringDTO();
                var query = $@"mkt.SP_ObtenerNombreCiudadOrigenAlumno";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdAlumno = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Obtiene el numero del alumno + codigo de pais para envio de mensajes de whatsapp
        /// </summary>
        /// <param name="IdConjuntoListaDetalle"></param>
        /// <returns></returns>
        public WhatsAppResultadoConjuntoListaDTO ObtenerNumeroConCodigoPaisWhatsApp(int idAlumno)
        {
            try
            {
                WhatsAppResultadoConjuntoListaDTO resultado = new WhatsAppResultadoConjuntoListaDTO();
                string _queryResultado = "SELECT IdAlumno,Celular,IdCodigoPais FROM [mkt].[V_TAlumno_NumeroWhatsApp] WHERE IdAlumno=@IdAlumno AND Estado = 1";
                var queryResultado = _dapper.FirstOrDefault(_queryResultado, new { IdAlumno = idAlumno });
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<WhatsAppResultadoConjuntoListaDTO>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene los alumnos de contactos a validar
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<AlumnoBO> ObtenerContactosValidarEstadoContactoWhatsApp()
        {
            try
            {
                var listaIdsAlumnoPorValidar = new List<ValorIntDTO>();
                string _query = "SELECT Valor FROM mkt.V_ObtenerContactosValidarEstadoContactoWhatsApp";
                var resultado = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaIdsAlumnoPorValidar = JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultado);
                }
                var newlist = listaIdsAlumnoPorValidar.Select(w => w.Valor).ToList();
                return this.GetBy(x => newlist.Contains(x.Id)).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Obtiene los alumnos de contactos a validarWhatsapp
        /// </summary>
        /// <returns></returns>
        public List<AlumnoWhatsappDTO> ObtenerALumnosaValidarWhatsapp()
        {
            try
            {
                var listaIdsAlumnoPorValidar = new List<AlumnoWhatsappDTO>();
                string _query = "Select  Celular,IdCodigoPais, IdAlumno  From mkt.V_ListaAlumnosValidacionWhatsapp where  celular !='1'";
                var resultado = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaIdsAlumnoPorValidar = JsonConvert.DeserializeObject<List<AlumnoWhatsappDTO>>(resultado);
                }

                return listaIdsAlumnoPorValidar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtiene los alumnos de contactos a validar
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<ValorIntDTO> ObtenerContactosValidarEstadoContactoWhatsAppv2()
        {
            try
            {
                var listaIdsAlumnoPorValidar = new List<ValorIntDTO>();
                string _query = "SELECT Valor FROM mkt.V_ObtenerContactosValidarEstadoContactoWhatsApp";
                var resultado = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaIdsAlumnoPorValidar = JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultado);
                }
                return listaIdsAlumnoPorValidar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene el nombre del programa general del ultimo envio masivo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerNombreProgramaGeneralUltimoEnvioMasivo(int id)
        {
            try
            {
                var _resultado = new ValorStringDTO();
                var query = $@"mkt.SP_ObtenerNombreProgramaGeneralUltimoEnvioMasivo";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdAlumno = id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene el nombre del programa general de la ultima solicitud de información
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerNombreProgramaGeneralUltimaSolicitudInformacion(int id)
        {
            try
            {
                var _resultado = new ValorStringDTO();
                var query = $@"mkt.SP_ObtenerNombreProgramaGeneralUltimaSolicitudInformacion";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdAlumno = id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la URL de feliz cumpleaños
        /// </summary>
        /// <returns>Cadena con la URL de la imagen de feliz cumpleaños</returns>
        public string ObtenerUrlImagenFelizCumpleanios()
        {
            try
            {
                var resultadoFinal = new ValorStringDTO();
                var query = $@"mkt.SP_ObtenerUrlFelizCumpleanios";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ObtenerNumeroWhatsApp(int codigoPais, string celular)
        {
            try
            {
                if (codigoPais == 51)
                {
                    if (celular.Length == 9)
                    {
                        celular = "51" + celular;
                    }
                }
                else if (codigoPais == 57)
                {
                    if (celular.StartsWith("00"))
                    {
                        celular = celular.Substring(2, celular.Length - 2);
                    }
                    if (celular.Length < 12)
                    {
                        celular = "57" + celular;
                    }
                }
                else if (codigoPais == 591)
                {
                    if (celular.StartsWith("00"))
                    {
                        celular = celular.Substring(2, celular.Length - 2);
                    }
                    if (celular.Length < 11)
                    {
                        celular = "591" + celular;
                    }
                }
                return celular;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la fecha de inicio de capacitacion
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera que esta enlazada (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la fecha de inicio de capacitacion de una matricula cabecera</returns>
        public string ObtenerFechaInicioCapacitacion(int IdMatriculaCabecera)
        {
            try
            {
                var FechaInicio = new ValorStringDTO();
                string query = "SELECT FechaInicio AS Valor FROM pla.V_FechaInicioFinCapacitacion Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapper.FirstOrDefault(query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return FechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string ObtenerFechaInicioCapacitacionModuloPespecifico(int IdMatriculaCabecera, int IdPespecifico)
        {
            try
            {
                var FechaInicio = new ValorStringDTO();
                string _query = "SELECT FechaInicio AS Valor FROM pla.V_FechaInicioFinCapacitacionModuloPespecifico Where IdMatriculaCabecera = @IdMatriculaCabecera and IdPEspecifico = @IdPespecifico";
                var resultado = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera, IdPespecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return FechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ObtenerFechaInicioCapacitacionModuloCursoMoodle(int IdMatriculaCabecera, int IdCursoMoodle)
        {
            try
            {
                var FechaInicio = new ValorStringDTO();
                string _query = "SELECT FechaInicio AS Valor FROM pla.V_FechaInicioFinCapacitacionAOnlineCursoMoodle Where IdMatriculaCabecera = @IdMatriculaCabecera and IdCursoMoodle = @IdCursoMoodle";
                var resultado = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera, IdCursoMoodle });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return FechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la fecha de fin de capacitacion
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera que esta enlazada (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la fecha de fin de capacitacion de una matricula cabecera</returns>
        public string ObtenerFechaFinCapacitacion(int IdMatriculaCabecera)
        {
            try
            {
                var FechaInicio = new ValorStringDTO();
                string _query = "SELECT FechaFin AS Valor FROM pla.V_FechaInicioFinCapacitacion Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return FechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ObtenerFechaFinCapacitacionModuloPespecifico(int IdMatriculaCabecera, int IdPespecifico)
        {
            try
            {
                var FechaInicio = new ValorStringDTO();
                string _query = "SELECT FechaFin AS Valor FROM pla.V_FechaInicioFinCapacitacionModuloPespecifico Where IdMatriculaCabecera = @IdMatriculaCabecera and IdPEspecifico = @IdPespecifico ";
                var resultado = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera, IdPespecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return FechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ObtenerFechaFinCapacitacionModuloCursoMoodle(int IdMatriculaCabecera, int IdCursoMoodle)
        {
            try
            {
                var FechaInicio = new ValorStringDTO();
                string _query = "SELECT FechaFin AS Valor FROM pla.V_FechaInicioFinCapacitacionAOnlineCursoMoodle Where IdMatriculaCabecera = @IdMatriculaCabecera and IdCursoMoodle = @IdCursoMoodle";
                var resultado = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera, IdCursoMoodle });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return FechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la nota promedio del alumno coincidente con la matricula cabecera
        /// </summary>
        /// <param name="idMatriculaCabecera">Es el Id de la Matricula Cabecera (PK de la tabla fin.T_MatriculaCabeceraa)</param>
        /// <returns>Cadena con nota promedio del alumno</returns>
        public string ObtenerNotaPromedio(int idMatriculaCabecera)
        {
            try
            {
                var notaPromedio = new ValorStringDTO();
                string query = "SELECT Nota AS Valor FROM ope.V_Alumno_NotaPromedio Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapper.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    notaPromedio = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return notaPromedio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ObtenerNotaPromedioModulo(int IdMatriculaCabecera, int IdCurso)
        {
            try
            {
                var NotaPromedio = new ValorStringDTO();
                string _query = "SELECT Nota AS Valor  FROM ope.V_ObtenerNotaPromedioModulo Where IdMatriculaCabecera = @IdMatriculaCabecera and IdCurso =@IdCurso";
                var resultado = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera, IdCurso });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    NotaPromedio = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return NotaPromedio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene fecha de emision
        /// </summary>
        /// <returns>Cadena con la fecha de emision formateada en texto comprensible</returns>
        public string ObtenerFechaEmision()
        {
            try
            {
                var FechaEmision = new ValorStringDTO();
                string _query = "SELECT FechaEmision AS Valor FROM ope.V_ObtenerFechaEmision ";
                var resultado = _dapper.FirstOrDefault(_query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaEmision = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return FechaEmision.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene codigo del certificado
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con la el codigo del certificado</returns>
        public string ObtenerCodigoCertificado(int idMatriculaCabecera)
        {
            try
            {
                var CodigoCertificado = new ValorStringDTO();
                string _query = "SELECT CodigoCertificado AS Valor FROM ope.V_ObtenerCodigoCertificado Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    CodigoCertificado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return CodigoCertificado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string ObtenerCodigoCertificadoModular(int IdMatriculaCabecera)
        {
            try
            {
                var CodigoCertificado = new ValorStringDTO();
                string _query = "SELECT CodigoCertificado AS Valor FROM ope.V_ObtenerCodigoCertificadoModular Where IdMatriculaCabecera = @IdMatriculaCabecera ";
                var resultado = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    CodigoCertificado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return CodigoCertificado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string ObtenerCodigoCertificadoIrca(int IdMatriculaCabecera)
        {
            try
            {
                var CodigoCertificado = new ValorStringDTO();
                string _query = "SELECT CodigoCertificado AS Valor FROM ope.V_ObtenerCodigoCertificadoIrca Where IdMatriculaCabecera = @IdMatriculaCabecera ";
                var resultado = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    CodigoCertificado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return CodigoCertificado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CronogramaNotaDTO> ObtenerCronogramaNota(int IdMatriculaCabecera)
        {
            try
            {
                List<CronogramaNotaDTO> cronogramaNota = new List<CronogramaNotaDTO>();
                string _query = "SELECT Curso,Nota,Estado  FROM ope.V_ObtenerCronogramaNota Where IdMatriculaCabecera = @IdMatriculaCabecera order by Orden ";
                var resultado = _dapper.QueryDapper(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    cronogramaNota = JsonConvert.DeserializeObject<List<CronogramaNotaDTO>>(resultado);
                }
                return cronogramaNota;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CronogramaAsistenciaDTO> ObtenerCronogramaAsistencia(int IdMatriculaCabecera)
        {
            try
            {
                List<CronogramaAsistenciaDTO> cronogramaNota = new List<CronogramaAsistenciaDTO>();
                string _query = "SELECT Curso,PorcentajeAsistencia FROM ope.V_ObtenerCronogramaPorcentajeAsistencia Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapper.QueryDapper(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    cronogramaNota = JsonConvert.DeserializeObject<List<CronogramaAsistenciaDTO>>(resultado);
                }
                return cronogramaNota;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string guardarArchivosQR(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"repositorioweb/certificados/CodigoQR/";

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
        /// Obtiene el Id,NombreCompleto de los alumnos
        /// </summary>
        /// <returns></returns>
        public List<AlumnoFiltroAutocompleteDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<AlumnoFiltroAutocompleteDTO> alumnos = new List<AlumnoFiltroAutocompleteDTO>();
                string _queryAlumnoFiltro = string.Empty;
                _queryAlumnoFiltro = "SELECT top 100 Id,NombreCompleto FROM mkt.V_TAlumno_NombreCompleto WHERE Estado = 1 ORDER By NombreCompleto ASC";
                var alumnoDB = _dapper.QueryDapper(_queryAlumnoFiltro, null);
                if (!string.IsNullOrEmpty(alumnoDB) && !alumnoDB.Contains("[]"))
                {
                    alumnos = JsonConvert.DeserializeObject<List<AlumnoFiltroAutocompleteDTO>>(alumnoDB);
                }
                return alumnos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Lista TipoCategoriaError
        /// </summary>
        /// <returns></returns>
        public List<AlumnoFiltroAutocompleteDTO> ObtenerTodoFiltroTipoCategoriaError()
        {
            try
            {
                List<AlumnoFiltroAutocompleteDTO> alumnos = new List<AlumnoFiltroAutocompleteDTO>();
                string _queryAlumnoFiltro = string.Empty;
                _queryAlumnoFiltro = "SELECT Id,Nombre AS NombreCompleto FROM pla.V_TipoCategoriaErrorFiltro WHERE Estado = 1 ORDER By Nombre ASC";
                var alumnoDB = _dapper.QueryDapper(_queryAlumnoFiltro, null);
                if (!string.IsNullOrEmpty(alumnoDB) && !alumnoDB.Contains("[]"))
                {
                    alumnos = JsonConvert.DeserializeObject<List<AlumnoFiltroAutocompleteDTO>>(alumnoDB);
                }
                return alumnos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: AlumnoRepositorio
        ///Autor: Edgar S.
        ///Fecha: 01/03/2021
        /// <summary>
        /// Obtiene el Id y Email del Alumno a traves del Email.
        /// </summary>
        /// <param name="email"> Búsqueda de Email </param>
        /// <returns> List<FiltroBasicoDTO> </returns>
        public List<FiltroBasicoDTO> CargarEmailAlumnoAutoComplete(string email)
        {
            try
            {
                string query = "SELECT Id, Email1 AS Nombre from mkt.V_TAlumno_NombreCompletoEmail WHERE Email1 LIKE CONCAT('%',@email,'%') AND Estado = 1 ORDER By Email1 ASC";
                string queryRespuesta = _dapper.QueryDapper(query, new { email });
                return JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: AlumnoRepositorio
        ///Autor: Edgar S.
        ///Fecha: 01/03/2021
        /// <summary>
        /// Obtiene el Id y nombre del Alumno a traves del nombre.
        /// </summary>
        /// <param name="nombre"> Búsqueda de contacto </param>
        /// <returns> List<FiltroBasicoDTO> </returns>
        public List<FiltroBasicoDTO> CargarNombreAlumnoAutoComplete(string nombre)
        {
            try
            {
                string query = "SELECT Id, NombreCompleto AS Nombre from mkt.V_TAlumno_ObtenerNombreApellidoAlumno WHERE NombreCompleto LIKE CONCAT('%',@nombre,'%') AND Estado = 1 ORDER By nombre ASC";
                string queryRespuesta = _dapper.QueryDapper(query, new { nombre });
                return JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la fecha de inicio de capacitacion
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera que esta enlazada (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la fecha de inicio de capacitacion de una matricula cabecera</returns>
        public string ObtenerFechaInicioCapacitacionPortalWeb(int IdMatriculaCabecera)
        {
            try
            {
                var FechaInicio = new ValorStringDTO();
                string query = "SELECT FechaInicio AS Valor FROM pla.V_FechaInicioFinCapacitacionPortalWeb     Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapper.FirstOrDefault(query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                else
                {

                    FechaInicio.Valor = ObtenerFechaInicioCapacitacion(IdMatriculaCabecera);
                }
                if (FechaInicio.Valor == " de  del ")
                {
                    FechaInicio.Valor = ObtenerFechaInicioCapacitacion(IdMatriculaCabecera);
                }
                return FechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la fecha de fin de capacitacion
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera que esta enlazada (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la fecha de fin de capacitacion de una matricula cabecera</returns>
        public string ObtenerFechaFinCapacitacionPortalWeb(int IdMatriculaCabecera)
        {
            try
            {
                var FechaInicio = new ValorStringDTO();
                string _query = "SELECT FechaFin AS Valor FROM pla.V_FechaInicioFinCapacitacionPortalWeb     Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                else
                {
                    FechaInicio.Valor = ObtenerFechaFinCapacitacion(IdMatriculaCabecera);
                }
                if (FechaInicio.Valor == " de  del ")
                {
                    FechaInicio.Valor = ObtenerFechaFinCapacitacion(IdMatriculaCabecera);
                }
                return FechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene las credenciales del portal web del alumno solicitado
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <returns>Cadena formateada con la fecha de fin de capacitacion de una matricula cabecera</returns>
        public CredencialesPortalWebAlumnoDTO ObtenerCredencialesPortalWebPorIdAlumno(int idAlumno)
        {
            try
            {
                CredencialesPortalWebAlumnoDTO credencialesObtenidas = new CredencialesPortalWebAlumnoDTO();

                string query = "SELECT IdAlumno, PortalWebUsuario, PortalWebClave FROM conf.V_ObtenerCredencialesPortalWebPorIdAlumno WHERE IdAlumno = @IdAlumno";
                var resultado = _dapper.FirstOrDefault(query, new { IdAlumno = idAlumno });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    credencialesObtenidas = JsonConvert.DeserializeObject<CredencialesPortalWebAlumnoDTO>(resultado);
                }

                return credencialesObtenidas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="cantidad"> Cantidad de alumnos </param>
        /// <param name="iterador"> Iterador </param>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoBO> ObtenerALumnosaValidarWhatsappPeru(int cantidad,int iterador)
        {
            try
            {
                var listaAlumnos= new List<AlumnoBO>();
                var saltar = cantidad * iterador;
                string query = "SELECT * FROM mkt.T_Alumno WHERE IdCodigoPais=51 AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 ORDER BY Id ASC OFFSET @Saltar ROWS FETCH NEXT @Cantidad ROWS ONLY ";
                var resultado = _dapper.QueryDapper(query, new { Saltar=saltar,Cantidad=cantidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoBO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="cantidad"> Cantidad de alumnos </param>
        /// <param name="iterador"> Iterador </param>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoBO> ObtenerALumnosaValidarWhatsappColombia(int cantidad, int iterador)
        {
            try
            {
                var listaAlumnos = new List<AlumnoBO>();
                var saltar = cantidad * iterador;
                string query = "SELECT * FROM mkt.T_Alumno WHERE IdCodigoPais=57 AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 ORDER BY Id ASC OFFSET @Saltar ROWS FETCH NEXT @Cantidad ROWS ONLY ";
                var resultado = _dapper.QueryDapper(query, new { Saltar = saltar, Cantidad = cantidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoBO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="cantidad"> Cantidad de alumnos </param>
        /// <param name="iterador"> Iterador </param>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoBO> ObtenerALumnosaValidarWhatsappBolivia(int cantidad, int iterador)
        {
            try
            {
                var listaAlumnos = new List<AlumnoBO>();
                var saltar = cantidad * iterador;
                string query = "SELECT * FROM mkt.T_Alumno WHERE IdCodigoPais=591 AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 ORDER BY Id ASC OFFSET @Saltar ROWS FETCH NEXT @Cantidad ROWS ONLY ";
                var resultado = _dapper.QueryDapper(query, new { Saltar = saltar, Cantidad = cantidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoBO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="cantidad"> Cantidad de alumnos </param>
        /// <param name="iterador"> Iterador </param>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoBO> ObtenerALumnosaValidarWhatsappInternacional(int cantidad, int iterador)
        {
            try
            {
                var listaAlumnos = new List<AlumnoBO>();
                var saltar = cantidad * iterador;
                string query = "SELECT * FROM mkt.T_Alumno WHERE IdCodigoPais NOT IN (591,57,51) AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 ORDER BY Id ASC OFFSET @Saltar ROWS FETCH NEXT @Cantidad ROWS ONLY ";
                var resultado = _dapper.QueryDapper(query, new { Saltar = saltar, Cantidad = cantidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoBO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="alumnos"> Alumnos a actualizar </param>
        /// <param name="estadoWhatsApp"> Estado a modificar a los alumnos </param>
        /// <returns> ResultadoFinalDTO </returns>
        public ResultadoFinalDTO ActualizarValidos(string alumnos, int estadoWhatsApp)
        {
            try
            {
                var respuesta = new ResultadoFinalDTO();
                string query = "mkt.SP_ActualizarIdEstadoContactoWhatsApp";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { Id= alumnos, Estado=estadoWhatsApp});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(resultado);
                }

                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="alumnos"> Alumnos a actualizar </param>
        /// <param name="estadoWhatsApp"> Estado a modificar a los alumnos </param>
        /// <returns> ResultadoFinalDTO </returns>
        public ResultadoFinalDTO ActualizarValidosSecundario(string alumnos, int estadoWhatsApp)
        {
            try
            {
                var respuesta = new ResultadoFinalDTO();
                string query = "mkt.SP_ActualizarIdEstadoContactoWhatsAppSecundario";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { Id = alumnos, Estado = estadoWhatsApp });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(resultado);
                }

                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoBO> ObtenerALumnosaRegularizarWhatsappPeru()
        {
            try
            {
                var listaAlumnos = new List<AlumnoBO>();
                string query = "SELECT * FROM mkt.T_Alumno WHERE IdCodigoPais=51 AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 AND IdEstadoContactoWhatsApp IS NULL ";
                var resultado = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoBO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoBO> ObtenerALumnosaRegularizarWhatsappColombia()
        {
            try
            {
                var listaAlumnos = new List<AlumnoBO>();
                string query = "SELECT * FROM mkt.T_Alumno WHERE IdCodigoPais=57 AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 AND IdEstadoContactoWhatsApp IS NULL";
                var resultado = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoBO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoBO> ObtenerALumnosaRegularizarWhatsappBolivia()
        {
            try
            {
                var listaAlumnos = new List<AlumnoBO>();
                string query = "SELECT * FROM mkt.T_Alumno WHERE IdCodigoPais=591 AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 AND IdEstadoContactoWhatsApp IS NULL ";
                var resultado = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoBO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoBO> ObtenerALumnosaRegularizarWhatsappInternacional()
        {
            try
            {
                var listaAlumnos = new List<AlumnoBO>();
                string query = "SELECT * FROM mkt.T_Alumno WHERE IdCodigoPais NOT IN (591,57,51) AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 AND IdEstadoContactoWhatsApp IS NULL ";
                var resultado = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoBO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
