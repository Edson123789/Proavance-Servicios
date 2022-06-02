using System;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Google.Api.Ads.Common.Util;
using BSI.Integra.Aplicacion.DTOs;
using System.Collections.Generic;
using CsvHelper;
using System.IO;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/RegistroMarcadorFecha")]
    public class RegistroMarcadorFechaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly RegistroMarcadorFechaRepositorio _repRegistroMarcadorFecha;
        public RegistroMarcadorFechaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repRegistroMarcadorFecha = new RegistroMarcadorFechaRepositorio(_integraDBContext);
        }
               
        [Route("[action]")]
        [HttpPost]
        public ActionResult MostrarFurs([FromForm] IFormFile files)
        {
            var ListaRegistroMarcadorFechaDTO = new List<RegistroMarcadorFechaDTO>();
            CsvFile file = new CsvFile();
            try
            {
                integraDBContext integraDB = new integraDBContext();
                RegistroMarcadorFechaRepositorio repFurRep = new RegistroMarcadorFechaRepositorio(_integraDBContext);
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

                        RegistroMarcadorFechaDTO registro = new RegistroMarcadorFechaDTO();
                        registro.IdPersonal = cvs.GetField<int>("IdPersonal");
                        registro.IdCiudad = cvs.GetField<int>("IdCiudad");
                        registro.Pin = cvs.GetField<string>("Pin");
                        registro.Fecha = cvs.GetField<string>("Fecha");
                        registro.M1 = cvs.GetField<string>("M1");
                        registro.M2 = cvs.GetField<string>("M2");
                        registro.M3 = cvs.GetField<string>("M3");
                        registro.M4 = cvs.GetField<string>("M4");
                        registro.M5 = cvs.GetField<string>("M5");
                        registro.M6 = cvs.GetField<string>("M6");
                        ListaRegistroMarcadorFechaDTO.Add(registro);
                    }
                }
                var Nregistros = index;
                return Ok(new { ListaRegistroMarcadorFechaDTO, Nregistros });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody]ListaRegistroMarcadorFechaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RegistroMarcadorFechaRepositorio registroMarcadorFechaRepositorio = new RegistroMarcadorFechaRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                 
                    foreach (var registro in Json.ListaRegistroMarcadorFecha)
                    {
                        
                        RegistroMarcadorFechaBO registroMarcadorFechaBO = new RegistroMarcadorFechaBO();
                        registroMarcadorFechaBO.IdCiudad = registro.IdCiudad;
                        registroMarcadorFechaBO.IdPersonal = registro.IdPersonal;
                        registroMarcadorFechaBO.Pin = registro.Pin;
                        registroMarcadorFechaBO.Fecha = DateTime.Parse(registro.Fecha);
                        registroMarcadorFechaBO.M1 = TimeSpan.Parse(registro.M1);
                        registroMarcadorFechaBO.M2 = TimeSpan.Parse(registro.M2);
                        registroMarcadorFechaBO.M3 = TimeSpan.Parse(registro.M3);
                        registroMarcadorFechaBO.M4 = TimeSpan.Parse(registro.M4);
                        registroMarcadorFechaBO.M5 = TimeSpan.Parse(registro.M5);
                        registroMarcadorFechaBO.M6 = TimeSpan.Parse(registro.M6);
                        registroMarcadorFechaBO.Estado = true;
                        registroMarcadorFechaBO.UsuarioCreacion = registro.Usuario;
                        registroMarcadorFechaBO.UsuarioModificacion = registro.Usuario;
                        registroMarcadorFechaBO.FechaCreacion = DateTime.Now;
                        registroMarcadorFechaBO.FechaModificacion = DateTime.Now;

                        registroMarcadorFechaRepositorio.Insert(registroMarcadorFechaBO);
                    }
                    scope.Complete();
                }
                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar(RegistroMarcadorFechaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RegistroMarcadorFechaRepositorio registroMarcadorFechaRepositorio = new RegistroMarcadorFechaRepositorio();

                RegistroMarcadorFechaBO registroMarcadorFechaBO = registroMarcadorFechaRepositorio.FirstById(Json.Id);
                registroMarcadorFechaBO.IdCiudad = Json.IdCiudad;
                registroMarcadorFechaBO.IdPersonal = Json.IdPersonal;
                registroMarcadorFechaBO.Pin = Json.Pin;
                registroMarcadorFechaBO.Fecha = DateTime.Parse(Json.Fecha);
                registroMarcadorFechaBO.M1 = TimeSpan.Parse(Json.M1);
                registroMarcadorFechaBO.M2 = TimeSpan.Parse(Json.M2);
                registroMarcadorFechaBO.M3 = TimeSpan.Parse(Json.M3);
                registroMarcadorFechaBO.M4 = TimeSpan.Parse(Json.M4);
                registroMarcadorFechaBO.M5 = TimeSpan.Parse(Json.M5);
                registroMarcadorFechaBO.M6 = TimeSpan.Parse(Json.M6);
                registroMarcadorFechaBO.UsuarioModificacion = Json.Usuario;
                registroMarcadorFechaBO.FechaModificacion = DateTime.Now;

                return Ok(registroMarcadorFechaRepositorio.Update(registroMarcadorFechaBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar(RegistroMarcadorFechaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RegistroMarcadorFechaRepositorio registroMarcadorFechaRepositorio = new RegistroMarcadorFechaRepositorio();
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (registroMarcadorFechaRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = registroMarcadorFechaRepositorio.Delete(Json.Id, Json.Usuario);
                    }

                    scope.Complete();
                }
                return Ok(estadoEliminacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
