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
    public class SemaforoFinancieroVariableRepositorio : BaseRepository<TSemaforoFinancieroVariable, SemaforoFinancieroVariableBO>
    {
        #region Metodos Base
        public SemaforoFinancieroVariableRepositorio() : base()
        {
        }
        public SemaforoFinancieroVariableRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<SemaforoFinancieroVariableBO> GetBy(Expression<Func<TSemaforoFinancieroVariable, bool>> filter)
        {
            IEnumerable<TSemaforoFinancieroVariable> listado = base.GetBy(filter);
            List<SemaforoFinancieroVariableBO> listadoBO = new List<SemaforoFinancieroVariableBO>();
            foreach (var itemEntidad in listado)
            {
                SemaforoFinancieroVariableBO objetoBO = Mapper.Map<TSemaforoFinancieroVariable, SemaforoFinancieroVariableBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SemaforoFinancieroVariableBO FirstById(int id)
        {
            try
            {
                TSemaforoFinancieroVariable entidad = base.FirstById(id);
                SemaforoFinancieroVariableBO objetoBO = new SemaforoFinancieroVariableBO();
                Mapper.Map<TSemaforoFinancieroVariable, SemaforoFinancieroVariableBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SemaforoFinancieroVariableBO FirstBy(Expression<Func<TSemaforoFinancieroVariable, bool>> filter)
        {
            try
            {
                TSemaforoFinancieroVariable entidad = base.FirstBy(filter);
                SemaforoFinancieroVariableBO objetoBO = Mapper.Map<TSemaforoFinancieroVariable, SemaforoFinancieroVariableBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SemaforoFinancieroVariableBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSemaforoFinancieroVariable entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SemaforoFinancieroVariableBO> listadoBO)
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

        public bool Update(SemaforoFinancieroVariableBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSemaforoFinancieroVariable entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SemaforoFinancieroVariableBO> listadoBO)
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
        private void AsignacionId(TSemaforoFinancieroVariable entidad, SemaforoFinancieroVariableBO objetoBO)
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

        private TSemaforoFinancieroVariable MapeoEntidad(SemaforoFinancieroVariableBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSemaforoFinancieroVariable entidad = new TSemaforoFinancieroVariable();
                entidad = Mapper.Map<SemaforoFinancieroVariableBO, TSemaforoFinancieroVariable>(objetoBO,
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
