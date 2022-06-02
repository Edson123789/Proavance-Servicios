using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Repositorio
{
    public class UrlBlockStorageRepositorio : BaseRepository<TUrlBlockStorage, UrlBlockStorageBO>
    {
        #region Metodos Base
        public UrlBlockStorageRepositorio() : base()
        {
        }
        public UrlBlockStorageRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<UrlBlockStorageBO> GetBy(Expression<Func<TUrlBlockStorage, bool>> filter)
        {
            IEnumerable<TUrlBlockStorage> listado = base.GetBy(filter);
            List<UrlBlockStorageBO> listadoBO = new List<UrlBlockStorageBO>();
            foreach (var itemEntidad in listado)
            {
                UrlBlockStorageBO objetoBO = Mapper.Map<TUrlBlockStorage, UrlBlockStorageBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public UrlBlockStorageBO FirstById(int id)
        {
            try
            {
                TUrlBlockStorage entidad = base.FirstById(id);
                UrlBlockStorageBO objetoBO = new UrlBlockStorageBO();
                Mapper.Map<TUrlBlockStorage, UrlBlockStorageBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public UrlBlockStorageBO FirstBy(Expression<Func<TUrlBlockStorage, bool>> filter)
        {
            try
            {
                TUrlBlockStorage entidad = base.FirstBy(filter);
                UrlBlockStorageBO objetoBO = Mapper.Map<TUrlBlockStorage, UrlBlockStorageBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(UrlBlockStorageBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TUrlBlockStorage entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<UrlBlockStorageBO> listadoBO)
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

        public bool Update(UrlBlockStorageBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TUrlBlockStorage entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<UrlBlockStorageBO> listadoBO)
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
        private void AsignacionId(TUrlBlockStorage entidad, UrlBlockStorageBO objetoBO)
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

        private TUrlBlockStorage MapeoEntidad(UrlBlockStorageBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TUrlBlockStorage entidad = new TUrlBlockStorage();
                entidad = Mapper.Map<UrlBlockStorageBO, TUrlBlockStorage>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<UrlBlockStorageBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TUrlBlockStorage, bool>>> filters, Expression<Func<TUrlBlockStorage, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TUrlBlockStorage> listado = base.GetFiltered(filters, orderBy, ascending);
            List<UrlBlockStorageBO> listadoBO = new List<UrlBlockStorageBO>();

            foreach (var itemEntidad in listado)
            {
                UrlBlockStorageBO objetoBO = Mapper.Map<TUrlBlockStorage, UrlBlockStorageBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene la informacion completa de los contenedores y subcontenedores segun el idUrlSubcontenedor
        /// </summary>
        /// <param name="idUrlSubcontenedor">Id del subcontenedor, PK de mkt.T_UrlSubContenedor</param>
        /// <returns>Informacion compelta de los contenedores y subcontenedores</returns>
        public ContenedorArchivoCompletoDTO ObtenerInformacionPorIdUrlSubcontenedor(int idUrlSubcontenedor)
        {
            try
            {
                var queryDapper = $@"SELECT TOP (1) IdContenedor,
                                        Contenedor,
                                        IdProveedorNube,
                                        Subdominio,
                                        AplicaSubcontenedores,
                                        AplicaSubidaMultiple,
                                        AplicaValidacion,
                                        IdSubcontenedor,
                                        Subcontenedor
                                    FROM [mkt].[V_RegistroArchivosContenedoresSubcontenedores]
                                    WHERE IdSubcontenedor = {idUrlSubcontenedor}";
                
                var dataDB = _dapper.FirstOrDefault(queryDapper, null);
                var data = !string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]") ? JsonConvert.DeserializeObject<ContenedorArchivoCompletoDTO>(dataDB) : null;

                return data;
            } catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
