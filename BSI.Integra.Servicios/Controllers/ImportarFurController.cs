using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using Google.Api.Ads.Common.Util;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.IO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.Transactions;
using BSI.Integra.Aplicacion.Finanzas.BO;
using System.Text;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ImportarFur")]
    public class ImportarFurController : Controller
    {
        [Route("[action]")]
        [HttpPost]
        public ActionResult MostrarFurs([FromForm] IFormFile files)
        {
            var ListaFurDTO = new List<FurDTO>();
            CsvFile file = new CsvFile();
            try
            {
                integraDBContext integraDB = new integraDBContext();
                CentroCostoRepositorio repCentroCostoRep = new CentroCostoRepositorio(integraDB);
                FurRepositorio repFurRep = new FurRepositorio(integraDB);
                int index = 0;

                using (var cvs = new CsvReader(new StreamReader(files.OpenReadStream())))
                {
                    cvs.Configuration.Delimiter = ";";
                    cvs.Configuration.MissingFieldFound = null;
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        index++;

                        FurDTO fur = new FurDTO();
                        fur.IdPersonalAreaTrabajo = cvs.GetField<int?>("IdPersonalAreaTrabajo");
                        fur.IdCiudad = cvs.GetField<int>("IdCiudad");
                        fur.IdProveedor = cvs.GetField<int?>("IdProveedor");
                        fur.IdProducto = cvs.GetField<int?>("IdProducto");
                        var historicoDatos = repFurRep.ObtenerHistoricoUltimaVersion(fur.IdProveedor.Value,fur.IdProducto.Value);
                        fur.RazonSocial = historicoDatos.Proveedor==null?"": historicoDatos.Proveedor;
                        fur.Producto = historicoDatos.Producto;
                        fur.Cantidad = cvs.GetField<decimal>("Cantidad");
                        fur.IdProductoPresentacion = historicoDatos.IdCantidad;
                        fur.ProductoPresentacion = historicoDatos.Cantidad;
                        fur.IdCentroCosto = cvs.GetField<int?>("IdCentroCosto");
                        fur.CentroCosto = repCentroCostoRep.FirstById(fur.IdCentroCosto.Value).Nombre;
                        fur.IdMoneda_Proveedor = historicoDatos.IdMoneda;
                        fur.NumeroCuenta = historicoDatos.CuentaContable;
                        fur.Cuenta = historicoDatos.Cuenta;
                        fur.PrecioUnitarioMonedaOrigen = historicoDatos.CostoOriginal;
                        fur.PrecioUnitarioDolares = historicoDatos.CostoDolares;
                        fur.FechaLimite = cvs.GetField<string>("FechaLimite");
                        fur.NumeroSemana = cvs.GetField<int?>("NumeroSemana");
                        ListaFurDTO.Add(fur);
                    }
                }
                var Nregistros = index;
                return Ok(new { ListaFurDTO, Nregistros });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarFurPorImportacion([FromBody] ListaFurDTO Json)
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
                    FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                    foreach (var fur in Json.ListaFur)
                    {
                        repFurRep.InsertarFurImportado(fur, _integraDBContext);
                    }
                    scope.Complete();
                }
                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}