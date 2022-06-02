using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: IntegraAspNetUsersRepositorio
    /// Autor: Nelson Huaman - Richard Zenteno - Wilber Choque - Esthephany Tanco - Priscila Pacsi - Carlos Crispin - Lisbeth Ortogorin - Jose Villena - Edgar Serruto.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Usuarios y Accesos al sistema T_IntegraAspNetUsers
    /// </summary>
    public class IntegraAspNetUsersRepositorio :BaseRepository<TIntegraAspNetUsers, IntegraAspNetUsersBO>
    {

        #region Metodos Base
        public IntegraAspNetUsersRepositorio() : base()
        {
        }
        public IntegraAspNetUsersRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<IntegraAspNetUsersBO> GetBy(Expression<Func<TIntegraAspNetUsers, bool>> filter)
        {
            IEnumerable<TIntegraAspNetUsers> listado = base.GetBy(filter);
            List<IntegraAspNetUsersBO> listadoBO = new List<IntegraAspNetUsersBO>();
            foreach (var itemEntidad in listado)
            {
                IntegraAspNetUsersBO objetoBO = Mapper.Map<TIntegraAspNetUsers, IntegraAspNetUsersBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IntegraAspNetUsersBO FirstById(int id)
        {
            try
            {
                TIntegraAspNetUsers entidad = base.FirstById(id);
                IntegraAspNetUsersBO objetoBO = new IntegraAspNetUsersBO();
                Mapper.Map<TIntegraAspNetUsers, IntegraAspNetUsersBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IntegraAspNetUsersBO FirstBy(Expression<Func<TIntegraAspNetUsers, bool>> filter)
        {
            try
            {
                TIntegraAspNetUsers entidad = base.FirstBy(filter);
                IntegraAspNetUsersBO objetoBO = Mapper.Map<TIntegraAspNetUsers, IntegraAspNetUsersBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IntegraAspNetUsersBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TIntegraAspNetUsers entidad = MapeoEntidad(objetoBO);
                entidad.Id = objetoBO.Id;
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

        public bool Insert(IEnumerable<IntegraAspNetUsersBO> listadoBO)
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

        public bool Update(IntegraAspNetUsersBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TIntegraAspNetUsers entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<IntegraAspNetUsersBO> listadoBO)
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
        private void AsignacionId(TIntegraAspNetUsers entidad, IntegraAspNetUsersBO objetoBO)
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
        
        private TIntegraAspNetUsers MapeoEntidad(IntegraAspNetUsersBO objetoBO)
        {
            try
            {
                TIntegraAspNetUsers entidad = new TIntegraAspNetUsers();
                entidad = Mapper.Map<IntegraAspNetUsersBO, TIntegraAspNetUsers>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<IntegraAspNetUsersBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TIntegraAspNetUsers, bool>>> filters, Expression<Func<TIntegraAspNetUsers, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TIntegraAspNetUsers> listado = base.GetFiltered(filters, orderBy, ascending);
            List<IntegraAspNetUsersBO> listadoBO = new List<IntegraAspNetUsersBO>();

            foreach (var itemEntidad in listado)
            {
                IntegraAspNetUsersBO objetoBO = Mapper.Map<TIntegraAspNetUsers, IntegraAspNetUsersBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene lista de Usuarios
        /// </summary>
        /// <param></param>
        /// <returns> List<GestionUsuariosDTO> </returns>
        public List<GestionUsuariosDTO> ObtenerGestionUsuarioLista()
        {
            try
            {
                List<GestionUsuariosDTO> gestionusuarios = new List<GestionUsuariosDTO>();
                string _queryGestionUsuarios = string.Empty;
                _queryGestionUsuarios = "SELECT Id,UserName,Email,Nombre,Rol,AreaTrabajo,RolId,PerId,UsClave,IdUsuario,UsuarioCreacion,UsuarioModificacion FROM gp.V_ObtenerDatosGestionUsuarios where activo=1 order by fechacreacion desc";
                var GestionUsuario = _dapper.QueryDapper(_queryGestionUsuarios, null);
                if (!string.IsNullOrEmpty(GestionUsuario) && !GestionUsuario.Contains("[]"))
                {
                    gestionusuarios = JsonConvert.DeserializeObject<List<GestionUsuariosDTO>>(GestionUsuario);
                }
                return gestionusuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene lista de módulos asignados a usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns> lista de objetos DTO: List<AsignarModuloDTO> </returns>
        public List<AsignarModuloDTO> ObtenerModulosAsignados(int idUsuario)
        {
            try
            {
                AsignarModuloDTO modulos = new AsignarModuloDTO();
                var query = "select IdUsuario,IdModulo,NombreGrupo,NombreModulo,URL from gp.V_ObtenerModulosAsignados where IdUsuario = @idUsuario and estado=1";
                var asignarModuloDB = _dapper.QueryDapper(query, new { idUsuario });
                return JsonConvert.DeserializeObject<List<AsignarModuloDTO>>(asignarModuloDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene lista de módulos no asignados a un Usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns> Lista de objetosDTO: List<AsignarModuloDTO> </returns>
        public List<AsignarModuloDTO> ObtenerModulosnoAsignados(int idUsuario)
        {
            try
            {
                AsignarModuloDTO modulos = new AsignarModuloDTO();
                var query = "select IdUsuario,IdModulo,NombreGrupo,NombreModulo,URL from gp.V_ObtenerModulosLista where idmodulo not in(select IdModuloSistema from conf.T_ModuloSistemaAcceso where IdUsuario = @idUsuario and estado = 1)";
                var asignarModuloDB = _dapper.QueryDapper(query, new { idUsuario });
                return JsonConvert.DeserializeObject<List<AsignarModuloDTO>>(asignarModuloDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene Usuario y Rol por Filtro
        /// </summary>
        /// <param></param>
        /// <returns> Lista de objetosDTO: List<UsuarioRolDTO> </returns>
        public List<UsuarioRolDTO> ObtenerUsuarioRolFiltro()
        {
            try
            {
                UsuarioRolDTO Usuario = new UsuarioRolDTO();
                var _query = "select Id,Nombre from gp.V_UsuarioRolFiltro";
                var UsuarioRolDB = _dapper.QueryDapper(_query,null);
                return JsonConvert.DeserializeObject<List<UsuarioRolDTO>>(UsuarioRolDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene Información de Acecsos de Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns> IntegraAspNetUsersBO </returns>
        public IntegraAspNetUsersBO ObtenerDatosParaActualizar(string id)
        {
            try
            {
                var listaASP = GetBy(o => true, x => new {
                    x.Id,
                    x.PasswordHash,
                    x.UsClave,
                    x.PerId,
                    x.AreaTrabajo,
                    x.RolId,
                    x.Email,
                    x.EmailConfirmed,
                    x.Estado,
                    x.UserName,
                    x.LockoutEnabled,
                    x.UsuarioModificacion,
                    x.UsuarioCreacion,
                    x.FechaModificacion,
                    x.FechaCreacion
                }).Where(x => x.Id == id).FirstOrDefault();

                IntegraAspNetUsersBO integraBO = new IntegraAspNetUsersBO();
              
                integraBO.PasswordHash = listaASP.PasswordHash;
                integraBO.UsClave = listaASP.UsClave;
                integraBO.PerId = listaASP.PerId;
                integraBO.AreaTrabajo = listaASP.AreaTrabajo;
                integraBO.RolId = listaASP.RolId;
                integraBO.Email = listaASP.Email;
                integraBO.EmailConfirmed = listaASP.EmailConfirmed;
                integraBO.Estado = listaASP.Estado;
                integraBO.UserName = listaASP.UserName;
                integraBO.UsuarioModificacion = listaASP.UsuarioModificacion;
                integraBO.UsuarioCreacion = listaASP.UsuarioCreacion;
                integraBO.FechaCreacion = listaASP.FechaCreacion;
                integraBO.FechaModificacion = listaASP.FechaModificacion;
                integraBO.LockoutEnabled = listaASP.LockoutEnabled;

                return integraBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene nombre de personal autocomplete
        /// </summary>
        /// <param name="valor"></param>
        /// <returns> Lista de ObjetosDTO: List<PersonalFiltroDTO> </returns>
        public List<PersonalFiltroDTO> ObtenerPersonalAutocomplete(string valor)
        {
            try
            {
                List<PersonalFiltroDTO> personalAutocompleteFiltro = new List<PersonalFiltroDTO>();
                string query = string.Empty;
                query = "select Id,Nombre from gp.V_PersonaFiltro WHERE Nombre LIKE CONCAT('%',@valor,'%')  ORDER By Nombre ASC";
                var personalDB = _dapper.QueryDapper(query, new { valor });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    personalAutocompleteFiltro = JsonConvert.DeserializeObject<List<PersonalFiltroDTO>>(personalDB);
                }
                return personalAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string Encriptar(string us_clave)
        {
            return Encriptar(us_clave, "pass75dc@avz10", "s@lAvz", "MD5", 1, "@1B2c3D4e5F6g7H8", 128);
        }

        public string Encriptar(string textoQueEncriptaremos, string passBase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(textoQueEncriptaremos);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passBase,
              saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged()
            {
                Mode = CipherMode.CBC
            };
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes,
              initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor,
             CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }



        public List<ModuloCreacionDTO> ObtenerDatosParaModulo(string Usuario)
        {
            try
            {
                List<ModuloCreacionDTO> Modulos = new List<ModuloCreacionDTO>();
                var _query = "select IdModulo,NombreModulo,IdGrupo,NombreGrupo,URL,Etiqueta, Icono from gp.V_ObtenerDataModuloDinamico where NombreUsuario=@Usuario and Estado=1 order by IdGrupo,Etiqueta,IdModulo";
                var AsignarModuloDB = _dapper.QueryDapper(_query, new { Usuario });
                return  JsonConvert.DeserializeObject<List<ModuloCreacionDTO>>(AsignarModuloDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Validar Usuario de módulo
        /// </summary>
        /// <param name="usuario">Usuario Personal</param>
        /// <param name="idModulo">Id Modulo</param>
        /// <returns> Retorna Validación de Confirmación: ResultadoFinaltextoDTO</returns>        
        public ResultadoFinaltextoDTO ValidarUsuarioModulo(string usuario,int idModulo)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("conf.SP_ValidarUsuarioModulo", new { usuario, idModulo });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinaltextoDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Carlos Crispin
        ///Fecha: 11/11/2021
        /// <summary>
        /// Validar Re-Login
        /// </summary>
        /// <param name="usuario">Usuario Personal</param>
        /// <returns> Retorna Validación de Confirmación: ResultadoFinaltextoDTO</returns>        
        public ResultadoFinaltextoDTO ValidarReLogin(string usuario)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("conf.SP_ValidarReLogin", new { usuario });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinaltextoDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Carlos Crispin
        ///Fecha: 11/11/2021
        /// <summary>
        /// Actualiza Re-Login
        /// </summary>
        /// <param name="usuario">Usuario Personal</param>
        /// <returns> Retorna Validación de Confirmación: ResultadoFinaltextoDTO</returns>        
        public ResultadoFinaltextoDTO ActualizarReLogin(string usuario)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("conf.SP_ActualizarReLogin", new { usuario });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinaltextoDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Inserta Usuario a lista de Accesos denegados por módulo
        /// </summary>
        /// <param name="usuario">Usuario Personal</param>
        /// <param name="idModulo">Id del Modulo</param>
        /// <returns> Retorna Validación de Confirmación </returns>
        /// <returns> objetoDTO: ResultadoFinalDTO </returns>
        public ResultadoFinalDTO InsertarAccesoDenegado(string usuario, int idModulo)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("conf.SP_InsertarAccesoDenegado", new { usuario, idModulo });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene lista de Información de Usuarios
        /// </summary>
        /// <param name="prueba"></param>
        /// <returns> Obtiene lista de información de Usuarios </returns>
        /// <returns> Lista de objetos DTO: List<UsuarioContraseñaDTO> </returns>
        public List<UsuarioContraseñaDTO> ListaUsuariosActualizar(int prueba)
        {
            try
            {
                List<UsuarioContraseñaDTO> Modulos = new List<UsuarioContraseñaDTO>();
                var query = "select PerId,Usuario,NuevaContrasena,RolId,IdIntegraAspNetUsers,Email from conf.V_ObtenerListaUsuarioCambiar where Estado=1 ";
                var asignarModuloDB = _dapper.QueryDapper(query, new { prueba });
                return JsonConvert.DeserializeObject<List<UsuarioContraseñaDTO>>(asignarModuloDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id y Nombres de Usuario dado su NombreUsuario 
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public DatoPersonalDTO ObtenerIdentidadUsusario(string Usuario)
        {
            try
            {
                List<DatoPersonalDTO> Usuarios = new List<DatoPersonalDTO>();
                var _query = string.Empty;
                _query = "EXEC [conf].[SP_ObtenerIdNombresPersonalPorUsername] @Usuario";
                var UsuariosDB = _dapper.QueryDapper(_query, new { Usuario});
                if (!string.IsNullOrEmpty(UsuariosDB) && !UsuariosDB.Contains("[]"))
                {
                    Usuarios = JsonConvert.DeserializeObject<List<DatoPersonalDTO>>(UsuariosDB);
                    if (Usuarios.Count > 1) throw new Exception("Error: Existe mas de un usuario que coincide con el parametro dado");
                    else return Usuarios[0];
                }
                else
                {
                    throw new Exception("Error: Ningun usuario coincide con el parametro dado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DatoPersonalDTO ObtenerIdentidadUsusarioDNI(string Usuario, string DNI)
        {
            try
            {
                List<DatoPersonalDTO> Usuarios = new List<DatoPersonalDTO>();
                var _query = string.Empty;
                _query = "EXEC [conf].[SP_ObtenerIdNombresPersonalPorUsernameDNI] @Usuario, @DNI";
                var UsuariosDB = _dapper.QueryDapper(_query, new { Usuario, DNI });
                if (!string.IsNullOrEmpty(UsuariosDB) && !UsuariosDB.Contains("[]"))
                {
                    Usuarios = JsonConvert.DeserializeObject<List<DatoPersonalDTO>>(UsuariosDB);
                    if (Usuarios.Count > 1) throw new Exception("Error: Existe mas de un usuario que coincide con el parametro dado");
                    else return Usuarios[0];
                }
                else
                {
                    throw new Exception("Error: Ningun usuario coincide con el parametro dado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        ///Obtiene Confirmación de Personal a Módulo
        /// </summary>
        /// <param name="url"></param>
        /// <param name="idPersonal"></param>
        /// <returns> Confirmación de Acceso a móduos </returns>
        /// <returns> bool </returns>
        public bool TieneAccesoModulo(string url, int idPersonal)
        {
            try
            {
                List<AccesoPersonal> accesosResultado = new List<AccesoPersonal>();

                var query = @"
                            SELECT MS.Url AS Url, 
                                   U.IdPersonal AS IdPersonal
                            FROM conf.T_ModuloSistemaAcceso AS MSA
                                 INNER JOIN conf.T_ModuloSistema AS MS ON MSA.IdModuloSistema = MS.Id
                                 INNER JOIN conf.T_Usuario AS U ON U.Id = MSA.IdUsuario
                            WHERE Url = @url
                                  AND IdPersonal = @idPersonal
                                  AND MSA.Estado = 1
                                  AND MS.Estado = 1
                                  AND U.Estado = 1
                            ;";

                var accesos = _dapper.QueryDapper(query, new { url, idPersonal });
                if (!string.IsNullOrEmpty(accesos) && !accesos.Contains("[]"))
                {
                    accesosResultado = JsonConvert.DeserializeObject<List<AccesoPersonal>>(accesos);
                }
                if (accesosResultado.Count() >= 1)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene Nombres y Apellidos Completos de Usuario dado su NombreUsuario 
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public PersonalFiltroDTO ObtenerNombresApellidosUsuario(string Usuario)
        {
            try
            {
                List<PersonalFiltroDTO> personal = new List<PersonalFiltroDTO>();
                string _query = string.Empty;
                _query = "Select Id, Nombre from conf.V_ObtenerNombresApellidosPorUsername WHERE Usuario = @Usuario and Estado = 1";
                var PersonalDB = _dapper.QueryDapper(_query, new { Usuario });
                if (!string.IsNullOrEmpty(PersonalDB) && !PersonalDB.Contains("[]"))
                {
                    personal = JsonConvert.DeserializeObject<List<PersonalFiltroDTO>>(PersonalDB);
                    if (personal.Count > 1) throw new Exception("Error: Existe mas de un usuario que coincide con el parametro dado");
                    else return personal[0];
                }
                else
                {
                    throw new Exception("Error: Ningun usuario coincide con el parametro dado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Indica si existe un usuario por userName
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
        public bool ExistePorNombreUsuario(string nombreUsuario) {
            try
            {
                return this.Exist(x => x.UserName == nombreUsuario);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        ///Repositorio: PersonalRepositorio
        ///Autor: Jose V.
        ///Fecha: 28/04/2021
        /// <summary>
        /// Obtener email por nombre usuario
        /// </summary>
        /// <param name="nombreUsuario"> Nombre Usuario </param>
        /// <returns> Email Usuario</returns>
        public string ObtenerEmailPorNombreUsuario(string nombreUsuario)
        {
            try
            {
                return this.FirstBy(x => x.UserName == nombreUsuario).Email;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        ///Registr IP y Cookies de Usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="cookieUsuario"></param>
        /// <param name="iPUsuario"></param>
        /// <returns> Confirmación de Inserción </returns>
        /// <returns> Objeto DTO: ResultadoFinalDTO </returns>
        public ResultadoFinalDTO InsertarIpCookieUsuario(string usuario, string cookieUsuario, string iPUsuario)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("conf.SP_InsertarIpCookieUsuario", new { usuario, cookieUsuario, iPUsuario });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtener información por usuario para visualización de módulos agrupados
        /// </summary>
        /// <param name="usuario">Usuario Personal</param>
        /// <returns>Lista de registros por Usuario</returns>
        /// <returns> lista de objetos DTO: List<ModuloCreacionAgrupadoDTO> </returns>
        public List<ModuloCreacionAgrupadoDTO> ObtenerDatosParaModuloAgrupado(string usuario)
        {
            try
            {
                List<ModuloCreacionAgrupadoDTO> modulos = new List<ModuloCreacionAgrupadoDTO>();
                var query = "select IdModulo,NombreModulo,IdGrupo,NombreGrupo,URL,IdModuloSistemaTipo, NombreModuloSistemaTipo, Etiqueta, Icono from gp.V_ObtenerDataModuloDinamicoAgrupado where NombreUsuario=@usuario and Estado=1 order by IdGrupo, IdModuloSistemaTipo, OrdenMenuPrincipal, NombreModulo, Etiqueta,IdModulo";
                var asignarModuloDB = _dapper.QueryDapper(query, new { usuario });
                return JsonConvert.DeserializeObject<List<ModuloCreacionAgrupadoDTO>>(asignarModuloDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
