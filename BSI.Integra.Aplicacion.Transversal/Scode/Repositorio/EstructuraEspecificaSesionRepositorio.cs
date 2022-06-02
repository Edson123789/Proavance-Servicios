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
    /// Repositorio: EstructuraEspecificaSesion
    /// Autor: Lourdes Priscila Pacsi Gamboa
    /// Fecha:21/09/2021
    /// <summary>
    /// Repositorio para consultas de ope.T_EstructuraEspecificaSesion
    /// </summary>
    public class EstructuraEspecificaSesionRepositorio : BaseRepository<TEstructuraEspecificaSesion, EstructuraEspecificaSesionBO>
    {
        #region Metodos Base
        public EstructuraEspecificaSesionRepositorio() : base()
        {
        }
        public EstructuraEspecificaSesionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstructuraEspecificaSesionBO> GetBy(Expression<Func<TEstructuraEspecificaSesion, bool>> filter)
        {
            IEnumerable<TEstructuraEspecificaSesion> listado = base.GetBy(filter);
            List<EstructuraEspecificaSesionBO> listadoBO = new List<EstructuraEspecificaSesionBO>();
            foreach (var itemEntidad in listado)
            {
                EstructuraEspecificaSesionBO objetoBO = Mapper.Map<TEstructuraEspecificaSesion, EstructuraEspecificaSesionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstructuraEspecificaSesionBO FirstById(int id)
        {
            try
            {
                TEstructuraEspecificaSesion entidad = base.FirstById(id);
                EstructuraEspecificaSesionBO objetoBO = new EstructuraEspecificaSesionBO();
                Mapper.Map<TEstructuraEspecificaSesion, EstructuraEspecificaSesionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstructuraEspecificaSesionBO FirstBy(Expression<Func<TEstructuraEspecificaSesion, bool>> filter)
        {
            try
            {
                TEstructuraEspecificaSesion entidad = base.FirstBy(filter);
                EstructuraEspecificaSesionBO objetoBO = Mapper.Map<TEstructuraEspecificaSesion, EstructuraEspecificaSesionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstructuraEspecificaSesionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstructuraEspecificaSesion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstructuraEspecificaSesionBO> listadoBO)
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

        public bool Update(EstructuraEspecificaSesionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstructuraEspecificaSesion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstructuraEspecificaSesionBO> listadoBO)
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
        private void AsignacionId(TEstructuraEspecificaSesion entidad, EstructuraEspecificaSesionBO objetoBO)
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

        private TEstructuraEspecificaSesion MapeoEntidad(EstructuraEspecificaSesionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstructuraEspecificaSesion entidad = new TEstructuraEspecificaSesion();
                entidad = Mapper.Map<EstructuraEspecificaSesionBO, TEstructuraEspecificaSesion>(objetoBO,
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
