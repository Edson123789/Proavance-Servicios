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
    /// Repositorio: EstructuraEspecificaTarea
    /// Autor: Lourdes Priscila Pacsi Gamboa
    /// Fecha:21/09/2021
    /// <summary>
    /// Repositorio para consultas de ope.T_EstructuraEspecificaTarea
    /// </summary>
    public class EstructuraEspecificaTareaRepositorio : BaseRepository<TEstructuraEspecificaTarea, EstructuraEspecificaTareaBO>
    {
        #region Metodos Base
        public EstructuraEspecificaTareaRepositorio() : base()
        {
        }
        public EstructuraEspecificaTareaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstructuraEspecificaTareaBO> GetBy(Expression<Func<TEstructuraEspecificaTarea, bool>> filter)
        {
            IEnumerable<TEstructuraEspecificaTarea> listado = base.GetBy(filter);
            List<EstructuraEspecificaTareaBO> listadoBO = new List<EstructuraEspecificaTareaBO>();
            foreach (var itemEntidad in listado)
            {
                EstructuraEspecificaTareaBO objetoBO = Mapper.Map<TEstructuraEspecificaTarea, EstructuraEspecificaTareaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstructuraEspecificaTareaBO FirstById(int id)
        {
            try
            {
                TEstructuraEspecificaTarea entidad = base.FirstById(id);
                EstructuraEspecificaTareaBO objetoBO = new EstructuraEspecificaTareaBO();
                Mapper.Map<TEstructuraEspecificaTarea, EstructuraEspecificaTareaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstructuraEspecificaTareaBO FirstBy(Expression<Func<TEstructuraEspecificaTarea, bool>> filter)
        {
            try
            {
                TEstructuraEspecificaTarea entidad = base.FirstBy(filter);
                EstructuraEspecificaTareaBO objetoBO = Mapper.Map<TEstructuraEspecificaTarea, EstructuraEspecificaTareaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstructuraEspecificaTareaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstructuraEspecificaTarea entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstructuraEspecificaTareaBO> listadoBO)
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

        public bool Update(EstructuraEspecificaTareaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstructuraEspecificaTarea entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstructuraEspecificaTareaBO> listadoBO)
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
        private void AsignacionId(TEstructuraEspecificaTarea entidad, EstructuraEspecificaTareaBO objetoBO)
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

        private TEstructuraEspecificaTarea MapeoEntidad(EstructuraEspecificaTareaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstructuraEspecificaTarea entidad = new TEstructuraEspecificaTarea();
                entidad = Mapper.Map<EstructuraEspecificaTareaBO, TEstructuraEspecificaTarea>(objetoBO,
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
