using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Gestion de Personas/PersonalLog
    /// Autor: Luis Huallpa - Britsel C. - Jose V.
    /// Fecha: 28/04/2021
    /// <summary>
    /// Repositorio para consultas de gp.T_PersonalLog
    /// </summary>
    public class PersonalLogRepositorio : BaseRepository<TPersonalLog, PersonalLogBO>
    {
        #region Metodos Base
        public PersonalLogRepositorio() : base()
        {
        }
        public PersonalLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalLogBO> GetBy(Expression<Func<TPersonalLog, bool>> filter)
        {
            IEnumerable<TPersonalLog> listado = base.GetBy(filter);
            List<PersonalLogBO> listadoBO = new List<PersonalLogBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalLogBO objetoBO = Mapper.Map<TPersonalLog, PersonalLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalLogBO FirstById(int id)
        {
            try
            {
                TPersonalLog entidad = base.FirstById(id);
                PersonalLogBO objetoBO = new PersonalLogBO();
                Mapper.Map<TPersonalLog, PersonalLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalLogBO FirstBy(Expression<Func<TPersonalLog, bool>> filter)
        {
            try
            {
                TPersonalLog entidad = base.FirstBy(filter);
                PersonalLogBO objetoBO = Mapper.Map<TPersonalLog, PersonalLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalLogBO> listadoBO)
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

        public bool Update(PersonalLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalLogBO> listadoBO)
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
        private void AsignacionId(TPersonalLog entidad, PersonalLogBO objetoBO)
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

        private TPersonalLog MapeoEntidad(PersonalLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalLog entidad = new TPersonalLog();
                entidad = Mapper.Map<PersonalLogBO, TPersonalLog>(objetoBO,
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

        ///Repositorio: PersonalLogRepositorio
        ///Autor: Luis H.,Edgar S.
        ///Fecha: 19/03/2021
        /// <summary>
        /// Obtiene la fecha fin del utlimo registro del personal
        /// </summary>
        /// <param name="id"> Id de Personal </param>
        /// <returns>  DateTime? </returns>
        public DateTime? ObtenerFechaFin(int id)
        {
            try
            {
                var _resultado = new DateTime?();
                string query = "SELECT FechaFin FROM gp.T_PersonalLog WHERE Estado = 1 AND IdPersonal = @Id";
                var resultado = _dapper.FirstOrDefault(query, new { id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<DateTime>(resultado);
                }
                return _resultado;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalLogRepositorio
        ///Autor: Luis H.,Edgar S.
        ///Fecha: 19/03/2021
        /// <summary>
        /// Obtiene la registro histórico de Jefe Inmediato por Personal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal</param>
        /// <returns> List<PersonalJefeInmediatoDTO> </returns>
        public List<PersonalJefeInmediatoDTO> ObtenerJefeInmediatoHistorico (int idPersonal)
        {
            try
            {
                List<PersonalJefeInmediatoDTO> listaJefeInmediato = new List<PersonalJefeInmediatoDTO>();
                string query = "SELECT Id,IdJefe,DatosJefe,FechaInicio,FechaFin FROM [gp].[V_TPersonalLog_ObtenerHistoricoJefeInmediato] WHERE Estado = 1 AND EstadoIdJefe = 1 AND IdPersonal = @idPersonal";
                var resultado = _dapper.QueryDapper(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaJefeInmediato = JsonConvert.DeserializeObject<List<PersonalJefeInmediatoDTO>>(resultado);
                }
                return listaJefeInmediato;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalLogRepositorio
        ///Autor: Luis H.,Edgar S.
        ///Fecha: 19/03/2021
        /// <summary>
        /// Obtiene la registro histórico de Tipo de Asesor
        /// </summary>
        /// <param name="idPersonal"> Id de Personal</param>
        /// <returns> List<PersonalTipoAsesorDTO> </returns>
        public List<PersonalTipoAsesorDTO> ObtenerTipoAsesorHistorico(int idPersonal)
        {
            try
            {
                List<PersonalTipoAsesorDTO> listaJefeInmediato = new List<PersonalTipoAsesorDTO>();
                string query = "SELECT Id,IdCerrador,EsCerrador, FechaInicio, FechaFin FROM [gp].[V_TPersonalLog_ObtenerHistoricoTipoAsesor] WHERE EstadoCerrador = 1 AND IdPersonal = @idPersonal";
                var resultado = _dapper.QueryDapper(query, new { idPersonal });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaJefeInmediato = JsonConvert.DeserializeObject<List<PersonalTipoAsesorDTO>>(resultado);
                }
                return listaJefeInmediato;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
