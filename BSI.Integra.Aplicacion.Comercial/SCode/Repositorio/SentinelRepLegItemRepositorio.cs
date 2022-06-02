using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class SentinelRepLegItemRepositorio : BaseRepository<TSentinelRepLegItem, SentinelRepLegItemBO>
    {
        #region Metodos Base
        public SentinelRepLegItemRepositorio() : base()
        {
        }
        public SentinelRepLegItemRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SentinelRepLegItemBO> GetBy(Expression<Func<TSentinelRepLegItem, bool>> filter)
        {
            IEnumerable<TSentinelRepLegItem> listado = base.GetBy(filter);
            List<SentinelRepLegItemBO> listadoBO = new List<SentinelRepLegItemBO>();
            foreach (var itemEntidad in listado)
            {
                SentinelRepLegItemBO objetoBO = Mapper.Map<TSentinelRepLegItem, SentinelRepLegItemBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SentinelRepLegItemBO FirstById(int id)
        {
            try
            {
                TSentinelRepLegItem entidad = base.FirstById(id);
                SentinelRepLegItemBO objetoBO = new SentinelRepLegItemBO();
                Mapper.Map<TSentinelRepLegItem, SentinelRepLegItemBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SentinelRepLegItemBO FirstBy(Expression<Func<TSentinelRepLegItem, bool>> filter)
        {
            try
            {
                TSentinelRepLegItem entidad = base.FirstBy(filter);
                SentinelRepLegItemBO objetoBO = Mapper.Map<TSentinelRepLegItem, SentinelRepLegItemBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SentinelRepLegItemBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSentinelRepLegItem entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SentinelRepLegItemBO> listadoBO)
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

        public bool Update(SentinelRepLegItemBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSentinelRepLegItem entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SentinelRepLegItemBO> listadoBO)
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
        private void AsignacionId(TSentinelRepLegItem entidad, SentinelRepLegItemBO objetoBO)
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

        private TSentinelRepLegItem MapeoEntidad(SentinelRepLegItemBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSentinelRepLegItem entidad = new TSentinelRepLegItem();
                entidad = Mapper.Map<SentinelRepLegItemBO, TSentinelRepLegItem>(objetoBO,
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
