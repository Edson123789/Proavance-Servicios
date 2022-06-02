using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: GestionRemuneracionPuestoTrabajo
    /// Autor: ---, Jashin Salazar
    /// Fecha: 07/07/2021
    /// <summary>
    /// Contiene los endpoints para la interfaz (O) Compensacion por puesto de trabajo
    /// </summary>
    [Route("api/GestionRemuneracionPuestoTrabajo")]
    [ApiController]
    public class GestionRemuneracionPuestoTrabajoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly DatoContratoPersonalRepositorio _repDatoContratoPersonal;
        private readonly PersonalAreaTrabajoRepositorio _repAreaTrabajo;
        private readonly TipoContratoRepositorio _repTipoContrato;
        private readonly PuestoTrabajoRepositorio _repPuestoTrabajo;
        private readonly PersonalRepositorio _repPersonal;
        private readonly CargoRepositorio _repCargo;
        private readonly TipoPerfilRepositorio _repTipoPerfil;

        private readonly ContratoEstadoRepositorio _repContratoEstado;
        private readonly DatoContratoComisionBonoRepositorio _repDatoContratoComisionBono;
        private readonly PuestoTrabajoRemuneracionRepositorio _repPuestoTrabajoRemuneracion;
        private readonly PuestoTrabajoRemuneracionDetalleRepositorio _repPuestoTrabajoRemuneracionVariable;
        private readonly PaisRepositorio _repPais;
        public GestionRemuneracionPuestoTrabajoController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;

            _repDatoContratoPersonal = new DatoContratoPersonalRepositorio(_integraDBContext);
            _repAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
            _repTipoContrato = new TipoContratoRepositorio(_integraDBContext);
            _repPuestoTrabajo = new PuestoTrabajoRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repCargo = new CargoRepositorio(_integraDBContext);
            _repTipoPerfil = new TipoPerfilRepositorio(_integraDBContext);
            _repContratoEstado = new ContratoEstadoRepositorio(_integraDBContext);
            _repDatoContratoComisionBono = new DatoContratoComisionBonoRepositorio(_integraDBContext);
            _repPuestoTrabajoRemuneracion = new PuestoTrabajoRemuneracionRepositorio(_integraDBContext);
            _repPuestoTrabajoRemuneracionVariable = new PuestoTrabajoRemuneracionDetalleRepositorio(_integraDBContext);
            _repPais = new PaisRepositorio(_integraDBContext);
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los puestos de trabajo registrados en el sistema.
        /// </summary>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerRemuneracionPuestoTrabajoRegistrados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaContratos = _repPuestoTrabajoRemuneracion.ObtenerPuestoTrabajoRemuneracionRegistrado();
                return Ok(listaContratos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene detalle de los puestos de trabajo
        /// </summary>
		/// <param name="IdRemuneracionPuestoTrabajo">DTO enviado desde la interfaz<</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerRemuneracionPuestoTrabajoVariablePorPuesto([FromBody] int IdRemuneracionPuestoTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaRemuneracionPuestoTrabajoVariable = _repPuestoTrabajoRemuneracionVariable.ObtenerPuestoTrabajoRemuneracionVariableRegistrado(IdRemuneracionPuestoTrabajo);
                return Ok(listaRemuneracionPuestoTrabajoVariable);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta nueva remuneracion de puesto de trabajo
        /// </summary>
		/// <param name="PuestoTrabajoRemuneracion">DTO enviado desde la interfaz<</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarRemuneracionPuestoTrabajo([FromBody] PuestoTrabajoRemuneracionDTO PuestoTrabajoRemuneracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                foreach (var item in PuestoTrabajoRemuneracion.ListaPuestoTrabajoRemuneracionDetalle)
                {
                    if (item.IdRemuneracion== null || item.IdTipoRemuneracion==null || item.IdClaseRemuneracion==null || item.IdPeriodoRemuneracion==null || item.TieneCondicion==null || item.Tasa==null)
                    {
                        throw new Exception("Debe ingresar los campos requeridos en el detalle de remuneración");
                    }
                }
                using (TransactionScope scope = new TransactionScope())
                {              
                    PuestoTrabajoRemuneracionBO puestoTrabajoRemuneracion = new PuestoTrabajoRemuneracionBO(_integraDBContext)
                    {
                        IdPuestoTrabajo = PuestoTrabajoRemuneracion.IdPuestoTrabajo,
                        IdPersonalAreaTrabajo = PuestoTrabajoRemuneracion.IdPersonalAreaTrabajo,
                        IdPais= PuestoTrabajoRemuneracion.IdPais,
                        IdTableroComercialCategoriaAsesor= PuestoTrabajoRemuneracion.IdCategoria,
                        Estado = true,
                        UsuarioCreacion = PuestoTrabajoRemuneracion.Usuario,
                        UsuarioModificacion = PuestoTrabajoRemuneracion.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    bool res2 = puestoTrabajoRemuneracion.InsertarRegistro( PuestoTrabajoRemuneracion.ListaPuestoTrabajoRemuneracionDetalle);                    
                    scope.Complete();
                    return Ok(res2);
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la remuneracion de puesto de trabajo
        /// </summary>
		/// <param name="PuestoTrabajoRemuneracion">DTO enviado desde la interfaz<</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarRemuneracionPuestoTrabajo([FromBody] PuestoTrabajoRemuneracionDTO PuestoTrabajoRemuneracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                foreach (var item in PuestoTrabajoRemuneracion.ListaPuestoTrabajoRemuneracionDetalle)
                {
                    if (item.IdRemuneracion == null || item.IdTipoRemuneracion == null || item.IdClaseRemuneracion == null || item.IdPeriodoRemuneracion == null || item.TieneCondicion == null || item.Tasa == null)
                    {
                        throw new Exception("Debe ingresar los campos requeridos en el detalle de remuneración");
                    }
                }
                PuestoTrabajoRemuneracionBO puestoTrabajoRemuneracion;
                puestoTrabajoRemuneracion = _repPuestoTrabajoRemuneracion.FirstById(PuestoTrabajoRemuneracion.Id,_integraDBContext);
                puestoTrabajoRemuneracion.IdPuestoTrabajo = PuestoTrabajoRemuneracion.IdPuestoTrabajo;
                puestoTrabajoRemuneracion.IdPersonalAreaTrabajo = PuestoTrabajoRemuneracion.IdPersonalAreaTrabajo;
                puestoTrabajoRemuneracion.IdPais = PuestoTrabajoRemuneracion.IdPais;
                puestoTrabajoRemuneracion.IdTableroComercialCategoriaAsesor = PuestoTrabajoRemuneracion.IdCategoria;
                puestoTrabajoRemuneracion.UsuarioModificacion = PuestoTrabajoRemuneracion.Usuario;
                puestoTrabajoRemuneracion.FechaModificacion = DateTime.Now;
                var res= puestoTrabajoRemuneracion.ActualizarRegistro(PuestoTrabajoRemuneracion.ListaPuestoTrabajoRemuneracionDetalle);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina remuneracion de puesto de trabajo.
        /// </summary>
        /// <param name="PuestoTrabajoRemuneracion">DTO enviado desde la interfaz</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarRemuneracionPuestoTrabajo([FromBody] EliminarDTO PuestoTrabajoRemuneracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var puestoRemuneracionAnterior = _repPuestoTrabajoRemuneracionVariable.GetBy(x => x.IdPuestoTrabajoRemuneracion == PuestoTrabajoRemuneracion.Id).ToList();

                foreach (var item in puestoRemuneracionAnterior)
                {
                    _repPuestoTrabajoRemuneracionVariable.Delete(item.Id, PuestoTrabajoRemuneracion.NombreUsuario);
                }

                if (_repPuestoTrabajoRemuneracion.Exist(PuestoTrabajoRemuneracion.Id))
                {
                    var res = _repPuestoTrabajoRemuneracion.Delete(PuestoTrabajoRemuneracion.Id, PuestoTrabajoRemuneracion.NombreUsuario);
                    return Ok(res);
                }
                else
                {
                    return BadRequest("La categoria a eliminar no existe o ya fue eliminada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos para la interfaz compensacion por puesto
        /// </summary>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ListaPuestoTrabajo = _repPuestoTrabajo.ObtenerFiltroPuestoTrabajo();
                ListaPuestoTrabajo = ListaPuestoTrabajo.OrderBy(x => x.Text).ToList();
                var obj = new
                {
                    ListaAreaTrabajo = _repAreaTrabajo.ObtenerTodoFiltro(),
                    ListaPuestoTrabajo,
                    ListaPais = _repPais.ObtenerListaPais(),
                    ListaCategoria = _repPuestoTrabajoRemuneracion.ObtenerCategoria(),
                    ListaRemuneracion = _repPuestoTrabajoRemuneracionVariable.ObtenerRemuneracion(),
                    ListaTipoRemuneracion = _repPuestoTrabajoRemuneracionVariable.ObtenerTipoRemuneracion(),
                    ListaClaseRemuneracion = _repPuestoTrabajoRemuneracionVariable.ObtenerClaseRemuneracion(),
                    ListaPeriodoRemuneracion = _repPuestoTrabajoRemuneracionVariable.ObtenerPeriodoRemuneracion(),
                    ListaMoneda= _repPuestoTrabajoRemuneracionVariable.ObtenerMonedaParaTableroComercial(),
                    ListaDescripcionMonetaria= _repPuestoTrabajoRemuneracionVariable.ObtenerDescripcionMonetaria(),
                };
                return Ok(obj);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 07/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Importa el archivo Excel de la seccion Nuevo y modificar
        /// </summary>
		/// <param name="ArchivoExcel">IFormFile a importar de formato Excel</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ImportarExcel([FromForm] IFormFile ArchivoExcel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //int cantidadCorrecto = 0;
                //int cantidadIncorrecto = 0;

                using (var paquete = new ExcelPackage(ArchivoExcel.OpenReadStream()))
                {
                    var worksheet = paquete.Workbook.Worksheets[0];

                    var inicio = worksheet.Dimension.Start;
                    var final = worksheet.Dimension.End;
                    #region Inicializacion Valores
                    List<CampoObligatorioCeldaDTO> listaValoresExcel = new List<CampoObligatorioCeldaDTO>();
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Area", Columna = 0, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Puesto de Trabajo", Columna = 1, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Pais", Columna = 2, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Categoria", Columna = 3, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Tipo Remuneracion", Columna = 4, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Tipo", Columna = 5, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Clase", Columna = 6, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Periodo", Columna = 7, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Tasa", Columna = 8, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Monto", Columna = 9, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Moneda", Columna = 10, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Porcentaje Tasa", Columna = 11, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Descripcion Equipo", Columna = 12, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Condicion", Columna = 13, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Descripcion Monetaria", Columna = 14, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Valor Minimo", Columna = 15, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Valor Maximo", Columna = 16, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Moneda Rango", Columna = 17, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Ingreso Mensual", Columna = 18, FlagObligatorio = false });
                    #endregion

                    object[,] valoresExcel = worksheet.Cells.GetValue<object[,]>();
                    PuestoTrabajoRemuneracionBO puestoTrabajoRemuneracion = new PuestoTrabajoRemuneracionBO(_integraDBContext);
                    List<PuestoTrabajoRemuneracionDetalleDTO> ListaDetalle = new List<PuestoTrabajoRemuneracionDetalleDTO>();
                    for (int i = inicio.Row; i < 2; i++)
                    {
                        puestoTrabajoRemuneracion.IdPersonalAreaTrabajo = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Area").Columna]);
                        puestoTrabajoRemuneracion.IdPuestoTrabajo = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Puesto de Trabajo").Columna]);
                        puestoTrabajoRemuneracion.IdPais = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Pais").Columna]);
                        puestoTrabajoRemuneracion.IdTableroComercialCategoriaAsesor = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Categoria").Columna]);
                        puestoTrabajoRemuneracion.Estado = true;
                        puestoTrabajoRemuneracion.UsuarioCreacion = "Importacion Excel";
                        puestoTrabajoRemuneracion.UsuarioModificacion = "Importacion Excel";
                        puestoTrabajoRemuneracion.FechaCreacion = DateTime.Now;
                        puestoTrabajoRemuneracion.FechaModificacion = DateTime.Now;
                    }


                    for (int i = inicio.Row; i < final.Row; i++)
                    {

                        PuestoTrabajoRemuneracionDetalleDTO detalle = new PuestoTrabajoRemuneracionDetalleDTO();
                        detalle.Id = 0;
                        detalle.IdRemuneracion= Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Tipo Remuneracion").Columna]);
                        detalle.IdTipoRemuneracion= Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Tipo").Columna]);
                        detalle.IdClaseRemuneracion = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Clase").Columna]);
                        detalle.IdPeriodoRemuneracion = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Periodo").Columna]);
                        detalle.Tasa = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Tasa").Columna] ?? "false").ToString().ToLower().Trim() == "true" ? true : false;
                        detalle.Monto = Convert.ToDecimal(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Monto").Columna]);
                        detalle.IdMoneda = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Moneda").Columna]);
                        detalle.PorcentajeTasa = Convert.ToDecimal(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Porcentaje Tasa").Columna]);
                        detalle.DescripcionEquipo = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Descripcion Equipo").Columna] ?? string.Empty).ToString();
                        detalle.TieneCondicion = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Condicion").Columna] ?? "false").ToString().ToLower().Trim() == "true" ? true : false;
                        detalle.IdDescripcionMonetaria = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Descripcion Monetaria").Columna]);
                        detalle.ValorMinimo = Convert.ToDecimal(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Valor Minimo").Columna]);
                        detalle.ValorMaximo = Convert.ToDecimal(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Valor Maximo").Columna]);
                        detalle.IdMonedaValorVariable = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Moneda Rango").Columna]);
                        detalle.IngresoMensual = Convert.ToDecimal(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Ingreso Mensual").Columna]);
                        ListaDetalle.Add(detalle);
                    }
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //_repConfigurarVideoPrograma.Insert(ConfigurarVideoPrograma);
                        puestoTrabajoRemuneracion.ValidarExistente();
                        puestoTrabajoRemuneracion.InsertarRegistro(ListaDetalle);
                        scope.Complete();
                    }
                }
                var mensaje = "Se realizo la importacion correctamente";
                return Ok(mensaje);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
