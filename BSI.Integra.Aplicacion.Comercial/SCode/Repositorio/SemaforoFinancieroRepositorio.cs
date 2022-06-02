using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class SemaforoFinancieroRepositorio : BaseRepository<TSemaforoFinanciero, SemaforoFinancieroBO>
	{
        #region Metodos Base
        public SemaforoFinancieroRepositorio() : base()
        {
        }
        public SemaforoFinancieroRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<SemaforoFinancieroBO> GetBy(Expression<Func<TSemaforoFinanciero, bool>> filter)
        {
            IEnumerable<TSemaforoFinanciero> listado = base.GetBy(filter);
            List<SemaforoFinancieroBO> listadoBO = new List<SemaforoFinancieroBO>();
            foreach (var itemEntidad in listado)
            {
                SemaforoFinancieroBO objetoBO = Mapper.Map<TSemaforoFinanciero, SemaforoFinancieroBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SemaforoFinancieroBO FirstById(int id)
        {
            try
            {
                TSemaforoFinanciero entidad = base.FirstById(id);
                SemaforoFinancieroBO objetoBO = new SemaforoFinancieroBO();
                Mapper.Map<TSemaforoFinanciero, SemaforoFinancieroBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SemaforoFinancieroBO FirstBy(Expression<Func<TSemaforoFinanciero, bool>> filter)
        {
            try
            {
                TSemaforoFinanciero entidad = base.FirstBy(filter);
                SemaforoFinancieroBO objetoBO = Mapper.Map<TSemaforoFinanciero, SemaforoFinancieroBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SemaforoFinancieroBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSemaforoFinanciero entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SemaforoFinancieroBO> listadoBO)
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

        public bool Update(SemaforoFinancieroBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSemaforoFinanciero entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SemaforoFinancieroBO> listadoBO)
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
        private void AsignacionId(TSemaforoFinanciero entidad, SemaforoFinancieroBO objetoBO)
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

        private TSemaforoFinanciero MapeoEntidad(SemaforoFinancieroBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSemaforoFinanciero entidad = new TSemaforoFinanciero();
                entidad = Mapper.Map<SemaforoFinancieroBO, TSemaforoFinanciero>(objetoBO,
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
