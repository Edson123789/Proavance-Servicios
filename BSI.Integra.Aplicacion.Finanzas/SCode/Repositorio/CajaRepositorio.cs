using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class CajaRepositorio : BaseRepository<TCaja, CajaBO>
    {
        #region Metodos Base
        public CajaRepositorio() : base()
        {
        }
        public CajaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CajaBO> GetBy(Expression<Func<TCaja, bool>> filter)
        {
            IEnumerable<TCaja> listado = base.GetBy(filter);
            List<CajaBO> listadoBO = new List<CajaBO>();
            foreach (var itemEntidad in listado)
            {
                CajaBO objetoBO = Mapper.Map<TCaja, CajaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CajaBO FirstById(int id)
        {
            try
            {
                TCaja entidad = base.FirstById(id);
                CajaBO objetoBO = new CajaBO();
                Mapper.Map<TCaja, CajaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CajaBO FirstBy(Expression<Func<TCaja, bool>> filter)
        {
            try
            {
                TCaja entidad = base.FirstBy(filter);
                CajaBO objetoBO = Mapper.Map<TCaja, CajaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CajaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCaja entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CajaBO> listadoBO)
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

        public bool Update(CajaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCaja entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CajaBO> listadoBO)
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
        private void AsignacionId(TCaja entidad, CajaBO objetoBO)
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

        private TCaja MapeoEntidad(CajaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCaja entidad = new TCaja();
                entidad = Mapper.Map<CajaBO, TCaja>(objetoBO,
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

        public List<CajaDTO> ObtenerCajasFinanzas()
        {
            try
            {
                List<CajaDTO> cajaFinanzas = new List<CajaDTO>();
                var _query = "SELECT Id,CodigoCaja,IdEmpresa,Empresa,IdBanco,Banco,IdCuenta,Cuenta,IdMoneda,Moneda,IdPais,Pais,IdCiudad,Ciudad,IdPersonal,Personal,Activo FROM FIN.V_ObtenerCajasFinanzas  order by Id desc";
                var cajaFinanzasDB = _dapper.QueryDapper(_query, null);
                if (!cajaFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(cajaFinanzasDB))
                {
                    cajaFinanzas = JsonConvert.DeserializeObject<List<CajaDTO>>(cajaFinanzasDB);
                }
                return cajaFinanzas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CajaResponsableDTO> ObtenerCajaResponsable()
        {
            try
            {
                List<CajaResponsableDTO> cajaResponsable = new List<CajaResponsableDTO>();
                var _query = "SELECT Id,CodigoCaja,Personal as PersonalResponsable,IdPersonal as IdPersonalResponsable,IdMoneda FROM FIN.V_ObtenerCajasFinanzas where activo=1 order by Id desc";
                var cajaFinanzasDB = _dapper.QueryDapper(_query, null);
                if (!cajaFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(cajaFinanzasDB))
                {
                    cajaResponsable = JsonConvert.DeserializeObject<List<CajaResponsableDTO>>(cajaFinanzasDB);
                }
                return cajaResponsable;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la Lista de Responsables de Caja
        /// </summary>
        /// <returns></returns>
        public List<ResponsableCajaDTO> ObtenerListaCajaResponsable()
        {
            try
            {
                List<ResponsableCajaDTO> cajaResponsable = new List<ResponsableCajaDTO>();
                var _query = "SELECT Id, Nombre FROM [fin].[V_ObtenerResponsablesCajaFinanzas]"; // falta confirmar si se debe verificar el 'Estado' y 'Activo'
                var cajaFinanzasDB = _dapper.QueryDapper(_query, null);
                if (!cajaFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(cajaFinanzasDB))
                {
                    cajaResponsable = JsonConvert.DeserializeObject<List<ResponsableCajaDTO>>(cajaFinanzasDB);
                }
                return cajaResponsable;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int obtenerIdCuentaCorriente(int IdCaja) {

            try
            {
                return this.GetBy(x => x.Estado == true && x.Id == IdCaja).Select(x => x.IdCuentaCorriente).FirstOrDefault();
            }
            catch (Exception e)
            {
                return  0;
            }
        }

        public List<ResumenCajaDTO> ObtenerResumenCaja()
        {
            try
            {
                List<ResumenCajaDTO> resumenCaja = new List<ResumenCajaDTO>();
                var _query = "SELECT IdCaja, CodigoCaja,IdEmpresaAutorizada,Direccion,Central,Ruc,IdEntidadFinanciera,IdCuentaCorriente,IdMoneda,IdCiudad,IdPais,PersonalResponsable FROM FIN.V_ObtenerResumenCaja where Activo=1";
                var cajaFinanzasDB = _dapper.QueryDapper(_query, null);
                if (!cajaFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(cajaFinanzasDB))
                {
                    resumenCaja = JsonConvert.DeserializeObject<List<ResumenCajaDTO>>(cajaFinanzasDB);
                }
                return resumenCaja;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
