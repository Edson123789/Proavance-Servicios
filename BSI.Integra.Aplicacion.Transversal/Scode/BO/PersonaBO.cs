using BSI.Integra.Aplicacion.Base.BO;
using System;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;
using static BSI.Integra.Aplicacion.Base.Classes.BaseHelper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: PersonaBO
    ///Autor: Wilber Choque - Britsel Calluchi - Jose Villena - Edgar Serruto.
    ///Fecha: 27/01/2021
    ///<summary>
    ///Columnas y funciones de la tabla T_Persona
    ///</summary>
    public class PersonaBO : BaseBO
    {
        ///Propiedades		Significado
		///-------------	-----------------------
		///Email1           Email de Persona
		///IdMigracion		Id de Migracion
        public string Email1 { get; set; }
        public int? IdMigracion { get; set; }
        public ClasificacionPersonaBO ClasificacionPersona;
    
        //repositorios
        private PersonaRepositorio _repPersona;
        private ClasificacionPersonaRepositorio _repClasificacionPersona;

        //maestros
        private AlumnoRepositorio _repAlumno;
        private PersonalRepositorio _repPersonal;
        private ExpositorRepositorio _repExpositor;
        private ProveedorRepositorio _repProveedor;
		private PostulanteRepositorio _repPostulante;
        private LogRepositorio _repLog;


		public PersonaBO() {
        }
        public PersonaBO(integraDBContext _context) 
        {
            _repPersona = new PersonaRepositorio(_context);
            _repClasificacionPersona = new ClasificacionPersonaRepositorio(_context);

            _repAlumno = new AlumnoRepositorio(_context);
            _repPersonal = new PersonalRepositorio(_context);
            _repExpositor = new ExpositorRepositorio(_context);
            _repProveedor = new ProveedorRepositorio(_context);
			_repPostulante = new PostulanteRepositorio(_context);
            _repLog = new LogRepositorio(_context);
        }

        /// <summary>
        /// Inserta una persona y su clasificacion persona
        /// </summary>
        /// <param name="idTablaOriginal">Id de la tabla original</param>
        /// <param name="tipoPersona">Tipo de Persona</param>
        /// <param name="usuario">Usuario</param>
        /// <returns> Si se logra insertar correctamente retorna el id de clasificacion persona, en caso no termine el proceso correctamente retorna null: Int?</returns>
        public int? InsertarPersona(int idTablaOriginal, TipoPersona tipoPersona, string usuario) {
            try
            {
                var email = this.ObtenerEmailPersona(idTablaOriginal, tipoPersona);
                if (!EsCorreoValido(email))
                {
                    throw new Exception("Email no valido");
                }

                //Si no existe el email en persona, lo insertamos
                if (!_repPersona.ExistePorEmail(email))
                {
                    this.Email1 = email;
                    this.Estado = true;
                    this.FechaCreacion = DateTime.Now;
                    this.FechaModificacion = DateTime.Now;
                    this.UsuarioCreacion = usuario;
                    this.UsuarioModificacion = usuario;
                }
                else  {
                    //si no ->llenamos la persona actual con los registros actuales
                    var persona = _repPersona.FirstBy(x => x.Email1 == email);

                    this.Id = persona.Id;
                    this.Email1 = persona.Email1;
                    this.Estado = persona.Estado;
                    this.FechaCreacion = persona.FechaCreacion;
                    this.FechaModificacion = persona.FechaModificacion;
                    this.UsuarioCreacion = persona.UsuarioCreacion;
                    this.UsuarioModificacion = persona.UsuarioModificacion;
                    this.IdMigracion = persona.IdMigracion;
                    this.RowVersion = persona.RowVersion;
                }

                //Si la persona no tiene un registro en clasificacionPersona, lo insertamos
                if (!_repClasificacionPersona.ExistePorTipoPersona(this.Id, tipoPersona))
                {
                    var clasificacionPersona = new ClasificacionPersonaBO()
                    {
                        IdPersona = this.Id,
                        IdTipoPersona = (int)tipoPersona,
                        IdTablaOriginal = idTablaOriginal,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario
                    };
                    this.ClasificacionPersona = clasificacionPersona;
                }
                else {
                    var clasificacionPersona = _repClasificacionPersona.FirstBy(x => x.IdTablaOriginal == idTablaOriginal && x.IdTipoPersona == (int)tipoPersona && x.IdPersona == this.Id);
                    var prueba = (int)tipoPersona;
                    this.ClasificacionPersona = new ClasificacionPersonaBO()
                    {
                        Id = clasificacionPersona.Id,
                    };
                }

                if (this.Id != 0)
                {
                    if (this.ClasificacionPersona.Id != 0)
                    {
                        return this.ClasificacionPersona.Id;
                    }
                    else {
                         _repPersona.Update(this);
                    }
                }
                else {
                    _repPersona.Insert(this);
                }
                //obtenemos el id de clasificacion persona
                return _repClasificacionPersona.FirstBy(x => x.IdTablaOriginal == idTablaOriginal && x.IdTipoPersona == (int)tipoPersona && x.IdPersona == this.Id).Id;
            }
            catch (Exception e)
            {
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "InsertarPersona", Parametros = $"{idTablaOriginal}/{tipoPersona}/{usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return null;
            }
        }

        /// <summary>
        /// Obtiene un el email dependiendo de la persona
        /// </summary>
        /// <param name="id">Id tabla Original</param>
        /// <param name="tipoPersona">Tipo de Persona</param>
        /// <returns> Email de Persona : String </returns>
        private string ObtenerEmailPersona(int idTablaOriginal, TipoPersona tipoPersona) {
            try
            {
                var email = "";
                switch (tipoPersona) //ObtenerEmail
                {
                    case TipoPersona.Alumno:
                        email = _repAlumno.ObtenerEmail(idTablaOriginal);
                        break;
                    case TipoPersona.Personal:
                        email = _repPersonal.ObtenerEmail(idTablaOriginal);
                        break;
                    case TipoPersona.Docente:
                        email = _repExpositor.ObtenerEmail(idTablaOriginal);
                        break;
                    case TipoPersona.Proveedor:
                        email = _repProveedor.ObtenerEmail(idTablaOriginal);
                        break;
					case TipoPersona.Postulante:
						email = _repPostulante.ObtenerEmail(idTablaOriginal);
						break;
					default:
                        break;
                }
                return email;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
