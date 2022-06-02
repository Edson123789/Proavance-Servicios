using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: AsignacionAutomaticaRepositorio
    /// Autor: --, Jashin Salazar
    /// Fecha: 14/05/2021
    /// <summary>
    /// Repositorio para consultas de Asignacion Automatica
    /// </summary>
    public class AsignacionAutomaticaRepositorio : BaseRepository<TAsignacionAutomatica, AsignacionAutomaticaBO>
    {
        #region Metodos Base
        public AsignacionAutomaticaRepositorio() : base()
        {
        }
        public AsignacionAutomaticaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsignacionAutomaticaBO> GetBy(Expression<Func<TAsignacionAutomatica, bool>> filter)
        {
            IEnumerable<TAsignacionAutomatica> listado = base.GetBy(filter);
            List<AsignacionAutomaticaBO> listadoBO = new List<AsignacionAutomaticaBO>();
            foreach (var itemEntidad in listado)
            {
                AsignacionAutomaticaBO objetoBO = Mapper.Map<TAsignacionAutomatica, AsignacionAutomaticaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsignacionAutomaticaBO FirstById(int id)
        {
            try
            {
                TAsignacionAutomatica entidad = base.FirstById(id);
                AsignacionAutomaticaBO objetoBO = new AsignacionAutomaticaBO();
                Mapper.Map<TAsignacionAutomatica, AsignacionAutomaticaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsignacionAutomaticaBO FirstBy(Expression<Func<TAsignacionAutomatica, bool>> filter)
        {
            try
            {
                TAsignacionAutomatica entidad = base.FirstBy(filter);
                AsignacionAutomaticaBO objetoBO = Mapper.Map<TAsignacionAutomatica, AsignacionAutomaticaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsignacionAutomaticaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsignacionAutomatica entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsignacionAutomaticaBO> listadoBO)
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

        public bool Update(AsignacionAutomaticaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsignacionAutomatica entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsignacionAutomaticaBO> listadoBO)
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
        private void AsignacionId(TAsignacionAutomatica entidad, AsignacionAutomaticaBO objetoBO)
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

        private TAsignacionAutomatica MapeoEntidad(AsignacionAutomaticaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsignacionAutomatica entidad = new TAsignacionAutomatica();
                entidad = Mapper.Map<AsignacionAutomaticaBO, TAsignacionAutomatica>(objetoBO,
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
        /// Obtiene los REgistros de AsignacionAutomatica Validados y Corregidos
        /// </summary>
        /// <param name="paginador"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public object ObtenerRegistrosImportados(Paginador paginador , FiltroAsignacionAutomaticaDTO filtro)
        {
            try
            {
                var filtros = new object();
                string _queryCondicion = "";
                string[] IdCategoriaOrigen = new string[6];
                string[] IdCentroCosto = new string[6];
                string[] IdProbabilidad = new string[6];
                string[] IdPais = new string[6];
                string[] IdIndustria = new string[6];
                string[] IdFormacion = new string[6];
                string[] IdCargo = new string[6];
                string[] IdTrabajo = new string[6];

                DateTime FechaFin =DateTime.Parse(filtro.FechaFin);
                DateTime fechaInicio =DateTime.Parse(filtro.FechaInicio);

                if (filtro.IdCentroCosto != "-1")
                {
                    _queryCondicion = _queryCondicion + "And IdCentroCosto in @IdCentroCosto ";
                    IdCentroCosto = filtro.IdCentroCosto.Split(",");
                }
                if (filtro.IdCategoriaDato != "-1")
                {
                    _queryCondicion = _queryCondicion + "And IdCategoria in @IdCategoriaOrigen ";
                    IdCategoriaOrigen = filtro.IdCategoriaDato.Split(",");
                }
                if (filtro.IdProbabilidad != "-1")
                {
                    _queryCondicion = _queryCondicion + "And IdProbabilidad in @IdProbabilidad ";
                    IdProbabilidad = filtro.IdProbabilidad.Split(",");
                }
                if (filtro.IdPaise != "-1")
                {
                    _queryCondicion = _queryCondicion + "And IdPais in @IdPais ";
                    IdPais = filtro.IdPaise.Split(",");
                }
                if (filtro.IdIndustria != "-1")
                {
                    _queryCondicion = _queryCondicion + "And IdIndustria in @IdIndustria ";
                    IdIndustria = filtro.IdIndustria.Split(",");
                }
                if (filtro.IdCargo != "-1")
                {
                    _queryCondicion = _queryCondicion + "And IdCargo in @IdCargo ";
                    IdCargo = filtro.IdCargo.Split(",");
                }
                if (filtro.IdAreaFormacion != "-1")
                {
                    _queryCondicion = _queryCondicion + "And IdAreaFormacion in @IdFormacion ";
                    IdFormacion = filtro.IdAreaFormacion.Split(",");
                }
                if (filtro.IdAreaTrabajo != "-1")
                {
                    _queryCondicion = _queryCondicion + "And IdAreaTrabajo in @IdTrabajo ";
                    IdTrabajo = filtro.IdAreaTrabajo.Split(",");
                }

                string _queryRegistro = "SELECT Alumno,Telefono,Celular,Email,CentroCosto,TipoDato,Origen,CodigoFase,AreaFormacion,AreaTrabajo,Industria,Cargo,NombrePais,NombreCiudad," +
                                        " origenCampania,FechaCreacion,ProbabilidadActual,NombreProbabilidadActual From com.V_TAsignacionAutomatica_RegistrosImportados" +
                                        " wHERE FechaCreacion Between @FechaInicio And @FechaFin And Corregido=1 and Validado=1 "+ _queryCondicion + "order by FechaCreacion desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";           

                string queryRegistro = _dapper.QueryDapper(_queryRegistro,new { fechaInicio, FechaFin,IdCentroCosto,IdProbabilidad,IdPais,IdCategoriaOrigen,IdIndustria,IdCargo,IdFormacion,IdTrabajo, Skip = paginador.skip, Take = paginador.take });
                var rpta = JsonConvert.DeserializeObject<List<AsignacionAutomaticaRegistroImportadoDTO>>(queryRegistro);

                string _queryCantidad = "SELECT Count(*) From com.V_TAsignacionAutomatica_RegistrosImportados wHERE FechaCreacion Between @FechaInicio And @FechaFin And Corregido=1 and Validado=1 " + _queryCondicion + "";
                string queryCantidad = _dapper.FirstOrDefault(_queryCantidad, new { fechaInicio, FechaFin, IdCentroCosto, IdProbabilidad, IdPais, IdCategoriaOrigen, IdIndustria, IdCargo, IdFormacion, IdTrabajo, Skip = paginador.skip, Take = paginador.take });
                var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(queryCantidad);

                foreach (var item in rpta)
                {
                    item.Email= EncriptarStringCorreo(item.Email);
                    item.Celular= EncriptarStringNumero(item.Email);
                }

                return new { data=rpta, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Encripta correos
        /// </summary>
        /// <param name="parametro"> Parametro </param>
        /// <returns> String </returns>
        public string EncriptarStringCorreo(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int posicion = parametro.IndexOf("@");

                if (posicion > 0)
                {
                    respuesta = new string('x', posicion) + parametro.Remove(0, posicion);
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Encripta numeros.
        /// </summary>
        /// <param name="parametro"> Parametro </param>
        /// <returns> String </returns>
        public string EncriptarStringNumero(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int longitud = parametro.Length;
                if (longitud > 4)
                {
                    int posicion = longitud - 4;
                    respuesta = parametro.Remove(posicion,4) + new string('x', 4);
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Obtiene todos los registros erroneos.
        /// </summary>
        /// <param name="paginador"> Paginador </param>
        /// <returns> Lista de objetos: List<AsignacionAutomaticaRegistroErroneoDTO> </returns>
        public List<AsignacionAutomaticaRegistroErroneoDTO> ObtenerTodoRegistrosErroneos(Paginador paginador)
        {
            try
            {
                string _queryRegistro = "SELECT Id,IdAlumno,ApellidoPaterno,ApellidoMaterno,nombre1,nombre2,Telefono,Celular,Email,CentroCosto,IdCentroCosto,NombrePrograma,TipoDato,IdTipoDato,Origen,IdOrigen,CodigoFase," +
                                        " IdFaseOportunidad,Formacion,IdAreaFormacion,Trabajo,IdAreaTrabajo,Industria,IdIndustria,Cargo,IdCargo,NombrePais,IdPais,NombreCiudad,IdCiudad,OrigenCampania,IdCampaniaScoring,IdCategoriaOrigen," +
                                        " IdAsignacionAutomaticaOrigen,FechaProgramada,FechaRegistroCampania,IdFaseOportunidadPortal,FechaCreacion,IdPersonal,IdCategoriaDato,IdTipoInteraccion,IdSubCategoriaDato,IdInteraccionFormulario From mkt.V_TAsignacionAutomatica_RegistroError Where IdAsignacionAutomaticaTipoError=1 and EStadoAsignacion=1 and EstadoError=1 and FechaRegistroCampania>='2018-22-09 00:00:00.00' " +
                                        "order by FechaCreacion desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                var queryRegistro = _dapper.QueryDapper(_queryRegistro,new { Skip=paginador.skip, Take=paginador.take });
                return JsonConvert.DeserializeObject<List<AsignacionAutomaticaRegistroErroneoDTO>>(queryRegistro);       
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la cantidad todos los registros erroneos.
        /// </summary>
        /// <returns> Int </returns>
        public int ObtenerTodoRegistroserroneosCount()
        {
            try
            {
                string _queryCantidad = "select count(*) From mkt.V_TAsignacionAutomatica_RegistroError Where IdAsignacionAutomaticaTipoError=1 and EStadoAsignacion=1 and EstadoError=1 and FechaRegistroCampania>='2018-22-09 00:00:00.00'";
                string queryCantidad = _dapper.FirstOrDefault(_queryCantidad, null);
                var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(queryCantidad);
                int Cantidad = CantidadRegistros.Select(w => w.Value).FirstOrDefault();
                return Cantidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
           

        }

        private object Ok(object p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene los registros duplicados.
        /// </summary>
        /// <returns> Lista de objetos: List<AsignacionAutomaticaRegistroDuplicadoDTO> </returns>
        public List<AsignacionAutomaticaRegistroDuplicadoDTO> ObtenerTodoRegistrosDuplicados()
        {
            try
            {
                string _queryRegistro = "Select Id,IdAlumno,ApellidoPaterno,ApellidoMaterno,nombre1,nombre2,Telefono,Celular,Email,CentroCosto,IdCentroCosto,NombrePrograma,TipoDato,IdTipoDato,Origen,IdOrigen,CodigoFase," +
                                        " IdFaseOportunidad,Formacion,IdAreaFormacion,Trabajo,IdAreaTrabajo,Industria,IdIndustria,Cargo,IdCargo,NombrePais,IdPais,NombreCiudad,IdCiudad,Validado,Corregido,OrigenCampania,IdCampaniaScoring,IdCategoriaOrigen," +
                                        " IdAsignacionAutomaticaOrigen,FechaRegistroCampania,IdFaseOportunidadPortal,FechaCreacion From mkt.V_TAsignacionAutomatica_RegistroDuplicado Where IdAsignacionAutomaticaTipoError=2 and EstadoError=1 and FechaRegistroCampania>='2018-22-09 00:00:00.00' order by FechaCreacion desc ";
                var queryRegistro = _dapper.QueryDapper(_queryRegistro,null);
                return JsonConvert.DeserializeObject<List<AsignacionAutomaticaRegistroDuplicadoDTO>>(queryRegistro);       
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta un nuevo modelo.
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <param name="valor"> Valor </param>
        /// <returns> Objeto: ResultadoFinalDTO </returns>
        public ResultadoFinalDTO InsertarNuevoModelo(int idOportunidad,decimal valor)
        {
            try
            {
                string _queryInsertar = "mkt.SP_ModeloPredictivoProbabilidad";
                var queryInsert = _dapper.QuerySPFirstOrDefault(_queryInsertar, new
                {
                    IdOportunidad = idOportunidad,
                    Valor = valor
                });
                return JsonConvert.DeserializeObject<ResultadoFinalDTO>(queryInsert);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros duplicados por el id de asignacion automatica.
        /// </summary>
        /// <param name="idAsignacionAutomatica"> Id de Asignacion automatica </param>
        /// <returns> Lista de objetos: List<AsignacionAutomaticaRegistroDuplicadoDTO> </returns>
        public List<AsignacionAutomaticaRegistroDuplicadoDTO> ObtenerTodoRegistrosDuplicadosPorIdAsignacionAutomatica(int idAsignacionAutomatica)
        {
            try
            {
                string _queryRegistro = "Select Id,IdAlumno,ApellidoPaterno,ApellidoMaterno,nombre1,nombre2,Telefono,Celular,Email,CentroCosto,IdCentroCosto,NombrePrograma,TipoDato,IdTipoDato,Origen,IdOrigen,CodigoFase," +
                                        " IdFaseOportunidad,Formacion,IdFormacion,Trabajo,IdTrabajo,Industria,IdIndustria,Cargo,IdCargo,NombrePais,IdPais,NombreCiudad,IdCiudad,Validado,Corregido,OrigenCampania,IdCampaniaScoring,IdCategoriaOrigen," +
                                        " IdAsignacionAutomaticaOrigen,FechaRegistroCampania,IdFaseOportunidadPortal,FechaCreacion From mkt.V_TAsignacionAutomatica_RegistroDuplicado Where IdAsignacionAutomaticaTipoError=2 and EstadoError=1 and Id=@IdAsignacionAutomatica and FechaRegistroCampania>='2018-22-09 00:00:00.00' order by FechaCreacion desc ";
                var queryRegistro = _dapper.QueryDapper(_queryRegistro, new { IdAsignacionAutomatica = idAsignacionAutomatica });
                return JsonConvert.DeserializeObject<List<AsignacionAutomaticaRegistroDuplicadoDTO>>(queryRegistro);       
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene los registro de landing page
        /// </summary>
        /// <param name="filtros"> Filtros </param>
        /// <returns> Lista de objetos: List<ReporteLandingPagePortalDTO> </returns>
        public List<ReporteLandingPagePortalDTO> ObtenerReporteLandingPagePortal(FiltroLandingPagePortalDTO filtros) {
            try
            {
                List<ReporteLandingPagePortalDTO> listadoRegistrosLandingPage = new List<ReporteLandingPagePortalDTO>();
                var _query = "";
                string registroLandingPageDB = "";
                if (!filtros.FechaInicial.HasValue && !filtros.FechaFinal.HasValue)
                {
                    _query = @"
                    SELECT top 1000 Id,IdAlumno, Nombres, Apellidos, Correo1, Fijo, Movil, Formacion, Trabajo, Cargo, Industria, Pais, Region, Ip, FechaCreacion, 
                    HoraCreacion, NombrePrograma, CentroCosto, IdCentroCosto, Origen, Campanha, Proveedor, Procesado, 
                    Formulario, IdCategoriaDato, IdCampania, CategoriaDato, EstadoOportunidad 
                    FROM mkt.V_ObtenerReporteLandingPagePortal ORDER BY CONVERT(DATE, FechaCreacion ) DESC ";
                    registroLandingPageDB = _dapper.QueryDapper(_query, new { });
                }
                else if (filtros.FechaInicial.HasValue && filtros.FechaFinal.HasValue)
                {
                    _query += @"
                    SELECT Id,IdAlumno, Nombres, Apellidos, Correo1, Fijo, Movil, Formacion, Trabajo, Cargo, Industria, Pais, Region, Ip, FechaCreacion, 
                    HoraCreacion, NombrePrograma, CentroCosto, IdCentroCosto, Origen, Campanha, Proveedor, Procesado, 
                    Formulario, IdCategoriaDato, IdCampania, CategoriaDato, EstadoOportunidad 
                    FROM mkt.V_ObtenerReporteLandingPagePortal WHERE Convert(Date, FechaCreacion) >= @fechaInicial and Convert(Date, FechaCreacion)  <= @fechaFinal ORDER BY CONVERT(DATE, FechaCreacion ) DESC ";
                    registroLandingPageDB = _dapper.QueryDapper(_query, new { fechaInicial = filtros.FechaInicial.Value.Date, fechaFinal = filtros.FechaFinal.Value.Date });
                }
                else  if (filtros.FechaInicial.HasValue && !filtros.FechaFinal.HasValue)
                {
                    filtros.FechaFinal = DateTime.Now;
                    registroLandingPageDB = _dapper.QueryDapper(_query, null);
                }
                if (!string.IsNullOrEmpty(registroLandingPageDB) && !registroLandingPageDB.Contains("[]"))
                {
                    listadoRegistrosLandingPage = JsonConvert.DeserializeObject<List<ReporteLandingPagePortalDTO>>(registroLandingPageDB);
                }

                foreach (var item in listadoRegistrosLandingPage)
                { 
                    item.Movil = EncriptarStringNumero(item.Movil);
                    item.Correo1 = EncriptarStringCorreo(item.Correo1);
                }
                return listadoRegistrosLandingPage;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un listado de registro del portal de facebook
        /// </summary>
        /// <param name="filtros"> Filtros </param>
        /// <returns> Lista de objetos: List<ReporteLandingPagePortalFacebookDTO> </returns>
        public List<ReporteLandingPagePortalFacebookDTO> ObtenerReporteLandingPagePortalFacebook(FiltroLandingPagePortalFacebookDTO filtros)
        {
            try
            {
                List<ReporteLandingPagePortalFacebookDTO> listadoRegistrosLandingPageFacebook = new List<ReporteLandingPagePortalFacebookDTO>();
                var _query = "";
                string registroLandingPageDB = "";
                if (!filtros.FechaInicial.HasValue && !filtros.FechaFinal.HasValue)
                {
                    _query = @"
                    SELECT TOP 1000 Id, IdAlumno, Nombres, Correo, Movil, AreaFormacion,   Cargo,  AreaTrabajo,  Industria,  Region,  FechaRegistro,  HoraRegistro,
                    NombrePrograma,  CentroCosto,  Categoria,  Campania,  Procesado,  Formulario,  EstadoOportunidad 
                    FROM mkt.V_ObtenerReporteLandingPageFacebook Order By Convert(Date, FechaRegistro ) Desc";
                    registroLandingPageDB = _dapper.QueryDapper(_query, new { });
                }
                else if (filtros.FechaInicial.HasValue && filtros.FechaFinal.HasValue)
                {
                    _query += @"
                    SELECT Id, IdAlumno, Nombres, Correo, Movil, AreaFormacion,   Cargo,  AreaTrabajo,  Industria,  Region,  FechaRegistro,  HoraRegistro,
                    NombrePrograma,  CentroCosto,  Categoria,  Campania,  Procesado,  Formulario,  EstadoOportunidad 
                    FROM mkt.V_ObtenerReporteLandingPageFacebook WHERE Convert(Date, FechaRegistro) >= @fechaInicial and Convert(Date, FechaRegistro)  <= @fechaFinal Order By Convert(Date, FechaRegistro ) Desc";
                    registroLandingPageDB = _dapper.QueryDapper(_query, new { fechaInicial = filtros.FechaInicial.Value.Date, fechaFinal = filtros.FechaFinal.Value.Date });
                }
                else if (filtros.FechaInicial.HasValue && !filtros.FechaFinal.HasValue)
                {
                    filtros.FechaFinal = DateTime.Now;
                    registroLandingPageDB = _dapper.QueryDapper(_query, null);
                }
                if (!string.IsNullOrEmpty(registroLandingPageDB) && !registroLandingPageDB.Contains("[]"))
                {
                    listadoRegistrosLandingPageFacebook = JsonConvert.DeserializeObject<List<ReporteLandingPagePortalFacebookDTO>>(registroLandingPageDB);
                }
                foreach (var item in listadoRegistrosLandingPageFacebook)
                {
                    item.Movil = EncriptarStringNumero(item.Movil);
                    item.Correo = EncriptarStringCorreo(item.Correo);
                }

                return listadoRegistrosLandingPageFacebook;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public string EncriptarString(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int LongNombres = (parametro.Length * 2) / 3;
                if (LongNombres > 0)
                {
                    respuesta = new string('x', LongNombres) + parametro.Remove(0, LongNombres);
                }
            }
            return respuesta;
        }

    }
}
