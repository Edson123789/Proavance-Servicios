using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Helper;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PersonalRecursoHabilidadRepositorio : BaseRepository<TPersonalRecursoHabilidad, PersonalRecursoHabilidadBO>
    {
        #region Metodos Base
        public PersonalRecursoHabilidadRepositorio() : base()
        {
        }
        public PersonalRecursoHabilidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalRecursoHabilidadBO> GetBy(Expression<Func<TPersonalRecursoHabilidad, bool>> filter)
        {
            IEnumerable<TPersonalRecursoHabilidad> listado = base.GetBy(filter);
            List<PersonalRecursoHabilidadBO> listadoBO = new List<PersonalRecursoHabilidadBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalRecursoHabilidadBO objetoBO = Mapper.Map<TPersonalRecursoHabilidad, PersonalRecursoHabilidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalRecursoHabilidadBO FirstById(int id)
        {
            try
            {
                TPersonalRecursoHabilidad entidad = base.FirstById(id);
                PersonalRecursoHabilidadBO objetoBO = new PersonalRecursoHabilidadBO();
                Mapper.Map<TPersonalRecursoHabilidad, PersonalRecursoHabilidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalRecursoHabilidadBO FirstBy(Expression<Func<TPersonalRecursoHabilidad, bool>> filter)
        {
            try
            {
                TPersonalRecursoHabilidad entidad = base.FirstBy(filter);
                PersonalRecursoHabilidadBO objetoBO = Mapper.Map<TPersonalRecursoHabilidad, PersonalRecursoHabilidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(PersonalRecursoHabilidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalRecursoHabilidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalRecursoHabilidadBO> listadoBO)
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

        public bool Update(PersonalRecursoHabilidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalRecursoHabilidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalRecursoHabilidadBO> listadoBO)
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
        private void AsignacionId(TPersonalRecursoHabilidad entidad, PersonalRecursoHabilidadBO objetoBO)
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

        private TPersonalRecursoHabilidad MapeoEntidad(PersonalRecursoHabilidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalRecursoHabilidad entidad = new TPersonalRecursoHabilidad();
                entidad = Mapper.Map<PersonalRecursoHabilidadBO, TPersonalRecursoHabilidad>(objetoBO,
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

        public void DeleteLogicoPorHabilidad(int IdPersonalRecurso, string usuario, List<PersonalRecursoHabilidadDTO> nuevos)
        {
            try
            {
                List<PersonalRecursoHabilidadDTO> listaBorrar = new List<PersonalRecursoHabilidadDTO>();
                string _query = "SELECT Id FROM  pla.T_PersonalRecursoHabilidad WHERE Estado = 1 and IdPersonalRecurso = @IdPersonalRecurso";
                var query = _dapper.QueryDapper(_query, new { IdPersonalRecurso });
                listaBorrar = JsonConvert.DeserializeObject<List<PersonalRecursoHabilidadDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdPersonalRecurso == x.IdPersonalRecurso));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PersonalRecursoHabilidadDTO> ObtenerPersonalRecursoHabilidadPorId (int Id)
        {
            try
            {
                List<PersonalRecursoHabilidadDTO> beneficiosCodigoMatricula = new List<PersonalRecursoHabilidadDTO>();
                var _query = "SELECT Id,IdPersonalRecurso,IdHabilidadSimulador ,Puntaje FROM pla.T_PersonalRecursoHabilidad where IdPersonalRecurso = @Id and Estado= 1";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(_query, new { Id});
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<PersonalRecursoHabilidadDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<DetalleHabilidadPersonalDTO> ObtenerPersonalRecursoHabilidadDetalle(int Id)
        {
            try
            {
                List<DetalleHabilidadPersonalDTO> beneficiosCodigoMatricula = new List<DetalleHabilidadPersonalDTO>();
                var _query = "SELECT Id,NombrePersonal,NombreHabilidad,IdHabilidad ,Puntaje FROM [ope].[V_PersonalRecursoHabilidad] where IdPersonalRecurso = @Id";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(_query, new { Id });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<DetalleHabilidadPersonalDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }



    }
}
