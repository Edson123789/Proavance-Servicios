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
    public class SemaforoFinancieroDetalleVariableRepositorio : BaseRepository<TSemaforoFinancieroDetalleVariable, SemaforoFinancieroDetalleVariableBO>
    {
        #region Metodos Base
        public SemaforoFinancieroDetalleVariableRepositorio() : base()
        {
        }
        public SemaforoFinancieroDetalleVariableRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<SemaforoFinancieroDetalleVariableBO> GetBy(Expression<Func<TSemaforoFinancieroDetalleVariable, bool>> filter)
        {
            IEnumerable<TSemaforoFinancieroDetalleVariable> listado = base.GetBy(filter);
            List<SemaforoFinancieroDetalleVariableBO> listadoBO = new List<SemaforoFinancieroDetalleVariableBO>();
            foreach (var itemEntidad in listado)
            {
                SemaforoFinancieroDetalleVariableBO objetoBO = Mapper.Map<TSemaforoFinancieroDetalleVariable, SemaforoFinancieroDetalleVariableBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SemaforoFinancieroDetalleVariableBO FirstById(int id)
        {
            try
            {
                TSemaforoFinancieroDetalleVariable entidad = base.FirstById(id);
                SemaforoFinancieroDetalleVariableBO objetoBO = new SemaforoFinancieroDetalleVariableBO();
                Mapper.Map<TSemaforoFinancieroDetalleVariable, SemaforoFinancieroDetalleVariableBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SemaforoFinancieroDetalleVariableBO FirstBy(Expression<Func<TSemaforoFinancieroDetalleVariable, bool>> filter)
        {
            try
            {
                TSemaforoFinancieroDetalleVariable entidad = base.FirstBy(filter);
                SemaforoFinancieroDetalleVariableBO objetoBO = Mapper.Map<TSemaforoFinancieroDetalleVariable, SemaforoFinancieroDetalleVariableBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SemaforoFinancieroDetalleVariableBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSemaforoFinancieroDetalleVariable entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SemaforoFinancieroDetalleVariableBO> listadoBO)
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

        public bool Update(SemaforoFinancieroDetalleVariableBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSemaforoFinancieroDetalleVariable entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SemaforoFinancieroDetalleVariableBO> listadoBO)
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
        private void AsignacionId(TSemaforoFinancieroDetalleVariable entidad, SemaforoFinancieroDetalleVariableBO objetoBO)
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

        private TSemaforoFinancieroDetalleVariable MapeoEntidad(SemaforoFinancieroDetalleVariableBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSemaforoFinancieroDetalleVariable entidad = new TSemaforoFinancieroDetalleVariable();
                entidad = Mapper.Map<SemaforoFinancieroDetalleVariableBO, TSemaforoFinancieroDetalleVariable>(objetoBO,
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
