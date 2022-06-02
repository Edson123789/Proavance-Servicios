using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: PersonalSeguroSaludRepositorio
    /// Autor: Britsel Calluchi - Luis Huallpa.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_PersonalSeguroSalud
    /// </summary>
    public class PersonalSeguroSaludRepositorio : BaseRepository<TPersonalSeguroSalud, PersonalSeguroSaludBO>
    {
        #region Metodos Base
        public PersonalSeguroSaludRepositorio() : base()
        {
        }
        public PersonalSeguroSaludRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalSeguroSaludBO> GetBy(Expression<Func<TPersonalSeguroSalud, bool>> filter)
        {
            IEnumerable<TPersonalSeguroSalud> listado = base.GetBy(filter);
            List<PersonalSeguroSaludBO> listadoBO = new List<PersonalSeguroSaludBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalSeguroSaludBO objetoBO = Mapper.Map<TPersonalSeguroSalud, PersonalSeguroSaludBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalSeguroSaludBO FirstById(int id)
        {
            try
            {
                TPersonalSeguroSalud entidad = base.FirstById(id);
                PersonalSeguroSaludBO objetoBO = new PersonalSeguroSaludBO();
                Mapper.Map<TPersonalSeguroSalud, PersonalSeguroSaludBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalSeguroSaludBO FirstBy(Expression<Func<TPersonalSeguroSalud, bool>> filter)
        {
            try
            {
                TPersonalSeguroSalud entidad = base.FirstBy(filter);
                PersonalSeguroSaludBO objetoBO = Mapper.Map<TPersonalSeguroSalud, PersonalSeguroSaludBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalSeguroSaludBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalSeguroSalud entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalSeguroSaludBO> listadoBO)
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

        public bool Update(PersonalSeguroSaludBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalSeguroSalud entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalSeguroSaludBO> listadoBO)
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
        private void AsignacionId(TPersonalSeguroSalud entidad, PersonalSeguroSaludBO objetoBO)
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

        private TPersonalSeguroSalud MapeoEntidad(PersonalSeguroSaludBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalSeguroSalud entidad = new TPersonalSeguroSalud();
                entidad = Mapper.Map<PersonalSeguroSaludBO, TPersonalSeguroSalud>(objetoBO,
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
        /// Repositorio: PersonalSeguroSaludRepositorio
        /// Autor: Luis Huallpa - Britsel Calluchi.
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene el seguro de salud registrado actual del personal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> List<PersonalSeguroSaludDTO> </returns>
        public List<PersonalSeguroSaludDTO> ObtenerPersonalSeguroSalud(int idPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdPersonal == idPersonal).OrderByDescending(x => x.FechaModificacion).Select(x => new PersonalSeguroSaludDTO
				{
					IdEntidadSeguroSalud = x.IdEntidadSeguroSalud,
					Activo = x.Activo,
					FechaModificacion = x.FechaModificacion,
					UsuarioModificacion = x.UsuarioModificacion
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }
}
