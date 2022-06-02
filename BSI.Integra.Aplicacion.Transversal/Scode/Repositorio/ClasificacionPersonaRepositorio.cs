using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Comercial/ClasificacionPersona
    /// Autor: Jose Villena.
    /// Fecha: 28/04/2021
    /// <summary>
    /// Repositorio para consultas de fin.T_ClasificacionPersona
    /// </summary>
    public class ClasificacionPersonaRepositorio : BaseRepository<TClasificacionPersona, ClasificacionPersonaBO>
    {
        #region Metodos Base
        public ClasificacionPersonaRepositorio() : base()
        {
        }
        public ClasificacionPersonaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ClasificacionPersonaBO> GetBy(Expression<Func<TClasificacionPersona, bool>> filter)
        {
            IEnumerable<TClasificacionPersona> listado = base.GetBy(filter);
            List<ClasificacionPersonaBO> listadoBO = new List<ClasificacionPersonaBO>();
            foreach (var itemEntidad in listado)
            {
                ClasificacionPersonaBO objetoBO = Mapper.Map<TClasificacionPersona, ClasificacionPersonaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ClasificacionPersonaBO FirstById(int id)
        {
            try
            {
                TClasificacionPersona entidad = base.FirstById(id);
                ClasificacionPersonaBO objetoBO = new ClasificacionPersonaBO();
                Mapper.Map<TClasificacionPersona, ClasificacionPersonaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ClasificacionPersonaBO FirstBy(Expression<Func<TClasificacionPersona, bool>> filter)
        {
            try
            {
                TClasificacionPersona entidad = base.FirstBy(filter);
                ClasificacionPersonaBO objetoBO = Mapper.Map<TClasificacionPersona, ClasificacionPersonaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ClasificacionPersonaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TClasificacionPersona entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ClasificacionPersonaBO> listadoBO)
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

        public bool Update(ClasificacionPersonaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TClasificacionPersona entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ClasificacionPersonaBO> listadoBO)
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
        private void AsignacionId(TClasificacionPersona entidad, ClasificacionPersonaBO objetoBO)
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

        private TClasificacionPersona MapeoEntidad(ClasificacionPersonaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TClasificacionPersona entidad = new TClasificacionPersona();
                entidad = Mapper.Map<ClasificacionPersonaBO, TClasificacionPersona>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ClasificacionPersonaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TClasificacionPersona, bool>>> filters, Expression<Func<TClasificacionPersona, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TClasificacionPersona> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ClasificacionPersonaBO> listadoBO = new List<ClasificacionPersonaBO>();

            foreach (var itemEntidad in listado)
            {
                ClasificacionPersonaBO objetoBO = Mapper.Map<TClasificacionPersona, ClasificacionPersonaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion


        ///Repositorio: PersonaRepositorio
        ///Autor: Jose Villena.
        ///Fecha: 28/04/2021
        /// <summary>
        /// Indica si existe un tipo de persona para una persona
        /// </summary>
        /// <param name="idPersona">Id Persona</param>
        /// <param name="tipoPersona">Tipo Persona</param>        
        /// <returns>Bool</returns>
        public bool ExistePorTipoPersona(int idPersona, TipoPersona tipoPersona)
        {
            try
            {
                return this.Exist(x => x.IdPersona == idPersona && x.IdTipoPersona == (int)tipoPersona);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
