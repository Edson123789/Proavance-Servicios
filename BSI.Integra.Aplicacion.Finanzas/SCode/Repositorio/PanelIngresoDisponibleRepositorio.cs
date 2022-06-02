using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class PanelIngresoDisponibleRepositorio : BaseRepository<TPanelIngresoDisponible, PanelIngresoDisponibleBO>
    {
        #region Metodos Base
        public PanelIngresoDisponibleRepositorio() : base()
        {
        }
        public PanelIngresoDisponibleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PanelIngresoDisponibleBO> GetBy(Expression<Func<TPanelIngresoDisponible, bool>> filter)
        {
            IEnumerable<TPanelIngresoDisponible> listado = base.GetBy(filter);
            List<PanelIngresoDisponibleBO> listadoBO = new List<PanelIngresoDisponibleBO>();
            foreach (var itemEntidad in listado)
            {
                PanelIngresoDisponibleBO objetoBO = Mapper.Map<TPanelIngresoDisponible, PanelIngresoDisponibleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PanelIngresoDisponibleBO FirstById(int id)
        {
            try
            {
                TPanelIngresoDisponible entidad = base.FirstById(id);
                PanelIngresoDisponibleBO objetoBO = new PanelIngresoDisponibleBO();
                Mapper.Map<TPanelIngresoDisponible, PanelIngresoDisponibleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PanelIngresoDisponibleBO FirstBy(Expression<Func<TPanelIngresoDisponible, bool>> filter)
        {
            try
            {
                TPanelIngresoDisponible entidad = base.FirstBy(filter);
                PanelIngresoDisponibleBO objetoBO = Mapper.Map<TPanelIngresoDisponible, PanelIngresoDisponibleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PanelIngresoDisponibleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPanelIngresoDisponible entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PanelIngresoDisponibleBO> listadoBO)
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

        public bool Update(PanelIngresoDisponibleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPanelIngresoDisponible entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PanelIngresoDisponibleBO> listadoBO)
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
        private void AsignacionId(TPanelIngresoDisponible entidad, PanelIngresoDisponibleBO objetoBO)
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

        private TPanelIngresoDisponible MapeoEntidad(PanelIngresoDisponibleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPanelIngresoDisponible entidad = new TPanelIngresoDisponible();
                entidad = Mapper.Map<PanelIngresoDisponibleBO, TPanelIngresoDisponible>(objetoBO,
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
        public List<PanelDepositoDisponibleDTO> ObtenerPanelDepositoDisponible()
        {
            try
            {
                List<PanelDepositoDisponibleDTO> panelDepositoDisponible = new List<PanelDepositoDisponibleDTO>();
                var _query = "SELECT Id,IdFormaPago,FormaPago,DiasDeposito,DiasDisponible,CuentaFeriados,CuentaFeriadosEstatales,ConsideraVSD,ConsideraDiasHabilesLunesViernes,ConsideraDiasHabilesLunesSabado,ConsideraDiasFijoSemana,IdDiaSemanaFijo,HoraCorte,MinutoCorte,PorcentajeCobro,UsuarioModificacion FROM FIN.V_ObtenerPanelIngresoDisponible  order by Id desc";
                var panelDepositoDB = _dapper.QueryDapper(_query, null);
                if (!panelDepositoDB.Contains("[]") && !string.IsNullOrEmpty(panelDepositoDB))
                {
                    panelDepositoDisponible = JsonConvert.DeserializeObject<List<PanelDepositoDisponibleDTO>>(panelDepositoDB);
                }
                return panelDepositoDisponible;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
