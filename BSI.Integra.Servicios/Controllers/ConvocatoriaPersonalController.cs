using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ConvocatoriaPersonalController
    /// Autor: Luis H - Britsel C - Edgar S.
    /// Fecha: 24/05/2021
    /// <summary>
    /// Gestiona todas la propiedades de la tabla T_ConvocatoriaPersonal
    /// </summary>
    [Route("api/ConvocatoriaPersonal")]
    public class ConvocatoriaPersonalController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly ConvocatoriaPersonalRepositorio _repConvocatoriaPersonal;
        private readonly ProveedorRepositorio _repProovedor;
        private readonly ProcesoSeleccionRepositorio _repProcesoSeleccion;
        private readonly PersonalRepositorio _repPersonal;
        private readonly SedeTrabajoRepositorio _repSedeTrabajo;
        private readonly PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo;

        public ConvocatoriaPersonalController()
        {
            _integraDBContext = new integraDBContext();
            _repConvocatoriaPersonal = new ConvocatoriaPersonalRepositorio(_integraDBContext);
            _repProovedor = new ProveedorRepositorio(_integraDBContext);
            _repProcesoSeleccion = new ProcesoSeleccionRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repSedeTrabajo = new SedeTrabajoRepositorio(_integraDBContext);
            _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
        }

        /// TipoFuncion: POST
        /// Autor: Luis H - Britsel C.
        /// Fecha: 24/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene convocatorias registradas
        /// </summary>
        /// <returns> List<ConvocatoriaPersonalDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerConvocatoriasRegistradas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaConvocatoriasRegistradas = _repConvocatoriaPersonal.ObtenerConvocatoriasRegistradas();
                return Ok(listaConvocatoriasRegistradas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }        
        /// TipoFuncion: POST
        /// Autor: Luis H - Britsel C.
        /// Fecha: 24/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina convocatoria registrada
        /// </summary>
        /// <param name="ConvocatoriaPersonal"> Id de convocatoria y usuario </param>
        /// <returns> Retorna StatusCodes, 200 si se eliminó existasamente con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarConvocatoria([FromBody] EliminarDTO ConvocatoriaPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repConvocatoriaPersonal.Exist(ConvocatoriaPersonal.Id))
                {
                    var res = _repConvocatoriaPersonal.Delete(ConvocatoriaPersonal.Id, ConvocatoriaPersonal.NombreUsuario);
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
        /// TipoFuncion: POST
        /// Autor: Luis H - Britsel C.
        /// Fecha: 24/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos para interfaz
        /// </summary>
        /// <returns> List<FiltroConvocatoriaPersonalDTO> </returns>
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
                var combos = new
                {
                    ListaProovedor = _repProovedor.ObtenerProveedoresConvocatoriaPersonal().OrderBy(x => x.RazonSocial).ToList(),
                    ListaSede = _repSedeTrabajo.GetBy(x => x.Estado == true).Select(x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre).ToList(),
                    ListaArea = _repPersonalAreaTrabajo.GetBy(x => x.Estado == true).Select(x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre).ToList(),
                    ListaPersonalEncargado = _repPersonal.ObtenerComboPersonalGestionPersonas().OrderBy(x => x.NombreCompleto),
                };
                return Ok(combos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Luis H - Britsel C.
        /// Fecha: 24/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene procesos de selección para convocatoria
        /// </summary>
        /// <returns> Objeto Agrupado </returns>
        [HttpPost]
		[Route("[action]")]
		public ActionResult ObtenerProcesoSeleccionConvocatoria()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				int? auxiiar = null;
				var listaProceso = _repProcesoSeleccion.ObtenerProcesoSeleccionConvocatoria();
				var agrupado = listaProceso.GroupBy(x => new { x.Id, x.Nombre, x.Codigo }).Select(x => new ProcesoSeleccionCompuestoDTO
				{
					Id = x.Key.Id,
					Nombre = x.Key.Nombre,
					Codigo = x.Key.Codigo,
					DetalleConvocatoria = x.GroupBy(y => new { y.IdConvocatoriaPersonal, y.CodigoConvocatoriaPersonal }).Select(y => new ConvocatoriaPersonalDetalleDTO
					{
						IdConvocatoriaPersonal = y.Key.IdConvocatoriaPersonal,
						CodigoConvocatoriaPersonal = y.Key.CodigoConvocatoriaPersonal,
						UltimaSecuencia = y.Key.CodigoConvocatoriaPersonal != null ? (Convert.ToInt32(Regex.Match(y.Key.CodigoConvocatoriaPersonal, @"-\d+").Value) * -1) : auxiiar
					}).OrderByDescending(y => y.UltimaSecuencia).ToList()
				}).ToList();
				return Ok(agrupado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 24/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Recibe valor codificado de configuración de convocatoria e Inserta nuevo registro
        /// </summary>
        /// <param name="ConvocatoriaPersonalCodificado"> Información de configuración de Convocatoria Codificado </param>
        /// <returns> Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación de inserción </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarConvocatoria([FromBody] ConvocatoriaPersonalDTO ConvocatoriaPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConvocatoriaPersonalBO ConvocatoriaPersonalDTO = new ConvocatoriaPersonalBO()
                {


                    Nombre = ConvocatoriaPersonal.Nombre,
                    Codigo = ConvocatoriaPersonal.Codigo,
                    IdProcesoSeleccion = ConvocatoriaPersonal.IdProcesoSeleccion,
                    IdProveedor = ConvocatoriaPersonal.IdProveedor,
                    FechaInicio = ConvocatoriaPersonal.FechaInicio,
                    FechaFin = ConvocatoriaPersonal.FechaFin,
                    CuerpoConvocatoria = ConvocatoriaPersonal.CuerpoConvocatoria,
                    Estado = true,
                    UsuarioCreacion = ConvocatoriaPersonal.Usuario,
                    UsuarioModificacion = ConvocatoriaPersonal.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    IdSedeTrabajo = ConvocatoriaPersonal.IdSedeTrabajo,
                    IdArea = ConvocatoriaPersonal.IdArea,
                    UrlAviso = ConvocatoriaPersonal.UrlAviso,
                    IdPersonal = ConvocatoriaPersonal.IdPersonal

                };
                //var res = _repConvocatoriaPersonal.Insert(ConvocatoriaPersonalDTO);
                //return Ok(res);

                var res = _repConvocatoriaPersonal.Insert(ConvocatoriaPersonalDTO);
                string respuesta = "Los datos se Guardaron Correctamente";
                return Ok(new { Respuesta = true, Mensaje = respuesta });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 24/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Recibe valor codificado de configuración de convocatoria y Actualiza el registro
        /// </summary>
        /// <param name="ConvocatoriaPersonalCodificado"> Información de configuración de Convocatoria Codificado </param>
        /// <returns> Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarConvocatoria([FromBody] ConvocatoriaPersonalDTO convocatoriaPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               
                {
                    var nuevaFechaInicio = convocatoriaPersonal.FechaInicio.Date.Add(new TimeSpan(0, 0, 0));
                    var nuevaFechaFin = convocatoriaPersonal.FechaFin.Date.Add(new TimeSpan(23, 23, 59));
                    convocatoriaPersonal.FechaInicio = nuevaFechaInicio;
                    convocatoriaPersonal.FechaFin = nuevaFechaFin;

                    var convocatoriaPersonalActualizar = _repConvocatoriaPersonal.FirstById(convocatoriaPersonal.Id);
                    convocatoriaPersonalActualizar.Nombre = convocatoriaPersonal.Nombre;
                    convocatoriaPersonalActualizar.Codigo = convocatoriaPersonal.Codigo;
                    convocatoriaPersonalActualizar.IdProcesoSeleccion = convocatoriaPersonal.IdProcesoSeleccion;
                    convocatoriaPersonalActualizar.IdProveedor = convocatoriaPersonal.IdProveedor;
                    convocatoriaPersonalActualizar.FechaInicio = convocatoriaPersonal.FechaInicio;
                    convocatoriaPersonalActualizar.FechaFin = convocatoriaPersonal.FechaFin;
                    convocatoriaPersonalActualizar.CuerpoConvocatoria = convocatoriaPersonal.CuerpoConvocatoria;
                    convocatoriaPersonalActualizar.IdSedeTrabajo = convocatoriaPersonal.IdSedeTrabajo;
                    convocatoriaPersonalActualizar.IdArea = convocatoriaPersonal.IdArea;
                    convocatoriaPersonalActualizar.UrlAviso = convocatoriaPersonal.UrlAviso;
                    convocatoriaPersonalActualizar.IdPersonal = convocatoriaPersonal.IdPersonal;
                    convocatoriaPersonalActualizar.UsuarioModificacion = convocatoriaPersonal.Usuario;
                    convocatoriaPersonalActualizar.FechaModificacion = DateTime.Now;

                    var res = _repConvocatoriaPersonal.Update(convocatoriaPersonalActualizar);
                    string respuesta = "Los datos se Actualizaron Correctamente";
                    return Ok(new { Respuesta = true, Mensaje = respuesta });
                }
         
            }
            catch (Exception e)
            {
                return BadRequest(new { Respuesta = false, Mensaje = e.Message });
            }
        }
    }
}