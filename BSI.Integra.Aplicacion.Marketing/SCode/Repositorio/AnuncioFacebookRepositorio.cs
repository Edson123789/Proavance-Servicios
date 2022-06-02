using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/AnuncioFacebook
    /// Autor: Gian Miranda
    /// Fecha: 01/06/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_AnuncioFacebook
    /// </summary>
    public class AnuncioFacebookRepositorio : BaseRepository<TAnuncioFacebook, AnuncioFacebookBO>
    {
        #region Metodos Base
        public AnuncioFacebookRepositorio() : base()
        {
        }
        public AnuncioFacebookRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AnuncioFacebookBO> GetBy(Expression<Func<TAnuncioFacebook, bool>> filter)
        {
            IEnumerable<TAnuncioFacebook> listado = base.GetBy(filter);
            List<AnuncioFacebookBO> listadoBO = new List<AnuncioFacebookBO>();
            foreach (var itemEntidad in listado)
            {
                AnuncioFacebookBO objetoBO = Mapper.Map<TAnuncioFacebook, AnuncioFacebookBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AnuncioFacebookBO FirstById(int id)
        {
            try
            {
                TAnuncioFacebook entidad = base.FirstById(id);
                AnuncioFacebookBO objetoBO = new AnuncioFacebookBO();
                Mapper.Map<TAnuncioFacebook, AnuncioFacebookBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AnuncioFacebookBO FirstBy(Expression<Func<TAnuncioFacebook, bool>> filter)
        {
            try
            {
                TAnuncioFacebook entidad = base.FirstBy(filter);
                AnuncioFacebookBO objetoBO = Mapper.Map<TAnuncioFacebook, AnuncioFacebookBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AnuncioFacebookBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAnuncioFacebook entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AnuncioFacebookBO> listadoBO)
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

        public bool Update(AnuncioFacebookBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAnuncioFacebook entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AnuncioFacebookBO> listadoBO)
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
        private void AsignacionId(TAnuncioFacebook entidad, AnuncioFacebookBO objetoBO)
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

        private TAnuncioFacebook MapeoEntidad(AnuncioFacebookBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAnuncioFacebook entidad = new TAnuncioFacebook();
                entidad = Mapper.Map<AnuncioFacebookBO, TAnuncioFacebook>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AnuncioFacebookBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TAnuncioFacebook, bool>>> filters, Expression<Func<TAnuncioFacebook, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TAnuncioFacebook> listado = base.GetFiltered(filters, orderBy, ascending);
            List<AnuncioFacebookBO> listadoBO = new List<AnuncioFacebookBO>();

            foreach (var itemEntidad in listado)
            {
                AnuncioFacebookBO objetoBO = Mapper.Map<TAnuncioFacebook, AnuncioFacebookBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
