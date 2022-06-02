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
    public class ElementoRepositorio : BaseRepository<TElemento, ElementoBO>
    {
        #region Metodos Base
        public ElementoRepositorio() : base()
        {
        }
        public ElementoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ElementoBO> GetBy(Expression<Func<TElemento, bool>> filter)
        {
            IEnumerable<TElemento> listado = base.GetBy(filter);
            List<ElementoBO> listadoBO = new List<ElementoBO>();
            foreach (var itemEntidad in listado)
            {
                ElementoBO objetoBO = Mapper.Map<TElemento, ElementoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ElementoBO FirstById(int id)
        {
            try
            {
                TElemento entidad = base.FirstById(id);
                ElementoBO objetoBO = new ElementoBO();
                Mapper.Map<TElemento, ElementoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ElementoBO FirstBy(Expression<Func<TElemento, bool>> filter)
        {
            try
            {
                TElemento entidad = base.FirstBy(filter);
                ElementoBO objetoBO = Mapper.Map<TElemento, ElementoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ElementoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TElemento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ElementoBO> listadoBO)
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

        public bool Update(ElementoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TElemento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ElementoBO> listadoBO)
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
        private void AsignacionId(TElemento entidad, ElementoBO objetoBO)
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

        private TElemento MapeoEntidad(ElementoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TElemento entidad = new TElemento();
                entidad = Mapper.Map<ElementoBO, TElemento>(objetoBO,
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
        ///  Obtiene la lista de registros (Estado=1) de T_Elemento (Usado para el llenado de grilla en su CRUD).
        /// </summary>
        /// <returns></returns>
        public List<ElementoDTO> ObtenerTodoElemento()
        {
            try
            {
                List<ElementoDTO> Registros = new List<ElementoDTO>();
                var _query = "SELECT Id, Codigo, Nombre, Descripcion, IdElementoCategoria, NombreElementoCategoria, IdElementoSubCategoria, NombreElementoSubCategoria FROM mkt.V_T_Elemento";
                var result = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<ElementoDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Obtiene un unico registro (Estado=1) de T_Elemento (Usado para el llenado de grilla en su CRUD).
        /// </summary>
        /// <returns></returns>
        public List<ElementoDTO> ObtenerRegistroElemento(int Id)
        {
            try
            {
                List<ElementoDTO> Registros = new List<ElementoDTO>();
                var _query = "SELECT Id, Codigo, Nombre, Descripcion, IdElementoCategoria, NombreElementoCategoria, IdElementoSubCategoria, NombreElementoSubCategoria FROM mkt.V_T_Elemento where Id=@Id";
                var result = _dapper.QueryDapper(_query, new { Id = Id});
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<ElementoDTO>>(result);
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
