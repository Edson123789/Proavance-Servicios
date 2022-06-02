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
    public class CajaEgresoRepositorio : BaseRepository<TCajaEgreso, CajaEgresoBO>
    {
        #region Metodos Base
        public CajaEgresoRepositorio() : base()
        {
        }
        public CajaEgresoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CajaEgresoBO> GetBy(Expression<Func<TCajaEgreso, bool>> filter)
        {
            IEnumerable<TCajaEgreso> listado = base.GetBy(filter);
            List<CajaEgresoBO> listadoBO = new List<CajaEgresoBO>();
            foreach (var itemEntidad in listado)
            {
                CajaEgresoBO objetoBO = Mapper.Map<TCajaEgreso, CajaEgresoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CajaEgresoBO FirstById(int id)
        {
            try
            {
                TCajaEgreso entidad = base.FirstById(id);
                CajaEgresoBO objetoBO = new CajaEgresoBO();
                Mapper.Map<TCajaEgreso, CajaEgresoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CajaEgresoBO FirstBy(Expression<Func<TCajaEgreso, bool>> filter)
        {
            try
            {
                TCajaEgreso entidad = base.FirstBy(filter);
                CajaEgresoBO objetoBO = Mapper.Map<TCajaEgreso, CajaEgresoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CajaEgresoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCajaEgreso entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CajaEgresoBO> listadoBO)
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

        public bool Update(CajaEgresoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCajaEgreso entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CajaEgresoBO> listadoBO)
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
        private void AsignacionId(TCajaEgreso entidad, CajaEgresoBO objetoBO)
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

        private TCajaEgreso MapeoEntidad(CajaEgresoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCajaEgreso entidad = new TCajaEgreso();
                entidad = Mapper.Map<CajaEgresoBO, TCajaEgreso>(objetoBO,
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
        /// Obtiene  registros con Estado=1  dado un IdCajaPorRendirCabecera de fin.T_CajaEgreso (para llenado de grilla en RendicionRequerimiento)
        /// </summary>
        /// <returns></returns>
        public List<CajaEgresoDTO> ObtenerRegistrosCajaEgreso(int IdCajaPorRendirCabecera)
        {
            try
            {
                List<CajaEgresoDTO> CajaPorRendirFinanzas = new List<CajaEgresoDTO>();
                var _query = "SELECT Id,IdFur,CodigoFur,Descripcion,IdMoneda,NombreMoneda,IdProveedor,NombreProveedor,RucProveedor,IdSunatDocumento,NombreSunatDocumento,Serie,Numero,FechaEmision,TotalEfectivo FROM  [fin].[V_ObtenerRegistrosCajaEgreso] where Estado=1 AND EsEnviado=0 And IdCajaPorRendirCabecera=" + IdCajaPorRendirCabecera;
                //var _query = "SELECT Id,IdFur,CodigoFur,Descripcion,IdMoneda,NombreMoneda,IdProveedor,NombreProveedor,RucProveedor,IdSunatDocumento,NombreSunatDocumento,Serie,Numero,FechaEmision,MontoBruto,TotalEfectivo FROM  [fin].[V_ObtenerRegistrosCajaEgreso] where Estado=1 AND EsEnviado=0 And IdCajaPorRendirCabecera=" + IdCajaPorRendirCabecera;
                var CajaPorRendirFinanzasDB = _dapper.QueryDapper(_query, null);
                if (!CajaPorRendirFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(CajaPorRendirFinanzasDB))
                {
                    CajaPorRendirFinanzas = JsonConvert.DeserializeObject<List<CajaEgresoDTO>>(CajaPorRendirFinanzasDB);
                }
                return CajaPorRendirFinanzas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene un (1)  registro con Estado=1  dado un Id de fin.T_CajaEgreso (para llenado de grilla en RendicionRequerimiento)
        /// </summary>
        /// <returns></returns>
        public List<CajaEgresoDTO> ObtenerRegistroCajaEgreso(int Id)
        {
            try
            {
                List<CajaEgresoDTO> CajaEgreso = new List<CajaEgresoDTO>();
                var _query = "SELECT Id,IdFur,CodigoFur,Descripcion,IdMoneda,NombreMoneda,IdProveedor,NombreProveedor,RucProveedor,IdSunatDocumento,NombreSunatDocumento,Serie,Numero,FechaEmision,TotalEfectivo FROM  [fin].[V_ObtenerRegistrosCajaEgreso] where Estado=1 AND EsEnviado=0 And Id=" + Id;
                var CajaEgresoDB = _dapper.QueryDapper(_query, null);
                if (!CajaEgresoDB.Contains("[]") && !string.IsNullOrEmpty(CajaEgresoDB))
                {
                    CajaEgreso = JsonConvert.DeserializeObject<List<CajaEgresoDTO>>(CajaEgresoDB);
                }
                return CajaEgreso;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<RegistroEgresoCajaDTO> ObtenerCajaEgresoEnviado(int idPersonalResponsable, int? idCaja, int? idSolicitante)
        {
            try
            {
                var _query = "";
                var camposTabla = "Id,IdComprobantePago,IdProveedor,NombreProveedor,RucProveedor,IdSunatDocumento,NombreSunatDocumento,Serie,Numero,IdFur,CodigoFur,Descripcion,IdMoneda,TotalEfectivo,FechaEmision,MontoFur,MontoPendiente,EsCancelado,IdPersonalSolicitante,PersonalSolicitante";

                List<RegistroEgresoCajaDTO> listaRegistroEgreso = new List<RegistroEgresoCajaDTO>();
                if (idCaja == null && idSolicitante==null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerCajaEgreso where EsEnviado=1 and IdCajaEgresoAprobado is null and IdPersonalResponsable=@idPersonalResponsable";

                }
                else
                {
                    if (idSolicitante == null)
                    {
                        _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerCajaEgreso where EsEnviado=1 and IdCajaEgresoAprobado is null and IdCaja=@idCaja";
                    }
                    else {
                        _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerCajaEgreso where EsEnviado=1 and IdCajaEgresoAprobado is null and IdCaja=@idCaja and IdPersonalSolicitante=@idSolicitante";
                    }
                    
                }
                var cajaEgresoDB = _dapper.QueryDapper(_query, new { idPersonalResponsable, idCaja, idSolicitante });
                if (!cajaEgresoDB.Contains("[]") && !string.IsNullOrEmpty(cajaEgresoDB))
                {
                    listaRegistroEgreso = JsonConvert.DeserializeObject<List<RegistroEgresoCajaDTO>>(cajaEgresoDB);
                }
                return listaRegistroEgreso;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }        
    } 
}
