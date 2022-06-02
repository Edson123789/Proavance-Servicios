using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FormaCalificacionEvaluacionRepositorio : BaseRepository<TFormaCalificacionEvaluacion, FormaCalificacionEvaluacionBO>
    {
        #region Metodos Base
        public FormaCalificacionEvaluacionRepositorio() : base()
        {
        }
        public FormaCalificacionEvaluacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FormaCalificacionEvaluacionBO> GetBy(Expression<Func<TFormaCalificacionEvaluacion, bool>> filter)
        {
            IEnumerable<TFormaCalificacionEvaluacion> listado = base.GetBy(filter);
            List<FormaCalificacionEvaluacionBO> listadoBO = new List<FormaCalificacionEvaluacionBO>();
            foreach (var itemEntidad in listado)
            {
                FormaCalificacionEvaluacionBO objetoBO = Mapper.Map<TFormaCalificacionEvaluacion, FormaCalificacionEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FormaCalificacionEvaluacionBO FirstById(int id)
        {
            try
            {
                TFormaCalificacionEvaluacion entidad = base.FirstById(id);
                FormaCalificacionEvaluacionBO objetoBO = new FormaCalificacionEvaluacionBO();
                Mapper.Map<TFormaCalificacionEvaluacion, FormaCalificacionEvaluacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FormaCalificacionEvaluacionBO FirstBy(Expression<Func<TFormaCalificacionEvaluacion, bool>> filter)
        {
            try
            {
                TFormaCalificacionEvaluacion entidad = base.FirstBy(filter);
                FormaCalificacionEvaluacionBO objetoBO = Mapper.Map<TFormaCalificacionEvaluacion, FormaCalificacionEvaluacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FormaCalificacionEvaluacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFormaCalificacionEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FormaCalificacionEvaluacionBO> listadoBO)
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

        public bool Update(FormaCalificacionEvaluacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFormaCalificacionEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FormaCalificacionEvaluacionBO> listadoBO)
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
        private void AsignacionId(TFormaCalificacionEvaluacion entidad, FormaCalificacionEvaluacionBO objetoBO)
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

        private TFormaCalificacionEvaluacion MapeoEntidad(FormaCalificacionEvaluacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFormaCalificacionEvaluacion entidad = new TFormaCalificacionEvaluacion();
                entidad = Mapper.Map<FormaCalificacionEvaluacionBO, TFormaCalificacionEvaluacion>(objetoBO,
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

        public IEnumerable<ComboGenericoDTO> ObtenerCombo()
        {
            return GetAll().Select(s => new ComboGenericoDTO() { Id = s.Id, Nombre = s.Nombre });
        }
    }
}
