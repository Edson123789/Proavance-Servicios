using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: FacebookFormularioLeadgen
    /// Autor: Joao Benavente - Ansoli Espinoza - Gian Miranda
    /// Fecha: 04/06/2021
    /// <summary>
    /// Gestion de las consultas a la tabla mkt.T_FacebookFormularioLeadgen
    /// </summary>
    public class FacebookFormularioLeadgenRepositorio : BaseRepository<TFacebookFormularioLeadgen, FacebookFormularioLeadgenBO>
    {
        #region Metodos Base
        public FacebookFormularioLeadgenRepositorio() : base()
        {
        }
        public FacebookFormularioLeadgenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FacebookFormularioLeadgenBO> GetBy(Expression<Func<TFacebookFormularioLeadgen, bool>> filter)
        {
            IEnumerable<TFacebookFormularioLeadgen> listado = base.GetBy(filter);
            List<FacebookFormularioLeadgenBO> listadoBO = new List<FacebookFormularioLeadgenBO>();
            foreach (var itemEntidad in listado)
            {
                FacebookFormularioLeadgenBO objetoBO = Mapper.Map<TFacebookFormularioLeadgen, FacebookFormularioLeadgenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FacebookFormularioLeadgenBO FirstById(int id)
        {
            try
            {
                TFacebookFormularioLeadgen entidad = base.FirstById(id);
                FacebookFormularioLeadgenBO objetoBO = new FacebookFormularioLeadgenBO();
                Mapper.Map<TFacebookFormularioLeadgen, FacebookFormularioLeadgenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FacebookFormularioLeadgenBO FirstBy(Expression<Func<TFacebookFormularioLeadgen, bool>> filter)
        {
            try
            {
                TFacebookFormularioLeadgen entidad = base.FirstBy(filter);
                FacebookFormularioLeadgenBO objetoBO = Mapper.Map<TFacebookFormularioLeadgen, FacebookFormularioLeadgenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FacebookFormularioLeadgenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFacebookFormularioLeadgen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FacebookFormularioLeadgenBO> listadoBO)
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

        public bool Update(FacebookFormularioLeadgenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFacebookFormularioLeadgen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FacebookFormularioLeadgenBO> listadoBO)
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
        private void AsignacionId(TFacebookFormularioLeadgen entidad, FacebookFormularioLeadgenBO objetoBO)
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

        private TFacebookFormularioLeadgen MapeoEntidad(FacebookFormularioLeadgenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFacebookFormularioLeadgen entidad = new TFacebookFormularioLeadgen();
                entidad = Mapper.Map<FacebookFormularioLeadgenBO, TFacebookFormularioLeadgen>(objetoBO,
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
