using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/ConjuntoLista
    /// Autor: Fischer Valdez - Wilber Choque - Richard Zenteno - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_ConjuntoLista
    /// </summary>
    public class ConjuntoListaRepositorio : BaseRepository<TConjuntoLista, ConjuntoListaBO>
    {
        #region Metodos Base
        public ConjuntoListaRepositorio() : base()
        {
        }
        public ConjuntoListaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConjuntoListaBO> GetBy(Expression<Func<TConjuntoLista, bool>> filter)
        {
            IEnumerable<TConjuntoLista> listado = base.GetBy(filter);
            List<ConjuntoListaBO> listadoBO = new List<ConjuntoListaBO>();
            foreach (var itemEntidad in listado)
            {
                ConjuntoListaBO objetoBO = Mapper.Map<TConjuntoLista, ConjuntoListaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConjuntoListaBO FirstById(int id)
        {
            try
            {
                TConjuntoLista entidad = base.FirstById(id);
                ConjuntoListaBO objetoBO = new ConjuntoListaBO();
                Mapper.Map<TConjuntoLista, ConjuntoListaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConjuntoListaBO FirstBy(Expression<Func<TConjuntoLista, bool>> filter)
        {
            try
            {
                TConjuntoLista entidad = base.FirstBy(filter);
                ConjuntoListaBO objetoBO = Mapper.Map<TConjuntoLista, ConjuntoListaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConjuntoListaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConjuntoLista entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConjuntoListaBO> listadoBO)
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

        public bool Update(ConjuntoListaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConjuntoLista entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConjuntoListaBO> listadoBO)
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
        private void AsignacionId(TConjuntoLista entidad, ConjuntoListaBO objetoBO)
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

        private TConjuntoLista MapeoEntidad(ConjuntoListaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConjuntoLista entidad = new TConjuntoLista();
                entidad = Mapper.Map<ConjuntoListaBO, TConjuntoLista>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));


                //mapea los hijos
                if (objetoBO.ListaConjuntoListaDetalle != null && objetoBO.ListaConjuntoListaDetalle.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaConjuntoListaDetalle)
                    {
                        TConjuntoListaDetalle entidadHijo = new TConjuntoListaDetalle();
                        entidadHijo = Mapper.Map<ConjuntoListaDetalleBO, TConjuntoListaDetalle>(hijo, opt => opt.ConfigureMap(MemberList.None));
                        entidad.TConjuntoListaDetalle.Add(entidadHijo);

                        if (hijo.ListaConjuntoListaDetalleValor != null && hijo.ListaConjuntoListaDetalleValor.Count > 0)
                        {
                            foreach (var hijo1 in hijo.ListaConjuntoListaDetalleValor)
                            {
                                TConjuntoListaDetalleValor entidadHijo1 = new TConjuntoListaDetalleValor();
                                entidadHijo1 = Mapper.Map<ConjuntoListaDetalleValorBO, TConjuntoListaDetalleValor>(hijo1, opt => opt.ConfigureMap(MemberList.None));
                                entidadHijo.TConjuntoListaDetalleValor.Add(entidadHijo1);
                            }
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
        /// Obtiene los conjunto lista 
        /// </summary>
        /// <returns></returns>
        public List<ConjuntoListaDTO> Obtener()
        {
            try
            {
                List<ConjuntoListaDTO> conjuntoLista = new List<ConjuntoListaDTO>();

                var _query = @"
                            SELECT Id, 
                                    Nombre, 
                                    Descripcion, 
                                    IdCategoriaObjetoFiltro,
                                    NombreCategoriaObjetoFiltro,
                                    IdFiltroSegmento,
                                    IdFiltroSegmentoTipoContacto,
                                    NroListasRepeticionContacto,
                                    ConsiderarYaEnviados,
                                    UsuarioCreacion, 
                                    UsuarioModificacion, 
                                    FechaCreacion, 
                                    FechaModificacion
                            FROM mkt.V_ObtenerConjuntoLista
                            WHERE EstadoConjuntoLista = 1
                                    AND EstadoCategoriaObjetoFiltro = 1
                                    AND EstadoFiltroSegmento = 1;
                            ";
                var query = _dapper.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<List<ConjuntoListaDTO>>(query);
                }
                return conjuntoLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los resultados de la ejecucion de conjunto lista por filtro segmento tipo contacto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ConjuntoListaCompuestoDTO> ObtenerResultado(int id, int idFiltroSegmentoTipoContacto)
        {
            try
            {
                List<ConjuntoListaCompuestoDTO> listaConjuntoLista= new List<ConjuntoListaCompuestoDTO>();
                var query = "";

                switch (idFiltroSegmentoTipoContacto)
                {
                    case 1:///alumno - exalumno
                        query = "mkt.SP_ObtenerResultadoConjuntoListaTipoAlumno";
                        break;
                    case 2://docente
                        query = "";
                        break;
                    case 6:///prospecto
                        query = "mkt.SP_ObtenerResultadoConjuntoLista";
                        break;
                    default:
                        break;
                }
                var listaConjuntoListaDB = _dapper.QuerySPDapper(query, new { IdConjuntoLista = id });
                if (!string.IsNullOrEmpty(listaConjuntoListaDB) && !listaConjuntoListaDB.Contains("[]"))
                {
                    listaConjuntoLista = JsonConvert.DeserializeObject<List<ConjuntoListaCompuestoDTO>>(listaConjuntoListaDB);
                }
                return listaConjuntoLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<FiltroDTO> ObtenerFiltro()
        {
            try
            {
                return GetBy(w => w.Estado == true, y => new FiltroDTO { Id = y.Id, Nombre = y.Nombre }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int ProcesarListasCorreoMailing(int IdCampaniaMailingLista, int IdCampaniaMailing, int IdConjuntoListaDetalle)
        {
            try
            {
                string _query = "[mkt].[SP_InsertarPrioridadMailchimpListaCorreo]";
                var _queryInsertar = _dapper.QuerySPFirstOrDefault(_query, new { CampaniaMailingLista = IdCampaniaMailingLista, CampaniaMailing = IdCampaniaMailing, IdConjuntoListaDetalle = IdConjuntoListaDetalle });
                var respuesta = JsonConvert.DeserializeObject<Dictionary<string, int>>(_queryInsertar);
                return respuesta.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public FacebookAudienciaDTO ObtenerAudienciaParaCrear(int IdConjuntoListaDetalle)
        {
            try
            {
                FacebookAudienciaDTO rpta = new FacebookAudienciaDTO();
                string _query = "Select  IdFiltroSegmento,FacebookIdAudiencia,Nombre,Descripcion, Cuenta " +
                                " From ";
                var respuesta = JsonConvert.DeserializeObject<FacebookAudienciaDTO>(_query);

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el numero de ejecucion actual
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ObtenerNroEjecucionActual(int id)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"mkt.SP_ObtenerNroEjecucionActualConjuntoLista";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdConjuntoLista = id });

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
        /// Obtiene el siguiente numero de ejecucion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ObtenerProximoNroEjecucion(int id)
        {
            try
            {
                return this.ObtenerNroEjecucionActual(id) + 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el conjunto lista por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ConjuntoListaDTO Obtener(int id)
        {
            try
            {
                ConjuntoListaDTO conjuntoLista = new ConjuntoListaDTO();

                var _query = @"
                            SELECT Id, 
                                    Nombre, 
                                    Descripcion, 
                                    IdCategoriaObjetoFiltro,
                                    NombreCategoriaObjetoFiltro,
                                    IdFiltroSegmento,
                                    IdFiltroSegmentoTipoContacto,
                                    NroListasRepeticionContacto,
                                    UsuarioCreacion, 
                                    UsuarioModificacion, 
                                    FechaCreacion, 
                                    FechaModificacion
                            FROM mkt.V_ObtenerConjuntoLista
                            WHERE EstadoConjuntoLista = 1
                                    AND EstadoCategoriaObjetoFiltro = 1
                                    AND EstadoFiltroSegmento = 1
                                    AND Id = @id
                            ";
                var query = _dapper.FirstOrDefault(_query, new { id });

                if (!string.IsNullOrEmpty(query))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<ConjuntoListaDTO>(query);
                }
                return conjuntoLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// USO EXCLUSIVO PARA EL REPORTE DE MONITOREO DE MENSAJES DE WHATSAPP
        /// Obtiene el Id, Nombre, Descripcion de todos los ConjuntoLista (WHERE ESTADO=1)
        /// Tambien precalcula datos adicionales por lo que el query puede tardar hasta 20 segundos en ejecutarse
        /// </summary>
        /// <returns></returns>
        public List<ConjuntoListaDTO> ObtenerInformacionBasicaConjuntoLista(DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                List<ConjuntoListaDTO> conjuntoLista = new List<ConjuntoListaDTO>();
                var query = _dapper.QuerySPDapper("[mkt].[SP_PrecalcularTablasBase_ObtenerConjuntoListas]", new { FechaInicio= FechaInicio, FechaFin=FechaFin });

                if (!string.IsNullOrEmpty(query))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<List<ConjuntoListaDTO>>(query);
                }
                return conjuntoLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
