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
    public class FormaCalculoEvaluacionRepositorio : BaseRepository<TFormaCalculoEvaluacion, FormaCalculoEvaluacionBO>
    {
        #region Metodos Base
        public FormaCalculoEvaluacionRepositorio() : base()
        {
        }
        public FormaCalculoEvaluacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FormaCalculoEvaluacionBO> GetBy(Expression<Func<TFormaCalculoEvaluacion, bool>> filter)
        {
            IEnumerable<TFormaCalculoEvaluacion> listado = base.GetBy(filter);
            List<FormaCalculoEvaluacionBO> listadoBO = new List<FormaCalculoEvaluacionBO>();
            foreach (var itemEntidad in listado)
            {
                FormaCalculoEvaluacionBO objetoBO = Mapper.Map<TFormaCalculoEvaluacion, FormaCalculoEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FormaCalculoEvaluacionBO FirstById(int id)
        {
            try
            {
                TFormaCalculoEvaluacion entidad = base.FirstById(id);
                FormaCalculoEvaluacionBO objetoBO = new FormaCalculoEvaluacionBO();
                Mapper.Map<TFormaCalculoEvaluacion, FormaCalculoEvaluacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FormaCalculoEvaluacionBO FirstBy(Expression<Func<TFormaCalculoEvaluacion, bool>> filter)
        {
            try
            {
                TFormaCalculoEvaluacion entidad = base.FirstBy(filter);
                FormaCalculoEvaluacionBO objetoBO = Mapper.Map<TFormaCalculoEvaluacion, FormaCalculoEvaluacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FormaCalculoEvaluacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFormaCalculoEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FormaCalculoEvaluacionBO> listadoBO)
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

        public bool Update(FormaCalculoEvaluacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFormaCalculoEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FormaCalculoEvaluacionBO> listadoBO)
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
        private void AsignacionId(TFormaCalculoEvaluacion entidad, FormaCalculoEvaluacionBO objetoBO)
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

        private TFormaCalculoEvaluacion MapeoEntidad(FormaCalculoEvaluacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFormaCalculoEvaluacion entidad = new TFormaCalculoEvaluacion();
                entidad = Mapper.Map<FormaCalculoEvaluacionBO, TFormaCalculoEvaluacion>(objetoBO,
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
            return GetAll().Select(s => new ComboGenericoDTO() {Id = s.Id, Nombre = s.Nombre});
        }
    }
}
