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
    public class ModeloPredictivoTrabajoRepositorio : BaseRepository<TModeloPredictivoTrabajo, ModeloPredictivoTrabajoBO>
    {
        #region Metodos Base
        public ModeloPredictivoTrabajoRepositorio() : base()
        {
        }
        public ModeloPredictivoTrabajoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloPredictivoTrabajoBO> GetBy(Expression<Func<TModeloPredictivoTrabajo, bool>> filter)
        {
            IEnumerable<TModeloPredictivoTrabajo> listado = base.GetBy(filter);
            List<ModeloPredictivoTrabajoBO> listadoBO = new List<ModeloPredictivoTrabajoBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloPredictivoTrabajoBO objetoBO = Mapper.Map<TModeloPredictivoTrabajo, ModeloPredictivoTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloPredictivoTrabajoBO FirstById(int id)
        {
            try
            {
                TModeloPredictivoTrabajo entidad = base.FirstById(id);
                ModeloPredictivoTrabajoBO objetoBO = new ModeloPredictivoTrabajoBO();
                Mapper.Map<TModeloPredictivoTrabajo, ModeloPredictivoTrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloPredictivoTrabajoBO FirstBy(Expression<Func<TModeloPredictivoTrabajo, bool>> filter)
        {
            try
            {
                TModeloPredictivoTrabajo entidad = base.FirstBy(filter);
                ModeloPredictivoTrabajoBO objetoBO = Mapper.Map<TModeloPredictivoTrabajo, ModeloPredictivoTrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloPredictivoTrabajoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloPredictivoTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloPredictivoTrabajoBO> listadoBO)
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

        public bool Update(ModeloPredictivoTrabajoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloPredictivoTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloPredictivoTrabajoBO> listadoBO)
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
        private void AsignacionId(TModeloPredictivoTrabajo entidad, ModeloPredictivoTrabajoBO objetoBO)
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

        private TModeloPredictivoTrabajo MapeoEntidad(ModeloPredictivoTrabajoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoTrabajo entidad = new TModeloPredictivoTrabajo();
                entidad = Mapper.Map<ModeloPredictivoTrabajoBO, TModeloPredictivoTrabajo>(objetoBO,
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
        /// Obtiene los Trabajos (activo) para el modelo Predictivo por programa 
        /// registradas en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ModeloPredictivoTrabajoDTO> ObtenerTrabajoPorPrograma(int idPGeneral)
        {
            try
            {
                List<ModeloPredictivoTrabajoDTO> resultadoDTO = new List<ModeloPredictivoTrabajoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,IdAreaTrabajo,Nombre,Valor,Validar FROM mkt.V_TModeloPredictivoTrabajo WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ModeloPredictivoTrabajoDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Modelos Area de Trabajo a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ModeloPredictivoTrabajoDTO> nuevos)
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

