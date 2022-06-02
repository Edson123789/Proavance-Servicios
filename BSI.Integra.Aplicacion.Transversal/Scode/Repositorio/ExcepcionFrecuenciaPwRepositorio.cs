using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: ExcepcionFrecuenciaPwRepositorio
    /// Autor:Jose Villena
    /// Fecha: 03/05/2021
    /// <summary>
    /// Gestión de ExcepcionFrecuenciaPwRepositorio
    /// </summary>
    public class ExcepcionFrecuenciaPwRepositorio : BaseRepository<TExcepcionFrecuenciaPw, ExcepcionFrecuenciaPwBO>
    {
        #region Metodos Base
        public ExcepcionFrecuenciaPwRepositorio() : base()
        {
        }
        public ExcepcionFrecuenciaPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ExcepcionFrecuenciaPwBO> GetBy(Expression<Func<TExcepcionFrecuenciaPw, bool>> filter)
        {
            IEnumerable<TExcepcionFrecuenciaPw> listado = base.GetBy(filter);
            List<ExcepcionFrecuenciaPwBO> listadoBO = new List<ExcepcionFrecuenciaPwBO>();
            foreach (var itemEntidad in listado)
            {
                ExcepcionFrecuenciaPwBO objetoBO = Mapper.Map<TExcepcionFrecuenciaPw, ExcepcionFrecuenciaPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ExcepcionFrecuenciaPwBO FirstById(int id)
        {
            try
            {
                TExcepcionFrecuenciaPw entidad = base.FirstById(id);
                ExcepcionFrecuenciaPwBO objetoBO = new ExcepcionFrecuenciaPwBO();
                Mapper.Map<TExcepcionFrecuenciaPw, ExcepcionFrecuenciaPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ExcepcionFrecuenciaPwBO FirstBy(Expression<Func<TExcepcionFrecuenciaPw, bool>> filter)
        {
            try
            {
                TExcepcionFrecuenciaPw entidad = base.FirstBy(filter);
                ExcepcionFrecuenciaPwBO objetoBO = Mapper.Map<TExcepcionFrecuenciaPw, ExcepcionFrecuenciaPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ExcepcionFrecuenciaPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TExcepcionFrecuenciaPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ExcepcionFrecuenciaPwBO> listadoBO)
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

        public bool Update(ExcepcionFrecuenciaPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TExcepcionFrecuenciaPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ExcepcionFrecuenciaPwBO> listadoBO)
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
        private void AsignacionId(TExcepcionFrecuenciaPw entidad, ExcepcionFrecuenciaPwBO objetoBO)
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

        private TExcepcionFrecuenciaPw MapeoEntidad(ExcepcionFrecuenciaPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TExcepcionFrecuenciaPw entidad = new TExcepcionFrecuenciaPw();
                entidad = Mapper.Map<ExcepcionFrecuenciaPwBO, TExcepcionFrecuenciaPw>(objetoBO,
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


        ///Repositorio: ExcepcionFrecuenciaPwRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene toda las excepciones de programas generales
        /// </summary>
        /// <returns> Lista Excepciones Programa generales: List<ExcepcionFrecuenciaPGeneralDTO></returns> 
        public List<ExcepcionFrecuenciaPGeneralDTO> ObtenerTodoProgramaGeneral() {
            try
            {
                List<ExcepcionFrecuenciaPGeneralDTO> excepcionesFrecuenciaPGeneral = new List<ExcepcionFrecuenciaPGeneralDTO>();
                string query = "SELECT Id, IdPEspecifico FROM pla.V_TExcepcionFrecuenciaPW_ObtenerExcepcionesPEspecifico WHERE Estado = 1";
                var parametroSEOProgramaGeneralDB = _dapper.QueryDapper(query, null);
                excepcionesFrecuenciaPGeneral = JsonConvert.DeserializeObject<List<ExcepcionFrecuenciaPGeneralDTO>>(parametroSEOProgramaGeneralDB);
                return excepcionesFrecuenciaPGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
