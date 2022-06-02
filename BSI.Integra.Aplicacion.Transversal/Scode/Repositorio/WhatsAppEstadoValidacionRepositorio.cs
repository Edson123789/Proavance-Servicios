using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class WhatsAppEstadoValidacionRepositorio : BaseRepository<TWhatsAppEstadoValidacion, WhatsAppEstadoValidacionBO>
    {
        #region Metodos Base
        public WhatsAppEstadoValidacionRepositorio() : base()
        {
        }
        public WhatsAppEstadoValidacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppEstadoValidacionBO> GetBy(Expression<Func<TWhatsAppEstadoValidacion, bool>> filter)
        {
            IEnumerable<TWhatsAppEstadoValidacion> listado = base.GetBy(filter);
            List<WhatsAppEstadoValidacionBO> listadoBO = new List<WhatsAppEstadoValidacionBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppEstadoValidacionBO objetoBO = Mapper.Map<TWhatsAppEstadoValidacion, WhatsAppEstadoValidacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppEstadoValidacionBO FirstById(int id)
        {
            try
            {
                TWhatsAppEstadoValidacion entidad = base.FirstById(id);
                WhatsAppEstadoValidacionBO objetoBO = new WhatsAppEstadoValidacionBO();
                Mapper.Map<TWhatsAppEstadoValidacion, WhatsAppEstadoValidacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppEstadoValidacionBO FirstBy(Expression<Func<TWhatsAppEstadoValidacion, bool>> filter)
        {
            try
            {
                TWhatsAppEstadoValidacion entidad = base.FirstBy(filter);
                WhatsAppEstadoValidacionBO objetoBO = Mapper.Map<TWhatsAppEstadoValidacion, WhatsAppEstadoValidacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppEstadoValidacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppEstadoValidacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppEstadoValidacionBO> listadoBO)
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

        public bool Update(WhatsAppEstadoValidacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppEstadoValidacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppEstadoValidacionBO> listadoBO)
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
        private void AsignacionId(TWhatsAppEstadoValidacion entidad, WhatsAppEstadoValidacionBO objetoBO)
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

        private TWhatsAppEstadoValidacion MapeoEntidad(WhatsAppEstadoValidacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppEstadoValidacion entidad = new TWhatsAppEstadoValidacion();
                entidad = Mapper.Map<WhatsAppEstadoValidacionBO, TWhatsAppEstadoValidacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<WhatsAppEstadoValidacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TWhatsAppEstadoValidacion, bool>>> filters, Expression<Func<TWhatsAppEstadoValidacion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TWhatsAppEstadoValidacion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<WhatsAppEstadoValidacionBO> listadoBO = new List<WhatsAppEstadoValidacionBO>();

            foreach (var itemEntidad in listado)
            {
                WhatsAppEstadoValidacionBO objetoBO = Mapper.Map<TWhatsAppEstadoValidacion, WhatsAppEstadoValidacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Funcion de donde retorna los tipos de estados de ejecucion en que se encuentra una lista
        /// </summary>
        /// <returns>Retorna los tipos de estados de ejecucion en que se encuentra una lista (List<WhatsAppEstadoValidacionDTO>)</returns>
        public List<WhatsAppEstadoValidacionDTO> ObtenerListaEstadosValidacionNumeroWhatsApp()
        {
            try
            {
                List<WhatsAppEstadoValidacionDTO> TipoPersona = new List<WhatsAppEstadoValidacionDTO>();
                string QueryEstadoValidacion = string.Empty;
                QueryEstadoValidacion = "SELECT Id, Nombre FROM mkt.V_TWhatsAppEstadoValidacion_ObtenerParaFiltro";
                var QueryLista = _dapper.QueryDapper(QueryEstadoValidacion, null);
                if (!string.IsNullOrEmpty(QueryLista) && !QueryLista.Contains("[]"))
                {
                    TipoPersona = JsonConvert.DeserializeObject<List<WhatsAppEstadoValidacionDTO>>(QueryLista);
                }
                return TipoPersona;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
