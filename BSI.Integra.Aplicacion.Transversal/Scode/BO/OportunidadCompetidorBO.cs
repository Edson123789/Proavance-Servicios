using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class OportunidadCompetidorBO : BaseBO
    {
        public int? IdOportunidad { get; set; }
        public string OtroBeneficio { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
        public Guid? IdMigracion { get; set; }

        public List<OportunidadPrerequisitoGeneralBO> ListaPrerequisitoGeneral;
        public List<OportunidadPrerequisitoEspecificoBO> ListaPrerequisitoEspecifico;
        public List<OportunidadBeneficioBO> ListaBeneficio;

        public List<DetalleOportunidadCompetidorBO> ListaCompetidor;
        public OportunidadCompetidorRepositorio _repOportunidadCompetidor;
        public OportunidadCompetidorBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
        public OportunidadCompetidorBO(int id)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repOportunidadCompetidor = new OportunidadCompetidorRepositorio();
            var Competidor = _repOportunidadCompetidor.FirstById(id);
            this.Id = Competidor.Id;
            this.IdOportunidad = Competidor.IdOportunidad;
            this.OtroBeneficio = Competidor.OtroBeneficio;
            this.Respuesta = Competidor.Respuesta;
            this.Completado = Competidor.Completado;
            this.FechaCreacion = Competidor.FechaCreacion;
            this.FechaModificacion = Competidor.FechaModificacion;
            this.UsuarioCreacion = Competidor.UsuarioCreacion;
            this.UsuarioModificacion = Competidor.UsuarioModificacion;
            this.Estado = Competidor.Estado;
            this.RowVersion = Competidor.RowVersion;
			this.IdMigracion = Competidor.IdMigracion;
        }
        public OportunidadCompetidorBO(int id, integraDBContext integraDBContext)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repOportunidadCompetidor = new OportunidadCompetidorRepositorio(integraDBContext);
            var Competidor = _repOportunidadCompetidor.FirstById(id);
            this.Id = Competidor.Id;
            this.IdOportunidad = Competidor.IdOportunidad;
            this.OtroBeneficio = Competidor.OtroBeneficio;
            this.Respuesta = Competidor.Respuesta;
            this.Completado = Competidor.Completado;
            this.FechaCreacion = Competidor.FechaCreacion;
            this.FechaModificacion = Competidor.FechaModificacion;
            this.UsuarioCreacion = Competidor.UsuarioCreacion;
            this.UsuarioModificacion = Competidor.UsuarioModificacion;
            this.Estado = Competidor.Estado;
            this.RowVersion = Competidor.RowVersion;
            this.IdMigracion = Competidor.IdMigracion;
        }
    }
}
