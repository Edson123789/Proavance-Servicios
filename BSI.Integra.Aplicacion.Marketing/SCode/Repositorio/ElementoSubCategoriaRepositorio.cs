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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ElementoSubCategoriaRepositorio : BaseRepository<TElementoSubCategoria, ElementoSubCategoriaBO>
    {
        #region Metodos Base
        public ElementoSubCategoriaRepositorio() : base()
        {
        }
        public ElementoSubCategoriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ElementoSubCategoriaBO> GetBy(Expression<Func<TElementoSubCategoria, bool>> filter)
        {
            IEnumerable<TElementoSubCategoria> listado = base.GetBy(filter);
            List<ElementoSubCategoriaBO> listadoBO = new List<ElementoSubCategoriaBO>();
            foreach (var itemEntidad in listado)
            {
                ElementoSubCategoriaBO objetoBO = Mapper.Map<TElementoSubCategoria, ElementoSubCategoriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ElementoSubCategoriaBO FirstById(int id)
        {
            try
            {
                TElementoSubCategoria entidad = base.FirstById(id);
                ElementoSubCategoriaBO objetoBO = new ElementoSubCategoriaBO();
                Mapper.Map<TElementoSubCategoria, ElementoSubCategoriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ElementoSubCategoriaBO FirstBy(Expression<Func<TElementoSubCategoria, bool>> filter)
        {
            try
            {
                TElementoSubCategoria entidad = base.FirstBy(filter);
                ElementoSubCategoriaBO objetoBO = Mapper.Map<TElementoSubCategoria, ElementoSubCategoriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ElementoSubCategoriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TElementoSubCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ElementoSubCategoriaBO> listadoBO)
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

        public bool Update(ElementoSubCategoriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TElementoSubCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ElementoSubCategoriaBO> listadoBO)
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
        private void AsignacionId(TElementoSubCategoria entidad, ElementoSubCategoriaBO objetoBO)
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

        private TElementoSubCategoria MapeoEntidad(ElementoSubCategoriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TElementoSubCategoria entidad = new TElementoSubCategoria();
                entidad = Mapper.Map<ElementoSubCategoriaBO, TElementoSubCategoria>(objetoBO,
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

        /// <summary>
        /// Obtiene la lista de nombres de ElementoSubCategoria (Estado=1) registradas en el sistema, y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <returns>Id, Nombre</returns>
        public List<ElementoSubCategoriaDTO> ObtenerElementoSubCategoriaFiltro()
        {
            try
            {
                List<ElementoSubCategoriaDTO> Registros = new List<ElementoSubCategoriaDTO>();
                var _query = "SELECT Id, Nombre, IdElementoCategoria FROM mkt.V_T_ElementoSubCategoriaFiltro";
                var result = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<ElementoSubCategoriaDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Obtiene la lista de registros (Estado=1) de T_ElementoSubCategoria (Usado para el llenado de grilla en su CRUD).
        /// </summary>
        /// <returns></returns>
        public List<ElementoSubCategoriaDTO> ObtenerTodoElementoSubCategoria()
        {
            try
            {
                List<ElementoSubCategoriaDTO> Registros = new List<ElementoSubCategoriaDTO>();
                var _query = "SELECT Id, Nombre, Descripcion, IdElementoCategoria, NombreElementoCategoria FROM mkt.V_T_ElementoSubCategoria";
                var result = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<ElementoSubCategoriaDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Obtiene un unico registro (Estado=1) de T_ElementoSubCategoria (Usado para el llenado de grilla en su CRUD).
        /// </summary>
        /// <returns></returns>
        public List<ElementoSubCategoriaDTO> ObtenerRegistroElementoSubCategoria(int Id)
        {
            try
            {
                List<ElementoSubCategoriaDTO> Registros = new List<ElementoSubCategoriaDTO>();
                var _query = "SELECT Id, Nombre, Descripcion, IdElementoCategoria, NombreElementoCategoria FROM mkt.V_T_ElementoSubCategoria where Id=@Id";
                var result = _dapper.QueryDapper(_query, new { Id = Id});
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<ElementoSubCategoriaDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
