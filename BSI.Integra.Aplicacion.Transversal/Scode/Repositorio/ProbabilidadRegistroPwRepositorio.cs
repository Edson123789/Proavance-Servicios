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
    /// Repositorio: ProbabilidadRegistroPwRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Probabilidad de Registro
    /// </summary>
    public class ProbabilidadRegistroPwRepositorio : BaseRepository<TProbabilidadRegistroPw, ProbabilidadRegistroPwBO>
    {
        #region Metodos Base
        public ProbabilidadRegistroPwRepositorio() : base()
        {
        }
        public ProbabilidadRegistroPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProbabilidadRegistroPwBO> GetBy(Expression<Func<TProbabilidadRegistroPw, bool>> filter)
        {
            IEnumerable<TProbabilidadRegistroPw> listado = base.GetBy(filter);
            List<ProbabilidadRegistroPwBO> listadoBO = new List<ProbabilidadRegistroPwBO>();
            foreach (var itemEntidad in listado)
            {
                ProbabilidadRegistroPwBO objetoBO = Mapper.Map<TProbabilidadRegistroPw, ProbabilidadRegistroPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProbabilidadRegistroPwBO FirstById(int id)
        {
            try
            {
                TProbabilidadRegistroPw entidad = base.FirstById(id);
                ProbabilidadRegistroPwBO objetoBO = new ProbabilidadRegistroPwBO();
                Mapper.Map<TProbabilidadRegistroPw, ProbabilidadRegistroPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProbabilidadRegistroPwBO FirstBy(Expression<Func<TProbabilidadRegistroPw, bool>> filter)
        {
            try
            {
                TProbabilidadRegistroPw entidad = base.FirstBy(filter);
                ProbabilidadRegistroPwBO objetoBO = Mapper.Map<TProbabilidadRegistroPw, ProbabilidadRegistroPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProbabilidadRegistroPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProbabilidadRegistroPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProbabilidadRegistroPwBO> listadoBO)
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

        public bool Update(ProbabilidadRegistroPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProbabilidadRegistroPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProbabilidadRegistroPwBO> listadoBO)
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
        private void AsignacionId(TProbabilidadRegistroPw entidad, ProbabilidadRegistroPwBO objetoBO)
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

        private TProbabilidadRegistroPw MapeoEntidad(ProbabilidadRegistroPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProbabilidadRegistroPw entidad = new TProbabilidadRegistroPw();
                entidad = Mapper.Map<ProbabilidadRegistroPwBO, TProbabilidadRegistroPw>(objetoBO,
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

        /// Repositorio: ProbabilidadRegistroPwRepositorio
        /// Autor: Edgar S.
        /// Fecha: 08/03/2021
        /// <summary>
        /// Obtiene información para combo box
        /// </summary>
        /// <param></param>
        /// <returns> Lista de ObjetosDTO: List<ProbabilidadRegistroPwFiltroDTO> </returns>
        public List<ProbabilidadRegistroPwFiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<ProbabilidadRegistroPwFiltroDTO> probabilidadesRegistro = new List<ProbabilidadRegistroPwFiltroDTO>();
                var _query = "SELECT Id, Nombre FROM pla.V_TProbabilidadRegistro_ParaFiltro WHERE estado = 1";
                var probabilidadRegistroPwDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(probabilidadRegistroPwDB) && !probabilidadRegistroPwDB.Contains("[]"))
                {
                    probabilidadesRegistro = JsonConvert.DeserializeObject<List<ProbabilidadRegistroPwFiltroDTO>>(probabilidadRegistroPwDB);
                }
                return probabilidadesRegistro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
