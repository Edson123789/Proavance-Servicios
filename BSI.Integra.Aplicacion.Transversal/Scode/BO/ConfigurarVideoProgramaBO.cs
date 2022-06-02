using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using static BSI.Integra.Aplicacion.Base.BO.Enum;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Planificacion/ConfigurarVideoPrograma
    /// Autor: Jorge Rivera Tito - Gian Miranda
    /// Fecha: 01/03/2021
    /// <summary>
    /// BO para la logica de la configuracion del video del programa
    /// </summary>
    public class ConfigurarVideoProgramaBO : BaseBO
    {
        /// Propiedades	                                Significado
        /// -----------	                                ------------
        /// IdPGeneral                                  Id del programa general (PK de la tabla pla.T_PGeneral)
        /// IdDocumentoSeccionPw                        Id de la seccion del documento (PK de la tabla pla.T_DocumentoSeccion_PW)
        /// VideoId                                     Guid del Video
        /// TotalMinutos                                Cadena con el total de minutos
        /// Archivo                                     Cadena con el nombre del archivo
        /// NroDiapositivas                             Cadena con el numero de diapositivas
        /// Configurado                                 Flag para validar si se encuentra configurado
        /// ConImagenVideo                              Flag para determinar si el video tiene imagen
        /// ImagenVideoNombre                           Nombre de la imagen del video
        /// ImagenVideoAncho                            Ancho de la imagen del video
        /// ImagenVideoAlto                             Alto de la imagen del video
        /// ImagenVideoPosicionX                        Posicion de la imagen X del video
        /// ImagenVideoPosicionY                        Posicion de la imagen Y del video
        /// ConImagenDiapositiva                        Flag para determinar si la diapositiva tiene imagen
        /// ImagenDiapositivaNombre                     Nombre de la imagen de la diapositiva
        /// ImagenDiapositivaAncho                      Ancho de la imagen de la diapositiva
        /// ImagenDiapositivaAlto                       Alto de la imagen de la diapositiva
        /// ImagenDiapositivaPosicionX                  Posicion de la imagen Y de la diapositiva
        /// ImagenDiapositivaPosicionY                  Posicion de la imagen Y de la diapositiva
        /// NumeroFila                                  Numero de la fila

        public int IdPgeneral { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public string VideoId { get; set; }
        public string VideoIdBrightcove { get; set; }
        public string TotalMinutos { get; set; }
        public string Archivo { get; set; }
        public string NroDiapositivas { get; set; }
        public bool Configurado { get; set; }
        public bool ConImagenVideo { get; set; }
        public string ImagenVideoNombre { get; set; }
        public string ImagenVideoAncho { get; set; }
        public string ImagenVideoAlto { get; set; }
        public string ImagenVideoPosicionX { get; set; }
        public string ImagenVideoPosicionY { get; set; }

        public bool ConImagenDiapositiva { get; set; }
        public string ImagenDiapositivaNombre { get; set; }
        public string ImagenDiapositivaAncho { get; set; }
        public string ImagenDiapositivaAlto { get; set; }
        public string ImagenDiapositivaPosicionX { get; set; }
        public string ImagenDiapositivaPosicionY { get; set; }

        public int NumeroFila { get; set; }
        public List<SesionConfigurarVideoBO> SesionConfigurarVideo { get; set; }

        private integraDBContext _integraDBContext;

        private ConfigurarVideoProgramaRepositorio _repConfigurarVideoPrograma;
        private PgeneralRepositorio _repPGeneral;

        public ConfigurarVideoProgramaBO()
        {
            _repPGeneral = new PgeneralRepositorio();
            _repConfigurarVideoPrograma = new ConfigurarVideoProgramaRepositorio();
        }

        public ConfigurarVideoProgramaBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repPGeneral = new PgeneralRepositorio(_integraDBContext);
            _repConfigurarVideoPrograma = new ConfigurarVideoProgramaRepositorio(_integraDBContext);
        }

        /// <summary>
        /// Obtiene la estructura en formato de Lista de un programa general
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Lista de objeto (EstructuraCapituloProgramaBO)</returns>
        public List<EstructuraCapituloProgramaBO> ObtenerEstructuraCapituloProgramaPorIdPGeneral(int idPGeneral)
        {
            List<EstructuraCapituloProgramaBO> lista = new List<EstructuraCapituloProgramaBO>();

            var listadoVideosPrograma = _repConfigurarVideoPrograma.ObtenerPreConfigurarVideoPrograma(idPGeneral);

            var listadoEstructura = (from x in listadoVideosPrograma
                                     group x by x.NumeroFila into newGroup
                                     select newGroup).ToList();

            foreach (var item in listadoEstructura)
            {
                EstructuraCapituloProgramaBO estructuraCapituloPrograma = new EstructuraCapituloProgramaBO();
                estructuraCapituloPrograma.OrdenFila = item.Key;
                foreach (var itemRegistros in item)
                {
                    switch (itemRegistros.NombreTitulo)
                    {
                        case "Capitulo":
                            estructuraCapituloPrograma.Nombre = itemRegistros.Nombre;
                            estructuraCapituloPrograma.Capitulo = itemRegistros.Contenido;
                            estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                            estructuraCapituloPrograma.IdPgeneral = itemRegistros.IdPgeneral;
                            estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                            estructuraCapituloPrograma.VideoId = itemRegistros.VideoId;
                            estructuraCapituloPrograma.Archivo = itemRegistros.Archivo;
                            estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                            estructuraCapituloPrograma.ConImagenVideo = itemRegistros.ConImagenVideo;
                            estructuraCapituloPrograma.ImagenVideoNombre = itemRegistros.ImagenVideoNombre;
                            estructuraCapituloPrograma.ImagenVideoAncho = itemRegistros.ImagenVideoAncho;
                            estructuraCapituloPrograma.ImagenVideoAlto = itemRegistros.ImagenVideoAlto;
                            estructuraCapituloPrograma.ConImagenDiapositiva = itemRegistros.ConImagenDiapositiva;
                            estructuraCapituloPrograma.ImagenDiapositivaNombre = itemRegistros.ImagenDiapositivaNombre;
                            estructuraCapituloPrograma.ImagenDiapositivaAncho = itemRegistros.ImagenDiapositivaAncho;
                            estructuraCapituloPrograma.ImagenDiapositivaAlto = itemRegistros.ImagenDiapositivaAlto;
                            estructuraCapituloPrograma.ImagenVideoPosicionX = itemRegistros.ImagenVideoPosicionX;
                            estructuraCapituloPrograma.ImagenVideoPosicionY = itemRegistros.ImagenVideoPosicionY;
                            estructuraCapituloPrograma.ImagenDiapositivaPosicionX = itemRegistros.ImagenDiapositivaPosicionX;
                            estructuraCapituloPrograma.ImagenDiapositivaPosicionY = itemRegistros.ImagenDiapositivaPosicionY;
                            //2
                            estructuraCapituloPrograma.Minuto = itemRegistros.Minuto;
                            estructuraCapituloPrograma.IdTipoVista = itemRegistros.IdTipoVista;
                            estructuraCapituloPrograma.NroDiapositiva = itemRegistros.NroDiapositiva;
                            estructuraCapituloPrograma.ConLogoVideo = itemRegistros.ConLogoVideo;
                            estructuraCapituloPrograma.ConLogoDiapositiva = itemRegistros.ConLogoDiapositiva;                            
                            estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;                            
                            break;
                        case "Sesion":
                            estructuraCapituloPrograma.Sesion = itemRegistros.Contenido;
                            estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                            estructuraCapituloPrograma.IdDocumentoSeccionPw = itemRegistros.Id;
                            estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                            estructuraCapituloPrograma.VideoId = itemRegistros.VideoId;
                            estructuraCapituloPrograma.Archivo = itemRegistros.Archivo;
                            estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                            estructuraCapituloPrograma.ConImagenVideo = itemRegistros.ConImagenVideo;
                            estructuraCapituloPrograma.ImagenVideoNombre = itemRegistros.ImagenVideoNombre;
                            estructuraCapituloPrograma.ImagenVideoAncho = itemRegistros.ImagenVideoAncho;
                            estructuraCapituloPrograma.ImagenVideoAlto = itemRegistros.ImagenVideoAlto;
                            estructuraCapituloPrograma.ConImagenDiapositiva = itemRegistros.ConImagenDiapositiva;
                            estructuraCapituloPrograma.ImagenDiapositivaNombre = itemRegistros.ImagenDiapositivaNombre;
                            estructuraCapituloPrograma.ImagenDiapositivaAncho = itemRegistros.ImagenDiapositivaAncho;
                            estructuraCapituloPrograma.ImagenDiapositivaAlto = itemRegistros.ImagenDiapositivaAlto;
                            estructuraCapituloPrograma.ImagenVideoPosicionX = itemRegistros.ImagenVideoPosicionX;
                            estructuraCapituloPrograma.ImagenVideoPosicionY = itemRegistros.ImagenVideoPosicionY;
                            estructuraCapituloPrograma.ImagenDiapositivaPosicionX = itemRegistros.ImagenDiapositivaPosicionX;
                            estructuraCapituloPrograma.ImagenDiapositivaPosicionY = itemRegistros.ImagenDiapositivaPosicionY;
                            //2
                            estructuraCapituloPrograma.Minuto = itemRegistros.Minuto;
                            estructuraCapituloPrograma.IdTipoVista = itemRegistros.IdTipoVista;
                            estructuraCapituloPrograma.NroDiapositiva = itemRegistros.NroDiapositiva;
                            estructuraCapituloPrograma.ConLogoVideo = itemRegistros.ConLogoVideo;
                            estructuraCapituloPrograma.ConLogoDiapositiva = itemRegistros.ConLogoDiapositiva;
                            estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                            break;
                        case "SubSeccion":
                            estructuraCapituloPrograma.SubSesion = itemRegistros.Contenido;
                            if (!string.IsNullOrEmpty(estructuraCapituloPrograma.SubSesion))
                            {
                                estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                estructuraCapituloPrograma.IdDocumentoSeccionPw = itemRegistros.Id;
                                estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                estructuraCapituloPrograma.VideoId = itemRegistros.VideoId;
                                estructuraCapituloPrograma.Archivo = itemRegistros.Archivo;
                                estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                estructuraCapituloPrograma.ConImagenVideo = itemRegistros.ConImagenVideo;
                                estructuraCapituloPrograma.ImagenVideoNombre = itemRegistros.ImagenVideoNombre;
                                estructuraCapituloPrograma.ImagenVideoAncho = itemRegistros.ImagenVideoAncho;
                                estructuraCapituloPrograma.ImagenVideoAlto = itemRegistros.ImagenVideoAlto;
                                estructuraCapituloPrograma.ConImagenDiapositiva = itemRegistros.ConImagenDiapositiva;
                                estructuraCapituloPrograma.ImagenDiapositivaNombre = itemRegistros.ImagenDiapositivaNombre;
                                estructuraCapituloPrograma.ImagenDiapositivaAncho = itemRegistros.ImagenDiapositivaAncho;
                                estructuraCapituloPrograma.ImagenDiapositivaAlto = itemRegistros.ImagenDiapositivaAlto;
                                estructuraCapituloPrograma.ImagenVideoPosicionX = itemRegistros.ImagenVideoPosicionX;
                                estructuraCapituloPrograma.ImagenVideoPosicionY = itemRegistros.ImagenVideoPosicionY;
                                estructuraCapituloPrograma.ImagenDiapositivaPosicionX = itemRegistros.ImagenDiapositivaPosicionX;
                                estructuraCapituloPrograma.ImagenDiapositivaPosicionY = itemRegistros.ImagenDiapositivaPosicionY;
                                //2
                                estructuraCapituloPrograma.Minuto = itemRegistros.Minuto;
                                estructuraCapituloPrograma.IdTipoVista = itemRegistros.IdTipoVista;
                                estructuraCapituloPrograma.NroDiapositiva = itemRegistros.NroDiapositiva;
                                estructuraCapituloPrograma.ConLogoVideo = itemRegistros.ConLogoVideo;
                                estructuraCapituloPrograma.ConLogoDiapositiva = itemRegistros.ConLogoDiapositiva;
                                estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                            }
                            break;
                        default:
                            estructuraCapituloPrograma.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);                            
                            break;
                    }
                }
                //Comentado
                lista.Add(estructuraCapituloPrograma);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene la estructura en formato de Lista de un programa general
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Lista de objeto (EstructuraCapituloProgramaBO)</returns>
        public List<EstructuraCapituloProgramaBO> ObtenerEstructuraCapituloProgramaPorIdPGeneralDescarga(int idPGeneral)
        {
            List<EstructuraCapituloProgramaBO> lista = new List<EstructuraCapituloProgramaBO>();

            var listadoVideosPrograma = _repConfigurarVideoPrograma.ObtenerPreConfigurarVideoProgramaDescarga(idPGeneral);

            if (listadoVideosPrograma != null)
            {
                var listadoEstructura = (from x in listadoVideosPrograma
                                         group x by x.NumeroFila into newGroup
                                         select newGroup).ToList();

                foreach (var item in listadoEstructura)
                {
                    EstructuraCapituloProgramaBO estructuraCapituloPrograma = new EstructuraCapituloProgramaBO();
                    estructuraCapituloPrograma.OrdenFila = item.Key;
                    foreach (var itemRegistros in item)
                    {
                        switch (itemRegistros.NombreTitulo)
                        {
                            case "Capitulo":
                                estructuraCapituloPrograma.Nombre = itemRegistros.Nombre;
                                estructuraCapituloPrograma.Capitulo = itemRegistros.Contenido;
                                estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                estructuraCapituloPrograma.IdPgeneral = itemRegistros.IdPgeneral;
                                estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                estructuraCapituloPrograma.VideoId = itemRegistros.VideoId;
                                estructuraCapituloPrograma.Archivo = itemRegistros.Archivo;
                                estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                estructuraCapituloPrograma.ConImagenVideo = itemRegistros.ConImagenVideo;
                                estructuraCapituloPrograma.ImagenVideoNombre = itemRegistros.ImagenVideoNombre;
                                estructuraCapituloPrograma.ImagenVideoAncho = itemRegistros.ImagenVideoAncho;
                                estructuraCapituloPrograma.ImagenVideoAlto = itemRegistros.ImagenVideoAlto;
                                estructuraCapituloPrograma.ConImagenDiapositiva = itemRegistros.ConImagenDiapositiva;
                                estructuraCapituloPrograma.ImagenDiapositivaNombre = itemRegistros.ImagenDiapositivaNombre;
                                estructuraCapituloPrograma.ImagenDiapositivaAncho = itemRegistros.ImagenDiapositivaAncho;
                                estructuraCapituloPrograma.ImagenDiapositivaAlto = itemRegistros.ImagenDiapositivaAlto;
                                estructuraCapituloPrograma.ImagenVideoPosicionX = itemRegistros.ImagenVideoPosicionX;
                                estructuraCapituloPrograma.ImagenVideoPosicionY = itemRegistros.ImagenVideoPosicionY;
                                estructuraCapituloPrograma.ImagenDiapositivaPosicionX = itemRegistros.ImagenDiapositivaPosicionX;
                                estructuraCapituloPrograma.ImagenDiapositivaPosicionY = itemRegistros.ImagenDiapositivaPosicionY;
                                //2
                                estructuraCapituloPrograma.Minuto = itemRegistros.Minuto;
                                estructuraCapituloPrograma.IdTipoVista = itemRegistros.IdTipoVista;
                                estructuraCapituloPrograma.NroDiapositiva = itemRegistros.NroDiapositiva;
                                estructuraCapituloPrograma.ConLogoVideo = itemRegistros.ConLogoVideo;
                                estructuraCapituloPrograma.ConLogoDiapositiva = itemRegistros.ConLogoDiapositiva;
                                estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                break;
                            case "Sesion":
                                estructuraCapituloPrograma.Sesion = itemRegistros.Contenido;
                                estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                estructuraCapituloPrograma.IdDocumentoSeccionPw = itemRegistros.Id;
                                estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                estructuraCapituloPrograma.VideoId = itemRegistros.VideoId;
                                estructuraCapituloPrograma.Archivo = itemRegistros.Archivo;
                                estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                estructuraCapituloPrograma.ConImagenVideo = itemRegistros.ConImagenVideo;
                                estructuraCapituloPrograma.ImagenVideoNombre = itemRegistros.ImagenVideoNombre;
                                estructuraCapituloPrograma.ImagenVideoAncho = itemRegistros.ImagenVideoAncho;
                                estructuraCapituloPrograma.ImagenVideoAlto = itemRegistros.ImagenVideoAlto;
                                estructuraCapituloPrograma.ConImagenDiapositiva = itemRegistros.ConImagenDiapositiva;
                                estructuraCapituloPrograma.ImagenDiapositivaNombre = itemRegistros.ImagenDiapositivaNombre;
                                estructuraCapituloPrograma.ImagenDiapositivaAncho = itemRegistros.ImagenDiapositivaAncho;
                                estructuraCapituloPrograma.ImagenDiapositivaAlto = itemRegistros.ImagenDiapositivaAlto;
                                estructuraCapituloPrograma.ImagenVideoPosicionX = itemRegistros.ImagenVideoPosicionX;
                                estructuraCapituloPrograma.ImagenVideoPosicionY = itemRegistros.ImagenVideoPosicionY;
                                estructuraCapituloPrograma.ImagenDiapositivaPosicionX = itemRegistros.ImagenDiapositivaPosicionX;
                                estructuraCapituloPrograma.ImagenDiapositivaPosicionY = itemRegistros.ImagenDiapositivaPosicionY;
                                //2
                                estructuraCapituloPrograma.Minuto = itemRegistros.Minuto;
                                estructuraCapituloPrograma.IdTipoVista = itemRegistros.IdTipoVista;
                                estructuraCapituloPrograma.NroDiapositiva = itemRegistros.NroDiapositiva;
                                estructuraCapituloPrograma.ConLogoVideo = itemRegistros.ConLogoVideo;
                                estructuraCapituloPrograma.ConLogoDiapositiva = itemRegistros.ConLogoDiapositiva;
                                estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                break;
                            case "SubSeccion":
                                estructuraCapituloPrograma.SubSesion = itemRegistros.Contenido;
                                if (!string.IsNullOrEmpty(estructuraCapituloPrograma.SubSesion))
                                {
                                    estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                    estructuraCapituloPrograma.IdDocumentoSeccionPw = itemRegistros.Id;
                                    estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                    estructuraCapituloPrograma.VideoId = itemRegistros.VideoId;
                                    estructuraCapituloPrograma.Archivo = itemRegistros.Archivo;
                                    estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                    estructuraCapituloPrograma.ConImagenVideo = itemRegistros.ConImagenVideo;
                                    estructuraCapituloPrograma.ImagenVideoNombre = itemRegistros.ImagenVideoNombre;
                                    estructuraCapituloPrograma.ImagenVideoAncho = itemRegistros.ImagenVideoAncho;
                                    estructuraCapituloPrograma.ImagenVideoAlto = itemRegistros.ImagenVideoAlto;
                                    estructuraCapituloPrograma.ConImagenDiapositiva = itemRegistros.ConImagenDiapositiva;
                                    estructuraCapituloPrograma.ImagenDiapositivaNombre = itemRegistros.ImagenDiapositivaNombre;
                                    estructuraCapituloPrograma.ImagenDiapositivaAncho = itemRegistros.ImagenDiapositivaAncho;
                                    estructuraCapituloPrograma.ImagenDiapositivaAlto = itemRegistros.ImagenDiapositivaAlto;
                                    estructuraCapituloPrograma.ImagenVideoPosicionX = itemRegistros.ImagenVideoPosicionX;
                                    estructuraCapituloPrograma.ImagenVideoPosicionY = itemRegistros.ImagenVideoPosicionY;
                                    estructuraCapituloPrograma.ImagenDiapositivaPosicionX = itemRegistros.ImagenDiapositivaPosicionX;
                                    estructuraCapituloPrograma.ImagenDiapositivaPosicionY = itemRegistros.ImagenDiapositivaPosicionY;
                                    //2
                                    estructuraCapituloPrograma.Minuto = itemRegistros.Minuto;
                                    estructuraCapituloPrograma.IdTipoVista = itemRegistros.IdTipoVista;
                                    estructuraCapituloPrograma.NroDiapositiva = itemRegistros.NroDiapositiva;
                                    estructuraCapituloPrograma.ConLogoVideo = itemRegistros.ConLogoVideo;
                                    estructuraCapituloPrograma.ConLogoDiapositiva = itemRegistros.ConLogoDiapositiva;
                                    estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                }
                                break;
                            default:
                                estructuraCapituloPrograma.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                                break;
                        }
                    }
                    //Comentado
                    lista.Add(estructuraCapituloPrograma);
                }
            }
            return lista;
        }


        /// <summary>
        /// Obtiene la estructura en formato de Lista de un programa general
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Lista de objeto (EstructuraCapituloProgramaBO)</returns>
        public List<EstructuraCapituloProgramaBO> ObtenerEstructuraCapituloProgramaPorIdPGeneralDescargaSinDatos(int idPGeneral)
        {
            List<EstructuraCapituloProgramaBO> lista = new List<EstructuraCapituloProgramaBO>();

            var listadoVideosPrograma = _repConfigurarVideoPrograma.ObtenerPreConfigurarVideoProgramaDescargaSinDatos(idPGeneral);

            if (listadoVideosPrograma != null)
            {
                var listadoEstructura = (from x in listadoVideosPrograma
                                         group x by x.NumeroFila into newGroup
                                         select newGroup).ToList();

                foreach (var item in listadoEstructura)
                {
                    EstructuraCapituloProgramaBO estructuraCapituloPrograma = new EstructuraCapituloProgramaBO();
                    estructuraCapituloPrograma.OrdenFila = item.Key;
                    foreach (var itemRegistros in item)
                    {
                        switch (itemRegistros.NombreTitulo)
                        {
                            case "Capitulo":
                                estructuraCapituloPrograma.Nombre = itemRegistros.Nombre;
                                estructuraCapituloPrograma.Capitulo = itemRegistros.Contenido;                                
                                estructuraCapituloPrograma.IdPgeneral = itemRegistros.IdPgeneral;
                                estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                //2                                
                                break;
                            case "Sesion":
                                estructuraCapituloPrograma.Sesion = itemRegistros.Contenido;
                                estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                //2
                                estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                break;
                            case "SubSeccion":
                                estructuraCapituloPrograma.SubSesion = itemRegistros.Contenido;
                                if (!string.IsNullOrEmpty(estructuraCapituloPrograma.SubSesion))
                                {
                                    estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                    estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                    //2
                                    estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                }
                                break;
                            default:
                                estructuraCapituloPrograma.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                                break;
                        }
                    }
                    //Comentado
                    lista.Add(estructuraCapituloPrograma);
                }
            }
            return lista;
        }

        /// <summary>
        /// Obtiene la plantilla para llenar y realizar la importacion de la seccion de Configurar Secuencia Video
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Archivo excel de la plantilla de configurar secuencia de video</returns>
        public byte[] ObtenerPlantillaExcelConfigurarSecuenciaVideo(int idPGeneral)
        {
            try
            {
                string pGeneral = _repPGeneral.FirstById(idPGeneral).Nombre;
                #region Campos Generados
                var listaCompletaProgramaSesionSubsesion = ObtenerEstructuraCapituloProgramaPorIdPGeneral(idPGeneral).OrderBy(c => c.OrdenCapitulo).ThenBy(c => c.OrdenFila).ToList();

                var camposGenerados = new List<CampoObligatorioDTO>();

                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Programa", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "ID del Programa", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "NroCap", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Capitulo", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Sesion", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Subsesion", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Orden Fila", FlagObligatorio = true });
                #endregion

                #region Campos Adicionales
                var camposAdicionales = new List<CampoObligatorioDTO>();

                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Id Video", FlagObligatorio = true });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Total segundos", FlagObligatorio = true });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Archivo de diapositiva", FlagObligatorio = true });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Nro. de diapositivas", FlagObligatorio = true });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Habilitar sello en video", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Nombre de imagen - ImgVideo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Ancho (px) - ImgVideo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Alto (px) - ImgVideo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Posicion X - ImgVideo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Posicion Y - ImgVideo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Habilitar sello en diapositivas", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Nombre de imagen - ImgDiapositiva", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Ancho (px) - ImgDiapositiva", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Alto (px) - ImgDiapositiva", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Posicion X - ImgDiapositiva", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Posicion Y - ImgDiapositiva", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Id Brightcove", FlagObligatorio = false });
                #endregion

                #region Creacion Plantilla
                MemoryStream memoryStreamPlantilla = new MemoryStream();

                using (var package = new ExcelPackage(memoryStreamPlantilla))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("PlantillaConfigurarSecuenciaVideo");
                    var listaNivel = new List<CeldaDTO>();
                    var listaCabecera = new List<CeldaDTO>();

                    worksheet.Cells.Style.Font.Name = "Calibri";
                    worksheet.Cells.Style.Font.Size = 10.5f;
                    worksheet.PrinterSettings.PaperSize = ePaperSize.A4;
                    Color colorCabeceraGenerado = Color.FromArgb(200, 200, 200);
                    Color colorCabeceraAdicional = Color.FromArgb(185, 200, 225);
                    Color colorCabeceraObligatoria = Color.FromArgb(225, 100, 100);

                    // Encabezado
                    int fila = 1, columna = 1;

                    foreach (var campo in camposGenerados)
                    {
                        worksheet.Cells[fila, columna].Value = campo.Campo;
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[fila, columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[fila, columna].Style.Fill.BackgroundColor.SetColor(colorCabeceraGenerado);

                        listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        columna++;
                    }

                    foreach (var campo in camposAdicionales)
                    {
                        worksheet.Cells[fila, columna].Value = campo.Campo;
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[fila, columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[fila, columna].Style.Fill.BackgroundColor.SetColor(campo.FlagObligatorio ? colorCabeceraObligatoria : colorCabeceraAdicional);

                        listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        columna++;
                    }

                    fila++;

                    foreach (var dato in listaCompletaProgramaSesionSubsesion)
                    {
                        columna = 1;
                        worksheet.Cells[fila, columna].Value = pGeneral;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.IdPgeneral;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.OrdenCapitulo;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.Capitulo ?? string.Empty;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.Sesion ?? string.Empty;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.SubSesion ?? string.Empty;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.OrdenFila;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.VideoId;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.TotalSegundos;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.Archivo;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.NroDiapositivas;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ConImagenVideo == true ? "si" : dato.ConImagenVideo == false ? "no" : "";
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenVideoNombre;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenVideoAncho;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenVideoAlto;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenVideoPosicionX;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenVideoPosicionY;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ConImagenDiapositiva == true ? "si" : dato.ConImagenDiapositiva == false ? "no" : "";
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenDiapositivaNombre;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenDiapositivaAncho;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenDiapositivaAlto;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenDiapositivaPosicionX;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenDiapositivaPosicionY;
                        fila++;
                    }

                    package.Save();
                }

                byte[] excel = memoryStreamPlantilla.ToArray();
                memoryStreamPlantilla.Close();
                #endregion

                return excel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    public class RegistroVideoProgramaBO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public string VideoId { get; set; }
        public string VideoIdBrightcove { get; set; }
        public string TotalMinutos { get; set; }
        public string Archivo { get; set; }
        public string NroDiapositivas { get; set; }
        public bool Configurado { get; set; }
        public bool ConImagenVideo { get; set; }
        public string ImagenVideoNombre { get; set; }
        public string ImagenVideoAncho { get; set; }
        public string ImagenVideoAlto { get; set; }
        public string ImagenVideoPosicionX { get; set; }
        public string ImagenVideoPosicionY { get; set; }

        public bool ConImagenDiapositiva { get; set; }
        public string ImagenDiapositivaNombre { get; set; }
        public string ImagenDiapositivaAncho { get; set; }
        public string ImagenDiapositivaAlto { get; set; }
        public string ImagenDiapositivaPosicionX { get; set; }
        public string ImagenDiapositivaPosicionY { get; set; }
        public int? NumeroFila { get; set; }
        public List<RegistroSesionConfigurarVideoBO> RegistroSesionConfigurar { get; set; }
    }

    public class PreEstructuraCapituloProgramaBO
    {
        public int Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string NombreTitulo { get; set; }
        public int IdSeccionTipoDetalle_PW { get; set; }
        public int NumeroFila { get; set; }
        public int TotalSegundos { get; set; }
        public string VideoId { get; set; }
        public string Archivo { get; set; }
        public string NroDiapositivas { get; set; }
        public bool? ConImagenVideo { get; set; }
        public string ImagenVideoNombre { get; set; }
        public string ImagenVideoAncho { get; set; }
        public string ImagenVideoAlto { get; set; }
        public bool? ConImagenDiapositiva { get; set; }
        public string ImagenDiapositivaNombre { get; set; }
        public string ImagenDiapositivaAncho { get; set; }
        public string ImagenDiapositivaAlto { get; set; }
        public int? ImagenVideoPosicionX { get; set; }
        public int? ImagenVideoPosicionY { get; set; }
        public int? ImagenDiapositivaPosicionX { get; set; }
        public int? ImagenDiapositivaPosicionY { get; set; }
        public int? Minuto { get; set; }
        public int? IdTipoVista { get; set; }
        public int? NroDiapositiva { get; set; }
        public bool? ConLogoVideo { get; set; }
        public bool? ConLogoDiapositiva { get; set; }        
    }

    public class CapitulosSesionesProgramaBO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public string Nombre { get; set; }
        public string Capitulo { get; set; }
        public int OrdenFila { get; set; }
        public List<EstructuraCapituloProgramaBO> ListaSesiones { get; set; }
    }

    public class EstructuraCapituloProgramaBO
    {
        public int Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int IdPgeneral { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public string Nombre { get; set; }
        public string Capitulo { get; set; }
        public string Sesion { get; set; }
        public string SubSesion { get; set; }
        public int OrdenFila { get; set; }
        public int OrdenCapitulo { get; set; }
        public int OrdenSeccion { get; set; }
        public int TotalSegundos { get; set; }
        public string VideoId { get; set; }
        public string Archivo { get; set; }
        public string NroDiapositivas { get; set; }
        public bool? ConImagenVideo { get; set; }
        public string ImagenVideoNombre { get; set; }
        public string ImagenVideoAncho { get; set; }
        public string ImagenVideoAlto { get; set; }
        public bool? ConImagenDiapositiva { get; set; }
        public string ImagenDiapositivaNombre { get; set; }
        public string ImagenDiapositivaAncho { get; set; }
        public string ImagenDiapositivaAlto { get; set; }
        public int? ImagenVideoPosicionX { get; set; }
        public int? ImagenVideoPosicionY { get; set; }
        public int? ImagenDiapositivaPosicionX { get; set; }
        public int? ImagenDiapositivaPosicionY { get; set; }
        public int? Minuto { get; set; }
        public int? IdTipoVista { get; set; }
        public int? NroDiapositiva { get; set; }
        public bool? ConLogoVideo { get; set; }
        public bool? ConLogoDiapositiva { get; set; }
    }

    public class ListaTipoVistaVideoBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class ConultarProgramaPadreBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NroHijos { get; set; }
    }
    public class ListaCursosPorProgramaBO
    {
        public int Id { get; set; }
        public string Programa { get; set; }
        public int IdHijo { get; set; }
        public string Curso { get; set; }
        public List<ListaCapitulosEstructuraProgramaBO> EstructuraCapitulos { get; set; }
    }

    public class ListaCapitulosEstructuraProgramaBO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string NombreTitulo { get; set; }
        public int NroCapitulo { get; set; }
    }

    public class configuracionPreVideoProgramaBO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string IdVideo { get; set; }
        public string IdVideoBrightcove { get; set; }
        public string Archivo { get; set; }
        public int UltimaSesion { get; set; }
        public string Ubicacion { get; set; }
        public bool ConImagenVideo { get; set; }
        public string ImagenVideoNombre { get; set; }
        public int ImagenVideoAncho { get; set; }
        public int ImagenVideoAlto { get; set; }
        public string ImagenVideoPosicionX { get; set; }
        public string ImagenVideoPosicionY { get; set; }

        public bool ConImagenDiapositiva { get; set; }
        public string ImagenDiapositivaNombre { get; set; }
        public int ImagenDiapositivaAncho { get; set; }
        public int ImagenDiapositivaAlto { get; set; }
        public string ImagenDiapositivaPosicionX { get; set; }
        public string ImagenDiapositivaPosicionY { get; set; }

        public int Segundos { get; set; }
        public int Cantidad { get; set; }
        public int intervalo { get; set; }
    }

    public class configuracionVideoProgramaBO
    {
        public string NombCurso { get; set; }
        public string IdVideo { get; set; }
        public string IdVideoBrightcove { get; set; }
        public string Archivo { get; set; }
        public string Ubicacion { get; set; }
        public int UltimaSesion { get; set; }
        public List<configuracionSecuenciaVideoBO> Configuracion { get; set; }
        public List<configuracionSelloVideoBO> OverlayVideo { get; set; }
        public List<configuracionSelloVideoBO> OverlaySlide { get; set; }
    }

    public class configuracionSecuenciaVideoBO
    {
        public string NroDiapositiva { get; set; }
        public int tiempo { get; set; }
        public string tipoVista { get; set; }
        public string NombEvaluacion { get; set; }
        public string EstadoEval { get; set; }
        public string MostrarEvalFin { get; set; }
        public string Evaluacion { get; set; }
        public string UrlEvaluacion { get; set; }
        public string QuitarOverlayVideo { get; set; }
        public string QuitarOverlaySlide { get; set; }
    }

    public class configuracionSecuenciaVideoSecuensiaBO
    {
        public string NroDiapositiva { get; set; }
        public string Tiempo { get; set; }
        public string tipoVista { get; set; }
        public string Evaluacion { get; set; }
        public string UrlEvaluacion { get; set; }
    }

    public class configuracionSelloVideoBO
    {
        public string n_imag { get; set; }
        public string n_coorx { get; set; }
        public string n_coory { get; set; }
        public string n_sizew { get; set; }
        public string n_sizeh { get; set; }
    }

    //
    public class ListadoGrupoPreguntaPorEstructuraBO
    {
        public int IdPgeneral { get; set; }
        public int IdTipoVista { get; set; }
        public int Segundos { get; set; }
        public string GrupoPregunta { get; set; }

    }
    public class ListadoPreguntaPorEstructuraBO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int OrdenFilaCapitulo { get; set; }
        public int OrdenFilaSesion { get; set; }
        public string GrupoPregunta { get; set; }
        public int IdTipoVista { get; set; }
        public int Segundos { get; set; }
        public int OrdenPreguntaGrupo { get; set; }
        public string EnunciadoPregunta { get; set; }
        public bool RespuestaAleatoria { get; set; }
        public bool MostrarFeedbackInmediato { get; set; }
        public bool MostrarFeedbackPorPregunta { get; set; }
        public int NumeroMaximoIntento { get; set; }
        public int TipoRespuesta { get; set; }
    }

    ///

    public class ListadoGrupoPreguntaSesion
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int OrdenFilaCapitulo { get; set; }
        public int OrdenFilaSesion { get; set; }
        public string EnunciadoPregunta { get; set; }
        public bool RespuestaAleatoria { get; set; }
        public bool MostrarFeedbackInmediato { get; set; }
        public bool MostrarFeedbackPorPregunta { get; set; }
        public int NumeroMaximoIntento { get; set; }
        public int TipoRespuesta { get; set; }
        public List<ListadoGrupoSesionRespuestas> Respuestas { get; set; }
    }
    public class ListadoGrupoSesionRespuestas
    {
        public int Id { get; set; }
        public int IdPreguntaProgramaCapacitacion { get; set; }
        public bool RespuestaCorrecta { get; set; }
        public int NroOrden { get; set; }
        public string EnunciadoRespuesta { get; set; }
        public int? Puntaje { get; set; }
        public string FeedbackPositivo { get; set; }
        public string FeedbackNegativo { get; set; }
    }

    public class registroFeedbackResultadoObtenidoBO
    {
        public int IdPgeneral { get; set; }
        public int IdSexo { get; set; }
        public int Puntaje { get; set; }
        public string NombreVideo { get; set; }

    }

    //

    public class configuracionCriterioEvaluacionProgramaBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Porcentaje { get; set; }
    }

    public class listaNumeroGruposSesionBO
    {
        public int IdPgeneral { get; set; }
        public int OrdenFilaCapitulo { get; set; }
        public int OrdenFilaSesion { get; set; }
        public int NumeroGrupos { get; set; }
        public string TipoInteraccion { get; set; }
    }

    public class registroParametroEvaluacionDetalleBO
    {
        public int IdParametroEvaluacion { get; set; }
        public int IdEscalaCalificacionDetalle { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
        public int Ponderacion { get; set; }
        public int IdEscalaCalificacion { get; set; }
        public int IdEsquemaEvaluacionPGeneralDetalle { get; set; }
    }

    public class registroParametroEscalaEvaluacionBO
    {
        public string NombreEscalaCalificacion { get; set; }
        public int Ponderacion { get; set; }
        public int IdParametroEvaluacion { get; set; }
        public string NombreCriterioEvaluacion { get; set; }
        public int IdEscalaCalificacion { get; set; }
        public string NombreParametroEvaluacion { get; set; }
        public int IdEsquemaEvaluacionPGeneralDetalle { get; set; }

        public List<registroParametroEscalaEvaluacionDetalleBO> listaParametroEscalaEvaluacion { get; set; }

    }

    public class registroParametroEscalaEvaluacionDetalleBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }

    }

    public class registroMatriculaCabeceraAlumnoAulaBO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdAlumno { get; set; }
        public int IdPEspecifico { get; set; }
        public string EstadoMatricula { get; set; }
        public DateTime FechaMatricula { get; set; }

    }

    public class registroMatriculaPorProgramaGeneralBO
    {
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdAlumno { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdPGeneral { get; set; }
        public string Tipo { get; set; }
        public int TipoId { get; set; }

    }
    public class registroNotaPromedioPorMatriculaMatriculaBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int Nota { get; set; }
        public int EscalaCalificacion { get; set; }
        public int NotaFinal { get; set; }
        public int Numero { get; set; }
        public int Promedio { get; set; }

    }

    public class registroRankingMatriculaNotaProgramaGeneralBO
    {
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdAlumno { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdPGeneral { get; set; }
        public string Tipo { get; set; }
        public int TipoId { get; set; }
        public int EscalaCalificacion { get; set; }
        public int NotaFinal { get; set; }
        public int Promedio { get; set; }
        public int Orden { get; set; }
        public int TopPorcentaje { get; set; }
    }
}
