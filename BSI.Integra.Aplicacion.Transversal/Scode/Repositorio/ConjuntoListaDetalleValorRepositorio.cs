using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ConjuntoListaDetalleValorRepositorio : BaseRepository<TConjuntoListaDetalleValor, ConjuntoListaDetalleValorBO>
    {
        #region Metodos Base
        public ConjuntoListaDetalleValorRepositorio() : base()
        {
        }
        public ConjuntoListaDetalleValorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConjuntoListaDetalleValorBO> GetBy(Expression<Func<TConjuntoListaDetalleValor, bool>> filter)
        {
            IEnumerable<TConjuntoListaDetalleValor> listado = base.GetBy(filter);
            List<ConjuntoListaDetalleValorBO> listadoBO = new List<ConjuntoListaDetalleValorBO>();
            foreach (var itemEntidad in listado)
            {
                ConjuntoListaDetalleValorBO objetoBO = Mapper.Map<TConjuntoListaDetalleValor, ConjuntoListaDetalleValorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConjuntoListaDetalleValorBO FirstById(int id)
        {
            try
            {
                TConjuntoListaDetalleValor entidad = base.FirstById(id);
                ConjuntoListaDetalleValorBO objetoBO = new ConjuntoListaDetalleValorBO();
                Mapper.Map<TConjuntoListaDetalleValor, ConjuntoListaDetalleValorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConjuntoListaDetalleValorBO FirstBy(Expression<Func<TConjuntoListaDetalleValor, bool>> filter)
        {
            try
            {
                TConjuntoListaDetalleValor entidad = base.FirstBy(filter);
                ConjuntoListaDetalleValorBO objetoBO = Mapper.Map<TConjuntoListaDetalleValor, ConjuntoListaDetalleValorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConjuntoListaDetalleValorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConjuntoListaDetalleValor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConjuntoListaDetalleValorBO> listadoBO)
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

        public bool Update(ConjuntoListaDetalleValorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConjuntoListaDetalleValor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConjuntoListaDetalleValorBO> listadoBO)
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
        private void AsignacionId(TConjuntoListaDetalleValor entidad, ConjuntoListaDetalleValorBO objetoBO)
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

        private TConjuntoListaDetalleValor MapeoEntidad(ConjuntoListaDetalleValorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConjuntoListaDetalleValor entidad = new TConjuntoListaDetalleValor();
                entidad = Mapper.Map<ConjuntoListaDetalleValorBO, TConjuntoListaDetalleValor>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene un conjunto de lista detalle valor
        /// </summary>
        /// <param name="idConjuntoListaDetalle"></param>
        /// <returns></returns>
        public List<FiltroSegmentoValorTipoDTO> ObtenerConjuntoListaDetalleValor(int idConjuntoListaDetalle)
        {
            try
            {
                return this.GetBy(x => x.IdConjuntoListaDetalle == idConjuntoListaDetalle && x.Estado == true, x => new FiltroSegmentoValorTipoDTO { Id = x.Id, IdCategoriaObjetoFiltro = x.IdCategoriaObjetoFiltro, Valor = x.Valor }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
