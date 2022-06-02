﻿using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    /// BO: Marketing/AnuncioFacebookMetrica
    /// Autor: Gian Miranda
    /// Fecha: 12/06/2021
    /// <summary>
    /// BO para la logica de la metrica de los anuncios de Facebook
    /// </summary>
    public class AnuncioFacebookMetricaBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdAnuncioFacebook                   Id del anuncio de Facebook (PK de la tabla mkt.T_AnuncioFacebook)
        /// Gasto                               Gasto del anuncio
        /// IdMoneda                            Id de la moneda (PK de la tabla pla.T_Moneda)
        /// Impresiones                         Numero de impresiones del anuncio
        /// CantidadClicsUnicos                 Cantidad de clics unicos
        /// CantidadClics                       Cantidad de clics
        /// Alcance                             Cantidad de personas alcanzados
        /// FechaConsulta                       Fecha de inicio de la captura de informacion
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public int? IdAnuncioFacebook { get; set; }
        public decimal? Gasto { get; set; }
        public int? IdMoneda { get; set; }
        public int? Impresiones { get; set; }
        public int? CantidadClicsUnicos { get; set; }
        public int? CantidadClics { get; set; }
        public int? CantidadClicsEnlace { get; set; }
        public int? Alcance { get; set; }
        public DateTime? FechaConsulta { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;
        private readonly CampaniaFacebookRepositorio _repCampaniaFacebook;
        private readonly ConjuntoAnuncioFacebookRepositorio _repConjuntoAnuncioFacebook;
        private readonly AnuncioFacebookRepositorio _repAnuncioFacebook;
        private readonly AnuncioFacebookMetricaRepositorio _repAnuncioFacebookMetrica;

        public AnuncioFacebookMetricaBO()
        {
            _repCampaniaFacebook = new CampaniaFacebookRepositorio();
            _repConjuntoAnuncioFacebook = new ConjuntoAnuncioFacebookRepositorio();
            _repAnuncioFacebook = new AnuncioFacebookRepositorio();
            _repAnuncioFacebookMetrica = new AnuncioFacebookMetricaRepositorio();
        }

        public AnuncioFacebookMetricaBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repCampaniaFacebook = new CampaniaFacebookRepositorio(_integraDBContext);
            _repConjuntoAnuncioFacebook = new ConjuntoAnuncioFacebookRepositorio(_integraDBContext);
            _repAnuncioFacebook = new AnuncioFacebookRepositorio(_integraDBContext);
            _repAnuncioFacebookMetrica = new AnuncioFacebookMetricaRepositorio(_integraDBContext);
        }

        /// <summary>
        /// Actualiza la metrica en Integra, segun la DB de Facebook
        /// </summary>
        /// <param name="datosActualizar">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool ActualizarMetricaIntegra(List<AnuncioFacebookMetricaDTO> datosActualizar, string usuario)
        {
            try
            {
                FacebookBO facebook = new FacebookBO();
                List<AnuncioFacebookMetricaBO> listaAInsertar = new List<AnuncioFacebookMetricaBO>();

                bool resultadoEliminado = _repAnuncioFacebookMetrica.EliminarDatosPorFecha(DateTime.Parse(datosActualizar[0].date_start), usuario);

                bool resultadoRegularizacion = RegularizarJerarquiaFacebook(datosActualizar, usuario);

                List<AnuncioFacebookBO> anuncioFacebookAInsertar = _repAnuncioFacebook.GetBy(x => datosActualizar.Select(s => s.ad_id).Contains(x.FacebookIdAnuncio)).ToList();

                foreach (var anuncioAInsertar in datosActualizar)
                {
                    try
                    {
                        listaAInsertar.Add(MapeoAnuncioFacebookMetricaDTOBO(anuncioAInsertar, anuncioFacebookAInsertar.Where(w => w.FacebookIdAnuncio == anuncioAInsertar.ad_id).First().Id, usuario));
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }

                bool resultadoInsercion = _repAnuncioFacebookMetrica.Insert(listaAInsertar);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza la metrica en Integra, segun la DB de Facebook
        /// </summary>
        /// <param name="datosActualizar">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public string EstructurarMensajeAnuncioFacebook(string cadenaFechaInicio, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                string texto = $@"<table style='font-family:'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; border-collapse: collapse;'>
                <tr style='padding: 5px;'>
                    <td>
                        <div style = 'background-color: #224060; padding: 25px 100px;'>
                            <img src='https://integrav4.bsginstitute.com/Images/Sistema/bsginstitute.png' alt='' />
                        </div>
                    </td>
                </tr>
                <tr><td><br /></td></tr>
                <tr style='text-align: center;'>
                       <td>Ha finalizado correctamente la actualización de los datos de Facebook:</td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>FECHA ACTUALIZADA:</h3>
                        <h3>{cadenaFechaInicio}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE INICIO:</h3>
                        <h3>{fechaInicio}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE FIN:</h3>
                        <h3>{fechaFin}</h3>
                    </td>
                </tr>
            </table>";

                return texto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene y estrucutra el reporte de anuncio de Cacebook
        /// </summary>
        /// <param name="idAreaCapacitacion">Id del area de capacitacion (PK de la tabla mkt.T_AreaCapacitacion)</param>
        /// <returns>Lsita de objetos de clase ReporteAnuncioFacebookMetricaDiasDTO</returns>
        public List<ReporteAnuncioFacebookMetricaDiasDTO> EstructurarReporteAnuncioFacebook(int? idAreaCapacitacion)
        {
            try
            {
                List<ReporteAnuncioFacebookMetricaDTO> listaReporteAnuncioFacebookMetrica = _repAnuncioFacebookMetrica.ObtenerReporteAnuncioFacebookMetrica(idAreaCapacitacion);

                DateTime fechaActual = DateTime.Now.Date;
                DateTime fechaUnDia = DateTime.Now.AddDays(-1).Date;
                DateTime fechaTresDias = DateTime.Now.AddDays(-3).Date;
                DateTime fechaSieteDias = DateTime.Now.AddDays(-7).Date;

                List<int> listaIdFacebookAnuncio = listaReporteAnuncioFacebookMetrica.Select(s => s.IdFacebookAnuncio).Distinct().ToList();

                List<ReporteAnuncioFacebookMetricaDiasDTO> listaUnicaCrudaDias = new List<ReporteAnuncioFacebookMetricaDiasDTO>();

                // Actual
                var listaActual = listaReporteAnuncioFacebookMetrica
                    .Where(w => w.FechaConsulta == fechaActual)
                    .GroupBy(g => new
                    {
                        g.IdCampaniaFacebook,
                        g.IdGrupoFiltroProgramaCritico,
                        g.NombreGrupoFiltroProgramaCritico,
                        g.FacebookNombreCampania,
                        g.IdFacebookConjuntoAnuncio,
                        g.FacebookNombreConjuntoAnuncio,
                        g.PresupuestoDiarioConjuntoAnuncio,
                        g.IdFacebookAnuncio,
                        g.FacebookIdAnuncio,
                        g.FacebookNombreAnuncio
                    }).ToList();

                // Un dia
                var listaUnDia = listaReporteAnuncioFacebookMetrica
                    .Where(w => w.FechaConsulta >= fechaUnDia && w.FechaConsulta < fechaActual)
                    .GroupBy(g => new
                    {
                        g.IdCampaniaFacebook,
                        g.IdGrupoFiltroProgramaCritico,
                        g.NombreGrupoFiltroProgramaCritico,
                        g.FacebookNombreCampania,
                        g.IdFacebookConjuntoAnuncio,
                        g.FacebookNombreConjuntoAnuncio,
                        g.PresupuestoDiarioConjuntoAnuncio,
                        g.IdFacebookAnuncio,
                        g.FacebookIdAnuncio,
                        g.FacebookNombreAnuncio
                    }).ToList();

                // Tres dias
                var listaTresDias = listaReporteAnuncioFacebookMetrica
                    .Where(w => w.FechaConsulta >= fechaTresDias && w.FechaConsulta < fechaActual)
                    .GroupBy(g => new
                    {
                        g.IdCampaniaFacebook,
                        g.IdGrupoFiltroProgramaCritico,
                        g.NombreGrupoFiltroProgramaCritico,
                        g.FacebookNombreCampania,
                        g.IdFacebookConjuntoAnuncio,
                        g.FacebookNombreConjuntoAnuncio,
                        g.PresupuestoDiarioConjuntoAnuncio,
                        g.IdFacebookAnuncio,
                        g.FacebookIdAnuncio,
                        g.FacebookNombreAnuncio
                    }).ToList();

                // Siete dias
                var listaSieteDias = listaReporteAnuncioFacebookMetrica
                    .Where(w => w.FechaConsulta >= fechaSieteDias && w.FechaConsulta < fechaActual)
                    .GroupBy(g => new
                    {
                        g.IdCampaniaFacebook,
                        g.IdGrupoFiltroProgramaCritico,
                        g.NombreGrupoFiltroProgramaCritico,
                        g.FacebookNombreCampania,
                        g.IdFacebookConjuntoAnuncio,
                        g.FacebookNombreConjuntoAnuncio,
                        g.PresupuestoDiarioConjuntoAnuncio,
                        g.IdFacebookAnuncio,
                        g.FacebookIdAnuncio,
                        g.FacebookNombreAnuncio
                    }).ToList();

                foreach (int idAnuncio in listaIdFacebookAnuncio)
                {
                    ReporteAnuncioFacebookMetricaDiasDTO conjuntoAnuncio = listaReporteAnuncioFacebookMetrica
                        .Where(w => w.IdFacebookAnuncio == idAnuncio)
                        .Select(s => new ReporteAnuncioFacebookMetricaDiasDTO
                        {
                            IdCampaniaFacebook = s.IdCampaniaFacebook,
                            IdGrupoFiltroProgramaCritico = s.IdGrupoFiltroProgramaCritico,
                            NombreGrupoFiltroProgramaCritico = s.NombreGrupoFiltroProgramaCritico,
                            FacebookNombreCampania = s.FacebookNombreCampania,
                            IdFacebookConjuntoAnuncio = s.IdFacebookConjuntoAnuncio,
                            FacebookNombreConjuntoAnuncio = s.FacebookNombreConjuntoAnuncio,
                            PresupuestoDiarioConjuntoAnuncio = s.PresupuestoDiarioConjuntoAnuncio,
                            IdFacebookAnuncio = s.IdFacebookAnuncio,
                            FacebookIdAnuncio = s.FacebookIdAnuncio,
                            FacebookNombreAnuncio = s.FacebookNombreAnuncio
                        }).First();

                    List<MetricaIndividualDTO> metricaTemporal = new List<MetricaIndividualDTO>();

                    int suma = 0;
                    var resultado = new Decimal(suma);

                    metricaTemporal = listaActual
                        .Where(w => w.Key.IdFacebookAnuncio == idAnuncio)
                        .Select(s => new MetricaIndividualDTO
                        {
                            Gasto = s.Sum(op => op.Gasto),
                            Impresiones = s.Sum(op => op.Impresiones),
                            CostoPorMil = s.Sum(op => op.Impresiones) == 0 ? 0 : s.Sum(op => op.Gasto) / (s.Sum(op => op.Impresiones) * 1.0 / 1000),
                            CantidadClics = s.Sum(op => op.CantidadClics),
                            ImpresionesPorClic = s.Sum(op => op.CantidadClics) == 0 ? 0 : s.Sum(op => op.Impresiones * 1.0) / s.Sum(op => op.CantidadClics * 1.0),
                            Registros = s.Sum(op => op.Registros),
                            ClicPorRegistro = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.Registros * 1.0),
                            RegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta),
                            PorcentajeRegistrosMuyAlta = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.RegistrosMuyAlta * 1.0) / s.Sum(op => op.Registros * 1.0),
                            ClicsRegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.RegistrosMuyAlta * 1.0),
                            GastoPorRegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : s.Sum(op => op.Gasto) / s.Sum(op => op.RegistrosMuyAlta)
                        }).ToList();

                    if (!metricaTemporal.Any()) metricaTemporal.Add(new MetricaIndividualDTO());
                    conjuntoAnuncio.Actual = metricaTemporal.First();
                    metricaTemporal.Clear();

                    metricaTemporal = listaUnDia
                        .Where(w => w.Key.IdFacebookAnuncio == idAnuncio)
                        .Select(s => new MetricaIndividualDTO
                        {
                            Gasto = s.Sum(op => op.Gasto),
                            Impresiones = s.Sum(op => op.Impresiones),
                            CostoPorMil = s.Sum(op => op.Impresiones) == 0 ? 0 : s.Sum(op => op.Gasto) / (s.Sum(op => op.Impresiones) * 1.0 / 1000),
                            CantidadClics = s.Sum(op => op.CantidadClics),
                            ImpresionesPorClic = s.Sum(op => op.CantidadClics) == 0 ? 0 : s.Sum(op => op.Impresiones * 1.0) / s.Sum(op => op.CantidadClics * 1.0),
                            Registros = s.Sum(op => op.Registros),
                            ClicPorRegistro = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.Registros * 1.0),
                            RegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta),
                            PorcentajeRegistrosMuyAlta = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.RegistrosMuyAlta * 1.0) / s.Sum(op => op.Registros * 1.0),
                            ClicsRegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.RegistrosMuyAlta * 1.0),
                            GastoPorRegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : s.Sum(op => op.Gasto) / s.Sum(op => op.RegistrosMuyAlta)
                        }).ToList();

                    if (!metricaTemporal.Any()) metricaTemporal.Add(new MetricaIndividualDTO());
                    conjuntoAnuncio.UnDia = metricaTemporal.First();
                    metricaTemporal.Clear();

                    metricaTemporal = listaTresDias
                        .Where(w => w.Key.IdFacebookAnuncio == idAnuncio)
                        .Select(s => new MetricaIndividualDTO
                        {
                            Gasto = s.Sum(op => op.Gasto),
                            Impresiones = s.Sum(op => op.Impresiones),
                            CostoPorMil = s.Sum(op => op.Impresiones) == 0 ? 0 : s.Sum(op => op.Gasto) / (s.Sum(op => op.Impresiones) * 1.0 / 1000),
                            CantidadClics = s.Sum(op => op.CantidadClics),
                            ImpresionesPorClic = s.Sum(op => op.CantidadClics) == 0 ? 0 : s.Sum(op => op.Impresiones * 1.0) / s.Sum(op => op.CantidadClics * 1.0),
                            Registros = s.Sum(op => op.Registros),
                            ClicPorRegistro = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.Registros * 1.0),
                            RegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta),
                            PorcentajeRegistrosMuyAlta = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.RegistrosMuyAlta * 1.0) / s.Sum(op => op.Registros * 1.0),
                            ClicsRegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.RegistrosMuyAlta * 1.0),
                            GastoPorRegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : s.Sum(op => op.Gasto) / s.Sum(op => op.RegistrosMuyAlta)
                        }).ToList();

                    if (!metricaTemporal.Any()) metricaTemporal.Add(new MetricaIndividualDTO());
                    conjuntoAnuncio.TresDias = metricaTemporal.First();
                    metricaTemporal.Clear();

                    metricaTemporal = listaSieteDias
                        .Where(w => w.Key.IdFacebookAnuncio == idAnuncio)
                        .Select(s => new MetricaIndividualDTO
                        {
                            Gasto = s.Sum(op => op.Gasto),
                            Impresiones = s.Sum(op => op.Impresiones),
                            CostoPorMil = s.Sum(op => op.Impresiones) == 0 ? 0 : s.Sum(op => op.Gasto) / (s.Sum(op => op.Impresiones) * 1.0 / 1000),
                            CantidadClics = s.Sum(op => op.CantidadClics),
                            ImpresionesPorClic = s.Sum(op => op.CantidadClics) == 0 ? 0 : s.Sum(op => op.Impresiones * 1.0) / s.Sum(op => op.CantidadClics * 1.0),
                            Registros = s.Sum(op => op.Registros),
                            ClicPorRegistro = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.Registros * 1.0),
                            RegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta),
                            PorcentajeRegistrosMuyAlta = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.RegistrosMuyAlta * 1.0) / s.Sum(op => op.Registros * 1.0),
                            ClicsRegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.RegistrosMuyAlta * 1.0),
                            GastoPorRegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : s.Sum(op => op.Gasto) / s.Sum(op => op.RegistrosMuyAlta)
                        }).ToList();

                    if (!metricaTemporal.Any()) metricaTemporal.Add(new MetricaIndividualDTO());
                    conjuntoAnuncio.SieteDias = metricaTemporal.First();
                    metricaTemporal.Clear();

                    listaUnicaCrudaDias.Add(conjuntoAnuncio);
                }

                return listaUnicaCrudaDias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza la metrica en Integra, segun la DB de Facebook
        /// </summary>
        /// <param name="datosActualizar">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool RegularizarJerarquiaFacebook(List<AnuncioFacebookMetricaDTO> datosActualizar, string usuario)
        {
            try
            {
                FacebookBO facebook = new FacebookBO();
                List<string> campaniaFacebookIntegra = _repCampaniaFacebook.GetBy(x => datosActualizar.Select(s => s.campaign_id).Contains(x.FacebookIdCampania)).Select(ss => ss.FacebookIdCampania).ToList();
                List<FacebookIntegraIdDTO> conjuntoAnuncioFacebookIntegra = _repConjuntoAnuncioFacebook.GetBy(x => datosActualizar.Select(s => s.adset_id).Contains(x.IdAnuncioFacebook)).Select(ss => new FacebookIntegraIdDTO { IdIntegra = ss.Id, IdFacebook = ss.IdAnuncioFacebook, NombreFacebook = ss.Name, IdCampaniaFacebook = ss.IdCampaniaFacebook }).ToList();
                List<string> anuncioFacebookIntegra = _repAnuncioFacebook.GetBy(x => datosActualizar.Select(s => s.ad_id).Contains(x.FacebookIdAnuncio)).Select(ss => ss.FacebookIdAnuncio).ToList();

                List<string> facebookCampaniaFaltante = datosActualizar.Where(w => !campaniaFacebookIntegra.Contains(w.campaign_id)).Select(ss => ss.campaign_id).Distinct().ToList();
                List<string> facebookConjuntoAnuncioFaltante = datosActualizar.Where(w => !conjuntoAnuncioFacebookIntegra.Select(s => s.IdFacebook).Contains(w.adset_id)).Select(ss => ss.adset_id).Distinct().ToList();
                List<string> facebookAnuncioFaltante = datosActualizar.Where(w => !anuncioFacebookIntegra.Contains(w.ad_id)).Select(ss => ss.ad_id).Distinct().ToList();

                if (facebookCampaniaFaltante.Any())
                {
                    List<CampaniaFacebookDTO> resultadoCampaniaFacebook = facebook.DescargarCampaniayPadre(facebookCampaniaFaltante).ToList();

                    // Verificacion de conjunto anuncio Facebook
                    List<CampaniaFacebookBO> listaFacebookCampaniaRegularizado = new List<CampaniaFacebookBO>();

                    foreach (var campaniaFacebook in resultadoCampaniaFacebook)
                    {
                        listaFacebookCampaniaRegularizado.Add(MapeoCampaniaFacebookDTOBO(campaniaFacebook));
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        bool resultadoInsercion = _repCampaniaFacebook.Insert(listaFacebookCampaniaRegularizado);

                        scope.Complete();
                    }
                }

                if (facebookConjuntoAnuncioFaltante.Any())
                {
                    List<FacebookIntegraIdDTO> listaIdCampaniaFacebookIntegra = _repCampaniaFacebook.GetBy(x => datosActualizar.Select(s => s.campaign_id).Contains(x.FacebookIdCampania)).Select(ss => new FacebookIntegraIdDTO { IdIntegra = ss.Id, IdFacebook = ss.FacebookIdCampania, NombreFacebook = ss.FacebookNombreCampania }).Distinct().ToList();
                    List<ConjuntoAnuncioFacebookJerarquiaDTO> resultadoConjuntoAnuncioFacebook = facebook.DescargarConjuntoAnuncioyPadre(facebookConjuntoAnuncioFaltante).ToList();

                    // Verificacion de conjunto anuncio Facebook
                    List<ConjuntoAnuncioFacebookBO> listaFacebookConjuntoAnuncioRegularizado = new List<ConjuntoAnuncioFacebookBO>();

                    foreach (var conjuntoAnuncioFacebook in resultadoConjuntoAnuncioFacebook)
                    {
                        listaFacebookConjuntoAnuncioRegularizado.Add(MapeoConjuntoAnuncioFacebookDTOBO(conjuntoAnuncioFacebook, listaIdCampaniaFacebookIntegra.Where(w => w.IdFacebook == conjuntoAnuncioFacebook.campaign_id).First().IdIntegra, listaIdCampaniaFacebookIntegra.Where(w => w.IdFacebook == conjuntoAnuncioFacebook.campaign_id).First().NombreFacebook));
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        bool resultadoInsercion = _repConjuntoAnuncioFacebook.Insert(listaFacebookConjuntoAnuncioRegularizado);

                        scope.Complete();
                    }
                }

                // Regularizar informacion ConjuntoAnuncioFacebook
                List<ConjuntoAnuncioFacebookBO> listaConjuntoAnuncioFacebookIntegraRegularizar = _repConjuntoAnuncioFacebook.GetBy(x => datosActualizar.Select(s => s.adset_id).Contains(x.IdAnuncioFacebook)).Distinct().ToList();

                if (listaConjuntoAnuncioFacebookIntegraRegularizar.Any())
                {
                    List<FacebookIntegraIdDTO> listaIdCampaniaFacebookIntegra = _repCampaniaFacebook.GetBy(x => datosActualizar.Select(s => s.campaign_id).Contains(x.FacebookIdCampania)).Select(ss => new FacebookIntegraIdDTO { IdIntegra = ss.Id, IdFacebook = ss.FacebookIdCampania, NombreFacebook = ss.FacebookNombreCampania }).Distinct().ToList();
                    List<ConjuntoAnuncioFacebookJerarquiaDTO> resultadoConjuntoAnuncioFacebookRegularizar = facebook.DescargarConjuntoAnuncioyPadre(listaConjuntoAnuncioFacebookIntegraRegularizar.Select(s => s.IdAnuncioFacebook).ToList()).ToList();

                    foreach (var conjuntoAnuncioFacebookIntegraRegularizar in listaConjuntoAnuncioFacebookIntegraRegularizar)
                    {
                        try
                        {
                            ConjuntoAnuncioFacebookJerarquiaDTO elementoResultadoJerarquia = resultadoConjuntoAnuncioFacebookRegularizar.Where(w => w.id == conjuntoAnuncioFacebookIntegraRegularizar.IdAnuncioFacebook).First();

                            conjuntoAnuncioFacebookIntegraRegularizar.Name = elementoResultadoJerarquia.name;
                            conjuntoAnuncioFacebookIntegraRegularizar.NombreCampania = listaIdCampaniaFacebookIntegra.Where(w => w.IdFacebook == elementoResultadoJerarquia.campaign_id).First().NombreFacebook;
                            conjuntoAnuncioFacebookIntegraRegularizar.OptimizationGoal = elementoResultadoJerarquia.optimization_goal;
                            conjuntoAnuncioFacebookIntegraRegularizar.CampaignId = elementoResultadoJerarquia.campaign_id;
                            conjuntoAnuncioFacebookIntegraRegularizar.BillinEevent = elementoResultadoJerarquia.billing_event;
                            conjuntoAnuncioFacebookIntegraRegularizar.IdCampaniaFacebook = listaIdCampaniaFacebookIntegra.Where(w => w.IdFacebook == elementoResultadoJerarquia.campaign_id).First().IdIntegra;
                            conjuntoAnuncioFacebookIntegraRegularizar.Status = elementoResultadoJerarquia.status;
                            conjuntoAnuncioFacebookIntegraRegularizar.EffectiveStatus = elementoResultadoJerarquia.effective_status;
                            conjuntoAnuncioFacebookIntegraRegularizar.ConfiguredStatus = elementoResultadoJerarquia.configured_status;
                            conjuntoAnuncioFacebookIntegraRegularizar.UsuarioModificacion = usuario;
                            conjuntoAnuncioFacebookIntegraRegularizar.FechaModificacion = DateTime.Now;

                            if (!string.IsNullOrEmpty(elementoResultadoJerarquia.daily_budget)) conjuntoAnuncioFacebookIntegraRegularizar.DailyBudget = int.Parse(elementoResultadoJerarquia.daily_budget);
                            if (!string.IsNullOrEmpty(elementoResultadoJerarquia.start_time)) conjuntoAnuncioFacebookIntegraRegularizar.StartTime = DateTime.Parse(elementoResultadoJerarquia.start_time);
                            if (!string.IsNullOrEmpty(elementoResultadoJerarquia.budget_remaining)) conjuntoAnuncioFacebookIntegraRegularizar.BudgetRemaining = double.Parse(elementoResultadoJerarquia.budget_remaining);
                            if (!string.IsNullOrEmpty(elementoResultadoJerarquia.updated_time)) conjuntoAnuncioFacebookIntegraRegularizar.UpdatedTime = DateTime.Parse(elementoResultadoJerarquia.updated_time);
                            if (!string.IsNullOrEmpty(elementoResultadoJerarquia.created_time)) conjuntoAnuncioFacebookIntegraRegularizar.CreatedTime = DateTime.Parse(elementoResultadoJerarquia.created_time);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    using (TransactionScope scope = new TransactionScope())
                    {
                        bool resultadoActualizacion = _repConjuntoAnuncioFacebook.Update(listaConjuntoAnuncioFacebookIntegraRegularizar);

                        scope.Complete();
                    }
                }

                if (facebookAnuncioFaltante.Any())
                {
                    List<FacebookIntegraIdDTO> listaIdConjuntoAnuncioFacebookIntegra = _repConjuntoAnuncioFacebook.GetBy(x => datosActualizar.Select(s => s.adset_id).Contains(x.IdAnuncioFacebook)).Select(ss => new FacebookIntegraIdDTO { IdIntegra = ss.Id, IdFacebook = ss.IdAnuncioFacebook, NombreFacebook = ss.Name }).ToList();
                    List<AnuncioFacebookDTO> resultadoAnuncioFacebook = facebook.DescargarAnuncioyPadre(facebookAnuncioFaltante).ToList();

                    // Verificacion de anuncio Facebook
                    List<AnuncioFacebookBO> listaFacebookAnuncioRegularizado = new List<AnuncioFacebookBO>();

                    foreach (var anuncioFacebook in resultadoAnuncioFacebook)
                    {
                        listaFacebookAnuncioRegularizado.Add(MapeoAnuncioFacebookDTOBO(anuncioFacebook, listaIdConjuntoAnuncioFacebookIntegra.Where(w => w.IdFacebook == anuncioFacebook.adset_id).First().IdIntegra));
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        bool resultadoInsercion = _repAnuncioFacebook.Insert(listaFacebookAnuncioRegularizado);

                        scope.Complete();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mapeo de DTO a BO para campania Facebook
        /// </summary>
        /// <param name="campaniaFacebook">Objeto de clase CampaniaFacebookDTO</param>
        /// <returns>Objeto de clase CampaniaFacebookBO</returns>
        public CampaniaFacebookBO MapeoCampaniaFacebookDTOBO(CampaniaFacebookDTO campaniaFacebook)
        {
            try
            {
                CampaniaFacebookBO campaniaFacebookBO = new CampaniaFacebookBO
                {
                    FacebookIdCampania = campaniaFacebook.id,
                    FacebookNombreCampania = campaniaFacebook.name,
                    FacebookIdCuenta = campaniaFacebook.account_id,
                    Estado = true,
                    UsuarioCreacion = "Regularizacion metrica",
                    UsuarioModificacion = "Regularizacion metrica",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                return campaniaFacebookBO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mapeo de DTO a BO para conjunto anuncio Facebook
        /// </summary>
        /// <param name="conjuntoAnuncioFacebook">Objeto de clase ConjuntoAnuncioFacebookJerarquiaDTO</param>
        /// <param name="idCampaniaFacebook">Id de la campania Facebook (PK de la tabla mkt.T_CampaniaFacebook)</param>
        /// <param name="nombreCampaniaFacebook">Nombre de la campania Facebook</param>
        /// <returns>Objeto de clase CampaniaFacebookBO</returns>
        public ConjuntoAnuncioFacebookBO MapeoConjuntoAnuncioFacebookDTOBO(ConjuntoAnuncioFacebookJerarquiaDTO conjuntoAnuncioFacebook, int idCampaniaFacebook, string nombreCampaniaFacebook)
        {
            try
            {
                ConjuntoAnuncioFacebookBO conjuntoAnuncio = new ConjuntoAnuncioFacebookBO
                {
                    IdAnuncioFacebook = conjuntoAnuncioFacebook.id,
                    Name = conjuntoAnuncioFacebook.name,
                    NombreCampania = nombreCampaniaFacebook,
                    OptimizationGoal = conjuntoAnuncioFacebook.optimization_goal,
                    CampaignId = conjuntoAnuncioFacebook.campaign_id,
                    BillinEevent = conjuntoAnuncioFacebook.billing_event,
                    IdCampaniaFacebook = idCampaniaFacebook,
                    Status = conjuntoAnuncioFacebook.status,
                    EffectiveStatus = conjuntoAnuncioFacebook.effective_status,
                    ConfiguredStatus = conjuntoAnuncioFacebook.configured_status,
                    Estado = true,
                    UsuarioCreacion = "Regularizacion metrica",
                    UsuarioModificacion = "Regularizacion metrica",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                if (!string.IsNullOrEmpty(conjuntoAnuncioFacebook.daily_budget)) conjuntoAnuncio.DailyBudget = int.Parse(conjuntoAnuncioFacebook.daily_budget);
                if (!string.IsNullOrEmpty(conjuntoAnuncioFacebook.start_time)) conjuntoAnuncio.StartTime = DateTime.Parse(conjuntoAnuncioFacebook.start_time);
                if (!string.IsNullOrEmpty(conjuntoAnuncioFacebook.budget_remaining)) conjuntoAnuncio.BudgetRemaining = double.Parse(conjuntoAnuncioFacebook.budget_remaining);
                if (!string.IsNullOrEmpty(conjuntoAnuncioFacebook.updated_time)) conjuntoAnuncio.UpdatedTime = DateTime.Parse(conjuntoAnuncioFacebook.updated_time);
                if (!string.IsNullOrEmpty(conjuntoAnuncioFacebook.created_time)) conjuntoAnuncio.CreatedTime = DateTime.Parse(conjuntoAnuncioFacebook.created_time);

                return conjuntoAnuncio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mapeo de DTO a BO para anuncio Facebook metrica
        /// </summary>
        /// <param name="anuncioFacebook">Objeto de clase AnuncioFacebookDTO</param>
        /// <param name="idConjuntoAnuncioFacebook">Id del conjunto anuncio de Facebook (PK de la tabla mkt.T_ConjuntoAnuncioFacebook)</param>
        /// <returns>Objeto de clase AnuncioFacebookMetricaBO</returns>
        public AnuncioFacebookBO MapeoAnuncioFacebookDTOBO(AnuncioFacebookDTO anuncioFacebook, int idConjuntoAnuncioFacebook)
        {
            try
            {
                AnuncioFacebookBO anuncioFacebookBO = new AnuncioFacebookBO
                {
                    FacebookIdAnuncio = anuncioFacebook.id,
                    FacebookNombreAnuncio = anuncioFacebook.name,
                    FacebookIdConjuntoAnuncio = anuncioFacebook.adset_id,
                    IdConjuntoAnuncioFacebook = idConjuntoAnuncioFacebook,
                    Estado = true,
                    UsuarioCreacion = "Regularizacion metrica",
                    UsuarioModificacion = "Regularizacion metrica",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                return anuncioFacebookBO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mapeo de DTO a BO para anuncio Facebook metrica
        /// </summary>
        /// <param name="anuncioFacebookMetrica">Objeto de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="idAnuncioFacebook">Id del anuncio de Facebook (PK de la tabla mkt.T_AnuncioFacebook)</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>Objeto de clase AnuncioFacebookMetricaBO</returns>
        public AnuncioFacebookMetricaBO MapeoAnuncioFacebookMetricaDTOBO(AnuncioFacebookMetricaDTO anuncioFacebookMetrica, int idAnuncioFacebook, string usuario)
        {
            try
            {
                CultureInfo formatoDecimal = new System.Globalization.CultureInfo("en-US");

                AnuncioFacebookMetricaBO anuncioFacebookMetricaBO = new AnuncioFacebookMetricaBO()
                {
                    IdAnuncioFacebook = idAnuncioFacebook,
                    Gasto = decimal.Parse(anuncioFacebookMetrica.spend, formatoDecimal),
                    IdMoneda = ValorEstatico.IdMonedaDolares,
                    Impresiones = int.Parse(anuncioFacebookMetrica.impressions),
                    CantidadClicsUnicos = int.Parse(anuncioFacebookMetrica.unique_clicks),
                    CantidadClics = int.Parse(anuncioFacebookMetrica.clicks),
                    CantidadClicsEnlace = int.Parse(anuncioFacebookMetrica.inline_link_clicks),
                    Alcance = int.Parse(anuncioFacebookMetrica.reach),
                    FechaConsulta = DateTime.Parse(anuncioFacebookMetrica.date_start),
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                return anuncioFacebookMetricaBO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
