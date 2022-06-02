using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Repositorio
{
    public class EvaluacionEscalaCalificacionRepositorio : BaseRepository<TEvaluacionEscalaCalificacion, EvaluacionEscalaCalificacionBO>
    {
        #region Metodos Base
        public EvaluacionEscalaCalificacionRepositorio() : base()
        {
        }
        public EvaluacionEscalaCalificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EvaluacionEscalaCalificacionBO> GetBy(Expression<Func<TEvaluacionEscalaCalificacion, bool>> filter)
        {
            IEnumerable<TEvaluacionEscalaCalificacion> listado = base.GetBy(filter);
            List<EvaluacionEscalaCalificacionBO> listadoBO = new List<EvaluacionEscalaCalificacionBO>();
            foreach (var itemEntidad in listado)
            {
                EvaluacionEscalaCalificacionBO objetoBO = Mapper.Map<TEvaluacionEscalaCalificacion, EvaluacionEscalaCalificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EvaluacionEscalaCalificacionBO FirstById(int id)
        {
            try
            {
                TEvaluacionEscalaCalificacion entidad = base.FirstById(id);
                EvaluacionEscalaCalificacionBO objetoBO = new EvaluacionEscalaCalificacionBO();
                Mapper.Map<TEvaluacionEscalaCalificacion, EvaluacionEscalaCalificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EvaluacionEscalaCalificacionBO FirstBy(Expression<Func<TEvaluacionEscalaCalificacion, bool>> filter)
        {
            try
            {
                TEvaluacionEscalaCalificacion entidad = base.FirstBy(filter);
                EvaluacionEscalaCalificacionBO objetoBO = Mapper.Map<TEvaluacionEscalaCalificacion, EvaluacionEscalaCalificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EvaluacionEscalaCalificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEvaluacionEscalaCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EvaluacionEscalaCalificacionBO> listadoBO)
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

        public bool Update(EvaluacionEscalaCalificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEvaluacionEscalaCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EvaluacionEscalaCalificacionBO> listadoBO)
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
        private void AsignacionId(TEvaluacionEscalaCalificacion entidad, EvaluacionEscalaCalificacionBO objetoBO)
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

        private TEvaluacionEscalaCalificacion MapeoEntidad(EvaluacionEscalaCalificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEvaluacionEscalaCalificacion entidad = new TEvaluacionEscalaCalificacion();
                entidad = Mapper.Map<EvaluacionEscalaCalificacionBO, TEvaluacionEscalaCalificacion>(objetoBO,
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

        public EvaluacionEscalaCalificacionBO ObtenerEscalaPorPEspecifico_Presencial(int idPespecifico)
        {
            EvaluacionEscalaCalificacionBO bo = null;
            
            CentroCostoRepositorio _repoCC = new CentroCostoRepositorio();
            var cc = _repoCC.ObtenerDatosCentroCostos(idPespecifico).FirstOrDefault();

            var listado = this.GetBy(w => w.IdModalidadCurso == 0);
            //recorre las escalas de la modalidad
            foreach (var escala_verificar in listado.OrderBy(o => o.Id))
            {
                //identifica cual coincide con el centro de costo
                if (cc.CentroCosto.Contains(escala_verificar.CodigoCiudad))
                    bo = escala_verificar;
            }

            return bo;
        }
    }
}
