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
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Marketing.Repositorio;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FormularioSolicitudBO: BaseBO
    {
        public int? IdFormularioRespuesta { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Campanha { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public string Proveedor { get; set; }
        public int IdFormularioSolicitudTextoBoton { get; set; }
        public int TipoSegmento { get; set; }
        public string CodigoSegmento { get; set; }
        public int TipoEvento { get; set; }
        public string UrlbotonInvitacionPagina { get; set; }
        public Guid? IdMigracion { get; set; }

        FormularioSolicitudRepositorio _repFormularioSolicitud;

        public FormularioSolicitudBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }

        public FormularioSolicitudBO(int id)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repFormularioSolicitud = new FormularioSolicitudRepositorio();

            var formulario = _repFormularioSolicitud.FirstById(id);
            if (formulario != null)
            {
                this.Id = formulario.Id;
                this.IdFormularioRespuesta = formulario.IdFormularioRespuesta;
                this.Nombre = formulario.Nombre;
                this.Codigo = formulario.Codigo;
                this.Campanha = formulario.Campanha;
                this.IdConjuntoAnuncio = formulario.IdConjuntoAnuncio;
                this.Proveedor = formulario.Proveedor;
                this.IdFormularioSolicitudTextoBoton = formulario.IdFormularioSolicitudTextoBoton;
                this.TipoSegmento = formulario.TipoSegmento;
                this.CodigoSegmento = formulario.CodigoSegmento;
                this.TipoEvento = formulario.TipoEvento;
                this.UrlbotonInvitacionPagina = formulario.UrlbotonInvitacionPagina;
                this.Estado = formulario.Estado;
                this.FechaCreacion = formulario.FechaCreacion;
                this.FechaModificacion = formulario.FechaModificacion;
                this.UsuarioCreacion = formulario.UsuarioModificacion;
                this.IdMigracion = formulario.IdMigracion;
                this.RowVersion = formulario.RowVersion;

            }
        }

        


    }

    
}
