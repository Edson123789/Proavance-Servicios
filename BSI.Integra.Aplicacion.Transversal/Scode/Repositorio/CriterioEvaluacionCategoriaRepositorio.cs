using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CriterioEvaluacionCategoriaRepositorio : BaseRepository<TCriterioEvaluacionCategoria, CriterioEvaluacionCategoriaBO>
    {
        #region Metodos Base
        public CriterioEvaluacionCategoriaRepositorio() : base()
        {
        }
        public CriterioEvaluacionCategoriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CriterioEvaluacionCategoriaBO> GetBy(Expression<Func<TCriterioEvaluacionCategoria, bool>> filter)
        {
            IEnumerable<TCriterioEvaluacionCategoria> listado = base.GetBy(filter);
            List<CriterioEvaluacionCategoriaBO> listadoBO = new List<CriterioEvaluacionCategoriaBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioEvaluacionCategoriaBO objetoBO = Mapper.Map<TCriterioEvaluacionCategoria, CriterioEvaluacionCategoriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IEnumerable<CriterioEvaluacionCategoriaBO> GetBy(Expression<Func<TCriterioEvaluacionCategoria, bool>> filter, int skip, int take)
        {
            IEnumerable<TCriterioEvaluacionCategoria> listado = base.GetBy(filter).Skip(skip).Take(take);
            List<CriterioEvaluacionCategoriaBO> listadoBO = new List<CriterioEvaluacionCategoriaBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioEvaluacionCategoriaBO objetoBO = Mapper.Map<TCriterioEvaluacionCategoria, CriterioEvaluacionCategoriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public CriterioEvaluacionCategoriaBO FirstById(int id)
        {
            try
            {
                TCriterioEvaluacionCategoria entidad = base.FirstById(id);
                CriterioEvaluacionCategoriaBO objetoBO = new CriterioEvaluacionCategoriaBO();
                Mapper.Map<TCriterioEvaluacionCategoria, CriterioEvaluacionCategoriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CriterioEvaluacionCategoriaBO FirstBy(Expression<Func<TCriterioEvaluacionCategoria, bool>> filter)
        {
            try
            {
                TCriterioEvaluacionCategoria entidad = base.FirstBy(filter);
                CriterioEvaluacionCategoriaBO objetoBO = Mapper.Map<TCriterioEvaluacionCategoria, CriterioEvaluacionCategoriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CriterioEvaluacionCategoriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCriterioEvaluacionCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CriterioEvaluacionCategoriaBO> listadoBO)
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

        public bool Update(CriterioEvaluacionCategoriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCriterioEvaluacionCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CriterioEvaluacionCategoriaBO> listadoBO)
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
        private void AsignacionId(TCriterioEvaluacionCategoria entidad, CriterioEvaluacionCategoriaBO objetoBO)
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

        private TCriterioEvaluacionCategoria MapeoEntidad(CriterioEvaluacionCategoriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCriterioEvaluacionCategoria entidad = new TCriterioEvaluacionCategoria();
                entidad = Mapper.Map<CriterioEvaluacionCategoriaBO, TCriterioEvaluacionCategoria>(objetoBO,
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
        /// Obtiene Lista de CiterioEvaluacionCategoria con estado activo
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerFiltro()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CriterioEvaluacionCategoriaDTO> ObtenerTodo()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new CriterioEvaluacionCategoriaDTO { Id = x.Id, Nombre = x.Nombre, Usuario = x.UsuarioModificacion }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
