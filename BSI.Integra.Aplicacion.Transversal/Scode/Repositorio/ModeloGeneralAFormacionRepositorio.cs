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
    public class ModeloGeneralAFormacionRepositorio : BaseRepository<TModeloGeneralAformacion, ModeloGeneralAFormacionBO>
    {
        #region Metodos Base
        public ModeloGeneralAFormacionRepositorio() : base()
        {
        }
        public ModeloGeneralAFormacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloGeneralAFormacionBO> GetBy(Expression<Func<TModeloGeneralAformacion, bool>> filter)
        {
            IEnumerable<TModeloGeneralAformacion> listado = base.GetBy(filter);
            List<ModeloGeneralAFormacionBO> listadoBO = new List<ModeloGeneralAFormacionBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloGeneralAFormacionBO objetoBO = Mapper.Map<TModeloGeneralAformacion, ModeloGeneralAFormacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloGeneralAFormacionBO FirstById(int id)
        {
            try
            {
                TModeloGeneralAformacion entidad = base.FirstById(id);
                ModeloGeneralAFormacionBO objetoBO = new ModeloGeneralAFormacionBO();
                Mapper.Map<TModeloGeneralAformacion, ModeloGeneralAFormacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloGeneralAFormacionBO FirstBy(Expression<Func<TModeloGeneralAformacion, bool>> filter)
        {
            try
            {
                TModeloGeneralAformacion entidad = base.FirstBy(filter);
                ModeloGeneralAFormacionBO objetoBO = Mapper.Map<TModeloGeneralAformacion, ModeloGeneralAFormacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloGeneralAFormacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloGeneralAformacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloGeneralAFormacionBO> listadoBO)
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

        public bool Update(ModeloGeneralAFormacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloGeneralAformacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloGeneralAFormacionBO> listadoBO)
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
        private void AsignacionId(TModeloGeneralAformacion entidad, ModeloGeneralAFormacionBO objetoBO)
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

        private TModeloGeneralAformacion MapeoEntidad(ModeloGeneralAFormacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloGeneralAformacion entidad = new TModeloGeneralAformacion();
                entidad = Mapper.Map<ModeloGeneralAFormacionBO, TModeloGeneralAformacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ModeloGeneralAFormacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TModeloGeneralAformacion, bool>>> filters, Expression<Func<TModeloGeneralAformacion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TModeloGeneralAformacion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ModeloGeneralAFormacionBO> listadoBO = new List<ModeloGeneralAFormacionBO>();

            foreach (var itemEntidad in listado)
            {
                ModeloGeneralAFormacionBO objetoBO = Mapper.Map<TModeloGeneralAformacion, ModeloGeneralAFormacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los registros de ModeloGeneralAFormacion filtrado por idModelo
        /// </summary>
        /// <param name="idModelo"></param>
        /// <returns></returns>
        public List<ModeloGeneralAFormacionDTO> ObtenerProgramaGeneralConfiguracionVariableFiltro(int idModelo)
        {
            try
            {
                return GetBy(x => x.IdModeloGeneral == idModelo && x.Estado == true, x => new ModeloGeneralAFormacionDTO { Id = x.Id, IdAreaFormacion = x.IdAreaFormacion, Nombre = x.Nombre, Valor = x.Valor, IdModeloGeneral = x.IdModeloGeneral }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los registros de ModeloGeneralAFormacion asociados por el IdModelo
        /// </summary>
        /// <param name="idModeloGeneral"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorIdModeloGeneral(int idModeloGeneral, string usuario, List<ModeloGeneralAFormacionDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdModeloGeneral == idModeloGeneral && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdAreaFormacion == x.IdAreaFormacion));
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
