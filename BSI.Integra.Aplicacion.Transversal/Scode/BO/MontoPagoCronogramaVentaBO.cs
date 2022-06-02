using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    //public class MontoPagoCronogramaVentaBO : BaseBO
    //{
    //    private int IdOportunidad;
    //    private string TipoPersonal;

    //    public MontoPagoCronogramaBO MontoPagoCronograma;
    //    public List<TipoDescuentoBO> ListaTipoDescuento;
    //    public List<MontosPagosVentasDTO> ListaMontosPagosVentas;
    //    public List<MontoPagoCronogramasHistorialDTO> ListaMontoPagoCronogramaHistorial;

    //    private integraDBContext _contexto = new integraDBContext();
    //    private DapperRepository _dapperRepository;

    //    //repositorios
    //    private MontoPagoCronogramaRepositorio _repo;
    //    private MontoPagoCronogramaDetalleRepositorio _repoDetalle;
    //    private AlumnoRepositorio _repoAlumno;
    //    private MatriculaCabeceraRepositorio _repoMatricula;
    //    private CursoPespecificoRepositorio _repoCursoPespecifico;
    //    private TipoCambioColRepositorio _repoTipoCambioCol;
    //    private PespecificoRepositorio _repoPespecifico;
    //    private CriterioDocRepositorio _repoCriterioDoc;
    //    private CentroCostoRepositorio _repoCentoCosto;

    //    public MontoPagoCronogramaVentaBO()
    //    {
    //        Inicializar();
    //    }

    //    public MontoPagoCronogramaVentaBO(int IdOportunidad, string TipoPersonal)
    //    {
    //        this.IdOportunidad = IdOportunidad;
    //        this.TipoPersonal = TipoPersonal;
    //        Inicializar();
    //        CargarTipoDescuento();
    //        CargarMontosPagos();
    //        CargarCronograma();
    //    }

    //    private void Inicializar()
    //    {
    //        ListaMontoPagoCronogramaHistorial = new List<MontoPagoCronogramasHistorialDTO>();

    //        _dapperRepository = new DapperRepository();

    //        ListaTipoDescuento = new List<TipoDescuentoBO>();
    //        ListaMontosPagosVentas = new List<MontosPagosVentasDTO>();
    //        MontoPagoCronograma = new MontoPagoCronogramaBO();

    //        _repo = new MontoPagoCronogramaRepositorio();
    //        _repoDetalle = new MontoPagoCronogramaDetalleRepositorio();
    //        _repoAlumno = new AlumnoRepositorio();
    //        _repoMatricula = new MatriculaCabeceraRepositorio();
    //        _repoCursoPespecifico = new CursoPespecificoRepositorio();
    //        _repoTipoCambioCol = new TipoCambioColRepositorio();
    //        _repoPespecifico = new PespecificoRepositorio();
    //        _repoCriterioDoc = new CriterioDocRepositorio();
    //        _repoCentoCosto = new CentroCostoRepositorio();
    //    }

    //    public bool GuardarCronogramaVentas(int IdOportunidad, int IdAlumno, bool Eliminar, MontoPagoCronogramaBO Cronograma)
    //    {
    //        AlumnoInformacionReducidaDTO _Alumno = new AlumnoInformacionReducidaDTO();
    //        PEspecificoInformacionDTO _PEspecifico = new PEspecificoInformacionDTO();

    //        string _codigousuario = string.Empty, codigobanco = string.Empty;
    //        string firma = string.Empty, codigoAlumno = string.Empty, mensaje = string.Empty;
    //        int numero = 0;
    //        bool validoCronograma = false;

    //        string _query = string.Empty;

    //        //proceso para guardar
    //        MontoPagoCronogramaBO _registroCronogramaBD = _repo.FirstBy(w => w.IdOportunidad == IdOportunidad);
    //        if (Eliminar)
    //        {
    //            EliminarMontoPagoCronograma(_registroCronogramaBD);
    //        }
    //        else
    //        {
    //            //_query = "SELECT Id, Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno, Email1, Email2, IdCodigoPais FROM mkt.T_Alumno WHERE Id=" + IdAlumno.ToString();
    //            var _AlumnoBD = _repoAlumno.Obtener_InformacionReducida(IdAlumno);
    //            if (_AlumnoBD != null)
    //            {
    //                _Alumno = _AlumnoBD;
    //            }

    //            //_query = "SELECT PE.Id, PE.Nombre, PE.Codigo, PE.Tipo, PE.Categoria, PE.CodigoBanco, PE.Ciudad from com.T_Oportunidad AS OP inner join pla.T_PEspecifico AS PE ON OP.IdCentroCosto=PE.IdCentroCosto where OP.Id = " + IdOportunidad.ToString();
    //            PEspecificoInformacionDTO _PEspecificoBD = _repoPespecifico.ObtenerPespecificoPorOportunidad(IdOportunidad);

    //            if (_PEspecificoBD != null)
    //            {
    //                _PEspecifico = _PEspecificoBD;
    //            }

    //            if (Int32.TryParse(_PEspecifico.CodigoBanco, out numero))
    //            {
    //                numero = (numero / 100);
    //                codigobanco = numero.ToString();
    //                _codigousuario = _Alumno.Id.ToString() + codigobanco;
    //                _codigousuario = _codigousuario.PadRight(14, '0');
    //            }
    //            else
    //            {
    //                codigobanco = _PEspecifico.CodigoBanco.ToUpper();
    //                _codigousuario = _Alumno.Id.ToString() + codigobanco;
    //            }

    //            Cronograma.CodigoMatricula = _codigousuario;

    //            if (Cronograma.EsAprobado)
    //            {
    //                Cronograma.MatriculaEnProceso = 1;
    //            }
    //            else
    //            {
    //                Cronograma.MatriculaEnProceso = 0;
    //            }

    //            if (_registroCronogramaBD == null) // Insertamos
    //            {
    //                InsertarMontoPagoCronograma(IdOportunidad, Cronograma);

    //                if (Cronograma.EsAprobado)
    //                {
    //                    //inicio de correo
    //                    HelperCorreo helpCorreo = new HelperCorreo();
    //                    HelperMensajes helpMensaje = new HelperMensajes();

    //                    string password = string.Empty;
    //                    string _passEncrip = string.Empty;
    //                    string simboloMoneda = string.Empty; ;

    //                    //DatosUsuarioPortalDTO _AspUsers = new DatosUsuarioPortalDTO();
    //                    //var _UsuarioPortal = _dapperRepository.QuerySPFirstOrDefault("conf.SP_GetUsuarioClavePortalWeb", new { idAlumno = _Alumno.Id, email = _Alumno.Email1 });
    //                    //_AspUsers = JsonConvert.DeserializeObject<DatosUsuarioPortalDTO>(_UsuarioPortal);

    //                    DatosUsuarioPortalDTO _AspUsers = _repo.Obtener_UsuarioClavePortalWeb(_Alumno.Id, _Alumno.Email1);

    //                    if (_AspUsers.UserName != null)
    //                    {
    //                        password = _AspUsers.Password;
    //                    }
    //                    else
    //                    {
    //                        password = CrearClave(6);
    //                        _passEncrip = HashPassword(password);

    //                        //var _UsuarioCreatePortal = _dapperRepository.QuerySPFirstOrDefault("conf.SP_CreateUsuarioClavePortalWeb", new { IdAlumno = _Alumno.Id, Email = _Alumno.Email1, Clave = password, ClaveEncriptada = _passEncrip, Nombres = (_Alumno.Nombre1 + " " + _Alumno.Nombre2), Apellidos = (_Alumno.ApellidoPaterno + " " + _Alumno.ApellidoMaterno), Celular = _Alumno.Celular, CodigoPais = _Alumno.IdCodigoPais.Value, Fecha = DateTime.Now });
    //                        _AspUsers = _repo.Crear_UsuarioClavePortalWeb(_Alumno.Id, _Alumno.Email1, password,
    //                            _passEncrip, (_Alumno.Nombre1 + " " + _Alumno.Nombre2),
    //                            (_Alumno.ApellidoPaterno + " " + _Alumno.ApellidoMaterno), _Alumno.Celular,
    //                            _Alumno.IdCodigoPais.Value, DateTime.Now);
    //                    }

    //                    if ((_PEspecifico.Ciudad == "AREQUIPA" || _PEspecifico.Ciudad == "LIMA" || _PEspecifico.Ciudad.Contains("ONLINE")) && _Alumno.IdCodigoPais.Value != 57)
    //                    {
    //                        codigoAlumno = _codigousuario;
    //                        if (_Alumno.IdCodigoPais.Value == 51)
    //                        {
    //                            mensaje = helpMensaje.mensaje_email_proceso_pago_peru(_Alumno.Email1, password, "https://bsginstitute.com/Cuenta", _Alumno, Cronograma._ListaCronogramaDetalle.ToList(), "", codigoAlumno, "PE", "S/.");
    //                            simboloMoneda = "S/.";
    //                        }
    //                        else
    //                        {
    //                            mensaje = helpMensaje.mensaje_email_proceso_pago_peru(_Alumno.Email1, password, "https://bsginstitute.com/Cuenta", _Alumno, Cronograma._ListaCronogramaDetalle.ToList(), "", codigoAlumno, "INT", "US$");
    //                            simboloMoneda = "US$";
    //                        }
    //                    }
    //                    else
    //                    {
    //                        codigoAlumno = _codigousuario;
    //                        codigoAlumno = codigoAlumno.Replace("A", "");
    //                        mensaje = helpMensaje.mensaje_email_proceso_pago_peru(_Alumno.Email1, password, "https://bsginstitute.com/Cuenta", _Alumno, Cronograma._ListaCronogramaDetalle.ToList(), "", codigoAlumno, "CO", "COL");
    //                        simboloMoneda = "COL";
    //                    }

    //                    firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + Cronograma.UsuarioCreacion + ".png' />";
    //                    mensaje += "<br><br>" + firma;

    //                    List<string> listaCorreos = new List<string>();
    //                    listaCorreos.Add("pbeltran@bsginstitute.com");

    //                    helpCorreo.envio_emailSinCopia(_Alumno.Email1, _Alumno.Nombre1 + " " + _Alumno.Nombre2 + " " + _Alumno.ApellidoPaterno + " " + _Alumno.ApellidoMaterno, "BSG INSTITUTE - Código de Pago " + "", mensaje);
    //                    string mensajeArea = helpMensaje.mensaje_email_finanzas(_Alumno, Cronograma._ListaCronogramaDetalle.ToList(), _codigousuario.ToUpper(), simboloMoneda);
    //                    helpCorreo.envio_email("matriculas@bsginstitute.com", _Alumno.Nombre1 + " " + _Alumno.Nombre2 + " " + _Alumno.ApellidoPaterno + " " + _Alumno.ApellidoMaterno, "BSG INSTITUTE - Código de Pago " + "", mensajeArea, listaCorreos);
    //                    //fin nuevo

    //                    try
    //                    {
    //                        GenerarArchivo_CrepCronograma(_Alumno, _PEspecifico, Cronograma, "Nuevo", firma);
    //                    }
    //                    catch (Exception ex)
    //                    {

    //                    }
    //                }

    //            }
    //            else // Actualizamos
    //            {
    //                EliminarMontoPagoCronograma(_registroCronogramaBD);
    //                InsertarMontoPagoCronograma(IdOportunidad, Cronograma);

    //                if (Cronograma.EsAprobado)
    //                {
    //                    //inicio de correo
    //                    HelperCorreo helpCorreo = new HelperCorreo();
    //                    HelperMensajes helpMensaje = new HelperMensajes();

    //                    string password = string.Empty;
    //                    string _passEncrip = string.Empty;
    //                    string simboloMoneda = string.Empty; ;

    //                    DatosUsuarioPortalDTO _AspUsers = _repo.Obtener_UsuarioClavePortalWeb(_Alumno.Id, _Alumno.Email1);

    //                    if (_AspUsers.UserName != null)
    //                    {
    //                        password = _AspUsers.Password;
    //                    }
    //                    else
    //                    {
    //                        password = CrearClave(6);
    //                        _passEncrip = HashPassword(password);

    //                        _AspUsers = _repo.Crear_UsuarioClavePortalWeb(_Alumno.Id, _Alumno.Email1, password,
    //                            _passEncrip, (_Alumno.Nombre1 + " " + _Alumno.Nombre2),
    //                            (_Alumno.ApellidoPaterno + " " + _Alumno.ApellidoMaterno), _Alumno.Celular,
    //                            _Alumno.IdCodigoPais.Value, DateTime.Now);
    //                    }

    //                    if ((_PEspecifico.Ciudad == "AREQUIPA" || _PEspecifico.Ciudad == "LIMA" || _PEspecifico.Ciudad.Contains("ONLINE")) && _Alumno.IdCodigoPais.Value != 57)
    //                    {
    //                        codigoAlumno = _codigousuario;
    //                        if (_Alumno.IdCodigoPais.Value == 51)
    //                        {
    //                            mensaje = helpMensaje.mensaje_email_proceso_pago_peru(_Alumno.Email1, password, "https://bsginstitute.com/Cuenta", _Alumno, Cronograma._ListaCronogramaDetalle.ToList(), "", codigoAlumno, "PE", "S/.");
    //                            simboloMoneda = "S/.";
    //                        }
    //                        else
    //                        {
    //                            mensaje = helpMensaje.mensaje_email_proceso_pago_peru(_Alumno.Email1, password, "https://bsginstitute.com/Cuenta", _Alumno, Cronograma._ListaCronogramaDetalle.ToList(), "", codigoAlumno, "INT", "US$");
    //                            simboloMoneda = "US$";
    //                        }
    //                    }
    //                    else
    //                    {
    //                        codigoAlumno = _codigousuario;
    //                        codigoAlumno = codigoAlumno.Replace("A", "");
    //                        mensaje = helpMensaje.mensaje_email_proceso_pago_peru(_Alumno.Email1, password, "https://bsginstitute.com/Cuenta", _Alumno, Cronograma._ListaCronogramaDetalle.ToList(), "", codigoAlumno, "CO", "COL");
    //                        simboloMoneda = "COL";
    //                    }

    //                    firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + Cronograma.UsuarioCreacion + ".png' />";
    //                    mensaje += "<br><br>" + firma;

    //                    List<string> listaCorreos = new List<string>();
    //                    listaCorreos.Add("pbeltran@bsginstitute.com");

    //                    helpCorreo.envio_emailSinCopia(_Alumno.Email1, _Alumno.Nombre1 + " " + _Alumno.Nombre2 + " " + _Alumno.ApellidoPaterno + " " + _Alumno.ApellidoMaterno, "BSG INSTITUTE - Código de Pago " + "", mensaje);
    //                    string mensajeArea = helpMensaje.mensaje_email_finanzas(_Alumno, Cronograma._ListaCronogramaDetalle.ToList(), _codigousuario.ToUpper(), simboloMoneda);
    //                    helpCorreo.envio_email("matriculas@bsginstitute.com", _Alumno.Nombre1 + " " + _Alumno.Nombre2 + " " + _Alumno.ApellidoPaterno + " " + _Alumno.ApellidoMaterno, "BSG INSTITUTE - Código de Pago " + "", mensajeArea, listaCorreos);
    //                    //fin nuevo

    //                    try
    //                    {
    //                        GenerarArchivo_CrepCronograma(_Alumno, _PEspecifico, Cronograma, "Nuevo", firma);
    //                    }
    //                    catch (Exception ex)
    //                    {

    //                    }
    //                }

    //            }
    //        }


    //        //proceso para guardar cronograma en finanzas
    //        try
    //        {
    //            if (Cronograma.Id != 0)
    //            {
    //                if (Cronograma.EsAprobado)
    //                {
    //                    GenerarCronogramaVentasPorCordinador(IdOportunidad, Cronograma);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //        //fin de proceso

    //        return true;
    //    }

    //    public void PruebaCrearUsuario()
    //    {
    //        string _query = string.Empty;

    //        string password = CrearClave(6);
    //        string _passEncrip = HashPassword(password);

    //        DatosUsuarioPortalDTO _AspUsers = _repo.Obtener_UsuarioClavePortalWeb(990991, "prueba_jr2@bs.net");
    //        var _RespuestaAspUsers = _repo.Crear_UsuarioClavePortalWeb(990991, "prueba_jr2@bs.net", password, _passEncrip,
    //            ("prueba sistema"), ("integra"), "963852741", 51, DateTime.Now);
    //    }

    //    public void InsertarMontoPagoCronograma(int IdOportunidad, MontoPagoCronogramaBO Cronograma)
    //    {
    //        MontoPagoCronogramaBO _mpc = new MontoPagoCronogramaBO();

    //        _mpc.CodigoMatricula = Cronograma.CodigoMatricula;
    //        _mpc.EsAprobado = Cronograma.EsAprobado;
    //        _mpc.Formula = Cronograma.Formula;
    //        _mpc.IdMoneda = Cronograma.IdMoneda;
    //        _mpc.IdMontoPago = Cronograma.IdMontoPago;
    //        _mpc.IdOportunidad = Cronograma.IdOportunidad;
    //        _mpc.IdPersonal = Cronograma.IdPersonal;
    //        _mpc.IdTipoDescuento = Cronograma.IdTipoDescuento;
    //        _mpc.MatriculaEnProceso = Cronograma.MatriculaEnProceso;
    //        _mpc.NombrePlural = Cronograma.NombrePlural;
    //        _mpc.Precio = Cronograma.Precio;
    //        _mpc.PrecioDescuento = Cronograma.PrecioDescuento;
    //        _mpc.UsuarioCreacion = "SYSTEM";
    //        _mpc.UsuarioModificacion = "SYSTEM";
    //        _mpc.Estado = true;
    //        _mpc.FechaCreacion = DateTime.Now;
    //        _mpc.FechaModificacion = DateTime.Now;

    //        _mpc._ListaCronogramaDetalle = new List<MontoPagoCronogramaDetalleBO>();

    //        foreach (var item in Cronograma._ListaCronogramaDetalle)
    //        {
    //            MontoPagoCronogramaDetalleBO _mpcd = new MontoPagoCronogramaDetalleBO();
    //            _mpcd.CuotaDescripcion = item.CuotaDescripcion;
    //            _mpcd.Estado = true;
    //            _mpcd.FechaCreacion = DateTime.Now;
    //            _mpcd.FechaModificacion = DateTime.Now;
    //            _mpcd.FechaPago = item.FechaPago;
    //            _mpcd.Matricula = item.Matricula;
    //            _mpcd.MontoCuota = item.MontoCuota;
    //            _mpcd.MontoCuotaDescuento = item.MontoCuotaDescuento;
    //            _mpcd.NumeroCuota = item.NumeroCuota;
    //            _mpcd.Pagado = false;
    //            _mpcd.UsuarioCreacion = "SYSTEM";
    //            _mpcd.UsuarioModificacion = "SYSTEM";

    //            _mpc._ListaCronogramaDetalle.Add(_mpcd);
    //        }

    //        _repo.Insert(_mpc);

    //        Cronograma.Id = _mpc.Id;
    //    }

    //    public void EliminarMontoPagoCronograma(MontoPagoCronogramaBO registroCronogramaBD)
    //    {
    //        var regMPC = _repo.FirstById(registroCronogramaBD.Id);
    //        regMPC.EsAprobado = false;
    //        regMPC.MatriculaEnProceso = 0;
    //        regMPC.Estado = false;
    //        regMPC.FechaModificacion = DateTime.Now;

    //        _repo.Update(regMPC);

    //        if (regMPC.EsAprobado)
    //        {
    //            EliminarCronogramaVentasPorCordinador(regMPC.Id);
    //        }
    //    }

    //    public void GenerarCronogramaVentasPorCordinador(int IdCronograma, MontoPagoCronogramaBO Cronograma)
    //    {
    //        bool Buscar = false;
    //        MatriculaCronogramaOportunidadDTO _PrePrograma = _repo.Obtener_MatriculaCronogramaOportunidad(IdCronograma);

    //        if (_PrePrograma != null)
    //            Buscar = true;

    //        if (Buscar)
    //        {
    //            //MatriculaCabeceraBO matriculaExistente = _repoMatricula.FirstBy(w => w.IdAlumno == _PrePrograma.IdAlumno && w.IdPespecifico == _PrePrograma.IdPEspecifico);
    //            if (!_repoMatricula.Exist(w => w.IdAlumno == _PrePrograma.IdAlumno && w.IdPespecifico == _PrePrograma.IdPEspecifico))
    //            {
    //                MatriculaCabeceraBO matriculaNueva = new MatriculaCabeceraBO();
    //                matriculaNueva.CodigoMatricula = _PrePrograma.IdAlumno.ToString() + _PrePrograma.CodigoBanco;
    //                matriculaNueva.IdAlumno = _PrePrograma.IdAlumno;
    //                matriculaNueva.IdPespecifico = _PrePrograma.IdPEspecifico;
    //                matriculaNueva.EstadoMatricula = "pormatricular";
    //                matriculaNueva.FechaMatricula = DateTime.Now;
    //                matriculaNueva.IdAsesor = _PrePrograma.IdPersonal_Asignado;
    //                matriculaNueva.IdCoordinador = 0;//validar
    //                matriculaNueva.IdEstadoMatricula = 1;
    //                matriculaNueva.UsuarioCoordinadorAcademico = "";
    //                matriculaNueva.ObservacionGeneralOperaciones = "";
    //                matriculaNueva.IdCronograma = Cronograma.Id;
    //                matriculaNueva.FechaMatricula = DateTime.Now;

    //                matriculaNueva.Estado = true;
    //                matriculaNueva.UsuarioCreacion = "SYSTEMV4";
    //                matriculaNueva.UsuarioModificacion = "SYSTEMV4";
    //                matriculaNueva.FechaCreacion = DateTime.Now;
    //                matriculaNueva.FechaModificacion = DateTime.Now;

    //                //mc["UsuarioCreacion"] = "SYSTEMV4";
    //                //mc["UsuarioModificacion"] = "SYSTEMV4";
    //                //mc["FechaCreacion"] = DateTime.Now;
    //                //mc["FechaMatricula"] = DateTime.Now;
    //                //mc["Estado"] = true;

    //                //_repMC.Insertar(matriculaNueva);
    //                _repoMatricula.Insert(matriculaNueva);

    //                //Insertamos el detalle//por cada proespid
    //                //_query = "SELECT Id FROM pla.T_CursoPEspecifico where IdPEspecifico=" + _PrePrograma.IdPEspecifico;
    //                var _listaIds = _repoCursoPespecifico.GetBy(w => w.IdPespecifico == _PrePrograma.IdPEspecifico,
    //                    s => new {s.Id});

    //                if (_listaIds  != null && _listaIds.Count>0)
    //                {
    //                    foreach (var item in _listaIds)
    //                    {
    //                        TMatriculaDetalle _registroMD = new TMatriculaDetalle();
    //                        _registroMD.IdCursoPespecifico = item.Id;
    //                        _registroMD.IdMatriculaCabecera = matriculaNueva.Id;

    //                        _registroMD.Estado = true;
    //                        _registroMD.FechaCreacion = DateTime.Now;
    //                        _registroMD.FechaModificacion = DateTime.Now;
    //                        _registroMD.UsuarioCreacion = "SYSTEMV4";
    //                        _registroMD.UsuarioModificacion = "SYSTEMV4";

    //                        _contexto.TMatriculaDetalle.Add(_registroMD);

    //                    }
    //                    _contexto.SaveChanges();
    //                }

    //                //Insertar Cronograma

    //                int contadorCoutas = 1;
    //                DateTime? fechaIniPago = DateTime.Now.Date;
    //                decimal cuotaInicial = Convert.ToDecimal(Cronograma.PrecioDescuento);

    //                contadorCoutas = Cronograma._ListaCronogramaDetalle.Count;

    //                var matricula = Cronograma._ListaCronogramaDetalle.FirstOrDefault(x => x.Matricula == true && x.NumeroCuota == 1);
    //                if (matricula != null)
    //                {
    //                    fechaIniPago = matricula.FechaPago;
    //                    cuotaInicial = Convert.ToDecimal(matricula.MontoCuotaDescuento);
    //                }

    //                //analizar
    //                double tipoCambioCol = 3000;
    //                double tipoCambioBD = _repoTipoCambioCol.Obtener_PesosDolares_UltimoTipoCambioColombia();
    //                if (tipoCambioBD != null)
    //                {
    //                    tipoCambioCol = tipoCambioBD;
    //                }

    //                TCronogramaPago cp = new TCronogramaPago();
    //                cp.IdMatriculaCabecera = matriculaNueva.Id;
    //                cp.IdAlumno = matriculaNueva.IdAlumno;
    //                cp.IdPespecifico = matriculaNueva.IdPespecifico;
    //                cp.Periodo = DateTime.Now.Year.ToString();
    //                cp.Moneda = (Cronograma.IdMoneda == 19 ? "soles" : "dolares");
    //                cp.AcuerdoPago = "Crédito";
    //                cp.TipoCambio = Convert.ToDouble("0");
    //                cp.TotalPagar = Cronograma.PrecioDescuento;
    //                if (Cronograma.IdMoneda == 10)
    //                {
    //                    cp.TotalPagar = Math.Round(Cronograma.PrecioDescuento / tipoCambioCol, 2, MidpointRounding.AwayFromZero);
    //                }
    //                cp.NroCuotas = contadorCoutas;
    //                cp.FechaIniPago = fechaIniPago;
    //                cp.ConCuotaInicial = true;
    //                cp.CuotaInicial = cuotaInicial;
    //                cp.CadaNdias = false;
    //                cp.Ndias = 0;

    //                string web_moneda = string.Empty;
    //                double web_tipocambio = cp.TipoCambio.Value;
    //                double web_totalPagarConv = cp.TotalPagar.Value;

    //                if (Cronograma.IdMoneda == 19)
    //                {
    //                    web_moneda = "0";//Soles
    //                }
    //                else if (Cronograma.IdMoneda == 20)
    //                {
    //                    web_moneda = "1";//Dolares
    //                }
    //                else if (Cronograma.IdMoneda == 10)
    //                {
    //                    web_moneda = "2";//Colombianos
    //                    web_tipocambio = tipoCambioCol;
    //                    web_totalPagarConv = Cronograma.PrecioDescuento / web_tipocambio;
    //                }
    //                else
    //                {
    //                    web_moneda = Cronograma.IdMoneda.ToString();
    //                }

    //                cp.WebMoneda = web_moneda;
    //                cp.WebTipoCambio = web_tipocambio;
    //                cp.WebTotalPagar = Cronograma.PrecioDescuento;//En la moneda original
    //                cp.WebTotalPagarConv = web_totalPagarConv;//Total convertido con todos los decimales (si es colomabianos)



    //                cp.UsuarioCreacion = "SYSTEMV4";
    //                cp.UsuarioModificacion = "SYSTEMV4";
    //                cp.FechaCreacion = DateTime.Now;
    //                cp.FechaModificacion = DateTime.Now;
    //                cp.Estado = true;


    //                //registrar
    //                _contexto.TCronogramaPago.Add(cp);
    //                _contexto.SaveChanges();

    //                decimal Saldo = Convert.ToDecimal(cp.TotalPagar);

    //                foreach (var item in Cronograma._ListaCronogramaDetalle)
    //                {
    //                    TCronogramaPagoDetalleOriginal cpdf = new TCronogramaPagoDetalleOriginal();
    //                    decimal Total = Saldo;

    //                    double montoCuotaFinal = item.MontoCuotaDescuento;
    //                    Saldo = Total - Convert.ToDecimal(montoCuotaFinal);

    //                    cpdf.IdMatriculaCabecera = matriculaNueva.Id;
    //                    cpdf.NroCuota = item.NumeroCuota;
    //                    cpdf.FechaVencimiento = item.FechaPago;
    //                    cpdf.TotalPagar = Total;
    //                    cpdf.Cuota = Convert.ToDecimal(montoCuotaFinal);
    //                    cpdf.Saldo = Saldo;
    //                    cpdf.Cancelado = false;
    //                    cpdf.TipoCuota = (item.Matricula == true && item.NumeroCuota == 1 ? "MATRICULA" : "CUOTA");
    //                    cpdf.Moneda = (Cronograma.IdMoneda == 19 ? "soles" : "dolares"); //revisar
    //                    cpdf.FechaCreacion = DateTime.Now;
    //                    cpdf.FechaModificacion = DateTime.Now;
    //                    cpdf.UsuarioCreacion = "SYSTEMV4";
    //                    cpdf.UsuarioModificacion = "SYSTEMV4";
    //                    cpdf.Estado = true;


    //                    _contexto.TCronogramaPagoDetalleOriginal.Add(cpdf);
    //                }
    //                _contexto.SaveChanges();
    //                //Add Documentos

    //                TControlDocAlumno cda = new TControlDocAlumno();
    //                cda.IdMatriculaCabecera = matriculaNueva.Id; //ojo
    //                cda.IdCriterioCalificacion = 0;
    //                cda.QuienEntrego = "";
    //                cda.FechaEntregaDocumento = null;
    //                cda.Observaciones = "";
    //                cda.ComisionableEditable = "Ninguno";
    //                cda.MontoComisionable = 0;
    //                cda.ObservacionesComisionable = "";
    //                cda.PagadoComisionable = 0;
    //                cda.UsuarioCreacion = "SYSTEMV4";
    //                cda.UsuarioModificacion = "SYSTEMV4";
    //                cda.FechaCreacion = DateTime.Now;
    //                cda.FechaModificacion = DateTime.Now;
    //                cda.Estado = true;

    //                _contexto.TControlDocAlumno.Add(cda);
    //                _contexto.SaveChanges();

    //                int modalidad = 0;

    //                //_query = "SELECT TipoId FROM pla.T_PEspecifico WHERE Id=" + matriculaNueva.IdPespecifico.ToString();
    //                //var modalidadBD = _dapperRepository.FirstOrDefault(_query);
    //                var modalidadBD =
    //                    _repoPespecifico.FirstBy(w => w.Id == matriculaNueva.IdPespecifico, s => new {s.TipoId});

    //                if (modalidadBD != null && modalidadBD.TipoId != null)
    //                    modalidad = modalidadBD.TipoId.Value;

    //                if (modalidad >= 0 && modalidad <= 2)
    //                {
    //                    //_query = "SELECT Id FROM fin.T_CriterioDoc WHERE ModalidadPresencial=1";
    //                    var criterioDocsBD =
    //                        _repoCriterioDoc.GetBy(w => w.ModalidadPresencial == true, s => new {s.Id});

    //                    if (tipoCambioBD != null)
    //                    {
    //                        foreach (var item in criterioDocsBD)
    //                        {
    //                            TControlDoc docs = new TControlDoc();
    //                            docs.IdMatriculaCabecera = matriculaNueva.Id;
    //                            docs.IdCriterioDoc = item.Id;

    //                            docs.Estado = true;
    //                            docs.UsuarioCreacion = "SYSTEMV4";
    //                            docs.UsuarioModificacion = "SYSTEMV4";
    //                            docs.FechaCreacion = DateTime.Now;
    //                            docs.FechaModificacion = DateTime.Now;

    //                            _contexto.TControlDoc.Add(docs);
    //                        }
    //                        _contexto.SaveChanges();

    //                    }
    //                }

    //            }
    //        }


    //    }

    //    public void EliminarCronogramaVentasPorCordinador(int IdCronograma)
    //    {
    //        string _query = string.Empty;

    //        //_query = "select Id,IdAlumno,IdPEspecifico,EstadoMatricula,Estado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion from fin.T_MatriculaCabecera where IdCronograma=" + IdCronograma.ToString();
    //        var matricula = _repoMatricula.FirstBy(w => w.IdCronograma == IdCronograma);

    //        if (matricula != null)
    //        {
    //            matricula.Estado = false;
    //            matricula.EstadoMatricula = "eliminado";
    //            matricula.FechaModificacion = DateTime.Now;

    //            _repoMatricula.Update(matricula);
    //        }
    //    }

    //    public void GenerarArchivo_CrepCronograma(AlumnoInformacionReducidaDTO alumno, PEspecificoInformacionDTO especifico, MontoPagoCronogramaBO montoPago, string EstadoCrep, string Firma)
    //    {
    //        string _query = string.Empty;
    //        string Monto = string.Empty, _Ciudad = string.Empty, _Banco = string.Empty, CodAutoriza = string.Empty, rpta = string.Empty, CodigoBancario = string.Empty, SimboloMoneda = string.Empty, CentroCostos = string.Empty;
    //        HelperCorreo helpCorreo = new HelperCorreo();
    //        HelperMensajes helpMensaje = new HelperMensajes();

    //        var cc_codigo = _repoCentoCosto.FirstBy(w => w.Id == especifico.IdCentroCosto.Value, s => new {s.Codigo});
    //        CentroCostos = cc_codigo != null ? cc_codigo.Codigo : "";

    //        if (montoPago._ListaCronogramaDetalle != null)
    //        {
    //            _Ciudad = especifico.Ciudad.ToUpper();
    //            if ((_Ciudad == "AREQUIPA" || _Ciudad == "LIMA" || _Ciudad.Contains("ONLINE")) && alumno.IdCodigoPais.Value != 57)
    //            {
    //                _Banco = "Banco BCP - PERÚ";
    //                string _nombrearchivo = string.Empty;
    //                string _monadaNombre = string.Empty;
    //                _nombrearchivo = "CREP_X";

    //                StringBuilder linea = new StringBuilder();
    //                string _tiporegistro = "CC";
    //                string _codigosucursal = "215";

    //                string _nrocuenta = "Credipago a BSG Institute a la cuenta Lima-Soles"; // 215-1863341-0-42 - CCI: 002-215-001863341042-20 - Cuenta Corriente en Soles programas Lima, Online y Aonline
    //                string _nrocuentacrep = "1863341"; //seccion del nro de cuenta

    //                if (montoPago.NombrePlural == "Dolares")
    //                {
    //                    _monadaNombre = "DOLARES";
    //                    SimboloMoneda = "US$";
    //                    if (_Ciudad.Contains("AREQUIPA"))
    //                    {
    //                        _nrocuenta = "Credipago a BSG Institute a la cuenta Arequipa-Dolares"; //215-2307651-1-32 - CCI: 002-215-002307651132-20 - Cuenta corriente en Dólares programas Arequipa
    //                        _nrocuentacrep = "2307651";
    //                    }
    //                    else//Dolares otros
    //                    {
    //                        _nrocuenta = "Credipago a BSG Institute a la cuenta Lima-Dolares"; //215-1870934-1-48 - CCI: 002-215-001870934148-23 - Cuenta corriente en Dólares programas Lima, Online y Aonline
    //                        _nrocuentacrep = "1870934";
    //                    }
    //                }
    //                else
    //                {
    //                    _monadaNombre = "SOLES";
    //                    SimboloMoneda = "S/.";
    //                    if (_Ciudad.Contains("AREQUIPA"))
    //                    {
    //                        _nrocuenta = "Credipago a BSG Institute a la cuenta Arequipa-Soles"; //215-1863344-0-72 - CCI: 002-215-001863344072-24 - Cuenta Corriente en Soles programas Arequipa
    //                        _nrocuentacrep = "1863344";
    //                    }
    //                    else//Soles otros
    //                    {
    //                        _nrocuenta = "Credipago a BSG Institute a la cuenta Lima-Soles"; //215-1863341-0-42 - CCI: 002-215-001863341042-20 - Cuenta corriente en Dólares programas Lima, Online y Aonline
    //                        _nrocuentacrep = "1863341";
    //                    }
    //                }
    //                string _codigomoneda = (_monadaNombre == "SOLES" ? "0" : "1");

    //                string _tipovalidacion = "C";

    //                CodigoBancario = _nrocuenta;

    //                string _nombreempresa = ("BSGINSTITUTE" + _Ciudad + _monadaNombre).PadRight(40);
    //                string _fecha = String.Format("{0:yyyyMMdd}", DateTime.Now);
    //                string _temp;

    //                _temp = montoPago._ListaCronogramaDetalle.Count.ToString();

    //                string _totalregistros = _temp.ToString().PadLeft(9, '0');
    //                string _tiporegistrod = "DD";
    //                string _libred = "".PadLeft(47);

    //                string _codigousuario = string.Empty, _nombreusuario, _fechaemision, _montomora, _montominimo, _tiporegistro2, nombres_b, apellidos_b;
    //                string _codigoespecial, _montocuota, _fechavencimiento, _montototal = string.Empty;
    //                string _tipoarchivo = string.Empty;

    //                if (EstadoCrep == "Nuevo")
    //                {
    //                    _tipoarchivo = "A";
    //                }
    //                else
    //                {
    //                    _tipoarchivo = "E";
    //                }

    //                string _libre = "000".PadLeft(113);
    //                DateTime fechaEmision = new DateTime();

    //                string codigobanco = string.Empty;
    //                int numero = 0;
    //                if (Int32.TryParse(especifico.CodigoBanco, out numero))
    //                {
    //                    numero = (numero / 100);
    //                    codigobanco = numero.ToString();
    //                    _codigousuario = alumno.Id.ToString() + codigobanco;
    //                    _codigousuario = _codigousuario.PadRight(14, '0');
    //                }
    //                else
    //                {
    //                    codigobanco = especifico.CodigoBanco.ToUpper();
    //                    _codigousuario = alumno.Id.ToString() + codigobanco;
    //                }
    //                CodAutoriza = _codigousuario;

    //                _codigousuario = _codigousuario.ToString().PadLeft(14);
    //                montoPago.CodigoMatricula = _codigousuario;


    //                string _auxNombres = string.Empty, _auxApellidos = string.Empty;
    //                _auxNombres = alumno.Nombre1.ToLower() + " " + alumno.Nombre2.ToLower();
    //                _auxApellidos = alumno.ApellidoPaterno.ToLower() + " " + alumno.ApellidoMaterno.ToLower();

    //                nombres_b = NormalizarCadena(_auxNombres.ToUpper());
    //                apellidos_b = NormalizarCadena(_auxApellidos.ToUpper());

    //                _nombreusuario = apellidos_b + " " + nombres_b;
    //                _nombreusuario = _nombreusuario.ToUpper();
    //                _nombreusuario = _nombreusuario.PadRight(40);

    //                fechaEmision = DateTime.Now.AddDays(1);
    //                _fechaemision = String.Format("{0:yyyyMMdd}", fechaEmision);

    //                _montomora = "0".PadLeft(15, '0');
    //                _montominimo = "0".PadLeft(9, '0');

    //                _montototal = montoPago.PrecioDescuento.ToString().PadLeft(15, '0');

    //                string nombreArchivoCrep = string.Empty;
    //                if (string.IsNullOrEmpty(alumno.Dni))
    //                {
    //                    nombreArchivoCrep = _nombrearchivo + "00000000";
    //                }
    //                else
    //                {
    //                    nombreArchivoCrep = _nombrearchivo + alumno.Dni;
    //                }

    //                int contadorpagos = 0;
    //                _montototal = montoPago.PrecioDescuento.ToString("0.00");
    //                _montototal = _montototal.Replace(".", "").Replace(",", "");
    //                byte[] registroCrepByte;

    //                using (MemoryStream ms = new MemoryStream())
    //                {
    //                    using (StreamWriter myStreamWriter = new StreamWriter(ms))
    //                    {
    //                        linea.Append(_tiporegistro + _codigosucursal + _codigomoneda + _nrocuentacrep + _tipovalidacion + _nombreempresa + _fecha + _totalregistros + _montototal + _tipoarchivo + _libre);
    //                        rpta = linea.ToString();
    //                        myStreamWriter.WriteLine(linea.ToString());
    //                        linea.Remove(0, linea.Length);

    //                        foreach (var pagos in montoPago._ListaCronogramaDetalle)
    //                        {
    //                            contadorpagos++;
    //                            if (contadorpagos < 10)
    //                                _codigoespecial = "10" + contadorpagos.ToString() + "01XXXXXX";
    //                            else
    //                                _codigoespecial = "1" + contadorpagos.ToString() + "01XXXXXX";

    //                            _codigoespecial = _codigoespecial.PadRight(30);
    //                            _montocuota = String.Format("{0:0.00}", (Convert.ToDouble(pagos.MontoCuotaDescuento.ToString()) + Convert.ToDouble("0"))).Replace(".", "").Replace(",", "").PadLeft(15, '0');
    //                            _fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(pagos.FechaPago.ToString()));
    //                            if (EstadoCrep == "Nuevo")
    //                            {
    //                                _tiporegistro2 = "A";
    //                            }
    //                            else
    //                            {
    //                                _tiporegistro2 = "E";
    //                            }

    //                            linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuentacrep + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
    //                            myStreamWriter.WriteLine(linea.ToString());
    //                            linea.Remove(0, linea.Length);
    //                        }
    //                        myStreamWriter.Close();//cerramos al terminar la escritura del archivo crep
    //                    }
    //                    registroCrepByte = ms.ToArray();
    //                }

    //                string mensaje = helpMensaje.mensaje_email_finanzas(alumno, montoPago._ListaCronogramaDetalle.ToList(), _codigousuario + " - " + _nrocuenta, SimboloMoneda);
    //                mensaje += "<br><br>" + Firma;

    //                List<string> listaCorreos = new List<string>();
    //                listaCorreos.Add("pbeltran@bsginstitute.com");

    //                helpCorreo.envioEmailAdjuntoBlob("pbeltran@bsginstitute.com", "Archivo CREP " + EstadoCrep + " - Ventas", EstadoCrep + " Archivo CREP " + especifico.Nombre.ToString(), mensaje, nombreArchivoCrep + "-" + EstadoCrep + ".txt", registroCrepByte, listaCorreos);
    //                //HelperCorreo.envioEmailAdjuntoBlob("jrivera@bsgrupo.com", "Archivo CREP " + EstadoCrep + " - Ventas", EstadoCrep + " Archivo CREP " + especifico.nombre.ToString(), mensaje, nombreArchivoCrep + "-" + EstadoCrep + ".txt", registroCrepByte, listaCorreos);
    //            }
    //            else
    //            {
    //                string _codigosistema = string.Empty, _codigousuario = string.Empty;
    //                _Banco = "Bancolombia - Colombia";
    //                SimboloMoneda = "COL $";
    //                _codigosistema = alumno.Id.ToString() + especifico.CodigoBanco.ToUpper();
    //                _codigousuario = _codigosistema;

    //                montoPago.CodigoMatricula = _codigousuario;

    //                CodigoBancario = "56470";//Nro Conv. Bancolombia: 56470 (lo que identifica nuestra cuenta)

    //                List<string> listaCorreos = new List<string>();
    //                listaCorreos.Add("pbeltran@bsginstitute.com");

    //                string mensaje = helpMensaje.mensaje_email_finanzas(alumno, montoPago._ListaCronogramaDetalle.ToList(), _codigousuario.ToUpper() + " " + _Banco, SimboloMoneda);
    //                mensaje += "<br><br>" + Firma;
    //                helpCorreo.envio_email("pbeltran@bsginstitute.com", "Peticion Bancolombia", "Código de Pago " + especifico.Nombre.ToString(), mensaje, listaCorreos);
    //                //HelperCorreo.envio_email("jrivera@bsgrupo.com", "Peticion Bancolombia", "Código de Pago " + especifico.nombre.ToString(), mensaje, listaCorreos);
    //            }
    //        }
    //    }



    //    #region Revisado
    //    public List<DetalleMontoPagoDTO> Obtener_DetalleMontoPago(int idMontoPago)
    //    {
    //        try
    //        {
    //            List<DetalleMontoPagoDTO> detalleMontoPago = _repo.Obtener_DetalleMontoPago(idMontoPago);
    //            return detalleMontoPago;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //    }

    //    public void Generar_HistorialCronograma(int idAsesor)
    //    {
    //        try
    //        {
    //            if (idAsesor > 0)
    //            {
    //                List<MontoPagoCronogramasHistorialDTO> historial = _repo.Obtener_HistorialCronograma(idAsesor);
    //                ListaMontoPagoCronogramaHistorial = historial;
    //            }
    //            else
    //            {
    //                ListaMontoPagoCronogramaHistorial = null;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ListaMontoPagoCronogramaHistorial = null;
    //            throw new Exception(ex.Message);
    //        }
    //    }

    //    private void CargarTipoDescuento()
    //    {
    //        try
    //        {
    //            List<TipoDescuentoBO> descuentos = _repo.Obtener_TipoDescuento(this.IdOportunidad, this.TipoPersonal);
    //            ListaTipoDescuento = descuentos;
    //        }
    //        catch (Exception ex)
    //        {
    //            ListaMontoPagoCronogramaHistorial = null;
    //            throw new Exception(ex.Message);
    //        }
    //    }

    //    private void CargarMontosPagos()
    //    {
    //        try
    //        {
    //            List<MontosPagosVentasDTO> montos = _repo.Obtener_MontosPagos(this.IdOportunidad);
    //            ListaMontosPagosVentas = montos;
    //        }
    //        catch (Exception ex)
    //        {
    //            ListaMontosPagosVentas = null;
    //            throw new Exception(ex.Message);
    //        }
    //    }

    //    public void CargarCronograma()
    //    {
    //        try
    //        {
    //            MontoPagoCronogramaBO montoPago = _repo.FirstBy(w => w.IdOportunidad == this.IdOportunidad);

    //            if (montoPago != null)
    //            {
    //                MontoPagoCronograma = montoPago;

    //                List<MontoPagoCronogramaDetalleBO> listadoDetalleMontoPago =
    //                    _repoDetalle.GetBy(w => w.IdMontoPagoCronograma == MontoPagoCronograma.Id).ToList();

    //                if (listadoDetalleMontoPago != null && listadoDetalleMontoPago.Count() > 0)
    //                    MontoPagoCronograma._ListaCronogramaDetalle = listadoDetalleMontoPago;
    //            }
    //            else
    //                MontoPagoCronograma = null;
    //        }
    //        catch (Exception ex)
    //        {
    //            MontoPagoCronograma = null;
    //            throw new Exception(ex.Message);
    //        }
    //    }

    //    public string CrearClave(int longitud)
    //    {
    //        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    //        StringBuilder res = new StringBuilder();
    //        Random rnd = new Random();
    //        while (0 < longitud--)
    //        {
    //            res.Append(valid[rnd.Next(valid.Length)]);
    //        }

    //        return res.ToString();
    //    }

    //    public string Encriptar(string textoQueEncriptaremos, string passBase, string saltValue, string hashAlgorithm,
    //        int passwordIterations, string initVector, int keySize)
    //    {
    //        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
    //        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
    //        byte[] plainTextBytes = Encoding.UTF8.GetBytes(textoQueEncriptaremos);
    //        PasswordDeriveBytes password =
    //            new PasswordDeriveBytes(passBase, saltValueBytes, hashAlgorithm, passwordIterations);
    //        byte[] keyBytes = password.GetBytes(keySize / 8);
    //        RijndaelManaged symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC };
    //        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
    //        MemoryStream memoryStream = new MemoryStream();
    //        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
    //        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
    //        cryptoStream.FlushFinalBlock();
    //        byte[] cipherTextBytes = memoryStream.ToArray();
    //        memoryStream.Close();
    //        cryptoStream.Close();
    //        string cipherText = Convert.ToBase64String(cipherTextBytes);
    //        return cipherText;
    //    }

    //    public string Encriptar(string us_clave)
    //    {
    //        return Encriptar(us_clave, "pass75dc@avz10", "s@lAvz", "MD5", 1, "@1B2c3D4e5F6g7H8", 128);
    //    }

    //    public string Hash(string password)
    //    {
    //        var bytes = new UTF8Encoding().GetBytes(password);
    //        var hashBytes = System.Security.Cryptography.HashAlgorithm.Create().ComputeHash(bytes);
    //        return Convert.ToBase64String(hashBytes);
    //    }

    //    public static string HashPassword(string password)
    //    {
    //        byte[] salt;
    //        byte[] buffer2;
    //        if (password == null)
    //        {
    //            throw new ArgumentNullException("password");
    //        }

    //        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
    //        {
    //            salt = bytes.Salt;
    //            buffer2 = bytes.GetBytes(0x20);
    //        }

    //        byte[] dst = new byte[0x31];
    //        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
    //        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
    //        return Convert.ToBase64String(dst);
    //    }

    //    public string NormalizarCadena(string input)
    //    {
    //        Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
    //        Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
    //        Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
    //        Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
    //        Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
    //        Regex replace_n_Accents = new Regex("[ñ]", RegexOptions.Compiled);

    //        input = replace_a_Accents.Replace(input, "a");
    //        input = replace_e_Accents.Replace(input, "e");
    //        input = replace_i_Accents.Replace(input, "i");
    //        input = replace_o_Accents.Replace(input, "o");
    //        input = replace_u_Accents.Replace(input, "u");
    //        input = replace_n_Accents.Replace(input, "n");
    //        input = input.Replace("ñ", "n");
    //        return input;
    //    }

    //    #endregion
    //}
}
