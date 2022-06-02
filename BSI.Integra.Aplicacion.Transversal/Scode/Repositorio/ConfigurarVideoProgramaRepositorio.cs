using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Planificacion/ConfigurarVideoPrograma
    /// Autor: Priscila Pacsi - Jorge Rivera - Gian Miranda
    /// Fecha: 22/02/2021
    /// <summary>
    /// Repositorio para consultas de pla.T_ConfigurarVideoPrograma
    /// </summary>
    public class ConfigurarVideoProgramaRepositorio : BaseRepository<TConfigurarVideoPrograma, ConfigurarVideoProgramaBO>
    {
        #region Metodos Base
        public ConfigurarVideoProgramaRepositorio() : base()
        {
        }
        public ConfigurarVideoProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfigurarVideoProgramaBO> GetBy(Expression<Func<TConfigurarVideoPrograma, bool>> filter)
        {
            IEnumerable<TConfigurarVideoPrograma> listado = base.GetBy(filter);
            List<ConfigurarVideoProgramaBO> listadoBO = new List<ConfigurarVideoProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                ConfigurarVideoProgramaBO objetoBO = Mapper.Map<TConfigurarVideoPrograma, ConfigurarVideoProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfigurarVideoProgramaBO FirstById(int id)
        {
            try
            {
                TConfigurarVideoPrograma entidad = base.FirstById(id);
                ConfigurarVideoProgramaBO objetoBO = new ConfigurarVideoProgramaBO();
                Mapper.Map<TConfigurarVideoPrograma, ConfigurarVideoProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfigurarVideoProgramaBO FirstBy(Expression<Func<TConfigurarVideoPrograma, bool>> filter)
        {
            try
            {
                TConfigurarVideoPrograma entidad = base.FirstBy(filter);
                ConfigurarVideoProgramaBO objetoBO = Mapper.Map<TConfigurarVideoPrograma, ConfigurarVideoProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfigurarVideoProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfigurarVideoPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfigurarVideoProgramaBO> listadoBO)
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

        public bool Update(ConfigurarVideoProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfigurarVideoPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfigurarVideoProgramaBO> listadoBO)
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
        private void AsignacionId(TConfigurarVideoPrograma entidad, ConfigurarVideoProgramaBO objetoBO)
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

        private TConfigurarVideoPrograma MapeoEntidad(ConfigurarVideoProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfigurarVideoPrograma entidad = new TConfigurarVideoPrograma();
                entidad = Mapper.Map<ConfigurarVideoProgramaBO, TConfigurarVideoPrograma>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.SesionConfigurarVideo != null && objetoBO.SesionConfigurarVideo.Count > 0)
                {
                    foreach (var hijo in objetoBO.SesionConfigurarVideo)
                    {
                        TSesionConfigurarVideo entidadHijo = new TSesionConfigurarVideo();
                        entidadHijo = Mapper.Map<SesionConfigurarVideoBO, TSesionConfigurarVideo>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSesionConfigurarVideo.Add(entidadHijo);
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
        /// Obtener Filtros por capitulo
        /// </summary>
        /// <returns>Retorna una lista de objetos (PreEstructuraCapituloProgramaBO)</returns>
        public List<PreEstructuraCapituloProgramaBO> ObtenerCapituloProgramaFiltro()
        {
            try
            {
                List<PreEstructuraCapituloProgramaBO> capitulosFiltro = new List<PreEstructuraCapituloProgramaBO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var queryfiltrocapitulo = "Select distinct Contenido,NroCapitulo FROM pla.V_ListadoEstructuraProgramaCapitulosV2";
                var subfiltroCapitulo = _dapper.QueryDapper(queryfiltrocapitulo, new { });
                if (!string.IsNullOrEmpty(subfiltroCapitulo) && !subfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<PreEstructuraCapituloProgramaBO>>(subfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtener Filtros por sesion
        /// </summary>
        /// <returns>Retorna una lista de objetos (PreEstructuraCapituloProgramaBO)</returns>
        public List<PreEstructuraCapituloProgramaBO> ObtenerSesionProgramaFiltro()
        {
            try
            {
                List<PreEstructuraCapituloProgramaBO> sesionFiltro = new List<PreEstructuraCapituloProgramaBO>();
                var queryfiltrosesion = "Select IdPGeneral,Contenido,NumeroFila FROM pla.V_ListadoEstructuraPrograma WHERE NombreTitulo = 'Sesion'";
                var subfiltroSesion = _dapper.QueryDapper(queryfiltrosesion, new { });
                if (!string.IsNullOrEmpty(subfiltroSesion) && !subfiltroSesion.Contains("[]"))
                {
                    sesionFiltro = JsonConvert.DeserializeObject<List<PreEstructuraCapituloProgramaBO>>(subfiltroSesion);
                }
                return sesionFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Retorna una lista de objetos (PreEstructuraCapituloProgramaBO)</returns>
        public List<PreEstructuraCapituloProgramaBO> ObtenerPreConfigurarVideoPrograma(int idPGeneral)
        {
            List<PreEstructuraCapituloProgramaBO> rpta = new List<PreEstructuraCapituloProgramaBO>();
            //string query = "SELECT Id, IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, IdSeccionTipoDetalle_PW, NumeroFila FROM pla.V_ListadoEstructuraPrograma WHERE IdPGeneral = @IdPGeneral";
            string query = "SELECT Id,IdConfigurarVideoPrograma, IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, IdSeccionTipoDetalle_PW, NumeroFila, VideoId,Archivo,NroDiapositivas,ConImagenVideo,ImagenVideoNombre,ImagenVideoAncho,ImagenVideoAlto," +
                "ConImagenDiapositiva,ImagenDiapositivaNombre,ImagenDiapositivaAncho,ImagenDiapositivaAlto,ImagenVideoPosicionX,ImagenVideoPosicionY,ImagenDiapositivaPosicionX,ImagenDiapositivaPosicionY ,Minuto ,IdTipoVista," +
                "NroDiapositiva,ConLogoVideo,ConLogoDiapositiva,TotalSegundos " +
                "FROM pla.V_ListadoEstructuraPrograma WHERE IdPGeneral=@IdPGeneral ORDER BY NumeroFila, IdSeccionTipoDetalle_PW";
            string queryDB = _dapper.QueryDapper(query, new { IdPGeneral = idPGeneral });
            if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<PreEstructuraCapituloProgramaBO>>(queryDB);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Valida si el programa general es de tipo padre para indicar que se asigne proyecto de aplicacion
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Retorna una lista de objetos (PreEstructuraCapituloProgramaBO)</returns>
        public bool ValidarPRogramaPadreParaProyectoAPlicacion(int idPGeneral)
        {
            List<PreEstructuraCapituloProgramaBO> rpta = new List<PreEstructuraCapituloProgramaBO>();
            //string query = "SELECT Id, IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, IdSeccionTipoDetalle_PW, NumeroFila FROM pla.V_ListadoEstructuraPrograma WHERE IdPGeneral = @IdPGeneral";
            string query = "SELECT IdPGeneral, TieneProyectoDeAplicacion, IdTipoPrograma FROM pla.V_ValidarProgramaGeneralProyectoAplicacion WHERE IdPGeneral=@IdPGeneral";
            string queryDB = _dapper.QueryDapper(query, new { IdPGeneral = idPGeneral });
            // retorna vacio o nulo significa que no es programa padre
            if (string.IsNullOrEmpty(queryDB) && queryDB.Contains("[]"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Retorna una lista de objetos (PreEstructuraCapituloProgramaBO)</returns>
        public List<PreEstructuraCapituloProgramaBO> ObtenerPreConfigurarVideoProgramaDescarga(int idPGeneral)
        {
            List<PreEstructuraCapituloProgramaBO> rpta = new List<PreEstructuraCapituloProgramaBO>();
            //string query = "SELECT Id, IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, IdSeccionTipoDetalle_PW, NumeroFila FROM pla.V_ListadoEstructuraPrograma WHERE IdPGeneral = @IdPGeneral";
            string query = "SELECT Id,IdConfigurarVideoPrograma, IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, IdSeccionTipoDetalle_PW, NumeroFila, VideoId,Archivo,NroDiapositivas,ConImagenVideo,ImagenVideoNombre,ImagenVideoAncho,ImagenVideoAlto," +
                "ConImagenDiapositiva,ImagenDiapositivaNombre,ImagenDiapositivaAncho,ImagenDiapositivaAlto,ImagenVideoPosicionX,ImagenVideoPosicionY,ImagenDiapositivaPosicionX,ImagenDiapositivaPosicionY ,Minuto ,IdTipoVista," +
                "NroDiapositiva,ConLogoVideo,ConLogoDiapositiva,TotalSegundos " +
                "FROM pla.V_ListadoEstructuraProgramaDescarga WHERE IdPGeneral=@IdPGeneral ORDER BY NumeroFila, IdSeccionTipoDetalle_PW";
            string queryDB = _dapper.QueryDapper(query, new { IdPGeneral = idPGeneral });
            if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<PreEstructuraCapituloProgramaBO>>(queryDB);
                return rpta;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Retorna una lista de objetos (PreEstructuraCapituloProgramaBO)</returns>
        public List<PreEstructuraCapituloProgramaBO> ObtenerPreConfigurarVideoProgramaDescargaSinDatos(int idPGeneral)
        {
            List<PreEstructuraCapituloProgramaBO> rpta = new List<PreEstructuraCapituloProgramaBO>();
            //string query = "SELECT Id, IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, IdSeccionTipoDetalle_PW, NumeroFila FROM pla.V_ListadoEstructuraPrograma WHERE IdPGeneral = @IdPGeneral";
            string query = "SELECT Id,IdConfigurarVideoPrograma, IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NumeroFila,NroDiapositivas,TotalSegundos" +
                " FROM pla.V_ListadoEstructuraProgramaSinDatos WHERE IdPGeneral=@IdPGeneral ORDER BY NumeroFila";
            string queryDB = _dapper.QueryDapper(query, new { IdPGeneral = idPGeneral });
            if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<PreEstructuraCapituloProgramaBO>>(queryDB);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public List<PreEstructuraCapituloProgramaBO> ObtenerPreConfigurarVideoProgramaEvaluaciones(int IdPGeneral, int NumeroFila)
        {
            List<PreEstructuraCapituloProgramaBO> rpta = new List<PreEstructuraCapituloProgramaBO>();
            //string _query = "Select Id,IdPGeneral,Nombre,Titulo,Contenido,NombreTitulo,IdSeccionTipoDetalle_PW,NumeroFila From pla.V_ListadoEstructuraPrograma Where IdPGeneral=@IdPGeneral";
            string _query = "Select Id,IdPGeneral,Nombre,Titulo,Contenido,NombreTitulo,IdSeccionTipoDetalle_PW,NumeroFila,TotalSegundos From pla.V_EvaluacionTrabajoEstructuraPorPrograma Where IdPGeneral=@IdPGeneral AND NumeroFila=@NumeroFila";
            string query = _dapper.QueryDapper(_query, new { IdPGeneral = IdPGeneral, NumeroFila = NumeroFila });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<PreEstructuraCapituloProgramaBO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idDocumentoSeccionPw">Id de la seccion del documento (PK de la tabla pla.T_DocumentoSeccion_PW)</param>
        /// <param name="numeroFila">Id de la seccion del documento (PK de la tabla pla.T_DocumentoSeccion_PW)</param>
        /// <returns>Retorna una lista de objetos (PreEstructuraCapituloProgramaBO)</returns>
        public RegistroVideoProgramaBO ObtenerConfigurarVideoPrograma(int idPGeneral, int idDocumentoSeccionPw, int numeroFila)
        {
            RegistroVideoProgramaBO rpta = new RegistroVideoProgramaBO();
            string query = "Select Id,IdPgeneral,IdDocumentoSeccionPw,VideoId,VideoIdBrightcove,TotalMinutos,Archivo,NroDiapositivas,Configurado,ISNULL(ConImagenVideo, 0) AS ConImagenVideo,ImagenVideoNombre,ISNULL(ImagenVideoAncho,0) AS ImagenVideoAncho, ISNULL(ImagenVideoAlto, 0) AS ImagenVideoAlto, ISNULL(ConImagenDiapositiva, 0) AS ConImagenDiapositiva, ImagenDiapositivaNombre, ISNULL(ImagenDiapositivaAncho, 0) AS ImagenDiapositivaAncho, ISNULL(ImagenDiapositivaAlto, 0) AS ImagenDiapositivaAlto" +
                ", ISNULL(ImagenVideoPosicionX, 0) AS ImagenVideoPosicionX, ISNULL(ImagenVideoPosicionY, 0) AS ImagenVideoPosicionY, ISNULL(ImagenDiapositivaPosicionX, 0) AS ImagenDiapositivaPosicionX, ISNULL(ImagenDiapositivaPosicionY, 0) AS ImagenDiapositivaPosicionY, NumeroFila From pla.T_ConfigurarVideoPrograma Where Estado=1 AND IdPGeneral=@IdPGeneral AND IdDocumentoSeccionPw=@IdDocumentoSeccionPw AND NumeroFila=@NumeroFila";
            string queryDb = _dapper.FirstOrDefault(query, new { IdPGeneral = idPGeneral, IdDocumentoSeccionPw = idDocumentoSeccionPw, NumeroFila = numeroFila });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<RegistroVideoProgramaBO>(queryDb);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public List<ListaTipoVistaVideoBO> ListaTipoVistaVideo()
        {
            List<ListaTipoVistaVideoBO> rpta = new List<ListaTipoVistaVideoBO>();
            string _query = "Select Id,Nombre From pla.T_TipoVista Where Estado=1";
            string query = _dapper.QueryDapper(_query, null);
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<ListaTipoVistaVideoBO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public ConultarProgramaPadreBO ConultarProgramaPadrePorIdPGeneral(int Id)
        {
            ConultarProgramaPadreBO rpta = new ConultarProgramaPadreBO();
            string _query = "Select Id,Nombre,NroHijos From pla.V_ConultarProgramaPadrePorIdPGeneral Where Id=@Id";
            string query = _dapper.FirstOrDefault(_query, new { Id = Id });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<ConultarProgramaPadreBO>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public List<ListaCursosPorProgramaBO> ListaCursosPorProgramaId(int Id)
        {
            try
            {
                List<ListaCursosPorProgramaBO> rpta = new List<ListaCursosPorProgramaBO>();
                string _query = "Select Id,Programa,IdHijo,Curso From pla.V_ListaCursosPorProgramaId Where Id=@Id";
                string query = _dapper.QueryDapper(_query, new { Id = Id });
                if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ListaCursosPorProgramaBO>>(query);
                    return rpta;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ListaCursosPorProgramaBO> RegistroCursoPorProgramaId(int Id)
        {
            try
            {
                List<ListaCursosPorProgramaBO> rpta = new List<ListaCursosPorProgramaBO>();
                string _query = "Select Id,Programa,IdHijo,Curso From pla.V_RegistroCursoPorProgramaId Where Id=@Id";
                string query = _dapper.QueryDapper(_query, new { Id = Id });
                if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ListaCursosPorProgramaBO>>(query);
                    return rpta;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ListaCapitulosEstructuraProgramaBO> ListaCapitulosEstructuraPrograma(int IdPGeneral)
        {
            List<ListaCapitulosEstructuraProgramaBO> rpta = new List<ListaCapitulosEstructuraProgramaBO>();
            string _query = "Select IdPGeneral,Nombre,Titulo,Contenido,NombreTitulo,NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral=@IdPGeneral";
            string query = _dapper.QueryDapper(_query, new { IdPGeneral = IdPGeneral });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<ListaCapitulosEstructuraProgramaBO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public configuracionPreVideoProgramaBO obtenerConfiguracionVideoSesion(int IdPGeneral, int Seccion, int Fila)
        {
            configuracionPreVideoProgramaBO rpta = new configuracionPreVideoProgramaBO();
            string _query = "Select Nombre,IdVideo,IdVideoBrightcove,Archivo,UltimaSesion,Ubicacion,ConImagenVideo,ImagenVideoNombre,ImagenVideoAncho,ImagenVideoAlto,ConImagenDiapositiva,ImagenDiapositivaNombre,ImagenDiapositivaAncho,ImagenDiapositivaAlto,Segundos,Cantidad,intervalo,ImagenVideoPosicionX,ImagenVideoPosicionY,ImagenDiapositivaPosicionX,ImagenDiapositivaPosicionY From pla.V_ObtenerConfiguracionVideoSesion Where IdPGeneral=@IdPGeneral AND Seccion=@Seccion AND Fila=@Fila ORDER BY FechaModificacion DESC";
            string query = _dapper.FirstOrDefault(_query, new { IdPGeneral = IdPGeneral, Seccion = Seccion, Fila = Fila });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<configuracionPreVideoProgramaBO>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public List<configuracionSecuenciaVideoBO> obtenerConfiguracionSecuenciaVideoSesion(int IdPGeneral, int Seccion, int Fila)
        {
            List<configuracionSecuenciaVideoBO> rpta = new List<configuracionSecuenciaVideoBO>();
            string _query = "Select NroDiapositiva,Tiempo,tipoVista,Evaluacion,UrlEvaluacion From pla.V_ObtenerConfiguracionSecuenciaVideoSesion Where IdPGeneral=@IdPGeneral AND Seccion=@Seccion AND Fila=@Fila";
            string query = _dapper.QueryDapper(_query, new { IdPGeneral = IdPGeneral, Seccion = Seccion, Fila = Fila });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<configuracionSecuenciaVideoBO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }
        public List<configuracionSecuenciaVideoBO> obtenerConfiguracionSecuenciaVideoSesion(int IdPGeneral, int Seccion, int Fila, int Capitulo)
        {
            List<configuracionSecuenciaVideoBO> rpta = new List<configuracionSecuenciaVideoBO>();
            //string _query = @"(Select NroDiapositiva,Tiempo=CAST(Tiempo AS int),tipoVista,Evaluacion,UrlEvaluacion, NombEvaluacion='', EstadoEval=0, MostrarEvalFin=0 From pla.V_ObtenerConfiguracionSecuenciaVideoSesion Where IdPGeneral=@IdPGeneral AND Seccion=@Seccion AND Fila=@Fila 
            //                union 
            //                Select NroDiapositiva,Tiempo=CAST(Segundos AS int),tipoVista=4,Evaluacion,UrlEvaluacion=GrupoPregunta, NombEvaluacion='Evaluacion', EstadoEval=0, MostrarEvalFin=0 From pla.V_ListadoGrupoPreguntaPorEstructura Where IdPgeneral=@IdPGeneral AND OrdenFilaCapitulo=@Capitulo AND OrdenFilaSesion=@Fila 
            //                union 
            //                select NroDiapositiva=0,Tiempo=Cast(ValorMarcador as int),tipoVista=4,Evaluacion=Id,UrlEvaluacion=CAST(Id as varchar), NombEvaluacion='Crucigrama', EstadoEval=0, MostrarEvalFin=0 from pla.T_CrucigramaProgramaCapacitacion where Estado=1 AND IdPgeneral=@IdPGeneral AND OrdenFilaCapitulo=@Capitulo AND OrdenFilaSesion=@Fila) 
            //                order by NroDiapositiva, NombEvaluacion";

            string _query = @"(Select NroDiapositiva,Tiempo=CAST(Tiempo AS int),tipoVista,Evaluacion,UrlEvaluacion, NombEvaluacion='', EstadoEval=0, MostrarEvalFin=0,QuitarOverlayVideo, QuitarOverlaySlide From pla.V_ObtenerConfiguracionSecuenciaVideoSesion Where IdPGeneral=@IdPGeneral AND Seccion=@Seccion AND Fila=@Fila 
                            union 
                            Select NroDiapositiva,Tiempo=CAST(Segundos AS int),tipoVista=4,Evaluacion,UrlEvaluacion=GrupoPregunta, NombEvaluacion='Evaluacion', EstadoEval=0, MostrarEvalFin=0, QuitarOverlayVideo=0, QuitarOverlaySlide=0 From pla.V_ListadoGrupoPreguntaPorEstructura Where IdPgeneral=@IdPGeneral AND OrdenFilaCapitulo=@Capitulo AND OrdenFilaSesion=@Fila ) 
                            order by NroDiapositiva, NombEvaluacion";

            string query = _dapper.QueryDapper(_query, new { IdPGeneral = IdPGeneral, Seccion = Seccion, Capitulo = Capitulo, Fila = Fila });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<configuracionSecuenciaVideoBO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public List<ListadoGrupoPreguntaPorEstructuraBO> obtenerConfiguracionGrupoPreguntasEstructura(int IdPGeneral, int Seccion, int Fila)
        {
            List<ListadoGrupoPreguntaPorEstructuraBO> rpta = new List<ListadoGrupoPreguntaPorEstructuraBO>();
            string _query = "Select IdPgeneral,GrupoPregunta,IdTipoVista,Segundos From pla.V_ListadoGrupoPreguntaPorEstructura Where IdPgeneral=@IdPGeneral AND OrdenFilaCapitulo=@Seccion AND OrdenFilaSesion=@Fila";
            string query = _dapper.QueryDapper(_query, new { IdPGeneral = IdPGeneral, Seccion = Seccion, Fila = Fila });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<ListadoGrupoPreguntaPorEstructuraBO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public registroFeedbackResultadoObtenidoBO obtenerFeedbackResultadoObtenido(int IdPGeneral, int IdSexo, int Puntaje)
        {
            registroFeedbackResultadoObtenidoBO rpta = new registroFeedbackResultadoObtenidoBO();
            //string _query = "Select IdPGeneral,IdSexo,Puntaje,NombreVideo From pla.V_FeedbackResultadoObtenido Where IdPGeneral=@IdPGeneral AND IdSexo=@IdSexo AND Puntaje<=@Puntaje AND @Puntaje>=Puntaje";
            string _query = "Select IdPGeneral,IdSexo,Puntaje,NombreVideo From pla.V_FeedbackResultadoObtenido Where IdPGeneral=@IdPGeneral AND IdSexo=@IdSexo AND Puntaje BETWEEN @Puntaje AND @Puntaje";

            string query = _dapper.FirstOrDefault(_query, new { IdPGeneral = IdPGeneral, IdSexo = IdSexo, Puntaje = Puntaje });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<registroFeedbackResultadoObtenidoBO>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public List<configuracionCriterioEvaluacionProgramaBO> obtenerConfiguracionSecuenciaVideoSesion(int IdPGeneral, int IdModalidadCurso)
        {
            List<configuracionCriterioEvaluacionProgramaBO> rpta = new List<configuracionCriterioEvaluacionProgramaBO>();
            string _query = "Select Id,Nombre,Porcentaje From pla.V_CriterioEvaluacionProgramaAulaVirtual Where IdPGeneral=@IdPGeneral AND IdModalidadCurso=@IdModalidadCurso";
            string query = _dapper.QueryDapper(_query, new { IdPGeneral = IdPGeneral, IdModalidadCurso = IdModalidadCurso });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<configuracionCriterioEvaluacionProgramaBO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public registroParametroEvaluacionDetalleBO obtenerParametroEvaluacionEscalaCalificacionDetalle(int IdPGeneral, string NombreEsquemaEvaluacionPGeneralDetalle, int Valor, string FechaMatricula)
        {
            registroParametroEvaluacionDetalleBO rpta = new registroParametroEvaluacionDetalleBO();
            string _query = "Select IdParametroEvaluacion,IdEscalaCalificacionDetalle,IdEsquemaEvaluacion,Ponderacion,IdEscalaCalificacion, IdEsquemaEvaluacionPGeneralDetalle From pla.V_ParametroEvaluacionEscalaCalificacionDetalle Where IdPGeneral=@IdPGeneral AND NombreEsquemaEvaluacionPGeneralDetalle=@NombreEsquemaEvaluacionPGeneralDetalle AND Valor=@Valor AND @FechaMatricula >= FechaInicio";
            string query = _dapper.FirstOrDefault(_query, new { IdPGeneral = IdPGeneral, NombreEsquemaEvaluacionPGeneralDetalle = NombreEsquemaEvaluacionPGeneralDetalle, Valor = Valor, FechaMatricula = FechaMatricula });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<registroParametroEvaluacionDetalleBO>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public registroParametroEvaluacionDetalleBO obtenerParametroEvaluacionEscalaProgramaGeneral(int IdPGeneral, string NombreEsquemaEvaluacionPGeneralDetalle, string FechaMatricula)
        {
            registroParametroEvaluacionDetalleBO rpta = new registroParametroEvaluacionDetalleBO();
            string _query = "Select IdParametroEvaluacion,IdEsquemaEvaluacion,Ponderacion,IdEscalaCalificacion, IdEsquemaEvaluacionPGeneralDetalle From pla.V_ParametroEvaluacionEscalaProgramaGeneral Where IdPGeneral=@IdPGeneral AND NombreEsquemaEvaluacionPGeneralDetalle=@NombreEsquemaEvaluacionPGeneralDetalle AND @FechaMatricula >= FechaInicio";
            string query = _dapper.FirstOrDefault(_query, new { IdPGeneral = IdPGeneral, NombreEsquemaEvaluacionPGeneralDetalle = NombreEsquemaEvaluacionPGeneralDetalle, FechaMatricula = FechaMatricula });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<registroParametroEvaluacionDetalleBO>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public registroParametroEscalaEvaluacionBO obtenerregistroParametroEscalaEvaluacion(int IdPGeneral, string NombreEsquemaEvaluacionPGeneralDetalle, string FechaMatricula)
        {
            registroParametroEscalaEvaluacionBO rpta = new registroParametroEscalaEvaluacionBO();
            string _query = "Select NombreEscalaCalificacion,Ponderacion,IdParametroEvaluacion,NombreCriterioEvaluacion,IdEscalaCalificacion,NombreParametroEvaluacion,IdEsquemaEvaluacionPGeneralDetalle From pla.V_ParametroEvaluacionEscalaProgramaGeneral Where IdPGeneral=@IdPGeneral AND NombreEsquemaEvaluacionPGeneralDetalle=@NombreEsquemaEvaluacionPGeneralDetalle AND FechaInicio <= @FechaMatricula";
            string query = _dapper.FirstOrDefault(_query, new { IdPGeneral = IdPGeneral, NombreEsquemaEvaluacionPGeneralDetalle = NombreEsquemaEvaluacionPGeneralDetalle, FechaMatricula = FechaMatricula });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<registroParametroEscalaEvaluacionBO>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public List<registroParametroEscalaEvaluacionDetalleBO> obtenerregistroParametroEscalaEvaluacionDetalle(int IdEscalaCalificacion)
        {
            List<registroParametroEscalaEvaluacionDetalleBO> rpta = new List<registroParametroEscalaEvaluacionDetalleBO>();
            string _query = "Select Id,Nombre,Valor From pla.T_EscalaCalificacionDetalle Where IdEscalaCalificacion=@IdEscalaCalificacion";
            string query = _dapper.QueryDapper(_query, new { IdEscalaCalificacion = IdEscalaCalificacion });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<registroParametroEscalaEvaluacionDetalleBO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public List<registroParametroEscalaEvaluacionBO> listaRegistroParametroEscalaEvaluacion(int IdPGeneral, string NombreEsquemaEvaluacionPGeneralDetalle, string FechaMatricula)
        {
            List<registroParametroEscalaEvaluacionBO> rpta = new List<registroParametroEscalaEvaluacionBO>();
            string _query = "Select NombreEscalaCalificacion,Ponderacion,IdParametroEvaluacion,NombreCriterioEvaluacion,IdEscalaCalificacion,NombreParametroEvaluacion,IdEsquemaEvaluacionPGeneralDetalle From pla.V_ParametroEvaluacionEscalaProgramaGeneral Where IdPGeneral=@IdPGeneral AND NombreEsquemaEvaluacionPGeneralDetalle=@NombreEsquemaEvaluacionPGeneralDetalle AND @FechaMatricula >= FechaInicio";
            string query = _dapper.QueryDapper(_query, new { IdPGeneral = IdPGeneral, NombreEsquemaEvaluacionPGeneralDetalle = NombreEsquemaEvaluacionPGeneralDetalle, FechaMatricula = FechaMatricula });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<registroParametroEscalaEvaluacionBO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public registroMatriculaCabeceraAlumnoAulaBO registroMatriculaCabeceraAlumno(int IdMatriculaCabecera)
        {
            registroMatriculaCabeceraAlumnoAulaBO rpta = new registroMatriculaCabeceraAlumnoAulaBO();
            string _query = "Select Id, CodigoMatricula,IdAlumno,IdPEspecifico,EstadoMatricula,FechaMatricula From fin.T_MatriculaCabecera Where Id=@IdMatriculaCabecera";
            string query = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera = IdMatriculaCabecera });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<registroMatriculaCabeceraAlumnoAulaBO>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public List<PreEstructuraCapituloProgramaBO> ObtenerPreConfigurarVideoProgramaEncuestas(int IdPGeneral, int NumeroFila)
        {
            List<PreEstructuraCapituloProgramaBO> rpta = new List<PreEstructuraCapituloProgramaBO>();
            //string _query = "Select Id,IdPGeneral,Nombre,Titulo,Contenido,NombreTitulo,IdSeccionTipoDetalle_PW,NumeroFila From pla.V_ListadoEstructuraPrograma Where IdPGeneral=@IdPGeneral";
            string _query = "Select Id,IdPGeneral,Nombre,Titulo,Contenido,NombreTitulo,IdSeccionTipoDetalle_PW,NumeroFila,TotalSegundos From pla.V_ExamenesEstructuraPorPrograma Where IdPGeneral=@IdPGeneral AND NumeroFila=@NumeroFila";
            string query = _dapper.QueryDapper(_query, new { IdPGeneral = IdPGeneral, NumeroFila = NumeroFila });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<PreEstructuraCapituloProgramaBO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        //

        public List<registroMatriculaPorProgramaGeneralBO> listaMatriculaPorProgramaGeneralRegistroActivo(int IdPGeneral, int TipoPrograma)
        {
            List<registroMatriculaPorProgramaGeneralBO> rpta = new List<registroMatriculaPorProgramaGeneralBO>();
            string _query = "Select IdMatriculaCabecera, CodigoMatricula,IdAlumno,IdPEspecifico,IdPGeneral,Tipo,TipoId From ope.V_MatriculaPorProgramaGeneralRegistroActivo Where IdPGeneral=@IdPGeneral AND TipoId=@TipoPrograma";
            string query = _dapper.QueryDapper(_query, new { IdPGeneral = IdPGeneral, TipoPrograma = TipoPrograma });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<registroMatriculaPorProgramaGeneralBO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public registroNotaPromedioPorMatriculaMatriculaBO listaNotasPromedioPorMatriculaMatricula(int IdMatriculaCabecera)
        {
            registroNotaPromedioPorMatriculaMatriculaBO rpta = new registroNotaPromedioPorMatriculaMatriculaBO();
            string _query = "Select IdMatriculaCabecera, Nota,EscalaCalificacion,NotaFinal,Numero,Promedio From ope.V_NotaPromedioPorMatriculaMatricula Where IdMatriculaCabecera=@IdMatriculaCabecera";
            string query = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera = IdMatriculaCabecera });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<registroNotaPromedioPorMatriculaMatriculaBO>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene la fecha de inicio de un programa especifico
        /// </summary>
        /// <param name="idProgramaGeneral"></param>        
        /// <returns></returns>
        public ResultadoFinalDTO EliminarConfiguracionVideo(int idProgramaGeneral)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("[pla].[SP_EliminarConfiguracionesVideo]", new { IdProgramaGeneral = idProgramaGeneral });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IdConfigurarVideoDTO> ObtenerIdConfigurarVideoNuevo(int IdProgramaGeneral)
        {
            try
            {
                var query = "Select Id from pla.T_ConfigurarVideoPrograma where IdPGeneral=@IdPGeneral AND Estado = 1";                
                var Lista = _dapper.QueryDapper(query, new { IdPGeneral= IdProgramaGeneral });
                return JsonConvert.DeserializeObject<List<IdConfigurarVideoDTO>>(Lista);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<SesionConfiguracionVideoDTO> ObtenerSesionConfigurarVideo(int IdConfigurarVideoPrograma)
        {
            try
            {
                var query = "Select Minuto,IdTipoVista,NroDiapositiva,ConLogoVideo,ConLogoDiapositiva from pla.T_SesionConfigurarVideo where IdConfigurarVideoPrograma=@IdConfigurarVideoPrograma AND Estado = 1 ORDER BY NroDiapositiva";
                var Lista = _dapper.QueryDapper(query, new { IdConfigurarVideoPrograma });
                return JsonConvert.DeserializeObject<List<SesionConfiguracionVideoDTO>>(Lista);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
