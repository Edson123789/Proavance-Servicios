using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using BSI.Integra.Aplicacion.DTOs;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FormularioRespuestaBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdPgeneral { get; set; }
        public string ProgramaGeneral { get; set; }
        public string ResumenProgramaGeneral { get; set; }
        public string ColorTextoPgeneral { get; set; }
        public string ColorTextoDescripcionPgeneral { get; set; }
        public string ColorTextoInvitacionBrochure { get; set; }
        public string TextoBotonBrochure { get; set; }
        public string ColorFondoBotonBrochure { get; set; }
        public string ColorTextoBotonBrochure { get; set; }
        public string ColorBordeInferiorBotonBrochure { get; set; }
        public string ColorTextoBotonInvitacion { get; set; }
        public string ColorFondoBotonInvitacion { get; set; }
        public string FondoBotonLadoInvitacion { get; set; }
        public string UrlImagenLadoInvitacion { get; set; }
        public string TextoBotonInvitacionPagina { get; set; }
        public string UrlBotonInvitacionPagina { get; set; }
        public string TextoBotonInvitacionArea { get; set; }
        public string UrlBotonInvitacionArea { get; set; }
        public string ContenidoSeccionTelefonos { get; set; }
        public int? IdFormularioRespuestaPlantilla { get; set; }
        public string Urlbrochure { get; set; }
        public string Urllogotipo { get; set; }
        public string TextoInvitacionBrochure { get; set; }
        public Guid? IdMigracion { get; set; }



        DapperRepository _dapperRepository;
        List<FormularioRespuestaDTO> ListaFormularioRespuesta;
        public FormularioRespuestaBO()
        {
            _dapperRepository = new DapperRepository();
            ListaFormularioRespuesta = new List<FormularioRespuestaDTO>();
        }

        public void ObtenerTodo()
        {
            string _queryFormularioRespuesta = "Select Id,Nombre,Codigo,IdPGeneral,ProgramaGeneral,ResumenProgramaGeneral,ColorTextoPGeneral,ColorTextoDescripcionPGeneral," +
                                               "ColorTextoInvitacionBrochure,TextoBotonBrochure,ColorFondoBotonBrochure,ColorTextoBotonBrochure,ColorBordeInferiorBotonBrochure," +
                                               "ColorTextoBotonInvitacion,ColorFondoBotonInvitacion,FondoBotonLadoInvitacion,UrlImagenLadoInvitacion,TextoBotonInvitacionPagina," +
                                               "UrlBotonInvitacionPagina,TextoBotonInvitacionAREA,UrlBotonInvitacionAREA,ContenidoSeccionTelefonos,IdFormularioRespuestaPlantilla," +
                                               "URLBrochure,URLLogotipo,TextoInvitacionBrochure" +
                                               " From mkt.V_TFormularioRespuesta_ObtenerTodo Where Estado=1";
            var queryFormularioRespuesta = _dapperRepository.QueryDapper(_queryFormularioRespuesta, null);
        }

        public void ObtenerPorFiltroGrilla(Paginador paginador, GridFilters filter = null)
        {
            string[] filtroGrilla =new string[3];
            int c = 0;
            if (filter != null)
            {
                foreach (var item in filter.Filters)
                {
                    
                    switch (item.Field)
                    {
                        case "Nombre":
                            filtroGrilla[c] = "and like '%" +item.Value+"%'";
                            break;
                        case "Codigo":
                            filtroGrilla[c] = "and like '%" +item.Value+"%'";
                            break;
                        default:
                            filtroGrilla[c] = "";   
                            break;
                    }
                    c++;
                }
            }

            string _queryFormularioRespuesta = "Select Id,Nombre,Codigo,IdPGeneral,ProgramaGeneral,ResumenProgramaGeneral,ColorTextoPGeneral,ColorTextoDescripcionPGeneral," +
                                               "ColorTextoInvitacionBrochure,TextoBotonBrochure,ColorFondoBotonBrochure,ColorTextoBotonBrochure,ColorBordeInferiorBotonBrochure," +
                                               "ColorTextoBotonInvitacion,ColorFondoBotonInvitacion,FondoBotonLadoInvitacion,UrlImagenLadoInvitacion,TextoBotonInvitacionPagina," +
                                               "UrlBotonInvitacionPagina,TextoBotonInvitacionAREA,UrlBotonInvitacionAREA,ContenidoSeccionTelefonos,IdFormularioRespuestaPlantilla," +
                                               "URLBrochure,URLLogotipo,TextoInvitacionBrochure" +
                                               " From mkt.V_TFormularioRespuesta_ObtenerTodo Where Estado=1 "+filtroGrilla.ToString();
            var queryFormularioRespuesta = _dapperRepository.QueryDapper(_queryFormularioRespuesta, null);
        }
    }

}
