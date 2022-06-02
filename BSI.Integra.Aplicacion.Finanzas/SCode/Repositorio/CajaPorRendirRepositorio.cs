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
    public class CajaPorRendirRepositorio : BaseRepository<TCajaPorRendir, CajaPorRendirBO>
    {
        #region Metodos Base
        public CajaPorRendirRepositorio() : base()
        {
        }
        public CajaPorRendirRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CajaPorRendirBO> GetBy(Expression<Func<TCajaPorRendir, bool>> filter)
        {
            IEnumerable<TCajaPorRendir> listado = base.GetBy(filter);
            List<CajaPorRendirBO> listadoBO = new List<CajaPorRendirBO>();
            foreach (var itemEntidad in listado)
            {
                CajaPorRendirBO objetoBO = Mapper.Map<TCajaPorRendir, CajaPorRendirBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CajaPorRendirBO FirstById(int id)
        {
            try
            {
                TCajaPorRendir entidad = base.FirstById(id);
                CajaPorRendirBO objetoBO = new CajaPorRendirBO();
                Mapper.Map<TCajaPorRendir, CajaPorRendirBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CajaPorRendirBO FirstBy(Expression<Func<TCajaPorRendir, bool>> filter)
        {
            try
            {
                TCajaPorRendir entidad = base.FirstBy(filter);
                CajaPorRendirBO objetoBO = Mapper.Map<TCajaPorRendir, CajaPorRendirBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CajaPorRendirBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCajaPorRendir entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CajaPorRendirBO> listadoBO)
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

        public bool Update(CajaPorRendirBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCajaPorRendir entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CajaPorRendirBO> listadoBO)
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
        private void AsignacionId(TCajaPorRendir entidad, CajaPorRendirBO objetoBO)
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

        private TCajaPorRendir MapeoEntidad(CajaPorRendirBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCajaPorRendir entidad = new TCajaPorRendir();
                entidad = Mapper.Map<CajaPorRendirBO, TCajaPorRendir>(objetoBO,
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

        public List<CajaPorRendirDTO> ObtenerCajaPorRendirEnviada(int idPersonalResponsable,int? idMonedaCaja, int? idPersonalSolicitante)
        {
            try
            {
                var _query = "";
                var camposTabla = "Id,IdFur,CodigoFur,IdPersonalSolicitante,NombrePersonalSolicitante,Descripcion,IdMoneda,NombreMoneda,TotalEfectivo,FechaEntregaEfectivo";

                List<CajaPorRendirDTO> listaPorRendir = new List<CajaPorRendirDTO>();
                if (idMonedaCaja == null && idPersonalSolicitante == null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerCajasPorRendirFinanzas where IdCaja is null and EsEnviado=1 and IdCajaPorRendirCabecera is null and IdPersonalResponsable=@idPersonalResponsable";
                }
                else if (idMonedaCaja == null && idPersonalSolicitante != null) {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerCajasPorRendirFinanzas where IdCaja is null and EsEnviado=1 and IdCajaPorRendirCabecera is null and IdPersonalResponsable=@idPersonalResponsable and IdPersonalSolicitante=@idPersonalSolicitante";
                }
                else if (idMonedaCaja != null && idPersonalSolicitante == null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerCajasPorRendirFinanzas where IdCaja is null and EsEnviado=1 and IdCajaPorRendirCabecera is null and IdPersonalResponsable=@idPersonalResponsable and IdMoneda=@idMonedaCaja";
                }
                else if (idMonedaCaja != null && idPersonalSolicitante != null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerCajasPorRendirFinanzas where IdCaja is null and EsEnviado=1 and IdCajaPorRendirCabecera is null and IdPersonalResponsable=@idPersonalResponsable and IdMoneda=@idMonedaCaja and IdPersonalSolicitante=@idPersonalSolicitante";
                }
                var cajaPorRendirDB = _dapper.QueryDapper(_query, new { idPersonalResponsable, idMonedaCaja, idPersonalSolicitante });
                if (!cajaPorRendirDB.Contains("[]") && !string.IsNullOrEmpty(cajaPorRendirDB))
                {
                    listaPorRendir = JsonConvert.DeserializeObject<List<CajaPorRendirDTO>>(cajaPorRendirDB);
                }
                return listaPorRendir;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FiltroDTO> ObtenerSolicitante(int idPersonalResponsable)
        {
            try
            {
                var _query = "";
                var camposTabla = "IdPersonalSolicitante as Id ,NombrePersonalSolicitante as Nombre";

                List<FiltroDTO> listaSolicitante = new List<FiltroDTO>();
                _query = "SELECT distinct " + camposTabla + " FROM FIN.V_ObtenerCajasPorRendirFinanzas where IdCaja is null and EsEnviado=1 and IdCajaPorRendirCabecera is null and IdPersonalResponsable=@idPersonalResponsable";
               
                var cajaPorRendirDB = _dapper.QueryDapper(_query, new { idPersonalResponsable });
                if (!cajaPorRendirDB.Contains("[]") && !string.IsNullOrEmpty(cajaPorRendirDB))
                {
                    listaSolicitante = JsonConvert.DeserializeObject<List<FiltroDTO>>(cajaPorRendirDB);
                }
                return listaSolicitante;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de registros con Estado=1 de un Usuario dado un IdPersonal de fin.T_CajasPorRendir (para llenado de grilla en SolicitudEfectivo)
        /// </summary>
        /// <returns></returns>
        public List<CajaPorRendirDTO> ObtenerCajasPorRendirFinanzas(int IdUsuario)
        {
            try
            {
                List<CajaPorRendirDTO> CajaPorRendirFinanzas = new List<CajaPorRendirDTO>();
                //var _query = "SELECT Id, IdFur, CodigoFur,IdPersonalSolicitante,NombrePersonalSolicitante,IdPersonalResponsable,NombrePersonalResponsable,Descripcion,IdMoneda,NombreMoneda,TotalEfectivo,FechaEntregaEfectivo FROM fin.T_CajaPorRendir where Estado=1 AND EsEnviado=0 And IdPersonalSolicitante=" + IdUsuario;
                var _query = "";

                if (IdUsuario == 213) // 213 Usuario de Juan C. Martinez D.
                    _query = "SELECT Id, IdFur, CodigoFur,IdPersonalSolicitante,NombrePersonalSolicitante,IdPersonalResponsable,NombrePersonalResponsable,Descripcion,IdMoneda,NombreMoneda,TotalEfectivo,FechaEntregaEfectivo FROM  [fin].[V_ObtenerCajasPorRendirFinanzas] where Estado=1 AND EsEnviado=0 ";
                else
                    _query = "SELECT Id, IdFur, CodigoFur,IdPersonalSolicitante,NombrePersonalSolicitante,IdPersonalResponsable,NombrePersonalResponsable,Descripcion,IdMoneda,NombreMoneda,TotalEfectivo,FechaEntregaEfectivo FROM  [fin].[V_ObtenerCajasPorRendirFinanzas] where Estado=1 AND EsEnviado=0 And IdPersonalSolicitante=" + IdUsuario;

                var CajaPorRendirFinanzasDB = _dapper.QueryDapper(_query, null);
                if (!CajaPorRendirFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(CajaPorRendirFinanzasDB))
                {
                    CajaPorRendirFinanzas = JsonConvert.DeserializeObject<List<CajaPorRendirDTO>>(CajaPorRendirFinanzasDB);
                }
                return CajaPorRendirFinanzas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CajaPorRendirDTO> ObtenerCajasPorRendirPorIdPorRendirCabecera(int IdCajaPorRendirCabecera)
        {
            try
            {
                List<CajaPorRendirDTO> CajaPorRendirFinanzas = new List<CajaPorRendirDTO>();
                var _query = "SELECT Id, CodigoCaja, CodigoFur, NombrePersonalSolicitante,Descripcion,NombreMoneda,TotalEfectivo,FechaEntregaEfectivo FROM  [fin].[V_ObtenerCajasPorRendirFinanzas] where Estado=1 AND IdCajaPorRendirCabecera=" + IdCajaPorRendirCabecera;
                var CajaPorRendirFinanzasDB = _dapper.QueryDapper(_query, null);
                if (!CajaPorRendirFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(CajaPorRendirFinanzasDB))
                {
                    CajaPorRendirFinanzas = JsonConvert.DeserializeObject<List<CajaPorRendirDTO>>(CajaPorRendirFinanzasDB);
                }
                return CajaPorRendirFinanzas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene un (1) registro con Estado=1 de un Usuario dado un IdPersonal de fin.T_CajasPorRendir (para llenado de grilla en SolicitudEfectivo)
        /// </summary>
        /// <returns></returns>
        public List<CajaPorRendirDTO> ObtenerCajasPorRendirSolicitudFinanzas(int IdUsuario, int IdRegistro)
        {
            try
            {
                List<CajaPorRendirDTO> CajaPorRendirFinanzas = new List<CajaPorRendirDTO>();
                //var _query = "SELECT Id, IdFur, CodigoFur,IdPersonalSolicitante,NombrePersonalSolicitante,IdPersonalResponsable,NombrePersonalResponsable,Descripcion,IdMoneda,NombreMoneda,TotalEfectivo,FechaEntregaEfectivo FROM fin.T_CajaPorRendir where Estado=1 AND EsEnviado=0 And IdPersonalSolicitante=" + IdUsuario;
                var _query = "SELECT Id, IdFur, CodigoFur,IdPersonalSolicitante,NombrePersonalSolicitante,IdPersonalResponsable,NombrePersonalResponsable,Descripcion,IdMoneda,NombreMoneda,TotalEfectivo,FechaEntregaEfectivo FROM  [fin].[V_ObtenerCajasPorRendirFinanzas] where Estado=1 AND EsEnviado=0 And IdPersonalSolicitante=" + IdUsuario + " AND Id="+IdRegistro;
                var CajaPorRendirFinanzasDB = _dapper.QueryDapper(_query, null);
                if (!CajaPorRendirFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(CajaPorRendirFinanzasDB))
                {
                    CajaPorRendirFinanzas = JsonConvert.DeserializeObject<List<CajaPorRendirDTO>>(CajaPorRendirFinanzasDB);
                }
                return CajaPorRendirFinanzas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MontoCajaDTO ObtenerMontoTotalCaja(int IdCaja)
        {
            try
            {
                MontoCajaDTO CajaPorRendirFinanzas = new MontoCajaDTO();
                //var _query = "SELECT Id, IdFur, CodigoFur,IdPersonalSolicitante,NombrePersonalSolicitante,IdPersonalResponsable,NombrePersonalResponsable,Descripcion,IdMoneda,NombreMoneda,TotalEfectivo,FechaEntregaEfectivo FROM fin.T_CajaPorRendir where Estado=1 AND EsEnviado=0 And IdPersonalSolicitante=" + IdUsuario;
                var CajaPorRendirFinanzasDB = _dapper.QuerySPFirstOrDefault("[fin].[SP_EstadoCaja]", new { IdCaja});
                if (!CajaPorRendirFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(CajaPorRendirFinanzasDB))
                {
                    CajaPorRendirFinanzas = JsonConvert.DeserializeObject<MontoCajaDTO>(CajaPorRendirFinanzasDB);
                }
                return CajaPorRendirFinanzas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ActualizarCajaPorRendirAprobacion(CajaPorRendirCabeceraDTO objetoPorRendirCab,int id, integraDBContext context)
        {
            try
            {
                CajaPorRendirRepositorio _repCajaPorRendirRep = new CajaPorRendirRepositorio(context);
                CajaPorRendirBO cajaPorRendir = new CajaPorRendirBO();
                cajaPorRendir = _repCajaPorRendirRep.FirstById(id);

                if (cajaPorRendir == null)
                    throw new Exception("No se encontro el registro de 'CajaPorRendir' que se quiere actualizar");

                cajaPorRendir.FechaAprobacion = DateTime.Now;
                cajaPorRendir.IdCajaPorRendirCabecera = objetoPorRendirCab.Id;
                cajaPorRendir.IdCaja = objetoPorRendirCab.IdCaja;               
                cajaPorRendir.UsuarioModificacion = objetoPorRendirCab.UsuarioModificacion;
                cajaPorRendir.FechaModificacion = DateTime.Now;

                _repCajaPorRendirRep.Update(cajaPorRendir);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertarRegistroPorRendirInmediato(CajaPorRendirDTO objetoPRCabecera,int idPorRendirCabecera , integraDBContext context)
        {

            try
            {
                CajaPorRendirRepositorio _repCajaPorRendirRep = new CajaPorRendirRepositorio(context);
                CajaPorRendirBO cajaPorRendir = new CajaPorRendirBO();
                cajaPorRendir.IdCaja = objetoPRCabecera.IdCaja;
                cajaPorRendir.IdFur = objetoPRCabecera.IdFur;
                cajaPorRendir.IdPersonalSolicitante = objetoPRCabecera.IdPersonalSolicitante;
                cajaPorRendir.IdPersonalResponsableCaja = objetoPRCabecera.IdPersonalResponsable;
                cajaPorRendir.Descripcion = objetoPRCabecera.Descripcion;
                cajaPorRendir.IdMoneda = objetoPRCabecera.IdMoneda;
                cajaPorRendir.TotalEfectivo = objetoPRCabecera.TotalEfectivo;
                cajaPorRendir.FechaEntregaEfectivo = objetoPRCabecera.FechaEntregaEfectivo;
                cajaPorRendir.EsEnviado = true;
                cajaPorRendir.FechaEnvio = DateTime.Now;
                cajaPorRendir.FechaAprobacion = DateTime.Now;
                cajaPorRendir.IdCajaPorRendirCabecera = idPorRendirCabecera;
                cajaPorRendir.Estado = true;
                cajaPorRendir.UsuarioCreacion = objetoPRCabecera.UsuarioModificacion;
                cajaPorRendir.UsuarioModificacion = objetoPRCabecera.UsuarioModificacion;
                cajaPorRendir.FechaModificacion = DateTime.Now;
                cajaPorRendir.FechaCreacion = DateTime.Now;

                _repCajaPorRendirRep.Insert(cajaPorRendir);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
