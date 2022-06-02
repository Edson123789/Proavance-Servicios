using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class BeneficioDatoAdicionalRepositorio : BaseRepository<TBeneficioDatoAdicional, BeneficioDatoAdicionalBO>
    {
        #region Metodos Base
        public BeneficioDatoAdicionalRepositorio() : base()
        {
        }
        public BeneficioDatoAdicionalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<BeneficioDatoAdicionalBO> GetBy(Expression<Func<TBeneficioDatoAdicional, bool>> filter)
        {
            IEnumerable<TBeneficioDatoAdicional> listado = base.GetBy(filter);
            List<BeneficioDatoAdicionalBO> listadoBO = new List<BeneficioDatoAdicionalBO>();
            foreach (var itemEntidad in listado)
            {
                BeneficioDatoAdicionalBO objetoBO = Mapper.Map<TBeneficioDatoAdicional, BeneficioDatoAdicionalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public BeneficioDatoAdicionalBO FirstById(int id)
        {
            try
            {
                TBeneficioDatoAdicional entidad = base.FirstById(id);
                BeneficioDatoAdicionalBO objetoBO = new BeneficioDatoAdicionalBO();
                Mapper.Map<TBeneficioDatoAdicional, BeneficioDatoAdicionalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public BeneficioDatoAdicionalBO FirstBy(Expression<Func<TBeneficioDatoAdicional, bool>> filter)
        {
            try
            {
                TBeneficioDatoAdicional entidad = base.FirstBy(filter);
                BeneficioDatoAdicionalBO objetoBO = Mapper.Map<TBeneficioDatoAdicional, BeneficioDatoAdicionalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(BeneficioDatoAdicionalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TBeneficioDatoAdicional entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<BeneficioDatoAdicionalBO> listadoBO)
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

        public bool Update(BeneficioDatoAdicionalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TBeneficioDatoAdicional entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<BeneficioDatoAdicionalBO> listadoBO)
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
        private void AsignacionId(TBeneficioDatoAdicional entidad, BeneficioDatoAdicionalBO objetoBO)
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

        private TBeneficioDatoAdicional MapeoEntidad(BeneficioDatoAdicionalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TBeneficioDatoAdicional entidad = new TBeneficioDatoAdicional();
                entidad = Mapper.Map<BeneficioDatoAdicionalBO, TBeneficioDatoAdicional>(objetoBO,
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

        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
