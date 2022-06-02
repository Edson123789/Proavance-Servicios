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
using System.IO;
using BSI.Integra.Aplicacion.DTOs.Reportes;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Planificacion/PEspecifico
    /// Autor: Fischer Valdez - Jose Villena - Esthephany Tanco - Carlos Crispin - Wilber Choque - Luis Huallpa - Alexsandra Flores - Gian Miranda - Edgar S.
    /// Fecha: 05/02/2021
    /// <summary>
    /// Repositorio para consultas de pla.T_PEspecifico
    /// </summary>
    public class PespecificoRepositorio : BaseRepository<TPespecifico, PespecificoBO>
    {
        #region Metodos Base
        public PespecificoRepositorio() : base()
        {
        }
        public PespecificoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PespecificoBO> GetBy(Expression<Func<TPespecifico, bool>> filter)
        {
            IEnumerable<TPespecifico> listado = base.GetBy(filter);
            List<PespecificoBO> listadoBO = new List<PespecificoBO>();
            foreach (var itemEntidad in listado)
            {
                PespecificoBO objetoBO = Mapper.Map<TPespecifico, PespecificoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PespecificoBO FirstById(int id)
        {
            try
            {
                TPespecifico entidad = base.FirstById(id);
                PespecificoBO objetoBO = new PespecificoBO();
                Mapper.Map<TPespecifico, PespecificoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PespecificoBO FirstBy(Expression<Func<TPespecifico, bool>> filter)
        {
            try
            {
                TPespecifico entidad = base.FirstBy(filter);
                PespecificoBO objetoBO = Mapper.Map<TPespecifico, PespecificoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PespecificoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPespecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PespecificoBO> listadoBO)
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

        public bool Update(PespecificoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPespecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PespecificoBO> listadoBO)
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
        private void AsignacionId(TPespecifico entidad, PespecificoBO objetoBO)
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

        private TPespecifico MapeoEntidad(PespecificoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPespecifico entidad = new TPespecifico();
                entidad = Mapper.Map<PespecificoBO, TPespecifico>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                if (objetoBO.CursoPespecifico != null)
                {
                    TCursoPespecifico entidadHijo = new TCursoPespecifico();
                    entidadHijo = Mapper.Map<CursoPespecificoBO, TCursoPespecifico>(objetoBO.CursoPespecifico,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TCursoPespecifico.Add(entidadHijo);
                }
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
        /// Obtiene Programas Especificos mediante Id Centro Costo
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns></returns>
        public PespecificoDTO ObtenerPespecificoPorCentroCosto(int idCentroCosto)
        {
            try
            {
                PespecificoDTO pEspecifico = new PespecificoDTO();
                string _queryPEspecifico = "Select Id,Nombre,EstadoP,Tipo,TipoAmbiente,Categoria,IdProgramaGeneral,Ciudad,EstadoPId,TipoId,IdCiudad,Duracion,CursoIndividual,IdSesion_Inicio,IdExpositor_Referencia AS IdExpositorReferencia,IdAmbiente,UrlDocumentoCronograma,IdMigracion, FechaHoraInicio, UrlDocumentoCronogramaGrupos "
                                + " From pla.V_TPEspecifico_ObtenerPEspecificos Where Estado = 1 and IdCentrocosto = @IdCentroCosto";
                var _programaEspecifico = _dapper.FirstOrDefault(_queryPEspecifico, new { IdCentroCosto = idCentroCosto });
                if (!string.IsNullOrEmpty(_programaEspecifico) && !_programaEspecifico.Equals("null"))
                {
                    pEspecifico = JsonConvert.DeserializeObject<PespecificoDTO>(_programaEspecifico);
                }
                return pEspecifico;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el TipoId de Programa Especifico mediante IdCentroCosto
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns></returns>
        public PespecificoIdTipoDTO ObtenerTipoId(int idCentroCosto)
        {
            try
            {
                string _queryPEspecifico = "Select TipoId from pla.V_TPEspecifico_IdTipo where IdCentroCosto = @IdCentroCosto and Estado = 1";
                var _programaEspecifico = _dapper.FirstOrDefault(_queryPEspecifico, new { IdCentroCosto = idCentroCosto });
                return JsonConvert.DeserializeObject<PespecificoIdTipoDTO>(_programaEspecifico);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Retorna templates con id de migracion para remplazar en las etiquetas de la agenda
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns></returns>
        public List<SeccionEtiquetaDTO> ObtenerSeccionEtiqueta(int idCentroCosto)
        {
            try
            {
                string _queryTemplates = "Select Valor,IdPlantillaPW,IdSeccionPW,IdCentroCosto from pla.V_SeccionesPlantillaTemplate Where EstadoPEspecifico = 1 AND EstadoPGeneral = 1 AND EstadoPGeneralDocumento = 1 AND EstadoDocumento = 1 AND EstadoDocumentoSeccion = 1 AND IdCentroCosto=@IdCentroCosto";
                var queryTemplates = _dapper.QueryDapper(_queryTemplates, new { IdCentroCosto = idCentroCosto });
                return JsonConvert.DeserializeObject<List<SeccionEtiquetaDTO>>(queryTemplates);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// Repositorio: PespecificoRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Retorna templates con id de migracion para remplazar en las etiquetas de la agenda
        /// </summary>
        /// <param name="IdPlantillaPW">Id de migracion de la Plantilla PW</param>
        /// <param name="IdSeccionPW">Id de migracion de la Seccion PW</param>
        /// <param name="idCentroCosto">Id del centro de costo (PK de la tabla pla.T_CentroCosto)</param>
        /// <returns> SeccionEtiquetaDTO </returns>
        public SeccionEtiquetaDTO ObtenerContenidoTemplate(Guid IdPlantillaPW, Guid IdSeccionPW, int idCentroCosto)
        {
            try
            {
                string queryTemplatesFinal = "Select Valor,IdPlantillaPW,IdSeccionPW,IdCentroCosto from pla.V_SeccionesPlantillaTemplate " +
                                         "Where EstadoPEspecifico = 1 AND EstadoPGeneral = 1 AND EstadoPGeneralDocumento = 1 AND EstadoDocumento = 1 AND EstadoDocumentoSeccion = 1 AND " +
                                         "IdCentroCosto=@IdCentroCosto AND IdPlantillaPW=@IdPlantillaPW AND IdSeccionPW = @IdSeccionPW";
                var queryTemplates = _dapper.FirstOrDefault(queryTemplatesFinal, new { IdCentroCosto = idCentroCosto, IdPlantillaPW, IdSeccionPW });
                return JsonConvert.DeserializeObject<SeccionEtiquetaDTO>(queryTemplates);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public SeccionEtiquetaDTO ObtenerSeccionEtiquetaAgendaMensaje(string idSeccion, string idPlantilla, int idCentroCosto)
        {
            try
            {
                string _queryTemplates = "Select Valor from pla.V_SeccionesPlantillaTemplate Where EstadoPEspecifico = 1 AND EstadoPGeneral = 1 AND EstadoPGeneralDocumento = 1 AND EstadoDocumento = 1 AND EstadoDocumentoSeccion = 1 AND IdCentroCosto=@IdCentroCosto and IdPlantillaPW=@IdPlantilla and IdSeccionPW=@IdSeccion";
                var queryTemplates = _dapper.FirstOrDefault(_queryTemplates, new { IdCentroCosto = idCentroCosto, IdPlantilla = idPlantilla, IdSeccion = idSeccion });
                return JsonConvert.DeserializeObject<SeccionEtiquetaDTO>(queryTemplates);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public PEspecificoInformacionDTO ObtenerPespecificoPorOportunidad(int idOportunidad)
        {
            try
            {
                string _queryTemplates = "Select Id,Nombre,Codigo,IdCentroCosto,Tipo,Categoria,CodigoBanco,Ciudad,IdProgramaGeneral,EstadoP from mkt.V_PespecificoOportunidad Where IdOportunidad = @idOportunidad";
                var queryTemplates = _dapper.FirstOrDefault(_queryTemplates, new { idOportunidad });
                return JsonConvert.DeserializeObject<PEspecificoInformacionDTO>(queryTemplates);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public PespecificoCentroCostoDTO ObtenerCentroCostoPresencial(string nombre, string ciudad)
        {
            try
            {
                string _query = "Select IdCentroCosto from pla.V_ObtenerCentroCosto where EstadoP = 'Lanzamiento' AND UPPER(NombreCorto) = @Nombre AND UPPER(Ciudad) = @Ciudad";
                var _queryRespuesta = _dapper.FirstOrDefault(_query, new { Nombre = nombre, Ciudad = ciudad });
                if (_queryRespuesta != "null")
                    return JsonConvert.DeserializeObject<PespecificoCentroCostoDTO>(_queryRespuesta);
                else
                    return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public PespecificoCentroCostoDTO ObtenerCentroCostoOnline(string nombre)
        {
            try
            {
                string _query = "Select IdCentroCosto from pla.V_ObtenerCentroCosto where EstadoP = 'Lanzamiento' AND UPPER(NombreCorto) = @Nombre AND Tipo like '%Online%'";
                var _queryRespuesta = _dapper.FirstOrDefault(_query, new { Nombre = nombre });
                if (_queryRespuesta != "null")
                    return JsonConvert.DeserializeObject<PespecificoCentroCostoDTO>(_queryRespuesta);
                else
                    return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<PespecificoAlumnosDTO> ObtenerAlumnosporIdPespecifico(int idPespecifico)
        {
            try
            {
                string _query = "Select IdAlumno,CodigoMatricula,Email,IdPais,NombrePais,ZonaHoraria from pla.t_AlumnoMatriculadosPespecifico where IdPespecifico=@idPespecifico";
                var _queryRespuesta = _dapper.QueryDapper(_query, new { IdPespecifico = idPespecifico });
                if (_queryRespuesta != "null")
                    return JsonConvert.DeserializeObject<List<PespecificoAlumnosDTO>>(_queryRespuesta);
                else
                    return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Se obtiene la lista de Alumnos de su sesion y/o Recuperacion de sesion por el idPespecificoSesion
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<PespecificoAlumnosDTO> ObtenerAlumnosporIdPespecificoSesion(int idPespecificoSesion,int Grupo)
        {
            try
            {
                List<PespecificoAlumnosDTO> alumnosPorPEspecificoSesion = new List<PespecificoAlumnosDTO>();
                
                var listadoAlumnosPorPEspecificoSesionDB = _dapper.QuerySPDapper("[pla.SP_ObtenerAlumnosPorIdPEspecificoSesion]", new { idPespecificoSesion = idPespecificoSesion,grupo= Grupo });
                if (!string.IsNullOrEmpty(listadoAlumnosPorPEspecificoSesionDB) && !listadoAlumnosPorPEspecificoSesionDB.Contains("[]"))
                {
                    alumnosPorPEspecificoSesion = JsonConvert.DeserializeObject<List<PespecificoAlumnosDTO>>(listadoAlumnosPorPEspecificoSesionDB);
                }
                return alumnosPorPEspecificoSesion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// Repositorio: Planificacion
        /// Autor: Jose Villena
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene la url sesion del webinar del alumno
        /// </summary>

        public SesionAlumnoWebinarDTO ObtenerSesionAlumnoProgramasWebinar(int IdMatriculaCabecera)
        {
            try
            {
                SesionAlumnoWebinarDTO alumnosSesionWebinar = new SesionAlumnoWebinarDTO();

                var alumnosSesionWebinarDB = _dapper.QuerySPFirstOrDefault("[pla].[SP_ObtenerSesionWebinarAlumnoPorIdMatriculaCabecera]", new { idMatriculaCabecera = IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(alumnosSesionWebinarDB) && !alumnosSesionWebinarDB.Contains("[]"))
                {
                    alumnosSesionWebinar = JsonConvert.DeserializeObject<SesionAlumnoWebinarDTO>(alumnosSesionWebinarDB);
                    var fechaActual = DateTime.Now;
                    if (alumnosSesionWebinar == null)
                    {
                        alumnosSesionWebinar = null;

                    }
                    else if (alumnosSesionWebinar.FechaHoraInicio.Date.Equals(fechaActual.Date))
                    {
                        alumnosSesionWebinar.EsHoy = 1;
                        alumnosSesionWebinar.EsAfuturo = 0;
                        alumnosSesionWebinar.NoHay = 0;
                    }
                    else if (alumnosSesionWebinar.FechaHoraInicio.Date > fechaActual.Date)
                    {
                        alumnosSesionWebinar.EsAfuturo = 1;
                        alumnosSesionWebinar.EsHoy = 0;
                        alumnosSesionWebinar.NoHay = 0;
                    }

                }

                return alumnosSesionWebinar;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PespecificoDocenteDTO> ObtenerDocentesporIdPespecificoSesion(int idPespecificoSesion)
        {
            try
            {
                List<PespecificoDocenteDTO> docentePorPEspecificoSesion = new List<PespecificoDocenteDTO>();

                var listadoDocentesPorPEspecificoSesionDB = _dapper.QuerySPDapper("[pla.SP_ObtenerDocentesPorIdPEspecificoSesion]", new { idPespecificoSesion = idPespecificoSesion });
                if (!string.IsNullOrEmpty(listadoDocentesPorPEspecificoSesionDB) && !listadoDocentesPorPEspecificoSesionDB.Contains("[]"))
                {
                    docentePorPEspecificoSesion = JsonConvert.DeserializeObject<List<PespecificoDocenteDTO>>(listadoDocentesPorPEspecificoSesionDB);
                }
                return docentePorPEspecificoSesion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PespecificoAlumnosConfirmadosDTO> ObtenerAlumnosporIdPespecificoSesionWebinar(int idPespecificoSesion)
        {
            PespecificoRepositorio _repProgramasRelacionados = new PespecificoRepositorio();
            OportunidadClasificacionOperacionesRepositorio _repOportunidadCalsificacion = new OportunidadClasificacionOperacionesRepositorio();
            List<PespecificoAlumnosConfirmadosDTO> alumnosPorPEspecificoSesionConfirmados = new List<PespecificoAlumnosConfirmadosDTO>();
            List<PespecificoAlumnosConfirmadosDTO> alumnosPorPEspecificoSesion = new List<PespecificoAlumnosConfirmadosDTO>();
            try
            {

                var listadoAlumnosPorPEspecificoSesionDB = _dapper.QuerySPDapper("[pla.SP_ObtenerAlumnosPorIdPEspecificoWebinarConfirmados]", new { idPespecificoSesion = idPespecificoSesion });
                if (!string.IsNullOrEmpty(listadoAlumnosPorPEspecificoSesionDB) && !listadoAlumnosPorPEspecificoSesionDB.Contains("[]"))
                {
                    alumnosPorPEspecificoSesion = JsonConvert.DeserializeObject<List<PespecificoAlumnosConfirmadosDTO>>(listadoAlumnosPorPEspecificoSesionDB);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            /*foreach(var alumno in alumnosPorPEspecificoSesion)
            {
                var configuracionProgramaRelacionados = _repProgramasRelacionados.ObtenerConfiguracionWebinar(alumno.IdWebinar);
                var estadoAlumno = _repOportunidadCalsificacion.ObtenerAvanceAlumno(alumno.IdMatriculaCabecera);
                if(estadoAlumno != null)
                {
                    bool condicion1 = false;
                    bool condicion2 = false;

                    if (configuracionProgramaRelacionados.IdOperadorComparacionAvance != null)
                    {                        
                        if (configuracionProgramaRelacionados.IdOperadorComparacionAvance == 2)//2Igual
                        {
                            if (estadoAlumno.ValorAvance == configuracionProgramaRelacionados.ValorAvance)
                            {
                                condicion1 = true;
                            }
                        }
                        if (configuracionProgramaRelacionados.IdOperadorComparacionAvance == 3)//MenorIgual
                        {
                            if (estadoAlumno.ValorAvance <= configuracionProgramaRelacionados.ValorAvance)
                            {
                                condicion1 = true;
                            }
                        }
                        if (configuracionProgramaRelacionados.IdOperadorComparacionAvance == 4)
                        { //MayorIgual
                            if (estadoAlumno.ValorAvance >= configuracionProgramaRelacionados.ValorAvance)
                            {
                                condicion1 = true;
                            }

                        }                        
                        if (configuracionProgramaRelacionados.IdOperadorComparacionAvance == 10)//Entre
                        {
                            if (estadoAlumno.ValorAvance >= configuracionProgramaRelacionados.ValorAvance && estadoAlumno.ValorAvance <= configuracionProgramaRelacionados.ValorAVanceOpc)
                            {
                                condicion1 = true;
                            }
                        }                        
                    }
                    else
                    {
                        condicion1 = true;
                    }
                    if (configuracionProgramaRelacionados.IdOperadorComparacionPromedio != null)
                    {                        
                        if (configuracionProgramaRelacionados.IdOperadorComparacionPromedio == 2)//2Igual
                        {
                            if (estadoAlumno.ValorPromedio == configuracionProgramaRelacionados.ValorPromedio)
                            {
                                condicion1 = true;
                            }
                        }
                        if (configuracionProgramaRelacionados.IdOperadorComparacionPromedio == 3)//MenorIgual
                        {
                            if (estadoAlumno.ValorPromedio <= configuracionProgramaRelacionados.ValorPromedio)
                            {
                                condicion2 = true;
                            }
                        }
                        if (configuracionProgramaRelacionados.IdOperadorComparacionPromedio == 4)
                        { //MayorIgual
                            if (estadoAlumno.ValorPromedio >= configuracionProgramaRelacionados.ValorPromedio)
                            {
                                condicion2 = true;
                            }

                        }                        
                        if (configuracionProgramaRelacionados.IdOperadorComparacionPromedio == 10)//Entre
                        {
                            if (estadoAlumno.ValorPromedio >= configuracionProgramaRelacionados.ValorPromedio && estadoAlumno.ValorPromedio <= configuracionProgramaRelacionados.ValorPromedioOpc)
                            {
                                condicion2 = true;
                            }
                        }                        
                    }
                    else
                    {
                        condicion2 = true;
                    }
                    if(condicion1 || condicion2)
                    {
                        alumnosPorPEspecificoSesionConfirmados.Add(alumno);
                    }
                }

            }*/

            return alumnosPorPEspecificoSesion;
        }

        public ConfiguracionWebinarDTO ObtenerConfiguracionWebinar(int IdWebinar)
        {
            try
            {
                ConfiguracionWebinarDTO obtenerConfiguracionWebinar = new ConfiguracionWebinarDTO();

                var obtenerConfiguracionWebinarDB = _dapper.QuerySPFirstOrDefault("[pla.SP_ObtenerConfiguracionWebinar]", new { IdWebinar = IdWebinar });
                if (!string.IsNullOrEmpty(obtenerConfiguracionWebinarDB) && !obtenerConfiguracionWebinarDB.Contains("[]"))
                {
                    obtenerConfiguracionWebinar = JsonConvert.DeserializeObject<ConfiguracionWebinarDTO>(obtenerConfiguracionWebinarDB);
                }
                return obtenerConfiguracionWebinar;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PespecificoAlumnosDTO> ObtenerAlumnosPorIdPEspecificoWebinar(int idPespecifico)
        {
            try
            {
                List<PespecificoAlumnosDTO> obtenerAlumnosPorPEspecifico = new List<PespecificoAlumnosDTO>();

                var obtenerAlumnosPorPEspecificoDB = _dapper.QuerySPDapper("[pla.SP_ObtenerAlumnosPorIdPEspecificoWebinar]", new { idPespecifico = idPespecifico });
                if (!string.IsNullOrEmpty(obtenerAlumnosPorPEspecificoDB) && !obtenerAlumnosPorPEspecificoDB.Contains("[]"))
                {
                    obtenerAlumnosPorPEspecifico = JsonConvert.DeserializeObject<List<PespecificoAlumnosDTO>>(obtenerAlumnosPorPEspecificoDB);
                }
                return obtenerAlumnosPorPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las sesiones por cursos del programa especifico que tienen una sesion en base a la fecha actual + cantidad de dias dentro de la semana actual
        /// </summary>
        /// <param name="id">Id del PEspecifico que se desea saber su proximo conjunto de sesiones (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el momento de la consulta</param>\
        /// <param name="incrementoZonaHoraria">Segun la diferencia horaria calcula una nueva hora</param>
        /// <param name="nombrePais">Nombre del pais del cual se va a evaluar la hora</param>
        /// <param name="incluirNombreCurso">Flag para validar si se incluye el nombre del curso en la cadena formateada</param>
        /// <returns>Cadena formateada con el proximo conjunto de sesion Webex</returns>
        public string ObtenerProximoConjuntoSesionWebex(int id, int cantidadDias, int incrementoZonaHoraria, string nombrePais, bool incluirNombreCurso)
        {
            try
            {
                var _resultado = new List<ConjuntoSesionProgramaEspecificoDTO>();
                var query = $@"ope.SP_ObtenerProximoConjuntoSesionProgramaEspecificoWebex";
                var resultado = _dapper.QuerySPDapper(query, new { IdPEspecifico = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<List<ConjuntoSesionProgramaEspecificoDTO>>(resultado);
                }


                var resultadoAgrupado = _resultado.GroupBy(x => new { x.IdPEspecifico, x.NombrePEspecifico })
                    .Select(y => new ConjuntoSesionProgramaEspecificoMaestroDTO
                    {
                        IdPEspecifico = y.Key.IdPEspecifico,
                        NombrePEspecifico = y.Key.NombrePEspecifico,
                        Sesiones = y.Select(w => new ConjuntoSesionProgramaEspecificoDetalleDTO()
                        {
                            DuracionSesionHoras = w.DuracionSesionHoras,
                            FechaSesion = w.FechaSesion,
                            //HorarioSesion = w.HorarioSesion
                            HorarioSesion = w.FechaSesion.AddHours(incrementoZonaHoraria).ToString("HH:mm") + " a " + w.FechaSesion.AddHours(w.DuracionSesionHoras).AddHours(incrementoZonaHoraria).ToString("HH:mm")
                        }).ToList()
                    });

                var htmlFinal = "";
                foreach (var item in resultadoAgrupado)
                {
                    if (incluirNombreCurso)
                        htmlFinal += $@"<p><strong>Curso:</strong> {item.NombrePEspecifico}</p>";

                    foreach (var sesion in item.Sesiones)
                    {
                        htmlFinal += $@"
										 <p>
										 <strong>Fecha:</strong> { sesion.FechaSesion.ToString("dd/MM/yyyy") }
										 <br/>
										 <strong>Horarios:</strong> { sesion.HorarioSesion } horario de {nombrePais}
										 <br/>
										 <strong>Duración:</strong> { sesion.DuracionSesionHoras } horas
                                         <br/>
										 </p>
						 ";
                    }

                    htmlFinal += $@"</br>";
                }
                htmlFinal += "";

                return htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public CiudadPEspecificoDTO ObtenerCiudad(int id)
        {
            try
            {
                CiudadPEspecificoDTO ciudadPEspecifico = new CiudadPEspecificoDTO();
                var _query = "SELECT IdCiudad FROM pla.V_ObtenerCiudadPorPEspecifico WHERE IdPEspecifico = @id AND EstadoPEspecifico = 1 AND EstadoRegionCiudad = 1";
                var ciudadPEspecificoDB = _dapper.FirstOrDefault(_query, new { id });
                ciudadPEspecifico = JsonConvert.DeserializeObject<CiudadPEspecificoDTO>(ciudadPEspecificoDB);
                return ciudadPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<DatosListaPespecificoDTO> ObtenerListaProgramaEspecifico(int idCentroCosto)
        {
            try
            {
                string _queryPEspecifico = "Select Id,Nombre,Codigo,IdCentroCosto,EstadoP,EstadoPId,TipoId,Tipo,IdProgramaGeneral,Ciudad,CursoIndividual" +
                                           ", CodigoBanco, OrigenPrograma, Duracion, ActualizacionAutomatica, IdCursoMoodle, IdExpositor_Referencia, " +
                                           "IdCiudad, IdAmbiente, UrlDocumentoCronograma, UrlDocumentoCronogramaGrupos, TipoSesion From pla.V_ListaProgramaEspecificoParaTabla where Estado=1 and IdCentroCosto=@IdCentroCosto Order by FechaCreacion desc";
                var queryPEspecifico = _dapper.QueryDapper(_queryPEspecifico, new { IdCentroCosto = idCentroCosto });
                return JsonConvert.DeserializeObject<List<DatosListaPespecificoDTO>>(queryPEspecifico);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Permite obtener los Id del programa especifico, nombre completo del centro de costo enviandole como parametro de los 2 ultimos anios
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<PEspecificoCentroCostoDTO> ObtenerPEspecificoPorCentroCosto2UltimosAnios(string nombre)
        {
            try
            {
                List<PEspecificoCentroCostoDTO> pEspecificoCentroCosto = new List<PEspecificoCentroCostoDTO>();
                var pEspecificoCentroCostoDB = _dapper.QuerySPDapper("[pla].[SP_ObtenerPEspecificoPorCentroCosto2Anios]", new { Nombre = nombre });

                if (!string.IsNullOrEmpty(pEspecificoCentroCostoDB) && !pEspecificoCentroCostoDB.Contains("[]"))
                {
                    pEspecificoCentroCosto = JsonConvert.DeserializeObject<List<PEspecificoCentroCostoDTO>>(pEspecificoCentroCostoDB);
                }
                return pEspecificoCentroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Permite obtener los Id del programa especifico, nombre completo del centro de costo enviandole como parametro 
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<PEspecificoCentroCostoDTO> ObtenerPEspecificoPorCentroCosto(string nombre)
        {
            try
            {
                List<PEspecificoCentroCostoDTO> pEspecificoCentroCosto = new List<PEspecificoCentroCostoDTO>();
                var _query = "SELECT IdPEspecifico, Nombre FROM pla.T_ObtenerIdPEspecificoPorCentroCosto WHERE  Nombre LIKE CONCAT('%',@nombre,'%') AND EstadoPEspecificio = 1 AND EstadoCentroCosto = 1";
                var pEspecificoCentroCostoDB = _dapper.QueryDapper(_query, new { nombre });
                if (!string.IsNullOrEmpty(pEspecificoCentroCostoDB) && !pEspecificoCentroCostoDB.Contains("[]"))
                {
                    pEspecificoCentroCosto = JsonConvert.DeserializeObject<List<PEspecificoCentroCostoDTO>>(pEspecificoCentroCostoDB);
                }
                return pEspecificoCentroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Equivalente a "ObtenerPEspecificoPorCentroCosto" pero esta version recive el Id de PEspecifico y retorna su Nombre deCentro de costo 
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<PEspecificoCentroCostoDTO> ObtenerCentroCostoPorPEspecifico(int IdPEspecifico)
        {
            try
            {
                List<PEspecificoCentroCostoDTO> pEspecificoCentroCosto = new List<PEspecificoCentroCostoDTO>();
                var _query = "SELECT IdPEspecifico, Nombre FROM pla.T_ObtenerIdPEspecificoPorCentroCosto WHERE  IdPEspecifico=@IdPEspecifico AND EstadoPEspecificio = 1 AND EstadoCentroCosto = 1";
                var pEspecificoCentroCostoDB = _dapper.QueryDapper(_query, new { IdPEspecifico = IdPEspecifico });
                if (!string.IsNullOrEmpty(pEspecificoCentroCostoDB) && !pEspecificoCentroCostoDB.Contains("[]"))
                {
                    pEspecificoCentroCosto = JsonConvert.DeserializeObject<List<PEspecificoCentroCostoDTO>>(pEspecificoCentroCostoDB);
                }
                return pEspecificoCentroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene el nombre de PGeneral y la descripcion de PGeneralDescripcion a traves del Id de PEspecifico
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public PGeneralNombreDescripcionDTO ObtenerNombreDescripcion(int Id)
        {
            try
            {
                PGeneralNombreDescripcionDTO pGeneralNombreDescripcionDTO = new PGeneralNombreDescripcionDTO();
                var _query = "SELECT IdPGeneral, Nombre, Descripcion FROM pla.V_ObtenerPGeneralNombreDescripcion WHERE EstadoPGeneral = 1 AND EstadoPEspecifico = 1 AND IdPEspecifico = @Id";
                var query = _dapper.FirstOrDefault(_query, new { Id = Id });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    pGeneralNombreDescripcionDTO = JsonConvert.DeserializeObject<PGeneralNombreDescripcionDTO>(query);
                }
                return pGeneralNombreDescripcionDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el nombre de Centro de Costo por el Id de PEspecifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PEspecificoCentroCostoDTO ObtenerPEspecificoCentroCostoPorId(int id)
        {
            try
            {
                PEspecificoCentroCostoDTO pEspecificoCentroCosto = new PEspecificoCentroCostoDTO();
                var _query = "SELECT IdPEspecifico, Nombre FROM pla.T_ObtenerIdPEspecificoPorCentroCosto WHERE  IdPEspecifico = @id AND EstadoPEspecificio = 1 AND EstadoCentroCosto = 1";
                var pEspecificoCentroCostoDB = _dapper.FirstOrDefault(_query, new { id });
                if (!string.IsNullOrEmpty(pEspecificoCentroCostoDB) && !pEspecificoCentroCostoDB.Contains("[]"))
                {
                    pEspecificoCentroCosto = JsonConvert.DeserializeObject<PEspecificoCentroCostoDTO>(pEspecificoCentroCostoDB);
                }
                return pEspecificoCentroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Lista de programas especifico mediante IdProgramaGeneral para filtro
        /// </summary>
        /// <param name="IdProgramaGeneral"></param>
        /// <returns></returns>
        public List<DatosListaPespecificoDTO> ObtenerListaProgramaEspecificoPorIdPGeneral(int idProgramaGeneral)
        {
            try
            {
                var query = "Select Id,Nombre,Codigo,IdCentroCosto,EstadoP,EstadoPId,TipoId,Tipo,IdProgramaGeneral,Ciudad,CursoIndividual" +
                                           ", CodigoBanco, OrigenPrograma, Duracion, ActualizacionAutomatica, IdCursoMoodle, IdExpositor_Referencia, " +
                                           "IdCiudad, IdAmbiente, UrlDocumentoCronograma, UrlDocumentoCronogramaGrupos, TipoSesion From pla.V_ListaProgramaEspecificoParaTabla where Estado=1 and IdProgramaGeneral=@idProgramaGeneral Order by FechaCreacion desc";
                var PEspecifico = _dapper.QueryDapper(query, new { idProgramaGeneral });

                return JsonConvert.DeserializeObject<List<DatosListaPespecificoDTO>>(PEspecifico);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene Lista de Programas Especificos para filtro
        /// </summary>
        /// <returns>Lista de objetos de clase DatosListaPespecificoDTO</returns>
        public List<DatosListaPespecificoDTO> ObtenerListaProgramaEspecificoParaFiltro()
        {
            try
            {
                var query = "SELECT Id,Nombre FROM pla.V_TPEspecifico_Webinar where Estado=1 ORDER BY Id DESC";
                var PEspecifico = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<DatosListaPespecificoDTO>>(PEspecifico);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene Lista de Programas Especificos para filtro para un combobox(PEspecifico) que depende 
        /// de otro combobox(PGeneral)
		/// </summary>
		/// <returns></returns>
		public List<DatosListaPespecificoDePgeneralDTO> ObtenerListaProgramaEspecificoParaFiltroDeProgramaGeneral()
        {
            try
            {
                var query = "SELECT Id, Nombre, IdProgramaGeneral FROM pla.V_ListaProgramaEspecificoParaTabla where Estado=1  ORDER BY Id DESC";
                var PEspecifico = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<DatosListaPespecificoDePgeneralDTO>>(PEspecifico);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// <summary>
        /// Obtiene datos de PGeneral que se usaran para PEspecifico mediante IdProgramaGeneral
        /// </summary>
        /// <param name="IdProgramaGeneral"></param>
        /// <returns></returns>
        public DatosPGeneralDTO ObtenerDatosPGeneralParaPEspecifico(int idProgramaGeneral)
        {
            try
            {
                var query = "Select Id, Nombre, Codigo, IdArea, IdSubArea, IdCategoria from pla.V_TPGeneral_ObtenerDatosParaPespecifico where Estado = 1 and Id = @idProgramaGeneral";
                var PGeneral = _dapper.FirstOrDefault(query, new { idProgramaGeneral });
                return JsonConvert.DeserializeObject<DatosPGeneralDTO>(PGeneral);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene datos de PGeneral que se usaran para PEspecifico mediante IdProgramaGeneral
        /// </summary>
        /// <returns></returns>
        public List<DatosProgramasWebexDTO> ObtenerProgramasWebex()
        {
            try
            {
                var query = "Select IdPEspecifico,IdTiempoFrecuencia,Valor,IdTiempoFrecuenciaCorreo,ValorFrecuenciaCorreo,IdPlantillaFrecuenciaCorreo,IdTiempoFrecuenciaWhatsapp,ValorFrecuenciaWhatsapp,IdPlantillaFrecuenciaWhatsapp,IdTiempoFrecuenciaCorreoConfirmacion,ValorFrecuenciaCorreoConfirmacion,IdPlantillaCorreoConfirmacion,IdTiempoFrecuenciaCorreoDocente,ValorFrecuenciaDocente,IdPlantillaDocente from pla.V_ObtenrconfiguracioncreacionWebex where Estado = 1";
                //var query = "Select IdPEspecifico,IdTiempoFrecuencia,Valor from pla.T_configuracioncreacion where Estado = 1";
                var Lista = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<DatosProgramasWebexDTO>>(Lista);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene sesiones webinar de PGeneral que se usaran para PEspecifico mediante IdProgramaGeneral
        /// </summary>
        /// <returns></returns>
        public List<DatosProgramasWebinarDTO> ObtenerProgramasWebinar()
        {
            try
            {
                var query = "Select IdPEspecifico,IdTiempoFrecuenciaCorreoConfirmacion,ValorFrecuenciaCorreoConfirmacion,IdTiempoFrecuenciaCreacion,ValorCreacion" +
                    " ,IdTiempoFrecuenciaCorreo,ValorFrecuenciaCorreo,IdTiempoFrecuenciaWhatsApp,ValorFrecuenciaWhasApp,IdPlantillaCorreoConfirmacion,IdPlantillaCorreo,IdPlantillaWhasApp from pla.T_configuracionwebinar where Estado = 1";

                var Lista = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<DatosProgramasWebinarDTO>>(Lista);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las categorias de las ciudades mediante IdCiudad y IdCategoriaPrograma
        /// </summary>
        /// <param name="IdCiudad"></param>
        /// <param name="IdCategoriaPrograma"></param>
        /// <returns></returns>
        public CategoriaCiudadDTO ObtenerCiudadCategoria(int idCiudad, int idCategoriaPrograma)
        {
            try
            {
                var query = "Select Id, IdCategoriaPrograma, IdCiudad, TroncalCompleto, IdRegionCiudad from pla.V_TCategoriaCiudad_ObtenerCategorias where Estado = 1 and IdCiudad = @idCiudad and IdCategoriaPrograma = @idCategoriaPrograma";
                var CiudadCategoria = _dapper.FirstOrDefault(query, new { idCiudad, idCategoriaPrograma });
                return JsonConvert.DeserializeObject<CategoriaCiudadDTO>(CiudadCategoria);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene CentroCosto po Pespecifico
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<PEspecificoCentroCostoDTO> ObtenerCentroCostoPorPespecifico()
        {
            try
            {
                List<PEspecificoCentroCostoDTO> pEspecificoCentroCosto = new List<PEspecificoCentroCostoDTO>();
                var _query = "SELECT IdPEspecifico, Nombre FROM pla.T_ObtenerIdPEspecificoPorCentroCosto WHERE EstadoPEspecificio = 1 AND EstadoCentroCosto = 1";
                var pEspecificoCentroCostoDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pEspecificoCentroCostoDB) && !pEspecificoCentroCostoDB.Contains("[]"))
                {
                    pEspecificoCentroCosto = JsonConvert.DeserializeObject<List<PEspecificoCentroCostoDTO>>(pEspecificoCentroCostoDB);
                }
                return pEspecificoCentroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Cronograma para modulo por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public string ObtenerCronogramaParaModulo(int idPespecifico)
        {

            try
            {
                string _queryProgramaEspecifico = "Select Id,Nombre,Duracion,IdCiudad,Tipo,IdSesion_Inicio From pla.V_ProgramaEspecificoPorCodigo Where Estado=1 and Id=@IdPespecifico";
                var queryProgramaEspecifico = _dapper.FirstOrDefault(_queryProgramaEspecifico, new { IdPespecifico = idPespecifico });
                DatosProgramaEspecificoDTO programaEspecifico = JsonConvert.DeserializeObject<DatosProgramaEspecificoDTO>(queryProgramaEspecifico);
                PespecificoSesionRepositorio _repPecificoSesion = new PespecificoSesionRepositorio();
                List<PespecificoSesionCompuestoDTO> sesiones = _repPecificoSesion.ObtenerCronogramaIndividualPorPEspecifico(programaEspecifico);
                PespecificoBO pespecifico = new PespecificoBO();
                string url = pespecifico.GenerarPDFCronograma(programaEspecifico.Id, true, programaEspecifico.Nombre, "System", sesiones);
                return url;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obtiene registros para la generacion de pdf
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public RegistroProgramaEspecificoDTO ObtenerRegistroPespecificoPorId(int idPespecifico)
        {
            string _queryPespecifico = "Select Id,Nombre,Codigo,IdCentroCosto,EstadoP,Tipo,IdProgramaGeneral,Ciudad,CursoIndividual From pla.V_ObtenerRegistroPespecifico Where Estado=1 and Id=@IdPespecifico";
            var queryPespecifico = _dapper.FirstOrDefault(_queryPespecifico, new { IdPespecifico = idPespecifico });
            return JsonConvert.DeserializeObject<RegistroProgramaEspecificoDTO>(queryPespecifico);

        }

        /// <summary>
        /// Obtiene cursos de centro costo por Programa especifico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        public List<CursosCentroCostoDTO> ObtenerCursosCentroCosto(int? idPEspecifico)
        {
            try
            {
                List<CursosCentroCostoDTO> cursosCentroCosto = new List<CursosCentroCostoDTO>();
                var _query = string.Empty;
                if (idPEspecifico > 0)
                {
                    _query = "SELECT Id, IdPEspecifico, NombreCursoEspecifico, Duracion, Orden, FechaCreacion, UsuarioCreacion, NombrePEspecifico, FechaModificacion, UsuarioModificacion  FROM pla.V_CursosCentroCosto_ProgramaEspecifico WHERE IdPEspecifico = @idPEspecifico AND EstadoCursoEspecifico = 1 AND EstadoProgramaEspecifico = 1 ORDER BY Orden";
                    var cursosCentroCostoDB = _dapper.QueryDapper(_query, new { idPEspecifico });
                    if (!string.IsNullOrEmpty(cursosCentroCostoDB) && !cursosCentroCostoDB.Contains("[]"))
                    {
                        cursosCentroCosto = JsonConvert.DeserializeObject<List<CursosCentroCostoDTO>>(cursosCentroCostoDB);
                    }
                }
                else
                {
                    _query = "SELECT Id, IdPEspecifico, NombreCursoEspecifico, Duracion, Orden, FechaCreacion, UsuarioCreacion, NombrePEspecifico, FechaModificacion, UsuarioModificacion  FROM pla.V_CursosCentroCosto_ProgramaEspecifico WHERE EstadoCursoEspecifico = 1 AND EstadoProgramaEspecifico = 1 ORDER BY Orden";
                    var cursosCentroCostoDB = _dapper.QueryDapper(_query, null);
                    if (!string.IsNullOrEmpty(cursosCentroCostoDB) && !cursosCentroCostoDB.Contains("[]"))
                    {
                        cursosCentroCosto = JsonConvert.DeserializeObject<List<CursosCentroCostoDTO>>(cursosCentroCostoDB);
                    }
                }

                return cursosCentroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// ObtieneInformacion de ProgramaEspecifico
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public DatosProgramaEspecificoDTO ObtenerDatosProgramaEspecificoPorId(int idPespecifico)
        {
            try
            {
                string _queryProgramaEspecifico = "Select Id,Nombre,Duracion,IdCiudad,Tipo,IdSesion_Inicio From pla.V_ProgramaEspecificoPorId Where Estado=1 and Id=@IdPespecifico";
                var queryProgramaEspecifico = _dapper.FirstOrDefault(_queryProgramaEspecifico, new { IdPespecifico = idPespecifico });
                return JsonConvert.DeserializeObject<DatosProgramaEspecificoDTO>(queryProgramaEspecifico);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
   
        ///Repositorio: PEspecificoRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene Informacion para ProgramaEspecifico
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifcio </param>
        /// <returns> Informacion Programa Especifico : DatosListaPespecificoDTO </returns>
        public DatosListaPespecificoDTO ObtenerDatosCompletosPespecificoPorId(int idPespecifico)
        {
            string queryPEspecifico = "Select Id,Nombre,Codigo,IdCentroCosto,EstadoP,EstadoPId,TipoId,Tipo,IdProgramaGeneral,Ciudad,CursoIndividual" +
                                           ", CodigoBanco, OrigenPrograma, Duracion, ActualizacionAutomatica, IdCursoMoodle, IdExpositor_Referencia, " +
                                           "IdCiudad, IdAmbiente, UrlDocumentoCronograma From pla.V_ListaProgramaEspecificoParaTabla where Estado=1 and Id=@IdPespecifico";
            var queryPEspecificoBD = _dapper.FirstOrDefault(queryPEspecifico, new { IdPespecifico = idPespecifico });
            return JsonConvert.DeserializeObject<DatosListaPespecificoDTO>(queryPEspecificoBD);
        }

        public List<DatosProgramaEspecificoDuracionDTO> ObtenerDatosduracionPespecifico(int idPespecifico)
        {
            try
            {
                string _queryDuracionPespecifico = "Select PEspecificoHijoId AS Id,Nombre,IdProgramaGeneral,Duracion From pla.V_VerificarDuracionProgramaespecifico Where Estado=1 and PEspecificoPadreId=@IdPespecifico";
                var queryDuracionPespecifico = _dapper.QueryDapper(_queryDuracionPespecifico, new { IdPespecifico = idPespecifico });
                return JsonConvert.DeserializeObject<List<DatosProgramaEspecificoDuracionDTO>>(queryDuracionPespecifico);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene Informacion de todos los ProgramaEspecifico
        /// </summary>
        /// <returns></returns>
        public List<DatosProgramaEspecificoListaDTO> ObtenerDatosProgramaEspecifico()
        {
            string _queryProgramaEspecifico = "Select id, name, pg From pla.V_ListaProgramaEspecifico";
            var queryProgramaEspecifico = _dapper.QueryDapper(_queryProgramaEspecifico, null);
            return JsonConvert.DeserializeObject<List<DatosProgramaEspecificoListaDTO>>(queryProgramaEspecifico);
        }

        /// <summary>
        /// Obtiene el Id y NombreCompleto de Pespecifico filtrado por el idPegeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<PEspecificoFiltroPGeneralDTO> ObtenerPEspecificoPorIdPGeneral(int idPGeneral)
        {
            try
            {
                List<PEspecificoFiltroPGeneralDTO> obtenerPEspecificoPorIdPGeneral = new List<PEspecificoFiltroPGeneralDTO>();
                var _query = "SELECT Id, NombreCompleto from pla.V_ObtenerPEspecificoPorPGeneral WHERE IdPGeneral = @idPGeneral AND PEspecificoEstado = 1 AND PGeneralEstado = 1";
                var obtenerPEspecificoIdPGeneralDB = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(obtenerPEspecificoIdPGeneralDB) && !obtenerPEspecificoIdPGeneralDB.Contains("[]"))
                {
                    obtenerPEspecificoPorIdPGeneral = JsonConvert.DeserializeObject<List<PEspecificoFiltroPGeneralDTO>>(obtenerPEspecificoIdPGeneralDB);
                }
                return obtenerPEspecificoPorIdPGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene registro de CursoIndividual de ProgramaEspecifico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        public int ObtenerCursoIndividual(int idPEspecifico)
        {
            try
            {
                var resultado = -1;
                var cursoIndividual = GetBy(x => x.Id == idPEspecifico && x.Estado == true, x => new { x.CursoIndividual });
                if (cursoIndividual != null)
                {
                    resultado = Convert.ToInt32(cursoIndividual);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Se obtiene la lista de sesiones por el idPespecifico y el cursoIndividual
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <param name="cursoIndividual"></param>
        /// <returns></returns>
        public List<PEspecificoSesionFiltroDTO> ObtenerSesionPorPEspecifico(int idPespecifico, int cursoIndividual)
        {
            try
            {
                List<PEspecificoSesionFiltroDTO> obtenerSesionPorPEspecifico = new List<PEspecificoSesionFiltroDTO>();
                var obtenerSesionPorPEspecificoDB = _dapper.QuerySPDapper("pla.SP_ObtenerSesionPorPEspecifico", new { idPespecifico, cursoIndividual });
                if (!string.IsNullOrEmpty(obtenerSesionPorPEspecificoDB) && !obtenerSesionPorPEspecificoDB.Contains("[]"))
                {
                    obtenerSesionPorPEspecifico = JsonConvert.DeserializeObject<List<PEspecificoSesionFiltroDTO>>(obtenerSesionPorPEspecificoDB);
                }
                return obtenerSesionPorPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Se obtiene la lista de sesiones por el idPespecifico y el cursoIndividual
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<PEspecificoSesionWebexDTO> ObtenerSesionesNoVencidasPorPEspecifico(int idPespecifico)
        {
            try
            {
                List<PEspecificoSesionWebexDTO> obtenerSesionPorPEspecifico = new List<PEspecificoSesionWebexDTO>();
                var obtenerSesionPorPEspecificoDB = _dapper.QuerySPDapper("pla.SP_ObtenerSesionPorPEspecifico", new { idPespecifico });
                if (!string.IsNullOrEmpty(obtenerSesionPorPEspecificoDB) && !obtenerSesionPorPEspecificoDB.Contains("[]"))
                {
                    obtenerSesionPorPEspecifico = JsonConvert.DeserializeObject<List<PEspecificoSesionWebexDTO>>(obtenerSesionPorPEspecificoDB);
                }
                return obtenerSesionPorPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// Autor: Jose Villena
        /// Fecha: 04-23-2021
        /// Version: 1.0
        /// <summary>
        /// Devulve todas las sesiones del grupo segun el id pespecifico
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="grupo"> Grupo Sesion</param>
        /// <returns>Objeto</returns> 
        public List<PEspecificoSesionGrupoAnteriorDTO> ObtenerSesionesPorPEspecificoGrupoAnterior(int idPespecifico, int grupo)
        {
            try
            {
                List<PEspecificoSesionGrupoAnteriorDTO> obtenerSesionPorPEspecifico = new List<PEspecificoSesionGrupoAnteriorDTO>();
                var obtenerSesionPorPEspecificoDB = _dapper.QuerySPDapper("pla.SP_ObtenerSesionesPorGrupoPorPEspecifico", new { idPespecifico, grupo });
                if (!string.IsNullOrEmpty(obtenerSesionPorPEspecificoDB) && !obtenerSesionPorPEspecificoDB.Contains("[]"))
                {
                    obtenerSesionPorPEspecifico = JsonConvert.DeserializeObject<List<PEspecificoSesionGrupoAnteriorDTO>>(obtenerSesionPorPEspecificoDB);
                }
                return obtenerSesionPorPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 04-23-2021
        /// Version: 1.0
        /// <summary>
        /// Devulve todas las sesiones del grupo segun el id pespecifico
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="grupo"> Grupo Sesion</param>
        /// <returns>Objeto</returns>
        public List<PEspecificoCronogramaGrupalDTO> ObtenerSesionesPorPEspecificoGrupo(int idPespecifico, int grupo)
        {
            try
            {
                List<PEspecificoCronogramaGrupalDTO> obtenerSesionPorPEspecifico = new List<PEspecificoCronogramaGrupalDTO>();
                var obtenerSesionPorPEspecificoDB = _dapper.QuerySPDapper("pla.SP_ObtenerSesionesPorGrupoPorPEspecifico", new { idPespecifico, grupo });
                if (!string.IsNullOrEmpty(obtenerSesionPorPEspecificoDB) && !obtenerSesionPorPEspecificoDB.Contains("[]"))
                {
                    obtenerSesionPorPEspecifico = JsonConvert.DeserializeObject<List<PEspecificoCronogramaGrupalDTO>>(obtenerSesionPorPEspecificoDB);
                }
                return obtenerSesionPorPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

         /// <summary>
        /// Se obtiene la lista de sesiones por el idPespecifico y el cursoIndividual
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<PEspecificoSesionWebexUrlDTO> ObtenerSesionesNoVencidasPorPEspecificoURL(int idPespecifico)
        {
            try
            {
                List<PEspecificoSesionWebexUrlDTO> obtenerSesionPorPEspecifico = new List<PEspecificoSesionWebexUrlDTO>();
                var obtenerSesionPorPEspecificoDB = _dapper.QuerySPDapper("pla.SP_ObtenerSesionPorPEspecificoUrl", new { idPespecifico });
                if (!string.IsNullOrEmpty(obtenerSesionPorPEspecificoDB) && !obtenerSesionPorPEspecificoDB.Contains("[]"))
                {
                    obtenerSesionPorPEspecifico = JsonConvert.DeserializeObject<List<PEspecificoSesionWebexUrlDTO>>(obtenerSesionPorPEspecificoDB);
                }
                return obtenerSesionPorPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Se obtiene la lista de sesiones por el idPespecifico y el cursoIndividual
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public TipoProgramaPEspecificoDTO ObtenerTipoProgramaPEspecifico(int idPEspecifico)
        {
            try
            {
                var query = "SELECT IdPGeneral,IdPEspecifico,IdTipoPrograma FROM pla.V_ObtenerTipoProgramaEspecifcico WHERE ESTADO = 1 AND IdPEspecifico = @idPEspecifico";
                var tipoPrograma = _dapper.QueryDapper(query, new { idPEspecifico });
                return JsonConvert.DeserializeObject<TipoProgramaPEspecificoDTO>(tipoPrograma);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<PEspecificoDatosDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PEspecificoDatosDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Codigo = y.Codigo,
                    IdCentroCosto = y.IdCentroCosto,
                    Frecuencia = y.Frecuencia,
                    EstadoP = y.EstadoP,
                    Tipo = y.Tipo,
                    TipoAmbiente = y.TipoAmbiente,
                    Categoria = y.Categoria,
                    IdProgramaGeneral = y.IdProgramaGeneral,
                    Ciudad = y.Ciudad,
                    FechaInicio = y.FechaInicio,
                    FechaTermino = y.FechaTermino,
                    FechaInicioV = y.FechaInicioV,
                    FechaTerminoV = y.FechaTerminoV,
                    CodigoBanco = y.CodigoBanco,
                    FechaInicioP = y.FechaInicioP,
                    FechaTerminoP = y.FechaTerminoP,
                    FrecuenciaId = y.FrecuenciaId,
                    EstadoPid = y.EstadoPid,
                    TipoId = y.TipoId,
                    CategoriaId = y.CategoriaId,
                    OrigenPrograma = y.OrigenPrograma,
                    IdCiudad = y.IdCiudad,
                    CoordinadoraAcademica = y.CoordinadoraAcademica,
                    CoordinadoraCobranza = y.CoordinadoraCobranza,
                    Duracion = y.Duracion,
                    ActualizacionAutomatica = y.ActualizacionAutomatica,
                    IdCursoMoodle = y.IdCursoMoodle,
                    CursoIndividual = y.CursoIndividual,
                    IdSesionInicio = y.IdSesionInicio,
                    IdExpositorReferencia = y.IdExpositorReferencia,
                    IdAmbiente = y.IdAmbiente,
                    UrlDocumentoCronograma = y.UrlDocumentoCronograma

                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la fecha de inicio de un programa especifico
        /// </summary>
        /// <param name="idProgramaGeneral"></param>
        /// <param name="idProgramaEspecifico"></param>
        /// <returns></returns>
        public FechaInicioProgramaEspecificoDTO FechaProgramaEspecifico(int idProgramaGeneral, int idProgramaEspecifico)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("pla.SP_ObtenerFechaInicioProgramaEspecifico", new { idPrograma = idProgramaGeneral, idEspecifico = idProgramaEspecifico });
                var rpta = JsonConvert.DeserializeObject<FechaInicioProgramaEspecificoDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la fecha de inicio de un programa especifico
        /// </summary>
        /// <param name="idProgramaEspecifico">Id del programa especifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="idTiempoFrecuencia">Id del tiempo de frecuencia (PK de la tabla mkt.T_TiempoFrecuencia)</param>
        /// <param name="valorTiempoFrecuencia">Entero con el valor segun el tiempo seleccionado</param>
        /// <param name="idTiempoFrecuenciaCorreo">Id del tiempo de frecuencia (PK de la tabla mkt.T_TiempoFrecuencia)</param>
        /// <param name="valorFrecuenciaCorreo">Entero con el valor de frecuencia de correo</param>
        /// <param name="idTiempoFrecuenciaWhatsapp">Id del tiempo de frecuencia (PK de la tabla mkt.T_TiempoFrecuencia)</param>
        /// <param name="valorFrecuenciaWhatsapp">Entero con el valor de frecuencia de Whatsapp</param>
        /// <param name="idPlantillaFrecuenciaCorreo">Id de la plantilla de frecuencia de correo (PK de la tabla mkt.T_Plantilla)</param>
        /// <param name="idPlantillaFrecuenciaWhatsapp">Id de la plantilla de frecuencia de WhatsApp (PK de la tabla mkt.T_Plantilla)</param>
        /// <param name="idTiempoFrecuenciaCorreoConfirmacion">Id del tiempo de frecuencia (PK de la tabla mkt.T_TiempoFrecuencia)</param>
        /// <param name="valorFrecuenciaCorreoConfirmacion">Entero con el valor de frecuencia de correo de confirmacion</param>
        /// <param name="idPlantillaCorreoConfirmacion">Id de la plantilla de correo de confirmacion (PK de la tabla mkt.T_Plantilla)</param>
        /// <param name="idTiempoFrecuenciaCorreoDocente">Id del tiempo de frecuencia (PK de la tabla mkt.T_TiempoFrecuencia)</param>
        /// <param name="valorFrecuenciaDocente">Entero con el valor de frecuencia de docente</param>
        /// <param name="idPlantillaDocente">Id de la plantilla de docente (PK de la tabla mkt.T_Plantilla)</param>
        /// <returns>Objeto de clase ResultadoFinalDTO</returns>
        public ResultadoFinalDTO InsertarFrecuenciaWebinar(int idProgramaEspecifico, int idTiempoFrecuencia, int valorTiempoFrecuencia, int idTiempoFrecuenciaCorreo, int valorFrecuenciaCorreo, int idTiempoFrecuenciaWhatsapp, int valorFrecuenciaWhatsapp, int idPlantillaFrecuenciaCorreo, int idPlantillaFrecuenciaWhatsapp, int idTiempoFrecuenciaCorreoConfirmacion, int valorFrecuenciaCorreoConfirmacion, int idPlantillaCorreoConfirmacion, int idTiempoFrecuenciaCorreoDocente, int valorFrecuenciaDocente, int idPlantillaDocente)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("pla.SP_InsertarConfiguracionCreacionWebinar", new { IdProgramaEspecifico = idProgramaEspecifico, IdTiempoFrecuencia = idTiempoFrecuencia, ValorTiempoFrecuencia = valorTiempoFrecuencia, IdTiempoFrecuenciaCorreo = idTiempoFrecuenciaCorreo, ValorFrecuenciaCorreo = valorFrecuenciaCorreo, IdTiempoFrecuenciaWhatsapp = idTiempoFrecuenciaWhatsapp, ValorFrecuenciaWhatsapp = valorFrecuenciaWhatsapp, IdPlantillaFrecuenciaCorreo = idPlantillaFrecuenciaCorreo, IdPlantillaFrecuenciaWhatsapp = idPlantillaFrecuenciaWhatsapp, IdTiempoFrecuenciaCorreoConfirmacion = idTiempoFrecuenciaCorreoConfirmacion, ValorFrecuenciaCorreoConfirmacion = valorFrecuenciaCorreoConfirmacion, IdPlantillaCorreoConfirmacion = idPlantillaCorreoConfirmacion, IdTiempoFrecuenciaCorreoDocente = idTiempoFrecuenciaCorreoDocente, ValorFrecuenciaDocente = valorFrecuenciaDocente, IdPlantillaDocente = idPlantillaDocente });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la fecha de inicio de un programa especifico
        /// </summary>
        /// <param name="idProgramaEspecifico">Id del programa especifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="usuario">Usuario que realiza la modificacion</param>
        /// <returns>Objeto de clase ResultadoFinalDTO</returns>
        public ResultadoFinalDTO EliminarFrecuenciaWebinar(int idProgramaEspecifico, string usuario = "SYSTEM")
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("pla.SP_EliminarConfiguracionCreacionWebinar", new { IdProgramaEspecifico = idProgramaEspecifico, Usuario = usuario });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la fecha de inicio de un programa especifico
        /// </summary>
        /// <param name="idProgramaEspecifico">Id del programa especifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Objeto de clase ResultadoFinalDTO</returns>
        public ResultadoFinalDTO InsertarConfiguracionWebinar(int idProgramaEspecifico, int idOperadorAvance, int idOperadorPromedio, int valorAvance, int valorAvanceOpc, int valorCalificaciones, int valorCalificacionesOpc, List<int> idPespecificoRelacionado)
        {
            try
            {
                ResultadoFinalDTO rpta = new ResultadoFinalDTO();
                foreach (var item in idPespecificoRelacionado)
                {
                    var query = _dapper.QuerySPFirstOrDefault("pla.SP_InsertarConfiguracionWebinar", new
                    {
                        IdProgramaEspecifico = idProgramaEspecifico,
                        IdOperadorAvance = idOperadorAvance,
                        IdOperadorPromedio = idOperadorPromedio,
                        ValorAvance = valorAvance,
                        ValorAvanceOpc = valorAvanceOpc,
                        ValorCalificaciones = valorCalificaciones,
                        ValorCalificacionesOpc = valorCalificacionesOpc,
                        IdPespecificoRelacionado = item
                    });
                    rpta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// <summary>
        /// Obtiene Lista de Id, Nombre de una persona, filtrado por el tipo persona y el IdPespecifico para combo 
        /// </summary>
        /// <param name="idTipoPersona"></param>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        public List<ListaPersonaTipoPersonaFiltroDTO> ObtenerListaPersonaPorPEspecifico(int idTipoPersona, int idPEspecifico)
        {
            try
            {
                List<ListaPersonaTipoPersonaFiltroDTO> personaTipoPersona = new List<ListaPersonaTipoPersonaFiltroDTO>();// se debe traer IdPersonaTipoPersona , Nombre

                var _query = string.Empty;

                if (idTipoPersona == 1)// Si el tipo de persona es Alumno
                {
                    _query = "SELECT IdPersonaTipoPersona, Nombre, IdPEspecifico FROM conf.V_ObtenerPersonaTipoAlumnoFiltro WHERE IdPEspecifico = @idPEspecifico AND EstadoAlumno = 1 AND EstadoPEspecifico = 1 AND EstadoMatriculaCabecera = 1";
                    var personaTipoAlumnoDB = _dapper.QueryDapper(_query, new { idPEspecifico });
                    personaTipoPersona = JsonConvert.DeserializeObject<List<ListaPersonaTipoPersonaFiltroDTO>>(personaTipoAlumnoDB);
                }

                if (idTipoPersona == 2)// Si el tipo de persona es Docente(Expositor)
                {
                    _query = "SELECT IdPersonaTipoPersona, Nombre, IdPEspecifico FROM conf.V_ObtenerPersonaTipoExpositorFiltro WHERE IdPEspecifico = @idPEspecifico AND EstadoExpositor = 1 AND EstadoPEspecifico = 1 AND EstadoPEspecificoSesion = 1 GROUP BY IdPersonaTipoPersona, Nombre, IdPEspecifico";
                    var personaTipoExpositorDB = _dapper.QueryDapper(_query, new { idPEspecifico });
                    personaTipoPersona = JsonConvert.DeserializeObject<List<ListaPersonaTipoPersonaFiltroDTO>>(personaTipoExpositorDB);
                }

                return personaTipoPersona;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id(IdPais) y Nombre de un Pais filtrado por el IdPEspecifico para ser listado
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        public PaisPorPEspecificoFiltroDTO ObtenerPaisPorIdPEspecifico(int idPEspecifico)
        {
            try
            {
                PaisPorPEspecificoFiltroDTO paisPEspecifico = new PaisPorPEspecificoFiltroDTO();
                var _query = "SELECT IdPais, NombrePais, IdPEspecifico, NombrePEspecifico, IdCiudad, NombreCiudad FROM conf.V_ObtenerPaisPorPEspecifico WHERE IdPEspecifico = @idPEspecifico AND EstadoPEspecifico = 1";
                var paisPEspecificoDB = _dapper.FirstOrDefault(_query, new { idPEspecifico });
                paisPEspecifico = JsonConvert.DeserializeObject<PaisPorPEspecificoFiltroDTO>(paisPEspecificoDB);
                return paisPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Valida los cruces entre fechas y docentes en los cronogramas
        /// </summary>
        /// <param name="objeto"></param>
        /// <param name="fechas"></param>
        /// <returns></returns>
        public List<CruceSesionPEspecificoDTO> ValidarFechaExpositorCruce(DocenteAmbientePEspecificoDTO objeto, List<PEspecificoSesionFechasDTO> fechas)
        {
            try
            {
                List<CruceSesionPEspecificoDTO> registros = new List<CruceSesionPEspecificoDTO>();
                var query = "";

                foreach (var item in fechas)
                {
                    var res = "";
                    if (objeto.IdAmbiente != null && objeto.IdProveedor == null)
                    {
                        query = "SELECT DISTINCT IdPEspecifico, Curso,NombreCentroCosto, Ambiente, Expositor,Proveedor, Duracion, FechaHoraInicio, FechaFin, IdAmbiente,IdExpositor,IdProveedor FROM pla.V_ObtenerInformacionSesionesPEspecifico WHERE @Fecha BETWEEN FechaHoraInicio and FechaFin AND IdAmbiente = @IdAmbiente AND Estado = 1 AND IdPEspecifico != @IdPEspecifico";
                        res = _dapper.QueryDapper(query, new { Fecha = item.FechaHoraInicio, IdAmbiente = objeto.IdAmbiente, IdPEspecifico = objeto.Id });
                    }
                    else if (objeto.IdProveedor != null && objeto.IdAmbiente == null)
                    {
                        query = "SELECT DISTINCT IdPEspecifico, Curso,NombreCentroCosto, Ambiente, Expositor,Proveedor, Duracion, FechaHoraInicio, FechaFin, IdAmbiente,IdExpositor,IdProveedor FROM pla.V_ObtenerInformacionSesionesPEspecifico WHERE @Fecha BETWEEN FechaHoraInicio and FechaFin AND IdProveedor = @IdProveedor AND Estado = 1 AND IdPEspecifico != @IdPEspecifico";
                        res = _dapper.QueryDapper(query, new { Fecha = item.FechaHoraInicio, IdProveedor = objeto.IdProveedor, IdPEspecifico = objeto.Id });
                    }
                    else
                    {
                        query = "SELECT DISTINCT IdPEspecifico, Curso,NombreCentroCosto, Ambiente, Expositor,Proveedor, Duracion, FechaHoraInicio, FechaFin, IdAmbiente,IdExpositor,IdProveedor FROM pla.V_ObtenerInformacionSesionesPEspecifico WHERE @Fecha BETWEEN FechaHoraInicio and FechaFin AND (IdAmbiente = @IdAmbiente OR IdProveedor = @IdProveedor) AND Estado = 1 AND IdPEspecifico != @IdPEspecifico";
                        res = _dapper.QueryDapper(query, new { Fecha = item.FechaHoraInicio, IdAmbiente = objeto.IdAmbiente, IdProveedor = objeto.IdProveedor, IdPEspecifico = objeto.Id });
                    }

                    registros.AddRange(JsonConvert.DeserializeObject<List<CruceSesionPEspecificoDTO>>(res));
                }
                return registros;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene numero de grupos en las sesiones individuales
        /// </summary>
        /// <param name="idPadre"></param>
        /// <returns></returns>
        public List<GrupoFiltroSesionDTO> ObtenerGruposSesionesIndividuales(int idPadre)
        {
            try
            {
                var query = "SELECT DISTINCT Id,Nombre FROM pla.V_TPespecificoSesion_ObtenerNumeroGrupo WHERE ESTADO = 1 AND IdPEspecifico = @idPadre";
                var grupos = _dapper.QueryDapper(query, new { idPadre });
                return JsonConvert.DeserializeObject<List<GrupoFiltroSesionDTO>>(grupos);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene numero de grupos en las sesiones
        /// </summary>
        /// <param name="idPadre"></param>
        /// <returns></returns>
        public List<GrupoFiltroSesionDTO> ObtenerGruposSesiones(int idPadre)
        {
            try
            {
                var query = "SELECT DISTINCT Id, Nombre FROM pla.V_ObtenerGruposSesiones WHERE ESTADO = 1 AND PEspecificoPadreId = @idPadre ORDER BY Id";
                var grupos = _dapper.QueryDapper(query, new { idPadre });
                return JsonConvert.DeserializeObject<List<GrupoFiltroSesionDTO>>(grupos);
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
        public List<ListaPEspecificoFiltroDTO> ObtenerListaPEspecificoAutocompleto(string nombre)
        {
            try
            {
                List<ListaPEspecificoFiltroDTO> pEspecifico = new List<ListaPEspecificoFiltroDTO>();
                var _query = "SELECT Id, Nombre FROM pla.V_ObtenerListaTPEspecifico WHERE  Nombre LIKE CONCAT('%',@nombre,'%') AND Estado = 1";
                var pEspecificoDB = _dapper.QueryDapper(_query, new { nombre });
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    pEspecifico = JsonConvert.DeserializeObject<List<ListaPEspecificoFiltroDTO>>(pEspecificoDB);
                }
                return pEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Lista de sesiones mediante programa especifico y numero de grupo
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <param name="numeroGrupo"></param>
        /// <returns></returns>
        public List<CronogramaGrupoDTO> ObtenerCronogramaPEspecificoGrupo(int idPEspecifico, List<int> listaPespecifico, int numeroGrupo)
        {
            try
            {
                List<CronogramaGrupoDTO> rpta = new List<CronogramaGrupoDTO>();
                string query = "";
                if (listaPespecifico.Count == 0)
                {
                    query = "SELECT DISTINCT Id,FechaHoraInicio,Duracion,DuracionTotal,Curso,IdExpositor,IdProveedor,IdAmbiente,IdCiudad,PEspecificoHijoId,Tipo,IdModalidadCurso,Comentario,EsSesionInicio, IdCentroCosto, Grupo, GrupoSesion, TieneFur, MostrarPortalWeb FROM [pla].[V_ObtenerCronogramaGrupoDuplicado] WHERE Estado = 1 AND PEspecificoPadreId = @idPEspecifico AND Grupo = @numeroGrupo ORDER BY FechaHoraInicio ASC, PEspecificoHijoId ASC";
                }
                else
                {
                    query = "SELECT DISTINCT Id,FechaHoraInicio,Duracion,DuracionTotal,Curso,IdExpositor,IdProveedor,IdAmbiente,IdCiudad,PEspecificoHijoId,Tipo,IdModalidadCurso,Comentario,EsSesionInicio, IdCentroCosto, Grupo, GrupoSesion, TieneFur, MostrarPortalWeb FROM [pla].[V_ObtenerCronogramaGrupoDuplicado] WHERE Estado = 1 AND PEspecificoPadreId = @idPEspecifico AND Grupo = @numeroGrupo and PEspecificoHijoId in @listaPespecifico ORDER BY FechaHoraInicio ASC, PEspecificoHijoId ASC";
                }
                var cronograma = _dapper.QueryDapper(query, new { idPEspecifico, numeroGrupo, listaPespecifico });
                return JsonConvert.DeserializeObject<List<CronogramaGrupoDTO>>(cronograma);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CronogramaGrupoDTO> ObtenerCronogramaPEspecificoGrupoSesionIndividual(int idPEspecifico, int numeroGrupo)
        {
            try
            {
                var query = "SELECT DISTINCT Id,FechaHoraInicio,Duracion,DuracionTotal,Curso,IdExpositor,IdProveedor,IdAmbiente,IdCiudad,PEspecificoHijoId,Tipo,IdModalidadCurso,Comentario,EsSesionInicio, IdCentroCosto, Grupo, GrupoSesion, TieneFur, MostrarPortalWeb FROM [pla].[V_ObtenerCronogramaGrupoDuplicadoSesionIndividual] WHERE Estado = 1 AND PEspecificoHijoId = @idPEspecifico AND Grupo = @numeroGrupo ORDER BY FechaHoraInicio ASC, PEspecificoHijoId ASC";
                var cronograma = _dapper.QueryDapper(query, new { idPEspecifico, numeroGrupo });
                return JsonConvert.DeserializeObject<List<CronogramaGrupoDTO>>(cronograma);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene lista de programas especificos con centro costos
        /// </summary>
        /// <returns></returns>
        public List<FiltroPespecificoDTO> ObtenerProgramaEspecificoSesionCentroCostoParaFiltro()
        {
            try
            {
                //var pespecifico = GetBy(x => x.Estado == true, x => new FiltroPespecificoDTO { Id = x.Id, Nombre = x.Nombre, IdCentroCosto = x.IdCentroCosto.HasValue == true ? x.IdCentroCosto.Value : 0 }).ToList();
                var query = "SELECT Id, Nombre, IdCentroCosto FROM [pla].[V_ObtenerSesionesProgramaEspecifico] WHERE Estado = 1";
                var res = _dapper.QueryDapper(query, null);
                var pespecificoSesion = JsonConvert.DeserializeObject<List<FiltroPespecificoDTO>>(res);

                return pespecificoSesion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de programas especificos por programa general
        /// </summary>
        /// <returns></returns>
        public List<FiltroPespecificoPGeneralDTO> ObtenerPorPGeneral()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroPespecificoPGeneralDTO { Id = x.Id, Nombre = x.Nombre, IdProgramaGeneral = x.IdProgramaGeneral }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ScrapingConfiguracionDTO> ObtenerConfiguracionScraping(DateTime fecha, string centroCosto, string estadoScraping)
        {
            try
            {
                var res = _dapper.QuerySPDapper("[pla].[SP_ScrapingConfiguracionVuelos]", new { FechaActual = fecha.Date, CentroCosto = centroCosto, EstadoConsulta = estadoScraping });
                var lista = JsonConvert.DeserializeObject<List<ScrapingConfiguracionDTO>>(res);
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: PespecificoRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene una lista de fechas de inicio de programas segun el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> Lista Programa Especifico: List<ListaProgramaEspecificoPorIdProgramaDTO> </returns>
        public List<ListaProgramaEspecificoPorIdProgramaDTO> ObtenerListaFechaInicioSesion(int idPGeneral)
        {
            try
            {
                string query;
                List<ListaProgramaEspecificoPorIdProgramaDTO> lista = new List<ListaProgramaEspecificoPorIdProgramaDTO>();

                query = "SELECT Id, Nombre, Ciudad, Tipo, Duracion, EstadoPId, FechaCreacion, IdCategoria FROM [pla].[V_ListaProgramaEspecificoPorIdPrograma] WHERE IdPGeneral = @idPGeneral";
                var repuesta = _dapper.QueryDapper(query, new { idPGeneral });

                if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ListaProgramaEspecificoPorIdProgramaDTO>>(repuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: PespecificoRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene la proxima sesion segun el idPEspecifico y un tipo
        /// </summary>
        /// <param name="tipo">Tipo Programa Especifico</param>
        /// <returns> Lista Informacion Programa Especifico: List<EspecificoFechasInicioDTO></returns> 
        public List<EspecificoFechasInicioDTO> PEspecificoSesionInformacionFechaInicio(List<int> idPEspecifico, int tipo)
        {
            try
            {
                var query = string.Empty;
                List<EspecificoFechasInicioDTO> objeto = new List<EspecificoFechasInicioDTO>();

                if (tipo == 1)
                {
                    query = "SELECT IdPEspecifico, FechaHoraInicio FROM [pla].[V_ListaFechaInicioPEspecificoPadrePEspecificoHijoPorIdPadre] WHERE PEspecificoPadreId IN @idPEspecifico";
                }
                else if (tipo == 2)
                {
                    query = "SELECT IdPEspecifico, FechaHoraInicio FROM [pla].[V_ListaFechaInicioPEspecificoSesionPorIdPEspecifico] WHERE IdPEspecifico IN @idPEspecifico";
                }
                else
                {
                    query = "SELECT IdPEspecifico, FechaHoraInicio FROM [pla].[V_ListaFechaInicioPEspecificoSesionSinInicioPorIdPEspecifico] WHERE Orden=1 AND IdPEspecifico IN @idPEspecifico";
                }

                var repuesta = _dapper.QueryDapper(query, new { idPEspecifico });

                if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
                {
                    objeto = JsonConvert.DeserializeObject<List<EspecificoFechasInicioDTO>>(repuesta);
                }

                if(objeto.Any())
                {
                    return objeto;
                }
                else
                {
                    if(tipo == 2)
                    {
                        query = "SELECT IdPEspecifico, FechaHoraInicio FROM [pla].[V_ListaFechaInicioPEspecificoSesionSinInicioPorIdPEspecifico] WHERE Orden=1 AND IdPEspecifico IN @idPEspecifico";
                        var repuestaAux = _dapper.QueryDapper(query, new { idPEspecifico });
                        if (!string.IsNullOrEmpty(repuestaAux) && !repuestaAux.Contains("[]"))
                        {
                            objeto = JsonConvert.DeserializeObject<List<EspecificoFechasInicioDTO>>(repuestaAux);
                        }
                        return objeto;
                    }
                    else
                    {
                        return objeto;
                    }
                }
            } catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PEspecificoHistorialParticipacionDocenteDTO> ObtenerHistorialParticipacion(int idOportunidad)
        {
            try
            {
                List<AgendaDocenteHistorialParticipacionDetalle> historialParticipacionDetalle = new List<AgendaDocenteHistorialParticipacionDetalle>();
                var _query = "SELECT IdOportunidadHijo, Programa, Curso, Modalidad, FechaInicio, FechaTermino, Ciudad, EstadoP, Actividad, FechaProgramada, FechaReal, Comentario FROM com.V_ObtenerPEspecifico_HistorialParticipacionDocente WHERE IdOportunidad=@idOportunidad";
                var _queryRespuesta = _dapper.QueryDapper(_query, new { idOportunidad });
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("[]"))
                {
                    historialParticipacionDetalle = JsonConvert.DeserializeObject<List<AgendaDocenteHistorialParticipacionDetalle>>(_queryRespuesta);
                }

                List<PEspecificoHistorialParticipacionDocenteDTO> historialParticipacion = new List<PEspecificoHistorialParticipacionDocenteDTO>();

                historialParticipacion = (from p in historialParticipacionDetalle
                                          group p by new
                                          {
                                              p.IdOportunidadHijo,
                                              p.Programa,
                                              p.Curso,
                                              p.Modalidad,
                                              p.FechaInicio,
                                              p.FechaTermino,
                                              p.Ciudad,
                                              p.EstadoP
                                          } into g
                                          select new PEspecificoHistorialParticipacionDocenteDTO
                                          {
                                              IdOportunidadHijo = g.Key.IdOportunidadHijo,
                                              Programa = g.Key.Programa,
                                              Curso = g.Key.Curso,
                                              Modalidad = g.Key.Modalidad,
                                              FechaInicio = g.Key.FechaInicio,
                                              FechaTermino = g.Key.FechaTermino,
                                              Ciudad = g.Key.Ciudad,
                                              EstadoP = g.Key.EstadoP,

                                              listaActividadDetalle = g.Select(o => new AgendaDocenteActividadDetalleDTO
                                              {
                                                  Actividad = o.Actividad,
                                                  FechaProgramada = o.FechaProgramada,
                                                  FechaReal = o.FechaReal,
                                                  Comentario = o.Comentario

                                              }).OrderByDescending(o => o.FechaProgramada).ToList()
                                          }).ToList();

                return historialParticipacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el periodo duracion del programa especifico
        /// </summary>
        /// <param name="id">Id del PEspecifico del que se desea obtener la duracion (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena del periodo y duracion</returns>
        public string ObtenerPeriodoDuracion(int id, int idMatriculaCabecera)
        {
            try
            {
                var resultadoFinal = new PeriodoDuracionProgramaEspecificoDTO();
                var query = $@"ope.SP_ObtenerDuracionProgramaEspecifico";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdPEspecifico = id, IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<PeriodoDuracionProgramaEspecificoDTO>(resultado);
                }

                var htmlFinal = "<span>";

                htmlFinal += $@"
                                     <strong>Fecha de Inicio: </strong> { resultadoFinal.FechaInicio.ToString("dd/MM/yyyy") }
                                     <br/>
                                     <strong>Duración total: </strong> { resultadoFinal.DuracionTotalAproximadaMeses } Meses Aprox.
                                     <br/>
                                     <strong>Fecha de Culminación: </strong> { resultadoFinal.FechaTermino.ToString("dd/MM/yyyy") }
                                     <br/>
                                     <strong>Fecha aproximada de certificación: </strong> { resultadoFinal.FechaAproximadaCertificacion.ToString("dd/MM/yyyy") }
                     ";

                htmlFinal += "</span>";

                return htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las sesiones por cursos del programa especifico
        /// </summary>
        /// <param name="id">Id del PEspecifico del cual se desea averiguar las sesiones (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Cadena formateada con el conjunto de sesiones del curso mencionado</returns>
        public string ObtenerConjuntoSesion(int id)
        {
            try
            {
                var resultadoFinal = new List<ConjuntoSesionProgramaEspecificoDTO>();
                var query = $@"ope.SP_ObtenerConjuntoSesionProgramaEspecifico";
                var resultado = _dapper.QuerySPDapper(query, new { IdPEspecifico = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<ConjuntoSesionProgramaEspecificoDTO>>(resultado);
                }


                var resultadoAgrupado = resultadoFinal.GroupBy(x => new { x.IdPEspecifico, x.NombrePEspecifico })
                    .Select(y => new ConjuntoSesionProgramaEspecificoMaestroDTO
                    {
                        IdPEspecifico = y.Key.IdPEspecifico,
                        NombrePEspecifico = y.Key.NombrePEspecifico,
                        Sesiones = y.Select(w => new ConjuntoSesionProgramaEspecificoDetalleDTO()
                        {
                            DuracionSesionHoras = w.DuracionSesionHoras,
                            FechaSesion = w.FechaSesion,
                            HorarioSesion = w.HorarioSesion
                        }).ToList()
                    });

                var htmlFinal = "";
                foreach (var item in resultadoAgrupado)
                {
                    htmlFinal += $@"<p>Curso: {item.NombrePEspecifico}</p>
									";
                    foreach (var sesion in item.Sesiones)
                    {
                        htmlFinal += $@"
										 <p>
										 Fecha: { sesion.FechaSesion.ToString("dd/MM/yyyy") }
										 <br/>
										 Horarios: { sesion.HorarioSesion }
										 <br/>
										 Duración: { sesion.DuracionSesionHoras } horas
										 <br/>
										 </p>
						 ";
                    }

                    htmlFinal += $@"</br>";
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
        /// Obtiene todas las sesiones por cursos del programa especifico que tienen una sesion en base a la fecha actual + cantidad de dias dentro de la semana actual
        /// </summary>
        /// <param name="id">Id del PEspecifico que se desea saber su proximo conjunto de sesiones (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el momento de la consulta</param>
        /// <returns>Cadena formateada con el proximo conjunto de sesion</returns>
        public string ObtenerProximoConjuntoSesion(int id, int cantidadDias)
        {
            try
            {
                var resultadoFinal = new List<ConjuntoSesionProgramaEspecificoDTO>();
                var query = $@"ope.SP_ObtenerProximoConjuntoSesionProgramaEspecifico";
                var resultado = _dapper.QuerySPDapper(query, new { IdPEspecifico = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<ConjuntoSesionProgramaEspecificoDTO>>(resultado);
                }

                var resultadoAgrupado = resultadoFinal.GroupBy(x => new { x.IdPEspecifico, x.NombrePEspecifico })
                    .Select(y => new ConjuntoSesionProgramaEspecificoMaestroDTO
                    {
                        IdPEspecifico = y.Key.IdPEspecifico,
                        NombrePEspecifico = y.Key.NombrePEspecifico,
                        Sesiones = y.Select(w => new ConjuntoSesionProgramaEspecificoDetalleDTO()
                        {
                            DuracionSesionHoras = w.DuracionSesionHoras,
                            FechaSesion = w.FechaSesion,
                            HorarioSesion = w.HorarioSesion
                        }).ToList()
                    });

                var htmlFinal = "";
                foreach (var item in resultadoAgrupado)
                {
                    htmlFinal += $@"<p><strong>Curso:</strong> {item.NombrePEspecifico}</p>
									";
                    foreach (var sesion in item.Sesiones)
                    {
                        htmlFinal += $@"
										 <p>
										 <strong>Fecha:</strong> { sesion.FechaSesion.ToString("dd/MM/yyyy") }
										 <br/>
										 <strong>Horarios:</strong> { sesion.HorarioSesion }
										 <br/>
										 <strong>Duración:</strong> { sesion.DuracionSesionHoras } horas
                                         <br/>
										 </p>
						 ";
                    }

                    htmlFinal += $@"</br>";
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
        /// Obtiene la url de acceso a la sesion online
        /// </summary>
        /// <param name="id">Id del PEspecifico del cual se desae obtener la URL de acceso a la sesion (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Cadena con la URL de la sesion online</returns>
        public string ObtenerUrlAccesoSesionOnline(int id)
        {
            try
            {
                var resultadoFinal = new ValorStringDTO();
                var query = $@"ope.SP_ObtenerUrlAccesoSesionOnline";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdPEspecifico = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }

                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el detalle de un programa especifico por matricula cabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns>Objetoo de tipo PEspecificoValorDTO</returns>
        public PEspecificoValorDTO ObtenerDetalle(int idMatriculaCabecera)
        {
            try
            {
                var resultadoFinal = new PEspecificoValorDTO();
                var query = $@"ope.SP_ObtenerDetallePEspecifico";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<PEspecificoValorDTO>(resultado);
                }

                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el calendario semanal en bytes
        /// </summary>
        /// <param name="programasEspecificos"></param>
        /// <returns></returns>
        public byte[] ObtenerCalendarioSemanal(List<PEspecificoSesionDetalleDTO> listaSesiones)
        {
            try
            {
                byte[] recordatorioCalendario;
                MemoryStream ms = new MemoryStream();

                StreamWriter sb = new StreamWriter(ms);
                sb.WriteLine("BEGIN:VCALENDAR");
                sb.WriteLine("VERSION:2.0");
                sb.WriteLine("PRODID:-//Microsoft Corporation//Outlook 11.0 MIMEDIR//EN");
                sb.WriteLine("METHOD:PUBLISH");

                sb.WriteLine("X-WR-CALNAME:BSG Institute - Sesiones");
                sb.WriteLine("X-WR-TIMEZONE:America/Lima");
                sb.WriteLine("X-WR-CALDESC:Calendario de Sesiones de BSG Institute");

                foreach (var sesion in listaSesiones)
                {
                    sb.WriteLine("BEGIN:VEVENT");

                    sb.WriteLine("UID:" + System.Guid.NewGuid().ToString());

                    sb.WriteLine("ORGANIZER:BSGInstitute");

                    sb.WriteLine("DTSTAMP:" + sesion.FechaHoraInicio.Value.ToUniversalTime().ToString("yyyyMMddTHHmmssZ")); //la misma fecha del evento
                    sb.WriteLine("DTSTART:" + sesion.FechaHoraInicio.Value.ToUniversalTime().ToString("yyyyMMddTHHmmssZ"));
                    sb.WriteLine("DTEND:" + sesion.FechaHoraFin.Value.ToUniversalTime().ToString("yyyyMMddTHHmmssZ"));

                    sb.WriteLine("SUMMARY;LANGUAGE=es-pe:Sesión de Clases - BSG Institute");
                    sb.WriteLine("LOCATION:BSG Institute");

                    sb.WriteLine("DESCRIPTION;LANGUAGE=es-pe:Diplomado: " + sesion.NombrePEspecifico + " \\nCurso: " + sesion.NombreCurso + "\\nFecha: " + sesion.FechaHoraInicio.Value.ToString("dd-MM-yyyy") + "\\nHorario: " + sesion.FechaHoraInicio.Value.ToString("HH:mm") + " a " + sesion.FechaHoraFin.Value.ToString("HH:mm") + " horas");

                    sb.WriteLine("BEGIN:VALARM");
                    sb.WriteLine("TRIGGER:-PT60M");
                    sb.WriteLine("ACTION:DISPLAY");
                    sb.WriteLine("DESCRIPTION:Reminder");
                    sb.WriteLine("END:VALARM");

                    sb.WriteLine("END:VEVENT");
                }

                sb.WriteLine("END:VCALENDAR");

                //cierra el stream
                sb.Close();

                recordatorioCalendario = ms.ToArray();

                //cierra el stream
                ms.Close();

                return recordatorioCalendario;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PEspecificoRelacionadoPorIdPGeneralDTO> ObtenerPEspecificoRelacionadoPorIdPGeneral(int idPEspecifico, int idMatriculaCabecera)
        {
            try
            {
                List<PEspecificoRelacionadoPorIdPGeneralDTO> lista = new List<PEspecificoRelacionadoPorIdPGeneralDTO>();
                var _query = "SELECT IdPEspecifico, Nombre FROM ope.V_ObtenerPEspecifico_Relacionado WHERE Id = @idPEspecifico AND IdPEspecifico NOT IN (SELECT IdPEspecifico FROM OPE.T_PEspecificoMatriculaAlumno WHERE IdMatriculaCabecera = @idMatriculaCabecera )";
                var _queryRespuesta = _dapper.QueryDapper(_query, new { idPEspecifico, idMatriculaCabecera });
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PEspecificoRelacionadoPorIdPGeneralDTO>>(_queryRespuesta);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 04-23-2021
        /// Version: 1.0
        /// <summary>
        /// Devulve todas las sesiones totalizado por IdPEspecifico
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <returns>Objeto</returns>
        public List<PEspecificoCronogramaGrupalGrupoDTO> ObtenerPEspecificoCronogramaGrupal(int idPEspecifico)
        {
            try
            {
                List<PEspecificoCronogramaGrupalDTO> lista = new List<PEspecificoCronogramaGrupalDTO>();
                var query = "SELECT Id,IdPEspecifico, FechaHoraInicio, Duracion, DuracionTotal, Curso, Tipo, Grupo FROM pla.V_ObtenerPEspecifico_CronogramaGrupal WHERE Id = @idPEspecifico";
                var queryRespuesta = _dapper.QueryDapper(query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PEspecificoCronogramaGrupalDTO>>(queryRespuesta);
                }

                var resultadoAgrupado = lista.GroupBy(x => new { x.Grupo })
                    .Select(y => new PEspecificoCronogramaGrupalGrupoDTO
                    {
                        Grupo = y.Key.Grupo,
                        lista = y.Select(w => new PEspecificoCronogramaGrupalDTO()
                        {
                            Id = w.Id,
                            IdPespecifico =w.IdPespecifico,
                            FechaHoraInicio = w.FechaHoraInicio,
                            Duracion = w.Duracion,
                            DuracionTotal = w.DuracionTotal,
                            Curso = w.Curso,
                            Tipo = w.Tipo,
                            Grupo = w.Grupo
                        }).OrderBy(x => x.FechaHoraInicio).ToList()
                    }).ToList();

                return resultadoAgrupado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la url de queja sugerencia por n dias n horas (Actualmente se ha visto que la funcion esta vacia)
        /// </summary>
        /// <param name="id">Id aun no determinado</param>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena vacia</returns>
        public string ObtenerFechaEmisionUltimoCertificado(int id, int idMatriculaCabecera)
        {
            try
            {
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la url de queja sugerencia por n dias n horas (Actualmente se ha visto que la funcion esta vacia)
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias segun la fecha actual</param>
        /// <param name="cantidadHoras">Cantidad de horas segun la fecha actual</param>
        /// <returns>Cadena vacia</returns>
        public string ObtenerUrlQuejaSugerenciaNDiasNHora(int idMatriculaCabecera, int cantidadDias, int cantidadHoras)
        {
            try
            {
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la url de queja sugerencia por n dias n horas (Actualmente se ha visto que la funcion esta vacia)
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias segun la fecha actual</param>
        /// <param name="cantidadHoras">Cantidad de horas segun la fecha actual</param>
        /// <returns>Cadena vacia</returns>
        public string ObtenerNombreCursoEncuestaNDiasNHora(int idMatriculaCabecera, int cantidadDias, int cantidadHoras)
        {
            try
            {
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la url de queja sugerencia por n dias n horas (Actualmente se ha visto que la funcion esta vacia)
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias segun la fecha actual</param>
        /// <param name="cantidadHoras">Cantidad de horas segun la fecha actual</param>
        /// <returns>Cadena vacia</returns>
        public string ObtenerUrlEncuestaNDiasNHora(int idMatriculaCabecera, int cantidadDias, int cantidadHoras)
        {
            try
            {
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene todos los grupos sesiones por programa especifico
        /// </summary>
        /// <returns></returns>
        public List<GrupoPEspecificoDTO> ObtenerGrupoSesiones()
        {
            try
            {
                var lista = new List<GrupoPEspecificoDTO>();
                var query = $@"
                       SELECT DISTINCT 
                               Id, 
                               Nombre, 
                               IdPEspecifico
                        FROM pla.V_TPespecificoSesion_ObtenerNumeroGrupo
                        WHERE ESTADO = 1
                ";
                var resultadoDB = _dapper.QueryDapper(query, new { });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<GrupoPEspecificoDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los grupos sesiones por programa especifico
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerPadres()
        {
            try
            {
                var lista = new List<FiltroDTO>();
                //var query = $@"
                //     SELECT Id, 
                //           Nombre
                //    FROM pla.V_ObtenerPEspecificoPadre
                //    WHERE EstadoPEspecifico = 1;
                //";
                var query = $@"
                    SELECT IdPEspecifico AS Id, 
                           ProgramaEspecifico AS Nombre
                    FROM pla.V_TPEspecifico_ClasificacionProgramaEspecifico
                    WHERE Estado = 1
                          AND RowNumber = 1
                          AND Tipo IN(0, 1)
                    AND IdModalidad != 1
                    ORDER BY Nombre ASC;
                ";

                var resultadoDB = _dapper.QueryDapper(query, new { });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<FiltroDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos programas especificos hijos
        /// </summary>
        /// <returns></returns>
        public List<PEspecificoHijoPEspecificoPadreDTO> ObtenerHijos()
        {
            try
            {
                var lista = new List<PEspecificoHijoPEspecificoPadreDTO>();
                //var query = $@"
                //   SELECT Id, 
                //           Nombre, 
                //           IdPEspecificoPadre
                //   FROM pla.V_ObtenerPEspecificoHijo
                //";
                var query = $@"
                   SELECT DISTINCT 
                           Id, 
                           Nombre, 
                           IdPEspecificoPadre
                    FROM pla.V_TPEspecifico_ObtenerPEspecificosHijosGrupo
                    WHERE Estado = 1
                          AND RowNumber = 1;
                ";
                var resultadoDB = _dapper.QueryDapper(query, new { });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PEspecificoHijoPEspecificoPadreDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene todos programas especificos hijos
        /// </summary>
        /// <returns></returns>
        public List<PEspecificoSesionGrupoDTO> ObtenerPorProgramaEspecificoGrupo(int id, int idGrupo)
        {
            try
            {
                var lista = new List<PEspecificoSesionGrupoDTO>();
                var query = $@"
                        SELECT Id, 
                               FechaHoraInicio, 
                               Curso, 
                               IdExpositor, 
                               IdPEspecificoHijo, 
                               Tipo, 
                               Comentario, 
                               IdPEspecificoPadre, 
                               IdGrupo
                        FROM pla.V_ObtenerCronogramaSesiones
                        WHERE EstadoPEspecificoSesion = 1
                              AND EstadoPEspecificoPadrePEspecificoHijo = 1
                              AND EstadoPEspecifico = 1
                              AND IdPEspecificoHijo = @id
                              AND IdGrupo = @idGrupo;
                ";
                var resultadoDB = _dapper.QueryDapper(query, new { id, idGrupo });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PEspecificoSesionGrupoDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los grupos de edicion disponibles por programa especifico
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Lista de objetos de clase FiltroDTO</returns>
        public List<FiltroDTO> ObtenerGrupoEdicionDisponible(int idPEspecifico)
        {
            try
            {
                var lista = new List<FiltroDTO>();
                var query = $@"
                                SELECT  Id, 
                                        Nombre
                                FROM ope.V_ObtenerGrupoEdicionDisponiblePorPEspecifico
                                WHERE IdPEspecifico = @idPEspecifico
                                        OR IdPEspecifico IS NULL
                                ORDER BY Id ASC;
                                ";
                var resultadoDB = _dapper.QueryDapper(query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<FiltroDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene lista de programas especificos padre mediante filtros
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ProgramaEspecificoMaterialDTO> ObtenerProgramasEspecificoFiltros(ProgramaEspecificoMaterialFiltroDTO filtro)
        {
            try
            {
                if (string.IsNullOrEmpty(filtro.IdProgramaEspecifico))
                {
                    filtro.IdProgramaEspecifico = "";
                }
                if (string.IsNullOrEmpty(filtro.IdCentroCosto))
                {
                    filtro.IdCentroCosto = "";
                }
                if (string.IsNullOrEmpty(filtro.CodigoBs))
                {
                    filtro.CodigoBs = "";
                }
                if (string.IsNullOrEmpty(filtro.IdEstadoPEspecifico))
                {
                    filtro.IdEstadoPEspecifico = "";
                }
                if (string.IsNullOrEmpty(filtro.IdModalidadCurso))
                {
                    filtro.IdModalidadCurso = "";
                }
                if (string.IsNullOrEmpty(filtro.IdPGeneral))
                {
                    filtro.IdPGeneral = "";
                }
                if (string.IsNullOrEmpty(filtro.IdArea))
                {
                    filtro.IdArea = "";
                }
                if (string.IsNullOrEmpty(filtro.IdSubArea))
                {
                    filtro.IdSubArea = "";
                }
                var query = "pla.SP_ProgramaEspecificoFiltro";
                var res = _dapper.QuerySPDapper(query, new { filtro.IdProgramaEspecifico, filtro.IdCentroCosto, filtro.CodigoBs, filtro.IdEstadoPEspecifico, filtro.IdModalidadCurso, filtro.IdPGeneral, filtro.IdArea, filtro.IdSubArea });
                var rpta = JsonConvert.DeserializeObject<List<ProgramaEspecificoMaterialDTO>>(res);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene lista de programas especificos padre
        /// </summary>
        /// <returns></returns>
        public List<PEspecificoProgramaGeneralFiltroDTO> ObtenerProgramasEspecificosPadres(int? tipo)
        {
            try
            {
                var query = "";
                if (tipo.HasValue)
                {
                    query = $@"
                            SELECT DISTINCT 
                                   IdPEspecifico AS Id, 
                                   PEspecifico AS Nombre, 
                                   IdPGeneral AS IdPGeneral
                            FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
                            WHERE Estado = 1
                                  AND RowNumber = 1
								  AND Tipo = 1;
                            ";
                }
                else
                {
                    query = $@"
                            SELECT DISTINCT 
                                   IdPEspecifico AS Id, 
                                   PEspecifico AS Nombre, 
                                   IdPGeneral AS IdPGeneral
                            FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
                            WHERE Estado = 1
                                  AND RowNumber = 1
                            ";
                }

                var res = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<PEspecificoProgramaGeneralFiltroDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene lista de programas especificos padre para rl filtro por expositor
        /// </summary>
        /// <returns></returns>
        public List<PEspecificoProgramaGeneralFiltroV2DTO> ObtenerProgramasEspecificosPadresV2(int? tipo)
        {
            try
            {
                var query = "";
                if (tipo.HasValue)
                {
                    query = $@"
                            SELECT DISTINCT 
                                    IdPEspecifico AS Id, 
                                    PEspecifico AS Nombre, 
                                    IdPGeneral AS IdPGeneral,
		                            T_PEspecifico.IdEstadoPEspecifico,
		                            T_ModalidadCurso.Id AS IdModalidad,
		                            T_RegionCiudad.CodigoBS AS IdCodigoBSCiudad
                            FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
                            JOIN pla.T_PEspecifico ON T_PEspecifico.Id = V_TPEspecifico_ObtenerProgramasParaFiltro.IdPEspecifico
                            JOIN pla.T_ModalidadCurso ON T_PEspecifico.TipoId = T_ModalidadCurso.Id
                            JOIN conf.T_RegionCiudad ON (T_PEspecifico.Ciudad = T_RegionCiudad.DenominacionBS AND T_RegionCiudad.Estado = 1)
                            WHERE V_TPEspecifico_ObtenerProgramasParaFiltro.Estado = 1
                                    AND RowNumber = 1
		                            AND V_TPEspecifico_ObtenerProgramasParaFiltro.Tipo = 1;
                            ";
                }
                else
                {
                    query = $@"
                            SELECT DISTINCT 
                                    IdPEspecifico AS Id, 
                                    PEspecifico AS Nombre, 
                                    IdPGeneral AS IdPGeneral,
		                            T_PEspecifico.IdEstadoPEspecifico,
		                            T_ModalidadCurso.Id AS IdModalidad,
		                            T_RegionCiudad.CodigoBS AS IdCodigoBSCiudad
                            FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
                            JOIN pla.T_PEspecifico ON T_PEspecifico.Id = V_TPEspecifico_ObtenerProgramasParaFiltro.IdPEspecifico
                            JOIN pla.T_ModalidadCurso ON T_PEspecifico.TipoId = T_ModalidadCurso.Id
                            JOIN conf.T_RegionCiudad ON (T_PEspecifico.Ciudad = T_RegionCiudad.DenominacionBS AND T_RegionCiudad.Estado = 1)
                            WHERE V_TPEspecifico_ObtenerProgramasParaFiltro.Estado = 1
                                    AND RowNumber = 1
                            ";
                }

                var res = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<PEspecificoProgramaGeneralFiltroV2DTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene lista de programas especificos padre mediante filtros
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<DatosListaPespecificoDTO> ObtenerProgramasEspecificoFiltrosPadreIndividual(ProgramaEspecificoMaterialFiltroDTO filtro)
        {
            try
            {
                var query = "pla.SP_ProgramaEspecificoPadreIndividualFiltro";
                var res = _dapper.QuerySPDapper(query, new { filtro.IdProgramaEspecifico, filtro.IdCentroCosto, filtro.CodigoBs, filtro.IdEstadoPEspecifico, filtro.IdModalidadCurso, filtro.IdPGeneral, filtro.IdArea, filtro.IdSubArea, filtro.IdCentroCostoD });
                var rpta = JsonConvert.DeserializeObject<List<DatosListaPespecificoDTO>>(res);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene programas especificos hijos, grupo de cronograma
        /// </summary>
        /// <param name="IdPEspecificoPadre"></param>
        /// <returns></returns>
        public List<PEspecificoGrupoDTO> ObtenerPEspecificoGruposPorPEspecificoPadre()
        {
            try
            {
                var query = "SELECT DISTINCT Id, Nombre, IdPEspecificoPadre FROM [pla].[V_TPEspecifico_ObtenerPEspecificosHijosGrupo] WHERE Estado = 1 AND RowNumber = 1";
                var res = _dapper.QueryDapper(query, null);
                var rpta = JsonConvert.DeserializeObject<List<PEspecificoGrupoDTO>>(res);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id,Nombre de los centros de costo por un parametro
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerProgramaEspecificoPadreAutoComplete(string valor)
        {
            try
            {
                List<FiltroDTO> programaEspecificoPadreFiltro = new List<FiltroDTO>();
                string _queryCentroCostoFiltro = string.Empty;
                _queryCentroCostoFiltro = "SELECT IdPEspecifico AS Id, ProgramaEspecifico AS Nombre from [pla].[V_TPEspecifico_ClasificacionProgramaEspecifico] WHERE ProgramaEspecifico LIKE CONCAT('%',@valor,'%') AND Estado = 1 AND RowNumber = 1 AND Tipo IN (0,1) AND IdModalidad != 1 ORDER By Nombre ASC";
                var pespecificoDB = _dapper.QueryDapper(_queryCentroCostoFiltro, new { valor });
                if (!string.IsNullOrEmpty(pespecificoDB) && !pespecificoDB.Contains("[]"))
                {
                    programaEspecificoPadreFiltro = JsonConvert.DeserializeObject<List<FiltroDTO>>(pespecificoDB);
                }
                return programaEspecificoPadreFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<FiltroDTO> ObtenerEstadoEntregaMaterialAlumnoFiltro()
        {
            try
            {
                var query = "SELECT Id,Nombre FROM [ope].[V_TEstadoEntregaMaterialAlumnoFiltro] WHERE Estado = 1";
                var res = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PEspecificoHistorialParticipacionDocenteV2DTO> ObtenerHistorialParticipacionV2(int idClasificacionPersona)
        {
            try
            {
                List<PEspecificoHistorialParticipacionDocenteV2DTO> listado =
                    new List<PEspecificoHistorialParticipacionDocenteV2DTO>();
                var _query =
                    "SELECT PGeneral, IdPEspecifico, PEspecifico, Modalidad, Ciudad, IdExpositor, EstadoParticipacion, IdClasificacionPersona, FechaInicio, FechaTermino  FROM pla.V_ObtenerPEspecifico_HistorialParticipacionDocenteV2 WHERE IdClasificacionPersona=@idClasificacionPersona";
                var _queryRespuesta = _dapper.QueryDapper(_query, new { idClasificacionPersona });
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<PEspecificoHistorialParticipacionDocenteV2DTO>>(_queryRespuesta);
                }

                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<PEspecificoHistorialParticipacionDocenteV3DTO> ObtenerHistorialParticipacionV3()
        {
            try
            {
                List<PEspecificoHistorialParticipacionDocenteV3DTO> listado =
                    new List<PEspecificoHistorialParticipacionDocenteV3DTO>();
                var _query =
                    "SELECT Anho, Id, PEspecificoPadre, EstadoPrograma, IdPEspecifico, PEspecifico, EstadoCurso, Modalidad, ModalidadPrograma, Ciudad, Orden, Grupo, EstadoParticipacion, ExpositorPlanificacion, ExpositorV3, ExpositorConfirmado, IdExpositorConfirmado, IdProveedorFur, ProveedorFur, FechaInicio, FechaTermino, EsNotaAprobada, EsAsistenciaAprobada, IdProveedorPlanificacionGrupo, IdProveedorOperacionesGrupoConfirmado, IdCentroCostoPrograma, CentroCostoPrograma, AplicaCierreAsistencia FROM pla.V_ObtenerPEspecifico_HistorialParticipacionDocenteV3";
                var _queryRespuesta = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<PEspecificoHistorialParticipacionDocenteV3DTO>>(_queryRespuesta);
                }

                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PEspecificoHistorialParticipacionDocenteV3DTO> ObtenerHistorialParticipacionV3(int idClasificacionPersona)
        {
            try
            {
                List<PEspecificoHistorialParticipacionDocenteV3DTO> listado =
                    new List<PEspecificoHistorialParticipacionDocenteV3DTO>();
                var _query =
                    "SELECT Anho, Id, PEspecificoPadre, EstadoPrograma, IdPEspecifico, PEspecifico, EstadoCurso, Modalidad, ModalidadPrograma, Ciudad, Orden, Grupo, EstadoParticipacion, ExpositorPlanificacion, ExpositorV3, ExpositorConfirmado, IdExpositorConfirmado, IdProveedorFur, ProveedorFur, FechaInicio, FechaTermino, EsNotaAprobada, EsAsistenciaAprobada, IdProveedorPlanificacionGrupo, IdProveedorOperacionesGrupoConfirmado, IdCentroCostoPrograma, CentroCostoPrograma, AplicaCierreAsistencia FROM pla.V_ObtenerPEspecifico_HistorialParticipacionDocenteV3 WHERE IdClasificacionPersonaPlanificacion = @idClasificacionPersona OR IdClasificacionPersonaV3 = @idClasificacionPersona OR IdClasificacionPersonaConfirmado = @idClasificacionPersona";
                var _queryRespuesta = _dapper.QueryDapper(_query, new { idClasificacionPersona });
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<PEspecificoHistorialParticipacionDocenteV3DTO>>(_queryRespuesta);
                }

                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PEspecificoHistorialParticipacionDocenteV3DTO> ObtenerHistorialParticipacionV3PorExpositorPortal(int idExpositor)
        {
            try
            {
                List<PEspecificoHistorialParticipacionDocenteV3DTO> listado =
                    new List<PEspecificoHistorialParticipacionDocenteV3DTO>();
                var _query =
                    "SELECT Anho, Id, PEspecificoPadre, EstadoPrograma, IdPEspecifico, PEspecifico, EstadoCurso, Modalidad, ModalidadPrograma, Ciudad, Orden, Grupo, EstadoParticipacion, ExpositorPlanificacion, ExpositorV3, ExpositorConfirmado, IdExpositorConfirmado, IdProveedorFur, ProveedorFur, FechaInicio, FechaTermino, EsNotaAprobada, EsAsistenciaAprobada, IdProveedorPlanificacionGrupo, IdProveedorOperacionesGrupoConfirmado, IdCentroCostoPrograma, CentroCostoPrograma, AplicaCierreAsistencia FROM pla.V_ObtenerPEspecifico_HistorialParticipacionDocenteV3 WHERE IdExpositorConfirmado = @idExpositor";
                var _queryRespuesta = _dapper.QueryDapper(_query, new { idExpositor });
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<PEspecificoHistorialParticipacionDocenteV3DTO>>(_queryRespuesta);
                }

                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PEspecificoHistorialParticipacionDocenteV3DTO> ObtenerHistorialParticipacionV3PorProveedorPortal(int idProveedor)
        {
            try
            {
                List<PEspecificoHistorialParticipacionDocenteV3DTO> listado =
                    new List<PEspecificoHistorialParticipacionDocenteV3DTO>();
                var _query =
                    "SELECT Anho, Id, PEspecificoPadre, EstadoPrograma, IdPEspecifico, PEspecifico, EstadoCurso, Modalidad, ModalidadPrograma, Ciudad, Orden, Grupo, EstadoParticipacion, ExpositorPlanificacion, ExpositorV3, ExpositorConfirmado, IdExpositorConfirmado, IdProveedorFur, ProveedorFur, FechaInicio, FechaTermino, EsNotaAprobada, EsAsistenciaAprobada, IdProveedorPlanificacionGrupo, IdProveedorOperacionesGrupoConfirmado, IdCentroCostoPrograma, CentroCostoPrograma, AplicaCierreAsistencia FROM pla.V_ObtenerPEspecifico_HistorialParticipacionDocenteV3 WHERE IdProveedorOperacionesGrupoConfirmado = @idProveedor";
                var _queryRespuesta = _dapper.QueryDapper(_query, new { idProveedor });
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<PEspecificoHistorialParticipacionDocenteV3DTO>>(_queryRespuesta);
                }

                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PEspecificoHistorialParticipacionDocentePortalDTO> ObtenerHistorialParticipacionV3PorProveedorPortalAonline(int idProveedor)
        {
            try
            {
                List<PEspecificoHistorialParticipacionDocentePortalDTO> listado =
                    new List<PEspecificoHistorialParticipacionDocentePortalDTO>();
                var _query =
                    "SELECT Anho, Id, PEspecificoPadre, PEspecificoPadre, IdEstadoPEspecificoPadre, EstadoPrograma, IdModalidadPrograma, ModalidadPrograma, IdPEspecifico," +
                    " PEspecifico, IdEstadoCur, IdAreaCapacitacion, IdSubAreaCapacitacion, IdProveedor FROM " +
                    "pla.V_ObtenerPEspecificoAonline_HistorialParticipacionDocente WHERE IdProveedor = @idProveedor";
                var _queryRespuesta = _dapper.QueryDapper(_query, new { idProveedor });
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<PEspecificoHistorialParticipacionDocentePortalDTO>>(_queryRespuesta);
                }

                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ProgramaEspecificoAlumnosDTO> ObtenerAlumnosProgramaEspecifico(int IdPEspecifico)
        {
            try
            {
                List<ProgramaEspecificoAlumnosDTO> listado = new List<ProgramaEspecificoAlumnosDTO>();
                var listadoDB = _dapper.QuerySPDapper("pla.SP_ObtenerAlumnosPorIdPEspecifico", new { IdPEspecifico });
                if (!string.IsNullOrEmpty(listadoDB) && !listadoDB.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<ProgramaEspecificoAlumnosDTO>>(listadoDB);
                }
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la duracion del programa especifico
        /// </summary>
        /// <param name="idPespecifico">Id del PEspecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con la duracion del programa especifico</returns>
        public string ObtenerDuracionProgramaEspecifico(int idPespecifico, int idMatriculaCabecera)
        {
            try
            {
                ValorStringDTO valor = new ValorStringDTO();
                var query =
                    "SELECT Duracion AS Valor FROM pla.V_Pespecifico_Duracion WHERE Id=@IdPespecifico and IdMatriculaCabecera=@IdMatriculaCabecera";
                var queryRespuesta = _dapper.FirstOrDefault(query, new { IdPespecifico = idPespecifico, IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("null"))
                {
                    valor = JsonConvert.DeserializeObject<ValorStringDTO>(queryRespuesta);
                }

                return valor.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string ObtenerDuracionProgramaEspecificoModulo(int IdPespecifico, int IdMatriculaCabecera)
        {
            try
            {
                ValorStringDTO valor = new ValorStringDTO();
                var _query =
                    "SELECT Duracion AS Valor FROM pla.V_Pespecifico_DuracionModulo WHERE Id=@IdPespecifico and IdMatriculaCabecera=@IdMatriculaCabecera";
                var _queryRespuesta = _dapper.FirstOrDefault(_query, new { IdPespecifico, IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("null"))
                {
                    valor = JsonConvert.DeserializeObject<ValorStringDTO>(_queryRespuesta);
                }

                return valor.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string ObtenerDuracionProgramaGeneralVersion(int IdProgramaGeneral, int IdMatriculaCabecera)
        {
            try
            {
                ValorStringDTO valor = new ValorStringDTO();
                var _query =
                    "SELECT Duracion AS Valor FROM pla.V_PgeneralCursosHijosporVersion WHERE IdPgeneral=@IdProgramaGeneral and IdMatriculaCabecera=@IdMatriculaCabecera";
                var _queryRespuesta = _dapper.FirstOrDefault(_query, new { IdProgramaGeneral, IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("null"))
                {
                    valor = JsonConvert.DeserializeObject<ValorStringDTO>(_queryRespuesta);
                }

                return valor.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PEspecificoHistorialParticipacionDocenteV3DTO> ObtenerHistorialParticipacionV3_Filtrado(ParticipacionExpositorFiltroDTO filtro)
        {
            try
            {
                var query = "pla.SP_ObtenerHistorialParticipacionV3_Filtrado";
                var res = _dapper.QuerySPDapper(query,
                    new
                    {
                        filtro.IdExpositor,
                        filtro.IdProgramaEspecifico,
                        filtro.IdCentroCosto,
                        filtro.IdCodigoBSCiudad,
                        filtro.IdEstadoPEspecifico,
                        filtro.IdModalidadCurso,
                        filtro.IdPGeneral,
                        filtro.IdArea,
                        filtro.IdSubArea,
                        filtro.IdCentroCostoD,
                        filtro.IdProveedorPlanificacion,
                        filtro.IdProveedorOperaciones,
                        filtro.IdProveedorFur
                    });

                return JsonConvert.DeserializeObject<List<PEspecificoHistorialParticipacionDocenteV3DTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PEspecificoHistorialParticipacionDocenteV3DTO> ObtenerHistorialParticipacionV3Portal_Filtrado(ParticipacionExpositorFiltroDTO filtro)
        {
            try
            {
                var query = "pla.SP_ObtenerHistorialParticipacionV3Portal_Filtrado";
                var res = _dapper.QuerySPDapper(query,
                    new
                    {
                        filtro.IdExpositor,
                        filtro.IdProgramaEspecifico,
                        filtro.IdCentroCosto,
                        filtro.IdCodigoBSCiudad,
                        filtro.IdEstadoPEspecifico,
                        filtro.IdModalidadCurso,
                        filtro.IdPGeneral,
                        filtro.IdArea,
                        filtro.IdSubArea,
                        filtro.IdCentroCostoD,
                        filtro.IdProveedorPlanificacion,
                        filtro.IdProveedorOperaciones,
                        filtro.IdProveedorFur,

                        filtro.SinNotaAprobada,
                        filtro.SinAsistenciaAprobada
                    });

                return JsonConvert.DeserializeObject<List<PEspecificoHistorialParticipacionDocenteV3DTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ProgramaEspecificoHijoFiltroDTO> ObtenerProgramaEspecificoHijos()
        {
            List<ProgramaEspecificoHijoFiltroDTO> rpta = new List<ProgramaEspecificoHijoFiltroDTO>();
            string _query = "Select Id,Nombre,IdProgramaGeneral From pla.V_ObtenerPEspecificoHijoFiltro ";
            string query = _dapper.QueryDapper(_query, null);
            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<ProgramaEspecificoHijoFiltroDTO>>(query);
            }
            return rpta;
        }
        public List<EstadoPespecificoDTO> ObtenerPespecificosActualizar()
        {
            List<EstadoPespecificoDTO> rpta = new List<EstadoPespecificoDTO>();
            string _query = "Select IdPespecifico,IdEstado From pla.V_PespecifoCambiarEstado where IdEstado Is not null";
            string query = _dapper.QueryDapper(_query, null);
            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<EstadoPespecificoDTO>>(query);
            }
            return rpta;
        }

        /// <summary>
		/// Obtiene lista Programas especificos Online de tipo Webinar Recurrente
		/// </summary>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerListaProgramasOnline()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM [pla].[V_TListaPEspecificos_Online_WebinarRecurrente] WHERE Estado = 1";
                var res = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ReporteAmbienteDTO> ObtenerExcelReporteAmbiente(string IdProgramaEspecifico, string IdCentroCosto, string CodigoBs, string IdEstadoPEspecifico, string IdModalidadCurso, string IdPGeneral, string IdArea, string IdSubArea, int? IdCentroCostoD)
        {
            try
            {
                if (IdProgramaEspecifico == "null")
                {
                    IdProgramaEspecifico = "";
                }

                if (IdCentroCosto == "null")
                {
                    IdCentroCosto = "";
                }

                if (CodigoBs == "null")
                {
                    CodigoBs = "";
                }

                if (IdEstadoPEspecifico == "null")
                {
                    IdEstadoPEspecifico = "";
                }

                if (IdModalidadCurso == "null")
                {
                    IdModalidadCurso = "";
                }

                if (IdPGeneral == "null")
                {
                    IdPGeneral = "";
                }

                if (IdArea == "null")
                {
                    IdArea = "";
                }

                if (IdSubArea == "null")
                {
                    IdSubArea = "";
                }
                var query = "pla.SP_GenerarReporteAmbiente";
                var res = _dapper.QuerySPDapper(query, new { IdProgramaEspecifico, IdCentroCosto, CodigoBs, IdEstadoPEspecifico, IdModalidadCurso, IdPGeneral, IdArea, IdSubArea });
                var rpta = JsonConvert.DeserializeObject<List<ReporteAmbienteDTO>>(res);

                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FiltroDTO> ObtenerListaPEspecificoWebinar()
        {
            try
            {
                var query = "SELECT Id, Nombre,Modalidad,Codigo FROM [pla].[V_TListaPEspecificos_Relacionados] WHERE Estado = 1 Order by Id desc";
                var res = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int ObtenerCentroCostoporProgramaHijo(int IdPgeneral, int IdMatriculaCabecera)
        {
            try
            {
                ValorIntDTO CentroCosto = new ValorIntDTO();
                var _query = string.Empty;
                _query = "SELECT IdCentroCosto as Valor  FROM pla.V_PgeneralCentroCostoporMatricula WHERE IdProgramaGeneral = @IdPgeneral AND IdMatriculaCabecera = @IdMatriculaCabecera ";
                var CentroCostoDB = _dapper.FirstOrDefault(_query, new { IdPgeneral, IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(CentroCostoDB) && !CentroCostoDB.Contains("null"))
                {
                    CentroCosto = JsonConvert.DeserializeObject<ValorIntDTO>(CentroCostoDB);
                }

                return CentroCosto.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int ObtenerCentroCostoporProgramaEspecifico(int IdPespecifico)
        {
            try
            {
                ValorIntDTO CentroCosto = new ValorIntDTO();
                var _query = string.Empty;
                _query = "SELECT IdCentroCosto as Valor  From pla.V_ObtenerRegistroPespecifico Where Estado=1 and Id=@IdPespecifico";
                var CentroCostoDB = _dapper.FirstOrDefault(_query, new { IdPespecifico });
                if (!string.IsNullOrEmpty(CentroCostoDB) && !CentroCostoDB.Contains("null"))
                {
                    CentroCosto = JsonConvert.DeserializeObject<ValorIntDTO>(CentroCostoDB);
                }

                return CentroCosto.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene los programas especificos con sus programas generales respectivos
		/// </summary>
		/// <returns>Lista de objetos de tipo PEspecificoProgramaGeneralFiltroDTO con los programas especificos y sus programas generales</returns>
		public List<PEspecificoProgramaGeneralFiltroDTO> ObtenerPEspecificoProgramaGeneral()
        {
            try
            {
                return this.GetBy(x => x.Estado == true).Select(x => new PEspecificoProgramaGeneralFiltroDTO { Id = x.Id, IdPGeneral = x.IdProgramaGeneral, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PespecificoFiltroDTO> ObtenerPEspecificoFiltro()
        {
            try
            {
                List<PespecificoFiltroDTO> pEspecifico = new List<PespecificoFiltroDTO>();
                string _queryPEspecifico = "Select Id,Nombre,IdProgramaGeneral From pla.V_PEspecificoFiltro";
                var _programaEspecifico = _dapper.QueryDapper(_queryPEspecifico, null);
                if (!string.IsNullOrEmpty(_programaEspecifico) && !_programaEspecifico.Equals("null"))
                {
                    pEspecifico = JsonConvert.DeserializeObject<List<PespecificoFiltroDTO>>(_programaEspecifico);
                }
                return pEspecifico;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        //Tipo de PEspecifico
        public PEspecificoTipoDTO getTipoPEspecifico(int idPEspecifico)
        {
            try
            {
                PEspecificoTipoDTO listado = new PEspecificoTipoDTO();
                var query = "SELECT Tipo From [pla].[V_PEspecificoTipo] WHERE Id = @IdPespecifico";
                var res = _dapper.FirstOrDefault(query, new { idPEspecifico });
                listado = JsonConvert.DeserializeObject<PEspecificoTipoDTO>(res);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        //Nombre de PEspecifico
        public string getNombrePEspecifico(int idPEspecifico)
        {
            try
            {
                ValorStringDTO listado = new ValorStringDTO();
                var query = "SELECT Valor From [pla].[V_PEspecificoNombre] WHERE Id = @IdPespecifico";
                var res = _dapper.FirstOrDefault(query, new { idPEspecifico });
                listado = JsonConvert.DeserializeObject<ValorStringDTO>(res);
                return listado.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PespecificoFiltroDTO> ObtenerPEspecificoNuevaAulaVirtual()
        {
            try
            {
                List<PespecificoFiltroDTO> pEspecificoNuevaAulaVirtual = new List<PespecificoFiltroDTO>();
                string _queryPEspecifico = @"SELECT Id,
                                                    Nombre,
                                                    IdProgramaGeneral
                                                    FROM pla.V_TPEspecificoNuevoAulaVirtual_DataBasica";

                var _programaEspecifico = _dapper.QueryDapper(_queryPEspecifico, null);
                if (!string.IsNullOrEmpty(_programaEspecifico) && !_programaEspecifico.Equals("null"))
                {
                    pEspecificoNuevaAulaVirtual = JsonConvert.DeserializeObject<List<PespecificoFiltroDTO>>(_programaEspecifico);
                }
                return pEspecificoNuevaAulaVirtual;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<SesionesOnlineWebinarDocenteDTO> ObtenerPEspecificoPorProveedor(int idProveedor)
        {
            try
            {
                List<SesionesOnlineWebinarDocenteDTO> listado = new List<SesionesOnlineWebinarDocenteDTO>();
                string _query = "SELECT DISTINCT IdPEspecificoPadre, PEspecificoPadre FROM pla.V_ObtenerSesionesOnlineWebinarDocente WHERE IdProveedor = @idProveedor";
                var query = _dapper.QueryDapper(_query, new { idProveedor });
                listado = JsonConvert.DeserializeObject<List<SesionesOnlineWebinarDocenteDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ComboGenericoDTO> ObtenerPEspecificoPorProveedor(int idProveedor, int IdPGeneral)
        {
            try
            {
                List<ComboGenericoDTO> listado = new List<ComboGenericoDTO>();
                string _query = "SELECT DISTINCT IdPEspecificoPadre as Id, PEspecificoPadre AS Nombre " +
                    "FROM pla.V_ObtenerSesionesOnlineWebinarDocente WHERE IdProveedor = @idProveedor and IdPGeneral=@IdPGeneral";
                var query = _dapper.QueryDapper(_query, new { IdProveedor = idProveedor, IdPGeneral = IdPGeneral });
                listado = JsonConvert.DeserializeObject<List<ComboGenericoDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el programa especifico y su webinar segun el id del programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Lista de objetos de clase ComboGenericoDTO</returns>
        public List<ComboGenericoDTO> ObtenerPEspecificoWebinar(int idPGeneral)
        {
            try
            {
                List<ComboGenericoDTO> listado = new List<ComboGenericoDTO>();
                string query = "SELECT Id, Nombre " +
                    "FROM pla.V_TPEspecifico_Webinar WHERE IdPGeneral=@IdPGeneral";
                var resultadoDB = _dapper.QueryDapper(query, new { IdPGeneral = idPGeneral });
                listado = JsonConvert.DeserializeObject<List<ComboGenericoDTO>>(resultadoDB);

                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ComboGenericoDTO> ObtenerPEspecificoHijoPorProveedor(int idProveedor, int IdPGeneral, int IdPEspecificoPadre)
        {
            try
            {
                List<ComboGenericoDTO> listado = new List<ComboGenericoDTO>();
                string _query = "SELECT DISTINCT IdPEspecificoHijo as Id, CursoNombre AS Nombre " +
                    "FROM pla.V_ObtenerSesionesOnlineWebinarDocente " +
                    "WHERE IdProveedor = @idProveedor and IdPGeneral=@IdPGeneral and IdPEspecificoPadre=@IdPEspecificoPadre";
                var query = _dapper.QueryDapper(_query, new { IdProveedor = idProveedor, IdPGeneral = IdPGeneral, IdPEspecificoPadre = IdPEspecificoPadre });
                listado = JsonConvert.DeserializeObject<List<ComboGenericoDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<SesionesOnlineWebinarDocenteDTO> ListadoSesionesPorDocenteFiltrado(SesionesOnlineWebinarDocenteDTO filtro)
        {
            try
            {
                var idProveedor = filtro.IdProveedor;
                var IdPGeneral = filtro.IdPGeneral;
                var IdPEspecificoPadre = filtro.IdPEspecificoPadre;
                var IdPEspecificoHijo = filtro.IdPEspecificoHijo;
                var Tipo = filtro.Tipo;
                List<SesionesOnlineWebinarDocenteDTO> listado = new List<SesionesOnlineWebinarDocenteDTO>();
                string _query = "SELECT * " +
                    "FROM pla.V_ObtenerSesionesOnlineWebinarDocente WHERE IdProveedor = @idProveedor ";
                if (IdPGeneral != 0)
                    _query += " and IdPGeneral=@IdPGeneral";
                if (IdPEspecificoPadre != 0)
                    _query += " and IdPEspecificoPadre=@IdPEspecificoPadre";
                if (IdPEspecificoHijo != 0)
                    _query += " and IdPEspecificoHijo=@IdPEspecificoHijo";
                if (Tipo != null)
                    _query += " and Tipo=@Tipo";
                var query = _dapper.QueryDapper(_query, filtro);
                listado = JsonConvert.DeserializeObject<List<SesionesOnlineWebinarDocenteDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<DetalleSesionesAlumnosDTO> DetalleSesionesPorAlumnosFiltrado(DetalleSesionesAlumnosDTO filtro)
        {
            try
            {
                var IdPGeneral = filtro.IdPGeneral;
                var IdPEspecifico = filtro.IdPEspecifico;
                var IdSesion = filtro.IdSesion;
                var IdMatriculaCabecera = filtro.IdMatriculaCabecera;
                var CodigoMatricula = filtro.CodigoMatricula;

                List<DetalleSesionesAlumnosDTO> listado = new List<DetalleSesionesAlumnosDTO>();
                string _query = "SELECT IdPGeneral, IdPEspecifico, IdSesion, IdCoordinadoraAcademica, NombreCoordinadoraAcademica, IdMatriculaCabecera, CodigoMatricula, NombreAlumno, CentroCosto, EstadoMatricula, Confirmo " +
                    "FROM pla.V_ObtenerDetalleSesionAlumnosWebinar WHERE IdSesion = @IdSesion ";
                if (IdPGeneral != 0)
                    _query += " and IdPGeneral=@IdPGeneral";
                if (IdPEspecifico != 0)
                    _query += " and IdPEspecifico=@IdPEspecifico";
                if (CodigoMatricula != null)
                    _query += " or CodigoMatricula=@CodigoMatricula";
                _query += " ORDER BY Confirmo DESC";
                var query = _dapper.QueryDapper(_query, filtro);
                listado = JsonConvert.DeserializeObject<List<DetalleSesionesAlumnosDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PEspecificoRelacionadoPorIdPGeneralDTO> ObtenerPEspecificoRelacionadoPGeneral(int idPEspecifico, int idMatriculaCabecera)
        {
            try
            {
                List<PEspecificoRelacionadoPorIdPGeneralDTO> lista = new List<PEspecificoRelacionadoPorIdPGeneralDTO>();
                var _query = "SELECT IdPEspecifico, Nombre FROM ope.V_ObtenerPEspecifico_Relacionado_PGeneral WHERE Id = @idPEspecifico AND IdPEspecifico NOT IN (SELECT IdPEspecifico FROM OPE.T_PEspecificoMatriculaAlumno WHERE IdMatriculaCabecera = @idMatriculaCabecera )";
                var _queryRespuesta = _dapper.QueryDapper(_query, new { idPEspecifico, idMatriculaCabecera });
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PEspecificoRelacionadoPorIdPGeneralDTO>>(_queryRespuesta);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /* public List<PEspecificoRelacionadoPorIdPGeneralDTO> ObtenerPEspecificoWebinar(int idPGeneral)
         {
             try
             {
                 List<PEspecificoRelacionadoPorIdPGeneralDTO> lista = new List<PEspecificoRelacionadoPorIdPGeneralDTO>();
                 var _query = "SELECT IdPEspecifico, Nombre FROM ope.V_ObtenerPEspecifico_Relacionado_PGeneral WHERE Id = @idPEspecifico AND IdPEspecifico NOT IN (SELECT IdPEspecifico FROM OPE.T_PEspecificoMatriculaAlumno WHERE IdMatriculaCabecera = @idMatriculaCabecera )";
                 var _queryRespuesta = _dapper.QueryDapper(_query, new { idPGeneral});
                 if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("[]"))
                 {
                     lista = JsonConvert.DeserializeObject<List<PEspecificoRelacionadoPorIdPGeneralDTO>>(_queryRespuesta);
                 }

                 return lista;
             }
             catch (Exception e)
             {
                 throw new Exception(e.Message);
             }
         }*/

        public DatosConfiguracionProgramasWebexDTO ObtenerConfiguracionWebinarPEspecifico(int idPEspecifico)
        {
            try
            {
                DatosConfiguracionProgramasWebexDTO lista = new DatosConfiguracionProgramasWebexDTO();
                var _query = "Select IdPEspecifico,IdTiempoFrecuencia,Valor,IdTiempoFrecuenciaCorreo,ValorFrecuenciaCorreo,IdPlantillaFrecuenciaCorreo,IdTiempoFrecuenciaWhatsapp,ValorFrecuenciaWhatsapp,IdPlantillaFrecuenciaWhatsapp,IdTiempoFrecuenciaCorreoConfirmacion,ValorFrecuenciaCorreoConfirmacion,IdPlantillaCorreoConfirmacion,IdTiempoFrecuenciaCorreoDocente,ValorFrecuenciaDocente,IdPlantillaDocente,FechaInicio,FechaFin,IdFrecuencia from pla.V_ObtenerConfiguracionWebinarPEspecifico where IdPEspecifico=@idPEspecifico AND Estado = 1";
                var _queryRespuesta = _dapper.FirstOrDefault(_query, new { IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<DatosConfiguracionProgramasWebexDTO>(_queryRespuesta);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene cursos relacionados de irca por programa especifico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="esCursoDSig"></param>
        /// <returns></returns>
        public List<PEspecificoRelacionadoPorIdPGeneralDTO> ObtenerPEspecificoRelacionadoIrca(int idPEspecifico, int idMatriculaCabecera, bool esCursoDSig)
        {
            try
            {
                List<PEspecificoRelacionadoPorIdPGeneralDTO> lista = new List<PEspecificoRelacionadoPorIdPGeneralDTO>();
                var _query = "ope.SP_ObtenerPEspecifico_RelacionadoIrca";
                var _queryRespuesta = _dapper.QuerySPDapper(_query, new { IdPespecifico = idPEspecifico, IdMatriculaCabecera = idMatriculaCabecera, EsProgramaDSIG = esCursoDSig });
                if (!string.IsNullOrEmpty(_queryRespuesta) && !_queryRespuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PEspecificoRelacionadoPorIdPGeneralDTO>>(_queryRespuesta);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 03-23-2021
        /// Version: 1.0
        /// <summary>
        /// Permite obterner el programa Especifico de un Alumno
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id Matricula Cabecera </param>
        /// <returns>Objeto</returns> 
        public PEspecificoAlumnoDTO ObtenerPespecificoTipoAlumno(int idMatriculaCabecera)
        {
            try
            {
                string query = "Select Id,Nombre,Tipo,Estado from pla.V_ObtenerPespecificoTipoAlumno Where IdMatriculaCabecera = @idMatriculaCabecera";
                var queryResultado = _dapper.FirstOrDefault(query, new { idMatriculaCabecera });
                return JsonConvert.DeserializeObject<PEspecificoAlumnoDTO>(queryResultado);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene cursos relacionados de irca por programa especifico
        /// </summary>
        /// <returns>Lista de objetos de tipo PEspecificoNuevoAulaVirtualDTO</returns>
        public List<PEspecificoNuevoAulaVirtualDTO> ObtenerPEspecificoNuevoAulaVirtualTipo()
        {
            try
            {
                var listaResultado = new List<PEspecificoNuevoAulaVirtualDTO>();
                var query = @"SELECT IdPEspecifico,
                                NombrePEspecifico,
		                        IdCentroCosto,
		                        EstadoP,
		                        Modalidad,
		                        IdPGeneral,
		                        Ciudad,
		                        IdCursoMoodle,
		                        IdCursoMoodlePrueba,
		                        TipoPEspecifico,
                                IdPEspecificoHijo,
                                NombrePEspecificoHijo
                            FROM pla.V_PEspecificoNuevoAulaVirtualTipoPadreHijoIndividual";
                var queryRespuesta = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    listaResultado = JsonConvert.DeserializeObject<List<PEspecificoNuevoAulaVirtualDTO>>(queryRespuesta);
                }

                return listaResultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene cursos relacionados de irca por programa especifico
        /// </summary>
        /// <returns>Lista de objetos de tipo PEspecificoNuevoAulaVirtualDTO</returns>
        public List<PEspecificoNuevoAulaVirtualDTO> ObtenerPEspecificoPersonalNuevoAulaVirtualTipo()
        {
            try
            {
                var listaResultado = new List<PEspecificoNuevoAulaVirtualDTO>();
                var query = @"SELECT IdPEspecifico,
                                NombrePEspecifico,
		                        IdCentroCosto,
		                        EstadoP,
		                        Modalidad,
		                        IdPGeneral,
		                        Ciudad,
		                        IdCursoMoodle,
		                        IdCursoMoodlePrueba,
		                        TipoPEspecifico,
                                IdPEspecificoHijo,
                                NombrePEspecificoHijo
                            FROM pla.V_PEspecificoNuevoAulaVirtualSinRestriccionTipoPadreHijoIndividual";
                var queryRespuesta = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    listaResultado = JsonConvert.DeserializeObject<List<PEspecificoNuevoAulaVirtualDTO>>(queryRespuesta);
                }

                return listaResultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id y NombreCompleto de Pespecifico filtrado por el idPegeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<PEspecificoFiltroPGeneralDTO> ObtenerProgramaEspecificoPorIdPGeneral(int idPGeneral)
        {
            try
            {
                List<PEspecificoFiltroPGeneralDTO> obtenerPEspecificoPorIdPGeneral = new List<PEspecificoFiltroPGeneralDTO>();
                var _query = "SELECT Id, Nombre as NombreCompleto from [pla].[V_PEspecificoInformacion]  WHERE IdPGeneral = @idPGeneral";
                var obtenerPEspecificoIdPGeneralDB = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(obtenerPEspecificoIdPGeneralDB) && !obtenerPEspecificoIdPGeneralDB.Contains("[]"))
                {
                    obtenerPEspecificoPorIdPGeneral = JsonConvert.DeserializeObject<List<PEspecificoFiltroPGeneralDTO>>(obtenerPEspecificoIdPGeneralDB);
                }
                return obtenerPEspecificoPorIdPGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<PEspecificoFiltroPGeneralDTO> ObtenerProgramaEspecifico()
        {
            try
            {
                List<PEspecificoFiltroPGeneralDTO> obtenerPEspecifico = new List<PEspecificoFiltroPGeneralDTO>();
                var _query = "SELECT Id, Nombre as NombreCompleto from [pla].[V_PEspecificoInformacion]";
                var obtenerPEspecificoData = _dapper.QueryDapper(_query, "");
                if (!string.IsNullOrEmpty(obtenerPEspecificoData) && !obtenerPEspecificoData.Contains("[]"))
                {
                    obtenerPEspecifico = JsonConvert.DeserializeObject<List<PEspecificoFiltroPGeneralDTO>>(obtenerPEspecificoData);
                }
                return obtenerPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el Id,Nombre de los programas especificos por el valor
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerProgramaEspecificoAutocomplete(string valor)
        {
            try
            {
                List<FiltroDTO> programaEspecificoFiltro = new List<FiltroDTO>();
                string _queryCentroCostoFiltro = string.Empty;
                _queryCentroCostoFiltro = "SELECT Id,Nombre from pla.T_PEspecifico  WHERE Nombre LIKE CONCAT('%',@valor,'%') AND Estado = 1";
                var pespecificoDB = _dapper.QueryDapper(_queryCentroCostoFiltro, new { valor });
                if (!string.IsNullOrEmpty(pespecificoDB) && !pespecificoDB.Contains("[]"))
                {
                    programaEspecificoFiltro = JsonConvert.DeserializeObject<List<FiltroDTO>>(pespecificoDB);
                }
                return programaEspecificoFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
