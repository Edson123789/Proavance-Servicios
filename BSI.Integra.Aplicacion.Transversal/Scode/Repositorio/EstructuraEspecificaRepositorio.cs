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
    /// Repositorio: EstructuraEspecifica
    /// Autor: Lourdes Priscila Pacsi Gamboa
    /// Fecha: 21/09/2021
    /// <summary>
    /// Repositorio para consultas de ope.T_EstructuraEspecifica
    /// </summary>
    public class EstructuraEspecificaRepositorio : BaseRepository<TEstructuraEspecifica, EstructuraEspecificaBO>
    {
        #region Metodos Base
        public EstructuraEspecificaRepositorio() : base()
        {
        }
        public EstructuraEspecificaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstructuraEspecificaBO> GetBy(Expression<Func<TEstructuraEspecifica, bool>> filter)
        {
            IEnumerable<TEstructuraEspecifica> listado = base.GetBy(filter);
            List<EstructuraEspecificaBO> listadoBO = new List<EstructuraEspecificaBO>();
            foreach (var itemEntidad in listado)
            {
                EstructuraEspecificaBO objetoBO = Mapper.Map<TEstructuraEspecifica, EstructuraEspecificaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstructuraEspecificaBO FirstById(int id)
        {
            try
            {
                TEstructuraEspecifica entidad = base.FirstById(id);
                EstructuraEspecificaBO objetoBO = new EstructuraEspecificaBO();
                Mapper.Map<TEstructuraEspecifica, EstructuraEspecificaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstructuraEspecificaBO FirstBy(Expression<Func<TEstructuraEspecifica, bool>> filter)
        {
            try
            {
                TEstructuraEspecifica entidad = base.FirstBy(filter);
                EstructuraEspecificaBO objetoBO = Mapper.Map<TEstructuraEspecifica, EstructuraEspecificaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstructuraEspecificaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstructuraEspecifica entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstructuraEspecificaBO> listadoBO)
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

        public bool Update(EstructuraEspecificaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstructuraEspecifica entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstructuraEspecificaBO> listadoBO)
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
        private void AsignacionId(TEstructuraEspecifica entidad, EstructuraEspecificaBO objetoBO)
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

        private TEstructuraEspecifica MapeoEntidad(EstructuraEspecificaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstructuraEspecifica entidad = new TEstructuraEspecifica();
                entidad = Mapper.Map<EstructuraEspecificaBO, TEstructuraEspecifica>(objetoBO,
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
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 21/09/2021
        /// Version: 1.0
        /// <summary>
        /// Manda la informacion serializada para al sp para el congelamiento de la estrucutura
        /// </summary>
        /// <param name="datos">Tipo de dato DatosEstructuraCurricularDTO</param>
        /// <param name="usuario">Usuario que esta congelando la estructura</param>
        /// <returns>retorna un objeto booleano</returns>
        public bool CongelarEstructuraAlumno(object datos, string usuario)
        {
            try
            {
                string Json = JsonConvert.SerializeObject(datos);
                var registroDB = _dapper.QuerySPFirstOrDefault("ope.SP_InsertarEstructuraEspecifica", new { Json, usuario });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 21/09/2021
        /// Version: 1.0
        /// <summary>
        /// Verifica si un alumno tiene congelado su estructura curricular
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera</param>
        /// <returns>retorna un objeto booleano</returns>
        public bool VerificacionEstructuraCongelada(int IdMatriculaCabecera)
        {
            try
            {
                List<DatosEstructuraEspecificaDTO> listaCongelamiento = new List<DatosEstructuraEspecificaDTO>();
                var query = "Select * From [ope].[T_EstructuraEspecifica] Where IdMatriculaCabecera=@IdMatriculaCabecera";
                var queryDB = _dapper.QueryDapper(query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("null"))
                {
                    listaCongelamiento = JsonConvert.DeserializeObject<List<DatosEstructuraEspecificaDTO>>(queryDB);
                }
                if(listaCongelamiento!= null)
                {
                    if(listaCongelamiento.Count() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
