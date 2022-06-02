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
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ArticuloRepositorio : BaseRepository<TArticulo, ArticuloBO>
    {
        #region Metodos Base
        public ArticuloRepositorio() : base()
        {
        }
        public ArticuloRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ArticuloBO> GetBy(Expression<Func<TArticulo, bool>> filter)
        {
            IEnumerable<TArticulo> listado = base.GetBy(filter);
            List<ArticuloBO> listadoBO = new List<ArticuloBO>();
            foreach (var itemEntidad in listado)
            {
                ArticuloBO objetoBO = Mapper.Map<TArticulo, ArticuloBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ArticuloBO FirstById(int id)
        {
            try
            {
                TArticulo entidad = base.FirstById(id);
                ArticuloBO objetoBO = new ArticuloBO();
                Mapper.Map<TArticulo, ArticuloBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ArticuloBO FirstBy(Expression<Func<TArticulo, bool>> filter)
        {
            try
            {
                TArticulo entidad = base.FirstBy(filter);
                ArticuloBO objetoBO = Mapper.Map<TArticulo, ArticuloBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ArticuloBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TArticulo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ArticuloBO> listadoBO)
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

        public bool Update(ArticuloBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TArticulo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ArticuloBO> listadoBO)
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
        private void AsignacionId(TArticulo entidad, ArticuloBO objetoBO)
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

        private TArticulo MapeoEntidad(ArticuloBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TArticulo entidad = new TArticulo();
                entidad = Mapper.Map<ArticuloBO, TArticulo>(objetoBO,
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

        /// Autor: Jorge Rivera
        /// Fecha: 24/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener Registros articulo
        /// </summary>
        /// <param>Paginador,FiltroGrilla</param>
        /// <returns>Objeto</returns>
        public object ObtenerRegistroArticulo(Paginador Paginador, GridFilters FiltroGrilla)
        {
            string condicion = string.Empty;
            string nombre = string.Empty;
            int tipo = 0;
            string autor = string.Empty;
            var query = string.Empty;

            if (FiltroGrilla != null)
            {

                foreach (var item in FiltroGrilla.Filters)
                {
                    if (item.Field == "Nombre" && item.Value.Contains(""))
                    {
                        condicion = condicion + " and Nombre like @nombre ";
                        nombre = item.Value;
                    }
                    if (item.Field == "Autor" && item.Value.Contains(""))
                    {
                        condicion = condicion + " and Autor like @autor ";
                        autor = item.Value;
                    }
                    if (item.Field == "IdTipoArticulo" && item.Value.Contains(""))
                    {
                        if ("WEBINAR".Contains(item.Value.ToUpper()))
                        {
                            tipo = 2;
                        }
                        else if ("WHITE PAPPER".Contains(item.Value.ToUpper()))
                        {
                            tipo = 3;
                        }
                        else if ("BLOG".Contains(item.Value.ToUpper()))
                        {
                            tipo = 1;
                        }
                        condicion = condicion + " and IdTipoArticulo =@IdTipoArticulo ";
                        
                    }
                }
            }

            List<DatosArticuloDTO> _datoArticulos = new List<DatosArticuloDTO>();

            if (Paginador != null && Paginador.take != 0)
            {
                query = "Select Id,IdWeb,Nombre,Titulo,ImgPortada,ImgPortadaAlt,ImgSecundaria,ImgSecundariaAlt,Autor,IdTipoArticulo,Contenido,IdArea,NombreArea,IdSubArea,NombreSubArea," +
                         "IdExpositor,NombreExpositor,IdCategoria,NombreCategoriaPrograma,UrlWeb,UrlDocumento,DescripcionGeneral FROM mkt.V_ObtenerRegistroArticulo WHERE Estado =1 " + condicion + " order by Id desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                var CertificadoSolicitud = _dapper.QueryDapper(query, new { Nombre = "%" + nombre + "%", Autor = "%" + autor + "%", IdTipoArticulo = tipo , Skip = Paginador.skip, Take = Paginador.take });
                if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                {
                    _datoArticulos = JsonConvert.DeserializeObject<List<DatosArticuloDTO>>(CertificadoSolicitud);
                    var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) FROM mkt.V_ObtenerRegistroArticulo WHERE Estado=1 " + condicion, new { Nombre = "%" + nombre + "%", Autor = "%" + autor + "%", IdTipoArticulo = tipo, Skip = Paginador.skip, Take = Paginador.take }));

                    return new { data = _datoArticulos, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                }
            }
            else
            {
                query = "Select Id,IdWeb,Nombre,Titulo,ImgPortada,ImgPortadaAlt,ImgSecundaria,ImgSecundariaAlt,Autor,IdTipoArticulo,Contenido,IdArea,NombreArea,IdSubArea,NombreSubArea," +
                         "IdExpositor,NombreExpositor,IdCategoria,NombreCategoriaPrograma,UrlWeb,UrlDocumento,DescripcionGeneral FROM mkt.V_ObtenerRegistroArticulo WHERE Estado =1 Order by Id desc " + condicion;
                var CertificadoSolicitud = _dapper.QueryDapper(query, new { Nombre = "%" + nombre + "%", Autor = "%" + autor + "%", IdTipoArticulo = tipo, Skip = Paginador.skip, Take = Paginador.take });
                if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                {
                    _datoArticulos = JsonConvert.DeserializeObject<List<DatosArticuloDTO>>(CertificadoSolicitud);
                    var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) FROM mkt.V_ObtenerRegistroArticulo WHERE Estado=1 " + condicion, new { Nombre = "%" + nombre + "%", Autor = "%" + autor + "%", IdTipoArticulo = tipo, Skip = Paginador.skip, Take = Paginador.take }));

                    return new { data = _datoArticulos, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                }
            }
            return new { data = _datoArticulos, Total = 0 };
        }
        public int ObtenerMaximaIdWeb()
        {
            ValorIntDTO rpta = new ValorIntDTO();
            string query = "Select max(IdWeb) AS Valor From pla.T_Articulo Where Estado=1";
            string resultadoQuery = _dapper.FirstOrDefault(query, null);
            if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoQuery);
                return rpta.Valor;
            }
            else
            {
                return 1;
            }
        }

 
        /// Autor: Richard Zenteno
        /// Fecha: 24/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de PGeneral NO asociados a un determinado articulo [Id,Nombre]
        /// </summary>
        /// <param>IdArticulo</param>
        /// <returns>Objeto Lista</returns>
        public List<FiltroDTO> ObtenerProgramasNoAsociadosArticulo(int IdArticulo)
        {
            try
            {
                List<FiltroDTO> tags = new List<FiltroDTO>();
                var query = _dapper.QuerySPDapper("[pla].[SP_ObtenerPGeneralesNoAsociadosArticulo]", new { IdArticulo });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    tags = JsonConvert.DeserializeObject<List<FiltroDTO>>(query);
                }
                return tags;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Richard Zenteno
        /// Fecha: 24/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de PGeneral asociados a un determinado articulo [Id,Nombre]
        /// </summary>
        /// <param>IdArticulo</param>
        /// <returns>Objeto</returns>
        public List<FiltroDTO> ObtenerProgramasAsociadosArticulo(int IdArticulo)
        {
            try
            {
                List<FiltroDTO> tags = new List<FiltroDTO>();
                var query = _dapper.QuerySPDapper("[pla].[SP_ObtenerPGeneralesAsociadosArticulo]", new { IdArticulo });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    tags = JsonConvert.DeserializeObject<List<FiltroDTO>>(query);
                }
                return tags;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
