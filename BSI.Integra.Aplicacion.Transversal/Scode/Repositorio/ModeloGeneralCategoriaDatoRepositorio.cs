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
    public class ModeloGeneralCategoriaDatoRepositorio : BaseRepository<TModeloGeneralCategoriaDato, ModeloGeneralCategoriaDatoBO>
    {
        #region Metodos Base
        public ModeloGeneralCategoriaDatoRepositorio() : base()
        {
        }
        public ModeloGeneralCategoriaDatoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloGeneralCategoriaDatoBO> GetBy(Expression<Func<TModeloGeneralCategoriaDato, bool>> filter)
        {
            IEnumerable<TModeloGeneralCategoriaDato> listado = base.GetBy(filter);
            List<ModeloGeneralCategoriaDatoBO> listadoBO = new List<ModeloGeneralCategoriaDatoBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloGeneralCategoriaDatoBO objetoBO = Mapper.Map<TModeloGeneralCategoriaDato, ModeloGeneralCategoriaDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloGeneralCategoriaDatoBO FirstById(int id)
        {
            try
            {
                TModeloGeneralCategoriaDato entidad = base.FirstById(id);
                ModeloGeneralCategoriaDatoBO objetoBO = new ModeloGeneralCategoriaDatoBO();
                Mapper.Map<TModeloGeneralCategoriaDato, ModeloGeneralCategoriaDatoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloGeneralCategoriaDatoBO FirstBy(Expression<Func<TModeloGeneralCategoriaDato, bool>> filter)
        {
            try
            {
                TModeloGeneralCategoriaDato entidad = base.FirstBy(filter);
                ModeloGeneralCategoriaDatoBO objetoBO = Mapper.Map<TModeloGeneralCategoriaDato, ModeloGeneralCategoriaDatoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloGeneralCategoriaDatoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloGeneralCategoriaDato entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloGeneralCategoriaDatoBO> listadoBO)
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

        public bool Update(ModeloGeneralCategoriaDatoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloGeneralCategoriaDato entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloGeneralCategoriaDatoBO> listadoBO)
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
        private void AsignacionId(TModeloGeneralCategoriaDato entidad, ModeloGeneralCategoriaDatoBO objetoBO)
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

        private TModeloGeneralCategoriaDato MapeoEntidad(ModeloGeneralCategoriaDatoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloGeneralCategoriaDato entidad = new TModeloGeneralCategoriaDato();
                entidad = Mapper.Map<ModeloGeneralCategoriaDatoBO, TModeloGeneralCategoriaDato>(objetoBO,
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
        /// Obtiene los registros de ModeloGeneralCategoriaDato filtrado por el IdModeloGeneral
        /// </summary>
        /// <param name="idModelo"></param>
        /// <returns></returns>
        public List<ModeloGeneralCategoriaDatoDTO> ObtenerProgramaGeneralConfiguracionVariableFiltro(int idModelo)
        {
            try
            {
                return GetBy(x => x.IdModeloGeneral == idModelo && x.Estado == true, x => new ModeloGeneralCategoriaDatoDTO {Id = x.Id, IdAsociado = x.IdAsociado, Nombre = x.Nombre, Valor = x.Valor, IdModeloGeneral = x.IdModeloGeneral, IdCategoriaDato = x.IdCategoriaDato}).ToList(); 
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los registros de ModeloGeneralCategoriaDato asociados por el IdModelo
        /// </summary>
        /// <param name="idModeloGeneral"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorIdModeloGeneral(int idModeloGeneral, string usuario, List<ModeloGeneralCategoriaDatoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdModeloGeneral == idModeloGeneral && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdCategoriaDato == x.IdCategoriaDato));
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
