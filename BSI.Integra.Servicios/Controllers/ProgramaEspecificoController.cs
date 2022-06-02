using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Transversal.BO;

using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Planificacion.BO;
using System.Net;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ProgramaEspecifico")]
    public class ProgramaEspecificoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PespecificoRepositorio _repPespecifico;

        public ProgramaEspecificoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repPespecifico = new PespecificoRepositorio(_integraDBContext);
        }
        [Route("[Action]/{IdProgramaGeneral}/{IdCentroCosto}")]
        [HttpGet]
        public ActionResult ObtenerTodoPEspecificoParaFiltro(int? IdProgramaGeneral, int? IdCentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                List<DatosListaPespecificoDTO> PEspecifico = new List<DatosListaPespecificoDTO>();
                if (IdProgramaGeneral != 0 || IdCentroCosto != 0)
                {
                    if (IdCentroCosto != 0)
                        PEspecifico = _repPEspecifico.ObtenerListaProgramaEspecifico(IdCentroCosto.Value);
                    if (IdProgramaGeneral != 0)
                        PEspecifico = _repPEspecifico.ObtenerListaProgramaEspecificoPorIdPGeneral(IdProgramaGeneral.Value);
                }
                else
                {
                    PEspecifico = _repPEspecifico.ObtenerListaProgramaEspecificoParaFiltro();
                }

                return Ok(new { data = PEspecifico, Total = PEspecifico.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaProgramaGeneral()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralRepositorio _pgeneralRepositorio = new PgeneralRepositorio();
                return Ok(new { data = _pgeneralRepositorio.ObtenerProgramasFiltro() });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idPEspecifico}")]
        [HttpGet]
        public ActionResult ObtenerAlumnosPEspecifico(int idPEspecifico)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);

                var Resultado = _repPEspecifico.ObtenerAlumnosProgramaEspecifico(idPEspecifico);
                return Ok(Resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCentroCosto()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(contexto);
                _repCentroCosto.ObtenerCentroCostoParaFiltro();

                return Ok(new { data = _repCentroCosto.ObtenerCentroCostoParaFiltro() });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaLocacionTroncal()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TroncalPgeneralRepositorio _repTroncal = new TroncalPgeneralRepositorio();
                var LocacionTroncal = _repTroncal.ObtenerListaLocacionTroncal();
                return Ok(new { data = LocacionTroncal });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerModalidadCurso()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModalidadCursoRepositorio _repModalidad = new ModalidadCursoRepositorio();
                var modalidades = _repModalidad.ObtenerModalidadCursoFiltro();

                return Ok(new { data = modalidades });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarCentroCostoCodigoNombre([FromBody] FiltroGenerarCodigoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _context = new integraDBContext();
                PespecificoBO PEspecifico = new PespecificoBO();
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_context);
                CentroCostoGeneradoDTO NuevoCentroCosto = new CentroCostoGeneradoDTO();
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_context);

                string CodigoCentroCosto;
                string NombrePEspecificoNuevo;
                var NombreCiudad = dto.NombreCiudad;
                var Condicion = "";
                var Modalidad = dto.Modalidad.ToUpper();

                var PGeneral = _repPEspecifico.ObtenerDatosPGeneralParaPEspecifico(dto.IdProgramaGeneral);
                if (PGeneral.IdCategoria == 0)
                    throw new Exception("Programa General no tiene Categoria");

                var CiudadCategoria = _repPEspecifico.ObtenerCiudadCategoria(dto.IdCiudad.Value, PGeneral.IdCategoria);
                if (CiudadCategoria == null)
                    throw new Exception("No existe Troncal para esta categoria en esta ciudad: " + dto.IdCiudad);

                string codigo = CiudadCategoria.TroncalCompleto;

                var CentroCostoUltimo = _repCentroCosto.GetBy(w => w.Codigo.Contains(codigo)).OrderByDescending(x => Convert.ToInt64(x.Codigo)).FirstOrDefault();
                if (CentroCostoUltimo == null)
                {
                    CodigoCentroCosto = codigo + "001";
                }
                else
                {
                    string codigoPrimeraParte = CentroCostoUltimo.Codigo.Substring(0, 6);
                    string codigoUltimosDigitos = CentroCostoUltimo.Codigo.Substring(6);
                    string sumado = (Int64.Parse(codigoUltimosDigitos) + 1).ToString();
                    if (codigoUltimosDigitos.Substring(0, 1).Equals("0")) sumado = "0" + sumado;
                    if (codigoUltimosDigitos.Substring(0, 2).Equals("00")) sumado = "0" + sumado;
                    CodigoCentroCosto = codigoPrimeraParte + (sumado).ToString();
                }

                if (NombreCiudad.ToUpper() == "AREQUIPA")
                    NombreCiudad = "AQP";

                NuevoCentroCosto.CentroCosto = new CentroCosto2DTO();

                if (Modalidad == "PRESENCIAL")
                {
                    Condicion = " (Nombre Not Like '%ONLINE%') ";
                    Modalidad = "";
                }

                if (Modalidad == "ONLINE ASINCRONICA")
                {
                    Condicion = " (Nombre Like '%AONLINE%') ";
                    Modalidad = "AONLINE";
                }

                if (Modalidad == "ONLINE SINCRONICA")
                {
                    Condicion = " (Nombre Like '%ONLINE%' and Nombre Not Like'%AONLINE%') ";
                    Modalidad = "ONLINE";
                }

                var ListaCentroCosto = _repCentroCosto.ObtenerCentroCostoParaPEspecifico(PGeneral.Codigo, Condicion, dto.Anio.ToString(), NombreCiudad);
                var FiltradoPorCodigo = ListaCentroCosto.Where(s => s.IdPgeneral.Equals(PGeneral.Codigo)).ToList();

                string roman = PEspecifico.ToRoman(FiltradoPorCodigo.Count() + 1);


                NombrePEspecificoNuevo = PGeneral.Nombre + ' '
                                    + (String.IsNullOrEmpty(Modalidad) ? "" : Modalidad + " ")
                                    + dto.Anio.ToString() + ' ' + roman + ' ' + NombreCiudad;
                NuevoCentroCosto.CentroCosto.IdArea = PGeneral.IdArea;
                NuevoCentroCosto.CentroCosto.IdSubArea = PGeneral.IdSubArea;
                NuevoCentroCosto.CentroCosto.IdPgeneral = PGeneral.Codigo;
                NuevoCentroCosto.CentroCosto.Nombre = PGeneral.Codigo + ' '
                                 + (String.IsNullOrEmpty(Modalidad) ? "" : Modalidad + " ")
                                 + dto.Anio.ToString() + ' ' + roman + ' ' + NombreCiudad;
                NuevoCentroCosto.CentroCosto.Codigo = CodigoCentroCosto;
                NuevoCentroCosto.Codigo = CodigoCentroCosto;
                NuevoCentroCosto.CentroCosto.IdAreaCc = "9-3";
                NuevoCentroCosto.NombreProgramaEspecifico = NombrePEspecificoNuevo;
                NuevoCentroCosto.CodigoBanco = "A" + (_context.TCentroCosto.OrderBy(s => s.Id).Last().Id + 1);
                NuevoCentroCosto.NombreProgramaGeneral = PGeneral.Nombre;

                return Ok(new { data = NuevoCentroCosto });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerLocaciones()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                LocacionRepositorio _repLocacion = new LocacionRepositorio();
                var Locacion = _repLocacion.ObtenerLocacionParaFiltro();
                return Ok(new { data = Locacion });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerAmbientes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AmbienteRepositorio _repAmbiente = new AmbienteRepositorio();
                var Ambiente = _repAmbiente.ObtenerAmbientesCiudad();
                return Ok(new { data = Ambiente });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerOrigenProgramas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrigenProgramaRepositorio _repOrigenPrograma = new OrigenProgramaRepositorio();
                var OrigenProgramas = _repOrigenPrograma.ObtenerOrigenProgramas();

                return Ok(new { data = OrigenProgramas });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerExpositores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExpositorRepositorio _repoExpositor = new ExpositorRepositorio();
                var expositores = _repoExpositor.ObtenerExpositoresFiltro();

                return Ok(new { data = expositores });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarPespecifico([FromBody] CompuestoProgramaEspecificoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                FurRepositorio _repFur = new FurRepositorio();
                PespecificoBO PEspecifico = _repPEspecifico.FirstById(dto.Objeto.Id);
                if (dto.Objeto.EstadoPid == 0)
                {
                    var furs = _repFur.ObtenerFurProgramaEspecifico(dto.Objeto.Id, 1, false);
                    if (furs.Count > 0)
                    {
                        foreach (var item in furs)
                        {
                            _repFur.Delete(item.Id, dto.Usuario);
                        }
                    }
                }

                PEspecifico.Nombre = dto.Objeto.Nombre;
                PEspecifico.Codigo = dto.Objeto.Codigo;
                PEspecifico.IdCentroCosto = dto.Objeto.IdCentroCosto;
                PEspecifico.EstadoP = dto.Objeto.EstadoP;
                PEspecifico.IdEstadoPespecifico = dto.Objeto.EstadoPid;
                PEspecifico.Tipo = dto.Objeto.Tipo;
                PEspecifico.TipoAmbiente = dto.Objeto.TipoAmbiente;
                PEspecifico.IdProgramaGeneral = dto.Objeto.IdProgramaGeneral;
                PEspecifico.Ciudad = dto.Objeto.Ciudad;
                PEspecifico.CodigoBanco = dto.Objeto.CodigoBanco;
                PEspecifico.EstadoPid = dto.Objeto.EstadoPid;
                PEspecifico.TipoId = dto.Objeto.TipoId;
                PEspecifico.OrigenPrograma = dto.Objeto.OrigenPrograma;
                PEspecifico.IdCiudad = dto.Objeto.IdCiudad;
                PEspecifico.Duracion = dto.Objeto.Duracion;
                PEspecifico.ActualizacionAutomatica = dto.Objeto.ActualizacionAutomatica;
                PEspecifico.IdCursoMoodle = dto.Objeto.IdCursoMoodle;
                PEspecifico.IdCursoMoodlePrueba = dto.Objeto.IdCursoMoodlePrueba;
                PEspecifico.CursoIndividual = dto.Objeto.CursoIndividual;
                //PEspecifico.IdExpositorReferencia = dto.Objeto.IdExpositorReferencia;
                //PEspecifico.IdAmbiente = dto.Objeto.IdAmbiente;
                PEspecifico.UrlDocumentoCronograma = dto.Objeto.UrlDocumentoCronograma;
                PEspecifico.UsuarioModificacion = dto.Usuario;
                PEspecifico.FechaModificacion = DateTime.Now;

                _repPEspecifico.Update(PEspecifico);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCrearCursosConCentroCosto([FromBody] FiltroInsertarPEspecificoDTO dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                RegistroProgramaEspecificoDTO ResultadoProgramaEspecifico = new RegistroProgramaEspecificoDTO();
                List<CentroCostoBO> ListaCentroCosto = new List<CentroCostoBO>();
                CentroCostoBO CentroCostoPadre = new CentroCostoBO(contexto);
                List<PespecificoPadrePespecificoHijoBO> ListaPadreHijo = new List<PespecificoPadrePespecificoHijoBO>();

                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(contexto);
                CursoPespecificoRepositorio _repCursoPEspecifico = new CursoPespecificoRepositorio(contexto);
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(contexto);
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(contexto);
                ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio(contexto);
                PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadrePespecificoHijo = new PespecificoPadrePespecificoHijoRepositorio(contexto);
                PEspecificoParticipacionExpositorRepositorio _repPEspecificoParticipacionExpositor = new PEspecificoParticipacionExpositorRepositorio(contexto);



                if (dto.CentroCosto != null)
                {
                    CentroCostoPadre.IdArea = dto.CentroCosto.IdArea;
                    CentroCostoPadre.IdSubArea = dto.CentroCosto.IdSubArea;
                    CentroCostoPadre.IdPgeneral = dto.CentroCosto.IdPgeneral;
                    CentroCostoPadre.Nombre = dto.CentroCosto.Nombre;
                    CentroCostoPadre.Codigo = dto.CentroCosto.Codigo;
                    CentroCostoPadre.IdAreaCc = dto.CentroCosto.IdAreaCc;
                    CentroCostoPadre.Ismtotales = dto.CentroCosto.Ismtotales;
                    CentroCostoPadre.Icpftotales = dto.CentroCosto.Icpftotales;
                    CentroCostoPadre.Estado = true;
                    CentroCostoPadre.UsuarioCreacion = dto.Usuario;
                    CentroCostoPadre.UsuarioModificacion = dto.Usuario;
                    CentroCostoPadre.FechaCreacion = DateTime.Now;
                    CentroCostoPadre.FechaModificacion = DateTime.Now;

                    if (CentroCostoPadre.HasErrors)
                        return BadRequest(CentroCostoPadre.GetErrors(null));
                }

                //dto.Objeto.IdCentroCosto = CentroCosto.Id;                

                if (_repPEspecifico.Exist(w => w.CodigoBanco == dto.Objeto.CodigoBanco))
                {
                    throw new Exception("Ya existe otro PEspecifico con este codigo Banco!");
                }

                PespecificoBO PEspecificoPadre = new PespecificoBO(contexto)
                {
                    Nombre = dto.Objeto.Nombre,
                    Codigo = dto.Objeto.Codigo,
                    IdCentroCosto = dto.Objeto.IdCentroCosto,
                    EstadoP = dto.Objeto.EstadoP,
                    Tipo = dto.Objeto.Tipo,
                    TipoAmbiente = dto.Objeto.TipoAmbiente,
                    IdProgramaGeneral = dto.Objeto.IdProgramaGeneral,
                    Ciudad = dto.Objeto.Ciudad,
                    Categoria = dto.Objeto.Categoria,
                    CodigoBanco = dto.Objeto.CodigoBanco,
                    EstadoPid = dto.Objeto.EstadoPid,
                    TipoId = dto.Objeto.TipoId,
                    OrigenPrograma = dto.Objeto.OrigenPrograma,
                    IdCiudad = dto.Objeto.IdCiudad,
                    Duracion = dto.Objeto.Duracion,
                    ActualizacionAutomatica = dto.Objeto.ActualizacionAutomatica,
                    IdCursoMoodle = dto.Objeto.IdCursoMoodle,
                    IdCursoMoodlePrueba = dto.Objeto.IdCursoMoodlePrueba,
                    CursoIndividual = dto.Objeto.CursoIndividual,
                    IdExpositorReferencia = dto.Objeto.IdExpositorReferencia,
                    IdAmbiente = dto.Objeto.IdAmbiente,
                    IdEstadoPespecifico = dto.Objeto.EstadoPid,
                    UrlDocumentoCronograma = dto.Objeto.UrlDocumentoCronograma,
                    Estado = true,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                PEspecificoPadre.CursoPespecifico = new CursoPespecificoBO()
                {
                    IdPespecifico = PEspecificoPadre.Id,
                    Nombre = PEspecificoPadre.Nombre,
                    Duracion = 1,
                    Orden = 1,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    Estado = true
                };
                if (PEspecificoPadre.CursoPespecifico.HasErrors)
                    return BadRequest(PEspecificoPadre.CursoPespecifico.GetErrors(null));

                if (dto.CentroCosto != null)
                {
                    if (!PEspecificoPadre.HasErrors)
                        CentroCostoPadre.ProgramaEspecifico = PEspecificoPadre;
                    else
                        return BadRequest(PEspecificoPadre.GetErrors(null));
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    if (dto.CentroCosto != null)
                    {
                        _repCentroCosto.Insert(CentroCostoPadre);
                        var pespecificotemp = _repPEspecifico.FirstBy(w => w.IdCentroCosto == CentroCostoPadre.Id, x => new { x.Id });
                        CentroCostoPadre.ProgramaEspecifico.Id = pespecificotemp.Id;
                        PEspecificoPadre.Id = pespecificotemp.Id;
                    }
                    else
                    {
                        _repPEspecifico.Insert(PEspecificoPadre);
                    }

                    var ListaPGenerales = _repPGeneral.ObtenerPgeneralCursos(dto.Objeto.IdProgramaGeneral.Value);

                    int contador = 0;

                    foreach (var item in ListaPGenerales)
                    {
                        contador++;
                        string NombrePEspecifico;
                        var PGeneral = _repPGeneral.ObtenerPGeneralParaPEspecifico(item.IdPGeneral_Hijo);
                        string Codigo = PEspecificoPadre.ObtenerCodigoPEspecifico(PGeneral, dto.IdCiudad);

                        string Modalidad = _repModalidadCurso.FirstBy(w => w.Estado == true && w.Id == dto.Objeto.TipoId, w => new { w.Codigo }).Codigo;

                        int centroCostoTemporal;

                        if (dto.CentroCosto == null)
                            centroCostoTemporal = dto.Objeto.IdCentroCosto.Value;
                        else
                            centroCostoTemporal = CentroCostoPadre.Id;

                        var CC = _repCentroCosto.FirstBy(w => w.Estado == true && w.Id == centroCostoTemporal, y => new CentroCostoDatosDTO { Id = 0, Nombre = y.Nombre, IdPgeneral = y.IdPgeneral });
                        int Anio = 0, cont = 4;

                        while (Anio < 2000)
                            Anio = Int32.Parse(CC.Nombre.Substring((CC.IdPgeneral + ' ' + Modalidad + ' ').Trim().Length, cont++));

                        CentroCostoDatosDTO CentroCosto2 = PEspecificoPadre.ObtenerCentroCostoPEspecifico(PGeneral, Codigo, dto.Objeto.Ciudad, Anio, Modalidad, out NombrePEspecifico);

                        CentroCostoBO NuevoCentroCosto = new CentroCostoBO(contexto)
                        {
                            IdArea = CentroCosto2.IdArea,
                            IdSubArea = CentroCosto2.IdSubArea,
                            IdPgeneral = CentroCosto2.IdPgeneral,
                            Nombre = CentroCosto2.Nombre,
                            IdAreaCc = CentroCosto2.IdAreaCc,
                            Codigo = CentroCosto2.Codigo,
                            UsuarioCreacion = dto.Usuario,
                            UsuarioModificacion = dto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };

                        if (NuevoCentroCosto.HasErrors)
                            return BadRequest(NuevoCentroCosto.GetErrors(null));
                        else
                            _repCentroCosto.Insert(NuevoCentroCosto);

                        PespecificoBO ProgramaEspecifico = new PespecificoBO(contexto)
                        {
                            Nombre = NombrePEspecifico,
                            Codigo = Codigo,
                            IdCentroCosto = NuevoCentroCosto.Id,
                            EstadoP = dto.Objeto.EstadoP,
                            Tipo = dto.Objeto.Tipo,
                            TipoAmbiente = "1",
                            Categoria = dto.Objeto.Categoria,
                            IdProgramaGeneral = item.IdPGeneral_Hijo,
                            Ciudad = dto.Objeto.Ciudad,
                            CodigoBanco = "A" + NuevoCentroCosto.Id.ToString(),
                            EstadoPid = dto.Objeto.EstadoPid,
                            IdEstadoPespecifico = dto.Objeto.EstadoPid,
                            TipoId = dto.Objeto.TipoId,
                            OrigenPrograma = dto.Objeto.OrigenPrograma,
                            IdCiudad = dto.Objeto.IdCiudad,
                            Duracion = "0",
                            ActualizacionAutomatica = "0",
                            CursoIndividual = dto.Objeto.CursoIndividual,
                            IdAmbiente = dto.Objeto.IdAmbiente,
                            UsuarioCreacion = dto.Usuario,
                            UsuarioModificacion = dto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };


                        if (!_repPEspecifico.Exist(w => w.CodigoBanco == ProgramaEspecifico.CodigoBanco))
                        {
                            /* Se crean cursos en la tabla antigua solo para que permita a los encargados de finanzas    *
                             * matricular a los alumnos, cuando se reemplaze el modulo de "Procesos de Matricula" borrar *
                             * esta parte del codigo y el parametro orden  */

                            if (ProgramaEspecifico.HasErrors)
                                return BadRequest(ProgramaEspecifico.GetErrors(null));
                            else
                                _repPEspecifico.Insert(ProgramaEspecifico);
                            CursoPespecificoBO CursoPespecifico = new CursoPespecificoBO(contexto)
                            {
                                IdPespecifico = ProgramaEspecifico.Id,
                                Nombre = ProgramaEspecifico.Nombre,
                                Duracion = 1,
                                Orden = contador,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = dto.Usuario,
                                UsuarioModificacion = dto.Usuario,
                                Estado = true

                            };
                            if (CursoPespecifico.HasErrors)
                                return BadRequest(CursoPespecifico.GetErrors(null));
                            else
                                _repCursoPEspecifico.Insert(CursoPespecifico);

                            //_repPEspecifico.Update(pespecificoDependiente);
                            //ListaCentroCosto.Add(NuevoCentroCosto);
                        }
                        else
                        {
                            throw new Exception("Ya existe otro PEspecifico con este codigo Banco!");
                        }

                        PEspecificoParticipacionExpositorBO pEspecificoParticipacionExpositor = new PEspecificoParticipacionExpositorBO();
                        pEspecificoParticipacionExpositor.IdPespecifico = ProgramaEspecifico.Id;
                        pEspecificoParticipacionExpositor.Orden = contador;
                        pEspecificoParticipacionExpositor.Grupo = 1;
                        pEspecificoParticipacionExpositor.Estado = true;
                        pEspecificoParticipacionExpositor.FechaCreacion = DateTime.Now;
                        pEspecificoParticipacionExpositor.FechaModificacion = DateTime.Now;
                        pEspecificoParticipacionExpositor.UsuarioCreacion = dto.Usuario;
                        pEspecificoParticipacionExpositor.UsuarioModificacion = dto.Usuario;

                        _repPEspecificoParticipacionExpositor.Insert(pEspecificoParticipacionExpositor);

                        var PEspecificoPadre2 = _repPEspecifico.FirstBy(w => w.Estado == true && w.IdCentroCosto == CentroCostoPadre.Id);
                        PespecificoPadrePespecificoHijoBO ProgramaEspecificoPadreHijo = new PespecificoPadrePespecificoHijoBO()
                        {
                            PespecificoPadreId = PEspecificoPadre2.Id,
                            PespecificoHijoId = ProgramaEspecifico.Id,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario,
                            UsuarioModificacion = dto.Usuario,
                            Estado = true
                        };
                        _repPespecificoPadrePespecificoHijo.Insert(ProgramaEspecificoPadreHijo);

                    }


                    scope.Complete();
                }

                ResultadoProgramaEspecifico.Id = PEspecificoPadre.Id;
                ResultadoProgramaEspecifico.Nombre = dto.Objeto.Nombre;
                ResultadoProgramaEspecifico.Codigo = dto.Objeto.Codigo;
                ResultadoProgramaEspecifico.IdCentroCosto = CentroCostoPadre.Id;
                ResultadoProgramaEspecifico.EstadoP = dto.Objeto.EstadoP;
                ResultadoProgramaEspecifico.IdProgramageneral = dto.Objeto.IdProgramaGeneral;
                ResultadoProgramaEspecifico.Ciudad = dto.Objeto.Ciudad;
                ResultadoProgramaEspecifico.CursoIndividual = dto.Objeto.CursoIndividual;
                if (dto.Objeto.CursoIndividual == true)
                {
                    PEspecificoParticipacionExpositorBO pEspecificoParticipacionExpositor = new PEspecificoParticipacionExpositorBO();
                    pEspecificoParticipacionExpositor.IdPespecifico = ResultadoProgramaEspecifico.Id;
                    pEspecificoParticipacionExpositor.Orden = 1;
                    pEspecificoParticipacionExpositor.Grupo = 1;
                    pEspecificoParticipacionExpositor.Estado = true;
                    pEspecificoParticipacionExpositor.FechaCreacion = DateTime.Now;
                    pEspecificoParticipacionExpositor.FechaModificacion = DateTime.Now;
                    pEspecificoParticipacionExpositor.UsuarioCreacion = dto.Usuario;
                    pEspecificoParticipacionExpositor.UsuarioModificacion = dto.Usuario;

                    _repPEspecificoParticipacionExpositor.Insert(pEspecificoParticipacionExpositor);
                }
                return Ok(new { data = ResultadoProgramaEspecifico });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult ObtenerCronogramaParaModulo(int idPespecifico)
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(contexto);
                string rpta = _repPespecifico.ObtenerCronogramaParaModulo(idPespecifico);
                return Ok(new { data = rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [Route("[Action]/{IdPespecifico}")]
        [HttpGet]
        public ActionResult ObtenerTodoPespecificosRelacionados(int IdPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadrePespecificoHijo = new PespecificoPadrePespecificoHijoRepositorio();

                var programasEspecificosHijo = _repPespecificoPadrePespecificoHijo.ObtenerPespecificosRelacionados(IdPespecifico);
                var listaGrupos = _repPespecifico.ObtenerGruposSesiones(IdPespecifico).Select(x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
                foreach (var item in programasEspecificosHijo)
                {
                    item.ListaGrupo = listaGrupos;
                    item.ListaGrupoEdicion = _repPespecifico.ObtenerGrupoEdicionDisponible(item.Id);
                }
                return Ok(new { data = programasEspecificosHijo });

                //PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                //if (CursoIndividual.HasValue)
                //{
                //    if (CursoIndividual.Value)
                //    {
                //        var gruposIndividuales = _repPespecifico.ObtenerGruposSesionesIndividuales(PEspecificoId);
                //        return Ok(gruposIndividuales);
                //    }
                //}
                //var grupos = _repPespecifico.ObtenerGruposSesiones(PEspecificoId);
                //return Ok(grupos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult VerificarFrecuenciaByPEspecifico(int idPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoFrecuenciaRepositorio _repPespecificoFrecuencia = new PespecificoFrecuenciaRepositorio();

                return Ok(new { data = _repPespecificoFrecuencia.Exist(w => w.IdPespecifico == idPespecifico) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]/{IdPespecifico}/{CursoIndividual}/{NumeroGrupo}")]
        [HttpGet]
        public ActionResult ObtenerCronogramaSesionesPEspecifico(int IdPespecifico, bool? CursoIndividual, int NumeroGrupo)
        {

            try
            {
                //integraDBContext contexto = new integraDBContext();
                //PespecificoSesionBO Objeto = new PespecificoSesionBO();

                //return Ok(new { data = Objeto.ObtenerCronogramaSesionesPEspecifico(idPespecifico, cursoIndividual) });
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio();

                var programaEspecifico = _repPespecifico.ObtenerDatosProgramaEspecificoPorId(IdPespecifico);

                if (CursoIndividual.HasValue)
                    if (CursoIndividual.Value)
                    {//Si es un curso individual
                        var ListaCronogramaSesiones = _repPespecificoSesion.ObtenerCronogramaIndividualPorPEspecifico(programaEspecifico);
                        return Ok(ListaCronogramaSesiones);
                    }

                var sesionesPespecificoCompuesto = _repPespecificoSesion.ListaPespecificoSesioneshijos(programaEspecifico.Id);
                return Ok(sesionesPespecificoCompuesto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult VerificarFechaSesion([FromBody] FiltroVerificarFechaSesionDTO Obj)
        {

            try
            {
                integraDBContext contexto = new integraDBContext();
                PespecificoSesionBO PespecificoSesion = new PespecificoSesionBO(contexto);

                return Ok(PespecificoSesion.VerificarFecha(Obj.IdSesion, Obj.Fecha));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarFechaPorSesion([FromBody] ActualizarFechaPorSesionDTO dTO)
        {

            try
            {
                integraDBContext contexto = new integraDBContext();
                PespecificoSesionBO PespecificoSesion = new PespecificoSesionBO(contexto); ;
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio(contexto);
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(contexto);
                PEspecificoParticipacionExpositorRepositorio _repPEspecificoParticipacionExpositor = new PEspecificoParticipacionExpositorRepositorio(contexto);
                ProveedorRepositorio _repProveedor = new ProveedorRepositorio(contexto);
                PespecificoBO Pespecifico = new PespecificoBO(contexto);

                //var datosSesionAntiguo = _repPespecificoSesion;
                var PespecificoSesionExistente = _repPespecificoSesion.FirstById(dTO.SesionId);

                PespecificoSesion.Id = PespecificoSesionExistente.Id;
                PespecificoSesion.IdPespecifico = PespecificoSesionExistente.IdPespecifico;
                PespecificoSesion.FechaHoraInicio = PespecificoSesionExistente.FechaHoraInicio;
                PespecificoSesion.Duracion = PespecificoSesionExistente.Duracion;
                PespecificoSesion.IdExpositor = PespecificoSesionExistente.IdExpositor;
                PespecificoSesion.Comentario = PespecificoSesionExistente.Comentario;
                PespecificoSesion.SesionAutoGenerada = PespecificoSesionExistente.SesionAutoGenerada;
                PespecificoSesion.IdAmbiente = PespecificoSesionExistente.IdAmbiente;
                PespecificoSesion.Predeterminado = PespecificoSesionExistente.Predeterminado;
                PespecificoSesion.Estado = PespecificoSesionExistente.Estado;
                PespecificoSesion.UsuarioCreacion = PespecificoSesionExistente.UsuarioCreacion;
                PespecificoSesion.FechaCreacion = PespecificoSesionExistente.FechaCreacion;
                PespecificoSesion.UsuarioModificacion = dTO.Usuario;
                PespecificoSesion.FechaModificacion = DateTime.Now;
                PespecificoSesion.RowVersion = PespecificoSesionExistente.RowVersion;



                if (dTO.esFechaInicio)
                {
                    var PEspecificoPadre = _repPespecificoSesion.ObtenerDatosPespecificoHijoPorSesion(dTO.SesionId);


                    if (PEspecificoPadre != null) //Si es Curso Padre
                        Pespecifico = _repPespecifico.FirstById(PEspecificoPadre.PEspecificoPadreId);
                    else//Si es Curso Individual
                        Pespecifico = _repPespecifico.FirstById(PespecificoSesion.IdPespecifico);

                    Pespecifico.IdSesionInicio = PespecificoSesion.Id;
                    Pespecifico.FechaModificacion = DateTime.Now;
                    Pespecifico.UsuarioModificacion = dTO.Usuario;
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    if (dTO.esFechaInicio)
                    {
                        _repPespecifico.Update(Pespecifico);
                    }
                    if (dTO.recorrerFecha)

                        PespecificoSesion.ActualizarFechaParaSesionRecorrerFechas(dTO.Usuario);
                    else
                    {
                        PespecificoSesion.EsSesionInicio = dTO.esFechaInicio;
                        PespecificoSesion.Comentario = dTO.Comentario;
                        PespecificoSesion.FechaHoraInicio = dTO.fecha;
                        PespecificoSesion.FechaModificacion = DateTime.Now;
                        PespecificoSesion.UsuarioModificacion = dTO.Usuario;
                        _repPespecificoSesion.Update(PespecificoSesion);
                    }

                    scope.Complete();
                }

                if (dTO.SesionId == _repPespecificoSesion.ObtenerSesionInicial(PespecificoSesionExistente.IdPespecifico, PespecificoSesionExistente.Grupo))
                {
                    ProveedorBO Proveedor = new ProveedorBO();
                    if (PespecificoSesionExistente.IdProveedor != null)
                    {
                        Proveedor = _repProveedor.FirstById(PespecificoSesionExistente.IdProveedor.Value);
                    }
                    PEspecificoParticipacionExpositorBO pEspecificoParticipacionExpositorBO = new PEspecificoParticipacionExpositorBO();
                    if (_repPEspecificoParticipacionExpositor.Exist(w => w.IdPespecifico == PespecificoSesionExistente.IdPespecifico && w.Grupo == PespecificoSesionExistente.Grupo))
                    {
                        pEspecificoParticipacionExpositorBO = _repPEspecificoParticipacionExpositor.FirstBy(w => w.IdPespecifico == PespecificoSesionExistente.IdPespecifico && w.Grupo == PespecificoSesionExistente.Grupo);
                        pEspecificoParticipacionExpositorBO.IdProveedorPlanificacionGrupo = PespecificoSesionExistente.IdProveedor;
                        pEspecificoParticipacionExpositorBO.IdExpositorGrupo = PespecificoSesionExistente.IdExpositor;
                        pEspecificoParticipacionExpositorBO.ExpositorGrupo = PespecificoSesionExistente.IdProveedor == null ? null : Proveedor.Nombre1 + " " + Proveedor.Nombre2 + " " + Proveedor.ApePaterno + " " + Proveedor.ApeMaterno;
                        pEspecificoParticipacionExpositorBO.FechaModificacion = DateTime.Now;
                        pEspecificoParticipacionExpositorBO.UsuarioModificacion = dTO.Usuario;

                    }
                    else
                    {
                        pEspecificoParticipacionExpositorBO.IdPespecifico = PespecificoSesionExistente.IdPespecifico;
                        pEspecificoParticipacionExpositorBO.IdExpositorGrupo = PespecificoSesionExistente.IdExpositor;
                        pEspecificoParticipacionExpositorBO.ExpositorGrupo = PespecificoSesionExistente.IdProveedor == null ? null : Proveedor.Nombre1 + " " + Proveedor.Nombre2 + " " + Proveedor.ApePaterno + " " + Proveedor.ApeMaterno;
                        pEspecificoParticipacionExpositorBO.IdProveedorPlanificacionGrupo = PespecificoSesionExistente.IdProveedor;
                        pEspecificoParticipacionExpositorBO.Grupo = PespecificoSesionExistente.Grupo;
                        pEspecificoParticipacionExpositorBO.Estado = true;
                        pEspecificoParticipacionExpositorBO.FechaCreacion = DateTime.Now;
                        pEspecificoParticipacionExpositorBO.FechaModificacion = DateTime.Now;
                        pEspecificoParticipacionExpositorBO.UsuarioCreacion = dTO.Usuario;
                        pEspecificoParticipacionExpositorBO.UsuarioModificacion = dTO.Usuario;
                    }
                    _repPEspecificoParticipacionExpositor.Update(pEspecificoParticipacionExpositorBO);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult ValidarPespecificoTieneSesiones(int idPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio();

                return Ok(new { data = _repPespecificoSesion.Exist(w => w.IdPespecifico == idPespecifico) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult VerificarSiTienePadrePEspecifico(int idPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoPadrePespecificoHijoRepositorio _pespecificoPadrePespecificoHijoRepository = new PespecificoPadrePespecificoHijoRepositorio();
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();

                var IdPadre = _pespecificoPadrePespecificoHijoRepository.FirstBy(w => w.Estado == true && w.PespecificoHijoId == idPespecifico, y => new { y.PespecificoPadreId });

                if (IdPadre != null)
                    return Ok(new { data = _repPespecifico.FirstBy(w => w.Estado == true && w.Id == IdPadre.PespecificoPadreId, y => new { y.Nombre }) });
                else
                    return Ok(new { data = IdPadre });


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[Action]/{IdPespecifico}")]
        [HttpGet]
        public ActionResult VerificarSiPespecificoIndividual(int IdPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                var rpta = _repPespecifico.Exist(w => w.CursoIndividual == true && w.Id == IdPespecifico);

                return Ok(new { rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]/{IdPespecifico}")]
        [HttpGet]
        public ActionResult VerificarDuracionPorIdProgramaEspecifico(int IdPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();

                var duracionPespecifico = _repPespecifico.ObtenerDatosduracionPespecifico(IdPespecifico);

                bool rpta = true;

                if (duracionPespecifico.Count() == 0)
                    return Ok(new { data = rpta });

                foreach (var item in duracionPespecifico)
                {
                    if (item.Duracion == "0")
                    {
                        rpta = false;
                        break;
                    }
                }

                return Ok(new { data = rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult ObtenerDatosCompletosPespecificoPorId(int idPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();

                var rpta = _repPespecifico.ObtenerDatosCompletosPespecificoPorId(idPespecifico);

                return Ok(new { data = rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaFrecuencia(int idPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FrecuenciaRepositorio _repFrecuencia = new FrecuenciaRepositorio();

                return Ok(new { data = _repFrecuencia.ObtenerListaFrecuencia() });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// TipoFuncion: POST
        /// Autor: Jose Villena.
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera PDF Cronograma
        /// </summary>
        /// <returns>rpta<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarPDFCronograma([FromBody] FiltroObtenerPDFDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoBO Objeto = new PespecificoBO();
                integraDBContext contexto = new integraDBContext();
                PespecificoRepositorio repPespecifico = new PespecificoRepositorio(contexto);
                List<PespecificoSesionCompuestoDTO> SesionNueva = new List<PespecificoSesionCompuestoDTO>();
                //reduccion de tiempo de la vista
                dto.Sesion.ForEach(f => { f.FechaHoraInicio = f.FechaHoraInicio.AddHours(-5); });
                var rpta = Objeto.GenerarPDFCronogramaV2(dto.IdPespecifico, dto.CursoIndividual, dto.CursoNombre, dto.Usuario, dto.Sesion,dto.Grupo);
                return Ok(new { data = rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarFrecuencia([FromBody] ParametrosInsertaFrecuenciaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PespecificoFrecuenciaRepositorio _repPespecificoFrecuencia = new PespecificoFrecuenciaRepositorio(contexto);
                PespecificoFrecuenciaDetalleRepositorio _repPespecificoFrecuenciaDetalle = new PespecificoFrecuenciaDetalleRepositorio(contexto);
                PespecificoPadrePespecificoHijoRepositorio _rePespecificoPadrePespecifico = new PespecificoPadrePespecificoHijoRepositorio(contexto);
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio(contexto);
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(contexto);
                FeriadoRepositorio _repFeriado = new FeriadoRepositorio(contexto);
                FrecuenciaRepositorio _repFrecuencia = new FrecuenciaRepositorio(contexto);

                PespecificoFrecuenciaBO Frecuencia = new PespecificoFrecuenciaBO(contexto);
                PespecificoBO pespecifico_ = new PespecificoBO(contexto);
                List<PespecificoFrecuenciaDetalleBO> frecuenciaDetalle = new List<PespecificoFrecuenciaDetalleBO>();

                using (TransactionScope scope = new TransactionScope())
                {
                    pespecifico_ = _repPespecifico.FirstById(dto.IdPespecifico);

                    if (_repPespecificoFrecuencia.Exist(w => w.IdPespecifico == dto.IdPespecifico))
                    {
                        Frecuencia = _repPespecificoFrecuencia.FirstBy(w => w.IdPespecifico == dto.IdPespecifico);

                        pespecifico_.IdSesionInicio = null;

                        Frecuencia.FechaInicio = dto.FechaInicio;
                        Frecuencia.FechaFin = dto.FechaFin;
                        Frecuencia.Frecuencia = -1;
                        Frecuencia.NroSesiones = dto.listaDetalles.Count;
                        Frecuencia.IdFrecuencia = dto.IdFrecuencia;
                        Frecuencia.FechaModificacion = DateTime.Now;
                        Frecuencia.UsuarioModificacion = dto.Usuario;
                        _repPespecificoFrecuencia.Update(Frecuencia);

                        var listaDetalle = _repPespecificoFrecuenciaDetalle.ObtenerFrecuenciaDetallePorIdPespecificoFrecuencia(Frecuencia.Id);

                        ////eliminamos los detalles de la frecuencia

                        var frecuenciaDetalleEliminar = listaDetalle.Where(w => w.IdPespecificoFrecuencia == Frecuencia.Id).Select(W => W.Id).ToList();

                        _repPespecificoFrecuenciaDetalle.Delete(frecuenciaDetalleEliminar, dto.Usuario);


                        //eliminamos las sesiones de todos los pespecificos hijos(su estado cambia a 0)
                        var listaCursos = _rePespecificoPadrePespecifico.ObtenerPespecificosHijos(dto.IdPespecifico);

                        //tiene cursos asociados
                        List<int> IdSesiones = new List<int>();
                        if (listaCursos.Count != 0)
                        {
                            foreach (var hijo in listaCursos)
                            {
                                var lista_sesiones = _repPespecificoSesion.ListaPespecificoSesiones(hijo.PEspecificoHijoId);

                                foreach (var sesion in lista_sesiones)
                                {
                                    IdSesiones.Add(sesion);
                                }
                            }

                        }
                        //es individual
                        else
                        {
                            var especifio = listaCursos.FirstOrDefault();
                            //var lista_sesiones_ind = _repPespecificoSesion.ListaPespecificoSesiones(especifio.PEspecificoHijoId);
                            var lista_sesiones_ind = _repPespecificoSesion.ListaPespecificoSesiones(dto.IdPespecifico);

                            foreach (var sesion in lista_sesiones_ind)
                            {
                                IdSesiones.Add(sesion);
                            }
                        }
                        _repPespecificoSesion.Delete(IdSesiones, dto.Usuario);
                    }
                    else
                    {
                        Frecuencia = new PespecificoFrecuenciaBO();

                        Frecuencia.IdPespecifico = dto.IdPespecifico;
                        Frecuencia.FechaInicio = dto.FechaInicio;
                        Frecuencia.FechaFin = dto.FechaFin;
                        //Frecuencia.FechaFin = dto.FechaFin;
                        Frecuencia.Frecuencia = -1;
                        Frecuencia.NroSesiones = dto.listaDetalles.Count;
                        Frecuencia.IdFrecuencia = dto.IdFrecuencia;
                        Frecuencia.Estado = true;
                        Frecuencia.FechaCreacion = DateTime.Now;
                        Frecuencia.FechaModificacion = DateTime.Now;
                        Frecuencia.UsuarioCreacion = dto.Usuario;
                        Frecuencia.UsuarioModificacion = dto.Usuario;
                        _repPespecificoFrecuencia.Insert(Frecuencia);
                    }
                    //se insertan nuevos detalles
                    foreach (var detalle in dto.listaDetalles)
                    {
                        PespecificoFrecuenciaDetalleBO frecuDetalle = new PespecificoFrecuenciaDetalleBO();

                        frecuDetalle.IdPespecificoFrecuencia = Frecuencia.Id;
                        frecuDetalle.DiaSemana = detalle.Dia.Value;
                        frecuDetalle.HoraDia = detalle.HoraDia.Value;
                        frecuDetalle.Duracion = detalle.Duracion.Value;
                        frecuDetalle.Estado = true;
                        frecuDetalle.FechaCreacion = DateTime.Now;
                        frecuDetalle.FechaModificacion = DateTime.Now;
                        frecuDetalle.UsuarioCreacion = dto.Usuario;
                        frecuDetalle.UsuarioModificacion = dto.Usuario;

                        //frecuenciaDetalle.Add(frecuDetalle);
                        _repPespecificoFrecuenciaDetalle.Insert(frecuDetalle);
                    }

                    var ListaCursos = _rePespecificoPadrePespecifico.ObtenerInformacionPespecificosHijos(dto.IdPespecifico);
                    var FrecuenciaPespecifico = _repPespecificoFrecuencia.ObtenerPespecificoFrecuencia(dto.IdPespecifico).FirstOrDefault();
                    var FrecuenciaDetalle = _repPespecificoFrecuenciaDetalle.ObtenerFrecuenciaDetallePorIdPespecificoFrecuencia(FrecuenciaPespecifico.Id);
                    var ListaFeriados = _repFeriado.ObtenerFeriadoPorTipo(0);
                    var FrecuenciaGeneral = _repFrecuencia.ObtenerFrecuencia(FrecuenciaPespecifico.IdFrecuencia.Value);

                    DateTime fechaAsignar = Frecuencia.FechaInicio;
                    List<PespecificoSesionBO> ListaSesiones = new List<PespecificoSesionBO>();
                    List<EsquemaSesionesDTO> estructuraSesiones = new List<EsquemaSesionesDTO>();
                    byte[] diasFrecuencia = FrecuenciaDetalle.Select(s => s.DiaSemana).ToArray();

                    int cont = 0;

                    if (ListaCursos.Count != 0) //Si tiene cursos asociados
                    {

                        foreach (var curso in ListaCursos)
                        {
                            if (dto.ListaPEspecificos.Contains(curso.Id)) {
                                decimal Duracion = Convert.ToDecimal(curso.Duracion);
                                while (Duracion > 0)
                                {
                                    EsquemaSesionesDTO esquemaSesion = new EsquemaSesionesDTO();
                                    esquemaSesion.Curso = curso;
                                    esquemaSesion.Duracion = Duracion - FrecuenciaDetalle[cont].Duracion < 0 ? Duracion : FrecuenciaDetalle[cont].Duracion;
                                    esquemaSesion.Dia = FrecuenciaDetalle[cont].DiaSemana;

                                    estructuraSesiones.Add(esquemaSesion);

                                    Duracion = Duracion - FrecuenciaDetalle[cont].Duracion;
                                    cont = (cont + 1) % diasFrecuencia.Length;
                                }
                            }
                        }
                    }
                    else // Si es un curso individual
                    {
                        DatosListaPespecificoDTO datosPespecifico = _repPespecifico.ObtenerDatosCompletosPespecificoPorId(dto.IdPespecifico);

                        ListainformacionProgramaEspecificoHijoDTO PEspecificoIndividual = new ListainformacionProgramaEspecificoHijoDTO();

                        PEspecificoIndividual.Id = datosPespecifico.Id;
                        PEspecificoIndividual.Nombre = datosPespecifico.Nombre;
                        PEspecificoIndividual.Duracion = datosPespecifico.Duracion;
                        PEspecificoIndividual.IdCiudad = datosPespecifico.IdCiudad;
                        PEspecificoIndividual.TipoAmbiente = datosPespecifico.TipoAmbiente;
                        PEspecificoIndividual.IdAmbiente = datosPespecifico.IdAmbiente;
                        PEspecificoIndividual.IdProgramaGeneral = datosPespecifico.IdProgramaGeneral;
                        PEspecificoIndividual.IdModalidadCurso = datosPespecifico.TipoId;

                        decimal Duracion = Convert.ToDecimal(PEspecificoIndividual.Duracion);

                        //var tipoPrograma = _repPespecifico.ObtenerTipoProgramaPEspecifico(dto.IdPespecifico);

                        /*if (tipoPrograma.IdTipoPrograma == 3)
                        {
                            for (DateTime i = dto.FechaInicio; i < dto.FechaFin; i = i.AddDays(1))
                            {
                                EsquemaSesionesDTO esquemaSesion = new EsquemaSesionesDTO();
                                esquemaSesion.Curso = PEspecificoIndividual;
                                esquemaSesion.Duracion = Duracion;
                                esquemaSesion.Dia = FrecuenciaDetalle[cont].DiaSemana;
                                estructuraSesiones.Add(esquemaSesion);
                                cont = (cont + 1) % diasFrecuencia.Length;
                            }
                        }*/
                        //else
                        //{
                            while (Duracion > 0)
                            {
                                EsquemaSesionesDTO esquemaSesion = new EsquemaSesionesDTO();
                                esquemaSesion.Curso = PEspecificoIndividual;
                                esquemaSesion.Duracion = Duracion - FrecuenciaDetalle[cont].Duracion < 0 ? Duracion : FrecuenciaDetalle[cont].Duracion;
                                esquemaSesion.Dia = FrecuenciaDetalle[cont].DiaSemana;

                                estructuraSesiones.Add(esquemaSesion);

                                Duracion = Duracion - FrecuenciaDetalle[cont].Duracion;
                                cont = (cont + 1) % diasFrecuencia.Length;
                            }
                        //}
                        
                    }


                    //Se obtiene las horas de las sesiones
                    DateTime fechaTemp = fechaAsignar;
                    //DateTime fechaTemp = fechaAsignar;
                    PespecificoSesionBO sesionAnterior = null;
                    int countGrupoSesiones = 1;
                    for (int i = 0; i < estructuraSesiones.Count; i++)
                    {
                        PespecificoSesionBO pespecificoSesion = new PespecificoSesionBO();

                        if (i % diasFrecuencia.Length == 0 && i != 0)
                        {
                            fechaAsignar = fechaTemp.AddDays(FrecuenciaGeneral.NumDias.Value);
                            fechaTemp = fechaAsignar;
                        }

                        fechaAsignar = pespecificoSesion.ObtenerFechaAsignar(estructuraSesiones[i].Curso, fechaAsignar, estructuraSesiones[i].Dia.Value, diasFrecuencia, ListaFeriados);
                        fechaAsignar = fechaAsignar.Date + FrecuenciaDetalle[i % diasFrecuencia.Length].HoraDia;
                        estructuraSesiones[i].FechaAsignar = fechaAsignar;

                        //Se inserta la sesion


                        pespecificoSesion.IdPespecifico = estructuraSesiones[i].Curso.Id;
                        pespecificoSesion.FechaHoraInicio = estructuraSesiones[i].FechaAsignar.Value;
                        pespecificoSesion.Duracion = estructuraSesiones[i].Duracion.Value;
                        pespecificoSesion.IdAmbiente = estructuraSesiones[i].Curso.IdAmbiente;
                        pespecificoSesion.IdModalidadCurso = estructuraSesiones[i].Curso.IdModalidadCurso;
                        pespecificoSesion.SesionAutoGenerada = true;
                        

                        if (ListaCursos.Count == 0)//si es curso individual se guarda el expositor del curso en la sesion
                            pespecificoSesion.IdExpositor = estructuraSesiones[i].Curso.IdExpositor_Referencia;
                        //271117 - para que arme la duracion y el hoario como con los que tienen esete flag activo
                        pespecificoSesion.Grupo = 1;
                        pespecificoSesion.Predeterminado = true;
                        if (sesionAnterior == null)
                        {
                            pespecificoSesion.GrupoSesion = countGrupoSesiones;
                        }
                        if (sesionAnterior != null && sesionAnterior.IdPespecifico != pespecificoSesion.IdPespecifico)
                        {
                            countGrupoSesiones = 1;
                            pespecificoSesion.GrupoSesion = countGrupoSesiones;
                        }
                        if (sesionAnterior != null && sesionAnterior.IdPespecifico == pespecificoSesion.IdPespecifico)
                        {
                            TimeSpan t = pespecificoSesion.FechaHoraInicio - sesionAnterior.FechaHoraInicio;
                            if (t.Days > 1)
                            {
                                countGrupoSesiones++;
                            }
                            pespecificoSesion.GrupoSesion = countGrupoSesiones;
                        }

                        sesionAnterior = pespecificoSesion;


                        pespecificoSesion.Estado = true;
                        pespecificoSesion.FechaCreacion = DateTime.Now;
                        pespecificoSesion.FechaModificacion = DateTime.Now;
                        pespecificoSesion.UsuarioCreacion = dto.Usuario;
                        pespecificoSesion.UsuarioModificacion = dto.Usuario;

                        ListaSesiones.Add(pespecificoSesion);
                        //_repPespecificoSesion.Insert(sesion);

                        //la primera sesion sera fecha de inicio y se inserta en el PEspecifico

                    }

                    _repPespecificoSesion.Insert(ListaSesiones);
                    if (ListaSesiones.Count > 0)
                    {
                        pespecifico_.IdSesionInicio = ListaSesiones[0].Id;
                        pespecifico_.FechaModificacion = DateTime.Now;
                        pespecifico_.UsuarioModificacion = dto.Usuario;

                        _repPespecifico.Update(pespecifico_);
                    }

                    if (dto.CheckTiempoFrecuencia == true || dto.CheckEnvioCorreo == true || dto.CheckEnvioWhatsApp == true || dto.CheckEnvioCorreoConfirmacion == true || dto.CheckEnvioCorreoDocente == true)
                    {
                        var listado = _repPespecifico.ObtenerConfiguracionWebinarPEspecifico(dto.IdPespecifico);
                        if(listado == null)
                        {
                            var resultado = _repPespecifico.InsertarFrecuenciaWebinar(dto.IdPespecifico, dto.IdTiempoFrecuencia == (int?)null ? 0 : dto.IdTiempoFrecuencia.Value, dto.ValorTiempoFrecuencia == (int?)null ? 0 : dto.ValorTiempoFrecuencia.Value, dto.IdTiempoFrecuenciaCorreo == (int?)null ? 0 : dto.IdTiempoFrecuenciaCorreo.Value, dto.ValorFrecuenciaCorreo == (int?)null ? 0 : dto.ValorFrecuenciaCorreo.Value, dto.IdTiempoFrecuenciaWhatsapp == (int?)null ? 0 : dto.IdTiempoFrecuenciaWhatsapp.Value, dto.ValorFrecuenciaWhatsapp == (int?)null ? 0 : dto.ValorFrecuenciaWhatsapp.Value, dto.IdPlantillaFrecuenciaCorreo == (int?)null ? 0 : dto.IdPlantillaFrecuenciaCorreo.Value, dto.IdPlantillaFrecuenciaWhatsapp == (int?)null ? 0 : dto.IdPlantillaFrecuenciaWhatsapp.Value, dto.IdTiempoFrecuenciaCorreoConfirmacion == (int?)null ? 0 : dto.IdTiempoFrecuenciaCorreoConfirmacion.Value, dto.ValorFrecuenciaCorreoConfirmacion == (int?)null ? 0 : dto.ValorFrecuenciaCorreoConfirmacion.Value, dto.IdPlantillaCorreoConfirmacion == (int?)null ? 0 : dto.IdPlantillaCorreoConfirmacion.Value, dto.IdTiempoFrecuenciaCorreoDocente == (int?)null ? 0 : dto.IdTiempoFrecuenciaCorreoDocente.Value, dto.ValorFrecuenciaDocente == (int?)null ? 0 : dto.ValorFrecuenciaDocente.Value, dto.IdPlantillaDocente == (int?)null ? 0 : dto.IdPlantillaDocente.Value);
                        }
                        else
                        {
                            var resultado = _repPespecifico.EliminarFrecuenciaWebinar(dto.IdPespecifico);
                            resultado = _repPespecifico.InsertarFrecuenciaWebinar(dto.IdPespecifico, dto.IdTiempoFrecuencia == (int?)null ? 0 : dto.IdTiempoFrecuencia.Value, dto.ValorTiempoFrecuencia == (int?)null ? 0 : dto.ValorTiempoFrecuencia.Value, dto.IdTiempoFrecuenciaCorreo == (int?)null ? 0 : dto.IdTiempoFrecuenciaCorreo.Value, dto.ValorFrecuenciaCorreo == (int?)null ? 0 : dto.ValorFrecuenciaCorreo.Value, dto.IdTiempoFrecuenciaWhatsapp == (int?)null ? 0 : dto.IdTiempoFrecuenciaWhatsapp.Value, dto.ValorFrecuenciaWhatsapp == (int?)null ? 0 : dto.ValorFrecuenciaWhatsapp.Value, dto.IdPlantillaFrecuenciaCorreo == (int?)null ? 0 : dto.IdPlantillaFrecuenciaCorreo.Value, dto.IdPlantillaFrecuenciaWhatsapp == (int?)null ? 0 : dto.IdPlantillaFrecuenciaWhatsapp.Value, dto.IdTiempoFrecuenciaCorreoConfirmacion == (int?)null ? 0 : dto.IdTiempoFrecuenciaCorreoConfirmacion.Value, dto.ValorFrecuenciaCorreoConfirmacion == (int?)null ? 0 : dto.ValorFrecuenciaCorreoConfirmacion.Value, dto.IdPlantillaCorreoConfirmacion == (int?)null ? 0 : dto.IdPlantillaCorreoConfirmacion.Value, dto.IdTiempoFrecuenciaCorreoDocente == (int?)null ? 0 : dto.IdTiempoFrecuenciaCorreoDocente.Value, dto.ValorFrecuenciaDocente == (int?)null ? 0 : dto.ValorFrecuenciaDocente.Value, dto.IdPlantillaDocente == (int?)null ? 0 : dto.IdPlantillaDocente.Value);
                        }
                        
                    }
                    else
                    {
                        var resultado = _repPespecifico.EliminarFrecuenciaWebinar(dto.IdPespecifico);
                    }

                    scope.Complete();

                    return Ok(true);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Carlos Crispin
        /// Fecha: 14/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Modifica la frecuencia de las sesiones en vivo
        /// </summary>
        /// <returns>Response 200 con booleano, caso contrario response 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult modificarFrecuencia([FromBody] ParametrosInsertaFrecuenciaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PespecificoFrecuenciaRepositorio _repPespecificoFrecuencia = new PespecificoFrecuenciaRepositorio(contexto);
                PespecificoFrecuenciaDetalleRepositorio _repPespecificoFrecuenciaDetalle = new PespecificoFrecuenciaDetalleRepositorio(contexto);
                PespecificoPadrePespecificoHijoRepositorio _rePespecificoPadrePespecifico = new PespecificoPadrePespecificoHijoRepositorio(contexto);
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio(contexto);
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(contexto);
                FeriadoRepositorio _repFeriado = new FeriadoRepositorio(contexto);
                FrecuenciaRepositorio _repFrecuencia = new FrecuenciaRepositorio(contexto);

                PespecificoFrecuenciaBO frecuenciaBO = new PespecificoFrecuenciaBO(contexto);
                PespecificoBO pespecifico_ = new PespecificoBO(contexto);
                List<PespecificoFrecuenciaDetalleBO> frecuenciaDetalle = new List<PespecificoFrecuenciaDetalleBO>();

                using (TransactionScope scope = new TransactionScope())
                {
                    pespecifico_ = _repPespecifico.FirstById(dto.IdPespecifico);

                    if (_repPespecificoFrecuencia.Exist(w => w.IdPespecifico == dto.IdPespecifico))
                    {
                        //tiene cursos asociados
                        List<int> IdSesiones = new List<int>();

                        foreach (var hijo in dto.ListaPEspecificos)
                        {
                            var lista_sesiones = _repPespecificoSesion.ListaPespecificoSesiones(hijo);

                            foreach (var sesion in lista_sesiones)
                            {
                                IdSesiones.Add(sesion);
                            }
                        }

                        _repPespecificoSesion.Delete(IdSesiones, dto.Usuario);
                    }
                    else
                    {
                        frecuenciaBO = new PespecificoFrecuenciaBO();

                        frecuenciaBO.IdPespecifico = dto.IdPespecifico;
                        frecuenciaBO.FechaInicio = dto.FechaInicio;
                        frecuenciaBO.Frecuencia = -1;
                        frecuenciaBO.NroSesiones = dto.listaDetalles.Count;
                        frecuenciaBO.IdFrecuencia = dto.IdFrecuencia;
                        frecuenciaBO.Estado = true;
                        frecuenciaBO.FechaCreacion = DateTime.Now;
                        frecuenciaBO.FechaModificacion = DateTime.Now;
                        frecuenciaBO.UsuarioCreacion = dto.Usuario;
                        frecuenciaBO.UsuarioModificacion = dto.Usuario;
                        _repPespecificoFrecuencia.Insert(frecuenciaBO);
                    }

                    var ListaCursos = _rePespecificoPadrePespecifico.ObtenerInformacionPespecificoSesion(dto.ListaPEspecificos[0]);
                    var ListaFeriados = _repFeriado.ObtenerFeriadoPorTipo(0);

                    DateTime fechaAsignar = dto.FechaInicio;
                    List<PespecificoSesionBO> ListaSesiones = new List<PespecificoSesionBO>();
                    List<EsquemaSesionesDTO> estructuraSesiones = new List<EsquemaSesionesDTO>();
                    byte[] diasFrecuencia = dto.listaDetalles.Select(s => s.Dia.Value).ToArray();

                    int cont = 0;

                    if (ListaCursos.Count != 0) // Si tiene cursos asociados
                    {

                        foreach (var curso in ListaCursos)
                        {
                            if (dto.ListaPEspecificos.Contains(curso.Id)) {
                                decimal Duracion = Convert.ToDecimal(curso.Duracion);
                                while (Duracion > 0)
                                {
                                    EsquemaSesionesDTO esquemaSesion = new EsquemaSesionesDTO();
                                    esquemaSesion.Curso = curso;
                                    esquemaSesion.Duracion = Duracion - dto.listaDetalles[cont].Duracion < 0 ? Duracion : dto.listaDetalles[cont].Duracion;
                                    esquemaSesion.Dia = dto.listaDetalles[cont].Dia;

                                    estructuraSesiones.Add(esquemaSesion);

                                    Duracion = Duracion - dto.listaDetalles[cont].Duracion.Value;
                                    cont = (cont + 1) % diasFrecuencia.Length;
                                }
                            }
                        }
                    }



                    //Se obtiene las horas de las sesiones
                    DateTime fechaTemp = fechaAsignar;
                    PespecificoSesionBO sesionAnterior = null;
                    int countGrupoSesiones = 1;
                    PespecificoBO pespecificohijo = new PespecificoBO();
                    for (int i = 0; i < estructuraSesiones.Count; i++)
                    {
                        PespecificoSesionBO pespecificoSesion = new PespecificoSesionBO();

                        if (i % diasFrecuencia.Length == 0 && i != 0)
                        {
                            fechaAsignar = fechaTemp.AddDays(dto.listaDetalles.Count);
                            fechaTemp = fechaAsignar;
                        }

                        fechaAsignar = pespecificoSesion.ObtenerFechaAsignar(estructuraSesiones[i].Curso, fechaAsignar, estructuraSesiones[i].Dia.Value, diasFrecuencia, ListaFeriados);
                        fechaAsignar = fechaAsignar.Date + dto.listaDetalles[i % diasFrecuencia.Length].HoraDia.Value;
                        estructuraSesiones[i].FechaAsignar = fechaAsignar;

                        //Se inserta la sesion
                        if (pespecificohijo.Id != estructuraSesiones[i].Curso.Id)
                        {
                            pespecificohijo = _repPespecifico.FirstById(estructuraSesiones[i].Curso.Id);
                        }
                        
                        pespecificoSesion.IdPespecifico = estructuraSesiones[i].Curso.Id;
                        pespecificoSesion.FechaHoraInicio = estructuraSesiones[i].FechaAsignar.Value;
                        pespecificoSesion.Duracion = estructuraSesiones[i].Duracion.Value;
                        pespecificoSesion.IdAmbiente = estructuraSesiones[i].Curso.IdAmbiente;
                        pespecificoSesion.IdModalidadCurso = pespecificohijo.TipoId;
                        pespecificoSesion.SesionAutoGenerada = true;

                        if (ListaCursos.Count == 0)//si es curso individual se guarda el expositor del curso en la sesion
                            pespecificoSesion.IdExpositor = estructuraSesiones[i].Curso.IdExpositor_Referencia;
                        //271117 - para que arme la duracion y el hoario como con los que tienen esete flag activo
                        pespecificoSesion.Grupo = 1;
                        pespecificoSesion.Predeterminado = true;
                        if (sesionAnterior == null)
                        {
                            pespecificoSesion.GrupoSesion = countGrupoSesiones;
                        }
                        if (sesionAnterior != null && sesionAnterior.IdPespecifico != pespecificoSesion.IdPespecifico)
                        {
                            countGrupoSesiones = 1;
                            pespecificoSesion.GrupoSesion = countGrupoSesiones;
                        }
                        if (sesionAnterior != null && sesionAnterior.IdPespecifico == pespecificoSesion.IdPespecifico)
                        {
                            TimeSpan t = pespecificoSesion.FechaHoraInicio - sesionAnterior.FechaHoraInicio;
                            if (t.Days > 1)
                            {
                                countGrupoSesiones++;
                            }
                            pespecificoSesion.GrupoSesion = countGrupoSesiones;
                        }

                        sesionAnterior = pespecificoSesion;


                        pespecificoSesion.Estado = true;
                        pespecificoSesion.FechaCreacion = DateTime.Now;
                        pespecificoSesion.FechaModificacion = DateTime.Now;
                        pespecificoSesion.UsuarioCreacion = dto.Usuario;
                        pespecificoSesion.UsuarioModificacion = dto.Usuario;

                        ListaSesiones.Add(pespecificoSesion);
                        //_repPespecificoSesion.Insert(sesion);

                        //la primera sesion sera fecha de inicio y se inserta en el PEspecifico

                    }

                    _repPespecificoSesion.Insert(ListaSesiones);
                    if (ListaSesiones.Count > 0)
                    {
                        pespecifico_.IdSesionInicio = ListaSesiones[0].Id;
                        pespecifico_.FechaModificacion = DateTime.Now;
                        pespecifico_.UsuarioModificacion = dto.Usuario;

                        _repPespecifico.Update(pespecifico_);
                    }

                    if (dto.CheckTiempoFrecuencia == true || dto.CheckEnvioCorreo == true || dto.CheckEnvioWhatsApp == true || dto.CheckEnvioCorreoConfirmacion == true)
                    {
                        var resultadoEliminado = _repPespecifico.EliminarFrecuenciaWebinar(dto.IdPespecifico, dto.Usuario);
                        var resultado = _repPespecifico.InsertarFrecuenciaWebinar(dto.IdPespecifico, dto.IdTiempoFrecuencia == (int?)null ? 0 : dto.IdTiempoFrecuencia.Value, dto.ValorTiempoFrecuencia == (int?)null ? 0 : dto.ValorTiempoFrecuencia.Value, dto.IdTiempoFrecuenciaCorreo == (int?)null ? 0 : dto.IdTiempoFrecuenciaCorreo.Value, dto.ValorFrecuenciaCorreo == (int?)null ? 0 : dto.ValorFrecuenciaCorreo.Value, dto.IdTiempoFrecuenciaWhatsapp == (int?)null ? 0 : dto.IdTiempoFrecuenciaWhatsapp.Value, dto.ValorFrecuenciaWhatsapp == (int?)null ? 0 : dto.ValorFrecuenciaWhatsapp.Value, dto.IdPlantillaFrecuenciaCorreo == (int?)null ? 0 : dto.IdPlantillaFrecuenciaCorreo.Value, dto.IdPlantillaFrecuenciaWhatsapp == (int?)null ? 0 : dto.IdPlantillaFrecuenciaWhatsapp.Value, dto.IdTiempoFrecuenciaCorreoConfirmacion == (int?)null ? 0 : dto.IdTiempoFrecuenciaCorreoConfirmacion.Value, dto.ValorFrecuenciaCorreoConfirmacion == (int?)null ? 0 : dto.ValorFrecuenciaCorreoConfirmacion.Value,dto.IdPlantillaCorreoConfirmacion == (int?)null ? 0 : dto.IdPlantillaCorreoConfirmacion.Value, dto.IdTiempoFrecuenciaCorreoDocente == (int?)null ? 0 : dto.IdTiempoFrecuenciaCorreoDocente.Value, dto.ValorFrecuenciaDocente == (int?)null ? 0 : dto.ValorFrecuenciaDocente.Value, dto.IdPlantillaDocente == (int?)null ? 0 : dto.IdPlantillaDocente.Value);
                    }
                    else
                    {
                        var resultado = _repPespecifico.EliminarFrecuenciaWebinar(dto.IdPespecifico);
                    }
                    
                    scope.Complete();

                    return Ok(true);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarDuracionInsertarSesion([FromBody] InformacionPespecificoSesionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio();

                PespecificoBO pEspecifico;

                pEspecifico = _repPespecifico.FirstById(dto.IdPespecifico);
                pEspecifico.Duracion = (Convert.ToDecimal(pEspecifico.Duracion) + dto.Duracion).ToString();
                pEspecifico.FechaModificacion = DateTime.Now;
                pEspecifico.UsuarioModificacion = dto.Usuario;


                PespecificoSesionBO especificoSesion = new PespecificoSesionBO();
                especificoSesion.IdPespecifico = dto.IdPespecifico;
                especificoSesion.FechaHoraInicio = dto.FechaHoraInicio.AddHours(-5);
                especificoSesion.Duracion = dto.Duracion;
                especificoSesion.IdExpositor = dto.IdPespecifico;
                especificoSesion.IdAmbiente = dto.IdAmbiente;
                especificoSesion.Comentario = dto.Comentario;
                especificoSesion.SesionAutoGenerada = dto.SesionAutoGenerada;
                especificoSesion.Grupo = dto.Grupo;
                especificoSesion.Version = 0;
                especificoSesion.IdModalidadCurso = pEspecifico.TipoId;
                especificoSesion.Estado = true;
                especificoSesion.FechaCreacion = DateTime.Now;
                especificoSesion.FechaModificacion = DateTime.Now;
                especificoSesion.UsuarioCreacion = dto.Usuario;
                especificoSesion.UsuarioModificacion = dto.Usuario;

                var estado = false;
                _repPespecifico.Update(pEspecifico);
                estado = _repPespecificoSesion.Insert(especificoSesion);

                return Ok(estado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]/{usuario}")]
        [HttpPost]
        public ActionResult InsertarEventoEspecial(string usuario, [FromBody] FiltroSesionEspecialDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                PgeneralRepositorio _repPgeneral = new PgeneralRepositorio();
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                PespecificoPadrePespecificoHijoRepositorio _repPadrePespecificoHijoRepositorio = new PespecificoPadrePespecificoHijoRepositorio();
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio();
                PespecificoBO pespecifico;

                pespecifico = _repPespecifico.FirstById(dto.PEspecificoPadreId);

                ProgramaGeneralTroncalDTO pgeneral = _repPgeneral.ObtenerProgramaGeneralParaPespecificoPorId(pespecifico.IdProgramaGeneral.Value);

                string codigo = "000000";

                string CentroCostoCodigoUltimo = _repCentroCosto.ObtenerUltimoCentroCostoPorCodigo(codigo);

                if (CentroCostoCodigoUltimo == null)
                    codigo = codigo + "001";
                else
                    codigo = (Int32.Parse(CentroCostoCodigoUltimo) + 1).ToString();

                CentroCostoBO centroCostoNuevo = new CentroCostoBO();
                centroCostoNuevo.IdArea = pgeneral.IdArea;
                centroCostoNuevo.IdSubArea = pgeneral.IdSubArea;
                centroCostoNuevo.IdPgeneral = pgeneral.Codigo;
                centroCostoNuevo.Nombre = "Evento Especial";
                centroCostoNuevo.Codigo = codigo;
                centroCostoNuevo.IdAreaCc = "0-0";
                centroCostoNuevo.Estado = true;
                centroCostoNuevo.FechaCreacion = DateTime.Now;
                centroCostoNuevo.FechaModificacion = DateTime.Now;
                centroCostoNuevo.UsuarioCreacion = usuario;
                centroCostoNuevo.UsuarioModificacion = usuario;

                PespecificoBO especificoNuevo = new PespecificoBO();
                especificoNuevo.Nombre = "Sesion Especial";
                especificoNuevo.Codigo = centroCostoNuevo.Codigo;
                especificoNuevo.EstadoP = pespecifico.EstadoP;
                especificoNuevo.Tipo = pespecifico.Tipo;
                especificoNuevo.TipoAmbiente = "1";
                especificoNuevo.Categoria = pespecifico.Categoria;
                especificoNuevo.IdProgramaGeneral = pespecifico.IdProgramaGeneral;
                especificoNuevo.Ciudad = pespecifico.Ciudad;

                especificoNuevo.EstadoPid = pespecifico.EstadoPid;
                especificoNuevo.TipoId = pespecifico.TipoId;
                especificoNuevo.OrigenPrograma = pespecifico.OrigenPrograma;
                especificoNuevo.IdCiudad = pespecifico.IdCiudad;
                especificoNuevo.Duracion = dto.Duracion.ToString();
                especificoNuevo.ActualizacionAutomatica = pespecifico.ActualizacionAutomatica;
                especificoNuevo.CursoIndividual = true;
                especificoNuevo.IdAmbiente = pespecifico.IdAmbiente;
                especificoNuevo.Estado = true;
                especificoNuevo.FechaCreacion = DateTime.Now;
                especificoNuevo.FechaModificacion = DateTime.Now;
                especificoNuevo.UsuarioCreacion = usuario;
                especificoNuevo.UsuarioModificacion = usuario;

                _repCentroCosto.Insert(centroCostoNuevo);

                especificoNuevo.CodigoBanco = "SesEs" + centroCostoNuevo.Id;
                especificoNuevo.IdCentroCosto = centroCostoNuevo.Id;

                _repPespecifico.Insert(especificoNuevo);

                PespecificoPadrePespecificoHijoBO dtoPadreHijo = new PespecificoPadrePespecificoHijoBO();

                dtoPadreHijo.PespecificoPadreId = dto.PEspecificoPadreId;
                dtoPadreHijo.PespecificoHijoId = especificoNuevo.Id;
                dtoPadreHijo.Estado = true;
                dtoPadreHijo.FechaCreacion = DateTime.Now;
                dtoPadreHijo.FechaModificacion = DateTime.Now;
                dtoPadreHijo.UsuarioCreacion = usuario;
                dtoPadreHijo.UsuarioModificacion = usuario;

                _repPadrePespecificoHijoRepositorio.Insert(dtoPadreHijo);

                PespecificoSesionBO dtoSesion = new PespecificoSesionBO();

                dtoSesion.IdPespecifico = especificoNuevo.Id;
                dtoSesion.FechaHoraInicio = dto.Fecha;
                dtoSesion.Duracion = dto.Duracion;
                dtoSesion.SesionAutoGenerada = false;
                dtoSesion.Grupo = dto.Grupo;

                dtoSesion.Estado = true;
                dtoSesion.FechaCreacion = DateTime.Now;
                dtoSesion.FechaModificacion = DateTime.Now;
                dtoSesion.UsuarioCreacion = usuario;
                dtoSesion.UsuarioModificacion = usuario;

                var estado = false;
                estado = _repPespecificoSesion.Insert(dtoSesion);
                return Ok(estado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerProgramaEspecificoAutocomplete([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                    return Ok(_repPEspecifico.GetBy(x => x.Nombre.Contains(Valor["filtro"]), x => new { x.Id, Nombre = x.Nombre }));

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

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarDocenteAmbienteProgramaEspecifico([FromBody] DocenteAmbientePEspecificoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repProgramaEspecifico = new PespecificoRepositorio();
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio();
                PEspecificoParticipacionExpositorRepositorio _repPEspecificoParticipacionExpositor = new PEspecificoParticipacionExpositorRepositorio();
                ProveedorRepositorio _repProveedor = new ProveedorRepositorio();
                var fechasSesiones = _repPespecificoSesion.GetBy(x => x.IdPespecifico == dto.Id, x => new PEspecificoSesionFechasDTO { Id = x.Id, IdPEspecifico = x.IdPespecifico, FechaHoraInicio = x.FechaHoraInicio }).ToList();
                var fechasCruces = _repProgramaEspecifico.ValidarFechaExpositorCruce(dto, fechasSesiones);
                bool estadoCruce = false;
                if (fechasCruces.Count != 0)
                {
                    estadoCruce = true;
                    return Ok(new { EstadoCruce = estadoCruce, Cruces = fechasCruces });
                }
                else
                {
                    var programaEspecifico = _repProgramaEspecifico.FirstById(dto.Id);
                    var listaSesiones = _repPespecificoSesion.GetBy(x => x.IdPespecifico == dto.Id && x.Grupo == 1);

                    if (dto.Duracion != null && dto.Duracion != "")
                        programaEspecifico.Duracion = dto.Duracion;
                    if (dto.IdAmbiente != null)
                        programaEspecifico.IdAmbiente = dto.IdAmbiente;
                    if (dto.IdExpositor_Referencia != null)
                        programaEspecifico.IdExpositorReferencia = dto.IdExpositor_Referencia;
                    if (dto.IdEstadoPEspecifico != null)
                    {
                        EstadoPespecificoRepositorio _repoEstadoPespecifico = new EstadoPespecificoRepositorio();
                        programaEspecifico.EstadoPid = dto.IdEstadoPEspecifico;
                        programaEspecifico.EstadoP =
                            _repoEstadoPespecifico.FirstById(dto.IdEstadoPEspecifico.Value).Nombre;
                        programaEspecifico.IdEstadoPespecifico = dto.IdEstadoPEspecifico;
                    }
                    if (dto.IdModalidadCurso != null)
                    {
                        ModalidadCursoRepositorio _repoModalidadCurso = new ModalidadCursoRepositorio();
                        programaEspecifico.TipoId = dto.IdModalidadCurso;
                        programaEspecifico.Tipo = _repoModalidadCurso.FirstById(dto.IdModalidadCurso.Value).Nombre;
                    }
                    if (dto.IdCursoMoodle != null)
                    {
                        programaEspecifico.IdCursoMoodle = dto.IdCursoMoodle == 0 ? null : dto.IdCursoMoodle;
                    }
                    if (dto.IdCursoMoodlePrueba != null)
                    {
                        programaEspecifico.IdCursoMoodlePrueba = dto.IdCursoMoodlePrueba == 0 ? null : dto.IdCursoMoodlePrueba;
                    }

                    programaEspecifico.UsuarioModificacion = dto.Usuario;
                    programaEspecifico.FechaModificacion = DateTime.Now;
                    _repProgramaEspecifico.Update(programaEspecifico);

                    PEspecificoParticipacionExpositorBO pEspecificoParticipacionExpositorBO = new PEspecificoParticipacionExpositorBO();

                    if (dto.IdProveedor != null)
                    {
                        var Proveedor = _repProveedor.FirstById(dto.IdProveedor.Value);
                        if (_repPEspecificoParticipacionExpositor.Exist(w => w.IdPespecifico == programaEspecifico.Id && w.Grupo == 1))
                        {
                            pEspecificoParticipacionExpositorBO = _repPEspecificoParticipacionExpositor.FirstBy(w => w.IdPespecifico == programaEspecifico.Id && w.Grupo == 1);
                            pEspecificoParticipacionExpositorBO.IdExpositorGrupo = dto.IdExpositor_Referencia;
                            pEspecificoParticipacionExpositorBO.ExpositorGrupo = dto.IdProveedor == null ? null : Proveedor.Nombre1 + " " + Proveedor.Nombre2 + " " + Proveedor.ApePaterno + " " + Proveedor.ApeMaterno;
                            pEspecificoParticipacionExpositorBO.IdProveedorPlanificacionGrupo = dto.IdProveedor;
                            pEspecificoParticipacionExpositorBO.FechaModificacion = DateTime.Now;
                            pEspecificoParticipacionExpositorBO.UsuarioModificacion = dto.Usuario;

                        }
                        else
                        {
                            pEspecificoParticipacionExpositorBO.IdPespecifico = programaEspecifico.Id;
                            pEspecificoParticipacionExpositorBO.IdExpositorGrupo = dto.IdProveedor;
                            pEspecificoParticipacionExpositorBO.ExpositorGrupo = Proveedor.Nombre1 + " " + Proveedor.Nombre2 + " " + Proveedor.ApePaterno + " " + Proveedor.ApeMaterno;
                            pEspecificoParticipacionExpositorBO.IdProveedorPlanificacionGrupo = dto.IdProveedor;
                            pEspecificoParticipacionExpositorBO.Grupo = 1;
                            pEspecificoParticipacionExpositorBO.Estado = true;
                            pEspecificoParticipacionExpositorBO.FechaCreacion = DateTime.Now;
                            pEspecificoParticipacionExpositorBO.FechaModificacion = DateTime.Now;
                            pEspecificoParticipacionExpositorBO.UsuarioCreacion = dto.Usuario;
                            pEspecificoParticipacionExpositorBO.UsuarioModificacion = dto.Usuario;
                        }
                        _repPEspecificoParticipacionExpositor.Update(pEspecificoParticipacionExpositorBO);
                    }

                    foreach (var item in listaSesiones)
                    {
                        if (dto.IdAmbiente != null)
                            item.IdAmbiente = dto.IdAmbiente;
                        if (dto.IdExpositor_Referencia != null)
                            item.IdExpositor = dto.IdExpositor_Referencia;
                        if (dto.IdProveedor != null)
                            item.IdProveedor = dto.IdProveedor;
                        item.UsuarioModificacion = dto.Usuario;
                        item.FechaModificacion = DateTime.Now;
                    }
                    _repPespecificoSesion.Update(listaSesiones);

                    return Ok(new { EstadoCuce = estadoCruce, IdProgramaEspecifico = programaEspecifico.Id });
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarInsertarModuloWebinar([FromBody] InsertarActualizarModuloWebinaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadrePespecificoHijo = new PespecificoPadrePespecificoHijoRepositorio();
                PespecificoPadrePespecificoHijoBO pespecificoPadrePespecificoHijoBO = new PespecificoPadrePespecificoHijoBO();
                if (dto.Id == 0)
                {
                    pespecificoPadrePespecificoHijoBO.PespecificoPadreId = dto.IdPespecificoPadre;
                    pespecificoPadrePespecificoHijoBO.PespecificoHijoId = dto.IdPespecifico;
                    pespecificoPadrePespecificoHijoBO.Estado = true;
                    pespecificoPadrePespecificoHijoBO.FechaCreacion = DateTime.Now;
                    pespecificoPadrePespecificoHijoBO.FechaModificacion = DateTime.Now;
                    pespecificoPadrePespecificoHijoBO.UsuarioCreacion = dto.Usuario;
                    pespecificoPadrePespecificoHijoBO.UsuarioModificacion = dto.Usuario;

                    _repPespecificoPadrePespecificoHijo.Insert(pespecificoPadrePespecificoHijoBO);

                    return Ok(true);
                }
                else
                {
                    pespecificoPadrePespecificoHijoBO = _repPespecificoPadrePespecificoHijo.FirstBy(w => w.PespecificoPadreId == dto.IdPespecificoPadre && w.PespecificoHijoId == dto.Id);
                    if (pespecificoPadrePespecificoHijoBO != null)
                    {
                        pespecificoPadrePespecificoHijoBO.PespecificoHijoId = dto.IdPespecifico;
                        pespecificoPadrePespecificoHijoBO.FechaModificacion = DateTime.Now;
                        pespecificoPadrePespecificoHijoBO.UsuarioModificacion = dto.Usuario;

                        _repPespecificoPadrePespecificoHijo.Update(pespecificoPadrePespecificoHijoBO);

                        return Ok(true);
                    }

                }
                return Ok(false);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{PEspecificoId}/{CursoIndividual}")]
        [HttpGet]
        public ActionResult ObtenerNumeroGrupos(int PEspecificoId, bool? CursoIndividual)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                if (CursoIndividual.HasValue)
                {
                    if (CursoIndividual.Value)
                    {
                        var gruposIndividuales = _repPespecifico.ObtenerGruposSesionesIndividuales(PEspecificoId);
                        return Ok(gruposIndividuales);
                    }
                }
                var grupos = _repPespecifico.ObtenerGruposSesiones(PEspecificoId);
                return Ok(grupos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{PEspecificoId}/{Usuario}/{IdPespecificoHijo}")]
        [HttpGet]
        public ActionResult ClonarSesiones(int PEspecificoId, string Usuario, int IdPespecificoHijo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var estado = false;
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadreHijo = new PespecificoPadrePespecificoHijoRepositorio();
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio();
                PEspecificoParticipacionExpositorRepositorio _repPEspecificoParticipacionExpositor = new PEspecificoParticipacionExpositorRepositorio();
                ProveedorRepositorio _repProveedor = new ProveedorRepositorio();

                if (IdPespecificoHijo == 0)
                {
                    var listaSesiones = _repPespecificoSesion.GetBy(x => x.IdPespecifico == PEspecificoId && x.Grupo == 1);

                    var grupoSesiones = _repPespecificoSesion.GetBy(x => x.IdPespecifico == PEspecificoId).OrderByDescending(x => x.Grupo).FirstOrDefault().Grupo;

                    //var grupoSesiones = _repPEspecifico.ObtenerGruposSesiones(PEspecificoId);
                    int nroGrupo = 0;
                    if (grupoSesiones != null)
                    {
                        nroGrupo = grupoSesiones + 1;
                    }
                    else
                    {
                        nroGrupo = 1;
                    }
                    int cont = 0;
                    int orden = 0;
                    foreach (var item in listaSesiones.OrderBy(w => w.Id))
                    {
                        cont++;
                        PespecificoSesionBO sesion = new PespecificoSesionBO();
                        sesion.IdPespecifico = item.IdPespecifico;
                        sesion.FechaHoraInicio = item.FechaHoraInicio;
                        sesion.Duracion = item.Duracion;
                        sesion.IdAmbiente = item.IdAmbiente;
                        sesion.Grupo = nroGrupo; //Nuevo
                        sesion.SesionAutoGenerada = item.SesionAutoGenerada;
                        sesion.IdExpositor = item.IdExpositor;
                        sesion.Predeterminado = item.Predeterminado;
                        sesion.Version = item.Version;
                        sesion.GrupoSesion = item.GrupoSesion;
                        sesion.IdModalidadCurso = item.IdModalidadCurso;
                        sesion.Estado = true;
                        sesion.FechaCreacion = DateTime.Now;
                        sesion.FechaModificacion = DateTime.Now;
                        sesion.UsuarioCreacion = Usuario;
                        sesion.UsuarioModificacion = Usuario;
                        estado = _repPespecificoSesion.Insert(sesion);

                        if (sesion.Id == _repPespecificoSesion.ObtenerSesionInicial(sesion.IdPespecifico, sesion.Grupo))
                        {
                            orden++;
                            ProveedorBO Proveedor = new ProveedorBO();
                            if (sesion.IdProveedor != null)
                            {
                                Proveedor = _repProveedor.FirstById(sesion.IdProveedor.Value);
                            }
                            PEspecificoParticipacionExpositorBO pEspecificoParticipacionExpositorBO = new PEspecificoParticipacionExpositorBO();
                            if (_repPEspecificoParticipacionExpositor.Exist(w => w.IdPespecifico == sesion.IdPespecifico && w.Grupo == sesion.Grupo))
                            {
                                pEspecificoParticipacionExpositorBO = _repPEspecificoParticipacionExpositor.FirstBy(w => w.IdPespecifico == sesion.IdPespecifico && w.Grupo == sesion.Grupo);
                                pEspecificoParticipacionExpositorBO.IdExpositorGrupo = sesion.IdExpositor;
                                pEspecificoParticipacionExpositorBO.IdProveedorPlanificacionGrupo = sesion.IdProveedor;
                                pEspecificoParticipacionExpositorBO.ExpositorGrupo = sesion.IdProveedor == null ? null : Proveedor.Nombre1 + " " + Proveedor.Nombre2 + " " + Proveedor.ApePaterno + " " + Proveedor.ApeMaterno;
                                pEspecificoParticipacionExpositorBO.FechaModificacion = DateTime.Now;
                                pEspecificoParticipacionExpositorBO.UsuarioModificacion = Usuario;

                            }
                            else
                            {
                                pEspecificoParticipacionExpositorBO.IdPespecifico = sesion.IdPespecifico;
                                pEspecificoParticipacionExpositorBO.IdExpositorGrupo = sesion.IdExpositor;
                                pEspecificoParticipacionExpositorBO.ExpositorGrupo = sesion.IdProveedor == null ? null : Proveedor.Nombre1 + " " + Proveedor.Nombre2 + " " + Proveedor.ApePaterno + " " + Proveedor.ApeMaterno;
                                pEspecificoParticipacionExpositorBO.IdProveedorPlanificacionGrupo = sesion.IdProveedor;
                                pEspecificoParticipacionExpositorBO.Grupo = sesion.Grupo;
                                pEspecificoParticipacionExpositorBO.Orden = orden;
                                pEspecificoParticipacionExpositorBO.Estado = true;
                                pEspecificoParticipacionExpositorBO.FechaCreacion = DateTime.Now;
                                pEspecificoParticipacionExpositorBO.FechaModificacion = DateTime.Now;
                                pEspecificoParticipacionExpositorBO.UsuarioCreacion = Usuario;
                                pEspecificoParticipacionExpositorBO.UsuarioModificacion = Usuario;
                            }
                            _repPEspecificoParticipacionExpositor.Update(pEspecificoParticipacionExpositorBO);
                        }
                    }
                }
                else
                {
                    if (!_repPespecificoPadreHijo.Exist(x => x.PespecificoPadreId == PEspecificoId && x.PespecificoHijoId == IdPespecificoHijo))
                    {
                        return BadRequest("No se Encontro Programa Asociado");
                    }
                    var listaSesiones = _repPespecificoSesion.GetBy(x => x.IdPespecifico == IdPespecificoHijo && x.Grupo == 1);

                    var grupoSesiones = _repPespecificoSesion.GetBy(x => x.IdPespecifico == IdPespecificoHijo).OrderByDescending(x => x.Grupo).FirstOrDefault().Grupo;

                    //var grupoSesiones = _repPEspecifico.ObtenerGruposSesiones(PEspecificoId);
                    int nroGrupo = 0;
                    if (grupoSesiones != null)
                    {
                        nroGrupo = grupoSesiones + 1;
                    }
                    else
                    {
                        nroGrupo = 1;
                    }
                    int cont = 0;
                    int orden = 0;
                    foreach (var item in listaSesiones.OrderBy(w => w.Id))
                    {
                        cont++;
                        PespecificoSesionBO sesion = new PespecificoSesionBO();
                        sesion.IdPespecifico = item.IdPespecifico;
                        sesion.FechaHoraInicio = item.FechaHoraInicio;
                        sesion.Duracion = item.Duracion;
                        sesion.IdAmbiente = item.IdAmbiente;
                        sesion.Grupo = nroGrupo; //Nuevo
                        sesion.SesionAutoGenerada = item.SesionAutoGenerada;
                        sesion.IdExpositor = item.IdExpositor;
                        sesion.Predeterminado = item.Predeterminado;
                        sesion.Version = item.Version;
                        sesion.GrupoSesion = item.GrupoSesion;
                        sesion.Estado = true;
                        sesion.FechaCreacion = DateTime.Now;
                        sesion.FechaModificacion = DateTime.Now;
                        sesion.UsuarioCreacion = Usuario;
                        sesion.UsuarioModificacion = Usuario;
                        estado = _repPespecificoSesion.Insert(sesion);

                        if (sesion.Id == _repPespecificoSesion.ObtenerSesionInicial(sesion.IdPespecifico, sesion.Grupo))
                        {
                            orden++;
                            ProveedorBO Proveedor = new ProveedorBO();
                            if (sesion.IdProveedor != null)
                            {
                                Proveedor = _repProveedor.FirstById(sesion.IdProveedor.Value);
                            }
                            PEspecificoParticipacionExpositorBO pEspecificoParticipacionExpositorBO = new PEspecificoParticipacionExpositorBO();
                            if (_repPEspecificoParticipacionExpositor.Exist(w => w.IdPespecifico == sesion.IdPespecifico && w.Grupo == sesion.Grupo))
                            {
                                pEspecificoParticipacionExpositorBO = _repPEspecificoParticipacionExpositor.FirstBy(w => w.IdPespecifico == sesion.IdPespecifico && w.Grupo == sesion.Grupo);
                                pEspecificoParticipacionExpositorBO.IdExpositorGrupo = sesion.IdExpositor;
                                pEspecificoParticipacionExpositorBO.IdProveedorPlanificacionGrupo = sesion.IdProveedor;
                                pEspecificoParticipacionExpositorBO.ExpositorGrupo = sesion.IdProveedor == null ? null : Proveedor.Nombre1 + " " + Proveedor.Nombre2 + " " + Proveedor.ApePaterno + " " + Proveedor.ApeMaterno;
                                pEspecificoParticipacionExpositorBO.FechaModificacion = DateTime.Now;
                                pEspecificoParticipacionExpositorBO.UsuarioModificacion = Usuario;

                            }
                            else
                            {
                                pEspecificoParticipacionExpositorBO.IdPespecifico = sesion.IdPespecifico;
                                pEspecificoParticipacionExpositorBO.IdExpositorGrupo = sesion.IdExpositor;
                                pEspecificoParticipacionExpositorBO.ExpositorGrupo = sesion.IdProveedor == null ? null : Proveedor.Nombre1 + " " + Proveedor.Nombre2 + " " + Proveedor.ApePaterno + " " + Proveedor.ApeMaterno;
                                pEspecificoParticipacionExpositorBO.IdProveedorPlanificacionGrupo = sesion.IdProveedor;
                                pEspecificoParticipacionExpositorBO.Grupo = sesion.Grupo;
                                pEspecificoParticipacionExpositorBO.Orden = orden;
                                pEspecificoParticipacionExpositorBO.Estado = true;
                                pEspecificoParticipacionExpositorBO.FechaCreacion = DateTime.Now;
                                pEspecificoParticipacionExpositorBO.FechaModificacion = DateTime.Now;
                                pEspecificoParticipacionExpositorBO.UsuarioCreacion = Usuario;
                                pEspecificoParticipacionExpositorBO.UsuarioModificacion = Usuario;
                            }
                            _repPEspecificoParticipacionExpositor.Update(pEspecificoParticipacionExpositorBO);
                        }
                    }
                }
                //var listaCursos = _repPespecificoPadreHijo.GetBy(x => x.PespecificoPadreId == PEspecificoId).Select(x => x.PespecificoHijoId);
                

                return Ok(estado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCronogramaPEspecifico([FromBody] FiltroObtenerSesionesDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio();
                PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadreHijo = new PespecificoPadrePespecificoHijoRepositorio();
                int numeroGrupo = 0;
                if (dto.nroGrupo == null)
                {
                    numeroGrupo = 1;
                }
                else
                {
                    numeroGrupo = dto.nroGrupo.Value;
                }
                var PEspecifico = _repPespecifico.FirstById(dto.PEspecificoId);
                if (dto.CursoIndividual.HasValue)
                {
                    if (dto.CursoIndividual.Value)
                    {
                        //var listaSesiones = _repPespecificoSesion.GetBy(x => x.IdPespecifico == PEspecificoId && x.Grupo == numeroGrupo).ToList();
                        //var rpta = listaSesiones.Select(x => new {
                        //	Id = x.Id,
                        //	FechaHoraInicio = x.FechaHoraInicio,
                        //	Duracion = x.Duracion,
                        //	DuracionTotal = PEspecifico.Duracion,
                        //	Curso = PEspecifico.Nombre,
                        //	IdExpositor = x.IdExpositor,
                        //	IdAmbiente = x.IdAmbiente,
                        //	IdCiudad = PEspecifico.IdCiudad,
                        //	PEspecificoHijoId = PEspecifico.Id,
                        //	Tipo = PEspecifico.Tipo,
                        //	Comentario = x.Comentario,
                        //	//Cruce = SesionTieneCruce(Sesiones),
                        //	//RowVersion = convertRowVersionToString(Sesiones.RowVersion),
                        //	EsSesionInicial = x.EsSesionInicio,
                        //	IdCentroCosto = x.id
                        //	MostrarPDF = true
                        //});
                        var rpta = _repPespecifico.ObtenerCronogramaPEspecificoGrupoSesionIndividual(dto.PEspecificoId, numeroGrupo);
                        return Ok(rpta);
                    }
                }
                var rpta2 = _repPespecifico.ObtenerCronogramaPEspecificoGrupo(dto.PEspecificoId, dto.ListaPEspecificos, numeroGrupo);
                return Ok(rpta2);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{PEspecificoId}/{NumeroGrupo}/{Usuario}")]
        [HttpGet]
        public ActionResult EliminarCronogramaDuplicado(int PEspecificoId, int NumeroGrupo, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var estado = false;
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadreHijo = new PespecificoPadrePespecificoHijoRepositorio();
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio();
                PEspecificoParticipacionExpositorRepositorio _repPEspecificoParticipacionExpositor = new PEspecificoParticipacionExpositorRepositorio();

                var listaCursos = _repPespecificoPadreHijo.GetBy(x => x.PespecificoPadreId == PEspecificoId).Select(x => x.PespecificoHijoId);
                if (listaCursos.Count() != 0)
                {
                    var listaSesiones = _repPespecificoSesion.GetBy(x => listaCursos.Contains(x.IdPespecifico) && x.Grupo == NumeroGrupo);
                    var listaParticipacion = _repPEspecificoParticipacionExpositor.GetBy(w => listaCursos.Contains(w.IdPespecifico) && w.Grupo == NumeroGrupo).Select(w => w.Id);

                    foreach (var item in listaSesiones)
                    {
                        estado = _repPespecificoSesion.Delete(item.Id, Usuario);
                    }
                    _repPEspecificoParticipacionExpositor.Delete(listaParticipacion, Usuario);

                }
                else
                {
                    var listaSesiones = _repPespecificoSesion.GetBy(x => x.IdPespecifico == PEspecificoId && x.Grupo == NumeroGrupo);
                    var listaParticipacion = _repPEspecificoParticipacionExpositor.GetBy(w => w.IdPespecifico== PEspecificoId && w.Grupo == NumeroGrupo).Select(w => w.Id);

                    foreach (var item in listaSesiones)
                    {
                        estado = _repPespecificoSesion.Delete(item.Id, Usuario);
                    }
                    _repPEspecificoParticipacionExpositor.Delete(listaParticipacion, Usuario);

                }

                return Ok(estado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [Route("[Action]/{IdPespecifico}/{Usuario}")]
        [HttpGet]
        public ActionResult GenerarCronogramaGrupal(int IdPespecifico, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                PespecificoBO pespecificoBO = new PespecificoBO();

                var pespecifico = _repPespecifico.FirstById(IdPespecifico);
                var nombre = pespecificoBO.GenerarPDFCronogramaGrupal(IdPespecifico, pespecifico.Nombre, Usuario);

                return Ok(nombre);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpGet]
        public ActionResult GenerarCronogramaGrupalMasivo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                PespecificoSesionRepositorio pespecificoSesionRepositorio = new PespecificoSesionRepositorio();
                PespecificoBO pespecificoBO = new PespecificoBO();

                //var sesiones = pespecificoSesionRepositorio.GetBy(x => x.FechaHoraInicio > DateTime.Now.AddDays(-60));

                //var pespecificos = sesiones.GroupBy(x => new { x.IdPespecifico })
                //    .Select(y => new EliminacionIdsDTO
                //    {
                //        Id = y.Key.IdPespecifico
                //    }).ToList();

                var pespecificos = _repPespecifico.GetBy(x => x.UrlDocumentoCronogramaGrupos == null && (x.Tipo == "Presencial" || x.Tipo == "Online Sincronica") && x.EstadoP == "Ejecucion" && x.FechaCreacion > DateTime.Now.AddDays(-430) && x.UrlDocumentoCronograma != null);

                foreach (var aux in pespecificos)
                {
                    try
                    {
                        //var pespecifico = _repPespecifico.FirstById(aux.Id);
                        var nombre = pespecificoBO.GenerarPDFCronogramaGrupal(aux.Id, aux.Nombre, "Joao");
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ProgramaEspecificoPadreIndividual([FromBody] ProgramaEspecificoMaterialFiltroDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
                var res = _repPespecifico.ObtenerProgramasEspecificoFiltrosPadreIndividual(Filtro);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPespecifico}/{IdEstadoPrograma}/{Usuario}")]
        public ActionResult ActualizarEstadoPrograma(int IdPespecifico, int IdEstadoPrograma, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PespecificoRepositorio _repProgramaEspecifico = new PespecificoRepositorio(_integraDBContext);
                EstadoPespecificoRepositorio _repEstadoPEspecifico = new EstadoPespecificoRepositorio(_integraDBContext);
                PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadrePespecificoHijo = new PespecificoPadrePespecificoHijoRepositorio(_integraDBContext);

                var programaEspecifico = _repProgramaEspecifico.FirstById(IdPespecifico);
                var estadoEspecifico = _repEstadoPEspecifico.FirstById(IdEstadoPrograma);

                programaEspecifico.EstadoPid = IdEstadoPrograma;
                programaEspecifico.IdEstadoPespecifico = IdEstadoPrograma;
                programaEspecifico.EstadoP = estadoEspecifico.Nombre;

                programaEspecifico.UsuarioModificacion = Usuario;
                programaEspecifico.FechaModificacion = DateTime.Now;
                _repProgramaEspecifico.Update(programaEspecifico);

                if (estadoEspecifico.Id == 2)
                { /*Ejecución*/
                    var Hijos = _repPespecificoPadrePespecificoHijo.ObtenerPespecificosHijos(IdPespecifico);
                    var estadoEspecificoHijos = _repEstadoPEspecifico.FirstById(5);/*Por Ejecucion*/

                    foreach (var item in Hijos)
                    {
                        var pespecificoHijoBO = _repProgramaEspecifico.FirstById(item.PEspecificoHijoId);
                        if (pespecificoHijoBO.EstadoPid == 3)/*Lanzamiento*/
                        {
                            pespecificoHijoBO.EstadoPid = estadoEspecificoHijos.Id;
                            pespecificoHijoBO.IdEstadoPespecifico = estadoEspecificoHijos.Id;
                            pespecificoHijoBO.EstadoP = estadoEspecificoHijos.Nombre;

                            pespecificoHijoBO.UsuarioModificacion = Usuario;
                            pespecificoHijoBO.FechaModificacion = DateTime.Now;
                            _repProgramaEspecifico.Update(pespecificoHijoBO); 
                        }
                    }
                }
                
                return Ok(new { Estado = true, IdProgramaEspecifico = programaEspecifico.Id });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ActualizarEstadoPespecificoAutomatico()
        {
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
                var ListaPespecifico = _repPespecifico.ObtenerPespecificosActualizar();
                foreach (var item in ListaPespecifico)
                {
                    try
                    {
                        var PespecificoBO = _repPespecifico.FirstById(item.IdPespecifico);
                        PespecificoBO.EstadoPid = item.IdEstado;
                        PespecificoBO.EstadoP = item.IdEstado == 2 ? "Ejecucion" : item.IdEstado == 1 ? "Concluido" : "Lanzamiento";
                        PespecificoBO.UsuarioModificacion = PespecificoBO.UsuarioModificacion + "-F";
                        PespecificoBO.FechaModificacion = DateTime.Now;
                        _repPespecifico.Update(PespecificoBO);

                    } catch (Exception e)
                    {

                    }
                }

                return Ok(ListaPespecifico);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult GenerarReporteAmbienteExcel(string IdProgramaEspecifico, string IdCentroCosto, string CodigoBs, string IdEstadoPEspecifico, string IdModalidadCurso, string IdPGeneral, string IdArea, string IdSubArea, int? IdCentroCostoD)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
                var res = _repPespecifico.ObtenerExcelReporteAmbiente(IdProgramaEspecifico, IdCentroCosto, CodigoBs, IdEstadoPEspecifico, IdModalidadCurso, IdPGeneral, IdArea, IdSubArea, IdCentroCostoD);
                ExcelBO excelBO = new ExcelBO();
                byte[] data = excelBO.ReporteAmbientePespecifico(res);
                //return Ok(data);
                return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte Programa Especifico.xlsx");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerProgramasWebex()
        {
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
                return Ok(_repPespecifico.ObtenerProgramasWebex());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerProgramasWebinar()
        {
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
                return Ok(_repPespecifico.ObtenerProgramasWebinar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarConfiguracionWebinar([FromBody] ConfigurarWebinarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext(); 
                
                ConfigurarWebinarRepositorio _repConfigurarWebinar = new ConfigurarWebinarRepositorio(_integraDBContext);
                ConfigurarWebinarBO configurarwebinar = new ConfigurarWebinarBO();
                
                
                PespecificoBO pespecifico = new PespecificoBO(_integraDBContext);
                List<PespecificoFrecuenciaDetalleBO> frecuenciaDetalle = new List<PespecificoFrecuenciaDetalleBO>();


                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                var valor = _repPespecifico.GetBy(w=>w.Id==Json.IdPEspecifico).FirstOrDefault();
                
                using (TransactionScope scope = new TransactionScope())
                {
                    configurarwebinar.IdPespecifico = Json.IdPEspecifico;
                    configurarwebinar.Modalidad = valor.Tipo;
                    configurarwebinar.Codigo = valor.Codigo;
                    configurarwebinar.IdOperadorComparacionAvance = Json.IdOperadorComparacionAvance;
                    configurarwebinar.ValorAvance = Json.ValorAvance;
                    configurarwebinar.ValorAvanceOpc = Json.ValorAvanceOpc;
                    configurarwebinar.IdOperadorComparacionPromedio = Json.IdOperadorComparacionPromedio;
                    configurarwebinar.ValorPromedio = Json.ValorPromedio;
                    configurarwebinar.ValorPromedioOpc = Json.ValorPromedioOpc;
                    configurarwebinar.Estado = true;
                    configurarwebinar.FechaCreacion = DateTime.Now;
                    configurarwebinar.FechaModificacion = DateTime.Now;
                    configurarwebinar.UsuarioCreacion = Json.Usuario;
                    configurarwebinar.UsuarioModificacion = Json.Usuario;
                    configurarwebinar.IdPespecificoPadre = Json.IdPespecificoPadre;
                    _repConfigurarWebinar.Insert(configurarwebinar);
                    scope.Complete();
                }

                return Ok(Json);
            }
            
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EjecutarCreacionSesionesWebex([FromBody]List<DatosProgramasWebexDTO> ListaProgramasWebex)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ListaProgramasWebex == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var horaactual = DateTime.Now;
                //var horaactual = new DateTime(2022, 02, 02, 21, 00, 00);

                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
                PespecificoSesionRepositorio  _repPespecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
                var cuentasWebex = _repPespecificoSesion.ObtenerCuentasWebex();

                foreach (var ProgramaEspecifico in ListaProgramasWebex)
                {
                    //if (ProgramaEspecifico.IdPEspecifico != 14201) continue;

                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            var listaSesiones = _repPespecifico.ObtenerSesionesNoVencidasPorPEspecifico(ProgramaEspecifico.IdPEspecifico).OrderBy(w => w.FechaInicio);

                            foreach (var item in listaSesiones)
                            {
                                if (ProgramaEspecifico.IdTiempoFrecuencia == 6)//horas
                                {
                                    var fechaComparar = item.FechaInicio.AddHours(-ProgramaEspecifico.Valor);
                                    //var fechaComparar = item.FechaInicio;
                                    if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                                    {
                                        //aqui llama para crear la sesiones webex
                                        var urlsesion = InsertarSesionWebex(item, cuentasWebex, _integraDBContext);

                                        PespecificoSesionBO PEspecificoSesion = _repPespecificoSesion.FirstById(item.Id);
                                        PEspecificoSesion.UrlWebex = urlsesion.UrlWebex;
                                        PEspecificoSesion.CuentaWebex = urlsesion.Cuenta;
                                        PEspecificoSesion.FechaModificacion = DateTime.Now;
                                        _repPespecificoSesion.Update(PEspecificoSesion);

                                    }
                                }
                                else if (ProgramaEspecifico.IdTiempoFrecuencia == 2)//dias
                                {
                                    var fechaComparar = item.FechaInicio.AddDays(-ProgramaEspecifico.Valor);
                                    //var fechaComparar = item.FechaInicio;
                                    if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                                    {
                                        //aqui llama para crear la sesiones webex
                                        var urlsesion = InsertarSesionWebex(item, cuentasWebex, _integraDBContext);

                                        PespecificoSesionBO PEspecificoSesion = _repPespecificoSesion.FirstById(item.Id);
                                        PEspecificoSesion.UrlWebex = urlsesion.UrlWebex;
                                        PEspecificoSesion.CuentaWebex = urlsesion.Cuenta;
                                        PEspecificoSesion.FechaModificacion = DateTime.Now;
                                        _repPespecificoSesion.Update(PEspecificoSesion);
                                    }
                                }
                                else if (ProgramaEspecifico.IdTiempoFrecuencia == 3)//semanas
                                {
                                    var valorDias = ProgramaEspecifico.Valor * 7;

                                    var fechaComparar = item.FechaInicio.AddDays(-valorDias);
                                    //var fechaComparar = item.FechaInicio;
                                    if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                                    {
                                        //aqui llama para crear la sesiones webex
                                        var urlsesion = InsertarSesionWebex(item, cuentasWebex, _integraDBContext);

                                        PespecificoSesionBO PEspecificoSesion = _repPespecificoSesion.FirstById(item.Id);
                                        PEspecificoSesion.UrlWebex = urlsesion.UrlWebex;
                                        PEspecificoSesion.CuentaWebex = urlsesion.Cuenta;
                                        PEspecificoSesion.FechaModificacion = DateTime.Now;
                                        _repPespecificoSesion.Update(PEspecificoSesion);
                                    }
                                }
                            }
                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {


                    }
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        static ResultadoWebexDTO InsertarSesionWebex(PEspecificoSesionWebexDTO sesion, List<TokenWebexDTO> cuentasWebex, integraDBContext _integraDBContext)
        {
            try
            {

                string URI = "https://webexapis.com/v1/meetings";
                ////string myParameters = "title=PruebaVisual&password=12345&start=2020-08-24 20:00:00&end=2020-08-24 22:00:00&timezone=America/Lima";
                //string token = "YmM4M2QwMTgtZWZlZi00ZThiLWIzMzMtY2ZhMTkxZjExN2VhMzhhOWUyMDItMmU1_P0A1_728e7250-50f6-47e6-96f6-dd2dad7b54a3";

                //var parametro = new DetalleCreacionReunionWebexDTO();
                //parametro.start.ToString("yyyy-MM-dd HH:mm"),

                
                List<InvitacionesDTO> listaInvitaciones = new List<InvitacionesDTO>();
                InvitacionesDTO invitacion1 = new InvitacionesDTO
                {
                    email = "ealegria1@bsginstitute.com",
                    displayName = "Edgar",
                    coHost = true
                };
                InvitacionesDTO invitacion2 = new InvitacionesDTO
                {
                    email = "fchavez@bsginstitute.com",
                    displayName = "Joao",
                    coHost = true
                };
                InvitacionesDTO invitacion3 = new InvitacionesDTO
                {
                    email = "wruiz@bsgrupo.com",
                    displayName = "Walter",
                    coHost = true
                };
                //InvitacionesDTO invitacion4 = new InvitacionesDTO
                //{
                //    email = "jcastillo@bsginstitute.com",
                //    displayName = "Jorge",
                //    coHost = true
                //};
                InvitacionesDTO invitacion5 = new InvitacionesDTO
                {
                    email = "medinac@bsginstitute.com",
                    displayName = "Miguel Angel",
                    coHost = true
                };
                InvitacionesDTO invitacion6 = new InvitacionesDTO
                {
                    email = "aarcana@bsgrupo.com",
                    displayName = "Alexander",
                    coHost = true
                };
                InvitacionesDTO invitacion7 = new InvitacionesDTO
                {
                    email = "jvillena@bsginstitute.com",
                    displayName = "Jose",
                    coHost = true
                };

                listaInvitaciones.Add(invitacion1);
                listaInvitaciones.Add(invitacion2);
                listaInvitaciones.Add(invitacion3);
                //listaInvitaciones.Add(invitacion4);
                listaInvitaciones.Add(invitacion5);
                listaInvitaciones.Add(invitacion6);
                listaInvitaciones.Add(invitacion7);

                var objeto = new CabeceraInvitacionDTO
                {
                    title = sesion.CentroCosto + " - " + sesion.NombrePrograma.Substring(0, sesion.NombrePrograma.Length <= 50 ? sesion.NombrePrograma.Length : 50),
                    password = "bs123",
                    start = sesion.FechaInicio,
                    end = sesion.FechaFin,
                    timezone = "America/Lima",
                    invitees = listaInvitaciones
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objeto);


                //string respuesta =
                //    "{\"id\":\"3c8c70a170054f65941aa2da216900da\",\"meetingNumber\":\"1204385532\",\"title\":\"PruebaVisual2\",\"password\":\"12345\",\"meetingType\":\"meetingSeries\",\"state\":\"active\",\"timezone\":\"America/Lima\",\"start\":\"2020-08-24T20:00:00-05:00\",\"end\":\"2020-08-24T22:00:00-05:00\",\"hostUserId\":\"Y2lzY29zcGFyazovL3VzL1BFT1BMRS8yNTkzZDc2Yy03NWM0LTQ1MzQtYWE3NS1hNWY4YmI2ODQzZGY\",\"hostDisplayName\":\"Integracion webex-bsg\",\"hostemail\":\"integracionwebex@bsginstitute.com\",\"hostKey\":\"294775\",\"webLink\":\"https://bsginstitute.webex.com/bsginstitute/j.php?MTID=m99dfa36faf005bd4b0b806455cad39fd\",\"sipAddress\":\"1204385532@bsginstitute.webex.com\",\"dialInIpAddress\":\"173.243.2.68\",\"enabledAutoRecordMeeting\":false,\"allowAnyUserToBeCoHost\":false,\"telephony\":{\"accessCode\":\"1204385532\",\"callInNumbers\":[{\"label\":\"Peru Toll\",\"callInNumber\":\"+51-1-706-3270\",\"tollType\":\"toll\"},{\"label\":\"Colombia Toll\",\"callInNumber\":\"+57-4-590-5771\",\"tollType\":\"toll\"}],\"links\":[{\"rel\":\"globalCallinNumbers\",\"href\":\"/v1/meetings/3c8c70a170054f65941aa2da216900da/globalCallinNumbers\",\"method\":\"GET\"}]}}";

                //var respuestaObject = JsonConvert.DeserializeObject<Root>(respuesta);


                //validamos con que cuenta se creara al sesiones 1-2-3-4-5-6-7-8-9-10
                string TokenCuentaFinal = "";
                int CuentaFinal = 0;

                bool Cruce = true;
                for (int j=1; j<10;j++)
                {
                    Cruce = ExisteCruce(j, sesion.FechaInicio, sesion.FechaFin, _integraDBContext);
                    if (Cruce)
                    {
                        continue;
                    }
                    else
                    {
                        TokenCuentaFinal = cuentasWebex.Where(w => w.Id == j).FirstOrDefault().Token;
                        CuentaFinal = j;
                        break;
                    }
                }

                if (TokenCuentaFinal == "")
                {
                    ResultadoWebexDTO resultadofinal = new ResultadoWebexDTO()
                    {
                        UrlWebex = null,
                        Cuenta = 0
                    };
                    return resultadofinal;
                }

                //TokenCuentaFinal= "YmM4M2QwMTgtZWZlZi00ZThiLWIzMzMtY2ZhMTkxZjExN2VhMzhhOWUyMDItMmU1_P0A1_728e7250-50f6-47e6-96f6-dd2dad7b54a3";
                //fin validamos con que cuenta se creara al sesiones 1-2-3-4-5-6


                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + TokenCuentaFinal);
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    //string HtmlResult = wc.UploadString(URI, "POST", myParameters);
                    string HtmlResult = wc.UploadString(URI, "POST", json);

                    var respuestaObject = JsonConvert.DeserializeObject<Root>(HtmlResult);
                    //return respuestaObject.webLink;

                    ResultadoWebexDTO resultadofinal = new ResultadoWebexDTO()
                    {
                        UrlWebex = respuestaObject.webLink,
                        Cuenta = CuentaFinal
                    };
                    return resultadofinal;
                }
            }
            catch (System.Exception e)
            {

                throw;
            }


        }
        static bool ExisteCruce(int idCuenta,DateTime HoraInicio,DateTime HoraFin, integraDBContext _integraDBContext)
        {
            PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
            var ListaSesiones=_repPespecificoSesion.ObtenerSesionesCuentaporDia(idCuenta, HoraInicio);

            int contador = 0;

            foreach (var item in ListaSesiones) 
            {

                if (HoraInicio > item.HoraFin && HoraFin < item.HoraInicio)
                {
                    continue;
                }
                else
                {
                    contador++;
                }

            }
            if (contador <= 1)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerConfiguracionWebinar([FromBody] FiltroDTO objeto)
            {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfigurarWebinarRepositorio _repConfigurarWebinar = new ConfigurarWebinarRepositorio(_integraDBContext);
                var listaConfigurarWebinar = _repConfigurarWebinar.ObtenerWebinarInscrito(objeto.Id);
                return Ok(listaConfigurarWebinar);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarConfigurarWebinar([FromBody] ConfigurarWebinarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfigurarWebinarRepositorio _repConfigurarWebinar = new ConfigurarWebinarRepositorio(_integraDBContext);
                ConfigurarWebinarBO configurarwebinar = new ConfigurarWebinarBO();
                if (_repConfigurarWebinar.Exist(Json.Id))
                {
                    configurarwebinar = _repConfigurarWebinar.FirstById(Json.Id);
                    PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                    var valor = _repPespecifico.GetBy(w => w.Id == Json.IdPEspecifico).FirstOrDefault();
                    using (TransactionScope scope = new TransactionScope())
                    {
                        configurarwebinar.IdPespecifico = Json.IdPEspecifico;
                        configurarwebinar.Modalidad = valor.Tipo;
                        configurarwebinar.Codigo = valor.Codigo;
                        configurarwebinar.IdOperadorComparacionAvance = Json.IdOperadorComparacionAvance;
                        configurarwebinar.ValorAvance = Json.ValorAvance;
                        configurarwebinar.ValorAvanceOpc = Json.ValorAvanceOpc;
                        configurarwebinar.IdOperadorComparacionPromedio = Json.IdOperadorComparacionPromedio;
                        configurarwebinar.ValorPromedio = Json.ValorPromedio;
                        configurarwebinar.ValorPromedioOpc = Json.ValorPromedioOpc;
                        configurarwebinar.UsuarioModificacion = Json.Usuario;
                        configurarwebinar.FechaModificacion = DateTime.Now;
                        _repConfigurarWebinar.Update(configurarwebinar);
                        scope.Complete();
                    }
                    return Ok(Json);

                }
                else
                {
                    return BadRequest("No existe");
                }
                

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarConfiguracionWebinar([FromBody] EliminarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfigurarWebinarRepositorio _repConfigurarWebinar = new ConfigurarWebinarRepositorio(_integraDBContext);                
                if (_repConfigurarWebinar.Exist(Json.Id))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        _repConfigurarWebinar.Delete(Json.Id, Json.NombreUsuario);
                        scope.Complete();
                    }
                    return Ok(Json);

                }
                else
                {
                    return BadRequest("No existe");
                }


            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }

        }

        [Route("[Action]/{IdPespecifico}")]
        [HttpGet]
        public ActionResult ObtenerCriteriosEvaluacionBase(int IdPespecifico)
        {
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);

                if (_repPespecifico.Exist(w => w.Id == IdPespecifico))
                {
                    var pespecifico = _repPespecifico.FirstBy(w => w.Id == IdPespecifico,
                        s => new {s.TipoId, s.IdProgramaGeneral});
                    if (pespecifico.TipoId != null && pespecifico.IdProgramaGeneral != null)
                    {
                        var _repoCriterio = new PGeneralCriterioEvaluacionRepositorio();
                        var listadoCriterios =
                            _repoCriterio.ListarCriteriosPorPespecificoModalidad(pespecifico.IdProgramaGeneral.Value,
                                pespecifico.TipoId.Value);
                        return Ok(listadoCriterios);
                    }
                    else
                    {
                        return BadRequest("El programa no tiene modalidad/programa general registrado");
                    }
                }
                else
                {
                    return BadRequest("El programa no existe");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdProveedor}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoAplicaProyectoAplicacionPorProveedor(int IdProveedor)
        {
            try
            {
                PgeneralProyectoAplicacionRepositorio _repoProyectoAplicacion = new PgeneralProyectoAplicacionRepositorio();

                var listado = _repoProyectoAplicacion.ObtenerPEspecificoAplicaProyectoAplicacionPorProveedor(IdProveedor);

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdProveedor}")]
        [HttpGet]
        public ActionResult ObtenerSesionesOnlineWebinarPorProveedor(int IdProveedor)
        {
            try
            {
                PespecificoSesionRepositorio _repPEspecificoSesion = new PespecificoSesionRepositorio();

                var listado = _repPEspecificoSesion.ObtenerSesionesOnlineWebinarPorProveedor(IdProveedor);

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdProveedor}")]
        [HttpGet]
        public ActionResult ObtenerPGeneralPorProveedor(int IdProveedor)
        {
            try
            {
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();

                var listado = _repPGeneral.ObtenerPGeneralPorProveedor(IdProveedor); 
                    

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }       

        [Route("[Action]/{IdProveedor}/{IdPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoPadrePorProveedor(int IdProveedor, int IdPGeneral)
        {
            try
            {
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                var listado = _repPEspecifico.ObtenerPEspecificoPorProveedor(IdProveedor, IdPGeneral);
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdProveedor}/{IdPGeneral}/{IdPEspecificoPadre}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoHijoPorProveedor(int IdProveedor, int IdPGeneral, int IdPEspecificoPadre)
        {
            try
            {
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();

                var listado = _repPEspecifico.ObtenerPEspecificoHijoPorProveedor(IdProveedor, IdPGeneral, IdPEspecificoPadre);


                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ListadoSesionesPorDocenteFiltrado([FromBody] SesionesOnlineWebinarDocenteDTO Filtro)
        {
            try
        
            {
                PespecificoRepositorio _repoPEspecifico = new PespecificoRepositorio();
                var listado = _repoPEspecifico.ListadoSesionesPorDocenteFiltrado(Filtro);
        return Ok(listado.OrderBy(o => o.FechaSesion).ThenBy(o => o.HoraSesion));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult DetalleSesionesPorAlumnosFiltrado([FromBody] DetalleSesionesAlumnosDTO Filtro)
        {
            try

            {
                PespecificoRepositorio _repoPEspecifico = new PespecificoRepositorio();
                var listado = _repoPEspecifico.DetalleSesionesPorAlumnosFiltrado(Filtro);
                return Ok(listado.OrderBy(o => o.NombreAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Villena
        /// Fecha: 29/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Consulta si el alumno tiene webinar disponible o no
        /// </summary>
        /// <returns></returns>

        [Route("[action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ConsultarWebinarAlumno(int IdMatriculaCabecera)
        {
            try

            {
                PespecificoRepositorio repoPEspecifico = new PespecificoRepositorio();
                var listado = repoPEspecifico.ObtenerSesionAlumnoProgramasWebinar(IdMatriculaCabecera);
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{idpespecifico}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionWebinarPEspecifico(int idpespecifico)
        {
            try

            {
                PespecificoRepositorio _repoPEspecifico = new PespecificoRepositorio();
                var listado = _repoPEspecifico.ObtenerConfiguracionWebinarPEspecifico(idpespecifico);
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
    
    public class ValidadorProgramaEspecificoDTO : AbstractValidator<TPespecifico>
    {
        public static ValidadorProgramaEspecificoDTO Current = new ValidadorProgramaEspecificoDTO();
        public ValidadorProgramaEspecificoDTO()
        {           
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
        }
    }


}
