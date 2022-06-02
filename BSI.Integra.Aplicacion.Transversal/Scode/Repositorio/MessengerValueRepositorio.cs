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
    public class MessengerValueRepositorio : BaseRepository<TMessengerValue, MessengerValueBO>
    {
        #region Metodos Base
        public MessengerValueRepositorio() : base()
        {
        }
        public MessengerValueRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MessengerValueBO> GetBy(Expression<Func<TMessengerValue, bool>> filter)
        {
            IEnumerable<TMessengerValue> listado = base.GetBy(filter);
            List<MessengerValueBO> listadoBO = new List<MessengerValueBO>();
            foreach (var itemEntidad in listado)
            {
                MessengerValueBO objetoBO = Mapper.Map<TMessengerValue, MessengerValueBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MessengerValueBO FirstById(int id)
        {
            try
            {
                TMessengerValue entidad = base.FirstById(id);
                MessengerValueBO objetoBO = new MessengerValueBO();
                Mapper.Map<TMessengerValue, MessengerValueBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MessengerValueBO FirstBy(Expression<Func<TMessengerValue, bool>> filter)
        {
            try
            {
                TMessengerValue entidad = base.FirstBy(filter);
                MessengerValueBO objetoBO = Mapper.Map<TMessengerValue, MessengerValueBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MessengerValueBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMessengerValue entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MessengerValueBO> listadoBO)
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

        public bool Update(MessengerValueBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMessengerValue entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MessengerValueBO> listadoBO)
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
        private void AsignacionId(TMessengerValue entidad, MessengerValueBO objetoBO)
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

        private TMessengerValue MapeoEntidad(MessengerValueBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMessengerValue entidad = new TMessengerValue();
                entidad = Mapper.Map<MessengerValueBO, TMessengerValue>(objetoBO,
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
