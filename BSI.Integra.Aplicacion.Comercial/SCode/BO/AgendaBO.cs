using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    /// BO: Comercial/Agenda
    /// Autor: Fischer Valdez - Wilber Choque - Joao - Priscila Pacsi - Luis Huallpa - Carlos Crispin - Esthephany Tanco - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// BO para la logica de la agenda
    /// </summary>
    public class AgendaBO
    {
        /// Propiedades	                                Significado
        /// -----------	                                ------------
        /// IdAsesor                                    Id del asesor (PK de la tabla pla.T_Personal)
        /// IdTab                                       Id del tab (PK de la tabla com.T_AgendaTab)
        /// ValidacionTabs                              Booleano con la validacion de tabs
        /// AreaTrabajo                                 Cadena con la abreviatura del area de trabajo
        /// Filtros                                     Diccionario (string, string)
        /// CantidadRN2                                 Entero con la cantidad de RN2
        /// ActividadesRealizadas                       Lista de objetos de tipo CompuestoActividadEjecutadaDTO
        /// ActividadesAgenda                           Diccionario (string, lista de objetos de tipo ActividadAgendaDTO)
        /// ActividadesAgendaOperaciones                Diccionario (string, lista de objetos de tipo ActividadAgendaDTO)
        /// HabilitarEstados                            Diccionario (string, bool)
        /// tabsAgenda                                  Lista de objetos de tipo TabAgendaDTO

        public int IdAsesor { get; set; }
        public int? DiferenciaHoraria { get; set; }
        public int IdTab { get; set; }
        public bool ValidacionTabs { get; set; }
        public string AreaTrabajo { get; set; }
        public Dictionary<string, string> Filtros;
        public int CantidadRN2 { get; set; }
        public List<CompuestoActividadEjecutadaDTO> ActividadesRealizadas;
        public Dictionary<string, List<ActividadAgendaDTO>> ActividadesAgenda { get; set; }
        public Dictionary<string, List<ActividadAgendaDTO>> ActividadesAgendaOperaciones { get; set; }
        public Dictionary<string, bool> HabilitarEstados { get; set; }

        private List<TabAgendaDTO> tabsAgenda;
        private ActividadDetalleRepositorio _repActividadDetalle;
        private AgendaTabRepositorio _repAgendaTab;

        public string LogCarlos { get; set; }

        /// <summary>
        /// Constructor para obtener las configuraciones de la Agenda
        /// </summary>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="codigoAreaTrabajo">Codigo del area de trabajo en codigo</param>
        public AgendaBO(int idAsesor, string codigoAreaTrabajo)
        {
            _repAgendaTab = new AgendaTabRepositorio();
            tabsAgenda = new List<TabAgendaDTO>();
            ActividadesAgenda = new Dictionary<string, List<ActividadAgendaDTO>>();
            ActividadesAgendaOperaciones = new Dictionary<string, List<ActividadAgendaDTO>>();
            ActividadesRealizadas = new List<CompuestoActividadEjecutadaDTO>();
            Filtros = new Dictionary<string, string>();
            this.IdAsesor = idAsesor;
            this.AreaTrabajo = codigoAreaTrabajo;
            ListaActividades();
            CargarActividades();
        }

        /// <summary>
        /// Constructor para obtener las configuraciones de la Agenda
        /// </summary>
        /// <param name="codigoAreaTrabajo">Codigo del area de trabajo en codigo</param>
        public AgendaBO(string codigoAreaTrabajo)
        {
            _repActividadDetalle = new ActividadDetalleRepositorio();
            _repAgendaTab = new AgendaTabRepositorio();
            tabsAgenda = new List<TabAgendaDTO>();
            Filtros = new Dictionary<string, string>();
            ActividadesAgenda = new Dictionary<string, List<ActividadAgendaDTO>>();
            ActividadesRealizadas = new List<CompuestoActividadEjecutadaDTO>();
            this.AreaTrabajo = codigoAreaTrabajo;
            ListaActividades();
            HabilitarEstados = new Dictionary<string, bool>();
        }

        /// <summary>
        /// Obtiene toda la lista de Actividades por cada Tab del Asesor
        /// </summary>
        private void CargarActividades()
        {
            Dictionary<string, List<ActividadAgendaDTO>> actividadesAux = new Dictionary<string, List<ActividadAgendaDTO>>();

            foreach (var item in this.tabsAgenda)
            {
                if (item.VisualizarActividad)
                {
                    var listaActividades = new List<ActividadAgendaDTO>();
                    if (item.CargarInformacionInicial)
                    {
                        if (item.VistaBaseDatos.Contains("ActividadRealizadaLlamada"))
                        {
                            //this.CargarActividadesRealizadas(item);
                        }
                        else if (item.VistaBaseDatos.Contains("ActividadProgramada"))
                        {
                            var actividades = _repAgendaTab.ObtenerActividadesProgramada(item, this.IdAsesor, this.Filtros);
                            foreach (var actividadProgramada in actividades)
                            {
                                var actividad = new ActividadAgendaDTO
                                {
                                    Id = actividadProgramada.Id,
                                    //_actividad.TipoActividad = actividadProgramada.TipoActividad;
                                    EstadoHoja = actividadProgramada.EstadoHoja,
                                    CentroCosto = actividadProgramada.CentroCosto,
                                    Contacto = actividadProgramada.Contacto,
                                    //_actividad.IdCargo = actividadProgramada.IdCargo == null ? 0 : actividadProgramada.IdCargo.Value;
                                    //_actividad.IdAFormacion = actividadProgramada.IdAFormacion == null ? 0 : actividadProgramada.IdAFormacion.Value;
                                    //_actividad.IdATrabajo = actividadProgramada.IdATrabajo == null ? 0 : actividadProgramada.IdATrabajo.Value;
                                    //_actividad.IdIndustria = actividadProgramada.IdIndustria == null ? 0 : actividadProgramada.IdIndustria.Value;
                                    CodigoFase = actividadProgramada.CodigoFase,
                                    NombreTipoDato = actividadProgramada.NombreTipoDato,
                                    Origen = actividadProgramada.Origen,
                                    UltimaFechaProgramada = actividadProgramada.UltimaFechaProgramada,
                                    IdAlumno = actividadProgramada.IdAlumno,
                                    IdClasificacionPersona = actividadProgramada.IdClasificacionPersona,
                                    IdOportunidad = actividadProgramada.IdOportunidad,
                                    UltimoComentario = actividadProgramada.UltimoComentario,
                                    IdActividadCabecera = actividadProgramada.IdActividadCabecera,
                                    //_actividad.ActividadesVencidas = actividadProgramada.ActividadesVencidas;
                                    ReprogramacionManual = actividadProgramada.ReprogramacionManual,
                                    ReprogramacionAutomatica = actividadProgramada.ReprogramacionAutomatica,
                                    ActividadCabecera = actividadProgramada.ActividadCabecera,
                                    Asesor = actividadProgramada.Asesor,
                                    IdPersonal_Asignado = actividadProgramada.IdPersonal_Asignado,
                                    IdCentroCosto = actividadProgramada.IdCentroCosto,
                                    IdFaseOportunidad = actividadProgramada.IdFaseOportunidad,
                                    IdTipoDato = actividadProgramada.IdTipoDato,
                                    ProbabilidadActualDesc = actividadProgramada.ProbabilidadActualDesc,
                                    CategoriaNombre = actividadProgramada.CategoriaNombre,
                                    CategoriaDescripcion = actividadProgramada.CategoriaDescripcion,
                                    IdCategoriaOrigen = actividadProgramada.IdCategoriaOrigen,
                                    IdSubCategoriaDato = actividadProgramada.IdSubCategoriaDato,
                                    IdEstadoOportunidad = actividadProgramada.IdEstadoOportunidad,
                                    ValidaLlamada = actividadProgramada.ValidaLlamada,
                                    IdPadre = actividadProgramada.IdPadre
                                };
                                //_actividad.EstadoOportunidad = actividadProgramada.EstadoOportunidad;
                                listaActividades.Add(actividad);
                            }

                        }
                        else if (item.VistaBaseDatos.Contains("ActividadNoProgramada") && item.Probabilidad.Contains("Muy Alta"))
                        {   
                            var actividades = _repAgendaTab.ObtenerActividadesNoProgramada(item, this.IdAsesor, this.Filtros);
                            foreach (var actividadProgramada in actividades)
                            {
                                var actividad = new ActividadAgendaDTO
                                {
                                    Id = actividadProgramada.Id,
                                    //_actividad.TipoActividad = actividadProgramada.TipoActividad;
                                    EstadoHoja = actividadProgramada.EstadoHoja,
                                    CentroCosto = actividadProgramada.CentroCosto,
                                    Contacto = actividadProgramada.Contacto,
                                    //_actividad.IdCargo = actividadProgramada.IdCargo == null ? 0 : actividadProgramada.IdCargo.Value;
                                    //_actividad.IdAFormacion = actividadProgramada.IdAFormacion == null ? 0 : actividadProgramada.IdAFormacion.Value;
                                    //_actividad.IdATrabajo = actividadProgramada.IdATrabajo == null ? 0 : actividadProgramada.IdATrabajo.Value;
                                    //_actividad.IdIndustria = actividadProgramada.IdIndustria == null ? 0 : actividadProgramada.IdIndustria.Value;
                                    CodigoFase = actividadProgramada.CodigoFase,
                                    NombreTipoDato = actividadProgramada.NombreTipoDato,
                                    Origen = actividadProgramada.Origen,
                                    UltimaFechaProgramada = actividadProgramada.UltimaFechaProgramada,
                                    IdAlumno = actividadProgramada.IdAlumno,
                                    IdClasificacionPersona = actividadProgramada.IdClasificacionPersona,
                                    IdOportunidad = actividadProgramada.IdOportunidad,
                                    UltimoComentario = actividadProgramada.UltimoComentario,
                                    IdActividadCabecera = actividadProgramada.IdActividadCabecera,
                                    //_actividad.ActividadesVencidas = actividadProgramada.ActividadesVencidas;
                                    ReprogramacionManual = actividadProgramada.ReprogramacionManual,
                                    ReprogramacionAutomatica = actividadProgramada.ReprogramacionAutomatica,
                                    ActividadCabecera = actividadProgramada.ActividadCabecera,
                                    Asesor = actividadProgramada.Asesor,
                                    IdPersonal_Asignado = actividadProgramada.IdPersonal_Asignado,
                                    IdCentroCosto = actividadProgramada.IdCentroCosto,
                                    IdFaseOportunidad = actividadProgramada.IdFaseOportunidad,
                                    IdTipoDato = actividadProgramada.IdTipoDato,
                                    ProbabilidadActualDesc = actividadProgramada.ProbabilidadActualDesc,
                                    CategoriaNombre = actividadProgramada.CategoriaNombre,
                                    CategoriaDescripcion = actividadProgramada.CategoriaDescripcion,
                                    IdCategoriaOrigen = actividadProgramada.IdCategoriaOrigen,
                                    IdSubCategoriaDato = actividadProgramada.IdSubCategoriaDato,
                                    IdEstadoOportunidad = actividadProgramada.IdEstadoOportunidad,
                                    ValidaLlamada = actividadProgramada.ValidaLlamada,
                                    IdPadre = actividadProgramada.IdPadre
                                };
                                //_actividad.EstadoOportunidad = actividadProgramada.EstadoOportunidad;
                                listaActividades.Add(actividad);
                            }

                        }
                        else
                        {
                            var actividades = _repAgendaTab.ObtenerActividades(item, this.IdAsesor, this.Filtros);
                            foreach (var actividadProgramada in actividades)
                            {
                                var actividad = new ActividadAgendaDTO
                                {
                                    Id = actividadProgramada.Id,
                                    //_actividad.TipoActividad = actividadProgramada.TipoActividad;
                                    EstadoHoja = actividadProgramada.EstadoHoja,
                                    CentroCosto = actividadProgramada.CentroCosto,
                                    Contacto = actividadProgramada.Contacto,
                                    //_actividad.IdCargo = actividadProgramada.IdCargo == null ? 0 : actividadProgramada.IdCargo.Value;
                                    //_actividad.IdAFormacion = actividadProgramada.IdAFormacion == null ? 0 : actividadProgramada.IdAFormacion.Value;
                                    //_actividad.IdATrabajo = actividadProgramada.IdATrabajo == null ? 0 : actividadProgramada.IdATrabajo.Value;
                                    //_actividad.IdIndustria = actividadProgramada.IdIndustria == null ? 0 : actividadProgramada.IdIndustria.Value;
                                    CodigoFase = actividadProgramada.CodigoFase,
                                    NombreTipoDato = actividadProgramada.NombreTipoDato,
                                    Origen = actividadProgramada.Origen,
                                    UltimaFechaProgramada = actividadProgramada.UltimaFechaProgramada,
                                    IdAlumno = actividadProgramada.IdAlumno,
                                    IdClasificacionPersona = actividadProgramada.IdClasificacionPersona,
                                    IdOportunidad = actividadProgramada.IdOportunidad,
                                    UltimoComentario = actividadProgramada.UltimoComentario,
                                    IdActividadCabecera = actividadProgramada.IdActividadCabecera,
                                    //_actividad.ActividadesVencidas = actividadProgramada.ActividadesVencidas;
                                    ReprogramacionManual = actividadProgramada.ReprogramacionManual,
                                    ReprogramacionAutomatica = actividadProgramada.ReprogramacionAutomatica,
                                    ActividadCabecera = actividadProgramada.ActividadCabecera,
                                    Asesor = actividadProgramada.Asesor,
                                    IdPersonal_Asignado = actividadProgramada.IdPersonal_Asignado,
                                    IdCentroCosto = actividadProgramada.IdCentroCosto,
                                    IdFaseOportunidad = actividadProgramada.IdFaseOportunidad,
                                    IdTipoDato = actividadProgramada.IdTipoDato,
                                    ProbabilidadActualDesc = actividadProgramada.ProbabilidadActualDesc,
                                    CategoriaNombre = actividadProgramada.CategoriaNombre,
                                    CategoriaDescripcion = actividadProgramada.CategoriaDescripcion,
                                    IdCategoriaOrigen = actividadProgramada.IdCategoriaOrigen,
                                    IdSubCategoriaDato = actividadProgramada.IdSubCategoriaDato,
                                    IdEstadoOportunidad = actividadProgramada.IdEstadoOportunidad,
                                    ValidaLlamada = actividadProgramada.ValidaLlamada,
                                    DiasSinContactoManhana = actividadProgramada.DiasSinContactoManhana,
                                    DiasSinContactoTarde = actividadProgramada.DiasSinContactoTarde,
                                    ActividadesManhana = actividadProgramada.ActividadesManhana,
                                    ActividadesTarde = actividadProgramada.ActividadesTarde,
                                    IdPadre = actividadProgramada.IdPadre
                                };
                                //_actividad.EstadoOportunidad = actividadProgramada.EstadoOportunidad;
                                listaActividades.Add(actividad);
                            }
                        }
                    }
                    var actividadesLista = actividadesAux.ContainsKey(item.Nombre);
                    if (!actividadesLista)
                    {
                        actividadesAux.Add(item.Nombre, listaActividades);
                    }
                    else
                    {
                        actividadesAux[item.Nombre].AddRange(listaActividades);
                    }
                }
            }
            foreach (var item in actividadesAux)
            {
                if (!item.Key.Contains("No Prog"))
                {
                    var datos = item.Value;
                    datos = datos.OrderBy(x => x.UltimaFechaProgramada).ToList();
                    ActividadesAgenda.Add(item.Key, datos);
                }
                else
                {
                    var datos = item.Value;
                    ActividadesAgenda.Add(item.Key, datos);
                }

            }
        }

        /*Mantener codigo por precaucion*/
        /// <summary>
        /// Obtiene datos por actividad para el Tab de realizadas en la Agenda
        /// </summary>
        /// <param name="actividadesBD">Id de la Actividad Detalle</param>
        //private void CargarActividadesRealizadas(TabAgendaDTO tabAgendaDTO)//(string actividadesBD) 
        //{
        //    var actividades = _repAgendaTab.ObtenerActividadesRealizadas(tabAgendaDTO, this.IdAsesor, this.Filtros);
        //    var resultado = (from p in actividades
        //                     group p by new
        //                     {
        //                         p.Id,
        //                         p.CentroCosto,
        //                         p.Contacto,
        //                         p.CodigoFase,
        //                         p.NombreTipoDato,
        //                         p.Origen,
        //                         p.FechaProgramada,
        //                         p.FechaReal,
        //                         p.DuracionReal,
        //                         p.Actividad,
        //                         p.Ocurrencia,
        //                         p.Comentario,
        //                         p.Asesor,
        //                         p.IdAlumno,
        //                         p.IdOportunidad,
        //                         p.probabilidadActualDesc,
        //                         p.NombreCategoriaOrigen,
        //                         p.IdCategoriaOrigen,
        //                         p.FaseInicial,
        //                         p.FaseMaxima,
        //                         //p.totalOportunidades,
        //                         p.UnicoTimbrado,
        //                         p.UnicoContesto,
        //                         p.UnicoEstadoLlamada,
        //                         p.NumeroLlamadas,
        //                         p.Estado,
        //                         p.Clasificacion,
        //                         p.UnicoFechaLlamada
        //                     } into g
        //                     select new CompuestoActividadEjecutadaDTO
        //                     {
        //                         Id = g.Key.Id,
        //                         CentroCosto = g.Key.CentroCosto,
        //                         Contacto = g.Key.Contacto,
        //                         CodigoFase = g.Key.CodigoFase,
        //                         NombreTipoDato = g.Key.NombreTipoDato,
        //                         Origen = g.Key.Origen,
        //                         FechaProgramada = g.Key.FechaProgramada,
        //                         FechaReal = g.Key.FechaReal,
        //                         Duracion = g.Key.DuracionReal,
        //                         Actividad = g.Key.Actividad,
        //                         Ocurrencia = g.Key.Ocurrencia,
        //                         Comentario = g.Key.Comentario,
        //                         Asesor = g.Key.Asesor,
        //                         IdContacto = g.Key.IdAlumno,
        //                         IdOportunidad = g.Key.IdOportunidad,
        //                         ProbabilidadActual = g.Key.probabilidadActualDesc,
        //                         CategoriaNombre = g.Key.NombreCategoriaOrigen,
        //                         IdCategoria = g.Key.IdCategoriaOrigen,
        //                         FaseInicial = g.Key.FaseInicial,
        //                         FaseMaxima = g.Key.FaseMaxima,
        //                         //totalOportunidades = g.Key.totalOportunidades,
        //                         UnicoTimbrado = g.Key.UnicoTimbrado,
        //                         UnicoContesto = g.Key.UnicoContesto,
        //                         UnicoEstadoLlamada = g.Key.UnicoEstadoLlamada,
        //                         UnicoClasificacion = g.Key.Clasificacion,
        //                         UnicoFechaLlamada = g.Key.UnicoFechaLlamada,
        //                         NumeroLlamadas = g.Key.NumeroLlamadas,
        //                         Estado = g.Key.Estado,

        //                         Lista = g.Select(o => new CompuestoActividadEjecutadaDetalleDTO
        //                         {
        //                             DuracionTimbrado = o.DuracionTimbrado,
        //                             DuracionContesto = o.DuracionContesto,
        //                             EstadoLlamada = o.EstadoLlamada,
        //                             FechaLlamada = o.FechaLlamada == null ? DateTime.Now : o.FechaLlamada,
        //                             EstadoClasificacion = o.EstadoClasificacion
        //                         }).OrderByDescending(o => o.FechaLlamada).ToList()
        //                     }).OrderBy(x => x.FechaReal);

        //    var flag = false;
        //    var count = 0;
        //    double minutos = 0;
        //    double totalContesto = 0;
        //    double totalTimbrado = 0;
        //    double totalPerdido = 0;
        //    double mayorPerdido = 0;
        //    DateTime fechaTemp = new DateTime();
        //    DateTime fechaActual = DateTime.Now;

        //    foreach (var itemr in resultado)
        //    {
        //        CompuestoActividadEjecutadaDTO itemDetalle = new CompuestoActividadEjecutadaDTO()
        //        {
        //            Id = itemr.Id,
        //            CentroCosto = itemr.CentroCosto,
        //            Contacto = itemr.Contacto,
        //            CodigoFase = itemr.CodigoFase,
        //            NombreTipoDato = itemr.NombreTipoDato,
        //            Origen = itemr.Origen,
        //            FechaProgramada = itemr.FechaProgramada,
        //            FechaReal = itemr.FechaReal,
        //            Duracion = itemr.Duracion,
        //            Actividad = itemr.Actividad,
        //            Ocurrencia = itemr.Ocurrencia,
        //            Comentario = itemr.Comentario,
        //            Asesor = itemr.Asesor,
        //            IdContacto = itemr.IdContacto,
        //            IdOportunidad = itemr.IdOportunidad,
        //            ProbabilidadActual = itemr.ProbabilidadActual,
        //            CategoriaNombre = itemr.CategoriaNombre,
        //            IdCategoria = itemr.IdCategoria,
        //            FaseInicial = itemr.FaseInicial,
        //            FaseMaxima = itemr.FaseMaxima,
        //            TotalOportunidades = itemr.TotalOportunidades,
        //            UnicoTimbrado = itemr.UnicoTimbrado,
        //            UnicoContesto = itemr.UnicoContesto,
        //            UnicoEstadoLlamada = itemr.UnicoEstadoLlamada,
        //            Estado = itemr.Estado
        //        };

        //        if (itemr.Lista != null && itemr.Lista.Select(s => s.DuracionTimbrado).FirstOrDefault() != null)
        //        {
        //            var ordenLlamadas = itemr.Lista.OrderBy(x => x.FechaLlamada).ToList();
        //            var fechaUltima = ordenLlamadas.Select(s => s.FechaLlamada).FirstOrDefault();
        //            if (count > 0 && flag)
        //            {
        //                if (DateTime.Now.Day == fechaTemp.Day)
        //                {
        //                    var min = ((fechaUltima.Value - fechaTemp).TotalSeconds / 60).ToString("0.0");
        //                    minutos = Convert.ToDouble(min);
        //                }
        //                else
        //                {
        //                    minutos = 0;
        //                }
        //            }
        //            if (fechaUltima != null)
        //            {
        //                flag = true;
        //                //fechaTemp = fechaUltima.Value;
        //                fechaTemp = itemr.Lista.Select(x => x.FechaLlamada).FirstOrDefault().Value;
        //                double contesto = Convert.ToDouble(itemr.Lista.Select(x => x.DuracionContesto).FirstOrDefault());
        //                double timbrado = Convert.ToDouble(itemr.Lista.Select(x => x.DuracionTimbrado).FirstOrDefault());
        //                fechaTemp = fechaTemp.AddSeconds(contesto + timbrado);
        //            }
        //            totalTimbrado += (itemr.Lista.Select(s => Convert.ToDouble(s.DuracionTimbrado))).Sum();
        //            totalContesto += (itemr.Lista.Select(s => Convert.ToDouble(s.DuracionContesto))).Sum();
        //            if (minutos >= 0)
        //            {
        //                totalPerdido += minutos;
        //            }
        //            itemDetalle.NumeroLlamadas = itemr.NumeroLlamadas;
        //            itemr.Lista = itemr.Lista.OrderBy(x => x.FechaLlamada).ToList();
        //            itemDetalle.DuracionTimbrado = String.Concat(itemr.Lista.Select(o => o.EstadoLlamada + " <strong >- TT:</strong > " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + " <strong >-</strong > " + o.FechaLlamada.Value.ToString("yyyy/MM/dd HH:mm") + "<br /><strong id='estadoNuevoT'>Nuevo Estado: </strong ><strong id='estadoNuevoC'> " + o.EstadoClasificacion + "</strong><br />"));
        //        }
        //        else
        //        {
        //            var fechaUltima = itemr.UnicoFechaLlamada;
        //            if (count > 0 && flag)
        //            {
        //                if (DateTime.Now.Day == fechaTemp.Day && fechaUltima != null)
        //                {
        //                    var min = ((fechaUltima.Value - fechaTemp).TotalSeconds / 60).ToString("0.0");
        //                    minutos = Convert.ToDouble(min);
        //                }
        //                else
        //                {
        //                    minutos = 0;
        //                }
        //            }
        //            if (fechaUltima != null)
        //            {
        //                flag = true;
        //                fechaTemp = fechaUltima.Value;
        //                double contesto = Convert.ToDouble(itemr.UnicoContesto);
        //                double timbrado = Convert.ToDouble(itemr.UnicoTimbrado);
        //                fechaTemp = fechaTemp.AddSeconds(contesto + timbrado);
        //            }
        //            totalTimbrado += Convert.ToDouble(itemr.UnicoTimbrado);
        //            totalContesto += Convert.ToDouble(itemr.UnicoContesto);
        //            if (minutos >= 0)
        //            {
        //                totalPerdido += minutos;
        //            }
        //            string date = itemr.UnicoFechaLlamada == null ? "" : itemr.UnicoFechaLlamada.Value.ToString("yyyy/MM/dd HH:mm");
        //            itemDetalle.NumeroLlamadas = 1;
        //            itemDetalle.DuracionTimbrado = itemr.UnicoEstadoLlamada + " <strong >- TT:</strong >" + itemr.UnicoTimbrado + "  <strong >TC:</strong >" + itemr.UnicoContesto + " <strong >-</strong > " + date + "<br /><strong id='estadoNuevoT'>Nuevo Estado: </strong ><strong id='estadoNuevoC'>" + itemr.UnicoClasificacion + "</strong><br />";
        //        }

        //        itemDetalle.MinutosIntervale = minutos;
        //        itemDetalle.MinutosTotalContesto = totalContesto;
        //        itemDetalle.MinutosTotalTimbrado = totalTimbrado;
        //        itemDetalle.MinutosTotalPerdido = totalPerdido;
        //        count++;

        //        mayorPerdido = mayorPerdido > minutos ? mayorPerdido : minutos;
        //        itemDetalle.MayorTiempo = mayorPerdido;
        //        //var llamadasTresCX = _tcrmHojaOportunidadService.Get_LLamadasTresCXByActividad(item.Id);
        //        //llamadasTresCX = llamadasTresCX == null ? null : llamadasTresCX.OrderBy(x => x.FechaLlamada).ToList();
        //        //itemDetalle.tiemposTresCX = String.Concat(llamadasTresCX.Select(o => o.estadoLlamada + " <strong >- TT:</strong > " + o.duracionTimbrado + " <strong >TC:</strong > " + o.duracionContesto + " <strong >-</strong > " + o.FechaLlamada.ToString("yyyy/MM/dd HH:mm") + "<br />"));
        //        ActividadesRealizadas.Add(itemDetalle);
        //    }
        //}

        /// <summary>
        /// Obtiene las Activdades Realizadas o Actividades para programar del Asesor 
        /// para un determinado Tab
        /// </summary>
        public void CargarActividadesSeleccionadaPorAsesor()
        {
            string query = string.Empty;
            var tabActividad = tabsAgenda.Where(x => x.Id == this.IdTab).ToList();
            if (tabActividad != null)
            {
                foreach (var item in tabActividad)
                {
                    item.Probabilidad = "0";
                    if (item.VistaBaseDatos.Contains("ActividadRealizadaLlamada"))
                    {
                        //this.CargarActividadesRealizadas(item);
                        return;
                    }
                    else
                    {
                        var ListaActividades = new List<ActividadAgendaDTO>();
                        ListaActividades = _repAgendaTab.ObtenerActividades(item, this.IdAsesor, new Dictionary<string, string>());
                        var actividadesLista = ActividadesAgenda.ContainsKey(item.Nombre);
                        if (!actividadesLista)
                        {
                            ActividadesAgenda.Add(item.Nombre, ListaActividades);
                        }
                        else
                        {
                            ActividadesAgenda[item.Nombre].AddRange(ListaActividades);
                        }
                    }
                }
            }
        }


        /// Autor: Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab, con los filtros realizados
        /// </summary>        
        /// <returns> </returns> 
        public void CargarActividadSeleccionadaPorFiltro()
        {
            var tabActividad = tabsAgenda.Where(x => x.Id == this.IdTab).ToList();
            if (tabActividad != null)
            {
                foreach (var item in tabActividad)
                {
                    var listaActividades = new List<ActividadAgendaDTO>();


                    if (this.IdTab == 12)
                    {
                        item.CamposVista = item.CamposVista.Replace("TOP 10", "");
                    }
                    if (item.VistaBaseDatos.Contains("ActividadRealizadaLlamada"))
                    {
                        //this.CargarActividadesRealizadas(item);
                        return;
                    }

                    else if (item.VistaBaseDatos.Contains("ActividadProgramada"))
                    {
                        var actividades = _repAgendaTab.ObtenerActividadesProgramada(item, this.IdAsesor, this.Filtros);
                        foreach (var actividadProgramada in actividades)
                        {
                            var _actividad = new ActividadAgendaDTO
                            {
                                Id = actividadProgramada.Id,
                                //_actividad.TipoActividad = actividadProgramada.TipoActividad;
                                EstadoHoja = actividadProgramada.EstadoHoja,
                                CentroCosto = actividadProgramada.CentroCosto,
                                Contacto = actividadProgramada.Contacto,
                                //_actividad.IdCargo = actividadProgramada.IdCargo == null ? 0 : actividadProgramada.IdCargo.Value;
                                //_actividad.IdAFormacion = actividadProgramada.IdAFormacion == null ? 0 : actividadProgramada.IdAFormacion.Value;
                                //_actividad.IdATrabajo = actividadProgramada.IdATrabajo == null ? 0 : actividadProgramada.IdATrabajo.Value;
                                //_actividad.IdIndustria = actividadProgramada.IdIndustria == null ? 0 : actividadProgramada.IdIndustria.Value;
                                CodigoFase = actividadProgramada.CodigoFase,
                                NombreTipoDato = actividadProgramada.NombreTipoDato,
                                Origen = actividadProgramada.Origen,
                                UltimaFechaProgramada = actividadProgramada.UltimaFechaProgramada,
                                IdAlumno = actividadProgramada.IdAlumno,
                                IdClasificacionPersona = actividadProgramada.IdClasificacionPersona,
                                IdOportunidad = actividadProgramada.IdOportunidad,
                                UltimoComentario = actividadProgramada.UltimoComentario,
                                IdActividadCabecera = actividadProgramada.IdActividadCabecera,
                                //_actividad.ActividadesVencidas = actividadProgramada.ActividadesVencidas;
                                ReprogramacionManual = actividadProgramada.ReprogramacionManual,
                                ReprogramacionAutomatica = actividadProgramada.ReprogramacionAutomatica,
                                ActividadCabecera = actividadProgramada.ActividadCabecera,
                                Asesor = actividadProgramada.Asesor,
                                IdPersonal_Asignado = actividadProgramada.IdPersonal_Asignado,
                                IdCentroCosto = actividadProgramada.IdCentroCosto,
                                IdFaseOportunidad = actividadProgramada.IdFaseOportunidad,
                                IdTipoDato = actividadProgramada.IdTipoDato,
                                ProbabilidadActualDesc = actividadProgramada.ProbabilidadActualDesc,
                                CategoriaNombre = actividadProgramada.CategoriaNombre,
                                CategoriaDescripcion = actividadProgramada.CategoriaDescripcion,
                                IdCategoriaOrigen = actividadProgramada.IdCategoriaOrigen,
                                IdSubCategoriaDato = actividadProgramada.IdSubCategoriaDato,
                                IdEstadoOportunidad = actividadProgramada.IdEstadoOportunidad,
                                ValidaLlamada = actividadProgramada.ValidaLlamada,
                                DiasSinContactoManhana = actividadProgramada.DiasSinContactoManhana,
                                DiasSinContactoTarde = actividadProgramada.DiasSinContactoTarde,
                                ActividadesManhana = actividadProgramada.ActividadesManhana,
                                ActividadesTarde = actividadProgramada.ActividadesTarde,
                                IdPadre = actividadProgramada.IdPadre
                            };
                            listaActividades.Add(_actividad);

                            //_actividad.EstadoOportunidad = actividadProgramada.EstadoOportunidad;
                        }
                        listaActividades = listaActividades.OrderBy(x => x.UltimaFechaProgramada).ToList();
                        var _actividadesLista = ActividadesAgenda.ContainsKey(item.Nombre);
                        if (!_actividadesLista)
                        {
                            ActividadesAgenda.Add(item.Nombre, listaActividades);
                        }
                        else
                        {
                            ActividadesAgenda[item.Nombre].AddRange(listaActividades);
                        }

                    }
                    else if (item.VistaBaseDatos.Contains("ActividadNoProgramada") && item.Probabilidad.Contains("Muy Alta"))
                    {
                        var actividades = _repAgendaTab.ObtenerActividadesNoProgramada(item, this.IdAsesor, this.Filtros);
                        foreach (var actividadProgramada in actividades)
                        {
                            var _actividad = new ActividadAgendaDTO
                            {
                                Id = actividadProgramada.Id,
                                //_actividad.TipoActividad = actividadProgramada.TipoActividad;
                                EstadoHoja = actividadProgramada.EstadoHoja,
                                CentroCosto = actividadProgramada.CentroCosto,
                                Contacto = actividadProgramada.Contacto,
                                //_actividad.IdCargo = actividadProgramada.IdCargo == null ? 0 : actividadProgramada.IdCargo.Value;
                                //_actividad.IdAFormacion = actividadProgramada.IdAFormacion == null ? 0 : actividadProgramada.IdAFormacion.Value;
                                //_actividad.IdATrabajo = actividadProgramada.IdATrabajo == null ? 0 : actividadProgramada.IdATrabajo.Value;
                                //_actividad.IdIndustria = actividadProgramada.IdIndustria == null ? 0 : actividadProgramada.IdIndustria.Value;
                                CodigoFase = actividadProgramada.CodigoFase,
                                NombreTipoDato = actividadProgramada.NombreTipoDato,
                                Origen = actividadProgramada.Origen,
                                UltimaFechaProgramada = actividadProgramada.UltimaFechaProgramada,
                                IdAlumno = actividadProgramada.IdAlumno,
                                IdClasificacionPersona = actividadProgramada.IdClasificacionPersona,
                                IdOportunidad = actividadProgramada.IdOportunidad,
                                UltimoComentario = actividadProgramada.UltimoComentario,
                                IdActividadCabecera = actividadProgramada.IdActividadCabecera,
                                //_actividad.ActividadesVencidas = actividadProgramada.ActividadesVencidas;
                                ReprogramacionManual = actividadProgramada.ReprogramacionManual,
                                ReprogramacionAutomatica = actividadProgramada.ReprogramacionAutomatica,
                                ActividadCabecera = actividadProgramada.ActividadCabecera,
                                Asesor = actividadProgramada.Asesor,
                                IdPersonal_Asignado = actividadProgramada.IdPersonal_Asignado,
                                IdCentroCosto = actividadProgramada.IdCentroCosto,
                                IdFaseOportunidad = actividadProgramada.IdFaseOportunidad,
                                IdTipoDato = actividadProgramada.IdTipoDato,
                                ProbabilidadActualDesc = actividadProgramada.ProbabilidadActualDesc,
                                CategoriaNombre = actividadProgramada.CategoriaNombre,
                                CategoriaDescripcion = actividadProgramada.CategoriaDescripcion,
                                IdCategoriaOrigen = actividadProgramada.IdCategoriaOrigen,
                                IdSubCategoriaDato = actividadProgramada.IdSubCategoriaDato,
                                IdEstadoOportunidad = actividadProgramada.IdEstadoOportunidad,
                                ValidaLlamada = actividadProgramada.ValidaLlamada,
                                IdPadre = actividadProgramada.IdPadre
                            };
                            listaActividades.Add(_actividad);

                            //_actividad.EstadoOportunidad = actividadProgramada.EstadoOportunidad;
                        }
                        //ListaActividades = ListaActividades.OrderBy(x => x.UltimaFechaProgramada).ToList();
                        var _actividadesLista = ActividadesAgenda.ContainsKey(item.Nombre);
                        if (!_actividadesLista)
                        {
                            ActividadesAgenda.Add(item.Nombre, listaActividades);
                        }
                        else
                        {
                            ActividadesAgenda[item.Nombre].AddRange(listaActividades);
                        }

                    }
                    else if (item.Nombre.Contains("Atraso") || item.Nombre.Contains("AlDia")
                            || item.Nombre.Contains("Seguimiento") || item.Nombre.Contains("Manual")
                            || item.Nombre.Contains("Reasignado") || item.Nombre == "Culminado"
                            || item.Nombre.Contains("Culminado Deudor") || item.Nombre.Contains("Reservado Con Deuda")
                            || item.Nombre.Contains("Reservado Sin Deuda") || item.Nombre.Contains("Retirado")
                            || item.Nombre.Contains("Abandonado") || item.Nombre.Contains("Evaluacion")
                            || item.Nombre.Contains("Solicitud") || item.Nombre.Contains("Certificado")
                            || item.Nombre.Contains("1+ Cuota Atraso") || item.Nombre.Contains("PorAbandonar")
                            || item.Nombre.Contains("Por Contactar") || item.Nombre.Contains("En Negociacion")
                            || item.Nombre.Contains("En Cierre De Negociacion") || item.Nombre.Contains("Bic")
                            || item.Nombre.Contains("Acceso Temporal") || item.Nombre.Contains("Pre Reporte CR") 
                            || item.Nombre.Contains("Reportado CR"))
                    {
                        var actividades = _repAgendaTab.ObtenerActividadesOperaciones(item, this.IdAsesor, this.Filtros);

                        CantidadRN2 = _repAgendaTab.CantidadActividadesPorTabOperaciones(item, this.IdAsesor, this.Filtros);

                        foreach (var actividadProgramada in actividades)
                        {
                            var actividad = new ActividadAgendaDTO
                            {
                                Id = actividadProgramada.Id,
                                EstadoHoja = actividadProgramada.EstadoHoja,
                                CentroCosto = actividadProgramada.CentroCosto,
                                PEspecifico = actividadProgramada.PEspecifico,
                                Modalidad = actividadProgramada.Modalidad,
                                FechaPrimeraSesion = actividadProgramada.FechaPrimeraSesion,
                                ValidoAccesoTemporal = actividadProgramada.ValidoAccesoTemporal,
                                Contacto = actividadProgramada.Contacto,
                                CodigoFase = actividadProgramada.CodigoFase,
                                NombreTipoDato = actividadProgramada.NombreTipoDato,
                                Origen = actividadProgramada.Origen,
                                UltimaFechaProgramada = actividadProgramada.UltimaFechaProgramada,
                                IdAlumno = actividadProgramada.IdAlumno,
                                IdClasificacionPersona = actividadProgramada.IdClasificacionPersona,
                                IdOportunidad = actividadProgramada.IdOportunidad,
                                UltimoComentario = actividadProgramada.UltimoComentario,
                                IdActividadCabecera = actividadProgramada.IdActividadCabecera,
                                ActividadCabecera = actividadProgramada.ActividadCabecera,
                                Asesor = actividadProgramada.Asesor,
                                IdPersonal_Asignado = actividadProgramada.IdPersonal_Asignado,
                                IdCentroCosto = actividadProgramada.IdCentroCosto,
                                IdFaseOportunidad = actividadProgramada.IdFaseOportunidad,
                                ProbabilidadActualDesc = actividadProgramada.ProbabilidadActualDesc,
                                CategoriaNombre = actividadProgramada.CategoriaNombre,
                                CategoriaDescripcion = actividadProgramada.CategoriaDescripcion,
                                IdCategoriaOrigen = actividadProgramada.IdCategoriaOrigen,
                                ValidaLlamada = actividadProgramada.ValidaLlamada,
                                ReprogramacionManual = actividadProgramada.ReprogramacionManual,
                                ReprogramacionAutomatica = actividadProgramada.ReprogramacionAutomatica,
                                IdEstadoOportunidad = actividadProgramada.IdEstadoOportunidad,
                                IdTipoDato = actividadProgramada.IdTipoDato,
                                IdSubCategoriaDato = actividadProgramada.IdSubCategoriaDato,
                                DiasSinContactoManhana = actividadProgramada.DiasSinContactoManhana,
                                DiasSinContactoTarde = actividadProgramada.DiasSinContactoTarde,
                                ActividadesManhana = actividadProgramada.ActividadesManhana,
                                ActividadesTarde = actividadProgramada.ActividadesTarde,
                                IdMatriculaCabecera = actividadProgramada.IdMatriculaCabecera,
                                IdEstadoMatricula = actividadProgramada.IdEstadoMatricula,
                                CodigoMatricula = actividadProgramada.CodigoMatricula,
                                DNI = actividadProgramada.DNI,
                                IdPadre = actividadProgramada.IdPadre,
                                DiasAtrasoCuotaPago = actividadProgramada.DiasAtrasoCuotaPago,
                                EstadoMatricula = actividadProgramada.EstadoMatricula,
                                GrupoCurso = actividadProgramada.GrupoCurso,
                                SubEstadoMatricula = actividadProgramada.SubEstadoMatricula,
                                DiasSeguimiento = actividadProgramada.DiasSeguimiento,
                                DiasActividadesEjecutadas = actividadProgramada.DiasActividadesEjecutadas,
                                Tarifario = actividadProgramada.Tarifario,
                                FechaGrabacion = actividadProgramada.FechaGrabacion,
                                FechaVerificacion = actividadProgramada.FechaVerificacion,
                                ActividadTotalUltimos7Dias = actividadProgramada.ActividadTotalUltimos7Dias,
                                ActividadEjecutadaUltimos7Dias = actividadProgramada.ActividadEjecutadaUltimos7Dias,
                                NumeroDiasActividadesReprogramadas = actividadProgramada.NumeroDiasActividadesReprogramadas,
                                TotalDiaActual = actividadProgramada.TotalDiaActual,
                                EjecutadasDiaActual = actividadProgramada.EjecutadasDiaActual,
                                FechaSolicitud = actividadProgramada.FechaSolicitud,
                                TipoSolicitudOperaciones = actividadProgramada.TipoSolicitudOperaciones
                            };
                            listaActividades.Add(actividad);
                        }
                        listaActividades = listaActividades.OrderBy(x => x.UltimaFechaProgramada).ToList();
                        var _actividadesLista = ActividadesAgenda.ContainsKey(item.Nombre);
                        if (!_actividadesLista)
                        {
                            ActividadesAgenda.Add(item.Nombre, listaActividades);
                        }
                        else
                        {
                            ActividadesAgenda[item.Nombre].AddRange(listaActividades);
                        }
                    }
                    else if (item.Nombre.Contains("Profesores"))
                    {
                        var actividades = _repAgendaTab.ObtenerActividadesOperaciones(item, this.IdAsesor, this.Filtros);

                        CantidadRN2 = _repAgendaTab.CantidadActividadesPorTabOperaciones(item, this.IdAsesor, this.Filtros);

                        foreach (var actividadProgramada in actividades)
                        {
                            var _actividad = new ActividadAgendaDTO
                            {
                                Id = actividadProgramada.Id,
                                EstadoHoja = actividadProgramada.EstadoHoja,
                                CentroCosto = actividadProgramada.CentroCosto,
                                Contacto = actividadProgramada.Contacto,
                                CodigoFase = actividadProgramada.CodigoFase,
                                NombreTipoDato = actividadProgramada.NombreTipoDato,
                                Origen = actividadProgramada.Origen,
                                UltimaFechaProgramada = actividadProgramada.UltimaFechaProgramada,
                                IdAlumno = actividadProgramada.IdAlumno,
                                IdClasificacionPersona = actividadProgramada.IdClasificacionPersona,
                                IdOportunidad = actividadProgramada.IdOportunidad,
                                UltimoComentario = actividadProgramada.UltimoComentario,
                                IdActividadCabecera = actividadProgramada.IdActividadCabecera,
                                ActividadCabecera = actividadProgramada.ActividadCabecera,
                                Asesor = actividadProgramada.Asesor,
                                IdPersonal_Asignado = actividadProgramada.IdPersonal_Asignado,
                                IdCentroCosto = actividadProgramada.IdCentroCosto,
                                IdFaseOportunidad = actividadProgramada.IdFaseOportunidad,
                                ProbabilidadActualDesc = actividadProgramada.ProbabilidadActualDesc,
                                CategoriaNombre = actividadProgramada.CategoriaNombre,
                                CategoriaDescripcion = actividadProgramada.CategoriaDescripcion,
                                IdCategoriaOrigen = actividadProgramada.IdCategoriaOrigen,
                                ValidaLlamada = actividadProgramada.ValidaLlamada,
                                ReprogramacionManual = actividadProgramada.ReprogramacionManual,
                                ReprogramacionAutomatica = actividadProgramada.ReprogramacionAutomatica,
                                IdEstadoOportunidad = actividadProgramada.IdEstadoOportunidad,
                                IdTipoDato = actividadProgramada.IdTipoDato,
                                IdSubCategoriaDato = actividadProgramada.IdSubCategoriaDato,
                                DiasSinContactoManhana = actividadProgramada.DiasSinContactoManhana,
                                DiasSinContactoTarde = actividadProgramada.DiasSinContactoTarde,
                                ActividadesManhana = actividadProgramada.ActividadesManhana,
                                ActividadesTarde = actividadProgramada.ActividadesTarde,
                                IdMatriculaCabecera = actividadProgramada.IdMatriculaCabecera,
                                IdEstadoMatricula = actividadProgramada.IdEstadoMatricula,
                                CodigoMatricula = actividadProgramada.CodigoMatricula,
                                DNI = actividadProgramada.DNI,
                                IdPadre = actividadProgramada.IdPadre,
                                DiasAtrasoCuotaPago = actividadProgramada.DiasAtrasoCuotaPago,
                                EstadoMatricula = actividadProgramada.EstadoMatricula,
                                GrupoCurso = actividadProgramada.GrupoCurso,
                                SubEstadoMatricula = actividadProgramada.SubEstadoMatricula,
                                DiasSeguimiento = actividadProgramada.DiasSeguimiento,
                                DiasActividadesEjecutadas = actividadProgramada.DiasActividadesEjecutadas
                            };
                            listaActividades.Add(_actividad);
                        }
                        listaActividades = listaActividades.OrderBy(x => x.UltimaFechaProgramada).ToList();
                        var _actividadesLista = ActividadesAgenda.ContainsKey(item.Nombre);
                        if (!_actividadesLista)
                        {
                            ActividadesAgenda.Add(item.Nombre, listaActividades);
                        }
                        else
                        {
                            ActividadesAgenda[item.Nombre].AddRange(listaActividades);
                        }
                    }
                    else
                    {
                        var actividades = _repAgendaTab.ObtenerActividades(item, this.IdAsesor, this.Filtros);

                        if (item.Nombre == "RN2")
                        {
                            CantidadRN2 = _repAgendaTab.CantidadActividadesPorTab(item, this.IdAsesor, this.Filtros);
                        }
                        foreach (var actividadProgramada in actividades)
                        {
                            var _actividad = new ActividadAgendaDTO
                            {
                                Id = actividadProgramada.Id,
                                EstadoHoja = actividadProgramada.EstadoHoja,
                                CentroCosto = actividadProgramada.CentroCosto,
                                Contacto = actividadProgramada.Contacto,
                                CodigoFase = actividadProgramada.CodigoFase,
                                NombreTipoDato = actividadProgramada.NombreTipoDato,
                                Origen = actividadProgramada.Origen,
                                UltimaFechaProgramada = actividadProgramada.UltimaFechaProgramada,
                                IdAlumno = actividadProgramada.IdAlumno,
                                IdClasificacionPersona = actividadProgramada.IdClasificacionPersona,
                                IdOportunidad = actividadProgramada.IdOportunidad,
                                UltimoComentario = actividadProgramada.UltimoComentario,
                                IdActividadCabecera = actividadProgramada.IdActividadCabecera,
                                ActividadCabecera = actividadProgramada.ActividadCabecera,
                                Asesor = actividadProgramada.Asesor,
                                IdPersonal_Asignado = actividadProgramada.IdPersonal_Asignado,
                                IdCentroCosto = actividadProgramada.IdCentroCosto,
                                IdFaseOportunidad = actividadProgramada.IdFaseOportunidad,
                                ProbabilidadActualDesc = actividadProgramada.ProbabilidadActualDesc,
                                CategoriaNombre = actividadProgramada.CategoriaNombre,
                                CategoriaDescripcion= actividadProgramada.CategoriaDescripcion,
                                IdCategoriaOrigen = actividadProgramada.IdCategoriaOrigen,
                                ValidaLlamada = actividadProgramada.ValidaLlamada,
                                ReprogramacionManual = actividadProgramada.ReprogramacionManual,
                                ReprogramacionAutomatica = actividadProgramada.ReprogramacionAutomatica,
                                IdEstadoOportunidad = actividadProgramada.IdEstadoOportunidad,
                                IdTipoDato = actividadProgramada.IdTipoDato,
                                IdSubCategoriaDato = actividadProgramada.IdSubCategoriaDato,
                                DiasSinContactoManhana = actividadProgramada.DiasSinContactoManhana,
                                DiasSinContactoTarde = actividadProgramada.DiasSinContactoTarde,
                                ActividadesManhana = actividadProgramada.ActividadesManhana,
                                ActividadesTarde = actividadProgramada.ActividadesTarde,
                                IdMatriculaCabecera = actividadProgramada.IdMatriculaCabecera,
                                IdEstadoMatricula = actividadProgramada.IdEstadoMatricula,
                                CodigoMatricula = actividadProgramada.CodigoMatricula,
                                DNI = actividadProgramada.DNI,
                                IdPadre = actividadProgramada.IdPadre,
                                DiasAtrasoCuotaPago = actividadProgramada.DiasAtrasoCuotaPago,
                                EstadoMatricula = actividadProgramada.EstadoMatricula,
                                GrupoCurso = actividadProgramada.GrupoCurso,
                            };
                            listaActividades.Add(_actividad);
                        }
                        listaActividades = listaActividades.OrderBy(x => x.UltimaFechaProgramada).ToList();
                        var _actividadesLista = ActividadesAgenda.ContainsKey(item.Nombre);
                        if (!_actividadesLista)
                        {
                            ActividadesAgenda.Add(item.Nombre, listaActividades);
                        }
                        else
                        {
                            ActividadesAgenda[item.Nombre].AddRange(listaActividades);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene los Tabs y sus configuraciones para la Agenda del Asesor
        /// </summary>
        /// <returns></returns>
        public void ListaActividades()
        {
            try
            {
                this.tabsAgenda = _repAgendaTab.ObtenerTabsConfigurados(this.AreaTrabajo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// metodo que retorna una actividad detalle filtrado por actividad detalle
        /// </summary>
        /// <param name="idActividadDetalle"></param>
        /// <returns></returns>
        //public CompuestoActividadEjecutadaTempDTO ObtenerAgendaRealizadaRegistroTiempoReal(int idActividadDetalle)
        //{
        //    //return _repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(idActividadDetalle);
        //}

        /// <summary>
        /// Obtiene los tabs de la agenda
        /// </summary>
        /// <returns></returns>
        public void ObtenerTabAgenda()
        {
            //Si es false carga tabs sin logica mas tabs con logica
            if (!ValidacionTabs)
            {
                ObtenerTabsSinValidacion();
            }
            ObtenerTabsConValidacion();
        }

        /// <summary>
        /// Llena los estados para la habilitacion
        /// </summary>
        /// <param name="tabs">Lista de objetos de tipo TabAgendaDTO</param>
        /// <returns></returns>
        public void LlenarEstadosHabilitar(List<TabAgendaDTO> tabs)
        {
            foreach (var item in tabs)
            {
                if (!HabilitarEstados.ContainsKey(item.Nombre))
                {
                    HabilitarEstados.Add(item.Nombre, false);
                }
            }
        }

        /// <summary>
        /// Actualiza los estados para la habilitacion
        /// </summary>
        /// <param name="tabs">Lista de objetos de tipo TabAgendaDTO</param>
        /// <returns></returns>
        public void ActualizarEstadosHabilitar(List<TabAgendaDTO> tabs)
        {
            foreach (var item in tabs)
            {
                HabilitarEstados[item.Nombre] = true;
            }
        }

        /// <summary>
        /// Obtiene los tabs con validacion
        /// </summary>
        /// <returns></returns>
        public void ObtenerTabsConValidacion()
        {

            var tabsConValidacion = _repAgendaTab.ObtenerTabsConfiguradoConValidacion(this.AreaTrabajo);
            LlenarEstadosHabilitar(tabsConValidacion);

            var tabVencidasIPICPF = tabsConValidacion.Where(x => x.Numeracion == 1 && x.ValidarFecha == true).ToList();
            ObtenerActividadesPorTab(tabVencidasIPICPF);
            var listaActividadesIPICPF = ActividadesAgenda.Where(x => x.Key == tabVencidasIPICPF.FirstOrDefault().Nombre).FirstOrDefault();
            var primeraFechaVencidasIPICPF = listaActividadesIPICPF.Value.Select(x => x.UltimaFechaProgramada).FirstOrDefault();
            string[] tabsNoProgramadas = new string[2];
            var fechaip = DateTime.Now;
            //if (this.IdAsesor == 5164 || this.IdAsesor == 5165 || this.IdAsesor == 5126 || this.IdAsesor == 5166 || this.IdAsesor == 5068)
            //{
            //    fechaip = DateTime.Now.AddHours(-1);
            //}
            //MEXICO
            if (this.DiferenciaHoraria != null)
            {
                fechaip = DateTime.Now.AddHours((int)DiferenciaHoraria);
            }
            if ((primeraFechaVencidasIPICPF == null || primeraFechaVencidasIPICPF > fechaip) && this.AreaTrabajo != "OP")//si primeraFechaVencidasIPICPF es mayor a la fecha actual no hay vencidas
            {
                var tabProgramadaManual = tabsConValidacion.Where(x => x.Numeracion == 2 && x.ValidarFecha == true).ToList();
                //cargamos los datos del tab de programadas manual
                ObtenerActividadesPorTab(tabProgramadaManual);
                //preguntamos si tenemos vencidas
                var listaActividadesProgramadaManual = ActividadesAgenda.Where(x => x.Key == tabProgramadaManual.FirstOrDefault().Nombre).FirstOrDefault();
                var primeraFechaVencidasProgramadaManual = listaActividadesProgramadaManual.Value.Select(x => x.UltimaFechaProgramada).FirstOrDefault();

                var fecha = DateTime.Now;
                //if(this.IdAsesor== 5164 || this.IdAsesor == 5165 || this.IdAsesor == 5126 || this.IdAsesor == 5166 || this.IdAsesor == 5068)
                //{
                //    fecha = DateTime.Now.AddHours(-1);
                //}
                if (this.DiferenciaHoraria != null)
                {
                    fecha = DateTime.Now.AddHours((int)DiferenciaHoraria);
                }
                if (primeraFechaVencidasProgramadaManual == null || primeraFechaVencidasProgramadaManual > fecha)
                {
                    //Si no hay programadas manual vencidas cargamos no programadas (1sol, +1sol)




                    var tabVencidasNoProg1SolMas1Sol = tabsConValidacion.Where(x => x.Numeracion == 3 && !x.ValidarFecha).ToList();//cantidad registros de NoProg1Sol, NoProg+1Sol

                    ObtenerActividadesPorTab(tabVencidasNoProg1SolMas1Sol);

                    //int cantidadOportunidadesNoProgramadas2 = 0;
                    //var primerNombre = tabVencidasNoProg1SolMas1Sol.FirstOrDefault().Nombre;
                    //tabsNoProgramadas[0] = primerNombre;
                    //calculamos la  cantidad registros en los 2 tabs

                    var actividadesNoProg1solMas1Sol = ActividadesAgenda.Where(x => tabVencidasNoProg1SolMas1Sol.DistinctBy(w => w.Nombre).Select(w => w.Nombre).Contains(x.Key)).ToList().Select(x => x.Value).ToList();

                    var cantidadOportunidadesNoProgramadas2 = 0;
                    foreach (var item in actividadesNoProg1solMas1Sol)
                    {
                        cantidadOportunidadesNoProgramadas2 += item.Count;
                    }
                    //foreach (var item in tabVencidasNoProg1SolMas1Sol)
                    //{
                    //    cantidadOportunidadesNoProgramadas2 += _repAgendaTab.ObtenerActividadesNoProgramadaCantidad(item, IdAsesor, Filtros);
                    //    //if (item.Nombre != primerNombre)
                    //    //{
                    //    //    tabsNoProgramadas[1] = item.Nombre;
                    //    //}
                    //}

                    //if (cantidadOportunidadesTipo2 == 0)
                    if (cantidadOportunidadesNoProgramadas2 == 0)// si no tenemos registros en los 2 tabs de no programadas
                    {
                        //cargamos los tabs

                        //var tabProgramaManual = tabsConValidacion.Where(x => x.Numeracion == 3 && x.ValidarFecha == true).ToList();
                        //ObtenerActividadesPorTab(tabProgramaManual);
                        //var listaActividadesProgramadasManual = ActividadesAgenda.Where(x => x.Key == tabProgramaManual.FirstOrDefault().Nombre).FirstOrDefault();
                        //var primeraFechaProgramadaManual = listaActividadesProgramadasManual.Value.Select(x => x.UltimaFechaProgramada).FirstOrDefault();
                        ////if (primeraFechaProgramadaManual == null || primeraFechaProgramadaManual > DateTime.Now)
                        //if (true)
                        //{
                        //    var tabsTipo3 = tabsConValidacion.Where(x => x.Numeracion == 4 && !x.ValidarFecha).ToList();
                        //    ObtenerActividadesPorTab(tabsTipo3);
                        //    ActualizarEstadosHabilitar(tabsTipo3);
                        //    //ActividadesAgenda[tabsNoProgramadas[0]] = new List<ActividadAgendaDTO>();
                        //    //ActividadesAgenda[tabsNoProgramadas[1]] = new List<ActividadAgendaDTO>();
                        //}
                        //ActualizarEstadosHabilitar(tabProgramaManual);
                        var tabProgramaAutomatica_Rn2 = tabsConValidacion.Where(x => x.Numeracion == 4).ToList();
                        //ObtenerActividadesPorTab(tabProgramaManual);
                        //var listaActividadesProgramadasManual = ActividadesAgenda.Where(x => x.Key == tabProgramaManual.FirstOrDefault().Nombre).FirstOrDefault();
                        //var primeraFechaProgramadaManual = listaActividadesProgramadasManual.Value.Select(x => x.UltimaFechaProgramada).FirstOrDefault();
                        //if (primeraFechaProgramadaManual == null || primeraFechaProgramadaManual > DateTime.Now)
                        //if (true)
                        //{
                        //var tabsTipo3 = tabsConValidacion.Where(x => x.Numeracion == 4 && !x.ValidarFecha).ToList();
                        ObtenerActividadesPorTab(tabProgramaAutomatica_Rn2);
                        ActualizarEstadosHabilitar(tabProgramaAutomatica_Rn2);
                        //ActividadesAgenda[tabsNoProgramadas[0]] = new List<ActividadAgendaDTO>();
                        //ActividadesAgenda[tabsNoProgramadas[1]] = new List<ActividadAgendaDTO>();
                        //}
                        //ActualizarEstadosHabilitar(tabProgramaManual);
                    }
                    else
                    {
                        //ObtenerActividadesPorTab(tabVencidasNoProg1SolMas1Sol);
                    }
                    LogCarlos = LogCarlos + "  cantidad registros de NoProg1Sol, NoProg+1Sol" + tabVencidasNoProg1SolMas1Sol.Count();
                    ActualizarEstadosHabilitar(tabVencidasNoProg1SolMas1Sol);
                }
                ActualizarEstadosHabilitar(tabProgramadaManual);
                //ActividadesAgenda[tabVencidasIPICPF.FirstOrDefault().Nombre] = new List<ActividadAgendaDTO>();
            }
            ActualizarEstadosHabilitar(tabVencidasIPICPF);


        }

        /// <summary>
        /// Obtiene los tabs de la agenda que no requieren validacion
        /// </summary>
        /// <returns></returns>
        public void ObtenerTabsSinValidacion()
        {
            var tabsAgendaNoValidados = _repAgendaTab.ObtenerTabsConfiguradosSinValidacion(this.AreaTrabajo);
            ObtenerActividadesPorTab(tabsAgendaNoValidados);
        }

        /// <summary>
        /// Obtiene las actividades por tab
        /// </summary>
        /// <param name="tabAgendas">Lista de objetos de tipo TabAgendaDTO</param>
        /// <returns></returns>
        public void ObtenerActividadesPorTab(List<TabAgendaDTO> tabAgendas)
        {

            Dictionary<string, List<ActividadAgendaDTO>> actividadesAux = new Dictionary<string, List<ActividadAgendaDTO>>();
            foreach (var item in tabAgendas)
            {
                if (item.VisualizarActividad)
                {
                    var ListaActividades = new List<ActividadAgendaDTO>();
                    if (item.CargarInformacionInicial)
                    {
                        if (item.VistaBaseDatos.Contains("ActividadRealizadaLlamada"))
                        {
                            //this.CargarActividadesRealizadas(item);
                        }
                        else if (item.VistaBaseDatos.Contains("ActividadProgramada"))
                        {
                            var actividades = _repAgendaTab.ObtenerActividadesProgramada(item, this.IdAsesor, this.Filtros);
                            foreach (var actividadProgramada in actividades)
                            {
                                var _actividad = new ActividadAgendaDTO
                                {
                                    Id = actividadProgramada.Id,
                                    //_actividad.TipoActividad = actividadProgramada.TipoActividad;
                                    EstadoHoja = actividadProgramada.EstadoHoja,
                                    CentroCosto = actividadProgramada.CentroCosto,
                                    Contacto = actividadProgramada.Contacto,
                                    //_actividad.IdCargo = actividadProgramada.IdCargo == null ? 0 : actividadProgramada.IdCargo.Value;
                                    //_actividad.IdAFormacion = actividadProgramada.IdAFormacion == null ? 0 : actividadProgramada.IdAFormacion.Value;
                                    //_actividad.IdATrabajo = actividadProgramada.IdATrabajo == null ? 0 : actividadProgramada.IdATrabajo.Value;
                                    //_actividad.IdIndustria = actividadProgramada.IdIndustria == null ? 0 : actividadProgramada.IdIndustria.Value;
                                    CodigoFase = actividadProgramada.CodigoFase,
                                    NombreTipoDato = actividadProgramada.NombreTipoDato,
                                    Origen = actividadProgramada.Origen,
                                    UltimaFechaProgramada = actividadProgramada.UltimaFechaProgramada,
                                    IdAlumno = actividadProgramada.IdAlumno,
                                    IdClasificacionPersona = actividadProgramada.IdClasificacionPersona,
                                    IdOportunidad = actividadProgramada.IdOportunidad,
                                    UltimoComentario = actividadProgramada.UltimoComentario,
                                    IdActividadCabecera = actividadProgramada.IdActividadCabecera,
                                    //_actividad.ActividadesVencidas = actividadProgramada.ActividadesVencidas;
                                    ReprogramacionManual = actividadProgramada.ReprogramacionManual,
                                    ReprogramacionAutomatica = actividadProgramada.ReprogramacionAutomatica,
                                    ActividadCabecera = actividadProgramada.ActividadCabecera,
                                    Asesor = actividadProgramada.Asesor,
                                    IdPersonal_Asignado = actividadProgramada.IdPersonal_Asignado,
                                    IdCentroCosto = actividadProgramada.IdCentroCosto,
                                    IdFaseOportunidad = actividadProgramada.IdFaseOportunidad,
                                    IdTipoDato = actividadProgramada.IdTipoDato,
                                    ProbabilidadActualDesc = actividadProgramada.ProbabilidadActualDesc,
                                    CategoriaNombre = actividadProgramada.CategoriaNombre,
                                    CategoriaDescripcion = actividadProgramada.CategoriaDescripcion,
                                    IdCategoriaOrigen = actividadProgramada.IdCategoriaOrigen,
                                    IdSubCategoriaDato = actividadProgramada.IdSubCategoriaDato,
                                    IdEstadoOportunidad = actividadProgramada.IdEstadoOportunidad,
                                    ValidaLlamada = actividadProgramada.ValidaLlamada,
                                    DiasSinContactoManhana = actividadProgramada.DiasSinContactoManhana,
                                    DiasSinContactoTarde = actividadProgramada.DiasSinContactoTarde,
                                    ActividadesManhana = actividadProgramada.ActividadesManhana,
                                    ActividadesTarde = actividadProgramada.ActividadesTarde,
                                    IdPadre = actividadProgramada.IdPadre
                                };
                                //_actividad.EstadoOportunidad = actividadProgramada.EstadoOportunidad;
                                ListaActividades.Add(_actividad);
                            }

                        }
                        else if (item.VistaBaseDatos.Contains("ActividadNoProgramada") && item.Probabilidad.Contains("Muy Alta"))
                        {
                            LogCarlos = LogCarlos + " / idestadooportunidad" + item.IdEstadoOportunidad + " " + item.Nombre + " " + item.Probabilidad + " " + item.Id.ToString() + " " + this.IdAsesor.ToString() + " " + this.Filtros.Count().ToString();
                            var actividades = _repAgendaTab.ObtenerActividadesNoProgramada(item, this.IdAsesor, this.Filtros);
                            LogCarlos = LogCarlos + ": total de ObtenerActividadesNoProgramada: " + actividades.Count().ToString();
                            foreach (var actividadProgramada in actividades)
                            {
                                var _actividad = new ActividadAgendaDTO
                                {
                                    Id = actividadProgramada.Id,
                                    //_actividad.TipoActividad = actividadProgramada.TipoActividad;
                                    EstadoHoja = actividadProgramada.EstadoHoja,
                                    CentroCosto = actividadProgramada.CentroCosto,
                                    Contacto = actividadProgramada.Contacto,
                                    //_actividad.IdCargo = actividadProgramada.IdCargo == null ? 0 : actividadProgramada.IdCargo.Value;
                                    //_actividad.IdAFormacion = actividadProgramada.IdAFormacion == null ? 0 : actividadProgramada.IdAFormacion.Value;
                                    //_actividad.IdATrabajo = actividadProgramada.IdATrabajo == null ? 0 : actividadProgramada.IdATrabajo.Value;
                                    //_actividad.IdIndustria = actividadProgramada.IdIndustria == null ? 0 : actividadProgramada.IdIndustria.Value;
                                    CodigoFase = actividadProgramada.CodigoFase,
                                    NombreTipoDato = actividadProgramada.NombreTipoDato,
                                    Origen = actividadProgramada.Origen,
                                    UltimaFechaProgramada = actividadProgramada.UltimaFechaProgramada,
                                    IdAlumno = actividadProgramada.IdAlumno,
                                    IdClasificacionPersona = actividadProgramada.IdClasificacionPersona,
                                    IdOportunidad = actividadProgramada.IdOportunidad,
                                    UltimoComentario = actividadProgramada.UltimoComentario,
                                    IdActividadCabecera = actividadProgramada.IdActividadCabecera,
                                    //_actividad.ActividadesVencidas = actividadProgramada.ActividadesVencidas;
                                    ReprogramacionManual = actividadProgramada.ReprogramacionManual,
                                    ReprogramacionAutomatica = actividadProgramada.ReprogramacionAutomatica,
                                    ActividadCabecera = actividadProgramada.ActividadCabecera,
                                    Asesor = actividadProgramada.Asesor,
                                    IdPersonal_Asignado = actividadProgramada.IdPersonal_Asignado,
                                    IdCentroCosto = actividadProgramada.IdCentroCosto,
                                    IdFaseOportunidad = actividadProgramada.IdFaseOportunidad,
                                    IdTipoDato = actividadProgramada.IdTipoDato,
                                    ProbabilidadActualDesc = actividadProgramada.ProbabilidadActualDesc,
                                    CategoriaNombre = actividadProgramada.CategoriaNombre,
                                    CategoriaDescripcion = actividadProgramada.CategoriaDescripcion,
                                    IdCategoriaOrigen = actividadProgramada.IdCategoriaOrigen,
                                    IdSubCategoriaDato = actividadProgramada.IdSubCategoriaDato,
                                    IdEstadoOportunidad = actividadProgramada.IdEstadoOportunidad,
                                    ValidaLlamada = actividadProgramada.ValidaLlamada,
                                    IdPadre = actividadProgramada.IdPadre
                                };
                                //_actividad.EstadoOportunidad = actividadProgramada.EstadoOportunidad;
                                ListaActividades.Add(_actividad);
                            }

                        }
                        else
                        {
                            var actividades = _repAgendaTab.ObtenerActividades(item, this.IdAsesor, this.Filtros);
                            foreach (var actividadProgramada in actividades)
                            {
                                var actividad = new ActividadAgendaDTO
                                {
                                    Id = actividadProgramada.Id,
                                    //_actividad.TipoActividad = actividadProgramada.TipoActividad;
                                    EstadoHoja = actividadProgramada.EstadoHoja,
                                    CentroCosto = actividadProgramada.CentroCosto,
                                    Contacto = actividadProgramada.Contacto,
                                    //_actividad.IdCargo = actividadProgramada.IdCargo == null ? 0 : actividadProgramada.IdCargo.Value;
                                    //_actividad.IdAFormacion = actividadProgramada.IdAFormacion == null ? 0 : actividadProgramada.IdAFormacion.Value;
                                    //_actividad.IdATrabajo = actividadProgramada.IdATrabajo == null ? 0 : actividadProgramada.IdATrabajo.Value;
                                    //_actividad.IdIndustria = actividadProgramada.IdIndustria == null ? 0 : actividadProgramada.IdIndustria.Value;
                                    CodigoFase = actividadProgramada.CodigoFase,
                                    NombreTipoDato = actividadProgramada.NombreTipoDato,
                                    Origen = actividadProgramada.Origen,
                                    UltimaFechaProgramada = actividadProgramada.UltimaFechaProgramada,
                                    IdAlumno = actividadProgramada.IdAlumno,
                                    IdClasificacionPersona = actividadProgramada.IdClasificacionPersona,
                                    IdOportunidad = actividadProgramada.IdOportunidad,
                                    UltimoComentario = actividadProgramada.UltimoComentario,
                                    IdActividadCabecera = actividadProgramada.IdActividadCabecera,
                                    //_actividad.ActividadesVencidas = actividadProgramada.ActividadesVencidas;
                                    ReprogramacionManual = actividadProgramada.ReprogramacionManual,
                                    ReprogramacionAutomatica = actividadProgramada.ReprogramacionAutomatica,
                                    ActividadCabecera = actividadProgramada.ActividadCabecera,
                                    Asesor = actividadProgramada.Asesor,
                                    IdPersonal_Asignado = actividadProgramada.IdPersonal_Asignado,
                                    IdCentroCosto = actividadProgramada.IdCentroCosto,
                                    IdFaseOportunidad = actividadProgramada.IdFaseOportunidad,
                                    IdTipoDato = actividadProgramada.IdTipoDato,
                                    ProbabilidadActualDesc = actividadProgramada.ProbabilidadActualDesc,
                                    CategoriaNombre = actividadProgramada.CategoriaNombre,
                                    CategoriaDescripcion = actividadProgramada.CategoriaDescripcion,
                                    IdCategoriaOrigen = actividadProgramada.IdCategoriaOrigen,
                                    IdSubCategoriaDato = actividadProgramada.IdSubCategoriaDato,
                                    IdEstadoOportunidad = actividadProgramada.IdEstadoOportunidad,
                                    ValidaLlamada = actividadProgramada.ValidaLlamada,
                                    DiasSinContactoManhana = actividadProgramada.DiasSinContactoManhana,
                                    DiasSinContactoTarde = actividadProgramada.DiasSinContactoTarde,
                                    ActividadesManhana = actividadProgramada.ActividadesManhana,
                                    ActividadesTarde = actividadProgramada.ActividadesTarde,
                                    IdPadre = actividadProgramada.IdPadre
                                };
                                //_actividad.EstadoOportunidad = actividadProgramada.EstadoOportunidad;
                                ListaActividades.Add(actividad);
                            }
                        }
                    }
                    var actividadesLista = actividadesAux.ContainsKey(item.Nombre);
                    if (!actividadesLista)
                    {
                        actividadesAux.Add(item.Nombre, ListaActividades);
                    }
                    else
                    {
                        actividadesAux[item.Nombre].AddRange(ListaActividades);
                    }
                }
            }
            foreach (var item in actividadesAux)
            {
                if (!item.Key.Contains("No Prog"))
                {
                    var datos = item.Value;
                    datos = datos.OrderBy(x => x.UltimaFechaProgramada).ToList();
                    ActividadesAgenda.Add(item.Key, datos);
                }
                else
                {
                    var datos = item.Value;
                    ActividadesAgenda.Add(item.Key, datos);
                }

            }
        }

    }
}
