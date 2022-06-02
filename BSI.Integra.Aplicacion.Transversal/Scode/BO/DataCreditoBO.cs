using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using BSI.Integra.Aplicacion.Servicios.BO;
using BSI.Integra.Aplicacion.Servicios.DTOs.DataCredito;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Mandrill.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoBO
    {
        private integraDBContext db;
        private DataCreditoLogRepositorio repoLog;

        private DataCreditoBusquedaRepositorio repoBusqueda;
        private DataCreditoConsultaRepositorio repoConsulta;
        private DataCreditoDataNaturalNacionalRepositorio repoNaturalNacional;
        private DataCreditoDataScoreRepositorio repoScore;
        private DataCreditoDataCuentaAhorroRepositorio repoCuentaAhorro;
        private DataCreditoDataTarjetaCreditoRepositorio repoTarjetaCredito;
        private DataCreditoDataCuentaCarteraRepositorio repoCuentaCartera;
        private DataCreditoDataEndeudamientoGlobalRepositorio repoEndeudamiento;
        private DataCreditoDataProductoValorRepositorio repoProductoValor;

        private DataCreditoDataInfAgrResumenPrincipalRepositorio repoResumenPrincipal;
        private DataCreditoDataInfAgrResumenSaldoRepositorio repoResumenSaldo;
        private DataCreditoDataInfAgrResumenSaldoSectorRepositorio repoResumenSaldoSector;
        private DataCreditoDataInfAgrResumenSaldoMesRepositorio repoResumenSaldoMes;
        private DataCreditoDataInfAgrResumenComportamientoRepositorio repoResumenCompotamiento;

        private DataCreditoDataInfAgrTotalRepositorio repoInfAgrTotal;
        private DataCreditoDataInfAgrComposicionPortafolioRepositorio repoInfAgrComposicionPortafolio;
        private DataCreditoDataInfAgrEvolucionDeudaTrimestreRepositorio repoInfAgrEvolucionDeudaTrimestre;
        private DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepositorio repoInfAgrEvolucionDeudaAnalisisPromedio;
        private DataCreditoDataInfAgrHistoricoSaldoTipoCuentaRepositorio repoInfAgrHistoricoSaldoTipoCuenta;
        private DataCreditoDataInfAgrHistoricoSaldoTotalRepositorio repoInfAgrHistoricoSaldoTotal;
        private DataCreditoDataInfAgrResumenEndeudamientoRepositorio repoInfAgrResumenEndeudamiento;

        private DataCreditoDataInfMicroPerfilGeneralRepositorio repoInfMicroPerfilGeneral;
        private DataCreditoDataInfMicroVectorSaldoMoraRepositorio repoInfMicroVerctorSaldoMora;
        private DataCreditoDataInfMicroEndeudamientoActualRepositorio repoInfMicroEndeudamientoActual;
        private DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepositorio repoInfMicroImagenTendencia;
        private DataCreditoDataInfMicroAnalisisVectorRepositorio repoInfMicroAnalisisVector;
        private DataCreditoDataInfMicroEvolucionDeudaRepositorio repoInfMicroEvolucionDeuda;

        public DataCreditoBO()
        {
            db = new integraDBContext();
            repoLog = new DataCreditoLogRepositorio(db);
            repoBusqueda = new DataCreditoBusquedaRepositorio(db);
            repoConsulta = new DataCreditoConsultaRepositorio(db);
            repoNaturalNacional = new DataCreditoDataNaturalNacionalRepositorio(db);
            repoScore = new DataCreditoDataScoreRepositorio(db);
            repoCuentaAhorro = new DataCreditoDataCuentaAhorroRepositorio(db);
            repoTarjetaCredito = new DataCreditoDataTarjetaCreditoRepositorio(db);
            repoCuentaCartera = new DataCreditoDataCuentaCarteraRepositorio(db);
            repoEndeudamiento = new DataCreditoDataEndeudamientoGlobalRepositorio(db);
            repoProductoValor = new DataCreditoDataProductoValorRepositorio(db);
            repoResumenPrincipal = new DataCreditoDataInfAgrResumenPrincipalRepositorio(db);
            repoResumenSaldo = new DataCreditoDataInfAgrResumenSaldoRepositorio(db);
            repoResumenSaldoSector = new DataCreditoDataInfAgrResumenSaldoSectorRepositorio(db);
            repoResumenSaldoMes = new DataCreditoDataInfAgrResumenSaldoMesRepositorio(db);
            repoResumenCompotamiento = new DataCreditoDataInfAgrResumenComportamientoRepositorio(db);
            repoInfAgrTotal = new DataCreditoDataInfAgrTotalRepositorio(db);
            repoInfAgrComposicionPortafolio = new DataCreditoDataInfAgrComposicionPortafolioRepositorio(db);
            repoInfAgrEvolucionDeudaTrimestre = new DataCreditoDataInfAgrEvolucionDeudaTrimestreRepositorio(db);
            repoInfAgrEvolucionDeudaAnalisisPromedio =
                new DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepositorio(db);
            repoInfAgrHistoricoSaldoTipoCuenta = new DataCreditoDataInfAgrHistoricoSaldoTipoCuentaRepositorio(db);
            repoInfAgrHistoricoSaldoTotal = new DataCreditoDataInfAgrHistoricoSaldoTotalRepositorio(db);
            repoInfAgrResumenEndeudamiento = new DataCreditoDataInfAgrResumenEndeudamientoRepositorio(db);
            repoInfMicroPerfilGeneral = new DataCreditoDataInfMicroPerfilGeneralRepositorio(db);
            repoInfMicroVerctorSaldoMora = new DataCreditoDataInfMicroVectorSaldoMoraRepositorio(db);
            repoInfMicroEndeudamientoActual = new DataCreditoDataInfMicroEndeudamientoActualRepositorio(db);
            repoInfMicroImagenTendencia = new DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepositorio(db);
            repoInfMicroAnalisisVector = new DataCreditoDataInfMicroAnalisisVectorRepositorio(db);
            repoInfMicroEvolucionDeuda = new DataCreditoDataInfMicroEvolucionDeudaRepositorio(db);
        }

        public bool ConsultarAlumnoColombia(string numeroDocumento, string primerApellido, int tipoIdentificacion, string usuario)
        {
            DataCreditoService service = new DataCreditoService();

            string respuestaServicio = service.ConsultarServicioHistoriaCreditoAlumnoColombia(numeroDocumento, primerApellido, tipoIdentificacion);

            //Respaldo de la consulta en la db
            DataCreditoLogBO bo = new DataCreditoLogBO()
            {
                NumeroDocumento = numeroDocumento,
                PrimerApellido = primerApellido,
                TipoIdentificacion = tipoIdentificacion,
                RespuestaXml = respuestaServicio,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            repoLog.Insert(bo);

            //filtra por errores
            if (respuestaServicio.Contains("\"estado\":") && respuestaServicio.Contains("\"mensaje\":"))
            {
                DataCreditoError error = JsonConvert.DeserializeObject<DataCreditoError>(respuestaServicio);
                throw new Exception(error.Mensaje);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Informes), new XmlRootAttribute("Informes"));
            StringReader stringReader = new StringReader(respuestaServicio);
            Informes informe = (Informes)serializer.Deserialize(stringReader);

            //mapeo cabecera informe
            DataCreditoBusquedaBO busqueda = new DataCreditoBusquedaBO()
            {
                FechaConsulta = DateTime.Parse(informe.Informe.FechaConsulta),
                CodigoSeguridad = informe.Informe.CodSeguridad,
                TipoIdentificacion = Convert.ToInt32(informe.Informe.IdentificacionDigitada),
                NroDocumento = informe.Informe.IdentificacionDigitada,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            repoBusqueda.Insert(busqueda);

            //añadir mapeo de errores
            if (informe.Informe.Respuesta != "13")
                throw new Exception("Valide los datos proporcinados, no se recibió información.");

            //mapeo informe
            DataCreditoDataNaturalNacionalBO naturalNacional = new DataCreditoDataNaturalNacionalBO()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                NroDocumento = informe.Informe.NaturalNacional.Identificacion.Numero,
                Nombres = informe.Informe.NaturalNacional.Nombres,
                PrimerApellido = informe.Informe.NaturalNacional.PrimerApellido,
                SegundoApellido = informe.Informe.NaturalNacional.SegundoApellido,
                NombreCompleto = informe.Informe.NaturalNacional.NombreCompleto,
                Validada = informe.Informe.NaturalNacional.Validada == "true"
                    ? true
                    : (informe.Informe.NaturalNacional.Validada == "false" ? false : (bool?)null),
                Rut = informe.Informe.NaturalNacional.Rut == "true"
                    ? true
                    : (informe.Informe.NaturalNacional.Rut == "false" ? false : (bool?)null),
                Genero = informe.Informe.NaturalNacional.Genero,
                IdentificacionEstado = informe.Informe.NaturalNacional.Identificacion.Estado,
                IdentificacionFechaExpedicion =
                    DateTime.Parse(informe.Informe.NaturalNacional.Identificacion.FechaExpedicion),
                IdentificacionCiudad = informe.Informe.NaturalNacional.Identificacion.Ciudad,
                IdentificacionDepartamento = informe.Informe.NaturalNacional.Identificacion.Departamento,
                IdentificacionNumero = informe.Informe.NaturalNacional.Identificacion.Numero,
                EdadMinima = Convert.ToInt32(informe.Informe.NaturalNacional.Edad.Min),
                EdadMaxima = Convert.ToInt32(informe.Informe.NaturalNacional.Edad.Max),

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            //repoNaturalNacional.Insert(naturalNacional);

            DataCreditoDataScoreBO score = new DataCreditoDataScoreBO()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                Tipo = informe.Informe.Score?.Tipo,
                Puntaje = informe.Informe.Score?.Puntaje,
                Fecha = informe.Informe.Score != null ? DateTime.Parse(informe.Informe.Score.Fecha) : (DateTime?)null,
                Poblacion = informe.Informe.Score?.Poblacion,

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            //repoScore.Insert(score);

            List<DataCreditoDataCuentaAhorroBO> listaCuentaAhorro = new List<DataCreditoDataCuentaAhorroBO>();
            informe.Informe.CuentaAhorro.ForEach(f =>
            {
                DataCreditoDataCuentaAhorroBO cuentaAhorro = new DataCreditoDataCuentaAhorroBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Bloqueada = f.Bloqueada == "true" ? true : (f.Bloqueada == "false" ? false : (bool?)null),
                    Entidad = f.Entidad,
                    Numero = f.Numero,
                    FechaApertura = f.FechaApertura != null ? DateTime.Parse(f.FechaApertura) : (DateTime?)null,
                    Calificacion = f.Calificacion,
                    SituacionTitular = f.SituacionTitular,
                    Oficina = f.Oficina,
                    Ciudad = f.Ciudad,
                    CodigoDaneCiudad = f.CodigoDaneCiudad,
                    TipoIdentificacion = f.TipoIdentificacion != null ? Convert.ToInt32(f.TipoIdentificacion) : (int?)null,
                    Identificacion = f.Identificacion,
                    Sector = f.Sector,
                    CaracteristicaClase = f.Caracteristicas?.Clase,
                    ValorMoneda = f.Valores.Valor != null && f.Valores.Valor.Any() ? f.Valores.Valor.First().Moneda : null,
                    ValorFecha = f.Valores.Valor != null && f.Valores.Valor.Any()
                        ? DateTime.Parse(f.Valores.Valor.First().Fecha)
                        : (DateTime?)null,
                    ValorCalificacion = f.Valores.Valor != null && f.Valores.Valor.Any() ? f.Valores.Valor.First().Calificacion : null,
                    EstadoCodigo = f.Estado?.Codigo,
                    EstadoFecha = f.Estado != null ? DateTime.Parse(f.Estado.Fecha) : (DateTime?)null,
                    //Estadocantidad = f.Estado.Cantidad
                    Llave = f.Llave,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaCuentaAhorro.Add(cuentaAhorro);
                //repoCuentaAhorro.Insert(cuentaAhorro);
            });

            List<DataCreditoDataTarjetaCreditoBO> listaTarjeta = new List<DataCreditoDataTarjetaCreditoBO>();
            informe.Informe.TarjetaCredito.ForEach(f =>
            {
                DataCreditoDataTarjetaCreditoBO tarjeta = new DataCreditoDataTarjetaCreditoBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Bloqueada = f.Bloqueada == "true" ? true : (f.Bloqueada == "false" ? false : (bool?)null),
                    Entidad = f.Entidad,
                    Numero = f.Numero,
                    FechaApertura = f.FechaApertura != null ? DateTime.Parse(f.FechaApertura) : (DateTime?)null,
                    FechaVencimiento = f.FechaVencimiento != null ? DateTime.Parse(f.FechaVencimiento) : (DateTime?)null,
                    Comportamiento = f.Comportamiento,
                    FormaPago = f.FormaPago,
                    ProbabilidadIncumplimiento = f.ProbabilidadIncumplimiento != null ? Convert.ToDecimal(f.ProbabilidadIncumplimiento) : (decimal?)null,
                    Calificacion = f.Calificacion,
                    SituacionTitular = f.SituacionTitular,
                    Oficina = f.Oficina,
                    Ciudad = f.Ciudad,
                    CodigoDaneCiudad = f.CodigoDaneCiudad,
                    TipoIdentificacion = f.TipoIdentificacion != null ? Convert.ToInt32(f.TipoIdentificacion) : (int?)null,
                    Identificacion = f.Identificacion,
                    Sector = f.Sector,
                    CalificacionHd =
                        f.CalificacionHD == "true" ? true : (f.Bloqueada == "false" ? false : (bool?)null),
                    CaracteristicaFranquicia = f.Caracteristicas?.Franquicia,
                    CaracteristicaClase = f.Caracteristicas?.Clase,
                    CaracteristicaMarca = f.Caracteristicas?.Marca,
                    CaracteristicaAmparada = f.Caracteristicas?.Amparada == "true"
                        ? true
                        : (f.Bloqueada == "false" ? false : (bool?)null),
                    CaracteristicaCodigoAmparada = f.Caracteristicas?.CodigoAmparada,
                    CaracteristicaGarantia = f.Caracteristicas?.Garantia,
                    ValorMoneda = f.Valores != null && f.Valores.Valor.Any() ? f.Valores.Valor.First().Moneda : null,
                    ValorFecha = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().Fecha != null
                        ? DateTime.Parse(f.Valores.Valor.First().Fecha)
                        : (DateTime?)null,
                    ValorCalificacion = f.Valores != null && f.Valores.Valor.Any() ? f.Valores.Valor.First().Calificacion : null,
                    ValorSaldoActual = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToDecimal(f.Valores.Valor.First().SaldoActual)
                        : (decimal?)null,
                    ValorSaldoMora = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToDecimal(f.Valores.Valor.First().SaldoMora)
                        : (decimal?)null,
                    ValorDisponible = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToDecimal(f.Valores.Valor.First().Disponible)
                        : (decimal?)null,
                    ValorCuota = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToDecimal(f.Valores.Valor.First().Cuota)
                        : (decimal?)null,
                    ValorCuotasMora = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToDecimal(f.Valores.Valor.First().CuotasMora)
                        : (decimal?)null,
                    ValorDiasMora = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToInt32(f.Valores.Valor.First().DiasMora)
                        : (int?)null,
                    ValorFechaPagoCuota = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().FechaPagoCuota != null
                        ? DateTime.Parse(f.Valores.Valor.First().FechaPagoCuota)
                        : (DateTime?)null,
                    ValorFechaLimitePago = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().FechaLimitePago != null
                        ? DateTime.Parse(f.Valores.Valor.First().FechaLimitePago)
                        : (DateTime?)null,
                    ValorCupoTotal = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToDecimal(f.Valores.Valor.First().CupoTotal)
                        : (decimal?)null,
                    EstadoPlasticoCodigo = f.Estados?.EstadoPlastico?.Codigo,
                    EstadoPlasticoFecha = (f.Estados != null && f.Estados.EstadoPlastico != null && f.Estados.EstadoPlastico.Fecha != null) ?
                        Convert.ToDateTime(f.Estados.EstadoPlastico.Fecha) : (DateTime?)null,
                    EstadoCuentaCodigo = f.Estados?.EstadoCuenta?.Codigo,
                    EstadoCuentaFecha = f.Estados?.EstadoCuenta != null && f.Estados?.EstadoCuenta.Fecha != null
                        ? DateTime.Parse(f.Estados?.EstadoCuenta.Fecha)
                        : (DateTime?)null,
                    EstadoOrigenCodigo = f.Estados?.EstadoOrigen?.Codigo,
                    EstadoOrigenFecha = f.Estados?.EstadoOrigen != null && f.Estados.EstadoOrigen.Fecha != null
                        ? DateTime.Parse(f.Estados.EstadoOrigen.Fecha)
                        : (DateTime?)null,
                    EstadoPagoCodigo = f.Estados?.EstadoPago?.Codigo,
                    EstadoPagoFecha = f.Estados?.EstadoPago != null && f.Estados.EstadoPago.Fecha != null
                        ? DateTime.Parse(f.Estados.EstadoPago.Fecha)
                        : (DateTime?)null,
                    Llave = f.Llave,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaTarjeta.Add(tarjeta);
                //repoTarjetaCredito.Insert(tarjeta);
            });

            List<DataCreditoDataCuentaCarteraBO> listaCuenta = new List<DataCreditoDataCuentaCarteraBO>();
            informe.Informe.CuentaCartera.ForEach(f =>
            {
                DataCreditoDataCuentaCarteraBO cuenta = new DataCreditoDataCuentaCarteraBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Bloqueada = (f.Bloqueada != null && f.Bloqueada == "true") ? true : (f.Bloqueada == "false" ? false : (bool?)null),
                    Entidad = f.Entidad,
                    Numero = f.Numero,
                    FechaApertura = f.FechaApertura != null ? DateTime.Parse(f.FechaApertura) : (DateTime?)null,
                    FechaVencimiento = f.FechaVencimiento != null ? DateTime.Parse(f.FechaVencimiento) : (DateTime?)null,
                    Comportamiento = f.Comportamiento,
                    FormaPago = f.FormaPago,
                    ProbabilidadIncumplimiento = f.ProbabilidadIncumplimiento != null ? Convert.ToDecimal(f.ProbabilidadIncumplimiento) : (decimal?)null,
                    Calificacion = f.Calificacion,
                    SituacionTitular = f.SituacionTitular,
                    Oficina = f.Oficina,
                    Ciudad = f.Ciudad,
                    CodigoDaneCiudad = f.CodigoDaneCiudad,
                    TipoIdentificacion = f.TipoIdentificacion != null ? Convert.ToInt32(f.TipoIdentificacion) : (int?)null,
                    Identificacion = f.Identificacion,
                    Sector = f.Sector,
                    CalificacionHd =
                        f.CalificacionHD != null && f.CalificacionHD == "true" ? true : (f.CalificacionHD == "false" ? false : (bool?)null),

                    CaracteristicaTipoCuenta = f.Caracteristicas?.TipoCuenta,
                    CaracteristicaTipoObligacion = f.Caracteristicas?.TipoObligacion,
                    CaracteristicaTipoContrato = f.Caracteristicas?.TipoContrato,
                    CaracteristicaEjecucionContrato = f.Caracteristicas?.EjecucionContrato,
                    CaracteristicaMesesPermanencia = f.Caracteristicas?.MesesPermanencia,
                    CaracteristicaCalidadDeudor = f.Caracteristicas?.CalidadDeudor,
                    CaracteristicaGarantia = f.Caracteristicas?.Garantia,
                    ValorMoneda = f.Valores != null && f.Valores.Valor.Any()
                        ? f.Valores.Valor.First().Moneda
                        : null,
                    ValorFecha = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().Fecha != null
                        ? DateTime.Parse(f.Valores.Valor.First().Fecha)
                        : (DateTime?)null,
                    ValorCalificacion = f.Valores != null && f.Valores.Valor.Any()
                        ? f.Valores.Valor.First().Calificacion
                        : null,
                    ValorSaldoActual = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().SaldoActual != null
                        ? Convert.ToDecimal(f.Valores.Valor.First().SaldoActual)
                        : (decimal?)null,
                    ValorSaldoMora = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().SaldoMora != null
                        ? Convert.ToDecimal(f.Valores.Valor.First().SaldoMora)
                        : (decimal?)null,
                    ValorDisponible = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().Disponible != null
                        ? Convert.ToDecimal(f.Valores.Valor.First().Disponible)
                        : (decimal?)null,
                    ValorCuota = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().Cuota != null
                        ? Convert.ToDecimal(f.Valores.Valor.First().Cuota)
                        : (decimal?)null,
                    ValorCuotasMora = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().CuotasMora != null
                        ? Convert.ToDecimal(f.Valores.Valor.First().CuotasMora)
                        : (decimal?)null,
                    ValorDiasMora = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().DiasMora != null
                        ? Convert.ToInt32(f.Valores.Valor.First().DiasMora)
                        : (int?)null,
                    ValorFechaPagoCuota = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().FechaPagoCuota != null
                        ? DateTime.Parse(f.Valores.Valor.First().FechaPagoCuota)
                        : (DateTime?)null,
                    ValorFechaLimitePago = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().FechaLimitePago != null
                        ? DateTime.Parse(f.Valores.Valor.First().FechaLimitePago)
                        : (DateTime?)null,
                    ValorPeriodicidad = f.Valores != null && f.Valores.Valor.Any()
                        ? f.Valores.Valor.First().Periodicidad
                        : null,
                    ValorTotalCuotas = f.Valores != null && f.Valores.Valor.Any()
                        ? f.Valores.Valor.First().TotalCuotas
                        : null,
                    ValorValorInicial = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().ValorInicial != null
                        ? Convert.ToDecimal(f.Valores.Valor.First().ValorInicial)
                        : (decimal?)null,
                    ValorCuotasCanceladas = f.Valores != null && f.Valores.Valor.Any()
                        ? f.Valores.Valor.First().CuotasCanceladas
                        : null,
                    EstadoCuentaCodigo = f.Estados.EstadoCuenta?.Codigo,
                    EstadoCuentaFecha = f.Estados.EstadoCuenta != null && f.Estados.EstadoCuenta.Fecha != null
                        ? DateTime.Parse(f.Estados.EstadoCuenta.Fecha)
                        : (DateTime?)null,
                    EstadoOrigenCodigo = f.Estados.EstadoOrigen?.Codigo,
                    EstadoOrigenFecha = f.Estados.EstadoOrigen != null && f.Estados.EstadoOrigen.Fecha != null
                        ? DateTime.Parse(f.Estados.EstadoOrigen.Fecha)
                        : (DateTime?)null,
                    EstadoPagoCodigo = f.Estados.EstadoPago?.Codigo,
                    EstadoPagoFecha = f.Estados.EstadoPago != null && f.Estados.EstadoPago.Fecha != null
                        ? DateTime.Parse(f.Estados.EstadoPago.Fecha)
                        : (DateTime?)null,
                    Llave = f.Llave,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaCuenta.Add(cuenta);
                //repoCuentaCartera.Insert(cuenta);
            });

            List<DataCreditoConsultaBO> listaConsulta = new List<DataCreditoConsultaBO>();
            informe.Informe.Consulta.ForEach(f =>
            {
                DataCreditoConsultaBO consulta = new DataCreditoConsultaBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Fecha = f.Fecha != null ? DateTime.Parse(f.Fecha) : (DateTime?)null,
                    TipoCuenta = f.TipoCuenta,
                    Entidad = f.Entidad,
                    Oficina = f.Oficina,
                    Ciudad = f.Ciudad,
                    Razon = f.Razon,
                    Cantidad = f.Cantidad,
                    NitSuscriptor = f.NitSuscriptor,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaConsulta.Add(consulta);
                //repoConsulta.Insert(consulta);
            });

            List<DataCreditoDataEndeudamientoGlobalBO> listaEndeudamiento = new List<DataCreditoDataEndeudamientoGlobalBO>();
            informe.Informe.EndeudamientoGlobal.ForEach(f =>
            {
                DataCreditoDataEndeudamientoGlobalBO endeudamiento = new DataCreditoDataEndeudamientoGlobalBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Calificacion = f.Calificacion,
                    Fuente = f.Fuente,
                    SaldoPendiente = f.SaldoPendiente,
                    TipoCredito = f.TipoCredito,
                    Moneda = f.Moneda,
                    NumeroCreditos = f.NumeroCreditos,
                    Independiente = f.Independiente,
                    FechaReporte = f.FechaReporte != null ? DateTime.Parse(f.FechaReporte) : (DateTime?)null,
                    EntidadNombre = f.Entidad?.Nombre,
                    EntidadNit = f.Entidad?.Nit,
                    EntidadSector = f.Entidad?.Sector,
                    GarantiaTipo = f.Garantia?.Tipo,
                    GarantiaValor = f.Garantia?.Valor,
                    Llave = f.Llave,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaEndeudamiento.Add(endeudamiento);
                //repoEndeudamiento.Insert(endeudamiento);
            });

            DataCreditoDataProductoValorBO productoValor = new DataCreditoDataProductoValorBO()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                Producto = informe.Informe.ProductosValores?.Producto,
                Valor1 = informe.Informe.ProductosValores?.Valor1,
                Valor2 = informe.Informe.ProductosValores?.Valor2,
                Valor3 = informe.Informe.ProductosValores?.Valor3,
                Valor4 = informe.Informe.ProductosValores?.Valor4,
                Valor5 = informe.Informe.ProductosValores?.Valor5,
                Valor6 = informe.Informe.ProductosValores?.Valor6,
                Valor7 = informe.Informe.ProductosValores?.Valor7,
                Valor8 = informe.Informe.ProductosValores?.Valor8,
                Valor9 = informe.Informe.ProductosValores?.Valor9,
                Valor10 = informe.Informe.ProductosValores?.Valor10,
                Valor1smlv = informe.Informe.ProductosValores.Valor1smlv,
                Valor2smlv = informe.Informe.ProductosValores.Valor2smlv,
                Valor3smlv = informe.Informe.ProductosValores.Valor3smlv,
                Valor4smlv = informe.Informe.ProductosValores.Valor4smlv,
                Valor5smlv = informe.Informe.ProductosValores.Valor5smlv,
                Valor6smlv = informe.Informe.ProductosValores.Valor6smlv,
                Valor7smlv = informe.Informe.ProductosValores.Valor7smlv,
                Valor8smlv = informe.Informe.ProductosValores.Valor8smlv,
                Valor9smlv = informe.Informe.ProductosValores.Valor9smlv,
                Valor10smlv = informe.Informe.ProductosValores.Valor10smlv,
                Razon1 = informe.Informe.ProductosValores?.Razon1,
                Razon2 = informe.Informe.ProductosValores?.Razon2,
                Razon3 = informe.Informe.ProductosValores?.Razon3,
                Razon4 = informe.Informe.ProductosValores?.Razon4,
                Razon5 = informe.Informe.ProductosValores?.Razon5,
                Razon6 = informe.Informe.ProductosValores?.Razon6,
                Razon7 = informe.Informe.ProductosValores?.Razon7,
                Razon8 = informe.Informe.ProductosValores?.Razon8,
                Razon9 = informe.Informe.ProductosValores?.Razon9,
                Razon10 = informe.Informe.ProductosValores?.Razon10,

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            //repoProductoValor.Insert(productoValor);

            //calculo del agregado
            //resumen
            DataCreditoDataInfAgrResumenPrincipalBO resumenPrincipal = new DataCreditoDataInfAgrResumenPrincipalBO()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                CreditosVigentes = informe.Informe.InfoAgregada.Resumen.Principales?.CreditoVigentes,
                CreditosCerrados = informe.Informe.InfoAgregada.Resumen.Principales?.CreditosCerrados,
                CreditosActualesNegativos = informe.Informe.InfoAgregada.Resumen.Principales?.CreditosActualesNegativos,
                HistNegUlt12Meses = informe.Informe.InfoAgregada.Resumen.Principales?.HistNegUlt12Meses,
                CuentasAbiertasAhoccb = informe.Informe.InfoAgregada.Resumen.Principales?.CuentasAbiertasAHOCCB,
                CuentasCerradasAhoccb = informe.Informe.InfoAgregada.Resumen.Principales?.CuentasCerradasAHOCCB,
                ConsultadasUlt6meses = informe.Informe.InfoAgregada.Resumen.Principales?.ConsultadasUlt6meses,
                DesacuerdosAlaFecha = informe.Informe.InfoAgregada.Resumen.Principales?.DesacuerdosALaFecha,
                ReclamosVigentes = informe.Informe.InfoAgregada.Resumen.Principales?.ReclamosVigentes,

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            //repoResumenPrincipal.Insert(resumenPrincipal);

            DataCreditoDataInfAgrResumenSaldoBO resumenSaldo = new DataCreditoDataInfAgrResumenSaldoBO()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SaldoTotalEnMora = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.SaldoTotalEnMora != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.SaldoTotalEnMora)
                    : (decimal?)null,
                SaldoM30 = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.SaldoM30 != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.SaldoM30)
                    : (decimal?)null,
                SaldoM60 = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.SaldoM60 != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.SaldoM60)
                    : (decimal?)null,
                SaldoM90 = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.SaldoM90 != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.SaldoM90)
                    : (decimal?)null,
                CuotaMensual = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.CuotaMensual != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.CuotaMensual)
                    : (decimal?)null,
                SaldoCreditoMasAlto = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.SaldoCreditoMasAlto != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.SaldoCreditoMasAlto)
                    : (decimal?)null,
                SaldoTotal = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.SaldoTotal != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.SaldoTotal)
                    : (decimal?)null,

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            //repoResumenSaldo.Insert(resumenSaldo);

            List<DataCreditoDataInfAgrResumenSaldoSectorBO> listaSaldoSector = new List<DataCreditoDataInfAgrResumenSaldoSectorBO>();
            informe.Informe.InfoAgregada.Resumen.Saldos.Sector.ForEach(f =>
            {
                DataCreditoDataInfAgrResumenSaldoSectorBO saldoSector = new DataCreditoDataInfAgrResumenSaldoSectorBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Sector = f.Sectores,
                    Saldo = f.Saldo != null ? Convert.ToDecimal(f.Saldo) : (decimal?)null,
                    Participacion = f.Participacion,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaSaldoSector.Add(saldoSector);
                //repoResumenSaldoSector.Insert(saldoSector);
            });

            List<DataCreditoDataInfAgrResumenSaldoMesBO> listaSaldoMes = new List<DataCreditoDataInfAgrResumenSaldoMesBO>();
            informe.Informe.InfoAgregada.Resumen.Saldos.Mes.ForEach(f =>
            {
                DataCreditoDataInfAgrResumenSaldoMesBO saldoMes = new DataCreditoDataInfAgrResumenSaldoMesBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Fecha = f.Fecha != null ? DateTime.Parse(f.Fecha) : (DateTime?)null,
                    SaldoTotalMora = f.SaldoTotalMora != null ? Convert.ToDecimal(f.SaldoTotalMora) : (decimal?)null,
                    SaldoTotal = f.SaldoTotal != null ? Convert.ToDecimal(f.SaldoTotal) : (decimal?)null,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaSaldoMes.Add(saldoMes);
                //repoResumenSaldoMes.Insert(saldoMes);
            });

            List<DataCreditoDataInfAgrResumenComportamientoBO> listaResumenComportamiento = new List<DataCreditoDataInfAgrResumenComportamientoBO>();
            informe.Informe.InfoAgregada.Resumen.Comportamiento.Mes.ForEach(f =>
            {
                DataCreditoDataInfAgrResumenComportamientoBO resumenComportamiento =
                    new DataCreditoDataInfAgrResumenComportamientoBO()
                    {
                        IdDataCreditoBusqueda = busqueda.Id,
                        Fecha = f.Fecha != null ? DateTime.Parse(f.Fecha) : (DateTime?)null,
                        Comportamiento = f.Comportamiento,
                        Cantidad = f.Cantidad != null ? Convert.ToInt32(f.Cantidad) : (int?)null,

                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                listaResumenComportamiento.Add(resumenComportamiento);
                //repoResumenCompotamiento.Insert(resumenComportamiento);
            });

            //totales
            List<DataCreditoDataInfAgrTotalBO> listaTotalTipoCuenta = new List<DataCreditoDataInfAgrTotalBO>();
            informe.Informe.InfoAgregada.Totales.TipoCuenta.ForEach(f =>
            {
                DataCreditoDataInfAgrTotalBO totalTipoCuenta = new DataCreditoDataInfAgrTotalBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    TipoMapeo = "TipoCuenta",
                    CodigoTipo = f.CodigoTipo,
                    Tipo = f.Tipo,
                    CalidadDeudor = f.CalidadDeudor,
                    Participacion = null,
                    Cupo = f.Cupo,
                    Saldo = f.Saldo,
                    SaldoMora = f.SaldoMora,
                    Cuota = f.Cuota,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaTotalTipoCuenta.Add(totalTipoCuenta);
                //repoInfAgrTotal.Insert(totalTipoCuenta);
            });

            informe.Informe.InfoAgregada.Totales.Total.ForEach(f =>
            {
                DataCreditoDataInfAgrTotalBO totalTipoCuenta = new DataCreditoDataInfAgrTotalBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    TipoMapeo = "Total ",
                    CodigoTipo = null,
                    Tipo = null,
                    CalidadDeudor = f.CalidadDeudor,
                    Participacion = f.Participacion,
                    Cupo = f.Cupo,
                    Saldo = f.Saldo,
                    SaldoMora = f.SaldoMora,
                    Cuota = f.Cuota,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaTotalTipoCuenta.Add(totalTipoCuenta);
                //repoInfAgrTotal.Insert(totalTipoCuenta);
            });

            //ComposicionPortafolio
            List<DataCreditoDataInfAgrComposicionPortafolioBO> listaTotalComposicionPortafolio = new List<DataCreditoDataInfAgrComposicionPortafolioBO>();
            informe.Informe.InfoAgregada.ComposicionPortafolio.TipoCuenta.ForEach(f =>
            {
                DataCreditoDataInfAgrComposicionPortafolioBO totalComposicionPortafolio = new DataCreditoDataInfAgrComposicionPortafolioBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Tipo = f.Tipo,
                    CalidadDeudor = f.CalidadDeudor,
                    Porcentaje = f.Porcentaje != null ? Convert.ToDecimal(f.Porcentaje) : (decimal?)null,
                    Cantidad = f.Cantidad != null ? Convert.ToInt32(f.Cantidad) : (int?)null,
                    EstadoCodigo = f.Estado.Codigo,
                    EstadoCantidad = f.Estado.Cantidad != null ? Convert.ToInt32(f.Estado.Cantidad) : (int?)null,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaTotalComposicionPortafolio.Add(totalComposicionPortafolio);
                //repoInfAgrComposicionPortafolio.Insert(totalComposicionPortafolio);
            });

            //EvolucionDeuda
            List<DataCreditoDataInfAgrEvolucionDeudaTrimestreBO> listaEvolucionDeudaTrimestre = new List<DataCreditoDataInfAgrEvolucionDeudaTrimestreBO>();
            informe.Informe.InfoAgregada.EvolucionDeuda.Trimestre.ForEach(f =>
            {
                DataCreditoDataInfAgrEvolucionDeudaTrimestreBO evolucionDeudaTrimestre =
                    new DataCreditoDataInfAgrEvolucionDeudaTrimestreBO()
                    {
                        IdDataCreditoBusqueda = busqueda.Id,
                        Fecha = f.Fecha != null ? DateTime.Parse(f.Fecha) : (DateTime?)null,
                        Cuota = f.Cuota != null ? Convert.ToDecimal(f.Cuota) : (decimal?)null,
                        Cupototal = f.CupoTotal != null ? Convert.ToDecimal(f.CupoTotal) : (decimal?)null,
                        Saldo = f.Saldo != null ? Convert.ToDecimal(f.Saldo) : (decimal?)null,
                        PorcentajeUso = f.PorcentajeUso,
                        Score = f.Score != null ? Convert.ToDecimal(f.Score) : (decimal?)null,
                        Calificacion = f.Calificacion,
                        AperturaCuentas = f.AperturaCuentas,
                        CierreCuentas = f.CierreCuentas,
                        TotalAbiertas = f.TotalAbiertas != null ? Convert.ToInt32(f.TotalAbiertas) : (int?)null,
                        TotalCerradas = f.TotalCerradas != null ? Convert.ToInt32(f.TotalCerradas) : (int?)null,
                        MoraMaxima = f.MoraMaxima,
                        MesesMoraMaxima = f.MesesMoraMaxima != null ? Convert.ToInt32(f.MesesMoraMaxima) : (int?)null,

                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                listaEvolucionDeudaTrimestre.Add(evolucionDeudaTrimestre);
                //repoInfAgrEvolucionDeudaTrimestre.Insert(evolucionDeudaTrimestre);
            });

            DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO analisisPromedio =
                new DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Cuota = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Cuota != null
                        ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Cuota)
                        : (decimal?)null,
                    CupoTotal = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.CupoTotal != null
                        ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.CupoTotal)
                        : (decimal?)null,
                    Saldo = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Saldo != null
                        ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Saldo)
                        : (decimal?)null,
                    Porcentaje = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.PorcentajeUso,
                    Score = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Score != null
                        ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Score)
                        : (decimal?)null,
                    Calificacion = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Calificacion != null
                        ? Convert.ToInt32(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Calificacion)
                        : (int?)null,
                    AperturaCuentas =
                        informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.AperturaCuentas != null
                            ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio
                                .AperturaCuentas)
                            : (decimal?)null,
                    CierreCuentas = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.CierreCuentas != null
                        ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.CierreCuentas)
                        : (decimal?)null,
                    TotalAbiertas = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.TotalAbiertas,
                    TotalCerradas = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.TotalCerradas,
                    MoraMaxima = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.MoraMaxima != null
                        ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.MoraMaxima)
                        : (decimal?)null,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
            //repoInfAgrEvolucionDeudaAnalisisPromedio.Insert(analisisPromedio);

            //HistoricoSaldos
            List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO> listaTipoCuenta = new List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO>();
            informe.Informe.InfoAgregada.HistoricoSaldos.TipoCuenta.ForEach(f =>
            {
                f.Trimestre.ForEach(g =>
                {
                    DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO tipoCuenta =
                        new DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO()
                        {
                            IdDataCreditoBusqueda = busqueda.Id,
                            Tipo = f.Tipo,
                            Fecha = g.Fecha != null ? DateTime.Parse(g.Fecha) : (DateTime?)null,
                            TotalCuentas = g.TotalCuentas != null ? Convert.ToInt32(g.TotalCuentas) : (int?)null,
                            CuentasConsideradas = g.CuentasConsideradas != null
                                ? Convert.ToInt32(g.CuentasConsideradas)
                                : (int?)null,
                            Saldo = g.Saldo != null ? Convert.ToDecimal(g.Saldo) : (decimal?)null,

                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    listaTipoCuenta.Add(tipoCuenta);
                    //repoInfAgrHistoricoSaldoTipoCuenta.Insert(tipoCuenta);
                });
            });

            List<DataCreditoDataInfAgrHistoricoSaldoTotalBO> listaSaldoTotal = new List<DataCreditoDataInfAgrHistoricoSaldoTotalBO>();
            informe.Informe.InfoAgregada.HistoricoSaldos.Totales.ForEach(f =>
            {
                DataCreditoDataInfAgrHistoricoSaldoTotalBO saldoTotal = new DataCreditoDataInfAgrHistoricoSaldoTotalBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Fecha = f.Fecha != null ? DateTime.Parse(f.Fecha) : (DateTime?)null,
                    TotalCuentas = f.TotalCuentas != null ? Convert.ToInt32(f.TotalCuentas) : (int?)null,
                    CuentasConsideradas = f.CuentasConsideradas != null ? Convert.ToInt32(f.CuentasConsideradas) : (int?)null,
                    Saldo = f.Saldo != null ? Convert.ToDecimal(f.Saldo) : (decimal?)null,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaSaldoTotal.Add(saldoTotal);
                //repoInfAgrHistoricoSaldoTotal.Insert(saldoTotal);
            });

            //ResumenEndeudamiento
            List<DataCreditoDataInfAgrResumenEndeudamientoBO> listaResumenEndeudamiento = new List<DataCreditoDataInfAgrResumenEndeudamientoBO>();
            informe.Informe.InfoAgregada.ResumenEndeudamiento.Trimestre.ForEach(t =>
            {
                t.Sector.ForEach(s =>
                {
                    s.Cartera.ForEach(c =>
                    {
                        DataCreditoDataInfAgrResumenEndeudamientoBO resumenEndeudamiento =
                            new DataCreditoDataInfAgrResumenEndeudamientoBO()
                            {
                                IdDataCreditoBusqueda = busqueda.Id,
                                TrimestreFecha = t.Fecha != null ? Convert.ToDateTime(t.Fecha) : (DateTime?)null,
                                SectorSector = s.Sectores,
                                SectorCodigoSector = s.CodigoSector,
                                SectorGarantiaAdmisible = s.GarantiaAdmisible,
                                SectorGarantiaOtro = s.GarantiaOtro,
                                CarteraTipo = c.Tipo,
                                CarteraNumeroCuentas = c.NumeroCuentas != null
                                    ? Convert.ToInt32(c.NumeroCuentas)
                                    : (int?)null,
                                CarteraValor = c.Valor != null ? Convert.ToDecimal(c.Valor) : (decimal?)null,

                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                        listaResumenEndeudamiento.Add(resumenEndeudamiento);
                        //repoInfAgrResumenEndeudamiento.Insert(resumenEndeudamiento);
                    });
                });
            });

            ////Calculo Microcredito
            //resumen
            DataCreditoDataInfMicroPerfilGeneralBO boCreditoVigente = new DataCreditoDataInfMicroPerfilGeneralBO()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosVigentes.SectorFinanciero,
                SectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosVigentes.SectorCooperativo,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosVigentes.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosVigentes.SectorTelcos,
                TotalComoPrincipal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosVigentes.TotalComoPrincipal,
                TotalComoCodeudorYotros = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosVigentes.TotalComoCodeudorYOtros,
                Tipo = "CreditosVigentes",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            DataCreditoDataInfMicroPerfilGeneralBO boCreditoCerrado = new DataCreditoDataInfMicroPerfilGeneralBO()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosCerrados.SectorFinanciero,
                SectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosCerrados.SectorCooperativo,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosCerrados.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosCerrados.SectorTelcos,
                TotalComoPrincipal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosCerrados.TotalComoPrincipal,
                TotalComoCodeudorYotros = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosCerrados.TotalComoCodeudorYOtros,
                Tipo = "CreditosCerrados ",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            DataCreditoDataInfMicroPerfilGeneralBO boCreditosReestructurado = new DataCreditoDataInfMicroPerfilGeneralBO()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosReestructurados.SectorFinanciero,
                SectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosReestructurados.SectorCooperativo,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosReestructurados.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosReestructurados.SectorTelcos,
                TotalComoPrincipal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosReestructurados.TotalComoPrincipal,
                TotalComoCodeudorYotros = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosReestructurados.TotalComoCodeudorYOtros,
                Tipo = "CreditosReestructurados",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            DataCreditoDataInfMicroPerfilGeneralBO boCreditosRefinanciado = new DataCreditoDataInfMicroPerfilGeneralBO()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosRefinanciados.SectorFinanciero,
                SectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosRefinanciados.SectorCooperativo,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosRefinanciados.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosRefinanciados.SectorTelcos,
                TotalComoPrincipal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosRefinanciados.TotalComoPrincipal,
                TotalComoCodeudorYotros = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosRefinanciados.TotalComoCodeudorYOtros,
                Tipo = "CreditosRefinanciados",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            DataCreditoDataInfMicroPerfilGeneralBO boConsultaUlt6Meses = new DataCreditoDataInfMicroPerfilGeneralBO()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.ConsultaUlt6Meses.SectorFinanciero,
                SectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.ConsultaUlt6Meses.SectorCooperativo,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.ConsultaUlt6Meses.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.ConsultaUlt6Meses.SectorTelcos,
                TotalComoPrincipal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.ConsultaUlt6Meses.TotalComoPrincipal,
                TotalComoCodeudorYotros = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.ConsultaUlt6Meses.TotalComoCodeudorYOtros,
                Tipo = "ConsultaUlt6Meses",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            DataCreditoDataInfMicroPerfilGeneralBO boDesacuerdos = new DataCreditoDataInfMicroPerfilGeneralBO()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.Desacuerdos.SectorFinanciero,
                SectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.Desacuerdos.SectorCooperativo,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.Desacuerdos.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.Desacuerdos.SectorTelcos,
                TotalComoPrincipal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.Desacuerdos.TotalComoPrincipal,
                TotalComoCodeudorYotros = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.Desacuerdos.TotalComoCodeudorYOtros,
                Tipo = "Desacuerdos",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            DataCreditoDataInfMicroPerfilGeneralBO boAntiguedadDesde = new DataCreditoDataInfMicroPerfilGeneralBO()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.AntiguedadDesde.SectorFinanciero,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.AntiguedadDesde.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.AntiguedadDesde.SectorTelcos,
                Tipo = "AntiguedadDesde",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            //repoInfMicroPerfilGeneral.Insert(boCreditoVigente);
            //repoInfMicroPerfilGeneral.Insert(boCreditoCerrado);
            //repoInfMicroPerfilGeneral.Insert(boCreditosReestructurado);
            //repoInfMicroPerfilGeneral.Insert(boCreditosRefinanciado);
            //repoInfMicroPerfilGeneral.Insert(boConsultaUlt6Meses);
            //repoInfMicroPerfilGeneral.Insert(boAntiguedadDesde);

            List<DataCreditoDataInfMicroVectorSaldoMoraBO> listaBoVectorSaldoMora = new List<DataCreditoDataInfMicroVectorSaldoMoraBO>();
            informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.SaldosYMoras.ForEach(f =>
            {
                DataCreditoDataInfMicroVectorSaldoMoraBO boVectorSaldoMora = new DataCreditoDataInfMicroVectorSaldoMoraBO()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    PoseeSectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorCooperativo == "true"
                        ? true
                        : (informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorCooperativo == "false" ? false : (bool?)null),
                    PoseeSectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorFinanciero == "true"
                        ? true
                        : (informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorFinanciero == "false" ? false : (bool?)null),
                    PoseeSectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorReal == "true"
                        ? true
                        : (informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorReal == "false" ? false : (bool?)null),
                    PoseeSectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorTelcos == "true"
                        ? true
                        : (informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorTelcos == "false" ? false : (bool?)null),
                    Fecha = f.Fecha != null ? DateTime.Parse(f.Fecha) : (DateTime?)null,
                    TotalCuentasMora = f.TotalCuentasMora != null ? Convert.ToInt32(f.TotalCuentasMora) : (int?)null,
                    SaldoDeudaTotalMora = f.SaldoDeudaTotalMora != null ? Convert.ToDecimal(f.SaldoDeudaTotalMora) : (decimal?)null,
                    SaldoDeudaTotal = f.SaldoDeudaTotal != null ? Convert.ToDecimal(f.SaldoDeudaTotal) : (decimal?)null,
                    MorasMaxSectorFinanciero = f.MorasMaxSectorFinanciero,
                    MorasMaxSectorReal = f.MorasMaxSectorReal,
                    MorasMaxSectorTelcos = f.MorasMaxSectorTelcos,
                    MorasMaximas = f.MorasMaximas,
                    NumCreditos30 = f.NumCreditos30 != null ? Convert.ToInt32(f.NumCreditos30) : (int?)null,
                    NumCreditosMayorIgual60 = f.NumCreditosMayorIgual60 != null ? Convert.ToInt32(f.NumCreditosMayorIgual60) : (int?)null,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaBoVectorSaldoMora.Add(boVectorSaldoMora);
                //repoInfMicroVerctorSaldoMora.Insert(boVectorSaldoMora);
            });

            List<DataCreditoDataInfMicroEndeudamientoActualBO> listaBoEndeudamiento = new List<DataCreditoDataInfMicroEndeudamientoActualBO>();
            informe.Informe.InfoAgregadaMicrocredito.Resumen.EndeudamientoActual.Sector.ForEach(s =>
            {
                s.TipoCuenta.ForEach(t =>
                {
                    t.Usuario.ForEach(u =>
                    {
                        u.Cuenta.ForEach(uc =>
                        {
                            DataCreditoDataInfMicroEndeudamientoActualBO boEndeudamiento =
                                new DataCreditoDataInfMicroEndeudamientoActualBO()
                                {
                                    IdDataCreditoBusqueda = busqueda.Id,
                                    SectorCodigoSector = s.CodSector,
                                    TipoCuenta = t.TipoCuentas,
                                    TipoUsuario = u.TipoUsuario,
                                    EstadoActual = uc.EstadoActual,
                                    Calificacion = uc.Calificacion,
                                    ValorInicial = uc.ValorInicial != null
                                        ? Convert.ToDecimal(uc.ValorInicial)
                                        : (decimal?)null,
                                    SaldoActual = uc.SaldoActual != null
                                        ? Convert.ToDecimal(uc.SaldoActual)
                                        : (decimal?)null,
                                    SaldoMora =
                                        uc.SaldoMora != null ? Convert.ToDecimal(uc.SaldoMora) : (decimal?)null,
                                    CuotaMes = uc.CuotaMes != null ? Convert.ToDecimal(uc.CuotaMes) : (decimal?)null,
                                    ComportamientoNegativo = uc.ComportamientoNegativo == "true"
                                        ? true
                                        : (uc.ComportamientoNegativo == "false" ? false : (bool?)null),
                                    TotalDeudaCarteras = uc.TotalDeudaCarteras != null
                                        ? Convert.ToDecimal(uc.TotalDeudaCarteras)
                                        : (decimal?)null,

                                    Estado = true,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                            listaBoEndeudamiento.Add(boEndeudamiento);
                            //repoInfMicroEndeudamientoActual.Insert(boEndeudamiento);
                        });
                    });

                    //t.Usuario.Cuenta.ForEach(uc =>
                    //{
                    //    DataCreditoDataInfMicroEndeudamientoActualBO boEndeudamiento =
                    //        new DataCreditoDataInfMicroEndeudamientoActualBO()
                    //        {
                    //            IdDataCreditoBusqueda = busqueda.Id,
                    //            SectorCodigoSector = s.CodSector,
                    //            TipoCuenta = t.TipoCuentas,
                    //            TipoUsuario = t.Usuario.TipoUsuario,
                    //            EstadoActual = uc.EstadoActual,
                    //            Calificacion = uc.Calificacion,
                    //            ValorInicial = uc.ValorInicial != null
                    //                ? Convert.ToDecimal(uc.ValorInicial)
                    //                : (decimal?) null,
                    //            SaldoActual = uc.SaldoActual != null
                    //                ? Convert.ToDecimal(uc.SaldoActual)
                    //                : (decimal?) null,
                    //            SaldoMora = uc.SaldoMora != null ? Convert.ToDecimal(uc.SaldoMora) : (decimal?) null,
                    //            CuotaMes = uc.CuotaMes != null ? Convert.ToDecimal(uc.CuotaMes) : (decimal?) null,
                    //            ComportamientoNegativo = uc.ComportamientoNegativo == "true"
                    //                ? true
                    //                : (uc.ComportamientoNegativo == "false" ? false : (bool?) null),
                    //            TotalDeudaCarteras = uc.TotalDeudaCarteras != null
                    //                ? Convert.ToDecimal(uc.TotalDeudaCarteras)
                    //                : (decimal?) null,

                    //            Estado = true,
                    //            UsuarioCreacion = usuario,
                    //            UsuarioModificacion = usuario,
                    //            FechaCreacion = DateTime.Now,
                    //            FechaModificacion = DateTime.Now
                    //        };
                    //    repoInfMicroEndeudamientoActual.Insert(boEndeudamiento);
                    //});
                });
            });

            List<DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO> listaBoImagenTendencia = new List<DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO>();
            informe.Informe.InfoAgregadaMicrocredito.Resumen.ImagenTendenciaEndeudamiento.Series.ForEach(s =>
            {
                s.Valores.Valor.ForEach(v =>
                {
                    DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO boImagenTendencia =
                        new DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO()
                        {
                            IdDataCreditoBusqueda = busqueda.Id,
                            Serie = s.Serie,
                            Valor = v.Valores != null ? Convert.ToDecimal(v.Valores) : (decimal?)null,
                            Fecha = v.Fecha != null ? DateTime.Parse(v.Fecha) : (DateTime?)null,

                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    listaBoImagenTendencia.Add(boImagenTendencia);
                    //repoInfMicroImagenTendencia.Insert(boImagenTendencia);
                });
            });

            //analisis vectores
            List<DataCreditoDataInfMicroAnalisisVectorBO> listaBoAnalisisVector = new List<DataCreditoDataInfMicroAnalisisVectorBO>();
            informe.Informe.InfoAgregadaMicrocredito.AnalisisVectores.Sector.ForEach(s =>
            {
                s.Cuenta.ForEach(c =>
                {
                    c.CaracterFecha.ForEach(cf =>
                    {
                        DataCreditoDataInfMicroAnalisisVectorBO boAnalisisVector =
                            new DataCreditoDataInfMicroAnalisisVectorBO()
                            {
                                IdDataCreditoBusqueda = busqueda.Id,
                                NombreSector = s.NombreSector,
                                CuentaEntidad = c.Entidad,
                                CuentaNumeroCuenta = c.NumeroCuenta,
                                CuentaTipoCuenta = c.TipoCuenta,
                                CuentaEstado = c.Estado,
                                ContieneDatos = c.ContieneDatos == "true"
                                    ? true
                                    : (c.ContieneDatos == "false" ? false : (bool?)null),
                                Fecha = cf.Fecha != null ? DateTime.Parse(cf.Fecha) : (DateTime?)null,
                                SaldoDeudaTotalMora = cf.SaldoDeudaTotalMora != null ? cf.SaldoDeudaTotalMora : null,

                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                        listaBoAnalisisVector.Add(boAnalisisVector);
                        //repoInfMicroAnalisisVector.Insert(boAnalisisVector);
                    });
                });
            });

            //evolucion deuda
            List<DataCreditoDataInfMicroEvolucionDeudaBO> listaBoEvolucionDeuda = new List<DataCreditoDataInfMicroEvolucionDeudaBO>();
            informe.Informe.InfoAgregadaMicrocredito.EvolucionDeuda.EvolucionDeudaSector.ForEach(e =>
            {
                e.EvolucionDeudaTipoCuenta.ForEach(tc =>
                {
                    tc.EvolucionDeudaValorTrimestre.ForEach(evt =>
                    {
                        DataCreditoDataInfMicroEvolucionDeudaBO boEvolucionDeuda =
                            new DataCreditoDataInfMicroEvolucionDeudaBO()
                            {
                                IdDataCreditoBusqueda = busqueda.Id,
                                CodigoSector = e.CodSector,
                                NombreSector = e.NombreSector,
                                TipoCuenta = tc.TipoCuenta,
                                Trimestre = evt.Trimestre,
                                Num = evt.Num,
                                CupoInicial = evt.CupoInicial != null
                                    ? Convert.ToDecimal(evt.CupoInicial)
                                    : (decimal?)null,
                                Saldo = evt.Saldo != null ? Convert.ToDecimal(evt.Saldo) : (decimal?)null,
                                SaldoMora = evt.SaldoMora != null ? Convert.ToDecimal(evt.SaldoMora) : (decimal?)null,
                                Cuota = evt.Cuota != null ? Convert.ToDecimal(evt.Cuota) : (decimal?)null,
                                PorcentajeDeuda = evt.PorcentajeDeuda != null
                                    ? Convert.ToDecimal(evt.PorcentajeDeuda)
                                    : (decimal?)null,
                                CodigoMenorCalificacion = evt.CodMenorCalificacion,
                                TextoMenorCalificacion = evt.TextoMenorCalificacion,

                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                        listaBoEvolucionDeuda.Add(boEvolucionDeuda);
                        //repoInfMicroEvolucionDeuda.Insert(boEvolucionDeuda);
                    });
                });
            });

            repoNaturalNacional.Insert(naturalNacional);
            repoScore.Insert(score);
            repoCuentaAhorro.Insert(listaCuentaAhorro);
            repoTarjetaCredito.Insert(listaTarjeta);
            repoCuentaCartera.Insert(listaCuenta);
            repoConsulta.Insert(listaConsulta);
            repoEndeudamiento.Insert(listaEndeudamiento);
            repoProductoValor.Insert(productoValor);
            repoResumenPrincipal.Insert(resumenPrincipal);
            repoResumenSaldo.Insert(resumenSaldo);
            repoResumenSaldoSector.Insert(listaSaldoSector);
            repoResumenSaldoMes.Insert(listaSaldoMes);
            repoResumenCompotamiento.Insert(listaResumenComportamiento);
            repoInfAgrTotal.Insert(listaTotalTipoCuenta);
            repoInfAgrComposicionPortafolio.Insert(listaTotalComposicionPortafolio);
            repoInfAgrEvolucionDeudaTrimestre.Insert(listaEvolucionDeudaTrimestre);
            repoInfAgrEvolucionDeudaAnalisisPromedio.Insert(analisisPromedio);
            repoInfAgrHistoricoSaldoTipoCuenta.Insert(listaTipoCuenta);
            repoInfAgrHistoricoSaldoTotal.Insert(listaSaldoTotal);
            repoInfAgrResumenEndeudamiento.Insert(listaResumenEndeudamiento);
            repoInfMicroPerfilGeneral.Insert(boCreditoVigente);
            repoInfMicroPerfilGeneral.Insert(boCreditoCerrado);
            repoInfMicroPerfilGeneral.Insert(boCreditosReestructurado);
            repoInfMicroPerfilGeneral.Insert(boCreditosRefinanciado);
            repoInfMicroPerfilGeneral.Insert(boConsultaUlt6Meses);
            repoInfMicroPerfilGeneral.Insert(boAntiguedadDesde);
            repoInfMicroVerctorSaldoMora.Insert(listaBoVectorSaldoMora);
            repoInfMicroEndeudamientoActual.Insert(listaBoEndeudamiento);
            repoInfMicroImagenTendencia.Insert(listaBoImagenTendencia);
            repoInfMicroAnalisisVector.Insert(listaBoAnalisisVector);
            repoInfMicroEvolucionDeuda.Insert(listaBoEvolucionDeuda);

            return true;
        }
    }
}
