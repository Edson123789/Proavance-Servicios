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
    /// Repositorio: AreaCapacitacionRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Área de Capacitación
    /// </summary>
    public class AreaCapacitacionRepositorio : BaseRepository<TAreaCapacitacion, AreaCapacitacionBO>
    {
        #region Metodos Base
        public AreaCapacitacionRepositorio() : base()
        {
        }
        public AreaCapacitacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AreaCapacitacionBO> GetBy(Expression<Func<TAreaCapacitacion, bool>> filter)
        {
            IEnumerable<TAreaCapacitacion> listado = base.GetBy(filter);
            List<AreaCapacitacionBO> listadoBO = new List<AreaCapacitacionBO>();
            foreach (var itemEntidad in listado)
            {
                AreaCapacitacionBO objetoBO = Mapper.Map<TAreaCapacitacion, AreaCapacitacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AreaCapacitacionBO FirstById(int id)
        {
            try
            {
                TAreaCapacitacion entidad = base.FirstById(id);
                AreaCapacitacionBO objetoBO = new AreaCapacitacionBO();
                Mapper.Map<TAreaCapacitacion, AreaCapacitacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AreaCapacitacionBO FirstBy(Expression<Func<TAreaCapacitacion, bool>> filter)
        {
            try
            {
                TAreaCapacitacion entidad = base.FirstBy(filter);
                AreaCapacitacionBO objetoBO = Mapper.Map<TAreaCapacitacion, AreaCapacitacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AreaCapacitacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAreaCapacitacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AreaCapacitacionBO> listadoBO)
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

        public bool Update(AreaCapacitacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAreaCapacitacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AreaCapacitacionBO> listadoBO)
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
        private void AsignacionId(TAreaCapacitacion entidad, AreaCapacitacionBO objetoBO)
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

        private TAreaCapacitacion MapeoEntidad(AreaCapacitacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAreaCapacitacion entidad = new TAreaCapacitacion();
                entidad = Mapper.Map<AreaCapacitacionBO, TAreaCapacitacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //Mapea los hijos
                if (objetoBO.AreaParametroSeoPw != null && objetoBO.AreaParametroSeoPw.Count > 0)
                {
                    foreach (var hijo in objetoBO.AreaParametroSeoPw)
                    {
                        TAreaParametroSeoPw entidadHijo = new TAreaParametroSeoPw();
                        entidadHijo = Mapper.Map<AreaParametroSeoPwBO, TAreaParametroSeoPw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TAreaParametroSeoPw.Add(entidadHijo);
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
        /// Obtiene el Id,Nombre de la area de capacitacion para ser listada
        /// </summary>
        /// <returns>Lista de objetos de clase FiltroDTO</returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<FiltroDTO> areasCapacitacionFiltro = new List<FiltroDTO>();
                var queryFiltroAreaCapacitacion = "SELECT Id, Nombre FROM pla.V_RegistrosFiltroAreaCapacitacion WHERE Estado = 1";
                var listaRegistros = _dapper.QueryDapper(queryFiltroAreaCapacitacion, null);
                if (!string.IsNullOrEmpty(listaRegistros) && !listaRegistros.Contains("[]"))
                {
                    areasCapacitacionFiltro = JsonConvert.DeserializeObject<List<FiltroDTO>>(listaRegistros);
                }
                return areasCapacitacionFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id,Nombre de la area de capacitacion para ser listada
        /// </summary>
        /// <returns>Lista de objetos (FiltroDTO)</returns>
        public List<FiltroDTO> ObtenerTodoFiltroWeb()
        {
            try
            {
                List<FiltroDTO> areasCapacitacionFiltro = new List<FiltroDTO>();
                var queryFiltroAreaCapacitacion = "SELECT Id, Nombre FROM pla.V_RegistrosFiltroAreaCapacitacionWeb";
                var listaRegistros = _dapper.QueryDapper(queryFiltroAreaCapacitacion, null);
                if (!string.IsNullOrEmpty(listaRegistros) && !listaRegistros.Contains("[]"))
                {
                    areasCapacitacionFiltro = JsonConvert.DeserializeObject<List<FiltroDTO>>(listaRegistros);
                }

                return areasCapacitacionFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Area Capacitacion Anterior segun el Id Area Capacitacion Actual
        /// </summary>
        /// <returns></returns>
        public int ObtenerAreaCapacitacionAnterior(int areaActual)
        {
            try
            {
                int area = 0;
                string _queryFiltroAreaCapacitacion = string.Empty;
                string _query = "SELECT IdActualArea,IdAnteriorArea FROM pla.V_ObtenerAreaCapacitacionAnterior WHERE IdActualArea = @IdActualArea";
                var registro = _dapper.FirstOrDefault(_query, new { IdActualArea = areaActual });
                if (!registro.Equals("null"))
                {
                    var areaData = JsonConvert.DeserializeObject<AreaTroncalAnteriorDTO>(registro);
                    area = areaData.IdAnteriorArea;
                }
                return area;
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
        public List<AreaCapacitacionDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new AreaCapacitacionDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,
                    DescripcionHtml = y.DescripcionHtml,
                    ImgPortada = y.ImgPortada,
                    ImgSecundaria = y.ImgSecundaria,
                    ImgPortadaAlt = y.ImgPortadaAlt,
                    ImgSecundariaAlt = y.ImgSecundariaAlt,
                    EsVisibleWeb = y.EsVisibleWeb,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///  Obtiene todos los registros sin los campos de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<AreaCapacitacionDTO> ObtenerTodoGridArea()
        {
            try
            {
                var lista = GetBy(x => true, y => new AreaCapacitacionDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,
                    DescripcionHtml = y.DescripcionHtml,
                    ImgPortada = y.ImgPortada,
                    ImgSecundaria = y.ImgSecundaria,
                    ImgPortadaAlt = y.ImgPortadaAlt,
                    ImgSecundariaAlt = y.ImgSecundariaAlt,
                    EsVisibleWeb = y.EsVisibleWeb,
                    IdArea = y.IdArea,
                    IdAreaCapacitacionFacebook = y.IdAreaCapacitacionFacebook
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: 
        /// Fecha: 24/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de AreaCapacitacion con los campos de Id, Nombre y IdAreaCapacitacionFacebook.
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public List<AreaCapacitacionDatosFiltroDTO> ObtenerAreaCapacitacionFiltro()
        {
            try
            {
                var lista = GetBy(x => true, y => new AreaCapacitacionDatosFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    IdAreaCapacitacionFacebook = y.IdAreaCapacitacionFacebook,
                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
