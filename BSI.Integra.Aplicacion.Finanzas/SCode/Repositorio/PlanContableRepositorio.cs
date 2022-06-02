using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class PlanContableRepositorio : BaseRepository<TPlanContable, PlanContableBO>
    {
        #region Metodos Base
        public PlanContableRepositorio() : base()
        {
        }
        public PlanContableRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlanContableBO> GetBy(Expression<Func<TPlanContable, bool>> filter)
        {
            IEnumerable<TPlanContable> listado = base.GetBy(filter);
            List<PlanContableBO> listadoBO = new List<PlanContableBO>();
            foreach (var itemEntidad in listado)
            {
                PlanContableBO objetoBO = Mapper.Map<TPlanContable, PlanContableBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlanContableBO FirstById(int id)
        {
            try
            {
                TPlanContable entidad = base.FirstById(id);
                PlanContableBO objetoBO = new PlanContableBO();
                Mapper.Map<TPlanContable, PlanContableBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PlanContableBO FirstBy(Expression<Func<TPlanContable, bool>> filter)
        {
            try
            {
                TPlanContable entidad = base.FirstBy(filter);
                PlanContableBO objetoBO = Mapper.Map<TPlanContable, PlanContableBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlanContableBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlanContable entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlanContableBO> listadoBO)
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

        public bool Update(PlanContableBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlanContable entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlanContableBO> listadoBO)
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
        private void AsignacionId(TPlanContable entidad, PlanContableBO objetoBO)
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

        private TPlanContable MapeoEntidad(PlanContableBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPlanContable entidad = new TPlanContable();
                entidad = Mapper.Map<PlanContableBO, TPlanContable>(objetoBO,
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

        public List<PlanContableDTO> ObtenerPlanContable()
        {
            try
            {
                List<PlanContableDTO> planContable = new List<PlanContableDTO>();
                var _query = "SELECT Id,Cuenta,Descripcion,Padre,Univel,Cbal,Debe,Haber,IdTipoCuenta,TipoCuenta,Analisis,CentroCosto,Estado,UsuarioModificacion,FechaModificacion FROM FIN.V_ObtenerPlanContable where Estado = 1 and cuenta like '__' order by Cuenta";
                var planContableDB = _dapper.QueryDapper(_query, null);
                if (!planContableDB.Contains("[]") && !string.IsNullOrEmpty(planContableDB))
                {
                    planContable = JsonConvert.DeserializeObject<List<PlanContableDTO>>(planContableDB);
                }
                return planContable;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las cuentas hijas de la clase Actual que se digita
        /// </summary>
        /// <param name="Cuenta"></param>
        /// <returns></returns>
        public List<PlanContableDTO> ObteneCuentasHijo(long cuenta)
        {
            try
            {
                List<PlanContableDTO> planContableCuentasHijo = new List<PlanContableDTO>();
                var _query = "SELECT Id,Cuenta,Descripcion,Padre,Univel,Cbal,Debe,Haber,IdTipoCuenta,TipoCuenta,Analisis,CentroCosto,Estado,UsuarioModificacion,FechaModificacion FROM FIN.V_ObtenerPlanContable where Estado = 1 and cuenta like CONCAT(@cuenta,'_') order by Cuenta";
                var planContableBD = _dapper.QueryDapper(_query, new { cuenta });
                if (!planContableBD.Contains("[]") && !string.IsNullOrEmpty(planContableBD))
                {
                    planContableCuentasHijo = JsonConvert.DeserializeObject<List<PlanContableDTO>>(planContableBD);
                }
                if (planContableCuentasHijo.Count() == 0 || cuenta.ToString().Length == 4)
                {
                    _query = "SELECT Id,Cuenta,Descripcion,Padre,Univel,Cbal,Debe,Haber,IdTipoCuenta,TipoCuenta,Analisis,CentroCosto,Estado,UsuarioModificacion,FechaModificacion FROM FIN.V_ObtenerPlanContable where Estado = 1 and cuenta like CONCAT(@cuenta,'_%') order by Cuenta";
                    planContableBD = _dapper.QueryDapper(_query, new { cuenta });
                    if (!planContableBD.Contains("[]") && !string.IsNullOrEmpty(planContableBD))
                    {
                        planContableCuentasHijo = JsonConvert.DeserializeObject<List<PlanContableDTO>>(planContableBD);
                    }
                }

                return planContableCuentasHijo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de PlanContable dado un Indicio de Nombre, usado para AutoComplete [Cuenta->Id, Descripcion->Nombre]
        /// </summary>
        /// <param name="NombreParcial"></param>
        /// <returns></returns>
        public List<PlanContableFiltroDTO> ObtenerPlanContableAutoComplete(string NombreParcial)
        {
            try
            {
                List<PlanContableFiltroDTO> lista = new List<PlanContableFiltroDTO>();
                string _query = string.Empty;
                _query = "SELECT CONVERT(VARCHAR(50), Cuenta) Id, CONVERT(VARCHAR(50), Cuenta)+'_'+Descripcion Nombre FROM fin.T_PlanContable WHERE Cuenta like '%" + NombreParcial + "%' or CONVERT(VARCHAR(50), Cuenta)+'_'+Descripcion like '%" + NombreParcial + "%' AND Estado=1";
                var listaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(listaDB) && !listaDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PlanContableFiltroDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene La lista de PlanContable para AutoComplete [Id->Id (Cuenta+Descripcion)->Nombre]
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerPlanContableFiltro(string NombreParcial)
        {
            try
            {
                var query = "SELECT Id, Nombre FROM fin.V_PlanContableEstadoCuentaPagadoPendiente where Nombre like '%"+NombreParcial+"%'";
                var planContable = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(planContable);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de Plan Contable con su rubro asociado (para llenado de grilla en modulo AsociarPlanContableRubro)
        /// </summary>
        /// <returns></returns>
        public List<PlanContableConRubroDTO> ObtenerPlanContableConRubro()
        {
            try
            {
                var query = "SELECT Id, Cuenta, Descripcion, IdFurTipoSolicitud, NombreFurTipoSolicitud FROM [fin].[V_PlanContableConRubro] where Estado = 1";
                var planContable = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<PlanContableConRubroDTO>>(planContable);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un registro de Plan Contable con su rubro asociado (para llenado de grilla en modulo AsociarPlanContableRubro)
        /// </summary>
        /// <returns></returns>
        public List<PlanContableConRubroDTO> ObtenerPlanContableConRubro(int IdPlanContable)
        {
            try
            {
                var query = "SELECT Id, Cuenta, Descripcion, IdFurTipoSolicitud, NombreFurTipoSolicitud FROM [fin].[V_PlanContableConRubro] where Estado = 1 and Id=" + IdPlanContable;
                var planContable = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<PlanContableConRubroDTO>>(planContable);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
