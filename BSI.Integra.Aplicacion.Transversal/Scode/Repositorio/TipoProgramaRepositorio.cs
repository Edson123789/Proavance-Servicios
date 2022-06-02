using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoProgramaRepositorio : BaseRepository<TTipoPrograma, TipoProgramaBO>
    {
        #region Metodos Base
        public TipoProgramaRepositorio() : base()
        {
        }
        public TipoProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoProgramaBO> GetBy(Expression<Func<TTipoPrograma, bool>> filter)
        {
            IEnumerable<TTipoPrograma> listado = base.GetBy(filter);
            List<TipoProgramaBO> listadoBO = new List<TipoProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoProgramaBO objetoBO = Mapper.Map<TTipoPrograma, TipoProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoProgramaBO FirstById(int id)
        {
            try
            {
                TTipoPrograma entidad = base.FirstById(id);
                TipoProgramaBO objetoBO = new TipoProgramaBO();
                Mapper.Map<TTipoPrograma, TipoProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoProgramaBO FirstBy(Expression<Func<TTipoPrograma, bool>> filter)
        {
            try
            {
                TTipoPrograma entidad = base.FirstBy(filter);
                TipoProgramaBO objetoBO = Mapper.Map<TTipoPrograma, TipoProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoProgramaBO> listadoBO)
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

        public bool Update(TipoProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoProgramaBO> listadoBO)
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
        private void AsignacionId(TTipoPrograma entidad, TipoProgramaBO objetoBO)
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

        private TTipoPrograma MapeoEntidad(TipoProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoPrograma entidad = new TTipoPrograma();
                entidad = Mapper.Map<TipoProgramaBO, TTipoPrograma>(objetoBO,
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
        /// Obtiene Lista de Tipo de Datos con estado activo
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerFiltro()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TipoProgramaDTO> getIdNombreTipoPrograma(int idPgeneral)
        {
            try
            {
                List<TipoProgramaDTO> programasGenerales = new List<TipoProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT TP.Id, TP.Nombre FROM pla.T_TipoPrograma TP join pla.T_PGeneral PG on PG.IdTipoPrograma = TP.Id where PG.Id = @idPgeneral";
                var pgeneralDB = _dapper.QueryDapper(_query, new { Id = idPgeneral });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<TipoProgramaDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
