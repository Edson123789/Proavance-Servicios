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
    public class PgeneralCodigoPartnerVersionProgramaRepositorio : BaseRepository<TPgeneralCodigoPartnerVersionPrograma, PgeneralCodigoPartnerVersionProgramaBO>
    {
        #region Metodos Base
        public PgeneralCodigoPartnerVersionProgramaRepositorio() : base()
        {
        }
        public PgeneralCodigoPartnerVersionProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralCodigoPartnerVersionProgramaBO> GetBy(Expression<Func<TPgeneralCodigoPartnerVersionPrograma, bool>> filter)
        {
            IEnumerable<TPgeneralCodigoPartnerVersionPrograma> listado = base.GetBy(filter);
            List<PgeneralCodigoPartnerVersionProgramaBO> listadoBO = new List<PgeneralCodigoPartnerVersionProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralCodigoPartnerVersionProgramaBO objetoBO = Mapper.Map<TPgeneralCodigoPartnerVersionPrograma, PgeneralCodigoPartnerVersionProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralCodigoPartnerVersionProgramaBO FirstById(int id)
        {
            try
            {
                TPgeneralCodigoPartnerVersionPrograma entidad = base.FirstById(id);
                PgeneralCodigoPartnerVersionProgramaBO objetoBO = new PgeneralCodigoPartnerVersionProgramaBO();
                Mapper.Map<TPgeneralCodigoPartnerVersionPrograma, PgeneralCodigoPartnerVersionProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralCodigoPartnerVersionProgramaBO FirstBy(Expression<Func<TPgeneralCodigoPartnerVersionPrograma, bool>> filter)
        {
            try
            {
                TPgeneralCodigoPartnerVersionPrograma entidad = base.FirstBy(filter);
                PgeneralCodigoPartnerVersionProgramaBO objetoBO = Mapper.Map<TPgeneralCodigoPartnerVersionPrograma, PgeneralCodigoPartnerVersionProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(PgeneralCodigoPartnerVersionProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralCodigoPartnerVersionPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralCodigoPartnerVersionProgramaBO> listadoBO)
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

        public bool Update(PgeneralCodigoPartnerVersionProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralCodigoPartnerVersionPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralCodigoPartnerVersionProgramaBO> listadoBO)
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
        private void AsignacionId(TPgeneralCodigoPartnerVersionPrograma entidad, PgeneralCodigoPartnerVersionProgramaBO objetoBO)
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

        private TPgeneralCodigoPartnerVersionPrograma MapeoEntidad(PgeneralCodigoPartnerVersionProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralCodigoPartnerVersionPrograma entidad = new TPgeneralCodigoPartnerVersionPrograma();
                entidad = Mapper.Map<PgeneralCodigoPartnerVersionProgramaBO, TPgeneralCodigoPartnerVersionPrograma>(objetoBO,
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
