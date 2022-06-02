using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FurConfiguracionAutomatica")]
    public class FurConfiguracionAutomaticaController : Controller
    {
        private readonly integraDBContext _integraDBContext ;
        public FurConfiguracionAutomaticaController() {
            _integraDBContext = new integraDBContext();
        }

        #region ServicosAdicionales
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosFurTipoPedido()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurTipoPedidoRepositorio _repoSede = new FurTipoPedidoRepositorio(_integraDBContext);
                var ListaElementos = _repoSede.ObtenerFurTiposPedidos();
                return Json(new { Result = "OK", Records = ListaElementos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosAreaNegocio()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SedeRepositorio _repoSede = new SedeRepositorio(_integraDBContext);
                var ListaElementos = _repoSede.ObtenerNombreCiudadPorCede();
                return Json(new { Result = "OK", Records = ListaElementos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaTipoConfiguracion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurTipoSolicitudRepositorio _repoFurTipoSolicitud = new FurTipoSolicitudRepositorio(_integraDBContext);
                var ListaElementos = _repoFurTipoSolicitud.ObtenerFurTipoSolicitud();
                return Json(new { Result = "OK", Records = ListaElementos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosCentroCostoAutoComplete(string NombreParcial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // evita que se devuelva todos los nombre (  todos encajan con string vacio ("")  )
                if (NombreParcial == null || NombreParcial.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return Json(new { Result = "OK", Records = ListaVacia });
                }


                CentroCostoRepositorio repCentroCostoRep = new CentroCostoRepositorio(_integraDBContext);
                var ListaElementos = repCentroCostoRep.GetBy(x => x.Estado == true && x.Nombre.Contains(NombreParcial), x => new { x.Id, x.Nombre }).ToList();
                return Json(new { Result = "OK", Records = ListaElementos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }



        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosProductoPorProveedor(int IdProveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                HistoricoProductoProveedorRepositorio _repoHistoricoProductoProveedor = new HistoricoProductoProveedorRepositorio(_integraDBContext);
                var ListaElementos = _repoHistoricoProductoProveedor.ObtenerListaProductoPorProveedor(IdProveedor);
                return Json(new { Result = "OK", Records = ListaElementos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosCronogramaAutoComplete(string NombreParcial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // evita que se devuelva todos los nombre (  todos encajan con string vacio ("")  )
                if (NombreParcial == null || NombreParcial.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return Json(new { Result = "OK", Records = ListaVacia });
                }


                GastoFinancieroCronogramaRepositorio _repoGastoFinancieroCronograma = new GastoFinancieroCronogramaRepositorio(_integraDBContext);
                var ListaElementos = _repoGastoFinancieroCronograma.GetBy(x => x.Estado == true && x.Nombre.Contains(NombreParcial), x => new { x.Id, x.Nombre }).ToList();
                return Json(new { Result = "OK", Records = ListaElementos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaCuotasPorCronograma(int IdCronograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GastoFinancieroCronogramaDetalleRepositorio _repoGastoFinancieroCronogramaDetalle = new GastoFinancieroCronogramaDetalleRepositorio(_integraDBContext);
                var ListaElementos = _repoGastoFinancieroCronogramaDetalle.ObtenerListaGastoFinancieroCronogramaDetallePorIdGastoFinanciero(IdCronograma);
                return Json(new { Result = "OK", Records = ListaElementos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosFrecuencia()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FrecuenciaRepositorio _repoFrecuencia = new FrecuenciaRepositorio(_integraDBContext);
                var ListaElementos = _repoFrecuencia.ObtenerListaFrecuencia();
                return Json(new { Result = "OK", Records = ListaElementos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }



        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosAreaSolicitante()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalAreaTrabajoRepositorio _repoPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
                var ListaElementos = _repoPersonalAreaTrabajo.ObtenerAreaTrabajoPersonal();
                return Json(new { Result = "OK", Records = ListaElementos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        #endregion


        //Se agrega idEmpresa
        #region CRUD
        [Route("[action]")]
        [HttpGet]
        public ActionResult VisualizarFurConfiguracionAutomatica()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurConfiguracionAutomaticaRepositorio _repoFurConfiguracionAutomatica = new FurConfiguracionAutomaticaRepositorio(_integraDBContext);
                var RegistrosFurConfiguracionAutomatica = _repoFurConfiguracionAutomatica.ObtenerRegistrosFurConfiguracionAutomatica();

                return Ok(new { Result = "OK", Records = RegistrosFurConfiguracionAutomatica });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarFurConfiguracionAutomatica([FromBody] FurConfiguracionAutomaticaDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurConfiguracionAutomaticaRepositorio _repoFurConfiguracionAutomatica = new FurConfiguracionAutomaticaRepositorio(_integraDBContext);

                FurConfiguracionAutomaticaBO FurConfiguracionAutomatica = new FurConfiguracionAutomaticaBO();

                FurConfiguracionAutomatica.IdFurTipoSolicitud = RequestDTO.IdFurTipoSolicitud;  // si es gasto financiero o costo fijo
                FurConfiguracionAutomatica.IdSede = RequestDTO.IdSede;
                FurConfiguracionAutomatica.IdEmpresa = RequestDTO.IdEmpresa;
                FurConfiguracionAutomatica.IdFurTipoPedido = RequestDTO.IdFurTipoPedido; // dato para el fur  (gasto imediato, ...);
                FurConfiguracionAutomatica.IdPersonalAreaTrabajo = RequestDTO.IdPersonalAreaTrabajo;
                FurConfiguracionAutomatica.Cantidad = RequestDTO.Cantidad;
                FurConfiguracionAutomatica.IdMonedaPagoReal = RequestDTO.IdMonedaPagoReal;
                FurConfiguracionAutomatica.AjusteNumeroSemana = RequestDTO.AjusteNumeroSemana;
                FurConfiguracionAutomatica.IdHistoricoProductoProveedor = RequestDTO.IdHistoricoProductoProveedor;
                FurConfiguracionAutomatica.IdFrecuencia = RequestDTO.IdFrecuencia;
                FurConfiguracionAutomatica.IdCentroCosto = RequestDTO.IdCentroCosto;
                FurConfiguracionAutomatica.Descripcion = RequestDTO.Descripcion;
                FurConfiguracionAutomatica.FechaGeneracionFur = RequestDTO.FechaGeneracionFur;
                FurConfiguracionAutomatica.FechaInicioConfiguracion = RequestDTO.FechaInicioConfiguracion;
                FurConfiguracionAutomatica.FechaFinConfiguracion = RequestDTO.FechaFinConfiguracion;

                FurConfiguracionAutomatica.Estado = true;
                FurConfiguracionAutomatica.UsuarioCreacion = RequestDTO.Usuario;
                FurConfiguracionAutomatica.UsuarioModificacion = RequestDTO.Usuario;
                FurConfiguracionAutomatica.FechaCreacion = DateTime.Now;
                FurConfiguracionAutomatica.FechaModificacion = DateTime.Now;

                _repoFurConfiguracionAutomatica.Insert(FurConfiguracionAutomatica);


                var RegistroReciente = _repoFurConfiguracionAutomatica.ObtenerRegistrosFurConfiguracionAutomaticaPorId(FurConfiguracionAutomatica.Id);
                if (RegistroReciente.Count == 0) new Exception("No se encontro la configuracion de fur insertada ¿Id Correcto?");

                return Ok(RegistroReciente[0]);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarMasivoFurConfiguracionAutomatica([FromBody] FurConfiguracionAutomaticaListaDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurConfiguracionAutomaticaRepositorio _repoFurConfiguracionAutomatica = new FurConfiguracionAutomaticaRepositorio(_integraDBContext);

                int[] Ids = new int[RequestDTO.ConfiguracionesAutomaticas.Count];
                for (int i = 0; i<RequestDTO.ConfiguracionesAutomaticas.Count ; ++i)
                {
                    FurConfiguracionAutomaticaBO FurConfiguracionAutomatica = new FurConfiguracionAutomaticaBO();

                    FurConfiguracionAutomatica.IdFurTipoSolicitud = RequestDTO.ConfiguracionesAutomaticas[i].IdFurTipoSolicitud; // si es gasto financiero o costo fijo
                    FurConfiguracionAutomatica.IdSede = RequestDTO.ConfiguracionesAutomaticas[i].IdSede;
                    FurConfiguracionAutomatica.IdFurTipoPedido = RequestDTO.ConfiguracionesAutomaticas[i].IdFurTipoPedido; // dato para el fur  (gasto imediato, ...);
                    FurConfiguracionAutomatica.IdPersonalAreaTrabajo = RequestDTO.ConfiguracionesAutomaticas[i].IdPersonalAreaTrabajo;
                    FurConfiguracionAutomatica.Cantidad = RequestDTO.ConfiguracionesAutomaticas[i].Cantidad;
                    FurConfiguracionAutomatica.IdMonedaPagoReal = RequestDTO.ConfiguracionesAutomaticas[i].IdMonedaPagoReal;
                    FurConfiguracionAutomatica.AjusteNumeroSemana = RequestDTO.ConfiguracionesAutomaticas[i].AjusteNumeroSemana;
                    FurConfiguracionAutomatica.IdHistoricoProductoProveedor = RequestDTO.ConfiguracionesAutomaticas[i].IdHistoricoProductoProveedor;
                    FurConfiguracionAutomatica.IdFrecuencia = RequestDTO.ConfiguracionesAutomaticas[i].IdFrecuencia;
                    FurConfiguracionAutomatica.IdCentroCosto = RequestDTO.ConfiguracionesAutomaticas[i].IdCentroCosto;
                    FurConfiguracionAutomatica.Descripcion = RequestDTO.ConfiguracionesAutomaticas[i].Descripcion;
                    FurConfiguracionAutomatica.FechaGeneracionFur = RequestDTO.ConfiguracionesAutomaticas[i].FechaGeneracionFur;
                    FurConfiguracionAutomatica.FechaInicioConfiguracion = RequestDTO.ConfiguracionesAutomaticas[i].FechaInicioConfiguracion;
                    FurConfiguracionAutomatica.FechaFinConfiguracion = RequestDTO.ConfiguracionesAutomaticas[i].FechaFinConfiguracion;

                    FurConfiguracionAutomatica.Estado = true;
                    FurConfiguracionAutomatica.UsuarioCreacion = RequestDTO.ConfiguracionesAutomaticas[i].Usuario;
                    FurConfiguracionAutomatica.UsuarioModificacion = RequestDTO.ConfiguracionesAutomaticas[i].Usuario;
                    FurConfiguracionAutomatica.FechaCreacion = DateTime.Now;
                    FurConfiguracionAutomatica.FechaModificacion = DateTime.Now;

                    _repoFurConfiguracionAutomatica.Insert(FurConfiguracionAutomatica);
                    Ids[i] = FurConfiguracionAutomatica.Id;
                }

                List<FurConfiguracionAutomaticaConProductoDTO> lista = new List<FurConfiguracionAutomaticaConProductoDTO>();
                for (int i=0; i<Ids.Length; ++i)
                {
                    var RegistroReciente = _repoFurConfiguracionAutomatica.ObtenerRegistrosFurConfiguracionAutomaticaPorId(Ids[i]);
                    lista.Add(RegistroReciente[0]);
                }

                return Ok(new { Result = "OK", Records = lista });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Abelson Quiñones
        /// Fecha: 14/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualizar el FurConfigurado de centro de costos ya registrado
        /// </summary>
        /// <param name="RequestDTO">El objeto del fur configurado FurConfiguracionAutomaticaDTO </param>
        /// <returns>FurConfiguracionAutomaticaDTO</returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarFurConfiguracionAutomatica([FromBody] FurConfiguracionAutomaticaDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurConfiguracionAutomaticaRepositorio _repoFurConfiguracionAutomatica = new FurConfiguracionAutomaticaRepositorio(_integraDBContext);

                FurConfiguracionAutomaticaBO FurConfiguracionAutomatica = _repoFurConfiguracionAutomatica.GetBy(x => x.Id == RequestDTO.Id).FirstOrDefault();
                if (FurConfiguracionAutomatica == null) throw new Exception("El registro que se desea actualizar no existe ¿Id correcto?");


                FurConfiguracionAutomatica.IdFurTipoSolicitud = RequestDTO.IdFurTipoSolicitud; 
                FurConfiguracionAutomatica.IdSede = RequestDTO.IdSede;
                FurConfiguracionAutomatica.IdEmpresa = RequestDTO.IdEmpresa;
                FurConfiguracionAutomatica.IdFurTipoPedido = RequestDTO.IdFurTipoPedido; // dato para el fur  (gasto imediato, ...);
                FurConfiguracionAutomatica.IdPersonalAreaTrabajo = RequestDTO.IdPersonalAreaTrabajo;
                FurConfiguracionAutomatica.Cantidad = RequestDTO.Cantidad;
                FurConfiguracionAutomatica.IdMonedaPagoReal = RequestDTO.IdMonedaPagoReal;
                FurConfiguracionAutomatica.AjusteNumeroSemana = RequestDTO.AjusteNumeroSemana;
                FurConfiguracionAutomatica.IdHistoricoProductoProveedor = RequestDTO.IdHistoricoProductoProveedor;
                FurConfiguracionAutomatica.IdFrecuencia = RequestDTO.IdFrecuencia;
                FurConfiguracionAutomatica.IdCentroCosto = RequestDTO.IdCentroCosto;
                FurConfiguracionAutomatica.Descripcion = RequestDTO.Descripcion;
                FurConfiguracionAutomatica.FechaGeneracionFur = RequestDTO.FechaGeneracionFur;
                FurConfiguracionAutomatica.FechaInicioConfiguracion = RequestDTO.FechaInicioConfiguracion;
                FurConfiguracionAutomatica.FechaFinConfiguracion = RequestDTO.FechaFinConfiguracion;

                FurConfiguracionAutomatica.Estado = true;
                FurConfiguracionAutomatica.UsuarioModificacion = RequestDTO.Usuario;
                FurConfiguracionAutomatica.FechaModificacion = DateTime.Now;

                _repoFurConfiguracionAutomatica.Update(FurConfiguracionAutomatica);

                var RegistroReciente = _repoFurConfiguracionAutomatica.ObtenerRegistrosFurConfiguracionAutomaticaPorId(FurConfiguracionAutomatica.Id);
                if (RegistroReciente.Count == 0) new Exception("No se encontro la configuracion de fur insertada ¿Id Correcto?");

                return Ok(RegistroReciente[0]);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarFurConfiguracionAutomatica([FromBody] EliminarDTO eliminarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurConfiguracionAutomaticaRepositorio _repoFurConfiguracionAutomatica = new FurConfiguracionAutomaticaRepositorio(_integraDBContext);

                
                FurConfiguracionAutomaticaBO FurConfiguracionAutomatica = _repoFurConfiguracionAutomatica.GetBy(x => x.Id == eliminarDTO.Id).FirstOrDefault();
                if (FurConfiguracionAutomatica == null) throw new Exception("El registro que se desea eliminar no existe ¿Id correcto?");

                _repoFurConfiguracionAutomatica.Delete(eliminarDTO.Id, eliminarDTO.NombreUsuario);
                return Ok(FurConfiguracionAutomatica);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
		#endregion

		[Route("[Action]")]
		[HttpPost]
		public ActionResult GenerarFurAutomatico()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				FurConfiguracionAutomaticaRepositorio _repFurConfiguracionAutomatica = new FurConfiguracionAutomaticaRepositorio();
				PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio();
				var configuracionAutomatica = _repFurConfiguracionAutomatica.ObtenerRegistrosFurConfiguracionAutomatica();
				
				var estado = false;
				FurRepositorio _repFur = new FurRepositorio();
				FurBO fur;
				foreach (var item in configuracionAutomatica)
				{
					var areaTrabajoAbrev = _repPersonalAreaTrabajo.FirstById(item.IdPersonalAreaTrabajo).Codigo;
					fur = new FurBO();
					var furInsertar = fur.obtenerFurInsertar(item, areaTrabajoAbrev);
					if(furInsertar)
						estado = _repFur.Insert(fur);
				}

				return Ok(estado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult GenerarFurAutomatico6()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				FurConfiguracionAutomaticaRepositorio _repFurConfiguracionAutomatica = new FurConfiguracionAutomaticaRepositorio();
				PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio();
				var configuracionAutomatica = _repFurConfiguracionAutomatica.ObtenerRegistrosFurConfiguracionAutomatica();

				var estado = false;
				FurRepositorio _repFur = new FurRepositorio();
				FurBO fur;
				foreach (var item in configuracionAutomatica)
				{
					var areaTrabajoAbrev = _repPersonalAreaTrabajo.FirstById(item.IdPersonalAreaTrabajo).Codigo;
					fur = new FurBO();
					bool furInsertar = false;
					for(int i = 0; i< 6; i++)
					{
						furInsertar = fur.obtenerFurInsertarAlterno(item, areaTrabajoAbrev, i);
						if (furInsertar)
						{
							estado = _repFur.Insert(fur);
							fur = new FurBO();
						}
							

					}
					
					
				}

				return Ok(estado);
			}
			catch (Exception e)
			{
				return BadRequest(ModelState);
			}
		}


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarEstadoAutomatico()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurConfiguracionAutomaticaRepositorio _repoFurConfiguracionAutomatica = new FurConfiguracionAutomaticaRepositorio(_integraDBContext);
                _repoFurConfiguracionAutomatica.EjecutarActualizacionEstadoFurOComprobante();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(ModelState);
            }
        }
    }
    
}
