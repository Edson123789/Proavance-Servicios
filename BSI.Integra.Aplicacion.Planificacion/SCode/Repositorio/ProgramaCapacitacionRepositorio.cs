using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class ProgramaCapacitacionRepositorio : BaseRepository<TProgramaCapacitacion, ProgramaCapacitacionBO>
    {
        #region Metodos Base
        public ProgramaCapacitacionRepositorio() : base()
        {
        }
        public ProgramaCapacitacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaCapacitacionBO> GetBy(Expression<Func<TProgramaCapacitacion, bool>> filter)
        {
            IEnumerable<TProgramaCapacitacion> listado = base.GetBy(filter);
            List<ProgramaCapacitacionBO> listadoBO = new List<ProgramaCapacitacionBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaCapacitacionBO objetoBO = Mapper.Map<TProgramaCapacitacion, ProgramaCapacitacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaCapacitacionBO FirstById(int id)
        {
            try
            {
                TProgramaCapacitacion entidad = base.FirstById(id);
                ProgramaCapacitacionBO objetoBO = new ProgramaCapacitacionBO();
                Mapper.Map<TProgramaCapacitacion, ProgramaCapacitacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaCapacitacionBO FirstBy(Expression<Func<TProgramaCapacitacion, bool>> filter)
        {
            try
            {
                TProgramaCapacitacion entidad = base.FirstBy(filter);
                ProgramaCapacitacionBO objetoBO = Mapper.Map<TProgramaCapacitacion, ProgramaCapacitacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaCapacitacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaCapacitacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaCapacitacionBO> listadoBO)
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

        public bool Update(ProgramaCapacitacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaCapacitacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaCapacitacionBO> listadoBO)
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
        private void AsignacionId(TProgramaCapacitacion entidad, ProgramaCapacitacionBO objetoBO)
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

        private TProgramaCapacitacion MapeoEntidad(ProgramaCapacitacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaCapacitacion entidad = new TProgramaCapacitacion();
                entidad = Mapper.Map<ProgramaCapacitacionBO, TProgramaCapacitacion>(objetoBO,
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
        /// Obtiene los datos de ProgramaCapacitaciones (activos) (para ser usados en comboboxes)
        /// </summary>
        /// <returns></returns>
        public List<ProgramaCapacitacionDTO> ObtenerTodoProgramaCapacitacion()
        {
            try
            {
                List<ProgramaCapacitacionDTO> ProgramaCapacitaciones = new List<ProgramaCapacitacionDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion, IdTipoTemaProgramaCapacitacion FROM pla.T_ProgramaCapacitacion WHERE Estado = 1";
                var ProgramaCapacitacionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ProgramaCapacitacionesDB) && !ProgramaCapacitacionesDB.Contains("[]"))
                {
                    ProgramaCapacitaciones = JsonConvert.DeserializeObject<List<ProgramaCapacitacionDTO>>(ProgramaCapacitacionesDB);
                }
                return ProgramaCapacitaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de ProgramaCapacitaciones (activos) que son TEMAS (para ser usados en comboboxes)
        /// </summary>
        /// <returns></returns>
        public List<ProgramaCapacitacionDTO> ObtenerTodoTemaProgramaCapacitacion()
        {
            try
            {
                List<ProgramaCapacitacionDTO> ProgramaCapacitaciones = new List<ProgramaCapacitacionDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion, IdTipoTemaProgramaCapacitacion FROM pla.T_ProgramaCapacitacion WHERE IdTipoTemaProgramaCapacitacion=2 AND Estado = 1";
                var ProgramaCapacitacionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ProgramaCapacitacionesDB) && !ProgramaCapacitacionesDB.Contains("[]"))
                {
                    ProgramaCapacitaciones = JsonConvert.DeserializeObject<List<ProgramaCapacitacionDTO>>(ProgramaCapacitacionesDB);
                }
                return ProgramaCapacitaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de ProgramaCapacitaciones (activos) que son EXPERIENCIA (para ser usados en comboboxes)
        /// </summary>
        /// <returns></returns>
        public List<ProgramaCapacitacionDTO> ObtenerTodoExperienciaProgramaCapacitacion()
        {
            try
            {
                List<ProgramaCapacitacionDTO> ProgramaCapacitaciones = new List<ProgramaCapacitacionDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion, IdTipoTemaProgramaCapacitacion FROM pla.T_ProgramaCapacitacion WHERE IdTipoTemaProgramaCapacitacion=1 AND Estado = 1";
                var ProgramaCapacitacionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ProgramaCapacitacionesDB) && !ProgramaCapacitacionesDB.Contains("[]"))
                {
                    ProgramaCapacitaciones = JsonConvert.DeserializeObject<List<ProgramaCapacitacionDTO>>(ProgramaCapacitacionesDB);
                }
                return ProgramaCapacitaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de ProgramaCapacitaciones (activos) para ser usados en una grilla (CRUD PROPIO), 
        /// tambien obtiene datos adicionales no propios de la tabla original como IdSubAreaCapacitacion,IdAreaCapacitacion, IdProgramaGeneral
        /// </summary>
        /// <returns></returns>
        public List<ProgramaCapacitacionConListasDTO> ObtenerTodoProgramaCapacitacionExtendido()
        {
            try
            {
                List<ProgramaCapacitacionConListasDTO> ProgramaCapacitaciones = new List<ProgramaCapacitacionConListasDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion, IdTipoTemaProgramaCapacitacion, NumeroOfertas FROM [pla].[V_ProgramaCapacitacionConNumeroOfertas] WHERE Estado = 1";
                var ProgramaCapacitacionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ProgramaCapacitacionesDB) && !ProgramaCapacitacionesDB.Contains("[]"))
                {
                    ProgramaCapacitaciones = JsonConvert.DeserializeObject<List<ProgramaCapacitacionConListasDTO>>(ProgramaCapacitacionesDB);
                }
                return ProgramaCapacitaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        /// <summary>
        /// Obtiene los datos de ProgramaCapacitaciones (activos) para llenar un registro de una grilla
        /// tambien obtiene datos adicionales no propios de la tabla original como IdSubAreaCapacitacion,IdAreaCapacitacion, IdProgramaGeneral
        /// </summary>
        /// <returns></returns>
        public List<ProgramaCapacitacionConListasDTO> ObtenerProgramaCapacitacionExtendidoPorId(int IdProgramaCapacitacion)
        {
            try
            {
                List<ProgramaCapacitacionConListasDTO> ProgramaCapacitaciones = new List<ProgramaCapacitacionConListasDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion, IdTipoTemaProgramaCapacitacion, NumeroOfertas FROM [pla].[V_ProgramaCapacitacionConNumeroOfertas] WHERE Estado = 1 and Id=" + IdProgramaCapacitacion;
                var ProgramaCapacitacionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ProgramaCapacitacionesDB) && !ProgramaCapacitacionesDB.Contains("[]"))
                {
                    ProgramaCapacitaciones = JsonConvert.DeserializeObject<List<ProgramaCapacitacionConListasDTO>>(ProgramaCapacitacionesDB);
                }
                return ProgramaCapacitaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de ProgramasGenerales, Areas y Subareas Asociadas a un ProgramaCapacitacion
        /// </summary>
        /// <returns></returns>
        public List<ProgramaCapacitacionConListasDTO> ObtenerListasPGeneralAreasSubAreasPorIdProgramaCapacitacion(int IdProgramaCapacitacion)
        {
            try
            {
                List<ProgramaCapacitacionConListasDTO> ProgramaCapacitaciones = new List<ProgramaCapacitacionConListasDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, NombreProgramaGeneral, NombreAreaCapacitacion, NombreSubAreaCapacitacion FROM [pla].[V_ProgramaCapacitacionConPGeneralAreaSubArea] WHERE Estado = 1 AND Id=" + IdProgramaCapacitacion;
                var ProgramaCapacitacionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ProgramaCapacitacionesDB) && !ProgramaCapacitacionesDB.Contains("[]"))
                {
                    ProgramaCapacitaciones = JsonConvert.DeserializeObject<List<ProgramaCapacitacionConListasDTO>>(ProgramaCapacitacionesDB);
                }
                return ProgramaCapacitaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de ProgramasGenerales, Areas y Subareas Asociadas a los ProgramaCapacitacion
        /// </summary>
        /// <returns></returns>
        public List<ProgramaCapacitacionConListasDTO> ObtenerListasPGeneralAreasSubAreasDeProgramasCapacitacion()
        {
            try
            {
                List<ProgramaCapacitacionConListasDTO> ProgramaCapacitaciones = new List<ProgramaCapacitacionConListasDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, NombreProgramaGeneral, NombreAreaCapacitacion, NombreSubAreaCapacitacion FROM [pla].[V_ProgramaCapacitacionConPGeneralAreaSubArea] WHERE Estado = 1";
                var ProgramaCapacitacionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ProgramaCapacitacionesDB) && !ProgramaCapacitacionesDB.Contains("[]"))
                {
                    ProgramaCapacitaciones = JsonConvert.DeserializeObject<List<ProgramaCapacitacionConListasDTO>>(ProgramaCapacitacionesDB);
                }
                return ProgramaCapacitaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene la lista de IDs de ProgramasGenerales Asociadas a un ProgramaCapacitacion
        /// </summary>
        /// <returns></returns>
        public List<ProgramaCapacitacionConPGeneralDTO> ObtenerListasIdPGeneralPorIdProgramaCapacitacion(int IdProgramaCapacitacion)
        {
            try
            {
                List<ProgramaCapacitacionConPGeneralDTO> ProgramaCapacitaciones = new List<ProgramaCapacitacionConPGeneralDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdProgramaGeneral FROM [pla].[V_ProgramaCapacitacionConPGeneralAreaSubArea] WHERE Estado = 1 AND Id=" + IdProgramaCapacitacion;
                var ProgramaCapacitacionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ProgramaCapacitacionesDB) && !ProgramaCapacitacionesDB.Contains("[]"))
                {
                    ProgramaCapacitaciones = JsonConvert.DeserializeObject<List<ProgramaCapacitacionConPGeneralDTO>>(ProgramaCapacitacionesDB);
                }
                return ProgramaCapacitaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
