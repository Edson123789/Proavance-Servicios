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
    public class CajaPorRendirCabeceraRepositorio : BaseRepository<TCajaPorRendirCabecera, CajaPorRendirCabeceraBO>
    {
        #region Metodos Base
        public CajaPorRendirCabeceraRepositorio() : base()
        {
        }
        public CajaPorRendirCabeceraRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CajaPorRendirCabeceraBO> GetBy(Expression<Func<TCajaPorRendirCabecera, bool>> filter)
        {
            IEnumerable<TCajaPorRendirCabecera> listado = base.GetBy(filter);
            List<CajaPorRendirCabeceraBO> listadoBO = new List<CajaPorRendirCabeceraBO>();
            foreach (var itemEntidad in listado)
            {
                CajaPorRendirCabeceraBO objetoBO = Mapper.Map<TCajaPorRendirCabecera, CajaPorRendirCabeceraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CajaPorRendirCabeceraBO FirstById(int id)
        {
            try
            {
                TCajaPorRendirCabecera entidad = base.FirstById(id);
                CajaPorRendirCabeceraBO objetoBO = new CajaPorRendirCabeceraBO();
                Mapper.Map<TCajaPorRendirCabecera, CajaPorRendirCabeceraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CajaPorRendirCabeceraBO FirstBy(Expression<Func<TCajaPorRendirCabecera, bool>> filter)
        {
            try
            {
                TCajaPorRendirCabecera entidad = base.FirstBy(filter);
                CajaPorRendirCabeceraBO objetoBO = Mapper.Map<TCajaPorRendirCabecera, CajaPorRendirCabeceraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CajaPorRendirCabeceraBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCajaPorRendirCabecera entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CajaPorRendirCabeceraBO> listadoBO)
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

        public bool Update(CajaPorRendirCabeceraBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCajaPorRendirCabecera entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CajaPorRendirCabeceraBO> listadoBO)
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
        private void AsignacionId(TCajaPorRendirCabecera entidad, CajaPorRendirCabeceraBO objetoBO)
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

        private TCajaPorRendirCabecera MapeoEntidad(CajaPorRendirCabeceraBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCajaPorRendirCabecera entidad = new TCajaPorRendirCabecera();
                entidad = Mapper.Map<CajaPorRendirCabeceraBO, TCajaPorRendirCabecera>(objetoBO,
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

        public int InsertarPorRendirCabecera(CajaPorRendirCabeceraDTO objetoPRCabecera, integraDBContext context)
        {

            try
            {
                CajaPorRendirCabeceraRepositorio _repCajaPorRendirCabRep = new CajaPorRendirCabeceraRepositorio(context);
                CajaPorRendirCabeceraBO PorRendirCab = new CajaPorRendirCabeceraBO();
                PorRendirCab.Codigo = objetoPRCabecera.Codigo;
                PorRendirCab.IdCaja = objetoPRCabecera.IdCaja;
                PorRendirCab.Anho = DateTime.Now.Year;
                PorRendirCab.IdPersonalAprobacion = objetoPRCabecera.IdPersonalAprobacion;
                PorRendirCab.IdPersonalSolicitante = objetoPRCabecera.IdPersonalSolicitante;
                PorRendirCab.Descripcion = objetoPRCabecera.Descripcion;
                PorRendirCab.Observacion = objetoPRCabecera.Observacion;
                PorRendirCab.EsRendido = objetoPRCabecera.EsRendido;
                PorRendirCab.MontoDevolucion = objetoPRCabecera.MontoDevolucion;
                PorRendirCab.Estado = true;
                PorRendirCab.UsuarioCreacion = objetoPRCabecera.UsuarioModificacion;
                PorRendirCab.UsuarioModificacion = objetoPRCabecera.UsuarioModificacion;
                PorRendirCab.FechaModificacion = DateTime.Now;
                PorRendirCab.FechaCreacion = DateTime.Now;

                _repCajaPorRendirCabRep.Insert(PorRendirCab);
                return PorRendirCab.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<FiltroDTO> ObtenerCajaPorRendirAutocomplete(string codigo)
        {
            try
            {
                var listaPorRendir = this.GetBy(x => x.Estado == true && x.Codigo.Contains(codigo), x => new FiltroDTO { Id = x.Id, Nombre = x.Codigo.ToUpper()}).ToList();
                return listaPorRendir;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public List<CajaPorRendirCabeceraRendicionDTO> ObtenerCajasPorRendirParaRendicion(int IdUsuario)
        {
            try
            {
                List<CajaPorRendirCabeceraRendicionDTO> CajaPorRendirFinanzas = new List<CajaPorRendirCabeceraRendicionDTO>();
               
                var _query = "";

                if (IdUsuario == 213) // 213 Usuario de Juan C. Martinez D.
                    _query = "exec fin.SP_ObtenerRegistrosPorRendirTodoPersonal";
                else
                    _query = "exec fin.SP_ObtenerRegistrosPorRendir @IdPersonal";
                


        var CajaPorRendirFinanzasDB = _dapper.QueryDapper(_query, new { IdPersonal = IdUsuario });
                if (!CajaPorRendirFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(CajaPorRendirFinanzasDB))
                {
                    CajaPorRendirFinanzas = JsonConvert.DeserializeObject<List<CajaPorRendirCabeceraRendicionDTO>>(CajaPorRendirFinanzasDB);
                }
                return CajaPorRendirFinanzas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene el registro de la vista de CajaPorRendirCabecera con filtros por fechas
        /// </summary>
        /// <param name="FechaInicial"></param>
        /// <param name="FechaFinal"></param>
        /// <param name="IdCaja"></param>
        /// <returns></returns>
        public List<CajaPorRendirGenerarPdfDTO> ObtenerCajaPorRendirByFecha(DateTime? FechaInicial, DateTime? FechaFinal, int IdCaja)
        {
            try
            {
                var _query = "";
                var cajaPorRendirDB = "";
                var camposTabla = "IdPorRendirCabecera ,CodigoPorRendir,CodigoFur,EntregadoA,MontoTotal,MontoPendienteRendicion,FechaAprobacion,MontoDevolucion,FechasRendicion,CodigoCajaEgreso,Detalle,Observacion,IdMoneda, Moneda  ";

                List<CajaPorRendirGenerarPdfDTO> listaPorRendir = new List<CajaPorRendirGenerarPdfDTO>();
                if (!FechaFinal.HasValue && !FechaFinal.HasValue)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerDatosCajaPorRendirPDF where IdCaja=@idCaja order by CodigoCaja,IdPorRendirCabecera Asc";
                    cajaPorRendirDB = _dapper.QueryDapper(_query, new { idCaja = IdCaja });
                }
                else if (FechaFinal.HasValue && FechaFinal.HasValue)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerDatosCajaPorRendirPDF WHERE IdCaja=@idCaja and Convert(Date,FechaAprobacion)>=@fechaInicial and Convert(Date, FechaAprobacion)  <= @fechaFinal Order By CodigoCaja,IdPorRendirCabecera Asc";
                    cajaPorRendirDB = _dapper.QueryDapper(_query, new { fechaInicial = FechaInicial.Value.Date, fechaFinal = FechaFinal.Value.Date, idCaja = IdCaja });

                }
                else if (FechaFinal.HasValue && !FechaFinal.HasValue)
                {
                    FechaFinal = DateTime.Now;
                    cajaPorRendirDB = _dapper.QueryDapper(_query, null);
                }
                if (!string.IsNullOrEmpty(cajaPorRendirDB) && !cajaPorRendirDB.Contains("[]"))
                {
                    listaPorRendir = JsonConvert.DeserializeObject<List<CajaPorRendirGenerarPdfDTO>>(cajaPorRendirDB);
                }

                return listaPorRendir;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene datos para la generacion de PDF
        /// </summary>
        /// <param name="IdEgresoCaja"></param>
        /// <returns></returns>
        public List<CajaPorRendirGenerarPdfDTO> ObtenerDatosCajaPorRendir(int[] IdPorRendirCabecera)
        {
            try
            {
                var _query = "";
                var cajaRecDB = "";
                var camposTabla = "SELECT  IdPorRendirCabecera,CodigoPorRendir,RazonSocial,Direccion,Ruc,Central,CuentaCaja,FechaAprobacion,CodigoFur,EntregadoA,MontoTotal,Moneda,Detalle,PersonalResponsable";

                List<CajaPorRendirGenerarPdfDTO> listaPorRendir = new List<CajaPorRendirGenerarPdfDTO>();
                _query = camposTabla + " FROM FIN.V_ObtenerDatosCajaPorRendirPDF where IdPorRendirCabecera IN @IdsPorRendir";
                cajaRecDB = _dapper.QueryDapper(_query, new { IdsPorRendir = IdPorRendirCabecera });

                if (!string.IsNullOrEmpty(cajaRecDB) && !cajaRecDB.Contains("[]"))
                {
                    listaPorRendir = JsonConvert.DeserializeObject<List<CajaPorRendirGenerarPdfDTO>>(cajaRecDB);
                }

                return listaPorRendir;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
