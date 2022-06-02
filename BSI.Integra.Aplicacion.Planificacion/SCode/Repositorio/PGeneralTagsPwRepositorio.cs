using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class PGeneralTagsPwRepositorio : BaseRepository<TPgeneralTagsPw, PGeneralTagPwBO>
    {
        #region Metodos Base
        public PGeneralTagsPwRepositorio() : base()
        {
        }
        public PGeneralTagsPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PGeneralTagPwBO> GetBy(Expression<Func<TPgeneralTagsPw, bool>> filter)
        {
            IEnumerable<TPgeneralTagsPw> listado = base.GetBy(filter);
            List<PGeneralTagPwBO> listadoBO = new List<PGeneralTagPwBO>();
            foreach (var itemEntidad in listado)
            {
                PGeneralTagPwBO objetoBO = Mapper.Map<TPgeneralTagsPw, PGeneralTagPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PGeneralTagPwBO FirstById(int id)
        {
            try
            {
                TPgeneralTagsPw entidad = base.FirstById(id);
                PGeneralTagPwBO objetoBO = new PGeneralTagPwBO();
                Mapper.Map<TPgeneralTagsPw, PGeneralTagPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PGeneralTagPwBO FirstBy(Expression<Func<TPgeneralTagsPw, bool>> filter)
        {
            try
            {
                TPgeneralTagsPw entidad = base.FirstBy(filter);
                PGeneralTagPwBO objetoBO = Mapper.Map<TPgeneralTagsPw, PGeneralTagPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PGeneralTagPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralTagsPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PGeneralTagPwBO> listadoBO)
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

        public bool Update(PGeneralTagPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralTagsPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PGeneralTagPwBO> listadoBO)
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
        private void AsignacionId(TPgeneralTagsPw entidad, PGeneralTagPwBO objetoBO)
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

        private TPgeneralTagsPw MapeoEntidad(PGeneralTagPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralTagsPw entidad = new TPgeneralTagsPw();
                entidad = Mapper.Map<PGeneralTagPwBO, TPgeneralTagsPw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<PGeneralTagPwBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPgeneralTagsPw, bool>>> filters, Expression<Func<TPgeneralTagsPw, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TPgeneralTagsPw> listado = base.GetFiltered(filters, orderBy, ascending);
            List<PGeneralTagPwBO> listadoBO = new List<PGeneralTagPwBO>();

            foreach (var itemEntidad in listado)
            {
                PGeneralTagPwBO objetoBO = Mapper.Map<TPgeneralTagsPw, PGeneralTagPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
