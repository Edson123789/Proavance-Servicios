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
    public class CausaRepositorio : BaseRepository<TCausa, CausaBO>
    {
        #region Metodos Base
        public CausaRepositorio() : base()
        {
        }
        public CausaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CausaBO> GetBy(Expression<Func<TCausa, bool>> filter)
        {
            IEnumerable<TCausa> listado = base.GetBy(filter);
            List<CausaBO> listadoBO = new List<CausaBO>();
            foreach (var itemEntidad in listado)
            {
                CausaBO objetoBO = Mapper.Map<TCausa, CausaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CausaBO FirstById(int id)
        {
            try
            {
                TCausa entidad = base.FirstById(id);
                CausaBO objetoBO = new CausaBO();
                Mapper.Map<TCausa, CausaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CausaBO FirstBy(Expression<Func<TCausa, bool>> filter)
        {
            try
            {
                TCausa entidad = base.FirstBy(filter);
                CausaBO objetoBO = Mapper.Map<TCausa, CausaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CausaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCausa entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CausaBO> listadoBO)
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

        public bool Update(CausaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCausa entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CausaBO> listadoBO)
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
        private void AsignacionId(TCausa entidad, CausaBO objetoBO)
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

        private TCausa MapeoEntidad(CausaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCausa entidad = new TCausa();
                entidad = Mapper.Map<CausaBO, TCausa>(objetoBO,
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
        /// Obtiene todas las causas asociadas a un problema
        /// </summary>
        /// <param name="IdProblema"></param>
        /// <returns></returns>
        public List<CausaDTO> ObtenerCausasPorIdProblema(int IdProblema)
        {
            try
            {
                List<CausaDTO> Causas = new List<CausaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdProblema, Nombre, Descripcion FROM mkt.T_Causa WHERE IdProblema=" + IdProblema+ " AND Estado=1";
                var CausasDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(CausasDB) && !CausasDB.Contains("[]"))
                {
                    Causas = JsonConvert.DeserializeObject<List<CausaDTO>>(CausasDB);
                }
                return Causas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
