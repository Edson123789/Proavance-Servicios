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
    public class CuentaCorrienteRepositorio : BaseRepository<TCuentaCorriente, CuentaCorrienteBO>
    {
        #region Metodos Base
        public CuentaCorrienteRepositorio() : base()
        {
        }
        public CuentaCorrienteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CuentaCorrienteBO> GetBy(Expression<Func<TCuentaCorriente, bool>> filter)
        {
            IEnumerable<TCuentaCorriente> listado = base.GetBy(filter);
            List<CuentaCorrienteBO> listadoBO = new List<CuentaCorrienteBO>();
            foreach (var itemEntidad in listado)
            {
                CuentaCorrienteBO objetoBO = Mapper.Map<TCuentaCorriente, CuentaCorrienteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CuentaCorrienteBO FirstById(int id)
        {
            try
            {
                TCuentaCorriente entidad = base.FirstById(id);
                CuentaCorrienteBO objetoBO = new CuentaCorrienteBO();
                Mapper.Map<TCuentaCorriente, CuentaCorrienteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CuentaCorrienteBO FirstBy(Expression<Func<TCuentaCorriente, bool>> filter)
        {
            try
            {
                TCuentaCorriente entidad = base.FirstBy(filter);
                CuentaCorrienteBO objetoBO = Mapper.Map<TCuentaCorriente, CuentaCorrienteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CuentaCorrienteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCuentaCorriente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CuentaCorrienteBO> listadoBO)
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

        public bool Update(CuentaCorrienteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCuentaCorriente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CuentaCorrienteBO> listadoBO)
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
        private void AsignacionId(TCuentaCorriente entidad, CuentaCorrienteBO objetoBO)
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

        private TCuentaCorriente MapeoEntidad(CuentaCorrienteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCuentaCorriente entidad = new TCuentaCorriente();
                entidad = Mapper.Map<CuentaCorrienteBO, TCuentaCorriente>(objetoBO,
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

        public List<CuentaCorrienteDTO> ObtenerCuentasCorrientes() {
            try
            {
                List<CuentaCorrienteDTO> cuentaCorriente = new List<CuentaCorrienteDTO>();
                var _query = "SELECT IdCta, NumeroCuenta, IdCiudad,Ciudad, NombreEntidadFinanciera FROM FIN.V_ObtenerCuentasCorrientes where EstadoCiudad = 1 and EstadoCuentaCorriente = 1 and EstadoEntidadFinanciera = 1";
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

        public List<CuentaBancariaDTO> ObtenerCuentasBancarias()
        {
            try
            {
                List<CuentaBancariaDTO> cuentaBancaria = new List<CuentaBancariaDTO>();
                var _query = "SELECT Id,NumeroCuenta,Moneda,IdMoneda,Ciudad,IdCiudad,NombreBanco,IdBanco,UsuarioModificacion,FechaModificacion,EstadoCuenta FROM FIN.V_ObtenerCuentasBancarias where EstadoMoneda = 1 and EstadoCiudad = 1 and EstadoEntidadFinanciera = 1 and EstadoCuenta = 1 order by NombreBanco";
                var cuentaBancariaDB = _dapper.QueryDapper(_query, null);
                if (!cuentaBancariaDB.Contains("[]") && !string.IsNullOrEmpty(cuentaBancariaDB))
                {
                    cuentaBancaria = JsonConvert.DeserializeObject<List<CuentaBancariaDTO>>(cuentaBancariaDB);
                }
                return cuentaBancaria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene la lista de Cuentas Corrientes Asociadas a Su EntidadFinanciera (usado para combobox)
        /// </summary>
        /// <returns></returns>
        public List<CuentaCorrienteEntidadCiudadDTO> ObtenerCuentaCorrienteConEntidad()
        {
            try
            {
                List<CuentaCorrienteEntidadCiudadDTO> lista = new List<CuentaCorrienteEntidadCiudadDTO>();
                string _query = string.Empty;
                _query = "SELECT * FROM fin.V_ObtenerCuentaEntidadCiudad WHERE Estado=1";
                var listaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(listaDB) && !listaDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<CuentaCorrienteEntidadCiudadDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FiltroDTO> ObtenerCuentaCorrienteIdNombre()
        {
            try
            {
                List<FiltroDTO> listaCuentaCorriente = this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.NumeroCuenta.ToUpper() }).ToList();
                return listaCuentaCorriente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string ObtenerCuentaCorrienteById(int Id) {
            try
            {
                return this.GetBy(x => x.Estado == true && x.Id == Id).Select(x => x.NumeroCuenta).FirstOrDefault();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
