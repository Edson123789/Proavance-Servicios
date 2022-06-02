using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Periodo
    /// Autor: Gian Miranda
    /// Fecha: 23/04/2021
    /// <summary>
    /// Gestion de los grupos de periodos (mes y año) (mkt.T_Periodo)
    /// </summary>
    public class PeriodoRepositorio : BaseRepository<TPeriodo, PeriodoBO>
    {
        #region Metodos Base
        public PeriodoRepositorio() : base()
        {
        }
        public PeriodoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PeriodoBO> GetBy(Expression<Func<TPeriodo, bool>> filter)
        {
            IEnumerable<TPeriodo> listado = base.GetBy(filter);
            List<PeriodoBO> listadoBO = new List<PeriodoBO>();
            foreach (var itemEntidad in listado)
            {
                PeriodoBO objetoBO = Mapper.Map<TPeriodo, PeriodoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PeriodoBO FirstById(int id)
        {
            try
            {
                TPeriodo entidad = base.FirstById(id);
                PeriodoBO objetoBO = new PeriodoBO();
                Mapper.Map<TPeriodo, PeriodoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PeriodoBO FirstBy(Expression<Func<TPeriodo, bool>> filter)
        {
            try
            {
                TPeriodo entidad = base.FirstBy(filter);
                PeriodoBO objetoBO = Mapper.Map<TPeriodo, PeriodoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PeriodoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPeriodo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PeriodoBO> listadoBO)
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

        public bool Update(PeriodoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPeriodo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PeriodoBO> listadoBO)
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
        private void AsignacionId(TPeriodo entidad, PeriodoBO objetoBO)
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

        private TPeriodo MapeoEntidad(PeriodoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPeriodo entidad = new TPeriodo();
                entidad = Mapper.Map<PeriodoBO, TPeriodo>(objetoBO,
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
        /// Obtiene el Id y Nombre de todos los registros.
        /// </summary>
        /// <returns>Lista de objetos de clase FiltroIdNombreDTO</returns>
        public List<FiltroIdNombreDTO> ObtenerUltimoPeriodo()
        {
            try
            {
                List<FiltroIdNombreDTO> lista = new List<FiltroIdNombreDTO>();
                var periodo = GetBy(x => true).OrderByDescending(x => x.FechaInicial).Select(y => new FiltroIdNombreDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre
                }).FirstOrDefault();
                lista.Add(periodo);

                FiltroIdNombreDTO grupo = new FiltroIdNombreDTO();
                grupo.Id = -1;
                grupo.Nombre = "TODOS";
                lista.Add(grupo);

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }       
        /// <summary>
        /// Obtiene toda la lista de Periodos para llenar un combo box
        /// </summary>
        /// <returns></returns>
        public List<PeriodoFiltroDTO> ObtenerPeriodos()
        {
            try
            {
                List<PeriodoFiltroDTO> lista = new List<PeriodoFiltroDTO>();

                lista = GetBy(x => x.Estado == true, y => new PeriodoFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    FechaInicial = y.FechaInicial,
                    FechaFin =y.FechaFin,
                    FechaCreacion = y.FechaCreacion
                }).OrderByDescending(x => x.FechaCreacion).ToList();
        

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene toda la lista de Periodos para llenar un multiselect en donde el estado puede ser false o true
        /// </summary>
        /// <returns></returns>
        public List<PeriodoFiltroDTO> ObtenerPeriodosPresupuesto()
        {
            try
            {
                List<PeriodoFiltroDTO> personalPeriodos = new List<PeriodoFiltroDTO>();
                var _query = "SELECT Id, Nombre, FechaInicial FROM fin.V_PeriodoFur order by Id desc";
                var personalDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    personalPeriodos = JsonConvert.DeserializeObject<List<PeriodoFiltroDTO>>(personalDB);
                }
                return personalPeriodos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 04/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la lista de Periodos para llenar en un combo en donde el estado es true
        /// </summary>
        /// <returns>Lista de periodos FiltroDTO</returns>
        public List<FiltroDTO> ObtenerPeriodosPendiente()
        {
            try
            {
                List<FiltroDTO> personalPeriodos = new List<FiltroDTO>();
                var _query = "SELECT Id, Nombre FROM mkt.T_Periodo where Estado = 1 order by Id desc";
                var personalDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    personalPeriodos = JsonConvert.DeserializeObject<List<FiltroDTO>>(personalDB);
                }
                return personalPeriodos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene fecha inicial del periodo
        /// </summary>
        /// <returns></returns>
        public DateTime ObtenerFechaInicial(int IdPeriodo)
        {
            try
            {
                DateTime lista = new DateTime();

                lista = GetBy(x => x.Id == IdPeriodo).Select(y => y.FechaInicial).FirstOrDefault();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene fecha final del periodo
        /// </summary>
        /// <returns></returns>
        public DateTime ObtenerFechaFinal(int IdPeriodo)
        {
            try
            {
                DateTime lista = new DateTime();

                lista = GetBy(x => x.Id == IdPeriodo).Select(y => y.FechaFin).FirstOrDefault();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Penultimo periodo mas reciente.
        /// </summary>
        /// <returns></returns>
        public int ObtenerPeriodoPenultimo()
        {
            try
            {
                int lista = new int();

                lista = GetBy(x => x.Estado == true).OrderByDescending(x => x.FechaCreacion).ToList().Select(y => y.Id).FirstOrDefault();

                int penultimoperiodo = lista - 1;

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PeriodoRepositorio
        /// Autor: _ _ _ _ _ _ _ _.
        /// Fecha: 17/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene id Periodo de la fecha actual.
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        public List<FiltroDTO> ObtenerIdPeriodoFechaActual()
        {
            try
            {
                List<FiltroDTO> personalPeriodos = new List<FiltroDTO>();
                var query = "SELECT Id, Nombre FROM mkt.T_Periodo where getdate() between fechainicial and fechafin";
                var personalDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    personalPeriodos = JsonConvert.DeserializeObject<List<FiltroDTO>>(personalDB);
                }
                return personalPeriodos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
