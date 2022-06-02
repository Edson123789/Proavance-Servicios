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
    /// Repositorio: PersonalSistemaPensionarioRepositorio
    /// Autor: Britsel Calluchi - Luis Huallpa .
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_PersonalSistemaPensionario
    /// </summary>
    public class PersonalSistemaPensionarioRepositorio : BaseRepository<TPersonalSistemaPensionario, PersonalSistemaPensionarioBO>
    {
        #region Metodos Base
        public PersonalSistemaPensionarioRepositorio() : base()
        {
        }
        public PersonalSistemaPensionarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalSistemaPensionarioBO> GetBy(Expression<Func<TPersonalSistemaPensionario, bool>> filter)
        {
            IEnumerable<TPersonalSistemaPensionario> listado = base.GetBy(filter);
            List<PersonalSistemaPensionarioBO> listadoBO = new List<PersonalSistemaPensionarioBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalSistemaPensionarioBO objetoBO = Mapper.Map<TPersonalSistemaPensionario, PersonalSistemaPensionarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalSistemaPensionarioBO FirstById(int id)
        {
            try
            {
                TPersonalSistemaPensionario entidad = base.FirstById(id);
                PersonalSistemaPensionarioBO objetoBO = new PersonalSistemaPensionarioBO();
                Mapper.Map<TPersonalSistemaPensionario, PersonalSistemaPensionarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalSistemaPensionarioBO FirstBy(Expression<Func<TPersonalSistemaPensionario, bool>> filter)
        {
            try
            {
                TPersonalSistemaPensionario entidad = base.FirstBy(filter);
                PersonalSistemaPensionarioBO objetoBO = Mapper.Map<TPersonalSistemaPensionario, PersonalSistemaPensionarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalSistemaPensionarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalSistemaPensionario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalSistemaPensionarioBO> listadoBO)
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

        public bool Update(PersonalSistemaPensionarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalSistemaPensionario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalSistemaPensionarioBO> listadoBO)
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
        private void AsignacionId(TPersonalSistemaPensionario entidad, PersonalSistemaPensionarioBO objetoBO)
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

        private TPersonalSistemaPensionario MapeoEntidad(PersonalSistemaPensionarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalSistemaPensionario entidad = new TPersonalSistemaPensionario();
                entidad = Mapper.Map<PersonalSistemaPensionarioBO, TPersonalSistemaPensionario>(objetoBO,
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
        /// Repositorio: PersonalSistemaPensionarioRepositorio
        /// Autor: Luis Huallpa - Britsel Calluchi.
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene el sistema pensionario actual del personal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> List<PersonalSistemaPensionarioDTO> </returns>
        public List<PersonalSistemaPensionarioDTO> ObtenerPersonalSistemaPensionario(int idPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdPersonal == idPersonal).OrderByDescending(x => x.FechaModificacion).Select(x => new PersonalSistemaPensionarioDTO
				{
					IdSistemaPensionario = x.IdSistemaPensionario,
					IdEntidadSistemaPensionario = x.IdEntidadSistemaPensionario,
					CodigoAfiliado = x.CodigoAfiliado,
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
