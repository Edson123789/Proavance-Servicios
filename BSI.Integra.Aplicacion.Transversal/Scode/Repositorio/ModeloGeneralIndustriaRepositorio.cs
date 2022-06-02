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
    public class ModeloGeneralIndustriaRepositorio : BaseRepository<TModeloGeneralIndustria, ModeloGeneralIndustriaBO>
    {
        #region Metodos Base
        public ModeloGeneralIndustriaRepositorio() : base()
        {
        }
        public ModeloGeneralIndustriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloGeneralIndustriaBO> GetBy(Expression<Func<TModeloGeneralIndustria, bool>> filter)
        {
            IEnumerable<TModeloGeneralIndustria> listado = base.GetBy(filter);
            List<ModeloGeneralIndustriaBO> listadoBO = new List<ModeloGeneralIndustriaBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloGeneralIndustriaBO objetoBO = Mapper.Map<TModeloGeneralIndustria, ModeloGeneralIndustriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloGeneralIndustriaBO FirstById(int id)
        {
            try
            {
                TModeloGeneralIndustria entidad = base.FirstById(id);
                ModeloGeneralIndustriaBO objetoBO = new ModeloGeneralIndustriaBO();
                Mapper.Map<TModeloGeneralIndustria, ModeloGeneralIndustriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloGeneralIndustriaBO FirstBy(Expression<Func<TModeloGeneralIndustria, bool>> filter)
        {
            try
            {
                TModeloGeneralIndustria entidad = base.FirstBy(filter);
                ModeloGeneralIndustriaBO objetoBO = Mapper.Map<TModeloGeneralIndustria, ModeloGeneralIndustriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloGeneralIndustriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloGeneralIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloGeneralIndustriaBO> listadoBO)
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

        public bool Update(ModeloGeneralIndustriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloGeneralIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloGeneralIndustriaBO> listadoBO)
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
        private void AsignacionId(TModeloGeneralIndustria entidad, ModeloGeneralIndustriaBO objetoBO)
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

        private TModeloGeneralIndustria MapeoEntidad(ModeloGeneralIndustriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloGeneralIndustria entidad = new TModeloGeneralIndustria();
                entidad = Mapper.Map<ModeloGeneralIndustriaBO, TModeloGeneralIndustria>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ModeloGeneralIndustriaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TModeloGeneralIndustria, bool>>> filters, Expression<Func<TModeloGeneralIndustria, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TModeloGeneralIndustria> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ModeloGeneralIndustriaBO> listadoBO = new List<ModeloGeneralIndustriaBO>();

            foreach (var itemEntidad in listado)
            {
                ModeloGeneralIndustriaBO objetoBO = Mapper.Map<TModeloGeneralIndustria, ModeloGeneralIndustriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los registros de ModeloGeneralIndustria filtrado por idModelo
        /// </summary>
        /// <param name="idModelo"></param>
        /// <returns></returns>
        public List<ModeloGeneralIndustriaDTO> ObtenerProgramaGeneralConfiguracionVariableFiltro(int idModelo)
        {
            try
            {
                return GetBy(x => x.IdModeloGeneral == idModelo && x.Estado == true, x => new ModeloGeneralIndustriaDTO { Id = x.Id, IdIndustria = x.IdIndustria, Nombre = x.Nombre, Valor = x.Valor, IdModeloGeneral = x.IdModeloGeneral }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los registros de ModeloGeneralIndustria asociados por el IdModelo
        /// </summary>
        /// <param name="idModeloGeneral"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorIdModeloGeneral(int idModeloGeneral, string usuario, List<ModeloGeneralIndustriaDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdModeloGeneral == idModeloGeneral && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdIndustria == x.IdIndustria));
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
