using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Finanzas/MatriculaCabecera
    /// Autor: Carlos Crispin - Jose Villena
    /// Fecha: 01/05/2021
    /// <summary>
    /// BO para el obtener informacion de la T_CronogramaPagoDetalleFinal
    /// </summary>
    public class CronogramaPagoDetalleFinalBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdMatriculaCabecera		            Id de la Matricula Cabecera (PK de la tabla fin.T_MatriculaCabecera)
        /// NroCuota                            Numero de cuota
        /// NroSubCuota		                    Numero de subcuota
        /// FechaVencimiento		            Fecha de vencimiento de la cuota
        /// TotalPagar		                    Monto total a pagar
        /// Cuota	                            Monto Cuota
        /// Saldo                               Saldo a pagar
        /// Mora                                Mora adicional
        /// MoraTarifario                       Mora por tarifario
        /// MontoPagado                         Monto Pagado
        /// FechaPago                           Fecha de Pago
        /// IdFormaPago                         Id de Forma de pago (PK de la tabla fin.T_FormaPago)
        /// IdCuenta                            Id de la Cuenta (PK de la tabla T_Cuenta)
        /// FechaPagoBanco                      Fecha de pago en el Banco
        /// Enviado                             Envio si o no
        /// Observaciones                       Observaciones
        /// IdDocumentoPago                     Id de Documento Pago (PK de la tabla T_DocumentoPago)
        /// NroDocumento                        Numero del documento de pago
        /// MonedaPago                          Tipo de moneda de pago (soles, dolares, etc)
        /// TipoCambio                          Tipo de cambio
        /// CuotaDolares                        Cuota de dolares
        /// FechaProcesoPago                    Fecha de registro del proceso de pagos
        /// Version                             Version
        /// Aprobado                            Esta aproabo o no(0,1)
        /// FechaDeposito                       Fecha de deposito
        /// FechaProcesoPagoReal                Indica la fecha proceso de pago real modificada
        /// FechaIngresoEnCuenta                Indica la fecha de pago que ingresa a la cuenta bancaria
        /// FechaEfectivoDisponible             Indica la fecha de disponibilidad del efectivo en las cuentas bancarias
        /// Aprobado                            Esta aproabo o no(0,1)
        /// FechaCompromiso(1-2-3)              Fecha de Compromiso Pago del Alumno 1,2,3
        /// FechaGeneracionCompromiso(1-2-3)    Fecha de Generacion del Compromiso 1,2,3
        public int? IdMatriculaCabecera { get; set; }
        public int? NroCuota { get; set; }
        public int? NroSubCuota { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal? TotalPagar { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? Mora { get; set; }
        public decimal? MoraTarifario { get; set; }
        public decimal? MontoPagado { get; set; }
        public bool? Cancelado { get; set; }
        public string TipoCuota { get; set; }
        public string Moneda { get; set; }
        public DateTime? FechaPago { get; set; }
        public int? IdFormaPago { get; set; }
        public int? IdCuenta { get; set; }
        public DateTime? FechaPagoBanco { get; set; }
        public bool? Enviado { get; set; }
        public string Observaciones { get; set; }
        public int? IdDocumentoPago { get; set; }
        public string NroDocumento { get; set; }
        public string MonedaPago { get; set; }
        public decimal? TipoCambio { get; set; }
        public decimal? CuotaDolares { get; set; }
        public DateTime? FechaProcesoPago { get; set; }
        public int? Version { get; set; }
        public bool? Aprobado { get; set; }
        public DateTime? FechaDeposito { get; set; }
        public Guid? IdMigracion { get; set; }
        public DateTime? FechaProcesoPagoReal { get; set; }
        public DateTime? FechaIngresoEnCuenta { get; set; }
        public DateTime? FechaEfectivoDisponible { get; set; }
        public DateTime? FechaCompromiso1 { get; set; }
        public DateTime? FechaCompromiso2 { get; set; }
        public DateTime? FechaCompromiso3 { get; set; }
        public DateTime? FechaGeneracionCompromiso1 { get; set; }
        public DateTime? FechaGeneracionCompromiso2 { get; set; }
        public DateTime? FechaGeneracionCompromiso3 { get; set; }
        public string UsuarioCoordinadorAcademico { get; set; }

        public byte[] GenerarCrep(CrepCabeceraDTO objeto, List<CrepListaCuotasSeleccionadasDTO> lista, List<CrepListaAlumnosDTO> listalumnos)
        {
            /*
           * Cabecera
           1 - 2 2 A Tipo de registro (CC= Cabecera, DD= Detalle)
           3 - 5 3 N Código de la sucursal1 (de la cuenta de la empresa afiliada)
           6 - 6 1 N Código de la moneda1 (de la cuenta de la empresa afiliada), (soles = “0”, dólares = “1”)
           7 - 13 7 N Número de cuenta de la empresa afiliada1
           14 - 14 1 A Tipo de validación (C= Completa)
           15 - 54 40 A Nombre de la empresa afiliada
           55 - 62 8 N Fecha de transmisión (AAAAMMDD)
           63 - 71 9 N Cantidad total de registros enviados en el detalle
           72 - 86 15 N Monto total enviado (2 decimales)(sólo validación completa)*
           87 - 87 1 A Tipo de archivo (R=Archivo de Reemplazo, A=Archivo de Actualización)
           88 - 200 113 N Filler (libre)
            */

            /*Detalle
            1 - 2 2 A Tipo de registro (CC= Cabecera, DD= Detalle)
            3 - 5 3 N Código de la sucursal (de la cuenta de la empresa afiliada)
            6 - 6 1 N Código de la moneda (de la cuenta de la empresa afiliada)
            7 - 13 7 N Número de cuenta de la empresa afiliada
            14 - 27 14 A-N-A/N Código de Identificación del Depositante o Usuario (ver página 5 y 6)
            28 - 67 40 A Nombre del Depositante (para registros tipo “A” o “M”)
            68 - 97 30 A-N-A/N Campo con información de retorno (para registros tipo “A” o “M”)
            98 - 105 8 N Fecha de emisión del cupón (sólo validación completa) (para registros tipo “A” o “M”)
            106 - 113 8 N Fecha de vencimiento del cupón (sólo validación completa) (para registros tipo “A” o “M”)
            114 - 128 15 N Monto del cupón (2 decimales) (sólo validación completa)(para registros tipo “A” o “M”)
            129 - 143 15 N Monto de mora* (2 decimales) (sólo validación completa) (para registros tipo “A” o “M”)
            144 - 152 9 N Monto mínimo1 (2 decimales) (sólo validación completa con rangos)(para registros tipo “A” o “M”)
            153 - 153 1 A Tipo de Registro2 (A=Registro a Agregar, M=Registro a Modificar, E=Registro a Eliminar)
            154 - 200 47 N Filler (libre)
                */

            //generamos la cabecera
            CronogramaPagoDetalleModLogFinalRepositorio _repCronogramaPagoDetalleModLogFinalRep = new CronogramaPagoDetalleModLogFinalRepositorio();
            StringBuilder linea = new StringBuilder();
            string _nombrearchivo = String.Empty;
            if (objeto.NombreArchivo == "")
            {
                _nombrearchivo = "CREP_X";
            }
            else
            {
                _nombrearchivo = objeto.NombreArchivo;
            }
            string _tiporegistro = "CC";
            string _codigosucursal = "215";
            string _codigomoneda = (objeto.Moneda == "SOLES" ? "0" : "1");
            string _nrocuenta = objeto.hidCuenta; //---------------//
            string _tipovalidacion = "C";
            //string _nombreempresa = "BSGRUPOLIMASOLES".PadRight(40);
            string _nombreempresa = ("BSGRUPO" + objeto.hidCiudad + objeto.Moneda).PadRight(40);
            string _fecha = String.Format("{0:yyyyMMdd}", DateTime.Now);
            string _temp = ObtenerTotales(listalumnos, lista, objeto);
            //colocamos el nombre para guiarnos
            var la = listalumnos;
            var lc = lista;

            string _totalregistros = _temp.Split('&').GetValue(1).ToString().PadLeft(9, '0');
            string _montototal = _temp.Split('&').GetValue(0).ToString().PadLeft(15, '0');

            string _tipoarchivo = "A";
            string _libre = "000".PadLeft(113);
            byte[] registroCrepByte;
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter myStreamWriter = new StreamWriter(ms))
                {
                    linea.Append(_tiporegistro + _codigosucursal + _codigomoneda + _nrocuenta + _tipovalidacion + _nombreempresa + _fecha + _totalregistros + _montototal + _tipoarchivo + _libre);
                    myStreamWriter.WriteLine(linea.ToString());
                    linea.Remove(0, linea.Length);

                    string _codigousuario, _nombreusuario, _fechaemision, _montomora, _montominimo, _tiporegistro2;
                    string _codigoespecial, _montocuota, _fechavencimiento;
                    /////////////////////////////////////////////////////////////

                    //generamos en forma automática o manual
                    if (objeto.ManualAutomatico == "Automatica")
                    {

                        string _cuotao = "";
                        string _fechav = "";
                        string _morao = "0";

                        //generamos el detalle
                        string _tiporegistrod = "DD";
                        string _libred = "".PadLeft(47);
                        for (int x = 0; x < la.Count; x++)
                        {
                            _codigousuario = la[x].CodigoMatricula.PadLeft(14);
                            _nombreusuario = la[x].NombreCompletoAlumno.PadRight(40);
                            _fechaemision = String.Format("{0:yyyyMMdd}", la[x].FechaMatricula);

                            _montomora = "0".PadLeft(15, '0');
                            _montominimo = "0".PadLeft(9, '0');

                            //_tiporegistro2 = Modalidad.Text; //el valor del combo Modalidad

                            //Obteniendo cuotas originales, o las que se han personalizado
                            //verificar si se ha personalizado (primera fuente), caso contrario, van las originales
                            //ir guardando los datos personalizados en una lista de clase

                            //***** buscar en lc en base al codigo de usuario
                            //filtramos con los datos del usuario actual
                            //List<AlumnoC> results = lc.Exists(
                            bool existeTmp = lc.Exists(
                                delegate (CrepListaCuotasSeleccionadasDTO c)//Alumnoc=CrepListaCuotasSeleccionadasDTO
                                {
                                    return c.CodUsuario == la[x].CodigoMatricula;
                                }
                                );

                            if (existeTmp)
                            {
                                int indexi = lc.FindIndex(
                                delegate (CrepListaCuotasSeleccionadasDTO c)
                                {
                                    return c.CodUsuario == la[x].CodigoMatricula;
                                }
                                );
                                int indexf = lc.FindLastIndex(
                                delegate (CrepListaCuotasSeleccionadasDTO c)
                                {
                                    return c.CodUsuario == la[x].CodigoMatricula;
                                }
                                );
                                for (int a = indexi; a <= indexf; a++)
                                {
                                    _codigoespecial = (lc[a].CodigoEspecial.ToString() + lc[a].Adicional).PadRight(30);
                                    if (lc[a].enviado) //se genera primero ELIMINAR
                                    {
                                        //extraemos la cantidad original y la fecha anterior enviada
                                        MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                                        var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == lc[a].CodUsuario, y => new { y.Id }).FirstOrDefault();
                                        var objdatoslog = _repCronogramaPagoDetalleModLogFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.NroCuota == lc[a].nroCuota && w.NroSubCuota == lc[a].nroSubcuota && w.Aprobado == true && w.Ultimo == true).Select(y => new { y.Cuota, y.Mora, y.FechaVencimiento }).FirstOrDefault();//ExtraerDatosLog(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);
                                        _cuotao = objdatoslog.Cuota.ToString();
                                        _fechav = objdatoslog.FechaVencimiento.ToString();
                                        //_morao = objdatoslog.mora.ToString();

                                        _montocuota = String.Format("{0:0.00}", (Convert.ToDouble(_cuotao) + Convert.ToDouble(_morao))).Replace(".", "").PadLeft(15, '0');
                                        //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(_fechav));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(_fechav, "d/MM/yyyy HH:mm:ss", null));
                                        _tiporegistro2 = "E";
                                        linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuenta + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                        myStreamWriter.WriteLine(linea.ToString());
                                        linea.Remove(0, linea.Length);
                                    }
                                    //luego ya se genera la ACTUALIZACION (verificando la fecha de vencimiento, si es antes se envió a Eliminar... 
                                    //--debe ser un día más en caso la fecha sea igual)
                                    _tiporegistro2 = "A";
                                    _montocuota = String.Format("{0:0.00}", (Convert.ToDouble(lc[a].Cuota) + Convert.ToDouble(lc[a].Mora))).Replace(".", "").PadLeft(15, '0');
                                    _fechavencimiento = "";
                                    if (lc[a].fechaVencimiento == lc[a].fechaAnterior)
                                    {
                                        //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(lc[a].fechaVencimiento).AddDays(1));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(lc[a].fechaVencimiento, "dd/MM/yyyy", null).AddDays(1));
                                    }
                                    else
                                    {
                                        //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(lc[a].fechaVencimiento));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(lc[a].fechaVencimiento, "dd/MM/yyyy", null));
                                    }
                                    linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuenta + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                    myStreamWriter.WriteLine(linea.ToString());
                                    linea.Remove(0, linea.Length);
                                    //actualizamos ENVIADO y ULTIMO

                                    CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinalRep = new CronogramaPagoDetalleFinalRepositorio();

                                    var insertado = _repCronogramaPagoDetalleFinalRep.ActualizarEnviado(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);
                                    var actualizado = _repCronogramaPagoDetalleFinalRep.ActualizarUltimo(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);
                                }
                            }
                            else //se extrae de la base
                            {
                                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinalRep = new CronogramaPagoDetalleFinalRepositorio();
                                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(w => w.CodigoMatricula == la[x].CodigoMatricula, w => new { w.Id }).FirstOrDefault();
                                var versionAprobada = _repCronogramaPagoDetalleFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Aprobado == true, w => new { w.Version }).OrderByDescending(w => w.Version).FirstOrDefault();


                                var dt = _repCronogramaPagoDetalleFinalRep.ObtenerCuotas(matriculaCabeceraTemp.Id, versionAprobada.Version);//ObtenerCuotas(la[x].CodigoMatricula);
                                for (int i = 0; i < dt.Count; i++)
                                {
                                    _codigoespecial = dt[i].CodigoEspecial.ToString().PadRight(30);
                                    if (Convert.ToBoolean(dt[i].Enviado)) //se genera primero ELIMINAR
                                    {
                                        //extraemos la cantidad original y la fecha anterior enviada
                                        var matriculaCabeceraTemp2 = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == la[x].CodigoMatricula, y => new { y.Id }).FirstOrDefault();

                                        var objdatoslog = _repCronogramaPagoDetalleModLogFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp2.Id && w.NroCuota == dt[i].NroCuota && w.NroSubCuota == dt[i].NroSubCuota && w.Aprobado == true && w.Ultimo == true).Select(y => new { y.Cuota, y.Mora, y.FechaVencimiento }).FirstOrDefault();//ExtraerDatosLog(la[x].CodigoMatricula, dt[i].NroCuota.ToString(), dt[i].NroSubCuota.ToString());
                                        _cuotao = objdatoslog.Cuota.ToString();
                                        _fechav = objdatoslog.FechaVencimiento.ToString();
                                        //_morao = objdatoslog.mora.ToString();
                                        _montocuota = String.Format("{0:0.00}", (Convert.ToDouble(_cuotao) + Convert.ToDouble(_morao))).Replace(".", "").PadLeft(15, '0');
                                        // _fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(_fechav));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(_fechav, "d/MM/yyyy HH:mm:ss", null));                                        
                                        _tiporegistro2 = "E";
                                        linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuenta + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                        myStreamWriter.WriteLine(linea.ToString());
                                        linea.Remove(0, linea.Length);
                                    }
                                    //luego ya se genera la ACTUALIZACION (verificando la fecha de vencimiento, si es antes se envió a Eliminar... 
                                    //--debe ser un día más en caso la fecha sea igual)
                                    _tiporegistro2 = "A";
                                    _montocuota = dt[i].Cuota.ToString().Replace(".", "").PadLeft(15, '0');
                                    _fechavencimiento = "";

                                    if ((dt[i].FechaVencimiento.ToString()) == (dt[i].FechaAnterior.ToString()))
                                    {
                                       // _fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dt[i].FechaVencimiento).AddDays(1));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(dt[i].FechaVencimiento, "dd/MM/yyyy", null).AddDays(1));                                        
                                    }
                                    else
                                    {
                                        //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dt[i].FechaVencimiento));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(dt[i].FechaVencimiento, "dd/MM/yyyy", null));                                        
                                    }

                                    linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuenta + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                    myStreamWriter.WriteLine(linea.ToString());
                                    linea.Remove(0, linea.Length);
                                    //actualizamos ENVIADO y ULTIMO
                                    //var insertado = _tcronogramapagosdetalle_finalRepository.ActualizarEnviado(la[x].CodigoMatricula, dt[i].NroCuota.ToString(), dt[i].NroSubCuota.ToString()).FirstOrDefault();
                                    //var actualizado = _tcronogramapagosdetalle_finalRepository.ActualizarUltimo(la[x].CodigoMatricula, dt[i].NroCuota.ToString(), dt[i].NroSubCuota.ToString()).FirstOrDefault();

                                    var insertado = _repCronogramaPagoDetalleFinalRep.ActualizarEnviado(la[x].CodigoMatricula, dt[i].NroCuota, dt[i].NroSubCuota);
                                    var actualizado = _repCronogramaPagoDetalleFinalRep.ActualizarUltimo(la[x].CodigoMatricula, dt[i].NroCuota, dt[i].NroSubCuota);

                                }
                            }
                            //
                        }
                    }
                    else //MANUAL
                    {
                        //generamos el detalle
                        string _tiporegistrod = "DD";
                        string _libred = "".PadLeft(47);
                        for (int x = 0; x < la.Count; x++)
                        {
                            _codigousuario = la[x].CodigoMatricula.PadLeft(14);
                            _nombreusuario = la[x].NombreCompletoAlumno.PadRight(40);
                            _fechaemision = String.Format("{0:yyyyMMdd}", la[x].FechaMatricula);

                            _montomora = "0".PadLeft(15, '0');
                            _montominimo = "0".PadLeft(9, '0');

                            _tiporegistro2 = objeto.ActualizarEliminar;//A-E//Modalidad.Text; //el valor del combo Modalidad

                            //Obteniendo cuotas originales, o las que se han personalizado
                            //verificar si se ha personalizado (primera fuente), caso contrario, van las originales
                            //ir guardando los datos personalizados en una lista de clase

                            //***** buscar en lc en base al codigo de usuario
                            //filtramos con los datos del usuario actual
                            //List<AlumnoC> results = lc.Exists(
                            bool existeTmp = lc.Exists(
                                delegate (CrepListaCuotasSeleccionadasDTO c)
                                {
                                    return c.CodUsuario == la[x].CodigoMatricula;
                                }
                                );

                            if (existeTmp)
                            {
                                int indexi = lc.FindIndex(
                                delegate (CrepListaCuotasSeleccionadasDTO c)
                                {
                                    return c.CodUsuario == la[x].CodigoMatricula;
                                }
                                );
                                int indexf = lc.FindLastIndex(
                                delegate (CrepListaCuotasSeleccionadasDTO c)
                                {
                                    return c.CodUsuario == la[x].CodigoMatricula;
                                }
                                );
                                for (int a = indexi; a <= indexf; a++)
                                {
                                    _codigoespecial = (lc[a].CodigoEspecial.ToString() + lc[a].Adicional).PadRight(30);
                                    _montocuota = String.Format("{0:0.00}", (Convert.ToDouble(lc[a].Cuota) + Convert.ToDouble(lc[a].Mora))).Replace(".", "").PadLeft(15, '0');
                                    _fechavencimiento = "";
                                    if (objeto.ActualizarEliminar == "A")
                                    {
                                        //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(lc[a].fechaVencimiento));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(lc[a].fechaVencimiento, "dd/MM/yyyy", null));
                                    }
                                    if (objeto.ActualizarEliminar == "E")
                                    {
                                        lc[a].fechaAnterior = lc[a].fechaAnterior == null ? "" : lc[a].fechaAnterior;
                                        if (lc[a].fechaAnterior.ToString() == "") //de repente es 1er envio
                                        {
                                            //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(lc[a].fechaVencimiento));
                                            _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(lc[a].fechaVencimiento, "dd/MM/yyyy", null));
                                        }
                                        else
                                        {
                                            //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(lc[a].fechaAnterior));
                                            _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(lc[a].fechaAnterior, "dd/MM/yyyy", null));
                                        }
                                    }
                                    linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuenta + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                    myStreamWriter.WriteLine(linea.ToString());
                                    linea.Remove(0, linea.Length);
                                    //actualizamos ENVIADO y ULTIMO
                                    //var insertado = _tcronogramapagosdetalle_finalRepository.ActualizarEnviado(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota).FirstOrDefault();
                                    //var actualizado = _tcronogramapagosdetalle_finalRepository.ActualizarUltimo(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota).FirstOrDefault();
                                    CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinalRep = new CronogramaPagoDetalleFinalRepositorio();
                                    var insertado = _repCronogramaPagoDetalleFinalRep.ActualizarEnviado(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);
                                    var actualizado = _repCronogramaPagoDetalleFinalRep.ActualizarUltimo(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);
                                }
                            }
                            else
                            {
                                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinalRep = new CronogramaPagoDetalleFinalRepositorio();
                                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == la[x].CodigoMatricula, y => new { y.Id }).FirstOrDefault();
                                var versionAprobada = _repCronogramaPagoDetalleFinalRep.GetBy(y => y.IdMatriculaCabecera == matriculaCabeceraTemp.Id && y.Aprobado == true, y => new { y.Version }).OrderByDescending(y => y.Version).FirstOrDefault();


                                var dt = _repCronogramaPagoDetalleFinalRep.ObtenerCuotas(matriculaCabeceraTemp.Id, versionAprobada.Version);//ObtenerCuotas(la[x].CodigoMatricula);
                                for (int i = 0; i < dt.Count; i++)
                                {
                                    _codigoespecial = dt[i].CodigoEspecial.ToString().PadRight(30);
                                    _montocuota = dt[i].Cuota.ToString().Replace(".", "").PadLeft(15, '0');
                                    _fechavencimiento = "";

                                    if (objeto.ActualizarEliminar == "A")
                                    {
                                        //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dt[i].FechaVencimiento));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(dt[i].FechaVencimiento, "dd/MM/yyyy", null));
                                    }
                                    if (objeto.ActualizarEliminar == "E")
                                    {
                                        if (dt[i].FechaAnterior.ToString() == "") //de repente es 1er envio
                                        {
                                            //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dt[i].FechaVencimiento));
                                            _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(dt[i].FechaVencimiento, "dd/MM/yyyy", null));
                                        }
                                        else
                                        {
                                            //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dt[i].FechaAnterior));
                                            _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(dt[i].FechaAnterior, "dd/MM/yyyy", null));
                                        }
                                    }
                                    linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuenta + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                    myStreamWriter.WriteLine(linea.ToString());
                                    linea.Remove(0, linea.Length);
                                    //actualizamos ENVIADO y ULTIMO
                                    //var insertado = _tcronogramapagosdetalle_finalRepository.ActualizarEnviado(la[x].CodigoMatricula, dt[i].NroCuota.ToString(), dt[i].NroSubCuota.ToString()).FirstOrDefault();
                                    //var actualizado = _tcronogramapagosdetalle_finalRepository.ActualizarUltimo(la[x].CodigoMatricula, dt[i].NroCuota.ToString(), dt[i].NroSubCuota.ToString()).FirstOrDefault();

                                    var insertado = _repCronogramaPagoDetalleFinalRep.ActualizarEnviado(la[x].CodigoMatricula, dt[i].NroCuota, dt[i].NroSubCuota);
                                    var actualizado = _repCronogramaPagoDetalleFinalRep.ActualizarUltimo(la[x].CodigoMatricula, dt[i].NroCuota, dt[i].NroSubCuota);
                                }
                            }
                            //
                        }
                    }

                    //escribimos el archivo
                    myStreamWriter.Close();
                }
                registroCrepByte = ms.ToArray();
            }

            return registroCrepByte;
        }

        private string ObtenerTotales(List<CrepListaAlumnosDTO> la, List<CrepListaCuotasSeleccionadasDTO> lc, CrepCabeceraDTO objeto)
        {
            CronogramaPagoDetalleModLogFinalRepositorio _repCronogramaPagoDetalleModLogFinalRep = new CronogramaPagoDetalleModLogFinalRepositorio();
            CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinalRep = new CronogramaPagoDetalleFinalRepositorio();

            string _cuotao = "";
            string _fechav = "";
            string _morao = "0";
            //total de registros y de monto
            int _totalregistros = 0;
            double _totalmonto = 0.00;
            for (int i = 0; i < la.Count; i++)
            {
                //interceptamos
                bool existeTmp = lc.Exists(
                    delegate (CrepListaCuotasSeleccionadasDTO c)
                    {
                        return c.CodUsuario == la[i].CodigoMatricula;
                    }
                    );

                if (existeTmp)
                {
                    int indexi = lc.FindIndex(
                    delegate (CrepListaCuotasSeleccionadasDTO c)
                    {
                        return c.CodUsuario == la[i].CodigoMatricula;
                    }
                    );
                    int indexf = lc.FindLastIndex(
                    delegate (CrepListaCuotasSeleccionadasDTO c)
                    {
                        return c.CodUsuario == la[i].CodigoMatricula;
                    }
                    );
                    for (int a = indexi; a <= indexf; a++)
                    {
                        if ((lc[a].enviado) && (objeto.ManualAutomatico == "Automatica")) //cuando es automático
                        {

                            //Obtenemos el IdMatricula
                            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                            var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == lc[a].CodUsuario, x => new { x.Id }).FirstOrDefault();
                            //extraemos la cantidad original y la fecha anterior enviada
                            var objdatoslog = _repCronogramaPagoDetalleModLogFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.NroCuota == lc[a].nroCuota && w.NroSubCuota == lc[a].nroSubcuota && w.Aprobado == true && w.Ultimo == true).Select(x => new { x.Cuota, x.Mora, x.FechaVencimiento }).FirstOrDefault();//lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);
                            _cuotao = objdatoslog.Cuota.ToString();
                            _fechav = objdatoslog.FechaVencimiento.ToString();
                            //_morao = objdatoslog.mora.ToString();
                            _totalmonto += (Convert.ToDouble(_cuotao) + Convert.ToDouble(_morao));
                            _totalregistros += 1;
                        }
                        _totalmonto += (Convert.ToDouble(lc[a].Cuota) + Convert.ToDouble(lc[a].Mora));
                    }
                    _totalregistros += (indexf - indexi + 1);
                }
                else
                {
                    //verificamos las montos para registros que ya han sido enviados
                    MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                    var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == la[i].CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                    var versionAprobada = _repCronogramaPagoDetalleFinalRep.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();

                    var dt = _repCronogramaPagoDetalleFinalRep.ObtenerCuotas(matriculaCabeceraTemp.Id, versionAprobada.Version);//ObtenerCuotas(la[i].Matricula);
                    for (int x = 0; x < dt.Count; x++)
                    {
                        if (Convert.ToBoolean(dt[x].Enviado)) //se genera primero ELIMINAR
                        {
                            //extraemos la cantidad original y la fecha anterior enviada
                            var objdatoslog = _repCronogramaPagoDetalleModLogFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.NroCuota == dt[x].NroCuota && w.NroSubCuota == dt[x].NroSubCuota && w.Aprobado == true && w.Ultimo == true).Select(w => new { w.Cuota, w.Mora, w.FechaVencimiento }).FirstOrDefault();//ExtraerDatosLog(la[i].Matricula, dt[x].nroCuota.ToString(), dt[x].nroSubCuota.ToString());
                            _cuotao = objdatoslog.Cuota.ToString();
                            _fechav = objdatoslog.FechaVencimiento.ToString();
                            //_morao = objdatoslog.mora.ToString();
                            _totalmonto += (Convert.ToDouble(_cuotao) + Convert.ToDouble(_morao));
                            _totalregistros += 1;
                        }
                        _totalmonto += (Convert.ToDouble(dt[x].Cuota.ToString()));
                    }
                    _totalregistros += dt.Count;
                    /*
                    ip.ObtenerTotales(la[i].Matricula); //se extraen los datos de la base
                    _totalmonto += ip.TotalMonto(); //monto global solo de los regulares
                    _totalregistros += ip.TotalRegistros(); //cantidad total de los regulares
                     */
                }
            }

            return String.Format("{0:0.00}", _totalmonto).Replace(".", "") + "&" + _totalregistros.ToString();
        }

        public bool GuardarPago(PagoCuotaCronogramaDTO objeto, List<CronogramaPagoDetalleFinalBO> CronogramaActual, int NroCuotaGlobal, int NroSubCuotaGlobal)
        {
            CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
            CronogramaCabeceraCambioRepositorio _repCronogramaCabeceraCambio = new CronogramaCabeceraCambioRepositorio();
            CronogramaPagoDetalleModLogFinalRepositorio _repCronogramaPagoDetalleModLogFinal = new CronogramaPagoDetalleModLogFinalRepositorio();
            CronogramaDetalleCambioRepositorio _repCronogramaDetalleCambio = new CronogramaDetalleCambioRepositorio();
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
            var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == objeto.CodigoMatricula, y => new { y.Id }).FirstOrDefault();

            try
            {
                //el monto ya viene con mora mas ya no necesito agregarlo aqui
                //objeto.monto = objeto.monto + objeto.mora;
                //el monto base le sumo la mora 
                objeto.MontoBase = objeto.MontoBase + objeto.Mora;

                decimal monto = 0;
                decimal tipocambio = 1;
                int monedabase = 0;

                if (objeto.MonedaBase == "soles")
                    monedabase = 1;
                if (objeto.MonedaBase == "dolares")
                    monedabase = 2;
                if (objeto.MonedaBase == "bolivianos")
                    monedabase = 3;
                if (objeto.MonedaBase == "pesos mexicanos")
                    monedabase = 4;
                if (objeto.MonedaBase == "pesos colombianos")
                    monedabase = 5;

                int tiposmoneda = 0;//1:misma moneda,2:deuda soles - pago dolares,3:deuda dolares - pago dolares


                if (monedabase == objeto.Moneda)//si son las mismas monedas
                {
                    monto = objeto.Monto;
                    tiposmoneda = 1;
                }
                else if (monedabase == 1 && objeto.Moneda == 2)//monto base soles y paga con dolares
                {
                    tipocambio = objeto.TipoCambio;
                    monto = objeto.Monto * tipocambio;//monto en soles
                    tiposmoneda = 2;
                }
                else if (monedabase == 2 && objeto.Moneda == 1)//deuda en dolares y estoy pagando en soles
                {
                    tipocambio = objeto.TipoCambio;
                    monto = objeto.Monto / tipocambio;//monto en  dolares
                    tiposmoneda = 3;
                }

                //traigo la cuota del cronograma actual // si es igual el monto //si es menor ya lo valida en la vista
                var cuotaapagar = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.NroCuota == objeto.NroCuota && w.NroSubCuota == objeto.NroSubCuota && w.Version == CronogramaActual.FirstOrDefault().Version).FirstOrDefault(); //this.GetbyCuotaSubCuotaVersion(objeto.NroCuota, objeto.NroSubCuota, CronogramaActual.FirstOrDefault().Version, objeto.CodigoMatricula).FirstOrDefault();//cronogramaactual.Where(w => w.Cuota == objeto.nrocuota && w.SubCuota == objeto.nrosubcuota).FirstOrDefault();//siempre deberia traer un valor
                //se valida con la mora mas ahora para que cuadre y no se cree otra cuota
                if ((cuotaapagar.Cuota + cuotaapagar.Mora) == monto || (cuotaapagar.Cuota + cuotaapagar.Mora) == Math.Round(monto, 2))// los montos son iguales en ese caso cancelo solo esa cuota sin crear una nueva version
                {
                    if ((cuotaapagar.Cuota + cuotaapagar.Mora) != monto)
                    {
                        monto = Math.Round(monto, 2);
                    }

                    string monedapagos = objeto.Moneda == 1 ? "soles" : (objeto.Moneda == 2 ? "dolares" : (objeto.Moneda == 3 ? "bolivianos" : (objeto.Moneda == 4 ? "pesos mexicanos" : "pesos colombianos")));
                    cuotaapagar.FechaProcesoPago = DateTime.Now;//el dia que registro el apgo en el sistema
                    cuotaapagar.TipoCambio = objeto.TipoCambio;
                    cuotaapagar.MonedaPago = monedapagos;
                    cuotaapagar.NroDocumento = objeto.NroDocumento;
                    cuotaapagar.IdDocumentoPago = objeto.Documento;
                    cuotaapagar.IdCuenta = objeto.NroCuenta;
                    cuotaapagar.IdFormaPago = objeto.FormaPago;
                    cuotaapagar.FechaPago = objeto.Fecha;
                    cuotaapagar.Cancelado = true;
                    cuotaapagar.MontoPagado = objeto.Monto;
                    cuotaapagar.FechaModificacion = DateTime.Now;
                    cuotaapagar.UsuarioModificacion = objeto.usuario;


                    var actualizado = _repCronogramaPagoDetalleFinal.Update(cuotaapagar);//this.Update(cuotaapagar);//hace el pago directo en la misma version
                    //inserto en tpagosfinal
                    //objeto.formapago//esta en int y debe ser en string
                    //objeto.documento//esta en int  y debe ser string
                    //objeto.nrocuenta//esta en int y debe ser string
                    var insertado = this.InsertarPagoWebFinal(objeto.CodigoMatricula, objeto.Monto, monedapagos, (float)objeto.TipoCambio, objeto.RUC, objeto.FormaPago, objeto.Documento, objeto.NroDocumento, objeto.NroCuenta, objeto.NroCheque, DateTime.Now, objeto.NroDeposito, objeto.usuario);
                    //fin inserto en tpagosfinal

                    //valido si la cuota es 1-1
                    if (objeto.NroCuota == 1 && objeto.NroSubCuota == 1)// si esta pagando la primera cuota
                    {
                        var OriginalActualizado = this.ActualizarOriginal(CronogramaActual.FirstOrDefault().Version.Value, objeto.CodigoMatricula, objeto.usuario);
                    }

                    return true;
                }
                else
                {
                    var cronograma = CronogramaActual;
                    int versionactual = cronograma.FirstOrDefault().Version.Value + 1;

                    string monedapagos = objeto.Moneda == 1 ? "soles" : (objeto.Moneda == 2 ? "dolares" : (objeto.Moneda == 3 ? "bolivianos" : (objeto.Moneda == 4 ? "pesos mexicanos" : "pesos colombianos")));

                    List<CronogramaPagoDetalleFinalBO> nuevocronograma = new List<CronogramaPagoDetalleFinalBO>();
                    List<CronogramaDetalleCambioBO> detallecambios = new List<CronogramaDetalleCambioBO>();
                    decimal resto = 0;
                    bool flagresto = false;
                    for (int i = 0; i < cronograma.Count(); i++)
                    {

                        if (flagresto == true)
                        {
                            if (cronograma.Where(w => w.NroCuota == cronograma[i].NroCuota && w.NroSubCuota == cronograma[i].NroSubCuota + 1).FirstOrDefault() != null)// si en el cronograma hay uno con el nro de subcuota +1
                            {
                                bool exista = true;
                                int cont = 1;
                                int maximosubcuota = 0;
                                while (exista)
                                {
                                    var itemexiste = cronograma.Where(w => w.NroCuota == cronograma[i].NroCuota && w.NroSubCuota == cronograma[i].NroSubCuota + cont).FirstOrDefault();

                                    if (itemexiste != null)
                                    {
                                        exista = true;
                                        cont++;
                                    }
                                    else
                                    {
                                        maximosubcuota = cronograma[i].NroSubCuota.Value + cont;
                                        exista = false;
                                    }

                                }
                                while (cronograma[i].NroSubCuota != maximosubcuota - 1)
                                {
                                    var temp = cronograma.Where(w => w.NroCuota == cronograma[i].NroCuota && w.NroSubCuota == maximosubcuota - 1).FirstOrDefault();
                                    CronogramaDetalleCambioBO nuevo1 = new CronogramaDetalleCambioBO()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        IdCronogramaCabeceraCambio = 0,//abajo lo cambio
                                        NroCuota = temp.NroCuota.Value,
                                        NroSubcuota = maximosubcuota,
                                        FechaVencimiento = temp.FechaVencimiento.Value,
                                        Cuota = temp.Cuota.Value,
                                        Mora = temp.Mora.Value,
                                        Moneda = temp.Moneda,
                                        Version = 0//abajo lo cambios

                                    };
                                    detallecambios.Add(nuevo1);
                                    cronograma.Where(w => w.NroCuota == cronograma[i].NroCuota && w.NroSubCuota == maximosubcuota - 1).FirstOrDefault().NroSubCuota = maximosubcuota;
                                    maximosubcuota--;
                                }

                                cronograma[i].NroSubCuota = cronograma[i].NroSubCuota + 1;
                                decimal restomonedapago = 0;
                                if (tiposmoneda == 1)//misma moneda
                                {
                                    cronograma[i].Cuota = cronograma[i].Cuota - resto;
                                    restomonedapago = resto;
                                }
                                if (tiposmoneda == 2)//(D)soles (P)dolares //sigue el resto en soles
                                {
                                    cronograma[i].Cuota = cronograma[i].Cuota - resto;
                                    restomonedapago = resto / tipocambio;
                                }
                                if (tiposmoneda == 3)//(D)dolares (P)soles //sigue el resto en dolares
                                {
                                    cronograma[i].Cuota = cronograma[i].Cuota - resto;
                                    restomonedapago = resto * tipocambio;
                                }
                                //se debe insertar en false proque es la cuota que se baja el monto
                                cronograma[i].Enviado = false;


                                //////////////////////////////////////////////////////////////////////////////////
                                //se inserta en log la nueva cuota que se modifoc de monto y se le resto el resto

                                CronogramaPagoDetalleModLogFinalBO log = new CronogramaPagoDetalleModLogFinalBO()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    Fecha = DateTime.Now,
                                    NroCuota = cronograma[i].NroCuota,
                                    NroSubCuota = cronograma[i].NroSubCuota,
                                    FechaVencimiento = cronograma[i].FechaVencimiento,
                                    TotalPagar = cronograma[i].TotalPagar,
                                    Cuota = cronograma[i].Cuota,
                                    Mora = cronograma[i].Mora,
                                    MontoPagado = 0,
                                    Saldo = cronograma[i].Saldo,
                                    Cancelado = cronograma[i].Cancelado,
                                    TipoCuota = cronograma[i].TipoCuota,
                                    Moneda = cronograma[i].Moneda,
                                    FechaPago = null,
                                    IdFormaPago = null,
                                    FechaPagoBanco = null,
                                    Ultimo = false,
                                    IdDocumentoPago = null,
                                    NroDocumento = null,
                                    MonedaPago = null,
                                    TipoCambio = null,
                                    MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA  (" + cronograma[i].Cuota + "," + cronograma[i].NroSubCuota + ")," + cronograma[i].Cuota,
                                    FechaProcesoPago = null,
                                    EstadoPrimerLog = null
                                };
                                log.Version = versionactual;
                                log.Aprobado = true;
                                log.Estado2 = true;
                                log.FechaCreacion = DateTime.Now;
                                log.FechaModificacion = DateTime.Now;
                                log.UsuarioCreacion = "SYSTEM";
                                log.UsuarioModificacion = "SYSTEM";
                                log.Estado = true;
                                var insertlog = _repCronogramaPagoDetalleModLogFinal.Insert(log);
                                //////////////////////////////////////////////////////////////////////////////////

                                //cronograma[i].Version = versionactual;
                                //nuevocronograma.Add(cronograma[i]);//añado el antiguo con subcuota+1

                                /////////////////////////////////////////// añado tmb el anterior que se volvera +1
                                CronogramaDetalleCambioBO nuevo2 = new CronogramaDetalleCambioBO()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    IdCronogramaCabeceraCambio = 0,//cabajo lo cambio
                                    NroCuota = cronograma[i].NroCuota.Value,
                                    NroSubcuota = cronograma[i].NroSubCuota.Value,
                                    FechaVencimiento = cronograma[i].FechaVencimiento.Value,
                                    Cuota = cronograma[i].Cuota.Value,
                                    Mora = cronograma[i].Mora.Value,
                                    Moneda = cronograma[i].Moneda,
                                    Version = 0//abajo lo cambios

                                };
                                detallecambios.Add(nuevo2);
                                /////////////////////////////////////////////
                                ///////////////////////////////////

                                var padre = cronograma.Where(w => w.NroCuota == objeto.NroCuota && w.NroSubCuota == objeto.NroSubCuota).FirstOrDefault();
                                //se pone la antigua como el resto
                                CronogramaPagoDetalleFinalBO nuevoresto = new CronogramaPagoDetalleFinalBO()
                                {
                                    Id = 0,
                                    NroCuota = cronograma[i].NroCuota,
                                    NroSubCuota = cronograma[i].NroSubCuota - 1,
                                    FechaVencimiento = padre.FechaVencimiento,
                                    //Total=
                                    Cuota = resto,
                                    Mora = 0,
                                    MontoPagado = restomonedapago,
                                    //Saldo
                                    Cancelado = true,
                                    TipoCuota = padre.TipoCuota,
                                    FechaDeposito = padre.FechaDeposito,
                                    Moneda = objeto.MonedaBase,
                                    FechaPago = objeto.Fecha,
                                    IdFormaPago = objeto.FormaPago,
                                    IdCuenta = objeto.NroCuenta,
                                    IdDocumentoPago = objeto.Documento,
                                    NroDocumento = objeto.NroDocumento,
                                    MonedaPago = objeto.Moneda == 1 ? "soles" : (objeto.Moneda == 2 ? "dolares" : (objeto.Moneda == 3 ? "bolivianos" : "pesos mexicanos")),
                                    TipoCambio = objeto.TipoCambio,
                                    FechaProcesoPago = DateTime.Now,
                                    Version = versionactual,//una version mas
                                    Enviado = true//200618 se manda a true la nueva con el monto del resto
                                };
                                nuevocronograma.Add(nuevoresto);//añado el nuevo resto
                                /////////////////////////////////////////////////////////////// tmb añado el resto
                                CronogramaDetalleCambioBO nuevo3 = new CronogramaDetalleCambioBO()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    IdCronogramaCabeceraCambio = 0,//cabajo lo cambio
                                    NroCuota = nuevoresto.NroCuota.Value,
                                    NroSubcuota = nuevoresto.NroSubCuota.Value,
                                    FechaVencimiento = nuevoresto.FechaVencimiento.Value,
                                    Cuota = nuevoresto.Cuota.Value,
                                    Mora = nuevoresto.Mora.Value,
                                    Moneda = nuevoresto.Moneda,
                                    Version = 0//abajo lo cambios

                                };
                                detallecambios.Add(nuevo3);
                                ///////////////////////////////////////////////////////////

                            }
                            else
                            {
                                //facil 
                                //la antigua pasa a ser la nueva  con la diferencia que se le resta el resto
                                decimal restomonedapago = 0;
                                cronograma[i].NroSubCuota = cronograma[i].NroSubCuota + 1;
                                if (tiposmoneda == 1)//misma moneda
                                {
                                    cronograma[i].Cuota = cronograma[i].Cuota - resto;
                                    restomonedapago = resto;
                                }
                                if (tiposmoneda == 2)//(D)soles (P)dolares //sigue el resto en soles
                                {
                                    cronograma[i].Cuota = cronograma[i].Cuota - resto;
                                    restomonedapago = resto / tipocambio;
                                }
                                if (tiposmoneda == 3)//(D)dolares (P)soles //sigue el resto en dolares
                                {
                                    cronograma[i].Cuota = cronograma[i].Cuota - resto;
                                    restomonedapago = resto * tipocambio;
                                }
                                //cronograma[i].Version = versionactual;
                                //nuevocronograma.Add(cronograma[i]);//añado el antiguo con subcuota+1
                                //a la cuota que se le resta se le pone ENVIADO=0
                                cronograma[i].Enviado = false;
                                //////////////////////////////////////////////////////////////////////////////////
                                //se inserta en log la nueva cuota que se modifoc de monto y se le resto el resto

                                CronogramaPagoDetalleModLogFinalBO log = new CronogramaPagoDetalleModLogFinalBO()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    Fecha = DateTime.Now,
                                    NroCuota = cronograma[i].NroCuota,
                                    NroSubCuota = cronograma[i].NroSubCuota,
                                    FechaVencimiento = cronograma[i].FechaVencimiento,
                                    TotalPagar = cronograma[i].TotalPagar,
                                    Cuota = cronograma[i].Cuota,
                                    Mora = cronograma[i].Mora,
                                    MontoPagado = 0,
                                    Saldo = cronograma[i].Saldo,
                                    Cancelado = cronograma[i].Cancelado,
                                    TipoCuota = cronograma[i].TipoCuota,
                                    Moneda = cronograma[i].Moneda,
                                    FechaPago = null,
                                    IdFormaPago = null,
                                    FechaPagoBanco = null,
                                    Ultimo = false,
                                    IdDocumentoPago = null,
                                    NroDocumento = null,
                                    MonedaPago = null,
                                    TipoCambio = null,
                                    MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA  (" + cronograma[i].NroCuota + "," + cronograma[i].NroSubCuota + ")," + cronograma[i].Cuota,
                                    FechaProcesoPago = null,
                                    EstadoPrimerLog = null
                                };
                                log.Version = versionactual;
                                log.Aprobado = true;
                                log.Estado2 = true;
                                log.FechaCreacion = DateTime.Now;
                                log.FechaModificacion = DateTime.Now;
                                log.UsuarioCreacion = "SYSTEM";
                                log.UsuarioModificacion = "SYSTEM";
                                log.Estado = true;
                                var insertlog = _repCronogramaPagoDetalleModLogFinal.Insert(log);
                                //////////////////////////////////////////////////////////////////////////////////


                                /////////////////////////////////////////// añado tmb el anterior que se volvera +1
                                CronogramaDetalleCambioBO nuevo1f = new CronogramaDetalleCambioBO()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    IdCronogramaCabeceraCambio = 0,//cabajo lo cambio
                                    NroCuota = cronograma[i].NroCuota.Value,
                                    NroSubcuota = cronograma[i].NroSubCuota.Value,
                                    FechaVencimiento = cronograma[i].FechaVencimiento.Value,
                                    Cuota = cronograma[i].Cuota.Value,
                                    Mora = cronograma[i].Mora.Value,
                                    Moneda = cronograma[i].Moneda,
                                    Version = 0//abajo lo cambios

                                };
                                detallecambios.Add(nuevo1f);
                                /////////////////////////////////////////////


                                var padre = cronograma.Where(w => w.NroCuota == objeto.NroCuota && w.NroSubCuota == objeto.NroSubCuota).FirstOrDefault();
                                //se pone la antigua como el resto
                                CronogramaPagoDetalleFinalBO nuevoresto = new CronogramaPagoDetalleFinalBO()
                                {
                                    Id = 0,
                                    NroCuota = cronograma[i].NroCuota,
                                    NroSubCuota = cronograma[i].NroSubCuota - 1,
                                    FechaVencimiento = padre.FechaVencimiento,
                                    //Total=
                                    Cuota = resto,
                                    Mora = 0,
                                    MontoPagado = restomonedapago,
                                    //Saldo
                                    Cancelado = true,
                                    TipoCuota = padre.TipoCuota,
                                    FechaDeposito = padre.FechaDeposito,
                                    Moneda = objeto.MonedaBase,
                                    FechaPago = objeto.Fecha,
                                    IdFormaPago = objeto.FormaPago,
                                    IdCuenta = objeto.NroCuenta,
                                    IdDocumentoPago = objeto.Documento,
                                    NroDocumento = objeto.NroDocumento,
                                    MonedaPago = objeto.Moneda == 1 ? "soles" : (objeto.Moneda == 2 ? "dolares" : (objeto.Moneda == 3 ? "bolivianos" : (objeto.Moneda == 4 ? "pesos mexicanos" : "pesos colombianos"))),
                                    TipoCambio = objeto.TipoCambio,
                                    FechaProcesoPago = DateTime.Now,
                                    Version = versionactual,//una version mas
                                    Enviado = true //a la cuota con el resto resta se le pone ENVIADO=1
                                };
                                nuevocronograma.Add(nuevoresto);//añado el nuevo resto
                                /////////////////////////////////////////////////////////////// tmb añado el resto
                                CronogramaDetalleCambioBO nuevo2f = new CronogramaDetalleCambioBO()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    IdCronogramaCabeceraCambio = 0,//cabajo lo cambio
                                    NroCuota = nuevoresto.NroCuota.Value,
                                    NroSubcuota = nuevoresto.NroSubCuota.Value,
                                    FechaVencimiento = nuevoresto.FechaVencimiento.Value,
                                    Cuota = nuevoresto.Cuota.Value,
                                    Mora = nuevoresto.Mora.Value,
                                    Moneda = nuevoresto.Moneda,
                                    Version = 0//abajo lo cambios

                                };
                                detallecambios.Add(nuevo2f);
                                ///////////////////////////////////////////////////////////
                            }
                            flagresto = false;
                        }

                        if (cronograma[i].NroCuota == objeto.NroCuota && cronograma[i].NroSubCuota == objeto.NroSubCuota)
                        {
                            //la cancelo
                            if (tiposmoneda == 1)//misma moneda
                            {
                                resto = monto - objeto.MontoBase;
                                objeto.Monto = monto - resto;
                                cronograma[i].MontoPagado = objeto.Monto;
                                cronograma[i].Cancelado = true;
                                cronograma[i].FechaPago = objeto.Fecha;
                                cronograma[i].IdFormaPago = objeto.FormaPago;
                                cronograma[i].IdCuenta = objeto.NroCuenta;
                                cronograma[i].IdDocumentoPago = objeto.Documento;
                                cronograma[i].NroDocumento = objeto.NroDocumento;
                                cronograma[i].MonedaPago = objeto.Moneda == 1 ? "soles" : (objeto.Moneda == 2 ? "dolares" : (objeto.Moneda == 3 ? "bolivianos" : (objeto.Moneda == 4 ? "pesos mexicanos" : "pesos colombianos")));
                                cronograma[i].TipoCambio = objeto.TipoCambio;
                                cronograma[i].FechaProcesoPago = DateTime.Now;
                                cronograma[i].Version = versionactual;//una version mas
                            }
                            //la cancelo
                            if (tiposmoneda == 2)//(D)soles (P)dolares
                            {
                                resto = monto - objeto.MontoBase;//sigue el resto en soles
                                objeto.Monto = monto - resto;
                                objeto.Monto = objeto.Monto / tipocambio;

                                cronograma[i].MontoPagado = objeto.Monto;
                                cronograma[i].Cancelado = true;
                                cronograma[i].FechaPago = objeto.Fecha;
                                cronograma[i].IdFormaPago = objeto.FormaPago;
                                cronograma[i].IdCuenta = objeto.NroCuenta;
                                cronograma[i].IdDocumentoPago = objeto.Documento;
                                cronograma[i].NroDocumento = objeto.NroDocumento;
                                cronograma[i].MonedaPago = objeto.Moneda == 1 ? "soles" : (objeto.Moneda == 2 ? "dolares" : (objeto.Moneda == 3 ? "bolivianos" : (objeto.Moneda == 4 ? "pesos mexicanos" : "pesos colombianos")));
                                cronograma[i].TipoCambio = objeto.TipoCambio;
                                cronograma[i].FechaProcesoPago = DateTime.Now;
                                cronograma[i].Version = versionactual;//una version mas
                            }
                            //la cancelo
                            if (tiposmoneda == 3)//(D)dolares (P)soles
                            {
                                resto = monto - objeto.MontoBase;//sigue el resto en dolares
                                objeto.Monto = monto - resto;
                                objeto.Monto = objeto.Monto * tipocambio;

                                cronograma[i].MontoPagado = objeto.Monto;
                                cronograma[i].Cancelado = true;
                                cronograma[i].FechaPago = objeto.Fecha;
                                cronograma[i].IdFormaPago = objeto.FormaPago;
                                cronograma[i].IdCuenta = objeto.NroCuenta;
                                cronograma[i].IdDocumentoPago = objeto.Documento;
                                cronograma[i].NroDocumento = objeto.NroDocumento;
                                cronograma[i].MonedaPago = objeto.Moneda == 1 ? "soles" : (objeto.Moneda == 2 ? "dolares" : (objeto.Moneda == 3 ? "bolivianos" : (objeto.Moneda == 4 ? "pesos mexicanos" : "pesos colombianos")));
                                cronograma[i].TipoCambio = objeto.TipoCambio;
                                cronograma[i].FechaProcesoPago = DateTime.Now;
                                cronograma[i].Version = versionactual;//una version mas
                            }
                            nuevocronograma.Add(cronograma[i]);

                            /////////////////////////////////////////////////////////////// tmb añado el resto
                            CronogramaDetalleCambioBO nuevo2f = new CronogramaDetalleCambioBO()
                            {
                                Id = 0,
                                IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                IdCronogramaCabeceraCambio = 0,//abajo lo cambio
                                NroCuota = cronograma[i].NroCuota.Value,
                                NroSubcuota = cronograma[i].NroSubCuota.Value,
                                FechaVencimiento = cronograma[i].FechaVencimiento.Value,
                                Cuota = cronograma[i].Cuota.Value,
                                Mora = cronograma[i].Mora.Value,
                                Moneda = cronograma[i].Moneda,
                                Version = 0//abajo lo cambios

                            };
                            detallecambios.Add(nuevo2f);
                            ///////////////////////////////////////////////////////////

                            if (Math.Round(resto, 2) > 0)//si hay resto
                            {

                                //030718 valido si cubre tmb la sgte cuota
                                if (resto >= (cronograma[i + 1].Cuota + cronograma[i + 1].Mora))
                                {
                                    objeto.NroCuota = cronograma[i + 1].NroCuota.Value;
                                    objeto.NroSubCuota = cronograma[i + 1].NroSubCuota.Value;
                                    objeto.MontoBase = cronograma[i + 1].Cuota.Value + cronograma[i + 1].Mora.Value;
                                    objeto.Mora = cronograma[i + 1].Mora.Value;
                                    monto = resto;
                                    //para que se valide con sgte y no vaya a resto
                                    flagresto = false;
                                }
                                else
                                {
                                    //La sgte cuota se divide en 2
                                    flagresto = true;
                                }
                                //fin 030718
                            }
                        }
                        else
                        {
                            cronograma[i].Version = versionactual;
                            nuevocronograma.Add(cronograma[i]);
                        }
                    }


                    CronogramaCabeceraCambioBO cambioagregar = new CronogramaCabeceraCambioBO()
                    {
                        Id = 0,
                        IdCronogramaTipoModificacion = 7,//add cuotas by pago
                        SolicitadoPor = 1,
                        AprobadoPor = 1,
                        Aprobado = true,
                        Cancelado = false,
                        Observacion = "Se añadio cuotas por pago excesivo en la cuota:" + objeto.NroCuota + " nrosubcuota:" + objeto.NroSubCuota,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = objeto.usuario,
                        UsuarioModificacion = objeto.usuario
                    };
                    var cambioinsertadoagregado = _repCronogramaCabeceraCambio.Insert(cambioagregar);
                    //insertamos el detalle
                    foreach (var detalles in detallecambios)
                    {
                        detalles.IdCronogramaCabeceraCambio = cambioagregar.Id;
                        detalles.Version = versionactual;
                        detalles.Estado = true;
                        detalles.FechaCreacion = DateTime.Now;
                        detalles.FechaModificacion = DateTime.Now;
                        detalles.UsuarioCreacion = objeto.usuario;
                        detalles.UsuarioModificacion = objeto.usuario;
                        var detalleinsert = _repCronogramaDetalleCambio.Insert(detalles);
                    }
                    //insertamos la nueva version
                    decimal totalmonto = nuevocronograma.Sum(w => w.Cuota.Value);

                    foreach (var itemcronograma in nuevocronograma)
                    {

                        CronogramaPagoDetalleFinalBO cuota = new CronogramaPagoDetalleFinalBO()
                        {
                            Id = 0,
                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                            NroCuota = itemcronograma.NroCuota,
                            NroSubCuota = itemcronograma.NroSubCuota,
                            FechaVencimiento = itemcronograma.FechaVencimiento,
                            TotalPagar = totalmonto,
                            Cuota = itemcronograma.Cuota,
                            Saldo = totalmonto - itemcronograma.Cuota,
                            Mora = itemcronograma.Mora,
                            MoraTarifario = itemcronograma.MoraTarifario,
                            Cancelado = itemcronograma.Cancelado,
                            TipoCuota = itemcronograma.TipoCuota,
                            Moneda = itemcronograma.Moneda,
                            TipoCambio = itemcronograma.TipoCambio,
                            Version = versionactual,
                            FechaPago = itemcronograma.FechaPago,
                            FechaDeposito = itemcronograma.FechaDeposito,
                            IdFormaPago = itemcronograma.IdFormaPago,
                            IdCuenta = itemcronograma.IdCuenta,
                            FechaPagoBanco = itemcronograma.FechaPagoBanco,
                            Enviado = itemcronograma.Enviado,
                            Observaciones = itemcronograma.Observaciones,
                            IdDocumentoPago = itemcronograma.IdDocumentoPago,
                            NroDocumento = itemcronograma.NroDocumento,
                            MonedaPago = itemcronograma.MonedaPago,
                            CuotaDolares = itemcronograma.CuotaDolares,
                            FechaProcesoPago = itemcronograma.FechaProcesoPago,
                            MontoPagado = itemcronograma.MontoPagado,
                            Aprobado = true,//Se convierte en true cuando aprueba los cambios
                            FechaProcesoPagoReal = itemcronograma.FechaProcesoPagoReal,
                            FechaEfectivoDisponible=itemcronograma.FechaEfectivoDisponible,
                            FechaIngresoEnCuenta=itemcronograma.FechaIngresoEnCuenta,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = objeto.usuario,
                            UsuarioModificacion = objeto.usuario,
                            FechaCompromiso1 = itemcronograma.FechaCompromiso1,
                            FechaCompromiso2 = itemcronograma.FechaCompromiso2,
                            FechaCompromiso3 = itemcronograma.FechaCompromiso3
                        };

                        _repCronogramaPagoDetalleFinal.Insert(cuota);
                        totalmonto = totalmonto - itemcronograma.Cuota.Value;//actualizo el nuevo montototal
                    }
                    var insertado = this.InsertarPagoWebFinal(objeto.CodigoMatricula, objeto.Monto + resto, monedapagos, (float)objeto.TipoCambio, objeto.RUC, objeto.FormaPago, objeto.Documento, objeto.NroDocumento, objeto.NroCuenta, objeto.NroCheque, DateTime.Now, objeto.NroDeposito, objeto.usuario);


                    //valido si la cuota es 1-1
                    if (NroCuotaGlobal == 1 && NroSubCuotaGlobal == 1)// si esta pagando la primera cuota
                    {
                        var OriginalActualizado = this.ActualizarOriginal(versionactual, objeto.CodigoMatricula, objeto.usuario);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Guarda el apgo hecho de una cuota
        /// </summary>
        /// <param name="CodigoMatricula"></param>
        /// <param name="Monto"></param>
        /// <param name="Moneda"></param>
        /// <param name="TipoCambio"></param>
        /// <param name="RUC"></param>
        /// <param name="FormaCobro"></param>
        /// <param name="Documento"></param>
        /// <param name="SerieNumero"></param>
        /// <param name="NroCta"></param>
        /// <param name="NroRefCheq"></param>
        /// <param name="FechaDocumento"></param>
        /// <param name="NroDeposito"></param>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        private int InsertarPagoWebFinal(string CodigoMatricula, decimal Monto, string Moneda, float TipoCambio, string RUC, int FormaCobro, int Documento, string SerieNumero, int? NroCta, string NroRefCheq, DateTime FechaDocumento, string NroDeposito, string Usuario)
        {
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
            var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == CodigoMatricula).FirstOrDefault();
            //Pagos Final Pendiente Insert
            PagoFinalBO PagoFinal = new PagoFinalBO();
            PagoFinal.Id = 0;
            PagoFinal.IdMatriculaCabecera = matriculaCabeceraTemp.Id;
            PagoFinal.Monto = Monto;
            PagoFinal.Moneda = Moneda;
            PagoFinal.TipoCambio = TipoCambio;
            PagoFinal.Ruc = RUC;
            PagoFinal.IdFormaPago = FormaCobro;
            PagoFinal.SerieNumero = SerieNumero;
            PagoFinal.IdCuentaCorriente = NroCta;
            PagoFinal.NroRefCheque = NroRefCheq;
            PagoFinal.FechaDocumento = FechaDocumento;
            PagoFinal.NroDeposito = NroDeposito;
            PagoFinal.FechaPago = DateTime.Now;
            PagoFinal.IdDocumentoPago = Documento;
            PagoFinal.EstadoPago = true;

            PagoFinal.Estado = true;
            PagoFinal.FechaCreacion = DateTime.Now;
            PagoFinal.FechaModificacion = DateTime.Now;
            PagoFinal.UsuarioCreacion = Usuario;
            PagoFinal.UsuarioModificacion = Usuario;

            PagoFinalRepositorio _repPagoFinal = new PagoFinalRepositorio();
            _repPagoFinal.Insert(PagoFinal);

            //Actualiza Estado Matricula

            matriculaCabeceraTemp.EstadoMatricula = "matriculado";
            matriculaCabeceraTemp.Estado = true;
            matriculaCabeceraTemp.FechaModificacion = DateTime.Now;
            matriculaCabeceraTemp.UsuarioModificacion = Usuario;
            //Actualiza la fecha de matricula
            CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
            int empresaPaga, primerPago;


            primerPago = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Estado == true && w.Cancelado == true && w.Aprobado == true).Count();
            empresaPaga = _repMatriculaCabecera.GetBy(w => w.Id == matriculaCabeceraTemp.Id && w.Estado == true && w.EmpresaPaga == "SI").Count();
            if (primerPago == 0 && empresaPaga == 0)
            {
                matriculaCabeceraTemp.FechaMatricula = FechaDocumento;
            }
            _repMatriculaCabecera.Update(matriculaCabeceraTemp);
            return 1;
        }
        private int ActualizarOriginal(int Version, string CodigoMatricula, string Usuario)
        {
            CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
            var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == CodigoMatricula).FirstOrDefault();
            //Primero Eliminadmos el Actual (Original)
            CronogramaPagoDetalleOriginalRepositorio _repCronogramaPagoDetalleOriginal = new CronogramaPagoDetalleOriginalRepositorio();
            _repCronogramaPagoDetalleOriginal.Delete(_repCronogramaPagoDetalleOriginal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id), Usuario);

            //Ahora Insertamos el nuevo original del actual final
            var listaActual = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == Version).ToList();
            List<CronogramaPagoDetalleOriginalBO> listaNuevosOriginales = new List<CronogramaPagoDetalleOriginalBO>();
            foreach (var item in listaActual)
            {
                CronogramaPagoDetalleOriginalBO nuevoOriginal = new CronogramaPagoDetalleOriginalBO();
                nuevoOriginal.IdMatriculaCabecera = item.IdMatriculaCabecera;
                nuevoOriginal.NroCuota = item.NroCuota.Value;
                nuevoOriginal.NroSubCuota = item.NroSubCuota.Value;
                nuevoOriginal.FechaVencimiento = item.FechaVencimiento.Value;
                nuevoOriginal.TotalPagar = item.TotalPagar.Value;
                nuevoOriginal.Cuota = item.Cuota.Value;
                nuevoOriginal.Saldo = item.Saldo.Value;
                nuevoOriginal.Cancelado = item.Cancelado.Value;
                nuevoOriginal.Monto = null;
                nuevoOriginal.TipoCuota = item.TipoCuota;
                nuevoOriginal.Moneda = item.Moneda;
                nuevoOriginal.TipocCambio = item.TipoCambio;
                nuevoOriginal.FechaCreacion = DateTime.Now;
                nuevoOriginal.FechaModificacion = DateTime.Now;
                nuevoOriginal.UsuarioCreacion = Usuario; 
                nuevoOriginal.UsuarioModificacion = Usuario;
                nuevoOriginal.Estado = true;
                listaNuevosOriginales.Add(nuevoOriginal);
            }
            _repCronogramaPagoDetalleOriginal.Insert(listaNuevosOriginales);
            return 1;
        }

        public bool ValidarCuota(string codigousuario, string codigoespecial)
        {
            CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinalRep = new CronogramaPagoDetalleFinalRepositorio();
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
            var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == codigousuario, y => new { y.Id }).FirstOrDefault();
            var versionAprobada = _repCronogramaPagoDetalleFinalRep.GetBy(y => y.IdMatriculaCabecera == matriculaCabeceraTemp.Id && y.Aprobado == true, y => new { y.Version }).OrderByDescending(y => y.Version).FirstOrDefault();
            //validamos que la cuota sea la correcta, es decir, la que continúa en la lista de pendientes de pago
            var lista = new List<int>() { 1, 2, 6, 7 };
            var Cuota = _repCronogramaPagoDetalleFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == versionAprobada.Version && (w.Cancelado == false || lista.Contains(w.IdFormaPago.Value))).Select(w => w.NroCuota).Min();
            var subCuota = _repCronogramaPagoDetalleFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == versionAprobada.Version && w.NroCuota == Cuota && (w.Cancelado == false || lista.Contains(w.IdFormaPago.Value))).Select(w => w.NroSubCuota).Min();
            string CuotaPad = Cuota.ToString().Length > 1 ? Cuota.ToString().Substring(Cuota.ToString().Length - 2, 2) : Cuota.ToString();
            string SubCuotaPad = subCuota.ToString().Length > 1 ? subCuota.ToString().Substring(subCuota.ToString().Length - 2, 2) : subCuota.ToString();
            string CuotaValidada = CuotaPad.PadLeft(2, '0') + SubCuotaPad.PadLeft(2, '0');
            Int16 cuotasgte = Convert.ToInt16(CuotaValidada);
            string CuotaPagada = String.Empty;
            if (codigoespecial.Substring(0, 2) == "1C") //archivo antiguo
            {
                CuotaPagada = codigoespecial.Substring(2, 2) + "01";
            }
            else
            {
                CuotaPagada = codigoespecial.Substring(1, 4);
            }

            if (Convert.ToInt16(CuotaPagada) == cuotasgte)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidarMonto(string codigousuario, string NroCuota, string NroSubCuota, double CuotaPagada, string ValAnt)
        {
            //validamos que la cuota sea la correcta, es decir, la que continúa en la lista de pendientes de pago
            CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinalRep = new CronogramaPagoDetalleFinalRepositorio();
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
            var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == codigousuario, y => new { y.Id }).FirstOrDefault();
            var versionAprobada = _repCronogramaPagoDetalleFinalRep.GetBy(y => y.IdMatriculaCabecera == matriculaCabeceraTemp.Id && y.Aprobado == true, y => new { y.Version }).OrderByDescending(y => y.Version).FirstOrDefault();
            //validamos que la cuota sea la correcta, es decir, la que continúa en la lista de pendientes de pago                     

            double CuotaOriginal = 0.00;
            if (ValAnt.Substring(0, 2) == "1C") //archivo antiguo
            {
                CuotaOriginal = Convert.ToDouble(_repCronogramaPagoDetalleFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == versionAprobada.Version && w.NroCuota == int.Parse(ValAnt.Substring(2, 2)) && w.NroSubCuota == 1).Select(x => new { Cuota = (Math.Round(x.Cuota.Value, 2) + Math.Round(x.Mora == null ? 0 : x.Mora.Value, 2)) }).Select(w => w.Cuota).FirstOrDefault());
            }
            else
            {
                CuotaOriginal = Convert.ToDouble(_repCronogramaPagoDetalleFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == versionAprobada.Version && w.NroCuota == int.Parse(NroCuota) && w.NroSubCuota == int.Parse(NroSubCuota)).Select(x => new { Cuota = (Math.Round(x.Cuota.Value, 2) + Math.Round(x.Mora == null ? 0 : x.Mora.Value, 2)) }).Select(w => w.Cuota).FirstOrDefault());
            }

            if (CuotaPagada == CuotaOriginal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene el Reporte de Pendientes
        /// </summary>
        /// <returns></returns>
        public ReportePendienteGeneralDTO GenerarReportePendienteOperacionesGeneral(ReportePendienteFiltroDTO FiltroPendiente)
        {

            ReportesRepositorio reporteRepositorio = new ReportesRepositorio();
            var entities = reporteRepositorio.ObtenerReportePendientePeriodoyCoordinador(FiltroPendiente).ToList();
            var cambios = reporteRepositorio.ObtenerReportePendienteCambiosPorCoordinador(FiltroPendiente).ToList();
            var modificaciones = reporteRepositorio.ObtenerReportePendienteDiferencias(FiltroPendiente).ToList();

            ReportePendienteGeneralDTO general = new ReportePendienteGeneralDTO();
            general.Periodo = entities;
            general.Cambios = cambios;
            general.Diferencias = modificaciones;
            return general;

        }
        /// <summary>
        /// Obtiene el Reporte de Pendientes
        /// </summary>
        /// <returns></returns>
        public List<ReportePendienteDetalles> GenerarReportePendienteOperacionesDetalles(ReportePendienteFiltroDTO FiltroPendiente)
        {

            ReportesRepositorio reporteRepositorio = new ReportesRepositorio();
            return reporteRepositorio.ObtenerReportePendienteDetalles(FiltroPendiente);

        }
        public IList<ReportePendienteDetalleFinalDTO> GenerarReportePendientePorPeriodoOperaciones(ReportePendienteGeneralDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.Periodo
                                   group r by new { r.PeriodoPorFechaVencimiento} into grupo
                                   select new ReportePendientePeriodoDTO {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       Diferencia = grupo.Select(x => x.Diferencia).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                       DiferenciaRetirosCD = grupo.Select(x => x.DiferenciaRetirosCD).Sum(),
                                       DiferenciaRetirosSD = grupo.Select(x => x.DiferenciaRetirosSD).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       IngresoVentas = grupo.Select(x => x.IngresoVentas).Sum(),
                                       MontoRecuperadoMes = grupo.Select(x => x.MontoRecuperadoMes).Sum(),
                                       PagosAdelantadoAcumulado = grupo.Select(x => x.PagosAdelantadoAcumulado).Sum(),
                                       PendientePorFactura = grupo.Select(x => x.PendientePorFactura).Sum(),
                                       PendienteSinFactura = grupo.Select(x => x.PendienteSinFactura).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = respuestaGeneral.Cambios;
            var modificaciones  = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReportePendientesDiferenciasDTO temp = new ReportePendientesDiferenciasDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReportePendienteDetalleDTO> detalles = new List<ReportePendienteDetalleDTO>();

            ReportePendienteDetalleDTO detalle1 = new ReportePendienteDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReportePendienteDetalleDTO detalle2 = new ReportePendienteDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReportePendienteDetalleDTO detalle3 = new ReportePendienteDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReportePendienteDetalleDTO detalle4 = new ReportePendienteDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReportePendienteDetalleDTO detalle5 = new ReportePendienteDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle5);
            //////////////////////////////////inicio
            ReportePendienteDetalleDTO detalle6 = new ReportePendienteDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReportePendienteDetalleDTO detalle7 = new ReportePendienteDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle7);
            ///////////////////////////////////fin
            ReportePendienteDetalleDTO detalle8 = new ReportePendienteDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReportePendienteDetalleDTO detalle12 = new ReportePendienteDetalleDTO();
            detalle12.Tipo = "Ingreso Ventas($)";
            detalle12.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle12);

            ReportePendienteDetalleDTO detalle13 = new ReportePendienteDetalleDTO();
            detalle13.Tipo = "Proyectado Inicial menos Ventas($)";
            detalle13.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle13);

            ReportePendienteDetalleDTO detalle18 = new ReportePendienteDetalleDTO();
            detalle18.Tipo = "Proyectado Actual menos Ventas($)";
            detalle18.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle18);

            ReportePendienteDetalleDTO detalle9 = new ReportePendienteDetalleDTO();
            detalle9.Tipo = "Monto Pagado menos Ventas($)";
            detalle9.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReportePendienteDetalleDTO detalle14 = new ReportePendienteDetalleDTO();
            detalle14.Tipo = "Recuperacion en el Mes($)";
            detalle14.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle14);

            ReportePendienteDetalleDTO detalle19 = new ReportePendienteDetalleDTO();
            detalle19.Tipo = "Pagos Adelantados($)";
            detalle19.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle19);

            ReportePendienteDetalleDTO detalle15 = new ReportePendienteDetalleDTO();
            detalle15.Tipo = "Pendiente($)";
            detalle15.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle15);

            ReportePendienteDetalleDTO detalle16 = new ReportePendienteDetalleDTO();
            detalle16.Tipo = "Pendiente por Factura($)";
            detalle16.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle16);

            ReportePendienteDetalleDTO detalle17 = new ReportePendienteDetalleDTO();
            detalle17.Tipo = "Pendiente sin Factura($)";
            detalle17.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle17);

            ReportePendienteDetalleDTO detalle10 = new ReportePendienteDetalleDTO();
            detalle10.Tipo = "% Cobrado/Inicial";
            detalle10.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle10);

            ReportePendienteDetalleDTO detalle11 = new ReportePendienteDetalleDTO();
            detalle11.Tipo = "% Cobrado/Actual";
            detalle11.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle11);


            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReportePendienteDetallesMesesDTO detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReportePendienteDetallesMesesDTO detallemes2 = new ReportePendienteDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReportePendienteDetallesMesesDTO detallemes3 = new ReportePendienteDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReportePendienteDetallesMesesDTO detallemes4 = new ReportePendienteDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReportePendienteDetallesMesesDTO detallemes5 = new ReportePendienteDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                /////////////////////inicio

                //Retiros Con Devolucion
                ReportePendienteDetallesMesesDTO detallemes6 = new ReportePendienteDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.DiferenciaRetirosCD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReportePendienteDetallesMesesDTO detallemes7 = new ReportePendienteDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.DiferenciaRetirosSD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);


                /////////////////////fin

                //Actual
                ReportePendienteDetallesMesesDTO detallemes8 = new ReportePendienteDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);


                //Ingreso Ventas
                ReportePendienteDetallesMesesDTO detallemes12 = new ReportePendienteDetallesMesesDTO();
                detallemes12.Mes = item.PeriodoPorFechaVencimiento;
                detallemes12.Monto = item.IngresoVentas.ToString();
                detalles.Where(w => w.Tipo == "Ingreso Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                //Proyectado Inicial menos Ventas
                ReportePendienteDetallesMesesDTO detallemes13 = new ReportePendienteDetallesMesesDTO();
                detallemes13.Mes = item.PeriodoPorFechaVencimiento;
                detallemes13.Monto = (item.Proyectado - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes13);

                //Proyectado Actual menos Ventas
                ReportePendienteDetallesMesesDTO detallemes18 = new ReportePendienteDetallesMesesDTO();
                detallemes18.Mes = item.PeriodoPorFechaVencimiento;
                detallemes18.Monto = (item.Actual - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes18);

                //MontoPagado
                ReportePendienteDetallesMesesDTO detallemes9 = new ReportePendienteDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = (item.MontoPagado-item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Monto Pagado menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Recuperacion en el Mes
                ReportePendienteDetallesMesesDTO detallemes14 = new ReportePendienteDetallesMesesDTO();
                detallemes14.Mes = item.PeriodoPorFechaVencimiento;
                detallemes14.Monto = item.MontoRecuperadoMes.ToString();
                detalles.Where(w => w.Tipo == "Recuperacion en el Mes($)").FirstOrDefault().ListaMontosMeses.Add(detallemes14);

                //Pagos Adelantados
                ReportePendienteDetallesMesesDTO detallemes19 = new ReportePendienteDetallesMesesDTO();
                detallemes19.Mes = item.PeriodoPorFechaVencimiento;
                detallemes19.Monto = item.PagosAdelantadoAcumulado.ToString();
                detalles.Where(w => w.Tipo == "Pagos Adelantados($)").FirstOrDefault().ListaMontosMeses.Add(detallemes19);

                //Pendiente
                ReportePendienteDetallesMesesDTO detallemes15 = new ReportePendienteDetallesMesesDTO();
                detallemes15.Mes = item.PeriodoPorFechaVencimiento;
                detallemes15.Monto = ((item.Actual - item.IngresoVentas) - (item.MontoPagado - item.IngresoVentas)).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes15);

                //Pendiente por factura
                ReportePendienteDetallesMesesDTO detallemes16 = new ReportePendienteDetallesMesesDTO();
                detallemes16.Mes = item.PeriodoPorFechaVencimiento;
                detallemes16.Monto = item.PendientePorFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente por Factura($)").FirstOrDefault().ListaMontosMeses.Add(detallemes16);

                //Pendiente sin factura
                ReportePendienteDetallesMesesDTO detallemes17 = new ReportePendienteDetallesMesesDTO();
                detallemes17.Mes = item.PeriodoPorFechaVencimiento;
                detallemes17.Monto = item.PendienteSinFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente sin Factura($)").FirstOrDefault().ListaMontosMeses.Add(detallemes17);

                //%CobradoInicial
                ReportePendienteDetallesMesesDTO detallemes10 = new ReportePendienteDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Proyectado - item.IngresoVentas == 0)
                {
                    detallemes10.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes10.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / (item.Proyectado - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Inicial").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                //%CobradoActual
                ReportePendienteDetallesMesesDTO detallemes11 = new ReportePendienteDetallesMesesDTO();
                detallemes11.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Actual - item.IngresoVentas == 0)
                {
                    detallemes11.Monto = "% " + (0.00m * 100).ToString("0.00");
                    
                }
                else
                {
                    detallemes11.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / (item.Actual - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Actual").FirstOrDefault().ListaMontosMeses.Add(detallemes11);

            }
            List<ReportePendienteDetalleFinalDTO> finales = new List<ReportePendienteDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReportePendienteDetalleFinalDTO item = new ReportePendienteDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }

        public IList<ReportePendienteDetalleFinalDTO> GenerarReportePendienteIngresoVentasPorPeriodoOperacionesAnterior(ReportePendienteGeneralDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.Periodo
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReportePendientePeriodoTotalizadoDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       IngresoVentas = grupo.Select(x => x.MatriculadosFechaPago).Sum(),
                                       PagoEnFechaVenc = grupo.Select(x => x.MatriculadosFechaVencimiento).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReportePendienteDetalleDTO> detalles = new List<ReportePendienteDetalleDTO>();

            ReportePendienteDetalleDTO detalle1 = new ReportePendienteDetalleDTO();
            detalle1.Tipo = "Ingreso Matriculas según Fecha de Cronograma($)";
            detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReportePendienteDetalleDTO detalle2 = new ReportePendienteDetalleDTO();
            detalle2.Tipo = "Ingreso según Fecha de Pago($)";
            detalle2.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle2);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReportePendienteDetallesMesesDTO detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.PagoEnFechaVenc.ToString();
                detalles.Where(w => w.Tipo == "Ingreso Matriculas según Fecha de Cronograma($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReportePendienteDetallesMesesDTO detallemes2 = new ReportePendienteDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.IngresoVentas.ToString();
                detalles.Where(w => w.Tipo == "Ingreso según Fecha de Pago($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

            }
            List<ReportePendienteDetalleFinalDTO> finales = new List<ReportePendienteDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReportePendienteDetalleFinalDTO item = new ReportePendienteDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 25/01/2021
        /// <summary>
        /// Obtiene los ingresos segun fecha y cronograma de matricula
        /// </summary>
        /// <returns>ReportePendienteDetalleFinalDTO</returns>
        public IList<ReportePendienteDetalleFinalDTO> GenerarReportePendienteIngresoVentasPorPeriodoOperaciones(ReportePendienteGeneralPeriodoDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.Matriculados
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReportePendientePeriodoTotalizadoDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       IngresoVentas = grupo.Select(x => x.MatriculadosFechaPago).Sum(),
                                       PagoEnFechaVenc = grupo.Select(x => x.MatriculadosFechaVencimiento).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
           
            /*CONVIERTO EL FORMATO A EL FORMATO NECESITADO*/

            List<ReportePendienteDetalleDTO> detalles = new List<ReportePendienteDetalleDTO>();

            ReportePendienteDetalleDTO detalle1 = new ReportePendienteDetalleDTO();
            detalle1.Tipo = "Ingreso Matriculas según Fecha de Cronograma($)";
            detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReportePendienteDetalleDTO detalle2 = new ReportePendienteDetalleDTO();
            detalle2.Tipo = "Ingreso según Fecha de Pago($)";
            detalle2.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle2);
            ReportePendienteDetallesMesesDTO detallemes1;
            ReportePendienteDetallesMesesDTO detallemes2;
            foreach (var items in entities)
            {
                //Proyectado Inicial
                detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = items.PeriodoPorFechaVencimiento;
                detallemes1.Monto = items.PagoEnFechaVenc.ToString();
                detalles.Where(w => w.Tipo == "Ingreso Matriculas según Fecha de Cronograma($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                detallemes2 = new ReportePendienteDetallesMesesDTO();
                detallemes2.Mes = items.PeriodoPorFechaVencimiento;
                detallemes2.Monto = items.IngresoVentas.ToString();
                detalles.Where(w => w.Tipo == "Ingreso según Fecha de Pago($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);
                
            }
            List<ReportePendienteDetalleFinalDTO> finales = new List<ReportePendienteDetalleFinalDTO>();
            ReportePendienteDetalleFinalDTO item;
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    item = new ReportePendienteDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReportePendienteDetalleFinalDTO> GenerarReportePendientePorCoordinadoraOperaciones(ReportePendienteGeneralDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.Periodo
                                   group r by new { r.Coordinador } into grupo
                                   select new ReportePendientePeriodoPorCoordinadorDTO
                                   {
                                       Coordinador = grupo.Key.Coordinador,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       Diferencia = grupo.Select(x => x.Diferencia).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                       DiferenciaRetirosCD = grupo.Select(x => x.DiferenciaRetirosCD).Sum(),
                                       DiferenciaRetirosSD = grupo.Select(x => x.DiferenciaRetirosSD).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       IngresoVentas = grupo.Select(x => x.IngresoVentas).Sum(),
                                       PendientePorFactura = grupo.Select(x => x.PendientePorFactura).Sum(),
                                       PendienteSinFactura = grupo.Select(x => x.PendienteSinFactura).Sum(),
                                       MontoVencido = grupo.Select(x => x.MontoVencido).Sum(),
                                       MontoPorVencer = grupo.Select(x => x.MontoPorVencer).Sum(),
                                       PagoPrevio = grupo.Select(x => x.PagoPrevio).Sum(),
                                       PagoDentroMes = grupo.Select(x => x.PagoDentroMes).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.Coordinador);
            var cambios = respuestaGeneral.Cambios;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReportePendientesDiferenciasDTO temp = new ReportePendientesDiferenciasDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {
                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReportePendienteDetalleDTO> detalles = new List<ReportePendienteDetalleDTO>();

            ReportePendienteDetalleDTO detalle1 = new ReportePendienteDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReportePendienteDetalleDTO detalle2 = new ReportePendienteDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReportePendienteDetalleDTO detalle3 = new ReportePendienteDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReportePendienteDetalleDTO detalle4 = new ReportePendienteDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReportePendienteDetalleDTO detalle5 = new ReportePendienteDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle5);
            //////////////////////////////////inicio
            ReportePendienteDetalleDTO detalle6 = new ReportePendienteDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReportePendienteDetalleDTO detalle7 = new ReportePendienteDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle7);
            ///////////////////////////////////fin

            ReportePendienteDetalleDTO detalle22 = new ReportePendienteDetalleDTO();
            detalle22.Tipo = "Proyectado Actual($)";
            detalle22.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle22);

            ReportePendienteDetalleDTO detalle21 = new ReportePendienteDetalleDTO();
            detalle21.Tipo = "Proyectado Inicial menos Ventas($)";
            detalle21.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle21);

            ReportePendienteDetalleDTO detalle19 = new ReportePendienteDetalleDTO();
            detalle19.Tipo = "Proyectado Actual menos Ventas($)";
            detalle19.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle19);

            ReportePendienteDetalleDTO detalle8 = new ReportePendienteDetalleDTO();
            detalle8.Tipo = "Vencido($)";
            detalle8.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReportePendienteDetalleDTO detalle12 = new ReportePendienteDetalleDTO();
            detalle12.Tipo = "Por Vencer($)";
            detalle12.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle12);

            ReportePendienteDetalleDTO detalle9 = new ReportePendienteDetalleDTO();
            detalle9.Tipo = "Monto Pagado($)";
            detalle9.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReportePendienteDetalleDTO detalle14 = new ReportePendienteDetalleDTO();
            detalle14.Tipo = "Real Ingreso Previo($)";
            detalle14.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle14);

            ReportePendienteDetalleDTO detalle18 = new ReportePendienteDetalleDTO();
            detalle18.Tipo = "Real Ingreso($)";
            detalle18.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle18);

            ReportePendienteDetalleDTO detalle15 = new ReportePendienteDetalleDTO();
            detalle15.Tipo = "Pendiente($)";
            detalle15.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle15);

            ReportePendienteDetalleDTO detalle16 = new ReportePendienteDetalleDTO();
            detalle16.Tipo = "Pendiente por Factura($)";
            detalle16.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle16);

            ReportePendienteDetalleDTO detalle17 = new ReportePendienteDetalleDTO();
            detalle17.Tipo = "Pendiente sin Factura($)";
            detalle17.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle17);

            ReportePendienteDetalleDTO detalle10 = new ReportePendienteDetalleDTO();
            detalle10.Tipo = "% Cobrado/Inicial";
            detalle10.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle10);

            ReportePendienteDetalleDTO detalle20 = new ReportePendienteDetalleDTO();
            detalle20.Tipo = "% Cobrado/Actual";
            detalle20.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle20);

            ReportePendienteDetalleDTO detalle11 = new ReportePendienteDetalleDTO();
            detalle11.Tipo = "% Cobrado/Vencido";
            detalle11.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle11);


            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReportePendienteDetallesMesesDTO detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = item.Coordinador;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReportePendienteDetallesMesesDTO detallemes2 = new ReportePendienteDetallesMesesDTO();
                detallemes2.Mes = item.Coordinador;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReportePendienteDetallesMesesDTO detallemes3 = new ReportePendienteDetallesMesesDTO();
                detallemes3.Mes = item.Coordinador;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReportePendienteDetallesMesesDTO detallemes4 = new ReportePendienteDetallesMesesDTO();
                detallemes4.Mes = item.Coordinador;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReportePendienteDetallesMesesDTO detallemes5 = new ReportePendienteDetallesMesesDTO();
                detallemes5.Mes = item.Coordinador;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                /////////////////////inicio

                //Retiros Con Devolucion
                ReportePendienteDetallesMesesDTO detallemes6 = new ReportePendienteDetallesMesesDTO();
                detallemes6.Mes = item.Coordinador;
                detallemes6.Monto = (item.DiferenciaRetirosCD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReportePendienteDetallesMesesDTO detallemes7 = new ReportePendienteDetallesMesesDTO();
                detallemes7.Mes = item.Coordinador;
                detallemes7.Monto = (item.DiferenciaRetirosSD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Actual
                ReportePendienteDetallesMesesDTO detallemes22 = new ReportePendienteDetallesMesesDTO();
                detallemes22.Mes = item.Coordinador;
                detallemes22.Monto = (item.Actual).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes22);

                /////////////////////fin
                //Proyectado con Cambios Inicial
                ReportePendienteDetallesMesesDTO detallemes21 = new ReportePendienteDetallesMesesDTO();
                detallemes21.Mes = item.Coordinador;
                detallemes21.Monto = (item.Proyectado - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes21);

                //Proyectado con Cambios Actual
                ReportePendienteDetallesMesesDTO detallemes19 = new ReportePendienteDetallesMesesDTO();
                detallemes19.Mes = item.Coordinador;
                detallemes19.Monto = (item.Actual - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes19);

                //Vencido
                ReportePendienteDetallesMesesDTO detallemes8 = new ReportePendienteDetallesMesesDTO();
                detallemes8.Mes = item.Coordinador;
                detallemes8.Monto = item.MontoVencido.ToString();
                detalles.Where(w => w.Tipo == "Vencido($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Por Vencer
                ReportePendienteDetallesMesesDTO detallemes12 = new ReportePendienteDetallesMesesDTO();
                detallemes12.Mes = item.Coordinador;
                detallemes12.Monto = item.MontoPorVencer.ToString();
                detalles.Where(w => w.Tipo == "Por Vencer($)").FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                //MontoPagado
                ReportePendienteDetallesMesesDTO detallemes9 = new ReportePendienteDetallesMesesDTO();
                detallemes9.Mes = item.Coordinador;
                detallemes9.Monto = (item.MontoPagado - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Monto Pagado($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Real Ingreso Previo
                ReportePendienteDetallesMesesDTO detallemes14 = new ReportePendienteDetallesMesesDTO();
                detallemes14.Mes = item.Coordinador;
                detallemes14.Monto = item.PagoPrevio.ToString();
                detalles.Where(w => w.Tipo == "Real Ingreso Previo($)").FirstOrDefault().ListaMontosMeses.Add(detallemes14);


                //Real Ingreso
                ReportePendienteDetallesMesesDTO detallemes18 = new ReportePendienteDetallesMesesDTO();
                detallemes18.Mes = item.Coordinador;
                detallemes18.Monto = item.PagoDentroMes.ToString();
                detalles.Where(w => w.Tipo == "Real Ingreso($)").FirstOrDefault().ListaMontosMeses.Add(detallemes18);

                //Pendiente
                ReportePendienteDetallesMesesDTO detallemes15 = new ReportePendienteDetallesMesesDTO();
                detallemes15.Mes = item.Coordinador;
                detallemes15.Monto = ((item.Actual - item.IngresoVentas) - (item.MontoPagado - item.IngresoVentas)).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes15);

                //Pendiente por factura
                ReportePendienteDetallesMesesDTO detallemes16 = new ReportePendienteDetallesMesesDTO();
                detallemes16.Mes = item.Coordinador;
                detallemes16.Monto = item.PendientePorFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente por Factura($)").FirstOrDefault().ListaMontosMeses.Add(detallemes16);

                //Pendiente sin factura
                ReportePendienteDetallesMesesDTO detallemes17 = new ReportePendienteDetallesMesesDTO();
                detallemes17.Mes = item.Coordinador;
                detallemes17.Monto = item.PendienteSinFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente sin Factura($)").FirstOrDefault().ListaMontosMeses.Add(detallemes17);

                //%CobradoInicial
                ReportePendienteDetallesMesesDTO detallemes10 = new ReportePendienteDetallesMesesDTO();
                detallemes10.Mes = item.Coordinador;
                if (item.Proyectado - item.IngresoVentas == 0)
                {
                    detallemes10.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes10.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / (item.Proyectado - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Inicial").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                //%CobradoActual
                ReportePendienteDetallesMesesDTO detallemes20 = new ReportePendienteDetallesMesesDTO();
                detallemes20.Mes = item.Coordinador;
                if (item.Actual - item.IngresoVentas == 0)
                {
                    detallemes20.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes20.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / (item.Actual - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Actual").FirstOrDefault().ListaMontosMeses.Add(detallemes20);

                //%CobradoVencido
                ReportePendienteDetallesMesesDTO detallemes11 = new ReportePendienteDetallesMesesDTO();
                detallemes11.Mes = item.Coordinador;
                if (item.MontoVencido == 0.00m)
                {
                    detallemes11.Monto = "% " + (0.00m * 100).ToString("0.00");
                }
                else
                {
                    detallemes11.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / item.MontoVencido) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Vencido").FirstOrDefault().ListaMontosMeses.Add(detallemes11);

            }
            List<ReportePendienteDetalleFinalDTO> finales = new List<ReportePendienteDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReportePendienteDetalleFinalDTO item = new ReportePendienteDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReportePendienteDetalleFinalPorCoordinadorDTO> GenerarReportePendientePeriodoyCoordinadorOperaciones(ReportePendienteGeneralDTO respuestaGeneral)
        {

            var entities = respuestaGeneral.Periodo.OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = respuestaGeneral.Cambios;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReportePendientesDiferenciasDTO temp = new ReportePendientesDiferenciasDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {
                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReportePendienteDetallePorCoordinadorDTO> detalles = new List<ReportePendienteDetallePorCoordinadorDTO>();
            var listaCoordinadoras = entities.Select(x => x.Coordinador).Distinct();
            foreach (var item in listaCoordinadoras)
            {

                ReportePendienteDetallePorCoordinadorDTO detalle1 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle1.Tipo = "Proyectado Inicial($)";
                detalle1.Coordinador = item;
                detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle1);

                ReportePendienteDetallePorCoordinadorDTO detalle2 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle2.Tipo = "Ajuste Cambio Fecha($)";
                detalle2.Coordinador = item;
                detalle2.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle2);

                ReportePendienteDetallePorCoordinadorDTO detalle3 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle3.Tipo = "Ajuste Cambio Monto($)";
                detalle3.Coordinador = item;
                detalle3.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle3);

                ReportePendienteDetallePorCoordinadorDTO detalle4 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
                detalle4.Coordinador = item;
                detalle4.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle4);

                ReportePendienteDetallePorCoordinadorDTO detalle5 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
                detalle5.Coordinador = item;
                detalle5.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle5);
                //////////////////////////////////inicio
                ReportePendienteDetallePorCoordinadorDTO detalle6 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle6.Tipo = "Retiros Con Devolucion($)";
                detalle6.Coordinador = item;
                detalle6.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle6);

                ReportePendienteDetallePorCoordinadorDTO detalle7 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle7.Tipo = "Retiros Sin Devolucion($)";
                detalle7.Coordinador = item;
                detalle7.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle7);
                ///////////////////////////////////fin
                ReportePendienteDetallePorCoordinadorDTO detalle8 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle8.Tipo = "Proyectado Actual($)";
                detalle8.Coordinador = item;
                detalle8.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle8);

                ReportePendienteDetallePorCoordinadorDTO detalle12 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle12.Tipo = "Ingreso Ventas($)";
                detalle12.Coordinador = item;
                detalle12.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle12);

                ReportePendienteDetallePorCoordinadorDTO detalle13 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle13.Tipo = "Proyectado Inicial menos Ventas($)";
                detalle13.Coordinador = item;
                detalle13.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle13);

                ReportePendienteDetallePorCoordinadorDTO detalle18 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle18.Tipo = "Proyectado Actual menos Ventas($)";
                detalle18.Coordinador = item;
                detalle18.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle18);

                ReportePendienteDetallePorCoordinadorDTO detalle9 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle9.Tipo = "Monto Pagado menos Ventas($)";
                detalle9.Coordinador = item;
                detalle9.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle9);

                ReportePendienteDetallePorCoordinadorDTO detalle14 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle14.Tipo = "Recuperacion en el Mes($)";
                detalle14.Coordinador = item;
                detalle14.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle14);

                ReportePendienteDetallePorCoordinadorDTO detalle19 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle19.Tipo = "Pagos Adelantados($)";
                detalle19.Coordinador = item;
                detalle19.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle19);

                ReportePendienteDetallePorCoordinadorDTO detalle15 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle15.Tipo = "Pendiente($)";
                detalle15.Coordinador = item;
                detalle15.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle15);

                ReportePendienteDetallePorCoordinadorDTO detalle16 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle16.Tipo = "Pendiente por Factura($)";
                detalle16.Coordinador = item;
                detalle16.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle16);

                ReportePendienteDetallePorCoordinadorDTO detalle17 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle17.Tipo = "Pendiente sin Factura($)";
                detalle17.Coordinador = item;
                detalle17.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle17);

                ReportePendienteDetallePorCoordinadorDTO detalle10 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle10.Tipo = "% Cobrado/Inicial";
                detalle10.Coordinador = item;
                detalle10.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle10);

                ReportePendienteDetallePorCoordinadorDTO detalle11 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle11.Tipo = "% Cobrado/Actual";
                detalle11.Coordinador = item;
                detalle11.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle11);
            }

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReportePendienteDetallesMesesDTO detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReportePendienteDetallesMesesDTO detallemes2 = new ReportePendienteDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReportePendienteDetallesMesesDTO detallemes3 = new ReportePendienteDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReportePendienteDetallesMesesDTO detallemes4 = new ReportePendienteDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReportePendienteDetallesMesesDTO detallemes5 = new ReportePendienteDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                /////////////////////inicio

                //Retiros Con Devolucion
                ReportePendienteDetallesMesesDTO detallemes6 = new ReportePendienteDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.DiferenciaRetirosCD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReportePendienteDetallesMesesDTO detallemes7 = new ReportePendienteDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.DiferenciaRetirosSD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes7);


                /////////////////////fin

                //Actual
                ReportePendienteDetallesMesesDTO detallemes8 = new ReportePendienteDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes8);


                //Ingreso Ventas
                ReportePendienteDetallesMesesDTO detallemes12 = new ReportePendienteDetallesMesesDTO();
                detallemes12.Mes = item.PeriodoPorFechaVencimiento;
                detallemes12.Monto = item.IngresoVentas.ToString();
                detalles.Where(w => w.Tipo == "Ingreso Ventas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                //Proyectado Inicial menos Ventas
                ReportePendienteDetallesMesesDTO detallemes13 = new ReportePendienteDetallesMesesDTO();
                detallemes13.Mes = item.PeriodoPorFechaVencimiento;
                detallemes13.Monto = (item.Proyectado - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial menos Ventas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes13);

                //Proyectado Actual menos Ventas
                ReportePendienteDetallesMesesDTO detallemes18 = new ReportePendienteDetallesMesesDTO();
                detallemes18.Mes = item.PeriodoPorFechaVencimiento;
                detallemes18.Monto = (item.Actual - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual menos Ventas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes18);

                //MontoPagado
                ReportePendienteDetallesMesesDTO detallemes9 = new ReportePendienteDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = (item.MontoPagado - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Monto Pagado menos Ventas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Recuperacion en el Mes
                ReportePendienteDetallesMesesDTO detallemes14 = new ReportePendienteDetallesMesesDTO();
                detallemes14.Mes = item.PeriodoPorFechaVencimiento;
                detallemes14.Monto = item.MontoRecuperadoMes.ToString();
                detalles.Where(w => w.Tipo == "Recuperacion en el Mes($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes14);

                //Pagos Adelantados
                ReportePendienteDetallesMesesDTO detallemes19 = new ReportePendienteDetallesMesesDTO();
                detallemes19.Mes = item.PeriodoPorFechaVencimiento;
                detallemes19.Monto = item.PagosAdelantadoAcumulado.ToString();
                detalles.Where(w => w.Tipo == "Pagos Adelantados($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes19);

                //Pendiente
                ReportePendienteDetallesMesesDTO detallemes15 = new ReportePendienteDetallesMesesDTO();
                detallemes15.Mes = item.PeriodoPorFechaVencimiento;
                detallemes15.Monto = ((item.Actual - item.IngresoVentas) - (item.MontoPagado - item.IngresoVentas)).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes15);

                //Pendiente por factura
                ReportePendienteDetallesMesesDTO detallemes16 = new ReportePendienteDetallesMesesDTO();
                detallemes16.Mes = item.PeriodoPorFechaVencimiento;
                detallemes16.Monto = item.PendientePorFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente por Factura($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes16);

                //Pendiente sin factura
                ReportePendienteDetallesMesesDTO detallemes17 = new ReportePendienteDetallesMesesDTO();
                detallemes17.Mes = item.PeriodoPorFechaVencimiento;
                detallemes17.Monto = item.PendienteSinFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente sin Factura($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes17);

                //%CobradoInicial
                ReportePendienteDetallesMesesDTO detallemes10 = new ReportePendienteDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Proyectado - item.IngresoVentas == 0)
                {
                    detallemes10.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes10.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / (item.Proyectado - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Inicial" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                //%CobradoActual
                ReportePendienteDetallesMesesDTO detallemes11 = new ReportePendienteDetallesMesesDTO();
                detallemes11.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Actual - item.IngresoVentas == 0)
                {
                    detallemes11.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes11.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / (item.Actual - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Actual" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes11);

            }
            List<ReportePendienteDetalleFinalPorCoordinadorDTO> finales = new List<ReportePendienteDetalleFinalPorCoordinadorDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReportePendienteDetalleFinalPorCoordinadorDTO item = new ReportePendienteDetalleFinalPorCoordinadorDTO();
                    item.TipoMonto = det.Tipo;
                    item.Coordinador = det.Coordinador;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }



            return finales;

        }

        /// <summary>
        /// Obtiene el Reporte de Resumen de Montos
        /// </summary>
        /// <returns></returns>
        public ReporteResumenMontosGeneralTotalDTO GenerarReporteResumenMontosGeneral(ReporteResumenMontosFiltroGeneralDTO FiltroPendiente)
        {

            ReportesRepositorio reporteRepositorio = new ReportesRepositorio();
            var entities = reporteRepositorio.ObtenerReporteResumenMontos(FiltroPendiente).ToList();
            
            ReporteResumenMontosGeneralTotalDTO general = new ReporteResumenMontosGeneralTotalDTO();
            general.ResumenMontos = entities;
  
            return general;
        }
        public ReporteResumenMontosGeneralTotalDTO GenerarReporteResumenMontosCierre(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {

            ReportesRepositorio reporteRepositorio = new ReportesRepositorio();
            var cierre = reporteRepositorio.ObtenerReporteResumenMontosCierre(FiltroPendiente).ToList();

            ReporteResumenMontosGeneralTotalDTO general = new ReporteResumenMontosGeneralTotalDTO();
            general.ResumenMontosCierre = cierre;

            return general;
        }
        public ReporteResumenMontosGeneralTotalDTO GenerarReporteResumenMontosCambiosPais(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {

            ReportesRepositorio reporteRepositorio = new ReportesRepositorio();
            var cambios = reporteRepositorio.ObtenerReporteResumenMontosCambiosPorPais(FiltroPendiente).ToList();

            ReporteResumenMontosGeneralTotalDTO general = new ReporteResumenMontosGeneralTotalDTO();
            general.Cambios = cambios;
            return general;
        }
        public ReporteResumenMontosGeneralTotalDTO GenerarReporteResumenMontosDiferencias(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {

            ReportesRepositorio reporteRepositorio = new ReportesRepositorio();
            var modificaciones = reporteRepositorio.ObtenerReporteResumenMontosDiferencias(FiltroPendiente).ToList();

            ReporteResumenMontosGeneralTotalDTO general = new ReporteResumenMontosGeneralTotalDTO();
            general.Diferencias = modificaciones;
            return general;
        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoPeriodoActual(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReporteResumenMontosDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            /*var cambios = from r in respuestaGeneral.Cambios where (r.PeriodoActual != null && r.PeriodoActual.Split(' ')[1] == respuestaGeneral.Year)||
                                                                    (r.PeriodoActual == null && r.PeriodoProyectado != null && r.PeriodoProyectado.Split(' ')[1] == respuestaGeneral.Year )
                                                                    select r;*/
            var cambios = respuestaGeneral.Cambios;
            var modificaciones = respuestaGeneral.Diferencias;
            var cantidad = cambios.Count();
            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                
                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoPeriodoCierre(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {

            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontosCierre
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReporteResumenMontosCierrePeriodoDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);

            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Real($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosVariacionMensual(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneralMontos = (from r in respuestaGeneral.ResumenMontos
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReporteResumenMontosVariacionesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,                                      
                                       ActualMontos = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagadoMontos = grupo.Select(x => x.MontoPagado).Sum(),
                                       DiferenciaPorModificacion = grupo.Select(x => x.DiferenciaPorModificacion).Sum(),
                                       NuevaConsultoria = grupo.Select(x => x.NuevaConsultoria).Sum(),
                                       NuevasMatriculas = grupo.Select(x => x.NuevasMatriculas).Sum(),
                                       IngresoRealNuevasMatriculas = grupo.Select(x => x.IngresoRealNuevasMatriculas).Sum(),
                                       PendientMesOrdenServicio = grupo.Select(x => x.PendientMesOrdenServicio).Sum(),
                                       PendientMesSinOrdenServicio = grupo.Select(x => x.PendientMesSinOrdenServicio).Sum(),
                                       RetirosCD_Mes = grupo.Select(x => x.RetirosCD_Mes).Sum(),
                                       RetirosSD_Mes = grupo.Select(x => x.RetirosSD_Mes).Sum(),
                                       IncrementosDisminucionesCronograma = grupo.Select(x => x.IncrementosDisminucionesCronograma).Sum(),
                                       ModificacionInhouse = grupo.Select(x => x.ModificacionInhouse).Sum(),
                                   });

            var agrupadoGeneralCierre = (from r in respuestaGeneral.ResumenMontosCierre
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReporteResumenMontosUnionCierreDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                   });

            var entitiesCierre = agrupadoGeneralCierre.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var entitiesMontos = agrupadoGeneralMontos.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
           
            var unionMontosCierre = (from montos in agrupadoGeneralMontos
                                     join cierre in agrupadoGeneralCierre on montos.PeriodoPorFechaVencimiento equals cierre.PeriodoPorFechaVencimiento 
                                     select new ReporteResumenMontosVariacionesDTO
                                     {
                                         PeriodoPorFechaVencimiento = montos.PeriodoPorFechaVencimiento,
                                         ActualMontos = montos.ActualMontos,
                                         ActualCierre = cierre.Actual,
                                         MontoPagadoMontos = montos.MontoPagadoMontos,
                                         MontoPagadoCierre = cierre.MontoPagado,
                                         DiferenciaPorModificacion = montos.DiferenciaPorModificacion,
                                         NuevaConsultoria = montos.NuevaConsultoria,
                                         NuevasMatriculas = montos.NuevasMatriculas,
                                         IngresoRealNuevasMatriculas = montos.IngresoRealNuevasMatriculas,
                                         PendientMesOrdenServicio = montos.PendientMesOrdenServicio,
                                         PendientMesSinOrdenServicio = montos.PendientMesSinOrdenServicio,
                                         RetirosCD_Mes = montos.RetirosCD_Mes,
                                         RetirosSD_Mes = montos.RetirosSD_Mes,
                                         IncrementosDisminucionesCronograma = montos.IncrementosDisminucionesCronograma,
                                         ModificacionInhouse = montos.ModificacionInhouse,
                                     });

            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "% DIFERENCIA PROYECCION";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "% DIFERENCIA PAGO REAL";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "DIFERENCIA PROYECCION($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Diferencia por modificaciones($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Nuevos Consultorias($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Nuevas matriculas proyectado($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Nuevas matriculas ingreso real del mes($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Ordenes de servicio($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Resto de cta 1 pago periodo actual($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Devolucion de Dinero Alumnos Inscritos($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            ReporteResumenMontosDetalleDTO detalle11 = new ReporteResumenMontosDetalleDTO();
            detalle11.Tipo = "Retiro de Alumnos Inscritos Autorizados($)";
            detalle11.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle11);

            ReporteResumenMontosDetalleDTO detalle12 = new ReporteResumenMontosDetalleDTO();
            detalle12.Tipo = "Cancelación de curso($)";
            detalle12.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle12);

            ReporteResumenMontosDetalleDTO detalle13 = new ReporteResumenMontosDetalleDTO();
            detalle13.Tipo = "Incrementos y disminuciones de Cronogramas($)";
            detalle13.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle13);

            ReporteResumenMontosDetalleDTO detalle14 = new ReporteResumenMontosDetalleDTO();
            detalle14.Tipo = "Modificaciones Inhouse($)";
            detalle14.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle14);

            ReporteResumenMontosDetalleDTO detalle15 = new ReporteResumenMontosDetalleDTO();
            detalle15.Tipo = "DIFERENCIA DE PROYECCION NETA($)";
            detalle15.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle15);

            ReporteResumenMontosDetalleDTO detalle16 = new ReporteResumenMontosDetalleDTO();
            detalle16.Tipo = "DIFERENCIA PAGO REAL($)";
            detalle16.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle16);

           foreach (var item2 in unionMontosCierre)
           {
                   var Valor = 0.00m;
                   //% DIFERENCIA PROYECCION
                   ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes1.Mes = item2.PeriodoPorFechaVencimiento;
                   if (item2.ActualCierre == 0)
                   {
                       detallemes1.Monto = "% " + (0.00m * 100).ToString("0.00");

                   }
                   else
                   {
                       detallemes1.Monto = "% " + (((item2.ActualMontos / item2.ActualCierre)-1) * 100).ToString("0.00");
                   }
                   detalles.Where(w => w.Tipo == "% DIFERENCIA PROYECCION").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                   //% DIFERENCIA PAGO REAL
                   ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes2.Mes = item2.PeriodoPorFechaVencimiento;
                   if (item2.MontoPagadoCierre == 0)
                   {
                       detallemes2.Monto = "% " + (0.00m * 100).ToString("0.00");

                   }
                   else
                   {
                       detallemes2.Monto = "% " + (((item2.MontoPagadoMontos / item2.MontoPagadoCierre)-1) * 100).ToString("0.00");
                   }
                   detalles.Where(w => w.Tipo == "% DIFERENCIA PAGO REAL").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                   //DIFERENCIA PROYECCION($)
                   ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes3.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes3.Monto = (item2.ActualMontos - item2.ActualCierre).ToString();
                   detalles.Where(w => w.Tipo == "DIFERENCIA PROYECCION($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                   //Diferencia por modificaciones($)
                   ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes4.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes4.Monto = item2.DiferenciaPorModificacion.ToString();
                   detalles.Where(w => w.Tipo == "Diferencia por modificaciones($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                   //Nuevos Consultorias($)
                   ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes5.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes5.Monto = item2.NuevaConsultoria.ToString();
                   //detallemes5.Monto = item.NuevasMatriculas.ToString();
                   detalles.Where(w => w.Tipo == "Nuevos Consultorias($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                   //Nuevas matriculas proyectado($)
                   ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes6.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes6.Monto = item2.NuevasMatriculas.ToString();
                   detalles.Where(w => w.Tipo == "Nuevas matriculas proyectado($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                   //Nuevas matriculas ingreso real del mes($)
                   ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes7.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes7.Monto = item2.IngresoRealNuevasMatriculas.ToString();
                   detalles.Where(w => w.Tipo == "Nuevas matriculas ingreso real del mes($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                   //Ordenes de servicio($)
                   ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes8.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes8.Monto = item2.PendientMesOrdenServicio.ToString();
                   detalles.Where(w => w.Tipo == "Ordenes de servicio($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                   //Resto de cta 1 pago periodo actual($)
                   ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes9.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes9.Monto = item2.PendientMesSinOrdenServicio.ToString();
                   detalles.Where(w => w.Tipo == "Resto de cta 1 pago periodo actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                   //Devolucion de Dinero Alumnos Inscritos($)
                   ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes10.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes10.Monto = (item2.RetirosCD_Mes * -1).ToString();
                   detalles.Where(w => w.Tipo == "Devolucion de Dinero Alumnos Inscritos($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                   //Retiro de Alumnos Inscritos Autorizados($)
                   ReporteResumenMontosDetallesMesesDTO detallemes11 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes11.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes11.Monto = (item2.RetirosSD_Mes * -1).ToString();
                   detalles.Where(w => w.Tipo == "Retiro de Alumnos Inscritos Autorizados($)").FirstOrDefault().ListaMontosMeses.Add(detallemes11);

                   //Cancelación de curso($)
                   ReporteResumenMontosDetallesMesesDTO detallemes12 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes12.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes12.Monto = Valor.ToString();
                   detalles.Where(w => w.Tipo == "Cancelación de curso($)").FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                   //Incrementos y disminuciones de Cronogramas($)
                   ReporteResumenMontosDetallesMesesDTO detallemes13 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes13.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes13.Monto = item2.IncrementosDisminucionesCronograma.ToString();
                   detalles.Where(w => w.Tipo == "Incrementos y disminuciones de Cronogramas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes13);

                   //Modificaciones Inhouse($)
                   ReporteResumenMontosDetallesMesesDTO detallemes14 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes14.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes14.Monto = item2.ModificacionInhouse.ToString();
                   detalles.Where(w => w.Tipo == "Modificaciones Inhouse($)").FirstOrDefault().ListaMontosMeses.Add(detallemes14);

                   //DIFERENCIA DE PROYECCION NETA($)
                   ReporteResumenMontosDetallesMesesDTO detallemes15 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes15.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes15.Monto = ((item2.ActualMontos - item2.ActualCierre) - (item2.DiferenciaPorModificacion + item2.NuevaConsultoria + item2.NuevasMatriculas +
                   (item2.RetirosCD_Mes * -1) + (item2.RetirosSD_Mes * -1) + item2.IncrementosDisminucionesCronograma + item2.ModificacionInhouse)).ToString();
                   detalles.Where(w => w.Tipo == "DIFERENCIA DE PROYECCION NETA($)").FirstOrDefault().ListaMontosMeses.Add(detallemes15);

                   //%DIFERENCIA PAGO REAL($)
                   ReporteResumenMontosDetallesMesesDTO detallemes16 = new ReporteResumenMontosDetallesMesesDTO();
                   detallemes16.Mes = item2.PeriodoPorFechaVencimiento;
                   detallemes16.Monto = (item2.MontoPagadoMontos - item2.MontoPagadoCierre).ToString();
                   detalles.Where(w => w.Tipo == "DIFERENCIA PAGO REAL($)").FirstOrDefault().ListaMontosMeses.Add(detallemes16);
               
           }

            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }

        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosNuevosMatriculados(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReporteResumenMontosPagosDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       IngresoRealNuevasMatriculas = grupo.Select(x => x.IngresoRealNuevasMatriculas).Sum(),
                                       IngresoRealNuevasMatriculasFechaPago = grupo.Select(x => x.IngresoRealNuevasMatriculasFechaPago).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);

            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Ingreso matriculas según fecha de cronograma($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ingreso según fecha de pago($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            foreach (var item in entities)
            {
                //Ingreso matriculas según fecha de cronograma($)
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.IngresoRealNuevasMatriculas.ToString();
                detalles.Where(w => w.Tipo == "Ingreso matriculas según fecha de cronograma($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //Ingreso según fecha de pago($)
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.IngresoRealNuevasMatriculasFechaPago.ToString();
                detalles.Where(w => w.Tipo == "Ingreso según fecha de pago($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }

        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoPeru(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadPresencialPeru(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadOnlinePeru(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadAonlinePeru(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();

            return finales;
        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadInHousePeru(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();

            return finales;

        }

        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoColombia(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 2
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais } into grupo
                                   select new ReporteResumenMontosDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 2 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }

            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadPresencialColombia(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 2 && r.IdTipoModalidad == 0
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 2 && r.IdTipoModalidad == 0 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadOnlineColombia(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 2 && r.IdTipoModalidad == 2
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 2 && r.IdTipoModalidad == 2 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadAonlineColombia(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 2 && r.IdTipoModalidad == 1
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 2 && r.IdTipoModalidad == 1 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadInHouseColombia(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 2 && r.IdTipoModalidad == -2
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 2 && r.IdTipoModalidad == -2 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }

        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoBolivia(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 5
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais } into grupo
                                   select new ReporteResumenMontosDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 5 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan
            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadPresencialBolivia(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 5 && r.IdTipoModalidad == 0
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 5 && r.IdTipoModalidad == 0 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadOnlineBolivia(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 5 && r.IdTipoModalidad == 2
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 5 && r.IdTipoModalidad == 2 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadAonlineBolivia(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 5 && r.IdTipoModalidad == 1
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 5 && r.IdTipoModalidad == 1 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadInHouseBolivia(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 5 && r.IdTipoModalidad == -2
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 5 && r.IdTipoModalidad == -2 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }

        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoOtrosPaises(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 0
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais } into grupo
                                   select new ReporteResumenMontosDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 0 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadPresencialOtrosPaises(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 0 && r.IdTipoModalidad == 0
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 0 && r.IdTipoModalidad == 0 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadOnlineOtrosPaises(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 0 && r.IdTipoModalidad == 2
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 0 && r.IdTipoModalidad == 2 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadAonlineOtrosPaises(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 0 && r.IdTipoModalidad == 1
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 0 && r.IdTipoModalidad == 1 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }
        public IList<ReporteResumenMontosDetalleFinalDTO> GenerarReporteResumenMontosTotalizadoModalidadInHouseOtrosPaises(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTroncalPais == 0 && r.IdTipoModalidad == -2
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTroncalPais, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTroncalPais = grupo.Key.IdTroncalPais,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTroncalPais == 0 && r.IdTipoModalidad == -2 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasPorPaisesDTO temp = new ReporteResumenMontosDiferenciasPorPaisesDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTroncalPais == item.IdTroncalPais && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }

        /// <summary>
        /// Obtiene el Reporte de Pendientes
        /// </summary>
        /// <returns></returns>
        public PagosDiaPeriodoGeneralDTO GenerarReportePagosDiaPeriodoGeneral(ReportePagosDiaPeriodoFiltroDTO FiltroReportePagosDiaPeriodo)
        {

            ReportesRepositorio reporteRepositorio = new ReportesRepositorio();
            var entities = reporteRepositorio.ObtenerReportePagosDia(FiltroReportePagosDiaPeriodo).OrderBy(x => x.FechaPagoDia).ToList();
            var entitiesperiodo = reporteRepositorio.ObtenerReportePagosPeriodo(FiltroReportePagosDiaPeriodo).OrderBy(x => x.FechaPagoDia).ToList();
            //var modificaciones = reporteRepositorio.ObtenerReportePendienteDiferencias(FiltroPendiente).ToList(); 

            PagosDiaPeriodoGeneralDTO general = new PagosDiaPeriodoGeneralDTO();
            general.Periodo = entities;
            general.PeriodoMeses = entitiesperiodo;
            //general.Diferencias = modificaciones;
            return general;

        }

        /// <summary>
        /// Obtiene el Reporte de Pagos por Dia
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> GenerarReportePagosPorDia(PagosDiaPeriodoGeneralDTO respuestaGeneral)
        {

            IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> agrupado = null;

            DateTime date2 = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime date3 = new DateTime(2400, 1, 1, 0, 0, 0);

            DateTime date4 = new DateTime(1900, 12, 12, 0, 0, 0);
            DateTime date5 = new DateTime(2400, 12, 12, 0, 0, 0);

            agrupado = respuestaGeneral.Periodo.GroupBy(x => x.FechaPagoDia)
            .Select(g => new ReportePagosDiaPeriodoAgrupadoDTO
            {
                Periodo = g.Key == date2 ? "Cuotas_Adelantadas" : g.Key == date3 ? "Cuotas_Atrasadas" : g.Key.Value.ToString("yyyyMMdd"),
                DetalleFecha = g.Select(y => new ReportePagosDiaPeriodoAgrupadoDetalleFechaDTO
                {
                    FechaVencimiento = y.FechaVencimiento.ToString("yyyy-MM-dd"),
                    PeriodoFechaVencimiento = y.PeriodoFechaVencimiento,
                    Actual = y.Actual,
                    MontoPagado = y.MontoPagado,
                    MontoPendiente = y.MontoPendiente,
                    ActualConAtrasos = y.ActualConAtrasos,
                    ActualSinAtrasos = y.ActualSinAtrasos,
                    TotalPagadoDentroDelMes = y.TotalPagadoDentroDelMes

                }).ToList()
            });

            return agrupado;

        }

        /// <summary>
        /// Obtiene el Reporte de Pagos por Periodo
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> GenerarReportePagosPorPeriodo(PagosDiaPeriodoGeneralDTO respuestaGeneral)
        {

            IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> agrupado = null;

            DateTime date2 = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime date3 = new DateTime(2400, 1, 1, 0, 0, 0);

            agrupado = respuestaGeneral.PeriodoMeses.GroupBy(x => x.PeriodoFechaPagoDia)
            .Select(g => new ReportePagosDiaPeriodoAgrupadoDTO
            {
                Periodo = g.Key,
                DetalleFecha = g.Select(y => new ReportePagosDiaPeriodoAgrupadoDetalleFechaDTO
                {
                    PeriodoFechaVencimiento = y.PeriodoFechaVencimiento,
                    FechaVencimiento = y.FechaVencimiento.ToString("yyyy-MM-dd"),
                    Actual = y.Actual,
                    MontoPagado = y.MontoPagado,
                    MontoPendiente = y.MontoPendiente,
                    ActualConAtrasos = y.ActualConAtrasos,
                    ActualSinAtrasos = y.ActualSinAtrasos,
                    TotalPagadoDentroDelMes = y.TotalPagadoDentroDelMes

                }).ToList()
            });

            return agrupado;

        }     
        
    }
}
