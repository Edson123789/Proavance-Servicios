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
    /// Repositorio: PersonalCeseRepositorio
    /// Autor: Ansoli Espinoza - Luis Huallpa.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_PersonalCese
    /// </summary>
    public class PersonalCeseRepositorio : BaseRepository<TPersonalCese, PersonalCeseBO>
    {
        #region Metodos Base
        public PersonalCeseRepositorio() : base()
        {
        }
        public PersonalCeseRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalCeseBO> GetBy(Expression<Func<TPersonalCese, bool>> filter)
        {
            IEnumerable<TPersonalCese> listado = base.GetBy(filter);
            List<PersonalCeseBO> listadoBO = new List<PersonalCeseBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalCeseBO objetoBO = Mapper.Map<TPersonalCese, PersonalCeseBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalCeseBO FirstById(int id)
        {
            try
            {
                TPersonalCese entidad = base.FirstById(id);
                PersonalCeseBO objetoBO = new PersonalCeseBO();
                Mapper.Map<TPersonalCese, PersonalCeseBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalCeseBO FirstBy(Expression<Func<TPersonalCese, bool>> filter)
        {
            try
            {
                TPersonalCese entidad = base.FirstBy(filter);
                PersonalCeseBO objetoBO = Mapper.Map<TPersonalCese, PersonalCeseBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalCeseBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalCese entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalCeseBO> listadoBO)
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

        public bool Update(PersonalCeseBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalCese entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalCeseBO> listadoBO)
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
        private void AsignacionId(TPersonalCese entidad, PersonalCeseBO objetoBO)
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

        private TPersonalCese MapeoEntidad(PersonalCeseBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalCese entidad = new TPersonalCese();
                entidad = Mapper.Map<PersonalCeseBO, TPersonalCese>(objetoBO,
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
        /// Obtiene el motivo y fecha de cese por IdPersonal
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public PersonalCeseDTO ObtenerMotivoFecha(int Id)
        {
            var cese = GetBy(x => x.Id == Id, y => new PersonalCeseDTO
            {
                Id = y.Id,
                IdMotivoCese = y.IdMotivoCese,
                FechaCese = y.FechaCese
            }).FirstOrDefault();
            return cese;
        }
        /// Repositorio: PersonalCeseRepositorio
		/// Autor: Luis Huallpa - Britsel Calluchi - Edgar Serruto.
		/// Fecha: 16/06/2021
		/// <summary>
		/// Obtiene el motivo y fecha de cese por IdPersonal
		/// </summary>
		/// <param name="id"> Id de Personal </param>
		/// <returns> PersonalCeseDTO </returns>
		public PersonalCeseDTO ObtenerMotivoFechaUltimo(int id)
		{
			var cese = GetBy(x => x.IdPersonal == id, y => new PersonalCeseDTO
			{
				Id = y.Id,
				IdMotivoCese = y.IdMotivoCese,
				FechaCese = y.FechaCese
			}).OrderByDescending(x=>x.FechaCese).FirstOrDefault();
			return cese;
		}
	}
}
