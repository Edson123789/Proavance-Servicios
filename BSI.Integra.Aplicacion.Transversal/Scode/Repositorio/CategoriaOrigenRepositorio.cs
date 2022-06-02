using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: CategoriaOrigenRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Categoría Origen
    /// </summary>
    public class CategoriaOrigenRepositorio : BaseRepository<TCategoriaOrigen, CategoriaOrigenBO>
    {
        #region Metodos Base
        public CategoriaOrigenRepositorio() : base()
        {
        }
        public CategoriaOrigenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CategoriaOrigenBO> GetBy(Expression<Func<TCategoriaOrigen, bool>> filter)
        {
            IEnumerable<TCategoriaOrigen> listado = base.GetBy(filter);
            List<CategoriaOrigenBO> listadoBO = new List<CategoriaOrigenBO>();
            foreach (var itemEntidad in listado)
            {
                CategoriaOrigenBO objetoBO = Mapper.Map<TCategoriaOrigen, CategoriaOrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CategoriaOrigenBO FirstById(int id)
        {
            try
            {
                TCategoriaOrigen entidad = base.FirstById(id);
                CategoriaOrigenBO objetoBO = new CategoriaOrigenBO();
                Mapper.Map<TCategoriaOrigen, CategoriaOrigenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CategoriaOrigenBO FirstBy(Expression<Func<TCategoriaOrigen, bool>> filter)
        {
            try
            {
                TCategoriaOrigen entidad = base.FirstBy(filter);
                CategoriaOrigenBO objetoBO = Mapper.Map<TCategoriaOrigen, CategoriaOrigenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CategoriaOrigenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CategoriaOrigenBO> listadoBO)
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

        public bool Update(CategoriaOrigenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CategoriaOrigenBO> listadoBO)
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
        private void AsignacionId(TCategoriaOrigen entidad, CategoriaOrigenBO objetoBO)
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

        private TCategoriaOrigen MapeoEntidad(CategoriaOrigenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCategoriaOrigen entidad = new TCategoriaOrigen();
                entidad = Mapper.Map<CategoriaOrigenBO, TCategoriaOrigen>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<CategoriaOrigenBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCategoriaOrigen, bool>>> filters, Expression<Func<TCategoriaOrigen, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TCategoriaOrigen> listado = base.GetFiltered(filters, orderBy, ascending);
            List<CategoriaOrigenBO> listadoBO = new List<CategoriaOrigenBO>();

            foreach (var itemEntidad in listado)
            {
                CategoriaOrigenBO objetoBO = Mapper.Map<TCategoriaOrigen, CategoriaOrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion


        /// Autor: Jose Villena
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de nombres de las Categoria Origen(activos)  registradas en el sistema 
        /// y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <param></param>
        /// <returns>Id, Nombre</returns> 
        public List<CategoriaOrigenFiltroDTO> ObtenerCategoriaFiltro()
        {
            try
            {
                List<CategoriaOrigenFiltroDTO> categoriasOrigen = new List<CategoriaOrigenFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM mkt.V_TCategoriaOrigen_ObtenerIdNombre WHERE  Estado = 1";
                var categoriasOrigenDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(categoriasOrigenDB) && !categoriasOrigenDB.Contains("[]"))
                {
                    categoriasOrigen = JsonConvert.DeserializeObject<List<CategoriaOrigenFiltroDTO>>(categoriasOrigenDB);
                }
                return categoriasOrigen;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de nombres de las Categoria Origen(activos)  registradas en el sistema 
        /// y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <param></param>
        /// <returns>Id, Nombre</returns> 
        public List<CategoriaOrigeCombonDTO> ObtenerCategoriaPorTipoDato(int TipoDato)
        {
            try
            {
                List<CategoriaOrigeCombonDTO> categoriasOrigen = new List<CategoriaOrigeCombonDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, IdTipoDato, IdTipoCategoriaOrigen FROM mkt.T_CategoriaOrigen WHERE IdTipoDato = @TipoDato";
                var categoriasOrigenDB = _dapper.QueryDapper(_query, new { TipoDato = TipoDato });
                if (!string.IsNullOrEmpty(categoriasOrigenDB) && !categoriasOrigenDB.Contains("[]"))
                {
                    categoriasOrigen = JsonConvert.DeserializeObject<List<CategoriaOrigeCombonDTO>>(categoriasOrigenDB);
                }
                return categoriasOrigen;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene un listado de las categorias para poder agrupadas por TipoCategoriaOrigen
        /// </summary>
        /// <returns>Lista de objetos de clase CategoriaOrigenFiltroGrupoDTO</returns>
        public List<CategoriaOrigenFiltroGrupoDTO> ObtenerCategoriaFiltroGrupo()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new CategoriaOrigenFiltroGrupoDTO { Id = x.Id, Nombre = x.Nombre, IdTipoCategoriaOrigen = x.IdTipoCategoriaOrigen }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las categoria origen para ser filtrados usando idTipoCategoriaOrigen
        /// </summary>
        /// <returns></returns>
        public List<NombreTipoCategoriaOrigenFiltroDTO> ObtenerTodoParaFitro()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new NombreTipoCategoriaOrigenFiltroDTO { Id = x.Id, Nombre = x.Nombre, IdTipoCategoriaOrigen = x.IdTipoCategoriaOrigen }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de nombres de las Categoria Origen(activos)  registradas en el sistema 
        /// y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <returns>Id, Nombre</returns>
        public List<CompuestoCategoriaOrigenConHijosDTO> ObtenerCategoriaConHijos()
        {
            try
            {
                List<CategoriaOrigenConHijosDTO> categoriasOrigen = new List<CategoriaOrigenConHijosDTO>();
                List<CompuestoCategoriaOrigenConHijosDTO> compuesto = new List<CompuestoCategoriaOrigenConHijosDTO>();
                var _query = string.Empty;
                _query = "SELECT IdCategoriaOrigen,Nombre,IdSubCategoria,IdTipoFormulario,NombreTipoFormulario " +
                    " FROM mkt.V_TCategoriaOrigen_ConHijos WHERE  EstadoCategoriaOrigen = 1 and EstaSubCategoria = 1 and EstadoTipoFormulario = 1";
                var categoriasOrigenDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(categoriasOrigenDB) && !categoriasOrigenDB.Contains("[]"))
                {
                    categoriasOrigen = JsonConvert.DeserializeObject<List<CategoriaOrigenConHijosDTO>>(categoriasOrigenDB);

                    compuesto = (from p in categoriasOrigen
                                 group p by new
                                 {
                                     p.IdCategoriaOrigen,
                                     p.Nombre,

                                 } into g
                                 select new CompuestoCategoriaOrigenConHijosDTO
                                 {
                                     IdCategoriaOrigen = g.Key.IdCategoriaOrigen,
                                     NombreCategoriaOrigen = g.Key.Nombre,

                                     SubCategorias = g.Select(o => new SubCategoriaFormularioDTO
                                     {
                                         IdSubCategoria = o.IdSubCategoria,
                                         IdTipoFormulario = o.IdTipoFormulario,
                                         NombreTipoFormulario = o.NombreTipoFormulario
                                     }).ToList(),

                                 }).ToList();
                }
                return compuesto;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// Obtener todos los datos de todas las categorias origen con estado 1
        /// </summary>
        /// <returns></returns>
        public List<CategoriaOrigenDTO> ObtenerTodo()
        {
            try
            {
                List<CategoriaOrigenDTO> categoriasOrigen = new List<CategoriaOrigenDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM mkt.V_TCategoriaOrigen_ObtenerIdNombre WHERE  Estado = 1";
                var categoriasOrigenDB = _dapper.QueryDapper(_query, null);
                categoriasOrigen = JsonConvert.DeserializeObject<List<CategoriaOrigenDTO>>(categoriasOrigenDB);
                return categoriasOrigen;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obtiene el id, nombre de un origen filtrado por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<CategoriaOrigenFiltroDTO> ObtenerCategoriaOrigenPorNombre(string Nombre)
        {
            try
            {
                List<CategoriaOrigenFiltroDTO> categoriasOrigen = new List<CategoriaOrigenFiltroDTO>();
                var _query = "SELECT Id, Nombre FROM mkt.V_TCategoriaOrigen_ObtenerIdNombre WHERE Estado = 1 AND Nombre = @nombre";//TODO
                var categoriasOrigenDB = _dapper.QueryDapper(_query, new { nombre = Nombre });
                categoriasOrigen = JsonConvert.DeserializeObject<List<CategoriaOrigenFiltroDTO>>(categoriasOrigenDB);
                return categoriasOrigen;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la categoria origen subcategoria dato
        /// </summary>
        /// <param name="idCategoriaOrigen">Id de la categoria origen (PK de la tabla mkt.T_CategoriaOrigen)</param>
        /// <param name="idTipoFormulario">Id del tipo de formulario (PK de la tabla mkt.T_TipoFormulario)</param>
        /// <returns>Objeto de clase CategoriaOrigenSubCategoriaDatoDTO</returns>
        public CategoriaOrigenSubCategoriaDatoDTO ObtenerCategoriaOrigenSubCategoriaDato(int idCategoriaOrigen, int idTipoFormulario)
        {
            try
            {
                CategoriaOrigenSubCategoriaDatoDTO categoriaOrigenSubCategoriaDato = new CategoriaOrigenSubCategoriaDatoDTO();
                var query = "SELECT IdCategoriaOrigen, IdSubCategoriaDato, CodigoOrigen, IdTipoCategoriaOrigen, NombreCategoriaOrigen FROM mkt.V_ObtenerCategoriaOrigen_SubCategoriaDato WHERE IdCategoriaOrigen = @idCategoriaOrigen AND IdTipoFormulario = @idTipoFormulario";
                var categoriaOrigenDB = _dapper.FirstOrDefault(query, new { idCategoriaOrigen, idTipoFormulario });
                categoriaOrigenSubCategoriaDato = JsonConvert.DeserializeObject<CategoriaOrigenSubCategoriaDatoDTO>(categoriaOrigenDB);
                return categoriaOrigenSubCategoriaDato;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<TipoInteraccionbyTipoFormularioDTO> ObtenerTipoInteraccionporTipoFormulario(int idOrigenFormulario)
        {
            try
            {
                TipoInteraccionbyTipoFormularioDTO TiposInteraccion = new TipoInteraccionbyTipoFormularioDTO();
                var query = "select IdProcedenciaFormulario,IdTipoInteraccion from mkt.V_ObtenerTipoInteraccionSubCategoriaDato where IdProcedenciaFormulario= @IdOrigenFormulario";
                var categoriaOrigenDB = _dapper.QueryDapper(query, new { idOrigenFormulario });
                return JsonConvert.DeserializeObject<List<TipoInteraccionbyTipoFormularioDTO>>(categoriaOrigenDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CategoriaOrigenCodigoOrigenDTO ObtenerCodigoOrigenIdTipoFormulario(int idCategoriaOrigen, int idTipoFormulario)
        {
            try
            {
                string _query = "SELECT CodigoOrigen, IdSubCategoriaDato FROM mkt.V_ObtenerCodigoOrigenIdTipoFormulario WHERE IdCategoriaOrigen = @IdCategoriaOrigen AND IdTipoFormulario = @IdTipoFormulario";
                string _queryRespuesta = _dapper.FirstOrDefault(_query, new { IdCategoriaOrigen = idCategoriaOrigen, IdTipoFormulario = idTipoFormulario });
                if (_queryRespuesta != "null")
                    return JsonConvert.DeserializeObject<CategoriaOrigenCodigoOrigenDTO>(_queryRespuesta);
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtener el Id y Nombre de la tabla T_CategoriaOrigen (estado=1) util para combobox
        /// </summary>
        /// <returns>Lista de objetos de clase CategoriaOrigenFiltroDTO</returns>
        public List<CategoriaOrigenFiltroDTO> ObtenerListaCategoriaOrigen()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM mkt.V_TCategoriaOrigen_Nombre WHERE Estado = 1";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<CategoriaOrigenFiltroDTO> listaCateogriaOrigen = JsonConvert.DeserializeObject<List<CategoriaOrigenFiltroDTO>>(responseQuery);
                return listaCateogriaOrigen;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<TipoDatoDTO> TipoDatoFiltro()
        {
            try
            {
                List<TipoDatoDTO> items = new List<TipoDatoDTO>();
                List<TipoDatoBO> lista = new List<TipoDatoBO>();
                TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio();
                lista = _repTipoDato.GetBy(o => true).OrderByDescending(x => x.Id).ToList();
                items = lista.Select(x => new TipoDatoDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Prioridad = x.Prioridad,

                }).ToList();

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ProveedorCampaniaIntegraDTO> ProveedorCampaniaIntegraFiltro()
        {
            try
            {
                List<ProveedorCampaniaIntegraDTO> items = new List<ProveedorCampaniaIntegraDTO>();
                List<ProveedorCampaniaIntegraBO> lista = new List<ProveedorCampaniaIntegraBO>();
                ProveedorCampaniaIntegraRepositorio _repProveedor = new ProveedorCampaniaIntegraRepositorio();
                lista = _repProveedor.GetBy(o => true).OrderByDescending(x => x.Id).ToList();
                items = lista.Select(x => new ProveedorCampaniaIntegraDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    PorDefecto = x.PorDefecto

                }).ToList();

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<TipoInteraccionDTO> TipoInteraccionFiltro()
        {
            try
            {
                List<TipoInteraccionDTO> items = new List<TipoInteraccionDTO>();
                List<TipoInteraccionBO> lista = new List<TipoInteraccionBO>();
                TipoInteraccionRepositorio _repTipoInteraccion = new TipoInteraccionRepositorio();
                lista = _repTipoInteraccion.GetBy(o => true).OrderByDescending(x => x.Id).ToList();
                items = lista.Select(x => new TipoInteraccionDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Canal = x.Canal

                }).ToList();

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<ProcedenciaFormularioDTO> ProcedenciaFormularioFiltro()
        {
            try
            {
                List<ProcedenciaFormularioDTO> items = new List<ProcedenciaFormularioDTO>();
                List<ProcedenciaFormularioBO> lista = new List<ProcedenciaFormularioBO>();
                ProcedenciaFormularioRepositorio _repProcedencia = new ProcedenciaFormularioRepositorio();
                //var lista2 = _repProcedencia.GetBy(o => true, x=>new { x.Id, x.Nombre, x.Descripcion}).OrderByDescending(x => x.Id).ToList();
                lista = _repProcedencia.GetBy(o => true).OrderByDescending(x => x.Id).ToList();

                items = lista.Select(x => new ProcedenciaFormularioDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion

                }).ToList();

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Tipo de categoria de origen para el filtro
        /// </summary>
        /// <returns>Lista de objetos de clase TipoCategoriaOrigenDTO</returns>
        public List<TipoCategoriaOrigenDTO> TipoCategoriaOrigenFiltro()
        {
            try
            {
                List<TipoCategoriaOrigenDTO> items = new List<TipoCategoriaOrigenDTO>();
                List<TipoCategoriaOrigenBO> lista = new List<TipoCategoriaOrigenBO>();
                TipoCategoriaOrigenRepositorio _repTipoCategoria = new TipoCategoriaOrigenRepositorio();
                lista = _repTipoCategoria.GetBy(o => true).OrderByDescending(x => x.Id).ToList();
                items = lista.Select(x => new TipoCategoriaOrigenDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Meta = x.Meta,
                    OportunidadMaxima = x.OportunidadMaxima

                }).ToList();

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Tipo de categoria para el filtro
        /// </summary>
        /// <returns>Lista de objetos de clase TipoCategoriaOrigenDTO</returns>
        public List<TipoCategoriaOrigenDTO> TipoCategoriaOrigenFiltroTodo()
        {
            try
            {
                List<TipoCategoriaOrigenDTO> tipoCategoria = new List<TipoCategoriaOrigenDTO>();
                string queryTipoCategoria = string.Empty;
                queryTipoCategoria = "SELECT id,Nombre FROM mkt.t_tipocategoriaorigen";
                var TipoCategoria = _dapper.QueryDapper(queryTipoCategoria, null);
                if (!string.IsNullOrEmpty(TipoCategoria) && !TipoCategoria.Contains("[]"))
                {
                    tipoCategoria = JsonConvert.DeserializeObject<List<TipoCategoriaOrigenDTO>>(TipoCategoria);
                }
                return tipoCategoria;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista las categorias de origen segun el DTO enviado como filtro
        /// </summary>
        /// <returns>Lista de objetos de clase CategoriaOrigenDTO</returns>
        public List<CategoriaOrigenDTO> ListarCategoriaOrigen(FiltroPaginadorDTO filtro)
        {
            try
            {
                List<CategoriaOrigenDTO> items = new List<CategoriaOrigenDTO>();
                List<Expression<Func<TCategoriaOrigen, bool>>> filters = new List<Expression<Func<TCategoriaOrigen, bool>>>();
                var total = 0;
                List<CategoriaOrigenBO> lista = new List<CategoriaOrigenBO>();
                if (filtro != null && filtro.Take != 0)
                {
                    if ((filtro.FiltroKendo != null && filtro.FiltroKendo.Filters.Count > 0))
                    {
                        // Creamos la Lista de filtros
                        foreach (var filterGrid in filtro.FiltroKendo.Filters)
                        {
                            switch (filterGrid.Field)
                            {
                                case "Nombre":
                                    filters.Add(o => o.Nombre.Contains(filterGrid.Value));
                                    break;
                                case "Descripcion":
                                    filters.Add(o => o.Descripcion.Contains(filterGrid.Value));
                                    break;
                                case "IdTipoDato":
                                    filters.Add(o => o.IdTipoDato.ToString().Contains(filterGrid.Value));
                                    break;
                                default:
                                    filters.Add(o => true);
                                    break;
                            }
                        }
                    }
                    lista = GetFiltered(filters, p => p.Id, false).ToList();
                    total = lista.Count();
                    lista = lista.Skip(filtro.Skip).Take(filtro.Take).ToList();
                }
                else
                {
                    lista = GetBy(o => true).OrderByDescending(x => x.Id).ToList();
                    total = lista.Count();


                }
                items = lista.Select(x => new CategoriaOrigenDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    IdTipoDato = x.IdTipoDato,
                    IdTipoCategoriaOrigen = x.IdTipoCategoriaOrigen,
                    Meta = x.Meta,
                    IdProveedorCampaniaIntegra = x.IdProveedorCampaniaIntegra,
                    IdFormularioProcedencia = x.IdFormularioProcedencia,
                    Considerar = x.Considerar,
                    CodigoOrigen = x.CodigoOrigen,
                    Total = total.ToString()
                }).ToList();

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Obtiene los tipos de interaccion por procedencia
        /// </summary>
        /// <returns>Lista de objetos de clase TipoInteraccionDTO</returns>
        public List<TipoInteraccionDTO> TiposInteraccionPorPorcedenciaFiltro()
        {
            try
            {
                List<TipoInteraccionDTO> tipoInteraccion = new List<TipoInteraccionDTO>();
                string queryTipoInteraccion = string.Empty;
                queryTipoInteraccion = " SELECT Id,Nombre FROM mkt.V_ObtenerTipoInteraccionPorProcedenciaFormulario WHERE Estado = 1";
                var queryResultado = _dapper.QueryDapper(queryTipoInteraccion, null);
                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("[]"))
                {
                    tipoInteraccion = JsonConvert.DeserializeObject<List<TipoInteraccionDTO>>(queryResultado);
                }
                return tipoInteraccion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        ///  Obtiene todos los registros sin los campos de auditoria
        /// </summary>
        /// <returns>Lista de objetos de clase TodoCategoriaOrigenDTO</returns>
        public List<TodoCategoriaOrigenDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new TodoCategoriaOrigenDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,
                    CodigoOrigen = y.CodigoOrigen,
                    IdTipoDato = y.IdTipoDato,
                    IdTipoCategoriaOrigen = y.IdTipoCategoriaOrigen,
                    Meta = y.Meta,
                    IdProveedorCampaniaIntegra = y.IdProveedorCampaniaIntegra,
                    IdFormularioProcedencia = y.IdFormularioProcedencia,
                    Considerar = y.Considerar
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///  Obtiene todos los registros sin los campos de auditoria
        /// </summary>
        /// <param name="id">Id de la categoria origen (PK de la tabla mkt.T_CategoriaOrigen)</param>
        /// <returns>Entero con el id tipo de categoria origen</returns>
        public int ObtneerTipoCategoriaOrigen(int id)
        {
            try
            {
                Dictionary<string, int> respuesta = new Dictionary<string, int>();
                string queryTipoInteraccion = string.Empty;
                queryTipoInteraccion = " SELECT IdTipoCategoriaOrigen FROM mkt.V_TCategoriaOrigen_ObtenerIdTipoCategoriaOrigen WHERE Id = @Id";
                var tipoInteraccion = _dapper.FirstOrDefault(queryTipoInteraccion, new { Id = id });
                if (!string.IsNullOrEmpty(tipoInteraccion) && !tipoInteraccion.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<Dictionary<string, int>>(tipoInteraccion);
                }
                //REVISAR CARLOS 17082019
                return respuesta["IdTipoCategoriaOrigen"];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de nombres de las Categoria Origen(activos)  registradas en el sistema 
        /// y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <returns>Id, Nombre</returns>
        public List<CategoriaOrigenFiltroDTO> ObtenerCategoriaPorTipoFuente(int IdConjuntoAnuncioFuente)
        {
            try
            {
                List<CategoriaOrigenFiltroDTO> categoriasOrigen = new List<CategoriaOrigenFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM mkt.V_CategoriaOrigenConjuntoAnuncio WHERE  IdConjuntoAnuncioFuente=@IdConjuntoAnuncioFuente";
                var categoriasOrigenDB = _dapper.QueryDapper(_query, new { IdConjuntoAnuncioFuente = IdConjuntoAnuncioFuente });
                if (!string.IsNullOrEmpty(categoriasOrigenDB) && !categoriasOrigenDB.Contains("[]"))
                {
                    categoriasOrigen = JsonConvert.DeserializeObject<List<CategoriaOrigenFiltroDTO>>(categoriasOrigenDB);
                }
                return categoriasOrigen;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 03-10-2021
        /// Version: 1.0
        /// <summary>
        /// Extrae el Id y Nombre de categoria origen de Adwords con campañas activas
        /// </summary>
        /// <param></param>
        /// <returns>List<CategoriaOrigenAdwordsDTO></returns> 
        public List<CategoriaOrigenAdwordsDTO> ObtenerCategoriaOrigenAdwords()
        {
            try
            {
                List<CategoriaOrigenAdwordsDTO> categoriasOrigenAdws = new List<CategoriaOrigenAdwordsDTO>();
                var query = string.Empty;
                query = "SELECT Id, Nombre FROM [mkt].[V_ObtenerCategoriaOrigenAdwordsActivos] WHERE  Estado = 1";
                var categoriasOrigenAdwsDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(categoriasOrigenAdwsDB) && !categoriasOrigenAdwsDB.Contains("[]"))
                {
                    categoriasOrigenAdws = JsonConvert.DeserializeObject<List<CategoriaOrigenAdwordsDTO>>(categoriasOrigenAdwsDB);
                }
                return categoriasOrigenAdws;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 03-10-2021
        /// Version: 1.0
        /// <summary>
        /// Permite obterner el Centro de Costo por el nombre de Campaña.
        /// </summary>
        /// <param name="Nombre"> Nombre de la Campaña </param>
        /// <returns>Objeto</returns> 
        public CentroCostoCampaniaDTO ObtenerCentroCostoPorCampania(string Nombre)
        {
            try
            {
                string query = "SELECT IdCentroCosto,CentroCosto, Codigo, Campania FROM [mkt].[V_ObtenerCentroCostoPorCampania] WHERE  Codigo LIKE CONCAT('%',@Nombre) AND Estado = 1";
                var centroCostoCampaniaAdwsDB = _dapper.FirstOrDefault(query, new { Nombre = Nombre });
                if (centroCostoCampaniaAdwsDB != "null")
                {
                    return JsonConvert.DeserializeObject<CentroCostoCampaniaDTO>(centroCostoCampaniaAdwsDB);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Jose Villena
        /// Fecha: 03-10-2021
        /// Version: 1.0
        /// <summary>
        /// Obterner el Centro de Costo por IdConjuntoAnuncio y NombreCampania
        /// </summary>
        /// <param name="IdConjuntoAnuncio">IdConjuntoAnuncio </param>
        /// <param name="NombreCampania">Nombre Campaña </param>
        /// <returns></returns> 
        public CentroCostoCampaniaDTO ObtenerCentroCostoPorNombreIdConjuntoAnuncio(int IdConjuntoAnuncio, string NombreCampania)
        {
            try
            {
                string query = "SELECT IdCentroCosto,CentroCosto, Codigo, Campania FROM [mkt].[V_ObtenerCentroCampaniaPorIdConjuntoAnuncioYNombreCampania] WHERE  IdConjuntoAnuncio=@IdConjuntoAnuncio AND Codigo LIKE CONCAT('%',@NombreCampania) AND Estado = 1";
                var centroCostoCampaniaAdwsDB = _dapper.FirstOrDefault(query, new { IdConjuntoAnuncio = IdConjuntoAnuncio, NombreCampania = NombreCampania });
                if (centroCostoCampaniaAdwsDB != "null")
                {
                    return JsonConvert.DeserializeObject<CentroCostoCampaniaDTO>(centroCostoCampaniaAdwsDB);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
