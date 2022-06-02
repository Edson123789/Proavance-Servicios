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
    public class RaConstanciaAlumnoRepositorio : BaseRepository<TRaConstanciaAlumno, RaConstanciaAlumnoBO>
    {
        #region Metodos Base
        public RaConstanciaAlumnoRepositorio() : base()
        {
        }
        public RaConstanciaAlumnoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaConstanciaAlumnoBO> GetBy(Expression<Func<TRaConstanciaAlumno, bool>> filter)
        {
            IEnumerable<TRaConstanciaAlumno> listado = base.GetBy(filter);
            List<RaConstanciaAlumnoBO> listadoBO = new List<RaConstanciaAlumnoBO>();
            foreach (var itemEntidad in listado)
            {
                RaConstanciaAlumnoBO objetoBO = Mapper.Map<TRaConstanciaAlumno, RaConstanciaAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaConstanciaAlumnoBO FirstById(int id)
        {
            try
            {
                TRaConstanciaAlumno entidad = base.FirstById(id);
                RaConstanciaAlumnoBO objetoBO = new RaConstanciaAlumnoBO();
                Mapper.Map<TRaConstanciaAlumno, RaConstanciaAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaConstanciaAlumnoBO FirstBy(Expression<Func<TRaConstanciaAlumno, bool>> filter)
        {
            try
            {
                TRaConstanciaAlumno entidad = base.FirstBy(filter);
                RaConstanciaAlumnoBO objetoBO = Mapper.Map<TRaConstanciaAlumno, RaConstanciaAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaConstanciaAlumnoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaConstanciaAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaConstanciaAlumnoBO> listadoBO)
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

        public bool Update(RaConstanciaAlumnoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaConstanciaAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaConstanciaAlumnoBO> listadoBO)
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
        private void AsignacionId(TRaConstanciaAlumno entidad, RaConstanciaAlumnoBO objetoBO)
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

        private TRaConstanciaAlumno MapeoEntidad(RaConstanciaAlumnoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaConstanciaAlumno entidad = new TRaConstanciaAlumno();
                entidad = Mapper.Map<RaConstanciaAlumnoBO, TRaConstanciaAlumno>(objetoBO,
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
