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
    public class ParametroEvaluacionRepositorio : BaseRepository<TParametroEvaluacion, ParametroEvaluacionBO>
    {
        #region Metodos Base
        public ParametroEvaluacionRepositorio() : base()
        {
        }
        public ParametroEvaluacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ParametroEvaluacionBO> GetBy(Expression<Func<TParametroEvaluacion, bool>> filter)
        {
            IEnumerable<TParametroEvaluacion> listado = base.GetBy(filter);
            List<ParametroEvaluacionBO> listadoBO = new List<ParametroEvaluacionBO>();
            foreach (var itemEntidad in listado)
            {
                ParametroEvaluacionBO objetoBO = Mapper.Map<TParametroEvaluacion, ParametroEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ParametroEvaluacionBO FirstById(int id)
        {
            try
            {
                TParametroEvaluacion entidad = base.FirstById(id);
                ParametroEvaluacionBO objetoBO = new ParametroEvaluacionBO();
                Mapper.Map<TParametroEvaluacion, ParametroEvaluacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ParametroEvaluacionBO FirstBy(Expression<Func<TParametroEvaluacion, bool>> filter)
        {
            try
            {
                TParametroEvaluacion entidad = base.FirstBy(filter);
                ParametroEvaluacionBO objetoBO = Mapper.Map<TParametroEvaluacion, ParametroEvaluacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ParametroEvaluacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TParametroEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ParametroEvaluacionBO> listadoBO)
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

        public bool Update(ParametroEvaluacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TParametroEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ParametroEvaluacionBO> listadoBO)
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
        private void AsignacionId(TParametroEvaluacion entidad, ParametroEvaluacionBO objetoBO)
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

        private TParametroEvaluacion MapeoEntidad(ParametroEvaluacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TParametroEvaluacion entidad = new TParametroEvaluacion();
                entidad = Mapper.Map<ParametroEvaluacionBO, TParametroEvaluacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ParametroEvaluacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TParametroEvaluacion, bool>>> filters, Expression<Func<TParametroEvaluacion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TParametroEvaluacion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ParametroEvaluacionBO> listadoBO = new List<ParametroEvaluacionBO>();

            foreach (var itemEntidad in listado)
            {
                ParametroEvaluacionBO objetoBO = Mapper.Map<TParametroEvaluacion, ParametroEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
