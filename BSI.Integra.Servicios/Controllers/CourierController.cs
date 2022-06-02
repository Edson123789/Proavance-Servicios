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
    /// Controlador: CourierController
    /// Autor: Max Mantilla
    /// Fecha: 25/11/2021
    /// <summary>
    /// Mantenimiento de la tabla pla.T_Courier
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public CourierController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// TipoFuncion: GET
        /// Autor: Max Mantilla
        /// Fecha: 25/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los couriers
        /// </summary>
        /// <returns>List<CourierDTO><returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCourier()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CourierRepositorio _repCourierRepositorioRepositorio = new CourierRepositorio(_integraDBContext);
                return Ok(_repCourierRepositorioRepositorio.ObtenerCourier());
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
        /// Obtiene valores de couriers para un combo
        /// </summary>
        /// <returns>List<FiltroDTO><returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult VizualizarCourier()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CourierRepositorio _repCourierRepositorioRepositorio = new CourierRepositorio(_integraDBContext);
                return Ok(_repCourierRepositorioRepositorio.ObtenerListaCourier());
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
        /// Guarda Couriers
        /// </summary>
        /// <param name=”ObjetoDTO”>DTO de la tabla courier</param>
        /// <returns>CourierDTO<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCourier([FromBody] CourierDTO ObjetoDTO)
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
                CourierRepositorio _repCourierRepositorioRepositorio = new CourierRepositorio(_integraDBContext);
                CourierBO nuevoRepositorio = new CourierBO
                {
                    Nombre = ObjetoDTO.Nombre,
                    IdPais = ObjetoDTO.IdPais,
                    IdCiudad = ObjetoDTO.IdCiudad,
                    Direccion = ObjetoDTO.Direccion,
                    Telefono = ObjetoDTO.Telefono,
                    Estado = true,
                    Url = ObjetoDTO.Url,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                _repCourierRepositorioRepositorio.Insert(nuevoRepositorio);
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
        /// Actualiza Couriers
        /// </summary>
        /// <param name=”ObjetoDTO”>DTO de la tabla courier</param>
        /// <returns>CourierDTO<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarCourier([FromBody] CourierDTO ObjetoDTO)
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

                CourierRepositorio _repCourierRepositorio = new CourierRepositorio(_integraDBContext);
                CourierBO courier = _repCourierRepositorio.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                courier.Nombre = ObjetoDTO.Nombre;
                courier.IdPais = ObjetoDTO.IdPais;
                courier.IdCiudad = ObjetoDTO.IdCiudad;
                courier.Direccion = ObjetoDTO.Direccion;
                courier.Telefono = ObjetoDTO.Telefono;
                courier.Url = ObjetoDTO.Url;
                courier.Estado = true;
                courier.UsuarioModificacion = ObjetoDTO.Usuario;
                courier.FechaModificacion = DateTime.Now;

                _repCourierRepositorio.Update(courier);

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
        /// Elimina Couriers
        /// </summary>
        /// <param name=”Eliminar”>DTO general que tiene el id a eliminar de una tabla</param>
        /// <returns>Bool<returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarCourier([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CourierRepositorio _repCourierRepositorio = new CourierRepositorio(_integraDBContext);
                CourierBO courier = _repCourierRepositorio.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repCourierRepositorio.Delete(Eliminar.Id, Eliminar.NombreUsuario);
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
        /// <returns>Objeto {string, List<PaisFiltroComboDTO>}<returns>
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
        /// Obtiene valores de couriers para un combo
        /// </summary>
        /// <returns>List<FiltroDTO><returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult VizualizarCourierMasCiudad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CourierRepositorio _repCourierRepositorioRepositorio = new CourierRepositorio(_integraDBContext);
                return Ok(_repCourierRepositorioRepositorio.ObtenerListaCourierMasCiudad());
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
        /// Obtiene valores de couriers filtrando por el valor recibido
        /// </summary>
        /// <returns>List<FiltroDTO><returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult VizualizarCourierMasCiudadAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    CourierRepositorio _repCourierRepositorioRepositorio = new CourierRepositorio(_integraDBContext);
                    return Ok(_repCourierRepositorioRepositorio.ObtenerListaCourierMasCiudadAutocomplete(Filtros["Valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

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
        /// Obtiene nombre de courier por id
        /// </summary>
        /// <param name=”Courier”>DTO general que tiene el nombre del courier según Id
        /// <returns><CourierDTO><returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCourierPorId(string Courier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CourierRepositorio _repCourierRepositorio = new CourierRepositorio(_integraDBContext);
                return Ok(_repCourierRepositorio.ObtenerCourierPorId(Courier));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
