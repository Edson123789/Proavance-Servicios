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
    public class PEspecificoParticipacionExpositorRepositorio : BaseRepository<TPespecificoParticipacionExpositor, PEspecificoParticipacionExpositorBO>
    {
        #region Metodos Base
        public PEspecificoParticipacionExpositorRepositorio() : base()
        {
        }
        public PEspecificoParticipacionExpositorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PEspecificoParticipacionExpositorBO> GetBy(Expression<Func<TPespecificoParticipacionExpositor, bool>> filter)
        {
            IEnumerable<TPespecificoParticipacionExpositor> listado = base.GetBy(filter);
            List<PEspecificoParticipacionExpositorBO> listadoBO = new List<PEspecificoParticipacionExpositorBO>();
            foreach (var itemEntidad in listado)
            {
                PEspecificoParticipacionExpositorBO objetoBO = Mapper.Map<TPespecificoParticipacionExpositor, PEspecificoParticipacionExpositorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PEspecificoParticipacionExpositorBO FirstById(int id)
        {
            try
            {
                TPespecificoParticipacionExpositor entidad = base.FirstById(id);
                PEspecificoParticipacionExpositorBO objetoBO = new PEspecificoParticipacionExpositorBO();
                Mapper.Map<TPespecificoParticipacionExpositor, PEspecificoParticipacionExpositorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PEspecificoParticipacionExpositorBO FirstBy(Expression<Func<TPespecificoParticipacionExpositor, bool>> filter)
        {
            try
            {
                TPespecificoParticipacionExpositor entidad = base.FirstBy(filter);
                PEspecificoParticipacionExpositorBO objetoBO = Mapper.Map<TPespecificoParticipacionExpositor, PEspecificoParticipacionExpositorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PEspecificoParticipacionExpositorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPespecificoParticipacionExpositor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PEspecificoParticipacionExpositorBO> listadoBO)
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

        public bool Update(PEspecificoParticipacionExpositorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPespecificoParticipacionExpositor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PEspecificoParticipacionExpositorBO> listadoBO)
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
        private void AsignacionId(TPespecificoParticipacionExpositor entidad, PEspecificoParticipacionExpositorBO objetoBO)
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

        private TPespecificoParticipacionExpositor MapeoEntidad(PEspecificoParticipacionExpositorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPespecificoParticipacionExpositor entidad = new TPespecificoParticipacionExpositor();
                entidad = Mapper.Map<PEspecificoParticipacionExpositorBO, TPespecificoParticipacionExpositor>(objetoBO,
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
