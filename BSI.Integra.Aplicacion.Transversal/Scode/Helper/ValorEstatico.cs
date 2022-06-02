using BSI.Integra.Aplicacion.Base.DTO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BSI.Integra.Aplicacion.Transversal.Helper
{
    public class ValorEstatico
    {
        private static readonly Lazy<ValorEstatico> instance = new Lazy<ValorEstatico>(() => new ValorEstatico());
        public static ValorEstatico Instance
        {
            get
            {
                return instance.Value;
            }
        }
        private static ConfiguracionFijaRepositorio _repoError;
        private ValorEstatico()
        {
            _repoError = new ConfiguracionFijaRepositorio();
            var fijo = _repoError.ObtenerTodosLosRegistros();
            LlenarAtributos(fijo);
        }
        private void LlenarAtributos(List<ValorEstaticoDTO> valores)
        {
            foreach (var valor in valores)
            {

                var propiedad = this.GetType().GetProperty(valor.NombreAtributo);

                if (propiedad != null)
                {
                    switch (valor.TipoDato)
                    {
                        case "int":
                            propiedad.SetValue(null, Convert.ToInt32(valor.Valor));
                            break;
                        case "string":
                            propiedad.SetValue(null, valor.Valor);
                            break;
                        case "bool":
                            propiedad.SetValue(null, Convert.ToBoolean(valor.Valor));
                            break;
                    }
                }

            }
        }

        #region HardCodeoAtributos
        public static string ParametroPrueba { get; private set; }
        public static int IdEstadoOportunidadNoProgramada { get; private set; }
        public static int IdEstadoOportunidadProgramada { get; private set; }
        public static int IdFaseOportunidadRN3 { get; private set; }
        public static int IdFaseOportunidadE { get; private set; }
        public static int IdFaseOportunidadOD { get; private set; }
        public static int IdFaseOportunidadOM { get; private set; }
        public static int IdPersonalAsignacionAutomatica { get; private set; }
        public static int IdEstadoActividadDetalleNoEjecutado { get; private set; }
        public static int IdEstadoActividadDetalleEjecutado { get; private set; }
        public static int IdEstadoOportunidadReasignacionVentaCruzada { get; private set; }
        public static int IdProbabilidadRegistroSinProbabilidad { get; private set; }
        public static int IdFacebookFormulario5Campos { get; private set; }
        public static int IdFacebookFormulario3Campos { get; private set; }
        public static int IdFacebookMultipleFormulario { get; private set; }
        public static int IdFacebookRemarketingFormulario { get; private set; }
        public static int IdTipoInteraccionPaso1 { get; private set; }
        public static int IdTipoDatoLanzamiento { get; private set; }
        public static int IdEstadoOportunidadEjecutada { get; private set; }
        public static int IdFaseOportunidadIS { get; private set; }
        public static int IdFaseOportunidadRN2 { get; private set; }
        public static int IdEstadoOcurrenciaNoEjecutado { get; private set; }
        public static int IdEstadoOcurrenciaEjecutado { get; private set; }
        public static int IdFaseOportunidadNulo { get; private set; }
        public static int IdFaseOportunidadBIC { get; private set; }
        public static int IdCronogramaTipoModificacionCuota { get; private set; }
        public static int IdEstadoPagoMatriculaPorMatricular { get; private set; }
        public static int IdOcurrenciaConfirmaPagoIs { get; private set; }
        public static int IdOcurrenciaIsSinLlamada { get; private set; }//descomentar para is sin llamada
        public static int IdEstadoOcurrenciaAsignacionManual { get; private set; }
        public static int IdEstadoMatriculaRegular { get; private set; }
        public static int IdEstadoMatriculaBeca { get; private set; }
        public static int IdEstadoMatriculaReincorporado { get; private set; }
        public static int IdEstadoPagoMatriculaMatriculado { get; private set; }
        public static int IdRolJefePlanificacion { get; private set; }
        public static int IdRolJefaturaFinanzas { get; private set; }
        public static int IdFurAprobadoPorJefeArea { get; private set; }
        public static int IdFurObservadoPorJefeArea { get; private set; }
        public static int IdFurObservadoPorJefeFinanzas { get; private set; }
        public static int IdFurAprobadoPorJefeFinanzas { get; private set; }
        public static int IdFurEstadoPorAprobar { get; private set; }
        public static int IdFurTodosPorEliminar { get; private set; }
        public static int IdFurProyectado { get; private set; }
        public static int IdMisFurPorEliminar { get; private set; }
        public static int IdRolCoordinadoraFinanzas { get; private set; }
        public static int IdRolAsistenteAdministracionFinanzas { get; private set; }

        public static int IdFaseOportunidadBNC { get; private set; }
        public static int IdFaseOportunidadIC { get; private set; }
        public static int IdFaseOportunidadIP { get; private set; }
        public static int IdFaseOportunidadIT { get; private set; }
        public static int IdFaseOportunidadPF { get; private set; }
        public static int IdFaseOportunidadRN { get; private set; }
        public static int IdTipoDatoHistorico { get; private set; }

        public static int IdActividadCabeceraLlamadaConfirmacionPago { get; private set; }
        public static int IdActividadCabeceraLlamadaCierre { get; private set; }
        public static int IdActividadCabeceraLlamadaConfirmacionRegistroPW { get; private set; }
        public static int IdActividadCabeceraLlamadaContactoInicial { get; private set; }
        public static int IdActividadCabeceraLlamadaConfirmacionRevisionInfo { get; private set; }
        public static int IdActividadCabeceraLlamadaConfirInteresProHis { get; private set; }
        public static int IdActividadCabeceraLlamadaConfirInteresProgPrelan { get; private set; }
        public static int IdActividadCabeceraLlamadaConfirSeguimientoRN { get; private set; }
        public static int IdActividadCabeceraLlamadaConfirEnvioDoc { get; private set; }
        public static int IdActividadCabeceraPrimerContactoClienteProbMedia { get; private set; }
        public static int IdActividadCabeceraLlamadaSeguimiento { get; private set; }
        public static int IdCategoriaOrigenFacebookPreLanC2FormularioPropio { get; private set; }
        public static int IdOcurrenciaCerradoReporteBic { get; private set; }
        public static int IdTipoCategoriaOrigenChat { get; private set; }
        public static int IdProgramaGeneralDSIG { get; private set; }
        public static int IdFaseOportunidadBNCRN2 { get; private set; }
        public static int IdActividadCabeceraLlamadaSeguimientoRN2 { get; private set; }
        public static int IdFormaPagoEnEfectivo { get; private set; }
        public static int IdMonedaDolares { get; private set; }
        public static int IdProgramaGeneralFFISO27001 { get; private set; }
        public static int IdProgramaGeneralFFISO9001 { get; private set; }
        public static int IdProgramaGeneralFFISO45001 { get; private set; }
        public static int IdProgramaGeneralFFISO37001 { get; private set; }
        public static int IdOcurrenciaReprogramacionAutomaticaRN2 { get; private set; }
        public static int IdOcurrenciaCierreRN8 { get; private set; }
        public static int IdCategoriaObjetoFiltroArea { get; private set; }
        public static int IdCategoriaObjetoFiltroSubArea { get; private set; }
        public static int IdCategoriaObjetoFiltroProgramaGeneral { get; private set; }
        public static int IdCategoriaObjetoFiltroProgramaEspecifico { get; private set; }
        public static int IdCategoriaObjetoFiltroTipoCategoriaOrigen { get; private set; }
        public static int IdCategoriaObjetoFiltroCategoriaOrigen { get; private set; }
        public static int IdCategoriaObjetoFiltroAreaTrabajo { get; private set; }
        public static int IdCategoriaObjetoFiltroAreaFormacion { get; private set; }
        public static int IdCategoriaObjetoFiltroIndustria { get; private set; }
        public static int IdCategoriaObjetoFiltroCargo { get; private set; }
        public static int IdCategoriaObjetoFiltroPais { get; private set; }
        public static int IdCategoriaObjetoFiltroCiudad { get; private set; }
        public static int IdCategoriaObjetoFiltroOpoInicialFaseActual { get; private set; }
        public static int IdCategoriaObjetoFiltroOpoInicialFaseMaxima { get; private set; }
        public static int IdCategoriaObjetoFiltroUltimaOpoFaseActual { get; private set; }
        public static int IdCategoriaObjetoFiltroUltimaOpoFaseMaxima { get; private set; }
        public static int IdCategoriaOrigenFacebookCorreo { get; private set; }
        public static int IdPersonalAsesorAsignacionHistorico { get; private set; }
        public static int IdTipoInteraccionFormularioEnviadoCompleto { get; private set; }
        public static int IdCategoriaOrigenFacebookComentarios { get; private set; }
        public static int IdCategoriaOrigenFacebookInbox { get; private set; }

        public static int IdTipoInteraccionCorreoElectronicoRecibido { get; private set; }
        public static int IdTipoInteraccionGeneralFormulario { get; private set; }
        public static int IdCategoriaObjetoFiltroTipoFormulario { get; private set; }
        public static int IdCategoriaObjetoFiltroTipoInteraccionFormulario { get; private set; }

        public static int IdTipoRemuneracionSueldo { get; private set; }
        public static int IdTipoRemuneracionGratificacion { get; private set; }
        public static int IdTipoRemuneracionCTS { get; private set; }
        public static int IdTipoRemuneracionBono { get; private set; }
        public static int IdAreaTrabajoGestionPersonas { get; private set; }
        public static string AreaTrabajoGestionPersonas { get; private set; }
        public static string AreaTrabajoOperaciones { get; private set; }
        public static string AreaTrabajoVentas { get; private set; }
        public static int IdSedeTrabajoArequipa { get; private set; }
        public static int IdSedeTrabajoLima { get; private set; }
        public static int IdSedeTrabajoBogota { get; private set; }
        public static int IdSedeTrabajoSantaCruz { get; private set; }
        public static int IdTipoPedidoGastoInmediato { get; private set; }
        public static int IdTipoPedidoACredito { get; private set; }
        public static int IdProductoPlanillaVentas { get; private set; }
        public static int IdProductoPlanillaProduccion { get; private set; }
        public static int IdProductoPlanillaAdministracion { get; private set; }
        public static int IdProductoCTSAdministracion { get; private set; }
        public static int IdProductoCtsProduccion { get; private set; }
        public static int IdProductoCtsVentas { get; private set; }
        public static int IdProductoGratificacionAdministracion { get; private set; }
        public static int IdProductoGratificacionProduccion { get; private set; }
        public static int IdProductoGratificacionVentas { get; private set; }
        public static int IdProductoComisionAdministracion { get; private set; }
        public static int IdProductoComisionProduccion { get; private set; }
        public static int IdProductoComisionVentas { get; private set; }
        public static int IdCentroCostoPersonal { get; private set; }
        public static int IdProveedorPersonal { get; private set; }
        public static int IdProductoBonoProductividad { get; private set; }
        public static int IdCentroCostoPersonalLima { get; private set; }
        public static int IdProductoEsSalud { get; private set; }
        public static int IdProveedorSeguroSocialDeSalud { get; private set; }
        public static int IdCiudadArequipa { get; private set; }
        public static int IdCiudadLima { get; private set; }
        public static int IdCiudadSantaCruz { get; private set; }
        public static int IdCiudadBogota { get; private set; }
        public static int IdFurAprobadoNoEjecutado { get; private set; }

        public static int IdCentroCostoRegistro2020ILima { get; private set; }

        public static int IdModuloCrearOportunidadesWhatsApp { get; private set; }
        public static int IdModuloSistemaWhatsAppMailing { get; private set; }
        public static int IdCategoriaObjetoFiltroActividadesLlamada { get; private set; }
        public static int IdCategoriaObjetoFiltroProbabilidadOportunidad { get; private set; }

        public static int IdCategoriaObjetoFiltroVCArea { get; private set; }
        public static int IdCategoriaObjetoFiltroVCSubArea { get; private set; }
        public static int IdCategoriaObjetoFiltroVCPGeneral { get; private set; }
        public static int IdCategoriaObjetoFiltroProbabilidadVentaCruzada { get; private set; }
        //public static int IdCategoriaObjetoFiltroPGeneralPrincipalExcluirCorreo { get; private set; }
        public static int IdCategoriaObjetoFiltroPGeneralPrincipalExcluir { get; private set; }
        public static int IdCategoriaObjetoFiltroCampaniaMailing { get; private set; }
        public static int IdCategoriaObjetoFiltroConjuntoLista { get; private set; }
        public static int IdCategoriaObjetoFiltroFiltroSegmento { get; private set; }
        public static int IdCategoriaObjetoFiltroActividadCabecera { get; private set; }
        public static int IdCategoriaObjetoFiltroOcurrencia { get; private set; }
        public static int IdCategoriaObjetoFiltroDocumentoAlumno { get; private set; }
        public static int IdCategoriaObjetoFiltroEstadoMatricula { get; private set; }
        public static int IdCategoriaObjetoFiltroSubEstadoMatricula { get; private set; }
        public static int IdCategoriaObjetoFiltroModalidadCurso { get; private set; }

        public static int IdCategoriaObjetoFiltroSesion { get; private set; }
        public static int IdCategoriaObjetoFiltroEstadoAcademico { get; private set; }
        public static int IdCategoriaObjetoFiltroEstadoPago { get; private set; }
        public static int IdCategoriaObjetoFiltroPorcentajeAvance { get; private set; }
        public static int IdCategoriaObjetoFiltroEstadoLlamada { get; private set; }

        public static int IdCategoriaObjetoFiltroSesionWebinar { get; private set; }
        public static int IdCategoriaObjetoFiltroTrabajoAlumno { get; private set; }
        public static int IdCategoriaObjetoFiltroTrabajoAlumnoFinal { get; private set; }
        public static int IdCategoriaObjetoFiltroTarifario { get; private set; }
        public static int IdPersonalAreaTrabajoVentas { get; private set; }
        public static int IdPersonalAreaTrabajoOperaciones { get; private set; }

        public static int IdCategoriaObjetoFiltroFaseOportunidadActual { get; private set; }

        public static int IdPlantillaBaseEmail { get; private set; }
        public static int IdPlantillaBaseWhatsAppFacebook { get; private set; }
        public static int IdPlantillaBaseWhatsAppPropio { get; private set; }
        public static int IdPlantillaBaseMensajeTexto { get; private set; }

        public static int IdEstadoContactoWhatsAppValido { get; private set; }
        public static int IdEstadoContactoWhatsAppInvalido { get; private set; }
        public static int IdEstadoContactoWhatsAppSinValidar { get; private set; }

        public static int IdModalidadCursoPresencial { get; private set; }
        public static int IdModalidadCursoOnlineAsincronica { get; private set; }
        public static int IdModalidadCursoOnlineSincronica { get; private set; }

        public static int IdPlantillaBienvenidaAlumnoPresencialOnline { get; private set; }
        public static int IdPlantillaBienvenidaAlumnoAOnline { get; private set; }

        public static int IdPlantillaInformacionCursoAutomatico { get; private set; }
        public static int IdPlantillaInformacionCursoVentas { get; private set; }

        public static int IdPlantillaMaestroTemplateV2 { get; private set; }
        public static int IdPlantillaDocenteProyectoPendienteCalificar { get; private set; }
        public static int IdPlantillaAlumnoProyectoCalificado { get; private set; }

        public static int IdPersonalCorreoPorDefecto { get; private set; }
        public static int IdPlantillaEnvioCorreoInformacion { get; private set; }
        public static int IdPersonalWhatsappNuevasOportunidades { get; private set; }
        public static int IdPlantillaInformacionCarrera { get; private set; }
        public static int IdWhatsAppMultipleSubCategoriaDato { get; private set; }

        public static int IdPlantillaAccesoTemporalMailing { get; private set; }
        public static int IdPlantillaAccesoTemporalWhatsApp { get; private set; }
        public static int IdPlantillaAccesoTemporalPersonalMailing { get; private set; }

        public static int IdMailingGeneralDefecto { get; private set; }

        public static int IdListaCursoAreaEtiquetaTi1 { get; private set; }

        public static int IdEstadoEnvioArchivadoCorrecto { get; private set; }
        public static int IdEstadoEnvioArchivadoIncorrecto { get; private set; }
        public static int IdEstadoEnvioEnProceso { get; private set; }

        public static int IdAutenticacionFacebookLeadsReportes { get; private set; }

        public static int IdUsernameMailChimpAPIMarketing { get; private set; }
        public static int IdTokenMailChimpAPIMarketing { get; private set; }

        public static int IdMailingBasesPropiasChat { get; private set; }
        public static int IdMailingBasesPropiasChatOffline { get; private set; }
        public static int IdMailingBasesPropiasFormularioAccesoPrueba { get; private set; }
        public static int IdMailingBasesPropiasFormularioCarrera { get; private set; }
        public static int IdMailingBasesPropiasFormularioContactenos { get; private set; }
        public static int IdMailingBasesPropiasFormularioPago { get; private set; }
        public static int IdMailingBasesPropiasFormularioPrograma { get; private set; }
        public static int IdMailingBasesPropiasFormularioPropio { get; private set; }
        public static int IdMailingBasesPropiasFormularioRegistrarse { get; private set; }
        public static int IdMailingBasesPropiasInteligenteChat { get; private set; }
        public static int IdMailingBasesPropiasInteligenteChatOffline { get; private set; }
        public static int IdMailingBasesPropiasIntFormularioAccesoPrueba { get; private set; }
        public static int IdMailingBasesPropiasInteligenteFormularioCarrera { get; private set; }
        public static int IdMailingBasesPropiasIntFormularioContactenos { get; private set; }
        public static int IdMailingBasesPropiasInteligenteFormularioPago { get; private set; }
        public static int IdMailingBasesPropiasInteligenteFormularioPrograma { get; private set; }
        public static int IdMailingBasesPropiasInteligenteFormularioPropio { get; private set; }
        public static int IdMailingBasesPropiasIntFormularioRegistrarse { get; private set; }

        public static int IdWhatsAppEstadoValidacionFallido { get; private set; }
        public static int IdEstadoSeguimientoPreProcesoListaWhatsAppSinDatos { get; private set; }

        public static int IdRecordatorioSms01Maniana { get; private set; }
        public static int IdRecordatorioSms01Tarde { get; private set; }
        public static int IdRecordatorioSms02 { get; private set; }
        public static int IdRecordatorioSms03 { get; private set; }
        public static int IdRecordatorioSms04 { get; private set; }

        public static int IdPaisPeru { get; private set; }
        public static int IdPaisColombia { get; private set; }
        public static int IdPaisBolivia { get; private set; }
        public static int IdPaisMexico { get; private set; }

        public static int IdOrigenCorreoElectronico { get; private set; }

        #endregion  HardCodeoAtributos

        public static Dictionary<string, string> GetProperties()
        {
            var listado = new Dictionary<string, string>();
            Type type = typeof(ValorEstatico);
            foreach (var p in type.GetFields(BindingFlags.Static | BindingFlags.NonPublic))
            {
                listado.Add(p.Name, p.GetValue(null).ToString());
            }
            return listado;
        }
    }
}
