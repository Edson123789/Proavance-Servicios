using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ModeloPredictivoTipoDatoRepositorio : BaseRepository<TModeloPredictivoTipoDato, ModeloPredictivoTipoDatoBO>
    {
        #region Metodos Base
        public ModeloPredictivoTipoDatoRepositorio() : base()
        {
        }
        public ModeloPredictivoTipoDatoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloPredictivoTipoDatoBO> GetBy(Expression<Func<TModeloPredictivoTipoDato, bool>> filter)
        {
            IEnumerable<TModeloPredictivoTipoDato> listado = base.GetBy(filter);
            List<ModeloPredictivoTipoDatoBO> listadoBO = new List<ModeloPredictivoTipoDatoBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloPredictivoTipoDatoBO objetoBO = Mapper.Map<TModeloPredictivoTipoDato, ModeloPredictivoTipoDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloPredictivoTipoDatoBO FirstById(int id)
        {
            try
            {
                TModeloPredictivoTipoDato entidad = base.FirstById(id);
                ModeloPredictivoTipoDatoBO objetoBO = new ModeloPredictivoTipoDatoBO();
                Mapper.Map<TModeloPredictivoTipoDato, ModeloPredictivoTipoDatoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloPredictivoTipoDatoBO FirstBy(Expression<Func<TModeloPredictivoTipoDato, bool>> filter)
        {
            try
            {
                TModeloPredictivoTipoDato entidad = base.FirstBy(filter);
                ModeloPredictivoTipoDatoBO objetoBO = Mapper.Map<TModeloPredictivoTipoDato, ModeloPredictivoTipoDatoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloPredictivoTipoDatoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloPredictivoTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloPredictivoTipoDatoBO> listadoBO)
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

        public bool Update(ModeloPredictivoTipoDatoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloPredictivoTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloPredictivoTipoDatoBO> listadoBO)
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
        private void AsignacionId(TModeloPredictivoTipoDato entidad, ModeloPredictivoTipoDatoBO objetoBO)
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

        private TModeloPredictivoTipoDato MapeoEntidad(ModeloPredictivoTipoDatoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoTipoDato entidad = new TModeloPredictivoTipoDato();
                entidad = Mapper.Map<ModeloPredictivoTipoDatoBO, TModeloPredictivoTipoDato>(objetoBO,
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
        /// Obtiene los Tipo de Datos (activo) para el modelo Predictivo por programa 
        /// registradas en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ModeloPredictivoTipoDatoDTO> ObtenerTipoDatoPorPrograma(int idPGeneral)
        {
            try
            {
                List<ModeloPredictivoTipoDatoDTO> resultadoDTO = new List<ModeloPredictivoTipoDatoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,IdTipoDato FROM mkt.V_TModeloPredictivoTipoDato WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ModeloPredictivoTipoDatoDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Modelos tipo Datos a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ModeloPredictivoTipoDatoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPgeneral == idPGeneral && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.Id));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
