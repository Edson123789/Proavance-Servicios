using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.DTO;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PasarelaPagoPWController
    /// Autor: Jorge Rivera
    /// Fecha: 04/03/2021
    /// <summary>
    /// Pasarelas de pagos
    /// </summary>
    [Route("api/PasarelaPagoPw")]
    public class PasarelaPagoPWController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        /// Controlador: funcion para obtener la lista de regidtros activos
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaRegistroActivo()
        {
            try
            {

                PasarelaPagoPWBO ObjetoRegistro = new PasarelaPagoPWBO();

                var Registro = ObjetoRegistro.ListaRegistroPasarelaPagoPw(true);

                return Ok(Registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaRegistroEliminado()
        {
            try
            {

                PasarelaPagoPWBO ObjetoRegistro = new PasarelaPagoPWBO();

                var Registro = ObjetoRegistro.ListaRegistroPasarelaPagoPw(false);

                return Ok(Registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] PasarelaPagoPWDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PasarelaPagoPWBO ObjetoRegistro = new PasarelaPagoPWBO();

                ObjetoRegistro.Nombre = Json.Nombre;
                ObjetoRegistro.Prioridad = Json.Prioridad;
                ObjetoRegistro.IdPais = Json.IdPais;
                ObjetoRegistro.IdProveedor = Json.IdProveedor;
                ObjetoRegistro.Estado = true;
                ObjetoRegistro.UsuarioCreacion = Json.Usuario;
                ObjetoRegistro.UsuarioModificacion = Json.Usuario;
                ObjetoRegistro.FechaCreacion = DateTime.Now;
                ObjetoRegistro.FechaModificacion = DateTime.Now;

                ObjetoRegistro.RegistrarPasarelaPagoPw();

                if (!ObjetoRegistro.Excepcion.Error)
                {
                    var _Registro = ObjetoRegistro.RegistroPasarelaPagoPwPorId(ObjetoRegistro.Id);
                    return Ok(_Registro);
                }
                else
                {
                    return Ok(ObjetoRegistro.Excepcion);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] PasarelaPagoPWDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PasarelaPagoPWBO ObjetoRegistro = new PasarelaPagoPWBO();

                ObjetoRegistro.Id = Json.Id;
                ObjetoRegistro.Nombre = Json.Nombre;
                ObjetoRegistro.Prioridad = Json.Prioridad;
                ObjetoRegistro.IdPais = Json.IdPais;
                ObjetoRegistro.IdProveedor = Json.IdProveedor;
                ObjetoRegistro.UsuarioModificacion = Json.Usuario;
                ObjetoRegistro.FechaModificacion = DateTime.Now;

                ObjetoRegistro.ActualizarPasarelaPagoPw();

                if (!ObjetoRegistro.Excepcion.Error)
                {
                    var _Registro = ObjetoRegistro.RegistroPasarelaPagoPwPorId(ObjetoRegistro.Id);
                    return Ok(_Registro);
                }
                else
                {
                    return Ok(ObjetoRegistro.Excepcion);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PasarelaPagoPWBO ObjetoRegistro = new PasarelaPagoPWBO();

                ObjetoRegistro.Id = Json.Id;
                ObjetoRegistro.UsuarioModificacion = Json.NombreUsuario;

                ObjetoRegistro.EliminarPasarelaPagoPw();

                if (!ObjetoRegistro.Excepcion.Error)
                {
                    return Ok(ObjetoRegistro);
                }
                else
                {
                    return Ok(ObjetoRegistro.Excepcion);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET 
        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de los metodos de pago del pais por el id de alumno
        /// </summary>
        /// <returns>List<RegistroPasarelaPagoPWDTO></returns>
        [Route("[action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerListaMetodoPagoPorIdAlumno(int IdAlumno)
        {
            try
            {

                PasarelaPagoPWBO objetoRegistro = new PasarelaPagoPWBO();

                var registro = objetoRegistro.ListaMetodoPagoPorIdAlumno(IdAlumno);

                return Ok(registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET 
        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el métodos de pago registrado por el IdMatriculaCabecera
        /// </summary>
        /// <returns>MedioPagoMatriculaCronogramaDTO</returns>
        [Route("[action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerMetodoPagoPorIdMatriculaCabecera(int IdMatriculaCabecera)
        {
            try
            {
                PasarelaPagoPWBO objetoRegistro = new PasarelaPagoPWBO();

                var registro = objetoRegistro.MedioPagoMatriculaCronogramaPorIdMatricula(IdMatriculaCabecera);

                return Ok(registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Registra el método de pago de la matrícula
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult RegistroMedioPagoMatriculaCronograma([FromBody] RegistroMedioPagoMatriculaCronogramaDTO model)
        {
            try
            {

                PasarelaPagoPWBO objetoRegistro = new PasarelaPagoPWBO();

                var registro = objetoRegistro.RegistroMedioPagoMatriculaCronograma(model);

                return Ok(registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Abelson Quiñones
        /// Fecha: 06/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener el IdMatriculaCabecera por IdAlumno y Centro de costo
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]/{IdAlumno}/{IdCentroCosto}")]
        [HttpGet]
        public ActionResult ObtenerMatriculaPorAlumnoCosto(int IdAlumno, int IdCentroCosto)
        {
            try
            {

                PasarelaPagoPWBO objetoRegistro = new PasarelaPagoPWBO();

                var registro = objetoRegistro.BuscarIdMatriculaCabeceraPorAlumnoCosto(IdAlumno,IdCentroCosto);

                return Ok(registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
