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

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class CriterioCalificacionRepositorio : BaseRepository<TCriterioCalificacion, CriterioCalificacionBO>
    {
        #region Metodos Base
        public CriterioCalificacionRepositorio() : base()
        {
        }
        public CriterioCalificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CriterioCalificacionBO> GetBy(Expression<Func<TCriterioCalificacion, bool>> filter)
        {
            IEnumerable<TCriterioCalificacion> listado = base.GetBy(filter);
            List<CriterioCalificacionBO> listadoBO = new List<CriterioCalificacionBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioCalificacionBO objetoBO = Mapper.Map<TCriterioCalificacion, CriterioCalificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CriterioCalificacionBO FirstById(int id)
        {
            try
            {
                TCriterioCalificacion entidad = base.FirstById(id);
                CriterioCalificacionBO objetoBO = new CriterioCalificacionBO();
                Mapper.Map<TCriterioCalificacion, CriterioCalificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CriterioCalificacionBO FirstBy(Expression<Func<TCriterioCalificacion, bool>> filter)
        {
            try
            {
                TCriterioCalificacion entidad = base.FirstBy(filter);
                CriterioCalificacionBO objetoBO = Mapper.Map<TCriterioCalificacion, CriterioCalificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CriterioCalificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCriterioCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CriterioCalificacionBO> listadoBO)
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

        public bool Update(CriterioCalificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCriterioCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CriterioCalificacionBO> listadoBO)
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
        private void AsignacionId(TCriterioCalificacion entidad, CriterioCalificacionBO objetoBO)
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

        private TCriterioCalificacion MapeoEntidad(CriterioCalificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCriterioCalificacion entidad = new TCriterioCalificacion();
                entidad = Mapper.Map<CriterioCalificacionBO, TCriterioCalificacion>(objetoBO,
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

        public List<CuentaCorrienteDTO> ObtenerCuentasCorrientes()
        {
            try
            {
                List<CuentaCorrienteDTO> cuentaCorriente = new List<CuentaCorrienteDTO>();
                var _query = "SELECT IdCta, NumeroCuenta, IdCiudad, NombreEntidadFinanciera FROM FIN.V_ObtenerCuentasCorrientes where EstadoCiudad = 1 and EstadoCuentaCorriente = 1 and EstadoEntidadFinanciera = 1";
                var cuentaCorrienteDB = _dapper.QueryDapper(_query,null);
                if (!cuentaCorrienteDB.Contains("[]") && !string.IsNullOrEmpty(cuentaCorrienteDB))
                {
                    cuentaCorriente = JsonConvert.DeserializeObject<List<CuentaCorrienteDTO>>(cuentaCorrienteDB);
                }
                return cuentaCorriente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
