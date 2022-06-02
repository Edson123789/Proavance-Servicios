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
    public class RaCursoTrabajoAlumnoTipoRepositorio : BaseRepository<TRaCursoTrabajoAlumnoTipo, RaCursoTrabajoAlumnoTipoBO>
    {
        #region Metodos Base
        public RaCursoTrabajoAlumnoTipoRepositorio() : base()
        {
        }
        public RaCursoTrabajoAlumnoTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaCursoTrabajoAlumnoTipoBO> GetBy(Expression<Func<TRaCursoTrabajoAlumnoTipo, bool>> filter)
        {
            IEnumerable<TRaCursoTrabajoAlumnoTipo> listado = base.GetBy(filter);
            List<RaCursoTrabajoAlumnoTipoBO> listadoBO = new List<RaCursoTrabajoAlumnoTipoBO>();
            foreach (var itemEntidad in listado)
            {
                RaCursoTrabajoAlumnoTipoBO objetoBO = Mapper.Map<TRaCursoTrabajoAlumnoTipo, RaCursoTrabajoAlumnoTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaCursoTrabajoAlumnoTipoBO FirstById(int id)
        {
            try
            {
                TRaCursoTrabajoAlumnoTipo entidad = base.FirstById(id);
                RaCursoTrabajoAlumnoTipoBO objetoBO = new RaCursoTrabajoAlumnoTipoBO();
                Mapper.Map<TRaCursoTrabajoAlumnoTipo, RaCursoTrabajoAlumnoTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaCursoTrabajoAlumnoTipoBO FirstBy(Expression<Func<TRaCursoTrabajoAlumnoTipo, bool>> filter)
        {
            try
            {
                TRaCursoTrabajoAlumnoTipo entidad = base.FirstBy(filter);
                RaCursoTrabajoAlumnoTipoBO objetoBO = Mapper.Map<TRaCursoTrabajoAlumnoTipo, RaCursoTrabajoAlumnoTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaCursoTrabajoAlumnoTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaCursoTrabajoAlumnoTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaCursoTrabajoAlumnoTipoBO> listadoBO)
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

        public bool Update(RaCursoTrabajoAlumnoTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaCursoTrabajoAlumnoTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaCursoTrabajoAlumnoTipoBO> listadoBO)
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
        private void AsignacionId(TRaCursoTrabajoAlumnoTipo entidad, RaCursoTrabajoAlumnoTipoBO objetoBO)
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

        private TRaCursoTrabajoAlumnoTipo MapeoEntidad(RaCursoTrabajoAlumnoTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaCursoTrabajoAlumnoTipo entidad = new TRaCursoTrabajoAlumnoTipo();
                entidad = Mapper.Map<RaCursoTrabajoAlumnoTipoBO, TRaCursoTrabajoAlumnoTipo>(objetoBO,
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
