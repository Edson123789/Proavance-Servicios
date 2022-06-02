using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: PersonalExperienciaRepositorio
    /// Autor: Britsel Calluchi - Luis Huallpa - Edgar Serruto .
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_PersonalExperiencia
    /// </summary>
    public class PersonalExperienciaRepositorio : BaseRepository<TPersonalExperiencia, PersonalExperienciaBO>
    {
        #region Metodos Base
        public PersonalExperienciaRepositorio() : base()
        {
        }
        public PersonalExperienciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalExperienciaBO> GetBy(Expression<Func<TPersonalExperiencia, bool>> filter)
        {
            IEnumerable<TPersonalExperiencia> listado = base.GetBy(filter);
            List<PersonalExperienciaBO> listadoBO = new List<PersonalExperienciaBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalExperienciaBO objetoBO = Mapper.Map<TPersonalExperiencia, PersonalExperienciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalExperienciaBO FirstById(int id)
        {
            try
            {
                TPersonalExperiencia entidad = base.FirstById(id);
                PersonalExperienciaBO objetoBO = new PersonalExperienciaBO();
                Mapper.Map<TPersonalExperiencia, PersonalExperienciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalExperienciaBO FirstBy(Expression<Func<TPersonalExperiencia, bool>> filter)
        {
            try
            {
                TPersonalExperiencia entidad = base.FirstBy(filter);
                PersonalExperienciaBO objetoBO = Mapper.Map<TPersonalExperiencia, PersonalExperienciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalExperienciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalExperiencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalExperienciaBO> listadoBO)
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

        public bool Update(PersonalExperienciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalExperiencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalExperienciaBO> listadoBO)
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
        private void AsignacionId(TPersonalExperiencia entidad, PersonalExperienciaBO objetoBO)
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

        private TPersonalExperiencia MapeoEntidad(PersonalExperienciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalExperiencia entidad = new TPersonalExperiencia();
                entidad = Mapper.Map<PersonalExperienciaBO, TPersonalExperiencia>(objetoBO,
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
        /// Repositorio: PersonalExperienciaRepositorio
        /// Autor: Luis Huallpa - Britsel Calluchi.
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene lista de experiencia del personal por idpersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> List<PersonalExperienciaDTO> </returns>
        public List<PersonalExperienciaDTO> ObtenerPersonalExperiencia(int idPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdPersonal == idPersonal).Select(x => new PersonalExperienciaDTO
				{
					Id = x.Id,
					IdPersonal = x.IdPersonal,
					FechaIngreso = x.FechaIngreso,
					FechaRetiro = x.FechaRetiro,
					IdAreaTrabajo = x.IdAreaTrabajo,
					IdCargo = x.IdCargo,
					IdEmpresa = x.IdEmpresa,
					MotivoRetiro = x.MotivoRetiro,
					NombreJefeInmediato = x.NombreJefeInmediato,
					TelefonoJefeInmediato = x.TelefonoJefeInmediato,
                    IdPersonalArchivo = x.IdPersonalArchivo
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}



        /// <summary>
        /// Obtiene lista de Experiencia por personal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<PersonalExperienciaFormularioDTO> ObtenerPorPersonal(int IdPersonal)
        {
            try
            {
                string query = "SELECT * from [gp].[TPersonalExperiencia_ObtenerPersonalExperiencia] WHERE IdPersonal = @IdPersonal AND Estado = 1 ORDER BY FechaIngreso";
                string queryRespuesta = _dapper.QueryDapper(query, new { IdPersonal });
                return JsonConvert.DeserializeObject<List<PersonalExperienciaFormularioDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
