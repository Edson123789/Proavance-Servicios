using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class OtroMovimientoCajaRepositorio : BaseRepository<TOtroMovimientoCaja, OtroMovimientoCajaBO>
    {
        #region Metodos Base
        public OtroMovimientoCajaRepositorio() : base()
        {
        }
        public OtroMovimientoCajaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OtroMovimientoCajaBO> GetBy(Expression<Func<TOtroMovimientoCaja, bool>> filter)
        {
            IEnumerable<TOtroMovimientoCaja> listado = base.GetBy(filter);
            List<OtroMovimientoCajaBO> listadoBO = new List<OtroMovimientoCajaBO>();
            foreach (var itemEntidad in listado)
            {
                OtroMovimientoCajaBO objetoBO = Mapper.Map<TOtroMovimientoCaja, OtroMovimientoCajaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OtroMovimientoCajaBO FirstById(int id)
        {
            try
            {
                TOtroMovimientoCaja entidad = base.FirstById(id);
                OtroMovimientoCajaBO objetoBO = new OtroMovimientoCajaBO();
                Mapper.Map<TOtroMovimientoCaja, OtroMovimientoCajaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OtroMovimientoCajaBO FirstBy(Expression<Func<TOtroMovimientoCaja, bool>> filter)
        {
            try
            {
                TOtroMovimientoCaja entidad = base.FirstBy(filter);
                OtroMovimientoCajaBO objetoBO = Mapper.Map<TOtroMovimientoCaja, OtroMovimientoCajaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OtroMovimientoCajaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOtroMovimientoCaja entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OtroMovimientoCajaBO> listadoBO)
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

        public bool Update(OtroMovimientoCajaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOtroMovimientoCaja entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OtroMovimientoCajaBO> listadoBO)
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
        private void AsignacionId(TOtroMovimientoCaja entidad, OtroMovimientoCajaBO objetoBO)
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

        private TOtroMovimientoCaja MapeoEntidad(OtroMovimientoCajaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOtroMovimientoCaja entidad = new TOtroMovimientoCaja();
                entidad = Mapper.Map<OtroMovimientoCajaBO, TOtroMovimientoCaja>(objetoBO,
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
        /// Obtiene La lista de registros con Estado=1  (usado para llenar grilla en CRUD)
        /// </summary>
        /// <returns></returns>
        public List<OtroMovimientoCajaDTO> ObtenerListaOtroMovimientoCaja()
        {
            try
            {
                List<OtroMovimientoCajaDTO> OtroMovimientoCajaFinanzas = new List<OtroMovimientoCajaDTO>();
                var _query = "SELECT * FROM fin.V_OtroMovimientoCajaObtenerDatos  where Estado=1 order by Id desc";
                var OtroMovimientoCajaFinanzasDB = _dapper.QueryDapper(_query, null);
                if (!OtroMovimientoCajaFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(OtroMovimientoCajaFinanzasDB))
                {
                    OtroMovimientoCajaFinanzas = JsonConvert.DeserializeObject<List<OtroMovimientoCajaDTO>>(OtroMovimientoCajaFinanzasDB);
                }
                return OtroMovimientoCajaFinanzas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un Registro con Estado=1  de OtroMovimientoCaja por su Id
        /// </summary>
        /// <returns></returns>
        public List<OtroMovimientoCajaDTO> ObtenerOtroMovimientoCajaPorID(int Id)
        {
            try
            {
                List<OtroMovimientoCajaDTO> OtroMovimientoCajaFinanzas = new List<OtroMovimientoCajaDTO>();
                var _query = "SELECT * FROM fin.V_OtroMovimientoCajaObtenerDatos  where Estado=1 AND Id=" +Id;
                var OtroMovimientoCajaFinanzasDB = _dapper.QueryDapper(_query, null);
                if (!OtroMovimientoCajaFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(OtroMovimientoCajaFinanzasDB))
                {
                    OtroMovimientoCajaFinanzas = JsonConvert.DeserializeObject<List<OtroMovimientoCajaDTO>>(OtroMovimientoCajaFinanzasDB);
                }
                return OtroMovimientoCajaFinanzas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
