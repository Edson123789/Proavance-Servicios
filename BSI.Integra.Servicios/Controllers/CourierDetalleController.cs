using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CourierDetalleController
    /// Autor: Max Mantilla
    /// Fecha: 25/11/2021
    /// <summary>
    /// Mantenimiento de la tabla pla.T_CourierDetalle
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class CourierDetalleController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public CourierDetalleController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// TipoFuncion: GET
        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene detalle de los couriers
        /// </summary>
        /// <returns>List<CourierDetalleDTO><returns>
        [Route("[Action]/{IdCourier}")]
        [HttpGet]
        public ActionResult ObtenerCourierDetalle(int IdCourier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CourierDetalleRepositorio _repCourierDetalleRepositorioRepositorio = new CourierDetalleRepositorio(_integraDBContext);
                return Ok(_repCourierDetalleRepositorioRepositorio.ObtenerCourierDetalle(IdCourier));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene valores de detalle de couriers para un combo
        /// </summary>
        /// <returns>List<FiltroDTO><returns>
        [Route("[Action]/{IdCourier}")]
        [HttpGet]
        public ActionResult VizualizarCourierDetalle(int IdCourier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CourierDetalleRepositorio _repCourierDetalleRepositorioRepositorio = new CourierDetalleRepositorio(_integraDBContext);
                return Ok(_repCourierDetalleRepositorioRepositorio.ObtenerListaCourierDetalle(IdCourier));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda detalle de Couriers
        /// </summary>
        /// <param name=”ObjetoDTO”>DTO de la tabla CourierDetalle</param>
        /// <returns>CourierDetalleDTO<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCourierDetalle([FromBody] CourierDetalleDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PaisRepositorio _repPaisRepositorio = new PaisRepositorio(_integraDBContext);
                PaisNombreDTO pais = _repPaisRepositorio.ObtenerNombrePaisPorId(ObjetoDTO.IdPais);
                ObjetoDTO.Pais = pais.NombrePais;

                CiudadRepositorio _repCiudadRepositorio = new CiudadRepositorio(_integraDBContext);
                CiudadNombreDTO ciudad = _repCiudadRepositorio.ObtenerNombreCiudadPorId(ObjetoDTO.IdCiudad);
                ObjetoDTO.Ciudad = ciudad.Nombre;
     
                CourierDetalleRepositorio _repCourierDetalleRepositorioRepositorio = new CourierDetalleRepositorio(_integraDBContext);
                CourierDetalleBO nuevoRepositorio = new CourierDetalleBO
                {
                    IdCourier = ObjetoDTO.IdCourier,
                    IdPais = ObjetoDTO.IdPais,
                    IdCiudad = ObjetoDTO.IdCiudad,
                    Direccion = ObjetoDTO.Direccion,
                    Telefono = ObjetoDTO.Telefono,
                    Estado = true,
                    TiempoEnvio = ObjetoDTO.TiempoEnvio,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                _repCourierDetalleRepositorioRepositorio.Insert(nuevoRepositorio);
                ObjetoDTO.Id = nuevoRepositorio.Id;
                return Ok(ObjetoDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza detalle de Couriers
        /// </summary>
        /// <param name=”ObjetoDTO”>DTO de la tabla courierDetalle</param>
        /// <returns>CourierDetalleDTO<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarCourierDetalle([FromBody] CourierDetalleDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PaisRepositorio _repPaisRepositorio = new PaisRepositorio(_integraDBContext);
                PaisNombreDTO pais = _repPaisRepositorio.ObtenerNombrePaisPorId(ObjetoDTO.IdPais);
                ObjetoDTO.Pais = pais.NombrePais;
                CiudadRepositorio _repCiudadRepositorio = new CiudadRepositorio(_integraDBContext);
                CiudadNombreDTO ciudad = _repCiudadRepositorio.ObtenerNombreCiudadPorId(ObjetoDTO.IdCiudad);
                ObjetoDTO.Ciudad = ciudad.Nombre;

                CourierDetalleRepositorio _repCourierDetalleRepositorio = new CourierDetalleRepositorio(_integraDBContext);
                CourierDetalleBO courierDetalle = _repCourierDetalleRepositorio.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                courierDetalle.IdCourier = ObjetoDTO.IdCourier;
                courierDetalle.IdPais = ObjetoDTO.IdPais;
                courierDetalle.IdCiudad = ObjetoDTO.IdCiudad;
                courierDetalle.Direccion = ObjetoDTO.Direccion;
                courierDetalle.Telefono = ObjetoDTO.Telefono;
                courierDetalle.TiempoEnvio = ObjetoDTO.TiempoEnvio;
                courierDetalle.Estado = true;
                courierDetalle.UsuarioModificacion = ObjetoDTO.Usuario;
                courierDetalle.FechaModificacion = DateTime.Now;

                _repCourierDetalleRepositorio.Update(courierDetalle);

                return Ok(ObjetoDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina detalle de Courier
        /// </summary>
        /// <param name=”Eliminar”>DTO general que tiene el id a eliminar de una tabla</param>
        /// <returns>Bool<returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarCourierDetalle([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CourierDetalleRepositorio _repCourierDetalleRepositorio = new CourierDetalleRepositorio(_integraDBContext);
                CourierDetalleBO courierDetalle = _repCourierDetalleRepositorio.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repCourierDetalleRepositorio.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Otiene los paises para el combo 
        /// </summary>
        /// <returns>Objeto {string, List<PaisFiltroComboDTO>}<returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPais()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PaisRepositorio _repPaisRepositorio = new PaisRepositorio(_integraDBContext);
                var listaPais = _repPaisRepositorio.ObtenerPaisesCombo();
                return Ok(new { Result = "OK", Records = listaPais });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Otiene las ciudades para el combo 
        /// </summary>
        /// <returns>Objeto {string, List<CiudadFiltroComboDTO>}<returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCiudad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CiudadRepositorio _repCiudadRepositorio = new CiudadRepositorio(_integraDBContext);
                var listaCiudad = _repCiudadRepositorio.ObtenerCiudadesFiltro();
                return Ok(new { Result = "OK", Records = listaCiudad });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Miguel Mora
        /// Fecha: 26/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene valores de detalle de couriers por un courier 
        /// </summary>
        /// <returns>List<FiltroDTO><returns>
        [Route("[Action]/{IdCourier}")]
        [HttpGet]
        public ActionResult VizualizarPaisCourierDetallePorCourier(int IdCourier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CourierDetalleRepositorio _repCourierDetalleRepositorioRepositorio = new CourierDetalleRepositorio(_integraDBContext);
                var ListaCourierDetalle = _repCourierDetalleRepositorioRepositorio.ObtenerCourierDetallePais(IdCourier) ;
                return Ok(ListaCourierDetalle);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Miguel Mora
        /// Fecha: 26/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene valores de detalle de couriers por un courier y por el pais del detalle
        /// </summary>
        /// <returns>List<FiltroDTO><returns>
        [Route("[Action]/{IdCourier}/{IdPais}")]
        [HttpGet]
        public ActionResult VizualizarCiudadCourierDetallePorCourier(int IdCourier,int IdPais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CourierDetalleRepositorio _repCourierDetalleRepositorioRepositorio = new CourierDetalleRepositorio(_integraDBContext);
                var ListaCourierDetalle = _repCourierDetalleRepositorioRepositorio.ObtenerCiudadCourierDetallePorPais(IdCourier,IdPais);
                return Ok(ListaCourierDetalle);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
