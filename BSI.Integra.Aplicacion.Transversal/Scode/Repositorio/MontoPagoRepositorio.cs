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
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: MontoPagoRepositorio
    /// Autor: _ _ _ _ _ _ _ .
    /// Fecha: 30/04/2021
    /// <summary>
    /// Repositorio para de tabla T_MontoPago
    /// </summary>
    public class MontoPagoRepositorio : BaseRepository<TMontoPago, MontoPagoBO>
    {
        #region Metodos Base
        public MontoPagoRepositorio() : base()
        {
        }
        public MontoPagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MontoPagoBO> GetBy(Expression<Func<TMontoPago, bool>> filter)
        {
            IEnumerable<TMontoPago> listado = base.GetBy(filter);
            List<MontoPagoBO> listadoBO = new List<MontoPagoBO>();
            foreach (var itemEntidad in listado)
            {
                MontoPagoBO objetoBO = Mapper.Map<TMontoPago, MontoPagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MontoPagoBO FirstById(int id)
        {
            try
            {
                TMontoPago entidad = base.FirstById(id);
                MontoPagoBO objetoBO = new MontoPagoBO();
                Mapper.Map<TMontoPago, MontoPagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MontoPagoBO FirstBy(Expression<Func<TMontoPago, bool>> filter)
        {
            try
            {
                TMontoPago entidad = base.FirstBy(filter);
                MontoPagoBO objetoBO = Mapper.Map<TMontoPago, MontoPagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MontoPagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMontoPago entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MontoPagoBO> listadoBO)
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

        public bool Update(MontoPagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMontoPago entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MontoPagoBO> listadoBO)
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
        private void AsignacionId(TMontoPago entidad, MontoPagoBO objetoBO)
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

        private TMontoPago MapeoEntidad(MontoPagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMontoPago entidad = new TMontoPago();
                entidad = Mapper.Map<MontoPagoBO, TMontoPago>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.MontoPagoPlataforma != null && objetoBO.MontoPagoPlataforma.Count > 0)
                {
                    foreach (var hijo in objetoBO.MontoPagoPlataforma)
                    {
                        TMontoPagoPlataforma entidadHijo = new TMontoPagoPlataforma();
                        entidadHijo = Mapper.Map<MontoPagoPlataformaBO, TMontoPagoPlataforma>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TMontoPagoPlataforma.Add(entidadHijo);
                    }
                }
                if (objetoBO.MontoPagoSuscripcion != null && objetoBO.MontoPagoSuscripcion.Count > 0)
                {
                    foreach (var hijo in objetoBO.MontoPagoSuscripcion)
                    {
                        TMontoPagoSuscripcion entidadHijo = new TMontoPagoSuscripcion();
                        entidadHijo = Mapper.Map<MontoPagoSuscripcionBO, TMontoPagoSuscripcion>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TMontoPagoSuscripcion.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        public MontoPagoPaqueteDTO ObtenerPaquete (int id)
        {
            try
            {
                string _queryMontoPago = "Select Id,Paquete From pla.V_TMontoPago_Obtenerpaquete where Id=@Id and Estado=1";
                var queryMontoPago = _dapper.FirstOrDefault(_queryMontoPago,new { Id=id});
                return JsonConvert.DeserializeObject<MontoPagoPaqueteDTO>(queryMontoPago);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        public List<PaqueteCentroCostoDTO> ObtenerPaquetesIdCentroCosto(int id)
        {
            try
            {
                string _queryMontoPago = "Select IdPaquete,Paquete,IdCentroCosto From pla.V_TCentrocosto_Obtenerpaquete where IdCentroCosto=@Id";
                var queryMontoPago = _dapper.QueryDapper(_queryMontoPago, new { Id = id });
                return JsonConvert.DeserializeObject<List<PaqueteCentroCostoDTO>>(queryMontoPago);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// Repositorio: MontoPagoRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene Versiones por monto de Pago
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> List<MontoPagoEtiquetaDTO> </returns>
        public List<MontoPagoEtiquetaDTO> ObtenerVersionesMontoPago (int idOportunidad)
        {
            try
            {
                string queryVersiones = "pla.SP_GetMontoPagoContadoByOportunidadEtiquetas";
                var resultado = _dapper.QuerySPDapper(queryVersiones, new { idOportunidad });
                return JsonConvert.DeserializeObject<List<MontoPagoEtiquetaDTO>>(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        /// Repositorio: MontoPagoRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene Versiones por monto de Pago
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad </param>
        /// <returns> List<MontoPagoEtiquetaDTO> </returns>
        public List<MontoPagoEtiquetaDTO> ObtenerVersionesMontoPagoV2(int idOportunidad)
        {
            try
            {
                string queryVersiones = "pla.SP_GetMontoPagoContadoByOportunidadEtiquetasV2";
                var respuesta = _dapper.QuerySPDapper(queryVersiones, new { idOportunidad });
                return JsonConvert.DeserializeObject<List<MontoPagoEtiquetaDTO>>(respuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// Repositorio: MontoPagoRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene Monto de Pago Por Id de Oportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> MontoPagoCompuestoDTO </returns>
        public MontoPagoCompuestoDTO ObtenerMontoPagoPorIdOportunidad (int idOportunidad)
        {
            try
            {
                string queryMontopadgo = "pla.SP_GetMontoPagoByIdOportunidad";
                var resultado = _dapper.QuerySPFirstOrDefault(queryMontopadgo, new { idOportunidad });
                return JsonConvert.DeserializeObject<MontoPagoCompuestoDTO>(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public List<MontoPagoCronogramaCompuestoDTO> ObtenerMontoPagoPorIdOportunidadV2(int idOportunidad)
        {
            try
            {
                string _queryMontopadgo = "pla.SP_GetMontoPagoByIdOportunidad";
                var queryMontopadgo = _dapper.QuerySPDapper(_queryMontopadgo, new { idOportunidad });
                return JsonConvert.DeserializeObject<List<MontoPagoCronogramaCompuestoDTO>>(queryMontopadgo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        /// Repositorio: MontoPagoRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene Monto de Pago Contado Por Id de Oportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> MontoPagoCompuestoDTO </returns>
        public MontoPagoCompuestoDTO ObtenerMontoPagoContadoPorIdOportunidad (int idOportunidad)
        {
            try
            {
                string querymontopago = "pla.SP_GetMontoPagoContadoByOportunidadId";
                var resultado = _dapper.QuerySPFirstOrDefault(querymontopago, new { idOportunidad });
                return JsonConvert.DeserializeObject<MontoPagoCompuestoDTO>(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        /// <summary>
        /// Obtiene los Montos de Pago Por Programa
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<MontoPagoPanelDTO> ObtenerMontoPagoPorPrograma(int idPgeneral)
        {
            try
            {
                MontoPagoPlataformaRepositorio repMontoPagoPlataforma = new MontoPagoPlataformaRepositorio();
                MontoPagoSuscripcionRepositorio repMontoPagoSuscripcion = new MontoPagoSuscripcionRepositorio();
                List<MontoPagoPanelDTO> items = new List<MontoPagoPanelDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Precio,PrecioLetras,IdMoneda,Matricula,Cuotas,NroCuotas,IdTipoDescuento,IdPrograma,IdTipoPago," +
                    "IdPais,Vencimiento,PrimeraCuota,CuotaDoble,Descripcion,VisibleWeb,Paquete,PorDefecto,MontoDescontado  FROM pla.V_TMontoPagoPrograma WHERE Estado = 1 and IdPrograma = @idPgeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { idPgeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<MontoPagoPanelDTO>>(respuestaDapper);
                    foreach (var item in items)
                    {
                        item.PlataformasPagos = new List<int>();
                        item.SuscripcionesPagos = new List<int>();
                        var MontoPagoPlataformas = repMontoPagoPlataforma.GetBy(x => x.Estado == true && x.IdMontoPago == item.Id).Select(y => y.IdPlataformaPago).ToList();
                        var MontoPagoSuscripciones = repMontoPagoSuscripcion.GetBy(x => x.Estado == true && x.IdMontoPago == item.Id).Select(y => y.IdSuscripcionProgramaGeneral).ToList();

                        item.PlataformasPagos = MontoPagoPlataformas;
                        item.SuscripcionesPagos = MontoPagoSuscripciones;
                    }
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

		public List<MontoPagoBeneficiosDTO> ObtenerBeneficiosAnexo03(int idProgramaGeneral, int idPais)
		{
			try
			{
				var query = "SELECT DISTINCT IdProgramaGeneral, Paquete, NombrePaquete, Beneficios, IdPais, IdMoneda FROM com.VObtenerBeneficiosAnexo03 WHERE IdProgramaGeneral = @idProgramaGeneral AND IdPais = @idPais and Beneficios is not null";
				var res = _dapper.QueryDapper(query, new { idProgramaGeneral, idPais});
				return JsonConvert.DeserializeObject<List<MontoPagoBeneficiosDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}     


    }
}
