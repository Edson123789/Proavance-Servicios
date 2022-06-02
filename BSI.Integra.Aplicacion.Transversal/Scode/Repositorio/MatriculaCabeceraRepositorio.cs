using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Helper;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Finanzas/MatriculaCabecera
    /// Autor: Fischer Valdez - Carlos Crispin - Gian Miranda - Edgar S.
    /// Fecha: 05/02/2021
    /// <summary>
    /// Repositorio para consultas de fin.T_MatriculaCabecera
    /// </summary>
    public class MatriculaCabeceraRepositorio : BaseRepository<TMatriculaCabecera, MatriculaCabeceraBO>
    {
        #region Metodos Base
        public MatriculaCabeceraRepositorio() : base()
        {
        }
        public MatriculaCabeceraRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MatriculaCabeceraBO> GetBy(Expression<Func<TMatriculaCabecera, bool>> filter)
        {
            IEnumerable<TMatriculaCabecera> listado = base.GetBy(filter);
            List<MatriculaCabeceraBO> listadoBO = new List<MatriculaCabeceraBO>();
            foreach (var itemEntidad in listado)
            {
                MatriculaCabeceraBO objetoBO = Mapper.Map<TMatriculaCabecera, MatriculaCabeceraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MatriculaCabeceraBO FirstById(int id)
        {
            try
            {
                TMatriculaCabecera entidad = base.FirstById(id);
                MatriculaCabeceraBO objetoBO = new MatriculaCabeceraBO();
                Mapper.Map<TMatriculaCabecera, MatriculaCabeceraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MatriculaCabeceraBO FirstBy(Expression<Func<TMatriculaCabecera, bool>> filter)
        {
            try
            {
                TMatriculaCabecera entidad = base.FirstBy(filter);
                MatriculaCabeceraBO objetoBO = Mapper.Map<TMatriculaCabecera, MatriculaCabeceraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(MatriculaCabeceraBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMatriculaCabecera entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MatriculaCabeceraBO> listadoBO)
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

        public bool Update(MatriculaCabeceraBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMatriculaCabecera entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MatriculaCabeceraBO> listadoBO)
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
        private void AsignacionId(TMatriculaCabecera entidad, MatriculaCabeceraBO objetoBO)
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

        private TMatriculaCabecera MapeoEntidad(MatriculaCabeceraBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMatriculaCabecera entidad = new TMatriculaCabecera();
                entidad = Mapper.Map<MatriculaCabeceraBO, TMatriculaCabecera>(objetoBO,
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

        public CodigoMatriculaDocumentoDTO CodigoMatriculaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                string _queryIdMatricula1 = "Select Id,CodigoMatricula,FechaMatricula FROM fin.V_TMatriculaCabecera_MatriculaPorIdOportunidad  where  IdOportunidad=@IdOportunidad and EsAprobado =1 and Estado=1";
                var _idMatricula1 = _dapper.FirstOrDefault(_queryIdMatricula1, new { IdOportunidad = idOportunidad });
                return JsonConvert.DeserializeObject<CodigoMatriculaDocumentoDTO>(_idMatricula1);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la matricula por la Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Es el Id de la Oportunidad</param>
        /// <returns>Retorna informacion de la matricula</returns>
        public MatriculaTemporalDTO ObtenerMatriculaPorOportunidad(int idOportunidad)
        {
            try
            {
                string querymatricula = "select  IdMatricula, CodigoMatricula, FechaMatricula from com.V_TOportunidad_CodigoMatricula" +
                                        " where IdOportunidad = @IdOportunidad and EstadoPE = 1";
                var matricula = _dapper.FirstOrDefault(querymatricula, new { IdOportunidad = idOportunidad });
                return JsonConvert.DeserializeObject<MatriculaTemporalDTO>(matricula);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<MatriculaDTO> ObtenerMatriculaPorAlumno(int IdAlumno)
        {
            try
            {
                string _querymatricula = "select  Id,CodigoMatricula,NombreProgramaGeneral,VersionPrograma,IdCentroCosto,Documentos from fin.V_ObtenerMatriculasPorAlumno " +
                                        " where IdAlumno = @IdAlumno ";
                var _Matricula = _dapper.QueryDapper(_querymatricula, new { IdAlumno });
                return JsonConvert.DeserializeObject<List<MatriculaDTO>>(_Matricula);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<EstadosMatriculaDTO> ObtenerEstadosMatricula()
        {
            try
            {
                List<EstadosMatriculaDTO> fasesOportunidad = new List<EstadosMatriculaDTO>();
                var _query = "SELECT Id, Nombre FROM fin.V_TEstadosMatriculas WHERE Estado = 1";
                var fasesOportunidadDB = _dapper.QueryDapper(_query, new { });
                fasesOportunidad = JsonConvert.DeserializeObject<List<EstadosMatriculaDTO>>(fasesOportunidadDB);
                return fasesOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 18/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos asociados 
        /// </summary>
        /// <param name="idAlumno">Id del alumno</param>
        /// <returns>Tipo de objeto que retorna la función</returns>

        ///Repositorio: MatriculaCabecera
        ///Autor: Jose Villena
        ///Fecha: 01/05/2021
        /// <summary>
        /// Obtener Codigo de Matricula y Programa del alumno
        /// </summary>
        /// <param name="idAlumno"> Id Alumno </param>        
        /// <returns> Lista Cronograma del Alumno : List<CodigoMatriculaPEspecificoDTO> </returns>
        public List<CodigoMatriculaPEspecificoDTO> ObtenerCodigoMatriculaPEspecificoPorAlumno(int idAlumno)
        {
            try
            {
                List<CodigoMatriculaPEspecificoDTO> codigosMatriculaPEspecifico = new List<CodigoMatriculaPEspecificoDTO>();
                var query = "SELECT CodigoMatricula, PEspecifico FROM fin.V_ObtenerCodigoMatriculaPEspecifico WHERE  IdAlumno = @idAlumno";
                var codigosMatriculaPEspecificoDB = _dapper.QueryDapper(query, new { idAlumno });
                if (!string.IsNullOrEmpty(codigosMatriculaPEspecificoDB) && !codigosMatriculaPEspecificoDB.Contains("[]"))
                {
                    codigosMatriculaPEspecifico = JsonConvert.DeserializeObject<List<CodigoMatriculaPEspecificoDTO>>(codigosMatriculaPEspecificoDB);
                }
                return codigosMatriculaPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: MatriculaCabecera
        ///Autor: Jose Villena
        ///Fecha: 01/05/2021
        /// <summary>
        /// Devuelve los identificadores importantes por matricula de alumno  
        /// </summary>
        /// <param name="idAlumno"> Id Alumno </param>        
        /// <returns> Lista Identificadores Matricula : List<IdentificadorMatriculaComboDTO> </returns>
        public List<IdentificadorMatriculaComboDTO> ObtenerIdentificadoresMatriculaComboPorAlumno(int idAlumno)
        {
            try
            {
                List<IdentificadorMatriculaComboDTO> codigosMatriculaPEspecifico = new List<IdentificadorMatriculaComboDTO>();
                var query = "SELECT IdMatriculaCabecera, CodigoMatricula, IdOportunidad, PEspecifico FROM ope.V_ObtenerIdentificadoresMatricula WHERE IdAlumno = @idAlumno";
                var codigosMatriculaPEspecificoDB = _dapper.QueryDapper(query, new { idAlumno });
                if (!string.IsNullOrEmpty(codigosMatriculaPEspecificoDB) && !codigosMatriculaPEspecificoDB.Contains("[]"))
                {
                    codigosMatriculaPEspecifico = JsonConvert.DeserializeObject<List<IdentificadorMatriculaComboDTO>>(codigosMatriculaPEspecificoDB);
                }
                return codigosMatriculaPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CodigoMatriculaIdStringDTO> ObtenerCodigoMatricula(string Codigo)
        {
            try
            {
                List<CodigoMatriculaIdStringDTO> codigosMatriculaPEspecifico = new List<CodigoMatriculaIdStringDTO>();
                var _query = "SELECT CodigoMatricula as Id FROM fin.T_MatriculaCabecera WHERE  CodigoMatricula like CONCAT('%',@Codigo,'%') ";
                var codigosMatriculaDB = _dapper.QueryDapper(_query, new { Codigo });
                if (!string.IsNullOrEmpty(codigosMatriculaDB) && !codigosMatriculaDB.Contains("[]"))
                {
                    codigosMatriculaPEspecifico = JsonConvert.DeserializeObject<List<CodigoMatriculaIdStringDTO>>(codigosMatriculaDB);
                }
                return codigosMatriculaPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CodigoMatriculaV2DTO ObtenerIdMatriculaPorCodigo(string Codigo)
        {
            try
            {
                List<CodigoMatriculaV2DTO> codigosMatriculaPEspecifico = new List<CodigoMatriculaV2DTO>();
                var _query = "SELECT top 1 Id,CodigoMatricula, EstadoMatricula FROM fin.T_MatriculaCabecera WHERE  CodigoMatricula=@Codigo ";
                var codigosMatriculaDB = _dapper.QueryDapper(_query, new { Codigo });
                if (!string.IsNullOrEmpty(codigosMatriculaDB) && !codigosMatriculaDB.Contains("[]"))
                {
                    codigosMatriculaPEspecifico = JsonConvert.DeserializeObject<List<CodigoMatriculaV2DTO>>(codigosMatriculaDB);
                }
                else {
                    return null;
                }
                return codigosMatriculaPEspecifico[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el idMatriculaCabecera por codigo de matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public IdMatriculaCabeceraDTO ObtenerIdMatriculaCabeceraPorCodigo(string Codigo)
        {
            try
            {
                List<IdMatriculaCabeceraDTO> codigosMatriculaPEspecifico = new List<IdMatriculaCabeceraDTO>();
                var _query = "SELECT top 1 Id FROM fin.T_MatriculaCabecera WHERE  CodigoMatricula=@Codigo and Estado=1 ";
                var codigosMatriculaDB = _dapper.QueryDapper(_query, new { Codigo });
                if (!string.IsNullOrEmpty(codigosMatriculaDB) && !codigosMatriculaDB.Contains("[]"))
                {
                    codigosMatriculaPEspecifico = JsonConvert.DeserializeObject<List<IdMatriculaCabeceraDTO>>(codigosMatriculaDB);
                }
                else
                {
                    return null;
                }
                return codigosMatriculaPEspecifico[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el alumno por el codigo del programa especifico
        /// </summary>
        /// <param name="idCabeceraMatricula"></param>
        /// <returns></returns>
        public List<AlumnoProgramaEspecificoDTO> ObtenerAlumnoProgramaEspecifico(int idCabeceraMatricula)
        {
            try
            {
                List<AlumnoProgramaEspecificoDTO> alumnoProgramaEspecifico = new List<AlumnoProgramaEspecificoDTO>();
                var _query = "SELECT CodigoMatricula, PEspecifico,NombreCompletoAlumno NombreCompleto FROM fin.V_ObtenerAlumnoProgramaEspecifico WHERE IdMatriculaCabecera = @idCabeceraMatricula AND EstadoPEspecifico = 1 AND EstadoAlumno = 1";
                var alumnoProgramaEspecificoDB = _dapper.QueryDapper(_query, new { idCabeceraMatricula });
                if (!string.IsNullOrEmpty(alumnoProgramaEspecificoDB) && !alumnoProgramaEspecificoDB.Contains("[]"))
                {
                    alumnoProgramaEspecifico = JsonConvert.DeserializeObject<List<AlumnoProgramaEspecificoDTO>>(alumnoProgramaEspecificoDB);
                }
                return alumnoProgramaEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene beneficios por el codigo de matricula
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <returns></returns>
        public List<BeneficiosCodigoMatriculaDTO> ObtenerBeneficiosCodigoMatricula(string codigoMatricula)
        {
            try
            {
                List<BeneficiosCodigoMatriculaDTO> beneficiosCodigoMatricula = new List<BeneficiosCodigoMatriculaDTO>();
                var _query = "SELECT Titulo , CodigoMatricula  FROM fin.V_ObtenerBeneficiosCodigoMatricula WHERE CodigoMatricula =   @codigoMatricula AND  EstadoSuscripcionProgramaGeneral= 1 AND EstadoMontoPagoSuscripcion = 1 AND EstadoMontoPagoCronograma = 1 AND EstadoMatriculaCabecera = 1";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(_query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<BeneficiosCodigoMatriculaDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene beneficios por el codigo de matricula
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <returns></returns>
        public List<BeneficiosCodigoMatriculaDTO> ObtenerBeneficiosMatriculaCabecera(string codigoMatricula)
        {
            try
            {
                List<BeneficiosCodigoMatriculaDTO> beneficiosCodigoMatricula = new List<BeneficiosCodigoMatriculaDTO>();
                var _query = "SELECT Titulo , CodigoMatricula  FROM com.V_MatriculaCabeceraBeneficios WHERE CodigoMatricula =  @codigoMatricula AND Estado=1 ";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(_query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<BeneficiosCodigoMatriculaDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene beneficios por el codigo de matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public ResultadoFinalDTO EliminarBeneficiosMatriculaCabeceraIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                ResultadoFinalDTO resultado = new ResultadoFinalDTO();
                var solicitudesCambiosDB = _dapper.QuerySPFirstOrDefault("fin.SP_EliminarBeneficiosMatriculaCabecera", new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(solicitudesCambiosDB) && !solicitudesCambiosDB.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<ResultadoFinalDTO>(solicitudesCambiosDB);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene beneficios por el codigo de matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public ResultadoFinalDTO InsertarBeneficiosMatriculaCabeceraIdMatricula(int idMatriculaCabecera, int nuevoPaquete, int idCronograma)
        {
            try
            {
                ResultadoFinalDTO resultado = new ResultadoFinalDTO();
                var solicitudesCambiosDB = _dapper.QuerySPFirstOrDefault("fin.SP_InsertarBeneficiosMatriculaCabecera", new { idMatriculaCabecera, nuevoPaquete, idCronograma });
                if (!string.IsNullOrEmpty(solicitudesCambiosDB) && !solicitudesCambiosDB.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<ResultadoFinalDTO>(solicitudesCambiosDB);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el listado de alumnos matricula, filtrado por el codigo especifico
        /// </summary>
        /// <param name="idProgramaEspecifico"></param>
        /// <returns></returns>
        public List<AlumnoMatriculaDTO> ObtenerListadoAlumnosMatriculaCodigoPEspecifico(int idProgramaEspecifico)
        {
            try
            {
                List<AlumnoMatriculaDTO> listadoAlumnosMatricula = new List<AlumnoMatriculaDTO>();
                var _query = "SELECT CodigoAlumno, CodigoProgramaEspecifico, NombreProgramaEspecifico, CodigoMatricula, NombreCompletoAlumno,FechaMatricula,Estado FROM fin.V_ObtenerListadoAlumnosMatricula WHERE  IdProgramaEspecifico= @idProgramaEspecifico";
                var listadoAlumnosMatriculaDB = _dapper.QueryDapper(_query, new { idProgramaEspecifico });
                if (!string.IsNullOrEmpty(listadoAlumnosMatriculaDB) && !listadoAlumnosMatriculaDB.Contains("[]"))
                {
                    listadoAlumnosMatricula = JsonConvert.DeserializeObject<List<AlumnoMatriculaDTO>>(listadoAlumnosMatriculaDB);
                }
                return listadoAlumnosMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el listado de alumnos matricula, filtrado por el id del alumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns></returns>
        public List<AlumnoMatriculaDTO> ObtenerListadoAlumnosMatriculaIdAlumno(int idAlumno)
        {
            try
            {
                List<AlumnoMatriculaDTO> listadoAlumnosMatricula = new List<AlumnoMatriculaDTO>();
                var _query = "SELECT CodigoAlumno, CodigoProgramaEspecifico, NombreProgramaEspecifico, CodigoMatricula, NombreCompletoAlumno,FechaMatricula,Estado FROM fin.V_ObtenerListadoAlumnosMatricula WHERE CodigoAlumno= @idAlumno";
                var listadoAlumnosMatriculaDB = _dapper.QueryDapper(_query, new { idAlumno });
                if (!string.IsNullOrEmpty(listadoAlumnosMatriculaDB) && !listadoAlumnosMatriculaDB.Contains("[]"))
                {
                    listadoAlumnosMatricula = JsonConvert.DeserializeObject<List<AlumnoMatriculaDTO>>(listadoAlumnosMatriculaDB);
                }
                return listadoAlumnosMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el listado de alumnos matricula 
        /// </summary>
        /// <returns></returns>
        public List<AlumnoMatriculaDTO> ObtenerListadoAlumnosMatricula()
        {
            try
            {
                List<AlumnoMatriculaDTO> listadoAlumnosMatricula = new List<AlumnoMatriculaDTO>();
                var _query = "SELECT CodigoAlumno, CodigoProgramaEspecifico, NombreProgramaEspecifico, CodigoMatricula, NombreCompletoAlumno,FechaMatricula,Estado FROM fin.V_ObtenerListadoAlumnosMatricula";
                var listadoAlumnosMatriculaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(listadoAlumnosMatriculaDB) && !listadoAlumnosMatriculaDB.Contains("[]"))
                {
                    listadoAlumnosMatricula = JsonConvert.DeserializeObject<List<AlumnoMatriculaDTO>>(listadoAlumnosMatriculaDB);
                }
                return listadoAlumnosMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de una matricula por codigoMatricual y version
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <param name="version"></param>
        public List<DatosMatriculaDTO> ObtenerDatosMatriculaPorCodigoMatriculaVersion(string codigoMatricula, int? version)
        {
            try
            {
                List<DatosMatriculaDTO> datosMatricula = new List<DatosMatriculaDTO>();
                var _query = "SELECT CodigoMatricula Id, IdPEspecifico, Moneda, TipoCambio, Max(TotalAPagar) AS TotalAPagar, Max(NroCuotas) AS NroCuotas, EstadoMatricula, Lower(Periodo) AS Periodo, Programa, Coordinador, Asesor, Paquete, Titulo, Observaciones, EmpresaPaga, EmpresaNombre, IdCoordinador,IdAsesor FROM fin.V_ObtenerDatosMatricula WHERE CodigoMatricula = @codigoMatricula AND version = @version GROUP  BY id, IdPEspecifico, EstadoMatricula, CodigoMatricula, moneda, TipoCambio, Periodo, Programa, Coordinador, Asesor, Paquete, Titulo, Observaciones, EmpresaPaga, EmpresaNombre, IdCoordinador, IdAsesor";
                var datosMatriculaDB = _dapper.QueryDapper(_query, new { codigoMatricula, version });
                if (!string.IsNullOrEmpty(datosMatriculaDB) && !datosMatriculaDB.Contains("[]"))
                {
                    datosMatricula = JsonConvert.DeserializeObject<List<DatosMatriculaDTO>>(datosMatriculaDB);
                }
                return datosMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el centro de costo por matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public CentroCostoNombreDTO ObtenerCentroCostoPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                CentroCostoNombreDTO nombresCentroCosto = new CentroCostoNombreDTO();
                var query = "SELECT NombreCentroCosto FROM fin.V_ObtenerCentroCostoPorMatricula WHERE IdMatriculaCabecera = @idMatriculaCabecera AND EstadoMatriculaCabecera = 1 AND EstadoPEspecifico = 1 AND EstadoCentroCosto = 1";
                var nombreCentroCostoDB = _dapper.FirstOrDefault(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(nombreCentroCostoDB) && !nombreCentroCostoDB.Contains("null"))
                {
                    nombresCentroCosto = JsonConvert.DeserializeObject<CentroCostoNombreDTO>(nombreCentroCostoDB);
                }
                return nombresCentroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtener las solicitudes de cambio de cronograma por personal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<SolicitudCambioDTO> ObtenerSolicitudesCambioCronograma(int idPersonal)
        {
            try
            {
                List<SolicitudCambioDTO> solicitudesCambios = new List<SolicitudCambioDTO>();
                var solicitudesCambiosDB = _dapper.QuerySPDapper("fin.ObtenerSolicitudesCambioCronogramaPorVersion", new { idPersonal });
                if (!string.IsNullOrEmpty(solicitudesCambiosDB) && !solicitudesCambiosDB.Contains("[]"))
                {
                    solicitudesCambios = JsonConvert.DeserializeObject<List<SolicitudCambioDTO>>(solicitudesCambiosDB);
                }
                return solicitudesCambios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las solicitudes de cambios de cronograma que hizo el personal en su ultima version
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<SolicitudPendienteDTO> ObtenerSolicitudesPendienteCambioCronograma(int idPersonal)
        {
            try
            {
                List<SolicitudPendienteDTO> solicitudesPendientesCambios = new List<SolicitudPendienteDTO>();
                var solicitudesPendientesCambiosDB = _dapper.QuerySPDapper("fin.SP_ObtenerSolicitudesPendientesCambioCronogramaPorVersion", new { idPersonal });
                if (!string.IsNullOrEmpty(solicitudesPendientesCambiosDB) && !solicitudesPendientesCambiosDB.Contains("[]"))
                {
                    solicitudesPendientesCambios = JsonConvert.DeserializeObject<List<SolicitudPendienteDTO>>(solicitudesPendientesCambiosDB);
                }
                return solicitudesPendientesCambios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene datos de la matricula manual por el codigo de matricula
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <returns></returns>
        public DatosMatriculaManualDTO ObtenerDatosMatriculaManual(string codigoMatricula)
        {
            try
            {
                DatosMatriculaManualDTO datosMatriculaManual = new DatosMatriculaManualDTO();
                //var query = "SELECT Id, CodigoMatricula, NombreCompletoAlumno, IdAlumno, Moneda, LEFT(convert(varchar,FechaIniPago,120),10) FechaIniPago, convert(decimal(10,2),TipoCambio) TipoCambio, convert(decimal(10,2), max(TotalPagar))TotalPagar, max(NroCuotas) NroCuotas, Periodo, NombreProgramaCentroCosto, IdPEspecifico, Coordinador, Asesor, TituloAcuerdoPago FROM fin.V_ObtenerDatosMatriculaManual WHERE CodigoMatricula = @codigoMatricula AND EstadoMatriculaCabecera = 1 AND EstadoCronogramaPago = 1 AND EstadoProgramaEspecifico =1 AND EstadoCentroCosto = 1 AND EstadoAlumno = 1 Group by Id, CodigoMatricula, NombreCompletoAlumno, IdAlumno, FechaIniPago, Periodo, Moneda, NombreProgramaCentroCosto, IdPEspecifico, NombreProgramaEspecifico, TipoCambio, Coordinador, Asesor, TituloAcuerdoPago";
                var query = "SELECT Id, CodigoMatricula, NombreCompletoAlumno, IdAlumno, Moneda, LEFT(convert(varchar,FechaIniPago,120),10) FechaIniPago, convert(decimal(10,2),TipoCambio) TipoCambio, convert(decimal(10,2), max(TotalPagar))TotalPagar, max(NroCuotas) NroCuotas, Periodo,  NombreProgramaCentroCosto AS NombrePrograma, IdPEspecifico, Coordinador, Asesor, TituloAcuerdoPago FROM fin.V_ObtenerDatosMatriculaManual WHERE CodigoMatricula = @codigoMatricula AND EstadoMatriculaCabecera = 1 AND EstadoProgramaEspecifico =1 AND EstadoCentroCosto = 1 AND EstadoAlumno = 1 Group by Id, CodigoMatricula, NombreCompletoAlumno, IdAlumno, FechaIniPago, Periodo, Moneda, NombreProgramaCentroCosto, IdPEspecifico, NombreProgramaEspecifico, TipoCambio, Coordinador, Asesor, TituloAcuerdoPago";
                var datosMatriculaManualDB = _dapper.FirstOrDefault(query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(datosMatriculaManualDB))
                {
                    datosMatriculaManual = JsonConvert.DeserializeObject<DatosMatriculaManualDTO>(datosMatriculaManualDB);
                }
                return datosMatriculaManual;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// retorna true o false dependiendo si existe un matricula para ese alumno con ese programa especifico
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        public bool ExisteMatriculaCabecera(int idAlumno, int idPEspecifico)
        {
            try
            {
                bool existe = false;
                var cantidad = GetBy(x => x.IdAlumno == idAlumno && x.IdPespecifico == idPEspecifico, x => new { x.Id }).Count();
                if (cantidad > 0)
                {
                    existe = true;
                }
                else
                {
                    existe = false;
                }
                return existe;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Metodo para eliminar matricula de forma fisica desde integra v3
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public bool EliminarMatricula_Sincronizacion(int idMatriculaCabecera)
        {
            try
            {
                var eliminarMatriculaSincronizacion = _dapper.QuerySPDapper("fin.SP_EliminarMatriculaSincronizacion", new { idMatriculaCabecera });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Permite obtener los Id del programa especifico, nombre completo del programa especifico enviandole como parametro 
		/// </summary>
		/// <param name="nombre"></param>
		/// <returns></returns>
		public List<DatosMatriculaCabeceraDTO> ObtenerCodigoMatriculaAutocompleto(string nombre)
        {
            try
            {
                List<DatosMatriculaCabeceraDTO> pEspecifico = new List<DatosMatriculaCabeceraDTO>();
                var _query = "SELECT Id, CodigoMatricula FROM fin.T_MatriculaCabecera WHERE  CodigoMatricula LIKE CONCAT('%',@nombre,'%') AND Estado = 1";
                var pEspecificoDB = _dapper.QueryDapper(_query, new { nombre });
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    pEspecifico = JsonConvert.DeserializeObject<List<DatosMatriculaCabeceraDTO>>(pEspecificoDB);
                }
                return pEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
		/// Obtiene el id y el codigomatricula de todas las matriculas 
		/// </summary>
		/// <param name="nombre"></param>
		/// <returns></returns>
		public List<DatosMatriculaCabeceraDTO> ObtenerCodigoMatriculaParaCombo()
        {
            try
            {
                List<DatosMatriculaCabeceraDTO> pEspecifico = new List<DatosMatriculaCabeceraDTO>();
                var _query = "SELECT Id, CodigoMatricula FROM fin.T_MatriculaCabecera WHERE  Estado = 1";
                var pEspecificoDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    pEspecifico = JsonConvert.DeserializeObject<List<DatosMatriculaCabeceraDTO>>(pEspecificoDB);
                }
                return pEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<EstadoMatriculadoDTO> ObtenerEstadoMatriculado(int IdAlumno)
        {
            try
            {
                List<EstadoMatriculadoDTO> estadoMatriculado = new List<EstadoMatriculadoDTO>();
                var _query = "com.SP_ObtenerEstadoMatriculado";
                var pEspecificoDB = _dapper.QuerySPDapper(_query, new { IdAlumno });
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<EstadoMatriculadoDTO>>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public EstadoMatriculadoDTO ObtenerEstadoEvaluacion(string CodigoMatricula)
        {
            try
            {
                EstadoMatriculadoDTO estadoMatriculado = new EstadoMatriculadoDTO();
                var _query = "com.SP_ObtenerEstadoEvaluacion";
                var pEspecificoDB = _dapper.QuerySPFirstOrDefault(_query, new { CodigoMatricula });
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("null"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<EstadoMatriculadoDTO>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CursoMoodleDTO> ObtenerCursoMoodle(string CodigoMatricula)
        {
            try
            {
                List<CursoMoodleDTO> cursoMoodle = new List<CursoMoodleDTO>();
                var _query = "Select CodigoMatricula,IdUsuario,IdCurso,NombreCurso, IdMatriculaMoodle From ope.V_ObtenerCursosPorMatricula Where CodigoMatricula = @CodigoMatricula";
                var _cursoMoodle = _dapper.QueryDapper(_query, new { CodigoMatricula });
                if (!string.IsNullOrEmpty(_cursoMoodle) && !_cursoMoodle.Contains("[]"))
                {
                    cursoMoodle = JsonConvert.DeserializeObject<List<CursoMoodleDTO>>(_cursoMoodle);
                }
                return cursoMoodle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CostosAdministrativosDTO> ObtenerCostosAdministrativos(int IdMatriculaCabecera)
        {
            try
            {
                List<CostosAdministrativosDTO> costos = new List<CostosAdministrativosDTO>();
                var _query = "Select Id,Concepto,IdMatriculaCabecera,Moneda,Monto,Gestionado,FechaCreacion,UrlDocumento,FechaEntregaEstimada,FechaEntregaReal,SolicitudCF,IdEstadoCertificadoFisico,IdCertificadoGeneradoAutomatico From pla.V_ObtenerCertificadoCronogramaPagoTarifario Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var _cursoMoodle = _dapper.QueryDapper(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(_cursoMoodle) && !_cursoMoodle.Contains("[]"))
                {
                    costos = JsonConvert.DeserializeObject<List<CostosAdministrativosDTO>>(_cursoMoodle);
                }
                return costos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CronogramaAutoEvaluacionDTO> ObtenerCrongramaAutoEvaluaciones(string CodigoMatricula)
        {
            try
            {
                List<CronogramaAutoEvaluacionDTO> cronogramaEvaluacion = new List<CronogramaAutoEvaluacionDTO>();
                var _query = "ope.SP_ObtenerAutoevaluacionesAlumno";
                var pEspecificoDB = _dapper.QuerySPDapper(_query, new { CodigoMatricula });
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    cronogramaEvaluacion = JsonConvert.DeserializeObject<List<CronogramaAutoEvaluacionDTO>>(pEspecificoDB);
                }
                else
                {
                    var _queryPresencial = "ope.SP_ObtenerReportedeNotas";
                    var pEspecificoDBPresencia = _dapper.QuerySPDapper(_queryPresencial, new { CodigoMatricula });
                    if (!string.IsNullOrEmpty(pEspecificoDBPresencia) && !pEspecificoDBPresencia.Contains("[]"))
                    {
                        cronogramaEvaluacion = JsonConvert.DeserializeObject<List<CronogramaAutoEvaluacionDTO>>(pEspecificoDBPresencia);
                    }
                }
                return cronogramaEvaluacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<FiltroDTO> ObtenerEstadoMatriculado()
        {
            try
            {
                List<FiltroDTO> estadoMatriculado = new List<FiltroDTO>();
                var _query = "Select Id,Nombre From fin.V_ObtenerEstadoMatriculado Where Estado=1";
                var pEspecificoDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<FiltroDTO>>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<FiltroDTO> ObtenerObservacionMatricula()
        {
            try
            {
                List<FiltroDTO> estadoMatriculado = new List<FiltroDTO>();
                var _query = "Select Id,Nombre From fin.T_MatriculaObservacion Where Estado=1";
                var pEspecificoDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<FiltroDTO>>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 01/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de Subestados de matricula para ser usados en filtros
        /// </summary>
        /// <param></param>
        /// <returns>Lista</returns>
        public List<SubEstadoMatriculaFiltroDTO> ObtenerSubEstadoMatricula()
        {
            try
            {
                List<SubEstadoMatriculaFiltroDTO> estadoMatriculado = new List<SubEstadoMatriculaFiltroDTO>();
                var query = "Select Id,Nombre,IdEstadoMatricula From fin.V_ObtenerSubEstadoMatricula Where Estado=1";
                var pEspecificoDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<SubEstadoMatriculaFiltroDTO>>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jose Villena
        /// Fecha: 01/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de Subestados de matricula para ser usados en filtros
        /// </summary>
        /// <param></param>
        /// <returns>Lista</returns>
        public List<SubEstadoMatriculaFiltroConfiguracionCoordinadoraDTO> ObtenerSubEstadoMatriculaConfiguracionCoordinadora()
        {
            try
            {
                List<SubEstadoMatriculaFiltroConfiguracionCoordinadoraDTO> estadoMatriculado = new List<SubEstadoMatriculaFiltroConfiguracionCoordinadoraDTO>();
                var query = "Select Id IdSubEstadoMatricula,Nombre SubEstadoMatricula,IdEstadoMatricula From fin.V_ObtenerSubEstadoMatricula Where Estado=1";
                var pEspecificoDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<SubEstadoMatriculaFiltroConfiguracionCoordinadoraDTO>>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ValorFiltroDTO> ObtenerCodigoPorIdOportunidad(int IdOportunidad)
        {
            try
            {
                List<ValorFiltroDTO> estadoMatriculado = new List<ValorFiltroDTO>();
                var _query = "Select Valor From fin.V_ObtenerCodigoPorIdOportunidad Where IdOportunidad=@IdOportunidad";
                var pEspecificoDB = _dapper.QueryDapper(_query, new { IdOportunidad });
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<ValorFiltroDTO>>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Actualiza El estado de La matricula Por IdSolicitud
        /// </summary>
        /// <param name="IdMatriculaCabecera"></param>
        /// <param name="EsInHouse"></param>
        /// <returns></returns>
        public List<MatriculaInHouseDTO> ObtenerListaInHouse()
        {
            try
            {
                List<MatriculaInHouseDTO> InHouseMatriculados = new List<MatriculaInHouseDTO>();
                var _query = "select CodigoMatricula, Cuota, Monto, FechaMatricula,FechaVencimiento from fin.V_ListaAlumnosInHouse ";
                var inHouseDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(inHouseDB) && !inHouseDB.Contains("[]"))
                {
                    InHouseMatriculados = JsonConvert.DeserializeObject<List<MatriculaInHouseDTO>>(inHouseDB);
                }
                return InHouseMatriculados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Actualiza El estado de La matricula Por IdSolicitud
        /// </summary>
        /// <param name="IdMatriculaCabecera"></param>
        /// <param name="EsInHouse"></param>
        /// <returns></returns>
        public bool ActualizarEstadoInHouseMatricula(int IdMatriculaCabecera, int EsInHouse)
        {
            try
            {

                var _query = "fin.SP_ActualizarEstadoInHouseMatricula";
                var inHouseDB = _dapper.QuerySPDapper(_query, new { IdMatriculaCabecera, EsInHouse});

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool ActualizarEstadoMatricula(int IdMatriculaCabecera, int IdEstadoMatricula, string CodigoMatricula)
        {
            try
            {

                var _query = "ope.SP_ActualizarEstadoMatricula";
                var pEspecificoDB = _dapper.QuerySPDapper(_query, new { IdMatriculaCabecera, IdEstadoMatricula, CodigoMatricula });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Actualiza El estado de La matricula Por IdSolicitud
        /// </summary>
        /// <param name="IdMatriculaCabecera"></param>
        /// <param name="IdEstadoMatricula"></param>
        /// <param name="CodigoMatricula"></param>
        /// <returns></returns>
        public bool ActualizarEstadoMatriculaPorSolicitud(int IdSolicitudOperaciones, string NombreEstado)
        {
            try
            {
                var _query = "ope.SP_ActualizarEstadoMatriculaPorSolicitud";
                var pEspecificoDB = _dapper.QuerySPDapper(_query, new { IdSolicitudOperaciones, NombreEstado });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Actualiza El estado de La matricula Por IdSolicitud
        /// </summary>
        /// <param name="IdMatriculaCabecera"></param>
        /// <param name="IdEstadoMatricula"></param>
        /// <param name="CodigoMatricula"></param>
        /// <returns></returns>
        public bool ActualizarSubEstadoMatriculaPorSolicitud(int IdSolicitudOperaciones, string NombreSubEstado)
        {
            try
            {
                var _query = "ope.SP_ActualizarSubEstadoMatriculaPorSolicitud";
                var pEspecificoDB = _dapper.QuerySPDapper(_query, new { IdSolicitudOperaciones, NombreSubEstado });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene detalle por matricula
        /// </summary>
        /// <param name="id">Id de la matricula cabecera que se desea averiguar(PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Objeto DetalleOportunidadOperacionesDTO para su consulta posterior</returns>
        public DetalleOportunidadOperacionesDTO ObtenerDetalleMatricula(int id)
        {
            try
            {
                var detalle = new DetalleOportunidadOperacionesDTO();

                var _query = $@"
                        SELECT Oportunidad.Id AS IdOportunidad, 
                               CentroCosto.Id AS IdCentroCosto, 
                               CentroCosto.Nombre AS NombreCentroCosto, 
                               PGeneral.Id AS IdProgramaGeneral,
                               PGeneral.Nombre AS NombreProgramaGeneral,
                               PEspecifico.Ciudad AS NombreCiudad,
                               Case When Pespecifico.TipoId=1 then 100 
                                    else Escala.EscalaCalificacion
                               End EscalaCalificacion
                        FROM ope.T_OportunidadClasificacionOperaciones AS OportunidadClasificacionOperaciones
                             INNER JOIN com.T_Oportunidad AS Oportunidad ON OportunidadClasificacionOperaciones.IdOportunidad = Oportunidad.Id
                             INNER JOIN pla.T_CentroCosto AS CentroCosto ON CentroCosto.Id = Oportunidad.IdCentroCosto
                             INNER JOIN pla.T_PEspecifico AS PEspecifico ON PEspecifico.IdCentroCosto = CentroCosto.id
                             INNER JOIN pla.T_PGeneral AS PGeneral ON PGeneral.Id = PEspecifico.IdProgramaGeneral
                             INNER JOIN pla.T_AreaCapacitacion AS AreaCapacitacion ON AreaCapacitacion.Id = PGeneral.IdArea
                             INNER JOIN pla.T_SubAreaCapacitacion AS SubAreaCapacitacion ON SubAreaCapacitacion.Id = PGeneral.IdSubArea
                             LEFT  JOIN pla.V_EscalaCalificacionPespecifico AS Escala ON Escala.CodigoCiudad = PEspecifico.Ciudad
                        WHERE IdMatriculaCabecera = @id;
                            ";

                var resultado = _dapper.FirstOrDefault(_query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    detalle = JsonConvert.DeserializeObject<DetalleOportunidadOperacionesDTO>(resultado);
                }

                return detalle;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtener detalle acceso aula virtual
        /// </summary>
        /// <param name="id">Id de la Matricula Cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Objeto del tipo DetalleAccesoAulaVirtualDTO</returns>
        public DetalleAccesoAulaVirtualDTO ObtenerDetalleAccesoAulaVirtual(int id)
        {
            try
            {
                var resultadoFinal = new DetalleAccesoAulaVirtualDTO();
                var query = "ope.SP_ObtenerDetalleAulaVirtual";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<DetalleAccesoAulaVirtualDTO>(resultado);
                }
                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtener detalle acceso aula virtual
        /// </summary>
        /// <param name="id">Id de la Matricula Cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Objeto del tipo DetalleCursoActualAulaVirtualDTO</returns>
        public List<DetalleCursoActualAulaVirtualDTO> ObtenerCursoActualAlumnoMoodle(int id)
        {
            try
            {
                List<DetalleCursoActualAulaVirtualDTO> resultadoFinal = new List<DetalleCursoActualAulaVirtualDTO>();
                string _queryCursoMoodle = "SELECT TOP 1 * from [ope].[V_ObtenerCursoActualAlumnoMoodle] WHERE IdMatriculaCabecera = @id";
                var resultado = _dapper.QueryDapper(_queryCursoMoodle, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<DetalleCursoActualAulaVirtualDTO>>(resultado);
                }
                else
                {
                    resultadoFinal = null;
                }
                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el cronograma de pago completo en html
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener el cronograma de pago completo (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="formatoHTMLMostrar">Enumerator de tipo FormatoHTMLMostrar, mostrando la lista y la tabla</param>
        /// <returns>Cadena formateada con el cronograma de pagos completo</returns>
        public string ObtenerCronogramaPagoCompleto(int id, FormatoHTMLMostrar formatoHTMLMostrar)
        {
            try
            {
                var resultadoFinal = new List<CuotaCronogramaDetalleDTO>();
                var query = $@"fin.SP_ObtenerCronogramaPagoCuotasCompleto";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultado);
                }

                //var _htmlFinal = "<ul>";

                //if (_resultado.Count() > 0 && _resultado != null)
                //{
                //    var totalCuotas = _resultado.Max(x => x.NroCuota);
                //    foreach (var item in _resultado)
                //    {
                //        _htmlFinal += $@"
                //                     <li>
                //                     <strong> Nro. cuota { item.NroCuota } de { totalCuotas } </strong>
                //                     <br/>
                //                     Fecha de vencimiento: { item.FechaVencimiento.ToString("dd/MM/yyyy") }
                //                     <br/>
                //                     Monto ({item.Moneda}): {item.Cuota}
                //                     </li>
                //     ";
                //    }
                //}
                //_htmlFinal += "</ul>";


                var htmlFinal = "";

                if (formatoHTMLMostrar == FormatoHTMLMostrar.Lista)
                {
                    if (resultadoFinal.Count() > 0 && resultadoFinal != null)
                    {
                        var totalCuotas = resultadoFinal.Max(x => x.NroCuota);
                        var ultimo = resultadoFinal.Last();
                        foreach (var item in resultadoFinal)
                        {
                            htmlFinal += $@"
                                     <span>
                                        Nro. cuota { item.NroCuota } de { totalCuotas }
                                        <br/>
                                        Fecha de vencimiento: { item.FechaVencimiento.ToString("dd/MM/yyyy") }
                                        <br/>
                                        Monto ({item.Moneda}): {item.Cuota}
                                     </span>";
                            if (!item.Equals(ultimo))
                            {
                                htmlFinal += $@"
                                            <br/>
                                            <br/>";
                            }
                        }
                    }
                }
                else if (formatoHTMLMostrar == FormatoHTMLMostrar.Tabla)
                {
                    if (resultadoFinal.Count() > 0 && resultadoFinal != null)
                    {

                        var totalCuotas = resultadoFinal.Max(x => x.NroCuota);
                        var ultimo = resultadoFinal.Last();

                        htmlFinal += $@"
                                    <table style='width: 390px;text-align: center;'>
                                        <tr>
                                            <th style='width:103px;height:28px;text-align: center;'> Nro. Cuota </th>
                                            <th style='width:140px;height:28px;text-align: center;'> Monto </th>
                                            <th style='width:193px;height:28px;text-align: center;'> Fecha de vencimiento </th>
                                        </tr>
                                        ";
                        foreach (var item in resultadoFinal)
                        {
                            htmlFinal += $@"
                                            <tr>
                                               <td style='width:103px;height:23px;text-align:center;'> { item.NroCuota } </td>
                                               <td style='width:140px;height:23px;text-align:center;'> { item.SimboloMoneda } { item.Cuota } </td>
                                               <td style='width:193px;height:23px;text-align:center;'> { item.FechaVencimiento.ToString("dd/MM/yyyy") } </td>
                                           </tr>
                                          ";
                        }
                        htmlFinal += $@"</table>";
                    }
                }
                return htmlFinal.Replace("dolares", "dólares");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el monto total del programa en el formato Simbolo +" " + MontoTotal +" "+ NombrePlural
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener el monto total (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con el monto y ell simbolo de la moneda</returns>
        public string ObtenerMontoTotal(int id)
        {
            try
            {
                var resultadoFinal = new ResumenCronogramaDTO();
                var query = $@"fin.SP_ObtenerMontoTotal";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ResumenCronogramaDTO>(resultado);
                }
                return string.Concat(resultadoFinal.SimboloMoneda, " ", resultadoFinal.MontoTotal, " ", resultadoFinal.NombrePluralMoneda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el cronograma de pago completo en html
        /// </summary>
        /// <param name="id">Id de la matricula cabecera a la cual se desea obtener el cronograma de pago completo de cuotas vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada de las cuotas de pago vencidas</returns>
        public string ObtenerCronogramaPagoCompletoCuotasVencidas(int id)
        {
            try
            {
                var resultadoFinal = new List<CuotaCronogramaDetalleDTO>();
                var query = $@"fin.SP_ObtenerCronogramaPagoCompletoCuotasVencidas";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultado);
                }

                var htmlFinal = string.Empty;

                if (resultadoFinal.Count() > 0 && resultadoFinal != null)
                {
                    var totalCuotas = resultadoFinal.Max(x => x.NroCuota);
                    var vencidos = resultadoFinal.Where(x => x.FechaVencimiento <= DateTime.Now);

                    if (!vencidos.Any())
                        return string.Empty;

                    var ultimo = vencidos.Last();
                    foreach (var item in vencidos)
                    {
                        htmlFinal += $@"
                                     <span>
                                        Nro. cuota { item.NroCuota } de { totalCuotas }
                                        <br/>
                                        Fecha de vencimiento: { item.FechaVencimiento.ToString("dd/MM/yyyy") }
                                        <br/>
                                        Monto ({item.Moneda}): {item.Cuota}
                                     </span>";
                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>";
                        }
                    }
                }
                return htmlFinal.Replace("dolares", "dólares");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el cronograma de pago de las cuotas que se vencen hoy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string ObtenerCronogramaPagoCuotasVencerHoy(int id, int version)
        {
            try
            {
                var cantidadMaximaCuotas = this.ObtenerMaximaCuota(id);

                var _resultado = new List<CuotaCronogramaDetalleDTO>();
                var query = $@"fin.SP_ObtenerCronogramaPagoCuotasVencerHoy";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id, Version = version });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultado);
                }

                //var _htmlFinal = "<ul>";
                //if (_resultado.Count() > 0 && _resultado != null)
                //{
                //    foreach (var item in _resultado)
                //    {
                //        _htmlFinal += $@"
                //                     <li>
                //                     <strong> Nro. cuota { item.NroCuota } de { cantidadMaximaCuotas } </strong>
                //                     <br/>
                //                     Fecha de vencimiento: { item.FechaVencimiento.ToString("dd/MM/yyyy") }
                //                     <br/>
                //                     Monto ({item.Moneda}): {item.Cuota}
                //                     </li>
                //                     <br/>
                //     ";
                //    }
                //}
                //_htmlFinal += "</ul>";
                //return _htmlFinal;
                var _htmlFinal = "";
                if (_resultado.Count() > 0 && _resultado != null)
                {
                    foreach (var item in _resultado)
                    {
                        var ultimo = _resultado.Last();
                        _htmlFinal += $@"
                                     <span>
                                     Nro. cuota { item.NroCuota } de { cantidadMaximaCuotas }
                                     <br/>
                                     Fecha de vencimiento: { item.FechaVencimiento.ToString("dd/MM/yyyy") }
                                     <br/>
                                     Monto ({item.Moneda}): {item.Cuota}
                                     </span>";
                        if (!item.Equals(ultimo))
                        {
                            _htmlFinal += $@"
                                            <br/>
                                            <br/>";
                        }
                    }
                }
                _htmlFinal += "";
                return _htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el cronograma de pago de las cuotas que se venceran en 3 dias
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string ObtenerCronogramaPagoCuotasVencerProximos3Dias(int id, int version)
        {
            try
            {

                var cantidadMaximaCuotas = this.ObtenerMaximaCuota(id);
                var _resultado = new List<CuotaCronogramaDetalleDTO>();
                var query = $@"fin.SP_ObtenerCronogramaPagoCuotasVencerProximos3Dias";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id, Version = version });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultado);
                }
                //var _htmlFinal = "<ul>";
                //if (_resultado.Count() > 0 && _resultado != null)
                //{
                //    foreach (var item in _resultado)
                //    {
                //        _htmlFinal += $@"
                //                     <li>
                //                     <strong> Nro. cuota { item.NroCuota } de { cantidadMaximaCuotas } </strong>
                //                     <br/>
                //                     Fecha de vencimiento: { item.FechaVencimiento.ToString("dd/MM/yyyy") }
                //                     <br/>
                //                     Monto ({item.Moneda}): {item.Cuota}
                //                     </li>
                //                     <br/>
                //     ";
                //    }
                //}
                //_htmlFinal += "</ul>";
                var _htmlFinal = "";
                if (_resultado.Count() > 0 && _resultado != null)
                {
                    var ultimo = _resultado.Last();
                    foreach (var item in _resultado)
                    {
                        _htmlFinal += $@"
                                     <span>
                                     Nro. cuota { item.NroCuota } de { cantidadMaximaCuotas }
                                     <br/>
                                     Fecha de vencimiento: { item.FechaVencimiento.ToString("dd/MM/yyyy") }
                                     <br/>
                                     Monto ({item.Moneda}): {item.Cuota}
                                     </span>";
                        if (!item.Equals(ultimo))
                        {
                            _htmlFinal += $@"
                                            <br/>
                                            <br/>";
                        }
                    }
                }
                _htmlFinal += "";
                return _htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el cronograma de pago completo en html
        /// </summary>
        /// <param name="id">Id de la matriculacabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con el cronograma de autoevaluacion completo</returns>
        public string ObtenerCronogramaAutoEvaluacionCompleto(int id)
        {
            try
            {
                var resultadoFinal = new List<AutoEvaluacionCronogramaDetalleDTO>();
                var query = $@"ope.SP_ObtenerAutoevaluacionesAlumnoMatriculado";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<AutoEvaluacionCronogramaDetalleDTO>>(resultado);
                }
                var htmlFinal = "";

                if (resultadoFinal.Count() > 0 && resultadoFinal != null)
                {
                    var ultimo = resultadoFinal.Last();

                    foreach (var item in resultadoFinal)
                    {
                        htmlFinal += $@"
                                     <span>
                                         { item.NombreAutoEvaluacion }
                                         <br/>
                                         Fecha Límite de Autoevaluación: { item.FechaCronograma.ToString("dd/MM/yyyy") }
                                     </span>";
                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>";
                        }
                    }
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el acceso del portal web
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DetalleAccesoPortalWebDTO ObtenerDetalleAccesoPortalWeb(int id)
        {
            try
            {
                var _resultado = new DetalleAccesoPortalWebDTO();
                var query = "ope.SP_ObtenerDetalleAccesoPortalWeb";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<DetalleAccesoPortalWebDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene el acceso del portal web
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DetalleAccesoPortalWebDTO ObtenerDetalleAccesoDocentePortalWeb(int idProveedor)
        {
            try
            {
                var _resultado = new DetalleAccesoPortalWebDTO();
                var query = "ope.SP_ObtenerDetalleAccesoDocentePortalWeb";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdProveedor = idProveedor });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<DetalleAccesoPortalWebDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el acceso del portal web V4
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DetalleAccesoPortalWebDTO ObtenerDetalleAccesoPortalWebV4(int id)
        {
            try
            {
                var _resultado = new DetalleAccesoPortalWebDTO();
                var query = "[ope].[SP_ObtenerDetalleAccesoPortalWebV4]";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<DetalleAccesoPortalWebDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Cantidad de autoevaluaciones pendientes
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Entero con la cantidad de autoevaluaciones pendientes por el alumno</returns>
        public int ObtenerCantidadAutoEvaluacionesPendientes(int id)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"ope.SP_ObtenerCantidadAutoevaluacionesPendientes";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

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
        /// Obtiene la cantidad de autoevaluaciones vencidas
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Entero con la cantidad de autoevaluaciones vencidas</returns>
        public int ObtenerCantidadAutoEvaluacionesVencidas(int id)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"ope.SP_ObtenerCantidadAutoevaluacionesVencidas";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

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
        /// Obtiene las AutoEvaluaciones vencidas en html
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las autoevaluaciones vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con las autoevaluaciones vencidas</returns>
        public string ObtenerAutoEvaluacionesVencidas(int id)
        {
            try
            {
                var resultadoFinal = new List<AutoEvaluacionCronogramaDetalleDTO>();
                var query = $@"ope.SP_ObtenerAutoevaluacionesVencidasAlumnoMatriculadoTotal";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<AutoEvaluacionCronogramaDetalleDTO>>(resultado);
                }

                var htmlFinal = "";

                if (resultadoFinal.Count() > 0 && resultadoFinal != null)
                {
                    var ultimo = resultadoFinal.Last();
                    foreach (var item in resultadoFinal)
                    {
                        htmlFinal += $@"
                                     <span>
                                         { item.NombreAutoEvaluacion }
                                         <br/>
                                         Fecha Límite de Autoevaluación: { item.FechaCronograma.ToString("dd/MM/yyyy") }
                                     </span>";
                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>";
                        }
                    }
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las AutoEvaluaciones completas en html
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las autoevaluaciones completas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con las autoevaluaciones completas</returns>
        public string ObtenerAutoEvaluacionesCompletas(int id)
        {
            try
            {
                var resultadoFinal = new List<AutoEvaluacionCompletaCronogramaDetalleDTO>();
                var query = $@"ope.SP_ObtenerAutoevaluacionesCompletasAlumnoMatriculado";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<AutoEvaluacionCompletaCronogramaDetalleDTO>>(resultado);
                }

                var htmlFinal = "";

                if (resultadoFinal.Count() > 0 && resultadoFinal != null)
                {
                    var ultimo = resultadoFinal.Last();
                    foreach (var item in resultadoFinal)
                    {
                        htmlFinal += $@"
                                     <span>
                                      { item.NombreAutoEvaluacion }
                                     <br/>
                                      Nota Obtenida: { item.Nota }
                                     <br/>
                                     Fecha de Evaluación: { item.FechaCronograma.ToString("dd/MM/yyyy") }
                                     </span>";
                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>";
                        }
                    }
                }

                htmlFinal += "";

                return htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las AutoEvaluaciones vencidas en una cantidad de dias exacta
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las autoevaluaciones vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el dia actual</param>
        /// <param name="esFechaExacta">Flag para determinar si la fecha ingresada es exacta</param>
        /// <param name="idPlantillaBase">Id de la plantilla base de la cual se buscara la informacion</param>
        /// <returns>Cadena con las autoevaluaciones vencidas</returns>
        public string ObtenerAutoEvaluacionesVencidas(int id, int cantidadDias, bool esFechaExacta, int idPlantillaBase)
        {
            try
            {
                var resultadoFinal = new ValorBoolDTO();
                var query = $@"ope.SP_CumpleCriterioAutoEvaluacionesVencidas";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorBoolDTO>(resultado);
                }
                var htmlFinal = "";
                if (resultadoFinal.Valor)
                {
                    var resultadoAutoEvaluacionesFinal = new List<AutoEvaluacionCronogramaDetalleDTO>();
                    var queryAutoEvaluaciones = "";

                    if (esFechaExacta)
                    {
                        queryAutoEvaluaciones = $@"ope.SP_ObtenerAutoevaluacionesVencidasAlumnoMatriculadoFechaExacta";
                    }
                    else
                    {
                        queryAutoEvaluaciones = $@"ope.SP_ObtenerAutoevaluacionesVencidasAlumnoMatriculado";
                    }
                    var resultadoAutoEvaluaciones = _dapper.QuerySPDapper(queryAutoEvaluaciones, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                    if (!string.IsNullOrEmpty(resultadoAutoEvaluaciones))
                    {
                        resultadoAutoEvaluacionesFinal = JsonConvert.DeserializeObject<List<AutoEvaluacionCronogramaDetalleDTO>>(resultadoAutoEvaluaciones);
                    }

                    if (resultadoAutoEvaluacionesFinal.Count() > 0 && resultadoAutoEvaluacionesFinal != null)
                    {
                        var ultimo = resultadoAutoEvaluacionesFinal.Last();
                        foreach (var item in resultadoAutoEvaluacionesFinal)
                        {
                            if (idPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                            {
                                htmlFinal += $@"
                                     <span>
                                         { item.NombreAutoEvaluacion }
                                         <br/>
                                         Fecha Límite de Autoevaluación: { item.FechaCronograma.ToString("dd/MM/yyyy") }
                                     </span>";
                                if (!item.Equals(ultimo))
                                {
                                    htmlFinal += $@"
                                            <br/>
                                            <br/>";
                                }
                            }
                            else if (idPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                            {
                                htmlFinal += $@"{ item.NombreAutoEvaluacion } \nFecha Límite de Autoevaluación: { item.FechaCronograma.ToString("dd/MM/yyyy") }";
                                if (!item.Equals(ultimo))
                                {
                                    htmlFinal += $@"\n";
                                }
                            }
                        }
                    }
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las AutoEvaluaciones vencidas en una cantidad de dias exacta
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener el detalle de las autoevaluaciones vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el dia actual</param>
        /// <param name="esFechaExacta">Flag para determinar si es una fecha exacta lo ingresado</param>
        /// <returns>Lista de objetos (AutoEvaluacionCronogramaDetalleDTO)</returns>
        public List<AutoEvaluacionCronogramaDetalleDTO> ObtenerDetalleAutoEvaluacionesVencidas(int id, int cantidadDias, bool esFechaExacta)
        {
            try
            {
                var resultadoAutoEvaluacionesFinal = new List<AutoEvaluacionCronogramaDetalleDTO>();

                var resultadoBool = new ValorBoolDTO();
                var query = $@"ope.SP_CumpleCriterioAutoEvaluacionesVencidas";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoBool = JsonConvert.DeserializeObject<ValorBoolDTO>(resultado);
                }
                if (resultadoBool.Valor)
                {
                    var queryAutoEvaluaciones = "";

                    if (esFechaExacta)
                    {
                        queryAutoEvaluaciones = $@"ope.SP_ObtenerAutoevaluacionesVencidasAlumnoMatriculadoFechaExacta";
                    }
                    else
                    {
                        queryAutoEvaluaciones = $@"ope.SP_ObtenerAutoevaluacionesVencidasAlumnoMatriculado";
                    }
                    var resultadoAutoEvaluaciones = _dapper.QuerySPDapper(queryAutoEvaluaciones, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                    if (!string.IsNullOrEmpty(resultadoAutoEvaluaciones))
                    {
                        resultadoAutoEvaluacionesFinal = JsonConvert.DeserializeObject<List<AutoEvaluacionCronogramaDetalleDTO>>(resultadoAutoEvaluaciones);
                    }

                }
                return resultadoAutoEvaluacionesFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las siguiente sesion
        /// </summary>
        /// <param name="id">Id del pespecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="cantidadDias">Cantidad de dias a partir del dia de hoy</param>
        /// <returns>Entero que retorna la proxima sesion</returns>
        public int ObtenerProximaSesion(int id, int cantidadDias)
        {
            try
            {
                var resultadoFinal = new ProgramaEspecificoSesionDetalleDTO();
                var query = $@"ope.SP_ObtenerProximaSesionProgramaEspecifico";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdPEspecifico = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ProgramaEspecificoSesionDetalleDTO>(resultado);
                }

                return resultadoFinal.IdPEspecificoSesion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las siguiente sesion
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener la maxima cuota</param>
        /// <returns>Entero con el numero maximo de cuotas de la matricula cabecera ingresada</returns>
        public int ObtenerMaximaCuota(int id)
        {
            try
            {
                var resultadoFinal = new ValorIntDTO();
                var query = $@"fin.SP_ObtenerMaximoNroCuota";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }

                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Cantidad de cuotas pendientes
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener la cantidad de cuotas pendientes (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Entero con la cantidad de cuotas pendientes</returns>
        public int ObtenerCantidadCuotasPendientes(int id)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"ope.SP_ObtenerCantidadCuotasPendientes";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

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
        /// Obtiene las cuotas vencidas en N dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias desde el dia actual de la consulta</param>
        /// <param name="esFechaExacta">Flag para determinar si la fecha ingresada es exacta o es un intervalo</param>
        /// <param name="idPlantillaBase">Id de la plantilla base (PK de la tabla pla.T_PlantillaBase)</param>
        /// <returns>Cadena con las cuotas vencidas dependiendo de los parametros ingresados</returns>
        public string ObtenerCuotasVencidas(int id, int cantidadDias, bool esFechaExacta, int idPlantillaBase)
        {
            try
            {
                var resultadoFinal = new ValorBoolDTO();

                var query = "";
                if (esFechaExacta)
                {
                    query = $@"ope.SP_CumpleCriterioCuotasVencidas";
                }
                else
                {
                    query = $@"ope.SP_CumpleCriterioCuotasVencidasFechaNoExacta";
                }

                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorBoolDTO>(resultado);
                }

                var _htmlFinal = "";
                if (resultadoFinal.Valor)
                {
                    var cantidadMaximaCuotas = this.ObtenerMaximaCuota(id);

                    var _resultadoCuotas = new List<CuotaCronogramaDetalleDTO>();
                    var queryCuotas = "";
                    if (esFechaExacta)
                    {
                        queryCuotas = $@"fin.SP_ObtenerCronogramaPagoCuotasVencidasFechaExacta";
                    }
                    else
                    {
                        queryCuotas = $@"fin.SP_ObtenerCronogramaPagoCuotasVencidas";
                    }

                    var resultadoCuotas = _dapper.QuerySPDapper(queryCuotas, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                    if (!string.IsNullOrEmpty(resultadoCuotas))
                    {
                        _resultadoCuotas = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultadoCuotas);
                    }

                    if (_resultadoCuotas.Count() > 0 && _resultadoCuotas != null)
                    {
                        var ultimo = _resultadoCuotas.Last();
                        foreach (var item in _resultadoCuotas)
                        {
                            if (idPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                            {

                                if (cantidadDias == 0 || cantidadDias == 3)
                                {
                                    _htmlFinal += $@"
                                     <span>
                                     <strong>Nro. cuota: </strong>{ item.NroCuota } de { cantidadMaximaCuotas }
                                     <br/>
                                     <strong>Fecha de vencimiento: </strong>{ item.FechaVencimiento.ToString("dd/MM/yyyy") }
                                     <br/>
                                     <strong>Monto ({item.Moneda}): </strong>{item.Cuota}
                                     </span>";
                                }
                                else
                                {
                                    _htmlFinal += $@"
                                     <span>
                                     Nro. cuota: { item.NroCuota } de { cantidadMaximaCuotas }
                                     <br/>
                                     Fecha de vencimiento: { item.FechaVencimiento.ToString("dd/MM/yyyy") }
                                     <br/>
                                     Monto ({item.Moneda}): {item.Cuota}
                                     </span>";
                                }
                                if (!item.Equals(ultimo))
                                {
                                    _htmlFinal += $@"
                                            <br/>
                                            <br/>";
                                }
                            }
                            else if (idPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                            {
                                _htmlFinal += $@"Nro. cuota { item.NroCuota } de { cantidadMaximaCuotas }\nFecha de vencimiento: { item.FechaVencimiento.ToString("dd/MM/yyyy") }\nMonto ({item.Moneda}): {item.Cuota}\n";
                                if (!item.Equals(ultimo))
                                {
                                    _htmlFinal += $@"";
                                }

                            }

                        }
                    }

                }

                return _htmlFinal.Replace("dolares", "dólares");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la cantidad de cuotas vencidas en N dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener la cantidad de cuotas vencidas en N dias</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde la fecha actual</param>
        /// <param name="esFechaExacta">Flag para determinar si la fecha ingresada es Fecha exacta o un intervalo</param>
        /// <param name="idPlantillaBase">Id de la plantilla base (PK de la tabla pla.T_PlantillaBase)</param>
        /// <returns>Entero con la cantidad de cuotas vencidas</returns>
        public int ObtenerCantidadCuotasVencidas(int id, int cantidadDias, bool esFechaExacta, int idPlantillaBase)
        {
            try
            {
                var resultadoFinal = new ValorBoolDTO();
                var cantidadCuotas = new ValorIntDTO();

                var query = "";
                if (esFechaExacta)
                {
                    query = $@"ope.SP_CumpleCriterioCuotasVencidas";
                }
                else
                {
                    query = $@"ope.SP_CumpleCriterioCuotasVencidasFechaNoExacta";
                }

                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorBoolDTO>(resultado);
                }

                if (resultadoFinal.Valor)
                {
                    var queryCuotas = "";
                    if (esFechaExacta)
                    {
                        queryCuotas = $@"fin.SP_ObtenerCronogramaPagoCantidadCuotasVencidasFechaExacta";
                    }
                    else
                    {
                        queryCuotas = $@"fin.SP_ObtenerCronogramaPagoCantidadCuotasVencidas";
                    }
                    var queryCantidad = _dapper.QuerySPFirstOrDefault(queryCuotas, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });
                    if (!string.IsNullOrEmpty(queryCantidad))
                    {
                        cantidadCuotas = JsonConvert.DeserializeObject<ValorIntDTO>(queryCantidad);
                    }
                }
                return cantidadCuotas.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene las cuotas vencidas en N dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias desde la fecha actual</param>
        /// <param name="esFechaExacta">Flag para determinar si es fecha exacta o un intervalo lo ingresado</param>
        /// <returns>Retorna una lista de objetos de tipo CuotaCronogramaDetalleDTO</returns>
        public List<CuotaCronogramaDetalleDTO> ObtenerDetalleCuotasVencidas(int id, int cantidadDias, bool esFechaExacta)
        {
            try
            {
                var resultadoCuotasFinal = new List<CuotaCronogramaDetalleDTO>();

                var resultadoFinal = new ValorBoolDTO();
                var query = $@"ope.SP_CumpleCriterioCuotasVencidas";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorBoolDTO>(resultado);
                }

                if (resultadoFinal.Valor)
                {
                    var queryCuotas = "";
                    if (esFechaExacta)
                    {
                        queryCuotas = $@"fin.SP_ObtenerCronogramaPagoCuotasVencidasFechaExacta";
                    }
                    else
                    {
                        queryCuotas = $@"fin.SP_ObtenerCronogramaPagoCuotasVencidas";
                    }

                    var resultadoCuotas = _dapper.QuerySPDapper(queryCuotas, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                    if (!string.IsNullOrEmpty(resultadoCuotas))
                    {
                        resultadoCuotasFinal = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultadoCuotas);
                    }
                }
                return resultadoCuotasFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las proxima fecha de autoevaluacion
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las autoevaluaciones vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el dia actual</param>
        /// <returns>Retorna objeto del tipo AutoEvaluacionCronogramaDetalleDTO</returns>
        public AutoEvaluacionCronogramaDetalleDTO ObtenerDetalleProximaAutoEvaluacion(int id)
        {
            try
            {
                var proximaCuota = new AutoEvaluacionCronogramaDetalleDTO();
                var query = $@"ope.SP_ObtenerProximaAutoEvaluacion";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    proximaCuota = JsonConvert.DeserializeObject<AutoEvaluacionCronogramaDetalleDTO>(resultado);
                }
                return proximaCuota;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las proxima cuota
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabeceras)</param>
        /// <returns>Objeto del tipo CuotaCronogramaDetalleDTO</returns>
        public CuotaCronogramaDetalleDTO ObtenerDetalleProximaCuota(int id)
        {
            try
            {
                var proximaCuota = new CuotaCronogramaDetalleDTO();
                var query = $@"ope.SP_ObtenerProximaCuota";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    proximaCuota = JsonConvert.DeserializeObject<CuotaCronogramaDetalleDTO>(resultado);
                }
                return proximaCuota;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la cantidad de cuotas vencidas
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Entero con la cantidad de cuotas vencidas</returns>
        public int ObtenerCantidadCuotasVencidas(int id)
        {
            try
            {
                var resultadoFinal = new ValorIntDTO();
                var query = $@"ope.SP_ObtenerCantidadCuotasVencidas";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Indica si la matricula cumple los criterios
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cantidadDias"></param>
        /// <returns></returns>
        public bool CumpleCriteriosAutoEvaluacionVencida(int id, int cantidadDias)
        {
            try
            {

                var _resultado = new ValorBoolDTO();
                var query = $@"ope.SP_CumpleCriterioAutoEvaluacionesVencidas";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorBoolDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public CambioCentroCostoDTO ObtenerRegistrosParaActualizar(int IdSolicitudOperaciones)
        {
            try
            {

                var _resultado = new CambioCentroCostoDTO();
                var query = $@"ope.SP_ObtenerRegistrosParaActualizar";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdSolicitudOperaciones });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<CambioCentroCostoDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public CambioCentroCostoDTO ObtenerRegistrosParaActualizarVersion(int IdSolicitudOperaciones)
        {
            try
            {

                var _resultado = new CambioCentroCostoDTO();
                var query = $@"ope.SP_ObtenerRegistrosParaActualizarVersion";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdSolicitudOperaciones });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<CambioCentroCostoDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ActualizarCentroCosto(CambioCentroCostoDTO SolicitudOperaciones)
        {
            try
            {

                var _resultado = new ValorBoolDTO();
                var query = $@"ope.SP_ActualizarCentroCosto";
                var resultado = _dapper.QuerySPFirstOrDefault(query,
                    new
                    {
                        SolicitudOperaciones.IdOportunidadV4,
                        SolicitudOperaciones.IdOportunidadV3,
                        SolicitudOperaciones.IdOportunidadPadreV3,
                        SolicitudOperaciones.IdOportunidadPadreV4,
                        SolicitudOperaciones.IdMatriculaCabeceraV3,
                        SolicitudOperaciones.IdMatriculaCabeceraV4,
                        SolicitudOperaciones.IdCronogramaPagoV3,
                        SolicitudOperaciones.IdCronogramaPagoV4,
                        SolicitudOperaciones.IdCentroCostoV3,
                        SolicitudOperaciones.IdCentroCostoV4,
                        SolicitudOperaciones.IdPespecificoV3,
                        SolicitudOperaciones.IdPespecificoV4
                    });

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Indica si cumple criterio de cuota vencidas
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cantidadDias"></param>
        /// <returns></returns>
        public bool CumpleCriteriosCuotaVencida(int id, int cantidadDias)
        {
            try
            {

                var _resultado = new ValorBoolDTO();
                var query = $@"ope.SP_CumpleCriterioCuotasVencidas";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorBoolDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la url del acceso sesion webinar en base a la cantidad de dias
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cantidadDias"></param>
        /// <returns></returns>
        public string ObtenerUrlAccesoSesionWebinarNDias(int id, int cantidadDias)
        {
            try
            {
                var _resultado = new ValorStringDTO();
                var query = $@"ope.SP_ObtenerUrlAccesoWebinarNDias";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

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
        /// Obtiene la sesiones webinar de un dia especifico
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias"></param>
        /// <returns>Obtiene en cadena las sesiones webinar de un dia especifico</returns>
        public string ObtenerSesionesWebinarNDias(int id, int cantidadDias, bool mostrarUrlAcceso)
        {
            try
            {
                var listadoSesionWebinar = new List<SesionWebinarDTO>();
                var query = $@"ope.SP_ObtenerSesionesWebinarNDias";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listadoSesionWebinar = JsonConvert.DeserializeObject<List<SesionWebinarDTO>>(resultado);
                }
                var _htmlFinal = "";

                if (listadoSesionWebinar.Count() > 0 && listadoSesionWebinar != null)
                {
                    //listadoSesionWebinar = listadoSesionWebinar.OrderBy(x => x.NombreAutoEvaluacion).ToList();
                    var ultimo = listadoSesionWebinar.Last();
                    foreach (var item in listadoSesionWebinar)
                    {
                        _htmlFinal += $@"
                                     <span>
                                         <strong>Fecha:</strong> { item.FechaInicio.ToString("dd/MM/yyyy") }
                                         <br/>
                                         <strong>Hora de inicio:</strong> { item.FechaInicio.ToString("hh:mm tt") }
                                         <br/>
                                         <strong>Hora de término:</strong> { item.FechaTermino.ToString("hh:mm tt") }
                                     </span>
                                     <br>
                                     <br>";
                        if (mostrarUrlAcceso)
                        {
                            _htmlFinal += $@"<span> Para conectarse al webinar programado presione el siguiente bot&oacute;n:
                                                <a href='{item.LinkWebinar}' target='_blank'>
                                                    <span>
                                                        conectarse al webinar
                                                    </span>
                                                </a> 
                                            </span>";
                        }

                        if (!item.Equals(ultimo))
                        {
                            _htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return _htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Obtiene la url del acceso sesion webinar en base a la cantidad de dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las sesiones (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias que se desea analizar desde el dia actual</param>
        /// <returns>Cadena con la sesion confirmada</returns>
        public string ObtenerSesionesWebinarConfirmadasNDias(int id, int cantidadDias, bool mostrarUrlAcceso)
        {
            try
            {
                var listadoSesionWebinar = new List<SesionWebinarDTO>();
                var query = $@"ope.SP_ObtenerSesionesWebinarConfirmadoNDias";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listadoSesionWebinar = JsonConvert.DeserializeObject<List<SesionWebinarDTO>>(resultado);
                }
                var htmlFinal = "";

                if (listadoSesionWebinar.Count() > 0 && listadoSesionWebinar != null)
                {
                    //listadoSesionWebinar = listadoSesionWebinar.OrderBy(x => x.NombreAutoEvaluacion).ToList();
                    var ultimo = listadoSesionWebinar.Last();
                    foreach (var item in listadoSesionWebinar)
                    {
                        htmlFinal += $@"
                                     <span>
                                         <strong>Fecha:</strong> { item.FechaInicio.ToString("dd/MM/yyyy") }
                                         <br/>
                                         <strong>Hora de inicio:</strong> { item.FechaInicio.ToString("hh:mm tt") }
                                         <br/>
                                         <strong>Hora de término:</strong> { item.FechaTermino.ToString("hh:mm tt") }
                                     </span>
                                     <br>
                                     <br>";
                        if (mostrarUrlAcceso)
                        {
                            htmlFinal += $@"<span> Para conectarse al webinar programado presione el siguiente bot&oacute;n:
                                                <a href='{item.LinkWebinar}' target='_blank'>
                                                    <span>
                                                        conectarse al webinar
                                                    </span>
                                                </a> 
                                            </span>";
                        }

                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la url del acceso sesion webinar en base a la cantidad de dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera que se desea analizar (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias que se desea obtener las sesiones webinar</param>
        /// <returns>Una lista de objetos (SesionWebinarDTO)</returns>
        public List<SesionWebinarDTO> ObtenerSesionesWebinarNDias(int id, int cantidadDias)
        {
            try
            {
                var listadoSesionWebinar = new List<SesionWebinarDTO>();
                var query = $@"ope.SP_ObtenerSesionesWebinarNDias";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listadoSesionWebinar = JsonConvert.DeserializeObject<List<SesionWebinarDTO>>(resultado);
                }
                return listadoSesionWebinar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la url del acceso sesion webinar confirmado en base a la cantidad de dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera que la cual se dese obtener las sesiones webinar confirmadas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias a partir del actual del que se desea obtener las sesiones webinar confirmadas</param>
        /// <returns>Lista de objetos (SesionWebinarDTO)</returns>
        public List<SesionWebinarDTO> ObtenerSesionesConfirmadasWebinarNDias(int id, int cantidadDias)
        {
            try
            {
                var listadoSesionWebinar = new List<SesionWebinarDTO>();
                var query = $@"ope.SP_ObtenerSesionesConfirmadasWebinarNDias";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listadoSesionWebinar = JsonConvert.DeserializeObject<List<SesionWebinarDTO>>(resultado);
                }
                return listadoSesionWebinar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la url de confirmacion de participacion webinar en base a dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias a analizar para obtener la URL</param>
        /// <returns>Cadena con la URL de confirmacion de los participantes de la sesion webinar</returns>
        public string ObtenerUrlConfirmacionParticipacionSesionWebinar(int id, int cantidadDias)
        {
            try
            {
                var _resultado = new ValorStringDTO();
                var query = $@"ope.SP_ObtenerUrlConfirmacionParticipacionSesionesWebinarNDias";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la url del acceso sesion webinar en base a la cantidad de dias
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cantidadDias"></param>
        /// <returns></returns>
        public string ObtenerMateriales(int id)
        {
            try
            {
                var listaMateriales = new List<MaterialDescargarDTO>();
                var query = $@"ope.SP_ObtenerMaterialesPorMatriculaCabecera";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listaMateriales = JsonConvert.DeserializeObject<List<MaterialDescargarDTO>>(resultado);
                }
                var _htmlFinal = "";

                if (listaMateriales.Count() > 0 && listaMateriales != null)
                {
                    //listadoSesionWebinar = listadoSesionWebinar.OrderBy(x => x.NombreAutoEvaluacion).ToList();
                    var ultimo = listaMateriales.Last();
                    foreach (var item in listaMateriales)
                    {
                        _htmlFinal += $@"
                                     <span>
                                         <a href='{item.UrlArchivo}'> {item.NombreArchivo} </a>
                                     </span>
                                     <br>
                                     <br>";

                        if (!item.Equals(ultimo))
                        {
                            _htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return _htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la url del acceso sesion webinar en base a la cantidad de dias
        /// </summary>
        /// <param name="idMaterialPEspecificoDetalle">Id del Material de un PEspecifico (PK de la tabla ope.T_MaterialPEspecificoDetalle)</param>
        /// <returns>Cadena formateada de los materiales de un PEspecifico</returns>
        public string ObtenerMaterialesPorMaterialPEspecificoDetalle(int idMaterialPEspecificoDetalle)
        {
            try
            {
                var listaMateriales = new List<MaterialDescargarDTO>();
                var query = $@"ope.SP_ObtenerMaterialesPorMaterialPEspecificoDetalle";
                var resultado = _dapper.QuerySPDapper(query, new { IdMaterialPEspecificoDetalle = idMaterialPEspecificoDetalle });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listaMateriales = JsonConvert.DeserializeObject<List<MaterialDescargarDTO>>(resultado);
                }
                var htmlFinal = "";

                if (listaMateriales.Count() > 0 && listaMateriales != null)
                {
                    //listadoSesionWebinar = listadoSesionWebinar.OrderBy(x => x.NombreAutoEvaluacion).ToList();
                    var ultimo = listaMateriales.Last();
                    foreach (var item in listaMateriales)
                    {
                        htmlFinal += $@"
                                     <span>
                                         <a href='{item.UrlArchivo}'> {item.NombreArchivo} </a>
                                     </span>
                                     <br>
                                     <br>";

                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Materiales de cada PEspecifico
        /// </summary>
        /// <param name="listaIdMaterialPEspecificoDetalle">Lista de entero de IdMaterialPEspecificoDetalle</param>
        /// <returns>Cadena formateada de los materiales</returns>
        public string ObtenerMaterialesPorMaterialPEspecificoDetalle(List<int> listaIdMaterialPEspecificoDetalle)
        {
            try
            {
                var listaMateriales = new List<MaterialDescargarDTO>();
                var query = $@"ope.SP_ObtenerListaMaterialesPorMaterialPEspecificoDetalle";
                var resultado = _dapper.QuerySPDapper(query, new { ListaMaterialPEspecificoDetalle = string.Join(",", listaIdMaterialPEspecificoDetalle.Select(x => x)) });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listaMateriales = JsonConvert.DeserializeObject<List<MaterialDescargarDTO>>(resultado);
                }
                var htmlFinal = "";

                if (listaMateriales.Count() > 0 && listaMateriales != null)
                {
                    //listadoSesionWebinar = listadoSesionWebinar.OrderBy(x => x.NombreAutoEvaluacion).ToList();
                    var ultimo = listaMateriales.Last();
                    foreach (var item in listaMateriales)
                    {
                        htmlFinal += $@"
                                     <span>
                                         <a href='{item.UrlArchivo}'> {item.NombreArchivo} </a>
                                     </span>
                                     <br>";

                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la presentacion de trabajo final en N Dias (No existe el SP en produccion)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cantidadDias"></param>
        /// <returns></returns>
        public string ObtenerPresentacionTrabajoFinalNDias(int id, int cantidadDias)
        {
            try
            {
                var listaTrabajoCurso = new List<TrabajoCursoAlumnoDTO>();
                var query = $@"ope.SP_ObtenerPresentacionTrabajoFinalNDias";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listaTrabajoCurso = JsonConvert.DeserializeObject<List<TrabajoCursoAlumnoDTO>>(resultado);
                }
                var _htmlFinal = "";

                if (listaTrabajoCurso.Count() > 0 && listaTrabajoCurso != null)
                {
                    var ultimo = listaTrabajoCurso.Last();
                    foreach (var item in listaTrabajoCurso)
                    {
                        _htmlFinal += $@"
                                     <span>
                                            Descripción: {item.DescripcionTrabajo}
                                            Forma de entrega: {item.NombreFormaEntrega}
                                            Fecha límite de entrega: {item.FechaEntrega}
                                     </span>
                                     <br>
                                     <br>";

                        if (!item.Equals(ultimo))
                        {
                            _htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return _htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la url de confirmacion de participacion webinar en base a dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias a analizar para obtener la URL</param>
        /// <returns>Cadena con la descripcion y fecha del trabajo a presentar</returns>
        public string ObtenerPresentacionTrabajoNDias(int id, int cantidadDias)
        {
            try
            {
                var listaTrabajoCurso = new List<TrabajoCursoAlumnoDTO>();
                var query = $@"ope.SP_ObtenerPresentacionTrabajoNDias";
                var resultado = _dapper.QuerySPDapper(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listaTrabajoCurso = JsonConvert.DeserializeObject<List<TrabajoCursoAlumnoDTO>>(resultado);
                }
                var _htmlFinal = "";

                if (listaTrabajoCurso.Count() > 0 && listaTrabajoCurso != null)
                {
                    var ultimo = listaTrabajoCurso.Last();
                    foreach (var item in listaTrabajoCurso)
                    {
                        _htmlFinal += $@"
                                     <span>
                                            Descripción: {item.DescripcionTrabajo}
                                            Forma de entrega: {item.NombreFormaEntrega}
                                            Fecha límite de entrega: {item.FechaEntrega}
                                     </span>
                                     <br>
                                     <br>";

                        if (!item.Equals(ultimo))
                        {
                            _htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return _htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la url de confirmacion de participacion webinar en base a dias
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cantidadDias"></param>
        /// <returns></returns>
        public DatosAlumnoMatriculaDTO ObtenerInformacionAlumnoPorCodigoMatricula(string CodigoMatricula)
        {
            try
            {
                DatosAlumnoMatriculaDTO _resultado = new DatosAlumnoMatriculaDTO();
                var query = "Select Id,CodigoMatricula,IdPespecifico,NombreAlumno,Genero,IdCentroCosto,NombreCentroCosto,IdEstadoMatricula,NombreEstadoMatricula " +
                            " From ope.V_ObtenerAlumnoPorMatricula where CodigoMatricula=@CodigoMatricula";
                var resultado = _dapper.FirstOrDefault(query, new { CodigoMatricula });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<DatosAlumnoMatriculaDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<DatosAlumnoMatriculaDTO> ObtenerInformacionAlumnoPorCentroCosto(int IdCentroCosto)
        {
            try
            {
                List<DatosAlumnoMatriculaDTO> _resultado = new List<DatosAlumnoMatriculaDTO>();
                var query = "Select Id,CodigoMatricula,IdPespecifico,NombreAlumno,Genero,IdCentroCosto,NombreCentroCosto,IdEstadoMatricula,NombreEstadoMatricula " +
                            " From ope.V_ObtenerAlumnoPorCentroCosto where IdCentroCosto=@IdCentroCosto and IdCertificadoDetalle is null";
                var resultado = _dapper.QueryDapper(query, new { IdCentroCosto });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    _resultado = JsonConvert.DeserializeObject<List<DatosAlumnoMatriculaDTO>>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DateTime? ObtenerFechaInicioAonline(string CodigoMatricula)
        {
            try
            {
                ValorDateTimeDTO _resultado = new ValorDateTimeDTO();
                var query = "Select FechaInicio AS Valor From ope.V_ObtenerFechaincioAlumnoAonline where CodigoMatricula=@CodigoMatricula";
                var resultado = _dapper.FirstOrDefault(query, new { CodigoMatricula });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<ValorDateTimeDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DateTime? ObtenerFechaFinAonline(string CodigoMatricula)
        {
            try
            {
                ValorDateTimeDTO _resultado = new ValorDateTimeDTO();
                var query = "Select FechaFin AS Valor From ope.V_ObtenerFechaFinAlumnoAonline where CodigoMatricula=@CodigoMatricula";
                var resultado = _dapper.FirstOrDefault(query, new { CodigoMatricula });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<ValorDateTimeDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DateTime? ObtenerFechaFinAonlineNoAplicaProyecto(string CodigoMatricula)
        {
            try
            {
                ValorDateTimeDTO _resultado = new ValorDateTimeDTO();
                var query = "Select FechaFin AS Valor From ope.V_ObtenerFechaFinAlumnoAonlineNoAplicaProyecto where CodigoMatricula=@CodigoMatricula";
                var resultado = _dapper.FirstOrDefault(query, new { CodigoMatricula });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<ValorDateTimeDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Actualiza usuario coordinador academico de matricula cabecera en v3
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool ActualizarTmatriculaCabecera(string codigoMatricula, string usuario)
        {
            try
            {
                var resultado = new Dictionary<string, bool>();

                string query = _dapper.QuerySPFirstOrDefault("fin.SP_ActualizarMatriculaCabeceraV3", new { CodigoMatricula = codigoMatricula, Usuario = usuario });
                if (!string.IsNullOrEmpty(query))
                {
                    resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
                }
                return resultado.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Obtiene el alumnos matriculados en un programa especifico
        /// </summary>
        /// <param name="idCabeceraMatricula"></param>
        /// <returns></returns>
        public List<ValorIntDTO> ObtenerAlumnosMatriculaProgramaEspecifico(int idPEspecifico)
        {
            try
            {
                var lista = new List<ValorIntDTO>();
                var _query = "ope.SP_ObtenerOportunidadesAlumnosMatriculadosProgramaEspecifico";
                var resultadoDB = _dapper.QuerySPDapper(_query, new { IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el alumnos matriculados en un programa especifico y grupo
        /// </summary>
        /// <param name="idCabeceraMatricula"></param>
        /// <returns></returns>
        public List<ValorIntDTO> ObtenerAlumnosMatriculaProgramaEspecificoGrupo(int idPEspecifico, int grupo)
        {
            try
            {
                var lista = new List<ValorIntDTO>();
                var _query = "ope.SP_ObtenerOportunidadesAlumnosMatriculadosProgramaEspecificoGrupo";
                var resultadoDB = _dapper.QuerySPDapper(_query, new { IdPEspecifico = idPEspecifico, Grupo = grupo });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Informacion de Certificado Generado
        /// </summary>
        /// <returns></returns>
        public CertificadoDatosMatriculaDTO ObtenerInformacionAlumnoCertificado(int idMatriculaCabecera, string codigoCertificado)
        {
            try
            {
                CertificadoDatosMatriculaDTO rpta = new CertificadoDatosMatriculaDTO();
                string _query = "Select Id,NombreAlumno,CodigoCertificado,NombrePrograma,FechaInicioCapacitacion,FechaFinCapacitacion,CodigoCertificado,CalificacionPromedio,EscalaCalificacion, " +
                                " Ciudad,CorrelativoConstancia,DuracionPespecifico,FechaEmisionCertificado from fin.V_ObtenerInformacionAlumnoPorIdMatricula Where Id=@IdMatriculaCabecera and CodigoCertificado=@CodigoCertificado";
                string query = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera = idMatriculaCabecera, codigoCertificado = codigoCertificado });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<CertificadoDatosMatriculaDTO>(query);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// Obtiene Informacion de Certificado Generado
        /// </summary>
        /// <returns></returns>
        public CertificadoDatosMatriculaDTO ObtenerInformacionAlumnoConstancia(int idMatriculaCabecera, string codigoCertificado)
        {
            try
            {
                CertificadoDatosMatriculaDTO rpta = new CertificadoDatosMatriculaDTO();
                string _query = "Select Id,NombreAlumno,CodigoCertificado,NombrePrograma,FechaInicioCapacitacion,FechaFinCapacitacion,CodigoCertificado,CalificacionPromedio,EscalaCalificacion, " +
                                " Ciudad,CorrelativoConstancia,DuracionPespecifico,FechaEmisionCertificado from fin.V_ObtenerInformacionAlumnoPorIdMatricula Where Id=@IdMatriculaCabecera and CorrelativoConstancia=@CodigoCertificado";
                string query = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera = idMatriculaCabecera, codigoCertificado = Convert.ToInt32(codigoCertificado) });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<CertificadoDatosMatriculaDTO>(query);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //Obtener datos del alumno filtrado por dni o codigo de matricula
        public List<FiltroMatriculaAlumnoDTO> ObtenerAlumnosMatriculados(string codMatricula, string DNI)
        {
            try
            {

                string condicion = string.Empty;
                if (!string.IsNullOrEmpty(codMatricula))
                {
                    condicion = condicion + " CodigoMatricula =@codMatricula ";
                }
                if (!string.IsNullOrEmpty(DNI))
                {
                    if (!string.IsNullOrEmpty(DNI) && !string.IsNullOrEmpty(codMatricula))
                    {
                        condicion = condicion + " and ";
                    }
                    condicion = condicion + " DNI = @DNI ";

                }


                List<FiltroMatriculaAlumnoDTO> lista = new List<FiltroMatriculaAlumnoDTO>();
                var _query = string.Empty;

                _query = "SELECT IdMatricula,CodigoMatricula, DNI, nombreAlumno, PersonalAsignado, CentroCosto, EstadoMatricula FROM mkt.V_Reclamo where " + condicion;
                var listaDB = _dapper.QueryDapper(_query, new { codMatricula = codMatricula, DNI = DNI });
                if (!string.IsNullOrEmpty(listaDB) && !listaDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<FiltroMatriculaAlumnoDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public List<FiltroIdCodigoMatriculaDTO> ObtenerCombosCodigoMatricula()
        {
            try
            {
                var data = new List<FiltroIdCodigoMatriculaDTO>();
                var _query = "SELECT IdCabeceraMatricula,CodigoMatricula FROM mkt.V_Reclamo where Estado = 1";
                var respuesta = _dapper.QueryDapper(_query, new { });
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data = JsonConvert.DeserializeObject<List<FiltroIdCodigoMatriculaDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoSubEstadoMatricuaDTO ObtenerEstadosPorPlantillas(int IdPlantillaBase, int IdPlantillaFrontal, int IdPlantillaPosterior, int IdEstadoMatricula, int IdSubEstadoMatricula)
        {
            try
            {
                EstadoSubEstadoMatricuaDTO rpta = new EstadoSubEstadoMatricuaDTO();
                string _query = "ope.SP_CalcularEstadosMatriculaPorCertificado";
                string query = _dapper.QuerySPFirstOrDefault(_query, new { IdPlantillaBase, IdPlantillaFrontal, IdPlantillaPosterior, IdEstadoMatricula, IdSubEstadoMatricula });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<EstadoSubEstadoMatricuaDTO>(query);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 30/01/2021
        /// Version:1.1
        /// <summary>
        /// Retorna los beneficios correspondientes a los alumnos.
        /// </summary>
        /// <param name="codigoMatricula">Codigo de la matricula del alumno</param>
        /// <returns>Lista de los beneficios del alumno: List<MatriculaCabeceraBeneficiosDTO></returns>
        public List<MatriculaCabeceraBeneficiosDTO> ObtenerBeneficiosCongeladosPorMatricula(string codigoMatricula)
        {
            try
            {
                List<MatriculaCabeceraBeneficiosDTO> beneficiosCodigoMatricula = new List<MatriculaCabeceraBeneficiosDTO>();
                var query = "SELECT Id,Titulo , EstadoMatriculaCabeceraBeneficio,FechaSolicitud,EstadoSolicitudBeneficio,FechaProgramada," +
                             "IdConfiguracionBeneficioProgramaGeneral,FechaEntregaBeneficio FROM com.V_MatriculaCabeceraBeneficios WHERE CodigoMatricula =  @codigoMatricula " +
                             "AND Estado=1 AND Titulo not like '%BSG%' AND Titulo not like '%horas%'";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<MatriculaCabeceraBeneficiosDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: MatriculaCabeceraRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtener Beneficio Solicitado Por Matricula
        /// </summary>
        /// <param name="codigomatricula">Codigo Matricula </param>
        /// <returns> Lista de beneficios solicitados: List<InformacionBeneficioSolicitadoDTO></returns> 
        public List<InformacionBeneficioSolicitadoDTO> ObtenerBeneficiosSolicitadosPorMatricula(string codigoMatricula)
        {
            try
            {
                List<InformacionBeneficioSolicitadoDTO> beneficiosCodigoMatricula = new List<InformacionBeneficioSolicitadoDTO>();
                var query = "SELECT Id,Beneficio,CentroCosto,Programa,FechaSolicitud,FechaProgramada,Coordinador,EstadoSolicitud,FechaEntregaBeneficio  FROM com.V_BeneficiosSolicitados WHERE CodigoMatricula =  @codigoMatricula";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<InformacionBeneficioSolicitadoDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BeneficioSolicitadoReporteDTO> ObtenerTodoBeneficioSolicitado()
        {
            try
            {
                List<BeneficioSolicitadoReporteDTO> beneficiosCodigoMatricula = new List<BeneficioSolicitadoReporteDTO>();
                var _query = "SELECT Id,Alumno,CodigoMatricula,Beneficio,CentroCosto,Programa,FechaSolicitud,Coordinador,EstadoSolicitud,FechaAprobacion,FechaProgramada,FechaEntrega,UsuarioAprobacion,UsuarioEntregoBeneficio  FROM com.V_BeneficiosSolicitadosReporte";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<BeneficioSolicitadoReporteDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<DatoAdicionalPWDTO> ObtenerDatosAdicionalesPorCodigo(string CodigoMatricula, int IdMatriculaCabeceraBeneficios)
        {
            try
            {
                List<DatoAdicionalPWDTO> beneficiosCodigoMatricula = new List<DatoAdicionalPWDTO>();
                var _query = "SELECT  IdMatriculaCabeceraBeneficios,CodigoMatricula,IdBeneficioDatoAdicional as Id,Contenido FROM com.V_ContenidoDatoAdicional where IdMatriculaCabeceraBeneficios = @IdMatriculaCabeceraBeneficios";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(_query, new { CodigoMatricula, IdMatriculaCabeceraBeneficios });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<DatoAdicionalPWDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BeneficioDatosAdicionalesDTO> ObtenerDatosAdicionalesPgeneralPorIdConfiguracion(int IdConfiguracionBeneficio)
        {
            try
            {
                List<BeneficioDatosAdicionalesDTO> beneficiosCodigoMatricula = new List<BeneficioDatosAdicionalesDTO>();
                var _query = "SELECT IdPgeneral,IdConfiguracionBeneficio,IdDatoAdicional FROM [com].[V_BeneficioDatosAdicionalesMatriculaCabecera] where IdConfiguracionBeneficio = @IdConfiguracionBeneficio";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(_query, new { IdConfiguracionBeneficio });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<BeneficioDatosAdicionalesDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int ActualizarEstadoMatriculaCabeceraBeneficio(int IdMatriculaCabeceraBeneficio)
        {
            try
            {

                var _query = "com.SP_ActualizarMatriculaCabeceraBeneficio";
                var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(_query, new { IdMatriculaCabeceraBeneficio });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int ActualizarFechaPlazoEntrega(int IdMatriculaCabeceraBeneficio)
        {
            try
            {

                var _query = "com.SP_ActualizarFechaSolicitud";
                var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(_query, new { IdMatriculaCabeceraBeneficio });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: MatriculaCabeceraRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Aprobar beneficio de la matricula cabecera beneficio
        /// </summary>
        /// <param name="IdMatriculaCabeceraBeneficio">Id Matricula Cabecera Beneficio </param>
        /// <returns> Retorna Resultado: int</returns>
        public int AprobarSolicitudBeneficio(int IdMatriculaCabeceraBeneficio)
        {
            try
            {

                var query = "com.SP_AprobarBeneficioMatriculaCabecera";
                var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(query, new { IdMatriculaCabeceraBeneficio });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int EntregarBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario)
        {
            try
            {

                var _query = "com.SP_EntregarBeneficio";
                var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(_query, new { IdMatriculaCabeceraBeneficio,Usuario });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string getCodigoMatricula(int idMatriculaCabecera)
        {
            CodigoMatriculaCabeceraDTO codigoMatricula = new CodigoMatriculaCabeceraDTO();
            var _query = "Select CodigoMatricula from [fin].[V_MatriculaCabeceraCodigoMatricula] where Id = @idMatriculaCabecera";
            var id = _dapper.FirstOrDefault(_query, new { idMatriculaCabecera });
            codigoMatricula = JsonConvert.DeserializeObject<CodigoMatriculaCabeceraDTO>(id);
            return codigoMatricula.CodigoMatricula;
        }
        /// <summary>
        ///  Obtiene Campo fechaFinalizacion de Matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public string ObtenerFechaFinalizacion(int idMatriculaCabecera)
        {
            try
            {
                ValorStringDTO rpta = new ValorStringDTO();
                var _query = "Select FechaFinalizacion as Valor From fin.T_MatriculaCabecera Where Id=@IdMatriculacabecera";
                var query = _dapper.FirstOrDefault(_query, new { IdMatriculacabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
                {
                    rpta = JsonConvert.DeserializeObject<ValorStringDTO>(query);
                }
                return rpta.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el descuento de cuotas pendientes acorde a un porcentaje dado
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="porcentaje">Porcentaje dado</param>
        /// <returns>Cadena formateada con el descuento de cuotas pendientes</returns>
        public string ObtenerDescuentoCuotasPendientesPorPorcentaje(int idMatriculaCabecera, decimal porcentaje)
        {
            try
            {
                var resultadoFinal = new MontoMonedaDTO();
                var query = $@"fin.SP_CalcularDescuentoCuotasPendientes";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera, PorcentajeDescuento = porcentaje });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<MontoMonedaDTO>(resultado);
                }
                return resultadoFinal.Cuota + " " + resultadoFinal.Moneda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Matriculas para pase a estado por Abandonar
        /// </summary>
        /// <returns></returns>
        public List<DatosMatriculaPorAbandonarDTO> MatriculasCulminadosSinCambiodeEstado()
        {
            try
            {
                var listaMatriculas = new List<DatosMatriculaPorAbandonarDTO>();
                var query = $@"fin.SP_ObtenerMatriculasCulminadasSinCambioDeEstado";
                var resultado = _dapper.QuerySPDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaMatriculas = JsonConvert.DeserializeObject<List<DatosMatriculaPorAbandonarDTO>>(resultado);
                }

                return listaMatriculas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// obtiene matriculas para pasarlos a abandonado
        /// </summary>
        /// <returns></returns>
        public List<DatosMatriculaPorAbandonarDTO> MatriculasParaPaseAAbandonado()
        {
            try
            {
                var listaMatriculas = new List<DatosMatriculaPorAbandonarDTO>();
                var query = $@"fin.SP_ObtenerMatriculasParaPaseAAbandonado";
                var resultado = _dapper.QuerySPDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaMatriculas = JsonConvert.DeserializeObject<List<DatosMatriculaPorAbandonarDTO>>(resultado);
                }

                return listaMatriculas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<DatosMatriculaPorAbandonarDTO> ObtenerMatriculasSinContactoMas10Dias()
        {
            try
            {
                var listaMatriculas = new List<DatosMatriculaPorAbandonarDTO>();
                var query = $@"fin.SP_ObtenerMatriculasSinContactoMas10Dias";
                var resultado = _dapper.QuerySPDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaMatriculas = JsonConvert.DeserializeObject<List<DatosMatriculaPorAbandonarDTO>>(resultado);
                }

                return listaMatriculas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        ///Repositorio: MatriculaCabeceraRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtener el programa especifico del Alumno 
        /// </summary>
        /// <param name="CodigoMatricula">Codigo Matricula </param>
        /// <returns>Retorna respuesta: ValorIntDTO</returns> 
        public int ObtenerAlumnoProgramaEspecifico(string CodigoMatricula)
        {

            try
            {
                ValorIntDTO pespecifico = new ValorIntDTO();
                var query = "Select IdPespecifico as Valor From fin.T_MatriculaCabecera Where CodigoMatricula=@CodigoMatricula";
                var queryDB = _dapper.FirstOrDefault(query, new { CodigoMatricula = CodigoMatricula });
                if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("null"))
                {
                    pespecifico = JsonConvert.DeserializeObject<ValorIntDTO>(queryDB);
                }
                return pespecifico.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: MatriculaCabeceraRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtener Programa General por Programa Especifico
        /// </summary>
        /// <param name="pespecifico">Id Programa Especifico </param>
        /// <returns> Retorna Respuesta: ValorIntDTO </returns> 
        public int ObtenerProgramaGeneral(int pespecifico)
        {

            try
            {
                ValorIntDTO pgeneral = new ValorIntDTO();
                var query = "Select IdProgramaGeneral as Valor From [pla].[T_PEspecifico] Where Id=@pespecifico";
                var queryDB = _dapper.FirstOrDefault(query, new { pespecifico });
                if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("null"))
                {
                    pgeneral = JsonConvert.DeserializeObject<ValorIntDTO>(queryDB);
                }
                return pgeneral.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: MatriculaCabeceraRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtener Estado Programa General del Beneficio
        /// </summary>
        /// <param name="pgeneral">Id Programa General </param>
        /// <returns> Lista Estado Matricula: List<EstadosMatriculaDTO></returns> 
        public List<EstadosMatriculaDTO> ObtenerEstadoPgeneralBeneficio(int pgeneral)
        {

            try
            {
                List<EstadosMatriculaDTO> estado = new List<EstadosMatriculaDTO>();
                var query = "Select IdEstadoMatricula as Id From [ope].[V_ObtenerEstadoConfiguracionBeneficio]  Where IdPGeneral=@pgeneral";
                var queryDB = _dapper.QueryDapper(query, new { pgeneral });
                if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("null"))
                {
                    estado = JsonConvert.DeserializeObject<List<EstadosMatriculaDTO>>(queryDB);
                }
                return estado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: MatriculaCabeceraRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtener Sub Estado del Programa General Beneficios
        /// </summary>
        /// <param name="IdPgeneral">Id Programa General </param>
        /// <returns> Lista Subestado Programa General Beneficio: List<EstadosMatriculaDTO></returns> 
        public List<EstadosMatriculaDTO> ObtenerSubEstadoPgeneralBeneficio(int pgeneral)
        {

            try
            {
                List<EstadosMatriculaDTO> estado = new List<EstadosMatriculaDTO>();
                var query = "Select IdSubEstadoMatricula as Id From [ope].[V_ObtenerSubEstadoConfiguracionBeneficio]  Where IdPGeneral=@pgeneral";
                var queryDB = _dapper.QueryDapper(query, new { pgeneral });
                if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("null"))
                {
                    estado = JsonConvert.DeserializeObject<List<EstadosMatriculaDTO>>(queryDB);
                }
                return estado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: MatriculaCabeceraRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtener Estado del Alumno
        /// </summary>
        /// <param name="codigomatricula">Codigo Matricula </param>
        /// <returns> Estado Alumno: ValorIntDTO</returns> 
        public int ObtenerEstadoAlumno(string codigomatricula)
        {

            try
            {
                ValorIntDTO estadoalumno = new ValorIntDTO();
                var query = "Select IdEstado_matricula as Valor From fin.T_MatriculaCabecera Where CodigoMatricula=@codigomatricula";
                var queryDB = _dapper.FirstOrDefault(query, new { codigomatricula });
                if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("null"))
                {
                    estadoalumno = JsonConvert.DeserializeObject<ValorIntDTO>(queryDB);
                }
                return estadoalumno.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: MatriculaCabeceraRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtener SubEstado del Alumno
        /// </summary>
        /// <param name="codigomatricula">Codigo Matricula </param>
        /// <returns> SubEstado Alumno: ValorIntDTO</returns> 
        public int ObtenerSubestadoAlumno(string codigomatricula)
        {

            try
            {
                ValorIntDTO estadoalumno = new ValorIntDTO();
                var query = "Select IdSubEstadoMatricula as Valor From fin.T_MatriculaCabecera Where CodigoMatricula=@codigomatricula";
                var queryDB = _dapper.FirstOrDefault(query, new { codigomatricula });
                if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("null"))
                {
                    estadoalumno = JsonConvert.DeserializeObject<ValorIntDTO>(queryDB);
                }
                return estadoalumno.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int PorAprobarSolicitudBeneficio(int IdMatriculaCabeceraBeneficio)
        {
            try
            {

                var _query = "com.SP_PorAprobarBeneficioMatriculaCabecera";
                var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(_query, new { IdMatriculaCabeceraBeneficio });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Perfil de Alumno Para Docente
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <param name="grupo"></param>
        /// <returns></returns>
        public List<PerfilAlumnoMatriculadoDTO> ListadoPerfilAlumno(int idPespecifico, int grupo)
        {
            try
            {
                var query = "select IdAlumno,IdMatriculaCabecera,Alumno,AreaFormacion,AreaTrabajo,Industria,Cargo,Empresa from ope.V_PerfilAlumnoMatriculasActivas where IdPespecifico = @idPespecifico AND GrupoCurso = @grupo";
                var res = _dapper.QueryDapper(query, new { idPespecifico = idPespecifico, grupo = grupo });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<PerfilAlumnoMatriculadoDTO>>(res);
                }

                List<PerfilAlumnoMatriculadoDTO> rpta = new List<PerfilAlumnoMatriculadoDTO>();
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Jose Villena
        /// Fecha: 22/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdMatriculaCabecera del alumno
        /// </summary>
        /// <param name="codigomatricula">Codigo Matricual</param>     
        /// <returns>IdMatriculaCabecera</returns>
        public int ObtenerMatriculaAlumno(string codigomatricula)
        {

            try
            {
                ValorIntDTO matriculaAlumno = new ValorIntDTO();
                var query = "Select Id as Valor From fin.T_MatriculaCabecera Where CodigoMatricula=@codigomatricula";
                var queryResultado = _dapper.FirstOrDefault(query, new { codigomatricula });
                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("null"))
                {
                    matriculaAlumno = JsonConvert.DeserializeObject<ValorIntDTO>(queryResultado);
                }
                return matriculaAlumno.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la informacion del programa relacionado a una matricula cabecera
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Retorna objeto de tipo (AgendaInformacionSolicitudAccesoTemporalDTO)</returns>
        public AgendaInformacionSolicitudAccesoTemporalDTO ObtenerInformacionProgramaPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                AgendaInformacionSolicitudAccesoTemporalDTO agendaInformacionResultado = new AgendaInformacionSolicitudAccesoTemporalDTO();
                var agendaInformacionConsulta = _dapper.QuerySPFirstOrDefault("com.SP_ObtenerInformacionCursoPorIdMatriculaCabecera", new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(agendaInformacionConsulta) && !agendaInformacionConsulta.Contains("[]"))
                {
                    agendaInformacionResultado = JsonConvert.DeserializeObject<AgendaInformacionSolicitudAccesoTemporalDTO>(agendaInformacionConsulta);
                }
                return agendaInformacionResultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Autor: Miguel Mora
        ///Fecha: 17/05/2021
        /// <summary>
        /// modifica el gestor de la tabla T_CronogramaPagoDetalleFinal
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="Usuario"> Usuario Responsable </param>
        /// <param name="Gestor"> Nuevo gestor</param>
        /// <param name="IdMatriculaCabecera"> Id de la matricula</param>
        public int ModificarGestorDeCobranza(string Usuario, string Gestor,int IdMatriculaCabecera)
        {
            try
            {
                var registroDB = _dapper.QuerySPFirstOrDefault("fin.SP_ActualizarGestorDeCobranza", new { Usuario, Gestor, IdMatriculaCabecera });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene el IdAlumno, UsuarioCoordinadorAcademico de todas una matricula
		/// </summary>
		/// <param name="nombre"></param>
		/// <returns></returns>
		public DatosAlumnoCoordinadorMatriculaCabeceraDTO ObtenerIdAlumnoCoordinadorAcademico(int IdMatriculaCabecera)
        {
            try
            {
                DatosAlumnoCoordinadorMatriculaCabeceraDTO matricula = new DatosAlumnoCoordinadorMatriculaCabeceraDTO();
                var _query = "SELECT IdAlumno, UsuarioCoordinadorAcademico FROM fin.T_MatriculaCabecera WHERE  Estado = 1 and Id=@IdMatriculaCabecera";
                var subQuery = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(subQuery) && !subQuery.Contains("[]"))
                {
                    matricula = JsonConvert.DeserializeObject<DatosAlumnoCoordinadorMatriculaCabeceraDTO>(subQuery);
                }
                return matricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene la informacion de la matriculaCabecera por IdCronograma
		/// </summary>
		/// <param name="nombre"></param>
		/// <returns></returns>
		public InformacionMatriculaCabeceraDTO ObtenerInformacionMatriculaCabeceraPorIdCronograma(int IdCronograma)
        {
            try
            {
                InformacionMatriculaCabeceraDTO matricula = new InformacionMatriculaCabeceraDTO();
                var _query = "SELECT Id,CodigoMatricula,IdPaquete FROM fin.T_MatriculaCabecera WHERE  Estado = 1 and IdCronograma=@IdCronograma";
                var subQuery = _dapper.FirstOrDefault(_query, new { IdCronograma });
                if (!string.IsNullOrEmpty(subQuery) && !subQuery.Contains("[]"))
                {
                    matricula = JsonConvert.DeserializeObject<InformacionMatriculaCabeceraDTO>(subQuery);
                }
                return matricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene la informacion de la matriculaCabecera por IdCronograma
		/// </summary>
		/// <param name="nombre"></param>
		/// <returns></returns>
		public List<InformacionMatriculaCabeceraDTO> ObtenerListaCursosCulminadosPorAlumno(int idAlumno)
        {
            try
            {
                List<InformacionMatriculaCabeceraDTO> matricula = new List<InformacionMatriculaCabeceraDTO>();
                var _query = "SELECT Id,CodigoMatricula,IdPaquete FROM fin.T_MatriculaCabecera WHERE  Estado = 1 and IdEstado_Matricula in (5,12) and IdAlumno=@idAlumno";
                var subQuery = _dapper.QueryDapper(_query, new { idAlumno });
                if (!string.IsNullOrEmpty(subQuery) && !subQuery.Contains("[]"))
                {
                    matricula = JsonConvert.DeserializeObject<List<InformacionMatriculaCabeceraDTO>>(subQuery);
                }
                return matricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Funcion que nos retorna una lista de correos
        /// </summary>
        /// <param name="Segmentos"></param>
        /// <returns></returns>
        public List<string> ConseguirCorreos(List<string> Segmentos)
        {
            try
            {
                List<string> mails = new List<string>();
                var Query = "SELECT email FROM conf.T_EnvioCorreoIncidencia WHERE UPPER(Segmento) LIKE '%" + Segmentos[0].ToUpper() + "%' ";
                if (Segmentos.Count > 2)
                {
                    var i = 1;
                    while (i < Segmentos.Count)
                    {
                        Query += "OR  UPPER(Segmento) LIKE '% " + Segmentos[i].ToUpper() + "%' ";
                        i++;
                    }
                }
                var registro = _dapper.QueryDapper(Query, new { });
                mails = JsonConvert.DeserializeObject<List<string>>(registro);
                return mails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Se obtiene una lista de matriculas validad apartir del 2019 que no existan en el congelamiento de  estructura
        /// </summary>
        /// <param name="cantidad">cantidad de registros consultados</param>
        /// <returns>List<MatriculaDTO></MatriculaDTO></returns>
        public List<MatriculaDTO> ObtenerListaEstructuraMatriculaNoCongelada(int cantidad)
        {
            try
            {
                List<MatriculaDTO> resultadoFinal = new List<MatriculaDTO>();
                var query = "SELECT TOP "+cantidad+ "* from [pla].[V_ListaEstructuraMatriculaNoCongelada]";
                var resultado = _dapper.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<MatriculaDTO>>(resultado);
                }
                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// <summary>
        /// Obtiene la informacion de la matriculaCabecera por IdMatriculaCabecera
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public InformacionMatriculaCabeceraDTO ObtenerInformacionMatriculaCabeceraPorIdMatriculaCabecera(int IdMatriculaCabecera)
        {
            try
            {
                InformacionMatriculaCabeceraDTO matricula = new InformacionMatriculaCabeceraDTO();
                var _query = "SELECT Id,CodigoMatricula,IdPaquete FROM fin.T_MatriculaCabecera WHERE  Estado = 1 and Id=@IdMatriculaCabecera";
                var subQuery = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(subQuery) && !subQuery.Contains("[]") && subQuery!="null")
                {
                    matricula = JsonConvert.DeserializeObject<InformacionMatriculaCabeceraDTO>(subQuery);
                }
                return matricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
