using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using System.Transactions;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Finanzas.BO;
using System.Net;
using System.IO;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ProveedorController
    /// Autor: ---, Jashin Salazar
    /// Fecha: 01/07/2021
    /// <summary>
    /// Maestro Proveedores
    /// </summary>
    [Route("api/MaestroProveedor")]
    public class ProveedorController : Controller
    {
        /// Tipo Función: POST
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene nombre del proveedor.
        /// </summary>
        /// <param name="Filtros">Filtros del formulario</param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerNombreProveedor([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                ProveedorRepositorio repProveedorRep = new ProveedorRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repProveedorRep.GetBy(x => x.Estado == true && (x.RazonSocial.Contains(Filtros["Valor"].ToString()) || (x.ApePaterno + " " + x.ApeMaterno + " " + x.Nombre1 + " " + x.Nombre2).Contains(Filtros["Valor"].ToString())), x => new { Id = x.Id, Nombre = "(" + x.NroDocIdentidad + ") " + (x.RazonSocial ?? "") + " " + (x.ApePaterno ?? "") + " " + (x.ApeMaterno ?? "") + ", " + (x.Nombre1 ?? "") + " " + (x.Nombre2 ?? "") }).ToList());
                }
                return Ok(repProveedorRep.GetBy(x => x.Estado, x => new { Id = x.Id, Nombre = "(" + x.NroDocIdentidad + ") " + (x.RazonSocial ?? "") + " " + (x.ApePaterno ?? "") + " " + (x.ApeMaterno ?? "") + ", " + (x.Nombre1 ?? "") + " " + (x.Nombre2 ?? "") }).ToList());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene nombre del proveedor para comboBox.
        /// </summary>
        /// <param name="Filtros">Filtros del formulario</param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerNombreProveedorAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                ProveedorRepositorio repProveedorRep = new ProveedorRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repProveedorRep.ObtenerNombreProveedorAutocomplete(Filtros["Valor"].ToString()));
                }
                return Ok(repProveedorRep.ObtenerNombreProveedorAutocomplete(""));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene ruc y nombre del proveedor para comboBox.
        /// </summary>
        /// <param name="Filtros">Filtros del formulario</param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerRucNombreProveedorAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                ProveedorRepositorio repProveedorRep = new ProveedorRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repProveedorRep.ObtenerProveedorRucAutocomplete(Filtros["Valor"].ToString()));
                }
                return Ok(repProveedorRep.ObtenerNombreProveedorAutocomplete(""));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene ruc y nombre del proveedor para comboBox v2.
        /// </summary>
        /// <param name="Filtros">Filtros del formulario</param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerRucNombreProveedorAutocomplete2([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                ProveedorRepositorio repProveedorRep = new ProveedorRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repProveedorRep.ObtenerProveedorPorRuc(Filtros["Valor"].ToString()));

                    
                }
                return Ok(repProveedorRep.ObtenerNombreProveedorAutocomplete(""));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene tipo de contribuyente.
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoContribuyente()
        {
            try
            {
                TipoContribuyenteRepositorio repTipoContribuyenteRep = new TipoContribuyenteRepositorio();
                return Ok(repTipoContribuyenteRep.ObtenerTipoContribuyente());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene documento de identidad.
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerDocumentoIdentidad()
        {
            try
            {
                DocumentoIdentidadRepositorio repDocumentoIdentidadRep = new DocumentoIdentidadRepositorio();
                return Ok(repDocumentoIdentidadRep.ObtenerDocumentoIdentidad());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene tipo cuenta banco.
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoCuentaBanco()
        {
            try
            {
                TipoCuentaBancoRepositorio repTipoCuentaBancoRep = new TipoCuentaBancoRepositorio();
                return Ok(repTipoCuentaBancoRep.ObtenerTipoCuentaBanco());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: ---, Jashin Salazart
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene proveedores para la grilla.
        /// </summary>
        /// <param name="IdProveedor">Id del proveedor</param>
        /// <returns>List<ProveedorDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerProveedorGrilla(int? IdProveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProveedorRepositorio repProveedorRep = new ProveedorRepositorio();
                var _repProveedorTipoServicio = new ProveedorTipoServicioRepositorio();
                var listadoProveedor = repProveedorRep.ObtenerTodoProveedorById(IdProveedor);
                var listaProveedorTipoServicio = _repProveedorTipoServicio.ObtenerPorProveedor(listadoProveedor.Select(x => x.Id).ToList());
                foreach (var item in listadoProveedor)
                {
                    item.ListaProveedorTipoServicio = listaProveedorTipoServicio.Where(x => x.IdProveedor == item.Id).ToList();
                }
                return Ok(listadoProveedor);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina proveedor.
        /// </summary>
        /// <param name="eliminar">Filtros del formulario</param>
        /// <returns>Boolean</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarProveedor([FromBody] EliminarDTO eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();
                using (TransactionScope scope = new TransactionScope())
                {
                    ProveedorRepositorio repProveedorRep = new ProveedorRepositorio(_integraDBContext);
                    var _repProveedorTipoServicio = new ProveedorTipoServicioRepositorio(_integraDBContext);
                    if (repProveedorRep.Exist(eliminar.Id))
                    {
                        repProveedorRep.Delete(eliminar.Id, eliminar.NombreUsuario);
                        _repProveedorTipoServicio.Delete(_repProveedorTipoServicio.GetBy(x => x.IdProveedor == eliminar.Id).Select(x => x.Id), eliminar.NombreUsuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene proveedores por subcriterio de calificacion.
        /// </summary>
        /// <returns>Object</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerProveedorSubCriterioCalificacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProveedorSubCriterioCalificacionRepositorio repSubCriterio = new ProveedorSubCriterioCalificacionRepositorio();

                var listadoProveedorSubCriterio = repSubCriterio.ObtenerSubCriterioCalificacion();

                return Ok(listadoProveedorSubCriterio);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta cuenta de banco a un proveedor.
        /// </summary>
        /// <param name="proveedorCuenta">Cuenta del proveedor</param>
        /// <returns>Boolean</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarProveedorCuentaBanco([FromBody] ProveedorCuentasDTO proveedorCuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    integraDBContext integraDBContext = new integraDBContext();
                    ProveedorBO proveedor = new ProveedorBO(integraDBContext);
                    proveedor.InsertarProveedorCuentaBanco(proveedorCuenta.proveedor, proveedorCuenta.listaCuentaBanco);
                    scope.Complete();
                }

                return Ok(true);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza cuenta de banco a un proveedor.
        /// </summary>
        /// <param name="proveedorCuenta">Cuenta del proveedor</param>
        /// <returns>Boolean</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarProveedorCuentaBanco([FromBody] ProveedorCuentasDTO proveedorCuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    integraDBContext integraDBContext = new integraDBContext();
                    ProveedorBO proveedor = new ProveedorBO(integraDBContext);
                    proveedor.ActualizarProveedorCuentaBanco(proveedorCuenta.proveedor, proveedorCuenta.listaCuentaBanco);
                    scope.Complete();
                }

                return Ok(true);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene cuenta de banco de un proveedor.
        /// </summary>
        /// <param name="IdProveedor">Id del proveedor</param>
        /// <returns>List<ProveedorCuentaBancoDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCuentaBancoProveedor(int IdProveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProveedorCuentaBancoRepositorio repCuentaBancoRep = new ProveedorCuentaBancoRepositorio();
                return Ok(repCuentaBancoRep.ObtenerCuentasProveedorById(IdProveedor));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina cuenta de banco de un proveedor.
        /// </summary>
        /// <param name="eliminar">Filtro del formulario</param>
        /// <returns>Boolean</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarProveedorCuentaBanco([FromBody] EliminarDTO eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();
                using (TransactionScope scope = new TransactionScope())
                {
                    ProveedorCuentaBancoRepositorio repCuentaBancoProvRep = new ProveedorCuentaBancoRepositorio(_integraDBContext);
                    if (repCuentaBancoProvRep.Exist(eliminar.Id))
                    {
                        repCuentaBancoProvRep.Delete(eliminar.Id, eliminar.NombreUsuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registro de prestaciones.
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPrestacionRegistro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PrestacionRegistroRepositorio repPrestacionRegistroRep = new PrestacionRegistroRepositorio();
                return Ok(repPrestacionRegistroRep.obtenerPrestacionRegistro());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta criterio de calificacion de proveedor.
        /// </summary>
        /// <param name="Calificacion">Filtro del formulario</param>
        /// <returns>Boolean</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarCriterioCalificacionAProveedor([FromBody] ProveedorCalificacionDTO Calificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {
                    integraDBContext integraDBContext = new integraDBContext();
                    ProveedorCalificacionBO proveedorCalificacion = new ProveedorCalificacionBO(integraDBContext);
                    proveedorCalificacion.InsertarCriterioCalificacion(Calificacion);
                    scope.Complete();
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida existencia de un proveedor.
        /// </summary>
        /// <param name="DocidentidadEmail">Documento de identidad y Email</param>
        /// <returns>Boolean</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ValidarExistenciaProveedor([FromBody] CadenaStringDTO DocidentidadEmail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProveedorRepositorio repProveedorRep = new ProveedorRepositorio();
                var proveedor = repProveedorRep.GetBy(x =>(x.NroDocIdentidad == DocidentidadEmail.Cadena1) || (x.Email == DocidentidadEmail.Cadena2), x => new { Id = x.Id }).ToList();
                if (proveedor.Count > 0)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene impuesto de un proveedor.
        /// </summary>
        /// <returns>List<></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerImpuestosProveedor()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoImpuestoRepositorio repImpuestoRep = new TipoImpuestoRepositorio();
                RetencionRepositorio repRetencionRep = new RetencionRepositorio();
                DetraccionRepositorio repDetraccionRep = new DetraccionRepositorio();

                var impuesto= repImpuestoRep.GetBy(x => x.Estado==true, x => new { Id = x.Id ,Nombre=x.Valor+"% - "+x.Nombre,IdPais=x.IdPais}).ToList();
                var retencion = repRetencionRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Valor+"% - " + x.Nombre, IdPais = x.IdPais }).ToList();
                var detraccion = repDetraccionRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Valor+ "% - " + x.Nombre, IdPais = x.IdPais }).ToList();

                    return Ok(new { impuesto, retencion, detraccion});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: ---, Jashin Salazar
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene nombre de un proveedor para honorario.
        /// </summary>
        /// <returns>List<ProveedorHonorarioComboDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerNombreProveedorParaHonorario()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProveedorRepositorio repProveedorRep = new ProveedorRepositorio();
                List<ProveedorHonorarioComboDTO> listado = repProveedorRep.ObtenerNombreProveedorParaHonorario();
                return Ok(listado);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        //[Route("[action]")]
        //[HttpPost]
        //public ActionResult ConsultaRucSunat([FromBody] CadenaStringDTO Ruc)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        integraDBContext integraDBContext = new integraDBContext();
        //        ProveedorBO proveedor = new ProveedorBO(integraDBContext);
        //        string captcha = proveedor.genCaptcha();
        //        ConsultaRucDTO OBJ = new ConsultaRucDTO();
        //        string myurl = @"http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" + Ruc.Cadena.Trim() + "&codigo=" + captcha.Trim().ToUpper() + "&tipdoc=1";
        //        HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myurl);
        //        myWebRequest.CookieContainer = proveedor.cokkie;
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        //        HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
        //        Console.Write(myhttpWebResponse);
        //        Stream myStream = myhttpWebResponse.GetResponseStream();
        //        StreamReader myStreamReader = new StreamReader(myStream);
        //        Console.Write(myStreamReader);
        //        string xDat = ""; int pos = 0;
        //        string cadena = "";
        //        while (!myStreamReader.EndOfStream)
        //        {
        //            OBJ.RUC = Ruc.Cadena;
        //            xDat = myStreamReader.ReadLine();
        //            if (xDat.Contains("http://www.sunat.gob.pe/inicio.htm"))
        //                xDat = "";

        //            cadena += xDat;
        //            pos++;
        //            Console.WriteLine(xDat);
        //            switch (pos)
        //            {
        //                case 156:
        //                    OBJ.TipoContribuyente = proveedor.getDatafromXML(xDat, 25);
        //                    break;
        //                case 151:
        //                    OBJ.Nombre = proveedor.getDatafromXML(xDat, 25);
        //                    break;
        //                case 166:
        //                    OBJ.FechaInscripcion = proveedor.getDatafromXML(xDat, 25);
        //                    break;
        //                case 168:
        //                    OBJ.FechaInicio = proveedor.getDatafromXML(xDat, 25);
        //                    break;
        //                case 172:
        //                    OBJ.Estado = proveedor.getDatafromXML(xDat, 25);
        //                    break;
        //                case 182:
        //                    OBJ.Condicion = proveedor.getDatafromXML(xDat, 0);
        //                    break;
        //                case 191:
        //                    OBJ.Domicilio = proveedor.getDatafromXML(xDat, 25);
        //                    break;
        //                case 198:
        //                    OBJ.Actividad = proveedor.getDatafromXML(xDat, 25);
        //                    break;
        //            }

        //        }

        //        return Ok(OBJ);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}