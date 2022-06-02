using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    /// Repositorio: EntidadFinancieraRepositorio
    /// Autor: Richard Zenteno - Britsel Calluchi - Edgar Serruto.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_EntidadFinanciera
    /// </summary>
    public class EntidadFinancieraRepositorio : BaseRepository<TEntidadFinanciera, EntidadFinancieraBO>
    {
        #region Metodos Base
        public EntidadFinancieraRepositorio() : base()
        {
        }
        public EntidadFinancieraRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EntidadFinancieraBO> GetBy(Expression<Func<TEntidadFinanciera, bool>> filter)
        {
            IEnumerable<TEntidadFinanciera> listado = base.GetBy(filter);
            List<EntidadFinancieraBO> listadoBO = new List<EntidadFinancieraBO>();
            foreach (var itemEntidad in listado)
            {
                EntidadFinancieraBO objetoBO = Mapper.Map<TEntidadFinanciera, EntidadFinancieraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EntidadFinancieraBO FirstById(int id)
        {
            try
            {
                TEntidadFinanciera entidad = base.FirstById(id);
                EntidadFinancieraBO objetoBO = new EntidadFinancieraBO();
                Mapper.Map<TEntidadFinanciera, EntidadFinancieraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EntidadFinancieraBO FirstBy(Expression<Func<TEntidadFinanciera, bool>> filter)
        {
            try
            {
                TEntidadFinanciera entidad = base.FirstBy(filter);
                EntidadFinancieraBO objetoBO = Mapper.Map<TEntidadFinanciera, EntidadFinancieraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EntidadFinancieraBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEntidadFinanciera entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EntidadFinancieraBO> listadoBO)
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

        public bool Update(EntidadFinancieraBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEntidadFinanciera entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EntidadFinancieraBO> listadoBO)
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
        private void AsignacionId(TEntidadFinanciera entidad, EntidadFinancieraBO objetoBO)
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

        private TEntidadFinanciera MapeoEntidad(EntidadFinancieraBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEntidadFinanciera entidad = new TEntidadFinanciera();
                entidad = Mapper.Map<EntidadFinancieraBO, TEntidadFinanciera>(objetoBO,
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
        /// Repositorio: EntidadFinancieraRepositorio
        /// Autor: 
        /// Fecha: 22/03/2021
        /// <summary>
        /// Obtiene Entidades financieras registradas
        /// </summary>
        /// <returns> List<EntidadFinancieraDTO> </returns>
        public List<EntidadFinancieraDTO> ObtenerEntidadesFinancieras()
        {
            try
            {
                List<EntidadFinancieraDTO> entidadFinanciera = new List<EntidadFinancieraDTO>();
                var query = "SELECT Id,Nombre,Descripcion,IdMoneda,Moneda,UsuarioModificacion,FechaModificacion,Estado FROM FIN.V_EntidadFinanciera where Estado = 1 order by Nombre";
                var entidadFinancieraDB = _dapper.QueryDapper(query, null);
                if (!entidadFinancieraDB.Contains("[]") && !string.IsNullOrEmpty(entidadFinancieraDB))
                {
                    entidadFinanciera = JsonConvert.DeserializeObject<List<EntidadFinancieraDTO>>(entidadFinancieraDB);
                }
                return entidadFinanciera;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<FiltroDTO> ObtenerEntidadFinanciera()
        {
            try
            {
                List<FiltroDTO> listaEntidad = this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre.ToUpper() }).ToList();
                return listaEntidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las Entidades Financieras de las que se tiene un prestamo (utilizado para combobox)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaEntidadFinancieraPrestamo()
        {
            try
            {
                List<FiltroDTO> EntidadesFinancieras = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM fin.V_ObtenerEntidadFinancieraConPrestamo";
                var EntidadesFinancierasDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(EntidadesFinancierasDB) && !EntidadesFinancierasDB.Contains("[]"))
                {
                    EntidadesFinancieras = JsonConvert.DeserializeObject<List<FiltroDTO>>(EntidadesFinancierasDB);
                }
                return EntidadesFinancieras;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
