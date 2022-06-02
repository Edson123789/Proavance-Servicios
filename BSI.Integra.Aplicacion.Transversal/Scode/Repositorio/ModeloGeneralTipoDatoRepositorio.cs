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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ModeloGeneralTipoDatoRepositorio : BaseRepository<TModeloGeneralTipoDato, ModeloGeneralTipoDatoBO>
    {
        #region Metodos Base
        public ModeloGeneralTipoDatoRepositorio() : base()
        {
        }
        public ModeloGeneralTipoDatoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloGeneralTipoDatoBO> GetBy(Expression<Func<TModeloGeneralTipoDato, bool>> filter)
        {
            IEnumerable<TModeloGeneralTipoDato> listado = base.GetBy(filter);
            List<ModeloGeneralTipoDatoBO> listadoBO = new List<ModeloGeneralTipoDatoBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloGeneralTipoDatoBO objetoBO = Mapper.Map<TModeloGeneralTipoDato, ModeloGeneralTipoDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloGeneralTipoDatoBO FirstById(int id)
        {
            try
            {
                TModeloGeneralTipoDato entidad = base.FirstById(id);
                ModeloGeneralTipoDatoBO objetoBO = new ModeloGeneralTipoDatoBO();
                Mapper.Map<TModeloGeneralTipoDato, ModeloGeneralTipoDatoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloGeneralTipoDatoBO FirstBy(Expression<Func<TModeloGeneralTipoDato, bool>> filter)
        {
            try
            {
                TModeloGeneralTipoDato entidad = base.FirstBy(filter);
                ModeloGeneralTipoDatoBO objetoBO = Mapper.Map<TModeloGeneralTipoDato, ModeloGeneralTipoDatoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloGeneralTipoDatoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloGeneralTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloGeneralTipoDatoBO> listadoBO)
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

        public bool Update(ModeloGeneralTipoDatoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloGeneralTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloGeneralTipoDatoBO> listadoBO)
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
        private void AsignacionId(TModeloGeneralTipoDato entidad, ModeloGeneralTipoDatoBO objetoBO)
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

        private TModeloGeneralTipoDato MapeoEntidad(ModeloGeneralTipoDatoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloGeneralTipoDato entidad = new TModeloGeneralTipoDato();
                entidad = Mapper.Map<ModeloGeneralTipoDatoBO, TModeloGeneralTipoDato>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ModeloGeneralTipoDatoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TModeloGeneralTipoDato, bool>>> filters, Expression<Func<TModeloGeneralTipoDato, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TModeloGeneralTipoDato> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ModeloGeneralTipoDatoBO> listadoBO = new List<ModeloGeneralTipoDatoBO>();

            foreach (var itemEntidad in listado)
            {
                ModeloGeneralTipoDatoBO objetoBO = Mapper.Map<TModeloGeneralTipoDato, ModeloGeneralTipoDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los registros de ModeloGeneralTipoDato filtrado por el idModelo
        /// </summary>
        /// <param name="idModelo"></param>
        /// <returns></returns>
        public List<ModeloGeneralTipoDatoDTO> ObtenerProgramaGeneralConfiguracionVariableFiltro(int idModelo)
        {
            try
            {
                return GetBy(x => x.IdModeloGeneral == idModelo && x.Estado == true, x => new ModeloGeneralTipoDatoDTO { Id = x.Id, IdTipoDato = x.IdTipoDato, IdModeloGeneral = x.IdModeloGeneral }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los registros de ModeloGeneralTipoDato asociados por el IdModelo
        /// </summary>
        /// <param name="idModeloGeneral"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorIdModeloGeneral(int idModeloGeneral, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdModeloGeneral == idModeloGeneral && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.Id));
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
