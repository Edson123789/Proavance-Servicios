using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TarifarioRepositorio : BaseRepository<TTarifario, TarifarioBO>
    {
        #region Metodos Base
        public TarifarioRepositorio() : base()
        {
        }
        public TarifarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TarifarioBO> GetBy(Expression<Func<TTarifario, bool>> filter)
        {
            IEnumerable<TTarifario> listado = base.GetBy(filter);
            List<TarifarioBO> listadoBO = new List<TarifarioBO>();
            foreach (var itemEntidad in listado)
            {
                TarifarioBO objetoBO = Mapper.Map<TTarifario, TarifarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TarifarioBO FirstById(int id)
        {
            try
            {
                TTarifario entidad = base.FirstById(id);
                TarifarioBO objetoBO = new TarifarioBO();
                Mapper.Map<TTarifario, TarifarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TarifarioBO FirstBy(Expression<Func<TTarifario, bool>> filter)
        {
            try
            {
                TTarifario entidad = base.FirstBy(filter);
                TarifarioBO objetoBO = Mapper.Map<TTarifario, TarifarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TarifarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTarifario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TarifarioBO> listadoBO)
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

        public bool Update(TarifarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTarifario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TarifarioBO> listadoBO)
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
        private void AsignacionId(TTarifario entidad, TarifarioBO objetoBO)
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

        private TTarifario MapeoEntidad(TarifarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTarifario entidad = new TTarifario();
                entidad = Mapper.Map<TarifarioBO, TTarifario>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<TarifarioBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTarifario, bool>>> filters, Expression<Func<TTarifario, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TTarifario> listado = base.GetFiltered(filters, orderBy, ascending);
            List<TarifarioBO> listadoBO = new List<TarifarioBO>();

            foreach (var itemEntidad in listado)
            {
                TarifarioBO objetoBO = Mapper.Map<TTarifario, TarifarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene todos los registros para filtro
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ActualizarGestionadoCronogramaPagoTarifario(int id)
        {
            try
            {
                var actualizargestionado = _dapper.QuerySPDapper("[fin].[SP_ActualizarGestionadoCronogramaPagoTarifario]", new { id });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
