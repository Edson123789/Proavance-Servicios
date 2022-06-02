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
    public class CajaEgresoAprobadoRepositorio : BaseRepository<TCajaEgresoAprobado, CajaEgresoAprobadoBO>
    {
        #region Metodos Base
        public CajaEgresoAprobadoRepositorio() : base()
        {
        }
        public CajaEgresoAprobadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CajaEgresoAprobadoBO> GetBy(Expression<Func<TCajaEgresoAprobado, bool>> filter)
        {
            IEnumerable<TCajaEgresoAprobado> listado = base.GetBy(filter);
            List<CajaEgresoAprobadoBO> listadoBO = new List<CajaEgresoAprobadoBO>();
            foreach (var itemEntidad in listado)
            {
                CajaEgresoAprobadoBO objetoBO = Mapper.Map<TCajaEgresoAprobado, CajaEgresoAprobadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CajaEgresoAprobadoBO FirstById(int id)
        {
            try
            {
                TCajaEgresoAprobado entidad = base.FirstById(id);
                CajaEgresoAprobadoBO objetoBO = new CajaEgresoAprobadoBO();
                Mapper.Map<TCajaEgresoAprobado, CajaEgresoAprobadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CajaEgresoAprobadoBO FirstBy(Expression<Func<TCajaEgresoAprobado, bool>> filter)
        {
            try
            {
                TCajaEgresoAprobado entidad = base.FirstBy(filter);
                CajaEgresoAprobadoBO objetoBO = Mapper.Map<TCajaEgresoAprobado, CajaEgresoAprobadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CajaEgresoAprobadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCajaEgresoAprobado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CajaEgresoAprobadoBO> listadoBO)
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

        public bool Update(CajaEgresoAprobadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCajaEgresoAprobado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CajaEgresoAprobadoBO> listadoBO)
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
        private void AsignacionId(TCajaEgresoAprobado entidad, CajaEgresoAprobadoBO objetoBO)
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

        private TCajaEgresoAprobado MapeoEntidad(CajaEgresoAprobadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCajaEgresoAprobado entidad = new TCajaEgresoAprobado();
                entidad = Mapper.Map<CajaEgresoAprobadoBO, TCajaEgresoAprobado>(objetoBO,
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

        public int InsertarRegistroEgresoAprobado(CajaEgresoAprobadoDTO registroEgresoAprobado)
        {

            try
            {
                CajaEgresoAprobadoBO egresoAprobado = new CajaEgresoAprobadoBO();
                egresoAprobado.CodigoRec = registroEgresoAprobado.CodigoRec;
                egresoAprobado.IdCaja = registroEgresoAprobado.IdCaja;
                egresoAprobado.Anho = registroEgresoAprobado.Anho;
                egresoAprobado.FechaCreacionRegistro = registroEgresoAprobado.FechaCreacionRegistro;
                egresoAprobado.Detalle = registroEgresoAprobado.Detalle;
                egresoAprobado.Observacion = registroEgresoAprobado.Observacion;
                egresoAprobado.Origen = registroEgresoAprobado.Origen;
                egresoAprobado.Estado = true;
                egresoAprobado.UsuarioCreacion = registroEgresoAprobado.UsuarioModificacion;
                egresoAprobado.UsuarioModificacion = registroEgresoAprobado.UsuarioModificacion;
                egresoAprobado.FechaModificacion = DateTime.Now;
                egresoAprobado.FechaCreacion = DateTime.Now;

                this.Insert(egresoAprobado);
                return egresoAprobado.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el registro de la vista de CajaPorRendirCabecera con filtros por fechas
        /// </summary>
        /// <param name="FechaInicial"></param>
        /// <param name="FechaFinal"></param>
        /// <param name="IdCaja"></param>
        /// <returns></returns>
        public List<CajaEgresoGenerarPdfDTO> ObtenerCajaEgresoAprobadoByFecha(DateTime? FechaInicial, DateTime? FechaFinal, int IdCaja)
        {
            try
            {
                var _query = "";
                var cajaEgresoDB = "";
                var camposTabla = "IdCajaEgresoAprobado,CodigoEgresoCaja,CodigosPorRendir,MontoTotal,FechaGeneracionREC,CodigoFur,Origen,RucProveedor,NombreProveedor, TipoDocumentosSunat,Comprobantes, FechaEmisionRecibo,EntregadoA,Detalle, Observacion,IdMoneda,Moneda  ";

                List<CajaEgresoGenerarPdfDTO> listaEgreso = new List<CajaEgresoGenerarPdfDTO>();
                if (!FechaFinal.HasValue && !FechaFinal.HasValue)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerDatosCajaEgresoPDF where IdCaja=@idCaja order by CodigoCaja, IdCajaEgresoAprobado Asc";
                    cajaEgresoDB = _dapper.QueryDapper(_query, new { idCaja = IdCaja });
                }
                else if (FechaFinal.HasValue && FechaFinal.HasValue)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerDatosCajaEgresoPDF WHERE IdCaja=@idCaja and Convert(Date,FechaGeneracionREC)>=@fechaInicial and Convert(Date, FechaGeneracionREC)  <= @fechaFinal Order By CodigoCaja,IdCajaEgresoAprobado Asc";
                    cajaEgresoDB = _dapper.QueryDapper(_query, new { fechaInicial = FechaInicial.Value.Date, fechaFinal = FechaFinal.Value.Date, idCaja = IdCaja });

                }
                else if (FechaFinal.HasValue && !FechaFinal.HasValue)
                {
                    FechaFinal = DateTime.Now;
                    cajaEgresoDB = _dapper.QueryDapper(_query, null);
                }
                if (!string.IsNullOrEmpty(cajaEgresoDB) && !cajaEgresoDB.Contains("[]"))
                {
                    listaEgreso = JsonConvert.DeserializeObject<List<CajaEgresoGenerarPdfDTO>>(cajaEgresoDB);
                }

                return listaEgreso;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CajaEgresoGenerarPdfDTO> ObtenerDatosCajaEgreso(int[] IdEgresoCaja)
        {
            try
            {
                var _query = "";
                var cajaRecDB = "";
                var camposTabla = "SELECT IdCajaEgresoAprobado,CodigoEgresoCaja,RazonSocial,Direccion,Ruc,Central,NumeroCuenta,FechaGeneracionREC,CodigoFur,EntregadoA,NombreProveedor,RucProveedor,TipoDocumentosSunat,Comprobantes,MontoTotal,Moneda,Detalle,Responsable ";

                List<CajaEgresoGenerarPdfDTO> listaNIC = new List<CajaEgresoGenerarPdfDTO>();
                _query = camposTabla + " FROM FIN.V_ObtenerDatosCajaEgresoPDF where IdCajaEgresoAprobado IN @IdsRec";
                cajaRecDB = _dapper.QueryDapper(_query, new { IdsRec = IdEgresoCaja });

                if (!string.IsNullOrEmpty(cajaRecDB) && !cajaRecDB.Contains("[]"))
                {
                    listaNIC = JsonConvert.DeserializeObject<List<CajaEgresoGenerarPdfDTO>>(cajaRecDB);
                }

                return listaNIC;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
