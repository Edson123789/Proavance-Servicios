using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Marketing/AsesorChatController
    /// Autor: Wilber Choque - Joao Benavente - Gian Miranda
    /// Fecha: 15/04/2021
    /// <summary>
    /// Configura los parametros para la asignacion de los chats de los programas generales a asesores
    /// </summary>
    /// 
    [Route("api/AsesorChat")]
    public class AsesorChatController : Controller
    {
        /// Tipo Función: GET
        /// Autor: Wilber Choque - Joao Benavente - Gian Miranda
        /// Fecha: 21/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la informacion en una lista de objeto de clase AsesorChatConsolidadoVisualizarDTO para los combos necesarios
        /// </summary>
        /// <returns>Lista de AsesorChatConsolidadoVisualizarDTO en formato JSON</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                AsesorChatRepositorio _repAsesorChat = new AsesorChatRepositorio();
                var listadoAsesorChatDetalle = _repAsesorChat.ObtenerTodoFiltro();

                var listadoAsesorChatDetalleAgrupado = (
                       from p in listadoAsesorChatDetalle
                       group p by new { p.Id , p.IdPersonal, p.NombrePersonal } into g
                       select new AsesorChatConsolidadoDTO()
                       {
                           Id = g.Key.Id,
                           IdPersonal = g.Key.IdPersonal,
                           NombrePersonal = g.Key.NombrePersonal,
                           ListaArea = g.Select(x => new AsesorChatDetalleDTO { Id = x.IdArea, Nombre = x.NombreArea }).ToList(),
                           ListaProgramaGeneral = g.Select(x => new AsesorChatDetalleDTO { Id = x.IdPGeneral, Nombre = x.NombrePGeneral }).ToList(),
                           ListaPais = g.Select(x => new AsesorChatDetalleDTO { Id = x.IdPais , Nombre = x.NombrePais }).ToList(),
                           ListaSubArea = g.Select(x => new AsesorChatDetalleDTO { Id = x.IdSubArea , Nombre = x.NombreSubArea }).ToList()
                       }
                ).ToList();

                List<AsesorChatConsolidadoVisualizarDTO> listadoAsesorChatDetalleVisualizar = new List<AsesorChatConsolidadoVisualizarDTO>();

                foreach (var item in listadoAsesorChatDetalleAgrupado)
                {
                    //area
                    string listadoArea = "<ul>";
                    foreach (var _area in item.ListaArea.Select(x => x.Nombre).Distinct().ToList())
                    {
                        listadoArea += "<li>" + _area + "</li>";
                    }
                    listadoArea += "</ul>";

                    //subarea
                    var listadoSubArea = "<ul>";
                    foreach (var _SubArea in item.ListaSubArea.Select(x => x.Nombre).Distinct().ToList())
                    {
                        listadoSubArea += "<li>" + _SubArea + "</li>";
                    }
                    listadoSubArea += "</ul>";

                    //pgeneral
                    var listadoPGeneral = "<ul>";
                    foreach (var _pGeneral in item.ListaProgramaGeneral.Select(x => x.Nombre).Distinct().ToList())
                    {
                        listadoPGeneral += "<li>" + _pGeneral + "</li>";
                    }
                    listadoPGeneral += "</ul>";

                    //pais
                    var listadoPais = "<ul>";
                    foreach (var _pais in item.ListaPais.Select(x => x.Nombre).Distinct().ToList())
                    {
                        listadoPais += "<li>" + _pais + "</li>";
                    }
                    listadoPais += "</ul>";

                    var asesorChatDetalleTemp = new AsesorChatConsolidadoVisualizarDTO()
                    {
                        Id = item.Id,
                        IdPersonal = item.IdPersonal,
                        NombrePersonal = item.NombrePersonal,
                        ListaArea = listadoArea,
                        ListaSubArea = listadoSubArea,
                        ListaProgramaGeneral = listadoPGeneral,
                        ListaPais = listadoPais
                    };
                    listadoAsesorChatDetalleVisualizar.Add(asesorChatDetalleTemp);
                }
                return Ok(listadoAsesorChatDetalleVisualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerChatAsignadosNoAsignados()
        {
            try
            {
                AsesorChatRepositorio _repAsesorChat = new AsesorChatRepositorio();
                return Ok(_repAsesorChat.ObtenerChatAsignadosNoAsignados());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Wilber Choque - Gian Miranda
        /// Fecha: 15/04/2021
        /// Versión: 1
        /// <summary>
        /// Elimina un AsesorChat mediante un objeto de clase EliminarDTO
        /// </summary>
        /// <param name="DTO">Objeto de clase AsesorEliminado</param>
        /// <returns>Response 200 con booleano true o caso contrario, response 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();

                AsesorChatRepositorio _repAsesorChat = new AsesorChatRepositorio(_integraDBContext);
                AsesorChatDetalleRepositorio _repAsesorChatDetalle = new AsesorChatDetalleRepositorio(_integraDBContext);

                if (_repAsesorChat.Exist(DTO.Id))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        _repAsesorChatDetalle.EliminarAsesorChatDetalle(DTO.Id, DTO.NombreUsuario);
                        _repAsesorChat.Delete(DTO.Id, DTO.NombreUsuario);
                        scope.Complete();
                    }
                    return Ok(true);
                }
                else
                {
                    return BadRequest("No existe el asesor chat!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //ObtenerListaProgramasAsignadosPorAsesor
    }
}