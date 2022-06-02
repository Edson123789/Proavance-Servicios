using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PgeneralAsubPgeneralVersionProgramaRepositorio : BaseRepository<TPgeneralAsubPgeneralVersionPrograma, PgeneralAsubPgeneralVersionProgramaBO>
    {
        #region Metodos Base
        public PgeneralAsubPgeneralVersionProgramaRepositorio() : base()
        {
        }
        public PgeneralAsubPgeneralVersionProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralAsubPgeneralVersionProgramaBO> GetBy(Expression<Func<TPgeneralAsubPgeneralVersionPrograma, bool>> filter)
        {
            IEnumerable<TPgeneralAsubPgeneralVersionPrograma> listado = base.GetBy(filter);
            List<PgeneralAsubPgeneralVersionProgramaBO> listadoBO = new List<PgeneralAsubPgeneralVersionProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralAsubPgeneralVersionProgramaBO objetoBO = Mapper.Map<TPgeneralAsubPgeneralVersionPrograma, PgeneralAsubPgeneralVersionProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IEnumerable<PgeneralAsubPgeneralVersionProgramaBO> GetBy(Expression<Func<TPgeneralAsubPgeneralVersionPrograma, bool>> filter, int skip, int take)
        {
            IEnumerable<TPgeneralAsubPgeneralVersionPrograma> listado = base.GetBy(filter).Skip(skip).Take(take);
            List<PgeneralAsubPgeneralVersionProgramaBO> listadoBO = new List<PgeneralAsubPgeneralVersionProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralAsubPgeneralVersionProgramaBO objetoBO = Mapper.Map<TPgeneralAsubPgeneralVersionPrograma, PgeneralAsubPgeneralVersionProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public PgeneralAsubPgeneralVersionProgramaBO FirstById(int id)
        {
            try
            {
                TPgeneralAsubPgeneralVersionPrograma entidad = base.FirstById(id);
                PgeneralAsubPgeneralVersionProgramaBO objetoBO = new PgeneralAsubPgeneralVersionProgramaBO();
                Mapper.Map<TPgeneralAsubPgeneralVersionPrograma, PgeneralAsubPgeneralVersionProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralAsubPgeneralVersionProgramaBO FirstBy(Expression<Func<TPgeneralAsubPgeneralVersionPrograma, bool>> filter)
        {
            try
            {
                TPgeneralAsubPgeneralVersionPrograma entidad = base.FirstBy(filter);
                PgeneralAsubPgeneralVersionProgramaBO objetoBO = Mapper.Map<TPgeneralAsubPgeneralVersionPrograma, PgeneralAsubPgeneralVersionProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralAsubPgeneralVersionProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralAsubPgeneralVersionPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralAsubPgeneralVersionProgramaBO> listadoBO)
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

        public bool Update(PgeneralAsubPgeneralVersionProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralAsubPgeneralVersionPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralAsubPgeneralVersionProgramaBO> listadoBO)
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
        private void AsignacionId(TPgeneralAsubPgeneralVersionPrograma entidad, PgeneralAsubPgeneralVersionProgramaBO objetoBO)
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

        private TPgeneralAsubPgeneralVersionPrograma MapeoEntidad(PgeneralAsubPgeneralVersionProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralAsubPgeneralVersionPrograma entidad = new TPgeneralAsubPgeneralVersionPrograma();
                entidad = Mapper.Map<PgeneralAsubPgeneralVersionProgramaBO, TPgeneralAsubPgeneralVersionPrograma>(objetoBO,
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
