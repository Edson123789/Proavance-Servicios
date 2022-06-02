using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using CsvHelper;
using Google.Api.Ads.Common.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ContenidoCertificadoIrca")]
    public class ContenidoCertificadoIrcaController : Controller
    {
        private integraDBContext _integraDBContext;
        public ContenidoCertificadoIrcaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [HttpPost]
        [Route("[Action]")]
        public ActionResult MostrarContenidoIrca([FromForm] IFormFile files)
        {
            var listaContenidoIrcaDTO = new List<ContenidoCertificadoIrcaDTO>();
            CsvFile file = new CsvFile();
            try
            {
                CentroCostoRepositorio repCentroCostoRep = new CentroCostoRepositorio(_integraDBContext);
                MatriculaCabeceraRepositorio repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                int index = 0;

                using (var cvs = new CsvReader(new StreamReader(files.OpenReadStream())))
                {
                    cvs.Configuration.Delimiter = ";";
                    cvs.Configuration.MissingFieldFound = null;
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        ContenidoCertificadoIrcaDTO contenidoIrca = new ContenidoCertificadoIrcaDTO();
                        try
                        {
                            index++;
                            
                            contenidoIrca.CodigoMatricula = cvs.GetField<string>("CodigoMatricula");
                            contenidoIrca.IdMatriculaCabecera = repMatriculaCabecera.ObtenerIdMatriculaPorCodigo(contenidoIrca.CodigoMatricula).Id;
                            contenidoIrca.CursoIrcaId = cvs.GetField<int>("CursoIrcaId");
                            contenidoIrca.NombreCurso = cvs.GetField<string>("NombreCurso");
                            contenidoIrca.CodigoCurso = cvs.GetField<string>("CodigoCurso");
                            string fechaI = cvs.GetField<string>("FechaInicio");
                            contenidoIrca.FechaInicio = DateTime.ParseExact(fechaI, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string fechaF = cvs.GetField<string>("FechaFin");
                            contenidoIrca.FechaFin = DateTime.ParseExact(fechaF, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            contenidoIrca.DuracionCurso = cvs.GetField<int>("DuracionCurso");
                            contenidoIrca.ResultadoCurso = cvs.GetField<string>("ResultadoCurso");
                            contenidoIrca.CentroCostoIrca = cvs.GetField<string>("CentroCostoIrca");
                            contenidoIrca.IdCentroCostoIrca = repCentroCostoRep.FirstBy(w => w.Nombre == contenidoIrca.CentroCostoIrca).Id;

                            listaContenidoIrcaDTO.Add(contenidoIrca);
                        }
                        catch(Exception ex)
                        {
                            if (listaContenidoIrcaDTO.Count == 0)
                            {
                                return BadRequest(ex.Message);
                            }
                        }
                        
                    }
                }
                var Nregistros = index;
                return Ok(new { listaContenidoIrcaDTO, Nregistros });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("[Action]")]
        public ActionResult InsertarContenidoCertificadoIrca([FromBody] List<ContenidoCertificadoIrcaDTO> obj)
        {
            var listaContenidoIrcaDTO = new List<ContenidoCertificadoIrcaDTO>();
            CsvFile file = new CsvFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CentroCostoRepositorio repCentroCostoRep = new CentroCostoRepositorio(_integraDBContext);
                MatriculaCabeceraRepositorio repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                ContenidoCertificadoIrcaRepositorio _repContenidoCertificadoIrca = new ContenidoCertificadoIrcaRepositorio(_integraDBContext);
                foreach (var item in obj)
                {
                    ContenidoCertificadoIrcaBO contenidoIrca = new ContenidoCertificadoIrcaBO();

                    contenidoIrca.IdMatriculaCabecera = item.IdMatriculaCabecera;
                    contenidoIrca.CursoIrcaId = item.CursoIrcaId;
                    contenidoIrca.NombreCurso = item.NombreCurso;
                    contenidoIrca.CodigoCurso = item.CodigoCurso;
                    contenidoIrca.FechaInicio = item.FechaInicio;
                    contenidoIrca.FechaFin = item.FechaFin;
                    contenidoIrca.DuracionCurso = item.DuracionCurso;
                    contenidoIrca.ResultadoCurso = item.ResultadoCurso;
                    contenidoIrca.IdCentroCostoIrca = item.IdCentroCostoIrca;
                    contenidoIrca.Procesado = false;
                    contenidoIrca.Estado = true;
                    contenidoIrca.UsuarioCreacion = item.Usuario;
                    contenidoIrca.UsuarioModificacion = item.Usuario;
                    contenidoIrca.FechaCreacion = DateTime.Now;
                    contenidoIrca.FechaModificacion = DateTime.Now;
                    
                    _repContenidoCertificadoIrca.Insert(contenidoIrca);
                }        
                        
                return Ok(obj);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
