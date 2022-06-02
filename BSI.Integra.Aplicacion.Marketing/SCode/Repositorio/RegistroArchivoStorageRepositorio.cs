using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class RegistroArchivoStorageRepositorio : BaseRepository<TRegistroArchivoStorage, RegistroArchivoStorageBO>
    {
        #region Metodos Base
        public RegistroArchivoStorageRepositorio() : base()
        {
        }
        public RegistroArchivoStorageRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RegistroArchivoStorageBO> GetBy(Expression<Func<TRegistroArchivoStorage, bool>> filter)
        {
            IEnumerable<TRegistroArchivoStorage> registro = base.GetBy(filter);
            List<RegistroArchivoStorageBO> registroBO = new List<RegistroArchivoStorageBO>();
            foreach (var itemEntidad in registro)
            {
                RegistroArchivoStorageBO objetoBO = Mapper.Map<TRegistroArchivoStorage, RegistroArchivoStorageBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                registroBO.Add(objetoBO);
            }

            return registroBO;
        }
        public RegistroArchivoStorageBO FirstById(int id)
        {
            try
            {
                TRegistroArchivoStorage entidad = base.FirstById(id);
                RegistroArchivoStorageBO objetoBO = new RegistroArchivoStorageBO();
                Mapper.Map<TRegistroArchivoStorage, RegistroArchivoStorageBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RegistroArchivoStorageBO FirstBy(Expression<Func<TRegistroArchivoStorage, bool>> filter)
        {
            try
            {
                TRegistroArchivoStorage entidad = base.FirstBy(filter);
                RegistroArchivoStorageBO objetoBO = Mapper.Map<TRegistroArchivoStorage, RegistroArchivoStorageBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RegistroArchivoStorageBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRegistroArchivoStorage entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RegistroArchivoStorageBO> registroBO)
        {
            try
            {
                foreach (var objetoBO in registroBO)
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

        public bool Update(RegistroArchivoStorageBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRegistroArchivoStorage entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RegistroArchivoStorageBO> registroBO)
        {
            try
            {
                foreach (var objetoBO in registroBO)
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
        private void AsignacionId(TRegistroArchivoStorage entidad, RegistroArchivoStorageBO objetoBO)
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

        private TRegistroArchivoStorage MapeoEntidad(RegistroArchivoStorageBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRegistroArchivoStorage entidad = new TRegistroArchivoStorage();
                entidad = Mapper.Map<RegistroArchivoStorageBO, TRegistroArchivoStorage>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<RegistroArchivoStorageBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TRegistroArchivoStorage, bool>>> filters, Expression<Func<TRegistroArchivoStorage, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TRegistroArchivoStorage> registro = base.GetFiltered(filters, orderBy, ascending);
            List<RegistroArchivoStorageBO> registroBO = new List<RegistroArchivoStorageBO>();

            foreach (var itemEntidad in registro)
            {
                RegistroArchivoStorageBO objetoBO = Mapper.Map<TRegistroArchivoStorage, RegistroArchivoStorageBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                registroBO.Add(objetoBO);
            }
            return registroBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los contenedores acorde al permiso del usuario, mediante idPersonal, idUrlBlockStorage, nombreArchivo
        /// </summary>
        /// <param name="idPersonal">Id del personal, PK de gp.T_Personal</param>
        /// <param name="idUrlBlockStorage">Id de UrlBlockStorage, PK de ope.T_UrlBlockStorage</param>
        /// <param name="nombreArchivo">Nombre del archivo a subir</param>
        /// <returns>Contenedores acorde al permiso del usuario y parametros</returns>
        public List<RegistroArchivoStorageDTO> ObtenerTodoPorPermisos(RegistroArchivoMostrarFiltroDTO registroArchivoMostrarFiltro)
        {
            try
            {
                var spDapper = "[ope].[SP_ObtenerContenedoresPorPermisos]";
                var dataDB = _dapper.QuerySPDapper(spDapper, new { registroArchivoMostrarFiltro.IdPersonal, registroArchivoMostrarFiltro.IdUrlBlockStorage, registroArchivoMostrarFiltro.NombreArchivo });
                var data = !string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]") ? JsonConvert.DeserializeObject<List<RegistroArchivoStorageDTO>>(dataDB) : null;

                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los contenedores para combo acorde al idPersonal
        /// </summary>
        /// <param name="idPersonal">Id del personal, PK de gp.T_Personal</param>
        /// <returns>Contenedores acorde al permiso del usuario</returns>
        public List<ComboContenedorArchivoDTO> ObtenerContenedores(int idPersonal)
        {
            try
            {
                var queryDapper = $@"SELECT DISTINCT
                                       IdContenedor,
                                       Contenedor,
                                       AplicaSubcontenedores,
                                       AplicaSubidaMultiple,
                                       AplicaValidacion
                                FROM mkt.V_RegistroArchivosContenedoresSubcontenedores RACS
                                INNER JOIN mkt.V_RegistroArchivoStoragePermisoUsuario RASP
                                    ON RACS.IdContenedor = RASP.IdUrlBlockStorage
                                WHERE IdPersonal = {idPersonal}";

                var dataDB = _dapper.QueryDapper(queryDapper, null);
                var data = !string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]") ? JsonConvert.DeserializeObject<List<ComboContenedorArchivoDTO>>(dataDB) : null;

                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los subcontenedores para combo acorde al idPersonal
        /// </summary>
        /// <param name="idPersonal">Id del personal, PK de gp.T_Personal</param>
        /// <returns>Subcontenedores acorde al permiso del usuario</returns>
        public List<ComboSubcontenedorArchivoDTO> ObtenerSubcontenedores(int idPersonal)
        {
            try
            {
                var queryDapper = $@"SELECT DISTINCT
                                       IdSubcontenedor,
                                       Subcontenedor,
                                       IdContenedor
                                FROM mkt.V_RegistroArchivosContenedoresSubcontenedores RACS
                                INNER JOIN mkt.V_RegistroArchivoStoragePermisoUsuario RASP
                                    ON RACS.IdContenedor = RASP.IdUrlBlockStorage
                                WHERE IdPersonal = {idPersonal}";

                var dataDB = _dapper.QueryDapper(queryDapper, null);
                var data = !string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]") ? JsonConvert.DeserializeObject<List<ComboSubcontenedorArchivoDTO>>(dataDB) : null;

                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los tipos de subcontenedores con los Ids de sus padres jerarquicos acoder al idPersonal
        /// </summary>
        /// <param name="idPersonal">Id del personal, PK de gp.T_Personal</param>
        /// <returns>Tipos de subcontenedores con los Ids de sus padres jerarquicos acorde al permiso del usuario</returns>
        public List <ComboTipoSubcontenedorArchivoDTO> ObtenerTipoSubcontenedores(int idPersonal)
        {
            try
            {
                var queryDapper = $@"SELECT DISTINCT RATS.Id,
                                        RATS.IdContenedor,
		                                RATS.IdUrlSubContenedor,
		                                RATS.Tipo,
		                                RATS.Ruta
                                    FROM mkt.V_RegistroArchivoTipoSubContenedor AS RATS
                                    INNER JOIN mkt.V_RegistroArchivoStoragePermisoUsuario AS RASP
	                                    ON RATS.IdContenedor = RASP.IdUrlBlockStorage
                                    WHERE RASP.IdPersonal = {idPersonal}";

                var dataDB = _dapper.QueryDapper(queryDapper, null);
                var data = !string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]") ? JsonConvert.DeserializeObject<List<ComboTipoSubcontenedorArchivoDTO>>(dataDB) : null;

                return data;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }    
}
