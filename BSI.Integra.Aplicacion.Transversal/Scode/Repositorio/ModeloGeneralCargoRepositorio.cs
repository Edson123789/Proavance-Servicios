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
    public class ModeloGeneralCargoRepositorio : BaseRepository<TModeloGeneralCargo, ModeloGeneralCargoBO>
    {
        #region Metodos Base
        public ModeloGeneralCargoRepositorio() : base()
        {
        }
        public ModeloGeneralCargoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloGeneralCargoBO> GetBy(Expression<Func<TModeloGeneralCargo, bool>> filter)
        {
            IEnumerable<TModeloGeneralCargo> listado = base.GetBy(filter);
            List<ModeloGeneralCargoBO> listadoBO = new List<ModeloGeneralCargoBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloGeneralCargoBO objetoBO = Mapper.Map<TModeloGeneralCargo, ModeloGeneralCargoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloGeneralCargoBO FirstById(int id)
        {
            try
            {
                TModeloGeneralCargo entidad = base.FirstById(id);
                ModeloGeneralCargoBO objetoBO = new ModeloGeneralCargoBO();
                Mapper.Map<TModeloGeneralCargo, ModeloGeneralCargoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloGeneralCargoBO FirstBy(Expression<Func<TModeloGeneralCargo, bool>> filter)
        {
            try
            {
                TModeloGeneralCargo entidad = base.FirstBy(filter);
                ModeloGeneralCargoBO objetoBO = Mapper.Map<TModeloGeneralCargo, ModeloGeneralCargoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloGeneralCargoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloGeneralCargo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloGeneralCargoBO> listadoBO)
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

        public bool Update(ModeloGeneralCargoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloGeneralCargo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloGeneralCargoBO> listadoBO)
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
        private void AsignacionId(TModeloGeneralCargo entidad, ModeloGeneralCargoBO objetoBO)
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

        private TModeloGeneralCargo MapeoEntidad(ModeloGeneralCargoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloGeneralCargo entidad = new TModeloGeneralCargo();
                entidad = Mapper.Map<ModeloGeneralCargoBO, TModeloGeneralCargo>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ModeloGeneralCargoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TModeloGeneralCargo, bool>>> filters, Expression<Func<TModeloGeneralCargo, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TModeloGeneralCargo> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ModeloGeneralCargoBO> listadoBO = new List<ModeloGeneralCargoBO>();

            foreach (var itemEntidad in listado)
            {
                ModeloGeneralCargoBO objetoBO = Mapper.Map<TModeloGeneralCargo, ModeloGeneralCargoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los registros de ModeloGeneralCargo filtrado por el IdModeloGeneral
        /// </summary>
        /// <param name="idModelo"></param>
        /// <returns></returns>
        public List<ModeloGeneralCargoDTO> ObtenerProgramaGeneralConfiguracionVariableFiltro(int idModelo)
        {
            try
            {
                return GetBy(x => x.IdModeloGeneral == idModelo && x.Estado == true, x => new ModeloGeneralCargoDTO { Id = x.Id, IdCargo = x.IdCargo, Nombre = x.Nombre, Valor = x.Valor, IdModeloGeneral = x.IdModeloGeneral }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///  Elimina (Actualiza estado a false ) todos los registros de ModeloGeneralCargo asociados por el IdModelo
        /// </summary>
        /// <param name="idModeloGeneral"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorIdModeloGeneral(int idModeloGeneral, string usuario, List<ModeloGeneralCargoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdModeloGeneral == idModeloGeneral && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdCargo == x.IdCargo));
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
