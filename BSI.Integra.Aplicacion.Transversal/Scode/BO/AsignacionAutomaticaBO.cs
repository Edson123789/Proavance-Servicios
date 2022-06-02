using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.Validador;
using BSI.Integra.Aplicacion.Transversal.DTO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System.Text.RegularExpressions;
using BSI.Integra.Aplicacion.Transversal.Tools;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: AsignacionAutomatica
    ///Autor: Gian Miranda
    ///Fecha: 07/05/2021
    ///<summary>
    ///Columnas y funciones de la tabla mkt.T_AsignacionAutomatica
    ///</summary>
    public class AsignacionAutomaticaBO : BaseBO
    {
        ///Propiedades		                        Significado
        ///-------------	                        -----------------------
        ///Nombre1                                  Primer nombre del dato entrante
        ///Nombre2                                  Segundo nombre del dato entrante
        ///ApellidoPaterno                          Apellido paterno del dato entrante
        ///ApellidoMaterno                          Apellido materno del dato entrante
        ///Telefono                                 Telefono del dato entrante
        ///Celular                                  Celular del dato entrante
        ///Email                                    Email del dato entrante
        ///IdCentroCosto                            Id del centro de costo del dato entrante (PK de la tabla pla.T_CentroCosto)
        ///NombrePrograma                           Nombre del centro de costo del dato entrante
        ///IdTipoDato                               Id del tipo de dato (PK de la tabla mkt.T_TipoDato)
        ///IdOrigen                                 Id del origen (PK de la tabla mkt.T_Origen)
        ///IdFaseOportunidad                        Id de la fase de oportunidad (PK de la tabla pla.T_FaseOportunidad)
        ///IdAreaFormacion                          Id del area de formacion (PK de la tabla pla.T_AreaFormacion)
        ///IdAreaTrabajo                            Id del area de trabajo (PK de la tabla pla.T_AreaTrabajo)
        ///IdIndustria                              Id de la industria (PK de la tabla pla.T_Industria)
        ///IdCargo                                  Id del cargo (PK de la tabla pla.T_Cargo)
        ///IdPais                                   Id del pais (PK de la tabla conf.T_Pais)
        ///IdCiudad                                 Id de la ciudad (PK de la tabla conf.T_Ciudad)
        ///Validado                                 Flag de validado de la asignacion automatica
        ///Corregido                                Flag de corregido de la asignacion automatica
        ///OrigenCampania                           Campania origen del dato entrante
        ///IdConjuntoAnuncio                        Id del conjunto anuncio(PK de la tabla mkt.T_ConjuntoAnuncio)
        ///IdCategoriaOrigen                        Id de la categoria de origen (PK de la tabla mkt.T_CategoriaOrigen)
        ///IdAsignacionAutomaticaOrigen             Id de la asignacion automatica origen
        ///IdAsignacionAutomaticaTemp               Id de la asignacion automatica temporal (PK de la tabla T_AsignacionAutomatica_Temp)
        ///IdCampaniaScoring                        Id del scoring de la campania
        ///FechaRegistroCampania                    Fecha de registro de la campania
        ///IdFaseOportunidadPortal                  Id de la fase oportunidad portal (PK de la tabla dbo.T_FaseOportunidadPortal - Portal Web)
        ///IdOportunidad                            Id de la oportunidad (PK de la tabla com.T_Oportunidad)
        ///IdPersonal                               Id del personal (PK de la tabla gp.T_Personal)
        ///IdTiempoCapacitacion                     Id del tiempo de capacitacion (PK de la tabla mkt.T_TiempoCapacitacion)
        ///IdCategoriaDato                          Id de la categoria dato (PK de la tabla mkt.T_CategoriaOrigen)
        ///IdTipoInteraccion                        Id del tipo de interaccion (PK de la tabla mkt.T_TipoInteracccion)
        ///IdSubCategoriaDato                       Id de la subcategoria dato (PK de la tabla mkt.T_SubCategoriaDato)
        ///IdInteraccionFormulario                  Id de la interaccion del formulario (PK de la tabla mkt.T_InteraccionFormulario)
        ///UrlOrigen                                Cadena Url del origen si es que se logra extraer
        ///ProbabilidadActual                       Probabilidad actual del dato
        ///ProbabilidadActualDesc                   Probabilidad actual desc del dato
        ///IdPagina                                 Id de la pagina de donde proviene el dato
        ///FechaProgramada                          Fecha de programacion del dato entrante
        ///IdAlumno                                 Id del alumno (PK de la tabla mkt.T_Alumno)
        ///IdMigracion                              Id de migracion (Campo nullable)
        ///idClasificacionPersona                   Id de la clasificacion persona (PK de la tabla conf.T_ClasificacionPersona)
        ///Error                                    Cadena con el error al momento de procesar, si es que hubiera
        ///AptoProcesamiento                        Flag para determinar si es apto para el procesamiento o no
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int? IdCentroCosto { get; set; }
        public string NombrePrograma { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdCargo { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public bool? Validado { get; set; }
        public bool? Corregido { get; set; }
        public string OrigenCampania { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdAnuncioFacebook { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdAsignacionAutomaticaOrigen { get; set; }
        public int? IdAsignacionAutomaticaTemp { get; set; }
        public int? IdCampaniaScoring { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public Guid? IdFaseOportunidadPortal { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdCategoriaDato { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public string UrlOrigen { get; set; }
        public double? ProbabilidadActual { get; set; }
        public string ProbabilidadActualDesc { get; set; }
        public int? IdPagina { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public int? IdAlumno { get; set; }
        public Guid? IdMigracion { get; set; }

        public int? idClasificacionPersona { get; set; }

        public string Error { get; set; }
        public bool AptoProcesamiento { get; set; } = true;

        private AsignacionAutomaticaErrorBO _asignacionAutomaticaError;
        private AsignacionAutomaticaTipoErrorBO _asignacionAutomaticaTipoError;
        private BloqueHorarioProcesaOportunidadBO _bloqueHorarioProcesaOportunidad;
        private PersonaBO _persona;

        private ValidadorBO _validor;
        private AlumnoBO _alumno;

        integraDBContext contexto;
        private DapperRepository _dapperRepository;


        private AsignacionAutomaticaTempRepositorio _repAsignacionAutomaticaTemp;
        private CategoriaOrigenRepositorio _repCategoriaOrigen;
        private Parser _parser;
        private CiudadRepositorio _repCiudad;
        private PaisRepositorio _repPais;
        private OrigenRepositorio _repOrigen;
        private AsignacionAutomaticaRepositorio _repAsignacionAutomatica;

        public AsignacionAutomaticaBO()
        {
            contexto = new integraDBContext();
            _dapperRepository = new DapperRepository(contexto);
            _repCiudad = new CiudadRepositorio();
            _repPais = new PaisRepositorio();
            _repOrigen = new OrigenRepositorio();
            _asignacionAutomaticaError = new AsignacionAutomaticaErrorBO();
            _asignacionAutomaticaTipoError = new AsignacionAutomaticaTipoErrorBO();
            _bloqueHorarioProcesaOportunidad = new BloqueHorarioProcesaOportunidadBO();
            _validor = new ValidadorBO();
            _alumno = new AlumnoBO();
            _repAsignacionAutomaticaTemp = new AsignacionAutomaticaTempRepositorio();
            _repCategoriaOrigen = new CategoriaOrigenRepositorio();
            _parser = new Parser();
        }


        /// <summary>
        /// Obtiene una oportunidad de asignacion automatica y errores por IdFaseOportunidadPortal
        /// </summary>
        /// <param name="idFaseOportunidadPortal"></param>
        /// <returns></returns>
        public AsignacionAutomaticaBO ObtenerValoresOportunidaAsignacionAutomaticaErrorPorIdFaseOportunidadPortal(string idFaseOportunidadPortal)
        {
            AsignacionAutomaticaBO asignacionAutomatica = new AsignacionAutomaticaBO();
            var _query = "SELECT IdOportunidad, Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno, Telefono, Celular, Email, IdPais, IdCiudad, IdCargo, IdAreaFormacion, IdAreaTrabajo, IdIndustria, IdCentroCosto, IdTipoDato, IdFaseOportunidad, IdOrigen, IdEmpresa, IdFaseOportunidadPortal, ERROR FROM com.V_ObtenerAsignacionAutomaticaYErroresPorIdFaseOportunidadPortal WHERE IdFaseOportunidadPortal = @idFaseOportunidadPortal";
            var asignacionAutomaticaDB = _dapperRepository.FirstOrDefault(_query, new { idFaseOportunidadPortal });
            if (asignacionAutomaticaDB != null || !asignacionAutomaticaDB.Contains("[]"))
            {
                asignacionAutomatica = JsonConvert.DeserializeObject<AsignacionAutomaticaBO>(asignacionAutomaticaDB);
            }
            return asignacionAutomatica;
        }

        private AsignacionAutomaticaTempDTO GetNuevosRegistroById(string idRegistroPortalWeb)
        {
            AsignacionAutomaticaTempDTO Registro = new AsignacionAutomaticaTempDTO();
            var RegistroDB = _dapperRepository.QuerySPFirstOrDefault("dbo.SP_GetContactoFaseOportunidadPWById", new { idRegistroPortalWeb });
            Registro = JsonConvert.DeserializeObject<AsignacionAutomaticaTempDTO>(RegistroDB);
            return Registro;
        }

        /// <summary>
        /// Obtiene una nuevos registros para asignacionautomaticatemp
        /// </summary>
        /// <returns>Lista de objetos de clase AsignacionAutomaticaTempDTO</returns>
        public List<AsignacionAutomaticaTempDTO> ObtenerNuevosRegistros()
        {
            List<AsignacionAutomaticaTempDTO> Registros = new List<AsignacionAutomaticaTempDTO>();
            //ANTIGUO V3//var RegistroDB = _dapperRepository.QuerySPDapper("dbo.SP_GetContactoFaseOportunidadPW", new { Id = 1 });
            var RegistroDB = _dapperRepository.QuerySPDapper("dbo.SP_GetContactoFaseOportunidadPWNuevo", new { Id = 1 });
            Registros = JsonConvert.DeserializeObject<List<AsignacionAutomaticaTempDTO>>(RegistroDB);
            return Registros;
        }

        /// <summary>
        /// Se conecta a la base de datos del portal web y actualiza en estado procesado a true de un registro
        /// </summary>
        /// <param name="idsFaseOportunidadPortal"></param>
        /// <param name="idPagina"></param>
        public void MarcarComoProcesados(string[] idsFaseOportunidadPortal)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                foreach (string idFaseOportunidadPortal in idsFaseOportunidadPortal)
                {
                    _dapperRepository.QuerySPFirstOrDefault("dbo.SP_UpdateFaseOportunidadPortal", new { IdFaseOportunidadPortal = idFaseOportunidadPortal });
                }
                Scope.Complete();
            }
        }

        /// Autor: Carlos Crispin R.
        /// Fecha: 07/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Realiza validaciones al BO para generar el registro en asignacion automatica
        /// </summary>
        /// <param name="idAsignacionAutomaticaTemp">Id de la asignacion automatica temporal (PK de la tabla mkt.T_AsignacionAutomatica_Temp)</param>
        /// <param name="listaOrigenes">Lista de origenes a analizar (PK de la tabla mkt.T_Origen)</param>
        /// <param name="listaPaises">Lista de paises a analizar (PK de la tabla conf.T_Pais)</param>
        public void ValidarRegistroFormularioAsignacionAutomaticaTemp(int idAsignacionAutomaticaTemp, Dictionary<int, string> listaPaises, Dictionary<string, OrigenesCategoriaOrigenDTO> listaOrigenes)
        {
            var registro = _repAsignacionAutomaticaTemp.FirstById(idAsignacionAutomaticaTemp);
            if (registro != null)
            {
                string preNombres = System.Text.RegularExpressions.Regex.Replace(registro.Nombres, @"\s+", " ");
                var nombres = _parser.ParserCaracteres(preNombres).Split(new char[] { ' ' }).ToList()
                .Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();
                //Nombres
                if (nombres.Count == 1)
                {
                    this.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                    this.Nombre2 = string.Empty;
                }
                else if (nombres.Count == 2)
                {
                    this.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                    this.Nombre2 = nombres[1].Length >= 100 ? nombres[1].Substring(0, 100) : nombres[1];
                }
                else if (nombres.Count > 2)
                {
                    this.Nombre1 = string.Join(" ", nombres.ToArray()).Length >= 100 ? String.Join(" ", nombres.ToArray()).Substring(0, 100) : String.Join(" ", nombres.ToArray());
                    this.Nombre2 = string.Empty;
                }

                //Apellidos
                registro.Apellidos = registro.Apellidos == null ? "" : registro.Apellidos;
                string preApellidos = System.Text.RegularExpressions.Regex.Replace(registro.Apellidos, @"\s+", " ");
                var apellidos = _parser.ParserCaracteres(preApellidos).Split(new char[] { ' ' }).ToList()
                    .Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();

                if (apellidos.Count == 1)
                {
                    this.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                    this.ApellidoMaterno = string.Empty;
                }
                else if (apellidos.Count == 2)
                {
                    this.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                    this.ApellidoMaterno = apellidos[1].Length >= 100 ? apellidos[1].Substring(0, 100) : apellidos[1];
                }
                else if (apellidos.Count > 2)
                {
                    this.ApellidoPaterno = String.Join(" ", apellidos.ToArray()).Length >= 100 ? String.Join(" ", apellidos.ToArray()).Substring(0, 100) : String.Join(" ", apellidos.ToArray());
                    this.ApellidoMaterno = string.Empty;
                }
                else
                {
                    this.ApellidoPaterno = string.Empty;
                    this.ApellidoMaterno = string.Empty;
                }
                //Celular
                this.Telefono = MapeadorReplace.MapTelefonoCelular(registro.Fijo ?? string.Empty);
                this.Celular = MapeadorReplace.MapTelefonoCelular(registro.Movil);

                string celularTemporal = string.Empty;

                if (Celular.Length == 0)
                    throw new Exception("Celular no valido");
                //eliminar ceros de adelante del numero si esque los hubiera
                int i = 0;
                for (; i < this.Celular.Length && this.Celular[i].Equals('0'); ++i) ;

                try
                {
                    this.Celular = this.Celular.Substring(i).Length > 0 ? this.Celular.Substring(i) : string.Concat("1", new String('0', this.Celular.Length - 1));
                }
                catch (Exception e)
                {
                    this.Celular = this.Celular.Substring(i);
                }

                if (this.Celular.Length == 12 && registro.IdPais == 52 && this.Celular.StartsWith("52"))
                    this.Celular = string.Concat("00", this.Celular);

                // asegurar que el dato tenga pais
                if (registro.IdPais == null)
                {
                    if (this.Celular.Length == 12 && this.Celular.StartsWith("57")) registro.IdPais = this.IdPais = 57;
                    else if (this.Celular.Length == 11 && this.Celular.StartsWith("591")) registro.IdPais = this.IdPais = 591;
                    else if (this.Celular.Length == 11 && this.Celular.StartsWith("51")) registro.IdPais = this.IdPais = 51;
                }

                // eliminar el codigo del pais del celular
                if (this.Celular.Length == 12 && registro.IdPais.Value == 57) this.Celular = this.Celular.Substring(2);
                else if (this.Celular.Length == 11 && registro.IdPais.Value == 591) this.Celular = this.Celular.Substring(3);
                else if (this.Celular.Length == 11 && registro.IdPais.Value == 51) this.Celular = this.Celular.Substring(2);


                this.Email = registro.Correo.Trim();
                this.IdCentroCosto = registro.IdCentroCosto;

                //asegurarse de que tenga un centro de costo por defecto en caso no se haya podido identificar el centro de costo o el usuario no lo haya registrado
                if (this.IdCentroCosto == null || this.IdCentroCosto == 0) this.IdCentroCosto = 15907;  // CC: "REGISTRO CENTRO DE COSTO 2020 I LIMA"
                else this.IdCentroCosto = registro.IdCentroCosto;
                this.NombrePrograma = registro.NombrePrograma;
                this.IdTipoDato = registro.IdTipoDato;

                //Origen
                StringBuilder origen = new StringBuilder();

                registro.IdCategoriaDato = registro.IdCategoriaDato == null ? 18 : registro.IdCategoriaDato;

                var categoriaDato = _repCategoriaOrigen.ObtenerCategoriaOrigenSubCategoriaDato((registro.IdCategoriaDato == null ? 0 : registro.IdCategoriaDato.Value), registro.IdTipoInteraccion == null ? 0 : registro.IdTipoInteraccion.Value);
                this.IdSubCategoriaDato = (categoriaDato != null || categoriaDato.IdSubCategoriaDato != 0) ? categoriaDato.IdSubCategoriaDato : 0;
                this.IdCategoriaDato = registro.IdCategoriaDato;
                this.IdTipoInteraccion = registro.IdTipoInteraccion;
                this.IdInteraccionFormulario = registro.IdInteraccionFormulario;
                this.UrlOrigen = registro.UrlOrigen;


                origen.Append("LAN").Append(listaPaises[registro.IdPais ?? default(int)]).Append(categoriaDato.CodigoOrigen.ToUpper());
                var origenNombre = origen.ToString().ToUpper();
                if (categoriaDato.IdTipoCategoriaOrigen == 16)
                {
                    if (categoriaDato.NombreCategoriaOrigen.Contains("Offline"))
                        this.IdOrigen = 132;
                    else
                        this.IdOrigen = 114;
                }
                else
                {
                    if (!listaOrigenes.ContainsKey(origenNombre))
                    {
                        this.IdOrigen = 0;
                    }
                    else
                    {
                        this.IdOrigen = listaOrigenes[origenNombre].Id;
                    }
                }
                this.OrigenCampania = registro.Origen;
                this.IdTipoDato = registro.IdTipoDato;
                this.IdFaseOportunidad = registro.IdFaseOportunidad;
                this.Email = registro.Correo.Trim();

                this.NombrePrograma = registro.NombrePrograma;
                this.IdCargo = registro.IdCargo;
                this.IdIndustria = registro.IdIndustria;
                this.IdAreaFormacion = registro.IdAreaFormacion;
                this.IdAreaTrabajo = registro.IdAreaTrabajo;
                this.IdPais = registro.IdPais;
                this.IdCiudad = registro.IdCiudad;
                this.IdConjuntoAnuncio = registro.IdConjuntoAnuncio;
                this.IdAnuncioFacebook = registro.IdAnuncioFacebook;
                this.FechaRegistroCampania = registro.FechaRegistroCampania;
                this.IdFaseOportunidadPortal = registro.IdFaseOportunidadPortal;

                this.IdTiempoCapacitacion = registro.IdTiempoCapacitacion;
                this.IdPagina = registro.IdPagina;

                this.Estado = true;
                this.FechaCreacion = DateTime.Now;
                this.FechaModificacion = DateTime.Now;
                this.UsuarioCreacion = "SYSTEM";
                this.UsuarioModificacion = "SYSTEM";
            }
        }
        public OportunidadBO GuardarOportunidad(string probabilidades, bool flagVentaCruzada)
        {
            return new OportunidadBO();
        }

        /// Fecha: 07/05/2021
        /// <summary>
        /// Aplica la configuracion segun la lista de inclusion y exclusion de casos
        /// </summary>
        /// <param name="inclusion">Lista de casos incluidos en el calculo de oportunidades</param>
        /// <param name="exclusion">Lista de casos excluidos en el calculo de oportunidades</param>
        /// <returns>Bool (true)</returns>
        public bool AplicarConfiguracion(List<AsignacionAutomaticaConfiguracionBO> inclusion, List<AsignacionAutomaticaConfiguracionBO> exclusion)
        {
            foreach (var config in inclusion)
            {
                if (!config.IdFaseOportunidad.Equals(0) && !config.IdFaseOportunidad.Equals(this.IdFaseOportunidad)) return false;
                if (!config.IdOrigen.Equals(0) && !config.IdOrigen.Equals(this.IdOrigen)) return false;
                if (!config.IdTipoDato.Equals(0) && !config.IdTipoDato.Equals(this.IdTipoDato)) return false;
            }
            foreach (var config in exclusion)
            {
                if (!config.IdFaseOportunidad.Equals(0) && config.IdFaseOportunidad.Equals(this.IdFaseOportunidad)) return false;
                if (!config.IdOrigen.Equals(0) && config.IdOrigen.Equals(this.IdOrigen)) return false;
                if (!config.IdTipoDato.Equals(0) && config.IdTipoDato.Equals(this.IdTipoDato)) return false;
            }
            return true;
        }


        /// Fecha: 07/05/2021
        /// <summary>
        /// Corrige el dato erroneo enviado en forma de AsignacionAutomaticaCompuestoDTO
        /// </summary>
        /// <param name="objeto">Objeto de clase AsignacionAutomaticaCompuestoDTO</param>
        public void CorregirErroneo(AsignacionAutomaticaCompuestoDTO objeto)
        {
            var ciudadTemp = _repCiudad.FirstBy(x => x.Id == this.IdCiudad, x => new { x.LongCelular, x.LongTelefono });
            objeto.LongTelefono = ciudadTemp.LongTelefono;
            objeto.LongCelular = ciudadTemp.LongCelular;
            if (!this.Celular.Substring(0, 1).Equals("0") && this.IdPais != 0 && objeto.LongCelular != 0)
            {
                //Volvemos a validar el registro
                if (this.Celular.Equals(""))
                {
                    StringBuilder builder = new StringBuilder();
                    for (var i = 0; i < objeto.LongCelular; i++)
                    {
                        builder.Append("0");
                    }
                    this.Celular = builder.ToString();
                }
                else
                { //Agregamos Codigos a Movil si existe y es internacional
                    if (this.IdPais != 51 && this.IdPais != 57 && this.IdPais != 591)
                    {
                        this.Celular = "00" + this.Celular;
                    }
                    else
                    {
                        if (this.Celular.StartsWith("51"))
                        {
                            var regex = new Regex(Regex.Escape("51"));
                            this.Celular = regex.Replace(this.Celular, "", 1);
                        }
                    }
                }
            }
            // Telefono
            if (!this.Telefono.Substring(0, 1).Equals("0") && this.IdPais != 0 && objeto.LongTelefono != 0)
            {
                if (this.Telefono.Equals("1"))
                {
                    StringBuilder builder = new StringBuilder();
                    for (var i = 0; i < objeto.LongTelefono; i++)
                    {
                        builder.Append("0");
                    }
                    this.Telefono = builder.ToString();
                }
                else
                { // Agregamos Codigos a Movil si existe y es internacional
                    if (this.IdPais != 51)
                    {
                        this.Telefono = "00" + this.Telefono;
                    }
                    else
                    {
                        this.Telefono = "0" + this.Telefono;
                    }
                }
            }
            if (this.IdOrigen.Equals("0") || this.IdOrigen == 0)
            {
                int idElSalvador = 503;
                string elSalvadorIniciales = "SAL";
                var morigen = new Dictionary<string, int>();
                var origenes = _repOrigen.GetBy(x => x.Estado == true, x => new { x.Nombre, x.Id });
                foreach (var item in origenes)
                {
                    morigen.Add(item.Nombre.Trim().ToUpper(), item.Id);
                }
                //Obtenemos los Paises
                var mpais = new Dictionary<int, string>();
                var paises = _repPais.GetBy(x => x.Estado == true, x => new { x.CodigoPais, x.NombrePais });
                foreach (var pais in paises)
                {
                    //EL SALVADOR
                    if (pais.CodigoPais == idElSalvador)
                    {
                        mpais.Add(pais.CodigoPais, elSalvadorIniciales);
                    }
                    else
                    {
                        mpais.Add(pais.CodigoPais, pais.NombrePais.Substring(0, 3).ToUpper());
                    }
                }

                var categoriadato = _repCategoriaOrigen.ObtenerCategoriaOrigenSubCategoriaDato(this.IdCategoriaDato.Value, this.IdTipoInteraccion.Value);

                StringBuilder origen = new StringBuilder();
                //origen.Append("LAN").Append(mpais[objeto.idPais]).Append(MapeadorReplace.mapOrigen(objeto.origenCampania.ToLower()).ToUpper());
                origen.Append("LAN").Append(mpais[this.IdPais.Value]).Append(categoriadato.CodigoOrigen.ToUpper());

                var origenNombre = origen.ToString().ToUpper();
                if (!morigen.ContainsKey(origenNombre))
                {
                    this.IdOrigen = 0;
                }
                else
                {
                    this.IdOrigen = morigen[origenNombre];
                }
            }
        }

        /// Fecha: 07/05/2021
        /// <summary>
        /// Valida la lista de asignacionautomatica erroneos
        /// </summary>
        /// <param name="contexto">Objeto de clase integraDBContext</param>
        /// <returns>Lista de objetos de clase AsignacionAutomaticaErrorBO</returns>
        public List<AsignacionAutomaticaErrorBO> Validar(integraDBContext contexto)
        {
            _persona = new PersonaBO(contexto);
            var ListaErrores = new List<AsignacionAutomaticaErrorBO>();
            //REGLAS DE VALIDACION PARA REGLAS ERRONEAS 
            //-> Validamos el email es correcto
            int idContacto = 0;
            if (this.IdPais == 0)
            {
                ListaErrores.Add(new AsignacionAutomaticaErrorBO(this.Id, "Ciudad", "Asigne una ciudad", _asignacionAutomaticaTipoError.TipoErrorDatoErroneo, idContacto));
                return ListaErrores;
            }
            string Campo = "email";
            try
            {
                _validor.ValidarEmail(this.Email);
            }
            catch (ValidatorException e)
            {
                ListaErrores.Add(new AsignacionAutomaticaErrorBO(this.Id, Campo, e.Message, _asignacionAutomaticaTipoError.TipoErrorDatoErroneo, idContacto));
            }
            //Validamos Longitud del Celular & Telefono
            Campo = "celular";
            try
            {
                //validad numero de celular por ciudad
                _validor.ValidarLongitudCelular(this.IdPais, this.Celular, contexto);//validar por pais
            }
            catch (ValidatorException e)
            {
                ListaErrores.Add(new AsignacionAutomaticaErrorBO(this.Id, Campo, e.Message, _asignacionAutomaticaTipoError.TipoErrorDatoErroneo, idContacto));
            }
            //Validamos Ciudad
            Campo = "ciudad";
            try
            {
                if (this.IdCentroCosto == null)
                {
                    this.IdCentroCosto = ValorEstatico.IdCentroCostoRegistro2020ILima;
                }
                else if (this.IdCentroCosto.Value == 0)
                {
                    this.IdCentroCosto = ValorEstatico.IdCentroCostoRegistro2020ILima;
                }

                if (this.IdCentroCosto == 0 || this.IdCentroCosto == null)
                {
                    Campo = "CentroCosto";
                    throw new ValidatorException("No se encontro centro de costo");
                }
            }
            catch (ValidatorException e)
            {
                ListaErrores.Add(new AsignacionAutomaticaErrorBO(this.Id, Campo, e.Message, _asignacionAutomaticaTipoError.TipoErrorDatoErroneo, idContacto));
            }
            //Validamos Si el existe el Origen
            Campo = "origen";
            try
            {
                if (this.IdOrigen.Equals(0))
                {
                    throw new ValidatorException("No se encontro Origen");
                }
            }
            catch (ValidatorException Ex)
            {
                ListaErrores.Add(new AsignacionAutomaticaErrorBO(this.Id, Campo, Ex.Message, _asignacionAutomaticaTipoError.TipoErrorDatoErroneo, idContacto));
            }
            //REGLAS DE VALIDACION PARA DATOS REPETIDOS->Solo las validamos si no hubo errores tipo erroneo
            if (ListaErrores.Count > 0)
            {
                return ListaErrores;
            }
            //Actualizar IdContacto
            AlumnoRepositorio _repAlumno = new AlumnoRepositorio(contexto);
            var alumnoValidaEmail = _repAlumno.ObtenerPormail(this.Email.ToUpper(), null) ?? _repAlumno.ObtenerPormail(null, this.Email.ToUpper());

            //Alumno = Alumno == null ? _repAlumno.ObtenerPor

            if (alumnoValidaEmail != null)
            {
                this.IdAlumno = alumnoValidaEmail.Id;
                this.Nombre1 = string.IsNullOrEmpty(this.Nombre1) ? alumnoValidaEmail.Nombre1 : this.Nombre1;
                this.Nombre2 = string.IsNullOrEmpty(this.Nombre2) ? alumnoValidaEmail.Nombre2 : this.Nombre2;
                this.ApellidoPaterno = string.IsNullOrEmpty(this.ApellidoPaterno) ? alumnoValidaEmail.ApellidoPaterno : this.ApellidoPaterno;
                this.ApellidoMaterno = string.IsNullOrEmpty(this.ApellidoMaterno) ? alumnoValidaEmail.ApellidoMaterno : this.ApellidoMaterno;
                this.Telefono = string.IsNullOrEmpty(this.Telefono) ? alumnoValidaEmail.Telefono : this.Telefono;
                this.Celular = string.IsNullOrEmpty(this.Celular) ? alumnoValidaEmail.Celular : this.Celular;
                this.Email = string.IsNullOrEmpty(this.Email) ? alumnoValidaEmail.Email1 : this.Email;
                this.IdPais = this.IdPais == 0 ? alumnoValidaEmail.IdCodigoPais ?? 0 : this.IdPais;
                this.IdCiudad = this.IdCiudad == 0 ? alumnoValidaEmail.IdCodigoRegionCiudad ?? 0 : this.IdCiudad;
                this.IdAreaFormacion = this.IdAreaFormacion.Equals(0) || this.IdAreaFormacion == null ? alumnoValidaEmail.IdAformacion : this.IdAreaFormacion;
                this.IdAreaTrabajo = this.IdAreaTrabajo.Equals(0) || this.IdAreaTrabajo == null ? alumnoValidaEmail.IdAtrabajo : this.IdAreaTrabajo;
                this.IdIndustria = this.IdIndustria.Equals(0) || this.IdIndustria == null ? alumnoValidaEmail.IdIndustria : this.IdIndustria;
                this.IdCargo = this.IdCargo.Equals(0) || this.IdCargo == null ? alumnoValidaEmail.IdCargo : this.IdCargo;

                //Valido la Funcion Calculo Individual
                int? idCreacionCorrecta = _persona.InsertarPersona(alumnoValidaEmail.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Alumno, "PortalWeb");
                if (idCreacionCorrecta == null)
                {
                    throw new Exception("No se creo el persona clasificacion");
                }
                this.idClasificacionPersona = idCreacionCorrecta;
            }
            else//CREO EL ALUMNO PARA QUE A CREAR LA OPORTUNIDAD YA TENGA ID
            {
                AlumnoBO alumnoNuevo = new AlumnoBO();
                alumnoNuevo.Nombre1 = this.Nombre1;
                alumnoNuevo.Nombre2 = this.Nombre2;
                alumnoNuevo.ApellidoPaterno = this.ApellidoPaterno;
                alumnoNuevo.ApellidoMaterno = this.ApellidoMaterno;
                alumnoNuevo.Telefono = this.Telefono;
                alumnoNuevo.Celular = this.Celular;
                alumnoNuevo.Email1 = this.Email;
                alumnoNuevo.IdCodigoPais = this.IdPais;
                alumnoNuevo.IdCodigoRegionCiudad = this.IdCiudad;
                alumnoNuevo.IdCiudad = this.IdCiudad;
                alumnoNuevo.IdAformacion = this.IdAreaFormacion;
                alumnoNuevo.IdAtrabajo = this.IdAreaTrabajo;
                alumnoNuevo.IdIndustria = this.IdIndustria;
                alumnoNuevo.IdCargo = this.IdCargo;
                alumnoNuevo.IdEmpresa = null;
                alumnoNuevo.Estado = true;
                alumnoNuevo.UsuarioCreacion = "SYSTEM";
                alumnoNuevo.UsuarioModificacion = "SYSTEM";
                alumnoNuevo.FechaModificacion = DateTime.Now;
                alumnoNuevo.FechaCreacion = DateTime.Now;
                alumnoNuevo.ValidarEstadoContactoWhatsAppTemporal(contexto);
                _repAlumno.Insert(alumnoNuevo);

                //Valido la Funcion Calculo Individual
                int? idCreacionCorrecta = _persona.InsertarPersona(alumnoNuevo.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Alumno, "PortalWeb");
                //Si boto error en al funcion 
                if (idCreacionCorrecta == null)
                {
                    var nombreTablaV3 = "talumnos";
                    var nombreTablaV4 = "mkt.T_Alumno";
                    var resultado = _repAlumno.EliminarFisicaAlumno(nombreTablaV4, nombreTablaV3, alumnoNuevo.Id, null, 0);
                    if (resultado == true)
                    {
                        throw new Exception("Se elimino el alumno");
                    }
                    else
                    {
                        throw new Exception("No se elimino alumno");
                    }
                    //throw new Exception("ocurrio un error NO se pudo Insertar el docente");
                }
                this.IdAlumno = alumnoNuevo.Id;
                this.idClasificacionPersona = idCreacionCorrecta;
            }
            return ListaErrores;
        }

    }
}
