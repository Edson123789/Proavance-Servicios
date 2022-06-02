using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ControlMensajesFiltradosController
    /// Autor: Edgar S.
    /// Fecha: 03/03/2021
    /// <summary>
    /// Gestión de Envío o Eliminación de Mensajes Ofensivos a asesores
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ControlMensajesFiltradosController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly ChatDetalleIntegraRepositorio _repChatDetalleIntegra;
        private readonly WhatsAppMensajeRecibidoRepositorio _repWhatsAppMensajeRecibido;
        private readonly DiccionarioPalabraOfensivaRepositorio _repDiccionarioPalabraOfensiva;
        public ControlMensajesFiltradosController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repChatDetalleIntegra = new ChatDetalleIntegraRepositorio(_integraDBContext);
            _repWhatsAppMensajeRecibido = new WhatsAppMensajeRecibidoRepositorio(_integraDBContext);
            _repDiccionarioPalabraOfensiva = new DiccionarioPalabraOfensivaRepositorio(_integraDBContext);
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 03/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros de Palabras ofensivas encontradas en chat de asesores
        /// </summary>
        /// <returns> Obtiene Lista de Palabras Ofensivas Registradas </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerMensajesFiltrados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaMensajesFiltrados = _repDiccionarioPalabraOfensiva.ObtenerPalabrasOfensivasFiltradas();
                return Ok(listaMensajesFiltrados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 03/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Envia lista de Mensajes a Asesores
        /// </summary>
        /// <returns> Obtiene Confirmación de envio: Bool </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult EnviarMensajes([FromBody] ListaMensajeChatDTO EnviarMensaje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //Portal Web
                    foreach (var item in EnviarMensaje.ListaMensajesPortal)
                    {
                        var chatDetalle = _repChatDetalleIntegra.FirstById(item);
                        chatDetalle.MensajeOfensivo = false;
                        chatDetalle.UsuarioModificacion = EnviarMensaje.Usuario;
                        chatDetalle.FechaModificacion = DateTime.Now;
                        _repChatDetalleIntegra.Update(chatDetalle);
                    }
                    //WhatsApp
                    foreach (var item in EnviarMensaje.ListaMensajesWhatsApp)
                    {
                        var chatWhatsAppDetalle = _repWhatsAppMensajeRecibido.FirstById(item);
                        chatWhatsAppDetalle.MensajeOfensivo = false;
                        chatWhatsAppDetalle.UsuarioModificacion = EnviarMensaje.Usuario;
                        chatWhatsAppDetalle.FechaModificacion = DateTime.Now;
                        _repWhatsAppMensajeRecibido.Update(chatWhatsAppDetalle);
                    }
                    scope.Complete();

                    List<IdAsesorChatDTO> listaIdAsesorGeneralPortal = new List<IdAsesorChatDTO>();
                    List<IdAsesorChatDTO> listaIdAsesorGeneralWhatsApp = new List<IdAsesorChatDTO>();
                    if (EnviarMensaje.ListaMensajesPortal.Count > 0)
                    {
                        List<IdAsesorChatDTO> listaIdAsesorPortal = new List<IdAsesorChatDTO>();
                        var listaIdChatPortal = "";
                        for (var i = 0; EnviarMensaje.ListaMensajesPortal.Count > i; i++)
                        {
                            if (i == 0) listaIdChatPortal = EnviarMensaje.ListaMensajesPortal[i] + "";
                            else listaIdChatPortal = listaIdChatPortal + "," + EnviarMensaje.ListaMensajesPortal[i];
                        }
                        listaIdAsesorPortal = _repDiccionarioPalabraOfensiva.ObtenerAsesorPorIdChatPortal(listaIdChatPortal);
                        if(listaIdAsesorPortal.Count > 0) listaIdAsesorGeneralPortal.AddRange(listaIdAsesorPortal);                       
                    }

                    if (EnviarMensaje.ListaMensajesWhatsApp.Count > 0)
                    {
                        List<IdAsesorChatDTO> listaIdAsesorWhatsApp = new List<IdAsesorChatDTO>();
                        var listaIdChatWhatsApp = "";
                        for (var i = 0; EnviarMensaje.ListaMensajesWhatsApp.Count > i; i++)
                        {
                            if (i == 0) listaIdChatWhatsApp = EnviarMensaje.ListaMensajesWhatsApp[i] + "";
                            else listaIdChatWhatsApp = listaIdChatWhatsApp + "," + EnviarMensaje.ListaMensajesWhatsApp[i];
                        }
                        listaIdAsesorWhatsApp = _repDiccionarioPalabraOfensiva.ObtenerAsesorPorIdChatWhatsApp(listaIdChatWhatsApp);
                        if (listaIdAsesorWhatsApp.Count > 0) listaIdAsesorGeneralWhatsApp.AddRange(listaIdAsesorWhatsApp);
                    }

                    List<int> listaIdAsesoresNotificarPortal = new List<int>();
                    List<int> listaIdAsesoresNotificarWhatsApp = new List<int>();
                    if (listaIdAsesorGeneralPortal.Count > 0) listaIdAsesoresNotificarPortal = listaIdAsesorGeneralPortal.Select(x => x.IdPersonal).Distinct().ToList();
                    if (listaIdAsesorGeneralPortal.Count > 0) listaIdAsesoresNotificarWhatsApp = listaIdAsesorGeneralWhatsApp.Select(x => x.IdPersonal).Distinct().ToList();

                    string cadenaIdAsesoresPortal = "";
                    if (listaIdAsesoresNotificarPortal.Count > 0) 
                    {
                        for (var i = 0; listaIdAsesoresNotificarPortal.Count > i; i++)
                        {
                            if (i == 0) cadenaIdAsesoresPortal = listaIdAsesoresNotificarPortal[i] + "";
                            else cadenaIdAsesoresPortal = cadenaIdAsesoresPortal + "," + listaIdAsesoresNotificarPortal[i];
                        }
                    }

                    string cadenaIdAsesoresWhatsApp = "";
                    if (listaIdAsesoresNotificarWhatsApp.Count > 0)
                    {
                        for (var i = 0; listaIdAsesoresNotificarWhatsApp.Count > i; i++)
                        {
                            if (i == 0) cadenaIdAsesoresWhatsApp = listaIdAsesoresNotificarWhatsApp[i] + "";
                            else cadenaIdAsesoresWhatsApp = cadenaIdAsesoresWhatsApp + "," + listaIdAsesoresNotificarWhatsApp[i];
                        }
                    }

                    if (cadenaIdAsesoresPortal == "" && cadenaIdAsesoresWhatsApp == "")
                    {
                        return Ok(new { Estado = false, ListaAsesoresPortal = cadenaIdAsesoresPortal, ListaAsesoresWhatsApp = cadenaIdAsesoresWhatsApp });
                    }
                    else
                    {
                        return Ok(new { Estado = true, ListaAsesoresPortal = cadenaIdAsesoresPortal, ListaAsesoresWhatsApp = cadenaIdAsesoresWhatsApp });
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 03/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina Lista de Mensajes a Asesores
        /// </summary>
        /// <returns> Obtiene Confirmación de envio: Bool </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult EliminarMensajes([FromBody] ListaMensajeChatDTO EnviarMensaje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //Portal Web
                    foreach (var item in EnviarMensaje.ListaMensajesPortal)
                    {
                        var chatDetalle = _repChatDetalleIntegra.FirstById(item);
                        if(chatDetalle != null)
                        {
                            _repChatDetalleIntegra.Delete(chatDetalle.Id, EnviarMensaje.Usuario);
                        }
                    }

                    //WhatsApp
                    foreach (var item in EnviarMensaje.ListaMensajesWhatsApp)
                    {
                        var chatWhatsApp = _repWhatsAppMensajeRecibido.FirstById(item);
                        if (chatWhatsApp != null)
                        {
                            _repWhatsAppMensajeRecibido.Delete(chatWhatsApp.Id, EnviarMensaje.Usuario);
                        }
                    }

                    scope.Complete();
                    return Ok(true);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
