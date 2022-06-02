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
    public class ModeloPredictivoIndustriaRepositorio : BaseRepository<TModeloPredictivoIndustria, ModeloPredictivoIndustriaBO>
    {
        #region Metodos Base
        public ModeloPredictivoIndustriaRepositorio() : base()
        {
        }
        public ModeloPredictivoIndustriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloPredictivoIndustriaBO> GetBy(Expression<Func<TModeloPredictivoIndustria, bool>> filter)
        {
            IEnumerable<TModeloPredictivoIndustria> listado = base.GetBy(filter);
            List<ModeloPredictivoIndustriaBO> listadoBO = new List<ModeloPredictivoIndustriaBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloPredictivoIndustriaBO objetoBO = Mapper.Map<TModeloPredictivoIndustria, ModeloPredictivoIndustriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloPredictivoIndustriaBO FirstById(int id)
        {
            try
            {
                TModeloPredictivoIndustria entidad = base.FirstById(id);
                ModeloPredictivoIndustriaBO objetoBO = new ModeloPredictivoIndustriaBO();
                Mapper.Map<TModeloPredictivoIndustria, ModeloPredictivoIndustriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloPredictivoIndustriaBO FirstBy(Expression<Func<TModeloPredictivoIndustria, bool>> filter)
        {
            try
            {
                TModeloPredictivoIndustria entidad = base.FirstBy(filter);
                ModeloPredictivoIndustriaBO objetoBO = Mapper.Map<TModeloPredictivoIndustria, ModeloPredictivoIndustriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloPredictivoIndustriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloPredictivoIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloPredictivoIndustriaBO> listadoBO)
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

        public bool Update(ModeloPredictivoIndustriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloPredictivoIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloPredictivoIndustriaBO> listadoBO)
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
        private void AsignacionId(TModeloPredictivoIndustria entidad, ModeloPredictivoIndustriaBO objetoBO)
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

        private TModeloPredictivoIndustria MapeoEntidad(ModeloPredictivoIndustriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoIndustria entidad = new TModeloPredictivoIndustria();
                entidad = Mapper.Map<ModeloPredictivoIndustriaBO, TModeloPredictivoIndustria>(objetoBO,
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
        /// Obtiene las Industrias (activo) para el modelo Predictivo por programa 
        /// registradas en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ModeloPredictivoIndustriaDTO> ObtenerIndustriaPorPrograma(int idPGeneral)
        {
            try
            {
                List<ModeloPredictivoIndustriaDTO> resultadoDTO = new List<ModeloPredictivoIndustriaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,IdIndustria,Nombre,Valor,Validar FROM mkt.V_TModeloPredictivoIndustria WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ModeloPredictivoIndustriaDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Modelos Industria a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ModeloPredictivoIndustriaDTO> nuevos)
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
