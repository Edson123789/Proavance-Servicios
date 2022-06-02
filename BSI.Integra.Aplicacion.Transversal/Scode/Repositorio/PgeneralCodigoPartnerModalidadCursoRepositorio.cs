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
    public class PgeneralCodigoPartnerModalidadCursoRepositorio : BaseRepository<TPgeneralCodigoPartnerModalidadCurso, PgeneralCodigoPartnerModalidadCursoBO>
    {
        #region Metodos Base
        public PgeneralCodigoPartnerModalidadCursoRepositorio() : base()
        {
        }
        public PgeneralCodigoPartnerModalidadCursoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralCodigoPartnerModalidadCursoBO> GetBy(Expression<Func<TPgeneralCodigoPartnerModalidadCurso, bool>> filter)
        {
            IEnumerable<TPgeneralCodigoPartnerModalidadCurso> listado = base.GetBy(filter);
            List<PgeneralCodigoPartnerModalidadCursoBO> listadoBO = new List<PgeneralCodigoPartnerModalidadCursoBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralCodigoPartnerModalidadCursoBO objetoBO = Mapper.Map<TPgeneralCodigoPartnerModalidadCurso, PgeneralCodigoPartnerModalidadCursoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralCodigoPartnerModalidadCursoBO FirstById(int id)
        {
            try
            {
                TPgeneralCodigoPartnerModalidadCurso entidad = base.FirstById(id);
                PgeneralCodigoPartnerModalidadCursoBO objetoBO = new PgeneralCodigoPartnerModalidadCursoBO();
                Mapper.Map<TPgeneralCodigoPartnerModalidadCurso, PgeneralCodigoPartnerModalidadCursoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralCodigoPartnerModalidadCursoBO FirstBy(Expression<Func<TPgeneralCodigoPartnerModalidadCurso, bool>> filter)
        {
            try
            {
                TPgeneralCodigoPartnerModalidadCurso entidad = base.FirstBy(filter);
                PgeneralCodigoPartnerModalidadCursoBO objetoBO = Mapper.Map<TPgeneralCodigoPartnerModalidadCurso, PgeneralCodigoPartnerModalidadCursoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(PgeneralCodigoPartnerModalidadCursoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralCodigoPartnerModalidadCurso entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralCodigoPartnerModalidadCursoBO> listadoBO)
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

        public bool Update(PgeneralCodigoPartnerModalidadCursoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralCodigoPartnerModalidadCurso entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralCodigoPartnerModalidadCursoBO> listadoBO)
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
        private void AsignacionId(TPgeneralCodigoPartnerModalidadCurso entidad, PgeneralCodigoPartnerModalidadCursoBO objetoBO)
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

        private TPgeneralCodigoPartnerModalidadCurso MapeoEntidad(PgeneralCodigoPartnerModalidadCursoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralCodigoPartnerModalidadCurso entidad = new TPgeneralCodigoPartnerModalidadCurso();
                entidad = Mapper.Map<PgeneralCodigoPartnerModalidadCursoBO, TPgeneralCodigoPartnerModalidadCurso>(objetoBO,
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
