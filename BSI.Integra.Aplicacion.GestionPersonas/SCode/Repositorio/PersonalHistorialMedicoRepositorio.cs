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
    /// Repositorio: PersonalHistorialMedicoRepositorio
    /// Autor: Luis Huallpa - Britsel Calluchi .
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_PersonalHistorialMedico
    /// </summary>
    public class PersonalHistorialMedicoRepositorio : BaseRepository<TPersonalHistorialMedico, PersonalHistorialMedicoBO>
    {
        #region Metodos Base
        public PersonalHistorialMedicoRepositorio() : base()
        {
        }
        public PersonalHistorialMedicoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalHistorialMedicoBO> GetBy(Expression<Func<TPersonalHistorialMedico, bool>> filter)
        {
            IEnumerable<TPersonalHistorialMedico> listado = base.GetBy(filter);
            List<PersonalHistorialMedicoBO> listadoBO = new List<PersonalHistorialMedicoBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalHistorialMedicoBO objetoBO = Mapper.Map<TPersonalHistorialMedico, PersonalHistorialMedicoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalHistorialMedicoBO FirstById(int id)
        {
            try
            {
                TPersonalHistorialMedico entidad = base.FirstById(id);
                PersonalHistorialMedicoBO objetoBO = new PersonalHistorialMedicoBO();
                Mapper.Map<TPersonalHistorialMedico, PersonalHistorialMedicoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalHistorialMedicoBO FirstBy(Expression<Func<TPersonalHistorialMedico, bool>> filter)
        {
            try
            {
                TPersonalHistorialMedico entidad = base.FirstBy(filter);
                PersonalHistorialMedicoBO objetoBO = Mapper.Map<TPersonalHistorialMedico, PersonalHistorialMedicoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalHistorialMedicoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalHistorialMedico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalHistorialMedicoBO> listadoBO)
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

        public bool Update(PersonalHistorialMedicoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalHistorialMedico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalHistorialMedicoBO> listadoBO)
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
        private void AsignacionId(TPersonalHistorialMedico entidad, PersonalHistorialMedicoBO objetoBO)
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

        private TPersonalHistorialMedico MapeoEntidad(PersonalHistorialMedicoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalHistorialMedico entidad = new TPersonalHistorialMedico();
                entidad = Mapper.Map<PersonalHistorialMedicoBO, TPersonalHistorialMedico>(objetoBO,
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
        /// Repositorio: PersonalHistorialMedicoRepositorio
        /// Autor: Luis Huallpa - Britsel Calluchi.
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene lista de historial medico del personal por idpersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> List<PersonalHistorialMedicoDTO> </returns>
        public List<PersonalHistorialMedicoDTO> ObtenerPersonalHistorialMedico(int idPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdPersonal == idPersonal).Select(x => new PersonalHistorialMedicoDTO
				{
					Id = x.Id,
					IdPersonal = x.IdPersonal,
					Enfermedad = x.Enfermedad,
					DetalleEnfermedad = x.DetalleEnfermedad,
					Periodo = x.Periodo
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        /// <summary>
		/// Obtiene Historial Médico por personal
		/// </summary>
		/// <param name="idPersonal"></param>
		/// <returns></returns>
		public List<PersonalHistorialMedicoFormularioDTO> ObtenerPorPersonal(int IdPersonal)
        {
            try
            {
                string query = "SELECT * from [gp].[TPersonalHistorialMedico_ObtenerPersonalHistorialMedico] WHERE IdPersonal = @IdPersonal AND Estado = 1";
                string queryRespuesta = _dapper.QueryDapper(query, new { IdPersonal });
                return JsonConvert.DeserializeObject<List<PersonalHistorialMedicoFormularioDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
