using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/CampaniaFacebook
    /// Autor: Gian Miranda
    /// Fecha: 12/06/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_CampaniaFacebook
    /// </summary>
    public class CampaniaFacebookRepositorio : BaseRepository<TCampaniaFacebook, CampaniaFacebookBO>
    {
        #region Metodos Base
        public CampaniaFacebookRepositorio() : base()
        {
        }
        public CampaniaFacebookRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampaniaFacebookBO> GetBy(Expression<Func<TCampaniaFacebook, bool>> filter)
        {
            IEnumerable<TCampaniaFacebook> listado = base.GetBy(filter);
            List<CampaniaFacebookBO> listadoBO = new List<CampaniaFacebookBO>();
            foreach (var itemEntidad in listado)
            {
                CampaniaFacebookBO objetoBO = Mapper.Map<TCampaniaFacebook, CampaniaFacebookBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampaniaFacebookBO FirstById(int id)
        {
            try
            {
                TCampaniaFacebook entidad = base.FirstById(id);
                CampaniaFacebookBO objetoBO = new CampaniaFacebookBO();
                Mapper.Map<TCampaniaFacebook, CampaniaFacebookBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampaniaFacebookBO FirstBy(Expression<Func<TCampaniaFacebook, bool>> filter)
        {
            try
            {
                TCampaniaFacebook entidad = base.FirstBy(filter);
                CampaniaFacebookBO objetoBO = Mapper.Map<TCampaniaFacebook, CampaniaFacebookBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampaniaFacebookBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampaniaFacebook entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampaniaFacebookBO> listadoBO)
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

        public bool Update(CampaniaFacebookBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampaniaFacebook entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampaniaFacebookBO> listadoBO)
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
        private void AsignacionId(TCampaniaFacebook entidad, CampaniaFacebookBO objetoBO)
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

        private TCampaniaFacebook MapeoEntidad(CampaniaFacebookBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampaniaFacebook entidad = new TCampaniaFacebook();
                entidad = Mapper.Map<CampaniaFacebookBO, TCampaniaFacebook>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CampaniaFacebookBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCampaniaFacebook, bool>>> filters, Expression<Func<TCampaniaFacebook, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TCampaniaFacebook> listado = base.GetFiltered(filters, orderBy, ascending);
            List<CampaniaFacebookBO> listadoBO = new List<CampaniaFacebookBO>();

            foreach (var itemEntidad in listado)
            {
                CampaniaFacebookBO objetoBO = Mapper.Map<TCampaniaFacebook, CampaniaFacebookBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
