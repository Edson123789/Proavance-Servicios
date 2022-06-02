using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoEtiquetaRepositorio : BaseRepository<TTipoEtiqueta, TipoEtiquetaBO>
    {
        #region Metodos Base
        public TipoEtiquetaRepositorio() : base()
        {
        }
        public TipoEtiquetaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoEtiquetaBO> GetBy(Expression<Func<TTipoEtiqueta, bool>> filter)
        {
            IEnumerable<TTipoEtiqueta> listado = base.GetBy(filter);
            List<TipoEtiquetaBO> listadoBO = new List<TipoEtiquetaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoEtiquetaBO objetoBO = Mapper.Map<TTipoEtiqueta, TipoEtiquetaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoEtiquetaBO FirstById(int id)
        {
            try
            {
                TTipoEtiqueta entidad = base.FirstById(id);
                TipoEtiquetaBO objetoBO = new TipoEtiquetaBO();
                Mapper.Map<TTipoEtiqueta, TipoEtiquetaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoEtiquetaBO FirstBy(Expression<Func<TTipoEtiqueta, bool>> filter)
        {
            try
            {
                TTipoEtiqueta entidad = base.FirstBy(filter);
                TipoEtiquetaBO objetoBO = Mapper.Map<TTipoEtiqueta, TipoEtiquetaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoEtiquetaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoEtiqueta entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoEtiquetaBO> listadoBO)
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

        public bool Update(TipoEtiquetaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoEtiqueta entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoEtiquetaBO> listadoBO)
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
        private void AsignacionId(TTipoEtiqueta entidad, TipoEtiquetaBO objetoBO)
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

        private TTipoEtiqueta MapeoEntidad(TipoEtiquetaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoEtiqueta entidad = new TTipoEtiqueta();
                entidad = Mapper.Map<TipoEtiquetaBO, TTipoEtiqueta>(objetoBO,
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
        /// Obtiene el id y nombre
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerParaFiltro() {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO() {Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
