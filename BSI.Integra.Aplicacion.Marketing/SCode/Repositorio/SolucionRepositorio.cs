using System;
using System.Collections.Generic;
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
    public class SolucionRepositorio : BaseRepository<TSolucion, SolucionBO>
    {
        #region Metodos Base
        public SolucionRepositorio() : base()
        {
        }
        public SolucionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SolucionBO> GetBy(Expression<Func<TSolucion, bool>> filter)
        {
            IEnumerable<TSolucion> listado = base.GetBy(filter);
            List<SolucionBO> listadoBO = new List<SolucionBO>();
            foreach (var itemEntidad in listado)
            {
                SolucionBO objetoBO = Mapper.Map<TSolucion, SolucionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SolucionBO FirstById(int id)
        {
            try
            {
                TSolucion entidad = base.FirstById(id);
                SolucionBO objetoBO = new SolucionBO();
                Mapper.Map<TSolucion, SolucionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SolucionBO FirstBy(Expression<Func<TSolucion, bool>> filter)
        {
            try
            {
                TSolucion entidad = base.FirstBy(filter);
                SolucionBO objetoBO = Mapper.Map<TSolucion, SolucionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SolucionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSolucion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SolucionBO> listadoBO)
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

        public bool Update(SolucionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSolucion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SolucionBO> listadoBO)
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
        private void AsignacionId(TSolucion entidad, SolucionBO objetoBO)
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

        private TSolucion MapeoEntidad(SolucionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSolucion entidad = new TSolucion();
                entidad = Mapper.Map<SolucionBO, TSolucion>(objetoBO,
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
        /// Obtiene todas las soluciones asociadas a una causa
        /// </summary>
        /// <param name="IdSolucion"></param>
        /// <returns></returns>
        public List<SolucionDTO> ObtenerSolucionesPorIdCausa(int IdSolucion)
        {
            try
            {
                List<SolucionDTO> Solucions = new List<SolucionDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdCausa, IdAgendaTipoUsuario, Nombre, Descripcion FROM mkt.T_Solucion WHERE IdCausa="+IdSolucion+" AND Estado=1";
                var SolucionsDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(SolucionsDB) && !SolucionsDB.Contains("[]"))
                {
                    Solucions = JsonConvert.DeserializeObject<List<SolucionDTO>>(SolucionsDB);
                }
                return Solucions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene todas las soluciones asociadas a una causa y por un tipo de usuario especifico
        /// </summary>
        /// <param name="IdCausa"></param>
        /// <returns></returns>
        public List<SolucionDTO> ObtenerSolucionesPorIdCausa(int IdCausa, int IdTipoUsuario)
        {
            try
            {
                List<SolucionDTO> Solucions = new List<SolucionDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdCausa, IdAgendaTipoUsuario, Nombre, Descripcion FROM mkt.T_Solucion WHERE IdCausa=" + IdCausa + " AND IdAgendaTipoUsuario="+IdTipoUsuario+" AND Estado=1";
                var SolucionsDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(SolucionsDB) && !SolucionsDB.Contains("[]"))
                {
                    Solucions = JsonConvert.DeserializeObject<List<SolucionDTO>>(SolucionsDB);
                }
                return Solucions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
