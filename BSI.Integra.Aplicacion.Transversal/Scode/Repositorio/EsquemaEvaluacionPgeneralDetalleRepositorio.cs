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
    /// Repositorio: Planioficacion/EsquemaEvaluacionPgeneralDetalle
    /// Autor:--
    /// Fecha: 01/10/2021
    /// <summary>
    /// Repositorio de los detalles del esquema de evaluacion general
    /// </summary>
    public class EsquemaEvaluacionPgeneralDetalleRepositorio : BaseRepository<TEsquemaEvaluacionPgeneralDetalle, EsquemaEvaluacionPgeneralDetalleBO>
    {
        #region Metodos Base
        public EsquemaEvaluacionPgeneralDetalleRepositorio() : base()
        {
        }
        public EsquemaEvaluacionPgeneralDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EsquemaEvaluacionPgeneralDetalleBO> GetBy(Expression<Func<TEsquemaEvaluacionPgeneralDetalle, bool>> filter)
        {
            IEnumerable<TEsquemaEvaluacionPgeneralDetalle> listado = base.GetBy(filter);
            List<EsquemaEvaluacionPgeneralDetalleBO> listadoBO = new List<EsquemaEvaluacionPgeneralDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                EsquemaEvaluacionPgeneralDetalleBO objetoBO = Mapper.Map<TEsquemaEvaluacionPgeneralDetalle, EsquemaEvaluacionPgeneralDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EsquemaEvaluacionPgeneralDetalleBO FirstById(int id)
        {
            try
            {
                TEsquemaEvaluacionPgeneralDetalle entidad = base.FirstById(id);
                EsquemaEvaluacionPgeneralDetalleBO objetoBO = new EsquemaEvaluacionPgeneralDetalleBO();
                Mapper.Map<TEsquemaEvaluacionPgeneralDetalle, EsquemaEvaluacionPgeneralDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EsquemaEvaluacionPgeneralDetalleBO FirstBy(Expression<Func<TEsquemaEvaluacionPgeneralDetalle, bool>> filter)
        {
            try
            {
                TEsquemaEvaluacionPgeneralDetalle entidad = base.FirstBy(filter);
                EsquemaEvaluacionPgeneralDetalleBO objetoBO = Mapper.Map<TEsquemaEvaluacionPgeneralDetalle, EsquemaEvaluacionPgeneralDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EsquemaEvaluacionPgeneralDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEsquemaEvaluacionPgeneralDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EsquemaEvaluacionPgeneralDetalleBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    objetoBO.Id =0;
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

        public bool Update(EsquemaEvaluacionPgeneralDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEsquemaEvaluacionPgeneralDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EsquemaEvaluacionPgeneralDetalleBO> listadoBO)
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
        private void AsignacionId(TEsquemaEvaluacionPgeneralDetalle entidad, EsquemaEvaluacionPgeneralDetalleBO objetoBO)
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

        private TEsquemaEvaluacionPgeneralDetalle MapeoEntidad(EsquemaEvaluacionPgeneralDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEsquemaEvaluacionPgeneralDetalle entidad = new TEsquemaEvaluacionPgeneralDetalle();
                entidad = Mapper.Map<EsquemaEvaluacionPgeneralDetalleBO, TEsquemaEvaluacionPgeneralDetalle>(objetoBO,
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
