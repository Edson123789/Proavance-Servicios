using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class EstadoChatRepositorio : BaseRepository<TEstadoChat, EstadoChatBO>
    {
        #region Metodos Base
        public EstadoChatRepositorio() : base()
        {
        }
        public EstadoChatRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstadoChatBO> GetBy(Expression<Func<TEstadoChat, bool>> filter)
        {
            IEnumerable<TEstadoChat> listado = base.GetBy(filter);
            List<EstadoChatBO> listadoBO = new List<EstadoChatBO>();
            foreach (var itemEntidad in listado)
            {
                EstadoChatBO objetoBO = Mapper.Map<TEstadoChat, EstadoChatBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstadoChatBO FirstById(int id)
        {
            try
            {
                TEstadoChat entidad = base.FirstById(id);
                EstadoChatBO objetoBO = new EstadoChatBO();
                Mapper.Map<TEstadoChat, EstadoChatBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoChatBO FirstBy(Expression<Func<TEstadoChat, bool>> filter)
        {
            try
            {
                TEstadoChat entidad = base.FirstBy(filter);
                EstadoChatBO objetoBO = Mapper.Map<TEstadoChat, EstadoChatBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstadoChatBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstadoChat entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstadoChatBO> listadoBO)
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

        public bool Update(EstadoChatBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstadoChat entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstadoChatBO> listadoBO)
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
        private void AsignacionId(TEstadoChat entidad, EstadoChatBO objetoBO)
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

        private TEstadoChat MapeoEntidad(EstadoChatBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstadoChat entidad = new TEstadoChat();
                entidad = Mapper.Map<EstadoChatBO, TEstadoChat>(objetoBO,
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
