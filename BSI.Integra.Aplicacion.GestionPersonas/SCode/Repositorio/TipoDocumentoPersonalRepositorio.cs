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
    /// Repositorio: TipoDocumentoPersonalRepositorio
    /// Autor: Ansoli Espinoza
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_TipoDocumentoPersonal
    /// </summary>
    public class TipoDocumentoPersonalRepositorio : BaseRepository<TTipoDocumentoPersonal, PersonalHorarioBO>
    {
        #region Metodos Base
        public TipoDocumentoPersonalRepositorio() : base()
        {
        }
        public TipoDocumentoPersonalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalHorarioBO> GetBy(Expression<Func<TTipoDocumentoPersonal, bool>> filter)
        {
            IEnumerable<TTipoDocumentoPersonal> listado = base.GetBy(filter);
            List<PersonalHorarioBO> listadoBO = new List<PersonalHorarioBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalHorarioBO objetoBO = Mapper.Map<TTipoDocumentoPersonal, PersonalHorarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalHorarioBO FirstById(int id)
        {
            try
            {
                TTipoDocumentoPersonal entidad = base.FirstById(id);
                PersonalHorarioBO objetoBO = new PersonalHorarioBO();
                Mapper.Map<TTipoDocumentoPersonal, PersonalHorarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalHorarioBO FirstBy(Expression<Func<TTipoDocumentoPersonal, bool>> filter)
        {
            try
            {
                TTipoDocumentoPersonal entidad = base.FirstBy(filter);
                PersonalHorarioBO objetoBO = Mapper.Map<TTipoDocumentoPersonal, PersonalHorarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalHorarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDocumentoPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalHorarioBO> listadoBO)
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

        public bool Update(PersonalHorarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDocumentoPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalHorarioBO> listadoBO)
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
        private void AsignacionId(TTipoDocumentoPersonal entidad, PersonalHorarioBO objetoBO)
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

        private TTipoDocumentoPersonal MapeoEntidad(PersonalHorarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDocumentoPersonal entidad = new TTipoDocumentoPersonal();
                entidad = Mapper.Map<PersonalHorarioBO, TTipoDocumentoPersonal>(objetoBO,
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
        /// Repositorio: TipoDocumentoPersonalRepositorio
        /// Autor: 
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene el Id y Nombre para ComboBox
        /// </summary>
        /// <returns> List<FiltroIdNombreDTO> </returns>
        public List<FiltroIdNombreDTO> GetFiltroIdNombre()
        {
            var lista = GetBy(x => true, y => new FiltroIdNombreDTO
            {
                Id = y.Id,
                Nombre = y.Nombre
            }).ToList();
            return lista;
        }
    }
}
