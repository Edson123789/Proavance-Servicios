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
    /// Repositorio: Planioficacion/EsquemaEvaluacionPgeneralModalidad
    /// Autor:--
    /// Fecha: 01/10/2021
    /// <summary>
    /// Repositorio de las modalidades del esquema de evaluacion general
    /// </summary>
    public class EsquemaEvaluacionPgeneralModalidadRepositorio : BaseRepository<TEsquemaEvaluacionPgeneralModalidad, EsquemaEvaluacionPgeneralModalidadBO>
    {
        #region Metodos Base
        public EsquemaEvaluacionPgeneralModalidadRepositorio() : base()
        {
        }
        public EsquemaEvaluacionPgeneralModalidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EsquemaEvaluacionPgeneralModalidadBO> GetBy(Expression<Func<TEsquemaEvaluacionPgeneralModalidad, bool>> filter)
        {
            IEnumerable<TEsquemaEvaluacionPgeneralModalidad> listado = base.GetBy(filter);
            List<EsquemaEvaluacionPgeneralModalidadBO> listadoBO = new List<EsquemaEvaluacionPgeneralModalidadBO>();
            foreach (var itemEntidad in listado)
            {
                EsquemaEvaluacionPgeneralModalidadBO objetoBO = Mapper.Map<TEsquemaEvaluacionPgeneralModalidad, EsquemaEvaluacionPgeneralModalidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EsquemaEvaluacionPgeneralModalidadBO FirstById(int id)
        {
            try
            {
                TEsquemaEvaluacionPgeneralModalidad entidad = base.FirstById(id);
                EsquemaEvaluacionPgeneralModalidadBO objetoBO = new EsquemaEvaluacionPgeneralModalidadBO();
                Mapper.Map<TEsquemaEvaluacionPgeneralModalidad, EsquemaEvaluacionPgeneralModalidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EsquemaEvaluacionPgeneralModalidadBO FirstBy(Expression<Func<TEsquemaEvaluacionPgeneralModalidad, bool>> filter)
        {
            try
            {
                TEsquemaEvaluacionPgeneralModalidad entidad = base.FirstBy(filter);
                EsquemaEvaluacionPgeneralModalidadBO objetoBO = Mapper.Map<TEsquemaEvaluacionPgeneralModalidad, EsquemaEvaluacionPgeneralModalidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EsquemaEvaluacionPgeneralModalidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEsquemaEvaluacionPgeneralModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EsquemaEvaluacionPgeneralModalidadBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    objetoBO.Id = 0;
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

        public bool Update(EsquemaEvaluacionPgeneralModalidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEsquemaEvaluacionPgeneralModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EsquemaEvaluacionPgeneralModalidadBO> listadoBO)
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
        private void AsignacionId(TEsquemaEvaluacionPgeneralModalidad entidad, EsquemaEvaluacionPgeneralModalidadBO objetoBO)
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

        private TEsquemaEvaluacionPgeneralModalidad MapeoEntidad(EsquemaEvaluacionPgeneralModalidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEsquemaEvaluacionPgeneralModalidad entidad = new TEsquemaEvaluacionPgeneralModalidad();
                entidad = Mapper.Map<EsquemaEvaluacionPgeneralModalidadBO, TEsquemaEvaluacionPgeneralModalidad>(objetoBO,
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
