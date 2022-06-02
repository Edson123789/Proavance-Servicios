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
    public class EsquemaEvaluacionDetalleRepositorio : BaseRepository<TEsquemaEvaluacionDetalle, EsquemaEvaluacionDetalleBO>
    {
        #region Metodos Base
        public EsquemaEvaluacionDetalleRepositorio() : base()
        {
        }
        public EsquemaEvaluacionDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EsquemaEvaluacionDetalleBO> GetBy(Expression<Func<TEsquemaEvaluacionDetalle, bool>> filter)
        {
            IEnumerable<TEsquemaEvaluacionDetalle> listado = base.GetBy(filter);
            List<EsquemaEvaluacionDetalleBO> listadoBO = new List<EsquemaEvaluacionDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                EsquemaEvaluacionDetalleBO objetoBO = Mapper.Map<TEsquemaEvaluacionDetalle, EsquemaEvaluacionDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EsquemaEvaluacionDetalleBO FirstById(int id)
        {
            try
            {
                TEsquemaEvaluacionDetalle entidad = base.FirstById(id);
                EsquemaEvaluacionDetalleBO objetoBO = new EsquemaEvaluacionDetalleBO();
                Mapper.Map<TEsquemaEvaluacionDetalle, EsquemaEvaluacionDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EsquemaEvaluacionDetalleBO FirstBy(Expression<Func<TEsquemaEvaluacionDetalle, bool>> filter)
        {
            try
            {
                TEsquemaEvaluacionDetalle entidad = base.FirstBy(filter);
                EsquemaEvaluacionDetalleBO objetoBO = Mapper.Map<TEsquemaEvaluacionDetalle, EsquemaEvaluacionDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EsquemaEvaluacionDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEsquemaEvaluacionDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EsquemaEvaluacionDetalleBO> listadoBO)
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

        public bool Update(EsquemaEvaluacionDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEsquemaEvaluacionDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EsquemaEvaluacionDetalleBO> listadoBO)
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
        private void AsignacionId(TEsquemaEvaluacionDetalle entidad, EsquemaEvaluacionDetalleBO objetoBO)
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

        private TEsquemaEvaluacionDetalle MapeoEntidad(EsquemaEvaluacionDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEsquemaEvaluacionDetalle entidad = new TEsquemaEvaluacionDetalle();
                entidad = Mapper.Map<EsquemaEvaluacionDetalleBO, TEsquemaEvaluacionDetalle>(objetoBO,
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
