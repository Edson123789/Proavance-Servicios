using AutoMapper;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.SCode.Repositorio
{
    public class BeneficioLaboralTipoRepositorio : BaseRepository<TBeneficioLaboralTipo, BeneficioLaboralTipoBO>
    {
        #region Metodos Base
        public BeneficioLaboralTipoRepositorio() : base()
        {
        }
        public BeneficioLaboralTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<BeneficioLaboralTipoBO> GetBy(Expression<Func<TBeneficioLaboralTipo, bool>> filter)
        {
            IEnumerable<TBeneficioLaboralTipo> listado = base.GetBy(filter);
            List<BeneficioLaboralTipoBO> listadoBO = new List<BeneficioLaboralTipoBO>();
            foreach (var itemEntidad in listado)
            {
                BeneficioLaboralTipoBO objetoBO = Mapper.Map<TBeneficioLaboralTipo, BeneficioLaboralTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public BeneficioLaboralTipoBO FirstById(int id)
        {
            try
            {
                TBeneficioLaboralTipo entidad = base.FirstById(id);
                BeneficioLaboralTipoBO objetoBO = new BeneficioLaboralTipoBO();
                Mapper.Map<TBeneficioLaboralTipo, BeneficioLaboralTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public BeneficioLaboralTipoBO FirstBy(Expression<Func<TBeneficioLaboralTipo, bool>> filter)
        {
            try
            {
                TBeneficioLaboralTipo entidad = base.FirstBy(filter);
                BeneficioLaboralTipoBO objetoBO = Mapper.Map<TBeneficioLaboralTipo, BeneficioLaboralTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(BeneficioLaboralTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TBeneficioLaboralTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<BeneficioLaboralTipoBO> listadoBO)
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

        public bool Update(BeneficioLaboralTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TBeneficioLaboralTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<BeneficioLaboralTipoBO> listadoBO)
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
        private void AsignacionId(TBeneficioLaboralTipo entidad, BeneficioLaboralTipoBO objetoBO)
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

        private TBeneficioLaboralTipo MapeoEntidad(BeneficioLaboralTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TBeneficioLaboralTipo entidad = new TBeneficioLaboralTipo();
                entidad = Mapper.Map<BeneficioLaboralTipoBO, TBeneficioLaboralTipo>(objetoBO,
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
