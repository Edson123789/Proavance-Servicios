using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class PublicidadWebRepositorio : BaseRepository<TPublicidadWeb, PublicidadWebBO>
    {
        #region Metodos Base
        public PublicidadWebRepositorio() : base()
        {
        }
        public PublicidadWebRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PublicidadWebBO> GetBy(Expression<Func<TPublicidadWeb, bool>> filter)
        {
            IEnumerable<TPublicidadWeb> listado = base.GetBy(filter);
            List<PublicidadWebBO> listadoBO = new List<PublicidadWebBO>();
            foreach (var itemEntidad in listado)
            {
                PublicidadWebBO objetoBO = Mapper.Map<TPublicidadWeb, PublicidadWebBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PublicidadWebBO FirstById(int id)
        {
            try
            {
                TPublicidadWeb entidad = base.FirstById(id);
                PublicidadWebBO objetoBO = new PublicidadWebBO();
                Mapper.Map<TPublicidadWeb, PublicidadWebBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PublicidadWebBO FirstBy(Expression<Func<TPublicidadWeb, bool>> filter)
        {
            try
            {
                TPublicidadWeb entidad = base.FirstBy(filter);
                PublicidadWebBO objetoBO = Mapper.Map<TPublicidadWeb, PublicidadWebBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PublicidadWebBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPublicidadWeb entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PublicidadWebBO> listadoBO)
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

        public bool Update(PublicidadWebBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPublicidadWeb entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PublicidadWebBO> listadoBO)
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
        private void AsignacionId(TPublicidadWeb entidad, PublicidadWebBO objetoBO)
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

        private TPublicidadWeb MapeoEntidad(PublicidadWebBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPublicidadWeb entidad = new TPublicidadWeb();
                entidad = Mapper.Map<PublicidadWebBO, TPublicidadWeb>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.PublicidadWebPrograma != null && objetoBO.PublicidadWebPrograma.Count > 0)
                {
                    foreach (var hijo in objetoBO.PublicidadWebPrograma)
                    {
                        TPublicidadWebPrograma entidadHijo = new TPublicidadWebPrograma();
                        entidadHijo = Mapper.Map<PublicidadWebProgramaBO, TPublicidadWebPrograma>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPublicidadWebPrograma.Add(entidadHijo);
                    }
                }
                if (objetoBO.PublicidadWebFormulario != null)
                {
                    TPublicidadWebFormulario entidadHijo = new TPublicidadWebFormulario();
                    entidadHijo = Mapper.Map<PublicidadWebFormularioBO, TPublicidadWebFormulario>(objetoBO.PublicidadWebFormulario,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TPublicidadWebFormulario.Add(entidadHijo);

                    //mapea al hijo interno
                    if (objetoBO.PublicidadWebFormulario.PublicidadWebFormularioCampo != null && objetoBO.PublicidadWebFormulario.PublicidadWebFormularioCampo.Count > 0)
                    {
                        foreach (var hijo in objetoBO.PublicidadWebFormulario.PublicidadWebFormularioCampo)
                        {
                            TPublicidadWebFormularioCampo entidadHijo2 = new TPublicidadWebFormularioCampo();
                            entidadHijo2 = Mapper.Map<PublicidadWebFormularioCampoBO, TPublicidadWebFormularioCampo>(hijo,
                                opt => opt.ConfigureMap(MemberList.None));
                            entidadHijo.TPublicidadWebFormularioCampo.Add(entidadHijo2);
                        }
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
        ///  Obtiene la lista de formularios webinar y papper(activos) registrados en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<PublicidadWebPanelDTO> ListarPublicidadWeb()
        {
            try
            {
                List<PublicidadWebPanelDTO> items = new List<PublicidadWebPanelDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdTipoPublicidadWeb, IdConjuntoAnuncio, IdCategoriaOrigen, NombreConjuntoAnuncio, Nombre, Codigo, Popup," +
                    "Titulo, Descripcion, Tiempo, IdChatZoopim, IdPespecifico, UrlImagen, UrlBrochure, UrlVideo, EsRegistroAdicional"+
                    " FROM mkt.V_TPublicidadWeb_Panel WHERE Estado = 1 order by Id desc";
                var pgeneralDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PublicidadWebPanelDTO>>(pgeneralDB);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene la lista de campanias no asociadas a algun formulario de solicitud
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<CampaniaNoAsociadaAFormularioDTO> ObtenerCampaniasNoAsociadas(string nombre)
        {
            try
            {
                List<CampaniaNoAsociadaAFormularioDTO> pEspecificoCentroCosto = new List<CampaniaNoAsociadaAFormularioDTO>();
                var _query = "SELECT Id, Nombre FROM mkt.V_TPublicidadWeb_ConsultaCampaniaNoAsociada WHERE  Nombre LIKE CONCAT('%',@nombre,'%') AND EstadoCampania = 1 AND IdSolicitud is null";
                var pEspecificoCentroCostoDB = _dapper.QueryDapper(_query, new { nombre });
                if (!string.IsNullOrEmpty(pEspecificoCentroCostoDB) && !pEspecificoCentroCostoDB.Contains("[]"))
                {
                    pEspecificoCentroCosto = JsonConvert.DeserializeObject<List<CampaniaNoAsociadaAFormularioDTO>>(pEspecificoCentroCostoDB);
                }
                return pEspecificoCentroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Elimina el registro de Publicidad Web y sus hijos en la base de Datos del Portal
        /// </summary>
        /// <returns></returns>
        public void EliminarPublicidadPortalWeb(int idPublicidadWeb, string usuario)
        {
            try
            {
                var query = _dapper.QuerySPDapper("mkt.SP_EliminarPublicidadWeb_PortalWeb", new
                {

                    IdPublicidad = idPublicidadWeb,
                    Usuario = usuario

                });
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Inserta el registro de Publicidad Web y sus hijos en la base de Datos del Portal
        /// </summary>
        /// <returns></returns>
        public void InsertarPublicidadPortalWeb(int idPublicidadWeb)
        {
            try
            {
                var query = _dapper.QuerySPDapper("mkt.SP_InsertarPublicidadWeb_PortalWeb", new
                {
                    IdPublicidad = idPublicidadWeb

                });
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Actualiza el registro de Publicidad Web y sus hijos en la base de Datos del Portal
        /// </summary>
        /// <returns></returns>
        public void ActualizarPublicidadPortalWeb(int idPublicidadWeb, string usuario)
        {
            try
            {
                var query = _dapper.QuerySPDapper("mkt.SP_ActualizarPublicidadWeb_PortalWeb", new
                {
                    IdPublicidad = idPublicidadWeb,
                    Usuario = usuario
                });
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
