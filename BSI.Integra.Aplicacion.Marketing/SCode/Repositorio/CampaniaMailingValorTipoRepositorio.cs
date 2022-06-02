using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class CampaniaMailingValorTipoRepositorio : BaseRepository<TCampaniaMailingValorTipo, CampaniaMailingValorTipoBO>
    {
        #region Metodos Base
        public CampaniaMailingValorTipoRepositorio() : base()
        {
        }
        public CampaniaMailingValorTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampaniaMailingValorTipoBO> GetBy(Expression<Func<TCampaniaMailingValorTipo, bool>> filter)
        {
            IEnumerable<TCampaniaMailingValorTipo> listado = base.GetBy(filter);
            List<CampaniaMailingValorTipoBO> listadoBO = new List<CampaniaMailingValorTipoBO>();
            foreach (var itemEntidad in listado)
            {
                CampaniaMailingValorTipoBO objetoBO = Mapper.Map<TCampaniaMailingValorTipo, CampaniaMailingValorTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampaniaMailingValorTipoBO FirstById(int id)
        {
            try
            {
                TCampaniaMailingValorTipo entidad = base.FirstById(id);
                CampaniaMailingValorTipoBO objetoBO = new CampaniaMailingValorTipoBO();
                Mapper.Map<TCampaniaMailingValorTipo, CampaniaMailingValorTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampaniaMailingValorTipoBO FirstBy(Expression<Func<TCampaniaMailingValorTipo, bool>> filter)
        {
            try
            {
                TCampaniaMailingValorTipo entidad = base.FirstBy(filter);
                CampaniaMailingValorTipoBO objetoBO = Mapper.Map<TCampaniaMailingValorTipo, CampaniaMailingValorTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampaniaMailingValorTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampaniaMailingValorTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampaniaMailingValorTipoBO> listadoBO)
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

        public bool Update(CampaniaMailingValorTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampaniaMailingValorTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampaniaMailingValorTipoBO> listadoBO)
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
        private void AsignacionId(TCampaniaMailingValorTipo entidad, CampaniaMailingValorTipoBO objetoBO)
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

        private TCampaniaMailingValorTipo MapeoEntidad(CampaniaMailingValorTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampaniaMailingValorTipo entidad = new TCampaniaMailingValorTipo();
                entidad = Mapper.Map<CampaniaMailingValorTipoBO, TCampaniaMailingValorTipo>(objetoBO,
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
        /// Eliminacion de los registros que no estan en la lista de exclusion
        /// </summary>
        /// <param name="campaniaMailing"></param>
        public void EliminacionLogicaPorCampaniaMailing(CampaniaMailingDTO campaniaMailing)
        {
            try
            {
                var listaBorrar = this.GetBy(x => x.IdCampaniaMailing == campaniaMailing.Id && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => campaniaMailing.ListaExcluirPorCampaniaMailing.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing));
                foreach (var item in listaBorrar)
                {
                    this.Delete(item.Id, campaniaMailing.UsuarioModificacion);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene campaniaMailingValorTipo por campaña mailing
        /// </summary>
        /// <param name="idCampaniaMailing"></param>
        /// <returns></returns>
        public List<CampaniaMailingValorTipoDTO> ObtenerPorIdCampaniaMailing(int idCampaniaMailing)
        {
            try
            {
                return this.GetBy(x => x.IdCampaniaMailing == idCampaniaMailing && x.Estado == true, x => new CampaniaMailingValorTipoDTO { Id = x.Id, IdCategoriaObjetoFiltro = x.IdCategoriaObjetoFiltro, Valor = x.Valor }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

