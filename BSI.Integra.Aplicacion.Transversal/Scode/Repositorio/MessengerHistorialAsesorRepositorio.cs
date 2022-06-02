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
    public class MessengerHistorialAsesorRepositorio : BaseRepository<TMessengerHistorialAsesor, MessengerHistorialAsesorBO>
    {
        #region Metodos Base
        public MessengerHistorialAsesorRepositorio() : base()
        {
        }
        public MessengerHistorialAsesorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MessengerHistorialAsesorBO> GetBy(Expression<Func<TMessengerHistorialAsesor, bool>> filter)
        {
            IEnumerable<TMessengerHistorialAsesor> listado = base.GetBy(filter);
            List<MessengerHistorialAsesorBO> listadoBO = new List<MessengerHistorialAsesorBO>();
            foreach (var itemEntidad in listado)
            {
                MessengerHistorialAsesorBO objetoBO = Mapper.Map<TMessengerHistorialAsesor, MessengerHistorialAsesorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MessengerHistorialAsesorBO FirstById(int id)
        {
            try
            {
                TMessengerHistorialAsesor entidad = base.FirstById(id);
                MessengerHistorialAsesorBO objetoBO = new MessengerHistorialAsesorBO();
                Mapper.Map<TMessengerHistorialAsesor, MessengerHistorialAsesorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MessengerHistorialAsesorBO FirstBy(Expression<Func<TMessengerHistorialAsesor, bool>> filter)
        {
            try
            {
                TMessengerHistorialAsesor entidad = base.FirstBy(filter);
                MessengerHistorialAsesorBO objetoBO = Mapper.Map<TMessengerHistorialAsesor, MessengerHistorialAsesorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MessengerHistorialAsesorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMessengerHistorialAsesor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MessengerHistorialAsesorBO> listadoBO)
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

        public bool Update(MessengerHistorialAsesorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMessengerHistorialAsesor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MessengerHistorialAsesorBO> listadoBO)
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
        private void AsignacionId(TMessengerHistorialAsesor entidad, MessengerHistorialAsesorBO objetoBO)
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

        private TMessengerHistorialAsesor MapeoEntidad(MessengerHistorialAsesorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMessengerHistorialAsesor entidad = new TMessengerHistorialAsesor();
                entidad = Mapper.Map<MessengerHistorialAsesorBO, TMessengerHistorialAsesor>(objetoBO,
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
