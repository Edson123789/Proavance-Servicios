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
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: PaisRepositorio
    /// Autor: Fischer Valdez - Johan Cayo - Luis Huallpa - Wilber Choque - Esthephany Tanco - Ansoli Espinoza - Richard Zenteno - Britsel Calluchi - Gian Miranda.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_Pais
    /// </summary>
    public class PaisRepositorio : BaseRepository<TPais, PaisBO>
    {
        #region Metodos Base
        public PaisRepositorio() : base()
        {
        }
        public PaisRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PaisBO> GetBy(Expression<Func<TPais, bool>> filter)
        {
            IEnumerable<TPais> listado = base.GetBy(filter);
            List<PaisBO> listadoBO = new List<PaisBO>();
            foreach (var itemEntidad in listado)
            {
                PaisBO objetoBO = Mapper.Map<TPais, PaisBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PaisBO FirstById(int id)
        {
            try
            {
                TPais entidad = base.FirstById(id);
                PaisBO objetoBO = new PaisBO();
                Mapper.Map<TPais, PaisBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PaisBO FirstBy(Expression<Func<TPais, bool>> filter)
        {
            try
            {
                TPais entidad = base.FirstBy(filter);
                PaisBO objetoBO = Mapper.Map<TPais, PaisBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PaisBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPais entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PaisBO> listadoBO)
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

        public bool Update(PaisBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPais entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PaisBO> listadoBO)
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
        private void AsignacionId(TPais entidad, PaisBO objetoBO)
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

        private TPais MapeoEntidad(PaisBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPais entidad = new TPais();
                entidad = Mapper.Map<PaisBO, TPais>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene todos los paises con codigo pais diferente de 0 y estado = 1
        /// </summary>
        /// <returns>Lista de objetos de clase PaisFiltroDTO</returns>
        public List<PaisFiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<PaisFiltroDTO> paises = new List<PaisFiltroDTO>();
                var query = @"SELECT Codigo,
                                        Nombre,
                                        ZonaHoraria
                                FROM com.V_TPais_ObtenerPaisComboBox
                                WHERE Estado = 1";
                var paisesDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(paisesDB) && !paisesDB.Contains("[]"))
                {
                    paises = JsonConvert.DeserializeObject<List<PaisFiltroDTO>>(paisesDB);
                }
                return paises;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los codigo pais con estado 1
        /// </summary>
        /// <returns>Lista de objetos de clase PaisCodigoDTO</returns>
        public List<PaisCodigoDTO> ObtenerTodoCodigoPais()
        {
            try
            {
                List<PaisCodigoDTO> codigosPais = new List<PaisCodigoDTO>();
                var _query = "SELECT Codigo FROM com.V_TPais_ObtenerPaisComboBox WHERE Estado = 1 GROUP BY Codigo";
                var paisesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(paisesDB) && !paisesDB.Contains("[]"))
                {
                    codigosPais = JsonConvert.DeserializeObject<List<PaisCodigoDTO>>(paisesDB);
                }
                return codigosPais;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// obtiene la lista de paises(estado=!) con sus codigos ISO para ser usado en combobox
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerPaisNombreYCodigoIso()
        {
            try
            {
                List<FiltroDTO> codigosPais = new List<FiltroDTO>();
                var _query = "SELECT Id,Nombre,Codigo FROM mkt.V_TPaisCodigoIso";
                var paisesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(paisesDB) && !paisesDB.Contains("[]"))
                {
                    codigosPais = JsonConvert.DeserializeObject<List<FiltroDTO>>(paisesDB);
                }
                return codigosPais;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PaisRepositorio
        /// Autor: ----
        /// Fecha: 11/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el nombre del pais mediante id con estado 1
        /// </summary>
        /// <param name="id">identificador de pais</param>
        /// <returns>PaisNombreDTO</returns>
        public PaisNombreDTO ObtenerNombrePaisPorId(int id)
        {
            try
            {
                string query = "select NombrePais From conf.V_TPais_Nombre where CodigoPais=@Id and Estado=1";
                var pais = _dapper.FirstOrDefault(query, new { Id = id });
                return JsonConvert.DeserializeObject<PaisNombreDTO>(pais);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PaisRepositorio
        /// Autor: Luis H, Edgar S.
        /// Fecha: 22/03/2021
        /// <summary>
        /// Obtiene los paises(activos) con campos Id, Nombre, Moneda registrado en el sistema
        /// </summary>
        /// <returns> List<PaisFiltroComboDTO> </returns>
        public List<PaisFiltroComboDTO> ObtenerPaisesCombo()
        {
            try
            {
                List<PaisFiltroComboDTO> items = new List<PaisFiltroComboDTO>();
                string queryText = string.Empty;
                queryText = "SELECT Id,Nombre,Moneda FROM pla.V_TPais_Filtro WHERE Estado=1";
                var query = _dapper.QueryDapper(queryText, null);
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PaisFiltroComboDTO>>(query);
                }
                return items;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
        /// <summary>
        /// Obtiene todas los paises para combobox
        /// </summary>
        /// <returns></returns>
        public List<PaisFiltroParaComboDTO> ObtenerPaisFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new PaisFiltroParaComboDTO { CodigoPais = x.CodigoPais, NombrePais = x.NombrePais }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PaisRepositorio
        /// Autor: Luis Huallpa - Edgar Serruto.
        /// Fecha: 05/08/2021
        /// <summary>
        /// Obtiene lista de paises para lista desplagable
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerListaPais()
        {
            try
            {
                return this.GetBy(x => x.Estado == true && x.Id > 0, x => new FiltroDTO { Id = x.CodigoPais, Nombre = x.NombrePais }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PaisRepositorio
        /// Autor: Luis Huallpa - Edgar Serruto.
        /// Fecha: 05/08/2021
        /// <summary>
        /// Obtiene lista de paises para lista desplagable
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerListaPaisTarifarios()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.CodigoPais, Nombre = x.NombrePais }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los paises para ser mostrados en una grilla (para CRUD Propio)
        /// </summary>
        /// <returns></returns>
        public List<PaisDTO> ObtenerTodoPaises()
        {
            try
            {
                List<PaisDTO> paises = new List<PaisDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, CodigoPais, CodigoISO, NombrePais, Moneda, ZonaHoraria, EstadoPublicacion FROM conf.T_Pais WHERE Estado = 1";
                var paisesDB = _dapper.QueryDapper(_query, null);
                paises = JsonConvert.DeserializeObject<List<PaisDTO>>(paisesDB);
                return paises;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los paises por IdPlantilla 
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        public List<PaisFiltroPorPlantillaDTO> ObtenerPaisesPorIdPlantilla(int idPlantilla)
        {
            try
            {
                List<PaisFiltroPorPlantillaDTO> documentos = new List<PaisFiltroPorPlantillaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,NombrePais,CodigoPais,CodigoISO FROM pla.V_ObtenerPaisesPorIdPlantilla WHERE " +
                    "Estado = 1 and IdPlantilla = @idPlantilla";
                var respuestaDapper = _dapper.QueryDapper(_query, new { idPlantilla });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    documentos = JsonConvert.DeserializeObject<List<PaisFiltroPorPlantillaDTO>>(respuestaDapper);
                }

                return documentos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los paises con campos Id, Nombre registrado en el sistema
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerPaisesResumenMontosCombo()
        {
            try
            {
                List<FiltroDTO> items = new List<FiltroDTO>();
                string _query = string.Empty;
                _query = "SELECT Id, Nombre FROM pla.T_TroncalPais WHERE Estado=1 and Id in (1,2,5)";
                var query = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<FiltroDTO>>(query);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Eliminar el pais segun el id y el usuario indicado
        /// </summary>
        /// <param name="idPais">Id del pais (PK de la tabla conf.T_Pais)</param>
        /// <returns>Objeto de clase PaisBO</returns>
        public bool EliminarPais(int idPais, string usuarioResponsable)
        {
            try
            {
                var querySp = "conf.SP_EliminarPais";
                var resultado = _dapper.QuerySPFirstOrDefault(querySp, new
                {
                    Id = idPais,
                    UsuarioModificacion = usuarioResponsable
                });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza el pais segun los parametros pasados
        /// </summary>
        /// <param name="paisActualizable">Objeto de clase PaisBO</param>
        /// <returns>Objeto de clase PaisBO</returns>
        public PaisBO ActualizarPais(PaisBO paisActualizable)
        {
            try
            {
                var paisActualizado = new PaisBO();

                var querySp = "conf.SP_ActualizarPais";
                var resultado = _dapper.QuerySPFirstOrDefault(querySp, new
                {
                    Id = paisActualizable.Id,
                    CodigoPais = paisActualizable.CodigoPais,
                    CodigoISO = paisActualizable.CodigoIso,
                    NombrePais = paisActualizable.NombrePais,
                    Moneda = paisActualizable.Moneda,
                    ZonaHoraria = paisActualizable.ZonaHoraria,
                    EstadoPublicacion = paisActualizable.EstadoPublicacion,
                    Estado = paisActualizable.Estado,
                    UsuarioCreacion = paisActualizable.UsuarioCreacion,
                    UsuarioModificacion = paisActualizable.UsuarioModificacion,
                    FechaCreacion = paisActualizable.FechaCreacion,
                    FechaModificacion = paisActualizable.FechaModificacion,
                    IdMigracion = paisActualizable.IdMigracion,
                    CodigoGoogleId = paisActualizable.CodigoGoogleId,
                    CodigoPaisMoodle = paisActualizable.CodigoPaisMoodle,
                    RutaBandera = paisActualizable.RutaBandera
                });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                    paisActualizado = JsonConvert.DeserializeObject<PaisBO>(resultado);

                return paisActualizado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene lista de paises en base a las sedes registradas en la BD
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerPaisPorSedesCombo()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM [conf].[V_TPais_ObtenerPaisPorSede] WHERE Estado = 1";
                var res = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los paises permitidos para la generacion de enlaces de publicidad de Whatsapp
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerPaisesParaEnlacesPublicidadWhatsapp()
        {
            try
            {
                List<FiltroDTO> paises = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre  FROM [mkt].[V_PaisesDisponiblesParaEnlacesWhatsapp]";
                var respuestaDapper = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    paises = JsonConvert.DeserializeObject<List<FiltroDTO>>(respuestaDapper);
                }

                return paises;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
