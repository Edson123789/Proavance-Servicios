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
    public class MessengerUsuarioLogRepositorio : BaseRepository<TMessengerUsuarioLog, MessengerUsuarioLogBO>
    {
        #region Metodos Base
        public MessengerUsuarioLogRepositorio() : base()
        {
        }
        public MessengerUsuarioLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MessengerUsuarioLogBO> GetBy(Expression<Func<TMessengerUsuarioLog, bool>> filter)
        {
            IEnumerable<TMessengerUsuarioLog> listado = base.GetBy(filter);
            List<MessengerUsuarioLogBO> listadoBO = new List<MessengerUsuarioLogBO>();
            foreach (var itemEntidad in listado)
            {
                MessengerUsuarioLogBO objetoBO = Mapper.Map<TMessengerUsuarioLog, MessengerUsuarioLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MessengerUsuarioLogBO FirstById(int id)
        {
            try
            {
                TMessengerUsuarioLog entidad = base.FirstById(id);
                MessengerUsuarioLogBO objetoBO = new MessengerUsuarioLogBO();
                Mapper.Map<TMessengerUsuarioLog, MessengerUsuarioLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MessengerUsuarioLogBO FirstBy(Expression<Func<TMessengerUsuarioLog, bool>> filter)
        {
            try
            {
                TMessengerUsuarioLog entidad = base.FirstBy(filter);
                MessengerUsuarioLogBO objetoBO = Mapper.Map<TMessengerUsuarioLog, MessengerUsuarioLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MessengerUsuarioLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMessengerUsuarioLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MessengerUsuarioLogBO> listadoBO)
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

        public bool Update(MessengerUsuarioLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMessengerUsuarioLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MessengerUsuarioLogBO> listadoBO)
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
        private void AsignacionId(TMessengerUsuarioLog entidad, MessengerUsuarioLogBO objetoBO)
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

        private TMessengerUsuarioLog MapeoEntidad(MessengerUsuarioLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMessengerUsuarioLog entidad = new TMessengerUsuarioLog();
                entidad = Mapper.Map<MessengerUsuarioLogBO, TMessengerUsuarioLog>(objetoBO,
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
