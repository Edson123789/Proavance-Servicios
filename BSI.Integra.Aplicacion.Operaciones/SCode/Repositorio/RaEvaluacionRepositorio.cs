using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class RaEvaluacionRepositorio : BaseRepository<TRaEvaluacion, RaEvaluacionBO>
    {
        #region Metodos Base
        public RaEvaluacionRepositorio() : base()
        {
        }
        public RaEvaluacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaEvaluacionBO> GetBy(Expression<Func<TRaEvaluacion, bool>> filter)
        {
            IEnumerable<TRaEvaluacion> listado = base.GetBy(filter);
            List<RaEvaluacionBO> listadoBO = new List<RaEvaluacionBO>();
            foreach (var itemEntidad in listado)
            {
                RaEvaluacionBO objetoBO = Mapper.Map<TRaEvaluacion, RaEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaEvaluacionBO FirstById(int id)
        {
            try
            {
                TRaEvaluacion entidad = base.FirstById(id);
                RaEvaluacionBO objetoBO = new RaEvaluacionBO();
                Mapper.Map<TRaEvaluacion, RaEvaluacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaEvaluacionBO FirstBy(Expression<Func<TRaEvaluacion, bool>> filter)
        {
            try
            {
                TRaEvaluacion entidad = base.FirstBy(filter);
                RaEvaluacionBO objetoBO = Mapper.Map<TRaEvaluacion, RaEvaluacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaEvaluacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaEvaluacionBO> listadoBO)
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

        public bool Update(RaEvaluacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaEvaluacionBO> listadoBO)
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
        private void AsignacionId(TRaEvaluacion entidad, RaEvaluacionBO objetoBO)
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

        private TRaEvaluacion MapeoEntidad(RaEvaluacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaEvaluacion entidad = new TRaEvaluacion();
                entidad = Mapper.Map<RaEvaluacionBO, TRaEvaluacion>(objetoBO,
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

        /// <summary>
        /// Obtiene el listado minimo filtrado por idRaCurso
        /// </summary>
        /// <param name="idRaCurso"></param>
        public List<RaListadoMinimoEvaluacionDTO> ObtenerListadoMinimoPorCurso(int idRaCurso) {
            try
            {
                return this.GetBy(x => x.IdRaCurso == idRaCurso, x => new RaListadoMinimoEvaluacionDTO {Id=x.Id, Nombre = x.Nombre, Porcentaje = x.Porcentaje }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
