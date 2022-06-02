using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class TipoEnvioAutomaticoRepositorio : BaseRepository<TTipoEnvioAutomatico, TipoEnvioAutomaticoBO>
    {
        #region Metodos Base
        public TipoEnvioAutomaticoRepositorio() : base()
        {
        }
        public TipoEnvioAutomaticoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoEnvioAutomaticoBO> GetBy(Expression<Func<TTipoEnvioAutomatico, bool>> filter)
        {
            IEnumerable<TTipoEnvioAutomatico> listado = base.GetBy(filter);
            List<TipoEnvioAutomaticoBO> listadoBO = new List<TipoEnvioAutomaticoBO>();
            foreach (var itemEntidad in listado)
            {
                TipoEnvioAutomaticoBO objetoBO = Mapper.Map<TTipoEnvioAutomatico, TipoEnvioAutomaticoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoEnvioAutomaticoBO FirstById(int id)
        {
            try
            {
                TTipoEnvioAutomatico entidad = base.FirstById(id);
                TipoEnvioAutomaticoBO objetoBO = new TipoEnvioAutomaticoBO();
                Mapper.Map<TTipoEnvioAutomatico, TipoEnvioAutomaticoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoEnvioAutomaticoBO FirstBy(Expression<Func<TTipoEnvioAutomatico, bool>> filter)
        {
            try
            {
                TTipoEnvioAutomatico entidad = base.FirstBy(filter);
                TipoEnvioAutomaticoBO objetoBO = Mapper.Map<TTipoEnvioAutomatico, TipoEnvioAutomaticoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoEnvioAutomaticoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoEnvioAutomatico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoEnvioAutomaticoBO> listadoBO)
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

        public bool Update(TipoEnvioAutomaticoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoEnvioAutomatico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoEnvioAutomaticoBO> listadoBO)
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
        private void AsignacionId(TTipoEnvioAutomatico entidad, TipoEnvioAutomaticoBO objetoBO)
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

        private TTipoEnvioAutomatico MapeoEntidad(TipoEnvioAutomaticoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoEnvioAutomatico entidad = new TTipoEnvioAutomatico();
                entidad = Mapper.Map<TipoEnvioAutomaticoBO, TTipoEnvioAutomatico>(objetoBO,
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

        /// Autor: Jose Villena
        /// Fecha: 21/10/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de Tipos de Envio para ser usados en filtros
        /// </summary>
        /// <param></param>
        /// <returns>Lista</returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
