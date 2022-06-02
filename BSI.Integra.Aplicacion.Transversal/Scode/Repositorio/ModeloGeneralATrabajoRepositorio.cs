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
    public class ModeloGeneralATrabajoRepositorio : BaseRepository<TModeloGeneralAtrabajo, ModeloGeneralATrabajoBO>
    {
        #region Metodos Base
        public ModeloGeneralATrabajoRepositorio() : base()
        {
        }
        public ModeloGeneralATrabajoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloGeneralATrabajoBO> GetBy(Expression<Func<TModeloGeneralAtrabajo, bool>> filter)
        {
            IEnumerable<TModeloGeneralAtrabajo> listado = base.GetBy(filter);
            List<ModeloGeneralATrabajoBO> listadoBO = new List<ModeloGeneralATrabajoBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloGeneralATrabajoBO objetoBO = Mapper.Map<TModeloGeneralAtrabajo, ModeloGeneralATrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloGeneralATrabajoBO FirstById(int id)
        {
            try
            {
                TModeloGeneralAtrabajo entidad = base.FirstById(id);
                ModeloGeneralATrabajoBO objetoBO = new ModeloGeneralATrabajoBO();
                Mapper.Map<TModeloGeneralAtrabajo, ModeloGeneralATrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloGeneralATrabajoBO FirstBy(Expression<Func<TModeloGeneralAtrabajo, bool>> filter)
        {
            try
            {
                TModeloGeneralAtrabajo entidad = base.FirstBy(filter);
                ModeloGeneralATrabajoBO objetoBO = Mapper.Map<TModeloGeneralAtrabajo, ModeloGeneralATrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloGeneralATrabajoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloGeneralAtrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloGeneralATrabajoBO> listadoBO)
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

        public bool Update(ModeloGeneralATrabajoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloGeneralAtrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloGeneralATrabajoBO> listadoBO)
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
        private void AsignacionId(TModeloGeneralAtrabajo entidad, ModeloGeneralATrabajoBO objetoBO)
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

        private TModeloGeneralAtrabajo MapeoEntidad(ModeloGeneralATrabajoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloGeneralAtrabajo entidad = new TModeloGeneralAtrabajo();
                entidad = Mapper.Map<ModeloGeneralATrabajoBO, TModeloGeneralAtrabajo>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ModeloGeneralATrabajoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TModeloGeneralAtrabajo, bool>>> filters, Expression<Func<TModeloGeneralAtrabajo, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TModeloGeneralAtrabajo> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ModeloGeneralATrabajoBO> listadoBO = new List<ModeloGeneralATrabajoBO>();

            foreach (var itemEntidad in listado)
            {
                ModeloGeneralATrabajoBO objetoBO = Mapper.Map<TModeloGeneralAtrabajo, ModeloGeneralATrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los registros de ModeloGeneralATrabajo filtrado por el idModelo
        /// </summary>
        /// <param name="idModelo"></param>
        /// <returns></returns>
        public List<ModeloGeneralATrabajoDTO> ObtenerProgramaGeneralConfiguracionVariableFiltro(int idModelo)
        {
            try
            {
                return GetBy(x => x.IdModeloGeneral == idModelo && x.Estado == true, x => new ModeloGeneralATrabajoDTO { Id = x.Id, IdAreaTrabajo = x.IdAreaTrabajo, Nombre = x.Nombre, Valor = x.Valor, IdModeloGeneral = x.IdModeloGeneral }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los registros de ModeloGeneraLATrabajo asociados por el IdModelo
        /// </summary>
        /// <param name="idModeloGeneral"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorIdModeloGeneral(int idModeloGeneral, string usuario, List<ModeloGeneralATrabajoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdModeloGeneral == idModeloGeneral && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdAreaTrabajo == x.IdAreaTrabajo));
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
