using System;
using System.Collections.Generic;
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
    public class HoraRepositorio : BaseRepository<THora, HoraBO>
    {
        #region Metodos Base
        public HoraRepositorio() : base()
        {
        }
        public HoraRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<HoraBO> GetBy(Expression<Func<THora, bool>> filter)
        {
            IEnumerable<THora> listado = base.GetBy(filter);
            List<HoraBO> listadoBO = new List<HoraBO>();
            foreach (var itemEntidad in listado)
            {
                HoraBO objetoBO = Mapper.Map<THora, HoraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public HoraBO FirstById(int id)
        {
            try
            {
                THora entidad = base.FirstById(id);
                HoraBO objetoBO = new HoraBO();
                Mapper.Map<THora, HoraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public HoraBO FirstBy(Expression<Func<THora, bool>> filter)
        {
            try
            {
                THora entidad = base.FirstBy(filter);
                HoraBO objetoBO = Mapper.Map<THora, HoraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(HoraBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                THora entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<HoraBO> listadoBO)
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

        public bool Update(HoraBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                THora entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<HoraBO> listadoBO)
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
        private void AsignacionId(THora entidad, HoraBO objetoBO)
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

        private THora MapeoEntidad(HoraBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                THora entidad = new THora();
                entidad = Mapper.Map<HoraBO, THora>(objetoBO,
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
        /// Obtener todos los registros de la tabla
        /// </summary>
        /// <returns>Lista de objetos de clase HoraDTO</returns>
        public List<HoraDTO> ObtenerListaHora()
        {
            try
            {
                string query = "select Id, Nombre from mkt.V_THora_Nombre where Estado = 1";
                var respuestaQuery = _dapper.QueryDapper(query, null);
                List<HoraDTO> listaHora = JsonConvert.DeserializeObject<List<HoraDTO>>(respuestaQuery);
                return listaHora;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtener todos los registros de la tabla segun el intervalo enviado
        /// </summary>
        /// <returns>Lista de objetos de clase HoraDTO</returns>
        public List<HoraDTO> ObtenerListaHoraIntervalo(int intervalo)
        {
            try
            {
                string querySp = "[pla].[SP_ObtenerHoraPorIntervalo]";
                var respuestaSP = _dapper.QuerySPDapper(querySp, new { Intervalo = intervalo });

                var horaResultado = JsonConvert.DeserializeObject<List<HoraDTO>>(respuestaSP);

                return horaResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
