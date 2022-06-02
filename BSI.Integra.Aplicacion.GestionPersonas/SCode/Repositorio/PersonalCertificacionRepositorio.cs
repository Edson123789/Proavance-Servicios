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
    /// Repositorio: PersonalCertificacionRepositorio
    /// Autor: Luis Huallpa - Britsel Calluchi - Edgar Serruto.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_PersonalCertificacion
    /// </summary>
    public class PersonalCertificacionRepositorio : BaseRepository<TPersonalCertificacion, PersonalCertificacionBO>
    {
        #region Metodos Base
        public PersonalCertificacionRepositorio() : base()
        {
        }
        public PersonalCertificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalCertificacionBO> GetBy(Expression<Func<TPersonalCertificacion, bool>> filter)
        {
            IEnumerable<TPersonalCertificacion> listado = base.GetBy(filter);
            List<PersonalCertificacionBO> listadoBO = new List<PersonalCertificacionBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalCertificacionBO objetoBO = Mapper.Map<TPersonalCertificacion, PersonalCertificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalCertificacionBO FirstById(int id)
        {
            try
            {
                TPersonalCertificacion entidad = base.FirstById(id);
                PersonalCertificacionBO objetoBO = new PersonalCertificacionBO();
                Mapper.Map<TPersonalCertificacion, PersonalCertificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalCertificacionBO FirstBy(Expression<Func<TPersonalCertificacion, bool>> filter)
        {
            try
            {
                TPersonalCertificacion entidad = base.FirstBy(filter);
                PersonalCertificacionBO objetoBO = Mapper.Map<TPersonalCertificacion, PersonalCertificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalCertificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalCertificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalCertificacionBO> listadoBO)
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

        public bool Update(PersonalCertificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalCertificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalCertificacionBO> listadoBO)
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
        private void AsignacionId(TPersonalCertificacion entidad, PersonalCertificacionBO objetoBO)
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

        private TPersonalCertificacion MapeoEntidad(PersonalCertificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalCertificacion entidad = new TPersonalCertificacion();
                entidad = Mapper.Map<PersonalCertificacionBO, TPersonalCertificacion>(objetoBO,
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
        /// Repositorio: PersonalCertificacionRepositorio
        /// Autor: Luis Huallpa - Britsel Calluchi.
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene lista de certificaciones del personal por idpersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> List<PersonalCertificacionDTO> </returns>
        public List<PersonalCertificacionDTO> ObtenerPersonalCertificacion(int idPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdPersonal == idPersonal).Select(x => new PersonalCertificacionDTO
				{
					Id = x.Id,
					IdPersonal = x.IdPersonal,
					FechaCertificacion = x.FechaCertificacion,
					Programa = x.Programa,
                    IdPersonalArchivo = x.IdPersonalArchivo,
                    IdCentroEstudio = x.IdCentroEstudio
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }
}
