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
    public class ElementoCategoriaRepositorio : BaseRepository<TElementoCategoria, ElementoCategoriaBO>
    {
        #region Metodos Base
        public ElementoCategoriaRepositorio() : base()
        {
        }
        public ElementoCategoriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ElementoCategoriaBO> GetBy(Expression<Func<TElementoCategoria, bool>> filter)
        {
            IEnumerable<TElementoCategoria> listado = base.GetBy(filter);
            List<ElementoCategoriaBO> listadoBO = new List<ElementoCategoriaBO>();
            foreach (var itemEntidad in listado)
            {
                ElementoCategoriaBO objetoBO = Mapper.Map<TElementoCategoria, ElementoCategoriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ElementoCategoriaBO FirstById(int id)
        {
            try
            {
                TElementoCategoria entidad = base.FirstById(id);
                ElementoCategoriaBO objetoBO = new ElementoCategoriaBO();
                Mapper.Map<TElementoCategoria, ElementoCategoriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ElementoCategoriaBO FirstBy(Expression<Func<TElementoCategoria, bool>> filter)
        {
            try
            {
                TElementoCategoria entidad = base.FirstBy(filter);
                ElementoCategoriaBO objetoBO = Mapper.Map<TElementoCategoria, ElementoCategoriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ElementoCategoriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TElementoCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ElementoCategoriaBO> listadoBO)
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

        public bool Update(ElementoCategoriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TElementoCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ElementoCategoriaBO> listadoBO)
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
        private void AsignacionId(TElementoCategoria entidad, ElementoCategoriaBO objetoBO)
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

        private TElementoCategoria MapeoEntidad(ElementoCategoriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TElementoCategoria entidad = new TElementoCategoria();
                entidad = Mapper.Map<ElementoCategoriaBO, TElementoCategoria>(objetoBO,
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
        /// Obtiene la lista de nombres de ElementoCategoria (Estado=1) registradas en el sistema, y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerElementoCategoriaFiltro()
        {
            try
            {
                List<FiltroDTO> Registros = new List<FiltroDTO>();
                var _query = "SELECT Id, Nombre FROM mkt.V_T_ElementoCategoriaFiltro";
                var result = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<FiltroDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        ///  Obtiene la lista de registrosa (Estado=1) de T_ElementoCategoria,  (Usado para el llenado de grilla en su CRUD).
        /// </summary>
        /// <returns></returns>
        public List<ElementoCategoriaDTO> ObtenerTodoElementoCategoria()
        {
            try
            {
                List<ElementoCategoriaDTO> Registros = new List<ElementoCategoriaDTO>();
                var _query = "SELECT Id, Nombre, Descripcion FROM mkt.V_T_ElementoCategoria";
                var result = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<ElementoCategoriaDTO>>(result);
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
