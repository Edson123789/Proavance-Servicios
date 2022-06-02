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
    /// Repositorio: PersonalInformacionMedicaRepositorio
    /// Autor: Britsel Calluchi - Luis Huallpa .
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_PersonalInformacionMedica
    /// </summary>
    public class PersonalInformacionMedicaRepositorio : BaseRepository<TPersonalInformacionMedica, PersonalInformacionMedicaBO>
    {
        #region Metodos Base
        public PersonalInformacionMedicaRepositorio() : base()
        {
        }
        public PersonalInformacionMedicaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalInformacionMedicaBO> GetBy(Expression<Func<TPersonalInformacionMedica, bool>> filter)
        {
            IEnumerable<TPersonalInformacionMedica> listado = base.GetBy(filter);
            List<PersonalInformacionMedicaBO> listadoBO = new List<PersonalInformacionMedicaBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalInformacionMedicaBO objetoBO = Mapper.Map<TPersonalInformacionMedica, PersonalInformacionMedicaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalInformacionMedicaBO FirstById(int id)
        {
            try
            {
                TPersonalInformacionMedica entidad = base.FirstById(id);
                PersonalInformacionMedicaBO objetoBO = new PersonalInformacionMedicaBO();
                Mapper.Map<TPersonalInformacionMedica, PersonalInformacionMedicaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalInformacionMedicaBO FirstBy(Expression<Func<TPersonalInformacionMedica, bool>> filter)
        {
            try
            {
                TPersonalInformacionMedica entidad = base.FirstBy(filter);
                PersonalInformacionMedicaBO objetoBO = Mapper.Map<TPersonalInformacionMedica, PersonalInformacionMedicaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalInformacionMedicaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalInformacionMedica entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalInformacionMedicaBO> listadoBO)
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

        public bool Update(PersonalInformacionMedicaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalInformacionMedica entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalInformacionMedicaBO> listadoBO)
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
        private void AsignacionId(TPersonalInformacionMedica entidad, PersonalInformacionMedicaBO objetoBO)
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

        private TPersonalInformacionMedica MapeoEntidad(PersonalInformacionMedicaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalInformacionMedica entidad = new TPersonalInformacionMedica();
                entidad = Mapper.Map<PersonalInformacionMedicaBO, TPersonalInformacionMedica>(objetoBO,
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
        /// Repositorio: PersonalInformacionMedicaRepositorio
        /// Autor: Luis Huallpa - Britsel Calluchi.
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene lista de informacion medica del persona por idpersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> List<PersonalInformacionMedicaDTO> </returns>
        public List<PersonalInformacionMedicaDTO> ObtenerPersonalInformacionMedica(int idPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdPersonal == idPersonal).Select(x => new PersonalInformacionMedicaDTO
				{
					Id = x.Id,
					IdPersonal = x.IdPersonal,
					Alergia = x.Alergia,
					Precaucion = x.Precaucion
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        /// <summary>
        /// Obtiene información médica por personal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<PersonalInformacionMedicaFormularioDTO> ObtenerPorPersonal(int IdPersonal)
        {
            try
            {
                string query = "SELECT * from [gp].[TPersonalInformacionMedica_ObtenerPersonalInformacionMedica] WHERE IdPersonal = @IdPersonal AND Estado = 1";
                string queryRespuesta = _dapper.QueryDapper(query, new { IdPersonal });
                return JsonConvert.DeserializeObject<List<PersonalInformacionMedicaFormularioDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
