using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTO;
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: MontoPagoCronogramaRepositorio
    /// Autor: Fischer Valdez - Luis Huallpa - Ansoli Espinoza - Carlos Crispin - Edgar Serruto
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Cronograma de Monto de Pago
    /// </summary>
    public class MontoPagoCronogramaRepositorio : BaseRepository<TMontoPagoCronograma, MontoPagoCronogramaBO>
    {
        #region Metodos Base
        public MontoPagoCronogramaRepositorio() : base()
        {
        }
        public MontoPagoCronogramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MontoPagoCronogramaBO> GetBy(Expression<Func<TMontoPagoCronograma, bool>> filter)
        {
            IEnumerable<TMontoPagoCronograma> listado = base.GetBy(filter);
            List<MontoPagoCronogramaBO> listadoBO = new List<MontoPagoCronogramaBO>();
            foreach (var itemEntidad in listado)
            {
                MontoPagoCronogramaBO objetoBO = Mapper.Map<TMontoPagoCronograma, MontoPagoCronogramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MontoPagoCronogramaBO FirstById(int id)
        {
            try
            {
                TMontoPagoCronograma entidad = base.FirstById(id);
                MontoPagoCronogramaBO objetoBO = new MontoPagoCronogramaBO();
                Mapper.Map<TMontoPagoCronograma, MontoPagoCronogramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MontoPagoCronogramaBO FirstBy(Expression<Func<TMontoPagoCronograma, bool>> filter)
        {
            try
            {
                TMontoPagoCronograma entidad = base.FirstBy(filter);
                MontoPagoCronogramaBO objetoBO = Mapper.Map<TMontoPagoCronograma, MontoPagoCronogramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MontoPagoCronogramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMontoPagoCronograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MontoPagoCronogramaBO> listadoBO)
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

        public bool Update(MontoPagoCronogramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMontoPagoCronograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MontoPagoCronogramaBO> listadoBO)
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
        private void AsignacionId(TMontoPagoCronograma entidad, MontoPagoCronogramaBO objetoBO)
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

        private TMontoPagoCronograma MapeoEntidad(MontoPagoCronogramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMontoPagoCronograma entidad = new TMontoPagoCronograma();
                entidad = Mapper.Map<MontoPagoCronogramaBO, TMontoPagoCronograma>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                ////mapea los hijos
                //if (objetoBO.ListaDetalleCuotas != null && objetoBO.ListaDetalleCuotas.Count > 0)
                //{
                //    foreach (var hijo in objetoBO.ListaDetalleCuotas)
                //    {
                //        TMontoPagoCronogramaDetalle entidadHijo = new TMontoPagoCronogramaDetalle();
                //        entidadHijo = Mapper.Map<MontoPagoCronogramaDetalleBO, TMontoPagoCronogramaDetalle>(hijo,
                //            opt => opt.ConfigureMap(MemberList.None));
                //        entidad.TMontoPagoCronogramaDetalle.Add(entidadHijo);
                //    }
                //}

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        public MontoPagoCronogramaDocumentoDTO ObtenerMontoPagoCronogramaDocumentoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                string _queryMontoPagoCronograma = "Select IdMoneda,PrecioDescuento,IdMontoPago From com.V_TMontoPagoCronograma_DatosDocumento where IdOportunidad=@IdOportunidad";
                var queryMontoPagoCronograma = _dapper.FirstOrDefault(_queryMontoPagoCronograma, new { IdOportunidad = idOportunidad });
                return JsonConvert.DeserializeObject<MontoPagoCronogramaDocumentoDTO>(queryMontoPagoCronograma);
            }
            catch (Exception e)
            {
                return null;
                //throw new Exception(e.Message);
            }
        }
        public MontoPagoCronogramaDocumentoDTO ObtenerMontoPagoCronogramaDocumentoPorIdOportunidadOperaciones(int idOportunidad)
        {
            try
            {
                string _queryMontoPagoCronograma = "Select IdMoneda,PrecioDescuento,IdMontoPago From com.V_TMontoPagoCronograma_DatosDocumentoOperaciones where IdOportunidad=@IdOportunidad";
                var queryMontoPagoCronograma = _dapper.FirstOrDefault(_queryMontoPagoCronograma, new { IdOportunidad = idOportunidad });
                return JsonConvert.DeserializeObject<MontoPagoCronogramaDocumentoDTO>(queryMontoPagoCronograma);
            }
            catch (Exception e)
            {
                return null;
                //throw new Exception(e.Message);
            }
        }

        ///Repositorio: CronogramaPagoDetalleFinal
        ///Autor: Jose Villena
        ///Fecha: 01/05/2021
        /// <summary>
        /// Obtener Detalle monto Pago
        /// </summary>
        /// <param name="idMontoPago"> Id  Monto Pago </param>        
        /// <returns> Lista Detalle Monto Pago : List<DetalleMontoPagoDTO> </returns>
        public List<DetalleMontoPagoDTO> ObtenerDetalleMontoPago(int idMontoPago)
        {
            try
            {
                string sql = @"
                    SELECT 
                        Titulo,
                        OrdenBeneficio
                    FROM pla.V_DetalleBeneficioPorMontoPago
                    WHERE IdMontoPago = @idMontoPago
                ";
                var resultado = _dapper.QueryDapper(sql, new { idMontoPago = idMontoPago });
                return JsonConvert.DeserializeObject<List<DetalleMontoPagoDTO>>(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<MontoPagoCronogramasHistorialDTO> Obtener_HistorialCronograma(int idAsesor)
        {
            try
            {
                var RegistrosDB = _dapper.QuerySPDapper("com.ObtenerHistorialCronogramaPagoPorPeriodoAsesor", new { idAsesor });

                return JsonConvert.DeserializeObject<List<MontoPagoCronogramasHistorialDTO>>(RegistrosDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TipoDescuentoBO> ObtenerTipoDescuento(int idOportunidad, string tipoPersonal)
        {
            try
            {
                List<TipoDescuentoBO> Descuentos = new List<TipoDescuentoBO>();
                var _query = "SELECT Id,Codigo,Descripcion,Formula,PorcentajeGeneral,PorcentajeMatricula,FraccionesMatricula,PorcentajeCuotas,CuotasAdicionales,Tipo FROM mkt.V_TiposDescuentos WHERE  IdOportunidad = @idOportunidad and Tipo = @tipoPersonal";
                var TiposDescuentosDB = _dapper.QueryDapper(_query, new { idOportunidad, tipoPersonal });
                if (!string.IsNullOrEmpty(TiposDescuentosDB) && !TiposDescuentosDB.Contains("[]"))
                {
                    Descuentos = JsonConvert.DeserializeObject<List<TipoDescuentoBO>>(TiposDescuentosDB);
                }
                return Descuentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<MontosPagosVentasBO> ObtenerMontosPagos(int idOportunidad)
        {

            try
            {
                List<MontosPagosVentasBO> Montos = new List<MontosPagosVentasBO>();
                var _query = "SELECT Id,Precio,PrecioLetras,IdMoneda,Matricula,Cuotas,NroCuotas,IdPrograma,IdTipoPago,IdPais,Vencimiento,PrimeraCuota,CuotaDoble,IdTipoDescuento,Formula,PorcentajeGeneral,PorcentajeMatricula,FraccionesMatricula,PorcentajeCuotas,CuotasAdicionales,NombrePlural,CuotasTipoPago,Paquete,Nombre,VisibleWeb,MontoDescontado FROM mkt.V_MontosPagos WHERE  IdOportunidad = @idOportunidad";
                var MontosDB = _dapper.QueryDapper(_query, new { idOportunidad });
                if (!string.IsNullOrEmpty(MontosDB) && !MontosDB.Contains("[]"))
                {
                    Montos = JsonConvert.DeserializeObject<List<MontosPagosVentasBO>>(MontosDB);
                }
                return Montos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            //try
            //{
            //    List<MontosPagosVentasDTO> respuesta = new List<MontosPagosVentasDTO>();

            //    string _query = "exec com.SP_Oportunidad_MontosPagos";

            //    var resultado = _dapper.QuerySPDapper(_query, new { idOportunidad = idOportunidad });

            //    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            //    {
            //        var _registrosBD = JsonConvert.DeserializeObject<List<MontosPagosVentasDTO>>(resultado);
            //        foreach (var item in _registrosBD)
            //        {
            //            var _Datos = new MontosPagosVentasDTO();
            //            _Datos.Id = item.Id;
            //            _Datos.Precio = item.Precio;
            //            _Datos.PrecioLetras = item.PrecioLetras;
            //            _Datos.IdMoneda = item.IdMoneda;
            //            _Datos.Matricula = item.Matricula;
            //            _Datos.Cuotas = item.Cuotas;
            //            _Datos.NroCuotas = item.NroCuotas;
            //            _Datos.IdPrograma = item.IdPrograma;
            //            _Datos.IdTipoPago = item.IdTipoPago;
            //            _Datos.IdPais = item.IdPais;
            //            _Datos.Vencimiento = item.Vencimiento;
            //            _Datos.PrimeraCuota = item.PrimeraCuota;
            //            _Datos.CuotaDoble = item.CuotaDoble;
            //            _Datos.IdTipoDescuento = item.IdTipoDescuento;
            //            _Datos.Formula = item.Formula;
            //            _Datos.PorcentajeGeneral = item.PorcentajeGeneral;
            //            _Datos.PorcentajeMatricula = item.PorcentajeMatricula;
            //            _Datos.FraccionesMatricula = item.FraccionesMatricula;
            //            _Datos.PorcentajeCuotas = item.PorcentajeCuotas;
            //            _Datos.CuotasAdicionales = item.CuotasAdicionales;
            //            _Datos.NombrePlural = item.NombrePlural;
            //            _Datos.CuotasTipoPago = item.CuotasTipoPago;
            //            _Datos.Paquete = item.Paquete;
            //            _Datos.Nombre = item.Nombre;
            //            _Datos.VisibleWeb = item.VisibleWeb;
            //            _Datos.MontoDescontado = item.MontoDescontado;

            //            respuesta.Add(_Datos);
            //        }
            //    }

            //    return respuesta;
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message);
            //}
        }
        public List<DatosMontosComplementariosDTO> ObtenerMontosComplementarios(int IdPGeneral, int IdPPais,int IdMontoPago,int IdMatriculaCabecera)
        {
            try
            {
                List<DatosMontosComplementariosDTO> Resultado = new List<DatosMontosComplementariosDTO>();
                string resultado = _dapper.QuerySPDapper("pla.SP_MontosComplementarios",
                    new { IdPGeneral=IdPGeneral, IdPPais = IdPPais, IdMontoPago=IdMontoPago, IdMatriculaCabecera= IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    Resultado = JsonConvert.DeserializeObject<List<DatosMontosComplementariosDTO>>(resultado);
                    return Resultado;
                }
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DatosUsuarioPortalDTO ObtenerUsuarioClavePortalWeb(int idAlumno, string email)
        {
            try
            {
                DatosUsuarioPortalDTO Resultado = new DatosUsuarioPortalDTO();
                string resultado = _dapper.QuerySPFirstOrDefault("conf.SP_GetUsuarioClavePortalWeb",
                    new {idAlumno = idAlumno, email = email});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado!="null")
                {
                    Resultado = JsonConvert.DeserializeObject<DatosUsuarioPortalDTO>(resultado);
                    return Resultado;
                }
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: MontoPagroCronogramaRepositorio
        /// Autor: Edgar Serruto.
        /// Fecha: 23/06/2021
        /// <summary>
        ///	Crea Usuario de Portal Clave
        /// </summary>
        /// <param name="idAlumno">Id de Alumno</param>
        /// <param name="apellidos">Apellidos</param>
        /// <param name="celular">Celular</param>
        /// <param name="clave">Clave</param>
        /// <param name="claveEncriptada">Clave Encriptada</param>
        /// <param name="email">Email de Alumno</param>
        /// <param name="fecha">Fecha de Creación</param>
        /// <param name="idCodigoCiudad">Id de Código de Ciudad</param>
        /// <param name="idCodigoPais">Id de Código de Pais</param>
        /// <param name="nombres">Nombre de Alumno</param>
        /// <param name="telefono">Teléfono de Alumno</param>
        /// <returns> DatosUsuarioPortalDTO </returns>
        public DatosUsuarioPortalDTO CrearUsuarioClavePortalWeb(int idAlumno, string email, string clave, string claveEncriptada, string nombres, string apellidos,string telefono, string celular,int? idCodigoCiudad ,int? idCodigoPais, DateTime fecha)
        {
            try
            {
                DatosUsuarioPortalDTO respuesta = new DatosUsuarioPortalDTO();
                string resultado = _dapper.QuerySPFirstOrDefault("conf.SP_CreateUsuarioClavePortalWeb",
                    new
                    {
                        IdAlumno = idAlumno, Email = email, Clave = clave, ClaveEncriptada = claveEncriptada,
                        Nombres = nombres, Apellidos = apellidos,Fijo= telefono, Celular = celular,
                        CodigoPais = idCodigoPais,CodigoCiudad = idCodigoCiudad,Fecha = fecha
                    });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<DatosUsuarioPortalDTO>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ResultadoDTO GenerarCronogramaByCoordinador(int CronogramaId)
        {
            try
            {
                ResultadoDTO Resultado = new ResultadoDTO();
                string resultado = _dapper.QuerySPFirstOrDefault("com.SP_GenerarCronogramaVentasByCordinador",
                    new
                    {
                        IdCronograma = CronogramaId,
                    });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    Resultado = JsonConvert.DeserializeObject<ResultadoDTO>(resultado);
                    return Resultado;
                }
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ResultadoDTO EliminarCronogramaVentasByCordinador(int CronogramaId)
        {
            try
            {
                ResultadoDTO Resultado = new ResultadoDTO();
                string resultado = _dapper.QuerySPFirstOrDefault("com.SP_EliminarCronogramaVentasByCordinador",
                    new
                    {
                        IdCronograma = CronogramaId,
                    });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    Resultado= JsonConvert.DeserializeObject<ResultadoDTO>(resultado);
                    return Resultado;
                }
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		public DatosUsuarioPortalDTO ObtenerAccesosPortalWeb(int idAlumno, string email)
		{
			try
			{
				DatosUsuarioPortalDTO Resultado = new DatosUsuarioPortalDTO();
				string resultado = _dapper.QuerySPFirstOrDefault("conf.SP_ObtenerAccesosPortalWeb",
					new { idAlumno = idAlumno, email = email });
				if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
				{
					Resultado = JsonConvert.DeserializeObject<DatosUsuarioPortalDTO>(resultado);
					return Resultado;
				}
				return Resultado;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        public ResultadoDTO CuotasPagadas(string CodigoMatricula)
        {
            try
            {
                ResultadoDTO Resultado = new ResultadoDTO();
                string resultado = _dapper.QuerySPFirstOrDefault("fin.SP_CuotasPagadas",
                    new
                    {
                        CodigoMatricula
                    });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    Resultado = JsonConvert.DeserializeObject<ResultadoDTO>(resultado);
                    return Resultado;
                }
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 06/05/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de Oportunidades de Operaciones
        /// </summary>
        /// <param name="Paginador">paginador</param>
        /// <param name="Filtro"> Filtro Modulo </param>
        /// <param name="FilterGrid"> filtros de grilla </param>        
        /// <returns>Lista</returns>
        public ResultadoFiltroReporteCompromisoDTO ObtenerReporteCompromisoPagoFiltrado(Paginador Paginador, ReporteCompromisoPagoDTO Filtro, GridFilters FilterGrid)
        {
            try
            {
                ResultadoFiltroReporteCompromisoDTO compromisos = new ResultadoFiltroReporteCompromisoDTO();

                var filtros = new
                {
                    ListaCoordinador = Filtro.ListaCoordinador.Count() == 0 ? "0" : string.Join(",", Filtro.ListaCoordinador.Select(x => x)),
                    ListaAlumno = Filtro.ListaAlumno == null ? "" : string.Join(",", Filtro.ListaAlumno.Select(x => x)),
                    ListaCentroCosto = Filtro.ListaCentroCosto == null ? "" : string.Join(",", Filtro.ListaCentroCosto.Select(x => x)),
                    EstadoCuotas = Filtro.EstadoCuotas,
                    FechaGeneradoInicio = Filtro.FechaGeneradoInicio,
                    FechaGeneradoFin = Filtro.FechaGeneradoFin,
                    FechaCompromisoInicio = Filtro.FechaCompromisoInicio,
                    FechaCompromisoFin = Filtro.FechaCompromisoFin,
                    CodigoMatricula = Filtro.CodigoMatricula,
                    Skip = Paginador.skip,
                    Take = Paginador.take,
                    Cantidad = false,
                };

                var filtrosV2 = new
                {
                    ListaCoordinador = Filtro.ListaCoordinador.Count() == 0 ? "0" : string.Join(",", Filtro.ListaCoordinador.Select(x => x)),
                    ListaAlumno = Filtro.ListaAlumno == null ? "" : string.Join(",", Filtro.ListaAlumno.Select(x => x)),
                    ListaCentroCosto = Filtro.ListaCentroCosto == null ? "" : string.Join(",", Filtro.ListaCentroCosto.Select(x => x)),
                    EstadoCuotas = Filtro.EstadoCuotas,
                    FechaGeneradoInicio = Filtro.FechaGeneradoInicio,
                    FechaGeneradoFin = Filtro.FechaGeneradoFin,
                    FechaCompromisoInicio = Filtro.FechaCompromisoInicio,
                    FechaCompromisoFin = Filtro.FechaCompromisoFin,
                    CodigoMatricula = Filtro.CodigoMatricula,
                    Skip = Paginador.skip,
                    Take = Paginador.take,
                    Cantidad = true,
                };

                
                string query = "[fin].[SP_ObtenerInformacionCompromisoPagoAlumnoV2]";                
                var queryDB = _dapper.QuerySPDapper(query, filtros);
                var queryCantidadDB = _dapper.QuerySPFirstOrDefault(query, filtrosV2);
                var rpta = JsonConvert.DeserializeObject<List<ReporteCompromisoPagoDetalleDTO>>(queryDB);
                var total = JsonConvert.DeserializeObject<TotalReporteCompromisoPagoDTO>(queryCantidadDB);
                compromisos.Lista = rpta;
                compromisos.Total = total.Cantidad;
                return compromisos;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
