using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FacebookUsuarioOportunidadRepositorio : BaseRepository<TFacebookUsuarioOportunidad, FacebookUsuarioOportunidadBO>
    {
        #region Metodos Base
        public FacebookUsuarioOportunidadRepositorio() : base()
        {
        }
        public FacebookUsuarioOportunidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FacebookUsuarioOportunidadBO> GetBy(Expression<Func<TFacebookUsuarioOportunidad, bool>> filter)
        {
            IEnumerable<TFacebookUsuarioOportunidad> listado = base.GetBy(filter);
            List<FacebookUsuarioOportunidadBO> listadoBO = new List<FacebookUsuarioOportunidadBO>();
            foreach (var itemEntidad in listado)
            {
                FacebookUsuarioOportunidadBO objetoBO = Mapper.Map<TFacebookUsuarioOportunidad, FacebookUsuarioOportunidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FacebookUsuarioOportunidadBO FirstById(int id)
        {
            try
            {
                TFacebookUsuarioOportunidad entidad = base.FirstById(id);
                FacebookUsuarioOportunidadBO objetoBO = new FacebookUsuarioOportunidadBO();
                Mapper.Map<TFacebookUsuarioOportunidad, FacebookUsuarioOportunidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FacebookUsuarioOportunidadBO FirstBy(Expression<Func<TFacebookUsuarioOportunidad, bool>> filter)
        {
            try
            {
                TFacebookUsuarioOportunidad entidad = base.FirstBy(filter);
                FacebookUsuarioOportunidadBO objetoBO = Mapper.Map<TFacebookUsuarioOportunidad, FacebookUsuarioOportunidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FacebookUsuarioOportunidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFacebookUsuarioOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FacebookUsuarioOportunidadBO> listadoBO)
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

        public bool Update(FacebookUsuarioOportunidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFacebookUsuarioOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FacebookUsuarioOportunidadBO> listadoBO)
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
        private void AsignacionId(TFacebookUsuarioOportunidad entidad, FacebookUsuarioOportunidadBO objetoBO)
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

        private TFacebookUsuarioOportunidad MapeoEntidad(FacebookUsuarioOportunidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFacebookUsuarioOportunidad entidad = new TFacebookUsuarioOportunidad();
                entidad = Mapper.Map<FacebookUsuarioOportunidadBO, TFacebookUsuarioOportunidad>(objetoBO,
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
