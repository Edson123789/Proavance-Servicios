using System;
using System.Linq;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Ciudad
    /// Autor: -, Jashin Salazar
    /// Fecha: 26/04/2021
    /// <summary>
    /// Contiene las acciones para la interaccion con la obtencion de ciudades.
    /// </summary>
    [Route("api/Ciudad")]
    public class CiudadController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public CiudadController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 26/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la ciudad de las sedes
        /// </summary>
        /// <returns> objeto DTO: CiudadDatosDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCiudadSedes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                return Ok(_repCiudad.ObtenerCiudadesDeSedesExistentes());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 26/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las ciudades por Pais
        /// </summary>
        /// <returns> objeto DTO: CiudadDatosDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodasCiudadesPorPais()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                return Ok(_repCiudad.ObtenerCiudadesPorPais());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 26/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las ciudades.
        /// </summary>
        /// <returns> objeto DTO: CiudadDatosDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                return Ok(_repCiudad.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.IdPais }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Filtro Ciudades
        /// </summary>
        /// <returns>Objeto(Alumno)<returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltroCiudades()
        {
            try
            {
                CiudadRepositorio repCiudad = new CiudadRepositorio(_integraDBContext);
                var ciudades = repCiudad.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.IdPais, x.Codigo });
                return Ok(new { ciudades });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 26/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las ciudades por Filtro
        /// </summary>
        /// <returns> objeto DTO: CiudadDatosDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltroCodigo()
        {
            try
            {
                CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                return Ok(_repCiudad.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.IdPais, x.Codigo }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 26/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene ciudades por estado activo
        /// </summary>
        /// <returns> objeto DTO: CiudadDatosDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFiltro()
        {
            try
            {
                CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                return Ok(_repCiudad.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 26/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene ciudades por estado activo
        /// </summary>
        /// <returns> objeto DTO: CiudadDatosDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPaises()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repoPais = new PaisRepositorio(_integraDBContext);
                var paises = _repoPais.ObtenerPaisesCombo();
                return Json(new { Result = "OK", Records = paises });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 26/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene ciudades por estado activo
        /// </summary>
        /// <returns> objeto DTO: CiudadDatosDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult VisualizarCiudad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                var Ciudad = _repCiudad.ObtenerTodoCiudades();
                return Json(new { Result = "OK", Records = Ciudad });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 26/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta ciudades en la base de datos
        /// </summary>
        /// <returns> objeto BO: CiudadBO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCiudad([FromBody] CiudadDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                CiudadBO NuevaCiudad = new CiudadBO
                {
                    Codigo = ObjetoDTO.Codigo,
                    Nombre = ObjetoDTO.Nombre,
                    IdPais = ObjetoDTO.IdPais,
                    LongCelular = ObjetoDTO.LongCelular,
                    LongTelefono = ObjetoDTO.LongTelefono,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };

                _repCiudad.Insert(NuevaCiudad);

                return Ok(NuevaCiudad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 26/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza ciudades en la base de datos
        /// </summary>
        /// <returns> objeto BO: CiudadBO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarCiudad([FromBody] CiudadDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                CiudadBO Ciudad = _repCiudad.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                Ciudad.Codigo = ObjetoDTO.Codigo;
                Ciudad.Nombre = ObjetoDTO.Nombre;
                Ciudad.IdPais = ObjetoDTO.IdPais;
                Ciudad.LongCelular = ObjetoDTO.LongCelular;
                Ciudad.LongTelefono = ObjetoDTO.LongTelefono;
                Ciudad.UsuarioModificacion = ObjetoDTO.Usuario;
                Ciudad.FechaModificacion = DateTime.Now;
                Ciudad.Estado = true;

                _repCiudad.Update(Ciudad);

                return Ok(Ciudad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 26/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza ciudades en la base de datos
        /// </summary>
        /// <returns> objeto BO: CiudadBO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarCiudadMultiple([FromBody] CiudadMultipleDTO Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                    
                    foreach (int tmp in Obj.ListaRegiones)
                    {
                        CiudadBO Ciudad = _repCiudad.GetBy(x => x.Id == tmp).FirstOrDefault();                        
                        
                        Ciudad.IdPais = Obj.IdPais;
                        Ciudad.LongCelular = Obj.LongitudCelular;
                        Ciudad.LongTelefono = Obj.LongitudTelefono;
                        Ciudad.UsuarioModificacion = Obj.Usuario;
                        Ciudad.FechaModificacion = DateTime.Now;
                        Ciudad.Estado = true;
                        _repCiudad.Update(Ciudad);
                    }
                    scope.Complete();
                }




                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 26/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina ciudades en la base de datos
        /// </summary>
        /// <returns> objeto BO: CiudadBO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarCiudad([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                CiudadBO Ciudad = _repCiudad.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repCiudad.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
