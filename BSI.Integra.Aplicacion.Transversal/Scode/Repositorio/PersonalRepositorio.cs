using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Gestion de Personas/Personal
    /// Autor: Esthephany Tanco - Luis Huallpa - Ansoli Espinoza - Richard Zenteno - Johan Cayo - Gian Miranda - Britsel C. - Edgar S.
    /// Fecha: 09/02/2021
    /// <summary>
    /// Repositorio para consultas de gp.T_Personal
    /// </summary>
    public class PersonalRepositorio : BaseRepository<TPersonal, PersonalBO>
    {
        #region Metodos Base
        public PersonalRepositorio() : base()
        {
        }
        public PersonalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalBO> GetBy(Expression<Func<TPersonal, bool>> filter)
        {
            IEnumerable<TPersonal> listado = base.GetBy(filter);
            List<PersonalBO> listadoBO = new List<PersonalBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalBO objetoBO = Mapper.Map<TPersonal, PersonalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalBO FirstById(int id)
        {
            try
            {
                TPersonal entidad = base.FirstById(id);
                PersonalBO objetoBO = new PersonalBO();
                Mapper.Map<TPersonal, PersonalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalBO FirstBy(Expression<Func<TPersonal, bool>> filter)
        {
            try
            {
                TPersonal entidad = base.FirstBy(filter);
                PersonalBO objetoBO = Mapper.Map<TPersonal, PersonalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonal entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<PersonalBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(PersonalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonal entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<PersonalBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TPersonal entidad, PersonalBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TPersonal MapeoEntidad(PersonalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonal entidad = new TPersonal();
                entidad = Mapper.Map<PersonalBO, TPersonal>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.PersonalLog != null && objetoBO.PersonalLog.Count > 0)
                {
                    foreach (var hijo in objetoBO.PersonalLog)
                    {
                        TPersonalLog entidadHijo = new TPersonalLog();
                        entidadHijo = Mapper.Map<PersonalLogBO, TPersonalLog>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPersonalLog.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene el Id, Nombre y Apellido a trav�s del email del Asesor.
        /// </summary>
        /// <param name="email"></param>
        /// <returns> Información de nombres, apellidos por email de Asesor : PersonalInformacionCorreoDTO </returns>
        public PersonalInformacionCorreoDTO ObtenerNombreApellido(string email)
        {
            try
            {
                string _query = "Select Id, Nombres, Apellidos, Email from gp.V_TPersonal_ObtenerNombreEmail Where Email=@Email and Estado=1";
                string queryRespuesta = _dapper.FirstOrDefault(_query, new { @Email = email });
                if (queryRespuesta != "null" && !queryRespuesta.Contains("[]"))
                {
                    PersonalInformacionCorreoDTO personalDTO = JsonConvert.DeserializeObject<PersonalInformacionCorreoDTO>(queryRespuesta);
                    return personalDTO;
                }
                throw new Exception(ErrorSistema.Instance.MensajeError(204));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 08/02/2021
        /// <summary>
        /// Obtener información de Personal por nombre AutoComplete
        /// </summary>
        /// <param name="nombre"> nombre de búsqueda </param>
        /// <returns> Lista de Personal por nombre Registrados : List<PersonalAutocompleteDTO> </returns>
        public List<PersonalAutocompleteDTO> CargarPersonalAutoComplete(string nombre)
        {
            try
            {
                string query = "SELECT Id, Nombre from gp.V_TPersonal_NombreCompleto WHERE Nombre LIKE CONCAT('%',@nombre,'%') AND Estado = 1 AND Rol = 'VENTAS' AND Activo = 1 ORDER By Nombre ASC";
                string queryRespuesta = _dapper.QueryDapper(query, new { nombre });
                return JsonConvert.DeserializeObject<List<PersonalAutocompleteDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Autor: Gian Miranda
        ///Fecha: 08/02/2021
        /// <summary>
        /// Obtener información del personal para filtro
        /// </summary>
        /// <returns>Lista de objetos de clase PersonalAutocompleteDTO</returns>
        public List<PersonalAutocompleteDTO> CargarPersonalParaFiltro()
        {
            try
            {
                string query = "SELECT Id, Nombre from gp.V_TPersonal_NombreCompleto WHERE  Estado = 1 AND Rol = 'VENTAS' AND Activo = 1 ORDER By Nombre ASC";
                string queryRespuesta = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<PersonalAutocompleteDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la informacion necesaria del Personal para la  Agenda.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns>retorna un objeto tipo PersonalDatosAgendaDTO</returns>
        public PersonalDatosAgendaDTO ObtenerDatosPersonalAgenda(int idPersonal)
        {
            try
            {
                string query = "SELECT Id, Nombres, Apellidos, Rol, TipoPersonal, Email, AreaAbrev, Anexo, IdJefe, Central, Anexo3Cx, Id3cx, Password3Cx, Dominio, UsuarioAsterisk, ContrasenaAsterisk FROM gp.V_TPersonal_DatosAgenda WHERE Estado=1 AND Activo=1 AND Id=@IdPersonal";
                var respuestaQuery = _dapper.FirstOrDefault(query, new { IdPersonal = idPersonal });
                if (respuestaQuery != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalDatosAgendaDTO>(respuestaQuery);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<PersonalAsignadoDTO> GetPersonalAsignado(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinados";
                string respuestaQuery = _dapper.QuerySPDapper(query, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PersonalEmailDTO ObtenerEmailPersonal(int idPersonal)
        {
            string _queryPersonal = "Select Email from gp.V_TPersonal_ObtenerEmail Where Id=@Id and Estado=1";
            var queryPersonal = _dapper.FirstOrDefault(_queryPersonal, new { Id = idPersonal });
            return JsonConvert.DeserializeObject<PersonalEmailDTO>(queryPersonal);
        }

        public PersonalIdFacebookDTO ObtenerIdFacebookPersonal(int idPersonal)
        {
            string _queryPersonal = "Select IdFacebookPersonal from gp.V_TPersonal_ObtenerIdFacebook Where Id=@Id and Estado=1";
            var queryPersonal = _dapper.FirstOrDefault(_queryPersonal, new { Id = idPersonal });
            return JsonConvert.DeserializeObject<PersonalIdFacebookDTO>(queryPersonal);
        }

        public PersonalMinReasignacionDTO ObtenerPersonalReasignacion(int idAsesor)
        {
            try
            {
                PersonalMinReasignacionDTO personalMinReasignacion = new PersonalMinReasignacionDTO();
                var _query = "SELECT IdAsesor, NombreCompletoAsesor, EmailAsesor, IdJefe, NombreCompletoJefe, EmailJefe FROM com.V_TPersonal_ObtenerAsesorCorreoReasignacion WHERE EstadoPersonal = 1 AND EstadoJefe = 1 AND IdAsesor = @idAsesor";
                var personalDB = _dapper.FirstOrDefault(_query, new { idAsesor });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    personalMinReasignacion = JsonConvert.DeserializeObject<PersonalMinReasignacionDTO>(personalDB);
                }
                return personalMinReasignacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de un personal para el modulo de Asesor Centro Costo
        /// </summary>
        /// <returns>Lista de todo el personal la informacion respectiva para el modulo de Asesor Centro Costo</returns>
        public List<PersonalAsesorCentroCostoDTO> ObtenerPersonalAsesorCentroCosto()
        {
            try
            {
                List<PersonalAsesorCentroCostoDTO> personalAsesorCentroCosto = new List<PersonalAsesorCentroCostoDTO>();
                //var _query = "SELECT NombreAsesor, IdAsesor, Habilitado, IdAsesorCentroCosto, AsignacionMaxima, CantidadAsignados, IdAsesorCentroCostoOcurrencia, AsignacionVA, BalanceoVA,AsignacionMaxBnc,AsignacionMin,HoraInicioDia,HoraFinDia,HoraInicioTarde,HoraFinTarde FROM gp.V_PersonalAsesorCentroCosto";
                var query = "SELECT NombreAsesor, IdAsesor, Habilitado, IdAsesorCentroCosto, AsignacionMaxima, AsignacionMinima, AsignacionMaximaBnc, AsignacionPais, CantidadAsignados FROM gp.V_PersonalAsesorCentroCosto";
                var personalDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(personalDB))
                {
                    personalAsesorCentroCosto = JsonConvert.DeserializeObject<List<PersonalAsesorCentroCostoDTO>>(personalDB);
                }
                return personalAsesorCentroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar Serruto
        ///Fecha: 22/07/2021
        /// <summary>
        /// Obtiene Personal Registrado
        /// </summary>
        /// <returns>List<PersonalGridDTO></returns>
        public List<PersonalGridDTO> ObtenerGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PersonalGridDTO
                {
                    Id = y.Id,
                    Nombres = y.Nombres,
                    Apellidos = y.Apellidos,
                    Rol = y.Rol,
                    Email = y.Email,
                    Activo = y.Activo,
                }).OrderByDescending(x => x.Id).ToList();
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PersonalDTO ObtenerDatosPersonal(int id)
        {
            try
            {
                var personal = GetBy(x => x.Id == id, y => new PersonalDTO
                {
                    Id = y.Id,
                    Nombres = y.Nombres,
                    Apellidos = y.Apellidos,
                    TipoPersonal = y.TipoPersonal,
                    Email = y.Email,
                    Anexo = y.Anexo,
                    IdJefe = y.IdJefe,
                    Central = y.Central,
                    Activo = y.Activo,
                    ApellidoPaterno = y.ApellidoPaterno,
                    ApellidoMaterno = y.ApellidoMaterno,
                    IdSexo = y.IdSexo,
                    IdEstadocivil = y.IdEstadocivil,
                    FechaNacimiento = y.FechaNacimiento,
                    IdPaisNacimiento = y.IdPaisNacimiento,
                    IdRegion = y.IdRegion,
                    IdTipoDocumento = y.IdTipoDocumento,
                    NumeroDocumento = y.NumeroDocumento,
                    UrlFirmaCorreos = y.UrlFirmaCorreos,
                    IdPaisDireccion = y.IdPaisDireccion,
                    IdRegionDireccion = y.IdRegionDireccion,
                    CiudadDireccion = y.CiudadDireccion,
                    NombreDireccion = y.NombreDireccion,
                    FijoReferencia = y.FijoReferencia,
                    MovilReferencia = y.MovilReferencia,
                    EmailReferencia = y.EmailReferencia,
                    IdSistemaPensionario = y.IdSistemaPensionario,
                    IdEntidadSistemaPensionario = y.IdEntidadSistemaPensionario,
                    NombreCuspp = y.NombreCuspp,
                    DistritoDireccion = y.DistritoDireccion
                }).FirstOrDefault();
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de Personal (activos) del Area de Ventas
        /// </summary>
        /// <returns></returns>
        public List<AsesorFiltroDTO> ObtenerPersonalAsesoresFiltro()
        {
            try
            {
                List<AsesorFiltroDTO> personalMinReasignacion = new List<AsesorFiltroDTO>();
                var _query = "SELECT Id, NombreCompleto, Asignado FROM com.V_TPersonal_ObtenerPersonalVentas WHERE Estado = 1 and  Rol = 'VENTAS'";
                var personalDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    personalMinReasignacion = JsonConvert.DeserializeObject<List<AsesorFiltroDTO>>(personalDB);
                }
                return personalMinReasignacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de Personal (activos) del Area de Ventas por Id
        /// </summary>
        /// <returns></returns>
        public List<AsesorFiltroDTO> ObtenerPersonalAsesoresFiltrobyIdPersonal(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinadosVentasAsesores";
                string respuestaQuery = _dapper.QuerySPDapper(query, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<AsesorFiltroDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de Coordinadores (activos) del Area de Ventas
        /// </summary>
        /// <returns></returns>
        public List<CoordinadorFiltroDTO> ObtenerPersonalCoordinadoresFiltro()
        {
            try
            {
                List<CoordinadorFiltroDTO> coordinadores = new List<CoordinadorFiltroDTO>();
                var _query = "SELECT Id, NombreCompleto FROM com.V_TPersonal_ObtenerCoordinadorVentas";
                var personalDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<CoordinadorFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de Coordinadores (activos) del Area de Ventas by Id
        /// </summary>
        /// <returns></returns>
        public List<CoordinadorFiltroDTO> ObtenerPersonalCoordinadoresFiltrobyPersonal(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinadosVentasCoordinadores";
                string respuestaQuery = _dapper.QuerySPDapper(query, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<CoordinadorFiltroDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene todo el Personal
        /// </summary>
        /// <returns> Lista de personal Registrado: List<GestionPersonalDTO> </returns>
        public List<GestionPersonalDTO> ObtenerTodoPersonal()
        {
            try
            {
                List<GestionPersonalDTO> personalMinReasignacion = new List<GestionPersonalDTO>();
                var query = "SELECT Id, Nombres, Apellidos,Area,AsesorCoordinador,AreaAbrev,email,UsuarioModificacion,FechaModificacion,Anexo,Jefe,IdCentral,IdJefe,IdArea,Activo,Estado, Id3CX, Password3CX,UsuarioAsterisk,ContrasenaAsterisk, IdGmailCliente, PasswordCorreo FROM gp.V_TPersonal_ObtenerDatos WHERE Estado = 1  AND RowNumber = 1 ORDER BY FechaModificacion DESC ";
                var personalDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    personalMinReasignacion = JsonConvert.DeserializeObject<List<GestionPersonalDTO>>(personalDB);
                }
                return personalMinReasignacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: PersonalRepositorio
        ///Autor: Jose Villena.
        ///Fecha: 28/04/2021
        /// <summary>
        /// Obtiene Nombres Filtrado AutoComplete
        /// </summary>
        /// <returns> Lista de nombres Registrados: List<PersonalAutocompleteDTO> </returns>
        public List<PersonalAutocompleteDTO> ObtenerNombresFiltroAutoComplete(string valor)
        {
            try
            {
                List<PersonalAutocompleteDTO> PersonalAutocompleteFiltro = new List<PersonalAutocompleteDTO>();
                string queryPersonalNombresFiltro = string.Empty;
                queryPersonalNombresFiltro = "SELECT Id,Nombre FROM gp.V_TPersonal_NombreCompleto WHERE Nombre LIKE CONCAT('%',@valor,'%') AND Estado = 1 ORDER By Id ASC";
                var PersonalDB = _dapper.QueryDapper(queryPersonalNombresFiltro, new { valor });
                if (!string.IsNullOrEmpty(PersonalDB) && !PersonalDB.Contains("[]"))
                {
                    PersonalAutocompleteFiltro = JsonConvert.DeserializeObject<List<PersonalAutocompleteDTO>>(PersonalDB);
                }
                return PersonalAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id,Nombre,Apellidos,NombreCompleto,IdAsesor de los todo el personal que son asesores
        /// </summary>
        /// <returns>Lista de objetos de clase DatosPersonalAsesorDTO</returns>
        public List<DatosPersonalAsesorDTO> ObtenerTodoPersonalAsesoresFiltro()
        {
            try
            {
                List<DatosPersonalAsesorDTO> personalAsesores = new List<DatosPersonalAsesorDTO>();
                var query = string.Empty;
                query = "SELECT Id,Nombres,Apellidos,Email,NombreCompleto,asignado,IdAsesor FROM gp.V_TPersonal_ObtenerAsesores WHERE Rol = 'VENTAS' and (TipoPersonal = 'Coordinador' or TipoPersonal = 'Asesor') and Estado = 1 order by Id";
                var personalAsesor = _dapper.QueryDapper(query, null);
                personalAsesores = JsonConvert.DeserializeObject<List<DatosPersonalAsesorDTO>>(personalAsesor);

                return personalAsesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene  todo el personal que son asesores por id de grupo
        /// </summary>
        /// <param name="idGrupo">Id del grupo de filtro programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)</param>
        /// <returns>Lista de objetos de clase DatosPersonalAsesorPorGrupoIdDTO</returns>
        public List<DatosPersonalAsesorPorGrupoIdDTO> ObtenerAsesoresPorGrupoId(int idGrupo)
        {
            try
            {
                List<DatosPersonalAsesorPorGrupoIdDTO> personalAsesores = new List<DatosPersonalAsesorPorGrupoIdDTO>();
                var query = string.Empty;
                query = "SELECT Id,Nombres,Apellidos,Email,NombreCompleto,asignado,IdAsesor FROM gp.V_TPersonal_ObtenerAsesoresPorGrupoId WHERE Rol = 'VENTAS' and IdGrupo = @IdGrupo and Estado = 1 ";
                var personalAsesor = _dapper.QueryDapper(query, new { IdGrupo = idGrupo });
                personalAsesores = JsonConvert.DeserializeObject<List<DatosPersonalAsesorPorGrupoIdDTO>>(personalAsesor);

                return personalAsesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar Serruto.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene Personal por Filtro
        /// </summary>
        /// <returns>List<FiltroCombosDTO></returns>
        public List<FiltroCombosDTO> ObtenerPersonalFiltro()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new FiltroCombosDTO { Id = x.Id, Nombre = string.Concat(x.Nombres, " ", x.Apellidos) }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Carlos Crispin R.
        ///Fecha: 17/03/2021
        /// <summary>
        /// Obtiene Personal responable para marketing por Filtro
        /// </summary>
        /// <returns>Lista de Personal de Marketing</returns>
        public List<FiltroCombosDTO> ObtenerPersonalMarketingFiltro()
        {
            try
            {
                return GetBy(x => x.Estado == true && x.Activo == true && x.Rol == "Marketing", x => new FiltroCombosDTO { Id = x.Id, Nombre = string.Concat(x.Nombres, " ", x.Apellidos) }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los campos del personal por los apellidos del Personal
        /// </summary>
        /// <returns></returns>
        public List<PersonalDatosPorApellidoDTO> ObtenerUsuariosAutoCompleto(string apellido)
        {
            try
            {

                List<PersonalDatosPorApellidoDTO> personalFiltro = new List<PersonalDatosPorApellidoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,NombreCompleto,Email,Rol FROM pla.V_ObtenerPersonalAutoCompleto WHERE NombreCompleto LIKE '%'+@apellido+'%'  and Estado = 1 ";
                var personalFiltros = _dapper.QueryDapper(_query, new { apellido });
                personalFiltro = JsonConvert.DeserializeObject<List<PersonalDatosPorApellidoDTO>>(personalFiltros);

                return personalFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        ///  Obtiene Id, Nombre, Apellido, CorreoElectronico de todo Personal Activo y con Estado=1
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns> 
        public List<PersonalActivoEmailDTO> GetTodoPersonalActivoParaFiltro()
        {
            try
            {
                List<PersonalActivoEmailDTO> personalAsesores = new List<PersonalActivoEmailDTO>();
                var _query = "SELECT Id, Nombres, Apellidos, Email FROM com.V_TPersonal_ObtenerAsesores where Rol = 'VENTAS' AND (TipoPersonal IN ('ASESOR','Coordinador')) AND Activo = 1 and Estado = 1";
                var personalAsesor = _dapper.QueryDapper(_query, null);
                personalAsesores = JsonConvert.DeserializeObject<List<PersonalActivoEmailDTO>>(personalAsesor);
                return personalAsesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los registros Id, NombreCompleto, Rol, Email del personal filtrados por  apellido
        /// </summary>
        /// <param name="apellidoPaterno"></param>
        /// <returns></returns>
        public List<PersonalDatosPorApellidoDTO> ObtenerPersonalAutocompletoPorApellidoPaterno(string apellidoPaterno)
        {
            try
            {
                List<PersonalDatosPorApellidoDTO> obtenerPersonalPorApellido = new List<PersonalDatosPorApellidoDTO>();
                var _query = "SELECT Id, NombreCompleto, Email, Rol from gp.V_ObtenerTPersonalFiltro where NombreCompleto LIKE CONCAT('%',@apellidoPaterno,'%') AND Estado = 1 AND TipoPersonal IS NOT NULL";
                var obtenerPersonalPorApelllidoDB = _dapper.QueryDapper(_query, new { apellidoPaterno });
                if (!string.IsNullOrEmpty(obtenerPersonalPorApelllidoDB) && !obtenerPersonalPorApelllidoDB.Contains("[]"))
                {
                    obtenerPersonalPorApellido = JsonConvert.DeserializeObject<List<PersonalDatosPorApellidoDTO>>(obtenerPersonalPorApelllidoDB);
                }
                return obtenerPersonalPorApellido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los subordinados activos de un Coordinador
        /// </summary>
        /// <param name="coordinador"></param>
        /// <returns></returns>
        public List<AsesorNombreFiltroDTO> ObtenerSubordinadosCoordinador(int coordinador)
        {
            try
            {
                List<AsesorNombreFiltroDTO> coordinadores = new List<AsesorNombreFiltroDTO>();
                var _query = "SELECT Id, NombreCompleto FROM gp.V_TPersonal_ObtenerSubordinado where IdJefe = @Coordinador and estado = 1 and activo = 1";
                var personalDB = _dapper.QueryDapper(_query, new { Coordinador = coordinador });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<AsesorNombreFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene todo el personal tipo asesor y pertenescan solo al area de ventas
        /// </summary>
        /// <param name="coordinador"></param>
        /// <returns></returns>
        public List<AsesorNombreFiltroDTO> ObtenerAsesoresVentas()
        {
            try
            {
                List<AsesorNombreFiltroDTO> coordinadores = new List<AsesorNombreFiltroDTO>();
                //var _query = "SELECT Id, NombreCompleto FROM gp.V_TPersonal_ObtenerSubordinado where estado = 1 and activo = 1 and TipoPersonal = @TipoPersonal and Rol = @Rol";
                //var personalDB = _dapper.QueryDapper(_query, new { TipoPersonal = "Asesor", Rol = "VENTAS" });
                var _query = "SELECT Id, NombreCompleto FROM gp.V_TPersonal_ObtenerSubordinado where estado = 1 and activo = 1 and  Rol = @Rol";
                var personalDB = _dapper.QueryDapper(_query, new { Rol = "VENTAS" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<AsesorNombreFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el personal indicado por marketing para el envio de sus archvios por el modulo de envio para 1 pc
        /// </summary>
        /// <returns></returns>
        public List<PersonalEnvioMarketingDTO> ObtenerPersonalEnviarCorreoMarketing()
        {
            try
            {
                List<PersonalEnvioMarketingDTO> listado = new List<PersonalEnvioMarketingDTO>();
                var _query = "SELECT Id, Email FROM [gp].[V_TPersonal_EnvioMarketing]";
                var personalDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    listado = JsonConvert.DeserializeObject<List<PersonalEnvioMarketingDTO>>(personalDB);
                }
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /////////////////FUNCIONES  A UTILIZAR PARA OBTENER ASESORES Y COORDINADORES///////////////////

        /// Repositorio: PersonalRepositorio
        /// Autor: _ _ _ _ _ _ _ _ _.
        /// Version: 1.1
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene todos los asesores sin ningun  tipo de restriccion
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<AsesorNombreFiltroDTO> </returns>
        public List<AsesorNombreFiltroDTO> ObtenerAsesoresVentasOficial()
        {
            try
            {
                List<AsesorNombreFiltroDTO> asesores = new List<AsesorNombreFiltroDTO>();
                var query = "SELECT Id,NombreCompleto,Activo,Estado,IdJefe FROM gp.V_TPersonal_Ventas where TipoPersonal <> @TipoPersonal";
                var personalDB = _dapper.QueryDapper(query, new { TipoPersonal = "Coordinador" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    asesores = JsonConvert.DeserializeObject<List<AsesorNombreFiltroDTO>>(personalDB);
                }
                return asesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Autor: Gian Miranda
        ///Fecha: 28/04/2021
        /// <summary>
        /// Obtiene todos los asesores de ventas para el reporte de seguimiento
        /// </summary>
        /// <returns>Lista de objetos de clase PersonalAsignadoDTO</returns>
        public List<PersonalAsignadoDTO> ObtenerAsesoresVentasOficialReporteSeguimiento()
        {
            try
            {
                List<PersonalAsignadoDTO> asesores = new List<PersonalAsignadoDTO>();
                var query = "SELECT Id,NombreCompleto Nombres, Email, Activo FROM gp.V_TPersonal_Ventas where TipoPersonal <> @TipoPersonal";
                var personalDB = _dapper.QueryDapper(query, new { TipoPersonal = "Coordinador" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    asesores = JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(personalDB);
                }
                return asesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PersonalRepositorio
        /// Autor: _ _ _ _ _ _ _ _.
        /// Fecha: 17/04/2021
        /// <summary>
        /// Obtiene todos los coordinadores sin ningun  tipo de restriccion
        /// </summary>
        /// <returns> List<CoordinadorFiltroDTO> </returns>
        public List<CoordinadorFiltroDTO> ObtenerCoordinadoresVentasOficial()
        {
            try
            {
                List<CoordinadorFiltroDTO> coordinadores = new List<CoordinadorFiltroDTO>();
                var query = "SELECT Id,NombreCompleto,Activo,Estado,IdJefe FROM gp.V_TPersonal_Ventas where TipoPersonal = @TipoPersonal";
                var personalDB = _dapper.QueryDapper(query, new { TipoPersonal = "Coordinador" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<CoordinadorFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los asesores activos=1,estado=1  tipo de restriccion
        /// </summary>
        /// <returns></returns>
        public List<AsesorNombreFiltroDTO> ObtenerAsesoresActivosVentasOficial()
        {
            try
            {
                List<AsesorNombreFiltroDTO> coordinadores = new List<AsesorNombreFiltroDTO>();
                var _query = "SELECT Id,NombreCompleto,Activo,Estado,IdJefe FROM gp.V_TPersonal_Ventas where TipoPersonal <> @TipoPersonal and Activo=1 and Estado=1";
                var personalDB = _dapper.QueryDapper(_query, new { TipoPersonal = "Coordinador" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<AsesorNombreFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los coordinadores activos=1,estado=1  tipo de restriccion
        /// </summary>
        /// <returns></returns>
        public List<AsesorNombreFiltroDTO> ObtenerCoordinadoresActivosVentasOficial()
        {
            try
            {
                List<AsesorNombreFiltroDTO> coordinadores = new List<AsesorNombreFiltroDTO>();
                var _query = "SELECT Id,NombreCompleto,Activo,Estado,IdJefe FROM gp.V_TPersonal_Ventas where TipoPersonal = @TipoPersonal and Activo=1 and Estado=1";
                var personalDB = _dapper.QueryDapper(_query, new { TipoPersonal = "Coordinador" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<AsesorNombreFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /////////////////FIN FUNCIONES  A UTILIZAR PARA OBTENER ASESORES Y COORDINADORES///////////////////

        /// <summary>
        /// Obtiene todo el personal tipo asesor y pertenescan solo al area de ventas
        /// </summary>
        /// <param name="coordinador"></param>
        /// <returns></returns>
        public List<AsesorNombreFiltroDTO> ObtenerCoordinadorasOperaciones()
        {
            try
            {
                List<AsesorNombreFiltroDTO> coordinadores = new List<AsesorNombreFiltroDTO>();
                var _query = "SELECT Id, NombreCompleto FROM gp.V_TPersonal_ObtenerSubordinado where estado = 1 and activo = 1 and  Rol = @Rol";
                var personalDB = _dapper.QueryDapper(_query, new { Rol = "OPERACIONES" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<AsesorNombreFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los asesores y sus coordinadores para filtros (id, nombres)
        /// </summary>
        /// <returns>Lista de objetos de clase AsesorNombreFiltroDTO</returns>
        public List<AsesorNombreFiltroDTO> ObtenerTodoAsesorCoordinadorVentas()
        {
            try
            {
                List<AsesorNombreFiltroDTO> coordinadores = new List<AsesorNombreFiltroDTO>();
                var query = @"SELECT Id,
                                    Concat(Nombres, ' ',Apellidos) AS NombreCompleto
                                FROM com.V_TPersonal_ObtenerAsesores
                                WHERE Rol = 'VENTAS' AND (TipoPersonal IN ('ASESOR','Coordinador')) AND estado = 1";
                var personalDB = _dapper.QueryDapper(query, new { TipoPersonal = "Asesor", Rol = "VENTAS" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<AsesorNombreFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el email de personal por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PersonalEmailFiltroDTO ObtenerEmailPersonalPorId(int id)
        {
            try
            {
                PersonalEmailFiltroDTO personal = new PersonalEmailFiltroDTO();
                var _query = string.Empty;
                _query = "SELECT Id, Email FROM GP.V_TPersonal_EmailPersonal WHERE Estado = 1 AND Id = @id";
                var personalDB = _dapper.FirstOrDefault(_query, new { id });
                personal = JsonConvert.DeserializeObject<PersonalEmailFiltroDTO>(personalDB);
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// Retrna todo el personal de ventas, Coordinador y asesores
        /// </summary>
        /// <returns></returns>
        public List<AsesorNombreFiltroDTO> ObtenerPersonalVentas()
        {
            try
            {
                List<AsesorNombreFiltroDTO> coordinadores = new List<AsesorNombreFiltroDTO>();
                var _query = "SELECT Id, NombreCompleto FROM gp.V_TPersonal_ObtenerSubordinado where estado = 1 and activo = 1  and Rol = @Rol";
                var personalDB = _dapper.QueryDapper(_query, new { Rol = "VENTAS" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<AsesorNombreFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PersonalRepositorio
        /// Autor: Edgar S.
        /// Fecha: 08/03/2021
        /// <summary>
        /// Obtiene los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns>Lista de ObjetosDTO: List(PersonalAsignadoDTO)</returns>
        public List<PersonalAsignadoDTO> GetPersonalAsignadoVentas(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinadosVentas";
                string respuestaQuery = _dapper.QuerySPDapper(query, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>Original solo activos
        /// Obtiene los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<PersonalAsignadoDTO> GetPersonalAsignadoOperaciones(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinadosOperaciones";
                string respuestaQuery = _dapper.QuerySPDapper(query, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene Todos los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesTotal(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinadosOperacionesTodos";
                string respuestaQuery = _dapper.QuerySPDapper(query, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesTotalV2(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinadosOperacionesTodosV2";
                string respuestaQuery = _dapper.QuerySPDapper(query, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Todos los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesUsuarioTotal(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinadosOperacionesTodosUsuario";
                string respuestaQuery = _dapper.QuerySPDapper(query, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Todos los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public PersonalAsignadoReportePendienteDTO ObtenerDatosUsuariosReportePendiente(string usuario)
        {
            try
            {
                PersonalAsignadoReportePendienteDTO personal = new PersonalAsignadoReportePendienteDTO();
                var _query = string.Empty;
                _query = "SELECT Id,Nombres,Activo,Email,TipoPersonal,Usuario FROM gp.V_ObtenerDatosPersonalPorUsuario WHERE Usuario = @usuario";
                var personalDB = _dapper.FirstOrDefault(_query, new { usuario });
                personal = JsonConvert.DeserializeObject<PersonalAsignadoReportePendienteDTO>(personalDB);
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el email1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerEmail(int id)
        {
            try
            {
                return this.GetBy(x => x.Id == id).FirstOrDefault().Email;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene email de Personal Repetido y desactivado
        /// </summary>        
        /// <param name="email"> Email del Personal </param>
        /// <returns> Registro email de Personal Repetido y desactivado : Dictionary<string, int> </returns>
        public int? ObtenerPersonalEliminadoEmailRepetido(string email)
        {
            try
            {
                Dictionary<string, int> personal = new Dictionary<string, int>();
                var query = "SELECT Id FROM gp.T_Personal where estado = 0 and Email=@Email";
                var personalDB = _dapper.QueryDapper(query, new { Email = email });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    personal = JsonConvert.DeserializeObject<Dictionary<string, int>>(personalDB);
                }
                return personal.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Britsel C., Luis H., Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Activa personal por Id
        /// </summary>        
        /// <param name="id"> Id del Personal </param>
        /// <returns> Confirmación de Activación de personal </returns>
        /// <returns> Bool </returns>
        public bool ActivarPersonal(int id)
        {
            try
            {
                _dapper.QueryDapper("UPDATE gp.T_Personal set Estado=1 where Id=@Id", new { id = id });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///// <summary>
        ///// Obtiene los asesores asignados a un Personal.
        ///// </summary>
        ///// <param name="idPersonal"></param>
        ///// <returns></returns>
        //public List<PersonalAsignadoDTO> GetPersonalAsignadoVentasAsesores(int idPersonal)
        //{
        //    try
        //    {
        //        string query = "com.SP_TPersonal_GetSubordinadosVentas";
        //        string respuestaQuery = _dapper.QuerySPDapper(query, new { IdPersonal = idPersonal });
        //        return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        ///// <summary>
        ///// Obtiene los asesores asignados a un Personal.
        ///// </summary>
        ///// <param name="idPersonal"></param>
        ///// <returns></returns>
        //public List<PersonalAsignadoDTO> GetPersonalAsignadoVentasCoordinadores(int idPersonal)
        //{
        //    try
        //    {
        //        string query = "com.SP_TPersonal_GetSubordinadosVentas";
        //        string respuestaQuery = _dapper.QuerySPDapper(query, new { IdPersonal = idPersonal });
        //        return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 04/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los coordinadores de operaciones cruzado con personal
        /// </summary>
        /// <returns>Lista de coordinadores DatoPersonalCoordinadorDTO</returns>
        public List<DatoPersonalCoordinadorDTO> ObtenerCoordinadoresOperaciones()
        {
            try
            {
                List<DatoPersonalCoordinadorDTO> coordinadores = new List<DatoPersonalCoordinadorDTO>();
                var _query = "SELECT Usuario, NombreCompleto FROM ope.V_ObtenerCoordinadorasOperaciones where Estado = 1 ORDER BY NombreCompleto";
                var personalDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<DatoPersonalCoordinadorDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el Personal que tiene como tipo personal coordinador,incluyendo el ninguno.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<DatoPersonalDTO> ObtenerTipoPersonalCoordinador(string coordinador)
        {
            try
            {
                string query = "gp.SP_ObtenerTipoPersonaCoordinador";
                string respuestaQuery = _dapper.QuerySPDapper(query, new { coordinador });
                return JsonConvert.DeserializeObject<List<DatoPersonalDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el Id,NombreCompleto de los asesores Area Ventas
        /// </summary>
        /// <returns></returns>
        public List<DatoPersonalDTO> ObtenerTodoPersonalAsesoresFiltroAutocomplete(string valor)
        {
            try
            {
                List<DatoPersonalDTO> personalAsesores = new List<DatoPersonalDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,NombreCompleto FROM gp.V_TPersonal_ObtenerAsesores WHERE Apellidos LIKE CONCAT('%',@valor,'%') and Rol = 'VENTAS' and (TipoPersonal = 'Coordinador' or TipoPersonal = 'Asesor') and Estado = 1";
                var personalAsesor = _dapper.QueryDapper(_query, new { valor });
                personalAsesores = JsonConvert.DeserializeObject<List<DatoPersonalDTO>>(personalAsesor);

                return personalAsesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el personal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DatoCompletoPersonalDTO ObtenerDatoPersonal(int id)
        {
            try
            {
                var personal = new DatoCompletoPersonalDTO();
                var query = $@"
                            SELECT Id, 
                                   Nombres, 
                                   Apellidos, 
                                   Anexo3Cx, 
                                   Central, 
                                   Email,
                                   MovilReferencia, 
                                   PrimerNombreApellidoPaterno,
                                   PrimerNombre AS Nombre1
                            FROM gp.V_ObtenerPersonalNombreCompleto
                            WHERE Estado = 1
                                  AND Activo = 1
                                  AND Id = @id
                            ";
                var personalAsesor = _dapper.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(personalAsesor))
                {
                    personal = JsonConvert.DeserializeObject<DatoCompletoPersonalDTO>(personalAsesor);
                }
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<DatosOperacionesDTO> GetDatosOperaciones(string DNI)
        {
            try
            {
                string query = "com.SP_TPersonal_GetDatosOperaciones";
                string respuestaQuery = _dapper.QuerySPDapper(query, new { DNIAlumno = DNI });
                return JsonConvert.DeserializeObject<List<DatosOperacionesDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los asesores de operaciones asignados por personal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<AsesorFiltroDTO> ObtenerPersonalAsesoresOperacionesPorIdPersonal(int idPersonal)
        {
            try
            {
                var listaPersonalAsignado = new List<AsesorFiltroDTO>();
                var query = "[com].[SP_TPersonal_GetSubordinadosOperaciones_V2]";
                var respuestaQuery = _dapper.QuerySPDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(respuestaQuery))
                {
                    listaPersonalAsignado = JsonConvert.DeserializeObject<List<AsesorFiltroDTO>>(respuestaQuery);
                }
                return listaPersonalAsignado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jashin Salazar
        /// Fecha: 21/06/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los asesores de operaciones activos
        /// </summary>
        /// <returns></returns>
        public List<AsesorFiltroDTO> ObtenerPersonalAsesoresOperacionesActivos()
        {
            try
            {
                var listaPersonal = new List<AsesorFiltroDTO>();
                var query = "com.SP_TPersonalObtenerAsistenteOperaciones";
                var respuestaQuery = _dapper.QuerySPDapper(query, new { });
                if (!string.IsNullOrEmpty(respuestaQuery))
                {
                    listaPersonal = JsonConvert.DeserializeObject<List<AsesorFiltroDTO>>(respuestaQuery);
                }
                return listaPersonal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el horario de trabajo del personal
        /// </summary>
        /// <param name="Id">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <returns>Obtener el horario de trabajo de un personal en formato HTML</returns>
        public string ObtenerHorarioTrabajo(int id)
        {
            try
            {
                var resultadoFinal = new ValorStringDTO();
                var query = "gp.SP_ObtenerPersonalHorarioAtencion";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdPersonal = id });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el puesto de trabajo de la persona
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerPuestoTrabajo(int id)
        {
            try
            {
                var personal = new ValorStringDTO();
                var query = $@"
                            SELECT
                            PuestoTrabajo.NomPuestoTrabajo AS Valor
                            FROM gp.T_Personal AS Personal
                                 INNER JOIN bsadmin_prod.DO.TDO_DatosContratosPersonal AS DatoContratoPersonal ON Personal.Id = DatoContratoPersonal.FK_IdPersonal
                                 INNER JOIN bsadmin_prod.DO.TDO_PuestoTrabajo AS PuestoTrabajo ON DatoContratoPersonal.FK_IdPuestoTrabajo = PuestoTrabajo.Id
                            WHERE PuestoTrabajo.ESTADO = 1
                                  AND DatoContratoPersonal.Estado_Contrato = 1			  
                                  AND Personal.Id = @id;

                            ";
                var personalAsesor = _dapper.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(personalAsesor))
                {
                    personal = JsonConvert.DeserializeObject<ValorStringDTO>(personalAsesor);
                }
                return personal.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 01/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener Coordinadores Para Filtro
        /// </summary>
        /// <param></param>
        /// <returns>Lista Objeto: List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerCoordinadoresParaFiltro()
        {
            try
            {
                var query = "SELECT Id, Nombre,TipoPersonal FROM [ope].[V_ObtenerCoordinadoresOperaciones] WHERE Estado = 1 AND Activo = 1 AND (Rol = 'OPERACIONES' or Rol = 'Atención al cliente') AND IdRol = 17";
                var res = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtener primer nombre del personal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerPrimerNombre(int id)
        {
            try
            {
                var _resultado = new ValorStringDTO();
                var query = $@"
                            SELECT PrimerNombre AS Valor
                            FROM gp.V_ObtenerPersonalNombreCompleto
                            WHERE Estado = 1
                                  AND Activo = 1
                                  AND Id = @id;
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene el primer nombre y apellido del personal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerPrimerNombreApellidoPaterno(int id)
        {
            try
            {
                var _resultado = new ValorStringDTO();
                var query = $@"
                            SELECT PrimerNombreApellidoPaterno as Valor
                            FROM gp.V_ObtenerPersonalNombreCompleto
                            WHERE Estado = 1
                                  AND Activo = 1
                                  AND Id = @Id 
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene el rol anterior
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerRolAnterior(int id)
        {
            try
            {
                return this.GetBy(x => x.Id == id).FirstOrDefault().Rol;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el tipo personal anterior
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerTipoPersonalAnterior(int id)
        {
            try
            {
                return this.GetBy(x => x.Id == id).FirstOrDefault().TipoPersonal;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la fecha de creacion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DateTime ObtenerFechaCreacion(int id)
        {
            try
            {
                return this.GetBy(x => x.Id == id).FirstOrDefault().FechaCreacion;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PersonalRepositorio
        /// Autor: 
        /// Fecha: 16/06/2021
        /// <summary>
        /// Se obtiene para filtro
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado == true && x.Activo == true, x => new FiltroDTO { Id = x.Id, Nombre = string.Concat(x.Nombres, " ", x.Apellidos) }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtiene lista de informacion de personal registrado
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns>ñosta de registros de personal en sistema</returns>
        public MaestroPersonalDTO ObtenerInformacionPersonal(int idPersonal)
        {
            try
            {
                MaestroPersonalDTO personal = new MaestroPersonalDTO();
                var query = @"
					SELECT Id, 
						   Apellidos, 
						   Nombres, 
                           IdPersonalAreaTrabajo,
						   FijoReferencia, 
						   MovilReferencia, 
						   EmailReferencia, 
						   IdPaisNacimiento, 
						   IdCiudad, 
						   FechaNacimiento, 
						   IdPaisDireccion, 
						   IdRegionDireccion, 
						   DistritoDireccion, 
						   NombreDireccion, 
						   IdTipoDocumento, 
						   NumeroDocumento, 
						   IdEstadoCivil, 
						   IdSexo, 
						   IdSistemaPensionario, 
						   IdEntidadSistemaPensionario, 
						   CodigoAfiliado,
						   IdEntidadSeguroSalud,
						   Email,
						   TipoPersonal,
						   IdJefe,
						   Central,
						   Anexo3CX,
						   UrlFirmaCorreos,
						   Activo, 
						   IdTipoSangre,
						   Estado
					FROM [gp].[V_TPersonal_InformacionPersonalRegistrado]
					WHERE Estado = 1 AND Id = @IdPersonal";
                var res = _dapper.FirstOrDefault(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(res))
                {
                    personal = JsonConvert.DeserializeObject<MaestroPersonalDTO>(res);
                }
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<DatoPersonalDTO> getDatosPersonal()
        {
            try
            {
                List<DatoPersonalDTO> personal = new List<DatoPersonalDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,NombreCompleto FROM [gp].[V_TPersonalDatos] WHERE Activo = 1 and Estado = 1";
                var tpersonal = _dapper.QueryDapper(_query, new { });
                personal = JsonConvert.DeserializeObject<List<DatoPersonalDTO>>(tpersonal);
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<DatoPersonalDTO> getDatosPersonalPorArea(string area)
        {
            try
            {
                List<DatoPersonalDTO> personal = new List<DatoPersonalDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,NombreCompleto FROM [gp].[V_TPersonalDatosPorArea] WHERE AreaAbrev =@area and Activo = 1 and Estado = 1 ";
                var tpersonal = _dapper.QueryDapper(_query, new { area });
                personal = JsonConvert.DeserializeObject<List<DatoPersonalDTO>>(tpersonal);
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<DatoPersonalDTO> getDatosIdPersonalPorArea(string area)
        {
            try
            {
                List<DatoPersonalDTO> personal = new List<DatoPersonalDTO>();
                var _query = string.Empty;
                _query = "SELECT Id FROM [gp].[V_TPersonalDatosPorArea] WHERE AreaAbrev =@area and Activo = 1 and Estado = 1 ";
                var tpersonal = _dapper.QueryDapper(_query, new { area });
                if (!string.IsNullOrEmpty(tpersonal) && !tpersonal.Contains("[]"))
                {
                    personal = JsonConvert.DeserializeObject<List<DatoPersonalDTO>>(tpersonal);
                }

                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<DatoPersonalDTO> getDatosPersonalGestionPersonas()
        {
            try
            {
                List<DatoPersonalDTO> personal = new List<DatoPersonalDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,NombreCompleto FROM [gp].[V_TPersonalDatosPorArea] WHERE AreaAbrev ='GP' and Estado = 1 ";
                var tpersonal = _dapper.QueryDapper(_query, null);
                personal = JsonConvert.DeserializeObject<List<DatoPersonalDTO>>(tpersonal);
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Se obtiene los emails en estado 0
        /// </summary>
        /// <returns></returns>
        public int ObtenerEmailEstadoFalse(string email)
        {
            try
            {
                Dictionary<string, int> personal = new Dictionary<string, int>();

                var _query = "SELECT Id FROM [gp].[V_TPersonal_EmailInactivo] WHERE Email=@Email";
                var personalDB = _dapper.QueryDapper(_query, new { Email = email });

                personalDB = personalDB.Replace("[", "").Replace("]", "");

                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    personal = JsonConvert.DeserializeObject<Dictionary<string, int>>(personalDB);
                }
                return personal.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Se obtiene el numero de documento de personal inactivo
        /// </summary>
        /// <returns></returns>
        public string ObtenerNumeroDocumento(int IdPersonal)
        {
            try
            {
                Dictionary<string, string> personal = new Dictionary<string, string>();
                var _query = "SELECT NumeroDocumento FROM [gp].[V_TPersonal_ObtenerDNI] where estado = 0 and Id=@IdPersonal";
                var personalDB = _dapper.QueryDapper(_query, new { IdPersonal = IdPersonal });

                personalDB = personalDB.Replace("[", "").Replace("]", "");

                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    personal = JsonConvert.DeserializeObject<Dictionary<string, string>>(personalDB);
                }
                return personal.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// <summary>
        /// Se obtiene los emails repetidos de personal activo
        /// </summary>
        /// <returns></returns>
        public List<PersonalDTO> ObtenerListaEmailRepetidosActivos(string email)
        {
            try
            {
                return GetBy(x => x.Estado == true && x.Email.Equals(email), x => new PersonalDTO { Id = x.Id, Email = x.Email }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Se obtiene Codigo y Nombre de Codigo de Postulante segun proceso de seleccion
        /// </summary>
        /// <returns></returns>
        public PostulanteAreaRolDTO ObtenerCodigoRolPersonal(int IdPostulante)
        {
            try
            {
                PostulanteAreaRolDTO personal = new PostulanteAreaRolDTO();
                var _query = "SELECT * FROM [gp].[V_TPostulante_ImportacionRolAPersonal] where estado = 1 AND RowNumber = 1 AND Id=@IdPostulante";
                var personalDB = _dapper.FirstOrDefault(_query, new { IdPostulante = IdPostulante });
                personalDB = personalDB.Replace("[", "").Replace("]", "");

                return personal = JsonConvert.DeserializeObject<PostulanteAreaRolDTO>(personalDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene el Id y Nombre del Personal a trav�s del nombre del personal.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<PersonalAutocompleteDTO> CargarPersonalAutoCompleteContrato(string nombre)
        {
            try
            {
                string query = "SELECT Id, Nombre from gp.V_TPersonal_NombreCompleto WHERE Nombre LIKE CONCAT('%',@nombre,'%') AND Estado = 1 ORDER By Nombre ASC";
                string queryRespuesta = _dapper.QueryDapper(query, new { nombre });
                return JsonConvert.DeserializeObject<List<PersonalAutocompleteDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




        /// <summary>
        /// Obtiene lista de Personal Y Definiciones de Id
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<PersonalFormularioDTO> ObtenerPorPersonal(int IdPersonal)
        {
            try
            {
                string query = "SELECT * from [gp].[TPersonal_ObtenerPersonalFormulario] WHERE Id = @IdPersonal AND Estado = 1";
                string queryRespuesta = _dapper.QueryDapper(query, new { IdPersonal });
                return JsonConvert.DeserializeObject<List<PersonalFormularioDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PersonalRepositorio
        /// Autor: Luis Huallpa - Britsel Calluchi.
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene Dirección Domiciliario del Personal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> List<PersonalDireccionVistaDTO> </returns>
        public List<PersonalDireccionVistaDTO> ObtenerPersonalDireccionDomiciliaria(int idPersonal)
        {
            try
            {
                List<PersonalDireccionVistaDTO> lista = new List<PersonalDireccionVistaDTO>();
                var query = "SELECT IdPersonal, IdPais, IdCiudad, Distrito, TipoVia, NombreVia, Manzana, Lote, TipoZonaUrbana, NombreZonaUrbana, Activo, UsuarioModificacion, FechaModificacion FROM gp.t_personaldireccion WHERE IdPersonal = @IdPersonal AND Estado = 1";
                var respuesta = _dapper.QueryDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PersonalDireccionVistaDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene lista de Personal y datos de Usuario
        /// </summary>
        /// <returns> Lista de Personal y datos de Usuario </returns>
        /// <returns> Lista de objeto DTO : List<AccesoSistemaDTO> </returns>
        public List<AccesoSistemaDTO> ObtenerInformacionPersonalUsuario()
        {
            try
            {
                List<AccesoSistemaDTO> personal = new List<AccesoSistemaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombres, Apellidos,Area,AsesorCoordinador,AreaAbrev,email,UsuarioModificacion,FechaModificacion,Anexo,NombreUsuario, ClaveIntegra, Jefe,IdCentral,IdJefe,IdArea,Activo,Estado, Id3CX, Password3CX, IdGmailCliente, PasswordCorreo, IdPuestoTrabajo, PuestoTrabajo, IdSedeTrabajo, SedeTrabajo, IdUsuarioRol, UsuarioRol FROM gp.V_ObtenerDatosPersonalAccesoSistema WHERE Estado = 1  AND RowNumber = 1 ORDER BY FechaModificacion DESC";
                var tpersonal = _dapper.QueryDapper(_query, null);
                personal = JsonConvert.DeserializeObject<List<AccesoSistemaDTO>>(tpersonal);
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene lista de Personal y datos de Usuario Nuevo
        /// </summary>
        /// <returns>Registro de Personal y datos de Usuario</returns>
        public AccesoSistemaDTO ObtenerInformacionPersonalUsuarioNuevo(int idPersonal)
        {
            try
            {
                AccesoSistemaDTO personal = new AccesoSistemaDTO();
                var query = string.Empty;
                query = "SELECT Id, Nombres, Apellidos,Area,AsesorCoordinador,AreaAbrev,email,UsuarioModificacion,FechaModificacion,Anexo,NombreUsuario, ClaveIntegra, Jefe,IdCentral,IdJefe,IdArea,Activo,Estado, Id3CX, Password3CX, IdGmailCliente, PasswordCorreo, IdPuestoTrabajo, PuestoTrabajo, IdSedeTrabajo, SedeTrabajo IdUsuarioRol, UsuarioRol FROM gp.V_ObtenerDatosPersonalAccesoSistema WHERE Estado = 1  AND RowNumber = 1 AND Id = @idPersonal ORDER BY FechaModificacion DESC";
                var tpersonal = _dapper.QueryDapper(query, new { idPersonal = idPersonal });

                tpersonal = tpersonal.Replace("[", "").Replace("]", "");

                if (!string.IsNullOrEmpty(tpersonal) && !tpersonal.Contains("[]"))
                {
                    personal = JsonConvert.DeserializeObject<AccesoSistemaDTO>(tpersonal);
                }

                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene lista de informacion de personal registrado Puesto y Sede
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> MaestroPersonalPuestoSedeDTO </returns>
        public MaestroPersonalPuestoSedeDTO ObtenerInformacionPersonalPuestoSede(int idPersonal)
        {
            try
            {
                MaestroPersonalPuestoSedeDTO personal = new MaestroPersonalPuestoSedeDTO();
                var query = @"
					SELECT Id, 
						   Apellidos, 
						   Nombres, 
                           IdPersonalAreaTrabajo,
						   FijoReferencia, 
						   MovilReferencia, 
						   EmailReferencia, 
						   IdPaisNacimiento, 
						   IdCiudad, 
						   FechaNacimiento, 
						   IdPaisDireccion, 
						   IdRegionDireccion, 
						   DistritoDireccion, 
						   NombreDireccion, 
						   IdTipoDocumento, 
						   NumeroDocumento, 
						   IdEstadoCivil, 
						   IdSexo, 
                           IdPuestoTrabajo,
                           IdSedeTrabajo,
						   IdSistemaPensionario, 
						   IdEntidadSistemaPensionario, 
						   CodigoAfiliado,
						   IdEntidadSeguroSalud,
						   Email,
						   TipoPersonal,
						   IdJefe,
						   Central,
						   Anexo3CX,
						   UrlFirmaCorreos,
						   Activo, 
						   IdTipoSangre,
                           EsCerrador,
                           IdCerrador,
                           IdPuestoTrabajoNivel,
						   Estado,
                           IdTableroComercialCategoriaAsesor,
                           IdPersonalArchivo
					FROM gp.V_TPersonal_InformacionPersonalRegistradoPuestoSede
					WHERE Id = @IdPersonal";
                var res = _dapper.FirstOrDefault(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(res))
                {
                    personal = JsonConvert.DeserializeObject<MaestroPersonalPuestoSedeDTO>(res);
                }
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 16/03/2021
        /// <summary>
        /// Se obtiene Asesores Cerrador para filtro
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        public List<FiltroDTO> ObtenerAsesorCerrador()
        {
            try
            {
                return this.GetBy(x => x.Estado == true && x.Activo == true && x.EsCerrador == true, x => new FiltroDTO { Id = x.Id, Nombre = string.Concat(x.Nombres, " ", x.Apellidos) }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 16/03/2021
        /// <summary>
        /// Se obtiene Información de Personal del Área de Gestión de Personas Activos para Filtro
        /// </summary>
        /// <returns> List<DatoPersonalDTO> </returns>
        public List<DatoPersonalDTO> ObtenerComboPersonalGestionPersonas()
        {
            try
            {
                List<DatoPersonalDTO> personal = new List<DatoPersonalDTO>();
                var query = string.Empty;
                query = "SELECT Id,NombreCompleto FROM gp.V_TPersonalDatosPorArea WHERE AreaAbrev ='GP' AND Activo = 1 AND Estado = 1 ";
                var respuesta = _dapper.QueryDapper(query, null);
                personal = JsonConvert.DeserializeObject<List<DatoPersonalDTO>>(respuesta);
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 16/03/2021
        /// <summary>
        /// Se obtiene Información de Personal y personal a Cargo
        /// </summary>
        /// <returns>List<PersonalJefaturaDTO></returns>
        public List<PersonalJefaturaDTO> ObtenerPersonalJefatura()
        {
            try
            {
                List<PersonalJefaturaDTO> personal = new List<PersonalJefaturaDTO>();
                var query = string.Empty;
                query = "SELECT IdPersonal, Personal, PuestoTrabajo, IdJefeInmediato FROM gp.V_TPersonal_ObtieneJefePersonal";
                var respuesta = _dapper.QueryDapper(query, null);
                personal = JsonConvert.DeserializeObject<List<PersonalJefaturaDTO>>(respuesta);
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 16/03/2021
        /// <summary>
        /// Se obtiene Información de Personal y personal a Cargo por Filtro
        /// </summary>
        /// <param name="condiciones">string de condiciones</param>
        /// <returns>List<FiltroPersonalJefaturaFiltroDTO></returns>
        public List<FiltroPersonalJefaturaFiltroDTO> ObtenerPersonalJefaturaFiltro(string condiciones)
        {
            try
            {
                List<FiltroPersonalJefaturaFiltroDTO> personal = new List<FiltroPersonalJefaturaFiltroDTO>();
                var query = string.Empty;
                if (condiciones.Length > 0)
                {
                    query = "SELECT PersonalAreaTrabajo,Personal,PersonalPuestoTrabajo,PersonasACargo, Estado, FechaInicioPuesto, FechaIngreso, FechaCese, JefeInmediato, PuestoJefeInmediato FROM gp.V_TPersonal_ObtieneJefePersonalFiltro WHERE " + condiciones;
                }
                else
                {
                    query = "SELECT PersonalAreaTrabajo,Personal,PersonalPuestoTrabajo,PersonasACargo, Estado, FechaInicioPuesto, FechaIngreso, FechaCese, JefeInmediato, PuestoJefeInmediato FROM gp.V_TPersonal_ObtieneJefePersonalFiltro";
                }
                var respuesta = _dapper.QueryDapper(query, null);
                personal = JsonConvert.DeserializeObject<List<FiltroPersonalJefaturaFiltroDTO>>(respuesta);
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PersonalConfiguracionOpenVoxDTO> ObtenerConfiguracionOpenVoxPorIdPersonal(int idPersonal)
        {
            try
            {
                var resultadoLista = new List<PersonalConfiguracionOpenVoxDTO>();
                var query = "SELECT IdPais, Prefijo, Anexo FROM COM.V_ObtenerConfiguracionOpenVoxPersonal WHERE IdPersonal = @IdPersonal";
                var resultadoPlano = _dapper.QueryDapper(query, new { IdPersonal = idPersonal });

                if (!string.IsNullOrEmpty(resultadoPlano))
                    resultadoLista = JsonConvert.DeserializeObject<List<PersonalConfiguracionOpenVoxDTO>>(resultadoPlano);

                return resultadoLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jashin Salazar
        /// Fecha: 30/07/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el primer nombre y apellido del personal
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns>ValorStringDTO</returns>
        public ValorStringDTO ObtenerPrimerNombreApellidoPaternoPorUserName(string Usuario)
        {
            try
            {
                var _resultado = new ValorStringDTO();
                var query = $@"
                            SELECT CONCAT(Nombre,' ',ApellidoPaterno) AS Valor FROM gp.V_TPersonal_ObtenerNombreApellidoPaterno WHERE Usuario=@Usuario 
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { Usuario });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 13/12/2021
        /// Version: 1.0
        /// <summary>
        /// Inserta Usuario Nueva Contraseña
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns>ResultadoFinalDTO</returns>
        public ResultadoFinalDTO InsertarUsuarioNuevaContraseña(string usuario, string contrasena)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("com.SP_InsertarCorreoNuevaContraseña", new { Usuario = usuario, NuevaContrasena = contrasena});
                var rpta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<DatoPersonalDiscadorDTO> ObtenerDiscadorPersonal(string filtro)
        {
            try
            {
                List<DatoPersonalDiscadorDTO> items = new List<DatoPersonalDiscadorDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ObtenerPersonalDiscador", new
                {
                    Valor = filtro
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<DatoPersonalDiscadorDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene lista de coordinadoras de los docentes
        /// </summary>
        /// <param name="coordinador"></param>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerCoordinadorasDocente()
        {
            try
            {
                var query = "SELECT DISTINCT PER.Id, CONCAT(Nombres, ' ', Apellidos) AS Nombre FROM gp.T_Personal AS PER INNER JOIN fin.T_Proveedor AS PRO ON PRO.IdPersonal_Asignado = PER.Id WHERE PER.Estado = 1 AND PRO.Estado = 1";
                var res = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jashin Salazar
        /// Fecha: 28/01/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la diferencia horaria del asesor
        /// </summary>
        /// <param name="idPersonal">Id del asesor</param>
        /// <returns>ResultadoFinalNuloDTO</returns>
        public ResultadoFinalNuloDTO ObtenerDiferenciaHoraria(int idPersonal)
        {
            try
            {
                var query = "SELECT DiferenciaHoraria AS Valor FROM gp.T_Personal WHERE Id = @IdPersonal";
                var resultado = _dapper.FirstOrDefault(query, new { IdPersonal = idPersonal });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinalNuloDTO>(resultado);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
