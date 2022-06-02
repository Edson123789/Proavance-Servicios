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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class WebinarCentroCostoRepositorio : BaseRepository<TWebinarCentroCosto, WebinarCentroCostoBO>
    {
        #region Metodos Base
        public WebinarCentroCostoRepositorio() : base()
        {
        }
        public WebinarCentroCostoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WebinarCentroCostoBO> GetBy(Expression<Func<TWebinarCentroCosto, bool>> filter)
        {
            IEnumerable<TWebinarCentroCosto> listado = base.GetBy(filter);
            List<WebinarCentroCostoBO> listadoBO = new List<WebinarCentroCostoBO>();
            foreach (var itemEntidad in listado)
            {
                WebinarCentroCostoBO objetoBO = Mapper.Map<TWebinarCentroCosto, WebinarCentroCostoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WebinarCentroCostoBO FirstById(int id)
        {
            try
            {
                TWebinarCentroCosto entidad = base.FirstById(id);
                WebinarCentroCostoBO objetoBO = new WebinarCentroCostoBO();
                Mapper.Map<TWebinarCentroCosto, WebinarCentroCostoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WebinarCentroCostoBO FirstBy(Expression<Func<TWebinarCentroCosto, bool>> filter)
        {
            try
            {
                TWebinarCentroCosto entidad = base.FirstBy(filter);
                WebinarCentroCostoBO objetoBO = Mapper.Map<TWebinarCentroCosto, WebinarCentroCostoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WebinarCentroCostoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWebinarCentroCosto entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WebinarCentroCostoBO> listadoBO)
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

        public bool Update(WebinarCentroCostoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWebinarCentroCosto entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WebinarCentroCostoBO> listadoBO)
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
        private void AsignacionId(TWebinarCentroCosto entidad, WebinarCentroCostoBO objetoBO)
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

        private TWebinarCentroCosto MapeoEntidad(WebinarCentroCostoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWebinarCentroCosto entidad = new TWebinarCentroCosto();
                entidad = Mapper.Map<WebinarCentroCostoBO, TWebinarCentroCosto>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<WebinarCentroCostoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TWebinarCentroCosto, bool>>> filters, Expression<Func<TWebinarCentroCosto, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TWebinarCentroCosto> listado = base.GetFiltered(filters, orderBy, ascending);
            List<WebinarCentroCostoBO> listadoBO = new List<WebinarCentroCostoBO>();

            foreach (var itemEntidad in listado)
            {
                WebinarCentroCostoBO objetoBO = Mapper.Map<TWebinarCentroCosto, WebinarCentroCostoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los centro de costos asociados a un webinar
        /// </summary>
        /// <param name="idWebinar"></param>
        /// <returns></returns>
        public List<WebinarCentroCostoBO> Obtener(int idWebinar) {
            try
            {
                return this.GetBy(x => x.Estado && x.IdWebinar == idWebinar).ToList();
            }
            catch (Exception e) 
            {
                throw e;
            }
        }
    }
}
