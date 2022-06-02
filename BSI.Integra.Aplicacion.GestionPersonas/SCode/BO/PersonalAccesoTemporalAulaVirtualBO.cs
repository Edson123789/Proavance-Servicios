using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Helpers;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    ///BO: PersonalAccesoTemporalAulaVirtualBO
    ///Autor: Gian Miranda
    ///Fecha: 29/04/2021
    ///<summary>
    ///Columnas y funciones de la tabla gp.T_PersonalAccesoTemporalAulaVirtual
    ///</summary>
    public class PersonalAccesoTemporalAulaVirtualBO : BaseBO
    {
        ///Propiedades		                        Significado
        ///-------------	                        -----------------------
        ///IdCentroCosto                            Id de Centro de Costo
        ///IdPespecificoPadre                       Id del PEspecifico padre (PK de la tabla pla.T_PEspecifico)
        ///IdPespecificoHijo                        Id del PEspecifico hijo (PK de la tabla pla.T_PEspecifico)
        ///FechaInicio                              Fecha de inicio del acceso temporal
        ///FechaFin                                 Fecha de fin del acceso temporal
        ///IdMigracion                              Id de migración (campo nullable)

        public int IdPersonal { get; set; }
        public int IdPespecificoPadre { get; set; }
        public int IdPespecificoHijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? IdMigracion { get; set; }

        private readonly PersonalAccesoTemporalAulaVirtualRepositorio _repPersonalAccesoTemporalAulaVirtual;
        private readonly MontoPagoCronogramaRepositorio _repMontoPagoCronograma;
        private readonly AlumnoRepositorio _repAlumno;
        private readonly PersonalRepositorio _repPersonal;
        private readonly PersonaRepositorio _repPersona;
        private readonly ClasificacionPersonaRepositorio _repClasificacionPersona;
        private readonly IntegraAspNetUsersRepositorio _repIntegraAspNetUsers;

        private AlumnoBO Alumno;
        private ClasificacionPersonaBO ClasificacionPersona;
        private PersonaBO Persona;
        private string IdPortalWeb;

        public PersonalAccesoTemporalAulaVirtualBO()
        {
            _repPersonalAccesoTemporalAulaVirtual = new PersonalAccesoTemporalAulaVirtualRepositorio();
            _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio();
            _repAlumno = new AlumnoRepositorio();
            _repPersonal = new PersonalRepositorio();
            _repPersona = new PersonaRepositorio();
            _repClasificacionPersona = new ClasificacionPersonaRepositorio();
            _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio();

            Alumno = new AlumnoBO();
            ClasificacionPersona = new ClasificacionPersonaBO();
            Persona = new PersonaBO();

            IdPortalWeb = string.Empty;
        }

        public PersonalAccesoTemporalAulaVirtualBO(integraDBContext integraDBContext)
        {
            _repPersonalAccesoTemporalAulaVirtual = new PersonalAccesoTemporalAulaVirtualRepositorio(integraDBContext);
            _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(integraDBContext);
            _repAlumno = new AlumnoRepositorio(integraDBContext);
            _repPersonal = new PersonalRepositorio(integraDBContext);
            _repPersona = new PersonaRepositorio(integraDBContext);
            _repClasificacionPersona = new ClasificacionPersonaRepositorio(integraDBContext);
            _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(integraDBContext);

            Alumno = new AlumnoBO(integraDBContext);
            ClasificacionPersona = new ClasificacionPersonaBO(integraDBContext);
            Persona = new PersonaBO(integraDBContext);

            IdPortalWeb = string.Empty;
        }

        /// <summary>
        /// Obtiene la lista de accesos temporales agrupados
        /// </summary>
        /// <param name="datosAccesoTemporal">Objeto de clase ActualizarAccesoTemporalDTO con los datos de de AccesoTemporal</param>
        /// <returns>Bool</returns>
        public bool ActualizarAccesosTemporalesIntegra(ActualizarAccesoTemporalDTO datosAccesoTemporal)
        {
            try
            {
                bool resultado = false;

                resultado = _repPersonalAccesoTemporalAulaVirtual.ActualizarAccesosTemporalesIntegra(datosAccesoTemporal);

                if (!resultado)
                {
                    return resultado;
                }

                var personal = _repPersonal.FirstBy(x => x.Id == datosAccesoTemporal.IdPersonal);

                // Verificacion y/o creacion de persona
                Persona = _repPersona.FirstBy(x => x.Email1 == personal.Email);

                if (Persona == null)
                {
                    Persona.Email1 = personal.Email;
                    Persona.Estado = true;
                    Persona.UsuarioCreacion = "AccesoTemporalGP";
                    Persona.UsuarioModificacion = "AccesoTemporalGP";
                    Persona.FechaCreacion = DateTime.Now;
                    Persona.FechaModificacion = DateTime.Now;

                    _repPersona.Insert(Persona);
                }

                // Verificacion y/o creacion de clasificacion persona
                ClasificacionPersona = _repClasificacionPersona.FirstBy(x => x.IdTipoPersona == 1/*Tipo Alumno*/ && x.IdPersona == Persona.Id);

                if (ClasificacionPersona == null)
                {
                    ClasificacionPersona = new ClasificacionPersonaBO();

                    Alumno = _repAlumno.FirstBy(x => x.Email1 == personal.Email);

                    if (Alumno != null)
                    {
                        ClasificacionPersona.IdPersona = Persona.Id;
                        ClasificacionPersona.IdTipoPersona = 1;/*Tipo Alumno*/
                        ClasificacionPersona.IdTablaOriginal = Alumno.Id;
                        ClasificacionPersona.Estado = true;
                        ClasificacionPersona.UsuarioCreacion = "AccesoTemporalGP";
                        ClasificacionPersona.UsuarioModificacion = "AccesoTemporalGP";
                        ClasificacionPersona.FechaCreacion = DateTime.Now;
                        ClasificacionPersona.FechaModificacion = DateTime.Now;

                        try
                        {
                            _repClasificacionPersona.Insert(ClasificacionPersona);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Comunicarse con sistemas para validar los datos del alumno");
                        }

                        ClasificacionPersona.IdMigracion = ClasificacionPersona.Id;

                        // Otra consulta por el cambio en el campo RowVersion
                        ClasificacionPersona = _repClasificacionPersona.FirstBy(x => x.Id == ClasificacionPersona.Id);

                        try
                        {
                            _repClasificacionPersona.Update(ClasificacionPersona);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Comunicarse con sistemas para validar los datos del alumno");
                        }
                    }
                }

                Alumno = _repAlumno.FirstBy(x => x.Id == ClasificacionPersona.IdTablaOriginal);

                if (Alumno == null)
                {
                    Alumno = new AlumnoBO();

                    //Nombres
                    var nombres = personal.Nombres.Split(new char[] { ' ' }).ToList().Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();

                    if (nombres.Count == 1)
                    {
                        Alumno.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                        Alumno.Nombre2 = string.Empty;
                    }
                    else if (nombres.Count == 2)
                    {
                        Alumno.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                        Alumno.Nombre2 = nombres[1].Length >= 100 ? nombres[1].Substring(0, 100) : nombres[1];
                    }
                    else if (nombres.Count > 2)
                    {
                        Alumno.Nombre1 = string.Join(" ", nombres.ToArray()).Length >= 100 ? String.Join(" ", nombres.ToArray()).Substring(0, 100) : String.Join(" ", nombres.ToArray());
                        Alumno.Nombre2 = string.Empty;
                    }

                    //Apellidos
                    personal.Apellidos = personal.Apellidos ?? string.Empty;

                    var apellidos = personal.Apellidos.Split(new char[] { ' ' }).ToList().Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();

                    if (apellidos.Count == 1)
                    {
                        Alumno.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                        Alumno.ApellidoMaterno = string.Empty;
                    }
                    else if (apellidos.Count == 2)
                    {
                        Alumno.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                        Alumno.ApellidoMaterno = apellidos[1].Length >= 100 ? apellidos[1].Substring(0, 100) : apellidos[1];
                    }
                    else if (apellidos.Count > 2)
                    {
                        Alumno.ApellidoPaterno = String.Join(" ", apellidos.ToArray()).Length >= 100 ? String.Join(" ", apellidos.ToArray()).Substring(0, 100) : String.Join(" ", apellidos.ToArray());
                        Alumno.ApellidoMaterno = string.Empty;
                    }
                    else
                    {
                        Alumno.ApellidoPaterno = string.Empty;
                        Alumno.ApellidoMaterno = string.Empty;
                    }

                    Alumno.IdAformacion = 3/*Sin area de formacion*/;
                    Alumno.IdAtrabajo = 3/*Sin area de trabajo*/;
                    Alumno.IdCargo = 11/*Sin cargo*/;
                    Alumno.IdIndustria = 48/*Sin industria*/;
                    Alumno.Celular = "955456433";
                    Alumno.IdCodigoRegionCiudad = null;
                    Alumno.IdCodigoPais = 51/*Peru*/;
                    Alumno.Telefono = string.Empty;
                    Alumno.Email1 = personal.Email;
                    Alumno.Email2 = personal.Email;
                    Alumno.Estado = true;
                    Alumno.UsuarioCreacion = "GestionPersonal";
                    Alumno.UsuarioModificacion = "SYSTEM";
                    Alumno.FechaModificacion = DateTime.Now;
                    Alumno.FechaCreacion = DateTime.Now;
                    Alumno.IdEstadoContactoWhatsApp = 3;
                    bool resultadoAlumno = _repAlumno.Insert(Alumno);

                    if (!resultadoAlumno)
                        return false;

                    ClasificacionPersona.IdPersona = Persona.Id;
                    ClasificacionPersona.IdTipoPersona = 1;/*Tipo Alumno*/
                    ClasificacionPersona.IdTablaOriginal = Alumno.Id;
                    ClasificacionPersona.Estado = true;
                    ClasificacionPersona.UsuarioCreacion = "AccesoTemporalGP";
                    ClasificacionPersona.UsuarioModificacion = "AccesoTemporalGP";
                    ClasificacionPersona.FechaCreacion = DateTime.Now;
                    ClasificacionPersona.FechaModificacion = DateTime.Now;

                    _repClasificacionPersona.Insert(ClasificacionPersona);

                    ClasificacionPersona.IdMigracion = ClasificacionPersona.Id;

                    // Otra consulta por el cambio en el campo RowVersion
                    ClasificacionPersona = _repClasificacionPersona.FirstBy(x => x.Id == ClasificacionPersona.Id);

                    try
                    {
                        _repClasificacionPersona.Update(ClasificacionPersona);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Comunicarse con sistemas para validar los datos del alumno");
                    }
                }

                if (Alumno.Email1 != Persona.Email1)
                    throw new Exception("Comunicarse con sistemas para validar los datos del alumno");

                var registroPortalWeb = _repPersonalAccesoTemporalAulaVirtual.ObtenerDatosBasicosPortalWebUsername(personal.Email);

                if (registroPortalWeb != null)
                {
                    IdPortalWeb = registroPortalWeb.IdUsuarioPortalWeb;

                    if (Alumno.Id != registroPortalWeb.IdAlumno)
                        _repPersonalAccesoTemporalAulaVirtual.ActualizarIdAlumnoUsuarioPortalWeb(registroPortalWeb.IdUsuarioPortalWeb, Alumno.Id);
                }

                if (string.IsNullOrEmpty(IdPortalWeb))
                {
                    /*Logica para crear el contacto*/
                    if (string.IsNullOrEmpty(IdPortalWeb))
                    {
                        var credencialesIntegra = _repIntegraAspNetUsers.FirstBy(x => x.PerId == personal.Id);

                        string claveIntegra = credencialesIntegra.UsClave;
                        string claveHash = string.Empty;
                        claveHash = Crypto.HashPassword(claveIntegra);

                        var resultadoAspNetUsers = _repMontoPagoCronograma.CrearUsuarioClavePortalWeb(Alumno.Id, personal.Email, claveIntegra, claveHash, personal.Nombres, personal.Apellidos, Alumno.Telefono, Alumno.Celular, Alumno.IdCodigoRegionCiudad, Alumno.IdCodigoPais, DateTime.Now);

                        IdPortalWeb = _repPersonalAccesoTemporalAulaVirtual.ObtenerIdUsuarioPortalWebCorreo(personal.Email);
                    }
                }

                bool resultadoPortalWeb = _repPersonalAccesoTemporalAulaVirtual.ActualizarAccesosTemporalesPortalWeb(personal.Id, IdPortalWeb, Alumno.Id);

                if (!resultadoPortalWeb)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de accesos temporales agrupados
        /// </summary>
        /// <param name="idPersonal">Id del personal del cual se busca obtener la lista de sus accesos temporales (PK de la tabla gp.T_Personal)</param>
        /// <returns>Lista de objetos de clase MaestroPersonalGrupoAccesoTemporalDTO</returns>
        public List<MaestroPersonalGrupoAccesoTemporalDTO> ObtenerListaAccesoTemporal(int idPersonal)
        {
            try
            {
                List<MaestroPersonalGrupoAccesoTemporalDTO> listaAccesoTemporal = new List<MaestroPersonalGrupoAccesoTemporalDTO>();

                var accesoTempAuxiliar = _repPersonalAccesoTemporalAulaVirtual.ObtenerListaAccesoTemporal(idPersonal);

                listaAccesoTemporal = accesoTempAuxiliar
                    .GroupBy(x => new { x.IdPersonal, x.IdPEspecificoPadre, x.NombreProgramaPadre, x.FechaInicio, x.FechaFin, x.EvaluacionHabilitada })
                    .Select(g =>
                    new MaestroPersonalGrupoAccesoTemporalDTO
                    {
                        IdPersonal = g.Key.IdPersonal,
                        IdPEspecificoPadre = g.Key.IdPEspecificoPadre,
                        NombreProgramaPadre = g.Key.NombreProgramaPadre,
                        EvaluacionHabilitada = g.Key.EvaluacionHabilitada,
                        IdPEspecificoHijo = accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.IdPEspecificoHijo).ToList(),
                        CantidadPreguntaConfigurada = accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.CantidadPreguntaConfigurada).ToList().Sum(),
                        CantidadCrucigramaConfigurado = accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.CantidadCrucigramaConfigurado).ToList().Sum(),
                        CantidadPreguntaResuelta = accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.CantidadPreguntaResuelta).ToList().Sum(),
                        CantidadCrucigramaResuelta = accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.CantidadCrucigramaResuelta).ToList().Sum(),
                        Avance = Math.Round(accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.Avance).ToList().Sum() / accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.Avance).ToList().Count(), 2),
                        Nota = Math.Round(accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.Nota).ToList().Sum() / accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.Nota).ToList().Count(), 2),
                        FechaInicio = g.Key.FechaInicio,
                        FechaFin = g.Key.FechaFin
                    }).ToList();

                return listaAccesoTemporal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
