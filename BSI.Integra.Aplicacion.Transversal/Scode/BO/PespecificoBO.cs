using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using Microsoft.WindowsAzure.Storage.Auth;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Clases;


namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: PespecificoBO
    /// Autor: _ _ _ _ _ _ .
    /// Fecha: 30/04/2021
    /// <summary>
    /// Columnas y funciones de la tabla T_Pespecifico
    /// </summary>
    public class PespecificoBO : BaseBO
	{
        /// Propiedades		                    Significado
        /// -------------	                    -----------------------
        /// Nombre                              Nombre de programa específico 
        /// Codigo                              Código de programa específico
        /// IdCentroCosto                       Id de centro de costo
        /// Frecuencia                          Frecuencia
        /// EstadoP                             Estado de programa específico
        /// Tipo                                Tipo de programa específico
        /// TipoAmbiente                        Tipo de Ambiente
        /// Categoria                           Categoría
        /// IdProgramaGeneral                   Id de programa general
        /// Ciudad                              Ciudad
        /// FechaInicio                         Fecha de inicio
        /// FechaTermino                        Fecha de término
        /// FechaInicioV                        Fecha de Inicio
        /// FechaTerminoV                       Fecha de término
        /// CodigoBanco                         Código de banco
        /// FrecuenciaId                        Id de frecuencia
        /// EstadoPid                           Estado de programa específico
        /// TipoId                              Tipo de Id
        /// CategoriaId                         Id de categoría
        /// OrigenPrograma                      Origen de programa
        /// IdCiudad                            Id de ciudad
        /// CoordinadoraAcademica               Coordinadora Académica
        /// CoordinadoraCobranza                Coordinadora de cobranza
        /// Duracion                            Duración
        /// ActualizacionAutomatica             Actualización automática
        /// IdCursoMoodle                       Id de curso moodle
        /// IdCursoMoodlePrueba                 Id de curso moodle prueba
        /// CursoIndividual                     Validación de curso individual
        /// IdSesionInicio                      Id de sesión de inicio
        /// IdExpositorReferencia               Id de referencia de expositor
        /// IdAmbiente                          Id de ambiente
        /// UrlDocumentoCronograma              Url de documento de cronograma
        /// IdEstadoPespecifico                 Id de estado de Programa Específico
        /// IdMigracion                         Id de migración
        /// UrlDocumentoCronogramaGrupos        Url de documento de grupo de cronograma
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        public string Frecuencia { get; set; }
        public string EstadoP { get; set; }
        public string Tipo { get; set; }
        public string TipoAmbiente { get; set; }
        public string Categoria { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public string Ciudad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string FechaInicioV { get; set; }
        public string FechaTerminoV { get; set; }
        public string CodigoBanco { get; set; }
        public string FechaInicioP { get; set; }
        public string FechaTerminoP { get; set; }
        public int? FrecuenciaId { get; set; }
        public int? EstadoPid { get; set; }
        public int? TipoId { get; set; }
        public int? CategoriaId { get; set; }
        public short? OrigenPrograma { get; set; }
        public int? IdCiudad { get; set; }
        public string CoordinadoraAcademica { get; set; }
        public string CoordinadoraCobranza { get; set; }
        public string Duracion { get; set; }
        public string ActualizacionAutomatica { get; set; }
        public int? IdCursoMoodle { get; set; }
		public int? IdCursoMoodlePrueba { get; set; }
		public bool? CursoIndividual { get; set; }
        public int? IdSesionInicio { get; set; }
        public int? IdExpositorReferencia { get; set; }
        public int? IdAmbiente { get; set; }
        public string UrlDocumentoCronograma { get; set; }
        public int? IdEstadoPespecifico { get; set; }
        public int? IdMigracion { get; set; }
        public string UrlDocumentoCronogramaGrupos { get; set; }
        public string ObservacionCursoFinalizado { get; set; }
        public CursoPespecificoBO CursoPespecifico { get; set; }


        private CategoriaCiudadRepositorio _repCategoriaCiudad;
        private CentroCostoRepositorio _repCentroCosto;
        private PespecificoRepositorio _repPEspecifico;

        CloudBlockBlob blockBlob;
        CloudBlobContainer container;

        public PespecificoBO(integraDBContext contexto)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repCategoriaCiudad = new CategoriaCiudadRepositorio(contexto);
            _repCentroCosto = new CentroCostoRepositorio(contexto);
        }

        public PespecificoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }

        public PespecificoBO(int IdPespecifico)
        {
			ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repPEspecifico = new PespecificoRepositorio();
            AsignarValores(IdPespecifico);
        }

        public PespecificoBO(int IdPespecifico,integraDBContext contexto)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repCategoriaCiudad = new CategoriaCiudadRepositorio(contexto);
            _repCentroCosto = new CentroCostoRepositorio(contexto);
            _repPEspecifico = new PespecificoRepositorio(contexto);
            AsignarValores(IdPespecifico);
        }

        private void AsignarValores(int idPespecifico)
        {        
            var Objeto = _repPEspecifico.FirstById(idPespecifico);
            this.Id = Objeto.Id;
            this.Nombre = Objeto.Nombre;
            this.Codigo = Objeto.Codigo;
            this.IdCentroCosto = Objeto.IdCentroCosto;
            this.Frecuencia = Objeto.Frecuencia;
            this.EstadoP = Objeto.EstadoP;
            this.Tipo = Objeto.Tipo;
            this.TipoAmbiente = Objeto.TipoAmbiente;
            this.Categoria = Objeto.Categoria;
            this.IdProgramaGeneral = Objeto.IdProgramaGeneral;
            this.Ciudad = Objeto.Ciudad;
            this.FechaInicio = Objeto.FechaInicio;
            this.FechaTermino = Objeto.FechaTermino;
            this.FechaInicioV = Objeto.FechaInicioV;
            this.FechaTerminoV = Objeto.FechaTerminoV;
            this.CodigoBanco = Objeto.CodigoBanco;
            this.FechaInicioP = Objeto.FechaInicioP;
            this.FechaTerminoP = Objeto.FechaTerminoP;
            this.FrecuenciaId = Objeto.FrecuenciaId;
            this.EstadoPid = Objeto.EstadoPid;
            this.TipoId = Objeto.TipoId;
            this.CategoriaId = Objeto.CategoriaId;
            this.OrigenPrograma = Objeto.OrigenPrograma;
            this.IdCiudad = Objeto.IdCiudad;
            this.CoordinadoraAcademica = Objeto.CoordinadoraAcademica;
            this.CoordinadoraCobranza = Objeto.CoordinadoraCobranza;
            this.Duracion = Objeto.Duracion;
            this.ActualizacionAutomatica = Objeto.ActualizacionAutomatica;
            this.IdCursoMoodle = Objeto.IdCursoMoodle;
            this.CursoIndividual = Objeto.CursoIndividual;
            this.IdSesionInicio = Objeto.IdSesionInicio;
            this.IdExpositorReferencia = Objeto.IdExpositorReferencia;
            this.IdAmbiente = Objeto.IdAmbiente;
            this.UrlDocumentoCronograma = Objeto.UrlDocumentoCronograma;
            this.IdEstadoPespecifico = Objeto.IdEstadoPespecifico;
            this.Estado = Objeto.Estado;
            this.UsuarioCreacion = Objeto.UsuarioCreacion;
            this.UsuarioModificacion = Objeto.UsuarioModificacion;
            this.FechaCreacion = Objeto.FechaCreacion;
            this.FechaModificacion = Objeto.FechaModificacion;
            this.RowVersion = Objeto.RowVersion;
        }
               
        public string ObtenerCodigoPEspecifico(PGeneralDatosDTO pGeneral, int idCiudad)
        {
            try
            {
                

                if (pGeneral.IdCategoria == 0)
                    throw new Exception("Programa General no tiene Categoria");


                var TroncalCompleto = _repCategoriaCiudad.FirstBy(w => w.Estado == true && w.IdCiudad == idCiudad && w.IdCategoriaPrograma == pGeneral.IdCategoria, w => new { w.TroncalCompleto });

                if (TroncalCompleto == null)
                    throw new Exception("No existe Troncal para esta categoria en esta ciudad: " + idCiudad);


                string codigo = TroncalCompleto.TroncalCompleto;

                var CentroCostoUltimo = _repCentroCosto.GetBy(w => w.Codigo.Contains(codigo) && w.Estado == true, w => new { w.Codigo }).OrderByDescending(w => Convert.ToInt64(w.Codigo)).FirstOrDefault();

                if (CentroCostoUltimo == null)
                {
                    return codigo + "001";
                }
                else
                {

                    string codigoPrimeraParte = CentroCostoUltimo.Codigo.Substring(0, 6);
                    string codigoUltimosDigitos = CentroCostoUltimo.Codigo.Substring(6);
                    string sumado = (Int64.Parse(codigoUltimosDigitos) + 1).ToString();
                    if (codigoUltimosDigitos.Substring(0, 1).Equals("0")) sumado = "0" + sumado;
                    if (codigoUltimosDigitos.Substring(0, 2).Equals("00")) sumado = "0" + sumado;

                    return codigoPrimeraParte + (sumado).ToString();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int ObtenerAnioDesdeCentroCosto(int? idCentroCosto, string tipoModalidad)
        {
            var CC = _repCentroCosto.FirstById(idCentroCosto.Value);
            int anio = 0, cont = 4;

            while (anio < 2000)
                anio = Int32.Parse(CC.Nombre.Substring((CC.IdPgeneral + ' ' + tipoModalidad + ' ').Trim().Length, cont++));

            return anio;
        }

        public CentroCostoDatosDTO ObtenerCentroCostoPEspecifico(PGeneralDatosDTO pGeneral, string codigo, string nombreCiudad, int Anio, string modalidad, out string nombrePEspecifico)
        {
            var Condicion = "";
            if (nombreCiudad.ToUpper() == "AREQUIPA")
                nombreCiudad = "AQP";

            if (modalidad == "")
            {
                Condicion = " (Nombre Not Like '%ONLINE%') ";
            }

            if (modalidad == "AONLINE")
            {
                Condicion = " (Nombre Like '%AONLINE%') ";
            }

            if (modalidad == "ONLINE")
            {
                Condicion = " (Nombre Like '%ONLINE%' and Nombre Not Like'%AONLINE%') ";
            }

            var ListaCentroCosto = _repCentroCosto.ObtenerCentroCostoParaPEspecifico(pGeneral.Codigo, Condicion, Anio.ToString(), nombreCiudad);
            var FiltradoPorCodigo = ListaCentroCosto.Where(s => s.IdPgeneral.Equals(pGeneral.Codigo)).ToList();


            string roman = ToRoman(FiltradoPorCodigo.Count() + 1);


            nombrePEspecifico = pGeneral.Nombre + ' '
                                + (String.IsNullOrEmpty(modalidad) ? "" : modalidad + " ")
                                + Anio.ToString() + ' ' + roman + ' ' + nombreCiudad;
            CentroCostoDatosDTO CentroCosto = new CentroCostoDatosDTO();

            CentroCosto.IdArea = pGeneral.IdArea;
            CentroCosto.IdSubArea = pGeneral.IdSubArea;
            CentroCosto.IdPgeneral = pGeneral.Codigo;
            CentroCosto.Nombre = pGeneral.Codigo + ' '
                             + (String.IsNullOrEmpty(modalidad) ? "" : modalidad + " ")
                             + Anio.ToString() + ' ' + roman + ' ' + nombreCiudad;
            CentroCosto.Codigo = codigo;
            CentroCosto.IdAreaCc = "9-3";

            return CentroCosto;
        }
        
        public bool VerificarSiPespecificoIndividual(int idPespecifico)
        {
            bool individual = _repPEspecifico.Exist(w => w.CursoIndividual == true && w.Id == idPespecifico);

            //bool Padre = _pespecificoPadrePespecificoHijoRepository.VerificarSiPEspecificoTienePadre(idPespecifico);

            //if (Padre == true && individual == true)
            //    throw new Exception("Este P. Especifico tiene cursos asociados y esta marcado como Curso individual");

            return individual;
        }

        /// Autor: Jose Villena
        /// Fecha: 23/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Pdf Cronograma Programa Grupo individual
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="cursoIndividual"> Curso Individual</param>
        /// <param name="cursoNombre"> Curso Nombre</param>
        /// <param name="Usuario"> Usuario</param>
        /// <param name="sesiones"> sesiones</param>        
        /// <returns>UrlFile</returns>
        public string GenerarPDFCronograma(int idPespecifico, bool? cursoIndividual, string cursoNombre, string usuario, List<PespecificoSesionCompuestoDTO> sesiones)
        {
            try
            {
                PespecificoSesionRepositorio repPespecificoSesion = new PespecificoSesionRepositorio();
                string tmpDir = "cronogramas//";
                var ListaOrdernadaInicio = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                var ListaOrdernadaAux = sesiones.OrderBy(x => x.FechaHoraInicio).Where(x => x.Tipo != "Online Asincronica").ToList();                
                List<PespecificoSesionCompuestoDTO> ListaOrdernada;
                var registrosCronograma = ListaOrdernadaInicio.Where(x => x.Tipo.Equals("Online Asincronica")).ToList();
                string nombreCurso = string.Empty;

                if (registrosCronograma != null && registrosCronograma.Count > 0)
                {
                    foreach (var iAsincro in registrosCronograma)
                    {
                        if (nombreCurso != iAsincro.Curso)
                        {
                            ListaOrdernadaAux.Add(iAsincro);
                            nombreCurso = iAsincro.Curso;
                        }
                    }
                    ListaOrdernada = ListaOrdernadaAux.OrderBy(x => x.FechaHoraInicio).ToList();
                }
                else
                {
                    ListaOrdernada = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                }
                PespecificoRepositorio repPecifico = new PespecificoRepositorio();
                RegistroProgramaEspecificoDTO especifico = repPecifico.ObtenerRegistroPespecificoPorId(idPespecifico);
                tmpDir += especifico.Codigo;
                //--------------------------------------------------------
                //Configura los nombres de los programas como programa general              
                var cursos = sesiones.GroupBy(test => test.Curso).Select(grp => grp.First()).ToList();
                foreach (var cur in cursos)
                {
                    var especificoCurso = repPecifico.ObtenerRegistroPespecificoPorId(cur.PEspecificoHijoId.Value);

                }
                //Configura texto sesion especial
                foreach (var sesi in sesiones)
                {
                    if (sesi.PEspecificoHijoId == 6009)
                    {
                        sesi.Curso = "Sesión audiovisual";
                    }
                }
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials("repositoriointegra", "gIaT0DXh2VL1BeK8lWvp5FU8LcJXkS8mzydcO3aB8n7R0TSQ5cEb1NPcz+ZSr7PVq5trhtYjdZHbAQaStAe2ZA=="), true);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                container = blobClient.GetContainerReference(especifico.Codigo);
                container.CreateIfNotExistsAsync();
                container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
                if (cursoIndividual.HasValue)
                {
                    if (cursoIndividual.Value)
                    {
                        var fileResultBytes1 = ProcesarDatosPDF(ListaOrdernada, cursoNombre, tmpDir);
                        string UrlFile2 = blockBlob.Uri.AbsoluteUri.ToString();
                        var finalPespecifico = repPecifico.FirstById(especifico.Id);
                        finalPespecifico.UrlDocumentoCronograma = UrlFile2;
                        finalPespecifico.UsuarioModificacion = usuario;
                        finalPespecifico.FechaModificacion = DateTime.Now;
                        repPecifico.Update(finalPespecifico);
                        return UrlFile2;
                    }
                }
                var fileResultBytes = ProcesarDatosPDF(ListaOrdernada, cursoNombre, tmpDir);
                string UrlFile = blockBlob.Uri.AbsoluteUri.ToString();
                var finalPespecifico2 = repPecifico.FirstById(especifico.Id);
                finalPespecifico2.UrlDocumentoCronograma = UrlFile;
                finalPespecifico2.UsuarioModificacion = usuario;
                finalPespecifico2.FechaModificacion = DateTime.Now;
                repPecifico.Update(finalPespecifico2);
                return UrlFile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 23/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Pdf Cronograma Programa Grupo individual version 2
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="cursoIndividual"> Curso Individual</param>
        /// <param name="cursoNombre"> Curso Nombre</param>
        /// <param name="Usuario"> Usuario</param>
        /// <param name="sesiones"> sesiones</param>
        /// <param name="grupo"> grupo</param>
        /// <returns>UrlFile</returns>
        public string GenerarPDFCronogramaV2(int idPespecifico, bool? cursoIndividual, string cursoNombre, string usuario, List<PespecificoSesionCompuestoDTO> sesiones, int grupo)
        {
            try
            {
                PespecificoSesionRepositorio repPespecificoSesion = new PespecificoSesionRepositorio();
                PespecificoRepositorio repPespecifico = new PespecificoRepositorio();
                
                //se añaden las sesiones de los grupos anteriores
                sesiones = CalcularSesionesCrongoramaCompletoDesdeGrupo(idPespecifico, grupo);

                string tmpDir = "cronogramas//";
                var ListaOrdernadaInicio = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                var ListaOrdernadaAux = sesiones.OrderBy(x => x.FechaHoraInicio).Where(x => x.Tipo != "Online Asincronica").ToList();               
                List<PespecificoSesionCompuestoDTO> ListaOrdernada = new List<PespecificoSesionCompuestoDTO>();                                
                var registrosCronograma = ListaOrdernadaInicio.Where(x => x.Tipo.Equals("Online Asincronica")).ToList();
                string nombreCurso = string.Empty;

                if (registrosCronograma != null && registrosCronograma.Count > 0)
                {
                    foreach (var iAsincro in registrosCronograma)
                    {
                        if (nombreCurso != iAsincro.Curso)
                        {
                            ListaOrdernadaAux.Add(iAsincro);
                            nombreCurso = iAsincro.Curso;
                        }
                    }
                    ListaOrdernada = ListaOrdernadaAux.OrderBy(x => x.FechaHoraInicio).ToList();                    
                }
                else
                {
                    ListaOrdernada = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                }
                PespecificoRepositorio repPecifico = new PespecificoRepositorio();
                RegistroProgramaEspecificoDTO especifico = repPecifico.ObtenerRegistroPespecificoPorId(idPespecifico);
                tmpDir += especifico.Codigo;
                //--------------------------------------------------------
                //Configura los nombres de los programas como programa general              
                var cursos = sesiones.GroupBy(test => test.Curso).Select(grp => grp.First()).ToList();
                foreach (var cur in cursos)
                {
                    var especificoCurso = repPecifico.ObtenerRegistroPespecificoPorId(cur.PEspecificoHijoId.Value);                   
                }               
                //Configura texto sesion especial
                foreach (var sesi in sesiones)
                {
                    if (sesi.PEspecificoHijoId == 6009)
                    {
                        sesi.Curso = "Sesión audiovisual";
                    }
                }              
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials("repositoriointegra", "gIaT0DXh2VL1BeK8lWvp5FU8LcJXkS8mzydcO3aB8n7R0TSQ5cEb1NPcz+ZSr7PVq5trhtYjdZHbAQaStAe2ZA=="), true);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                container = blobClient.GetContainerReference(especifico.Codigo);
                container.CreateIfNotExistsAsync();
                container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
                if (cursoIndividual.HasValue)
                {
                    if (cursoIndividual.Value)
                    {
                        var fileResultBytes1 = ProcesarDatosPDF(ListaOrdernada, cursoNombre, tmpDir);
                        string UrlFile2 = blockBlob.Uri.AbsoluteUri.ToString();
						var finalPespecifico = repPecifico.FirstById(especifico.Id);
                        finalPespecifico.UrlDocumentoCronograma = UrlFile2;
                        finalPespecifico.UsuarioModificacion = usuario;
						finalPespecifico.FechaModificacion = DateTime.Now;
                        repPecifico.Update(finalPespecifico);
                        return UrlFile2;
                    }
                }
                var fileResultBytes = ProcesarDatosPDF(ListaOrdernada, cursoNombre, tmpDir);
                string UrlFile = blockBlob.Uri.AbsoluteUri.ToString();
                var finalPespecifico2 = repPecifico.FirstById(especifico.Id);
                finalPespecifico2.UrlDocumentoCronograma = UrlFile;
                finalPespecifico2.UsuarioModificacion = usuario;
				finalPespecifico2.FechaModificacion = DateTime.Now;
                repPecifico.Update(finalPespecifico2);
                return UrlFile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 23/04/2021
        /// Version: 1.0
        /// <summary>
        /// Calcular Sesiones Crongorama Completo Desde Grupo
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="grupo"> grupo</param>
        /// <returns>sesiones</returns>
        private List<PespecificoSesionCompuestoDTO  > CalcularSesionesCrongoramaCompletoDesdeGrupo (int idPespecifico, int grupo)
        {
            PespecificoRepositorio repPespecifico = new PespecificoRepositorio();
            List<PespecificoSesionCompuestoDTO> listado = new List<PespecificoSesionCompuestoDTO>();

            for (int grupoAnterior = grupo; grupoAnterior > 0; grupoAnterior--)
            {
                var sesionesPEspecifico = repPespecifico.ObtenerSesionesPorPEspecificoGrupoAnterior(idPespecifico, grupoAnterior);

                if (sesionesPEspecifico != null && sesionesPEspecifico.Count > 0)
                {
                    var listadoIdPespecificosExistentes = listado.Select(s => s.IdPespecifico).Distinct().ToList();
                    var listadoSesionesAdicionar = sesionesPEspecifico.Where(w => !listadoIdPespecificosExistentes.Contains(w.IdPEspecifico)).ToList();
                    listado.AddRange(listadoSesionesAdicionar.Select(s => new PespecificoSesionCompuestoDTO()
                    {
                        Curso = s.Curso,
                        Duracion = s.Duracion,
                        DuracionTotal = s.DuracionTotal,
                        FechaHoraInicio = s.FechaHoraInicio,
                        IdExpositor = s.IdExpositor,
                        IdPespecifico = s.IdPEspecifico,
                        Id = s.Id,
                        Tipo = s.ModalidadSesion,
                        PEspecificoHijoId = s.IdPEspecifico
                    }));
                }
            }
            return listado;
        }

        private byte[] ProcesarDatosPDF(List<PespecificoSesionCompuestoDTO> listaOrdernada, string cursoNombre, string tmpDir)
        {
            try
            {
                IList<string[]> rows = new List<string[]>();
                string pdfCurso = cursoNombre;

                foreach (var item in listaOrdernada)
                {
                    string modalidad = (string.IsNullOrEmpty(item.Tipo) ? "" : item.Tipo);
                    decimal dur = item.Duracion.Value;
                    //DateTime fecha = item.FechaHoraInicio.AddHours(-5);
                    DateTime fecha = item.FechaHoraInicio;
                    string curso = item.Curso; 
                    decimal durTot = 0;
                    durTot = (modalidad == "Online Asincronica" ? item.DuracionTotal.Value : item.Duracion.Value);                 
                    string[] columnas = new string[9]; //año, mes, día, fecha, horarios, curso, duración
                    columnas[0] = fecha.Year.ToString();
                    columnas[1] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha.ToString("MMMM", CultureInfo.CreateSpecificCulture("es")));
                    columnas[2] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha.ToString("dddd", CultureInfo.CreateSpecificCulture("es")));
                    columnas[3] = fecha.Day.ToString();
                    DateTime fechaF = fecha.AddHours(Convert.ToDouble(dur));
                    columnas[4] = (modalidad == "Online Asincronica" ? "" : fecha.ToString("HH:mm") + " a " + fechaF.ToString("HH:mm") + " horas");
                    columnas[5] = curso;
                    //columnas[6] = durTot.ToString();
                    columnas[6] = String.Format("{0:0.##}", durTot).Replace(",", ".");
                    columnas[7] = columnas[0] + "/" + fecha.ToString("MM") + "/" + columnas[3];
                    columnas[8] = modalidad;

                    rows.Add(columnas);
                }

                IList<string[]> newRows = rows.ToList(); //OrderBy(a => a[7])
                newRows.Insert(0, new string[] { "Año", "Mes", "Día", "Fecha", "Horarios", "Curso", "Duración", "Modalidad" });

                //Aqui Generamos el PDF en memoria y retornamos un Byte[]
                var pdf = GenerarBytePDF(newRows, pdfCurso);
                blockBlob = container.GetBlockBlobReference(ToURLSlug(cursoNombre) + ".pdf");
                blockBlob.Properties.ContentType = "application/pdf";
                blockBlob.Metadata["filename"] = ToURLSlug(cursoNombre) + ".pdf";
                blockBlob.Metadata["filemime"] = "application/pdf";
                Stream stream = new MemoryStream(pdf);
                
                blockBlob.UploadFromStreamAsync(stream).Wait();
                return pdf;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Genera el cuerpo del cronograma
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="nombreCurso"></param>
        /// <returns></returns>
        public byte[] GenerarBytePDF(IList<string[]> rows, string nombreCurso)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document _document = new Document(iTextSharp.text.PageSize.A4.Rotate(), 65f, 65f, 120f, 65f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(_document, ms);
                    pdfWriter.PageEvent = new ITextEventsCronograma();

                    _document.AddTitle("BSG Institute - Cronograma " + nombreCurso);
                    _document.AddCreator("BSG institute");
                    _document.Open();

                    iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                    var para = new iTextSharp.text.Paragraph("Cronograma de alumnos del " + nombreCurso, _standardFont2);
                    para.Alignment = Element.ALIGN_CENTER;
                    _document.Add(para);
                    _document.Add(Chunk.NEWLINE);

                    //De aquí en adelante se crerá la tabla del cronograma:
                    //html += "<tr style='background-color: rgba(0, 0, 0, 0.15);'>";
                    iTextSharp.text.Font _FEncabezadoCrograma = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                    iTextSharp.text.Font _FCuerpoCrograma = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8.5f, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);

                    PdfPTable TablaCronograma = new PdfPTable(8);
                    TablaCronograma.DefaultCell.Padding = 4f;
                    TablaCronograma.WidthPercentage = 100;
                    TablaCronograma.HorizontalAlignment = Element.ALIGN_CENTER;
                    float[] widthsTCronograma = new float[] { 10f, 15f, 15f, 10f, 15f, 45f, 15f, 15f };
                    TablaCronograma.SetWidths(widthsTCronograma);
                    bool FirstRow = true;
                    int cont = 0;
                    foreach (var Row in rows)
                    {
                        if (FirstRow)
                        {
                            //cell.BackgroundColor = new iTextSharp.text.BaseColor(220, 220, 220);
                            PdfPCell cell2 = new PdfPCell();
                            cell2 = new PdfPCell(new Phrase(Row[0], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[1], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[2], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[3], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[4], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[5], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[7], _FEncabezadoCrograma)); // tipo de modalidad
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);
                            FirstRow = false;

                            cell2 = new PdfPCell(new Phrase(Row[6] + " (*)", _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);
                            FirstRow = false;
                        }
                        else
                        {
                            PdfPCell cell3 = new PdfPCell();
                            cell3 = new PdfPCell(new Phrase(Row[0], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[1], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[2], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[3], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[4], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[5], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            //cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[8], _FCuerpoCrograma)); // tipo de modalidad
                            cell3.Padding = 4f;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.HorizontalAlignment = 1;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[6], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.HorizontalAlignment = 1;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);
                        }
                    }
                    _document.Add(TablaCronograma);

                    _document.Close();

                }
                return ms.ToArray();
            }
        }
        public string ToURLSlug(string s)
        {
            return Regex.Replace(s, @"[^a-z0-9]+", "-", RegexOptions.IgnoreCase)
                .Trim(new char[] { '-' })
                .ToLower();
        }
        
        public string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("Valores entre 1 y 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }

        /// Autor: Jose Villena
        /// Fecha: 23/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Pdf Cronograma Programa Grupal
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="cursoNombre"> Curso Nombre</param>
        /// <param name="Usuario"> Usuario</param>
        /// <returns>UrlFile</returns>
        public string GenerarPDFCronogramaGrupal(int idPespecifico, string cursoNombre, string usuario)
        {
            try
            {
                PespecificoRepositorio repPecifico = new PespecificoRepositorio();
                List<PEspecificoCronogramaGrupalGrupoDTO> listaSesiones = repPecifico.ObtenerPEspecificoCronogramaGrupal(idPespecifico);
                List<PEspecificoCronogramaGrupalDTO> sesionesAux = new List<PEspecificoCronogramaGrupalDTO>();
                if (listaSesiones.Count == 0)
                {
                    throw new Exception("Este programa no tiene sesiones");
                }
                foreach (var item in listaSesiones)
                {
                    sesionesAux = CalcularSesionesCrongroamaGrupoCompletoDesdeGrupo(idPespecifico, item.Grupo);
                    item.lista = sesionesAux;
                }                
                foreach (var listaSesion in listaSesiones)
                {                              
                    var sesiones = listaSesion.lista;
                    var ListaOrdernadaInicio = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                    var ListaOrdernadaAux = sesiones.OrderBy(x => x.FechaHoraInicio).Where(x => x.Tipo != "Online Asincronica").ToList();
                    var registrosCronograma = ListaOrdernadaInicio.Where(x => x.Tipo.Equals("Online Asincronica")).ToList();
                    string nombreCurso = string.Empty;

                    if (registrosCronograma != null && registrosCronograma.Count > 0)
                    {
                        foreach (var iAsincro in registrosCronograma)
                        {
                            if (nombreCurso != iAsincro.Curso)
                            {
                                ListaOrdernadaAux.Add(iAsincro);
                                nombreCurso = iAsincro.Curso;
                            }
                        }
                        listaSesion.lista = ListaOrdernadaAux.OrderBy(x => x.FechaHoraInicio).ToList();
                    }
                    else
                    {
                        listaSesion.lista = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                    }
                }

                RegistroProgramaEspecificoDTO especifico = repPecifico.ObtenerRegistroPespecificoPorId(idPespecifico);

                //Configura texto sesion especial
                //foreach (var sesi in sesiones)
                //{
                //    if (sesi.PEspecificoHijoId == 6009)
                //    {
                //        sesi.Curso = "Sesión audiovisual";
                //    }
                //}

                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials("repositoriointegra", "gIaT0DXh2VL1BeK8lWvp5FU8LcJXkS8mzydcO3aB8n7R0TSQ5cEb1NPcz+ZSr7PVq5trhtYjdZHbAQaStAe2ZA=="), true);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                container = blobClient.GetContainerReference("000000000");
                container.CreateIfNotExistsAsync();
                container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
                var fileResultBytes = GenerarBytePDFGrupal(cursoNombre, listaSesiones);
                blockBlob = container.GetBlockBlobReference(ToURLSlug(especifico.Codigo + "-" + cursoNombre) + ".pdf");
                blockBlob.Properties.ContentType = "application/pdf";
                blockBlob.Metadata["filename"] = ToURLSlug(especifico.Codigo + "-" + cursoNombre) + ".pdf";
                blockBlob.Metadata["filemime"] = "application/pdf";
                Stream stream = new MemoryStream(fileResultBytes);
                var objetoBlob =  blockBlob.UploadFromStreamAsync(stream);
                objetoBlob.Wait();
                string UrlFile = "error";

                if (objetoBlob.IsCompletedSuccessfully)
                {
                    UrlFile = blockBlob.Uri.AbsoluteUri.ToString();
                    var finalPespecifico2 = repPecifico.FirstById(especifico.Id);
                    finalPespecifico2.UrlDocumentoCronogramaGrupos = UrlFile;
                    finalPespecifico2.UsuarioModificacion = usuario;
                    finalPespecifico2.FechaModificacion = DateTime.Now;
                    repPecifico.Update(finalPespecifico2);
                }
                else
                {
                    throw new Exception("Error al subir el Documento al BlobStorage");
                }
                return UrlFile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 23/04/2021
        /// Version: 1.0
        /// <summary>
        /// Calcular Sesiones Crongorama Completo Grupo Desde Grupo
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="grupo"> grupo</param>
        /// <returns>sesiones</returns>
        private List<PEspecificoCronogramaGrupalDTO> CalcularSesionesCrongroamaGrupoCompletoDesdeGrupo(int idPespecifico, int grupo)
        {
            PespecificoRepositorio repPespecifico = new PespecificoRepositorio();
            List<PEspecificoCronogramaGrupalDTO> listado = new List<PEspecificoCronogramaGrupalDTO>();

            for (int grupoAnterior = grupo; grupoAnterior > 0; grupoAnterior--)
            {
                var sesionesPEspecifico = repPespecifico.ObtenerSesionesPorPEspecificoGrupo(idPespecifico, grupoAnterior);

                if (sesionesPEspecifico != null && sesionesPEspecifico.Count > 0)
                {
                    var listadoIdPespecificosExistentes = listado.Select(s => s.IdPespecifico).Distinct().ToList();
                    var listadoSesionesAdicionar = sesionesPEspecifico.Where(w => !listadoIdPespecificosExistentes.Contains(w.IdPespecifico)).ToList();
                    listado.AddRange(listadoSesionesAdicionar.Select(s => new PEspecificoCronogramaGrupalDTO()
                    {
                        IdPespecifico = s.IdPespecifico,
                        FechaHoraInicio = s.FechaHoraInicio,
                        Duracion = s.Duracion,
                        DuracionTotal = s.DuracionTotal,
                        Curso = s.Curso,
                        Tipo = s.ModalidadSesion,
                        Grupo = s.Grupo                        
                    }));
                }
            }
            return listado;
        }

        private IList<string[]> ProcesarDatosPDFGrupal(List<PEspecificoCronogramaGrupalDTO> listaOrdernada)
        {
            try
            {
                IList<string[]> rows = new List<string[]>();

                foreach (var item in listaOrdernada)
                {
                    string modalidad = (string.IsNullOrEmpty(item.Tipo) ? "" : item.Tipo);
                    double dur = item.Duracion;
                    DateTime fecha = item.FechaHoraInicio;
                    string curso = item.Curso;
                    string durTot = "0";
                    durTot = modalidad == "Online Asincronica" ? item.DuracionTotal : item.Duracion.ToString();
                    string[] columnas = new string[9]; //año, mes, día, fecha, horarios, curso, duración
                    columnas[0] = fecha.Year.ToString();
                    columnas[1] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha.ToString("MMMM", CultureInfo.CreateSpecificCulture("es")));
                    columnas[2] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha.ToString("dddd", CultureInfo.CreateSpecificCulture("es")));
                    columnas[3] = fecha.Day.ToString();
                    DateTime fechaF = fecha.AddHours(Convert.ToDouble(dur));
                    columnas[4] = (modalidad == "Online Asincronica" ? "" : fecha.ToString("HH:mm") + " a " + fechaF.ToString("HH:mm") + " horas");
                    columnas[5] = curso;
                    //columnas[6] = durTot;
                    columnas[6] = String.Format("{0:0.##}", durTot).Replace(",", ".");
                    columnas[7] = columnas[0] + "/" + fecha.ToString("MM") + "/" + columnas[3];
                    columnas[8] = modalidad;

                    rows.Add(columnas);
                }

                IList<string[]> newRows = rows.ToList(); //OrderBy(a => a[7])
                newRows.Insert(0, new string[] { "Año", "Mes", "Día", "Fecha", "Horarios", "Curso", "Duración", "Modalidad" });


                return newRows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] GenerarBytePDFGrupal(string nombreCurso, List<PEspecificoCronogramaGrupalGrupoDTO> listaSesiones)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document _document = new Document(iTextSharp.text.PageSize.A4.Rotate(), 65f, 65f, 120f, 65f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(_document, ms);
                    pdfWriter.PageEvent = new ITextEventsCronograma();

                    _document.AddTitle("BSG Institute - Cronograma " + nombreCurso);
                    _document.AddCreator("BSG institute");
                    _document.Open();

                    iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                    var para = new iTextSharp.text.Paragraph("Cronograma de alumnos del " + nombreCurso, _standardFont2);
                    para.Alignment = Element.ALIGN_CENTER;
                    _document.Add(para);
                    _document.Add(Chunk.NEWLINE);

                    IList<string[]> rows;
                    foreach (var grupo in listaSesiones)
                    {
                        rows = ProcesarDatosPDFGrupal(grupo.lista);

                        iTextSharp.text.Font _standardFontGrupo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                        var textGrupo = new iTextSharp.text.Paragraph("Grupo " + grupo.Grupo, _standardFontGrupo);
                        para.Alignment = Element.ALIGN_LEFT;
                        _document.Add(textGrupo);
                        _document.Add(new Paragraph("\n"));

                        //De aquí en adelante se crerá la tabla del cronograma:
                        //html += "<tr style='background-color: rgba(0, 0, 0, 0.15);'>";
                        iTextSharp.text.Font _FEncabezadoCrograma = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                        iTextSharp.text.Font _FCuerpoCrograma = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8.5f, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);

                        PdfPTable TablaCronograma = new PdfPTable(8);
                        TablaCronograma.DefaultCell.Padding = 4f;
                        TablaCronograma.WidthPercentage = 100;
                        TablaCronograma.HorizontalAlignment = Element.ALIGN_CENTER;
                        float[] widthsTCronograma = new float[] { 10f, 15f, 15f, 10f, 15f, 45f, 15f, 15f };
                        TablaCronograma.SetWidths(widthsTCronograma);
                        bool FirstRow = true;
                        int cont = 0;
                        foreach (var Row in rows)
                        {
                            if (FirstRow)
                            {
                                //cell.BackgroundColor = new iTextSharp.text.BaseColor(220, 220, 220);
                                PdfPCell cell2 = new PdfPCell();
                                cell2 = new PdfPCell(new Phrase(Row[0], _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);

                                cell2 = new PdfPCell(new Phrase(Row[1], _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);

                                cell2 = new PdfPCell(new Phrase(Row[2], _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);

                                cell2 = new PdfPCell(new Phrase(Row[3], _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);

                                cell2 = new PdfPCell(new Phrase(Row[4], _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);

                                cell2 = new PdfPCell(new Phrase(Row[5], _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);

                                cell2 = new PdfPCell(new Phrase(Row[7], _FEncabezadoCrograma)); // tipo de modalidad
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);
                                FirstRow = false;

                                cell2 = new PdfPCell(new Phrase(Row[6] + " (*)", _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);
                                FirstRow = false;
                            }
                            else
                            {
                                PdfPCell cell3 = new PdfPCell();
                                cell3 = new PdfPCell(new Phrase(Row[0], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                cell3.HorizontalAlignment = 1;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[1], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                cell3.HorizontalAlignment = 1;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[2], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                cell3.HorizontalAlignment = 1;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[3], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                cell3.HorizontalAlignment = 1;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[4], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                cell3.HorizontalAlignment = 1;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[5], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                //cell3.HorizontalAlignment = 1;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[8], _FCuerpoCrograma)); // tipo de modalidad
                                cell3.Padding = 4f;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.HorizontalAlignment = 1;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[6], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.HorizontalAlignment = 1;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);
                            }
                        }
                        _document.Add(TablaCronograma);
                        _document.Add(Chunk.NEXTPAGE);
                    }

                    _document.Close();

                }
                return ms.ToArray();
            }
        }
    }

}
