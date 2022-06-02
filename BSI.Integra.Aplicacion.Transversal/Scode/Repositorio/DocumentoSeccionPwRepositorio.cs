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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Planificacion/DocumentoSeccion_PW
    /// Autor: Fischer Valdez - Priscila Pacsi - Joao - Edgar Serruto - Luis Huallpa - Gian Miranda
    /// Fecha: 05/02/2021
    /// <summary>
    /// Repositorio para consultas de pla.T_DocumentoSeccion_PW
    /// </summary>
    public class DocumentoSeccionPwRepositorio : BaseRepository<TDocumentoSeccionPw, DocumentoSeccionPwBO>
    {
        #region Metodos Base
        public DocumentoSeccionPwRepositorio() : base()
        {
        }
        public DocumentoSeccionPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentoSeccionPwBO> GetBy(Expression<Func<TDocumentoSeccionPw, bool>> filter)
        {
            IEnumerable<TDocumentoSeccionPw> listado = base.GetBy(filter);
            List<DocumentoSeccionPwBO> listadoBO = new List<DocumentoSeccionPwBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoSeccionPwBO objetoBO = Mapper.Map<TDocumentoSeccionPw, DocumentoSeccionPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoSeccionPwBO FirstById(int id)
        {
            try
            {
                TDocumentoSeccionPw entidad = base.FirstById(id);
                DocumentoSeccionPwBO objetoBO = new DocumentoSeccionPwBO();
                Mapper.Map<TDocumentoSeccionPw, DocumentoSeccionPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoSeccionPwBO FirstBy(Expression<Func<TDocumentoSeccionPw, bool>> filter)
        {
            try
            {
                TDocumentoSeccionPw entidad = base.FirstBy(filter);
                DocumentoSeccionPwBO objetoBO = Mapper.Map<TDocumentoSeccionPw, DocumentoSeccionPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoSeccionPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentoSeccionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoSeccionPwBO> listadoBO)
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

        public bool Update(DocumentoSeccionPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentoSeccionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoSeccionPwBO> listadoBO)
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
        private void AsignacionId(TDocumentoSeccionPw entidad, DocumentoSeccionPwBO objetoBO)
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

        private TDocumentoSeccionPw MapeoEntidad(DocumentoSeccionPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentoSeccionPw entidad = new TDocumentoSeccionPw();
                entidad = Mapper.Map<DocumentoSeccionPwBO, TDocumentoSeccionPw>(objetoBO,
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

        /// Repositorio: DocumentoSeccionPwRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene las secciones disponibles por el programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general del cual se desae obtener las secciones disponibles (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public List<SeccionDocumentoDTO> ObtenerSecciones(int idPGeneral)
        {
            try
            {
                List<SeccionDocumentoDTO> seccionDocumento = new List<SeccionDocumentoDTO>();
                string querySeccion = "SELECT Id, Titulo, Contenido, IdPGeneral, OrdenWeb FROM pla.V_ListaSeccionesPorIdPrograma WHERE Estado = 1 AND VisibleWeb = 1 AND IdPGeneral = @idPGeneral ORDER BY OrdenWeb;";
                var resultado = _dapper.QueryDapper(querySeccion, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    seccionDocumento = JsonConvert.DeserializeObject<List<SeccionDocumentoDTO>>(resultado);
                }
                return seccionDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Retorna las secciones por programa general
        /// </summary>
        /// <param name="idPgeneral"></param>
        /// <returns></returns>
        public List<SeccionDocumentoDTO> ObtenerDocumentoSeccion(int idPgeneral)
        {
            try
            {
                List<SeccionDocumentoDTO> listadoSeccionesDocumento = new List<SeccionDocumentoDTO>();
                var _querySeccion = "Select Id,Titulo,Contenido,IdPGeneral, Orden from pla.V_ListaSeccionesPorIdPrograma_Silabos where IdPGeneral= @idPgeneral";
                var querySeccion = _dapper.QueryDapper(_querySeccion, new { idPgeneral });
                if (!string.IsNullOrEmpty(querySeccion) && !querySeccion.Contains("[]"))
                {
                    listadoSeccionesDocumento = JsonConvert.DeserializeObject<List<SeccionDocumentoDTO>>(querySeccion);
                }
                return listadoSeccionesDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obteniene el contenido del DocumentoSeccion y Etiquetas
        /// </summary>
        /// <param name="idPrioridad"></param>
        /// <returns></returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiqueta(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> estructuraProgramas = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                string _queryObtenerContenidoPrograma = "select Contenido, Etiqueta from mkt.V_ObtenerContenidoYEtiqueta where IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle " +
                    "AND VisibleWeb = 1 AND TituloDocumento= @TipoDocumento and EstadoDetallePrograma = 1 and TipoPrograma != @TipoPrograma";
                string registrosBD = _dapper.QueryDapper(_queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @TipoPrograma = "Filtro",
                    @TipoDocumento = "Estructura Curricular"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    estructuraProgramas = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return estructuraProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obteniene el contenido del DocumentoSeccion y Etiquetas
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle"></param>
        /// <returns>Lista de objeto de tipo CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaPorCampaniaGeneralDetalle(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> estructuraProgramas = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta from mkt.V_ObtenerContenidoYEtiquetaCampaniaGeneral where IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle " +
                    "AND VisibleWeb = 1";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle,
                    @TipoDocumento = "Estructura Curricular"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    estructuraProgramas = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return estructuraProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el contenido de la certificacion y duracion del programa 
        /// </summary>
        /// <param name="idPrioridad"></param>
        /// <returns></returns>
        public List<CampaniaMailingCertificacionDuracionDTO> ObtenerCertificacionDuracionEtiqueta(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingCertificacionDuracionDTO> certificacionProgramas = new List<CampaniaMailingCertificacionDuracionDTO>();
                string _queryObtenerCertificacionDuracion = "select EtiquetaCertificacion, ContenidoCertificacion, EtiquetaDuracion, ContenidoDuracion from mkt.V_ObtenerCertificacionDuracionEtiqueta where IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle " +
                    "AND VisibleWeb = 1 AND TituloDocumento= @TituloDocumento and EstadoDetallePrograma = 1 and TipoPrograma != @TipoPrograma ";
                string registrosBD = _dapper.QueryDapper(_queryObtenerCertificacionDuracion, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @TipoPrograma = "Filtro",
                    @TituloDocumento = "Certificación"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    certificacionProgramas = JsonConvert.DeserializeObject<List<CampaniaMailingCertificacionDuracionDTO>>(registrosBD);
                }
                return certificacionProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el contenido de la certificacion y duracion del programa 
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de tipo CampaniaMailingCertificacionDuracionDTO</returns>
        public List<CampaniaMailingCertificacionDuracionDTO> ObtenerCertificacionDuracionEtiquetaCampaniaGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaMailingCertificacionDuracionDTO> certificacionProgramas = new List<CampaniaMailingCertificacionDuracionDTO>();
                string _queryObtenerCertificacionDuracion = "select EtiquetaCertificacion, ContenidoCertificacion, EtiquetaDuracion, ContenidoDuracion from mkt.V_ObtenerCertificacionDuracionEtiquetaCampaniaGeneral where IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle " +
                    "AND VisibleWeb = 1 AND TituloDocumento= @TituloDocumento";
                string registrosBD = _dapper.QueryDapper(_queryObtenerCertificacionDuracion, new
                {
                    @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle,
                    @TituloDocumento = "Certificación"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    certificacionProgramas = JsonConvert.DeserializeObject<List<CampaniaMailingCertificacionDuracionDTO>>(registrosBD);
                }
                return certificacionProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Nombre, Contenido y Etiqueta de DocumentoSeccion por el Id de CAmpaniaMailingDetalle
        /// </summary>
        /// <param name="idPrioridad"></param>
        /// <returns></returns>
        public List<CampaniaMailingNombreProgramaDetalleDTO> ObtenerNombreContenidoEtiquetaDocumentoSeccion(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingNombreProgramaDetalleDTO> listaNombreProgramaDetalle = new List<CampaniaMailingNombreProgramaDetalleDTO>();
                string _queryObtenerDocumentoSeccion = "select IdPGeneral, Nombre, Contenido, Titulo, Etiqueta from mkt.V_ObtenerNombreContenidoEtiquetaDocumentoSeccion where IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle AND EstadoDocumentoSeccion = 1 " +
                                    "AND VisibleWeb = 1 AND Titulo in ('Objetivos', 'Estructura Curricular', 'Certificación') and EstadoDetallePrograma = 1 and TipoPrograma != @TipoPrograma  ";
                string registrosBD = _dapper.QueryDapper(_queryObtenerDocumentoSeccion, new { @IdCampaniaMailingDetalle = idPrioridad, @TipoPrograma = "Filtro" });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    listaNombreProgramaDetalle = JsonConvert.DeserializeObject<List<CampaniaMailingNombreProgramaDetalleDTO>>(registrosBD);
                }
                return listaNombreProgramaDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Nombre, Contenido y Etiqueta de DocumentoSeccion por el Id de CAmpaniaMailingDetalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general (pla.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objeto de tipo CampaniaMailingNombreProgramaDetalleDTO</returns>
        public List<CampaniaMailingNombreProgramaDetalleDTO> ObtenerNombreContenidoEtiquetaDocumentoSeccionCampaniaGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaMailingNombreProgramaDetalleDTO> listaNombreProgramaDetalle = new List<CampaniaMailingNombreProgramaDetalleDTO>();
                string _queryObtenerDocumentoSeccion = "select IdPGeneral, Nombre, Contenido, Titulo, Etiqueta from mkt.V_ObtenerNombreContenidoEtiquetaDocumentoSeccionCampaniaGeneral where IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle " +
                                    "AND VisibleWeb = 1 AND Titulo in ('Objetivos', 'Estructura Curricular', 'Certificación') ";
                string registrosBD = _dapper.QueryDapper(_queryObtenerDocumentoSeccion, new { @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    listaNombreProgramaDetalle = JsonConvert.DeserializeObject<List<CampaniaMailingNombreProgramaDetalleDTO>>(registrosBD);
                }
                return listaNombreProgramaDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///  Obtiene todos los registros por IdDoc
        /// </summary>
        /// <returns></returns>
        public List<DocumentoSeccionPwBO> ObtenerNoMaestraPorIdDoc(int IdDoc)
        {
            try
            {
                var lista = GetBy(x => x.IdDocumentoPw == IdDoc && x.Tipo == 0, y => new DocumentoSeccionPwBO
                {
                    Id = y.Id,
                    Titulo = y.Titulo,
                    Contenido = y.Contenido,
                    IdPlantillaPw = y.IdPlantillaPw,
                    Posicion = y.Posicion,
                    Tipo = y.Tipo,
                    IdDocumentoPw = y.IdDocumentoPw,
                    IdSeccionPw = y.IdSeccionPw,
                    VisibleWeb = y.VisibleWeb,
                    ZonaWeb = y.ZonaWeb,
                    OrdenWeb = y.OrdenWeb,
                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los DocumentoSeccion asociados a una IdDocumento
        /// </summary>
        /// <param name="idDoc"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorDocumentoPw(int idDoc, string usuario, List<SeccionPwDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdDocumentoPw == idDoc && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.IdSeccionPw));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///  Obtiene todos los registros por IdDoc
        /// </summary>
        /// <returns></returns>
        public bool Merge(DocumentoSeccionPwBO persiste, int contador)
        {
            try
            {
                var objeto = FirstById(persiste.Id);
                var auxiliar = objeto;
                auxiliar.Posicion = contador;
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene registros de DocumentoSeccion filtrado por el Id
        /// </summary>
        /// <returns></returns>
        public List<DocumentoSeccionPwFiltroDTO> ObtenerDocumentoSeccionPorId(int idDocumentoSeccion)
        {
            try
            {
                List<DocumentoSeccionPwFiltroDTO> obtenerDocumentoSeccionPorId = new List<DocumentoSeccionPwFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Titulo, VisibleWeb, ZonaWeb, OrdenEeb, Contenido, IdPlantillaPW, Posicion, Tipo, IdDocumentoPW, IdSeccionPW, IdSeccionTipoContenido, NombreSeccionTipoContenido," +
                    "IdSeccionTipoDetallePw, NombreSubSeccion, IdSubSeccionTipoContenido, ContenidoSubSeccion,NumeroFila,Cabecera,PiePagina FROM pla.V_ObtenerDocumentoSeccionPorId WHERE IdDocumentoPW = @idDocumentoSeccion and EstadoDocumentoSeccion = 1 ";
                var documentoSeccionPorIdDB = _dapper.QueryDapper(_query, new { idDocumentoSeccion });
                if (!string.IsNullOrEmpty(documentoSeccionPorIdDB) && !documentoSeccionPorIdDB.Contains("[]"))
                {
                    obtenerDocumentoSeccionPorId = JsonConvert.DeserializeObject<List<DocumentoSeccionPwFiltroDTO>>(documentoSeccionPorIdDB);
                }
                return obtenerDocumentoSeccionPorId;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene registros de DocumentoSeccion filtrado por el IdPEspecifico
        /// </summary>
        /// <returns></returns>
        public List<DocumentoSeccionPwFiltroDTO> ObtenerDocumentoSeccionPorIdPEspecifico(int idPEspecifico)
        {
            try
            {
                List<DocumentoSeccionPwFiltroDTO> obtenerDocumentoSeccionPorId = new List<DocumentoSeccionPwFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Titulo, VisibleWeb, ZonaWeb, OrdenEeb, Contenido, IdPlantillaPW, Posicion, Tipo, IdDocumentoPW, IdSeccionPW, IdSeccionTipoContenido, NombreSeccionTipoContenido," +
                    "IdSeccionTipoDetallePw, NombreSubSeccion, IdSubSeccionTipoContenido, ContenidoSubSeccion,NumeroFila,Cabecera,PiePagina FROM pla.V_ObtenerDocumentoSeccionPorIdPGeneral WHERE IdPEspecifico = @idPEspecifico and EstadoDocumentoSeccion = 1 and Titulo = 'Esquema de Evaluaciones'";
                var documentoSeccionPorIdDB = _dapper.QueryDapper(_query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(documentoSeccionPorIdDB) && !documentoSeccionPorIdDB.Contains("[]"))
                {
                    obtenerDocumentoSeccionPorId = JsonConvert.DeserializeObject<List<DocumentoSeccionPwFiltroDTO>>(documentoSeccionPorIdDB);
                }
                return obtenerDocumentoSeccionPorId;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///  Elimina (Actualiza estado a false ) todos las registros de DocumentoSeccion asociados a IdDocumento
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorIdDocumento(int idDocumento, string usuario, List<DocumentoSeccionPwFiltroDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdDocumentoPw == idDocumento && x.Estado == true).ToList();
                //listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdSeccionPW.Equals(x.IdSeccionPw)));

                foreach (var item in nuevos)
                {
                    if (item.listaGridListaSecciones.Count > 0 && (item.IdSeccionPW == 91 || item.IdSeccionPW == 108 ))
                    {
                        foreach (var elemento in item.listaGridListaSecciones)
                        {
                            //if (listaBorrar.Exists(w => w.Contenido == elemento.Valor && w.IdSeccionPw == 91))
                            if (listaBorrar.Exists(w => w.Contenido == elemento.Valor))
                            {
                                listaBorrar.RemoveAll(x => x.Contenido == elemento.Valor);
                            }
                        }
                    }
                    //if (item.IdSeccionPW == 91)
                    //{
                        
                    //}
                    
                    
                }
                int con=0;
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                    con++;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las secciones del portal que le pertenecen a un alumno para un proyecto de aplicacion
        /// </summary>
        /// <returns></returns>
        public List<DocumentoSeccionPwFiltroPortalDTO> ObtenerDocumentoSeccionPorIdAlumnoPortal(int IdAlumno)
        {
            try
            {
                List<DocumentoSeccionPwFiltroPortalDTO> obtenerDocumentoSeccionPorId = new List<DocumentoSeccionPwFiltroPortalDTO>();
                var obtenerDocumentoSeccionPorIdDB = _dapper.QuerySPDapper("pla.SP_ObtenerSeccionesProyectoAplicacionPorIdAlumno", new { IdAlumno });
                if (!string.IsNullOrEmpty(obtenerDocumentoSeccionPorIdDB) && !obtenerDocumentoSeccionPorIdDB.Contains("[]"))
                {
                    obtenerDocumentoSeccionPorId = JsonConvert.DeserializeObject<List<DocumentoSeccionPwFiltroPortalDTO>>(obtenerDocumentoSeccionPorIdDB);
                }
                return obtenerDocumentoSeccionPorId;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: DocumentoSeccionPwRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene las secciones de documento de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general del cual se desea obtener sus secciones (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> Lista Secciones documentos: List<RegistroListaSeccionesDocumentoDTO> </returns>
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumento(int idPGeneral)
		{
			try
			{
				List<RegistroListaSeccionesDocumentoDTO> lista = new List<RegistroListaSeccionesDocumentoDTO>();
				var query = "SELECT Titulo, Contenido, IdSeccionTipoDetalle_PW, NumeroFila, Cabecera, PiePagina, OrdenWeb FROM pla.V_ListaSeccionesPorIdPrograma_Documento WHERE Titulo != 'Estructura Curricular' AND IdSeccionTipoDetalle_PW != 14 AND IdSeccionTipoDetalle_PW != 15 AND IdPGeneral = @IdPGeneral";
				var res = _dapper.QueryDapper(query, new { IdPGeneral = idPGeneral });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// Repositorio: DocumentoSeccionPwRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene la plantilla inicial para envio de correo masivo por operaciones
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General </param>
        /// <returns> List<RegistroListaSeccionesDocumentoDTO> </returns>
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV2(int idPGeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> lista = new List<RegistroListaSeccionesDocumentoDTO>();
                var query = "SELECT Titulo, Contenido, IdSeccionTipoDetalle_PW, NumeroFila, Cabecera, PiePagina, OrdenWeb FROM pla.V_ListaSeccionesPorIdPrograma_Documento WHERE Titulo != 'Estructura Curricular' AND Titulo != 'Beneficios' AND  Titulo != 'Certificacion' AND Titulo != 'Prerrequisitos' AND  IdPGeneral = @IdPGeneral";
                var res = _dapper.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV1(int idPGeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> lista = new List<RegistroListaSeccionesDocumentoDTO>();
                var query = "SELECT Titulo, Contenido,IdPGeneral, OrdenWeb FROM pla.V_DatoProgramaGeneralContenidoPorIdPrograma WHERE IdPGeneral = @IdPGeneral";

                //var query = "SELECT IdPGeneral, Titulo, Contenido, OrdenWeb FROM pla.V_DatoProgramaGeneralContenidoPorIdPrograma WHERE Titulo != 'Vista Previa' AND Titulo != 'Beneficios' AND Titulo != 'Expositores' AND Titulo != 'Marca Registrada' AND  Titulo != 'Certificacion' AND  Titulo != 'Certificación' AND  Titulo != 'Estructura Curricular' AND  Titulo != 'Pre-Requisitos' AND IdPGeneral = @IdPGeneral";
                var res = _dapper.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ProgramaExpositoresDTO> ObtenerExpositoresPorIdGeneral(int idPGeneral)
        {
            try
            {
                List<ProgramaExpositoresDTO> lista = new List<ProgramaExpositoresDTO>();
                var query = "SELECT Id, PrimerNombre, SegundoNombre,ApellidoPaterno,ApellidoMaterno,NombrePais, HojaVidaResumidaPerfil, IdPGeneral FROM pla.V_ObtenerExpositorPorIdPrograma WHERE IdPGeneral = @IdPGeneral";

                var res = _dapper.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ProgramaExpositoresDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: DocumentoSeccionPwRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Metodo que retorna lista de secciones de documento del programa general filtrando por la estructura curricular
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General del cual se desea averiguar los programas generales hijos (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> Lista Seccion Estructura curricular: List<RegistroListaSeccionesDocumentoDTO> </returns>
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumentoEstructuraCurricular(int idPGeneral)
		{
			try
			{
				List<RegistroListaSeccionesDocumentoDTO> lista = new List<RegistroListaSeccionesDocumentoDTO>();
				var query = "SELECT Titulo, Contenido, IdSeccionTipoDetalle_PW, NumeroFila, Cabecera, PiePagina, OrdenWeb, IdPGeneral, Nombre AS NombreCurso FROM pla.V_ListaSeccionesPorIdPrograma_Documento WHERE Titulo = 'Estructura Curricular' AND IdSeccionTipoDetalle_PW != 14 AND IdSeccionTipoDetalle_PW != 15 AND IdPGeneral = @IdPGeneral ORDER BY NumeroFila ASC, IdSeccionTipoDetalle_PW ASC";
				var res = _dapper.QueryDapper(query, new { IdPGeneral = idPGeneral });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        ///Repositorio: DocumentoSeccionPwRepositorio
        ///Autor: Fischer Valdez
        ///Fecha: 02/08/2021
        /// <summary>
        /// Obtiene la estructura curricular para un curso
        /// </summary>
        /// <param name="idPGeneral">Id del programa general del cual se desea obtener estructura (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> Lista de contenido para un programa general: List<RegistroListaSeccionesDocumentoDTO> </returns>
        public List<EstructuraCursoDTO> ObtenerEstructuraCurso(int idPGeneral)
        {
            try
            {
                List<EstructuraCursoDTO> lista = new List<EstructuraCursoDTO>();
                var query = "SELECT Contenido FROM pla.V_EstructuraCurso WHERE IdPGeneral = @IdPGeneral Order By NumeroFila";
                var res = _dapper.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<EstructuraCursoDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
