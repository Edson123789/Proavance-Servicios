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
    public class ModeloGeneralEscalaRepositorio : BaseRepository<TModeloGeneralEscala, ModeloGeneralEscalaBO>
    {
        #region Metodos Base
        public ModeloGeneralEscalaRepositorio() : base()
        {
        }
        public ModeloGeneralEscalaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloGeneralEscalaBO> GetBy(Expression<Func<TModeloGeneralEscala, bool>> filter)
        {
            IEnumerable<TModeloGeneralEscala> listado = base.GetBy(filter);
            List<ModeloGeneralEscalaBO> listadoBO = new List<ModeloGeneralEscalaBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloGeneralEscalaBO objetoBO = Mapper.Map<TModeloGeneralEscala, ModeloGeneralEscalaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloGeneralEscalaBO FirstById(int id)
        {
            try
            {
                TModeloGeneralEscala entidad = base.FirstById(id);
                ModeloGeneralEscalaBO objetoBO = new ModeloGeneralEscalaBO();
                Mapper.Map<TModeloGeneralEscala, ModeloGeneralEscalaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloGeneralEscalaBO FirstBy(Expression<Func<TModeloGeneralEscala, bool>> filter)
        {
            try
            {
                TModeloGeneralEscala entidad = base.FirstBy(filter);
                ModeloGeneralEscalaBO objetoBO = Mapper.Map<TModeloGeneralEscala, ModeloGeneralEscalaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloGeneralEscalaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloGeneralEscala entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloGeneralEscalaBO> listadoBO)
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

        public bool Update(ModeloGeneralEscalaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloGeneralEscala entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloGeneralEscalaBO> listadoBO)
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
        private void AsignacionId(TModeloGeneralEscala entidad, ModeloGeneralEscalaBO objetoBO)
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

        private TModeloGeneralEscala MapeoEntidad(ModeloGeneralEscalaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloGeneralEscala entidad = new TModeloGeneralEscala();
                entidad = Mapper.Map<ModeloGeneralEscalaBO, TModeloGeneralEscala>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ModeloGeneralEscalaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TModeloGeneralEscala, bool>>> filters, Expression<Func<TModeloGeneralEscala, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TModeloGeneralEscala> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ModeloGeneralEscalaBO> listadoBO = new List<ModeloGeneralEscalaBO>();

            foreach (var itemEntidad in listado)
            {
                ModeloGeneralEscalaBO objetoBO = Mapper.Map<TModeloGeneralEscala, ModeloGeneralEscalaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los registros de ModeloGeneralEscala filtrado por el idModelo
        /// </summary>
        /// <param name="idModelo"></param>
        /// <returns></returns>
        public List<ModeloGeneralEscalaDTO> ObtenerProgramaGeneralConfiguracionVariableFiltro(int idModelo)
        {
            try
            {
                return GetBy(x => x.IdModeloGeneral == idModelo && x.Estado == true, x => new ModeloGeneralEscalaDTO { Id = x.Id, Orden = x.Orden, Nombre = x.Nombre, ValorMaximo = x.ValorMaximo, ValorMinimo = x.ValorMinimo, IdModeloGeneral = x.IdModeloGeneral }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los registros de ModeloGeneralEscala asociados por el IdModelo
        /// </summary>
        /// <param name="idModeloGeneral"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorIdModeloGeneral(int idModeloGeneral, string usuario, List<ModeloGeneralEscalaDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdModeloGeneral == idModeloGeneral && x.Estado == true).ToList();
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
