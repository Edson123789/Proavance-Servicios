using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class CompetidorVentajaDesventajaRepositorio : BaseRepository<TCompetidorVentajaDesventaja, CompetidorVentajaDesventajaBO>
    {
        #region Metodos Base
        public CompetidorVentajaDesventajaRepositorio() : base()
        {
        }
        public CompetidorVentajaDesventajaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CompetidorVentajaDesventajaBO> GetBy(Expression<Func<TCompetidorVentajaDesventaja, bool>> filter)
        {
            IEnumerable<TCompetidorVentajaDesventaja> listado = base.GetBy(filter);
            List<CompetidorVentajaDesventajaBO> listadoBO = new List<CompetidorVentajaDesventajaBO>();
            foreach (var itemEntidad in listado)
            {
                CompetidorVentajaDesventajaBO objetoBO = Mapper.Map<TCompetidorVentajaDesventaja, CompetidorVentajaDesventajaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CompetidorVentajaDesventajaBO FirstById(int id)
        {
            try
            {
                TCompetidorVentajaDesventaja entidad = base.FirstById(id);
                CompetidorVentajaDesventajaBO objetoBO = new CompetidorVentajaDesventajaBO();
                Mapper.Map<TCompetidorVentajaDesventaja, CompetidorVentajaDesventajaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CompetidorVentajaDesventajaBO FirstBy(Expression<Func<TCompetidorVentajaDesventaja, bool>> filter)
        {
            try
            {
                TCompetidorVentajaDesventaja entidad = base.FirstBy(filter);
                CompetidorVentajaDesventajaBO objetoBO = Mapper.Map<TCompetidorVentajaDesventaja, CompetidorVentajaDesventajaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CompetidorVentajaDesventajaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCompetidorVentajaDesventaja entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CompetidorVentajaDesventajaBO> listadoBO)
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

        public bool Update(CompetidorVentajaDesventajaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCompetidorVentajaDesventaja entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CompetidorVentajaDesventajaBO> listadoBO)
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
        private void AsignacionId(TCompetidorVentajaDesventaja entidad, CompetidorVentajaDesventajaBO objetoBO)
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

        private TCompetidorVentajaDesventaja MapeoEntidad(CompetidorVentajaDesventajaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCompetidorVentajaDesventaja entidad = new TCompetidorVentajaDesventaja();
                entidad = Mapper.Map<CompetidorVentajaDesventajaBO, TCompetidorVentajaDesventaja>(objetoBO,
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
        /// Obtiene una lista de Ventajas dado un IdCompetidor [Tipo=1: Significa VENTAJA]
        /// </summary>
        /// <param name="IdCompetidor"></param>
        /// <returns></returns>
        public List<CompetidorVentajaDesventajaDTO> ObtenerCompetidorVentajas(int IdCompetidor)
        {
            try
            {
                string _query = "SELECT Id, IdCompetidor, Tipo, Contenido FROM pla.T_CompetidorVentajaDesventaja WHERE IdCompetidor = @Id AND Tipo=1 AND Estado=1";
                var RegistrosBO = _dapper.QueryDapper(_query, new { Id = IdCompetidor });
                List<CompetidorVentajaDesventajaDTO> listaCompetidorVentaja = JsonConvert.DeserializeObject<List<CompetidorVentajaDesventajaDTO>>(RegistrosBO);
                return listaCompetidorVentaja;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        /// <summary>
        /// Obtiene una lista de Desventajas dado un IdCompetidor [Tipo=0: Significa DESVENTAJA]
        /// </summary>
        /// <param name="IdCompetidor"></param>
        /// <returns></returns>
        public List<CompetidorVentajaDesventajaDTO> ObtenerCompetidorDesventajas(int IdCompetidor)
        {
            try
            {
                string _query = "SELECT Id, IdCompetidor, Tipo, Contenido FROM pla.T_CompetidorVentajaDesventaja WHERE IdCompetidor = @Id AND Tipo=0 AND Estado=1";
                var RegistrosBO = _dapper.QueryDapper(_query, new { Id = IdCompetidor });
                List<CompetidorVentajaDesventajaDTO> listaCompetidorDesventaja = JsonConvert.DeserializeObject<List<CompetidorVentajaDesventajaDTO>>(RegistrosBO);
                return listaCompetidorDesventaja;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }



    }
}
