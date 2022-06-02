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
    public class ModeloPredictivoFormacionRepositorio : BaseRepository<TModeloPredictivoFormacion, ModeloPredictivoFormacionBO>
    {
        #region Metodos Base
        public ModeloPredictivoFormacionRepositorio() : base()
        {
        }
        public ModeloPredictivoFormacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloPredictivoFormacionBO> GetBy(Expression<Func<TModeloPredictivoFormacion, bool>> filter)
        {
            IEnumerable<TModeloPredictivoFormacion> listado = base.GetBy(filter);
            List<ModeloPredictivoFormacionBO> listadoBO = new List<ModeloPredictivoFormacionBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloPredictivoFormacionBO objetoBO = Mapper.Map<TModeloPredictivoFormacion, ModeloPredictivoFormacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloPredictivoFormacionBO FirstById(int id)
        {
            try
            {
                TModeloPredictivoFormacion entidad = base.FirstById(id);
                ModeloPredictivoFormacionBO objetoBO = new ModeloPredictivoFormacionBO();
                Mapper.Map<TModeloPredictivoFormacion, ModeloPredictivoFormacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloPredictivoFormacionBO FirstBy(Expression<Func<TModeloPredictivoFormacion, bool>> filter)
        {
            try
            {
                TModeloPredictivoFormacion entidad = base.FirstBy(filter);
                ModeloPredictivoFormacionBO objetoBO = Mapper.Map<TModeloPredictivoFormacion, ModeloPredictivoFormacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloPredictivoFormacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloPredictivoFormacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloPredictivoFormacionBO> listadoBO)
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

        public bool Update(ModeloPredictivoFormacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloPredictivoFormacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloPredictivoFormacionBO> listadoBO)
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
        private void AsignacionId(TModeloPredictivoFormacion entidad, ModeloPredictivoFormacionBO objetoBO)
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

        private TModeloPredictivoFormacion MapeoEntidad(ModeloPredictivoFormacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoFormacion entidad = new TModeloPredictivoFormacion();
                entidad = Mapper.Map<ModeloPredictivoFormacionBO, TModeloPredictivoFormacion>(objetoBO,
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
        /// Obtiene las Areas de Formacion (activo) para el modelo Predictivo por programa 
        /// registradas en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ModeloPredictivoFormacionDTO> ObtenerAreaFormacionPorPrograma(int idPGeneral)
        {
            try
            {
                List<ModeloPredictivoFormacionDTO> resultadoDTO = new List<ModeloPredictivoFormacionDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,IdAreaFormacion,Nombre,Valor,Validar FROM mkt.V_TModeloPredictivoFormacion WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ModeloPredictivoFormacionDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Modelos Area de Formacion a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ModeloPredictivoFormacionDTO> nuevos)
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
