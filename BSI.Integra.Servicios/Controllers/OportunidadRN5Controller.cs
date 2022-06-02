using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;


namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: OportunidadRN5Controller
    /// Autor: Jose Villena.
    /// Fecha: 09/02/2021
    /// <summary>
    /// Cerrar Oprtunidades de BNC a RN5
    /// </summary>
    [Route("api/OportunidadRN5")]    
    public class OportunidadRN5Controller : Controller
    {
        private readonly integraDBContext _integraDBContext;        
        public OportunidadRN5Controller(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 09/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta la llamada por el servicio para cerrar BNC a RN5
        /// </summary>
        /// <returns><returns>
        [Route("[action]")]
        [HttpGet]        
        public ActionResult EjecutarBNCX()
        {
            ConfiguracionRutinaBncObsoletoBO cerrarBncARn5 = new ConfiguracionRutinaBncObsoletoBO();
            cerrarBncARn5.VerificarOportunidadesParaFaseX();
            return Ok();
        }
        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 09/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Oportunidades a RN5
        /// </summary>
        /// <returns>bool<returns>
        [Route("[Action]/{IdOportunidad}/{Usuario}/{IdOcurrencia}")]
        [HttpGet]
        public ActionResult CerrarOportunidadRN5Automatico(int IdOportunidad, string Usuario,int IdOcurrencia)
        {
            try
            {
                OportunidadRepositorio repOportunidad = new OportunidadRepositorio(_integraDBContext);
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        OportunidadBO oportunidadBD = new OportunidadBO(IdOportunidad, Usuario, _integraDBContext);
                        if (oportunidadBD == null)
                            throw new Exception("No existe oportunidad!");

                        OportunidadDTO oportunidad = new OportunidadDTO();


                        oportunidad.Id = oportunidadBD.Id;
                        oportunidad.IdCentroCosto = oportunidadBD.IdCentroCosto.Value;
                        oportunidad.IdPersonalAsignado = oportunidadBD.IdPersonalAsignado;
                        oportunidad.IdTipoDato = oportunidadBD.IdTipoDato;
                        oportunidad.IdFaseOportunidad = oportunidadBD.IdFaseOportunidad;
                        oportunidad.IdOrigen = oportunidadBD.IdOrigen;
                        oportunidad.IdAlumno = oportunidadBD.IdAlumno;
                        oportunidad.UltimoComentario = oportunidadBD.UltimoComentario;
                        oportunidad.IdActividadDetalleUltima = oportunidadBD.IdActividadDetalleUltima;
                        oportunidad.IdActividadCabeceraUltima = oportunidadBD.IdActividadCabeceraUltima;
                        oportunidad.IdEstadoActividadDetalleUltimoEstado = oportunidadBD.IdEstadoActividadDetalleUltimoEstado;
                        oportunidad.UltimaFechaProgramada = oportunidadBD.UltimaFechaProgramada.ToString();
                        oportunidad.IdEstadoOportunidad = oportunidadBD.IdEstadoOportunidad;
                        oportunidad.IdEstadoOcurrenciaUltimo = oportunidadBD.IdEstadoOcurrenciaUltimo;
                        oportunidad.IdFaseOportunidadMaxima = oportunidadBD.IdFaseOportunidadMaxima;
                        oportunidad.IdFaseOportunidadInicial = oportunidadBD.IdFaseOportunidadInicial;
                        oportunidad.IdCategoriaOrigen = oportunidadBD.IdCategoriaOrigen;
                        oportunidad.IdConjuntoAnuncio = oportunidadBD.IdConjuntoAnuncio;
                        oportunidad.IdCampaniaScoring = oportunidadBD.IdCampaniaScoring;
                        oportunidad.IdFaseOportunidadIp = oportunidadBD.IdFaseOportunidadIp;
                        oportunidad.IdFaseOportunidadIc = oportunidadBD.IdFaseOportunidadIc;
                        oportunidad.FechaEnvioFaseOportunidadPf = oportunidadBD.FechaEnvioFaseOportunidadPf;
                        oportunidad.FechaPagoFaseOportunidadPf = oportunidadBD.FechaPagoFaseOportunidadPf;
                        oportunidad.FechaPagoFaseOportunidadIc = oportunidadBD.FechaPagoFaseOportunidadIc;
                        oportunidad.FechaRegistroCampania = oportunidadBD.FechaRegistroCampania;
                        oportunidad.IdFaseOportunidadPortal = oportunidadBD.IdFaseOportunidadPortal;
                        oportunidad.IdFaseOportunidadPf = oportunidadBD.IdFaseOportunidadPf;
                        oportunidad.CodigoPagoIc = oportunidadBD.CodigoPagoIc;
                        oportunidad.FlagVentaCruzada = oportunidadBD.IdTiempoCapacitacion;
                        oportunidad.IdTiempoCapacitacion = oportunidadBD.IdTiempoCapacitacion;
                        oportunidad.IdTiempoCapacitacionValidacion = oportunidadBD.IdTiempoCapacitacionValidacion;
                        oportunidad.IdSubCategoriaDato = oportunidadBD.IdSubCategoriaDato;
                        oportunidad.IdInteraccionFormulario = oportunidadBD.IdInteraccionFormulario;
                        oportunidad.UrlOrigen = oportunidadBD.UrlOrigen;
                        oportunidad.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                        oportunidad.IdPersonalAreaTrabajo = oportunidadBD.IdPersonalAreaTrabajo;
                        if (oportunidadBD.FechaPaso2 != null)
                        {
                            oportunidad.FechaPaso2 = oportunidadBD.FechaPaso2.Value;
                        }
                        if (oportunidadBD.Paso2 != null)
                        {
                            oportunidad.Paso2 = oportunidadBD.Paso2.Value;
                        }
                        if (oportunidadBD.CodMailing != null)
                        {
                            oportunidad.CodMailing = oportunidadBD.CodMailing;
                        }
                        oportunidad.IdPagina = oportunidadBD.IdPagina;

                        //Finalizar Actividad
                        oportunidadBD.ActividadAntigua = new ActividadDetalleBO();

                        oportunidadBD.ActividadAntigua.Id = oportunidadBD.IdActividadDetalleUltima;
                        oportunidadBD.ActividadAntigua.Comentario = "Cerrado Fase RN5";
                        oportunidadBD.ActividadAntigua.IdOcurrencia = IdOcurrencia;
                        oportunidadBD.ActividadAntigua.IdOcurrenciaActividad = null;
                        oportunidadBD.ActividadAntigua.IdAlumno = oportunidadBD.IdAlumno;
                        oportunidadBD.ActividadAntigua.IdClasificacionPersona = oportunidadBD.IdClasificacionPersona;
                        oportunidadBD.ActividadAntigua.IdOportunidad = oportunidadBD.Id;
                        oportunidadBD.ActividadAntigua.IdCentralLlamada = 0;
                        oportunidadBD.ActividadAntigua.IdActividadCabecera = oportunidadBD.IdActividadCabeceraUltima;
                        oportunidadBD.FinalizarActividad(false, oportunidad);

                        repOportunidad.Update(oportunidadBD);

                        scope.Complete();
                    }
                }
                catch (Exception e)
                {
                    return Ok(false);
                }


                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
    
}
