using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: EmpresaRepositorio
    /// Autor: Richard Zenteno - Edgar Serruto.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_Empresa
    /// </summary>
    public class EmpresaRepositorio : BaseRepository<TEmpresa, EmpresaBO>
    {
        #region Metodos Base
        public EmpresaRepositorio() : base()
        {
        }
        public EmpresaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EmpresaBO> GetBy(Expression<Func<TEmpresa, bool>> filter)
        {
            IEnumerable<TEmpresa> listado = base.GetBy(filter);
            List<EmpresaBO> listadoBO = new List<EmpresaBO>();
            foreach (var itemEntidad in listado)
            {
                EmpresaBO objetoBO = Mapper.Map<TEmpresa, EmpresaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EmpresaBO FirstById(int id)
        {
            try
            {
                TEmpresa entidad = base.FirstById(id);
                EmpresaBO objetoBO = new EmpresaBO();
                Mapper.Map<TEmpresa, EmpresaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EmpresaBO FirstBy(Expression<Func<TEmpresa, bool>> filter)
        {
            try
            {
                TEmpresa entidad = base.FirstBy(filter);
                EmpresaBO objetoBO = Mapper.Map<TEmpresa, EmpresaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EmpresaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEmpresa entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EmpresaBO> listadoBO)
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

        public bool Update(EmpresaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEmpresa entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EmpresaBO> listadoBO)
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
        private void AsignacionId(TEmpresa entidad, EmpresaBO objetoBO)
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

        private TEmpresa MapeoEntidad(EmpresaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEmpresa entidad = new TEmpresa();
                entidad = Mapper.Map<EmpresaBO, TEmpresa>(objetoBO,
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
        /// Obtiene una empresa para usado para filtro por id 
        /// </summary>
        public EmpresaFiltroDTO ObtenerFiltroPorId(int id)
        {
            try
            {
                EmpresaFiltroDTO empresa = new EmpresaFiltroDTO();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre FROM pla.V_TEmpresa_ObtenerIdNombre WHERE Id = @id AND Estado = 1";
                var cargoDB = _dapper.FirstOrDefault(_query, new { id });
                empresa = JsonConvert.DeserializeObject<EmpresaFiltroDTO>(cargoDB);
                return empresa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene una lista de empresas competidoras
        /// IdTipoEmpresa = 1: COMPETIDOR";
        /// IdTipoEmpresa = 0: NO_COMPETIDOR";
        /// </summary>
        public List<EmpresaFiltroDTO> ObtenerTodoCompetidores()
        {
            try
            {
                List<EmpresaFiltroDTO> empresa = new List<EmpresaFiltroDTO>();
                string _query = string.Empty;
                _query = "SELECT Id,Nombre FROM pla.V_TEmpresa_ObtenerIdNombre WHERE Estado = 1 AND IdTipoEmpresa = 1";
                var empresasCompetidorasDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(empresasCompetidorasDB) && !empresasCompetidorasDB.Contains("[]"))
                {
                    empresa = JsonConvert.DeserializeObject<List<EmpresaFiltroDTO>>(empresasCompetidorasDB);
                }
                return empresa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Obtiene Tamano de Empresa
        /// </summary>
        public EmpresaTamanioDTO ObtenerTamanioEmpresa(int idEmpresa)
        {
            try
            {
                string _queryEmpres = "Select Id,IdTamanio from pla.V_TEmpresa_IdTAmanio Where Id=@IdEmpresa and Estado=1";
                var queryEmpres = _dapper.FirstOrDefault(_queryEmpres, new { IdEmpresa = idEmpresa });
                return JsonConvert.DeserializeObject<EmpresaTamanioDTO>(queryEmpres);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public EmpresaFiltroDTO ObtenerFiltro(int id) {
            try
            {
                EmpresaFiltroDTO empresa = new EmpresaFiltroDTO();
                string _queryCompetidor = "SELECT Nombre FROM pla.T_Empresa WHERE  Id = @id";
                var empresaDB = _dapper.FirstOrDefault(_queryCompetidor, new { id });
                empresa = JsonConvert.DeserializeObject<EmpresaFiltroDTO>(empresaDB);
                return empresa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: EmpresaRepositorio
        /// Autor: Edgar S.
        /// Fecha: 22/03/2021
        /// <summary>
        /// Obtiene las empresas que contengan el valor nombre.
        /// </summary>
        /// <param name="nombre"> Nombre de Empresa</param>
        /// <returns> List<EmpresaFiltroDTO> </returns>
        public List<EmpresaFiltroDTO> CargarEmpresasAutoComplete(string nombre)
        {
            string query = "SELECT Id, Nombre from pla.V_TEmpresa_ObtenerIdNombre WHERE Nombre LIKE CONCAT('%',@nombre,'%') AND Estado = 1 ORDER BY Nombre ASC";
            string queryRespuesta = _dapper.QueryDapper(query, new { nombre });
            return JsonConvert.DeserializeObject<List<EmpresaFiltroDTO>>(queryRespuesta);
        }

        public ICollection<FiltroDTO> ObtenerTodoFiltroAutoComplete(string valor)
        {
            try
            {
                ICollection<FiltroDTO> rpta = GetBy(w => w.Nombre.Contains(valor), w => new FiltroDTO { Id = w.Id, Nombre = w.Nombre });
                
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: EmpresaRepositorio
        /// Autor: Luis Huallpa - Edgar Serruto.
        /// Fecha: 22/03/2021
        /// <summary>
        /// Obtiene una toda lista de empresas 
        /// </summary>
        /// <return> List<EmpresaFiltroDTO> </return>
        public List<EmpresaFiltroDTO> ObtenerTodoEmpresasFiltro()
        {
            try
            {
                List<EmpresaFiltroDTO> empresa = new List<EmpresaFiltroDTO>();
                string query = string.Empty;
                query = "SELECT Id,Nombre FROM pla.V_TEmpresa_ObtenerIdNombre WHERE Estado = 1";
                var empresasCompetidorasDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(empresasCompetidorasDB) && !empresasCompetidorasDB.Contains("[]"))
                {
                    empresa = JsonConvert.DeserializeObject<List<EmpresaFiltroDTO>>(empresasCompetidorasDB);
                }
                return empresa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
