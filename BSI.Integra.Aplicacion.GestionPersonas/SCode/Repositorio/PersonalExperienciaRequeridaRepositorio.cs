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
    public class PersonalExperienciaRequeridaRepositorio : BaseRepository<TPersonalExperienciaRequerida, PersonalExperienciaRequeridaBO>
    {
        #region Metodos Base
        public PersonalExperienciaRequeridaRepositorio() : base()
        {
        }
        public PersonalExperienciaRequeridaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalExperienciaRequeridaBO> GetBy(Expression<Func<TPersonalExperienciaRequerida, bool>> filter)
        {
            IEnumerable<TPersonalExperienciaRequerida> listado = base.GetBy(filter);
            List<PersonalExperienciaRequeridaBO> listadoBO = new List<PersonalExperienciaRequeridaBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalExperienciaRequeridaBO objetoBO = Mapper.Map<TPersonalExperienciaRequerida, PersonalExperienciaRequeridaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalExperienciaRequeridaBO FirstById(int id)
        {
            try
            {
                TPersonalExperienciaRequerida entidad = base.FirstById(id);
                PersonalExperienciaRequeridaBO objetoBO = new PersonalExperienciaRequeridaBO();
                Mapper.Map<TPersonalExperienciaRequerida, PersonalExperienciaRequeridaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalExperienciaRequeridaBO FirstBy(Expression<Func<TPersonalExperienciaRequerida, bool>> filter)
        {
            try
            {
                TPersonalExperienciaRequerida entidad = base.FirstBy(filter);
                PersonalExperienciaRequeridaBO objetoBO = Mapper.Map<TPersonalExperienciaRequerida, PersonalExperienciaRequeridaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalExperienciaRequeridaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalExperienciaRequerida entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalExperienciaRequeridaBO> listadoBO)
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

        public bool Update(PersonalExperienciaRequeridaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalExperienciaRequerida entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalExperienciaRequeridaBO> listadoBO)
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
        private void AsignacionId(TPersonalExperienciaRequerida entidad, PersonalExperienciaRequeridaBO objetoBO)
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

        private TPersonalExperienciaRequerida MapeoEntidad(PersonalExperienciaRequeridaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalExperienciaRequerida entidad = new TPersonalExperienciaRequerida();
                entidad = Mapper.Map<PersonalExperienciaRequeridaBO, TPersonalExperienciaRequerida>(objetoBO,
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
    }
}
