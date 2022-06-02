using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AsignacionAutomaticaErrorBO : BaseBO
    {
        public int Id { get; set; }
        public string Campo { get; set; }
        public string Descripcion { get; set; }
        public int? IdContacto { get; set; }
        public int? IdAsignacionAutomatica { get; set; }
        public int? IdAsignacionAutomaticaTipoError { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }


        public AsignacionAutomaticaErrorBO() {
        }

        public AsignacionAutomaticaErrorBO(int IdAsignacionAutomatica, string Campo, string Descripcion, int IdAsignacionAutomaticaTipoError, int IdContacto)
        {
            this.IdAsignacionAutomatica = IdAsignacionAutomatica;
            this.Campo = Campo;
            this.Descripcion = Descripcion;
            this.IdAsignacionAutomaticaTipoError = IdAsignacionAutomaticaTipoError;
            this.IdContacto = IdContacto;
        }

        public TAsignacionAutomaticaError MapearAEntidad(AsignacionAutomaticaErrorBO AsignacionAutomaticaError)
        {
            TAsignacionAutomaticaError Entidad = new TAsignacionAutomaticaError();
            Entidad.Id = AsignacionAutomaticaError.Id;
            Entidad.Campo = AsignacionAutomaticaError.Campo;
            Entidad.Descripcion = AsignacionAutomaticaError.Descripcion;
            Entidad.IdContacto = AsignacionAutomaticaError.IdContacto;
            Entidad.IdAsignacionAutomatica = AsignacionAutomaticaError.IdAsignacionAutomatica;
            Entidad.IdAsignacionAutomaticaTipoError = AsignacionAutomaticaError.IdAsignacionAutomaticaTipoError;
            Entidad.Estado = AsignacionAutomaticaError.Estado;
            Entidad.FechaCreacion = AsignacionAutomaticaError.FechaCreacion;
            Entidad.FechaModificacion = AsignacionAutomaticaError.FechaModificacion;
            Entidad.UsuarioCreacion = AsignacionAutomaticaError.UsuarioCreacion;
            Entidad.UsuarioModificacion = AsignacionAutomaticaError.UsuarioModificacion;
            return Entidad;
        }

     
    }
}
