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
    public class PersonalRecursoRepositorio : BaseRepository<TPersonalRecurso, PersonalRecursoBO>
    {
        #region Metodos Base
        public PersonalRecursoRepositorio() : base()
        {
        }
        public PersonalRecursoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalRecursoBO> GetBy(Expression<Func<TPersonalRecurso, bool>> filter)
        {
            IEnumerable<TPersonalRecurso> listado = base.GetBy(filter);
            List<PersonalRecursoBO> listadoBO = new List<PersonalRecursoBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalRecursoBO objetoBO = Mapper.Map<TPersonalRecurso, PersonalRecursoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalRecursoBO FirstById(int id)
        {
            try
            {
                TPersonalRecurso entidad = base.FirstById(id);
                PersonalRecursoBO objetoBO = new PersonalRecursoBO();
                Mapper.Map<TPersonalRecurso, PersonalRecursoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalRecursoBO FirstBy(Expression<Func<TPersonalRecurso, bool>> filter)
        {
            try
            {
                TPersonalRecurso entidad = base.FirstBy(filter);
                PersonalRecursoBO objetoBO = Mapper.Map<TPersonalRecurso, PersonalRecursoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(PersonalRecursoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalRecurso entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalRecursoBO> listadoBO)
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

        public bool Update(PersonalRecursoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalRecurso entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalRecursoBO> listadoBO)
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
        private void AsignacionId(TPersonalRecurso entidad, PersonalRecursoBO objetoBO)
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

        private TPersonalRecurso MapeoEntidad(PersonalRecursoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalRecurso entidad = new TPersonalRecurso();
                entidad = Mapper.Map<PersonalRecursoBO, TPersonalRecurso>(objetoBO,
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

        public List<PersonalRecursoDTO> ObtenerHabilidadesSimulador()
        {
            try
            {
                List<PersonalRecursoDTO> recurso = new List<PersonalRecursoDTO>();
                var _query = "SELECT Id,NombrePersonal,ApellidosPersonal,DescripcionPersonal,UrlfotoPersonal,CostoHorario,IdMoneda,Productividad,IdTipoDisponibilidadPersonal FROM [pla].[T_PersonalRecurso] where Estado = 1";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    recurso = JsonConvert.DeserializeObject<List<PersonalRecursoDTO>>(beneficiosCodigoMatriculaDB);
                }
                return recurso;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
