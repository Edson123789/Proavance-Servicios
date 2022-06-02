using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.Tools;
using BSI.Integra.Aplicacion.Transversal.DTO;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.Text.RegularExpressions;
using System.Globalization;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using System.Net;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: AsignacionAutomaticaTemp
    ///Autor: Gian Miranda
    ///Fecha: 01/06/2021
    ///<summary>
    ///Columnas y funciones de la tabla mkt.T_AsignacionAutomatica_Temp
    ///</summary>
    public class AsignacionAutomaticaTempBO : BaseBO
    {
        ///Propiedades		                Significado
        ///-------------	                -----------------------
        ///Nombres                          Nombres del nuevo dato, los dos nombres en una misma propiedad
        ///Apellidos                        Apellidos del nuevo dato, los dos apellidos en una misma propiedad
        ///Correo                           Correo del nuevo dato
        ///Fijo                             Numero telefonico fijo
        ///Movil                            Celular del dato
        ///IdPais                           Id del pais (PK de la tabla conf.T_Pais)
        ///IdCiudad                         Id de la ciudad (PK de la tabla conf.T_Ciudad)
        ///IdAreaFormacion                  Id del area de formacion (PK de la tabla pla.T_AreaFormacion)
        ///IdCargo                          Id del cargo (PK de la tablapla.T_Cargo)
        ///IdAreaTrabajo                    Id del area de trabajo (PK de la tabla pla.T_AreaTrabajo)
        ///IdIndustria                      Id de la industria (PK de la tabla pla.T_Industria)
        ///NombrePrograma                   Nombre del programa general
        ///IdCentroCosto                    Id del centro de costo (PK de la tabla pla.T_CentroCosto)
        ///CentroCosto                      Nombre del centro de costo
        ///IdTipoDato                       Id del tipo de dato (PK de la tabla mkt.T_TipoDato)
        ///IdFaseOportunidad                Id de la fase de la oportunidad (PK de la tabla pla.T_FaseOportunidad)
        ///Origen                           Id del origen (PK de la tabla mkt.T_Origen)
        ///Procesado                        Flag de procesado de la oportunidad
        ///IdFacebookFormularioLeadgen      Id del leadgen de facebook (PK de la tabla mkt.T_FacebookFormularioLeadgen)
        ///IdConjuntoAnuncio                Id del conjunto de anuncio (PK de la tabla mkt.T_ConjuntoAnuncio)
        ///IdAnuncioFacebook                Id del anuncio de Facebook (PK de la tabla mkt.T_AnuncioFacebook)
        ///IdFaseOportunidadPortal          Id de la fase de la oportunidad portal (PK de la tabla del portal dbo.T_FaseOportunidadPortal)
        ///FechaRegistroCampania            Fecha de creacion original de la campania en Facebook
        ///IdTiempoCapacitacion             Id del tiempo de capacitacion (PK de la tabla mkt.T_TiempoCapacitacion)
        ///IdCategoriaDato                  Id de la categoria dato (PK de la tabla mkt.T_CategoriaOrigen)
        ///IdTipoInteraccion                Id del tipo de interaccion (PK de la tabla mkt.T_TipoInteracccion)
        ///IdInteraccionFormulario          Id de la interaccion en el formulario (PK de la tabla mkt.T_InteraccionFormulario)
        ///UrlOrigen                        Url del origen
        ///IdPagina                         Id de la pagina (PK de la tabla pla.T_PaginaWeb_PW)
        ///IdMigracion                      Id de la migracion V3 (Campo nullable)
        ///CiudadFacebook                   Ciudad en formato original de Facebook
        ///AptoProcesamiento                Flag para determinar si es apto para el procesamiento o no
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Fijo { get; set; }
        public string Movil { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string NombrePrograma { get; set; }
        public int? IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public string Origen { get; set; }
        public bool? Procesado { get; set; }
        public int? IdFacebookFormularioLeadgen { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdAnuncioFacebook { get; set; }
        public Guid? IdFaseOportunidadPortal { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdCategoriaDato { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public string UrlOrigen { get; set; }
        public int? IdPagina { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public string CiudadFacebook { get; set; }
        public bool AptoProcesamiento { get; set; } = true;

        private Parser _parser;
        //repositorios
        private AsignacionAutomaticaTempRepositorio repAsignacionAutomaticaTemp;
        private CategoriaOrigenRepositorio repCategoriaOrigen;
        private PaisRepositorio _repPais;
        private OrigenRepositorio _repOrigen;
        private AsignacionAutomaticaConfiguracionRepositorio _repAsignacionAutomaticaConfiguracion;

        public AsignacionAutomaticaTempBO()
        {
            _repPais = new PaisRepositorio();
            _repOrigen = new OrigenRepositorio();
            _parser = new Parser();
            _repAsignacionAutomaticaConfiguracion = new AsignacionAutomaticaConfiguracionRepositorio();
            repAsignacionAutomaticaTemp = new AsignacionAutomaticaTempRepositorio();
            repCategoriaOrigen = new CategoriaOrigenRepositorio();
        }

        public AsignacionAutomaticaTempBO(LeadgenInformacionDTO leadgenInformacionDTO, integraDBContext contexto)
        {

            AreaFormacionRepositorio _areaFormacionRepositorio = new AreaFormacionRepositorio(contexto);
            AreaTrabajoRepositorio _areaTrabajoRepositorio = new AreaTrabajoRepositorio(contexto);
            IndustriaRepositorio _industriaRepositorio = new IndustriaRepositorio(contexto);
            CargoRepositorio _cargoRepositorio = new CargoRepositorio(contexto);
            AlumnoRepositorio _alumnoRepositorio = new AlumnoRepositorio(contexto);
            CiudadRepositorio _ciudadRepositorio = new CiudadRepositorio(contexto);
            CentroCostoRepositorio _centroCostoRepositorio = new CentroCostoRepositorio(contexto);
            TiempoCapacitacionRepositorio _tiempoCapacitacionRepositorio = new TiempoCapacitacionRepositorio(contexto);
            PespecificoRepositorio _pespecificoRepositorio = new PespecificoRepositorio(contexto);
            PgeneralRepositorio _pgeneralRepositorio = new PgeneralRepositorio(contexto);
            CategoriaOrigenRepositorio _categoriaOrigenRepositorio = new CategoriaOrigenRepositorio(contexto);
            TipoInteraccionRepositorio _tipoInteraccionRepositorio = new TipoInteraccionRepositorio(contexto);

            try
            {
                int pais, region;
                string movil = ObtenerNumeroTelefonico(leadgenInformacionDTO.Telefono);
                movil = QuitarCerosIzquierda(movil);

                string idCampania = leadgenInformacionDTO.AdsetId;
                string ciudadStringTemp = QuitarCaracteres(leadgenInformacionDTO.Ciudad.ToLower());
                var listaPaises = new List<int>(new int[] { 51, 57, 591, 52 });
                List<string> nombresApellidosSeparados = ProcesarNombre(leadgenInformacionDTO.NombreCompleto);

                List<CiudadBO> listaCiudades = new List<CiudadBO>();

                int firma = 0;

                /*Comparacion Mexico*/
                var ciudadMexico = _ciudadRepositorio.FirstBy(x => x.Nombre.ToLower().StartsWith(string.Concat(ciudadStringTemp, ",")) && x.IdPais == 52/*Mexico*/);

                if (ciudadMexico != null && !string.IsNullOrEmpty(movil))
                {
                    if (movil.StartsWith("5201") && movil.Length == 14)
                        movil = string.Concat("52", movil.Substring(4));
                    else if (movil.StartsWith("1") && movil.Length == 11)
                        movil = string.Concat("52", movil.Substring(1));
                    else if (movil.Length == 10)
                        movil = string.Concat("52", movil);

                    if (movil.StartsWith("52"))
                        listaCiudades.Add(ciudadMexico);
                }

                if (!listaCiudades.Any())
                {
                    listaCiudades = _ciudadRepositorio.GetBy(a => a.Nombre.ToLower().Contains(ciudadStringTemp) && a.IdPais != 52/*Mexico*/).ToList();
                    listaPaises.Remove(52);
                }

                foreach (CiudadBO item in listaCiudades)
                {
                    if (item.IdPais == 51) firma += 51;
                    else if (item.IdPais == 57) firma += 57;
                    else if (item.IdPais == 591) firma += 591;
                    else if (item.IdPais == 52) firma += 52;
                }

                //CiudadBO ciudadTemp = _ciudadRepositorio.FirstBy(a => a.Nombre.ToLower().Equals(ciudadStringTemp));
                CiudadBO ciudadTemp = null;
                if (firma == 51)
                    ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 57);
                else if (firma == 57)
                    ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 51);
                else if (firma == 591)
                    ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 591);
                else if (firma == 52)
                    ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 52);
                else if (firma == 108) // detectados colombia y peru
                {
                    if (movil.Length == 12) ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 57);
                    else if (movil.Length == 11) ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 51);
                }
                else if (firma == 648) // detectados colombia y bolivia
                {
                    if (movil.Length == 12) ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 57);
                    else if (movil.Length == 11) ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 591);
                }
                else if (firma == 642) // detectados peru y bolivia
                {
                    if (movil.Length == 11 && movil.StartsWith("591")) ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 591);
                    else if (movil.Length == 11 && movil.StartsWith("51")) ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 51);
                }

                if (ciudadTemp == null) // probablemente pusieron el nombre de la ciudad exacta y en nuestra db solo hacemos match con departamentos y por ello confunde un dato de colombia como mexico
                {
                    if (movil.Length == 11 && movil.StartsWith("591")) ciudadTemp = _ciudadRepositorio.GetBy(x => x.Id == 2061).FirstOrDefault(); // la paz por defecto
                    else if (movil.Length == 11 && movil.StartsWith("51")) ciudadTemp = _ciudadRepositorio.GetBy(x => x.Id == 14).FirstOrDefault();  // lima por defecto
                    else if (movil.Length == 12 && movil.StartsWith("57")) ciudadTemp = _ciudadRepositorio.GetBy(x => x.Id == 1956).FirstOrDefault();  // bogota por defecto
                }

                if (ciudadTemp == null)
                {
                    ciudadStringTemp = ciudadStringTemp.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'ó').Replace('ú', 'u');
                    ciudadTemp = _ciudadRepositorio.FirstBy(a => a.Nombre.ToLower().Contains(ciudadStringTemp) && listaPaises.Contains(a.IdPais));
                    if (ciudadTemp == null) ciudadTemp = _ciudadRepositorio.FirstBy(a => a.Nombre.ToLower().Contains(ciudadStringTemp));
                }
                if (ciudadTemp != null)
                {
                    pais = ciudadTemp.IdPais;
                    region = ciudadTemp.Id;

                    if (movil.Equals("")) movil = new string('0', ciudadTemp.LongCelular);
                    else
                    {
                        if (pais != 51)
                        {
                            //movil = "00" + movil;
                            if (pais == 57)
                            {
                                if (movil.Length > ciudadTemp.LongCelular)
                                {
                                    int resto = (movil.Length - ciudadTemp.LongCelular);
                                    movil = movil.Substring(resto, ciudadTemp.LongCelular);// "57 3178160602"
                                }
                            }
                            else if (pais == 591)
                            {
                                if (movil.Length > ciudadTemp.LongCelular)
                                {
                                    int resto = (movil.Length - ciudadTemp.LongCelular);
                                    movil = movil.Substring(resto, ciudadTemp.LongCelular);// "591 31781606"
                                }
                            }
                            else
                            {
                                movil = "00" + movil;
                            }

                        }
                        else
                        {
                            if (movil.StartsWith("51"))
                            {
                                var regex = new Regex(Regex.Escape("51"));
                                movil = regex.Replace(movil, "", 1);
                            }
                        }
                    }
                }
                else
                {
                    ciudadTemp = _ciudadRepositorio.FirstBy(a => a.Id == 2370);
                    pais = ciudadTemp.IdPais;
                    region = ciudadTemp.Id;
                    this.CiudadFacebook = ciudadStringTemp;
                }

                string areaFormacion = QuitarCaracteres(leadgenInformacionDTO.AreaFormacion);
                string industria = QuitarCaracteres(leadgenInformacionDTO.Industria);
                string areaTrabajo = QuitarCaracteres(leadgenInformacionDTO.AreaTrabajo);
                string cargo = QuitarCaracteres(leadgenInformacionDTO.Cargo);
                string cc = ObtenerCentroCosto(leadgenInformacionDTO.AdsetName);
                string tiempoCapacitacion = QuitarCaracteres(leadgenInformacionDTO.InicioCapacitacion);
                string programaGeneral = QuitarCaracteres(leadgenInformacionDTO.CondicionalPregunta1);
                string modalidad = QuitarCaracteres(leadgenInformacionDTO.CondicionalPregunta2);

                if (leadgenInformacionDTO.FormularioRemarketing)
                {
                    AlumnoBO alumno = _alumnoRepositorio.FirstBy(x => x.Email1 == leadgenInformacionDTO.Email);
                    if (alumno != null)
                    {
                        this.IdAreaFormacion = alumno.IdAformacion == null ? null : alumno.IdAformacion;
                        this.IdAreaTrabajo = alumno.IdAtrabajo == null ? null : alumno.IdAtrabajo;
                        this.IdIndustria = alumno.IdIndustria == null ? null : alumno.IdIndustria;
                        this.IdCargo = alumno.IdCargo == null ? null : alumno.IdCargo;
                    }
                    else
                    {
                        this.IdAreaFormacion = null;
                        this.IdAreaTrabajo = null;
                        this.IdIndustria = null;
                        this.IdCargo = null;
                    }
                }
                else
                {
                    var objAreaFormacion = _areaFormacionRepositorio.FirstBy(x => x.Nombre == areaFormacion, s => new { s.Id });
                    if (objAreaFormacion != null) this.IdAreaFormacion = objAreaFormacion.Id;
                    else this.IdAreaFormacion = 0;
                    this.IdAreaTrabajo = _areaTrabajoRepositorio.FirstBy(x => x.Nombre == areaTrabajo).Id;
                    this.IdIndustria = industria == "x" ? this.IdIndustria : _industriaRepositorio.FirstBy(x => x.Nombre == industria).Id;
                    this.IdCargo = _cargoRepositorio.FirstBy(x => x.Nombre == cargo).Id;
                }

                var objCentroCosto = _centroCostoRepositorio.FirstBy(x => x.Nombre.Contains(cc), s => new { s.Id });
                if (objCentroCosto != null) this.IdCentroCosto = objCentroCosto.Id;

                PgeneralIdPaginaDTO pgeneralIdPaginaDTO = _pgeneralRepositorio.ObtenerIdPagina(this.IdCentroCosto ?? 0);
                if (pgeneralIdPaginaDTO != null) this.IdPagina = pgeneralIdPaginaDTO.IdPagina;

                var objTiempoCapacitacion = _tiempoCapacitacionRepositorio.FirstBy(a => a.Nombre.ToUpper().Equals(tiempoCapacitacion.ToUpper()), s => new { s.Id });
                if (objTiempoCapacitacion != null) this.IdTiempoCapacitacion = objTiempoCapacitacion.Id;
                else this.IdTiempoCapacitacion = 0;

                if (programaGeneral != "x" && modalidad != "x")
                {
                    PespecificoCentroCostoDTO pespecificoCentroCostoDTO = _pespecificoRepositorio.ObtenerCentroCostoPresencial(programaGeneral, modalidad);
                    if (pespecificoCentroCostoDTO == null) pespecificoCentroCostoDTO = _pespecificoRepositorio.ObtenerCentroCostoOnline(programaGeneral);
                    this.IdCentroCosto = pespecificoCentroCostoDTO.IdCentroCosto;
                }

                this.Correo = leadgenInformacionDTO.Email.Trim();
                this.IdFaseOportunidad = 2;
                this.Fijo = "";
                this.Movil = movil;
                this.NombrePrograma = cc;

                try
                {
                    var centroCostoFinal = _centroCostoRepositorio.ObtenerCentrosCostoPorNombre(this.NombrePrograma);

                    if (centroCostoFinal != null)
                    {
                        if (this.IdCentroCosto != null && this.IdCentroCosto != centroCostoFinal.IdCentroCosto)
                        {
                            this.IdCentroCosto = centroCostoFinal.IdCentroCosto;
                        }
                    }
                }
                catch (Exception e)
                {
                }


                this.Nombres = nombresApellidosSeparados[0];
                this.Apellidos = nombresApellidosSeparados[1];

                if (this.IdTiempoCapacitacion != 0 && leadgenInformacionDTO.FormularioRemarketing == false)
                {
                    this.Origen = "Fomulario Facebook 5 Campos";
                    this.IdCategoriaDato = ValorEstatico.IdFacebookFormulario5Campos;
                }

                if (this.IdTiempoCapacitacion == 0 && leadgenInformacionDTO.FormularioRemarketing == false)
                {
                    this.Origen = "Fomulario Facebook 3 Campos";
                    this.IdCategoriaDato = ValorEstatico.IdFacebookFormulario3Campos;
                }

                if (leadgenInformacionDTO.FormularioMultiple)
                {
                    this.Origen = "Facebook Multiple Formulario Facebook";
                    this.IdCategoriaDato = ValorEstatico.IdFacebookMultipleFormulario;
                }

                if (leadgenInformacionDTO.FormularioRemarketing)
                {
                    this.Origen = "Facebook Remarketing Formulario Facebook";
                    this.IdCategoriaDato = ValorEstatico.IdFacebookRemarketingFormulario;
                }

                this.IdTipoInteraccion = _tipoInteraccionRepositorio.FirstBy(x => x.Nombre == "Paso - 1").Id;
                this.IdPais = pais;
                this.IdCiudad = region;
                this.IdTipoDato = ValorEstatico.IdTipoDatoLanzamiento;
                this.FechaRegistroCampania = leadgenInformacionDTO.created_time;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Este asignacionAutomaticaTemp se Procesara
        /// </summary>
        /// <returns></returns>
        public AsignacionAutomaticaBO ProcesarAsignacionAutomaticaTemp()
        {
            int idElSalvador = 503;
            string elSalvadorIniciales = "SAL";
            var configuraciones = _repAsignacionAutomaticaConfiguracion.GetAllHabilitado();
            var Inclusion = configuraciones.Where(o => o.Inclusivo == true).ToList();
            var Exclusion = configuraciones.Where(o => o.Inclusivo == false).ToList();
            var listaOrigen = new Dictionary<string, string>();
            var origenes = _repOrigen.ObtenerTodoFiltro();
            foreach (var origen in origenes)
            {
                if (!listaOrigen.ContainsKey(origen.Nombre.Trim().ToUpper()))
                {
                    listaOrigen.Add(origen.Nombre.Trim().ToUpper(), origen.Id.ToString());
                }
            }
            var listaPaises = new Dictionary<int, string>();
            var paises = _repPais.ObtenerTodoFiltro();
            foreach (var pais in paises)
            {
                if (pais.Codigo == idElSalvador)
                {
                    listaPaises.Add(pais.Codigo, elSalvadorIniciales);
                }
                else
                {
                    listaPaises.Add(pais.Codigo, pais.Nombre.Substring(0, 3).ToUpper());
                }
            }

            AsignacionAutomaticaBO asignacionAutomaticaProcesado = this.PreProcesar(listaOrigen, listaPaises);
            //Si cumple con las reglas de configuracion de la asignacion automatica, insertamos un nuevo registro para la validacion
            if (AplicarConfiguracion(asignacionAutomaticaProcesado, Inclusion, Exclusion))
            {
                //Actualizamos temporal como procesado
                this.Procesado = true;
                //this.Actualizar();
                //Insertamos en la lista de Registros para la validacion
                asignacionAutomaticaProcesado.Validado = false;
                asignacionAutomaticaProcesado.Corregido = false;
                asignacionAutomaticaProcesado.IdAsignacionAutomaticaOrigen = AsignacionAutomaticaOrigenBO.PortalWeb;
                asignacionAutomaticaProcesado.IdCategoriaOrigen = this.IdCategoriaDato;
            }
            return asignacionAutomaticaProcesado;
        }

        /// <summary>
        /// Valida si se debe aplicar la configuracion definida
        /// </summary>
        /// <param name="AsignacionAutomatica"></param>
        /// <param name="Inclusion"></param>
        /// <param name="Exclusion"></param>
        /// <returns>Bool</returns>
        public bool AplicarConfiguracion(AsignacionAutomaticaBO AsignacionAutomatica, List<AsignacionAutomaticaConfiguracionBO> Inclusion, List<AsignacionAutomaticaConfiguracionBO> Exclusion)
        {
            var Vacio = 0;
            foreach (var Config in Inclusion)
            {
                if (!Config.IdFaseOportunidad.Equals(Vacio) && !Config.IdFaseOportunidad.Equals(AsignacionAutomatica.IdFaseOportunidad)) return false;
                if (!Config.IdOrigen.Equals(Vacio) && !Config.IdOrigen.Equals(AsignacionAutomatica.IdOrigen)) return false;
                if (!Config.IdTipoDato.Equals(Vacio) && !Config.IdTipoDato.Equals(AsignacionAutomatica.IdTipoDato)) return false;
            }
            foreach (var config in Exclusion)
            {
                if (!config.IdFaseOportunidad.Equals(Vacio) && config.IdFaseOportunidad.Equals(AsignacionAutomatica.IdFaseOportunidad)) return false;
                if (!config.IdOrigen.Equals(Vacio) && config.IdOrigen.Equals(AsignacionAutomatica.IdOrigen)) return false;
                if (!config.IdTipoDato.Equals(Vacio) && config.IdTipoDato.Equals(AsignacionAutomatica.IdTipoDato)) return false;
            }
            return true;
        }

        public AsignacionAutomaticaBO PreProcesar(Dictionary<string, string> listaOrigen, Dictionary<int, string> listaPais)
        {
            AsignacionAutomaticaBO AsignacionAutomatica = new AsignacionAutomaticaBO
            {
                IdPagina = this.IdPagina
            };
            string prenombres = System.Text.RegularExpressions.Regex.Replace(this.Nombres, @"\s+", " ");
            var nombres = _parser.ParserCaracteres(prenombres).Split(new char[] { ' ' }).ToList()
                .Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();
            if (nombres.Count == 1)
            {
                AsignacionAutomatica.Nombre1 = nombres.FirstOrDefault();
                AsignacionAutomatica.Nombre2 = "";
            }
            else if (nombres.Count == 2)
            {
                AsignacionAutomatica.Nombre1 = nombres.FirstOrDefault();
                AsignacionAutomatica.Nombre2 = nombres[1];
            }
            else if (nombres.Count > 2)
            {
                AsignacionAutomatica.Nombre1 = String.Join(" ", nombres.ToArray());
                AsignacionAutomatica.Nombre2 = "";
            }
            string preapellidos = System.Text.RegularExpressions.Regex.Replace(this.Apellidos, @"\s+", " ");
            var apellidos = _parser.ParserCaracteres(preapellidos).Split(new char[] { ' ' }).ToList()
                .Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();
            if (apellidos.Count == 1)
            {
                AsignacionAutomatica.ApellidoPaterno = apellidos.FirstOrDefault();
                AsignacionAutomatica.ApellidoMaterno = "";
            }
            else if (apellidos.Count == 2)
            {
                AsignacionAutomatica.ApellidoPaterno = apellidos.FirstOrDefault();
                AsignacionAutomatica.ApellidoMaterno = apellidos[1];
            }
            else if (apellidos.Count > 2)
            {
                AsignacionAutomatica.ApellidoPaterno = String.Join(" ", apellidos.ToArray());
                AsignacionAutomatica.ApellidoMaterno = "";
            }
            else
            {
                AsignacionAutomatica.ApellidoPaterno = "";
                AsignacionAutomatica.ApellidoMaterno = "";
            }
            StringBuilder origen = new StringBuilder();
            var categoriadato = repCategoriaOrigen.ObtenerCategoriaOrigenSubCategoriaDato(Convert.ToInt32(this.IdCategoriaDato), Convert.ToInt32(this.IdTipoInteraccion));
            AsignacionAutomatica.IdSubCategoriaDato = (categoriadato != null || categoriadato.IdSubCategoriaDato != 0) ? categoriadato.IdSubCategoriaDato : 0;
            AsignacionAutomatica.IdCategoriaDato = this.IdCategoriaDato;// fk_categoriadato;
            AsignacionAutomatica.IdTipoInteraccion = this.IdTipoInteraccion;
            AsignacionAutomatica.IdInteraccionFormulario = this.IdInteraccionFormulario;
            AsignacionAutomatica.UrlOrigen = this.UrlOrigen;
            origen.Append("LAN").Append(listaPais[Convert.ToInt32(this.IdPais)]).Append(categoriadato.CodigoOrigen != null ? categoriadato.CodigoOrigen.ToUpper() : "");
            if (AsignacionAutomatica.IdSubCategoriaDato == 1567 || AsignacionAutomatica.IdSubCategoriaDato == 1925)//TODO
            {
                if (AsignacionAutomatica.IdSubCategoriaDato == 1567)
                    AsignacionAutomatica.IdOrigen = 114;//Chat
                if (AsignacionAutomatica.IdSubCategoriaDato == 1925)
                    AsignacionAutomatica.IdOrigen = 132;//Chat Offline
            }
            else
            {
                var origenNombre = origen.ToString().ToUpper();
                if (!listaOrigen.ContainsKey(origenNombre))
                {
                    AsignacionAutomatica.IdOrigen = 0;
                }
                else
                {
                    AsignacionAutomatica.IdOrigen = Convert.ToInt32(listaOrigen[origenNombre]);
                }
            }
            AsignacionAutomatica.OrigenCampania = this.Origen;
            AsignacionAutomatica.IdTipoDato = this.IdTipoDato;
            AsignacionAutomatica.IdFaseOportunidad = this.IdFaseOportunidad;
            AsignacionAutomatica.Email = this.Correo.Trim();
            AsignacionAutomatica.IdCentroCosto = this.IdCentroCosto;
            AsignacionAutomatica.NombrePrograma = this.NombrePrograma;
            AsignacionAutomatica.IdCargo = this.IdCargo;
            AsignacionAutomatica.IdIndustria = this.IdIndustria;
            AsignacionAutomatica.IdAreaFormacion = this.IdAreaFormacion;
            AsignacionAutomatica.IdAreaTrabajo = this.IdAreaTrabajo;
            AsignacionAutomatica.Celular = MapeadorReplace.MapTelefonoCelular(this.Movil);
            AsignacionAutomatica.Telefono = this.Fijo;
            AsignacionAutomatica.IdPais = this.IdPais;
            AsignacionAutomatica.IdCiudad = this.IdCiudad;
            AsignacionAutomatica.IdConjuntoAnuncio = this.IdConjuntoAnuncio;
            AsignacionAutomatica.FechaRegistroCampania = this.FechaRegistroCampania;
            if (this.IdPais == 0 && this.IdCiudad == 2370)
            {
                AsignacionAutomatica.FechaCreacion = Convert.ToDateTime(this.CiudadFacebook);
            }
            AsignacionAutomatica.IdTiempoCapacitacion = this.IdTiempoCapacitacion;
            AsignacionAutomatica.IdPagina = this.IdPagina;
            return AsignacionAutomatica;
        }

        public void ProcesarRegistroFormularioPortalWeb(string idRegistroPortalWeb, int idPagina)
        {
            //Traemos el registro de la pagina web y lo guardamos de manera temporal
            AsignacionAutomaticaTempDTO registro = repAsignacionAutomaticaTemp.GetNuevosRegistroById(idRegistroPortalWeb, idPagina);
            if (registro == null)
            {
                throw new Exception("Registro no se encontro o ya fue procesado");
            }
            else
            {
                MapearAsignacionAutomaticaTemp(registro);
                FechaCreacion = DateTime.Now;
                FechaModificacion = DateTime.Now;
                Estado = true;
                UsuarioCreacion = "Signal";
                UsuarioModificacion = "Signal";
            }
        }

        /// <summary>
        /// Procesa el registro del formulario nuevo del portal web
        /// </summary>
        /// <param name="idRegistroPortalWeb">Id del registro del portal web</param>
        /// <param name="idPagina">Id de la pagina de donde proviene el dato</param>
        public void ProcesarRegistroFormularioNuevoPortalWeb(string idRegistroPortalWeb, int idPagina)
        {
            //Traemos el registro de la pagina web y lo guardamos de manera temporal
            AsignacionAutomaticaTempDTO registro = repAsignacionAutomaticaTemp.ObtenerNuevosRegistroById(idRegistroPortalWeb, idPagina);
            if (registro == null)
            {
                throw new Exception("Registro no se encontro o ya fue procesado");
            }
            else
            {
                MapearAsignacionAutomaticaTemp(registro);
                FechaCreacion = DateTime.Now;
                FechaModificacion = DateTime.Now;
                Estado = true;
                UsuarioCreacion = "Signal";
                UsuarioModificacion = "Signal";
            }
        }

        /// <summary>
        /// Marca como procesado el dato entrante
        /// </summary>
        /// <param name="procesados">Array de cadena con los elementos procesados</param>
        /// <param name="idPagina">Id de la pagina de donde proviene el dato</param>
        public void MarcarComoProcesados(string[] procesados, int idPagina)
        {
            foreach (string procesado in procesados)
            {
                try
                {
                    // repAsignacionAutomaticaTemp.MarcarComoProcesado(Procesado, IdPagina);

                    string URI = "http://localhost:4348/portal/MarcarComoProcesado?idFaseOportunidadPortal=" + procesado;
                    //string URI = "http://localhost:4348/portal/MarcarComoProcesado?idFaseOportunidadPortal=" + procesado;

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(URI);
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 17/03/2021
        /// Version: 1.0
        /// <summary>
        /// Mapea Oportunidades a Asignacion Automatica Temp 
        /// </summary>
        /// <param name="nuevo"> Lista de oportunidades del portal web </param>  
        public void MapearAsignacionAutomaticaTemp(AsignacionAutomaticaTempDTO nuevo)
        {
            this.Id = 0;
            this.Nombres = nuevo.Nombres;
            this.Apellidos = nuevo.Apellidos;
            this.IdAreaFormacion = nuevo.IdAreaFormacion;
            this.IdAreaTrabajo = nuevo.IdAreaTrabajo;
            this.IdCargo = nuevo.IdCargo;

            if (nuevo.CentroCosto == null || nuevo.CentroCosto.Equals(""))
            {
                bool esAdwords = EsAdwords((int)nuevo.IdCategoriaOrigen);

                if (esAdwords)
                {
                    if (nuevo.Campania != null)
                    {
                        var centroCostoAdws = repCategoriaOrigen.ObtenerCentroCostoPorCampania(nuevo.Campania);
                        this.CentroCosto = centroCostoAdws.CentroCosto;
                        nuevo.IdCentroCosto = centroCostoAdws.IdCentroCosto;
                    }
                    else if (nuevo.IdConjuntoAnuncio != null)
                    {
                        var nombreCampania = repAsignacionAutomaticaTemp.ObtenerNombreCampaniaPorIdFaseOportunidad(nuevo.IdFaseOportunidadPortal);
                        var centroCostoPorCampaniaAdws = repCategoriaOrigen.ObtenerCentroCostoPorNombreIdConjuntoAnuncio((int)nombreCampania.IdConjuntoAnuncio, nombreCampania.NombreCampania);
                        this.CentroCosto = centroCostoPorCampaniaAdws.CentroCosto;
                        nuevo.IdCentroCosto = centroCostoPorCampaniaAdws.IdCentroCosto;
                    }
                    else
                    {
                        this.CentroCosto = "REGISTRO CENTRO DE COSTO 2020 I LIMA";
                    }
                }
                else
                {
                    this.CentroCosto = "REGISTRO CENTRO DE COSTO 2020 I LIMA";
                }
            }
            else
            {
                this.CentroCosto = nuevo.CentroCosto;
            }

            if (nuevo.IdCentroCosto == null || nuevo.IdCentroCosto.Value == 0)
            {
                this.IdCentroCosto = 15907;
            }
            else
            {
                this.IdCentroCosto = nuevo.IdCentroCosto;
            }

            this.Correo = nuevo.Correo;
            this.IdFaseOportunidad = nuevo.IdFaseOportunidad;
            this.Fijo = nuevo.Fijo;
            this.IdIndustria = nuevo.IdIndustria;
            this.Movil = nuevo.Movil;
            this.NombrePrograma = nuevo.NombrePrograma;
            this.Origen = nuevo.IdOrigen;
            this.IdPais = nuevo.Pais == null ? 0 : Convert.ToInt32(nuevo.Pais);
            this.Procesado = false;
            this.IdCiudad = nuevo.Ciudad == null ? 0 : Convert.ToInt32(nuevo.Ciudad);
            this.IdTipoDato = nuevo.IdTipoDato;
            this.IdConjuntoAnuncio = nuevo.IdConjuntoAnuncio;

            if (nuevo.FechaRegistroCampania != null)
            {
                try
                {
                    this.FechaRegistroCampania = Convert.ToDateTime(nuevo.FechaRegistroCampania);//???
                }
                catch (Exception)
                {
                    this.FechaRegistroCampania = DateTime.Now;
                }
            }
            else
            {
                this.FechaRegistroCampania = DateTime.Now;
            }
            if (Guid.TryParse(nuevo.IdFaseOportunidadPortal, out Guid idFaseOportunidadPortal))
            {
                this.IdFaseOportunidadPortal = idFaseOportunidadPortal;
            }
            this.IdTipoInteraccion = nuevo.IdTipoInteraccion;
            this.IdCategoriaDato = nuevo.IdCategoriaOrigen;
            this.IdInteraccionFormulario = nuevo.IdInteraccionFormulario;
            this.UrlOrigen = nuevo.UrlOrigen;
            this.IdTiempoCapacitacion = nuevo.IdTiempoCapacitacion;
            this.IdPagina = nuevo.IdPagina;
            this.FechaCreacion = DateTime.Now;
            this.FechaModificacion = DateTime.Now;
            this.UsuarioCreacion = "System";
            this.UsuarioModificacion = "System";
            this.Estado = true;
        }

        public string QuitarCaracteres(string cadena)
        {
            if (cadena != null)
            {
                cadena = cadena.Replace("_", " ").Trim();
            }
            return cadena;
        }
        public string ObtenerCentroCosto(string cadena)
        {
            char delimiter = '-';
            string[] substrings = cadena.Split(delimiter);
            return QuitarEspacios(substrings[0]);
        }
        public string QuitarEspacios(string cadena)
        {
            string tmp = cadena;
            tmp = cadena.Trim();
            return tmp;
        }
        public string ObtenerNumeroTelefonico(string numero)
        {
            StringBuilder nuevoNumero = new StringBuilder();
            for (var i = 0; i < numero.Length; i++)
            {
                if (char.IsNumber(numero[i]))
                {
                    nuevoNumero.Append(numero[i].ToString());
                }
            }
            return nuevoNumero.ToString();
        }

        public string QuitarCerosIzquierda(string numero)
        {
            int i = 0;
            for (; i < numero.Length; i++)
                if (numero[i] != '0') break;

            return numero.Substring(i);
        }

        /// Autor: Jose Villena
        /// Fecha: 17/03/2021
        /// Version: 1.0
        /// <summary>
        /// Valida si es oportunidad de Categoria Adwords
        /// </summary>
        /// <param name="idCategoriaOrigen"> Id Categoria Origen </param>        
        /// <returns></returns>    
        static bool EsAdwords(int idCategoriaOrigen)
        {
            CategoriaOrigenRepositorio repCategoriaOrigen = new CategoriaOrigenRepositorio();
            var listaCategoriasAdwords = repCategoriaOrigen.ObtenerCategoriaOrigenAdwords();
            bool flag = false;
            foreach (var item in listaCategoriasAdwords)
            {
                if (item.Id == idCategoriaOrigen)
                {
                    flag = true;
                    break;
                }
                else
                {
                    continue;
                }
            }
            if (flag)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<string> ProcesarNombre(string fullName)
        {
            List<string> nombresApellidos = new List<string>();
            fullName = fullName.ToLower();
            fullName = fullName.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o')
                        .Replace('ú', 'u').Replace('ñ', 'n');
            char delimiter = ' ';
            string[] substrings = fullName.Split(delimiter);
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            for (int i = 0; i < substrings.Length; i++)
            {
                if (!string.IsNullOrEmpty(substrings[i]))
                {
                    nombresApellidos.Add(myTI.ToTitleCase(substrings[i]));
                }
            }
            List<string> nombresApellidosSeparados = new List<string>();
            return SepararNombresApellidos(nombresApellidos);

        }
        public List<string> SepararNombresApellidos(List<string> lista)
        {
            List<string> NombresApellidos = new List<string>();
            switch (lista.Count)
            {
                case 1:
                    NombresApellidos.Add(lista[0]);
                    NombresApellidos.Add(lista[0]);
                    break;
                case 2:
                    NombresApellidos.Add(lista[0]);
                    NombresApellidos.Add(lista[1]);
                    break;
                case 3:
                    NombresApellidos.Add(lista[0]);
                    NombresApellidos.Add(lista[1] + " " + lista[2]);
                    break;
                case 4:
                    NombresApellidos.Add(lista[0] + " " + lista[1]);
                    NombresApellidos.Add(lista[2] + " " + lista[3]);
                    break;
                default:
                    string nombres = "";
                    for (int i = 0; i < lista.Count - 2; i++)
                    {
                        if (i == 0)
                            nombres = lista[i];
                        else
                            nombres = nombres + " " + lista[i];
                    }
                    NombresApellidos.Add(nombres);
                    NombresApellidos.Add(lista[lista.Count - 2] + " " + lista[lista.Count - 1]);
                    break;
            }
            return NombresApellidos;
        }
    }
}
