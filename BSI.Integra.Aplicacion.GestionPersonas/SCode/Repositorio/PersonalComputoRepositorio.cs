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
    /// Repositorio: PersonalComputoRepositorio
    /// Autor: Britsel Calluchi - Luis Huallpa .
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_PersonalComputo
    /// </summary>
    public class PersonalComputoRepositorio : BaseRepository<TPersonalComputo, PersonalComputoBO>
    {
        #region Metodos Base
        public PersonalComputoRepositorio() : base()
        {
        }
        public PersonalComputoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalComputoBO> GetBy(Expression<Func<TPersonalComputo, bool>> filter)
        {
            IEnumerable<TPersonalComputo> listado = base.GetBy(filter);
            List<PersonalComputoBO> listadoBO = new List<PersonalComputoBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalComputoBO objetoBO = Mapper.Map<TPersonalComputo, PersonalComputoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalComputoBO FirstById(int id)
        {
            try
            {
                TPersonalComputo entidad = base.FirstById(id);
                PersonalComputoBO objetoBO = new PersonalComputoBO();
                Mapper.Map<TPersonalComputo, PersonalComputoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalComputoBO FirstBy(Expression<Func<TPersonalComputo, bool>> filter)
        {
            try
            {
                TPersonalComputo entidad = base.FirstBy(filter);
                PersonalComputoBO objetoBO = Mapper.Map<TPersonalComputo, PersonalComputoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalComputoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalComputo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalComputoBO> listadoBO)
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

        public bool Update(PersonalComputoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalComputo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalComputoBO> listadoBO)
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
        private void AsignacionId(TPersonalComputo entidad, PersonalComputoBO objetoBO)
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

        private TPersonalComputo MapeoEntidad(PersonalComputoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalComputo entidad = new TPersonalComputo();
                entidad = Mapper.Map<PersonalComputoBO, TPersonalComputo>(objetoBO,
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
        /// Repositorio: PersonalComputoRepositorio
        /// Autor: Luis Huallpa - Britsel Calluchi.
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtener personal computo por idpersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> List<PersonalInformaticaDTO> </returns>
        public List<PersonalInformaticaDTO> ObtenerPersonalComputo(int idPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdPersonal == idPersonal).Select(x => new PersonalInformaticaDTO
				{
					IdPersonal = x.IdPersonal,
					Id = x.Id,
					IdCentroEstudio = x.IdCentroEstudio,
					IdNivelCompetenciaTecnica = x.IdNivelCompetenciaTecnica,
					Programa = x.Programa,
                    IdPersonalArchivo = x.IdPersonalArchivo
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
        public List<PersonalComputoFormularioDTO> ObtenerPorPersonal(int IdPersonal)
        {
            try
            {
                string query = "SELECT * from [gp].[TPersonalComputo_ObtenerPersonalComputo] WHERE IdPersonal = @IdPersonal AND Estado = 1";
                string queryRespuesta = _dapper.QueryDapper(query, new { IdPersonal });
                return JsonConvert.DeserializeObject<List<PersonalComputoFormularioDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
