using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class AscensoRepositorio : BaseRepository<TAscenso, AscensoBO>
    {
        #region Metodos Base
        public AscensoRepositorio() : base()
        {
        }
        public AscensoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AscensoBO> GetBy(Expression<Func<TAscenso, bool>> filter)
        {
            IEnumerable<TAscenso> listado = base.GetBy(filter);
            List<AscensoBO> listadoBO = new List<AscensoBO>();
            foreach (var itemEntidad in listado)
            {
                AscensoBO objetoBO = Mapper.Map<TAscenso, AscensoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AscensoBO FirstById(int id)
        {
            try
            {
                TAscenso entidad = base.FirstById(id);
                AscensoBO objetoBO = new AscensoBO();
                Mapper.Map<TAscenso, AscensoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AscensoBO FirstBy(Expression<Func<TAscenso, bool>> filter)
        {
            try
            {
                TAscenso entidad = base.FirstBy(filter);
                AscensoBO objetoBO = Mapper.Map<TAscenso, AscensoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AscensoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAscenso entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AscensoBO> listadoBO)
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

        public bool Update(AscensoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAscenso entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AscensoBO> listadoBO)
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
        private void AsignacionId(TAscenso entidad, AscensoBO objetoBO)
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

        private TAscenso MapeoEntidad(AscensoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAscenso entidad = new TAscenso();
                entidad = Mapper.Map<AscensoBO, TAscenso>(objetoBO,
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
        /// Obtiene los Ascensos y datos Adicionales de Empresa e Industria para ser llenados en una grilla (CRUD PROPIO)
        /// </summary>
        /// <returns></returns>
        public List<AscensoEmpresaIndustriaDTO> ObtenerTodoAscensosIndustriaEmpresa()
        {
            try
            {
                List<AscensoEmpresaIndustriaDTO> Ascensos = new List<AscensoEmpresaIndustriaDTO>();
                var _query = string.Empty;
                _query = "Select  Id,  CargoMercado,  Activo,  FechaPublicacion,  SueldoMin, IdMoneda,  IdAreaTrabajo,  IdPortalEmpleo,  IdCargo,  IdEmpresa,  IdPais,  IdRegionCiudad,  IdCiudad, UrlOferta,  NombreEmpresa, IdCodigoCiiuIndustria, IdIndustria, NombreIndustria, IdTamanioEmpresa, NombreTamanioEmpresa, FechaModificacion, UsuarioModificacion FROM pla.V_TAscensoConIndustriaYEmpresa WHERE  Estado = 1";
                var _resultado = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_resultado) && !_resultado.Contains("[]"))
                {
                    Ascensos = JsonConvert.DeserializeObject<List<AscensoEmpresaIndustriaDTO>>(_resultado);
                }
                return Ascensos;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene datos de un (1) Ascenso y datos Adicionales de Empresa e Industria para ser llenados en una grilla
        /// </summary>
        /// <returns></returns>
        public List<AscensoEmpresaIndustriaDTO> ObtenerAscensosIndustriaEmpresaPorId(int IdAscenso)
        {
            try
            {
                List<AscensoEmpresaIndustriaDTO> Ascensos = new List<AscensoEmpresaIndustriaDTO>();
                var _query = string.Empty;
                _query = "Select  Id,  CargoMercado,  Activo,  FechaPublicacion,  SueldoMin, IdMoneda,  IdAreaTrabajo,  IdPortalEmpleo,  IdCargo,  IdEmpresa,  IdPais,  IdRegionCiudad,  IdCiudad, UrlOferta,  NombreEmpresa, IdCodigoCiiuIndustria, IdIndustria, NombreIndustria, IdTamanioEmpresa, NombreTamanioEmpresa, FechaModificacion, UsuarioModificacion FROM pla.V_TAscensoConIndustriaYEmpresa WHERE  Estado = 1 AND Id="+IdAscenso;
                var _resultado = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_resultado) && !_resultado.Contains("[]"))
                {
                    Ascensos = JsonConvert.DeserializeObject<List<AscensoEmpresaIndustriaDTO>>(_resultado);
                }
                return Ascensos;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


}
