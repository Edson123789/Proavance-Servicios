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
    /// Repositorio: SubAreaCapacitacionRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de SubÁrea de Capacitación
    /// </summary>
    public class SubAreaCapacitacionRepositorio : BaseRepository<TSubAreaCapacitacion, SubAreaCapacitacionBO>
    {
        #region Metodos Base
        public SubAreaCapacitacionRepositorio() : base()
        {
        }
        public SubAreaCapacitacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SubAreaCapacitacionBO> GetBy(Expression<Func<TSubAreaCapacitacion, bool>> filter)
        {
            IEnumerable<TSubAreaCapacitacion> listado = base.GetBy(filter);
            List<SubAreaCapacitacionBO> listadoBO = new List<SubAreaCapacitacionBO>();
            foreach (var itemEntidad in listado)
            {
                SubAreaCapacitacionBO objetoBO = Mapper.Map<TSubAreaCapacitacion, SubAreaCapacitacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SubAreaCapacitacionBO FirstById(int id)
        {
            try
            {
                TSubAreaCapacitacion entidad = base.FirstById(id);
                SubAreaCapacitacionBO objetoBO = new SubAreaCapacitacionBO();
                Mapper.Map<TSubAreaCapacitacion, SubAreaCapacitacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SubAreaCapacitacionBO FirstBy(Expression<Func<TSubAreaCapacitacion, bool>> filter)
        {
            try
            {
                TSubAreaCapacitacion entidad = base.FirstBy(filter);
                SubAreaCapacitacionBO objetoBO = Mapper.Map<TSubAreaCapacitacion, SubAreaCapacitacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SubAreaCapacitacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSubAreaCapacitacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SubAreaCapacitacionBO> listadoBO)
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

        public bool Update(SubAreaCapacitacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSubAreaCapacitacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SubAreaCapacitacionBO> listadoBO)
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
        private void AsignacionId(TSubAreaCapacitacion entidad, SubAreaCapacitacionBO objetoBO)
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

        private TSubAreaCapacitacion MapeoEntidad(SubAreaCapacitacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSubAreaCapacitacion entidad = new TSubAreaCapacitacion();
                entidad = Mapper.Map<SubAreaCapacitacionBO, TSubAreaCapacitacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.SubAreaParametroSeo != null && objetoBO.SubAreaParametroSeo.Count > 0)
                {
                    foreach (var hijo in objetoBO.SubAreaParametroSeo)
                    {
                        TSubAreaParametroSeoPw entidadHijo = new TSubAreaParametroSeoPw();
                        entidadHijo = Mapper.Map<SubAreaParametroSeoBO, TSubAreaParametroSeoPw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSubAreaParametroSeoPw.Add(entidadHijo);
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
        /// Obtiene el Id,Nombre,IdAreaCapacitacion de la subarea de capacitacion para ser listada 
        /// </summary>
        /// <returns>Lista de objetos de clase SubAreaCapacitacionFiltroDTO</returns>
        public List<SubAreaCapacitacionFiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<SubAreaCapacitacionFiltroDTO> subAreasCapacitacionFiltro = new List<SubAreaCapacitacionFiltroDTO>();
                string querySubAreaCapacitacion = string.Empty;
                querySubAreaCapacitacion = "SELECT Id, Nombre, IdAreaCapacitacion FROM pla.V_RegistrosFiltroSubAreaCapacitacion WHERE Estado = 1";
                var subAreaCapacitacionDB = _dapper.QueryDapper(querySubAreaCapacitacion, null);
                if (!string.IsNullOrEmpty(subAreaCapacitacionDB) && !subAreaCapacitacionDB.Contains("[]"))
                {
                    subAreasCapacitacionFiltro = JsonConvert.DeserializeObject<List<SubAreaCapacitacionFiltroDTO>>(subAreaCapacitacionDB);
                }
                return subAreasCapacitacionFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }


        /// Autor: 
        /// Fecha: 24/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id,Nombre de la subarea de capacitacion para ser listada 
        /// </summary>
        /// <returns>Lista de objetos de clase SubAreaCapacitacionFiltroDTO</returns>
        public List<SubAreaCapacitacionFiltroDTO> ObtenerSubAreasParaFiltro()
        {
            try
            {
                List<SubAreaCapacitacionFiltroDTO> subAreasCapacitacionFiltro = new List<SubAreaCapacitacionFiltroDTO>();
                var querySubAreaCapacitacion = "SELECT Id, Nombre, IdAreaCapacitacion FROM pla.V_RegistrosFiltroSubAreaCapacitacion WHERE Estado = 1";
                var subAreaCapacitacionDB = _dapper.QueryDapper(querySubAreaCapacitacion, new { });
                if (!string.IsNullOrEmpty(subAreaCapacitacionDB) && !subAreaCapacitacionDB.Contains("[]"))
                {
                    subAreasCapacitacionFiltro =JsonConvert.DeserializeObject<List<SubAreaCapacitacionFiltroDTO>>(subAreaCapacitacionDB);
                }
                return subAreasCapacitacionFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id,Nombre de la subarea de capacitacion para ser listada validando que sean visibles para el portal web
        /// </summary>
        /// <returns>Lista de objetos (SubAreaCapacitacionFiltroDTO)</returns>
        public List<SubAreaCapacitacionFiltroDTO> ObtenerSubAreasParaFiltroWeb()
        {
            try
            {
                List<SubAreaCapacitacionFiltroDTO> subAreasCapacitacionFiltro = new List<SubAreaCapacitacionFiltroDTO>();
                var querySubAreaCapacitacion = "SELECT Id, Nombre, IdAreaCapacitacion FROM pla.V_RegistrosFiltroSubAreaCapacitacionWeb";
                var subAreaCapacitacionDB = _dapper.QueryDapper(querySubAreaCapacitacion, new { });
                if (!string.IsNullOrEmpty(subAreaCapacitacionDB) && !subAreaCapacitacionDB.Contains("[]"))
                {
                    subAreasCapacitacionFiltro =JsonConvert.DeserializeObject<List<SubAreaCapacitacionFiltroDTO>>(subAreaCapacitacionDB);
                }
                return subAreasCapacitacionFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id,IdAreaCapacitacion,Nombre de la subarea de capacitacion(activo) registradas en el sistema
        /// </summary>
        /// <returns></returns>
        public List<SubAreaCapacitacionAutoselectDTO> ObtenerTodoFiltroAutoSelect()
        {
            try
            {
                List<SubAreaCapacitacionAutoselectDTO> subAreasCapacitacionAutoSelect = new List<SubAreaCapacitacionAutoselectDTO>();
                string _querySubAreaCapacitacionSelect = string.Empty;
                _querySubAreaCapacitacionSelect = "SELECT Id,IdAreaCapacitacion,Nombre FROM pla.V_RegistrosFiltroSubAreaCapacitacion WHERE Estado=1";
                var SubAreaCapacitacionDB = _dapper.QueryDapper(_querySubAreaCapacitacionSelect, null);
                if (!string.IsNullOrEmpty(SubAreaCapacitacionDB) && !SubAreaCapacitacionDB.Contains("[]"))
                {
                    subAreasCapacitacionAutoSelect = JsonConvert.DeserializeObject<List<SubAreaCapacitacionAutoselectDTO>>(SubAreaCapacitacionDB);
                }
                return subAreasCapacitacionAutoSelect;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la SubArea Capacitacion Anterior segun el Id Area Capacitacion Actual
        /// </summary>
        /// <returns></returns>
        public int ObtenerSubAreaCapacitacionAnterior(int subAreaActual)
        {
            try
            {
                int subArea = 0;
                string _query = "SELECT IdActualSubArea,IdAnteriorSubArea FROM pla.V_ObtenerSubAreaCapacitacionAnterior WHERE IdActualSubArea = @IdActualSubArea";
                var registro = _dapper.FirstOrDefault(_query, new { IdActualSubArea= subAreaActual });
                if (!registro.Equals("null"))
                {
                    var areaData = JsonConvert.DeserializeObject<SubAreaTroncalAnteriorDTO>(registro);
                    subArea = areaData.IdAnteriorSubArea;
                }
                return subArea;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<SubAreaCapacitacionPrincipalDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new SubAreaCapacitacionPrincipalDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,
                    IdAreaCapacitacion = y.IdAreaCapacitacion,
                    EsVisibleWeb = y.EsVisibleWeb,
                    IdSubArea = y.IdSubArea,
                    DescripcionHtml = y.DescripcionHtml

                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene registros de ParametroSeo y SubAreaParametroSeo por el Id de SubAreaCapacitacion
        /// </summary>
        /// <param name="idSubAreaCapacitacion"></param>
        /// <returns></returns>
        public List<ParametroContenidoDTO> ObtenerTodoPorIdSubAreaCapacitacion(int idSubAreaCapacitacion)
        {
            try
            {
                List<ParametroContenidoDTO> obtenerTodoIdSubAreaCapacitacion = new List<ParametroContenidoDTO>();
                var _query = "SELECT Id, Nombre , NumeroCaracteres, Contenido  FROM pla.V_ObtenerSubAreaParametrosSeoPorIdSubAreaCapacitacion WHERE IdSubAreaCapacitacion =   @idSubAreaCapacitacion AND  EstadoSubAreaParametroSeoPW = 1 AND EstadoParametroSeoPW = 1 ";
                var obtenerTodoIdSubAreaCapacitacionBO = _dapper.QueryDapper(_query, new { idSubAreaCapacitacion });
                if (!string.IsNullOrEmpty(obtenerTodoIdSubAreaCapacitacionBO) && !obtenerTodoIdSubAreaCapacitacionBO.Contains("[]"))
                {
                    obtenerTodoIdSubAreaCapacitacion = JsonConvert.DeserializeObject<List<ParametroContenidoDTO>>(obtenerTodoIdSubAreaCapacitacionBO);
                }
                return obtenerTodoIdSubAreaCapacitacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene registros de ParametroSeo y SubAreaParametroSeo por el Id de SubAreaCapacitacion
        /// </summary>
        /// <param name="idSubAreaCapacitacion"></param>
        /// <returns></returns>
        public List<SusAreaCapacitacionFacebookDTO> ObtenerSubAreaPorAreaCapacitacionFacebook(int idAreaCapacitacionFacebook)
        {
            try
            {
                List<SusAreaCapacitacionFacebookDTO> lista = new List<SusAreaCapacitacionFacebookDTO>();
                var _query = "SELECT Id, AliasFacebook FROM mkt.V_ObtenerSubAreaPorAreaFacebook WHERE IdAreaCapacitacionFacebook =   @idAreaCapacitacionFacebook AND  EstadoSubAreaCapacitacion = 1 AND EsVisibleWebSubAreaCapacitacion = 1 " +
                    "AND EstadoAreaCapacitacion = 1 AND EsVisibleWebAreaCapacitacion = 1 AND EstadoAreaFacebook = 1";
                var respuesta = _dapper.QueryDapper(_query, new { idAreaCapacitacionFacebook });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<SusAreaCapacitacionFacebookDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: SubAreaCapacitacionRepositorio
        /// Autor: Edgar Serruto
        /// Fecha: 08/07/2021
        /// <summary>
        /// Obtiene registros de IdSubArea y Area Asociada para combo
        /// </summary>
        /// <returns>List<AreaCapacitacionSubAreaCapacitacionFiltroDTO></returns>
        public List<SubAreaCapacitacionAreaCapacitacionFiltroDTO> ObtenerSubAreaAreaParaCombo()
        {
            try
            {
                List<SubAreaCapacitacionAreaCapacitacionFiltroDTO> lista = new List<SubAreaCapacitacionAreaCapacitacionFiltroDTO>();
                var query = "SELECT Id, Nombre, IdAreaCapacitacion FROM pla.V_TSubAreaCapacitacion_FiltroCompuesto";
                var respuesta = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<SubAreaCapacitacionAreaCapacitacionFiltroDTO>>(respuesta);
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
