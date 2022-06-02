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
    /// Repositorio: PersonalFormacionRepositorio
    /// Autor: Britsel Calluchi - Luis Huallpa.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_PersonalFormacion
    /// </summary>
    public class PersonalFormacionRepositorio : BaseRepository<TPersonalFormacion, PersonalFormacionBO>
    {
        #region Metodos Base
        public PersonalFormacionRepositorio() : base()
        {
        }
        public PersonalFormacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalFormacionBO> GetBy(Expression<Func<TPersonalFormacion, bool>> filter)
        {
            IEnumerable<TPersonalFormacion> listado = base.GetBy(filter);
            List<PersonalFormacionBO> listadoBO = new List<PersonalFormacionBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalFormacionBO objetoBO = Mapper.Map<TPersonalFormacion, PersonalFormacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalFormacionBO FirstById(int id)
        {
            try
            {
                TPersonalFormacion entidad = base.FirstById(id);
                PersonalFormacionBO objetoBO = new PersonalFormacionBO();
                Mapper.Map<TPersonalFormacion, PersonalFormacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalFormacionBO FirstBy(Expression<Func<TPersonalFormacion, bool>> filter)
        {
            try
            {
                TPersonalFormacion entidad = base.FirstBy(filter);
                PersonalFormacionBO objetoBO = Mapper.Map<TPersonalFormacion, PersonalFormacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalFormacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalFormacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalFormacionBO> listadoBO)
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

        public bool Update(PersonalFormacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalFormacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalFormacionBO> listadoBO)
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
        private void AsignacionId(TPersonalFormacion entidad, PersonalFormacionBO objetoBO)
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

        private TPersonalFormacion MapeoEntidad(PersonalFormacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalFormacion entidad = new TPersonalFormacion();
                entidad = Mapper.Map<PersonalFormacionBO, TPersonalFormacion>(objetoBO,
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
        /// Repositorio: PersonalFormacionRepositorio
        /// Autor: Luis Huallpa - Britsel Calluchi.
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene formación del personal por idpersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> List<PersonalFormacionDTO> </returns>
        public List<PersonalFormacionDTO> ObtenerPersonalFormacion(int idPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdPersonal == idPersonal).Select(x => new PersonalFormacionDTO
				{
					Id = x.Id,
					AlaActualidad = x.AlaActualidad == null ? false : x.AlaActualidad.Value,
					FechaFin = x.FechaFin,
					FechaInicio = x.FechaInicio,
					IdAreaFormacion = x.IdAreaFormacion,
					IdCentroEstudio = x.IdCentroEstudio,
					IdEstadoEstudio = x.IdEstadoEstudio,
					IdPersonal = x.IdPersonal,
					IdTipoEstudio = x.IdTipoEstudio,
					Logro = x.Logro,
                    IdPersonalArchivo = x.IdPersonalArchivo
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        /// <summary>
		/// Obtiene lista de Familiares por personal
		/// </summary>
		/// <param name="idPersonal"></param>
		/// <returns></returns>
		public List<PersonalFormacionFormularioDTO> ObtenerPorPersonal(int IdPersonal)
        {
            try
            {
                string query = "SELECT * from [gp].[TPersonalFormacion_ObtenerPersonalFormacion] WHERE IdPersonal = @IdPersonal AND Estado = 1 ORDER BY FechaInicio DESC";
                string queryRespuesta = _dapper.QueryDapper(query, new { IdPersonal });
                return JsonConvert.DeserializeObject<List<PersonalFormacionFormularioDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
