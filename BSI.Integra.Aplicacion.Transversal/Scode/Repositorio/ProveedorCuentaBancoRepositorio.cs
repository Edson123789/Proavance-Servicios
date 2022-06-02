using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{

    public class ProveedorCuentaBancoRepositorio : BaseRepository<TProveedorCuentaBanco, ProveedorCuentaBancoBO>
    {
        #region Metodos Base
        public ProveedorCuentaBancoRepositorio() : base()
        {
        }
        public ProveedorCuentaBancoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProveedorCuentaBancoBO> GetBy(Expression<Func<TProveedorCuentaBanco, bool>> filter)
        {
            IEnumerable<TProveedorCuentaBanco> listado = base.GetBy(filter);
            List<ProveedorCuentaBancoBO> listadoBO = new List<ProveedorCuentaBancoBO>();
            foreach (var itemEntidad in listado)
            {
                ProveedorCuentaBancoBO objetoBO = Mapper.Map<TProveedorCuentaBanco, ProveedorCuentaBancoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProveedorCuentaBancoBO FirstById(int id)
        {
            try
            {
                TProveedorCuentaBanco entidad = base.FirstById(id); 
                ProveedorCuentaBancoBO objetoBO = new ProveedorCuentaBancoBO();
                Mapper.Map<TProveedorCuentaBanco, ProveedorCuentaBancoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProveedorCuentaBancoBO FirstBy(Expression<Func<TProveedorCuentaBanco, bool>> filter)
        {
            try
            {
                TProveedorCuentaBanco entidad = base.FirstBy(filter);
                ProveedorCuentaBancoBO objetoBO = Mapper.Map<TProveedorCuentaBanco, ProveedorCuentaBancoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProveedorCuentaBancoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProveedorCuentaBanco entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProveedorCuentaBancoBO> listadoBO)
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

        public bool Update(ProveedorCuentaBancoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProveedorCuentaBanco entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProveedorCuentaBancoBO> listadoBO)
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
        private void AsignacionId(TProveedorCuentaBanco entidad, ProveedorCuentaBancoBO objetoBO)
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

        private TProveedorCuentaBanco MapeoEntidad(ProveedorCuentaBancoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProveedorCuentaBanco entidad = new TProveedorCuentaBanco();
                entidad = Mapper.Map<ProveedorCuentaBancoBO, TProveedorCuentaBanco>(objetoBO,
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

        /// <summary>
        /// Obtiene datos para llenar grilla
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<ProveedorCuentaBancoDTO> ObtenerCuentasProveedorById(int IdProveedor)
        {
            try
            {
                var camposTabla = "SELECT Id,IdProveedor,IdTipoCuentaBanco,TipoCuenta,IdEntidadFinanciera,NombreBanco,IdMoneda,Moneda,NroCuenta,CuentaInterbancaria ";
                List<ProveedorCuentaBancoDTO> ProveedorCuenta = new List<ProveedorCuentaBancoDTO>();
                var _query = camposTabla + "FROM  [fin].[V_ObtenerProveedorCuentaBanco] where Estado=1 and IdProveedor="+ IdProveedor;
                var ProveedorDB = _dapper.QueryDapper(_query, null);
                if (!ProveedorDB.Contains("[]") && !string.IsNullOrEmpty(ProveedorDB))
                {
                    ProveedorCuenta = JsonConvert.DeserializeObject<List<ProveedorCuentaBancoDTO>>(ProveedorDB);
                }
                return ProveedorCuenta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
