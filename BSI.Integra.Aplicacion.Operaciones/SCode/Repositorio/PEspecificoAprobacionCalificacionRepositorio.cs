using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class PEspecificoAprobacionCalificacionRepositorio : BaseRepository<TPespecificoAprobacionCalificacion, PEspecificoAprobacionCalificacionBO>
    {
        #region Metodos Base
        public PEspecificoAprobacionCalificacionRepositorio() : base()
        {
        }
        public PEspecificoAprobacionCalificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PEspecificoAprobacionCalificacionBO> GetBy(Expression<Func<TPespecificoAprobacionCalificacion, bool>> filter)
        {
            IEnumerable<TPespecificoAprobacionCalificacion> listado = base.GetBy(filter);
            List<PEspecificoAprobacionCalificacionBO> listadoBO = new List<PEspecificoAprobacionCalificacionBO>();
            foreach (var itemEntidad in listado)
            {
                PEspecificoAprobacionCalificacionBO objetoBO = Mapper.Map<TPespecificoAprobacionCalificacion, PEspecificoAprobacionCalificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PEspecificoAprobacionCalificacionBO FirstById(int id)
        {
            try
            {
                TPespecificoAprobacionCalificacion entidad = base.FirstById(id);
                PEspecificoAprobacionCalificacionBO objetoBO = new PEspecificoAprobacionCalificacionBO();
                Mapper.Map<TPespecificoAprobacionCalificacion, PEspecificoAprobacionCalificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PEspecificoAprobacionCalificacionBO FirstBy(Expression<Func<TPespecificoAprobacionCalificacion, bool>> filter)
        {
            try
            {
                TPespecificoAprobacionCalificacion entidad = base.FirstBy(filter);
                PEspecificoAprobacionCalificacionBO objetoBO = Mapper.Map<TPespecificoAprobacionCalificacion, PEspecificoAprobacionCalificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PEspecificoAprobacionCalificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPespecificoAprobacionCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PEspecificoAprobacionCalificacionBO> listadoBO)
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

        public bool Update(PEspecificoAprobacionCalificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPespecificoAprobacionCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PEspecificoAprobacionCalificacionBO> listadoBO)
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
        private void AsignacionId(TPespecificoAprobacionCalificacion entidad, PEspecificoAprobacionCalificacionBO objetoBO)
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

        private TPespecificoAprobacionCalificacion MapeoEntidad(PEspecificoAprobacionCalificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPespecificoAprobacionCalificacion entidad = new TPespecificoAprobacionCalificacion();
                entidad = Mapper.Map<PEspecificoAprobacionCalificacionBO, TPespecificoAprobacionCalificacion>(objetoBO,
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
