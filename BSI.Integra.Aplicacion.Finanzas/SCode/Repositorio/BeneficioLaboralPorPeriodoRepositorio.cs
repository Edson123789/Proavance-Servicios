using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;


namespace BSI.Integra.Aplicacion.Finanzas.SCode.Repositorio
{
    public class BeneficioLaboralPorPeriodoRepositorio : BaseRepository<TBeneficioLaboralPorPeriodo, BeneficioLaboralPorPeriodoBO>
    {
        #region Metodos Base
        public BeneficioLaboralPorPeriodoRepositorio() : base()
        {
        }
        public BeneficioLaboralPorPeriodoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<BeneficioLaboralPorPeriodoBO> GetBy(Expression<Func<TBeneficioLaboralPorPeriodo, bool>> filter)
        {
            IEnumerable<TBeneficioLaboralPorPeriodo> listado = base.GetBy(filter);
            List<BeneficioLaboralPorPeriodoBO> listadoBO = new List<BeneficioLaboralPorPeriodoBO>();
            foreach (var itemEntidad in listado)
            {
                BeneficioLaboralPorPeriodoBO objetoBO = Mapper.Map<TBeneficioLaboralPorPeriodo, BeneficioLaboralPorPeriodoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public BeneficioLaboralPorPeriodoBO FirstById(int id)
        {
            try
            {
                TBeneficioLaboralPorPeriodo entidad = base.FirstById(id);
                BeneficioLaboralPorPeriodoBO objetoBO = new BeneficioLaboralPorPeriodoBO();
                Mapper.Map<TBeneficioLaboralPorPeriodo, BeneficioLaboralPorPeriodoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public BeneficioLaboralPorPeriodoBO FirstBy(Expression<Func<TBeneficioLaboralPorPeriodo, bool>> filter)
        {
            try
            {
                TBeneficioLaboralPorPeriodo entidad = base.FirstBy(filter);
                BeneficioLaboralPorPeriodoBO objetoBO = Mapper.Map<TBeneficioLaboralPorPeriodo, BeneficioLaboralPorPeriodoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(BeneficioLaboralPorPeriodoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TBeneficioLaboralPorPeriodo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<BeneficioLaboralPorPeriodoBO> listadoBO)
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

        public bool Update(BeneficioLaboralPorPeriodoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TBeneficioLaboralPorPeriodo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<BeneficioLaboralPorPeriodoBO> listadoBO)
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
        private void AsignacionId(TBeneficioLaboralPorPeriodo entidad, BeneficioLaboralPorPeriodoBO objetoBO)
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

        private TBeneficioLaboralPorPeriodo MapeoEntidad(BeneficioLaboralPorPeriodoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TBeneficioLaboralPorPeriodo entidad = new TBeneficioLaboralPorPeriodo();
                entidad = Mapper.Map<BeneficioLaboralPorPeriodoBO, TBeneficioLaboralPorPeriodo>(objetoBO,
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
        public List<BeneficioLaboralVentasDTO> ObtenerBeneficioLaboralVentasPorPeriodo(int IdPeriodo)
        {
            try
            {
                var _query = "";
                var camposTabla = "IdAgendaTipoUsuario,TipoPersona,Sueldo,Comisiones,SistemaPensionario,RentaQuintaCategoria,EsSalud,CTS,Gratificacion,ParticipacionesUtilidades,Publicidad ";

                List<BeneficioLaboralVentasDTO> listaBeneficioLaboralPeriodo = new List<BeneficioLaboralVentasDTO>();

                _query = "SELECT " + camposTabla + " FROM FIN.V_BeneficioLaboralVentas where IdPeriodo=@IdPeriodo";

                var BeneficioLaboralDB = _dapper.QueryDapper(_query, new { IdPeriodo});
                if (!BeneficioLaboralDB.Contains("[]") && !string.IsNullOrEmpty(BeneficioLaboralDB))
                {
                    listaBeneficioLaboralPeriodo = JsonConvert.DeserializeObject<List<BeneficioLaboralVentasDTO>>(BeneficioLaboralDB);
                }
                return listaBeneficioLaboralPeriodo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
