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
    public class ModeloPredictivoCategoriaDatoRepositorio : BaseRepository<TModeloPredictivoCategoriaDato, ModeloPredictivoCategoriaDatoBO>
    {
        #region Metodos Base
        public ModeloPredictivoCategoriaDatoRepositorio() : base()
        {
        }
        public ModeloPredictivoCategoriaDatoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloPredictivoCategoriaDatoBO> GetBy(Expression<Func<TModeloPredictivoCategoriaDato, bool>> filter)
        {
            IEnumerable<TModeloPredictivoCategoriaDato> listado = base.GetBy(filter);
            List<ModeloPredictivoCategoriaDatoBO> listadoBO = new List<ModeloPredictivoCategoriaDatoBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloPredictivoCategoriaDatoBO objetoBO = Mapper.Map<TModeloPredictivoCategoriaDato, ModeloPredictivoCategoriaDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloPredictivoCategoriaDatoBO FirstById(int id)
        {
            try
            {
                TModeloPredictivoCategoriaDato entidad = base.FirstById(id);
                ModeloPredictivoCategoriaDatoBO objetoBO = new ModeloPredictivoCategoriaDatoBO();
                Mapper.Map<TModeloPredictivoCategoriaDato, ModeloPredictivoCategoriaDatoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloPredictivoCategoriaDatoBO FirstBy(Expression<Func<TModeloPredictivoCategoriaDato, bool>> filter)
        {
            try
            {
                TModeloPredictivoCategoriaDato entidad = base.FirstBy(filter);
                ModeloPredictivoCategoriaDatoBO objetoBO = Mapper.Map<TModeloPredictivoCategoriaDato, ModeloPredictivoCategoriaDatoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloPredictivoCategoriaDatoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloPredictivoCategoriaDato entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloPredictivoCategoriaDatoBO> listadoBO)
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

        public bool Update(ModeloPredictivoCategoriaDatoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloPredictivoCategoriaDato entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloPredictivoCategoriaDatoBO> listadoBO)
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
        private void AsignacionId(TModeloPredictivoCategoriaDato entidad, ModeloPredictivoCategoriaDatoBO objetoBO)
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

        private TModeloPredictivoCategoriaDato MapeoEntidad(ModeloPredictivoCategoriaDatoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoCategoriaDato entidad = new TModeloPredictivoCategoriaDato();
                entidad = Mapper.Map<ModeloPredictivoCategoriaDatoBO, TModeloPredictivoCategoriaDato>(objetoBO,
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
        /// Obtiene las Categoria de Datos (activo) para el modelo Predictivo por programa 
        /// registradas en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ModeloPredictivoCategoriaDatoDTO> ObtenerCategoriaDatoPorPrograma(int idPGeneral)
        {
            try
            {
                List<ModeloPredictivoCategoriaDatoDTO> resultadoDTO = new List<ModeloPredictivoCategoriaDatoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,IdCategoriaOrigen,Nombre,Valor,Validar,IdSubCategoriaDato FROM pla.V_T_ModeloPredictivoCategoriaDato WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ModeloPredictivoCategoriaDatoDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Modelos Area Categoria Datos a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ModeloPredictivoCategoriaDatoDTO> nuevos)
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
