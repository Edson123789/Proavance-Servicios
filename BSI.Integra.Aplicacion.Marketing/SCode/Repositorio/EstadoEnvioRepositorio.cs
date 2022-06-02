using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class EstadoEnvioRepositorio : BaseRepository<TEstadoEnvio, EstadoEnvioBO>
    {
        #region Metodos Base
        public EstadoEnvioRepositorio() : base()
        {
        }
        public EstadoEnvioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstadoEnvioBO> GetBy(Expression<Func<TEstadoEnvio, bool>> filter)
        {
            IEnumerable<TEstadoEnvio> listado = base.GetBy(filter);
            List<EstadoEnvioBO> listadoBO = new List<EstadoEnvioBO>();
            foreach (var itemEntidad in listado)
            {
                EstadoEnvioBO objetoBO = Mapper.Map<TEstadoEnvio, EstadoEnvioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstadoEnvioBO FirstById(int id)
        {
            try
            {
                TEstadoEnvio entidad = base.FirstById(id);
                EstadoEnvioBO objetoBO = new EstadoEnvioBO();
                Mapper.Map<TEstadoEnvio, EstadoEnvioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoEnvioBO FirstBy(Expression<Func<TEstadoEnvio, bool>> filter)
        {
            try
            {
                TEstadoEnvio entidad = base.FirstBy(filter);
                EstadoEnvioBO objetoBO = Mapper.Map<TEstadoEnvio, EstadoEnvioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstadoEnvioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstadoEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstadoEnvioBO> listadoBO)
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

        public bool Update(EstadoEnvioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstadoEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstadoEnvioBO> listadoBO)
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
        private void AsignacionId(TEstadoEnvio entidad, EstadoEnvioBO objetoBO)
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

        private TEstadoEnvio MapeoEntidad(EstadoEnvioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstadoEnvio entidad = new TEstadoEnvio();
                entidad = Mapper.Map<EstadoEnvioBO, TEstadoEnvio>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<EstadoEnvioBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TEstadoEnvio, bool>>> filters, Expression<Func<TEstadoEnvio, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TEstadoEnvio> listado = base.GetFiltered(filters, orderBy, ascending);
            List<EstadoEnvioBO> listadoBO = new List<EstadoEnvioBO>();

            foreach (var itemEntidad in listado)
            {
                EstadoEnvioBO objetoBO = Mapper.Map<TEstadoEnvio, EstadoEnvioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
