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
    public class CriterioEvaluacionTipoRepositorio : BaseRepository<TCriterioEvaluacionTipo, CriterioEvaluacionTipoBO>
    {
        #region Metodos Base
        public CriterioEvaluacionTipoRepositorio() : base()
        {
        }
        public CriterioEvaluacionTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CriterioEvaluacionTipoBO> GetBy(Expression<Func<TCriterioEvaluacionTipo, bool>> filter)
        {
            IEnumerable<TCriterioEvaluacionTipo> listado = base.GetBy(filter);
            List<CriterioEvaluacionTipoBO> listadoBO = new List<CriterioEvaluacionTipoBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioEvaluacionTipoBO objetoBO = Mapper.Map<TCriterioEvaluacionTipo, CriterioEvaluacionTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CriterioEvaluacionTipoBO FirstById(int id)
        {
            try
            {
                TCriterioEvaluacionTipo entidad = base.FirstById(id);
                CriterioEvaluacionTipoBO objetoBO = new CriterioEvaluacionTipoBO();
                Mapper.Map<TCriterioEvaluacionTipo, CriterioEvaluacionTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CriterioEvaluacionTipoBO FirstBy(Expression<Func<TCriterioEvaluacionTipo, bool>> filter)
        {
            try
            {
                TCriterioEvaluacionTipo entidad = base.FirstBy(filter);
                CriterioEvaluacionTipoBO objetoBO = Mapper.Map<TCriterioEvaluacionTipo, CriterioEvaluacionTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CriterioEvaluacionTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCriterioEvaluacionTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CriterioEvaluacionTipoBO> listadoBO)
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

        public bool Update(CriterioEvaluacionTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCriterioEvaluacionTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CriterioEvaluacionTipoBO> listadoBO)
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
        private void AsignacionId(TCriterioEvaluacionTipo entidad, CriterioEvaluacionTipoBO objetoBO)
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

        private TCriterioEvaluacionTipo MapeoEntidad(CriterioEvaluacionTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCriterioEvaluacionTipo entidad = new TCriterioEvaluacionTipo();
                entidad = Mapper.Map<CriterioEvaluacionTipoBO, TCriterioEvaluacionTipo>(objetoBO,
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
