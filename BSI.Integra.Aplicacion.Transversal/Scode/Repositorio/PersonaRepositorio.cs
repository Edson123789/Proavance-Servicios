using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Comercial/Persona
    /// Autor: Jose Villena.
    /// Fecha: 28/04/2021
    /// <summary>
    /// Repositorio para consultas de conf.T_Persona
    /// </summary>
    public class PersonaRepositorio : BaseRepository<TPersona, PersonaBO>
    {
        #region Metodos Base
        public PersonaRepositorio() : base()
        {
        }
        public PersonaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonaBO> GetBy(Expression<Func<TPersona, bool>> filter)
        {
            IEnumerable<TPersona> listado = base.GetBy(filter);
            List<PersonaBO> listadoBO = new List<PersonaBO>();
            foreach (var itemEntidad in listado)
            {
                PersonaBO objetoBO = Mapper.Map<TPersona, PersonaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonaBO FirstById(int id)
        {
            try
            {
                TPersona entidad = base.FirstById(id);
                PersonaBO objetoBO = new PersonaBO();
                Mapper.Map<TPersona, PersonaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonaBO FirstBy(Expression<Func<TPersona, bool>> filter)
        {
            try
            {
                TPersona entidad = base.FirstBy(filter);
                PersonaBO objetoBO = Mapper.Map<TPersona, PersonaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersona entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonaBO> listadoBO)
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

        public bool Update(PersonaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersona entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonaBO> listadoBO)
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
        private void AsignacionId(TPersona entidad, PersonaBO objetoBO)
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

        private TPersona MapeoEntidad(PersonaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersona entidad = new TPersona();
                entidad = Mapper.Map<PersonaBO, TPersona>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ClasificacionPersona != null)
                {
                    TClasificacionPersona entidadHijo = new TClasificacionPersona();
                    entidadHijo = Mapper.Map<ClasificacionPersonaBO, TClasificacionPersona>(objetoBO.ClasificacionPersona,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TClasificacionPersona.Add(entidadHijo);
                }
                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<PersonaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPersona, bool>>> filters, Expression<Func<TPersona, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TPersona> listado = base.GetFiltered(filters, orderBy, ascending);
            List<PersonaBO> listadoBO = new List<PersonaBO>();

            foreach (var itemEntidad in listado)
            {
                PersonaBO objetoBO = Mapper.Map<TPersona, PersonaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion


        ///Repositorio: PersonaRepositorio
        ///Autor: Jose Villena.
        ///Fecha: 28/04/2021
        /// <summary>
        /// Indica si existe una persona con ese email
        /// </summary>
        /// <param name="email"> Email Persona </param>
        /// <returns>Bool</returns>
        public bool ExistePorEmail(string email) {
            try
            {
                return this.Exist(x => x.Email1 == email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
