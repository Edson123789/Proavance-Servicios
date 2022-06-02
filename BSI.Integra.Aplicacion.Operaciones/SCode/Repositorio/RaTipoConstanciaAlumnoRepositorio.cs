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
    public class RaTipoConstanciaAlumnoRepositorio : BaseRepository<TRaTipoConstanciaAlumno, RaTipoConstanciaAlumnoBO>
    {
        #region Metodos Base
        public RaTipoConstanciaAlumnoRepositorio() : base()
        {
        }
        public RaTipoConstanciaAlumnoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaTipoConstanciaAlumnoBO> GetBy(Expression<Func<TRaTipoConstanciaAlumno, bool>> filter)
        {
            IEnumerable<TRaTipoConstanciaAlumno> listado = base.GetBy(filter);
            List<RaTipoConstanciaAlumnoBO> listadoBO = new List<RaTipoConstanciaAlumnoBO>();
            foreach (var itemEntidad in listado)
            {
                RaTipoConstanciaAlumnoBO objetoBO = Mapper.Map<TRaTipoConstanciaAlumno, RaTipoConstanciaAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaTipoConstanciaAlumnoBO FirstById(int id)
        {
            try
            {
                TRaTipoConstanciaAlumno entidad = base.FirstById(id);
                RaTipoConstanciaAlumnoBO objetoBO = new RaTipoConstanciaAlumnoBO();
                Mapper.Map<TRaTipoConstanciaAlumno, RaTipoConstanciaAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaTipoConstanciaAlumnoBO FirstBy(Expression<Func<TRaTipoConstanciaAlumno, bool>> filter)
        {
            try
            {
                TRaTipoConstanciaAlumno entidad = base.FirstBy(filter);
                RaTipoConstanciaAlumnoBO objetoBO = Mapper.Map<TRaTipoConstanciaAlumno, RaTipoConstanciaAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaTipoConstanciaAlumnoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaTipoConstanciaAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaTipoConstanciaAlumnoBO> listadoBO)
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

        public bool Update(RaTipoConstanciaAlumnoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaTipoConstanciaAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaTipoConstanciaAlumnoBO> listadoBO)
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
        private void AsignacionId(TRaTipoConstanciaAlumno entidad, RaTipoConstanciaAlumnoBO objetoBO)
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

        private TRaTipoConstanciaAlumno MapeoEntidad(RaTipoConstanciaAlumnoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaTipoConstanciaAlumno entidad = new TRaTipoConstanciaAlumno();
                entidad = Mapper.Map<RaTipoConstanciaAlumnoBO, TRaTipoConstanciaAlumno>(objetoBO,
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
