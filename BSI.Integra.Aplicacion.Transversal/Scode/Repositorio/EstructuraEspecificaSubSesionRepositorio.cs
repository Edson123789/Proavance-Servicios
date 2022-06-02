using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.DTO;
namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class EstructuraEspecificaSubSesionRepositorio : BaseRepository<TEstructuraEspecificaSubSesion, EstructuraEspecificaSubSesionBO>
    {
        /// Repositorio: EstructuraEspecificaSubSesion
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha:21/09/2021
        /// <summary>
        /// Repositorio para consultas de ope.T_EstructuraEspecificaSubSesion
        /// </summary>
        #region Metodos Base
        public EstructuraEspecificaSubSesionRepositorio() : base()
        {
        }
        public EstructuraEspecificaSubSesionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstructuraEspecificaSubSesionBO> GetBy(Expression<Func<TEstructuraEspecificaSubSesion, bool>> filter)
        {
            IEnumerable<TEstructuraEspecificaSubSesion> listado = base.GetBy(filter);
            List<EstructuraEspecificaSubSesionBO> listadoBO = new List<EstructuraEspecificaSubSesionBO>();
            foreach (var itemEntidad in listado)
            {
                EstructuraEspecificaSubSesionBO objetoBO = Mapper.Map<TEstructuraEspecificaSubSesion, EstructuraEspecificaSubSesionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstructuraEspecificaSubSesionBO FirstById(int id)
        {
            try
            {
                TEstructuraEspecificaSubSesion entidad = base.FirstById(id);
                EstructuraEspecificaSubSesionBO objetoBO = new EstructuraEspecificaSubSesionBO();
                Mapper.Map<TEstructuraEspecificaSubSesion, EstructuraEspecificaSubSesionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstructuraEspecificaSubSesionBO FirstBy(Expression<Func<TEstructuraEspecificaSubSesion, bool>> filter)
        {
            try
            {
                TEstructuraEspecificaSubSesion entidad = base.FirstBy(filter);
                EstructuraEspecificaSubSesionBO objetoBO = Mapper.Map<TEstructuraEspecificaSubSesion, EstructuraEspecificaSubSesionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstructuraEspecificaSubSesionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstructuraEspecificaSubSesion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstructuraEspecificaSubSesionBO> listadoBO)
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

        public bool Update(EstructuraEspecificaSubSesionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstructuraEspecificaSubSesion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstructuraEspecificaSubSesionBO> listadoBO)
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
        private void AsignacionId(TEstructuraEspecificaSubSesion entidad, EstructuraEspecificaSubSesionBO objetoBO)
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

        private TEstructuraEspecificaSubSesion MapeoEntidad(EstructuraEspecificaSubSesionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstructuraEspecificaSubSesion entidad = new TEstructuraEspecificaSubSesion();
                entidad = Mapper.Map<EstructuraEspecificaSubSesionBO, TEstructuraEspecificaSubSesion>(objetoBO,
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
