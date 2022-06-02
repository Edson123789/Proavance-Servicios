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
    /// Repositorio: PersonalIdiomaRepositorio
    /// Autor: Britsel Calluchi - Luis Huallpa .
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_PersonalIdioma
    /// </summary>
    public class PersonalIdiomaRepositorio : BaseRepository<TPersonalIdioma, PersonalIdiomaBO>
    {
        #region Metodos Base
        public PersonalIdiomaRepositorio() : base()
        {
        }
        public PersonalIdiomaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalIdiomaBO> GetBy(Expression<Func<TPersonalIdioma, bool>> filter)
        {
            IEnumerable<TPersonalIdioma> listado = base.GetBy(filter);
            List<PersonalIdiomaBO> listadoBO = new List<PersonalIdiomaBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalIdiomaBO objetoBO = Mapper.Map<TPersonalIdioma, PersonalIdiomaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalIdiomaBO FirstById(int id)
        {
            try
            {
                TPersonalIdioma entidad = base.FirstById(id);
                PersonalIdiomaBO objetoBO = new PersonalIdiomaBO();
                Mapper.Map<TPersonalIdioma, PersonalIdiomaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalIdiomaBO FirstBy(Expression<Func<TPersonalIdioma, bool>> filter)
        {
            try
            {
                TPersonalIdioma entidad = base.FirstBy(filter);
                PersonalIdiomaBO objetoBO = Mapper.Map<TPersonalIdioma, PersonalIdiomaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalIdiomaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalIdioma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalIdiomaBO> listadoBO)
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

        public bool Update(PersonalIdiomaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalIdioma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalIdiomaBO> listadoBO)
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
        private void AsignacionId(TPersonalIdioma entidad, PersonalIdiomaBO objetoBO)
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

        private TPersonalIdioma MapeoEntidad(PersonalIdiomaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalIdioma entidad = new TPersonalIdioma();
                entidad = Mapper.Map<PersonalIdiomaBO, TPersonalIdioma>(objetoBO,
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
        /// Repositorio: PersonalIdiomaRepositorio
        /// Autor: Luis Huallpa - Britsel Calluchi.
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene lista de idiomas que conoce el persona por el idpersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> List<PersonalIdiomasDTO> </returns>
        public List<PersonalIdiomasDTO> ObtenerPersonalIdioma(int idPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdPersonal == idPersonal).Select(x => new PersonalIdiomasDTO
				{
					Id = x.Id,
					IdPersonal = x.IdPersonal,
					IdCentroEstudio = x.IdCentroEstudio,
					IdIdioma = x.IdIdioma,
					IdNivelIdioma = x.IdNivelIdioma,
                    IdPersonalArchivo = x.IdPersonalArchivo
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        /// <summary>
        /// Obtiene lista de Idioma por personal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<PersonalIdiomaFormularioDTO> ObtenerPorPersonal(int IdPersonal)
        {
            try
            {
                string query = "SELECT * from [gp].[TPersonalIdioma_ObtenerPersonalIdioma] WHERE IdPersonal = @IdPersonal AND Estado = 1";
                string queryRespuesta = _dapper.QueryDapper(query, new { IdPersonal });
                return JsonConvert.DeserializeObject<List<PersonalIdiomaFormularioDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
