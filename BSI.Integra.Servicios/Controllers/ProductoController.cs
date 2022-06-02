using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaestroProducto")]
    public class ProductoController : Controller
    {

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerNombreProducto([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                ProductoRepositorio repProductoRep = new ProductoRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repProductoRep.GetBy(x => x.Nombre.Contains(Filtros["Valor"].ToString()) && x.Estado==true, x => new { Id = x.Id, Nombre = x.Nombre.ToUpper() }).ToList());
                }
                return Ok(repProductoRep.GetBy(x => x.Estado, x => new { Id = x.Id, Nombre = x.Nombre.ToUpper() }).ToList());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPresentacionProducto()
        {
            try
            {
                ProductoPresentacionRepositorio repProductoPresentacionRep = new ProductoPresentacionRepositorio();
                return Ok(repProductoPresentacionRep.GetBy(x => x.Estado==true, x => new { Id = x.Id, Nombre = x.Nombre.ToUpper() }).ToList());
                
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]/{IdProducto}")]
        [HttpGet]
        public ActionResult ObtenerProductoCuentaContable(int IdProducto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductoRepositorio _repProductoRep = new ProductoRepositorio();

                return Ok(_repProductoRep.ObtenerProductoCuentaContable(IdProducto).FirstOrDefault());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarProducto([FromBody] ProductoDTO objetoProducto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string result = "SUCCESS";
                integraDBContext context = new integraDBContext();
                using (TransactionScope scope = new TransactionScope())
                {
                    ProductoRepositorio _repProductoRep = new ProductoRepositorio();
                    objetoProducto = _repProductoRep.InsertarProducto(objetoProducto, context);
                    scope.Complete();
                }
                if (objetoProducto.Id == 0)
                {
                    result = "ERROR NO INSERTO";
                }
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarProducto([FromBody] ProductoDTO objetoProducto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contextDB = new integraDBContext();
                using (TransactionScope scope = new TransactionScope())
                {
                    ProductoRepositorio _repProductoRep = new ProductoRepositorio(contextDB);
                    _repProductoRep.ActualizarProducto(objetoProducto, contextDB);
                    scope.Complete();
                }

                string result = "SUCCESS";
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}