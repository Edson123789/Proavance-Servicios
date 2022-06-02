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
    public class CategoriaObjetoFiltroRepositorio : BaseRepository<TCategoriaObjetoFiltro, CategoriaObjetoFiltroBO>
    {
        #region Metodos Base
        public CategoriaObjetoFiltroRepositorio() : base()
        {
        }
        public CategoriaObjetoFiltroRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CategoriaObjetoFiltroBO> GetBy(Expression<Func<TCategoriaObjetoFiltro, bool>> filter)
        {
            IEnumerable<TCategoriaObjetoFiltro> listado = base.GetBy(filter);
            List<CategoriaObjetoFiltroBO> listadoBO = new List<CategoriaObjetoFiltroBO>();
            foreach (var itemEntidad in listado)
            {
                CategoriaObjetoFiltroBO objetoBO = Mapper.Map<TCategoriaObjetoFiltro, CategoriaObjetoFiltroBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CategoriaObjetoFiltroBO FirstById(int id)
        {
            try
            {
                TCategoriaObjetoFiltro entidad = base.FirstById(id);
                CategoriaObjetoFiltroBO objetoBO = new CategoriaObjetoFiltroBO();
                Mapper.Map<TCategoriaObjetoFiltro, CategoriaObjetoFiltroBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CategoriaObjetoFiltroBO FirstBy(Expression<Func<TCategoriaObjetoFiltro, bool>> filter)
        {
            try
            {
                TCategoriaObjetoFiltro entidad = base.FirstBy(filter);
                CategoriaObjetoFiltroBO objetoBO = Mapper.Map<TCategoriaObjetoFiltro, CategoriaObjetoFiltroBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CategoriaObjetoFiltroBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCategoriaObjetoFiltro entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CategoriaObjetoFiltroBO> listadoBO)
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

        public bool Update(CategoriaObjetoFiltroBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCategoriaObjetoFiltro entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CategoriaObjetoFiltroBO> listadoBO)
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
        private void AsignacionId(TCategoriaObjetoFiltro entidad, CategoriaObjetoFiltroBO objetoBO)
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

        private TCategoriaObjetoFiltro MapeoEntidad(CategoriaObjetoFiltroBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCategoriaObjetoFiltro entidad = new TCategoriaObjetoFiltro();
                entidad = Mapper.Map<CategoriaObjetoFiltroBO, TCategoriaObjetoFiltro>(objetoBO,
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
        /// Obtiene todos los filtros disponibles para conjunto lista
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerParaConjuntoAnuncio() {
            return this.GetBy(x => x.AplicaConjuntoLista, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
        }
    }
}
