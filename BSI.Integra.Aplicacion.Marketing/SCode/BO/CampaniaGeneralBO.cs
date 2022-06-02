using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.SCode.Repository;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class CampaniaGeneralBO : BaseBO
    {
        public string Nombre { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int? IdCategoriaObjetoFiltro { get; set; }
        public int? IdHoraEnvioMailing { get; set; }
        public int? IdTipoAsociacion { get; set; }
        public int? IdProbabilidadRegistroPw { get; set; }
        public int? NroMaximoSegmentos { get; set; }
        public int? CantidadPeriodoSinCorreo { get; set; }
        public int? IdTiempoFrecuencia { get; set; }
        public int? IdFiltroSegmento { get; set; }
        public int? IdPlantillaMailing { get; set; }
        public int? IdRemitenteMailing { get; set; }
        public bool? IncluyeWhatsapp { get; set; }
        public DateTime? FechaInicioEnvioWhatsapp { get; set; }
        public DateTime? FechaFinEnvioWhatsapp { get; set; }
        public int? NumeroMinutosPrimerEnvio { get; set; }
        public int? IdHoraEnvioWhatsapp { get; set; }
        public int? DiasSinWhatsapp { get; set; }
        public int? IdPlantillaWhatsapp { get; set; }
        public bool? IncluirRebotes { get; set; }
        public int IdEstadoEnvioMailing { get; set; }
        public int IdEstadoEnvioWhatsapp { get; set; }


        public List<CampaniaGeneralDetalleBO> listaCampaniaGeneralDetalleBO;

        private CampaniaGeneralRepositorio _repCampaniaGeneral;
        private CampaniaGeneralDetalleRepositorio _repCampaniaGeneralDetalle;
        private CampaniaGeneralDetalleProgramaRepositorio _repCampaniaGeneralDetallePrograma;
        private ConjuntoAnuncioRepositorio _repConjuntoAnuncio;
        private CampaniaGeneralDetalleAreaRepositorio _repAreaCampaniaGeneralDetalle;
        private CampaniaGeneralDetalleSubAreaRepositorio _repSubAreaCampaniaGeneralDetalle;
        private CampaniaGeneralDetalleResponsableRepositorio _repCampaniaGeneralDetalleResponsable;
        private PrioridadMailChimpListaRepositorio _repPrioridadMailChimpLista;

        public CampaniaGeneralBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();

            _repCampaniaGeneral = new CampaniaGeneralRepositorio();
            _repCampaniaGeneralDetalle = new CampaniaGeneralDetalleRepositorio();
            _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio();
            _repCampaniaGeneralDetallePrograma = new CampaniaGeneralDetalleProgramaRepositorio();
            _repAreaCampaniaGeneralDetalle = new CampaniaGeneralDetalleAreaRepositorio();
            _repSubAreaCampaniaGeneralDetalle = new CampaniaGeneralDetalleSubAreaRepositorio();
            _repCampaniaGeneralDetalleResponsable = new CampaniaGeneralDetalleResponsableRepositorio();
            _repPrioridadMailChimpLista = new PrioridadMailChimpListaRepositorio();
        }

        public CampaniaGeneralBO(integraDBContext contexto)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repCampaniaGeneral = new CampaniaGeneralRepositorio(contexto);
            _repCampaniaGeneralDetalle = new CampaniaGeneralDetalleRepositorio(contexto);
            _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio(contexto);
            _repCampaniaGeneralDetallePrograma = new CampaniaGeneralDetalleProgramaRepositorio(contexto);
            _repAreaCampaniaGeneralDetalle = new CampaniaGeneralDetalleAreaRepositorio(contexto);
            _repSubAreaCampaniaGeneralDetalle = new CampaniaGeneralDetalleSubAreaRepositorio(contexto);
            _repCampaniaGeneralDetalleResponsable = new CampaniaGeneralDetalleResponsableRepositorio(contexto);
            _repPrioridadMailChimpLista = new PrioridadMailChimpListaRepositorio(contexto);
        }

        /// <summary>
        /// Inserta o Actualiza un registro a la tabla T_CampaniaGeneral
        /// </summary>
        /// <param name="campaniaGeneralDTO">Objeto de clase CampaniaGeneralDTO</param>
        public void InsertarOActualizarCampaniaGeneral(CampaniaGeneralDTO campaniaGeneralDTO)
        {
            if (campaniaGeneralDTO.Id != 0)
            {
                var campaniaGeneral = _repCampaniaGeneral.FirstById(campaniaGeneralDTO.Id);

                if (campaniaGeneral != null)
                {
                    this.Id = campaniaGeneralDTO.Id;
                    this.Nombre = campaniaGeneralDTO.Nombre;
                    this.IdCategoriaOrigen = campaniaGeneralDTO.IdCategoriaOrigen;
                    this.FechaEnvio = campaniaGeneralDTO.FechaEnvio;
                    this.IdCategoriaObjetoFiltro = campaniaGeneralDTO.IdNivelSegmentacion;
                    this.IdHoraEnvioMailing = campaniaGeneralDTO.IdHoraEnvio_Mailing;
                    this.IdTipoAsociacion = campaniaGeneralDTO.IdTipoAsociacion;
                    this.IdProbabilidadRegistroPw = campaniaGeneralDTO.IdProbabilidadRegistro_Nivel;
                    this.NroMaximoSegmentos = campaniaGeneralDTO.NroMaximoSegmentos;
                    this.CantidadPeriodoSinCorreo = campaniaGeneralDTO.CantidadPeriodoSinCorreo;
                    this.IdTiempoFrecuencia = campaniaGeneralDTO.IdTiempoFrecuencia;
                    this.IdFiltroSegmento = campaniaGeneralDTO.IdFiltroSegmento;
                    this.IdPlantillaMailing = campaniaGeneralDTO.IdPlantilla_Mailing;
                    this.IdRemitenteMailing = campaniaGeneralDTO.IdRemitenteMailing;
                    this.IncluyeWhatsapp = campaniaGeneralDTO.IncluyeWhatsapp;
                    this.FechaInicioEnvioWhatsapp = campaniaGeneralDTO.FechaInicioEnvioWhatsapp;
                    this.FechaFinEnvioWhatsapp = campaniaGeneralDTO.FechaFinEnvioWhatsapp;
                    this.NumeroMinutosPrimerEnvio = campaniaGeneralDTO.NumeroMinutosPrimerEnvio;
                    this.IdHoraEnvioWhatsapp = campaniaGeneralDTO.IdHoraEnvio_Whatsapp;
                    this.DiasSinWhatsapp = campaniaGeneralDTO.DiasSinWhatsapp;
                    this.IdPlantillaWhatsapp = campaniaGeneralDTO.IdPlantilla_Whatsapp;
                    this.IncluirRebotes = campaniaGeneral.IncluirRebotes;
                    this.IdEstadoEnvioMailing = campaniaGeneral.IdEstadoEnvioMailing;
                    this.IdEstadoEnvioWhatsapp = campaniaGeneral.IdEstadoEnvioWhatsapp;
                    this.Estado = campaniaGeneral.Estado;
                    this.FechaCreacion = campaniaGeneral.FechaCreacion;
                    this.FechaModificacion = DateTime.Now;
                    this.UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion;
                    this.UsuarioCreacion = campaniaGeneral.UsuarioCreacion;
                    this.RowVersion = campaniaGeneral.RowVersion;
                }
                var campaniaGeneralDetalleBO = _repCampaniaGeneralDetalle.GetBy(x => x.IdCampaniaGeneral == campaniaGeneralDTO.Id).ToList();

                campaniaGeneralDetalleBO.RemoveAll(x => campaniaGeneralDTO.ListaPrioridades.Any(y => y.Id == x.Id));
                if (campaniaGeneralDetalleBO != null)
                {
                    _repCampaniaGeneralDetalle.Delete(campaniaGeneralDetalleBO.Select(x => x.Id), campaniaGeneralDTO.UsuarioModificacion);
                }
            }
            else
            {
                this.Id = campaniaGeneralDTO.Id;
                this.Nombre = campaniaGeneralDTO.Nombre;
                this.IdCategoriaOrigen = campaniaGeneralDTO.IdCategoriaOrigen;
                this.FechaEnvio = campaniaGeneralDTO.FechaEnvio;
                this.IdCategoriaObjetoFiltro = campaniaGeneralDTO.IdNivelSegmentacion;
                this.IdHoraEnvioMailing = campaniaGeneralDTO.IdHoraEnvio_Mailing;
                this.IdTipoAsociacion = campaniaGeneralDTO.IdTipoAsociacion;
                this.IdProbabilidadRegistroPw = campaniaGeneralDTO.IdProbabilidadRegistro_Nivel;
                this.NroMaximoSegmentos = campaniaGeneralDTO.NroMaximoSegmentos;
                this.CantidadPeriodoSinCorreo = campaniaGeneralDTO.CantidadPeriodoSinCorreo;
                this.IdTiempoFrecuencia = campaniaGeneralDTO.IdTiempoFrecuencia;
                this.IdFiltroSegmento = campaniaGeneralDTO.IdFiltroSegmento;
                this.IdPlantillaMailing = campaniaGeneralDTO.IdPlantilla_Mailing;
                this.IdRemitenteMailing = campaniaGeneralDTO.IdRemitenteMailing;
                this.IncluyeWhatsapp = campaniaGeneralDTO.IncluyeWhatsapp;
                this.FechaInicioEnvioWhatsapp = campaniaGeneralDTO.FechaInicioEnvioWhatsapp;
                this.FechaFinEnvioWhatsapp = campaniaGeneralDTO.FechaFinEnvioWhatsapp;
                this.NumeroMinutosPrimerEnvio = campaniaGeneralDTO.NumeroMinutosPrimerEnvio;
                this.IdHoraEnvioWhatsapp = campaniaGeneralDTO.IdHoraEnvio_Whatsapp;
                this.DiasSinWhatsapp = campaniaGeneralDTO.DiasSinWhatsapp;
                this.IdPlantillaWhatsapp = campaniaGeneralDTO.IdPlantilla_Whatsapp;
                this.IdEstadoEnvioMailing = 1;
                this.IdEstadoEnvioWhatsapp = 1;
                this.Estado = true;
                this.FechaCreacion = DateTime.Now;
                this.FechaModificacion = DateTime.Now;
                this.UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion;
                this.UsuarioCreacion = campaniaGeneralDTO.UsuarioCreacion;
            }

            if (campaniaGeneralDTO.ListaPrioridades != null)
            {
                this.listaCampaniaGeneralDetalleBO = new List<CampaniaGeneralDetalleBO>();

                foreach (var detalle in campaniaGeneralDTO.ListaPrioridades)
                {
                    //detalle.IdCampaniaMailing = this.Id;
                    if (detalle.Id == 0)
                    {
                        var conjuntoAnuncioBO = new ConjuntoAnuncioBO
                        {
                            Nombre = detalle.Nombre,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            FechaCreacionCampania = DateTime.Now,
                            IdCategoriaOrigen = campaniaGeneralDTO.IdCategoriaOrigen,
                            Origen = "CAMPANIA_MAILING",
                            UsuarioCreacion = campaniaGeneralDTO.UsuarioModificacion,
                            UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion
                        };

                        _repConjuntoAnuncio.Insert(conjuntoAnuncioBO);

                        #region Insercion legada a V3
                        try
                        {
                            _repConjuntoAnuncio.InsertarConjuntoAnuncioV3(conjuntoAnuncioBO.Id);
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        detalle.IdConjuntoAnuncio = conjuntoAnuncioBO.Id;

                        var campaniaGeneralDetalleBO = new CampaniaGeneralDetalleBO
                        {
                            IdCampaniaGeneral = detalle.IdCampaniaGeneral ?? 0,
                            Nombre = detalle.Nombre,
                            Prioridad = detalle.Prioridad,
                            Asunto = detalle.Asunto,
                            IdPersonal = detalle.IdPersonal,
                            IdCentroCosto = detalle.IdCentroCosto,
                            IdConjuntoAnuncio = detalle.IdConjuntoAnuncio,
                            CantidadContactosMailing = detalle.CantidadContactosMailing,
                            CantidadContactosWhatsapp = detalle.CantidadContactosWhatsapp,
                            NoIncluyeWhatsaap = detalle.NoIncluyeWhatsaap,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = campaniaGeneralDTO.UsuarioModificacion,
                            UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion,

                            listaCampaniaGeneralDetalleProgramaBO = new List<CampaniaGeneralDetalleProgramaBO>()
                        };
                        int i = 0;

                        if (detalle.ProgramasFiltro != null)
                        {
                            i = 0;
                            foreach (var programa in detalle.ProgramasFiltro)
                            {
                                i += 1;
                                campaniaGeneralDetalleBO.listaCampaniaGeneralDetalleProgramaBO.Add(CrearCampaniaGeneralDetallePrograma(programa, i));
                            }
                        }

                        campaniaGeneralDetalleBO.AreaCampaniaGeneralDetalle = new List<CampaniaGeneralDetalleAreaBO>();
                        if (detalle.Areas.Count() > 0)
                        {
                            foreach (var item in detalle.Areas)
                            {
                                CampaniaGeneralDetalleAreaBO area = new CampaniaGeneralDetalleAreaBO
                                {
                                    IdAreaCapacitacion = item,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = this.UsuarioModificacion,
                                    UsuarioModificacion = this.UsuarioModificacion
                                };
                                campaniaGeneralDetalleBO.AreaCampaniaGeneralDetalle.Add(area);
                            }
                        }

                        campaniaGeneralDetalleBO.SubAreaCampaniaGeneralDetalle = new List<CampaniaGeneralDetalleSubAreaBO>();
                        if (detalle.SubAreas.Count() > 0)
                        {
                            foreach (var item in detalle.SubAreas)
                            {
                                CampaniaGeneralDetalleSubAreaBO subArea = new CampaniaGeneralDetalleSubAreaBO
                                {
                                    IdSubAreaCapacitacion = item,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = this.UsuarioModificacion,
                                    UsuarioModificacion = this.UsuarioModificacion
                                };
                                campaniaGeneralDetalleBO.SubAreaCampaniaGeneralDetalle.Add(subArea);
                            }
                        }

                        campaniaGeneralDetalleBO.ResponsableCampaniaGeneralDetalle = new List<CampaniaGeneralDetalleResponsableBO>();
                        if (detalle.Responsables.Count() > 0)
                        {
                            foreach (var item in detalle.Responsables)
                            {
                                CampaniaGeneralDetalleResponsableBO responsable = new CampaniaGeneralDetalleResponsableBO
                                {
                                    IdPersonal = item.IdResponsable == null ? 0 : item.IdResponsable.Value,
                                    Dia1 = item.Dia1 == null ? 0 : item.Dia1.Value,
                                    Dia2 = item.Dia2 == null ? 0 : item.Dia2.Value,
                                    Dia3 = item.Dia3 == null ? 0 : item.Dia3.Value,
                                    Dia4 = item.Dia4 == null ? 0 : item.Dia4.Value,
                                    Dia5 = item.Dia5 == null ? 0 : item.Dia5.Value,
                                    Total = item.Total == null ? 0 : item.Total.Value,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = this.UsuarioModificacion,
                                    UsuarioModificacion = this.UsuarioModificacion
                                };
                                campaniaGeneralDetalleBO.ResponsableCampaniaGeneralDetalle.Add(responsable);
                            }
                        }

                        this.listaCampaniaGeneralDetalleBO.Add(campaniaGeneralDetalleBO);
                    }
                    else
                    {
                        //actualizamos la campaña
                        if (!string.IsNullOrEmpty(detalle.Nombre))
                        {
                            var conjuntoAnuncioBO = new ConjuntoAnuncioBO();

                            if (detalle.IdConjuntoAnuncio != null)
                            {
                                var conjuntoAnuncio = _repConjuntoAnuncio.FirstBy(x => x.Id == detalle.IdConjuntoAnuncio);

                                conjuntoAnuncio.Nombre = detalle.Nombre;
                                conjuntoAnuncio.Estado = true;
                                conjuntoAnuncio.FechaModificacion = DateTime.Now;
                                conjuntoAnuncio.UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion;
                                conjuntoAnuncio.IdCategoriaOrigen = campaniaGeneralDTO.IdCategoriaOrigen;
                                conjuntoAnuncio.Origen = "CAMPANIA_MAILING";
                                _repConjuntoAnuncio.Update(conjuntoAnuncio);

                                #region Actualizacion legada a V3
                                try
                                {
                                    _repConjuntoAnuncio.ActualizarConjuntoAnuncioV3(conjuntoAnuncio.Id);
                                }
                                catch (Exception ex)
                                {
                                }
                                #endregion
                            }
                            else
                            {
                                conjuntoAnuncioBO.Nombre = detalle.Nombre;
                                conjuntoAnuncioBO.Estado = true;
                                conjuntoAnuncioBO.FechaCreacion = DateTime.Now;
                                conjuntoAnuncioBO.FechaModificacion = DateTime.Now;
                                conjuntoAnuncioBO.UsuarioCreacion = campaniaGeneralDTO.UsuarioModificacion;
                                conjuntoAnuncioBO.FechaCreacionCampania = conjuntoAnuncioBO.FechaCreacion;
                                conjuntoAnuncioBO.IdCategoriaOrigen = campaniaGeneralDTO.IdCategoriaOrigen;
                                conjuntoAnuncioBO.Origen = "CAMPANIA_MAILING";
                                conjuntoAnuncioBO.UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion;

                                _repConjuntoAnuncio.Insert(conjuntoAnuncioBO);
                                detalle.IdConjuntoAnuncio = conjuntoAnuncioBO.Id;

                                #region Insercion legada a V3
                                try
                                {
                                    _repConjuntoAnuncio.InsertarConjuntoAnuncioV3(conjuntoAnuncioBO.Id);
                                }
                                catch (Exception ex)
                                {
                                }
                                #endregion
                            }
                        }
                        var campaniaGeneralDetalle = _repCampaniaGeneralDetalle.FirstById(detalle.Id);

                        CampaniaGeneralDetalleBO campaniaGeneralDetalleBO = new CampaniaGeneralDetalleBO
                        {
                            Id = campaniaGeneralDetalle.Id,
                            IdCampaniaGeneral = detalle.IdCampaniaGeneral ?? 0,
                            Nombre = detalle.Nombre,
                            Prioridad = detalle.Prioridad,
                            Asunto = detalle.Asunto,
                            IdPersonal = detalle.IdPersonal,
                            IdCentroCosto = detalle.IdCentroCosto,
                            IdConjuntoAnuncio = detalle.IdConjuntoAnuncio,
                            CantidadContactosMailing = detalle.CantidadContactosMailing,
                            CantidadContactosWhatsapp = detalle.CantidadContactosWhatsapp,
                            NoIncluyeWhatsaap = detalle.NoIncluyeWhatsaap,
                            Estado = campaniaGeneralDetalle.Estado,
                            FechaCreacion = campaniaGeneralDetalle.FechaCreacion,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = campaniaGeneralDetalle.UsuarioCreacion,
                            UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion,
                            IdMigracion = campaniaGeneralDetalle.IdMigracion,
                            RowVersion = campaniaGeneralDetalle.RowVersion
                        };

                        var listaCampaniaGeneralDetallePrograma = _repCampaniaGeneralDetallePrograma.GetBy(x => x.IdCampaniaGeneralDetalle == detalle.Id).ToList();
                        foreach (var campaniaGeneralDetallePrograma in listaCampaniaGeneralDetallePrograma)
                        {
                            _repCampaniaGeneralDetallePrograma.Delete(campaniaGeneralDetallePrograma.Id, campaniaGeneralDTO.UsuarioModificacion);
                        }
                        _repAreaCampaniaGeneralDetalle.EliminacionLogicoPorCampaniaGeneral(detalle.Id, campaniaGeneralDTO.UsuarioModificacion, detalle.Areas);
                        _repSubAreaCampaniaGeneralDetalle.EliminacionLogicoPorCampaniaGeneral(detalle.Id, campaniaGeneralDTO.UsuarioModificacion, detalle.SubAreas);

                        campaniaGeneralDetalleBO.listaCampaniaGeneralDetalleProgramaBO = new List<CampaniaGeneralDetalleProgramaBO>();

                        int i = 0;
                        if (detalle.ProgramasFiltro != null)
                        {
                            i = 0;
                            foreach (var programa in detalle.ProgramasFiltro)
                            {
                                i += 1;
                                campaniaGeneralDetalleBO.listaCampaniaGeneralDetalleProgramaBO.Add(CrearCampaniaGeneralDetallePrograma(programa, i));
                            }
                        }
                        campaniaGeneralDetalleBO.AreaCampaniaGeneralDetalle = new List<CampaniaGeneralDetalleAreaBO>();
                        foreach (var item in detalle.Areas)
                        {
                            CampaniaGeneralDetalleAreaBO area;
                            if (_repAreaCampaniaGeneralDetalle.Exist(x => x.IdAreaCapacitacion == item && x.IdCampaniaGeneralDetalle == detalle.Id))
                            {
                                area = _repAreaCampaniaGeneralDetalle.FirstBy(x => x.IdAreaCapacitacion == item && x.IdCampaniaGeneralDetalle == detalle.Id);
                                area.IdAreaCapacitacion = item;
                                area.UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion;
                                area.FechaModificacion = DateTime.Now;

                            }
                            else
                            {
                                area = new CampaniaGeneralDetalleAreaBO
                                {
                                    IdAreaCapacitacion = item,
                                    UsuarioCreacion = campaniaGeneralDTO.UsuarioModificacion,
                                    UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }

                            campaniaGeneralDetalleBO.AreaCampaniaGeneralDetalle.Add(area);
                        }
                        campaniaGeneralDetalleBO.SubAreaCampaniaGeneralDetalle = new List<CampaniaGeneralDetalleSubAreaBO>();
                        foreach (var item in detalle.SubAreas)
                        {
                            CampaniaGeneralDetalleSubAreaBO subArea;
                            if (_repSubAreaCampaniaGeneralDetalle.Exist(x => x.IdSubAreaCapacitacion == item && x.IdCampaniaGeneralDetalle == detalle.Id))
                            {
                                subArea = _repSubAreaCampaniaGeneralDetalle.FirstBy(x => x.IdSubAreaCapacitacion == item && x.IdCampaniaGeneralDetalle == detalle.Id);
                                subArea.IdSubAreaCapacitacion = item;
                                subArea.UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion;
                                subArea.FechaModificacion = DateTime.Now;

                            }
                            else
                            {
                                subArea = new CampaniaGeneralDetalleSubAreaBO
                                {
                                    IdSubAreaCapacitacion = item,
                                    UsuarioCreacion = campaniaGeneralDTO.UsuarioModificacion,
                                    UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }

                            campaniaGeneralDetalleBO.SubAreaCampaniaGeneralDetalle.Add(subArea);
                        }

                        campaniaGeneralDetalleBO.ResponsableCampaniaGeneralDetalle = new List<CampaniaGeneralDetalleResponsableBO>();
                        foreach (var item in detalle.Responsables)
                        {
                            CampaniaGeneralDetalleResponsableBO responsable;
                            if (_repCampaniaGeneralDetalleResponsable.Exist(x => x.Id == item.Id && x.IdCampaniaGeneralDetalle == detalle.Id))
                            {
                                responsable = _repCampaniaGeneralDetalleResponsable.FirstBy(x => x.Id == item.Id && x.IdCampaniaGeneralDetalle == detalle.Id);
                                responsable.IdPersonal = item.IdResponsable == null ? 0 : item.IdResponsable.Value;
                                responsable.Dia1 = item.Dia1 == null ? 0 : item.Dia1.Value;
                                responsable.Dia2 = item.Dia2 == null ? 0 : item.Dia2.Value;
                                responsable.Dia3 = item.Dia3 == null ? 0 : item.Dia3.Value;
                                responsable.Dia4 = item.Dia4 == null ? 0 : item.Dia4.Value;
                                responsable.Dia5 = item.Dia5 == null ? 0 : item.Dia5.Value;
                                responsable.Total = item.Total == null ? 0 : item.Total.Value;
                                responsable.UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion;
                                responsable.FechaModificacion = DateTime.Now;

                            }
                            else
                            {
                                responsable = new CampaniaGeneralDetalleResponsableBO
                                {
                                    IdPersonal = item.IdResponsable == null ? 0 : item.IdResponsable.Value,
                                    Dia1 = item.Dia1 == null ? 0 : item.Dia1.Value,
                                    Dia2 = item.Dia2 == null ? 0 : item.Dia2.Value,
                                    Dia3 = item.Dia3 == null ? 0 : item.Dia3.Value,
                                    Dia4 = item.Dia4 == null ? 0 : item.Dia4.Value,
                                    Dia5 = item.Dia5 == null ? 0 : item.Dia5.Value,
                                    Total = item.Total == null ? 0 : item.Total.Value,
                                    UsuarioCreacion = campaniaGeneralDTO.UsuarioModificacion,
                                    UsuarioModificacion = campaniaGeneralDTO.UsuarioModificacion,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }

                            campaniaGeneralDetalleBO.ResponsableCampaniaGeneralDetalle.Add(responsable);
                        }
                        this.listaCampaniaGeneralDetalleBO.Add(campaniaGeneralDetalleBO);

                        //Nuevo filtro

                    }
                }
            }
        }

        /// Autor: Carlos Crispin
        /// Fecha: 16/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los detalle de una campaña
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Objeto de clase CampaniaGeneralDTO</returns>
        public CampaniaGeneralDTO ObtenerDetalle(int idCampaniaGeneral)
        {
            try
            {
                var campaniaGeneral = _repCampaniaGeneral.Obtener(idCampaniaGeneral);
                campaniaGeneral.ListaPrioridades = _repCampaniaGeneral.ObtenerListaCampaniaGeneralDetalleConProgramas(idCampaniaGeneral);
                return campaniaGeneral;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 27/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los detalle de una campaña
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Objeto de clase CampaniaGeneralDetalleEstadoEnEjecucionDTO</returns>
        public List<CampaniaGeneralDetalleEstadoEnEjecucionDTO> ObtenerEstadoEjecucion(int idCampaniaGeneral)
        {
            try
            {
                var campaniaGeneralEstadoEnEjecucion = _repCampaniaGeneral.ObtenerEstadoEjecucionCampaniaGeneralDetalle(idCampaniaGeneral);

                return campaniaGeneralEstadoEnEjecucion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los detalle de una campaña detalle responsable
        /// </summary>
        /// <returns></returns>
        public List<ResponsablesDTO> ObtenerDetalleResponsables(int IdCampaniaGeneralDetalle)
        {
            try
            {
                var ListaResponsables = _repCampaniaGeneral.ObtenerListaCampaniaGeneralDetalleResponsables(IdCampaniaGeneralDetalle);
                return ListaResponsables;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Genera lista de CampaniaGeneralDetallePrograma
        /// </summary>
        /// <param name="listaCampaniaGeneralDetallePrograma">Objeto de clase CampaniaGeneralDetalleProgramaDTO</param>
        /// <param name="i">Indice</param>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Objeto de clase CampaniaGeneralDetalleProgramaBO</returns>
        public CampaniaGeneralDetalleProgramaBO CrearCampaniaGeneralDetallePrograma(CampaniaGeneralDetalleProgramaDTO listaCampaniaGeneralDetallePrograma, int i, int idCampaniaGeneralDetalle = 0)
        {
            CampaniaGeneralDetalleProgramaBO campaniaGeneralDetallePrograma = new CampaniaGeneralDetalleProgramaBO();

            if (idCampaniaGeneralDetalle != 0) campaniaGeneralDetallePrograma.IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle;
            campaniaGeneralDetallePrograma.IdPGeneral = listaCampaniaGeneralDetallePrograma.IdPgeneral;
            campaniaGeneralDetallePrograma.NombreProgramaGeneral = listaCampaniaGeneralDetallePrograma.NombreProgramaGeneral;
            campaniaGeneralDetallePrograma.Orden = i;
            campaniaGeneralDetallePrograma.Estado = true;
            campaniaGeneralDetallePrograma.FechaCreacion = DateTime.Now;
            campaniaGeneralDetallePrograma.FechaModificacion = DateTime.Now;
            campaniaGeneralDetallePrograma.UsuarioCreacion = this.UsuarioModificacion;
            campaniaGeneralDetallePrograma.UsuarioModificacion = this.UsuarioModificacion;
            return campaniaGeneralDetallePrograma;
        }

        public bool ActualizarEstadoArchivado(CampaniaGeneralBO campaniaGeneral)
        {
            try
            {
                List<int> listaCampaniaGeneralDetalle = _repCampaniaGeneralDetalle.GetBy(x => x.IdCampaniaGeneral == campaniaGeneral.Id)?.Select(s => s.Id).ToList();
                List<PrioridadMailChimpListaBO> listaPrioridades = new List<PrioridadMailChimpListaBO>();

                int cantidadEnviadosTotal = 0;

                if (listaCampaniaGeneralDetalle.Any())
                {
                    listaPrioridades = _repPrioridadMailChimpLista.GetBy(x => listaCampaniaGeneralDetalle.Contains(x.IdCampaniaGeneralDetalle.Value)).ToList();

                    cantidadEnviadosTotal = listaPrioridades.Where(w => w.CantidadEnviadosMailChimp >= 0).Select(s => s.CantidadEnviadosMailChimp.Value).ToList().Sum();

                    campaniaGeneral.IdEstadoEnvioMailing = cantidadEnviadosTotal > 0 ? ValorEstatico.IdEstadoEnvioArchivadoCorrecto : ValorEstatico.IdEstadoEnvioArchivadoIncorrecto;

                    _repCampaniaGeneral.Update(campaniaGeneral);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
