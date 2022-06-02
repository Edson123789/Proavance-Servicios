using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class UnsuscribedMailchimpRepositorio : BaseRepository<TUnsuscribedMailchimp, UnsuscribedMailchimpBO>
    {
        #region Metodos Base
        public UnsuscribedMailchimpRepositorio() : base()
        {
        }
        public UnsuscribedMailchimpRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<UnsuscribedMailchimpBO> GetBy(Expression<Func<TUnsuscribedMailchimp, bool>> filter)
        {
            IEnumerable<TUnsuscribedMailchimp> listado = base.GetBy(filter);
            List<UnsuscribedMailchimpBO> listadoBO = new List<UnsuscribedMailchimpBO>();
            foreach (var itemEntidad in listado)
            {
                UnsuscribedMailchimpBO objetoBO = Mapper.Map<TUnsuscribedMailchimp, UnsuscribedMailchimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public UnsuscribedMailchimpBO FirstById(int id)
        {
            try
            {
                TUnsuscribedMailchimp entidad = base.FirstById(id);
                UnsuscribedMailchimpBO objetoBO = new UnsuscribedMailchimpBO();
                Mapper.Map<TUnsuscribedMailchimp, UnsuscribedMailchimpBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public UnsuscribedMailchimpBO FirstBy(Expression<Func<TUnsuscribedMailchimp, bool>> filter)
        {
            try
            {
                TUnsuscribedMailchimp entidad = base.FirstBy(filter);
                UnsuscribedMailchimpBO objetoBO = Mapper.Map<TUnsuscribedMailchimp, UnsuscribedMailchimpBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(UnsuscribedMailchimpBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TUnsuscribedMailchimp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<UnsuscribedMailchimpBO> listadoBO)
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

        public bool Update(UnsuscribedMailchimpBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TUnsuscribedMailchimp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<UnsuscribedMailchimpBO> listadoBO)
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
        private void AsignacionId(TUnsuscribedMailchimp entidad, UnsuscribedMailchimpBO objetoBO)
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

        private TUnsuscribedMailchimp MapeoEntidad(UnsuscribedMailchimpBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TUnsuscribedMailchimp entidad = new TUnsuscribedMailchimp();
                entidad = Mapper.Map<UnsuscribedMailchimpBO, TUnsuscribedMailchimp>(objetoBO,
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
        /// Validad si existe un registro con el email registrada en la tabla TUnsuscribedMailchimp
        /// </summary>
        /// <returns></returns>
        public bool ValidadExisteEmail(string email)
        {
            try
            {
                var valor = FirstBy(x => x.Email == email);
                if (valor == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Actualiza a desuscrito alumno integra
        /// </summary>
        /// <returns></returns>
        public bool ActualizarAlumnoDesuscritos()
        {
            try
            {
                string _queryDesuscrito = "mkt.SP_ActualizarAlumnoDesuscrito";
                var queryDesuscrito = _dapper.QuerySPDapper(_queryDesuscrito, null);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
