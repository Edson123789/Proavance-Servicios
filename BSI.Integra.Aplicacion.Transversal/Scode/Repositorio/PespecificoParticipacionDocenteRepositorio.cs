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
    public class PespecificoParticipacionDocenteRepositorio : BaseRepository<TPespecificoParticipacionDocente, PespecificoParticipacionDocenteBO>
    {
        #region Metodos Base
        public PespecificoParticipacionDocenteRepositorio() : base()
        {
        }
        public PespecificoParticipacionDocenteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PespecificoParticipacionDocenteBO> GetBy(Expression<Func<TPespecificoParticipacionDocente, bool>> filter)
        {
            IEnumerable<TPespecificoParticipacionDocente> listado = base.GetBy(filter);
            List<PespecificoParticipacionDocenteBO> listadoBO = new List<PespecificoParticipacionDocenteBO>();
            foreach (var itemEntidad in listado)
            {
                PespecificoParticipacionDocenteBO objetoBO = Mapper.Map<TPespecificoParticipacionDocente, PespecificoParticipacionDocenteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PespecificoParticipacionDocenteBO FirstById(int id)
        {
            try
            {
                TPespecificoParticipacionDocente entidad = base.FirstById(id);
                PespecificoParticipacionDocenteBO objetoBO = new PespecificoParticipacionDocenteBO();
                Mapper.Map<TPespecificoParticipacionDocente, PespecificoParticipacionDocenteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PespecificoParticipacionDocenteBO FirstBy(Expression<Func<TPespecificoParticipacionDocente, bool>> filter)
        {
            try
            {
                TPespecificoParticipacionDocente entidad = base.FirstBy(filter);
                PespecificoParticipacionDocenteBO objetoBO = Mapper.Map<TPespecificoParticipacionDocente, PespecificoParticipacionDocenteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PespecificoParticipacionDocenteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPespecificoParticipacionDocente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PespecificoParticipacionDocenteBO> listadoBO)
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

        public bool Update(PespecificoParticipacionDocenteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPespecificoParticipacionDocente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PespecificoParticipacionDocenteBO> listadoBO)
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
        private void AsignacionId(TPespecificoParticipacionDocente entidad, PespecificoParticipacionDocenteBO objetoBO)
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

        private TPespecificoParticipacionDocente MapeoEntidad(PespecificoParticipacionDocenteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPespecificoParticipacionDocente entidad = new TPespecificoParticipacionDocente();
                entidad = Mapper.Map<PespecificoParticipacionDocenteBO, TPespecificoParticipacionDocente>(objetoBO,
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
