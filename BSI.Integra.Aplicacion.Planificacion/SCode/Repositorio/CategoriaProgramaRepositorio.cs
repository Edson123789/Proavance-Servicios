using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CategoriaProgramaRepositorio : BaseRepository<TCategoriaPrograma, CategoriaProgramaBO>
    {
        #region Metodos Base
        public CategoriaProgramaRepositorio() : base()
        {
        }
        public CategoriaProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CategoriaProgramaBO> GetBy(Expression<Func<TCategoriaPrograma, bool>> filter)
        {
            IEnumerable<TCategoriaPrograma> listado = base.GetBy(filter);
            List<CategoriaProgramaBO> listadoBO = new List<CategoriaProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                CategoriaProgramaBO objetoBO = Mapper.Map<TCategoriaPrograma, CategoriaProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CategoriaProgramaBO FirstById(int id)
        {
            try
            {
                TCategoriaPrograma entidad = base.FirstById(id);
                CategoriaProgramaBO objetoBO = new CategoriaProgramaBO();
                Mapper.Map<TCategoriaPrograma, CategoriaProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CategoriaProgramaBO FirstBy(Expression<Func<TCategoriaPrograma, bool>> filter)
        {
            try
            {
                TCategoriaPrograma entidad = base.FirstBy(filter);
                CategoriaProgramaBO objetoBO = Mapper.Map<TCategoriaPrograma, CategoriaProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CategoriaProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCategoriaPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CategoriaProgramaBO> listadoBO)
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

        public bool Update(CategoriaProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCategoriaPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CategoriaProgramaBO> listadoBO)
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
        private void AsignacionId(TCategoriaPrograma entidad, CategoriaProgramaBO objetoBO)
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

        private TCategoriaPrograma MapeoEntidad(CategoriaProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCategoriaPrograma entidad = new TCategoriaPrograma();
                entidad = Mapper.Map<CategoriaProgramaBO, TCategoriaPrograma>(objetoBO,
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

        /// Autor: 
        /// Fecha: 24/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda las categoria de datos para llenar combos
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public List<CategoriaProgramaFiltroDTO> ObtenerCategoriasPrograma()
        {
            try
            {
                List<CategoriaProgramaFiltroDTO> categoria = new List<CategoriaProgramaFiltroDTO>();
                string queryCategoriaPrograma = string.Empty;
                queryCategoriaPrograma = "Select Id,Categoria From pla.V_TCategoriaPrograma_Filtro Where Estado=1";
                var resultadoQueryCategoriaPrograma = _dapper.QueryDapper(queryCategoriaPrograma, null);
                if (!string.IsNullOrEmpty(resultadoQueryCategoriaPrograma) && !resultadoQueryCategoriaPrograma.Contains("[]"))
                {
                    categoria = JsonConvert.DeserializeObject<List<CategoriaProgramaFiltroDTO>>(resultadoQueryCategoriaPrograma);
                }
                return categoria;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// Obtiene toda las categoria de datos para llenar combos con los Nombre de las Categoriae
        /// </summary>
        /// <returns></returns>
        public List<CategoriaProgramaFiltroPorNombreDTO> ObtenerCategoriasNombrePrograma()
        {
            try
            {
                List<CategoriaProgramaFiltroPorNombreDTO> categoria = new List<CategoriaProgramaFiltroPorNombreDTO>();
                string _queryCategoriaPrograma = string.Empty;
                _queryCategoriaPrograma = "Select Id,Categoria Nombre From pla.V_TCategoriaPrograma_Filtro Where Estado=1";
                var queryCategoriaPrograma = _dapper.QueryDapper(_queryCategoriaPrograma, null);
                if (!string.IsNullOrEmpty(queryCategoriaPrograma) && !queryCategoriaPrograma.Contains("[]"))
                {
                    categoria = JsonConvert.DeserializeObject<List<CategoriaProgramaFiltroPorNombreDTO>>(queryCategoriaPrograma);
                }
                return categoria;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        ///  Obtiene la lista de categorias de programas registrados en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<CategoriaProgramaDTO> ListarCategoriasProgramasPanel()
        {
            try
            {
                List<CategoriaProgramaDTO> categoriaPrograma = new List<CategoriaProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Categoria, Visible FROM pla.T_CategoriaPrograma WHERE Estado = 1";
                var categoriaprogramaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(categoriaprogramaDB) && !categoriaprogramaDB.Contains("[]"))
                {
                    categoriaPrograma = JsonConvert.DeserializeObject<List<CategoriaProgramaDTO>>(categoriaprogramaDB);
                }

                return categoriaPrograma;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
