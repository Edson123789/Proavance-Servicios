using System;
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
    public class WebinarExcluirRepositorio : BaseRepository<TWebinarExcluir, WebinarExcluirBO>
    {
        #region Metodos Base
        public WebinarExcluirRepositorio() : base()
        {
        }
        public WebinarExcluirRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WebinarExcluirBO> GetBy(Expression<Func<TWebinarExcluir, bool>> filter)
        {
            IEnumerable<TWebinarExcluir> listado = base.GetBy(filter);
            List<WebinarExcluirBO> listadoBO = new List<WebinarExcluirBO>();
            foreach (var itemEntidad in listado)
            {
                WebinarExcluirBO objetoBO = Mapper.Map<TWebinarExcluir, WebinarExcluirBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WebinarExcluirBO FirstById(int id)
        {
            try
            {
                TWebinarExcluir entidad = base.FirstById(id);
                WebinarExcluirBO objetoBO = new WebinarExcluirBO();
                Mapper.Map<TWebinarExcluir, WebinarExcluirBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WebinarExcluirBO FirstBy(Expression<Func<TWebinarExcluir, bool>> filter)
        {
            try
            {
                TWebinarExcluir entidad = base.FirstBy(filter);
                WebinarExcluirBO objetoBO = Mapper.Map<TWebinarExcluir, WebinarExcluirBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WebinarExcluirBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWebinarExcluir entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WebinarExcluirBO> listadoBO)
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

        public bool Update(WebinarExcluirBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWebinarExcluir entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WebinarExcluirBO> listadoBO)
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
        private void AsignacionId(TWebinarExcluir entidad, WebinarExcluirBO objetoBO)
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

        private TWebinarExcluir MapeoEntidad(WebinarExcluirBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWebinarExcluir entidad = new TWebinarExcluir();
                entidad = Mapper.Map<WebinarExcluirBO, TWebinarExcluir>(objetoBO,
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

    }
}
